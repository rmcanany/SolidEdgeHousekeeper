Option Explicit On

Imports System.Data.Common
Imports System.Globalization
Imports System.Net.Http
Imports Newtonsoft.Json
'Imports Newtonsoft.Json
Imports SolidEdgeConstants
'Imports System.Runtime.Serialization.Json


Public Class FormVariableInputEditor
    'Private boxes(5) As TextBox
    Private ColumnNames As New List(Of String)
    Dim ColumnControlsList As New List(Of String)
    Dim PopulatingControls As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub PopulateControls()
        ' https://stackoverflow.com/questions/11312239/how-to-programmatically-add-controls-to-a-form-in-vb-net
        Dim CheckBox As CheckBox
        Dim TextBox As TextBox
        Dim ComboBox As ComboBox

        PopulatingControls = True

        ColumnNames.Add("Select")
        ColumnNames.Add("VariableName")
        ColumnNames.Add("Formula")
        ColumnNames.Add("Units")
        ColumnNames.Add("Expose")
        ColumnNames.Add("ExposeName")

        ColumnControlsList.Add("CheckBox")
        ColumnControlsList.Add("TextBox")
        ColumnControlsList.Add("TextBox")
        ColumnControlsList.Add("ComboBox")
        ColumnControlsList.Add("CheckBox")
        ColumnControlsList.Add("TextBox")

        For RowIndex = 1 To TableLayoutPanel1.RowCount - 1
            For ColumnIndex = 0 To TableLayoutPanel1.ColumnCount - 1
                If ColumnControlsList(ColumnIndex) = "CheckBox" Then
                    CheckBox = New CheckBox
                    CheckBox.Name = String.Format("CheckBox{0}{1}", RowIndex, ColumnNames(ColumnIndex))
                    CheckBox.Text = ""
                    CheckBox.Size = New Size(15, 15)
                    CheckBox.Anchor = AnchorStyles.None
                    If ColumnNames(ColumnIndex) = "Select" Then
                        CheckBox.TabStop = False
                    End If
                    AddHandler CheckBox.CheckedChanged, AddressOf CheckBox_CheckedChanged
                    TableLayoutPanel1.Controls.Add(CheckBox, ColumnIndex, RowIndex)
                End If

                If ColumnControlsList(ColumnIndex) = "TextBox" Then
                    TextBox = New TextBox
                    TextBox.Name = String.Format("TextBox{0}{1}", RowIndex, ColumnNames(ColumnIndex))

                    If ColumnNames(ColumnIndex) = "VariableName" Then
                        TextBox.Size = New Size(175, 20)
                    End If
                    If ColumnNames(ColumnIndex) = "Formula" Then
                        TextBox.Size = New Size(200, 20)
                    End If
                    If ColumnNames(ColumnIndex) = "ExposeName" Then
                        TextBox.Size = New Size(175, 20)
                    End If

                    TextBox.Anchor = AnchorStyles.Left + AnchorStyles.Right
                    AddHandler TextBox.TextChanged, AddressOf TextBox_TextChanged

                    TableLayoutPanel1.Controls.Add(TextBox, ColumnIndex, RowIndex)
                End If

                If ColumnControlsList(ColumnIndex) = "ComboBox" Then
                    ComboBox = New ComboBox
                    ComboBox.Name = String.Format("ComboBox{0}{1}", RowIndex, ColumnNames(ColumnIndex))
                    ComboBox.Anchor = AnchorStyles.None
                    ComboBox.Size = New Size(100, 20)
                    ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                    ComboBox.TabStop = False
                    For Each UnitTypeConstant As SolidEdgeConstants.UnitTypeConstants In System.Enum.GetValues(GetType(SolidEdgeConstants.UnitTypeConstants))
                        ComboBox.Items.Add(UnitTypeConstant.ToString.Replace("igUnit", ""))
                    Next
                    ComboBox.Text = ComboBox.Items(0)

                    TableLayoutPanel1.Controls.Add(ComboBox, ColumnIndex, RowIndex)
                End If

            Next
        Next

        PopulatingControls = False

    End Sub
    Private Sub SaveVariables()
        Dim TableValuesDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim VariableNameControl As String
        Dim VariableName As String
        Dim ControlsDict As New Dictionary(Of String, Control)
        Dim ColumnName As String
        Dim ColumnControlType As String
        Dim s As String
        Dim ControlName

        Dim TextBox As New TextBox
        Dim ComboBox As New ComboBox
        Dim CheckBox As New CheckBox

        ControlsDict = GetControlsDict()

        For RowIndex = 1 To TableLayoutPanel1.RowCount - 1

            VariableNameControl = String.Format("TextBox{0}VariableName", RowIndex)
            TextBox = ControlsDict(VariableNameControl)
            VariableName = TextBox.Text
            If Not VariableName.Trim = "" Then
                TableValuesDict(VariableName) = New Dictionary(Of String, String)

                For Each s In ColumnNames.Zip(Of String, String)(ColumnControlsList, Function(x, y) x + ":" + y)
                    ' https://stackoverflow.com/questions/43906647/using-zip-in-vb-net-to-send-items-in-pairs-from-two-lists-to-a-console-writeline
                    ColumnName = s.Split(":")(0)
                    ColumnControlType = s.Split(":")(1)

                    If (ColumnName = "Select") Or (ColumnName = "VariableName") Then Continue For

                    If ColumnControlType = "TextBox" Then
                        ControlName = String.Format("TextBox{0}{1}", RowIndex, ColumnName)
                        TextBox = ControlsDict(ControlName)
                        TableValuesDict(VariableName)(ColumnName) = TextBox.Text
                    End If

                    If ColumnControlType = "ComboBox" Then
                        ControlName = String.Format("ComboBox{0}{1}", RowIndex, ColumnName)
                        ComboBox = ControlsDict(ControlName)
                        TableValuesDict(VariableName)(ColumnName) = ComboBox.Text
                    End If

                    If ColumnControlType = "CheckBox" Then
                        ControlName = String.Format("CheckBox{0}{1}", RowIndex, ColumnName)
                        CheckBox = ControlsDict(ControlName)
                        TableValuesDict(VariableName)(ColumnName) = CheckBox.Checked.ToString
                    End If
                Next
            End If
        Next

        TextBoxResult.Text = JsonConvert.SerializeObject(TableValuesDict)


    End Sub
    Private Sub RestoreVariables()

        Dim TableValuesDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim VariableNameControl As String
        Dim VariableName As String
        Dim ControlsDict As New Dictionary(Of String, Control)
        Dim ColumnName As String
        Dim ColumnControlType As String
        Dim s As String
        Dim ControlName
        Dim Proceed As Boolean = True
        Dim tf As Boolean

        Dim TextBox As New TextBox
        Dim ComboBox As New ComboBox
        Dim CheckBox As New CheckBox

        ControlsDict = GetControlsDict()

        TextBoxResult.Text = Form1.TextBoxVariableEditPart.Text

        If Not TextBoxResult.Text = "" Then
            ' Possible to have bogus string in Form1.TextBox1
            Try
                TableValuesDict = JsonConvert.DeserializeObject(
                Of Dictionary(Of String, Dictionary(Of String, String)))(TextBoxResult.Text)
            Catch ex As Exception
                Proceed = False
            End Try
        Else
            Proceed = False
        End If

        If Proceed Then

            For RowIndex = 1 To TableLayoutPanel1.RowCount - 1

                VariableNameControl = String.Format("TextBox{0}VariableName", RowIndex)
                TextBox = ControlsDict(VariableNameControl)

                ' VariableName = TextBox.Text
                VariableName = TableValuesDict.Keys(RowIndex - 1)

                tf = Not VariableName Is Nothing
                tf = tf And Not VariableName = ""

                If tf Then
                    TextBox.Text = VariableName

                    For Each s In ColumnNames.Zip(Of String, String)(ColumnControlsList, Function(x, y) x + ":" + y)
                        ' https://stackoverflow.com/questions/43906647/using-zip-in-vb-net-to-send-items-in-pairs-from-two-lists-to-a-console-writeline
                        ColumnName = s.Split(":")(0)
                        ColumnControlType = s.Split(":")(1)

                        If (ColumnName = "Select") Or (ColumnName = "VariableName") Then Continue For

                        If ColumnControlType = "TextBox" Then
                            ControlName = String.Format("TextBox{0}{1}", RowIndex, ColumnName)
                            TextBox = ControlsDict(ControlName)
                            TextBox.Text = TableValuesDict(VariableName)(ColumnName)
                        End If

                        If ColumnControlType = "ComboBox" Then
                            ControlName = String.Format("ComboBox{0}{1}", RowIndex, ColumnName)
                            ComboBox = ControlsDict(ControlName)
                            ComboBox.Text = TableValuesDict(VariableName)(ColumnName)
                        End If

                        If ColumnControlType = "CheckBox" Then
                            ControlName = String.Format("CheckBox{0}{1}", RowIndex, ColumnName)
                            CheckBox = ControlsDict(ControlName)
                            If TableValuesDict(VariableName)(ColumnName).ToLower = "true" Then
                                CheckBox.Checked = True
                            Else
                                CheckBox.Checked = False
                            End If
                        End If
                    Next
                End If
            Next

        End If
        ' TextBoxResult.Text = JsonConvert.SerializeObject(TableValuesDict)


    End Sub
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' https://www.codeproject.com/Tips/842418/Designing-the-Layout-of-Windows-Forms-using-a
        ' https://www.vbforums.com/showthread.php?891013-How-to-iterate-rows-and-columns-in-TableLayoutPanel

        ButtonMoveSelectedUp.Text = ChrW(8593)
        ButtonMoveSelectedDown.Text = ChrW(8595)

        PopulateControls()
        RestoreVariables()


    End Sub

    'Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
    '	'e.Graphics.DrawLine(Pens.Black, New Point(32, 70), New Point(32, 30))
    '	'e.Graphics.DrawLine(Pens.Black, New Point(32, 30), New Point(50, 30))
    '	'e.Graphics.DrawLine(Pens.Black, New Point(450, 50), New Point(500, 50))
    'End Sub


    Public Sub ShowInputEditor(Filter As String)
        If Filter = "asm" Then CheckBoxCopyToAsm.Enabled = False
        If Filter = "par" Then CheckBoxCopyToPar.Enabled = False
        If Filter = "psm" Then CheckBoxCopyToPsm.Enabled = False
        If Filter = "dft" Then CheckBoxCopyToDraft.Enabled = False

        Me.ShowDialog()

    End Sub

    Private Function GetCheckBoxes() As List(Of CheckBox)
        Dim CheckBoxList As New List(Of CheckBox)
        Dim Control As Control
        'Dim CheckBox As CheckBox

        For Each Control In TableLayoutPanel1.Controls
            If Control.GetType() Is GetType(CheckBox) Then
                CheckBoxList.Add(DirectCast(Control, CheckBox))
            End If
        Next

        Return CheckBoxList
    End Function
    Private Sub TextBox_TextChanged(sender As System.Object, e As System.EventArgs)
        'When you modify the contents of any textbox, the name of that textbox
        'and its current contents will be displayed in the title bar

        'Dim box As TextBox = DirectCast(sender, TextBox)
        'Me.Text = box.Name & ": " & box.Text
    End Sub

    Private Function GetSelectedRowIndices() As List(Of Integer)
        Dim SelectedRowIndices As New List(Of Integer)
        Dim CheckBoxList As New List(Of CheckBox)
        Dim CheckBox As CheckBox
        Dim s As String

        CheckBoxList = GetCheckBoxes()

        For Each CheckBox In CheckBoxList
            If CheckBox.Name.Contains("Select") Then
                If Not CheckBox.Name.Contains("SelectAll") Then
                    If CheckBox.Checked Then
                        ' CheckBox.Name = String.Format("CheckBox{0}{1}", RowIndex, ColumnNames(ColumnIndex))
                        s = CheckBox.Name.Replace("CheckBox", "")(0)
                        SelectedRowIndices.Add(CInt(s))
                    End If
                End If
            End If
        Next

        Return SelectedRowIndices
    End Function

    Private Sub CheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs)
        If Not PopulatingControls Then
            Dim CheckBox As CheckBox = DirectCast(sender, CheckBox)
            Dim SelectedRowIndices As New List(Of Integer)

            If CheckBox.Name.Contains("Select") Then
                SelectedRowIndices = GetSelectedRowIndices()
                If SelectedRowIndices.Count = 1 Then
                    ButtonMoveSelectedDown.Enabled = True
                    ButtonMoveSelectedUp.Enabled = True
                Else
                    ButtonMoveSelectedDown.Enabled = False
                    ButtonMoveSelectedUp.Enabled = False
                End If

                If SelectedRowIndices.Count > 0 Then
                    ButtonClearSelected.Enabled = True
                Else
                    ButtonClearSelected.Enabled = False
                End If
            End If

            'Me.Text = String.Format("{0}: {1}", CheckBox.Name, CheckBox.Checked.ToString) 'CheckBox.Name
        End If
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        ' https://stackoverflow.com/questions/20551203/how-to-get-values-from-a-dialog-form-in-vb-net

        SaveVariables()

        Me.DialogResult = DialogResult.OK

    End Sub

    Private Function GetControlsDict() As Dictionary(Of String, Control)
        Dim ControlsDict As New Dictionary(Of String, Control)

        For Each Control As Control In TableLayoutPanel1.Controls
            ControlsDict.Add(Control.Name, Control)
        Next

        Return ControlsDict
    End Function

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub CheckBoxSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSelectAll.CheckedChanged
        Dim CheckBoxList As New List(Of CheckBox)
        Dim CheckBox As CheckBox
        Dim tf As Boolean

        CheckBoxList = GetCheckBoxes()

        For Each CheckBox In CheckBoxList
            If Not CheckBox.Name.Contains("SelectAll") Then
                tf = CheckBoxSelectAll.Checked
                tf = tf And CheckBox.Name.Contains("Select")
                If tf Then
                    CheckBox.Checked = True
                Else
                    CheckBox.Checked = False
                End If
            End If
        Next

        If CheckBoxSelectAll.Checked Then
            ButtonClearSelected.Enabled = True
        Else
            ButtonClearSelected.Enabled = False
        End If
        ButtonMoveSelectedDown.Enabled = False
        ButtonMoveSelectedUp.Enabled = False

    End Sub

    Private Sub ButtonClearSelected_Click(sender As Object, e As EventArgs) Handles ButtonClearSelected.Click

        ' https://stackoverflow.com/questions/62449364/how-to-access-each-field-in-a-winform-tablelayoutpanel-in-c-sharp
        ' https://stackoverflow.com/questions/199521/vb-net-iterating-through-controls-in-a-container-object
        Dim Control As Control
        Dim tf As Boolean

        For RowIndex As Integer = 1 To TableLayoutPanel1.RowCount - 1
            For ColumnIndex As Integer = 0 To TableLayoutPanel1.ColumnCount - 1
                Control = TableLayoutPanel1.GetControlFromPosition(ColumnIndex, RowIndex)
                tf = ColumnIndex = 0
                tf = tf And (Control.GetType() Is GetType(CheckBox))
                If tf Then
                    If Not DirectCast(Control, CheckBox).Checked Then
                        Exit For
                    End If
                Else
                    If Control.GetType Is GetType(TextBox) Then
                        DirectCast(Control, TextBox).Text = ""
                    ElseIf Control.GetType Is GetType(ComboBox) Then
                        DirectCast(Control, ComboBox).Text = "Distance"
                    ElseIf Control.GetType Is GetType(CheckBox) Then
                        DirectCast(Control, CheckBox).Checked = False
                    End If
                End If
            Next
        Next

        RemoveBlankLines()

    End Sub

    Private Sub RemoveBlankLines()
        Dim BlankRowList As New List(Of Integer)
        Dim PopulatedRowList As New List(Of Integer)
        Dim SourceCheckBox As CheckBox
        Dim TargetCheckBox As CheckBox
        Dim TextBox As TextBox
        Dim SourceTextBox As TextBox
        Dim TargetTextBox As TextBox
        Dim SourceComboBox As ComboBox
        Dim TargetComboBox As ComboBox
        Dim VariableColumnIndex As Integer = 1
        Dim RowIndex As Integer
        Dim FirstBlankRowIndex As Integer
        'Dim TargetBlankRowIndex As Integer
        Dim i As Integer
        Dim SourceControl As Control
        Dim TargetControl As Control

        ' Find which rows are blank and which are populated.
        For RowIndex = 1 To TableLayoutPanel1.RowCount - 1
            TextBox = TableLayoutPanel1.GetControlFromPosition(VariableColumnIndex, RowIndex)
            Dim s As String = TextBox.Text
            If TextBox.Text.Trim = "" Then
                BlankRowList.Add(RowIndex)
            Else
                PopulatedRowList.Add(RowIndex)
            End If
        Next

        ' Populated rows above the first blank row do not need to be processed.
        If (BlankRowList.Count > 0) And (PopulatedRowList.Count > 0) Then
            FirstBlankRowIndex = BlankRowList(0)
            i = PopulatedRowList.Count - 1
            For i = PopulatedRowList.Count - 1 To 0 Step -1
                If PopulatedRowList(i) < FirstBlankRowIndex Then
                    PopulatedRowList.RemoveAt(i)
                End If
            Next
        End If

        ' Move the first populated row to the first blank row, the second to the second, etc.
        For i = 0 To PopulatedRowList.Count - 1
            For ColumnIndex As Integer = 0 To TableLayoutPanel1.ColumnCount - 1
                SourceControl = TableLayoutPanel1.GetControlFromPosition(ColumnIndex, PopulatedRowList(i))
                TargetControl = TableLayoutPanel1.GetControlFromPosition(ColumnIndex, BlankRowList(i))
                If SourceControl.GetType Is GetType(TextBox) Then
                    SourceTextBox = DirectCast(SourceControl, TextBox)
                    TargetTextBox = DirectCast(TargetControl, TextBox)
                    TargetTextBox.Text = SourceTextBox.Text
                    SourceTextBox.Text = ""
                ElseIf SourceControl.GetType Is GetType(ComboBox) Then
                    SourceComboBox = DirectCast(SourceControl, ComboBox)
                    TargetComboBox = DirectCast(TargetControl, ComboBox)
                    TargetComboBox.Text = SourceComboBox.Text
                    SourceComboBox.Text = "Distance"
                ElseIf SourceControl.GetType Is GetType(CheckBox) Then
                    SourceCheckBox = DirectCast(SourceControl, CheckBox)
                    TargetCheckBox = DirectCast(TargetControl, CheckBox)
                    TargetCheckBox.Checked = SourceCheckBox.Checked
                    SourceCheckBox.Checked = False
                End If
            Next
            BlankRowList.Add(PopulatedRowList(i))
            BlankRowList.Sort()
        Next

        ClearSelectionCheckmarks()

    End Sub

    Private Sub ClearSelectionCheckmarks()
        Dim CheckBoxList As New List(Of CheckBox)
        Dim CheckBox As CheckBox

        CheckBoxList = GetCheckBoxes()

        For Each CheckBox In CheckBoxList
            If CheckBox.Name.Contains("Select") Then
                CheckBox.Checked = False
            End If
        Next

        ButtonClearSelected.Enabled = False
        ButtonMoveSelectedDown.Enabled = False
        ButtonMoveSelectedUp.Enabled = False
    End Sub

    Private Sub MoveSelected(Direction As String)
        Dim TargetTextBox As TextBox
        Dim TargetCheckBox As CheckBox
        Dim TargetComboBox As ComboBox
        Dim SourceTextBox As TextBox
        Dim SourceCheckBox As CheckBox
        Dim SourceComboBox As ComboBox

        Dim ColumnName As String
        Dim ColumnIndexToNameDict As New Dictionary(Of Integer, String)
        Dim Index As Integer
        Dim tf As Boolean = False
        'Dim Control As Control

        Index = 0
        For Each ColumnName In ColumnNames
            ColumnIndexToNameDict(Index) = ColumnName
            Index += 1
        Next

        Dim OldTargetRow As New Collection

        OldTargetRow.Add(New CheckBox, "Select")
        OldTargetRow.Add(New TextBox, "VariableName")
        OldTargetRow.Add(New TextBox, "Formula")
        OldTargetRow.Add(New ComboBox, "Units")
        OldTargetRow.Add(New CheckBox, "Expose")
        OldTargetRow.Add(New TextBox, "ExposeName")

        Dim SelectedRowIndices As New List(Of Integer)
        Dim SourceRowIndex As Integer
        Dim TargetRowIndex As Integer

        SelectedRowIndices = GetSelectedRowIndices()

        ' Need exactly one row selected
        If SelectedRowIndices.Count = 1 Then

            SourceRowIndex = SelectedRowIndices(0)

            ' Can't move up from the first row, or down from the last row.
            If Direction.ToLower = "up" Then
                tf = SourceRowIndex > 1
                TargetRowIndex = SourceRowIndex - 1
            Else
                tf = SourceRowIndex < TableLayoutPanel1.RowCount - 1
                TargetRowIndex = SourceRowIndex + 1
            End If

            If tf Then

                For ColumnIndex As Integer = 0 To TableLayoutPanel1.ColumnCount - 1
                    ' Checkboxes
                    tf = ColumnIndexToNameDict(ColumnIndex) = "Select"
                    tf = tf Or ColumnIndexToNameDict(ColumnIndex) = "Expose"
                    If tf Then
                        TargetCheckBox = New CheckBox
                        TargetCheckBox = TableLayoutPanel1.GetControlFromPosition(ColumnIndex, TargetRowIndex)
                        OldTargetRow(ColumnIndexToNameDict(ColumnIndex)).checked = TargetCheckBox.Checked

                        SourceCheckBox = New CheckBox
                        SourceCheckBox = TableLayoutPanel1.GetControlFromPosition(ColumnIndex, SourceRowIndex)
                        TargetCheckBox.Checked = SourceCheckBox.Checked
                        SourceCheckBox.Checked = OldTargetRow(ColumnIndexToNameDict(ColumnIndex)).checked
                    End If

                    ' Textboxes
                    tf = ColumnIndexToNameDict(ColumnIndex) = "VariableName"
                    tf = tf Or ColumnIndexToNameDict(ColumnIndex) = "Formula"
                    tf = tf Or ColumnIndexToNameDict(ColumnIndex) = "ExposeName"
                    If tf Then
                        TargetTextBox = New TextBox
                        TargetTextBox = TableLayoutPanel1.GetControlFromPosition(ColumnIndex, TargetRowIndex)
                        OldTargetRow(ColumnIndexToNameDict(ColumnIndex)).Text = TargetTextBox.Text

                        SourceTextBox = New TextBox
                        SourceTextBox = TableLayoutPanel1.GetControlFromPosition(ColumnIndex, SourceRowIndex)
                        TargetTextBox.Text = SourceTextBox.Text
                        SourceTextBox.Text = OldTargetRow(ColumnIndexToNameDict(ColumnIndex)).Text
                    End If

                    ' Comboboxes
                    tf = ColumnIndexToNameDict(ColumnIndex) = "Units"
                    If tf Then
                        TargetComboBox = New ComboBox
                        TargetComboBox = TableLayoutPanel1.GetControlFromPosition(ColumnIndex, TargetRowIndex)
                        OldTargetRow(ColumnIndexToNameDict(ColumnIndex)).Text = TargetComboBox.Text

                        SourceComboBox = New ComboBox
                        SourceComboBox = TableLayoutPanel1.GetControlFromPosition(ColumnIndex, SourceRowIndex)
                        TargetComboBox.Text = SourceComboBox.Text
                        SourceComboBox.Text = OldTargetRow(ColumnIndexToNameDict(ColumnIndex)).Text
                    End If

                Next
            End If
        End If

    End Sub


    Private Sub ButtonMoveSelectedUp_Click(sender As Object, e As EventArgs) Handles ButtonMoveSelectedUp.Click
        MoveSelected("Up")
    End Sub

    Private Sub ButtonMoveSelectedDown_Click(sender As Object, e As EventArgs) Handles ButtonMoveSelectedDown.Click
        MoveSelected("Down")
    End Sub
End Class