Option Strict On

Public Class TopLevelAssemblyUtilities
    Private _mainInstance As Form1

    Public Sub New(mainInstance As Form1)
        _mainInstance = mainInstance
    End Sub

    Public Function GetLinks(SearchType As String,
                             TopLevelFolder As String,
                             TopLevelAssembly As String,
                             ActiveFileExtensionsList As List(Of String),
                             Optional Report As Boolean = False) As List(Of String)
        Dim Foundfiles As New List(Of String)

        If SearchType.ToLower = "topdown" Then
            Foundfiles = GetLinksTopDown(TopLevelFolder, TopLevelAssembly, ActiveFileExtensionsList, Report)
        Else
            Foundfiles = GetLinksBottomUp(TopLevelFolder, TopLevelAssembly, ActiveFileExtensionsList)
        End If
        Return Foundfiles
    End Function

    Public Function GetLinksTopDown(TopLevelFolder As String,
                                    TopLevelAssembly As String,
                                    ActiveFileExtensionsList As List(Of String),
                                    Report As Boolean) As List(Of String)

        Dim DMApp As New DesignManager.Application
        Dim AllLinkedFilenames As New List(Of String)
        Dim FoundFiles As New List(Of String)
        Dim FileExtension As String
        Dim AllFilenames As New Dictionary(Of String, String)
        Dim LinkDict As New Dictionary(Of String, Dictionary(Of String, List(Of String)))
        Dim UnrelatedFiles As New List(Of String)

        ' DMApp = New DesignManager.Application()

        DMApp.Visible = 1  ' So it can be seen and closed in case of program malfunction.

        Form1.Activate()

        AllFilenames = GetAllFilenamesTopDown(TopLevelFolder)
        If Not AllFilenames.Keys.Contains(TopLevelAssembly.ToLower) Then
            AllFilenames.Add(TopLevelAssembly.ToLower, TopLevelAssembly)
        End If

        LinkDict = CreateLinkDict(AllFilenames, LinkDict)

        LinkDict = PopulateLinkDict(DMApp, LinkDict, TopLevelFolder)

        If CheckInterruptRequest() Then
            DMApp.Quit()
            Return FoundFiles
        End If
        LinkDict = VisitLinks(TopLevelAssembly, LinkDict, TopLevelFolder)

        For Each Filename In LinkDict.Keys
            FileExtension = System.IO.Path.GetExtension(Filename).Replace(".", "*.")
            If ActiveFileExtensionsList.Contains(FileExtension) Then
                If LinkDict(Filename)("Visited")(0) = "True" Then
                    FoundFiles.Add(Filename)
                Else
                    UnrelatedFiles.Add(Filename)
                End If
            End If
        Next

        If Report Then
            If UnrelatedFiles.Count > 0 Then
                Dim Timestamp As String = System.DateTime.Now.ToString("yyyyMMdd_HHmmss")
                Dim LogfileName As String
                Dim msg As String
                LogfileName = TopLevelFolder + "\Housekeeper_" + Timestamp + "_Unrelated_Files.log"

                Try
                    Using writer As New IO.StreamWriter(LogfileName, True)
                        For Each Filename In UnrelatedFiles
                            Filename = Filename.Replace(TopLevelFolder, "")
                            writer.WriteLine(String.Format(Filename))
                        Next
                    End Using

                    LogfileName = LogfileName.Replace(TopLevelFolder, "")
                    msg = String.Format("Files unrelated to top level assembly found.  See log file{0}", vbCrLf)
                    msg += LogfileName
                    MsgBox(msg)
                Catch ex As Exception
                End Try


            End If

        End If

        DMApp.Quit()

        Return FoundFiles

    End Function

    Private Function CreateLinkDict(
         AllFilenames As Dictionary(Of String, String),
         LinkDict As Dictionary(Of String, Dictionary(Of String, List(Of String)))
         ) As Dictionary(Of String, Dictionary(Of String, List(Of String)))

        Dim FilenameLowerCase As String
        Dim Filename As String

        For Each FilenameLowerCase In AllFilenames.Keys
            Filename = AllFilenames(FilenameLowerCase)

            LinkDict.Add(Filename, New Dictionary(Of String, List(Of String)))

            LinkDict(Filename).Add("Visited", New List(Of String))
            LinkDict(Filename)("Visited").Add("False")

            LinkDict(Filename).Add("Contains", New List(Of String))

            LinkDict(Filename).Add("ContainedBy", New List(Of String))
        Next

        Return LinkDict
    End Function

    Private Function PopulateLinkDict(
         DMApp As DesignManager.Application,
         LinkDict As Dictionary(Of String, Dictionary(Of String, List(Of String))),
         TopLevelFolder As String
         ) As Dictionary(Of String, Dictionary(Of String, List(Of String)))

        Dim Filename As String

        For Each Filename In LinkDict.Keys
            Form1.TextBoxStatus.Text = Filename

            If CheckInterruptRequest() Then
                Return LinkDict
            End If

            ' LinkDict = GetDownstreamLinks(DMApp, LinkDict, Filename)
            LinkDict = GetContains(DMApp, LinkDict, Filename, TopLevelFolder)
        Next

        'LinkDict = GetUpstreamLinks(LinkDict)
        LinkDict = GetContainedBy(LinkDict, TopLevelFolder)

        Return LinkDict
    End Function

    Private Function GetContains(
        DMApp As DesignManager.Application,
        LinkDict As Dictionary(Of String, Dictionary(Of String, List(Of String))),
        Filename As String,
        TopLevelFolder As String
         ) As Dictionary(Of String, Dictionary(Of String, List(Of String)))

        Dim DMDoc As DesignManager.Document
        Dim LinkedDocs As DesignManager.LinkedDocuments
        Dim LinkedDoc As DesignManager.Document
        Dim CorrectedFilename As String
        Dim tf As Boolean

        If CheckInterruptRequest() Then
            Return LinkDict
        End If

        UpdateStatus("Follow Links", Filename, TopLevelFolder)

        DMDoc = CType(DMApp.Open(Filename), DesignManager.Document)

        ' Some files are corrrupt or have other problems
        Try
            LinkedDocs = CType(DMDoc.LinkedDocuments, DesignManager.LinkedDocuments)

            For Each LinkedDoc In LinkedDocs
                CorrectedFilename = GetCorrectedFilename(LinkDict, LinkedDoc.FullName)
                If Not LinkDict(Filename)("Contains").Contains(CorrectedFilename) Then
                    If CorrectedFilename <> "" Then
                        LinkDict(Filename)("Contains").Add(CorrectedFilename)
                    End If
                End If
            Next
        Catch ex As Exception
        End Try

        Return LinkDict
    End Function

    Private Function GetContainedBy(
        LinkDict As Dictionary(Of String, Dictionary(Of String, List(Of String))),
        TopLevelFolder As String
         ) As Dictionary(Of String, Dictionary(Of String, List(Of String)))

        Dim Filename As String
        Dim ContainsFilename As String

        For Each Filename In LinkDict.Keys
            UpdateStatus("Contained By", Filename, TopLevelFolder)
            For Each ContainsFilename In LinkDict(Filename)("Contains")
                LinkDict(ContainsFilename)("ContainedBy").Add(Filename)
            Next
        Next

        Return LinkDict
    End Function

    Private Function VisitLinks(
        Filename As String,
        LinkDict As Dictionary(Of String, Dictionary(Of String, List(Of String))),
        TopLevelFolder As String
        ) As Dictionary(Of String, Dictionary(Of String, List(Of String)))

        Dim LinkedFilename As String
        Dim ContainedByFilename As String
        Dim Extension As String

        UpdateStatus("Visit Links", Filename, TopLevelFolder)

        'For Each LinkedFilename In LinkDict(Filename)("Contains")
        '    If Not LinkDict(LinkedFilename)("Visited")(0) = "True" Then
        '        LinkDict(LinkedFilename)("Visited")(0) = "True"
        '        LinkDict = VisitLinks(LinkedFilename, LinkDict, TopLevelFolder)
        '    End If
        'Next

        'For Each LinkedFilename In LinkDict(Filename)("ContainedBy")
        '    If Not LinkDict(LinkedFilename)("Visited")(0) = "True" Then
        '        LinkDict(LinkedFilename)("Visited")(0) = "True"
        '        LinkDict = VisitLinks(LinkedFilename, LinkDict, TopLevelFolder)
        '    End If
        'Next

        LinkDict(Filename)("Visited")(0) = "True"
        For Each ContainedByFilename In LinkDict(Filename)("ContainedBy")
            Extension = IO.Path.GetExtension(ContainedByFilename)
            If Extension = ".dft" Then
                LinkDict(ContainedByFilename)("Visited")(0) = "True"
            End If
        Next

        For Each LinkedFilename In LinkDict(Filename)("Contains")
            If Not LinkDict(LinkedFilename)("Visited")(0) = "True" Then
                LinkDict(LinkedFilename)("Visited")(0) = "True"
                For Each ContainedByFilename In LinkDict(LinkedFilename)("ContainedBy")
                    Extension = IO.Path.GetExtension(ContainedByFilename)
                    If Extension = ".dft" Then
                        LinkDict(ContainedByFilename)("Visited")(0) = "True"
                    End If
                Next
                LinkDict = VisitLinks(LinkedFilename, LinkDict, TopLevelFolder)
            End If
        Next

        Return LinkDict
    End Function

    Private Function GetCorrectedFilename(
        LinkDict As Dictionary(Of String, Dictionary(Of String, List(Of String))),
        Filename As String
        ) As String

        ' The CorrectFilename respects the capitalization as it is on disk
        ' which may not match the capitilization of the link

        Dim CorrectFilename As String = ""

        For Each CorrectFilename In LinkDict.Keys
            If Filename.Contains("!") Then
                If CorrectFilename.ToLower = Filename.ToLower.Split("!"c)(0) Then
                    Return CorrectFilename
                End If
            End If
            If CorrectFilename.ToLower = Filename.ToLower Then
                Return CorrectFilename
            End If
        Next

        Return ""
    End Function

    Private Function FollowLinksTopDown(LinkedDocument As DesignManager.Document,
                                 AllLinkedFilenames As List(Of String)) As List(Of String)
        Dim LinkedDocs As DesignManager.LinkedDocuments
        Dim LinkedDoc As DesignManager.Document
        Dim msg As String
        Dim ValidExtensions As New List(Of String)
        Dim Extension As String

        ValidExtensions.Add(".asm")
        ValidExtensions.Add(".par")
        ValidExtensions.Add(".psm")
        ValidExtensions.Add(".dft")

        If FileIO.FileSystem.FileExists(LinkedDocument.FullName) Then
            If Not AllLinkedFilenames.Contains(LinkedDocument.FullName) Then
                AllLinkedFilenames.Add(LinkedDocument.FullName)

                msg = LinkedDocument.FullName.Replace(Form1.TextBoxInputDirectory.Text, "")
                msg = "Follow Links " + msg
                Form1.TextBoxStatus.Text = msg

                ' In case of corrupted file or other problem
                Try
                    LinkedDocs = CType(LinkedDocument.LinkedDocuments, DesignManager.LinkedDocuments)
                    If LinkedDocs.Count > 0 Then
                        For Each LinkedDoc In LinkedDocs
                            Extension = IO.Path.GetExtension(LinkedDoc.FullName)
                            If ValidExtensions.Contains(Extension) Then
                                AllLinkedFilenames = FollowLinksTopDown(LinkedDoc, AllLinkedFilenames)
                            End If
                        Next
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        Return AllLinkedFilenames

    End Function

    Private Function GetAllFilenamesTopDown(TopLevelFolder As String) As Dictionary(Of String, String)
        Dim AllFilenames As IReadOnlyCollection(Of String)
        Dim Filename As String
        Dim ActiveFileExtensionsList As New List(Of String)
        Dim AllFilenamesDict As New Dictionary(Of String, String)

        ActiveFileExtensionsList.Add("*.asm")
        ActiveFileExtensionsList.Add("*.par")
        ActiveFileExtensionsList.Add("*.psm")
        ActiveFileExtensionsList.Add("*.dft")

        AllFilenames = FileIO.FileSystem.GetFiles(TopLevelFolder,
                        FileIO.SearchOption.SearchAllSubDirectories,
                        ActiveFileExtensionsList.ToArray)

        For Each Filename In AllFilenames
            AllFilenamesDict.Add(Filename.ToLower, Filename)
        Next

        Return AllFilenamesDict

    End Function





    Public Function GetLinksBottomUp(TopLevelFolder As String,
                             TopLevelAssembly As String,
                             ActiveFileExtensionsList As List(Of String)) As List(Of String)

        Dim DMApp As New DesignManager.Application
        Dim AllLinkedFilenames As New List(Of String)
        Dim FoundFiles As New List(Of String)
        Dim FileExtension As String
        Dim AllFilenames As New Dictionary(Of String, String)
        Dim tf As Boolean

        DMApp.Visible = 1
        DMApp.DisplayAlerts = 0

        Form1.Activate()

        AllFilenames = GetAllFilenamesTopDown(TopLevelFolder)

        AllLinkedFilenames = FollowLinksBottomUp(DMApp, TopLevelAssembly, AllLinkedFilenames,
                                                 TopLevelFolder, AllFilenames)

        DMApp.Quit()

        'AllLinkedFilenames = GetWhereUsed(DMApp, TopLevelFolder, AllLinkedFilenames)

        'AllLinkedFilenames = GetLinksFromDraft(DMApp, AllLinkedFilenames)

        For Each Filename In AllLinkedFilenames
            FileExtension = System.IO.Path.GetExtension(Filename).Replace(".", "*.")
            tf = ActiveFileExtensionsList.Contains(FileExtension)
            tf = tf And (Not FoundFiles.Contains(Filename))
            If tf Then
                FoundFiles.Add(Filename)
            End If
        Next

        Return FoundFiles

    End Function

    Private Function FollowLinksBottomUp(DMApp As DesignManager.Application,
                                         Filename As String,
                                         AllLinkedFilenames As List(Of String),
                                         TopLevelFolder As String,
                                         AllFilenames As Dictionary(Of String, String)) As List(Of String)

        Dim DMDoc As DesignManager.Document
        Dim LinkedDocs As DesignManager.LinkedDocuments
        Dim LinkedDoc As DesignManager.Document
        Dim LinkedDocName As String
        Dim msg As String
        Dim ValidExtensions As New List(Of String)
        Dim Extension As String
        Dim WhereUsedFiles As New List(Of String)
        Dim WhereUsedFile As String
        Dim CorrectedFilename As String
        Dim tf As Boolean
        Dim CorrectedWhereUsedFile As String

        If CheckInterruptRequest() Then
            Return AllLinkedFilenames
        End If

        ValidExtensions.Add(".asm")
        ValidExtensions.Add(".par")
        ValidExtensions.Add(".psm")
        ValidExtensions.Add(".dft")

        If FileIO.FileSystem.FileExists(Filename) Then
            CorrectedFilename = GetCorrectedFilenameBottomUp(Filename, AllFilenames)
            tf = CorrectedFilename <> ""
            tf = tf And Not AllLinkedFilenames.Contains(CorrectedFilename)
            If tf Then
                AllLinkedFilenames.Add(CorrectedFilename)

                UpdateStatus("Follow Links", CorrectedFilename, TopLevelFolder)

                ' In case of corrupted file or other problem
                Try
                    DMDoc = CType(DMApp.OpenFileInDesignManager(Filename), DesignManager.Document)

                    ' Get any draft files containing this file.
                    WhereUsedFiles = GetWhereUsedBottomUp(DMApp, TopLevelFolder, DMDoc.FullName)
                    For Each WhereUsedFile In WhereUsedFiles
                        ' 20210204 Changed from
                        'AllLinkedFilenames = FollowLinksBottomUp(DMApp, WhereUsedFile, AllLinkedFilenames,
                        '                                         TopLevelFolder, AllFilenames)
                        ' 20210204 To
                        Extension = IO.Path.GetExtension(WhereUsedFile)
                        If Extension = ".dft" Then
                            CorrectedWhereUsedFile = GetCorrectedFilenameBottomUp(WhereUsedFile, AllFilenames)
                            AllLinkedFilenames.Add(CorrectedWhereUsedFile)
                        End If
                        ' 20210204 End change
                    Next

                    ' Follow links contained by this file, if any.
                    LinkedDocs = CType(DMDoc.LinkedDocuments, DesignManager.LinkedDocuments)
                    If LinkedDocs.Count > 0 Then
                        For Each LinkedDoc In LinkedDocs
                            LinkedDocName = LinkedDoc.FullName
                            If LinkedDocName.Contains("!") Then
                                LinkedDocName = LinkedDocName.Split("!"c)(0)
                            End If
                            Extension = IO.Path.GetExtension(LinkedDocName)
                            If ValidExtensions.Contains(Extension) Then
                                AllLinkedFilenames = FollowLinksBottomUp(DMApp, LinkedDocName, AllLinkedFilenames,
                                                                         TopLevelFolder, AllFilenames)
                            End If
                        Next
                    End If

                    DMDoc.Close()
                    System.Threading.Thread.Sleep(100)
                Catch ex As Exception
                    MsgBox(Filename)
                End Try
            End If
        End If


        Return AllLinkedFilenames

    End Function


    'Private Function Bare(Filename As String) As String
    '    Dim BareFilename As String

    '    If Filename.Contains("!") Then
    '        BareFilename = Filename.Split("!"c)(0)
    '    Else
    '        BareFilename = Filename
    '    End If

    '    Return BareFilename
    'End Function

    Private Function GetWhereUsedBottomUp(
                     DMApp As DesignManager.Application,
                     TopLevelFolder As String,
                     Filename As String) As List(Of String)

        Dim AllWhereUsedFileNames As New List(Of String)
        Dim msg As String
        Dim Extension As String

        Dim WhereUsedDocuments As New List(Of DesignManager.Document)
        Dim WhereUsedDocument As DesignManager.Document

        If CheckInterruptRequest() Then
            Return AllWhereUsedFileNames
        End If
        UpdateStatus("Where Used", Filename, TopLevelFolder)

        Extension = IO.Path.GetExtension(Filename)

        If Not Extension = ".dft" Then  ' Draft files are not "Used" anywhere.
            'This "resets" DMApp.FindWhereUsed().  Somehow.
            DMApp.WhereUsedCriteria(Nothing, True) = TopLevelFolder

            'Finds the first WhereUsed Document, if any.
            WhereUsedDocument = CType(
                DMApp.FindWhereUsed(FileIO.FileSystem.GetFileInfo(Filename)),
                DesignManager.Document)

            While Not WhereUsedDocument Is Nothing
                If Not AllWhereUsedFileNames.Contains(WhereUsedDocument.FullName) Then
                    If IO.Path.GetExtension(WhereUsedDocument.FullName) = ".dft" Then
                        AllWhereUsedFileNames.Add(WhereUsedDocument.FullName)
                    End If
                End If
                'Finds the next WhereUsed document, if any.
                WhereUsedDocument = CType(DMApp.FindWhereUsed(), DesignManager.Document)
            End While

        End If

        Return AllWhereUsedFileNames

    End Function

    Private Function GetCorrectedFilenameBottomUp(Filename As String,
                                                  AllFilenames As Dictionary(Of String, String)) As String
        Dim CorrectedFilename As String = ""

        ' The link (Filename) may be pointing outside the top level folder
        Try
            CorrectedFilename = AllFilenames(Filename.ToLower)
        Catch ex As Exception
        End Try

        Return CorrectedFilename
    End Function



    Private Sub UpdateStatus(Description As String, Filename As String, TopLevelFolder As String)
        Dim msg As String

        msg = Filename.Replace(TopLevelFolder, "")
        msg = String.Format("{0} {1}", Description, msg)

        Form1.TextBoxStatus.Text = msg
    End Sub

    Private Function CheckInterruptRequest() As Boolean
        Dim tf As Boolean = False

        System.Windows.Forms.Application.DoEvents()
        If Form1.StopProcess Then
            Form1.TextBoxStatus.Text = "Processing aborted"
            tf = True
        End If

        Return tf
    End Function

End Class
