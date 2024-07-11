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
        Me.TabPageHome = New System.Windows.Forms.TabPage()
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
        Me.TabPageTasks = New System.Windows.Forms.TabPage()
        Me.TaskPanel = New System.Windows.Forms.Panel()
        Me.TaskFooterPanel = New System.Windows.Forms.Panel()
        Me.EditTaskListButton = New System.Windows.Forms.Button()
        Me.TaskHeaderPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TaskHeaderEnableButton = New System.Windows.Forms.Button()
        Me.TaskHeaderCollapseButton = New System.Windows.Forms.Button()
        Me.TaskHeaderToggleAssemblyButton = New System.Windows.Forms.Button()
        Me.TaskHeaderTogglePartButton = New System.Windows.Forms.Button()
        Me.TaskHeaderToggleSheetmetalButton = New System.Windows.Forms.Button()
        Me.TaskHeaderToggleDraftButton = New System.Windows.Forms.Button()
        Me.TaskHeaderHelpButton = New System.Windows.Forms.Button()
        Me.TaskHeaderNameLabel = New System.Windows.Forms.Label()
        Me.TabPageConfiguration = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPageTopLevelAssy = New System.Windows.Forms.TabPage()
        Me.CheckBoxTLAIgnoreIncludeInReports = New System.Windows.Forms.CheckBox()
        Me.CheckBoxTLAIncludePartCopies = New System.Windows.Forms.CheckBox()
        Me.CheckBoxDraftAndModelSameName = New System.Windows.Forms.CheckBox()
        Me.CheckBoxWarnBareTLA = New System.Windows.Forms.CheckBox()
        Me.CheckBoxTLAAutoIncludeTLF = New System.Windows.Forms.CheckBox()
        Me.ButtonFastSearchScopeFilename = New System.Windows.Forms.Button()
        Me.TextBoxFastSearchScopeFilename = New System.Windows.Forms.TextBox()
        Me.LabelFastSearchScopeFilename = New System.Windows.Forms.Label()
        Me.CheckBoxTLAReportUnrelatedFiles = New System.Windows.Forms.CheckBox()
        Me.RadioButtonTLATopDown = New System.Windows.Forms.RadioButton()
        Me.RadioButtonTLABottomUp = New System.Windows.Forms.RadioButton()
        Me.TabPageStatus = New System.Windows.Forms.TabPage()
        Me.LabelStatusAfter = New System.Windows.Forms.Label()
        Me.LabelStatusBefore = New System.Windows.Forms.Label()
        Me.LabelStatusOutReleased = New System.Windows.Forms.Label()
        Me.LabelStatusOutObsolete = New System.Windows.Forms.Label()
        Me.LabelStatusOutIW = New System.Windows.Forms.Label()
        Me.LabelStatusOutInReview = New System.Windows.Forms.Label()
        Me.LabelStatusOutBaselined = New System.Windows.Forms.Label()
        Me.LabelStatusOutAvailable = New System.Windows.Forms.Label()
        Me.GroupBoxStatusInR = New System.Windows.Forms.GroupBox()
        Me.RadioButtonStatusRtoR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusRtoO = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusRtoIW = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusRtoIR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusRtoB = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusRtoA = New System.Windows.Forms.RadioButton()
        Me.GroupBoxStatusInO = New System.Windows.Forms.GroupBox()
        Me.RadioButtonStatusOtoR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusOtoO = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusOtoIW = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusOtoIR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusOtoB = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusOtoA = New System.Windows.Forms.RadioButton()
        Me.GroupBoxStatusInIW = New System.Windows.Forms.GroupBox()
        Me.RadioButtonStatusIWtoR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIWtoO = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIWtoIW = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIWtoIR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIWtoB = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIWtoA = New System.Windows.Forms.RadioButton()
        Me.GroupBoxStatusInIR = New System.Windows.Forms.GroupBox()
        Me.RadioButtonStatusIRtoR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIRtoO = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIRtoIW = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIRtoIR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIRtoB = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIRtoA = New System.Windows.Forms.RadioButton()
        Me.GroupBoxStatusInB = New System.Windows.Forms.GroupBox()
        Me.RadioButtonStatusBtoR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusBtoO = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusBtoIW = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusBtoIR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusBtoB = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusBtoA = New System.Windows.Forms.RadioButton()
        Me.GroupBoxStatusInA = New System.Windows.Forms.GroupBox()
        Me.RadioButtonStatusAtoR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusAtoO = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusAtoIW = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusAtoIR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusAtoB = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusAtoA = New System.Windows.Forms.RadioButton()
        Me.LabelStatusInReleased = New System.Windows.Forms.Label()
        Me.LabelStatusInObsolete = New System.Windows.Forms.Label()
        Me.LabelStatusInInWork = New System.Windows.Forms.Label()
        Me.LabelStatusInInReview = New System.Windows.Forms.Label()
        Me.LabelStatusInBaselined = New System.Windows.Forms.Label()
        Me.LabelStatusInAvailable = New System.Windows.Forms.Label()
        Me.RadioButtonReadOnlyChange = New System.Windows.Forms.RadioButton()
        Me.RadioButtonReadOnlyRevert = New System.Windows.Forms.RadioButton()
        Me.CheckBoxProcessReadOnly = New System.Windows.Forms.CheckBox()
        Me.TabPageSorting = New System.Windows.Forms.TabPage()
        Me.TextBoxRandomSampleFraction = New System.Windows.Forms.TextBox()
        Me.LabelRandomSampleFraction = New System.Windows.Forms.Label()
        Me.RadioButtonListSortRandomSample = New System.Windows.Forms.RadioButton()
        Me.CheckBoxListIncludeNoDependencies = New System.Windows.Forms.CheckBox()
        Me.RadioButtonListSortDependency = New System.Windows.Forms.RadioButton()
        Me.RadioButtonListSortAlphabetical = New System.Windows.Forms.RadioButton()
        Me.RadioButtonListSortNone = New System.Windows.Forms.RadioButton()
        Me.TabPageGeneral = New System.Windows.Forms.TabPage()
        Me.CheckBoxPropertyFilterCheckDraftFile = New System.Windows.Forms.CheckBox()
        Me.CheckBoxUseCurrentSession = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoUpdateMRU = New System.Windows.Forms.CheckBox()
        Me.CheckBoxWarnSave = New System.Windows.Forms.CheckBox()
        Me.LabelFontSize = New System.Windows.Forms.Label()
        Me.TextBoxFontSize = New System.Windows.Forms.TextBox()
        Me.CheckBoxPropertyFilterFollowDraftLinks = New System.Windows.Forms.CheckBox()
        Me.CheckBoxBackgroundProcessing = New System.Windows.Forms.CheckBox()
        Me.CheckBoxRememberTasks = New System.Windows.Forms.CheckBox()
        Me.TabPageHelp = New System.Windows.Forms.TabPage()
        Me.LabelReadmeNavigation2 = New System.Windows.Forms.Label()
        Me.LabelReadmeNavigation1 = New System.Windows.Forms.Label()
        Me.LinkLabelGitHubReadme = New System.Windows.Forms.LinkLabel()
        Me.PictureBoxTableOfContents = New System.Windows.Forms.PictureBox()
        Me.TextBoxStatus = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
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
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.TabControl1.SuspendLayout()
        Me.TabPageHome.SuspendLayout()
        Me.ToolStrip_Filter.SuspendLayout()
        Me.ToolStrip_List.SuspendLayout()
        Me.TabPageTasks.SuspendLayout()
        Me.TaskFooterPanel.SuspendLayout()
        Me.TaskHeaderPanel.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TabPageConfiguration.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPageTopLevelAssy.SuspendLayout()
        Me.TabPageStatus.SuspendLayout()
        Me.GroupBoxStatusInR.SuspendLayout()
        Me.GroupBoxStatusInO.SuspendLayout()
        Me.GroupBoxStatusInIW.SuspendLayout()
        Me.GroupBoxStatusInIR.SuspendLayout()
        Me.GroupBoxStatusInB.SuspendLayout()
        Me.GroupBoxStatusInA.SuspendLayout()
        Me.TabPageSorting.SuspendLayout()
        Me.TabPageGeneral.SuspendLayout()
        Me.TabPageHelp.SuspendLayout()
        CType(Me.PictureBoxTableOfContents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Menu_ListViewFile.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageHome)
        Me.TabControl1.Controls.Add(Me.TabPageTasks)
        Me.TabControl1.Controls.Add(Me.TabPageConfiguration)
        Me.TabControl1.Controls.Add(Me.TabPageHelp)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.TabPage_ImageList
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(559, 676)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageHome
        '
        Me.TabPageHome.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageHome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageHome.Controls.Add(Me.ListViewFiles)
        Me.TabPageHome.Controls.Add(Me.ToolStrip_Filter)
        Me.TabPageHome.Controls.Add(Me.ToolStrip_List)
        Me.TabPageHome.ImageKey = "list"
        Me.TabPageHome.Location = New System.Drawing.Point(4, 25)
        Me.TabPageHome.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageHome.Name = "TabPageHome"
        Me.TabPageHome.Padding = New System.Windows.Forms.Padding(2, 2, 2, 0)
        Me.TabPageHome.Size = New System.Drawing.Size(551, 647)
        Me.TabPageHome.TabIndex = 0
        Me.TabPageHome.Text = "Home"
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
        Me.ListViewFiles.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ListViewFiles.Name = "ListViewFiles"
        Me.ListViewFiles.Size = New System.Drawing.Size(545, 593)
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
        Me.TabPage_ImageList.Images.SetKeyName(20, "expand")
        '
        'ToolStrip_Filter
        '
        Me.ToolStrip_Filter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip_Filter.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip_Filter.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.new_CheckBoxEnablePropertyFilter, Me.new_ButtonPropertyFilter, Me.ToolStripSeparator6, Me.new_CheckBoxFileSearch, Me.new_ComboBoxFileSearch, Me.new_ButtonFileSearchDelete, Me.ToolStripSeparator7})
        Me.ToolStrip_Filter.Location = New System.Drawing.Point(2, 620)
        Me.ToolStrip_Filter.Name = "ToolStrip_Filter"
        Me.ToolStrip_Filter.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip_Filter.Size = New System.Drawing.Size(545, 25)
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
        Me.new_CheckBoxFileSearch.Size = New System.Drawing.Size(74, 22)
        Me.new_CheckBoxFileSearch.Text = "Wildcard"
        '
        'new_ComboBoxFileSearch
        '
        Me.new_ComboBoxFileSearch.Name = "new_ComboBoxFileSearch"
        Me.new_ComboBoxFileSearch.Size = New System.Drawing.Size(140, 25)
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
        Me.ToolStrip_List.Size = New System.Drawing.Size(545, 25)
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
        Me.BT_TopLevelAsm.Image = Global.Housekeeper.My.Resources.Resources.SE_asm
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
        Me.new_CheckBoxFilterDft.Image = Global.Housekeeper.My.Resources.Resources.SE_dft
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
        Me.new_CheckBoxFilterPsm.Image = Global.Housekeeper.My.Resources.Resources.SE_psm
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
        Me.new_CheckBoxFilterPar.Image = Global.Housekeeper.My.Resources.Resources.SE_par
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
        Me.new_CheckBoxFilterAsm.Image = Global.Housekeeper.My.Resources.Resources.SE_asm
        Me.new_CheckBoxFilterAsm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.new_CheckBoxFilterAsm.Name = "new_CheckBoxFilterAsm"
        Me.new_CheckBoxFilterAsm.Size = New System.Drawing.Size(23, 22)
        Me.new_CheckBoxFilterAsm.Text = "ToolStripButton4"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(55, 22)
        Me.ToolStripLabel1.Text = "File Type:"
        '
        'TabPageTasks
        '
        Me.TabPageTasks.AutoScroll = True
        Me.TabPageTasks.Controls.Add(Me.TaskPanel)
        Me.TabPageTasks.Controls.Add(Me.TaskFooterPanel)
        Me.TabPageTasks.Controls.Add(Me.TaskHeaderPanel)
        Me.TabPageTasks.Location = New System.Drawing.Point(4, 24)
        Me.TabPageTasks.Name = "TabPageTasks"
        Me.TabPageTasks.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTasks.Size = New System.Drawing.Size(551, 648)
        Me.TabPageTasks.TabIndex = 7
        Me.TabPageTasks.Text = "Tasks"
        Me.TabPageTasks.UseVisualStyleBackColor = True
        '
        'TaskPanel
        '
        Me.TaskPanel.AutoScroll = True
        Me.TaskPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TaskPanel.Location = New System.Drawing.Point(3, 37)
        Me.TaskPanel.Name = "TaskPanel"
        Me.TaskPanel.Size = New System.Drawing.Size(545, 562)
        Me.TaskPanel.TabIndex = 2
        '
        'TaskFooterPanel
        '
        Me.TaskFooterPanel.BackColor = System.Drawing.Color.LightSkyBlue
        Me.TaskFooterPanel.Controls.Add(Me.EditTaskListButton)
        Me.TaskFooterPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TaskFooterPanel.Location = New System.Drawing.Point(3, 599)
        Me.TaskFooterPanel.Name = "TaskFooterPanel"
        Me.TaskFooterPanel.Size = New System.Drawing.Size(545, 46)
        Me.TaskFooterPanel.TabIndex = 1
        '
        'EditTaskListButton
        '
        Me.EditTaskListButton.Location = New System.Drawing.Point(19, 10)
        Me.EditTaskListButton.Name = "EditTaskListButton"
        Me.EditTaskListButton.Size = New System.Drawing.Size(96, 27)
        Me.EditTaskListButton.TabIndex = 0
        Me.EditTaskListButton.Text = "Edit Task List"
        Me.EditTaskListButton.UseVisualStyleBackColor = True
        '
        'TaskHeaderPanel
        '
        Me.TaskHeaderPanel.Controls.Add(Me.TableLayoutPanel2)
        Me.TaskHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TaskHeaderPanel.Location = New System.Drawing.Point(3, 3)
        Me.TaskHeaderPanel.Name = "TaskHeaderPanel"
        Me.TaskHeaderPanel.Size = New System.Drawing.Size(545, 34)
        Me.TaskHeaderPanel.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.LightSkyBlue
        Me.TableLayoutPanel2.ColumnCount = 9
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TaskHeaderEnableButton, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TaskHeaderCollapseButton, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TaskHeaderToggleAssemblyButton, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TaskHeaderTogglePartButton, 4, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TaskHeaderToggleSheetmetalButton, 5, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TaskHeaderToggleDraftButton, 6, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TaskHeaderHelpButton, 8, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TaskHeaderNameLabel, 7, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(545, 34)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TaskHeaderEnableButton
        '
        Me.TaskHeaderEnableButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TaskHeaderEnableButton.FlatAppearance.BorderSize = 0
        Me.TaskHeaderEnableButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TaskHeaderEnableButton.Image = Global.Housekeeper.My.Resources.Resources.Checked
        Me.TaskHeaderEnableButton.Location = New System.Drawing.Point(33, 3)
        Me.TaskHeaderEnableButton.Name = "TaskHeaderEnableButton"
        Me.TaskHeaderEnableButton.Size = New System.Drawing.Size(24, 28)
        Me.TaskHeaderEnableButton.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.TaskHeaderEnableButton, "Unselect all")
        Me.TaskHeaderEnableButton.UseVisualStyleBackColor = True
        '
        'TaskHeaderCollapseButton
        '
        Me.TaskHeaderCollapseButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TaskHeaderCollapseButton.Image = Global.Housekeeper.My.Resources.Resources.collapse
        Me.TaskHeaderCollapseButton.Location = New System.Drawing.Point(63, 3)
        Me.TaskHeaderCollapseButton.Name = "TaskHeaderCollapseButton"
        Me.TaskHeaderCollapseButton.Size = New System.Drawing.Size(24, 28)
        Me.TaskHeaderCollapseButton.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.TaskHeaderCollapseButton, "Collapse all")
        Me.TaskHeaderCollapseButton.UseVisualStyleBackColor = True
        '
        'TaskHeaderToggleAssemblyButton
        '
        Me.TaskHeaderToggleAssemblyButton.FlatAppearance.BorderSize = 0
        Me.TaskHeaderToggleAssemblyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TaskHeaderToggleAssemblyButton.Image = Global.Housekeeper.My.Resources.Resources.SE_asm
        Me.TaskHeaderToggleAssemblyButton.Location = New System.Drawing.Point(93, 3)
        Me.TaskHeaderToggleAssemblyButton.Name = "TaskHeaderToggleAssemblyButton"
        Me.TaskHeaderToggleAssemblyButton.Size = New System.Drawing.Size(24, 27)
        Me.TaskHeaderToggleAssemblyButton.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TaskHeaderToggleAssemblyButton, "Toggle assembly selection")
        Me.TaskHeaderToggleAssemblyButton.UseVisualStyleBackColor = True
        '
        'TaskHeaderTogglePartButton
        '
        Me.TaskHeaderTogglePartButton.FlatAppearance.BorderSize = 0
        Me.TaskHeaderTogglePartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TaskHeaderTogglePartButton.Image = Global.Housekeeper.My.Resources.Resources.SE_par
        Me.TaskHeaderTogglePartButton.Location = New System.Drawing.Point(123, 3)
        Me.TaskHeaderTogglePartButton.Name = "TaskHeaderTogglePartButton"
        Me.TaskHeaderTogglePartButton.Size = New System.Drawing.Size(24, 27)
        Me.TaskHeaderTogglePartButton.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.TaskHeaderTogglePartButton, "Toggle part selection")
        Me.TaskHeaderTogglePartButton.UseVisualStyleBackColor = True
        '
        'TaskHeaderToggleSheetmetalButton
        '
        Me.TaskHeaderToggleSheetmetalButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TaskHeaderToggleSheetmetalButton.FlatAppearance.BorderSize = 0
        Me.TaskHeaderToggleSheetmetalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TaskHeaderToggleSheetmetalButton.Image = Global.Housekeeper.My.Resources.Resources.SE_psm
        Me.TaskHeaderToggleSheetmetalButton.Location = New System.Drawing.Point(153, 3)
        Me.TaskHeaderToggleSheetmetalButton.Name = "TaskHeaderToggleSheetmetalButton"
        Me.TaskHeaderToggleSheetmetalButton.Size = New System.Drawing.Size(24, 28)
        Me.TaskHeaderToggleSheetmetalButton.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.TaskHeaderToggleSheetmetalButton, "Toggle sheetmetal selection")
        Me.TaskHeaderToggleSheetmetalButton.UseVisualStyleBackColor = True
        '
        'TaskHeaderToggleDraftButton
        '
        Me.TaskHeaderToggleDraftButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TaskHeaderToggleDraftButton.FlatAppearance.BorderSize = 0
        Me.TaskHeaderToggleDraftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TaskHeaderToggleDraftButton.Image = Global.Housekeeper.My.Resources.Resources.SE_dft
        Me.TaskHeaderToggleDraftButton.Location = New System.Drawing.Point(183, 3)
        Me.TaskHeaderToggleDraftButton.Name = "TaskHeaderToggleDraftButton"
        Me.TaskHeaderToggleDraftButton.Size = New System.Drawing.Size(24, 28)
        Me.TaskHeaderToggleDraftButton.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.TaskHeaderToggleDraftButton, "Toggle Draft selection")
        Me.TaskHeaderToggleDraftButton.UseVisualStyleBackColor = True
        '
        'TaskHeaderHelpButton
        '
        Me.TaskHeaderHelpButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TaskHeaderHelpButton.Image = Global.Housekeeper.My.Resources.Resources.Help
        Me.TaskHeaderHelpButton.Location = New System.Drawing.Point(518, 3)
        Me.TaskHeaderHelpButton.Name = "TaskHeaderHelpButton"
        Me.TaskHeaderHelpButton.Size = New System.Drawing.Size(24, 28)
        Me.TaskHeaderHelpButton.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.TaskHeaderHelpButton, "Help")
        Me.TaskHeaderHelpButton.UseVisualStyleBackColor = True
        '
        'TaskHeaderNameLabel
        '
        Me.TaskHeaderNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TaskHeaderNameLabel.AutoSize = True
        Me.TaskHeaderNameLabel.Location = New System.Drawing.Point(213, 9)
        Me.TaskHeaderNameLabel.Name = "TaskHeaderNameLabel"
        Me.TaskHeaderNameLabel.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.TaskHeaderNameLabel.Size = New System.Drawing.Size(81, 16)
        Me.TaskHeaderNameLabel.TabIndex = 7
        Me.TaskHeaderNameLabel.Text = "TASK NAME"
        '
        'TabPageConfiguration
        '
        Me.TabPageConfiguration.AutoScroll = True
        Me.TabPageConfiguration.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageConfiguration.Controls.Add(Me.TabControl2)
        Me.TabPageConfiguration.ImageKey = "Tools"
        Me.TabPageConfiguration.Location = New System.Drawing.Point(4, 24)
        Me.TabPageConfiguration.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageConfiguration.Name = "TabPageConfiguration"
        Me.TabPageConfiguration.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageConfiguration.Size = New System.Drawing.Size(551, 648)
        Me.TabPageConfiguration.TabIndex = 5
        Me.TabPageConfiguration.Text = "Configuration"
        '
        'TabControl2
        '
        Me.TabControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl2.Controls.Add(Me.TabPageTopLevelAssy)
        Me.TabControl2.Controls.Add(Me.TabPageStatus)
        Me.TabControl2.Controls.Add(Me.TabPageSorting)
        Me.TabControl2.Controls.Add(Me.TabPageGeneral)
        Me.TabControl2.ImageList = Me.TabPage_ImageList
        Me.TabControl2.Location = New System.Drawing.Point(0, 0)
        Me.TabControl2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(540, 818)
        Me.TabControl2.TabIndex = 44
        '
        'TabPageTopLevelAssy
        '
        Me.TabPageTopLevelAssy.Controls.Add(Me.CheckBoxTLAIgnoreIncludeInReports)
        Me.TabPageTopLevelAssy.Controls.Add(Me.CheckBoxTLAIncludePartCopies)
        Me.TabPageTopLevelAssy.Controls.Add(Me.CheckBoxDraftAndModelSameName)
        Me.TabPageTopLevelAssy.Controls.Add(Me.CheckBoxWarnBareTLA)
        Me.TabPageTopLevelAssy.Controls.Add(Me.CheckBoxTLAAutoIncludeTLF)
        Me.TabPageTopLevelAssy.Controls.Add(Me.ButtonFastSearchScopeFilename)
        Me.TabPageTopLevelAssy.Controls.Add(Me.TextBoxFastSearchScopeFilename)
        Me.TabPageTopLevelAssy.Controls.Add(Me.LabelFastSearchScopeFilename)
        Me.TabPageTopLevelAssy.Controls.Add(Me.CheckBoxTLAReportUnrelatedFiles)
        Me.TabPageTopLevelAssy.Controls.Add(Me.RadioButtonTLATopDown)
        Me.TabPageTopLevelAssy.Controls.Add(Me.RadioButtonTLABottomUp)
        Me.TabPageTopLevelAssy.ImageKey = "asm"
        Me.TabPageTopLevelAssy.Location = New System.Drawing.Point(4, 25)
        Me.TabPageTopLevelAssy.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageTopLevelAssy.Name = "TabPageTopLevelAssy"
        Me.TabPageTopLevelAssy.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageTopLevelAssy.Size = New System.Drawing.Size(532, 789)
        Me.TabPageTopLevelAssy.TabIndex = 2
        Me.TabPageTopLevelAssy.Text = "Top Level Assy"
        Me.TabPageTopLevelAssy.UseVisualStyleBackColor = True
        '
        'CheckBoxTLAIgnoreIncludeInReports
        '
        Me.CheckBoxTLAIgnoreIncludeInReports.AutoSize = True
        Me.CheckBoxTLAIgnoreIncludeInReports.Checked = True
        Me.CheckBoxTLAIgnoreIncludeInReports.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxTLAIgnoreIncludeInReports.Enabled = False
        Me.CheckBoxTLAIgnoreIncludeInReports.Location = New System.Drawing.Point(10, 285)
        Me.CheckBoxTLAIgnoreIncludeInReports.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxTLAIgnoreIncludeInReports.Name = "CheckBoxTLAIgnoreIncludeInReports"
        Me.CheckBoxTLAIgnoreIncludeInReports.Size = New System.Drawing.Size(261, 20)
        Me.CheckBoxTLAIgnoreIncludeInReports.TabIndex = 21
        Me.CheckBoxTLAIgnoreIncludeInReports.Text = "Ignore occurrence's IncludeInReports setting"
        Me.CheckBoxTLAIgnoreIncludeInReports.UseVisualStyleBackColor = True
        Me.CheckBoxTLAIgnoreIncludeInReports.Visible = False
        '
        'CheckBoxTLAIncludePartCopies
        '
        Me.CheckBoxTLAIncludePartCopies.AutoSize = True
        Me.CheckBoxTLAIncludePartCopies.Location = New System.Drawing.Point(10, 64)
        Me.CheckBoxTLAIncludePartCopies.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxTLAIncludePartCopies.Name = "CheckBoxTLAIncludePartCopies"
        Me.CheckBoxTLAIncludePartCopies.Size = New System.Drawing.Size(496, 20)
        Me.CheckBoxTLAIncludePartCopies.TabIndex = 20
        Me.CheckBoxTLAIncludePartCopies.Text = "Include parents of all part copies in search results, even if they are not in the" &
    " top level assy"
        Me.CheckBoxTLAIncludePartCopies.UseVisualStyleBackColor = True
        '
        'CheckBoxDraftAndModelSameName
        '
        Me.CheckBoxDraftAndModelSameName.AutoSize = True
        Me.CheckBoxDraftAndModelSameName.Location = New System.Drawing.Point(10, 199)
        Me.CheckBoxDraftAndModelSameName.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxDraftAndModelSameName.Name = "CheckBoxDraftAndModelSameName"
        Me.CheckBoxDraftAndModelSameName.Size = New System.Drawing.Size(320, 20)
        Me.CheckBoxDraftAndModelSameName.TabIndex = 19
        Me.CheckBoxDraftAndModelSameName.Text = "Draft and model have same name and are in same folder"
        Me.CheckBoxDraftAndModelSameName.UseVisualStyleBackColor = True
        '
        'CheckBoxWarnBareTLA
        '
        Me.CheckBoxWarnBareTLA.AutoSize = True
        Me.CheckBoxWarnBareTLA.Checked = True
        Me.CheckBoxWarnBareTLA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxWarnBareTLA.Location = New System.Drawing.Point(10, 35)
        Me.CheckBoxWarnBareTLA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxWarnBareTLA.Name = "CheckBoxWarnBareTLA"
        Me.CheckBoxWarnBareTLA.Size = New System.Drawing.Size(413, 20)
        Me.CheckBoxWarnBareTLA.TabIndex = 18
        Me.CheckBoxWarnBareTLA.Text = "Warn me if a top-level assembly does not have a top-level folder specified"
        Me.CheckBoxWarnBareTLA.UseVisualStyleBackColor = True
        '
        'CheckBoxTLAAutoIncludeTLF
        '
        Me.CheckBoxTLAAutoIncludeTLF.AutoSize = True
        Me.CheckBoxTLAAutoIncludeTLF.Checked = True
        Me.CheckBoxTLAAutoIncludeTLF.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxTLAAutoIncludeTLF.Location = New System.Drawing.Point(10, 6)
        Me.CheckBoxTLAAutoIncludeTLF.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxTLAAutoIncludeTLF.Name = "CheckBoxTLAAutoIncludeTLF"
        Me.CheckBoxTLAAutoIncludeTLF.Size = New System.Drawing.Size(367, 20)
        Me.CheckBoxTLAAutoIncludeTLF.TabIndex = 17
        Me.CheckBoxTLAAutoIncludeTLF.Text = "Automatically include the folder if a top-level assembly is chosen"
        Me.CheckBoxTLAAutoIncludeTLF.UseVisualStyleBackColor = True
        '
        'ButtonFastSearchScopeFilename
        '
        Me.ButtonFastSearchScopeFilename.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonFastSearchScopeFilename.Location = New System.Drawing.Point(431, 244)
        Me.ButtonFastSearchScopeFilename.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonFastSearchScopeFilename.Name = "ButtonFastSearchScopeFilename"
        Me.ButtonFastSearchScopeFilename.Size = New System.Drawing.Size(88, 29)
        Me.ButtonFastSearchScopeFilename.TabIndex = 16
        Me.ButtonFastSearchScopeFilename.Text = "Browse"
        Me.ButtonFastSearchScopeFilename.UseVisualStyleBackColor = True
        '
        'TextBoxFastSearchScopeFilename
        '
        Me.TextBoxFastSearchScopeFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFastSearchScopeFilename.Location = New System.Drawing.Point(10, 246)
        Me.TextBoxFastSearchScopeFilename.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFastSearchScopeFilename.Name = "TextBoxFastSearchScopeFilename"
        Me.TextBoxFastSearchScopeFilename.Size = New System.Drawing.Size(406, 23)
        Me.TextBoxFastSearchScopeFilename.TabIndex = 15
        '
        'LabelFastSearchScopeFilename
        '
        Me.LabelFastSearchScopeFilename.AutoSize = True
        Me.LabelFastSearchScopeFilename.Location = New System.Drawing.Point(7, 227)
        Me.LabelFastSearchScopeFilename.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFastSearchScopeFilename.Name = "LabelFastSearchScopeFilename"
        Me.LabelFastSearchScopeFilename.Size = New System.Drawing.Size(153, 16)
        Me.LabelFastSearchScopeFilename.TabIndex = 14
        Me.LabelFastSearchScopeFilename.Text = "Fast Search Scope Filename"
        '
        'CheckBoxTLAReportUnrelatedFiles
        '
        Me.CheckBoxTLAReportUnrelatedFiles.AutoSize = True
        Me.CheckBoxTLAReportUnrelatedFiles.Location = New System.Drawing.Point(10, 91)
        Me.CheckBoxTLAReportUnrelatedFiles.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxTLAReportUnrelatedFiles.Name = "CheckBoxTLAReportUnrelatedFiles"
        Me.CheckBoxTLAReportUnrelatedFiles.Size = New System.Drawing.Size(251, 20)
        Me.CheckBoxTLAReportUnrelatedFiles.TabIndex = 13
        Me.CheckBoxTLAReportUnrelatedFiles.Text = "Report files unrelated to top level assembly"
        Me.CheckBoxTLAReportUnrelatedFiles.UseVisualStyleBackColor = True
        '
        'RadioButtonTLATopDown
        '
        Me.RadioButtonTLATopDown.AutoSize = True
        Me.RadioButtonTLATopDown.Location = New System.Drawing.Point(10, 146)
        Me.RadioButtonTLATopDown.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonTLATopDown.Name = "RadioButtonTLATopDown"
        Me.RadioButtonTLATopDown.Size = New System.Drawing.Size(350, 20)
        Me.RadioButtonTLATopDown.TabIndex = 12
        Me.RadioButtonTLATopDown.Text = "Top Down Search -- Best for self-contained project directories"
        Me.RadioButtonTLATopDown.UseVisualStyleBackColor = True
        '
        'RadioButtonTLABottomUp
        '
        Me.RadioButtonTLABottomUp.AutoSize = True
        Me.RadioButtonTLABottomUp.Checked = True
        Me.RadioButtonTLABottomUp.Location = New System.Drawing.Point(10, 172)
        Me.RadioButtonTLABottomUp.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonTLABottomUp.Name = "RadioButtonTLABottomUp"
        Me.RadioButtonTLABottomUp.Size = New System.Drawing.Size(323, 20)
        Me.RadioButtonTLABottomUp.TabIndex = 11
        Me.RadioButtonTLABottomUp.TabStop = True
        Me.RadioButtonTLABottomUp.Text = "Bottom Up Search -- Best for general purpose directories"
        Me.RadioButtonTLABottomUp.UseVisualStyleBackColor = True
        '
        'TabPageStatus
        '
        Me.TabPageStatus.Controls.Add(Me.LabelStatusAfter)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusBefore)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusOutReleased)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusOutObsolete)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusOutIW)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusOutInReview)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusOutBaselined)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusOutAvailable)
        Me.TabPageStatus.Controls.Add(Me.GroupBoxStatusInR)
        Me.TabPageStatus.Controls.Add(Me.GroupBoxStatusInO)
        Me.TabPageStatus.Controls.Add(Me.GroupBoxStatusInIW)
        Me.TabPageStatus.Controls.Add(Me.GroupBoxStatusInIR)
        Me.TabPageStatus.Controls.Add(Me.GroupBoxStatusInB)
        Me.TabPageStatus.Controls.Add(Me.GroupBoxStatusInA)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusInReleased)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusInObsolete)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusInInWork)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusInInReview)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusInBaselined)
        Me.TabPageStatus.Controls.Add(Me.LabelStatusInAvailable)
        Me.TabPageStatus.Controls.Add(Me.RadioButtonReadOnlyChange)
        Me.TabPageStatus.Controls.Add(Me.RadioButtonReadOnlyRevert)
        Me.TabPageStatus.Controls.Add(Me.CheckBoxProcessReadOnly)
        Me.TabPageStatus.ImageKey = "folder"
        Me.TabPageStatus.Location = New System.Drawing.Point(4, 24)
        Me.TabPageStatus.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageStatus.Name = "TabPageStatus"
        Me.TabPageStatus.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageStatus.Size = New System.Drawing.Size(532, 790)
        Me.TabPageStatus.TabIndex = 3
        Me.TabPageStatus.Text = "Status"
        Me.TabPageStatus.UseVisualStyleBackColor = True
        '
        'LabelStatusAfter
        '
        Me.LabelStatusAfter.AutoSize = True
        Me.LabelStatusAfter.Location = New System.Drawing.Point(258, 134)
        Me.LabelStatusAfter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusAfter.Name = "LabelStatusAfter"
        Me.LabelStatusAfter.Size = New System.Drawing.Size(83, 16)
        Me.LabelStatusAfter.TabIndex = 95
        Me.LabelStatusAfter.Text = "STATUS AFTER"
        '
        'LabelStatusBefore
        '
        Me.LabelStatusBefore.AutoSize = True
        Me.LabelStatusBefore.Location = New System.Drawing.Point(110, 169)
        Me.LabelStatusBefore.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusBefore.Name = "LabelStatusBefore"
        Me.LabelStatusBefore.Size = New System.Drawing.Size(91, 16)
        Me.LabelStatusBefore.TabIndex = 94
        Me.LabelStatusBefore.Text = "STATUS BEFORE"
        '
        'LabelStatusOutReleased
        '
        Me.LabelStatusOutReleased.AutoSize = True
        Me.LabelStatusOutReleased.Location = New System.Drawing.Point(380, 169)
        Me.LabelStatusOutReleased.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusOutReleased.Name = "LabelStatusOutReleased"
        Me.LabelStatusOutReleased.Size = New System.Drawing.Size(14, 16)
        Me.LabelStatusOutReleased.TabIndex = 93
        Me.LabelStatusOutReleased.Text = "R"
        '
        'LabelStatusOutObsolete
        '
        Me.LabelStatusOutObsolete.AutoSize = True
        Me.LabelStatusOutObsolete.Location = New System.Drawing.Point(351, 169)
        Me.LabelStatusOutObsolete.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusOutObsolete.Name = "LabelStatusOutObsolete"
        Me.LabelStatusOutObsolete.Size = New System.Drawing.Size(16, 16)
        Me.LabelStatusOutObsolete.TabIndex = 92
        Me.LabelStatusOutObsolete.Text = "O"
        '
        'LabelStatusOutIW
        '
        Me.LabelStatusOutIW.AutoSize = True
        Me.LabelStatusOutIW.Location = New System.Drawing.Point(321, 169)
        Me.LabelStatusOutIW.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusOutIW.Name = "LabelStatusOutIW"
        Me.LabelStatusOutIW.Size = New System.Drawing.Size(21, 16)
        Me.LabelStatusOutIW.TabIndex = 91
        Me.LabelStatusOutIW.Text = "IW"
        '
        'LabelStatusOutInReview
        '
        Me.LabelStatusOutInReview.AutoSize = True
        Me.LabelStatusOutInReview.Location = New System.Drawing.Point(290, 169)
        Me.LabelStatusOutInReview.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusOutInReview.Name = "LabelStatusOutInReview"
        Me.LabelStatusOutInReview.Size = New System.Drawing.Size(17, 16)
        Me.LabelStatusOutInReview.TabIndex = 90
        Me.LabelStatusOutInReview.Text = "IR"
        '
        'LabelStatusOutBaselined
        '
        Me.LabelStatusOutBaselined.AutoSize = True
        Me.LabelStatusOutBaselined.Location = New System.Drawing.Point(263, 169)
        Me.LabelStatusOutBaselined.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusOutBaselined.Name = "LabelStatusOutBaselined"
        Me.LabelStatusOutBaselined.Size = New System.Drawing.Size(14, 16)
        Me.LabelStatusOutBaselined.TabIndex = 89
        Me.LabelStatusOutBaselined.Text = "B"
        '
        'LabelStatusOutAvailable
        '
        Me.LabelStatusOutAvailable.AutoSize = True
        Me.LabelStatusOutAvailable.Location = New System.Drawing.Point(234, 169)
        Me.LabelStatusOutAvailable.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusOutAvailable.Name = "LabelStatusOutAvailable"
        Me.LabelStatusOutAvailable.Size = New System.Drawing.Size(15, 16)
        Me.LabelStatusOutAvailable.TabIndex = 88
        Me.LabelStatusOutAvailable.Text = "A"
        '
        'GroupBoxStatusInR
        '
        Me.GroupBoxStatusInR.Controls.Add(Me.RadioButtonStatusRtoR)
        Me.GroupBoxStatusInR.Controls.Add(Me.RadioButtonStatusRtoO)
        Me.GroupBoxStatusInR.Controls.Add(Me.RadioButtonStatusRtoIW)
        Me.GroupBoxStatusInR.Controls.Add(Me.RadioButtonStatusRtoIR)
        Me.GroupBoxStatusInR.Controls.Add(Me.RadioButtonStatusRtoB)
        Me.GroupBoxStatusInR.Controls.Add(Me.RadioButtonStatusRtoA)
        Me.GroupBoxStatusInR.Location = New System.Drawing.Point(234, 364)
        Me.GroupBoxStatusInR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInR.Name = "GroupBoxStatusInR"
        Me.GroupBoxStatusInR.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInR.Size = New System.Drawing.Size(175, 35)
        Me.GroupBoxStatusInR.TabIndex = 87
        Me.GroupBoxStatusInR.TabStop = False
        '
        'RadioButtonStatusRtoR
        '
        Me.RadioButtonStatusRtoR.AutoSize = True
        Me.RadioButtonStatusRtoR.Location = New System.Drawing.Point(146, 15)
        Me.RadioButtonStatusRtoR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusRtoR.Name = "RadioButtonStatusRtoR"
        Me.RadioButtonStatusRtoR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusRtoR.TabIndex = 5
        Me.RadioButtonStatusRtoR.TabStop = True
        Me.RadioButtonStatusRtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoO
        '
        Me.RadioButtonStatusRtoO.AutoSize = True
        Me.RadioButtonStatusRtoO.Location = New System.Drawing.Point(117, 15)
        Me.RadioButtonStatusRtoO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusRtoO.Name = "RadioButtonStatusRtoO"
        Me.RadioButtonStatusRtoO.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusRtoO.TabIndex = 4
        Me.RadioButtonStatusRtoO.TabStop = True
        Me.RadioButtonStatusRtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoIW
        '
        Me.RadioButtonStatusRtoIW.AutoSize = True
        Me.RadioButtonStatusRtoIW.Location = New System.Drawing.Point(88, 15)
        Me.RadioButtonStatusRtoIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusRtoIW.Name = "RadioButtonStatusRtoIW"
        Me.RadioButtonStatusRtoIW.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusRtoIW.TabIndex = 3
        Me.RadioButtonStatusRtoIW.TabStop = True
        Me.RadioButtonStatusRtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoIR
        '
        Me.RadioButtonStatusRtoIR.AutoSize = True
        Me.RadioButtonStatusRtoIR.Location = New System.Drawing.Point(58, 15)
        Me.RadioButtonStatusRtoIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusRtoIR.Name = "RadioButtonStatusRtoIR"
        Me.RadioButtonStatusRtoIR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusRtoIR.TabIndex = 2
        Me.RadioButtonStatusRtoIR.TabStop = True
        Me.RadioButtonStatusRtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoB
        '
        Me.RadioButtonStatusRtoB.AutoSize = True
        Me.RadioButtonStatusRtoB.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonStatusRtoB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusRtoB.Name = "RadioButtonStatusRtoB"
        Me.RadioButtonStatusRtoB.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusRtoB.TabIndex = 1
        Me.RadioButtonStatusRtoB.TabStop = True
        Me.RadioButtonStatusRtoB.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoA
        '
        Me.RadioButtonStatusRtoA.AutoSize = True
        Me.RadioButtonStatusRtoA.Checked = True
        Me.RadioButtonStatusRtoA.Location = New System.Drawing.Point(0, 15)
        Me.RadioButtonStatusRtoA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusRtoA.Name = "RadioButtonStatusRtoA"
        Me.RadioButtonStatusRtoA.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusRtoA.TabIndex = 0
        Me.RadioButtonStatusRtoA.TabStop = True
        Me.RadioButtonStatusRtoA.UseVisualStyleBackColor = True
        '
        'GroupBoxStatusInO
        '
        Me.GroupBoxStatusInO.Controls.Add(Me.RadioButtonStatusOtoR)
        Me.GroupBoxStatusInO.Controls.Add(Me.RadioButtonStatusOtoO)
        Me.GroupBoxStatusInO.Controls.Add(Me.RadioButtonStatusOtoIW)
        Me.GroupBoxStatusInO.Controls.Add(Me.RadioButtonStatusOtoIR)
        Me.GroupBoxStatusInO.Controls.Add(Me.RadioButtonStatusOtoB)
        Me.GroupBoxStatusInO.Controls.Add(Me.RadioButtonStatusOtoA)
        Me.GroupBoxStatusInO.Location = New System.Drawing.Point(234, 330)
        Me.GroupBoxStatusInO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInO.Name = "GroupBoxStatusInO"
        Me.GroupBoxStatusInO.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInO.Size = New System.Drawing.Size(175, 35)
        Me.GroupBoxStatusInO.TabIndex = 86
        Me.GroupBoxStatusInO.TabStop = False
        '
        'RadioButtonStatusOtoR
        '
        Me.RadioButtonStatusOtoR.AutoSize = True
        Me.RadioButtonStatusOtoR.Location = New System.Drawing.Point(146, 15)
        Me.RadioButtonStatusOtoR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusOtoR.Name = "RadioButtonStatusOtoR"
        Me.RadioButtonStatusOtoR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusOtoR.TabIndex = 5
        Me.RadioButtonStatusOtoR.TabStop = True
        Me.RadioButtonStatusOtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoO
        '
        Me.RadioButtonStatusOtoO.AutoSize = True
        Me.RadioButtonStatusOtoO.Location = New System.Drawing.Point(117, 15)
        Me.RadioButtonStatusOtoO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusOtoO.Name = "RadioButtonStatusOtoO"
        Me.RadioButtonStatusOtoO.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusOtoO.TabIndex = 4
        Me.RadioButtonStatusOtoO.TabStop = True
        Me.RadioButtonStatusOtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoIW
        '
        Me.RadioButtonStatusOtoIW.AutoSize = True
        Me.RadioButtonStatusOtoIW.Location = New System.Drawing.Point(88, 15)
        Me.RadioButtonStatusOtoIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusOtoIW.Name = "RadioButtonStatusOtoIW"
        Me.RadioButtonStatusOtoIW.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusOtoIW.TabIndex = 3
        Me.RadioButtonStatusOtoIW.TabStop = True
        Me.RadioButtonStatusOtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoIR
        '
        Me.RadioButtonStatusOtoIR.AutoSize = True
        Me.RadioButtonStatusOtoIR.Location = New System.Drawing.Point(58, 15)
        Me.RadioButtonStatusOtoIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusOtoIR.Name = "RadioButtonStatusOtoIR"
        Me.RadioButtonStatusOtoIR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusOtoIR.TabIndex = 2
        Me.RadioButtonStatusOtoIR.TabStop = True
        Me.RadioButtonStatusOtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoB
        '
        Me.RadioButtonStatusOtoB.AutoSize = True
        Me.RadioButtonStatusOtoB.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonStatusOtoB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusOtoB.Name = "RadioButtonStatusOtoB"
        Me.RadioButtonStatusOtoB.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusOtoB.TabIndex = 1
        Me.RadioButtonStatusOtoB.TabStop = True
        Me.RadioButtonStatusOtoB.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoA
        '
        Me.RadioButtonStatusOtoA.AutoSize = True
        Me.RadioButtonStatusOtoA.Checked = True
        Me.RadioButtonStatusOtoA.Location = New System.Drawing.Point(0, 15)
        Me.RadioButtonStatusOtoA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusOtoA.Name = "RadioButtonStatusOtoA"
        Me.RadioButtonStatusOtoA.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusOtoA.TabIndex = 0
        Me.RadioButtonStatusOtoA.TabStop = True
        Me.RadioButtonStatusOtoA.UseVisualStyleBackColor = True
        '
        'GroupBoxStatusInIW
        '
        Me.GroupBoxStatusInIW.Controls.Add(Me.RadioButtonStatusIWtoR)
        Me.GroupBoxStatusInIW.Controls.Add(Me.RadioButtonStatusIWtoO)
        Me.GroupBoxStatusInIW.Controls.Add(Me.RadioButtonStatusIWtoIW)
        Me.GroupBoxStatusInIW.Controls.Add(Me.RadioButtonStatusIWtoIR)
        Me.GroupBoxStatusInIW.Controls.Add(Me.RadioButtonStatusIWtoB)
        Me.GroupBoxStatusInIW.Controls.Add(Me.RadioButtonStatusIWtoA)
        Me.GroupBoxStatusInIW.Location = New System.Drawing.Point(234, 294)
        Me.GroupBoxStatusInIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInIW.Name = "GroupBoxStatusInIW"
        Me.GroupBoxStatusInIW.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInIW.Size = New System.Drawing.Size(175, 35)
        Me.GroupBoxStatusInIW.TabIndex = 85
        Me.GroupBoxStatusInIW.TabStop = False
        '
        'RadioButtonStatusIWtoR
        '
        Me.RadioButtonStatusIWtoR.AutoSize = True
        Me.RadioButtonStatusIWtoR.Location = New System.Drawing.Point(146, 15)
        Me.RadioButtonStatusIWtoR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIWtoR.Name = "RadioButtonStatusIWtoR"
        Me.RadioButtonStatusIWtoR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIWtoR.TabIndex = 5
        Me.RadioButtonStatusIWtoR.TabStop = True
        Me.RadioButtonStatusIWtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoO
        '
        Me.RadioButtonStatusIWtoO.AutoSize = True
        Me.RadioButtonStatusIWtoO.Location = New System.Drawing.Point(117, 15)
        Me.RadioButtonStatusIWtoO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIWtoO.Name = "RadioButtonStatusIWtoO"
        Me.RadioButtonStatusIWtoO.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIWtoO.TabIndex = 4
        Me.RadioButtonStatusIWtoO.TabStop = True
        Me.RadioButtonStatusIWtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoIW
        '
        Me.RadioButtonStatusIWtoIW.AutoSize = True
        Me.RadioButtonStatusIWtoIW.Location = New System.Drawing.Point(88, 15)
        Me.RadioButtonStatusIWtoIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIWtoIW.Name = "RadioButtonStatusIWtoIW"
        Me.RadioButtonStatusIWtoIW.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIWtoIW.TabIndex = 3
        Me.RadioButtonStatusIWtoIW.TabStop = True
        Me.RadioButtonStatusIWtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoIR
        '
        Me.RadioButtonStatusIWtoIR.AutoSize = True
        Me.RadioButtonStatusIWtoIR.Location = New System.Drawing.Point(58, 15)
        Me.RadioButtonStatusIWtoIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIWtoIR.Name = "RadioButtonStatusIWtoIR"
        Me.RadioButtonStatusIWtoIR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIWtoIR.TabIndex = 2
        Me.RadioButtonStatusIWtoIR.TabStop = True
        Me.RadioButtonStatusIWtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoB
        '
        Me.RadioButtonStatusIWtoB.AutoSize = True
        Me.RadioButtonStatusIWtoB.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonStatusIWtoB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIWtoB.Name = "RadioButtonStatusIWtoB"
        Me.RadioButtonStatusIWtoB.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIWtoB.TabIndex = 1
        Me.RadioButtonStatusIWtoB.TabStop = True
        Me.RadioButtonStatusIWtoB.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoA
        '
        Me.RadioButtonStatusIWtoA.AutoSize = True
        Me.RadioButtonStatusIWtoA.Checked = True
        Me.RadioButtonStatusIWtoA.Location = New System.Drawing.Point(0, 15)
        Me.RadioButtonStatusIWtoA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIWtoA.Name = "RadioButtonStatusIWtoA"
        Me.RadioButtonStatusIWtoA.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIWtoA.TabIndex = 0
        Me.RadioButtonStatusIWtoA.TabStop = True
        Me.RadioButtonStatusIWtoA.UseVisualStyleBackColor = True
        '
        'GroupBoxStatusInIR
        '
        Me.GroupBoxStatusInIR.Controls.Add(Me.RadioButtonStatusIRtoR)
        Me.GroupBoxStatusInIR.Controls.Add(Me.RadioButtonStatusIRtoO)
        Me.GroupBoxStatusInIR.Controls.Add(Me.RadioButtonStatusIRtoIW)
        Me.GroupBoxStatusInIR.Controls.Add(Me.RadioButtonStatusIRtoIR)
        Me.GroupBoxStatusInIR.Controls.Add(Me.RadioButtonStatusIRtoB)
        Me.GroupBoxStatusInIR.Controls.Add(Me.RadioButtonStatusIRtoA)
        Me.GroupBoxStatusInIR.Location = New System.Drawing.Point(234, 259)
        Me.GroupBoxStatusInIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInIR.Name = "GroupBoxStatusInIR"
        Me.GroupBoxStatusInIR.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInIR.Size = New System.Drawing.Size(175, 35)
        Me.GroupBoxStatusInIR.TabIndex = 84
        Me.GroupBoxStatusInIR.TabStop = False
        '
        'RadioButtonStatusIRtoR
        '
        Me.RadioButtonStatusIRtoR.AutoSize = True
        Me.RadioButtonStatusIRtoR.Location = New System.Drawing.Point(146, 15)
        Me.RadioButtonStatusIRtoR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIRtoR.Name = "RadioButtonStatusIRtoR"
        Me.RadioButtonStatusIRtoR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIRtoR.TabIndex = 5
        Me.RadioButtonStatusIRtoR.TabStop = True
        Me.RadioButtonStatusIRtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoO
        '
        Me.RadioButtonStatusIRtoO.AutoSize = True
        Me.RadioButtonStatusIRtoO.Location = New System.Drawing.Point(117, 15)
        Me.RadioButtonStatusIRtoO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIRtoO.Name = "RadioButtonStatusIRtoO"
        Me.RadioButtonStatusIRtoO.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIRtoO.TabIndex = 4
        Me.RadioButtonStatusIRtoO.TabStop = True
        Me.RadioButtonStatusIRtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoIW
        '
        Me.RadioButtonStatusIRtoIW.AutoSize = True
        Me.RadioButtonStatusIRtoIW.Location = New System.Drawing.Point(88, 15)
        Me.RadioButtonStatusIRtoIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIRtoIW.Name = "RadioButtonStatusIRtoIW"
        Me.RadioButtonStatusIRtoIW.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIRtoIW.TabIndex = 3
        Me.RadioButtonStatusIRtoIW.TabStop = True
        Me.RadioButtonStatusIRtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoIR
        '
        Me.RadioButtonStatusIRtoIR.AutoSize = True
        Me.RadioButtonStatusIRtoIR.Location = New System.Drawing.Point(58, 15)
        Me.RadioButtonStatusIRtoIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIRtoIR.Name = "RadioButtonStatusIRtoIR"
        Me.RadioButtonStatusIRtoIR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIRtoIR.TabIndex = 2
        Me.RadioButtonStatusIRtoIR.TabStop = True
        Me.RadioButtonStatusIRtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoB
        '
        Me.RadioButtonStatusIRtoB.AutoSize = True
        Me.RadioButtonStatusIRtoB.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonStatusIRtoB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIRtoB.Name = "RadioButtonStatusIRtoB"
        Me.RadioButtonStatusIRtoB.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIRtoB.TabIndex = 1
        Me.RadioButtonStatusIRtoB.TabStop = True
        Me.RadioButtonStatusIRtoB.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoA
        '
        Me.RadioButtonStatusIRtoA.AutoSize = True
        Me.RadioButtonStatusIRtoA.Checked = True
        Me.RadioButtonStatusIRtoA.Location = New System.Drawing.Point(0, 15)
        Me.RadioButtonStatusIRtoA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIRtoA.Name = "RadioButtonStatusIRtoA"
        Me.RadioButtonStatusIRtoA.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIRtoA.TabIndex = 0
        Me.RadioButtonStatusIRtoA.TabStop = True
        Me.RadioButtonStatusIRtoA.UseVisualStyleBackColor = True
        '
        'GroupBoxStatusInB
        '
        Me.GroupBoxStatusInB.Controls.Add(Me.RadioButtonStatusBtoR)
        Me.GroupBoxStatusInB.Controls.Add(Me.RadioButtonStatusBtoO)
        Me.GroupBoxStatusInB.Controls.Add(Me.RadioButtonStatusBtoIW)
        Me.GroupBoxStatusInB.Controls.Add(Me.RadioButtonStatusBtoIR)
        Me.GroupBoxStatusInB.Controls.Add(Me.RadioButtonStatusBtoB)
        Me.GroupBoxStatusInB.Controls.Add(Me.RadioButtonStatusBtoA)
        Me.GroupBoxStatusInB.Location = New System.Drawing.Point(234, 225)
        Me.GroupBoxStatusInB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInB.Name = "GroupBoxStatusInB"
        Me.GroupBoxStatusInB.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInB.Size = New System.Drawing.Size(175, 35)
        Me.GroupBoxStatusInB.TabIndex = 83
        Me.GroupBoxStatusInB.TabStop = False
        '
        'RadioButtonStatusBtoR
        '
        Me.RadioButtonStatusBtoR.AutoSize = True
        Me.RadioButtonStatusBtoR.Location = New System.Drawing.Point(146, 15)
        Me.RadioButtonStatusBtoR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusBtoR.Name = "RadioButtonStatusBtoR"
        Me.RadioButtonStatusBtoR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusBtoR.TabIndex = 5
        Me.RadioButtonStatusBtoR.TabStop = True
        Me.RadioButtonStatusBtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoO
        '
        Me.RadioButtonStatusBtoO.AutoSize = True
        Me.RadioButtonStatusBtoO.Location = New System.Drawing.Point(117, 15)
        Me.RadioButtonStatusBtoO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusBtoO.Name = "RadioButtonStatusBtoO"
        Me.RadioButtonStatusBtoO.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusBtoO.TabIndex = 4
        Me.RadioButtonStatusBtoO.TabStop = True
        Me.RadioButtonStatusBtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoIW
        '
        Me.RadioButtonStatusBtoIW.AutoSize = True
        Me.RadioButtonStatusBtoIW.Location = New System.Drawing.Point(88, 15)
        Me.RadioButtonStatusBtoIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusBtoIW.Name = "RadioButtonStatusBtoIW"
        Me.RadioButtonStatusBtoIW.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusBtoIW.TabIndex = 3
        Me.RadioButtonStatusBtoIW.TabStop = True
        Me.RadioButtonStatusBtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoIR
        '
        Me.RadioButtonStatusBtoIR.AutoSize = True
        Me.RadioButtonStatusBtoIR.Location = New System.Drawing.Point(58, 15)
        Me.RadioButtonStatusBtoIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusBtoIR.Name = "RadioButtonStatusBtoIR"
        Me.RadioButtonStatusBtoIR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusBtoIR.TabIndex = 2
        Me.RadioButtonStatusBtoIR.TabStop = True
        Me.RadioButtonStatusBtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoB
        '
        Me.RadioButtonStatusBtoB.AutoSize = True
        Me.RadioButtonStatusBtoB.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonStatusBtoB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusBtoB.Name = "RadioButtonStatusBtoB"
        Me.RadioButtonStatusBtoB.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusBtoB.TabIndex = 1
        Me.RadioButtonStatusBtoB.TabStop = True
        Me.RadioButtonStatusBtoB.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoA
        '
        Me.RadioButtonStatusBtoA.AutoSize = True
        Me.RadioButtonStatusBtoA.Checked = True
        Me.RadioButtonStatusBtoA.Location = New System.Drawing.Point(0, 15)
        Me.RadioButtonStatusBtoA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusBtoA.Name = "RadioButtonStatusBtoA"
        Me.RadioButtonStatusBtoA.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusBtoA.TabIndex = 0
        Me.RadioButtonStatusBtoA.TabStop = True
        Me.RadioButtonStatusBtoA.UseVisualStyleBackColor = True
        '
        'GroupBoxStatusInA
        '
        Me.GroupBoxStatusInA.Controls.Add(Me.RadioButtonStatusAtoR)
        Me.GroupBoxStatusInA.Controls.Add(Me.RadioButtonStatusAtoO)
        Me.GroupBoxStatusInA.Controls.Add(Me.RadioButtonStatusAtoIW)
        Me.GroupBoxStatusInA.Controls.Add(Me.RadioButtonStatusAtoIR)
        Me.GroupBoxStatusInA.Controls.Add(Me.RadioButtonStatusAtoB)
        Me.GroupBoxStatusInA.Controls.Add(Me.RadioButtonStatusAtoA)
        Me.GroupBoxStatusInA.Location = New System.Drawing.Point(234, 190)
        Me.GroupBoxStatusInA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInA.Name = "GroupBoxStatusInA"
        Me.GroupBoxStatusInA.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInA.Size = New System.Drawing.Size(175, 35)
        Me.GroupBoxStatusInA.TabIndex = 82
        Me.GroupBoxStatusInA.TabStop = False
        '
        'RadioButtonStatusAtoR
        '
        Me.RadioButtonStatusAtoR.AutoSize = True
        Me.RadioButtonStatusAtoR.Location = New System.Drawing.Point(146, 15)
        Me.RadioButtonStatusAtoR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusAtoR.Name = "RadioButtonStatusAtoR"
        Me.RadioButtonStatusAtoR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusAtoR.TabIndex = 5
        Me.RadioButtonStatusAtoR.TabStop = True
        Me.RadioButtonStatusAtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoO
        '
        Me.RadioButtonStatusAtoO.AutoSize = True
        Me.RadioButtonStatusAtoO.Location = New System.Drawing.Point(117, 15)
        Me.RadioButtonStatusAtoO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusAtoO.Name = "RadioButtonStatusAtoO"
        Me.RadioButtonStatusAtoO.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusAtoO.TabIndex = 4
        Me.RadioButtonStatusAtoO.TabStop = True
        Me.RadioButtonStatusAtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoIW
        '
        Me.RadioButtonStatusAtoIW.AutoSize = True
        Me.RadioButtonStatusAtoIW.Location = New System.Drawing.Point(88, 15)
        Me.RadioButtonStatusAtoIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusAtoIW.Name = "RadioButtonStatusAtoIW"
        Me.RadioButtonStatusAtoIW.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusAtoIW.TabIndex = 3
        Me.RadioButtonStatusAtoIW.TabStop = True
        Me.RadioButtonStatusAtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoIR
        '
        Me.RadioButtonStatusAtoIR.AutoSize = True
        Me.RadioButtonStatusAtoIR.Location = New System.Drawing.Point(58, 15)
        Me.RadioButtonStatusAtoIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusAtoIR.Name = "RadioButtonStatusAtoIR"
        Me.RadioButtonStatusAtoIR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusAtoIR.TabIndex = 2
        Me.RadioButtonStatusAtoIR.TabStop = True
        Me.RadioButtonStatusAtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoB
        '
        Me.RadioButtonStatusAtoB.AutoSize = True
        Me.RadioButtonStatusAtoB.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonStatusAtoB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusAtoB.Name = "RadioButtonStatusAtoB"
        Me.RadioButtonStatusAtoB.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusAtoB.TabIndex = 1
        Me.RadioButtonStatusAtoB.TabStop = True
        Me.RadioButtonStatusAtoB.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoA
        '
        Me.RadioButtonStatusAtoA.AutoSize = True
        Me.RadioButtonStatusAtoA.Checked = True
        Me.RadioButtonStatusAtoA.Location = New System.Drawing.Point(0, 15)
        Me.RadioButtonStatusAtoA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusAtoA.Name = "RadioButtonStatusAtoA"
        Me.RadioButtonStatusAtoA.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusAtoA.TabIndex = 0
        Me.RadioButtonStatusAtoA.TabStop = True
        Me.RadioButtonStatusAtoA.UseVisualStyleBackColor = True
        '
        'LabelStatusInReleased
        '
        Me.LabelStatusInReleased.AutoSize = True
        Me.LabelStatusInReleased.Location = New System.Drawing.Point(135, 377)
        Me.LabelStatusInReleased.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusInReleased.Name = "LabelStatusInReleased"
        Me.LabelStatusInReleased.Size = New System.Drawing.Size(71, 16)
        Me.LabelStatusInReleased.TabIndex = 81
        Me.LabelStatusInReleased.Text = "Released (R)"
        '
        'LabelStatusInObsolete
        '
        Me.LabelStatusInObsolete.AutoSize = True
        Me.LabelStatusInObsolete.Location = New System.Drawing.Point(139, 341)
        Me.LabelStatusInObsolete.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusInObsolete.Name = "LabelStatusInObsolete"
        Me.LabelStatusInObsolete.Size = New System.Drawing.Size(74, 16)
        Me.LabelStatusInObsolete.TabIndex = 80
        Me.LabelStatusInObsolete.Text = "Obsolete (O)"
        '
        'LabelStatusInInWork
        '
        Me.LabelStatusInInWork.AutoSize = True
        Me.LabelStatusInInWork.Location = New System.Drawing.Point(139, 307)
        Me.LabelStatusInInWork.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusInInWork.Name = "LabelStatusInInWork"
        Me.LabelStatusInInWork.Size = New System.Drawing.Size(76, 16)
        Me.LabelStatusInInWork.TabIndex = 79
        Me.LabelStatusInInWork.Text = "In Work  (IW)"
        '
        'LabelStatusInInReview
        '
        Me.LabelStatusInInReview.AutoSize = True
        Me.LabelStatusInInReview.Location = New System.Drawing.Point(132, 272)
        Me.LabelStatusInInReview.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusInInReview.Name = "LabelStatusInInReview"
        Me.LabelStatusInInReview.Size = New System.Drawing.Size(78, 16)
        Me.LabelStatusInInReview.TabIndex = 78
        Me.LabelStatusInInReview.Text = "In Review (IR)"
        '
        'LabelStatusInBaselined
        '
        Me.LabelStatusInBaselined.AutoSize = True
        Me.LabelStatusInBaselined.Location = New System.Drawing.Point(134, 237)
        Me.LabelStatusInBaselined.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusInBaselined.Name = "LabelStatusInBaselined"
        Me.LabelStatusInBaselined.Size = New System.Drawing.Size(75, 16)
        Me.LabelStatusInBaselined.TabIndex = 77
        Me.LabelStatusInBaselined.Text = "Baselined (B)"
        '
        'LabelStatusInAvailable
        '
        Me.LabelStatusInAvailable.AutoSize = True
        Me.LabelStatusInAvailable.Location = New System.Drawing.Point(141, 203)
        Me.LabelStatusInAvailable.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusInAvailable.Name = "LabelStatusInAvailable"
        Me.LabelStatusInAvailable.Size = New System.Drawing.Size(74, 16)
        Me.LabelStatusInAvailable.TabIndex = 76
        Me.LabelStatusInAvailable.Text = "Available (A)"
        '
        'RadioButtonReadOnlyChange
        '
        Me.RadioButtonReadOnlyChange.AutoSize = True
        Me.RadioButtonReadOnlyChange.Location = New System.Drawing.Point(30, 84)
        Me.RadioButtonReadOnlyChange.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonReadOnlyChange.Name = "RadioButtonReadOnlyChange"
        Me.RadioButtonReadOnlyChange.Size = New System.Drawing.Size(187, 20)
        Me.RadioButtonReadOnlyChange.TabIndex = 75
        Me.RadioButtonReadOnlyChange.TabStop = True
        Me.RadioButtonReadOnlyChange.Text = "Change status after processing"
        Me.RadioButtonReadOnlyChange.UseVisualStyleBackColor = True
        '
        'RadioButtonReadOnlyRevert
        '
        Me.RadioButtonReadOnlyRevert.AutoSize = True
        Me.RadioButtonReadOnlyRevert.Checked = True
        Me.RadioButtonReadOnlyRevert.Location = New System.Drawing.Point(30, 49)
        Me.RadioButtonReadOnlyRevert.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonReadOnlyRevert.Name = "RadioButtonReadOnlyRevert"
        Me.RadioButtonReadOnlyRevert.Size = New System.Drawing.Size(241, 20)
        Me.RadioButtonReadOnlyRevert.TabIndex = 74
        Me.RadioButtonReadOnlyRevert.TabStop = True
        Me.RadioButtonReadOnlyRevert.Text = "Revert to previous status after processing"
        Me.RadioButtonReadOnlyRevert.UseVisualStyleBackColor = True
        '
        'CheckBoxProcessReadOnly
        '
        Me.CheckBoxProcessReadOnly.AutoSize = True
        Me.CheckBoxProcessReadOnly.Location = New System.Drawing.Point(10, 14)
        Me.CheckBoxProcessReadOnly.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxProcessReadOnly.Name = "CheckBoxProcessReadOnly"
        Me.CheckBoxProcessReadOnly.Size = New System.Drawing.Size(318, 20)
        Me.CheckBoxProcessReadOnly.TabIndex = 73
        Me.CheckBoxProcessReadOnly.Text = "Process files as Available regardless of document Status"
        Me.CheckBoxProcessReadOnly.UseVisualStyleBackColor = True
        '
        'TabPageSorting
        '
        Me.TabPageSorting.Controls.Add(Me.TextBoxRandomSampleFraction)
        Me.TabPageSorting.Controls.Add(Me.LabelRandomSampleFraction)
        Me.TabPageSorting.Controls.Add(Me.RadioButtonListSortRandomSample)
        Me.TabPageSorting.Controls.Add(Me.CheckBoxListIncludeNoDependencies)
        Me.TabPageSorting.Controls.Add(Me.RadioButtonListSortDependency)
        Me.TabPageSorting.Controls.Add(Me.RadioButtonListSortAlphabetical)
        Me.TabPageSorting.Controls.Add(Me.RadioButtonListSortNone)
        Me.TabPageSorting.ImageKey = "list"
        Me.TabPageSorting.Location = New System.Drawing.Point(4, 24)
        Me.TabPageSorting.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageSorting.Name = "TabPageSorting"
        Me.TabPageSorting.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageSorting.Size = New System.Drawing.Size(532, 790)
        Me.TabPageSorting.TabIndex = 4
        Me.TabPageSorting.Text = "Sorting"
        Me.TabPageSorting.UseVisualStyleBackColor = True
        '
        'TextBoxRandomSampleFraction
        '
        Me.TextBoxRandomSampleFraction.Location = New System.Drawing.Point(236, 142)
        Me.TextBoxRandomSampleFraction.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextBoxRandomSampleFraction.Name = "TextBoxRandomSampleFraction"
        Me.TextBoxRandomSampleFraction.Size = New System.Drawing.Size(116, 23)
        Me.TextBoxRandomSampleFraction.TabIndex = 55
        Me.TextBoxRandomSampleFraction.Text = "0.1"
        '
        'LabelRandomSampleFraction
        '
        Me.LabelRandomSampleFraction.AutoSize = True
        Me.LabelRandomSampleFraction.Location = New System.Drawing.Point(135, 146)
        Me.LabelRandomSampleFraction.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelRandomSampleFraction.Name = "LabelRandomSampleFraction"
        Me.LabelRandomSampleFraction.Size = New System.Drawing.Size(90, 16)
        Me.LabelRandomSampleFraction.TabIndex = 54
        Me.LabelRandomSampleFraction.Text = "Sample fraction"
        '
        'RadioButtonListSortRandomSample
        '
        Me.RadioButtonListSortRandomSample.AutoSize = True
        Me.RadioButtonListSortRandomSample.Location = New System.Drawing.Point(10, 143)
        Me.RadioButtonListSortRandomSample.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonListSortRandomSample.Name = "RadioButtonListSortRandomSample"
        Me.RadioButtonListSortRandomSample.Size = New System.Drawing.Size(109, 20)
        Me.RadioButtonListSortRandomSample.TabIndex = 53
        Me.RadioButtonListSortRandomSample.TabStop = True
        Me.RadioButtonListSortRandomSample.Text = "Random sample"
        Me.RadioButtonListSortRandomSample.UseVisualStyleBackColor = True
        '
        'CheckBoxListIncludeNoDependencies
        '
        Me.CheckBoxListIncludeNoDependencies.AutoSize = True
        Me.CheckBoxListIncludeNoDependencies.Location = New System.Drawing.Point(10, 110)
        Me.CheckBoxListIncludeNoDependencies.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxListIncludeNoDependencies.Name = "CheckBoxListIncludeNoDependencies"
        Me.CheckBoxListIncludeNoDependencies.Size = New System.Drawing.Size(350, 20)
        Me.CheckBoxListIncludeNoDependencies.TabIndex = 52
        Me.CheckBoxListIncludeNoDependencies.Text = "Include files with no Part Copy dependencies in search results"
        Me.CheckBoxListIncludeNoDependencies.UseVisualStyleBackColor = True
        '
        'RadioButtonListSortDependency
        '
        Me.RadioButtonListSortDependency.AutoSize = True
        Me.RadioButtonListSortDependency.Location = New System.Drawing.Point(10, 78)
        Me.RadioButtonListSortDependency.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonListSortDependency.Name = "RadioButtonListSortDependency"
        Me.RadioButtonListSortDependency.Size = New System.Drawing.Size(172, 20)
        Me.RadioButtonListSortDependency.TabIndex = 51
        Me.RadioButtonListSortDependency.Text = "Sorted in dependency order"
        Me.RadioButtonListSortDependency.UseVisualStyleBackColor = True
        '
        'RadioButtonListSortAlphabetical
        '
        Me.RadioButtonListSortAlphabetical.AutoSize = True
        Me.RadioButtonListSortAlphabetical.Checked = True
        Me.RadioButtonListSortAlphabetical.Location = New System.Drawing.Point(10, 45)
        Me.RadioButtonListSortAlphabetical.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonListSortAlphabetical.Name = "RadioButtonListSortAlphabetical"
        Me.RadioButtonListSortAlphabetical.Size = New System.Drawing.Size(171, 20)
        Me.RadioButtonListSortAlphabetical.TabIndex = 50
        Me.RadioButtonListSortAlphabetical.TabStop = True
        Me.RadioButtonListSortAlphabetical.Text = "Sorted in alphabetical order"
        Me.RadioButtonListSortAlphabetical.UseVisualStyleBackColor = True
        '
        'RadioButtonListSortNone
        '
        Me.RadioButtonListSortNone.AutoSize = True
        Me.RadioButtonListSortNone.Location = New System.Drawing.Point(10, 12)
        Me.RadioButtonListSortNone.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonListSortNone.Name = "RadioButtonListSortNone"
        Me.RadioButtonListSortNone.Size = New System.Drawing.Size(73, 20)
        Me.RadioButtonListSortNone.TabIndex = 49
        Me.RadioButtonListSortNone.Text = "Unsorted"
        Me.RadioButtonListSortNone.UseVisualStyleBackColor = True
        '
        'TabPageGeneral
        '
        Me.TabPageGeneral.AutoScroll = True
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxPropertyFilterCheckDraftFile)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxUseCurrentSession)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxNoUpdateMRU)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxWarnSave)
        Me.TabPageGeneral.Controls.Add(Me.LabelFontSize)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxFontSize)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxPropertyFilterFollowDraftLinks)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxBackgroundProcessing)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxRememberTasks)
        Me.TabPageGeneral.ImageKey = "config"
        Me.TabPageGeneral.Location = New System.Drawing.Point(4, 24)
        Me.TabPageGeneral.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageGeneral.Name = "TabPageGeneral"
        Me.TabPageGeneral.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageGeneral.Size = New System.Drawing.Size(532, 790)
        Me.TabPageGeneral.TabIndex = 6
        Me.TabPageGeneral.Text = "General"
        Me.TabPageGeneral.UseVisualStyleBackColor = True
        '
        'CheckBoxPropertyFilterCheckDraftFile
        '
        Me.CheckBoxPropertyFilterCheckDraftFile.AutoSize = True
        Me.CheckBoxPropertyFilterCheckDraftFile.Location = New System.Drawing.Point(6, 300)
        Me.CheckBoxPropertyFilterCheckDraftFile.Name = "CheckBoxPropertyFilterCheckDraftFile"
        Me.CheckBoxPropertyFilterCheckDraftFile.Size = New System.Drawing.Size(301, 20)
        Me.CheckBoxPropertyFilterCheckDraftFile.TabIndex = 73
        Me.CheckBoxPropertyFilterCheckDraftFile.Text = "Property Filter -- Include the Draft file itself in search"
        Me.CheckBoxPropertyFilterCheckDraftFile.UseVisualStyleBackColor = True
        '
        'CheckBoxUseCurrentSession
        '
        Me.CheckBoxUseCurrentSession.AutoSize = True
        Me.CheckBoxUseCurrentSession.Location = New System.Drawing.Point(7, 6)
        Me.CheckBoxUseCurrentSession.Name = "CheckBoxUseCurrentSession"
        Me.CheckBoxUseCurrentSession.Size = New System.Drawing.Size(226, 20)
        Me.CheckBoxUseCurrentSession.TabIndex = 72
        Me.CheckBoxUseCurrentSession.Text = "Use current Solid Edge session (if any)"
        Me.CheckBoxUseCurrentSession.UseVisualStyleBackColor = True
        '
        'CheckBoxNoUpdateMRU
        '
        Me.CheckBoxNoUpdateMRU.AutoSize = True
        Me.CheckBoxNoUpdateMRU.Location = New System.Drawing.Point(7, 89)
        Me.CheckBoxNoUpdateMRU.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxNoUpdateMRU.Name = "CheckBoxNoUpdateMRU"
        Me.CheckBoxNoUpdateMRU.Size = New System.Drawing.Size(311, 20)
        Me.CheckBoxNoUpdateMRU.TabIndex = 71
        Me.CheckBoxNoUpdateMRU.Text = "Do not show processed files in Most Recently Used list"
        Me.CheckBoxNoUpdateMRU.UseVisualStyleBackColor = True
        '
        'CheckBoxWarnSave
        '
        Me.CheckBoxWarnSave.AutoSize = True
        Me.CheckBoxWarnSave.Checked = True
        Me.CheckBoxWarnSave.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxWarnSave.Location = New System.Drawing.Point(7, 48)
        Me.CheckBoxWarnSave.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxWarnSave.Name = "CheckBoxWarnSave"
        Me.CheckBoxWarnSave.Size = New System.Drawing.Size(186, 20)
        Me.CheckBoxWarnSave.TabIndex = 70
        Me.CheckBoxWarnSave.Text = "Warn me if file save is required"
        Me.CheckBoxWarnSave.UseVisualStyleBackColor = True
        '
        'LabelFontSize
        '
        Me.LabelFontSize.AutoSize = True
        Me.LabelFontSize.Location = New System.Drawing.Point(59, 133)
        Me.LabelFontSize.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelFontSize.Name = "LabelFontSize"
        Me.LabelFontSize.Size = New System.Drawing.Size(90, 16)
        Me.LabelFontSize.TabIndex = 55
        Me.LabelFontSize.Text = "File list font size"
        '
        'TextBoxFontSize
        '
        Me.TextBoxFontSize.Location = New System.Drawing.Point(7, 130)
        Me.TextBoxFontSize.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextBoxFontSize.Name = "TextBoxFontSize"
        Me.TextBoxFontSize.Size = New System.Drawing.Size(44, 23)
        Me.TextBoxFontSize.TabIndex = 54
        Me.TextBoxFontSize.Text = "8"
        '
        'CheckBoxPropertyFilterFollowDraftLinks
        '
        Me.CheckBoxPropertyFilterFollowDraftLinks.AutoSize = True
        Me.CheckBoxPropertyFilterFollowDraftLinks.Checked = True
        Me.CheckBoxPropertyFilterFollowDraftLinks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxPropertyFilterFollowDraftLinks.Location = New System.Drawing.Point(7, 255)
        Me.CheckBoxPropertyFilterFollowDraftLinks.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxPropertyFilterFollowDraftLinks.Name = "CheckBoxPropertyFilterFollowDraftLinks"
        Me.CheckBoxPropertyFilterFollowDraftLinks.Size = New System.Drawing.Size(351, 20)
        Me.CheckBoxPropertyFilterFollowDraftLinks.TabIndex = 52
        Me.CheckBoxPropertyFilterFollowDraftLinks.Text = "Property Filter -- Include Draft file model documents in search"
        Me.CheckBoxPropertyFilterFollowDraftLinks.UseVisualStyleBackColor = True
        '
        'CheckBoxBackgroundProcessing
        '
        Me.CheckBoxBackgroundProcessing.AutoSize = True
        Me.CheckBoxBackgroundProcessing.Location = New System.Drawing.Point(7, 214)
        Me.CheckBoxBackgroundProcessing.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxBackgroundProcessing.Name = "CheckBoxBackgroundProcessing"
        Me.CheckBoxBackgroundProcessing.Size = New System.Drawing.Size(248, 20)
        Me.CheckBoxBackgroundProcessing.TabIndex = 49
        Me.CheckBoxBackgroundProcessing.Text = "Process tasks in background (no graphics)"
        Me.CheckBoxBackgroundProcessing.UseVisualStyleBackColor = True
        '
        'CheckBoxRememberTasks
        '
        Me.CheckBoxRememberTasks.AutoSize = True
        Me.CheckBoxRememberTasks.Checked = True
        Me.CheckBoxRememberTasks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRememberTasks.Location = New System.Drawing.Point(7, 173)
        Me.CheckBoxRememberTasks.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxRememberTasks.Name = "CheckBoxRememberTasks"
        Me.CheckBoxRememberTasks.Size = New System.Drawing.Size(251, 20)
        Me.CheckBoxRememberTasks.TabIndex = 48
        Me.CheckBoxRememberTasks.Text = "Remember selected tasks between sessions"
        Me.CheckBoxRememberTasks.UseVisualStyleBackColor = True
        '
        'TabPageHelp
        '
        Me.TabPageHelp.AutoScroll = True
        Me.TabPageHelp.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageHelp.Controls.Add(Me.LabelReadmeNavigation2)
        Me.TabPageHelp.Controls.Add(Me.LabelReadmeNavigation1)
        Me.TabPageHelp.Controls.Add(Me.LinkLabelGitHubReadme)
        Me.TabPageHelp.Controls.Add(Me.PictureBoxTableOfContents)
        Me.TabPageHelp.ImageKey = "Help"
        Me.TabPageHelp.Location = New System.Drawing.Point(4, 24)
        Me.TabPageHelp.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageHelp.Name = "TabPageHelp"
        Me.TabPageHelp.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageHelp.Size = New System.Drawing.Size(551, 648)
        Me.TabPageHelp.TabIndex = 6
        Me.TabPageHelp.Text = "Help"
        '
        'LabelReadmeNavigation2
        '
        Me.LabelReadmeNavigation2.AutoSize = True
        Me.LabelReadmeNavigation2.Location = New System.Drawing.Point(18, 119)
        Me.LabelReadmeNavigation2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelReadmeNavigation2.Name = "LabelReadmeNavigation2"
        Me.LabelReadmeNavigation2.Size = New System.Drawing.Size(162, 16)
        Me.LabelReadmeNavigation2.TabIndex = 6
        Me.LabelReadmeNavigation2.Text = "as shown in the image below."
        '
        'LabelReadmeNavigation1
        '
        Me.LabelReadmeNavigation1.AutoSize = True
        Me.LabelReadmeNavigation1.Location = New System.Drawing.Point(18, 98)
        Me.LabelReadmeNavigation1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelReadmeNavigation1.Name = "LabelReadmeNavigation1"
        Me.LabelReadmeNavigation1.Size = New System.Drawing.Size(424, 16)
        Me.LabelReadmeNavigation1.TabIndex = 2
        Me.LabelReadmeNavigation1.Text = "Access the Table of Contents on GitHub by clicking the icon left of README.md"
        '
        'LinkLabelGitHubReadme
        '
        Me.LinkLabelGitHubReadme.AutoSize = True
        Me.LinkLabelGitHubReadme.Location = New System.Drawing.Point(18, 35)
        Me.LinkLabelGitHubReadme.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabelGitHubReadme.Name = "LinkLabelGitHubReadme"
        Me.LinkLabelGitHubReadme.Size = New System.Drawing.Size(180, 16)
        Me.LinkLabelGitHubReadme.TabIndex = 1
        Me.LinkLabelGitHubReadme.TabStop = True
        Me.LinkLabelGitHubReadme.Text = "text populated in Form1.Startup()"
        '
        'PictureBoxTableOfContents
        '
        Me.PictureBoxTableOfContents.Image = Global.Housekeeper.My.Resources.Resources.table_of_contents
        Me.PictureBoxTableOfContents.Location = New System.Drawing.Point(18, 161)
        Me.PictureBoxTableOfContents.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PictureBoxTableOfContents.Name = "PictureBoxTableOfContents"
        Me.PictureBoxTableOfContents.Size = New System.Drawing.Size(364, 394)
        Me.PictureBoxTableOfContents.TabIndex = 5
        Me.PictureBoxTableOfContents.TabStop = False
        '
        'TextBoxStatus
        '
        Me.TextBoxStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStatus.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel1.SetColumnSpan(Me.TextBoxStatus, 3)
        Me.TextBoxStatus.Location = New System.Drawing.Point(2, 9)
        Me.TextBoxStatus.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxStatus.Name = "TextBoxStatus"
        Me.TextBoxStatus.Size = New System.Drawing.Size(555, 23)
        Me.TextBoxStatus.TabIndex = 1
        Me.TextBoxStatus.Text = "Select file(s) to process OR Select none to process all (ESC to clear selections)" &
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
        'LabelTimeRemaining
        '
        Me.LabelTimeRemaining.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelTimeRemaining.AutoSize = True
        Me.LabelTimeRemaining.Location = New System.Drawing.Point(12, 722)
        Me.LabelTimeRemaining.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelTimeRemaining.Name = "LabelTimeRemaining"
        Me.LabelTimeRemaining.Size = New System.Drawing.Size(0, 16)
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
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonProcess, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonCancel, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxStatus, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 676)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(559, 76)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'ButtonProcess
        '
        Me.ButtonProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonProcess.Image = Global.Housekeeper.My.Resources.Resources.Play
        Me.ButtonProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonProcess.Location = New System.Drawing.Point(327, 36)
        Me.ButtonProcess.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonProcess.Name = "ButtonProcess"
        Me.ButtonProcess.Size = New System.Drawing.Size(113, 38)
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
        Me.ButtonCancel.Location = New System.Drawing.Point(444, 36)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(113, 38)
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
        'NotifyIcon1
        '
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(559, 752)
        Me.Controls.Add(Me.LabelTimeRemaining)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(400, 200)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(575, 450)
        Me.Name = "Form1"
        Me.Text = "Solid Edge Housekeeper"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageHome.ResumeLayout(False)
        Me.TabPageHome.PerformLayout()
        Me.ToolStrip_Filter.ResumeLayout(False)
        Me.ToolStrip_Filter.PerformLayout()
        Me.ToolStrip_List.ResumeLayout(False)
        Me.ToolStrip_List.PerformLayout()
        Me.TabPageTasks.ResumeLayout(False)
        Me.TaskFooterPanel.ResumeLayout(False)
        Me.TaskHeaderPanel.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TabPageConfiguration.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPageTopLevelAssy.ResumeLayout(False)
        Me.TabPageTopLevelAssy.PerformLayout()
        Me.TabPageStatus.ResumeLayout(False)
        Me.TabPageStatus.PerformLayout()
        Me.GroupBoxStatusInR.ResumeLayout(False)
        Me.GroupBoxStatusInR.PerformLayout()
        Me.GroupBoxStatusInO.ResumeLayout(False)
        Me.GroupBoxStatusInO.PerformLayout()
        Me.GroupBoxStatusInIW.ResumeLayout(False)
        Me.GroupBoxStatusInIW.PerformLayout()
        Me.GroupBoxStatusInIR.ResumeLayout(False)
        Me.GroupBoxStatusInIR.PerformLayout()
        Me.GroupBoxStatusInB.ResumeLayout(False)
        Me.GroupBoxStatusInB.PerformLayout()
        Me.GroupBoxStatusInA.ResumeLayout(False)
        Me.GroupBoxStatusInA.PerformLayout()
        Me.TabPageSorting.ResumeLayout(False)
        Me.TabPageSorting.PerformLayout()
        Me.TabPageGeneral.ResumeLayout(False)
        Me.TabPageGeneral.PerformLayout()
        Me.TabPageHelp.ResumeLayout(False)
        Me.TabPageHelp.PerformLayout()
        CType(Me.PictureBoxTableOfContents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Menu_ListViewFile.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageHome As TabPage
    Friend WithEvents TextBoxStatus As TextBox
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonProcess As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TabPageConfiguration As TabPage
    Friend WithEvents TabPageHelp As TabPage
    Friend WithEvents LabelTimeRemaining As Label
    Friend WithEvents PrintDialog1 As PrintDialog
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
    Friend WithEvents BT_FindLinks As ToolStripMenuItem
    Friend WithEvents LinkLabelGitHubReadme As LinkLabel
    Friend WithEvents LabelReadmeNavigation1 As Label
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents PictureBoxTableOfContents As PictureBox
    Friend WithEvents LabelReadmeNavigation2 As Label
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPageTopLevelAssy As TabPage
    Friend WithEvents CheckBoxTLAIgnoreIncludeInReports As CheckBox
    Friend WithEvents CheckBoxTLAIncludePartCopies As CheckBox
    Friend WithEvents CheckBoxDraftAndModelSameName As CheckBox
    Friend WithEvents CheckBoxWarnBareTLA As CheckBox
    Friend WithEvents CheckBoxTLAAutoIncludeTLF As CheckBox
    Friend WithEvents ButtonFastSearchScopeFilename As Button
    Friend WithEvents TextBoxFastSearchScopeFilename As TextBox
    Friend WithEvents LabelFastSearchScopeFilename As Label
    Friend WithEvents CheckBoxTLAReportUnrelatedFiles As CheckBox
    Friend WithEvents RadioButtonTLATopDown As RadioButton
    Friend WithEvents RadioButtonTLABottomUp As RadioButton
    Friend WithEvents TabPageStatus As TabPage
    Friend WithEvents LabelStatusAfter As Label
    Friend WithEvents LabelStatusBefore As Label
    Friend WithEvents LabelStatusOutReleased As Label
    Friend WithEvents LabelStatusOutObsolete As Label
    Friend WithEvents LabelStatusOutIW As Label
    Friend WithEvents LabelStatusOutInReview As Label
    Friend WithEvents LabelStatusOutBaselined As Label
    Friend WithEvents LabelStatusOutAvailable As Label
    Friend WithEvents GroupBoxStatusInR As GroupBox
    Friend WithEvents RadioButtonStatusRtoR As RadioButton
    Friend WithEvents RadioButtonStatusRtoO As RadioButton
    Friend WithEvents RadioButtonStatusRtoIW As RadioButton
    Friend WithEvents RadioButtonStatusRtoIR As RadioButton
    Friend WithEvents RadioButtonStatusRtoB As RadioButton
    Friend WithEvents RadioButtonStatusRtoA As RadioButton
    Friend WithEvents GroupBoxStatusInO As GroupBox
    Friend WithEvents RadioButtonStatusOtoR As RadioButton
    Friend WithEvents RadioButtonStatusOtoO As RadioButton
    Friend WithEvents RadioButtonStatusOtoIW As RadioButton
    Friend WithEvents RadioButtonStatusOtoIR As RadioButton
    Friend WithEvents RadioButtonStatusOtoB As RadioButton
    Friend WithEvents RadioButtonStatusOtoA As RadioButton
    Friend WithEvents GroupBoxStatusInIW As GroupBox
    Friend WithEvents RadioButtonStatusIWtoR As RadioButton
    Friend WithEvents RadioButtonStatusIWtoO As RadioButton
    Friend WithEvents RadioButtonStatusIWtoIW As RadioButton
    Friend WithEvents RadioButtonStatusIWtoIR As RadioButton
    Friend WithEvents RadioButtonStatusIWtoB As RadioButton
    Friend WithEvents RadioButtonStatusIWtoA As RadioButton
    Friend WithEvents GroupBoxStatusInIR As GroupBox
    Friend WithEvents RadioButtonStatusIRtoR As RadioButton
    Friend WithEvents RadioButtonStatusIRtoO As RadioButton
    Friend WithEvents RadioButtonStatusIRtoIW As RadioButton
    Friend WithEvents RadioButtonStatusIRtoIR As RadioButton
    Friend WithEvents RadioButtonStatusIRtoB As RadioButton
    Friend WithEvents RadioButtonStatusIRtoA As RadioButton
    Friend WithEvents GroupBoxStatusInB As GroupBox
    Friend WithEvents RadioButtonStatusBtoR As RadioButton
    Friend WithEvents RadioButtonStatusBtoO As RadioButton
    Friend WithEvents RadioButtonStatusBtoIW As RadioButton
    Friend WithEvents RadioButtonStatusBtoIR As RadioButton
    Friend WithEvents RadioButtonStatusBtoB As RadioButton
    Friend WithEvents RadioButtonStatusBtoA As RadioButton
    Friend WithEvents GroupBoxStatusInA As GroupBox
    Friend WithEvents RadioButtonStatusAtoR As RadioButton
    Friend WithEvents RadioButtonStatusAtoO As RadioButton
    Friend WithEvents RadioButtonStatusAtoIW As RadioButton
    Friend WithEvents RadioButtonStatusAtoIR As RadioButton
    Friend WithEvents RadioButtonStatusAtoB As RadioButton
    Friend WithEvents RadioButtonStatusAtoA As RadioButton
    Friend WithEvents LabelStatusInReleased As Label
    Friend WithEvents LabelStatusInObsolete As Label
    Friend WithEvents LabelStatusInInWork As Label
    Friend WithEvents LabelStatusInInReview As Label
    Friend WithEvents LabelStatusInBaselined As Label
    Friend WithEvents LabelStatusInAvailable As Label
    Friend WithEvents RadioButtonReadOnlyChange As RadioButton
    Friend WithEvents RadioButtonReadOnlyRevert As RadioButton
    Friend WithEvents CheckBoxProcessReadOnly As CheckBox
    Friend WithEvents TabPageSorting As TabPage
    Friend WithEvents TextBoxRandomSampleFraction As TextBox
    Friend WithEvents LabelRandomSampleFraction As Label
    Friend WithEvents RadioButtonListSortRandomSample As RadioButton
    Friend WithEvents CheckBoxListIncludeNoDependencies As CheckBox
    Friend WithEvents RadioButtonListSortDependency As RadioButton
    Friend WithEvents RadioButtonListSortAlphabetical As RadioButton
    Friend WithEvents RadioButtonListSortNone As RadioButton
    Friend WithEvents TabPageGeneral As TabPage
    Friend WithEvents CheckBoxPropertyFilterFollowDraftLinks As CheckBox
    Friend WithEvents CheckBoxBackgroundProcessing As CheckBox
    Friend WithEvents CheckBoxRememberTasks As CheckBox
    Friend WithEvents TextBoxFontSize As TextBox
    Friend WithEvents LabelFontSize As Label
    Friend WithEvents TabPageTasks As TabPage
    Friend WithEvents CheckBoxNoUpdateMRU As CheckBox
    Friend WithEvents CheckBoxWarnSave As CheckBox
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents CheckBoxUseCurrentSession As CheckBox
    Friend WithEvents CheckBoxPropertyFilterCheckDraftFile As CheckBox
    Friend WithEvents TaskPanel As Panel
    Friend WithEvents TaskFooterPanel As Panel
    Friend WithEvents EditTaskListButton As Button
    Friend WithEvents TaskHeaderPanel As Panel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TaskHeaderEnableButton As Button
    Friend WithEvents TaskHeaderCollapseButton As Button
    Friend WithEvents TaskHeaderToggleAssemblyButton As Button
    Friend WithEvents TaskHeaderTogglePartButton As Button
    Friend WithEvents TaskHeaderToggleSheetmetalButton As Button
    Friend WithEvents TaskHeaderToggleDraftButton As Button
    Friend WithEvents TaskHeaderHelpButton As Button
    Friend WithEvents TaskHeaderNameLabel As Label
End Class
