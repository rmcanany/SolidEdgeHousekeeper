Option Strict On
Imports System.Security.AccessControl
Imports System.Windows.Forms.VisualStyles
Imports Newtonsoft.Json

Public Class InputEditorDoctor
    Public BrushColor As Brush = Brushes.LightSteelBlue
    Private HeaderColor As Color = Color.LightSteelBlue

    Public Function GetControlsDict(TableLayoutPanel As TableLayoutPanel) As Dictionary(Of String, Control)
        Dim ControlsDict As New Dictionary(Of String, Control)
        ' ControlsDict 
        ' { "CheckBox1Find_PT": CheckBox,
        '   "TextBox1ReplaceString": TextBox,
        '   ...
        ' }

        For Each Control As Control In TableLayoutPanel.Controls
            ControlsDict.Add(Control.Name, Control)
        Next

        Return ControlsDict
    End Function

    Public Sub PopulateControls(
        TableLayoutPanel As TableLayoutPanel,
        ColumnsDict As Dictionary(Of Integer, Dictionary(Of String, String))
        )

        Dim CheckBox As CheckBox
        Dim TextBox As TextBox
        Dim ComboBox As ComboBox
        Dim RowIndex As Integer
        Dim ColumnIndex As Integer
        Dim ColumnName As String
        Dim ControlType As String

        If Not ColumnsDict.Keys.Count = TableLayoutPanel.ColumnCount Then
            MsgBox("Not ColumnsDict.Keys.Count = TableLayoutPanel.ColumnCount", vbOKOnly)
            Exit Sub
        End If

        For RowIndex = 1 To TableLayoutPanel.RowCount - 1

            For ColumnIndex = 0 To ColumnsDict.Count - 1

                'ColumnsDict format
                'ColumnIndex = 0
                'ColumnName = "Select"
                'ColumnsDict(ColumnIndex) = New Dictionary(Of String, String)
                'ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox"
                'ColumnsDict(ColumnIndex)("ColumnName") = ColumnName
                'ColumnsDict(ColumnIndex)("AllowBlank") = CStr(True)
                'ColumnsDict(ColumnIndex)("DefaultValue") = CStr(True)
                'ColumnsDict(ColumnIndex)("PopulateWithDefault") = CStr(True)


                ColumnName = ColumnsDict(ColumnIndex)("ColumnName")
                ControlType = ColumnsDict(ColumnIndex)("ColumnControl")

                If ControlType = "CheckBox" Then
                    CheckBox = New CheckBox
                    CheckBox.Name = String.Format("CheckBox{0}{1}", RowIndex, ColumnName)
                    If CBool(ColumnsDict(ColumnIndex)("PopulateWithDefault")) Then
                        CheckBox.Checked = CBool(ColumnsDict(ColumnIndex)("DefaultValue"))
                    End If
                    'If ColumnName.Contains("_PT") Then
                    '    CheckBox.Checked = True
                    'End If
                    CheckBox.Text = ""
                    CheckBox.Size = New Size(15, 15)
                    CheckBox.Anchor = AnchorStyles.None
                    CheckBox.TabStop = False
                    'AddHandler CheckBox.CheckedChanged, AddressOf CheckBox_CheckedChanged

                    TableLayoutPanel.Controls.Add(CheckBox, ColumnIndex, RowIndex)

                ElseIf ControlType = "TextBox" Then
                    TextBox = New TextBox
                    TextBox.Name = String.Format("TextBox{0}{1}", RowIndex, ColumnName)
                    If CBool(ColumnsDict(ColumnIndex)("PopulateWithDefault")) Then
                        TextBox.Text = ColumnsDict(ColumnIndex)("DefaultValue")
                    End If
                    'TextBox.Anchor = CType(AnchorStyles.Left + AnchorStyles.Right, AnchorStyles)
                    TextBox.Dock = DockStyle.Fill
                    TextBox.Margin = New Padding(3, 1, 3, 1)
                    TextBox.Multiline = True
                    'AddHandler TextBox.TextChanged, AddressOf TextBox_TextChanged

                    TableLayoutPanel.Controls.Add(TextBox, ColumnIndex, RowIndex)

                ElseIf ControlType = "ComboBox" Then
                    ComboBox = New ComboBox
                    ComboBox.Name = String.Format("ComboBox{0}{1}", RowIndex, ColumnName)
                    'ComboBox.Anchor = CType(AnchorStyles.Left + AnchorStyles.Right, AnchorStyles)
                    ComboBox.Dock = DockStyle.Fill
                    ComboBox.Margin = New Padding(3, 1, 3, 1)
                    ComboBox.DropDownStyle = If(ComboBox.Name.EndsWith("PropertySet"), ComboBoxStyle.DropDownList, ComboBoxStyle.Simple)
                    ComboBox.TabStop = False

                    TableLayoutPanel.Controls.Add(ComboBox, ColumnIndex, RowIndex)

                Else
                    MsgBox(String.Format("ControlType '{0}' not recognized", ControlType))
                End If
            Next
        Next

    End Sub

    Public Function GetCheckBoxes(TableLayoutPanel As TableLayoutPanel) As Dictionary(Of String, CheckBox)
        Dim CheckBoxDict As New Dictionary(Of String, CheckBox)
        Dim Control As Control
        'Dim CheckBox As CheckBox

        For Each Control In TableLayoutPanel.Controls
            If Control.GetType() Is GetType(CheckBox) Then
                CheckBoxDict(Control.Name) = (DirectCast(Control, CheckBox))
            End If
        Next

        Return CheckBoxDict
    End Function

    Public Function GetTextBoxes(TableLayoutPanel As TableLayoutPanel) As Dictionary(Of String, TextBox)
        Dim TextBoxDict As New Dictionary(Of String, TextBox)
        Dim Control As Control
        'Dim TextBox As TextBox

        For Each Control In TableLayoutPanel.Controls
            If Control.GetType() Is GetType(TextBox) Then
                TextBoxDict(Control.Name) = (DirectCast(Control, TextBox))
            End If
        Next

        Return TextBoxDict
    End Function

    Public Function GetComboBoxes(TableLayoutPanel As TableLayoutPanel) As Dictionary(Of String, ComboBox)
        Dim ComboBoxDict As New Dictionary(Of String, ComboBox)
        Dim Control As Control
        'Dim CheckBox As CheckBox

        For Each Control In TableLayoutPanel.Controls
            If Control.GetType() Is GetType(ComboBox) Then
                ComboBoxDict(Control.Name) = (DirectCast(Control, ComboBox))
            End If
        Next

        Return ComboBoxDict
    End Function

    Public Sub SetHeaderRowColor(TableLayoutPanel As TableLayoutPanel, ProcessAllRows As Boolean)
        Dim ColumnIndex As Integer
        Dim RowIndex As Integer
        Dim LastRowIndex As Integer
        Dim Control As Control

        If ProcessAllRows Then
            LastRowIndex = TableLayoutPanel.RowCount - 1
        Else
            LastRowIndex = 0
        End If

        For RowIndex = 0 To LastRowIndex
            For ColumnIndex = 0 To TableLayoutPanel.ColumnCount - 1
                Control = TableLayoutPanel.GetControlFromPosition(ColumnIndex, RowIndex)
                If Not Control Is Nothing Then
                    If Not Control.GetType = GetType(Button) Then
                        Control.BackColor = Me.HeaderColor
                        If Control.GetType = GetType(TableLayoutPanel) Then
                            SetHeaderRowColor(CType(Control, TableLayoutPanel), ProcessAllRows:=True)
                        End If
                    End If
                End If
            Next
        Next

    End Sub

    Public Sub ClearSelected(TableLayoutPanel As TableLayoutPanel)
        ' https://stackoverflow.com/questions/62449364/how-to-access-each-field-in-a-winform-tablelayoutpanel-in-c-sharp
        ' https://stackoverflow.com/questions/199521/vb-net-iterating-through-controls-in-a-container-object
        Dim Control As Control
        Dim tf As Boolean

        For RowIndex As Integer = 1 To TableLayoutPanel.RowCount - 1
            For ColumnIndex As Integer = 0 To TableLayoutPanel.ColumnCount - 1

                Control = TableLayoutPanel.GetControlFromPosition(ColumnIndex, RowIndex)

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
                        DirectCast(Control, ComboBox).Text = "System"
                    ElseIf Control.GetType Is GetType(CheckBox) Then
                        DirectCast(Control, CheckBox).Checked = False
                    End If
                End If
            Next
        Next

        RemoveBlankLines(TableLayoutPanel)

    End Sub

    Private Sub RemoveBlankLines(TableLayoutPanel As TableLayoutPanel)
        Dim BlankRowList As New List(Of Integer)
        Dim PopulatedRowList As New List(Of Integer)
        Dim SourceCheckBox As CheckBox
        Dim TargetCheckBox As CheckBox
        Dim TextBox As ComboBox
        Dim SourceTextBox As TextBox
        Dim TargetTextBox As TextBox
        Dim SourceComboBox As ComboBox
        Dim TargetComboBox As ComboBox
        Dim VariableColumnIndex As Integer = 2
        Dim RowIndex As Integer
        Dim FirstBlankRowIndex As Integer
        'Dim TargetBlankRowIndex As Integer
        Dim i As Integer
        Dim SourceControl As Control
        Dim TargetControl As Control

        ' Find which rows are blank and which are populated.
        For RowIndex = 1 To TableLayoutPanel.RowCount - 1
            TextBox = CType(TableLayoutPanel.GetControlFromPosition(VariableColumnIndex, RowIndex), ComboBox)
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
            For ColumnIndex As Integer = 0 To TableLayoutPanel.ColumnCount - 1
                SourceControl = TableLayoutPanel.GetControlFromPosition(ColumnIndex, PopulatedRowList(i))
                TargetControl = TableLayoutPanel.GetControlFromPosition(ColumnIndex, BlankRowList(i))
                If SourceControl.GetType Is GetType(TextBox) Then
                    SourceTextBox = DirectCast(SourceControl, TextBox)
                    TargetTextBox = DirectCast(TargetControl, TextBox)
                    TargetTextBox.Text = SourceTextBox.Text
                    SourceTextBox.Text = ""
                ElseIf SourceControl.GetType Is GetType(ComboBox) Then
                    SourceComboBox = DirectCast(SourceControl, ComboBox)
                    TargetComboBox = DirectCast(TargetControl, ComboBox)
                    TargetComboBox.Text = SourceComboBox.Text
                    SourceComboBox.Text = SourceComboBox.Items(0).ToString
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

        ClearSelectionCheckmarks(TableLayoutPanel)

    End Sub

    Private Sub ClearSelectionCheckmarks(TableLayoutPanel As TableLayoutPanel)
        Dim CheckBoxDict As New Dictionary(Of String, CheckBox)
        'Dim CheckBox As CheckBox
        Dim CheckBoxName As String

        CheckBoxDict = GetCheckBoxes(TableLayoutPanel)

        For Each CheckBoxName In CheckBoxDict.Keys
            If CheckBoxName.Contains("Select") Then
                CheckBoxDict(CheckBoxName).Checked = False
            End If
        Next

        'ButtonClearSelected.Enabled = False
        'ButtonMoveSelectedDown.Enabled = False
        'ButtonMoveSelectedUp.Enabled = False
    End Sub

    Public Sub MoveSelected(
        TableLayoutPanel As TableLayoutPanel,
        ColumnsDict As Dictionary(Of Integer, Dictionary(Of String, String)),
        Direction As String)

        Dim TargetTextBox As TextBox
        Dim TargetCheckBox As CheckBox
        Dim TargetComboBox As ComboBox
        Dim SourceTextBox As TextBox
        Dim SourceCheckBox As CheckBox
        Dim SourceComboBox As ComboBox

        'Dim ColumnName As String
        'Dim ColumnIndexToNameDict As New Dictionary(Of Integer, String)
        Dim tf As Boolean = False
        Dim ColumnIndex As Integer

        Dim OldTargetRow As New Dictionary(Of Integer, Dictionary(Of String, String))

        For ColumnIndex = 0 To ColumnsDict.Keys.Count - 1

            If ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox" Then
                OldTargetRow(ColumnIndex) = New Dictionary(Of String, String)
                OldTargetRow(ColumnIndex)("ControlType") = "CheckBox"
                OldTargetRow(ColumnIndex)("ControlState") = ""

            ElseIf ColumnsDict(ColumnIndex)("ColumnControl") = "TextBox" Then
                OldTargetRow(ColumnIndex) = New Dictionary(Of String, String)
                OldTargetRow(ColumnIndex)("ControlType") = "TextBox"
                OldTargetRow(ColumnIndex)("ControlState") = ""

            ElseIf ColumnsDict(ColumnIndex)("ColumnControl") = "ComboBox" Then
                OldTargetRow(ColumnIndex) = New Dictionary(Of String, String)
                OldTargetRow(ColumnIndex)("ControlType") = "ComboBox"
                OldTargetRow(ColumnIndex)("ControlState") = ""

            End If

        Next

        'For Each ColumnName In ColumnsDict.Keys

        '    'If ColumnsDict(ColumnName)("ColumnControl") = "CheckBox" Then
        '    '    OldTargetRow(ColumnName) = New Dictionary(Of String, String)
        '    '    OldTargetRow(ColumnName)("ControlType") = "CheckBox"
        '    '    OldTargetRow(ColumnName)("ControlState") = ""

        '    'ElseIf ColumnsDict(ColumnName)("ColumnControl") = "TextBox" Then
        '    '    OldTargetRow(ColumnName) = New Dictionary(Of String, String)
        '    '    OldTargetRow(ColumnName)("ControlType") = "TextBox"
        '    '    OldTargetRow(ColumnName)("ControlState") = ""

        '    'ElseIf ColumnsDict(ColumnName)("ColumnControl") = "ComboBox" Then
        '    '    OldTargetRow(ColumnName) = New Dictionary(Of String, String)
        '    '    OldTargetRow(ColumnName)("ControlType") = "ComboBox"
        '    '    OldTargetRow(ColumnName)("ControlState") = ""

        '    'End If
        'Next

        Dim SelectedRowIndices As New List(Of Integer)
        Dim SourceRowIndex As Integer
        Dim TargetRowIndex As Integer
        'Dim ColumnIndex As Integer

        SelectedRowIndices = GetSelectedRowIndices(TableLayoutPanel)

        ' Need exactly one row selected
        If SelectedRowIndices.Count = 1 Then

            SourceRowIndex = SelectedRowIndices(0)

            ' Can't move up from the first row, or down from the last row.
            If Direction.ToLower = "up" Then
                tf = SourceRowIndex > 1
                TargetRowIndex = SourceRowIndex - 1
            Else
                tf = SourceRowIndex < TableLayoutPanel.RowCount - 1
                TargetRowIndex = SourceRowIndex + 1
            End If

            If tf Then

                For ColumnIndex = 0 To ColumnsDict.Keys.Count - 1

                    If ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox" Then
                        'ColumnIndex = CInt(ColumnsDict(ColumnName)("ColumnIndex"))
                        TargetCheckBox = New CheckBox
                        TargetCheckBox = CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex, TargetRowIndex), CheckBox)
                        OldTargetRow(ColumnIndex)("ControlState") = TargetCheckBox.Checked.ToString

                        SourceCheckBox = New CheckBox
                        SourceCheckBox = CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex, SourceRowIndex), CheckBox)
                        TargetCheckBox.Checked = SourceCheckBox.Checked
                        If OldTargetRow(ColumnIndex)("ControlState").ToLower = "true" Then
                            SourceCheckBox.Checked = True
                        Else
                            SourceCheckBox.Checked = False
                        End If

                    ElseIf ColumnsDict(ColumnIndex)("ColumnControl") = "TextBox" Then
                        'ColumnIndex = CInt(ColumnsDict(ColumnName)("ColumnIndex"))
                        TargetTextBox = New TextBox
                        TargetTextBox = CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex, TargetRowIndex), TextBox)
                        OldTargetRow(ColumnIndex)("ControlState") = TargetTextBox.Text

                        SourceTextBox = New TextBox
                        SourceTextBox = CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex, SourceRowIndex), TextBox)
                        TargetTextBox.Text = SourceTextBox.Text
                        SourceTextBox.Text = OldTargetRow(ColumnIndex)("ControlState")

                    ElseIf ColumnsDict(ColumnIndex)("ColumnControl") = "ComboBox" Then
                        'ColumnIndex = CInt(ColumnsDict(ColumnName)("ColumnIndex"))
                        TargetComboBox = New ComboBox
                        TargetComboBox = CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex, TargetRowIndex), ComboBox)
                        OldTargetRow(ColumnIndex)("ControlState") = TargetComboBox.Text

                        SourceComboBox = New ComboBox
                        SourceComboBox = CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex, SourceRowIndex), ComboBox)
                        TargetComboBox.Text = SourceComboBox.Text
                        SourceComboBox.Text = OldTargetRow(ColumnIndex)("ControlState")
                        If OldTargetRow(ColumnIndex)("ControlState") = "" Then SourceComboBox.SelectedItem = Nothing

                    End If

                Next
                'For Each ColumnName In ColumnsDict.Keys
                '    'ColumnIndex += 1
                '    'ColumnName = "PropertySet"
                '    'ColumnsDict(ColumnName) = New Dictionary(Of String, String)
                '    'ColumnsDict(ColumnName)("ColumnControl") = "ComboBox"
                '    'ColumnsDict(ColumnName)("ColumnIndex") = CStr(ColumnIndex)

                '    'If ColumnsDict(ColumnName)("ColumnControl") = "CheckBox" Then
                '    '    ColumnIndex = CInt(ColumnsDict(ColumnName)("ColumnIndex"))
                '    '    TargetCheckBox = New CheckBox
                '    '    TargetCheckBox = CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex, TargetRowIndex), CheckBox)
                '    '    OldTargetRow(ColumnName)("ControlState") = TargetCheckBox.Checked.ToString

                '    '    SourceCheckBox = New CheckBox
                '    '    SourceCheckBox = CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex, SourceRowIndex), CheckBox)
                '    '    TargetCheckBox.Checked = SourceCheckBox.Checked
                '    '    If OldTargetRow(ColumnName)("ControlState").ToLower = "true" Then
                '    '        SourceCheckBox.Checked = True
                '    '    Else
                '    '        SourceCheckBox.Checked = False
                '    '    End If

                '    'ElseIf ColumnsDict(ColumnName)("ColumnControl") = "TextBox" Then
                '    '    ColumnIndex = CInt(ColumnsDict(ColumnName)("ColumnIndex"))
                '    '    TargetTextBox = New TextBox
                '    '    TargetTextBox = CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex, TargetRowIndex), TextBox)
                '    '    OldTargetRow(ColumnName)("ControlState") = TargetTextBox.Text

                '    '    SourceTextBox = New TextBox
                '    '    SourceTextBox = CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex, SourceRowIndex), TextBox)
                '    '    TargetTextBox.Text = SourceTextBox.Text
                '    '    SourceTextBox.Text = OldTargetRow(ColumnName)("ControlState")

                '    'ElseIf ColumnsDict(ColumnName)("ColumnControl") = "ComboBox" Then
                '    '    ColumnIndex = CInt(ColumnsDict(ColumnName)("ColumnIndex"))
                '    '    TargetComboBox = New ComboBox
                '    '    TargetComboBox = CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex, TargetRowIndex), ComboBox)
                '    '    OldTargetRow(ColumnName)("ControlState") = TargetComboBox.Text

                '    '    SourceComboBox = New ComboBox
                '    '    SourceComboBox = CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex, SourceRowIndex), ComboBox)
                '    '    TargetComboBox.Text = SourceComboBox.Text
                '    '    SourceComboBox.Text = OldTargetRow(ColumnName)("ControlState")

                '    'End If
                'Next

            End If
        End If

    End Sub

    Public Function GetSelectedRowIndices(TableLayoutPanel As TableLayoutPanel) As List(Of Integer)
        Dim SelectedRowIndices As New List(Of Integer)
        Dim CheckBoxDict As New Dictionary(Of String, CheckBox)
        Dim CheckBoxName As String
        Dim s As String

        CheckBoxDict = GetCheckBoxes(TableLayoutPanel)

        For Each CheckBoxName In CheckBoxDict.Keys
            If CheckBoxName.Contains("Select") Then
                If Not CheckBoxName.Contains("SelectAll") Then
                    If CheckBoxDict(CheckBoxName).Checked Then
                        ' CheckBox.Name = String.Format("CheckBox{0}{1}", RowIndex, ColumnNames(ColumnIndex))
                        s = CheckBoxName.Replace("CheckBox", "")(0)  ' "CheckBox6Select" -> "6Select"  -> "6"
                        SelectedRowIndices.Add(CInt(s))
                    End If
                End If
            End If
        Next

        Return SelectedRowIndices
    End Function

    Public Sub ProcessCheckBoxSelectAll(
        TableLayoutPanel As TableLayoutPanel,
        CheckBoxSelectAll As CheckBox)

        Dim CheckBoxDict As New Dictionary(Of String, CheckBox)
        Dim CheckBoxName As String
        Dim tf As Boolean

        CheckBoxDict = GetCheckBoxes(TableLayoutPanel)

        For Each CheckBoxName In CheckBoxDict.Keys
            tf = CheckBoxSelectAll.Checked
            tf = tf And CheckBoxName.Contains("Select")
            If tf Then
                CheckBoxDict(CheckBoxName).Checked = True
            Else
                CheckBoxDict(CheckBoxName).Checked = False
            End If
        Next

    End Sub

    Public Function GetTableValues(
        TableLayoutPanel As TableLayoutPanel,
        ColumnsDict As Dictionary(Of Integer, Dictionary(Of String, String))
        ) As Dictionary(Of Integer, Dictionary(Of String, String))

        Dim TableValuesDict As New Dictionary(Of Integer, Dictionary(Of String, String))
        Dim ControlsDict As New Dictionary(Of String, Control)
        Dim ColumnName As String
        Dim ControlName As String
        Dim ColumnIndex As Integer

        Dim TextBox As New TextBox
        Dim ComboBox As New ComboBox
        Dim CheckBox As New CheckBox

        ControlsDict = GetControlsDict(TableLayoutPanel)

        ' ControlsDict format
        ' { "CheckBox1Find_PT": CheckBox,
        '   "TextBox1ReplaceString": TextBox,
        '   ...
        ' }

        For RowIndex = 1 To TableLayoutPanel.RowCount - 1

            TableValuesDict(RowIndex) = New Dictionary(Of String, String)

            For ColumnIndex = 0 To ColumnsDict.Keys.Count - 1

                ColumnName = ColumnsDict(ColumnIndex)("ColumnName")

                If ColumnsDict(ColumnIndex)("ColumnControl") = "TextBox" Then
                    ControlName = String.Format("TextBox{0}{1}", RowIndex, ColumnName)
                    'ColumnIndex = CInt(ColumnsDict(ColumnIndex)("ColumnIndex"))
                    ControlName = TableLayoutPanel.GetControlFromPosition(ColumnIndex, RowIndex).Name
                    TextBox = CType(ControlsDict(ControlName), TextBox)
                    TableValuesDict(RowIndex)(ColumnName) = TextBox.Text
                    If Not CBool(ColumnsDict(ColumnIndex)("AllowBlank")) Then
                        If TableValuesDict(RowIndex)(ColumnName) = "" Then
                            TableValuesDict.Remove(RowIndex)
                            Exit For
                        End If
                    End If

                ElseIf ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox" Then
                    ControlName = String.Format("CheckBox{0}{1}", RowIndex, ColumnName)
                    'ColumnIndex = CInt(ColumnsDict(ColumnIndex)("ColumnIndex"))
                    ControlName = TableLayoutPanel.GetControlFromPosition(ColumnIndex, RowIndex).Name
                    CheckBox = CType(ControlsDict(ControlName), CheckBox)
                    TableValuesDict(RowIndex)(ColumnName) = CStr(CheckBox.Checked)

                ElseIf ColumnsDict(ColumnIndex)("ColumnControl") = "ComboBox" Then
                    ControlName = String.Format("ComboBox{0}{1}", RowIndex, ColumnName)
                    'ColumnIndex = CInt(ColumnsDict(ColumnIndex)("ColumnIndex"))
                    ControlName = TableLayoutPanel.GetControlFromPosition(ColumnIndex, RowIndex).Name
                    ComboBox = CType(ControlsDict(ControlName), ComboBox)
                    TableValuesDict(RowIndex)(ColumnName) = ComboBox.Text
                    If Not CBool(ColumnsDict(ColumnIndex)("AllowBlank")) Then
                        If TableValuesDict(RowIndex)(ColumnName) = "" Then
                            TableValuesDict.Remove(RowIndex)
                            Exit For
                        End If
                    End If

                Else
                    MsgBox(String.Format("Control type '{0}' not implemented", ColumnsDict(ColumnIndex)("ColumnControl")))
                    'Exit Function
                End If

            Next
        Next

        Return TableValuesDict

    End Function

    Public Sub RestoreTableValues(
        TableLayoutPanel As TableLayoutPanel,
        ColumnsDict As Dictionary(Of Integer, Dictionary(Of String, String)),
        JSONString As String)

        Dim TableValuesDict As New Dictionary(Of Integer, Dictionary(Of String, String))
        Dim tmpTableValuesDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim ControlsDict As New Dictionary(Of String, Control)
        Dim ColumnName As String
        Dim Proceed As Boolean = True
        'Dim tf As Boolean

        Dim ControlName As String
        Dim ColumnIndex As Integer
        Dim ColumnIndexString As String
        Dim Key As String

        Dim TextBox As New TextBox
        Dim ComboBox As New ComboBox
        Dim CheckBox As New CheckBox

        If Not ColumnsDict.Keys.Count = TableLayoutPanel.ColumnCount Then
            MsgBox("Not ColumnsDict.Keys.Count = TableLayoutPanel.ColumnCount", vbOKOnly)
            Exit Sub
        End If

        ControlsDict = GetControlsDict(TableLayoutPanel)

        If Not JSONString = "" Then
            ' Possible to have bogus JSONString
            Try
                tmpTableValuesDict = JsonConvert.DeserializeObject(
                Of Dictionary(Of String, Dictionary(Of String, String)))(JSONString)

                For Each ColumnIndexString In tmpTableValuesDict.Keys
                    ColumnIndex = CInt(ColumnIndexString)
                    TableValuesDict(ColumnIndex) = New Dictionary(Of String, String)

                    For Each Key In tmpTableValuesDict(ColumnIndexString).Keys
                        TableValuesDict(ColumnIndex)(Key) = tmpTableValuesDict(ColumnIndexString)(Key)
                    Next

                Next
            Catch ex As Exception
                Proceed = False
            End Try
        Else
            Proceed = False
        End If

        If Proceed Then

            For RowIndex = 1 To TableValuesDict.Keys.Count

                For ColumnIndex = 0 To ColumnsDict.Count - 1

                    ColumnName = ColumnsDict(ColumnIndex)("ColumnName")

                    If ColumnsDict(ColumnIndex)("ColumnControl") = "TextBox" Then
                        ControlName = TableLayoutPanel.GetControlFromPosition(ColumnIndex, RowIndex).Name
                        TextBox = CType(ControlsDict(ControlName), TextBox)

                        If CBool(ColumnsDict(ColumnIndex)("PopulateWithDefault")) Then
                            TextBox.Text = ColumnsDict(ColumnIndex)("DefaultValue")
                        Else
                            If TableValuesDict.ContainsKey(RowIndex) Then
                                TextBox.Text = TableValuesDict(RowIndex)(ColumnName)
                            Else
                                TextBox.Text = ""
                            End If

                        End If

                    ElseIf ColumnsDict(ColumnIndex)("ColumnControl") = "CheckBox" Then
                        ControlName = TableLayoutPanel.GetControlFromPosition(ColumnIndex, RowIndex).Name
                        CheckBox = CType(ControlsDict(ControlName), CheckBox)

                        If CBool(ColumnsDict(ColumnIndex)("PopulateWithDefault")) Then
                            CheckBox.Checked = CBool(ColumnsDict(ColumnIndex)("DefaultValue"))
                        Else
                            If TableValuesDict.ContainsKey(RowIndex) Then
                                CheckBox.Checked = CBool(TableValuesDict(RowIndex)(ColumnName))
                            Else
                                CheckBox.Checked = False
                            End If

                        End If

                    ElseIf ColumnsDict(ColumnIndex)("ColumnControl") = "ComboBox" Then
                        ControlName = TableLayoutPanel.GetControlFromPosition(ColumnIndex, RowIndex).Name
                        ComboBox = CType(ControlsDict(ControlName), ComboBox)
                        If TableValuesDict.ContainsKey(RowIndex) Then
                            ComboBox.Text = TableValuesDict(RowIndex)(ColumnName)
                        Else
                            If ComboBox.Items.Count > 0 Then
                                ComboBox.SelectedItem = ComboBox.Items(0)
                            Else
                                ComboBox.SelectedItem = Nothing
                            End If
                        End If

                        'If ComboBox.Name.EndsWith("PropertyName") Then
                        '    Try
                        '        FormPropertyInputEditor.SetCombo(CType(TableLayoutPanel.GetControlFromPosition(ColumnIndex - 1, RowIndex), ComboBox))
                        '    Catch ex As Exception

                        '    End Try

                        'End If

                        If CBool(ColumnsDict(ColumnIndex)("PopulateWithDefault")) Then
                            ComboBox.Text = ColumnsDict(ColumnIndex)("DefaultValue")
                        Else
                            If TableValuesDict.ContainsKey(RowIndex) Then
                                ComboBox.Text = TableValuesDict(RowIndex)(ColumnName)
                            Else
                                If ComboBox.Items.Count > 0 Then
                                    ComboBox.SelectedItem = ComboBox.Items(0)
                                Else
                                    ComboBox.SelectedItem = Nothing
                                End If
                            End If
                        End If
                    Else
                        MsgBox(String.Format("Control type '{0}' not implemented", ColumnsDict(ColumnIndex)("ColumnControl")))
                        'Exit Function
                    End If

                Next
            Next
        End If

    End Sub


End Class
