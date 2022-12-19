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

            ElseIf TypeOf Ctrl Is ListView Then
                Dim c As ListView = CType(Ctrl, ListView)
                Dim s1 As String
                Dim s2 As String

                '.Groups.Items
                '(0) {Files sources}
                '  .Name "Sources"
                '  .Items
                '    (0) {Text = "Top level assembly"}
                '      .Name "D:\CAD\scripts\test_files\20181212_auto_cube_fill\7481 AUTO CUBE FILL 2021\7481-00000_AUTO_CUBE_FILL_2021.asm"
                '    (1) {Text = "Top level asm folder"}
                '    (2) {Text = "Folder"}
                '    (3) {Text = "Folder with subfolders"}
                '    (4) {Text = "Top level asm folder"}
                '    (5) {Text = "Folder"}
                '    (6) {Text = "Folder with subfolders"}
                '(1)	{Excluded files}
                '(2)	{Assemblies}
                '(3)	{Parts}
                '(4)	{Sheetmetals}
                '(5)	{Drafts}

                For i As Integer = 0 To c.Groups.Count - 1
                    If c.Groups(i).Name = "Sources" Then
                        For j As Integer = 0 To c.Groups(i).Items.Count - 1
                            s1 = String.Format("{0}.{1}.{2}", c.Name, c.Groups(i).Items(j).Text, j)
                            s2 = c.Groups(i).Items(j).Name
                            Configuration.Add(s1, s2)
                        Next
                        s1 = ""
                    End If

                Next
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

        ExcludeControls.Add(new_CheckBoxEnablePropertyFilter.Name)
        ExcludeControls.Add(TextBoxReadme.Name)
        ' ExcludeControls.Add(ListViewFiles.Name)

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

        tf = tb.Name.ToLower.Contains("formula")
        If tf Then
            tb.Text = Value
        End If

        tf = tb.Name.ToLower.Contains("template")
        tf = tf Or tb.Name.ToLower.Contains("materiallibrary")
        tf = tf Or tb.Name.ToLower.Contains("toplevelassembly")
        tf = tf Or tb.Name.ToLower.Contains("externalprogram")
        tf = tf Or tb.Name.ToLower.Contains("fastsearchscope")
        tf = tf Or tb.Name.ToLower.Contains("watermarkfilename")
        If tf Then
            If FileIO.FileSystem.FileExists(Value) Then
                tb.Text = Value
            Else
                tb.Text = ""
            End If
        End If

        tf = tb.Name.ToLower.Contains("watermarkscale")
        tf = tf Or tb.Name.ToLower.Contains("watermarkx")
        tf = tf Or tb.Name.ToLower.Contains("watermarky")
        If tf Then
            If Value = "" Then
                tb.Text = "0.5"
            Else
                Try
                    tb.Text = CStr(Value)
                Catch ex As Exception
                    tb.Text = "0.5"
                End Try
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
        tf = tf Or tb.Name.ToLower.Contains("printoptions")
        If tf Then
            tb.Text = Value
        End If

        tf = tb.Name.ToLower.Contains("exposevariables")
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
        ' FileTypesString += ":bmp (*.bmp):jpg (*.jpg):tif (*.tif)"
        FileTypesString += ":bmp (*.bmp):jpg (*.jpg):png (*.png):tif (*.tif)"

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
        ComboBoxSaveAsSheetmetalFileType.Items.Add("PDF Drawing (*.pdf)")
        ComboBoxSaveAsSheetmetalFileType.Items.Add("DXF Flat (*.dxf)")
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

    Private Sub CreateFilenameCharmap()

        Dim FCD As New FilenameCharmapDoctor()  ' Creates the file filename_charmap.txt if it does not exist.

        'Dim s As String
        's = FCD.SubstituteIllegalCharacters("<>:""/\|?*! ")
        's = FCD.SubstituteIllegalCharacters("L 3 X 6 X 1/4")

        'Dim msg As String = ""

        'Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        'Dim Charmap As String() = Nothing
        'Dim CharmapList As New List(Of String)

        'Dim CharmapFilename As String = String.Format("{0}\filename_charmap.txt", StartupPath)

        'Try
        '    Charmap = IO.File.ReadAllLines(CharmapFilename)
        'Catch ex As Exception
        '    ' File does not exist.  Create it.
        '    ' https://www.mtu.edu/umc/services/websites/writing/characters-avoid/

        '    CharmapList.Add("c This file contains a list of common illegal characters")
        '    CharmapList.Add("c for files and directories.")
        '    CharmapList.Add("c https://www.mtu.edu/umc/services/websites/writing/characters-avoid/")
        '    CharmapList.Add("c It covers operating systems such as Windows, Mac and Linux")
        '    CharmapList.Add("c and devices such as desktops, tablets and smartphones.")
        '    CharmapList.Add("c ")
        '    CharmapList.Add("c The format is <illegal character><space><replacement value>")
        '    CharmapList.Add("c ")
        '    CharmapList.Add("c So, '# my_replacement' means ")
        '    CharmapList.Add("c If '#' is found, replace it with 'my_replacement'.")
        '    CharmapList.Add("c ")
        '    CharmapList.Add("c (Note, in the default mapping below, the '.' is just text.")
        '    CharmapList.Add("c It doesn't have any special meaning or function.)")
        '    CharmapList.Add("c ")
        '    CharmapList.Add("c If you're just using Windows, you probably only have to define")
        '    CharmapList.Add("c replacements for the characters    < > : "" / \ | ? * !")
        '    CharmapList.Add("c and comment out or delete the rest.")
        '    CharmapList.Add("c ")
        '    CharmapList.Add("c (Actually Windows doesn't care about !, but Solid Edge does.")
        '    CharmapList.Add("c It used to identify Assembly Family members.)")
        '    CharmapList.Add("c ")
        '    CharmapList.Add("c If you mess up, you can delete this file and Housekeeper will")
        '    CharmapList.Add("c regenerate it next time you start the program.")
        '    CharmapList.Add("c ")
        '    CharmapList.Add("c To comment out a line (so Housekeeper ignores it) start the line")
        '    CharmapList.Add("c with 'c' as done in this header text.")
        '    CharmapList.Add("c ")
        '    CharmapList.Add("c There is no error checking when the program reads in this file.")
        '    CharmapList.Add("c So don't do stuff like '* ?' or '   c This is too complicated'.")
        '    CharmapList.Add("c ")
        '    CharmapList.Add("c ")
        '    CharmapList.Add("c Character mapping below")
        '    CharmapList.Add("")

        '    CharmapList.Add("# .pound.")
        '    CharmapList.Add("% .percent.")
        '    CharmapList.Add("& .ampersand.")
        '    CharmapList.Add("{ .leftcurlybracket.")
        '    CharmapList.Add("} .rightcurlybracket.")
        '    CharmapList.Add("\ .backslash.")
        '    CharmapList.Add("< .leftanglebracket.")
        '    CharmapList.Add("> .rightanglebracket.")
        '    CharmapList.Add("* .asterisk.")
        '    CharmapList.Add("? .questionmark.")
        '    CharmapList.Add("/ .forwardslash.")
        '    CharmapList.Add("  .blankspace.")
        '    CharmapList.Add("$ .dollarsign.")
        '    CharmapList.Add("! .exclamationpoint.")
        '    CharmapList.Add("' .singlequote.")

        '    'CharmapList.Add("".doublequote.")
        '    CharmapList.Add(String.Format("{0} {1}", Chr(34), ".doublequote."))

        '    CharmapList.Add(": .colon.")
        '    CharmapList.Add("@ .atsign.")
        '    CharmapList.Add("+ .plussign.")
        '    CharmapList.Add("` .backtick.")
        '    CharmapList.Add("| .pipe.")
        '    CharmapList.Add("= .equalsign.")

        '    IO.File.WriteAllLines(CharmapFilename, CharmapList)

        '    'Dim msg As String = ""
        '    'For Each s As String In CharmapList
        '    '    msg = String.Format("{0}{1}{2}", msg, s, vbCrLf)
        '    'Next
        '    'MsgBox(msg)

        'End Try

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

        CreateFilenameCharmap()

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

                        If Key = "new_ComboBoxFileSearch" Then
                            If Value.Contains("new_ComboBoxFileSearchItem.") Then
                                c.Items.Add(Value.Replace("new_ComboBoxFileSearchItem.", ""))
                            Else
                                c.Text = Value
                            End If
                        Else
                            c.Text = Value
                        End If
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
            'TextBoxInputDirectory.Text = Application.StartupPath
            ' If the file 'defaults.txt' does not exist, this is the first run of the program.
            ' In that case, display the Readme Tab.
            ' TODO: Figure out how to un-highlight the text
            TabControl1.SelectedTab = TabControl1.TabPages("TabPageReadme")
            'TextBoxReadme.SelectAll()
            'TextBoxReadme.DeselectAll()
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

                If c.Name = "new_ComboBoxFileSearch" Then
                    If c.Items.Count > 0 Then
                        For i As Integer = 0 To c.Items.Count - 1
                            Dim Value As String = c.Items(i).ToString
                            Defaults.Add(String.Format("{0}=new_ComboBoxFileSearchItem.{1}", c.Name, Value))

                        Next
                    End If
                End If

                Defaults.Add(String.Format("{0}={1}", c.Name, c.Text))
            End If
        Next


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
        Dim ReadmeFileName As String = "D:\CAD\scripts\SolidEdgeHousekeeper\README.md"
        Dim StartupPath As String = "D:\CAD\scripts\SolidEdgeHousekeeper\bin\Release"
        Dim TaskListHeader As String = "## TASK DESCRIPTIONS"
        Dim Proceed As Boolean = True
        Dim i As Integer
        Dim msg As String

        Dim ReadmeIn As String() = Nothing
        Dim ReadmeOut As New List(Of String)

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


        ' The readme file is not needed on the user's machine.  
        ' The startup path is hard coded, hopefully most users won't have this exact location on their machines.  
        If FileIO.FileSystem.DirectoryExists(StartupPath) Then
            Try
                ReadmeIn = IO.File.ReadAllLines(ReadmeFileName)
            Catch ex As Exception
                MsgBox(String.Format("Error opening {0}", ReadmeFileName))
                Proceed = False
            End Try
        Else
            Proceed = False
        End If

        If Proceed Then
            For i = 0 To ReadmeIn.Count - 1
                If Not ReadmeIn(i).Contains(TaskListHeader) Then
                    ReadmeOut.Add(ReadmeIn(i))
                Else
                    Exit For
                End If
            Next

            ReadmeOut.Add(TaskListHeader)
            ReadmeOut.Add("")

            For i = 0 To 3
                ReadmeOut.Add(Names(i))
                ReadmeOut.Add("")

                If i = 0 Then
                    For Each Key In LabelToActionAssembly.Keys
                        ReadmeOut.Add(String.Format("#### {0}", Key))
                        If LabelToActionAssembly(Key).HelpText <> "" Then
                            ReadmeOut.Add(LabelToActionAssembly(Key).HelpText)
                        End If
                        ReadmeOut.Add("")
                    Next
                ElseIf i = 1 Then
                    For Each Key In LabelToActionPart.Keys
                        ReadmeOut.Add(String.Format("#### {0}", Key))
                        If LabelToActionPart(Key).HelpText <> "" Then
                            ReadmeOut.Add(LabelToActionPart(Key).HelpText)
                        End If
                        ReadmeOut.Add("")
                    Next
                ElseIf i = 2 Then
                    For Each Key In LabelToActionSheetmetal.Keys
                        ReadmeOut.Add(String.Format("#### {0}", Key))
                        If LabelToActionSheetmetal(Key).HelpText <> "" Then
                            ReadmeOut.Add(LabelToActionSheetmetal(Key).HelpText)
                        End If
                        ReadmeOut.Add("")
                    Next
                Else  ' i = 3
                    For Each Key In LabelToActionDraft.Keys
                        ReadmeOut.Add(String.Format("#### {0}", Key))
                        If LabelToActionDraft(Key).HelpText <> "" Then
                            ReadmeOut.Add(LabelToActionDraft(Key).HelpText)
                        End If
                        ReadmeOut.Add("")
                    Next


                End If

            Next


            msg = ""
            ReadmeOut.Add("")
            msg = "## CODE ORGANIZATION"
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")
            msg = "Processing starts in Form1.vb.  A short description of the code's organization can be found there."
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")


            IO.File.WriteAllLines(ReadmeFileName, ReadmeOut)


        End If


    End Sub

    Private Sub LoadTextBoxReadme_OLD()
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

        Names.Add("### ASSEMBLY")
        Names.Add("### PART")
        Names.Add("### SHEETMETAL")
        Names.Add("### DRAFT")

        msg = "# Solid Edge Housekeeper v0.1.10.3"
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = "Robert McAnany 2022"
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
        msg += "@SeanCresswell, @Jean-Louis, @Jan_Bos, @MonkTheOCD_Engie, @[mike miller], "
        msg += "@Fiorini, @[Martin Bernhard], @Derek G, @Chris42, @Jason1607436093479, @Bob Henry, "
        msg += "@JayJay101"
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

        msg = "## GETTING HELP"
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = "Ask questions or suggest improvements on the Solid Edge Forum: "
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = "https://community.sw.siemens.com/s/topic/0TO4O000000MihiWAC/solid-edge"
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = "To subscribe to update notices or volunteer to be a beta tester, message me, RobertMcAnany, on the forum.  "
        msg += "(Click your profile picture, then 'My Messages', then 'Create').  "
        msg += "Unsubscribe the same way.  "
        msg += "To combat bots and spam, I will probably ignore requests from 'User16612341234...'.  "
        msg += "(Change your nickname by clicking your profile picture, then 'My Profile', then 'Edit').  "
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "## INSTALLATION"
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = "There is no installation per se.  The preferred method is to download or clone "
        msg += "the project and compile it yourself."
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "The other option is to use the latest released version here "
        msg += "https://github.com/rmcanany/SolidEdgeHousekeeper/releases  "
        msg += "Under the latest release, click the SolidEdgeHousekeeper-vx.x.x.zip file "
        msg += "(sometimes hidden under the Assets dropdown).  "
        msg += "It should prompt you to save it.  "
        msg += "Choose a convenient location on your machine.  "
        msg += "Extract the zip file (probably by right-clicking and selecting Extract All).  "
        msg += "Double-click the .exe file to run."
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "If you are upgrading from a previous release, you should be able to copy "
        msg += "the settings files from the old version to the new.   "
        msg += "The files are 'defaults.txt', 'property_filters.txt', and 'filename_charmap.txt'.  "
        msg += "If you haven't used Property Filter, 'property_filters.txt' won't be there.  "
        msg += "Versions prior to 0.1.10 won't have 'filename_charmap.txt' either.  "
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

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
        msg += "When processing is complete, the log file is opened in Notepad for review."
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
        msg = "Update 10/10/2021: Some users have reported success with BiDM managed files.  "
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = "Update 1/25/2022: One user has reported success with Teamcenter 'cached' files.  "
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

        msg = "Does not support all printer settings, e.g., duplexing, collating, etc.  "
        msg += "Cause: Not exposed in the DraftPrintUtility() API.  "
        msg += "Possible workaround: Create a new Windows printer with the desired settings.  "
        msg += "Refer to the TESTS AND ACTIONS topic below for more details.  "
        readme_github.Add(msg)
        readme_tab.Add(msg)
        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)

        msg = "Pathfinder is sometimes blank when running the 'Interactive Edit' task.  "
        msg += "Cause: Unknown.  "
        msg += "Possible workaround: Refresh the screen by minimizing and maximizing the Solid Edge window.  "
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

        msg = vbCrLf + "A bottom up search requires a valid Fast Search Scope filename, "
        msg += "(e.g., C:\Program Files\Siemens\Solid Edge 2021\Preferences\FastSearchScope.txt).  "
        msg += "It is set on the Configuration tab, and tells the program "
        msg += "if the input directory is on an indexed drive.  "
        readme_tab.Add(msg)

        msg = vbCrLf + "Top down is meant for self-contained project directories (e.g., C:\Projects\Project123\).  "
        msg += "The program opens every file within and below the input directory.  "
        msg += "As it does, it creates a graph of the links.  "
        msg += "The graph is subsequently traversed to find related files.  "
        msg += "I don't know how it works; my son did that part.  "
        readme_tab.Add(msg)

        msg = vbCrLf + "A top down search can optionally report files with no links to the top level assembly.  "
        readme_tab.Add(msg)

        msg = vbCrLf + "Most file selection options occur as soon as they are selected.  "
        msg += "A top-level assembly search is different.  "
        msg += "Since it can be somewhat time consuming, the update is only conducted after "
        msg += "clicking the Update File List button.  "
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

        msg = vbCrLf + "Filtering by wildcard is done by entering the wildcard pattern in the provided combobox.  "
        msg += "Wildcard patterns are automatically saved for future use.  "
        msg += "Delete a pattern that is no longer needed by selecting it and clicking Delete.  "
        msg += vbCrLf + "Internally, the search is implemented with the VB 'Like' operator, "
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
        'readme_tab.Add(msg)
        msg = "## CODE ORGANIZATION"
        readme_github.Add(msg)
        msg = "Processing starts in Form1.vb.  A short description of the code's organization can be found there."
        readme_github.Add(msg)

        'msg = ""
        'readme_github.Add(msg)
        'readme_tab.Add(msg)
        'msg = "## ABOUT ME"
        'readme_github.Add(msg)
        'readme_tab.Add(msg)

        'msg = "A coworker saw this program and said, 'Do you have Obsessive Compulsive Disorder?'  "
        'msg += "I said, 'There's nothing disorderly about it.'  "
        'readme_github.Add(msg)
        'readme_tab.Add(msg)

        'msg = ""
        'readme_github.Add(msg)
        'readme_tab.Add(msg)
        'msg = "I had a colonoscopy the other day.  "
        'msg += "Afterwards the nurse said, 'You look like John Lithgow.'  "
        'msg += "I said, 'You mean my face?'  "
        'readme_github.Add(msg)
        'readme_tab.Add(msg)

        'msg = ""
        'readme_github.Add(msg)
        'readme_tab.Add(msg)
        'msg = "The HR Department asked for our preferred pronouns.  "
        'msg += "I put 'we/us'.  "
        'readme_github.Add(msg)
        'readme_tab.Add(msg)

        msg = ""
        readme_github.Add(msg)
        readme_tab.Add(msg)


        For Each s As String In readme_tab
            readme_tab_formatted.Add(s.Replace("# ", "").Replace("#", ""))
        Next
        If Not TextBoxReadme.Font.Size = CSng(TextBoxFontSize.Text) Then
            TextBoxReadme.Font = New Font("Microsoft Sans Serif", CSng(TextBoxFontSize.Text), FontStyle.Regular)
        End If

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