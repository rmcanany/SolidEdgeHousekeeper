Option Strict On

'Imports System.Runtime.InteropServices

Module HousekeeperCLI

    ' Passed to AllowSetForegroundWindow to lift the restriction for every
    ' process rather than one specific PID.
    Private Const ASFW_ANY As Integer = -1

    <Runtime.InteropServices.DllImport("user32.dll")>
    Private Function AllowSetForegroundWindow(dwProcessId As Integer) As Boolean
    End Function

    Sub Main(ByVal Args() As String)

        ' Opt-in switch: see the AllowSetForegroundWindow call below.  Housekeeper.exe's
        ' own argument parser (Form_Main's constructor) requires an exact argument count
        ' and errors out on anything it doesn't recognize, so -af is consumed here and
        ' never forwarded along in ArgsString.
        Dim AllowForeground As Boolean = Args.Any(Function(a) a.ToLower = "-af")
        Dim FilteredArgs = Args.Where(Function(a) a.ToLower <> "-af").ToArray()

        Dim ArgsString As String = ""
        Dim PreviousArg As String = ""
        For Each s As String In FilteredArgs  ' eg. [-p, SetDocumentStatus_T2, -l, .\file list.txt] ' Note some args have space characters.

            ' Resolve the -l filelist path to an absolute path here, against this
            ' process's own working directory, before passing it along -- so it
            ' reaches Housekeeper.exe unambiguous regardless of what working
            ' directory it ends up running with.
            Dim ArgValue As String = s
            If PreviousArg.ToLower = "-l" Then
                ArgValue = IO.Path.GetFullPath(s)
            End If

            If ArgValue.Contains(" ") Then
                ArgsString = $"{ArgsString} ""{ArgValue}"""
            Else
                ArgsString = $"{ArgsString} {ArgValue}"
            End If

            PreviousArg = s
        Next

        Dim LogDirectory = $"{System.IO.Path.GetTempPath()}Housekeeper"  ' GetTempPath already has an ending '\'
        Dim ActiveFileExtensionsList As New List(Of String)
        ActiveFileExtensionsList.Add("*.log")

        Dim OldLogFiles = FileIO.FileSystem.GetFiles(LogDirectory,
                                     FileIO.SearchOption.SearchTopLevelOnly,
                                     ActiveFileExtensionsList.ToArray)

        Dim P As New Diagnostics.Process

        'MsgBox($"AppDomain.CurrentDomain.BaseDirectory {AppDomain.CurrentDomain.BaseDirectory}")

        'P.StartInfo.FileName = "C:\data\CAD\scripts\SolidEdgeHousekeeper\bin\Debug\Housekeeper.exe"
        P.StartInfo.FileName = $"{AppDomain.CurrentDomain.BaseDirectory}Housekeeper.exe"
        P.StartInfo.Arguments = ArgsString

        P.StartInfo.RedirectStandardOutput = True
        P.StartInfo.RedirectStandardError = True
        P.StartInfo.UseShellExecute = False

        ' Redirecting a stream without ever reading it lets the OS pipe buffer fill,
        ' which makes the child block on its next write to that stream -- a classic
        ' Process deadlock.  Drain both streams asynchronously instead of only
        ' checking for new log files after WaitForExit().
        '
        ' Both streams are forwarded to OUR stderr, not stdout: this process's
        ' stdout is a contract with the calling script (it reports new log file
        ' paths, one per line, after the child exits -- see NewLogFiles below).
        ' Forwarding the child's own console spew onto that same stream mixes
        ' arbitrary lines in with the log paths, and a caller that opens every
        ' stdout line as a file (eg. TestTaskCLI.ps1) ends up spawning Notepad
        ' once per stray line.
        ' Solid Edge's own DXF flat-pattern translator reports export progress as
        ' "Progress: NN.NN%" lines (preceded by one "Meter Limit: NN" line, followed
        ' by "------- Stopped" lines) with no way to disable it.  It's routine noise,
        ' not diagnostic signal, so it's filtered out here rather than forwarded.
        Dim IsNoiseLine As Func(Of String, Boolean) =
            Function(line) line.StartsWith("Progress:") OrElse line.StartsWith("Meter Limit:") OrElse line.StartsWith("------- Stopped")

        AddHandler P.OutputDataReceived, Sub(sender, e)
                                             If e.Data IsNot Nothing AndAlso Not IsNoiseLine(e.Data) Then Console.Error.WriteLine(e.Data)
                                         End Sub
        AddHandler P.ErrorDataReceived, Sub(sender, e)
                                            If e.Data IsNot Nothing AndAlso Not IsNoiseLine(e.Data) Then Console.Error.WriteLine(e.Data)
                                        End Sub

        P.Start()

        ' Windows' focus-stealing prevention can let a process launched by another
        ' background process (this one, itself started by a script) appear on
        ' screen without ever actually holding true foreground/active status,
        ' even though Activate()/WindowState calls report success.  Housekeeper's
        ' Solid Edge automation (eg. the flat-pattern DXF exporter) appears to
        ' depend on genuinely having that status, which is why a batch launched
        ' this way can hang on calls that never hang when Housekeeper is started
        ' directly by the user.
        '
        ' Lifting the restriction is opt-in (-af) rather than automatic: ASFW_ANY
        ' doesn't just cover Housekeeper.exe/edge.exe, it lifts the restriction for
        ' every process system-wide until the next SetForegroundWindow call or user
        ' input resets it, and -- separately from that -- it also means Solid Edge
        ' and/or Housekeeper's own window can genuinely steal keyboard focus from
        ' whatever else the user is actively working in.  So far this has only been
        ' needed for one preset (saving flat patterns as DXF) out of many, so it's
        ' not worth that tradeoff by default.
        If AllowForeground Then
            AllowSetForegroundWindow(ASFW_ANY)
        End If

        P.BeginOutputReadLine()
        P.BeginErrorReadLine()

        P.WaitForExit()
        Dim ExitCode = P.ExitCode

        Dim NewLogFiles = FileIO.FileSystem.GetFiles(LogDirectory,
                                     FileIO.SearchOption.SearchTopLevelOnly,
                                     ActiveFileExtensionsList.ToArray)


        ' ###### These are written to stdout to be processed by the calling program
        'Console.WriteLine($"Command line: '{ArgsString}'")

        ' Report new log files found, if any.
        For Each s As String In NewLogFiles
            If Not OldLogFiles.Contains(s) Then
                Console.WriteLine(s)
            End If
        Next

    End Sub

End Module
