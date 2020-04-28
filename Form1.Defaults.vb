Partial Class Form1

    Private Sub LoadDefaults()
        'See format example in SaveDefaults()

        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim Defaults As String() = Nothing
        Dim Key As String
        Dim Value As String

        DefaultsFilename = StartupPath + "\" + "defaults.txt"

        Try
            Defaults = IO.File.ReadAllLines(DefaultsFilename)
            Dim msg As String = ""
            For Each KVPair As String In Defaults
                Key = KVPair.Split("=")(0)
                Value = KVPair.Split("=")(1)

                If Key = TextBoxInputDirectory.Name Then
                    If FileIO.FileSystem.DirectoryExists(Value) Then
                        TextBoxInputDirectory.Text = Value
                    Else
                        TextBoxInputDirectory.Text = Application.StartupPath
                    End If

                ElseIf Key = TextBoxTemplateAssembly.Name Then
                    If FileIO.FileSystem.FileExists(Value) Then
                        TextBoxTemplateAssembly.Text = Value
                    Else
                        TextBoxTemplateAssembly.Text = ""
                    End If

                ElseIf Key = TextBoxTemplatePart.Name Then
                    If FileIO.FileSystem.FileExists(Value) Then
                        TextBoxTemplatePart.Text = Value
                    Else
                        TextBoxTemplatePart.Text = ""
                    End If

                ElseIf Key = TextBoxTemplateSheetmetal.Name Then
                    If FileIO.FileSystem.FileExists(Value) Then
                        TextBoxTemplateSheetmetal.Text = Value
                    Else
                        TextBoxTemplateSheetmetal.Text = ""
                    End If

                ElseIf Key = TextBoxTemplateDraft.Name Then
                    If FileIO.FileSystem.FileExists(Value) Then
                        TextBoxTemplateDraft.Text = Value
                    Else
                        TextBoxTemplateDraft.Text = ""
                    End If

                ElseIf Key = TextBoxActiveMaterialLibrary.Name Then
                    If FileIO.FileSystem.FileExists(Value) Then
                        TextBoxActiveMaterialLibrary.Text = Value
                    Else
                        TextBoxActiveMaterialLibrary.Text = ""
                    End If

                ElseIf Key = ComboBoxPartNumberPropertySet.Name Then
                    ComboBoxPartNumberPropertySet.Text = Value

                ElseIf Key = TextBoxPartNumberPropertyName.Name Then
                    TextBoxPartNumberPropertyName.Text = Value

                ElseIf Key = TextBoxRestartAfter.Name Then
                    If Value = "" Then
                        TextBoxRestartAfter.Text = 50
                    Else
                        Try
                            TextBoxRestartAfter.Text = Int(Value)
                        Catch ex As Exception
                            TextBoxRestartAfter.Text = 50
                        End Try
                    End If

                ElseIf Key = TextBoxLaserOutputDirectory.Name Then
                    If FileIO.FileSystem.DirectoryExists(Value) Then
                        TextBoxLaserOutputDirectory.Text = Value
                    Else
                        TextBoxLaserOutputDirectory.Text = Application.StartupPath
                    End If

                Else
                    PopulateCheckboxDefault(KVPair)
                End If
            Next
        Catch ex As Exception
            TextBoxInputDirectory.Text = Application.StartupPath
            'MsgBox("Exception" + ex.ToString)
        End Try

        UpdateTemplateRequired()

    End Sub

    Private Sub SaveDefaults()
        'Format Example
        'TextBoxInputDirectory=D:\CAD\scripts\test_files
        'TextBoxTemplateAssembly=C:\Program Files\Siemens\Solid Edge 2020\Template\ANSI Inch\ansi inch assembly.asm
        'TextBoxTemplatePart=C:\Program Files\Siemens\Solid Edge 2020\Template\ANSI Inch\ansi inch part.par
        'TextBoxTemplateSheetmetal=C:\Program Files\Siemens\Solid Edge 2020\Template\ANSI Inch\ansi inch sheet metal.psm
        'TextBoxTemplateDraft=C:\Program Files\Siemens\Solid Edge 2020\Template\ANSI Inch\Hallmark_B.dft
        'TextBoxActiveMaterialLibrary=C:\Program Files\Siemens\Solid Edge 2020\Preferences\Materials\hmk_Materials.mtl
        'ComboBoxPartNumberPropertySet=Custom
        'TextBoxPartNumberPropertyName=hmk_Part_Number
        'TextBoxRestartAfter=50
        'TextBoxLaserOutputDirectory=D:\CAD\scripts\test_files\20181212_auto_cube_fill\laser_files_robert
        'Assembly.Occurrence missing file=Checked
        'Assembly.Occurrence outside project directory=Unchecked
        'Assembly.Failed relationships=Unchecked

        Dim Defaults As New List(Of String)
        Dim msg As String

        Defaults.Add(TextBoxInputDirectory.Name + "=" + TextBoxInputDirectory.Text)
        Defaults.Add(TextBoxTemplateAssembly.Name + "=" + TextBoxTemplateAssembly.Text)
        Defaults.Add(TextBoxTemplatePart.Name + "=" + TextBoxTemplatePart.Text)
        Defaults.Add(TextBoxTemplateSheetmetal.Name + "=" + TextBoxTemplateSheetmetal.Text)
        Defaults.Add(TextBoxTemplateDraft.Name + "=" + TextBoxTemplateDraft.Text)
        Defaults.Add(TextBoxActiveMaterialLibrary.Name + "=" + TextBoxActiveMaterialLibrary.Text)
        Defaults.Add(ComboBoxPartNumberPropertySet.Name + "=" + ComboBoxPartNumberPropertySet.Text)
        Defaults.Add(TextBoxPartNumberPropertyName.Name + "=" + TextBoxPartNumberPropertyName.Text)
        Defaults.Add(TextBoxRestartAfter.Name + "=" + TextBoxRestartAfter.Text)
        Defaults.Add(TextBoxLaserOutputDirectory.Name + "=" + TextBoxLaserOutputDirectory.Text)

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

    Private Sub LoadTextBoxAbout()
        Dim msg As New List(Of String)
        Dim msg2 As New List(Of String)
        Dim ReadmeFileName As String = ""
        Dim FilenameList As New List(Of String)

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

        msg.Add("# Solid Edge Housekeeper")
        msg.Add("Robert McAnany 2020")
        msg.Add("")
        msg.Add("Portions adapted from code by Jason Newell, Greg Chasteen, Tushar Suradkar, and others.")
        msg.Add("")
        msg.Add("## DESCRIPTION")
        msg.Add("This tool is designed to help you find annoying little errors in your project.  It can identify failed features in 3D models, detached dimensions in drawings, missing parts in assemblies, and more.  It can also update certain individual file settings to match those in a template you specify.")
        msg.Add("")
        msg.Add("## INSTALLATION")
        msg.Add("There is no installation per se.  The preferred method is to download or clone the project and compile it yourself.")
        msg.Add("")
        msg.Add("Not everyone can do that, so the compiled executable is provided.  To get it, click the bin/Release folder above and select Housekeeper.exe.  On that page, click Download and save it to a convenient location.  Double-click the executable to run.")
        msg.Add("")
        msg.Add("## OPERATION")
        msg.Add("Select which errors to detect by clicking the appropriate checkboxes on each file type's tab.  Select which files to process by navigating to the desired input folder, and clicking the desired search type.")
        msg.Add("")
        msg.Add("If any errors are found, a text file will be written to the input folder.  It will identify each error and the file in which it occurred.  When processing is complete, a message box will give you the file name.")
        msg.Add("")
        msg.Add("The first time you use the program, you need to supply some user-specific information.  This includes the location of your templates, material table, and the like.  These are accessed on the Configuration Tab.")
        msg.Add("")
        msg.Add("## CAVEATS")
        msg.Add("Since the program can process a large number of files in a short amount of time, it can be very taxing on Solid Edge.  To maintain a clean environment, the program restarts Solid Edge periodically.  This is by design and does not necessarily indicate a problem.  ")
        msg.Add("")
        msg.Add("However, problems can arise.  Those cases will also be reported in the text file with the message 'Error processing file'.  A stack trace will be included, which may be useful for program debugging.")
        msg.Add("")
        msg.Add("Please note this is not a perfect program.  It is not guaranteed not to mess up your files.  Back up any files before using it.")
        msg.Add("")
        msg.Add("## DETAILS")
        msg.Add("Comprehensive list of implemented tests and actions.")
        For i As Integer = 0 To 3
            msg.Add(Names(i))
            For Each Item In CheckBoxList(i).Items
                msg.Add("    " + Item)
            Next
        Next
        msg.Add("")
        msg.Add("## CODE ORGANIZATION")
        msg.Add("Processing starts in Form1.vb.  A short description of the code's organization can be found there.")

        For Each s As String In msg
            msg2.Add(s.Replace("# ", "").Replace("#", ""))
        Next
        TextBoxReadme.Lines = msg2.ToArray

        FilenameList = System.IO.Path.GetDirectoryName(DefaultsFilename).Split("\").ToList
        For i As Integer = 0 To 1  ' Remove the last two directories
            FilenameList.RemoveAt(FilenameList.Count - 1)
        Next
        For i As Integer = 0 To FilenameList.Count - 1
            ReadmeFileName += FilenameList(i) + "\"
        Next
        ReadmeFileName += "README.md"

        IO.File.WriteAllLines(ReadmeFileName, msg)

    End Sub

End Class