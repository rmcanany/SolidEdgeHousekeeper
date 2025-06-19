Option Strict On

Public Class TaskUpdateDrawingBlocks

    Inherits Task

    Private _BlockLibrary As String
    Public Property BlockLibrary As String
        Get
            Return _BlockLibrary
        End Get
        Set(value As String)
            _BlockLibrary = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BlockLibrary.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _ReplaceBlocks As Boolean
    Public Property ReplaceBlocks As Boolean
        Get
            Return _ReplaceBlocks
        End Get
        Set(value As Boolean)
            _ReplaceBlocks = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ReplaceBlocks.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _AddBlocks As Boolean
    Public Property AddBlocks As Boolean
        Get
            Return _AddBlocks
        End Get
        Set(value As Boolean)
            _AddBlocks = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AddBlocks.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _DeleteBlocks As Boolean
    Public Property DeleteBlocks As Boolean
        Get
            Return _DeleteBlocks
        End Get
        Set(value As Boolean)
            _DeleteBlocks = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DeleteBlocks.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _ReplaceBlocksList As List(Of String)
    Public Property ReplaceBlocksList As List(Of String)
        Get
            Return _ReplaceBlocksList
        End Get
        Set(value As List(Of String))
            _ReplaceBlocksList = value
            '_ReplaceBlocksList.Sort()

            'If Me.TaskOptionsTLP IsNot Nothing Then
            '    Dim tmpDataGridView As DataGridView = CType(ControlsDict(ControlNames.ReplaceBlocksDGV.ToString), DataGridView)
            '    'Dim s1 As String = ""
            '    'For Each s2 As String In _ReplaceBlocksList
            '    '    If Not s2.Trim = "" Then
            '    '        s1 = String.Format("{0}{1}{2}", s1, s2, vbCrLf)
            '    '    End If
            '    'Next
            '    'If Not s1 = "" Then s1 = s1.Substring(0, s1.Length - 1) ' Remove trailing vbcrlf

            '    'If Not tmpTextBox.Text = s1 Then tmpTextBox.Text = s1

            'End If
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
        Browse
        BlockLibrary
        ReplaceBlocks
        ReplaceBlocksDGV
        DeleteBlocks
        DeleteBlocksDGV
        AddBlocks
        AddBlocksDGV
        AutoHideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = False
        Me.AppliesToPart = False
        Me.AppliesToSheetmetal = False
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskUpdateMaterialFromMaterialTable
        Me.Category = "Update"
        'Me.RequiresMaterialTable = True
        'Me.RequiresPropertiesData = True
        SetColorFromCategory(Me)
        'Me.SolidEdgeRequired = False
        Me.SolidEdgeRequired = True  ' Default is so checking the box toggles a property update

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.BlockLibrary = ""
        Me.ReplaceBlocks = False
        Me.AddBlocks = False
        Me.DeleteBlocks = False
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

        'Dim PropertySetName As String
        'Dim PropertyName As String
        'Dim FinishName As String = Nothing

        'Dim UC As New UtilsCommon
        'Dim DocType As String = UC.GetDocType(SEDoc)

        'Select Case DocType
        '    Case "par", "psm"
        '        If Me.UseFinishFaceStyle Then
        '            PropertySetName = UC.PropSetFromFormula(Me.FinishPropertyFormula)
        '            PropertyName = UC.PropNameFromFormula(Me.FinishPropertyFormula)
        '            FinishName = CStr(UC.GetPropValue(
        '                SEDoc, PropertySetName, PropertyName, ModelLinkIdx:=0, AddProp:=False))

        '            If FinishName Is Nothing Then
        '                TaskLogger.AddMessage(String.Format("Property '{0}' not found", Me.FinishPropertyFormula))
        '            End If
        '        End If

        '        If Not TaskLogger.HasErrors Then
        '            Dim UM As New UtilsMaterials

        '            UM.UpdateMaterialFromMaterialTable(
        '                SEApp, SEDoc, Me.MaterialTable, Me.RemoveFaceStyleOverrides, Me.UpdateFaceStyles,
        '                Me.UseFinishFaceStyle, FinishName, Me.ExcludedFinishesList,
        '                Me.OverrideBodyFaceStyle, Me.OverrideMaterialFaceStyle, TaskLogger)
        '        End If

        '    Case Else
        '        MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        'End Select

        'If SEDoc.ReadOnly Then
        '    TaskLogger.AddMessage("Cannot save document marked 'Read Only'")

        'Else
        '    SEDoc.Save()
        '    SEApp.DoIdle()
        'End If

    End Sub

    Private Overloads Sub ProcessInternal(ByVal FullName As String)

    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Button As Button
        Dim TextBox As TextBox
        'Dim Label As Label
        Dim DataGridView As DataGridView
        Dim ColumnHeaders As List(Of String)

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        Button = FormatOptionsButton(ControlNames.Browse.ToString, "Block Library")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.BlockLibrary.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ReplaceBlocks.ToString, "Replace blocks")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        ColumnHeaders = {"File block name", "Template block name"}.ToList
        DataGridView = FormatOptionsDataGridView(ControlNames.ReplaceBlocksDGV.ToString, ColumnHeaders)
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
        tmpTLPOptions.Controls.Add(DataGridView, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(DataGridView, 2)
        For i = 0 To ColumnHeaders.Count - 1
            DataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        ControlsDict(DataGridView.Name) = DataGridView
        DataGridView.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DeleteBlocks.ToString, "Delete blocks")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        'CheckBox.Visible = False

        RowIndex += 1

        ColumnHeaders = {"File block name"}.ToList
        DataGridView = FormatOptionsDataGridView(ControlNames.DeleteBlocksDGV.ToString, ColumnHeaders)
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
        tmpTLPOptions.Controls.Add(DataGridView, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(DataGridView, 2)
        For i = 0 To ColumnHeaders.Count - 1
            DataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        ControlsDict(DataGridView.Name) = DataGridView
        DataGridView.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AddBlocks.ToString, "Add blocks")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        'CheckBox.Visible = False

        RowIndex += 1

        ColumnHeaders = {"Template block name"}.ToList
        DataGridView = FormatOptionsDataGridView(ControlNames.AddBlocksDGV.ToString, ColumnHeaders)
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
        tmpTLPOptions.Controls.Add(DataGridView, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(DataGridView, 2)
        For i = 0 To ColumnHeaders.Count - 1
            DataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        ControlsDict(DataGridView.Name) = DataGridView
        DataGridView.Visible = False

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

            'If Not FileIO.FileSystem.FileExists(Me.MaterialTable) Then
            '    ErrorLogger.AddMessage("Select a valid material table")
            'End If

            'If Me.UpdateFaceStyles And Me.UseFinishFaceStyle Then
            '    Dim UC As New UtilsCommon

            '    If Not UC.CheckValidPropertyFormulas(Me.FinishPropertyFormula) Then
            '        ErrorLogger.AddMessage(String.Format("Could not parse property formula '{0}'", Me.FinishPropertyFormula))
            '    End If

            '    If Not ((Me.OverrideBodyFaceStyle) Or (Me.OverrideMaterialFaceStyle)) Then
            '        ErrorLogger.AddMessage("Select an override method")
            '    End If
            'End If
        End If

    End Sub


    Public Sub DataGridViewOptions_Leave(sender As System.Object, e As System.EventArgs)

        Dim DataGridView = CType(sender, DataGridView)
        Dim Name = DataGridView.Name

        Select Case Name
            Case ControlNames.ReplaceBlocksDGV.ToString
                Dim i = 0
                Me.ReplaceBlocksList.Clear()
                For RowIdx = 0 To DataGridView.Rows.Count - 1
                    If DataGridView.Rows(i).IsNewRow Then
                        Continue For
                    End If
                    Dim ListItem As String = ""
                    For ColIdx = 0 To DataGridView.Columns.Count - 1
                        Dim Value As String = CStr(DataGridView.Rows(RowIdx).Cells(ColIdx).Value)
                        If ColIdx = 0 Then
                            ListItem = Value
                        Else
                            ListItem = $"{ListItem},{Value}"
                        End If
                    Next
                    Me.ReplaceBlocksList.Add(ListItem)
                Next

            Case ControlNames.DeleteBlocksDGV.ToString
                Dim i = 0
            Case ControlNames.AddBlocksDGV.ToString
                Dim i = 0

        End Select

    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.ReplaceBlocks.ToString
                Me.ReplaceBlocks = Checkbox.Checked
                CType(ControlsDict(ControlNames.ReplaceBlocksDGV.ToString), DataGridView).Visible = Me.ReplaceBlocks
                CType(ControlsDict(ControlNames.ReplaceBlocksDGV.ToString), DataGridView).Width = TaskOptionsTLP.Width - 5

            Case ControlNames.AddBlocks.ToString
                Me.AddBlocks = Checkbox.Checked
                CType(ControlsDict(ControlNames.AddBlocksDGV.ToString), DataGridView).Visible = Me.AddBlocks
                CType(ControlsDict(ControlNames.AddBlocksDGV.ToString), DataGridView).Width = TaskOptionsTLP.Width - 5

            Case ControlNames.DeleteBlocks.ToString
                Me.DeleteBlocks = Checkbox.Checked
                CType(ControlsDict(ControlNames.DeleteBlocksDGV.ToString), DataGridView).Visible = Me.DeleteBlocks
                CType(ControlsDict(ControlNames.DeleteBlocksDGV.ToString), DataGridView).Width = TaskOptionsTLP.Width - 5

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
                tmpFileDialog.Title = "Select a block library"
                tmpFileDialog.Filter = "Draft Documents|*.dft"

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.BlockLibrary = tmpFileDialog.FileName
                    TextBox = CType(ControlsDict(ControlNames.BlockLibrary.ToString), TextBox)
                    TextBox.Text = Me.BlockLibrary
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name
            Case ControlNames.BlockLibrary.ToString
                Me.BlockLibrary = TextBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Adds, replaces and/or deletes blocks from a draft file. "

        HelpString += vbCrLf + vbCrLf + "![UpdateMaterialFromMaterialTable](My%20Project/media/task_update_material_from_material_table.png)"


        Return HelpString
    End Function


End Class
