Option Strict On

Public Class UtilsTopLevelAssembly
    Public Property FMain As Form_Main

    Public Sub New(_Form_Main As Form_Main)
        Me.FMain = _Form_Main
    End Sub


    Public Function GetLinksTopDown(
         TopLevelFolders As List(Of String),
         TopLevelAssembly As String,
         ActiveFileExtensionsList As List(Of String),
         Report As Boolean
         ) As List(Of String)

        Dim FoundFiles As New List(Of String)


        TopLevelFolders = RemoveNestedFolders(TopLevelFolders)

        Dim AllFilenames As New List(Of String)
        Dim FileLinksContainer As New FileLinksContainer
        Dim UnrelatedFiles As New List(Of String)

        '###### Get filenames in top level folders ######
        AllFilenames = GetAllFilesTopDown(TopLevelFolders, TopLevelAssembly)

        '###### Get links contained in each file ######
        FileLinksContainer = GetContains(AllFilenames)

        '###### Populate ContainedBy information ######
        FileLinksContainer = GetContainedBy(FileLinksContainer)

        '###### Find files related to the top level assembly ######
        FileLinksContainer = VisitLinks(TopLevelAssembly, FileLinksContainer)

        '###### Prep the TLA file list ######
        For Each FileLinks As FileLinks In FileLinksContainer.GetItems
            Dim Extension As String = IO.Path.GetExtension(FileLinks.Name).Replace(".", "*.")
            If ActiveFileExtensionsList.Contains(Extension) Then
                If FileLinks.Visited Then
                    FoundFiles.Add(FileLinks.Name)
                Else
                    UnrelatedFiles.Add(FileLinks.Name)
                End If
            End If
        Next

        '###### Check for unprocessed files ######
        If FileLinksContainer.UnprocessedFilenames.Count > 0 Then
            Dim Title As String = String.Format("The following files, and any links they contain, could not be processed.  This can happen if{0}", vbCrLf)
            Title = String.Format("{0}-- The file name (including path) has > 260 characters.{1}", Title, vbCrLf)
            Title = String.Format("{0}-- The file is already open in another process.{1}", Title, vbCrLf)
            Title = String.Format("{0}-- The file is not a Solid Edge file, or is somehow corrupt.{1}", Title, vbCrLf)
            Title = String.Format("{0}-- An unknown error occurred.{1}{1}", Title, vbCrLf)
            Title = String.Format("{0}Processing will continue.  Please verify results.", Title)

            Dim UC As New UtilsCommon
            Dim s As String = UC.FormatMsgBoxText(FileLinksContainer.UnprocessedFilenames, Title)
            If Not s = "" Then MsgBox(s, vbOKOnly)
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

            Dim tmpAllFilenames = FileIO.FileSystem.GetFiles(
                                      TopLevelFolder,
                                      FileIO.SearchOption.SearchAllSubDirectories,
                                      ActiveFileExtensionsList.ToArray)

            For Each tmpFilename As String In tmpAllFilenames

                FMain.TextBoxStatus.Text = String.Format("Getting files {0}", IO.Path.GetFileName(tmpFilename))

                If Not AllFilenames.Contains(tmpFilename, StringComparer.OrdinalIgnoreCase) Then
                    AllFilenames.Add(tmpFilename)
                End If
            Next
        Next

        Return AllFilenames
    End Function

    Public Function GetContains(AllFilenames As List(Of String)) As FileLinksContainer

        Dim FileLinksContainer As New FileLinksContainer
        Dim SSDoc As HCStructuredStorageDoc = Nothing
        Dim SSLinkDoc As HCStructuredStorageDoc = Nothing
        Dim LinkNames As List(Of String)

        For Each Filename As String In AllFilenames
            FMain.TextBoxStatus.Text = String.Format("Getting links {0}", IO.Path.GetFileName(Filename))

            Try
                Dim FileLinks As New FileLinks(Filename)

                SSDoc = New HCStructuredStorageDoc(Filename)
                SSDoc.ReadLinks(FMain.LinkManagementOrder)
                LinkNames = SSDoc.GetLinkNames

                For Each LinkName As String In LinkNames

                    Try
                        Try
                            SSLinkDoc = New HCStructuredStorageDoc(LinkName)
                        Catch ex As Exception
                            If SSLinkDoc IsNot Nothing Then SSLinkDoc.Close()
                            If Not FileLinksContainer.UnprocessedFilenames.Contains(LinkName) Then
                                FileLinksContainer.UnprocessedFilenames.Add(LinkName)
                            End If
                        End Try

                        If (SSLinkDoc IsNot Nothing) AndAlso (Not SSLinkDoc.IsFOPMaster) Then

                            If FMain.TLAIncludePartCopies Then
                                FileLinks.Contains.Add(LinkName)

                            Else
                                Dim Extension As String = IO.Path.GetExtension(Filename)
                                If Not ((Extension = ".par") Or (Extension = ".psm")) Then
                                    FileLinks.Contains.Add(LinkName)
                                End If
                            End If

                        Else
                            Dim i = 0
                        End If

                        If SSLinkDoc IsNot Nothing Then SSLinkDoc.Close()

                    Catch ex As Exception
                        If SSLinkDoc IsNot Nothing Then SSLinkDoc.Close()
                        If Not FileLinksContainer.UnprocessedFilenames.Contains(LinkName) Then
                            FileLinksContainer.UnprocessedFilenames.Add(LinkName)
                        End If
                    End Try


                Next

                SSDoc.Close()

                FileLinks.Contains = FileLinks.Contains.Distinct.ToList

                FileLinksContainer.AddItem(FileLinks)

            Catch ex2 As Exception
                If SSLinkDoc IsNot Nothing Then SSLinkDoc.Close()
                If SSDoc IsNot Nothing Then SSDoc.Close()
                If Not FileLinksContainer.UnprocessedFilenames.Contains(Filename) Then
                    FileLinksContainer.UnprocessedFilenames.Add(Filename)
                End If
            End Try
        Next

        Return FileLinksContainer
    End Function

    Private Function GetContainedBy(FileLinksContainer As FileLinksContainer) As FileLinksContainer

        For Each FileLinks As FileLinks In FileLinksContainer.GetItems
            FMain.TextBoxStatus.Text = String.Format("Contained by {0}", IO.Path.GetFileName(FileLinks.Name))

            For Each ContainsFilename In FileLinks.Contains
                Dim tmpFileLinks As FileLinks = FileLinksContainer.GetItem(ContainsFilename)
                If tmpFileLinks IsNot Nothing Then tmpFileLinks.ContainedBy.Add(FileLinks.Name)
            Next
        Next

        Return FileLinksContainer
    End Function

    Private Function VisitLinks(
        Filename As String,
        FileLinksContainer As FileLinksContainer
        ) As FileLinksContainer

        Dim FileLinks As FileLinks

        Dim LinkedFilename As String
        Dim ContainedByFilename As String
        Dim Extension As String

        FMain.TextBoxStatus.Text = String.Format("Visit links {0}", IO.Path.GetFileName(Filename))

        FileLinks = FileLinksContainer.GetItem(Filename)
        If FileLinks IsNot Nothing Then
            FileLinks.Visited = True

            For Each ContainedByFilename In FileLinks.ContainedBy
                Extension = IO.Path.GetExtension(ContainedByFilename)
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
                            Extension = IO.Path.GetExtension(ContainedByFilename)
                            If Extension = ".dft" Then
                                Dim tmptmpFileLinks As FileLinks = FileLinksContainer.GetItem(ContainedByFilename)
                                If tmptmpFileLinks IsNot Nothing Then tmptmpFileLinks.Visited = True
                                FileLinksContainer = VisitLinks(ContainedByFilename, FileLinksContainer)
                            End If
                        Next
                        Extension = IO.Path.GetExtension(LinkedFilename)
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

    Private Sub ReportUnrelatedFiles(UnrelatedFiles As List(Of String))

        If UnrelatedFiles.Count > 0 Then
            Dim Timestamp As String = System.DateTime.Now.ToString("yyyyMMdd_HHmmss")
            Dim LogfileName As String
            LogfileName = IO.Path.GetTempPath + "\Housekeeper_" + Timestamp + "_Unrelated_Files.log"

            Try
                Using writer As New IO.StreamWriter(LogfileName, True)
                    writer.WriteLine("UNRELATED FILES")
                    writer.WriteLine("")
                    For Each Filename In UnrelatedFiles
                        writer.WriteLine(String.Format(Filename))
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


    Private Sub ReportUnrelatedFilesOLD(
               TopLevelFolders As List(Of String),
               Foundfiles As List(Of String))

        Dim AllFiles As New List(Of String)
        Dim SomeFiles As IReadOnlyCollection(Of String)
        Dim ActiveFileExtensionsList As New List(Of String)
        Dim UnrelatedFiles As New List(Of String)

        If TopLevelFolders.Count = 0 Then
            Exit Sub
        End If

        ActiveFileExtensionsList.Add("*.asm")
        ActiveFileExtensionsList.Add("*.par")
        ActiveFileExtensionsList.Add("*.psm")
        ActiveFileExtensionsList.Add("*.dft")

        For Each TopLevelFolder In TopLevelFolders
            SomeFiles = FileIO.FileSystem.GetFiles(TopLevelFolder,
                        FileIO.SearchOption.SearchAllSubDirectories,
                        ActiveFileExtensionsList.ToArray)

            For Each Filename In SomeFiles
                If Not Foundfiles.Contains(Filename, StringComparer.OrdinalIgnoreCase) Then
                    If Not UnrelatedFiles.Contains(Filename, StringComparer.OrdinalIgnoreCase) Then
                        UnrelatedFiles.Add(Filename)
                    End If
                End If
            Next

        Next

        If UnrelatedFiles.Count > 0 Then
            Dim Timestamp As String = System.DateTime.Now.ToString("yyyyMMdd_HHmmss")
            Dim LogfileName As String
            LogfileName = IO.Path.GetTempPath + "\Housekeeper_" + Timestamp + "_Unrelated_Files.log"

            Try
                Using writer As New IO.StreamWriter(LogfileName, True)
                    writer.WriteLine("UNRELATED FILES")
                    writer.WriteLine("")
                    For Each Filename In UnrelatedFiles
                        writer.WriteLine(String.Format(Filename))
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



    Public Function GetLinksBottomUp(TopLevelFolders As List(Of String),
                             TopLevelAssembly As String,
                             ActiveFileExtensionsList As List(Of String),
                             DraftAndModelSameName As Boolean,
                             Report As Boolean) As List(Of String)

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


        Dim DMApp As New RevisionManager.Application
        Dim TLADoc As RevisionManager.Document

        DMApp.Visible = 1
        DMApp.DisplayAlerts = 0

        FMain.Activate()

        FMain.TextBoxStatus.Text = String.Format("Opening {0}", System.IO.Path.GetFileName(TopLevelAssembly))

        TopLevelFolders = RemoveNestedFolders(TopLevelFolders)

        TLADoc = CType(DMApp.OpenFileInRevisionManager(TopLevelAssembly), RevisionManager.Document)

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

            For Each tmpAllLinkedFilename In tmpAllLinkedFilenames
                If Not AllLinkedFilenames.Contains(tmpAllLinkedFilename, StringComparer.OrdinalIgnoreCase) Then
                    AllLinkedFilenames.Add(tmpAllLinkedFilename)
                End If
            Next

        End If

        Try
            DMApp.Quit()
        Catch ex As Exception
        End Try

        For Each Filename In AllLinkedFilenames
            FileExtension = System.IO.Path.GetExtension(Filename).Replace(".", "*.")
            tf = ActiveFileExtensionsList.Contains(FileExtension)
            tf = tf And (Not FoundFiles.Contains(Filename, StringComparer.OrdinalIgnoreCase))
            If tf Then
                FoundFiles.Add(Filename)
            End If
        Next

        If Report Then
            ReportUnrelatedFilesOLD(TopLevelFolders, FoundFiles)

        End If

        Return FoundFiles

    End Function

    Private Function FollowLinksBottomUp(DMApp As RevisionManager.Application,
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

        If CheckInterruptRequest() Then
            Return AllLinkedFilenames
        End If

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
                        If Not AllLinkedFilenames.Contains(WhereUsedFile, StringComparer.OrdinalIgnoreCase) Then
                            AllLinkedFilenames.Add(WhereUsedFile)

                            Extension = IO.Path.GetExtension(WhereUsedFile)
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
                        tf = System.IO.Path.GetExtension(Filename) = ".asm"
                        tf = tf Or System.IO.Path.GetExtension(Filename) = ".dft"
                    End If

                    ' Follow links contained by this file, if any.
                    If tf Then
                        LinkedDocs = CType(DMDoc.LinkedDocuments, RevisionManager.LinkedDocuments)
                        If LinkedDocs.Count > 0 Then
                            For Each LinkedDoc In LinkedDocs

                                ' Get FOP status
                                Dim FOPStatus As Integer
                                LinkedDoc.IsDocumentFOP(FOPStatus)

                                ' FOP Masters can have links to many unrelated files.  For example, a fastener.  
                                ' Do not include them, or follow their links.
                                If Not (FOPStatus = RevisionManager.DocFOPStatus.FOPMasterDocument) Then
                                    LinkedDocName = LinkedDoc.FullName

                                    LinkedDocName = UC.GetFOAFilename(LinkedDocName)

                                    Extension = IO.Path.GetExtension(LinkedDocName)

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
                     DraftAndModelSameName As Boolean) As List(Of String)

        Dim AllWhereUsedFileNames As New List(Of String)

        Dim Extension As String

        Dim WhereUsedDocuments As New List(Of RevisionManager.Document)
        Dim WhereUsedDocument As RevisionManager.Document

        Dim arrDocUsed As Object = Nothing

        Dim DraftFilename As String

        UpdateStatus("Where Used", Filename)

        If TopLevelFolder = "" Then
            Return AllWhereUsedFileNames
        End If

        If CheckInterruptRequest() Then
            Return AllWhereUsedFileNames
        End If

        If DraftAndModelSameName Then
            DraftFilename = System.IO.Path.ChangeExtension(Filename, ".dft")
            AllWhereUsedFileNames.Add(DraftFilename)
            Return AllWhereUsedFileNames
        End If

        Extension = IO.Path.GetExtension(Filename)

        If Not Extension = ".dft" Then  ' Draft files are not "Used" anywhere.
            If Not IsDriveIndexed Then
                'This "resets" DMApp.FindWhereUsed().  Somehow.
                DMApp.WhereUsedCriteria(Nothing, True) = TopLevelFolder

                'Finds the first WhereUsed Document, if any.
                WhereUsedDocument = CType(
                DMApp.FindWhereUsed(FileIO.FileSystem.GetFileInfo(Filename)),
                RevisionManager.Document)

                While Not WhereUsedDocument Is Nothing
                    If Not AllWhereUsedFileNames.Contains(WhereUsedDocument.FullName, StringComparer.OrdinalIgnoreCase) Then
                        ' For bottom_up search, the only applicable where used results are draft files
                        If IO.Path.GetExtension(WhereUsedDocument.FullName) = ".dft" Then
                            AllWhereUsedFileNames.Add(WhereUsedDocument.FullName)
                        End If
                    End If
                    'Finds the next WhereUsed document, if any.
                    WhereUsedDocument = CType(DMApp.FindWhereUsed(), RevisionManager.Document)
                End While

            Else  'It is indexed
                Try
                    DMApp.WhereUsedCriteria(Nothing, True) = TopLevelFolder

                    DMApp.FindWhereUsedDocuments(FileIO.FileSystem.GetFileInfo(Filename), arrDocUsed)

                    For Each item As String In DirectCast(arrDocUsed, Array)
                        If Not AllWhereUsedFileNames.Contains(item, StringComparer.OrdinalIgnoreCase) Then
                            ' For bottom_up search, the only applicable where used results are draft files
                            If IO.Path.GetExtension(item) = ".dft" Then
                                AllWhereUsedFileNames.Add(item)
                            End If
                        End If
                    Next

                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try

            End If

        End If

        Return AllWhereUsedFileNames

    End Function

    Private Function RemoveNestedFolders(
        TopLevelFolders As List(Of String)
        ) As List(Of String)

        ' Sorts the list
        ' [
        '   c:\data
        '   c:\data\projects
        '   c:\data\projects\project1
        '   c:\data\standard_parts
        '   d:\other
        '   d:\other\something
        ' ]
        ' Adds the first entry to the output list.
        ' Checks if each subsequent entry contains any entry in the output list.
        ' eg, "c:\data\projects".Contains("c:\data")
        ' If not, it is added.

        Dim tmpTopLevelFolders As New List(Of String)
        Dim OutTopLevelFolders As New List(Of String)

        If TopLevelFolders.Count < 2 Then
            Return TopLevelFolders
        End If

        For Each Dirname In TopLevelFolders
            tmpTopLevelFolders.Add(Dirname.ToLower)
        Next

        tmpTopLevelFolders.Sort()

        OutTopLevelFolders.Add(tmpTopLevelFolders(0))

        For i = 1 To tmpTopLevelFolders.Count - 1
            Dim IsChildFolder As Boolean = False
            For Each OutTLF As String In OutTopLevelFolders
                If tmpTopLevelFolders(i).Contains(OutTLF) Then  ' eg, "c:\data\projects".Contains("c:\data")
                    IsChildFolder = True
                    Exit For
                End If
            Next
            If Not IsChildFolder Then
                OutTopLevelFolders.Add(tmpTopLevelFolders(i))
            End If
        Next

        Return OutTopLevelFolders
    End Function


    Private Sub UpdateStatus(Description As String, Filename As String)
        Dim msg As String

        msg = System.IO.Path.GetFileName(Filename)
        msg = String.Format("{0} {1}", Description, msg)

        FMain.TextBoxStatus.Text = msg
    End Sub

    Private Function CheckInterruptRequest() As Boolean
        Dim tf As Boolean = False

        System.Windows.Forms.Application.DoEvents()
        If Form_Main.StopProcess Then
            FMain.TextBoxStatus.Text = "Processing aborted"
            tf = True
        End If

        Return tf
    End Function

    Private Function GetIndexedDrives() As List(Of String)

        Dim SearchScopeFilename As String = FMain.TextBoxFastSearchScopeFilename.Text

        Dim SearchScope As String() = Nothing
        Dim CommentString As String = "\\ "
        Dim Line As String
        Dim IndexedDrives As New List(Of String)
        Dim msg As String = ""

        Try
            SearchScope = IO.File.ReadAllLines(SearchScopeFilename)
        Catch ex As Exception
            msg = String.Format("Fast search scope file '{0}' (on Configuration Tab) not found.{1}", SearchScopeFilename, vbCrLf)
            MsgBox(msg)
        End Try

        If (SearchScope.Count > 0) And (SearchScope IsNot Nothing) Then
            For Each item As String In SearchScope
                Line = item.TrimStart()
                If (Not Line.StartsWith(CommentString)) And (Line.Count > 0) Then
                    IndexedDrives.Add(Line)
                End If
            Next
        End If

        Return IndexedDrives
    End Function



