Option Strict On

Partial Class Form1

    Public Function TruncateFullPath(ByVal Path As String) As String
        Dim Length As Integer = Len(TextBoxInputDirectory.Text)
        Dim NewPath As String

        If Path.Contains(TextBoxInputDirectory.Text) Then
            NewPath = Path.Remove(0, Length)
            NewPath = "~" + NewPath
        Else
            NewPath = Path
        End If
        Return NewPath
    End Function

    Private Sub LogfileAppend(
        ByVal Filename As String,
        ByVal ErrorMessagesCombined As Dictionary(Of String, List(Of String))
        )

        Try
            Using writer As New IO.StreamWriter(LogfileName, True)
                writer.WriteLine(TruncateFullPath(Filename))
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

        ErrorsOccurred = True
    End Sub

    Private Sub LogfileSetName()
        Dim Timestamp As String = System.DateTime.Now.ToString("yyyyMMdd_HHmmss")
        LogfileName = TextBoxInputDirectory.Text + "\Housekeeper_" + Timestamp + ".log"

        ErrorsOccurred = False
    End Sub


End Class