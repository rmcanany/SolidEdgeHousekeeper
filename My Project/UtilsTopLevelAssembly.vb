Option Strict On

Public Class UtilsTopLevelAssembly
    Public Property FMain As Form_Main
    Public Property ErrorLogger As Logger

    Private Property RootReachables As Dictionary(Of String, Boolean)
    Private Property TopLevelFolders As List(Of String)


    Public Sub New(_Form_Main As Form_Main, _ErrorLogger As Logger)
        Me.FMain = _Form_Main
        Me.ErrorLogger = _ErrorLogger

        Me.RootReachables = New Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)
    End Sub


    Public Function GetLinksTopDown(
         TopLevelFolders As List(Of String),
         TopLevelAssembly As String,
         ActiveFileExtensionsList As List(Of String),
         Report As Boolean
         ) As List(Of String)

        Dim FoundFiles As New List(Of String)

        TopLevelFolders = RemoveNestedFolders(TopLevelFolders)
        Me.TopLevelFolders = TopLevelFolders

        Dim AllFilenames As List(Of String)
        Dim FileLinksContainer As FileLinksContainer
        Dim UnrelatedFiles As New List(Of String)

        '###### Get filenames in top level folders ######
        AllFilenames = GetAllFilesTopDown(TopLevelFolders, TopLevelAssembly)
        If CheckInterruptRequest() Then Return New List(Of String)

        '###### Get links contained in each file ######
        FileLinksContainer = GetContains(AllFilenames)
        If CheckInterruptRequest() Then Return New List(Of String)

        '###### Populate ContainedBy information ######
        FileLinksContainer = GetContainedBy(FileLinksContainer)
        If CheckInterruptRequest() Then Return New List(Of String)

        '###### Find files related to the top level assembly ######
        FileLinksContainer = VisitLinks(TopLevelAssembly, FileLinksContainer)
        If CheckInterruptRequest() Then Return New List(Of String)

        '###### Prep the TLA file list ######
        For Each FileLinks As FileLinks In FileLinksContainer.GetItems
            Dim Extension As String = IO.Path.GetExtension(FileLinks.Name).ToLower.Replace(".", "*.")
            If ActiveFileExtensionsList.Contains(Extension) Then
                If FileLinks.Visited Then
                    FoundFiles.Add(FileLinks.Name)
                Else
                    UnrelatedFiles.Add(FileLinks.Name)
                End If
            End If
        Next

        '###### Check for unprocessed files ######
        Dim Total As Integer = FileLinksContainer.UnprocessedFilenames.Count
        If Total > 0 Then
            Dim UnprocessedFileLogger = Me.ErrorLogger.AddLogger("Unable to process these files (or any of their links)")
            UnprocessedFileLogger.AddMessage("(See 'Select by Top Level Assembly' > 'Some Common Issues' in the Help)")
            Dim MaxToReport As Integer = 10
            Dim MaxStragglersToAllow As Integer = 5
            Dim Count As Integer = 0

            For Each FilePath As String In FileLinksContainer.UnprocessedFilenames.OrderBy(Function(f) f)
                If Total > MaxToReport + MaxStragglersToAllow Then
                    If Count = MaxToReport Then
                        UnprocessedFileLogger.AddMessage($"(and {Total - Count} more)")
                        Exit For
                    End If
                End If
                Dim SlashIdx As Integer = FilePath.LastIndexOf("\"c)
                Dim Dir As String = FilePath.Substring(0, SlashIdx + 1)
                Dim Filename As String = FilePath.Substring(SlashIdx + 1)
                UnprocessedFileLogger.AddMessage($"{Filename} in {Dir}")
                Count += 1
            Next
        End If

        '###### Report unrelated files if required ######
        If Report Then
            ReportUnrelatedFiles(UnrelatedFiles)
        End If

        Return FoundFiles

    End Function

    Private Function GetAllFilesTopDown(
        TopLevelFolders As List(Of String),
        TopLevelAssembly As String
        ) As List(Of String)

        Dim AllFilenames As New List(Of String)
        Dim ActiveFileExtensionsList As List(Of String) = "*.asm *.par *.psm *.dft".Split(CChar(" ")).ToList

        AllFilenames.Add(TopLevelAssembly)

        For Each TopLevelFolder As String In TopLevelFolders
            GetFilesAndRecurseSubs(TopLevelFolder, ActiveFileExtensionsList, AllFilenames)
        Next

        Return AllFilenames
    End Function

    Private Sub GetFilesAndRecurseSubs(
        Path As String,
        ValidExtensions As List(Of String),
        ByRef AllFilenames As List(Of String))

        If CheckInterruptRequest() Then Return

        Try
            '###### COLLECT ELIGIBLE FILES IN THIS DIRECTORY ######
            Dim tmpFiles = FileIO.FileSystem.GetFiles(Path, FileIO.SearchOption.SearchTopLevelOnly, ValidExtensions.ToArray)
            For Each tmpFile As String In tmpFiles
                If Not AllFilenames.Contains(tmpFile, StringComparer.OrdinalIgnoreCase) Then
                    FMain.TextBoxStatus.Text = $"Getting files {IO.Path.GetFileName(tmpFile)}"
                    AllFilenames.Add(tmpFile)
                End If
            Next

            '###### FOLLOW ANY DIRECTORIES ######
            Dim tmpPaths = FileIO.FileSystem.GetDirectories(Path, FileIO.SearchOption.SearchTopLevelOnly)
            For Each tmpPath As String In tmpPaths
                GetFilesAndRecurseSubs(tmpPath, ValidExtensions, AllFilenames)
            Next

        Catch ex As UnauthorizedAccessException
            'Access Denied exception.  Assuming SE also would be denied access.

        Catch ex1 As Exception
            Me.ErrorLogger.AddMessage($"Could not process '{Path}'")

        End Try

    End Sub

    Public Function GetContains(AllFilenames As List(Of String)) As FileLinksContainer

        Dim FileLinksContainer As New FileLinksContainer

        Dim ParentExtensions As List(Of String) = {".par", ".psm", ".asm", ".dft"}.ToList

        For Each ParentExtension As String In ParentExtensions
            For Each Filename As String In AllFilenames

                If Not IO.Path.GetExtension(Filename).ToLower = ParentExtension Then
                    Continue For
                End If

                If CheckInterruptRequest() Then Return FileLinksContainer

                FMain.TextBoxStatus.Text = $"Getting links {IO.Path.GetFileName(Filename)}"

                Dim SSDoc As HCStructuredStorageDoc = Nothing
                Dim SSLinkDoc As HCStructuredStorageDoc = Nothing

                Try
                    Dim FileLinks As New FileLinks(Filename)
                    Dim LinkNames As List(Of String)

                    SSDoc = New HCStructuredStorageDoc(Filename, _OpenReadWrite:=False)
                    SSDoc.ReadLinks(FMain.LinkManagementOrder)
                    LinkNames = SSDoc.GetLinkNames

                    FileLinks.IsFOPMaster = SSDoc.IsFOPMaster

                    For Each LinkName As String In LinkNames


                        ' ###### CHECK BAILOUT CONDITIONS ######

                        ' Known bad
                        If FileLinksContainer.UnprocessedFilenames.Contains(LinkName) Then
                            Continue For
                        End If

                        ' Already processed
                        Dim LinkFileLinks As FileLinks = FileLinksContainer.GetItem(LinkName)

                        If LinkFileLinks IsNot Nothing Then
                            If Not LinkFileLinks.IsFOPMaster Then

                                If FMain.TLAIncludePartCopies Then
                                    FileLinks.Contains.Add(LinkName)

                                Else
                                    Dim Extension As String = IO.Path.GetExtension(Filename).ToLower
                                    If Not ((Extension = ".par") Or (Extension = ".psm")) Then
                                        FileLinks.Contains.Add(LinkName)
                                    End If
                                End If

                            End If
                            Continue For
                        End If

                        ' Unreachable path
                        If LinkName.Length > 260 OrElse Not IsRootReachable(LinkName, RootReachables) Then
                            FileLinksContainer.UnprocessedFilenames.Add(LinkName)
                            Continue For
                        End If

                        ' Outside top level folders
                        If Not FileInTopLevelFolders(LinkName) Then
                            FileLinksContainer.UnprocessedFilenames.Add(LinkName)
                            Continue For
                        End If

                        ' ###### PROCESS LINK FILE ######

                        SSLinkDoc = Nothing

                        Try
                            Try
                                SSLinkDoc = New HCStructuredStorageDoc(LinkName, _OpenReadWrite:=False)
                            Catch ex As Exception
                                If SSLinkDoc IsNot Nothing Then SSLinkDoc.Close()
                                FileLinksContainer.UnprocessedFilenames.Add(LinkName)
                            End Try

                            If (SSLinkDoc IsNot Nothing) AndAlso (Not SSLinkDoc.IsFOPMaster) Then

                                If FMain.TLAIncludePartCopies Then
                                    FileLinks.Contains.Add(LinkName)

                                Else
                                    Dim Extension As String = IO.Path.GetExtension(Filename).ToLower
                                    If Not ((Extension = ".par") Or (Extension = ".psm")) Then
                                        FileLinks.Contains.Add(LinkName)
                                    End If
                                End If

                            End If

                            If SSLinkDoc IsNot Nothing Then SSLinkDoc.Close()

                        Catch ex As Exception
                            If SSLinkDoc IsNot Nothing Then SSLinkDoc.Close()
                            FileLinksContainer.UnprocessedFilenames.Add(LinkName)
                        End Try

                    Next

                    SSDoc.Close()

                    FileLinks.Contains = FileLinks.Contains.Distinct.ToList

                    FileLinksContainer.AddItem(FileLinks)

                Catch ex2 As Exception
                    If SSLinkDoc IsNot Nothing Then SSLinkDoc.Close()
                    If SSDoc IsNot Nothing Then SSDoc.Close()
                    FileLinksContainer.UnprocessedFilenames.Add(Filename)
                End Try
            Next
        Next

        Return FileLinksContainer
    End Function

    Private Function GetContainedBy(FileLinksContainer As FileLinksContainer) As FileLinksContainer

        For Each FileLinks As FileLinks In FileLinksContainer.GetItems

            If CheckInterruptRequest() Then Return FileLinksContainer

            FMain.TextBoxStatus.Text = $"Contained by {IO.Path.GetFileName(FileLinks.Name)}"

            For Each ContainsFilename In FileLinks.Contains
                Dim tmpFileLinks As FileLinks = FileLinksContainer.GetItem(ContainsFilename)
                If tmpFileLinks IsNot Nothing Then
                    If Not tmpFileLinks.IsFOPMaster Then
                        tmpFileLinks.ContainedBy.Add(FileLinks.Name)
                    End If
                End If
            Next
        Next

        Return FileLinksContainer
    End Function

    Private Function VisitLinks(
        Filename As String,
        FileLinksContainer As FileLinksContainer
        ) As FileLinksContainer

        If CheckInterruptRequest() Then Return FileLinksContainer

        Dim FileLinks As FileLinks

        Dim LinkedFilename As String
        Dim ContainedByFilename As String
        Dim Extension As String

        FMain.TextBoxStatus.Text = $"Visit links {IO.Path.GetFileName(Filename)}"
        Windows.Forms.Application.DoEvents()

        FileLinks = FileLinksContainer.GetItem(Filename)
        If FileLinks IsNot Nothing Then
            FileLinks.Visited = True

            For Each ContainedByFilename In FileLinks.ContainedBy
                Extension = IO.Path.GetExtension(ContainedByFilename).ToLower
                If Extension = ".dft" Then
                    Dim tmpFileLinks As FileLinks = FileLinksContainer.GetItem(ContainedByFilename)
                    If tmpFileLinks IsNot Nothing Then tmpFileLinks.Visited = True
                    FileLinksContainer = VisitLinks(ContainedByFilename, FileLinksContainer)
                End If
            Next

            For Each LinkedFilename In FileLinks.Contains
                Dim tmpFileLinks As FileLinks = FileLinksContainer.GetItem(LinkedFilename)
                If tmpFileLinks IsNot Nothing Then
                    If Not tmpFileLinks.Visited Then
                        tmpFileLinks.Visited = True
                        For Each ContainedByFilename In tmpFileLinks.ContainedBy
                            Extension = IO.Path.GetExtension(ContainedByFilename).ToLower
                            If Extension = ".dft" Then
                                Dim tmptmpFileLinks As FileLinks = FileLinksContainer.GetItem(ContainedByFilename)
                                If tmptmpFileLinks IsNot Nothing Then tmptmpFileLinks.Visited = True
                                FileLinksContainer = VisitLinks(ContainedByFilename, FileLinksContainer)
                            End If
                        Next
                        Extension = IO.Path.GetExtension(LinkedFilename).ToLower
                        If Extension = ".asm" Then
                            FileLinksContainer = VisitLinks(LinkedFilename, FileLinksContainer)
                        ElseIf FMain.TLAIncludePartCopies And (Extension = ".par" Or Extension = ".psm") Then
                            FileLinksContainer = VisitLinks(LinkedFilename, FileLinksContainer)
                        End If
                    End If
                End If
            Next

        End If


        Return FileLinksContainer
    End Function



    Public Function GetLinksBottomUp(
        TopLevelFolders As List(Of String),
        TopLevelAssembly As String,
        ActiveFileExtensionsList As List(Of String),
        DraftAndModelSameName As Boolean,
        Report As Boolean
        ) As List(Of String)

        ' ###### Changed all references from DesignManager to RevisionManager RM 20250115 ######
        ' Code with DesignManager references is commented out above.
        ' Affected Functions: GetLinksBottomUp, FollowLinksBottomUp, GetWhereUsedBottomUp.
        ' Did not change variable names, eg DMApp is now a RevisionManager.Application

        Dim AllLinkedFilenames As New List(Of String)
        Dim tmpAllLinkedFilenames As New List(Of String)
        Dim FoundFiles As New List(Of String)
        Dim FileExtension As String
        Dim AllFilenames As New Dictionary(Of String, String)
        Dim tf As Boolean
        Dim IndexedDrives As New List(Of String)
        Dim IsDriveIndexed As Boolean = False

        Dim TopLevelFolder As String
        Dim tmpAllLinkedFilename As String

        FMain.TextBoxStatus.Text = $"Opening {System.IO.Path.GetFileName(TopLevelAssembly)}"

        TopLevelFolders = RemoveNestedFolders(TopLevelFolders)

        Dim DMApp As RevisionManager.Application = Nothing
        Dim Proceed As Boolean = True

        Try
            DMApp = New RevisionManager.Application
            Dim TLADoc As RevisionManager.Document

            DMApp.Visible = 1
            DMApp.DisplayAlerts = 0
            FMain.Activate()

            TLADoc = CType(DMApp.OpenFileInRevisionManager(TopLevelAssembly), RevisionManager.Document)

            If TLADoc Is Nothing OrElse TLADoc.LinkedDocuments Is Nothing Then
                Proceed = False
                Me.ErrorLogger.AddMessage($"Revision Manager unable to open '{IO.Path.GetFileName(TopLevelAssembly)}'")
            Else
                If TopLevelFolders.Count > 0 Then
                    IndexedDrives = GetIndexedDrives()

                    For Each TopLevelFolder In TopLevelFolders

                        IsDriveIndexed = False
                        If IndexedDrives.Count > 0 Then
                            For Each IndexedDrive In IndexedDrives
                                If TopLevelFolder.ToLower().StartsWith(IndexedDrive.ToLower()) Then
                                    IsDriveIndexed = True
                                    Exit For
                                End If
                            Next
                        End If

                        tmpAllLinkedFilenames = FollowLinksBottomUp(DMApp, TLADoc, AllLinkedFilenames,
                                                 TopLevelFolder, AllFilenames, IsDriveIndexed, DraftAndModelSameName)

                        If CheckInterruptRequest() Then Return New List(Of String)

                        For Each tmpAllLinkedFilename In tmpAllLinkedFilenames
                            If Not AllLinkedFilenames.Contains(tmpAllLinkedFilename, StringComparer.OrdinalIgnoreCase) Then
                                AllLinkedFilenames.Add(tmpAllLinkedFilename)
                            End If
                        Next

                    Next
                Else
                    ' Bare top level assy.  Call FollowLinksBottomUp with TopLevelFolder = ""
                    ' Since there are no folders to process, indexed drives are not relevant.

                    tmpAllLinkedFilenames = FollowLinksBottomUp(DMApp, TLADoc, AllLinkedFilenames,
                                                 "", AllFilenames, IsDriveIndexed:=False, DraftAndModelSameName)

                    If CheckInterruptRequest() Then Return New List(Of String)

                    For Each tmpAllLinkedFilename In tmpAllLinkedFilenames
                        If Not AllLinkedFilenames.Contains(tmpAllLinkedFilename, StringComparer.OrdinalIgnoreCase) Then
                            AllLinkedFilenames.Add(tmpAllLinkedFilename)
                        End If
                    Next

                End If
            End If

        Catch ex As Exception
            Proceed = False
            Me.ErrorLogger.AddMessage($"Unable to read all files linked to '{IO.Path.GetFileName(TopLevelAssembly)}': ({ex.Message})")
        Finally
            Try
                If DMApp IsNot Nothing Then DMApp.Quit()
            Catch ex As Exception
            End Try
        End Try

        For Each Filename In AllLinkedFilenames
            FileExtension = System.IO.Path.GetExtension(Filename).ToLower.Replace(".", "*.")
            tf = ActiveFileExtensionsList.Contains(FileExtension)
            tf = tf And (Not FoundFiles.Contains(Filename, StringComparer.OrdinalIgnoreCase))
            If tf Then
                FoundFiles.Add(Filename)
            End If
        Next

        If Proceed And Report Then
            GetUnrelatedFilesBottomUp(TopLevelFolders, FoundFiles, ActiveFileExtensionsList)

            If CheckInterruptRequest() Then Return New List(Of String)
        End If

        Return FoundFiles

    End Function

    Private Function FollowLinksBottomUp(
        DMApp As RevisionManager.Application,
        DMDoc As RevisionManager.Document,
        AllLinkedFilenames As List(Of String),
        TopLevelFolder As String,
        AllFilenames As Dictionary(Of String, String),
        IsDriveIndexed As Boolean,
        DraftAndModelSameName As Boolean
        ) As List(Of String)

        Dim LinkedDocs As RevisionManager.LinkedDocuments
        Dim LinkedDoc As RevisionManager.Document
        Dim LinkedDocName As String
        Dim ValidExtensions As New List(Of String)
        Dim Extension As String
        Dim WhereUsedFiles As New List(Of String)
        Dim WhereUsedFile As String
        Dim tf As Boolean

        Dim Filename As String

        Dim WhereUsedDoc As RevisionManager.Document
        Dim WhereUsedLinkedDocs As RevisionManager.LinkedDocuments
        Dim WhereUsedLinkedDoc As RevisionManager.Document

        If CheckInterruptRequest() Then Return AllLinkedFilenames

        ValidExtensions.AddRange({".asm", ".par", ".psm", ".dft"})

        Filename = DMDoc.FullName

        Dim UC As New UtilsCommon

        Filename = UC.GetFOAFilename(Filename)

        If FileIO.FileSystem.FileExists(Filename) Then
            tf = Not AllLinkedFilenames.Contains(Filename, StringComparer.OrdinalIgnoreCase)
            If tf Then
                AllLinkedFilenames.Add(Filename)

                UpdateStatus("Follow Links", Filename)

                ' In case of corrupted file or other problem
                Try

                    ' Get any draft files containing this file.
                    WhereUsedFiles = GetWhereUsedBottomUp(DMApp, TopLevelFolder, Filename, IsDriveIndexed, DraftAndModelSameName)

                    For Each WhereUsedFile In WhereUsedFiles

                        If CheckInterruptRequest() Then Return AllLinkedFilenames

                        If Not AllLinkedFilenames.Contains(WhereUsedFile, StringComparer.OrdinalIgnoreCase) Then
                            AllLinkedFilenames.Add(WhereUsedFile)

                            Extension = IO.Path.GetExtension(WhereUsedFile).ToLower
                            If Extension = ".dft" And IO.File.Exists(WhereUsedFile) Then
                                Try
                                    WhereUsedDoc = CType(DMApp.OpenFileInRevisionManager(WhereUsedFile), RevisionManager.Document)
                                    WhereUsedLinkedDocs = CType(WhereUsedDoc.LinkedDocuments, RevisionManager.LinkedDocuments)
                                    For Each WhereUsedLinkedDoc In WhereUsedLinkedDocs
                                        AllLinkedFilenames = FollowLinksBottomUp(DMApp, WhereUsedLinkedDoc, AllLinkedFilenames,
                                                                             TopLevelFolder, AllFilenames, IsDriveIndexed, DraftAndModelSameName)
                                    Next
                                    WhereUsedDoc.Close()
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    Next

                    If FMain.TLAIncludePartCopies Then
                        tf = True
                    Else
                        tf = System.IO.Path.GetExtension(Filename).ToLower = ".asm"
                        tf = tf Or System.IO.Path.GetExtension(Filename).ToLower = ".dft"
                    End If

                    ' Follow links contained by this file, if any.
                    If tf Then
                        LinkedDocs = CType(DMDoc.LinkedDocuments, RevisionManager.LinkedDocuments)
                        If LinkedDocs.Count > 0 Then
                            For Each LinkedDoc In LinkedDocs

                                If CheckInterruptRequest() Then Return AllLinkedFilenames

                                ' Get FOP status
                                Dim FOPStatus As Integer
                                LinkedDoc.IsDocumentFOP(FOPStatus)

                                ' FOP Masters can have links to many unrelated files.  For example, a fastener.  
                                ' Do not include them, or follow their links.
                                If Not (FOPStatus = RevisionManager.DocFOPStatus.FOPMasterDocument) Then
                                    LinkedDocName = LinkedDoc.FullName

                                    LinkedDocName = UC.GetFOAFilename(LinkedDocName)

                                    Extension = IO.Path.GetExtension(LinkedDocName).ToLower

                                    If ValidExtensions.Contains(Extension) Then
                                        AllLinkedFilenames = FollowLinksBottomUp(DMApp, LinkedDoc, AllLinkedFilenames,
                                                                             TopLevelFolder, AllFilenames, IsDriveIndexed, DraftAndModelSameName)
                                    End If

                                End If
                            Next

                        End If

                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        Return AllLinkedFilenames

    End Function

    Private Function GetWhereUsedBottomUp(
        DMApp As RevisionManager.Application,
        TopLevelFolder As String,
        Filename As String,
        IsDriveIndexed As Boolean,
        DraftAndModelSameName As Boolean
        ) As List(Of String)

        Dim AllWhereUsedFileNames As New List(Of String)

        UpdateStatus("Where Used", Filename)

        If TopLevelFolder = "" Then
            Return AllWhereUsedFileNames
        End If

        If CheckInterruptRequest() Then Return AllWhereUsedFileNames

        If DraftAndModelSameName Then
            Dim DraftFilename As String = System.IO.Path.ChangeExtension(Filename, ".dft")
            If IO.File.Exists(DraftFilename) Then
                AllWhereUsedFileNames.Add(DraftFilename)
            End If
            Return AllWhereUsedFileNames
        End If

        Dim WhereUsedDocuments As New List(Of RevisionManager.Document)

        If Not IO.Path.GetExtension(Filename).ToLower = ".dft" Then  ' Draft files are not "Used" anywhere.
            If Not IsDriveIndexed Then
                'This "resets" DMApp.FindWhereUsed().  Somehow.
                DMApp.WhereUsedCriteria(Nothing, True) = TopLevelFolder

                'Finds the first WhereUsed Document, if any.
                Dim WhereUsedDocument As RevisionManager.Document
                WhereUsedDocument = CType(DMApp.FindWhereUsed(FileIO.FileSystem.GetFileInfo(Filename)), RevisionManager.Document)

                While Not WhereUsedDocument Is Nothing
                    If Not AllWhereUsedFileNames.Contains(WhereUsedDocument.FullName, StringComparer.OrdinalIgnoreCase) Then
                        ' For bottom_up search, the only applicable where used results are draft files
                        If IO.Path.GetExtension(WhereUsedDocument.FullName).ToLower = ".dft" Then
                            AllWhereUsedFileNames.Add(WhereUsedDocument.FullName)
                        End If
                    End If
                    'Finds the next WhereUsed document, if any.
                    WhereUsedDocument = CType(DMApp.FindWhereUsed(), RevisionManager.Document)
                End While

            Else  'It is indexed
                Dim arrDocUsed As Object = Nothing

                Try
                    DMApp.WhereUsedCriteria(Nothing, True) = TopLevelFolder

                    DMApp.FindWhereUsedDocuments(FileIO.FileSystem.GetFileInfo(Filename), arrDocUsed)

                    If arrDocUsed IsNot Nothing Then
                        For Each item As String In DirectCast(arrDocUsed, Array)
                            If Not AllWhereUsedFileNames.Contains(item, StringComparer.OrdinalIgnoreCase) Then
                                ' For bottom_up search, the only applicable where used results are draft files
                                If IO.Path.GetExtension(item).ToLower = ".dft" Then
                                    AllWhereUsedFileNames.Add(item)
                                End If
                            End If
                        Next
                    End If

                Catch ex As Exception
                    Me.ErrorLogger.AddMessage($"Unable to process where used for '{IO.Path.GetFileName(Filename)}': {ex.Message}")
                End Try

            End If

        End If

        Return AllWhereUsedFileNames

    End Function

    Private Sub GetUnrelatedFilesBottomUp(
        TopLevelFolders As List(Of String),
        Foundfiles As List(Of String),
        ActiveFileExtensionsList As List(Of String))

        Dim UnrelatedFiles As New List(Of String)

        If TopLevelFolders.Count = 0 Then
            Exit Sub
        End If

        For Each TopLevelFolder In TopLevelFolders
            Dim SomeFiles As New List(Of String)
            GetFilesAndRecurseSubs(TopLevelFolder, ActiveFileExtensionsList, SomeFiles)

            For Each Filename In SomeFiles
                If Not Foundfiles.Contains(Filename, StringComparer.OrdinalIgnoreCase) Then
                    If Not UnrelatedFiles.Contains(Filename, StringComparer.OrdinalIgnoreCase) Then
                        UnrelatedFiles.Add(Filename)
                    End If
                End If
            Next

        Next

        If CheckInterruptRequest() Then Exit Sub

        ReportUnrelatedFiles(UnrelatedFiles)

    End Sub


    Private Sub ReportUnrelatedFiles(UnrelatedFiles As List(Of String))

        If UnrelatedFiles.Count > 0 Then
            Dim UP As New UtilsPreferences

            Dim Timestamp As String = System.DateTime.Now.ToString("yyyyMMdd_HHmmss")

            Dim LogfileName As String
            LogfileName = $"{UP.GetTempDirectory}\{Timestamp}_Unrelated_Files.log"

            Try
                Using writer As New IO.StreamWriter(LogfileName, True)
                    writer.WriteLine("UNRELATED FILES")
                    writer.WriteLine("")
                    For Each Filename In UnrelatedFiles
                        writer.WriteLine(Filename)
                    Next
                End Using

                Try
                    ' Try to use the default application to open the file.
                    Process.Start(LogfileName)
                Catch ex As Exception
                    ' If none, open with notepad.exe
                    Process.Start("notepad.exe", LogfileName)
                End Try


            Catch ex As Exception
            End Try

        End If

    End Sub

    Private Function RemoveNestedFolders(
        TopLevelFolders As List(Of String)
        ) As List(Of String)

        ' Sorts the list
        ' [
        '   c:\
        '   c:\data
        '   c:\data\projects
        '   c:\data\projects\project1
        '   c:\data\projects\project111
        '   c:\data\standard_parts
        '   d:\other
        '   d:\other\something
        ' ]
        ' Adds the first entry to the output list.
        ' Checks if each subsequent entry contains any entry in the output list.
        ' eg, "c:\data\projects\".StartsWith("c:\data\")
        ' If not, it is added.

        Dim tmpFolders As New List(Of String)
        Dim OutFolders As New List(Of String)

        If TopLevelFolders.Count < 2 Then
            Return TopLevelFolders
        End If

        For Each Dirname In TopLevelFolders
            If Dirname.EndsWith("\") Then  ' Deal with pre-existing trailing backslash, eg 'c:\'
                Dirname = Dirname.Remove(Dirname.Length - 1)
            End If
            Dirname = $"{Dirname}\"
            Dirname = Dirname.ToLower
            If Not tmpFolders.Contains(Dirname) Then tmpFolders.Add(Dirname)
        Next

        tmpFolders.Sort()

        OutFolders.Add(tmpFolders(0))

        For i = 1 To tmpFolders.Count - 1
            Dim IsChildFolder As Boolean = False
            For Each OutTLF As String In OutFolders
                If tmpFolders(i).StartsWith(OutTLF, StringComparison.Ordinal) Then  ' eg, "c:\data\projects\".StartsWith("c:\data\")
                    IsChildFolder = True
                    Exit For
                End If
            Next
            If Not IsChildFolder Then
                OutFolders.Add(tmpFolders(i))
            End If
        Next

        For i = 0 To OutFolders.Count - 1
            If Not OutFolders(i).EndsWith(":\") Then
                OutFolders(i) = OutFolders(i).Remove(OutFolders(i).Length - 1)
            End If
        Next

        Return OutFolders
    End Function

    Private Sub UpdateStatus(Description As String, Filename As String)
        Dim msg As String

        msg = System.IO.Path.GetFileName(Filename)
        msg = $"{Description} {msg}"

        FMain.TextBoxStatus.Text = msg
    End Sub

    Private Function CheckInterruptRequest() As Boolean
        Dim tf As Boolean = False

        System.Windows.Forms.Application.DoEvents()
        If FMain.StopProcess Then
            Dim s As String = "Processing stopped by user"
            If Not Me.ErrorLogger.ContainsMessage(s) Then Me.ErrorLogger.AddMessage(s)
            tf = True
        End If

        Return tf
    End Function

    Private Function GetIndexedDrives() As List(Of String)

        Dim IndexedDrives As New List(Of String)

        Dim SearchScopeFilename As String = FMain.TextBoxFastSearchScopeFilename.Text
        Dim SearchScope As String() = Nothing

        Try
            SearchScope = IO.File.ReadAllLines(SearchScopeFilename)
        Catch ex As Exception
            Me.ErrorLogger.AddMessage($"Fast search scope file '{SearchScopeFilename}' (on Configuration Tab) not found.  Exception: {ex.Message}")
        End Try

        If SearchScope IsNot Nothing AndAlso SearchScope.Length > 0 Then
            Dim CommentString As String = "\\ "
            For Each item As String In SearchScope
                Dim Line As String = item.Trim
                If (Not Line.StartsWith(CommentString)) And (Line.Length > 0) Then
                    IndexedDrives.Add(Line)
                End If
            Next
        End If

        Return IndexedDrives
    End Function

    Private Function IsRootReachable(
        Path As String,
        RootReachables As Dictionary(Of String, Boolean)
        ) As Boolean

        ' \\server\share\a\b\c.par, -> \\server\share
        ' D:\Projects\a\b.par, -> D:\
        ' a.par -> ""

        Dim TargetCount As Integer
        If Path.StartsWith("\\") Then
            TargetCount = 4
        Else
            TargetCount = 1
        End If

        Dim EndIdx As Integer
        Dim SlashCount As Integer = 0
        For EndIdx = 0 To Path.Length - 1
            If Path(EndIdx) = "\" Then SlashCount += 1
            If SlashCount = TargetCount Then Exit For
        Next

        Dim Root As String = Nothing

        If SlashCount = TargetCount Then
            If TargetCount = 1 Then EndIdx += 1  ' Keep the trailing \ on local drives
            Root = Path.Substring(0, EndIdx)
        Else
            Root = ""
        End If

        If Not RootReachables.ContainsKey(Root) Then
            RootReachables(Root) = IO.Directory.Exists(Root)
        End If

        Return RootReachables(Root)

    End Function

    Private Function FileInTopLevelFolders(Filename As String) As Boolean
        Dim IsIn As Boolean = False
        Dim tmpTopLevelFolder As String

        For Each TopLevelFolder As String In Me.TopLevelFolders
            tmpTopLevelFolder = TopLevelFolder
            If Not tmpTopLevelFolder.EndsWith("\") Then tmpTopLevelFolder = $"{tmpTopLevelFolder}\"
            If Filename.StartsWith(tmpTopLevelFolder, StringComparison.OrdinalIgnoreCase) Then
                IsIn = True
                Exit For
            End If
        Next

        Return IsIn
    End Function
End Class

Public Class FileLinksContainer
    Public Property UnprocessedFilenames As HashSet(Of String)

    Private Property Items As Dictionary(Of String, FileLinks)

    Public Sub New()
        Me.Items = New Dictionary(Of String, FileLinks)(StringComparer.OrdinalIgnoreCase)
        Me.UnprocessedFilenames = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
    End Sub

    Public Sub AddItem(FileLinks As FileLinks)
        Me.Items(FileLinks.Name) = FileLinks
    End Sub

    Public Function GetItem(ItemName As String) As FileLinks
        If Me.Items.ContainsKey(ItemName) Then
            Return Me.Items(ItemName)
        Else
            Return Nothing
        End If
    End Function

    Public Function GetItems() As List(Of FileLinks)
        Return Me.Items.Values.ToList
    End Function

End Class

Public Class FileLinks
    Public Property Name As String
    Public Property Contains As List(Of String)
    Public Property ContainedBy As List(Of String)
    Public Property Visited As Boolean
    Public Property IsFOPMaster As Boolean

    Public Sub New(FullName As String)
        Me.Name = FullName
        Me.Contains = New List(Of String)
        Me.ContainedBy = New List(Of String)
        Me.Visited = False
        Me.IsFOPMaster = False
    End Sub

End Class
