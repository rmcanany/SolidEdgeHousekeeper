Option Strict On
Imports System.Runtime.InteropServices
Imports System.Security.AccessControl
Imports Newtonsoft.Json

Public Class FormPropertyInputEditor
    Private ColumnsDict As New Dictionary(Of Integer, Dictionary(Of String, String))
    Private InputEditorDoctor As New InputEditorDoctor
    Private ProcessCheckBoxEvents As Boolean
    Private FileType As String
    Public Property JSONDict As String
    'Public Property UseNewTaskTab As Boolean

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
    Dim t As Timer = New Timer()

    'Public Sub ShowInputEditor(FileType As String)
    '    Me.FileType = FileType

    '    If FileType = "asm" Then
    '        CheckBoxCopyToAsm.Checked = True
    '        CheckBoxCopyToAsm.Enabled = False
    '    End If
    '    If FileType = "par" Then
    '        CheckBoxCopyToPar.Checked = True
    '        CheckBoxCopyToPar.Enabled = False
    '    End If
    '    If FileType = "psm" Then
    '        CheckBoxCopyToPsm.Checked = True
    '        CheckBoxCopyToPsm.Enabled = False
    '    End If
    '    If FileType = "dft" Then
    '        CheckBoxCopyToDft.Checked = True
    '        CheckBoxCopyToDft.Enabled = False
    '    End If

    '    Me.ShowDialog()

    'End Sub

    Private Sub FormPropertyInputEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        t.Interval = 1500
        AddHandler t.Tick, AddressOf HandleTimerTick

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
            If ComboBoxName.EndsWith("PropertySet") Then
                ComboBoxDict(ComboBoxName).Items.Add("System")
                'ComboBoxDict(ComboBoxName).Items.Add("Project")
                ComboBoxDict(ComboBoxName).Items.Add("Custom")
                ComboBoxDict(ComboBoxName).Text = ComboBoxDict(ComboBoxName).Items(0).ToString
                AddHandler ComboBoxDict(ComboBoxName).SelectedValueChanged, AddressOf ComboBox_TextChanged
            End If
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
        ColumnName = "PropertySet"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "ComboBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(False)
        ColumnsDict(ColumnIndex)("DefaultValue") = ""
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "PropertyName"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "ComboBox" '"TextBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(False)
        ColumnsDict(ColumnIndex)("DefaultValue") = ""
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "Find_PT"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(True)
        ColumnsDict(ColumnIndex)("DefaultValue") = CStr(True)
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "Find_WC"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(True)
        ColumnsDict(ColumnIndex)("DefaultValue") = CStr(False)
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "Find_RX"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(True)
        ColumnsDict(ColumnIndex)("DefaultValue") = CStr(False)
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "FindString"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "TextBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(True)
        ColumnsDict(ColumnIndex)("DefaultValue") = ""
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "Replace_PT"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(True)
        ColumnsDict(ColumnIndex)("DefaultValue") = CStr(True)
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "Replace_RX"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(True)
        ColumnsDict(ColumnIndex)("DefaultValue") = CStr(False)
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "Replace_EX"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(True)
        ColumnsDict(ColumnIndex)("DefaultValue") = CStr(False)
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

        ColumnIndex += 1
        ColumnName = "ReplaceString"
        ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
        ColumnsDict(ColumnIndex)("ColumnControl") = "TextBox"
        ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
        ColumnsDict(ColumnIndex)("AllowBlank") = CStr(True)
        ColumnsDict(ColumnIndex)("DefaultValue") = ""
        ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(False)

    End Sub


    Private Sub ComboBox_TextChanged(sender As System.Object, e As System.EventArgs)

        If ProcessCheckBoxEvents Then

            Dim ComboBox As ComboBox = DirectCast(sender, ComboBox)

            SetCombo(ComboBox, False)

            'Dim ComboBoxDict As New Dictionary(Of String, ComboBox)
            'ComboBoxDict = InputEditorDoctor.GetComboBoxes(TableLayoutPanel1)
            'Dim tmpComboBox As ComboBox = ComboBoxDict(ComboBox.Name.Replace("PropertySet", "PropertyName"))

            'If ComboBox.Text = "System" Then
            '    tmpComboBox.DropDownStyle = ComboBoxStyle.DropDownList
            '    tmpComboBox.Items.Clear()
            '    tmpComboBox.Items.AddRange({"Title", "Subject", "Author", "Keywords", "Comments"}) ', "Category", "Company", "Manager"}) <-- are in different stream, TBD
            '    tmpComboBox.SelectedItem = tmpComboBox.Items(0)

            'ElseIf ComboBox.Text = "Project" Then
            '    tmpComboBox.DropDownStyle = ComboBoxStyle.DropDownList
            '    tmpComboBox.Items.Clear()
            '    tmpComboBox.Items.AddRange({"Document Number", "Revision", "Project Name"})
            '    tmpComboBox.SelectedItem = tmpComboBox.Items(0)

            'ElseIf ComboBox.Text = "Custom" Then
            '    tmpComboBox.Items.Clear()
            '    tmpComboBox.DropDownStyle = ComboBoxStyle.Simple
            '    tmpComboBox.Text = ""

            'End If

        End If

    End Sub

    Public Sub SetCombo(ComboBox As ComboBox, keepvalue As Boolean)

        Dim ComboBoxDict As New Dictionary(Of String, ComboBox)
        ComboBoxDict = InputEditorDoctor.GetComboBoxes(TableLayoutPanel1)
        Dim tmpComboBox As ComboBox = ComboBoxDict(ComboBox.Name.Replace("PropertySet", "PropertyName"))
        Dim tmpValue As String = tmpComboBox.Text

        If ComboBox.Text = "System" Then

            tmpComboBox.DropDownStyle = ComboBoxStyle.DropDownList
            tmpComboBox.Items.Clear()
            tmpComboBox.Items.AddRange({"", "Title", "Subject", "Author", "Keywords", "Comments", "Category", "Company", "Manager",
                                       "Document Number", "Revision", "Project Name", "Material", "Sheet Metal Gage"})
            If Not keepvalue Then
                tmpComboBox.SelectedItem = Nothing
            Else
                tmpComboBox.SelectedIndex = tmpComboBox.Items.IndexOf(tmpValue)
            End If

            'ElseIf ComboBox.Text = "Material" Then

            '    tmpComboBox.DropDownStyle = ComboBoxStyle.DropDownList
            '    tmpComboBox.Items.Clear()
            '    tmpComboBox.Items.AddRange({"Material", "Coef. ofThermal Exp", "Thermal Conductivity", "Specific Heat", "Modulus of Elasticity"}......) ', "Category", "Company", "Manager"}) <-- are in different stream, TBD
            '    If Not keepvalue Then
            '        tmpComboBox.SelectedItem = tmpComboBox.Items(0)
            '    Else
            '        tmpComboBox.SelectedIndex = tmpComboBox.Items.IndexOf(tmpValue)
            '    End If

            'ElseIf ComboBox.Text = "Project" Then
            '    tmpComboBox.DropDownStyle = ComboBoxStyle.DropDownList
            '    tmpComboBox.Items.Clear()
            '    tmpComboBox.Items.AddRange({"Document Number", "Revision", "Project Name"})
            '    If Not keepvalue Then
            '        tmpComboBox.SelectedItem = Nothing
            '    Else
            '        tmpComboBox.SelectedIndex = tmpComboBox.Items.IndexOf(tmpValue)
            '    End If

        ElseIf ComboBox.Text = "Custom" Then
            tmpComboBox.Items.Clear()
            tmpComboBox.DropDownStyle = ComboBoxStyle.Simple
            If Not keepvalue Then
                tmpComboBox.Text = ""
            Else
                tmpComboBox.Text = tmpValue
            End If

        End If

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
                        CheckBoxDict(CheckBox.Name.Replace("PT", "EX")).Checked = False
                    End If

                    If SearchType = "RX" Then
                        CheckBoxDict(CheckBox.Name.Replace("RX", "PT")).Checked = False
                        CheckBoxDict(CheckBox.Name.Replace("RX", "EX")).Checked = False
                    End If

                    If SearchType = "EX" Then
                        CheckBoxDict(CheckBox.Name.Replace("EX", "PT")).Checked = False
                        CheckBoxDict(CheckBox.Name.Replace("EX", "RX")).Checked = False
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
                    tf = tf Or CheckBoxDict(String.Format("CheckBox{0}Replace_EX", RowIndexString)).Checked
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
            tf = tf Or CheckBoxDict(String.Format("CheckBox{0}Replace_EX", RowIndex)).Checked
            If Not tf Then
                CheckBoxDict(String.Format("CheckBox{0}Replace_PT", RowIndex)).Checked = True
            End If

        Next

        Dim ComboBoxDict As New Dictionary(Of String, ComboBox)
        Dim ComboBoxName As String
        ComboBoxDict = InputEditorDoctor.GetComboBoxes(TableLayoutPanel1)
        For Each ComboBoxName In ComboBoxDict.Keys
            If ComboBoxName.EndsWith("PropertySet") Then

                SetCombo(ComboBoxDict(ComboBoxName), True)

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
        Dim TableValuesDict As New Dictionary(Of Integer, Dictionary(Of String, String))

        TableValuesDict = InputEditorDoctor.GetTableValues(TableLayoutPanel1, ColumnsDict)
        TextBoxJSON.Text = JsonConvert.SerializeObject(TableValuesDict)

        Me.DialogResult = DialogResult.OK
    End Sub


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub TextBoxJSON_TextChanged(sender As Object, e As EventArgs) Handles TextBoxJSON.TextChanged
        Me.JSONDict = TextBoxJSON.Text
    End Sub

    Private Sub ButtonNCalc_Click(sender As Object, e As EventArgs) Handles ButtonNCalc.Click

        Dim tmp As New FormNCalc
        tmp.TextEditorFormula.Language = FastColoredTextBoxNS.Language.SQL
        'tmp.TextEditorNCalc.Text = "'%{System.Title}' + '-' + toString(cast(substring('%{System.Comments}', lastIndexOf('%{System.Comments}', 'L=')+2, length('%{System.Comments}') - lastIndexOf('%{System.Comments}', ' ')),'System.Int32'),'D4') + '-' + substring('%{System.Comments}', lastIndexOf('%{System.Comments}', ' ')+1)"
        tmp.ShowDialog()
        Dim A = tmp.Formula.Replace(vbCrLf, "")
        A = A.Split(CType("\\", Char)).First

        If A <> "" Then
            Clipboard.SetText(A)
            MessageTimeOut("Expression copied in clipboard", "Expression editor", 1)
        End If

    End Sub

    Sub MessageTimeOut(sMessage As String, sTitle As String, iSeconds As Integer)

        Dim tmpForm As New Form
        Dim tmpSize = New Size(200, 75)
        tmpForm.Text = String.Empty
        tmpForm.ControlBox = False
        tmpForm.BackColor = Color.White

        'tmpForm.FormBorderStyle = FormBorderStyle.None
        tmpForm.Size = tmpSize
        tmpForm.StartPosition = FormStartPosition.Manual
        tmpForm.Location = New Point(CInt(Me.Left + Me.Width / 2 - tmpForm.Width / 2), CInt(Me.Top + Me.Height / 2 - tmpForm.Height / 2))

        Dim tmpLabel As New Label
        tmpLabel.Font = New Font(Me.Font.Name, 8, FontStyle.Bold)
        tmpLabel.Width = 180
        tmpLabel.Dock = DockStyle.Fill
        tmpLabel.TextAlign = ContentAlignment.MiddleCenter
        tmpLabel.Text = "Expression copied to clipboard"
        tmpForm.Controls.Add(tmpLabel)

        tmpForm.Show(Me)

        t.Start()

    End Sub

    Sub HandleTimerTick(sender As Object, e As EventArgs)

        For Each item In Me.OwnedForms
            item.Close()
        Next

        T.Stop()

    End Sub


End Class