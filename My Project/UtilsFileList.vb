Option Strict On

'Imports System.Reflection
'Imports ListViewExtended

Public Class UtilsFileList
    Public Property ListViewFiles As ListViewExtended.ListViewCollapsible
    Public Property ListViewSources As ListView
    Public Property FMain As Form_Main
    Public Property ErrorLogger As HCErrorLogger
    Private Property ListOfColumns As List(Of PropertyColumn)


    Public Sub New(_Form_Main As Form_Main)
        Me.FMain = _Form_Main
        Me.ListViewFiles = Me.FMain.ListViewFiles
        Me.ListViewSources = Me.FMain.ListViewSources
        Me.ErrorLogger = New HCErrorLogger("Housekeeper")

        Me.ListOfColumns = New List(Of PropertyColumn)
        If FMain.ListOfColumns IsNot Nothing Then
            For Each tmpPropCol As PropertyColumn In FMain.ListOfColumns
                Me.ListOfColumns.Add(tmpPropCol.DeepCopy)
            Next
        End If
    End Sub


    Public Sub New_UpdateFileList()

        Dim GroupTags As New List(Of String)
        Dim BareTopLevelAssembly As Boolean = False

        Dim ElapsedTime As Double
        Dim ElapsedTimeText As String

        Dim StartTime As DateTime = Now

        Dim ActiveExtensions As List(Of String) = GetActiveExtensions()

        FMain.TextBoxStatus.Text = "Updating list..."
        FMain.LabelTimeRemaining.Text = ""
        System.Windows.Forms.Application.DoEvents()

        ListViewFiles.Items.Clear()

        If Not CheckStartConditions(BareTopLevelAssembly) Then
            FMain.Cursor = Cursors.Default
            FMain.TextBoxStatus.Text = ""

            Exit Sub
        End If

        Dim tmpFoundFiles As New List(Of String)

        Dim FindFilesLogger As Logger = Me.ErrorLogger.AddFile("Find Files")

        For Each item As ListViewItem In ListViewSources.Items
            Dim tmptmpFoundFiles = FindFiles(item, BareTopLevelAssembly, FindFilesLogger)
            If tmptmpFoundFiles IsNot Nothing Then
                tmpFoundFiles.AddRange(tmptmpFoundFiles)
            End If
            If CheckInterruptRequest() Then
                Exit Sub
            End If
        Next

        If Not FMain.SortNone Then
            tmpFoundFiles = tmpFoundFiles.Distinct(System.StringComparer.OrdinalIgnoreCase).ToList
        Else
            If Not FMain.KeepUnsortedDuplicates Then
                tmpFoundFiles = tmpFoundFiles.Distinct(System.StringComparer.OrdinalIgnoreCase).ToList
            End If
        End If

        Dim FoundFiles As IReadOnlyCollection(Of String)
        FoundFiles = tmpFoundFiles

        ' Dependency sort
        If FoundFiles IsNot Nothing Then
            If FMain.SortDependency Then
                FoundFiles = GetDependencySortedFiles(FoundFiles, FindFilesLogger)
            End If
            If CheckInterruptRequest() Then
                Exit Sub
            End If
        End If

        ' Run filters
        If FoundFiles IsNot Nothing Then

            ' Filter by file type
            FoundFiles = FilterByFiletype(FoundFiles, ActiveExtensions)
            If CheckInterruptRequest() Then
                Exit Sub
            End If


            ' Filter by file wildcard search
            If FMain.EnableFileWildcard Then
                FoundFiles = FileWildcardSearch(FoundFiles, FMain.FileWildcard)
                If CheckInterruptRequest() Then
                    Exit Sub
                End If
            End If

            ' Filter by properties
            If FMain.EnablePropertyFilter Then
                Dim UPF As New UtilsPropertyFilters(Me.FMain, FindFilesLogger)
                FoundFiles = UPF.FilterProperties(FoundFiles)
                If CheckInterruptRequest() Then
                    Exit Sub
                End If

            End If

            If FMain.SortAlphabetical Then
                FoundFiles = SortAlphabetical(FoundFiles)
            End If

            If FMain.SortRandomSample Then
                FoundFiles = SortRandomSample(FoundFiles, FMain.SortRandomSampleFraction, FindFilesLogger)
            End If
        End If

        ListViewFiles.BeginUpdate()

        ' Populate ListView
        If Not FoundFiles Is Nothing Then
            PopulateListView(FoundFiles)
            If CheckInterruptRequest() Then
                ListViewFiles.Items.Clear()
                ListViewFiles.EndUpdate()
                Exit Sub
            End If
        End If

        ListViewFiles.EndUpdate()

        FMain.Cursor = Cursors.Default

        ElapsedTime = Now.Subtract(StartTime).TotalMinutes
        If ElapsedTime < 60 Then
            ElapsedTimeText = $"in {ElapsedTime.ToString("0.0")} min."
        Else
            ElapsedTimeText = $"in {(ElapsedTime / 60).ToString("0.0")} hr."
        End If

        Dim filecount As Integer = ListViewFiles.Items.Count

        If Not FMain.StopProcess Then FMain.TextBoxStatus.Text = $"{filecount} files found in {ElapsedTimeText}"

        If Me.ErrorLogger.HasErrors Then ErrorLogger.ReportErrors(UseMessageBox:=False)

        ' This should happen in the caller
        'FMain.StopProcess = False
        'FMain.ButtonCancel.Text = "Cancel"


    End Sub

    Private Function CheckStartConditions(ByRef BareTopLevelAssembly As Boolean) As Boolean
        Dim Proceed As Boolean = True

        Dim StartLogger As Logger = ErrorLogger.AddFile("Check Start Conditions")

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
                StartLogger.AddMessage(msg)
            End If
        End If

        If GroupTags.Contains("asm") Then
            If (FMain.TLABottomUp) And (Not FileIO.FileSystem.FileExists(FMain.FastSearchScopeFilename)) Then
                msg = "Fast search scope file not found.  Set it on the Configuration Tab -- Top Level Assembly Page."
                StartLogger.AddMessage(msg)
            End If

            If (FMain.TLATopDown) And (Not FileIO.FileSystem.FileExists(FMain.LinkManagementFilename)) Then
                msg = "LinkMgmt.txt file not found.  Set it on the Configuration Tab -- Top Level Assembly Page."
                StartLogger.AddMessage(msg)
            End If

            If GroupTags.Where(Function(t) t = "asm").Count() > 1 Then
                StartLogger.AddMessage("Only one top-level assembly is allowed. Please delete the extras to proceed.")
            End If

            If Not (GroupTags.Contains("ASM_Folder")) Then
                If FMain.WarnBareTLA Then
                    msg = "A top-level assembly with no top-level folder detected.  "
                    msg += "No 'Where Used' will be performed."
                    msg += "Disable this check on the Configuration Tab -- Top Level Assembly page"
                    StartLogger.AddMessage(msg)
                Else
                    BareTopLevelAssembly = True
                End If
            End If

        End If

        If GroupTags.Contains("ActiveFile") Or GroupTags.Contains("ActiveFiles") Then

            If GroupTags.Where(Function(t) t = "ActiveFile").Count > 1 Then
                StartLogger.AddMessage("Only one Active File entry is allowed. Please delete the extras to proceed.")
            End If

            If GroupTags.Where(Function(t) t = "ActiveFiles").Count > 1 Then
                StartLogger.AddMessage("Only one Active Files entry is allowed. Please delete the extras to proceed.")
            End If

            If GroupTags.Contains("ActiveFile") And GroupTags.Contains("ActiveFiles") Then
                StartLogger.AddMessage("Cannot process an Active File AND Active Files.  Please delete one or the other.")
            End If

            Dim USEA As New UtilsSEApp(FMain)

            If Not USEA.SEIsRunning Then
                StartLogger.AddMessage("SE must be running to obtain active files")
            Else
                Dim Result = USEA.GetActiveFiles

                If Not Result.Success Then
                    StartLogger.AddMessage("Unable to connect to Solid Edge")
                Else

                    If GroupTags.Contains("ActiveFile") Then
                        Dim ActiveFile = Result.ActiveFile
                        If ActiveFile = "" Then
                            StartLogger.AddMessage("No file open in SE")
                        ElseIf Not IO.File.Exists(ActiveFile) Then
                            StartLogger.AddMessage($"File must be saved before continuing: '{IO.Path.GetFileName(ActiveFile)}'")
                        End If
                    End If

                    If GroupTags.Contains("ActiveFiles") Then
                        Dim ActiveFiles As List(Of String) = Result.ActiveFiles
                        If ActiveFiles.Count = 0 Then
                            StartLogger.AddMessage("No files open in SE")
                        Else
                            For Each ActiveFile As String In ActiveFiles
                                If Not IO.File.Exists(ActiveFile) Then
                                    StartLogger.AddMessage($"File must be saved before continuing: '{IO.Path.GetFileName(ActiveFile)}'")
                                End If
                            Next
                        End If
                    End If

                End If

            End If


        End If

        If Me.ErrorLogger.HasErrors Then
            Me.ErrorLogger.ReportErrors(UseMessageBox:=True)
            Proceed = False
        End If

        Return Proceed
    End Function


    ' ###### FILES AND DIRECTORIES ######

    Private Function FindFiles(
        Source As ListViewItem,
        BareTopLevelAssembly As Boolean,
        FindFilesLogger As Logger
        ) As IReadOnlyCollection(Of String)

        ' Searches one Source from ListViewSources.  They are aggregated in the calling function.

        Dim SubLogger As Logger = FindFilesLogger.AddLogger(Source.Name)

        Dim FoundFiles As IReadOnlyCollection(Of String) = Nothing

        FMain.ListViewFilesOutOfDate = False

        Dim ActiveExtensions As List(Of String) = GetActiveExtensions()

        If ActiveExtensions.Count = 0 Then
            Return FoundFiles
        End If

        Select Case Source.Tag.ToString

            Case = "Files"
                FoundFiles = DoTabDelimitedString(Source, SubLogger)
            Case = "Folder"
                FoundFiles = DoFolder(Source, ActiveExtensions, SubLogger)
            Case = "Folders"
                FoundFiles = DoFolders(Source, ActiveExtensions, SubLogger)
            Case = "ASM_Folder"
                    ' Nothing to do here.  Dealt with in 'Case = "asm"'
            Case = "asm"
                FoundFiles = DoTLA(BareTopLevelAssembly, Source, ActiveExtensions, SubLogger)
            Case "ActiveFile"
                FoundFiles = DoActiveFile(Source, SubLogger)
            Case "ActiveFiles"
                FoundFiles = DoActiveFiles(Source, SubLogger)
            Case = "TeamCenter"
                FoundFiles = DoTabDelimitedString(Source, SubLogger)
            Case = "txt", "csv", "tsv"
                FoundFiles = DoTextList(Source, SubLogger)
            Case = "excel"
                FoundFiles = DoExcelList(Source, SubLogger)

                'Case = "DragDrop"
                ' Now handled as a 'Files' source

            Case Else
                FoundFiles = Nothing
                FMain.TextBoxStatus.Text = "No files found"
        End Select

        ' Remove problem files
        If FoundFiles IsNot Nothing Then
            If Not (Source.Tag.ToString = "Folder" OrElse Source.Tag.ToString = "Folders") Then
                Dim ProblemFilesLogger As Logger = SubLogger.AddLogger("Unable to process all files")
                FoundFiles = RemoveProblemFiles(FoundFiles, ProblemFilesLogger)
                If CheckInterruptRequest() Then
                    FoundFiles = Nothing
                End If
            End If
        End If

        Return FoundFiles
    End Function


    Private Function GetActiveExtensions() As List(Of String)
        Dim ActiveExtensions As New List(Of String)

        If FMain.FilterAsm Then ActiveExtensions.Add("*.asm")
        If FMain.FilterPar Then ActiveExtensions.Add("*.par")
        If FMain.FilterPsm Then ActiveExtensions.Add("*.psm")
        If FMain.FilterDft Then ActiveExtensions.Add("*.dft")

        Return ActiveExtensions
    End Function

    Private Function DoTabDelimitedString(
        Source As ListViewItem,
        SubLogger As Logger
        ) As IReadOnlyCollection(Of String)

        Dim tmpFoundFiles As New List(Of String)
        tmpFoundFiles.AddRange(Source.Name.Split(CChar(vbTab)))
        Return tmpFoundFiles
    End Function

    Private Function DoFolder(
        Source As ListViewItem,
        ActiveExtensions As List(Of String),
        SubLogger As Logger
        ) As IReadOnlyCollection(Of String)

        Dim tmpFoundFiles As IReadOnlyCollection(Of String) = Nothing

        FMain.TextBoxStatus.Text = $"Processing folder '{System.IO.Path.GetFileName(Source.Name)}'"
        System.Windows.Forms.Application.DoEvents()

        If FileIO.FileSystem.DirectoryExists(Source.Name) Then
            Try
                tmpFoundFiles = FileIO.FileSystem.GetFiles(Source.Name,
                                     FileIO.SearchOption.SearchTopLevelOnly,
                                     ActiveExtensions.ToArray)
            Catch ex As Exception
                SubLogger.AddMessage($"Could not process '{Source.Name}'.  Exception: {ex.Message}")
                tmpFoundFiles = Nothing
            End Try
        Else
            SubLogger.AddMessage($"Could not process '{Source.Name}'")
        End If


        Return tmpFoundFiles
    End Function

    Private Function DoFolders(
        Source As ListViewItem,
        ActiveExtensions As List(Of String),
        SubLogger As Logger
        ) As IReadOnlyCollection(Of String)

        Dim myFoundFiles As New List(Of String)

        FMain.TextBoxStatus.Text = $"Processing folders '{System.IO.Path.GetFileName(Source.Name)}'"
        System.Windows.Forms.Application.DoEvents()

        If FileIO.FileSystem.DirectoryExists(Source.Name) Then

            Dim tmplist = GetAllDirectories(Source.Name, SubLogger)
            Dim tmpFolders As String()
            tmpFolders = tmplist.ToArray
            Dim tmpFoundFiles As New List(Of String)
            Dim s As String = ""

            For Each tmpFolder In tmpFolders
                Try
                    tmpFoundFiles.AddRange(FileIO.FileSystem.GetFiles(tmpFolder,
                                FileIO.SearchOption.SearchTopLevelOnly,
                                ActiveExtensions.ToArray))
                Catch ex As Exception
                    s = $"{s}{tmpFolder}{vbCrLf}"
                End Try
            Next

            myFoundFiles = tmpFoundFiles

            If Not s = "" Then
                s = $"Could not process the following folder(s) in {Source.Name}{vbCrLf}{s}"
                SubLogger.AddMessage(s)
            End If

        Else
            SubLogger.AddMessage($"Could not process '{Source.Name}'")
        End If

        Return myFoundFiles
    End Function

    Private Function DoActiveFile(
        Source As ListViewItem,
        SubLogger As Logger
        ) As IReadOnlyCollection(Of String)

        Dim tmpFoundFiles As New List(Of String)

        Dim USEA As New UtilsSEApp(FMain)

        If USEA.SEIsRunning Then
            Dim Result = USEA.GetActiveFiles
            Dim ActiveFile = Result.ActiveFile
            tmpFoundFiles.Add(ActiveFile)
        End If

        Return tmpFoundFiles
    End Function

    Private Function DoActiveFiles(
        Source As ListViewItem,
        SubLogger As Logger
        ) As IReadOnlyCollection(Of String)

        Dim tmpFoundFiles As New List(Of String)

        Dim USEA As New UtilsSEApp(FMain)

        If USEA.SEIsRunning Then
            Dim Result = USEA.GetActiveFiles
            Dim ActiveFiles = Result.ActiveFiles
            tmpFoundFiles = ActiveFiles
        End If

        Return tmpFoundFiles
    End Function

    Private Function DoTextList(
        Source As ListViewItem,
        SubLogger As Logger
        ) As IReadOnlyCollection(Of String)

        Dim tmpFoundFiles As New List(Of String)

        Dim ListName As String = System.IO.Path.GetFileName(Source.Name)
        FMain.TextBoxStatus.Text = $"Processing list '{ListName}'"
        System.Windows.Forms.Application.DoEvents()

        If FileIO.FileSystem.FileExists(Source.Name) Then

            Try
                Dim InFiles As String() = IO.File.ReadAllLines(Source.Name)
                Dim OutFiles As New List(Of String)

                For Each InFile As String In InFiles
                    Select Case Source.Tag.ToString
                        Case "txt"
                            InFile = InFile.Trim
                        Case "csv"
                            InFile = InFile.Split(CChar(",")).First
                            InFile = InFile.Replace("""", "").Trim
                        Case "tsv"
                            InFile = InFile.Split(CChar(vbTab)).First.Trim
                    End Select
                    OutFiles.Add(InFile)
                Next

                tmpFoundFiles = OutFiles
            Catch ex As Exception
                SubLogger.AddMessage($"Could not process '{ListName}'")
            End Try

        Else
            SubLogger.AddMessage($"File not found '{ListName}'")
        End If

        Return tmpFoundFiles
    End Function

    Private Function DoExcelList(
        Source As ListViewItem,
        SubLogger As Logger
        ) As IReadOnlyCollection(Of String)

        Dim tmpFoundFiles As New List(Of String)

        FMain.TextBoxStatus.Text = $"Processing excel '{System.IO.Path.GetFileName(Source.Name)}'"
        System.Windows.Forms.Application.DoEvents()

        If FileIO.FileSystem.FileExists(Source.Name) Then
            Dim UC As New UtilsCommon

            Try
                tmpFoundFiles = UC.ReadExcel(Source.Name)
            Catch ex As Exception
                SubLogger.AddMessage($"Could not process '{System.IO.Path.GetFileName(Source.Name)}'")
            End Try
        Else
            SubLogger.AddMessage($"File not found '{System.IO.Path.GetFileName(Source.Name)}'")
        End If

        Return tmpFoundFiles
    End Function

    Private Function GetAllDirectories(
        ByVal Path As String,
        SubLogger As Logger
        ) As List(Of String)

        Dim tmpList As New List(Of String)

        tmpList.Add(Path)

        GetMyDirectories(Path, tmpList, SubLogger)

        Return tmpList

    End Function

    Private Sub GetMyDirectories(
        ByVal Path As String,
        ByRef DirectoriesList As List(Of String),
        SubLogger As Logger)

        'Dim SubDirectory As String = ""
        Try

            Dim tmpList As String() = IO.Directory.GetDirectories(Path, "*.*", IO.SearchOption.TopDirectoryOnly)
            'Get Current Directories
            DirectoriesList.AddRange(tmpList)

            'Loop  over sub-directories
            'For Each subDirectory As String In IO.Directory.GetDirectories(Path, "*.*", IO.SearchOption.TopDirectoryOnly)
            For Each SubDirectory As String In tmpList

                Me.GetMyDirectories(SubDirectory, DirectoriesList, SubLogger)

            Next

        Catch ex As UnauthorizedAccessException
            'Access Denied exception

        Catch ex1 As Exception
            SubLogger.AddMessage($"Could not process '{Path}'")

        End Try

    End Sub


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

        'FMain.TextBoxStatus.Text = "Getting files..."

        If ListViewFiles.SelectedItems.Count > 0 Then
            For i As Integer = 0 To ListViewFiles.SelectedItems.Count - 1
                Filename = ListViewFiles.SelectedItems.Item(i).Name
                If System.IO.Path.GetExtension(Filename).ToLower = FileExtension Or FileExtension = "*.*" Then
                    FoundFilesList.Add(Filename)
                End If
            Next
        Else
            'Dim ProcessLast As New List(Of String)  ' Trying to process top level assy last

            For i As Integer = 0 To ListViewFiles.Items.Count - 1
                Filename = ListViewFiles.Items(i).Name
                If System.IO.Path.GetExtension(Filename).ToLower = FileExtension Or FileExtension = "*.*" Then
                    If ListViewFiles.Items(i).Group.Name <> "Excluded" Then
                        'If ListViewFiles.Items(i).Group.Name = "Sources" Then
                        '    ProcessLast.Add(Filename)
                        'Else
                        '    FoundFilesList.Add(Filename)
                        'End If
                        FoundFilesList.Add(Filename)
                    End If
                End If
            Next

            'For Each Filename In ProcessLast
            '    FoundFilesList.Add(Filename)
            'Next

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

    Private Function RemoveProblemFiles(
        FoundFiles As IReadOnlyCollection(Of String),
        SubLogger As Logger
        ) As List(Of String)

        Dim tmpFoundFiles As New List(Of String)

        Dim UC As New UtilsCommon

        Dim BadFilenames As New List(Of String)
        Dim MissingFiles As New List(Of String)

        For Each Filename In FoundFiles
            If Filename IsNot Nothing Then
                If Not Filename.Trim = "" Then
                    If UC.FilenameIsOK(Filename) Then
                        If Not FMain.MoveMissingToExcluded Then
                            If IO.File.Exists(Filename) Then
                                tmpFoundFiles.Add(Filename)
                            Else
                                MissingFiles.Add(Filename)
                            End If
                        Else
                            tmpFoundFiles.Add(Filename)
                        End If
                    Else
                        BadFilenames.Add(Filename)
                    End If
                End If
            End If
        Next

        If BadFilenames.Count > 0 Then
            Dim BadLogger As Logger = SubLogger.AddLogger("Invalid file names")
            For Each BadFilename As String In BadFilenames
                BadLogger.AddMessage(BadFilename)
            Next
        End If

        If MissingFiles.Count > 0 AndAlso Not FMain.MoveMissingToExcluded Then
            Dim MissingLogger As Logger = SubLogger.AddLogger("Files not found")
            For Each MissingFile As String In MissingFiles
                MissingLogger.AddMessage(MissingFile)
            Next
        End If

        Return tmpFoundFiles

    End Function

    Private Function FilterByFiletype(
        FoundFiles As IReadOnlyCollection(Of String),
        ActiveFileExtensionsList As List(Of String)
        ) As IReadOnlyCollection(Of String)

        Dim tmpFoundFiles As New List(Of String)

        Dim UC As New UtilsCommon

        For Each Filename In FoundFiles
            If Filename IsNot Nothing Then
                Dim Extension As String = UC.GetExtension(Filename)

                ' ActiveFileExtensionsList has '*.par' format.  That is required for folder searches.
                ' GetExtension does not add the asterisk.
                Extension = Extension.Replace(".", "*.")

                If ActiveFileExtensionsList.Contains(Extension.ToLower) Then tmpFoundFiles.Add(Filename)
            End If
        Next

        Return tmpFoundFiles

    End Function

    ' ###### TOP LEVEL ASSEMBLY ######

    Private Function DoTLA(
        BareTopLevelAssembly As Boolean,
        Source As ListViewItem,
        ActiveFileExtensionsList As List(Of String),
        SubLogger As Logger
        ) As IReadOnlyCollection(Of String)

        FMain.TextBoxStatus.Text = $"Processing top level assembly '{System.IO.Path.GetFileName(Source.Name)}'"
        System.Windows.Forms.Application.DoEvents()

        Dim FoundFiles As IReadOnlyCollection(Of String) = Nothing

        If Not FileIO.FileSystem.FileExists(Source.Name) Then
            SubLogger.AddMessage($"File not found '{Source.Name}'")
            Return Nothing
        End If

        If Not BareTopLevelAssembly Then
            Dim tmpFolders As New List(Of String)

            For Each item As ListViewItem In ListViewSources.Items
                If item.Tag.ToString = "ASM_Folder" Then
                    If Not tmpFolders.Contains(item.Name) Then tmpFolders.Add(item.Name)
                End If
            Next

            Dim tmpFoundFiles As New List(Of String)

            If FMain.TLABottomUp Then
                Dim UTLA As New UtilsTopLevelAssembly(FMain, SubLogger)

                FMain.TextBoxStatus.Text = "Finding all linked files.  This may take some time."

                tmpFoundFiles.AddRange(UTLA.GetLinksBottomUp(tmpFolders,
                                                           Source.SubItems.Item(1).Text,
                                                           ActiveFileExtensionsList,
                                                           FMain.DraftAndModelSameName,
                                                           FMain.TLAReportUnrelatedFiles))

            Else
                Dim UTLA As New UtilsTopLevelAssembly(FMain, SubLogger)

                tmpFoundFiles.AddRange(UTLA.GetLinksTopDown(tmpFolders,
                                                       Source.SubItems.Item(1).Text,
                                                       ActiveFileExtensionsList,
                                                       Report:=FMain.TLAReportUnrelatedFiles))
            End If

            FoundFiles = CType(tmpFoundFiles, IReadOnlyCollection(Of String))

            FMain.TextBoxStatus.Text = ""

        Else

            Dim UTLA As New UtilsTopLevelAssembly(FMain, SubLogger)

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

            'FMain.TextBoxStatus.Text = ""

        End If

        Return FoundFiles

    End Function


    ' ###### SORT AND FILTER ######

    Private Function FileWildcardSearch(
        ByVal FoundFiles As IReadOnlyCollection(Of String),
        ByVal WildcardString As String) As IReadOnlyCollection(Of String)

        Dim tmpFoundFiles As New List(Of String)
        Dim FilePath As String
        Dim Filename As String

        Dim Count As Integer = 0
        For Each FilePath In FoundFiles
            Filename = System.IO.Path.GetFileName(FilePath)

            If Count Mod 100 = 0 Then
                FMain.TextBoxStatus.Text = $"Wildcard Search {Filename}"
                System.Windows.Forms.Application.DoEvents()
            End If

            If Filename.ToLower Like WildcardString.ToLower Then
                tmpFoundFiles.Add(FilePath)
            End If
            Count += 1
        Next

        Return tmpFoundFiles
    End Function

    Private Function GetDependencySortedFiles(
        Foundfiles As IReadOnlyCollection(Of String),
        FindFilesLogger As Logger
        ) As IReadOnlyCollection(Of String)

        Dim SubLogger As Logger = FindFilesLogger.AddLogger("Dependency Sort")

        Dim DependencyDict As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
        Dim MissingFiles As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim UnreadableFiles As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim FilesWithBadLinks As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        Dim N As Integer = 0
        Dim Tot As Integer = Foundfiles.Count

        For Each Filename As String In Foundfiles

            If CheckInterruptRequest() Then
                Return CType(New List(Of String), IReadOnlyCollection(Of String))
            End If

            N += 1

            If DependencyDict.ContainsKey(Filename) Then
                Continue For
            End If

            If Not IO.File.Exists(Filename) Then
                MissingFiles.Add(Filename)
                Continue For
            End If
            FMain.TextBoxStatus.Text = $"{N}/{Tot} Dependency Sort {IO.Path.GetFileName(Filename)}"
            System.Windows.Forms.Application.DoEvents()

            Dim SSDoc As HCStructuredStorageDoc = Nothing
            Try
                SSDoc = New HCStructuredStorageDoc(Filename, _OpenReadWrite:=False)
                SSDoc.ReadLinks(FMain.LinkManagementOrder)
            Catch ex As Exception
                If SSDoc IsNot Nothing Then SSDoc.Close()
                UnreadableFiles.Add(Filename)
                Continue For
            End Try

            DependencyDict = GetLinks(SSDoc, DependencyDict, MissingFiles, UnreadableFiles, FilesWithBadLinks)

            If SSDoc IsNot Nothing Then SSDoc.Close()

        Next

        Dim OutList As List(Of String) = SortByDependency(DependencyDict)

        If CheckInterruptRequest() Then
            Return New List(Of String)
        End If

        If FilesWithBadLinks.Count > 0 Then
            Dim FilesWithBadLinksLogger As Logger = SubLogger.AddLogger("File(s) with links that could not be found or opened")
            For Each FilesWithBadLink As String In FilesWithBadLinks
                Dim Filename As String = IO.Path.GetFileName(FilesWithBadLink)
                Dim Dir As String = IO.Path.GetDirectoryName(FilesWithBadLink)
                FilesWithBadLinksLogger.AddMessage($"{Filename} in {Dir}")
            Next
        End If

        If MissingFiles.Count > 0 Then
            Dim MissingFilesLogger As Logger = SubLogger.AddLogger("File(s) that could not be found")
            For Each MissingFile As String In MissingFiles
                If Not FMain.MoveMissingToExcluded Then
                    Dim Filename As String = IO.Path.GetFileName(MissingFile)
                    Dim Dir As String = IO.Path.GetDirectoryName(MissingFile)
                    MissingFilesLogger.AddMessage($"{Filename} in {Dir}")
                Else
                    OutList.Add(MissingFile)
                End If
            Next
        End If

        If UnreadableFiles.Count > 0 Then
            Dim UnreadableFilesLogger As Logger = SubLogger.AddLogger("File(s) that could not be opened")
            For Each UnreadableFile As String In UnreadableFiles
                Dim Filename As String = IO.Path.GetFileName(UnreadableFile)
                Dim Dir As String = IO.Path.GetDirectoryName(UnreadableFile)
                UnreadableFilesLogger.AddMessage($"{Filename} in {Dir}")
            Next
        End If

        Return OutList

    End Function

    Private Function GetLinks(
        SSDoc As HCStructuredStorageDoc,
        DependencyDict As Dictionary(Of String, List(Of String)),
        MissingFiles As HashSet(Of String),
        UnreadableFiles As HashSet(Of String),
        FilesWithBadLinks As HashSet(Of String)
        ) As Dictionary(Of String, List(Of String))

        If CheckInterruptRequest() Then
            Return New Dictionary(Of String, List(Of String))
        End If

        Dim SSDocName As String
        Dim ValidExtensions As New List(Of String)({".par", ".psm", ".asm", ".dft"})

        Dim SSLinkNames As List(Of String)
        Dim SSLinkDoc As HCStructuredStorageDoc = Nothing

        Dim UC As New UtilsCommon

        SSDocName = SSDoc.FullName

        If Not DependencyDict.ContainsKey(SSDocName) Then

            DependencyDict.Add(SSDocName, New List(Of String))

            If Not SSDoc.IsFOPMaster Then
                SSLinkNames = SSDoc.GetLinkNames
                If SSLinkNames.Count > 0 Then
                    For Each SSLinkName As String In SSLinkNames

                        If ValidExtensions.Contains(IO.Path.GetExtension(SSLinkName).ToLower) Then

                            If DependencyDict.ContainsKey(SSLinkName) Then
                                If Not DependencyDict(SSDocName).Contains(SSLinkName) Then DependencyDict(SSDocName).Add(SSLinkName)
                            ElseIf Not (MissingFiles.Contains(SSLinkName) OrElse UnreadableFiles.Contains(SSLinkName)) Then
                                If IO.File.Exists(SSLinkName) Then
                                    Try
                                        SSLinkDoc = New HCStructuredStorageDoc(SSLinkName, _OpenReadWrite:=False)
                                        SSLinkDoc.ReadLinks(FMain.LinkManagementOrder)
                                        If Not DependencyDict(SSDocName).Contains(SSLinkName) Then DependencyDict(SSDocName).Add(SSLinkName)
                                        DependencyDict = GetLinks(SSLinkDoc, DependencyDict, MissingFiles, UnreadableFiles, FilesWithBadLinks)
                                        SSLinkDoc.Close()
                                    Catch ex As Exception
                                        UnreadableFiles.Add(SSLinkName)
                                        FilesWithBadLinks.Add(SSDocName)
                                        If SSLinkDoc IsNot Nothing Then SSLinkDoc.Close()
                                    End Try
                                Else
                                    MissingFiles.Add(SSLinkName)
                                    FilesWithBadLinks.Add(SSDocName)
                                End If
                            Else
                                FilesWithBadLinks.Add(SSDocName)
                            End If

                        End If
                    Next
                End If

            End If
        End If

        Return DependencyDict
    End Function

    Private Function SortByDependency(
        OriginalLinkDict As Dictionary(Of String, List(Of String))
        ) As List(Of String)

        Dim LinkDict As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
        Dim Outlist As New List(Of String)
        Dim OutHashset As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
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
                OutHashset.Add(Filename)
                NoDependencies.Add(Filename)
            End If
        Next

        ' Remove files with no dependencies from the copied dictionary
        For Each Filename In Outlist
            If LinkDict.ContainsKey(Filename) Then
                LinkDict.Remove(Filename)
            End If
        Next

        While OldCount > Count

            If CheckInterruptRequest() Then
                Return New List(Of String)
            End If

            For Each Filename In LinkDict.Keys
                FMain.TextBoxStatus.Text = IO.Path.GetFileName(Filename)
                If LinkDict(Filename).Count = 0 Then
                    Outlist.Add(Filename)
                    OutHashset.Add(Filename)
                    Continue For
                Else
                    HasUnmetDependencies = False
                    For Each LinkFilename As String In LinkDict(Filename)
                        If Not OutHashset.Contains(LinkFilename) Then
                            HasUnmetDependencies = True
                        End If
                    Next
                    If Not HasUnmetDependencies Then
                        Outlist.Add(Filename)
                        OutHashset.Add(Filename)
                    End If
                End If
            Next

            OldCount = LinkDict.Count

            For Each Filename In Outlist
                If LinkDict.ContainsKey(Filename) Then
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
                    Extension = IO.Path.GetExtension(Filename).ToLower
                    If Not ExtensionList.Contains(Extension) Then
                        ExtensionList.Add(Extension)
                    End If
                Next

                ' Remove one type of file starting with .par, then .psm, .asm, .dft in that order
                For Each ValidExtension In ValidExtensions
                    If ExtensionList.Contains(ValidExtension) Then
                        For Each Filename In RemainingFileList
                            If IO.Path.GetExtension(Filename).ToLower = ValidExtension Then
                                Outlist.Add(Filename)
                                OutHashset.Add(Filename)
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
                If OutHashset.Contains(Filename) Then
                    Outlist.Remove(Filename)
                    OutHashset.Remove(Filename)
                End If
            Next

            Count = 0
            OldCount = 100
            While OldCount > Count

                If CheckInterruptRequest() Then
                    Return New List(Of String)
                End If

                OldCount = Outlist.Count
                For Each Filename In OriginalLinkDict.Keys
                    If IO.Path.GetExtension(Filename).ToLower = ".asm" Then
                        NoRemainingLinks = True
                        For Each LinkFilename As String In OriginalLinkDict(Filename)
                            If OutHashset.Contains(LinkFilename) Then
                                NoRemainingLinks = False
                            End If
                        Next
                        If NoRemainingLinks Then
                            Outlist.Remove(Filename)
                            OutHashset.Remove(Filename)
                        End If
                    End If
                Next
                Count = Outlist.Count
            End While

            For Each Filename In OriginalLinkDict.Keys

                If CheckInterruptRequest() Then
                    Return New List(Of String)
                End If

                If IO.Path.GetExtension(Filename).ToLower = ".dft" Then
                    NoRemainingLinks = True
                    For Each LinkFilename As String In OriginalLinkDict(Filename)
                        If OutHashset.Contains(LinkFilename) Then
                            NoRemainingLinks = False
                        End If
                    Next
                    If NoRemainingLinks Then
                        Outlist.Remove(Filename)
                        OutHashset.Remove(Filename)
                    End If
                End If
            Next

        End If

        If Count > 0 Then
            For Each Filename In LinkDict.Keys
                Outlist.Add(Filename)
                OutHashset.Add(Filename)
            Next
        End If

        Return Outlist

    End Function

    Private Function SortRandomSample(
        FoundFiles As IReadOnlyCollection(Of String),
        FractionString As String,
        SubLogger As Logger
        ) As IReadOnlyCollection(Of String)

        ' https://stackoverflow.com/questions/29358857/shuffling-an-array-of-strings-in-vb-net

        Dim ShuffleList As New List(Of String)
        Dim OutList As New List(Of String)

        ShuffleList.AddRange(FoundFiles)

        Dim Fraction As Double = 0.1

        If Not Double.TryParse(FractionString,
            System.Globalization.NumberStyles.Float,
            System.Globalization.CultureInfo.InvariantCulture,
            Fraction) Then

            Fraction = 0.1
            FMain.SortRandomSampleFraction = Fraction.ToString(System.Globalization.CultureInfo.InvariantCulture)
            SubLogger.AddMessage($"Could not parse random sample sort fraction '{FractionString}'.  It was reset to 0.1.")
        Else
            If Not (Fraction > 0 And Fraction <= 1) Then
                Fraction = 0.1
                FMain.SortRandomSampleFraction = Fraction.ToString(System.Globalization.CultureInfo.InvariantCulture)
                SubLogger.AddMessage($"Random sample sort fraction out of range '{FractionString}'.  It was reset to 0.1.")
            End If

        End If

        ' Shuffle all
        Dim rnd As New Random()
        For n As Int32 = ShuffleList.Count - 1 To 0 Step -1
            Dim j = rnd.Next(0, n + 1)
            ' Swap them.
            Dim tmp = ShuffleList(n)
            ShuffleList(n) = ShuffleList(j)
            ShuffleList(j) = tmp
        Next n

        ' Extract fraction
        Dim NumberToReturn = CInt(Fraction * ShuffleList.Count)

        If Not ShuffleList.Count = 0 AndAlso NumberToReturn = 0 Then
            SubLogger.AddMessage($"No files found.  {Fraction} of {ShuffleList.Count} = 0")
        Else
            For i As Integer = 0 To NumberToReturn - 1
                OutList.Add(ShuffleList(i))
            Next
        End If

        Return OutList
    End Function

    Private Function SortAlphabetical(FoundFiles As IReadOnlyCollection(Of String)) As IReadOnlyCollection(Of String)

        Dim UC As New UtilsCommon
        Return FoundFiles.OrderBy(Function(f) UC.GetFileName(f), StringComparer.OrdinalIgnoreCase).ToList

    End Function


    ' ###### LISTVIEW ######

    Private Sub PopulateListView(FoundFiles As IReadOnlyCollection(Of String))

        Dim UC As New UtilsCommon
        Dim tf As Boolean
        Dim NumProcessed As Integer = 0
        Dim PopulatePropertyColumns As Boolean = True

        ' ###### User input for displaying file properties for a large number of files.

        If (Me.ListOfColumns.Count > 2) And (FoundFiles.Count > 1000) Then

            Dim HasColumnsToProcess As Boolean = False
            For Each PropColumn In Me.ListOfColumns
                If PropColumn.Name <> "Name" And PropColumn.Name <> "Path" Then
                    If PropColumn.Visible Then
                        HasColumnsToProcess = True
                        Exit For
                    End If
                End If
            Next

            If HasColumnsToProcess Then
                Dim s As String = $"Getting file properties on {FoundFiles.Count} files can take some time.  "
                s = $"{s}Do you want to have them displayed anyway?"
                Dim Result As MsgBoxResult = MsgBox(s, vbYesNo)
                If Result = MsgBoxResult.No Then
                    PopulatePropertyColumns = False
                End If
            End If
        End If

        ListViewFiles.BeginUpdate()

        For Each FoundFile In FoundFiles

            FMain.TextBoxStatus.Text = $"Updating List {System.IO.Path.GetFileName(FoundFile)}"

            If NumProcessed Mod 100 = 0 Then
                System.Windows.Forms.Application.DoEvents()
                If CheckInterruptRequest() Then
                    ListViewFiles.Items.Clear()
                    ListViewFiles.EndUpdate()
                    Exit Sub
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

                tmpLVItem.Text = UC.GetFileName(FoundFile)
                tmpLVItem.UseItemStyleForSubItems = False
                tmpLVItem.SubItems.Add(UC.GetDirectoryName(FoundFile))

                tmpLVItem.ImageKey = "Error"
                tmpLVItem.Tag = UC.GetExtension(FoundFile).ToLower 'Backup gruppo
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


    ' ###### PROPERTY COLUMNS ######


    Public Sub UpdatePropertiesColumns()
        UpdatePropertiesColumns2()
    End Sub

    Private Sub CreateColumns()

        For Each PropName In Me.ListOfColumns

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

        'If (LVItem.Group.Name <> "Sources") And (Me.ListOfColumns.Count > 2) Then
        If (Me.ListOfColumns.Count > 2) Then

            Dim HasColumnsToProcess As Boolean = False
            For Each PropColumn In Me.ListOfColumns
                If PropColumn.Name <> "Name" And PropColumn.Name <> "Path" Then
                    If PropColumn.Visible Then
                        HasColumnsToProcess = True
                        Exit For
                    End If
                End If
            Next

            If HasColumnsToProcess Then
                Dim Proceed As Boolean = True
                Dim UC As New UtilsCommon

                Dim SSDoc As HCStructuredStorageDoc = Nothing
                Try
                    SSDoc = New HCStructuredStorageDoc(LVItem.Name, _OpenReadWrite:=False)
                    SSDoc.ReadProperties(FMain.PropertiesData)
                Catch ex As Exception
                    Proceed = False
                End Try

                If Not Proceed Then
                    For Each PropColumn In Me.ListOfColumns
                        LVItem.SubItems.Add("** Error **", Color.Black, Color.LightPink, LVItem.Font)
                    Next
                Else

                    Try
                        'Adding extra properties data if needed
                        For Each PropColumn In Me.ListOfColumns
                            If PropColumn.Name <> "Name" And PropColumn.Name <> "Path" Then

                                Dim PropValue As String = Nothing
                                Dim tmpColor As Color = LVItem.BackColor 'Color.White ####### Changed to reflect the file read-only backcolor

                                If PropColumn.Visible Then '###### Only a visible column need a value. F.Arfilli

                                    Dim tmpIsReadOnly As Boolean = False
                                    Try
                                        Dim PropName = UC.PropNameFromFormula(PropColumn.Formula)
                                        If Not IsReadOnly Then
                                            If Not LVItem.Group.Name = ".dft" Then  ' Draft files do not have a PartsLiteData stream
                                                ' Unable to parse PartsLiteData stream in some files.
                                                Try
                                                    tmpIsReadOnly = SSDoc.IsExposedVariable(PropName)
                                                Catch ex As Exception
                                                End Try
                                            End If
                                        End If

                                        Dim tmpErrorLogger As New Logger("tmpLogger", Nothing)
                                        PropValue = SSDoc.SubstitutePropertyFormulas(PropColumn.Formula, tmpErrorLogger)
                                    Catch ex As Exception
                                    End Try

                                    If IsNothing(PropValue) Then
                                        PropValue = ""
                                        tmpColor = Color.Gainsboro '<---------- Properties not present
                                    End If

                                    If IsReadOnly Or tmpIsReadOnly Then tmpColor = Color.LightGray

                                End If

                                LVItem.SubItems.Add(PropValue, Color.Empty, tmpColor, LVItem.Font)

                            End If

                        Next
                        Application.DoEvents()

                    Catch ex As Exception

                        For Each PropColumn In Me.ListOfColumns
                            LVItem.SubItems.Add("** Error **", Color.Black, Color.LightPink, LVItem.Font)
                        Next

                    End Try

                End If

                If SSDoc IsNot Nothing Then SSDoc.Close()

            End If

        Else
            'Eventually insert code to personalize Sources group

        End If

    End Sub



    Public Sub UpdatePropertiesColumns2()


        ListViewFiles.BeginUpdate()

        Try
            'Resetting the columns
            If ListViewFiles.Columns.Count > 2 Then
                Do Until ListViewFiles.Columns.Count = 2
                    ListViewFiles.Columns.RemoveAt(ListViewFiles.Columns.Count - 1)
                Loop
            End If

            If Not HasPropertiesToProcess() Then
                ' ListViewFiles.EndUpdate()  ' Handled in the Finally clause
                Exit Sub
            End If

            Dim ListViewFilesLUT As Dictionary(Of String, List(Of Integer)) = BuildListViewFilesLUT()

            Dim Threads As Integer
            If Environment.ProcessorCount = 1 Or Not CanRunParallel() Then
                Threads = 1
            Else
                Threads = Math.Min(8, Environment.ProcessorCount - 1)
            End If

            Dim TrySingleThreaded As Boolean = False
            If TrySingleThreaded Then Threads = 1

            If Threads = 1 Then
                Dim NumDone As Integer = 0
                For Each Filename As String In ListViewFilesLUT.Keys
                    Dim Results As Dictionary(Of String, String)
                    Results = GetFileProperties(Filename)
                    UpdateLVItem2(Results, ListViewFilesLUT)

                    NumDone += 1
                    FMain.TextBoxStatus.Text = $"Getting properties {NumDone} of {ListViewFilesLUT.Count}"
                    System.Windows.Forms.Application.DoEvents()
                    If CheckInterruptRequest() Then
                        'ListViewFiles.EndUpdate()
                        Exit For
                    End If
                Next
            Else
                ParallelProps(Threads, ListViewFilesLUT)
            End If
            'Catch ex As Exception
        Finally
            CreateColumns()
            ListViewFiles.EndUpdate()
        End Try


    End Sub

    Private Sub UpdateLVItem2(Results As Dictionary(Of String, String), ListViewFilesLUT As Dictionary(Of String, List(Of Integer)))

        Dim Filename As String = Results("Filename")

        For Each Idx As Integer In ListViewFilesLUT(Filename)
            Dim tmpLVItem As ListViewItem = ListViewFiles.Items(Idx)

            If tmpLVItem.SubItems.Count > 2 Then
                Do Until tmpLVItem.SubItems.Count = 2
                    tmpLVItem.SubItems.RemoveAt(tmpLVItem.SubItems.Count - 1)
                Loop
            End If

            tmpLVItem.UseItemStyleForSubItems = False

            If Results.Keys.Count < 2 Then  ' ResultsDict only has one key, "Filename", if Try/Catch on SSDoc fails.
                For Each PropCol In Me.ListOfColumns
                    If PropCol.Name = "Name" Or PropCol.Name = "Path" Then Continue For

                    tmpLVItem.SubItems.Add("** Error **", Color.Black, Color.LightPink, tmpLVItem.Font)
                Next
                Continue For
            End If

            For Each PropCol In Me.ListOfColumns
                Dim PropValue As String = Nothing
                Dim tmpColor As Color = tmpLVItem.BackColor

                If PropCol.Name = "Name" Or PropCol.Name = "Path" Then Continue For

                If PropCol.Visible Then

                    PropValue = Results(PropCol.Name)

                    If PropValue Is Nothing Then
                        tmpColor = Color.Gainsboro
                    End If

                    Dim IsReadOnly As Boolean = tmpLVItem.SubItems(0).BackColor = Color.LightGray

                    If IsReadOnly Or CBool(Results($"{PropCol.Name}:IsExposedVariable")) Then
                        tmpColor = Color.LightGray
                    End If
                End If

                tmpLVItem.SubItems.Add(PropValue, Color.Empty, tmpColor, tmpLVItem.Font)
            Next


        Next

    End Sub


    Private Function GetFileProperties(Filename As String) As Dictionary(Of String, String)

        Dim ResultsDict As New Dictionary(Of String, String)
        ResultsDict("Filename") = Filename

        Dim Proceed As Boolean = True

        Dim SSDoc As HCStructuredStorageDoc = Nothing
        Try
            SSDoc = New HCStructuredStorageDoc(Filename, _OpenReadWrite:=False)
            SSDoc.ReadProperties(FMain.PropertiesData)
        Catch ex As Exception
            Proceed = False
        End Try

        If Not Proceed Then
            If SSDoc IsNot Nothing Then SSDoc.Close()
            Return ResultsDict
        End If

        Dim UC As New UtilsCommon

        For Each PropCol As PropertyColumn In Me.ListOfColumns

            If (PropCol.Name = "Name" Or PropCol.Name = "Path" Or Not PropCol.Visible) Then Continue For

            Dim PropValue As String = Nothing
            Try
                Dim tmpErrorLogger As New Logger("tmpLogger", Nothing)
                PropValue = SSDoc.SubstitutePropertyFormulas(PropCol.Formula, tmpErrorLogger)
            Catch ex As Exception
            End Try

            'If PropValue IsNot Nothing Then
            '    ResultsDict(PropCol.Name) = PropValue
            'Else
            '    ResultsDict(PropCol.Name) = "Nothing"
            'End If
            ResultsDict(PropCol.Name) = PropValue

            If Not IO.Path.GetExtension(Filename).ToLower = ".dft" Then
                Try
                    ResultsDict($"{PropCol.Name}:IsExposedVariable") = CStr(SSDoc.IsExposedVariable(UC.PropNameFromFormula(PropCol.Formula)))
                Catch ex As Exception
                    ResultsDict($"{PropCol.Name}:IsExposedVariable") = "False"
                End Try
            Else
                ResultsDict($"{PropCol.Name}:IsExposedVariable") = "False"
            End If

        Next

        If SSDoc IsNot Nothing Then SSDoc.Close()

        Return ResultsDict
    End Function

    Private Function BuildListViewFilesLUT() As Dictionary(Of String, List(Of Integer))
        ' Look up table that ties filenames to a ListViewFiles item index.
        ' Need a list of indices, rather than a singleton, because unsorted lists
        ' can contain duplicate filenames.

        Dim ListViewFilesLUT As New Dictionary(Of String, List(Of Integer))

        For i As Integer = 0 To ListViewFiles.Items.Count - 1
            Dim tmpLVItem As ListViewItem = ListViewFiles.Items(i)

            If tmpLVItem.Group.Name = "Excluded" Then Continue For

            If Not ListViewFilesLUT.Keys.Contains(tmpLVItem.Name) Then
                ListViewFilesLUT(tmpLVItem.Name) = {i}.ToList
            Else
                ListViewFilesLUT(tmpLVItem.Name).Add(i)
            End If
        Next

        Return ListViewFilesLUT
    End Function

    Private Sub ParallelProps(Threads As Integer, ListViewFilesLUT As Dictionary(Of String, List(Of Integer)))

        Using CancellationTokenSource As New System.Threading.CancellationTokenSource

            Dim Options As New System.Threading.Tasks.ParallelOptions
            Options.MaxDegreeOfParallelism = Threads
            Options.CancellationToken = CancellationTokenSource.Token

            Dim Finished As New System.Collections.Concurrent.ConcurrentQueue(Of Dictionary(Of String, String))

            Dim Work As System.Threading.Tasks.Task = System.Threading.Tasks.Task.Run(
                Sub()
                    System.Threading.Tasks.Parallel.ForEach(Of String)(
                            ListViewFilesLUT.Keys,
                            Options,
                            Sub(Filename As String)
                                Finished.Enqueue(GetFileProperties(Filename))
                            End Sub)
                End Sub)

            Dim NumDone As Integer = 0
            Do
                Dim Results As Dictionary(Of String, String) = Nothing
                Do While Finished.TryDequeue(Results)
                    UpdateLVItem2(Results, ListViewFilesLUT)
                    NumDone += 1
                Loop

                FMain.TextBoxStatus.Text = $"Getting properties {NumDone} of {ListViewFilesLUT.Count}"
                System.Windows.Forms.Application.DoEvents()
                If CheckInterruptRequest() Then
                    CancellationTokenSource.Cancel()
                End If

                If Work.IsCompleted AndAlso Finished.IsEmpty Then Exit Do
                System.Threading.Thread.Sleep(50)
            Loop

            Try
                Work.Wait()
            Catch ex As System.AggregateException When TypeOf ex.InnerException Is System.OperationCanceledException

            End Try

        End Using


    End Sub

    Private Function CanRunParallel() As Boolean
        Dim Proceed As Boolean = True

        Dim UC As New UtilsCommon

        For Each PropCol As PropertyColumn In Me.ListOfColumns

            If Not PropCol.Visible Then Continue For

            If PropCol.Name <> "Name" And PropCol.Name <> "Path" Then
                If UC.PropSetFromFormula(PropCol.Formula).ToLower = "server" Then
                    Proceed = False
                    Exit For
                End If
                If Not UC.ModelIdxFromFormula(PropCol.Formula) = 0 Then
                    Proceed = False
                    Exit For
                End If
            End If

        Next

        Return Proceed
    End Function

    Private Function HasPropertiesToProcess() As Boolean
        Dim Proceed As Boolean = False

        For Each PropCol As PropertyColumn In Me.ListOfColumns

            If Not PropCol.Visible Then Continue For

            If Not PropCol.Name = "Name" And Not PropCol.Name = "Path" Then
                Proceed = True
                Exit For
            End If
        Next

        Return Proceed
    End Function


    Private Function CheckInterruptRequest() As Boolean
        Dim tf As Boolean = False

        System.Windows.Forms.Application.DoEvents()
        If FMain.StopProcess Then
            FMain.TextBoxStatus.Text = "Processing stopped by user"
            tf = True
        End If

        Return tf
    End Function


End Class
