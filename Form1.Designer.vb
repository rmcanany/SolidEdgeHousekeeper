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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Files sources", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Excluded files", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Assemblies", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Parts", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup5 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Sheetmetals", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup6 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Drafts", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"FakeFile number 1", "C:\Documents\Projects\FakeProject"}, "Unchecked")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"FakeFile number 2", "C:\Documents\Projects\FakeProject"}, "Unchecked")
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"FakeFile number 3", "C:\Documents\Projects\FakeProject"}, "Unchecked")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"FakeFile number 4", "C:\Documents\Projects\FakeProject"}, "Unchecked")
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"FakeFile number 5", "C:\Documents\Projects\FakeProject"}, "Checked")
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"FakeFile number 6", "C:\Documents\Projects\FakeProject"}, "Error")
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"FakeFile number 7", "C:\Documents\Projects\FakeProject\Drawings"}, "Checked")
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"FakeFile number 8", "C:\Documents\Projects\FakeProject\Drawings"}, "Checked")
        Dim ListViewItem9 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("C:\Documents\Projects", "folder")
        Dim ListViewItem10 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("C:\Documents\Projects", "folders")
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGeneral = New System.Windows.Forms.TabPage()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BT_AddFolder = New System.Windows.Forms.ToolStripButton()
        Me.BT_AddFolderSubfolders = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_TopLevelAsm = New System.Windows.Forms.ToolStripButton()
        Me.BT_ASM_Folder = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_AddFromlist = New System.Windows.Forms.ToolStripButton()
        Me.BT_ExportList = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_ErrorList = New System.Windows.Forms.ToolStripButton()
        Me.BT_DeleteAll = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_Update = New System.Windows.Forms.ToolStripButton()
        Me.new_CheckBoxFilterDft = New System.Windows.Forms.ToolStripButton()
        Me.new_CheckBoxFilterPsm = New System.Windows.Forms.ToolStripButton()
        Me.new_CheckBoxFilterPar = New System.Windows.Forms.ToolStripButton()
        Me.new_CheckBoxFilterAsm = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ListViewFiles = New System.Windows.Forms.ListView()
        Me.FileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FilePath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage_ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.ButtonFileSearch = New System.Windows.Forms.Button()
        Me.ComboBoxFileSearch = New System.Windows.Forms.ComboBox()
        Me.LabelFontSize = New System.Windows.Forms.Label()
        Me.TextBoxFontSize = New System.Windows.Forms.TextBox()
        Me.CheckBoxFileSearch = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFilterDft = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFilterPsm = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFilterPar = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFilterAsm = New System.Windows.Forms.CheckBox()
        Me.CheckBoxEnablePropertyFilter = New System.Windows.Forms.CheckBox()
        Me.ButtonPropertyFilter = New System.Windows.Forms.Button()
        Me.LabelListboxFiles = New System.Windows.Forms.Label()
        Me.TabPageAssembly = New System.Windows.Forms.TabPage()
        Me.TextBoxSaveAsFormulaAssembly = New System.Windows.Forms.TextBox()
        Me.CheckBoxSaveAsFormulaAssembly = New System.Windows.Forms.CheckBox()
        Me.TextBoxExposeVariablesAssembly = New System.Windows.Forms.TextBox()
        Me.LabelExposeVariablesAssembly = New System.Windows.Forms.Label()
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
        Me.TextBoxSaveAsFormulaPart = New System.Windows.Forms.TextBox()
        Me.CheckBoxSaveAsFormulaPart = New System.Windows.Forms.CheckBox()
        Me.TextBoxExposeVariablesPart = New System.Windows.Forms.TextBox()
        Me.LabelExposeVariablesPart = New System.Windows.Forms.Label()
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
        Me.TextBoxSaveAsFormulaSheetmetal = New System.Windows.Forms.TextBox()
        Me.CheckBoxSaveAsFormulaSheetmetal = New System.Windows.Forms.CheckBox()
        Me.TextBoxExposeVariablesSheetmetal = New System.Windows.Forms.TextBox()
        Me.LabelExposeVariablesSheetmetal = New System.Windows.Forms.Label()
        Me.TextBoxFindReplaceReplaceSheetmetal = New System.Windows.Forms.TextBox()
        Me.TextBoxFindReplaceFindSheetmetal = New System.Windows.Forms.TextBox()
        Me.TextBoxFindReplacePropertyNameSheetmetal = New System.Windows.Forms.TextBox()
        Me.ComboBoxFindReplacePropertySetSheetmetal = New System.Windows.Forms.ComboBox()
        Me.LabelFindReplaceReplaceSheetmetal = New System.Windows.Forms.Label()
        Me.LabelFindReplaceFindSheetmetal = New System.Windows.Forms.Label()
        Me.LabelFindReplacePropertyNameSheetmetal = New System.Windows.Forms.Label()
        Me.LabelFindReplacePropertySetSheetmetal = New System.Windows.Forms.Label()
        Me.ButtonExternalProgramSheetmetal = New System.Windows.Forms.Button()
        Me.TextBoxExternalProgramSheetmetal = New System.Windows.Forms.TextBox()
        Me.LabelExternalProgramSheetmetal = New System.Windows.Forms.Label()
        Me.ComboBoxSaveAsSheetmetalFileType = New System.Windows.Forms.ComboBox()
        Me.CheckBoxSaveAsSheetmetalOutputDirectory = New System.Windows.Forms.CheckBox()
        Me.ButtonSaveAsSheetmetalOutputDirectory = New System.Windows.Forms.Button()
        Me.TextBoxSaveAsSheetmetalOutputDirectory = New System.Windows.Forms.TextBox()
        Me.LabelSaveAsSheetmetal = New System.Windows.Forms.Label()
        Me.LabelSheetmetalTabNote = New System.Windows.Forms.Label()
        Me.CheckedListBoxSheetmetal = New System.Windows.Forms.CheckedListBox()
        Me.TabPageDraft = New System.Windows.Forms.TabPage()
        Me.TextBoxSaveAsFormulaDraft = New System.Windows.Forms.TextBox()
        Me.CheckBoxSaveAsFormulaDraft = New System.Windows.Forms.CheckBox()
        Me.ButtonWatermark = New System.Windows.Forms.Button()
        Me.TextBoxWatermarkFilename = New System.Windows.Forms.TextBox()
        Me.TextBoxWatermarkScale = New System.Windows.Forms.TextBox()
        Me.LabelWatermarkScale = New System.Windows.Forms.Label()
        Me.TextBoxWatermarkY = New System.Windows.Forms.TextBox()
        Me.LabelWatermarkY = New System.Windows.Forms.Label()
        Me.TextBoxWatermarkX = New System.Windows.Forms.TextBox()
        Me.LabelWatermarkX = New System.Windows.Forms.Label()
        Me.CheckBoxWatermark = New System.Windows.Forms.CheckBox()
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
        Me.CheckBoxSaveAsImageCrop = New System.Windows.Forms.CheckBox()
        Me.CheckBoxBackgroundProcessing = New System.Windows.Forms.CheckBox()
        Me.CheckBoxRunExternalProgramSaveFile = New System.Windows.Forms.CheckBox()
        Me.GroupBoxPictorialViews = New System.Windows.Forms.GroupBox()
        Me.RadioButtonPictorialViewTrimetric = New System.Windows.Forms.RadioButton()
        Me.RadioButtonPictorialViewDimetric = New System.Windows.Forms.RadioButton()
        Me.RadioButtonPictorialViewIsometric = New System.Windows.Forms.RadioButton()
        Me.LabelPrintOptionsCopies = New System.Windows.Forms.Label()
        Me.LabelPrintOptionsHeight = New System.Windows.Forms.Label()
        Me.LabelPrintOptionsWidth = New System.Windows.Forms.Label()
        Me.LabelPrintOptionsPrinter = New System.Windows.Forms.Label()
        Me.CheckBoxPrintOptionsScaleLineWidths = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPrintOptionsScaleLineTypes = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPrintOptionsPrintAsBlack = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPrintOptionsBestFit = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPrintOptionsAutoOrient = New System.Windows.Forms.CheckBox()
        Me.TextBoxPrintOptionsCopies = New System.Windows.Forms.TextBox()
        Me.TextBoxPrintOptionsHeight = New System.Windows.Forms.TextBox()
        Me.TextBoxPrintOptionsWidth = New System.Windows.Forms.TextBox()
        Me.TextBoxPrintOptionsPrinter = New System.Windows.Forms.TextBox()
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.Menu_ListViewFile = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BT_Open = New System.Windows.Forms.ToolStripMenuItem()
        Me.BT_OpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_Remove = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGeneral.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.TabPageAssembly.SuspendLayout()
        Me.TabPagePart.SuspendLayout()
        Me.TabPageSheetmetal.SuspendLayout()
        Me.TabPageDraft.SuspendLayout()
        Me.TabPageConfiguration.SuspendLayout()
        Me.GroupBoxPictorialViews.SuspendLayout()
        Me.GroupBoxTLAOptions.SuspendLayout()
        Me.TabPageReadme.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Menu_ListViewFile.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageGeneral)
        Me.TabControl1.Controls.Add(Me.TabPageAssembly)
        Me.TabControl1.Controls.Add(Me.TabPagePart)
        Me.TabControl1.Controls.Add(Me.TabPageSheetmetal)
        Me.TabControl1.Controls.Add(Me.TabPageDraft)
        Me.TabControl1.Controls.Add(Me.TabPageConfiguration)
        Me.TabControl1.Controls.Add(Me.TabPageReadme)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.TabPage_ImageList
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(519, 621)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageGeneral
        '
        Me.TabPageGeneral.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageGeneral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageGeneral.Controls.Add(Me.ToolStrip1)
        Me.TabPageGeneral.Controls.Add(Me.ListViewFiles)
        Me.TabPageGeneral.Controls.Add(Me.ButtonFileSearch)
        Me.TabPageGeneral.Controls.Add(Me.ComboBoxFileSearch)
        Me.TabPageGeneral.Controls.Add(Me.LabelFontSize)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxFontSize)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxFileSearch)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxFilterDft)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxFilterPsm)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxFilterPar)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxFilterAsm)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxEnablePropertyFilter)
        Me.TabPageGeneral.Controls.Add(Me.ButtonPropertyFilter)
        Me.TabPageGeneral.Controls.Add(Me.LabelListboxFiles)
        Me.TabPageGeneral.ImageKey = "se"
        Me.TabPageGeneral.Location = New System.Drawing.Point(4, 23)
        Me.TabPageGeneral.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageGeneral.Name = "TabPageGeneral"
        Me.TabPageGeneral.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageGeneral.Size = New System.Drawing.Size(511, 594)
        Me.TabPageGeneral.TabIndex = 0
        Me.TabPageGeneral.Text = "General"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.White
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BT_AddFolder, Me.BT_AddFolderSubfolders, Me.ToolStripSeparator4, Me.BT_TopLevelAsm, Me.BT_ASM_Folder, Me.ToolStripSeparator1, Me.BT_AddFromlist, Me.BT_ExportList, Me.ToolStripSeparator2, Me.BT_ErrorList, Me.BT_DeleteAll, Me.ToolStripSeparator3, Me.BT_Update, Me.new_CheckBoxFilterDft, Me.new_CheckBoxFilterPsm, Me.new_CheckBoxFilterPar, Me.new_CheckBoxFilterAsm, Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(2, 2)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(505, 25)
        Me.ToolStrip1.TabIndex = 33
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BT_AddFolder
        '
        Me.BT_AddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_AddFolder.Image = CType(resources.GetObject("BT_AddFolder.Image"), System.Drawing.Image)
        Me.BT_AddFolder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_AddFolder.Name = "BT_AddFolder"
        Me.BT_AddFolder.Size = New System.Drawing.Size(23, 22)
        Me.BT_AddFolder.Text = "Add single folder"
        '
        'BT_AddFolderSubfolders
        '
        Me.BT_AddFolderSubfolders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_AddFolderSubfolders.Image = CType(resources.GetObject("BT_AddFolderSubfolders.Image"), System.Drawing.Image)
        Me.BT_AddFolderSubfolders.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_AddFolderSubfolders.Name = "BT_AddFolderSubfolders"
        Me.BT_AddFolderSubfolders.Size = New System.Drawing.Size(23, 22)
        Me.BT_AddFolderSubfolders.Text = "Add folder and subfolders"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'BT_TopLevelAsm
        '
        Me.BT_TopLevelAsm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_TopLevelAsm.Image = CType(resources.GetObject("BT_TopLevelAsm.Image"), System.Drawing.Image)
        Me.BT_TopLevelAsm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_TopLevelAsm.Name = "BT_TopLevelAsm"
        Me.BT_TopLevelAsm.Size = New System.Drawing.Size(23, 22)
        Me.BT_TopLevelAsm.Text = "Top level asm"
        '
        'BT_ASM_Folder
        '
        Me.BT_ASM_Folder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_ASM_Folder.Image = CType(resources.GetObject("BT_ASM_Folder.Image"), System.Drawing.Image)
        Me.BT_ASM_Folder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_ASM_Folder.Name = "BT_ASM_Folder"
        Me.BT_ASM_Folder.Size = New System.Drawing.Size(23, 22)
        Me.BT_ASM_Folder.Text = "Top level assembly folder"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BT_AddFromlist
        '
        Me.BT_AddFromlist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_AddFromlist.Image = CType(resources.GetObject("BT_AddFromlist.Image"), System.Drawing.Image)
        Me.BT_AddFromlist.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_AddFromlist.Name = "BT_AddFromlist"
        Me.BT_AddFromlist.Size = New System.Drawing.Size(23, 22)
        Me.BT_AddFromlist.Text = "Add files from a list"
        Me.BT_AddFromlist.ToolTipText = "Add files from a list"
        '
        'BT_ExportList
        '
        Me.BT_ExportList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_ExportList.Image = CType(resources.GetObject("BT_ExportList.Image"), System.Drawing.Image)
        Me.BT_ExportList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_ExportList.Name = "BT_ExportList"
        Me.BT_ExportList.Size = New System.Drawing.Size(23, 22)
        Me.BT_ExportList.Text = "Export list"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'BT_ErrorList
        '
        Me.BT_ErrorList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_ErrorList.Image = CType(resources.GetObject("BT_ErrorList.Image"), System.Drawing.Image)
        Me.BT_ErrorList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_ErrorList.Name = "BT_ErrorList"
        Me.BT_ErrorList.Size = New System.Drawing.Size(23, 22)
        Me.BT_ErrorList.Text = "Show only file with errors"
        '
        'BT_DeleteAll
        '
        Me.BT_DeleteAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_DeleteAll.Image = CType(resources.GetObject("BT_DeleteAll.Image"), System.Drawing.Image)
        Me.BT_DeleteAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_DeleteAll.Name = "BT_DeleteAll"
        Me.BT_DeleteAll.Size = New System.Drawing.Size(23, 22)
        Me.BT_DeleteAll.Text = "Delete all"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'BT_Update
        '
        Me.BT_Update.Image = CType(resources.GetObject("BT_Update.Image"), System.Drawing.Image)
        Me.BT_Update.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Update.Name = "BT_Update"
        Me.BT_Update.Size = New System.Drawing.Size(65, 22)
        Me.BT_Update.Text = "Update"
        '
        'new_CheckBoxFilterDft
        '
        Me.new_CheckBoxFilterDft.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.new_CheckBoxFilterDft.Checked = True
        Me.new_CheckBoxFilterDft.CheckOnClick = True
        Me.new_CheckBoxFilterDft.CheckState = System.Windows.Forms.CheckState.Checked
        Me.new_CheckBoxFilterDft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.new_CheckBoxFilterDft.Image = CType(resources.GetObject("new_CheckBoxFilterDft.Image"), System.Drawing.Image)
        Me.new_CheckBoxFilterDft.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.new_CheckBoxFilterDft.Name = "new_CheckBoxFilterDft"
        Me.new_CheckBoxFilterDft.Size = New System.Drawing.Size(23, 22)
        Me.new_CheckBoxFilterDft.Text = "ToolStripButton2"
        '
        'new_CheckBoxFilterPsm
        '
        Me.new_CheckBoxFilterPsm.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.new_CheckBoxFilterPsm.Checked = True
        Me.new_CheckBoxFilterPsm.CheckOnClick = True
        Me.new_CheckBoxFilterPsm.CheckState = System.Windows.Forms.CheckState.Checked
        Me.new_CheckBoxFilterPsm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.new_CheckBoxFilterPsm.Image = CType(resources.GetObject("new_CheckBoxFilterPsm.Image"), System.Drawing.Image)
        Me.new_CheckBoxFilterPsm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.new_CheckBoxFilterPsm.Name = "new_CheckBoxFilterPsm"
        Me.new_CheckBoxFilterPsm.Size = New System.Drawing.Size(23, 22)
        Me.new_CheckBoxFilterPsm.Text = "ToolStripButton3"
        '
        'new_CheckBoxFilterPar
        '
        Me.new_CheckBoxFilterPar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.new_CheckBoxFilterPar.Checked = True
        Me.new_CheckBoxFilterPar.CheckOnClick = True
        Me.new_CheckBoxFilterPar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.new_CheckBoxFilterPar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.new_CheckBoxFilterPar.Image = CType(resources.GetObject("new_CheckBoxFilterPar.Image"), System.Drawing.Image)
        Me.new_CheckBoxFilterPar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.new_CheckBoxFilterPar.Name = "new_CheckBoxFilterPar"
        Me.new_CheckBoxFilterPar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.new_CheckBoxFilterPar.Size = New System.Drawing.Size(23, 22)
        Me.new_CheckBoxFilterPar.Text = "ToolStripButton1"
        '
        'new_CheckBoxFilterAsm
        '
        Me.new_CheckBoxFilterAsm.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.new_CheckBoxFilterAsm.Checked = True
        Me.new_CheckBoxFilterAsm.CheckOnClick = True
        Me.new_CheckBoxFilterAsm.CheckState = System.Windows.Forms.CheckState.Checked
        Me.new_CheckBoxFilterAsm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.new_CheckBoxFilterAsm.Image = CType(resources.GetObject("new_CheckBoxFilterAsm.Image"), System.Drawing.Image)
        Me.new_CheckBoxFilterAsm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.new_CheckBoxFilterAsm.Name = "new_CheckBoxFilterAsm"
        Me.new_CheckBoxFilterAsm.Size = New System.Drawing.Size(23, 22)
        Me.new_CheckBoxFilterAsm.Text = "ToolStripButton4"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(41, 22)
        Me.ToolStripLabel1.Text = "Filters:"
        '
        'ListViewFiles
        '
        Me.ListViewFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListViewFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListViewFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FileName, Me.FilePath})
        Me.ListViewFiles.Cursor = System.Windows.Forms.Cursors.Default
        Me.ListViewFiles.FullRowSelect = True
        Me.ListViewFiles.GridLines = True
        ListViewGroup1.Header = "Files sources"
        ListViewGroup1.Name = "Sources"
        ListViewGroup2.Header = "Excluded files"
        ListViewGroup2.Name = "Excluded"
        ListViewGroup3.Header = "Assemblies"
        ListViewGroup3.Name = ".asm"
        ListViewGroup4.Header = "Parts"
        ListViewGroup4.Name = ".par"
        ListViewGroup5.Header = "Sheetmetals"
        ListViewGroup5.Name = ".psm"
        ListViewGroup6.Header = "Drafts"
        ListViewGroup6.Name = ".dft"
        Me.ListViewFiles.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3, ListViewGroup4, ListViewGroup5, ListViewGroup6})
        Me.ListViewFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListViewFiles.HideSelection = False
        ListViewItem1.Group = ListViewGroup3
        ListViewItem2.Group = ListViewGroup4
        ListViewItem3.Group = ListViewGroup4
        ListViewItem4.Group = ListViewGroup4
        ListViewItem5.Group = ListViewGroup5
        ListViewItem6.Group = ListViewGroup5
        ListViewItem7.Group = ListViewGroup6
        ListViewItem8.Group = ListViewGroup6
        ListViewItem9.Group = ListViewGroup1
        ListViewItem10.Group = ListViewGroup1
        Me.ListViewFiles.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9, ListViewItem10})
        Me.ListViewFiles.Location = New System.Drawing.Point(2, 30)
        Me.ListViewFiles.Name = "ListViewFiles"
        Me.ListViewFiles.Size = New System.Drawing.Size(505, 467)
        Me.ListViewFiles.SmallImageList = Me.TabPage_ImageList
        Me.ListViewFiles.TabIndex = 32
        Me.ListViewFiles.UseCompatibleStateImageBehavior = False
        Me.ListViewFiles.View = System.Windows.Forms.View.Details
        '
        'FileName
        '
        Me.FileName.Text = "Name"
        Me.FileName.Width = 150
        '
        'FilePath
        '
        Me.FilePath.Text = "Path"
        Me.FilePath.Width = 300
        '
        'TabPage_ImageList
        '
        Me.TabPage_ImageList.ImageStream = CType(resources.GetObject("TabPage_ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TabPage_ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TabPage_ImageList.Images.SetKeyName(0, "se")
        Me.TabPage_ImageList.Images.SetKeyName(1, "asm")
        Me.TabPage_ImageList.Images.SetKeyName(2, "cfg")
        Me.TabPage_ImageList.Images.SetKeyName(3, "dft")
        Me.TabPage_ImageList.Images.SetKeyName(4, "par")
        Me.TabPage_ImageList.Images.SetKeyName(5, "psm")
        Me.TabPage_ImageList.Images.SetKeyName(6, "Checked")
        Me.TabPage_ImageList.Images.SetKeyName(7, "Unchecked")
        Me.TabPage_ImageList.Images.SetKeyName(8, "config")
        Me.TabPage_ImageList.Images.SetKeyName(9, "Help")
        Me.TabPage_ImageList.Images.SetKeyName(10, "Info")
        Me.TabPage_ImageList.Images.SetKeyName(11, "Error")
        Me.TabPage_ImageList.Images.SetKeyName(12, "txt")
        Me.TabPage_ImageList.Images.SetKeyName(13, "csv")
        Me.TabPage_ImageList.Images.SetKeyName(14, "excel")
        Me.TabPage_ImageList.Images.SetKeyName(15, "folder")
        Me.TabPage_ImageList.Images.SetKeyName(16, "folders")
        Me.TabPage_ImageList.Images.SetKeyName(17, "ASM_Folder")
        '
        'ButtonFileSearch
        '
        Me.ButtonFileSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonFileSearch.Location = New System.Drawing.Point(217, 534)
        Me.ButtonFileSearch.Name = "ButtonFileSearch"
        Me.ButtonFileSearch.Size = New System.Drawing.Size(75, 20)
        Me.ButtonFileSearch.TabIndex = 31
        Me.ButtonFileSearch.Text = "Delete"
        Me.ButtonFileSearch.UseVisualStyleBackColor = True
        '
        'ComboBoxFileSearch
        '
        Me.ComboBoxFileSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxFileSearch.FormattingEnabled = True
        Me.ComboBoxFileSearch.Location = New System.Drawing.Point(84, 534)
        Me.ComboBoxFileSearch.Name = "ComboBoxFileSearch"
        Me.ComboBoxFileSearch.Size = New System.Drawing.Size(125, 21)
        Me.ComboBoxFileSearch.Sorted = True
        Me.ComboBoxFileSearch.TabIndex = 30
        '
        'LabelFontSize
        '
        Me.LabelFontSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFontSize.AutoSize = True
        Me.LabelFontSize.Location = New System.Drawing.Point(63, 505)
        Me.LabelFontSize.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFontSize.Name = "LabelFontSize"
        Me.LabelFontSize.Size = New System.Drawing.Size(49, 13)
        Me.LabelFontSize.TabIndex = 29
        Me.LabelFontSize.Text = "Font size"
        '
        'TextBoxFontSize
        '
        Me.TextBoxFontSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFontSize.Location = New System.Drawing.Point(2, 502)
        Me.TextBoxFontSize.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFontSize.Name = "TextBoxFontSize"
        Me.TextBoxFontSize.Size = New System.Drawing.Size(57, 20)
        Me.TextBoxFontSize.TabIndex = 28
        Me.TextBoxFontSize.Text = "8"
        '
        'CheckBoxFileSearch
        '
        Me.CheckBoxFileSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFileSearch.AutoSize = True
        Me.CheckBoxFileSearch.Location = New System.Drawing.Point(2, 534)
        Me.CheckBoxFileSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxFileSearch.Name = "CheckBoxFileSearch"
        Me.CheckBoxFileSearch.Size = New System.Drawing.Size(77, 17)
        Me.CheckBoxFileSearch.TabIndex = 26
        Me.CheckBoxFileSearch.Text = "File search"
        Me.CheckBoxFileSearch.UseVisualStyleBackColor = True
        '
        'CheckBoxFilterDft
        '
        Me.CheckBoxFilterDft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFilterDft.AutoSize = True
        Me.CheckBoxFilterDft.Checked = True
        Me.CheckBoxFilterDft.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxFilterDft.Location = New System.Drawing.Point(296, 382)
        Me.CheckBoxFilterDft.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxFilterDft.Name = "CheckBoxFilterDft"
        Me.CheckBoxFilterDft.Size = New System.Drawing.Size(45, 17)
        Me.CheckBoxFilterDft.TabIndex = 25
        Me.CheckBoxFilterDft.Text = "*.dft"
        Me.CheckBoxFilterDft.UseVisualStyleBackColor = True
        '
        'CheckBoxFilterPsm
        '
        Me.CheckBoxFilterPsm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFilterPsm.AutoSize = True
        Me.CheckBoxFilterPsm.Checked = True
        Me.CheckBoxFilterPsm.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxFilterPsm.Location = New System.Drawing.Point(240, 382)
        Me.CheckBoxFilterPsm.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxFilterPsm.Name = "CheckBoxFilterPsm"
        Me.CheckBoxFilterPsm.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxFilterPsm.TabIndex = 24
        Me.CheckBoxFilterPsm.Text = "*.psm"
        Me.CheckBoxFilterPsm.UseVisualStyleBackColor = True
        '
        'CheckBoxFilterPar
        '
        Me.CheckBoxFilterPar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFilterPar.AutoSize = True
        Me.CheckBoxFilterPar.Checked = True
        Me.CheckBoxFilterPar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxFilterPar.Location = New System.Drawing.Point(184, 382)
        Me.CheckBoxFilterPar.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxFilterPar.Name = "CheckBoxFilterPar"
        Me.CheckBoxFilterPar.Size = New System.Drawing.Size(48, 17)
        Me.CheckBoxFilterPar.TabIndex = 23
        Me.CheckBoxFilterPar.Text = "*.par"
        Me.CheckBoxFilterPar.UseVisualStyleBackColor = True
        '
        'CheckBoxFilterAsm
        '
        Me.CheckBoxFilterAsm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFilterAsm.AutoSize = True
        Me.CheckBoxFilterAsm.Checked = True
        Me.CheckBoxFilterAsm.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxFilterAsm.Location = New System.Drawing.Point(127, 382)
        Me.CheckBoxFilterAsm.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxFilterAsm.Name = "CheckBoxFilterAsm"
        Me.CheckBoxFilterAsm.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxFilterAsm.TabIndex = 22
        Me.CheckBoxFilterAsm.Text = "*.asm"
        Me.CheckBoxFilterAsm.UseVisualStyleBackColor = True
        '
        'CheckBoxEnablePropertyFilter
        '
        Me.CheckBoxEnablePropertyFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxEnablePropertyFilter.AutoSize = True
        Me.CheckBoxEnablePropertyFilter.Location = New System.Drawing.Point(2, 567)
        Me.CheckBoxEnablePropertyFilter.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxEnablePropertyFilter.Name = "CheckBoxEnablePropertyFilter"
        Me.CheckBoxEnablePropertyFilter.Size = New System.Drawing.Size(122, 17)
        Me.CheckBoxEnablePropertyFilter.TabIndex = 20
        Me.CheckBoxEnablePropertyFilter.Text = "Enable property filter"
        Me.CheckBoxEnablePropertyFilter.UseVisualStyleBackColor = True
        '
        'ButtonPropertyFilter
        '
        Me.ButtonPropertyFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonPropertyFilter.Enabled = False
        Me.ButtonPropertyFilter.Location = New System.Drawing.Point(125, 560)
        Me.ButtonPropertyFilter.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonPropertyFilter.Name = "ButtonPropertyFilter"
        Me.ButtonPropertyFilter.Size = New System.Drawing.Size(75, 28)
        Me.ButtonPropertyFilter.TabIndex = 18
        Me.ButtonPropertyFilter.Text = "Configure"
        Me.ButtonPropertyFilter.UseVisualStyleBackColor = True
        '
        'LabelListboxFiles
        '
        Me.LabelListboxFiles.AutoSize = True
        Me.LabelListboxFiles.Location = New System.Drawing.Point(126, 505)
        Me.LabelListboxFiles.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelListboxFiles.Name = "LabelListboxFiles"
        Me.LabelListboxFiles.Size = New System.Drawing.Size(381, 13)
        Me.LabelListboxFiles.TabIndex = 12
        Me.LabelListboxFiles.Text = "Select file(s) to process OR Select none to process all (ESC to clean selections)" &
    ""
        '
        'TabPageAssembly
        '
        Me.TabPageAssembly.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageAssembly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageAssembly.Controls.Add(Me.TextBoxSaveAsFormulaAssembly)
        Me.TabPageAssembly.Controls.Add(Me.CheckBoxSaveAsFormulaAssembly)
        Me.TabPageAssembly.Controls.Add(Me.TextBoxExposeVariablesAssembly)
        Me.TabPageAssembly.Controls.Add(Me.LabelExposeVariablesAssembly)
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
        Me.TabPageAssembly.ImageKey = "asm"
        Me.TabPageAssembly.Location = New System.Drawing.Point(4, 23)
        Me.TabPageAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageAssembly.Name = "TabPageAssembly"
        Me.TabPageAssembly.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageAssembly.Size = New System.Drawing.Size(511, 594)
        Me.TabPageAssembly.TabIndex = 1
        Me.TabPageAssembly.Text = "Assembly"
        '
        'TextBoxSaveAsFormulaAssembly
        '
        Me.TextBoxSaveAsFormulaAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsFormulaAssembly.Location = New System.Drawing.Point(8, 396)
        Me.TextBoxSaveAsFormulaAssembly.Name = "TextBoxSaveAsFormulaAssembly"
        Me.TextBoxSaveAsFormulaAssembly.Size = New System.Drawing.Size(457, 20)
        Me.TextBoxSaveAsFormulaAssembly.TabIndex = 21
        '
        'CheckBoxSaveAsFormulaAssembly
        '
        Me.CheckBoxSaveAsFormulaAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsFormulaAssembly.AutoSize = True
        Me.CheckBoxSaveAsFormulaAssembly.Location = New System.Drawing.Point(8, 376)
        Me.CheckBoxSaveAsFormulaAssembly.Name = "CheckBoxSaveAsFormulaAssembly"
        Me.CheckBoxSaveAsFormulaAssembly.Size = New System.Drawing.Size(142, 17)
        Me.CheckBoxSaveAsFormulaAssembly.TabIndex = 20
        Me.CheckBoxSaveAsFormulaAssembly.Text = "Use subdirectory formula"
        Me.CheckBoxSaveAsFormulaAssembly.UseVisualStyleBackColor = True
        '
        'TextBoxExposeVariablesAssembly
        '
        Me.TextBoxExposeVariablesAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExposeVariablesAssembly.Location = New System.Drawing.Point(8, 546)
        Me.TextBoxExposeVariablesAssembly.Name = "TextBoxExposeVariablesAssembly"
        Me.TextBoxExposeVariablesAssembly.Size = New System.Drawing.Size(457, 20)
        Me.TextBoxExposeVariablesAssembly.TabIndex = 19
        '
        'LabelExposeVariablesAssembly
        '
        Me.LabelExposeVariablesAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExposeVariablesAssembly.AutoSize = True
        Me.LabelExposeVariablesAssembly.Location = New System.Drawing.Point(8, 526)
        Me.LabelExposeVariablesAssembly.Name = "LabelExposeVariablesAssembly"
        Me.LabelExposeVariablesAssembly.Size = New System.Drawing.Size(99, 13)
        Me.LabelExposeVariablesAssembly.TabIndex = 18
        Me.LabelExposeVariablesAssembly.Text = "Variables to expose"
        '
        'TextBoxFindReplaceReplaceAssembly
        '
        Me.TextBoxFindReplaceReplaceAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceReplaceAssembly.Location = New System.Drawing.Point(350, 496)
        Me.TextBoxFindReplaceReplaceAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceReplaceAssembly.Name = "TextBoxFindReplaceReplaceAssembly"
        Me.TextBoxFindReplaceReplaceAssembly.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFindReplaceReplaceAssembly.TabIndex = 17
        '
        'TextBoxFindReplaceFindAssembly
        '
        Me.TextBoxFindReplaceFindAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceFindAssembly.Location = New System.Drawing.Point(235, 496)
        Me.TextBoxFindReplaceFindAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceFindAssembly.Name = "TextBoxFindReplaceFindAssembly"
        Me.TextBoxFindReplaceFindAssembly.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFindReplaceFindAssembly.TabIndex = 16
        '
        'TextBoxFindReplacePropertyNameAssembly
        '
        Me.TextBoxFindReplacePropertyNameAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplacePropertyNameAssembly.Location = New System.Drawing.Point(120, 496)
        Me.TextBoxFindReplacePropertyNameAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplacePropertyNameAssembly.Name = "TextBoxFindReplacePropertyNameAssembly"
        Me.TextBoxFindReplacePropertyNameAssembly.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFindReplacePropertyNameAssembly.TabIndex = 15
        '
        'ComboBoxFindReplacePropertySetAssembly
        '
        Me.ComboBoxFindReplacePropertySetAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxFindReplacePropertySetAssembly.FormattingEnabled = True
        Me.ComboBoxFindReplacePropertySetAssembly.Location = New System.Drawing.Point(8, 496)
        Me.ComboBoxFindReplacePropertySetAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxFindReplacePropertySetAssembly.Name = "ComboBoxFindReplacePropertySetAssembly"
        Me.ComboBoxFindReplacePropertySetAssembly.Size = New System.Drawing.Size(95, 21)
        Me.ComboBoxFindReplacePropertySetAssembly.TabIndex = 14
        '
        'LabelFindReplaceReplaceAssembly
        '
        Me.LabelFindReplaceReplaceAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceReplaceAssembly.AutoSize = True
        Me.LabelFindReplaceReplaceAssembly.Location = New System.Drawing.Point(350, 476)
        Me.LabelFindReplaceReplaceAssembly.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceReplaceAssembly.Name = "LabelFindReplaceReplaceAssembly"
        Me.LabelFindReplaceReplaceAssembly.Size = New System.Drawing.Size(47, 13)
        Me.LabelFindReplaceReplaceAssembly.TabIndex = 13
        Me.LabelFindReplaceReplaceAssembly.Text = "Replace"
        '
        'LabelFindReplaceFindAssembly
        '
        Me.LabelFindReplaceFindAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceFindAssembly.AutoSize = True
        Me.LabelFindReplaceFindAssembly.Location = New System.Drawing.Point(235, 476)
        Me.LabelFindReplaceFindAssembly.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceFindAssembly.Name = "LabelFindReplaceFindAssembly"
        Me.LabelFindReplaceFindAssembly.Size = New System.Drawing.Size(27, 13)
        Me.LabelFindReplaceFindAssembly.TabIndex = 12
        Me.LabelFindReplaceFindAssembly.Text = "Find"
        '
        'LabelFindReplacePropertyNameAssembly
        '
        Me.LabelFindReplacePropertyNameAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertyNameAssembly.AutoSize = True
        Me.LabelFindReplacePropertyNameAssembly.Location = New System.Drawing.Point(120, 476)
        Me.LabelFindReplacePropertyNameAssembly.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertyNameAssembly.Name = "LabelFindReplacePropertyNameAssembly"
        Me.LabelFindReplacePropertyNameAssembly.Size = New System.Drawing.Size(75, 13)
        Me.LabelFindReplacePropertyNameAssembly.TabIndex = 11
        Me.LabelFindReplacePropertyNameAssembly.Text = "Property name"
        '
        'LabelFindReplacePropertySetAssembly
        '
        Me.LabelFindReplacePropertySetAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertySetAssembly.AutoSize = True
        Me.LabelFindReplacePropertySetAssembly.Location = New System.Drawing.Point(8, 476)
        Me.LabelFindReplacePropertySetAssembly.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertySetAssembly.Name = "LabelFindReplacePropertySetAssembly"
        Me.LabelFindReplacePropertySetAssembly.Size = New System.Drawing.Size(65, 13)
        Me.LabelFindReplacePropertySetAssembly.TabIndex = 10
        Me.LabelFindReplacePropertySetAssembly.Text = "Property Set"
        '
        'ButtonExternalProgramAssembly
        '
        Me.ButtonExternalProgramAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalProgramAssembly.Location = New System.Drawing.Point(389, 446)
        Me.ButtonExternalProgramAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonExternalProgramAssembly.Name = "ButtonExternalProgramAssembly"
        Me.ButtonExternalProgramAssembly.Size = New System.Drawing.Size(75, 19)
        Me.ButtonExternalProgramAssembly.TabIndex = 9
        Me.ButtonExternalProgramAssembly.Text = "Browse"
        Me.ButtonExternalProgramAssembly.UseVisualStyleBackColor = True
        '
        'TextBoxExternalProgramAssembly
        '
        Me.TextBoxExternalProgramAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalProgramAssembly.Location = New System.Drawing.Point(8, 446)
        Me.TextBoxExternalProgramAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxExternalProgramAssembly.Name = "TextBoxExternalProgramAssembly"
        Me.TextBoxExternalProgramAssembly.Size = New System.Drawing.Size(375, 20)
        Me.TextBoxExternalProgramAssembly.TabIndex = 8
        '
        'LabelExternalProgramAssembly
        '
        Me.LabelExternalProgramAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExternalProgramAssembly.AutoSize = True
        Me.LabelExternalProgramAssembly.Location = New System.Drawing.Point(8, 426)
        Me.LabelExternalProgramAssembly.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelExternalProgramAssembly.Name = "LabelExternalProgramAssembly"
        Me.LabelExternalProgramAssembly.Size = New System.Drawing.Size(86, 13)
        Me.LabelExternalProgramAssembly.TabIndex = 7
        Me.LabelExternalProgramAssembly.Text = "External program"
        '
        'ComboBoxSaveAsAssemblyFileType
        '
        Me.ComboBoxSaveAsAssemblyFileType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxSaveAsAssemblyFileType.FormattingEnabled = True
        Me.ComboBoxSaveAsAssemblyFileType.Items.AddRange(New Object() {"fake_choice_1", "fake_choice_2"})
        Me.ComboBoxSaveAsAssemblyFileType.Location = New System.Drawing.Point(150, 322)
        Me.ComboBoxSaveAsAssemblyFileType.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxSaveAsAssemblyFileType.Name = "ComboBoxSaveAsAssemblyFileType"
        Me.ComboBoxSaveAsAssemblyFileType.Size = New System.Drawing.Size(132, 21)
        Me.ComboBoxSaveAsAssemblyFileType.Sorted = True
        Me.ComboBoxSaveAsAssemblyFileType.TabIndex = 6
        '
        'CheckBoxSaveAsAssemblyOutputDirectory
        '
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsAssemblyOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Location = New System.Drawing.Point(319, 326)
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Name = "CheckBoxSaveAsAssemblyOutputDirectory"
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Size = New System.Drawing.Size(146, 17)
        Me.CheckBoxSaveAsAssemblyOutputDirectory.TabIndex = 5
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsAssemblyOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsAssemblyOutputDirectory
        '
        Me.ButtonSaveAsAssemblyOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsAssemblyOutputDirectory.Location = New System.Drawing.Point(389, 346)
        Me.ButtonSaveAsAssemblyOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSaveAsAssemblyOutputDirectory.Name = "ButtonSaveAsAssemblyOutputDirectory"
        Me.ButtonSaveAsAssemblyOutputDirectory.Size = New System.Drawing.Size(75, 20)
        Me.ButtonSaveAsAssemblyOutputDirectory.TabIndex = 4
        Me.ButtonSaveAsAssemblyOutputDirectory.Text = "Browse"
        Me.ButtonSaveAsAssemblyOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsAssemblyOutputDirectory
        '
        Me.TextBoxSaveAsAssemblyOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsAssemblyOutputDirectory.Location = New System.Drawing.Point(8, 346)
        Me.TextBoxSaveAsAssemblyOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxSaveAsAssemblyOutputDirectory.Name = "TextBoxSaveAsAssemblyOutputDirectory"
        Me.TextBoxSaveAsAssemblyOutputDirectory.Size = New System.Drawing.Size(376, 20)
        Me.TextBoxSaveAsAssemblyOutputDirectory.TabIndex = 3
        '
        'LabelSaveAsAssembly
        '
        Me.LabelSaveAsAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelSaveAsAssembly.AutoSize = True
        Me.LabelSaveAsAssembly.Location = New System.Drawing.Point(8, 326)
        Me.LabelSaveAsAssembly.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelSaveAsAssembly.Name = "LabelSaveAsAssembly"
        Me.LabelSaveAsAssembly.Size = New System.Drawing.Size(123, 13)
        Me.LabelSaveAsAssembly.TabIndex = 2
        Me.LabelSaveAsAssembly.Text = "Save As output directory"
        '
        'LabelAssemblyTabNote
        '
        Me.LabelAssemblyTabNote.AutoSize = True
        Me.LabelAssemblyTabNote.Location = New System.Drawing.Point(19, 8)
        Me.LabelAssemblyTabNote.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelAssemblyTabNote.Name = "LabelAssemblyTabNote"
        Me.LabelAssemblyTabNote.Size = New System.Drawing.Size(315, 13)
        Me.LabelAssemblyTabNote.TabIndex = 1
        Me.LabelAssemblyTabNote.Text = "Double-click anywhere in the checkbox control to toggle all/none"
        '
        'CheckedListBoxAssembly
        '
        Me.CheckedListBoxAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckedListBoxAssembly.BackColor = System.Drawing.SystemColors.Control
        Me.CheckedListBoxAssembly.CheckOnClick = True
        Me.CheckedListBoxAssembly.FormattingEnabled = True
        Me.CheckedListBoxAssembly.Items.AddRange(New Object() {"Fake name 1.  Real checkboxes populated at run time.", "Fake name 2", "Fake name 3"})
        Me.CheckedListBoxAssembly.Location = New System.Drawing.Point(19, 28)
        Me.CheckedListBoxAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckedListBoxAssembly.Name = "CheckedListBoxAssembly"
        Me.CheckedListBoxAssembly.Size = New System.Drawing.Size(451, 274)
        Me.CheckedListBoxAssembly.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxAssembly, "Double-click to check/uncheck all")
        '
        'TabPagePart
        '
        Me.TabPagePart.BackColor = System.Drawing.SystemColors.Control
        Me.TabPagePart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPagePart.Controls.Add(Me.TextBoxSaveAsFormulaPart)
        Me.TabPagePart.Controls.Add(Me.CheckBoxSaveAsFormulaPart)
        Me.TabPagePart.Controls.Add(Me.TextBoxExposeVariablesPart)
        Me.TabPagePart.Controls.Add(Me.LabelExposeVariablesPart)
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
        Me.TabPagePart.ImageKey = "par"
        Me.TabPagePart.Location = New System.Drawing.Point(4, 23)
        Me.TabPagePart.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPagePart.Name = "TabPagePart"
        Me.TabPagePart.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPagePart.Size = New System.Drawing.Size(511, 594)
        Me.TabPagePart.TabIndex = 2
        Me.TabPagePart.Text = "Part"
        '
        'TextBoxSaveAsFormulaPart
        '
        Me.TextBoxSaveAsFormulaPart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsFormulaPart.Location = New System.Drawing.Point(8, 396)
        Me.TextBoxSaveAsFormulaPart.Name = "TextBoxSaveAsFormulaPart"
        Me.TextBoxSaveAsFormulaPart.Size = New System.Drawing.Size(457, 20)
        Me.TextBoxSaveAsFormulaPart.TabIndex = 29
        '
        'CheckBoxSaveAsFormulaPart
        '
        Me.CheckBoxSaveAsFormulaPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsFormulaPart.AutoSize = True
        Me.CheckBoxSaveAsFormulaPart.Location = New System.Drawing.Point(8, 376)
        Me.CheckBoxSaveAsFormulaPart.Name = "CheckBoxSaveAsFormulaPart"
        Me.CheckBoxSaveAsFormulaPart.Size = New System.Drawing.Size(142, 17)
        Me.CheckBoxSaveAsFormulaPart.TabIndex = 28
        Me.CheckBoxSaveAsFormulaPart.Text = "Use subdirectory formula"
        Me.CheckBoxSaveAsFormulaPart.UseVisualStyleBackColor = True
        '
        'TextBoxExposeVariablesPart
        '
        Me.TextBoxExposeVariablesPart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExposeVariablesPart.Location = New System.Drawing.Point(8, 546)
        Me.TextBoxExposeVariablesPart.Name = "TextBoxExposeVariablesPart"
        Me.TextBoxExposeVariablesPart.Size = New System.Drawing.Size(457, 20)
        Me.TextBoxExposeVariablesPart.TabIndex = 27
        '
        'LabelExposeVariablesPart
        '
        Me.LabelExposeVariablesPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExposeVariablesPart.AutoSize = True
        Me.LabelExposeVariablesPart.Location = New System.Drawing.Point(8, 526)
        Me.LabelExposeVariablesPart.Name = "LabelExposeVariablesPart"
        Me.LabelExposeVariablesPart.Size = New System.Drawing.Size(99, 13)
        Me.LabelExposeVariablesPart.TabIndex = 26
        Me.LabelExposeVariablesPart.Text = "Variables to expose"
        '
        'TextBoxFindReplaceReplacePart
        '
        Me.TextBoxFindReplaceReplacePart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceReplacePart.Location = New System.Drawing.Point(350, 496)
        Me.TextBoxFindReplaceReplacePart.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceReplacePart.Name = "TextBoxFindReplaceReplacePart"
        Me.TextBoxFindReplaceReplacePart.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFindReplaceReplacePart.TabIndex = 25
        '
        'TextBoxFindReplaceFindPart
        '
        Me.TextBoxFindReplaceFindPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceFindPart.Location = New System.Drawing.Point(235, 496)
        Me.TextBoxFindReplaceFindPart.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceFindPart.Name = "TextBoxFindReplaceFindPart"
        Me.TextBoxFindReplaceFindPart.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFindReplaceFindPart.TabIndex = 24
        '
        'TextBoxFindReplacePropertyNamePart
        '
        Me.TextBoxFindReplacePropertyNamePart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplacePropertyNamePart.Location = New System.Drawing.Point(120, 496)
        Me.TextBoxFindReplacePropertyNamePart.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplacePropertyNamePart.Name = "TextBoxFindReplacePropertyNamePart"
        Me.TextBoxFindReplacePropertyNamePart.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFindReplacePropertyNamePart.TabIndex = 23
        '
        'ComboBoxFindReplacePropertySetPart
        '
        Me.ComboBoxFindReplacePropertySetPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxFindReplacePropertySetPart.FormattingEnabled = True
        Me.ComboBoxFindReplacePropertySetPart.Location = New System.Drawing.Point(8, 496)
        Me.ComboBoxFindReplacePropertySetPart.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxFindReplacePropertySetPart.Name = "ComboBoxFindReplacePropertySetPart"
        Me.ComboBoxFindReplacePropertySetPart.Size = New System.Drawing.Size(95, 21)
        Me.ComboBoxFindReplacePropertySetPart.TabIndex = 22
        '
        'LabelFindReplaceReplacePart
        '
        Me.LabelFindReplaceReplacePart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceReplacePart.AutoSize = True
        Me.LabelFindReplaceReplacePart.Location = New System.Drawing.Point(350, 476)
        Me.LabelFindReplaceReplacePart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceReplacePart.Name = "LabelFindReplaceReplacePart"
        Me.LabelFindReplaceReplacePart.Size = New System.Drawing.Size(47, 13)
        Me.LabelFindReplaceReplacePart.TabIndex = 21
        Me.LabelFindReplaceReplacePart.Text = "Replace"
        '
        'LabelFindReplaceFindPart
        '
        Me.LabelFindReplaceFindPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceFindPart.AutoSize = True
        Me.LabelFindReplaceFindPart.Location = New System.Drawing.Point(235, 476)
        Me.LabelFindReplaceFindPart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceFindPart.Name = "LabelFindReplaceFindPart"
        Me.LabelFindReplaceFindPart.Size = New System.Drawing.Size(27, 13)
        Me.LabelFindReplaceFindPart.TabIndex = 20
        Me.LabelFindReplaceFindPart.Text = "Find"
        '
        'LabelFindReplacePropertyNamePart
        '
        Me.LabelFindReplacePropertyNamePart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertyNamePart.AutoSize = True
        Me.LabelFindReplacePropertyNamePart.Location = New System.Drawing.Point(120, 476)
        Me.LabelFindReplacePropertyNamePart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertyNamePart.Name = "LabelFindReplacePropertyNamePart"
        Me.LabelFindReplacePropertyNamePart.Size = New System.Drawing.Size(75, 13)
        Me.LabelFindReplacePropertyNamePart.TabIndex = 19
        Me.LabelFindReplacePropertyNamePart.Text = "Property name"
        '
        'LabelFindReplacePropertySetPart
        '
        Me.LabelFindReplacePropertySetPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertySetPart.AutoSize = True
        Me.LabelFindReplacePropertySetPart.Location = New System.Drawing.Point(8, 476)
        Me.LabelFindReplacePropertySetPart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertySetPart.Name = "LabelFindReplacePropertySetPart"
        Me.LabelFindReplacePropertySetPart.Size = New System.Drawing.Size(65, 13)
        Me.LabelFindReplacePropertySetPart.TabIndex = 18
        Me.LabelFindReplacePropertySetPart.Text = "Property Set"
        '
        'ButtonExternalProgramPart
        '
        Me.ButtonExternalProgramPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalProgramPart.Location = New System.Drawing.Point(389, 446)
        Me.ButtonExternalProgramPart.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonExternalProgramPart.Name = "ButtonExternalProgramPart"
        Me.ButtonExternalProgramPart.Size = New System.Drawing.Size(75, 19)
        Me.ButtonExternalProgramPart.TabIndex = 10
        Me.ButtonExternalProgramPart.Text = "Browse"
        Me.ButtonExternalProgramPart.UseVisualStyleBackColor = True
        '
        'TextBoxExternalProgramPart
        '
        Me.TextBoxExternalProgramPart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalProgramPart.Location = New System.Drawing.Point(8, 446)
        Me.TextBoxExternalProgramPart.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxExternalProgramPart.Name = "TextBoxExternalProgramPart"
        Me.TextBoxExternalProgramPart.Size = New System.Drawing.Size(375, 20)
        Me.TextBoxExternalProgramPart.TabIndex = 9
        '
        'LabelExternalProgramPart
        '
        Me.LabelExternalProgramPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExternalProgramPart.AutoSize = True
        Me.LabelExternalProgramPart.Location = New System.Drawing.Point(8, 426)
        Me.LabelExternalProgramPart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelExternalProgramPart.Name = "LabelExternalProgramPart"
        Me.LabelExternalProgramPart.Size = New System.Drawing.Size(86, 13)
        Me.LabelExternalProgramPart.TabIndex = 8
        Me.LabelExternalProgramPart.Text = "External program"
        '
        'ComboBoxSaveAsPartFileType
        '
        Me.ComboBoxSaveAsPartFileType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxSaveAsPartFileType.FormattingEnabled = True
        Me.ComboBoxSaveAsPartFileType.Items.AddRange(New Object() {"fake_item_1"})
        Me.ComboBoxSaveAsPartFileType.Location = New System.Drawing.Point(150, 322)
        Me.ComboBoxSaveAsPartFileType.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxSaveAsPartFileType.Name = "ComboBoxSaveAsPartFileType"
        Me.ComboBoxSaveAsPartFileType.Size = New System.Drawing.Size(132, 21)
        Me.ComboBoxSaveAsPartFileType.Sorted = True
        Me.ComboBoxSaveAsPartFileType.TabIndex = 7
        '
        'CheckBoxSaveAsPartOutputDirectory
        '
        Me.CheckBoxSaveAsPartOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsPartOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsPartOutputDirectory.Location = New System.Drawing.Point(319, 326)
        Me.CheckBoxSaveAsPartOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxSaveAsPartOutputDirectory.Name = "CheckBoxSaveAsPartOutputDirectory"
        Me.CheckBoxSaveAsPartOutputDirectory.Size = New System.Drawing.Size(146, 17)
        Me.CheckBoxSaveAsPartOutputDirectory.TabIndex = 6
        Me.CheckBoxSaveAsPartOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsPartOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsPartOutputDirectory
        '
        Me.ButtonSaveAsPartOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsPartOutputDirectory.Location = New System.Drawing.Point(389, 347)
        Me.ButtonSaveAsPartOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSaveAsPartOutputDirectory.Name = "ButtonSaveAsPartOutputDirectory"
        Me.ButtonSaveAsPartOutputDirectory.Size = New System.Drawing.Size(75, 20)
        Me.ButtonSaveAsPartOutputDirectory.TabIndex = 5
        Me.ButtonSaveAsPartOutputDirectory.Text = "Browse"
        Me.ButtonSaveAsPartOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsPartOutputDirectory
        '
        Me.TextBoxSaveAsPartOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsPartOutputDirectory.Location = New System.Drawing.Point(8, 347)
        Me.TextBoxSaveAsPartOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxSaveAsPartOutputDirectory.Name = "TextBoxSaveAsPartOutputDirectory"
        Me.TextBoxSaveAsPartOutputDirectory.Size = New System.Drawing.Size(376, 20)
        Me.TextBoxSaveAsPartOutputDirectory.TabIndex = 4
        '
        'LabelSaveAsPart
        '
        Me.LabelSaveAsPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelSaveAsPart.AutoSize = True
        Me.LabelSaveAsPart.Location = New System.Drawing.Point(8, 326)
        Me.LabelSaveAsPart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelSaveAsPart.Name = "LabelSaveAsPart"
        Me.LabelSaveAsPart.Size = New System.Drawing.Size(123, 13)
        Me.LabelSaveAsPart.TabIndex = 3
        Me.LabelSaveAsPart.Text = "Save As output directory"
        '
        'LabelPartTabNote
        '
        Me.LabelPartTabNote.AutoSize = True
        Me.LabelPartTabNote.Location = New System.Drawing.Point(19, 8)
        Me.LabelPartTabNote.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPartTabNote.Name = "LabelPartTabNote"
        Me.LabelPartTabNote.Size = New System.Drawing.Size(315, 13)
        Me.LabelPartTabNote.TabIndex = 2
        Me.LabelPartTabNote.Text = "Double-click anywhere in the checkbox control to toggle all/none"
        '
        'CheckedListBoxPart
        '
        Me.CheckedListBoxPart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckedListBoxPart.BackColor = System.Drawing.SystemColors.Control
        Me.CheckedListBoxPart.CheckOnClick = True
        Me.CheckedListBoxPart.FormattingEnabled = True
        Me.CheckedListBoxPart.Items.AddRange(New Object() {"Fake name 1", "Fake name 2"})
        Me.CheckedListBoxPart.Location = New System.Drawing.Point(19, 28)
        Me.CheckedListBoxPart.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckedListBoxPart.Name = "CheckedListBoxPart"
        Me.CheckedListBoxPart.Size = New System.Drawing.Size(451, 274)
        Me.CheckedListBoxPart.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxPart, "Double-click to check/uncheck all")
        '
        'TabPageSheetmetal
        '
        Me.TabPageSheetmetal.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageSheetmetal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxSaveAsFormulaSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.CheckBoxSaveAsFormulaSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxExposeVariablesSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelExposeVariablesSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxFindReplaceReplaceSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxFindReplaceFindSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxFindReplacePropertyNameSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.ComboBoxFindReplacePropertySetSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelFindReplaceReplaceSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelFindReplaceFindSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelFindReplacePropertyNameSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelFindReplacePropertySetSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.ButtonExternalProgramSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxExternalProgramSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelExternalProgramSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.ComboBoxSaveAsSheetmetalFileType)
        Me.TabPageSheetmetal.Controls.Add(Me.CheckBoxSaveAsSheetmetalOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.ButtonSaveAsSheetmetalOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.TextBoxSaveAsSheetmetalOutputDirectory)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelSaveAsSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.LabelSheetmetalTabNote)
        Me.TabPageSheetmetal.Controls.Add(Me.CheckedListBoxSheetmetal)
        Me.TabPageSheetmetal.ImageKey = "psm"
        Me.TabPageSheetmetal.Location = New System.Drawing.Point(4, 23)
        Me.TabPageSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageSheetmetal.Name = "TabPageSheetmetal"
        Me.TabPageSheetmetal.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageSheetmetal.Size = New System.Drawing.Size(511, 594)
        Me.TabPageSheetmetal.TabIndex = 3
        Me.TabPageSheetmetal.Text = "Sheetmetal"
        '
        'TextBoxSaveAsFormulaSheetmetal
        '
        Me.TextBoxSaveAsFormulaSheetmetal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsFormulaSheetmetal.Location = New System.Drawing.Point(8, 396)
        Me.TextBoxSaveAsFormulaSheetmetal.Name = "TextBoxSaveAsFormulaSheetmetal"
        Me.TextBoxSaveAsFormulaSheetmetal.Size = New System.Drawing.Size(457, 20)
        Me.TextBoxSaveAsFormulaSheetmetal.TabIndex = 37
        '
        'CheckBoxSaveAsFormulaSheetmetal
        '
        Me.CheckBoxSaveAsFormulaSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsFormulaSheetmetal.AutoSize = True
        Me.CheckBoxSaveAsFormulaSheetmetal.Location = New System.Drawing.Point(8, 376)
        Me.CheckBoxSaveAsFormulaSheetmetal.Name = "CheckBoxSaveAsFormulaSheetmetal"
        Me.CheckBoxSaveAsFormulaSheetmetal.Size = New System.Drawing.Size(142, 17)
        Me.CheckBoxSaveAsFormulaSheetmetal.TabIndex = 36
        Me.CheckBoxSaveAsFormulaSheetmetal.Text = "Use subdirectory formula"
        Me.CheckBoxSaveAsFormulaSheetmetal.UseVisualStyleBackColor = True
        '
        'TextBoxExposeVariablesSheetmetal
        '
        Me.TextBoxExposeVariablesSheetmetal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExposeVariablesSheetmetal.Location = New System.Drawing.Point(8, 546)
        Me.TextBoxExposeVariablesSheetmetal.Name = "TextBoxExposeVariablesSheetmetal"
        Me.TextBoxExposeVariablesSheetmetal.Size = New System.Drawing.Size(457, 20)
        Me.TextBoxExposeVariablesSheetmetal.TabIndex = 35
        '
        'LabelExposeVariablesSheetmetal
        '
        Me.LabelExposeVariablesSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExposeVariablesSheetmetal.AutoSize = True
        Me.LabelExposeVariablesSheetmetal.Location = New System.Drawing.Point(8, 526)
        Me.LabelExposeVariablesSheetmetal.Name = "LabelExposeVariablesSheetmetal"
        Me.LabelExposeVariablesSheetmetal.Size = New System.Drawing.Size(99, 13)
        Me.LabelExposeVariablesSheetmetal.TabIndex = 34
        Me.LabelExposeVariablesSheetmetal.Text = "Variables to expose"
        '
        'TextBoxFindReplaceReplaceSheetmetal
        '
        Me.TextBoxFindReplaceReplaceSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceReplaceSheetmetal.Location = New System.Drawing.Point(350, 496)
        Me.TextBoxFindReplaceReplaceSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceReplaceSheetmetal.Name = "TextBoxFindReplaceReplaceSheetmetal"
        Me.TextBoxFindReplaceReplaceSheetmetal.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFindReplaceReplaceSheetmetal.TabIndex = 33
        '
        'TextBoxFindReplaceFindSheetmetal
        '
        Me.TextBoxFindReplaceFindSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceFindSheetmetal.Location = New System.Drawing.Point(235, 496)
        Me.TextBoxFindReplaceFindSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceFindSheetmetal.Name = "TextBoxFindReplaceFindSheetmetal"
        Me.TextBoxFindReplaceFindSheetmetal.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFindReplaceFindSheetmetal.TabIndex = 32
        '
        'TextBoxFindReplacePropertyNameSheetmetal
        '
        Me.TextBoxFindReplacePropertyNameSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplacePropertyNameSheetmetal.Location = New System.Drawing.Point(120, 496)
        Me.TextBoxFindReplacePropertyNameSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplacePropertyNameSheetmetal.Name = "TextBoxFindReplacePropertyNameSheetmetal"
        Me.TextBoxFindReplacePropertyNameSheetmetal.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFindReplacePropertyNameSheetmetal.TabIndex = 31
        '
        'ComboBoxFindReplacePropertySetSheetmetal
        '
        Me.ComboBoxFindReplacePropertySetSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxFindReplacePropertySetSheetmetal.FormattingEnabled = True
        Me.ComboBoxFindReplacePropertySetSheetmetal.Location = New System.Drawing.Point(8, 496)
        Me.ComboBoxFindReplacePropertySetSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxFindReplacePropertySetSheetmetal.Name = "ComboBoxFindReplacePropertySetSheetmetal"
        Me.ComboBoxFindReplacePropertySetSheetmetal.Size = New System.Drawing.Size(95, 21)
        Me.ComboBoxFindReplacePropertySetSheetmetal.TabIndex = 30
        '
        'LabelFindReplaceReplaceSheetmetal
        '
        Me.LabelFindReplaceReplaceSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceReplaceSheetmetal.AutoSize = True
        Me.LabelFindReplaceReplaceSheetmetal.Location = New System.Drawing.Point(350, 476)
        Me.LabelFindReplaceReplaceSheetmetal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceReplaceSheetmetal.Name = "LabelFindReplaceReplaceSheetmetal"
        Me.LabelFindReplaceReplaceSheetmetal.Size = New System.Drawing.Size(47, 13)
        Me.LabelFindReplaceReplaceSheetmetal.TabIndex = 29
        Me.LabelFindReplaceReplaceSheetmetal.Text = "Replace"
        '
        'LabelFindReplaceFindSheetmetal
        '
        Me.LabelFindReplaceFindSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceFindSheetmetal.AutoSize = True
        Me.LabelFindReplaceFindSheetmetal.Location = New System.Drawing.Point(235, 476)
        Me.LabelFindReplaceFindSheetmetal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceFindSheetmetal.Name = "LabelFindReplaceFindSheetmetal"
        Me.LabelFindReplaceFindSheetmetal.Size = New System.Drawing.Size(27, 13)
        Me.LabelFindReplaceFindSheetmetal.TabIndex = 28
        Me.LabelFindReplaceFindSheetmetal.Text = "Find"
        '
        'LabelFindReplacePropertyNameSheetmetal
        '
        Me.LabelFindReplacePropertyNameSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertyNameSheetmetal.AutoSize = True
        Me.LabelFindReplacePropertyNameSheetmetal.Location = New System.Drawing.Point(120, 476)
        Me.LabelFindReplacePropertyNameSheetmetal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertyNameSheetmetal.Name = "LabelFindReplacePropertyNameSheetmetal"
        Me.LabelFindReplacePropertyNameSheetmetal.Size = New System.Drawing.Size(75, 13)
        Me.LabelFindReplacePropertyNameSheetmetal.TabIndex = 27
        Me.LabelFindReplacePropertyNameSheetmetal.Text = "Property name"
        '
        'LabelFindReplacePropertySetSheetmetal
        '
        Me.LabelFindReplacePropertySetSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertySetSheetmetal.AutoSize = True
        Me.LabelFindReplacePropertySetSheetmetal.Location = New System.Drawing.Point(8, 476)
        Me.LabelFindReplacePropertySetSheetmetal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertySetSheetmetal.Name = "LabelFindReplacePropertySetSheetmetal"
        Me.LabelFindReplacePropertySetSheetmetal.Size = New System.Drawing.Size(65, 13)
        Me.LabelFindReplacePropertySetSheetmetal.TabIndex = 26
        Me.LabelFindReplacePropertySetSheetmetal.Text = "Property Set"
        '
        'ButtonExternalProgramSheetmetal
        '
        Me.ButtonExternalProgramSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalProgramSheetmetal.Location = New System.Drawing.Point(389, 446)
        Me.ButtonExternalProgramSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonExternalProgramSheetmetal.Name = "ButtonExternalProgramSheetmetal"
        Me.ButtonExternalProgramSheetmetal.Size = New System.Drawing.Size(75, 19)
        Me.ButtonExternalProgramSheetmetal.TabIndex = 15
        Me.ButtonExternalProgramSheetmetal.Text = "Browse"
        Me.ButtonExternalProgramSheetmetal.UseVisualStyleBackColor = True
        '
        'TextBoxExternalProgramSheetmetal
        '
        Me.TextBoxExternalProgramSheetmetal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalProgramSheetmetal.Location = New System.Drawing.Point(8, 446)
        Me.TextBoxExternalProgramSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxExternalProgramSheetmetal.Name = "TextBoxExternalProgramSheetmetal"
        Me.TextBoxExternalProgramSheetmetal.Size = New System.Drawing.Size(375, 20)
        Me.TextBoxExternalProgramSheetmetal.TabIndex = 14
        '
        'LabelExternalProgramSheetmetal
        '
        Me.LabelExternalProgramSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExternalProgramSheetmetal.AutoSize = True
        Me.LabelExternalProgramSheetmetal.Location = New System.Drawing.Point(8, 426)
        Me.LabelExternalProgramSheetmetal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelExternalProgramSheetmetal.Name = "LabelExternalProgramSheetmetal"
        Me.LabelExternalProgramSheetmetal.Size = New System.Drawing.Size(86, 13)
        Me.LabelExternalProgramSheetmetal.TabIndex = 13
        Me.LabelExternalProgramSheetmetal.Text = "External program"
        '
        'ComboBoxSaveAsSheetmetalFileType
        '
        Me.ComboBoxSaveAsSheetmetalFileType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxSaveAsSheetmetalFileType.FormattingEnabled = True
        Me.ComboBoxSaveAsSheetmetalFileType.Items.AddRange(New Object() {"fake_item_1"})
        Me.ComboBoxSaveAsSheetmetalFileType.Location = New System.Drawing.Point(150, 323)
        Me.ComboBoxSaveAsSheetmetalFileType.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxSaveAsSheetmetalFileType.Name = "ComboBoxSaveAsSheetmetalFileType"
        Me.ComboBoxSaveAsSheetmetalFileType.Size = New System.Drawing.Size(132, 21)
        Me.ComboBoxSaveAsSheetmetalFileType.Sorted = True
        Me.ComboBoxSaveAsSheetmetalFileType.TabIndex = 12
        '
        'CheckBoxSaveAsSheetmetalOutputDirectory
        '
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Location = New System.Drawing.Point(319, 327)
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Name = "CheckBoxSaveAsSheetmetalOutputDirectory"
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Size = New System.Drawing.Size(146, 17)
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.TabIndex = 11
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsSheetmetalOutputDirectory
        '
        Me.ButtonSaveAsSheetmetalOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsSheetmetalOutputDirectory.Location = New System.Drawing.Point(389, 348)
        Me.ButtonSaveAsSheetmetalOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSaveAsSheetmetalOutputDirectory.Name = "ButtonSaveAsSheetmetalOutputDirectory"
        Me.ButtonSaveAsSheetmetalOutputDirectory.Size = New System.Drawing.Size(75, 20)
        Me.ButtonSaveAsSheetmetalOutputDirectory.TabIndex = 9
        Me.ButtonSaveAsSheetmetalOutputDirectory.Text = "Browse"
        Me.ButtonSaveAsSheetmetalOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsSheetmetalOutputDirectory
        '
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Location = New System.Drawing.Point(8, 346)
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Name = "TextBoxSaveAsSheetmetalOutputDirectory"
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Size = New System.Drawing.Size(376, 20)
        Me.TextBoxSaveAsSheetmetalOutputDirectory.TabIndex = 8
        '
        'LabelSaveAsSheetmetal
        '
        Me.LabelSaveAsSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelSaveAsSheetmetal.AutoSize = True
        Me.LabelSaveAsSheetmetal.Location = New System.Drawing.Point(8, 326)
        Me.LabelSaveAsSheetmetal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelSaveAsSheetmetal.Name = "LabelSaveAsSheetmetal"
        Me.LabelSaveAsSheetmetal.Size = New System.Drawing.Size(123, 13)
        Me.LabelSaveAsSheetmetal.TabIndex = 7
        Me.LabelSaveAsSheetmetal.Text = "Save As output directory"
        '
        'LabelSheetmetalTabNote
        '
        Me.LabelSheetmetalTabNote.AutoSize = True
        Me.LabelSheetmetalTabNote.Location = New System.Drawing.Point(19, 8)
        Me.LabelSheetmetalTabNote.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelSheetmetalTabNote.Name = "LabelSheetmetalTabNote"
        Me.LabelSheetmetalTabNote.Size = New System.Drawing.Size(315, 13)
        Me.LabelSheetmetalTabNote.TabIndex = 6
        Me.LabelSheetmetalTabNote.Text = "Double-click anywhere in the checkbox control to toggle all/none"
        '
        'CheckedListBoxSheetmetal
        '
        Me.CheckedListBoxSheetmetal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckedListBoxSheetmetal.BackColor = System.Drawing.SystemColors.Control
        Me.CheckedListBoxSheetmetal.CheckOnClick = True
        Me.CheckedListBoxSheetmetal.FormattingEnabled = True
        Me.CheckedListBoxSheetmetal.Items.AddRange(New Object() {"Fake name 1", "Fake name 2"})
        Me.CheckedListBoxSheetmetal.Location = New System.Drawing.Point(19, 28)
        Me.CheckedListBoxSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckedListBoxSheetmetal.Name = "CheckedListBoxSheetmetal"
        Me.CheckedListBoxSheetmetal.Size = New System.Drawing.Size(451, 274)
        Me.CheckedListBoxSheetmetal.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxSheetmetal, "Double-click to check/uncheck all")
        '
        'TabPageDraft
        '
        Me.TabPageDraft.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageDraft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageDraft.Controls.Add(Me.TextBoxSaveAsFormulaDraft)
        Me.TabPageDraft.Controls.Add(Me.CheckBoxSaveAsFormulaDraft)
        Me.TabPageDraft.Controls.Add(Me.ButtonWatermark)
        Me.TabPageDraft.Controls.Add(Me.TextBoxWatermarkFilename)
        Me.TabPageDraft.Controls.Add(Me.TextBoxWatermarkScale)
        Me.TabPageDraft.Controls.Add(Me.LabelWatermarkScale)
        Me.TabPageDraft.Controls.Add(Me.TextBoxWatermarkY)
        Me.TabPageDraft.Controls.Add(Me.LabelWatermarkY)
        Me.TabPageDraft.Controls.Add(Me.TextBoxWatermarkX)
        Me.TabPageDraft.Controls.Add(Me.LabelWatermarkX)
        Me.TabPageDraft.Controls.Add(Me.CheckBoxWatermark)
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
        Me.TabPageDraft.ImageKey = "dft"
        Me.TabPageDraft.Location = New System.Drawing.Point(4, 23)
        Me.TabPageDraft.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageDraft.Name = "TabPageDraft"
        Me.TabPageDraft.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageDraft.Size = New System.Drawing.Size(511, 594)
        Me.TabPageDraft.TabIndex = 4
        Me.TabPageDraft.Text = "Draft"
        '
        'TextBoxSaveAsFormulaDraft
        '
        Me.TextBoxSaveAsFormulaDraft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsFormulaDraft.Location = New System.Drawing.Point(8, 446)
        Me.TextBoxSaveAsFormulaDraft.Name = "TextBoxSaveAsFormulaDraft"
        Me.TextBoxSaveAsFormulaDraft.Size = New System.Drawing.Size(456, 20)
        Me.TextBoxSaveAsFormulaDraft.TabIndex = 26
        '
        'CheckBoxSaveAsFormulaDraft
        '
        Me.CheckBoxSaveAsFormulaDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsFormulaDraft.AutoSize = True
        Me.CheckBoxSaveAsFormulaDraft.Location = New System.Drawing.Point(8, 426)
        Me.CheckBoxSaveAsFormulaDraft.Name = "CheckBoxSaveAsFormulaDraft"
        Me.CheckBoxSaveAsFormulaDraft.Size = New System.Drawing.Size(142, 17)
        Me.CheckBoxSaveAsFormulaDraft.TabIndex = 25
        Me.CheckBoxSaveAsFormulaDraft.Text = "Use subdirectory formula"
        Me.CheckBoxSaveAsFormulaDraft.UseVisualStyleBackColor = True
        '
        'ButtonWatermark
        '
        Me.ButtonWatermark.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonWatermark.Location = New System.Drawing.Point(389, 497)
        Me.ButtonWatermark.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonWatermark.Name = "ButtonWatermark"
        Me.ButtonWatermark.Size = New System.Drawing.Size(75, 20)
        Me.ButtonWatermark.TabIndex = 24
        Me.ButtonWatermark.Text = "Browse"
        Me.ButtonWatermark.UseVisualStyleBackColor = True
        '
        'TextBoxWatermarkFilename
        '
        Me.TextBoxWatermarkFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWatermarkFilename.Location = New System.Drawing.Point(8, 497)
        Me.TextBoxWatermarkFilename.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxWatermarkFilename.Name = "TextBoxWatermarkFilename"
        Me.TextBoxWatermarkFilename.Size = New System.Drawing.Size(376, 20)
        Me.TextBoxWatermarkFilename.TabIndex = 23
        '
        'TextBoxWatermarkScale
        '
        Me.TextBoxWatermarkScale.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWatermarkScale.Location = New System.Drawing.Point(297, 476)
        Me.TextBoxWatermarkScale.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxWatermarkScale.Name = "TextBoxWatermarkScale"
        Me.TextBoxWatermarkScale.Size = New System.Drawing.Size(38, 20)
        Me.TextBoxWatermarkScale.TabIndex = 22
        '
        'LabelWatermarkScale
        '
        Me.LabelWatermarkScale.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelWatermarkScale.AutoSize = True
        Me.LabelWatermarkScale.Location = New System.Drawing.Point(255, 478)
        Me.LabelWatermarkScale.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelWatermarkScale.Name = "LabelWatermarkScale"
        Me.LabelWatermarkScale.Size = New System.Drawing.Size(34, 13)
        Me.LabelWatermarkScale.TabIndex = 21
        Me.LabelWatermarkScale.Text = "Scale"
        '
        'TextBoxWatermarkY
        '
        Me.TextBoxWatermarkY.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWatermarkY.Location = New System.Drawing.Point(202, 476)
        Me.TextBoxWatermarkY.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxWatermarkY.Name = "TextBoxWatermarkY"
        Me.TextBoxWatermarkY.Size = New System.Drawing.Size(38, 20)
        Me.TextBoxWatermarkY.TabIndex = 20
        '
        'LabelWatermarkY
        '
        Me.LabelWatermarkY.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelWatermarkY.AutoSize = True
        Me.LabelWatermarkY.Location = New System.Drawing.Point(172, 478)
        Me.LabelWatermarkY.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelWatermarkY.Name = "LabelWatermarkY"
        Me.LabelWatermarkY.Size = New System.Drawing.Size(27, 13)
        Me.LabelWatermarkY.TabIndex = 19
        Me.LabelWatermarkY.Text = "Y/H"
        '
        'TextBoxWatermarkX
        '
        Me.TextBoxWatermarkX.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWatermarkX.Location = New System.Drawing.Point(120, 476)
        Me.TextBoxWatermarkX.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxWatermarkX.Name = "TextBoxWatermarkX"
        Me.TextBoxWatermarkX.Size = New System.Drawing.Size(38, 20)
        Me.TextBoxWatermarkX.TabIndex = 18
        '
        'LabelWatermarkX
        '
        Me.LabelWatermarkX.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelWatermarkX.AutoSize = True
        Me.LabelWatermarkX.Location = New System.Drawing.Point(90, 478)
        Me.LabelWatermarkX.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelWatermarkX.Name = "LabelWatermarkX"
        Me.LabelWatermarkX.Size = New System.Drawing.Size(30, 13)
        Me.LabelWatermarkX.TabIndex = 17
        Me.LabelWatermarkX.Text = "X/W"
        '
        'CheckBoxWatermark
        '
        Me.CheckBoxWatermark.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxWatermark.AutoSize = True
        Me.CheckBoxWatermark.Location = New System.Drawing.Point(8, 476)
        Me.CheckBoxWatermark.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxWatermark.Name = "CheckBoxWatermark"
        Me.CheckBoxWatermark.Size = New System.Drawing.Size(78, 17)
        Me.CheckBoxWatermark.TabIndex = 16
        Me.CheckBoxWatermark.Text = "Watermark"
        Me.CheckBoxWatermark.UseVisualStyleBackColor = True
        '
        'ButtonExternalProgramDraft
        '
        Me.ButtonExternalProgramDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalProgramDraft.Location = New System.Drawing.Point(389, 546)
        Me.ButtonExternalProgramDraft.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonExternalProgramDraft.Name = "ButtonExternalProgramDraft"
        Me.ButtonExternalProgramDraft.Size = New System.Drawing.Size(75, 19)
        Me.ButtonExternalProgramDraft.TabIndex = 15
        Me.ButtonExternalProgramDraft.Text = "Browse"
        Me.ButtonExternalProgramDraft.UseVisualStyleBackColor = True
        '
        'TextBoxExternalProgramDraft
        '
        Me.TextBoxExternalProgramDraft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalProgramDraft.Location = New System.Drawing.Point(8, 546)
        Me.TextBoxExternalProgramDraft.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxExternalProgramDraft.Name = "TextBoxExternalProgramDraft"
        Me.TextBoxExternalProgramDraft.Size = New System.Drawing.Size(375, 20)
        Me.TextBoxExternalProgramDraft.TabIndex = 14
        '
        'LabelExternalProgramDraft
        '
        Me.LabelExternalProgramDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExternalProgramDraft.AutoSize = True
        Me.LabelExternalProgramDraft.Location = New System.Drawing.Point(8, 526)
        Me.LabelExternalProgramDraft.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelExternalProgramDraft.Name = "LabelExternalProgramDraft"
        Me.LabelExternalProgramDraft.Size = New System.Drawing.Size(86, 13)
        Me.LabelExternalProgramDraft.TabIndex = 13
        Me.LabelExternalProgramDraft.Text = "External program"
        '
        'ComboBoxSaveAsDraftFileType
        '
        Me.ComboBoxSaveAsDraftFileType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxSaveAsDraftFileType.FormattingEnabled = True
        Me.ComboBoxSaveAsDraftFileType.Items.AddRange(New Object() {"fake_item_1"})
        Me.ComboBoxSaveAsDraftFileType.Location = New System.Drawing.Point(150, 372)
        Me.ComboBoxSaveAsDraftFileType.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxSaveAsDraftFileType.Name = "ComboBoxSaveAsDraftFileType"
        Me.ComboBoxSaveAsDraftFileType.Size = New System.Drawing.Size(132, 21)
        Me.ComboBoxSaveAsDraftFileType.Sorted = True
        Me.ComboBoxSaveAsDraftFileType.TabIndex = 12
        '
        'CheckBoxSaveAsDraftOutputDirectory
        '
        Me.CheckBoxSaveAsDraftOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsDraftOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsDraftOutputDirectory.Location = New System.Drawing.Point(319, 376)
        Me.CheckBoxSaveAsDraftOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxSaveAsDraftOutputDirectory.Name = "CheckBoxSaveAsDraftOutputDirectory"
        Me.CheckBoxSaveAsDraftOutputDirectory.Size = New System.Drawing.Size(146, 17)
        Me.CheckBoxSaveAsDraftOutputDirectory.TabIndex = 10
        Me.CheckBoxSaveAsDraftOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsDraftOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsDraftOutputDirectory
        '
        Me.ButtonSaveAsDraftOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsDraftOutputDirectory.Location = New System.Drawing.Point(389, 397)
        Me.ButtonSaveAsDraftOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSaveAsDraftOutputDirectory.Name = "ButtonSaveAsDraftOutputDirectory"
        Me.ButtonSaveAsDraftOutputDirectory.Size = New System.Drawing.Size(75, 20)
        Me.ButtonSaveAsDraftOutputDirectory.TabIndex = 8
        Me.ButtonSaveAsDraftOutputDirectory.Text = "Browse"
        Me.ButtonSaveAsDraftOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsDraftOutputDirectory
        '
        Me.TextBoxSaveAsDraftOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsDraftOutputDirectory.Location = New System.Drawing.Point(8, 397)
        Me.TextBoxSaveAsDraftOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxSaveAsDraftOutputDirectory.Name = "TextBoxSaveAsDraftOutputDirectory"
        Me.TextBoxSaveAsDraftOutputDirectory.Size = New System.Drawing.Size(376, 20)
        Me.TextBoxSaveAsDraftOutputDirectory.TabIndex = 6
        '
        'LabelSaveAsDraftOutputDirectory
        '
        Me.LabelSaveAsDraftOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelSaveAsDraftOutputDirectory.AutoSize = True
        Me.LabelSaveAsDraftOutputDirectory.Location = New System.Drawing.Point(8, 376)
        Me.LabelSaveAsDraftOutputDirectory.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelSaveAsDraftOutputDirectory.Name = "LabelSaveAsDraftOutputDirectory"
        Me.LabelSaveAsDraftOutputDirectory.Size = New System.Drawing.Size(123, 13)
        Me.LabelSaveAsDraftOutputDirectory.TabIndex = 4
        Me.LabelSaveAsDraftOutputDirectory.Text = "Save As output directory"
        '
        'LabelDraftTabNote
        '
        Me.LabelDraftTabNote.AutoSize = True
        Me.LabelDraftTabNote.Location = New System.Drawing.Point(19, 8)
        Me.LabelDraftTabNote.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelDraftTabNote.Name = "LabelDraftTabNote"
        Me.LabelDraftTabNote.Size = New System.Drawing.Size(315, 13)
        Me.LabelDraftTabNote.TabIndex = 3
        Me.LabelDraftTabNote.Text = "Double-click anywhere in the checkbox control to toggle all/none"
        '
        'CheckedListBoxDraft
        '
        Me.CheckedListBoxDraft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckedListBoxDraft.BackColor = System.Drawing.SystemColors.Control
        Me.CheckedListBoxDraft.CheckOnClick = True
        Me.CheckedListBoxDraft.FormattingEnabled = True
        Me.CheckedListBoxDraft.Items.AddRange(New Object() {"Fake name 1", "Fake name 2"})
        Me.CheckedListBoxDraft.Location = New System.Drawing.Point(19, 28)
        Me.CheckedListBoxDraft.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckedListBoxDraft.Name = "CheckedListBoxDraft"
        Me.CheckedListBoxDraft.Size = New System.Drawing.Size(451, 334)
        Me.CheckedListBoxDraft.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxDraft, "Double-click to check/uncheck all")
        '
        'TabPageConfiguration
        '
        Me.TabPageConfiguration.AutoScroll = True
        Me.TabPageConfiguration.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageConfiguration.Controls.Add(Me.CheckBoxSaveAsImageCrop)
        Me.TabPageConfiguration.Controls.Add(Me.CheckBoxBackgroundProcessing)
        Me.TabPageConfiguration.Controls.Add(Me.CheckBoxRunExternalProgramSaveFile)
        Me.TabPageConfiguration.Controls.Add(Me.GroupBoxPictorialViews)
        Me.TabPageConfiguration.Controls.Add(Me.LabelPrintOptionsCopies)
        Me.TabPageConfiguration.Controls.Add(Me.LabelPrintOptionsHeight)
        Me.TabPageConfiguration.Controls.Add(Me.LabelPrintOptionsWidth)
        Me.TabPageConfiguration.Controls.Add(Me.LabelPrintOptionsPrinter)
        Me.TabPageConfiguration.Controls.Add(Me.CheckBoxPrintOptionsScaleLineWidths)
        Me.TabPageConfiguration.Controls.Add(Me.CheckBoxPrintOptionsScaleLineTypes)
        Me.TabPageConfiguration.Controls.Add(Me.CheckBoxPrintOptionsPrintAsBlack)
        Me.TabPageConfiguration.Controls.Add(Me.CheckBoxPrintOptionsBestFit)
        Me.TabPageConfiguration.Controls.Add(Me.CheckBoxPrintOptionsAutoOrient)
        Me.TabPageConfiguration.Controls.Add(Me.TextBoxPrintOptionsCopies)
        Me.TabPageConfiguration.Controls.Add(Me.TextBoxPrintOptionsHeight)
        Me.TabPageConfiguration.Controls.Add(Me.TextBoxPrintOptionsWidth)
        Me.TabPageConfiguration.Controls.Add(Me.TextBoxPrintOptionsPrinter)
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
        Me.TabPageConfiguration.ImageKey = "config"
        Me.TabPageConfiguration.Location = New System.Drawing.Point(4, 23)
        Me.TabPageConfiguration.Name = "TabPageConfiguration"
        Me.TabPageConfiguration.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageConfiguration.Size = New System.Drawing.Size(511, 594)
        Me.TabPageConfiguration.TabIndex = 5
        Me.TabPageConfiguration.Text = "Configuration"
        '
        'CheckBoxSaveAsImageCrop
        '
        Me.CheckBoxSaveAsImageCrop.AutoSize = True
        Me.CheckBoxSaveAsImageCrop.Location = New System.Drawing.Point(8, 631)
        Me.CheckBoxSaveAsImageCrop.Name = "CheckBoxSaveAsImageCrop"
        Me.CheckBoxSaveAsImageCrop.Size = New System.Drawing.Size(194, 17)
        Me.CheckBoxSaveAsImageCrop.TabIndex = 37
        Me.CheckBoxSaveAsImageCrop.Text = "Save as image -- Crop to model size"
        Me.CheckBoxSaveAsImageCrop.UseVisualStyleBackColor = True
        '
        'CheckBoxBackgroundProcessing
        '
        Me.CheckBoxBackgroundProcessing.AutoSize = True
        Me.CheckBoxBackgroundProcessing.Location = New System.Drawing.Point(8, 557)
        Me.CheckBoxBackgroundProcessing.Name = "CheckBoxBackgroundProcessing"
        Me.CheckBoxBackgroundProcessing.Size = New System.Drawing.Size(227, 17)
        Me.CheckBoxBackgroundProcessing.TabIndex = 36
        Me.CheckBoxBackgroundProcessing.Text = "Process tasks in background (no graphics)"
        Me.CheckBoxBackgroundProcessing.UseVisualStyleBackColor = True
        '
        'CheckBoxRunExternalProgramSaveFile
        '
        Me.CheckBoxRunExternalProgramSaveFile.AutoSize = True
        Me.CheckBoxRunExternalProgramSaveFile.Location = New System.Drawing.Point(8, 607)
        Me.CheckBoxRunExternalProgramSaveFile.Name = "CheckBoxRunExternalProgramSaveFile"
        Me.CheckBoxRunExternalProgramSaveFile.Size = New System.Drawing.Size(222, 17)
        Me.CheckBoxRunExternalProgramSaveFile.TabIndex = 35
        Me.CheckBoxRunExternalProgramSaveFile.Text = "Run external program -- Save file after run"
        Me.CheckBoxRunExternalProgramSaveFile.UseVisualStyleBackColor = True
        '
        'GroupBoxPictorialViews
        '
        Me.GroupBoxPictorialViews.Controls.Add(Me.RadioButtonPictorialViewTrimetric)
        Me.GroupBoxPictorialViews.Controls.Add(Me.RadioButtonPictorialViewDimetric)
        Me.GroupBoxPictorialViews.Controls.Add(Me.RadioButtonPictorialViewIsometric)
        Me.GroupBoxPictorialViews.Location = New System.Drawing.Point(11, 956)
        Me.GroupBoxPictorialViews.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBoxPictorialViews.Name = "GroupBoxPictorialViews"
        Me.GroupBoxPictorialViews.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBoxPictorialViews.Size = New System.Drawing.Size(112, 81)
        Me.GroupBoxPictorialViews.TabIndex = 34
        Me.GroupBoxPictorialViews.TabStop = False
        Me.GroupBoxPictorialViews.Text = "Pictorial views"
        '
        'RadioButtonPictorialViewTrimetric
        '
        Me.RadioButtonPictorialViewTrimetric.AutoSize = True
        Me.RadioButtonPictorialViewTrimetric.Location = New System.Drawing.Point(0, 61)
        Me.RadioButtonPictorialViewTrimetric.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonPictorialViewTrimetric.Name = "RadioButtonPictorialViewTrimetric"
        Me.RadioButtonPictorialViewTrimetric.Size = New System.Drawing.Size(65, 17)
        Me.RadioButtonPictorialViewTrimetric.TabIndex = 2
        Me.RadioButtonPictorialViewTrimetric.TabStop = True
        Me.RadioButtonPictorialViewTrimetric.Text = "Trimetric"
        Me.RadioButtonPictorialViewTrimetric.UseVisualStyleBackColor = True
        '
        'RadioButtonPictorialViewDimetric
        '
        Me.RadioButtonPictorialViewDimetric.AutoSize = True
        Me.RadioButtonPictorialViewDimetric.Location = New System.Drawing.Point(0, 41)
        Me.RadioButtonPictorialViewDimetric.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonPictorialViewDimetric.Name = "RadioButtonPictorialViewDimetric"
        Me.RadioButtonPictorialViewDimetric.Size = New System.Drawing.Size(63, 17)
        Me.RadioButtonPictorialViewDimetric.TabIndex = 1
        Me.RadioButtonPictorialViewDimetric.TabStop = True
        Me.RadioButtonPictorialViewDimetric.Text = "Dimetric"
        Me.RadioButtonPictorialViewDimetric.UseVisualStyleBackColor = True
        '
        'RadioButtonPictorialViewIsometric
        '
        Me.RadioButtonPictorialViewIsometric.AutoSize = True
        Me.RadioButtonPictorialViewIsometric.Location = New System.Drawing.Point(0, 20)
        Me.RadioButtonPictorialViewIsometric.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonPictorialViewIsometric.Name = "RadioButtonPictorialViewIsometric"
        Me.RadioButtonPictorialViewIsometric.Size = New System.Drawing.Size(67, 17)
        Me.RadioButtonPictorialViewIsometric.TabIndex = 0
        Me.RadioButtonPictorialViewIsometric.TabStop = True
        Me.RadioButtonPictorialViewIsometric.Text = "Isometric"
        Me.RadioButtonPictorialViewIsometric.UseVisualStyleBackColor = True
        '
        'LabelPrintOptionsCopies
        '
        Me.LabelPrintOptionsCopies.AutoSize = True
        Me.LabelPrintOptionsCopies.Location = New System.Drawing.Point(11, 785)
        Me.LabelPrintOptionsCopies.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPrintOptionsCopies.Name = "LabelPrintOptionsCopies"
        Me.LabelPrintOptionsCopies.Size = New System.Drawing.Size(39, 13)
        Me.LabelPrintOptionsCopies.TabIndex = 33
        Me.LabelPrintOptionsCopies.Text = "Copies"
        '
        'LabelPrintOptionsHeight
        '
        Me.LabelPrintOptionsHeight.AutoSize = True
        Me.LabelPrintOptionsHeight.Location = New System.Drawing.Point(112, 744)
        Me.LabelPrintOptionsHeight.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPrintOptionsHeight.Name = "LabelPrintOptionsHeight"
        Me.LabelPrintOptionsHeight.Size = New System.Drawing.Size(55, 13)
        Me.LabelPrintOptionsHeight.TabIndex = 32
        Me.LabelPrintOptionsHeight.Text = "Height (in)"
        '
        'LabelPrintOptionsWidth
        '
        Me.LabelPrintOptionsWidth.AutoSize = True
        Me.LabelPrintOptionsWidth.Location = New System.Drawing.Point(11, 744)
        Me.LabelPrintOptionsWidth.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPrintOptionsWidth.Name = "LabelPrintOptionsWidth"
        Me.LabelPrintOptionsWidth.Size = New System.Drawing.Size(52, 13)
        Me.LabelPrintOptionsWidth.TabIndex = 31
        Me.LabelPrintOptionsWidth.Text = "Width (in)"
        '
        'LabelPrintOptionsPrinter
        '
        Me.LabelPrintOptionsPrinter.AutoSize = True
        Me.LabelPrintOptionsPrinter.Location = New System.Drawing.Point(11, 704)
        Me.LabelPrintOptionsPrinter.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPrintOptionsPrinter.Name = "LabelPrintOptionsPrinter"
        Me.LabelPrintOptionsPrinter.Size = New System.Drawing.Size(37, 13)
        Me.LabelPrintOptionsPrinter.TabIndex = 30
        Me.LabelPrintOptionsPrinter.Text = "Printer"
        '
        'CheckBoxPrintOptionsScaleLineWidths
        '
        Me.CheckBoxPrintOptionsScaleLineWidths.AutoSize = True
        Me.CheckBoxPrintOptionsScaleLineWidths.Location = New System.Drawing.Point(11, 912)
        Me.CheckBoxPrintOptionsScaleLineWidths.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrintOptionsScaleLineWidths.Name = "CheckBoxPrintOptionsScaleLineWidths"
        Me.CheckBoxPrintOptionsScaleLineWidths.Size = New System.Drawing.Size(105, 17)
        Me.CheckBoxPrintOptionsScaleLineWidths.TabIndex = 29
        Me.CheckBoxPrintOptionsScaleLineWidths.Text = "Scale line widths"
        Me.CheckBoxPrintOptionsScaleLineWidths.UseVisualStyleBackColor = True
        '
        'CheckBoxPrintOptionsScaleLineTypes
        '
        Me.CheckBoxPrintOptionsScaleLineTypes.AutoSize = True
        Me.CheckBoxPrintOptionsScaleLineTypes.Location = New System.Drawing.Point(11, 891)
        Me.CheckBoxPrintOptionsScaleLineTypes.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrintOptionsScaleLineTypes.Name = "CheckBoxPrintOptionsScaleLineTypes"
        Me.CheckBoxPrintOptionsScaleLineTypes.Size = New System.Drawing.Size(100, 17)
        Me.CheckBoxPrintOptionsScaleLineTypes.TabIndex = 28
        Me.CheckBoxPrintOptionsScaleLineTypes.Text = "Scale line types"
        Me.CheckBoxPrintOptionsScaleLineTypes.UseVisualStyleBackColor = True
        '
        'CheckBoxPrintOptionsPrintAsBlack
        '
        Me.CheckBoxPrintOptionsPrintAsBlack.AutoSize = True
        Me.CheckBoxPrintOptionsPrintAsBlack.Location = New System.Drawing.Point(11, 871)
        Me.CheckBoxPrintOptionsPrintAsBlack.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrintOptionsPrintAsBlack.Name = "CheckBoxPrintOptionsPrintAsBlack"
        Me.CheckBoxPrintOptionsPrintAsBlack.Size = New System.Drawing.Size(90, 17)
        Me.CheckBoxPrintOptionsPrintAsBlack.TabIndex = 27
        Me.CheckBoxPrintOptionsPrintAsBlack.Text = "Print as black"
        Me.CheckBoxPrintOptionsPrintAsBlack.UseVisualStyleBackColor = True
        '
        'CheckBoxPrintOptionsBestFit
        '
        Me.CheckBoxPrintOptionsBestFit.AutoSize = True
        Me.CheckBoxPrintOptionsBestFit.Location = New System.Drawing.Point(11, 851)
        Me.CheckBoxPrintOptionsBestFit.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrintOptionsBestFit.Name = "CheckBoxPrintOptionsBestFit"
        Me.CheckBoxPrintOptionsBestFit.Size = New System.Drawing.Size(58, 17)
        Me.CheckBoxPrintOptionsBestFit.TabIndex = 26
        Me.CheckBoxPrintOptionsBestFit.Text = "Best fit"
        Me.CheckBoxPrintOptionsBestFit.UseVisualStyleBackColor = True
        '
        'CheckBoxPrintOptionsAutoOrient
        '
        Me.CheckBoxPrintOptionsAutoOrient.AutoSize = True
        Me.CheckBoxPrintOptionsAutoOrient.Location = New System.Drawing.Point(11, 831)
        Me.CheckBoxPrintOptionsAutoOrient.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrintOptionsAutoOrient.Name = "CheckBoxPrintOptionsAutoOrient"
        Me.CheckBoxPrintOptionsAutoOrient.Size = New System.Drawing.Size(77, 17)
        Me.CheckBoxPrintOptionsAutoOrient.TabIndex = 25
        Me.CheckBoxPrintOptionsAutoOrient.Text = "Auto orient"
        Me.CheckBoxPrintOptionsAutoOrient.UseVisualStyleBackColor = True
        '
        'TextBoxPrintOptionsCopies
        '
        Me.TextBoxPrintOptionsCopies.Enabled = False
        Me.TextBoxPrintOptionsCopies.Location = New System.Drawing.Point(11, 801)
        Me.TextBoxPrintOptionsCopies.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPrintOptionsCopies.Name = "TextBoxPrintOptionsCopies"
        Me.TextBoxPrintOptionsCopies.Size = New System.Drawing.Size(76, 20)
        Me.TextBoxPrintOptionsCopies.TabIndex = 24
        '
        'TextBoxPrintOptionsHeight
        '
        Me.TextBoxPrintOptionsHeight.Enabled = False
        Me.TextBoxPrintOptionsHeight.Location = New System.Drawing.Point(112, 760)
        Me.TextBoxPrintOptionsHeight.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPrintOptionsHeight.Name = "TextBoxPrintOptionsHeight"
        Me.TextBoxPrintOptionsHeight.Size = New System.Drawing.Size(76, 20)
        Me.TextBoxPrintOptionsHeight.TabIndex = 23
        '
        'TextBoxPrintOptionsWidth
        '
        Me.TextBoxPrintOptionsWidth.Enabled = False
        Me.TextBoxPrintOptionsWidth.Location = New System.Drawing.Point(11, 760)
        Me.TextBoxPrintOptionsWidth.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPrintOptionsWidth.Name = "TextBoxPrintOptionsWidth"
        Me.TextBoxPrintOptionsWidth.Size = New System.Drawing.Size(76, 20)
        Me.TextBoxPrintOptionsWidth.TabIndex = 22
        '
        'TextBoxPrintOptionsPrinter
        '
        Me.TextBoxPrintOptionsPrinter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPrintOptionsPrinter.Enabled = False
        Me.TextBoxPrintOptionsPrinter.Location = New System.Drawing.Point(11, 720)
        Me.TextBoxPrintOptionsPrinter.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPrintOptionsPrinter.Name = "TextBoxPrintOptionsPrinter"
        Me.TextBoxPrintOptionsPrinter.Size = New System.Drawing.Size(78, 20)
        Me.TextBoxPrintOptionsPrinter.TabIndex = 21
        '
        'ButtonPrintOptions
        '
        Me.ButtonPrintOptions.Location = New System.Drawing.Point(8, 679)
        Me.ButtonPrintOptions.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonPrintOptions.Name = "ButtonPrintOptions"
        Me.ButtonPrintOptions.Size = New System.Drawing.Size(112, 19)
        Me.ButtonPrintOptions.TabIndex = 20
        Me.ButtonPrintOptions.Text = "Printer Settings"
        Me.ButtonPrintOptions.UseVisualStyleBackColor = True
        '
        'CheckBoxRememberTasks
        '
        Me.CheckBoxRememberTasks.AutoSize = True
        Me.CheckBoxRememberTasks.Checked = True
        Me.CheckBoxRememberTasks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRememberTasks.Location = New System.Drawing.Point(8, 532)
        Me.CheckBoxRememberTasks.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxRememberTasks.Name = "CheckBoxRememberTasks"
        Me.CheckBoxRememberTasks.Size = New System.Drawing.Size(235, 17)
        Me.CheckBoxRememberTasks.TabIndex = 19
        Me.CheckBoxRememberTasks.Text = "Remember selected tasks between sessions"
        Me.CheckBoxRememberTasks.UseVisualStyleBackColor = True
        '
        'CheckBoxMoveDrawingViewAllowPartialSuccess
        '
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.AutoSize = True
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Location = New System.Drawing.Point(8, 582)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Name = "CheckBoxMoveDrawingViewAllowPartialSuccess"
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Size = New System.Drawing.Size(266, 17)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.TabIndex = 18
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Text = "Update styles from template -- Allow partial success"
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
        Me.GroupBoxTLAOptions.Location = New System.Drawing.Point(8, 353)
        Me.GroupBoxTLAOptions.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBoxTLAOptions.Name = "GroupBoxTLAOptions"
        Me.GroupBoxTLAOptions.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBoxTLAOptions.Size = New System.Drawing.Size(444, 140)
        Me.GroupBoxTLAOptions.TabIndex = 17
        Me.GroupBoxTLAOptions.TabStop = False
        Me.GroupBoxTLAOptions.Text = "Top level assembly processing options -- See Readme tab for details"
        '
        'ButtonFastSearchScopeFilename
        '
        Me.ButtonFastSearchScopeFilename.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonFastSearchScopeFilename.Location = New System.Drawing.Point(348, 97)
        Me.ButtonFastSearchScopeFilename.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonFastSearchScopeFilename.Name = "ButtonFastSearchScopeFilename"
        Me.ButtonFastSearchScopeFilename.Size = New System.Drawing.Size(75, 20)
        Me.ButtonFastSearchScopeFilename.TabIndex = 5
        Me.ButtonFastSearchScopeFilename.Text = "Browse"
        Me.ButtonFastSearchScopeFilename.UseVisualStyleBackColor = True
        '
        'TextBoxFastSearchScopeFilename
        '
        Me.TextBoxFastSearchScopeFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFastSearchScopeFilename.Location = New System.Drawing.Point(11, 98)
        Me.TextBoxFastSearchScopeFilename.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFastSearchScopeFilename.Name = "TextBoxFastSearchScopeFilename"
        Me.TextBoxFastSearchScopeFilename.Size = New System.Drawing.Size(324, 20)
        Me.TextBoxFastSearchScopeFilename.TabIndex = 4
        '
        'LabelFastSearchScopeFilename
        '
        Me.LabelFastSearchScopeFilename.AutoSize = True
        Me.LabelFastSearchScopeFilename.Location = New System.Drawing.Point(11, 81)
        Me.LabelFastSearchScopeFilename.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFastSearchScopeFilename.Name = "LabelFastSearchScopeFilename"
        Me.LabelFastSearchScopeFilename.Size = New System.Drawing.Size(143, 13)
        Me.LabelFastSearchScopeFilename.TabIndex = 3
        Me.LabelFastSearchScopeFilename.Text = "Fast Search Scope Filename"
        '
        'CheckBoxTLAReportUnrelatedFiles
        '
        Me.CheckBoxTLAReportUnrelatedFiles.AutoSize = True
        Me.CheckBoxTLAReportUnrelatedFiles.Location = New System.Drawing.Point(11, 61)
        Me.CheckBoxTLAReportUnrelatedFiles.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxTLAReportUnrelatedFiles.Name = "CheckBoxTLAReportUnrelatedFiles"
        Me.CheckBoxTLAReportUnrelatedFiles.Size = New System.Drawing.Size(227, 17)
        Me.CheckBoxTLAReportUnrelatedFiles.TabIndex = 2
        Me.CheckBoxTLAReportUnrelatedFiles.Text = "Report files unrelated to top level assembly"
        Me.CheckBoxTLAReportUnrelatedFiles.UseVisualStyleBackColor = True
        '
        'RadioButtonTLATopDown
        '
        Me.RadioButtonTLATopDown.AutoSize = True
        Me.RadioButtonTLATopDown.Checked = True
        Me.RadioButtonTLATopDown.Location = New System.Drawing.Point(11, 41)
        Me.RadioButtonTLATopDown.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonTLATopDown.Name = "RadioButtonTLATopDown"
        Me.RadioButtonTLATopDown.Size = New System.Drawing.Size(276, 17)
        Me.RadioButtonTLATopDown.TabIndex = 1
        Me.RadioButtonTLATopDown.TabStop = True
        Me.RadioButtonTLATopDown.Text = "Top down -- Best for self-contained project directories"
        Me.RadioButtonTLATopDown.UseVisualStyleBackColor = True
        '
        'RadioButtonTLABottomUp
        '
        Me.RadioButtonTLABottomUp.AutoSize = True
        Me.RadioButtonTLABottomUp.Location = New System.Drawing.Point(11, 20)
        Me.RadioButtonTLABottomUp.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonTLABottomUp.Name = "RadioButtonTLABottomUp"
        Me.RadioButtonTLABottomUp.Size = New System.Drawing.Size(253, 17)
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
        Me.CheckBoxWarnSave.Location = New System.Drawing.Point(8, 508)
        Me.CheckBoxWarnSave.Name = "CheckBoxWarnSave"
        Me.CheckBoxWarnSave.Size = New System.Drawing.Size(170, 17)
        Me.CheckBoxWarnSave.TabIndex = 16
        Me.CheckBoxWarnSave.Text = "Warn me if file save is required"
        Me.CheckBoxWarnSave.UseVisualStyleBackColor = True
        '
        'TextBoxRestartAfter
        '
        Me.TextBoxRestartAfter.Location = New System.Drawing.Point(8, 317)
        Me.TextBoxRestartAfter.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxRestartAfter.Name = "TextBoxRestartAfter"
        Me.TextBoxRestartAfter.Size = New System.Drawing.Size(76, 20)
        Me.TextBoxRestartAfter.TabIndex = 15
        Me.TextBoxRestartAfter.Text = "50"
        '
        'LabelRestartAfter
        '
        Me.LabelRestartAfter.AutoSize = True
        Me.LabelRestartAfter.Location = New System.Drawing.Point(8, 301)
        Me.LabelRestartAfter.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelRestartAfter.Name = "LabelRestartAfter"
        Me.LabelRestartAfter.Size = New System.Drawing.Size(195, 13)
        Me.LabelRestartAfter.TabIndex = 14
        Me.LabelRestartAfter.Text = "Restart After This Many Files Processed"
        '
        'TextBoxPartNumberPropertyName
        '
        Me.TextBoxPartNumberPropertyName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPartNumberPropertyName.Location = New System.Drawing.Point(172, 268)
        Me.TextBoxPartNumberPropertyName.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPartNumberPropertyName.Name = "TextBoxPartNumberPropertyName"
        Me.TextBoxPartNumberPropertyName.Size = New System.Drawing.Size(0, 20)
        Me.TextBoxPartNumberPropertyName.TabIndex = 13
        '
        'LabelPartNumberPropertyName
        '
        Me.LabelPartNumberPropertyName.AutoSize = True
        Me.LabelPartNumberPropertyName.Location = New System.Drawing.Point(172, 252)
        Me.LabelPartNumberPropertyName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPartNumberPropertyName.Name = "LabelPartNumberPropertyName"
        Me.LabelPartNumberPropertyName.Size = New System.Drawing.Size(139, 13)
        Me.LabelPartNumberPropertyName.TabIndex = 12
        Me.LabelPartNumberPropertyName.Text = "Part Number Property Name"
        '
        'ComboBoxPartNumberPropertySet
        '
        Me.ComboBoxPartNumberPropertySet.FormattingEnabled = True
        Me.ComboBoxPartNumberPropertySet.Items.AddRange(New Object() {"fake_item_1"})
        Me.ComboBoxPartNumberPropertySet.Location = New System.Drawing.Point(8, 268)
        Me.ComboBoxPartNumberPropertySet.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxPartNumberPropertySet.Name = "ComboBoxPartNumberPropertySet"
        Me.ComboBoxPartNumberPropertySet.Size = New System.Drawing.Size(132, 21)
        Me.ComboBoxPartNumberPropertySet.Sorted = True
        Me.ComboBoxPartNumberPropertySet.TabIndex = 11
        '
        'LabelPartNumberPropertySet
        '
        Me.LabelPartNumberPropertySet.AutoSize = True
        Me.LabelPartNumberPropertySet.Location = New System.Drawing.Point(8, 252)
        Me.LabelPartNumberPropertySet.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPartNumberPropertySet.Name = "LabelPartNumberPropertySet"
        Me.LabelPartNumberPropertySet.Size = New System.Drawing.Size(127, 13)
        Me.LabelPartNumberPropertySet.TabIndex = 10
        Me.LabelPartNumberPropertySet.Text = "Part Number Property Set"
        '
        'ButtonActiveMaterialLibrary
        '
        Me.ButtonActiveMaterialLibrary.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonActiveMaterialLibrary.Location = New System.Drawing.Point(377, 219)
        Me.ButtonActiveMaterialLibrary.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonActiveMaterialLibrary.Name = "ButtonActiveMaterialLibrary"
        Me.ButtonActiveMaterialLibrary.Size = New System.Drawing.Size(75, 20)
        Me.ButtonActiveMaterialLibrary.TabIndex = 9
        Me.ButtonActiveMaterialLibrary.Text = "Browse"
        Me.ButtonActiveMaterialLibrary.UseVisualStyleBackColor = True
        '
        'TextBoxActiveMaterialLibrary
        '
        Me.TextBoxActiveMaterialLibrary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxActiveMaterialLibrary.Location = New System.Drawing.Point(8, 219)
        Me.TextBoxActiveMaterialLibrary.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxActiveMaterialLibrary.Name = "TextBoxActiveMaterialLibrary"
        Me.TextBoxActiveMaterialLibrary.Size = New System.Drawing.Size(363, 20)
        Me.TextBoxActiveMaterialLibrary.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.TextBoxActiveMaterialLibrary, "Material Library")
        '
        'LabelActiveMaterialLibrary
        '
        Me.LabelActiveMaterialLibrary.AutoSize = True
        Me.LabelActiveMaterialLibrary.Location = New System.Drawing.Point(8, 203)
        Me.LabelActiveMaterialLibrary.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelActiveMaterialLibrary.Name = "LabelActiveMaterialLibrary"
        Me.LabelActiveMaterialLibrary.Size = New System.Drawing.Size(78, 13)
        Me.LabelActiveMaterialLibrary.TabIndex = 7
        Me.LabelActiveMaterialLibrary.Text = "Material Library"
        '
        'ButtonTemplateAssembly
        '
        Me.ButtonTemplateAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTemplateAssembly.Location = New System.Drawing.Point(377, 24)
        Me.ButtonTemplateAssembly.Name = "ButtonTemplateAssembly"
        Me.ButtonTemplateAssembly.Size = New System.Drawing.Size(75, 20)
        Me.ButtonTemplateAssembly.TabIndex = 3
        Me.ButtonTemplateAssembly.Text = "Browse"
        Me.ButtonTemplateAssembly.UseVisualStyleBackColor = True
        '
        'TextBoxTemplateAssembly
        '
        Me.TextBoxTemplateAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplateAssembly.Location = New System.Drawing.Point(8, 24)
        Me.TextBoxTemplateAssembly.Name = "TextBoxTemplateAssembly"
        Me.TextBoxTemplateAssembly.Size = New System.Drawing.Size(363, 20)
        Me.TextBoxTemplateAssembly.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TextBoxTemplateAssembly, "Assembly Template")
        '
        'LabelTemplateAssembly
        '
        Me.LabelTemplateAssembly.AutoSize = True
        Me.LabelTemplateAssembly.Location = New System.Drawing.Point(8, 8)
        Me.LabelTemplateAssembly.Name = "LabelTemplateAssembly"
        Me.LabelTemplateAssembly.Size = New System.Drawing.Size(117, 13)
        Me.LabelTemplateAssembly.TabIndex = 1
        Me.LabelTemplateAssembly.Text = "Assembly Template File"
        '
        'ButtonTemplatePart
        '
        Me.ButtonTemplatePart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTemplatePart.Location = New System.Drawing.Point(377, 73)
        Me.ButtonTemplatePart.Name = "ButtonTemplatePart"
        Me.ButtonTemplatePart.Size = New System.Drawing.Size(75, 20)
        Me.ButtonTemplatePart.TabIndex = 6
        Me.ButtonTemplatePart.Text = "Browse"
        Me.ButtonTemplatePart.UseVisualStyleBackColor = True
        '
        'TextBoxTemplatePart
        '
        Me.TextBoxTemplatePart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplatePart.Location = New System.Drawing.Point(8, 73)
        Me.TextBoxTemplatePart.Name = "TextBoxTemplatePart"
        Me.TextBoxTemplatePart.Size = New System.Drawing.Size(363, 20)
        Me.TextBoxTemplatePart.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.TextBoxTemplatePart, "Part Template")
        '
        'LabelTemplatePart
        '
        Me.LabelTemplatePart.AutoSize = True
        Me.LabelTemplatePart.Location = New System.Drawing.Point(8, 57)
        Me.LabelTemplatePart.Name = "LabelTemplatePart"
        Me.LabelTemplatePart.Size = New System.Drawing.Size(92, 13)
        Me.LabelTemplatePart.TabIndex = 4
        Me.LabelTemplatePart.Text = "Part Template File"
        '
        'ButtonTemplateSheetmetal
        '
        Me.ButtonTemplateSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTemplateSheetmetal.Location = New System.Drawing.Point(377, 122)
        Me.ButtonTemplateSheetmetal.Name = "ButtonTemplateSheetmetal"
        Me.ButtonTemplateSheetmetal.Size = New System.Drawing.Size(75, 20)
        Me.ButtonTemplateSheetmetal.TabIndex = 6
        Me.ButtonTemplateSheetmetal.Text = "Browse"
        Me.ButtonTemplateSheetmetal.UseVisualStyleBackColor = True
        '
        'TextBoxTemplateSheetmetal
        '
        Me.TextBoxTemplateSheetmetal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplateSheetmetal.Location = New System.Drawing.Point(8, 122)
        Me.TextBoxTemplateSheetmetal.Name = "TextBoxTemplateSheetmetal"
        Me.TextBoxTemplateSheetmetal.Size = New System.Drawing.Size(363, 20)
        Me.TextBoxTemplateSheetmetal.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.TextBoxTemplateSheetmetal, "Sheetmetal Template")
        '
        'LabelTemplateSheetmetal
        '
        Me.LabelTemplateSheetmetal.AutoSize = True
        Me.LabelTemplateSheetmetal.Location = New System.Drawing.Point(8, 106)
        Me.LabelTemplateSheetmetal.Name = "LabelTemplateSheetmetal"
        Me.LabelTemplateSheetmetal.Size = New System.Drawing.Size(126, 13)
        Me.LabelTemplateSheetmetal.TabIndex = 4
        Me.LabelTemplateSheetmetal.Text = "Sheetmetal Template File"
        '
        'ButtonTemplateDraft
        '
        Me.ButtonTemplateDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTemplateDraft.Location = New System.Drawing.Point(377, 171)
        Me.ButtonTemplateDraft.Name = "ButtonTemplateDraft"
        Me.ButtonTemplateDraft.Size = New System.Drawing.Size(75, 20)
        Me.ButtonTemplateDraft.TabIndex = 6
        Me.ButtonTemplateDraft.Text = "Browse"
        Me.ButtonTemplateDraft.UseVisualStyleBackColor = True
        '
        'TextBoxTemplateDraft
        '
        Me.TextBoxTemplateDraft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplateDraft.Location = New System.Drawing.Point(8, 171)
        Me.TextBoxTemplateDraft.Name = "TextBoxTemplateDraft"
        Me.TextBoxTemplateDraft.Size = New System.Drawing.Size(363, 20)
        Me.TextBoxTemplateDraft.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.TextBoxTemplateDraft, "Draft Template")
        '
        'LabelTemplateDraft
        '
        Me.LabelTemplateDraft.AutoSize = True
        Me.LabelTemplateDraft.Location = New System.Drawing.Point(8, 154)
        Me.LabelTemplateDraft.Name = "LabelTemplateDraft"
        Me.LabelTemplateDraft.Size = New System.Drawing.Size(96, 13)
        Me.LabelTemplateDraft.TabIndex = 4
        Me.LabelTemplateDraft.Text = "Draft Template File"
        '
        'TabPageReadme
        '
        Me.TabPageReadme.AutoScroll = True
        Me.TabPageReadme.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageReadme.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageReadme.Controls.Add(Me.TextBoxReadme)
        Me.TabPageReadme.ImageKey = "Info"
        Me.TabPageReadme.Location = New System.Drawing.Point(4, 23)
        Me.TabPageReadme.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageReadme.Name = "TabPageReadme"
        Me.TabPageReadme.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageReadme.Size = New System.Drawing.Size(511, 594)
        Me.TabPageReadme.TabIndex = 6
        Me.TabPageReadme.Text = "Readme"
        '
        'TextBoxReadme
        '
        Me.TextBoxReadme.BackColor = System.Drawing.SystemColors.Window
        Me.TextBoxReadme.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxReadme.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxReadme.Location = New System.Drawing.Point(2, 2)
        Me.TextBoxReadme.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBoxReadme.Multiline = True
        Me.TextBoxReadme.Name = "TextBoxReadme"
        Me.TextBoxReadme.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxReadme.Size = New System.Drawing.Size(505, 588)
        Me.TextBoxReadme.TabIndex = 0
        Me.TextBoxReadme.Text = "Populated at build time."
        '
        'TextBoxStatus
        '
        Me.TextBoxStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStatus.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel1.SetColumnSpan(Me.TextBoxStatus, 3)
        Me.TextBoxStatus.Location = New System.Drawing.Point(2, 2)
        Me.TextBoxStatus.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxStatus.Name = "TextBoxStatus"
        Me.TextBoxStatus.Size = New System.Drawing.Size(515, 20)
        Me.TextBoxStatus.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.TextBoxStatus, "Status")
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ButtonCancel.Image = CType(resources.GetObject("ButtonCancel.Image"), System.Drawing.Image)
        Me.ButtonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCancel.Location = New System.Drawing.Point(421, 26)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(96, 27)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonProcess
        '
        Me.ButtonProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonProcess.Image = CType(resources.GetObject("ButtonProcess.Image"), System.Drawing.Image)
        Me.ButtonProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonProcess.Location = New System.Drawing.Point(321, 26)
        Me.ButtonProcess.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonProcess.Name = "ButtonProcess"
        Me.ButtonProcess.Size = New System.Drawing.Size(96, 27)
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
        Me.LabelTimeRemaining.Location = New System.Drawing.Point(4, 641)
        Me.LabelTimeRemaining.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelTimeRemaining.Name = "LabelTimeRemaining"
        Me.LabelTimeRemaining.Size = New System.Drawing.Size(0, 13)
        Me.LabelTimeRemaining.TabIndex = 4
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonProcess, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonCancel, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxStatus, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 621)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(519, 55)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 23)
        '
        'Menu_ListViewFile
        '
        Me.Menu_ListViewFile.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BT_Open, Me.BT_OpenFolder, Me.ToolStripSeparator5, Me.BT_Remove})
        Me.Menu_ListViewFile.Name = "Menu_ListViewFile"
        Me.Menu_ListViewFile.Size = New System.Drawing.Size(165, 76)
        '
        'BT_Open
        '
        Me.BT_Open.Image = CType(resources.GetObject("BT_Open.Image"), System.Drawing.Image)
        Me.BT_Open.Name = "BT_Open"
        Me.BT_Open.Size = New System.Drawing.Size(164, 22)
        Me.BT_Open.Text = "Open"
        '
        'BT_OpenFolder
        '
        Me.BT_OpenFolder.Image = CType(resources.GetObject("BT_OpenFolder.Image"), System.Drawing.Image)
        Me.BT_OpenFolder.Name = "BT_OpenFolder"
        Me.BT_OpenFolder.Size = New System.Drawing.Size(164, 22)
        Me.BT_OpenFolder.Text = "Open folder"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(161, 6)
        '
        'BT_Remove
        '
        Me.BT_Remove.Image = CType(resources.GetObject("BT_Remove.Image"), System.Drawing.Image)
        Me.BT_Remove.Name = "BT_Remove"
        Me.BT_Remove.Size = New System.Drawing.Size(164, 22)
        Me.BT_Remove.Text = "Remove from list"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(519, 676)
        Me.Controls.Add(Me.LabelTimeRemaining)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(535, 715)
        Me.Name = "Form1"
        Me.Text = "Solid Edge Housekeeper"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGeneral.ResumeLayout(False)
        Me.TabPageGeneral.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
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
        Me.GroupBoxPictorialViews.ResumeLayout(False)
        Me.GroupBoxPictorialViews.PerformLayout()
        Me.GroupBoxTLAOptions.ResumeLayout(False)
        Me.GroupBoxTLAOptions.PerformLayout()
        Me.TabPageReadme.ResumeLayout(False)
        Me.TabPageReadme.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Menu_ListViewFile.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageGeneral As TabPage
    Friend WithEvents TabPageAssembly As TabPage
    Friend WithEvents TabPagePart As TabPage
    Friend WithEvents TabPageSheetmetal As TabPage
    Friend WithEvents TabPageDraft As TabPage
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
    Friend WithEvents LabelTimeRemaining As Label
    Friend WithEvents LabelListboxFiles As Label
    Friend WithEvents ButtonPropertyFilter As Button
    Friend WithEvents CheckBoxSaveAsAssemblyOutputDirectory As CheckBox
    Friend WithEvents CheckBoxSaveAsPartOutputDirectory As CheckBox
    Friend WithEvents CheckBoxSaveAsSheetmetalOutputDirectory As CheckBox
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
    Friend WithEvents CheckBoxFilterDft As CheckBox
    Friend WithEvents CheckBoxFilterPsm As CheckBox
    Friend WithEvents CheckBoxFilterPar As CheckBox
    Friend WithEvents CheckBoxFilterAsm As CheckBox
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
    Friend WithEvents TextBoxPrintOptionsPrinter As TextBox
    Friend WithEvents TextBoxPrintOptionsHeight As TextBox
    Friend WithEvents TextBoxPrintOptionsWidth As TextBox
    Friend WithEvents TextBoxPrintOptionsCopies As TextBox
    Friend WithEvents CheckBoxPrintOptionsScaleLineWidths As CheckBox
    Friend WithEvents CheckBoxPrintOptionsScaleLineTypes As CheckBox
    Friend WithEvents CheckBoxPrintOptionsPrintAsBlack As CheckBox
    Friend WithEvents CheckBoxPrintOptionsBestFit As CheckBox
    Friend WithEvents CheckBoxPrintOptionsAutoOrient As CheckBox
    Friend WithEvents LabelPrintOptionsCopies As Label
    Friend WithEvents LabelPrintOptionsHeight As Label
    Friend WithEvents LabelPrintOptionsWidth As Label
    Friend WithEvents LabelPrintOptionsPrinter As Label
    Friend WithEvents TextBoxWatermarkScale As TextBox
    Friend WithEvents LabelWatermarkScale As Label
    Friend WithEvents TextBoxWatermarkY As TextBox
    Friend WithEvents LabelWatermarkY As Label
    Friend WithEvents TextBoxWatermarkX As TextBox
    Friend WithEvents LabelWatermarkX As Label
    Friend WithEvents CheckBoxWatermark As CheckBox
    Friend WithEvents ButtonWatermark As Button
    Friend WithEvents TextBoxWatermarkFilename As TextBox
    Friend WithEvents GroupBoxPictorialViews As GroupBox
    Friend WithEvents RadioButtonPictorialViewTrimetric As RadioButton
    Friend WithEvents RadioButtonPictorialViewDimetric As RadioButton
    Friend WithEvents RadioButtonPictorialViewIsometric As RadioButton
    Friend WithEvents CheckBoxRunExternalProgramSaveFile As CheckBox
    Friend WithEvents ButtonFileSearch As Button
    Friend WithEvents ComboBoxFileSearch As ComboBox
    Friend WithEvents CheckBoxBackgroundProcessing As CheckBox
    Friend WithEvents TextBoxExposeVariablesAssembly As TextBox
    Friend WithEvents LabelExposeVariablesAssembly As Label
    Friend WithEvents TextBoxExposeVariablesPart As TextBox
    Friend WithEvents LabelExposeVariablesPart As Label
    Friend WithEvents TextBoxExposeVariablesSheetmetal As TextBox
    Friend WithEvents LabelExposeVariablesSheetmetal As Label
    Friend WithEvents CheckBoxSaveAsImageCrop As CheckBox
    Friend WithEvents TextBoxSaveAsFormulaSheetmetal As TextBox
    Friend WithEvents CheckBoxSaveAsFormulaSheetmetal As CheckBox
    Friend WithEvents TextBoxSaveAsFormulaAssembly As TextBox
    Friend WithEvents CheckBoxSaveAsFormulaAssembly As CheckBox
    Friend WithEvents TextBoxSaveAsFormulaPart As TextBox
    Friend WithEvents CheckBoxSaveAsFormulaPart As CheckBox
    Friend WithEvents TextBoxSaveAsFormulaDraft As TextBox
    Friend WithEvents CheckBoxSaveAsFormulaDraft As CheckBox
    Friend WithEvents TabPage_ImageList As ImageList
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ListViewFiles As ListView
    Friend WithEvents FileName As ColumnHeader
    Friend WithEvents FilePath As ColumnHeader
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents BT_AddFolder As ToolStripButton
    Friend WithEvents BT_AddFolderSubfolders As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents BT_AddFromlist As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents BT_Update As ToolStripButton
    Friend WithEvents BT_DeleteAll As ToolStripButton
    Friend WithEvents BT_TopLevelAsm As ToolStripButton
    Friend WithEvents BT_ExportList As ToolStripButton
    Friend WithEvents new_CheckBoxFilterPar As ToolStripButton
    Friend WithEvents new_CheckBoxFilterDft As ToolStripButton
    Friend WithEvents new_CheckBoxFilterPsm As ToolStripButton
    Friend WithEvents new_CheckBoxFilterAsm As ToolStripButton
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents BT_ASM_Folder As ToolStripButton
    Friend WithEvents BT_ErrorList As ToolStripButton
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents Menu_ListViewFile As ContextMenuStrip
    Friend WithEvents BT_Open As ToolStripMenuItem
    Friend WithEvents BT_OpenFolder As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents BT_Remove As ToolStripMenuItem
End Class
