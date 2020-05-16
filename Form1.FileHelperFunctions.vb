Option Strict On

Partial Class Form1

    Private Function GetFileNames(ByVal FileWildcard As String) As List(Of String)
        ' Build up list of files to process depending on which option was selected.
        Dim FoundFiles As ObjectModel.ReadOnlyCollection(Of String)
        Dim FoundFilesArray As String() = {}
        Dim FoundFilesList As New List(Of String)
        Dim FileExtension As String = FileWildcard.Replace("*", "")
        Dim Filename As String

        TextBoxStatus.Text = "Getting files..."

        If RadioButtonFilesDirectoriesAndSubdirectories.Checked Then
            FoundFiles = FileIO.FileSystem.GetFiles(TextBoxInputDirectory.Text,
                                    FileIO.SearchOption.SearchAllSubDirectories,
                                    FileWildcard)
            FoundFilesList = FoundFiles.ToList
        End If

        If RadioButtonFilesDirectoryOnly.Checked Then
            FoundFiles = FileIO.FileSystem.GetFiles(TextBoxInputDirectory.Text,
                                    FileIO.SearchOption.SearchTopLevelOnly,
                                    FileWildcard)
            FoundFilesList = FoundFiles.ToList
        End If

        If RadioButtonFilesSelected.Checked Then
            If ListBoxFiles.SelectedItems.Count > 0 Then
                For i As Integer = 0 To ListBoxFiles.SelectedItems.Count - 1
                    Filename = CType(ListBoxFiles.SelectedItems(i), String)
                    If System.IO.Path.GetExtension(Filename) = FileExtension Then
                        Filename = TextBoxInputDirectory.Text + "\" + Filename
                        FoundFilesList.Add(Filename)
                    End If
                Next
            End If
        End If

        Return FoundFilesList
    End Function

    Private Function GetTotalFilesToProcess() As Integer
        Dim Count As Integer = 0

        If CheckBoxFileTypeAssembly.Checked Then
            Count += GetFileNames("*.asm").Count
        End If
        If CheckBoxFileTypePart.Checked Then
            Count += GetFileNames("*.par").Count
        End If
        If CheckBoxFileTypeSheetmetal.Checked Then
            Count += GetFileNames("*.psm").Count
        End If
        If CheckBoxFileTypeDraft.Checked Then
            Count += GetFileNames("*.dft").Count
        End If

        Return Count
    End Function

    Private Function IsCheckedFilesToProcess() As Boolean
        Dim TF As Boolean

        TF = RadioButtonFilesDirectoriesAndSubdirectories.Checked
        TF = TF Or RadioButtonFilesDirectoryOnly.Checked
        TF = TF Or RadioButtonFilesSelected.Checked

        Return TF
    End Function

End Class