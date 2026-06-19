Option Strict On

'Imports FastColoredTextBoxNS
'Imports System.Collections.ArrayList
'Imports Microsoft.VisualBasic
'Imports System.Linq

Public Class TaskRunExternalProgram

    Inherits Task

    Private _ExternalProgram As String
    Public Property ExternalProgram As String
        Get
            Return _ExternalProgram
        End Get
        Set(value As String)
            _ExternalProgram = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ExternalProgram.ToString), TextBox).Text = value

                'If value IsNot Nothing AndAlso Not value = "" Then
                '    Dim tf As Boolean = IO.Path.GetExtension(value) = ".snp"
                '    CType(ControlsDict(ControlNames.EditSnippet.ToString), Button).Visible = tf
                'End If
            End If
        End Set
    End Property

    Private _HideConsoleWindow As Boolean
    Public Property HideConsoleWindow As Boolean
        Get
            Return _HideConsoleWindow
        End Get
        Set(value As Boolean)
            _HideConsoleWindow = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.HideConsoleWindow.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _DeleteTempFiles As Boolean
    Public Property DeleteTempFiles As Boolean
        Get
            Return _DeleteTempFiles
        End Get
        Set(value As Boolean)
            _DeleteTempFiles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DeleteTempFiles.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _SaveAfterProcessing As Boolean
    Public Property SaveAfterProcessing As Boolean
        Get
            Return _SaveAfterProcessing
        End Get
        Set(value As Boolean)
            _SaveAfterProcessing = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.SaveAfterProcessing.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    'Private _UseLocalPowershell As Boolean
    'Public Property UseLocalPowershell As Boolean
    '    Get
    '        Return _UseLocalPowershell
    '    End Get
    '    Set(value As Boolean)
    '        _UseLocalPowershell = value
    '        If Me.TaskOptionsTLP IsNot Nothing Then
    '            CType(ControlsDict(ControlNames.UseLocalPowershell.ToString), CheckBox).Checked = value
    '        End If
    '    End Set
    'End Property

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


    Enum ControlNames
        Browse
        ExternalProgram
        EditSnippet
        HideConsoleWindow
        DeleteTempFiles
        SaveAfterProcessing
        'UseLocalPowershell
        AutoHideOptions
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
        Me.Image = My.Resources.TaskRunExternalProgram
        Me.Category = "Output"
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.ExternalProgram = ""
        Me.SaveAfterProcessing = False

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

        OleMessageFilter.Register()

        Dim ExternalProgramDirectory As String = System.IO.Path.GetDirectoryName(Me.ExternalProgram)
        Dim ExitCode As Integer

        Dim UP As New UtilsPreferences

        ' ###### Copy form_main_settings.json to the external program's local directory. ######
        ' ###### Provides access to program settings if needed. ######
        Dim SettingsFilename = UP.GetFormMainSettingsFilename(CheckExisting:=True)
        Dim NewSettingsFilename As String = ""
        If Not SettingsFilename = "" Then
            NewSettingsFilename = System.IO.Path.GetFileName(SettingsFilename)
            NewSettingsFilename = $"{ExternalProgramDirectory}\{NewSettingsFilename}"
            If IO.File.Exists(NewSettingsFilename) Then IO.File.Delete(NewSettingsFilename)
            IO.File.Copy(SettingsFilename, NewSettingsFilename)
        End If

        Dim Extension As String
        Extension = IO.Path.GetExtension(Me.ExternalProgram)

        Dim PSError As String = ""

        If Extension = ".ps1" Then

            'P = New Diagnostics.Process
            'P.StartInfo.FileName = "powershell.exe"
            'P.StartInfo.Arguments = String.Format("-command {1}{0}{1}", Me.ExternalProgram.Replace(" ", "` "), Chr(34))
            'P.StartInfo.RedirectStandardError = True
            'P.StartInfo.UseShellExecute = False
            'If Me.HideConsoleWindow Then P.StartInfo.CreateNoWindow = True
            'P.Start()
            'PSError = P.StandardError.ReadToEnd

            Dim UPS As New UtilsPowerShell

            Try
                PSError = UPS.RunPowerShellFile(Me.ExternalProgram)
            Catch ex As Exception
                PSError = ex.Message
            End Try

        ElseIf Extension = ".snp" Then

            'Dim NewWay As Boolean = True

            'If Not NewWay Then
            '    'P = New Diagnostics.Process
            '    'P.StartInfo.FileName = "powershell.exe"
            '    'P.StartInfo.Arguments = String.Format("-command {1}{0}{1}", UPS.BuildSnippetFile(Me.ExternalProgram).Replace(" ", "` "), Chr(34))
            '    'P.StartInfo.RedirectStandardError = True
            '    'P.StartInfo.UseShellExecute = False
            '    'If Me.HideConsoleWindow Then P.StartInfo.CreateNoWindow = True
            '    'P.Start()
            '    'PSError = P.StandardError.ReadToEnd

            'Else

            '    'Dim ScriptList As List(Of String) = System.IO.File.ReadAllLines(UPS.BuildSnippetFile(Me.ExternalProgram)).ToList

            '    'Dim ScriptText As String = ""
            '    'For Each s As String In ScriptList
            '    '    ScriptText = $"{ScriptText}{vbCrLf}{s}"
            '    'Next

            '    'Try
            '    '    PSError = UPS.RunScript(ScriptText)
            '    'Catch ex As Exception
            '    '    PSError = ex.Message
            '    'End Try

            'End If

            Dim UPS As New UtilsPowerShell

            Try
                PSError = UPS.RunPowerShellFile(UPS.BuildSnippetFile(Me.ExternalProgram))
            Catch ex As Exception
                PSError = ex.Message
            End Try

            If Me.DeleteTempFiles Then
                Dim PowerShellFilename As String = IO.Path.ChangeExtension(Me.ExternalProgram, ".ps1")
                If IO.File.Exists(PowerShellFilename) Then IO.File.Delete(PowerShellFilename)
            End If

        Else  ' *.exe or *.vbs

            Dim P As Diagnostics.Process = Nothing

            P = New Diagnostics.Process
            P.StartInfo.FileName = Me.ExternalProgram
            If Me.HideConsoleWindow Then
                P.StartInfo.RedirectStandardError = True
                P.StartInfo.UseShellExecute = False
                P.StartInfo.CreateNoWindow = True
            End If
            P.Start()
            If Me.HideConsoleWindow Then PSError = P.StandardError.ReadToEnd

            If P IsNot Nothing Then
                P.WaitForExit()
                ExitCode = P.ExitCode
            End If
        End If

        If Not PSError = "" Then
            TaskLogger.AddMessage("The external program reported the following error")
            TaskLogger.AddMessage(PSError)
        End If

        If IO.File.Exists(NewSettingsFilename) Then IO.File.Delete(NewSettingsFilename)

        Dim ErrorMessageFilename As String
        Dim ErrorMessages As String()

        ErrorMessageFilename = $"{ExternalProgramDirectory}\error_messages.txt"

        If FileIO.FileSystem.FileExists(ErrorMessageFilename) Then
            ErrorMessages = IO.File.ReadAllLines(ErrorMessageFilename)
            If ErrorMessages.Length > 0 Then
                For Each ErrorMessageFromProgram As String In ErrorMessages
                    TaskLogger.AddMessage(ErrorMessageFromProgram)
                Next
            Else
                If Not ExitCode = 0 Then TaskLogger.AddMessage($"Program terminated with exit code {ExitCode}")
            End If

            IO.File.Delete(ErrorMessageFilename)
        Else
            If Not ExitCode = 0 Then TaskLogger.AddMessage($"Program terminated with exit code {ExitCode}")
        End If

        If Me.SaveAfterProcessing Then
            If SEDoc.ReadOnly Then
                TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        End If

    End Sub


    Private Sub DevelopSnippetCode()

        ' Develop the snippet code here, then copy to the snippet file.
        ' Un-indent for proper formatting in the *.ps1 file.
        ' An easy way to do the un-indent is to comment out the body of the program before copy/paste.
        ' Then in Notepad, Replace "        '" with ""

        ' NOTES ON POWERSHELL VB SYNTAX
        ' $"{VariableName}" format does not work.  Use String.Format("{0}", VariableName) instead.


        Dim ExitStatus As Integer = 0
        Dim ErrorMessageList As New List(Of String)

        Dim SEApp As SolidEdgeFramework.Application
        Dim SEDoc As SolidEdgeFramework.SolidEdgeDocument
        SEApp = CType(Runtime.InteropServices.Marshal.GetActiveObject("SolidEdge.Application"), SolidEdgeFramework.Application)
        SEDoc = CType(SEApp.ActiveDocument, SolidEdgeFramework.SolidEdgeDocument)
        Console.WriteLine("")

        Dim DocType As String = IO.Path.GetExtension(SEDoc.FullName)

        Try
            ' ############## SNIPPET CODE START ##############

            Dim something = SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalUseDimensionStyleElementMapLinDim
            Dim avalue As Object = Nothing
            SEApp.GetGlobalParameter(something, avalue)
            Dim i = 0
            ' ############## SNIPPET CODE END ##############

        Catch ex As Exception

        End Try

    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Button As Button
        Dim TextBox As TextBox

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        Button = FormatOptionsButton(ControlNames.Browse.ToString, "Program")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.ExternalProgram.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.SaveAfterProcessing.ToString, "Save file after processing")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.HideConsoleWindow.ToString, "Hide the program console window")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.EditSnippet.ToString, "Edit *.snp")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button
        Button.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DeleteTempFiles.ToString, "Delete temp files after processing")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If

            If Not FileIO.FileSystem.FileExists(Me.ExternalProgram) Then
                ErrorLogger.AddMessage("Select a valid external program")
            End If

        End If

    End Sub


    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.HideConsoleWindow.ToString
                Me.HideConsoleWindow = Checkbox.Checked

            Case ControlNames.DeleteTempFiles.ToString
                Me.DeleteTempFiles = Checkbox.Checked

            Case ControlNames.SaveAfterProcessing.ToString
                Me.SaveAfterProcessing = Checkbox.Checked

            'Case ControlNames.UseLocalPowershell.ToString
            '    Me.UseLocalPowershell = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
        End Select

    End Sub

    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox

        Select Case Name
            Case ControlNames.Browse.ToString

                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a program file"
                tmpFileDialog.Filter = "Programs|*.exe;*.vbs;*.ps1;*.snp"

                If IO.File.Exists(Me.ExternalProgram) Then
                    tmpFileDialog.InitialDirectory = IO.Path.GetDirectoryName(Me.ExternalProgram)
                Else
                    tmpFileDialog.InitialDirectory = Form_Main.WorkingFilesPath
                End If

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.ExternalProgram = tmpFileDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.ExternalProgram.ToString), TextBox)
                    TextBox.Text = Me.ExternalProgram

                End If

            Case ControlNames.EditSnippet.ToString

                Dim FEE As New FormExpressionEditor

                FEE.TextEditorFormula.Language = FastColoredTextBoxNS.Language.VB
                FEE.OutputType = "Snippet"
                If IO.Path.GetExtension(Me.ExternalProgram) = ".snp" Then
                    FEE.SnippetFilename = Me.ExternalProgram
                    'FEE.Formula = IO.File.ReadAllText(Me.ExternalProgram)
                    FEE.InputText = IO.File.ReadAllText(Me.ExternalProgram)
                Else
                    FEE.SnippetFilename = ""
                    FEE.SnippetFormula = ""
                End If

                Dim Result As DialogResult = FEE.ShowDialog()

                If Result = DialogResult.OK Then
                    Me.ExternalProgram = FEE.SnippetFilename

                End If

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
        End Select


    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name

            Case ControlNames.ExternalProgram.ToString
                Me.ExternalProgram = TextBox.Text

                Dim Extension As String = IO.Path.GetExtension(Me.ExternalProgram)

                CType(ControlsDict(ControlNames.EditSnippet.ToString), Button).Visible = False
                CType(ControlsDict(ControlNames.DeleteTempFiles.ToString), CheckBox).Visible = False
                CType(ControlsDict(ControlNames.HideConsoleWindow.ToString), CheckBox).Visible = False

                Select Case Extension
                    Case ".exe", ".vbs"
                        CType(ControlsDict(ControlNames.HideConsoleWindow.ToString), CheckBox).Visible = True
                    Case ".ps1"
                    Case ".snp"
                        CType(ControlsDict(ControlNames.EditSnippet.ToString), Button).Visible = True
                        CType(ControlsDict(ControlNames.DeleteTempFiles.ToString), CheckBox).Visible = True
                End Select

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Runs an `*.exe`, `*.vbs`, `*.ps1`, or `*.snp` program. "
        HelpString += "Housekeeper opens each Solid Edge file in turn, then launches the program.  "

        HelpString += vbCrLf + vbCrLf + "This command turns a typical single-file macro into a batch routine, "
        HelpString += "also enhancing it with Housekeeper's extensive file selection, filtering, and error-reporting capabilities. "

        HelpString += vbCrLf + vbCrLf + "![RunExternalProgram](My%20Project/media/task_run_external_program.png)"

        HelpString += vbCrLf + vbCrLf + "Select the program with the `Program` button on the Options panel. "
        HelpString += "Note, for downloaded programs, Windows sometimes sets a `Block` flag.  "
        HelpString += "Before you run it the first time, you can right-click the executable and select `Properties`.  "
        HelpString += "If it is blocked, there should be an option on the General Tab to `Unblock` it.  "

        HelpString += vbCrLf + vbCrLf + "The file types `*.ps1` and `*.snp` are PowerShell scripts.  "
        HelpString += "They are not normally run from an OS shell, but rather use an internal `dotnet` library.  "
        HelpString += "The library is supposed to improve compatibility across varied system configurations.  "

        HelpString += vbCrLf + vbCrLf + "To use the shell instead, enable `Use locally installed PowerShell` "
        HelpString += "on the **Configuration Tab -- General Page**.  "
        HelpString += "If that results in an error running the external program, you may need to change PowerShell's execution policy.  "
        HelpString += "To do so, open a PowerShell command prompt, then issue the command:  "
        HelpString += vbCrLf + "`Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser`.  "
        HelpString += vbCrLf + "Here is a Microsoft "
        HelpString += "[<ins>**link**</ins>](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_execution_policies?view=powershell-7.5) "
        HelpString += "with some details.  "
        HelpString += "If you're not an expert on such matters, you might want to run it by your IT department first.  "

        HelpString += vbCrLf + vbCrLf + "If you are writing your own program, "
        HelpString += "be aware several interoperability rules apply.  See "
        HelpString += "[<ins>**HousekeeperExternalPrograms**</ins>](https://github.com/rmcanany/HousekeeperExternalPrograms#readme) "
        HelpString += "for details and examples. "

        HelpString += vbCrLf + vbCrLf + "**Code Snippets**"

        HelpString += vbCrLf + vbCrLf + "Unlike the other file types, a `*.snp` is a special file containing only a small section of code. "
        HelpString += "The code snippet is the (often very short) part that does the actual task at hand. "
        HelpString += "You can see a few examples "
        HelpString += "[<ins>**here**</ins>](https://github.com/rmcanany/HousekeeperExternalPrograms/tree/main/Snippets)."

        HelpString += vbCrLf + vbCrLf + "The code snippet is just a text file written in VB.Net syntax.  It can be created in Notepad, "
        HelpString += "or more conveniently in the expression editor.  "
        HelpString += "Click `Edit *.snp` to use the editor.  "
        HelpString += "See the **Edit Properties Help Topic** for details on using the editor.  "

        HelpString += vbCrLf + vbCrLf + "The program inserts the snippet into a predefined PowerShell script.  "
        HelpString += "The script has two sections that take care of the task's set-up and wrap-up, respectively. "
        HelpString += "It has the same name and directory as the snippet file, except with a `.ps1` extension.  "
        'HelpString += "While it is a PowerShell script, it is not run in an OS shell.  "
        'HelpString += "Rather, it uses an internal dotnet library -- hopefully improving compatibility across varied system configurations.  "

        HelpString += vbCrLf + vbCrLf + "The intent is to address one-off automation chores, "
        HelpString += "where the time to do the job manually can't justify the time needed to "
        HelpString += "write, test, and maintain a separate program to do it automatically. "

        HelpString += vbCrLf + vbCrLf + "If you ask a programmer how to do **X**, you'll often get an answer.  "
        HelpString += "But only for **X** itself, not all the *other stuff*. "
        HelpString += "Things that may seem obvious to them but not to anyone else, "
        HelpString += "like the need to connect to the application, "
        HelpString += "activate a document, trap errors, etc., etc.  "
        HelpString += "The code snippet functionality is meant for the *other stuff*. "

        HelpString += vbCrLf + vbCrLf + "Here's an example that enables the Physical Properties `Update on Save` flag. "

        HelpString += vbCrLf + vbCrLf + "```"
        HelpString += vbCrLf + "If DocType = "".asm"" Then SEDoc.PhysicalProperties.UpdateOnFileSaveStatus = True"
        HelpString += vbCrLf + "If DocType = "".par"" Then SEDoc.UpdateOnFileSave = True"
        HelpString += vbCrLf + "If DocType = "".psm"" Then SEDoc.UpdateOnFileSave = True"
        HelpString += vbCrLf + "If DocType = "".dft"" Then ExitStatus = 1"
        HelpString += vbCrLf + "If ExitStatus = 0 Then"
        HelpString += vbCrLf + "    SEDoc.Save()"
        HelpString += vbCrLf + "    SEApp.DoIdle()"
        HelpString += vbCrLf + "Else"
        HelpString += vbCrLf + "    ErrorMessageList.Add(""An error occurred"")"
        HelpString += vbCrLf + "End If"
        HelpString += vbCrLf + "```"

        HelpString += vbCrLf + vbCrLf + "The program defines these variables, which you can use in your code. "
        HelpString += vbCrLf + "- `SEApp` The Solid Edge application."
        HelpString += vbCrLf + "- `SEDoc` The active document in the application."
        HelpString += vbCrLf + "- `ExitStatus` An integer.  0 = Success, 1 = Error."
        HelpString += vbCrLf + "- `ErrorMessageList` A list of error messages that Housekeeper reports."
        HelpString += vbCrLf + "- `DocType` The file extension of `SEDoc`."

        Return HelpString
    End Function


End Class
