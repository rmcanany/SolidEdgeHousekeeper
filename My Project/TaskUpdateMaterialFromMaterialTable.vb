Option Strict On

Public Class TaskUpdateMaterialFromMaterialTable

    Inherits Task

    Private _MaterialTable As String
    Public Property MaterialTable As String
        Get
            Return _MaterialTable
        End Get
        Set(value As String)
            _MaterialTable = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox).Text = value
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

    'Private _StructuredStorageEdit As Boolean
    'Public Property StructuredStorageEdit As Boolean
    '    Get
    '        Return _StructuredStorageEdit
    '    End Get
    '    Set(value As Boolean)
    '        _StructuredStorageEdit = value
    '        If Me.TaskOptionsTLP IsNot Nothing Then
    '            CType(ControlsDict(ControlNames.StructuredStorageEdit.ToString), CheckBox).Checked = value
    '            Me.SolidEdgeRequired = Not value
    '        End If
    '    End Set
    'End Property

    Private _RemoveFaceStyleOverrides As Boolean
    Public Property RemoveFaceStyleOverrides As Boolean
        Get
            Return _RemoveFaceStyleOverrides
        End Get
        Set(value As Boolean)
            _RemoveFaceStyleOverrides = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.RemoveFaceStyleOverrides.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateFaceStyles As Boolean
    Public Property UpdateFaceStyles As Boolean
        Get
            Return _UpdateFaceStyles
        End Get
        Set(value As Boolean)
            _UpdateFaceStyles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateFaceStyles.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UseFinishFaceStyle As Boolean
    Public Property UseFinishFaceStyle As Boolean
        Get
            Return _UseFinishFaceStyle
        End Get
        Set(value As Boolean)
            _UseFinishFaceStyle = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UseFinishFaceStyle.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _FinishPropertyFormula As String
    Public Property FinishPropertyFormula As String
        Get
            Return _FinishPropertyFormula
        End Get
        Set(value As String)
            _FinishPropertyFormula = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.FinishPropertyFormula.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _ExcludedFinishesList As List(Of String)
    Public Property ExcludedFinishesList As List(Of String)
        Get
            Return _ExcludedFinishesList
        End Get
        Set(value As List(Of String))
            _ExcludedFinishesList = value
            _ExcludedFinishesList.Sort()

            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim tmpTextBox As TextBox = CType(ControlsDict(ControlNames.ExcludedFinishes.ToString), TextBox)
                Dim s1 As String = ""
                For Each s2 As String In _ExcludedFinishesList
                    If Not s2.Trim = "" Then
                        s1 = String.Format("{0}{1}{2}", s1, s2, vbCrLf)
                    End If
                Next
                If Not s1 = "" Then s1 = s1.Substring(0, s1.Length - 1) ' Remove trailing vbcrlf

                If Not tmpTextBox.Text = s1 Then tmpTextBox.Text = s1

            End If
        End Set
    End Property

    Private _OverrideBodyFaceStyle As Boolean
    Public Property OverrideBodyFaceStyle As Boolean
        Get
            Return _OverrideBodyFaceStyle
        End Get
        Set(value As Boolean)
            _OverrideBodyFaceStyle = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.OverrideBodyFaceStyle.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _OverrideMaterialFaceStyle As Boolean
    Public Property OverrideMaterialFaceStyle As Boolean
        Get
            Return _OverrideMaterialFaceStyle
        End Get
        Set(value As Boolean)
            _OverrideMaterialFaceStyle = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.OverrideMaterialFaceStyle.ToString), CheckBox).Checked = value
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
        UseConfigurationPageTemplates
        Browse
        MaterialTable
        'StructuredStorageEdit
        RemoveFaceStyleOverrides
        UpdateFaceStyles
        UseFinishFaceStyle
        FinishPropertyFormulaLabel
        FinishPropertyFormula
        ExcludedFinishesLabel
        ExcludedFinishes
        'DeleteExcludedFinish
        OverrideBodyFaceStyle
        OverrideMaterialFaceStyle
        AutoHideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = False
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = False
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskUpdateMaterialFromMaterialTable
        Me.Category = "Update"
        Me.RequiresMaterialTable = True
        Me.RequiresPropertiesData = True
        SetColorFromCategory(Me)
        'Me.SolidEdgeRequired = False
        Me.SolidEdgeRequired = True  ' Default is so checking the box toggles a property update

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.MaterialTable = ""
        Me.RemoveFaceStyleOverrides = False
        Me.UpdateFaceStyles = False
        Me.UseFinishFaceStyle = False
        Me.FinishPropertyFormula = ""
        Me.ExcludedFinishesList = New List(Of String)
        Me.OverrideBodyFaceStyle = False
        Me.OverrideMaterialFaceStyle = False
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

        ErrorMessage = ProcessInternal(FileName)

        Return ErrorMessage

    End Function

    Private Overloads Function ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ExitStatus As Integer = 0
        Dim ErrorMessageList As New List(Of String)
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim PropertySetName As String
        Dim PropertyName As String
        Dim FinishName As String = Nothing

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case "par", "psm"
                If Me.UseFinishFaceStyle Then
                    PropertySetName = UC.PropSetFromFormula(Me.FinishPropertyFormula)
                    PropertyName = UC.PropNameFromFormula(Me.FinishPropertyFormula)
                    FinishName = CStr(UC.GetPropValue(
                        SEDoc, PropertySetName, PropertyName, ModelLinkIdx:=0, AddProp:=False))

                    If FinishName Is Nothing Then
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Property '{0}' not found", Me.FinishPropertyFormula))
                    End If
                End If

                If ExitStatus = 0 Then
                    Dim UM As New UtilsMaterials

                    SupplementalErrorMessage = UM.UpdateMaterialFromMaterialTable(
                        SEApp, SEDoc, Me.MaterialTable, Me.RemoveFaceStyleOverrides, Me.UpdateFaceStyles,
                        Me.UseFinishFaceStyle, FinishName, Me.ExcludedFinishesList,
                        Me.OverrideBodyFaceStyle, Me.OverrideMaterialFaceStyle)

                    AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)
                End If

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        End Select

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

    Private Overloads Function ProcessInternal(ByVal FullName As String) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'Dim Proceed As Boolean = True
        'Dim Matl As String = ""

        'Dim SSDoc As HCStructuredStorageDoc = Nothing
        'Dim SSMatTable As HCStructuredStorageDoc = Nothing

        'Try
        '    SSDoc = New HCStructuredStorageDoc(FullName)
        '    SSMatTable = New HCStructuredStorageDoc(Me.MaterialTable)
        'Catch ex As Exception
        '    If SSDoc IsNot Nothing Then SSDoc.Close()
        '    If SSMatTable IsNot Nothing Then SSMatTable.Close()
        '    Proceed = False
        '    ExitStatus = 1
        '    ErrorMessageList.Add(ex.Message)
        'End Try

        'If Proceed Then
        '    SSDoc.ReadProperties(Me.PropertiesData)
        '    SSMatTable.ReadMaterialTable()

        '    Matl = CType(SSDoc.GetPropValue("System", "Material"), String)

        '    If (Matl Is Nothing) Or (SSDoc.IsFileEmpty) Then
        '        Proceed = False  ' Empty files or ones without a Material property are not errors.
        '    End If
        'End If

        'If Proceed Then
        '    If Matl.Trim = "" Then
        '        Proceed = False
        '        ExitStatus = 1
        '        ErrorMessageList.Add("Material 'None' not in material table")
        '    End If
        'End If

        'If Proceed Then
        '    If Not SSMatTable.MaterialInTable(Matl) Then
        '        Proceed = False
        '        ExitStatus = 1
        '        ErrorMessageList.Add(String.Format("Material '{0}' not in material table", Matl))
        '    End If
        'End If

        'If Proceed Then
        '    If Not SSMatTable.UpdateMaterial(SSDoc) Then
        '        Proceed = False
        '        ExitStatus = 1
        '        ErrorMessageList.Add("Unable to update material")
        '    End If
        'End If

        'If Proceed Then
        '    SSDoc.Save()
        'End If

        'If SSDoc IsNot Nothing Then SSDoc.Close()
        'If SSMatTable IsNot Nothing Then SSMatTable.Close()

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Button As Button
        Dim TextBox As TextBox
        'Dim Combobox As ComboBox
        Dim Label As Label

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.UseConfigurationPageTemplates.ToString, "Use configuration page material table")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.Browse.ToString, "Matl Table")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.MaterialTable.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        'RowIndex += 1

        'CheckBox = FormatOptionsCheckBox(ControlNames.StructuredStorageEdit.ToString, "Run task without Solid Edge")
        'AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        'tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        'tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        'ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.RemoveFaceStyleOverrides.ToString, "Remove face style overrides")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        'CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateFaceStyles.ToString, "Update face styles")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        'CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UseFinishFaceStyle.ToString, "Finish property determines face style")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        Label = FormatOptionsLabel(ControlNames.FinishPropertyFormulaLabel.ToString, "Finish property")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(Label, 2)
        ControlsDict(Label.Name) = Label
        Label.Visible = False

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.FinishPropertyFormula.ToString, "")
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(TextBox, 2)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        RowIndex += 1

        Label = FormatOptionsLabel(ControlNames.ExcludedFinishesLabel.ToString, "Finishes that do not change material appearance")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(Label, 2)
        ControlsDict(Label.Name) = Label
        Label.Visible = False

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.ExcludedFinishes.ToString, "")
        AddHandler TextBox.LostFocus, AddressOf TextBoxOptions_Text_Changed
        TextBox.Multiline = True
        TextBox.ScrollBars = ScrollBars.Vertical
        TextBox.Width = 200
        TextBox.Height = 60
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(TextBox, 2)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.OverrideBodyFaceStyle.ToString, "Override the Body face style")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.OverrideMaterialFaceStyle.ToString, "Override the Material face style")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False
        CheckBox.Enabled = False

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

            If Not FileIO.FileSystem.FileExists(Me.MaterialTable) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select a valid material table", Indent))
            End If

            If Me.UpdateFaceStyles And Me.UseFinishFaceStyle Then
                Dim UC As New UtilsCommon

                If Not UC.CheckValidPropertyFormulas(Me.FinishPropertyFormula) Then
                    If Not ErrorMessageList.Contains(Me.Description) Then
                        ErrorMessageList.Add(Me.Description)
                    End If
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("{0}Could not parse property formula '{1}'", Indent, Me.FinishPropertyFormula))
                End If

                If Not ((Me.OverrideBodyFaceStyle) Or (Me.OverrideMaterialFaceStyle)) Then
                    If Not ErrorMessageList.Contains(Me.Description) Then
                        ErrorMessageList.Add(Me.Description)
                    End If
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("{0}Select an override method", Indent))
                End If
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
        Dim tf As Boolean

        Select Case Name

            Case ControlNames.UseConfigurationPageTemplates.ToString
                Me.UseConfigurationPageTemplates = Checkbox.Checked

                If Me.UseConfigurationPageTemplates Then
                    Me.MaterialTable = Form_Main.MaterialTable
                    CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = False
                    CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox).Visible = False

                Else
                    CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = True
                    CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox).Visible = True

                End If

            'Case ControlNames.StructuredStorageEdit.ToString
            '    Me.StructuredStorageEdit = Checkbox.Checked
            '    Me.SolidEdgeRequired = Not Checkbox.Checked

            '    CType(ControlsDict(ControlNames.UpdateFaceStyles.ToString), CheckBox).Visible = Not Me.StructuredStorageEdit
            '    CType(ControlsDict(ControlNames.RemoveFaceStyleOverrides.ToString), CheckBox).Visible = Not Me.StructuredStorageEdit

            '    tf = Not Me.StructuredStorageEdit
            '    tf = tf And CType(ControlsDict(ControlNames.UpdateFaceStyles.ToString), CheckBox).Checked

            '    CType(ControlsDict(ControlNames.UseFinishFaceStyle.ToString), CheckBox).Visible = tf

            '    tf = Not Me.StructuredStorageEdit
            '    tf = tf And CType(ControlsDict(ControlNames.UpdateFaceStyles.ToString), CheckBox).Checked
            '    tf = tf And CType(ControlsDict(ControlNames.UseFinishFaceStyle.ToString), CheckBox).Checked

            '    CType(ControlsDict(ControlNames.FinishPropertyFormulaLabel.ToString), Label).Visible = tf
            '    CType(ControlsDict(ControlNames.FinishPropertyFormula.ToString), TextBox).Visible = tf
            '    CType(ControlsDict(ControlNames.ExcludedFinishesLabel.ToString), Label).Visible = tf
            '    CType(ControlsDict(ControlNames.ExcludedFinishes.ToString), ComboBox).Visible = tf
            '    CType(ControlsDict(ControlNames.DeleteExcludedFinish.ToString), Button).Visible = tf
            '    CType(ControlsDict(ControlNames.OverrideBodyFaceStyle.ToString), CheckBox).Visible = tf
            '    CType(ControlsDict(ControlNames.OverrideMaterialFaceStyle.ToString), CheckBox).Visible = tf

            Case ControlNames.RemoveFaceStyleOverrides.ToString
                Me.RemoveFaceStyleOverrides = Checkbox.Checked

            Case ControlNames.UpdateFaceStyles.ToString
                Me.UpdateFaceStyles = Checkbox.Checked

                tf = Me.UpdateFaceStyles
                'tf = tf And Not Me.StructuredStorageEdit

                CType(ControlsDict(ControlNames.UseFinishFaceStyle.ToString), CheckBox).Visible = tf

                tf = Me.UpdateFaceStyles
                'tf = tf And Not Me.StructuredStorageEdit
                tf = tf And CType(ControlsDict(ControlNames.UseFinishFaceStyle.ToString), CheckBox).Checked

                CType(ControlsDict(ControlNames.FinishPropertyFormulaLabel.ToString), Label).Visible = tf
                CType(ControlsDict(ControlNames.FinishPropertyFormula.ToString), TextBox).Visible = tf
                CType(ControlsDict(ControlNames.ExcludedFinishesLabel.ToString), Label).Visible = tf
                CType(ControlsDict(ControlNames.ExcludedFinishes.ToString), TextBox).Visible = tf
                'CType(ControlsDict(ControlNames.DeleteExcludedFinish.ToString), Button).Visible = tf
                CType(ControlsDict(ControlNames.OverrideBodyFaceStyle.ToString), CheckBox).Visible = tf
                CType(ControlsDict(ControlNames.OverrideMaterialFaceStyle.ToString), CheckBox).Visible = tf

            Case ControlNames.UseFinishFaceStyle.ToString
                Me.UseFinishFaceStyle = Checkbox.Checked

                tf = Me.UseFinishFaceStyle
                tf = tf And CType(ControlsDict(ControlNames.UpdateFaceStyles.ToString), CheckBox).Checked
                tf = tf And CType(ControlsDict(ControlNames.UseFinishFaceStyle.ToString), CheckBox).Checked

                CType(ControlsDict(ControlNames.FinishPropertyFormulaLabel.ToString), Label).Visible = tf
                CType(ControlsDict(ControlNames.FinishPropertyFormula.ToString), TextBox).Visible = tf
                CType(ControlsDict(ControlNames.ExcludedFinishesLabel.ToString), Label).Visible = tf
                CType(ControlsDict(ControlNames.ExcludedFinishes.ToString), TextBox).Visible = tf
                'CType(ControlsDict(ControlNames.DeleteExcludedFinish.ToString), Button).Visible = tf
                CType(ControlsDict(ControlNames.OverrideBodyFaceStyle.ToString), CheckBox).Visible = tf
                CType(ControlsDict(ControlNames.OverrideMaterialFaceStyle.ToString), CheckBox).Visible = tf

            Case ControlNames.OverrideBodyFaceStyle.ToString
                'Me.OverrideBodyFaceStyle = Checkbox.Checked

                'CType(ControlsDict(ControlNames.OverrideMaterialFaceStyle.ToString), CheckBox).Checked = Not Me.OverrideBodyFaceStyle

                Me.OverrideBodyFaceStyle = True

            Case ControlNames.OverrideMaterialFaceStyle.ToString
                'Me.OverrideMaterialFaceStyle = Checkbox.Checked

                'CType(ControlsDict(ControlNames.OverrideBodyFaceStyle.ToString), CheckBox).Checked = Not Me.OverrideMaterialFaceStyle

                Me.OverrideMaterialFaceStyle = False

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox
        'Dim ComboBox As ComboBox

        Select Case Name
            Case ControlNames.Browse.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a material table file"
                tmpFileDialog.Filter = "Material Documents|*.mtl"

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.MaterialTable = tmpFileDialog.FileName
                    TextBox = CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox)
                    TextBox.Text = Me.MaterialTable
                End If

                'Case ControlNames.DeleteExcludedFinish.ToString
                '    ComboBox = CType(ControlsDict(ControlNames.ExcludedFinishes.ToString), ComboBox)
                '    ComboBox.Items.Remove(ComboBox.SelectedItem)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name
            Case ControlNames.MaterialTable.ToString '"ExternalProgram"
                Me.MaterialTable = TextBox.Text

            Case ControlNames.FinishPropertyFormula.ToString
                Me.FinishPropertyFormula = TextBox.Text

            Case ControlNames.ExcludedFinishes.ToString
                Dim tmpList As New List(Of String)
                For Each s As String In TextBox.Text.Split(CChar(vbLf)).ToList
                    s = s.Replace(vbCr, "").Replace(vbCrLf, "")
                    If Not s.Trim = "" Then
                        tmpList.Add(s)
                    End If
                Next
                Me.ExcludedFinishesList = tmpList

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub

    Public Sub ComboBoxOptions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Dim ComboBox = CType(sender, ComboBox)
        Dim Name = ComboBox.Name
        Dim CheckBox As CheckBox = Nothing
        Dim TextBox As TextBox = Nothing
        Dim Label As Label = Nothing

        Select Case Name
            Case ControlNames.ExcludedFinishes.ToString

                If Not ComboBox.Items.Contains(ComboBox.Text) Then
                    ComboBox.Items.Add(ComboBox.Text)
                End If

                Me.ExcludedFinishesList.Clear()

                For Each Item As String In ComboBox.Items
                    ExcludedFinishesList.Add(Item)
                Next


            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select

    End Sub

    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Checks to see if the part's material name and properties match any material "
        HelpString += "in your material table. "

        HelpString += vbCrLf + vbCrLf + "![UpdateMaterialFromMaterialTable](My%20Project/media/task_update_material_from_material_table.png)"

        HelpString += vbCrLf + vbCrLf + "If the names match, "
        HelpString += "but their properties (e.g., density, face style, etc.) do not, the material is updated. "
        HelpString += "If no match is found, or no material is assigned, it is reported in the log file."

        HelpString += vbCrLf + vbCrLf + "There are several options for this command. "
        HelpString += vbCrLf + vbCrLf + "- `Remove face style overrides`:  Change all face styles to match that of the material. "
        HelpString += vbCrLf + "- `Update face styles`:  Disabling this option leaves faces unchanged. "
        HelpString += vbCrLf + "- `Finish property determines face style`:  Uses the finish rather than the material face style. "
        HelpString += "Note a face style with the same name as the finish must be present in the file. "
        HelpString += vbCrLf + "- `Finish property`: The property that contains finish information. "
        HelpString += "Note the syntax required in the image above. "
        HelpString += "Right click the text box to select it from a list. "
        HelpString += vbCrLf + "- `Finishes that don't change material appearance`: Enter these in the list provided. "
        HelpString += "Note no action is taken with these finishes, so their face styles do *not* need to be present in the file. "
        HelpString += vbCrLf + "- `Override the Body face style`: Uses Part Painter to change the faces. "
        HelpString += vbCrLf + "- `Override the Material face style`: Uses the Material Table to change the faces. "
        HelpString += "Note this option is currently disabled. "

        Return HelpString
    End Function


End Class
