<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormTeamCenterAdd
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormTeamCenterAdd))
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.ButtonAddAndClose = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.ExTableLayoutPanel1 = New Housekeeper.ExTableLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ListViewDownloadedFiles = New System.Windows.Forms.ListView()
        Me.fileNam = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.filePath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonDownloadAll = New System.Windows.Forms.Button()
        Me.ListViewTeamCenterItems = New System.Windows.Forms.ListView()
        Me.fileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.itemID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.revision = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.fileType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DataGridViewItems = New System.Windows.Forms.DataGridView()
        Me.ItemIDs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rev = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ButtonDownload = New System.Windows.Forms.Button()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.ButtonSearchAndAdd = New System.Windows.Forms.Button()
        Me.ToolStrip_List = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.CheckBoxAsm = New System.Windows.Forms.ToolStripButton()
        Me.CheckBoxPar = New System.Windows.Forms.ToolStripButton()
        Me.CheckBoxPsm = New System.Windows.Forms.ToolStripButton()
        Me.CheckBoxDft = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ButtonSettings = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonHelp = New System.Windows.Forms.ToolStripButton()
        Me.ExTableLayoutPanel2 = New Housekeeper.ExTableLayoutPanel()
        Me.ExTableLayoutPanel3 = New Housekeeper.ExTableLayoutPanel()
        Me.ExTableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridViewItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip_List.SuspendLayout()
        Me.ExTableLayoutPanel2.SuspendLayout()
        Me.ExTableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelStatus
        '
        Me.LabelStatus.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Location = New System.Drawing.Point(4, 9)
        Me.LabelStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(474, 15)
        Me.LabelStatus.TabIndex = 9
        Me.LabelStatus.Text = "Enter an Item ID and Revision, or paste from Excel.  Latest revision is used if n" &
    "ot specified."
        '
        'ButtonAddAndClose
        '
        Me.ButtonAddAndClose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonAddAndClose.Location = New System.Drawing.Point(612, 3)
        Me.ButtonAddAndClose.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonAddAndClose.Name = "ButtonAddAndClose"
        Me.ButtonAddAndClose.Size = New System.Drawing.Size(92, 28)
        Me.ButtonAddAndClose.TabIndex = 5
        Me.ButtonAddAndClose.Text = "Add and close"
        Me.ButtonAddAndClose.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Cancel.Location = New System.Drawing.Point(711, 3)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(94, 28)
        Me.Cancel.TabIndex = 6
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.ExTableLayoutPanel1.ColumnCount = 3
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.Label3, 2, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ListViewDownloadedFiles, 2, 1)
        Me.ExTableLayoutPanel1.Controls.Add(Me.Label2, 1, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonDownloadAll, 1, 3)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ListViewTeamCenterItems, 1, 1)
        Me.ExTableLayoutPanel1.Controls.Add(Me.DataGridViewItems, 0, 1)
        Me.ExTableLayoutPanel1.Controls.Add(Me.Label4, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonDownload, 1, 2)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonSearch, 0, 2)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonSearchAndAdd, 0, 3)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(3, 43)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 4
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(808, 425)
        Me.ExTableLayoutPanel1.TabIndex = 21
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(610, 2)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 15)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "Files in cache"
        '
        'ListViewDownloadedFiles
        '
        Me.ListViewDownloadedFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListViewDownloadedFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.fileNam, Me.filePath})
        Me.ListViewDownloadedFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewDownloadedFiles.HideSelection = False
        Me.ListViewDownloadedFiles.Location = New System.Drawing.Point(610, 23)
        Me.ListViewDownloadedFiles.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ListViewDownloadedFiles.Name = "ListViewDownloadedFiles"
        Me.ListViewDownloadedFiles.Size = New System.Drawing.Size(194, 339)
        Me.ListViewDownloadedFiles.TabIndex = 24
        Me.ListViewDownloadedFiles.TabStop = False
        Me.ListViewDownloadedFiles.UseCompatibleStateImageBehavior = False
        Me.ListViewDownloadedFiles.View = System.Windows.Forms.View.Details
        '
        'fileNam
        '
        Me.fileNam.Text = "File Name"
        Me.fileNam.Width = 100
        '
        'filePath
        '
        Me.filePath.Text = "File Path"
        Me.filePath.Width = 1000
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(206, 2)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 15)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Files in TeamCenter"
        '
        'ButtonDownloadAll
        '
        Me.ButtonDownloadAll.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ButtonDownloadAll.Location = New System.Drawing.Point(206, 398)
        Me.ButtonDownloadAll.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonDownloadAll.Name = "ButtonDownloadAll"
        Me.ButtonDownloadAll.Size = New System.Drawing.Size(175, 24)
        Me.ButtonDownloadAll.TabIndex = 4
        Me.ButtonDownloadAll.Text = "Add all to cache"
        Me.ButtonDownloadAll.UseVisualStyleBackColor = True
        '
        'ListViewTeamCenterItems
        '
        Me.ListViewTeamCenterItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListViewTeamCenterItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.fileName, Me.itemID, Me.revision, Me.fileType})
        Me.ListViewTeamCenterItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewTeamCenterItems.HideSelection = False
        Me.ListViewTeamCenterItems.Location = New System.Drawing.Point(206, 23)
        Me.ListViewTeamCenterItems.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ListViewTeamCenterItems.Name = "ListViewTeamCenterItems"
        Me.ListViewTeamCenterItems.Size = New System.Drawing.Size(396, 339)
        Me.ListViewTeamCenterItems.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListViewTeamCenterItems.TabIndex = 25
        Me.ListViewTeamCenterItems.TabStop = False
        Me.ListViewTeamCenterItems.UseCompatibleStateImageBehavior = False
        Me.ListViewTeamCenterItems.View = System.Windows.Forms.View.Details
        '
        'fileName
        '
        Me.fileName.Text = "File Name"
        Me.fileName.Width = 100
        '
        'itemID
        '
        Me.itemID.Text = "Item ID"
        Me.itemID.Width = 100
        '
        'revision
        '
        Me.revision.Text = "Revision"
        Me.revision.Width = 75
        '
        'fileType
        '
        Me.fileType.Text = "File Type"
        '
        'DataGridViewItems
        '
        Me.DataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ItemIDs, Me.rev})
        Me.DataGridViewItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewItems.Location = New System.Drawing.Point(4, 23)
        Me.DataGridViewItems.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DataGridViewItems.Name = "DataGridViewItems"
        Me.DataGridViewItems.Size = New System.Drawing.Size(194, 339)
        Me.DataGridViewItems.TabIndex = 20
        Me.DataGridViewItems.TabStop = False
        '
        'ItemIDs
        '
        Me.ItemIDs.HeaderText = "Item ID"
        Me.ItemIDs.Name = "ItemIDs"
        Me.ItemIDs.Width = 75
        '
        'rev
        '
        Me.rev.HeaderText = "Revision"
        Me.rev.Name = "rev"
        Me.rev.Width = 75
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 2)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 15)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Search terms"
        '
        'ButtonDownload
        '
        Me.ButtonDownload.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ButtonDownload.Location = New System.Drawing.Point(206, 368)
        Me.ButtonDownload.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonDownload.Name = "ButtonDownload"
        Me.ButtonDownload.Size = New System.Drawing.Size(175, 24)
        Me.ButtonDownload.TabIndex = 3
        Me.ButtonDownload.Text = "Add selected to cache"
        Me.ButtonDownload.UseVisualStyleBackColor = True
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ButtonSearch.Location = New System.Drawing.Point(4, 368)
        Me.ButtonSearch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(175, 24)
        Me.ButtonSearch.TabIndex = 1
        Me.ButtonSearch.Text = "Search"
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'ButtonSearchAndAdd
        '
        Me.ButtonSearchAndAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ButtonSearchAndAdd.Location = New System.Drawing.Point(4, 398)
        Me.ButtonSearchAndAdd.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonSearchAndAdd.Name = "ButtonSearchAndAdd"
        Me.ButtonSearchAndAdd.Size = New System.Drawing.Size(175, 24)
        Me.ButtonSearchAndAdd.TabIndex = 2
        Me.ButtonSearchAndAdd.Text = "Search and add all to cache"
        Me.ButtonSearchAndAdd.UseVisualStyleBackColor = True
        '
        'ToolStrip_List
        '
        Me.ToolStrip_List.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ToolStrip_List.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip_List.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip_List.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip_List.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.CheckBoxAsm, Me.CheckBoxPar, Me.CheckBoxPsm, Me.CheckBoxDft, Me.ToolStripSeparator1, Me.ToolStripLabel2, Me.ButtonSettings, Me.ToolStripSeparator2, Me.ButtonHelp})
        Me.ToolStrip_List.Location = New System.Drawing.Point(3, 6)
        Me.ToolStrip_List.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.ToolStrip_List.Name = "ToolStrip_List"
        Me.ToolStrip_List.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip_List.Size = New System.Drawing.Size(264, 27)
        Me.ToolStrip_List.TabIndex = 34
        Me.ToolStrip_List.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(56, 24)
        Me.ToolStripLabel1.Text = "File Type:"
        '
        'CheckBoxAsm
        '
        Me.CheckBoxAsm.Checked = True
        Me.CheckBoxAsm.CheckOnClick = True
        Me.CheckBoxAsm.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxAsm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CheckBoxAsm.Image = Global.Housekeeper.My.Resources.Resources.SE_asm
        Me.CheckBoxAsm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CheckBoxAsm.Name = "CheckBoxAsm"
        Me.CheckBoxAsm.Size = New System.Drawing.Size(24, 24)
        Me.CheckBoxAsm.Text = "Filter ASM"
        '
        'CheckBoxPar
        '
        Me.CheckBoxPar.Checked = True
        Me.CheckBoxPar.CheckOnClick = True
        Me.CheckBoxPar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxPar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CheckBoxPar.Image = Global.Housekeeper.My.Resources.Resources.SE_par
        Me.CheckBoxPar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CheckBoxPar.Name = "CheckBoxPar"
        Me.CheckBoxPar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckBoxPar.Size = New System.Drawing.Size(24, 24)
        Me.CheckBoxPar.Text = "Filter PAR"
        '
        'CheckBoxPsm
        '
        Me.CheckBoxPsm.Checked = True
        Me.CheckBoxPsm.CheckOnClick = True
        Me.CheckBoxPsm.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxPsm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CheckBoxPsm.Image = Global.Housekeeper.My.Resources.Resources.SE_psm
        Me.CheckBoxPsm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CheckBoxPsm.Name = "CheckBoxPsm"
        Me.CheckBoxPsm.Size = New System.Drawing.Size(24, 24)
        Me.CheckBoxPsm.Text = "Filter PSM"
        '
        'CheckBoxDft
        '
        Me.CheckBoxDft.Checked = True
        Me.CheckBoxDft.CheckOnClick = True
        Me.CheckBoxDft.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxDft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CheckBoxDft.Image = Global.Housekeeper.My.Resources.Resources.SE_dft
        Me.CheckBoxDft.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CheckBoxDft.Name = "CheckBoxDft"
        Me.CheckBoxDft.Size = New System.Drawing.Size(24, 24)
        Me.CheckBoxDft.Text = "Filter DFT"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(49, 24)
        Me.ToolStripLabel2.Text = "Settings"
        '
        'ButtonSettings
        '
        Me.ButtonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonSettings.Image = Global.Housekeeper.My.Resources.Resources.config
        Me.ButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonSettings.Name = "ButtonSettings"
        Me.ButtonSettings.Size = New System.Drawing.Size(24, 24)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 27)
        '
        'ButtonHelp
        '
        Me.ButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonHelp.Image = Global.Housekeeper.My.Resources.Resources.Help
        Me.ButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonHelp.Name = "ButtonHelp"
        Me.ButtonHelp.Size = New System.Drawing.Size(24, 24)
        Me.ButtonHelp.ToolTipText = "Help"
        '
        'ExTableLayoutPanel2
        '
        Me.ExTableLayoutPanel2.BackColor = System.Drawing.Color.White
        Me.ExTableLayoutPanel2.ColumnCount = 1
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.Controls.Add(Me.ExTableLayoutPanel1, 0, 1)
        Me.ExTableLayoutPanel2.Controls.Add(Me.ExTableLayoutPanel3, 0, 2)
        Me.ExTableLayoutPanel2.Controls.Add(Me.ToolStrip_List, 0, 0)
        Me.ExTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel2.Name = "ExTableLayoutPanel2"
        Me.ExTableLayoutPanel2.RowCount = 3
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.ExTableLayoutPanel2.Size = New System.Drawing.Size(814, 511)
        Me.ExTableLayoutPanel2.TabIndex = 22
        Me.ExTableLayoutPanel2.Task = Nothing
        '
        'ExTableLayoutPanel3
        '
        Me.ExTableLayoutPanel3.BackColor = System.Drawing.Color.White
        Me.ExTableLayoutPanel3.ColumnCount = 3
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ExTableLayoutPanel3.Controls.Add(Me.LabelStatus, 0, 0)
        Me.ExTableLayoutPanel3.Controls.Add(Me.Cancel, 2, 0)
        Me.ExTableLayoutPanel3.Controls.Add(Me.ButtonAddAndClose, 1, 0)
        Me.ExTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel3.Location = New System.Drawing.Point(3, 474)
        Me.ExTableLayoutPanel3.Name = "ExTableLayoutPanel3"
        Me.ExTableLayoutPanel3.RowCount = 1
        Me.ExTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel3.Size = New System.Drawing.Size(808, 34)
        Me.ExTableLayoutPanel3.TabIndex = 23
        Me.ExTableLayoutPanel3.Task = Nothing
        '
        'FormTeamCenterAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(814, 511)
        Me.Controls.Add(Me.ExTableLayoutPanel2)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MinimumSize = New System.Drawing.Size(830, 550)
        Me.Name = "FormTeamCenterAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add TeamCenter Item"
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.PerformLayout()
        CType(Me.DataGridViewItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip_List.ResumeLayout(False)
        Me.ToolStrip_List.PerformLayout()
        Me.ExTableLayoutPanel2.ResumeLayout(False)
        Me.ExTableLayoutPanel2.PerformLayout()
        Me.ExTableLayoutPanel3.ResumeLayout(False)
        Me.ExTableLayoutPanel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonSearch As Button
    Friend WithEvents ListViewTeamCenterItems As ListView
    Friend WithEvents fileName As ColumnHeader
    Friend WithEvents revision As ColumnHeader
    Friend WithEvents itemID As ColumnHeader
    Friend WithEvents ButtonDownload As Button
    Friend WithEvents ListViewDownloadedFiles As ListView
    Friend WithEvents fileNam As ColumnHeader
    Friend WithEvents filePath As ColumnHeader
    Friend WithEvents LabelStatus As Label
    Friend WithEvents ButtonAddAndClose As Button
    Friend WithEvents ToolStrip_List As ToolStrip
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents CheckBoxDft As ToolStripButton
    Friend WithEvents CheckBoxPsm As ToolStripButton
    Friend WithEvents CheckBoxPar As ToolStripButton
    Friend WithEvents CheckBoxAsm As ToolStripButton
    Friend WithEvents ButtonDownloadAll As Button
    Friend WithEvents DataGridViewItems As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents ButtonSearchAndAdd As Button
    Friend WithEvents fileType As ColumnHeader
    Friend WithEvents ItemIDs As DataGridViewTextBoxColumn
    Friend WithEvents rev As DataGridViewTextBoxColumn
    Friend WithEvents Cancel As Button
    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel2 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel3 As ExTableLayoutPanel
    Friend WithEvents ButtonSettings As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ButtonHelp As ToolStripButton
End Class
