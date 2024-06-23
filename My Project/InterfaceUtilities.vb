Option Strict On
Imports Housekeeper.Task

Public Class InterfaceUtilities

    Private Property TaskList As List(Of Task)

    'Public Sub BuildTaskPage(
    '    TaskList As List(Of Task),
    '    TabPageTask As TabPage)

    '    ' TabPageTask is the tab on Form1 that holds the interface built here.
    '    ' TLP Base has one row each for: TLPHeader, TLPParent, and TLPEditTaskList.
    '    ' TLPHeader has buttons that act on all tasks.
    '    ' TLPParent has one row for each TLPTask.
    '    ' TLPEditTaskList holds the button for customizing the task list.

    '    Me.TaskList = TaskList

    '    Dim RowIndex As Integer

    '    Dim TLPBase As New ExTableLayoutPanel

    '    ' TLPBase.SuspendLayout()

    '    Dim TLPParent As New ExTableLayoutPanel

    '    ' TLPParent.SuspendLayout()

    '    TLPBase.ColumnCount = 1
    '    TLPBase.RowCount = 3
    '    TLPBase.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
    '    TLPBase.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))
    '    TLPBase.RowStyles.Add(New RowStyle(SizeType.Percent, 100))
    '    TLPBase.RowStyles.Add(New RowStyle(SizeType.Absolute, 40))
    '    TLPBase.Width = TabPageTask.Width
    '    TLPBase.Dock = DockStyle.Fill

    '    RowIndex = 0

    '    Dim TLPHeader = BuildTLPHeader(TLPParent)

    '    TLPBase.Controls.Add(TLPHeader, 0, RowIndex)

    '    RowIndex += 1

    '    TLPParent.ColumnCount = 1
    '    TLPParent.RowCount = TaskList.Count + 1
    '    TLPParent.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
    '    TLPParent.BorderStyle = BorderStyle.None
    '    TLPParent.Width = TabPageTask.Width - 10  ' Needed for AutoSize to work correctly (maybe).
    '    'TLPParent.Dock = DockStyle.Fill
    '    TLPParent.AutoScroll = True

    '    TLPParent.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))

    '    Dim i As Integer = 0

    '    For i = 0 To TLPParent.RowCount - 1
    '        TLPParent.RowStyles.Add(New RowStyle(SizeType.AutoSize))
    '    Next
    '    ' Last row is blank
    '    TLPParent.RowStyles.Add(New RowStyle(SizeType.Absolute, 15))  ' Needed for AutoSize to work correctly (maybe).

    '    TLPParent.Anchor = CType(AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom, AnchorStyles)

    '    TLPParent.AutoSize = True
    '    'TLPParent.AutoSizeMode = AutoSizeMode.GrowAndShrink

    '    i = 0

    '    For Each Task In TaskList
    '        Dim TLPTask = Task.GetTLPTask(TLPParent)

    '        Dim PU As New PreferencesUtilities
    '        PU.ConfigureTaskUI(Task)

    '        TLPParent.Controls.Add(TLPTask, 0, i)

    '        i += 1
    '    Next

    '    TLPBase.Controls.Add(TLPParent, 0, RowIndex)

    '    RowIndex += 1

    '    Dim TLPEditTaskList = BuildTLPEditTaskList(TLPParent)

    '    TLPBase.Controls.Add(TLPEditTaskList, 0, RowIndex)

    '    'TLPParent.ResumeLayout()

    '    'TLPBase.ResumeLayout()

    '    TabPageTask.Controls.Add(TLPBase)

    'End Sub

    'Public Function BuildTLPHeader(TLPParent As ExTableLayoutPanel) As ExTableLayoutPanel

    '    Dim TLPHeader As New ExTableLayoutPanel

    '    Dim TLPTasksParameters = GetTLPTasksParameters()

    '    Dim ColumnWidth As Integer = CInt(TLPTasksParameters("ColumnWidth"))
    '    Dim RowHeight As Integer = CInt(TLPTasksParameters("RowHeight"))
    '    Dim ColumnNames = Split(TLPTasksParameters("ColumnNamesString"), " ").ToList
    '    TLPHeader.ColumnCount = CInt(TLPTasksParameters("ColumnCount"))

    '    Dim ColumnName As String

    '    TLPHeader.RowCount = 1
    '    TLPHeader.CellBorderStyle = TableLayoutPanelCellBorderStyle.None
    '    TLPHeader.BorderStyle = BorderStyle.None
    '    'TLPHeader.Width = TLPParent.Width - 10
    '    TLPHeader.Height = RowHeight + 2
    '    TLPHeader.Padding = New Padding(5, 0, 20, 0)

    '    AddHandler TLPHeader.CellPaint, AddressOf Task_EventHandler.TLPHeader_CellPaint

    '    Dim RowIndex As Integer
    '    Dim ColumnIndex As Integer

    '    Dim Button As Button
    '    Dim Label As Label

    '    RowIndex = 0

    '    For ColumnIndex = 0 To TLPHeader.ColumnCount - 1
    '        ColumnName = ColumnNames(ColumnIndex)

    '        Select Case ColumnName
    '            Case BaseControlNames.Expand.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                Button = FormatButton(ColumnName, My.Resources.expand, True)
    '                TLPHeader.Controls.Add(Button, ColumnIndex, RowIndex)
    '                BaseControlsDict(Button.Name) = Button

    '            Case BaseControlNames.SelectTask.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                Button = FormatButton(ColumnName, My.Resources.Unchecked, True)
    '                TLPHeader.Controls.Add(Button, ColumnIndex, RowIndex)
    '                BaseControlsDict(Button.Name) = Button

    '            Case BaseControlNames.SelectAssembly.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                Button = FormatButton(ColumnName, My.Resources.SE_asm, True)
    '                TLPHeader.Controls.Add(Button, ColumnIndex, RowIndex)
    '                BaseControlsDict(Button.Name) = Button

    '            Case BaseControlNames.SelectPart.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                Button = FormatButton(ColumnName, My.Resources.SE_par, True)
    '                TLPHeader.Controls.Add(Button, ColumnIndex, RowIndex)
    '                BaseControlsDict(Button.Name) = Button

    '            Case BaseControlNames.SelectSheetmetal.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                Button = FormatButton(ColumnName, My.Resources.SE_psm, True)
    '                TLPHeader.Controls.Add(Button, ColumnIndex, RowIndex)
    '                BaseControlsDict(Button.Name) = Button

    '            Case BaseControlNames.SelectDraft.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                Button = FormatButton(ColumnName, My.Resources.SE_dft, True)
    '                TLPHeader.Controls.Add(Button, ColumnIndex, RowIndex)
    '                BaseControlsDict(Button.Name) = Button

    '            Case BaseControlNames.Task.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
    '                Label = FormatLabel(ColumnName, "TASK", Nothing)
    '                Label.Padding = New Padding(5, 5, 0, 0)     '<----- added to control label position
    '                'Label.Anchor = AnchorStyles.Left
    '                TLPHeader.Controls.Add(Label, ColumnIndex, RowIndex)
    '                BaseControlsDict(Label.Name) = Label

    '            Case BaseControlNames.Help.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth + 2)) '<--- added +2 to not truncate the icon
    '                Button = FormatButton(ColumnName, My.Resources.Help, True)
    '                TLPHeader.Controls.Add(Button, ColumnIndex, RowIndex)
    '                BaseControlsDict(Button.Name) = Button

    '            Case Else
    '                MsgBox(String.Format("{0} Column name '{1}' not recognized", Me.ToString, ColumnName))

    '        End Select
    '    Next

    '    For RowIndex = 0 To TLPHeader.RowCount - 1
    '        TLPHeader.RowStyles.Add(New RowStyle(SizeType.Absolute, RowHeight))
    '    Next

    '    TLPHeader.Dock = DockStyle.Fill

    '    Return TLPHeader
    'End Function

    'Public Function BuildTLPTask(
    '    Task As Task,
    '    ) As UCTaskControl

    '    Dim TaskUC As New UCTaskControl(Task)

    '    'AddHandler TLPTask.CellPaint, AddressOf Task_EventHandler.TLPTask_CellPaint

    '    Return TaskUC
    'End Function

    'Public Function BuildTLPEditTaskList(TLPParent As ExTableLayoutPanel) As ExTableLayoutPanel
    '    Dim TLPEditTaskList As New ExTableLayoutPanel

    '    AddHandler TLPEditTaskList.CellPaint, AddressOf Task_EventHandler.TLPHeader_CellPaint

    '    TLPEditTaskList.RowCount = 1
    '    TLPEditTaskList.CellBorderStyle = TableLayoutPanelCellBorderStyle.None
    '    TLPEditTaskList.BorderStyle = BorderStyle.None
    '    TLPEditTaskList.Padding = New Padding(5, 0, 20, 0)

    '    TLPEditTaskList.ColumnCount = 2

    '    TLPEditTaskList.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 100))
    '    TLPEditTaskList.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))

    '    TLPEditTaskList.Dock = DockStyle.Fill

    '    Dim Button As Button
    '    'Dim Label As Label

    '    Button = FormatOptionsButton("EditTaskList", "Edit task list")
    '    'Button.Width = 100
    '    AddHandler Button.Click, AddressOf Task_EventHandler.Button_Click

    '    TLPEditTaskList.Controls.Add(Button, 0, 0)

    '    'Label = FormatOptionsLabel("EditTaskListLabel", "Edit task list")
    '    'TLPEditTaskList.Controls.Add(Label, 1, 0)


    '    Return TLPEditTaskList
    'End Function

    'Public Function GetTLPTasksParameters() As Dictionary(Of String, String)
    '    Dim TLPTasksParameters As New Dictionary(Of String, String)
    '    Dim ColumnNamesString As String

    '    TLPTasksParameters("ColumnWidth") = CStr(23)    '<---- test with 23 instead of 20 to not truncate icons
    '    TLPTasksParameters("RowHeight") = TLPTasksParameters("ColumnWidth")
    '    TLPTasksParameters("ColumnCount") = CStr(8)

    '    ColumnNamesString = BaseControlNames.Expand.ToString
    '    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.SelectTask.ToString)
    '    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.SelectAssembly.ToString)
    '    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.SelectPart.ToString)
    '    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.SelectSheetmetal.ToString)
    '    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.SelectDraft.ToString)
    '    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.Task.ToString)
    '    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.Help.ToString)

    '    TLPTasksParameters("ColumnNamesString") = ColumnNamesString


    '    Return TLPTasksParameters
    'End Function



    'Public Function FormatButton(
    '    Name As String,
    '    Image As Image,
    '    Enabled As Boolean
    '    ) As Button

    '    Dim Button As New Button

    '    Button.Name = Name
    '    Button.Text = ""
    '    Button.FlatStyle = FlatStyle.Flat
    '    Button.FlatAppearance.BorderSize = 0
    '    Button.Image = Image
    '    Button.Enabled = Enabled

    '    'Button.Dock = DockStyle.Fill
    '    Button.Anchor = AnchorStyles.None

    '    AddHandler Button.Click, AddressOf Task_EventHandler.Button_Click

    '    Return Button
    'End Function

    'Private Function FormatCheckBox(
    '    Name As String,
    '    Image As Image,
    '    Enabled As Boolean
    '    ) As CheckBox

    '    Dim CheckBox As New CheckBox

    '    CheckBox.Name = Name
    '    CheckBox.Text = ""

    '    'CheckBox.Dock = DockStyle.Fill
    '    CheckBox.Anchor = AnchorStyles.None

    '    CheckBox.Appearance = Appearance.Button
    '    CheckBox.FlatStyle = FlatStyle.Flat
    '    CheckBox.FlatAppearance.BorderSize = 0
    '    CheckBox.FlatAppearance.CheckedBackColor = Color.Transparent
    '    CheckBox.FlatAppearance.MouseOverBackColor = Color.Transparent 'SystemColors.Control
    '    CheckBox.FlatAppearance.MouseDownBackColor = Color.Transparent

    '    CheckBox.Image = Image

    '    CheckBox.Enabled = Enabled

    '    AddHandler CheckBox.CheckedChanged, AddressOf Task_EventHandler.CheckBox_CheckedChanged

    '    Return CheckBox
    'End Function

    'Public Function FormatLabel(
    '    Name As String,
    '    LabelText As String,
    '    Image As Image
    '    ) As Label

    '    Dim Label As New Label
    '    Dim Indent As String = "        "

    '    Label.Name = Name
    '    'Label.Font = New Font(Label.Font.Name, 9.5, FontStyle.Regular) '<--- this to force label height to 16 pixel and not truncate the icons

    '    Label.Text = String.Format("{0}{1}", Indent, LabelText)
    '    Label.AutoSize = False                                          '<--- controlling position with padding, autosize truncate the icons if text is small
    '    Label.Anchor = CType(AnchorStyles.Left + AnchorStyles.Right, AnchorStyles)
    '    'Label.BackColor = Color.LightBlue                              '<--- use it to debug label size

    '    If Image IsNot Nothing Then
    '        Label.Image = Image
    '        Label.ImageAlign = ContentAlignment.MiddleLeft
    '    End If

    '    Return Label
    'End Function

    Public Sub FormatTLPOptions(
        TLP As TableLayoutPanel,
        Name As String,
        NumRows As Integer,
        Optional NumColumns As Integer = 2)

        TLP.Name = Name
        TLP.RowCount = NumRows
        For i As Integer = 0 To TLP.RowCount - 1
            TLP.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        Next

        TLP.ColumnCount = NumColumns
        For i As Integer = 0 To NumColumns - 1
            If i < NumColumns - 1 Then
                TLP.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            Else
                TLP.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
            End If

        Next
        TLP.Dock = DockStyle.Fill

        TLP.AutoSize = True
        TLP.Visible = False

    End Sub

    Public Sub FormatTLPOptionsEx(
        TLP As TableLayoutPanel,
        Name As String,
        NumRows As Integer,
        Column1Width As Integer,
        Column2Width As Integer)

        TLP.Name = Name
        TLP.RowCount = NumRows
        For i As Integer = 0 To TLP.RowCount - 1
            TLP.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        Next

        TLP.ColumnCount = 3

        TLP.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, Column1Width))
        TLP.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, Column2Width))
        TLP.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))

        TLP.Dock = DockStyle.Fill

        TLP.AutoSize = True
        TLP.Visible = False

    End Sub

    Public Function FormatOptionsCheckBox(
        ControlName As String,
        CheckBoxText As String
        ) As CheckBox

        Dim CheckBox = New CheckBox
        CheckBox.Name = ControlName
        CheckBox.Text = CheckBoxText
        CheckBox.Anchor = AnchorStyles.Left
        CheckBox.AutoSize = True

        Return CheckBox
    End Function

    Public Function FormatOptionsButton(
        ControlName As String,
        ButtonText As String
        ) As Button

        Dim Button = New Button
        Button.Name = ControlName
        Button.Text = ButtonText
        Button.Anchor = AnchorStyles.Left
        Button.AutoSize = True

        Return Button
    End Function

    Public Function FormatOptionsTextBox(
        ControlName As String,
        TextBoxText As String
        ) As TextBox

        Dim TextBox = New TextBox
        TextBox.Name = ControlName
        TextBox.Text = TextBoxText
        TextBox.Anchor = CType(AnchorStyles.Left + AnchorStyles.Right, AnchorStyles)
        'TextBox.AutoSize = True

        Return TextBox
    End Function

    Public Function FormatOptionsComboBox(
        ControlName As String,
        ComboBoxItems As List(Of String),
        DropDownStyleName As String
        ) As ComboBox

        ' DropDownStyleName: Simple | DropDown | DropDownList

        Dim ComboBox = New ComboBox
        ComboBox.Name = ControlName
        Dim ComboBoxItem As String
        Dim MaxCharacters As Integer = 0

        Select Case DropDownStyleName
            Case "Simple"
                ComboBox.DropDownStyle = ComboBoxStyle.Simple
            Case "DropDown"
                ComboBox.DropDownStyle = ComboBoxStyle.DropDown
            Case "DropDownList"
                ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
            Case Else
                MsgBox(String.Format("{0} DropDownStyleName '{1}' not recognized", Me.ToString, DropDownStyleName))

        End Select

        For Each ComboBoxItem In ComboBoxItems
            ComboBox.Items.Add(ComboBoxItem)
            If Len(ComboBoxItem) > MaxCharacters Then MaxCharacters = Len(ComboBoxItem)
        Next
        ComboBox.Text = CStr(ComboBox.Items(0))

        ComboBox.Width = MaxCharacters * 15

        ComboBox.Anchor = CType(AnchorStyles.Left, AnchorStyles)

        Return ComboBox
    End Function

    Public Function FormatOptionsLabel(
        Name As String,
        LabelText As String
        ) As Label

        Dim Label As New Label

        Label.Name = Name
        Label.Text = LabelText
        Label.AutoSize = True
        Label.Anchor = CType(AnchorStyles.Left, AnchorStyles)

        Return Label
    End Function

End Class
