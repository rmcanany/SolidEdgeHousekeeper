Partial Class Form1

    Private Function TruncateFullPath(ByVal Path As String) As String
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
        ByVal ErrorMessage As String,
        ByVal Path As String,
        ByVal SupplementalErrorMessage As String
        )

        Try
            Using writer As New IO.StreamWriter(LogfileName, True)
                writer.WriteLine(ErrorMessage + ": " + Path)
                writer.WriteLine(SupplementalErrorMessage)
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