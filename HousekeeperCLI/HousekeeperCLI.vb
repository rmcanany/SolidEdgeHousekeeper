Option Strict On

Module HousekeeperCLI

    Sub Main(ByVal Args() As String)
        'If Not Args.Count = 4 Then
        '    'MsgBox("Argument error")
        '    End
        'End If

        Dim ArgsString As String = ""
        For Each s As String In Args  ' eg. [-p, SetDocumentStatus_T2, -l, .\file list.txt] ' Note last arg has a space character in it.
            If s.Contains(" ") Then
                ArgsString = $"{ArgsString} ""{s}"""
            Else
                ArgsString = $"{ArgsString} {s}"
            End If
        Next

        'MsgBox(ArgsString)

        Dim Source = $"{System.IO.Path.GetTempPath()}Housekeeper"  ' GetTempPath already has an ending '\'
        Dim ActiveFileExtensionsList As New List(Of String)
        ActiveFileExtensionsList.Add("*.log")

        Dim FoundFiles = FileIO.FileSystem.GetFiles(Source,
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

        Dim NewFoundFiles = FileIO.FileSystem.GetFiles(Source,
                                     FileIO.SearchOption.SearchTopLevelOnly,
                                     ActiveFileExtensionsList.ToArray)


        ' ###### These are written to stdout which are received by the calling program
        Console.WriteLine("RECEIVED ARGUMENTS")
        For Each s As String In Args
            Console.WriteLine(s)
        Next

        Console.WriteLine("NEW LOG FILES")
        For Each s As String In NewFoundFiles
            If Not FoundFiles.Contains(s) Then
                Console.WriteLine(s)
            End If
        Next

    End Sub

End Module
