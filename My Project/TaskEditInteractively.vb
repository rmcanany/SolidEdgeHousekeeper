Option Strict On
Imports System.Reflection
'Imports Microsoft.WindowsAPICodePack.Dialogs
'Imports SolidEdgeAssembly
'Imports SolidEdgeFramework
'Imports SolidEdgePart

Public Class TaskEditInteractively

    Inherits Task

    Public Property FormX As Integer
    Public Property FormY As Integer
    Public Property UseCountdownTimer As Boolean
    Public Property PauseTime As Double
    Public Property SaveAfterTimeout As Boolean
    Public Property RunCommands As Boolean
    Public Property CommandIDAssembly As Integer
    Public Property CommandIDPart As Integer
    Public Property CommandIDSheetmetal As Integer
    Public Property CommandIDDraft As Integer

    Private Property CommandDict As Dictionary(Of String, Dictionary(Of String, Integer))

    Enum ControlNames
        FormX
        FormXLabel
        FormY
        FormYLabel
        UseCountdownTimer
        UseCountdownTimeLabel
        CountdownTime
        SaveAfterTimeout
        RunCommands
        SelectedCommand
        SelectedCommandLabel
        StartCommandAssembly
        StartCommandAssemblyLabel
        StartCommandPart
        StartCommandPartLabel
        StartCommandSheetmetal
        StartCommandSheetmetalLabel
        StartCommandDraft
        StartCommandDraftLabel
        HideOptions
    End Enum


    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = False
        Me.AppliesToAssembly = True
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskEditInteractively
        Me.Category = "Edit"
        SetColorFromCategory(Me)

        CommandDict = GenerateCommandDict()

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.FormX = 0
        Me.FormY = 0
        Me.UseCountdownTimer = False
        Me.PauseTime = 0
        Me.SaveAfterTimeout = False
        Me.RunCommands = False
        Me.CommandIDAssembly = 0
        Me.CommandIDPart = 0
        Me.CommandIDSheetmetal = 0
        Me.CommandIDDraft = 0



    End Sub

    Private Function GenerateCommandDict() As Dictionary(Of String, Dictionary(Of String, Integer))

        Dim CommandDict As New Dictionary(Of String, Dictionary(Of String, Integer))

        Dim PU As New PreferencesUtilities

        Dim EditInteractivelyCommandsFilename = String.Format("{0}\EditInteractivelyCommands.txt", PU.GetPreferencesDirectory)

        If Not FileIO.FileSystem.FileExists(EditInteractivelyCommandsFilename) Then
            PU.CreateEditInteractivelyCommands()
        End If

        Dim CommandList As String()
        CommandList = IO.File.ReadAllLines(EditInteractivelyCommandsFilename)

        For Each Item As String In CommandList
            Dim Line As String = Item.Trim

            If Len(Line) = 0 Then
                Continue For
            End If

            If Line(0) = "'" Then
                Continue For
            End If

            Dim Linelist = Line.Split(","c)

            Dim Description = Linelist(0).Trim
            Dim ACmd = Linelist(1).Trim
            Dim PCmd = Linelist(2).Trim
            Dim SCmd = Linelist(3).Trim
            Dim DCmd = Linelist(4).Trim

            CommandDict(Description) = New Dictionary(Of String, Integer)
            CommandDict(Description)("Assembly") = CInt(ACmd)
            CommandDict(Description)("Part") = CInt(PCmd)
            CommandDict(Description)("Sheetmetal") = CInt(SCmd)
            CommandDict(Description)("Draft") = CInt(DCmd)
        Next

        Return CommandDict

    End Function

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

        SEApp.DisplayAlerts = True

        Dim TC As New Task_Common

        Dim FEI As New FormEditInteractively

        FEI.Left = Me.FormX
        FEI.Top = Me.FormY
        FEI.UseCountdownTimer = Me.UseCountdownTimer
        FEI.PauseTime = Me.PauseTime
        FEI.RunCommands = Me.RunCommands
        FEI.CommandIDAssembly = Me.CommandIDAssembly
        FEI.CommandIDPart = Me.CommandIDPart
        FEI.CommandIDSheetmetal = Me.CommandIDSheetmetal
        FEI.CommandIDDraft = Me.CommandIDDraft
        FEI.Filetype = TC.GetDocType(SEDoc)
        FEI.SEApp = SEApp
        FEI.SaveAfterTimeout = Me.SaveAfterTimeout

        Dim Result As DialogResult = FEI.ShowDialog()

        'MsgBox(Result.ToString)

        If Result = DialogResult.Yes Then
            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save read-only file.")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        ElseIf Result = DialogResult.No Then
            ' Nothing to do here
        ElseIf Result = DialogResult.Abort Then
            ExitStatus = 99
            ErrorMessageList.Add("Operation was cancelled.")
        End If

        SEApp.DisplayAlerts = False

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
        Dim ControlWidth As Integer = 125

        Dim ComboBoxItems As New List(Of String)

        For Each CommandName In CommandDict.Keys
            ComboBoxItems.Add(CommandName)
        Next

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        TextBox = FormatOptionsTextBox(ControlNames.FormX.ToString, "0")
        TextBox.Width = ControlWidth
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        Label = FormatOptionsLabel(ControlNames.FormXLabel.ToString, "Dialog X (pixels from left)")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.FormY.ToString, "0")
        TextBox.Width = ControlWidth
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        Label = FormatOptionsLabel(ControlNames.FormYLabel.ToString, "Dialog Y (pixels from top)")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UseCountdownTimer.ToString, "Use countdown timer")
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.CountdownTime.ToString, "0")
        TextBox.Width = ControlWidth
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        Label = FormatOptionsLabel(ControlNames.UseCountdownTimeLabel.ToString, "Time (s)")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label
        Label.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.SaveAfterTimeout.ToString, "Save file after timeout")
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.RunCommands.ToString, "Start command")
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        ComboBox = FormatOptionsComboBox(ControlNames.SelectedCommand.ToString, ComboBoxItems, "DropDownList")
        ComboBox.Width = ControlWidth
        AddHandler ComboBox.SelectedIndexChanged, AddressOf ComboBoxOptions_SelectedIndexChanged
        tmpTLPOptions.Controls.Add(ComboBox, 0, RowIndex)
        ControlsDict(ComboBox.Name) = ComboBox
        ComboBox.Visible = False

        Label = FormatOptionsLabel(ControlNames.SelectedCommandLabel.ToString, "Command choices")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label
        Label.Visible = False

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.StartCommandAssembly.ToString, "0")
        TextBox.Width = ControlWidth
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        Label = FormatOptionsLabel(ControlNames.StartCommandAssemblyLabel.ToString, "Assembly command ID")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label
        Label.Visible = False

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.StartCommandPart.ToString, "0")
        TextBox.Width = ControlWidth
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        Label = FormatOptionsLabel(ControlNames.StartCommandPartLabel.ToString, "Part command ID")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label
        Label.Visible = False

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.StartCommandSheetmetal.ToString, "0")
        TextBox.Width = ControlWidth
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        Label = FormatOptionsLabel(ControlNames.StartCommandSheetmetalLabel.ToString, "Sheetmetal command ID")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label
        Label.Visible = False

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.StartCommandDraft.ToString, "0")
        TextBox.Width = ControlWidth
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        Label = FormatOptionsLabel(ControlNames.StartCommandDraftLabel.ToString, "Draft command ID")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label
        Label.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.HideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
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

            Dim i As Integer
            Dim s As String

            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one type of file to process", Indent))
            End If

            If (Me.UseCountdownTimer And Me.PauseTime <= 0) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Countdown time must be > 0", Indent))
            End If

            If Me.RunCommands Then
                Try
                    i = CInt(Me.CommandIDAssembly)
                    i = CInt(Me.CommandIDPart)
                    i = CInt(Me.CommandIDSheetmetal)
                    i = CInt(Me.CommandIDDraft)
                Catch ex As Exception
                    If Not ErrorMessageList.Contains(Me.Description) Then
                        ErrorMessageList.Add(Me.Description)
                    End If
                    ExitStatus = 1
                    s = String.Format("{0}All command IDs must be an integer", Indent)
                    ErrorMessageList.Add(s)
                End Try

            End If

            Try
                i = CInt(FormX)
                i = CInt(FormY)
                If Me.FormX < 0 Or Me.FormY < 0 Then
                    i = CInt("forty-two")
                End If
            Catch ex As Exception
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                s = String.Format("{0}Both dialog locations must be integers >= 0", Indent)
                s = String.Format("{0}, preferably < the screen size (in pixels)", s)
                ErrorMessageList.Add(s)

            End Try

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    Public Sub ComboBoxOptions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Dim ComboBox = CType(sender, ComboBox)
        Dim Name = ComboBox.Name

        Select Case Name
            Case ControlNames.SelectedCommand.ToString
                Dim Command = ComboBox.Text

                Me.CommandIDAssembly = CommandDict(Command)("Assembly")
                CType(ControlsDict(ControlNames.StartCommandAssembly.ToString), TextBox).Text = CStr(Me.CommandIDAssembly)

                Me.CommandIDPart = CommandDict(Command)("Part")
                CType(ControlsDict(ControlNames.StartCommandPart.ToString), TextBox).Text = CStr(Me.CommandIDPart)

                Me.CommandIDSheetmetal = CommandDict(Command)("Sheetmetal")
                CType(ControlsDict(ControlNames.StartCommandSheetmetal.ToString), TextBox).Text = CStr(Me.CommandIDSheetmetal)

                Me.CommandIDDraft = CommandDict(Command)("Draft")
                CType(ControlsDict(ControlNames.StartCommandDraft.ToString), TextBox).Text = CStr(Me.CommandIDDraft)


            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name
            Case ControlNames.HideOptions.ToString
                HandleHideOptionsChange(Me, Me.TaskOptionsTLP, Checkbox)

            Case ControlNames.UseCountdownTimer.ToString
                Me.UseCountdownTimer = Checkbox.Checked

                CType(ControlsDict(ControlNames.CountdownTime.ToString), TextBox).Visible = Checkbox.Checked
                CType(ControlsDict(ControlNames.UseCountdownTimeLabel.ToString), Label).Visible = Checkbox.Checked
                CType(ControlsDict(ControlNames.SaveAfterTimeout.ToString), CheckBox).Visible = Checkbox.Checked

            Case ControlNames.RunCommands.ToString
                Me.RunCommands = Checkbox.Checked

                CType(ControlsDict(ControlNames.SelectedCommand.ToString), ComboBox).Visible = Checkbox.Checked
                CType(ControlsDict(ControlNames.SelectedCommandLabel.ToString), Label).Visible = Checkbox.Checked

                CType(ControlsDict(ControlNames.StartCommandAssembly.ToString), TextBox).Visible = Checkbox.Checked
                CType(ControlsDict(ControlNames.StartCommandAssemblyLabel.ToString), Label).Visible = Checkbox.Checked

                CType(ControlsDict(ControlNames.StartCommandPart.ToString), TextBox).Visible = Checkbox.Checked
                CType(ControlsDict(ControlNames.StartCommandPartLabel.ToString), Label).Visible = Checkbox.Checked

                CType(ControlsDict(ControlNames.StartCommandSheetmetal.ToString), TextBox).Visible = Checkbox.Checked
                CType(ControlsDict(ControlNames.StartCommandSheetmetalLabel.ToString), Label).Visible = Checkbox.Checked

                CType(ControlsDict(ControlNames.StartCommandDraft.ToString), TextBox).Visible = Checkbox.Checked
                CType(ControlsDict(ControlNames.StartCommandDraftLabel.ToString), Label).Visible = Checkbox.Checked

            Case ControlNames.SaveAfterTimeout.ToString
                Me.SaveAfterTimeout = Checkbox.Checked

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name
            Case ControlNames.FormX.ToString
                Try
                    Me.FormX = CInt(TextBox.Text)
                Catch ex As Exception
                End Try

            Case ControlNames.FormY.ToString
                Try
                    Me.FormY = CInt(TextBox.Text)
                Catch ex As Exception
                End Try

            Case ControlNames.CountdownTime.ToString
                Try
                    Me.PauseTime = CDbl(TextBox.Text)
                Catch ex As Exception
                End Try

            Case ControlNames.StartCommandAssembly.ToString
                Try
                    Me.CommandIDAssembly = CInt(TextBox.Text)
                Catch ex As Exception
                End Try

            Case ControlNames.StartCommandPart.ToString
                Try
                    Me.CommandIDPart = CInt(TextBox.Text)
                Catch ex As Exception
                End Try

            Case ControlNames.StartCommandSheetmetal.ToString
                Try
                    Me.CommandIDSheetmetal = CInt(TextBox.Text)
                Catch ex As Exception
                End Try

            Case ControlNames.StartCommandDraft.ToString
                Try
                    Me.CommandIDDraft = CInt(TextBox.Text)
                Catch ex As Exception
                End Try

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub



    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Brings up files one at a time for manual processing. "
        HelpString += "A dialog box lets you tell Housekeeper when you are done. "
        HelpString += "You can save the file or not, or choose to abort.  "
        HelpString += "Aborting stops processing and returns you to the Housekeeper main form.  "

        HelpString += vbCrLf + vbCrLf + "![Edit Interactively Dialog](My%20Project/media/edit_interactively_dialog.png)"

        HelpString += vbCrLf + vbCrLf + "As shown below, you choose where the dialog box is placed on the screen. "
        HelpString += "The `X` and `Y` values are the number of pixels from the left and top of the screen, respectively. "
        HelpString += "You can optionally set a countdown timer and/or start a command. "

        HelpString += vbCrLf + vbCrLf + "![Edit Interactively Options](My%20Project/media/edit_interactively.png)"

        HelpString += vbCrLf + vbCrLf + "The countdown timer lets you run hands-free. "
        HelpString += "This can be handy for doing a quick inspection of a batch of files, for example. "
        HelpString += "If something catches your eye, you can pause the timer.  "
        HelpString += "There is an option, `Save file after timeout`, that tells Housekeeper how to proceed if the timer runs to completion.  "

        HelpString += vbCrLf + vbCrLf + "The `Start command` option launches a command when the file opens.  "
        HelpString += "This can help keep you on track when you have a small chore to complete on a bunch of files.  "
        HelpString += "For example, activating the `Update file on save` option on the `Physical Properties` dialog.  "

        HelpString += vbCrLf + vbCrLf + "The dropdown list contains commands that we thought might be useful.  "
        HelpString += "The first entry on the list, `Manual entry` is a special case.  "
        HelpString += "It instructs the program to execute the command id entered in the textboxes below the dropdown. "
        HelpString += "If you don't want a command to start for a given file type, enter `0` in the textbox. "

        HelpString += vbCrLf + vbCrLf + "You can customize the list.  Instructions to do so are in the file "
        HelpString += "`EditInteractivelyCommands.txt` in the Housekeeper `Preferences` directory. "
        HelpString += "Note, you have to run this command one time to create the file. "
        HelpString += "That file also shows how to find commands and their corresponding ID numbers. "
        HelpString += "Hundreds of commands are available.  It's worth checking out. "

        HelpString += vbCrLf + vbCrLf + "Some rules for interactive editing apply. "
        HelpString += "It is important to leave Solid Edge in the state you found it when the file was opened. "
        HelpString += "For example, if you open another file, such as a drawing, you need to close it. "
        HelpString += "If you add or modify a feature, you need to click Finish. "
        HelpString += "If you used the `Start command` option, you need to close any dialog opened in the process. "

        HelpString += vbCrLf + vbCrLf + "Also, do not `Close` or `Save As` the file being processed. "
        HelpString += "Housekeeper maintains a `reference` to the file. "
        HelpString += "Those two commands cause the reference to be lost, resulting in an error. "

        HelpString += vbCrLf + vbCrLf + "One last thing.  Macros interact with Solid Edge through something called the Windows Component Object Model.  "
        HelpString += "That framework appears to have some sort of built-in inactivity detection.  "
        HelpString += "If you let this command sit idle for a period of time, COM reports an error. "
        HelpString += "It doesn't really hurt anything, but Housekeeper stops and restarts SE any time a COM error occurs. "
        HelpString += "I get around it by selecting only a small number of files to work on at a time. "

        Return HelpString
    End Function


End Class
