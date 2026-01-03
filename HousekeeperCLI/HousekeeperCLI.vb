Option Strict On

Module HousekeeperCLI

    Sub Main(ByVal Args() As String)
        'If Not Args.Count = 4 Then
        '    'MsgBox("Argument error")
        '    End
        'End If
        Dim ArgsString As String = ""
        For Each s As String In Args
            If s.Contains(" ") Then
                ArgsString = $"{ArgsString} ""{s}"""
            Else
                ArgsString = $"{ArgsString} {s}"
            End If
        Next

        Dim Source = $"{System.IO.Path.GetTempPath()}Housekeeper"
        Dim ActiveFileExtensionsList As New List(Of String)
        ActiveFileExtensionsList.Add("*.log")

        Dim FoundFiles = FileIO.FileSystem.GetFiles(Source,
                                     FileIO.SearchOption.SearchTopLevelOnly,
                                     ActiveFileExtensionsList.ToArray)

        Dim NewFoundFiles As New List(Of String)
        NewFoundFiles.Add($"{Source}\junk.log")
        NewFoundFiles.Add($"{Source}\junk2.log")

        Dim P As New Diagnostics.Process

        P.StartInfo.FileName = "C:\data\CAD\scripts\SolidEdgeHousekeeper\bin\Debug\Housekeeper.exe"
        'P.StartInfo.Arguments = "-p ""Preset 123"" -l ""test 02.txt"""
        P.StartInfo.Arguments = ArgsString
        P.StartInfo.RedirectStandardOutput = True
        P.StartInfo.UseShellExecute = False
        'If Me.HideConsoleWindow Then
        '    P.StartInfo.RedirectStandardError = True
        '    P.StartInfo.UseShellExecute = False
        '    P.StartInfo.CreateNoWindow = True
        'End If
        P.Start()
        'If Me.HideConsoleWindow Then PSError = P.StandardError.ReadToEnd
        'End If

        'If Not PSError = "" Then
        '    TaskLogger.AddMessage("The external program reported the following error")
        '    TaskLogger.AddMessage(PSError)
        'End If

        P.WaitForExit()
        Dim ExitCode = P.ExitCode

        Console.WriteLine("RECEIVED ARGUMENTS")
        For Each s As String In Args
            Console.WriteLine(s)
        Next

        Console.WriteLine("EXISTING LOG FILES")
        For Each s As String In FoundFiles
            Console.WriteLine(s)
        Next

        Console.WriteLine("NEW LOG FILES")
        For Each s As String In NewFoundFiles
            If Not FoundFiles.Contains(s) Then
                Console.WriteLine(s)
            End If
        Next

        'MsgBox(ArgsString)
    End Sub

End Module
