Option Strict On

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

        Dim Toplist As New List(Of String)
        Dim Midlist As New List(Of String)
        Dim Botlist As New List(Of String)
        Dim Outlist As New List(Of String)
        Dim s As String
        Dim Indent As String = "                "

        Dim UP As New UtilsPreferences

        Dim DllPath As String = UP.GetStartupDirectory

        Dim PowerShellFilename As String = IO.Path.ChangeExtension(SnippetFilename, ".ps1")

        Toplist.Add("$StartupPath = Split-Path $script:MyInvocation.MyCommand.Path")
        Toplist.Add("")
        Toplist.Add("$DLLs = (")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgeFramework.dll"",")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgeFrameworkSupport.dll"",")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgeConstants.dll"",")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgePart.dll"",")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgeAssembly.dll"",")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgeDraft.dll"",")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgeGeometry.dll""")
        Toplist.Add("    )")
        Toplist.Add("")
        Toplist.Add("$Source = @""")
        Toplist.Add("")
        Toplist.Add("Imports System")
        Toplist.Add("Imports System.Collections.Generic")
        Toplist.Add("")
        Toplist.Add("Public Class Snippet")
        Toplist.Add("")
        Toplist.Add("    Public Shared Function RunSnippet(StartupPath As String) As Integer")
        Toplist.Add("        Dim ExitStatus As Integer = 0")
        Toplist.Add("        Dim ErrorMessageList As New List(Of String)")
        Toplist.Add("")
        Toplist.Add("        Dim SEApp As SolidEdgeFramework.Application = Nothing")
        Toplist.Add("        Dim SEDoc As SolidEdgeFramework.SolidEdgeDocument = Nothing")
        Toplist.Add("")
        Toplist.Add("        Try")
        Toplist.Add("            SEApp = CType(Runtime.InteropServices.Marshal.GetActiveObject(""SolidEdge.Application""), SolidEdgeFramework.Application)")
        Toplist.Add("            SEDoc = CType(SEApp.ActiveDocument, SolidEdgeFramework.SolidEdgeDocument)")
        Toplist.Add("            Console.WriteLine(String.Format(""Processing {0}"", SEDoc.Name))")
        Toplist.Add("        Catch ex As Exception")
        Toplist.Add("            ExitStatus = 1")
        Toplist.Add("            ErrorMessageList.Add(""Unable to connect to Solid Edge, or no file is open"")")
        Toplist.Add("        End Try")
        Toplist.Add("")
        Toplist.Add("        If ExitStatus = 0 Then")
        Toplist.Add("")
        Toplist.Add("            Dim DocType = IO.Path.GetExtension(SEDoc.Fullname)")
        Toplist.Add("")
        Toplist.Add("            Try")

        Dim tmpMidlist = IO.File.ReadAllLines(SnippetFilename).ToList
        For Each s In tmpMidlist
            Midlist.Add(String.Format("{0}{1}", Indent, s))
        Next

        Botlist.Add("            Catch ex As Exception")
        Botlist.Add("                ExitStatus = 1")
        Botlist.Add("                ErrorMessageList.Add(String.Format(""{0}"", ex.Message))")
        Botlist.Add("            End Try")
        Botlist.Add("        End If")
        Botlist.Add("")
        Botlist.Add("        If Not ExitStatus = 0 Then")
        Botlist.Add("            SaveErrorMessages(StartupPath, ErrorMessageList)")
        Botlist.Add("        End If")
        Botlist.Add("")
        Botlist.Add("        Return ExitStatus")
        Botlist.Add("    End Function")
        Botlist.Add("")
        Botlist.Add("    Public Shared Sub LoadLibrary(ParamArray libs As Object())")
        Botlist.Add("        For Each [lib] As String In libs")
        Botlist.Add("            'Console.WriteLine(String.Format(""Loading library:  {0}"", [lib]))")
        Botlist.Add("            Dim assm As System.Reflection.Assembly = System.Reflection.Assembly.LoadFrom([lib])")
        Botlist.Add("            'Console.WriteLine(assm.GetName().ToString())")
        Botlist.Add("        Next")
        Botlist.Add("    End Sub")
        Botlist.Add("")
        Botlist.Add("    Private Shared Sub SaveErrorMessages(StartupPath As String, ErrorMessageList As List(Of String))")
        Botlist.Add("        Dim ErrorFilename As String")
        Botlist.Add("        ErrorFilename = String.Format(""{0}\error_messages.txt"", StartupPath)")
        Botlist.Add("        IO.File.WriteAllLines(ErrorFilename, ErrorMessageList)")
        Botlist.Add("    End Sub")
        Botlist.Add("")
        Botlist.Add("End Class")
        Botlist.Add("""@")
        Botlist.Add("")
        'Botlist.Add("Add-Type -TypeDefinition $Source -Language VisualBasic")
        Botlist.Add("Add-Type -TypeDefinition $Source -ReferencedAssemblies $DLLs -Language VisualBasic")
        Botlist.Add("")
        Botlist.Add("[Snippet]::LoadLibrary($DLLs)")
        Botlist.Add("")
        Botlist.Add("$ExitStatus = [Snippet]::RunSnippet($StartupPath)")
        Botlist.Add("")
        Botlist.Add("Function ExitWithCode($exitcode) {")
        Botlist.Add("  $host.SetShouldExit($exitcode)")
        Botlist.Add("  Exit $exitcode")
        Botlist.Add("}")
        Botlist.Add("")
        Botlist.Add("ExitWithCode($ExitStatus)")

        For Each L As List(Of String) In {Toplist, Midlist, Botlist}
            For Each s In L
                Outlist.Add(s)
            Next
        Next

        IO.File.WriteAllLines(PowerShellFilename, Outlist)

        Return PowerShellFilename
    End Function

    Private Sub DevelopSnippetCode()

        ' Develop the snippet code here, then copy to the snippet file.
        ' Unindent for proper formatting in the *.ps1 file.

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
                Dim tmpSEDoc As SolidEdgeDraft.DraftDocument = CType(SEDoc, SolidEdgeDraft.DraftDocument)

                Dim PerimeterLength As Double = 0

                For Each Sheet As SolidEdgeDraft.Sheet In tmpSEDoc.Sheets
                    For Each DV As SolidEdgeDraft.DrawingView In Sheet.DrawingViews
                        If DV.DVEllipticalArcs2d IsNot Nothing AndAlso DV.DVEllipticalArcs2d.Count > 0 Then
                            For Each DVEA As SolidEdgeDraft.DVEllipticalArc2d In DV.DVEllipticalArcs2d
                                Dim a As Double = DVEA.MajorRadius
                                Dim b As Double = DVEA.MinorRadius
                                Dim e As Double = Math.Sqrt(1 - (b ^ 2 / a ^ 2))
                                Dim Theta1 As Double = DVEA.StartAngle
                                Dim Theta2 As Double = DVEA.SweepAngle + Theta1
                                Dim DeltaTheta As Double = DVEA.SweepAngle / 100

                                For Theta As Double = Theta1 To Theta2 Step DeltaTheta
                                    PerimeterLength += a * Math.Sqrt(1 - (e ^ 2) * (Math.Sin(Theta) ^ 2)) * DeltaTheta
                                Next
                            Next
                        End If
                    Next
                Next
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Perimeter was {0}", PerimeterLength))
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

        HelpString += vbCrLf + vbCrLf + "The code snippet is just a text file written in VB.Net syntax.  It can be created in Notepad.  "
        HelpString += "The program inserts the snippet into a predefined PowerShell script.  "
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

        'HelpString += vbCrLf + vbCrLf + "One present annoyance of using PowerShell is that I haven't found how "
        'HelpString += "to tell it about Solid Edge `type libraries`.  "
        'HelpString += "That means it doesn't understand things like: "
        'HelpString += vbCrLf + vbCrLf + "`Dim TemplateDoc As SolidEdgePart.PartDocument` "
        'HelpString += vbCrLf + vbCrLf + "You have to use `Dim TemplateDoc As Object` instead. "
        'HelpString += vbCrLf + vbCrLf + "It's not all bad news. "
        'HelpString += "For one thing, you can chain assignments like in the VB6 days. "

        'HelpString += vbCrLf + vbCrLf + "```"
        'HelpString += vbCrLf + "Dim igQueryPlane = 6"
        'HelpString += vbCrLf + "Dim Faces = SEDoc.Models.Item(1).Body.Faces(FaceType:=igQueryPlane)"
        'HelpString += vbCrLf + "```"

        'HelpString += vbCrLf + vbCrLf + "Another annoyance is troubleshooting. "
        'HelpString += "The Console Window from `Run External Program` disappears too fast to see syntax errors. "

        'HelpString += vbCrLf + vbCrLf + "To get around that, you can `Run External Program` once to create the PowerShell program.  "
        'HelpString += "Then run that code separately in a PowerShell terminal, where the error messages are persistent. "
        'HelpString += "Once it's working, copy the relevant part back into the `*.snp` file. "

        Return HelpString
    End Function


End Class
