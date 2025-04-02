Option Strict On

Public Class TaskCheckInterference

    Inherits Task

    Private _NumOccurrencesLimit As Integer
    Public Property NumOccurrencesLimit As Integer
        Get
            Return _NumOccurrencesLimit
        End Get
        Set(value As Integer)
            _NumOccurrencesLimit = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.NumOccurrencesLimit.ToString), TextBox).Text = CStr(value)
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
        NumOccurrencesLimit
        NumOccurrencesLimitLabel
        AutoHideOptions
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

        Dim tmpSEDoc = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

        Dim ComparisonMethod = SolidEdgeConstants.InterferenceComparisonConstants.seInterferenceComparisonSet1vsItself
        Dim Status As SolidEdgeAssembly.InterferenceStatusConstants
        Dim Occurrences As SolidEdgeAssembly.Occurrences = tmpSEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence = Nothing
        Dim i As Integer
        Dim NumInterferences As Object = Nothing
        Dim IgnoreT = SolidEdgeConstants.InterferenceOptionsConstants.seIntfOptIgnoreThreadVsNonThreaded
        Dim IgnoreD = SolidEdgeConstants.InterferenceOptionsConstants.seIntfOptIgnoreSameNominalDia
        Dim NumOccurrences As Integer

        Dim SetList As New List(Of Object)

        For i = 1 To Occurrences.Count
            SetList.Add(Occurrences.Item(i))
        Next

        Dim UO As New UtilsOccurrences(tmpSEDoc, IgnoreIncludeInReportsFlag:=True)
        NumOccurrences = UO.AllOccurrences.Count + UO.AllSubOccurrences.Count

        If NumOccurrences > Me.NumOccurrencesLimit Then
            Dim s = String.Format("Number of occurrences {0} exceeds limit {1}.", NumOccurrences, Me.NumOccurrencesLimit)
            TaskLogger.AddMessage(s)
        End If

        If (Not TaskLogger.HasErrors) And (NumOccurrences > 1) Then
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
                TaskLogger.AddMessage("Error running interference check")
            End Try
        End If

        If (Not TaskLogger.HasErrors) And (NumOccurrences > 1) Then
            If Not Status = SolidEdgeAssembly.InterferenceStatusConstants.seInterferenceStatusNoInterference Then
                TaskLogger.AddMessage("Interference detected")
            End If
        End If

    End Sub


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

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
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

            Try
                Me.NumOccurrencesLimit = CInt(ControlsDict(ControlNames.NumOccurrencesLimit.ToString).Text)
                If Not Me.NumOccurrencesLimit > 0 Then
                    ErrorLogger.AddMessage("Enter a number > 0 for number of occurrences limit")
                End If
            Catch ex As Exception
                ErrorLogger.AddMessage("Enter a valid number of occurrences limit")
            End Try
        End If

    End Sub


    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

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

        HelpString += vbCrLf + vbCrLf + "![CheckInterference](My%20Project/media/task_check_interference.png)"

        HelpString += vbCrLf + vbCrLf + "This can take a long time on large assemblies, "
        HelpString += "so there is a limit to the number of parts to check. "
        HelpString += "Set it on the Options panel."

        Return HelpString
    End Function


End Class
