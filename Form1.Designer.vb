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
        Me.LabelFontSize = New System.Windows.Forms.Label()
        Me.TextBoxFontSize = New System.Windows.Forms.TextBox()
        Me.TextBoxFileSearch = New System.Windows.Forms.TextBox()
        Me.CheckBoxFileSearch = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFilterDft = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFilterPsm = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFilterPar = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFilterAsm = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCreateTODOList = New System.Windows.Forms.CheckBox()
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
        Me.RadioButtonTODOList = New System.Windows.Forms.RadioButton()
        Me.RadioButtonTopLevelAssembly = New System.Windows.Forms.RadioButton()
        Me.RadioButtonFilesDirectoryOnly = New System.Windows.Forms.RadioButton()
        Me.RadioButtonFilesDirectoriesAndSubdirectories = New System.Windows.Forms.RadioButton()
        Me.ListBoxFiles = New System.Windows.Forms.ListBox()
        Me.LabelInputDirectory = New System.Windows.Forms.Label()
        Me.TextBoxInputDirectory = New System.Windows.Forms.TextBox()
        Me.ButtonInputDirectory = New System.Windows.Forms.Button()
        Me.TabPageAssembly = New System.Windows.Forms.TabPage()
        Me.TextBoxFindReplaceReplaceAssembly = New System.Windows.Forms.TextBox()
        Me.TextBoxFindReplaceFindAssembly = New System.Windows.Forms.TextBox()
        Me.TextBoxFindReplacePropertyNameAssembly = New System.Windows.Forms.TextBox()
        Me.ComboBoxFindReplacePropertySetAssembly = New System.Windows.Forms.ComboBox()
        Me.LabelFindReplaceReplaceAssembly = New System.Windows.Forms.Label()
        Me.LabelFindReplaceFindAssembly = New System.Windows.Forms.Label()
        Me.LabelFindReplacePropertyNameAssembly = New System.Windows.Forms.Label()
        Me.LabelFindReplacePropertySetAssembly = New System.Windows.Forms.Label()
        Me.ButtonExternalProgramAssembly = New System.Windows.Forms.Button()
        Me.TextBoxExternalProgramAssembly = New System.Windows.Forms.TextBox()
        Me.LabelExternalProgramAssembly = New System.Windows.Forms.Label()
        Me.ComboBoxSaveAsAssemblyFileType = New System.Windows.Forms.ComboBox()
        Me.CheckBoxSaveAsAssemblyOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.ButtonSaveAsAssemblyOutputDirectory = New System.Windows.Forms.Button()
        Me.TextBoxSaveAsAssemblyOutputDirectory = New System.Windows.Forms.TextBox()
        Me.LabelSaveAsAssembly = New System.Windows.Forms.Label()
        Me.LabelAssemblyTabNote = New System.Windows.Forms.Label()
        Me.CheckedListBoxAssembly = New System.Windows.Forms.CheckedListBox()
        Me.TabPagePart = New System.Windows.Forms.TabPage()
        Me.TextBoxFindReplaceReplacePart = New System.Windows.Forms.TextBox()
        Me.TextBoxFindReplaceFindPart = New System.Windows.Forms.TextBox()
        Me.TextBoxFindReplacePropertyNamePart = New System.Windows.Forms.TextBox()
        Me.ComboBoxFindReplacePropertySetPart = New System.Windows.Forms.ComboBox()
        Me.LabelFindReplaceReplacePart = New System.Windows.Forms.Label()
        Me.LabelFindReplaceFindPart = New System.Windows.Forms.Label()
        Me.LabelFindReplacePropertyNamePart = New System.Windows.Forms.Label()
        Me.LabelFindReplacePropertySetPart = New System.Windows.Forms.Label()
        Me.ButtonExternalProgramPart = New System.Windows.Forms.Button()
        Me.TextBoxExternalProgramPart = New System.Windows.Forms.TextBox()
        Me.LabelExternalProgramPart = New System.Windows.Forms.Label()
        Me.ComboBoxSaveAsPartFileType = New System.Windows.Forms.ComboBox()
        Me.CheckBoxSaveAsPartOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.ButtonSaveAsPartOutputDirectory = New System.Windows.Forms.Button()
        Me.TextBoxSaveAsPartOutputDirectory = New System.Windows.Forms.TextBox()
        Me.LabelSaveAsPart = New System.Windows.Forms.Label()
        Me.LabelPartTabNote = New System.Windows.Forms.Label()
        Me.CheckedListBoxPart = New System.Windows.Forms.CheckedListBox()
        Me.TabPageSheetmetal = New System.Windows.Forms.TabPage()
        Me.TextBoxFindReplaceReplaceSheetmetal = New System.Windows.Forms.TextBox()
        Me.TextBoxFindReplaceFindSheetmetal = New System.Windows.Forms.TextBox()
        Me.TextBoxFindReplacePropertyNameSheetmetal = New System.Windows.Forms.TextBox()
        Me.ComboBoxFindReplacePropertySetSheetmetal = New System.Windows.Forms.ComboBox()
        Me.LabelFindReplaceReplaceSheetmetal = New System.Windows.Forms.Label()
        Me.LabelFindReplaceFindSheetmetal = New System.Windows.Forms.Label()
        Me.LabelFindReplacePropertyNameSheetmetal = New System.Windows.Forms.Label()
        Me.LabelFindReplacePropertySetSheetmetal = New System.Windows.Forms.Label()
        Me.CheckBoxSaveAsFlatDXFOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.ButtonSaveAsFlatDXF = New System.Windows.Forms.Button()
        Me.LabelSaveAsFlatDXF = New System.Windows.Forms.Label()
        Me.TextBoxSaveAsFlatDXFOutputDirectory = New System.Windows.Forms.TextBox()
        Me.ButtonExternalProgramSheetmetal = New System.Windows.Forms.Button()
        Me.TextBoxExternalProgramSheetmetal = New System.Windows.Forms.TextBox()
        Me.LabelExternalProgramSheetmetal = New System.Windows.Forms.Label()
        Me.ComboBoxSaveAsSheetmetalFileType = New System.Windows.Forms.ComboBox()
        Me.CheckBoxSaveAsSheetmetalOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.CheckBoxLaserOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.ButtonSaveAsSheetmetalOutputDirectory = New System.Windows.Forms.Button()
        Me.TextBoxSaveAsSheetmetalOutputDirectory = New System.Windows.Forms.TextBox()
        Me.LabelSaveAsSheetmetal = New System.Windows.Forms.Label()
        Me.LabelSheetmetalTabNote = New System.Windows.Forms.Label()
        Me.ButtonLaserOutputDirectory = New System.Windows.Forms.Button()
        Me.TextBoxLaserOutputDirectory = New System.Windows.Forms.TextBox()
        Me.LabelLaserOutputDirectory = New System.Windows.Forms.Label()
        Me.CheckedListBoxSheetmetal = New System.Windows.Forms.CheckedListBox()
        Me.TabPageDraft = New System.Windows.Forms.TabPage()
        Me.ButtonExternalProgramDraft = New System.Windows.Forms.Button()
        Me.TextBoxExternalProgramDraft = New System.Windows.Forms.TextBox()
        Me.LabelExternalProgramDraft = New System.Windows.Forms.Label()
        Me.ComboBoxSaveAsDraftFileType = New System.Windows.Forms.ComboBox()
        Me.CheckBoxSaveAsDraftOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.ButtonSaveAsDraftOutputDirectory = New System.Windows.Forms.Button()
        Me.TextBoxSaveAsDraftOutputDirectory = New System.Windows.Forms.TextBox()
        Me.LabelSaveAsDraftOutputDirectory = New System.Windows.Forms.Label()
        Me.LabelDraftTabNote = New System.Windows.Forms.Label()
        Me.CheckedListBoxDraft = New System.Windows.Forms.CheckedListBox()
        Me.TabPageConfiguration = New System.Windows.Forms.TabPage()
        Me.ButtonPrintOptions = New System.Windows.Forms.Button()
        Me.CheckBoxRememberTasks = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess = New System.Windows.Forms.CheckBox()
        Me.GroupBoxTLAOptions = New System.Windows.Forms.GroupBox()
        Me.ButtonFastSearchScopeFilename = New System.Windows.Forms.Button()
        Me.TextBoxFastSearchScopeFilename = New System.Windows.Forms.TextBox()
        Me.LabelFastSearchScopeFilename = New System.Windows.Forms.Label()
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
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
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
        Me.TabPageGeneral.Controls.Add(Me.LabelFontSize)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxFontSize)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxFileSearch)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxFileSearch)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxFilterDft)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxFilterPsm)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxFilterPar)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxFilterAsm)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxCreateTODOList)
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
        'LabelFontSize
        '
        Me.LabelFontSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFontSize.AutoSize = True
        Me.LabelFontSize.Location = New System.Drawing.Point(96, 520)
        Me.LabelFontSize.Name = "LabelFontSize"
        Me.LabelFontSize.Size = New System.Drawing.Size(65, 17)
        Me.LabelFontSize.TabIndex = 29
        Me.LabelFontSize.Text = "Font size"
        '
        'TextBoxFontSize
        '
        Me.TextBoxFontSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFontSize.Location = New System.Drawing.Point(10, 518)
        Me.TextBoxFontSize.Name = "TextBoxFontSize"
        Me.TextBoxFontSize.Size = New System.Drawing.Size(75, 22)
        Me.TextBoxFontSize.TabIndex = 28
        Me.TextBoxFontSize.Text = "8"
        '
        'TextBoxFileSearch
        '
        Me.TextBoxFileSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFileSearch.Location = New System.Drawing.Point(455, 554)
        Me.TextBoxFileSearch.Name = "TextBoxFileSearch"
        Me.TextBoxFileSearch.Size = New System.Drawing.Size(165, 22)
        Me.TextBoxFileSearch.TabIndex = 27
        '
        'CheckBoxFileSearch
        '
        Me.CheckBoxFileSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFileSearch.AutoSize = True
        Me.CheckBoxFileSearch.Location = New System.Drawing.Point(350, 556)
        Me.CheckBoxFileSearch.Name = "CheckBoxFileSearch"
        Me.CheckBoxFileSearch.Size = New System.Drawing.Size(99, 21)
        Me.CheckBoxFileSearch.TabIndex = 26
        Me.CheckBoxFileSearch.Text = "File search"
        Me.CheckBoxFileSearch.UseVisualStyleBackColor = True
        '
        'CheckBoxFilterDft
        '
        Me.CheckBoxFilterDft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFilterDft.AutoSize = True
        Me.CheckBoxFilterDft.Checked = True
        Me.CheckBoxFilterDft.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxFilterDft.Location = New System.Drawing.Point(575, 518)
        Me.CheckBoxFilterDft.Name = "CheckBoxFilterDft"
        Me.CheckBoxFilterDft.Size = New System.Drawing.Size(55, 21)
        Me.CheckBoxFilterDft.TabIndex = 25
        Me.CheckBoxFilterDft.Text = "*.dft"
        Me.CheckBoxFilterDft.UseVisualStyleBackColor = True
        '
        'CheckBoxFilterPsm
        '
        Me.CheckBoxFilterPsm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFilterPsm.AutoSize = True
        Me.CheckBoxFilterPsm.Checked = True
        Me.CheckBoxFilterPsm.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxFilterPsm.Location = New System.Drawing.Point(500, 518)
        Me.CheckBoxFilterPsm.Name = "CheckBoxFilterPsm"
        Me.CheckBoxFilterPsm.Size = New System.Drawing.Size(65, 21)
        Me.CheckBoxFilterPsm.TabIndex = 24
        Me.CheckBoxFilterPsm.Text = "*.psm"
        Me.CheckBoxFilterPsm.UseVisualStyleBackColor = True
        '
        'CheckBoxFilterPar
        '
        Me.CheckBoxFilterPar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFilterPar.AutoSize = True
        Me.CheckBoxFilterPar.Checked = True
        Me.CheckBoxFilterPar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxFilterPar.Location = New System.Drawing.Point(425, 518)
        Me.CheckBoxFilterPar.Name = "CheckBoxFilterPar"
        Me.CheckBoxFilterPar.Size = New System.Drawing.Size(60, 21)
        Me.CheckBoxFilterPar.TabIndex = 23
        Me.CheckBoxFilterPar.Text = "*.par"
        Me.CheckBoxFilterPar.UseVisualStyleBackColor = True
        '
        'CheckBoxFilterAsm
        '
        Me.CheckBoxFilterAsm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFilterAsm.AutoSize = True
        Me.CheckBoxFilterAsm.Checked = True
        Me.CheckBoxFilterAsm.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxFilterAsm.Location = New System.Drawing.Point(350, 518)
        Me.CheckBoxFilterAsm.Name = "CheckBoxFilterAsm"
        Me.CheckBoxFilterAsm.Size = New System.Drawing.Size(65, 21)
        Me.CheckBoxFilterAsm.TabIndex = 22
        Me.CheckBoxFilterAsm.Text = "*.asm"
        Me.CheckBoxFilterAsm.UseVisualStyleBackColor = True
        '
        'CheckBoxCreateTODOList
        '
        Me.CheckBoxCreateTODOList.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxCreateTODOList.AutoSize = True
        Me.CheckBoxCreateTODOList.Location = New System.Drawing.Point(425, 690)
        Me.CheckBoxCreateTODOList.Name = "CheckBoxCreateTODOList"
        Me.CheckBoxCreateTODOList.Size = New System.Drawing.Size(138, 21)
        Me.CheckBoxCreateTODOList.TabIndex = 21
        Me.CheckBoxCreateTODOList.Text = "Create TODO list"
        Me.CheckBoxCreateTODOList.UseVisualStyleBackColor = True
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
        Me.ButtonUpdateListBoxFiles.BackColor = System.Drawing.SystemColors.Control
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
        Me.TextBoxColumnWidth.Location = New System.Drawing.Point(10, 553)
        Me.TextBoxColumnWidth.Name = "TextBoxColumnWidth"
        Me.TextBoxColumnWidth.Size = New System.Drawing.Size(75, 22)
        Me.TextBoxColumnWidth.TabIndex = 11
        Me.TextBoxColumnWidth.Text = "5.5"
        '
        'LabelColumnWidth
        '
        Me.LabelColumnWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelColumnWidth.AutoSize = True
        Me.LabelColumnWidth.Location = New System.Drawing.Point(96, 556)
        Me.LabelColumnWidth.Name = "LabelColumnWidth"
        Me.LabelColumnWidth.Size = New System.Drawing.Size(172, 17)
        Me.LabelColumnWidth.TabIndex = 9
        Me.LabelColumnWidth.Text = "Column width (pixels/char)"
        '
        'GroupBoxFilesToProcess
        '
        Me.GroupBoxFilesToProcess.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxFilesToProcess.Controls.Add(Me.RadioButtonTODOList)
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
        'RadioButtonTODOList
        '
        Me.RadioButtonTODOList.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadioButtonTODOList.AutoSize = True
        Me.RadioButtonTODOList.Location = New System.Drawing.Point(475, 25)
        Me.RadioButtonTODOList.Name = "RadioButtonTODOList"
        Me.RadioButtonTODOList.Size = New System.Drawing.Size(91, 21)
        Me.RadioButtonTODOList.TabIndex = 3
        Me.RadioButtonTODOList.TabStop = True
        Me.RadioButtonTODOList.Text = "TODO list"
        Me.RadioButtonTODOList.UseVisualStyleBackColor = True
        '
        'RadioButtonTopLevelAssembly
        '
        Me.RadioButtonTopLevelAssembly.AutoSize = True
        Me.RadioButtonTopLevelAssembly.Location = New System.Drawing.Point(300, 25)
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
        Me.RadioButtonFilesDirectoryOnly.Location = New System.Drawing.Point(15, 25)
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
        Me.RadioButtonFilesDirectoriesAndSubdirectories.Location = New System.Drawing.Point(150, 25)
        Me.RadioButtonFilesDirectoriesAndSubdirectories.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RadioButtonFilesDirectoriesAndSubdirectories.Name = "RadioButtonFilesDirectoriesAndSubdirectories"
        Me.RadioButtonFilesDirectoriesAndSubdirectories.Size = New System.Drawing.Size(120, 21)
        Me.RadioButtonFilesDirectoriesAndSubdirectories.TabIndex = 0
        Me.RadioButtonFilesDirectoriesAndSubdirectories.Text = "Subdirectories"
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
        Me.ListBoxFiles.Size = New System.Drawing.Size(610, 404)
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
        Me.TabPageAssembly.Controls.Add(Me.TextBoxFindReplaceReplaceAssembly)
        Me.TabPageAssembly.Controls.Add(Me.TextBoxFindReplaceFindAssembly)
        Me.TabPageAssembly.Controls.Add(Me.TextBoxFindReplacePropertyNameAssembly)
        Me.TabPageAssembly.Controls.Add(Me.ComboBoxFindReplacePropertySetAssembly)
        Me.TabPageAssembly.Controls.Add(Me.LabelFindReplaceReplaceAssembly)
        Me.TabPageAssembly.Controls.Add(Me.LabelFindReplaceFindAssembly)
        Me.TabPageAssembly.Controls.Add(Me.LabelFindReplacePropertyNameAssembly)
        Me.TabPageAssembly.Controls.Add(Me.LabelFindReplacePropertySetAssembly)
        Me.TabPageAssembly.Controls.Add(Me.ButtonExternalProgramAssembly)
        Me.TabPageAssembly.Controls.Add(Me.TextBoxExternalProgramAssembly)
        Me.TabPageAssembly.Controls.Add(Me.LabelExternalProgramAssembly)
        Me.TabPageAssembly.Controls.Add(Me.ComboBoxSaveAsAssemblyFileType)
        Me.TabPageAssembly.Controls.Add(Me.CheckBoxSaveAsAssemblyOutputDirectory)
        Me.TabPageAssembly.Controls.Add(Me.ButtonSaveAsAssemblyOutputDirectory)
        Me.TabPageAssembly.Controls.Add(Me.TextBoxSaveAsAssemblyOutputDirectory)
        Me.TabPageAssembly.Controls.Add(Me.LabelSaveAsAssembly)
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
        'TextBoxFindReplaceReplaceAssembly
        '
        Me.TextBoxFindReplaceReplaceAssembly.Location = New System.Drawing.Point(460, 565)
        Me.TextBoxFindReplaceReplaceAssembly.Name = "TextBoxFindReplaceReplaceAssembly"
        Me.TextBoxFindReplaceReplaceAssembly.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceReplaceAssembly.TabIndex = 17
        '
        'TextBoxFindReplaceFindAssembly
        '
        Me.TextBoxFindReplaceFindAssembly.Location = New System.Drawing.Point(310, 565)
        Me.TextBoxFindReplaceFindAssembly.Name = "TextBoxFindReplaceFindAssembly"
        Me.TextBoxFindReplaceFindAssembly.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceFindAssembly.TabIndex = 16
        '
        'TextBoxFindReplacePropertyNameAssembly
        '
        Me.TextBoxFindReplacePropertyNameAssembly.Location = New System.Drawing.Point(160, 565)
        Me.TextBoxFindReplacePropertyNameAssembly.Name = "TextBoxFindReplacePropertyNameAssembly"
        Me.TextBoxFindReplacePropertyNameAssembly.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplacePropertyNameAssembly.TabIndex = 15
        '
        'ComboBoxFindReplacePropertySetAssembly
        '
        Me.ComboBoxFindReplacePropertySetAssembly.FormattingEnabled = True
        Me.ComboBoxFindReplacePropertySetAssembly.Location = New System.Drawing.Point(10, 565)
        Me.ComboBoxFindReplacePropertySetAssembly.Name = "ComboBoxFindReplacePropertySetAssembly"
        Me.ComboBoxFindReplacePropertySetAssembly.Size = New System.Drawing.Size(125, 24)
        Me.ComboBoxFindReplacePropertySetAssembly.TabIndex = 14
        '
        'LabelFindReplaceReplaceAssembly
        '
        Me.LabelFindReplaceReplaceAssembly.AutoSize = True
        Me.LabelFindReplaceReplaceAssembly.Location = New System.Drawing.Point(460, 540)
        Me.LabelFindReplaceReplaceAssembly.Name = "LabelFindReplaceReplaceAssembly"
        Me.LabelFindReplaceReplaceAssembly.Size = New System.Drawing.Size(60, 17)
        Me.LabelFindReplaceReplaceAssembly.TabIndex = 13
        Me.LabelFindReplaceReplaceAssembly.Text = "Replace"
        '
        'LabelFindReplaceFindAssembly
        '
        Me.LabelFindReplaceFindAssembly.AutoSize = True
        Me.LabelFindReplaceFindAssembly.Location = New System.Drawing.Point(310, 540)
        Me.LabelFindReplaceFindAssembly.Name = "LabelFindReplaceFindAssembly"
        Me.LabelFindReplaceFindAssembly.Size = New System.Drawing.Size(35, 17)
        Me.LabelFindReplaceFindAssembly.TabIndex = 12
        Me.LabelFindReplaceFindAssembly.Text = "Find"
        '
        'LabelFindReplacePropertyNameAssembly
        '
        Me.LabelFindReplacePropertyNameAssembly.AutoSize = True
        Me.LabelFindReplacePropertyNameAssembly.Location = New System.Drawing.Point(160, 540)
        Me.LabelFindReplacePropertyNameAssembly.Name = "LabelFindReplacePropertyNameAssembly"
        Me.LabelFindReplacePropertyNameAssembly.Size = New System.Drawing.Size(101, 17)
        Me.LabelFindReplacePropertyNameAssembly.TabIndex = 11
        Me.LabelFindReplacePropertyNameAssembly.Text = "Property name"
        '
        'LabelFindReplacePropertySetAssembly
        '
        Me.LabelFindReplacePropertySetAssembly.AutoSize = True
        Me.LabelFindReplacePropertySetAssembly.Location = New System.Drawing.Point(10, 540)
        Me.LabelFindReplacePropertySetAssembly.Name = "LabelFindReplacePropertySetAssembly"
        Me.LabelFindReplacePropertySetAssembly.Size = New System.Drawing.Size(87, 17)
        Me.LabelFindReplacePropertySetAssembly.TabIndex = 10
        Me.LabelFindReplacePropertySetAssembly.Text = "Property Set"
        '
        'ButtonExternalProgramAssembly
        '
        Me.ButtonExternalProgramAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalProgramAssembly.Location = New System.Drawing.Point(509, 505)
        Me.ButtonExternalProgramAssembly.Name = "ButtonExternalProgramAssembly"
        Me.ButtonExternalProgramAssembly.Size = New System.Drawing.Size(100, 23)
        Me.ButtonExternalProgramAssembly.TabIndex = 9
        Me.ButtonExternalProgramAssembly.Text = "Browse"
        Me.ButtonExternalProgramAssembly.UseVisualStyleBackColor = True
        '
        'TextBoxExternalProgramAssembly
        '
        Me.TextBoxExternalProgramAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalProgramAssembly.Location = New System.Drawing.Point(10, 505)
        Me.TextBoxExternalProgramAssembly.Name = "TextBoxExternalProgramAssembly"
        Me.TextBoxExternalProgramAssembly.Size = New System.Drawing.Size(490, 22)
        Me.TextBoxExternalProgramAssembly.TabIndex = 8
        '
        'LabelExternalProgramAssembly
        '
        Me.LabelExternalProgramAssembly.AutoSize = True
        Me.LabelExternalProgramAssembly.Location = New System.Drawing.Point(10, 480)
        Me.LabelExternalProgramAssembly.Name = "LabelExternalProgramAssembly"
        Me.LabelExternalProgramAssembly.Size = New System.Drawing.Size(116, 17)
        Me.LabelExternalProgramAssembly.TabIndex = 7
        Me.LabelExternalProgramAssembly.Text = "External program"
        '
        'ComboBoxSaveAsAssemblyFileType
        '
        Me.ComboBoxSaveAsAssemblyFileType.FormattingEnabled = True
        Me.ComboBoxSaveAsAssemblyFileType.Items.AddRange(New Object() {"fake_choice_1", "fake_choice_2"})
        Me.ComboBoxSaveAsAssemblyFileType.Location = New System.Drawing.Point(200, 415)
        Me.ComboBoxSaveAsAssemblyFileType.Name = "ComboBoxSaveAsAssemblyFileType"
        Me.ComboBoxSaveAsAssemblyFileType.Size = New System.Drawing.Size(175, 24)
        Me.ComboBoxSaveAsAssemblyFileType.Sorted = True
        Me.ComboBoxSaveAsAssemblyFileType.TabIndex = 6
        '
        'CheckBoxSaveAsAssemblyOutputDirectory
        '
        Me.CheckBoxSaveAsAssemblyOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Location = New System.Drawing.Point(425, 420)
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Name = "CheckBoxSaveAsAssemblyOutputDirectory"
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Size = New System.Drawing.Size(194, 21)
        Me.CheckBoxSaveAsAssemblyOutputDirectory.TabIndex = 5
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsAssemblyOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsAssemblyOutputDirectory
        '
        Me.ButtonSaveAsAssemblyOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsAssemblyOutputDirectory.Location = New System.Drawing.Point(509, 445)
        Me.ButtonSaveAsAssemblyOutputDirectory.Name = "ButtonSaveAsAssemblyOutputDirectory"
        Me.ButtonSaveAsAssemblyOutputDirectory.Size = New System.Drawing.Size(100, 25)
        Me.ButtonSaveAsAssemblyOutputDirectory.TabIndex = 4
        Me.ButtonSaveAsAssemblyOutputDirectory.Text = "Browse"
        Me.ButtonSaveAsAssemblyOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsAssemblyOutputDirectory
        '
        Me.TextBoxSaveAsAssemblyOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsAssemblyOutputDirectory.Location = New System.Drawing.Point(10, 445)
        Me.TextBoxSaveAsAssemblyOutputDirectory.Name = "TextBoxSaveAsAssemblyOutputDirectory"
        Me.TextBoxSaveAsAssemblyOutputDirectory.Size = New System.Drawing.Size(491, 22)
        Me.TextBoxSaveAsAssemblyOutputDirectory.TabIndex = 3
        '
        'LabelSaveAsAssembly
        '
        Me.LabelSaveAsAssembly.AutoSize = True
        Me.LabelSaveAsAssembly.Location = New System.Drawing.Point(10, 420)
        Me.LabelSaveAsAssembly.Name = "LabelSaveAsAssembly"
        Me.LabelSaveAsAssembly.Size = New System.Drawing.Size(163, 17)
        Me.LabelSaveAsAssembly.TabIndex = 2
        Me.LabelSaveAsAssembly.Text = "Save As output directory"
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
        Me.CheckedListBoxAssembly.Size = New System.Drawing.Size(600, 361)
        Me.CheckedListBoxAssembly.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxAssembly, "Double-click to check/uncheck all")
        '
        'TabPagePart
        '
        Me.TabPagePart.BackColor = System.Drawing.SystemColors.Control
        Me.TabPagePart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPagePart.Controls.Add(Me.TextBoxFindReplaceReplacePart)
        Me.TabPagePart.Controls.Add(Me.TextBoxFindReplaceFindPart)
        Me.TabPagePart.Controls.Add(Me.TextBoxFindReplacePropertyNamePart)
        Me.TabPagePart.Controls.Add(Me.ComboBoxFindReplacePropertySetPart)
        Me.TabPagePart.Controls.Add(Me.LabelFindReplaceReplacePart)
        Me.TabPagePart.Controls.Add(Me.LabelFindReplaceFindPart)
        Me.TabPagePart.Controls.Add(Me.LabelFindReplacePropertyNamePart)
        Me.TabPagePart.Controls.Add(Me.LabelFindReplacePropertySetPart)
        Me.TabPagePart.Controls.Add(Me.ButtonExternalProgramPart)
        Me.TabPagePart.Controls.Add(Me.TextBoxExternalProgramPart)
        Me.TabPagePart.Controls.Add(Me.LabelExternalProgramPart)
        Me.TabPagePart.Controls.Add(Me.ComboBoxSaveAsPartFileType)
        Me.TabPagePart.Controls.Add(Me.CheckBoxSaveAsPartOutputDirectory)
        Me.TabPagePart.Controls.Add(Me.ButtonSaveAsPartOutputDirectory)
        Me.TabPagePart.Controls.Add(Me.TextBoxSaveAsPartOutputDirectory)
        Me.TabPagePart.Controls.Add(Me.LabelSaveAsPart)
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
        'TextBoxFindReplaceReplacePart
        '
        Me.TextBoxFindReplaceReplacePart.Location = New System.Drawing.Point(460, 565)
        Me.TextBoxFindReplaceReplacePart.Name = "TextBoxFindReplaceReplacePart"
        Me.TextBoxFindReplaceReplacePart.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceReplacePart.TabIndex = 25
        '
        'TextBoxFindReplaceFindPart
        '
        Me.TextBoxFindReplaceFindPart.Location = New System.Drawing.Point(310, 565)
        Me.TextBoxFindReplaceFindPart.Name = "TextBoxFindReplaceFindPart"
        Me.TextBoxFindReplaceFindPart.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceFindPart.TabIndex = 24
        '
        'TextBoxFindReplacePropertyNamePart
        '
        Me.TextBoxFindReplacePropertyNamePart.Location = New System.Drawing.Point(160, 565)
        Me.TextBoxFindReplacePropertyNamePart.Name = "TextBoxFindReplacePropertyNamePart"
        Me.TextBoxFindReplacePropertyNamePart.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplacePropertyNamePart.TabIndex = 23
        '
        'ComboBoxFindReplacePropertySetPart
        '
        Me.ComboBoxFindReplacePropertySetPart.FormattingEnabled = True
        Me.ComboBoxFindReplacePropertySetPart.Location = New System.Drawing.Point(10, 565)
        Me.ComboBoxFindReplacePropertySetPart.Name = "ComboBoxFindReplacePropertySetPart"
        Me.ComboBoxFindReplacePropertySetPart.Size = New System.Drawing.Size(125, 24)
        Me.ComboBoxFindReplacePropertySetPart.TabIndex = 22
        '
        'LabelFindReplaceReplacePart
        '
        Me.LabelFindReplaceReplacePart.AutoSize = True
        Me.LabelFindReplaceReplacePart.Location = New System.Drawing.Point(460, 540)
        Me.LabelFindReplaceReplacePart.Name = "LabelFindReplaceReplacePart"
        Me.LabelFindReplaceReplacePart.Size = New System.Drawing.Size(60, 17)
        Me.LabelFindReplaceReplacePart.TabIndex = 21
        Me.LabelFindReplaceReplacePart.Text = "Replace"
        '
        'LabelFindReplaceFindPart
        '
        Me.LabelFindReplaceFindPart.AutoSize = True
        Me.LabelFindReplaceFindPart.Location = New System.Drawing.Point(310, 540)
        Me.LabelFindReplaceFindPart.Name = "LabelFindReplaceFindPart"
        Me.LabelFindReplaceFindPart.Size = New System.Drawing.Size(35, 17)
        Me.LabelFindReplaceFindPart.TabIndex = 20
        Me.LabelFindReplaceFindPart.Text = "Find"
        '
        'LabelFindReplacePropertyNamePart
        '
        Me.LabelFindReplacePropertyNamePart.AutoSize = True
        Me.LabelFindReplacePropertyNamePart.Location = New System.Drawing.Point(160, 540)
        Me.LabelFindReplacePropertyNamePart.Name = "LabelFindReplacePropertyNamePart"
        Me.LabelFindReplacePropertyNamePart.Size = New System.Drawing.Size(101, 17)
        Me.LabelFindReplacePropertyNamePart.TabIndex = 19
        Me.LabelFindReplacePropertyNamePart.Text = "Property name"
        '
        'LabelFindReplacePropertySetPart
        '
        Me.LabelFindReplacePropertySetPart.AutoSize = True
        Me.LabelFindReplacePropertySetPart.Location = New System.Drawing.Point(10, 540)
        Me.LabelFindReplacePropertySetPart.Name = "LabelFindReplacePropertySetPart"
        Me.LabelFindReplacePropertySetPart.Size = New System.Drawing.Size(87, 17)
        Me.LabelFindReplacePropertySetPart.TabIndex = 18
        Me.LabelFindReplacePropertySetPart.Text = "Property Set"
        '
        'ButtonExternalProgramPart
        '
        Me.ButtonExternalProgramPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalProgramPart.Location = New System.Drawing.Point(509, 505)
        Me.ButtonExternalProgramPart.Name = "ButtonExternalProgramPart"
        Me.ButtonExternalProgramPart.Size = New System.Drawing.Size(100, 23)
        Me.ButtonExternalProgramPart.TabIndex = 10
        Me.ButtonExternalProgramPart.Text = "Browse"
        Me.ButtonExternalProgramPart.UseVisualStyleBackColor = True
        '
        'TextBoxExternalProgramPart
        '
        Me.TextBoxExternalProgramPart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalProgramPart.Location = New System.Drawing.Point(10, 505)
        Me.TextBoxExternalProgramPart.Name = "TextBoxExternalProgramPart"
        Me.TextBoxExternalProgramPart.Size = New System.Drawing.Size(490, 22)
        Me.TextBoxExternalProgramPart.TabIndex = 9
        '
        'LabelExternalProgramPart
        '
        Me.LabelExternalProgramPart.AutoSize = True
        Me.LabelExternalProgramPart.Location = New System.Drawing.Point(10, 480)
        Me.LabelExternalProgramPart.Name = "LabelExternalProgramPart"
        Me.LabelExternalProgramPart.Size = New System.Drawing.Size(116, 17)
        Me.LabelExternalProgramPart.TabIndex = 8
        Me.LabelExternalProgramPart.Text = "External program"
        '
        'ComboBoxSaveAsPartFileType
        '
        Me.ComboBoxSaveAsPartFileType.FormattingEnabled = True
        Me.ComboBoxSaveAsPartFileType.Items.AddRange(New Object() {"fake_item_1"})
        Me.ComboBoxSaveAsPartFileType.Location = New System.Drawing.Point(200, 415)
        Me.ComboBoxSaveAsPartFileType.Name = "ComboBoxSaveAsPartFileType"
        Me.ComboBoxSaveAsPartFileType.Size = New System.Drawing.Size(175, 24)
        Me.ComboBoxSaveAsPartFileType.Sorted = True
        Me.ComboBoxSaveAsPartFileType.TabIndex = 7
        '
        'CheckBoxSaveAsPartOutputDirectory
        '
        Me.CheckBoxSaveAsPartOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsPartOutputDirectory.Location = New System.Drawing.Point(425, 420)
        Me.CheckBoxSaveAsPartOutputDirectory.Name = "CheckBoxSaveAsPartOutputDirectory"
        Me.CheckBoxSaveAsPartOutputDirectory.Size = New System.Drawing.Size(194, 21)
        Me.CheckBoxSaveAsPartOutputDirectory.TabIndex = 6
        Me.CheckBoxSaveAsPartOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsPartOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsPartOutputDirectory
        '
        Me.ButtonSaveAsPartOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsPartOutputDirectory.Location = New System.Drawing.Point(509, 445)
        Me.ButtonSaveAsPartOutputDirectory.Name = "ButtonSaveAsPartOutputDirectory"
        Me.ButtonSaveAsPartOutputDirectory.Size = New System.Drawing.Size(100, 25)
        Me.ButtonSaveAsPartOutputDirectory.TabIndex = 5
        Me.ButtonSaveAsPartOutputDirectory.Text = "Browse"
        Me.ButtonSaveAsPartOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsPartOutputDirectory
        '
        Me.TextBoxSaveAsPartOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsPartOutputDirectory.Location = New System.Drawing.Point(10, 445)
        Me.TextBoxSaveAsPartOutputDirectory.Name = "TextBoxSaveAsPartOutputDirectory"
        Me.TextBoxSaveAsPartOutputDirectory.Size = New System.Drawing.Size(491, 22)
        Me.TextBoxSaveAsPartOutputDirectory.TabIndex = 4
        '
        'LabelSaveAsPart
        '
        Me.LabelSaveAsPart.AutoSize = True
        Me.LabelSaveAsPart.Location = New System.Drawing.Point(10, 420)
        Me.LabelSaveAsPart.Name = "LabelSaveAsPart"
        Me.LabelSaveAsPart.Size = New System.Drawing.Size(163, 17)
        Me.LabelSaveAsPart.TabIndex = 3
        Me.LabelSaveAsPart.Text = "Save As output directory"
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
        Me.CheckedListBoxPart.Size = New System.Drawing.Size(600, 361)
        Me.CheckedListBoxPart.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxPart, "Double-click to check/uncheck all")
        '
        'TabPageSheetmetal
        '
        Me.TabPageSheetmetal.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageSheetmetal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxFindReplaceReplaceSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxFindReplaceFindSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxFindReplacePropertyNameSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.ComboBoxFindReplacePropertySetSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelFindReplaceReplaceSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelFindReplaceFindSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelFindReplacePropertyNameSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelFindReplacePropertySetSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.CheckBoxSaveAsFlatDXFOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.ButtonSaveAsFlatDXF)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelSaveAsFlatDXF)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxSaveAsFlatDXFOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.ButtonExternalProgramSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxExternalProgramSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelExternalProgramSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.ComboBoxSaveAsSheetmetalFileType)
        Me.TabPageSheetmetal.Controls.Add(Me.CheckBoxSaveAsSheetmetalOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.CheckBoxLaserOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.ButtonSaveAsSheetmetalOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxSaveAsSheetmetalOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelSaveAsSheetmetal)
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
        'TextBoxFindReplaceReplaceSheetmetal
        '
        Me.TextBoxFindReplaceReplaceSheetmetal.Location = New System.Drawing.Point(460, 685)
        Me.TextBoxFindReplaceReplaceSheetmetal.Name = "TextBoxFindReplaceReplaceSheetmetal"
        Me.TextBoxFindReplaceReplaceSheetmetal.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceReplaceSheetmetal.TabIndex = 33
        '
        'TextBoxFindReplaceFindSheetmetal
        '
        Me.TextBoxFindReplaceFindSheetmetal.Location = New System.Drawing.Point(310, 685)
        Me.TextBoxFindReplaceFindSheetmetal.Name = "TextBoxFindReplaceFindSheetmetal"
        Me.TextBoxFindReplaceFindSheetmetal.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceFindSheetmetal.TabIndex = 32
        '
        'TextBoxFindReplacePropertyNameSheetmetal
        '
        Me.TextBoxFindReplacePropertyNameSheetmetal.Location = New System.Drawing.Point(160, 685)
        Me.TextBoxFindReplacePropertyNameSheetmetal.Name = "TextBoxFindReplacePropertyNameSheetmetal"
        Me.TextBoxFindReplacePropertyNameSheetmetal.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplacePropertyNameSheetmetal.TabIndex = 31
        '
        'ComboBoxFindReplacePropertySetSheetmetal
        '
        Me.ComboBoxFindReplacePropertySetSheetmetal.FormattingEnabled = True
        Me.ComboBoxFindReplacePropertySetSheetmetal.Location = New System.Drawing.Point(10, 685)
        Me.ComboBoxFindReplacePropertySetSheetmetal.Name = "ComboBoxFindReplacePropertySetSheetmetal"
        Me.ComboBoxFindReplacePropertySetSheetmetal.Size = New System.Drawing.Size(125, 24)
        Me.ComboBoxFindReplacePropertySetSheetmetal.TabIndex = 30
        '
        'LabelFindReplaceReplaceSheetmetal
        '
        Me.LabelFindReplaceReplaceSheetmetal.AutoSize = True
        Me.LabelFindReplaceReplaceSheetmetal.Location = New System.Drawing.Point(460, 660)
        Me.LabelFindReplaceReplaceSheetmetal.Name = "LabelFindReplaceReplaceSheetmetal"
        Me.LabelFindReplaceReplaceSheetmetal.Size = New System.Drawing.Size(60, 17)
        Me.LabelFindReplaceReplaceSheetmetal.TabIndex = 29
        Me.LabelFindReplaceReplaceSheetmetal.Text = "Replace"
        '
        'LabelFindReplaceFindSheetmetal
        '
        Me.LabelFindReplaceFindSheetmetal.AutoSize = True
        Me.LabelFindReplaceFindSheetmetal.Location = New System.Drawing.Point(310, 660)
        Me.LabelFindReplaceFindSheetmetal.Name = "LabelFindReplaceFindSheetmetal"
        Me.LabelFindReplaceFindSheetmetal.Size = New System.Drawing.Size(35, 17)
        Me.LabelFindReplaceFindSheetmetal.TabIndex = 28
        Me.LabelFindReplaceFindSheetmetal.Text = "Find"
        '
        'LabelFindReplacePropertyNameSheetmetal
        '
        Me.LabelFindReplacePropertyNameSheetmetal.AutoSize = True
        Me.LabelFindReplacePropertyNameSheetmetal.Location = New System.Drawing.Point(160, 660)
        Me.LabelFindReplacePropertyNameSheetmetal.Name = "LabelFindReplacePropertyNameSheetmetal"
        Me.LabelFindReplacePropertyNameSheetmetal.Size = New System.Drawing.Size(101, 17)
        Me.LabelFindReplacePropertyNameSheetmetal.TabIndex = 27
        Me.LabelFindReplacePropertyNameSheetmetal.Text = "Property name"
        '
        'LabelFindReplacePropertySetSheetmetal
        '
        Me.LabelFindReplacePropertySetSheetmetal.AutoSize = True
        Me.LabelFindReplacePropertySetSheetmetal.Location = New System.Drawing.Point(10, 660)
        Me.LabelFindReplacePropertySetSheetmetal.Name = "LabelFindReplacePropertySetSheetmetal"
        Me.LabelFindReplacePropertySetSheetmetal.Size = New System.Drawing.Size(87, 17)
        Me.LabelFindReplacePropertySetSheetmetal.TabIndex = 26
        Me.LabelFindReplacePropertySetSheetmetal.Text = "Property Set"
        '
        'CheckBoxSaveAsFlatDXFOutputDirectory
        '
        Me.CheckBoxSaveAsFlatDXFOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsFlatDXFOutputDirectory.Location = New System.Drawing.Point(245, 480)
        Me.CheckBoxSaveAsFlatDXFOutputDirectory.Name = "CheckBoxSaveAsFlatDXFOutputDirectory"
        Me.CheckBoxSaveAsFlatDXFOutputDirectory.Size = New System.Drawing.Size(194, 21)
        Me.CheckBoxSaveAsFlatDXFOutputDirectory.TabIndex = 19
        Me.CheckBoxSaveAsFlatDXFOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsFlatDXFOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsFlatDXF
        '
        Me.ButtonSaveAsFlatDXF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsFlatDXF.Location = New System.Drawing.Point(509, 505)
        Me.ButtonSaveAsFlatDXF.Name = "ButtonSaveAsFlatDXF"
        Me.ButtonSaveAsFlatDXF.Size = New System.Drawing.Size(100, 23)
        Me.ButtonSaveAsFlatDXF.TabIndex = 18
        Me.ButtonSaveAsFlatDXF.Text = "Browse"
        Me.ButtonSaveAsFlatDXF.UseVisualStyleBackColor = True
        '
        'LabelSaveAsFlatDXF
        '
        Me.LabelSaveAsFlatDXF.AutoSize = True
        Me.LabelSaveAsFlatDXF.Location = New System.Drawing.Point(10, 480)
        Me.LabelSaveAsFlatDXF.Name = "LabelSaveAsFlatDXF"
        Me.LabelSaveAsFlatDXF.Size = New System.Drawing.Size(221, 17)
        Me.LabelSaveAsFlatDXF.TabIndex = 17
        Me.LabelSaveAsFlatDXF.Text = "Save As Flat DXF output directory"
        '
        'TextBoxSaveAsFlatDXFOutputDirectory
        '
        Me.TextBoxSaveAsFlatDXFOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsFlatDXFOutputDirectory.Location = New System.Drawing.Point(10, 505)
        Me.TextBoxSaveAsFlatDXFOutputDirectory.Name = "TextBoxSaveAsFlatDXFOutputDirectory"
        Me.TextBoxSaveAsFlatDXFOutputDirectory.Size = New System.Drawing.Size(491, 22)
        Me.TextBoxSaveAsFlatDXFOutputDirectory.TabIndex = 16
        '
        'ButtonExternalProgramSheetmetal
        '
        Me.ButtonExternalProgramSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalProgramSheetmetal.Location = New System.Drawing.Point(509, 625)
        Me.ButtonExternalProgramSheetmetal.Name = "ButtonExternalProgramSheetmetal"
        Me.ButtonExternalProgramSheetmetal.Size = New System.Drawing.Size(100, 23)
        Me.ButtonExternalProgramSheetmetal.TabIndex = 15
        Me.ButtonExternalProgramSheetmetal.Text = "Browse"
        Me.ButtonExternalProgramSheetmetal.UseVisualStyleBackColor = True
        '
        'TextBoxExternalProgramSheetmetal
        '
        Me.TextBoxExternalProgramSheetmetal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalProgramSheetmetal.Location = New System.Drawing.Point(10, 625)
        Me.TextBoxExternalProgramSheetmetal.Name = "TextBoxExternalProgramSheetmetal"
        Me.TextBoxExternalProgramSheetmetal.Size = New System.Drawing.Size(490, 22)
        Me.TextBoxExternalProgramSheetmetal.TabIndex = 14
        '
        'LabelExternalProgramSheetmetal
        '
        Me.LabelExternalProgramSheetmetal.AutoSize = True
        Me.LabelExternalProgramSheetmetal.Location = New System.Drawing.Point(10, 600)
        Me.LabelExternalProgramSheetmetal.Name = "LabelExternalProgramSheetmetal"
        Me.LabelExternalProgramSheetmetal.Size = New System.Drawing.Size(116, 17)
        Me.LabelExternalProgramSheetmetal.TabIndex = 13
        Me.LabelExternalProgramSheetmetal.Text = "External program"
        '
        'ComboBoxSaveAsSheetmetalFileType
        '
        Me.ComboBoxSaveAsSheetmetalFileType.FormattingEnabled = True
        Me.ComboBoxSaveAsSheetmetalFileType.Items.AddRange(New Object() {"fake_item_1"})
        Me.ComboBoxSaveAsSheetmetalFileType.Location = New System.Drawing.Point(200, 415)
        Me.ComboBoxSaveAsSheetmetalFileType.Name = "ComboBoxSaveAsSheetmetalFileType"
        Me.ComboBoxSaveAsSheetmetalFileType.Size = New System.Drawing.Size(175, 24)
        Me.ComboBoxSaveAsSheetmetalFileType.Sorted = True
        Me.ComboBoxSaveAsSheetmetalFileType.TabIndex = 12
        '
        'CheckBoxSaveAsSheetmetalOutputDirectory
        '
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Location = New System.Drawing.Point(425, 420)
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Name = "CheckBoxSaveAsSheetmetalOutputDirectory"
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Size = New System.Drawing.Size(194, 21)
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.TabIndex = 11
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.UseVisualStyleBackColor = True
        '
        'CheckBoxLaserOutputDirectory
        '
        Me.CheckBoxLaserOutputDirectory.AutoSize = True
        Me.CheckBoxLaserOutputDirectory.Location = New System.Drawing.Point(245, 540)
        Me.CheckBoxLaserOutputDirectory.Name = "CheckBoxLaserOutputDirectory"
        Me.CheckBoxLaserOutputDirectory.Size = New System.Drawing.Size(194, 21)
        Me.CheckBoxLaserOutputDirectory.TabIndex = 10
        Me.CheckBoxLaserOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxLaserOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsSheetmetalOutputDirectory
        '
        Me.ButtonSaveAsSheetmetalOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsSheetmetalOutputDirectory.Location = New System.Drawing.Point(509, 445)
        Me.ButtonSaveAsSheetmetalOutputDirectory.Name = "ButtonSaveAsSheetmetalOutputDirectory"
        Me.ButtonSaveAsSheetmetalOutputDirectory.Size = New System.Drawing.Size(100, 25)
        Me.ButtonSaveAsSheetmetalOutputDirectory.TabIndex = 9
        Me.ButtonSaveAsSheetmetalOutputDirectory.Text = "Browse"
        Me.ButtonSaveAsSheetmetalOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsSheetmetalOutputDirectory
        '
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Location = New System.Drawing.Point(10, 445)
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Name = "TextBoxSaveAsSheetmetalOutputDirectory"
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Size = New System.Drawing.Size(491, 22)
        Me.TextBoxSaveAsSheetmetalOutputDirectory.TabIndex = 8
        '
        'LabelSaveAsSheetmetal
        '
        Me.LabelSaveAsSheetmetal.AutoSize = True
        Me.LabelSaveAsSheetmetal.Location = New System.Drawing.Point(10, 420)
        Me.LabelSaveAsSheetmetal.Name = "LabelSaveAsSheetmetal"
        Me.LabelSaveAsSheetmetal.Size = New System.Drawing.Size(163, 17)
        Me.LabelSaveAsSheetmetal.TabIndex = 7
        Me.LabelSaveAsSheetmetal.Text = "Save As output directory"
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
        Me.ButtonLaserOutputDirectory.Location = New System.Drawing.Point(510, 565)
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
        Me.TextBoxLaserOutputDirectory.Location = New System.Drawing.Point(10, 565)
        Me.TextBoxLaserOutputDirectory.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxLaserOutputDirectory.Name = "TextBoxLaserOutputDirectory"
        Me.TextBoxLaserOutputDirectory.Size = New System.Drawing.Size(491, 22)
        Me.TextBoxLaserOutputDirectory.TabIndex = 4
        '
        'LabelLaserOutputDirectory
        '
        Me.LabelLaserOutputDirectory.AutoSize = True
        Me.LabelLaserOutputDirectory.Location = New System.Drawing.Point(10, 540)
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
        Me.CheckedListBoxSheetmetal.Size = New System.Drawing.Size(600, 361)
        Me.CheckedListBoxSheetmetal.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxSheetmetal, "Double-click to check/uncheck all")
        '
        'TabPageDraft
        '
        Me.TabPageDraft.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageDraft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageDraft.Controls.Add(Me.ButtonExternalProgramDraft)
        Me.TabPageDraft.Controls.Add(Me.TextBoxExternalProgramDraft)
        Me.TabPageDraft.Controls.Add(Me.LabelExternalProgramDraft)
        Me.TabPageDraft.Controls.Add(Me.ComboBoxSaveAsDraftFileType)
        Me.TabPageDraft.Controls.Add(Me.CheckBoxSaveAsDraftOutputDirectory)
        Me.TabPageDraft.Controls.Add(Me.ButtonSaveAsDraftOutputDirectory)
        Me.TabPageDraft.Controls.Add(Me.TextBoxSaveAsDraftOutputDirectory)
        Me.TabPageDraft.Controls.Add(Me.LabelSaveAsDraftOutputDirectory)
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
        'ButtonExternalProgramDraft
        '
        Me.ButtonExternalProgramDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalProgramDraft.Location = New System.Drawing.Point(509, 504)
        Me.ButtonExternalProgramDraft.Name = "ButtonExternalProgramDraft"
        Me.ButtonExternalProgramDraft.Size = New System.Drawing.Size(100, 23)
        Me.ButtonExternalProgramDraft.TabIndex = 15
        Me.ButtonExternalProgramDraft.Text = "Browse"
        Me.ButtonExternalProgramDraft.UseVisualStyleBackColor = True
        '
        'TextBoxExternalProgramDraft
        '
        Me.TextBoxExternalProgramDraft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalProgramDraft.Location = New System.Drawing.Point(10, 504)
        Me.TextBoxExternalProgramDraft.Name = "TextBoxExternalProgramDraft"
        Me.TextBoxExternalProgramDraft.Size = New System.Drawing.Size(490, 22)
        Me.TextBoxExternalProgramDraft.TabIndex = 14
        '
        'LabelExternalProgramDraft
        '
        Me.LabelExternalProgramDraft.AutoSize = True
        Me.LabelExternalProgramDraft.Location = New System.Drawing.Point(10, 479)
        Me.LabelExternalProgramDraft.Name = "LabelExternalProgramDraft"
        Me.LabelExternalProgramDraft.Size = New System.Drawing.Size(116, 17)
        Me.LabelExternalProgramDraft.TabIndex = 13
        Me.LabelExternalProgramDraft.Text = "External program"
        '
        'ComboBoxSaveAsDraftFileType
        '
        Me.ComboBoxSaveAsDraftFileType.FormattingEnabled = True
        Me.ComboBoxSaveAsDraftFileType.Items.AddRange(New Object() {"fake_item_1"})
        Me.ComboBoxSaveAsDraftFileType.Location = New System.Drawing.Point(200, 414)
        Me.ComboBoxSaveAsDraftFileType.Name = "ComboBoxSaveAsDraftFileType"
        Me.ComboBoxSaveAsDraftFileType.Size = New System.Drawing.Size(175, 24)
        Me.ComboBoxSaveAsDraftFileType.Sorted = True
        Me.ComboBoxSaveAsDraftFileType.TabIndex = 12
        '
        'CheckBoxSaveAsDraftOutputDirectory
        '
        Me.CheckBoxSaveAsDraftOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsDraftOutputDirectory.Location = New System.Drawing.Point(425, 419)
        Me.CheckBoxSaveAsDraftOutputDirectory.Name = "CheckBoxSaveAsDraftOutputDirectory"
        Me.CheckBoxSaveAsDraftOutputDirectory.Size = New System.Drawing.Size(194, 21)
        Me.CheckBoxSaveAsDraftOutputDirectory.TabIndex = 10
        Me.CheckBoxSaveAsDraftOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsDraftOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsDraftOutputDirectory
        '
        Me.ButtonSaveAsDraftOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsDraftOutputDirectory.Location = New System.Drawing.Point(509, 444)
        Me.ButtonSaveAsDraftOutputDirectory.Name = "ButtonSaveAsDraftOutputDirectory"
        Me.ButtonSaveAsDraftOutputDirectory.Size = New System.Drawing.Size(100, 25)
        Me.ButtonSaveAsDraftOutputDirectory.TabIndex = 8
        Me.ButtonSaveAsDraftOutputDirectory.Text = "Browse"
        Me.ButtonSaveAsDraftOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsDraftOutputDirectory
        '
        Me.TextBoxSaveAsDraftOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsDraftOutputDirectory.Location = New System.Drawing.Point(10, 444)
        Me.TextBoxSaveAsDraftOutputDirectory.Name = "TextBoxSaveAsDraftOutputDirectory"
        Me.TextBoxSaveAsDraftOutputDirectory.Size = New System.Drawing.Size(491, 22)
        Me.TextBoxSaveAsDraftOutputDirectory.TabIndex = 6
        '
        'LabelSaveAsDraftOutputDirectory
        '
        Me.LabelSaveAsDraftOutputDirectory.AutoSize = True
        Me.LabelSaveAsDraftOutputDirectory.Location = New System.Drawing.Point(10, 419)
        Me.LabelSaveAsDraftOutputDirectory.Name = "LabelSaveAsDraftOutputDirectory"
        Me.LabelSaveAsDraftOutputDirectory.Size = New System.Drawing.Size(163, 17)
        Me.LabelSaveAsDraftOutputDirectory.TabIndex = 4
        Me.LabelSaveAsDraftOutputDirectory.Text = "Save As output directory"
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
        Me.CheckedListBoxDraft.Size = New System.Drawing.Size(600, 361)
        Me.CheckedListBoxDraft.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxDraft, "Double-click to check/uncheck all")
        '
        'TabPageConfiguration
        '
        Me.TabPageConfiguration.AutoScroll = True
        Me.TabPageConfiguration.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageConfiguration.Controls.Add(Me.ButtonPrintOptions)
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
        'ButtonPrintOptions
        '
        Me.ButtonPrintOptions.Location = New System.Drawing.Point(10, 735)
        Me.ButtonPrintOptions.Name = "ButtonPrintOptions"
        Me.ButtonPrintOptions.Size = New System.Drawing.Size(150, 23)
        Me.ButtonPrintOptions.TabIndex = 20
        Me.ButtonPrintOptions.Text = "Printer Settings"
        Me.ButtonPrintOptions.UseVisualStyleBackColor = True
        '
        'CheckBoxRememberTasks
        '
        Me.CheckBoxRememberTasks.AutoSize = True
        Me.CheckBoxRememberTasks.Checked = True
        Me.CheckBoxRememberTasks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRememberTasks.Location = New System.Drawing.Point(11, 685)
        Me.CheckBoxRememberTasks.Name = "CheckBoxRememberTasks"
        Me.CheckBoxRememberTasks.Size = New System.Drawing.Size(309, 21)
        Me.CheckBoxRememberTasks.TabIndex = 19
        Me.CheckBoxRememberTasks.Text = "Remember selected tasks between sessions"
        Me.CheckBoxRememberTasks.UseVisualStyleBackColor = True
        '
        'CheckBoxMoveDrawingViewAllowPartialSuccess
        '
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.AutoSize = True
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Location = New System.Drawing.Point(11, 655)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Name = "CheckBoxMoveDrawingViewAllowPartialSuccess"
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Size = New System.Drawing.Size(368, 21)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.TabIndex = 18
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Text = "Move drawing to new template -- Allow partial success"
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.UseVisualStyleBackColor = True
        '
        'GroupBoxTLAOptions
        '
        Me.GroupBoxTLAOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxTLAOptions.Controls.Add(Me.ButtonFastSearchScopeFilename)
        Me.GroupBoxTLAOptions.Controls.Add(Me.TextBoxFastSearchScopeFilename)
        Me.GroupBoxTLAOptions.Controls.Add(Me.LabelFastSearchScopeFilename)
        Me.GroupBoxTLAOptions.Controls.Add(Me.CheckBoxTLAReportUnrelatedFiles)
        Me.GroupBoxTLAOptions.Controls.Add(Me.RadioButtonTLATopDown)
        Me.GroupBoxTLAOptions.Controls.Add(Me.RadioButtonTLABottomUp)
        Me.GroupBoxTLAOptions.Location = New System.Drawing.Point(11, 435)
        Me.GroupBoxTLAOptions.Name = "GroupBoxTLAOptions"
        Me.GroupBoxTLAOptions.Size = New System.Drawing.Size(564, 172)
        Me.GroupBoxTLAOptions.TabIndex = 17
        Me.GroupBoxTLAOptions.TabStop = False
        Me.GroupBoxTLAOptions.Text = "Top level assembly processing options -- See Readme tab for details"
        '
        'ButtonFastSearchScopeFilename
        '
        Me.ButtonFastSearchScopeFilename.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonFastSearchScopeFilename.Location = New System.Drawing.Point(436, 119)
        Me.ButtonFastSearchScopeFilename.Name = "ButtonFastSearchScopeFilename"
        Me.ButtonFastSearchScopeFilename.Size = New System.Drawing.Size(100, 25)
        Me.ButtonFastSearchScopeFilename.TabIndex = 5
        Me.ButtonFastSearchScopeFilename.Text = "Browse"
        Me.ButtonFastSearchScopeFilename.UseVisualStyleBackColor = True
        '
        'TextBoxFastSearchScopeFilename
        '
        Me.TextBoxFastSearchScopeFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFastSearchScopeFilename.Location = New System.Drawing.Point(15, 120)
        Me.TextBoxFastSearchScopeFilename.Name = "TextBoxFastSearchScopeFilename"
        Me.TextBoxFastSearchScopeFilename.Size = New System.Drawing.Size(402, 22)
        Me.TextBoxFastSearchScopeFilename.TabIndex = 4
        '
        'LabelFastSearchScopeFilename
        '
        Me.LabelFastSearchScopeFilename.AutoSize = True
        Me.LabelFastSearchScopeFilename.Location = New System.Drawing.Point(15, 100)
        Me.LabelFastSearchScopeFilename.Name = "LabelFastSearchScopeFilename"
        Me.LabelFastSearchScopeFilename.Size = New System.Drawing.Size(189, 17)
        Me.LabelFastSearchScopeFilename.TabIndex = 3
        Me.LabelFastSearchScopeFilename.Text = "Fast Search Scope Filename"
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
        Me.CheckBoxWarnSave.Location = New System.Drawing.Point(11, 625)
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
        Me.ComboBoxPartNumberPropertySet.Items.AddRange(New Object() {"fake_item_1"})
        Me.ComboBoxPartNumberPropertySet.Location = New System.Drawing.Point(11, 330)
        Me.ComboBoxPartNumberPropertySet.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ComboBoxPartNumberPropertySet.Name = "ComboBoxPartNumberPropertySet"
        Me.ComboBoxPartNumberPropertySet.Size = New System.Drawing.Size(175, 24)
        Me.ComboBoxPartNumberPropertySet.Sorted = True
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
        Me.ButtonActiveMaterialLibrary.Location = New System.Drawing.Point(447, 270)
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
        Me.TextBoxActiveMaterialLibrary.Size = New System.Drawing.Size(417, 22)
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
        Me.ButtonTemplateAssembly.Location = New System.Drawing.Point(447, 30)
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
        Me.TextBoxTemplateAssembly.Size = New System.Drawing.Size(417, 22)
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
        Me.ButtonTemplatePart.Location = New System.Drawing.Point(447, 90)
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
        Me.TextBoxTemplatePart.Size = New System.Drawing.Size(417, 22)
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
        Me.ButtonTemplateSheetmetal.Location = New System.Drawing.Point(447, 150)
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
        Me.TextBoxTemplateSheetmetal.Size = New System.Drawing.Size(417, 22)
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
        Me.ButtonTemplateDraft.Location = New System.Drawing.Point(447, 210)
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
        Me.TextBoxTemplateDraft.Size = New System.Drawing.Size(417, 22)
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
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
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
    Friend WithEvents ButtonSaveAsAssemblyOutputDirectory As Button
    Friend WithEvents TextBoxSaveAsAssemblyOutputDirectory As TextBox
    Friend WithEvents LabelSaveAsAssembly As Label
    Friend WithEvents ButtonSaveAsPartOutputDirectory As Button
    Friend WithEvents TextBoxSaveAsPartOutputDirectory As TextBox
    Friend WithEvents LabelSaveAsPart As Label
    Friend WithEvents ButtonSaveAsSheetmetalOutputDirectory As Button
    Friend WithEvents TextBoxSaveAsSheetmetalOutputDirectory As TextBox
    Friend WithEvents LabelSaveAsSheetmetal As Label
    Friend WithEvents ButtonSaveAsDraftOutputDirectory As Button
    Friend WithEvents TextBoxSaveAsDraftOutputDirectory As TextBox
    Friend WithEvents LabelSaveAsDraftOutputDirectory As Label
    Friend WithEvents TextBoxColumnWidth As TextBox
    Friend WithEvents LabelColumnWidth As Label
    Friend WithEvents LabelTimeRemaining As Label
    Friend WithEvents LabelListboxFiles As Label
    Friend WithEvents ButtonPropertyFilter As Button
    Friend WithEvents ButtonTopLevelAssembly As Button
    Friend WithEvents LabelTopLevelAssembly As Label
    Friend WithEvents TextBoxTopLevelAssembly As TextBox
    Friend WithEvents ButtonUpdateListBoxFiles As Button
    Friend WithEvents CheckBoxSaveAsAssemblyOutputDirectory As CheckBox
    Friend WithEvents CheckBoxSaveAsPartOutputDirectory As CheckBox
    Friend WithEvents CheckBoxSaveAsSheetmetalOutputDirectory As CheckBox
    Friend WithEvents CheckBoxLaserOutputDirectory As CheckBox
    Friend WithEvents CheckBoxSaveAsDraftOutputDirectory As CheckBox
    Friend WithEvents CheckBoxEnablePropertyFilter As CheckBox
    Friend WithEvents GroupBoxTLAOptions As GroupBox
    Friend WithEvents CheckBoxTLAReportUnrelatedFiles As CheckBox
    Friend WithEvents RadioButtonTLATopDown As RadioButton
    Friend WithEvents RadioButtonTLABottomUp As RadioButton
    Friend WithEvents CheckBoxWarnSave As CheckBox
    Friend WithEvents CheckBoxMoveDrawingViewAllowPartialSuccess As CheckBox
    Friend WithEvents CheckBoxRememberTasks As CheckBox
    Friend WithEvents ComboBoxSaveAsAssemblyFileType As ComboBox
    Friend WithEvents ComboBoxSaveAsPartFileType As ComboBox
    Friend WithEvents ComboBoxSaveAsSheetmetalFileType As ComboBox
    Friend WithEvents ComboBoxSaveAsDraftFileType As ComboBox
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents ButtonExternalProgramAssembly As Button
    Friend WithEvents TextBoxExternalProgramAssembly As TextBox
    Friend WithEvents LabelExternalProgramAssembly As Label
    Friend WithEvents ButtonExternalProgramPart As Button
    Friend WithEvents TextBoxExternalProgramPart As TextBox
    Friend WithEvents LabelExternalProgramPart As Label
    Friend WithEvents ButtonExternalProgramSheetmetal As Button
    Friend WithEvents TextBoxExternalProgramSheetmetal As TextBox
    Friend WithEvents LabelExternalProgramSheetmetal As Label
    Friend WithEvents ButtonExternalProgramDraft As Button
    Friend WithEvents TextBoxExternalProgramDraft As TextBox
    Friend WithEvents LabelExternalProgramDraft As Label
    Friend WithEvents ButtonPrintOptions As Button
    Friend WithEvents CheckBoxCreateTODOList As CheckBox
    Friend WithEvents RadioButtonTODOList As RadioButton
    Friend WithEvents CheckBoxFilterDft As CheckBox
    Friend WithEvents CheckBoxFilterPsm As CheckBox
    Friend WithEvents CheckBoxFilterPar As CheckBox
    Friend WithEvents CheckBoxFilterAsm As CheckBox
    Friend WithEvents CheckBoxSaveAsFlatDXFOutputDirectory As CheckBox
    Friend WithEvents ButtonSaveAsFlatDXF As Button
    Friend WithEvents LabelSaveAsFlatDXF As Label
    Friend WithEvents TextBoxSaveAsFlatDXFOutputDirectory As TextBox
    Friend WithEvents TextBoxFileSearch As TextBox
    Friend WithEvents CheckBoxFileSearch As CheckBox
    Friend WithEvents TextBoxFindReplaceReplaceAssembly As TextBox
    Friend WithEvents TextBoxFindReplaceFindAssembly As TextBox
    Friend WithEvents TextBoxFindReplacePropertyNameAssembly As TextBox
    Friend WithEvents ComboBoxFindReplacePropertySetAssembly As ComboBox
    Friend WithEvents LabelFindReplaceReplaceAssembly As Label
    Friend WithEvents LabelFindReplaceFindAssembly As Label
    Friend WithEvents LabelFindReplacePropertyNameAssembly As Label
    Friend WithEvents LabelFindReplacePropertySetAssembly As Label
    Friend WithEvents TextBoxFindReplaceReplacePart As TextBox
    Friend WithEvents TextBoxFindReplaceFindPart As TextBox
    Friend WithEvents TextBoxFindReplacePropertyNamePart As TextBox
    Friend WithEvents ComboBoxFindReplacePropertySetPart As ComboBox
    Friend WithEvents LabelFindReplaceReplacePart As Label
    Friend WithEvents LabelFindReplaceFindPart As Label
    Friend WithEvents LabelFindReplacePropertyNamePart As Label
    Friend WithEvents LabelFindReplacePropertySetPart As Label
    Friend WithEvents TextBoxFindReplaceReplaceSheetmetal As TextBox
    Friend WithEvents TextBoxFindReplaceFindSheetmetal As TextBox
    Friend WithEvents TextBoxFindReplacePropertyNameSheetmetal As TextBox
    Friend WithEvents ComboBoxFindReplacePropertySetSheetmetal As ComboBox
    Friend WithEvents LabelFindReplaceReplaceSheetmetal As Label
    Friend WithEvents LabelFindReplaceFindSheetmetal As Label
    Friend WithEvents LabelFindReplacePropertyNameSheetmetal As Label
    Friend WithEvents LabelFindReplacePropertySetSheetmetal As Label
    Friend WithEvents LabelFontSize As Label
    Friend WithEvents TextBoxFontSize As TextBox
    Friend WithEvents TextBoxFastSearchScopeFilename As TextBox
    Friend WithEvents LabelFastSearchScopeFilename As Label
    Friend WithEvents ButtonFastSearchScopeFilename As Button
End Class
