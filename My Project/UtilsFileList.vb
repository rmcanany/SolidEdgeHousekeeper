﻿Option Strict On

Public Class UtilsFileList
    Public Property ListViewFiles As ListView
    Public Property FMain As Form_Main


    Public Sub New(_Form_Main As Form_Main, ListViewFiles As ListView)
        Me.FMain = _Form_Main
        Me.ListViewFiles = ListViewFiles
    End Sub

    Public Sub New_UpdateFileList()

        FMain.Cursor = Cursors.WaitCursor

        Dim GroupTags As New List(Of String)
        Dim BareTopLevelAssembly As Boolean = False
        Dim msg As String

        Dim ElapsedTime As Double
        Dim ElapsedTimeText As String

        Dim StartTime As DateTime = Now


        FMain.TextBoxStatus.Text = "Updating list..."
        FMain.LabelTimeRemaining.Text = ""
        System.Windows.Forms.Application.DoEvents()

        ListViewFiles.BeginUpdate()

        ' Remove everything except the "Sources" group.
        For i = ListViewFiles.Items.Count - 1 To 0 Step -1
            If ListViewFiles.Items.Item(i).Group.Name <> "Sources" Then
                ListViewFiles.Items.Item(i).Remove()
            Else
                GroupTags.Add(CType(ListViewFiles.Items.Item(i).Tag, String))
            End If
        Next

        If (GroupTags.Contains("ASM_Folder")) And Not (GroupTags.Contains("asm")) Then
            msg = "A top level assembly folder was found with no top level assembly.  "
            msg += "Please add an assembly, or delete the folder(s)."
            ListViewFiles.EndUpdate()
            FMain.Cursor = Cursors.Default
            FMain.TextBoxStatus.Text = ""
            MsgBox(msg, vbOKOnly)
            Exit Sub
        End If

        If (FMain.RadioButtonTLABottomUp.Checked) And (Not FileIO.FileSystem.FileExists(FMain.TextBoxFastSearchScopeFilename.Text)) Then
            msg = "Fast search scope file (on Configuration Tab) not found" + Chr(13)
            ListViewFiles.EndUpdate()
            FMain.Cursor = Cursors.Default
            FMain.TextBoxStatus.Text = ""
            MsgBox(msg, vbOKOnly)
            Exit Sub
        End If

        If (GroupTags.Contains("asm")) And Not (GroupTags.Contains("ASM_Folder")) Then

            'If CheckBoxWarnBareTLA.Enabled And CheckBoxWarnBareTLA.Checked Then
            If FMain.CheckBoxWarnBareTLA.Checked Then
                msg = "A top-level assembly with no top-level folder detected.  "
                msg += "No 'Where Used' will be performed." + vbCrLf + vbCrLf
                msg += "Click OK to continue, or Cancel to stop." + vbCrLf
                msg += "Disable this message on the Configuration tab."
                Dim result As MsgBoxResult = MsgBox(msg, vbOKCancel)
                If result = MsgBoxResult.Ok Then
                    BareTopLevelAssembly = True
                Else
                    ListViewFiles.EndUpdate()
                    FMain.Cursor = Cursors.Default
                    FMain.TextBoxStatus.Text = ""
                    Exit Sub
                End If
            Else
                BareTopLevelAssembly = True
            End If
        End If

        'Dim UFL As New UtilsFileList(Me, ListViewFiles)

        ' Only remaining items should be in the "Sources" group.
        For Each item As ListViewItem In ListViewFiles.Items
            UpdateListViewFiles(item, BareTopLevelAssembly)
        Next

        'DragDropCache.Clear()
        'For Each item As ListViewItem In ListViewFiles.Items
        '    DragDropCache.Add(item)
        'Next

        ListViewFiles.EndUpdate()

        FMain.Cursor = Cursors.Default
        'If TextBoxStatus.Text = "Updating list..." Then
        '    TextBoxStatus.Text = "No files found"
        'End If

        ElapsedTime = Now.Subtract(StartTime).TotalMinutes
        If ElapsedTime < 60 Then
            ElapsedTimeText = "in " + ElapsedTime.ToString("0.0") + " min."
        Else
            ElapsedTimeText = "in " + (ElapsedTime / 60).ToString("0.0") + " hr."
        End If


        Dim filecount As Integer = ListViewFiles.Items.Count - ListViewFiles.Groups.Item("Sources").Items.Count
        FMain.TextBoxStatus.Text = String.Format("{0} files found in {1}", filecount, ElapsedTimeText)


    End Sub

    Public Sub UpdateListViewFiles(Source As ListViewItem, BareTopLevelAssembly As Boolean)

        Dim FoundFiles As IReadOnlyCollection(Of String) = Nothing
        Dim FoundFile As String
        Dim ActiveFileExtensionsList As New List(Of String)
        Dim tf As Boolean
        'Dim msg As String

        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim TODOFile As String = String.Format("{0}\{1}", StartupPath, "todo.txt")

        Dim UC As New UtilsCommon

        FMain.ListViewFilesOutOfDate = False
        FMain.BT_Update.BackColor = Color.FromName("Control")

        Form_Main.StopProcess = False
        FMain.ButtonCancel.Text = "Stop"

        If FMain.new_CheckBoxFilterAsm.Checked Then
            ActiveFileExtensionsList.Add("*.asm")
        End If
        If FMain.new_CheckBoxFilterPar.Checked Then
            ActiveFileExtensionsList.Add("*.par")
        End If
        If FMain.new_CheckBoxFilterPsm.Checked Then
            ActiveFileExtensionsList.Add("*.psm")
        End If
        If FMain.new_CheckBoxFilterDft.Checked Then
            ActiveFileExtensionsList.Add("*.dft")
        End If

        If ActiveFileExtensionsList.Count > 0 Then

            Select Case Source.Tag.ToString
                Case = "Folder"
                    If FileIO.FileSystem.DirectoryExists(Source.Name) Then
                        FoundFiles = FileIO.FileSystem.GetFiles(Source.Name,
                                     FileIO.SearchOption.SearchTopLevelOnly,
                                     ActiveFileExtensionsList.ToArray)
                    End If

                Case = "Folders"
                    If FileIO.FileSystem.DirectoryExists(Source.Name) Then
                        FoundFiles = FileIO.FileSystem.GetFiles(Source.Name,
                                    FileIO.SearchOption.SearchAllSubDirectories,
                                    ActiveFileExtensionsList.ToArray)
                    End If

                Case = "csv", "txt"
                    If FileIO.FileSystem.FileExists(Source.Name) Then
                        FoundFiles = IO.File.ReadAllLines(Source.Name)
                    End If

                Case = "excel"
                    If FileIO.FileSystem.FileExists(Source.Name) Then
                        FoundFiles = UC.ReadExcel(Source.Name)
                    End If

                Case = "DragDrop"
                    Dim tmpFoundFiles As New List(Of String)

                    For Each tmpItem As ListViewItem In FMain.DragDropCache
                        tmpFoundFiles.Add(tmpItem.Name)
                    Next
                    FoundFiles = tmpFoundFiles

                    'MsgBox("Here")

                Case = "ASM_Folder"
                    ' Nothing to do here.  Dealt with in 'Case = "asm"'

                Case = "asm"

                    If (Not BareTopLevelAssembly) And (FileIO.FileSystem.FileExists(Source.Name)) Then
                        Dim tmpFolders As New List(Of String)

                        For Each item As ListViewItem In ListViewFiles.Items
                            If item.Tag.ToString = "ASM_Folder" Then
                                If Not tmpFolders.Contains(item.Name) Then tmpFolders.Add(item.Name)
                            End If
                        Next

                        Dim tmpFoundFiles As New List(Of String)

                        If FMain.RadioButtonTLABottomUp.Checked Then
                            Dim UTLA As New UtilsTopLevelAssembly(FMain)

                            FMain.TextBoxStatus.Text = "Finding all linked files.  This may take some time."

                            tmpFoundFiles.AddRange(UTLA.GetLinksBottomUp(tmpFolders,
                                                           Source.SubItems.Item(1).Text,
                                                           ActiveFileExtensionsList,
                                                           FMain.CheckBoxDraftAndModelSameName.Checked,
                                                           FMain.CheckBoxTLAReportUnrelatedFiles.Checked))

                        Else
                            Dim UTLA As New UtilsTopLevelAssembly(FMain)
                            tmpFoundFiles.AddRange(UTLA.GetLinksTopDown(tmpFolders,
                                                       Source.SubItems.Item(1).Text,
                                                       ActiveFileExtensionsList,
                                                       Report:=FMain.CheckBoxTLAReportUnrelatedFiles.Checked))
                        End If

                        FoundFiles = CType(tmpFoundFiles, IReadOnlyCollection(Of String))

                        FMain.TextBoxStatus.Text = ""

                    ElseIf (BareTopLevelAssembly) And (FileIO.FileSystem.FileExists(Source.Name)) Then

                        Dim UTLA As New UtilsTopLevelAssembly(FMain)

                        FMain.TextBoxStatus.Text = "Finding all linked files.  This may take some time."

                        Dim tmpFoundFiles As New List(Of String)
                        Dim tmpFolders As New List(Of String)

                        ' Empty tmpFolders is flag for BareTopLevelAssembly
                        ' No 'where used' is performed.
                        ' Bare top level assemblies are always processed bottom up.

                        tmpFoundFiles.AddRange(UTLA.GetLinksBottomUp(tmpFolders,
                                                           Source.SubItems.Item(1).Text,
                                                           ActiveFileExtensionsList,
                                                           FMain.CheckBoxDraftAndModelSameName.Checked,
                                                           FMain.CheckBoxTLAReportUnrelatedFiles.Checked))

                        FoundFiles = CType(tmpFoundFiles, IReadOnlyCollection(Of String))

                        FMain.TextBoxStatus.Text = ""

                    End If

                Case Else
                    FoundFiles = Nothing
                    FMain.TextBoxStatus.Text = "No files found"
            End Select


        End If

        ' Remove problem files
        If Not FoundFiles Is Nothing Then
            Dim tmpFoundFiles As New List(Of String)
            For Each item In FoundFiles
                tf = UC.FilenameIsOK(item)
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


        ' Dependency sort
        If Not FoundFiles Is Nothing Then
            If FMain.RadioButtonSortDependency.Checked Then
                FoundFiles = GetDependencySortedFiles(FoundFiles)
            End If
        End If


        ' Run filters
        If Not FoundFiles Is Nothing Then

            ' Filter by file wildcard search
            If FMain.CheckBoxEnableFileWildcard.Checked Then
                FoundFiles = FileWildcardSearch(FoundFiles, FMain.ComboBoxFileWildcard.Text)
            End If

            ' Filter by properties
            If FMain.CheckBoxEnablePropertyFilter.Checked Then
                System.Threading.Thread.Sleep(1000)
                Dim UPF As New UtilsPropertyFilters(Me.FMain)
                FoundFiles = UPF.PropertyFilter(FoundFiles, FMain.PropertyFilterDict)
            End If

            If FMain.RadioButtonSortAlphabetical.Checked Then
                FoundFiles = SortAlphabetical(FoundFiles)
            End If

            If FMain.RadioButtonSortRandomSample.Checked Then
                Dim Fraction As Double = 0.1
                Try
                    Fraction = CDbl(FMain.TextBoxSortRandomSampleFraction.Text)
                Catch ex As Exception
                    MsgBox(String.Format("Cannot convert Sample fraction, '{0}', to a number", FMain.TextBoxSortRandomSampleFraction.Text))
                End Try
                FoundFiles = SortRandomSample(FoundFiles, Fraction)
            End If

            ListViewFiles.BeginUpdate()

            For Each FoundFile In FoundFiles

                FMain.TextBoxStatus.Text = String.Format("Updating List {0}", System.IO.Path.GetFileName(FoundFile))
                System.Windows.Forms.Application.DoEvents()

                If UC.FilenameIsOK(FoundFile) Then

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

            ListViewFiles.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent)

            ListViewFiles.EndUpdate()

            FMain.TextBoxStatus.Text = ""

        Else
            'TextBoxStatus.Text = "No files found"
        End If


        Form_Main.StopProcess = False
        FMain.ButtonCancel.Text = "Cancel"

    End Sub

    Public Function FileWildcardSearch(
        ByVal FoundFiles As IReadOnlyCollection(Of String),
        ByVal WildcardString As String) As IReadOnlyCollection(Of String)

        Dim tmpFoundFiles As New List(Of String)
        Dim FilePath As String
        Dim Filename As String

        For Each FilePath In FoundFiles
            Filename = System.IO.Path.GetFileName(FilePath)
            FMain.TextBoxStatus.Text = String.Format("Wildcard Search {0}", Filename)
            If Filename Like WildcardString Then
                tmpFoundFiles.Add(FilePath)
            End If
        Next

        Return tmpFoundFiles
    End Function


    Public Function GetFileNames(ByVal FileWildcard As String) As List(Of String)
        ' Build up list of files to process depending on which option was selected.
        ' Dim FoundFiles As ObjectModel.ReadOnlyCollection(Of String)
        Dim FoundFilesArray As String() = {}
        Dim FoundFilesList As New List(Of String)
        Dim FileExtension As String = FileWildcard.Replace("*", "")
        Dim Filename As String

        FMain.TextBoxStatus.Text = "Getting files..."

        If ListViewFiles.SelectedItems.Count > 0 Then
            For i As Integer = 0 To ListViewFiles.SelectedItems.Count - 1
                Filename = ListViewFiles.SelectedItems.Item(i).Name
                If System.IO.Path.GetExtension(Filename) = FileExtension Then
                    FoundFilesList.Add(Filename)
                End If
            Next
        Else
            Dim ProcessLast As New List(Of String)  ' Trying to process top level assy last

            For i As Integer = 0 To ListViewFiles.Items.Count - 1
                Filename = ListViewFiles.Items(i).Name
                If System.IO.Path.GetExtension(Filename) = FileExtension Then
                    If ListViewFiles.Items(i).Group.Name <> "Excluded" Then
                        If ListViewFiles.Items(i).Group.Name = "Sources" Then
                            ProcessLast.Add(Filename)
                        Else
                            FoundFilesList.Add(Filename)
                        End If
                    End If
                End If
            Next

            For Each Filename In ProcessLast
                FoundFilesList.Add(Filename)
            Next

        End If

        Return FoundFilesList
    End Function


    Public Function GetTotalFilesToProcess() As Integer
        Dim Count As Integer = 0

        Dim PartCount As Integer = 0
        Dim SheetmetalCount As Integer = 0
        Dim AssemblyCount As Integer = 0
        Dim DraftCount As Integer = 0

        For Each Task As Task In FMain.TaskList
            If Task.IsSelectedPart Then PartCount += 1
            If Task.IsSelectedSheetmetal Then SheetmetalCount += 1
            If Task.IsSelectedAssembly Then AssemblyCount += 1
            If Task.IsSelectedDraft Then DraftCount += 1
        Next

        If PartCount > 0 Then Count += GetFileNames("*.par").Count
        If SheetmetalCount > 0 Then Count += GetFileNames("*.psm").Count
        If AssemblyCount > 0 Then Count += GetFileNames("*.asm").Count
        If DraftCount > 0 Then Count += GetFileNames("*.dft").Count

        Return Count
    End Function

    Private Function GetDependencySortedFiles(Foundfiles As IReadOnlyCollection(Of String)) As IReadOnlyCollection(Of String)
        Dim OutList As New List(Of String)
        Dim MissingFilesList As New List(Of String)
        'Dim tmpMissingFilesList As New List(Of String)
        Dim DependencyDict As New Dictionary(Of String, List(Of String))
        Dim Filename As String
        Dim DMDoc As DesignManager.Document

        Dim DMApp As New DesignManager.Application
        DMApp.Visible = 1

        FMain.Activate()


        For Each Filename In Foundfiles

            FMain.TextBoxStatus.Text = String.Format("Dependency Sort (this can take some time) {0}", IO.Path.GetFileName(Filename))

            DMDoc = CType(DMApp.Open(Filename), DesignManager.Document)

            Dim tmp_DependencyDict As New Dictionary(Of String, List(Of String))

            tmp_DependencyDict = GetLinks(DMDoc, tmp_DependencyDict, MissingFilesList)

            For Each s As String In tmp_DependencyDict.Keys
                FMain.TextBoxStatus.Text = s
                If Not DependencyDict.Keys.Contains(s) Then
                    DependencyDict.Add(s, tmp_DependencyDict(s))

                End If
            Next

        Next

        OutList = SortByDependency(DependencyDict)

        DMApp.Quit()

        If MissingFilesList.Count > 0 Then
            Dim Timestamp As String = System.DateTime.Now.ToString("yyyyMMdd_HHmmss")
            Dim MissingFilesFileName As String
            'Dim msg As String
            MissingFilesFileName = IO.Path.GetTempPath + "\Housekeeper_" + Timestamp + "_Missing_Files.log"

            Try
                Using writer As New IO.StreamWriter(MissingFilesFileName, True)
                    writer.WriteLine("FILES NOT FOUND")
                    For Each Filename In MissingFilesList
                        ' Filename = Filename.Replace(TopLevelFolder, "")
                        writer.WriteLine(String.Format(Filename))
                    Next
                End Using

                Process.Start("Notepad.exe", MissingFilesFileName)

            Catch ex As Exception
            End Try

        End If

        Return OutList

    End Function

    Private Function GetLinks(
        DMDoc As DesignManager.Document,
        LinkDict As Dictionary(Of String, List(Of String)),
        MissingFilesList As List(Of String)
        ) As Dictionary(Of String, List(Of String))

        'Dim LinkDict As New Dictionary(Of String, List(Of String))
        Dim LinkDoc As DesignManager.Document
        Dim LinkDocs As DesignManager.LinkedDocuments
        Dim DMDocName As String
        Dim LinkDocName As String
        'Dim Filename As String
        Dim ValidExtensions As New List(Of String)({".par", ".psm", ".asm", ".dft"})
        Dim tf As Boolean

        'For Each Filename In OriginalLinkDict.Keys
        '    LinkDict.Add(Filename, New List(Of String))
        'Next

        Dim UC As New UtilsCommon

        DMDocName = UC.SplitFOAName(DMDoc.FullName)("Filename")
        'If DMDocName.Contains("!") Then
        '    DMDocName = DMDocName.Split("!"c)(0)
        'End If

        If Not LinkDict.Keys.Contains(DMDocName) Then

            LinkDict.Add(DMDocName, New List(Of String))

            ' Master FOP reports Child documents as LinkedDocuments.
            ' Not correct in this dependency context.
            'FOPStatus
            '1 Not FOP 
            '2 FOP Master
            '4 FOP not Master
            Dim FOPStatus As Integer
            DMDoc.IsDocumentFOP(FOPStatus)

            'tf = CommonTasks.GetDocTypeByExtension(DMDocName) = ".dft"
            'tf = tf Or (FOPStatus = 2)
            tf = FOPStatus = 2

            If Not tf Then
                LinkDocs = CType(DMDoc.LinkedDocuments, DesignManager.LinkedDocuments)
                If Not LinkDocs Is Nothing Then
                    If LinkDocs.Count > 0 Then
                        For Each LinkDoc In LinkDocs

                            LinkDocName = UC.SplitFOAName(LinkDoc.FullName)("Filename")
                            'If LinkDocName.Contains("!") Then
                            '    LinkDocName = LinkDocName.Split("!"c)(0)
                            'End If

                            If ValidExtensions.Contains(IO.Path.GetExtension(LinkDocName)) Then
                                If IO.File.Exists(LinkDocName) Then
                                    LinkDict(DMDocName).Add(LinkDocName)
                                    LinkDict = GetLinks(LinkDoc, LinkDict, MissingFilesList)
                                Else
                                    If Not MissingFilesList.Contains(LinkDocName) Then
                                        MissingFilesList.Add(LinkDocName)
                                    End If
                                End If
                            End If
                        Next
                    End If

                End If
            End If
        End If

        Return LinkDict
    End Function

    Private Function SortByDependency(
        OriginalLinkDict As Dictionary(Of String, List(Of String))
        ) As List(Of String)

        Dim LinkDict As New Dictionary(Of String, List(Of String))
        Dim Outlist As New List(Of String)
        Dim HasUnmetDependencies As Boolean
        Dim NoRemainingLinks As Boolean
        Dim NoDependencies As New List(Of String)
        Dim Filename As String

        Dim Count As Integer = 0
        Dim OldCount As Integer = 100

        ' Copy the original dictionary
        For Each Filename In OriginalLinkDict.Keys
            LinkDict.Add(Filename, OriginalLinkDict(Filename))
        Next

        ' Find files with no dependencies.
        For Each Filename In LinkDict.Keys
            If LinkDict(Filename).Count = 0 Then
                Outlist.Add(Filename)
                NoDependencies.Add(Filename)
            End If
        Next

        ' Remove files with no dependencies from the copied dictionary
        For Each Filename In Outlist
            If LinkDict.Keys.Contains(Filename) Then
                LinkDict.Remove(Filename)
            End If
        Next

        Dim NumPasses As Integer = 0

        While OldCount > Count
            For Each Filename In LinkDict.Keys
                FMain.TextBoxStatus.Text = IO.Path.GetFileName(Filename)
                If LinkDict(Filename).Count = 0 Then
                    Outlist.Add(Filename)
                    Continue For
                Else
                    HasUnmetDependencies = False
                    For Each LinkFilename As String In LinkDict(Filename)
                        If Not Outlist.Contains(LinkFilename) Then
                            HasUnmetDependencies = True
                        End If
                    Next
                    If Not HasUnmetDependencies Then
                        Outlist.Add(Filename)
                    End If
                End If
            Next

            OldCount = LinkDict.Count

            For Each Filename In Outlist
                If LinkDict.Keys.Contains(Filename) Then
                    LinkDict.Remove(Filename)
                End If
            Next

            Count = LinkDict.Count

            ' Try to deal with mutual dependencies, like interpart copies
            ' Remove *.par first, then *.psm, *.asm, *.dft in order
            If OldCount = Count Then
                Dim ExtensionList As New List(Of String)
                Dim Extension As String
                Dim RemainingFileList As New List(Of String)
                Dim ValidExtensions As New List(Of String)({".par", ".psm", ".asm", ".dft"})
                Dim ValidExtension As String

                NumPasses += 1

                ' See what type of files remain
                For Each Filename In LinkDict.Keys
                    RemainingFileList.Add(Filename)
                    Extension = IO.Path.GetExtension(Filename)
                    If Not ExtensionList.Contains(Extension) Then
                        ExtensionList.Add(Extension)
                    End If
                Next

                ' Remove one type of file starting with .par, then .psm, .asm, .dft in that order
                For Each ValidExtension In ValidExtensions
                    If ExtensionList.Contains(ValidExtension) Then
                        For Each Filename In RemainingFileList
                            If IO.Path.GetExtension(Filename) = ValidExtension Then
                                Outlist.Add(Filename)
                                LinkDict.Remove(Filename)
                            End If
                        Next
                        Exit For
                    End If
                Next
            End If

            Count = LinkDict.Count

        End While

        If Not FMain.CheckBoxSortIncludeNoDependencies.Checked Then
            For Each Filename In NoDependencies
                If Outlist.Contains(Filename) Then
                    Outlist.Remove(Filename)
                End If
            Next

            Count = 0
            OldCount = 100
            While OldCount > Count
                OldCount = Outlist.Count
                For Each Filename In OriginalLinkDict.Keys
                    If IO.Path.GetExtension(Filename) = ".asm" Then
                        NoRemainingLinks = True
                        For Each LinkFilename As String In OriginalLinkDict(Filename)
                            If Outlist.Contains(LinkFilename) Then
                                NoRemainingLinks = False
                            End If
                        Next
                        If NoRemainingLinks Then
                            Outlist.Remove(Filename)
                        End If
                    End If
                Next
                Count = Outlist.Count
            End While

            For Each Filename In OriginalLinkDict.Keys
                If IO.Path.GetExtension(Filename) = ".dft" Then
                    NoRemainingLinks = True
                    For Each LinkFilename As String In OriginalLinkDict(Filename)
                        If Outlist.Contains(LinkFilename) Then
                            NoRemainingLinks = False
                        End If
                    Next
                    If NoRemainingLinks Then
                        Outlist.Remove(Filename)
                    End If
                End If
            Next

        End If

        If Count > 0 Then
            For Each Filename In LinkDict.Keys
                Outlist.Add(Filename)
            Next
        End If

        'MsgBox(NumPasses.ToString)

        Return Outlist

    End Function

    Private Function SortRandomSample(FoundFiles As IReadOnlyCollection(Of String), Fraction As Double) As IReadOnlyCollection(Of String)
        ' https://stackoverflow.com/questions/29358857/shuffling-an-array-of-strings-in-vb-net

        Dim ShuffleList As New List(Of String)
        Dim OutList As New List(Of String)
        Dim Foundfile As String
        Dim temp As String
        Dim rnd As New Random()
        Dim j As Int32
        Dim NumberToReturn As Integer

        For Each Foundfile In FoundFiles
            ShuffleList.Add(Foundfile)
        Next

        ' Shuffle all
        For n As Int32 = ShuffleList.Count - 1 To 0 Step -1
            j = rnd.Next(0, n + 1)
            ' Swap them.
            temp = ShuffleList(n)
            ShuffleList(n) = ShuffleList(j)
            ShuffleList(j) = temp
        Next n

        ' Extract fraction
        NumberToReturn = CInt(Fraction * ShuffleList.Count)

        For i As Integer = 0 To NumberToReturn - 1
            OutList.Add(ShuffleList(i))
        Next

        Return OutList
    End Function

    Private Function SortAlphabetical(FoundFiles As IReadOnlyCollection(Of String)) As IReadOnlyCollection(Of String)
        Dim Outlist As New List(Of String)
        Dim SortedOutlist As New List(Of String)
        Dim FoundFile As String
        Dim Filename As String
        Dim s As String

        For Each FoundFile In FoundFiles
            Filename = IO.Path.GetFileName(FoundFile)
            s = String.Format("{0}*{1}", Filename, FoundFile)
            Outlist.Add(s)
        Next

        Outlist.Sort()

        For Each s In Outlist
            Filename = s.Split(CType("*", Char()))(1)
            SortedOutlist.Add(Filename)
        Next

        Return SortedOutlist

    End Function



    Public Function GetSourceDirectories() As List(Of String)
        Dim SourceDirectories As New List(Of String)
        Dim TagName As String
        Dim s As String

        For Each Item As ListViewItem In ListViewFiles.Items 'L-istBoxFiles.Items

            'ListViewFiles.BeginUpdate()

            If Item.Group.Name = "Sources" Then

                TagName = CType(Item.Tag, String)

                If TagName.ToLower.Contains("folder") Then
                    s = Item.Name
                    If Not SourceDirectories.Contains(s) Then SourceDirectories.Add(s)
                ElseIf TagName.ToLower = "asm" Then
                    s = System.IO.Path.GetDirectoryName(Item.Name)
                    If Not SourceDirectories.Contains(s) Then SourceDirectories.Add(s)
                End If

            End If

            'ListViewFiles.EndUpdate()

        Next

        Return SourceDirectories
    End Function

End Class
