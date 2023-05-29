Option Strict On

Imports SolidEdgeCommunity

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

        ModelLinks = SEDoc.ModelLinks

        For Each ModelLink In ModelLinks
            If Not FileIO.FileSystem.FileExists(ModelLink.FileName) Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0} not found", CommonTasks.TruncateFullPath(ModelLink.FileName, Configuration)))
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
                ErrorMessageList.Add(String.Format("{0} not found", CommonTasks.TruncateFullPath(ModelLink.FileName, Configuration)))
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
            ErrorMessageList.Add("Problem updating drawing border")
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

    '    Dim CheckAutoGeneratedFile As Func(Of SolidEdgeDraft.DraftDocument, Boolean)
    '    CheckAutoGeneratedFile = Function(SourceDoc)
    '                                 Dim Filename As String = System.IO.Path.GetFileName(SourceDoc.FullName)

    '                                 If Filename.Contains("-HousekeeperOld") Then
    '                                     ExitStatus = 1
    '                                     ErrorMessageList.Add(String.Format("Auto-generated file not processed {0}", Filename))
    '                                     Return True
    '                                 Else
    '                                     Return False
    '                                 End If
    '                             End Function

    '    Dim GetNewDocFilename As Func(Of SolidEdgeDraft.DraftDocument, String)
    '    GetNewDocFilename = Function(SourceDoc)
    '                            Dim NewFilename As String
    '                            NewFilename = String.Format("{0}\{1}-Housekeeper.dft",
    '                                                        System.IO.Path.GetDirectoryName(SEDoc.FullName),
    '                                                        System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))
    '                            Return NewFilename
    '                        End Function

    '    Dim GetRemnantsDocFilename As Func(Of SolidEdgeDraft.DraftDocument, String)
    '    GetRemnantsDocFilename = Function(SourceDoc)
    '                                 Dim NewFilename As String
    '                                 NewFilename = String.Format("{0}\{1}-HousekeeperOld.dft",
    '                                                             System.IO.Path.GetDirectoryName(SEDoc.FullName),
    '                                                             System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))
    '                                 Return NewFilename
    '                             End Function

    '    Dim GetSheets As Func(Of SolidEdgeDraft.DraftDocument, String, List(Of SolidEdgeDraft.Sheet))
    '    GetSheets = Function(Doc, SectionType)
    '                    Dim SheetList As New List(Of SolidEdgeDraft.Sheet)
    '                    Dim Sheet As SolidEdgeDraft.Sheet
    '                    Dim Section As SolidEdgeDraft.Section = Nothing
    '                    Dim SectionSheets As SolidEdgeDraft.SectionSheets
    '                    Dim SheetGroups As SolidEdgeDraft.SheetGroups
    '                    Dim SheetGroup As SolidEdgeDraft.SheetGroup

    '                    Dim count As Integer

    '                    If SectionType = "Working" Then
    '                        Section = Doc.Sections.WorkingSection
    '                    ElseIf SectionType = "Background" Then
    '                        Section = Doc.Sections.BackgroundSection
    '                    ElseIf SectionType = "2DModel" Then
    '                        Section = Doc.Sections.WorkingSection  ' Ignored below
    '                    ElseIf SectionType = "UserGenerated" Then
    '                        Section = Doc.Sections.WorkingSection
    '                        SheetGroups = CType(Doc.SheetGroups, SolidEdgeDraft.SheetGroups)
    '                    ElseIf SectionType = "AutoGenerated" Then
    '                        Section = Doc.Sections.WorkingSection
    '                        SheetGroups = CType(Doc.SheetGroups, SolidEdgeDraft.SheetGroups)
    '                    Else
    '                        MsgBox(String.Format("SectionType '{0}' not recognized.  Quitting...", SectionType))
    '                    End If

    '                    SectionSheets = Section.Sheets

    '                    If (SectionType = "Working") Or (SectionType = "Background") Then
    '                        For Each Sheet In SectionSheets.OfType(Of SolidEdgeDraft.Sheet)()
    '                            SheetList.Add(Sheet)
    '                        Next
    '                    ElseIf (SectionType = "2DModel") Then
    '                        SheetList.Add(Doc.Sections.Get2DModelSheet)
    '                    Else
    '                        SheetGroups = CType(Doc.SheetGroups, SolidEdgeDraft.SheetGroups)
    '                        count = 0
    '                        For Each SheetGroup In SheetGroups
    '                            For Each Sheet In SheetGroup.Sheets.OfType(Of SolidEdgeDraft.Sheet)()
    '                                If (SectionType = "UserGenerated") And (count = 0) Then
    '                                    SheetList.Add(Sheet)
    '                                End If
    '                                If (SectionType = "AutoGenerated") And (count > 0) Then
    '                                    SheetList.Add(Sheet)
    '                                End If
    '                            Next
    '                            count += 1
    '                        Next
    '                    End If

    '                    Return SheetList
    '                End Function

    '    Dim SheetNameToObject As Func(Of SolidEdgeDraft.DraftDocument, String, String, SolidEdgeDraft.Sheet)
    '    SheetNameToObject = Function(Doc, SectionType, SheetName)
    '                            Dim Sheet As SolidEdgeDraft.Sheet

    '                            For Each Sheet In GetSheets(Doc, SectionType)
    '                                If Sheet.Name = SheetName Then
    '                                    Return Sheet
    '                                End If
    '                            Next
    '                            Return Nothing
    '                        End Function

    '    Dim AddSheet As Action(Of SolidEdgeDraft.DraftDocument, String)
    '    AddSheet = Sub(Doc, SheetName)
    '                   Dim Sheet As SolidEdgeDraft.Sheet
    '                   Dim SheetAlreadyExists As Boolean = False

    '                   For Each Sheet In GetSheets(Doc, "Working")
    '                       If SheetName = Sheet.Name Then
    '                           SheetAlreadyExists = True
    '                       End If
    '                   Next

    '                   If Not SheetAlreadyExists Then
    '                       Doc.Sheets.AddSheet(SheetName, SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection)
    '                   End If
    '               End Sub

    '    Dim SetTargetBackgrounds As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument, String)
    '    SetTargetBackgrounds = Sub(SourceDoc, TargetDoc, DummyName)
    '                               Dim SourceSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "Working")
    '                               Dim TargetSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(TargetDoc, "Working")

    '                               Dim SourceSheet As SolidEdgeDraft.Sheet
    '                               Dim TargetSheet As SolidEdgeDraft.Sheet

    '                               Dim SourceSheetNames As New List(Of String)
    '                               Dim TargetSheetNames As New List(Of String)

    '                               Dim SourceBackgroundSheet As SolidEdgeDraft.Sheet
    '                               Dim TargetBackgroundSheet As SolidEdgeDraft.Sheet

    '                               Dim msg2 As String = ""

    '                               For Each TargetSheet In TargetSheets
    '                                   If Not TargetSheet.Name = DummyName Then
    '                                       SourceSheet = SheetNameToObject(SourceDoc, "Working", TargetSheet.Name)
    '                                       ' SourceSheetNames.Add(SourceSheet.Name)
    '                                       ' Not all sheets have a background defined
    '                                       Try
    '                                           SourceBackgroundSheet = SourceSheet.Background
    '                                           TargetBackgroundSheet = SheetNameToObject(TargetDoc, "Background", SourceBackgroundSheet.Name)
    '                                           If Not TargetBackgroundSheet Is Nothing Then
    '                                               TargetSheet.Background = TargetBackgroundSheet
    '                                               TargetSheet.BackgroundVisible = SourceSheet.BackgroundVisible
    '                                               TargetSheet.SheetSetup.SheetSizeOption = SourceSheet.SheetSetup.SheetSizeOption
    '                                           Else
    '                                               ExitStatus = 1
    '                                               msg2 = String.Format("Template does not have a background named '{0}'", SourceBackgroundSheet.Name)
    '                                               If Not ErrorMessageList.Contains(msg2) Then
    '                                                   ErrorMessageList.Add(msg2)
    '                                               End If
    '                                           End If
    '                                       Catch ex As Exception
    '                                       End Try
    '                                   End If
    '                               Next
    '                           End Sub

    '    Dim AddSheetsToTarget As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument, String)
    '    AddSheetsToTarget = Sub(SourceDoc, TargetDoc, DummyName)
    '                            ' Add sheets to target to match source.
    '                            ' Remove sheets from target that don't match.
    '                            ' Set backgrounds to match.  Report to log if target does not have the background sheet.

    '                            Dim SourceSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "UserGenerated")
    '                            Dim TargetSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(TargetDoc, "UserGenerated")

    '                            Dim SourceSheet As SolidEdgeDraft.Sheet
    '                            Dim TargetSheet As SolidEdgeDraft.Sheet

    '                            Dim SourceSheetNames As New List(Of String)
    '                            Dim TargetSheetNames As New List(Of String)

    '                            For Each SourceSheet In SourceSheets
    '                                SourceSheetNames.Add(SourceSheet.Name)
    '                            Next

    '                            For Each TargetSheet In TargetSheets
    '                                TargetSheetNames.Add(TargetSheet.Name)
    '                            Next

    '                            For Each SourceSheet In SourceSheets
    '                                If Not TargetSheetNames.Contains(SourceSheet.Name) Then
    '                                    AddSheet(TargetDoc, SourceSheet.Name)
    '                                End If
    '                            Next

    '                            For Each TargetSheetName In TargetSheetNames
    '                                If Not SourceSheetNames.Contains(TargetSheetName) Then
    '                                    SheetNameToObject(TargetDoc, "Working", TargetSheetName).Delete()
    '                                End If
    '                            Next

    '                            SetTargetBackgrounds(SourceDoc, TargetDoc, DummyName)

    '                        End Sub

    '    Dim MoveDVsToDummySheet As Func(Of SolidEdgeDraft.DraftDocument, String, List(Of String))
    '    MoveDVsToDummySheet = Function(SourceDoc, SourceDocDummySheetName)
    '                              Dim DVSheetNames As New List(Of String)
    '                              Dim Sheet As SolidEdgeDraft.Sheet
    '                              Dim DrawingView As SolidEdgeDraft.DrawingView
    '                              Dim TargetSheet As SolidEdgeDraft.Sheet = SheetNameToObject(SourceDoc, "UserGenerated", SourceDocDummySheetName)
    '                              Dim msg2 As String

    '                              For Each Sheet In GetSheets(SourceDoc, "UserGenerated")
    '                                  If Not Sheet.Name = SourceDocDummySheetName Then
    '                                      For Each DrawingView In Sheet.DrawingViews
    '                                          DVSheetNames.Add(Sheet.Name)
    '                                          'Dim Name As String = DrawingView.Name
    '                                          ' Issue with broken out section view
    '                                          Try
    '                                              DrawingView.Sheet = TargetSheet
    '                                          Catch ex As Exception
    '                                              ExitStatus = 1
    '                                              msg2 = "Some drawing views may not have transferred"
    '                                              If Not ErrorMessageList.Contains(msg2) Then
    '                                                  ErrorMessageList.Add(msg2)
    '                                              End If
    '                                          End Try
    '                                      Next
    '                                  End If
    '                              Next

    '                              Return DVSheetNames
    '                          End Function


    '    Dim MoveDVsToTarget As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument, List(Of String), String)
    '    MoveDVsToTarget = Sub(SourceDoc, TargetDoc, DrawingViewSheetnames, DummyName)


    '                          Dim SourceSheet As SolidEdgeDraft.Sheet = SheetNameToObject(SourceDoc, "UserGenerated", DummyName)
    '                          Dim TargetSheet As SolidEdgeDraft.Sheet = SheetNameToObject(TargetDoc, "UserGenerated", DummyName)
    '                          Dim SheetWindow As SolidEdgeDraft.SheetWindow
    '                          Dim DrawingView As SolidEdgeDraft.DrawingView

    '                          If DrawingViewSheetnames.Count > 0 Then
    '                              ' Sometimes get cut/paste error on drawing views.  Need a do-over.  Don't save anything.
    '                              Try
    '                                  SourceDoc.Activate()
    '                                  SourceSheet.Activate()
    '                                  SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

    '                                  For Each DrawingView In SourceSheet.DrawingViews
    '                                      DrawingView.Select()
    '                                      DrawingView.AddConnectedAnnotationsToSelectSet()
    '                                      DrawingView.AddConnectedDimensionsToSelectSet()
    '                                  Next

    '                                  SEApp.DoIdle()
    '                                  SheetWindow.Cut()
    '                                  SEApp.DoIdle()

    '                                  TargetDoc.Activate()
    '                                  TargetSheet.Activate()
    '                                  SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '                                  SheetWindow.Paste()
    '                                  SEApp.DoIdle()
    '                              Catch ex As Exception
    '                                  ExitStatus = 2
    '                                  Exit Sub
    '                              End Try

    '                          End If

    '                      End Sub

    '    Dim MoveDVsToCorrectSheet As Action(Of SolidEdgeDraft.DraftDocument, List(Of String), String)
    '    MoveDVsToCorrectSheet = Sub(TargetDoc, DrawingViewSheetNames, DummyName)

    '                                Dim DrawingView As SolidEdgeDraft.DrawingView

    '                                Dim DummySheet As SolidEdgeDraft.Sheet = SheetNameToObject(TargetDoc, "UserGenerated", DummyName)
    '                                Dim count As Integer = 0
    '                                Dim msg2 As String
    '                                Dim tf2 As Boolean

    '                                If DrawingViewSheetNames.Count > 0 Then
    '                                    TargetDoc.Activate()

    '                                    For Each DrawingView In DummySheet.DrawingViews
    '                                        Dim Name As String = DrawingView.Name

    '                                        tf2 = DrawingView.IsBrokenOutSectionTarget
    '                                        ' Issue with broken out section view
    '                                        Try
    '                                            DrawingView.Sheet = SheetNameToObject(TargetDoc, "UserGenerated", DrawingViewSheetNames(count))
    '                                        Catch ex As Exception
    '                                            ExitStatus = 1
    '                                            msg2 = "Some drawing views may not have transferred"
    '                                            If Not ErrorMessageList.Contains(msg2) Then
    '                                                ErrorMessageList.Add(msg2)
    '                                            End If
    '                                        End Try
    '                                        count += 1
    '                                    Next
    '                                End If

    '                            End Sub

    '    Dim UpdateRange As Func(Of Double, Double, Double, Double, List(Of Double), List(Of Double))
    '    UpdateRange = Function(XMin, YMin, XMax, YMax, Ranges)
    '                      Dim NewRanges As New List(Of Double)

    '                      If XMin < Ranges(0) Then
    '                          NewRanges.Add(XMin)
    '                      Else
    '                          NewRanges.Add(Ranges(0))
    '                      End If

    '                      If YMin < Ranges(1) Then
    '                          NewRanges.Add(YMin)
    '                      Else
    '                          NewRanges.Add(Ranges(1))
    '                      End If

    '                      If XMax > Ranges(2) Then
    '                          NewRanges.Add(XMax)
    '                      Else
    '                          NewRanges.Add(Ranges(2))
    '                      End If

    '                      If YMax > Ranges(3) Then
    '                          NewRanges.Add(YMax)
    '                      Else
    '                          NewRanges.Add(Ranges(3))
    '                      End If

    '                      Return NewRanges
    '                  End Function

    '    Dim GetCenter As Func(Of SolidEdgeDraft.DrawingView, List(Of Double))
    '    GetCenter = Function(DrawingView)
    '                    Dim Center As New List(Of Double)
    '                    Dim CenterX As Double
    '                    Dim CenterY As Double

    '                    DrawingView.GetOrigin(CenterX, CenterY)
    '                    Center.Add(CenterX)
    '                    Center.Add(CenterY)
    '                    Return Center
    '                End Function

    '    Dim CloseEnough As Func(Of Double, Double, Double, Boolean)
    '    CloseEnough = Function(D1, D2, MaxDiff)
    '                      If Math.Abs(D1 - D2) < MaxDiff Then
    '                          Return True
    '                      Else
    '                          Return False
    '                      End If

    '                  End Function

    '    Dim GetAlignedDVs As Func(Of SolidEdgeDraft.Sheet, Dictionary(Of SolidEdgeDraft.DrawingView, List(Of Double)), List(Of SolidEdgeDraft.DrawingView))
    '    GetAlignedDVs = Function(Sheet, DVCenters)
    '                        Dim AlignedDVs As New List(Of SolidEdgeDraft.DrawingView)
    '                        Dim DrawingView As SolidEdgeDraft.DrawingView
    '                        Dim DVList As New List(Of SolidEdgeDraft.DrawingView)
    '                        Dim DVX As New Dictionary(Of Integer, Double)
    '                        Dim DVY As New Dictionary(Of Integer, Double)
    '                        Dim SortedDictionary As New Dictionary(Of Integer, Double)
    '                        Dim XOrder As New List(Of Integer)
    '                        Dim YOrder As New List(Of Integer)

    '                        Dim count As Integer
    '                        Dim i As Integer
    '                        Dim x1 As Double
    '                        Dim x2 As Double
    '                        Dim y1 As Double
    '                        Dim y2 As Double

    '                        count = 0
    '                        For Each DrawingView In Sheet.DrawingViews
    '                            DVList.Add(DrawingView)
    '                            DVX(count) = DVCenters(DrawingView)(0)
    '                            DVY(count) = DVCenters(DrawingView)(1)
    '                            count += 1
    '                        Next

    '                        Dim XSorted = From pair In DVX
    '                                      Order By pair.Value
    '                        SortedDictionary = XSorted.ToDictionary(Function(p) p.Key, Function(p) p.Value)
    '                        XOrder = SortedDictionary.Keys.ToList

    '                        Dim YSorted = From pair In DVY
    '                                      Order By pair.Value
    '                        SortedDictionary = YSorted.ToDictionary(Function(p) p.Key, Function(p) p.Value)
    '                        YOrder = SortedDictionary.Keys.ToList

    '                        For i = 1 To XOrder.Count - 1
    '                            x1 = DVCenters(DVList(XOrder(i - 1)))(0)
    '                            x2 = DVCenters(DVList(XOrder(i)))(0)
    '                            If CloseEnough(x1, x2, 0.000001) Then
    '                                AlignedDVs.Add(DVList(XOrder(i - 1)))
    '                                AlignedDVs.Add(DVList(XOrder(i)))
    '                            End If
    '                        Next

    '                        For i = 1 To YOrder.Count - 1
    '                            y1 = DVCenters(DVList(YOrder(i - 1)))(1)
    '                            y2 = DVCenters(DVList(YOrder(i)))(1)
    '                            If CloseEnough(y1, y2, 0.000001) Then
    '                                AlignedDVs.Add(DVList(YOrder(i - 1)))
    '                                AlignedDVs.Add(DVList(YOrder(i)))
    '                            End If
    '                        Next

    '                        AlignedDVs = AlignedDVs.Distinct.ToList

    '                        Return AlignedDVs
    '                    End Function

    '    Dim AlignSheetViews As Action(Of SolidEdgeDraft.Sheet)
    '    AlignSheetViews = Sub(Sheet)
    '                          Dim DrawingView As SolidEdgeDraft.DrawingView
    '                          Dim DVCenters As New Dictionary(Of SolidEdgeDraft.DrawingView, List(Of Double))
    '                          Dim DVX As New Dictionary(Of Integer, Double)
    '                          Dim DVY As New Dictionary(Of Integer, Double)
    '                          Dim AlignedDrawingViews As New List(Of SolidEdgeDraft.DrawingView)
    '                          Dim SheetWindow As SolidEdgeDraft.SheetWindow
    '                          Dim SelectSet As SolidEdgeFramework.SelectSet

    '                          If Sheet.DrawingViews.Count > 1 Then
    '                              For Each DrawingView In Sheet.DrawingViews
    '                                  DVCenters(DrawingView) = GetCenter(DrawingView)
    '                              Next

    '                              AlignedDrawingViews = GetAlignedDVs(Sheet, DVCenters)

    '                              If AlignedDrawingViews.Count > 1 Then
    '                                  Sheet.Activate()
    '                                  SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '                                  SelectSet = SheetWindow.SelectSet

    '                                  For Each DrawingView In AlignedDrawingViews
    '                                      SelectSet.Add(DrawingView)
    '                                  Next

    '                                  SEApp.StartCommand(
    '                                      CType(SolidEdgeConstants.DetailCommandConstants.DetailRelationshipsAlignViews,
    '                                            SolidEdgeFramework.SolidEdgeCommandConstants))
    '                              End If

    '                          End If

    '                      End Sub


    '    Dim AlignViews As Action(Of SolidEdgeDraft.DraftDocument)
    '    AlignViews = Sub(TargetDoc)
    '                     Dim Sheet As SolidEdgeDraft.Sheet

    '                     For Each Sheet In GetSheets(TargetDoc, "UserGenerated")
    '                         AlignSheetViews(Sheet)
    '                     Next
    '                 End Sub

    '    Dim Move2DModelsToTarget As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument)
    '    Move2DModelsToTarget = Sub(SourceDoc, TargetDoc)
    '                               Dim SheetWindow As SolidEdgeDraft.SheetWindow
    '                               'Dim Sections As SolidEdgeDraft.Sections

    '                               Dim Source2DSheet As SolidEdgeDraft.Sheet = GetSheets(SourceDoc, "2DModel")(0)
    '                               Dim Target2DSheet As SolidEdgeDraft.Sheet = GetSheets(TargetDoc, "2DModel")(0)

    '                               Dim DrawingObjects As SolidEdgeFrameworkSupport.DrawingObjects
    '                               Dim SelectSet As SolidEdgeFramework.SelectSet

    '                               DrawingObjects = Source2DSheet.DrawingObjects

    '                               If DrawingObjects.Count > 0 Then
    '                                   Try
    '                                       SourceDoc.Activate()
    '                                       Source2DSheet.Activate()
    '                                       SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '                                       SelectSet = SheetWindow.SelectSet

    '                                       For Each DrawingObject In DrawingObjects
    '                                           SelectSet.Add(DrawingObject)
    '                                       Next

    '                                       SEApp.DoIdle()
    '                                       SheetWindow.Cut()
    '                                       SEApp.DoIdle()
    '                                       SheetWindow.Display2DModelSheetTab = False
    '                                       SheetWindow.DisplayBackgroundSheetTabs = False

    '                                       TargetDoc.Activate()
    '                                       Target2DSheet.Activate()
    '                                       SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

    '                                       SheetWindow.Paste()
    '                                       SEApp.DoIdle()
    '                                       SheetWindow.Display2DModelSheetTab = False
    '                                       SheetWindow.DisplayBackgroundSheetTabs = False

    '                                   Catch ex As Exception
    '                                       ExitStatus = 2
    '                                       Exit Sub
    '                                   End Try
    '                               End If

    '                           End Sub

    '    Dim MoveRemainingItemsToTarget As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument)
    '    MoveRemainingItemsToTarget = Sub(SourceDoc, TargetDoc)
    '                                     Dim SheetWindow As SolidEdgeDraft.SheetWindow
    '                                     Dim SourceSheet As SolidEdgeDraft.Sheet
    '                                     Dim TargetSheet As SolidEdgeDraft.Sheet
    '                                     Dim DrawingObjects As SolidEdgeFrameworkSupport.DrawingObjects
    '                                     Dim SelectSet As SolidEdgeFramework.SelectSet

    '                                     Dim msg2 As String = ""

    '                                     For Each SourceSheet In GetSheets(SourceDoc, "UserGenerated")
    '                                         If SourceSheet.DrawingObjects.Count > 0 Then
    '                                             SourceDoc.Activate()
    '                                             SourceSheet.Activate()
    '                                             SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '                                             SheetWindow.FitEx(SolidEdgeDraft.SheetFitConstants.igFitAll)
    '                                             SelectSet = SheetWindow.SelectSet

    '                                             DrawingObjects = SourceSheet.DrawingObjects
    '                                             For Each DrawingObject In DrawingObjects
    '                                                 SelectSet.Add(DrawingObject)
    '                                             Next
    '                                             ' Catch copy/paste problem
    '                                             Try
    '                                                 SEApp.DoIdle()
    '                                                 SheetWindow.Cut()
    '                                                 SEApp.DoIdle()

    '                                                 TargetDoc.Activate()
    '                                                 TargetSheet = SheetNameToObject(TargetDoc, "UserGenerated", SourceSheet.Name)
    '                                                 TargetSheet.Activate()
    '                                                 SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

    '                                                 SheetWindow.Paste()
    '                                                 SEApp.DoIdle()

    '                                             Catch ex As Exception
    '                                                 ExitStatus = 2
    '                                                 Exit Sub
    '                                             End Try
    '                                         End If
    '                                     Next

    '                                 End Sub

    '    Dim CheckAutoGeneratedSheets As Action(Of SolidEdgeDraft.DraftDocument)
    '    CheckAutoGeneratedSheets = Sub(SourceDoc)
    '                                   Dim AutoGeneratedSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "AutoGenerated")
    '                                   Dim msg2 As String = ""

    '                                   If AutoGeneratedSheets.Count > 0 Then
    '                                       ExitStatus = 1
    '                                       ErrorMessageList.Add("Auto generated sheets not processed.  Please regenerate.")
    '                                       For Each Sheet In AutoGeneratedSheets
    '                                           msg2 += String.Format("{0}, ", Sheet.Name)
    '                                       Next
    '                                       ErrorMessageList.Add(msg2)
    '                                   End If
    '                               End Sub

    '    Dim CheckDVsOnBackground As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument, String)
    '    CheckDVsOnBackground = Sub(SourceDoc, TargetDoc, DummyName)
    '                               Dim BackgroundSheet As SolidEdgeDraft.Sheet
    '                               Dim SheetWindow As SolidEdgeDraft.SheetWindow
    '                               Dim DrawingView As SolidEdgeDraft.DrawingView
    '                               Dim TargetSheet As SolidEdgeDraft.Sheet = SheetNameToObject(TargetDoc, "UserGenerated", DummyName)

    '                               Dim msg2 As String = ""

    '                               For Each BackgroundSheet In GetSheets(SourceDoc, "Background")
    '                                   If BackgroundSheet.DrawingViews.Count > 0 Then
    '                                       ExitStatus = 1
    '                                       msg2 = String.Format("Drawing view found on background '{0}'.  Moved to sheet '{1}'.", BackgroundSheet.Name, DummyName)
    '                                       ErrorMessageList.Add(msg2)
    '                                       msg2 = "Verify all items were transferred from "
    '                                       msg2 += String.Format("{0}", System.IO.Path.GetFileName(GetRemnantsDocFilename(SEDoc)))
    '                                       ErrorMessageList.Add(String.Format("    {0}", msg2))

    '                                       SourceDoc.Activate()
    '                                       BackgroundSheet.Activate()
    '                                       SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

    '                                       For Each DrawingView In BackgroundSheet.DrawingViews
    '                                           DrawingView.Select()
    '                                           DrawingView.AddConnectedAnnotationsToSelectSet()
    '                                           DrawingView.AddConnectedDimensionsToSelectSet()
    '                                       Next

    '                                       ' Catch copy/paste problems
    '                                       Try
    '                                           SheetWindow.Cut()

    '                                           TargetDoc.Activate()
    '                                           TargetSheet.Activate()
    '                                           SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '                                           SheetWindow.Paste()

    '                                       Catch ex As Exception
    '                                           ExitStatus = 2
    '                                           Exit Sub
    '                                       End Try

    '                                   End If
    '                               Next
    '                           End Sub

    '    Dim CheckForOrphanedItems As Action(Of SolidEdgeDraft.DraftDocument, String)
    '    CheckForOrphanedItems = Sub(TargetDoc, DummyName)
    '                                Dim TargetSheet As SolidEdgeDraft.Sheet = SheetNameToObject(TargetDoc, "UserGenerated", DummyName)
    '                                Dim ContainsSubstring As Boolean = False
    '                                Dim s As String

    '                                TargetDoc.Activate()

    '                                If TargetSheet.DrawingObjects.Count > 0 Then
    '                                    ExitStatus = 1
    '                                    For Each s In ErrorMessageList
    '                                        If s.Contains("Drawing view found on sheet") Then
    '                                            ContainsSubstring = True
    '                                        End If
    '                                    Next
    '                                    If Not ContainsSubstring Then
    '                                        ErrorMessageList.Add(String.Format("Orphaned items on sheet '{0}'", DummyName))
    '                                        ErrorMessageList.Add(String.Format(
    '                                                         "    Please transfer to the correct sheet, then delete '{0}'", DummyName))
    '                                    End If
    '                                Else
    '                                    TargetDoc.Sheets.Item(1).Activate()
    '                                    TargetSheet.Delete()
    '                                End If

    '                            End Sub

    '    Dim TidyUp As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument, String)
    '    TidyUp = Sub(SourceDoc, TargetDoc, DummyName)
    '                 Dim SourceSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "UserGenerated")
    '                 Dim TargetSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(TargetDoc, "UserGenerated")
    '                 Dim SheetWindow As SolidEdgeDraft.SheetWindow

    '                 SourceDoc.Activate()
    '                 SourceSheets(0).Activate()
    '                 SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '                 SheetWindow.Display2DModelSheetTab = False
    '                 'SheetWindow.DisplayBackgroundSheetTabs = False

    '                 For Each SourceSheet In SourceSheets
    '                     If SourceSheet.Name = DummyName Then
    '                         SourceSheet.Delete()
    '                         Exit For
    '                     End If
    '                 Next

    '                 TargetDoc.Activate()
    '                 For Each TargetSheet In TargetSheets
    '                     TargetSheet.Activate()
    '                     SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '                     SheetWindow.Display2DModelSheetTab = False
    '                     SheetWindow.DisplayBackgroundSheetTabs = False
    '                     SheetWindow.FitEx(SolidEdgeDraft.SheetFitConstants.igFitSheet)
    '                 Next
    '                 TargetSheets(0).Activate()

    '             End Sub

    '    Dim NewDoc As SolidEdgeDraft.DraftDocument

    '    Dim NewDocFilename As String
    '    Dim SEDocDVSheetNames As New List(Of String)  ' List of sheet names in order of drawing views

    '    Dim msg As String = ""
    '    Dim tf As Boolean

    '    'Dim SEDocUserSheetGroupSheetNames As New List(Of String)
    '    'Dim SEDocAutoSheetGroupSheetNames As New List(Of String)

    '    Dim DummySheetName As String = "Housekeeper"

    '    ' Open target document
    '    NewDocFilename = GetNewDocFilename(SEDoc)
    '    NewDoc = CType(SEApp.Documents.Add("SolidEdge.DraftDocument",
    '                                       Configuration("TextBoxTemplateDraft")),
    '                                       SolidEdgeDraft.DraftDocument)

    '    ' Make sure the file is not an auto-generated file from previous runs of this task
    '    ' Note an AutoGenerateFile is not the same thing as as AutoGeneratedSheet.
    '    ' The former is created by Housekeeper and may be left over if not everything transfered.
    '    ' The latter is a set of sheets SE creates, for example for a BOM that is configured to be placed
    '    ' on its own sheet(s)

    '    If Not CheckAutoGeneratedFile(SEDoc) Then
    '        ' Create dummy sheet source document
    '        AddSheet(SEDoc, DummySheetName)

    '        ' Create new sheets in target.  Delete any sheets in target not in source.  Set background each sheet.
    '        AddSheetsToTarget(SEDoc, NewDoc, DummySheetName)

    '        ' Move source drawing views to dummy sheet.  Return original drawing view sheet names
    '        SEDocDVSheetNames = MoveDVsToDummySheet(SEDoc, DummySheetName)

    '        ' Move source drawing views to target dummy sheet
    '        ' If this fails, it sets ExitStatus = 2.  Time to bail.
    '        MoveDVsToTarget(SEDoc, NewDoc, SEDocDVSheetNames, DummySheetName)

    '        If Not ExitStatus = 2 Then
    '            ' Move target drawing views to correct sheets using original drawing view sheet names
    '            MoveDVsToCorrectSheet(NewDoc, SEDocDVSheetNames, DummySheetName)

    '            ' Create view alignments that get lost when transferring drawing views to other sheets.
    '            AlignViews(NewDoc)

    '            ' Move source 2D models to target
    '            Move2DModelsToTarget(SEDoc, NewDoc)

    '            If Not ExitStatus = 2 Then
    '                ' Move remaining drawing objects from source to target
    '                MoveRemainingItemsToTarget(SEDoc, NewDoc)

    '                If Not ExitStatus = 2 Then
    '                    ' Check if there are auto-generated sheets in source
    '                    CheckAutoGeneratedSheets(SEDoc)

    '                    ' Check if there are any drawing views on background sheets.  Move to target dummy sheet
    '                    CheckDVsOnBackground(SEDoc, NewDoc, DummySheetName)

    '                    If Not ExitStatus = 2 Then
    '                        ' Check for orphaned drawing objects on target dummy sheet
    '                        CheckForOrphanedItems(NewDoc, DummySheetName)

    '                        ' Tidy up
    '                        TidyUp(SEDoc, NewDoc, DummySheetName)

    '                        tf = Configuration("CheckBoxMoveDrawingViewAllowPartialSuccess") = "True"
    '                        tf = tf And ExitStatus = 1
    '                        tf = tf Or ExitStatus = 0
    '                        If tf Then
    '                            NewDoc.SaveAs(NewDocFilename)
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    '    NewDoc.Close()
    '    SEApp.DoIdle()

    '    tf = Configuration("CheckBoxMoveDrawingViewAllowPartialSuccess") = "True"
    '    tf = tf And ExitStatus = 1

    '    If tf Then
    '        If SEDoc.ReadOnly Then
    '            ExitStatus = 1
    '            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
    '        Else
    '            SEDoc.Save()
    '            SEApp.DoIdle()
    '            Dim RemnantsDocFilename As String
    '            RemnantsDocFilename = String.Format("{0}\{1}-HousekeeperOld.dft",
    '                                                             System.IO.Path.GetDirectoryName(SEDoc.FullName),
    '                                                             System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))

    '            msg = String.Format("After correcting issues, please delete {0}", RemnantsDocFilename)
    '            ErrorMessageList.Add(msg)
    '        End If
    '    ElseIf (Not tf) Or (ExitStatus = 2) Then
    '        If Not tf Then
    '            ErrorMessageList.Add("Partial success, but no changes made.  If desired, change this behavior on the Configuration tab.")
    '        Else
    '            ' Botched cut/paste.  Don't save anything.
    '            ErrorMessageList.Add("Problem with cut/paste.  No changes made.  Please try again or transfer manually.")
    '        End If

    '    End If

    '    ErrorMessage(ExitStatus) = ErrorMessageList
    '    Return ErrorMessage
    'End Function


    ' MoveDrawingToNewTemplate
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

        Dim SupplementalErrorMessageList As New List(Of String)

        Dim NewDoc As SolidEdgeDraft.DraftDocument

        Dim NewDocFilename As String = String.Format("{0}\{1}-Housekeeper.dft",
                                       System.IO.Path.GetDirectoryName(SEDoc.FullName),
                                       System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))
        Dim RemnantsDocFilename As String = NewDocFilename.Replace("Housekeeper", "HousekeeperOld")

        Dim SEDocDVSheetNames As New List(Of String)  ' List of sheet names in order of drawing views

        Dim SheetName As String
        Dim BOSVCountBefore As New Dictionary(Of String, Integer)
        Dim BOSVCountAfter As New Dictionary(Of String, Integer)
        Dim Delta As Integer

        Dim msg As String = ""

        Dim DummySheetName As String = "Housekeeper"

        Dim DVOriginalSheetDict As New Dictionary(Of SolidEdgeDraft.DrawingView, SolidEdgeDraft.Sheet)
        Dim DVIdxOriginalSheetDict As New Dictionary(Of String, Dictionary(Of Integer, String))

        ' Open target document
        NewDoc = CType(SEApp.Documents.Add("SolidEdge.DraftDocument",
                                           Configuration("TextBoxTemplateDraft")),
                                           SolidEdgeDraft.DraftDocument)

        If SEDoc.FullName.Contains("-HousekeeperOld") Then
            ExitStatus = 2
            msg = CommonTasks.TruncateFullPath(SEDoc.FullName, Configuration)
            ErrorMessageList.Add(String.Format("Auto-generated file '{0}' not processed", msg))

        Else

            ' Check for Drawing Views on background sheets
            Dim BackgroundSheet As SolidEdgeDraft.Sheet
            For Each BackgroundSheet In GetSheets(SEDoc, "Background")
                If BackgroundSheet.DrawingViews.Count > 0 Then
                    ExitStatus = 2
                    msg = String.Format("Drawing view found on background '{0}'.", BackgroundSheet.Name)
                    msg = String.Format("{0}  No changes made.  Please transfer to a working sheet and try again.", msg)
                    ErrorMessageList.Add(msg)

                End If
            Next

            If ExitStatus < 2 Then
                ' Create new sheets in target.  Delete any sheets in target not in source.  Set background each sheet.
                SupplementalErrorMessageList = AddSheetsToTarget(SEDoc, NewDoc, DummySheetName)
                If SupplementalErrorMessageList.Count > 0 Then
                    ExitStatus = 1
                    For Each s As String In SupplementalErrorMessageList
                        ErrorMessageList.Add(s)
                    Next
                End If

                ' Records sheet locations for each drawing view
                DVOriginalSheetDict = GetDVOriginalSheet(SEDoc)

                ' Get any broken out section views from the source document
                BOSVCountBefore = GetBOSVCount(SEDoc)

                ' Move drawing views, such as sections and details, to the sheet holding the parent view.
                ' This eliminates duplicate source drawing views when they are cut/pasted from the old doc to the new.
                MoveDVToSourceViewSheet(SEDoc, DVOriginalSheetDict)

                ' Cut/paste changes the drawing view object IDs, but not their item order on the sheet.
                DVIdxOriginalSheetDict = GetDVIdx(SEDoc, DVOriginalSheetDict)

                ExitStatus = TransferSheetContentsToTarget(SEApp, SEDoc, NewDoc)

                If ExitStatus = 2 Then
                    ' Botched cut/paste.  Don't save anything.
                    ErrorMessageList.Add("Problem with cut/paste.  No changes made.  Please update manually.")

                Else
                    ExitStatus = Move2DModelsToTarget(SEApp, SEDoc, NewDoc)

                    If ExitStatus = 2 Then
                        ' Problem transferring 2D model
                        ErrorMessageList.Add("Problem with 2D model.  No changes made.  Please update manually.")

                    Else

                        MoveDVsToTargetSheet(NewDoc, DVIdxOriginalSheetDict)

                        ' Get any broken out section views from the source document
                        BOSVCountAfter = GetBOSVCount(NewDoc)

                        ' Check if any broken out section views did not update correctly
                        For Each SheetName In BOSVCountBefore.Keys
                            If BOSVCountAfter.Keys.Contains(SheetName) Then
                                Delta = BOSVCountBefore(SheetName) - BOSVCountAfter(SheetName)
                                If Delta > 0 Then
                                    ExitStatus = 1
                                    msg = String.Format("Sheet '{0}': Broken Out Section View(s) did not update correctly", SheetName)
                                    ErrorMessageList.Add(msg)
                                End If
                            End If
                        Next

                        ' Check for Parts Lists that were created with the option 'Create new sheets for table'
                        If GetSheets(SEDoc, "AutoGenerated").Count > 0 Then
                            ExitStatus = 1
                            ErrorMessageList.Add("Auto generated sheets (probably a Parts List) not processed.  Please regenerate.")
                            msg = ""
                            For Each Sheet In GetSheets(SEDoc, "AutoGenerated")
                                msg += String.Format("{0}, ", Sheet.Name)
                            Next
                            ErrorMessageList.Add(msg)
                        End If

                        TidyUpUpdateStylesFromTemplate(SEApp, SEDoc, NewDoc)

                    End If

                End If

            End If

        End If


        If ExitStatus = 2 Then
            ' Don't save anything
        ElseIf ExitStatus = 1 Then
            If Configuration("CheckBoxMoveDrawingViewAllowPartialSuccess") = "True" Then
                NewDoc.SaveAs(NewDocFilename)
                SEApp.DoIdle()

                If SEDoc.ReadOnly Then
                    ExitStatus = 1
                    ErrorMessageList.Add("Cannot save document marked 'Read Only'")
                Else
                    SEDoc.Save()
                    SEApp.DoIdle()

                    msg = CommonTasks.TruncateFullPath(RemnantsDocFilename, Configuration)
                    msg = String.Format("After correcting issues, please delete {0}", msg)
                    ErrorMessageList.Add(msg)
                End If
            End If
        ElseIf ExitStatus = 0 Then
            NewDoc.SaveAs(NewDocFilename)
            SEApp.DoIdle()
        End If

        NewDoc.Close()
        SEApp.DoIdle()


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Private Function GetBOSVCount(Doc As SolidEdgeDraft.DraftDocument) As Dictionary(Of String, Integer)
        Dim BOSVCount As New Dictionary(Of String, Integer)
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim count As Integer
        Dim DrawingViews As SolidEdgeDraft.DrawingViews
        Dim DrawingView As SolidEdgeDraft.DrawingView


        For Each Sheet In GetSheets(Doc, "Working")
            count = 0
            DrawingViews = Sheet.DrawingViews
            For Each DrawingView In DrawingViews
                If DrawingView.IsBrokenOutSectionTarget Then
                    count += 1
                End If
            Next
            BOSVCount(Sheet.Name) = count
        Next

        Return BOSVCount
    End Function

    Private Sub TidyUpUpdateStylesFromTemplate(SEApp As SolidEdgeFramework.Application,
                                               SourceDoc As SolidEdgeDraft.DraftDocument,
                                               TargetDoc As SolidEdgeDraft.DraftDocument)

        Dim SourceSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "UserGenerated")
        Dim TargetSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(TargetDoc, "UserGenerated")
        Dim SheetWindow As SolidEdgeDraft.SheetWindow

        SourceDoc.Activate()
        SourceSheets(0).Activate()
        SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
        SheetWindow.Display2DModelSheetTab = False
        'SheetWindow.DisplayBackgroundSheetTabs = False

        'For Each SourceSheet In SourceSheets
        '    If SourceSheet.Name = DummyName Then
        '        SourceSheet.Delete()
        '        Exit For
        '    End If
        'Next

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

    'Dim TidyUp As Action(Of SolidEdgeDraft.DraftDocument, SolidEdgeDraft.DraftDocument, String)
    '    TidyUp = Sub(SourceDoc, TargetDoc, DummyName)

    '             End Sub

    Private Function Move2DModelsToTarget(SEApp As SolidEdgeFramework.Application,
                                          SourceDoc As SolidEdgeDraft.DraftDocument,
                                          TargetDoc As SolidEdgeDraft.DraftDocument
                                          ) As Integer

        Dim ExitStatus As Integer = 0

        Dim SheetWindow As SolidEdgeDraft.SheetWindow
        'Dim Sections As SolidEdgeDraft.Sections

        Dim Source2DSheet As SolidEdgeDraft.Sheet = GetSheets(SourceDoc, "2DModel")(0)
        Dim Target2DSheet As SolidEdgeDraft.Sheet = GetSheets(TargetDoc, "2DModel")(0)

        Dim DrawingObjects As SolidEdgeFrameworkSupport.DrawingObjects
        'Dim SelectSet As SolidEdgeFramework.SelectSet

        DrawingObjects = Source2DSheet.DrawingObjects

        If DrawingObjects.Count > 0 Then
            Try
                SourceDoc.Activate()
                Source2DSheet.Activate()
                SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
                'SelectSet = SheetWindow.SelectSet

                'For Each DrawingObject In DrawingObjects
                '    SelectSet.Add(DrawingObject)
                'Next

                Source2DSheet.FenceLocate(-1000, 1000, 1000, -1000)


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
            End Try
        End If

        Return ExitStatus
    End Function
    'End Sub

    Private Sub MoveDVsToTargetSheet(Doc As SolidEdgeDraft.DraftDocument,
                              DVIdxOriginalSheetDict As Dictionary(Of String, Dictionary(Of Integer, String))
                              )


        Dim Sheet As SolidEdgeDraft.Sheet
        Dim DrawingViews As SolidEdgeDraft.DrawingViews
        Dim DrawingView As SolidEdgeDraft.DrawingView
        Dim idx As Integer
        Dim TargetSheetName As String

        For Each Sheet In GetSheets(Doc, "Working")
            DrawingViews = Sheet.DrawingViews
            If DrawingViews.Count > 0 Then
                idx = 1
                For Each DrawingView In DrawingViews
                    If DVIdxOriginalSheetDict(Sheet.Name).Keys.Contains(idx) Then
                        TargetSheetName = DVIdxOriginalSheetDict(Sheet.Name)(idx)
                        If Sheet.Name <> TargetSheetName Then
                            DrawingView.Sheet = SheetNameToObject(Doc, "Working", TargetSheetName)
                        End If
                    End If
                    idx += 1
                Next
            End If
        Next

    End Sub

    Private Function GetDVIdx(Doc As SolidEdgeDraft.DraftDocument,
                              DVOriginalSheetDict As Dictionary(Of SolidEdgeDraft.DrawingView, SolidEdgeDraft.Sheet)
                              ) As Dictionary(Of String, Dictionary(Of Integer, String))

        Dim DVIdxOriginalSheetDict As New Dictionary(Of String, Dictionary(Of Integer, String))
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim DrawingViews As SolidEdgeDraft.DrawingViews
        Dim DrawingView As SolidEdgeDraft.DrawingView
        Dim idx As Integer

        For Each Sheet In GetSheets(Doc, "Working")
            DVIdxOriginalSheetDict(Sheet.Name) = New Dictionary(Of Integer, String)
            DrawingViews = Sheet.DrawingViews
            If DrawingViews.Count > 0 Then
                idx = 1
                For Each DrawingView In DrawingViews
                    DVIdxOriginalSheetDict(Sheet.Name)(idx) = DVOriginalSheetDict(DrawingView).Name
                    idx += 1
                Next
            End If
        Next

        Return DVIdxOriginalSheetDict
    End Function

    Private Function AddSheetsToTarget(SourceDoc As SolidEdgeDraft.DraftDocument,
                                  TargetDoc As SolidEdgeDraft.DraftDocument,
                                  DummyName As String
                                  ) As List(Of String)
        ' Add sheets to target to match source.
        ' Remove sheets from target that don't match.
        ' Set backgrounds to match.  Report to log if target does not have the background sheet.

        Dim ErrorMessageList As New List(Of String)

        'Dim SourceSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(SourceDoc, "UserGenerated")
        'Dim TargetSheets As List(Of SolidEdgeDraft.Sheet) = GetSheets(TargetDoc, "UserGenerated")

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

        ' SetTargetBackgrounds(SourceDoc, TargetDoc, DummyName)
        Dim SourceBackgroundSheet As SolidEdgeDraft.Sheet
        Dim TargetBackgroundSheet As SolidEdgeDraft.Sheet

        Dim msg2 As String = ""

        TargetSheets = GetSheets(TargetDoc, "UserGenerated")

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
                        ' ExitStatus = 1
                        msg2 = String.Format("Template does not have a background named '{0}'", SourceBackgroundSheet.Name)
                        If Not ErrorMessageList.Contains(msg2) Then
                            ErrorMessageList.Add(msg2)
                        End If
                    End If
                Catch ex As Exception
                End Try
            End If
        Next

        Return ErrorMessageList

    End Function

    Private Function TransferSheetContentsToTarget(SEApp As SolidEdgeFramework.Application,
                                                   SourceDoc As SolidEdgeDraft.DraftDocument,
                                                   TargetDoc As SolidEdgeDraft.DraftDocument
                                                   ) As Integer

        Dim ExitStatus As Integer = 0

        Dim SheetWindow As SolidEdgeDraft.SheetWindow
        Dim SourceSheet As SolidEdgeDraft.Sheet
        Dim TargetSheet As SolidEdgeDraft.Sheet
        'Dim DrawingObjects As SolidEdgeFrameworkSupport.DrawingObjects

        Dim SelectSet As SolidEdgeFramework.SelectSet
        Dim DrawingViews As SolidEdgeDraft.DrawingViews
        Dim DrawingView As SolidEdgeDraft.DrawingView

        Dim msg2 As String = ""
        Dim HasBrokenView As Boolean

        Dim SelectSetCount As Integer

        For Each SourceSheet In GetSheets(SourceDoc, "UserGenerated")
            If SourceSheet.DrawingObjects.Count > 0 Then
                SourceDoc.Activate()
                SourceSheet.Activate()

                SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
                SheetWindow.FitEx(SolidEdgeDraft.SheetFitConstants.igFitAll)

                ' Deal with Broken Views
                DrawingViews = SourceSheet.DrawingViews
                If DrawingViews.Count > 0 Then
                    HasBrokenView = False

                    For Each DrawingView In DrawingViews
                        If DrawingView.IsBroken Then
                            HasBrokenView = True
                            Exit For
                        End If
                    Next

                    If HasBrokenView Then
                        SelectSet = SheetWindow.SelectSet
                        For Each DrawingView In DrawingViews
                            DrawingView.Select()
                            DrawingView.AddConnectedAnnotationsToSelectSet()
                            DrawingView.AddConnectedDimensionsToSelectSet()
                        Next
                        SEApp.DoIdle()
                        SheetWindow.Cut()
                        SEApp.DoIdle()

                        TargetDoc.Activate()
                        TargetSheet = SheetNameToObject(TargetDoc, "UserGenerated", SourceSheet.Name)
                        TargetSheet.Activate()
                        SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
                        SheetWindow.Paste()
                        SEApp.DoIdle()

                        SelectSetCount = SelectSet.Count
                        SelectSet.RemoveAll()
                        SelectSetCount = SelectSet.Count
                        SEApp.DoIdle()

                    End If

                End If

                SourceDoc.Activate()
                SourceSheet.Activate()

                SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

                SourceSheet.FenceLocate(-1000, 1000, 1000, -1000)

                SelectSet = SheetWindow.SelectSet

                SelectSetCount = SelectSet.Count
                If SelectSet.Count > 0 Then
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

                    End Try

                End If
            End If
        Next

        Return ExitStatus

    End Function

    Private Function GetDVOriginalSheet(Doc As SolidEdgeDraft.DraftDocument
                                        ) As Dictionary(Of SolidEdgeDraft.DrawingView, SolidEdgeDraft.Sheet)

        Dim DVOriginalSheet As New Dictionary(Of SolidEdgeDraft.DrawingView, SolidEdgeDraft.Sheet)
        Dim SectionTypes As New List(Of String)
        Dim SectionType As String

        Dim Sheets As New List(Of SolidEdgeDraft.Sheet)
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim DrawingViews As SolidEdgeDraft.DrawingViews
        Dim DrawingView As SolidEdgeDraft.DrawingView

        SectionTypes.Add("Working")
        SectionTypes.Add("Background")

        For Each SectionType In SectionTypes
            Sheets = GetSheets(Doc, SectionType)
            For Each Sheet In Sheets
                If Sheet.DrawingViews.Count > 0 Then
                    DrawingViews = Sheet.DrawingViews
                    For Each DrawingView In DrawingViews
                        DVOriginalSheet(DrawingView) = Sheet
                    Next
                End If
            Next
        Next

        Return DVOriginalSheet

    End Function

    Private Sub MoveDVToSourceViewSheet(Doc As SolidEdgeDraft.DraftDocument,
                                        DVOriginalSheetsDict As Dictionary(Of SolidEdgeDraft.DrawingView, SolidEdgeDraft.Sheet))

        For Each DV In DVOriginalSheetsDict.Keys
            If Not DV.SourceDrawingView Is DV Then
                DV.Sheet = DVOriginalSheetsDict(DV.SourceDrawingView)  ' Key is the drawing view, value is the sheet
            End If
        Next


    End Sub

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

    Private Function SheetNameToObject(Doc As SolidEdgeDraft.DraftDocument,
                                       SectionType As String,
                                       SheetName As String) As SolidEdgeDraft.Sheet
        'Dim SheetNameToObject As Func(Of SolidEdgeDraft.DraftDocument, String, String, SolidEdgeDraft.Sheet)
        'SheetNameToObject = Function(Doc, SectionType, SheetName)
        '                        Dim Sheet As SolidEdgeDraft.Sheet

        For Each Sheet In GetSheets(Doc, SectionType)
            If Sheet.Name = SheetName Then
                Return Sheet
            End If
        Next
        Return Nothing
    End Function

    Private Sub AddSheet(Doc As SolidEdgeDraft.DraftDocument, SheetName As String)
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim SheetAlreadyExists As Boolean = False

        For Each Sheet In GetSheets(Doc, "Working")
            If SheetName = Sheet.Name Then
                SheetAlreadyExists = True
                Exit For
            End If
        Next

        If Not SheetAlreadyExists Then
            Doc.Sheets.AddSheet(SheetName, SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection)
        End If
    End Sub



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
            ErrorMessageList.Add(String.Format("Error saving {0}", CommonTasks.TruncateFullPath(DXFFilename, Configuration)))
        End Try

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
        NewExtension = Split(NewExtension, Delimiter:="*")(1)  ' "Parasolid (*.xt)" -> ".xt)"
        NewExtension = Split(NewExtension, Delimiter:=")")(0)  ' ".xt)" -> ".xt"

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

                'SupplementalErrorMessage = ParseSubdirectoryFormula(SEDoc, Configuration, Formula)
                '' SubDir = ParseSubdirectoryFormula(SEDoc, Configuration, Formula)
                'SupplementalExitStatus = SupplementalErrorMessage.Keys(0)
                'If SupplementalExitStatus = 0 Then
                '    SubDir = SupplementalErrorMessage(0)(0)

                '    BaseDir = String.Format("{0}\{1}", BaseDir, SubDir)
                '    If Not FileIO.FileSystem.DirectoryExists(BaseDir) Then
                '        Try
                '            FileIO.FileSystem.CreateDirectory(BaseDir)
                '        Catch ex As Exception
                '            Proceed = False
                '            ExitStatus = 1
                '            ErrorMessageList.Add(String.Format("Could not create '{0}'", BaseDir))
                '        End Try
                '    End If
                'Else
                '    ExitStatus = 1
                '    Proceed = False
                '    For Each msg In SupplementalErrorMessage(SupplementalExitStatus)
                '        ErrorMessageList.Add(msg)
                '    Next
                '    ErrorMessageList.Add(String.Format("Could not create subdirectory from formula '{0}'", Formula))
                'End If

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
            SEDoc.SaveAs(NewFilename)
            SEApp.DoIdle()
        Catch ex As Exception
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Error saving file {0}", CommonTasks.TruncateFullPath(NewFilename, Configuration)))
        End Try

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Private Function ParseSubdirectoryFormula(SEDoc As SolidEdgeDraft.DraftDocument,
                                              Configuration As Dictionary(Of String, String),
                                              SubdirectoryFormula As String
                                              ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim SupplementalExitStatus As Integer

        Dim OutString As String = ""

        Dim FCD As New FilenameCharmapDoctor()

        ' Formatting for subdirectory name formula
        ' Example property callout: %{hmk_Part_Number/CP|G}  
        ' Need to know PropertySet, so maybe: %{Custom.hmk_Part_Number}
        ' For Drafts, maybe: %{Custom.hmk_Part_Number|R1}

        ' Example 1 Formula: "Material_%{System.Material}_Thickness_%{Custom.Material Thickness}"
        ' Example 2 Formula: "%{System.Material} %{Custom.Material Thickness}"

        Dim PropertySet As String
        Dim PropertyName As String

        Dim DocValues As New List(Of String)
        Dim DocValue As String

        Dim StartPositions As New List(Of Integer)
        Dim StartPosition As Integer
        Dim EndPositions As New List(Of Integer)
        Dim EndPosition As Integer
        Dim Length As Integer
        Dim i As Integer
        Dim msg As String

        Dim Proceed As Boolean = True

        Dim LastPipeCharPosition As Integer
        Dim ModelLinkIdx As Integer
        Dim ModelLinkSpecifier As String

        Dim Formulas As New List(Of String)
        Dim Formula As String


        If Not SubdirectoryFormula.Contains("%") Then
            OutString = SubdirectoryFormula
            Proceed = False
        End If

        If Proceed Then

            For StartPosition = 0 To SubdirectoryFormula.Length - 1
                If SubdirectoryFormula.Substring(StartPosition, 1) = "%" Then
                    StartPositions.Add(StartPosition)
                End If
            Next

            For EndPosition = 0 To SubdirectoryFormula.Length - 1
                If SubdirectoryFormula.Substring(EndPosition, 1) = "}" Then
                    EndPositions.Add(EndPosition)
                End If
            Next

            For i = 0 To StartPositions.Count - 1
                Length = EndPositions(i) - StartPositions(i) + 1
                Formulas.Add(SubdirectoryFormula.Substring(StartPositions(i), Length))
            Next

            For Each Formula In Formulas
                Formula = Formula.Replace("%{", "")  ' "%{Custom.hmk_Engineer|R1}" -> "Custom.hmk_Engineer|R1}"
                Formula = Formula.Replace("}", "")   ' "Custom.hmk_Engineer|R1}" -> "Custom.hmk_Engineer|R1"
                i = Formula.IndexOf(".")  ' First occurrence
                PropertySet = Formula.Substring(0, i)    ' "Custom"
                PropertyName = Formula.Substring(i + 1)  ' "hmk_Engineer|R1"

                LastPipeCharPosition = 0
                For i = 0 To PropertyName.Length - 1
                    If PropertyName.Substring(i, 1) = "|" Then
                        LastPipeCharPosition = i
                    End If
                Next

                If LastPipeCharPosition = 0 Then
                    ModelLinkIdx = 0
                Else
                    ModelLinkSpecifier = PropertyName.Substring(LastPipeCharPosition)
                    PropertyName = PropertyName.Substring(0, LastPipeCharPosition)

                    Try
                        ModelLinkIdx = CInt(ModelLinkSpecifier.Replace("|R", ""))
                    Catch ex As Exception
                        Proceed = False
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Could not resolve '{0}'", ModelLinkSpecifier))
                        Exit For
                    End Try
                End If

                SupplementalErrorMessage = GetPropertyValue(SEDoc, Configuration, PropertySet, PropertyName, ModelLinkIdx)
                SupplementalExitStatus = SupplementalErrorMessage.Keys(0)
                If SupplementalExitStatus = 0 Then
                    DocValue = SupplementalErrorMessage(SupplementalExitStatus)(0)
                Else
                    DocValue = ""
                    ExitStatus = 1
                    For Each msg In SupplementalErrorMessage(SupplementalExitStatus)
                        If Not ErrorMessageList.Contains(msg) Then
                            ErrorMessageList.Add(msg)
                        End If
                    Next
                End If
                DocValues.Add(DocValue)
            Next

            If Proceed Then
                OutString = SubdirectoryFormula

                For i = 0 To DocValues.Count - 1
                    OutString = OutString.Replace(Formulas(i), DocValues(i))
                Next
            End If
        End If

        If ExitStatus = 0 Then
            OutString = FCD.SubstituteIllegalCharacters(OutString)
            ErrorMessageList.Add(OutString)
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Private Function GetPropertyValue(SEDoc As SolidEdgeDraft.DraftDocument,
                                      Configuration As Dictionary(Of String, String),
                                      PropertySet As String,
                                      PropertyName As String,
                                      ModelLinkIdx As Integer
                                      ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim PropertySets As SolidEdgeFramework.PropertySets = Nothing
        Dim Properties As SolidEdgeFramework.Properties = Nothing
        Dim Prop As SolidEdgeFramework.Property = Nothing

        Dim DocValue As String = ""
        Dim PropertyFound As Boolean = False
        Dim tf As Boolean
        Dim msg As String
        Dim Proceed As Boolean = True

        Dim ModelDocName As String = ""

        If ModelLinkIdx = 0 Then
            Try
                PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)
            Catch ex As Exception
                Proceed = False
                ExitStatus = 1
                ErrorMessageList.Add("Problem accessing PropertySets.")
            End Try
        Else
            Dim ModelLink As SolidEdgeDraft.ModelLink
            Dim Typename As String

            Try
                ModelLink = SEDoc.ModelLinks.Item(ModelLinkIdx)
                Typename = Microsoft.VisualBasic.Information.TypeName(ModelLink.ModelDocument)  ' "PartDocument", "SheetmetalDocument", "AssemblyDocument"

                If Typename.ToLower = "partdocument" Then
                    Dim ModelDoc As SolidEdgePart.PartDocument = CType(ModelLink.ModelDocument, SolidEdgePart.PartDocument)
                    Try
                        PropertySets = CType(ModelDoc.Properties, SolidEdgeFramework.PropertySets)
                    Catch ex As Exception
                        Proceed = False
                        ExitStatus = 1
                        ErrorMessageList.Add("Problem accessing PropertySets.")
                    End Try
                    ModelDocName = ModelDoc.FullName
                End If

                If Typename.ToLower = "sheetmetaldocument" Then
                    Dim ModelDoc As SolidEdgePart.SheetMetalDocument = CType(ModelLink.ModelDocument, SolidEdgePart.SheetMetalDocument)
                    Try
                        PropertySets = CType(ModelDoc.Properties, SolidEdgeFramework.PropertySets)
                    Catch ex As Exception
                        Proceed = False
                        ExitStatus = 1
                        ErrorMessageList.Add("Problem accessing PropertySets.")
                    End Try
                    ModelDocName = ModelDoc.FullName
                End If

                If Typename.ToLower = "assemblydocument" Then
                    Dim ModelDoc As SolidEdgeAssembly.AssemblyDocument = CType(ModelLink.ModelDocument, SolidEdgeAssembly.AssemblyDocument)
                    Try
                        PropertySets = CType(ModelDoc.Properties, SolidEdgeFramework.PropertySets)
                    Catch ex As Exception
                        Proceed = False
                        ExitStatus = 1
                        ErrorMessageList.Add("Problem accessing PropertySets.")
                    End Try
                    ModelDocName = ModelDoc.FullName
                End If
            Catch ex As Exception
                Proceed = False
                ExitStatus = 1
                msg = String.Format("Problem accessing index reference {0}", ModelLinkIdx)
                If Not ErrorMessageList.Contains(msg) Then
                    ErrorMessageList.Add(msg)
                End If
            End Try

        End If

        If Proceed Then
            For Each Properties In PropertySets
                For Each Prop In Properties
                    tf = (PropertySet.ToLower = "custom")
                    tf = tf And (Properties.Name.ToLower = "custom")
                    If tf Then
                        ' Some properties do not have names
                        Try
                            If Prop.Name.ToLower = PropertyName.ToLower Then
                                PropertyFound = True
                                DocValue = Prop.Value.ToString
                                Exit For
                            End If
                        Catch ex As Exception
                        End Try
                    Else
                        ' Some properties do not have names
                        Try
                            If Prop.Name.ToLower = PropertyName.ToLower Then
                                PropertyFound = True
                                DocValue = Prop.Value.ToString
                                Exit For
                            End If
                        Catch ex As Exception
                        End Try
                    End If
                Next
                If PropertyFound Then
                    Exit For
                End If
            Next

            If Not PropertyFound Then
                ExitStatus = 1
                If ModelLinkIdx = 0 Then
                    msg = String.Format("Property '{0}' not found in {1}", PropertyName, CommonTasks.TruncateFullPath(SEDoc.FullName, Configuration))
                    ErrorMessageList.Add(msg)
                Else
                    msg = String.Format("Property '{0}' not found in {1}", PropertyName, CommonTasks.TruncateFullPath(ModelDocName, Configuration))
                    ErrorMessageList.Add(msg)
                End If
            End If
        End If

        If ExitStatus = 0 Then
            ErrorMessageList.Add(DocValue)
        End If

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
        Dim h As Double
        Dim w As Double

        Dim DraftPrinter As SolidEdgeDraft.DraftPrintUtility = CType(SEApp.GetDraftPrintUtility(), SolidEdgeDraft.DraftPrintUtility)

        Dim msg As String = ""

        DraftPrinter.Units = SolidEdgeDraft.DraftPrintUnitsConstants.igDraftPrintInches

        DraftPrinter.Printer = Configuration("TextBoxPrintOptionsPrinter")

        w = CDbl(Configuration("TextBoxPrintOptionsWidth"))
        h = CDbl(Configuration("TextBoxPrintOptionsHeight"))

        ' Weird workaround
        DraftPrinter.PaperHeight = 3.0
        DraftPrinter.PaperHeight = h
        DraftPrinter.PaperHeight = w

        DraftPrinter.Copies = CShort(Configuration("TextBoxPrintOptionsCopies"))

        If Configuration("CheckBoxPrintOptionsAutoOrient").ToLower = "true" Then
            DraftPrinter.AutoOrient = True
        Else
            DraftPrinter.AutoOrient = False
        End If

        If Configuration("CheckBoxPrintOptionsBestFit").ToLower = "true" Then
            DraftPrinter.BestFit = True
        Else
            DraftPrinter.BestFit = False
        End If

        If Configuration("CheckBoxPrintOptionsPrintAsBlack").ToLower = "true" Then
            DraftPrinter.PrintAsBlack = True
        Else
            DraftPrinter.PrintAsBlack = False
        End If

        If Configuration("CheckBoxPrintOptionsScaleLineTypes").ToLower = "true" Then
            DraftPrinter.ScaleLineTypes = True
        Else
            DraftPrinter.ScaleLineTypes = False
        End If

        If Configuration("CheckBoxPrintOptionsScaleLineWidths").ToLower = "true" Then
            DraftPrinter.ScaleLineWidths = True
        Else
            DraftPrinter.ScaleLineWidths = False
        End If

        'msg = String.Format("{0}{1}DraftPrinter.Printer={2}", msg, vbCrLf, DraftPrinter.Printer)
        'msg = String.Format("{0}{1}DraftPrinter.PaperHeight={2}", msg, vbCrLf, DraftPrinter.PaperHeight)
        'msg = String.Format("{0}{1}DraftPrinter.PaperWidth={2}", msg, vbCrLf, DraftPrinter.PaperWidth)
        'msg = String.Format("{0}{1}DraftPrinter.Copies={2}", msg, vbCrLf, DraftPrinter.Copies)
        'msg = String.Format("{0}{1}DraftPrinter.AutoOrient={2}", msg, vbCrLf, DraftPrinter.AutoOrient)
        'msg = String.Format("{0}{1}DraftPrinter.BestFit={2}", msg, vbCrLf, DraftPrinter.BestFit)
        'msg = String.Format("{0}{1}DraftPrinter.PrintAsBlack={2}", msg, vbCrLf, DraftPrinter.PrintAsBlack)
        'msg = String.Format("{0}{1}DraftPrinter.ScaleLineTypes={2}", msg, vbCrLf, DraftPrinter.ScaleLineTypes)
        'msg = String.Format("{0}{1}DraftPrinter.ScaleLineWidths={2}", msg, vbCrLf, DraftPrinter.ScaleLineWidths)

        'MsgBox(msg)

        'Capturing a fault to update ExitStatus
        Try
            DraftPrinter.AddDocument(SEDoc)
            DraftPrinter.PrintOut()
            SEApp.DoIdle()
        Catch ex As Exception
            ExitStatus = 1
            ErrorMessageList.Add("Print drawing did not succeed")
        End Try

        DraftPrinter.RemoveAllDocuments()
        SEApp.DoIdle()

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

    'Public Function InteractiveEdit(
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
    '                               AddressOf InteractiveEditInternal,
    '                               CType(SEDoc, SolidEdgeDraft.DraftDocument),
    '                               Configuration,
    '                               SEApp)

    '    Return ErrorMessage

    'End Function

    'Private Function InteractiveEditInternal(
    '    ByVal SEDoc As SolidEdgeDraft.DraftDocument,
    '    ByVal Configuration As Dictionary(Of String, String),
    '    ByVal SEApp As SolidEdgeFramework.Application
    '    ) As Dictionary(Of Integer, List(Of String))

    '    Dim ErrorMessageList As New List(Of String)
    '    Dim ExitStatus As Integer = 0
    '    Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

    '    Dim Result As MsgBoxResult
    '    Dim msg As String
    '    Dim indent As String = "    "

    '    SEApp.DisplayAlerts = True

    '    msg = String.Format("When finished, do one of the following:{0}", vbCrLf)
    '    msg = String.Format("{0}{1}Click Yes to save and close{2}", msg, indent, vbCrLf)
    '    msg = String.Format("{0}{1}Click No to close without saving{2}", msg, indent, vbCrLf)
    '    msg = String.Format("{0}{1}Click Cancel to quit{2}", msg, indent, vbCrLf)

    '    Result = MsgBox(msg, MsgBoxStyle.YesNoCancel Or MsgBoxStyle.SystemModal, Title:="Solid Edge Housekeeper")

    '    If Result = vbYes Then
    '        If SEDoc.ReadOnly Then
    '            ExitStatus = 1
    '            ErrorMessageList.Add("Cannot save read-only file.")
    '        Else
    '            SEDoc.Save()
    '            SEApp.DoIdle()
    '        End If
    '    ElseIf Result = vbNo Then
    '        'ExitStatus = 1
    '        'ErrorMessageList.Add("File was not saved.")
    '    Else  ' Cancel was chosen
    '        ExitStatus = 99
    '        ErrorMessageList.Add("Operation was cancelled.")
    '    End If

    '    SEApp.DisplayAlerts = False

    '    ErrorMessage(ExitStatus) = ErrorMessageList
    '    Return ErrorMessage
    'End Function


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


    'Public Function RunExternalProgram(
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
    '                               AddressOf RunExternalProgramInternal,
    '                               CType(SEDoc, SolidEdgeDraft.DraftDocument),
    '                               Configuration,
    '                               SEApp)

    '    Return ErrorMessage

    'End Function

    'Private Function RunExternalProgramInternal(
    '    ByVal SEDoc As SolidEdgeDraft.DraftDocument,
    '    ByVal Configuration As Dictionary(Of String, String),
    '    ByVal SEApp As SolidEdgeFramework.Application
    '    ) As Dictionary(Of Integer, List(Of String))

    '    Dim ErrorMessageList As New List(Of String)
    '    Dim SupplementalErrorMessageList As New List(Of String)
    '    Dim ExitStatus As Integer = 0
    '    Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
    '    Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))


    '    Dim ExternalProgram As String = Configuration("TextBoxExternalProgramDraft")

    '    SupplementalErrorMessage = CommonTasks.RunExternalProgram(ExternalProgram)

    '    ExitStatus = SupplementalErrorMessage.Keys(0)

    '    SupplementalErrorMessageList = SupplementalErrorMessage(ExitStatus)

    '    If SupplementalErrorMessageList.Count > 0 Then
    '        For Each s As String In SupplementalErrorMessageList
    '            ErrorMessageList.Add(s)
    '        Next
    '    End If

    '    If Configuration("CheckBoxRunExternalProgramSaveFile").ToLower = "true" Then
    '        If SEDoc.ReadOnly Then
    '            ExitStatus = 1
    '            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
    '        Else
    '            SEDoc.Save()
    '            SEApp.DoIdle()
    '        End If
    '    End If

    '    ErrorMessage(ExitStatus) = ErrorMessageList
    '    Return ErrorMessage
    'End Function


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
