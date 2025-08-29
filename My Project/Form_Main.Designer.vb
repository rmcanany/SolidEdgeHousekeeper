Imports ListViewExtended

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Main))
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("File sources", System.Windows.Forms.HorizontalAlignment.Left)
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageHome = New System.Windows.Forms.TabPage()
        Me.ColumnSelectionPanel = New System.Windows.Forms.Panel()
        Me.BT_DeleteCLBItem = New System.Windows.Forms.Button()
        Me.ButtonCloseListOfColumns = New System.Windows.Forms.Button()
        Me.ButtonAddToListOfColumns = New System.Windows.Forms.Button()
        Me.CLB_Properties = New System.Windows.Forms.CheckedListBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ListViewSources = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage_ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.ListViewFiles = New ListViewExtended.ListViewCollapsible()
        Me.FileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FilePath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ToolStrip_Filter = New System.Windows.Forms.ToolStrip()
        Me.CheckBoxEnablePropertyFilter = New System.Windows.Forms.ToolStripButton()
        Me.new_ButtonPropertyFilter = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.CheckBoxEnableFileWildcard = New System.Windows.Forms.ToolStripButton()
        Me.ComboBoxFileWildcard = New System.Windows.Forms.ToolStripComboBox()
        Me.new_ButtonFileSearchDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStrip_List = New System.Windows.Forms.ToolStrip()
        Me.BT_AddSingleFiles = New System.Windows.Forms.ToolStripButton()
        Me.BT_AddFolder = New System.Windows.Forms.ToolStripButton()
        Me.BT_AddFolderSubfolders = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_AddTeamCenter = New System.Windows.Forms.ToolStripButton()
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
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_Help = New System.Windows.Forms.ToolStripButton()
        Me.BT_ColumnsSelect = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
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
        Me.ExTableLayoutPanel4 = New Housekeeper.ExTableLayoutPanel()
        Me.LabelTLAListOptions = New System.Windows.Forms.Label()
        Me.CheckBoxTLAAutoIncludeTLF = New System.Windows.Forms.CheckBox()
        Me.CheckBoxDraftAndModelSameName = New System.Windows.Forms.CheckBox()
        Me.CheckBoxTLAIncludePartCopies = New System.Windows.Forms.CheckBox()
        Me.CheckBoxWarnBareTLA = New System.Windows.Forms.CheckBox()
        Me.CheckBoxTLAReportUnrelatedFiles = New System.Windows.Forms.CheckBox()
        Me.LabelTLASearchOptions = New System.Windows.Forms.Label()
        Me.RadioButtonTLABottomUp = New System.Windows.Forms.RadioButton()
        Me.RadioButtonTLATopDown = New System.Windows.Forms.RadioButton()
        Me.ExTableLayoutPanel5 = New Housekeeper.ExTableLayoutPanel()
        Me.ButtonFastSearchScopeFilename = New System.Windows.Forms.Button()
        Me.TextBoxFastSearchScopeFilename = New System.Windows.Forms.TextBox()
        Me.ExTableLayoutPanel10 = New Housekeeper.ExTableLayoutPanel()
        Me.ButtonLinkManagementFilename = New System.Windows.Forms.Button()
        Me.TextBoxLinkManagementFilename = New System.Windows.Forms.TextBox()
        Me.TabPageStatus = New System.Windows.Forms.TabPage()
        Me.ExTableLayoutPanel6 = New Housekeeper.ExTableLayoutPanel()
        Me.CheckBoxProcessAsAvailable = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LabelStatusInBaselined = New System.Windows.Forms.Label()
        Me.LabelStatusAfter = New System.Windows.Forms.Label()
        Me.LabelStatusInAvailable = New System.Windows.Forms.Label()
        Me.LabelStatusBefore = New System.Windows.Forms.Label()
        Me.LabelStatusInInReview = New System.Windows.Forms.Label()
        Me.LabelStatusOutReleased = New System.Windows.Forms.Label()
        Me.LabelStatusInInWork = New System.Windows.Forms.Label()
        Me.LabelStatusOutObsolete = New System.Windows.Forms.Label()
        Me.LabelStatusInObsolete = New System.Windows.Forms.Label()
        Me.LabelStatusOutIW = New System.Windows.Forms.Label()
        Me.LabelStatusInReleased = New System.Windows.Forms.Label()
        Me.LabelStatusOutInReview = New System.Windows.Forms.Label()
        Me.GroupBoxStatusInA = New System.Windows.Forms.GroupBox()
        Me.RadioButtonStatusAtoR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusAtoO = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusAtoIW = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusAtoIR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusAtoB = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusAtoA = New System.Windows.Forms.RadioButton()
        Me.LabelStatusOutBaselined = New System.Windows.Forms.Label()
        Me.GroupBoxStatusInB = New System.Windows.Forms.GroupBox()
        Me.RadioButtonStatusBtoR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusBtoO = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusBtoIW = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusBtoIR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusBtoB = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusBtoA = New System.Windows.Forms.RadioButton()
        Me.LabelStatusOutAvailable = New System.Windows.Forms.Label()
        Me.GroupBoxStatusInIR = New System.Windows.Forms.GroupBox()
        Me.RadioButtonStatusIRtoR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIRtoO = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIRtoIW = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIRtoIR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIRtoB = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIRtoA = New System.Windows.Forms.RadioButton()
        Me.GroupBoxStatusInR = New System.Windows.Forms.GroupBox()
        Me.RadioButtonStatusRtoR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusRtoO = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusRtoIW = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusRtoIR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusRtoB = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusRtoA = New System.Windows.Forms.RadioButton()
        Me.GroupBoxStatusInIW = New System.Windows.Forms.GroupBox()
        Me.RadioButtonStatusIWtoR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIWtoO = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIWtoIW = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIWtoIR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIWtoB = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusIWtoA = New System.Windows.Forms.RadioButton()
        Me.GroupBoxStatusInO = New System.Windows.Forms.GroupBox()
        Me.RadioButtonStatusOtoR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusOtoO = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusOtoIW = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusOtoIR = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusOtoB = New System.Windows.Forms.RadioButton()
        Me.RadioButtonStatusOtoA = New System.Windows.Forms.RadioButton()
        Me.RadioButtonProcessAsAvailableRevert = New System.Windows.Forms.RadioButton()
        Me.RadioButtonProcessAsAvailableChange = New System.Windows.Forms.RadioButton()
        Me.TabPageSorting = New System.Windows.Forms.TabPage()
        Me.ExTableLayoutPanel7 = New Housekeeper.ExTableLayoutPanel()
        Me.RadioButtonSortNone = New System.Windows.Forms.RadioButton()
        Me.ExTableLayoutPanel8 = New Housekeeper.ExTableLayoutPanel()
        Me.TextBoxSortRandomSampleFraction = New System.Windows.Forms.TextBox()
        Me.LabelSortRandomSampleFraction = New System.Windows.Forms.Label()
        Me.RadioButtonSortRandomSample = New System.Windows.Forms.RadioButton()
        Me.CheckBoxSortIncludeNoDependencies = New System.Windows.Forms.CheckBox()
        Me.RadioButtonSortDependency = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSortAlphabetical = New System.Windows.Forms.RadioButton()
        Me.CheckBoxKeepUnsortedDuplicates = New System.Windows.Forms.CheckBox()
        Me.TabPageTemplates = New System.Windows.Forms.TabPage()
        Me.ExTableLayoutPanel1 = New Housekeeper.ExTableLayoutPanel()
        Me.ButtonAssemblyTemplate = New System.Windows.Forms.Button()
        Me.TextBoxAssemblyTemplate = New System.Windows.Forms.TextBox()
        Me.ButtonPartTemplate = New System.Windows.Forms.Button()
        Me.TextBoxPartTemplate = New System.Windows.Forms.TextBox()
        Me.ButtonSheetmetalTemplate = New System.Windows.Forms.Button()
        Me.TextBoxSheetmetalTemplate = New System.Windows.Forms.TextBox()
        Me.ButtonDraftTemplate = New System.Windows.Forms.Button()
        Me.TextBoxDraftTemplate = New System.Windows.Forms.TextBox()
        Me.ButtonMaterialTable = New System.Windows.Forms.Button()
        Me.TextBoxMaterialTable = New System.Windows.Forms.TextBox()
        Me.LabelCustomizeTemplatePropertyDict = New System.Windows.Forms.Label()
        Me.ButtonCustomizePropertiesData = New System.Windows.Forms.Button()
        Me.ButtonUpdatePropertiesData = New System.Windows.Forms.Button()
        Me.LabelUpdateProperties = New System.Windows.Forms.Label()
        Me.TabPageServerQuery = New System.Windows.Forms.TabPage()
        Me.ExTableLayoutPanel9 = New Housekeeper.ExTableLayoutPanel()
        Me.TextBoxServerConnectionString = New System.Windows.Forms.TextBox()
        Me.LabelServerConnectionString = New System.Windows.Forms.Label()
        Me.LabelServerQuery = New System.Windows.Forms.Label()
        Me.FastColoredServerQuery = New FastColoredTextBoxNS.FastColoredTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPageGeneral = New System.Windows.Forms.TabPage()
        Me.ExTableLayoutPanel2 = New Housekeeper.ExTableLayoutPanel()
        Me.CheckBoxUseCurrentSession = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPropertyFilterIncludeDraftItself = New System.Windows.Forms.CheckBox()
        Me.CheckBoxWarnSave = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPropertyFilterIncludeDraftModel = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoUpdateMRU = New System.Windows.Forms.CheckBox()
        Me.CheckBoxRunInBackground = New System.Windows.Forms.CheckBox()
        Me.ExTableLayoutPanel3 = New Housekeeper.ExTableLayoutPanel()
        Me.TextBoxFileListFontSize = New System.Windows.Forms.TextBox()
        Me.LabelFontSize = New System.Windows.Forms.Label()
        Me.CheckBoxRememberTasks = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCheckForNewerVersion = New System.Windows.Forms.CheckBox()
        Me.CheckBoxGroupFiles = New System.Windows.Forms.CheckBox()
        Me.ExTableLayoutPanel11 = New Housekeeper.ExTableLayoutPanel()
        Me.TextBoxListViewUpdateFrequency = New System.Windows.Forms.TextBox()
        Me.LabelListViewUpdateFrequency = New System.Windows.Forms.Label()
        Me.CheckBoxRemindFilelistUpdate = New System.Windows.Forms.CheckBox()
        Me.CheckBoxProcessDraftsInactive = New System.Windows.Forms.CheckBox()
        Me.ExTableLayoutPanel12 = New Housekeeper.ExTableLayoutPanel()
        Me.ComboBoxExpressionEditorLanguage = New System.Windows.Forms.ComboBox()
        Me.LabelExpressionEditorLanguage = New System.Windows.Forms.Label()
        Me.ToolStripPresets = New System.Windows.Forms.ToolStrip()
        Me.LabelPreset = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ComboBoxPresetName = New System.Windows.Forms.ToolStripComboBox()
        Me.ButtonPresetLoad = New System.Windows.Forms.ToolStripButton()
        Me.ButtonPresetSave = New System.Windows.Forms.ToolStripButton()
        Me.ButtonPresetDelete = New System.Windows.Forms.ToolStripButton()
        Me.TextBoxStatus = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.LabelTimeRemaining = New System.Windows.Forms.Label()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonProcess = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonHelp = New System.Windows.Forms.Button()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.Menu_ListViewFile = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BT_Open = New System.Windows.Forms.ToolStripMenuItem()
        Me.BT_OpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_FindLinks = New System.Windows.Forms.ToolStripMenuItem()
        Me.BT_ProcessSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_ExcludeFromProcessing = New System.Windows.Forms.ToolStripMenuItem()
        Me.BT_RemoveFromList = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_MoveToRecycleBin = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.TabControl1.SuspendLayout()
        Me.TabPageHome.SuspendLayout()
        Me.ColumnSelectionPanel.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ToolStrip_Filter.SuspendLayout()
        Me.ToolStrip_List.SuspendLayout()
        Me.TabPageTasks.SuspendLayout()
        Me.TaskFooterPanel.SuspendLayout()
        Me.TaskHeaderPanel.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TabPageConfiguration.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPageTopLevelAssy.SuspendLayout()
        Me.ExTableLayoutPanel4.SuspendLayout()
        Me.ExTableLayoutPanel5.SuspendLayout()
        Me.ExTableLayoutPanel10.SuspendLayout()
        Me.TabPageStatus.SuspendLayout()
        Me.ExTableLayoutPanel6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBoxStatusInA.SuspendLayout()
        Me.GroupBoxStatusInB.SuspendLayout()
        Me.GroupBoxStatusInIR.SuspendLayout()
        Me.GroupBoxStatusInR.SuspendLayout()
        Me.GroupBoxStatusInIW.SuspendLayout()
        Me.GroupBoxStatusInO.SuspendLayout()
        Me.TabPageSorting.SuspendLayout()
        Me.ExTableLayoutPanel7.SuspendLayout()
        Me.ExTableLayoutPanel8.SuspendLayout()
        Me.TabPageTemplates.SuspendLayout()
        Me.ExTableLayoutPanel1.SuspendLayout()
        Me.TabPageServerQuery.SuspendLayout()
        Me.ExTableLayoutPanel9.SuspendLayout()
        CType(Me.FastColoredServerQuery, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageGeneral.SuspendLayout()
        Me.ExTableLayoutPanel2.SuspendLayout()
        Me.ExTableLayoutPanel3.SuspendLayout()
        Me.ExTableLayoutPanel11.SuspendLayout()
        Me.ExTableLayoutPanel12.SuspendLayout()
        Me.ToolStripPresets.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Menu_ListViewFile.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageHome)
        Me.TabControl1.Controls.Add(Me.TabPageTasks)
        Me.TabControl1.Controls.Add(Me.TabPageConfiguration)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.TabPage_ImageList
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(559, 647)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageHome
        '
        Me.TabPageHome.BackColor = System.Drawing.Color.White
        Me.TabPageHome.Controls.Add(Me.ColumnSelectionPanel)
        Me.TabPageHome.Controls.Add(Me.SplitContainer1)
        Me.TabPageHome.Controls.Add(Me.ToolStrip_Filter)
        Me.TabPageHome.Controls.Add(Me.ToolStrip_List)
        Me.TabPageHome.ImageKey = "list"
        Me.TabPageHome.Location = New System.Drawing.Point(4, 24)
        Me.TabPageHome.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPageHome.Name = "TabPageHome"
        Me.TabPageHome.Padding = New System.Windows.Forms.Padding(2, 2, 2, 0)
        Me.TabPageHome.Size = New System.Drawing.Size(551, 619)
        Me.TabPageHome.TabIndex = 0
        Me.TabPageHome.Text = "Home"
        '
        'ColumnSelectionPanel
        '
        Me.ColumnSelectionPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ColumnSelectionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ColumnSelectionPanel.Controls.Add(Me.BT_DeleteCLBItem)
        Me.ColumnSelectionPanel.Controls.Add(Me.ButtonCloseListOfColumns)
        Me.ColumnSelectionPanel.Controls.Add(Me.ButtonAddToListOfColumns)
        Me.ColumnSelectionPanel.Controls.Add(Me.CLB_Properties)
        Me.ColumnSelectionPanel.Location = New System.Drawing.Point(398, 2)
        Me.ColumnSelectionPanel.Name = "ColumnSelectionPanel"
        Me.ColumnSelectionPanel.Size = New System.Drawing.Size(146, 192)
        Me.ColumnSelectionPanel.TabIndex = 36
        Me.ColumnSelectionPanel.Visible = False
        '
        'BT_DeleteCLBItem
        '
        Me.BT_DeleteCLBItem.Image = CType(resources.GetObject("BT_DeleteCLBItem.Image"), System.Drawing.Image)
        Me.BT_DeleteCLBItem.Location = New System.Drawing.Point(121, 43)
        Me.BT_DeleteCLBItem.Name = "BT_DeleteCLBItem"
        Me.BT_DeleteCLBItem.Size = New System.Drawing.Size(20, 20)
        Me.BT_DeleteCLBItem.TabIndex = 38
        Me.BT_DeleteCLBItem.UseVisualStyleBackColor = True
        Me.BT_DeleteCLBItem.Visible = False
        '
        'ButtonCloseListOfColumns
        '
        Me.ButtonCloseListOfColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonCloseListOfColumns.Image = CType(resources.GetObject("ButtonCloseListOfColumns.Image"), System.Drawing.Image)
        Me.ButtonCloseListOfColumns.Location = New System.Drawing.Point(73, 162)
        Me.ButtonCloseListOfColumns.Name = "ButtonCloseListOfColumns"
        Me.ButtonCloseListOfColumns.Size = New System.Drawing.Size(71, 28)
        Me.ButtonCloseListOfColumns.TabIndex = 37
        Me.ButtonCloseListOfColumns.Text = "Close"
        Me.ButtonCloseListOfColumns.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonCloseListOfColumns.UseVisualStyleBackColor = True
        '
        'ButtonAddToListOfColumns
        '
        Me.ButtonAddToListOfColumns.Dock = System.Windows.Forms.DockStyle.Left
        Me.ButtonAddToListOfColumns.Image = CType(resources.GetObject("ButtonAddToListOfColumns.Image"), System.Drawing.Image)
        Me.ButtonAddToListOfColumns.Location = New System.Drawing.Point(0, 162)
        Me.ButtonAddToListOfColumns.Name = "ButtonAddToListOfColumns"
        Me.ButtonAddToListOfColumns.Size = New System.Drawing.Size(73, 28)
        Me.ButtonAddToListOfColumns.TabIndex = 36
        Me.ButtonAddToListOfColumns.Text = "Add"
        Me.ButtonAddToListOfColumns.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonAddToListOfColumns.UseVisualStyleBackColor = True
        '
        'CLB_Properties
        '
        Me.CLB_Properties.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.CLB_Properties.CheckOnClick = True
        Me.CLB_Properties.Dock = System.Windows.Forms.DockStyle.Top
        Me.CLB_Properties.FormattingEnabled = True
        Me.CLB_Properties.Items.AddRange(New Object() {"Name", "Path"})
        Me.CLB_Properties.Location = New System.Drawing.Point(0, 0)
        Me.CLB_Properties.Name = "CLB_Properties"
        Me.CLB_Properties.Size = New System.Drawing.Size(144, 162)
        Me.CLB_Properties.TabIndex = 35
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 27)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ListViewSources)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ListViewFiles)
        Me.SplitContainer1.Size = New System.Drawing.Size(547, 567)
        Me.SplitContainer1.SplitterDistance = 126
        Me.SplitContainer1.TabIndex = 38
        '
        'ListViewSources
        '
        Me.ListViewSources.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListViewSources.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListViewSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewSources.FullRowSelect = True
        ListViewGroup1.Header = "File sources"
        ListViewGroup1.Name = "Sources"
        Me.ListViewSources.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1})
        Me.ListViewSources.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListViewSources.HideSelection = False
        Me.ListViewSources.Location = New System.Drawing.Point(0, 0)
        Me.ListViewSources.Margin = New System.Windows.Forms.Padding(0)
        Me.ListViewSources.Name = "ListViewSources"
        Me.ListViewSources.ShowItemToolTips = True
        Me.ListViewSources.Size = New System.Drawing.Size(547, 126)
        Me.ListViewSources.SmallImageList = Me.TabPage_ImageList
        Me.ListViewSources.TabIndex = 37
        Me.ListViewSources.UseCompatibleStateImageBehavior = False
        Me.ListViewSources.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Type"
        Me.ColumnHeader1.Width = 150
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Path"
        Me.ColumnHeader2.Width = 300
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
        Me.TabPage_ImageList.Images.SetKeyName(21, "Query")
        Me.TabPage_ImageList.Images.SetKeyName(22, "teamcenter.png")
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
        Me.ListViewFiles.HideSelection = False
        Me.ListViewFiles.Location = New System.Drawing.Point(0, 0)
        Me.ListViewFiles.Margin = New System.Windows.Forms.Padding(0)
        Me.ListViewFiles.Name = "ListViewFiles"
        Me.ListViewFiles.OwnerDraw = True
        Me.ListViewFiles.ShowItemToolTips = True
        Me.ListViewFiles.Size = New System.Drawing.Size(547, 437)
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
        'ToolStrip_Filter
        '
        Me.ToolStrip_Filter.BackColor = System.Drawing.Color.White
        Me.ToolStrip_Filter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip_Filter.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip_Filter.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CheckBoxEnablePropertyFilter, Me.new_ButtonPropertyFilter, Me.ToolStripSeparator6, Me.CheckBoxEnableFileWildcard, Me.ComboBoxFileWildcard, Me.new_ButtonFileSearchDelete, Me.ToolStripSeparator7})
        Me.ToolStrip_Filter.Location = New System.Drawing.Point(2, 594)
        Me.ToolStrip_Filter.Name = "ToolStrip_Filter"
        Me.ToolStrip_Filter.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip_Filter.Size = New System.Drawing.Size(547, 25)
        Me.ToolStrip_Filter.TabIndex = 34
        Me.ToolStrip_Filter.Text = "ToolStrip1"
        '
        'CheckBoxEnablePropertyFilter
        '
        Me.CheckBoxEnablePropertyFilter.CheckOnClick = True
        Me.CheckBoxEnablePropertyFilter.Image = CType(resources.GetObject("CheckBoxEnablePropertyFilter.Image"), System.Drawing.Image)
        Me.CheckBoxEnablePropertyFilter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CheckBoxEnablePropertyFilter.Name = "CheckBoxEnablePropertyFilter"
        Me.CheckBoxEnablePropertyFilter.Size = New System.Drawing.Size(99, 22)
        Me.CheckBoxEnablePropertyFilter.Text = "Property filter"
        '
        'new_ButtonPropertyFilter
        '
        Me.new_ButtonPropertyFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.new_ButtonPropertyFilter.Image = CType(resources.GetObject("new_ButtonPropertyFilter.Image"), System.Drawing.Image)
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
        'CheckBoxEnableFileWildcard
        '
        Me.CheckBoxEnableFileWildcard.CheckOnClick = True
        Me.CheckBoxEnableFileWildcard.Image = CType(resources.GetObject("CheckBoxEnableFileWildcard.Image"), System.Drawing.Image)
        Me.CheckBoxEnableFileWildcard.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CheckBoxEnableFileWildcard.Name = "CheckBoxEnableFileWildcard"
        Me.CheckBoxEnableFileWildcard.Size = New System.Drawing.Size(74, 22)
        Me.CheckBoxEnableFileWildcard.Text = "Wildcard"
        '
        'ComboBoxFileWildcard
        '
        Me.ComboBoxFileWildcard.Name = "ComboBoxFileWildcard"
        Me.ComboBoxFileWildcard.Size = New System.Drawing.Size(140, 25)
        Me.ComboBoxFileWildcard.Sorted = True
        '
        'new_ButtonFileSearchDelete
        '
        Me.new_ButtonFileSearchDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.new_ButtonFileSearchDelete.Image = CType(resources.GetObject("new_ButtonFileSearchDelete.Image"), System.Drawing.Image)
        Me.new_ButtonFileSearchDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.new_ButtonFileSearchDelete.Name = "new_ButtonFileSearchDelete"
        Me.new_ButtonFileSearchDelete.Size = New System.Drawing.Size(23, 22)
        Me.new_ButtonFileSearchDelete.Text = "Clear"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStrip_List
        '
        Me.ToolStrip_List.BackColor = System.Drawing.Color.White
        Me.ToolStrip_List.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip_List.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BT_AddSingleFiles, Me.BT_AddFolder, Me.BT_AddFolderSubfolders, Me.ToolStripSeparator12, Me.BT_AddTeamCenter, Me.ToolStripSeparator4, Me.BT_TopLevelAsm, Me.BT_ASM_Folder, Me.ToolStripSeparator1, Me.BT_AddFromlist, Me.BT_ExportList, Me.ToolStripSeparator2, Me.BT_ErrorList, Me.BT_DeleteAll, Me.ToolStripSeparator3, Me.BT_Update, Me.ToolStripSeparator9, Me.BT_Help, Me.BT_ColumnsSelect, Me.ToolStripSeparator10, Me.new_CheckBoxFilterDft, Me.new_CheckBoxFilterPsm, Me.new_CheckBoxFilterPar, Me.new_CheckBoxFilterAsm, Me.ToolStripLabel1})
        Me.ToolStrip_List.Location = New System.Drawing.Point(2, 2)
        Me.ToolStrip_List.Name = "ToolStrip_List"
        Me.ToolStrip_List.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip_List.Size = New System.Drawing.Size(547, 25)
        Me.ToolStrip_List.TabIndex = 33
        Me.ToolStrip_List.Text = "ToolStrip1"
        '
        'BT_AddSingleFiles
        '
        Me.BT_AddSingleFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_AddSingleFiles.Image = CType(resources.GetObject("BT_AddSingleFiles.Image"), System.Drawing.Image)
        Me.BT_AddSingleFiles.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_AddSingleFiles.Name = "BT_AddSingleFiles"
        Me.BT_AddSingleFiles.Size = New System.Drawing.Size(23, 22)
        Me.BT_AddSingleFiles.Text = "Add single files"
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
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(6, 25)
        '
        'BT_AddTeamCenter
        '
        Me.BT_AddTeamCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_AddTeamCenter.Image = Global.Housekeeper.My.Resources.Resources.teamcenter
        Me.BT_AddTeamCenter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_AddTeamCenter.Name = "BT_AddTeamCenter"
        Me.BT_AddTeamCenter.Size = New System.Drawing.Size(23, 22)
        Me.BT_AddTeamCenter.Text = "Add TeamCenter"
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
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 25)
        '
        'BT_Help
        '
        Me.BT_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_Help.Image = CType(resources.GetObject("BT_Help.Image"), System.Drawing.Image)
        Me.BT_Help.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Help.Name = "BT_Help"
        Me.BT_Help.Size = New System.Drawing.Size(23, 22)
        Me.BT_Help.Text = "ToolStripButton2"
        Me.BT_Help.ToolTipText = "Help"
        '
        'BT_ColumnsSelect
        '
        Me.BT_ColumnsSelect.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.BT_ColumnsSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_ColumnsSelect.Image = CType(resources.GetObject("BT_ColumnsSelect.Image"), System.Drawing.Image)
        Me.BT_ColumnsSelect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_ColumnsSelect.Name = "BT_ColumnsSelect"
        Me.BT_ColumnsSelect.Size = New System.Drawing.Size(23, 22)
        Me.BT_ColumnsSelect.Text = "Columns configuration"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 25)
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
        Me.new_CheckBoxFilterDft.Text = "Filter DFT"
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
        Me.new_CheckBoxFilterPsm.Text = "Filter PSM"
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
        Me.new_CheckBoxFilterPar.Text = "Filter PAR"
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
        Me.new_CheckBoxFilterAsm.Text = "Filter ASM"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(56, 15)
        Me.ToolStripLabel1.Text = "File Type:"
        '
        'TabPageTasks
        '
        Me.TabPageTasks.AutoScroll = True
        Me.TabPageTasks.BackColor = System.Drawing.Color.White
        Me.TabPageTasks.Controls.Add(Me.TaskPanel)
        Me.TabPageTasks.Controls.Add(Me.TaskFooterPanel)
        Me.TabPageTasks.Controls.Add(Me.TaskHeaderPanel)
        Me.TabPageTasks.ImageKey = "se"
        Me.TabPageTasks.Location = New System.Drawing.Point(4, 24)
        Me.TabPageTasks.Name = "TabPageTasks"
        Me.TabPageTasks.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTasks.Size = New System.Drawing.Size(551, 619)
        Me.TabPageTasks.TabIndex = 7
        Me.TabPageTasks.Text = "Tasks"
        '
        'TaskPanel
        '
        Me.TaskPanel.AutoScroll = True
        Me.TaskPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TaskPanel.Location = New System.Drawing.Point(3, 37)
        Me.TaskPanel.Name = "TaskPanel"
        Me.TaskPanel.Size = New System.Drawing.Size(545, 533)
        Me.TaskPanel.TabIndex = 2
        '
        'TaskFooterPanel
        '
        Me.TaskFooterPanel.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TaskFooterPanel.Controls.Add(Me.EditTaskListButton)
        Me.TaskFooterPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TaskFooterPanel.Location = New System.Drawing.Point(3, 570)
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
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.LightSteelBlue
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
        Me.TaskHeaderEnableButton.Image = CType(resources.GetObject("TaskHeaderEnableButton.Image"), System.Drawing.Image)
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
        Me.TaskHeaderCollapseButton.Image = CType(resources.GetObject("TaskHeaderCollapseButton.Image"), System.Drawing.Image)
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
        Me.TaskHeaderToggleAssemblyButton.Image = CType(resources.GetObject("TaskHeaderToggleAssemblyButton.Image"), System.Drawing.Image)
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
        Me.TaskHeaderTogglePartButton.Image = CType(resources.GetObject("TaskHeaderTogglePartButton.Image"), System.Drawing.Image)
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
        Me.TaskHeaderToggleSheetmetalButton.Image = CType(resources.GetObject("TaskHeaderToggleSheetmetalButton.Image"), System.Drawing.Image)
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
        Me.TaskHeaderToggleDraftButton.Image = CType(resources.GetObject("TaskHeaderToggleDraftButton.Image"), System.Drawing.Image)
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
        Me.TaskHeaderHelpButton.Image = CType(resources.GetObject("TaskHeaderHelpButton.Image"), System.Drawing.Image)
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
        Me.TaskHeaderNameLabel.Size = New System.Drawing.Size(81, 15)
        Me.TaskHeaderNameLabel.TabIndex = 7
        Me.TaskHeaderNameLabel.Text = "TASK NAME"
        '
        'TabPageConfiguration
        '
        Me.TabPageConfiguration.AutoScroll = True
        Me.TabPageConfiguration.BackColor = System.Drawing.Color.White
        Me.TabPageConfiguration.Controls.Add(Me.TabControl2)
        Me.TabPageConfiguration.ImageKey = "Tools"
        Me.TabPageConfiguration.Location = New System.Drawing.Point(4, 24)
        Me.TabPageConfiguration.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageConfiguration.Name = "TabPageConfiguration"
        Me.TabPageConfiguration.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageConfiguration.Size = New System.Drawing.Size(551, 619)
        Me.TabPageConfiguration.TabIndex = 5
        Me.TabPageConfiguration.Text = "Configuration"
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPageTopLevelAssy)
        Me.TabControl2.Controls.Add(Me.TabPageStatus)
        Me.TabControl2.Controls.Add(Me.TabPageSorting)
        Me.TabControl2.Controls.Add(Me.TabPageTemplates)
        Me.TabControl2.Controls.Add(Me.TabPageServerQuery)
        Me.TabControl2.Controls.Add(Me.TabPageGeneral)
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.ImageList = Me.TabPage_ImageList
        Me.TabControl2.Location = New System.Drawing.Point(4, 3)
        Me.TabControl2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(543, 613)
        Me.TabControl2.TabIndex = 44
        '
        'TabPageTopLevelAssy
        '
        Me.TabPageTopLevelAssy.Controls.Add(Me.ExTableLayoutPanel4)
        Me.TabPageTopLevelAssy.ImageKey = "asm"
        Me.TabPageTopLevelAssy.Location = New System.Drawing.Point(4, 24)
        Me.TabPageTopLevelAssy.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageTopLevelAssy.Name = "TabPageTopLevelAssy"
        Me.TabPageTopLevelAssy.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageTopLevelAssy.Size = New System.Drawing.Size(535, 585)
        Me.TabPageTopLevelAssy.TabIndex = 2
        Me.TabPageTopLevelAssy.Text = "Top Level Assy"
        Me.TabPageTopLevelAssy.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel4
        '
        Me.ExTableLayoutPanel4.BackColor = System.Drawing.Color.White
        Me.ExTableLayoutPanel4.ColumnCount = 1
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelTLAListOptions, 0, 0)
        Me.ExTableLayoutPanel4.Controls.Add(Me.CheckBoxTLAAutoIncludeTLF, 0, 1)
        Me.ExTableLayoutPanel4.Controls.Add(Me.CheckBoxDraftAndModelSameName, 0, 8)
        Me.ExTableLayoutPanel4.Controls.Add(Me.CheckBoxTLAIncludePartCopies, 0, 3)
        Me.ExTableLayoutPanel4.Controls.Add(Me.CheckBoxWarnBareTLA, 0, 2)
        Me.ExTableLayoutPanel4.Controls.Add(Me.CheckBoxTLAReportUnrelatedFiles, 0, 4)
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelTLASearchOptions, 0, 5)
        Me.ExTableLayoutPanel4.Controls.Add(Me.RadioButtonTLABottomUp, 0, 7)
        Me.ExTableLayoutPanel4.Controls.Add(Me.RadioButtonTLATopDown, 0, 6)
        Me.ExTableLayoutPanel4.Controls.Add(Me.ExTableLayoutPanel5, 0, 9)
        Me.ExTableLayoutPanel4.Controls.Add(Me.ExTableLayoutPanel10, 0, 10)
        Me.ExTableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel4.Location = New System.Drawing.Point(4, 3)
        Me.ExTableLayoutPanel4.Name = "ExTableLayoutPanel4"
        Me.ExTableLayoutPanel4.RowCount = 12
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel4.Size = New System.Drawing.Size(527, 579)
        Me.ExTableLayoutPanel4.TabIndex = 22
        Me.ExTableLayoutPanel4.Task = Nothing
        '
        'LabelTLAListOptions
        '
        Me.LabelTLAListOptions.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelTLAListOptions.AutoSize = True
        Me.LabelTLAListOptions.Location = New System.Drawing.Point(3, 12)
        Me.LabelTLAListOptions.Name = "LabelTLAListOptions"
        Me.LabelTLAListOptions.Size = New System.Drawing.Size(82, 15)
        Me.LabelTLAListOptions.TabIndex = 0
        Me.LabelTLAListOptions.Text = "LIST OPTIONS"
        '
        'CheckBoxTLAAutoIncludeTLF
        '
        Me.CheckBoxTLAAutoIncludeTLF.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxTLAAutoIncludeTLF.AutoSize = True
        Me.CheckBoxTLAAutoIncludeTLF.Checked = True
        Me.CheckBoxTLAAutoIncludeTLF.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxTLAAutoIncludeTLF.Location = New System.Drawing.Point(3, 45)
        Me.CheckBoxTLAAutoIncludeTLF.Name = "CheckBoxTLAAutoIncludeTLF"
        Me.CheckBoxTLAAutoIncludeTLF.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxTLAAutoIncludeTLF.Size = New System.Drawing.Size(374, 19)
        Me.CheckBoxTLAAutoIncludeTLF.TabIndex = 17
        Me.CheckBoxTLAAutoIncludeTLF.Text = "Automatically include the folder if a top-level assembly is chosen"
        Me.CheckBoxTLAAutoIncludeTLF.UseVisualStyleBackColor = True
        '
        'CheckBoxDraftAndModelSameName
        '
        Me.CheckBoxDraftAndModelSameName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxDraftAndModelSameName.AutoSize = True
        Me.CheckBoxDraftAndModelSameName.Checked = True
        Me.CheckBoxDraftAndModelSameName.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxDraftAndModelSameName.Location = New System.Drawing.Point(3, 265)
        Me.CheckBoxDraftAndModelSameName.Name = "CheckBoxDraftAndModelSameName"
        Me.CheckBoxDraftAndModelSameName.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxDraftAndModelSameName.Size = New System.Drawing.Size(329, 19)
        Me.CheckBoxDraftAndModelSameName.TabIndex = 19
        Me.CheckBoxDraftAndModelSameName.Text = "Draft and model have same name and are in same folder"
        Me.CheckBoxDraftAndModelSameName.UseVisualStyleBackColor = True
        '
        'CheckBoxTLAIncludePartCopies
        '
        Me.CheckBoxTLAIncludePartCopies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxTLAIncludePartCopies.AutoSize = True
        Me.CheckBoxTLAIncludePartCopies.Location = New System.Drawing.Point(3, 105)
        Me.CheckBoxTLAIncludePartCopies.Name = "CheckBoxTLAIncludePartCopies"
        Me.CheckBoxTLAIncludePartCopies.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxTLAIncludePartCopies.Size = New System.Drawing.Size(502, 19)
        Me.CheckBoxTLAIncludePartCopies.TabIndex = 20
        Me.CheckBoxTLAIncludePartCopies.Text = "Include parents of all part copies in search results, even if they are not in the" &
    " top level assy"
        Me.CheckBoxTLAIncludePartCopies.UseVisualStyleBackColor = True
        '
        'CheckBoxWarnBareTLA
        '
        Me.CheckBoxWarnBareTLA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxWarnBareTLA.AutoSize = True
        Me.CheckBoxWarnBareTLA.Checked = True
        Me.CheckBoxWarnBareTLA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxWarnBareTLA.Location = New System.Drawing.Point(3, 75)
        Me.CheckBoxWarnBareTLA.Name = "CheckBoxWarnBareTLA"
        Me.CheckBoxWarnBareTLA.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxWarnBareTLA.Size = New System.Drawing.Size(420, 19)
        Me.CheckBoxWarnBareTLA.TabIndex = 18
        Me.CheckBoxWarnBareTLA.Text = "Warn me if a top-level assembly does not have a top-level folder specified"
        Me.CheckBoxWarnBareTLA.UseVisualStyleBackColor = True
        '
        'CheckBoxTLAReportUnrelatedFiles
        '
        Me.CheckBoxTLAReportUnrelatedFiles.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxTLAReportUnrelatedFiles.AutoSize = True
        Me.CheckBoxTLAReportUnrelatedFiles.Location = New System.Drawing.Point(3, 135)
        Me.CheckBoxTLAReportUnrelatedFiles.Name = "CheckBoxTLAReportUnrelatedFiles"
        Me.CheckBoxTLAReportUnrelatedFiles.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxTLAReportUnrelatedFiles.Size = New System.Drawing.Size(257, 19)
        Me.CheckBoxTLAReportUnrelatedFiles.TabIndex = 13
        Me.CheckBoxTLAReportUnrelatedFiles.Text = "Report files unrelated to top level assembly"
        Me.CheckBoxTLAReportUnrelatedFiles.UseVisualStyleBackColor = True
        '
        'LabelTLASearchOptions
        '
        Me.LabelTLASearchOptions.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelTLASearchOptions.AutoSize = True
        Me.LabelTLASearchOptions.Location = New System.Drawing.Point(3, 172)
        Me.LabelTLASearchOptions.Name = "LabelTLASearchOptions"
        Me.LabelTLASearchOptions.Size = New System.Drawing.Size(104, 15)
        Me.LabelTLASearchOptions.TabIndex = 21
        Me.LabelTLASearchOptions.Text = "SEARCH OPTIONS"
        '
        'RadioButtonTLABottomUp
        '
        Me.RadioButtonTLABottomUp.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonTLABottomUp.AutoSize = True
        Me.RadioButtonTLABottomUp.Location = New System.Drawing.Point(3, 235)
        Me.RadioButtonTLABottomUp.Name = "RadioButtonTLABottomUp"
        Me.RadioButtonTLABottomUp.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.RadioButtonTLABottomUp.Size = New System.Drawing.Size(328, 19)
        Me.RadioButtonTLABottomUp.TabIndex = 11
        Me.RadioButtonTLABottomUp.Text = "Bottom Up Search -- Best for general purpose directories"
        Me.RadioButtonTLABottomUp.UseVisualStyleBackColor = True
        '
        'RadioButtonTLATopDown
        '
        Me.RadioButtonTLATopDown.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonTLATopDown.AutoSize = True
        Me.RadioButtonTLATopDown.Checked = True
        Me.RadioButtonTLATopDown.Location = New System.Drawing.Point(3, 205)
        Me.RadioButtonTLATopDown.Name = "RadioButtonTLATopDown"
        Me.RadioButtonTLATopDown.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.RadioButtonTLATopDown.Size = New System.Drawing.Size(355, 19)
        Me.RadioButtonTLATopDown.TabIndex = 12
        Me.RadioButtonTLATopDown.TabStop = True
        Me.RadioButtonTLATopDown.Text = "Top Down Search -- Best for self-contained project directories"
        Me.RadioButtonTLATopDown.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel5
        '
        Me.ExTableLayoutPanel5.ColumnCount = 2
        Me.ExTableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.ExTableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel5.Controls.Add(Me.ButtonFastSearchScopeFilename, 0, 0)
        Me.ExTableLayoutPanel5.Controls.Add(Me.TextBoxFastSearchScopeFilename, 1, 0)
        Me.ExTableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel5.Location = New System.Drawing.Point(3, 293)
        Me.ExTableLayoutPanel5.Name = "ExTableLayoutPanel5"
        Me.ExTableLayoutPanel5.RowCount = 1
        Me.ExTableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel5.Size = New System.Drawing.Size(521, 34)
        Me.ExTableLayoutPanel5.TabIndex = 22
        Me.ExTableLayoutPanel5.Task = Nothing
        '
        'ButtonFastSearchScopeFilename
        '
        Me.ButtonFastSearchScopeFilename.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonFastSearchScopeFilename.Location = New System.Drawing.Point(2, 2)
        Me.ButtonFastSearchScopeFilename.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonFastSearchScopeFilename.Name = "ButtonFastSearchScopeFilename"
        Me.ButtonFastSearchScopeFilename.Size = New System.Drawing.Size(146, 30)
        Me.ButtonFastSearchScopeFilename.TabIndex = 16
        Me.ButtonFastSearchScopeFilename.Text = "FastSearchScope.txt"
        Me.ButtonFastSearchScopeFilename.UseVisualStyleBackColor = True
        '
        'TextBoxFastSearchScopeFilename
        '
        Me.TextBoxFastSearchScopeFilename.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFastSearchScopeFilename.Location = New System.Drawing.Point(152, 5)
        Me.TextBoxFastSearchScopeFilename.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFastSearchScopeFilename.Name = "TextBoxFastSearchScopeFilename"
        Me.TextBoxFastSearchScopeFilename.Size = New System.Drawing.Size(367, 23)
        Me.TextBoxFastSearchScopeFilename.TabIndex = 15
        '
        'ExTableLayoutPanel10
        '
        Me.ExTableLayoutPanel10.ColumnCount = 2
        Me.ExTableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.ExTableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel10.Controls.Add(Me.ButtonLinkManagementFilename, 0, 0)
        Me.ExTableLayoutPanel10.Controls.Add(Me.TextBoxLinkManagementFilename, 1, 0)
        Me.ExTableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel10.Location = New System.Drawing.Point(3, 333)
        Me.ExTableLayoutPanel10.Name = "ExTableLayoutPanel10"
        Me.ExTableLayoutPanel10.RowCount = 1
        Me.ExTableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.ExTableLayoutPanel10.Size = New System.Drawing.Size(521, 34)
        Me.ExTableLayoutPanel10.TabIndex = 23
        Me.ExTableLayoutPanel10.Task = Nothing
        '
        'ButtonLinkManagementFilename
        '
        Me.ButtonLinkManagementFilename.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonLinkManagementFilename.Location = New System.Drawing.Point(3, 3)
        Me.ButtonLinkManagementFilename.Name = "ButtonLinkManagementFilename"
        Me.ButtonLinkManagementFilename.Size = New System.Drawing.Size(144, 28)
        Me.ButtonLinkManagementFilename.TabIndex = 0
        Me.ButtonLinkManagementFilename.Text = "LinkMgmt.txt"
        Me.ButtonLinkManagementFilename.UseVisualStyleBackColor = True
        '
        'TextBoxLinkManagementFilename
        '
        Me.TextBoxLinkManagementFilename.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxLinkManagementFilename.Location = New System.Drawing.Point(153, 5)
        Me.TextBoxLinkManagementFilename.Name = "TextBoxLinkManagementFilename"
        Me.TextBoxLinkManagementFilename.Size = New System.Drawing.Size(365, 23)
        Me.TextBoxLinkManagementFilename.TabIndex = 1
        '
        'TabPageStatus
        '
        Me.TabPageStatus.Controls.Add(Me.ExTableLayoutPanel6)
        Me.TabPageStatus.ImageKey = "folder"
        Me.TabPageStatus.Location = New System.Drawing.Point(4, 24)
        Me.TabPageStatus.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageStatus.Name = "TabPageStatus"
        Me.TabPageStatus.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageStatus.Size = New System.Drawing.Size(535, 585)
        Me.TabPageStatus.TabIndex = 3
        Me.TabPageStatus.Text = "Status"
        Me.TabPageStatus.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel6
        '
        Me.ExTableLayoutPanel6.BackColor = System.Drawing.Color.White
        Me.ExTableLayoutPanel6.ColumnCount = 1
        Me.ExTableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel6.Controls.Add(Me.CheckBoxProcessAsAvailable, 0, 0)
        Me.ExTableLayoutPanel6.Controls.Add(Me.Panel1, 0, 3)
        Me.ExTableLayoutPanel6.Controls.Add(Me.RadioButtonProcessAsAvailableRevert, 0, 1)
        Me.ExTableLayoutPanel6.Controls.Add(Me.RadioButtonProcessAsAvailableChange, 0, 2)
        Me.ExTableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel6.Location = New System.Drawing.Point(4, 3)
        Me.ExTableLayoutPanel6.Name = "ExTableLayoutPanel6"
        Me.ExTableLayoutPanel6.RowCount = 5
        Me.ExTableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ExTableLayoutPanel6.Size = New System.Drawing.Size(527, 579)
        Me.ExTableLayoutPanel6.TabIndex = 97
        Me.ExTableLayoutPanel6.Task = Nothing
        '
        'CheckBoxProcessAsAvailable
        '
        Me.CheckBoxProcessAsAvailable.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxProcessAsAvailable.AutoSize = True
        Me.CheckBoxProcessAsAvailable.Location = New System.Drawing.Point(3, 5)
        Me.CheckBoxProcessAsAvailable.Name = "CheckBoxProcessAsAvailable"
        Me.CheckBoxProcessAsAvailable.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxProcessAsAvailable.Size = New System.Drawing.Size(323, 19)
        Me.CheckBoxProcessAsAvailable.TabIndex = 73
        Me.CheckBoxProcessAsAvailable.Text = "Process files as Available regardless of document Status"
        Me.CheckBoxProcessAsAvailable.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LabelStatusInBaselined)
        Me.Panel1.Controls.Add(Me.LabelStatusAfter)
        Me.Panel1.Controls.Add(Me.LabelStatusInAvailable)
        Me.Panel1.Controls.Add(Me.LabelStatusBefore)
        Me.Panel1.Controls.Add(Me.LabelStatusInInReview)
        Me.Panel1.Controls.Add(Me.LabelStatusOutReleased)
        Me.Panel1.Controls.Add(Me.LabelStatusInInWork)
        Me.Panel1.Controls.Add(Me.LabelStatusOutObsolete)
        Me.Panel1.Controls.Add(Me.LabelStatusInObsolete)
        Me.Panel1.Controls.Add(Me.LabelStatusOutIW)
        Me.Panel1.Controls.Add(Me.LabelStatusInReleased)
        Me.Panel1.Controls.Add(Me.LabelStatusOutInReview)
        Me.Panel1.Controls.Add(Me.GroupBoxStatusInA)
        Me.Panel1.Controls.Add(Me.LabelStatusOutBaselined)
        Me.Panel1.Controls.Add(Me.GroupBoxStatusInB)
        Me.Panel1.Controls.Add(Me.LabelStatusOutAvailable)
        Me.Panel1.Controls.Add(Me.GroupBoxStatusInIR)
        Me.Panel1.Controls.Add(Me.GroupBoxStatusInR)
        Me.Panel1.Controls.Add(Me.GroupBoxStatusInIW)
        Me.Panel1.Controls.Add(Me.GroupBoxStatusInO)
        Me.Panel1.Location = New System.Drawing.Point(3, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(307, 275)
        Me.Panel1.TabIndex = 96
        '
        'LabelStatusInBaselined
        '
        Me.LabelStatusInBaselined.AutoSize = True
        Me.LabelStatusInBaselined.Location = New System.Drawing.Point(25, 95)
        Me.LabelStatusInBaselined.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusInBaselined.Name = "LabelStatusInBaselined"
        Me.LabelStatusInBaselined.Size = New System.Drawing.Size(75, 15)
        Me.LabelStatusInBaselined.TabIndex = 77
        Me.LabelStatusInBaselined.Text = "Baselined (B)"
        '
        'LabelStatusAfter
        '
        Me.LabelStatusAfter.AutoSize = True
        Me.LabelStatusAfter.Location = New System.Drawing.Point(161, 7)
        Me.LabelStatusAfter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusAfter.Name = "LabelStatusAfter"
        Me.LabelStatusAfter.Size = New System.Drawing.Size(84, 15)
        Me.LabelStatusAfter.TabIndex = 95
        Me.LabelStatusAfter.Text = "STATUS AFTER"
        '
        'LabelStatusInAvailable
        '
        Me.LabelStatusInAvailable.AutoSize = True
        Me.LabelStatusInAvailable.Location = New System.Drawing.Point(26, 61)
        Me.LabelStatusInAvailable.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusInAvailable.Name = "LabelStatusInAvailable"
        Me.LabelStatusInAvailable.Size = New System.Drawing.Size(74, 15)
        Me.LabelStatusInAvailable.TabIndex = 76
        Me.LabelStatusInAvailable.Text = "Available (A)"
        '
        'LabelStatusBefore
        '
        Me.LabelStatusBefore.AutoSize = True
        Me.LabelStatusBefore.Location = New System.Drawing.Point(9, 35)
        Me.LabelStatusBefore.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusBefore.Name = "LabelStatusBefore"
        Me.LabelStatusBefore.Size = New System.Drawing.Size(91, 15)
        Me.LabelStatusBefore.TabIndex = 94
        Me.LabelStatusBefore.Text = "STATUS BEFORE"
        '
        'LabelStatusInInReview
        '
        Me.LabelStatusInInReview.AutoSize = True
        Me.LabelStatusInInReview.Location = New System.Drawing.Point(22, 130)
        Me.LabelStatusInInReview.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusInInReview.Name = "LabelStatusInInReview"
        Me.LabelStatusInInReview.Size = New System.Drawing.Size(78, 15)
        Me.LabelStatusInInReview.TabIndex = 78
        Me.LabelStatusInInReview.Text = "In Review (IR)"
        '
        'LabelStatusOutReleased
        '
        Me.LabelStatusOutReleased.AutoSize = True
        Me.LabelStatusOutReleased.Location = New System.Drawing.Point(268, 27)
        Me.LabelStatusOutReleased.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusOutReleased.Name = "LabelStatusOutReleased"
        Me.LabelStatusOutReleased.Size = New System.Drawing.Size(14, 15)
        Me.LabelStatusOutReleased.TabIndex = 93
        Me.LabelStatusOutReleased.Text = "R"
        '
        'LabelStatusInInWork
        '
        Me.LabelStatusInInWork.AutoSize = True
        Me.LabelStatusInInWork.Location = New System.Drawing.Point(24, 165)
        Me.LabelStatusInInWork.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusInInWork.Name = "LabelStatusInInWork"
        Me.LabelStatusInInWork.Size = New System.Drawing.Size(76, 15)
        Me.LabelStatusInInWork.TabIndex = 79
        Me.LabelStatusInInWork.Text = "In Work  (IW)"
        '
        'LabelStatusOutObsolete
        '
        Me.LabelStatusOutObsolete.AutoSize = True
        Me.LabelStatusOutObsolete.Location = New System.Drawing.Point(239, 27)
        Me.LabelStatusOutObsolete.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusOutObsolete.Name = "LabelStatusOutObsolete"
        Me.LabelStatusOutObsolete.Size = New System.Drawing.Size(16, 15)
        Me.LabelStatusOutObsolete.TabIndex = 92
        Me.LabelStatusOutObsolete.Text = "O"
        '
        'LabelStatusInObsolete
        '
        Me.LabelStatusInObsolete.AutoSize = True
        Me.LabelStatusInObsolete.Location = New System.Drawing.Point(26, 199)
        Me.LabelStatusInObsolete.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusInObsolete.Name = "LabelStatusInObsolete"
        Me.LabelStatusInObsolete.Size = New System.Drawing.Size(74, 15)
        Me.LabelStatusInObsolete.TabIndex = 80
        Me.LabelStatusInObsolete.Text = "Obsolete (O)"
        '
        'LabelStatusOutIW
        '
        Me.LabelStatusOutIW.AutoSize = True
        Me.LabelStatusOutIW.Location = New System.Drawing.Point(209, 27)
        Me.LabelStatusOutIW.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusOutIW.Name = "LabelStatusOutIW"
        Me.LabelStatusOutIW.Size = New System.Drawing.Size(21, 15)
        Me.LabelStatusOutIW.TabIndex = 91
        Me.LabelStatusOutIW.Text = "IW"
        '
        'LabelStatusInReleased
        '
        Me.LabelStatusInReleased.AutoSize = True
        Me.LabelStatusInReleased.Location = New System.Drawing.Point(29, 235)
        Me.LabelStatusInReleased.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusInReleased.Name = "LabelStatusInReleased"
        Me.LabelStatusInReleased.Size = New System.Drawing.Size(71, 15)
        Me.LabelStatusInReleased.TabIndex = 81
        Me.LabelStatusInReleased.Text = "Released (R)"
        '
        'LabelStatusOutInReview
        '
        Me.LabelStatusOutInReview.AutoSize = True
        Me.LabelStatusOutInReview.Location = New System.Drawing.Point(178, 27)
        Me.LabelStatusOutInReview.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusOutInReview.Name = "LabelStatusOutInReview"
        Me.LabelStatusOutInReview.Size = New System.Drawing.Size(17, 15)
        Me.LabelStatusOutInReview.TabIndex = 90
        Me.LabelStatusOutInReview.Text = "IR"
        '
        'GroupBoxStatusInA
        '
        Me.GroupBoxStatusInA.Controls.Add(Me.RadioButtonStatusAtoR)
        Me.GroupBoxStatusInA.Controls.Add(Me.RadioButtonStatusAtoO)
        Me.GroupBoxStatusInA.Controls.Add(Me.RadioButtonStatusAtoIW)
        Me.GroupBoxStatusInA.Controls.Add(Me.RadioButtonStatusAtoIR)
        Me.GroupBoxStatusInA.Controls.Add(Me.RadioButtonStatusAtoB)
        Me.GroupBoxStatusInA.Controls.Add(Me.RadioButtonStatusAtoA)
        Me.GroupBoxStatusInA.Location = New System.Drawing.Point(118, 43)
        Me.GroupBoxStatusInA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInA.Name = "GroupBoxStatusInA"
        Me.GroupBoxStatusInA.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInA.Size = New System.Drawing.Size(175, 40)
        Me.GroupBoxStatusInA.TabIndex = 82
        Me.GroupBoxStatusInA.TabStop = False
        '
        'RadioButtonStatusAtoR
        '
        Me.RadioButtonStatusAtoR.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusAtoR.AutoSize = True
        Me.RadioButtonStatusAtoR.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusAtoR.Location = New System.Drawing.Point(146, 15)
        Me.RadioButtonStatusAtoR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusAtoR.Name = "RadioButtonStatusAtoR"
        Me.RadioButtonStatusAtoR.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusAtoR.TabIndex = 5
        Me.RadioButtonStatusAtoR.TabStop = True
        Me.RadioButtonStatusAtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoO
        '
        Me.RadioButtonStatusAtoO.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusAtoO.AutoSize = True
        Me.RadioButtonStatusAtoO.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusAtoO.Location = New System.Drawing.Point(117, 15)
        Me.RadioButtonStatusAtoO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusAtoO.Name = "RadioButtonStatusAtoO"
        Me.RadioButtonStatusAtoO.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusAtoO.TabIndex = 4
        Me.RadioButtonStatusAtoO.TabStop = True
        Me.RadioButtonStatusAtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoIW
        '
        Me.RadioButtonStatusAtoIW.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusAtoIW.AutoSize = True
        Me.RadioButtonStatusAtoIW.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusAtoIW.Location = New System.Drawing.Point(88, 15)
        Me.RadioButtonStatusAtoIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusAtoIW.Name = "RadioButtonStatusAtoIW"
        Me.RadioButtonStatusAtoIW.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusAtoIW.TabIndex = 3
        Me.RadioButtonStatusAtoIW.TabStop = True
        Me.RadioButtonStatusAtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoIR
        '
        Me.RadioButtonStatusAtoIR.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusAtoIR.AutoSize = True
        Me.RadioButtonStatusAtoIR.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusAtoIR.Location = New System.Drawing.Point(58, 15)
        Me.RadioButtonStatusAtoIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusAtoIR.Name = "RadioButtonStatusAtoIR"
        Me.RadioButtonStatusAtoIR.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusAtoIR.TabIndex = 2
        Me.RadioButtonStatusAtoIR.TabStop = True
        Me.RadioButtonStatusAtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoB
        '
        Me.RadioButtonStatusAtoB.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusAtoB.AutoSize = True
        Me.RadioButtonStatusAtoB.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusAtoB.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonStatusAtoB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusAtoB.Name = "RadioButtonStatusAtoB"
        Me.RadioButtonStatusAtoB.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusAtoB.TabIndex = 1
        Me.RadioButtonStatusAtoB.TabStop = True
        Me.RadioButtonStatusAtoB.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoA
        '
        Me.RadioButtonStatusAtoA.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusAtoA.AutoSize = True
        Me.RadioButtonStatusAtoA.Checked = True
        Me.RadioButtonStatusAtoA.Image = Global.Housekeeper.My.Resources.Resources.Checked
        Me.RadioButtonStatusAtoA.Location = New System.Drawing.Point(0, 15)
        Me.RadioButtonStatusAtoA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusAtoA.Name = "RadioButtonStatusAtoA"
        Me.RadioButtonStatusAtoA.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusAtoA.TabIndex = 0
        Me.RadioButtonStatusAtoA.TabStop = True
        Me.RadioButtonStatusAtoA.UseVisualStyleBackColor = True
        '
        'LabelStatusOutBaselined
        '
        Me.LabelStatusOutBaselined.AutoSize = True
        Me.LabelStatusOutBaselined.Location = New System.Drawing.Point(151, 27)
        Me.LabelStatusOutBaselined.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusOutBaselined.Name = "LabelStatusOutBaselined"
        Me.LabelStatusOutBaselined.Size = New System.Drawing.Size(14, 15)
        Me.LabelStatusOutBaselined.TabIndex = 89
        Me.LabelStatusOutBaselined.Text = "B"
        '
        'GroupBoxStatusInB
        '
        Me.GroupBoxStatusInB.Controls.Add(Me.RadioButtonStatusBtoR)
        Me.GroupBoxStatusInB.Controls.Add(Me.RadioButtonStatusBtoO)
        Me.GroupBoxStatusInB.Controls.Add(Me.RadioButtonStatusBtoIW)
        Me.GroupBoxStatusInB.Controls.Add(Me.RadioButtonStatusBtoIR)
        Me.GroupBoxStatusInB.Controls.Add(Me.RadioButtonStatusBtoB)
        Me.GroupBoxStatusInB.Controls.Add(Me.RadioButtonStatusBtoA)
        Me.GroupBoxStatusInB.Location = New System.Drawing.Point(118, 78)
        Me.GroupBoxStatusInB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInB.Name = "GroupBoxStatusInB"
        Me.GroupBoxStatusInB.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInB.Size = New System.Drawing.Size(175, 40)
        Me.GroupBoxStatusInB.TabIndex = 83
        Me.GroupBoxStatusInB.TabStop = False
        '
        'RadioButtonStatusBtoR
        '
        Me.RadioButtonStatusBtoR.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusBtoR.AutoSize = True
        Me.RadioButtonStatusBtoR.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusBtoR.Location = New System.Drawing.Point(146, 15)
        Me.RadioButtonStatusBtoR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusBtoR.Name = "RadioButtonStatusBtoR"
        Me.RadioButtonStatusBtoR.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusBtoR.TabIndex = 5
        Me.RadioButtonStatusBtoR.TabStop = True
        Me.RadioButtonStatusBtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoO
        '
        Me.RadioButtonStatusBtoO.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusBtoO.AutoSize = True
        Me.RadioButtonStatusBtoO.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusBtoO.Location = New System.Drawing.Point(117, 15)
        Me.RadioButtonStatusBtoO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusBtoO.Name = "RadioButtonStatusBtoO"
        Me.RadioButtonStatusBtoO.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusBtoO.TabIndex = 4
        Me.RadioButtonStatusBtoO.TabStop = True
        Me.RadioButtonStatusBtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoIW
        '
        Me.RadioButtonStatusBtoIW.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusBtoIW.AutoSize = True
        Me.RadioButtonStatusBtoIW.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusBtoIW.Location = New System.Drawing.Point(88, 15)
        Me.RadioButtonStatusBtoIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusBtoIW.Name = "RadioButtonStatusBtoIW"
        Me.RadioButtonStatusBtoIW.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusBtoIW.TabIndex = 3
        Me.RadioButtonStatusBtoIW.TabStop = True
        Me.RadioButtonStatusBtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoIR
        '
        Me.RadioButtonStatusBtoIR.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusBtoIR.AutoSize = True
        Me.RadioButtonStatusBtoIR.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusBtoIR.Location = New System.Drawing.Point(58, 15)
        Me.RadioButtonStatusBtoIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusBtoIR.Name = "RadioButtonStatusBtoIR"
        Me.RadioButtonStatusBtoIR.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusBtoIR.TabIndex = 2
        Me.RadioButtonStatusBtoIR.TabStop = True
        Me.RadioButtonStatusBtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoB
        '
        Me.RadioButtonStatusBtoB.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusBtoB.AutoSize = True
        Me.RadioButtonStatusBtoB.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusBtoB.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonStatusBtoB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusBtoB.Name = "RadioButtonStatusBtoB"
        Me.RadioButtonStatusBtoB.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusBtoB.TabIndex = 1
        Me.RadioButtonStatusBtoB.TabStop = True
        Me.RadioButtonStatusBtoB.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoA
        '
        Me.RadioButtonStatusBtoA.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusBtoA.AutoSize = True
        Me.RadioButtonStatusBtoA.Checked = True
        Me.RadioButtonStatusBtoA.Image = Global.Housekeeper.My.Resources.Resources.Checked
        Me.RadioButtonStatusBtoA.Location = New System.Drawing.Point(0, 15)
        Me.RadioButtonStatusBtoA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusBtoA.Name = "RadioButtonStatusBtoA"
        Me.RadioButtonStatusBtoA.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusBtoA.TabIndex = 0
        Me.RadioButtonStatusBtoA.TabStop = True
        Me.RadioButtonStatusBtoA.UseVisualStyleBackColor = True
        '
        'LabelStatusOutAvailable
        '
        Me.LabelStatusOutAvailable.AutoSize = True
        Me.LabelStatusOutAvailable.Location = New System.Drawing.Point(122, 27)
        Me.LabelStatusOutAvailable.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatusOutAvailable.Name = "LabelStatusOutAvailable"
        Me.LabelStatusOutAvailable.Size = New System.Drawing.Size(15, 15)
        Me.LabelStatusOutAvailable.TabIndex = 88
        Me.LabelStatusOutAvailable.Text = "A"
        '
        'GroupBoxStatusInIR
        '
        Me.GroupBoxStatusInIR.Controls.Add(Me.RadioButtonStatusIRtoR)
        Me.GroupBoxStatusInIR.Controls.Add(Me.RadioButtonStatusIRtoO)
        Me.GroupBoxStatusInIR.Controls.Add(Me.RadioButtonStatusIRtoIW)
        Me.GroupBoxStatusInIR.Controls.Add(Me.RadioButtonStatusIRtoIR)
        Me.GroupBoxStatusInIR.Controls.Add(Me.RadioButtonStatusIRtoB)
        Me.GroupBoxStatusInIR.Controls.Add(Me.RadioButtonStatusIRtoA)
        Me.GroupBoxStatusInIR.Location = New System.Drawing.Point(118, 112)
        Me.GroupBoxStatusInIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInIR.Name = "GroupBoxStatusInIR"
        Me.GroupBoxStatusInIR.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInIR.Size = New System.Drawing.Size(175, 40)
        Me.GroupBoxStatusInIR.TabIndex = 84
        Me.GroupBoxStatusInIR.TabStop = False
        '
        'RadioButtonStatusIRtoR
        '
        Me.RadioButtonStatusIRtoR.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusIRtoR.AutoSize = True
        Me.RadioButtonStatusIRtoR.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusIRtoR.Location = New System.Drawing.Point(146, 15)
        Me.RadioButtonStatusIRtoR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIRtoR.Name = "RadioButtonStatusIRtoR"
        Me.RadioButtonStatusIRtoR.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusIRtoR.TabIndex = 5
        Me.RadioButtonStatusIRtoR.TabStop = True
        Me.RadioButtonStatusIRtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoO
        '
        Me.RadioButtonStatusIRtoO.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusIRtoO.AutoSize = True
        Me.RadioButtonStatusIRtoO.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusIRtoO.Location = New System.Drawing.Point(117, 15)
        Me.RadioButtonStatusIRtoO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIRtoO.Name = "RadioButtonStatusIRtoO"
        Me.RadioButtonStatusIRtoO.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusIRtoO.TabIndex = 4
        Me.RadioButtonStatusIRtoO.TabStop = True
        Me.RadioButtonStatusIRtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoIW
        '
        Me.RadioButtonStatusIRtoIW.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusIRtoIW.AutoSize = True
        Me.RadioButtonStatusIRtoIW.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusIRtoIW.Location = New System.Drawing.Point(88, 15)
        Me.RadioButtonStatusIRtoIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIRtoIW.Name = "RadioButtonStatusIRtoIW"
        Me.RadioButtonStatusIRtoIW.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusIRtoIW.TabIndex = 3
        Me.RadioButtonStatusIRtoIW.TabStop = True
        Me.RadioButtonStatusIRtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoIR
        '
        Me.RadioButtonStatusIRtoIR.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusIRtoIR.AutoSize = True
        Me.RadioButtonStatusIRtoIR.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusIRtoIR.Location = New System.Drawing.Point(58, 15)
        Me.RadioButtonStatusIRtoIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIRtoIR.Name = "RadioButtonStatusIRtoIR"
        Me.RadioButtonStatusIRtoIR.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusIRtoIR.TabIndex = 2
        Me.RadioButtonStatusIRtoIR.TabStop = True
        Me.RadioButtonStatusIRtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoB
        '
        Me.RadioButtonStatusIRtoB.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusIRtoB.AutoSize = True
        Me.RadioButtonStatusIRtoB.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusIRtoB.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonStatusIRtoB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIRtoB.Name = "RadioButtonStatusIRtoB"
        Me.RadioButtonStatusIRtoB.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusIRtoB.TabIndex = 1
        Me.RadioButtonStatusIRtoB.TabStop = True
        Me.RadioButtonStatusIRtoB.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoA
        '
        Me.RadioButtonStatusIRtoA.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusIRtoA.AutoSize = True
        Me.RadioButtonStatusIRtoA.Checked = True
        Me.RadioButtonStatusIRtoA.Image = Global.Housekeeper.My.Resources.Resources.Checked
        Me.RadioButtonStatusIRtoA.Location = New System.Drawing.Point(0, 15)
        Me.RadioButtonStatusIRtoA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIRtoA.Name = "RadioButtonStatusIRtoA"
        Me.RadioButtonStatusIRtoA.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusIRtoA.TabIndex = 0
        Me.RadioButtonStatusIRtoA.TabStop = True
        Me.RadioButtonStatusIRtoA.UseVisualStyleBackColor = True
        '
        'GroupBoxStatusInR
        '
        Me.GroupBoxStatusInR.Controls.Add(Me.RadioButtonStatusRtoR)
        Me.GroupBoxStatusInR.Controls.Add(Me.RadioButtonStatusRtoO)
        Me.GroupBoxStatusInR.Controls.Add(Me.RadioButtonStatusRtoIW)
        Me.GroupBoxStatusInR.Controls.Add(Me.RadioButtonStatusRtoIR)
        Me.GroupBoxStatusInR.Controls.Add(Me.RadioButtonStatusRtoB)
        Me.GroupBoxStatusInR.Controls.Add(Me.RadioButtonStatusRtoA)
        Me.GroupBoxStatusInR.Location = New System.Drawing.Point(118, 219)
        Me.GroupBoxStatusInR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInR.Name = "GroupBoxStatusInR"
        Me.GroupBoxStatusInR.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInR.Size = New System.Drawing.Size(175, 40)
        Me.GroupBoxStatusInR.TabIndex = 87
        Me.GroupBoxStatusInR.TabStop = False
        '
        'RadioButtonStatusRtoR
        '
        Me.RadioButtonStatusRtoR.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusRtoR.AutoSize = True
        Me.RadioButtonStatusRtoR.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusRtoR.Location = New System.Drawing.Point(146, 15)
        Me.RadioButtonStatusRtoR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusRtoR.Name = "RadioButtonStatusRtoR"
        Me.RadioButtonStatusRtoR.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusRtoR.TabIndex = 5
        Me.RadioButtonStatusRtoR.TabStop = True
        Me.RadioButtonStatusRtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoO
        '
        Me.RadioButtonStatusRtoO.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusRtoO.AutoSize = True
        Me.RadioButtonStatusRtoO.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusRtoO.Location = New System.Drawing.Point(117, 15)
        Me.RadioButtonStatusRtoO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusRtoO.Name = "RadioButtonStatusRtoO"
        Me.RadioButtonStatusRtoO.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusRtoO.TabIndex = 4
        Me.RadioButtonStatusRtoO.TabStop = True
        Me.RadioButtonStatusRtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoIW
        '
        Me.RadioButtonStatusRtoIW.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusRtoIW.AutoSize = True
        Me.RadioButtonStatusRtoIW.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusRtoIW.Location = New System.Drawing.Point(88, 15)
        Me.RadioButtonStatusRtoIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusRtoIW.Name = "RadioButtonStatusRtoIW"
        Me.RadioButtonStatusRtoIW.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusRtoIW.TabIndex = 3
        Me.RadioButtonStatusRtoIW.TabStop = True
        Me.RadioButtonStatusRtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoIR
        '
        Me.RadioButtonStatusRtoIR.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusRtoIR.AutoSize = True
        Me.RadioButtonStatusRtoIR.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusRtoIR.Location = New System.Drawing.Point(58, 15)
        Me.RadioButtonStatusRtoIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusRtoIR.Name = "RadioButtonStatusRtoIR"
        Me.RadioButtonStatusRtoIR.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusRtoIR.TabIndex = 2
        Me.RadioButtonStatusRtoIR.TabStop = True
        Me.RadioButtonStatusRtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoB
        '
        Me.RadioButtonStatusRtoB.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusRtoB.AutoSize = True
        Me.RadioButtonStatusRtoB.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusRtoB.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonStatusRtoB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusRtoB.Name = "RadioButtonStatusRtoB"
        Me.RadioButtonStatusRtoB.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusRtoB.TabIndex = 1
        Me.RadioButtonStatusRtoB.TabStop = True
        Me.RadioButtonStatusRtoB.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoA
        '
        Me.RadioButtonStatusRtoA.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusRtoA.AutoSize = True
        Me.RadioButtonStatusRtoA.Checked = True
        Me.RadioButtonStatusRtoA.Image = Global.Housekeeper.My.Resources.Resources.Checked
        Me.RadioButtonStatusRtoA.Location = New System.Drawing.Point(0, 15)
        Me.RadioButtonStatusRtoA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusRtoA.Name = "RadioButtonStatusRtoA"
        Me.RadioButtonStatusRtoA.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusRtoA.TabIndex = 0
        Me.RadioButtonStatusRtoA.TabStop = True
        Me.RadioButtonStatusRtoA.UseVisualStyleBackColor = True
        '
        'GroupBoxStatusInIW
        '
        Me.GroupBoxStatusInIW.Controls.Add(Me.RadioButtonStatusIWtoR)
        Me.GroupBoxStatusInIW.Controls.Add(Me.RadioButtonStatusIWtoO)
        Me.GroupBoxStatusInIW.Controls.Add(Me.RadioButtonStatusIWtoIW)
        Me.GroupBoxStatusInIW.Controls.Add(Me.RadioButtonStatusIWtoIR)
        Me.GroupBoxStatusInIW.Controls.Add(Me.RadioButtonStatusIWtoB)
        Me.GroupBoxStatusInIW.Controls.Add(Me.RadioButtonStatusIWtoA)
        Me.GroupBoxStatusInIW.Location = New System.Drawing.Point(118, 147)
        Me.GroupBoxStatusInIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInIW.Name = "GroupBoxStatusInIW"
        Me.GroupBoxStatusInIW.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInIW.Size = New System.Drawing.Size(175, 40)
        Me.GroupBoxStatusInIW.TabIndex = 85
        Me.GroupBoxStatusInIW.TabStop = False
        '
        'RadioButtonStatusIWtoR
        '
        Me.RadioButtonStatusIWtoR.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusIWtoR.AutoSize = True
        Me.RadioButtonStatusIWtoR.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusIWtoR.Location = New System.Drawing.Point(146, 15)
        Me.RadioButtonStatusIWtoR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIWtoR.Name = "RadioButtonStatusIWtoR"
        Me.RadioButtonStatusIWtoR.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusIWtoR.TabIndex = 5
        Me.RadioButtonStatusIWtoR.TabStop = True
        Me.RadioButtonStatusIWtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoO
        '
        Me.RadioButtonStatusIWtoO.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusIWtoO.AutoSize = True
        Me.RadioButtonStatusIWtoO.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusIWtoO.Location = New System.Drawing.Point(117, 15)
        Me.RadioButtonStatusIWtoO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIWtoO.Name = "RadioButtonStatusIWtoO"
        Me.RadioButtonStatusIWtoO.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusIWtoO.TabIndex = 4
        Me.RadioButtonStatusIWtoO.TabStop = True
        Me.RadioButtonStatusIWtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoIW
        '
        Me.RadioButtonStatusIWtoIW.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusIWtoIW.AutoSize = True
        Me.RadioButtonStatusIWtoIW.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusIWtoIW.Location = New System.Drawing.Point(88, 15)
        Me.RadioButtonStatusIWtoIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIWtoIW.Name = "RadioButtonStatusIWtoIW"
        Me.RadioButtonStatusIWtoIW.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusIWtoIW.TabIndex = 3
        Me.RadioButtonStatusIWtoIW.TabStop = True
        Me.RadioButtonStatusIWtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoIR
        '
        Me.RadioButtonStatusIWtoIR.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusIWtoIR.AutoSize = True
        Me.RadioButtonStatusIWtoIR.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusIWtoIR.Location = New System.Drawing.Point(58, 15)
        Me.RadioButtonStatusIWtoIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIWtoIR.Name = "RadioButtonStatusIWtoIR"
        Me.RadioButtonStatusIWtoIR.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusIWtoIR.TabIndex = 2
        Me.RadioButtonStatusIWtoIR.TabStop = True
        Me.RadioButtonStatusIWtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoB
        '
        Me.RadioButtonStatusIWtoB.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusIWtoB.AutoSize = True
        Me.RadioButtonStatusIWtoB.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusIWtoB.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonStatusIWtoB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIWtoB.Name = "RadioButtonStatusIWtoB"
        Me.RadioButtonStatusIWtoB.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusIWtoB.TabIndex = 1
        Me.RadioButtonStatusIWtoB.TabStop = True
        Me.RadioButtonStatusIWtoB.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoA
        '
        Me.RadioButtonStatusIWtoA.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusIWtoA.AutoSize = True
        Me.RadioButtonStatusIWtoA.Checked = True
        Me.RadioButtonStatusIWtoA.Image = Global.Housekeeper.My.Resources.Resources.Checked
        Me.RadioButtonStatusIWtoA.Location = New System.Drawing.Point(0, 15)
        Me.RadioButtonStatusIWtoA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusIWtoA.Name = "RadioButtonStatusIWtoA"
        Me.RadioButtonStatusIWtoA.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusIWtoA.TabIndex = 0
        Me.RadioButtonStatusIWtoA.TabStop = True
        Me.RadioButtonStatusIWtoA.UseVisualStyleBackColor = True
        '
        'GroupBoxStatusInO
        '
        Me.GroupBoxStatusInO.Controls.Add(Me.RadioButtonStatusOtoR)
        Me.GroupBoxStatusInO.Controls.Add(Me.RadioButtonStatusOtoO)
        Me.GroupBoxStatusInO.Controls.Add(Me.RadioButtonStatusOtoIW)
        Me.GroupBoxStatusInO.Controls.Add(Me.RadioButtonStatusOtoIR)
        Me.GroupBoxStatusInO.Controls.Add(Me.RadioButtonStatusOtoB)
        Me.GroupBoxStatusInO.Controls.Add(Me.RadioButtonStatusOtoA)
        Me.GroupBoxStatusInO.Location = New System.Drawing.Point(118, 183)
        Me.GroupBoxStatusInO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInO.Name = "GroupBoxStatusInO"
        Me.GroupBoxStatusInO.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBoxStatusInO.Size = New System.Drawing.Size(175, 40)
        Me.GroupBoxStatusInO.TabIndex = 86
        Me.GroupBoxStatusInO.TabStop = False
        '
        'RadioButtonStatusOtoR
        '
        Me.RadioButtonStatusOtoR.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusOtoR.AutoSize = True
        Me.RadioButtonStatusOtoR.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusOtoR.Location = New System.Drawing.Point(146, 15)
        Me.RadioButtonStatusOtoR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusOtoR.Name = "RadioButtonStatusOtoR"
        Me.RadioButtonStatusOtoR.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusOtoR.TabIndex = 5
        Me.RadioButtonStatusOtoR.TabStop = True
        Me.RadioButtonStatusOtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoO
        '
        Me.RadioButtonStatusOtoO.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusOtoO.AutoSize = True
        Me.RadioButtonStatusOtoO.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusOtoO.Location = New System.Drawing.Point(117, 15)
        Me.RadioButtonStatusOtoO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusOtoO.Name = "RadioButtonStatusOtoO"
        Me.RadioButtonStatusOtoO.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusOtoO.TabIndex = 4
        Me.RadioButtonStatusOtoO.TabStop = True
        Me.RadioButtonStatusOtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoIW
        '
        Me.RadioButtonStatusOtoIW.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusOtoIW.AutoSize = True
        Me.RadioButtonStatusOtoIW.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusOtoIW.Location = New System.Drawing.Point(88, 15)
        Me.RadioButtonStatusOtoIW.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusOtoIW.Name = "RadioButtonStatusOtoIW"
        Me.RadioButtonStatusOtoIW.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusOtoIW.TabIndex = 3
        Me.RadioButtonStatusOtoIW.TabStop = True
        Me.RadioButtonStatusOtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoIR
        '
        Me.RadioButtonStatusOtoIR.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusOtoIR.AutoSize = True
        Me.RadioButtonStatusOtoIR.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusOtoIR.Location = New System.Drawing.Point(58, 15)
        Me.RadioButtonStatusOtoIR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusOtoIR.Name = "RadioButtonStatusOtoIR"
        Me.RadioButtonStatusOtoIR.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusOtoIR.TabIndex = 2
        Me.RadioButtonStatusOtoIR.TabStop = True
        Me.RadioButtonStatusOtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoB
        '
        Me.RadioButtonStatusOtoB.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusOtoB.AutoSize = True
        Me.RadioButtonStatusOtoB.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.RadioButtonStatusOtoB.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonStatusOtoB.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusOtoB.Name = "RadioButtonStatusOtoB"
        Me.RadioButtonStatusOtoB.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusOtoB.TabIndex = 1
        Me.RadioButtonStatusOtoB.TabStop = True
        Me.RadioButtonStatusOtoB.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoA
        '
        Me.RadioButtonStatusOtoA.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonStatusOtoA.AutoSize = True
        Me.RadioButtonStatusOtoA.Checked = True
        Me.RadioButtonStatusOtoA.Image = Global.Housekeeper.My.Resources.Resources.Checked
        Me.RadioButtonStatusOtoA.Location = New System.Drawing.Point(0, 15)
        Me.RadioButtonStatusOtoA.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonStatusOtoA.Name = "RadioButtonStatusOtoA"
        Me.RadioButtonStatusOtoA.Size = New System.Drawing.Size(22, 22)
        Me.RadioButtonStatusOtoA.TabIndex = 0
        Me.RadioButtonStatusOtoA.TabStop = True
        Me.RadioButtonStatusOtoA.UseVisualStyleBackColor = True
        '
        'RadioButtonProcessAsAvailableRevert
        '
        Me.RadioButtonProcessAsAvailableRevert.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonProcessAsAvailableRevert.AutoSize = True
        Me.RadioButtonProcessAsAvailableRevert.Checked = True
        Me.RadioButtonProcessAsAvailableRevert.Location = New System.Drawing.Point(3, 33)
        Me.RadioButtonProcessAsAvailableRevert.Name = "RadioButtonProcessAsAvailableRevert"
        Me.RadioButtonProcessAsAvailableRevert.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.RadioButtonProcessAsAvailableRevert.Size = New System.Drawing.Size(246, 19)
        Me.RadioButtonProcessAsAvailableRevert.TabIndex = 74
        Me.RadioButtonProcessAsAvailableRevert.TabStop = True
        Me.RadioButtonProcessAsAvailableRevert.Text = "Revert to previous status after processing"
        Me.RadioButtonProcessAsAvailableRevert.UseVisualStyleBackColor = True
        '
        'RadioButtonProcessAsAvailableChange
        '
        Me.RadioButtonProcessAsAvailableChange.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonProcessAsAvailableChange.AutoSize = True
        Me.RadioButtonProcessAsAvailableChange.Location = New System.Drawing.Point(3, 58)
        Me.RadioButtonProcessAsAvailableChange.Name = "RadioButtonProcessAsAvailableChange"
        Me.RadioButtonProcessAsAvailableChange.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.RadioButtonProcessAsAvailableChange.Size = New System.Drawing.Size(192, 19)
        Me.RadioButtonProcessAsAvailableChange.TabIndex = 75
        Me.RadioButtonProcessAsAvailableChange.TabStop = True
        Me.RadioButtonProcessAsAvailableChange.Text = "Change status after processing"
        Me.RadioButtonProcessAsAvailableChange.UseVisualStyleBackColor = True
        '
        'TabPageSorting
        '
        Me.TabPageSorting.Controls.Add(Me.ExTableLayoutPanel7)
        Me.TabPageSorting.ImageKey = "list"
        Me.TabPageSorting.Location = New System.Drawing.Point(4, 24)
        Me.TabPageSorting.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageSorting.Name = "TabPageSorting"
        Me.TabPageSorting.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageSorting.Size = New System.Drawing.Size(535, 585)
        Me.TabPageSorting.TabIndex = 4
        Me.TabPageSorting.Text = "Sorting"
        Me.TabPageSorting.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel7
        '
        Me.ExTableLayoutPanel7.BackColor = System.Drawing.Color.White
        Me.ExTableLayoutPanel7.ColumnCount = 1
        Me.ExTableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel7.Controls.Add(Me.RadioButtonSortNone, 0, 0)
        Me.ExTableLayoutPanel7.Controls.Add(Me.ExTableLayoutPanel8, 0, 6)
        Me.ExTableLayoutPanel7.Controls.Add(Me.RadioButtonSortRandomSample, 0, 5)
        Me.ExTableLayoutPanel7.Controls.Add(Me.CheckBoxSortIncludeNoDependencies, 0, 4)
        Me.ExTableLayoutPanel7.Controls.Add(Me.RadioButtonSortDependency, 0, 3)
        Me.ExTableLayoutPanel7.Controls.Add(Me.RadioButtonSortAlphabetical, 0, 2)
        Me.ExTableLayoutPanel7.Controls.Add(Me.CheckBoxKeepUnsortedDuplicates, 0, 1)
        Me.ExTableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel7.Location = New System.Drawing.Point(4, 3)
        Me.ExTableLayoutPanel7.Name = "ExTableLayoutPanel7"
        Me.ExTableLayoutPanel7.RowCount = 8
        Me.ExTableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel7.Size = New System.Drawing.Size(527, 579)
        Me.ExTableLayoutPanel7.TabIndex = 56
        Me.ExTableLayoutPanel7.Task = Nothing
        '
        'RadioButtonSortNone
        '
        Me.RadioButtonSortNone.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonSortNone.AutoSize = True
        Me.RadioButtonSortNone.Location = New System.Drawing.Point(3, 5)
        Me.RadioButtonSortNone.Name = "RadioButtonSortNone"
        Me.RadioButtonSortNone.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.RadioButtonSortNone.Size = New System.Drawing.Size(78, 19)
        Me.RadioButtonSortNone.TabIndex = 49
        Me.RadioButtonSortNone.Text = "Unsorted"
        Me.RadioButtonSortNone.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel8
        '
        Me.ExTableLayoutPanel8.ColumnCount = 2
        Me.ExTableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.ExTableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel8.Controls.Add(Me.TextBoxSortRandomSampleFraction, 0, 0)
        Me.ExTableLayoutPanel8.Controls.Add(Me.LabelSortRandomSampleFraction, 1, 0)
        Me.ExTableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel8.Location = New System.Drawing.Point(3, 173)
        Me.ExTableLayoutPanel8.Name = "ExTableLayoutPanel8"
        Me.ExTableLayoutPanel8.RowCount = 1
        Me.ExTableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel8.Size = New System.Drawing.Size(521, 34)
        Me.ExTableLayoutPanel8.TabIndex = 54
        Me.ExTableLayoutPanel8.Task = Nothing
        '
        'TextBoxSortRandomSampleFraction
        '
        Me.TextBoxSortRandomSampleFraction.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TextBoxSortRandomSampleFraction.Location = New System.Drawing.Point(16, 5)
        Me.TextBoxSortRandomSampleFraction.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextBoxSortRandomSampleFraction.Name = "TextBoxSortRandomSampleFraction"
        Me.TextBoxSortRandomSampleFraction.Size = New System.Drawing.Size(35, 23)
        Me.TextBoxSortRandomSampleFraction.TabIndex = 55
        Me.TextBoxSortRandomSampleFraction.Text = "0.1"
        Me.TextBoxSortRandomSampleFraction.Visible = False
        '
        'LabelSortRandomSampleFraction
        '
        Me.LabelSortRandomSampleFraction.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelSortRandomSampleFraction.AutoSize = True
        Me.LabelSortRandomSampleFraction.Location = New System.Drawing.Point(59, 9)
        Me.LabelSortRandomSampleFraction.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSortRandomSampleFraction.Name = "LabelSortRandomSampleFraction"
        Me.LabelSortRandomSampleFraction.Size = New System.Drawing.Size(90, 15)
        Me.LabelSortRandomSampleFraction.TabIndex = 54
        Me.LabelSortRandomSampleFraction.Text = "Sample fraction"
        Me.LabelSortRandomSampleFraction.Visible = False
        '
        'RadioButtonSortRandomSample
        '
        Me.RadioButtonSortRandomSample.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonSortRandomSample.AutoSize = True
        Me.RadioButtonSortRandomSample.Location = New System.Drawing.Point(3, 145)
        Me.RadioButtonSortRandomSample.Name = "RadioButtonSortRandomSample"
        Me.RadioButtonSortRandomSample.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.RadioButtonSortRandomSample.Size = New System.Drawing.Size(116, 19)
        Me.RadioButtonSortRandomSample.TabIndex = 53
        Me.RadioButtonSortRandomSample.TabStop = True
        Me.RadioButtonSortRandomSample.Text = "Random sample"
        Me.RadioButtonSortRandomSample.UseVisualStyleBackColor = True
        '
        'CheckBoxSortIncludeNoDependencies
        '
        Me.CheckBoxSortIncludeNoDependencies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxSortIncludeNoDependencies.AutoSize = True
        Me.CheckBoxSortIncludeNoDependencies.Location = New System.Drawing.Point(3, 118)
        Me.CheckBoxSortIncludeNoDependencies.Name = "CheckBoxSortIncludeNoDependencies"
        Me.CheckBoxSortIncludeNoDependencies.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.CheckBoxSortIncludeNoDependencies.Size = New System.Drawing.Size(365, 19)
        Me.CheckBoxSortIncludeNoDependencies.TabIndex = 52
        Me.CheckBoxSortIncludeNoDependencies.Text = "Include files with no Part Copy dependencies in search results"
        Me.CheckBoxSortIncludeNoDependencies.UseVisualStyleBackColor = True
        Me.CheckBoxSortIncludeNoDependencies.Visible = False
        '
        'RadioButtonSortDependency
        '
        Me.RadioButtonSortDependency.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonSortDependency.AutoSize = True
        Me.RadioButtonSortDependency.Location = New System.Drawing.Point(3, 90)
        Me.RadioButtonSortDependency.Name = "RadioButtonSortDependency"
        Me.RadioButtonSortDependency.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.RadioButtonSortDependency.Size = New System.Drawing.Size(176, 19)
        Me.RadioButtonSortDependency.TabIndex = 51
        Me.RadioButtonSortDependency.Text = "Sorted in dependency order"
        Me.RadioButtonSortDependency.UseVisualStyleBackColor = True
        '
        'RadioButtonSortAlphabetical
        '
        Me.RadioButtonSortAlphabetical.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonSortAlphabetical.AutoSize = True
        Me.RadioButtonSortAlphabetical.Checked = True
        Me.RadioButtonSortAlphabetical.Location = New System.Drawing.Point(3, 60)
        Me.RadioButtonSortAlphabetical.Name = "RadioButtonSortAlphabetical"
        Me.RadioButtonSortAlphabetical.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.RadioButtonSortAlphabetical.Size = New System.Drawing.Size(175, 19)
        Me.RadioButtonSortAlphabetical.TabIndex = 50
        Me.RadioButtonSortAlphabetical.TabStop = True
        Me.RadioButtonSortAlphabetical.Text = "Sorted in alphabetical order"
        Me.RadioButtonSortAlphabetical.UseVisualStyleBackColor = True
        '
        'CheckBoxKeepUnsortedDuplicates
        '
        Me.CheckBoxKeepUnsortedDuplicates.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxKeepUnsortedDuplicates.AutoSize = True
        Me.CheckBoxKeepUnsortedDuplicates.Location = New System.Drawing.Point(3, 33)
        Me.CheckBoxKeepUnsortedDuplicates.Name = "CheckBoxKeepUnsortedDuplicates"
        Me.CheckBoxKeepUnsortedDuplicates.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.CheckBoxKeepUnsortedDuplicates.Size = New System.Drawing.Size(124, 19)
        Me.CheckBoxKeepUnsortedDuplicates.TabIndex = 55
        Me.CheckBoxKeepUnsortedDuplicates.Text = "Keep duplicates"
        Me.CheckBoxKeepUnsortedDuplicates.UseVisualStyleBackColor = True
        Me.CheckBoxKeepUnsortedDuplicates.Visible = False
        '
        'TabPageTemplates
        '
        Me.TabPageTemplates.BackColor = System.Drawing.Color.White
        Me.TabPageTemplates.Controls.Add(Me.ExTableLayoutPanel1)
        Me.TabPageTemplates.ImageKey = "se"
        Me.TabPageTemplates.Location = New System.Drawing.Point(4, 24)
        Me.TabPageTemplates.Name = "TabPageTemplates"
        Me.TabPageTemplates.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTemplates.Size = New System.Drawing.Size(535, 585)
        Me.TabPageTemplates.TabIndex = 7
        Me.TabPageTemplates.Text = "Templates"
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExTableLayoutPanel1.ColumnCount = 2
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonAssemblyTemplate, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.TextBoxAssemblyTemplate, 1, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonPartTemplate, 0, 1)
        Me.ExTableLayoutPanel1.Controls.Add(Me.TextBoxPartTemplate, 1, 1)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonSheetmetalTemplate, 0, 2)
        Me.ExTableLayoutPanel1.Controls.Add(Me.TextBoxSheetmetalTemplate, 1, 2)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonDraftTemplate, 0, 3)
        Me.ExTableLayoutPanel1.Controls.Add(Me.TextBoxDraftTemplate, 1, 3)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonMaterialTable, 0, 4)
        Me.ExTableLayoutPanel1.Controls.Add(Me.TextBoxMaterialTable, 1, 4)
        Me.ExTableLayoutPanel1.Controls.Add(Me.LabelCustomizeTemplatePropertyDict, 1, 6)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonCustomizePropertiesData, 0, 6)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonUpdatePropertiesData, 0, 5)
        Me.ExTableLayoutPanel1.Controls.Add(Me.LabelUpdateProperties, 1, 5)
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(6, 6)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 8
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(522, 256)
        Me.ExTableLayoutPanel1.TabIndex = 0
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'ButtonAssemblyTemplate
        '
        Me.ButtonAssemblyTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonAssemblyTemplate.Location = New System.Drawing.Point(3, 3)
        Me.ButtonAssemblyTemplate.Name = "ButtonAssemblyTemplate"
        Me.ButtonAssemblyTemplate.Size = New System.Drawing.Size(94, 24)
        Me.ButtonAssemblyTemplate.TabIndex = 0
        Me.ButtonAssemblyTemplate.Text = "Assembly"
        Me.ButtonAssemblyTemplate.UseVisualStyleBackColor = True
        '
        'TextBoxAssemblyTemplate
        '
        Me.TextBoxAssemblyTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxAssemblyTemplate.Location = New System.Drawing.Point(103, 3)
        Me.TextBoxAssemblyTemplate.Name = "TextBoxAssemblyTemplate"
        Me.TextBoxAssemblyTemplate.Size = New System.Drawing.Size(416, 23)
        Me.TextBoxAssemblyTemplate.TabIndex = 1
        '
        'ButtonPartTemplate
        '
        Me.ButtonPartTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonPartTemplate.Location = New System.Drawing.Point(3, 33)
        Me.ButtonPartTemplate.Name = "ButtonPartTemplate"
        Me.ButtonPartTemplate.Size = New System.Drawing.Size(94, 24)
        Me.ButtonPartTemplate.TabIndex = 2
        Me.ButtonPartTemplate.Text = "Part"
        Me.ButtonPartTemplate.UseVisualStyleBackColor = True
        '
        'TextBoxPartTemplate
        '
        Me.TextBoxPartTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxPartTemplate.Location = New System.Drawing.Point(103, 33)
        Me.TextBoxPartTemplate.Name = "TextBoxPartTemplate"
        Me.TextBoxPartTemplate.Size = New System.Drawing.Size(416, 23)
        Me.TextBoxPartTemplate.TabIndex = 3
        '
        'ButtonSheetmetalTemplate
        '
        Me.ButtonSheetmetalTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonSheetmetalTemplate.Location = New System.Drawing.Point(3, 63)
        Me.ButtonSheetmetalTemplate.Name = "ButtonSheetmetalTemplate"
        Me.ButtonSheetmetalTemplate.Size = New System.Drawing.Size(94, 24)
        Me.ButtonSheetmetalTemplate.TabIndex = 4
        Me.ButtonSheetmetalTemplate.Text = "Sheetmetal"
        Me.ButtonSheetmetalTemplate.UseVisualStyleBackColor = True
        '
        'TextBoxSheetmetalTemplate
        '
        Me.TextBoxSheetmetalTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxSheetmetalTemplate.Location = New System.Drawing.Point(103, 63)
        Me.TextBoxSheetmetalTemplate.Name = "TextBoxSheetmetalTemplate"
        Me.TextBoxSheetmetalTemplate.Size = New System.Drawing.Size(416, 23)
        Me.TextBoxSheetmetalTemplate.TabIndex = 5
        '
        'ButtonDraftTemplate
        '
        Me.ButtonDraftTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonDraftTemplate.Location = New System.Drawing.Point(3, 93)
        Me.ButtonDraftTemplate.Name = "ButtonDraftTemplate"
        Me.ButtonDraftTemplate.Size = New System.Drawing.Size(94, 24)
        Me.ButtonDraftTemplate.TabIndex = 6
        Me.ButtonDraftTemplate.Text = "Draft"
        Me.ButtonDraftTemplate.UseVisualStyleBackColor = True
        '
        'TextBoxDraftTemplate
        '
        Me.TextBoxDraftTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxDraftTemplate.Location = New System.Drawing.Point(103, 93)
        Me.TextBoxDraftTemplate.Name = "TextBoxDraftTemplate"
        Me.TextBoxDraftTemplate.Size = New System.Drawing.Size(416, 23)
        Me.TextBoxDraftTemplate.TabIndex = 7
        '
        'ButtonMaterialTable
        '
        Me.ButtonMaterialTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonMaterialTable.Location = New System.Drawing.Point(3, 123)
        Me.ButtonMaterialTable.Name = "ButtonMaterialTable"
        Me.ButtonMaterialTable.Size = New System.Drawing.Size(94, 24)
        Me.ButtonMaterialTable.TabIndex = 9
        Me.ButtonMaterialTable.Text = "Material Table"
        Me.ButtonMaterialTable.UseVisualStyleBackColor = True
        '
        'TextBoxMaterialTable
        '
        Me.TextBoxMaterialTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxMaterialTable.Location = New System.Drawing.Point(103, 123)
        Me.TextBoxMaterialTable.Name = "TextBoxMaterialTable"
        Me.TextBoxMaterialTable.Size = New System.Drawing.Size(416, 23)
        Me.TextBoxMaterialTable.TabIndex = 10
        '
        'LabelCustomizeTemplatePropertyDict
        '
        Me.LabelCustomizeTemplatePropertyDict.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelCustomizeTemplatePropertyDict.AutoSize = True
        Me.LabelCustomizeTemplatePropertyDict.Location = New System.Drawing.Point(103, 187)
        Me.LabelCustomizeTemplatePropertyDict.Name = "LabelCustomizeTemplatePropertyDict"
        Me.LabelCustomizeTemplatePropertyDict.Size = New System.Drawing.Size(287, 15)
        Me.LabelCustomizeTemplatePropertyDict.TabIndex = 12
        Me.LabelCustomizeTemplatePropertyDict.Text = "Customize selection and order of template properties"
        '
        'ButtonCustomizePropertiesData
        '
        Me.ButtonCustomizePropertiesData.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonCustomizePropertiesData.Location = New System.Drawing.Point(3, 183)
        Me.ButtonCustomizePropertiesData.Name = "ButtonCustomizePropertiesData"
        Me.ButtonCustomizePropertiesData.Size = New System.Drawing.Size(94, 24)
        Me.ButtonCustomizePropertiesData.TabIndex = 11
        Me.ButtonCustomizePropertiesData.Text = "Customize"
        Me.ButtonCustomizePropertiesData.UseVisualStyleBackColor = True
        '
        'ButtonUpdatePropertiesData
        '
        Me.ButtonUpdatePropertiesData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonUpdatePropertiesData.Location = New System.Drawing.Point(3, 153)
        Me.ButtonUpdatePropertiesData.Name = "ButtonUpdatePropertiesData"
        Me.ButtonUpdatePropertiesData.Size = New System.Drawing.Size(94, 24)
        Me.ButtonUpdatePropertiesData.TabIndex = 15
        Me.ButtonUpdatePropertiesData.Text = "Update"
        Me.ButtonUpdatePropertiesData.UseVisualStyleBackColor = True
        '
        'LabelUpdateProperties
        '
        Me.LabelUpdateProperties.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelUpdateProperties.AutoSize = True
        Me.LabelUpdateProperties.Location = New System.Drawing.Point(103, 157)
        Me.LabelUpdateProperties.Name = "LabelUpdateProperties"
        Me.LabelUpdateProperties.Size = New System.Drawing.Size(292, 15)
        Me.LabelUpdateProperties.TabIndex = 16
        Me.LabelUpdateProperties.Text = "Update template properties for use in property dialogs"
        '
        'TabPageServerQuery
        '
        Me.TabPageServerQuery.Controls.Add(Me.ExTableLayoutPanel9)
        Me.TabPageServerQuery.ImageKey = "Query"
        Me.TabPageServerQuery.Location = New System.Drawing.Point(4, 24)
        Me.TabPageServerQuery.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPageServerQuery.Name = "TabPageServerQuery"
        Me.TabPageServerQuery.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageServerQuery.Size = New System.Drawing.Size(535, 585)
        Me.TabPageServerQuery.TabIndex = 8
        Me.TabPageServerQuery.Text = "Server Query"
        Me.TabPageServerQuery.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel9
        '
        Me.ExTableLayoutPanel9.BackColor = System.Drawing.Color.White
        Me.ExTableLayoutPanel9.ColumnCount = 1
        Me.ExTableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel9.Controls.Add(Me.TextBoxServerConnectionString, 0, 1)
        Me.ExTableLayoutPanel9.Controls.Add(Me.LabelServerConnectionString, 0, 0)
        Me.ExTableLayoutPanel9.Controls.Add(Me.LabelServerQuery, 0, 2)
        Me.ExTableLayoutPanel9.Controls.Add(Me.FastColoredServerQuery, 0, 3)
        Me.ExTableLayoutPanel9.Controls.Add(Me.Label1, 0, 4)
        Me.ExTableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel9.Location = New System.Drawing.Point(3, 3)
        Me.ExTableLayoutPanel9.Margin = New System.Windows.Forms.Padding(0)
        Me.ExTableLayoutPanel9.Name = "ExTableLayoutPanel9"
        Me.ExTableLayoutPanel9.RowCount = 5
        Me.ExTableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ExTableLayoutPanel9.Size = New System.Drawing.Size(529, 579)
        Me.ExTableLayoutPanel9.TabIndex = 0
        Me.ExTableLayoutPanel9.Task = Nothing
        '
        'TextBoxServerConnectionString
        '
        Me.TextBoxServerConnectionString.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxServerConnectionString.Location = New System.Drawing.Point(3, 33)
        Me.TextBoxServerConnectionString.Name = "TextBoxServerConnectionString"
        Me.TextBoxServerConnectionString.Size = New System.Drawing.Size(523, 23)
        Me.TextBoxServerConnectionString.TabIndex = 0
        '
        'LabelServerConnectionString
        '
        Me.LabelServerConnectionString.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelServerConnectionString.AutoSize = True
        Me.LabelServerConnectionString.Location = New System.Drawing.Point(3, 7)
        Me.LabelServerConnectionString.Name = "LabelServerConnectionString"
        Me.LabelServerConnectionString.Size = New System.Drawing.Size(135, 15)
        Me.LabelServerConnectionString.TabIndex = 2
        Me.LabelServerConnectionString.Text = "Server connection string"
        '
        'LabelServerQuery
        '
        Me.LabelServerQuery.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelServerQuery.AutoSize = True
        Me.LabelServerQuery.Location = New System.Drawing.Point(3, 67)
        Me.LabelServerQuery.Name = "LabelServerQuery"
        Me.LabelServerQuery.Size = New System.Drawing.Size(39, 15)
        Me.LabelServerQuery.TabIndex = 3
        Me.LabelServerQuery.Text = "Query"
        '
        'FastColoredServerQuery
        '
        Me.FastColoredServerQuery.AutoCompleteBracketsList = New Char() {Global.Microsoft.VisualBasic.ChrW(40), Global.Microsoft.VisualBasic.ChrW(41), Global.Microsoft.VisualBasic.ChrW(123), Global.Microsoft.VisualBasic.ChrW(125), Global.Microsoft.VisualBasic.ChrW(91), Global.Microsoft.VisualBasic.ChrW(93), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(39), Global.Microsoft.VisualBasic.ChrW(39)}
        Me.FastColoredServerQuery.AutoIndentCharsPatterns = ""
        Me.FastColoredServerQuery.AutoScrollMinSize = New System.Drawing.Size(2, 14)
        Me.FastColoredServerQuery.BackBrush = Nothing
        Me.FastColoredServerQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FastColoredServerQuery.CharHeight = 14
        Me.FastColoredServerQuery.CharWidth = 8
        Me.FastColoredServerQuery.CommentPrefix = "--"
        Me.FastColoredServerQuery.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.FastColoredServerQuery.DisabledColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.FastColoredServerQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FastColoredServerQuery.IsReplaceMode = False
        Me.FastColoredServerQuery.Language = FastColoredTextBoxNS.Language.SQL
        Me.FastColoredServerQuery.LeftBracket = Global.Microsoft.VisualBasic.ChrW(40)
        Me.FastColoredServerQuery.Location = New System.Drawing.Point(3, 93)
        Me.FastColoredServerQuery.Name = "FastColoredServerQuery"
        Me.FastColoredServerQuery.Paddings = New System.Windows.Forms.Padding(0)
        Me.FastColoredServerQuery.RightBracket = Global.Microsoft.VisualBasic.ChrW(41)
        Me.FastColoredServerQuery.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.FastColoredServerQuery.ServiceColors = CType(resources.GetObject("FastColoredServerQuery.ServiceColors"), FastColoredTextBoxNS.ServiceColors)
        Me.FastColoredServerQuery.Size = New System.Drawing.Size(523, 94)
        Me.FastColoredServerQuery.TabIndex = 6
        Me.FastColoredServerQuery.Zoom = 100
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 200)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(386, 105)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'TabPageGeneral
        '
        Me.TabPageGeneral.AutoScroll = True
        Me.TabPageGeneral.Controls.Add(Me.ExTableLayoutPanel2)
        Me.TabPageGeneral.ImageKey = "config"
        Me.TabPageGeneral.Location = New System.Drawing.Point(4, 24)
        Me.TabPageGeneral.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageGeneral.Name = "TabPageGeneral"
        Me.TabPageGeneral.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPageGeneral.Size = New System.Drawing.Size(535, 585)
        Me.TabPageGeneral.TabIndex = 6
        Me.TabPageGeneral.Text = "General"
        Me.TabPageGeneral.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel2
        '
        Me.ExTableLayoutPanel2.BackColor = System.Drawing.Color.White
        Me.ExTableLayoutPanel2.ColumnCount = 1
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.Controls.Add(Me.CheckBoxUseCurrentSession, 0, 0)
        Me.ExTableLayoutPanel2.Controls.Add(Me.CheckBoxPropertyFilterIncludeDraftItself, 0, 9)
        Me.ExTableLayoutPanel2.Controls.Add(Me.CheckBoxWarnSave, 0, 1)
        Me.ExTableLayoutPanel2.Controls.Add(Me.CheckBoxPropertyFilterIncludeDraftModel, 0, 8)
        Me.ExTableLayoutPanel2.Controls.Add(Me.CheckBoxNoUpdateMRU, 0, 2)
        Me.ExTableLayoutPanel2.Controls.Add(Me.CheckBoxRunInBackground, 0, 7)
        Me.ExTableLayoutPanel2.Controls.Add(Me.ExTableLayoutPanel3, 0, 4)
        Me.ExTableLayoutPanel2.Controls.Add(Me.CheckBoxRememberTasks, 0, 6)
        Me.ExTableLayoutPanel2.Controls.Add(Me.CheckBoxCheckForNewerVersion, 0, 10)
        Me.ExTableLayoutPanel2.Controls.Add(Me.CheckBoxGroupFiles, 0, 5)
        Me.ExTableLayoutPanel2.Controls.Add(Me.ExTableLayoutPanel11, 0, 3)
        Me.ExTableLayoutPanel2.Controls.Add(Me.CheckBoxRemindFilelistUpdate, 0, 11)
        Me.ExTableLayoutPanel2.Controls.Add(Me.CheckBoxProcessDraftsInactive, 0, 12)
        Me.ExTableLayoutPanel2.Controls.Add(Me.ExTableLayoutPanel12, 0, 13)
        Me.ExTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel2.Location = New System.Drawing.Point(4, 3)
        Me.ExTableLayoutPanel2.Name = "ExTableLayoutPanel2"
        Me.ExTableLayoutPanel2.RowCount = 15
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel2.Size = New System.Drawing.Size(527, 579)
        Me.ExTableLayoutPanel2.TabIndex = 74
        Me.ExTableLayoutPanel2.Task = Nothing
        '
        'CheckBoxUseCurrentSession
        '
        Me.CheckBoxUseCurrentSession.AutoSize = True
        Me.CheckBoxUseCurrentSession.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxUseCurrentSession.Location = New System.Drawing.Point(3, 3)
        Me.CheckBoxUseCurrentSession.Name = "CheckBoxUseCurrentSession"
        Me.CheckBoxUseCurrentSession.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxUseCurrentSession.Size = New System.Drawing.Size(521, 24)
        Me.CheckBoxUseCurrentSession.TabIndex = 72
        Me.CheckBoxUseCurrentSession.Text = "Use current Solid Edge session (if any)"
        Me.CheckBoxUseCurrentSession.UseVisualStyleBackColor = True
        '
        'CheckBoxPropertyFilterIncludeDraftItself
        '
        Me.CheckBoxPropertyFilterIncludeDraftItself.AutoSize = True
        Me.CheckBoxPropertyFilterIncludeDraftItself.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxPropertyFilterIncludeDraftItself.Location = New System.Drawing.Point(3, 273)
        Me.CheckBoxPropertyFilterIncludeDraftItself.Name = "CheckBoxPropertyFilterIncludeDraftItself"
        Me.CheckBoxPropertyFilterIncludeDraftItself.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxPropertyFilterIncludeDraftItself.Size = New System.Drawing.Size(521, 24)
        Me.CheckBoxPropertyFilterIncludeDraftItself.TabIndex = 73
        Me.CheckBoxPropertyFilterIncludeDraftItself.Text = "Property Filter -- Include the Draft file itself in search"
        Me.CheckBoxPropertyFilterIncludeDraftItself.UseVisualStyleBackColor = True
        '
        'CheckBoxWarnSave
        '
        Me.CheckBoxWarnSave.AutoSize = True
        Me.CheckBoxWarnSave.Checked = True
        Me.CheckBoxWarnSave.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxWarnSave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxWarnSave.Location = New System.Drawing.Point(3, 33)
        Me.CheckBoxWarnSave.Name = "CheckBoxWarnSave"
        Me.CheckBoxWarnSave.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxWarnSave.Size = New System.Drawing.Size(521, 24)
        Me.CheckBoxWarnSave.TabIndex = 70
        Me.CheckBoxWarnSave.Text = "Warn me if file save is required"
        Me.CheckBoxWarnSave.UseVisualStyleBackColor = True
        '
        'CheckBoxPropertyFilterIncludeDraftModel
        '
        Me.CheckBoxPropertyFilterIncludeDraftModel.AutoSize = True
        Me.CheckBoxPropertyFilterIncludeDraftModel.Checked = True
        Me.CheckBoxPropertyFilterIncludeDraftModel.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxPropertyFilterIncludeDraftModel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxPropertyFilterIncludeDraftModel.Location = New System.Drawing.Point(3, 243)
        Me.CheckBoxPropertyFilterIncludeDraftModel.Name = "CheckBoxPropertyFilterIncludeDraftModel"
        Me.CheckBoxPropertyFilterIncludeDraftModel.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxPropertyFilterIncludeDraftModel.Size = New System.Drawing.Size(521, 24)
        Me.CheckBoxPropertyFilterIncludeDraftModel.TabIndex = 52
        Me.CheckBoxPropertyFilterIncludeDraftModel.Text = "Property Filter -- Include Draft file model documents in search"
        Me.CheckBoxPropertyFilterIncludeDraftModel.UseVisualStyleBackColor = True
        '
        'CheckBoxNoUpdateMRU
        '
        Me.CheckBoxNoUpdateMRU.AutoSize = True
        Me.CheckBoxNoUpdateMRU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxNoUpdateMRU.Location = New System.Drawing.Point(3, 63)
        Me.CheckBoxNoUpdateMRU.Name = "CheckBoxNoUpdateMRU"
        Me.CheckBoxNoUpdateMRU.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxNoUpdateMRU.Size = New System.Drawing.Size(521, 24)
        Me.CheckBoxNoUpdateMRU.TabIndex = 71
        Me.CheckBoxNoUpdateMRU.Text = "Do not show processed files in Most Recently Used list"
        Me.CheckBoxNoUpdateMRU.UseVisualStyleBackColor = True
        '
        'CheckBoxRunInBackground
        '
        Me.CheckBoxRunInBackground.AutoSize = True
        Me.CheckBoxRunInBackground.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxRunInBackground.Location = New System.Drawing.Point(3, 213)
        Me.CheckBoxRunInBackground.Name = "CheckBoxRunInBackground"
        Me.CheckBoxRunInBackground.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxRunInBackground.Size = New System.Drawing.Size(521, 24)
        Me.CheckBoxRunInBackground.TabIndex = 49
        Me.CheckBoxRunInBackground.Text = "Process tasks in background (no graphics)"
        Me.CheckBoxRunInBackground.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel3
        '
        Me.ExTableLayoutPanel3.ColumnCount = 2
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel3.Controls.Add(Me.TextBoxFileListFontSize, 0, 0)
        Me.ExTableLayoutPanel3.Controls.Add(Me.LabelFontSize, 1, 0)
        Me.ExTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel3.Location = New System.Drawing.Point(3, 123)
        Me.ExTableLayoutPanel3.Name = "ExTableLayoutPanel3"
        Me.ExTableLayoutPanel3.RowCount = 1
        Me.ExTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.ExTableLayoutPanel3.Size = New System.Drawing.Size(521, 24)
        Me.ExTableLayoutPanel3.TabIndex = 73
        Me.ExTableLayoutPanel3.Task = Nothing
        '
        'TextBoxFileListFontSize
        '
        Me.TextBoxFileListFontSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxFileListFontSize.Location = New System.Drawing.Point(5, 1)
        Me.TextBoxFileListFontSize.Margin = New System.Windows.Forms.Padding(5, 1, 4, 3)
        Me.TextBoxFileListFontSize.Name = "TextBoxFileListFontSize"
        Me.TextBoxFileListFontSize.Size = New System.Drawing.Size(41, 23)
        Me.TextBoxFileListFontSize.TabIndex = 54
        Me.TextBoxFileListFontSize.Text = "9"
        '
        'LabelFontSize
        '
        Me.LabelFontSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelFontSize.AutoSize = True
        Me.LabelFontSize.Location = New System.Drawing.Point(54, 4)
        Me.LabelFontSize.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelFontSize.Name = "LabelFontSize"
        Me.LabelFontSize.Size = New System.Drawing.Size(90, 15)
        Me.LabelFontSize.TabIndex = 55
        Me.LabelFontSize.Text = "File list font size"
        Me.LabelFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CheckBoxRememberTasks
        '
        Me.CheckBoxRememberTasks.AutoSize = True
        Me.CheckBoxRememberTasks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxRememberTasks.Location = New System.Drawing.Point(3, 183)
        Me.CheckBoxRememberTasks.Name = "CheckBoxRememberTasks"
        Me.CheckBoxRememberTasks.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxRememberTasks.Size = New System.Drawing.Size(521, 24)
        Me.CheckBoxRememberTasks.TabIndex = 48
        Me.CheckBoxRememberTasks.Text = "Remember selected tasks between sessions"
        Me.CheckBoxRememberTasks.UseVisualStyleBackColor = True
        '
        'CheckBoxCheckForNewerVersion
        '
        Me.CheckBoxCheckForNewerVersion.AutoSize = True
        Me.CheckBoxCheckForNewerVersion.Checked = True
        Me.CheckBoxCheckForNewerVersion.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxCheckForNewerVersion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxCheckForNewerVersion.Location = New System.Drawing.Point(3, 303)
        Me.CheckBoxCheckForNewerVersion.Name = "CheckBoxCheckForNewerVersion"
        Me.CheckBoxCheckForNewerVersion.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxCheckForNewerVersion.Size = New System.Drawing.Size(521, 24)
        Me.CheckBoxCheckForNewerVersion.TabIndex = 74
        Me.CheckBoxCheckForNewerVersion.Text = "Check for newer version at startup"
        Me.CheckBoxCheckForNewerVersion.UseVisualStyleBackColor = True
        '
        'CheckBoxGroupFiles
        '
        Me.CheckBoxGroupFiles.AutoSize = True
        Me.CheckBoxGroupFiles.Checked = True
        Me.CheckBoxGroupFiles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxGroupFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxGroupFiles.Location = New System.Drawing.Point(3, 153)
        Me.CheckBoxGroupFiles.Name = "CheckBoxGroupFiles"
        Me.CheckBoxGroupFiles.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxGroupFiles.Size = New System.Drawing.Size(521, 24)
        Me.CheckBoxGroupFiles.TabIndex = 75
        Me.CheckBoxGroupFiles.Text = "Group files by type"
        Me.CheckBoxGroupFiles.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel11
        '
        Me.ExTableLayoutPanel11.ColumnCount = 2
        Me.ExTableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.ExTableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel11.Controls.Add(Me.TextBoxListViewUpdateFrequency, 0, 0)
        Me.ExTableLayoutPanel11.Controls.Add(Me.LabelListViewUpdateFrequency, 1, 0)
        Me.ExTableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel11.Location = New System.Drawing.Point(3, 93)
        Me.ExTableLayoutPanel11.Name = "ExTableLayoutPanel11"
        Me.ExTableLayoutPanel11.RowCount = 1
        Me.ExTableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.ExTableLayoutPanel11.Size = New System.Drawing.Size(521, 24)
        Me.ExTableLayoutPanel11.TabIndex = 76
        Me.ExTableLayoutPanel11.Task = Nothing
        '
        'TextBoxListViewUpdateFrequency
        '
        Me.TextBoxListViewUpdateFrequency.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxListViewUpdateFrequency.Location = New System.Drawing.Point(3, 3)
        Me.TextBoxListViewUpdateFrequency.Name = "TextBoxListViewUpdateFrequency"
        Me.TextBoxListViewUpdateFrequency.Size = New System.Drawing.Size(44, 23)
        Me.TextBoxListViewUpdateFrequency.TabIndex = 0
        Me.TextBoxListViewUpdateFrequency.Text = "1"
        '
        'LabelListViewUpdateFrequency
        '
        Me.LabelListViewUpdateFrequency.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelListViewUpdateFrequency.AutoSize = True
        Me.LabelListViewUpdateFrequency.Location = New System.Drawing.Point(54, 4)
        Me.LabelListViewUpdateFrequency.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelListViewUpdateFrequency.Name = "LabelListViewUpdateFrequency"
        Me.LabelListViewUpdateFrequency.Size = New System.Drawing.Size(304, 15)
        Me.LabelListViewUpdateFrequency.TabIndex = 1
        Me.LabelListViewUpdateFrequency.Text = "When processing, update the file list after this many files"
        '
        'CheckBoxRemindFilelistUpdate
        '
        Me.CheckBoxRemindFilelistUpdate.AutoSize = True
        Me.CheckBoxRemindFilelistUpdate.Checked = True
        Me.CheckBoxRemindFilelistUpdate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRemindFilelistUpdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxRemindFilelistUpdate.Location = New System.Drawing.Point(3, 333)
        Me.CheckBoxRemindFilelistUpdate.Name = "CheckBoxRemindFilelistUpdate"
        Me.CheckBoxRemindFilelistUpdate.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxRemindFilelistUpdate.Size = New System.Drawing.Size(521, 24)
        Me.CheckBoxRemindFilelistUpdate.TabIndex = 77
        Me.CheckBoxRemindFilelistUpdate.Text = "Remind me if I need to update the file list"
        Me.CheckBoxRemindFilelistUpdate.UseVisualStyleBackColor = True
        '
        'CheckBoxProcessDraftsInactive
        '
        Me.CheckBoxProcessDraftsInactive.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxProcessDraftsInactive.AutoSize = True
        Me.CheckBoxProcessDraftsInactive.Location = New System.Drawing.Point(3, 365)
        Me.CheckBoxProcessDraftsInactive.Name = "CheckBoxProcessDraftsInactive"
        Me.CheckBoxProcessDraftsInactive.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxProcessDraftsInactive.Size = New System.Drawing.Size(181, 19)
        Me.CheckBoxProcessDraftsInactive.TabIndex = 78
        Me.CheckBoxProcessDraftsInactive.Text = "Process draft files as inactive"
        Me.CheckBoxProcessDraftsInactive.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel12
        '
        Me.ExTableLayoutPanel12.ColumnCount = 2
        Me.ExTableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.ExTableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel12.Controls.Add(Me.ComboBoxExpressionEditorLanguage, 0, 0)
        Me.ExTableLayoutPanel12.Controls.Add(Me.LabelExpressionEditorLanguage, 1, 0)
        Me.ExTableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel12.Location = New System.Drawing.Point(3, 393)
        Me.ExTableLayoutPanel12.Name = "ExTableLayoutPanel12"
        Me.ExTableLayoutPanel12.RowCount = 1
        Me.ExTableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel12.Size = New System.Drawing.Size(521, 24)
        Me.ExTableLayoutPanel12.TabIndex = 79
        Me.ExTableLayoutPanel12.Task = Nothing
        '
        'ComboBoxExpressionEditorLanguage
        '
        Me.ComboBoxExpressionEditorLanguage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxExpressionEditorLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxExpressionEditorLanguage.FormattingEnabled = True
        Me.ComboBoxExpressionEditorLanguage.Items.AddRange(New Object() {"NCalc", "VB"})
        Me.ComboBoxExpressionEditorLanguage.Location = New System.Drawing.Point(3, 3)
        Me.ComboBoxExpressionEditorLanguage.Name = "ComboBoxExpressionEditorLanguage"
        Me.ComboBoxExpressionEditorLanguage.Size = New System.Drawing.Size(69, 23)
        Me.ComboBoxExpressionEditorLanguage.TabIndex = 0
        '
        'LabelExpressionEditorLanguage
        '
        Me.LabelExpressionEditorLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelExpressionEditorLanguage.AutoSize = True
        Me.LabelExpressionEditorLanguage.Location = New System.Drawing.Point(78, 4)
        Me.LabelExpressionEditorLanguage.Name = "LabelExpressionEditorLanguage"
        Me.LabelExpressionEditorLanguage.Size = New System.Drawing.Size(148, 15)
        Me.LabelExpressionEditorLanguage.TabIndex = 1
        Me.LabelExpressionEditorLanguage.Text = "Expression editor language"
        '
        'ToolStripPresets
        '
        Me.ToolStripPresets.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.SetColumnSpan(Me.ToolStripPresets, 4)
        Me.ToolStripPresets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripPresets.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripPresets.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LabelPreset, Me.ToolStripSeparator11, Me.ComboBoxPresetName, Me.ButtonPresetLoad, Me.ButtonPresetSave, Me.ButtonPresetDelete})
        Me.ToolStripPresets.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripPresets.Name = "ToolStripPresets"
        Me.ToolStripPresets.Padding = New System.Windows.Forms.Padding(5, 0, 1, 0)
        Me.ToolStripPresets.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStripPresets.Size = New System.Drawing.Size(559, 26)
        Me.ToolStripPresets.TabIndex = 75
        Me.ToolStripPresets.Text = "ToolStrip1"
        '
        'LabelPreset
        '
        Me.LabelPreset.Image = CType(resources.GetObject("LabelPreset.Image"), System.Drawing.Image)
        Me.LabelPreset.Name = "LabelPreset"
        Me.LabelPreset.Size = New System.Drawing.Size(60, 23)
        Me.LabelPreset.Text = "Presets"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 26)
        '
        'ComboBoxPresetName
        '
        Me.ComboBoxPresetName.Name = "ComboBoxPresetName"
        Me.ComboBoxPresetName.Size = New System.Drawing.Size(200, 26)
        Me.ComboBoxPresetName.Sorted = True
        Me.ComboBoxPresetName.ToolTipText = "Select preset"
        '
        'ButtonPresetLoad
        '
        Me.ButtonPresetLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonPresetLoad.Image = CType(resources.GetObject("ButtonPresetLoad.Image"), System.Drawing.Image)
        Me.ButtonPresetLoad.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonPresetLoad.Name = "ButtonPresetLoad"
        Me.ButtonPresetLoad.Size = New System.Drawing.Size(23, 23)
        Me.ButtonPresetLoad.Text = "ToolStripButton2"
        Me.ButtonPresetLoad.ToolTipText = "Load preset"
        '
        'ButtonPresetSave
        '
        Me.ButtonPresetSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonPresetSave.Image = CType(resources.GetObject("ButtonPresetSave.Image"), System.Drawing.Image)
        Me.ButtonPresetSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonPresetSave.Name = "ButtonPresetSave"
        Me.ButtonPresetSave.Size = New System.Drawing.Size(23, 23)
        Me.ButtonPresetSave.Text = "ToolStripButton2"
        Me.ButtonPresetSave.ToolTipText = "Save preset"
        '
        'ButtonPresetDelete
        '
        Me.ButtonPresetDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonPresetDelete.Image = CType(resources.GetObject("ButtonPresetDelete.Image"), System.Drawing.Image)
        Me.ButtonPresetDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonPresetDelete.Name = "ButtonPresetDelete"
        Me.ButtonPresetDelete.Size = New System.Drawing.Size(23, 23)
        Me.ButtonPresetDelete.Text = "ToolStripButton2"
        Me.ButtonPresetDelete.ToolTipText = "Delete preset"
        '
        'TextBoxStatus
        '
        Me.TextBoxStatus.BackColor = System.Drawing.Color.White
        Me.TextBoxStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.TextBoxStatus, 4)
        Me.TextBoxStatus.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBoxStatus.Location = New System.Drawing.Point(2, 28)
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
        Me.LabelTimeRemaining.Size = New System.Drawing.Size(0, 15)
        Me.LabelTimeRemaining.TabIndex = 4
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ToolStripPresets, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonProcess, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonCancel, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxStatus, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonHelp, 3, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 647)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(559, 105)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'ButtonProcess
        '
        Me.ButtonProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonProcess.Image = CType(resources.GetObject("ButtonProcess.Image"), System.Drawing.Image)
        Me.ButtonProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonProcess.Location = New System.Drawing.Point(171, 54)
        Me.ButtonProcess.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonProcess.Name = "ButtonProcess"
        Me.ButtonProcess.Size = New System.Drawing.Size(126, 49)
        Me.ButtonProcess.TabIndex = 3
        Me.ButtonProcess.Text = "Process"
        Me.ButtonProcess.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ButtonCancel.Image = CType(resources.GetObject("ButtonCancel.Image"), System.Drawing.Image)
        Me.ButtonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCancel.Location = New System.Drawing.Point(301, 54)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(126, 49)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonHelp
        '
        Me.ButtonHelp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonHelp.Image = CType(resources.GetObject("ButtonHelp.Image"), System.Drawing.Image)
        Me.ButtonHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonHelp.Location = New System.Drawing.Point(432, 55)
        Me.ButtonHelp.Name = "ButtonHelp"
        Me.ButtonHelp.Size = New System.Drawing.Size(124, 47)
        Me.ButtonHelp.TabIndex = 4
        Me.ButtonHelp.Text = "Help"
        Me.ButtonHelp.UseVisualStyleBackColor = True
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 23)
        '
        'Menu_ListViewFile
        '
        Me.Menu_ListViewFile.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BT_Open, Me.BT_OpenFolder, Me.ToolStripSeparator8, Me.BT_FindLinks, Me.BT_ProcessSelected, Me.ToolStripSeparator5, Me.BT_ExcludeFromProcessing, Me.BT_RemoveFromList, Me.ToolStripSeparator13, Me.BT_MoveToRecycleBin})
        Me.Menu_ListViewFile.Name = "Menu_ListViewFile"
        Me.Menu_ListViewFile.Size = New System.Drawing.Size(206, 176)
        '
        'BT_Open
        '
        Me.BT_Open.Image = CType(resources.GetObject("BT_Open.Image"), System.Drawing.Image)
        Me.BT_Open.Name = "BT_Open"
        Me.BT_Open.Size = New System.Drawing.Size(205, 22)
        Me.BT_Open.Text = "Open"
        '
        'BT_OpenFolder
        '
        Me.BT_OpenFolder.Image = CType(resources.GetObject("BT_OpenFolder.Image"), System.Drawing.Image)
        Me.BT_OpenFolder.Name = "BT_OpenFolder"
        Me.BT_OpenFolder.Size = New System.Drawing.Size(205, 22)
        Me.BT_OpenFolder.Text = "Open folder"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(202, 6)
        '
        'BT_FindLinks
        '
        Me.BT_FindLinks.Image = CType(resources.GetObject("BT_FindLinks.Image"), System.Drawing.Image)
        Me.BT_FindLinks.Name = "BT_FindLinks"
        Me.BT_FindLinks.Size = New System.Drawing.Size(205, 22)
        Me.BT_FindLinks.Text = "Find linked files"
        '
        'BT_ProcessSelected
        '
        Me.BT_ProcessSelected.Image = CType(resources.GetObject("BT_ProcessSelected.Image"), System.Drawing.Image)
        Me.BT_ProcessSelected.Name = "BT_ProcessSelected"
        Me.BT_ProcessSelected.Size = New System.Drawing.Size(205, 22)
        Me.BT_ProcessSelected.Text = "Process selected"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(202, 6)
        '
        'BT_ExcludeFromProcessing
        '
        Me.BT_ExcludeFromProcessing.Image = Global.Housekeeper.My.Resources.Resources.Hide
        Me.BT_ExcludeFromProcessing.Name = "BT_ExcludeFromProcessing"
        Me.BT_ExcludeFromProcessing.Size = New System.Drawing.Size(205, 22)
        Me.BT_ExcludeFromProcessing.Text = "Exclude from processing"
        '
        'BT_RemoveFromList
        '
        Me.BT_RemoveFromList.Image = CType(resources.GetObject("BT_RemoveFromList.Image"), System.Drawing.Image)
        Me.BT_RemoveFromList.Name = "BT_RemoveFromList"
        Me.BT_RemoveFromList.Size = New System.Drawing.Size(205, 22)
        Me.BT_RemoveFromList.Text = "Remove from list"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(202, 6)
        '
        'BT_MoveToRecycleBin
        '
        Me.BT_MoveToRecycleBin.Image = CType(resources.GetObject("BT_MoveToRecycleBin.Image"), System.Drawing.Image)
        Me.BT_MoveToRecycleBin.Name = "BT_MoveToRecycleBin"
        Me.BT_MoveToRecycleBin.Size = New System.Drawing.Size(205, 22)
        Me.BT_MoveToRecycleBin.Text = "Move files to Recycle Bin"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'Form_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(559, 752)
        Me.Controls.Add(Me.LabelTimeRemaining)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(400, 200)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(575, 450)
        Me.Name = "Form_Main"
        Me.Text = "Solid Edge Housekeeper"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageHome.ResumeLayout(False)
        Me.TabPageHome.PerformLayout()
        Me.ColumnSelectionPanel.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
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
        Me.ExTableLayoutPanel4.ResumeLayout(False)
        Me.ExTableLayoutPanel4.PerformLayout()
        Me.ExTableLayoutPanel5.ResumeLayout(False)
        Me.ExTableLayoutPanel5.PerformLayout()
        Me.ExTableLayoutPanel10.ResumeLayout(False)
        Me.ExTableLayoutPanel10.PerformLayout()
        Me.TabPageStatus.ResumeLayout(False)
        Me.ExTableLayoutPanel6.ResumeLayout(False)
        Me.ExTableLayoutPanel6.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBoxStatusInA.ResumeLayout(False)
        Me.GroupBoxStatusInA.PerformLayout()
        Me.GroupBoxStatusInB.ResumeLayout(False)
        Me.GroupBoxStatusInB.PerformLayout()
        Me.GroupBoxStatusInIR.ResumeLayout(False)
        Me.GroupBoxStatusInIR.PerformLayout()
        Me.GroupBoxStatusInR.ResumeLayout(False)
        Me.GroupBoxStatusInR.PerformLayout()
        Me.GroupBoxStatusInIW.ResumeLayout(False)
        Me.GroupBoxStatusInIW.PerformLayout()
        Me.GroupBoxStatusInO.ResumeLayout(False)
        Me.GroupBoxStatusInO.PerformLayout()
        Me.TabPageSorting.ResumeLayout(False)
        Me.ExTableLayoutPanel7.ResumeLayout(False)
        Me.ExTableLayoutPanel7.PerformLayout()
        Me.ExTableLayoutPanel8.ResumeLayout(False)
        Me.ExTableLayoutPanel8.PerformLayout()
        Me.TabPageTemplates.ResumeLayout(False)
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.PerformLayout()
        Me.TabPageServerQuery.ResumeLayout(False)
        Me.ExTableLayoutPanel9.ResumeLayout(False)
        Me.ExTableLayoutPanel9.PerformLayout()
        CType(Me.FastColoredServerQuery, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageGeneral.ResumeLayout(False)
        Me.ExTableLayoutPanel2.ResumeLayout(False)
        Me.ExTableLayoutPanel2.PerformLayout()
        Me.ExTableLayoutPanel3.ResumeLayout(False)
        Me.ExTableLayoutPanel3.PerformLayout()
        Me.ExTableLayoutPanel11.ResumeLayout(False)
        Me.ExTableLayoutPanel11.PerformLayout()
        Me.ExTableLayoutPanel12.ResumeLayout(False)
        Me.ExTableLayoutPanel12.PerformLayout()
        Me.ToolStripPresets.ResumeLayout(False)
        Me.ToolStripPresets.PerformLayout()
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
    Friend WithEvents LabelTimeRemaining As Label
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents TabPage_ImageList As ImageList
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ListViewFiles As ListViewCollapsible
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
    Friend WithEvents BT_RemoveFromList As ToolStripMenuItem
    Friend WithEvents ToolStrip_Filter As ToolStrip
    Friend WithEvents CheckBoxEnablePropertyFilter As ToolStripButton
    Friend WithEvents new_ButtonPropertyFilter As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents CheckBoxEnableFileWildcard As ToolStripButton
    Friend WithEvents ComboBoxFileWildcard As ToolStripComboBox
    Friend WithEvents new_ButtonFileSearchDelete As ToolStripButton
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents BT_ProcessSelected As ToolStripMenuItem
    Friend WithEvents BT_FindLinks As ToolStripMenuItem
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPageTopLevelAssy As TabPage
    Friend WithEvents CheckBoxTLAIncludePartCopies As CheckBox
    Friend WithEvents CheckBoxDraftAndModelSameName As CheckBox
    Friend WithEvents CheckBoxWarnBareTLA As CheckBox
    Friend WithEvents CheckBoxTLAAutoIncludeTLF As CheckBox
    Friend WithEvents ButtonFastSearchScopeFilename As Button
    Friend WithEvents TextBoxFastSearchScopeFilename As TextBox
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
    Friend WithEvents RadioButtonProcessAsAvailableChange As RadioButton
    Friend WithEvents RadioButtonProcessAsAvailableRevert As RadioButton
    Friend WithEvents CheckBoxProcessAsAvailable As CheckBox
    Friend WithEvents TabPageSorting As TabPage
    Friend WithEvents TextBoxSortRandomSampleFraction As TextBox
    Friend WithEvents LabelSortRandomSampleFraction As Label
    Friend WithEvents RadioButtonSortRandomSample As RadioButton
    Friend WithEvents CheckBoxSortIncludeNoDependencies As CheckBox
    Friend WithEvents RadioButtonSortDependency As RadioButton
    Friend WithEvents RadioButtonSortAlphabetical As RadioButton
    Friend WithEvents RadioButtonSortNone As RadioButton
    Friend WithEvents TabPageGeneral As TabPage
    Friend WithEvents CheckBoxPropertyFilterIncludeDraftModel As CheckBox
    Friend WithEvents CheckBoxRunInBackground As CheckBox
    Friend WithEvents CheckBoxRememberTasks As CheckBox
    Friend WithEvents TextBoxFileListFontSize As TextBox
    Friend WithEvents LabelFontSize As Label
    Friend WithEvents TabPageTasks As TabPage
    Friend WithEvents CheckBoxNoUpdateMRU As CheckBox
    Friend WithEvents CheckBoxWarnSave As CheckBox
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents CheckBoxUseCurrentSession As CheckBox
    Friend WithEvents CheckBoxPropertyFilterIncludeDraftItself As CheckBox
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
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents BT_Help As ToolStripButton
    Friend WithEvents TabPageTemplates As TabPage
    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents ButtonAssemblyTemplate As Button
    Friend WithEvents TextBoxAssemblyTemplate As TextBox
    Friend WithEvents ButtonPartTemplate As Button
    Friend WithEvents TextBoxPartTemplate As TextBox
    Friend WithEvents ButtonSheetmetalTemplate As Button
    Friend WithEvents TextBoxSheetmetalTemplate As TextBox
    Friend WithEvents ButtonDraftTemplate As Button
    Friend WithEvents TextBoxDraftTemplate As TextBox
    Friend WithEvents ButtonMaterialTable As Button
    Friend WithEvents TextBoxMaterialTable As TextBox
    Friend WithEvents ButtonCustomizePropertiesData As Button
    Friend WithEvents LabelCustomizeTemplatePropertyDict As Label
    Friend WithEvents ButtonUpdatePropertiesData As Button
    Friend WithEvents ExTableLayoutPanel2 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel3 As ExTableLayoutPanel
    Friend WithEvents CheckBoxCheckForNewerVersion As CheckBox
    Friend WithEvents ExTableLayoutPanel4 As ExTableLayoutPanel
    Friend WithEvents LabelTLAListOptions As Label
    Friend WithEvents LabelTLASearchOptions As Label
    Friend WithEvents ExTableLayoutPanel5 As ExTableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ExTableLayoutPanel6 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel7 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel8 As ExTableLayoutPanel
    Friend WithEvents ButtonHelp As Button
    Friend WithEvents LabelUpdateProperties As Label
    Friend WithEvents BT_ColumnsSelect As ToolStripButton
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents CLB_Properties As CheckedListBox
    Friend WithEvents ColumnSelectionPanel As Panel
    Friend WithEvents ButtonCloseListOfColumns As Button
    Friend WithEvents ButtonAddToListOfColumns As Button
    Friend WithEvents BT_DeleteCLBItem As Button
    Friend WithEvents TabPageServerQuery As TabPage
    Friend WithEvents ExTableLayoutPanel9 As ExTableLayoutPanel
    Friend WithEvents TextBoxServerConnectionString As TextBox
    Friend WithEvents LabelServerConnectionString As Label
    Friend WithEvents LabelServerQuery As Label
    Friend WithEvents ToolStripPresets As ToolStrip
    Friend WithEvents ComboBoxPresetName As ToolStripComboBox
    Friend WithEvents ButtonPresetLoad As ToolStripButton
    Friend WithEvents ButtonPresetSave As ToolStripButton
    Friend WithEvents ButtonPresetDelete As ToolStripButton
    Friend WithEvents LabelPreset As ToolStripLabel
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents FastColoredServerQuery As FastColoredTextBoxNS.FastColoredTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CheckBoxGroupFiles As CheckBox
    Friend WithEvents ListViewSources As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents BT_AddSingleFiles As ToolStripButton
    Friend WithEvents ExTableLayoutPanel10 As ExTableLayoutPanel
    Friend WithEvents ButtonLinkManagementFilename As Button
    Friend WithEvents TextBoxLinkManagementFilename As TextBox
    Friend WithEvents ExTableLayoutPanel11 As ExTableLayoutPanel
    Friend WithEvents TextBoxListViewUpdateFrequency As TextBox
    Friend WithEvents LabelListViewUpdateFrequency As Label
    Friend WithEvents BT_MoveToRecycleBin As ToolStripMenuItem
    Friend WithEvents CheckBoxKeepUnsortedDuplicates As CheckBox
    Friend WithEvents BT_AddTeamCenter As ToolStripButton
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents CheckBoxRemindFilelistUpdate As CheckBox
    Friend WithEvents CheckBoxProcessDraftsInactive As CheckBox
    Friend WithEvents BT_ExcludeFromProcessing As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As ToolStripSeparator
    Friend WithEvents ExTableLayoutPanel12 As ExTableLayoutPanel
    Friend WithEvents ComboBoxExpressionEditorLanguage As ComboBox
    Friend WithEvents LabelExpressionEditorLanguage As Label
End Class
