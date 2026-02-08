Option Strict On

Imports FastColoredTextBoxNS
'Imports System.Collections.ArrayList
Imports Microsoft.VisualBasic
Imports System.Linq

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

        Dim ExternalProgramDirectory As String = System.IO.Path.GetDirectoryName(Me.ExternalProgram)
        Dim P As New Diagnostics.Process
        Dim ExitCode As Integer
        Dim ErrorMessageFilename As String
        Dim ErrorMessages As String()
        Dim Extension As String



        Dim UP As New UtilsPreferences

        Dim SettingsFilename = UP.GetFormMainSettingsFilename(CheckExisting:=True)
        Dim NewSettingsFilename As String = ""
        If Not SettingsFilename = "" Then
            NewSettingsFilename = System.IO.Path.GetFileName(SettingsFilename)
            NewSettingsFilename = String.Format("{0}\{1}", ExternalProgramDirectory, NewSettingsFilename)
            If IO.File.Exists(NewSettingsFilename) Then IO.File.Delete(NewSettingsFilename)
            System.IO.File.Copy(SettingsFilename, NewSettingsFilename)
        End If

        Extension = IO.Path.GetExtension(Me.ExternalProgram)

        Dim PSError As String = ""

        If Extension = ".ps1" Then
            P.StartInfo.FileName = "powershell.exe"
            P.StartInfo.Arguments = String.Format("-command {1}{0}{1}", Me.ExternalProgram.Replace(" ", "` "), Chr(34))
            P.StartInfo.RedirectStandardError = True
            P.StartInfo.UseShellExecute = False
            If Me.HideConsoleWindow Then P.StartInfo.CreateNoWindow = True
            P.Start()
            PSError = P.StandardError.ReadToEnd
        ElseIf Extension = ".snp" Then
            P.StartInfo.FileName = "powershell.exe"
            P.StartInfo.Arguments = String.Format("-command {1}{0}{1}", BuildSnippetFile(Me.ExternalProgram).Replace(" ", "` "), Chr(34))
            P.StartInfo.RedirectStandardError = True
            P.StartInfo.UseShellExecute = False
            If Me.HideConsoleWindow Then P.StartInfo.CreateNoWindow = True
            P.Start()
            PSError = P.StandardError.ReadToEnd
        Else
            'P = Diagnostics.Process.Start(Me.ExternalProgram)

            P.StartInfo.FileName = Me.ExternalProgram
            'P.StartInfo.Arguments = String.Format("{1}{0}{1}", Me.ExternalProgram, Chr(34))
            If Me.HideConsoleWindow Then
                P.StartInfo.RedirectStandardError = True
                P.StartInfo.UseShellExecute = False
                P.StartInfo.CreateNoWindow = True
            End If
            P.Start()
            If Me.HideConsoleWindow Then PSError = P.StandardError.ReadToEnd
        End If

        If Not PSError = "" Then
            TaskLogger.AddMessage("The external program reported the following error")
            TaskLogger.AddMessage(PSError)
        End If

        P.WaitForExit()
        ExitCode = P.ExitCode

        If IO.File.Exists(NewSettingsFilename) Then IO.File.Delete(NewSettingsFilename)

        If Me.DeleteTempFiles And Extension = ".snp" Then
            Dim PowerShellFilename As String = IO.Path.ChangeExtension(ExternalProgram, ".ps1")
            If IO.File.Exists(PowerShellFilename) Then IO.File.Delete(PowerShellFilename)
        End If

        ErrorMessageFilename = String.Format("{0}\error_messages.txt", ExternalProgramDirectory)

        If ExitCode <> 0 Then
            If FileIO.FileSystem.FileExists(ErrorMessageFilename) Then
                ErrorMessages = IO.File.ReadAllLines(ErrorMessageFilename)
                If ErrorMessages.Length > 0 Then
                    For Each ErrorMessageFromProgram As String In ErrorMessages
                        TaskLogger.AddMessage(ErrorMessageFromProgram)
                    Next
                Else
                    TaskLogger.AddMessage(String.Format("Program terminated with exit code {0}", ExitCode))
                End If

                IO.File.Delete(ErrorMessageFilename)
            Else
                TaskLogger.AddMessage(String.Format("Program terminated with exit code {0}", ExitCode))
            End If
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


    Private Function BuildSnippetFile(SnippetFilename As String) As String
        ' https://www.codestack.net/solidworks-pdm-api/permissions/set-folder-permissions/

        Dim PowerShellFilename As String = IO.Path.ChangeExtension(SnippetFilename, ".ps1")

        Dim UPS As New UtilsPowerShell
        PowerShellFilename = UPS.BuildSnippetFile(SnippetFilename)

        Return PowerShellFilename

    End Function

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

            If DocType = ".dft" Then

                '#### BEGIN USER INPUT ####

                ' Any line that starts with a ' character, like this one, is a comment and will be ignored.

                ' This is the variable that holds the file name and location (full path) of DraftList.txt
                ' It is used to verify that each Parts List Style in the lookup table exists in the file.
                ' Example: Dim DraftListPath As String = "C:\Program Files\Siemens\Solid Edge 2025\Template\Reports\DraftList.txt"
                ' Enter the full path and file name within the double quotes below
                Dim DraftListPath As String = "C:\Program Files\Siemens\Solid Edge 2025\Template\Reports\DraftList.txt"

                ' This is the lookup table that maps the Parts List Style to the Working Sheet's Background Name.
                ' The format is: LUT(<background sheet name>) = <parts list style name>
                ' It is picky about capitalization, punctuation, space characters, etc.
                Dim LUT As New Dictionary(Of String, String)
                ' Example: LUT("A3-quer") = "T - A3"
                LUT("A3-quer") = "T - A3"
                LUT("A2-quer") = "T - A2"
                LUT("A1-quer") = "T - A1"
                LUT("A0-quer") = "T - A0"
                'LUT("B-Sheet") = "ANSI"

                '#### END USER INPUT ####


                '#### VERIFY PARTS LIST STYLE NAMES ####

                If Not IO.File.Exists(DraftListPath) Then
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("File not found '{0}'", DraftListPath))
                Else
                    ' DraftList.txt format
                    'BEGINSET
                    ' CONFIGNAME = ANSI
                    ' TextStyle = Normal
                    ' ...
                    'BEGINSET
                    ' CONFIGNAME = ISO
                    ' TextStyle = Normal
                    ' ...

                    ' Parse DraftList.txt file and store style names it contains.

                    Dim KnownStyles As New List(Of String)
                    Dim DraftListContents As List(Of String) = IO.File.ReadAllLines(DraftListPath).ToList
                    'Dim DraftListContents As String() = IO.File.ReadAllLines(DraftListPath)
                    Dim SearchingHeader As Boolean = True
                    Dim SearchingName As Boolean = False

                    For Each Line As String In DraftListContents
                        If SearchingHeader Then
                            If Line.ToUpper.Trim = "BEGINSET" Then
                                SearchingHeader = False
                                SearchingName = True
                                Continue For
                            End If
                        End If
                        If SearchingName Then
                            If Line.ToUpper.Trim.StartsWith("CONFIGNAME") Then
                                Dim KnownStyle As String = Line.Split("="c)(1).Trim
                                If Not KnownStyles.Contains(KnownStyle) Then
                                    KnownStyles.Add(KnownStyle)
                                Else
                                    ExitStatus = 1
                                    ErrorMessageList.Add(String.Format("DraftList.txt duplicate style name found `{0}`", KnownStyle))
                                End If
                                SearchingHeader = True
                                SearchingName = False
                                Continue For
                            End If
                        End If
                    Next

                    ' Check the lookup table for valid parts list style names

                    For Each BackgroundName As String In LUT.Keys
                        If Not KnownStyles.Contains(LUT(BackgroundName)) Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("DraftList.txt style name not found '{0}'", LUT(BackgroundName)))
                        End If
                    Next

                End If


                ' #### UPDATE PARTS LIST STYLES ####

                If ExitStatus = 0 Then
                    Dim tmpSEDoc As SolidEdgeDraft.DraftDocument = CType(SEDoc, SolidEdgeDraft.DraftDocument)

                    If tmpSEDoc.Sheets Is Nothing Then
                        ExitStatus = 1
                        ErrorMessageList.Add("Could not process Sheets")
                    Else
                        For Each Sheet As SolidEdgeDraft.Sheet In tmpSEDoc.Sheets
                            If Sheet.Section.Type = 0 Then
                                For Each Item As Object In Sheet.DrawingObjects
                                    Dim PartsList As SolidEdgeDraft.PartsList = TryCast(Item, SolidEdgeDraft.PartsList)
                                    If PartsList IsNot Nothing Then
                                        'Dim IsAlreadyPresent As Boolean = False
                                        'For Each Key As String In LUT.Keys
                                        '    If Key = Sheet.Background.Name Then
                                        '        IsAlreadyPresent = True
                                        '        Exit For
                                        '    End If
                                        'Next
                                        If Not LUT.Keys.Contains(Sheet.Background.Name) Then
                                            ExitStatus = 1
                                            ErrorMessageList.Add(String.Format("Not in lookup table: Sheet '{0}' background '{1}'", Sheet.Name, Sheet.Background.Name))

                                        Else
                                            Try
                                                PartsList.SavedSettings = LUT(Sheet.Background.Name)
                                                PartsList.Update()
                                                SEApp.DoIdle()
                                            Catch ex As Exception
                                                ExitStatus = 1
                                                Dim s As String = "Could not update parts list style: "
                                                s = String.Format("{0}Sheet '{1}' Style '{2}'{3}", s, Sheet.Name, LUT(Sheet.Background.Name), vbCrLf)
                                                s = String.Format("{0}Error message was: {1}", s, ex.Message)
                                                ErrorMessageList.Add(s)
                                            End Try

                                        End If
                                    End If
                                Next
                            End If
                        Next

                        Try
                            SEDoc.Save()
                            SEApp.DoIdle()
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Could not save file: '{0}'", ex.Message))
                        End Try

                    End If
                End If

            End If

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

        Button = FormatOptionsButton(ControlNames.EditSnippet.ToString, "Edit *.snp")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button
        'Button.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.HideConsoleWindow.ToString, "Hide the program console window")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DeleteTempFiles.ToString, "Delete temp files after processing")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.SaveAfterProcessing.ToString, "Save file after processing")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

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

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
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

                    'Form_Main.WorkingFilesPath = IO.Path.GetDirectoryName(Me.ExternalProgram)
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
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name

            Case ControlNames.ExternalProgram.ToString
                Me.ExternalProgram = TextBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Runs an `*.exe`, `*.vbs`, `*.ps1`, or `*.snp` program. "
        HelpString += "Housekeeper opens each Solid Edge file in turn, then launches the program.  "

        HelpString += vbCrLf + vbCrLf + "This command turns a typical single-file macro into a batch routine, "
        HelpString += "also enhancing it with Housekeeper's extensive file selection, filtering, and error-reporting capabilities. "

        HelpString += vbCrLf + vbCrLf + "![RunExternalProgram](My%20Project/media/task_run_external_program.png)"

        HelpString += vbCrLf + vbCrLf + "Select the program with the `Browse` button on the Options panel. "
        HelpString += "Note, for downloaded programs, Windows sometimes sets a `Block` flag.  "
        HelpString += "Before you run it the first time, you can right-click the executable and select `Properties`.  "
        HelpString += "If it is blocked, there should be an option on the General Tab to `Unblock` it.  "

        HelpString += vbCrLf + vbCrLf + "For PowerShell programs, `*.ps1` and `*.snp`, you may need to change your security settings.  "
        HelpString += "You can do so by opening a PowerShell command prompt.  "
        HelpString += "Then issue the command `Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser`.  Here is a Microsoft "
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
        HelpString += "Click `Edit *.snp` to use the editor.  See the **Edit Properties** help topic for details on its use.  "

        HelpString += vbCrLf + vbCrLf + "The program inserts the snippet into a predefined PowerShell script.  "
        HelpString += "The script has two sections that take care of the task's set-up and wrap-up, respectively. "
        HelpString += "It has the same name and directory as the snippet file, except with a `.ps1` extension.  "

        HelpString += vbCrLf + vbCrLf + "The intent is to address one-off automation chores, "
        HelpString += "where the time to do the job manually can't justify the time needed to "
        HelpString += "write, test, and maintain a separate program to do it automatically. "

        HelpString += vbCrLf + vbCrLf + "If you ask a programmer how to do **X**, you'll often get an answer.  "
        HelpString += "But only for **X** itself, not all the *other stuff*. "
        HelpString += "Things that may seem obvious to them but not to anyone else, "
        HelpString += "like the need to connect to the application, "
        HelpString += "activate a document, trap errors, etc., etc.  "
        HelpString += "The code snippet functionality is meant for the *other stuff*. "

        HelpString += vbCrLf + vbCrLf + "Here's an example snippet for enabling the Physical Properties `Update on Save` flag. "

        HelpString += vbCrLf + vbCrLf + "```"
        HelpString += vbCrLf + $"If DocType = "".asm"" Then SEDoc.PhysicalProperties.UpdateOnFileSaveStatus = True"
        HelpString += vbCrLf + $"If DocType = "".par"" Then SEDoc.UpdateOnFileSave = True"
        HelpString += vbCrLf + $"If DocType = "".psm"" Then SEDoc.UpdateOnFileSave = True"
        HelpString += vbCrLf + $"If DocType = "".dft"" ExitStatus = 1"
        HelpString += vbCrLf + "If ExitStatus = 0 Then"
        HelpString += vbCrLf + "    SEDoc.Save()"
        HelpString += vbCrLf + "    SEApp.DoIdle()"
        HelpString += vbCrLf + "Else"
        HelpString += vbCrLf + $"    ErrorMessageList.Add(""An error occurred"")"
        HelpString += vbCrLf + "End If"
        HelpString += vbCrLf + "```"

        HelpString += vbCrLf + vbCrLf + "The program defines these variables, which you can use in your code. "
        HelpString += vbCrLf + "- `SEApp` The Solid Edge application."
        HelpString += vbCrLf + "- `SEDoc` The active document in the application."
        HelpString += vbCrLf + "- `ExitStatus` An integer.  0 = Success, 1 = Error."
        HelpString += vbCrLf + "- `ErrorMessageList` A list of error messages that Housekeeper reports."
        HelpString += vbCrLf + "- `DocType` The file extension of `SEDoc`."

        HelpString += vbCrLf + vbCrLf + "It is possible to refer to a property "
        HelpString += "that may not be present in every file. "
        HelpString += "Normally in that situation, it would take the value of `Nothing` at run time. "
        HelpString += "Currently code snippets do not support that.  "
        HelpString += "Instead, missing properties are returned as a `String` with the value of `""<Nothing>""`.  "
        HelpString += "You can check for that in your snippet when needed.  "

        Return HelpString
    End Function


End Class
