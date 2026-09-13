Option Strict On

Public Class TaskUpdateDrawingStylesFromTemplate

    Inherits Task

    Private _SelectTemplateByProperty As Boolean
    Public Property SelectTemplateByProperty As Boolean
        Get
            Return _SelectTemplateByProperty
        End Get
        Set(value As Boolean)
            _SelectTemplateByProperty = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.SelectTemplateByProperty.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UseConfigurationPageTemplates As Boolean
    Public Property UseConfigurationPageTemplates As Boolean
        Get
            Return _UseConfigurationPageTemplates
        End Get
        Set(value As Boolean)
            _UseConfigurationPageTemplates = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UseConfigurationPageTemplates.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _DraftTemplate As String
    Public Property DraftTemplate As String
        Get
            Return _DraftTemplate
        End Get
        Set(value As String)
            _DraftTemplate = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DraftTemplate.ToString), TextBox).Text = value
            End If
        End Set
    End Property


    Private _DraftTemplateCriteria As List(Of List(Of String))
    Public Property DraftTemplateCriteria As List(Of List(Of String))
        Get
            Return _DraftTemplateCriteria
        End Get
        Set(value As List(Of List(Of String)))
            _DraftTemplateCriteria = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                UpdateDGV()
            End If
        End Set
    End Property


    Private _UpdateBorder As Boolean
    Public Property UpdateBorder As Boolean
        Get
            Return _UpdateBorder
        End Get
        Set(value As Boolean)
            _UpdateBorder = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateBorder.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _AddMissingBorders As Boolean
    Public Property AddMissingBorders As Boolean
        Get
            Return _AddMissingBorders
        End Get
        Set(value As Boolean)
            _AddMissingBorders = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AddMissingBorders.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateLibraryBlocks As Boolean
    Public Property UpdateLibraryBlocks As Boolean
        Get
            Return _UpdateLibraryBlocks
        End Get
        Set(value As Boolean)
            _UpdateLibraryBlocks = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateLibraryBlocks.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _MatchSheetSize As Boolean
    Public Property MatchSheetSize As Boolean
        Get
            Return _MatchSheetSize
        End Get
        Set(value As Boolean)
            _MatchSheetSize = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.MatchSheetSize.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _RenameSheet As Boolean
    Public Property RenameSheet As Boolean
        Get
            Return _RenameSheet
        End Get
        Set(value As Boolean)
            _RenameSheet = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.RenameSheet.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateStyles As Boolean
    Public Property UpdateStyles As Boolean
        Get
            Return _UpdateStyles
        End Get
        Set(value As Boolean)
            _UpdateStyles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateStyles.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _AutoHideOptions As Boolean
    Public Property AutoHideOptions As Boolean
        Get
            Return _AutoHideOptions
        End Get
        Set(value As Boolean)
            _AutoHideOptions = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AutoHideOptions.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private Property ContextMenuStrip1 As ContextMenuStrip
    Private Property DGVRow As Integer



    Enum ControlNames
        SelectTemplateByProperty
        UseConfigurationPageTemplates
        Browse
        DraftTemplate
        DraftTemplateCriteria
        UpdateBorder
        AddMissingBorders
        UpdateLibraryBlocks
        MatchSheetSize
        RenameSheet
        UpdateStyles
        AutoHideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = False
        Me.AppliesToPart = False
        Me.AppliesToSheetmetal = False
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskUpdateDrawingStylesFromTemplate
        Me.Category = "Restyle"
        Me.RequiresDraftTemplate = True
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.DraftTemplate = ""
        Me.UpdateBorder = False
        Me.AddMissingBorders = False
        Me.UpdateLibraryBlocks = False
        Me.UpdateStyles = False

        Me.DraftTemplateCriteria = New List(Of List(Of String))
        'Me.DraftTemplateCriteria.Add({"A", "B", "C"}.ToList)
        'Me.DraftTemplateCriteria.Add({"D", "E", "F"}.ToList)

    End Sub


    Public Overrides Sub Process(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application)

        Me.TaskLogger = Me.FileLogger.AddLogger(Me.Description)

        InvokeSTAThread(
            Of SolidEdgeFramework.SolidEdgeDocument,
            SolidEdgeFramework.Application)(
                AddressOf ProcessInternal,
                SEDoc,
                SEApp)
    End Sub

    Public Overrides Sub Process(ByVal FileName As String)
        Me.TaskLogger = Me.FileLogger.AddLogger(Me.Description)
    End Sub

    Private Sub ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        OleMessageFilter.Register()

        'Me.DraftTemplate = MaybeEvaluateExpression(SEDoc, Me.DraftTemplate)

        Dim tmpDraftTemplate As String = Nothing

        If Not Me.SelectTemplateByProperty Then
            tmpDraftTemplate = MaybeEvaluateExpression(SEDoc, Me.DraftTemplate)
        Else
            tmpDraftTemplate = DoSelectTemplateByProperty(SEDoc)
        End If

        If tmpDraftTemplate Is Nothing Then
            TaskLogger.AddMessage("Template file not found")
            Exit Sub
        End If

        If SEDoc.FullName = tmpDraftTemplate Then
            TaskLogger.AddMessage("Template file itself ineligible for processing")
            Exit Sub
        End If

        Dim SETemplateDoc As SolidEdgeDraft.DraftDocument = Nothing

        Dim tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

        'Open template
        Try
            SETemplateDoc = CType(SEApp.Documents.Open(tmpDraftTemplate), SolidEdgeDraft.DraftDocument)
            SEApp.DoIdle()
        Catch ex As Exception
            TaskLogger.AddMessage($"Could not open template '{tmpDraftTemplate}'.  Exception: {ex.Message}")
        End Try

        SEDoc.Activate()
        SEApp.DoIdle()

        If Me.UpdateBorder And SETemplateDoc IsNot Nothing Then
            DoReplaceBorders(tmpSEDoc, SETemplateDoc, SEApp)
        End If

        If Me.UpdateLibraryBlocks And SETemplateDoc IsNot Nothing Then
            DoUpdateLibraryBlocks(tmpSEDoc, SETemplateDoc, SEApp)
        End If


        If Me.UpdateStyles And SETemplateDoc IsNot Nothing Then

            ' All style collections.
            ' DashStyles, DimensionStyles, DrawingViewStyles, FillStyles, HatchPatternStyles, 
            ' LinearStyles, SmartFrame2dStyles, TableStyles, TextCharStyles, TextStyles

            ' Style collections to receive updates.
            ' DimensionStyles, DrawingViewStyles, LinearStyles, TableStyles, TextCharStyles, TextStyles

            ' Styles not updated at this time.
            ' DashStyles, FillStyles, HatchPatternStyles, SmartFrame2dStyles


            ' Ordered by style dependency

            DoLinearStyles(tmpSEDoc, SETemplateDoc)

            DoTextCharStyles(tmpSEDoc, SETemplateDoc)

            DoTextStyles(tmpSEDoc, SETemplateDoc)

            DoDimensionStyles(tmpSEDoc, SETemplateDoc)

            DoDrawingViewStyles(tmpSEDoc, SETemplateDoc)

            DoTableStyles(tmpSEDoc, SETemplateDoc)


        End If

        If SETemplateDoc IsNot Nothing Then
            SETemplateDoc.Close(False)
            SEApp.DoIdle()

            If SEDoc.ReadOnly Then
                TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If

        End If

    End Sub


    Private Sub DoReplaceBorders(
        tmpSEDoc As SolidEdgeDraft.DraftDocument,
        SETemplateDoc As SolidEdgeDraft.DraftDocument,
        SEApp As SolidEdgeFramework.Application)

        Dim UC As New UtilsCommon

        Dim TemplateSheets As List(Of SolidEdgeDraft.Sheet) = UC.GetSheets(SETemplateDoc, "Background")
        Dim TemplateSheetNames As New List(Of String)
        Dim TemplateSheetSizeOptions As New List(Of SolidEdgeDraft.PaperSizeConstants)

        For Each Sheet As SolidEdgeDraft.Sheet In TemplateSheets
            TemplateSheetNames.Add(Sheet.Name)
            TemplateSheetSizeOptions.Add(Sheet.SheetSetup.SheetSizeOption)
        Next

        Dim tmpDocSheetsInUse As List(Of SolidEdgeDraft.Sheet) = UC.GetSheets(tmpSEDoc, "Working")
        Dim tmpDocSheetsInUseNames As New List(Of String)
        For Each tmpSheet As SolidEdgeDraft.Sheet In tmpDocSheetsInUse
            Try
                Dim tmpName As String = tmpSheet.Background.Name
                If Not tmpDocSheetsInUseNames.Contains(tmpName) Then tmpDocSheetsInUseNames.Add(tmpName)
            Catch ex As Exception

            End Try
        Next

        Dim tmpSEDocSheets As List(Of SolidEdgeDraft.Sheet) = UC.GetSheets(tmpSEDoc, "Background")
        Dim tmpSEDocSheetNames As New List(Of String)
        Dim tmpSEDocSheetSizeOptions As New List(Of SolidEdgeDraft.PaperSizeConstants)

        For Each Sheet As SolidEdgeDraft.Sheet In tmpSEDocSheets
            tmpSEDocSheetNames.Add(Sheet.Name)
            tmpSEDocSheetSizeOptions.Add(Sheet.SheetSetup.SheetSizeOption)
        Next

        For i As Integer = 0 To tmpSEDocSheets.Count - 1
            Dim tmpSEDocSheet As SolidEdgeDraft.Sheet = tmpSEDocSheets(i)
            Dim tmpSEDocSheetName As String = tmpSEDocSheetNames(i)
            Dim tmpSEDocSheetSizeOption As SolidEdgeDraft.PaperSizeConstants = tmpSEDocSheetSizeOptions(i)

            If TemplateSheetNames.Contains(tmpSEDocSheetName) Then
                Try
                    tmpSEDocSheet.ReplaceBackground(SETemplateDoc.FullName, tmpSEDocSheetName)
                Catch ex As Exception
                    TaskLogger.AddMessage($"Exception on replace background on sheet '{tmpSEDocSheetName}'.  Exception was '{ex.Message}'")
                End Try
                SEApp.DoIdle()
            ElseIf Me.MatchSheetSize And TemplateSheetSizeOptions.Contains(tmpSEDocSheetSizeOption) Then
                Dim j As Integer = TemplateSheetSizeOptions.IndexOf(tmpSEDocSheetSizeOption)
                Try
                    tmpSEDocSheet.ReplaceBackground(SETemplateDoc.FullName, TemplateSheetNames(j))
                Catch ex As Exception
                    TaskLogger.AddMessage($"Exception on replace background on sheet '{tmpSEDocSheetName}'.  Exception was '{ex.Message}'")
                End Try
                SEApp.DoIdle()
                If Me.RenameSheet Then
                    tmpSEDocSheet.Name = TemplateSheetNames(j)
                End If
            Else
                If tmpDocSheetsInUseNames.Contains(tmpSEDocSheetName) Then
                    TaskLogger.AddMessage($"Template has no matching background '{tmpSEDocSheetName}'")

                End If
            End If
        Next

        ' Refresh tmpSEDocSheetNames
        tmpSEDocSheetNames.Clear()
        For Each Sheet As SolidEdgeDraft.Sheet In UC.GetSheets(tmpSEDoc, "Background")
            tmpSEDocSheetNames.Add(Sheet.Name)
        Next

        If Me.AddMissingBorders Then
            For Each TemplateSheet As SolidEdgeDraft.Sheet In TemplateSheets
                If Not tmpSEDocSheetNames.Contains(TemplateSheet.Name) Then
                    Dim AddedSheet As SolidEdgeDraft.Sheet = Nothing

                    Try
                        AddedSheet = tmpSEDoc.Sheets.AddSheet(
                            TemplateSheet.Name,
                            SolidEdgeDraft.SheetSectionTypeConstants.igBackgroundSection)
                        SEApp.DoIdle()

                        AddedSheet.ReplaceBackground(SETemplateDoc.FullName, TemplateSheet.Name)
                        SEApp.DoIdle()

                        'TaskLogger.AddMessage($"Added missing drawing border '{TemplateSheet.Name}'")

                    Catch ex As Exception
                        If AddedSheet IsNot Nothing Then
                            Try
                                AddedSheet.Delete()
                                SEApp.DoIdle()
                            Catch
                            End Try
                        End If

                        TaskLogger.AddMessage(
                            $"Error adding missing drawing border '{TemplateSheet.Name}': {ex.Message}")
                    End Try
                End If
            Next
        End If

    End Sub

    Private Sub DoUpdateLibraryBlocks(
        tmpSEDoc As SolidEdgeDraft.DraftDocument,
        SETemplateDoc As SolidEdgeDraft.DraftDocument,
        SEApp As SolidEdgeFramework.Application)

        Dim DocumentBlockNames As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        For Each DocumentBlock As SolidEdgeDraft.Block In tmpSEDoc.Blocks
            DocumentBlockNames.Add(DocumentBlock.Name)
        Next

        For Each TemplateBlock As SolidEdgeDraft.Block In SETemplateDoc.Blocks
            Try
                If DocumentBlockNames.Contains(TemplateBlock.Name) Then
                    ' Replace the existing block definition and preserve its occurrences.
                    tmpSEDoc.Blocks.ReplaceBlock(TemplateBlock)
                Else
                    ' Copy block definitions that are present only in the template.
                    tmpSEDoc.Blocks.CopyBlock(TemplateBlock)
                    DocumentBlockNames.Add(TemplateBlock.Name)
                End If

                SEApp.DoIdle()

            Catch ex As Exception
                TaskLogger.AddMessage(
                    $"Error copying or updating library block '{TemplateBlock.Name}': {ex.Message}")
            End Try
        Next

    End Sub


    Private Function DocStyleNotInTemplate(
       DocStyleNameList As List(Of String),
       TemplateStyleNameList As List(Of String)
       ) As String

        Dim Names As String = ""
        For Each s As String In DocStyleNameList
            If Not TemplateStyleNameList.Contains(s) Then
                Names = $"{Names} {s},"
            End If
        Next

        If Len(Names) > 0 Then
            ' Remove trailing comma.
            Names = Names.Substring(0, Len(Names) - 1)
        End If

        Return Names
    End Function

    Private Sub DoDimensionStyles(
        tmpSEDoc As SolidEdgeDraft.DraftDocument,
        SETemplateDoc As SolidEdgeDraft.DraftDocument)

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim TemplateStyleInDoc As Boolean
        Dim MissingStyles As String
        Dim UC As New UtilsCommon

        ' ############ DimensionStyles ############

        Dim DocStyleNamesInUse As New List(Of String)
        Dim Dims = UC.GetDocDimensions(CType(tmpSEDoc, SolidEdgeFramework.SolidEdgeDocument))
        For Each DimName As String In Dims.Keys
            Dim tmpDim As SolidEdgeFrameworkSupport.Dimension = Dims(DimName)
            If Not DocStyleNamesInUse.Contains(tmpDim.Style.Name) Then
                DocStyleNamesInUse.Add(tmpDim.Style.Name)
            End If
        Next
        'Dim NewWay As Boolean = True
        'If NewWay Then
        '    Dim i = 0
        'End If

        Dim DocDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles
        DocDimensionStyles = CType(tmpSEDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)

        Dim TemplateDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles
        TemplateDimensionStyles = CType(SETemplateDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)

        For Each TemplateDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle In TemplateDimensionStyles
            If Not TemplateStyleNames.Contains(TemplateDimensionStyle.Name) Then
                TemplateStyleNames.Add(TemplateDimensionStyle.Name)
            End If
            TemplateStyleInDoc = False
            For Each DocDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle In DocDimensionStyles
                If Not DocStyleNames.Contains(DocDimensionStyle.Name) Then
                    DocStyleNames.Add(DocDimensionStyle.Name)
                End If
                If TemplateDimensionStyle.Name = DocDimensionStyle.Name Then
                    TemplateStyleInDoc = True
                    Try
                        UC.CopyProperties(TemplateDimensionStyle, DocDimensionStyle, TaskLogger.AddLogger($"Dimension style {TemplateDimensionStyle.Name}"))
                        ' #### The following are not updating correctly in SE2019 with UC.CopyProperties
                        DocDimensionStyle.HoleCalloutCounterdrill = TemplateDimensionStyle.HoleCalloutCounterdrill
                        DocDimensionStyle.HoleCalloutCounterdrillThreaded = TemplateDimensionStyle.HoleCalloutCounterdrillThreaded
                    Catch ex As Exception
                        TaskLogger.AddMessage($"Error applying DimensionStyle '{TemplateDimensionStyle.Name}'.  Exception: {ex.Message}")
                    End Try
                End If
            Next
            If Not TemplateStyleInDoc Then
                Dim tmpDocDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle
                Try
                    tmpDocDimensionStyle = DocDimensionStyles.Add(TemplateDimensionStyle.Name, "")
                    UC.CopyProperties(TemplateDimensionStyle, tmpDocDimensionStyle, TaskLogger.AddLogger($"Dimension style {TemplateDimensionStyle.Name}"))
                    ' #### The following are not updating correctly in SE2019 with UC.CopyProperties
                    tmpDocDimensionStyle.HoleCalloutCounterdrill = TemplateDimensionStyle.HoleCalloutCounterdrill
                    tmpDocDimensionStyle.HoleCalloutCounterdrillThreaded = TemplateDimensionStyle.HoleCalloutCounterdrillThreaded
                Catch ex As Exception
                    TaskLogger.AddMessage($"Error adding DimensionStyle '{TemplateDimensionStyle.Name}'.  Exception: {ex.Message}")
                End Try
            End If
        Next

        MissingStyles = DocStyleNotInTemplate(DocStyleNamesInUse, TemplateStyleNames)
        If Len(MissingStyles) > 0 Then
            TaskLogger.AddMessage($"Dimension styles in Draft but not in Template: {MissingStyles}")
        End If

    End Sub

    Private Sub DoDrawingViewStyles(
        tmpSEDoc As SolidEdgeDraft.DraftDocument,
        SETemplateDoc As SolidEdgeDraft.DraftDocument)

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim TemplateStyleInDoc As Boolean
        Dim UC As New UtilsCommon

        ' ############ DrawingViewStyles ############

        Dim DocDrawingViewStyles As SolidEdgeFrameworkSupport.DrawingViewStyles
        DocDrawingViewStyles = CType(tmpSEDoc.DrawingViewStyles, SolidEdgeFrameworkSupport.DrawingViewStyles)

        Dim TemplateDrawingViewStyles As SolidEdgeFrameworkSupport.DrawingViewStyles
        TemplateDrawingViewStyles = CType(SETemplateDoc.DrawingViewStyles, SolidEdgeFrameworkSupport.DrawingViewStyles)

        For Each TemplateDrawingViewStyle As SolidEdgeFrameworkSupport.DrawingViewStyle In TemplateDrawingViewStyles
            If Not TemplateStyleNames.Contains(TemplateDrawingViewStyle.Name) Then
                TemplateStyleNames.Add(TemplateDrawingViewStyle.Name)
            End If
            TemplateStyleInDoc = False
            For Each DocDrawingViewStyle As SolidEdgeFrameworkSupport.DrawingViewStyle In DocDrawingViewStyles
                If Not DocStyleNames.Contains(DocDrawingViewStyle.Name) Then
                    DocStyleNames.Add(DocDrawingViewStyle.Name)
                End If
                If TemplateDrawingViewStyle.Name = DocDrawingViewStyle.Name Then
                    TemplateStyleInDoc = True
                    Try
                        UC.CopyProperties(TemplateDrawingViewStyle, DocDrawingViewStyle, TaskLogger.AddLogger($"Drawing view style {TemplateDrawingViewStyle.Name}"))
                    Catch ex As Exception
                        TaskLogger.AddMessage($"Error applying DrawingViewStyle '{TemplateDrawingViewStyle.Name}'.  Exception: {ex.Message}")
                    End Try
                End If
            Next
            If Not TemplateStyleInDoc Then
                Dim tmpDocDrawingViewStyle As SolidEdgeFrameworkSupport.DrawingViewStyle
                Try
                    tmpDocDrawingViewStyle = DocDrawingViewStyles.Add(TemplateDrawingViewStyle.Name, "")
                    UC.CopyProperties(TemplateDrawingViewStyle, tmpDocDrawingViewStyle, TaskLogger.AddLogger($"Drawing view style {TemplateDrawingViewStyle.Name}"))
                Catch ex As Exception
                    TaskLogger.AddMessage($"Error adding DrawingViewStyle '{TemplateDrawingViewStyle.Name}'.  Exception: {ex.Message}")
                End Try
            End If
        Next

    End Sub

    Private Sub DoLinearStyles(
        tmpSEDoc As SolidEdgeDraft.DraftDocument,
        SETemplateDoc As SolidEdgeDraft.DraftDocument)

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim TemplateStyleInDoc As Boolean
        Dim MissingStyles As String
        Dim UC As New UtilsCommon

        ' ############ LinearStyles ############

        Dim DocLinearStyles As SolidEdgeFramework.LinearStyles
        DocLinearStyles = CType(tmpSEDoc.LinearStyles, SolidEdgeFramework.LinearStyles)

        Dim TemplateLinearStyles As SolidEdgeFramework.LinearStyles
        TemplateLinearStyles = CType(SETemplateDoc.LinearStyles, SolidEdgeFramework.LinearStyles)

        For Each TemplateLinearStyle As SolidEdgeFramework.LinearStyle In TemplateLinearStyles
            If Not TemplateStyleNames.Contains(TemplateLinearStyle.Name) Then
                TemplateStyleNames.Add(TemplateLinearStyle.Name)
            End If
            TemplateStyleInDoc = False
            For Each DocLinearStyle As SolidEdgeFramework.LinearStyle In DocLinearStyles
                If Not DocStyleNames.Contains(DocLinearStyle.Name) Then
                    DocStyleNames.Add(DocLinearStyle.Name)
                End If
                If TemplateLinearStyle.Name = DocLinearStyle.Name Then
                    TemplateStyleInDoc = True
                    Try
                        UC.CopyProperties(TemplateLinearStyle, DocLinearStyle, TaskLogger.AddLogger($"Linear style {TemplateLinearStyle.Name}"))
                    Catch ex As Exception
                        TaskLogger.AddMessage($"Error applying LinearStyle '{TemplateLinearStyle.Name}'.  Exception: {ex.Message}")
                    End Try
                End If
            Next
            If Not TemplateStyleInDoc Then
                Dim tmpDocLinearStyle As SolidEdgeFramework.LinearStyle
                Try
                    tmpDocLinearStyle = DocLinearStyles.Add(TemplateLinearStyle.Name, "")
                    UC.CopyProperties(TemplateLinearStyle, tmpDocLinearStyle, TaskLogger.AddLogger($"Linear style {TemplateLinearStyle.Name}"))
                Catch ex As Exception
                    TaskLogger.AddMessage($"Error adding LinearStyle '{TemplateLinearStyle.Name}'.  Exception: {ex.Message}")
                End Try
            End If
        Next

        MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
        If Len(MissingStyles) > 0 Then
            TaskLogger.AddMessage($"Linear styles in Draft but not in Template: {MissingStyles}")
        End If

    End Sub

    Private Sub DoTableStyles(
        tmpSEDoc As SolidEdgeDraft.DraftDocument,
        SETemplateDoc As SolidEdgeDraft.DraftDocument)

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim TemplateStyleInDoc As Boolean
        Dim MissingStyles As String
        Dim UC As New UtilsCommon

        ' ############ TableStyles ############

        Dim DocTableStyles As SolidEdgeFrameworkSupport.TableStyles
        DocTableStyles = CType(tmpSEDoc.TableStyles, SolidEdgeFrameworkSupport.TableStyles)



        Dim TemplateTableStyles As SolidEdgeFrameworkSupport.TableStyles
        TemplateTableStyles = CType(SETemplateDoc.TableStyles, SolidEdgeFrameworkSupport.TableStyles)

        For Each TemplateTableStyle As SolidEdgeFrameworkSupport.TableStyle In TemplateTableStyles

            If Not TemplateStyleNames.Contains(TemplateTableStyle.Name) Then
                TemplateStyleNames.Add(TemplateTableStyle.Name)
            End If
            TemplateStyleInDoc = False
            For Each DocTableStyle As SolidEdgeFrameworkSupport.TableStyle In DocTableStyles
                If Not DocStyleNames.Contains(DocTableStyle.Name) Then
                    DocStyleNames.Add(DocTableStyle.Name)
                End If
                If TemplateTableStyle.Name = DocTableStyle.Name Then
                    TemplateStyleInDoc = True
                    Try
                        UC.CopyProperties(TemplateTableStyle, DocTableStyle, TaskLogger.AddLogger($"Table style {TemplateTableStyle.Name}"))
                        '#### added because CopyProperties didn't work in old SE Release, to be verified if still needed
                        For c = 0 To 6
                            DocTableStyle.LineColor(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineColor(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                            DocTableStyle.LineDashType(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineDashType(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                            DocTableStyle.LineWidth(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineWidth(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                        Next
                        ''#### 20251021 CopyProperties not doing these either
                        'DocTableStyle.HeaderTextStyle = TemplateTableStyle.HeaderTextStyle
                        'DocTableStyle.DataTextStyle = TemplateTableStyle.DataTextStyle
                    Catch ex As Exception
                        TaskLogger.AddMessage($"Error applying TableStyle '{TemplateTableStyle.Name}'.  Exception: {ex.Message}")
                    End Try
                End If
            Next
            If Not TemplateStyleInDoc Then
                Dim tmpDocTableStyle As SolidEdgeFrameworkSupport.TableStyle
                Try
                    tmpDocTableStyle = DocTableStyles.Add(TemplateTableStyle.Name, "")
                    'SEDoc.Save()
                    'SEApp.DoIdle()
                    UC.CopyProperties(TemplateTableStyle, tmpDocTableStyle, TaskLogger.AddLogger($"Table style {TemplateTableStyle.Name}"))
                    '#### added because CopyProperties didn't work in old SE Release, to be verified if still needed
                    For c = 0 To 6
                        tmpDocTableStyle.LineColor(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineColor(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                        tmpDocTableStyle.LineDashType(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineDashType(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                        tmpDocTableStyle.LineWidth(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineWidth(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                    Next
                    ''#### 20251021 CopyProperties not doing these either
                    'tmpDocTableStyle.HeaderTextStyle = TemplateTableStyle.HeaderTextStyle
                    'tmpDocTableStyle.DataTextStyle = TemplateTableStyle.DataTextStyle
                Catch ex As Exception
                    TaskLogger.AddMessage($"Error adding TableStyle '{TemplateTableStyle.Name}'.  Exception: {ex.Message}")
                End Try
            End If
        Next

        MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
        If Len(MissingStyles) > 0 Then
            TaskLogger.AddMessage($"Table styles in Draft but not in Template: {MissingStyles}")
        End If
    End Sub

    Private Sub DoTextCharStyles(
        tmpSEDoc As SolidEdgeDraft.DraftDocument,
        SETemplateDoc As SolidEdgeDraft.DraftDocument)

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim TemplateStyleInDoc As Boolean
        Dim MissingStyles As String
        Dim UC As New UtilsCommon

        ' ############ TextCharStyles ############

        Dim DocTextCharStyles As SolidEdgeFramework.TextCharStyles
        DocTextCharStyles = CType(tmpSEDoc.TextCharStyles, SolidEdgeFramework.TextCharStyles)

        Dim TemplateTextCharStyles As SolidEdgeFramework.TextCharStyles
        TemplateTextCharStyles = CType(SETemplateDoc.TextCharStyles, SolidEdgeFramework.TextCharStyles)

        For Each TemplateTextCharStyle As SolidEdgeFramework.TextCharStyle In TemplateTextCharStyles
            If Not TemplateStyleNames.Contains(TemplateTextCharStyle.Name) Then
                TemplateStyleNames.Add(TemplateTextCharStyle.Name)
            End If
            TemplateStyleInDoc = False
            For Each DocTextCharStyle As SolidEdgeFramework.TextCharStyle In DocTextCharStyles
                If Not DocStyleNames.Contains(DocTextCharStyle.Name) Then
                    DocStyleNames.Add(DocTextCharStyle.Name)
                End If
                If TemplateTextCharStyle.Name = DocTextCharStyle.Name Then
                    TemplateStyleInDoc = True
                    Try
                        UC.CopyProperties(TemplateTextCharStyle, DocTextCharStyle, TaskLogger.AddLogger($"Text char style {TemplateTextCharStyle.Name}"))
                    Catch ex As Exception
                        TaskLogger.AddMessage($"Error applying TextCharStyle '{TemplateTextCharStyle.Name}'.  Exception: {ex.Message}")
                    End Try
                End If
            Next
            If Not TemplateStyleInDoc Then
                Dim tmpDocTextCharStyle As SolidEdgeFramework.TextCharStyle
                Try
                    tmpDocTextCharStyle = DocTextCharStyles.Add(TemplateTextCharStyle.Name, "")
                    UC.CopyProperties(TemplateTextCharStyle, tmpDocTextCharStyle, TaskLogger.AddLogger($"Text char style {TemplateTextCharStyle.Name}"))
                Catch ex As Exception
                    TaskLogger.AddMessage($"Error adding TextCharStyle '{TemplateTextCharStyle.Name}'.  Exception: {ex.Message}")
                End Try
            End If
        Next

        MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
        If Len(MissingStyles) > 0 Then
            TaskLogger.AddMessage($"Text Char styles in Draft but not in Template: {MissingStyles}")
        End If
    End Sub

    Private Sub DoTextStyles(
        tmpSEDoc As SolidEdgeDraft.DraftDocument,
        SETemplateDoc As SolidEdgeDraft.DraftDocument)

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim TemplateStyleInDoc As Boolean
        Dim MissingStyles As String
        Dim UC As New UtilsCommon

        ' ############ TextStyles ############

        Dim DocTextStyles As SolidEdgeFramework.TextStyles
        DocTextStyles = CType(tmpSEDoc.TextStyles, SolidEdgeFramework.TextStyles)

        Dim TemplateTextStyles As SolidEdgeFramework.TextStyles
        TemplateTextStyles = CType(SETemplateDoc.TextStyles, SolidEdgeFramework.TextStyles)

        For Each TemplateTextStyle As SolidEdgeFramework.TextStyle In TemplateTextStyles
            If Not TemplateStyleNames.Contains(TemplateTextStyle.Name) Then
                TemplateStyleNames.Add(TemplateTextStyle.Name)
            End If
            TemplateStyleInDoc = False
            For Each DocTextStyle As SolidEdgeFramework.TextStyle In DocTextStyles
                If Not DocStyleNames.Contains(DocTextStyle.Name) Then
                    DocStyleNames.Add(DocTextStyle.Name)
                End If
                If TemplateTextStyle.Name = DocTextStyle.Name Then
                    TemplateStyleInDoc = True
                    Try
                        UC.CopyProperties(TemplateTextStyle, DocTextStyle, TaskLogger.AddLogger($"Text style {TemplateTextStyle.Name}"))
                    Catch ex As Exception
                        TaskLogger.AddMessage($"Error applying TextStyle '{TemplateTextStyle.Name}'.  Exception: {ex.Message}")
                    End Try
                End If
            Next
            If Not TemplateStyleInDoc Then
                Dim tmpDocTextStyle As SolidEdgeFramework.TextStyle
                Try
                    tmpDocTextStyle = DocTextStyles.Add(TemplateTextStyle.Name, "")
                    UC.CopyProperties(TemplateTextStyle, tmpDocTextStyle, TaskLogger.AddLogger($"Text style {TemplateTextStyle.Name}"))
                Catch ex As Exception
                    TaskLogger.AddMessage($"Error adding TextStyle '{TemplateTextStyle.Name}'.  Exception: {ex.Message}")
                End Try
            End If
        Next

        MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
        If Len(MissingStyles) > 0 Then
            TaskLogger.AddMessage($"Text styles in Draft but not in Template: {MissingStyles}")
        End If
    End Sub


    Private Function MaybeEvaluateExpression(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        tmpDraftTemplate As String) As String

        Dim OutString As String = ""

        Dim Success As Boolean = True
        Dim UC As New UtilsCommon
        Dim UFC As New UtilsFilenameCharmap

        If tmpDraftTemplate.StartsWith("EXPRESSION_") Or tmpDraftTemplate.StartsWith("SavedSetting:") Then
            OutString = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, tmpDraftTemplate, Me.PropertiesData, TaskLogger, IsExpression:=True)
            OutString = OutString.Replace(vbCrLf, "")

            If OutString Is Nothing OrElse OutString.ToLower.Contains("<nothing>") Then
                Success = False
                Me.TaskLogger.AddMessage($"Could not parse search directory expression '{tmpDraftTemplate}'")
            Else
                Dim DoNotSubstituteChars As New List(Of String)
                DoNotSubstituteChars.Add("\")
                DoNotSubstituteChars.Add(":")
                OutString = UFC.SubstituteIllegalCharacters(OutString, DoNotSubstituteChars)
            End If

        Else
            OutString = tmpDraftTemplate

        End If

        If Success Then
            Return OutString
        Else
            Return Nothing
        End If


        Return OutString
    End Function

    Private Function DoSelectTemplateByProperty(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument
        ) As String

        Dim tmpDraftTemplate As String = Nothing

        If Me.DraftTemplateCriteria Is Nothing Then
            TaskLogger.AddMessage("Draft template criteria not initialized")
            Return Nothing
        End If

        If Me.DraftTemplateCriteria.Count = 0 Then
            TaskLogger.AddMessage("No draft template criteria to process")
            Return Nothing
        End If

        Dim UC As New UtilsCommon

        For Each L As List(Of String) In Me.DraftTemplateCriteria
            Dim PropertyFormula As String = L(0)
            Dim Value As String = L(1)
            Dim DraftTemplate As String = L(2)

            If Value = "*" Then
                tmpDraftTemplate = DraftTemplate
                Exit For
            End If

            Dim tmpValue As String = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, PropertyFormula, Me.PropertiesData, Me.TaskLogger)

            If tmpValue.ToLower = Value.ToLower Then
                tmpDraftTemplate = DraftTemplate
                Exit For
            End If
        Next

        Return tmpDraftTemplate
    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim TextBox As TextBox
        Dim Button As Button
        Dim DataGridView As DataGridView

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        Me.ContextMenuStrip1 = New ContextMenuStrip
        Me.ContextMenuStrip1.Items.Add(New ToolStripMenuItem("Edit row", Nothing, New EventHandler(AddressOf EditRow)))
        Me.ContextMenuStrip1.Items.Add(New ToolStripMenuItem("Move row up", Nothing, New EventHandler(AddressOf MoveRowUp)))
        Me.ContextMenuStrip1.Items.Add(New ToolStripMenuItem("Move row down", Nothing, New EventHandler(AddressOf MoveRowDown)))
        Me.ContextMenuStrip1.Items.Add(New ToolStripMenuItem("Delete row", Nothing, New EventHandler(AddressOf DeleteRow)))

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.SelectTemplateByProperty.ToString, "Select template by file property")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UseConfigurationPageTemplates.ToString, "Use configuration page templates")
        CheckBox.Padding = New Padding(Me.ControlIndent, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.Browse.ToString, "Dft Template")
        Button.Margin = New Padding(Me.ControlIndent, 0, 0, 0)
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.DraftTemplate.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Dim ColumnHeaders As List(Of String) = {"Property Formula", "Value", "Template"}.ToList
        DataGridView = FormatOptionsDataGridView(ControlNames.DraftTemplateCriteria.ToString, ColumnHeaders, "Textbox", Nothing)
        DataGridView.Margin = New Padding(Me.ControlIndent, 0, 0, 0)
        AddHandler DataGridView.MouseDown, AddressOf DataGridViewOptions_MouseDown
        tmpTLPOptions.Controls.Add(DataGridView, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(DataGridView, 2)
        For i = 0 To ColumnHeaders.Count - 1
            DataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        DataGridView.Height = (DataGridView.Rows(0).Height + 1) * (DataGridView.Rows.Count + 2)
        DataGridView.ReadOnly = True
        'DataGridView.ClearSelection()
        ControlsDict(DataGridView.Name) = DataGridView
        DataGridView.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateBorder.ToString, "Update drawing border")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.MatchSheetSize.ToString, "If no matching name: Match by sheet size")
        CheckBox.Padding = New Padding(Me.ControlIndent, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.RenameSheet.ToString, "If matched by sheet size: Rename sheet")
        CheckBox.Padding = New Padding(Me.ControlIndent, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AddMissingBorders.ToString, "Add missing drawing borders from template")
        CheckBox.Padding = New Padding(Me.ControlIndent, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateLibraryBlocks.ToString, "Copy/update library blocks from template")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateStyles.ToString, "Update styles")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If

            If Not (Me.UpdateBorder Or Me.UpdateLibraryBlocks Or Me.UpdateStyles) Then
                ErrorLogger.AddMessage("Select Update border, Copy/update library blocks, Update styles, or a combination")
            End If

            If Not Me.SelectTemplateByProperty Then
                Dim tf As Boolean = FileIO.FileSystem.FileExists(Me.DraftTemplate)
                tf = tf Or Me.DraftTemplate.Contains("EXPRESSION_")
                tf = tf Or Me.DraftTemplate.Contains("SavedSetting:")
                If Not tf Then
                    ErrorLogger.AddMessage("Select a valid drawing template")
                End If
            Else
                If Me.DraftTemplateCriteria Is Nothing Then
                    ErrorLogger.AddMessage("Draft template criteria not initialized")
                Else
                    If Me.DraftTemplateCriteria.Count = 0 Then
                        ErrorLogger.AddMessage("No draft template criteria to process")
                    Else
                        Dim UC As New UtilsCommon
                        For Each L As List(Of String) In Me.DraftTemplateCriteria
                            Dim PropertyFormula As String = L(0)
                            Dim Value As String = L(1)
                            Dim DraftTemplate As String = L(2)

                            If Not IO.File.Exists(DraftTemplate) Then
                                ErrorLogger.AddMessage($"Draft template not found '{DraftTemplate}'")
                            End If

                            If Not Value = "*" Then
                                If Not UC.CheckValidPropertyFormulas(PropertyFormula) Then
                                    ErrorLogger.AddMessage($"Unable to parse property formula '{PropertyFormula}'")
                                End If
                            End If
                        Next
                    End If
                End If
            End If

        End If

    End Sub


    Private Sub DataGridViewOptions_MouseDown(sender As Object, e As MouseEventArgs)

        Dim DataGridView = CType(sender, DataGridView)

        Me.DGVRow = DataGridView.HitTest(e.X, e.Y).RowIndex

        If e.Button = MouseButtons.Right Then
            If Me.DGVRow >= 0 Then
                Me.ContextMenuStrip1.Show(DataGridView, New Point(e.X, e.Y))
            End If
        End If

    End Sub

    Private Sub EditRow(sender As Object, e As EventArgs)

        If DGVRow < 0 Then Exit Sub

        Dim FEDTC As New FormEditDraftTemplateCriterion

        If Not Me.DraftTemplateCriteria.Count >= DGVRow + 1 Then  ' Need to add a row to Me.DraftTemplateCriteria
            Dim tmpDraftTemplateCriteria As List(Of List(Of String)) = Me.DraftTemplateCriteria
            tmpDraftTemplateCriteria.Add({"", "", ""}.ToList)
            Me.DraftTemplateCriteria = tmpDraftTemplateCriteria
        End If
        FEDTC.DraftTemplateCriterion = Me.DraftTemplateCriteria(Me.DGVRow)

        Dim Result As DialogResult = FEDTC.ShowDialog()

        If Result = DialogResult.OK Then
            Dim tmpDraftTemplateCriteria As List(Of List(Of String)) = Me.DraftTemplateCriteria
            tmpDraftTemplateCriteria(Me.DGVRow) = FEDTC.DraftTemplateCriterion
            Me.DraftTemplateCriteria = tmpDraftTemplateCriteria
        End If

    End Sub

    Private Sub MoveRowUp(sender As Object, e As EventArgs)
        MoveRow("Up")
    End Sub

    Private Sub MoveRowDown(sender As Object, e As EventArgs)
        MoveRow("Down")
    End Sub

    Private Sub MoveRow(Direction As String)
        ' Example
        ' 0 A
        ' 1 B
        ' 2 C <- DGVRow = 2
        ' 3 D
        ' 4 (NewRow)

        If DGVRow < 0 Or DGVRow > Me.DraftTemplateCriteria.Count - 1 Then Exit Sub

        Dim tmpDraftTemplateCriteria As New List(Of List(Of String))

        If Direction = "Up" Then
            If DGVRow = 0 Then Exit Sub
            For i = 0 To Me.DraftTemplateCriteria.Count - 1
                If i = DGVRow - 1 Then
                    tmpDraftTemplateCriteria.Add(Me.DraftTemplateCriteria(i + 1))
                ElseIf i = DGVRow Then
                    tmpDraftTemplateCriteria.Add(Me.DraftTemplateCriteria(i - 1))
                Else
                    tmpDraftTemplateCriteria.Add(Me.DraftTemplateCriteria(i))
                End If
            Next
        Else
            If DGVRow >= Me.DraftTemplateCriteria.Count - 1 Then Exit Sub  ' Accounts for NewRow in DataGridView
            For i = 0 To Me.DraftTemplateCriteria.Count - 1
                If i = DGVRow Then
                    tmpDraftTemplateCriteria.Add(Me.DraftTemplateCriteria(i + 1))
                ElseIf i = DGVRow + 1 Then
                    tmpDraftTemplateCriteria.Add(Me.DraftTemplateCriteria(i - 1))
                Else
                    tmpDraftTemplateCriteria.Add(Me.DraftTemplateCriteria(i))
                End If
            Next

        End If

        Me.DraftTemplateCriteria = tmpDraftTemplateCriteria

    End Sub

    Private Sub DeleteRow(sender As Object, e As EventArgs)

        If DGVRow < 0 Or DGVRow > Me.DraftTemplateCriteria.Count - 1 Then Exit Sub

        Dim tmpDraftTemplateCriteria As List(Of List(Of String)) = Me.DraftTemplateCriteria
        tmpDraftTemplateCriteria.RemoveAt(DGVRow)
        Me.DraftTemplateCriteria = tmpDraftTemplateCriteria

    End Sub

    Private Sub UpdateDGV()

        Dim DGV As DataGridView = CType(ControlsDict(ControlNames.DraftTemplateCriteria.ToString), DataGridView)
        DGV.Rows.Clear()

        For Each L As List(Of String) In Me.DraftTemplateCriteria
            DGV.Rows.Add(L(0), L(1), L(2))
        Next

        DGV.Height = (DGV.Rows(0).Height + 1) * (DGV.Rows.Count + 2)

        DGV.ClearSelection()
    End Sub

    'Public Sub UpdateDGVSize(DGV As DataGridView)
    '    DGV.Height = (DGV.Rows(0).Height + 1) * (DGV.Rows.Count + 2)
    'End Sub




    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox

        Select Case Name

            Case ControlNames.Browse.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a draft template file"
                tmpFileDialog.Filter = "dft files|*.dft"

                If IO.File.Exists(Me.DraftTemplate) Then
                    tmpFileDialog.InitialDirectory = IO.Path.GetDirectoryName(Me.DraftTemplate)
                Else
                    tmpFileDialog.InitialDirectory = Form_Main.SETemplatePath
                End If

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.DraftTemplate = tmpFileDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.DraftTemplate.ToString), TextBox)
                    TextBox.Text = Me.DraftTemplate

                    'Form_Main.SETemplatePath = IO.Path.GetDirectoryName(Me.DraftTemplate)
                End If

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
        End Select

    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.SelectTemplateByProperty.ToString
                Me.SelectTemplateByProperty = Checkbox.Checked

                CType(ControlsDict(ControlNames.UseConfigurationPageTemplates.ToString), CheckBox).Visible = Not Me.SelectTemplateByProperty

                Dim tf As Boolean = Not Me.SelectTemplateByProperty
                tf = tf And Not Me.UseConfigurationPageTemplates

                CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = tf
                CType(ControlsDict(ControlNames.DraftTemplate.ToString), TextBox).Visible = tf

                CType(ControlsDict(ControlNames.DraftTemplateCriteria.ToString), DataGridView).Visible = Me.SelectTemplateByProperty


            Case ControlNames.UseConfigurationPageTemplates.ToString
                Me.UseConfigurationPageTemplates = Checkbox.Checked

                If Me.UseConfigurationPageTemplates Then
                    Me.DraftTemplate = Form_Main.DraftTemplate
                    CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = False
                    CType(ControlsDict(ControlNames.DraftTemplate.ToString), TextBox).Visible = False

                Else
                    CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = True
                    CType(ControlsDict(ControlNames.DraftTemplate.ToString), TextBox).Visible = True

                End If

            Case ControlNames.UpdateBorder.ToString
                Me.UpdateBorder = Checkbox.Checked

                CType(ControlsDict(ControlNames.AddMissingBorders.ToString), CheckBox).Visible = Me.UpdateBorder
                CType(ControlsDict(ControlNames.MatchSheetSize.ToString), CheckBox).Visible = Me.UpdateBorder
                CType(ControlsDict(ControlNames.RenameSheet.ToString), CheckBox).Visible = Me.UpdateBorder

            Case ControlNames.AddMissingBorders.ToString
                Me.AddMissingBorders = Checkbox.Checked

            Case ControlNames.MatchSheetSize.ToString
                Me.MatchSheetSize = Checkbox.Checked

            Case ControlNames.RenameSheet.ToString
                Me.RenameSheet = Checkbox.Checked

            Case ControlNames.UpdateLibraryBlocks.ToString
                Me.UpdateLibraryBlocks = Checkbox.Checked

            Case ControlNames.UpdateStyles.ToString
                Me.UpdateStyles = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
        End Select

    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name

            Case ControlNames.DraftTemplate.ToString
                Me.DraftTemplate = TextBox.Text

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Updates styles, background sheets, and/or library blocks from a template you specify. "

        HelpString += vbCrLf + vbCrLf + "![UpdateDrawingStylesFromTemplate](My%20Project/media/task_update_drawing_styles_from_template.png)"

        HelpString += vbCrLf + vbCrLf + "**Options**"

        HelpString += vbCrLf + "- **Dft Template:** Drawing that contains the desired styles and background sheets. "
        HelpString += "To use the draft template defined on the **Configuration Tab -- Templates Page**, "
        HelpString += "enable the option `Use configuration page templates.`  "

        HelpString += vbCrLf + "Another way to specify a draft template is to use an Expression.  "
        HelpString += "Right-click the textbox and choose insert/edit expression.  "
        HelpString += "See the [<ins>**Edit Properties Help Topic**</ins>](#edit-properties) for details on using them.  "

        HelpString += vbCrLf + "- **Update Drawing Border:** Replace the drawing border in the file with one of the same name in the template. "
        HelpString += vbCrLf + "  - **If no matching name: Match by sheet size:** "
        HelpString += "If no names match, this option checks for sheet height and width. "
        HelpString += "If a match is found, that border is used as the replacement. "
        HelpString += vbCrLf + "  - **If matched by sheet size: Rename sheet:** "
        HelpString += "If a size match is found, this option renames the background to match the template. "
        HelpString += vbCrLf + "  - **Add missing drawing borders from template:** "
        HelpString += "Add background sheets that exist in the template but are missing from the file. "

        HelpString += vbCrLf + vbCrLf
        HelpString += "- **Copy/update library blocks from template:** "
        HelpString += "Copies block definitions that are missing from the file and replaces same-name block definitions with those from the template. "
        HelpString += "Existing block occurrences remain in place. "
        HelpString += "Note, for more fine-grained control, such as replacing differently names blocks, "
        HelpString += "take a look at the `Update Blocks` command.  "

        HelpString += vbCrLf + vbCrLf
        HelpString += "- **Update Styles:** Updates styles from template.  These styles are processed: "
        HelpString += "`DimensionStyles`, `DrawingViewStyles`, `LinearStyles`, `TableStyles`, `TextCharStyles`, `TextStyles`. "
        HelpString += "These are not: `FillStyles`, `HatchPatternStyles`, `SmartFrame2dStyles`. "
        HelpString += "The latter group encountered errors with the current implementation.  The errors were not thoroughly investigated, however. "
        HelpString += "If you need one or more of those styles updated, please ask on the Forum. "

        Return HelpString
    End Function


End Class
