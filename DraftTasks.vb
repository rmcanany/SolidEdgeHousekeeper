Option Strict On

Imports SolidEdgeCommunity

Public Class DraftTasks
    Inherits IsolatedTaskProxy


    Public Function DrawingViewsMissingFile(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeDraft.DraftDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf DrawingViewsMissingFileInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function DrawingViewsMissingFileInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim ModelLinks As SolidEdgeDraft.ModelLinks = Nothing
        Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing

        ModelLinks = SEDoc.ModelLinks

        For Each ModelLink In ModelLinks
            If Not FileIO.FileSystem.FileExists(ModelLink.FileName) Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0} not found", TruncateFullPath(ModelLink.FileName, Configuration)))
            End If
        Next

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function DrawingViewsOutOfDate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeDraft.DraftDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf DrawingViewsOutOfDateInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function DrawingViewsOutOfDateInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Sections As SolidEdgeDraft.Sections = Nothing
        Dim Section As SolidEdgeDraft.Section = Nothing
        Dim SectionSheets As SolidEdgeDraft.SectionSheets = Nothing
        Dim Sheet As SolidEdgeDraft.Sheet = Nothing
        Dim DrawingViews As SolidEdgeDraft.DrawingViews = Nothing
        Dim DrawingView As SolidEdgeDraft.DrawingView = Nothing
        Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing

        Dim PartsLists As SolidEdgeDraft.PartsLists
        Dim PartsList As SolidEdgeDraft.PartsList

        PartsLists = SEDoc.PartsLists

        ' Not all draft files have PartsLists
        Try
            For Each PartsList In PartsLists
                If Not PartsList.IsUpToDate Then
                    ExitStatus = 1
                    Exit For
                End If
            Next
        Catch ex As Exception
        End Try

        Sections = SEDoc.Sections
        Section = Sections.WorkingSection
        SectionSheets = Section.Sheets

        For Each Sheet In SectionSheets.OfType(Of SolidEdgeDraft.Sheet)()
            DrawingViews = Sheet.DrawingViews
            For Each DrawingView In DrawingViews.OfType(Of SolidEdgeDraft.DrawingView)()
                If Not DrawingView.IsUpToDate Then
                    ExitStatus = 1
                    Exit For
                End If
                ' Some drawing views do not have a ModelLink
                Try
                    If DrawingView.ModelLink IsNot Nothing Then
                        ModelLink = CType(DrawingView.ModelLink, SolidEdgeDraft.ModelLink)
                        If ModelLink.ModelOutOfDate Then
                            ExitStatus = 1
                            Exit For
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next DrawingView
            If ExitStatus = 1 Then
                Exit For
            End If
        Next Sheet

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function DetachedDimensionsOrAnnotations(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeDraft.DraftDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf DetachedDimensionsOrAnnotationsInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function DetachedDimensionsOrAnnotationsInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim msg As String = ""
        Dim DimensionErrorConstants As String = "125"
        Dim TF As Boolean

        Dim Variables As SolidEdgeFramework.Variables = Nothing
        Dim VariableList As SolidEdgeFramework.VariableList = Nothing
        Dim Variable As SolidEdgeFramework.variable = Nothing
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension = Nothing

        Dim Balloons As SolidEdgeFrameworkSupport.Balloons
        Dim Balloon As SolidEdgeFrameworkSupport.Balloon

        Dim Sections As SolidEdgeDraft.Sections = Nothing
        Dim Section As SolidEdgeDraft.Section = Nothing
        Dim SectionSheets As SolidEdgeDraft.SectionSheets = Nothing
        Dim Sheet As SolidEdgeDraft.Sheet = Nothing
        Dim DrawingViews As SolidEdgeDraft.DrawingViews = Nothing
        Dim DrawingView As SolidEdgeDraft.DrawingView = Nothing

        Sections = SEDoc.Sections
        Section = Sections.WorkingSection
        SectionSheets = Section.Sheets

        ' Callouts are 'Balloons' in Solid Edge.
        For Each Sheet In SectionSheets.OfType(Of SolidEdgeDraft.Sheet)()
            Balloons = CType(Sheet.Balloons, SolidEdgeFrameworkSupport.Balloons)
            For Each Balloon In Balloons
                'Doesn't always work
                Try
                    'TF = Not Balloon.IsTerminatorAttachedToEntity
                    'TF = TF And Balloon.VertexCount > 0
                    If Balloon.Leader Then
                        If Not Balloon.IsTerminatorAttachedToEntity Then
                            ExitStatus = 1
                            ErrorMessageList.Add(Balloon.BalloonDisplayedText)
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next Balloon
        Next Sheet

        Variables = DirectCast(SEDoc.Variables, SolidEdgeFramework.Variables)

        ' Get a reference to the variablelist.
        VariableList = DirectCast(Variables.Query(pFindCriterium:="*",
                                  NamedBy:=SolidEdgeConstants.VariableNameBy.seVariableNameByBoth,
                                  VarType:=SolidEdgeConstants.VariableVarType.SeVariableVarTypeBoth),
                                  SolidEdgeFramework.VariableList)

        ' Process variables.
        For Each VariableListItem In VariableList.OfType(Of Object)()
            Dim VariableListItemType = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(
                Of SolidEdgeFramework.ObjectType)(VariableListItem, "Type", CType(0, SolidEdgeFramework.ObjectType))

            If VariableListItemType = SolidEdgeFramework.ObjectType.igDimension Then
                Dimension = DirectCast(VariableListItem, SolidEdgeFrameworkSupport.Dimension)
                TF = Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seDimStatusDetached
                TF = TF Or Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seDimStatusError
                TF = TF Or Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seOneEndDetached

                If TF Then
                    ExitStatus = 1
                    ErrorMessageList.Add(Dimension.DisplayName)
                End If
            End If

        Next

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function FileNameDoesNotMatchModelFilename(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeDraft.DraftDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf FileNameDoesNotMatchModelFilenameInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function FileNameDoesNotMatchModelFilenameInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim ModelLinks As SolidEdgeDraft.ModelLinks = Nothing
        Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing
        Dim ModelLinkFilenames As New List(Of String)
        Dim ModelLinkFilename As String

        Dim PartNumber As String = ""
        Dim PartNumberPropertyFound As Boolean = False
        Dim PartNumberFound As Boolean
        Dim Filename As String

        'Get the bare file name without directory information or extension
        Filename = System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName)

        Dim msg As String = ""

        ModelLinks = SEDoc.ModelLinks

        For Each ModelLink In ModelLinks
            ModelLinkFilenames.Add(System.IO.Path.GetFileNameWithoutExtension(ModelLink.FileName))
        Next

        If ModelLinkFilenames.Count > 0 Then
            For Each ModelLinkFilename In ModelLinkFilenames
                If Filename = ModelLinkFilename Then
                    PartNumberFound = True
                End If
            Next
            If Not PartNumberFound Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Drawing file name '{0}' not the same as any model file name:", Filename))
                For Each ModelLinkFilename In ModelLinkFilenames
                    ErrorMessageList.Add(String.Format("    '{0}'", ModelLinkFilename))
                Next
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function UpdateDrawingViews(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeDraft.DraftDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UpdateDrawingViewsInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UpdateDrawingViewsInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Sections As SolidEdgeDraft.Sections = Nothing
        Dim Section As SolidEdgeDraft.Section = Nothing
        Dim SectionSheets As SolidEdgeDraft.SectionSheets = Nothing
        Dim Sheet As SolidEdgeDraft.Sheet = Nothing
        Dim DrawingViews As SolidEdgeDraft.DrawingViews = Nothing
        Dim DrawingView As SolidEdgeDraft.DrawingView = Nothing

        Sections = SEDoc.Sections
        Section = Sections.WorkingSection
        SectionSheets = Section.Sheets

        Dim ModelLinks As SolidEdgeDraft.ModelLinks = Nothing
        Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing

        Dim UpdatedView As Boolean = False

        Dim PartsLists As SolidEdgeDraft.PartsLists
        Dim PartsList As SolidEdgeDraft.PartsList

        PartsLists = SEDoc.PartsLists

        ' Not all draft files have PartsLists
        Try
            For Each PartsList In PartsLists
                If Not PartsList.IsUpToDate Then
                    PartsList.Update()
                End If
            Next
        Catch ex As Exception
        End Try

        ModelLinks = SEDoc.ModelLinks

        For Each ModelLink In ModelLinks
            If Not FileIO.FileSystem.FileExists(ModelLink.FileName) Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0} not found", TruncateFullPath(ModelLink.FileName, Configuration)))
            End If
        Next

        If Not ExitStatus = 1 Then
            For Each Sheet In SectionSheets.OfType(Of SolidEdgeDraft.Sheet)()
                DrawingViews = Sheet.DrawingViews
                For Each DrawingView In DrawingViews.OfType(Of SolidEdgeDraft.DrawingView)()
                    If Not DrawingView.IsUpToDate Then
                        DrawingView.Update()
                        UpdatedView = True
                    End If
                Next DrawingView
            Next Sheet
            If UpdatedView Then
                If SEDoc.ReadOnly Then
                    ExitStatus = 1
                    ErrorMessageList.Add("Cannot save document marked 'Read Only'")
                Else
                    SEDoc.Save()
                    SEApp.DoIdle()
                End If
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function UpdateDrawingBorderFromTemplate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeDraft.DraftDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UpdateDrawingBorderFromTemplateInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UpdateDrawingBorderFromTemplateInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TemplateFilename As String = Configuration("TextBoxTemplateDraft")
        Dim SETemplateDoc As SolidEdgeDraft.DraftDocument
        Dim Sections As SolidEdgeDraft.Sections
        Dim Section As SolidEdgeDraft.Section
        Dim SectionSheets As SolidEdgeDraft.SectionSheets
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim TemplateSheetNames As New List(Of String)

        SETemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgeDraft.DraftDocument)
        SEApp.DoIdle()

        Sections = SETemplateDoc.Sections
        Section = Sections.BackgroundSection
        SectionSheets = Section.Sheets

        For Each Sheet In SectionSheets
            TemplateSheetNames.Add(Sheet.Name)
        Next

        SETemplateDoc.Close()
        SEApp.DoIdle()

        Sections = SEDoc.Sections
        Section = Sections.BackgroundSection
        SectionSheets = Section.Sheets

        For Each Sheet In SectionSheets
            If TemplateSheetNames.Contains(Sheet.Name) Then
                Sheet.ReplaceBackground(TemplateFilename, Sheet.Name)
            Else
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Template has no background named '{0}'", Sheet.Name))
            End If
        Next

        If ExitStatus = 0 Then
            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function UpdateDimensionStylesFromTemplate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeDraft.DraftDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UpdateDimensionStylesFromTemplateInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UpdateDimensionStylesFromTemplateInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TemplateFilename As String = Configuration("TextBoxTemplateDraft")
        Dim SETemplateDoc As SolidEdgeDraft.DraftDocument
        Dim DimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles
        Dim DocDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles
        Dim DimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle
        Dim ActiveDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle = Nothing
        Dim PrimaryDecimalRoundOffThisDimension As SolidEdgeFrameworkSupport.DimDecimalRoundOffTypeConstants
        Dim TextHeightThisDimension As Double
        Dim Dimensions As SolidEdgeFrameworkSupport.Dimensions = Nothing
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension = Nothing

        Dim Balloons As SolidEdgeFrameworkSupport.Balloons
        Dim Balloon As SolidEdgeFrameworkSupport.Balloon

        Dim Sections As SolidEdgeDraft.Sections = Nothing
        Dim Section As SolidEdgeDraft.Section = Nothing
        Dim SectionSheets As SolidEdgeDraft.SectionSheets = Nothing
        Dim Sheet As SolidEdgeDraft.Sheet = Nothing

        'Copy DimensionStyles from template
        SETemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgeDraft.DraftDocument)
        SEApp.DoIdle()

        DimensionStyles = CType(SETemplateDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)
        DocDimensionStyles = CType(SEDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)

        For Each DimensionStyle In DimensionStyles
            DocDimensionStyles.AddEx(DimensionStyle.Name, True, SETemplateDoc)
        Next

        SETemplateDoc.Close()
        SEApp.DoIdle()

        'Dimensions and callouts with overrides are not automatically updated to the new style.
        Sections = SEDoc.Sections

        For Each Section In Sections
            SectionSheets = Section.Sheets
            For Each Sheet In SectionSheets
                Dimensions = CType(Sheet.Dimensions, SolidEdgeFrameworkSupport.Dimensions)
                For Each Dimension In Dimensions
                    For Each DimensionStyle In DocDimensionStyles
                        If DimensionStyle.Name = Dimension.Style.Name Then
                            ActiveDimensionStyle = DimensionStyle
                        End If
                    Next
                    If Dimension.Style.PrimaryDecimalRoundOff <> ActiveDimensionStyle.PrimaryDecimalRoundOff Then
                        PrimaryDecimalRoundOffThisDimension = Dimension.Style.PrimaryDecimalRoundOff
                        Dimension.Style.Name = ActiveDimensionStyle.Name
                        Dimension.Style.PrimaryDecimalRoundOff = PrimaryDecimalRoundOffThisDimension
                    End If
                Next
            Next
        Next

        Section = Nothing
        SectionSheets = Nothing
        Sheet = Nothing
        SEApp.DoIdle()

        For Each Section In Sections
            SectionSheets = Section.Sheets
            For Each Sheet In SectionSheets
                Balloons = CType(Sheet.Balloons, SolidEdgeFrameworkSupport.Balloons)
                For Each Balloon In Balloons
                    For Each DimensionStyle In DocDimensionStyles
                        If DimensionStyle.Name = Balloon.Style.Name Then
                            ActiveDimensionStyle = DimensionStyle
                        End If
                    Next
                    If Balloon.Style.Height <> ActiveDimensionStyle.Height Then
                        TextHeightThisDimension = Balloon.Style.Height
                        Balloon.Style.Name = ActiveDimensionStyle.Name
                        Balloon.Style.Height = TextHeightThisDimension
                    Else
                        Balloon.Style.Name = ActiveDimensionStyle.Name
                    End If
                Next
            Next
        Next

        Section = Nothing
        SectionSheets = Nothing
        Sheet = Nothing
        SEApp.DoIdle()

        Section = Sections.WorkingSection

        SEApp.DoIdle()

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function MoveDrawingToNewTemplate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeDraft.DraftDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf MoveDrawingToNewTemplateInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function MoveDrawingToNewTemplateInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim CheckAutoGeneratedFile As Func(Of SolidEdgeDraft.DraftDocument, Boolean)
        CheckAutoGeneratedFile = Function(SourceDoc)
                                     Dim Filename As String = System.IO.Path.GetFileName(SourceDoc.FullName)

                                     If Filename.Contains("-HousekeeperOld") Then
                                         ExitStatus = 1
                                         ErrorMessageList.Add(String.Format("Auto-generated file not processed {0}", Filename))
                                         Return True
                                     Else
                                         Return False
                                     End If
                                 End Function

        Dim GetNewDocFilename As Func(Of SolidEdgeDraft.DraftDocument, String)
        GetNewDocFilename = Function(SourceDoc)
                                Dim NewFilename As String
                                NewFilename = String.Format("{0}\{1}-Housekeeper.dft",
                                                            System.IO.Path.GetDirectoryName(SEDoc.FullName),
                                                            System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))
                                Return NewFilename
                            End Function

        Dim GetRemnantsDocFilename As Func(Of SolidEdgeDraft.DraftDocument, String)
        GetRemnantsDocFilename = Function(SourceDoc)
                                     Dim NewFilename As String
                                     NewFilename = String.Format("{0}\{1}-HousekeeperOld.dft",
                                                                 System.IO.Path.GetDirectoryName(SEDoc.FullName),
                                                                 System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))
                                     Return NewFilename
                                 End Function

        Dim GetSheets As Func(Of SolidEdgeDraft.DraftDocument, String, List(Of SolidEdgeDraft.Sheet))
        GetSheets = Function(Doc, SectionType)
                        Dim SheetList As New List(Of SolidEdgeDraft.Sheet)
                        Dim Sheet As SolidEdgeDraft.Sheet
                        Dim Section As SolidEdgeDraft.Section = Nothing
                        Dim SectionSheets As SolidEdgeDraft.SectionSheets
                        Dim SheetGroups As SolidEdgeDraft.SheetGroups
                        Dim SheetGroup As SolidEdgeDraft.SheetGroup

                        Dim count As Integer

                        If SectionType = "Working" Then
                            Section = Doc.Sections.WorkingSection
                        ElseIf SectionType = "Background" Then
                            Section = Doc.Sections.BackgroundSection
                        ElseIf SectionType = "2DModel" Then
                            Section = Doc.Sections.WorkingSection  ' Ignored below
                        ElseIf SectionType = "UserGenerated" Then
                            Section = Doc.Sections.WorkingSection
                            SheetGroups = CType(Doc.SheetGroups, SolidEdgeDraft.SheetGroups)
                        ElseIf SectionType = "AutoGenerated" Then
                            Section = Doc.Sections.WorkingSection
                            SheetGroups = CType(Doc.SheetGroups, SolidEdgeDraft.SheetGroups)
                        Else
                            MsgBox(String.Format("SectionType '{0}' not recognized.  Quitting...", SectionType))
                        End If

                        SectionSheets = Section.Sheets

                        If (SectionType = "Working") Or (SectionType = "Background") Then
                            For Each Sheet In SectionSheets.OfType(Of SolidEdgeDraft.Sheet)()
                                SheetList.Add(Sheet)
                            Next
                        ElseIf (SectionType = "2DModel") Then
                            SheetList.Add(Doc.Sections.Get2DModelSheet)
                        Else
                            SheetGroups = CType(Doc.SheetGroups, SolidEdgeDraft.SheetGroups)
                            count = 0
                            For Each SheetGroup In SheetGroups
                                For Each Sheet In SheetGroup.Sheets.OfType(Of SolidEdgeDraft.Sheet)()
                                    If (SectionType = "UserGenerated") And (count = 0) Then
                                        SheetList.Add(Sheet)
                                    End If
                                    If (SectionType = "AutoGenerated") And (count > 0) Then
                                        SheetList.Add(Sheet)
                                    End If
                                Next
                                count += 1
                            Next
                        End If

                        Return SheetList
                    End Function

        Dim SheetNameToObject As Func(Of SolidEdgeDraft.DraftDocument, String, String, SolidEdgeDraft.Sheet)
        SheetNameToObject = Function(Doc, SectionType, SheetName)
                                Dim Sheet As SolidEdgeDraft.Sheet

                                For Each Sheet In GetSheets(Doc, SectionType)
                                    If Sheet.Name = SheetName Then
                                        Return Sheet
                                    End If
                                Next
                                Return Nothing
                            End Function

        Dim AddSheet As Action(Of SolidEdgeDraft.DraftDocument, String)
        AddSheet = Sub(Doc, SheetName)
                       Dim Sheet As SolidEdgeDraft.Sheet
                       Dim SheetAlreadyExists As Boolean = False

                       For Each Sheet In GetSheets(Doc, "Working")
                           If SheetName = Sheet.Name Then
                               SheetAlreadyExists = True
                           End If
                       Next

                       If Not SheetAlreadyExists Then
                           Doc.Sheets.AddSheet(SheetName, SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection)
                       End If
                   End Sub

        Dim SetTargetBackgrounds As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument, String)
        SetTargetBackgrounds = Sub(SourceDoc, TargetDoc, DummyName)
                                   Dim SourceSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "Working")
                                   Dim TargetSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(TargetDoc, "Working")

                                   Dim SourceSheet As SolidEdgeDraft.Sheet
                                   Dim TargetSheet As SolidEdgeDraft.Sheet

                                   Dim SourceSheetNames As New List(Of String)
                                   Dim TargetSheetNames As New List(Of String)

                                   Dim SourceBackgroundSheet As SolidEdgeDraft.Sheet
                                   Dim TargetBackgroundSheet As SolidEdgeDraft.Sheet

                                   Dim msg2 As String = ""

                                   For Each TargetSheet In TargetSheets
                                       If Not TargetSheet.Name = DummyName Then
                                           SourceSheet = SheetNameToObject(SourceDoc, "Working", TargetSheet.Name)
                                           ' SourceSheetNames.Add(SourceSheet.Name)
                                           ' Not all sheets have a background defined
                                           Try
                                               SourceBackgroundSheet = SourceSheet.Background
                                               TargetBackgroundSheet = SheetNameToObject(TargetDoc, "Background", SourceBackgroundSheet.Name)
                                               If Not TargetBackgroundSheet Is Nothing Then
                                                   TargetSheet.Background = TargetBackgroundSheet
                                                   TargetSheet.BackgroundVisible = SourceSheet.BackgroundVisible
                                                   TargetSheet.SheetSetup.SheetSizeOption = SourceSheet.SheetSetup.SheetSizeOption
                                               Else
                                                   ExitStatus = 1
                                                   msg2 = String.Format("Template does not have a background named '{0}'", SourceBackgroundSheet.Name)
                                                   If Not ErrorMessageList.Contains(msg2) Then
                                                       ErrorMessageList.Add(msg2)
                                                   End If
                                               End If
                                           Catch ex As Exception
                                           End Try
                                       End If
                                   Next
                               End Sub

        Dim AddSheetsToTarget As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument, String)
        AddSheetsToTarget = Sub(SourceDoc, TargetDoc, DummyName)
                                ' Add sheets to target to match source.
                                ' Remove sheets from target that don't match.
                                ' Set backgrounds to match.  Report to log if target does not have the background sheet.

                                Dim SourceSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "UserGenerated")
                                Dim TargetSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(TargetDoc, "UserGenerated")

                                Dim SourceSheet As SolidEdgeDraft.Sheet
                                Dim TargetSheet As SolidEdgeDraft.Sheet

                                Dim SourceSheetNames As New List(Of String)
                                Dim TargetSheetNames As New List(Of String)

                                For Each SourceSheet In SourceSheets
                                    SourceSheetNames.Add(SourceSheet.Name)
                                Next

                                For Each TargetSheet In TargetSheets
                                    TargetSheetNames.Add(TargetSheet.Name)
                                Next

                                For Each SourceSheet In SourceSheets
                                    If Not TargetSheetNames.Contains(SourceSheet.Name) Then
                                        AddSheet(TargetDoc, SourceSheet.Name)
                                    End If
                                Next

                                For Each TargetSheetName In TargetSheetNames
                                    If Not SourceSheetNames.Contains(TargetSheetName) Then
                                        SheetNameToObject(TargetDoc, "Working", TargetSheetName).Delete()
                                    End If
                                Next

                                SetTargetBackgrounds(SourceDoc, TargetDoc, DummyName)

                            End Sub

        Dim MoveDVsToDummySheet As Func(Of SolidEdgeDraft.DraftDocument, String, List(Of String))
        MoveDVsToDummySheet = Function(SourceDoc, SourceDocDummySheetName)
                                  Dim DVSheetNames As New List(Of String)
                                  Dim Sheet As SolidEdgeDraft.Sheet
                                  Dim DrawingView As SolidEdgeDraft.DrawingView
                                  Dim TargetSheet As SolidEdgeDraft.Sheet = SheetNameToObject(SourceDoc, "UserGenerated", SourceDocDummySheetName)
                                  Dim msg2 As String

                                  For Each Sheet In GetSheets(SourceDoc, "UserGenerated")
                                      If Not Sheet.Name = SourceDocDummySheetName Then
                                          For Each DrawingView In Sheet.DrawingViews
                                              DVSheetNames.Add(Sheet.Name)
                                              Dim Name As String = DrawingView.Name
                                              ' Issue with broken out section view
                                              Try
                                                  DrawingView.Sheet = TargetSheet
                                              Catch ex As Exception
                                                  ExitStatus = 1
                                                  msg2 = "Some drawing views may not have transferred"
                                                  If Not ErrorMessageList.Contains(msg2) Then
                                                      ErrorMessageList.Add(msg2)
                                                  End If
                                              End Try
                                          Next
                                      End If
                                  Next

                                  Return DVSheetNames
                              End Function


        Dim MoveDVsToTarget As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument, List(Of String), String)
        MoveDVsToTarget = Sub(SourceDoc, TargetDoc, DrawingViewSheetnames, DummyName)


                              Dim SourceSheet As SolidEdgeDraft.Sheet = SheetNameToObject(SourceDoc, "UserGenerated", DummyName)
                              Dim TargetSheet As SolidEdgeDraft.Sheet = SheetNameToObject(TargetDoc, "UserGenerated", DummyName)
                              Dim SheetWindow As SolidEdgeDraft.SheetWindow
                              Dim DrawingView As SolidEdgeDraft.DrawingView

                              If DrawingViewSheetnames.Count > 0 Then
                                  ' Sometimes get cut/paste error on drawing views.  Need a do-over.  Don't save anything.
                                  Try
                                      SourceDoc.Activate()
                                      SourceSheet.Activate()
                                      SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

                                      For Each DrawingView In SourceSheet.DrawingViews
                                          DrawingView.Select()
                                          DrawingView.AddConnectedAnnotationsToSelectSet()
                                          DrawingView.AddConnectedDimensionsToSelectSet()
                                      Next

                                      SEApp.DoIdle()
                                      SheetWindow.Cut()
                                      SEApp.DoIdle()

                                      TargetDoc.Activate()
                                      TargetSheet.Activate()
                                      SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
                                      SheetWindow.Paste()
                                      SEApp.DoIdle()
                                  Catch ex As Exception
                                      ExitStatus = 2
                                      Exit Sub
                                  End Try

                              End If

                          End Sub

        Dim MoveDVsToCorrectSheet As Action(Of SolidEdgeDraft.DraftDocument, List(Of String), String)
        MoveDVsToCorrectSheet = Sub(TargetDoc, DrawingViewSheetNames, DummyName)

                                    Dim DrawingView As SolidEdgeDraft.DrawingView

                                    Dim DummySheet As SolidEdgeDraft.Sheet = SheetNameToObject(TargetDoc, "UserGenerated", DummyName)
                                    Dim count As Integer = 0
                                    Dim msg2 As String
                                    Dim tf2 As Boolean

                                    If DrawingViewSheetNames.Count > 0 Then
                                        TargetDoc.Activate()

                                        For Each DrawingView In DummySheet.DrawingViews
                                            Dim Name As String = DrawingView.Name

                                            tf2 = DrawingView.IsBrokenOutSectionTarget
                                            ' Issue with broken out section view
                                            Try
                                                DrawingView.Sheet = SheetNameToObject(TargetDoc, "UserGenerated", DrawingViewSheetNames(count))
                                            Catch ex As Exception
                                                ExitStatus = 1
                                                msg2 = "Some drawing views may not have transferred"
                                                If Not ErrorMessageList.Contains(msg2) Then
                                                    ErrorMessageList.Add(msg2)
                                                End If
                                            End Try
                                            count += 1
                                        Next
                                    End If

                                End Sub

        Dim UpdateRange As Func(Of Double, Double, Double, Double, List(Of Double), List(Of Double))
        UpdateRange = Function(XMin, YMin, XMax, YMax, Ranges)
                          Dim NewRanges As New List(Of Double)

                          If XMin < Ranges(0) Then
                              NewRanges.Add(XMin)
                          Else
                              NewRanges.Add(Ranges(0))
                          End If

                          If YMin < Ranges(1) Then
                              NewRanges.Add(YMin)
                          Else
                              NewRanges.Add(Ranges(1))
                          End If

                          If XMax > Ranges(2) Then
                              NewRanges.Add(XMax)
                          Else
                              NewRanges.Add(Ranges(2))
                          End If

                          If YMax > Ranges(3) Then
                              NewRanges.Add(YMax)
                          Else
                              NewRanges.Add(Ranges(3))
                          End If

                          Return NewRanges
                      End Function

        Dim GetCenter As Func(Of SolidEdgeDraft.DrawingView, List(Of Double))
        GetCenter = Function(DrawingView)
                        Dim Center As New List(Of Double)
                        Dim CenterX As Double
                        Dim CenterY As Double

                        DrawingView.GetOrigin(CenterX, CenterY)
                        Center.Add(CenterX)
                        Center.Add(CenterY)
                        Return Center
                    End Function

        Dim CloseEnough As Func(Of Double, Double, Double, Boolean)
        CloseEnough = Function(D1, D2, MaxDiff)
                          If Math.Abs(D1 - D2) < MaxDiff Then
                              Return True
                          Else
                              Return False
                          End If

                      End Function

        Dim GetAlignedDVs As Func(Of SolidEdgeDraft.Sheet, Dictionary(Of SolidEdgeDraft.DrawingView, List(Of Double)), List(Of SolidEdgeDraft.DrawingView))
        GetAlignedDVs = Function(Sheet, DVCenters)
                            Dim AlignedDVs As New List(Of SolidEdgeDraft.DrawingView)
                            Dim DrawingView As SolidEdgeDraft.DrawingView
                            Dim DVList As New List(Of SolidEdgeDraft.DrawingView)
                            Dim DVX As New Dictionary(Of Integer, Double)
                            Dim DVY As New Dictionary(Of Integer, Double)
                            Dim SortedDictionary As New Dictionary(Of Integer, Double)
                            Dim XOrder As New List(Of Integer)
                            Dim YOrder As New List(Of Integer)

                            Dim count As Integer
                            Dim i As Integer
                            Dim x1 As Double
                            Dim x2 As Double
                            Dim y1 As Double
                            Dim y2 As Double

                            count = 0
                            For Each DrawingView In Sheet.DrawingViews
                                DVList.Add(DrawingView)
                                DVX(count) = DVCenters(DrawingView)(0)
                                DVY(count) = DVCenters(DrawingView)(1)
                                count += 1
                            Next

                            Dim XSorted = From pair In DVX
                                          Order By pair.Value
                            SortedDictionary = XSorted.ToDictionary(Function(p) p.Key, Function(p) p.Value)
                            XOrder = SortedDictionary.Keys.ToList

                            Dim YSorted = From pair In DVY
                                          Order By pair.Value
                            SortedDictionary = YSorted.ToDictionary(Function(p) p.Key, Function(p) p.Value)
                            YOrder = SortedDictionary.Keys.ToList

                            For i = 1 To XOrder.Count - 1
                                x1 = DVCenters(DVList(XOrder(i - 1)))(0)
                                x2 = DVCenters(DVList(XOrder(i)))(0)
                                If CloseEnough(x1, x2, 0.000001) Then
                                    AlignedDVs.Add(DVList(XOrder(i - 1)))
                                    AlignedDVs.Add(DVList(XOrder(i)))
                                End If
                            Next

                            For i = 1 To YOrder.Count - 1
                                y1 = DVCenters(DVList(YOrder(i - 1)))(1)
                                y2 = DVCenters(DVList(YOrder(i)))(1)
                                If CloseEnough(y1, y2, 0.000001) Then
                                    AlignedDVs.Add(DVList(YOrder(i - 1)))
                                    AlignedDVs.Add(DVList(YOrder(i)))
                                End If
                            Next

                            AlignedDVs = AlignedDVs.Distinct.ToList

                            Return AlignedDVs
                        End Function

        Dim AlignSheetViews As Action(Of SolidEdgeDraft.Sheet)
        AlignSheetViews = Sub(Sheet)
                              Dim DrawingView As SolidEdgeDraft.DrawingView
                              Dim DVCenters As New Dictionary(Of SolidEdgeDraft.DrawingView, List(Of Double))
                              Dim DVX As New Dictionary(Of Integer, Double)
                              Dim DVY As New Dictionary(Of Integer, Double)
                              Dim AlignedDrawingViews As New List(Of SolidEdgeDraft.DrawingView)
                              Dim SheetWindow As SolidEdgeDraft.SheetWindow
                              Dim SelectSet As SolidEdgeFramework.SelectSet

                              If Sheet.DrawingViews.Count > 1 Then
                                  For Each DrawingView In Sheet.DrawingViews
                                      DVCenters(DrawingView) = GetCenter(DrawingView)
                                  Next

                                  AlignedDrawingViews = GetAlignedDVs(Sheet, DVCenters)

                                  If AlignedDrawingViews.Count > 1 Then
                                      Sheet.Activate()
                                      SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
                                      SelectSet = SheetWindow.SelectSet

                                      For Each DrawingView In AlignedDrawingViews
                                          SelectSet.Add(DrawingView)
                                      Next

                                      SEApp.StartCommand(
                                          CType(SolidEdgeConstants.DetailCommandConstants.DetailRelationshipsAlignViews,
                                                SolidEdgeFramework.SolidEdgeCommandConstants))
                                  End If

                              End If

                          End Sub


        Dim AlignViews As Action(Of SolidEdgeDraft.DraftDocument)
        AlignViews = Sub(TargetDoc)
                         Dim Sheet As SolidEdgeDraft.Sheet

                         For Each Sheet In GetSheets(TargetDoc, "UserGenerated")
                             AlignSheetViews(Sheet)
                         Next
                     End Sub

        Dim Move2DModelsToTarget As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument)
        Move2DModelsToTarget = Sub(SourceDoc, TargetDoc)
                                   Dim SheetWindow As SolidEdgeDraft.SheetWindow
                                   'Dim Sections As SolidEdgeDraft.Sections

                                   Dim Source2DSheet As SolidEdgeDraft.Sheet = GetSheets(SourceDoc, "2DModel")(0)
                                   Dim Target2DSheet As SolidEdgeDraft.Sheet = GetSheets(TargetDoc, "2DModel")(0)

                                   Dim DrawingObjects As SolidEdgeFrameworkSupport.DrawingObjects
                                   Dim SelectSet As SolidEdgeFramework.SelectSet

                                   DrawingObjects = Source2DSheet.DrawingObjects

                                   If DrawingObjects.Count > 0 Then
                                       Try
                                           SourceDoc.Activate()
                                           Source2DSheet.Activate()
                                           SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
                                           SelectSet = SheetWindow.SelectSet

                                           For Each DrawingObject In DrawingObjects
                                               SelectSet.Add(DrawingObject)
                                           Next

                                           SEApp.DoIdle()
                                           SheetWindow.Cut()
                                           SEApp.DoIdle()
                                           SheetWindow.Display2DModelSheetTab = False
                                           SheetWindow.DisplayBackgroundSheetTabs = False

                                           TargetDoc.Activate()
                                           Target2DSheet.Activate()
                                           SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

                                           SheetWindow.Paste()
                                           SEApp.DoIdle()
                                           SheetWindow.Display2DModelSheetTab = False
                                           SheetWindow.DisplayBackgroundSheetTabs = False

                                       Catch ex As Exception
                                           ExitStatus = 2
                                           Exit Sub
                                       End Try
                                   End If

                               End Sub

        Dim MoveRemainingItemsToTarget As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument)
        MoveRemainingItemsToTarget = Sub(SourceDoc, TargetDoc)
                                         Dim SheetWindow As SolidEdgeDraft.SheetWindow
                                         Dim SourceSheet As SolidEdgeDraft.Sheet
                                         Dim TargetSheet As SolidEdgeDraft.Sheet
                                         Dim DrawingObjects As SolidEdgeFrameworkSupport.DrawingObjects
                                         Dim SelectSet As SolidEdgeFramework.SelectSet

                                         Dim msg2 As String = ""

                                         For Each SourceSheet In GetSheets(SourceDoc, "UserGenerated")
                                             If SourceSheet.DrawingObjects.Count > 0 Then
                                                 SourceDoc.Activate()
                                                 SourceSheet.Activate()
                                                 SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
                                                 SheetWindow.FitEx(SolidEdgeDraft.SheetFitConstants.igFitAll)
                                                 SelectSet = SheetWindow.SelectSet

                                                 DrawingObjects = SourceSheet.DrawingObjects
                                                 For Each DrawingObject In DrawingObjects
                                                     SelectSet.Add(DrawingObject)
                                                 Next
                                                 ' Catch copy/paste problem
                                                 Try
                                                     SEApp.DoIdle()
                                                     SheetWindow.Cut()
                                                     SEApp.DoIdle()

                                                     TargetDoc.Activate()
                                                     TargetSheet = SheetNameToObject(TargetDoc, "UserGenerated", SourceSheet.Name)
                                                     TargetSheet.Activate()
                                                     SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

                                                     SheetWindow.Paste()
                                                     SEApp.DoIdle()

                                                 Catch ex As Exception
                                                     ExitStatus = 2
                                                     Exit Sub
                                                 End Try
                                             End If
                                         Next

                                     End Sub

        Dim CheckAutoGeneratedSheets As Action(Of SolidEdgeDraft.DraftDocument)
        CheckAutoGeneratedSheets = Sub(SourceDoc)
                                       Dim AutoGeneratedSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "AutoGenerated")
                                       Dim msg2 As String = ""

                                       If AutoGeneratedSheets.Count > 0 Then
                                           ExitStatus = 1
                                           ErrorMessageList.Add("Auto generated sheets not processed.  Please regenerate.")
                                           For Each Sheet In AutoGeneratedSheets
                                               msg2 += String.Format("{0}, ", Sheet.Name)
                                           Next
                                           ErrorMessageList.Add(msg2)
                                       End If
                                   End Sub

        Dim CheckDVsOnBackground As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument, String)
        CheckDVsOnBackground = Sub(SourceDoc, TargetDoc, DummyName)
                                   Dim BackgroundSheet As SolidEdgeDraft.Sheet
                                   Dim SheetWindow As SolidEdgeDraft.SheetWindow
                                   Dim DrawingView As SolidEdgeDraft.DrawingView
                                   Dim TargetSheet As SolidEdgeDraft.Sheet = SheetNameToObject(TargetDoc, "UserGenerated", DummyName)

                                   Dim msg2 As String = ""

                                   For Each BackgroundSheet In GetSheets(SourceDoc, "Background")
                                       If BackgroundSheet.DrawingViews.Count > 0 Then
                                           ExitStatus = 1
                                           msg2 = String.Format("Drawing view found on background '{0}'.  Moved to sheet '{1}'.", BackgroundSheet.Name, DummyName)
                                           ErrorMessageList.Add(msg2)
                                           msg2 = "Verify all items were transferred from "
                                           msg2 += String.Format("{0}", System.IO.Path.GetFileName(GetRemnantsDocFilename(SEDoc)))
                                           ErrorMessageList.Add(String.Format("    {0}", msg2))

                                           SourceDoc.Activate()
                                           BackgroundSheet.Activate()
                                           SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

                                           For Each DrawingView In BackgroundSheet.DrawingViews
                                               DrawingView.Select()
                                               DrawingView.AddConnectedAnnotationsToSelectSet()
                                               DrawingView.AddConnectedDimensionsToSelectSet()
                                           Next

                                           ' Catch copy/paste problems
                                           Try
                                               SheetWindow.Cut()

                                               TargetDoc.Activate()
                                               TargetSheet.Activate()
                                               SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
                                               SheetWindow.Paste()

                                           Catch ex As Exception
                                               ExitStatus = 2
                                               Exit Sub
                                           End Try

                                       End If
                                   Next
                               End Sub

        Dim CheckForOrphanedItems As Action(Of SolidEdgeDraft.DraftDocument, String)
        CheckForOrphanedItems = Sub(TargetDoc, DummyName)
                                    Dim TargetSheet As SolidEdgeDraft.Sheet = SheetNameToObject(TargetDoc, "UserGenerated", DummyName)
                                    Dim ContainsSubstring As Boolean = False
                                    Dim s As String

                                    TargetDoc.Activate()

                                    If TargetSheet.DrawingObjects.Count > 0 Then
                                        ExitStatus = 1
                                        For Each s In ErrorMessageList
                                            If s.Contains("Drawing view found on sheet") Then
                                                ContainsSubstring = True
                                            End If
                                        Next
                                        If Not ContainsSubstring Then
                                            ErrorMessageList.Add(String.Format("Orphaned items on sheet '{0}'", DummyName))
                                            ErrorMessageList.Add(String.Format(
                                                             "    Please transfer to the correct sheet, then delete '{0}'", DummyName))
                                        End If
                                    Else
                                        TargetDoc.Sheets.Item(1).Activate()
                                        TargetSheet.Delete()
                                    End If

                                End Sub

        Dim TidyUp As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument, String)
        TidyUp = Sub(SourceDoc, TargetDoc, DummyName)
                     Dim SourceSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "UserGenerated")
                     Dim TargetSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(TargetDoc, "UserGenerated")
                     Dim SheetWindow As SolidEdgeDraft.SheetWindow

                     SourceDoc.Activate()
                     SourceSheets(0).Activate()
                     SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
                     SheetWindow.Display2DModelSheetTab = False
                     'SheetWindow.DisplayBackgroundSheetTabs = False

                     For Each SourceSheet In SourceSheets
                         If SourceSheet.Name = DummyName Then
                             SourceSheet.Delete()
                             Exit For
                         End If
                     Next

                     TargetDoc.Activate()
                     For Each TargetSheet In TargetSheets
                         TargetSheet.Activate()
                         SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
                         SheetWindow.Display2DModelSheetTab = False
                         SheetWindow.DisplayBackgroundSheetTabs = False
                         SheetWindow.FitEx(SolidEdgeDraft.SheetFitConstants.igFitSheet)
                     Next
                     TargetSheets(0).Activate()

                 End Sub

        Dim NewDoc As SolidEdgeDraft.DraftDocument

        Dim NewDocFilename As String
        Dim SEDocDVSheetNames As New List(Of String)  ' List of sheet names in order of drawing views

        Dim msg As String = ""
        Dim tf As Boolean

        Dim SEDocUserSheetGroupSheetNames As New List(Of String)
        Dim SEDocAutoSheetGroupSheetNames As New List(Of String)

        Dim DummySheetName As String = "Housekeeper"

        ' Open target document
        NewDocFilename = GetNewDocFilename(SEDoc)
        NewDoc = CType(SEApp.Documents.Add("SolidEdge.DraftDocument",
                                           Configuration("TextBoxTemplateDraft")),
                                           SolidEdgeDraft.DraftDocument)

        ' Make sure the file is not an auto-generated file from previous runs of this task
        If Not CheckAutoGeneratedFile(SEDoc) Then
            ' Create dummy sheet source document
            AddSheet(SEDoc, DummySheetName)

            ' Create new sheets in target.  Delete any sheets in target not in source.  Set background each sheet.
            AddSheetsToTarget(SEDoc, NewDoc, DummySheetName)

            ' Move source drawing views to dummy sheet.  Return original drawing view sheet names
            SEDocDVSheetNames = MoveDVsToDummySheet(SEDoc, DummySheetName)

            ' Move source drawing views to target dummy sheet
            ' If this fails, it sets ExitStatus = 2.  Time to bail.
            MoveDVsToTarget(SEDoc, NewDoc, SEDocDVSheetNames, DummySheetName)

            If Not ExitStatus = 2 Then
                ' Move target drawing views to correct sheets using original drawing view sheet names
                MoveDVsToCorrectSheet(NewDoc, SEDocDVSheetNames, DummySheetName)

                ' Create view alignments that get lost when transferring drawing views to other sheets.
                AlignViews(NewDoc)

                ' Move source 2D models to target
                Move2DModelsToTarget(SEDoc, NewDoc)

                If Not ExitStatus = 2 Then
                    ' Move remaining drawing objects from source to target
                    MoveRemainingItemsToTarget(SEDoc, NewDoc)

                    If Not ExitStatus = 2 Then
                        ' Check if there are auto-generated sheets in source
                        CheckAutoGeneratedSheets(SEDoc)

                        ' Check if there are any drawing views on background sheets.  Move to target dummy sheet
                        CheckDVsOnBackground(SEDoc, NewDoc, DummySheetName)

                        If Not ExitStatus = 2 Then
                            ' Check for orphaned drawing objects on target dummy sheet
                            CheckForOrphanedItems(NewDoc, DummySheetName)

                            ' Tidy up
                            TidyUp(SEDoc, NewDoc, DummySheetName)

                            tf = Configuration("CheckBoxMoveDrawingViewAllowPartialSuccess") = "True"
                            tf = tf And ExitStatus = 1
                            tf = tf Or ExitStatus = 0
                            If tf Then
                                NewDoc.SaveAs(NewDocFilename)
                            End If
                        End If
                    End If
                End If
            End If
        End If

        NewDoc.Close()
        SEApp.DoIdle()

        tf = Configuration("CheckBoxMoveDrawingViewAllowPartialSuccess") = "True"
        tf = tf And ExitStatus = 1

        If tf Then
            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
                msg = String.Format("After correcting issues, please delete {0}", System.IO.Path.GetFileName(GetRemnantsDocFilename(SEDoc)))
                ErrorMessageList.Add(msg)
            End If
        ElseIf (Not tf) Or (ExitStatus = 2) Then
            If Not tf Then
                ErrorMessageList.Add("Partial success, but no changes made.  If desired, change this behavior on the Configuration tab.")
            Else
                ' Botched cut/paste.  Don't save anything.
                ErrorMessageList.Add("Problem with cut/paste.  No changes made.  Please try again or transfer manually.")
            End If

        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function FitView(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeDraft.DraftDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf FitViewInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function FitViewInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim SheetWindow As SolidEdgeDraft.SheetWindow
        Dim Sections As SolidEdgeDraft.Sections = Nothing
        Dim Section As SolidEdgeDraft.Section = Nothing
        Dim SectionSheets As SolidEdgeDraft.SectionSheets = Nothing
        Dim Sheet As SolidEdgeDraft.Sheet = Nothing

        Sections = SEDoc.Sections
        Section = Sections.WorkingSection
        SectionSheets = Section.Sheets

        SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

        For Each Sheet In SectionSheets.OfType(Of SolidEdgeDraft.Sheet)()
            SheetWindow.ActiveSheet = Sheet
            SheetWindow.FitEx(SolidEdgeDraft.SheetFitConstants.igFitSheet)
        Next

        SheetWindow.ActiveSheet = SectionSheets.OfType(Of SolidEdgeDraft.Sheet)().ElementAt(0)

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function SaveAsPDF(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeDraft.DraftDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf SaveAsPDFInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function SaveAsPDFInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim PDFFilename As String = ""
        Dim DraftBaseFilename As String

        DraftBaseFilename = System.IO.Path.GetFileName(SEDoc.FullName)

        ' CheckBoxPdfDraftOutputDirectory
        If Configuration("CheckBoxPdfDraftOutputDirectory") = "False" Then
            PDFFilename = Configuration("TextBoxPdfDraftOutputDirectory") + "\" + System.IO.Path.ChangeExtension(DraftBaseFilename, ".pdf")
        Else
            PDFFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, ".pdf")
        End If

        'Capturing a fault to update ExitStatus
        Try
            SEDoc.SaveAs(PDFFilename)
            SEApp.DoIdle()
        Catch ex As Exception
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Error saving {0}", TruncateFullPath(PDFFilename, Configuration)))
        End Try

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function SaveAsDXF(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeDraft.DraftDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf SaveAsDXFInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function SaveAsDXFInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim DXFFilename As String = ""
        Dim DraftBaseFilename As String

        DraftBaseFilename = System.IO.Path.GetFileName(SEDoc.FullName)

        ' CheckBoxDxfDraftOutputDirectory
        If Configuration("CheckBoxDxfDraftOutputDirectory") = "False" Then
            DXFFilename = Configuration("TextBoxDxfDraftOutputDirectory") + "\" + System.IO.Path.ChangeExtension(DraftBaseFilename, ".dxf")
        Else
            DXFFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, ".dxf")
        End If

        'Capturing a fault to update ExitStatus
        Try
            SEDoc.SaveAs(DXFFilename)
            SEApp.DoIdle()
        Catch ex As Exception
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Error saving {0}", TruncateFullPath(DXFFilename, Configuration)))
        End Try

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Private Function TruncateFullPath(ByVal Path As String,
        Configuration As Dictionary(Of String, String)
        ) As String

        Dim Length As Integer = Len(Configuration("TextBoxInputDirectory"))
        Dim NewPath As String

        If Path.Contains(Configuration("TextBoxInputDirectory")) Then
            NewPath = Path.Remove(0, Length)
            NewPath = "~" + NewPath
        Else
            NewPath = Path
        End If
        Return NewPath
    End Function

End Class
