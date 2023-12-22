Option Strict On

Imports SolidEdgeCommunity
Imports SolidEdgeConstants

Public Class DraftTasks
    Inherits IsolatedTaskProxy


    Public Function BrokenLinks(
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
                                   AddressOf BrokenLinksInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function BrokenLinksInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim ModelLinks As SolidEdgeDraft.ModelLinks = Nothing
        Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing

        Dim Filename As String

        ModelLinks = SEDoc.ModelLinks

        For Each ModelLink In ModelLinks
            If ModelLink.IsAssemblyFamilyMember Then
                Filename = ModelLink.FileName.Split("!"c)(0)
            Else
                Filename = ModelLink.FileName
            End If

            If Not FileIO.FileSystem.FileExists(Filename) Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0} not found", CommonTasks.TruncateFullPath(Filename, Configuration)))
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
        Dim ParentSheet As SolidEdgeDraft.Sheet = Nothing

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
                            msg = String.Format("Sheet {0}: {1}", Sheet.Name, Balloon.BalloonDisplayedText)
                            ErrorMessageList.Add(msg)
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
            Dim VariableListItemType2 As String = Microsoft.VisualBasic.Information.TypeName(VariableListItem)



            If VariableListItemType = SolidEdgeFramework.ObjectType.igDimension Then
                Dimension = DirectCast(VariableListItem, SolidEdgeFrameworkSupport.Dimension)
                TF = Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seDimStatusDetached
                TF = TF Or Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seDimStatusError
                TF = TF Or Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seOneEndDetached

                If TF Then
                    ExitStatus = 1
                    ParentSheet = CType(Dimension.Parent, SolidEdgeDraft.Sheet)
                    msg = String.Format("Sheet {0}: {1}", ParentSheet.Name, Dimension.DisplayName)
                    ErrorMessageList.Add(msg)
                End If
            End If

        Next

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function PartsListMissingOrOutOfDate(
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
                                   AddressOf PartsListMissingOrOutOfDateInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function PartsListMissingOrOutOfDateInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim PartsLists As SolidEdgeDraft.PartsLists = SEDoc.PartsLists
        Dim PartsList As SolidEdgeDraft.PartsList

        If PartsLists.Count = 0 Then
            ExitStatus = 1
            ErrorMessageList.Add("Parts list missing")
        Else
            For Each PartsList In PartsLists
                If Not PartsList.IsUpToDate Then
                    ExitStatus = 1
                    ErrorMessageList.Add("Parts list out of date")
                    Exit For
                End If
            Next

        End If


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

        Dim Filename As String

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
            If ModelLink.IsAssemblyFamilyMember Then
                Filename = ModelLink.FileName.Split("!"c)(0)
            Else
                Filename = ModelLink.FileName
            End If

            If Not FileIO.FileSystem.FileExists(Filename) Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0} not found", CommonTasks.TruncateFullPath(Filename, Configuration)))
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



    Public Function UpdateStylesFromTemplate(
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
                                   AddressOf UpdateStylesFromTemplateInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function DocStyleNotInTemplate(
       DocStyleNameList As List(Of String),
       TemplateStyleNameList As List(Of String)
       ) As String
        Dim Names As String = ""
        For Each s As String In DocStyleNameList
            If Not TemplateStyleNameList.Contains(s) Then
                Names = String.Format("{0} {1},", Names, s)
            End If
        Next

        If Len(Names) > 0 Then
            ' Remove trailing comma.
            Names = Names.Substring(0, Len(Names) - 1)
        End If

        Return Names
    End Function


    Private Function UpdateStylesFromTemplateInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TemplateFilename As String = Configuration("TextBoxTemplateDraft")
        Dim SETemplateDoc As SolidEdgeDraft.DraftDocument

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim MissingStyles As String = ""

        Dim SupplementalExitStatus As Integer = 0
        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        ' Update drawing border
        SupplementalErrorMessage = UpdateDrawingBorderFromTemplate(CType(SEDoc, SolidEdgeFramework.SolidEdgeDocument), Configuration, SEApp)
        If SupplementalErrorMessage.Keys(0) <> 0 Then
            ExitStatus = SupplementalErrorMessage.Keys(0)
            For Each V As String In SupplementalErrorMessage(ExitStatus)
                ErrorMessageList.Add(V)
            Next
        End If


        'Open template
        SETemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgeDraft.DraftDocument)
        SEApp.DoIdle()

        SEDoc.Activate()
        SEApp.DoIdle()

        ' All style collections.
        ' DashStyles, DimensionStyles, DrawingViewStyles, FillStyles, HatchPatternStyles, 
        ' LinearStyles, SmartFrame2dStyles, TableStyles, TextCharStyles, TextStyles

        ' Style collections to receive updates.
        ' DimensionStyles, DrawingViewStyles, LinearStyles, TableStyles, TextCharStyles, TextStyles

        ' Styles not updated at this time.
        ' DashStyles, FillStyles, HatchPatternStyles, SmartFrame2dStyles



        ' ############  DimensionStyles ############
        Dim DocDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles
        DocDimensionStyles = CType(SEDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)

        Dim TemplateDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles
        TemplateDimensionStyles = CType(SETemplateDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)

        For Each TemplateDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle In TemplateDimensionStyles
            If Not TemplateStyleNames.Contains(TemplateDimensionStyle.Name) Then
                TemplateStyleNames.Add(TemplateDimensionStyle.Name)
            End If
            For Each DocDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle In DocDimensionStyles
                If Not DocStyleNames.Contains(DocDimensionStyle.Name) Then
                    DocStyleNames.Add(DocDimensionStyle.Name)
                End If
                If TemplateDimensionStyle.Name = DocDimensionStyle.Name Then
                    Try
                        CommonTasks.CopyProperties(TemplateDimensionStyle, DocDimensionStyle)
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Error applying DimensionStyle '{0}'", TemplateDimensionStyle.Name))
                    End Try
                End If
            Next
        Next

        MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
        If Len(MissingStyles) > 0 Then
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Dimension styles in Draft but not in Template: {0}", MissingStyles))
        End If
        DocStyleNames.Clear()
        TemplateStyleNames.Clear()
        MissingStyles = ""


        Dim DocDrawingViewStyles As SolidEdgeFrameworkSupport.DrawingViewStyles
        DocDrawingViewStyles = CType(SEDoc.DrawingViewStyles, SolidEdgeFrameworkSupport.DrawingViewStyles)

        Dim TemplateDrawingViewStyles As SolidEdgeFrameworkSupport.DrawingViewStyles
        TemplateDrawingViewStyles = CType(SETemplateDoc.DrawingViewStyles, SolidEdgeFrameworkSupport.DrawingViewStyles)

        For Each TemplateDrawingViewStyle As SolidEdgeFrameworkSupport.DrawingViewStyle In TemplateDrawingViewStyles
            If Not TemplateStyleNames.Contains(TemplateDrawingViewStyle.Name) Then
                TemplateStyleNames.Add(TemplateDrawingViewStyle.Name)
            End If
            For Each DocDrawingViewStyle As SolidEdgeFrameworkSupport.DrawingViewStyle In DocDrawingViewStyles
                If Not DocStyleNames.Contains(DocDrawingViewStyle.Name) Then
                    DocStyleNames.Add(DocDrawingViewStyle.Name)
                End If
                If TemplateDrawingViewStyle.Name = DocDrawingViewStyle.Name Then
                    Try
                        CommonTasks.CopyProperties(TemplateDrawingViewStyle, DocDrawingViewStyle)
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Error applying DrawingViewStyle '{0}'", TemplateDrawingViewStyle.Name))
                    End Try
                End If
            Next
        Next

        MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
        If Len(MissingStyles) > 0 Then
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Drawing View styles in Draft but not in Template: {0}", MissingStyles))
        End If
        DocStyleNames.Clear()
        TemplateStyleNames.Clear()
        MissingStyles = ""




        Dim DocLinearStyles As SolidEdgeFramework.LinearStyles
        DocLinearStyles = CType(SEDoc.LinearStyles, SolidEdgeFramework.LinearStyles)

        Dim TemplateLinearStyles As SolidEdgeFramework.LinearStyles
        TemplateLinearStyles = CType(SETemplateDoc.LinearStyles, SolidEdgeFramework.LinearStyles)

        For Each TemplateLinearStyle As SolidEdgeFramework.LinearStyle In TemplateLinearStyles
            If Not TemplateStyleNames.Contains(TemplateLinearStyle.Name) Then
                TemplateStyleNames.Add(TemplateLinearStyle.Name)
            End If
            For Each DocLinearStyle As SolidEdgeFramework.LinearStyle In DocLinearStyles
                If Not DocStyleNames.Contains(DocLinearStyle.Name) Then
                    DocStyleNames.Add(DocLinearStyle.Name)
                End If
                If TemplateLinearStyle.Name = DocLinearStyle.Name Then
                    Try
                        CommonTasks.CopyProperties(TemplateLinearStyle, DocLinearStyle)
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Error applying LinearStyle '{0}'", TemplateLinearStyle.Name))
                    End Try
                End If
            Next
        Next

        MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
        If Len(MissingStyles) > 0 Then
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Linear styles in Draft but not in Template: {0}", MissingStyles))
        End If
        DocStyleNames.Clear()
        TemplateStyleNames.Clear()
        MissingStyles = ""






        Dim DocTableStyles As SolidEdgeFrameworkSupport.TableStyles
        DocTableStyles = CType(SEDoc.TableStyles, SolidEdgeFrameworkSupport.TableStyles)

        Dim TemplateTableStyles As SolidEdgeFrameworkSupport.TableStyles
        TemplateTableStyles = CType(SETemplateDoc.TableStyles, SolidEdgeFrameworkSupport.TableStyles)

        For Each TemplateTableStyle As SolidEdgeFrameworkSupport.TableStyle In TemplateTableStyles
            If Not TemplateStyleNames.Contains(TemplateTableStyle.Name) Then
                TemplateStyleNames.Add(TemplateTableStyle.Name)
            End If
            For Each DocTableStyle As SolidEdgeFrameworkSupport.TableStyle In DocTableStyles
                If Not DocStyleNames.Contains(DocTableStyle.Name) Then
                    DocStyleNames.Add(DocTableStyle.Name)
                End If
                If TemplateTableStyle.Name = DocTableStyle.Name Then
                    Try
                        CommonTasks.CopyProperties(TemplateTableStyle, DocTableStyle)
                        '#### added because CopyProperties didn't work in old SE Release, to be verified if still needed
                        For c = 0 To 6
                            DocTableStyle.LineColor(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineColor(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                            DocTableStyle.LineDashType(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineDashType(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                            DocTableStyle.LineWidth(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineWidth(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                        Next
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Error applying TableStyle '{0}'", TemplateTableStyle.Name))
                    End Try
                End If
            Next
        Next

        MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
        If Len(MissingStyles) > 0 Then
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Table styles in Draft but not in Template: {0}", MissingStyles))
        End If
        DocStyleNames.Clear()
        TemplateStyleNames.Clear()
        MissingStyles = ""




        Dim DocTextCharStyles As SolidEdgeFramework.TextCharStyles
        DocTextCharStyles = CType(SEDoc.TextCharStyles, SolidEdgeFramework.TextCharStyles)

        Dim TemplateTextCharStyles As SolidEdgeFramework.TextCharStyles
        TemplateTextCharStyles = CType(SETemplateDoc.TextCharStyles, SolidEdgeFramework.TextCharStyles)

        For Each TemplateTextCharStyle As SolidEdgeFramework.TextCharStyle In TemplateTextCharStyles
            If Not TemplateStyleNames.Contains(TemplateTextCharStyle.Name) Then
                TemplateStyleNames.Add(TemplateTextCharStyle.Name)
            End If
            For Each DocTextCharStyle As SolidEdgeFramework.TextCharStyle In DocTextCharStyles
                If Not DocStyleNames.Contains(DocTextCharStyle.Name) Then
                    DocStyleNames.Add(DocTextCharStyle.Name)
                End If
                If TemplateTextCharStyle.Name = DocTextCharStyle.Name Then
                    Try
                        CommonTasks.CopyProperties(TemplateTextCharStyle, DocTextCharStyle)
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Error applying TextCharStyle '{0}'", TemplateTextCharStyle.Name))
                    End Try
                End If
            Next
        Next

        MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
        If Len(MissingStyles) > 0 Then
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Text Char styles in Draft but not in Template: {0}", MissingStyles))
        End If
        DocStyleNames.Clear()
        TemplateStyleNames.Clear()
        MissingStyles = ""




        Dim DocTextStyles As SolidEdgeFramework.TextStyles
        DocTextStyles = CType(SEDoc.TextStyles, SolidEdgeFramework.TextStyles)

        Dim TemplateTextStyles As SolidEdgeFramework.TextStyles
        TemplateTextStyles = CType(SETemplateDoc.TextStyles, SolidEdgeFramework.TextStyles)

        For Each TemplateTextStyle As SolidEdgeFramework.TextStyle In TemplateTextStyles
            If Not TemplateStyleNames.Contains(TemplateTextStyle.Name) Then
                TemplateStyleNames.Add(TemplateTextStyle.Name)
            End If
            For Each DocTextStyle As SolidEdgeFramework.TextStyle In DocTextStyles
                If Not DocStyleNames.Contains(DocTextStyle.Name) Then
                    DocStyleNames.Add(DocTextStyle.Name)
                End If
                If TemplateTextStyle.Name = DocTextStyle.Name Then
                    Try
                        CommonTasks.CopyProperties(TemplateTextStyle, DocTextStyle)
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Error applying TextStyle '{0}'", TemplateTextStyle.Name))
                    End Try
                End If
            Next
        Next

        MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
        If Len(MissingStyles) > 0 Then
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Text styles in Draft but not in Template: {0}", MissingStyles))
        End If
        DocStyleNames.Clear()
        TemplateStyleNames.Clear()
        MissingStyles = ""




        SETemplateDoc.Close()
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



    'Public Function MoveDrawingToNewTemplate(
    '    ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
    '    ByVal Configuration As Dictionary(Of String, String),
    '    ByVal SEApp As SolidEdgeFramework.Application
    '    ) As Dictionary(Of Integer, List(Of String))

    '    Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

    '    ErrorMessage = InvokeSTAThread(
    '                           Of SolidEdgeDraft.DraftDocument,
    '                           Dictionary(Of String, String),
    '                           SolidEdgeFramework.Application,
    '                           Dictionary(Of Integer, List(Of String)))(
    '                               AddressOf MoveDrawingToNewTemplateInternal,
    '                               CType(SEDoc, SolidEdgeDraft.DraftDocument),
    '                               Configuration,
    '                               SEApp)

    '    Return ErrorMessage

    'End Function

    'Private Function MoveDrawingToNewTemplateInternal(
    '    ByVal SEDoc As SolidEdgeDraft.DraftDocument,
    '    ByVal Configuration As Dictionary(Of String, String),
    '    ByVal SEApp As SolidEdgeFramework.Application
    '    ) As Dictionary(Of Integer, List(Of String))

    '    Dim ErrorMessageList As New List(Of String)
    '    Dim ExitStatus As Integer = 0
    '    Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

    '    Dim SupplementalErrorMessageList As New List(Of String)

    '    Dim NewDoc As SolidEdgeDraft.DraftDocument

    '    Dim NewDocFilename As String = String.Format("{0}\{1}-Housekeeper.dft",
    '                                   System.IO.Path.GetDirectoryName(SEDoc.FullName),
    '                                   System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))
    '    Dim RemnantsDocFilename As String = NewDocFilename.Replace("Housekeeper", "HousekeeperOld")

    '    Dim SEDocDVSheetNames As New List(Of String)  ' List of sheet names in order of drawing views

    '    Dim SheetName As String
    '    Dim BOSVCountBefore As New Dictionary(Of String, Integer)
    '    Dim BOSVCountAfter As New Dictionary(Of String, Integer)
    '    Dim Delta As Integer

    '    Dim msg As String = ""

    '    Dim DummySheetName As String = "Housekeeper"

    '    Dim DVOriginalSheetDict As New Dictionary(Of SolidEdgeDraft.DrawingView, SolidEdgeDraft.Sheet)
    '    Dim DVIdxOriginalSheetDict As New Dictionary(Of String, Dictionary(Of Integer, String))

    '    ' Open target document
    '    NewDoc = CType(SEApp.Documents.Add("SolidEdge.DraftDocument",
    '                                       Configuration("TextBoxTemplateDraft")),
    '                                       SolidEdgeDraft.DraftDocument)

    '    If SEDoc.FullName.Contains("-HousekeeperOld") Then
    '        ExitStatus = 2
    '        msg = CommonTasks.TruncateFullPath(SEDoc.FullName, Configuration)
    '        ErrorMessageList.Add(String.Format("Auto-generated file '{0}' not processed", msg))

    '    Else

    '        ' Check for Drawing Views on background sheets
    '        Dim BackgroundSheet As SolidEdgeDraft.Sheet
    '        For Each BackgroundSheet In GetSheets(SEDoc, "Background")
    '            If BackgroundSheet.DrawingViews.Count > 0 Then
    '                ExitStatus = 2
    '                msg = String.Format("Drawing view found on background '{0}'.", BackgroundSheet.Name)
    '                msg = String.Format("{0}  No changes made.  Please transfer to a working sheet and try again.", msg)
    '                ErrorMessageList.Add(msg)

    '            End If
    '        Next

    '        If ExitStatus < 2 Then
    '            ' Create new sheets in target.  Delete any sheets in target not in source.  Set background each sheet.
    '            SupplementalErrorMessageList = AddSheetsToTarget(SEDoc, NewDoc, DummySheetName)
    '            If SupplementalErrorMessageList.Count > 0 Then
    '                ExitStatus = 1
    '                For Each s As String In SupplementalErrorMessageList
    '                    ErrorMessageList.Add(s)
    '                Next
    '            End If

    '            ' Records sheet locations for each drawing view
    '            DVOriginalSheetDict = GetDVOriginalSheet(SEDoc)

    '            ' Get any broken out section views from the source document
    '            BOSVCountBefore = GetBOSVCount(SEDoc)

    '            ' Move drawing views, such as sections and details, to the sheet holding the parent view.
    '            ' This eliminates duplicate source drawing views when they are cut/pasted from the old doc to the new.
    '            MoveDVToSourceViewSheet(SEDoc, DVOriginalSheetDict)

    '            ' Cut/paste changes the drawing view object IDs, but not their item order on the sheet.
    '            DVIdxOriginalSheetDict = GetDVIdx(SEDoc, DVOriginalSheetDict)

    '            ExitStatus = TransferSheetContentsToTarget(SEApp, SEDoc, NewDoc)

    '            If ExitStatus = 2 Then
    '                ' Botched cut/paste.  Don't save anything.
    '                ErrorMessageList.Add("Problem with cut/paste.  No changes made.  Please update manually.")

    '            Else
    '                ExitStatus = Move2DModelsToTarget(SEApp, SEDoc, NewDoc)

    '                If ExitStatus = 2 Then
    '                    ' Problem transferring 2D model
    '                    ErrorMessageList.Add("Problem with 2D model.  No changes made.  Please update manually.")

    '                Else

    '                    MoveDVsToTargetSheet(NewDoc, DVIdxOriginalSheetDict)

    '                    ' Get any broken out section views from the source document
    '                    BOSVCountAfter = GetBOSVCount(NewDoc)

    '                    ' Check if any broken out section views did not update correctly
    '                    For Each SheetName In BOSVCountBefore.Keys
    '                        If BOSVCountAfter.Keys.Contains(SheetName) Then
    '                            Delta = BOSVCountBefore(SheetName) - BOSVCountAfter(SheetName)
    '                            If Delta > 0 Then
    '                                ExitStatus = 1
    '                                msg = String.Format("Sheet '{0}': Broken Out Section View(s) did not update correctly", SheetName)
    '                                ErrorMessageList.Add(msg)
    '                            End If
    '                        End If
    '                    Next

    '                    ' Check for Parts Lists that were created with the option 'Create new sheets for table'
    '                    If GetSheets(SEDoc, "AutoGenerated").Count > 0 Then
    '                        ExitStatus = 1
    '                        ErrorMessageList.Add("Auto generated sheets (probably a Parts List) not processed.  Please regenerate.")
    '                        msg = ""
    '                        For Each Sheet In GetSheets(SEDoc, "AutoGenerated")
    '                            msg += String.Format("{0}, ", Sheet.Name)
    '                        Next
    '                        ErrorMessageList.Add(msg)
    '                    End If

    '                    TidyUpUpdateStylesFromTemplate(SEApp, SEDoc, NewDoc)

    '                End If

    '            End If

    '        End If

    '    End If


    '    If ExitStatus = 2 Then
    '        ' Don't save anything
    '    ElseIf ExitStatus = 1 Then
    '        If Configuration("CheckBoxMoveDrawingViewAllowPartialSuccess") = "True" Then
    '            NewDoc.SaveAs(NewDocFilename)
    '            SEApp.DoIdle()

    '            If SEDoc.ReadOnly Then
    '                ExitStatus = 1
    '                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
    '            Else
    '                SEDoc.Save()
    '                SEApp.DoIdle()

    '                msg = CommonTasks.TruncateFullPath(RemnantsDocFilename, Configuration)
    '                msg = String.Format("After correcting issues, please delete {0}", msg)
    '                ErrorMessageList.Add(msg)
    '            End If
    '        End If
    '    ElseIf ExitStatus = 0 Then
    '        NewDoc.SaveAs(NewDocFilename)
    '        SEApp.DoIdle()
    '    End If

    '    NewDoc.Close()
    '    SEApp.DoIdle()


    '    ErrorMessage(ExitStatus) = ErrorMessageList
    '    Return ErrorMessage
    'End Function

    'Private Function GetBOSVCount(Doc As SolidEdgeDraft.DraftDocument) As Dictionary(Of String, Integer)
    '    Dim BOSVCount As New Dictionary(Of String, Integer)
    '    Dim Sheet As SolidEdgeDraft.Sheet
    '    Dim count As Integer
    '    Dim DrawingViews As SolidEdgeDraft.DrawingViews
    '    Dim DrawingView As SolidEdgeDraft.DrawingView


    '    For Each Sheet In GetSheets(Doc, "Working")
    '        count = 0
    '        DrawingViews = Sheet.DrawingViews
    '        For Each DrawingView In DrawingViews
    '            If DrawingView.IsBrokenOutSectionTarget Then
    '                count += 1
    '            End If
    '        Next
    '        BOSVCount(Sheet.Name) = count
    '    Next

    '    Return BOSVCount
    'End Function

    'Private Sub TidyUpUpdateStylesFromTemplate(SEApp As SolidEdgeFramework.Application,
    '                                           SourceDoc As SolidEdgeDraft.DraftDocument,
    '                                           TargetDoc As SolidEdgeDraft.DraftDocument)

    '    Dim SourceSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "UserGenerated")
    '    Dim TargetSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(TargetDoc, "UserGenerated")
    '    Dim SheetWindow As SolidEdgeDraft.SheetWindow

    '    SourceDoc.Activate()
    '    SourceSheets(0).Activate()
    '    SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '    SheetWindow.Display2DModelSheetTab = False
    '    'SheetWindow.DisplayBackgroundSheetTabs = False

    '    'For Each SourceSheet In SourceSheets
    '    '    If SourceSheet.Name = DummyName Then
    '    '        SourceSheet.Delete()
    '    '        Exit For
    '    '    End If
    '    'Next

    '    TargetDoc.Activate()
    '    For Each TargetSheet In TargetSheets
    '        TargetSheet.Activate()
    '        SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '        SheetWindow.Display2DModelSheetTab = False
    '        SheetWindow.DisplayBackgroundSheetTabs = False
    '        SheetWindow.FitEx(SolidEdgeDraft.SheetFitConstants.igFitSheet)
    '    Next
    '    TargetSheets(0).Activate()

    'End Sub

    'Dim TidyUp As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument, String)
    '    TidyUp = Sub(SourceDoc, TargetDoc, DummyName)

    '             End Sub

    'Private Function Move2DModelsToTarget(SEApp As SolidEdgeFramework.Application,
    '                                      SourceDoc As SolidEdgeDraft.DraftDocument,
    '                                      TargetDoc As SolidEdgeDraft.DraftDocument
    '                                      ) As Integer

    '    Dim ExitStatus As Integer = 0

    '    Dim SheetWindow As SolidEdgeDraft.SheetWindow
    '    'Dim Sections As SolidEdgeDraft.Sections

    '    Dim Source2DSheet As SolidEdgeDraft.Sheet = GetSheets(SourceDoc, "2DModel")(0)
    '    Dim Target2DSheet As SolidEdgeDraft.Sheet = GetSheets(TargetDoc, "2DModel")(0)

    '    Dim DrawingObjects As SolidEdgeFrameworkSupport.DrawingObjects
    '    'Dim SelectSet As SolidEdgeFramework.SelectSet

    '    DrawingObjects = Source2DSheet.DrawingObjects

    '    If DrawingObjects.Count > 0 Then
    '        Try
    '            SourceDoc.Activate()
    '            Source2DSheet.Activate()
    '            SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '            'SelectSet = SheetWindow.SelectSet

    '            'For Each DrawingObject In DrawingObjects
    '            '    SelectSet.Add(DrawingObject)
    '            'Next

    '            Source2DSheet.FenceLocate(-1000, 1000, 1000, -1000)


    '            SEApp.DoIdle()
    '            SheetWindow.Cut()
    '            SEApp.DoIdle()
    '            SheetWindow.Display2DModelSheetTab = False
    '            SheetWindow.DisplayBackgroundSheetTabs = False

    '            TargetDoc.Activate()
    '            Target2DSheet.Activate()
    '            SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

    '            SheetWindow.Paste()
    '            SEApp.DoIdle()
    '            SheetWindow.Display2DModelSheetTab = False
    '            SheetWindow.DisplayBackgroundSheetTabs = False

    '        Catch ex As Exception
    '            ExitStatus = 2
    '        End Try
    '    End If

    '    Return ExitStatus
    'End Function
    'End Sub

    'Private Sub MoveDVsToTargetSheet(Doc As SolidEdgeDraft.DraftDocument,
    '                          DVIdxOriginalSheetDict As Dictionary(Of String, Dictionary(Of Integer, String))
    '                          )


    '    Dim Sheet As SolidEdgeDraft.Sheet
    '    Dim DrawingViews As SolidEdgeDraft.DrawingViews
    '    Dim DrawingView As SolidEdgeDraft.DrawingView
    '    Dim idx As Integer
    '    Dim TargetSheetName As String

    '    For Each Sheet In GetSheets(Doc, "Working")
    '        DrawingViews = Sheet.DrawingViews
    '        If DrawingViews.Count > 0 Then
    '            idx = 1
    '            For Each DrawingView In DrawingViews
    '                If DVIdxOriginalSheetDict(Sheet.Name).Keys.Contains(idx) Then
    '                    TargetSheetName = DVIdxOriginalSheetDict(Sheet.Name)(idx)
    '                    If Sheet.Name <> TargetSheetName Then
    '                        DrawingView.Sheet = SheetNameToObject(Doc, "Working", TargetSheetName)
    '                    End If
    '                End If
    '                idx += 1
    '            Next
    '        End If
    '    Next

    'End Sub

    'Private Function GetDVIdx(Doc As SolidEdgeDraft.DraftDocument,
    '                          DVOriginalSheetDict As Dictionary(Of SolidEdgeDraft.DrawingView, SolidEdgeDraft.Sheet)
    '                          ) As Dictionary(Of String, Dictionary(Of Integer, String))

    '    Dim DVIdxOriginalSheetDict As New Dictionary(Of String, Dictionary(Of Integer, String))
    '    Dim Sheet As SolidEdgeDraft.Sheet
    '    Dim DrawingViews As SolidEdgeDraft.DrawingViews
    '    Dim DrawingView As SolidEdgeDraft.DrawingView
    '    Dim idx As Integer

    '    For Each Sheet In GetSheets(Doc, "Working")
    '        DVIdxOriginalSheetDict(Sheet.Name) = New Dictionary(Of Integer, String)
    '        DrawingViews = Sheet.DrawingViews
    '        If DrawingViews.Count > 0 Then
    '            idx = 1
    '            For Each DrawingView In DrawingViews
    '                DVIdxOriginalSheetDict(Sheet.Name)(idx) = DVOriginalSheetDict(DrawingView).Name
    '                idx += 1
    '            Next
    '        End If
    '    Next

    '    Return DVIdxOriginalSheetDict
    'End Function

    'Private Function AddSheetsToTarget(SourceDoc As SolidEdgeDraft.DraftDocument,
    '                              TargetDoc As SolidEdgeDraft.DraftDocument,
    '                              DummyName As String
    '                              ) As List(Of String)
    '    ' Add sheets to target to match source.
    '    ' Remove sheets from target that don't match.
    '    ' Set backgrounds to match.  Report to log if target does not have the background sheet.

    '    Dim ErrorMessageList As New List(Of String)

    '    'Dim SourceSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "UserGenerated")
    '    'Dim TargetSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(TargetDoc, "UserGenerated")

    '    Dim SourceSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "UserGenerated")
    '    Dim TargetSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(TargetDoc, "UserGenerated")

    '    Dim SourceSheet As SolidEdgeDraft.Sheet
    '    Dim TargetSheet As SolidEdgeDraft.Sheet

    '    Dim SourceSheetNames As New List(Of String)
    '    Dim TargetSheetNames As New List(Of String)

    '    For Each SourceSheet In SourceSheets
    '        SourceSheetNames.Add(SourceSheet.Name)
    '    Next

    '    For Each TargetSheet In TargetSheets
    '        TargetSheetNames.Add(TargetSheet.Name)
    '    Next

    '    For Each SourceSheet In SourceSheets
    '        If Not TargetSheetNames.Contains(SourceSheet.Name) Then
    '            AddSheet(TargetDoc, SourceSheet.Name)
    '        End If
    '    Next

    '    For Each TargetSheetName In TargetSheetNames
    '        If Not SourceSheetNames.Contains(TargetSheetName) Then
    '            SheetNameToObject(TargetDoc, "Working", TargetSheetName).Delete()
    '        End If
    '    Next

    '    ' SetTargetBackgrounds(SourceDoc, TargetDoc, DummyName)
    '    Dim SourceBackgroundSheet As SolidEdgeDraft.Sheet
    '    Dim TargetBackgroundSheet As SolidEdgeDraft.Sheet

    '    Dim msg2 As String = ""

    '    TargetSheets = GetSheets(TargetDoc, "UserGenerated")

    '    For Each TargetSheet In TargetSheets
    '        If Not TargetSheet.Name = DummyName Then
    '            SourceSheet = SheetNameToObject(SourceDoc, "Working", TargetSheet.Name)
    '            ' SourceSheetNames.Add(SourceSheet.Name)
    '            ' Not all sheets have a background defined
    '            Try
    '                SourceBackgroundSheet = SourceSheet.Background
    '                TargetBackgroundSheet = SheetNameToObject(TargetDoc, "Background", SourceBackgroundSheet.Name)
    '                If Not TargetBackgroundSheet Is Nothing Then
    '                    TargetSheet.Background = TargetBackgroundSheet
    '                    TargetSheet.BackgroundVisible = SourceSheet.BackgroundVisible
    '                    TargetSheet.SheetSetup.SheetSizeOption = SourceSheet.SheetSetup.SheetSizeOption
    '                Else
    '                    ' ExitStatus = 1
    '                    msg2 = String.Format("Template does not have a background named '{0}'", SourceBackgroundSheet.Name)
    '                    If Not ErrorMessageList.Contains(msg2) Then
    '                        ErrorMessageList.Add(msg2)
    '                    End If
    '                End If
    '            Catch ex As Exception
    '            End Try
    '        End If
    '    Next

    '    Return ErrorMessageList

    'End Function

    'Private Function TransferSheetContentsToTarget(SEApp As SolidEdgeFramework.Application,
    '                                               SourceDoc As SolidEdgeDraft.DraftDocument,
    '                                               TargetDoc As SolidEdgeDraft.DraftDocument
    '                                               ) As Integer

    '    Dim ExitStatus As Integer = 0

    '    Dim SheetWindow As SolidEdgeDraft.SheetWindow
    '    Dim SourceSheet As SolidEdgeDraft.Sheet
    '    Dim TargetSheet As SolidEdgeDraft.Sheet
    '    'Dim DrawingObjects As SolidEdgeFrameworkSupport.DrawingObjects

    '    Dim SelectSet As SolidEdgeFramework.SelectSet
    '    Dim DrawingViews As SolidEdgeDraft.DrawingViews
    '    Dim DrawingView As SolidEdgeDraft.DrawingView

    '    Dim msg2 As String = ""
    '    Dim HasBrokenView As Boolean

    '    Dim SelectSetCount As Integer

    '    For Each SourceSheet In GetSheets(SourceDoc, "UserGenerated")
    '        If SourceSheet.DrawingObjects.Count > 0 Then
    '            SourceDoc.Activate()
    '            SourceSheet.Activate()

    '            SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '            SheetWindow.FitEx(SolidEdgeDraft.SheetFitConstants.igFitAll)

    '            ' Deal with Broken Views
    '            DrawingViews = SourceSheet.DrawingViews
    '            If DrawingViews.Count > 0 Then
    '                HasBrokenView = False

    '                For Each DrawingView In DrawingViews
    '                    If DrawingView.IsBroken Then
    '                        HasBrokenView = True
    '                        Exit For
    '                    End If
    '                Next

    '                If HasBrokenView Then
    '                    SelectSet = SheetWindow.SelectSet
    '                    For Each DrawingView In DrawingViews
    '                        DrawingView.Select()
    '                        DrawingView.AddConnectedAnnotationsToSelectSet()
    '                        DrawingView.AddConnectedDimensionsToSelectSet()
    '                    Next
    '                    SEApp.DoIdle()
    '                    SheetWindow.Cut()
    '                    SEApp.DoIdle()

    '                    TargetDoc.Activate()
    '                    TargetSheet = SheetNameToObject(TargetDoc, "UserGenerated", SourceSheet.Name)
    '                    TargetSheet.Activate()
    '                    SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '                    SheetWindow.Paste()
    '                    SEApp.DoIdle()

    '                    SelectSetCount = SelectSet.Count
    '                    SelectSet.RemoveAll()
    '                    SelectSetCount = SelectSet.Count
    '                    SEApp.DoIdle()

    '                End If

    '            End If

    '            SourceDoc.Activate()
    '            SourceSheet.Activate()

    '            SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

    '            SourceSheet.FenceLocate(-1000, 1000, 1000, -1000)

    '            SelectSet = SheetWindow.SelectSet

    '            SelectSetCount = SelectSet.Count
    '            If SelectSet.Count > 0 Then
    '                ' Catch copy/paste problem
    '                Try

    '                    SEApp.DoIdle()
    '                    SheetWindow.Cut()
    '                    SEApp.DoIdle()

    '                    TargetDoc.Activate()
    '                    TargetSheet = SheetNameToObject(TargetDoc, "UserGenerated", SourceSheet.Name)
    '                    TargetSheet.Activate()
    '                    SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

    '                    SheetWindow.Paste()
    '                    SEApp.DoIdle()

    '                Catch ex As Exception
    '                    ExitStatus = 2

    '                End Try

    '            End If
    '        End If
    '    Next

    '    Return ExitStatus

    'End Function

    'Private Function GetDVOriginalSheet(Doc As SolidEdgeDraft.DraftDocument
    '                                    ) As Dictionary(Of SolidEdgeDraft.DrawingView, SolidEdgeDraft.Sheet)

    '    Dim DVOriginalSheet As New Dictionary(Of SolidEdgeDraft.DrawingView, SolidEdgeDraft.Sheet)
    '    Dim SectionTypes As New List(Of String)
    '    Dim SectionType As String

    '    Dim Sheets As New List(Of SolidEdgeDraft.Sheet)
    '    Dim Sheet As SolidEdgeDraft.Sheet
    '    Dim DrawingViews As SolidEdgeDraft.DrawingViews
    '    Dim DrawingView As SolidEdgeDraft.DrawingView

    '    SectionTypes.Add("Working")
    '    SectionTypes.Add("Background")

    '    For Each SectionType In SectionTypes
    '        Sheets = GetSheets(Doc, SectionType)
    '        For Each Sheet In Sheets
    '            If Sheet.DrawingViews.Count > 0 Then
    '                DrawingViews = Sheet.DrawingViews
    '                For Each DrawingView In DrawingViews
    '                    DVOriginalSheet(DrawingView) = Sheet
    '                Next
    '            End If
    '        Next
    '    Next

    '    Return DVOriginalSheet

    'End Function

    'Private Sub MoveDVToSourceViewSheet(Doc As SolidEdgeDraft.DraftDocument,
    '                                    DVOriginalSheetsDict As Dictionary(Of SolidEdgeDraft.DrawingView, SolidEdgeDraft.Sheet))

    '    For Each DV In DVOriginalSheetsDict.Keys
    '        If Not DV.SourceDrawingView Is DV Then
    '            DV.Sheet = DVOriginalSheetsDict(DV.SourceDrawingView)  ' Key is the drawing view, value is the sheet
    '        End If
    '    Next


    'End Sub

    Private Function GetSheets(Doc As SolidEdgeDraft.DraftDocument,
        SectionType As String
        ) As List(Of SolidEdgeDraft.Sheet)

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

        Else  ' So, 'UserGenerated' or 'AutoGenerated'
            SheetGroups = CType(Doc.SheetGroups, SolidEdgeDraft.SheetGroups)

            ' 'count' is an index on SheetGroups.  The first SheetGroup is user generated.
            ' The others appear to be automatically generated for PartsLists
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

    'Private Function SheetNameToObject(Doc As SolidEdgeDraft.DraftDocument,
    '                                   SectionType As String,
    '                                   SheetName As String) As SolidEdgeDraft.Sheet
    '    'Dim SheetNameToObject As Func(Of SolidEdgeDraft.DraftDocument, String, String, SolidEdgeDraft.Sheet)
    '    'SheetNameToObject = Function(Doc, SectionType, SheetName)
    '    '                        Dim Sheet As SolidEdgeDraft.Sheet

    '    For Each Sheet In GetSheets(Doc, SectionType)
    '        If Sheet.Name = SheetName Then
    '            Return Sheet
    '        End If
    '    Next
    '    Return Nothing
    'End Function

    'Private Sub AddSheet(Doc As SolidEdgeDraft.DraftDocument, SheetName As String)
    '    Dim Sheet As SolidEdgeDraft.Sheet
    '    Dim SheetAlreadyExists As Boolean = False

    '    For Each Sheet In GetSheets(Doc, "Working")
    '        If SheetName = Sheet.Name Then
    '            SheetAlreadyExists = True
    '            Exit For
    '        End If
    '    Next

    '    If Not SheetAlreadyExists Then
    '        Doc.Sheets.AddSheet(SheetName, SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection)
    '    End If
    'End Sub



    Public Function OpenSave(
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
                                   AddressOf OpenSaveInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function OpenSaveInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

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




    Public Function SaveAs(
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
                                   AddressOf SaveAsInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function SaveAsInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))
        ' Dim SupplementalExitStatus As Integer

        Dim NewFilename As String = ""
        Dim NewExtension As String = ""
        Dim NewFileFormat As String = ""
        Dim DraftBaseFilename As String

        Dim BaseDir As String
        Dim SubDir As String
        Dim Formula As String
        Dim Proceed As Boolean = True
        ' Dim msg As String
        Dim FCD As New FilenameCharmapDoctor()

        ' ComboBoxSaveAsSheetmetalFileType
        ' Format: Parasolid (*.xt), IGES (*.igs)
        NewExtension = Configuration("ComboBoxSaveAsDraftFileType")
        NewExtension = NewExtension.Split("*"c)(1)  ' "Parasolid (*.xt)" -> ".xt)"
        NewExtension = NewExtension.Split(")"c)(0)  ' "Parasolid (*.xt)" -> ".xt)"

        NewFileFormat = Configuration("ComboBoxSaveAsDraftFileType")
        NewFileFormat = NewFileFormat.Split("("c)(0)  ' "Parasolid (*.xt)" -> "Parasolid "

        DraftBaseFilename = System.IO.Path.GetFileName(SEDoc.FullName)

        ' CheckBoxSaveAsDraftOutputDirectory
        If Configuration("CheckBoxSaveAsDraftOutputDirectory") = "False" Then
            BaseDir = Configuration("TextBoxSaveAsDraftOutputDirectory")

            If Configuration("CheckBoxSaveAsFormulaDraft").ToLower = "true" Then
                Formula = Configuration("TextBoxSaveAsFormulaDraft")

                SubDir = CommonTasks.SubstitutePropertyFormula(CType(SEDoc, SolidEdgeFramework.SolidEdgeDocument), Formula)
                SubDir = FCD.SubstituteIllegalCharacters(SubDir)

                BaseDir = String.Format("{0}\{1}", BaseDir, SubDir)
                If Not FileIO.FileSystem.DirectoryExists(BaseDir) Then
                    Try
                        FileIO.FileSystem.CreateDirectory(BaseDir)
                    Catch ex As Exception
                        Proceed = False
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Could not create '{0}'", BaseDir))
                    End Try
                End If

            End If

            If Proceed Then
                NewFilename = BaseDir + "\" + System.IO.Path.ChangeExtension(DraftBaseFilename, NewExtension)
            End If

        Else
            NewFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, NewExtension)
        End If

        If Configuration("CheckBoxWatermark") = "True" Then
            Dim Sections As SolidEdgeDraft.Sections
            Dim Section As SolidEdgeDraft.Section
            Dim SectionSheets As SolidEdgeDraft.SectionSheets
            Dim OriginalSheet As SolidEdgeDraft.Sheet
            Dim Sheet As SolidEdgeDraft.Sheet
            Dim Watermark As SolidEdgeFrameworkSupport.Image2d
            Dim SheetWindow As SolidEdgeDraft.SheetWindow

            Dim ImageFilename As String = Configuration("TextBoxWatermarkFilename")
            Dim ImageX As Double
            Dim ImageY As Double
            Dim X As Double = CDbl(Configuration("TextBoxWatermarkX"))
            Dim Y As Double = CDbl(Configuration("TextBoxWatermarkY"))
            Dim ImageScale As Double = CDbl(Configuration("TextBoxWatermarkScale"))
            Dim SheetW As Double
            Dim SheetH As Double


            OriginalSheet = SEDoc.ActiveSheet
            Sections = SEDoc.Sections
            Section = Sections.BackgroundSection
            SectionSheets = Section.Sheets

            For Each Sheet In SectionSheets
                Sheet.Activate()
                SheetW = Sheet.SheetSetup.SheetWidth
                SheetH = Sheet.SheetSetup.SheetHeight
                Watermark = Sheet.Images2d.AddImage(False, ImageFilename)
                Watermark.Height = ImageScale * Watermark.Height
                Watermark.ShowBorder = False
                ImageX = X * SheetW - Watermark.Width / 2
                ImageY = Y * SheetH - Watermark.Height / 2
                ' Watermark.GetOrigin(ImageX, ImageY)
                Watermark.SetOrigin(ImageX, ImageY)

                SEApp.DoIdle()
            Next

            OriginalSheet.Activate()
            SEApp.DoIdle()

            SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
            SheetWindow.DisplayBackgroundSheetTabs = False
            SEApp.DoIdle()

        End If

        'Capturing a fault to update ExitStatus
        Try
            If Not Configuration("ComboBoxSaveAsDraftFileType").ToLower.Contains("copy") Then
                If Not NewFileFormat.Contains("PDF per Sheet") Then
                    SEDoc.SaveAs(NewFilename)
                    SEApp.DoIdle()

                Else  ' Save as one pdf file per sheet

                    Dim PreviousSetting As Object = Nothing
                    Dim SheetList As New List(Of SolidEdgeDraft.Sheet)
                    Dim Sheet As SolidEdgeDraft.Sheet
                    Dim SheetName As String
                    Dim tmpNewFilename As String

                    ' seApplicationGlobalDraftSaveAsPDFSheetOptions (same mapping as SolidEdgeConstants.DraftSaveAsPDFSheetOptionsConstants)
                    ' 0: Active sheet only
                    ' 1: All sheets
                    ' 2: Sheets: (sheet number and/or ranges)

                    SEApp.GetGlobalParameter(
                        SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalDraftSaveAsPDFSheetOptions,
                        PreviousSetting)
                    SEApp.SetGlobalParameter(
                        SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalDraftSaveAsPDFSheetOptions,
                        0)

                    SheetList = GetSheets(SEDoc, "Working")

                    tmpNewFilename = NewFilename

                    For Each Sheet In SheetList
                        Sheet.Activate()

                        SheetName = String.Format("-{0}", Sheet.Name)
                        SheetName = FCD.SubstituteIllegalCharacters(SheetName)
                        If Configuration("CheckBoxSaveAsPDFPerSheetSupress").ToLower = "true" Then
                            If SheetList.Count = 1 Then
                                SheetName = ""
                            End If
                        End If

                        NewFilename = tmpNewFilename.Substring(0, tmpNewFilename.Count - 4)
                        NewFilename = String.Format("{0}{1}.pdf", NewFilename, SheetName)
                        SEDoc.SaveAs(NewFilename)
                        SEApp.DoIdle()

                    Next

                    SEApp.SetGlobalParameter(
                        SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalDraftSaveAsPDFSheetOptions,
                        PreviousSetting)

                End If
            Else
                If Configuration("CheckBoxSaveAsDraftOutputDirectory").ToLower = "false" Then
                    SEDoc.SaveCopyAs(NewFilename)
                    SEApp.DoIdle()
                Else
                    ExitStatus = 1
                    ErrorMessageList.Add("Can not SaveCopyAs to the original directory")
                    Proceed = False
                End If
            End If

            'SEDoc.SaveAs(NewFilename)
            'SEApp.DoIdle()
        Catch ex As Exception
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Error saving file {0}", CommonTasks.TruncateFullPath(NewFilename, Configuration)))
        End Try

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function





    Public Function Print(
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
                                   AddressOf PrintInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function PrintInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim PD As New PrinterDoctor
        Dim AllSheets As New Dictionary(Of String, SolidEdgeDraft.PaperSizeConstants)
        AllSheets = PD.GetSheetSizes("All")

        Dim s As String
        Dim idx As Integer

        Dim DraftPrinterList As New List(Of SolidEdgeDraft.DraftPrintUtility)
        Dim DraftPrinterIdxList As New List(Of Integer)

        Dim DraftPrinter1 As SolidEdgeDraft.DraftPrintUtility = Nothing

        Dim DraftPrinter2 As SolidEdgeDraft.DraftPrintUtility = Nothing
        Dim Printer2SelectedSheets As New List(Of SolidEdgeDraft.PaperSizeConstants)

        Dim DraftPrinterX As SolidEdgeDraft.DraftPrintUtility = Nothing

        Dim Sheets As New List(Of SolidEdgeDraft.Sheet)
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim SheetSetup As SolidEdgeDraft.SheetSetup
        Dim PaperSizeConstant As SolidEdgeDraft.PaperSizeConstants

        If Configuration("CheckBoxEnablePrinter1").ToLower = "true" Then
            DraftPrinter1 = CType(SEApp.GetDraftPrintUtility(), SolidEdgeDraft.DraftPrintUtility)
            DraftPrinterList.Add(DraftPrinter1)
            DraftPrinterIdxList.Add(1)
        End If

        If Configuration("CheckBoxEnablePrinter2").ToLower = "true" Then
            DraftPrinter2 = CType(SEApp.GetDraftPrintUtility(), SolidEdgeDraft.DraftPrintUtility)
            DraftPrinterList.Add(DraftPrinter2)
            DraftPrinterIdxList.Add(1)

            For Each s In Split(Configuration("TextBoxPrinter2SheetSelections"), Delimiter:=",")
                Printer2SelectedSheets.Add(AllSheets(s.Trim))
            Next
        End If

        For i As Integer = 0 To DraftPrinterList.Count - 1

            DraftPrinterX = DraftPrinterList(i)
            idx = DraftPrinterIdxList(i)

            DraftPrinterX.Units = SolidEdgeDraft.DraftPrintUnitsConstants.igDraftPrintInches

            s = "ComboBoxPrinterX".Replace("PrinterX", String.Format("Printer{0}", idx))
            DraftPrinterX.Printer = Configuration(s)

            s = Configuration("TextBoxPrinterXCopies".Replace("PrinterX", String.Format("Printer{0}", idx)))
            DraftPrinterX.Copies = CShort(s)

            DraftPrinterX.AutoOrient = False
            DraftPrinterX.BestFit = False
            DraftPrinterX.PrintAsBlack = False
            DraftPrinterX.ScaleLineTypes = False
            DraftPrinterX.ScaleLineWidths = False

            s = "CheckBoxPrinterXAutoOrient".Replace("PrinterX", String.Format("Printer{0}", idx))
            If Configuration(s).ToLower = "true" Then
                DraftPrinterX.AutoOrient = True
            End If

            s = "CheckBoxPrinterXBestFit".Replace("PrinterX", String.Format("Printer{0}", idx))
            If Configuration(s).ToLower = "true" Then
                DraftPrinterX.BestFit = True
            End If

            s = "CheckBoxPrinterXPrintAsBlack".Replace("PrinterX", String.Format("Printer{0}", idx))
            If Configuration(s).ToLower = "true" Then
                DraftPrinterX.PrintAsBlack = True
            End If

            s = "CheckBoxPrinterXScaleLineTypes".Replace("PrinterX", String.Format("Printer{0}", idx))
            If Configuration(s).ToLower = "true" Then
                DraftPrinterX.ScaleLineTypes = True
            End If

            s = "CheckBoxPrinterXScaleLineWidths".Replace("PrinterX", String.Format("Printer{0}", idx))
            If Configuration(s).ToLower = "true" Then
                DraftPrinterX.ScaleLineWidths = True
            End If

        Next

        If Configuration("CheckBoxEnablePrinter2").ToLower = "true" Then
            Sheets = GetSheets(SEDoc, "Working")
            For Each Sheet In Sheets
                SheetSetup = Sheet.SheetSetup
                PaperSizeConstant = SheetSetup.SheetSizeOption
                Try
                    If Printer2SelectedSheets.Contains(PaperSizeConstant) Then
                        DraftPrinter2.AddSheet(Sheet)
                        DraftPrinter2.PrintOut()
                        SEApp.DoIdle()
                    Else
                        If Configuration("CheckBoxEnablePrinter1").ToLower = "true" Then
                            DraftPrinter1.AddSheet(Sheet)
                            DraftPrinter1.PrintOut()
                            SEApp.DoIdle()
                        End If
                    End If
                Catch ex As Exception
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("Print drawing sheet {0} did not succeed", Sheet.Name))
                End Try
            Next
        Else
            Try
                DraftPrinter1.AddDocument(SEDoc)
                DraftPrinter1.PrintOut()
                SEApp.DoIdle()
            Catch ex As Exception
                ExitStatus = 1
                ErrorMessageList.Add("Print drawing did not succeed")
            End Try
        End If

        If Configuration("CheckBoxEnablePrinter1").ToLower = "true" Then
            DraftPrinter1.RemoveAllDocuments()
            SEApp.DoIdle()
        End If

        If Configuration("CheckBoxEnablePrinter2").ToLower = "true" Then
            DraftPrinter2.RemoveAllDocuments()
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function InteractiveEdit(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeFramework.SolidEdgeDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf CommonTasks.InteractiveEdit,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function


    Public Function RunExternalProgram(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim ExternalProgram As String = Configuration("TextBoxExternalProgramDraft")

        ErrorMessage = InvokeSTAThread(
                            Of String,
                            SolidEdgeFramework.SolidEdgeDocument,
                            Dictionary(Of String, String),
                            SolidEdgeFramework.Application,
                            Dictionary(Of Integer, List(Of String)))(
                                AddressOf CommonTasks.RunExternalProgram,
                                ExternalProgram,
                                SEDoc,
                                Configuration,
                                SEApp)

        Return ErrorMessage

    End Function



    Public Function DrawingViewOnBackgroundSheet(
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
                                   AddressOf DrawingViewOnBackgroundSheetInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function DrawingViewOnBackgroundSheetInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim BackgroundSheet As SolidEdgeDraft.Sheet
        For Each BackgroundSheet In GetSheets(SEDoc, "Background")
            If BackgroundSheet.DrawingViews.Count > 0 Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Drawing view found on background '{0}'.", BackgroundSheet.Name))

            End If
        Next

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Public Function PropertyFindReplace(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'ErrorMessage = InvokeSTAThread(
        '                       Of SolidEdgePart.SheetMetalDocument,
        '                       Dictionary(Of String, String),
        '                       SolidEdgeFramework.Application,
        '                       Dictionary(Of Integer, List(Of String)))(
        '                           AddressOf PropertyFindReplaceInternal,
        '                           CType(SEDoc, SolidEdgePart.SheetMetalDocument),
        '                           Configuration,
        '                           SEApp)

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeFramework.SolidEdgeDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf CommonTasks.PropertyFindReplace,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function


    Public Function Dummy(
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
                                   AddressOf DummyInternal,
                                   CType(SEDoc, SolidEdgeDraft.DraftDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function DummyInternal(
        ByVal SEDoc As SolidEdgeDraft.DraftDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

End Class
