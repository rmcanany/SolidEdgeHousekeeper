Option Explicit On

Imports System.Data.Common
Imports System.Globalization
Imports System.Net.Http
Imports Newtonsoft.Json
'Imports Newtonsoft.Json
Imports SolidEdgeConstants
'Imports System.Runtime.Serialization.Json


Public Class FormVariableInputEditor
    Private ColumnsDict As New Dictionary(Of String, Dictionary(Of String, String))
    Private InputEditorDoctor As New InputEditorDoctor
    Private ProcessCheckBoxEvents As Boolean
    Private FileType As String

    'Private ColumnNames As New List(Of String)
    'Dim ColumnControlsList As New List(Of String)
    'Dim PopulatingControls As Boolean

    Public Sub ShowInputEditor(FileType As String)
        Me.FileType = FileType

        If FileType = "asm" Then
            CheckBoxCopyToAsm.Checked = True
            CheckBoxCopyToAsm.Enabled = False
        End If
        If FileType = "par" Then
            CheckBoxCopyToPar.Checked = True
            CheckBoxCopyToPar.Enabled = False
        End If
        If FileType = "psm" Then
            CheckBoxCopyToPsm.Checked = True
            CheckBoxCopyToPsm.Enabled = False
        End If
        If FileType = "dft" Then
            CheckBoxCopyToDft.Checked = True
            CheckBoxCopyToDft.Enabled = False
        End If

        CheckBoxCopyToDft.Enabled = False

        Me.ShowDialog()

    End Sub


    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' https://www.codeproject.com/Tips/842418/Designing-the-Layout-of-Windows-Forms-using-a
        ' https://www.vbforums.com/showthread.php?891013-How-to-iterate-rows-and-columns-in-TableLayoutPanel

        BuildColumnsDict()

        ProcessCheckBoxEvents = False
        InputEditorDoctor.PopulateControls(TableLayoutPanel1, ColumnsDict)
        ProcessCheckBoxEvents = True

        InputEditorDoctor.SetHeaderRowColor(TableLayoutPanel1, ProcessAllRows:=False)

        Dim CheckBoxDict As New Dictionary(Of String, CheckBox)
        Dim CheckBoxName As String
        CheckBoxDict = InputEditorDoctor.GetCheckBoxes(TableLayoutPanel1)
        For Each CheckBoxName In CheckBoxDict.Keys
            AddHandler CheckBoxDict(CheckBoxName).CheckedChanged, AddressOf CheckBox_CheckedChanged
        Next

        Dim ComboBoxDict As New Dictionary(Of String, ComboBox)
        Dim ComboBoxName As String
        ComboBoxDict = InputEditorDoctor.GetComboBoxes(TableLayoutPanel1)
        For Each ComboBoxName In ComboBoxDict.Keys
            For Each UnitTypeConstant As SolidEdgeConstants.UnitTypeConstants In System.Enum.GetValues(GetType(SolidEdgeConstants.UnitTypeConstants))
                ComboBoxDict(ComboBoxName).Items.Add(UnitTypeConstant.ToString.Replace("igUnit", ""))
            Next
            ComboBoxDict(ComboBoxName).Text = ComboBoxDict(ComboBoxName).Items(0).ToString
        Next

        If Me.FileType = "asm" Then TextBoxJSON.Text = Form1.TextBoxVariablesEditAssembly.Text
        If Me.FileType = "par" Then TextBoxJSON.Text = Form1.TextBoxVariablesEditPart.Text
        If Me.FileType = "psm" Then TextBoxJSON.Text = Form1.TextBoxVariablesEditSheetmetal.Text
        'If Me.FileType = "dft" Then TextBoxJSON.Text = Form1.TextBoxVariablesEditDraft.Text

        ProcessCheckBoxEvents = False
        InputEditorDoctor.RestoreTableValues(TableLayoutPanel1, ColumnsDict, "VariableName", TextBoxJSON.Text)
        ProcessCheckBoxEvents = True

        ReconcileFormControls()

    End Sub

    Private Sub BuildColumnsDict()
        '{ColumnName: {
        '    ColumnControl: "CheckBox" | "TextBox" | "ComboBox",
        '    ColumnIndex: Integer
        '    }
        '}
        Dim ColumnName As String
        Dim ColumnIndex As Integer = 0

        ColumnName = "Select"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "VariableName"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "TextBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "Formula"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "TextBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "UnitType"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "ComboBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "Expose"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "ExposeName"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "TextBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub



    'Private Function GetCheckBoxes() As Dictionary(Of String, CheckBox)
    '    Dim CheckBoxDict As New Dictionary(Of String, CheckBox)
    '    Dim Control As Control

    '    For Each Control In TableLayoutPanel1.Controls
    '        If Control.GetType() Is GetType(CheckBox) Then
    '            CheckBoxDict(Control.Name) = DirectCast(Control, CheckBox)
    '        End If
    '    Next

    '    Return CheckBoxDict
    'End Function

    'Private Function GetTextBoxes() As Dictionary(Of String, TextBox)
    '    Dim TextBoxDict As New Dictionary(Of String, TextBox)
    '    Dim Control As Control

    '    For Each Control In TableLayoutPanel1.Controls
    '        If Control.GetType() Is GetType(TextBox) Then
    '            TextBoxDict(Control.Name) = DirectCast(Control, TextBox)
    '        End If
    '    Next

    '    Return TextBoxDict
    'End Function

    Private Sub TextBox_TextChanged(sender As System.Object, e As System.EventArgs)
        'When you modify the contents of any textbox, the name of that textbox
        'and its current contents will be displayed in the title bar

        'Dim box As TextBox = DirectCast(sender, TextBox)
        'Me.Text = box.Name & ": " & box.Text
    End Sub

    Private Sub ReconcileFormControls()
        Dim CheckBoxDict As New Dictionary(Of String, CheckBox)
        Dim CheckBoxName As String
        Dim TextBoxDict As New Dictionary(Of String, TextBox)
        Dim TextBoxName As String
        Dim RowIndex As Integer
        Dim s As String
        Dim VariableName As String
        Dim SelectedRowIndices As New List(Of Integer)

        CheckBoxDict = InputEditorDoctor.GetCheckBoxes(TableLayoutPanel1)
        TextBoxDict = InputEditorDoctor.GetTextBoxes(TableLayoutPanel1)

        For Each CheckBoxName In CheckBoxDict.Keys
            If CheckBoxName.Contains("Expose") Then
                s = CheckBoxName.Replace("CheckBox", "")(0)
                RowIndex = CInt(s)

                TextBoxName = String.Format("TextBox{0}ExposeName", RowIndex)
                VariableName = String.Format("TextBox{0}VariableName", RowIndex)

                If CheckBoxDict(CheckBoxName).Checked Then
                    TextBoxDict(TextBoxName).Enabled = True
                    If TextBoxDict(TextBoxName).Text = "" Then
                        TextBoxDict(TextBoxName).Text = TextBoxDict(VariableName).Text
                    End If
                Else
                    TextBoxDict(TextBoxName).Enabled = False
                    TextBoxDict(TextBoxName).Text = ""
                End If

            End If
        Next

        SelectedRowIndices = InputEditorDoctor.GetSelectedRowIndices(TableLayoutPanel1)

        ButtonClearSelected.Enabled = False
        ButtonMoveSelectedDown.Enabled = False
        ButtonMoveSelectedUp.Enabled = False

        If SelectedRowIndices.Count = 1 Then
            ButtonClearSelected.Enabled = True
            ButtonMoveSelectedDown.Enabled = True
            ButtonMoveSelectedUp.Enabled = True
        ElseIf SelectedRowIndices.Count > 1 Then
            ButtonClearSelected.Enabled = True
        End If

    End Sub

    Private Sub CheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs)
        If ProcessCheckBoxEvents Then
            ReconcileFormControls()
        End If
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        ' https://stackoverflow.com/questions/20551203/how-to-get-values-from-a-dialog-form-in-vb-net

        Dim TableValuesDict As New Dictionary(Of String, Dictionary(Of String, String))

        TableValuesDict = InputEditorDoctor.GetTableValues(TableLayoutPanel1, ColumnsDict, "VariableName")
        TextBoxJSON.Text = JsonConvert.SerializeObject(TableValuesDict)

        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub CheckBoxSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSelectAll.CheckedChanged
        ProcessCheckBoxEvents = False
        InputEditorDoctor.ProcessCheckBoxSelectAll(TableLayoutPanel1, CheckBoxSelectAll)
        ProcessCheckBoxEvents = True

        ReconcileFormControls()

    End Sub

    Private Sub ButtonClearSelected_Click(sender As Object, e As EventArgs) Handles ButtonClearSelected.Click
        ProcessCheckBoxEvents = False
        InputEditorDoctor.ClearSelected(TableLayoutPanel1)
        ProcessCheckBoxEvents = True

        CheckBoxSelectAll.Checked = False

        ReconcileFormControls()

    End Sub


    Private Sub ButtonMoveSelectedUp_Click(sender As Object, e As EventArgs) Handles ButtonMoveSelectedUp.Click
        ProcessCheckBoxEvents = False
        InputEditorDoctor.MoveSelected(TableLayoutPanel1, ColumnsDict, "Up")
        ProcessCheckBoxEvents = True

        ReconcileFormControls()

        'MoveSelected("Up")
    End Sub

    Private Sub ButtonMoveSelectedDown_Click(sender As Object, e As EventArgs) Handles ButtonMoveSelectedDown.Click
        ProcessCheckBoxEvents = False
        InputEditorDoctor.MoveSelected(TableLayoutPanel1, ColumnsDict, "Down")
        ProcessCheckBoxEvents = True

        ReconcileFormControls()

        'MoveSelected("Down")
    End Sub

    Private Sub TableLayoutPanel1_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) Handles TableLayoutPanel1.CellPaint
        ' https://stackoverflow.com/questions/34064499/how-to-set-cell-color-in-tablelayoutpanel-dynamically
        If e.Row = 0 Then
            e.Graphics.FillRectangle(InputEditorDoctor.BrushColor, e.CellBounds)
        End If
    End Sub

End Class