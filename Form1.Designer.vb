<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGeneral = New System.Windows.Forms.TabPage()
        Me.CheckBoxEnablePropertyFilter = New System.Windows.Forms.CheckBox()
        Me.ButtonUpdateListBoxFiles = New System.Windows.Forms.Button()
        Me.ButtonPropertyFilter = New System.Windows.Forms.Button()
        Me.ButtonTopLevelAssembly = New System.Windows.Forms.Button()
        Me.LabelTopLevelAssembly = New System.Windows.Forms.Label()
        Me.TextBoxTopLevelAssembly = New System.Windows.Forms.TextBox()
        Me.LabelListboxFiles = New System.Windows.Forms.Label()
        Me.TextBoxColumnWidth = New System.Windows.Forms.TextBox()
        Me.LabelColumnWidth = New System.Windows.Forms.Label()
        Me.GroupBoxFilesToProcess = New System.Windows.Forms.GroupBox()
        Me.RadioButtonTopLevelAssembly = New System.Windows.Forms.RadioButton()
        Me.RadioButtonFilesDirectoryOnly = New System.Windows.Forms.RadioButton()
        Me.RadioButtonFilesDirectoriesAndSubdirectories = New System.Windows.Forms.RadioButton()
        Me.ListBoxFiles = New System.Windows.Forms.ListBox()
        Me.LabelInputDirectory = New System.Windows.Forms.Label()
        Me.TextBoxInputDirectory = New System.Windows.Forms.TextBox()
        Me.ButtonInputDirectory = New System.Windows.Forms.Button()
        Me.TabPageAssembly = New System.Windows.Forms.TabPage()
        Me.CheckBoxStepAssemblyOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.ButtonStepAssemblyOutputDirectory = New System.Windows.Forms.Button()
        Me.TextBoxStepAssemblyOutputDirectory = New System.Windows.Forms.TextBox()
        Me.LabelStepAssemblyOutputDirectory = New System.Windows.Forms.Label()
        Me.LabelAssemblyTabNote = New System.Windows.Forms.Label()
        Me.CheckedListBoxAssembly = New System.Windows.Forms.CheckedListBox()
        Me.TabPagePart = New System.Windows.Forms.TabPage()
        Me.CheckBoxStepPartOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.ButtonStepPartOutputDirectory = New System.Windows.Forms.Button()
        Me.TextBoxStepPartOutputDirectory = New System.Windows.Forms.TextBox()
        Me.LabelStepPartOutputDirectory = New System.Windows.Forms.Label()
        Me.LabelPartTabNote = New System.Windows.Forms.Label()
        Me.CheckedListBoxPart = New System.Windows.Forms.CheckedListBox()
        Me.TabPageSheetmetal = New System.Windows.Forms.TabPage()
        Me.CheckBoxStepSheetmetalOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.CheckBoxLaserOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.ButtonStepSheetmetalOutputDirectory = New System.Windows.Forms.Button()
        Me.TextBoxStepSheetmetalOutputDirectory = New System.Windows.Forms.TextBox()
        Me.LabelStepSheetmetalOutputDirectory = New System.Windows.Forms.Label()
        Me.LabelSheetmetalTabNote = New System.Windows.Forms.Label()
        Me.ButtonLaserOutputDirectory = New System.Windows.Forms.Button()
        Me.TextBoxLaserOutputDirectory = New System.Windows.Forms.TextBox()
        Me.LabelLaserOutputDirectory = New System.Windows.Forms.Label()
        Me.CheckedListBoxSheetmetal = New System.Windows.Forms.CheckedListBox()
        Me.TabPageDraft = New System.Windows.Forms.TabPage()
        Me.CheckBoxDxfDraftOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPdfDraftOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.ButtonDxfDraftOutputDirectory = New System.Windows.Forms.Button()
        Me.ButtonPdfDraftOutputDirectory = New System.Windows.Forms.Button()
        Me.TextBoxDxfDraftOutputDirectory = New System.Windows.Forms.TextBox()
        Me.TextBoxPdfDraftOutputDirectory = New System.Windows.Forms.TextBox()
        Me.LabelDxfDraftOutputDirectory = New System.Windows.Forms.Label()
        Me.LabelPdfDraftOutputDirectory = New System.Windows.Forms.Label()
        Me.LabelDraftTabNote = New System.Windows.Forms.Label()
        Me.CheckedListBoxDraft = New System.Windows.Forms.CheckedListBox()
        Me.TabPageConfiguration = New System.Windows.Forms.TabPage()
        Me.CheckBoxRememberTasks = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess = New System.Windows.Forms.CheckBox()
        Me.GroupBoxTLAOptions = New System.Windows.Forms.GroupBox()
        Me.CheckBoxTLAReportUnrelatedFiles = New System.Windows.Forms.CheckBox()
        Me.RadioButtonTLATopDown = New System.Windows.Forms.RadioButton()
        Me.RadioButtonTLABottomUp = New System.Windows.Forms.RadioButton()
        Me.CheckBoxWarnSave = New System.Windows.Forms.CheckBox()
        Me.TextBoxRestartAfter = New System.Windows.Forms.TextBox()
        Me.LabelRestartAfter = New System.Windows.Forms.Label()
        Me.TextBoxPartNumberPropertyName = New System.Windows.Forms.TextBox()
        Me.LabelPartNumberPropertyName = New System.Windows.Forms.Label()
        Me.ComboBoxPartNumberPropertySet = New System.Windows.Forms.ComboBox()
        Me.LabelPartNumberPropertySet = New System.Windows.Forms.Label()
        Me.ButtonActiveMaterialLibrary = New System.Windows.Forms.Button()
        Me.TextBoxActiveMaterialLibrary = New System.Windows.Forms.TextBox()
        Me.LabelActiveMaterialLibrary = New System.Windows.Forms.Label()
        Me.ButtonTemplateAssembly = New System.Windows.Forms.Button()
        Me.TextBoxTemplateAssembly = New System.Windows.Forms.TextBox()
        Me.LabelTemplateAssembly = New System.Windows.Forms.Label()
        Me.ButtonTemplatePart = New System.Windows.Forms.Button()
        Me.TextBoxTemplatePart = New System.Windows.Forms.TextBox()
        Me.LabelTemplatePart = New System.Windows.Forms.Label()
        Me.ButtonTemplateSheetmetal = New System.Windows.Forms.Button()
        Me.TextBoxTemplateSheetmetal = New System.Windows.Forms.TextBox()
        Me.LabelTemplateSheetmetal = New System.Windows.Forms.Label()
        Me.ButtonTemplateDraft = New System.Windows.Forms.Button()
        Me.TextBoxTemplateDraft = New System.Windows.Forms.TextBox()
        Me.LabelTemplateDraft = New System.Windows.Forms.Label()
        Me.TabPageReadme = New System.Windows.Forms.TabPage()
        Me.TextBoxReadme = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.TextBoxStatus = New System.Windows.Forms.TextBox()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonProcess = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.FakeFolderBrowserDialog = New System.Windows.Forms.OpenFileDialog()
        Me.LabelTimeRemaining = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGeneral.SuspendLayout()
        Me.GroupBoxFilesToProcess.SuspendLayout()
        Me.TabPageAssembly.SuspendLayout()
        Me.TabPagePart.SuspendLayout()
        Me.TabPageSheetmetal.SuspendLayout()
        Me.TabPageDraft.SuspendLayout()
        Me.TabPageConfiguration.SuspendLayout()
        Me.GroupBoxTLAOptions.SuspendLayout()
        Me.TabPageReadme.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGeneral)
        Me.TabControl1.Controls.Add(Me.TabPageAssembly)
        Me.TabControl1.Controls.Add(Me.TabPagePart)
        Me.TabControl1.Controls.Add(Me.TabPageSheetmetal)
        Me.TabControl1.Controls.Add(Me.TabPageDraft)
        Me.TabControl1.Controls.Add(Me.TabPageConfiguration)
        Me.TabControl1.Controls.Add(Me.TabPageReadme)
        Me.TabControl1.Location = New System.Drawing.Point(-4, -5)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(651, 750)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageGeneral
        '
        Me.TabPageGeneral.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageGeneral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxEnablePropertyFilter)
        Me.TabPageGeneral.Controls.Add(Me.ButtonUpdateListBoxFiles)
        Me.TabPageGeneral.Controls.Add(Me.ButtonPropertyFilter)
        Me.TabPageGeneral.Controls.Add(Me.ButtonTopLevelAssembly)
        Me.TabPageGeneral.Controls.Add(Me.LabelTopLevelAssembly)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxTopLevelAssembly)
        Me.TabPageGeneral.Controls.Add(Me.LabelListboxFiles)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxColumnWidth)
        Me.TabPageGeneral.Controls.Add(Me.LabelColumnWidth)
        Me.TabPageGeneral.Controls.Add(Me.GroupBoxFilesToProcess)
        Me.TabPageGeneral.Controls.Add(Me.ListBoxFiles)
        Me.TabPageGeneral.Controls.Add(Me.LabelInputDirectory)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxInputDirectory)
        Me.TabPageGeneral.Controls.Add(Me.ButtonInputDirectory)
        Me.TabPageGeneral.Location = New System.Drawing.Point(4, 25)
        Me.TabPageGeneral.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPageGeneral.Name = "TabPageGeneral"
        Me.TabPageGeneral.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPageGeneral.Size = New System.Drawing.Size(643, 721)
        Me.TabPageGeneral.TabIndex = 0
        Me.TabPageGeneral.Text = "General"
        '
        'CheckBoxEnablePropertyFilter
        '
        Me.CheckBoxEnablePropertyFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxEnablePropertyFilter.AutoSize = True
        Me.CheckBoxEnablePropertyFilter.Location = New System.Drawing.Point(15, 690)
        Me.CheckBoxEnablePropertyFilter.Name = "CheckBoxEnablePropertyFilter"
        Me.CheckBoxEnablePropertyFilter.Size = New System.Drawing.Size(162, 21)
        Me.CheckBoxEnablePropertyFilter.TabIndex = 20
        Me.CheckBoxEnablePropertyFilter.Text = "Enable property filter"
        Me.CheckBoxEnablePropertyFilter.UseVisualStyleBackColor = True
        '
        'ButtonUpdateListBoxFiles
        '
        Me.ButtonUpdateListBoxFiles.Location = New System.Drawing.Point(15, 70)
        Me.ButtonUpdateListBoxFiles.Name = "ButtonUpdateListBoxFiles"
        Me.ButtonUpdateListBoxFiles.Size = New System.Drawing.Size(125, 25)
        Me.ButtonUpdateListBoxFiles.TabIndex = 19
        Me.ButtonUpdateListBoxFiles.Text = "Update File List"
        Me.ButtonUpdateListBoxFiles.UseVisualStyleBackColor = True
        '
        'ButtonPropertyFilter
        '
        Me.ButtonPropertyFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonPropertyFilter.Enabled = False
        Me.ButtonPropertyFilter.Location = New System.Drawing.Point(200, 690)
        Me.ButtonPropertyFilter.Name = "ButtonPropertyFilter"
        Me.ButtonPropertyFilter.Size = New System.Drawing.Size(100, 25)
        Me.ButtonPropertyFilter.TabIndex = 18
        Me.ButtonPropertyFilter.Text = "Configure"
        Me.ButtonPropertyFilter.UseVisualStyleBackColor = True
        '
        'ButtonTopLevelAssembly
        '
        Me.ButtonTopLevelAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTopLevelAssembly.Location = New System.Drawing.Point(525, 660)
        Me.ButtonTopLevelAssembly.Name = "ButtonTopLevelAssembly"
        Me.ButtonTopLevelAssembly.Size = New System.Drawing.Size(100, 25)
        Me.ButtonTopLevelAssembly.TabIndex = 15
        Me.ButtonTopLevelAssembly.Text = "Browse"
        Me.ButtonTopLevelAssembly.UseVisualStyleBackColor = True
        '
        'LabelTopLevelAssembly
        '
        Me.LabelTopLevelAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelTopLevelAssembly.AutoSize = True
        Me.LabelTopLevelAssembly.Location = New System.Drawing.Point(15, 635)
        Me.LabelTopLevelAssembly.Name = "LabelTopLevelAssembly"
        Me.LabelTopLevelAssembly.Size = New System.Drawing.Size(129, 17)
        Me.LabelTopLevelAssembly.TabIndex = 14
        Me.LabelTopLevelAssembly.Text = "Top level assembly"
        '
        'TextBoxTopLevelAssembly
        '
        Me.TextBoxTopLevelAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTopLevelAssembly.Location = New System.Drawing.Point(15, 660)
        Me.TextBoxTopLevelAssembly.Name = "TextBoxTopLevelAssembly"
        Me.TextBoxTopLevelAssembly.Size = New System.Drawing.Size(500, 22)
        Me.TextBoxTopLevelAssembly.TabIndex = 13
        '
        'LabelListboxFiles
        '
        Me.LabelListboxFiles.AutoSize = True
        Me.LabelListboxFiles.Location = New System.Drawing.Point(196, 73)
        Me.LabelListboxFiles.Name = "LabelListboxFiles"
        Me.LabelListboxFiles.Size = New System.Drawing.Size(348, 17)
        Me.LabelListboxFiles.TabIndex = 12
        Me.LabelListboxFiles.Text = "Select file(s) to process OR Select none to process all"
        '
        'TextBoxColumnWidth
        '
        Me.TextBoxColumnWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxColumnWidth.Location = New System.Drawing.Point(10, 550)
        Me.TextBoxColumnWidth.Name = "TextBoxColumnWidth"
        Me.TextBoxColumnWidth.Size = New System.Drawing.Size(75, 22)
        Me.TextBoxColumnWidth.TabIndex = 11
        Me.TextBoxColumnWidth.Text = "5.5"
        '
        'LabelColumnWidth
        '
        Me.LabelColumnWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelColumnWidth.AutoSize = True
        Me.LabelColumnWidth.Location = New System.Drawing.Point(96, 555)
        Me.LabelColumnWidth.Name = "LabelColumnWidth"
        Me.LabelColumnWidth.Size = New System.Drawing.Size(204, 17)
        Me.LabelColumnWidth.TabIndex = 9
        Me.LabelColumnWidth.Text = "Column width (pixels/character)"
        '
        'GroupBoxFilesToProcess
        '
        Me.GroupBoxFilesToProcess.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxFilesToProcess.Controls.Add(Me.RadioButtonTopLevelAssembly)
        Me.GroupBoxFilesToProcess.Controls.Add(Me.RadioButtonFilesDirectoryOnly)
        Me.GroupBoxFilesToProcess.Controls.Add(Me.RadioButtonFilesDirectoriesAndSubdirectories)
        Me.GroupBoxFilesToProcess.Location = New System.Drawing.Point(15, 580)
        Me.GroupBoxFilesToProcess.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBoxFilesToProcess.Name = "GroupBoxFilesToProcess"
        Me.GroupBoxFilesToProcess.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBoxFilesToProcess.Size = New System.Drawing.Size(605, 50)
        Me.GroupBoxFilesToProcess.TabIndex = 6
        Me.GroupBoxFilesToProcess.TabStop = False
        Me.GroupBoxFilesToProcess.Text = "Files to process"
        '
        'RadioButtonTopLevelAssembly
        '
        Me.RadioButtonTopLevelAssembly.AutoSize = True
        Me.RadioButtonTopLevelAssembly.Location = New System.Drawing.Point(400, 25)
        Me.RadioButtonTopLevelAssembly.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RadioButtonTopLevelAssembly.Name = "RadioButtonTopLevelAssembly"
        Me.RadioButtonTopLevelAssembly.Size = New System.Drawing.Size(150, 21)
        Me.RadioButtonTopLevelAssembly.TabIndex = 2
        Me.RadioButtonTopLevelAssembly.Text = "Top level assembly"
        Me.RadioButtonTopLevelAssembly.UseVisualStyleBackColor = True
        '
        'RadioButtonFilesDirectoryOnly
        '
        Me.RadioButtonFilesDirectoryOnly.AutoSize = True
        Me.RadioButtonFilesDirectoryOnly.Location = New System.Drawing.Point(250, 25)
        Me.RadioButtonFilesDirectoryOnly.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RadioButtonFilesDirectoryOnly.Name = "RadioButtonFilesDirectoryOnly"
        Me.RadioButtonFilesDirectoryOnly.Size = New System.Drawing.Size(116, 21)
        Me.RadioButtonFilesDirectoryOnly.TabIndex = 1
        Me.RadioButtonFilesDirectoryOnly.Text = "Directory only"
        Me.RadioButtonFilesDirectoryOnly.UseVisualStyleBackColor = True
        '
        'RadioButtonFilesDirectoriesAndSubdirectories
        '
        Me.RadioButtonFilesDirectoriesAndSubdirectories.AutoSize = True
        Me.RadioButtonFilesDirectoriesAndSubdirectories.Location = New System.Drawing.Point(15, 25)
        Me.RadioButtonFilesDirectoriesAndSubdirectories.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RadioButtonFilesDirectoriesAndSubdirectories.Name = "RadioButtonFilesDirectoriesAndSubdirectories"
        Me.RadioButtonFilesDirectoriesAndSubdirectories.Size = New System.Drawing.Size(207, 21)
        Me.RadioButtonFilesDirectoriesAndSubdirectories.TabIndex = 0
        Me.RadioButtonFilesDirectoriesAndSubdirectories.Text = "Directory and subdirectories"
        Me.RadioButtonFilesDirectoriesAndSubdirectories.UseVisualStyleBackColor = True
        '
        'ListBoxFiles
        '
        Me.ListBoxFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxFiles.ColumnWidth = 200
        Me.ListBoxFiles.FormattingEnabled = True
        Me.ListBoxFiles.ItemHeight = 16
        Me.ListBoxFiles.Location = New System.Drawing.Point(15, 100)
        Me.ListBoxFiles.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ListBoxFiles.MultiColumn = True
        Me.ListBoxFiles.Name = "ListBoxFiles"
        Me.ListBoxFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBoxFiles.Size = New System.Drawing.Size(610, 436)
        Me.ListBoxFiles.Sorted = True
        Me.ListBoxFiles.TabIndex = 5
        '
        'LabelInputDirectory
        '
        Me.LabelInputDirectory.AutoSize = True
        Me.LabelInputDirectory.Location = New System.Drawing.Point(15, 10)
        Me.LabelInputDirectory.Name = "LabelInputDirectory"
        Me.LabelInputDirectory.Size = New System.Drawing.Size(100, 17)
        Me.LabelInputDirectory.TabIndex = 4
        Me.LabelInputDirectory.Text = "Input Directory"
        '
        'TextBoxInputDirectory
        '
        Me.TextBoxInputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxInputDirectory.Location = New System.Drawing.Point(15, 34)
        Me.TextBoxInputDirectory.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxInputDirectory.Name = "TextBoxInputDirectory"
        Me.TextBoxInputDirectory.Size = New System.Drawing.Size(500, 22)
        Me.TextBoxInputDirectory.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.TextBoxInputDirectory, "Input Directory")
        '
        'ButtonInputDirectory
        '
        Me.ButtonInputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonInputDirectory.Location = New System.Drawing.Point(525, 34)
        Me.ButtonInputDirectory.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonInputDirectory.Name = "ButtonInputDirectory"
        Me.ButtonInputDirectory.Size = New System.Drawing.Size(100, 25)
        Me.ButtonInputDirectory.TabIndex = 2
        Me.ButtonInputDirectory.Text = "Browse"
        Me.ButtonInputDirectory.UseVisualStyleBackColor = True
        '
        'TabPageAssembly
        '
        Me.TabPageAssembly.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageAssembly.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageAssembly.Controls.Add(Me.CheckBoxStepAssemblyOutputDirectory)
        Me.TabPageAssembly.Controls.Add(Me.ButtonStepAssemblyOutputDirectory)
        Me.TabPageAssembly.Controls.Add(Me.TextBoxStepAssemblyOutputDirectory)
        Me.TabPageAssembly.Controls.Add(Me.LabelStepAssemblyOutputDirectory)
        Me.TabPageAssembly.Controls.Add(Me.LabelAssemblyTabNote)
        Me.TabPageAssembly.Controls.Add(Me.CheckedListBoxAssembly)
        Me.TabPageAssembly.Location = New System.Drawing.Point(4, 25)
        Me.TabPageAssembly.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPageAssembly.Name = "TabPageAssembly"
        Me.TabPageAssembly.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPageAssembly.Size = New System.Drawing.Size(643, 721)
        Me.TabPageAssembly.TabIndex = 1
        Me.TabPageAssembly.Text = "Assembly"
        '
        'CheckBoxStepAssemblyOutputDirectory
        '
        Me.CheckBoxStepAssemblyOutputDirectory.AutoSize = True
        Me.CheckBoxStepAssemblyOutputDirectory.Location = New System.Drawing.Point(225, 350)
        Me.CheckBoxStepAssemblyOutputDirectory.Name = "CheckBoxStepAssemblyOutputDirectory"
        Me.CheckBoxStepAssemblyOutputDirectory.Size = New System.Drawing.Size(194, 21)
        Me.CheckBoxStepAssemblyOutputDirectory.TabIndex = 5
        Me.CheckBoxStepAssemblyOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxStepAssemblyOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonStepAssemblyOutputDirectory
        '
        Me.ButtonStepAssemblyOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonStepAssemblyOutputDirectory.Location = New System.Drawing.Point(509, 370)
        Me.ButtonStepAssemblyOutputDirectory.Name = "ButtonStepAssemblyOutputDirectory"
        Me.ButtonStepAssemblyOutputDirectory.Size = New System.Drawing.Size(100, 25)
        Me.ButtonStepAssemblyOutputDirectory.TabIndex = 4
        Me.ButtonStepAssemblyOutputDirectory.Text = "Browse"
        Me.ButtonStepAssemblyOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxStepAssemblyOutputDirectory
        '
        Me.TextBoxStepAssemblyOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStepAssemblyOutputDirectory.Location = New System.Drawing.Point(10, 375)
        Me.TextBoxStepAssemblyOutputDirectory.Name = "TextBoxStepAssemblyOutputDirectory"
        Me.TextBoxStepAssemblyOutputDirectory.Size = New System.Drawing.Size(491, 22)
        Me.TextBoxStepAssemblyOutputDirectory.TabIndex = 3
        '
        'LabelStepAssemblyOutputDirectory
        '
        Me.LabelStepAssemblyOutputDirectory.AutoSize = True
        Me.LabelStepAssemblyOutputDirectory.Location = New System.Drawing.Point(10, 350)
        Me.LabelStepAssemblyOutputDirectory.Name = "LabelStepAssemblyOutputDirectory"
        Me.LabelStepAssemblyOutputDirectory.Size = New System.Drawing.Size(169, 17)
        Me.LabelStepAssemblyOutputDirectory.TabIndex = 2
        Me.LabelStepAssemblyOutputDirectory.Text = "Step files output directory"
        '
        'LabelAssemblyTabNote
        '
        Me.LabelAssemblyTabNote.AutoSize = True
        Me.LabelAssemblyTabNote.Location = New System.Drawing.Point(25, 10)
        Me.LabelAssemblyTabNote.Name = "LabelAssemblyTabNote"
        Me.LabelAssemblyTabNote.Size = New System.Drawing.Size(412, 17)
        Me.LabelAssemblyTabNote.TabIndex = 1
        Me.LabelAssemblyTabNote.Text = "Double-click anywhere in the checkbox control to toggle all/none"
        '
        'CheckedListBoxAssembly
        '
        Me.CheckedListBoxAssembly.BackColor = System.Drawing.SystemColors.Control
        Me.CheckedListBoxAssembly.CheckOnClick = True
        Me.CheckedListBoxAssembly.FormattingEnabled = True
        Me.CheckedListBoxAssembly.Items.AddRange(New Object() {"Fake name 1.  Real checkboxes populated at run time.", "Fake name 2", "Fake name 3"})
        Me.CheckedListBoxAssembly.Location = New System.Drawing.Point(25, 35)
        Me.CheckedListBoxAssembly.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CheckedListBoxAssembly.Name = "CheckedListBoxAssembly"
        Me.CheckedListBoxAssembly.Size = New System.Drawing.Size(600, 293)
        Me.CheckedListBoxAssembly.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxAssembly, "Double-click to check/uncheck all")
        '
        'TabPagePart
        '
        Me.TabPagePart.BackColor = System.Drawing.SystemColors.Control
        Me.TabPagePart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPagePart.Controls.Add(Me.CheckBoxStepPartOutputDirectory)
        Me.TabPagePart.Controls.Add(Me.ButtonStepPartOutputDirectory)
        Me.TabPagePart.Controls.Add(Me.TextBoxStepPartOutputDirectory)
        Me.TabPagePart.Controls.Add(Me.LabelStepPartOutputDirectory)
        Me.TabPagePart.Controls.Add(Me.LabelPartTabNote)
        Me.TabPagePart.Controls.Add(Me.CheckedListBoxPart)
        Me.TabPagePart.Location = New System.Drawing.Point(4, 25)
        Me.TabPagePart.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPagePart.Name = "TabPagePart"
        Me.TabPagePart.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPagePart.Size = New System.Drawing.Size(643, 721)
        Me.TabPagePart.TabIndex = 2
        Me.TabPagePart.Text = "Part"
        '
        'CheckBoxStepPartOutputDirectory
        '
        Me.CheckBoxStepPartOutputDirectory.AutoSize = True
        Me.CheckBoxStepPartOutputDirectory.Location = New System.Drawing.Point(225, 350)
        Me.CheckBoxStepPartOutputDirectory.Name = "CheckBoxStepPartOutputDirectory"
        Me.CheckBoxStepPartOutputDirectory.Size = New System.Drawing.Size(194, 21)
        Me.CheckBoxStepPartOutputDirectory.TabIndex = 6
        Me.CheckBoxStepPartOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxStepPartOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonStepPartOutputDirectory
        '
        Me.ButtonStepPartOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonStepPartOutputDirectory.Location = New System.Drawing.Point(509, 370)
        Me.ButtonStepPartOutputDirectory.Name = "ButtonStepPartOutputDirectory"
        Me.ButtonStepPartOutputDirectory.Size = New System.Drawing.Size(100, 25)
        Me.ButtonStepPartOutputDirectory.TabIndex = 5
        Me.ButtonStepPartOutputDirectory.Text = "Browse"
        Me.ButtonStepPartOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxStepPartOutputDirectory
        '
        Me.TextBoxStepPartOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStepPartOutputDirectory.Location = New System.Drawing.Point(10, 375)
        Me.TextBoxStepPartOutputDirectory.Name = "TextBoxStepPartOutputDirectory"
        Me.TextBoxStepPartOutputDirectory.Size = New System.Drawing.Size(491, 22)
        Me.TextBoxStepPartOutputDirectory.TabIndex = 4
        '
        'LabelStepPartOutputDirectory
        '
        Me.LabelStepPartOutputDirectory.AutoSize = True
        Me.LabelStepPartOutputDirectory.Location = New System.Drawing.Point(10, 350)
        Me.LabelStepPartOutputDirectory.Name = "LabelStepPartOutputDirectory"
        Me.LabelStepPartOutputDirectory.Size = New System.Drawing.Size(169, 17)
        Me.LabelStepPartOutputDirectory.TabIndex = 3
        Me.LabelStepPartOutputDirectory.Text = "Step files output directory"
        '
        'LabelPartTabNote
        '
        Me.LabelPartTabNote.AutoSize = True
        Me.LabelPartTabNote.Location = New System.Drawing.Point(25, 10)
        Me.LabelPartTabNote.Name = "LabelPartTabNote"
        Me.LabelPartTabNote.Size = New System.Drawing.Size(412, 17)
        Me.LabelPartTabNote.TabIndex = 2
        Me.LabelPartTabNote.Text = "Double-click anywhere in the checkbox control to toggle all/none"
        '
        'CheckedListBoxPart
        '
        Me.CheckedListBoxPart.BackColor = System.Drawing.SystemColors.Control
        Me.CheckedListBoxPart.CheckOnClick = True
        Me.CheckedListBoxPart.FormattingEnabled = True
        Me.CheckedListBoxPart.Items.AddRange(New Object() {"Fake name 1", "Fake name 2"})
        Me.CheckedListBoxPart.Location = New System.Drawing.Point(25, 35)
        Me.CheckedListBoxPart.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CheckedListBoxPart.Name = "CheckedListBoxPart"
        Me.CheckedListBoxPart.Size = New System.Drawing.Size(600, 293)
        Me.CheckedListBoxPart.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxPart, "Double-click to check/uncheck all")
        '
        'TabPageSheetmetal
        '
        Me.TabPageSheetmetal.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageSheetmetal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageSheetmetal.Controls.Add(Me.CheckBoxStepSheetmetalOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.CheckBoxLaserOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.ButtonStepSheetmetalOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxStepSheetmetalOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelStepSheetmetalOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelSheetmetalTabNote)
        Me.TabPageSheetmetal.Controls.Add(Me.ButtonLaserOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxLaserOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelLaserOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.CheckedListBoxSheetmetal)
        Me.TabPageSheetmetal.Location = New System.Drawing.Point(4, 25)
        Me.TabPageSheetmetal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPageSheetmetal.Name = "TabPageSheetmetal"
        Me.TabPageSheetmetal.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPageSheetmetal.Size = New System.Drawing.Size(643, 721)
        Me.TabPageSheetmetal.TabIndex = 3
        Me.TabPageSheetmetal.Text = "Sheetmetal"
        '
        'CheckBoxStepSheetmetalOutputDirectory
        '
        Me.CheckBoxStepSheetmetalOutputDirectory.AutoSize = True
        Me.CheckBoxStepSheetmetalOutputDirectory.Location = New System.Drawing.Point(225, 410)
        Me.CheckBoxStepSheetmetalOutputDirectory.Name = "CheckBoxStepSheetmetalOutputDirectory"
        Me.CheckBoxStepSheetmetalOutputDirectory.Size = New System.Drawing.Size(194, 21)
        Me.CheckBoxStepSheetmetalOutputDirectory.TabIndex = 11
        Me.CheckBoxStepSheetmetalOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxStepSheetmetalOutputDirectory.UseVisualStyleBackColor = True
        '
        'CheckBoxLaserOutputDirectory
        '
        Me.CheckBoxLaserOutputDirectory.AutoSize = True
        Me.CheckBoxLaserOutputDirectory.Location = New System.Drawing.Point(225, 350)
        Me.CheckBoxLaserOutputDirectory.Name = "CheckBoxLaserOutputDirectory"
        Me.CheckBoxLaserOutputDirectory.Size = New System.Drawing.Size(194, 21)
        Me.CheckBoxLaserOutputDirectory.TabIndex = 10
        Me.CheckBoxLaserOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxLaserOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonStepSheetmetalOutputDirectory
        '
        Me.ButtonStepSheetmetalOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonStepSheetmetalOutputDirectory.Location = New System.Drawing.Point(509, 430)
        Me.ButtonStepSheetmetalOutputDirectory.Name = "ButtonStepSheetmetalOutputDirectory"
        Me.ButtonStepSheetmetalOutputDirectory.Size = New System.Drawing.Size(100, 25)
        Me.ButtonStepSheetmetalOutputDirectory.TabIndex = 9
        Me.ButtonStepSheetmetalOutputDirectory.Text = "Browse"
        Me.ButtonStepSheetmetalOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxStepSheetmetalOutputDirectory
        '
        Me.TextBoxStepSheetmetalOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStepSheetmetalOutputDirectory.Location = New System.Drawing.Point(10, 435)
        Me.TextBoxStepSheetmetalOutputDirectory.Name = "TextBoxStepSheetmetalOutputDirectory"
        Me.TextBoxStepSheetmetalOutputDirectory.Size = New System.Drawing.Size(491, 22)
        Me.TextBoxStepSheetmetalOutputDirectory.TabIndex = 8
        '
        'LabelStepSheetmetalOutputDirectory
        '
        Me.LabelStepSheetmetalOutputDirectory.AutoSize = True
        Me.LabelStepSheetmetalOutputDirectory.Location = New System.Drawing.Point(10, 410)
        Me.LabelStepSheetmetalOutputDirectory.Name = "LabelStepSheetmetalOutputDirectory"
        Me.LabelStepSheetmetalOutputDirectory.Size = New System.Drawing.Size(169, 17)
        Me.LabelStepSheetmetalOutputDirectory.TabIndex = 7
        Me.LabelStepSheetmetalOutputDirectory.Text = "Step files output directory"
        '
        'LabelSheetmetalTabNote
        '
        Me.LabelSheetmetalTabNote.AutoSize = True
        Me.LabelSheetmetalTabNote.Location = New System.Drawing.Point(25, 10)
        Me.LabelSheetmetalTabNote.Name = "LabelSheetmetalTabNote"
        Me.LabelSheetmetalTabNote.Size = New System.Drawing.Size(412, 17)
        Me.LabelSheetmetalTabNote.TabIndex = 6
        Me.LabelSheetmetalTabNote.Text = "Double-click anywhere in the checkbox control to toggle all/none"
        '
        'ButtonLaserOutputDirectory
        '
        Me.ButtonLaserOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonLaserOutputDirectory.Location = New System.Drawing.Point(509, 370)
        Me.ButtonLaserOutputDirectory.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonLaserOutputDirectory.Name = "ButtonLaserOutputDirectory"
        Me.ButtonLaserOutputDirectory.Size = New System.Drawing.Size(100, 25)
        Me.ButtonLaserOutputDirectory.TabIndex = 5
        Me.ButtonLaserOutputDirectory.Text = "Browse"
        Me.ButtonLaserOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxLaserOutputDirectory
        '
        Me.TextBoxLaserOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxLaserOutputDirectory.Location = New System.Drawing.Point(10, 375)
        Me.TextBoxLaserOutputDirectory.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxLaserOutputDirectory.Name = "TextBoxLaserOutputDirectory"
        Me.TextBoxLaserOutputDirectory.Size = New System.Drawing.Size(491, 22)
        Me.TextBoxLaserOutputDirectory.TabIndex = 4
        '
        'LabelLaserOutputDirectory
        '
        Me.LabelLaserOutputDirectory.AutoSize = True
        Me.LabelLaserOutputDirectory.Location = New System.Drawing.Point(10, 350)
        Me.LabelLaserOutputDirectory.Name = "LabelLaserOutputDirectory"
        Me.LabelLaserOutputDirectory.Size = New System.Drawing.Size(176, 17)
        Me.LabelLaserOutputDirectory.TabIndex = 3
        Me.LabelLaserOutputDirectory.Text = "Laser files output directory"
        '
        'CheckedListBoxSheetmetal
        '
        Me.CheckedListBoxSheetmetal.BackColor = System.Drawing.SystemColors.Control
        Me.CheckedListBoxSheetmetal.CheckOnClick = True
        Me.CheckedListBoxSheetmetal.FormattingEnabled = True
        Me.CheckedListBoxSheetmetal.Items.AddRange(New Object() {"Fake name 1", "Fake name 2"})
        Me.CheckedListBoxSheetmetal.Location = New System.Drawing.Point(25, 35)
        Me.CheckedListBoxSheetmetal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CheckedListBoxSheetmetal.Name = "CheckedListBoxSheetmetal"
        Me.CheckedListBoxSheetmetal.Size = New System.Drawing.Size(600, 293)
        Me.CheckedListBoxSheetmetal.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxSheetmetal, "Double-click to check/uncheck all")
        '
        'TabPageDraft
        '
        Me.TabPageDraft.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageDraft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageDraft.Controls.Add(Me.CheckBoxDxfDraftOutputDirectory)
        Me.TabPageDraft.Controls.Add(Me.CheckBoxPdfDraftOutputDirectory)
        Me.TabPageDraft.Controls.Add(Me.ButtonDxfDraftOutputDirectory)
        Me.TabPageDraft.Controls.Add(Me.ButtonPdfDraftOutputDirectory)
        Me.TabPageDraft.Controls.Add(Me.TextBoxDxfDraftOutputDirectory)
        Me.TabPageDraft.Controls.Add(Me.TextBoxPdfDraftOutputDirectory)
        Me.TabPageDraft.Controls.Add(Me.LabelDxfDraftOutputDirectory)
        Me.TabPageDraft.Controls.Add(Me.LabelPdfDraftOutputDirectory)
        Me.TabPageDraft.Controls.Add(Me.LabelDraftTabNote)
        Me.TabPageDraft.Controls.Add(Me.CheckedListBoxDraft)
        Me.TabPageDraft.Location = New System.Drawing.Point(4, 25)
        Me.TabPageDraft.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPageDraft.Name = "TabPageDraft"
        Me.TabPageDraft.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPageDraft.Size = New System.Drawing.Size(643, 721)
        Me.TabPageDraft.TabIndex = 4
        Me.TabPageDraft.Text = "Draft"
        '
        'CheckBoxDxfDraftOutputDirectory
        '
        Me.CheckBoxDxfDraftOutputDirectory.AutoSize = True
        Me.CheckBoxDxfDraftOutputDirectory.Location = New System.Drawing.Point(225, 410)
        Me.CheckBoxDxfDraftOutputDirectory.Name = "CheckBoxDxfDraftOutputDirectory"
        Me.CheckBoxDxfDraftOutputDirectory.Size = New System.Drawing.Size(194, 21)
        Me.CheckBoxDxfDraftOutputDirectory.TabIndex = 11
        Me.CheckBoxDxfDraftOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxDxfDraftOutputDirectory.UseVisualStyleBackColor = True
        '
        'CheckBoxPdfDraftOutputDirectory
        '
        Me.CheckBoxPdfDraftOutputDirectory.AutoSize = True
        Me.CheckBoxPdfDraftOutputDirectory.Location = New System.Drawing.Point(225, 350)
        Me.CheckBoxPdfDraftOutputDirectory.Name = "CheckBoxPdfDraftOutputDirectory"
        Me.CheckBoxPdfDraftOutputDirectory.Size = New System.Drawing.Size(194, 21)
        Me.CheckBoxPdfDraftOutputDirectory.TabIndex = 10
        Me.CheckBoxPdfDraftOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxPdfDraftOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonDxfDraftOutputDirectory
        '
        Me.ButtonDxfDraftOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDxfDraftOutputDirectory.Location = New System.Drawing.Point(509, 430)
        Me.ButtonDxfDraftOutputDirectory.Name = "ButtonDxfDraftOutputDirectory"
        Me.ButtonDxfDraftOutputDirectory.Size = New System.Drawing.Size(100, 25)
        Me.ButtonDxfDraftOutputDirectory.TabIndex = 9
        Me.ButtonDxfDraftOutputDirectory.Text = "Browse"
        Me.ButtonDxfDraftOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonPdfDraftOutputDirectory
        '
        Me.ButtonPdfDraftOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonPdfDraftOutputDirectory.Location = New System.Drawing.Point(509, 370)
        Me.ButtonPdfDraftOutputDirectory.Name = "ButtonPdfDraftOutputDirectory"
        Me.ButtonPdfDraftOutputDirectory.Size = New System.Drawing.Size(100, 25)
        Me.ButtonPdfDraftOutputDirectory.TabIndex = 8
        Me.ButtonPdfDraftOutputDirectory.Text = "Browse"
        Me.ButtonPdfDraftOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxDxfDraftOutputDirectory
        '
        Me.TextBoxDxfDraftOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDxfDraftOutputDirectory.Location = New System.Drawing.Point(10, 435)
        Me.TextBoxDxfDraftOutputDirectory.Name = "TextBoxDxfDraftOutputDirectory"
        Me.TextBoxDxfDraftOutputDirectory.Size = New System.Drawing.Size(491, 22)
        Me.TextBoxDxfDraftOutputDirectory.TabIndex = 7
        '
        'TextBoxPdfDraftOutputDirectory
        '
        Me.TextBoxPdfDraftOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPdfDraftOutputDirectory.Location = New System.Drawing.Point(10, 375)
        Me.TextBoxPdfDraftOutputDirectory.Name = "TextBoxPdfDraftOutputDirectory"
        Me.TextBoxPdfDraftOutputDirectory.Size = New System.Drawing.Size(491, 22)
        Me.TextBoxPdfDraftOutputDirectory.TabIndex = 6
        '
        'LabelDxfDraftOutputDirectory
        '
        Me.LabelDxfDraftOutputDirectory.AutoSize = True
        Me.LabelDxfDraftOutputDirectory.Location = New System.Drawing.Point(10, 410)
        Me.LabelDxfDraftOutputDirectory.Name = "LabelDxfDraftOutputDirectory"
        Me.LabelDxfDraftOutputDirectory.Size = New System.Drawing.Size(167, 17)
        Me.LabelDxfDraftOutputDirectory.TabIndex = 5
        Me.LabelDxfDraftOutputDirectory.Text = "DXF files output directory"
        '
        'LabelPdfDraftOutputDirectory
        '
        Me.LabelPdfDraftOutputDirectory.AutoSize = True
        Me.LabelPdfDraftOutputDirectory.Location = New System.Drawing.Point(10, 350)
        Me.LabelPdfDraftOutputDirectory.Name = "LabelPdfDraftOutputDirectory"
        Me.LabelPdfDraftOutputDirectory.Size = New System.Drawing.Size(167, 17)
        Me.LabelPdfDraftOutputDirectory.TabIndex = 4
        Me.LabelPdfDraftOutputDirectory.Text = "PDF files output directory"
        '
        'LabelDraftTabNote
        '
        Me.LabelDraftTabNote.AutoSize = True
        Me.LabelDraftTabNote.Location = New System.Drawing.Point(25, 10)
        Me.LabelDraftTabNote.Name = "LabelDraftTabNote"
        Me.LabelDraftTabNote.Size = New System.Drawing.Size(412, 17)
        Me.LabelDraftTabNote.TabIndex = 3
        Me.LabelDraftTabNote.Text = "Double-click anywhere in the checkbox control to toggle all/none"
        '
        'CheckedListBoxDraft
        '
        Me.CheckedListBoxDraft.BackColor = System.Drawing.SystemColors.Control
        Me.CheckedListBoxDraft.CheckOnClick = True
        Me.CheckedListBoxDraft.FormattingEnabled = True
        Me.CheckedListBoxDraft.Items.AddRange(New Object() {"Fake name 1", "Fake name 2"})
        Me.CheckedListBoxDraft.Location = New System.Drawing.Point(25, 35)
        Me.CheckedListBoxDraft.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CheckedListBoxDraft.Name = "CheckedListBoxDraft"
        Me.CheckedListBoxDraft.Size = New System.Drawing.Size(600, 293)
        Me.CheckedListBoxDraft.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxDraft, "Double-click to check/uncheck all")
        '
        'TabPageConfiguration
        '
        Me.TabPageConfiguration.AutoScroll = True
        Me.TabPageConfiguration.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageConfiguration.Controls.Add(Me.CheckBoxRememberTasks)
        Me.TabPageConfiguration.Controls.Add(Me.CheckBoxMoveDrawingViewAllowPartialSuccess)
        Me.TabPageConfiguration.Controls.Add(Me.GroupBoxTLAOptions)
        Me.TabPageConfiguration.Controls.Add(Me.CheckBoxWarnSave)
        Me.TabPageConfiguration.Controls.Add(Me.TextBoxRestartAfter)
        Me.TabPageConfiguration.Controls.Add(Me.LabelRestartAfter)
        Me.TabPageConfiguration.Controls.Add(Me.TextBoxPartNumberPropertyName)
        Me.TabPageConfiguration.Controls.Add(Me.LabelPartNumberPropertyName)
        Me.TabPageConfiguration.Controls.Add(Me.ComboBoxPartNumberPropertySet)
        Me.TabPageConfiguration.Controls.Add(Me.LabelPartNumberPropertySet)
        Me.TabPageConfiguration.Controls.Add(Me.ButtonActiveMaterialLibrary)
        Me.TabPageConfiguration.Controls.Add(Me.TextBoxActiveMaterialLibrary)
        Me.TabPageConfiguration.Controls.Add(Me.LabelActiveMaterialLibrary)
        Me.TabPageConfiguration.Controls.Add(Me.ButtonTemplateAssembly)
        Me.TabPageConfiguration.Controls.Add(Me.TextBoxTemplateAssembly)
        Me.TabPageConfiguration.Controls.Add(Me.LabelTemplateAssembly)
        Me.TabPageConfiguration.Controls.Add(Me.ButtonTemplatePart)
        Me.TabPageConfiguration.Controls.Add(Me.TextBoxTemplatePart)
        Me.TabPageConfiguration.Controls.Add(Me.LabelTemplatePart)
        Me.TabPageConfiguration.Controls.Add(Me.ButtonTemplateSheetmetal)
        Me.TabPageConfiguration.Controls.Add(Me.TextBoxTemplateSheetmetal)
        Me.TabPageConfiguration.Controls.Add(Me.LabelTemplateSheetmetal)
        Me.TabPageConfiguration.Controls.Add(Me.ButtonTemplateDraft)
        Me.TabPageConfiguration.Controls.Add(Me.TextBoxTemplateDraft)
        Me.TabPageConfiguration.Controls.Add(Me.LabelTemplateDraft)
        Me.TabPageConfiguration.Location = New System.Drawing.Point(4, 25)
        Me.TabPageConfiguration.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPageConfiguration.Name = "TabPageConfiguration"
        Me.TabPageConfiguration.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPageConfiguration.Size = New System.Drawing.Size(643, 721)
        Me.TabPageConfiguration.TabIndex = 5
        Me.TabPageConfiguration.Text = "Configuration"
        '
        'CheckBoxRememberTasks
        '
        Me.CheckBoxRememberTasks.AutoSize = True
        Me.CheckBoxRememberTasks.Checked = True
        Me.CheckBoxRememberTasks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRememberTasks.Location = New System.Drawing.Point(11, 625)
        Me.CheckBoxRememberTasks.Name = "CheckBoxRememberTasks"
        Me.CheckBoxRememberTasks.Size = New System.Drawing.Size(309, 21)
        Me.CheckBoxRememberTasks.TabIndex = 19
        Me.CheckBoxRememberTasks.Text = "Remember selected tasks between sessions"
        Me.CheckBoxRememberTasks.UseVisualStyleBackColor = True
        '
        'CheckBoxMoveDrawingViewAllowPartialSuccess
        '
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.AutoSize = True
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Location = New System.Drawing.Point(11, 595)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Name = "CheckBoxMoveDrawingViewAllowPartialSuccess"
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Size = New System.Drawing.Size(368, 21)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.TabIndex = 18
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Text = "Move drawing to new template -- Allow partial success"
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.UseVisualStyleBackColor = True
        '
        'GroupBoxTLAOptions
        '
        Me.GroupBoxTLAOptions.Controls.Add(Me.CheckBoxTLAReportUnrelatedFiles)
        Me.GroupBoxTLAOptions.Controls.Add(Me.RadioButtonTLATopDown)
        Me.GroupBoxTLAOptions.Controls.Add(Me.RadioButtonTLABottomUp)
        Me.GroupBoxTLAOptions.Location = New System.Drawing.Point(11, 435)
        Me.GroupBoxTLAOptions.Name = "GroupBoxTLAOptions"
        Me.GroupBoxTLAOptions.Size = New System.Drawing.Size(492, 112)
        Me.GroupBoxTLAOptions.TabIndex = 17
        Me.GroupBoxTLAOptions.TabStop = False
        Me.GroupBoxTLAOptions.Text = "Top level assembly processing options -- See Readme tab for details"
        '
        'CheckBoxTLAReportUnrelatedFiles
        '
        Me.CheckBoxTLAReportUnrelatedFiles.AutoSize = True
        Me.CheckBoxTLAReportUnrelatedFiles.Location = New System.Drawing.Point(15, 75)
        Me.CheckBoxTLAReportUnrelatedFiles.Name = "CheckBoxTLAReportUnrelatedFiles"
        Me.CheckBoxTLAReportUnrelatedFiles.Size = New System.Drawing.Size(302, 21)
        Me.CheckBoxTLAReportUnrelatedFiles.TabIndex = 2
        Me.CheckBoxTLAReportUnrelatedFiles.Text = "Report files unrelated to top level assembly"
        Me.CheckBoxTLAReportUnrelatedFiles.UseVisualStyleBackColor = True
        '
        'RadioButtonTLATopDown
        '
        Me.RadioButtonTLATopDown.AutoSize = True
        Me.RadioButtonTLATopDown.Location = New System.Drawing.Point(15, 50)
        Me.RadioButtonTLATopDown.Name = "RadioButtonTLATopDown"
        Me.RadioButtonTLATopDown.Size = New System.Drawing.Size(368, 21)
        Me.RadioButtonTLATopDown.TabIndex = 1
        Me.RadioButtonTLATopDown.TabStop = True
        Me.RadioButtonTLATopDown.Text = "Top down -- Best for self-contained project directories"
        Me.RadioButtonTLATopDown.UseVisualStyleBackColor = True
        '
        'RadioButtonTLABottomUp
        '
        Me.RadioButtonTLABottomUp.AutoSize = True
        Me.RadioButtonTLABottomUp.Location = New System.Drawing.Point(15, 25)
        Me.RadioButtonTLABottomUp.Name = "RadioButtonTLABottomUp"
        Me.RadioButtonTLABottomUp.Size = New System.Drawing.Size(340, 21)
        Me.RadioButtonTLABottomUp.TabIndex = 0
        Me.RadioButtonTLABottomUp.TabStop = True
        Me.RadioButtonTLABottomUp.Text = "Bottom Up -- Best for general purpose directories"
        Me.RadioButtonTLABottomUp.UseVisualStyleBackColor = True
        '
        'CheckBoxWarnSave
        '
        Me.CheckBoxWarnSave.AutoSize = True
        Me.CheckBoxWarnSave.Checked = True
        Me.CheckBoxWarnSave.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxWarnSave.Location = New System.Drawing.Point(11, 565)
        Me.CheckBoxWarnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBoxWarnSave.Name = "CheckBoxWarnSave"
        Me.CheckBoxWarnSave.Size = New System.Drawing.Size(225, 21)
        Me.CheckBoxWarnSave.TabIndex = 16
        Me.CheckBoxWarnSave.Text = "Warn me if file save is required"
        Me.CheckBoxWarnSave.UseVisualStyleBackColor = True
        '
        'TextBoxRestartAfter
        '
        Me.TextBoxRestartAfter.Location = New System.Drawing.Point(11, 390)
        Me.TextBoxRestartAfter.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxRestartAfter.Name = "TextBoxRestartAfter"
        Me.TextBoxRestartAfter.Size = New System.Drawing.Size(100, 22)
        Me.TextBoxRestartAfter.TabIndex = 15
        Me.TextBoxRestartAfter.Text = "50"
        '
        'LabelRestartAfter
        '
        Me.LabelRestartAfter.AutoSize = True
        Me.LabelRestartAfter.Location = New System.Drawing.Point(11, 370)
        Me.LabelRestartAfter.Name = "LabelRestartAfter"
        Me.LabelRestartAfter.Size = New System.Drawing.Size(261, 17)
        Me.LabelRestartAfter.TabIndex = 14
        Me.LabelRestartAfter.Text = "Restart After This Many Files Processed"
        '
        'TextBoxPartNumberPropertyName
        '
        Me.TextBoxPartNumberPropertyName.Location = New System.Drawing.Point(229, 330)
        Me.TextBoxPartNumberPropertyName.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxPartNumberPropertyName.Name = "TextBoxPartNumberPropertyName"
        Me.TextBoxPartNumberPropertyName.Size = New System.Drawing.Size(175, 22)
        Me.TextBoxPartNumberPropertyName.TabIndex = 13
        '
        'LabelPartNumberPropertyName
        '
        Me.LabelPartNumberPropertyName.AutoSize = True
        Me.LabelPartNumberPropertyName.Location = New System.Drawing.Point(229, 310)
        Me.LabelPartNumberPropertyName.Name = "LabelPartNumberPropertyName"
        Me.LabelPartNumberPropertyName.Size = New System.Drawing.Size(187, 17)
        Me.LabelPartNumberPropertyName.TabIndex = 12
        Me.LabelPartNumberPropertyName.Text = "Part Number Property Name"
        '
        'ComboBoxPartNumberPropertySet
        '
        Me.ComboBoxPartNumberPropertySet.FormattingEnabled = True
        Me.ComboBoxPartNumberPropertySet.Items.AddRange(New Object() {"System", "Custom"})
        Me.ComboBoxPartNumberPropertySet.Location = New System.Drawing.Point(11, 330)
        Me.ComboBoxPartNumberPropertySet.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ComboBoxPartNumberPropertySet.Name = "ComboBoxPartNumberPropertySet"
        Me.ComboBoxPartNumberPropertySet.Size = New System.Drawing.Size(175, 24)
        Me.ComboBoxPartNumberPropertySet.TabIndex = 11
        '
        'LabelPartNumberPropertySet
        '
        Me.LabelPartNumberPropertySet.AutoSize = True
        Me.LabelPartNumberPropertySet.Location = New System.Drawing.Point(11, 310)
        Me.LabelPartNumberPropertySet.Name = "LabelPartNumberPropertySet"
        Me.LabelPartNumberPropertySet.Size = New System.Drawing.Size(171, 17)
        Me.LabelPartNumberPropertySet.TabIndex = 10
        Me.LabelPartNumberPropertySet.Text = "Part Number Property Set"
        '
        'ButtonActiveMaterialLibrary
        '
        Me.ButtonActiveMaterialLibrary.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonActiveMaterialLibrary.Location = New System.Drawing.Point(510, 270)
        Me.ButtonActiveMaterialLibrary.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonActiveMaterialLibrary.Name = "ButtonActiveMaterialLibrary"
        Me.ButtonActiveMaterialLibrary.Size = New System.Drawing.Size(100, 25)
        Me.ButtonActiveMaterialLibrary.TabIndex = 9
        Me.ButtonActiveMaterialLibrary.Text = "Browse"
        Me.ButtonActiveMaterialLibrary.UseVisualStyleBackColor = True
        '
        'TextBoxActiveMaterialLibrary
        '
        Me.TextBoxActiveMaterialLibrary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxActiveMaterialLibrary.Location = New System.Drawing.Point(11, 270)
        Me.TextBoxActiveMaterialLibrary.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxActiveMaterialLibrary.Name = "TextBoxActiveMaterialLibrary"
        Me.TextBoxActiveMaterialLibrary.Size = New System.Drawing.Size(480, 22)
        Me.TextBoxActiveMaterialLibrary.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.TextBoxActiveMaterialLibrary, "Material Library")
        '
        'LabelActiveMaterialLibrary
        '
        Me.LabelActiveMaterialLibrary.AutoSize = True
        Me.LabelActiveMaterialLibrary.Location = New System.Drawing.Point(11, 250)
        Me.LabelActiveMaterialLibrary.Name = "LabelActiveMaterialLibrary"
        Me.LabelActiveMaterialLibrary.Size = New System.Drawing.Size(106, 17)
        Me.LabelActiveMaterialLibrary.TabIndex = 7
        Me.LabelActiveMaterialLibrary.Text = "Material Library"
        '
        'ButtonTemplateAssembly
        '
        Me.ButtonTemplateAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTemplateAssembly.Location = New System.Drawing.Point(510, 30)
        Me.ButtonTemplateAssembly.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonTemplateAssembly.Name = "ButtonTemplateAssembly"
        Me.ButtonTemplateAssembly.Size = New System.Drawing.Size(100, 25)
        Me.ButtonTemplateAssembly.TabIndex = 3
        Me.ButtonTemplateAssembly.Text = "Browse"
        Me.ButtonTemplateAssembly.UseVisualStyleBackColor = True
        '
        'TextBoxTemplateAssembly
        '
        Me.TextBoxTemplateAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplateAssembly.Location = New System.Drawing.Point(11, 30)
        Me.TextBoxTemplateAssembly.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxTemplateAssembly.Name = "TextBoxTemplateAssembly"
        Me.TextBoxTemplateAssembly.Size = New System.Drawing.Size(480, 22)
        Me.TextBoxTemplateAssembly.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TextBoxTemplateAssembly, "Assembly Template")
        '
        'LabelTemplateAssembly
        '
        Me.LabelTemplateAssembly.AutoSize = True
        Me.LabelTemplateAssembly.Location = New System.Drawing.Point(11, 10)
        Me.LabelTemplateAssembly.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTemplateAssembly.Name = "LabelTemplateAssembly"
        Me.LabelTemplateAssembly.Size = New System.Drawing.Size(157, 17)
        Me.LabelTemplateAssembly.TabIndex = 1
        Me.LabelTemplateAssembly.Text = "Assembly Template File"
        '
        'ButtonTemplatePart
        '
        Me.ButtonTemplatePart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTemplatePart.Location = New System.Drawing.Point(510, 90)
        Me.ButtonTemplatePart.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonTemplatePart.Name = "ButtonTemplatePart"
        Me.ButtonTemplatePart.Size = New System.Drawing.Size(100, 25)
        Me.ButtonTemplatePart.TabIndex = 6
        Me.ButtonTemplatePart.Text = "Browse"
        Me.ButtonTemplatePart.UseVisualStyleBackColor = True
        '
        'TextBoxTemplatePart
        '
        Me.TextBoxTemplatePart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplatePart.Location = New System.Drawing.Point(11, 90)
        Me.TextBoxTemplatePart.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxTemplatePart.Name = "TextBoxTemplatePart"
        Me.TextBoxTemplatePart.Size = New System.Drawing.Size(480, 22)
        Me.TextBoxTemplatePart.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.TextBoxTemplatePart, "Part Template")
        '
        'LabelTemplatePart
        '
        Me.LabelTemplatePart.AutoSize = True
        Me.LabelTemplatePart.Location = New System.Drawing.Point(11, 70)
        Me.LabelTemplatePart.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTemplatePart.Name = "LabelTemplatePart"
        Me.LabelTemplatePart.Size = New System.Drawing.Size(123, 17)
        Me.LabelTemplatePart.TabIndex = 4
        Me.LabelTemplatePart.Text = "Part Template File"
        '
        'ButtonTemplateSheetmetal
        '
        Me.ButtonTemplateSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTemplateSheetmetal.Location = New System.Drawing.Point(510, 150)
        Me.ButtonTemplateSheetmetal.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonTemplateSheetmetal.Name = "ButtonTemplateSheetmetal"
        Me.ButtonTemplateSheetmetal.Size = New System.Drawing.Size(100, 25)
        Me.ButtonTemplateSheetmetal.TabIndex = 6
        Me.ButtonTemplateSheetmetal.Text = "Browse"
        Me.ButtonTemplateSheetmetal.UseVisualStyleBackColor = True
        '
        'TextBoxTemplateSheetmetal
        '
        Me.TextBoxTemplateSheetmetal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplateSheetmetal.Location = New System.Drawing.Point(11, 150)
        Me.TextBoxTemplateSheetmetal.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxTemplateSheetmetal.Name = "TextBoxTemplateSheetmetal"
        Me.TextBoxTemplateSheetmetal.Size = New System.Drawing.Size(480, 22)
        Me.TextBoxTemplateSheetmetal.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.TextBoxTemplateSheetmetal, "Sheetmetal Template")
        '
        'LabelTemplateSheetmetal
        '
        Me.LabelTemplateSheetmetal.AutoSize = True
        Me.LabelTemplateSheetmetal.Location = New System.Drawing.Point(11, 130)
        Me.LabelTemplateSheetmetal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTemplateSheetmetal.Name = "LabelTemplateSheetmetal"
        Me.LabelTemplateSheetmetal.Size = New System.Drawing.Size(168, 17)
        Me.LabelTemplateSheetmetal.TabIndex = 4
        Me.LabelTemplateSheetmetal.Text = "Sheetmetal Template File"
        '
        'ButtonTemplateDraft
        '
        Me.ButtonTemplateDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTemplateDraft.Location = New System.Drawing.Point(510, 210)
        Me.ButtonTemplateDraft.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonTemplateDraft.Name = "ButtonTemplateDraft"
        Me.ButtonTemplateDraft.Size = New System.Drawing.Size(100, 25)
        Me.ButtonTemplateDraft.TabIndex = 6
        Me.ButtonTemplateDraft.Text = "Browse"
        Me.ButtonTemplateDraft.UseVisualStyleBackColor = True
        '
        'TextBoxTemplateDraft
        '
        Me.TextBoxTemplateDraft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplateDraft.Location = New System.Drawing.Point(11, 210)
        Me.TextBoxTemplateDraft.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxTemplateDraft.Name = "TextBoxTemplateDraft"
        Me.TextBoxTemplateDraft.Size = New System.Drawing.Size(480, 22)
        Me.TextBoxTemplateDraft.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.TextBoxTemplateDraft, "Draft Template")
        '
        'LabelTemplateDraft
        '
        Me.LabelTemplateDraft.AutoSize = True
        Me.LabelTemplateDraft.Location = New System.Drawing.Point(11, 190)
        Me.LabelTemplateDraft.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTemplateDraft.Name = "LabelTemplateDraft"
        Me.LabelTemplateDraft.Size = New System.Drawing.Size(128, 17)
        Me.LabelTemplateDraft.TabIndex = 4
        Me.LabelTemplateDraft.Text = "Draft Template File"
        '
        'TabPageReadme
        '
        Me.TabPageReadme.AutoScroll = True
        Me.TabPageReadme.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageReadme.Controls.Add(Me.TextBoxReadme)
        Me.TabPageReadme.Location = New System.Drawing.Point(4, 25)
        Me.TabPageReadme.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPageReadme.Name = "TabPageReadme"
        Me.TabPageReadme.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPageReadme.Size = New System.Drawing.Size(643, 721)
        Me.TabPageReadme.TabIndex = 6
        Me.TabPageReadme.Text = "Readme"
        '
        'TextBoxReadme
        '
        Me.TextBoxReadme.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxReadme.BackColor = System.Drawing.SystemColors.Window
        Me.TextBoxReadme.Location = New System.Drawing.Point(7, 6)
        Me.TextBoxReadme.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxReadme.Multiline = True
        Me.TextBoxReadme.Name = "TextBoxReadme"
        Me.TextBoxReadme.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxReadme.Size = New System.Drawing.Size(629, 700)
        Me.TextBoxReadme.TabIndex = 0
        Me.TextBoxReadme.Text = "Populated at build time."
        '
        'TextBoxStatus
        '
        Me.TextBoxStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStatus.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxStatus.Location = New System.Drawing.Point(5, 750)
        Me.TextBoxStatus.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxStatus.Name = "TextBoxStatus"
        Me.TextBoxStatus.Size = New System.Drawing.Size(649, 22)
        Me.TextBoxStatus.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.TextBoxStatus, "Status")
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ButtonCancel.Location = New System.Drawing.Point(547, 788)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(100, 25)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonProcess
        '
        Me.ButtonProcess.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonProcess.Location = New System.Drawing.Point(421, 788)
        Me.ButtonProcess.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonProcess.Name = "ButtonProcess"
        Me.ButtonProcess.Size = New System.Drawing.Size(100, 25)
        Me.ButtonProcess.TabIndex = 3
        Me.ButtonProcess.Text = "Process"
        Me.ButtonProcess.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'FakeFolderBrowserDialog
        '
        Me.FakeFolderBrowserDialog.CheckFileExists = False
        Me.FakeFolderBrowserDialog.FileName = "Select Folder"
        Me.FakeFolderBrowserDialog.Title = "Select Folder"
        Me.FakeFolderBrowserDialog.ValidateNames = False
        '
        'LabelTimeRemaining
        '
        Me.LabelTimeRemaining.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelTimeRemaining.AutoSize = True
        Me.LabelTimeRemaining.Location = New System.Drawing.Point(5, 785)
        Me.LabelTimeRemaining.Name = "LabelTimeRemaining"
        Me.LabelTimeRemaining.Size = New System.Drawing.Size(0, 17)
        Me.LabelTimeRemaining.TabIndex = 4
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(657, 828)
        Me.Controls.Add(Me.LabelTimeRemaining)
        Me.Controls.Add(Me.ButtonProcess)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.TextBoxStatus)
        Me.Controls.Add(Me.TabControl1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Form1"
        Me.Text = "Solid Edge Housekeeper"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGeneral.ResumeLayout(False)
        Me.TabPageGeneral.PerformLayout()
        Me.GroupBoxFilesToProcess.ResumeLayout(False)
        Me.GroupBoxFilesToProcess.PerformLayout()
        Me.TabPageAssembly.ResumeLayout(False)
        Me.TabPageAssembly.PerformLayout()
        Me.TabPagePart.ResumeLayout(False)
        Me.TabPagePart.PerformLayout()
        Me.TabPageSheetmetal.ResumeLayout(False)
        Me.TabPageSheetmetal.PerformLayout()
        Me.TabPageDraft.ResumeLayout(False)
        Me.TabPageDraft.PerformLayout()
        Me.TabPageConfiguration.ResumeLayout(False)
        Me.TabPageConfiguration.PerformLayout()
        Me.GroupBoxTLAOptions.ResumeLayout(False)
        Me.GroupBoxTLAOptions.PerformLayout()
        Me.TabPageReadme.ResumeLayout(False)
        Me.TabPageReadme.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageGeneral As TabPage
    Friend WithEvents TabPageAssembly As TabPage
    Friend WithEvents TabPagePart As TabPage
    Friend WithEvents TabPageSheetmetal As TabPage
    Friend WithEvents TabPageDraft As TabPage
    Friend WithEvents GroupBoxFilesToProcess As GroupBox
    Friend WithEvents RadioButtonTopLevelAssembly As RadioButton
    Friend WithEvents RadioButtonFilesDirectoryOnly As RadioButton
    Friend WithEvents RadioButtonFilesDirectoriesAndSubdirectories As RadioButton
    Friend WithEvents ListBoxFiles As ListBox
    Friend WithEvents LabelInputDirectory As Label
    Friend WithEvents TextBoxInputDirectory As TextBox
    Friend WithEvents ButtonInputDirectory As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents TextBoxStatus As TextBox
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonProcess As Button
    Friend WithEvents CheckedListBoxAssembly As CheckedListBox
    Friend WithEvents CheckedListBoxPart As CheckedListBox
    Friend WithEvents CheckedListBoxSheetmetal As CheckedListBox
    Friend WithEvents CheckedListBoxDraft As CheckedListBox
    Friend WithEvents ButtonTemplateAssembly As Button
    Friend WithEvents TextBoxTemplateAssembly As TextBox
    Friend WithEvents LabelTemplateAssembly As Label
    Friend WithEvents ButtonTemplatePart As Button
    Friend WithEvents TextBoxTemplatePart As TextBox
    Friend WithEvents LabelTemplatePart As Label
    Friend WithEvents ButtonTemplateSheetmetal As Button
    Friend WithEvents TextBoxTemplateSheetmetal As TextBox
    Friend WithEvents LabelTemplateSheetmetal As Label
    Friend WithEvents ButtonTemplateDraft As Button
    Friend WithEvents TextBoxTemplateDraft As TextBox
    Friend WithEvents LabelTemplateDraft As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TabPageConfiguration As TabPage
    Friend WithEvents TextBoxReadme As TextBox
    Friend WithEvents TabPageReadme As TabPage
    Friend WithEvents LabelActiveMaterialLibrary As Label
    Friend WithEvents ButtonActiveMaterialLibrary As Button
    Friend WithEvents TextBoxActiveMaterialLibrary As TextBox
    Friend WithEvents LabelPartNumberPropertyName As Label
    Friend WithEvents ComboBoxPartNumberPropertySet As ComboBox
    Friend WithEvents LabelPartNumberPropertySet As Label
    Friend WithEvents TextBoxPartNumberPropertyName As TextBox
    Friend WithEvents TextBoxRestartAfter As TextBox
    Friend WithEvents LabelRestartAfter As Label
    Friend WithEvents ButtonLaserOutputDirectory As Button
    Friend WithEvents TextBoxLaserOutputDirectory As TextBox
    Friend WithEvents LabelLaserOutputDirectory As Label
    Friend WithEvents FakeFolderBrowserDialog As OpenFileDialog
    Friend WithEvents LabelAssemblyTabNote As Label
    Friend WithEvents LabelPartTabNote As Label
    Friend WithEvents LabelSheetmetalTabNote As Label
    Friend WithEvents LabelDraftTabNote As Label
    Friend WithEvents ButtonStepAssemblyOutputDirectory As Button
    Friend WithEvents TextBoxStepAssemblyOutputDirectory As TextBox
    Friend WithEvents LabelStepAssemblyOutputDirectory As Label
    Friend WithEvents ButtonStepPartOutputDirectory As Button
    Friend WithEvents TextBoxStepPartOutputDirectory As TextBox
    Friend WithEvents LabelStepPartOutputDirectory As Label
    Friend WithEvents ButtonStepSheetmetalOutputDirectory As Button
    Friend WithEvents TextBoxStepSheetmetalOutputDirectory As TextBox
    Friend WithEvents LabelStepSheetmetalOutputDirectory As Label
    Friend WithEvents ButtonDxfDraftOutputDirectory As Button
    Friend WithEvents ButtonPdfDraftOutputDirectory As Button
    Friend WithEvents TextBoxDxfDraftOutputDirectory As TextBox
    Friend WithEvents TextBoxPdfDraftOutputDirectory As TextBox
    Friend WithEvents LabelDxfDraftOutputDirectory As Label
    Friend WithEvents LabelPdfDraftOutputDirectory As Label
    Friend WithEvents TextBoxColumnWidth As TextBox
    Friend WithEvents LabelColumnWidth As Label
    Friend WithEvents LabelTimeRemaining As Label
    Friend WithEvents LabelListboxFiles As Label
    Friend WithEvents ButtonPropertyFilter As Button
    Friend WithEvents ButtonTopLevelAssembly As Button
    Friend WithEvents LabelTopLevelAssembly As Label
    Friend WithEvents TextBoxTopLevelAssembly As TextBox
    Friend WithEvents ButtonUpdateListBoxFiles As Button
    Friend WithEvents CheckBoxStepAssemblyOutputDirectory As CheckBox
    Friend WithEvents CheckBoxStepPartOutputDirectory As CheckBox
    Friend WithEvents CheckBoxStepSheetmetalOutputDirectory As CheckBox
    Friend WithEvents CheckBoxLaserOutputDirectory As CheckBox
    Friend WithEvents CheckBoxDxfDraftOutputDirectory As CheckBox
    Friend WithEvents CheckBoxPdfDraftOutputDirectory As CheckBox
    Friend WithEvents CheckBoxEnablePropertyFilter As CheckBox
    Friend WithEvents GroupBoxTLAOptions As GroupBox
    Friend WithEvents CheckBoxTLAReportUnrelatedFiles As CheckBox
    Friend WithEvents RadioButtonTLATopDown As RadioButton
    Friend WithEvents RadioButtonTLABottomUp As RadioButton
    Friend WithEvents CheckBoxWarnSave As CheckBox
    Friend WithEvents CheckBoxMoveDrawingViewAllowPartialSuccess As CheckBox
    Friend WithEvents CheckBoxRememberTasks As CheckBox
End Class
