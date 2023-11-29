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
        ' ExcludeControls.Add(ListViewFiles.Name)

        tf = TypeOf Ctrl Is ContainerControl
        tf = tf Or TypeOf Ctrl Is TabControl
        tf = tf Or TypeOf Ctrl Is TabPage
        tf = tf Or TypeOf Ctrl Is GroupBox
        'tf = Ctrl.HasChildren

        If tf Then
            For Each ChildControl In Ctrl.Controls
                ControlDict = RecurseFormControls(ChildControl, ControlDict, Exclude)
            Next
        Else
            tf = TypeOf Ctrl Is Button  ' Don't need to save buttons or labels.
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

        tf = tb.Name.ToLower.Contains("randomsample")
        If tf Then
            If Value = "" Then
                tb.Text = "0.1"
            Else
                Try
                    tb.Text = CStr(Value)
                Catch ex As Exception
                    tb.Text = "0.1"
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
        Dim FileTypesString As String
        Dim FileType As String

        ' Common model file types
        FileTypesString = "Step (*.stp):IGES (*.igs):Parasolid Text (*.x_t):Parasolid Binary (*.x_b)"
        FileTypesString += ":OBJ (*.obj):STL (*.stl)"
        FileTypesString += ":bmp (*.bmp):jpg (*.jpg):png (*.png):tif (*.tif)"

        ' Assembly
        ComboBoxSaveAsAssemblyFileType.Items.Clear()
        For Each FileType In Split(FileTypesString, Delimiter:=":")
            ComboBoxSaveAsAssemblyFileType.Items.Add(FileType)
        Next
        ComboBoxSaveAsAssemblyFileType.Items.Add("Copy (*.asm)")

        ComboBoxSaveAsAssemblyFileType.Text = CType(ComboBoxSaveAsAssemblyFileType.Items(0), String)

        ' Part
        ComboBoxSaveAsPartFileType.Items.Clear()
        For Each FileType In Split(FileTypesString, Delimiter:=":")
            ComboBoxSaveAsPartFileType.Items.Add(FileType)
        Next
        ComboBoxSaveAsPartFileType.Items.Add("Copy (*.par)")

        ComboBoxSaveAsPartFileType.Text = CType(ComboBoxSaveAsPartFileType.Items(0), String)

        ' Sheetmetal
        ComboBoxSaveAsSheetmetalFileType.Items.Clear()
        For Each FileType In Split(FileTypesString, Delimiter:=":")
            ComboBoxSaveAsSheetmetalFileType.Items.Add(FileType)
        Next
        ComboBoxSaveAsSheetmetalFileType.Items.Add("PDF Drawing (*.pdf)")
        ComboBoxSaveAsSheetmetalFileType.Items.Add("DXF Flat (*.dxf)")
        ComboBoxSaveAsSheetmetalFileType.Items.Add("Copy (*.psm)")
        ComboBoxSaveAsSheetmetalFileType.Text = CType(ComboBoxSaveAsSheetmetalFileType.Items(0), String)

        ' Draft
        FileTypesString = "PDF (*.pdf):DXF (*.dxf):DWG (*.dwg):IGES (*.igs)"

        ComboBoxSaveAsDraftFileType.Items.Clear()
        For Each FileType In Split(FileTypesString, Delimiter:=":")
            ComboBoxSaveAsDraftFileType.Items.Add(FileType)
        Next
        ComboBoxSaveAsDraftFileType.Items.Add("Copy (*.dft)")

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

        'ComboBoxFindReplacePropertySetDraft
        ComboBoxFindReplacePropertySetDraft.Items.Clear()
        For Each s As String In Split("System Custom")
            ComboBoxFindReplacePropertySetDraft.Items.Add(s)
        Next
        ComboBoxFindReplacePropertySetDraft.Text = CType(ComboBoxFindReplacePropertySetDraft.Items(0), String)


    End Sub

    Private Sub CreatePreferencesFolder()
        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim PreferencesFolder As String = String.Format("{0}\Preferences", StartupPath)
        If Not FileIO.FileSystem.DirectoryExists(PreferencesFolder) Then
            FileIO.FileSystem.CreateDirectory(PreferencesFolder)
        End If
    End Sub

    Private Sub CreateFilenameCharmap()
        Dim FCD As New FilenameCharmapDoctor()  ' Creates the file filename_charmap.txt if it does not exist.
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

        DefaultsFilename = StartupPath + "\Preferences\" + "defaults.txt"

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

                If Key = "new_ComboBoxFileSearch" Then
                    'Example format
                    'new_ComboBoxFileSearch = new_ComboBoxFileSearchItem.????-?????[!-]
                    'new_ComboBoxFileSearch = new_ComboBoxFileSearchItem.????-???00*.*

                    Value = Value.Replace("new_ComboBoxFileSearchItem.", "")
                    new_ComboBoxFileSearch.Items.Add(Value)
                    Continue For
                End If

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

                    ElseIf (TypeOf Ctrl Is ComboBox) Then
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

        End Try

        ReconcileFormChanges()

    End Sub


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

        ' The combobox new_ComboBoxFileSearch is not a Control, it is a ToolStripCombobox
        ' It has to be handled separately.
        If new_ComboBoxFileSearch.Items.Count > 0 Then
            For i As Integer = 0 To new_ComboBoxFileSearch.Items.Count - 1
                Dim Value As String = new_ComboBoxFileSearch.Items(i).ToString
                Defaults.Add(String.Format("new_ComboBoxFileSearch=new_ComboBoxFileSearchItem.{0}", Value))
            Next
        End If

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

    Private Sub BuildReadmeFile()
        Dim ReadmeFileName As String = "D:\CAD\scripts\SolidEdgeHousekeeper\README.md"
        Dim StartupPath As String = "D:\CAD\scripts\SolidEdgeHousekeeper\bin\Release"
        Dim TaskListHeader As String = "<!-- Start -->"
        Dim Proceed As Boolean = True
        Dim i As Integer
        Dim msg As String

        Dim ReadmeIn As String() = Nothing
        Dim ReadmeOut As New List(Of String)

        Dim L2AList As New List(Of LabelToAction)
        Dim L2AX As LabelToAction
        Dim Names As New List(Of String)

        L2AList.Add(LabelToActionAssembly)
        L2AList.Add(LabelToActionPart)
        L2AList.Add(LabelToActionSheetmetal)
        L2AList.Add(LabelToActionDraft)

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

            i = 0

            For Each L2AX In L2AList
                ReadmeOut.Add(Names(i))
                ReadmeOut.Add("")

                For Each Key In L2AX.Keys
                    ReadmeOut.Add(String.Format("#### {0}", Key))
                    If L2AX(Key).HelpText <> "" Then
                        ReadmeOut.Add(L2AX(Key).HelpText)
                    End If
                    ReadmeOut.Add("")
                Next

                i += 1
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


End Class