Option Strict On

Imports System.Reflection
Imports ListViewExtended

Public Class UtilsFileList
    Public Property ListViewFiles As ListView
    Public Property ListViewSources As ListView
    Public Property FMain As Form_Main


    Public Sub New(_Form_Main As Form_Main, ListViewFiles As ListViewCollapsible, ListViewSources As ListView)
        Me.FMain = _Form_Main
        Me.ListViewFiles = ListViewFiles
        Me.ListViewSources = ListViewSources
    End Sub


    Private Function CheckStartConditions(ByRef BareTopLevelAssembly As Boolean) As Boolean
        Dim Proceed As Boolean = True

        Dim ErrorList As New List(Of String)
        Dim GroupTags As New List(Of String)
        Dim msg As String

        For i = 0 To ListViewSources.Items.Count - 1

            If ListViewSources.Items(i).Group.Name = "Sources" Then
                GroupTags.Add(CType(ListViewSources.Items(i).Tag, String))
            End If
        Next

        If Not GroupTags.Contains("asm") Then

            If GroupTags.Contains("ASM_Folder") Then
                msg = "A top level assembly folder was found with no top level assembly specified.  "
                msg += "Please add an assembly, or delete the folder(s)."
                ErrorList.Add(msg)
            End If
        End If

        If GroupTags.Contains("asm") Then

            If (FMain.TLABottomUp) And (Not FileIO.FileSystem.FileExists(FMain.FastSearchScopeFilename)) Then
                msg = "Fast search scope file not found.  Set it on the Configuration Tab -- Top Level Assembly Page."
                ErrorList.Add(msg)
            End If

            If (FMain.TLATopDown) And (Not FileIO.FileSystem.FileExists(FMain.LinkManagementFilename)) Then
                msg = "LinkMgmt.txt file not found.  Set it on the Configuration Tab -- Top Level Assembly Page."
                ErrorList.Add(msg)
            End If

            If Not (GroupTags.Contains("ASM_Folder")) Then

                If FMain.WarnBareTLA Then
                    msg = "A top-level assembly with no top-level folder detected.  "
                    msg += "No 'Where Used' will be performed."
                    ErrorList.Add(msg)
                Else
                    BareTopLevelAssembly = True
                End If
            End If

        End If

        If ErrorList.Count > 0 Then

            Dim WarningDetected As Boolean = False

            Dim ErrorMessage As String = ""
            For Each s As String In ErrorList
                If s.Contains("no top-level folder") Then WarningDetected = True
                ErrorMessage = String.Format("{0}{1}{2}", ErrorMessage, s, vbCrLf)
            Next

            If (WarningDetected) And (ErrorList.Count = 1) Then

                ErrorMessage = String.Format("{0}{1}Click OK to continue, or Cancel to stop.", ErrorMessage, vbCrLf)
                ErrorMessage = String.Format("{0}{1}Disable this message on the Configuration Tab -- Top Level Assembly Page.", ErrorMessage, vbCrLf)

                Dim Result As MsgBoxResult = MsgBox(ErrorMessage, vbOKCancel)
                If Result = MsgBoxResult.Ok Then
                    BareTopLevelAssembly = True
                    Proceed = True
                Else
                    Proceed = False
                End If
            Else
                MsgBox(ErrorMessage, vbOKOnly)
                Proceed = False
            End If

        End If

        Return Proceed
    End Function

    Public Sub New_UpdateFileList()

        FMain.Cursor = Cursors.WaitCursor

        Dim GroupTags As New List(Of String)
        Dim BareTopLevelAssembly As Boolean = False

        Dim ElapsedTime As Double
        Dim ElapsedTimeText As String

        Dim StartTime As DateTime = Now

        Dim ActiveFileExtensionsList As New List(Of String)

        FMain.TextBoxStatus.Text = "Updating list..."
        FMain.LabelTimeRemaining.Text = ""
        System.Windows.Forms.Application.DoEvents()

        If FMain.FilterAsm Then ActiveFileExtensionsList.Add("*.asm")
        If FMain.FilterPar Then ActiveFileExtensionsList.Add("*.par")
        If FMain.FilterPsm Then ActiveFileExtensionsList.Add("*.psm")
        If FMain.FilterDft Then ActiveFileExtensionsList.Add("*.dft")

        ListViewFiles.BeginUpdate()

        ListViewFiles.Items.Clear()


        If Not CheckStartConditions(BareTopLevelAssembly) Then
            ListViewFiles.EndUpdate()
            FMain.Cursor = Cursors.Default
            FMain.TextBoxStatus.Text = ""

            Exit Sub
        End If

        Dim tmpFoundFiles As New List(Of String)

        For Each item As ListViewItem In ListViewSources.Items
            Dim tmptmpFoundFiles = FindFiles(item, BareTopLevelAssembly)
            If tmptmpFoundFiles IsNot Nothing Then
                tmpFoundFiles.AddRange(tmptmpFoundFiles)
            End If
        Next

        If Not FMain.SortNone Then
            tmpFoundFiles = tmpFoundFiles.Distinct.ToList
        Else
            If Not FMain.KeepUnsortedDuplicates Then
                tmpFoundFiles = tmpFoundFiles.Distinct.ToList
            End If
        End If

        Dim FoundFiles As IReadOnlyCollection(Of String)
        FoundFiles = tmpFoundFiles

        ' Remove problem files
        If Not FoundFiles Is Nothing Then
            FoundFiles = RemoveProblemFiles(FoundFiles, ActiveFileExtensionsList)
        End If


        ' Dependency sort
        If Not FoundFiles Is Nothing Then
            If FMain.SortDependency Then
                FoundFiles = GetDependencySortedFiles(FoundFiles)
            End If
        End If


        ' Run filters
        If Not FoundFiles Is Nothing Then

            ' Filter by file wildcard search
            If FMain.EnableFileWildcard Then
                FoundFiles = FileWildcardSearch(FoundFiles, FMain.FileWildcard)
            End If

            ' Filter by properties
            If FMain.EnablePropertyFilter Then
                System.Threading.Thread.Sleep(100)
                Dim UPF As New UtilsPropertyFilters(Me.FMain)
                FoundFiles = UPF.FilterProperties(FoundFiles)
            End If

            If FMain.SortAlphabetical Then
                FoundFiles = SortAlphabetical(FoundFiles)
            End If

            If FMain.SortRandomSample Then
                Dim Fraction As Double = 0.1
                Try
                    Fraction = CDbl(FMain.SortRandomSampleFraction)
                Catch ex As Exception
                    Fraction = 0.1
                    FMain.SortRandomSampleFraction = CStr(Fraction)
                End Try
                FoundFiles = SortRandomSample(FoundFiles, Fraction)
            End If
        End If


        ' Populate ListView
        If Not FoundFiles Is Nothing Then
            PopulateListView(FoundFiles)
        End If


        FMain.StopProcess = False
        FMain.ButtonCancel.Text = "Cancel"

        ListViewFiles.EndUpdate()

        FMain.Cursor = Cursors.Default

        ElapsedTime = Now.Subtract(StartTime).TotalMinutes
        If ElapsedTime < 60 Then
            ElapsedTimeText = "in " + ElapsedTime.ToString("0.0") + " min."
        Else
            ElapsedTimeText = "in " + (ElapsedTime / 60).ToString("0.0") + " hr."
        End If

        Dim filecount As Integer = ListViewFiles.Items.Count - ListViewFiles.Groups.Item("Sources").Items.Count
        FMain.TextBoxStatus.Text = String.Format("{0} files found in {1}", filecount, ElapsedTimeText)


    End Sub

    Private Function FindFiles(
        Source As ListViewItem,
        BareTopLevelAssembly As Boolean
        ) As IReadOnlyCollection(Of String)

        Dim NewWay As Boolean = True

        Dim FoundFiles As IReadOnlyCollection(Of String) = Nothing
        Dim ActiveFileExtensionsList As New List(Of String)

        Dim UP As New UtilsPreferences
        Dim StartupPath As String = UP.GetStartupDirectory()

        Dim UC As New UtilsCommon

        FMain.ListViewFilesOutOfDate = False

        FMain.StopProcess = False
        FMain.ButtonCancel.Text = "Stop"

        If FMain.FilterAsm Then ActiveFileExtensionsList.Add("*.asm")
        If FMain.FilterPar Then ActiveFileExtensionsList.Add("*.par")
        If FMain.FilterPsm Then ActiveFileExtensionsList.Add("*.psm")
        If FMain.FilterDft Then ActiveFileExtensionsList.Add("*.dft")

        If ActiveFileExtensionsList.Count > 0 Then

            Select Case Source.Tag.ToString
                Case = "Folder"
                    FMain.TextBoxStatus.Text = String.Format("Processing folder '{0}'", System.IO.Path.GetFileName(Source.Name))
                    System.Windows.Forms.Application.DoEvents()

                    If FileIO.FileSystem.DirectoryExists(Source.Name) Then
                        Try
                            FoundFiles = FileIO.FileSystem.GetFiles(Source.Name,
                                     FileIO.SearchOption.SearchTopLevelOnly,
                                     ActiveFileExtensionsList.ToArray)
                        Catch ex As Exception
                            Dim s As String = "An error occurred searching for files.  Please rectify the error and try again."
                            s = String.Format("{0}{1}{2}", s, vbCrLf, ex.ToString)
                            MsgBox(s, vbOKOnly)
                            FoundFiles = Nothing
                        End Try
                    End If

                Case = "Folders"
                    FMain.TextBoxStatus.Text = String.Format("Processing folders '{0}'", System.IO.Path.GetFileName(Source.Name))
                    System.Windows.Forms.Application.DoEvents()

                    If FileIO.FileSystem.DirectoryExists(Source.Name) Then

                        Dim tmplist = GetAllDirectories(Source.Name)

                        Dim tmpFolders As String()

                        tmpFolders = tmplist.ToArray

                        Dim tmpFoundFiles As New List(Of String)
                        Dim s As String = ""

                        For Each tmpFolder In tmpFolders

                            Try

                                tmpFoundFiles.AddRange(FileIO.FileSystem.GetFiles(tmpFolder,
                                FileIO.SearchOption.SearchTopLevelOnly,
                                ActiveFileExtensionsList.ToArray))

                            Catch ex As Exception

                                s = String.Format("{0}{1}{2}", s, tmpFolder, vbCrLf)

                            End Try

                        Next

                        FoundFiles = tmpFoundFiles

                        If Not s = "" Then
                            s = String.Format("The following folder(s) could not be processed and were ignored{0}{1}", vbCrLf, s)
                            MsgBox(s, vbOKOnly)
                        End If

                    End If

                Case = "csv", "txt"
                    FMain.TextBoxStatus.Text = String.Format("Processing list '{0}'", System.IO.Path.GetFileName(Source.Name))

                    If FileIO.FileSystem.FileExists(Source.Name) Then

                        Dim tmpFoundFiles = IO.File.ReadAllLines(Source.Name)

                        For i = 0 To tmpFoundFiles.Count - 1
                            tmpFoundFiles(i) = tmpFoundFiles(i).Split(CChar(",")).First
                        Next

                        FoundFiles = tmpFoundFiles

                    End If

                Case = "tsv"
                    FMain.TextBoxStatus.Text = String.Format("Processing list '{0}'", System.IO.Path.GetFileName(Source.Name))

                    If FileIO.FileSystem.FileExists(Source.Name) Then

                        Dim tmpFoundFiles = IO.File.ReadAllLines(Source.Name)

                        For i = 0 To tmpFoundFiles.Count - 1
                            tmpFoundFiles(i) = tmpFoundFiles(i).Split(CChar(vbTab)).First
                        Next

                        FoundFiles = tmpFoundFiles

                    End If

                Case = "excel"
                    FMain.TextBoxStatus.Text = String.Format("Processing excel '{0}'", System.IO.Path.GetFileName(Source.Name))

                    If FileIO.FileSystem.FileExists(Source.Name) Then
                        FoundFiles = UC.ReadExcel(Source.Name)
                    End If

                Case = "DragDrop"
                    Dim tmpFoundFiles As New List(Of String)

                    For Each tmpItem As ListViewItem In FMain.DragDropCache
                        tmpFoundFiles.Add(tmpItem.Name)
                    Next
                    FoundFiles = tmpFoundFiles

                Case = "Files"
                    Dim tmpFoundFiles As New List(Of String)

                    If NewWay Then
                        tmpFoundFiles.AddRange(Source.Name.Split(CChar(vbTab)))
                    Else
                        tmpFoundFiles.AddRange(Source.Name.Split(CChar(",")))
                    End If

                    FoundFiles = tmpFoundFiles

                Case = "TeamCenter"
                    Dim tmpFoundFiles As New List(Of String)

                    If NewWay Then
                        tmpFoundFiles.AddRange(Source.Name.Split(CChar(vbTab)))
                    Else
                        tmpFoundFiles.AddRange(Source.Name.Split(CChar(",")))
                    End If

                    FoundFiles = tmpFoundFiles

                Case = "ASM_Folder"
                    ' Nothing to do here.  Dealt with in 'Case = "asm"'

                Case = "asm"
                    FMain.TextBoxStatus.Text = String.Format("Processing top level assy '{0}'", System.IO.Path.GetFileName(Source.Name))
                    System.Windows.Forms.Application.DoEvents()

                    FoundFiles = ProcessTLA(BareTopLevelAssembly, Source, ActiveFileExtensionsList)

                Case Else
                    FoundFiles = Nothing
                    FMain.TextBoxStatus.Text = "No files found"
            End Select


        End If

        Return FoundFiles
    End Function


    Private Function GetAllDirectories(ByVal Path As String) As List(Of String)

        Dim tmpList As New List(Of String)

        tmpList.Add(Path)

        GetMyDirectories(Path, tmpList)

        Return tmpList

    End Function

    Public Sub GetMyDirectories(ByVal Path As String, ByRef DirectoriesList As List(Of String))

        Try

            Dim tmpList As String() = IO.Directory.GetDirectories(Path, "*.*", IO.SearchOption.TopDirectoryOnly)
            'Get Current Directories
            DirectoriesList.AddRange(tmpList)

            'Loop  over sub-directories
            For Each subDirectory As String In IO.Directory.GetDirectories(Path, "*.*", IO.SearchOption.TopDirectoryOnly)

                Me.GetMyDirectories(subDirectory, DirectoriesList)

            Next

        Catch ex As UnauthorizedAccessException
            'Access Denied exception

        Catch ex1 As Exception
            'Other exceptions

        End Try

    End Sub

    Public Sub PopulateListView(FoundFiles As IReadOnlyCollection(Of String))

        Dim UC As New UtilsCommon
        Dim tf As Boolean
        Dim NumProcessed As Integer = 0
        Dim PopulatePropertyColumns As Boolean = True

        ' ###### User input for displaying file properties for a large number of files.
        If (FMain.ListOfColumns.Count > 2) And (FoundFiles.Count > 1000) Then
            Dim s As String = String.Format("Getting file properties on {0} files can take some time.  ", FoundFiles.Count)
            s = String.Format("{0}Do you want to have them displayed anyway?", s)
            Dim Result As MsgBoxResult = MsgBox(s, vbYesNo)
            If Result = MsgBoxResult.No Then
                PopulatePropertyColumns = False
            End If
        End If

        ListViewFiles.BeginUpdate()

        For Each FoundFile In FoundFiles

            FMain.TextBoxStatus.Text = String.Format("Updating List {0}", System.IO.Path.GetFileName(FoundFile))

            If NumProcessed Mod 100 = 0 Then
                System.Windows.Forms.Application.DoEvents()
                If FMain.StopProcess Then
                    Exit For
                End If

            End If


            tf = True
            tf = tf And UC.FilenameIsOK(FoundFile)
            tf = tf And IO.File.Exists(FoundFile)

            If tf Then

                Dim tmpFinfo As IO.FileInfo = My.Computer.FileSystem.GetFileInfo(FoundFile)

                Dim tmpLVItem As New ListViewItem

                tmpLVItem.BackColor = Color.White

                tmpLVItem.Text = IO.Path.GetFileName(FoundFile)
                tmpLVItem.UseItemStyleForSubItems = False
                tmpLVItem.SubItems.Add(IO.Path.GetDirectoryName(FoundFile))

                tmpLVItem.ImageKey = "Unchecked"
                tmpLVItem.Tag = IO.Path.GetExtension(FoundFile).ToLower 'Backup gruppo
                tmpLVItem.Name = FoundFile
                tmpLVItem.Group = ListViewFiles.Groups.Item(IO.Path.GetExtension(FoundFile).ToLower)

                tmpLVItem.ToolTipText = tmpLVItem.Name & vbCrLf &
                    "Size: " & (tmpFinfo.Length / 1024).ToString("0") & " KB" & vbCrLf &
                    "Date created: " & tmpFinfo.CreationTime.ToShortDateString() & vbCrLf &
                    "Date modified: " & tmpFinfo.LastWriteTime.ToShortDateString()

                If tmpFinfo.IsReadOnly Then tmpLVItem.BackColor = Color.LightGray : tmpLVItem.SubItems.Item(1).BackColor = Color.LightGray '<----- setting the read-only backcolor

                ListViewFiles.Items.Add(tmpLVItem)
            Else
                ' Trying to add a file that does not exist
                Dim tmpLVItem As New ListViewItem

                tmpLVItem.BackColor = Color.White

                tmpLVItem.Text = IO.Path.GetFileName(FoundFile)
                tmpLVItem.UseItemStyleForSubItems = False
                tmpLVItem.SubItems.Add(IO.Path.GetDirectoryName(FoundFile))

                tmpLVItem.ImageKey = "Error"
                tmpLVItem.Tag = IO.Path.GetExtension(FoundFile).ToLower 'Backup gruppo
                tmpLVItem.Name = FoundFile
                tmpLVItem.Group = ListViewFiles.Groups.Item("Excluded") 'ListViewFiles.Groups.Item(IO.Path.GetExtension(FoundFile).ToLower)

                tmpLVItem.ToolTipText = "File not found"

                tmpLVItem.ForeColor = Color.Red : tmpLVItem.SubItems.Item(1).ForeColor = Color.Red

                ListViewFiles.Items.Add(tmpLVItem)
            End If

            NumProcessed += 1
        Next

        If PopulatePropertyColumns Then UpdatePropertiesColumns()

        ListViewFiles.EndUpdate()

        FMain.TextBoxStatus.Text = ""
    End Sub

    Public Function RemoveProblemFiles(
        FoundFiles As IReadOnlyCollection(Of String),
        ActiveFileExtensionsList As List(Of String)
        ) As IReadOnlyCollection(Of String)

        Dim UC As New UtilsCommon
        Dim tf As Boolean

        Dim tmpFoundFiles As New List(Of String)
        For Each item In FoundFiles

            If item IsNot Nothing Then 'better handling of first item being Nothing; this will not cause the excepetion; it's always better to handle known issues

                tf = UC.FilenameIsOK(item)             ' Testing file not found
                tf = tf And IO.File.Exists(item)       ' Testing file not found
                tf = True   '<-------- SHOW THE FILE NOT FOUND

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
            End If

        Next
        FoundFiles = CType(tmpFoundFiles, IReadOnlyCollection(Of String))

        Return FoundFiles

    End Function

    Public Function ProcessTLA(
        BareTopLevelAssembly As Boolean,
        Source As ListViewItem,
        ActiveFileExtensionsList As List(Of String)
        ) As IReadOnlyCollection(Of String)

        Dim FoundFiles As IReadOnlyCollection(Of String) = Nothing

        If (Not BareTopLevelAssembly) And (FileIO.FileSystem.FileExists(Source.Name)) Then
            Dim tmpFolders As New List(Of String)

            For Each item As ListViewItem In ListViewSources.Items
                If item.Tag.ToString = "ASM_Folder" Then
                    If Not tmpFolders.Contains(item.Name) Then tmpFolders.Add(item.Name)
                End If
            Next

            Dim tmpFoundFiles As New List(Of String)

            If FMain.TLABottomUp Then
                Dim UTLA As New UtilsTopLevelAssembly(FMain)

                FMain.TextBoxStatus.Text = "Finding all linked files.  This may take some time."

                tmpFoundFiles.AddRange(UTLA.GetLinksBottomUp(tmpFolders,
                                                           Source.SubItems.Item(1).Text,
                                                           ActiveFileExtensionsList,
                                                           FMain.DraftAndModelSameName,
                                                           FMain.TLAReportUnrelatedFiles))

            Else
                Dim UTLA As New UtilsTopLevelAssembly(FMain)
                tmpFoundFiles.AddRange(UTLA.GetLinksTopDown(tmpFolders,
                                                       Source.SubItems.Item(1).Text,
                                                       ActiveFileExtensionsList,
                                                       Report:=FMain.TLAReportUnrelatedFiles))
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
                                                           FMain.DraftAndModelSameName,
                                                           FMain.TLAReportUnrelatedFiles))

            FoundFiles = CType(tmpFoundFiles, IReadOnlyCollection(Of String))

            FMain.TextBoxStatus.Text = ""

        End If

        Return FoundFiles

    End Function

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
        Dim FoundFilesArray As String() = {}
        Dim FoundFilesList As New List(Of String)
        Dim Filename As String

        'Dim FileExtension As String = FileWildcard.Replace("*", "")
        Dim FileExtension As String = ""

        Select Case FileWildcard
            Case "*.asm", "*.par", "*.psm", "*.dft"
                FileExtension = FileWildcard.Replace("*", "")
            Case "*.*"
                FileExtension = FileWildcard
        End Select

        FMain.TextBoxStatus.Text = "Getting files..."

        If ListViewFiles.SelectedItems.Count > 0 Then
            For i As Integer = 0 To ListViewFiles.SelectedItems.Count - 1
                Filename = ListViewFiles.SelectedItems.Item(i).Name
                If System.IO.Path.GetExtension(Filename) = FileExtension Or FileExtension = "*.*" Then
                    FoundFilesList.Add(Filename)
                End If
            Next
        Else
            Dim ProcessLast As New List(Of String)  ' Trying to process top level assy last

            For i As Integer = 0 To ListViewFiles.Items.Count - 1
                Filename = ListViewFiles.Items(i).Name
                If System.IO.Path.GetExtension(Filename) = FileExtension Or FileExtension = "*.*" Then
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
        Dim DependencyDict As New Dictionary(Of String, List(Of String))
        Dim Filename As String

        Dim SSDoc As HCStructuredStorageDoc = Nothing
        Dim UP As New UtilsPreferences

        For Each Filename In Foundfiles

            FMain.TextBoxStatus.Text = String.Format("Dependency Sort (this can take some time) {0}", IO.Path.GetFileName(Filename))
            System.Windows.Forms.Application.DoEvents()

            Try
                SSDoc = New HCStructuredStorageDoc(Filename)
                SSDoc.ReadLinks(FMain.LinkManagementOrder)
            Catch ex As Exception
                If SSDoc IsNot Nothing Then SSDoc.Close()
            End Try

            Dim tmpDependencyDict As New Dictionary(Of String, List(Of String))

            tmpDependencyDict = GetLinks(SSDoc, tmpDependencyDict, MissingFilesList)

            If SSDoc IsNot Nothing Then SSDoc.Close()

            For Each s As String In tmpDependencyDict.Keys
                FMain.TextBoxStatus.Text = s
                If Not DependencyDict.Keys.Contains(s) Then
                    DependencyDict.Add(s, tmpDependencyDict(s))

                End If
            Next

        Next

        OutList = SortByDependency(DependencyDict)

        If MissingFilesList.Count > 0 Then
            Dim Timestamp As String = System.DateTime.Now.ToString("yyyyMMdd_HHmmss")

            Dim MissingFilesFileName As String
            MissingFilesFileName = $"{UP.GetTempDirectory}\{Timestamp}_Missing_Files.log"

            Try
                Using writer As New IO.StreamWriter(MissingFilesFileName, True)
                    writer.WriteLine("FILES NOT FOUND")
                    For Each Filename In MissingFilesList
                        writer.WriteLine(String.Format(Filename))
                    Next
                End Using

                Try
                    ' Try to use the default application to open the file.
                    Process.Start(MissingFilesFileName)
                Catch ex As Exception
                    ' If none, open with notepad.exe
                    Process.Start("notepad.exe", MissingFilesFileName)
                End Try


            Catch ex As Exception
            End Try

        End If

        Return OutList

    End Function

    Private Function GetLinks(
        SSDoc As HCStructuredStorageDoc,
        LinkDict As Dictionary(Of String, List(Of String)),
        MissingFilesList As List(Of String)
        ) As Dictionary(Of String, List(Of String))

        Dim SSDocName As String
        Dim ValidExtensions As New List(Of String)({".par", ".psm", ".asm", ".dft"})

        Dim SSLinkNames As List(Of String)
        Dim SSLinkName As String
        Dim SSLinkDoc As HCStructuredStorageDoc = Nothing

        Dim UC As New UtilsCommon

        SSDocName = SSDoc.FullName

        If Not LinkDict.Keys.Contains(SSDocName) Then

            LinkDict.Add(SSDocName, New List(Of String))

            If Not SSDoc.IsFOPMaster Then
                SSLinkNames = SSDoc.GetLinkNames
                If SSLinkNames.Count > 0 Then
                    For Each SSLinkName In SSLinkNames

                        If ValidExtensions.Contains(IO.Path.GetExtension(SSLinkName)) Then
                            If IO.File.Exists(SSLinkName) Then
                                Try
                                    SSLinkDoc = New HCStructuredStorageDoc(SSLinkName)
                                    SSLinkDoc.ReadLinks(FMain.LinkManagementOrder)
                                    LinkDict(SSDocName).Add(SSLinkName)
                                    LinkDict = GetLinks(SSLinkDoc, LinkDict, MissingFilesList)
                                    SSLinkDoc.Close()
                                Catch ex As Exception
                                    If SSLinkDoc IsNot Nothing Then SSLinkDoc.Close()
                                End Try
                            Else
                                If Not MissingFilesList.Contains(SSLinkName) Then
                                    MissingFilesList.Add(SSLinkName)
                                End If
                            End If
                        End If
                    Next
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

        If Not FMain.SortIncludeNoDependencies Then
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

        For Each Item As ListViewItem In ListViewSources.Items

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

        Next

        Return SourceDirectories
    End Function

    Public Sub UpdatePropertiesColumns()

        Dim UC As New UtilsCommon

        ListViewFiles.BeginUpdate()

        'Resetting the columns
        If ListViewFiles.Columns.Count > 2 Then
            Do Until ListViewFiles.Columns.Count = 2
                ListViewFiles.Columns.RemoveAt(ListViewFiles.Columns.Count - 1)
            Loop
        End If

        Dim NumProcessed As Integer = 0

        For Each tmpLVItem As ListViewItem In ListViewFiles.Items

            If tmpLVItem.SubItems.Count > 2 Then

                Do Until tmpLVItem.SubItems.Count = 2

                    tmpLVItem.SubItems.RemoveAt(tmpLVItem.SubItems.Count - 1)

                Loop

            End If

            tmpLVItem.UseItemStyleForSubItems = False

            Dim FullName As String = tmpLVItem.SubItems.Item(0).Name

            FMain.TextBoxStatus.Text = String.Format("Getting properties {0}", System.IO.Path.GetFileName(FullName))
            If NumProcessed Mod 100 = 0 Then
                System.Windows.Forms.Application.DoEvents()
                If FMain.StopProcess Then
                    Exit For
                End If

            End If

            Dim IsReadOnly As Boolean = tmpLVItem.SubItems(0).BackColor = Color.LightGray

            UpdateLVItem(tmpLVItem, IsReadOnly)

            NumProcessed += 1
        Next

        CreateColumns()

        ListViewFiles.EndUpdate()

    End Sub

    Private Sub CreateColumns()

        For Each PropName In FMain.ListOfColumns

            If PropName.Name <> "Name" And PropName.Name <> "Path" Then
                ListViewFiles.Columns.Add(PropName.Name, 0)
                If PropName.Visible Then ListViewFiles.Columns.Item(ListViewFiles.Columns.Count - 1).Width = PropName.Width
            ElseIf PropName.Name = "Name" Then
                If Not PropName.Visible Then
                    ListViewFiles.Columns.Item(0).Width = 0
                Else
                    ListViewFiles.Columns.Item(0).Width = PropName.Width
                End If
            ElseIf PropName.Name = "Path" Then
                If Not PropName.Visible Then
                    ListViewFiles.Columns.Item(1).Width = 0
                Else
                    ListViewFiles.Columns.Item(1).Width = PropName.Width
                End If
            End If

        Next

    End Sub

    Private Sub UpdateLVItem(LVItem As ListViewItem, IsReadOnly As Boolean)


        '#### For some reason LVItem.Bounds.Bottom return a wrong value, ListViewCollapsible is the suspect
        'If LVItem.Bounds.IntersectsWith(FMain.ListViewFiles.ClientRectangle) Then
        'Dim x = LVItem.Bounds
        'Dim y = LVItem.GetBounds(ItemBoundsPortion.Entire)
        'Dim z = FMain.ListViewFiles.ClientRectangle

        If (LVItem.Group.Name <> "Sources") And (FMain.ListOfColumns.Count > 2) Then
            Dim UC As New UtilsCommon
            Try
                'Adding extra properties data if needed
                For Each PropColumn In FMain.ListOfColumns
                    If PropColumn.Name <> "Name" And PropColumn.Name <> "Path" Then

                        Dim PropValue As String = Nothing
                        Dim tmpColor As Color = LVItem.BackColor 'Color.White ####### Changed to reflect the file read-only backcolor

                        If PropColumn.Visible Then '###### Only a visible column need a value. F.Arfilli

                            Try
                                Dim SSDoc As New HCStructuredStorageDoc(LVItem.Name, _OpenReadWrite:=False)
                                SSDoc.ReadProperties(FMain.PropertiesData)

                                Dim tmpErrorLogger As New Logger("tmpLogger", Nothing)
                                PropValue = SSDoc.SubstitutePropertyFormulas(PropColumn.Formula, tmpErrorLogger)

                                SSDoc.Close()

                            Catch ex As Exception
                                'I think this quit working because:
                                ' -- The SSDoc New() call no longer throws an exception on read-only files.
                                ' -- If PropColumn.Formula is not found in SSDoc, there is no exception.  It returns Nothing.
                                'PropValue = ""
                                'tmpColor = Color.Gainsboro '<--- Properties not present ######## for some reason this doesn't work anymore
                            End Try

                            If IsNothing(PropValue) Then
                                PropValue = ""
                                tmpColor = Color.Gainsboro '<---------- Properties not present
                            End If

                            If IsReadOnly Then tmpColor = Color.LightGray

                        End If

                        LVItem.SubItems.Add(PropValue, Color.Empty, tmpColor, LVItem.Font)

                    End If

                Next
                Application.DoEvents()

            Catch ex As Exception

                For Each PropColumn In FMain.ListOfColumns
                    LVItem.SubItems.Add("** Error **", Color.Black, Color.LightPink, LVItem.Font)
                Next

            End Try

        Else
            'Eventually insert code to personalize Sources group

        End If

    End Sub

End Class
