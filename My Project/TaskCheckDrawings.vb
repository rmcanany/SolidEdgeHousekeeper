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
        DetachedDimensionsOrAnnotations
        DrawingViewOnBackgroundSheet
        DrawInView
        DimensionsOverridden
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
        Me.DetachedDimensionsOrAnnotations = False
        Me.DrawingViewOnBackgroundSheet = False
        Me.DrawInView = False
        Me.DimensionsOverridden = False

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

        Dim s As String
        Dim tf As Boolean

        Dim Sheet As SolidEdgeDraft.Sheet = Nothing


        Dim UC As New UtilsCommon

        Dim tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

        If Me.DrawingViewsOutOfDate Then CheckDrawingViewsOutOfDate(tmpSEDoc)

        If DetachedDimensionsOrAnnotations Then CheckDetachedDimensionsOrAnnotations(tmpSEDoc)

        If DrawingViewOnBackgroundSheet Then CheckDrawingViewOnBackgroundSheet(tmpSEDoc)

        If Me.DrawInView Then CheckDrawInView(tmpSEDoc)

        If Me.DimensionsOverridden Then CheckDimensionsOverridden(tmpSEDoc)

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
                    s = String.Format("Drawing views out of date on sheet '{0}'", Sheet.Name)
                    If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                    Exit For
                End If
                ' Some drawing views do not have a ModelLink
                Try
                    If DrawingView.ModelLink IsNot Nothing Then
                        ModelLink = CType(DrawingView.ModelLink, SolidEdgeDraft.ModelLink)
                        If ModelLink.ModelOutOfDate Then
                            s = String.Format("Model out of date on sheet '{0}'", Sheet.Name)
                            If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                            Exit For
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next DrawingView
        Next Sheet
    End Sub

    Private Sub CheckDetachedDimensionsOrAnnotations(tmpSEDoc As SolidEdgeDraft.DraftDocument)

        Dim UC As New UtilsCommon

        Dim s As String
        Dim tf As Boolean

        Dim Balloons As SolidEdgeFrameworkSupport.Balloons
        Dim Balloon As SolidEdgeFrameworkSupport.Balloon

        Dim DocDimensionDict As New Dictionary(Of String, SolidEdgeFrameworkSupport.Dimension)
        Dim DimensionName As String
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension

        Dim ParentSheet As SolidEdgeDraft.Sheet

        ' Check callouts.  Callouts are 'Balloons' in Solid Edge.
        For Each Sheet In UC.GetSheets(tmpSEDoc, "Working")
            Balloons = CType(Sheet.Balloons, SolidEdgeFrameworkSupport.Balloons)
            For Each Balloon In Balloons
                'Doesn't always work
                Try
                    If Balloon.Leader Then
                        If Not Balloon.IsTerminatorAttachedToEntity Then
                            s = String.Format("Detached annotation on sheet '{0}'.  Displayed text is '{1}'", Sheet.Name, Balloon.BalloonDisplayedText)
                            TaskLogger.AddMessage(s)
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next Balloon
        Next Sheet

        ' Check dimensions.
        DocDimensionDict = UC.GetDocDimensions(CType(tmpSEDoc, SolidEdgeFramework.SolidEdgeDocument))
        If DocDimensionDict Is Nothing Then
            TaskLogger.AddMessage("Unable to access dimensions")

        Else
            For Each DimensionName In DocDimensionDict.Keys
                Dimension = DocDimensionDict(DimensionName)

                tf = Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seDimStatusDetached
                tf = tf Or Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seDimStatusError
                tf = tf Or Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seOneEndDetached

                If tf Then
                    ParentSheet = CType(Dimension.Parent, SolidEdgeDraft.Sheet)
                    Dim DimValue As Double
                    Dimension.GetValueEx(DimValue, SolidEdgeFramework.seUnitsTypeConstants.seUnitsType_Document)
                    s = String.Format("Detached dimension on sheet '{0}'.  Displayed value is '{1}'", ParentSheet.Name, DimValue)
                    TaskLogger.AddMessage(s)
                End If

            Next
        End If
    End Sub

    Private Sub CheckDrawingViewOnBackgroundSheet(tmpSEDoc As SolidEdgeDraft.DraftDocument)

        Dim UC As New UtilsCommon
        Dim s As String

        Dim BackgroundSheet As SolidEdgeDraft.Sheet
        For Each BackgroundSheet In UC.GetSheets(tmpSEDoc, "Background")
            If BackgroundSheet.DrawingViews.Count > 0 Then
                s = String.Format("Drawing view on background sheet '{0}'", BackgroundSheet.Name)
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
                        s = String.Format("Draw-In-View graphics on sheet '{0}'", Sheet.Name)
                        If Not TaskLogger.GetMessages.Contains(s) Then TaskLogger.AddMessage(s)
                        Exit For
                    End If
                End If
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
                            s = String.Format("Not-To-Scale dimensions on sheet '{0}'.  Displayed text is '{1}'", Sheet.Name, Dimension.OverrideString)
                            If Not TaskLogger.GetMessages.Contains(s) Then TaskLogger.AddMessage(s)
                        End If

                        If Dimension.DisplayType = SolidEdgeFrameworkSupport.DimDispTypeConstants.igDimDisplayTypeBlank Then
                            s = String.Format("Hidden dimension values on sheet '{0}'.  Displayed text is '", Sheet.Name)
                            Dim TextList As New List(Of String)
                            TextList.AddRange({Dimension.PrefixString, Dimension.SuperfixString, Dimension.SubfixString, Dimension.SubfixString2, Dimension.SuffixString})
                            Dim s1 As String = ""
                            For Each s2 As String In TextList
                                If Not s2 = "" Then
                                    If s1 = "" Then
                                        s = String.Format("{0}{1}", s, s2)
                                    Else
                                        s = String.Format("{0} {1}", s, s2)
                                    End If
                                End If
                            Next
                            s = String.Format("{0}'", s)
                            If Not TaskLogger.GetMessages.Contains(s) Then TaskLogger.AddMessage(s)
                        End If

                    End If

                Next

            End If

        Next Sheet

    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        'Dim IU As New InterfaceUtilities

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

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
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
            tf = tf Or Me.DetachedDimensionsOrAnnotations
            tf = tf Or Me.DrawingViewOnBackgroundSheet
            tf = tf Or Me.DrawInView
            tf = tf Or Me.DimensionsOverridden

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
                    Me.DetachedDimensionsOrAnnotations = True
                    Me.DrawingViewOnBackgroundSheet = True
                    Me.DrawInView = True
                    Me.DimensionsOverridden = True

                    CType(ControlsDict(ControlNames.DrawingViewsOutOfDate.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.DetachedDimensionsOrAnnotations.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.DrawingViewOnBackgroundSheet.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.DrawInView.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.DimensionsOverridden.ToString), CheckBox).Checked = True

                End If

                CType(ControlsDict(ControlNames.DrawingViewsOutOfDate.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.DetachedDimensionsOrAnnotations.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.DrawingViewOnBackgroundSheet.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.DrawInView.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.DimensionsOverridden.ToString), CheckBox).Visible = Not Checkbox.Checked


            Case ControlNames.DrawingViewsOutOfDate.ToString
                Me.DrawingViewsOutOfDate = Checkbox.Checked

            Case ControlNames.DetachedDimensionsOrAnnotations.ToString
                Me.DetachedDimensionsOrAnnotations = Checkbox.Checked

            Case ControlNames.DrawingViewOnBackgroundSheet.ToString
                Me.DrawingViewOnBackgroundSheet = Checkbox.Checked

            Case ControlNames.DrawInView.ToString
                Me.DrawInView = Checkbox.Checked

            Case ControlNames.DimensionsOverridden.ToString
                Me.DimensionsOverridden = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Checks draft files for various problems. "

        HelpString += vbCrLf + vbCrLf + "![CheckDrawings](My%20Project/media/task_check_drawings.png)"

        HelpString += vbCrLf + vbCrLf + "The options are: "
        HelpString += vbCrLf + "- `Drawing views out of date`: Checks if any drawing views are not up to date. "
        HelpString += vbCrLf + "- `Detached dimensions or annotations`: Checks that dimensions, "
        HelpString += "balloons, callouts, etc. are attached to geometry in the drawing. "
        HelpString += vbCrLf + "- `Drawing view on background sheet`: Checks background sheets for the presence of drawing views. "
        HelpString += vbCrLf + "- `Drawing view has Draw In View graphics`: Checks if any drawing view was modified with the Draw In View command. "
        HelpString += vbCrLf + "- `Overridden dimensions`: Checks if any dimensions are not to scale, or have the value hidden. "

        Return HelpString
    End Function


End Class
