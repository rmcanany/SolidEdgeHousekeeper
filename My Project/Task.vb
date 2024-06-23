Option Strict On
Imports Newtonsoft.Json
Imports SolidEdgeConstants

Public MustInherit Class Task
    Inherits IsolatedTaskProxy

    Public Property Name As String
    Public Property Description As String
    Public Property HelpText As String
    Public Property RequiresSave As Boolean
    Public Property AppliesToAssembly As Boolean
    Public Property AppliesToPart As Boolean
    Public Property AppliesToSheetmetal As Boolean
    Public Property AppliesToDraft As Boolean
    Public Property HasOptions As Boolean
    Public Property HelpURL As String
    Public Property Image As Image
    Public Property TaskControl As UCTaskControl
    Public Property TaskOptionsTLP As ExTableLayoutPanel
    Public Property ManualOptionsOnlyString As String = "Only show options manually. Use [+] to show."
    Public Property IsSelectedTask As Boolean
    Public Property IsSelectedAssembly As Boolean
    Public Property IsSelectedPart As Boolean
    Public Property IsSelectedSheetmetal As Boolean
    Public Property IsSelectedDraft As Boolean
    Public Property AutoHideOptions As Boolean
    Public Property IsOptionsHidden As Boolean
    Public Property RememberTaskSelections As Boolean
    Public Property Visible As Boolean
    Public Property RequiresSourceDirectories As Boolean
    Public Property SourceDirectories As List(Of String)


    Public TLPHeader As ExTableLayoutPanel
    Public Property ControlsDict As Dictionary(Of String, Control)
    Shared Property BaseControlsDict As New Dictionary(Of String, Control)
    Public Property ManuallySelectFileTypes As Boolean
    Public Property Task_EventHandler As Task_EventHandler
    Public Property ColorHue As String
    Public Property ColorSaturation As Double
    Public Property ColorBrightness As Double
    Public Property ColorR As Integer
    Public Property ColorG As Integer
    Public Property ColorB As Integer

    Public Property AssemblyTemplate As String
    Public Property PartTemplate As String
    Public Property SheetmetalTemplate As String
    Public Property DraftTemplate As String
    Public Property MaterialTable As String
    Public Property Category As String
    Public Property SolidEdgeRequired As Boolean = True




    Public Enum BaseControlNames
        Expand
        SelectTask
        SelectAssembly
        SelectPart
        SelectSheetmetal
        SelectDraft
        Task
        Help
        EditTaskList
    End Enum

    Public MustOverride Function Process(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        Configuration As Dictionary(Of String, String),
        SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

    Public MustOverride Function Process(FileName As String) As Dictionary(Of Integer, List(Of String))

    'Public MustOverride Function GetTaskControl(
    '    TLPParent As ExTableLayoutPanel
    '    ) As UCTaskControl

    Public MustOverride Function CheckStartConditions(
        PriorErrorMessage As Dictionary(Of Integer, List(Of String))
        ) As Dictionary(Of Integer, List(Of String))


    Public Sub New()

    End Sub

    Public Sub GenerateTaskControl()

        ControlsDict = New Dictionary(Of String, Control)

        Me.TaskControl = New UCTaskControl(Me)

        'If Me.Description.Contains("Print") Then
        '    MsgBox(Me.Description)
        'End If

        For Each Control As Control In Me.TaskControl.Controls
            If ControlsDict.Keys.Contains(Control.Name) Then
                MsgBox(String.Format("ControlsDict already has Key '{0}'", Control.Name))
            End If
            ControlsDict(Control.Name) = Control
        Next

    End Sub


    'BUILD INTERFACE

    'Public Sub BuildTaskPage(
    '    TaskList As List(Of Task),
    '    TabPageTask As TabPage)

    '    ' TabPageTask is the tab on Form1 that holds the interface built here.
    '    ' TLP Base has one row each for: TLPHeader, TLPParent, and TLPEditTaskList.
    '    ' TLPHeader has buttons that act on all tasks.
    '    ' TLPParent has one row for each TLPTask.
    '    ' TLPEditTaskList holds the button for customizing the task list.

    '    Dim RowIndex As Integer

    '    Dim TLPBase As New ExTableLayoutPanel

    '    Dim TLPParent As New ExTableLayoutPanel

    '    TLPBase.ColumnCount = 1
    '    TLPBase.RowCount = 3
    '    TLPBase.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
    '    TLPBase.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))
    '    TLPBase.RowStyles.Add(New RowStyle(SizeType.Percent, 100))
    '    TLPBase.RowStyles.Add(New RowStyle(SizeType.Absolute, 40))
    '    TLPBase.Width = TabPageTask.Width
    '    TLPBase.Dock = DockStyle.Fill

    '    RowIndex = 0

    '    TLPHeader = BuildTLPHeader(TLPParent)

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

    '    AddHandler TLPHeader.CellPaint, AddressOf TLPHeader_CellPaint

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
    '                Button = FormatButton(ColumnName, My.Resources.asm, True)
    '                TLPHeader.Controls.Add(Button, ColumnIndex, RowIndex)
    '                BaseControlsDict(Button.Name) = Button

    '            Case BaseControlNames.SelectPart.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                Button = FormatButton(ColumnName, My.Resources.par, True)
    '                TLPHeader.Controls.Add(Button, ColumnIndex, RowIndex)
    '                BaseControlsDict(Button.Name) = Button

    '            Case BaseControlNames.SelectSheetmetal.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                Button = FormatButton(ColumnName, My.Resources.psm, True)
    '                TLPHeader.Controls.Add(Button, ColumnIndex, RowIndex)
    '                BaseControlsDict(Button.Name) = Button

    '            Case BaseControlNames.SelectDraft.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                Button = FormatButton(ColumnName, My.Resources.dft, True)
    '                TLPHeader.Controls.Add(Button, ColumnIndex, RowIndex)
    '                BaseControlsDict(Button.Name) = Button

    '            Case BaseControlNames.Task.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
    '                Label = FormatLabel(ColumnName, "TASK", Nothing)
    '                'Label.Anchor = AnchorStyles.Left
    '                TLPHeader.Controls.Add(Label, ColumnIndex, RowIndex)
    '                BaseControlsDict(Label.Name) = Label

    '            Case BaseControlNames.Help.ToString
    '                TLPHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                Button = FormatButton(ColumnName, My.Resources.Help, True)
    '                TLPHeader.Controls.Add(Button, ColumnIndex, RowIndex)
    '                BaseControlsDict(Button.Name) = Button

    '            Case Else
    '                Throw New ArgumentException(String.Format("Column name '{0}' not recognized", ColumnName))

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
    '    TLPParent As ExTableLayoutPanel
    '    ) As ExTableLayoutPanel

    '    Dim TLPTask As New ExTableLayoutPanel(Task)

    '    AddHandler TLPTask.CellPaint, AddressOf Task_EventHandler.TLPTask_CellPaint

    '    Dim ColumnIndex As Integer
    '    Dim RowIndex As Integer

    '    Dim Button As Button
    '    Dim CheckBox As CheckBox
    '    Dim Label As Label

    '    Dim TLPTasksParameters = GetTLPTasksParameters()

    '    Dim ColumnWidth As Integer = CInt(TLPTasksParameters("ColumnWidth"))
    '    Dim RowHeight As Integer = CInt(TLPTasksParameters("RowHeight"))
    '    Dim ColumnNames = Split(TLPTasksParameters("ColumnNamesString"), " ").ToList
    '    TLPTask.ColumnCount = CInt(TLPTasksParameters("ColumnCount"))

    '    Dim ColumnName As String

    '    TLPTask.RowCount = 2
    '    TLPTask.CellBorderStyle = TableLayoutPanelCellBorderStyle.None
    '    TLPTask.BorderStyle = BorderStyle.None
    '    TLPTask.Width = TLPParent.Width - 10
    '    TLPTask.AutoSize = True

    '    RowIndex = 0

    '    For ColumnIndex = 0 To TLPTask.ColumnCount - 1
    '        ColumnName = ColumnNames(ColumnIndex)

    '        Select Case ColumnName

    '            Case BaseControlNames.Expand.ToString '"Expand"
    '                TLPTask.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                If Task.HasOptions Then
    '                    Button = FormatButton(ColumnName, My.Resources.expand, True)
    '                    TLPTask.Controls.Add(Button, ColumnIndex, RowIndex)
    '                Else
    '                    Button = FormatButton(ColumnName, My.Resources.expand_disabled, False)
    '                    TLPTask.Controls.Add(Button, ColumnIndex, RowIndex)
    '                End If

    '            Case BaseControlNames.SelectTask.ToString
    '                TLPTask.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                CheckBox = FormatCheckBox(ColumnName, My.Resources.Unchecked, True)
    '                TLPTask.Controls.Add(CheckBox, ColumnIndex, RowIndex)

    '            Case BaseControlNames.SelectAssembly.ToString
    '                TLPTask.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                If Task.AppliesToAssembly Then
    '                    CheckBox = FormatCheckBox(ColumnName, My.Resources.Unchecked, True)
    '                    TLPTask.Controls.Add(CheckBox, ColumnIndex, RowIndex)
    '                Else
    '                    CheckBox = FormatCheckBox(ColumnName, My.Resources.unchecked_disabled, False)
    '                    TLPTask.Controls.Add(CheckBox, ColumnIndex, RowIndex)
    '                End If

    '            Case BaseControlNames.SelectPart.ToString
    '                TLPTask.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                If Task.AppliesToPart Then
    '                    CheckBox = FormatCheckBox(ColumnName, My.Resources.Unchecked, True)
    '                    TLPTask.Controls.Add(CheckBox, ColumnIndex, RowIndex)
    '                Else
    '                    CheckBox = FormatCheckBox(ColumnName, My.Resources.unchecked_disabled, False)
    '                    TLPTask.Controls.Add(CheckBox, ColumnIndex, RowIndex)
    '                End If

    '            Case BaseControlNames.SelectSheetmetal.ToString
    '                TLPTask.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                If Task.AppliesToSheetmetal Then
    '                    CheckBox = FormatCheckBox(ColumnName, My.Resources.Unchecked, True)
    '                    TLPTask.Controls.Add(CheckBox, ColumnIndex, RowIndex)
    '                Else
    '                    CheckBox = FormatCheckBox(ColumnName, My.Resources.unchecked_disabled, False)
    '                    TLPTask.Controls.Add(CheckBox, ColumnIndex, RowIndex)
    '                End If

    '            Case BaseControlNames.SelectDraft.ToString
    '                TLPTask.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                If Task.AppliesToDraft Then
    '                    CheckBox = FormatCheckBox(ColumnName, My.Resources.Unchecked, True)
    '                    TLPTask.Controls.Add(CheckBox, ColumnIndex, RowIndex)
    '                Else
    '                    CheckBox = FormatCheckBox(ColumnName, My.Resources.unchecked_disabled, False)
    '                    TLPTask.Controls.Add(CheckBox, ColumnIndex, RowIndex)
    '                End If

    '            Case BaseControlNames.Task.ToString
    '                TLPTask.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
    '                Label = FormatLabel(ColumnName, Task.Description, Task.Image)
    '                Label.Padding = New Padding(5, 0, 0, 0)
    '                TLPTask.Controls.Add(Label, ColumnIndex, RowIndex)

    '            Case BaseControlNames.Help.ToString
    '                TLPTask.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, ColumnWidth))
    '                Button = FormatButton(ColumnName, My.Resources.Help, True)
    '                TLPTask.Controls.Add(Button, ColumnIndex, RowIndex)

    '            Case Else
    '                Throw New ArgumentException(String.Format("Column name '{0}' not recognized", ColumnName))
    '        End Select
    '    Next

    '    For RowIndex = 0 To TLPTask.RowCount - 1
    '        'TLPTask.RowStyles.Add(New RowStyle(SizeType.Absolute, RowHeight))
    '        TLPTask.RowStyles.Add(New RowStyle(SizeType.AutoSize))
    '    Next

    '    'TLPTask.Dock = DockStyle.Fill
    '    TLPTask.Anchor = CType(AnchorStyles.Left + AnchorStyles.Top + AnchorStyles.Right + AnchorStyles.Bottom, AnchorStyles)

    '    Return TLPTask
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

    '    Dim IU As New InterfaceUtilities

    '    Button = IU.FormatOptionsButton("EditTaskList", "Edit task list")
    '    'Button.Width = 100
    '    AddHandler Button.Click, AddressOf Task_EventHandler.Button_Click

    '    TLPEditTaskList.Controls.Add(Button, 0, 0)

    '    'Label = FormatOptionsLabel("EditTaskListLabel", "Edit task list")
    '    'TLPEditTaskList.Controls.Add(Label, 1, 0)


    '    Return TLPEditTaskList
    'End Function

    ''Public Function GetTLPTasksParameters() As Dictionary(Of String, String)
    ''    Dim TLPTasksParameters As New Dictionary(Of String, String)
    ''    Dim ColumnNamesString As String

    ''    TLPTasksParameters("ColumnWidth") = CStr(20)
    ''    TLPTasksParameters("RowHeight") = TLPTasksParameters("ColumnWidth")
    ''    TLPTasksParameters("ColumnCount") = CStr(8)

    ''    ColumnNamesString = BaseControlNames.Expand.ToString
    ''    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.SelectTask.ToString)
    ''    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.SelectAssembly.ToString)
    ''    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.SelectPart.ToString)
    ''    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.SelectSheetmetal.ToString)
    ''    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.SelectDraft.ToString)
    ''    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.Task.ToString)
    ''    ColumnNamesString = String.Format("{0} {1}", ColumnNamesString, BaseControlNames.Help.ToString)

    ''    TLPTasksParameters("ColumnNamesString") = ColumnNamesString


    ''    Return TLPTasksParameters
    ''End Function


    ''CONTROL CREATION AND FORMATTING

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
    '    CheckBox.FlatAppearance.CheckedBackColor = SystemColors.Control

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
    '    Label.Text = String.Format("{0}{1}", Indent, LabelText)
    '    Label.AutoSize = True
    '    Label.Anchor = CType(AnchorStyles.Left + AnchorStyles.Right, AnchorStyles)

    '    If Image IsNot Nothing Then
    '        Label.Image = Image
    '        Label.ImageAlign = ContentAlignment.MiddleLeft
    '    End If

    '    Return Label
    'End Function

    'Public Sub FormatTLPOptions(
    '    TLP As TableLayoutPanel,
    '    Name As String,
    '    NumRows As Integer,
    '    Optional NumColumns As Integer = 2)

    '    TLP.Name = Name
    '    TLP.RowCount = NumRows
    '    For i As Integer = 0 To TLP.RowCount - 1
    '        TLP.RowStyles.Add(New RowStyle(SizeType.AutoSize))
    '    Next

    '    TLP.ColumnCount = NumColumns
    '    For i As Integer = 0 To NumColumns - 1
    '        If i < NumColumns - 1 Then
    '            TLP.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
    '        Else
    '            TLP.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
    '        End If

    '    Next
    '    TLP.Dock = DockStyle.Fill

    '    TLP.AutoSize = True
    '    TLP.Visible = False

    'End Sub

    'Public Sub FormatTLPOptionsEx(
    '    TLP As TableLayoutPanel,
    '    Name As String,
    '    NumRows As Integer,
    '    Column1Width As Integer,
    '    Column2Width As Integer)

    '    TLP.Name = Name
    '    TLP.RowCount = NumRows
    '    For i As Integer = 0 To TLP.RowCount - 1
    '        TLP.RowStyles.Add(New RowStyle(SizeType.AutoSize))
    '    Next

    '    TLP.ColumnCount = 3

    '    TLP.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, Column1Width))
    '    TLP.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, Column2Width))
    '    TLP.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))

    '    TLP.Dock = DockStyle.Fill

    '    TLP.AutoSize = True
    '    TLP.Visible = False

    'End Sub

    'Public Function FormatOptionsCheckBox(
    '    ControlName As String,
    '    CheckBoxText As String
    '    ) As CheckBox

    '    Dim CheckBox = New CheckBox
    '    CheckBox.Name = ControlName
    '    CheckBox.Text = CheckBoxText
    '    CheckBox.Anchor = AnchorStyles.Left
    '    CheckBox.AutoSize = True

    '    Return CheckBox
    'End Function

    'Public Function FormatOptionsButton(
    '    ControlName As String,
    '    ButtonText As String
    '    ) As Button

    '    Dim Button = New Button
    '    Button.Name = ControlName
    '    Button.Text = ButtonText
    '    Button.Anchor = AnchorStyles.Left
    '    Button.AutoSize = True

    '    Return Button
    'End Function

    'Public Function FormatOptionsTextBox(
    '    ControlName As String,
    '    TextBoxText As String
    '    ) As TextBox

    '    Dim TextBox = New TextBox
    '    TextBox.Name = ControlName
    '    TextBox.Text = TextBoxText
    '    TextBox.Anchor = CType(AnchorStyles.Left + AnchorStyles.Right, AnchorStyles)
    '    'TextBox.AutoSize = True

    '    Return TextBox
    'End Function

    'Public Function FormatOptionsComboBox(
    '    ControlName As String,
    '    ComboBoxItems As List(Of String),
    '    DropDownStyleName As String
    '    ) As ComboBox

    '    ' DropDownStyleName: Simple | DropDown | DropDownList

    '    Dim ComboBox = New ComboBox
    '    ComboBox.Name = ControlName
    '    Dim ComboBoxItem As String
    '    Dim MaxCharacters As Integer = 0

    '    Select Case DropDownStyleName
    '        Case "Simple"
    '            ComboBox.DropDownStyle = ComboBoxStyle.Simple
    '        Case "DropDown"
    '            ComboBox.DropDownStyle = ComboBoxStyle.DropDown
    '        Case "DropDownList"
    '            ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
    '        Case Else
    '            Throw New ArgumentException(String.Format("DropDownStyleName '{0}' not recognized", DropDownStyleName))

    '    End Select

    '    For Each ComboBoxItem In ComboBoxItems
    '        ComboBox.Items.Add(ComboBoxItem)
    '        If Len(ComboBoxItem) > MaxCharacters Then MaxCharacters = Len(ComboBoxItem)
    '    Next
    '    ComboBox.Text = CStr(ComboBox.Items(0))

    '    ComboBox.Width = MaxCharacters * 15

    '    ComboBox.Anchor = CType(AnchorStyles.Left, AnchorStyles)

    '    Return ComboBox
    'End Function

    'Public Function FormatOptionsLabel(
    '    Name As String,
    '    LabelText As String
    '    ) As Label

    '    Dim Label As New Label

    '    Label.Name = Name
    '    Label.Text = LabelText
    '    Label.AutoSize = True
    '    Label.Anchor = CType(AnchorStyles.Left, AnchorStyles)

    '    Return Label
    'End Function


    'UTILITIES

    Public Sub SetColorFromCategory(Task As Task)

        Task.ColorSaturation = 0.2
        Task.ColorBrightness = 1

        Select Case Task.Category.ToLower
            Case "update"
                Task.ColorHue = "Green"
            Case "edit"
                Task.ColorHue = "Magenta"
            Case "restyle"
                Task.ColorHue = "Cyan"
            Case "check"
                Task.ColorHue = "Yellow"
            Case "output"
                Task.ColorHue = "Red"
            Case Else
                MsgBox(String.Format("Task '{0}' category '{1}' not recognized", Task.Name, Task.Category.ToLower))
        End Select

        SetRBGFromHSB(Task)
    End Sub

    Public Sub SetRBGFromHSB(Task As Task)
        Dim R As Integer = 0
        Dim G As Integer = 0
        Dim B As Integer = 0

        Select Case Task.ColorHue
            Case "Red"
                R = 255
                G = CInt(255 * (1 - Task.ColorSaturation))
                B = CInt(255 * (1 - Task.ColorSaturation))
            Case "Green"
                R = CInt(255 * (1 - Task.ColorSaturation))
                G = 255
                B = CInt(255 * (1 - Task.ColorSaturation))
            Case "Blue"
                R = CInt(255 * (1 - Task.ColorSaturation))
                G = CInt(255 * (1 - Task.ColorSaturation))
                B = 255
            Case "Cyan"
                R = CInt(255 * (1 - Task.ColorSaturation))
                G = 255
                B = 255
            Case "Magenta"
                R = 255
                G = CInt(255 * (1 - Task.ColorSaturation))
                B = 255
            Case "Yellow"
                R = 255
                G = 255
                B = CInt(255 * (1 - Task.ColorSaturation))
            Case "White"
                R = 255
                G = 255
                B = 255
        End Select

        R = CInt(R * Task.ColorBrightness)
        G = CInt(G * Task.ColorBrightness)
        B = CInt(B * Task.ColorBrightness)

        Task.ColorR = R
        Task.ColorG = G
        Task.ColorB = B

    End Sub

    Public Sub AddSupplementalErrorMessage(
        ByRef ExitStatus As Integer,
        ErrorMessageList As List(Of String),
        SupplementalErrorMessage As Dictionary(Of Integer, List(Of String))
        )

        Dim SupplementalExitStatus As Integer = SupplementalErrorMessage.Keys(0)

        If Not SupplementalExitStatus = 0 Then
            If SupplementalExitStatus > ExitStatus Then
                ExitStatus = SupplementalExitStatus
            End If
            For Each s As String In SupplementalErrorMessage(SupplementalExitStatus)
                ErrorMessageList.Add(s)
            Next
        End If
    End Sub

    Public Sub UpdateErrorMessageList(
        ErrorMessageList As List(Of String),
        ErrorMessage As String,
        TreatAsSubtask As Boolean,
        LabelText As String)

        Dim Indent As String = "    "

        If TreatAsSubtask Then
            If Not ErrorMessageList.Contains(LabelText) Then
                ErrorMessageList.Add(LabelText)
            End If
            ErrorMessageList.Add(String.Format("{0}{1}", Indent, ErrorMessage))
        Else
            ErrorMessageList.Add(ErrorMessage)
        End If


    End Sub

    Public Function GenerateLabelText() As String
        ' Scratch.TaskOpenSave -> Open save
        ' Housekeeper.TaskOpenSave -> Open save

        Dim InString As String
        Dim OutString As String = ""

        InString = Me.ToString
        InString = InString.Replace("Scratch.Task", "")  ' 'Scratch.TaskOpenSave' -> 'OpenSave'
        InString = InString.Replace("Housekeeper.Task", "")  ' 'Housekeeper.TaskOpenSave' -> 'OpenSave'

        OutString = InString(0)  ' '' -> 'O'
        InString = Right(InString, Len(InString) - 1)  ' 'OpenSave' -> 'penSave'

        For Each c As Char In InString
            If (Asc(c) >= 65) And (Asc(c) <= 90) Then  ' It's a capital letter
                ' Upper case.  Add a space and change the character to lower case.
                OutString = String.Format("{0} {1}", OutString, CStr(c).ToLower)
            Else
                ' Lower case.  Add the character as is.
                OutString = String.Format("{0}{1}", OutString, CStr(c))
            End If
        Next

        Return OutString
    End Function

    Public Function GenerateCtrlText(CtrlName As String) As String
        ' AutoOrient -> Auto orient
        ' PrintAsBlack -> Print as black

        Dim InString As String
        Dim OutString As String = ""

        InString = CtrlName

        OutString = InString(0)  ' '' -> 'O'
        InString = Right(InString, Len(InString) - 1)  ' 'OpenSave' -> 'penSave'

        For Each c As Char In InString
            If (Asc(c) >= 65) And (Asc(c) <= 90) Then  ' It's a capital letter
                ' Upper case.  Add a space and change the character to lower case.
                OutString = String.Format("{0} {1}", OutString, CStr(c).ToLower)
            Else
                ' Lower case.  Add the character as is.
                OutString = String.Format("{0}{1}", OutString, CStr(c))
            End If
        Next

        Return OutString
    End Function

    Public Function GenerateHelpURL(tmpLabelText As String) As String
        Dim s As String
        Dim tmpHelpURL As String

        s = tmpLabelText
        s = s.Replace("/", "")
        s = s.Replace(" ", "-")
        s = s.ToLower

        tmpHelpURL = String.Format("https://github.com/rmcanany/SolidEdgeHousekeeper#{0}", s)

        Return tmpHelpURL
    End Function

    Public Sub HandleHideOptionsChange(
        Task As Task,
        TaskOptionsTLP As ExTableLayoutPanel,
        HideOptionsCheckbox As CheckBox)

        'Dim Button = CType(Task.ControlsDict(BaseControlNames.Expand.ToString), Button)
        'Dim ButtonImage As Bitmap

        'If HideOptionsCheckbox.Checked Then
        '    ButtonImage = My.Resources.expand
        'Else
        '    ButtonImage = My.Resources.collapse
        'End If

        Task.AutoHideOptions = HideOptionsCheckbox.Checked

        'Me.TaskOptionsTLP.Visible = Not HideOptionsCheckbox.Checked

        'Button.Image = ButtonImage

    End Sub

    Public Sub HandleMutuallyExclusiveCheckBoxes(
        TLPOptions As ExTableLayoutPanel,
        NewlyCheckedCheckBox As CheckBox,
        ParticipatingCheckBoxes As List(Of CheckBox)
        )

        Dim ParticipatingCheckBox As CheckBox
        'Dim OtherCheckBox As CheckBox
        'Dim Ctrl As Control

        For Each ParticipatingCheckBox In ParticipatingCheckBoxes
            If Not ParticipatingCheckBox Is NewlyCheckedCheckBox Then
                ParticipatingCheckBox.Checked = False
            End If
        Next


    End Sub



    'FORM STATE

    Public Function GetFormState() As String
        Dim JSONString As String

        Dim tmpJSONDict As New Dictionary(Of String, String)
        Dim Ctrl As Control
        Dim CtrlName As String

        For Each CtrlName In ControlsDict.Keys
            Ctrl = ControlsDict(CtrlName)

            Select Case Ctrl.GetType
                Case GetType(CheckBox)
                    Dim c = CType(Ctrl, CheckBox)
                    tmpJSONDict(CtrlName) = CStr(c.Checked)

                Case GetType(TextBox)
                    Dim c = CType(Ctrl, TextBox)
                    tmpJSONDict(CtrlName) = c.Text

                Case GetType(ComboBox)
                    Dim c = CType(Ctrl, ComboBox)
                    tmpJSONDict(CtrlName) = c.Text

                Case GetType(Button)
                    ' Nothing to do here

                Case GetType(Label)
                    ' Nothing to do here

                Case GetType(ExTableLayoutPanel)
                    ' Nothing to do here

                Case Else
                    MsgBox(String.Format("{0} Control type '{1}' not recognized", "Task", Ctrl.GetType.ToString))

            End Select

        Next

        tmpJSONDict("TaskName") = Me.Name

        JSONString = JsonConvert.SerializeObject(tmpJSONDict)

        Return JSONString
    End Function

    Public Sub SetFormState(JSONString As String)

        ' Dictionary format
        '{
        '    "ShowCOG":"False",
        '    "HideCOG":"True",
        '    "HideOptions":"False",
        '    "SelectTask":"True",
        '    "SelectAssembly":"True",
        '    "SelectPart":"False",
        '    "SelectSheetmetal":"False",
        '    "SelectDraft":"False"
        '}

        Dim tmpJSONDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

        Dim Ctrl As Control
        Dim CtrlName As String

        Dim tf As Boolean

        For Each CtrlName In ControlsDict.Keys

            tf = CtrlName = BaseControlNames.SelectTask.ToString
            tf = tf Or CtrlName = BaseControlNames.SelectAssembly.ToString
            tf = tf Or CtrlName = BaseControlNames.SelectPart.ToString
            tf = tf Or CtrlName = BaseControlNames.SelectSheetmetal.ToString
            tf = tf Or CtrlName = BaseControlNames.SelectDraft.ToString
            tf = tf And Not RememberTaskSelections

            If tf Then
                Continue For
            End If

            If tmpJSONDict.Keys.Contains(CtrlName) Then

                Ctrl = ControlsDict(CtrlName)

                Select Case Ctrl.GetType

                    Case GetType(CheckBox)
                        Dim c = CType(Ctrl, CheckBox)
                        c.Checked = CBool(tmpJSONDict(CtrlName))

                    Case GetType(TextBox)
                        Dim c = CType(Ctrl, TextBox)
                        c.Text = tmpJSONDict(CtrlName)

                    Case GetType(ComboBox)
                        Dim c = CType(Ctrl, ComboBox)
                        Try
                            c.Text = tmpJSONDict(CtrlName)
                        Catch ex As Exception
                        End Try

                    Case Else
                        MsgBox(String.Format("{0} Control type '{1}' not recognized", "Task", Ctrl.GetType.ToString))

                End Select
            End If
        Next


    End Sub


End Class
