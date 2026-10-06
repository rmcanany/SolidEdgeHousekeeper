Option Strict On

Public Class TaskCheckDrawings
    Inherits Task

    Private _CheckAll As Boolean
    Public Property CheckAll As Boolean
        Get
            Return _CheckAll
        End Get
        Set(value As Boolean)
            _CheckAll = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.CheckAll.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _DrawingViewsOutOfDate As Boolean
    Public Property DrawingViewsOutOfDate As Boolean
        Get
            Return _DrawingViewsOutOfDate
        End Get
        Set(value As Boolean)
            _DrawingViewsOutOfDate = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DrawingViewsOutOfDate.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _DrawingTablesOutOfDate As Boolean
    Public Property DrawingTablesOutOfDate As Boolean
        Get
            Return _DrawingTablesOutOfDate
        End Get
        Set(value As Boolean)
            _DrawingTablesOutOfDate = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DrawingTablesOutOfDate.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _DetachedDimensionsOrAnnotations As Boolean
    Public Property DetachedDimensionsOrAnnotations As Boolean
        Get
            Return _DetachedDimensionsOrAnnotations
        End Get
        Set(value As Boolean)
            _DetachedDimensionsOrAnnotations = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DetachedDimensionsOrAnnotations.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _DrawingViewOnBackgroundSheet As Boolean
    Public Property DrawingViewOnBackgroundSheet As Boolean
        Get
            Return _DrawingViewOnBackgroundSheet
        End Get
        Set(value As Boolean)
            _DrawingViewOnBackgroundSheet = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DrawingViewOnBackgroundSheet.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _DrawInView As Boolean
    Public Property DrawInView As Boolean
        Get
            Return _DrawInView
        End Get
        Set(value As Boolean)
            _DrawInView = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DrawInView.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _DimensionsOverridden As Boolean
    Public Property DimensionsOverridden As Boolean
        Get
            Return _DimensionsOverridden
        End Get
        Set(value As Boolean)
            _DimensionsOverridden = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DimensionsOverridden.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _SheetScale As Boolean
    Public Property SheetScale As Boolean
        Get
            Return _SheetScale
        End Get
        Set(value As Boolean)
            _SheetScale = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.SheetScale.ToString), CheckBox).Checked = value
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

    Enum ControlNames
        CheckAll
        DrawingViewsOutOfDate
        DrawingTablesOutOfDate
        DetachedDimensionsOrAnnotations
        DrawingViewOnBackgroundSheet
        DrawInView
        DimensionsOverridden
        SheetScale
        AutoHideOptions
    End Enum


    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = False
        Me.AppliesToAssembly = False
        Me.AppliesToPart = False
        Me.AppliesToSheetmetal = False
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskCheckDrawings
        Me.Category = "Check"
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.DrawingViewsOutOfDate = False
        Me.DrawingTablesOutOfDate = False
        Me.DetachedDimensionsOrAnnotations = False
        Me.DrawingViewOnBackgroundSheet = False
        Me.DrawInView = False
        Me.DimensionsOverridden = False
        Me.SheetScale = False

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

        Dim Sheet As SolidEdgeDraft.Sheet = Nothing


        Dim UC As New UtilsCommon

        Dim tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

        If Me.DrawingViewsOutOfDate Then CheckDrawingViewsOutOfDate(tmpSEDoc)

        If Me.DrawingTablesOutOfDate Then CheckDrawingTablesOutOfDate(tmpSEDoc)

        If DetachedDimensionsOrAnnotations Then CheckDetachedDimensionsOrAnnotations(tmpSEDoc)

        If DrawingViewOnBackgroundSheet Then CheckDrawingViewOnBackgroundSheet(tmpSEDoc)

        If Me.DrawInView Then CheckDrawInView(tmpSEDoc)

        If Me.DimensionsOverridden Then CheckDimensionsOverridden(tmpSEDoc)

        If Me.SheetScale Then CheckSheetScale(tmpSEDoc)

    End Sub


    Private Sub CheckDrawingViewsOutOfDate(tmpSEDoc As SolidEdgeDraft.DraftDocument)

        Dim UC As New UtilsCommon

        Dim s As String

        Dim PartsList As SolidEdgeDraft.PartsList

        Dim DrawingViews As SolidEdgeDraft.DrawingViews = Nothing
        Dim DrawingView As SolidEdgeDraft.DrawingView = Nothing
        Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing

        ' Check Parts lists.
        ' Not all draft files have PartsLists
        Try
            For Each PartsList In tmpSEDoc.PartsLists
                If Not PartsList.IsUpToDate Then
                    s = "Parts list out of date"
                    If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)

                End If
            Next
        Catch ex As Exception
        End Try

        ' Check drawing views.
        For Each Sheet In UC.GetSheets(tmpSEDoc, "Working")

            DrawingViews = Sheet.DrawingViews
            For Each DrawingView In DrawingViews.OfType(Of SolidEdgeDraft.DrawingView)()
                If Not DrawingView.IsUpToDate Then
                    s = $"Drawing views out of date on sheet '{Sheet.Name}'"
                    If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                    Exit For
                End If
                ' Some drawing views do not have a ModelLink
                Try
                    If DrawingView.ModelLink IsNot Nothing Then
                        ModelLink = CType(DrawingView.ModelLink, SolidEdgeDraft.ModelLink)
                        If ModelLink.ModelOutOfDate Then
                            s = $"Model out of date on sheet '{Sheet.Name}'"
                            If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                            Exit For
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next DrawingView
        Next Sheet
    End Sub

    Private Sub CheckDrawingTablesOutOfDate(tmpSEDoc As SolidEdgeDraft.DraftDocument)

        Dim s As String

        ' PartsList, BlockTable, and ConnectorTable expose IsUpToDate.
        ' HoleTable, DraftBendTable, and generic Table expose Update(), but
        ' do not expose an IsUpToDate property in the Solid Edge Draft API.

        Try
            For Each PartsList As SolidEdgeDraft.PartsList In tmpSEDoc.PartsLists
                If Not PartsList.IsUpToDate Then
                    s = "Parts list out of date"
                    If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                End If
            Next
        Catch ex As Exception
            TaskLogger.AddMessage($"Unable to check parts lists: {ex.Message}")
        End Try

        Try
            For Each BlockTable As SolidEdgeDraft.BlockTable In tmpSEDoc.BlockTables
                If Not BlockTable.IsUpToDate Then
                    s = "Block table out of date"
                    If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                End If
            Next
        Catch ex As Exception
            TaskLogger.AddMessage($"Unable to check block tables: {ex.Message}")
        End Try

        Try
            For Each ConnectorTable As SolidEdgeDraft.ConnectorTable In tmpSEDoc.ConnectorTables
                If Not ConnectorTable.IsUpToDate Then
                    s = "Connector table out of date"
                    If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                End If
            Next
        Catch ex As Exception
            TaskLogger.AddMessage($"Unable to check connector tables: {ex.Message}")
        End Try

    End Sub


    Private Sub CheckDetachedDimensionsOrAnnotations(tmpSEDoc As SolidEdgeDraft.DraftDocument)

        Dim UC As New UtilsCommon

        Dim Sheets As List(Of SolidEdgeDraft.Sheet) = UC.GetSheets(tmpSEDoc, "Working")

        ' ###### CALLOUTS ###### (Callouts are 'Balloons' in Solid Edge.)

        For Each Sheet As SolidEdgeDraft.Sheet In Sheets
            Dim Balloons As SolidEdgeFrameworkSupport.Balloons = CType(Sheet.Balloons, SolidEdgeFrameworkSupport.Balloons)
            For Each Balloon As SolidEdgeFrameworkSupport.Balloon In Balloons
                'Doesn't always work
                Try
                    If Balloon.Leader Then
                        If Not Balloon.IsTerminatorAttachedToEntity Then
                            Dim s As String = $"Detached annotation on sheet '{Sheet.Name}'.  Displayed text is '{Balloon.BalloonDisplayedText}'"
                            TaskLogger.AddMessage(s)
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next Balloon
        Next Sheet

        ' ###### DIMENSIONS ######

        Dim DocDimensionDict As Dictionary(Of String, SolidEdgeFrameworkSupport.Dimension) = UC.GetDocDimensions(CType(tmpSEDoc, SolidEdgeFramework.SolidEdgeDocument))
        If DocDimensionDict Is Nothing Then
            TaskLogger.AddMessage("Unable to access dimensions")

        Else
            For Each DimensionName As String In DocDimensionDict.Keys
                Dim Dimension As SolidEdgeFrameworkSupport.Dimension = DocDimensionDict(DimensionName)

                Dim tf As Boolean
                tf = Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seDimStatusDetached
                tf = tf Or Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seDimStatusError
                tf = tf Or Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seOneEndDetached

                If tf Then
                    ' Some dimension parents cannot be cast to Sheet
                    Try
                        Dim ParentSheet As SolidEdgeDraft.Sheet = CType(Dimension.Parent, SolidEdgeDraft.Sheet)
                        Dim DimValue As Double
                        Dimension.GetValueEx(DimValue, SolidEdgeFramework.seUnitsTypeConstants.seUnitsType_Document)
                        Dim s As String = $"Detached dimension on sheet '{ParentSheet.Name}'.  Displayed value is '{DimValue}'"
                        TaskLogger.AddMessage(s)
                    Catch ex As Exception
                    End Try
                End If

            Next
        End If

        ' ###### CENTERMARKS ######

        For Each Sheet As SolidEdgeDraft.Sheet In Sheets
            Dim Centermarks As SolidEdgeFrameworkSupport.CenterMarks = CType(Sheet.CenterMarks, SolidEdgeFrameworkSupport.CenterMarks)
            Dim Count As Integer = 0
            For Each Centermark As SolidEdgeFrameworkSupport.CenterMark In Centermarks
                Dim ConnectObject As Object = Centermark.ConnectObject
                If ConnectObject Is Nothing Then
                    Count += 1
                End If
            Next
            If Count > 0 Then
                Dim s As String = ""
                If Count = 1 Then s = "centermark" Else s = "centermarks"
                TaskLogger.AddMessage($"Found ({Count}) detached {s} on sheet `{Sheet.Name}`")
            End If
        Next

        ' ###### CENTERLINES ######

        For Each Sheet As SolidEdgeDraft.Sheet In Sheets
            Dim Centerlines As SolidEdgeFrameworkSupport.CenterLines = CType(Sheet.CenterLines, SolidEdgeFrameworkSupport.CenterLines)
            Dim Count As Integer = 0
            For Each Centerline As SolidEdgeFrameworkSupport.CenterLine In Centerlines
                Dim ConnectObject1 As Object = Nothing
                Dim ConnectObject2 As Object = Nothing
                Centerline.ConnectObjects(ConnectObject1, ConnectObject2)
                If ConnectObject1 Is Nothing Or ConnectObject2 Is Nothing Then
                    Count += 1
                End If
            Next
            If Count > 0 Then
                Dim s As String = ""
                If Count = 1 Then s = "centerline" Else s = "centerlines"
                TaskLogger.AddMessage($"Found ({Count}) detached {s} on sheet `{Sheet.Name}`")
            End If
        Next

        '' ###### BOLT HOLE CIRCLES ######

        'For Each Sheet As SolidEdgeDraft.Sheet In Sheets
        '    Dim BoltHoleCircles As SolidEdgeFrameworkSupport.BoltHoleCircles = CType(Sheet.BoltHoleCircles, SolidEdgeFrameworkSupport.BoltHoleCircles)
        '    'If BoltHoleCircles.Count > 0 Then TaskLogger.AddMessage($"Bolt hole circle count: {BoltHoleCircles.Count}")
        '    Dim Count As Integer = 0
        '    For Each BoltHoleCircle As SolidEdgeFrameworkSupport.BoltHoleCircle In BoltHoleCircles
        '        Dim ConnectObject1 As Object = Nothing
        '        Dim KeypointIdx1 As Integer = 0
        '        Dim ConnectObject2 As Object = Nothing
        '        Dim KeypointIdx2 As Integer = 0
        '        Dim ConnectObject3 As Object = Nothing
        '        Dim KeypointIdx3 As Integer = 0
        '        Try
        '            BoltHoleCircle.GetConnectElements3Objects(ConnectObject1, KeypointIdx1, ConnectObject2, KeypointIdx2, ConnectObject3, KeypointIdx3)
        '            If BoltHoleCircle.IsDefinedBy2Points Then
        '                'BoltHoleCircle.GetConnectElementsCenterRadius(ConnectObject1, KeypointIdx1, ConnectObject2, KeypointIdx2)
        '                If ConnectObject1 Is Nothing Or ConnectObject2 Is Nothing Then
        '                    Count += 1
        '                End If
        '            ElseIf BoltHoleCircle.IsDefinedBy3Points Then
        '                'BoltHoleCircle.GetConnectElements3Objects(ConnectObject1, KeypointIdx1, ConnectObject2, KeypointIdx2, ConnectObject3, KeypointIdx3)
        '                If ConnectObject1 Is Nothing Or ConnectObject2 Is Nothing Or ConnectObject3 Is Nothing Then
        '                    Count += 1
        '                End If
        '            End If
        '        Catch ex As Exception
        '            'TaskLogger.AddMessage("Exception")
        '        End Try
        '    Next
        '    If Count > 0 Then
        '        Dim s As String = ""
        '        If Count = 1 Then s = "bolt hole circle" Else s = "bolt hole circles"
        '        TaskLogger.AddMessage($"Found ({Count}) detached {s} on sheet `{Sheet.Name}`")
        '    End If
        'Next

        ' ###### WELD SYMBOLS ######

        For Each Sheet As SolidEdgeDraft.Sheet In Sheets
            Dim Count As Integer = 0
            Dim WeldSymbols As SolidEdgeFrameworkSupport.WeldSymbols = CType(Sheet.WeldSymbols, SolidEdgeFrameworkSupport.WeldSymbols)
            For Each WeldSymbol As SolidEdgeFrameworkSupport.WeldSymbol In WeldSymbols
                If WeldSymbol.Leader And Not WeldSymbol.IsTerminatorAttachedToEntity Then
                    Count += 1
                End If
            Next
            If Count > 0 Then
                Dim s As String = ""
                If Count = 1 Then s = "weld symbol" Else s = "weld symbols"
                TaskLogger.AddMessage($"Found ({Count}) detached {s} on sheet `{Sheet.Name}`")
            End If
        Next

        ' ###### DATUM FRAMES ######

        For Each Sheet As SolidEdgeDraft.Sheet In Sheets
            Dim DatumFrames As SolidEdgeFrameworkSupport.DatumFrames = CType(Sheet.DatumFrames, SolidEdgeFrameworkSupport.DatumFrames)
            For Each DatumFrame As SolidEdgeFrameworkSupport.DatumFrame In DatumFrames
                If DatumFrame.Leader And Not DatumFrame.IsTerminatorAttachedToEntity Then
                    TaskLogger.AddMessage($"Detached datum frame on sheet '{Sheet.Name}'.  Displayed text is is '{DatumFrame.Datum}'")
                End If
            Next
        Next

        ' ###### DATUM TARGETS ######

        For Each Sheet As SolidEdgeDraft.Sheet In Sheets
            Dim DatumTargets As SolidEdgeFrameworkSupport.DatumTargets = CType(Sheet.DatumTargets, SolidEdgeFrameworkSupport.DatumTargets)
            For Each DatumTarget As SolidEdgeFrameworkSupport.DatumTarget In DatumTargets
                If DatumTarget.Leader And Not DatumTarget.IsTerminatorAttachedToEntity Then
                    TaskLogger.AddMessage($"Detached datum target on sheet '{Sheet.Name}'.  Displayed text is is '{DatumTarget.DatumReference}{DatumTarget.DatumNumber}'")
                End If
            Next
        Next

        ' ###### SURFACE FINISH SYMBOLS ######

        For Each Sheet As SolidEdgeDraft.Sheet In Sheets
            Dim Count As Integer = 0
            Dim SurfaceFinishSymbols As SolidEdgeFrameworkSupport.SurfaceFinishSymbols = CType(Sheet.SurfaceFinishSymbols, SolidEdgeFrameworkSupport.SurfaceFinishSymbols)
            For Each SurfaceFinishSymbol As SolidEdgeFrameworkSupport.SurfaceFinishSymbol In SurfaceFinishSymbols
                If Not SurfaceFinishSymbol.IsTerminatorAttachedToEntity Then
                    Count += 1
                End If
            Next
            If Count > 0 Then
                Dim s As String = ""
                If Count = 1 Then s = "surface finish symbol" Else s = "surface finish symbols"
                TaskLogger.AddMessage($"Found ({Count}) detached {s} on sheet `{Sheet.Name}`")
            End If
        Next

        ' ###### FEATURE CONTROL FRAMES ######

        For Each Sheet As SolidEdgeDraft.Sheet In Sheets
            Dim FeatureControlFrames As SolidEdgeFrameworkSupport.FeatureControlFrames = CType(Sheet.FeatureControlFrames, SolidEdgeFrameworkSupport.FeatureControlFrames)
            For Each FeatureControlFrame As SolidEdgeFrameworkSupport.FeatureControlFrame In FeatureControlFrames
                If Not FeatureControlFrame.IsTerminatorAttachedToEntity Then
                    TaskLogger.AddMessage($"Detached feature control frame on sheet '{Sheet.Name}'.  Displayed text is is '{FeatureControlFrame.PrimaryFrame}'")
                End If
            Next
        Next

        ' ###### LEADERS ######

        For Each Sheet As SolidEdgeDraft.Sheet In Sheets
            Dim Count As Integer = 0
            Dim Leaders As SolidEdgeFrameworkSupport.Leaders = CType(Sheet.Leaders, SolidEdgeFrameworkSupport.Leaders)
            For Each Leader As SolidEdgeFrameworkSupport.Leader In Leaders
                If Not Leader.IsTerminatorAttachedToEntity Then
                    Count += 1
                End If
            Next
            If Count > 0 Then
                Dim s As String = ""
                If Count = 1 Then s = "leader" Else s = "leaders"
                TaskLogger.AddMessage($"Found ({Count}) detached {s} on sheet `{Sheet.Name}`")
            End If
        Next

    End Sub

    Private Sub CheckDrawingViewOnBackgroundSheet(tmpSEDoc As SolidEdgeDraft.DraftDocument)

        Dim UC As New UtilsCommon
        Dim s As String

        Dim BackgroundSheet As SolidEdgeDraft.Sheet
        For Each BackgroundSheet In UC.GetSheets(tmpSEDoc, "Background")
            If BackgroundSheet.DrawingViews.Count > 0 Then
                s = $"Drawing view on background sheet '{BackgroundSheet.Name}'"
                If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)

            End If
        Next

    End Sub

    Private Sub CheckDrawInView(tmpSEDoc As SolidEdgeDraft.DraftDocument)
        Dim UC As New UtilsCommon

        Dim s As String

        Dim DrawingViews As SolidEdgeDraft.DrawingViews
        Dim DVSheet As SolidEdgeDraft.Sheet

        For Each Sheet In UC.GetSheets(tmpSEDoc, "Working")

            DrawingViews = Sheet.DrawingViews
            For Each DrawingView In DrawingViews.OfType(Of SolidEdgeDraft.DrawingView)()
                ' Failed on a mysterious empty view placed at the origin.  I vaguely recall a previous bug in SE that did that.
                Try
                    If DrawingView.Sheet IsNot Nothing Then
                        Dim Count As Integer = 0
                        DVSheet = CType(DrawingView.Sheet, SolidEdgeDraft.Sheet)

                        If DVSheet.Arcs2d IsNot Nothing Then Count += DVSheet.Arcs2d.Count
                        If DVSheet.BsplineCurves2d IsNot Nothing Then Count += DVSheet.BsplineCurves2d.Count
                        If DVSheet.Circles2d IsNot Nothing Then Count += DVSheet.Circles2d.Count
                        If DVSheet.Conics2d IsNot Nothing Then Count += DVSheet.Conics2d.Count
                        If DVSheet.Curves2d IsNot Nothing Then Count += DVSheet.Curves2d.Count
                        If DVSheet.Ellipses2d IsNot Nothing Then Count += DVSheet.Ellipses2d.Count
                        If DVSheet.EllipticalArcs2d IsNot Nothing Then Count += DVSheet.EllipticalArcs2d.Count
                        If DVSheet.Lines2d IsNot Nothing Then Count += DVSheet.Lines2d.Count
                        If DVSheet.LineStrings2d IsNot Nothing Then Count += DVSheet.LineStrings2d.Count
                        If DVSheet.Points2d IsNot Nothing Then Count += DVSheet.Points2d.Count

                        If Count > 0 Then
                            s = $"Draw-In-View graphics on sheet '{Sheet.Name}'"
                            If Not TaskLogger.GetMessages.Contains(s) Then TaskLogger.AddMessage(s)
                            Exit For
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next DrawingView
        Next Sheet

    End Sub

    Private Sub CheckDimensionsOverridden(tmpSEDoc As SolidEdgeDraft.DraftDocument)

        Dim UC As New UtilsCommon

        Dim s As String
        Dim tf As Boolean

        Dim Dimensions As SolidEdgeFrameworkSupport.Dimensions

        For Each Sheet In UC.GetSheets(tmpSEDoc, "Working")

            Dimensions = CType(Sheet.Dimensions, SolidEdgeFrameworkSupport.Dimensions)

            If Dimensions IsNot Nothing Then

                For Each Dimension As SolidEdgeFrameworkSupport.Dimension In Dimensions

                    ' Detached dimensions populate the override string with last known value.
                    ' Suppressing double-reporting.

                    tf = Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seDimStatusDetached
                    tf = tf Or Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seDimStatusError
                    tf = tf Or Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seOneEndDetached

                    If Not tf Then

                        If Dimension.OverrideString IsNot Nothing AndAlso Not Dimension.OverrideString = "" Then
                            s = $"Not-To-Scale dimensions on sheet '{Sheet.Name}'.  Displayed text is '{Dimension.OverrideString}'"
                            If Not TaskLogger.GetMessages.Contains(s) Then TaskLogger.AddMessage(s)
                        End If

                        If Dimension.DisplayType = SolidEdgeFrameworkSupport.DimDispTypeConstants.igDimDisplayTypeBlank Then
                            s = $"Hidden dimension values on sheet '{Sheet.Name}'.  Displayed text is '"
                            Dim TextList As New List(Of String)
                            TextList.AddRange({Dimension.PrefixString, Dimension.SuperfixString, Dimension.SubfixString, Dimension.SubfixString2, Dimension.SuffixString})
                            Dim s1 As String = ""
                            For Each s2 As String In TextList
                                If Not s2 = "" Then
                                    If s1 = "" Then
                                        s = $"{s}{s2}"
                                    Else
                                        s = $"{s} {s2}"
                                    End If
                                End If
                            Next
                            s = $"{s}'"
                            If Not TaskLogger.GetMessages.Contains(s) Then TaskLogger.AddMessage(s)
                        End If

                    End If

                Next

            End If

        Next Sheet

    End Sub

    Private Sub CheckSheetScale(tmpSEDoc As SolidEdgeDraft.DraftDocument)

        Dim UC As New UtilsCommon

        Dim DrawingViews As SolidEdgeDraft.DrawingViews = Nothing
        Dim DrawingView As SolidEdgeDraft.DrawingView = Nothing
        Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing

        ' Check drawing views.
        For Each Sheet In UC.GetSheets(tmpSEDoc, "Working")

            DrawingViews = Sheet.DrawingViews

            If DrawingViews.Count > 0 Then

                'DrawingView = CType(DrawingViews(0), SolidEdgeDraft.DrawingView)

                'Dim PaperRatioComponent As Double
                'Dim ModelRatioComponent As Double
                'Sheet.SheetSetup.GetDefaultDrawingViewScale(PaperRatioComponent, ModelRatioComponent)

                'Dim SheetScale As Double = 1 / ModelRatioComponent

                'If Math.Abs(SheetScale - DrawingView.ScaleFactor) > 0.0001 Then
                '    s = $"Scale mismatch on {Sheet.Name}: Sheet scale {SheetScale}, Drawing view scale {DrawingView.ScaleFactor}"
                '    TaskLogger.AddMessage(s)
                'End If

                If Sheet.SheetSetup.IsManualSheetScale Then
                    TaskLogger.AddMessage($"Sheet '{Sheet.Name}' scale not linked to a drawing view")
                End If

            End If

        Next Sheet
    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.CheckAll.ToString, "Check all")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DrawingViewsOutOfDate.ToString, "Out of date drawing views")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DrawingTablesOutOfDate.ToString, "Out of date drawing tables")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DetachedDimensionsOrAnnotations.ToString, "Detatched dimensions or annotations")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DrawingViewOnBackgroundSheet.ToString, "Drawing views on background sheet")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DrawInView.ToString, "Drawing views have Draw In View graphics")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DimensionsOverridden.ToString, "Overridden or hidden dimension values")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.SheetScale.ToString, "Sheet scale not linked to a drawing view")
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

        Dim tf As Boolean

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If

            tf = Me.DrawingViewsOutOfDate
            tf = tf Or Me.DrawingTablesOutOfDate
            tf = tf Or Me.DetachedDimensionsOrAnnotations
            tf = tf Or Me.DrawingViewOnBackgroundSheet
            tf = tf Or Me.DrawInView
            tf = tf Or Me.DimensionsOverridden
            tf = tf Or Me.SheetScale

            If Not tf Then
                ErrorLogger.AddMessage("Select at least one type of drawing error to check")
            End If

        End If

    End Sub


    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.CheckAll.ToString
                Me.CheckAll = Checkbox.Checked

                If Me.CheckAll Then
                    Me.DrawingViewsOutOfDate = True
                    Me.DrawingTablesOutOfDate = True
                    Me.DetachedDimensionsOrAnnotations = True
                    Me.DrawingViewOnBackgroundSheet = True
                    Me.DrawInView = True
                    Me.DimensionsOverridden = True
                    Me.SheetScale = True

                    CType(ControlsDict(ControlNames.DrawingViewsOutOfDate.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.DrawingTablesOutOfDate.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.DetachedDimensionsOrAnnotations.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.DrawingViewOnBackgroundSheet.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.DrawInView.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.DimensionsOverridden.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.SheetScale.ToString), CheckBox).Checked = True

                End If

                CType(ControlsDict(ControlNames.DrawingViewsOutOfDate.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.DrawingTablesOutOfDate.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.DetachedDimensionsOrAnnotations.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.DrawingViewOnBackgroundSheet.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.DrawInView.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.DimensionsOverridden.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.SheetScale.ToString), CheckBox).Visible = Not Checkbox.Checked


            Case ControlNames.DrawingViewsOutOfDate.ToString
                Me.DrawingViewsOutOfDate = Checkbox.Checked

            Case ControlNames.DrawingTablesOutOfDate.ToString
                Me.DrawingTablesOutOfDate = Checkbox.Checked

            Case ControlNames.DetachedDimensionsOrAnnotations.ToString
                Me.DetachedDimensionsOrAnnotations = Checkbox.Checked

            Case ControlNames.DrawingViewOnBackgroundSheet.ToString
                Me.DrawingViewOnBackgroundSheet = Checkbox.Checked

            Case ControlNames.DrawInView.ToString
                Me.DrawInView = Checkbox.Checked

            Case ControlNames.DimensionsOverridden.ToString
                Me.DimensionsOverridden = Checkbox.Checked

            Case ControlNames.SheetScale.ToString
                Me.SheetScale = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
        End Select

    End Sub

    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Checks draft files for various problems. "

        HelpString += vbCrLf + vbCrLf + "![CheckDrawings](My%20Project/media/task_check_drawings.png)"

        HelpString += vbCrLf + vbCrLf + "The options are: "
        HelpString += vbCrLf + "- `Drawing views out of date`: Checks if any drawing views, and associated models, are not up to date. "
        HelpString += vbCrLf + "- `Out of date drawing tables`: Checks Parts Lists, Block Tables, and Connector Tables for an out-of-date status. "
        HelpString += "Hole Tables, Bend Tables, and User Tables can be updated by `Update drawing views`, but the Solid Edge API does not expose an equivalent out-of-date status for those table types. "
        HelpString += vbCrLf + "- `Detached dimensions or annotations`: Checks that dimensions, "
        HelpString += "balloons, callouts, etc. are attached to geometry in the drawing. "
        HelpString += vbCrLf + "- `Drawing view on background sheet`: Checks background sheets for the presence of drawing views. "
        HelpString += vbCrLf + "- `Drawing view has Draw In View graphics`: Checks if any drawing view was modified with the Draw In View command. "
        HelpString += vbCrLf + "- `Overridden dimensions`: Checks if any dimensions are not to scale, or have the value hidden. "
        HelpString += vbCrLf + "- `Sheet scale not linked to a drawing view`: Checks what it says. "

        Return HelpString
    End Function


End Class
