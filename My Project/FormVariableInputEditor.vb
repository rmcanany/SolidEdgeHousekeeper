Option Explicit On

Imports Newtonsoft.Json

Public Class FormVariableInputEditor
    Private ColumnsDict As New Dictionary(Of Integer, Dictionary(Of String, String))
    Private InputEditorDoctor As New InputEditorDoctor
    Private ProcessCheckBoxEvents As Boolean
    Private FileType As String
    Public Property JSONDict As String
    Public Property UseNewTaskTab As Boolean



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

        TextBoxJSON.Text = Me.JSONDict

        ProcessCheckBoxEvents = False
        InputEditorDoctor.RestoreTableValues(TableLayoutPanel1, ColumnsDict, TextBoxJSON.Text)
        ProcessCheckBoxEvents = True

        ReconcileFormControls()

    End Sub

    Private Sub BuildColumnsDict()

        '{ColumnIndex: {
        '    ColumnControl: "CheckBox" | "TextBox" | "ComboBox",
        '    ColumnName: String,
        '    ...
        '    }
        '}

        Dim ColumnName As String
        Dim ColumnIndex As Integer

        ColumnIndex = 0
        ColumnName = "Select"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(True)
        ColumnsDict(ColumnIndex)("DefaultValue") = CStr(False)
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(True)

        ColumnIndex += 1
        ColumnName = "VariableName"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "TextBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(False)
        ColumnsDict(ColumnIndex)("DefaultValue") = ""
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "Formula"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "TextBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(True)
        ColumnsDict(ColumnIndex)("DefaultValue") = ""
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "UnitType"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "ComboBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(False)
        ColumnsDict(ColumnIndex)("DefaultValue") = ""
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "Expose"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(False)
        ColumnsDict(ColumnIndex)("DefaultValue") = CStr(False)
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "ExposeName"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "TextBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(True)
        ColumnsDict(ColumnIndex)("DefaultValue") = ""
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

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

        Dim TableValuesDict As New Dictionary(Of Integer, Dictionary(Of String, String))

        TableValuesDict = InputEditorDoctor.GetTableValues(TableLayoutPanel1, ColumnsDict)
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

    Private Sub TextBoxJSON_TextChanged(sender As Object, e As EventArgs) Handles TextBoxJSON.TextChanged
        Me.JSONDict = TextBoxJSON.Text
    End Sub

End Class