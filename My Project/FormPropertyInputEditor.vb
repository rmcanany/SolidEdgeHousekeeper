Option Strict On
Imports System.Runtime.InteropServices
Imports System.Security.AccessControl
Imports Newtonsoft.Json

Public Class FormPropertyInputEditor
    Private ColumnsDict As New Dictionary(Of String, Dictionary(Of String, String))
    Private InputEditorDoctor As New InputEditorDoctor
    Private ProcessCheckBoxEvents As Boolean
    Private FileType As String

    'TableValuesDict format
    '{"Material":{
    '    "PropertySet":"System",
    '    "PropertyName":"Material",
    '    "Find_PT":"True",
    '    "Find_WC":"False",
    '    "Find_RX":"False",
    '    "FindString":"Aluminum",
    '    "Replace_PT":"True",
    '    "Replace_RX":"False",
    '    "ReplaceString":"Aluminum 6061-T6"},
    ' ...
    '}

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

        Me.ShowDialog()

    End Sub

    Private Sub FormPropertyInputEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
            ComboBoxDict(ComboBoxName).Items.Add("System")
            ComboBoxDict(ComboBoxName).Items.Add("Custom")
            ComboBoxDict(ComboBoxName).Text = ComboBoxDict(ComboBoxName).Items(0).ToString
        Next

        If Me.FileType = "asm" Then TextBoxJSON.Text = Form1.TextBoxPropertiesEditAssembly.Text
        If Me.FileType = "par" Then TextBoxJSON.Text = Form1.TextBoxPropertiesEditPart.Text
        If Me.FileType = "psm" Then TextBoxJSON.Text = Form1.TextBoxPropertiesEditSheetmetal.Text
        If Me.FileType = "dft" Then TextBoxJSON.Text = Form1.TextBoxPropertiesEditDraft.Text

        ProcessCheckBoxEvents = False
        InputEditorDoctor.RestoreTableValues(TableLayoutPanel1, ColumnsDict, "PropertyName", TextBoxJSON.Text)
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
        ColumnName = "PropertySet"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "ComboBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "PropertyName"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "TextBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "Find_PT"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "Find_WC"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "Find_RX"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "FindString"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "TextBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "Replace_PT"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "Replace_RX"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

        ColumnIndex += 1
        ColumnName = "ReplaceString"
        ColumnsDict(ColumnName) = New Dictionary(Of String, String)
        ColumnsDict(ColumnName)("ColumnControl") = "TextBox"
        ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

    End Sub

    Private Sub CheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs)
        Dim CheckBox As CheckBox = DirectCast(sender, CheckBox)
        Dim tf As Boolean
        Dim s As String = ""
        Dim RowIndexString As String = ""
        Dim FindOrReplace As String = ""
        Dim SearchType As String = ""
        Dim CheckBoxDict As New Dictionary(Of String, CheckBox)

        If CheckBox.Name.Contains("Select") Then ReconcileFormControls()

        If ProcessCheckBoxEvents Then
            CheckBoxDict = InputEditorDoctor.GetCheckBoxes(TableLayoutPanel1)


            ' Make sure only one checkbox is checked.
            tf = CheckBox.Name.Contains("Find_")
            tf = tf Or CheckBox.Name.Contains("Replace_")

            ' Extract info from CheckBox name.
            If tf Then
                ' CheckBox naming convention "CheckBox1Find_PT", "CheckBox3Replace_RX", ...

                s = CheckBox.Name.Replace("CheckBox", "")  ' "CheckBox1Find_PT" -> "1Find_PT"
                RowIndexString = s(0)

                s = s.Replace(RowIndexString, "")  ' "1Find_PT" -> "Find_PT"
                FindOrReplace = s.Split("_"c)(0)  ' "Find_PT" -> "Find"
                SearchType = s.Split("_"c)(1)  ' "Find_PT" -> "PT"

            End If

            If tf And CheckBox.Checked Then
                If FindOrReplace = "Find" Then
                    If SearchType = "PT" Then
                        CheckBoxDict(CheckBox.Name.Replace("PT", "WC")).Checked = False
                        CheckBoxDict(CheckBox.Name.Replace("PT", "RX")).Checked = False
                    End If

                    If SearchType = "WC" Then
                        CheckBoxDict(CheckBox.Name.Replace("WC", "PT")).Checked = False
                        CheckBoxDict(CheckBox.Name.Replace("WC", "RX")).Checked = False
                    End If

                    If SearchType = "RX" Then
                        CheckBoxDict(CheckBox.Name.Replace("RX", "PT")).Checked = False
                        CheckBoxDict(CheckBox.Name.Replace("RX", "WC")).Checked = False
                    End If
                End If

                If FindOrReplace = "Replace" Then
                    If SearchType = "PT" Then
                        CheckBoxDict(CheckBox.Name.Replace("PT", "RX")).Checked = False
                    End If

                    If SearchType = "RX" Then
                        CheckBoxDict(CheckBox.Name.Replace("RX", "PT")).Checked = False
                    End If
                End If
            End If

            If tf And (Not CheckBox.Checked) Then
                If FindOrReplace = "Find" Then
                    tf = CheckBoxDict(String.Format("CheckBox{0}Find_PT", RowIndexString)).Checked
                    tf = tf Or CheckBoxDict(String.Format("CheckBox{0}Find_WC", RowIndexString)).Checked
                    tf = tf Or CheckBoxDict(String.Format("CheckBox{0}Find_RX", RowIndexString)).Checked
                    If Not tf Then
                        CheckBoxDict(String.Format("CheckBox{0}Find_PT", RowIndexString)).Checked = True
                    End If
                End If

                If FindOrReplace = "Replace" Then
                    tf = CheckBoxDict(String.Format("CheckBox{0}Replace_PT", RowIndexString)).Checked
                    tf = tf Or CheckBoxDict(String.Format("CheckBox{0}Replace_RX", RowIndexString)).Checked
                    If Not tf Then
                        CheckBoxDict(String.Format("CheckBox{0}Replace_PT", RowIndexString)).Checked = True
                    End If
                End If
            End If

        End If

    End Sub

    Private Sub ReconcileFormControls()
        Dim CheckBoxDict As New Dictionary(Of String, CheckBox)
        Dim RowIndex As Integer
        Dim tf As Boolean
        Dim SelectedRowIndices As New List(Of Integer)

        CheckBoxDict = InputEditorDoctor.GetCheckBoxes(TableLayoutPanel1)

        For RowIndex = 1 To TableLayoutPanel1.RowCount - 1
            tf = CheckBoxDict(String.Format("CheckBox{0}Find_PT", RowIndex)).Checked
            tf = tf Or CheckBoxDict(String.Format("CheckBox{0}Find_WC", RowIndex)).Checked
            tf = tf Or CheckBoxDict(String.Format("CheckBox{0}Find_RX", RowIndex)).Checked
            If Not tf Then
                CheckBoxDict(String.Format("CheckBox{0}Find_PT", RowIndex)).Checked = True
            End If

            tf = CheckBoxDict(String.Format("CheckBox{0}Replace_PT", RowIndex)).Checked
            tf = tf Or CheckBoxDict(String.Format("CheckBox{0}Replace_RX", RowIndex)).Checked
            If Not tf Then
                CheckBoxDict(String.Format("CheckBox{0}Replace_PT", RowIndex)).Checked = True
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

    Private Sub TableLayoutPanel1_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) Handles TableLayoutPanel1.CellPaint
        ' https://stackoverflow.com/questions/34064499/how-to-set-cell-color-in-tablelayoutpanel-dynamically
        If e.Row = 0 Then
            e.Graphics.FillRectangle(InputEditorDoctor.BrushColor, e.CellBounds)
        End If
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
    End Sub

    Private Sub ButtonMoveSelectedDown_Click(sender As Object, e As EventArgs) Handles ButtonMoveSelectedDown.Click
        ProcessCheckBoxEvents = False
        InputEditorDoctor.MoveSelected(TableLayoutPanel1, ColumnsDict, "Down")
        ProcessCheckBoxEvents = True

        ReconcileFormControls()
    End Sub

    Private Sub CheckBoxSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSelectAll.CheckedChanged
        ProcessCheckBoxEvents = False
        InputEditorDoctor.ProcessCheckBoxSelectAll(TableLayoutPanel1, CheckBoxSelectAll)
        ProcessCheckBoxEvents = True

        ReconcileFormControls()
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Dim TableValuesDict As New Dictionary(Of String, Dictionary(Of String, String))

        TableValuesDict = InputEditorDoctor.GetTableValues(TableLayoutPanel1, ColumnsDict, "PropertyName")
        TextBoxJSON.Text = JsonConvert.SerializeObject(TableValuesDict)

        Me.DialogResult = DialogResult.OK
    End Sub


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub
End Class