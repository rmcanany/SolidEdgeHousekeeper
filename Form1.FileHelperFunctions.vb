Option Strict On

Imports Excel = Microsoft.Office.Interop.Excel

Partial Class Form1

    Private Sub UpdateListViewFiles(Source As ListViewItem)

        Dim FoundFiles As IReadOnlyCollection(Of String) = Nothing
        Dim FoundFile As String
        Dim ActiveFileExtensionsList As New List(Of String)
        Dim msg As String

        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim TODOFile As String = String.Format("{0}\{1}", StartupPath, "todo.txt")

        ListViewFilesOutOfDate = False

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

        If ActiveFileExtensionsList.Count > 0 Then

            Select Case Source.Tag.ToString
                Case = "Folder"
                    If FileIO.FileSystem.DirectoryExists(Source.SubItems.Item(1).Text) Then FoundFiles = FileIO.FileSystem.GetFiles(Source.SubItems.Item(1).Text,
                                    FileIO.SearchOption.SearchTopLevelOnly,
                                    ActiveFileExtensionsList.ToArray)

                Case = "Folders"
                    If FileIO.FileSystem.DirectoryExists(Source.SubItems.Item(1).Text) Then FoundFiles = FileIO.FileSystem.GetFiles(Source.SubItems.Item(1).Text,
                                    FileIO.SearchOption.SearchAllSubDirectories,
                                    ActiveFileExtensionsList.ToArray)

                Case = "csv", "txt"
                    If FileIO.FileSystem.FileExists(Source.SubItems.Item(1).Text) Then FoundFiles = IO.File.ReadAllLines(Source.SubItems.Item(1).Text)

                Case = "excel"
                    If FileIO.FileSystem.FileExists(Source.SubItems.Item(1).Text) Then FoundFiles = CommonTasks.ReadExcel(Source.SubItems.Item(1).Text)

                Case = "asm"
                    If FileIO.FileSystem.FileExists(Source.SubItems.Item(1).Text) Then

                        Dim tmpList As New Collection

                        For Each item As ListViewItem In ListViewFiles.Items
                            If item.Tag.ToString = "Folder" Or item.Tag.ToString = "Folders" Then
                                tmpList.Add(item.SubItems.Item(1).Text)
                            End If
                        Next

                        If tmpList.Count = 0 Then tmpList.Add(IO.Path.GetDirectoryName(Source.SubItems.Item(1).Text))

                        For Each tmpFolder As String In tmpList

                            Dim TLAU As New TopLevelAssemblyUtilities(Me)

                            TextBoxStatus.Text = "Finding all linked files.  This may take some time."

                            If RadioButtonTLABottomUp.Checked Then
                                If Not FileIO.FileSystem.FileExists(TextBoxFastSearchScopeFilename.Text) Then
                                    msg = "Fast search scope file (on Configuration Tab) not found" + Chr(13)
                                    MsgBox(msg, vbOKOnly)
                                    Exit Sub
                                End If

                                FoundFiles = TLAU.GetLinks("BottomUp", tmpFolder,
                                                       Source.SubItems.Item(1).Text,
                                                       ActiveFileExtensionsList)
                            Else
                                FoundFiles = TLAU.GetLinks("TopDown", tmpFolder,
                                                       Source.SubItems.Item(1).Text,
                                                       ActiveFileExtensionsList,
                                                       Report:=CheckBoxTLAReportUnrelatedFiles.Checked)
                            End If

                        Next

                        TextBoxStatus.Text = ""

                    End If

                Case Else
                    FoundFiles = Nothing

            End Select


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
                FoundFiles = FileWildcardSearch(FoundFiles, ComboBoxFileSearch.Text)
            End If

            If Not ListViewFiles.Font.Size = CSng(TextBoxFontSize.Text) Then
                ListViewFiles.Font = New Font("Microsoft Sans Serif", CSng(TextBoxFontSize.Text), FontStyle.Regular)
            End If

            ListViewFiles.BeginUpdate()

            For Each FoundFile In FoundFiles

                If CommonTasks.FilenameIsOK(FoundFile) Then

                    If Not ListViewFiles.Items.ContainsKey(FoundFile) Then

                        Dim tmpLVItem As New ListViewItem
                        tmpLVItem.Text = IO.Path.GetFileName(FoundFile)
                        tmpLVItem.SubItems.Add(IO.Path.GetDirectoryName(FoundFile))
                        tmpLVItem.ImageKey = "Unchecked"
                        tmpLVItem.Tag = FoundFile
                        tmpLVItem.Name = FoundFile
                        tmpLVItem.Group = ListViewFiles.Groups.Item(IO.Path.GetExtension(FoundFile).ToLower)
                        ListViewFiles.Items.Add(tmpLVItem)

                    End If

                End If

            Next

            'ListViewFiles.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent)
            ListViewFiles.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent)

            ListViewFiles.EndUpdate()

            TextBoxStatus.Text = String.Format("{0} files found", FoundFiles.Count)

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

        If ListViewFiles.SelectedItems.Count > 0 Then
            For i As Integer = 0 To ListViewFiles.SelectedItems.Count - 1
                Filename = CType(ListViewFiles.SelectedItems.Item(i).Tag, String)
                If System.IO.Path.GetExtension(Filename) = FileExtension Then
                    FoundFilesList.Add(Filename)
                End If
            Next
        Else
            For i As Integer = 0 To ListViewFiles.Items.Count - 1
                Filename = CType(ListViewFiles.Items(i).Tag, String)
                If System.IO.Path.GetExtension(Filename) = FileExtension Then
                    If ListViewFiles.Items(i).Group.Name <> "Excluded" Then FoundFilesList.Add(Filename)
                End If
            Next

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

End Class