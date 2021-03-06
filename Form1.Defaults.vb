Option Strict On

Partial Class Form1

    Private Function GetConfiguration() As Dictionary(Of String, String)
        Dim Configuration As New Dictionary(Of String, String)
        Dim ControlDict As New Dictionary(Of String, Control)
        Dim Ctrl As Control

        ControlDict = RecurseFormControls(Me, ControlDict, False)

        For Each Key In ControlDict.Keys
            Ctrl = ControlDict(Key)

            If TypeOf Ctrl Is TextBox Then
                Dim c As TextBox = CType(Ctrl, TextBox)
                Configuration.Add(c.Name, c.Text)

            ElseIf TypeOf Ctrl Is CheckBox Then
                Dim c As CheckBox = CType(Ctrl, CheckBox)
                Configuration.Add(c.Name, c.Checked.ToString)

            ElseIf TypeOf Ctrl Is RadioButton Then
                Dim c As RadioButton = CType(Ctrl, RadioButton)
                Configuration.Add(c.Name, c.Checked.ToString)

            ElseIf TypeOf Ctrl Is ComboBox Then
                Dim c As ComboBox = CType(Ctrl, ComboBox)
                Configuration.Add(c.Name, c.Text)
            End If
        Next


        Return Configuration
    End Function

    Private Function RecurseFormControls(Ctrl As Control,
                                         ControlDict As Dictionary(Of String, Control),
                                         Exclude As Boolean
                                         ) As Dictionary(Of String, Control)

        Dim ChildControl As Control
        Dim tf As Boolean
        Dim ExcludeControls As New List(Of String)

        ExcludeControls.Add(RadioButtonFilesDirectoriesAndSubdirectories.Name)
        ExcludeControls.Add(RadioButtonFilesDirectoryOnly.Name)
        ExcludeControls.Add(RadioButtonTopLevelAssembly.Name)
        ExcludeControls.Add(RadioButtonTODOList.Name)
        ExcludeControls.Add(CheckBoxEnablePropertyFilter.Name)
        ExcludeControls.Add(CheckBoxCreateTODOList.Name)
        ExcludeControls.Add(TextBoxReadme.Name)
        ExcludeControls.Add(ListBoxFiles.Name)

        tf = TypeOf Ctrl Is ContainerControl
        tf = tf Or TypeOf Ctrl Is TabControl
        tf = tf Or TypeOf Ctrl Is TabPage
        tf = tf Or TypeOf Ctrl Is GroupBox

        If tf Then
            For Each ChildControl In Ctrl.Controls
                ControlDict = RecurseFormControls(ChildControl, ControlDict, Exclude)
            Next
        Else
            tf = TypeOf Ctrl Is Button
            tf = tf Or TypeOf Ctrl Is Label
            If Exclude Then
                tf = tf Or ExcludeControls.Contains(Ctrl.Name)
            End If

            If Not tf Then
                ControlDict.Add(Ctrl.Name, Ctrl)
            End If
        End If

        Return ControlDict
    End Function

    Private Sub PopulateTextBox(tb As TextBox, Value As String, StartupPath As String)
        Dim tf As Boolean

        tf = tb.Name.ToLower.Contains("directory")
        If tf Then
            If FileIO.FileSystem.DirectoryExists(Value) Then
                tb.Text = Value
            Else
                tb.Text = StartupPath
            End If
        End If

        tf = tb.Name.ToLower.Contains("template")
        tf = tf Or tb.Name.ToLower.Contains("materiallibrary")
        tf = tf Or tb.Name.ToLower.Contains("toplevelassembly")
        tf = tf Or tb.Name.ToLower.Contains("externalprogram")
        If tf Then
            If FileIO.FileSystem.FileExists(Value) Then
                tb.Text = Value
            Else
                tb.Text = ""
            End If
        End If

        tf = tb.Name.ToLower.Contains("columnwidth")
        If tf Then
            If Value = "" Then
                tb.Text = "5.5"
            Else
                Try
                    tb.Text = CStr(Value)
                Catch ex As Exception
                    tb.Text = "5.5"
                End Try
            End If
        End If

        tf = tb.Name.ToLower.Contains("fontsize")
        If tf Then
            If Value = "" Then
                tb.Text = "8"
            Else
                Try
                    tb.Text = CStr(Value)
                Catch ex As Exception
                    tb.Text = "8"
                End Try
            End If
        End If

        tf = tb.Name.ToLower.Contains("restartafter")
        If tf Then
            If Value = "" Then
                tb.Text = "250"
            Else
                Try
                    tb.Text = CStr(Value)
                Catch ex As Exception
                    tb.Text = "250"
                End Try
            End If
        End If

        tf = tb.Name.ToLower.Contains("propertyname")
        tf = tf Or tb.Name.ToLower.Contains("findreplace")
        tf = tf Or tb.Name.ToLower.Contains("filesearch")
        If tf Then
            tb.Text = Value
        End If


    End Sub

    Private Sub PopulateComboBoxes()
        'Dim FileTypesAssembly As String()
        'Dim FileTypesPart As String()
        'Dim FileTypesSheetmetal As String()
        'Dim FileTypesDraft As String()
        Dim FileTypesString As String
        Dim FileType As String

        ' Assembly
        FileTypesString = "Step (*.stp):IGES (*.igs):Parasolid Text (*.x_t):Parasolid Binary (*.x_b)"
        FileTypesString += ":OBJ (*.obj):STL (*.stl)"

        ComboBoxSaveAsAssemblyFileType.Items.Clear()
        For Each FileType In Split(FileTypesString, Delimiter:=":")
            ComboBoxSaveAsAssemblyFileType.Items.Add(FileType)
        Next
        ComboBoxSaveAsAssemblyFileType.Text = CType(ComboBoxSaveAsAssemblyFileType.Items(0), String)

        ' Part
        ComboBoxSaveAsPartFileType.Items.Clear()
        For Each FileType In Split(FileTypesString, Delimiter:=":")
            ComboBoxSaveAsPartFileType.Items.Add(FileType)
        Next
        ComboBoxSaveAsPartFileType.Text = CType(ComboBoxSaveAsPartFileType.Items(0), String)

        ' Sheetmetal
        ComboBoxSaveAsSheetmetalFileType.Items.Clear()
        For Each FileType In Split(FileTypesString, Delimiter:=":")
            ComboBoxSaveAsSheetmetalFileType.Items.Add(FileType)
        Next
        ComboBoxSaveAsSheetmetalFileType.Text = CType(ComboBoxSaveAsSheetmetalFileType.Items(0), String)

        ' Draft
        FileTypesString = "PDF (*.pdf):DXF (*.dxf):DWG (*.dwg):IGES (*.igs)"

        ComboBoxSaveAsDraftFileType.Items.Clear()
        For Each FileType In Split(FileTypesString, Delimiter:=":")
            ComboBoxSaveAsDraftFileType.Items.Add(FileType)
        Next
        ComboBoxSaveAsDraftFileType.Text = CType(ComboBoxSaveAsDraftFileType.Items(0), String)

        'ComboBoxPartNumberPropertySet
        ComboBoxPartNumberPropertySet.Items.Clear()
        For Each s As String In Split("System Custom")
            ComboBoxPartNumberPropertySet.Items.Add(s)
        Next
        ComboBoxPartNumberPropertySet.Text = CType(ComboBoxPartNumberPropertySet.Items(0), String)

        'ComboBoxFindReplacePropertySetAssembly
        ComboBoxFindReplacePropertySetAssembly.Items.Clear()
        For Each s As String In Split("System Custom")
            ComboBoxFindReplacePropertySetAssembly.Items.Add(s)
        Next
        ComboBoxFindReplacePropertySetAssembly.Text = CType(ComboBoxFindReplacePropertySetAssembly.Items(0), String)

        'ComboBoxFindReplacePropertySetPart
        ComboBoxFindReplacePropertySetPart.Items.Clear()
        For Each s As String In Split("System Custom")
            ComboBoxFindReplacePropertySetPart.Items.Add(s)
        Next
        ComboBoxFindReplacePropertySetPart.Text = CType(ComboBoxFindReplacePropertySetPart.Items(0), String)

        'ComboBoxFindReplacePropertySetSheetmetal
        ComboBoxFindReplacePropertySetSheetmetal.Items.Clear()
        For Each s As String In Split("System Custom")
            ComboBoxFindReplacePropertySetSheetmetal.Items.Add(s)
        Next
        ComboBoxFindReplacePropertySetSheetmetal.Text = CType(ComboBoxFindReplacePropertySetSheetmetal.Items(0), String)

    End Sub

    Private Sub LoadDefaults()
        'See format example in SaveDefaults()

        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim Defaults As String() = Nothing
        Dim Key As String
        Dim Value As String
        Dim msg As String
        Dim tf As Boolean

        Dim ControlDict As New Dictionary(Of String, Control)
        Dim Ctrl As Control

        DefaultsFilename = StartupPath + "\" + "defaults.txt"

        PopulateComboBoxes()

        ControlDict = RecurseFormControls(Me, ControlDict, True)

        Try
            Defaults = IO.File.ReadAllLines(DefaultsFilename)
            msg = ""
            For Each KVPair As String In Defaults
                If Not KVPair.Contains("=") Then
                    Continue For
                End If

                Key = KVPair.Split("="c)(0)
                Value = KVPair.Split("="c)(1)

                If ControlDict.Keys.Contains(Key) Then
                    Ctrl = ControlDict(Key)

                    If TypeOf Ctrl Is TextBox Then
                        Dim c As TextBox = CType(Ctrl, TextBox)
                        PopulateTextBox(c, Value, Application.StartupPath)

                    ElseIf TypeOf Ctrl Is CheckBox Then
                        Dim c As CheckBox = CType(Ctrl, CheckBox)
                        If Value.ToLower = "true" Then
                            c.Checked = True
                        Else
                            c.Checked = False
                        End If

                    ElseIf TypeOf Ctrl Is RadioButton Then
                        Dim c As RadioButton = CType(Ctrl, RadioButton)
                        If Value.ToLower = "true" Then
                            c.Checked = True
                        Else
                            c.Checked = False
                        End If

                    ElseIf TypeOf Ctrl Is ComboBox Then
                        Dim c As ComboBox = CType(Ctrl, ComboBox)
                        c.Text = Value
                    End If
                Else
                    tf = Key.Contains("Assembly.")
                    tf = tf Or Key.Contains("Part.")
                    tf = tf Or Key.Contains("Sheetmetal.")
                    tf = tf Or Key.Contains("Draft.")
                    tf = tf And CheckBoxRememberTasks.Checked
                    If tf Then
                        PopulateCheckboxDefault(KVPair)
                    End If
                End If
            Next

        Catch ex As Exception
            ' MsgBox(ex.ToString)
            TextBoxInputDirectory.Text = Application.StartupPath
        End Try

        ReconcileFormChanges()

    End Sub


    'Private Function GetPartsListStyles() As List(Of String)
    '    Dim PartsListStyles As New List(Of String)
    '    '<install dir>/Template/Reports/DraftList.txt
    '    ' Dim InstallData As SEInstallDataLib.SEInstallData = Nothing
    '    Dim InstallData As New SEInstallDataLib.SEInstallDataClass
    '    Dim InstallPath As String
    '    Dim DraftListPath As String
    '    Dim DraftList As String() = Nothing
    '    Dim Key As String
    '    Dim Value As String
    '    Dim InstallPathList As List(Of String)

    '    InstallPath = InstallData.GetInstalledPath  ' "C:\Program Files\Siemens\Solid Edge 2020\Program"
    '    ' Need to get rid of the last directory
    '    InstallPathList = InstallPath.Split("\"c).ToList
    '    InstallPathList.RemoveAt(InstallPathList.Count - 1)
    '    InstallPath = String.Join("\", InstallPathList)
    '    DraftListPath = String.Format("{0}\Template\Reports\DraftList.txt", InstallPath)
    '    Try
    '        DraftList = IO.File.ReadAllLines(DraftListPath)
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    '    ' MsgBox(DraftListPath)

    '    For Each KVPair As String In DraftList
    '        If KVPair.Contains("CONFIGNAME") Then
    '            Key = KVPair.Split("="c).ToList(0)
    '            Value = KVPair.Split("="c).ToList(1)
    '            PartsListStyles.Add(Value)
    '        End If
    '    Next
    '    'MsgBox("GetPartsListStyles end")
    '    Return PartsListStyles
    'End Function

    Private Sub SaveDefaults()
        'Format Example
        'TextBoxInputDirectory=D:\CAD\scripts\test_files
        'TextBoxTemplateAssembly=C:\Program Files\Siemens\Solid Edge 2020\Template\ANSI Inch\ansi inch assembly.asm
        'ComboBoxPartNumberPropertySet=Custom
        'TextBoxPartNumberPropertyName=hmk_Part_Number
        'TextBoxRestartAfter=50
        'TextBoxLaserOutputDirectory=D:\CAD\scripts\test_files\20181212_auto_cube_fill\laser_files_robert
        'Assembly.Occurrence missing file=Checked
        'Assembly.Occurrence outside project directory=Unchecked
        'Assembly.Failed relationships=Unchecked

        Dim Defaults As New List(Of String)
        Dim msg As String
        Dim ControlDict As New Dictionary(Of String, Control)
        Dim Key As String
        Dim Ctrl As Control

        ControlDict = RecurseFormControls(Me, ControlDict, True)

        For Each Key In ControlDict.Keys
            Ctrl = ControlDict(Key)

            If TypeOf Ctrl Is TextBox Then
                Dim c As TextBox = CType(Ctrl, TextBox)
                Defaults.Add(String.Format("{0}={1}", c.Name, c.Text))

            ElseIf TypeOf Ctrl Is CheckBox Then
                Dim c As CheckBox = CType(Ctrl, CheckBox)
                Defaults.Add(String.Format("{0}={1}", c.Name, c.Checked.ToString))

            ElseIf TypeOf Ctrl Is RadioButton Then
                Dim c As RadioButton = CType(Ctrl, RadioButton)
                Defaults.Add(String.Format("{0}={1}", c.Name, c.Checked.ToString))

            ElseIf TypeOf Ctrl Is ComboBox Then
                Dim c As ComboBox = CType(Ctrl, ComboBox)
                Defaults.Add(String.Format("{0}={1}", c.Name, c.Text))
            End If
        Next

        'Defaults.Add(TextBoxInputDirectory.Name + "=" + TextBoxInputDirectory.Text)
        ''MsgBox(TextBoxInputDirectory.Text)
        'Defaults.Add(TextBoxTemplateAssembly.Name + "=" + TextBoxTemplateAssembly.Text)
        'Defaults.Add(TextBoxTemplatePart.Name + "=" + TextBoxTemplatePart.Text)
        'Defaults.Add(TextBoxTemplateSheetmetal.Name + "=" + TextBoxTemplateSheetmetal.Text)
        'Defaults.Add(TextBoxTemplateDraft.Name + "=" + TextBoxTemplateDraft.Text)
        'Defaults.Add(TextBoxActiveMaterialLibrary.Name + "=" + TextBoxActiveMaterialLibrary.Text)
        'Defaults.Add(ComboBoxPartNumberPropertySet.Name + "=" + ComboBoxPartNumberPropertySet.Text)
        'Defaults.Add(TextBoxPartNumberPropertyName.Name + "=" + TextBoxPartNumberPropertyName.Text)
        'Defaults.Add(TextBoxRestartAfter.Name + "=" + TextBoxRestartAfter.Text)
        'Defaults.Add(TextBoxLaserOutputDirectory.Name + "=" + TextBoxLaserOutputDirectory.Text)
        'Defaults.Add(CheckBoxWarnSave.Name + "=" + CheckBoxWarnSave.Checked.ToString)
        'Defaults.Add(TextBoxStepAssemblyOutputDirectory.Name + "=" + TextBoxStepAssemblyOutputDirectory.Text)
        'Defaults.Add(TextBoxStepPartOutputDirectory.Name + "=" + TextBoxStepPartOutputDirectory.Text)
        'Defaults.Add(TextBoxStepSheetmetalOutputDirectory.Name + "=" + TextBoxStepSheetmetalOutputDirectory.Text)
        'Defaults.Add(TextBoxPdfDraftOutputDirectory.Name + "=" + TextBoxPdfDraftOutputDirectory.Text)
        'Defaults.Add(TextBoxDxfDraftOutputDirectory.Name + "=" + TextBoxDxfDraftOutputDirectory.Text)
        'Defaults.Add(TextBoxColumnWidth.Name + "=" + TextBoxColumnWidth.Text)
        'Defaults.Add(TextBoxTopLevelAssembly.Name + "=" + TextBoxTopLevelAssembly.Text)
        '' Defaults.Add(TextBoxPropertyFilter.Name + "=" + TextBoxPropertyFilter.Text)

        'Defaults.Add(CheckBoxStepAssemblyOutputDirectory.Name + "=" + CheckBoxStepAssemblyOutputDirectory.Checked.ToString)
        'Defaults.Add(CheckBoxStepPartOutputDirectory.Name + "=" + CheckBoxStepPartOutputDirectory.Checked.ToString)
        'Defaults.Add(CheckBoxLaserOutputDirectory.Name + "=" + CheckBoxLaserOutputDirectory.Checked.ToString)
        'Defaults.Add(CheckBoxStepSheetmetalOutputDirectory.Name + "=" + CheckBoxStepSheetmetalOutputDirectory.Checked.ToString)
        'Defaults.Add(CheckBoxPdfDraftOutputDirectory.Name + "=" + CheckBoxPdfDraftOutputDirectory.Checked.ToString)
        'Defaults.Add(CheckBoxDxfDraftOutputDirectory.Name + "=" + CheckBoxDxfDraftOutputDirectory.Checked.ToString)

        'Defaults.Add(RadioButtonTLABottomUp.Name + "=" + RadioButtonTLABottomUp.Checked.ToString)
        'Defaults.Add(RadioButtonTLATopDown.Name + "=" + RadioButtonTLATopDown.Checked.ToString)
        'Defaults.Add(CheckBoxTLAReportUnrelatedFiles.Name + "=" + CheckBoxTLAReportUnrelatedFiles.Checked.ToString)

        ''MsgBox(ComboBoxPartsListStyle.SelectedText)
        ''Defaults.Add(ComboBoxPartsListStyle.Name + "=" + ComboBoxPartsListStyle.SelectedItem.ToString)
        ''Defaults.Add(ComboBoxPartsListAutoballoon.Name + "=" + ComboBoxPartsListAutoballoon.SelectedItem.ToString)
        ''Defaults.Add(ComboBoxPartsListCreateList.Name + "=" + ComboBoxPartsListCreateList.SelectedItem.ToString)

        For idx = 0 To CheckedListBoxAssembly.Items.Count - 1
            msg = "Assembly." + CheckedListBoxAssembly.Items(idx).ToString + "="
            msg += CheckedListBoxAssembly.GetItemCheckState(idx).ToString()
            Defaults.Add(msg)
        Next
        For idx = 0 To CheckedListBoxPart.Items.Count - 1
            msg = "Part." + CheckedListBoxPart.Items(idx).ToString + "="
            msg += CheckedListBoxPart.GetItemCheckState(idx).ToString()
            Defaults.Add(msg)
        Next
        For idx = 0 To CheckedListBoxSheetmetal.Items.Count - 1
            msg = "Sheetmetal." + CheckedListBoxSheetmetal.Items(idx).ToString + "="
            msg += CheckedListBoxSheetmetal.GetItemCheckState(idx).ToString()
            Defaults.Add(msg)
        Next
        For idx = 0 To CheckedListBoxDraft.Items.Count - 1
            msg = "Draft." + CheckedListBoxDraft.Items(idx).ToString + "="
            msg += CheckedListBoxDraft.GetItemCheckState(idx).ToString()
            Defaults.Add(msg)
        Next

        IO.File.WriteAllLines(DefaultsFilename, Defaults)

    End Sub

    Private Sub PopulateCheckboxDefault(KVPair As String)
        'See format example in SaveDefaults

        Dim KeyAndName As String
        Dim Key As String
        Dim Value As String
        Dim CheckBoxName As String
        Dim ValueAsBoolean As Boolean
        Dim idx As Integer

        KeyAndName = KVPair.Split(("=").ToCharArray(0, 1))(0)
        Key = KeyAndName.Split((".").ToCharArray(0, 1))(0)
        CheckBoxName = KeyAndName.Split((".").ToCharArray(0, 1))(1)
        Value = KVPair.Split(("=").ToCharArray(0, 1))(1)

        If Value = "Checked" Then
            ValueAsBoolean = True
        Else
            ValueAsBoolean = False
        End If

        If Key = "Assembly" Then
            For idx = 0 To CheckedListBoxAssembly.Items.Count - 1
                If CheckedListBoxAssembly.Items(idx).ToString = CheckBoxName Then
                    CheckedListBoxAssembly.SetItemChecked(idx, ValueAsBoolean)
                End If
            Next
        End If
        If Key = "Part" Then
            For idx = 0 To CheckedListBoxPart.Items.Count - 1
                If CheckedListBoxPart.Items(idx).ToString = CheckBoxName Then
                    CheckedListBoxPart.SetItemChecked(idx, ValueAsBoolean)
                End If
            Next
        End If
        If Key = "Sheetmetal" Then
            For idx = 0 To CheckedListBoxSheetmetal.Items.Count - 1
                If CheckedListBoxSheetmetal.Items(idx).ToString = CheckBoxName Then
                    CheckedListBoxSheetmetal.SetItemChecked(idx, ValueAsBoolean)
                End If
            Next
        End If
        If Key = "Draft" Then
            For idx = 0 To CheckedListBoxDraft.Items.Count - 1
                If CheckedListBoxDraft.Items(idx).ToString = CheckBoxName Then
                    CheckedListBoxDraft.SetItemChecked(idx, ValueAsBoolean)
                End If
            Next
        End If
    End Sub

    Private Sub LoadTextBoxReadme()
        Dim msg As String
        Dim readme_github As New List(Of String)  ' Used to create the Readme file for GitHub
        Dim readme_tab As New List(Of String)  ' Used to create the Readme tab on Form1
        Dim readme_tab_formatted As New List(Of String)  ' Reformats msg2 to eliminate Markdown directives
        Dim ReadmeFileName As String
        Dim FilenameList As New List(Of String)
        Dim tf As Boolean
        Dim StartupPath As String

        Dim CheckBoxList As New List(Of CheckedListBox)
        Dim Names As New List(Of String)

        CheckBoxList.Add(CheckedListBoxAssembly)
        CheckBoxList.Add(CheckedListBoxPart)
        CheckBoxList.Add(CheckedListBoxSheetmetal)
        CheckBoxList.Add(CheckedListBoxDraft)

        Names.Add("### Assembly")
        Names.Add("### Part")
        Names.Add("### Sheetmetal")
        Names.Add("### Draft")

        msg = "# Solid Edge Housekeeper v0.1.7"
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = "Robert McAnany 2021"
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "Portions adapted from code by Jason Newell, Greg Chasteen, Tushar Suradkar, and others.  "
        msg += "Most of the rest copied verbatim from Jason's repo and Tushar's blog."
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "Helpful feedback and bug reports: @Satyen, @n0minus38, @wku, @aredderson, @bshand, @TeeVar, "
        msg += "@SeanCresswell, @Jean-Louis, @Jan_Bos"
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "## DESCRIPTION"
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = "This tool is designed to help you find annoying little errors in your project.  "
        msg += "It can identify failed features in 3D models, detached dimensions in drawings, "
        msg += "missing parts in assemblies, and more.  It can also update certain individual "
        msg += "file settings to match those in a template you specify."
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "## INSTALLATION"
        readme_github.Add(msg)
        msg = "There is no installation per se.  The preferred method is to download or clone "
        msg += "the project and compile it yourself."
        readme_github.Add(msg)
        msg = ""
        readme_github.Add(msg)

        msg = "The other option is to use the latest released version here "
        msg += "https://github.com/rmcanany/SolidEdgeHousekeeper/releases  "
        msg += "Click the latest release, then from the Assets list, click the SolidEdgeHousekeeper zip file.  "
        msg += "It should prompt you to save it.  "
        msg += "Choose a convenient location on your machine.  "
        msg += "Extract the zip file (probably by right-clicking and selecting Extract All).  "
        msg += "Double-click the .exe file to run."
        readme_github.Add(msg)
        msg = ""
        readme_github.Add(msg)

        msg = "## OPERATION"
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "On each file type's tab, select which errors to detect.  "
        msg += "On the General tab, browse to the desired input folder, "
        msg += "then select the desired file search option.  "
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = "You can refine the search using a file filter, a property filter, or both.  "
        msg += "See the file selection section for details.  "
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "If any errors are found, a log file will be written to the input folder.  "
        msg += "It will identify each error and the file in which it occurred.  "
        msg += "When processing is complete, a message box will give you the file name."
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "The first time you use the program, some site-specific information is needed.  "
        msg += "This includes the location of your templates, material table, etc.  "
        msg += "These are populated on the Configuration Tab."
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "You can interrupt the program before it finishes.  While processing, the Cancel button changes to a Stop button.  Just click that to halt processing.  It may take several seconds to register the request.  It doesn't hurt to click it a couple of times."
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "## CAVEATS"
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = "Since the program can process a large number of files in a short amount of time, "
        msg += "it can be very taxing on Solid Edge.  "
        msg += "To maintain a clean environment, the program restarts Solid Edge periodically.  "
        msg += "This is by design and does not necessarily indicate a problem."
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "However, problems can arise.  "
        msg += "Those cases will be reported in the log file with the message 'Error processing file'.  "
        msg += "A stack trace will be included.  The stack trace looks scary, but may be useful for program debugging.  "
        msg += "If four of these errors are detected in a run, the programs halts with the "
        msg += "Status Bar message 'Processing aborted'."
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "Please note this is not a perfect program.  It is not guaranteed not to mess up your files.  Back up any files before using it."
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "## KNOWN ISSUES"
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "Does not support managed files.  Cause: Unknown.  "
        msg += "Possible workaround: Process the files in an unmanaged workspace.   "
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "Some tasks may not support versions of Solid Edge prior to SE2020.  "
        msg += "Cause: Maybe an API call not available in previous versions.  "
        msg += "Possible workaround: Use SE2020 or later.  "
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "May not support multiple installed Solid Edge versions on the same machine.  "
        msg += "Cause: Unknown.  "
        msg += "Possible workaround: Use the version that was 'silently' installed.  "
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)


        msg = vbCrLf + "FILE SELECTION"
        readme_tab.Add(msg)

        msg = "There are four file selection options: "
        msg += "'Directory', 'Subdirectories', 'Top level assembly', and 'TODO list'.  "
        msg += "The first two are hopefully self-explanatory.  The other two are described next.  "
        readme_tab.Add(msg)

        msg = vbCrLf + "Top Level Assembly"
        readme_tab.Add(msg)

        msg = "This option uses an assembly file as a starting point and "
        msg += "finds all links to it.  "
        msg += "The search scope is any file within or below the current input directory."
        msg += "The search can be conducted in one of two ways, bottom up or top down.  "
        msg += "These options are set on the Configuration tab and are described next."
        readme_tab.Add(msg)

        msg = vbCrLf + "Bottom up is meant for general purpose directories (e.g., \\BIG_SERVER\all_parts\).  "
        msg += "The program gets links by recursion, then "
        msg += "finds draft files with 'Where Used'.  "
        readme_tab.Add(msg)

        msg = vbCrLf + "Top down is meant for self-contained project directories (e.g., C:\Projects\Project123\).  "
        msg += "The program checks every file within and below the input directory.  "
        msg += "As it does, it creates a graph of the links.  "
        msg += "The graph is subsequently traversed to find related files.  "
        msg += "I don't know how it works; my son did that part.  "
        readme_tab.Add(msg)

        msg = "A top down search can optionally report files with no links to the top level assembly.  "
        readme_tab.Add(msg)

        msg = vbCrLf + "Most file selection options occur as soon as they are selected.  "
        msg += "A top-level assembly search is different.  "
        msg += "Since it can be somewhat time consuming, the update is only conducted after "
        msg += "clicking the Update file list button.  "
        readme_tab.Add(msg)

        msg = vbCrLf + "TODO List"
        readme_tab.Add(msg)

        msg = "The 'TODO List' option uses output from a previous run as a starting point.  "
        msg += "To create the list, check the Create TODO list checkbox before processing files.  "
        msg += "Files with any errors will be added to the list.  "
        msg += "To then process those files, select 'TODO List' as the files-to-process option.  "
        msg += "This option is intended mainly to work with Interactive Edit, but can be used with any task.  "
        readme_tab.Add(msg)

        msg = vbCrLf + "The file list is stored in the Housekeeper directory in a text file named 'todo.txt'.  "
        msg += "If you have a file list from another source, you can save it in the text file and process it as above."
        readme_tab.Add(msg)




        msg = vbCrLf + "FILTERING"
        readme_tab.Add(msg)

        msg = "You can refine the file list by file name, property value, or both.  "
        msg += "For details on the property search, see the Readme tab on the Property Filter dialog.  "
        readme_tab.Add(msg)

        msg = vbCrLf + "File filtering can occur on file extension, file wildcard, or both.  "
        msg += "Filtering by extension is done by checking/unchecking the appropriate extension.  "
        readme_tab.Add(msg)

        msg = vbCrLf + "Filtering by wildcard is done by entering the wildcard pattern in the provided textbox.  "
        msg += "Internally, it is implemented with the VB 'Like' operator, "
        msg += "which is similar to the old DOS wildcard search, but with a few more options.  "
        msg += "For details and examples, see "
        msg += "https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator."
        readme_tab.Add(msg)

        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)


        msg = "## DETAILS"
        readme_github.Add(msg)
        'readme_tab.Add(msg)

        msg = vbCrLf + "### TESTS AND ACTIONS"
        readme_github.Add(msg)
        readme_tab.Add(msg)
        readme_tab.Add("")
        For i As Integer = 0 To 3
            readme_github.Add(Names(i))
            readme_tab.Add(Names(i))
            readme_tab.Add("")
            'For Each Item In CheckBoxList(i).Items
            '    msg1.Add("    " + CStr(Item))
            '    msg2.Add("    " + CStr(Item))
            'Next
            If i = 0 Then
                For Each Key In LabelToActionAssembly.Keys
                    readme_github.Add("    " + Key)
                    readme_tab.Add("--" + Key)
                    If LabelToActionAssembly(Key).HelpText <> "" Then
                        readme_tab.Add(LabelToActionAssembly(Key).HelpText)
                    End If
                    readme_tab.Add("")
                Next
            ElseIf i = 1 Then
                For Each Key In LabelToActionPart.Keys
                    readme_github.Add("    " + Key)
                    readme_tab.Add("--" + Key)
                    If LabelToActionPart(Key).HelpText <> "" Then
                        readme_tab.Add(LabelToActionPart(Key).HelpText)
                    End If
                    readme_tab.Add("")
                Next
            ElseIf i = 2 Then
                For Each Key In LabelToActionSheetmetal.Keys
                    readme_github.Add("    " + Key)
                    readme_tab.Add("--" + Key)
                    If LabelToActionSheetmetal(Key).HelpText <> "" Then
                        readme_tab.Add(LabelToActionSheetmetal(Key).HelpText)
                    End If
                    readme_tab.Add("")
                Next
            Else  ' i = 3
                For Each Key In LabelToActionDraft.Keys
                    readme_github.Add("    " + Key)
                    readme_tab.Add("--" + Key)
                    If LabelToActionDraft(Key).HelpText <> "" Then
                        readme_tab.Add(LabelToActionDraft(Key).HelpText)
                    End If
                    readme_tab.Add("")
                Next


            End If

        Next


        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = "## CODE ORGANIZATION"
        readme_github.Add(msg)
        msg = "Processing starts in Form1.vb.  A short description of the code's organization can be found there."
        readme_github.Add(msg)

        For Each s As String In readme_tab
            readme_tab_formatted.Add(s.Replace("# ", "").Replace("#", ""))
        Next
        TextBoxReadme.Lines = readme_tab_formatted.ToArray

        ' The readme file is not needed on the user's machine.  
        ' The file name is hard coded, hopefully most users won't have this exact location on their machines.  
        ReadmeFileName = "D:\CAD\scripts\SolidEdgeHousekeeper\README.md"

        StartupPath = "D:\CAD\scripts\SolidEdgeHousekeeper\bin\Release"
        tf = FileIO.FileSystem.DirectoryExists(StartupPath)
        If tf Then
            IO.File.WriteAllLines(ReadmeFileName, readme_github)
        End If

    End Sub

End Class