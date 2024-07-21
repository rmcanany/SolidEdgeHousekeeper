Option Strict On
Imports System.Security.Cryptography
Imports System.Threading

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
        Dim AlreadyProcessed As Boolean = False

        tf = tb.Name.ToLower.Contains("directory")
        If tf Then
            If FileIO.FileSystem.DirectoryExists(Value) Then
                tb.Text = Value
            Else
                tb.Text = StartupPath
            End If
            AlreadyProcessed = True
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
            AlreadyProcessed = True
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
            AlreadyProcessed = True
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
            AlreadyProcessed = True
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
            AlreadyProcessed = True
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
            AlreadyProcessed = True
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
            AlreadyProcessed = True
        End If

        tf = tb.Name.ToLower.Contains("interference")
        If tf Then
            If Value = "" Then
                tb.Text = "1000"
            Else
                Try
                    tb.Text = CStr(Value)
                Catch ex As Exception
                    tb.Text = "1000"
                End Try
            End If
            AlreadyProcessed = True
        End If

        'TextBoxPrinter2Copies
        tf = tb.Name.ToLower.Contains("printer1copies")
        tf = tf Or tb.Name.ToLower.Contains("printer2copies")
        If tf Then
            If Value = "" Then
                tb.Text = "1"
            Else
                Try
                    tb.Text = CStr(Value)
                Catch ex As Exception
                    tb.Text = "1"
                End Try
            End If
            AlreadyProcessed = True
        End If


        If Not AlreadyProcessed Then
            tb.Text = Value
        End If


    End Sub

    Private Sub PopulateComboBoxes()

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

        Dim KVPairList As New List(Of String)

        DefaultsFilename = StartupPath + "\Preferences\" + "defaults.txt"

        'CreateFilenameCharmap()

        PopulateComboBoxes()

        ControlDict = RecurseFormControls(Me, ControlDict, True)

        Try
            Defaults = IO.File.ReadAllLines(DefaultsFilename)
            msg = ""
            For Each KVPair As String In Defaults
                If Not KVPair.Contains("=") Then
                    Continue For
                End If

                KVPairList = KVPair.Split("="c).ToList

                Key = KVPairList(0)

                ' Deal with multiple "=" characters in the input.
                Value = ""

                If KVPairList.Count = 2 Then
                    Value = KVPairList(1)
                End If
                If KVPairList.Count > 2 Then
                    For i As Integer = 1 To KVPairList.Count - 1
                        If i = 1 Then
                            Value = KVPairList(i)
                        Else
                            Value = String.Format("{0}={1}", Value, KVPairList(i))
                        End If
                    Next
                End If


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
            MsgBox("Some form defaults were not restored.  The program will continue.  Please verify settings.")
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


        IO.File.WriteAllLines(DefaultsFilename, Defaults)

    End Sub

    Private Sub PopulateCheckboxDefault(KVPair As String)
        'See format example in SaveDefaults

        Dim KeyAndName As String
        Dim Key As String
        Dim Value As String
        Dim CheckBoxName As String
        Dim ValueAsBoolean As Boolean

        KeyAndName = KVPair.Split(("=").ToCharArray(0, 1))(0)
        Key = KeyAndName.Split((".").ToCharArray(0, 1))(0)
        CheckBoxName = KeyAndName.Split((".").ToCharArray(0, 1))(1)
        Value = KVPair.Split(("=").ToCharArray(0, 1))(1)

        If Value = "Checked" Then
            ValueAsBoolean = True
        Else
            ValueAsBoolean = False
        End If

    End Sub

    Private Sub BuildReadmeFile()

        Dim ReadmeFileName As String = "C:\data\CAD\scripts\SolidEdgeHousekeeper\README.md"
        Dim VersionSpecificReadmeFileName = ReadmeFileName.Replace(".md", String.Format("-{0}.md", Me.Version))

        ' StartupPath is hard coded so this doesn't do anything on a user's machine
        Dim StartupPath As String = "C:\data\CAD\scripts\SolidEdgeHousekeeper\bin\Release"

        Dim TaskListHeader As String = "<!-- Start -->"
        Dim Proceed As Boolean = True
        Dim i As Integer
        Dim msg As String

        Dim ReadmeIn As String() = Nothing
        Dim ReadmeOut As New List(Of String)


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

            Dim PU As New PreferencesUtilities
            Dim tmpTaskList = PU.BuildTaskListFromScratch

            For Each Task As Task In tmpTaskList
                ReadmeOut.Add(String.Format("### {0}", Task.Description))
                ReadmeOut.Add(Task.HelpText)
                ReadmeOut.Add("")
            Next

            ReadmeOut.Add("")
            msg = "## KNOWN ISSUES"
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            msg = "**The program is not perfect**"
            ReadmeOut.Add(msg)
            msg = "- *Cause*: The programmer is not perfect."
            ReadmeOut.Add(msg)
            msg = "- *Possible workaround*: Back up any files before using it.  The program can process a large number of files in a short amount of time.  It can do damage at the same rate.  It has been tested on thousands of our files, but none of yours.  So, you know, back up any files before using it.  "
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            msg = "**Does not support managed files**"
            ReadmeOut.Add(msg)
            msg = "- *Cause*: Unknown."
            ReadmeOut.Add(msg)
            msg = "- *Possible workaround*: Process the files in an unmanaged workspace."
            ReadmeOut.Add(msg)
            msg = "- *Update 10/10/2021* Some users have reported success with BiDM managed files."
            ReadmeOut.Add(msg)
            msg = "- *Update 1/25/2022* One user has reported success with Teamcenter 'cached' files."
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            msg = "**Some tasks cannot run on older Solid Edge versions**"
            ReadmeOut.Add(msg)
            msg = "- *Cause*: Probably an API call not available in previous versions."
            ReadmeOut.Add(msg)
            msg = "- *Possible workaround*: Use the latest version, or avoid use of the task causing problems."
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            msg = "**May not support multiple installed Solid Edge versions**"
            ReadmeOut.Add(msg)
            msg = "- *Cause*: Unknown."
            ReadmeOut.Add(msg)
            msg = "- *Possible workaround*: Use the version that was 'silently' installed."
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            msg = "**Pathfinder sometimes blank during Interactive Edit**"
            ReadmeOut.Add(msg)
            msg = "- *Cause*: Unknown."
            ReadmeOut.Add(msg)
            msg = "- *Possible workaround*: Refresh the screen by minimizing and maximizing the Solid Edge window."
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")


            msg = ""
            ReadmeOut.Add("")
            msg = "## CODE ORGANIZATION"
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")
            msg = "Processing starts in Form1.vb.  A short description of the code's organization can be found there."
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")


            IO.File.WriteAllLines(ReadmeFileName, ReadmeOut)
            IO.File.WriteAllLines(VersionSpecificReadmeFileName, ReadmeOut)

        End If


    End Sub


End Class