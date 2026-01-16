Option Strict On

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
        Me.RequiresPropertiesData = True
        Me.SolidEdgeRequired = False

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.NewStatus = ""
        Me.StructuredStorageEdit = True

    End Sub


    Public Overrides Sub Process(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application)

        Me.TaskLogger = Me.FileLogger.AddLogger(Me.Description)
    End Sub

    Public Overrides Sub Process(ByVal FileName As String)
        Me.TaskLogger = Me.FileLogger.AddLogger(Me.Description)
        ProcessInternal(FileName)
    End Sub

    Private Overloads Sub ProcessInternal(ByVal FullName As String)

        Dim Proceed As Boolean = True

        Dim SSDoc As HCStructuredStorageDoc = Nothing

        Try
            SSDoc = New HCStructuredStorageDoc(FullName)
            SSDoc.ReadProperties(Me.PropertiesData)
            'SSDoc.ReadVariableNames()
        Catch ex As Exception
            Proceed = False
            TaskLogger.AddMessage($"Unable to open file: {ex.Message}")

        End Try

        If Proceed Then

            Proceed = SSDoc.SetStatus(Me.NewStatus)

            If Not Proceed Then
                TaskLogger.AddMessage(String.Format("Unable to change status to '{0}'", Me.NewStatus))

            End If

        End If

        If Proceed Then
            If SSDoc IsNot Nothing Then SSDoc.Save()
        End If

        If SSDoc IsNot Nothing Then SSDoc.Close()

    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim ComboBox As ComboBox
        Dim ComboBoxItems As New List(Of String)
        ComboBoxItems.AddRange({"", "Available", "Baselined", "InReview", "InWork", "Obsolete", "Released"})
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

        CheckBox = FormatOptionsCheckBox(ControlNames.StructuredStorageEdit.ToString, "Run task without Solid Edge")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        CheckBox.Checked = True
        CheckBox.Enabled = False
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

                If Me.NewStatus = "" Then
                    ErrorLogger.AddMessage("Select the new status")
                End If

                If Form_Main.ProcessAsAvailable Then
                    ErrorLogger.AddMessage("Disable 'Process as available' (Configuration Tab -- Status Page) to use this command")
                End If

            End If
        End If

    End Sub


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
                ' Not enabled.  Always checked.

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
        HelpString = "Sets document status. Select the new status on the Options pane. "

        HelpString += vbCrLf + vbCrLf + "![SetDocumentStatus](My%20Project/media/task_set_document_status.png)"

        HelpString += vbCrLf + vbCrLf + "Because certain status settings make the file read-only, the command only runs in "
        HelpString += "Structured Storage mode (i.e. without Solid Edge). "
        HelpString += vbCrLf + vbCrLf + "To eliminate potential confusion, it cannot run with the "
        HelpString += "`Process as available` option on the **Configuration Tab -- Status Page**. "

        Return HelpString
    End Function


End Class
