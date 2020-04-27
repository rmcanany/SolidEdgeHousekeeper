Partial Class Form1

    Private Function GetFileNames(ByVal FileWildcard As String) As List(Of String)
        ' Build up list of files to process depending on which option was selected.
        ' Dim intFileNameCount As Integer = 0
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
            'FoundFilesArray = FoundFiles.ToArray
            FoundFilesList = FoundFiles.ToList
        End If

        If RadioButtonFilesDirectoryOnly.Checked Then
            FoundFiles = FileIO.FileSystem.GetFiles(TextBoxInputDirectory.Text,
                                    FileIO.SearchOption.SearchTopLevelOnly,
                                    FileWildcard)
            'FoundFilesArray = FoundFiles.ToArray
            FoundFilesList = FoundFiles.ToList
        End If

        If RadioButtonFilesSelected.Checked Then
            If ListBoxFiles.SelectedItems.Count > 0 Then
                For i As Integer = 0 To ListBoxFiles.SelectedItems.Count - 1
                    Filename = ListBoxFiles.SelectedItems(i)
                    If System.IO.Path.GetExtension(Filename) = FileExtension Then
                        Filename = TextBoxInputDirectory.Text + "\" + Filename
                        FoundFilesList.Add(Filename)
                    End If
                Next

            End If
        End If

        TextBoxStatus.Text = "Found " + FoundFilesArray.Count.ToString + " file(s)"

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

    Private Function IsCheckedFilesToProcess()
        Dim TF As Boolean

        TF = RadioButtonFilesDirectoriesAndSubdirectories.Checked
        TF = TF Or RadioButtonFilesDirectoryOnly.Checked
        TF = TF Or RadioButtonFilesSelected.Checked

        Return TF
    End Function

    Private Function GetActiveFileExtensions() As String()
        Dim ActiveFileExtensionsList As New List(Of String)

        If CheckBoxFileTypeAssembly.Checked Then
            ActiveFileExtensionsList.Add("*.asm")
        End If
        If CheckBoxFileTypePart.Checked Then
            ActiveFileExtensionsList.Add("*.par")
        End If
        If CheckBoxFileTypeSheetmetal.Checked Then
            ActiveFileExtensionsList.Add("*.psm")
        End If
        If CheckBoxFileTypeDraft.Checked Then
            ActiveFileExtensionsList.Add("*.dft")
        End If

        Return ActiveFileExtensionsList.ToArray
    End Function

    Private Sub UpdateFileTypes()
        If CheckedListBoxAssembly.CheckedItems.Count > 0 Then
            CheckBoxFileTypeAssembly.Checked = True
        Else
            CheckBoxFileTypeAssembly.Checked = False
        End If
        If CheckedListBoxPart.CheckedItems.Count > 0 Then
            CheckBoxFileTypePart.Checked = True
        Else
            CheckBoxFileTypePart.Checked = False
        End If
        If CheckedListBoxSheetmetal.CheckedItems.Count > 0 Then
            CheckBoxFileTypeSheetmetal.Checked = True
        Else
            CheckBoxFileTypeSheetmetal.Checked = False
        End If
        If CheckedListBoxDraft.CheckedItems.Count > 0 Then
            CheckBoxFileTypeDraft.Checked = True
        Else
            CheckBoxFileTypeDraft.Checked = False
        End If

        UpdateListBoxFiles()
    End Sub

    Private Sub UpdateListBoxFiles()
        Dim FoundFiles As IReadOnlyCollection(Of String)
        Dim FoundFile As String

        ListBoxFiles.Items.Clear()

        FoundFiles = FileIO.FileSystem.GetFiles(TextBoxInputDirectory.Text,
                                    FileIO.SearchOption.SearchTopLevelOnly,
                                    GetActiveFileExtensions())

        For Each FoundFile In FoundFiles
            ListBoxFiles.Items.Add(System.IO.Path.GetFileName(FoundFile))
        Next

    End Sub

    Private Sub UpdateTemplateRequired()
        'Go through CheckedListBox items to see if any require a template
        Dim LabelText As String

        'Assembly
        TemplateRequiredAssembly = False
        For Each LabelText In CheckedListBoxAssembly.CheckedItems
            If ListToTemplateAssembly(LabelText) Then
                TemplateRequiredAssembly = True
            End If
        Next

        'Part
        TemplateRequiredPart = False
        For Each LabelText In CheckedListBoxPart.CheckedItems
            If ListToTemplatePart(LabelText) Then
                TemplateRequiredPart = True
            End If
        Next

        'Sheetmetal
        TemplateRequiredSheetmetal = False
        For Each LabelText In CheckedListBoxSheetmetal.CheckedItems
            If ListToTemplateSheetmetal(LabelText) Then
                TemplateRequiredSheetmetal = True
            End If
        Next

        'Draft
        TemplateRequiredDraft = False
        For Each LabelText In CheckedListBoxDraft.CheckedItems
            If ListToTemplateDraft(LabelText) Then
                TemplateRequiredDraft = True
            End If
        Next
    End Sub

End Class