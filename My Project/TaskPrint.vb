Option Strict On

Public Class TaskPrint

    Inherits Task

    Public Property PrinterName As String
    Public Property Copies As Short
    Public Property ShowPrintingOptions As Boolean
    Public Property AutoOrient As Boolean
    Public Property BestFit As Boolean
    Public Property PrintAsBlack As Boolean
    Public Property ScaleLineTypes As Boolean
    Public Property ScaleLineWidths As Boolean
    Public Property SelectedSheets As List(Of String)

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
        HideOptions
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

        Me.SelectedSheets = New List(Of String)
    End Sub


    Public Overrides Function Process(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeFramework.SolidEdgeDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf ProcessInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Public Overrides Function Process(ByVal FileName As String) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Return ErrorMessage

    End Function

    Private Function ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim DraftPrinter As SolidEdgeDraft.DraftPrintUtility = Nothing

        Dim Sheets As New List(Of SolidEdgeDraft.Sheet)
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim SheetSetup As SolidEdgeDraft.SheetSetup
        Dim PaperSizeConstant As SolidEdgeDraft.PaperSizeConstants

        Dim TC As New Task_Common

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


        Sheets = TC.GetSheets(tmpSEDoc, "Working")
        For Each Sheet In Sheets
            SheetSetup = Sheet.SheetSetup
            PaperSizeConstant = SheetSetup.SheetSizeOption
            Try
                If SelectedSheets.Contains(PaperSizeConstant.ToString) Then
                    DraftPrinter.AddSheet(Sheet)
                    DraftPrinter.PrintOut()
                    SEApp.DoIdle()
                End If
            Catch ex As Exception
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Print drawing sheet {0} did not succeed", Sheet.Name))
            End Try
        Next

        DraftPrinter.RemoveAllDocuments()
        SEApp.DoIdle()

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

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

        'Dim PD As New PrinterDoctor
        Dim ComboBoxItems As List(Of String) = GetInstalledPrinterNames()

        Dim CtrlName As String
        Dim CtrlText As String

        Dim OptionList As New List(Of String)

        'Dim IU As New InterfaceUtilities

        FormatTLPOptionsEx(tmpTLPOptions, "TLPOptions", 10, 100, 200)

        RowIndex = 0

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


        'RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.HideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function


    Public Overrides Function CheckStartConditions(
        PriorErrorMessage As Dictionary(Of Integer, List(Of String))
        ) As Dictionary(Of Integer, List(Of String))

        Dim PriorExitStatus As Integer = PriorErrorMessage.Keys(0)

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim ExitStatus As Integer = 0
        Dim ErrorMessageList = PriorErrorMessage(PriorExitStatus)
        Dim Indent = "    "

        If Me.IsSelectedTask Then
            ' Check start conditions.
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one type of file to process", Indent))
            End If

            If SelectedSheets.Count = 0 Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one sheet size to print", Indent))
            End If

            Try
                Me.Copies = CShort(ControlsDict(ControlNames.Copies.ToString).Text)
                If Not Me.Copies > 0 Then
                    If Not ErrorMessageList.Contains(Me.Description) Then
                        ErrorMessageList.Add(Me.Description)
                    End If
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("{0}Enter a number of copies > 0", Indent))
                End If
            Catch ex As Exception
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Enter a valid number of copies", Indent))
            End Try

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    'UTILITIES

    Public Function GetInstalledPrinterNames() As List(Of String)
        Dim PrinterList As New List(Of String)
        Dim InstalledPrinter As String
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
                If Name.ToLower.Contains("iso a") Then
                    SheetSizeList.Add(Name)
                End If
            End If

            If Filter.ToLower = "all" Then
                If Not Name.ToLower.Contains("custom") Then
                    SheetSizeList.Add(Name)
                End If
            End If
        Next

        Return SheetSizeList
    End Function


    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox

        Select Case Name

            Case ControlNames.SelectSheets.ToString
                Dim FSS As New FormSheetSelector(Me)

                FSS.ShowSheetSelector()

                If FSS.DialogResult = DialogResult.OK Then

                    Me.SelectedSheets = FSS.SelectedSheets

                    TextBox = CType(ControlsDict(ControlNames.SelectedSheets.ToString), TextBox)
                    TextBox.Text = ""
                    For Each SheetSize As String In Me.SelectedSheets
                        If TextBox.Text = "" Then
                            TextBox.Text = SheetSize
                        Else
                            TextBox.Text = String.Format("{0} {1}", TextBox.Text, SheetSize)
                        End If
                    Next

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

            Case ControlNames.HideOptions.ToString
                HandleHideOptionsChange(Me, Me.TaskOptionsTLP, Checkbox)

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
                If Not TextBox.Text = "" Then
                    SelectedSheets = Split(TextBox.Text, " ").ToList
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Prints drawings. "
        HelpString += vbCrLf + vbCrLf + "![Printer_Setup](My%20Project/media/print.png)"
        HelpString += vbCrLf + vbCrLf + "The dropdown should list all installed printers. "
        HelpString += vbCrLf + vbCrLf + "If you use more than one printer, use `Edit task list` to add one or more Print tasks. "
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