End Class

Public Class FileLinksContainer
    Public Property UnprocessedFilenames As List(Of String)

    Private Property Items As List(Of FileLinks)
    Private Property ItemNames As List(Of String)

    Public Sub New()
        Me.Items = New List(Of FileLinks)
        Me.ItemNames = New List(Of String)
        Me.UnprocessedFilenames = New List(Of String)
    End Sub

    Public Sub AddItem(FileLinks As FileLinks)
        Me.Items.Add(FileLinks)
        Me.ItemNames.Add(FileLinks.Name.ToLower)
    End Sub

    Public Function GetItem(ItemName As String) As FileLinks
        Dim idx As Integer = Me.ItemNames.IndexOf(ItemName.ToLower)
        If Not idx = -1 Then
            Return Me.Items(idx)
        Else
            Return Nothing
        End If
    End Function

    Public Function GetItems() As List(Of FileLinks)
        Return Me.Items
    End Function
End Class

Public Class FileLinks
    Public Property Name As String
    Public Property Contains As List(Of String)
    Public Property ContainedBy As List(Of String)
    Public Property Visited As Boolean

    Public Sub New(FullName As String)
        Me.Name = FullName
        Me.Contains = New List(Of String)
        Me.ContainedBy = New List(Of String)
        Me.Visited = False
    End Sub

End Class
