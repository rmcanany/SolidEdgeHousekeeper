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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGeneral = New System.Windows.Forms.TabPage()
        Me.ListViewFiles = New System.Windows.Forms.ListView()
        Me.FileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FilePath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage_ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolStrip_Filter = New System.Windows.Forms.ToolStrip()
        Me.new_CheckBoxEnablePropertyFilter = New System.Windows.Forms.ToolStripButton()
        Me.new_ButtonPropertyFilter = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.new_CheckBoxFileSearch = New System.Windows.Forms.ToolStripButton()
        Me.new_ComboBoxFileSearch = New System.Windows.Forms.ToolStripComboBox()
        Me.new_ButtonFileSearchDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStrip_List = New System.Windows.Forms.ToolStrip()
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
        Me.LabelFontSize = New System.Windows.Forms.Label()
        Me.TextBoxFontSize = New System.Windows.Forms.TextBox()
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
        Me.LinkLabelGitHubReadme = New System.Windows.Forms.LinkLabel()
        Me.PictureBoxReadmeNavigation = New System.Windows.Forms.PictureBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.TextBoxStatus = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.FakeFolderBrowserDialog = New System.Windows.Forms.OpenFileDialog()
        Me.LabelTimeRemaining = New System.Windows.Forms.Label()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonProcess = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.Menu_ListViewFile = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BT_Open = New System.Windows.Forms.ToolStripMenuItem()
        Me.BT_OpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_FindLinks = New System.Windows.Forms.ToolStripMenuItem()
        Me.BT_ProcessSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_Remove = New System.Windows.Forms.ToolStripMenuItem()
        Me.LabelReadmeNavigation = New System.Windows.Forms.Label()
        Me.LabelVersion = New System.Windows.Forms.Label()
        Me.TextBoxVersion = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGeneral.SuspendLayout()
        Me.ToolStrip_Filter.SuspendLayout()
        Me.ToolStrip_List.SuspendLayout()
        Me.TabPageAssembly.SuspendLayout()
        Me.TabPagePart.SuspendLayout()
        Me.TabPageSheetmetal.SuspendLayout()
        Me.TabPageDraft.SuspendLayout()
        Me.TabPageConfiguration.SuspendLayout()
        Me.GroupBoxPictorialViews.SuspendLayout()
        Me.GroupBoxTLAOptions.SuspendLayout()
        Me.TabPageReadme.SuspendLayout()
        CType(Me.PictureBoxReadmeNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TabPageGeneral.Controls.Add(Me.ListViewFiles)
        Me.TabPageGeneral.Controls.Add(Me.ToolStrip_Filter)
        Me.TabPageGeneral.Controls.Add(Me.ToolStrip_List)
        Me.TabPageGeneral.ImageKey = "list"
        Me.TabPageGeneral.Location = New System.Drawing.Point(4, 23)
        Me.TabPageGeneral.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageGeneral.Name = "TabPageGeneral"
        Me.TabPageGeneral.Padding = New System.Windows.Forms.Padding(2, 2, 2, 0)
        Me.TabPageGeneral.Size = New System.Drawing.Size(511, 594)
        Me.TabPageGeneral.TabIndex = 0
        Me.TabPageGeneral.Text = "General"
        '
        'ListViewFiles
        '
        Me.ListViewFiles.AllowDrop = True
        Me.ListViewFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListViewFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FileName, Me.FilePath})
        Me.ListViewFiles.Cursor = System.Windows.Forms.Cursors.Default
        Me.ListViewFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewFiles.FullRowSelect = True
        Me.ListViewFiles.GridLines = True
        Me.ListViewFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListViewFiles.HideSelection = False
        Me.ListViewFiles.Location = New System.Drawing.Point(2, 27)
        Me.ListViewFiles.Name = "ListViewFiles"
        Me.ListViewFiles.Size = New System.Drawing.Size(505, 540)
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
        Me.TabPage_ImageList.Images.SetKeyName(18, "list")
        Me.TabPage_ImageList.Images.SetKeyName(19, "Tools")
        '
        'ToolStrip_Filter
        '
        Me.ToolStrip_Filter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip_Filter.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip_Filter.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.new_CheckBoxEnablePropertyFilter, Me.new_ButtonPropertyFilter, Me.ToolStripSeparator6, Me.new_CheckBoxFileSearch, Me.new_ComboBoxFileSearch, Me.new_ButtonFileSearchDelete, Me.ToolStripSeparator7})
        Me.ToolStrip_Filter.Location = New System.Drawing.Point(2, 567)
        Me.ToolStrip_Filter.Name = "ToolStrip_Filter"
        Me.ToolStrip_Filter.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip_Filter.Size = New System.Drawing.Size(505, 25)
        Me.ToolStrip_Filter.TabIndex = 34
        Me.ToolStrip_Filter.Text = "ToolStrip1"
        '
        'new_CheckBoxEnablePropertyFilter
        '
        Me.new_CheckBoxEnablePropertyFilter.CheckOnClick = True
        Me.new_CheckBoxEnablePropertyFilter.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.new_CheckBoxEnablePropertyFilter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.new_CheckBoxEnablePropertyFilter.Name = "new_CheckBoxEnablePropertyFilter"
        Me.new_CheckBoxEnablePropertyFilter.Size = New System.Drawing.Size(99, 22)
        Me.new_CheckBoxEnablePropertyFilter.Text = "Property filter"
        '
        'new_ButtonPropertyFilter
        '
        Me.new_ButtonPropertyFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.new_ButtonPropertyFilter.Image = Global.Housekeeper.My.Resources.Resources.Tools
        Me.new_ButtonPropertyFilter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.new_ButtonPropertyFilter.Name = "new_ButtonPropertyFilter"
        Me.new_ButtonPropertyFilter.Size = New System.Drawing.Size(23, 22)
        Me.new_ButtonPropertyFilter.Text = "Configure"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'new_CheckBoxFileSearch
        '
        Me.new_CheckBoxFileSearch.CheckOnClick = True
        Me.new_CheckBoxFileSearch.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.new_CheckBoxFileSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.new_CheckBoxFileSearch.Name = "new_CheckBoxFileSearch"
        Me.new_CheckBoxFileSearch.Size = New System.Drawing.Size(82, 22)
        Me.new_CheckBoxFileSearch.Text = "File search"
        '
        'new_ComboBoxFileSearch
        '
        Me.new_ComboBoxFileSearch.Name = "new_ComboBoxFileSearch"
        Me.new_ComboBoxFileSearch.Size = New System.Drawing.Size(121, 25)
        Me.new_ComboBoxFileSearch.Sorted = True
        '
        'new_ButtonFileSearchDelete
        '
        Me.new_ButtonFileSearchDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.new_ButtonFileSearchDelete.Image = Global.Housekeeper.My.Resources.Resources.Cancel
        Me.new_ButtonFileSearchDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.new_ButtonFileSearchDelete.Name = "new_ButtonFileSearchDelete"
        Me.new_ButtonFileSearchDelete.Size = New System.Drawing.Size(23, 22)
        Me.new_ButtonFileSearchDelete.Text = "ToolStripButton5"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStrip_List
        '
        Me.ToolStrip_List.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip_List.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BT_AddFolder, Me.BT_AddFolderSubfolders, Me.ToolStripSeparator4, Me.BT_TopLevelAsm, Me.BT_ASM_Folder, Me.ToolStripSeparator1, Me.BT_AddFromlist, Me.BT_ExportList, Me.ToolStripSeparator2, Me.BT_ErrorList, Me.BT_DeleteAll, Me.ToolStripSeparator3, Me.BT_Update, Me.new_CheckBoxFilterDft, Me.new_CheckBoxFilterPsm, Me.new_CheckBoxFilterPar, Me.new_CheckBoxFilterAsm, Me.ToolStripLabel1})
        Me.ToolStrip_List.Location = New System.Drawing.Point(2, 2)
        Me.ToolStrip_List.Name = "ToolStrip_List"
        Me.ToolStrip_List.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip_List.Size = New System.Drawing.Size(505, 25)
        Me.ToolStrip_List.TabIndex = 33
        Me.ToolStrip_List.Text = "ToolStrip1"
        '
        'BT_AddFolder
        '
        Me.BT_AddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_AddFolder.Image = Global.Housekeeper.My.Resources.Resources.folder
        Me.BT_AddFolder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_AddFolder.Name = "BT_AddFolder"
        Me.BT_AddFolder.Size = New System.Drawing.Size(23, 22)
        Me.BT_AddFolder.Text = "Add single folder"
        '
        'BT_AddFolderSubfolders
        '
        Me.BT_AddFolderSubfolders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_AddFolderSubfolders.Image = Global.Housekeeper.My.Resources.Resources.folders
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
        Me.BT_TopLevelAsm.Image = Global.Housekeeper.My.Resources.Resources.asm
        Me.BT_TopLevelAsm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_TopLevelAsm.Name = "BT_TopLevelAsm"
        Me.BT_TopLevelAsm.Size = New System.Drawing.Size(23, 22)
        Me.BT_TopLevelAsm.Text = "Top level asm"
        '
        'BT_ASM_Folder
        '
        Me.BT_ASM_Folder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_ASM_Folder.Image = Global.Housekeeper.My.Resources.Resources.ASM_Folder
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
        Me.BT_AddFromlist.Image = Global.Housekeeper.My.Resources.Resources.Import
        Me.BT_AddFromlist.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_AddFromlist.Name = "BT_AddFromlist"
        Me.BT_AddFromlist.Size = New System.Drawing.Size(23, 22)
        Me.BT_AddFromlist.Text = "Add files from a list"
        Me.BT_AddFromlist.ToolTipText = "Add files from a list"
        '
        'BT_ExportList
        '
        Me.BT_ExportList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_ExportList.Image = Global.Housekeeper.My.Resources.Resources.Export
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
        Me.BT_ErrorList.Image = Global.Housekeeper.My.Resources.Resources.Errore
        Me.BT_ErrorList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_ErrorList.Name = "BT_ErrorList"
        Me.BT_ErrorList.Size = New System.Drawing.Size(23, 22)
        Me.BT_ErrorList.Text = "Show only file with errors"
        '
        'BT_DeleteAll
        '
        Me.BT_DeleteAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_DeleteAll.Image = Global.Housekeeper.My.Resources.Resources.delete
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
        Me.BT_Update.Image = Global.Housekeeper.My.Resources.Resources.Update
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
        Me.new_CheckBoxFilterDft.Image = Global.Housekeeper.My.Resources.Resources.dft
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
        Me.new_CheckBoxFilterPsm.Image = Global.Housekeeper.My.Resources.Resources.psm
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
        Me.new_CheckBoxFilterPar.Image = Global.Housekeeper.My.Resources.Resources.par
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
        Me.new_CheckBoxFilterAsm.Image = Global.Housekeeper.My.Resources.Resources.asm
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
        Me.TabPageConfiguration.Controls.Add(Me.LabelFontSize)
        Me.TabPageConfiguration.Controls.Add(Me.TextBoxFontSize)
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
        Me.TabPageConfiguration.ImageKey = "Tools"
        Me.TabPageConfiguration.Location = New System.Drawing.Point(4, 23)
        Me.TabPageConfiguration.Name = "TabPageConfiguration"
        Me.TabPageConfiguration.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageConfiguration.Size = New System.Drawing.Size(511, 594)
        Me.TabPageConfiguration.TabIndex = 5
        Me.TabPageConfiguration.Text = "Configuration"
        '
        'LabelFontSize
        '
        Me.LabelFontSize.AutoSize = True
        Me.LabelFontSize.Location = New System.Drawing.Point(8, 305)
        Me.LabelFontSize.Name = "LabelFontSize"
        Me.LabelFontSize.Size = New System.Drawing.Size(95, 13)
        Me.LabelFontSize.TabIndex = 39
        Me.LabelFontSize.Text = "Font size for file list"
        '
        'TextBoxFontSize
        '
        Me.TextBoxFontSize.Location = New System.Drawing.Point(8, 321)
        Me.TextBoxFontSize.Name = "TextBoxFontSize"
        Me.TextBoxFontSize.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFontSize.TabIndex = 38
        Me.TextBoxFontSize.Text = "8"
        '
        'CheckBoxSaveAsImageCrop
        '
        Me.CheckBoxSaveAsImageCrop.AutoSize = True
        Me.CheckBoxSaveAsImageCrop.Location = New System.Drawing.Point(8, 680)
        Me.CheckBoxSaveAsImageCrop.Name = "CheckBoxSaveAsImageCrop"
        Me.CheckBoxSaveAsImageCrop.Size = New System.Drawing.Size(194, 17)
        Me.CheckBoxSaveAsImageCrop.TabIndex = 37
        Me.CheckBoxSaveAsImageCrop.Text = "Save as image -- Crop to model size"
        Me.CheckBoxSaveAsImageCrop.UseVisualStyleBackColor = True
        '
        'CheckBoxBackgroundProcessing
        '
        Me.CheckBoxBackgroundProcessing.AutoSize = True
        Me.CheckBoxBackgroundProcessing.Location = New System.Drawing.Point(8, 606)
        Me.CheckBoxBackgroundProcessing.Name = "CheckBoxBackgroundProcessing"
        Me.CheckBoxBackgroundProcessing.Size = New System.Drawing.Size(227, 17)
        Me.CheckBoxBackgroundProcessing.TabIndex = 36
        Me.CheckBoxBackgroundProcessing.Text = "Process tasks in background (no graphics)"
        Me.CheckBoxBackgroundProcessing.UseVisualStyleBackColor = True
        '
        'CheckBoxRunExternalProgramSaveFile
        '
        Me.CheckBoxRunExternalProgramSaveFile.AutoSize = True
        Me.CheckBoxRunExternalProgramSaveFile.Location = New System.Drawing.Point(8, 656)
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
        Me.GroupBoxPictorialViews.Location = New System.Drawing.Point(11, 1000)
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
        Me.LabelPrintOptionsCopies.Location = New System.Drawing.Point(11, 844)
        Me.LabelPrintOptionsCopies.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPrintOptionsCopies.Name = "LabelPrintOptionsCopies"
        Me.LabelPrintOptionsCopies.Size = New System.Drawing.Size(39, 13)
        Me.LabelPrintOptionsCopies.TabIndex = 33
        Me.LabelPrintOptionsCopies.Text = "Copies"
        '
        'LabelPrintOptionsHeight
        '
        Me.LabelPrintOptionsHeight.AutoSize = True
        Me.LabelPrintOptionsHeight.Location = New System.Drawing.Point(112, 803)
        Me.LabelPrintOptionsHeight.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPrintOptionsHeight.Name = "LabelPrintOptionsHeight"
        Me.LabelPrintOptionsHeight.Size = New System.Drawing.Size(55, 13)
        Me.LabelPrintOptionsHeight.TabIndex = 32
        Me.LabelPrintOptionsHeight.Text = "Height (in)"
        '
        'LabelPrintOptionsWidth
        '
        Me.LabelPrintOptionsWidth.AutoSize = True
        Me.LabelPrintOptionsWidth.Location = New System.Drawing.Point(11, 803)
        Me.LabelPrintOptionsWidth.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPrintOptionsWidth.Name = "LabelPrintOptionsWidth"
        Me.LabelPrintOptionsWidth.Size = New System.Drawing.Size(52, 13)
        Me.LabelPrintOptionsWidth.TabIndex = 31
        Me.LabelPrintOptionsWidth.Text = "Width (in)"
        '
        'LabelPrintOptionsPrinter
        '
        Me.LabelPrintOptionsPrinter.AutoSize = True
        Me.LabelPrintOptionsPrinter.Location = New System.Drawing.Point(11, 763)
        Me.LabelPrintOptionsPrinter.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPrintOptionsPrinter.Name = "LabelPrintOptionsPrinter"
        Me.LabelPrintOptionsPrinter.Size = New System.Drawing.Size(37, 13)
        Me.LabelPrintOptionsPrinter.TabIndex = 30
        Me.LabelPrintOptionsPrinter.Text = "Printer"
        '
        'CheckBoxPrintOptionsScaleLineWidths
        '
        Me.CheckBoxPrintOptionsScaleLineWidths.AutoSize = True
        Me.CheckBoxPrintOptionsScaleLineWidths.Location = New System.Drawing.Point(11, 971)
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
        Me.CheckBoxPrintOptionsScaleLineTypes.Location = New System.Drawing.Point(11, 950)
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
        Me.CheckBoxPrintOptionsPrintAsBlack.Location = New System.Drawing.Point(11, 930)
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
        Me.CheckBoxPrintOptionsBestFit.Location = New System.Drawing.Point(11, 910)
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
        Me.CheckBoxPrintOptionsAutoOrient.Location = New System.Drawing.Point(11, 890)
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
        Me.TextBoxPrintOptionsCopies.Location = New System.Drawing.Point(11, 860)
        Me.TextBoxPrintOptionsCopies.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPrintOptionsCopies.Name = "TextBoxPrintOptionsCopies"
        Me.TextBoxPrintOptionsCopies.Size = New System.Drawing.Size(76, 20)
        Me.TextBoxPrintOptionsCopies.TabIndex = 24
        '
        'TextBoxPrintOptionsHeight
        '
        Me.TextBoxPrintOptionsHeight.Enabled = False
        Me.TextBoxPrintOptionsHeight.Location = New System.Drawing.Point(112, 819)
        Me.TextBoxPrintOptionsHeight.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPrintOptionsHeight.Name = "TextBoxPrintOptionsHeight"
        Me.TextBoxPrintOptionsHeight.Size = New System.Drawing.Size(76, 20)
        Me.TextBoxPrintOptionsHeight.TabIndex = 23
        '
        'TextBoxPrintOptionsWidth
        '
        Me.TextBoxPrintOptionsWidth.Enabled = False
        Me.TextBoxPrintOptionsWidth.Location = New System.Drawing.Point(11, 819)
        Me.TextBoxPrintOptionsWidth.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPrintOptionsWidth.Name = "TextBoxPrintOptionsWidth"
        Me.TextBoxPrintOptionsWidth.Size = New System.Drawing.Size(76, 20)
        Me.TextBoxPrintOptionsWidth.TabIndex = 22
        '
        'TextBoxPrintOptionsPrinter
        '
        Me.TextBoxPrintOptionsPrinter.Enabled = False
        Me.TextBoxPrintOptionsPrinter.Location = New System.Drawing.Point(11, 779)
        Me.TextBoxPrintOptionsPrinter.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPrintOptionsPrinter.Name = "TextBoxPrintOptionsPrinter"
        Me.TextBoxPrintOptionsPrinter.Size = New System.Drawing.Size(200, 20)
        Me.TextBoxPrintOptionsPrinter.TabIndex = 21
        '
        'ButtonPrintOptions
        '
        Me.ButtonPrintOptions.Location = New System.Drawing.Point(8, 738)
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
        Me.CheckBoxRememberTasks.Location = New System.Drawing.Point(8, 581)
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
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Location = New System.Drawing.Point(8, 631)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Name = "CheckBoxMoveDrawingViewAllowPartialSuccess"
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Size = New System.Drawing.Size(266, 17)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.TabIndex = 18
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Text = "Update styles from template -- Allow partial success"
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.UseVisualStyleBackColor = True
        '
        'GroupBoxTLAOptions
        '
        Me.GroupBoxTLAOptions.Controls.Add(Me.ButtonFastSearchScopeFilename)
        Me.GroupBoxTLAOptions.Controls.Add(Me.TextBoxFastSearchScopeFilename)
        Me.GroupBoxTLAOptions.Controls.Add(Me.LabelFastSearchScopeFilename)
        Me.GroupBoxTLAOptions.Controls.Add(Me.CheckBoxTLAReportUnrelatedFiles)
        Me.GroupBoxTLAOptions.Controls.Add(Me.RadioButtonTLATopDown)
        Me.GroupBoxTLAOptions.Controls.Add(Me.RadioButtonTLABottomUp)
        Me.GroupBoxTLAOptions.Location = New System.Drawing.Point(8, 402)
        Me.GroupBoxTLAOptions.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBoxTLAOptions.Name = "GroupBoxTLAOptions"
        Me.GroupBoxTLAOptions.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBoxTLAOptions.Size = New System.Drawing.Size(475, 140)
        Me.GroupBoxTLAOptions.TabIndex = 17
        Me.GroupBoxTLAOptions.TabStop = False
        Me.GroupBoxTLAOptions.Text = "Top level assembly processing options -- See Readme for details"
        '
        'ButtonFastSearchScopeFilename
        '
        Me.ButtonFastSearchScopeFilename.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonFastSearchScopeFilename.Location = New System.Drawing.Point(379, 97)
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
        Me.TextBoxFastSearchScopeFilename.Size = New System.Drawing.Size(355, 20)
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
        Me.CheckBoxWarnSave.Location = New System.Drawing.Point(8, 557)
        Me.CheckBoxWarnSave.Name = "CheckBoxWarnSave"
        Me.CheckBoxWarnSave.Size = New System.Drawing.Size(170, 17)
        Me.CheckBoxWarnSave.TabIndex = 16
        Me.CheckBoxWarnSave.Text = "Warn me if file save is required"
        Me.CheckBoxWarnSave.UseVisualStyleBackColor = True
        '
        'TextBoxRestartAfter
        '
        Me.TextBoxRestartAfter.Location = New System.Drawing.Point(8, 371)
        Me.TextBoxRestartAfter.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxRestartAfter.Name = "TextBoxRestartAfter"
        Me.TextBoxRestartAfter.Size = New System.Drawing.Size(76, 20)
        Me.TextBoxRestartAfter.TabIndex = 15
        Me.TextBoxRestartAfter.Text = "50"
        '
        'LabelRestartAfter
        '
        Me.LabelRestartAfter.AutoSize = True
        Me.LabelRestartAfter.Location = New System.Drawing.Point(8, 355)
        Me.LabelRestartAfter.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelRestartAfter.Name = "LabelRestartAfter"
        Me.LabelRestartAfter.Size = New System.Drawing.Size(195, 13)
        Me.LabelRestartAfter.TabIndex = 14
        Me.LabelRestartAfter.Text = "Restart After This Many Files Processed"
        '
        'TextBoxPartNumberPropertyName
        '
        Me.TextBoxPartNumberPropertyName.Location = New System.Drawing.Point(172, 268)
        Me.TextBoxPartNumberPropertyName.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPartNumberPropertyName.Name = "TextBoxPartNumberPropertyName"
        Me.TextBoxPartNumberPropertyName.Size = New System.Drawing.Size(200, 20)
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
        Me.ButtonActiveMaterialLibrary.Location = New System.Drawing.Point(410, 219)
        Me.ButtonActiveMaterialLibrary.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonActiveMaterialLibrary.Name = "ButtonActiveMaterialLibrary"
        Me.ButtonActiveMaterialLibrary.Size = New System.Drawing.Size(75, 20)
        Me.ButtonActiveMaterialLibrary.TabIndex = 9
        Me.ButtonActiveMaterialLibrary.Text = "Browse"
        Me.ButtonActiveMaterialLibrary.UseVisualStyleBackColor = True
        '
        'TextBoxActiveMaterialLibrary
        '
        Me.TextBoxActiveMaterialLibrary.Location = New System.Drawing.Point(8, 219)
        Me.TextBoxActiveMaterialLibrary.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxActiveMaterialLibrary.Name = "TextBoxActiveMaterialLibrary"
        Me.TextBoxActiveMaterialLibrary.Size = New System.Drawing.Size(390, 20)
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
        Me.ButtonTemplateAssembly.Location = New System.Drawing.Point(410, 24)
        Me.ButtonTemplateAssembly.Name = "ButtonTemplateAssembly"
        Me.ButtonTemplateAssembly.Size = New System.Drawing.Size(75, 20)
        Me.ButtonTemplateAssembly.TabIndex = 3
        Me.ButtonTemplateAssembly.Text = "Browse"
        Me.ButtonTemplateAssembly.UseVisualStyleBackColor = True
        '
        'TextBoxTemplateAssembly
        '
        Me.TextBoxTemplateAssembly.Location = New System.Drawing.Point(8, 24)
        Me.TextBoxTemplateAssembly.Name = "TextBoxTemplateAssembly"
        Me.TextBoxTemplateAssembly.Size = New System.Drawing.Size(390, 20)
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
        Me.ButtonTemplatePart.Location = New System.Drawing.Point(410, 73)
        Me.ButtonTemplatePart.Name = "ButtonTemplatePart"
        Me.ButtonTemplatePart.Size = New System.Drawing.Size(75, 20)
        Me.ButtonTemplatePart.TabIndex = 6
        Me.ButtonTemplatePart.Text = "Browse"
        Me.ButtonTemplatePart.UseVisualStyleBackColor = True
        '
        'TextBoxTemplatePart
        '
        Me.TextBoxTemplatePart.Location = New System.Drawing.Point(8, 73)
        Me.TextBoxTemplatePart.Name = "TextBoxTemplatePart"
        Me.TextBoxTemplatePart.Size = New System.Drawing.Size(390, 20)
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
        Me.ButtonTemplateSheetmetal.Location = New System.Drawing.Point(410, 122)
        Me.ButtonTemplateSheetmetal.Name = "ButtonTemplateSheetmetal"
        Me.ButtonTemplateSheetmetal.Size = New System.Drawing.Size(75, 20)
        Me.ButtonTemplateSheetmetal.TabIndex = 6
        Me.ButtonTemplateSheetmetal.Text = "Browse"
        Me.ButtonTemplateSheetmetal.UseVisualStyleBackColor = True
        '
        'TextBoxTemplateSheetmetal
        '
        Me.TextBoxTemplateSheetmetal.Location = New System.Drawing.Point(8, 122)
        Me.TextBoxTemplateSheetmetal.Name = "TextBoxTemplateSheetmetal"
        Me.TextBoxTemplateSheetmetal.Size = New System.Drawing.Size(390, 20)
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
        Me.ButtonTemplateDraft.Location = New System.Drawing.Point(410, 171)
        Me.ButtonTemplateDraft.Name = "ButtonTemplateDraft"
        Me.ButtonTemplateDraft.Size = New System.Drawing.Size(75, 20)
        Me.ButtonTemplateDraft.TabIndex = 6
        Me.ButtonTemplateDraft.Text = "Browse"
        Me.ButtonTemplateDraft.UseVisualStyleBackColor = True
        '
        'TextBoxTemplateDraft
        '
        Me.TextBoxTemplateDraft.Location = New System.Drawing.Point(8, 171)
        Me.TextBoxTemplateDraft.Name = "TextBoxTemplateDraft"
        Me.TextBoxTemplateDraft.Size = New System.Drawing.Size(390, 20)
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
        Me.TabPageReadme.Controls.Add(Me.TextBoxVersion)
        Me.TabPageReadme.Controls.Add(Me.LabelVersion)
        Me.TabPageReadme.Controls.Add(Me.LabelReadmeNavigation)
        Me.TabPageReadme.Controls.Add(Me.LinkLabelGitHubReadme)
        Me.TabPageReadme.Controls.Add(Me.PictureBoxReadmeNavigation)
        Me.TabPageReadme.ImageKey = "Info"
        Me.TabPageReadme.Location = New System.Drawing.Point(4, 23)
        Me.TabPageReadme.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageReadme.Name = "TabPageReadme"
        Me.TabPageReadme.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageReadme.Size = New System.Drawing.Size(511, 594)
        Me.TabPageReadme.TabIndex = 6
        Me.TabPageReadme.Text = "Readme"
        '
        'LinkLabelGitHubReadme
        '
        Me.LinkLabelGitHubReadme.AutoSize = True
        Me.LinkLabelGitHubReadme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabelGitHubReadme.Location = New System.Drawing.Point(15, 50)
        Me.LinkLabelGitHubReadme.Name = "LinkLabelGitHubReadme"
        Me.LinkLabelGitHubReadme.Size = New System.Drawing.Size(149, 13)
        Me.LinkLabelGitHubReadme.TabIndex = 1
        Me.LinkLabelGitHubReadme.TabStop = True
        Me.LinkLabelGitHubReadme.Text = "Help is hosted on GitHub"
        '
        'PictureBoxReadmeNavigation
        '
        Me.PictureBoxReadmeNavigation.Image = CType(resources.GetObject("PictureBoxReadmeNavigation.Image"), System.Drawing.Image)
        Me.PictureBoxReadmeNavigation.Location = New System.Drawing.Point(15, 100)
        Me.PictureBoxReadmeNavigation.Name = "PictureBoxReadmeNavigation"
        Me.PictureBoxReadmeNavigation.Size = New System.Drawing.Size(381, 211)
        Me.PictureBoxReadmeNavigation.TabIndex = 0
        Me.PictureBoxReadmeNavigation.TabStop = False
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
        Me.TextBoxStatus.Text = "Select file(s) to process OR Select none to process all (ESC to clean selections)" &
    ""
        Me.ToolTip1.SetToolTip(Me.TextBoxStatus, "Status")
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
        'ButtonProcess
        '
        Me.ButtonProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonProcess.Image = Global.Housekeeper.My.Resources.Resources.Play
        Me.ButtonProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonProcess.Location = New System.Drawing.Point(321, 26)
        Me.ButtonProcess.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonProcess.Name = "ButtonProcess"
        Me.ButtonProcess.Size = New System.Drawing.Size(96, 27)
        Me.ButtonProcess.TabIndex = 3
        Me.ButtonProcess.Text = "Process"
        Me.ButtonProcess.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ButtonCancel.Image = Global.Housekeeper.My.Resources.Resources.Close
        Me.ButtonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCancel.Location = New System.Drawing.Point(421, 26)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(96, 27)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 23)
        '
        'Menu_ListViewFile
        '
        Me.Menu_ListViewFile.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BT_Open, Me.BT_OpenFolder, Me.ToolStripSeparator8, Me.BT_FindLinks, Me.BT_ProcessSelected, Me.ToolStripSeparator5, Me.BT_Remove})
        Me.Menu_ListViewFile.Name = "Menu_ListViewFile"
        Me.Menu_ListViewFile.Size = New System.Drawing.Size(165, 126)
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
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(161, 6)
        '
        'BT_FindLinks
        '
        Me.BT_FindLinks.Image = CType(resources.GetObject("BT_FindLinks.Image"), System.Drawing.Image)
        Me.BT_FindLinks.Name = "BT_FindLinks"
        Me.BT_FindLinks.Size = New System.Drawing.Size(164, 22)
        Me.BT_FindLinks.Text = "Find linked files"
        '
        'BT_ProcessSelected
        '
        Me.BT_ProcessSelected.Image = CType(resources.GetObject("BT_ProcessSelected.Image"), System.Drawing.Image)
        Me.BT_ProcessSelected.Name = "BT_ProcessSelected"
        Me.BT_ProcessSelected.Size = New System.Drawing.Size(164, 22)
        Me.BT_ProcessSelected.Text = "Process selected"
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
        'LabelReadmeNavigation
        '
        Me.LabelReadmeNavigation.AutoSize = True
        Me.LabelReadmeNavigation.Location = New System.Drawing.Point(15, 75)
        Me.LabelReadmeNavigation.Name = "LabelReadmeNavigation"
        Me.LabelReadmeNavigation.Size = New System.Drawing.Size(379, 13)
        Me.LabelReadmeNavigation.TabIndex = 2
        Me.LabelReadmeNavigation.Text = "Navigate the GitHub Help page with the indicated Table of Contents dropdown"
        '
        'LabelVersion
        '
        Me.LabelVersion.AutoSize = True
        Me.LabelVersion.Location = New System.Drawing.Point(15, 20)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New System.Drawing.Size(163, 13)
        Me.LabelVersion.TabIndex = 3
        Me.LabelVersion.Text = "Solid Edge Housekeeper Version"
        '
        'TextBoxVersion
        '
        Me.TextBoxVersion.Enabled = False
        Me.TextBoxVersion.Location = New System.Drawing.Point(183, 17)
        Me.TextBoxVersion.Name = "TextBoxVersion"
        Me.TextBoxVersion.Size = New System.Drawing.Size(50, 20)
        Me.TextBoxVersion.TabIndex = 4
        Me.TextBoxVersion.Text = "2023-1"
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
        Me.ToolStrip_Filter.ResumeLayout(False)
        Me.ToolStrip_Filter.PerformLayout()
        Me.ToolStrip_List.ResumeLayout(False)
        Me.ToolStrip_List.PerformLayout()
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
        CType(Me.PictureBoxReadmeNavigation, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents CheckBoxSaveAsAssemblyOutputDirectory As CheckBox
    Friend WithEvents CheckBoxSaveAsPartOutputDirectory As CheckBox
    Friend WithEvents CheckBoxSaveAsSheetmetalOutputDirectory As CheckBox
    Friend WithEvents CheckBoxSaveAsDraftOutputDirectory As CheckBox
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
    Friend WithEvents ToolStrip_List As ToolStrip
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
    Friend WithEvents ToolStrip_Filter As ToolStrip
    Friend WithEvents new_CheckBoxEnablePropertyFilter As ToolStripButton
    Friend WithEvents new_ButtonPropertyFilter As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents new_CheckBoxFileSearch As ToolStripButton
    Friend WithEvents new_ComboBoxFileSearch As ToolStripComboBox
    Friend WithEvents new_ButtonFileSearchDelete As ToolStripButton
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents BT_ProcessSelected As ToolStripMenuItem
    Friend WithEvents TextBoxFontSize As TextBox
    Friend WithEvents LabelFontSize As Label
    Friend WithEvents BT_FindLinks As ToolStripMenuItem
    Friend WithEvents PictureBoxReadmeNavigation As PictureBox
    Friend WithEvents LinkLabelGitHubReadme As LinkLabel
    Friend WithEvents TextBoxVersion As TextBox
    Friend WithEvents LabelVersion As Label
    Friend WithEvents LabelReadmeNavigation As Label
End Class
