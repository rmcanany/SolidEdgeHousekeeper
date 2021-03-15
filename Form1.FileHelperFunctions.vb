Option Strict On

Partial Class Form1

    Private Sub UpdateListBoxFiles()
        ' Update ListBoxFiles on Form1

        Dim FoundFiles As IReadOnlyCollection(Of String) = Nothing
        Dim FoundFile As String
        Dim ActiveFileExtensionsList As New List(Of String)
        Dim tf As Boolean

        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim TODOFile As String = String.Format("{0}\{1}", StartupPath, "todo.txt")

        ListBoxFilesOutOfDate = False

        ButtonUpdateListBoxFiles.BackColor = System.Drawing.SystemColors.Control
        ButtonUpdateListBoxFiles.UseVisualStyleBackColor = True

        tf = RadioButtonFilesDirectoriesAndSubdirectories.Checked = False
        tf = tf And RadioButtonFilesDirectoryOnly.Checked = False
        tf = tf And RadioButtonTopLevelAssembly.Checked = False
        tf = tf And RadioButtonTODOList.Checked = False
        If tf Then
            MsgBox("Select an option for files to process")
            Exit Sub
        End If

        StopProcess = False
        ButtonCancel.Text = "Stop"

        If CheckBoxFilterAsm.Checked Then
            ActiveFileExtensionsList.Add("*.asm")
        End If
        If CheckBoxFilterPar.Checked Then
            ActiveFileExtensionsList.Add("*.par")
        End If
        If CheckBoxFilterPsm.Checked Then
            ActiveFileExtensionsList.Add("*.psm")
        End If
        If CheckBoxFilterDft.Checked Then
            ActiveFileExtensionsList.Add("*.dft")
        End If

        ListBoxFiles.Items.Clear()

        If ActiveFileExtensionsList.Count > 0 Then
            If FileIO.FileSystem.DirectoryExists(TextBoxInputDirectory.Text) Then
                If RadioButtonFilesDirectoriesAndSubdirectories.Checked Then
                    FoundFiles = FileIO.FileSystem.GetFiles(TextBoxInputDirectory.Text,
                                    FileIO.SearchOption.SearchAllSubDirectories,
                                    ActiveFileExtensionsList.ToArray)

                ElseIf RadioButtonFilesDirectoryOnly.Checked Then
                    FoundFiles = FileIO.FileSystem.GetFiles(TextBoxInputDirectory.Text,
                                    FileIO.SearchOption.SearchTopLevelOnly,
                                    ActiveFileExtensionsList.ToArray)

                ElseIf RadioButtonTopLevelAssembly.Checked Then
                    Dim TLAU As New TopLevelAssemblyUtilities(Me)

                    TextBoxStatus.Text = "Finding all linked files.  This may take some time."

                    If RadioButtonTLABottomUp.Checked Then
                        FoundFiles = TLAU.GetLinks("BottomUp", TextBoxInputDirectory.Text,
                                               TextBoxTopLevelAssembly.Text,
                                               ActiveFileExtensionsList)
                    Else
                        FoundFiles = TLAU.GetLinks("TopDown", TextBoxInputDirectory.Text,
                                               TextBoxTopLevelAssembly.Text,
                                               ActiveFileExtensionsList,
                                               Report:=CheckBoxTLAReportUnrelatedFiles.Checked)
                    End If

                    TextBoxStatus.Text = ""

                ElseIf RadioButtonTODOList.Checked Then
                    If FileIO.FileSystem.FileExists(TODOFile) Then
                        FoundFiles = IO.File.ReadAllLines(TODOFile)

                    End If

                Else  ' No RadioButton on FilesToProcess checked
                    FoundFiles = Nothing
                End If

                If Not FoundFiles Is Nothing Then

                    ' Filter by properties
                    If CheckBoxEnablePropertyFilter.Checked Then
                        System.Threading.Thread.Sleep(1000)
                        Dim PropertyFilter As New PropertyFilter(Me)
                        FoundFiles = PropertyFilter.PropertyFilter(FoundFiles, PropertyFilterDict, PropertyFilterFormula)
                    End If

                    ' Filter by file wildcard search
                    If CheckBoxFileSearch.Checked Then
                        FoundFiles = FileWildcardSearch(FoundFiles, TextBoxFileSearch.Text)
                    End If

                    Dim MaxFilenameLength As Integer
                    MaxFilenameLength = 0
                    Dim BaseFilename As String
                    For Each FoundFile In FoundFiles
                        BaseFilename = FoundFile.Replace(TextBoxInputDirectory.Text, "")
                        'BaseFilename = System.IO.Path.GetFileName(FoundFile)
                        If Len(BaseFilename) > MaxFilenameLength Then
                            MaxFilenameLength = Len(BaseFilename)
                        End If
                        ListBoxFiles.Items.Add(BaseFilename)
                    Next
                    ListBoxFiles.ColumnWidth = CInt(CDbl(TextBoxColumnWidth.Text) * MaxFilenameLength)
                    ' MsgBox(MaxFilenameLength)

                    TextBoxStatus.Text = String.Format("{0} files found", FoundFiles.Count)

                End If
            End If
        End If

        StopProcess = False
        ButtonCancel.Text = "Cancel"



    End Sub

    Private Function FileWildcardSearch(
        ByVal FoundFiles As IReadOnlyCollection(Of String),
        ByVal WildcardString As String) As IReadOnlyCollection(Of String)

        Dim LocalFoundFiles As New List(Of String)
        Dim FilePath As String
        Dim Filename As String

        For Each FilePath In FoundFiles
            Filename = System.IO.Path.GetFileName(FilePath)
            If Filename Like WildcardString Then
                LocalFoundFiles.Add(FilePath)
            End If
        Next

        Return LocalFoundFiles
    End Function

    Private Function GetFileNames(ByVal FileWildcard As String) As List(Of String)
        ' Build up list of files to process depending on which option was selected.
        ' Dim FoundFiles As ObjectModel.ReadOnlyCollection(Of String)
        Dim FoundFilesArray As String() = {}
        Dim FoundFilesList As New List(Of String)
        Dim FileExtension As String = FileWildcard.Replace("*", "")
        Dim Filename As String

        TextBoxStatus.Text = "Getting files..."

        If ListBoxFiles.SelectedItems.Count > 0 Then
            For i As Integer = 0 To ListBoxFiles.SelectedItems.Count - 1
                Filename = CType(ListBoxFiles.SelectedItems(i), String)
                If System.IO.Path.GetExtension(Filename) = FileExtension Then
                    Filename = TextBoxInputDirectory.Text + Filename
                    FoundFilesList.Add(Filename)
                End If
            Next
        Else
            For i As Integer = 0 To ListBoxFiles.Items.Count - 1
                Filename = CType(ListBoxFiles.Items(i), String)
                If System.IO.Path.GetExtension(Filename) = FileExtension Then
                    'Filename = TextBoxInputDirectory.Text + "\" + Filename
                    Filename = TextBoxInputDirectory.Text + Filename
                    FoundFilesList.Add(Filename)
                End If
            Next
            'If RadioButtonFilesDirectoriesAndSubdirectories.Checked Then
            '    FoundFiles = FileIO.FileSystem.GetFiles(TextBoxInputDirectory.Text,
            '                        FileIO.SearchOption.SearchAllSubDirectories,
            '                        FileWildcard)
            '    FoundFilesList = FoundFiles.ToList
            'End If

            'If RadioButtonFilesDirectoryOnly.Checked Then
            '    FoundFiles = FileIO.FileSystem.GetFiles(TextBoxInputDirectory.Text,
            '                        FileIO.SearchOption.SearchTopLevelOnly,
            '                        FileWildcard)
            '    FoundFilesList = FoundFiles.ToList
            'End If

        End If

        Return FoundFilesList
    End Function

    Private Function GetTotalFilesToProcess() As Integer
        Dim Count As Integer = 0

        If CheckedListBoxAssembly.CheckedItems.Count > 0 Then
            Count += GetFileNames("*.asm").Count
        End If
        If CheckedListBoxPart.CheckedItems.Count > 0 Then
            Count += GetFileNames("*.par").Count
        End If
        If CheckedListBoxSheetmetal.CheckedItems.Count > 0 Then
            Count += GetFileNames("*.psm").Count
        End If
        If CheckedListBoxDraft.CheckedItems.Count > 0 Then
            Count += GetFileNames("*.dft").Count
        End If

        Return Count
    End Function

    Private Function IsCheckedFilesToProcess() As Boolean
        Dim tf As Boolean

        tf = RadioButtonFilesDirectoriesAndSubdirectories.Checked
        tf = tf Or RadioButtonFilesDirectoryOnly.Checked
        tf = tf Or RadioButtonTopLevelAssembly.Checked
        tf = tf Or RadioButtonTODOList.Checked

        Return tf
    End Function

End Class