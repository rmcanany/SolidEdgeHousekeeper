Option Strict On

Public Class UtilsDefaults

    Public Property Form1 As Form1
    Public Property DefaultsFilename As String

    Public Sub New(_Form1 As Form1)

        Me.Form1 = _Form1

    End Sub

    Public Function GetConfiguration() As Dictionary(Of String, String)
        Dim Configuration As New Dictionary(Of String, String)
        Dim ControlDict As New Dictionary(Of String, Control)
        Dim Ctrl As Control

        ControlDict = RecurseFormControls(Me.Form1, ControlDict, False)

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

    Public Function RecurseFormControls(Ctrl As Control,
                                         ControlDict As Dictionary(Of String, Control),
                                         Exclude As Boolean
                                         ) As Dictionary(Of String, Control)

        Dim ChildControl As Control
        Dim tf As Boolean
        Dim ExcludeControls As New List(Of String)

        ExcludeControls.Add(Form1.new_CheckBoxEnablePropertyFilter.Name)
        ' ExcludeControls.Add(ListViewFiles.Name)

        tf = TypeOf Ctrl Is ContainerControl
        tf = tf Or TypeOf Ctrl Is TabControl
        tf = tf Or TypeOf Ctrl Is TabPage
        tf = tf Or TypeOf Ctrl Is GroupBox
        tf = tf Or TypeOf Ctrl Is ExTableLayoutPanel
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

    Public Sub PopulateTextBox(tb As TextBox, Value As String, StartupPath As String)
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


    Public Sub LoadDefaults()
        'See format example in SaveDefaults()

        Dim UP As New UtilsPreferences

        Dim StartupPath As String = UP.GetStartupDirectory()
        Dim PreferencesPath As String = UP.GetPreferencesDirectory()
        Dim Defaults As String() = Nothing
        Dim Key As String
        Dim Value As String
        Dim msg As String
        Dim tf As Boolean

        Dim ControlDict As New Dictionary(Of String, Control)
        Dim Ctrl As Control

        Dim KVPairList As New List(Of String)

        DefaultsFilename = String.Format("{0}/defaults.txt", PreferencesPath)

        'CreateFilenameCharmap()

        PopulateComboBoxes()

        ControlDict = RecurseFormControls(Me.Form1, ControlDict, True)

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
                    Form1.new_ComboBoxFileSearch.Items.Add(Value)
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
                    tf = tf And Form1.CheckBoxRememberTasks.Checked
                    If tf Then
                        PopulateCheckboxDefault(KVPair)
                    End If
                End If
            Next

        Catch ex As Exception
            MsgBox("Some form defaults were not restored.  The program will continue.  Please verify settings.")
        End Try

        Form1.ReconcileFormChanges()

    End Sub


    Public Sub SaveDefaults()
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

        ControlDict = RecurseFormControls(Form1, ControlDict, True)

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
        If Form1.new_ComboBoxFileSearch.Items.Count > 0 Then
            For i As Integer = 0 To Form1.new_ComboBoxFileSearch.Items.Count - 1
                Dim Value As String = Form1.new_ComboBoxFileSearch.Items(i).ToString
                Defaults.Add(String.Format("new_ComboBoxFileSearch=new_ComboBoxFileSearchItem.{0}", Value))
            Next
        End If

        Dim UP As New UtilsPreferences
        Dim PreferencesPath As String = UP.GetPreferencesDirectory()
        DefaultsFilename = String.Format("{0}/defaults.txt", PreferencesPath)

        IO.File.WriteAllLines(DefaultsFilename, Defaults)

    End Sub

    Public Sub PopulateCheckboxDefault(KVPair As String)
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

End Class
