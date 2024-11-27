Public Class TaskSetDocumentStatus
    Inherits Task

    Private _NewStatus As String
    Public Property NewStatus As String
        Get
            Return _NewStatus
        End Get
        Set(value As String)
            _NewStatus = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.NewStatus.ToString), ComboBox).Text = value
            End If
        End Set
    End Property

    Private _StructuredStorageEdit As Boolean
    Public Property StructuredStorageEdit As Boolean
        Get
            Return _StructuredStorageEdit
        End Get
        Set(value As Boolean)
            _StructuredStorageEdit = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.StructuredStorageEdit.ToString), CheckBox).Checked = value
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
        NewStatus
        NewStatusLabel
        StructuredStorageEdit
        AutoHideOptions
    End Enum


    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = True
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskSetDocumentStatus
        Me.Category = "Update"
        SetColorFromCategory(Me)
        Me.SolidEdgeRequired = False
        Me.CompatibleWithOtherTasks = False

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.NewStatus = ""
        Me.StructuredStorageEdit = True

    End Sub

    Public Overrides Function Process(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'ErrorMessage = InvokeSTAThread(
        '                       Of SolidEdgeFramework.SolidEdgeDocument,
        '                       Dictionary(Of String, String),
        '                       SolidEdgeFramework.Application,
        '                       Dictionary(Of Integer, List(Of String)))(
        '                           AddressOf ProcessInternal,
        '                           SEDoc,
        '                           Configuration,
        '                           SEApp)

        Return ErrorMessage

    End Function

    Public Overrides Function Process(ByVal FileName As String) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = ProcessInternal(FileName)

        Return ErrorMessage

    End Function

    'Private Overloads Function ProcessInternal(
    '    ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
    '    ByVal Configuration As Dictionary(Of String, String),
    '    ByVal SEApp As SolidEdgeFramework.Application
    '    ) As Dictionary(Of Integer, List(Of String))

    '    Dim ErrorMessageList As New List(Of String)
    '    Dim ExitStatus As Integer = 0
    '    Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))


    '    ErrorMessage(ExitStatus) = ErrorMessageList
    '    Return ErrorMessage
    'End Function

    Private Overloads Function ProcessInternal(ByVal FullName As String) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim NewStatusConstant As SolidEdgeConstants.DocumentStatus
        Dim Success As Boolean
        Dim UC As New UtilsCommon

        Select Case Me.NewStatus
            Case "Available"
                NewStatusConstant = SolidEdgeConstants.DocumentStatus.igStatusAvailable
            Case "Baselined"
                NewStatusConstant = SolidEdgeConstants.DocumentStatus.igStatusBaselined
            Case "InReview"
                NewStatusConstant = SolidEdgeConstants.DocumentStatus.igStatusInReview
            Case "InWork"
                NewStatusConstant = SolidEdgeConstants.DocumentStatus.igStatusInWork
            Case "Obsolete"
                NewStatusConstant = SolidEdgeConstants.DocumentStatus.igStatusObsolete
            Case "Released"
                NewStatusConstant = SolidEdgeConstants.DocumentStatus.igStatusReleased
        End Select

        Success = UC.SetOLEStatus(FullName, NewStatusConstant)

        If Not Success Then
            ExitStatus = 1
            ErrorMessageList.Add("Unable to change document status")
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function



    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim ComboBox As ComboBox
        Dim ComboBoxItems As List(Of String) = Split("Available Baselined InReview InWork Obsolete Released", " ").ToList
        'Dim TextBox As TextBox
        Dim Label As Label
        Dim ControlWidth As Integer = 225

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        ComboBox = FormatOptionsComboBox(ControlNames.NewStatus.ToString, ComboBoxItems, "DropDownList")
        ComboBox.Width = ControlWidth
        AddHandler ComboBox.SelectedIndexChanged, AddressOf ComboBoxOptions_SelectedIndexChanged
        tmpTLPOptions.Controls.Add(ComboBox, 0, RowIndex)
        ControlsDict(ComboBox.Name) = ComboBox

        Label = FormatOptionsLabel(ControlNames.NewStatusLabel.ToString, "New status")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.StructuredStorageEdit.ToString, "Edit properties outside of Solid Edge")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        CheckBox.Checked = True
        CheckBox.Enabled = False
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

            If Me.NewStatus = "" Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select the new status", Indent))
            End If

            If Form_Main.ProcessAsAvailable Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Disable 'Process as available' (Configuration Tab -- Status Page) to use this command", Indent))
            End If

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    Public Sub ComboBoxOptions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Dim ComboBox = CType(sender, ComboBox)
        Dim Name = ComboBox.Name

        Select Case Name
            Case ControlNames.NewStatus.ToString
                Me.NewStatus = ComboBox.Text
            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name
            Case ControlNames.StructuredStorageEdit.ToString
                'Me.StructuredStorageEdit = Checkbox.Checked
                'Me.RequiresSave = Not Checkbox.Checked
                'Me.SolidEdgeRequired = Not Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    'Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
    '    Dim TextBox = CType(sender, TextBox)
    '    Dim Name = TextBox.Name

    '    Dim UC As New UtilsCommon

    '    Select Case Name
    '        Case ControlNames.PropertyFormula.ToString
    '            Me.PropertyFormula = TextBox.Text
    '        Case Else
    '            MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
    '    End Select


    'End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Sets document status. Select the new status on the Options pane. "

        HelpString += vbCrLf + vbCrLf + "![SetDocumentStatus](My%20Project/media/task_set_document_status.png)"

        HelpString += vbCrLf + vbCrLf + "There are a couple of things to know about this command. "

        HelpString += vbCrLf + vbCrLf + "- For performance reasons, the command only runs in "
        HelpString += "Structured Storage mode (i.e. Outside Solid Edge). "
        HelpString += vbCrLf + "- To avoid issues with Read Only conditions, "
        HelpString += "it cannot run with other Tasks enabled. "
        HelpString += vbCrLf + "- To eliminate potential confusion, it cannot run with the "
        HelpString += "`Process as available` option on the **Configuration Tab -- Status Page**. "

        Return HelpString
    End Function


End Class
