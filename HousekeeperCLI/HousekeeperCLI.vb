Option Strict On

Module HousekeeperCLI

    Sub Main(ByVal Args() As String)

        Dim ArgsString As String = ""
        For Each s As String In Args  ' eg. [-p, SetDocumentStatus_T2, -l, .\file list.txt] ' Note some args have space characters.
            If s.Contains(" ") Then
                ArgsString = $"{ArgsString} ""{s}"""
            Else
                ArgsString = $"{ArgsString} {s}"
            End If
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
        P.StartInfo.UseShellExecute = False

        P.Start()

        P.WaitForExit()
        Dim ExitCode = P.ExitCode

        Dim NewLogFiles = FileIO.FileSystem.GetFiles(LogDirectory,
                                     FileIO.SearchOption.SearchTopLevelOnly,
                                     ActiveFileExtensionsList.ToArray)


        ' ###### These are written to stdout which are received by the calling program
        Console.WriteLine($"Command line: '{ArgsString}'")

        ' List any new log files found, if any
        For Each s As String In NewLogFiles
            If Not OldLogFiles.Contains(s) Then
                Console.WriteLine(s)
            End If
        Next

    End Sub

End Module
