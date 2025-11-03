Option Strict On

Public Class TaskCheckMaterialNotInMaterialTable

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

    Private _StructuredStorageEdit As Boolean
    Public Property StructuredStorageEdit As Boolean
        Get
            Return _StructuredStorageEdit
        End Get
        Set(value As Boolean)
            _StructuredStorageEdit = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.StructuredStorageEdit.ToString), CheckBox).Checked = value
                Me.SolidEdgeRequired = Not value
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
        Me.AppliesToDraft = False
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskCheckMaterialNotInMaterialTable
        Me.Category = "Check"
        Me.RequiresMaterialTable = True
        Me.RequiresPropertiesData = True
        SetColorFromCategory(Me)
        Me.SolidEdgeRequired = True  ' Default is so checking the box toggles a property update

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.MaterialTable = ""

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
        ProcessInternal(FileName)
    End Sub

    Private Overloads Sub ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case "asm", "par", "psm"
                Dim UM As New UtilsMaterials
                UM.MaterialNotInMaterialTable(SEApp, SEDoc, Me.MaterialTable, TaskLogger)
            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        End Select

    End Sub

    Private Overloads Sub ProcessInternal(ByVal FullName As String)

        Dim Proceed As Boolean = True
        Dim Matl As String = ""

        Dim SSDoc As HCStructuredStorageDoc = Nothing
        Dim SSMatTable As HCStructuredStorageDoc = Nothing

        Try
            SSDoc = New HCStructuredStorageDoc(FullName)
            SSMatTable = New HCStructuredStorageDoc(Me.MaterialTable)
        Catch ex As Exception
            If SSDoc IsNot Nothing Then SSDoc.Close()
            If SSMatTable IsNot Nothing Then SSMatTable.Close()
            Proceed = False
            TaskLogger.AddMessage(ex.Message)
        End Try

        If Proceed Then
            SSDoc.ReadProperties(Me.PropertiesData)
            SSMatTable.ReadMaterialTable()

            Matl = CType(SSDoc.GetPropValue("System", "Material"), String)

            If (Matl Is Nothing) Or (SSDoc.IsFileEmpty) Then
                Proceed = False  ' Empty files or ones without a Material property are not errors.
            End If
        End If

        If Proceed Then
            If Matl.Trim = "" Then
                Proceed = False
                TaskLogger.AddMessage("Material 'None' not in material table")
            End If
        End If

        If Proceed Then
            If Not SSMatTable.MaterialInTable(Matl) Then
                Proceed = False
                TaskLogger.AddMessage(String.Format("Material '{0}' not in material table", Matl))
            End If
        End If

        If SSDoc IsNot Nothing Then SSDoc.Close()
        If SSMatTable IsNot Nothing Then SSMatTable.Close()

    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim TextBox As TextBox
        Dim Button As Button

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 2)

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

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.StructuredStorageEdit.ToString, "Run task without Solid Edge")
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

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If

            If Not FileIO.FileSystem.FileExists(Me.MaterialTable) Then
                ErrorLogger.AddMessage("Select a valid material table")
            End If

        End If

    End Sub


    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox

        Select Case Name

            Case ControlNames.Browse.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a material library file"
                tmpFileDialog.Filter = "mtl files|*.mtl"

                tmpFileDialog.InitialDirectory = Form_Main.SEMaterialsPath

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.MaterialTable = tmpFileDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox)
                    TextBox.Text = Me.MaterialTable

                    Form_Main.SEMaterialsPath = IO.Path.GetDirectoryName(Me.MaterialTable)
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select

    End Sub

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

            Case ControlNames.StructuredStorageEdit.ToString
                Me.StructuredStorageEdit = Checkbox.Checked
                Me.SolidEdgeRequired = Not Checkbox.Checked

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

        Select Case Name

            Case ControlNames.MaterialTable.ToString
                Me.MaterialTable = TextBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub



    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Checks if the material in the file exists in the material table. "

        HelpString += vbCrLf + vbCrLf + "![CheckMaterialNotInMaterialTable](My%20Project/media/task_check_material_not_in_material_table.png)"

        HelpString += vbCrLf + vbCrLf + "The material table is chosen on the Options panel. "
        HelpString += "This command only checks the name.  To check/update its properties (density, face style, etc.), "
        HelpString += "run the `Update material from material table` command. "

        Return HelpString
    End Function

End Class
