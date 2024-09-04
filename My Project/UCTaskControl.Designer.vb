<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCTaskControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.TLP = New Housekeeper.ExTableLayoutPanel()
        Me.LBImage = New System.Windows.Forms.Label()
        Me.CBSheetmetal = New System.Windows.Forms.CheckBox()
        Me.CBPart = New System.Windows.Forms.CheckBox()
        Me.CBAssembly = New System.Windows.Forms.CheckBox()
        Me.CBExpand = New System.Windows.Forms.CheckBox()
        Me.CBEnabled = New System.Windows.Forms.CheckBox()
        Me.CBDraft = New System.Windows.Forms.CheckBox()
        Me.TaskName = New System.Windows.Forms.Label()
        Me.HelpButton = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.InsertPropertyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TLP.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TLP
        '
        Me.TLP.AutoSize = True
        Me.TLP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TLP.BackColor = System.Drawing.Color.Transparent
        Me.TLP.ColumnCount = 9
        Me.TLP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLP.Controls.Add(Me.LBImage, 0, 0)
        Me.TLP.Controls.Add(Me.CBSheetmetal, 5, 0)
        Me.TLP.Controls.Add(Me.CBPart, 4, 0)
        Me.TLP.Controls.Add(Me.CBAssembly, 3, 0)
        Me.TLP.Controls.Add(Me.CBExpand, 2, 0)
        Me.TLP.Controls.Add(Me.CBEnabled, 1, 0)
        Me.TLP.Controls.Add(Me.CBDraft, 6, 0)
        Me.TLP.Controls.Add(Me.TaskName, 7, 0)
        Me.TLP.Controls.Add(Me.HelpButton, 8, 0)
        Me.TLP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLP.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TLP.Location = New System.Drawing.Point(0, 0)
        Me.TLP.Margin = New System.Windows.Forms.Padding(4)
        Me.TLP.Name = "TLP"
        Me.TLP.RowCount = 2
        Me.TLP.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLP.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TLP.Size = New System.Drawing.Size(324, 37)
        Me.TLP.TabIndex = 0
        Me.TLP.Task = Nothing
        '
        'LBImage
        '
        Me.LBImage.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LBImage.BackColor = System.Drawing.Color.Transparent
        Me.LBImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LBImage.Location = New System.Drawing.Point(1, 0)
        Me.LBImage.Margin = New System.Windows.Forms.Padding(0)
        Me.LBImage.Name = "LBImage"
        Me.LBImage.Size = New System.Drawing.Size(28, 37)
        Me.LBImage.TabIndex = 10
        Me.LBImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CBSheetmetal
        '
        Me.CBSheetmetal.Appearance = System.Windows.Forms.Appearance.Button
        Me.CBSheetmetal.AutoSize = True
        Me.CBSheetmetal.BackColor = System.Drawing.Color.Transparent
        Me.CBSheetmetal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CBSheetmetal.FlatAppearance.BorderSize = 0
        Me.CBSheetmetal.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.CBSheetmetal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBSheetmetal.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.CBSheetmetal.Location = New System.Drawing.Point(156, 6)
        Me.CBSheetmetal.Margin = New System.Windows.Forms.Padding(6)
        Me.CBSheetmetal.Name = "CBSheetmetal"
        Me.CBSheetmetal.Size = New System.Drawing.Size(18, 25)
        Me.CBSheetmetal.TabIndex = 5
        Me.CBSheetmetal.UseVisualStyleBackColor = False
        '
        'CBPart
        '
        Me.CBPart.Appearance = System.Windows.Forms.Appearance.Button
        Me.CBPart.AutoSize = True
        Me.CBPart.BackColor = System.Drawing.Color.Transparent
        Me.CBPart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CBPart.FlatAppearance.BorderSize = 0
        Me.CBPart.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.CBPart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBPart.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.CBPart.Location = New System.Drawing.Point(126, 6)
        Me.CBPart.Margin = New System.Windows.Forms.Padding(6)
        Me.CBPart.Name = "CBPart"
        Me.CBPart.Size = New System.Drawing.Size(18, 25)
        Me.CBPart.TabIndex = 4
        Me.CBPart.UseVisualStyleBackColor = False
        '
        'CBAssembly
        '
        Me.CBAssembly.Appearance = System.Windows.Forms.Appearance.Button
        Me.CBAssembly.AutoSize = True
        Me.CBAssembly.BackColor = System.Drawing.Color.Transparent
        Me.CBAssembly.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CBAssembly.FlatAppearance.BorderSize = 0
        Me.CBAssembly.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.CBAssembly.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBAssembly.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.CBAssembly.Location = New System.Drawing.Point(96, 6)
        Me.CBAssembly.Margin = New System.Windows.Forms.Padding(6)
        Me.CBAssembly.Name = "CBAssembly"
        Me.CBAssembly.Size = New System.Drawing.Size(18, 25)
        Me.CBAssembly.TabIndex = 3
        Me.CBAssembly.UseVisualStyleBackColor = False
        '
        'CBExpand
        '
        Me.CBExpand.Appearance = System.Windows.Forms.Appearance.Button
        Me.CBExpand.AutoSize = True
        Me.CBExpand.BackColor = System.Drawing.Color.Transparent
        Me.CBExpand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CBExpand.FlatAppearance.BorderSize = 0
        Me.CBExpand.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.CBExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBExpand.Image = Global.Housekeeper.My.Resources.Resources.expand_disabled
        Me.CBExpand.Location = New System.Drawing.Point(66, 6)
        Me.CBExpand.Margin = New System.Windows.Forms.Padding(6)
        Me.CBExpand.Name = "CBExpand"
        Me.CBExpand.Size = New System.Drawing.Size(18, 25)
        Me.CBExpand.TabIndex = 2
        Me.CBExpand.UseVisualStyleBackColor = False
        '
        'CBEnabled
        '
        Me.CBEnabled.Appearance = System.Windows.Forms.Appearance.Button
        Me.CBEnabled.AutoSize = True
        Me.CBEnabled.BackColor = System.Drawing.Color.Transparent
        Me.CBEnabled.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CBEnabled.FlatAppearance.BorderSize = 0
        Me.CBEnabled.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.CBEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBEnabled.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.CBEnabled.Location = New System.Drawing.Point(36, 6)
        Me.CBEnabled.Margin = New System.Windows.Forms.Padding(6)
        Me.CBEnabled.Name = "CBEnabled"
        Me.CBEnabled.Size = New System.Drawing.Size(18, 25)
        Me.CBEnabled.TabIndex = 1
        Me.CBEnabled.UseVisualStyleBackColor = False
        '
        'CBDraft
        '
        Me.CBDraft.Appearance = System.Windows.Forms.Appearance.Button
        Me.CBDraft.AutoSize = True
        Me.CBDraft.BackColor = System.Drawing.Color.Transparent
        Me.CBDraft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CBDraft.FlatAppearance.BorderSize = 0
        Me.CBDraft.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.CBDraft.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBDraft.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.CBDraft.Location = New System.Drawing.Point(186, 6)
        Me.CBDraft.Margin = New System.Windows.Forms.Padding(6)
        Me.CBDraft.Name = "CBDraft"
        Me.CBDraft.Size = New System.Drawing.Size(18, 25)
        Me.CBDraft.TabIndex = 11
        Me.CBDraft.UseVisualStyleBackColor = False
        '
        'TaskName
        '
        Me.TaskName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TaskName.AutoSize = True
        Me.TaskName.Location = New System.Drawing.Point(214, 11)
        Me.TaskName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.TaskName.Name = "TaskName"
        Me.TaskName.Padding = New System.Windows.Forms.Padding(12, 0, 0, 0)
        Me.TaskName.Size = New System.Drawing.Size(76, 15)
        Me.TaskName.TabIndex = 12
        Me.TaskName.Text = "Task Name"
        '
        'HelpButton
        '
        Me.HelpButton.FlatAppearance.BorderSize = 0
        Me.HelpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.HelpButton.Image = Global.Housekeeper.My.Resources.Resources.Help
        Me.HelpButton.Location = New System.Drawing.Point(298, 4)
        Me.HelpButton.Margin = New System.Windows.Forms.Padding(4)
        Me.HelpButton.Name = "HelpButton"
        Me.HelpButton.Size = New System.Drawing.Size(22, 28)
        Me.HelpButton.TabIndex = 13
        Me.HelpButton.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InsertPropertyToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(152, 26)
        '
        'InsertPropertyToolStripMenuItem
        '
        Me.InsertPropertyToolStripMenuItem.Name = "InsertPropertyToolStripMenuItem"
        Me.InsertPropertyToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.InsertPropertyToolStripMenuItem.Text = "Insert property"
        '
        'UCTaskControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.TLP)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "UCTaskControl"
        Me.Size = New System.Drawing.Size(324, 37)
        Me.TLP.ResumeLayout(False)
        Me.TLP.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CBEnabled As CheckBox
    Friend WithEvents CBExpand As CheckBox
    Friend WithEvents CBSheetmetal As CheckBox
    Friend WithEvents CBPart As CheckBox
    Friend WithEvents CBAssembly As CheckBox
    'Friend WithEvents ImageList1 As ImageList
    Friend WithEvents LBImage As Label
    Friend WithEvents CBDraft As CheckBox
    Friend WithEvents TaskName As Label
    Friend WithEvents HelpButton As Button
    Friend WithEvents TLP As ExTableLayoutPanel
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents InsertPropertyToolStripMenuItem As ToolStripMenuItem
End Class
