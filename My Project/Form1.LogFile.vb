Option Strict On

Partial Class Form1

    Private Sub LogfileAppend(
        ByVal Filename As String,
        ByVal ErrorMessagesCombined As Dictionary(Of String, List(Of String))
        )
        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim TODOFile As String = String.Format("{0}\{1}", StartupPath, "todo.txt")

        Try
            Using writer As New IO.StreamWriter(MissingFilesFileName, True)
                writer.WriteLine(CommonTasks.TruncateFullPath(Filename, Nothing))
                For Each Key In ErrorMessagesCombined.Keys
                    writer.WriteLine(String.Format("    {0}", Key))
                    If ErrorMessagesCombined(Key).Count > 0 Then
                        For Each Line In ErrorMessagesCombined(Key)
                            writer.WriteLine(String.Format("        {0}", Line))
                        Next
                    End If
                Next
                writer.WriteLine("")
            End Using
        Catch ex As Exception
            MsgBox("Error saving logfile")
        End Try

        'If CheckBoxCreateTODOList.Checked Then
        '    Using writer As New IO.StreamWriter(TODOFile, True)
        '        writer.WriteLine(Filename)
        '    End Using
        'End If

        ErrorsOccurred = True
    End Sub

    Private Sub LogfileSetName()
        Dim Timestamp As String = System.DateTime.Now.ToString("yyyyMMdd_HHmmss")
        MissingFilesFileName = IO.Path.GetTempPath + "\Housekeeper_" + Timestamp + ".log"

        ErrorsOccurred = False
    End Sub

    Private Sub ClearTODOList()
        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim TODOFile As String = String.Format("{0}\{1}", StartupPath, "todo.txt")

        System.IO.File.Create(TODOFile).Dispose()
    End Sub

End Class