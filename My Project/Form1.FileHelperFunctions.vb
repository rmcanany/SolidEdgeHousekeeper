Option Strict On

Partial Class Form1

    Private Sub UpdateListViewFiles(Source As ListViewItem, BareTopLevelAssembly As Boolean)

        Dim FoundFiles As IReadOnlyCollection(Of String) = Nothing
        Dim FoundFile As String
        Dim ActiveFileExtensionsList As New List(Of String)
        Dim tf As Boolean
        'Dim msg As String

        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim TODOFile As String = String.Format("{0}\{1}", StartupPath, "todo.txt")

        Dim ElapsedTime As Double
        Dim ElapsedTimeText As String

        StartTime = Now

        ListViewFilesOutOfDate = False
        BT_Update.BackColor = Color.FromName("Control")

        StopProcess = False
        ButtonCancel.Text = "Stop"

        If new_CheckBoxFilterAsm.Checked Then
            ActiveFileExtensionsList.Add("*.asm")
        End If
        If new_CheckBoxFilterPar.Checked Then
            ActiveFileExtensionsList.Add("*.par")
        End If
        If new_CheckBoxFilterPsm.Checked Then
            ActiveFileExtensionsList.Add("*.psm")
        End If
        If new_CheckBoxFilterDft.Checked Then
            ActiveFileExtensionsList.Add("*.dft")
        End If

        If ActiveFileExtensionsList.Count > 0 Then

            Select Case Source.Tag.ToString
                Case = "Folder"
                    If FileIO.FileSystem.DirectoryExists(Source.Name) Then FoundFiles = FileIO.FileSystem.GetFiles(Source.Name,
                                    FileIO.SearchOption.SearchTopLevelOnly,
                                    ActiveFileExtensionsList.ToArray)

                Case = "Folders"
                    If FileIO.FileSystem.DirectoryExists(Source.Name) Then FoundFiles = FileIO.FileSystem.GetFiles(Source.Name,
                                    FileIO.SearchOption.SearchAllSubDirectories,
                                    ActiveFileExtensionsList.ToArray)

                Case = "csv", "txt"
                    If FileIO.FileSystem.FileExists(Source.Name) Then FoundFiles = IO.File.ReadAllLines(Source.Name)

                Case = "excel"
                    If FileIO.FileSystem.FileExists(Source.Name) Then FoundFiles = CommonTasks.ReadExcel(Source.Name)

                Case = "ASM_Folder"
                    ' Nothing to do here.  Dealt with in 'Case = "asm"'

                Case = "asm"

                    If (Not BareTopLevelAssembly) And (FileIO.FileSystem.FileExists(Source.Name)) Then
                        Dim tmpFolders As New List(Of String)
                        'tmpList.Add(IO.Path.GetDirectoryName(Source.Name), IO.Path.GetDirectoryName(Source.Name))

                        For Each item As ListViewItem In ListViewFiles.Items
                            If item.Tag.ToString = "ASM_Folder" Then
                                If Not tmpFolders.Contains(item.Name) Then tmpFolders.Add(item.Name)
                            End If
                        Next

                        Dim tmpFoundFiles As New List(Of String)

                        If RadioButtonTLABottomUp.Checked Then
                            For Each tmpFolder As String In tmpFolders

                                Dim TLAU As New TopLevelAssemblyUtilities(Me)

                                TextBoxStatus.Text = "Finding all linked files.  This may take some time."

                                tmpFoundFiles.AddRange(TLAU.GetLinksBottomUp(tmpFolder,
                                                           Source.SubItems.Item(1).Text,
                                                           ActiveFileExtensionsList,
                                                           CheckBoxDraftAndModelSameName.Checked))
                                'If RadioButtonTLABottomUp.Checked Then
                                '    'tmpFoundFiles.AddRange(TLAU.GetLinks("BottomUp", tmpFolder,
                                '    '                   Source.SubItems.Item(1).Text,
                                '    '                   ActiveFileExtensionsList))
                                '    tmpFoundFiles.AddRange(TLAU.GetLinksBottomUp(tmpFolder,
                                '                           Source.SubItems.Item(1).Text,
                                '                           ActiveFileExtensionsList,
                                '                           CheckBoxDraftAndModelSameName.Checked))
                                'Else
                                '    'tmpFoundFiles.AddRange(TLAU.GetLinks("TopDown", tmpFolder,
                                '    '                   Source.SubItems.Item(1).Text,
                                '    '                   ActiveFileExtensionsList,
                                '    '                   Report:=CheckBoxTLAReportUnrelatedFiles.Checked))
                                '    tmpFoundFiles.AddRange(TLAU.GetLinksTopDown(tmpFolders,
                                '                       Source.SubItems.Item(1).Text,
                                '                       ActiveFileExtensionsList,
                                '                       Report:=CheckBoxTLAReportUnrelatedFiles.Checked))
                                'End If

                            Next
                        Else
                            Dim TLAU As New TopLevelAssemblyUtilities(Me)
                            tmpFoundFiles.AddRange(TLAU.GetLinksTopDown(tmpFolders,
                                                       Source.SubItems.Item(1).Text,
                                                       ActiveFileExtensionsList,
                                                       Report:=CheckBoxTLAReportUnrelatedFiles.Checked))



                        End If

                        'For Each tmpFolder As String In tmpFolders

                        '    Dim TLAU As New TopLevelAssemblyUtilities(Me)

                        '    TextBoxStatus.Text = "Finding all linked files.  This may take some time."

                        '    If RadioButtonTLABottomUp.Checked Then
                        '        tmpFoundFiles.AddRange(TLAU.GetLinks("BottomUp", tmpFolder,
                        '                               Source.SubItems.Item(1).Text,
                        '                               ActiveFileExtensionsList))
                        '    Else
                        '        tmpFoundFiles.AddRange(TLAU.GetLinks("TopDown", tmpFolder,
                        '                               Source.SubItems.Item(1).Text,
                        '                               ActiveFileExtensionsList,
                        '                               Report:=CheckBoxTLAReportUnrelatedFiles.Checked))
                        '    End If

                        'Next

                        FoundFiles = CType(tmpFoundFiles, IReadOnlyCollection(Of String))

                        TextBoxStatus.Text = ""

                    ElseIf (BareTopLevelAssembly) And (FileIO.FileSystem.FileExists(Source.Name)) Then

                        Dim TLAU As New TopLevelAssemblyUtilities(Me)

                        TextBoxStatus.Text = "Finding all linked files.  This may take some time."

                        Dim tmpFoundFiles As New List(Of String)

                        ' Set TopLevelFolder to the empty string (""), which means this is a
                        ' bare top level assembly.  No 'where used' is performed.
                        ' Bare top level assemblies are always processed bottom up.
                        tmpFoundFiles.AddRange(TLAU.GetLinksBottomUp("",
                                                           Source.SubItems.Item(1).Text,
                                                           ActiveFileExtensionsList,
                                                           CheckBoxDraftAndModelSameName.Checked))

                        'tmpFoundFiles.AddRange(TLAU.GetLinks("BottomUp", "",
                        '                               Source.SubItems.Item(1).Text,
                        '                               ActiveFileExtensionsList))

                        FoundFiles = CType(tmpFoundFiles, IReadOnlyCollection(Of String))

                        TextBoxStatus.Text = ""

                    End If

                Case Else
                    FoundFiles = Nothing

            End Select


        End If

        If Not FoundFiles Is Nothing Then
            Dim tmpFoundFiles As New List(Of String)
            For Each item In FoundFiles
                tf = CommonTasks.FilenameIsOK(item)
                tf = tf And IO.File.Exists(item)
                tf = tf And Not tmpFoundFiles.Contains(item)
                ' Exporting from LibreOffice Calc to Excel, the first item can sometimes be Nothing
                ' Causes a problem comparing extensions
                Try
                    tf = tf And ActiveFileExtensionsList.Contains(IO.Path.GetExtension(item).Replace(".", "*."))
                Catch ex As Exception
                    ' MsgBox("Catch")
                End Try
                If tf Then
                    tmpFoundFiles.Add(item)
                End If
            Next
            FoundFiles = CType(tmpFoundFiles, IReadOnlyCollection(Of String))
        End If



        If Not FoundFiles Is Nothing Then

            ' Filter by file wildcard search
            If new_CheckBoxFileSearch.Checked Then
                FoundFiles = FileWildcardSearch(FoundFiles, new_ComboBoxFileSearch.Text)
            End If

            ' Filter by properties
            If new_CheckBoxEnablePropertyFilter.Checked Then
                System.Threading.Thread.Sleep(1000)
                Dim PropertyFilter As New PropertyFilter(Me)
                FoundFiles = PropertyFilter.PropertyFilter(FoundFiles, PropertyFilterDict, PropertyFilterFormula)
            End If


            ListViewFiles.BeginUpdate()

            For Each FoundFile In FoundFiles

                TextBoxStatus.Text = String.Format("Updating List {0}", System.IO.Path.GetFileName(FoundFile))
                System.Windows.Forms.Application.DoEvents()

                If CommonTasks.FilenameIsOK(FoundFile) Then

                    If IO.File.Exists(FoundFile) Then

                        If Not ListViewFiles.Items.ContainsKey(FoundFile) Then

                            Dim tmpLVItem As New ListViewItem
                            tmpLVItem.Text = IO.Path.GetFileName(FoundFile)
                            tmpLVItem.SubItems.Add(IO.Path.GetDirectoryName(FoundFile))
                            tmpLVItem.ImageKey = "Unchecked"
                            tmpLVItem.Tag = IO.Path.GetExtension(FoundFile).ToLower 'Backup gruppo
                            tmpLVItem.Name = FoundFile
                            tmpLVItem.Group = ListViewFiles.Groups.Item(IO.Path.GetExtension(FoundFile).ToLower)
                            ListViewFiles.Items.Add(tmpLVItem)

                        End If

                    End If

                End If

            Next

            'ListViewFiles.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent)
            ListViewFiles.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent)

            ListViewFiles.EndUpdate()

            ElapsedTime = Now.Subtract(StartTime).TotalMinutes
            If ElapsedTime < 60 Then
                ElapsedTimeText = "in " + ElapsedTime.ToString("0.0") + " min."
            Else
                ElapsedTimeText = "in " + (ElapsedTime / 60).ToString("0.0") + " hr."
            End If


            'TextBoxStatus.Text = String.Format("{0} files found", FoundFiles.Count)
            TextBoxStatus.Text = String.Format("{0} files found in {1}", ListViewFiles.Items.Count, ElapsedTimeText)

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
            TextBoxStatus.Text = String.Format("Wildcard Search {0}", Filename)
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
                Filename = ListViewFiles.SelectedItems.Item(i).Name
                If System.IO.Path.GetExtension(Filename) = FileExtension Then
                    FoundFilesList.Add(Filename)
                End If
            Next
        Else
            For i As Integer = 0 To ListViewFiles.Items.Count - 1
                Filename = ListViewFiles.Items(i).Name
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