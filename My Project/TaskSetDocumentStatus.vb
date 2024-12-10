Option Strict On

Imports System.IO
Imports OpenMcdf

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

    Private _UpdateAdditionalProperties As Boolean
    Public Property UpdateAdditionalProperties As Boolean
        Get
            Return _UpdateAdditionalProperties
        End Get
        Set(value As Boolean)
            _UpdateAdditionalProperties = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateAdditionalProperties.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _AutoAddMissingProperty As Boolean
    Public Property AutoAddMissingProperty As Boolean
        Get
            Return _AutoAddMissingProperty
        End Get
        Set(value As Boolean)
            _AutoAddMissingProperty = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AutoAddMissingProperty.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _AuxProperty1 As String
    Public Property AuxProperty1 As String
        Get
            Return _AuxProperty1
        End Get
        Set(value As String)
            _AuxProperty1 = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AuxProperty1.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _AuxValue1 As String
    Public Property AuxValue1 As String
        Get
            Return _AuxValue1
        End Get
        Set(value As String)
            _AuxValue1 = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AuxValue1.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _AuxProperty2 As String
    Public Property AuxProperty2 As String
        Get
            Return _AuxProperty2
        End Get
        Set(value As String)
            _AuxProperty2 = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AuxProperty2.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _AuxValue2 As String
    Public Property AuxValue2 As String
        Get
            Return _AuxValue2
        End Get
        Set(value As String)
            _AuxValue2 = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AuxValue2.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _AuxProperty3 As String
    Public Property AuxProperty3 As String
        Get
            Return _AuxProperty3
        End Get
        Set(value As String)
            _AuxProperty3 = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AuxProperty3.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _AuxValue3 As String
    Public Property AuxValue3 As String
        Get
            Return _AuxValue3
        End Get
        Set(value As String)
            _AuxValue3 = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AuxValue3.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _AuxProperty4 As String
    Public Property AuxProperty4 As String
        Get
            Return _AuxProperty4
        End Get
        Set(value As String)
            _AuxProperty4 = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AuxProperty4.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _AuxValue4 As String
    Public Property AuxValue4 As String
        Get
            Return _AuxValue4
        End Get
        Set(value As String)
            _AuxValue4 = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AuxValue4.ToString), TextBox).Text = value
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

    Public Property PropertiesData As PropertiesData

    Enum ControlNames
        NewStatus
        NewStatusLabel
        StructuredStorageEdit
        UpdateAdditionalProperties
        AutoAddMissingProperty
        PropertyLabel
        ValueLabel
        AuxProperty1
        AuxValue1
        AuxProperty2
        AuxValue2
        AuxProperty3
        AuxValue3
        AuxProperty4
        AuxValue4
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
        Me.CompatibleWithOtherTasks = False

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.NewStatus = ""
        Me.StructuredStorageEdit = True
        Me.UpdateAdditionalProperties = False
        Me.AutoAddMissingProperty = False
        Me.AuxProperty1 = ""
        Me.AuxValue1 = ""
        Me.AuxProperty2 = ""
        Me.AuxValue2 = ""
        Me.AuxProperty3 = ""
        Me.AuxValue3 = ""
        Me.AuxProperty4 = ""
        Me.AuxValue4 = ""

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
        Dim Proceed As Boolean
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

        Proceed = UC.SetOLEStatus(FullName, NewStatusConstant)

        If Not Proceed Then
            ExitStatus = 1
            ErrorMessageList.Add("Unable to change document status")
        Else
            If Me.UpdateAdditionalProperties Then

                Dim AuxProperties As New List(Of String)
                AuxProperties.AddRange({Me.AuxProperty1, Me.AuxProperty2, Me.AuxProperty3, Me.AuxProperty4})

                Dim s As String

                Dim Substitutions As List(Of String) = GetSubstitutions(FullName)

                If Substitutions Is Nothing Then
                    Proceed = False
                    ExitStatus = 1
                    s = "Problem processing additional property values"
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)

                Else
                    For i As Integer = 0 To AuxProperties.Count - 1
                        If (Proceed) And (Not AuxProperties(i).Trim = "") Then

                            ' ###### UPDATE PROPERTY ######
                            Dim PropertyName As String = UC.PropNameFromFormula(AuxProperties(i))
                            Dim PropData As PropertyData = Me.PropertiesData.GetPropertyData(PropertyName)

                            Dim PropertySetConstant As PropertyData.PropertySetNameConstants = PropData.PropertySetName
                            Dim PropertyNameEnglish As String = PropData.EnglishName
                            Dim PropertySet As String = ""

                            Select Case PropertySetConstant
                                Case PropertyData.PropertySetNameConstants.Custom
                                    PropertySet = "Custom"
                                Case PropertyData.PropertySetNameConstants.System
                                    PropertySet = "System"
                                Case PropertyData.PropertySetNameConstants.Duplicate
                                    PropertySet = "System"
                            End Select


                            If Not UC.SetOLEPropValue(FullName, PropertySet, PropertyNameEnglish, Substitutions(i)) Then
                                'Proceed = False
                                ExitStatus = 1
                                s = String.Format("Unable to update property '{0}' to '{1}'", AuxProperties(i), Substitutions(i))
                                If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                            End If

                        End If
                    Next
                End If
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function

    Private Function GetSubstitutions(FullName As String) As List(Of String)
        Dim Substitutions As New List(Of String)
        Substitutions.AddRange({"", "", "", ""})

        Dim AuxProperties As New List(Of String)
        AuxProperties.AddRange({Me.AuxProperty1, Me.AuxProperty2, Me.AuxProperty3, Me.AuxProperty4})
        Dim AuxValues As New List(Of String)
        AuxValues.AddRange({Me.AuxValue1, Me.AuxValue2, Me.AuxValue3, Me.AuxValue4})

        Dim Proceed As Boolean = True
        Dim UC As New UtilsCommon
        Dim fs As FileStream = Nothing
        Dim cf As CompoundFile = Nothing

        Try
            fs = New FileStream(FullName, FileMode.Open, FileAccess.ReadWrite)
        Catch ex As Exception
            Proceed = False
        End Try

        If Proceed Then
            Dim cfg As CFSConfiguration = CFSConfiguration.SectorRecycle Or CFSConfiguration.EraseFreeSectors
            cf = New CompoundFile(fs, CFSUpdateMode.Update, cfg)

            For i As Integer = 0 To AuxProperties.Count - 1
                If Not AuxProperties(i).Trim = "" Then

                    Try
                        Substitutions(i) = UC.SubstitutePropertyFormula(
                            Nothing, cf, Nothing, FullName, AuxValues(i), ValidFilenameRequired:=False, Me.PropertiesData)
                    Catch ex As Exception
                        Proceed = False
                        Exit For
                    End Try

                End If
            Next
        End If

        If cf IsNot Nothing Then
            cf.Close()
        End If

        If fs IsNot Nothing Then
            fs.Close()
            System.Windows.Forms.Application.DoEvents()
        End If

        If Proceed Then
            Return Substitutions
        Else
            Return Nothing
        End If
    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim ComboBox As ComboBox
        Dim ComboBoxItems As List(Of String) = Split("Available Baselined InReview InWork Obsolete Released", " ").ToList
        Dim TextBox As TextBox
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

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateAdditionalProperties.ToString, "Update additional properties")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        'CheckBox.Checked = True
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoAddMissingProperty.ToString, "Add any property not already in the file")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        'CheckBox.Checked = True
        CheckBox.Visible = False
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Label = FormatOptionsLabel(ControlNames.PropertyLabel.ToString, "Property (blank entries ignored)")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        Label.Visible = False
        ControlsDict(Label.Name) = Label

        Label = FormatOptionsLabel(ControlNames.ValueLabel.ToString, "Value")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        Label.Visible = False
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.AuxProperty1.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        TextBox = FormatOptionsTextBox(ControlNames.AuxValue1.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.AuxProperty2.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        TextBox = FormatOptionsTextBox(ControlNames.AuxValue2.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.AuxProperty3.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        TextBox = FormatOptionsTextBox(ControlNames.AuxValue3.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.AuxProperty4.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        TextBox = FormatOptionsTextBox(ControlNames.AuxValue4.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

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

            Case ControlNames.UpdateAdditionalProperties.ToString
                Me.UpdateAdditionalProperties = Checkbox.Checked

                CType(ControlsDict(ControlNames.AutoAddMissingProperty.ToString), CheckBox).Visible = Me.UpdateAdditionalProperties

                CType(ControlsDict(ControlNames.PropertyLabel.ToString), Label).Visible = Me.UpdateAdditionalProperties
                CType(ControlsDict(ControlNames.ValueLabel.ToString), Label).Visible = Me.UpdateAdditionalProperties

                CType(ControlsDict(ControlNames.AuxProperty1.ToString), TextBox).Visible = Me.UpdateAdditionalProperties
                CType(ControlsDict(ControlNames.AuxValue1.ToString), TextBox).Visible = Me.UpdateAdditionalProperties
                CType(ControlsDict(ControlNames.AuxProperty2.ToString), TextBox).Visible = Me.UpdateAdditionalProperties
                CType(ControlsDict(ControlNames.AuxValue2.ToString), TextBox).Visible = Me.UpdateAdditionalProperties
                CType(ControlsDict(ControlNames.AuxProperty3.ToString), TextBox).Visible = Me.UpdateAdditionalProperties
                CType(ControlsDict(ControlNames.AuxValue3.ToString), TextBox).Visible = Me.UpdateAdditionalProperties
                CType(ControlsDict(ControlNames.AuxProperty4.ToString), TextBox).Visible = Me.UpdateAdditionalProperties
                CType(ControlsDict(ControlNames.AuxValue4.ToString), TextBox).Visible = Me.UpdateAdditionalProperties


            Case ControlNames.AutoAddMissingProperty.ToString
                Me.AutoAddMissingProperty = Checkbox.Checked

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

        Dim UC As New UtilsCommon

        Select Case Name
            Case ControlNames.AuxProperty1.ToString
                Me.AuxProperty1 = TextBox.Text

            Case ControlNames.AuxValue1.ToString
                Me.AuxValue1 = TextBox.Text

            Case ControlNames.AuxProperty2.ToString
                Me.AuxProperty2 = TextBox.Text

            Case ControlNames.AuxValue2.ToString
                Me.AuxValue2 = TextBox.Text

            Case ControlNames.AuxProperty3.ToString
                Me.AuxProperty3 = TextBox.Text

            Case ControlNames.AuxValue3.ToString
                Me.AuxValue3 = TextBox.Text

            Case ControlNames.AuxProperty4.ToString
                Me.AuxProperty4 = TextBox.Text

            Case ControlNames.AuxValue4.ToString
                Me.AuxValue4 = TextBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub


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
