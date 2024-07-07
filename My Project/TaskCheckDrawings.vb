Option Strict On
Imports SolidEdgeDraft
Imports SolidEdgeFrameworkSupport

Public Class TaskCheckDrawings

    Inherits Task

    Public Property DrawingViewsOutOfDate As Boolean
    Public Property DetachedDimensionsOrAnnotations As Boolean
    Public Property DrawingViewOnBackgroundSheet As Boolean

    Enum ControlNames
        DrawingViewsOutOfDate
        DetachedDimensionsOrAnnotations
        DrawingViewOnBackgroundSheet
        HideOptions
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

    End Sub

    Public Overrides Function Process(
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
                                   AddressOf ProcessInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Public Overrides Function Process(ByVal FileName As String) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Return ErrorMessage

    End Function

    Private Function ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim s As String
        Dim tf As Boolean

        Dim Sheet As SolidEdgeDraft.Sheet = Nothing


        Dim TC As New Task_Common

        Dim tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

        If Me.DrawingViewsOutOfDate Then
            Dim PartsList As SolidEdgeDraft.PartsList

            Dim DrawingViews As SolidEdgeDraft.DrawingViews = Nothing
            Dim DrawingView As SolidEdgeDraft.DrawingView = Nothing
            Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing

            ' Check Parts lists.
            ' Not all draft files have PartsLists
            Try
                For Each PartsList In tmpSEDoc.PartsLists
                    If Not PartsList.IsUpToDate Then
                        ExitStatus = 1
                        s = "Parts list out of date"
                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                        'UpdateErrorMessageList(ErrorMessageList, SketchAssembly.Name, True, CheckItem)
                    End If
                Next
            Catch ex As Exception
            End Try

            ' Check drawing views.
            For Each Sheet In TC.GetSheets(tmpSEDoc, "Working")

                DrawingViews = Sheet.DrawingViews
                For Each DrawingView In DrawingViews.OfType(Of SolidEdgeDraft.DrawingView)()
                    If Not DrawingView.IsUpToDate Then
                        ExitStatus = 1
                        s = "Drawing views out of date"
                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                    End If
                    ' Some drawing views do not have a ModelLink
                    Try
                        If DrawingView.ModelLink IsNot Nothing Then
                            ModelLink = CType(DrawingView.ModelLink, SolidEdgeDraft.ModelLink)
                            If ModelLink.ModelOutOfDate Then
                                ExitStatus = 1
                                s = "Drawing views out of date"
                                If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                Next DrawingView
            Next Sheet

        End If

        If DetachedDimensionsOrAnnotations Then
            Dim Balloons As SolidEdgeFrameworkSupport.Balloons
            Dim Balloon As SolidEdgeFrameworkSupport.Balloon

            Dim DocDimensionDict As New Dictionary(Of String, SolidEdgeFrameworkSupport.Dimension)
            Dim DimensionName As String
            Dim Dimension As SolidEdgeFrameworkSupport.Dimension

            Dim ParentSheet As SolidEdgeDraft.Sheet

            ' Check callouts.  Callouts are 'Balloons' in Solid Edge.
            For Each Sheet In TC.GetSheets(tmpSEDoc, "Working")
                Balloons = CType(Sheet.Balloons, SolidEdgeFrameworkSupport.Balloons)
                For Each Balloon In Balloons
                    'Doesn't always work
                    Try
                        If Balloon.Leader Then
                            If Not Balloon.IsTerminatorAttachedToEntity Then
                                ExitStatus = 1
                                s = String.Format("Detached annotation: Sheet {0}: {1}", Sheet.Name, Balloon.BalloonDisplayedText)
                                ErrorMessageList.Add(s)
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                Next Balloon
            Next Sheet

            ' Check dimensions.
            DocDimensionDict = TC.GetDocDimensions(SEDoc)
            If DocDimensionDict Is Nothing Then
                ExitStatus = 1
                ErrorMessageList.Add("Unable to access dimensions")
            Else
                For Each DimensionName In DocDimensionDict.Keys
                    Dimension = DocDimensionDict(DimensionName)

                    tf = Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seDimStatusDetached
                    tf = tf Or Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seDimStatusError
                    tf = tf Or Dimension.StatusOfDimension = SolidEdgeFrameworkSupport.DimStatusConstants.seOneEndDetached

                    If tf Then
                        ExitStatus = 1
                        ParentSheet = CType(Dimension.Parent, SolidEdgeDraft.Sheet)
                        s = String.Format("Detached dimension: Sheet {0}: {1}", ParentSheet.Name, Dimension.DisplayName)
                        ErrorMessageList.Add(s)
                    End If

                Next
            End If


        End If

        If DrawingViewOnBackgroundSheet Then
            Dim BackgroundSheet As SolidEdgeDraft.Sheet
            For Each BackgroundSheet In TC.GetSheets(tmpSEDoc, "Background")
                If BackgroundSheet.DrawingViews.Count > 0 Then
                    ExitStatus = 1
                    s = String.Format("Drawing view found on background '{0}'.", BackgroundSheet.Name)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End If
            Next

        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage


    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        'Dim IU As New InterfaceUtilities

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.DrawingViewsOutOfDate.ToString, "Check for out of date drawing views")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DetachedDimensionsOrAnnotations.ToString, "Check for detatched dimensions or annotations")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DrawingViewOnBackgroundSheet.ToString, "Check for drawing views on background sheet")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.HideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Private Sub InitializeOptionProperties()
        Dim CheckBox As CheckBox

        CheckBox = CType(ControlsDict(ControlNames.DrawingViewsOutOfDate.ToString), CheckBox)
        Me.DrawingViewsOutOfDate = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.DetachedDimensionsOrAnnotations.ToString), CheckBox)
        Me.DetachedDimensionsOrAnnotations = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.DrawingViewOnBackgroundSheet.ToString), CheckBox)
        Me.DrawingViewOnBackgroundSheet = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.HideOptions.ToString), CheckBox)
        Me.AutoHideOptions = CheckBox.Checked

    End Sub

    Public Overrides Function CheckStartConditions(
        PriorErrorMessage As Dictionary(Of Integer, List(Of String))
        ) As Dictionary(Of Integer, List(Of String))

        Dim PriorExitStatus As Integer = PriorErrorMessage.Keys(0)

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim ExitStatus As Integer = 0
        Dim ErrorMessageList = PriorErrorMessage(PriorExitStatus)
        Dim Indent = "    "

        If Me.IsSelectedTask Then
            ' Check start conditions.
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one type of file to process", Indent))
            End If

            If Not (Me.DrawingViewsOutOfDate Or Me.DetachedDimensionsOrAnnotations Or Me.DrawingViewOnBackgroundSheet) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one type of drawing error to check", Indent))
            End If

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name
        'Dim Ctrl As Control
        'Dim Button As Button

        Select Case Name
            Case ControlNames.DrawingViewsOutOfDate.ToString
                Me.DrawingViewsOutOfDate = Checkbox.Checked

            Case ControlNames.DetachedDimensionsOrAnnotations.ToString
                Me.DetachedDimensionsOrAnnotations = Checkbox.Checked

            Case ControlNames.DrawingViewOnBackgroundSheet.ToString
                Me.DrawingViewOnBackgroundSheet = Checkbox.Checked

            Case ControlNames.HideOptions.ToString
                HandleHideOptionsChange(Me, Me.TaskOptionsTLP, Checkbox)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Checks draft files for various problems.  The options are: "
        HelpString += vbCrLf + "`Drawing views out of date`: Checks if any drawing views are not up to date. "
        HelpString += vbCrLf + "`Detached dimensions or annotations`: Checks that dimensions, "
        HelpString += "balloons, callouts, etc. are attached to geometry in the drawing. "
        HelpString += vbCrLf + "`Drawing view on background sheet`: Checks background sheets for the presence of drawing views. "

        Return HelpString
    End Function


End Class
