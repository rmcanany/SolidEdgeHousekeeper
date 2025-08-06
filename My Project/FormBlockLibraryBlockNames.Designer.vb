<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBlockLibraryBlockNames
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ExTableLayoutPanel1 = New Housekeeper.ExTableLayoutPanel()
        Me.DataGridViewBlockLibraryBlockNames = New System.Windows.Forms.DataGridView()
        Me.LibraryBlocks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ExTableLayoutPanel2 = New Housekeeper.ExTableLayoutPanel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ExTableLayoutPanel3 = New Housekeeper.ExTableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonUpdateLibrary = New System.Windows.Forms.Button()
        Me.DataGridViewManuallyAddedBlockNames = New System.Windows.Forms.DataGridView()
        Me.FileBlocks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ExTableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridViewBlockLibraryBlockNames, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExTableLayoutPanel2.SuspendLayout()
        Me.ExTableLayoutPanel3.SuspendLayout()
        CType(Me.DataGridViewManuallyAddedBlockNames, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.ExTableLayoutPanel1.ColumnCount = 1
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.DataGridViewBlockLibraryBlockNames, 0, 1)
        Me.ExTableLayoutPanel1.Controls.Add(Me.Label3, 0, 2)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ExTableLayoutPanel2, 0, 4)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ExTableLayoutPanel3, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.DataGridViewManuallyAddedBlockNames, 0, 3)
        Me.ExTableLayoutPanel1.Controls.Add(Me.Panel1, 0, 5)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 6
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(303, 476)
        Me.ExTableLayoutPanel1.TabIndex = 0
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'DataGridViewBlockLibraryBlockNames
        '
        Me.DataGridViewBlockLibraryBlockNames.AllowUserToAddRows = False
        Me.DataGridViewBlockLibraryBlockNames.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewBlockLibraryBlockNames.BackgroundColor = System.Drawing.Color.White
        Me.DataGridViewBlockLibraryBlockNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewBlockLibraryBlockNames.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LibraryBlocks})
        Me.DataGridViewBlockLibraryBlockNames.EnableHeadersVisualStyles = False
        Me.DataGridViewBlockLibraryBlockNames.Location = New System.Drawing.Point(3, 43)
        Me.DataGridViewBlockLibraryBlockNames.MultiSelect = False
        Me.DataGridViewBlockLibraryBlockNames.Name = "DataGridViewBlockLibraryBlockNames"
        Me.DataGridViewBlockLibraryBlockNames.RowHeadersWidth = 30
        Me.DataGridViewBlockLibraryBlockNames.Size = New System.Drawing.Size(297, 168)
        Me.DataGridViewBlockLibraryBlockNames.TabIndex = 1
        '
        'LibraryBlocks
        '
        Me.LibraryBlocks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LibraryBlocks.HeaderText = "Library Blocks"
        Me.LibraryBlocks.Name = "LibraryBlocks"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 222)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(221, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Manually add/remove additional block names"
        '
        'ExTableLayoutPanel2
        '
        Me.ExTableLayoutPanel2.ColumnCount = 3
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.ExTableLayoutPanel2.Controls.Add(Me.ButtonCancel, 2, 0)
        Me.ExTableLayoutPanel2.Controls.Add(Me.ButtonOK, 1, 0)
        Me.ExTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel2.Location = New System.Drawing.Point(3, 409)
        Me.ExTableLayoutPanel2.Name = "ExTableLayoutPanel2"
        Me.ExTableLayoutPanel2.RowCount = 1
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel2.Size = New System.Drawing.Size(297, 34)
        Me.ExTableLayoutPanel2.TabIndex = 2
        Me.ExTableLayoutPanel2.Task = Nothing
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(219, 3)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(138, 3)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 25)
        Me.ButtonOK.TabIndex = 2
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel3
        '
        Me.ExTableLayoutPanel3.ColumnCount = 2
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel3.Controls.Add(Me.Label2, 1, 0)
        Me.ExTableLayoutPanel3.Controls.Add(Me.ButtonUpdateLibrary, 0, 0)
        Me.ExTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.ExTableLayoutPanel3.Name = "ExTableLayoutPanel3"
        Me.ExTableLayoutPanel3.RowCount = 1
        Me.ExTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel3.Size = New System.Drawing.Size(297, 34)
        Me.ExTableLayoutPanel3.TabIndex = 3
        Me.ExTableLayoutPanel3.Task = Nothing
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(84, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(194, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Update block list from library (opens SE)"
        '
        'ButtonUpdateLibrary
        '
        Me.ButtonUpdateLibrary.Location = New System.Drawing.Point(3, 3)
        Me.ButtonUpdateLibrary.Name = "ButtonUpdateLibrary"
        Me.ButtonUpdateLibrary.Size = New System.Drawing.Size(75, 25)
        Me.ButtonUpdateLibrary.TabIndex = 2
        Me.ButtonUpdateLibrary.Text = "Update"
        Me.ButtonUpdateLibrary.UseVisualStyleBackColor = True
        '
        'DataGridViewManuallyAddedBlockNames
        '
        Me.DataGridViewManuallyAddedBlockNames.BackgroundColor = System.Drawing.Color.White
        Me.DataGridViewManuallyAddedBlockNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewManuallyAddedBlockNames.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FileBlocks})
        Me.DataGridViewManuallyAddedBlockNames.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewManuallyAddedBlockNames.EnableHeadersVisualStyles = False
        Me.DataGridViewManuallyAddedBlockNames.Location = New System.Drawing.Point(3, 247)
        Me.DataGridViewManuallyAddedBlockNames.Name = "DataGridViewManuallyAddedBlockNames"
        Me.DataGridViewManuallyAddedBlockNames.RowHeadersWidth = 30
        Me.DataGridViewManuallyAddedBlockNames.Size = New System.Drawing.Size(297, 156)
        Me.DataGridViewManuallyAddedBlockNames.TabIndex = 4
        '
        'FileBlocks
        '
        Me.FileBlocks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.FileBlocks.HeaderText = "File Blocks"
        Me.FileBlocks.Name = "FileBlocks"
        '
        'LabelStatus
        '
        Me.LabelStatus.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Location = New System.Drawing.Point(9, 5)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(37, 13)
        Me.LabelStatus.TabIndex = 5
        Me.LabelStatus.Text = "Status"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel1.Controls.Add(Me.LabelStatus)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 449)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(297, 24)
        Me.Panel1.TabIndex = 5
        '
        'FormBlockLibraryBlockNames
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 476)
        Me.Controls.Add(Me.ExTableLayoutPanel1)
        Me.Name = "FormBlockLibraryBlockNames"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Blocks"
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.PerformLayout()
        CType(Me.DataGridViewBlockLibraryBlockNames, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExTableLayoutPanel2.ResumeLayout(False)
        Me.ExTableLayoutPanel3.ResumeLayout(False)
        Me.ExTableLayoutPanel3.PerformLayout()
        CType(Me.DataGridViewManuallyAddedBlockNames, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents DataGridViewBlockLibraryBlockNames As DataGridView
    Friend WithEvents ExTableLayoutPanel2 As ExTableLayoutPanel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ExTableLayoutPanel3 As ExTableLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ButtonUpdateLibrary As Button
    Friend WithEvents DataGridViewManuallyAddedBlockNames As DataGridView
    Friend WithEvents ButtonOK As Button
    Friend WithEvents LabelStatus As Label
    Friend WithEvents LibraryBlocks As DataGridViewTextBoxColumn
    Friend WithEvents FileBlocks As DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As Panel
End Class
