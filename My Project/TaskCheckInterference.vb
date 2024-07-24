Option Strict On

'Imports SolidEdgeConstants

Public Class TaskCheckInterference

    Inherits Task

    Public Property NumOccurrencesLimit As Integer

    Enum ControlNames
        NumOccurrencesLimit
        NumOccurrencesLimitLabel
        HideOptions
    End Enum


    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = False
        Me.AppliesToAssembly = True
        Me.AppliesToPart = False
        Me.AppliesToSheetmetal = False
        Me.AppliesToDraft = False
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskCheckInterference
        Me.Category = "Check"
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.NumOccurrencesLimit = 1000

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

        Dim tmpSEDoc = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

        Dim ComparisonMethod = SolidEdgeConstants.InterferenceComparisonConstants.seInterferenceComparisonSet1vsItself
        Dim Status As SolidEdgeAssembly.InterferenceStatusConstants
        Dim Occurrences As SolidEdgeAssembly.Occurrences = Nothing
        Dim Occurrence As SolidEdgeAssembly.Occurrence = Nothing
        Dim i As Integer
        Dim NumInterferences As Object = Nothing
        Occurrences = tmpSEDoc.Occurrences
        Dim IgnoreT As SolidEdgeConstants.InterferenceOptionsConstants = SolidEdgeConstants.InterferenceOptionsConstants.seIntfOptIgnoreThreadVsNonThreaded
        Dim IgnoreD As SolidEdgeConstants.InterferenceOptionsConstants = SolidEdgeConstants.InterferenceOptionsConstants.seIntfOptIgnoreSameNominalDia
        Dim NumOccurrences As Integer
        'Dim NumOccurrencesLimit As Integer

        Dim SetList As New List(Of Object)

        For i = 1 To Occurrences.Count
            SetList.Add(Occurrences.Item(i))
        Next
        Dim OG As New OccurrenceGetter(tmpSEDoc, False)

        NumOccurrences = OG.AllOccurrences.Count + OG.AllSubOccurrences.Count

        'NumOccurrencesLimit = CInt(Configuration("TextBoxCheckInterferenceMaxOccurrences"))

        If NumOccurrences > Me.NumOccurrencesLimit Then
            ExitStatus = 1
            ErrorMessageList.Add(String.Format(
                "Not performing interference check.  Number of occurrences {0} exceeds limit {1}.",
                NumOccurrences,
                Me.NumOccurrencesLimit))
        End If

        If (ExitStatus = 0) And (NumOccurrences > 1) Then
            Try
                tmpSEDoc.CheckInterference2(
                NumElementsSet1:=SetList.Count,
                Set1:=SetList.ToArray,
                Status:=Status,
                ComparisonMethod:=ComparisonMethod,
                AddInterferenceAsOccurrence:=False,
                NumInterferences:=NumInterferences,
                IgnoreSameNominalDiaConstant:=IgnoreD,
                IgnoreNonThreadVsThreadConstant:=IgnoreT)
            Catch ex As Exception
                ExitStatus = 1
                ErrorMessageList.Add("Error running interference check")
            End Try
        End If

        If (ExitStatus = 0) And (NumOccurrences > 1) Then
            If Not Status = SolidEdgeAssembly.InterferenceStatusConstants.seInterferenceStatusNoInterference Then
                ExitStatus = 1
                ErrorMessageList.Add("Interference detected")
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function



    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        '        Dim Button As Button
        Dim TextBox As TextBox
        Dim Label As Label

        'Dim IU As New InterfaceUtilities

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        TextBox = FormatOptionsTextBox(ControlNames.NumOccurrencesLimit.ToString, "")
        TextBox.Width = 40
        TextBox.TextAlign = HorizontalAlignment.Right
        TextBox.Text = "1000"
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        Label = FormatOptionsLabel(ControlNames.NumOccurrencesLimitLabel.ToString, "Max number occurrences to process")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

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
        Dim TextBox As TextBox

        TextBox = CType(ControlsDict(ControlNames.NumOccurrencesLimit.ToString), TextBox)
        Me.NumOccurrencesLimit = CInt(TextBox.Text)

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

            Try
                Me.NumOccurrencesLimit = CInt(ControlsDict(ControlNames.NumOccurrencesLimit.ToString).Text)
                If Not Me.NumOccurrencesLimit > 0 Then
                    If Not ErrorMessageList.Contains(Me.Description) Then
                        ErrorMessageList.Add(Me.Description)
                    End If
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("{0}Enter a number > 0 for number of occurrences limit", Indent))
                End If
            Catch ex As Exception
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Enter a valid number of occurrences limit", Indent))
            End Try

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

        Select Case Name

            Case ControlNames.HideOptions.ToString
                HandleHideOptionsChange(Me, Me.TaskOptionsTLP, Checkbox)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name
        Dim i As Integer

        Select Case Name
            Case ControlNames.NumOccurrencesLimit.ToString
                Try
                    i = CInt(TextBox.Text)
                    Me.NumOccurrencesLimit = i
                Catch ex As Exception
                End Try

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Runs an interference check.  All parts are checked against all others. "
        HelpString += "This can take a long time on large assemblies, "
        HelpString += "so there is a limit to the number of parts to check. "
        HelpString += "Set it on the Options panel."

        Return HelpString
    End Function


End Class
