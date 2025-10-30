Option Strict On
Imports System.Security.AccessControl
Imports FastColoredTextBoxNS

Public Class TaskPrint

    Inherits Task

    Private _PrinterName As String
    Public Property PrinterName As String
        Get
            Return _PrinterName
        End Get
        Set(value As String)
            _PrinterName = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.PrinterName.ToString), ComboBox).Text = value
            End If
        End Set
    End Property

    Private _Copies As Short
    Public Property Copies As Short
        Get
            Return _Copies
        End Get
        Set(value As Short)
            _Copies = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.Copies.ToString), TextBox).Text = CStr(value)
            End If
        End Set
    End Property

    Private _SelectedSheets As String
    Public Property SelectedSheets As String
        Get
            Return _SelectedSheets
        End Get
        Set(value As String)
            _SelectedSheets = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.SelectedSheets.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _ShowPrintingOptions As Boolean
    Public Property ShowPrintingOptions As Boolean
        Get
            Return _ShowPrintingOptions
        End Get
        Set(value As Boolean)
            _ShowPrintingOptions = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ShowPrintingOptions.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _AutoOrient As Boolean
    Public Property AutoOrient As Boolean
        Get
            Return _AutoOrient
        End Get
        Set(value As Boolean)
            _AutoOrient = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AutoOrient.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _BestFit As Boolean
    Public Property BestFit As Boolean
        Get
            Return _BestFit
        End Get
        Set(value As Boolean)
            _BestFit = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BestFit.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _PrintAsBlack As Boolean
    Public Property PrintAsBlack As Boolean
        Get
            Return _PrintAsBlack
        End Get
        Set(value As Boolean)
            _PrintAsBlack = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.PrintAsBlack.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _ScaleLineTypes As Boolean
    Public Property ScaleLineTypes As Boolean
        Get
            Return _ScaleLineTypes
        End Get
        Set(value As Boolean)
            _ScaleLineTypes = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ScaleLineTypes.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _ScaleLineWidths As Boolean
    Public Property ScaleLineWidths As Boolean
        Get
            Return _ScaleLineWidths
        End Get
        Set(value As Boolean)
            _ScaleLineWidths = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ScaleLineWidths.ToString), CheckBox).Checked = value
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

    Public Property SelectedSheetsList As List(Of String)
    Public Property CustomUnits As String
    Public Property CustomXMin As Double
    Public Property CustomXMax As Double
    Public Property CustomYMin As Double
    Public Property CustomYMax As Double


    Enum ControlNames
        PrinterName
        PrinterNameLabel
        Copies
        CopiesLabel
        ShowPrintingOptions
        AutoOrient
        BestFit
        PrintAsBlack
        ScaleLineTypes
        ScaleLineWidths
        SelectSheets
        SelectedSheets
        AutoHideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = False
        Me.AppliesToAssembly = False
        Me.AppliesToPart = False
        Me.AppliesToSheetmetal = False
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskPrint
        Me.Category = "Output"
        SetColorFromCategory(Me)
        Me.CompatibleWithInactiveDraft = True

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.PrinterName = ""
        Me.Copies = 1
        Me.AutoOrient = False
        Me.BestFit = False
        Me.PrintAsBlack = False
        Me.ScaleLineTypes = False
        Me.ScaleLineWidths = False
        Me.CustomUnits = "in"
        Me.CustomXMin = 0
        Me.CustomXMax = 0
        Me.CustomYMin = 0
        Me.CustomYMax = 0

        Me.SelectedSheetsList = New List(Of String)
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
    End Sub

    Private Sub ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        Dim DraftPrinter As SolidEdgeDraft.DraftPrintUtility = Nothing

        Dim Sheets As New List(Of SolidEdgeDraft.Sheet)
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim SheetSetup As SolidEdgeDraft.SheetSetup
        Dim PaperSizeConstant As SolidEdgeDraft.PaperSizeConstants

        Dim UC As New UtilsCommon

        Dim tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

        DraftPrinter = CType(SEApp.GetDraftPrintUtility(), SolidEdgeDraft.DraftPrintUtility)
        DraftPrinter.Units = SolidEdgeDraft.DraftPrintUnitsConstants.igDraftPrintInches
        DraftPrinter.Printer = Me.PrinterName
        DraftPrinter.Copies = Me.Copies
        DraftPrinter.AutoOrient = Me.AutoOrient
        DraftPrinter.BestFit = Me.BestFit
        DraftPrinter.PrintAsBlack = Me.PrintAsBlack
        DraftPrinter.ScaleLineTypes = Me.ScaleLineTypes
        DraftPrinter.ScaleLineWidths = Me.ScaleLineWidths


        Sheets = UC.GetSheets(tmpSEDoc, "Working")
        For Each Sheet In Sheets
            SheetSetup = Sheet.SheetSetup
            PaperSizeConstant = SheetSetup.SheetSizeOption
            Try
                If SelectedSheetsList.Contains(PaperSizeConstant.ToString) Then
                    If Not PaperSizeConstant.ToString.ToLower.Contains("custom") Then
                        DraftPrinter.AddSheet(Sheet)
                        DraftPrinter.PrintOut()
                        SEApp.DoIdle()
                    Else
                        Dim W As Double = SheetSetup.SheetWidth
                        Dim H As Double = SheetSetup.SheetHeight
                        If Me.CustomUnits = "mm" Then
                            W = 1000 * W
                            H = 1000 * H
                        Else
                            W = 1000 * W / 25.4
                            H = 1000 * H / 25.4
                        End If
                        Dim tf As Boolean = True
                        tf = tf And W >= Me.CustomXMin
                        tf = tf And W <= Me.CustomXMax
                        tf = tf And H >= Me.CustomYMin
                        tf = tf And H <= Me.CustomYMax
                        If tf Then
                            DraftPrinter.AddSheet(Sheet)
                            DraftPrinter.PrintOut()
                            SEApp.DoIdle()
                        End If
                    End If
                End If
            Catch ex As Exception
                'If PaperSizeConstant.ToString.ToLower.Contains("custom") Then
                '    TaskLogger.AddMessage($"PaperSizeConstant '{PaperSizeConstant.ToString}', X '{}', Y '{}'")
                'End If
                TaskLogger.AddMessage(String.Format("Print drawing sheet {0} did not succeed", Sheet.Name))
            End Try
        Next

        DraftPrinter.RemoveAllDocuments()
        SEApp.DoIdle()

    End Sub


    Public Function GetInstalledPrinterNames() As List(Of String)
        Dim PrinterList As New List(Of String)
        Dim InstalledPrinter As String

        PrinterList.Add("")
        For Each InstalledPrinter In System.Drawing.Printing.PrinterSettings.InstalledPrinters
            PrinterList.Add(InstalledPrinter)
        Next InstalledPrinter

        Return PrinterList
    End Function

    Public Function ConstantNameToDisplayName(ConstantName As String) As String
        Dim DisplayName As String
        DisplayName = ConstantName
        DisplayName = DisplayName.Replace("ig", "")
        DisplayName = DisplayName.Replace("Eng", "Eng ")
        DisplayName = DisplayName.Replace("Ansi", "Ansi ")
        DisplayName = DisplayName.Replace("Iso", "Iso ")
        DisplayName = DisplayName.Replace("Tall", " Tall")
        DisplayName = DisplayName.Replace("Wide", " Wide")

        Return DisplayName
    End Function

    Public Function DisplayNameToConstantName(DisplayName As String) As String
        Dim ConstantName As String

        ConstantName = String.Format("ig{0}", DisplayName.Replace(" ", ""))

        Return ConstantName
    End Function

    Public Function GetSheetSizes(Filter As String) As List(Of String)
        Dim SheetSizeList As New List(Of String)
        Dim PaperSizeConstant As SolidEdgeDraft.PaperSizeConstants
        Dim Name As String = ""

        For Each PaperSizeConstant In System.Enum.GetValues(GetType(SolidEdgeDraft.PaperSizeConstants))
            Name = PaperSizeConstant.ToString

            If Filter.ToLower = "ansi" Then
                If Name.ToLower.Contains("ansi") Then
                    SheetSizeList.Add(Name)
                End If
            End If

            If Filter.ToLower = "iso" Then
                If Name.ToLower.Contains("isoa") Then
                    SheetSizeList.Add(Name)
                End If
            End If

            If Filter.ToLower = "all" Then
                SheetSizeList.Add(Name)
                'If Not Name.ToLower.Contains("custom") Then
                '    SheetSizeList.Add(Name)
                'End If
            End If
        Next

        Return SheetSizeList
    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim ComboBox As ComboBox
        Dim TextBox As TextBox
        Dim Label As Label
        Dim Button As Button
        Dim ControlWidth As Integer = 200
        Dim NewFileTypeLabelText = ""

        Dim ComboBoxItems As List(Of String) = GetInstalledPrinterNames()

        Dim CtrlName As String
        Dim CtrlText As String

        Dim OptionList As New List(Of String)

        FormatTLPOptionsEx(tmpTLPOptions, "TLPOptions", 10, 100, 200)

        RowIndex = 0

        Label = FormatOptionsLabel(ControlNames.CopiesLabel.ToString, "Select Printer")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        ComboBox = FormatOptionsComboBox(ControlNames.PrinterName.ToString, ComboBoxItems, "DropDownList")
        ComboBox.Anchor = CType(AnchorStyles.Left + AnchorStyles.Right, AnchorStyles)
        AddHandler ComboBox.SelectedIndexChanged, AddressOf ComboBoxOptions_SelectedIndexChanged
        tmpTLPOptions.Controls.Add(ComboBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(ComboBox, 3)
        ControlsDict(ComboBox.Name) = ComboBox

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.Copies.ToString, "1")
        TextBox.Anchor = AnchorStyles.Left
        TextBox.Width = 100
        TextBox.TextAlign = HorizontalAlignment.Right
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_TextChanged
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        Label = FormatOptionsLabel(ControlNames.CopiesLabel.ToString, "Copies")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.SelectSheets.ToString, "Select Sheets")
        Button.AutoSize = False
        Button.Width = 100
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.SelectedSheets.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_TextChanged
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        tmpTLPOptions.SetColumnSpan(TextBox, 2)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ShowPrintingOptions.ToString, "Show printing options")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        OptionList.Add(ControlNames.AutoOrient.ToString)
        OptionList.Add(ControlNames.BestFit.ToString)
        OptionList.Add(ControlNames.PrintAsBlack.ToString)
        OptionList.Add(ControlNames.ScaleLineTypes.ToString)
        OptionList.Add(ControlNames.ScaleLineWidths.ToString)

        For Each s As String In OptionList
            CtrlName = s
            CtrlText = GenerateCtrlText(s)

            CheckBox = FormatOptionsCheckBox(CtrlName, CtrlText)
            AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
            tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
            tmpTLPOptions.SetColumnSpan(CheckBox, 3)
            CheckBox.Visible = False
            ControlsDict(CheckBox.Name) = CheckBox

            RowIndex += 1
        Next

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If

            If SelectedSheetsList.Count = 0 Then
                ErrorLogger.AddMessage("Select at least one sheet size to print")
            End If

            Try
                Me.Copies = CShort(ControlsDict(ControlNames.Copies.ToString).Text)
                If Not Me.Copies > 0 Then
                    ErrorLogger.AddMessage("Enter a number of copies > 0")
                End If
            Catch ex As Exception
                ErrorLogger.AddMessage("Enter a valid number of copies")
            End Try
        End If

    End Sub


    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox

        Select Case Name

            Case ControlNames.SelectSheets.ToString
                Dim FSS As New FormSheetSelector(Me)

                Dim Result As DialogResult = FSS.ShowDialog()

                If Result = DialogResult.OK Then

                    'Me.SelectedSheetsList = FSS.SelectedSheets  ' Handled in FSS along with custom size ranges

                    ' Update textbox
                    Dim s = ""
                    For Each SheetSize As String In Me.SelectedSheetsList
                        If s = "" Then ' First item
                            s = SheetSize
                        Else
                            s = String.Format("{0} {1}", s, SheetSize)
                        End If
                    Next
                    TextBox = CType(ControlsDict(ControlNames.SelectedSheets.ToString), TextBox)
                    TextBox.Text = s

                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select


    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.ShowPrintingOptions.ToString
                Me.ShowPrintingOptions = Checkbox.Checked

                CType(ControlsDict(ControlNames.AutoOrient.ToString), CheckBox).Visible = Me.ShowPrintingOptions
                CType(ControlsDict(ControlNames.BestFit.ToString), CheckBox).Visible = Me.ShowPrintingOptions
                CType(ControlsDict(ControlNames.PrintAsBlack.ToString), CheckBox).Visible = Me.ShowPrintingOptions
                CType(ControlsDict(ControlNames.ScaleLineTypes.ToString), CheckBox).Visible = Me.ShowPrintingOptions
                CType(ControlsDict(ControlNames.ScaleLineWidths.ToString), CheckBox).Visible = Me.ShowPrintingOptions

            Case ControlNames.AutoOrient.ToString
                Me.AutoOrient = Checkbox.Checked
            Case ControlNames.BestFit.ToString
                Me.BestFit = Checkbox.Checked
            Case ControlNames.PrintAsBlack.ToString
                Me.PrintAsBlack = Checkbox.Checked
            Case ControlNames.ScaleLineTypes.ToString
                Me.ScaleLineTypes = Checkbox.Checked
            Case ControlNames.ScaleLineWidths.ToString
                Me.ScaleLineWidths = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select

    End Sub

    Public Sub ComboBoxOptions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Dim ComboBox = CType(sender, ComboBox)
        Dim Name = ComboBox.Name

        Select Case Name
            Case ControlNames.PrinterName.ToString
                Me.PrinterName = ComboBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select

    End Sub

    Public Sub TextBoxOptions_TextChanged(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name
        Dim i As Short

        Select Case Name
            Case ControlNames.Copies.ToString
                Try
                    i = CShort(TextBox.Text)
                    Me.Copies = i
                Catch ex As Exception
                End Try

            Case ControlNames.SelectedSheets.ToString
                Me.SelectedSheets = TextBox.Text
                If Not TextBox.Text = "" Then
                    SelectedSheetsList = Split(TextBox.Text, " ").ToList
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Prints drawings. "

        HelpString += vbCrLf + vbCrLf + "![Print](My%20Project/media/task_print.png)"

        HelpString += vbCrLf + vbCrLf + "The dropdown should list all installed printers. "

        HelpString += vbCrLf + vbCrLf + "If you use more than one printer, "
        HelpString += "use the `Edit task list` function on the **Task Tab** to add one or more Print tasks. "
        HelpString += "Set up each by selecting the printer/plotter, sheet sizes, and other options as desired. "

        HelpString += vbCrLf + vbCrLf + "You assign sheet sizes to a printer with the `Select Sheets` button. "
        HelpString += "Print jobs are routed on a per-sheet basis. "
        HelpString += "So if a drawing has some sheets that need a printer and others that need a plotter, it will do what you expect. "

        HelpString += vbCrLf + vbCrLf + "![Printer_Setup](My%20Project/media/sheet_selector.png)"

        HelpString += vbCrLf + vbCrLf + "This command may not work with PDF printers. "
        HelpString += "Try the Save As PDF command instead. "

        Return HelpString
    End Function


End Class
