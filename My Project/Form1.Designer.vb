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
        Me.TabPageAssembly = New System.Windows.Forms.TabPage()
        Me.CheckBoxFindReplaceReplaceRXAssembly = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceReplacePTAssembly = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceFindRXAssembly = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceFindWCAssembly = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceFindPTAssembly = New System.Windows.Forms.CheckBox()
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
        Me.ButtonVariableEditPart = New System.Windows.Forms.Button()
        Me.TextBoxVariableEditPart = New System.Windows.Forms.TextBox()
        Me.CheckBoxFindReplaceReplaceRXPart = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceReplacePTPart = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceFindRXPart = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceFindWCPart = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceFindPTPart = New System.Windows.Forms.CheckBox()
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
        Me.CheckBoxFindReplaceReplaceRXSheetmetal = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceReplacePTSheetmetal = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceFindRXSheetmetal = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceFindWCSheetmetal = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceFindPTSheetmetal = New System.Windows.Forms.CheckBox()
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
        Me.CheckBoxFindReplaceReplaceRXDraft = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceReplacePTDraft = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceFindRXDraft = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceFindWCDraft = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFindReplaceFindPTDraft = New System.Windows.Forms.CheckBox()
        Me.TextBoxFindReplaceReplaceDraft = New System.Windows.Forms.TextBox()
        Me.TextBoxFindReplaceFindDraft = New System.Windows.Forms.TextBox()
        Me.TextBoxFindReplacePropertyNameDraft = New System.Windows.Forms.TextBox()
        Me.ComboBoxFindReplacePropertySetDraft = New System.Windows.Forms.ComboBox()
        Me.LabelFindReplaceReplaceDraft = New System.Windows.Forms.Label()
        Me.LabelFindReplaceFindDraft = New System.Windows.Forms.Label()
        Me.LabelFindReplacePropertyNameDraft = New System.Windows.Forms.Label()
        Me.LabelFindReplacePropertySetDraft = New System.Windows.Forms.Label()
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
        Me.TabPageSorting = New System.Windows.Forms.TabPage()
        Me.TextBoxRandomSampleFraction = New System.Windows.Forms.TextBox()
        Me.LabelRandomSampleFraction = New System.Windows.Forms.Label()
        Me.RadioButtonListSortRandomSample = New System.Windows.Forms.RadioButton()
        Me.CheckBoxListIncludeNoDependencies = New System.Windows.Forms.CheckBox()
        Me.RadioButtonListSortDependency = New System.Windows.Forms.RadioButton()
        Me.RadioButtonListSortAlphabetical = New System.Windows.Forms.RadioButton()
        Me.RadioButtonListSortNone = New System.Windows.Forms.RadioButton()
        Me.TabPageOpenSave = New System.Windows.Forms.TabPage()
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
        Me.CheckBoxSaveAsPDFPerSheetSupress = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSaveAsImageCrop = New System.Windows.Forms.CheckBox()
        Me.CheckBoxRunExternalProgramSaveFile = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoUpdateMRU = New System.Windows.Forms.CheckBox()
        Me.CheckBoxWarnSave = New System.Windows.Forms.CheckBox()
        Me.TabPageTemplates = New System.Windows.Forms.TabPage()
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
        Me.TabPagePrinting = New System.Windows.Forms.TabPage()
        Me.GroupBoxPrinter2 = New System.Windows.Forms.GroupBox()
        Me.ButtonPrinter2SheetSelections = New System.Windows.Forms.Button()
        Me.TextBoxPrinter2SheetSelections = New System.Windows.Forms.TextBox()
        Me.LabelPrinter2SheetSelections = New System.Windows.Forms.Label()
        Me.RadioButtonSheetsAllPrinter2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSheetsIsoPrinter2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSheetsAnsiPrinter2 = New System.Windows.Forms.RadioButton()
        Me.CheckBoxEnablePrinter2 = New System.Windows.Forms.CheckBox()
        Me.TextBoxPrinter2Copies = New System.Windows.Forms.TextBox()
        Me.CheckBoxPrinter2AutoOrient = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPrinter2BestFit = New System.Windows.Forms.CheckBox()
        Me.LabelPrinter2Copies = New System.Windows.Forms.Label()
        Me.CheckBoxPrinter2PrintAsBlack = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPrinter2ScaleLineTypes = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPrinter2ScaleLineWidths = New System.Windows.Forms.CheckBox()
        Me.ComboBoxPrinter2 = New System.Windows.Forms.ComboBox()
        Me.GroupBoxPrinter1 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxEnablePrinter1 = New System.Windows.Forms.CheckBox()
        Me.TextBoxPrinter1Copies = New System.Windows.Forms.TextBox()
        Me.CheckBoxPrinter1AutoOrient = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPrinter1BestFit = New System.Windows.Forms.CheckBox()
        Me.LabelPrinter1Copies = New System.Windows.Forms.Label()
        Me.CheckBoxPrinter1PrintAsBlack = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPrinter1ScaleLineTypes = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPrinter1ScaleLineWidths = New System.Windows.Forms.CheckBox()
        Me.ComboBoxPrinter1 = New System.Windows.Forms.ComboBox()
        Me.TabPageGeneral = New System.Windows.Forms.TabPage()
        Me.LabelFontSize = New System.Windows.Forms.Label()
        Me.TextBoxFontSize = New System.Windows.Forms.TextBox()
        Me.GroupBoxPictorialViews = New System.Windows.Forms.GroupBox()
        Me.RadioButtonPictorialViewTrimetric = New System.Windows.Forms.RadioButton()
        Me.RadioButtonPictorialViewDimetric = New System.Windows.Forms.RadioButton()
        Me.RadioButtonPictorialViewIsometric = New System.Windows.Forms.RadioButton()
        Me.CheckBoxPropertyFilterFollowDraftLinks = New System.Windows.Forms.CheckBox()
        Me.CheckBoxAutoAddMissingProperty = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPartCopiesRecursiveSearch = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess = New System.Windows.Forms.CheckBox()
        Me.CheckBoxBackgroundProcessing = New System.Windows.Forms.CheckBox()
        Me.CheckBoxRememberTasks = New System.Windows.Forms.CheckBox()
        Me.TextBoxPartNumberPropertyName = New System.Windows.Forms.TextBox()
        Me.LabelPartNumberPropertyName = New System.Windows.Forms.Label()
        Me.TextBoxRestartAfter = New System.Windows.Forms.TextBox()
        Me.ComboBoxPartNumberPropertySet = New System.Windows.Forms.ComboBox()
        Me.LabelPartNumberPropertySet = New System.Windows.Forms.Label()
        Me.LabelRestartAfter = New System.Windows.Forms.Label()
        Me.TabPageHelp = New System.Windows.Forms.TabPage()
        Me.LabelReadmeNavigation2 = New System.Windows.Forms.Label()
        Me.PictureBoxTableOfContents = New System.Windows.Forms.PictureBox()
        Me.LabelReadmeNavigation1 = New System.Windows.Forms.Label()
        Me.LinkLabelGitHubReadme = New System.Windows.Forms.LinkLabel()
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
        Me.TabControl1.SuspendLayout()
        Me.TabPageHome.SuspendLayout()
        Me.ToolStrip_Filter.SuspendLayout()
        Me.ToolStrip_List.SuspendLayout()
        Me.TabPageAssembly.SuspendLayout()
        Me.TabPagePart.SuspendLayout()
        Me.TabPageSheetmetal.SuspendLayout()
        Me.TabPageDraft.SuspendLayout()
        Me.TabPageConfiguration.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPageTopLevelAssy.SuspendLayout()
        Me.TabPageSorting.SuspendLayout()
        Me.TabPageOpenSave.SuspendLayout()
        Me.GroupBoxStatusInR.SuspendLayout()
        Me.GroupBoxStatusInO.SuspendLayout()
        Me.GroupBoxStatusInIW.SuspendLayout()
        Me.GroupBoxStatusInIR.SuspendLayout()
        Me.GroupBoxStatusInB.SuspendLayout()
        Me.GroupBoxStatusInA.SuspendLayout()
        Me.TabPageTemplates.SuspendLayout()
        Me.TabPagePrinting.SuspendLayout()
        Me.GroupBoxPrinter2.SuspendLayout()
        Me.GroupBoxPrinter1.SuspendLayout()
        Me.TabPageGeneral.SuspendLayout()
        Me.GroupBoxPictorialViews.SuspendLayout()
        Me.TabPageHelp.SuspendLayout()
        CType(Me.PictureBoxTableOfContents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Menu_ListViewFile.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageHome)
        Me.TabControl1.Controls.Add(Me.TabPageAssembly)
        Me.TabControl1.Controls.Add(Me.TabPagePart)
        Me.TabControl1.Controls.Add(Me.TabPageSheetmetal)
        Me.TabControl1.Controls.Add(Me.TabPageDraft)
        Me.TabControl1.Controls.Add(Me.TabPageConfiguration)
        Me.TabControl1.Controls.Add(Me.TabPageHelp)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.TabPage_ImageList
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(519, 718)
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
        Me.TabPageHome.Location = New System.Drawing.Point(4, 24)
        Me.TabPageHome.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageHome.Name = "TabPageHome"
        Me.TabPageHome.Padding = New System.Windows.Forms.Padding(2, 2, 2, 0)
        Me.TabPageHome.Size = New System.Drawing.Size(511, 690)
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
        Me.ListViewFiles.Name = "ListViewFiles"
        Me.ListViewFiles.Size = New System.Drawing.Size(505, 636)
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
        Me.ToolStrip_Filter.Location = New System.Drawing.Point(2, 663)
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
        Me.new_CheckBoxFileSearch.Size = New System.Drawing.Size(74, 22)
        Me.new_CheckBoxFileSearch.Text = "Wildcard"
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
        Me.ToolStripLabel1.Size = New System.Drawing.Size(55, 22)
        Me.ToolStripLabel1.Text = "File Type:"
        '
        'TabPageAssembly
        '
        Me.TabPageAssembly.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageAssembly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageAssembly.Controls.Add(Me.CheckBoxFindReplaceReplaceRXAssembly)
        Me.TabPageAssembly.Controls.Add(Me.CheckBoxFindReplaceReplacePTAssembly)
        Me.TabPageAssembly.Controls.Add(Me.CheckBoxFindReplaceFindRXAssembly)
        Me.TabPageAssembly.Controls.Add(Me.CheckBoxFindReplaceFindWCAssembly)
        Me.TabPageAssembly.Controls.Add(Me.CheckBoxFindReplaceFindPTAssembly)
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
        Me.TabPageAssembly.Location = New System.Drawing.Point(4, 24)
        Me.TabPageAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageAssembly.Name = "TabPageAssembly"
        Me.TabPageAssembly.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageAssembly.Size = New System.Drawing.Size(511, 690)
        Me.TabPageAssembly.TabIndex = 1
        Me.TabPageAssembly.Text = "Assembly"
        '
        'CheckBoxFindReplaceReplaceRXAssembly
        '
        Me.CheckBoxFindReplaceReplaceRXAssembly.AutoSize = True
        Me.CheckBoxFindReplaceReplaceRXAssembly.Location = New System.Drawing.Point(390, 572)
        Me.CheckBoxFindReplaceReplaceRXAssembly.Name = "CheckBoxFindReplaceReplaceRXAssembly"
        Me.CheckBoxFindReplaceReplaceRXAssembly.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceReplaceRXAssembly.TabIndex = 26
        Me.CheckBoxFindReplaceReplaceRXAssembly.Text = "RX"
        Me.CheckBoxFindReplaceReplaceRXAssembly.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceReplacePTAssembly
        '
        Me.CheckBoxFindReplaceReplacePTAssembly.AutoSize = True
        Me.CheckBoxFindReplaceReplacePTAssembly.Location = New System.Drawing.Point(350, 572)
        Me.CheckBoxFindReplaceReplacePTAssembly.Name = "CheckBoxFindReplaceReplacePTAssembly"
        Me.CheckBoxFindReplaceReplacePTAssembly.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceReplacePTAssembly.TabIndex = 25
        Me.CheckBoxFindReplaceReplacePTAssembly.Text = "PT"
        Me.CheckBoxFindReplaceReplacePTAssembly.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceFindRXAssembly
        '
        Me.CheckBoxFindReplaceFindRXAssembly.AutoSize = True
        Me.CheckBoxFindReplaceFindRXAssembly.Location = New System.Drawing.Point(300, 572)
        Me.CheckBoxFindReplaceFindRXAssembly.Name = "CheckBoxFindReplaceFindRXAssembly"
        Me.CheckBoxFindReplaceFindRXAssembly.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceFindRXAssembly.TabIndex = 24
        Me.CheckBoxFindReplaceFindRXAssembly.Text = "RX"
        Me.CheckBoxFindReplaceFindRXAssembly.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceFindWCAssembly
        '
        Me.CheckBoxFindReplaceFindWCAssembly.AutoSize = True
        Me.CheckBoxFindReplaceFindWCAssembly.Location = New System.Drawing.Point(255, 572)
        Me.CheckBoxFindReplaceFindWCAssembly.Name = "CheckBoxFindReplaceFindWCAssembly"
        Me.CheckBoxFindReplaceFindWCAssembly.Size = New System.Drawing.Size(44, 19)
        Me.CheckBoxFindReplaceFindWCAssembly.TabIndex = 23
        Me.CheckBoxFindReplaceFindWCAssembly.Text = "WC"
        Me.CheckBoxFindReplaceFindWCAssembly.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceFindPTAssembly
        '
        Me.CheckBoxFindReplaceFindPTAssembly.AutoSize = True
        Me.CheckBoxFindReplaceFindPTAssembly.Location = New System.Drawing.Point(215, 572)
        Me.CheckBoxFindReplaceFindPTAssembly.Name = "CheckBoxFindReplaceFindPTAssembly"
        Me.CheckBoxFindReplaceFindPTAssembly.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceFindPTAssembly.TabIndex = 22
        Me.CheckBoxFindReplaceFindPTAssembly.Text = "PT"
        Me.CheckBoxFindReplaceFindPTAssembly.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsFormulaAssembly
        '
        Me.TextBoxSaveAsFormulaAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsFormulaAssembly.Location = New System.Drawing.Point(8, 457)
        Me.TextBoxSaveAsFormulaAssembly.Name = "TextBoxSaveAsFormulaAssembly"
        Me.TextBoxSaveAsFormulaAssembly.Size = New System.Drawing.Size(462, 22)
        Me.TextBoxSaveAsFormulaAssembly.TabIndex = 21
        '
        'CheckBoxSaveAsFormulaAssembly
        '
        Me.CheckBoxSaveAsFormulaAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsFormulaAssembly.AutoSize = True
        Me.CheckBoxSaveAsFormulaAssembly.Location = New System.Drawing.Point(8, 435)
        Me.CheckBoxSaveAsFormulaAssembly.Name = "CheckBoxSaveAsFormulaAssembly"
        Me.CheckBoxSaveAsFormulaAssembly.Size = New System.Drawing.Size(148, 19)
        Me.CheckBoxSaveAsFormulaAssembly.TabIndex = 20
        Me.CheckBoxSaveAsFormulaAssembly.Text = "Use subdirectory formula"
        Me.CheckBoxSaveAsFormulaAssembly.UseVisualStyleBackColor = True
        '
        'TextBoxExposeVariablesAssembly
        '
        Me.TextBoxExposeVariablesAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExposeVariablesAssembly.Location = New System.Drawing.Point(8, 653)
        Me.TextBoxExposeVariablesAssembly.Name = "TextBoxExposeVariablesAssembly"
        Me.TextBoxExposeVariablesAssembly.Size = New System.Drawing.Size(462, 22)
        Me.TextBoxExposeVariablesAssembly.TabIndex = 19
        '
        'LabelExposeVariablesAssembly
        '
        Me.LabelExposeVariablesAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExposeVariablesAssembly.AutoSize = True
        Me.LabelExposeVariablesAssembly.Location = New System.Drawing.Point(8, 630)
        Me.LabelExposeVariablesAssembly.Name = "LabelExposeVariablesAssembly"
        Me.LabelExposeVariablesAssembly.Size = New System.Drawing.Size(100, 15)
        Me.LabelExposeVariablesAssembly.TabIndex = 18
        Me.LabelExposeVariablesAssembly.Text = "Variables to expose"
        '
        'TextBoxFindReplaceReplaceAssembly
        '
        Me.TextBoxFindReplaceReplaceAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceReplaceAssembly.Location = New System.Drawing.Point(350, 595)
        Me.TextBoxFindReplaceReplaceAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceReplaceAssembly.Name = "TextBoxFindReplaceReplaceAssembly"
        Me.TextBoxFindReplaceReplaceAssembly.Size = New System.Drawing.Size(120, 22)
        Me.TextBoxFindReplaceReplaceAssembly.TabIndex = 17
        '
        'TextBoxFindReplaceFindAssembly
        '
        Me.TextBoxFindReplaceFindAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceFindAssembly.Location = New System.Drawing.Point(215, 595)
        Me.TextBoxFindReplaceFindAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceFindAssembly.Name = "TextBoxFindReplaceFindAssembly"
        Me.TextBoxFindReplaceFindAssembly.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceFindAssembly.TabIndex = 16
        '
        'TextBoxFindReplacePropertyNameAssembly
        '
        Me.TextBoxFindReplacePropertyNameAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplacePropertyNameAssembly.Location = New System.Drawing.Point(100, 595)
        Me.TextBoxFindReplacePropertyNameAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplacePropertyNameAssembly.Name = "TextBoxFindReplacePropertyNameAssembly"
        Me.TextBoxFindReplacePropertyNameAssembly.Size = New System.Drawing.Size(100, 22)
        Me.TextBoxFindReplacePropertyNameAssembly.TabIndex = 15
        '
        'ComboBoxFindReplacePropertySetAssembly
        '
        Me.ComboBoxFindReplacePropertySetAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxFindReplacePropertySetAssembly.FormattingEnabled = True
        Me.ComboBoxFindReplacePropertySetAssembly.Location = New System.Drawing.Point(8, 595)
        Me.ComboBoxFindReplacePropertySetAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxFindReplacePropertySetAssembly.Name = "ComboBoxFindReplacePropertySetAssembly"
        Me.ComboBoxFindReplacePropertySetAssembly.Size = New System.Drawing.Size(80, 23)
        Me.ComboBoxFindReplacePropertySetAssembly.TabIndex = 14
        '
        'LabelFindReplaceReplaceAssembly
        '
        Me.LabelFindReplaceReplaceAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceReplaceAssembly.AutoSize = True
        Me.LabelFindReplaceReplaceAssembly.Location = New System.Drawing.Point(350, 549)
        Me.LabelFindReplaceReplaceAssembly.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceReplaceAssembly.Name = "LabelFindReplaceReplaceAssembly"
        Me.LabelFindReplaceReplaceAssembly.Size = New System.Drawing.Size(45, 15)
        Me.LabelFindReplaceReplaceAssembly.TabIndex = 13
        Me.LabelFindReplaceReplaceAssembly.Text = "Replace"
        '
        'LabelFindReplaceFindAssembly
        '
        Me.LabelFindReplaceFindAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceFindAssembly.AutoSize = True
        Me.LabelFindReplaceFindAssembly.Location = New System.Drawing.Point(215, 549)
        Me.LabelFindReplaceFindAssembly.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceFindAssembly.Name = "LabelFindReplaceFindAssembly"
        Me.LabelFindReplaceFindAssembly.Size = New System.Drawing.Size(27, 15)
        Me.LabelFindReplaceFindAssembly.TabIndex = 12
        Me.LabelFindReplaceFindAssembly.Text = "Find"
        '
        'LabelFindReplacePropertyNameAssembly
        '
        Me.LabelFindReplacePropertyNameAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertyNameAssembly.AutoSize = True
        Me.LabelFindReplacePropertyNameAssembly.Location = New System.Drawing.Point(100, 572)
        Me.LabelFindReplacePropertyNameAssembly.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertyNameAssembly.Name = "LabelFindReplacePropertyNameAssembly"
        Me.LabelFindReplacePropertyNameAssembly.Size = New System.Drawing.Size(78, 15)
        Me.LabelFindReplacePropertyNameAssembly.TabIndex = 11
        Me.LabelFindReplacePropertyNameAssembly.Text = "Property name"
        '
        'LabelFindReplacePropertySetAssembly
        '
        Me.LabelFindReplacePropertySetAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertySetAssembly.AutoSize = True
        Me.LabelFindReplacePropertySetAssembly.Location = New System.Drawing.Point(8, 572)
        Me.LabelFindReplacePropertySetAssembly.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertySetAssembly.Name = "LabelFindReplacePropertySetAssembly"
        Me.LabelFindReplacePropertySetAssembly.Size = New System.Drawing.Size(68, 15)
        Me.LabelFindReplacePropertySetAssembly.TabIndex = 10
        Me.LabelFindReplacePropertySetAssembly.Text = "Property Set"
        '
        'ButtonExternalProgramAssembly
        '
        Me.ButtonExternalProgramAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalProgramAssembly.Location = New System.Drawing.Point(400, 515)
        Me.ButtonExternalProgramAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonExternalProgramAssembly.Name = "ButtonExternalProgramAssembly"
        Me.ButtonExternalProgramAssembly.Size = New System.Drawing.Size(75, 22)
        Me.ButtonExternalProgramAssembly.TabIndex = 9
        Me.ButtonExternalProgramAssembly.Text = "Browse"
        Me.ButtonExternalProgramAssembly.UseVisualStyleBackColor = True
        '
        'TextBoxExternalProgramAssembly
        '
        Me.TextBoxExternalProgramAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalProgramAssembly.Location = New System.Drawing.Point(8, 515)
        Me.TextBoxExternalProgramAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxExternalProgramAssembly.Name = "TextBoxExternalProgramAssembly"
        Me.TextBoxExternalProgramAssembly.Size = New System.Drawing.Size(385, 22)
        Me.TextBoxExternalProgramAssembly.TabIndex = 8
        '
        'LabelExternalProgramAssembly
        '
        Me.LabelExternalProgramAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExternalProgramAssembly.AutoSize = True
        Me.LabelExternalProgramAssembly.Location = New System.Drawing.Point(8, 492)
        Me.LabelExternalProgramAssembly.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelExternalProgramAssembly.Name = "LabelExternalProgramAssembly"
        Me.LabelExternalProgramAssembly.Size = New System.Drawing.Size(88, 15)
        Me.LabelExternalProgramAssembly.TabIndex = 7
        Me.LabelExternalProgramAssembly.Text = "External program"
        '
        'ComboBoxSaveAsAssemblyFileType
        '
        Me.ComboBoxSaveAsAssemblyFileType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxSaveAsAssemblyFileType.FormattingEnabled = True
        Me.ComboBoxSaveAsAssemblyFileType.Items.AddRange(New Object() {"fake_choice_1", "fake_choice_2"})
        Me.ComboBoxSaveAsAssemblyFileType.Location = New System.Drawing.Point(150, 372)
        Me.ComboBoxSaveAsAssemblyFileType.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxSaveAsAssemblyFileType.Name = "ComboBoxSaveAsAssemblyFileType"
        Me.ComboBoxSaveAsAssemblyFileType.Size = New System.Drawing.Size(132, 23)
        Me.ComboBoxSaveAsAssemblyFileType.Sorted = True
        Me.ComboBoxSaveAsAssemblyFileType.TabIndex = 6
        '
        'CheckBoxSaveAsAssemblyOutputDirectory
        '
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsAssemblyOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Location = New System.Drawing.Point(319, 377)
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Name = "CheckBoxSaveAsAssemblyOutputDirectory"
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Size = New System.Drawing.Size(152, 19)
        Me.CheckBoxSaveAsAssemblyOutputDirectory.TabIndex = 5
        Me.CheckBoxSaveAsAssemblyOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsAssemblyOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsAssemblyOutputDirectory
        '
        Me.ButtonSaveAsAssemblyOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsAssemblyOutputDirectory.Location = New System.Drawing.Point(400, 399)
        Me.ButtonSaveAsAssemblyOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSaveAsAssemblyOutputDirectory.Name = "ButtonSaveAsAssemblyOutputDirectory"
        Me.ButtonSaveAsAssemblyOutputDirectory.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSaveAsAssemblyOutputDirectory.TabIndex = 4
        Me.ButtonSaveAsAssemblyOutputDirectory.Text = "Browse"
        Me.ButtonSaveAsAssemblyOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsAssemblyOutputDirectory
        '
        Me.TextBoxSaveAsAssemblyOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsAssemblyOutputDirectory.Location = New System.Drawing.Point(8, 399)
        Me.TextBoxSaveAsAssemblyOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxSaveAsAssemblyOutputDirectory.Name = "TextBoxSaveAsAssemblyOutputDirectory"
        Me.TextBoxSaveAsAssemblyOutputDirectory.Size = New System.Drawing.Size(385, 22)
        Me.TextBoxSaveAsAssemblyOutputDirectory.TabIndex = 3
        '
        'LabelSaveAsAssembly
        '
        Me.LabelSaveAsAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelSaveAsAssembly.AutoSize = True
        Me.LabelSaveAsAssembly.Location = New System.Drawing.Point(8, 376)
        Me.LabelSaveAsAssembly.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelSaveAsAssembly.Name = "LabelSaveAsAssembly"
        Me.LabelSaveAsAssembly.Size = New System.Drawing.Size(127, 15)
        Me.LabelSaveAsAssembly.TabIndex = 2
        Me.LabelSaveAsAssembly.Text = "Save As output directory"
        '
        'LabelAssemblyTabNote
        '
        Me.LabelAssemblyTabNote.AutoSize = True
        Me.LabelAssemblyTabNote.Location = New System.Drawing.Point(19, 9)
        Me.LabelAssemblyTabNote.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelAssemblyTabNote.Name = "LabelAssemblyTabNote"
        Me.LabelAssemblyTabNote.Size = New System.Drawing.Size(327, 15)
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
        Me.CheckedListBoxAssembly.Location = New System.Drawing.Point(19, 32)
        Me.CheckedListBoxAssembly.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckedListBoxAssembly.Name = "CheckedListBoxAssembly"
        Me.CheckedListBoxAssembly.Size = New System.Drawing.Size(451, 310)
        Me.CheckedListBoxAssembly.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxAssembly, "Double-click to check/uncheck all")
        '
        'TabPagePart
        '
        Me.TabPagePart.BackColor = System.Drawing.SystemColors.Control
        Me.TabPagePart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPagePart.Controls.Add(Me.ButtonVariableEditPart)
        Me.TabPagePart.Controls.Add(Me.TextBoxVariableEditPart)
        Me.TabPagePart.Controls.Add(Me.CheckBoxFindReplaceReplaceRXPart)
        Me.TabPagePart.Controls.Add(Me.CheckBoxFindReplaceReplacePTPart)
        Me.TabPagePart.Controls.Add(Me.CheckBoxFindReplaceFindRXPart)
        Me.TabPagePart.Controls.Add(Me.CheckBoxFindReplaceFindWCPart)
        Me.TabPagePart.Controls.Add(Me.CheckBoxFindReplaceFindPTPart)
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
        Me.TabPagePart.Location = New System.Drawing.Point(4, 24)
        Me.TabPagePart.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPagePart.Name = "TabPagePart"
        Me.TabPagePart.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPagePart.Size = New System.Drawing.Size(511, 690)
        Me.TabPagePart.TabIndex = 2
        Me.TabPagePart.Text = "Part"
        '
        'ButtonVariableEditPart
        '
        Me.ButtonVariableEditPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonVariableEditPart.Location = New System.Drawing.Point(400, 650)
        Me.ButtonVariableEditPart.Name = "ButtonVariableEditPart"
        Me.ButtonVariableEditPart.Size = New System.Drawing.Size(75, 23)
        Me.ButtonVariableEditPart.TabIndex = 36
        Me.ButtonVariableEditPart.Text = "Edit"
        Me.ButtonVariableEditPart.UseVisualStyleBackColor = True
        '
        'TextBoxVariableEditPart
        '
        Me.TextBoxVariableEditPart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxVariableEditPart.Location = New System.Drawing.Point(8, 650)
        Me.TextBoxVariableEditPart.Name = "TextBoxVariableEditPart"
        Me.TextBoxVariableEditPart.Size = New System.Drawing.Size(385, 22)
        Me.TextBoxVariableEditPart.TabIndex = 35
        '
        'CheckBoxFindReplaceReplaceRXPart
        '
        Me.CheckBoxFindReplaceReplaceRXPart.AutoSize = True
        Me.CheckBoxFindReplaceReplaceRXPart.Location = New System.Drawing.Point(390, 572)
        Me.CheckBoxFindReplaceReplaceRXPart.Name = "CheckBoxFindReplaceReplaceRXPart"
        Me.CheckBoxFindReplaceReplaceRXPart.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceReplaceRXPart.TabIndex = 34
        Me.CheckBoxFindReplaceReplaceRXPart.Text = "RX"
        Me.CheckBoxFindReplaceReplaceRXPart.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceReplacePTPart
        '
        Me.CheckBoxFindReplaceReplacePTPart.AutoSize = True
        Me.CheckBoxFindReplaceReplacePTPart.Location = New System.Drawing.Point(350, 572)
        Me.CheckBoxFindReplaceReplacePTPart.Name = "CheckBoxFindReplaceReplacePTPart"
        Me.CheckBoxFindReplaceReplacePTPart.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceReplacePTPart.TabIndex = 33
        Me.CheckBoxFindReplaceReplacePTPart.Text = "PT"
        Me.CheckBoxFindReplaceReplacePTPart.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceFindRXPart
        '
        Me.CheckBoxFindReplaceFindRXPart.AutoSize = True
        Me.CheckBoxFindReplaceFindRXPart.Location = New System.Drawing.Point(300, 572)
        Me.CheckBoxFindReplaceFindRXPart.Name = "CheckBoxFindReplaceFindRXPart"
        Me.CheckBoxFindReplaceFindRXPart.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceFindRXPart.TabIndex = 32
        Me.CheckBoxFindReplaceFindRXPart.Text = "RX"
        Me.CheckBoxFindReplaceFindRXPart.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceFindWCPart
        '
        Me.CheckBoxFindReplaceFindWCPart.AutoSize = True
        Me.CheckBoxFindReplaceFindWCPart.Location = New System.Drawing.Point(255, 572)
        Me.CheckBoxFindReplaceFindWCPart.Name = "CheckBoxFindReplaceFindWCPart"
        Me.CheckBoxFindReplaceFindWCPart.Size = New System.Drawing.Size(44, 19)
        Me.CheckBoxFindReplaceFindWCPart.TabIndex = 31
        Me.CheckBoxFindReplaceFindWCPart.Text = "WC"
        Me.CheckBoxFindReplaceFindWCPart.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceFindPTPart
        '
        Me.CheckBoxFindReplaceFindPTPart.AutoSize = True
        Me.CheckBoxFindReplaceFindPTPart.Location = New System.Drawing.Point(215, 572)
        Me.CheckBoxFindReplaceFindPTPart.Name = "CheckBoxFindReplaceFindPTPart"
        Me.CheckBoxFindReplaceFindPTPart.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceFindPTPart.TabIndex = 30
        Me.CheckBoxFindReplaceFindPTPart.Text = "PT"
        Me.CheckBoxFindReplaceFindPTPart.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsFormulaPart
        '
        Me.TextBoxSaveAsFormulaPart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsFormulaPart.Location = New System.Drawing.Point(8, 457)
        Me.TextBoxSaveAsFormulaPart.Name = "TextBoxSaveAsFormulaPart"
        Me.TextBoxSaveAsFormulaPart.Size = New System.Drawing.Size(462, 22)
        Me.TextBoxSaveAsFormulaPart.TabIndex = 29
        '
        'CheckBoxSaveAsFormulaPart
        '
        Me.CheckBoxSaveAsFormulaPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsFormulaPart.AutoSize = True
        Me.CheckBoxSaveAsFormulaPart.Location = New System.Drawing.Point(8, 435)
        Me.CheckBoxSaveAsFormulaPart.Name = "CheckBoxSaveAsFormulaPart"
        Me.CheckBoxSaveAsFormulaPart.Size = New System.Drawing.Size(148, 19)
        Me.CheckBoxSaveAsFormulaPart.TabIndex = 28
        Me.CheckBoxSaveAsFormulaPart.Text = "Use subdirectory formula"
        Me.CheckBoxSaveAsFormulaPart.UseVisualStyleBackColor = True
        '
        'TextBoxExposeVariablesPart
        '
        Me.TextBoxExposeVariablesPart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExposeVariablesPart.Location = New System.Drawing.Point(8, 675)
        Me.TextBoxExposeVariablesPart.Name = "TextBoxExposeVariablesPart"
        Me.TextBoxExposeVariablesPart.Size = New System.Drawing.Size(467, 22)
        Me.TextBoxExposeVariablesPart.TabIndex = 27
        '
        'LabelExposeVariablesPart
        '
        Me.LabelExposeVariablesPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExposeVariablesPart.AutoSize = True
        Me.LabelExposeVariablesPart.Location = New System.Drawing.Point(8, 630)
        Me.LabelExposeVariablesPart.Name = "LabelExposeVariablesPart"
        Me.LabelExposeVariablesPart.Size = New System.Drawing.Size(100, 15)
        Me.LabelExposeVariablesPart.TabIndex = 26
        Me.LabelExposeVariablesPart.Text = "Variables to expose"
        '
        'TextBoxFindReplaceReplacePart
        '
        Me.TextBoxFindReplaceReplacePart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceReplacePart.Location = New System.Drawing.Point(350, 595)
        Me.TextBoxFindReplaceReplacePart.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceReplacePart.Name = "TextBoxFindReplaceReplacePart"
        Me.TextBoxFindReplaceReplacePart.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceReplacePart.TabIndex = 25
        '
        'TextBoxFindReplaceFindPart
        '
        Me.TextBoxFindReplaceFindPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceFindPart.Location = New System.Drawing.Point(215, 595)
        Me.TextBoxFindReplaceFindPart.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceFindPart.Name = "TextBoxFindReplaceFindPart"
        Me.TextBoxFindReplaceFindPart.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceFindPart.TabIndex = 24
        '
        'TextBoxFindReplacePropertyNamePart
        '
        Me.TextBoxFindReplacePropertyNamePart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplacePropertyNamePart.Location = New System.Drawing.Point(100, 595)
        Me.TextBoxFindReplacePropertyNamePart.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplacePropertyNamePart.Name = "TextBoxFindReplacePropertyNamePart"
        Me.TextBoxFindReplacePropertyNamePart.Size = New System.Drawing.Size(100, 22)
        Me.TextBoxFindReplacePropertyNamePart.TabIndex = 23
        '
        'ComboBoxFindReplacePropertySetPart
        '
        Me.ComboBoxFindReplacePropertySetPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxFindReplacePropertySetPart.FormattingEnabled = True
        Me.ComboBoxFindReplacePropertySetPart.Location = New System.Drawing.Point(8, 595)
        Me.ComboBoxFindReplacePropertySetPart.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxFindReplacePropertySetPart.Name = "ComboBoxFindReplacePropertySetPart"
        Me.ComboBoxFindReplacePropertySetPart.Size = New System.Drawing.Size(80, 23)
        Me.ComboBoxFindReplacePropertySetPart.TabIndex = 22
        '
        'LabelFindReplaceReplacePart
        '
        Me.LabelFindReplaceReplacePart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceReplacePart.AutoSize = True
        Me.LabelFindReplaceReplacePart.Location = New System.Drawing.Point(350, 549)
        Me.LabelFindReplaceReplacePart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceReplacePart.Name = "LabelFindReplaceReplacePart"
        Me.LabelFindReplaceReplacePart.Size = New System.Drawing.Size(45, 15)
        Me.LabelFindReplaceReplacePart.TabIndex = 21
        Me.LabelFindReplaceReplacePart.Text = "Replace"
        '
        'LabelFindReplaceFindPart
        '
        Me.LabelFindReplaceFindPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceFindPart.AutoSize = True
        Me.LabelFindReplaceFindPart.Location = New System.Drawing.Point(215, 549)
        Me.LabelFindReplaceFindPart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceFindPart.Name = "LabelFindReplaceFindPart"
        Me.LabelFindReplaceFindPart.Size = New System.Drawing.Size(27, 15)
        Me.LabelFindReplaceFindPart.TabIndex = 20
        Me.LabelFindReplaceFindPart.Text = "Find"
        '
        'LabelFindReplacePropertyNamePart
        '
        Me.LabelFindReplacePropertyNamePart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertyNamePart.AutoSize = True
        Me.LabelFindReplacePropertyNamePart.Location = New System.Drawing.Point(100, 572)
        Me.LabelFindReplacePropertyNamePart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertyNamePart.Name = "LabelFindReplacePropertyNamePart"
        Me.LabelFindReplacePropertyNamePart.Size = New System.Drawing.Size(78, 15)
        Me.LabelFindReplacePropertyNamePart.TabIndex = 19
        Me.LabelFindReplacePropertyNamePart.Text = "Property name"
        '
        'LabelFindReplacePropertySetPart
        '
        Me.LabelFindReplacePropertySetPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertySetPart.AutoSize = True
        Me.LabelFindReplacePropertySetPart.Location = New System.Drawing.Point(8, 572)
        Me.LabelFindReplacePropertySetPart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertySetPart.Name = "LabelFindReplacePropertySetPart"
        Me.LabelFindReplacePropertySetPart.Size = New System.Drawing.Size(68, 15)
        Me.LabelFindReplacePropertySetPart.TabIndex = 18
        Me.LabelFindReplacePropertySetPart.Text = "Property Set"
        '
        'ButtonExternalProgramPart
        '
        Me.ButtonExternalProgramPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalProgramPart.Location = New System.Drawing.Point(400, 515)
        Me.ButtonExternalProgramPart.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonExternalProgramPart.Name = "ButtonExternalProgramPart"
        Me.ButtonExternalProgramPart.Size = New System.Drawing.Size(75, 22)
        Me.ButtonExternalProgramPart.TabIndex = 10
        Me.ButtonExternalProgramPart.Text = "Browse"
        Me.ButtonExternalProgramPart.UseVisualStyleBackColor = True
        '
        'TextBoxExternalProgramPart
        '
        Me.TextBoxExternalProgramPart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalProgramPart.Location = New System.Drawing.Point(8, 515)
        Me.TextBoxExternalProgramPart.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxExternalProgramPart.Name = "TextBoxExternalProgramPart"
        Me.TextBoxExternalProgramPart.Size = New System.Drawing.Size(384, 22)
        Me.TextBoxExternalProgramPart.TabIndex = 9
        '
        'LabelExternalProgramPart
        '
        Me.LabelExternalProgramPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExternalProgramPart.AutoSize = True
        Me.LabelExternalProgramPart.Location = New System.Drawing.Point(8, 492)
        Me.LabelExternalProgramPart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelExternalProgramPart.Name = "LabelExternalProgramPart"
        Me.LabelExternalProgramPart.Size = New System.Drawing.Size(88, 15)
        Me.LabelExternalProgramPart.TabIndex = 8
        Me.LabelExternalProgramPart.Text = "External program"
        '
        'ComboBoxSaveAsPartFileType
        '
        Me.ComboBoxSaveAsPartFileType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxSaveAsPartFileType.FormattingEnabled = True
        Me.ComboBoxSaveAsPartFileType.Items.AddRange(New Object() {"fake_item_1"})
        Me.ComboBoxSaveAsPartFileType.Location = New System.Drawing.Point(150, 372)
        Me.ComboBoxSaveAsPartFileType.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxSaveAsPartFileType.Name = "ComboBoxSaveAsPartFileType"
        Me.ComboBoxSaveAsPartFileType.Size = New System.Drawing.Size(132, 23)
        Me.ComboBoxSaveAsPartFileType.Sorted = True
        Me.ComboBoxSaveAsPartFileType.TabIndex = 7
        '
        'CheckBoxSaveAsPartOutputDirectory
        '
        Me.CheckBoxSaveAsPartOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsPartOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsPartOutputDirectory.Location = New System.Drawing.Point(319, 377)
        Me.CheckBoxSaveAsPartOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxSaveAsPartOutputDirectory.Name = "CheckBoxSaveAsPartOutputDirectory"
        Me.CheckBoxSaveAsPartOutputDirectory.Size = New System.Drawing.Size(152, 19)
        Me.CheckBoxSaveAsPartOutputDirectory.TabIndex = 6
        Me.CheckBoxSaveAsPartOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsPartOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsPartOutputDirectory
        '
        Me.ButtonSaveAsPartOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsPartOutputDirectory.Location = New System.Drawing.Point(400, 400)
        Me.ButtonSaveAsPartOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSaveAsPartOutputDirectory.Name = "ButtonSaveAsPartOutputDirectory"
        Me.ButtonSaveAsPartOutputDirectory.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSaveAsPartOutputDirectory.TabIndex = 5
        Me.ButtonSaveAsPartOutputDirectory.Text = "Browse"
        Me.ButtonSaveAsPartOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsPartOutputDirectory
        '
        Me.TextBoxSaveAsPartOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsPartOutputDirectory.Location = New System.Drawing.Point(8, 400)
        Me.TextBoxSaveAsPartOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxSaveAsPartOutputDirectory.Name = "TextBoxSaveAsPartOutputDirectory"
        Me.TextBoxSaveAsPartOutputDirectory.Size = New System.Drawing.Size(385, 22)
        Me.TextBoxSaveAsPartOutputDirectory.TabIndex = 4
        '
        'LabelSaveAsPart
        '
        Me.LabelSaveAsPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelSaveAsPart.AutoSize = True
        Me.LabelSaveAsPart.Location = New System.Drawing.Point(8, 376)
        Me.LabelSaveAsPart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelSaveAsPart.Name = "LabelSaveAsPart"
        Me.LabelSaveAsPart.Size = New System.Drawing.Size(127, 15)
        Me.LabelSaveAsPart.TabIndex = 3
        Me.LabelSaveAsPart.Text = "Save As output directory"
        '
        'LabelPartTabNote
        '
        Me.LabelPartTabNote.AutoSize = True
        Me.LabelPartTabNote.Location = New System.Drawing.Point(19, 9)
        Me.LabelPartTabNote.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPartTabNote.Name = "LabelPartTabNote"
        Me.LabelPartTabNote.Size = New System.Drawing.Size(327, 15)
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
        Me.CheckedListBoxPart.Location = New System.Drawing.Point(19, 32)
        Me.CheckedListBoxPart.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckedListBoxPart.Name = "CheckedListBoxPart"
        Me.CheckedListBoxPart.Size = New System.Drawing.Size(451, 310)
        Me.CheckedListBoxPart.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxPart, "Double-click to check/uncheck all")
        '
        'TabPageSheetmetal
        '
        Me.TabPageSheetmetal.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageSheetmetal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageSheetmetal.Controls.Add(Me.CheckBoxFindReplaceReplaceRXSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.CheckBoxFindReplaceReplacePTSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.CheckBoxFindReplaceFindRXSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.CheckBoxFindReplaceFindWCSheetmetal)
        Me.TabPageSheetmetal.Controls.Add(Me.CheckBoxFindReplaceFindPTSheetmetal)
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
        Me.TabPageSheetmetal.Location = New System.Drawing.Point(4, 24)
        Me.TabPageSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageSheetmetal.Name = "TabPageSheetmetal"
        Me.TabPageSheetmetal.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageSheetmetal.Size = New System.Drawing.Size(511, 690)
        Me.TabPageSheetmetal.TabIndex = 3
        Me.TabPageSheetmetal.Text = "Sheetmetal"
        '
        'CheckBoxFindReplaceReplaceRXSheetmetal
        '
        Me.CheckBoxFindReplaceReplaceRXSheetmetal.AutoSize = True
        Me.CheckBoxFindReplaceReplaceRXSheetmetal.Location = New System.Drawing.Point(390, 572)
        Me.CheckBoxFindReplaceReplaceRXSheetmetal.Name = "CheckBoxFindReplaceReplaceRXSheetmetal"
        Me.CheckBoxFindReplaceReplaceRXSheetmetal.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceReplaceRXSheetmetal.TabIndex = 43
        Me.CheckBoxFindReplaceReplaceRXSheetmetal.Text = "RX"
        Me.CheckBoxFindReplaceReplaceRXSheetmetal.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceReplacePTSheetmetal
        '
        Me.CheckBoxFindReplaceReplacePTSheetmetal.AutoSize = True
        Me.CheckBoxFindReplaceReplacePTSheetmetal.Location = New System.Drawing.Point(350, 572)
        Me.CheckBoxFindReplaceReplacePTSheetmetal.Name = "CheckBoxFindReplaceReplacePTSheetmetal"
        Me.CheckBoxFindReplaceReplacePTSheetmetal.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceReplacePTSheetmetal.TabIndex = 42
        Me.CheckBoxFindReplaceReplacePTSheetmetal.Text = "PT"
        Me.CheckBoxFindReplaceReplacePTSheetmetal.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceFindRXSheetmetal
        '
        Me.CheckBoxFindReplaceFindRXSheetmetal.AutoSize = True
        Me.CheckBoxFindReplaceFindRXSheetmetal.Location = New System.Drawing.Point(300, 572)
        Me.CheckBoxFindReplaceFindRXSheetmetal.Name = "CheckBoxFindReplaceFindRXSheetmetal"
        Me.CheckBoxFindReplaceFindRXSheetmetal.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceFindRXSheetmetal.TabIndex = 40
        Me.CheckBoxFindReplaceFindRXSheetmetal.Text = "RX"
        Me.CheckBoxFindReplaceFindRXSheetmetal.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceFindWCSheetmetal
        '
        Me.CheckBoxFindReplaceFindWCSheetmetal.AutoSize = True
        Me.CheckBoxFindReplaceFindWCSheetmetal.Location = New System.Drawing.Point(255, 572)
        Me.CheckBoxFindReplaceFindWCSheetmetal.Name = "CheckBoxFindReplaceFindWCSheetmetal"
        Me.CheckBoxFindReplaceFindWCSheetmetal.Size = New System.Drawing.Size(44, 19)
        Me.CheckBoxFindReplaceFindWCSheetmetal.TabIndex = 39
        Me.CheckBoxFindReplaceFindWCSheetmetal.Text = "WC"
        Me.CheckBoxFindReplaceFindWCSheetmetal.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceFindPTSheetmetal
        '
        Me.CheckBoxFindReplaceFindPTSheetmetal.AutoSize = True
        Me.CheckBoxFindReplaceFindPTSheetmetal.Location = New System.Drawing.Point(215, 572)
        Me.CheckBoxFindReplaceFindPTSheetmetal.Name = "CheckBoxFindReplaceFindPTSheetmetal"
        Me.CheckBoxFindReplaceFindPTSheetmetal.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceFindPTSheetmetal.TabIndex = 38
        Me.CheckBoxFindReplaceFindPTSheetmetal.Text = "PT"
        Me.CheckBoxFindReplaceFindPTSheetmetal.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsFormulaSheetmetal
        '
        Me.TextBoxSaveAsFormulaSheetmetal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsFormulaSheetmetal.Location = New System.Drawing.Point(8, 457)
        Me.TextBoxSaveAsFormulaSheetmetal.Name = "TextBoxSaveAsFormulaSheetmetal"
        Me.TextBoxSaveAsFormulaSheetmetal.Size = New System.Drawing.Size(462, 22)
        Me.TextBoxSaveAsFormulaSheetmetal.TabIndex = 37
        '
        'CheckBoxSaveAsFormulaSheetmetal
        '
        Me.CheckBoxSaveAsFormulaSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsFormulaSheetmetal.AutoSize = True
        Me.CheckBoxSaveAsFormulaSheetmetal.Location = New System.Drawing.Point(8, 435)
        Me.CheckBoxSaveAsFormulaSheetmetal.Name = "CheckBoxSaveAsFormulaSheetmetal"
        Me.CheckBoxSaveAsFormulaSheetmetal.Size = New System.Drawing.Size(148, 19)
        Me.CheckBoxSaveAsFormulaSheetmetal.TabIndex = 36
        Me.CheckBoxSaveAsFormulaSheetmetal.Text = "Use subdirectory formula"
        Me.CheckBoxSaveAsFormulaSheetmetal.UseVisualStyleBackColor = True
        '
        'TextBoxExposeVariablesSheetmetal
        '
        Me.TextBoxExposeVariablesSheetmetal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExposeVariablesSheetmetal.Location = New System.Drawing.Point(8, 653)
        Me.TextBoxExposeVariablesSheetmetal.Name = "TextBoxExposeVariablesSheetmetal"
        Me.TextBoxExposeVariablesSheetmetal.Size = New System.Drawing.Size(467, 22)
        Me.TextBoxExposeVariablesSheetmetal.TabIndex = 35
        '
        'LabelExposeVariablesSheetmetal
        '
        Me.LabelExposeVariablesSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExposeVariablesSheetmetal.AutoSize = True
        Me.LabelExposeVariablesSheetmetal.Location = New System.Drawing.Point(8, 630)
        Me.LabelExposeVariablesSheetmetal.Name = "LabelExposeVariablesSheetmetal"
        Me.LabelExposeVariablesSheetmetal.Size = New System.Drawing.Size(100, 15)
        Me.LabelExposeVariablesSheetmetal.TabIndex = 34
        Me.LabelExposeVariablesSheetmetal.Text = "Variables to expose"
        '
        'TextBoxFindReplaceReplaceSheetmetal
        '
        Me.TextBoxFindReplaceReplaceSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceReplaceSheetmetal.Location = New System.Drawing.Point(350, 595)
        Me.TextBoxFindReplaceReplaceSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceReplaceSheetmetal.Name = "TextBoxFindReplaceReplaceSheetmetal"
        Me.TextBoxFindReplaceReplaceSheetmetal.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceReplaceSheetmetal.TabIndex = 33
        '
        'TextBoxFindReplaceFindSheetmetal
        '
        Me.TextBoxFindReplaceFindSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceFindSheetmetal.Location = New System.Drawing.Point(215, 595)
        Me.TextBoxFindReplaceFindSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceFindSheetmetal.Name = "TextBoxFindReplaceFindSheetmetal"
        Me.TextBoxFindReplaceFindSheetmetal.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceFindSheetmetal.TabIndex = 32
        '
        'TextBoxFindReplacePropertyNameSheetmetal
        '
        Me.TextBoxFindReplacePropertyNameSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplacePropertyNameSheetmetal.Location = New System.Drawing.Point(100, 595)
        Me.TextBoxFindReplacePropertyNameSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplacePropertyNameSheetmetal.Name = "TextBoxFindReplacePropertyNameSheetmetal"
        Me.TextBoxFindReplacePropertyNameSheetmetal.Size = New System.Drawing.Size(100, 22)
        Me.TextBoxFindReplacePropertyNameSheetmetal.TabIndex = 31
        '
        'ComboBoxFindReplacePropertySetSheetmetal
        '
        Me.ComboBoxFindReplacePropertySetSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxFindReplacePropertySetSheetmetal.FormattingEnabled = True
        Me.ComboBoxFindReplacePropertySetSheetmetal.Location = New System.Drawing.Point(8, 595)
        Me.ComboBoxFindReplacePropertySetSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxFindReplacePropertySetSheetmetal.Name = "ComboBoxFindReplacePropertySetSheetmetal"
        Me.ComboBoxFindReplacePropertySetSheetmetal.Size = New System.Drawing.Size(80, 23)
        Me.ComboBoxFindReplacePropertySetSheetmetal.TabIndex = 30
        '
        'LabelFindReplaceReplaceSheetmetal
        '
        Me.LabelFindReplaceReplaceSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceReplaceSheetmetal.AutoSize = True
        Me.LabelFindReplaceReplaceSheetmetal.Location = New System.Drawing.Point(350, 549)
        Me.LabelFindReplaceReplaceSheetmetal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceReplaceSheetmetal.Name = "LabelFindReplaceReplaceSheetmetal"
        Me.LabelFindReplaceReplaceSheetmetal.Size = New System.Drawing.Size(45, 15)
        Me.LabelFindReplaceReplaceSheetmetal.TabIndex = 29
        Me.LabelFindReplaceReplaceSheetmetal.Text = "Replace"
        '
        'LabelFindReplaceFindSheetmetal
        '
        Me.LabelFindReplaceFindSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceFindSheetmetal.AutoSize = True
        Me.LabelFindReplaceFindSheetmetal.Location = New System.Drawing.Point(215, 549)
        Me.LabelFindReplaceFindSheetmetal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceFindSheetmetal.Name = "LabelFindReplaceFindSheetmetal"
        Me.LabelFindReplaceFindSheetmetal.Size = New System.Drawing.Size(27, 15)
        Me.LabelFindReplaceFindSheetmetal.TabIndex = 28
        Me.LabelFindReplaceFindSheetmetal.Text = "Find"
        '
        'LabelFindReplacePropertyNameSheetmetal
        '
        Me.LabelFindReplacePropertyNameSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertyNameSheetmetal.AutoSize = True
        Me.LabelFindReplacePropertyNameSheetmetal.Location = New System.Drawing.Point(100, 572)
        Me.LabelFindReplacePropertyNameSheetmetal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertyNameSheetmetal.Name = "LabelFindReplacePropertyNameSheetmetal"
        Me.LabelFindReplacePropertyNameSheetmetal.Size = New System.Drawing.Size(78, 15)
        Me.LabelFindReplacePropertyNameSheetmetal.TabIndex = 27
        Me.LabelFindReplacePropertyNameSheetmetal.Text = "Property name"
        '
        'LabelFindReplacePropertySetSheetmetal
        '
        Me.LabelFindReplacePropertySetSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertySetSheetmetal.AutoSize = True
        Me.LabelFindReplacePropertySetSheetmetal.Location = New System.Drawing.Point(8, 572)
        Me.LabelFindReplacePropertySetSheetmetal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertySetSheetmetal.Name = "LabelFindReplacePropertySetSheetmetal"
        Me.LabelFindReplacePropertySetSheetmetal.Size = New System.Drawing.Size(68, 15)
        Me.LabelFindReplacePropertySetSheetmetal.TabIndex = 26
        Me.LabelFindReplacePropertySetSheetmetal.Text = "Property Set"
        '
        'ButtonExternalProgramSheetmetal
        '
        Me.ButtonExternalProgramSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalProgramSheetmetal.Location = New System.Drawing.Point(400, 515)
        Me.ButtonExternalProgramSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonExternalProgramSheetmetal.Name = "ButtonExternalProgramSheetmetal"
        Me.ButtonExternalProgramSheetmetal.Size = New System.Drawing.Size(75, 22)
        Me.ButtonExternalProgramSheetmetal.TabIndex = 15
        Me.ButtonExternalProgramSheetmetal.Text = "Browse"
        Me.ButtonExternalProgramSheetmetal.UseVisualStyleBackColor = True
        '
        'TextBoxExternalProgramSheetmetal
        '
        Me.TextBoxExternalProgramSheetmetal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalProgramSheetmetal.Location = New System.Drawing.Point(8, 515)
        Me.TextBoxExternalProgramSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxExternalProgramSheetmetal.Name = "TextBoxExternalProgramSheetmetal"
        Me.TextBoxExternalProgramSheetmetal.Size = New System.Drawing.Size(384, 22)
        Me.TextBoxExternalProgramSheetmetal.TabIndex = 14
        '
        'LabelExternalProgramSheetmetal
        '
        Me.LabelExternalProgramSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExternalProgramSheetmetal.AutoSize = True
        Me.LabelExternalProgramSheetmetal.Location = New System.Drawing.Point(8, 492)
        Me.LabelExternalProgramSheetmetal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelExternalProgramSheetmetal.Name = "LabelExternalProgramSheetmetal"
        Me.LabelExternalProgramSheetmetal.Size = New System.Drawing.Size(88, 15)
        Me.LabelExternalProgramSheetmetal.TabIndex = 13
        Me.LabelExternalProgramSheetmetal.Text = "External program"
        '
        'ComboBoxSaveAsSheetmetalFileType
        '
        Me.ComboBoxSaveAsSheetmetalFileType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxSaveAsSheetmetalFileType.FormattingEnabled = True
        Me.ComboBoxSaveAsSheetmetalFileType.Items.AddRange(New Object() {"fake_item_1"})
        Me.ComboBoxSaveAsSheetmetalFileType.Location = New System.Drawing.Point(150, 372)
        Me.ComboBoxSaveAsSheetmetalFileType.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxSaveAsSheetmetalFileType.Name = "ComboBoxSaveAsSheetmetalFileType"
        Me.ComboBoxSaveAsSheetmetalFileType.Size = New System.Drawing.Size(132, 23)
        Me.ComboBoxSaveAsSheetmetalFileType.Sorted = True
        Me.ComboBoxSaveAsSheetmetalFileType.TabIndex = 12
        '
        'CheckBoxSaveAsSheetmetalOutputDirectory
        '
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Location = New System.Drawing.Point(319, 377)
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Name = "CheckBoxSaveAsSheetmetalOutputDirectory"
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Size = New System.Drawing.Size(152, 19)
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.TabIndex = 11
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsSheetmetalOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsSheetmetalOutputDirectory
        '
        Me.ButtonSaveAsSheetmetalOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsSheetmetalOutputDirectory.Location = New System.Drawing.Point(400, 402)
        Me.ButtonSaveAsSheetmetalOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSaveAsSheetmetalOutputDirectory.Name = "ButtonSaveAsSheetmetalOutputDirectory"
        Me.ButtonSaveAsSheetmetalOutputDirectory.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSaveAsSheetmetalOutputDirectory.TabIndex = 9
        Me.ButtonSaveAsSheetmetalOutputDirectory.Text = "Browse"
        Me.ButtonSaveAsSheetmetalOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsSheetmetalOutputDirectory
        '
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Location = New System.Drawing.Point(8, 399)
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Name = "TextBoxSaveAsSheetmetalOutputDirectory"
        Me.TextBoxSaveAsSheetmetalOutputDirectory.Size = New System.Drawing.Size(385, 22)
        Me.TextBoxSaveAsSheetmetalOutputDirectory.TabIndex = 8
        '
        'LabelSaveAsSheetmetal
        '
        Me.LabelSaveAsSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelSaveAsSheetmetal.AutoSize = True
        Me.LabelSaveAsSheetmetal.Location = New System.Drawing.Point(8, 376)
        Me.LabelSaveAsSheetmetal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelSaveAsSheetmetal.Name = "LabelSaveAsSheetmetal"
        Me.LabelSaveAsSheetmetal.Size = New System.Drawing.Size(127, 15)
        Me.LabelSaveAsSheetmetal.TabIndex = 7
        Me.LabelSaveAsSheetmetal.Text = "Save As output directory"
        '
        'LabelSheetmetalTabNote
        '
        Me.LabelSheetmetalTabNote.AutoSize = True
        Me.LabelSheetmetalTabNote.Location = New System.Drawing.Point(19, 9)
        Me.LabelSheetmetalTabNote.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelSheetmetalTabNote.Name = "LabelSheetmetalTabNote"
        Me.LabelSheetmetalTabNote.Size = New System.Drawing.Size(327, 15)
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
        Me.CheckedListBoxSheetmetal.Location = New System.Drawing.Point(19, 32)
        Me.CheckedListBoxSheetmetal.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckedListBoxSheetmetal.Name = "CheckedListBoxSheetmetal"
        Me.CheckedListBoxSheetmetal.Size = New System.Drawing.Size(451, 310)
        Me.CheckedListBoxSheetmetal.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxSheetmetal, "Double-click to check/uncheck all")
        '
        'TabPageDraft
        '
        Me.TabPageDraft.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageDraft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageDraft.Controls.Add(Me.CheckBoxFindReplaceReplaceRXDraft)
        Me.TabPageDraft.Controls.Add(Me.CheckBoxFindReplaceReplacePTDraft)
        Me.TabPageDraft.Controls.Add(Me.CheckBoxFindReplaceFindRXDraft)
        Me.TabPageDraft.Controls.Add(Me.CheckBoxFindReplaceFindWCDraft)
        Me.TabPageDraft.Controls.Add(Me.CheckBoxFindReplaceFindPTDraft)
        Me.TabPageDraft.Controls.Add(Me.TextBoxFindReplaceReplaceDraft)
        Me.TabPageDraft.Controls.Add(Me.TextBoxFindReplaceFindDraft)
        Me.TabPageDraft.Controls.Add(Me.TextBoxFindReplacePropertyNameDraft)
        Me.TabPageDraft.Controls.Add(Me.ComboBoxFindReplacePropertySetDraft)
        Me.TabPageDraft.Controls.Add(Me.LabelFindReplaceReplaceDraft)
        Me.TabPageDraft.Controls.Add(Me.LabelFindReplaceFindDraft)
        Me.TabPageDraft.Controls.Add(Me.LabelFindReplacePropertyNameDraft)
        Me.TabPageDraft.Controls.Add(Me.LabelFindReplacePropertySetDraft)
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
        Me.TabPageDraft.Location = New System.Drawing.Point(4, 24)
        Me.TabPageDraft.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageDraft.Name = "TabPageDraft"
        Me.TabPageDraft.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageDraft.Size = New System.Drawing.Size(511, 690)
        Me.TabPageDraft.TabIndex = 4
        Me.TabPageDraft.Text = "Draft"
        '
        'CheckBoxFindReplaceReplaceRXDraft
        '
        Me.CheckBoxFindReplaceReplaceRXDraft.AutoSize = True
        Me.CheckBoxFindReplaceReplaceRXDraft.Location = New System.Drawing.Point(395, 613)
        Me.CheckBoxFindReplaceReplaceRXDraft.Name = "CheckBoxFindReplaceReplaceRXDraft"
        Me.CheckBoxFindReplaceReplaceRXDraft.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceReplaceRXDraft.TabIndex = 56
        Me.CheckBoxFindReplaceReplaceRXDraft.Text = "RX"
        Me.CheckBoxFindReplaceReplaceRXDraft.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceReplacePTDraft
        '
        Me.CheckBoxFindReplaceReplacePTDraft.AutoSize = True
        Me.CheckBoxFindReplaceReplacePTDraft.Location = New System.Drawing.Point(355, 613)
        Me.CheckBoxFindReplaceReplacePTDraft.Name = "CheckBoxFindReplaceReplacePTDraft"
        Me.CheckBoxFindReplaceReplacePTDraft.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceReplacePTDraft.TabIndex = 55
        Me.CheckBoxFindReplaceReplacePTDraft.Text = "PT"
        Me.CheckBoxFindReplaceReplacePTDraft.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceFindRXDraft
        '
        Me.CheckBoxFindReplaceFindRXDraft.AutoSize = True
        Me.CheckBoxFindReplaceFindRXDraft.Location = New System.Drawing.Point(305, 613)
        Me.CheckBoxFindReplaceFindRXDraft.Name = "CheckBoxFindReplaceFindRXDraft"
        Me.CheckBoxFindReplaceFindRXDraft.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceFindRXDraft.TabIndex = 54
        Me.CheckBoxFindReplaceFindRXDraft.Text = "RX"
        Me.CheckBoxFindReplaceFindRXDraft.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceFindWCDraft
        '
        Me.CheckBoxFindReplaceFindWCDraft.AutoSize = True
        Me.CheckBoxFindReplaceFindWCDraft.Location = New System.Drawing.Point(260, 613)
        Me.CheckBoxFindReplaceFindWCDraft.Name = "CheckBoxFindReplaceFindWCDraft"
        Me.CheckBoxFindReplaceFindWCDraft.Size = New System.Drawing.Size(44, 19)
        Me.CheckBoxFindReplaceFindWCDraft.TabIndex = 53
        Me.CheckBoxFindReplaceFindWCDraft.Text = "WC"
        Me.CheckBoxFindReplaceFindWCDraft.UseVisualStyleBackColor = True
        '
        'CheckBoxFindReplaceFindPTDraft
        '
        Me.CheckBoxFindReplaceFindPTDraft.AutoSize = True
        Me.CheckBoxFindReplaceFindPTDraft.Location = New System.Drawing.Point(220, 613)
        Me.CheckBoxFindReplaceFindPTDraft.Name = "CheckBoxFindReplaceFindPTDraft"
        Me.CheckBoxFindReplaceFindPTDraft.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxFindReplaceFindPTDraft.TabIndex = 52
        Me.CheckBoxFindReplaceFindPTDraft.Text = "PT"
        Me.CheckBoxFindReplaceFindPTDraft.UseVisualStyleBackColor = True
        '
        'TextBoxFindReplaceReplaceDraft
        '
        Me.TextBoxFindReplaceReplaceDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceReplaceDraft.Location = New System.Drawing.Point(355, 636)
        Me.TextBoxFindReplaceReplaceDraft.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceReplaceDraft.Name = "TextBoxFindReplaceReplaceDraft"
        Me.TextBoxFindReplaceReplaceDraft.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceReplaceDraft.TabIndex = 51
        '
        'TextBoxFindReplaceFindDraft
        '
        Me.TextBoxFindReplaceFindDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplaceFindDraft.Location = New System.Drawing.Point(220, 636)
        Me.TextBoxFindReplaceFindDraft.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplaceFindDraft.Name = "TextBoxFindReplaceFindDraft"
        Me.TextBoxFindReplaceFindDraft.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxFindReplaceFindDraft.TabIndex = 50
        '
        'TextBoxFindReplacePropertyNameDraft
        '
        Me.TextBoxFindReplacePropertyNameDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFindReplacePropertyNameDraft.Location = New System.Drawing.Point(105, 636)
        Me.TextBoxFindReplacePropertyNameDraft.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFindReplacePropertyNameDraft.Name = "TextBoxFindReplacePropertyNameDraft"
        Me.TextBoxFindReplacePropertyNameDraft.Size = New System.Drawing.Size(100, 22)
        Me.TextBoxFindReplacePropertyNameDraft.TabIndex = 49
        '
        'ComboBoxFindReplacePropertySetDraft
        '
        Me.ComboBoxFindReplacePropertySetDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxFindReplacePropertySetDraft.FormattingEnabled = True
        Me.ComboBoxFindReplacePropertySetDraft.Location = New System.Drawing.Point(13, 636)
        Me.ComboBoxFindReplacePropertySetDraft.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxFindReplacePropertySetDraft.Name = "ComboBoxFindReplacePropertySetDraft"
        Me.ComboBoxFindReplacePropertySetDraft.Size = New System.Drawing.Size(80, 23)
        Me.ComboBoxFindReplacePropertySetDraft.TabIndex = 48
        '
        'LabelFindReplaceReplaceDraft
        '
        Me.LabelFindReplaceReplaceDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceReplaceDraft.AutoSize = True
        Me.LabelFindReplaceReplaceDraft.Location = New System.Drawing.Point(355, 590)
        Me.LabelFindReplaceReplaceDraft.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceReplaceDraft.Name = "LabelFindReplaceReplaceDraft"
        Me.LabelFindReplaceReplaceDraft.Size = New System.Drawing.Size(45, 15)
        Me.LabelFindReplaceReplaceDraft.TabIndex = 47
        Me.LabelFindReplaceReplaceDraft.Text = "Replace"
        '
        'LabelFindReplaceFindDraft
        '
        Me.LabelFindReplaceFindDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplaceFindDraft.AutoSize = True
        Me.LabelFindReplaceFindDraft.Location = New System.Drawing.Point(220, 590)
        Me.LabelFindReplaceFindDraft.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplaceFindDraft.Name = "LabelFindReplaceFindDraft"
        Me.LabelFindReplaceFindDraft.Size = New System.Drawing.Size(27, 15)
        Me.LabelFindReplaceFindDraft.TabIndex = 46
        Me.LabelFindReplaceFindDraft.Text = "Find"
        '
        'LabelFindReplacePropertyNameDraft
        '
        Me.LabelFindReplacePropertyNameDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertyNameDraft.AutoSize = True
        Me.LabelFindReplacePropertyNameDraft.Location = New System.Drawing.Point(105, 613)
        Me.LabelFindReplacePropertyNameDraft.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertyNameDraft.Name = "LabelFindReplacePropertyNameDraft"
        Me.LabelFindReplacePropertyNameDraft.Size = New System.Drawing.Size(78, 15)
        Me.LabelFindReplacePropertyNameDraft.TabIndex = 45
        Me.LabelFindReplacePropertyNameDraft.Text = "Property name"
        '
        'LabelFindReplacePropertySetDraft
        '
        Me.LabelFindReplacePropertySetDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFindReplacePropertySetDraft.AutoSize = True
        Me.LabelFindReplacePropertySetDraft.Location = New System.Drawing.Point(13, 613)
        Me.LabelFindReplacePropertySetDraft.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFindReplacePropertySetDraft.Name = "LabelFindReplacePropertySetDraft"
        Me.LabelFindReplacePropertySetDraft.Size = New System.Drawing.Size(68, 15)
        Me.LabelFindReplacePropertySetDraft.TabIndex = 44
        Me.LabelFindReplacePropertySetDraft.Text = "Property Set"
        '
        'TextBoxSaveAsFormulaDraft
        '
        Me.TextBoxSaveAsFormulaDraft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsFormulaDraft.Location = New System.Drawing.Point(8, 443)
        Me.TextBoxSaveAsFormulaDraft.Name = "TextBoxSaveAsFormulaDraft"
        Me.TextBoxSaveAsFormulaDraft.Size = New System.Drawing.Size(467, 22)
        Me.TextBoxSaveAsFormulaDraft.TabIndex = 26
        '
        'CheckBoxSaveAsFormulaDraft
        '
        Me.CheckBoxSaveAsFormulaDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsFormulaDraft.AutoSize = True
        Me.CheckBoxSaveAsFormulaDraft.Location = New System.Drawing.Point(8, 421)
        Me.CheckBoxSaveAsFormulaDraft.Name = "CheckBoxSaveAsFormulaDraft"
        Me.CheckBoxSaveAsFormulaDraft.Size = New System.Drawing.Size(148, 19)
        Me.CheckBoxSaveAsFormulaDraft.TabIndex = 25
        Me.CheckBoxSaveAsFormulaDraft.Text = "Use subdirectory formula"
        Me.CheckBoxSaveAsFormulaDraft.UseVisualStyleBackColor = True
        '
        'ButtonWatermark
        '
        Me.ButtonWatermark.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonWatermark.Location = New System.Drawing.Point(400, 501)
        Me.ButtonWatermark.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonWatermark.Name = "ButtonWatermark"
        Me.ButtonWatermark.Size = New System.Drawing.Size(75, 23)
        Me.ButtonWatermark.TabIndex = 24
        Me.ButtonWatermark.Text = "Browse"
        Me.ButtonWatermark.UseVisualStyleBackColor = True
        '
        'TextBoxWatermarkFilename
        '
        Me.TextBoxWatermarkFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWatermarkFilename.Location = New System.Drawing.Point(8, 502)
        Me.TextBoxWatermarkFilename.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxWatermarkFilename.Name = "TextBoxWatermarkFilename"
        Me.TextBoxWatermarkFilename.Size = New System.Drawing.Size(385, 22)
        Me.TextBoxWatermarkFilename.TabIndex = 23
        '
        'TextBoxWatermarkScale
        '
        Me.TextBoxWatermarkScale.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWatermarkScale.Location = New System.Drawing.Point(297, 478)
        Me.TextBoxWatermarkScale.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxWatermarkScale.Name = "TextBoxWatermarkScale"
        Me.TextBoxWatermarkScale.Size = New System.Drawing.Size(38, 22)
        Me.TextBoxWatermarkScale.TabIndex = 22
        '
        'LabelWatermarkScale
        '
        Me.LabelWatermarkScale.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelWatermarkScale.AutoSize = True
        Me.LabelWatermarkScale.Location = New System.Drawing.Point(255, 480)
        Me.LabelWatermarkScale.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelWatermarkScale.Name = "LabelWatermarkScale"
        Me.LabelWatermarkScale.Size = New System.Drawing.Size(33, 15)
        Me.LabelWatermarkScale.TabIndex = 21
        Me.LabelWatermarkScale.Text = "Scale"
        '
        'TextBoxWatermarkY
        '
        Me.TextBoxWatermarkY.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWatermarkY.Location = New System.Drawing.Point(202, 478)
        Me.TextBoxWatermarkY.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxWatermarkY.Name = "TextBoxWatermarkY"
        Me.TextBoxWatermarkY.Size = New System.Drawing.Size(38, 22)
        Me.TextBoxWatermarkY.TabIndex = 20
        '
        'LabelWatermarkY
        '
        Me.LabelWatermarkY.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelWatermarkY.AutoSize = True
        Me.LabelWatermarkY.Location = New System.Drawing.Point(172, 480)
        Me.LabelWatermarkY.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelWatermarkY.Name = "LabelWatermarkY"
        Me.LabelWatermarkY.Size = New System.Drawing.Size(26, 15)
        Me.LabelWatermarkY.TabIndex = 19
        Me.LabelWatermarkY.Text = "Y/H"
        '
        'TextBoxWatermarkX
        '
        Me.TextBoxWatermarkX.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWatermarkX.Location = New System.Drawing.Point(120, 478)
        Me.TextBoxWatermarkX.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxWatermarkX.Name = "TextBoxWatermarkX"
        Me.TextBoxWatermarkX.Size = New System.Drawing.Size(38, 22)
        Me.TextBoxWatermarkX.TabIndex = 18
        '
        'LabelWatermarkX
        '
        Me.LabelWatermarkX.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelWatermarkX.AutoSize = True
        Me.LabelWatermarkX.Location = New System.Drawing.Point(90, 480)
        Me.LabelWatermarkX.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelWatermarkX.Name = "LabelWatermarkX"
        Me.LabelWatermarkX.Size = New System.Drawing.Size(28, 15)
        Me.LabelWatermarkX.TabIndex = 17
        Me.LabelWatermarkX.Text = "X/W"
        '
        'CheckBoxWatermark
        '
        Me.CheckBoxWatermark.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxWatermark.AutoSize = True
        Me.CheckBoxWatermark.Location = New System.Drawing.Point(8, 479)
        Me.CheckBoxWatermark.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxWatermark.Name = "CheckBoxWatermark"
        Me.CheckBoxWatermark.Size = New System.Drawing.Size(78, 19)
        Me.CheckBoxWatermark.TabIndex = 16
        Me.CheckBoxWatermark.Text = "Watermark"
        Me.CheckBoxWatermark.UseVisualStyleBackColor = True
        '
        'ButtonExternalProgramDraft
        '
        Me.ButtonExternalProgramDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalProgramDraft.Location = New System.Drawing.Point(400, 557)
        Me.ButtonExternalProgramDraft.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonExternalProgramDraft.Name = "ButtonExternalProgramDraft"
        Me.ButtonExternalProgramDraft.Size = New System.Drawing.Size(75, 22)
        Me.ButtonExternalProgramDraft.TabIndex = 15
        Me.ButtonExternalProgramDraft.Text = "Browse"
        Me.ButtonExternalProgramDraft.UseVisualStyleBackColor = True
        '
        'TextBoxExternalProgramDraft
        '
        Me.TextBoxExternalProgramDraft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalProgramDraft.Location = New System.Drawing.Point(8, 558)
        Me.TextBoxExternalProgramDraft.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxExternalProgramDraft.Name = "TextBoxExternalProgramDraft"
        Me.TextBoxExternalProgramDraft.Size = New System.Drawing.Size(384, 22)
        Me.TextBoxExternalProgramDraft.TabIndex = 14
        '
        'LabelExternalProgramDraft
        '
        Me.LabelExternalProgramDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelExternalProgramDraft.AutoSize = True
        Me.LabelExternalProgramDraft.Location = New System.Drawing.Point(8, 535)
        Me.LabelExternalProgramDraft.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelExternalProgramDraft.Name = "LabelExternalProgramDraft"
        Me.LabelExternalProgramDraft.Size = New System.Drawing.Size(88, 15)
        Me.LabelExternalProgramDraft.TabIndex = 13
        Me.LabelExternalProgramDraft.Text = "External program"
        '
        'ComboBoxSaveAsDraftFileType
        '
        Me.ComboBoxSaveAsDraftFileType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxSaveAsDraftFileType.FormattingEnabled = True
        Me.ComboBoxSaveAsDraftFileType.Items.AddRange(New Object() {"fake_item_1"})
        Me.ComboBoxSaveAsDraftFileType.Location = New System.Drawing.Point(150, 358)
        Me.ComboBoxSaveAsDraftFileType.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxSaveAsDraftFileType.Name = "ComboBoxSaveAsDraftFileType"
        Me.ComboBoxSaveAsDraftFileType.Size = New System.Drawing.Size(132, 23)
        Me.ComboBoxSaveAsDraftFileType.Sorted = True
        Me.ComboBoxSaveAsDraftFileType.TabIndex = 12
        '
        'CheckBoxSaveAsDraftOutputDirectory
        '
        Me.CheckBoxSaveAsDraftOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveAsDraftOutputDirectory.AutoSize = True
        Me.CheckBoxSaveAsDraftOutputDirectory.Location = New System.Drawing.Point(319, 363)
        Me.CheckBoxSaveAsDraftOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxSaveAsDraftOutputDirectory.Name = "CheckBoxSaveAsDraftOutputDirectory"
        Me.CheckBoxSaveAsDraftOutputDirectory.Size = New System.Drawing.Size(152, 19)
        Me.CheckBoxSaveAsDraftOutputDirectory.TabIndex = 10
        Me.CheckBoxSaveAsDraftOutputDirectory.Text = "Same as original directory"
        Me.CheckBoxSaveAsDraftOutputDirectory.UseVisualStyleBackColor = True
        '
        'ButtonSaveAsDraftOutputDirectory
        '
        Me.ButtonSaveAsDraftOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveAsDraftOutputDirectory.Location = New System.Drawing.Point(400, 387)
        Me.ButtonSaveAsDraftOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSaveAsDraftOutputDirectory.Name = "ButtonSaveAsDraftOutputDirectory"
        Me.ButtonSaveAsDraftOutputDirectory.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSaveAsDraftOutputDirectory.TabIndex = 8
        Me.ButtonSaveAsDraftOutputDirectory.Text = "Browse"
        Me.ButtonSaveAsDraftOutputDirectory.UseVisualStyleBackColor = True
        '
        'TextBoxSaveAsDraftOutputDirectory
        '
        Me.TextBoxSaveAsDraftOutputDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSaveAsDraftOutputDirectory.Location = New System.Drawing.Point(8, 387)
        Me.TextBoxSaveAsDraftOutputDirectory.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxSaveAsDraftOutputDirectory.Name = "TextBoxSaveAsDraftOutputDirectory"
        Me.TextBoxSaveAsDraftOutputDirectory.Size = New System.Drawing.Size(385, 22)
        Me.TextBoxSaveAsDraftOutputDirectory.TabIndex = 6
        '
        'LabelSaveAsDraftOutputDirectory
        '
        Me.LabelSaveAsDraftOutputDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelSaveAsDraftOutputDirectory.AutoSize = True
        Me.LabelSaveAsDraftOutputDirectory.Location = New System.Drawing.Point(8, 362)
        Me.LabelSaveAsDraftOutputDirectory.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelSaveAsDraftOutputDirectory.Name = "LabelSaveAsDraftOutputDirectory"
        Me.LabelSaveAsDraftOutputDirectory.Size = New System.Drawing.Size(127, 15)
        Me.LabelSaveAsDraftOutputDirectory.TabIndex = 4
        Me.LabelSaveAsDraftOutputDirectory.Text = "Save As output directory"
        '
        'LabelDraftTabNote
        '
        Me.LabelDraftTabNote.AutoSize = True
        Me.LabelDraftTabNote.Location = New System.Drawing.Point(19, 9)
        Me.LabelDraftTabNote.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelDraftTabNote.Name = "LabelDraftTabNote"
        Me.LabelDraftTabNote.Size = New System.Drawing.Size(327, 15)
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
        Me.CheckedListBoxDraft.Location = New System.Drawing.Point(19, 32)
        Me.CheckedListBoxDraft.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckedListBoxDraft.Name = "CheckedListBoxDraft"
        Me.CheckedListBoxDraft.Size = New System.Drawing.Size(451, 310)
        Me.CheckedListBoxDraft.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CheckedListBoxDraft, "Double-click to check/uncheck all")
        '
        'TabPageConfiguration
        '
        Me.TabPageConfiguration.AutoScroll = True
        Me.TabPageConfiguration.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageConfiguration.Controls.Add(Me.TabControl2)
        Me.TabPageConfiguration.ImageKey = "Tools"
        Me.TabPageConfiguration.Location = New System.Drawing.Point(4, 24)
        Me.TabPageConfiguration.Name = "TabPageConfiguration"
        Me.TabPageConfiguration.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageConfiguration.Size = New System.Drawing.Size(511, 690)
        Me.TabPageConfiguration.TabIndex = 5
        Me.TabPageConfiguration.Text = "Configuration"
        '
        'TabControl2
        '
        Me.TabControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl2.Controls.Add(Me.TabPageTopLevelAssy)
        Me.TabControl2.Controls.Add(Me.TabPageSorting)
        Me.TabControl2.Controls.Add(Me.TabPageOpenSave)
        Me.TabControl2.Controls.Add(Me.TabPageTemplates)
        Me.TabControl2.Controls.Add(Me.TabPagePrinting)
        Me.TabControl2.Controls.Add(Me.TabPageGeneral)
        Me.TabControl2.ImageList = Me.TabPage_ImageList
        Me.TabControl2.Location = New System.Drawing.Point(0, 0)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(503, 676)
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
        Me.TabPageTopLevelAssy.Location = New System.Drawing.Point(4, 24)
        Me.TabPageTopLevelAssy.Name = "TabPageTopLevelAssy"
        Me.TabPageTopLevelAssy.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTopLevelAssy.Size = New System.Drawing.Size(495, 648)
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
        Me.CheckBoxTLAIgnoreIncludeInReports.Location = New System.Drawing.Point(14, 119)
        Me.CheckBoxTLAIgnoreIncludeInReports.Name = "CheckBoxTLAIgnoreIncludeInReports"
        Me.CheckBoxTLAIgnoreIncludeInReports.Size = New System.Drawing.Size(245, 19)
        Me.CheckBoxTLAIgnoreIncludeInReports.TabIndex = 21
        Me.CheckBoxTLAIgnoreIncludeInReports.Text = "Ignore occurrence's IncludeInReports setting"
        Me.CheckBoxTLAIgnoreIncludeInReports.UseVisualStyleBackColor = True
        Me.CheckBoxTLAIgnoreIncludeInReports.Visible = False
        '
        'CheckBoxTLAIncludePartCopies
        '
        Me.CheckBoxTLAIncludePartCopies.AutoSize = True
        Me.CheckBoxTLAIncludePartCopies.Location = New System.Drawing.Point(14, 65)
        Me.CheckBoxTLAIncludePartCopies.Name = "CheckBoxTLAIncludePartCopies"
        Me.CheckBoxTLAIncludePartCopies.Size = New System.Drawing.Size(466, 19)
        Me.CheckBoxTLAIncludePartCopies.TabIndex = 20
        Me.CheckBoxTLAIncludePartCopies.Text = "Include parents of all part copies in search results, even if they are not in the" &
    " top level assy"
        Me.CheckBoxTLAIncludePartCopies.UseVisualStyleBackColor = True
        '
        'CheckBoxDraftAndModelSameName
        '
        Me.CheckBoxDraftAndModelSameName.AutoSize = True
        Me.CheckBoxDraftAndModelSameName.Location = New System.Drawing.Point(14, 211)
        Me.CheckBoxDraftAndModelSameName.Name = "CheckBoxDraftAndModelSameName"
        Me.CheckBoxDraftAndModelSameName.Size = New System.Drawing.Size(294, 19)
        Me.CheckBoxDraftAndModelSameName.TabIndex = 19
        Me.CheckBoxDraftAndModelSameName.Text = "Draft and model have same name and are in same folder"
        Me.CheckBoxDraftAndModelSameName.UseVisualStyleBackColor = True
        '
        'CheckBoxWarnBareTLA
        '
        Me.CheckBoxWarnBareTLA.AutoSize = True
        Me.CheckBoxWarnBareTLA.Checked = True
        Me.CheckBoxWarnBareTLA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxWarnBareTLA.Location = New System.Drawing.Point(14, 37)
        Me.CheckBoxWarnBareTLA.Name = "CheckBoxWarnBareTLA"
        Me.CheckBoxWarnBareTLA.Size = New System.Drawing.Size(382, 19)
        Me.CheckBoxWarnBareTLA.TabIndex = 18
        Me.CheckBoxWarnBareTLA.Text = "Warn me if a top-level assembly does not have a top-level folder specified"
        Me.CheckBoxWarnBareTLA.UseVisualStyleBackColor = True
        '
        'CheckBoxTLAAutoIncludeTLF
        '
        Me.CheckBoxTLAAutoIncludeTLF.AutoSize = True
        Me.CheckBoxTLAAutoIncludeTLF.Checked = True
        Me.CheckBoxTLAAutoIncludeTLF.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxTLAAutoIncludeTLF.Location = New System.Drawing.Point(14, 9)
        Me.CheckBoxTLAAutoIncludeTLF.Name = "CheckBoxTLAAutoIncludeTLF"
        Me.CheckBoxTLAAutoIncludeTLF.Size = New System.Drawing.Size(342, 19)
        Me.CheckBoxTLAAutoIncludeTLF.TabIndex = 17
        Me.CheckBoxTLAAutoIncludeTLF.Text = "Automatically include the folder if a top-level assembly is chosen"
        Me.CheckBoxTLAAutoIncludeTLF.UseVisualStyleBackColor = True
        '
        'ButtonFastSearchScopeFilename
        '
        Me.ButtonFastSearchScopeFilename.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonFastSearchScopeFilename.Location = New System.Drawing.Point(419, 262)
        Me.ButtonFastSearchScopeFilename.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonFastSearchScopeFilename.Name = "ButtonFastSearchScopeFilename"
        Me.ButtonFastSearchScopeFilename.Size = New System.Drawing.Size(75, 23)
        Me.ButtonFastSearchScopeFilename.TabIndex = 16
        Me.ButtonFastSearchScopeFilename.Text = "Browse"
        Me.ButtonFastSearchScopeFilename.UseVisualStyleBackColor = True
        '
        'TextBoxFastSearchScopeFilename
        '
        Me.TextBoxFastSearchScopeFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFastSearchScopeFilename.Location = New System.Drawing.Point(14, 263)
        Me.TextBoxFastSearchScopeFilename.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFastSearchScopeFilename.Name = "TextBoxFastSearchScopeFilename"
        Me.TextBoxFastSearchScopeFilename.Size = New System.Drawing.Size(389, 22)
        Me.TextBoxFastSearchScopeFilename.TabIndex = 15
        '
        'LabelFastSearchScopeFilename
        '
        Me.LabelFastSearchScopeFilename.AutoSize = True
        Me.LabelFastSearchScopeFilename.Location = New System.Drawing.Point(14, 240)
        Me.LabelFastSearchScopeFilename.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFastSearchScopeFilename.Name = "LabelFastSearchScopeFilename"
        Me.LabelFastSearchScopeFilename.Size = New System.Drawing.Size(141, 15)
        Me.LabelFastSearchScopeFilename.TabIndex = 14
        Me.LabelFastSearchScopeFilename.Text = "Fast Search Scope Filename"
        '
        'CheckBoxTLAReportUnrelatedFiles
        '
        Me.CheckBoxTLAReportUnrelatedFiles.AutoSize = True
        Me.CheckBoxTLAReportUnrelatedFiles.Location = New System.Drawing.Point(14, 90)
        Me.CheckBoxTLAReportUnrelatedFiles.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxTLAReportUnrelatedFiles.Name = "CheckBoxTLAReportUnrelatedFiles"
        Me.CheckBoxTLAReportUnrelatedFiles.Size = New System.Drawing.Size(236, 19)
        Me.CheckBoxTLAReportUnrelatedFiles.TabIndex = 13
        Me.CheckBoxTLAReportUnrelatedFiles.Text = "Report files unrelated to top level assembly"
        Me.CheckBoxTLAReportUnrelatedFiles.UseVisualStyleBackColor = True
        '
        'RadioButtonTLATopDown
        '
        Me.RadioButtonTLATopDown.AutoSize = True
        Me.RadioButtonTLATopDown.Location = New System.Drawing.Point(14, 155)
        Me.RadioButtonTLATopDown.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonTLATopDown.Name = "RadioButtonTLATopDown"
        Me.RadioButtonTLATopDown.Size = New System.Drawing.Size(326, 19)
        Me.RadioButtonTLATopDown.TabIndex = 12
        Me.RadioButtonTLATopDown.Text = "Top Down Search -- Best for self-contained project directories"
        Me.RadioButtonTLATopDown.UseVisualStyleBackColor = True
        '
        'RadioButtonTLABottomUp
        '
        Me.RadioButtonTLABottomUp.AutoSize = True
        Me.RadioButtonTLABottomUp.Checked = True
        Me.RadioButtonTLABottomUp.Location = New System.Drawing.Point(14, 185)
        Me.RadioButtonTLABottomUp.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonTLABottomUp.Name = "RadioButtonTLABottomUp"
        Me.RadioButtonTLABottomUp.Size = New System.Drawing.Size(300, 19)
        Me.RadioButtonTLABottomUp.TabIndex = 11
        Me.RadioButtonTLABottomUp.TabStop = True
        Me.RadioButtonTLABottomUp.Text = "Bottom Up Search -- Best for general purpose directories"
        Me.RadioButtonTLABottomUp.UseVisualStyleBackColor = True
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
        Me.TabPageSorting.Name = "TabPageSorting"
        Me.TabPageSorting.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSorting.Size = New System.Drawing.Size(495, 648)
        Me.TabPageSorting.TabIndex = 4
        Me.TabPageSorting.Text = "Sorting"
        Me.TabPageSorting.UseVisualStyleBackColor = True
        '
        'TextBoxRandomSampleFraction
        '
        Me.TextBoxRandomSampleFraction.Location = New System.Drawing.Point(226, 149)
        Me.TextBoxRandomSampleFraction.Name = "TextBoxRandomSampleFraction"
        Me.TextBoxRandomSampleFraction.Size = New System.Drawing.Size(100, 22)
        Me.TextBoxRandomSampleFraction.TabIndex = 55
        Me.TextBoxRandomSampleFraction.Text = "0.1"
        '
        'LabelRandomSampleFraction
        '
        Me.LabelRandomSampleFraction.AutoSize = True
        Me.LabelRandomSampleFraction.Location = New System.Drawing.Point(139, 151)
        Me.LabelRandomSampleFraction.Name = "LabelRandomSampleFraction"
        Me.LabelRandomSampleFraction.Size = New System.Drawing.Size(82, 15)
        Me.LabelRandomSampleFraction.TabIndex = 54
        Me.LabelRandomSampleFraction.Text = "Sample fraction"
        '
        'RadioButtonListSortRandomSample
        '
        Me.RadioButtonListSortRandomSample.AutoSize = True
        Me.RadioButtonListSortRandomSample.Location = New System.Drawing.Point(12, 148)
        Me.RadioButtonListSortRandomSample.Name = "RadioButtonListSortRandomSample"
        Me.RadioButtonListSortRandomSample.Size = New System.Drawing.Size(100, 19)
        Me.RadioButtonListSortRandomSample.TabIndex = 53
        Me.RadioButtonListSortRandomSample.TabStop = True
        Me.RadioButtonListSortRandomSample.Text = "Random sample"
        Me.RadioButtonListSortRandomSample.UseVisualStyleBackColor = True
        '
        'CheckBoxListIncludeNoDependencies
        '
        Me.CheckBoxListIncludeNoDependencies.AutoSize = True
        Me.CheckBoxListIncludeNoDependencies.Location = New System.Drawing.Point(39, 123)
        Me.CheckBoxListIncludeNoDependencies.Name = "CheckBoxListIncludeNoDependencies"
        Me.CheckBoxListIncludeNoDependencies.Size = New System.Drawing.Size(329, 19)
        Me.CheckBoxListIncludeNoDependencies.TabIndex = 52
        Me.CheckBoxListIncludeNoDependencies.Text = "Include files with no Part Copy dependencies in search results"
        Me.CheckBoxListIncludeNoDependencies.UseVisualStyleBackColor = True
        '
        'RadioButtonListSortDependency
        '
        Me.RadioButtonListSortDependency.AutoSize = True
        Me.RadioButtonListSortDependency.Location = New System.Drawing.Point(12, 95)
        Me.RadioButtonListSortDependency.Name = "RadioButtonListSortDependency"
        Me.RadioButtonListSortDependency.Size = New System.Drawing.Size(161, 19)
        Me.RadioButtonListSortDependency.TabIndex = 51
        Me.RadioButtonListSortDependency.Text = "Sorted in dependency order"
        Me.RadioButtonListSortDependency.UseVisualStyleBackColor = True
        '
        'RadioButtonListSortAlphabetical
        '
        Me.RadioButtonListSortAlphabetical.AutoSize = True
        Me.RadioButtonListSortAlphabetical.Checked = True
        Me.RadioButtonListSortAlphabetical.Location = New System.Drawing.Point(11, 66)
        Me.RadioButtonListSortAlphabetical.Name = "RadioButtonListSortAlphabetical"
        Me.RadioButtonListSortAlphabetical.Size = New System.Drawing.Size(159, 19)
        Me.RadioButtonListSortAlphabetical.TabIndex = 50
        Me.RadioButtonListSortAlphabetical.TabStop = True
        Me.RadioButtonListSortAlphabetical.Text = "Sorted in alphabetical order"
        Me.RadioButtonListSortAlphabetical.UseVisualStyleBackColor = True
        '
        'RadioButtonListSortNone
        '
        Me.RadioButtonListSortNone.AutoSize = True
        Me.RadioButtonListSortNone.Location = New System.Drawing.Point(11, 39)
        Me.RadioButtonListSortNone.Name = "RadioButtonListSortNone"
        Me.RadioButtonListSortNone.Size = New System.Drawing.Size(69, 19)
        Me.RadioButtonListSortNone.TabIndex = 49
        Me.RadioButtonListSortNone.Text = "Unsorted"
        Me.RadioButtonListSortNone.UseVisualStyleBackColor = True
        '
        'TabPageOpenSave
        '
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusAfter)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusBefore)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusOutReleased)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusOutObsolete)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusOutIW)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusOutInReview)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusOutBaselined)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusOutAvailable)
        Me.TabPageOpenSave.Controls.Add(Me.GroupBoxStatusInR)
        Me.TabPageOpenSave.Controls.Add(Me.GroupBoxStatusInO)
        Me.TabPageOpenSave.Controls.Add(Me.GroupBoxStatusInIW)
        Me.TabPageOpenSave.Controls.Add(Me.GroupBoxStatusInIR)
        Me.TabPageOpenSave.Controls.Add(Me.GroupBoxStatusInB)
        Me.TabPageOpenSave.Controls.Add(Me.GroupBoxStatusInA)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusInReleased)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusInObsolete)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusInInWork)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusInInReview)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusInBaselined)
        Me.TabPageOpenSave.Controls.Add(Me.LabelStatusInAvailable)
        Me.TabPageOpenSave.Controls.Add(Me.RadioButtonReadOnlyChange)
        Me.TabPageOpenSave.Controls.Add(Me.RadioButtonReadOnlyRevert)
        Me.TabPageOpenSave.Controls.Add(Me.CheckBoxProcessReadOnly)
        Me.TabPageOpenSave.Controls.Add(Me.CheckBoxSaveAsPDFPerSheetSupress)
        Me.TabPageOpenSave.Controls.Add(Me.CheckBoxSaveAsImageCrop)
        Me.TabPageOpenSave.Controls.Add(Me.CheckBoxRunExternalProgramSaveFile)
        Me.TabPageOpenSave.Controls.Add(Me.CheckBoxNoUpdateMRU)
        Me.TabPageOpenSave.Controls.Add(Me.CheckBoxWarnSave)
        Me.TabPageOpenSave.ImageKey = "folder"
        Me.TabPageOpenSave.Location = New System.Drawing.Point(4, 24)
        Me.TabPageOpenSave.Name = "TabPageOpenSave"
        Me.TabPageOpenSave.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageOpenSave.Size = New System.Drawing.Size(495, 648)
        Me.TabPageOpenSave.TabIndex = 3
        Me.TabPageOpenSave.Text = "Open/Save"
        Me.TabPageOpenSave.UseVisualStyleBackColor = True
        '
        'LabelStatusAfter
        '
        Me.LabelStatusAfter.AutoSize = True
        Me.LabelStatusAfter.Location = New System.Drawing.Point(220, 252)
        Me.LabelStatusAfter.Name = "LabelStatusAfter"
        Me.LabelStatusAfter.Size = New System.Drawing.Size(75, 15)
        Me.LabelStatusAfter.TabIndex = 95
        Me.LabelStatusAfter.Text = "STATUS AFTER"
        '
        'LabelStatusBefore
        '
        Me.LabelStatusBefore.AutoSize = True
        Me.LabelStatusBefore.Location = New System.Drawing.Point(44, 280)
        Me.LabelStatusBefore.Name = "LabelStatusBefore"
        Me.LabelStatusBefore.Size = New System.Drawing.Size(81, 15)
        Me.LabelStatusBefore.TabIndex = 94
        Me.LabelStatusBefore.Text = "STATUS BEFORE"
        '
        'LabelStatusOutReleased
        '
        Me.LabelStatusOutReleased.AutoSize = True
        Me.LabelStatusOutReleased.Location = New System.Drawing.Point(325, 280)
        Me.LabelStatusOutReleased.Name = "LabelStatusOutReleased"
        Me.LabelStatusOutReleased.Size = New System.Drawing.Size(13, 15)
        Me.LabelStatusOutReleased.TabIndex = 93
        Me.LabelStatusOutReleased.Text = "R"
        '
        'LabelStatusOutObsolete
        '
        Me.LabelStatusOutObsolete.AutoSize = True
        Me.LabelStatusOutObsolete.Location = New System.Drawing.Point(300, 280)
        Me.LabelStatusOutObsolete.Name = "LabelStatusOutObsolete"
        Me.LabelStatusOutObsolete.Size = New System.Drawing.Size(15, 15)
        Me.LabelStatusOutObsolete.TabIndex = 92
        Me.LabelStatusOutObsolete.Text = "O"
        '
        'LabelStatusOutIW
        '
        Me.LabelStatusOutIW.AutoSize = True
        Me.LabelStatusOutIW.Location = New System.Drawing.Point(274, 280)
        Me.LabelStatusOutIW.Name = "LabelStatusOutIW"
        Me.LabelStatusOutIW.Size = New System.Drawing.Size(20, 15)
        Me.LabelStatusOutIW.TabIndex = 91
        Me.LabelStatusOutIW.Text = "IW"
        '
        'LabelStatusOutInReview
        '
        Me.LabelStatusOutInReview.AutoSize = True
        Me.LabelStatusOutInReview.Location = New System.Drawing.Point(248, 280)
        Me.LabelStatusOutInReview.Name = "LabelStatusOutInReview"
        Me.LabelStatusOutInReview.Size = New System.Drawing.Size(16, 15)
        Me.LabelStatusOutInReview.TabIndex = 90
        Me.LabelStatusOutInReview.Text = "IR"
        '
        'LabelStatusOutBaselined
        '
        Me.LabelStatusOutBaselined.AutoSize = True
        Me.LabelStatusOutBaselined.Location = New System.Drawing.Point(225, 280)
        Me.LabelStatusOutBaselined.Name = "LabelStatusOutBaselined"
        Me.LabelStatusOutBaselined.Size = New System.Drawing.Size(13, 15)
        Me.LabelStatusOutBaselined.TabIndex = 89
        Me.LabelStatusOutBaselined.Text = "B"
        '
        'LabelStatusOutAvailable
        '
        Me.LabelStatusOutAvailable.AutoSize = True
        Me.LabelStatusOutAvailable.Location = New System.Drawing.Point(200, 280)
        Me.LabelStatusOutAvailable.Name = "LabelStatusOutAvailable"
        Me.LabelStatusOutAvailable.Size = New System.Drawing.Size(14, 15)
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
        Me.GroupBoxStatusInR.Location = New System.Drawing.Point(200, 442)
        Me.GroupBoxStatusInR.Name = "GroupBoxStatusInR"
        Me.GroupBoxStatusInR.Size = New System.Drawing.Size(150, 29)
        Me.GroupBoxStatusInR.TabIndex = 87
        Me.GroupBoxStatusInR.TabStop = False
        '
        'RadioButtonStatusRtoR
        '
        Me.RadioButtonStatusRtoR.AutoSize = True
        Me.RadioButtonStatusRtoR.Location = New System.Drawing.Point(125, 12)
        Me.RadioButtonStatusRtoR.Name = "RadioButtonStatusRtoR"
        Me.RadioButtonStatusRtoR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusRtoR.TabIndex = 5
        Me.RadioButtonStatusRtoR.TabStop = True
        Me.RadioButtonStatusRtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoO
        '
        Me.RadioButtonStatusRtoO.AutoSize = True
        Me.RadioButtonStatusRtoO.Location = New System.Drawing.Point(100, 12)
        Me.RadioButtonStatusRtoO.Name = "RadioButtonStatusRtoO"
        Me.RadioButtonStatusRtoO.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusRtoO.TabIndex = 4
        Me.RadioButtonStatusRtoO.TabStop = True
        Me.RadioButtonStatusRtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoIW
        '
        Me.RadioButtonStatusRtoIW.AutoSize = True
        Me.RadioButtonStatusRtoIW.Location = New System.Drawing.Point(75, 12)
        Me.RadioButtonStatusRtoIW.Name = "RadioButtonStatusRtoIW"
        Me.RadioButtonStatusRtoIW.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusRtoIW.TabIndex = 3
        Me.RadioButtonStatusRtoIW.TabStop = True
        Me.RadioButtonStatusRtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoIR
        '
        Me.RadioButtonStatusRtoIR.AutoSize = True
        Me.RadioButtonStatusRtoIR.Location = New System.Drawing.Point(50, 12)
        Me.RadioButtonStatusRtoIR.Name = "RadioButtonStatusRtoIR"
        Me.RadioButtonStatusRtoIR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusRtoIR.TabIndex = 2
        Me.RadioButtonStatusRtoIR.TabStop = True
        Me.RadioButtonStatusRtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusRtoB
        '
        Me.RadioButtonStatusRtoB.AutoSize = True
        Me.RadioButtonStatusRtoB.Location = New System.Drawing.Point(25, 12)
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
        Me.RadioButtonStatusRtoA.Location = New System.Drawing.Point(0, 12)
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
        Me.GroupBoxStatusInO.Location = New System.Drawing.Point(200, 413)
        Me.GroupBoxStatusInO.Name = "GroupBoxStatusInO"
        Me.GroupBoxStatusInO.Size = New System.Drawing.Size(150, 29)
        Me.GroupBoxStatusInO.TabIndex = 86
        Me.GroupBoxStatusInO.TabStop = False
        '
        'RadioButtonStatusOtoR
        '
        Me.RadioButtonStatusOtoR.AutoSize = True
        Me.RadioButtonStatusOtoR.Location = New System.Drawing.Point(125, 12)
        Me.RadioButtonStatusOtoR.Name = "RadioButtonStatusOtoR"
        Me.RadioButtonStatusOtoR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusOtoR.TabIndex = 5
        Me.RadioButtonStatusOtoR.TabStop = True
        Me.RadioButtonStatusOtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoO
        '
        Me.RadioButtonStatusOtoO.AutoSize = True
        Me.RadioButtonStatusOtoO.Location = New System.Drawing.Point(100, 12)
        Me.RadioButtonStatusOtoO.Name = "RadioButtonStatusOtoO"
        Me.RadioButtonStatusOtoO.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusOtoO.TabIndex = 4
        Me.RadioButtonStatusOtoO.TabStop = True
        Me.RadioButtonStatusOtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoIW
        '
        Me.RadioButtonStatusOtoIW.AutoSize = True
        Me.RadioButtonStatusOtoIW.Location = New System.Drawing.Point(75, 12)
        Me.RadioButtonStatusOtoIW.Name = "RadioButtonStatusOtoIW"
        Me.RadioButtonStatusOtoIW.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusOtoIW.TabIndex = 3
        Me.RadioButtonStatusOtoIW.TabStop = True
        Me.RadioButtonStatusOtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoIR
        '
        Me.RadioButtonStatusOtoIR.AutoSize = True
        Me.RadioButtonStatusOtoIR.Location = New System.Drawing.Point(50, 12)
        Me.RadioButtonStatusOtoIR.Name = "RadioButtonStatusOtoIR"
        Me.RadioButtonStatusOtoIR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusOtoIR.TabIndex = 2
        Me.RadioButtonStatusOtoIR.TabStop = True
        Me.RadioButtonStatusOtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusOtoB
        '
        Me.RadioButtonStatusOtoB.AutoSize = True
        Me.RadioButtonStatusOtoB.Location = New System.Drawing.Point(25, 12)
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
        Me.RadioButtonStatusOtoA.Location = New System.Drawing.Point(0, 12)
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
        Me.GroupBoxStatusInIW.Location = New System.Drawing.Point(200, 384)
        Me.GroupBoxStatusInIW.Name = "GroupBoxStatusInIW"
        Me.GroupBoxStatusInIW.Size = New System.Drawing.Size(150, 29)
        Me.GroupBoxStatusInIW.TabIndex = 85
        Me.GroupBoxStatusInIW.TabStop = False
        '
        'RadioButtonStatusIWtoR
        '
        Me.RadioButtonStatusIWtoR.AutoSize = True
        Me.RadioButtonStatusIWtoR.Location = New System.Drawing.Point(125, 12)
        Me.RadioButtonStatusIWtoR.Name = "RadioButtonStatusIWtoR"
        Me.RadioButtonStatusIWtoR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIWtoR.TabIndex = 5
        Me.RadioButtonStatusIWtoR.TabStop = True
        Me.RadioButtonStatusIWtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoO
        '
        Me.RadioButtonStatusIWtoO.AutoSize = True
        Me.RadioButtonStatusIWtoO.Location = New System.Drawing.Point(100, 12)
        Me.RadioButtonStatusIWtoO.Name = "RadioButtonStatusIWtoO"
        Me.RadioButtonStatusIWtoO.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIWtoO.TabIndex = 4
        Me.RadioButtonStatusIWtoO.TabStop = True
        Me.RadioButtonStatusIWtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoIW
        '
        Me.RadioButtonStatusIWtoIW.AutoSize = True
        Me.RadioButtonStatusIWtoIW.Location = New System.Drawing.Point(75, 12)
        Me.RadioButtonStatusIWtoIW.Name = "RadioButtonStatusIWtoIW"
        Me.RadioButtonStatusIWtoIW.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIWtoIW.TabIndex = 3
        Me.RadioButtonStatusIWtoIW.TabStop = True
        Me.RadioButtonStatusIWtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoIR
        '
        Me.RadioButtonStatusIWtoIR.AutoSize = True
        Me.RadioButtonStatusIWtoIR.Location = New System.Drawing.Point(50, 12)
        Me.RadioButtonStatusIWtoIR.Name = "RadioButtonStatusIWtoIR"
        Me.RadioButtonStatusIWtoIR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIWtoIR.TabIndex = 2
        Me.RadioButtonStatusIWtoIR.TabStop = True
        Me.RadioButtonStatusIWtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIWtoB
        '
        Me.RadioButtonStatusIWtoB.AutoSize = True
        Me.RadioButtonStatusIWtoB.Location = New System.Drawing.Point(25, 12)
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
        Me.RadioButtonStatusIWtoA.Location = New System.Drawing.Point(0, 12)
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
        Me.GroupBoxStatusInIR.Location = New System.Drawing.Point(200, 355)
        Me.GroupBoxStatusInIR.Name = "GroupBoxStatusInIR"
        Me.GroupBoxStatusInIR.Size = New System.Drawing.Size(150, 29)
        Me.GroupBoxStatusInIR.TabIndex = 84
        Me.GroupBoxStatusInIR.TabStop = False
        '
        'RadioButtonStatusIRtoR
        '
        Me.RadioButtonStatusIRtoR.AutoSize = True
        Me.RadioButtonStatusIRtoR.Location = New System.Drawing.Point(125, 12)
        Me.RadioButtonStatusIRtoR.Name = "RadioButtonStatusIRtoR"
        Me.RadioButtonStatusIRtoR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIRtoR.TabIndex = 5
        Me.RadioButtonStatusIRtoR.TabStop = True
        Me.RadioButtonStatusIRtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoO
        '
        Me.RadioButtonStatusIRtoO.AutoSize = True
        Me.RadioButtonStatusIRtoO.Location = New System.Drawing.Point(100, 12)
        Me.RadioButtonStatusIRtoO.Name = "RadioButtonStatusIRtoO"
        Me.RadioButtonStatusIRtoO.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIRtoO.TabIndex = 4
        Me.RadioButtonStatusIRtoO.TabStop = True
        Me.RadioButtonStatusIRtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoIW
        '
        Me.RadioButtonStatusIRtoIW.AutoSize = True
        Me.RadioButtonStatusIRtoIW.Location = New System.Drawing.Point(75, 12)
        Me.RadioButtonStatusIRtoIW.Name = "RadioButtonStatusIRtoIW"
        Me.RadioButtonStatusIRtoIW.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIRtoIW.TabIndex = 3
        Me.RadioButtonStatusIRtoIW.TabStop = True
        Me.RadioButtonStatusIRtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoIR
        '
        Me.RadioButtonStatusIRtoIR.AutoSize = True
        Me.RadioButtonStatusIRtoIR.Location = New System.Drawing.Point(50, 12)
        Me.RadioButtonStatusIRtoIR.Name = "RadioButtonStatusIRtoIR"
        Me.RadioButtonStatusIRtoIR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusIRtoIR.TabIndex = 2
        Me.RadioButtonStatusIRtoIR.TabStop = True
        Me.RadioButtonStatusIRtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusIRtoB
        '
        Me.RadioButtonStatusIRtoB.AutoSize = True
        Me.RadioButtonStatusIRtoB.Location = New System.Drawing.Point(25, 12)
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
        Me.RadioButtonStatusIRtoA.Location = New System.Drawing.Point(0, 12)
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
        Me.GroupBoxStatusInB.Location = New System.Drawing.Point(200, 327)
        Me.GroupBoxStatusInB.Name = "GroupBoxStatusInB"
        Me.GroupBoxStatusInB.Size = New System.Drawing.Size(150, 29)
        Me.GroupBoxStatusInB.TabIndex = 83
        Me.GroupBoxStatusInB.TabStop = False
        '
        'RadioButtonStatusBtoR
        '
        Me.RadioButtonStatusBtoR.AutoSize = True
        Me.RadioButtonStatusBtoR.Location = New System.Drawing.Point(125, 12)
        Me.RadioButtonStatusBtoR.Name = "RadioButtonStatusBtoR"
        Me.RadioButtonStatusBtoR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusBtoR.TabIndex = 5
        Me.RadioButtonStatusBtoR.TabStop = True
        Me.RadioButtonStatusBtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoO
        '
        Me.RadioButtonStatusBtoO.AutoSize = True
        Me.RadioButtonStatusBtoO.Location = New System.Drawing.Point(100, 12)
        Me.RadioButtonStatusBtoO.Name = "RadioButtonStatusBtoO"
        Me.RadioButtonStatusBtoO.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusBtoO.TabIndex = 4
        Me.RadioButtonStatusBtoO.TabStop = True
        Me.RadioButtonStatusBtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoIW
        '
        Me.RadioButtonStatusBtoIW.AutoSize = True
        Me.RadioButtonStatusBtoIW.Location = New System.Drawing.Point(75, 12)
        Me.RadioButtonStatusBtoIW.Name = "RadioButtonStatusBtoIW"
        Me.RadioButtonStatusBtoIW.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusBtoIW.TabIndex = 3
        Me.RadioButtonStatusBtoIW.TabStop = True
        Me.RadioButtonStatusBtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoIR
        '
        Me.RadioButtonStatusBtoIR.AutoSize = True
        Me.RadioButtonStatusBtoIR.Location = New System.Drawing.Point(50, 12)
        Me.RadioButtonStatusBtoIR.Name = "RadioButtonStatusBtoIR"
        Me.RadioButtonStatusBtoIR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusBtoIR.TabIndex = 2
        Me.RadioButtonStatusBtoIR.TabStop = True
        Me.RadioButtonStatusBtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusBtoB
        '
        Me.RadioButtonStatusBtoB.AutoSize = True
        Me.RadioButtonStatusBtoB.Location = New System.Drawing.Point(25, 12)
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
        Me.RadioButtonStatusBtoA.Location = New System.Drawing.Point(0, 12)
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
        Me.GroupBoxStatusInA.Location = New System.Drawing.Point(200, 298)
        Me.GroupBoxStatusInA.Name = "GroupBoxStatusInA"
        Me.GroupBoxStatusInA.Size = New System.Drawing.Size(150, 29)
        Me.GroupBoxStatusInA.TabIndex = 82
        Me.GroupBoxStatusInA.TabStop = False
        '
        'RadioButtonStatusAtoR
        '
        Me.RadioButtonStatusAtoR.AutoSize = True
        Me.RadioButtonStatusAtoR.Location = New System.Drawing.Point(125, 12)
        Me.RadioButtonStatusAtoR.Name = "RadioButtonStatusAtoR"
        Me.RadioButtonStatusAtoR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusAtoR.TabIndex = 5
        Me.RadioButtonStatusAtoR.TabStop = True
        Me.RadioButtonStatusAtoR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoO
        '
        Me.RadioButtonStatusAtoO.AutoSize = True
        Me.RadioButtonStatusAtoO.Location = New System.Drawing.Point(100, 12)
        Me.RadioButtonStatusAtoO.Name = "RadioButtonStatusAtoO"
        Me.RadioButtonStatusAtoO.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusAtoO.TabIndex = 4
        Me.RadioButtonStatusAtoO.TabStop = True
        Me.RadioButtonStatusAtoO.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoIW
        '
        Me.RadioButtonStatusAtoIW.AutoSize = True
        Me.RadioButtonStatusAtoIW.Location = New System.Drawing.Point(75, 12)
        Me.RadioButtonStatusAtoIW.Name = "RadioButtonStatusAtoIW"
        Me.RadioButtonStatusAtoIW.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusAtoIW.TabIndex = 3
        Me.RadioButtonStatusAtoIW.TabStop = True
        Me.RadioButtonStatusAtoIW.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoIR
        '
        Me.RadioButtonStatusAtoIR.AutoSize = True
        Me.RadioButtonStatusAtoIR.Location = New System.Drawing.Point(50, 12)
        Me.RadioButtonStatusAtoIR.Name = "RadioButtonStatusAtoIR"
        Me.RadioButtonStatusAtoIR.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusAtoIR.TabIndex = 2
        Me.RadioButtonStatusAtoIR.TabStop = True
        Me.RadioButtonStatusAtoIR.UseVisualStyleBackColor = True
        '
        'RadioButtonStatusAtoB
        '
        Me.RadioButtonStatusAtoB.AutoSize = True
        Me.RadioButtonStatusAtoB.Location = New System.Drawing.Point(25, 12)
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
        Me.RadioButtonStatusAtoA.Location = New System.Drawing.Point(0, 12)
        Me.RadioButtonStatusAtoA.Name = "RadioButtonStatusAtoA"
        Me.RadioButtonStatusAtoA.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonStatusAtoA.TabIndex = 0
        Me.RadioButtonStatusAtoA.TabStop = True
        Me.RadioButtonStatusAtoA.UseVisualStyleBackColor = True
        '
        'LabelStatusInReleased
        '
        Me.LabelStatusInReleased.AutoSize = True
        Me.LabelStatusInReleased.Location = New System.Drawing.Point(118, 452)
        Me.LabelStatusInReleased.Name = "LabelStatusInReleased"
        Me.LabelStatusInReleased.Size = New System.Drawing.Size(65, 15)
        Me.LabelStatusInReleased.TabIndex = 81
        Me.LabelStatusInReleased.Text = "Released (R)"
        '
        'LabelStatusInObsolete
        '
        Me.LabelStatusInObsolete.AutoSize = True
        Me.LabelStatusInObsolete.Location = New System.Drawing.Point(121, 423)
        Me.LabelStatusInObsolete.Name = "LabelStatusInObsolete"
        Me.LabelStatusInObsolete.Size = New System.Drawing.Size(68, 15)
        Me.LabelStatusInObsolete.TabIndex = 80
        Me.LabelStatusInObsolete.Text = "Obsolete (O)"
        '
        'LabelStatusInInWork
        '
        Me.LabelStatusInInWork.AutoSize = True
        Me.LabelStatusInInWork.Location = New System.Drawing.Point(116, 395)
        Me.LabelStatusInInWork.Name = "LabelStatusInInWork"
        Me.LabelStatusInInWork.Size = New System.Drawing.Size(69, 15)
        Me.LabelStatusInInWork.TabIndex = 79
        Me.LabelStatusInInWork.Text = "In Work  (IW)"
        '
        'LabelStatusInInReview
        '
        Me.LabelStatusInInReview.AutoSize = True
        Me.LabelStatusInInReview.Location = New System.Drawing.Point(112, 366)
        Me.LabelStatusInInReview.Name = "LabelStatusInInReview"
        Me.LabelStatusInInReview.Size = New System.Drawing.Size(71, 15)
        Me.LabelStatusInInReview.TabIndex = 78
        Me.LabelStatusInInReview.Text = "In Review (IR)"
        '
        'LabelStatusInBaselined
        '
        Me.LabelStatusInBaselined.AutoSize = True
        Me.LabelStatusInBaselined.Location = New System.Drawing.Point(116, 337)
        Me.LabelStatusInBaselined.Name = "LabelStatusInBaselined"
        Me.LabelStatusInBaselined.Size = New System.Drawing.Size(68, 15)
        Me.LabelStatusInBaselined.TabIndex = 77
        Me.LabelStatusInBaselined.Text = "Baselined (B)"
        '
        'LabelStatusInAvailable
        '
        Me.LabelStatusInAvailable.AutoSize = True
        Me.LabelStatusInAvailable.Location = New System.Drawing.Point(120, 308)
        Me.LabelStatusInAvailable.Name = "LabelStatusInAvailable"
        Me.LabelStatusInAvailable.Size = New System.Drawing.Size(66, 15)
        Me.LabelStatusInAvailable.TabIndex = 76
        Me.LabelStatusInAvailable.Text = "Available (A)"
        '
        'RadioButtonReadOnlyChange
        '
        Me.RadioButtonReadOnlyChange.AutoSize = True
        Me.RadioButtonReadOnlyChange.Location = New System.Drawing.Point(25, 211)
        Me.RadioButtonReadOnlyChange.Name = "RadioButtonReadOnlyChange"
        Me.RadioButtonReadOnlyChange.Size = New System.Drawing.Size(175, 19)
        Me.RadioButtonReadOnlyChange.TabIndex = 75
        Me.RadioButtonReadOnlyChange.TabStop = True
        Me.RadioButtonReadOnlyChange.Text = "Change status after processing"
        Me.RadioButtonReadOnlyChange.UseVisualStyleBackColor = True
        '
        'RadioButtonReadOnlyRevert
        '
        Me.RadioButtonReadOnlyRevert.AutoSize = True
        Me.RadioButtonReadOnlyRevert.Checked = True
        Me.RadioButtonReadOnlyRevert.Location = New System.Drawing.Point(25, 182)
        Me.RadioButtonReadOnlyRevert.Name = "RadioButtonReadOnlyRevert"
        Me.RadioButtonReadOnlyRevert.Size = New System.Drawing.Size(226, 19)
        Me.RadioButtonReadOnlyRevert.TabIndex = 74
        Me.RadioButtonReadOnlyRevert.TabStop = True
        Me.RadioButtonReadOnlyRevert.Text = "Revert to previous status after processing"
        Me.RadioButtonReadOnlyRevert.UseVisualStyleBackColor = True
        '
        'CheckBoxProcessReadOnly
        '
        Me.CheckBoxProcessReadOnly.AutoSize = True
        Me.CheckBoxProcessReadOnly.Location = New System.Drawing.Point(8, 153)
        Me.CheckBoxProcessReadOnly.Name = "CheckBoxProcessReadOnly"
        Me.CheckBoxProcessReadOnly.Size = New System.Drawing.Size(296, 19)
        Me.CheckBoxProcessReadOnly.TabIndex = 73
        Me.CheckBoxProcessReadOnly.Text = "Process files as Available regardless of document Status"
        Me.CheckBoxProcessReadOnly.UseVisualStyleBackColor = True
        '
        'CheckBoxSaveAsPDFPerSheetSupress
        '
        Me.CheckBoxSaveAsPDFPerSheetSupress.AutoSize = True
        Me.CheckBoxSaveAsPDFPerSheetSupress.Location = New System.Drawing.Point(8, 125)
        Me.CheckBoxSaveAsPDFPerSheetSupress.Name = "CheckBoxSaveAsPDFPerSheetSupress"
        Me.CheckBoxSaveAsPDFPerSheetSupress.Size = New System.Drawing.Size(347, 19)
        Me.CheckBoxSaveAsPDFPerSheetSupress.TabIndex = 72
        Me.CheckBoxSaveAsPDFPerSheetSupress.Text = "Save as PDF per sheet -- Supress sheet suffix on 1 page documents"
        Me.CheckBoxSaveAsPDFPerSheetSupress.UseVisualStyleBackColor = True
        '
        'CheckBoxSaveAsImageCrop
        '
        Me.CheckBoxSaveAsImageCrop.AutoSize = True
        Me.CheckBoxSaveAsImageCrop.Location = New System.Drawing.Point(8, 96)
        Me.CheckBoxSaveAsImageCrop.Name = "CheckBoxSaveAsImageCrop"
        Me.CheckBoxSaveAsImageCrop.Size = New System.Drawing.Size(199, 19)
        Me.CheckBoxSaveAsImageCrop.TabIndex = 71
        Me.CheckBoxSaveAsImageCrop.Text = "Save as image -- Crop to model size"
        Me.CheckBoxSaveAsImageCrop.UseVisualStyleBackColor = True
        '
        'CheckBoxRunExternalProgramSaveFile
        '
        Me.CheckBoxRunExternalProgramSaveFile.AutoSize = True
        Me.CheckBoxRunExternalProgramSaveFile.Location = New System.Drawing.Point(8, 67)
        Me.CheckBoxRunExternalProgramSaveFile.Name = "CheckBoxRunExternalProgramSaveFile"
        Me.CheckBoxRunExternalProgramSaveFile.Size = New System.Drawing.Size(227, 19)
        Me.CheckBoxRunExternalProgramSaveFile.TabIndex = 70
        Me.CheckBoxRunExternalProgramSaveFile.Text = "Run external program -- Save file after run"
        Me.CheckBoxRunExternalProgramSaveFile.UseVisualStyleBackColor = True
        '
        'CheckBoxNoUpdateMRU
        '
        Me.CheckBoxNoUpdateMRU.AutoSize = True
        Me.CheckBoxNoUpdateMRU.Location = New System.Drawing.Point(8, 38)
        Me.CheckBoxNoUpdateMRU.Name = "CheckBoxNoUpdateMRU"
        Me.CheckBoxNoUpdateMRU.Size = New System.Drawing.Size(294, 19)
        Me.CheckBoxNoUpdateMRU.TabIndex = 69
        Me.CheckBoxNoUpdateMRU.Text = "Do not show processed files in Most Recently Used list"
        Me.CheckBoxNoUpdateMRU.UseVisualStyleBackColor = True
        '
        'CheckBoxWarnSave
        '
        Me.CheckBoxWarnSave.AutoSize = True
        Me.CheckBoxWarnSave.Checked = True
        Me.CheckBoxWarnSave.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxWarnSave.Location = New System.Drawing.Point(8, 9)
        Me.CheckBoxWarnSave.Name = "CheckBoxWarnSave"
        Me.CheckBoxWarnSave.Size = New System.Drawing.Size(175, 19)
        Me.CheckBoxWarnSave.TabIndex = 68
        Me.CheckBoxWarnSave.Text = "Warn me if file save is required"
        Me.CheckBoxWarnSave.UseVisualStyleBackColor = True
        '
        'TabPageTemplates
        '
        Me.TabPageTemplates.Controls.Add(Me.ButtonActiveMaterialLibrary)
        Me.TabPageTemplates.Controls.Add(Me.TextBoxActiveMaterialLibrary)
        Me.TabPageTemplates.Controls.Add(Me.LabelActiveMaterialLibrary)
        Me.TabPageTemplates.Controls.Add(Me.ButtonTemplateAssembly)
        Me.TabPageTemplates.Controls.Add(Me.TextBoxTemplateAssembly)
        Me.TabPageTemplates.Controls.Add(Me.LabelTemplateAssembly)
        Me.TabPageTemplates.Controls.Add(Me.ButtonTemplatePart)
        Me.TabPageTemplates.Controls.Add(Me.TextBoxTemplatePart)
        Me.TabPageTemplates.Controls.Add(Me.LabelTemplatePart)
        Me.TabPageTemplates.Controls.Add(Me.ButtonTemplateSheetmetal)
        Me.TabPageTemplates.Controls.Add(Me.TextBoxTemplateSheetmetal)
        Me.TabPageTemplates.Controls.Add(Me.LabelTemplateSheetmetal)
        Me.TabPageTemplates.Controls.Add(Me.ButtonTemplateDraft)
        Me.TabPageTemplates.Controls.Add(Me.TextBoxTemplateDraft)
        Me.TabPageTemplates.Controls.Add(Me.LabelTemplateDraft)
        Me.TabPageTemplates.ImageKey = "se"
        Me.TabPageTemplates.Location = New System.Drawing.Point(4, 24)
        Me.TabPageTemplates.Name = "TabPageTemplates"
        Me.TabPageTemplates.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTemplates.Size = New System.Drawing.Size(495, 648)
        Me.TabPageTemplates.TabIndex = 1
        Me.TabPageTemplates.Text = "Templates"
        Me.TabPageTemplates.UseVisualStyleBackColor = True
        '
        'ButtonActiveMaterialLibrary
        '
        Me.ButtonActiveMaterialLibrary.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonActiveMaterialLibrary.Location = New System.Drawing.Point(414, 252)
        Me.ButtonActiveMaterialLibrary.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonActiveMaterialLibrary.Name = "ButtonActiveMaterialLibrary"
        Me.ButtonActiveMaterialLibrary.Size = New System.Drawing.Size(75, 23)
        Me.ButtonActiveMaterialLibrary.TabIndex = 24
        Me.ButtonActiveMaterialLibrary.Text = "Browse"
        Me.ButtonActiveMaterialLibrary.UseVisualStyleBackColor = True
        '
        'TextBoxActiveMaterialLibrary
        '
        Me.TextBoxActiveMaterialLibrary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxActiveMaterialLibrary.Location = New System.Drawing.Point(11, 252)
        Me.TextBoxActiveMaterialLibrary.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxActiveMaterialLibrary.Name = "TextBoxActiveMaterialLibrary"
        Me.TextBoxActiveMaterialLibrary.Size = New System.Drawing.Size(397, 22)
        Me.TextBoxActiveMaterialLibrary.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.TextBoxActiveMaterialLibrary, "Material Library")
        '
        'LabelActiveMaterialLibrary
        '
        Me.LabelActiveMaterialLibrary.AutoSize = True
        Me.LabelActiveMaterialLibrary.Location = New System.Drawing.Point(11, 233)
        Me.LabelActiveMaterialLibrary.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelActiveMaterialLibrary.Name = "LabelActiveMaterialLibrary"
        Me.LabelActiveMaterialLibrary.Size = New System.Drawing.Size(83, 15)
        Me.LabelActiveMaterialLibrary.TabIndex = 22
        Me.LabelActiveMaterialLibrary.Text = "Material Library"
        '
        'ButtonTemplateAssembly
        '
        Me.ButtonTemplateAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTemplateAssembly.Location = New System.Drawing.Point(414, 27)
        Me.ButtonTemplateAssembly.Name = "ButtonTemplateAssembly"
        Me.ButtonTemplateAssembly.Size = New System.Drawing.Size(75, 23)
        Me.ButtonTemplateAssembly.TabIndex = 12
        Me.ButtonTemplateAssembly.Text = "Browse"
        Me.ButtonTemplateAssembly.UseVisualStyleBackColor = True
        '
        'TextBoxTemplateAssembly
        '
        Me.TextBoxTemplateAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplateAssembly.Location = New System.Drawing.Point(11, 27)
        Me.TextBoxTemplateAssembly.Name = "TextBoxTemplateAssembly"
        Me.TextBoxTemplateAssembly.Size = New System.Drawing.Size(397, 22)
        Me.TextBoxTemplateAssembly.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.TextBoxTemplateAssembly, "Assembly Template")
        '
        'LabelTemplateAssembly
        '
        Me.LabelTemplateAssembly.AutoSize = True
        Me.LabelTemplateAssembly.Location = New System.Drawing.Point(11, 8)
        Me.LabelTemplateAssembly.Name = "LabelTemplateAssembly"
        Me.LabelTemplateAssembly.Size = New System.Drawing.Size(121, 15)
        Me.LabelTemplateAssembly.TabIndex = 10
        Me.LabelTemplateAssembly.Text = "Assembly Template File"
        '
        'ButtonTemplatePart
        '
        Me.ButtonTemplatePart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTemplatePart.Location = New System.Drawing.Point(414, 83)
        Me.ButtonTemplatePart.Name = "ButtonTemplatePart"
        Me.ButtonTemplatePart.Size = New System.Drawing.Size(75, 23)
        Me.ButtonTemplatePart.TabIndex = 19
        Me.ButtonTemplatePart.Text = "Browse"
        Me.ButtonTemplatePart.UseVisualStyleBackColor = True
        '
        'TextBoxTemplatePart
        '
        Me.TextBoxTemplatePart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplatePart.Location = New System.Drawing.Point(11, 83)
        Me.TextBoxTemplatePart.Name = "TextBoxTemplatePart"
        Me.TextBoxTemplatePart.Size = New System.Drawing.Size(397, 22)
        Me.TextBoxTemplatePart.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.TextBoxTemplatePart, "Part Template")
        '
        'LabelTemplatePart
        '
        Me.LabelTemplatePart.AutoSize = True
        Me.LabelTemplatePart.Location = New System.Drawing.Point(11, 65)
        Me.LabelTemplatePart.Name = "LabelTemplatePart"
        Me.LabelTemplatePart.Size = New System.Drawing.Size(93, 15)
        Me.LabelTemplatePart.TabIndex = 13
        Me.LabelTemplatePart.Text = "Part Template File"
        '
        'ButtonTemplateSheetmetal
        '
        Me.ButtonTemplateSheetmetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTemplateSheetmetal.Location = New System.Drawing.Point(414, 140)
        Me.ButtonTemplateSheetmetal.Name = "ButtonTemplateSheetmetal"
        Me.ButtonTemplateSheetmetal.Size = New System.Drawing.Size(75, 23)
        Me.ButtonTemplateSheetmetal.TabIndex = 20
        Me.ButtonTemplateSheetmetal.Text = "Browse"
        Me.ButtonTemplateSheetmetal.UseVisualStyleBackColor = True
        '
        'TextBoxTemplateSheetmetal
        '
        Me.TextBoxTemplateSheetmetal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplateSheetmetal.Location = New System.Drawing.Point(11, 140)
        Me.TextBoxTemplateSheetmetal.Name = "TextBoxTemplateSheetmetal"
        Me.TextBoxTemplateSheetmetal.Size = New System.Drawing.Size(397, 22)
        Me.TextBoxTemplateSheetmetal.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.TextBoxTemplateSheetmetal, "Sheetmetal Template")
        '
        'LabelTemplateSheetmetal
        '
        Me.LabelTemplateSheetmetal.AutoSize = True
        Me.LabelTemplateSheetmetal.Location = New System.Drawing.Point(11, 121)
        Me.LabelTemplateSheetmetal.Name = "LabelTemplateSheetmetal"
        Me.LabelTemplateSheetmetal.Size = New System.Drawing.Size(129, 15)
        Me.LabelTemplateSheetmetal.TabIndex = 14
        Me.LabelTemplateSheetmetal.Text = "Sheetmetal Template File"
        '
        'ButtonTemplateDraft
        '
        Me.ButtonTemplateDraft.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTemplateDraft.Location = New System.Drawing.Point(414, 196)
        Me.ButtonTemplateDraft.Name = "ButtonTemplateDraft"
        Me.ButtonTemplateDraft.Size = New System.Drawing.Size(75, 23)
        Me.ButtonTemplateDraft.TabIndex = 21
        Me.ButtonTemplateDraft.Text = "Browse"
        Me.ButtonTemplateDraft.UseVisualStyleBackColor = True
        '
        'TextBoxTemplateDraft
        '
        Me.TextBoxTemplateDraft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplateDraft.Location = New System.Drawing.Point(11, 196)
        Me.TextBoxTemplateDraft.Name = "TextBoxTemplateDraft"
        Me.TextBoxTemplateDraft.Size = New System.Drawing.Size(397, 22)
        Me.TextBoxTemplateDraft.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.TextBoxTemplateDraft, "Draft Template")
        '
        'LabelTemplateDraft
        '
        Me.LabelTemplateDraft.AutoSize = True
        Me.LabelTemplateDraft.Location = New System.Drawing.Point(11, 177)
        Me.LabelTemplateDraft.Name = "LabelTemplateDraft"
        Me.LabelTemplateDraft.Size = New System.Drawing.Size(98, 15)
        Me.LabelTemplateDraft.TabIndex = 15
        Me.LabelTemplateDraft.Text = "Draft Template File"
        '
        'TabPagePrinting
        '
        Me.TabPagePrinting.Controls.Add(Me.GroupBoxPrinter2)
        Me.TabPagePrinting.Controls.Add(Me.GroupBoxPrinter1)
        Me.TabPagePrinting.ImageKey = "dft"
        Me.TabPagePrinting.Location = New System.Drawing.Point(4, 24)
        Me.TabPagePrinting.Name = "TabPagePrinting"
        Me.TabPagePrinting.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePrinting.Size = New System.Drawing.Size(495, 648)
        Me.TabPagePrinting.TabIndex = 5
        Me.TabPagePrinting.Text = "Printing"
        Me.TabPagePrinting.UseVisualStyleBackColor = True
        '
        'GroupBoxPrinter2
        '
        Me.GroupBoxPrinter2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxPrinter2.Controls.Add(Me.ButtonPrinter2SheetSelections)
        Me.GroupBoxPrinter2.Controls.Add(Me.TextBoxPrinter2SheetSelections)
        Me.GroupBoxPrinter2.Controls.Add(Me.LabelPrinter2SheetSelections)
        Me.GroupBoxPrinter2.Controls.Add(Me.RadioButtonSheetsAllPrinter2)
        Me.GroupBoxPrinter2.Controls.Add(Me.RadioButtonSheetsIsoPrinter2)
        Me.GroupBoxPrinter2.Controls.Add(Me.RadioButtonSheetsAnsiPrinter2)
        Me.GroupBoxPrinter2.Controls.Add(Me.CheckBoxEnablePrinter2)
        Me.GroupBoxPrinter2.Controls.Add(Me.TextBoxPrinter2Copies)
        Me.GroupBoxPrinter2.Controls.Add(Me.CheckBoxPrinter2AutoOrient)
        Me.GroupBoxPrinter2.Controls.Add(Me.CheckBoxPrinter2BestFit)
        Me.GroupBoxPrinter2.Controls.Add(Me.LabelPrinter2Copies)
        Me.GroupBoxPrinter2.Controls.Add(Me.CheckBoxPrinter2PrintAsBlack)
        Me.GroupBoxPrinter2.Controls.Add(Me.CheckBoxPrinter2ScaleLineTypes)
        Me.GroupBoxPrinter2.Controls.Add(Me.CheckBoxPrinter2ScaleLineWidths)
        Me.GroupBoxPrinter2.Controls.Add(Me.ComboBoxPrinter2)
        Me.GroupBoxPrinter2.Location = New System.Drawing.Point(10, 177)
        Me.GroupBoxPrinter2.Name = "GroupBoxPrinter2"
        Me.GroupBoxPrinter2.Size = New System.Drawing.Size(475, 228)
        Me.GroupBoxPrinter2.TabIndex = 37
        Me.GroupBoxPrinter2.TabStop = False
        Me.GroupBoxPrinter2.Text = "PRINTER 2"
        '
        'ButtonPrinter2SheetSelections
        '
        Me.ButtonPrinter2SheetSelections.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonPrinter2SheetSelections.Location = New System.Drawing.Point(393, 178)
        Me.ButtonPrinter2SheetSelections.Name = "ButtonPrinter2SheetSelections"
        Me.ButtonPrinter2SheetSelections.Size = New System.Drawing.Size(75, 23)
        Me.ButtonPrinter2SheetSelections.TabIndex = 58
        Me.ButtonPrinter2SheetSelections.Text = "Set"
        Me.ButtonPrinter2SheetSelections.UseVisualStyleBackColor = True
        '
        'TextBoxPrinter2SheetSelections
        '
        Me.TextBoxPrinter2SheetSelections.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPrinter2SheetSelections.Location = New System.Drawing.Point(4, 178)
        Me.TextBoxPrinter2SheetSelections.Name = "TextBoxPrinter2SheetSelections"
        Me.TextBoxPrinter2SheetSelections.Size = New System.Drawing.Size(383, 22)
        Me.TextBoxPrinter2SheetSelections.TabIndex = 57
        '
        'LabelPrinter2SheetSelections
        '
        Me.LabelPrinter2SheetSelections.AutoSize = True
        Me.LabelPrinter2SheetSelections.Location = New System.Drawing.Point(8, 127)
        Me.LabelPrinter2SheetSelections.Name = "LabelPrinter2SheetSelections"
        Me.LabelPrinter2SheetSelections.Size = New System.Drawing.Size(83, 15)
        Me.LabelPrinter2SheetSelections.TabIndex = 56
        Me.LabelPrinter2SheetSelections.Text = "Sheet selection"
        '
        'RadioButtonSheetsAllPrinter2
        '
        Me.RadioButtonSheetsAllPrinter2.AutoSize = True
        Me.RadioButtonSheetsAllPrinter2.Location = New System.Drawing.Point(120, 149)
        Me.RadioButtonSheetsAllPrinter2.Name = "RadioButtonSheetsAllPrinter2"
        Me.RadioButtonSheetsAllPrinter2.Size = New System.Drawing.Size(38, 19)
        Me.RadioButtonSheetsAllPrinter2.TabIndex = 54
        Me.RadioButtonSheetsAllPrinter2.TabStop = True
        Me.RadioButtonSheetsAllPrinter2.Text = "All"
        Me.RadioButtonSheetsAllPrinter2.UseVisualStyleBackColor = True
        '
        'RadioButtonSheetsIsoPrinter2
        '
        Me.RadioButtonSheetsIsoPrinter2.AutoSize = True
        Me.RadioButtonSheetsIsoPrinter2.Location = New System.Drawing.Point(65, 149)
        Me.RadioButtonSheetsIsoPrinter2.Name = "RadioButtonSheetsIsoPrinter2"
        Me.RadioButtonSheetsIsoPrinter2.Size = New System.Drawing.Size(39, 19)
        Me.RadioButtonSheetsIsoPrinter2.TabIndex = 52
        Me.RadioButtonSheetsIsoPrinter2.TabStop = True
        Me.RadioButtonSheetsIsoPrinter2.Text = "Iso"
        Me.RadioButtonSheetsIsoPrinter2.UseVisualStyleBackColor = True
        '
        'RadioButtonSheetsAnsiPrinter2
        '
        Me.RadioButtonSheetsAnsiPrinter2.AutoSize = True
        Me.RadioButtonSheetsAnsiPrinter2.Location = New System.Drawing.Point(10, 149)
        Me.RadioButtonSheetsAnsiPrinter2.Name = "RadioButtonSheetsAnsiPrinter2"
        Me.RadioButtonSheetsAnsiPrinter2.Size = New System.Drawing.Size(46, 19)
        Me.RadioButtonSheetsAnsiPrinter2.TabIndex = 51
        Me.RadioButtonSheetsAnsiPrinter2.TabStop = True
        Me.RadioButtonSheetsAnsiPrinter2.Text = "Ansi"
        Me.RadioButtonSheetsAnsiPrinter2.UseVisualStyleBackColor = True
        '
        'CheckBoxEnablePrinter2
        '
        Me.CheckBoxEnablePrinter2.AutoSize = True
        Me.CheckBoxEnablePrinter2.Location = New System.Drawing.Point(10, 25)
        Me.CheckBoxEnablePrinter2.Name = "CheckBoxEnablePrinter2"
        Me.CheckBoxEnablePrinter2.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxEnablePrinter2.TabIndex = 50
        Me.CheckBoxEnablePrinter2.UseVisualStyleBackColor = True
        '
        'TextBoxPrinter2Copies
        '
        Me.TextBoxPrinter2Copies.Location = New System.Drawing.Point(10, 58)
        Me.TextBoxPrinter2Copies.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPrinter2Copies.Name = "TextBoxPrinter2Copies"
        Me.TextBoxPrinter2Copies.Size = New System.Drawing.Size(35, 22)
        Me.TextBoxPrinter2Copies.TabIndex = 43
        '
        'CheckBoxPrinter2AutoOrient
        '
        Me.CheckBoxPrinter2AutoOrient.AutoSize = True
        Me.CheckBoxPrinter2AutoOrient.Location = New System.Drawing.Point(10, 92)
        Me.CheckBoxPrinter2AutoOrient.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrinter2AutoOrient.Name = "CheckBoxPrinter2AutoOrient"
        Me.CheckBoxPrinter2AutoOrient.Size = New System.Drawing.Size(81, 19)
        Me.CheckBoxPrinter2AutoOrient.TabIndex = 44
        Me.CheckBoxPrinter2AutoOrient.Text = "Auto orient"
        Me.CheckBoxPrinter2AutoOrient.UseVisualStyleBackColor = True
        '
        'CheckBoxPrinter2BestFit
        '
        Me.CheckBoxPrinter2BestFit.AutoSize = True
        Me.CheckBoxPrinter2BestFit.Location = New System.Drawing.Point(92, 92)
        Me.CheckBoxPrinter2BestFit.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrinter2BestFit.Name = "CheckBoxPrinter2BestFit"
        Me.CheckBoxPrinter2BestFit.Size = New System.Drawing.Size(60, 19)
        Me.CheckBoxPrinter2BestFit.TabIndex = 45
        Me.CheckBoxPrinter2BestFit.Text = "Best fit"
        Me.CheckBoxPrinter2BestFit.UseVisualStyleBackColor = True
        '
        'LabelPrinter2Copies
        '
        Me.LabelPrinter2Copies.AutoSize = True
        Me.LabelPrinter2Copies.Location = New System.Drawing.Point(49, 62)
        Me.LabelPrinter2Copies.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPrinter2Copies.Name = "LabelPrinter2Copies"
        Me.LabelPrinter2Copies.Size = New System.Drawing.Size(41, 15)
        Me.LabelPrinter2Copies.TabIndex = 49
        Me.LabelPrinter2Copies.Text = "Copies"
        '
        'CheckBoxPrinter2PrintAsBlack
        '
        Me.CheckBoxPrinter2PrintAsBlack.AutoSize = True
        Me.CheckBoxPrinter2PrintAsBlack.Location = New System.Drawing.Point(153, 92)
        Me.CheckBoxPrinter2PrintAsBlack.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrinter2PrintAsBlack.Name = "CheckBoxPrinter2PrintAsBlack"
        Me.CheckBoxPrinter2PrintAsBlack.Size = New System.Drawing.Size(90, 19)
        Me.CheckBoxPrinter2PrintAsBlack.TabIndex = 46
        Me.CheckBoxPrinter2PrintAsBlack.Text = "Print as black"
        Me.CheckBoxPrinter2PrintAsBlack.UseVisualStyleBackColor = True
        '
        'CheckBoxPrinter2ScaleLineTypes
        '
        Me.CheckBoxPrinter2ScaleLineTypes.AutoSize = True
        Me.CheckBoxPrinter2ScaleLineTypes.Location = New System.Drawing.Point(247, 92)
        Me.CheckBoxPrinter2ScaleLineTypes.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrinter2ScaleLineTypes.Name = "CheckBoxPrinter2ScaleLineTypes"
        Me.CheckBoxPrinter2ScaleLineTypes.Size = New System.Drawing.Size(103, 19)
        Me.CheckBoxPrinter2ScaleLineTypes.TabIndex = 47
        Me.CheckBoxPrinter2ScaleLineTypes.Text = "Scale line types"
        Me.CheckBoxPrinter2ScaleLineTypes.UseVisualStyleBackColor = True
        '
        'CheckBoxPrinter2ScaleLineWidths
        '
        Me.CheckBoxPrinter2ScaleLineWidths.AutoSize = True
        Me.CheckBoxPrinter2ScaleLineWidths.Location = New System.Drawing.Point(359, 92)
        Me.CheckBoxPrinter2ScaleLineWidths.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrinter2ScaleLineWidths.Name = "CheckBoxPrinter2ScaleLineWidths"
        Me.CheckBoxPrinter2ScaleLineWidths.Size = New System.Drawing.Size(108, 19)
        Me.CheckBoxPrinter2ScaleLineWidths.TabIndex = 48
        Me.CheckBoxPrinter2ScaleLineWidths.Text = "Scale line widths"
        Me.CheckBoxPrinter2ScaleLineWidths.UseVisualStyleBackColor = True
        '
        'ComboBoxPrinter2
        '
        Me.ComboBoxPrinter2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxPrinter2.FormattingEnabled = True
        Me.ComboBoxPrinter2.Location = New System.Drawing.Point(35, 23)
        Me.ComboBoxPrinter2.Name = "ComboBoxPrinter2"
        Me.ComboBoxPrinter2.Size = New System.Drawing.Size(427, 23)
        Me.ComboBoxPrinter2.TabIndex = 42
        '
        'GroupBoxPrinter1
        '
        Me.GroupBoxPrinter1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxPrinter1.Controls.Add(Me.CheckBoxEnablePrinter1)
        Me.GroupBoxPrinter1.Controls.Add(Me.TextBoxPrinter1Copies)
        Me.GroupBoxPrinter1.Controls.Add(Me.CheckBoxPrinter1AutoOrient)
        Me.GroupBoxPrinter1.Controls.Add(Me.CheckBoxPrinter1BestFit)
        Me.GroupBoxPrinter1.Controls.Add(Me.LabelPrinter1Copies)
        Me.GroupBoxPrinter1.Controls.Add(Me.CheckBoxPrinter1PrintAsBlack)
        Me.GroupBoxPrinter1.Controls.Add(Me.CheckBoxPrinter1ScaleLineTypes)
        Me.GroupBoxPrinter1.Controls.Add(Me.CheckBoxPrinter1ScaleLineWidths)
        Me.GroupBoxPrinter1.Controls.Add(Me.ComboBoxPrinter1)
        Me.GroupBoxPrinter1.Location = New System.Drawing.Point(10, 12)
        Me.GroupBoxPrinter1.Name = "GroupBoxPrinter1"
        Me.GroupBoxPrinter1.Size = New System.Drawing.Size(475, 144)
        Me.GroupBoxPrinter1.TabIndex = 36
        Me.GroupBoxPrinter1.TabStop = False
        Me.GroupBoxPrinter1.Text = "PRINTER 1"
        '
        'CheckBoxEnablePrinter1
        '
        Me.CheckBoxEnablePrinter1.AutoSize = True
        Me.CheckBoxEnablePrinter1.Location = New System.Drawing.Point(10, 25)
        Me.CheckBoxEnablePrinter1.Name = "CheckBoxEnablePrinter1"
        Me.CheckBoxEnablePrinter1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxEnablePrinter1.TabIndex = 42
        Me.CheckBoxEnablePrinter1.UseVisualStyleBackColor = True
        '
        'TextBoxPrinter1Copies
        '
        Me.TextBoxPrinter1Copies.Location = New System.Drawing.Point(10, 58)
        Me.TextBoxPrinter1Copies.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPrinter1Copies.Name = "TextBoxPrinter1Copies"
        Me.TextBoxPrinter1Copies.Size = New System.Drawing.Size(35, 22)
        Me.TextBoxPrinter1Copies.TabIndex = 34
        '
        'CheckBoxPrinter1AutoOrient
        '
        Me.CheckBoxPrinter1AutoOrient.AutoSize = True
        Me.CheckBoxPrinter1AutoOrient.Location = New System.Drawing.Point(10, 92)
        Me.CheckBoxPrinter1AutoOrient.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrinter1AutoOrient.Name = "CheckBoxPrinter1AutoOrient"
        Me.CheckBoxPrinter1AutoOrient.Size = New System.Drawing.Size(81, 19)
        Me.CheckBoxPrinter1AutoOrient.TabIndex = 35
        Me.CheckBoxPrinter1AutoOrient.Text = "Auto orient"
        Me.CheckBoxPrinter1AutoOrient.UseVisualStyleBackColor = True
        '
        'CheckBoxPrinter1BestFit
        '
        Me.CheckBoxPrinter1BestFit.AutoSize = True
        Me.CheckBoxPrinter1BestFit.Location = New System.Drawing.Point(92, 92)
        Me.CheckBoxPrinter1BestFit.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrinter1BestFit.Name = "CheckBoxPrinter1BestFit"
        Me.CheckBoxPrinter1BestFit.Size = New System.Drawing.Size(60, 19)
        Me.CheckBoxPrinter1BestFit.TabIndex = 36
        Me.CheckBoxPrinter1BestFit.Text = "Best fit"
        Me.CheckBoxPrinter1BestFit.UseVisualStyleBackColor = True
        '
        'LabelPrinter1Copies
        '
        Me.LabelPrinter1Copies.AutoSize = True
        Me.LabelPrinter1Copies.Location = New System.Drawing.Point(49, 62)
        Me.LabelPrinter1Copies.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPrinter1Copies.Name = "LabelPrinter1Copies"
        Me.LabelPrinter1Copies.Size = New System.Drawing.Size(41, 15)
        Me.LabelPrinter1Copies.TabIndex = 41
        Me.LabelPrinter1Copies.Text = "Copies"
        '
        'CheckBoxPrinter1PrintAsBlack
        '
        Me.CheckBoxPrinter1PrintAsBlack.AutoSize = True
        Me.CheckBoxPrinter1PrintAsBlack.Location = New System.Drawing.Point(153, 92)
        Me.CheckBoxPrinter1PrintAsBlack.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrinter1PrintAsBlack.Name = "CheckBoxPrinter1PrintAsBlack"
        Me.CheckBoxPrinter1PrintAsBlack.Size = New System.Drawing.Size(90, 19)
        Me.CheckBoxPrinter1PrintAsBlack.TabIndex = 37
        Me.CheckBoxPrinter1PrintAsBlack.Text = "Print as black"
        Me.CheckBoxPrinter1PrintAsBlack.UseVisualStyleBackColor = True
        '
        'CheckBoxPrinter1ScaleLineTypes
        '
        Me.CheckBoxPrinter1ScaleLineTypes.AutoSize = True
        Me.CheckBoxPrinter1ScaleLineTypes.Location = New System.Drawing.Point(247, 92)
        Me.CheckBoxPrinter1ScaleLineTypes.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrinter1ScaleLineTypes.Name = "CheckBoxPrinter1ScaleLineTypes"
        Me.CheckBoxPrinter1ScaleLineTypes.Size = New System.Drawing.Size(103, 19)
        Me.CheckBoxPrinter1ScaleLineTypes.TabIndex = 38
        Me.CheckBoxPrinter1ScaleLineTypes.Text = "Scale line types"
        Me.CheckBoxPrinter1ScaleLineTypes.UseVisualStyleBackColor = True
        '
        'CheckBoxPrinter1ScaleLineWidths
        '
        Me.CheckBoxPrinter1ScaleLineWidths.AutoSize = True
        Me.CheckBoxPrinter1ScaleLineWidths.Location = New System.Drawing.Point(359, 92)
        Me.CheckBoxPrinter1ScaleLineWidths.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPrinter1ScaleLineWidths.Name = "CheckBoxPrinter1ScaleLineWidths"
        Me.CheckBoxPrinter1ScaleLineWidths.Size = New System.Drawing.Size(108, 19)
        Me.CheckBoxPrinter1ScaleLineWidths.TabIndex = 39
        Me.CheckBoxPrinter1ScaleLineWidths.Text = "Scale line widths"
        Me.CheckBoxPrinter1ScaleLineWidths.UseVisualStyleBackColor = True
        '
        'ComboBoxPrinter1
        '
        Me.ComboBoxPrinter1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxPrinter1.FormattingEnabled = True
        Me.ComboBoxPrinter1.Location = New System.Drawing.Point(35, 23)
        Me.ComboBoxPrinter1.Name = "ComboBoxPrinter1"
        Me.ComboBoxPrinter1.Size = New System.Drawing.Size(427, 23)
        Me.ComboBoxPrinter1.TabIndex = 0
        '
        'TabPageGeneral
        '
        Me.TabPageGeneral.Controls.Add(Me.LabelFontSize)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxFontSize)
        Me.TabPageGeneral.Controls.Add(Me.GroupBoxPictorialViews)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxPropertyFilterFollowDraftLinks)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxAutoAddMissingProperty)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxPartCopiesRecursiveSearch)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxMoveDrawingViewAllowPartialSuccess)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxBackgroundProcessing)
        Me.TabPageGeneral.Controls.Add(Me.CheckBoxRememberTasks)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxPartNumberPropertyName)
        Me.TabPageGeneral.Controls.Add(Me.LabelPartNumberPropertyName)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxRestartAfter)
        Me.TabPageGeneral.Controls.Add(Me.ComboBoxPartNumberPropertySet)
        Me.TabPageGeneral.Controls.Add(Me.LabelPartNumberPropertySet)
        Me.TabPageGeneral.Controls.Add(Me.LabelRestartAfter)
        Me.TabPageGeneral.ImageKey = "config"
        Me.TabPageGeneral.Location = New System.Drawing.Point(4, 24)
        Me.TabPageGeneral.Name = "TabPageGeneral"
        Me.TabPageGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGeneral.Size = New System.Drawing.Size(495, 648)
        Me.TabPageGeneral.TabIndex = 6
        Me.TabPageGeneral.Text = "General"
        Me.TabPageGeneral.UseVisualStyleBackColor = True
        '
        'LabelFontSize
        '
        Me.LabelFontSize.AutoSize = True
        Me.LabelFontSize.Location = New System.Drawing.Point(62, 100)
        Me.LabelFontSize.Name = "LabelFontSize"
        Me.LabelFontSize.Size = New System.Drawing.Size(86, 15)
        Me.LabelFontSize.TabIndex = 55
        Me.LabelFontSize.Text = "File list font size"
        '
        'TextBoxFontSize
        '
        Me.TextBoxFontSize.Location = New System.Drawing.Point(12, 96)
        Me.TextBoxFontSize.Name = "TextBoxFontSize"
        Me.TextBoxFontSize.Size = New System.Drawing.Size(38, 22)
        Me.TextBoxFontSize.TabIndex = 54
        Me.TextBoxFontSize.Text = "8"
        '
        'GroupBoxPictorialViews
        '
        Me.GroupBoxPictorialViews.Controls.Add(Me.RadioButtonPictorialViewTrimetric)
        Me.GroupBoxPictorialViews.Controls.Add(Me.RadioButtonPictorialViewDimetric)
        Me.GroupBoxPictorialViews.Controls.Add(Me.RadioButtonPictorialViewIsometric)
        Me.GroupBoxPictorialViews.Location = New System.Drawing.Point(2, 337)
        Me.GroupBoxPictorialViews.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBoxPictorialViews.Name = "GroupBoxPictorialViews"
        Me.GroupBoxPictorialViews.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBoxPictorialViews.Size = New System.Drawing.Size(475, 115)
        Me.GroupBoxPictorialViews.TabIndex = 53
        Me.GroupBoxPictorialViews.TabStop = False
        Me.GroupBoxPictorialViews.Text = "PICTORIAL VIEW OPTIONS"
        '
        'RadioButtonPictorialViewTrimetric
        '
        Me.RadioButtonPictorialViewTrimetric.AutoSize = True
        Me.RadioButtonPictorialViewTrimetric.Location = New System.Drawing.Point(8, 70)
        Me.RadioButtonPictorialViewTrimetric.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonPictorialViewTrimetric.Name = "RadioButtonPictorialViewTrimetric"
        Me.RadioButtonPictorialViewTrimetric.Size = New System.Drawing.Size(69, 19)
        Me.RadioButtonPictorialViewTrimetric.TabIndex = 2
        Me.RadioButtonPictorialViewTrimetric.TabStop = True
        Me.RadioButtonPictorialViewTrimetric.Text = "Trimetric"
        Me.RadioButtonPictorialViewTrimetric.UseVisualStyleBackColor = True
        '
        'RadioButtonPictorialViewDimetric
        '
        Me.RadioButtonPictorialViewDimetric.AutoSize = True
        Me.RadioButtonPictorialViewDimetric.Location = New System.Drawing.Point(8, 47)
        Me.RadioButtonPictorialViewDimetric.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonPictorialViewDimetric.Name = "RadioButtonPictorialViewDimetric"
        Me.RadioButtonPictorialViewDimetric.Size = New System.Drawing.Size(68, 19)
        Me.RadioButtonPictorialViewDimetric.TabIndex = 1
        Me.RadioButtonPictorialViewDimetric.TabStop = True
        Me.RadioButtonPictorialViewDimetric.Text = "Dimetric"
        Me.RadioButtonPictorialViewDimetric.UseVisualStyleBackColor = True
        '
        'RadioButtonPictorialViewIsometric
        '
        Me.RadioButtonPictorialViewIsometric.AutoSize = True
        Me.RadioButtonPictorialViewIsometric.Location = New System.Drawing.Point(8, 23)
        Me.RadioButtonPictorialViewIsometric.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonPictorialViewIsometric.Name = "RadioButtonPictorialViewIsometric"
        Me.RadioButtonPictorialViewIsometric.Size = New System.Drawing.Size(71, 19)
        Me.RadioButtonPictorialViewIsometric.TabIndex = 0
        Me.RadioButtonPictorialViewIsometric.TabStop = True
        Me.RadioButtonPictorialViewIsometric.Text = "Isometric"
        Me.RadioButtonPictorialViewIsometric.UseVisualStyleBackColor = True
        '
        'CheckBoxPropertyFilterFollowDraftLinks
        '
        Me.CheckBoxPropertyFilterFollowDraftLinks.AutoSize = True
        Me.CheckBoxPropertyFilterFollowDraftLinks.Checked = True
        Me.CheckBoxPropertyFilterFollowDraftLinks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxPropertyFilterFollowDraftLinks.Location = New System.Drawing.Point(12, 286)
        Me.CheckBoxPropertyFilterFollowDraftLinks.Name = "CheckBoxPropertyFilterFollowDraftLinks"
        Me.CheckBoxPropertyFilterFollowDraftLinks.Size = New System.Drawing.Size(328, 19)
        Me.CheckBoxPropertyFilterFollowDraftLinks.TabIndex = 52
        Me.CheckBoxPropertyFilterFollowDraftLinks.Text = "Property Filter -- Include Draft file model documents in search"
        Me.CheckBoxPropertyFilterFollowDraftLinks.UseVisualStyleBackColor = True
        '
        'CheckBoxAutoAddMissingProperty
        '
        Me.CheckBoxAutoAddMissingProperty.AutoSize = True
        Me.CheckBoxAutoAddMissingProperty.Location = New System.Drawing.Point(12, 257)
        Me.CheckBoxAutoAddMissingProperty.Name = "CheckBoxAutoAddMissingProperty"
        Me.CheckBoxAutoAddMissingProperty.Size = New System.Drawing.Size(276, 19)
        Me.CheckBoxAutoAddMissingProperty.TabIndex = 51
        Me.CheckBoxAutoAddMissingProperty.Text = "Property Find/Replace -- Auto add missing property"
        Me.CheckBoxAutoAddMissingProperty.UseVisualStyleBackColor = True
        '
        'CheckBoxPartCopiesRecursiveSearch
        '
        Me.CheckBoxPartCopiesRecursiveSearch.AutoSize = True
        Me.CheckBoxPartCopiesRecursiveSearch.Location = New System.Drawing.Point(12, 228)
        Me.CheckBoxPartCopiesRecursiveSearch.Name = "CheckBoxPartCopiesRecursiveSearch"
        Me.CheckBoxPartCopiesRecursiveSearch.Size = New System.Drawing.Size(244, 19)
        Me.CheckBoxPartCopiesRecursiveSearch.TabIndex = 50
        Me.CheckBoxPartCopiesRecursiveSearch.Text = "Update insert part copies -- Recursive search"
        Me.CheckBoxPartCopiesRecursiveSearch.UseVisualStyleBackColor = True
        '
        'CheckBoxMoveDrawingViewAllowPartialSuccess
        '
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.AutoSize = True
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Location = New System.Drawing.Point(12, 200)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Name = "CheckBoxMoveDrawingViewAllowPartialSuccess"
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Size = New System.Drawing.Size(279, 19)
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.TabIndex = 47
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.Text = "Update styles from template -- Allow partial success"
        Me.CheckBoxMoveDrawingViewAllowPartialSuccess.UseVisualStyleBackColor = True
        '
        'CheckBoxBackgroundProcessing
        '
        Me.CheckBoxBackgroundProcessing.AutoSize = True
        Me.CheckBoxBackgroundProcessing.Location = New System.Drawing.Point(12, 171)
        Me.CheckBoxBackgroundProcessing.Name = "CheckBoxBackgroundProcessing"
        Me.CheckBoxBackgroundProcessing.Size = New System.Drawing.Size(227, 19)
        Me.CheckBoxBackgroundProcessing.TabIndex = 49
        Me.CheckBoxBackgroundProcessing.Text = "Process tasks in background (no graphics)"
        Me.CheckBoxBackgroundProcessing.UseVisualStyleBackColor = True
        '
        'CheckBoxRememberTasks
        '
        Me.CheckBoxRememberTasks.AutoSize = True
        Me.CheckBoxRememberTasks.Checked = True
        Me.CheckBoxRememberTasks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRememberTasks.Location = New System.Drawing.Point(12, 142)
        Me.CheckBoxRememberTasks.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxRememberTasks.Name = "CheckBoxRememberTasks"
        Me.CheckBoxRememberTasks.Size = New System.Drawing.Size(239, 19)
        Me.CheckBoxRememberTasks.TabIndex = 48
        Me.CheckBoxRememberTasks.Text = "Remember selected tasks between sessions"
        Me.CheckBoxRememberTasks.UseVisualStyleBackColor = True
        '
        'TextBoxPartNumberPropertyName
        '
        Me.TextBoxPartNumberPropertyName.Location = New System.Drawing.Point(176, 28)
        Me.TextBoxPartNumberPropertyName.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPartNumberPropertyName.Name = "TextBoxPartNumberPropertyName"
        Me.TextBoxPartNumberPropertyName.Size = New System.Drawing.Size(200, 22)
        Me.TextBoxPartNumberPropertyName.TabIndex = 44
        '
        'LabelPartNumberPropertyName
        '
        Me.LabelPartNumberPropertyName.AutoSize = True
        Me.LabelPartNumberPropertyName.Location = New System.Drawing.Point(176, 9)
        Me.LabelPartNumberPropertyName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPartNumberPropertyName.Name = "LabelPartNumberPropertyName"
        Me.LabelPartNumberPropertyName.Size = New System.Drawing.Size(144, 15)
        Me.LabelPartNumberPropertyName.TabIndex = 43
        Me.LabelPartNumberPropertyName.Text = "Part Number Property Name"
        '
        'TextBoxRestartAfter
        '
        Me.TextBoxRestartAfter.Location = New System.Drawing.Point(12, 67)
        Me.TextBoxRestartAfter.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxRestartAfter.Name = "TextBoxRestartAfter"
        Me.TextBoxRestartAfter.Size = New System.Drawing.Size(38, 22)
        Me.TextBoxRestartAfter.TabIndex = 46
        Me.TextBoxRestartAfter.Text = "500"
        '
        'ComboBoxPartNumberPropertySet
        '
        Me.ComboBoxPartNumberPropertySet.FormattingEnabled = True
        Me.ComboBoxPartNumberPropertySet.Items.AddRange(New Object() {"fake_item_1"})
        Me.ComboBoxPartNumberPropertySet.Location = New System.Drawing.Point(12, 28)
        Me.ComboBoxPartNumberPropertySet.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxPartNumberPropertySet.Name = "ComboBoxPartNumberPropertySet"
        Me.ComboBoxPartNumberPropertySet.Size = New System.Drawing.Size(132, 23)
        Me.ComboBoxPartNumberPropertySet.Sorted = True
        Me.ComboBoxPartNumberPropertySet.TabIndex = 42
        '
        'LabelPartNumberPropertySet
        '
        Me.LabelPartNumberPropertySet.AutoSize = True
        Me.LabelPartNumberPropertySet.Location = New System.Drawing.Point(12, 9)
        Me.LabelPartNumberPropertySet.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPartNumberPropertySet.Name = "LabelPartNumberPropertySet"
        Me.LabelPartNumberPropertySet.Size = New System.Drawing.Size(132, 15)
        Me.LabelPartNumberPropertySet.TabIndex = 41
        Me.LabelPartNumberPropertySet.Text = "Part Number Property Set"
        '
        'LabelRestartAfter
        '
        Me.LabelRestartAfter.AutoSize = True
        Me.LabelRestartAfter.Location = New System.Drawing.Point(62, 72)
        Me.LabelRestartAfter.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelRestartAfter.Name = "LabelRestartAfter"
        Me.LabelRestartAfter.Size = New System.Drawing.Size(199, 15)
        Me.LabelRestartAfter.TabIndex = 45
        Me.LabelRestartAfter.Text = "Restart After This Many Files Processed"
        '
        'TabPageHelp
        '
        Me.TabPageHelp.AutoScroll = True
        Me.TabPageHelp.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPageHelp.Controls.Add(Me.LabelReadmeNavigation2)
        Me.TabPageHelp.Controls.Add(Me.PictureBoxTableOfContents)
        Me.TabPageHelp.Controls.Add(Me.LabelReadmeNavigation1)
        Me.TabPageHelp.Controls.Add(Me.LinkLabelGitHubReadme)
        Me.TabPageHelp.ImageKey = "Help"
        Me.TabPageHelp.Location = New System.Drawing.Point(4, 24)
        Me.TabPageHelp.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageHelp.Name = "TabPageHelp"
        Me.TabPageHelp.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageHelp.Size = New System.Drawing.Size(511, 690)
        Me.TabPageHelp.TabIndex = 6
        Me.TabPageHelp.Text = "Help"
        '
        'LabelReadmeNavigation2
        '
        Me.LabelReadmeNavigation2.AutoSize = True
        Me.LabelReadmeNavigation2.Location = New System.Drawing.Point(15, 98)
        Me.LabelReadmeNavigation2.Name = "LabelReadmeNavigation2"
        Me.LabelReadmeNavigation2.Size = New System.Drawing.Size(148, 15)
        Me.LabelReadmeNavigation2.TabIndex = 6
        Me.LabelReadmeNavigation2.Text = "as shown in the image below."
        '
        'PictureBoxTableOfContents
        '
        Me.PictureBoxTableOfContents.Image = Global.Housekeeper.My.Resources.Resources.table_of_contents
        Me.PictureBoxTableOfContents.Location = New System.Drawing.Point(15, 133)
        Me.PictureBoxTableOfContents.Name = "PictureBoxTableOfContents"
        Me.PictureBoxTableOfContents.Size = New System.Drawing.Size(312, 324)
        Me.PictureBoxTableOfContents.TabIndex = 5
        Me.PictureBoxTableOfContents.TabStop = False
        '
        'LabelReadmeNavigation1
        '
        Me.LabelReadmeNavigation1.AutoSize = True
        Me.LabelReadmeNavigation1.Location = New System.Drawing.Point(15, 81)
        Me.LabelReadmeNavigation1.Name = "LabelReadmeNavigation1"
        Me.LabelReadmeNavigation1.Size = New System.Drawing.Size(393, 15)
        Me.LabelReadmeNavigation1.TabIndex = 2
        Me.LabelReadmeNavigation1.Text = "Access the Table of Contents on GitHub by clicking the icon left of README.md"
        '
        'LinkLabelGitHubReadme
        '
        Me.LinkLabelGitHubReadme.AutoSize = True
        Me.LinkLabelGitHubReadme.Location = New System.Drawing.Point(15, 29)
        Me.LinkLabelGitHubReadme.Name = "LinkLabelGitHubReadme"
        Me.LinkLabelGitHubReadme.Size = New System.Drawing.Size(163, 15)
        Me.LinkLabelGitHubReadme.TabIndex = 1
        Me.LinkLabelGitHubReadme.TabStop = True
        Me.LinkLabelGitHubReadme.Text = "text populated in Form1.Startup()"
        '
        'TextBoxStatus
        '
        Me.TextBoxStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStatus.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel1.SetColumnSpan(Me.TextBoxStatus, 3)
        Me.TextBoxStatus.Location = New System.Drawing.Point(2, 4)
        Me.TextBoxStatus.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxStatus.Name = "TextBoxStatus"
        Me.TextBoxStatus.Size = New System.Drawing.Size(515, 22)
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
        Me.LabelTimeRemaining.Location = New System.Drawing.Point(10, 757)
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
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonProcess, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonCancel, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxStatus, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 718)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(519, 63)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'ButtonProcess
        '
        Me.ButtonProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonProcess.Image = Global.Housekeeper.My.Resources.Resources.Play
        Me.ButtonProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonProcess.Location = New System.Drawing.Point(321, 30)
        Me.ButtonProcess.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonProcess.Name = "ButtonProcess"
        Me.ButtonProcess.Size = New System.Drawing.Size(96, 31)
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
        Me.ButtonCancel.Location = New System.Drawing.Point(421, 30)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(96, 31)
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
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(519, 781)
        Me.Controls.Add(Me.LabelTimeRemaining)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI Variable Display", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(400, 200)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(535, 819)
        Me.Name = "Form1"
        Me.Text = "Solid Edge Housekeeper"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageHome.ResumeLayout(False)
        Me.TabPageHome.PerformLayout()
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
        Me.TabControl2.ResumeLayout(False)
        Me.TabPageTopLevelAssy.ResumeLayout(False)
        Me.TabPageTopLevelAssy.PerformLayout()
        Me.TabPageSorting.ResumeLayout(False)
        Me.TabPageSorting.PerformLayout()
        Me.TabPageOpenSave.ResumeLayout(False)
        Me.TabPageOpenSave.PerformLayout()
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
        Me.TabPageTemplates.ResumeLayout(False)
        Me.TabPageTemplates.PerformLayout()
        Me.TabPagePrinting.ResumeLayout(False)
        Me.GroupBoxPrinter2.ResumeLayout(False)
        Me.GroupBoxPrinter2.PerformLayout()
        Me.GroupBoxPrinter1.ResumeLayout(False)
        Me.GroupBoxPrinter1.PerformLayout()
        Me.TabPageGeneral.ResumeLayout(False)
        Me.TabPageGeneral.PerformLayout()
        Me.GroupBoxPictorialViews.ResumeLayout(False)
        Me.GroupBoxPictorialViews.PerformLayout()
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
    Friend WithEvents TabPageAssembly As TabPage
    Friend WithEvents TabPagePart As TabPage
    Friend WithEvents TabPageSheetmetal As TabPage
    Friend WithEvents TabPageDraft As TabPage
    Friend WithEvents TextBoxStatus As TextBox
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonProcess As Button
    Friend WithEvents CheckedListBoxAssembly As CheckedListBox
    Friend WithEvents CheckedListBoxPart As CheckedListBox
    Friend WithEvents CheckedListBoxSheetmetal As CheckedListBox
    Friend WithEvents CheckedListBoxDraft As CheckedListBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TabPageConfiguration As TabPage
    Friend WithEvents TabPageHelp As TabPage
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
    Friend WithEvents TextBoxWatermarkScale As TextBox
    Friend WithEvents LabelWatermarkScale As Label
    Friend WithEvents TextBoxWatermarkY As TextBox
    Friend WithEvents LabelWatermarkY As Label
    Friend WithEvents TextBoxWatermarkX As TextBox
    Friend WithEvents LabelWatermarkX As Label
    Friend WithEvents CheckBoxWatermark As CheckBox
    Friend WithEvents ButtonWatermark As Button
    Friend WithEvents TextBoxWatermarkFilename As TextBox
    Friend WithEvents TextBoxExposeVariablesAssembly As TextBox
    Friend WithEvents LabelExposeVariablesAssembly As Label
    Friend WithEvents TextBoxExposeVariablesPart As TextBox
    Friend WithEvents LabelExposeVariablesPart As Label
    Friend WithEvents TextBoxExposeVariablesSheetmetal As TextBox
    Friend WithEvents LabelExposeVariablesSheetmetal As Label
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
    Friend WithEvents BT_FindLinks As ToolStripMenuItem
    Friend WithEvents LinkLabelGitHubReadme As LinkLabel
    Friend WithEvents LabelReadmeNavigation1 As Label
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents PictureBoxTableOfContents As PictureBox
    Friend WithEvents LabelReadmeNavigation2 As Label
    Friend WithEvents CheckBoxFindReplaceFindRXAssembly As CheckBox
    Friend WithEvents CheckBoxFindReplaceFindWCAssembly As CheckBox
    Friend WithEvents CheckBoxFindReplaceFindPTAssembly As CheckBox
    Friend WithEvents CheckBoxFindReplaceFindRXPart As CheckBox
    Friend WithEvents CheckBoxFindReplaceFindWCPart As CheckBox
    Friend WithEvents CheckBoxFindReplaceFindPTPart As CheckBox
    Friend WithEvents CheckBoxFindReplaceFindRXSheetmetal As CheckBox
    Friend WithEvents CheckBoxFindReplaceFindWCSheetmetal As CheckBox
    Friend WithEvents CheckBoxFindReplaceFindPTSheetmetal As CheckBox
    Friend WithEvents CheckBoxFindReplaceReplaceRXAssembly As CheckBox
    Friend WithEvents CheckBoxFindReplaceReplacePTAssembly As CheckBox
    Friend WithEvents CheckBoxFindReplaceReplaceRXPart As CheckBox
    Friend WithEvents CheckBoxFindReplaceReplacePTPart As CheckBox
    Friend WithEvents CheckBoxFindReplaceReplaceRXSheetmetal As CheckBox
    Friend WithEvents CheckBoxFindReplaceReplacePTSheetmetal As CheckBox
    Friend WithEvents CheckBoxFindReplaceReplaceRXDraft As CheckBox
    Friend WithEvents CheckBoxFindReplaceReplacePTDraft As CheckBox
    Friend WithEvents CheckBoxFindReplaceFindRXDraft As CheckBox
    Friend WithEvents CheckBoxFindReplaceFindWCDraft As CheckBox
    Friend WithEvents CheckBoxFindReplaceFindPTDraft As CheckBox
    Friend WithEvents TextBoxFindReplaceReplaceDraft As TextBox
    Friend WithEvents TextBoxFindReplaceFindDraft As TextBox
    Friend WithEvents TextBoxFindReplacePropertyNameDraft As TextBox
    Friend WithEvents ComboBoxFindReplacePropertySetDraft As ComboBox
    Friend WithEvents LabelFindReplaceReplaceDraft As Label
    Friend WithEvents LabelFindReplaceFindDraft As Label
    Friend WithEvents LabelFindReplacePropertyNameDraft As Label
    Friend WithEvents LabelFindReplacePropertySetDraft As Label
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPageTemplates As TabPage
    Friend WithEvents ButtonActiveMaterialLibrary As Button
    Friend WithEvents TextBoxActiveMaterialLibrary As TextBox
    Friend WithEvents LabelActiveMaterialLibrary As Label
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
    Friend WithEvents TabPageOpenSave As TabPage
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
    Friend WithEvents CheckBoxSaveAsPDFPerSheetSupress As CheckBox
    Friend WithEvents CheckBoxSaveAsImageCrop As CheckBox
    Friend WithEvents CheckBoxRunExternalProgramSaveFile As CheckBox
    Friend WithEvents CheckBoxNoUpdateMRU As CheckBox
    Friend WithEvents CheckBoxWarnSave As CheckBox
    Friend WithEvents TabPageSorting As TabPage
    Friend WithEvents TextBoxRandomSampleFraction As TextBox
    Friend WithEvents LabelRandomSampleFraction As Label
    Friend WithEvents RadioButtonListSortRandomSample As RadioButton
    Friend WithEvents CheckBoxListIncludeNoDependencies As CheckBox
    Friend WithEvents RadioButtonListSortDependency As RadioButton
    Friend WithEvents RadioButtonListSortAlphabetical As RadioButton
    Friend WithEvents RadioButtonListSortNone As RadioButton
    Friend WithEvents TabPagePrinting As TabPage
    Friend WithEvents GroupBoxPrinter2 As GroupBox
    Friend WithEvents ButtonPrinter2SheetSelections As Button
    Friend WithEvents TextBoxPrinter2SheetSelections As TextBox
    Friend WithEvents LabelPrinter2SheetSelections As Label
    Friend WithEvents RadioButtonSheetsAllPrinter2 As RadioButton
    Friend WithEvents RadioButtonSheetsIsoPrinter2 As RadioButton
    Friend WithEvents RadioButtonSheetsAnsiPrinter2 As RadioButton
    Friend WithEvents CheckBoxEnablePrinter2 As CheckBox
    Friend WithEvents TextBoxPrinter2Copies As TextBox
    Friend WithEvents CheckBoxPrinter2AutoOrient As CheckBox
    Friend WithEvents CheckBoxPrinter2BestFit As CheckBox
    Friend WithEvents LabelPrinter2Copies As Label
    Friend WithEvents CheckBoxPrinter2PrintAsBlack As CheckBox
    Friend WithEvents CheckBoxPrinter2ScaleLineTypes As CheckBox
    Friend WithEvents CheckBoxPrinter2ScaleLineWidths As CheckBox
    Friend WithEvents ComboBoxPrinter2 As ComboBox
    Friend WithEvents GroupBoxPrinter1 As GroupBox
    Friend WithEvents TextBoxPrinter1Copies As TextBox
    Friend WithEvents CheckBoxPrinter1AutoOrient As CheckBox
    Friend WithEvents CheckBoxPrinter1BestFit As CheckBox
    Friend WithEvents LabelPrinter1Copies As Label
    Friend WithEvents CheckBoxPrinter1PrintAsBlack As CheckBox
    Friend WithEvents CheckBoxPrinter1ScaleLineTypes As CheckBox
    Friend WithEvents CheckBoxPrinter1ScaleLineWidths As CheckBox
    Friend WithEvents ComboBoxPrinter1 As ComboBox
    Friend WithEvents TabPageGeneral As TabPage
    Friend WithEvents GroupBoxPictorialViews As GroupBox
    Friend WithEvents RadioButtonPictorialViewTrimetric As RadioButton
    Friend WithEvents RadioButtonPictorialViewDimetric As RadioButton
    Friend WithEvents RadioButtonPictorialViewIsometric As RadioButton
    Friend WithEvents CheckBoxPropertyFilterFollowDraftLinks As CheckBox
    Friend WithEvents CheckBoxAutoAddMissingProperty As CheckBox
    Friend WithEvents CheckBoxPartCopiesRecursiveSearch As CheckBox
    Friend WithEvents CheckBoxMoveDrawingViewAllowPartialSuccess As CheckBox
    Friend WithEvents CheckBoxBackgroundProcessing As CheckBox
    Friend WithEvents CheckBoxRememberTasks As CheckBox
    Friend WithEvents TextBoxPartNumberPropertyName As TextBox
    Friend WithEvents LabelPartNumberPropertyName As Label
    Friend WithEvents TextBoxRestartAfter As TextBox
    Friend WithEvents ComboBoxPartNumberPropertySet As ComboBox
    Friend WithEvents LabelPartNumberPropertySet As Label
    Friend WithEvents LabelRestartAfter As Label
    Friend WithEvents TextBoxFontSize As TextBox
    Friend WithEvents LabelFontSize As Label
    Friend WithEvents CheckBoxEnablePrinter1 As CheckBox
    Friend WithEvents ButtonVariableEditPart As Button
    Friend WithEvents TextBoxVariableEditPart As TextBox
End Class
