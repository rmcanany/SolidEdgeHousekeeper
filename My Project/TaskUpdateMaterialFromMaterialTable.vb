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
        UpdateFaceStyles
        RemoveFaceStyleOverrides
        'StructuredStorageEdit
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
        Me.UpdateFaceStyles = False
        Me.RemoveFaceStyleOverrides = False
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

        'ErrorMessage = ProcessInternal(FileName)

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

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case "par", "psm"
                Dim UM As New UtilsMaterials
                SupplementalErrorMessage = UM.UpdateMaterialFromMaterialTable(
                    SEApp, SEDoc, Me.MaterialTable, Me.UpdateFaceStyles, Me.RemoveFaceStyleOverrides)
                AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)

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

        'Dim IU As New InterfaceUtilities

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

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateFaceStyles.ToString, "Update face styles")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        'CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.RemoveFaceStyleOverrides.ToString, "Remove face style overrides")
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

            Case ControlNames.UpdateFaceStyles.ToString
                Me.UpdateFaceStyles = Checkbox.Checked

                'CType(ControlsDict(ControlNames.RemoveFaceStyleOverrides.ToString), CheckBox).Visible = Me.UpdateFaceStyles

            Case ControlNames.RemoveFaceStyleOverrides.ToString
                Me.RemoveFaceStyleOverrides = Checkbox.Checked

            'Case ControlNames.StructuredStorageEdit.ToString
            '    Me.StructuredStorageEdit = Checkbox.Checked
            '    'Me.RequiresSave = Not Checkbox.Checked
            '    Me.SolidEdgeRequired = Not Checkbox.Checked

            '    CType(ControlsDict(ControlNames.RemoveFaceStyleOverrides.ToString), CheckBox).Visible = Not Me.StructuredStorageEdit

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
            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Checks to see if the part's material name and properties match any material "
        HelpString += "in a file you specify on the Options panel. "

        HelpString += vbCrLf + vbCrLf + "![UpdateMaterialFromMaterialTable](My%20Project/media/task_update_material_from_material_table.png)"

        HelpString += vbCrLf + vbCrLf + "If the names match, "
        HelpString += "but their properties (e.g., density, face style, etc.) do not, the material is updated. "
        HelpString += "If no match is found, or no material is assigned, it is reported in the log file."
        HelpString += vbCrLf + vbCrLf + "You can optionally remove any face style overrides. "
        HelpString += "Set the option on the Options panel. "

        Return HelpString
    End Function


End Class
