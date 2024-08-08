<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormVariableInputEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormVariableInputEditor))
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.ExTableLayoutPanel1 = New Housekeeper.ExTableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PanelVariables = New System.Windows.Forms.Panel()
        Me.ExTableLayoutPanelVariables = New Housekeeper.ExTableLayoutPanel()
        Me.UcEditVariables1 = New Housekeeper.UCEditVariables()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ToolStripEditVariables = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ComboBoxSavedSettings = New System.Windows.Forms.ToolStripComboBox()
        Me.ButtonSaveSetting = New System.Windows.Forms.ToolStripButton()
        Me.ButtonDeleteSetting = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonUp = New System.Windows.Forms.ToolStripButton()
        Me.ButtonDown = New System.Windows.Forms.ToolStripButton()
        Me.ButtonDeleteRow = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonHelp = New System.Windows.Forms.ToolStripButton()
        Me.PanelHeader.SuspendLayout()
        Me.ExTableLayoutPanel1.SuspendLayout()
        Me.PanelVariables.SuspendLayout()
        Me.ExTableLayoutPanelVariables.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.ToolStripEditVariables.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(573, 324)
        Me.ButtonOK.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 25)
        Me.ButtonOK.TabIndex = 0
        Me.ButtonOK.Text = "OK"
        Me.ToolTip1.SetToolTip(Me.ButtonOK, "OK")
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(667, 324)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.ButtonCancel, "Cancel")
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'PanelHeader
        '
        Me.PanelHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelHeader.Controls.Add(Me.ExTableLayoutPanel1)
        Me.PanelHeader.Location = New System.Drawing.Point(5, 30)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(750, 35)
        Me.PanelHeader.TabIndex = 5
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ExTableLayoutPanel1.ColumnCount = 6
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.Label1, 1, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.Label8, 2, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.Label9, 3, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.Label10, 4, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.Label11, 5, 0)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 1
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(750, 35)
        Me.ExTableLayoutPanel1.TabIndex = 0
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Variable Name"
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(205, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 16)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Number / Formula"
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(377, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 16)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Unit Type"
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(549, 9)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(20, 16)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "EX"
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(579, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 16)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Expose Name"
        '
        'PanelVariables
        '
        Me.PanelVariables.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelVariables.Controls.Add(Me.ExTableLayoutPanelVariables)
        Me.PanelVariables.Location = New System.Drawing.Point(5, 65)
        Me.PanelVariables.Name = "PanelVariables"
        Me.PanelVariables.Size = New System.Drawing.Size(750, 200)
        Me.PanelVariables.TabIndex = 6
        '
        'ExTableLayoutPanelVariables
        '
        Me.ExTableLayoutPanelVariables.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ExTableLayoutPanelVariables.ColumnCount = 1
        Me.ExTableLayoutPanelVariables.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanelVariables.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ExTableLayoutPanelVariables.Controls.Add(Me.UcEditVariables1, 0, 0)
        Me.ExTableLayoutPanelVariables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanelVariables.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanelVariables.Name = "ExTableLayoutPanelVariables"
        Me.ExTableLayoutPanelVariables.RowCount = 2
        Me.ExTableLayoutPanelVariables.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanelVariables.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanelVariables.Size = New System.Drawing.Size(750, 200)
        Me.ExTableLayoutPanelVariables.TabIndex = 0
        Me.ExTableLayoutPanelVariables.Task = Nothing
        '
        'UcEditVariables1
        '
        Me.UcEditVariables1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcEditVariables1.Expose = False
        Me.UcEditVariables1.ExposeName = ""
        Me.UcEditVariables1.Formula = ""
        Me.UcEditVariables1.Location = New System.Drawing.Point(4, 4)
        Me.UcEditVariables1.Margin = New System.Windows.Forms.Padding(4)
        Me.UcEditVariables1.Name = "UcEditVariables1"
        Me.UcEditVariables1.NotifyVariableEditor = False
        Me.UcEditVariables1.Selected = False
        Me.UcEditVariables1.Size = New System.Drawing.Size(742, 27)
        Me.UcEditVariables1.TabIndex = 0
        Me.UcEditVariables1.UnitType = ""
        Me.UcEditVariables1.VariableEditor = Nothing
        Me.UcEditVariables1.VariableName = ""
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Location = New System.Drawing.Point(5, 265)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(750, 50)
        Me.Panel3.TabIndex = 7
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(110, 15)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(61, 16)
        Me.Label13.TabIndex = 1
        Me.Label13.Text = "EX: Expose"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(20, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(55, 16)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "*LEGEND"
        '
        'ToolStripEditVariables
        '
        Me.ToolStripEditVariables.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.ComboBoxSavedSettings, Me.ButtonSaveSetting, Me.ButtonDeleteSetting, Me.ToolStripLabel1, Me.ToolStripSeparator2, Me.ButtonUp, Me.ButtonDown, Me.ButtonDeleteRow, Me.ToolStripLabel2, Me.ToolStripSeparator3, Me.ButtonHelp})
        Me.ToolStripEditVariables.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripEditVariables.Name = "ToolStripEditVariables"
        Me.ToolStripEditVariables.Size = New System.Drawing.Size(759, 25)
        Me.ToolStripEditVariables.TabIndex = 8
        Me.ToolStripEditVariables.Text = "ToolStrip1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ComboBoxSavedSettings
        '
        Me.ComboBoxSavedSettings.Name = "ComboBoxSavedSettings"
        Me.ComboBoxSavedSettings.Size = New System.Drawing.Size(121, 25)
        Me.ComboBoxSavedSettings.Sorted = True
        '
        'ButtonSaveSetting
        '
        Me.ButtonSaveSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonSaveSetting.Image = Global.Housekeeper.My.Resources.Resources.Save
        Me.ButtonSaveSetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonSaveSetting.Name = "ButtonSaveSetting"
        Me.ButtonSaveSetting.Size = New System.Drawing.Size(23, 22)
        Me.ButtonSaveSetting.Text = "ToolStripButton1"
        Me.ButtonSaveSetting.ToolTipText = "Save setting"
        '
        'ButtonDeleteSetting
        '
        Me.ButtonDeleteSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonDeleteSetting.Image = Global.Housekeeper.My.Resources.Resources.Close
        Me.ButtonDeleteSetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonDeleteSetting.Name = "ButtonDeleteSetting"
        Me.ButtonDeleteSetting.Size = New System.Drawing.Size(23, 22)
        Me.ButtonDeleteSetting.Text = "ToolStripButton1"
        Me.ButtonDeleteSetting.ToolTipText = "Delete setting"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(83, 22)
        Me.ToolStripLabel1.Text = "Saved Settings"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ButtonUp
        '
        Me.ButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonUp.Image = Global.Housekeeper.My.Resources.Resources.up
        Me.ButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonUp.Name = "ButtonUp"
        Me.ButtonUp.Size = New System.Drawing.Size(23, 22)
        Me.ButtonUp.Text = "ToolStripButton1"
        Me.ButtonUp.ToolTipText = "Move row up"
        '
        'ButtonDown
        '
        Me.ButtonDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonDown.Image = Global.Housekeeper.My.Resources.Resources.down
        Me.ButtonDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonDown.Name = "ButtonDown"
        Me.ButtonDown.Size = New System.Drawing.Size(23, 22)
        Me.ButtonDown.Text = "ToolStripButton2"
        Me.ButtonDown.ToolTipText = "Move row down"
        '
        'ButtonDeleteRow
        '
        Me.ButtonDeleteRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonDeleteRow.Image = Global.Housekeeper.My.Resources.Resources.Close
        Me.ButtonDeleteRow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonDeleteRow.Name = "ButtonDeleteRow"
        Me.ButtonDeleteRow.Size = New System.Drawing.Size(23, 22)
        Me.ButtonDeleteRow.Text = "ToolStripButton3"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(60, 22)
        Me.ToolStripLabel2.Text = "Row Tools"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ButtonHelp
        '
        Me.ButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonHelp.Image = Global.Housekeeper.My.Resources.Resources.Help
        Me.ButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonHelp.Name = "ButtonHelp"
        Me.ButtonHelp.Size = New System.Drawing.Size(23, 22)
        Me.ButtonHelp.Text = "ToolStripButton1"
        Me.ButtonHelp.ToolTipText = "Help"
        '
        'FormVariableInputEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 361)
        Me.Controls.Add(Me.ToolStripEditVariables)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.PanelVariables)
        Me.Controls.Add(Me.PanelHeader)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormVariableInputEditor"
        Me.Text = "Variable Input Editor"
        Me.PanelHeader.ResumeLayout(False)
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.PerformLayout()
        Me.PanelVariables.ResumeLayout(False)
        Me.ExTableLayoutPanelVariables.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ToolStripEditVariables.ResumeLayout(False)
        Me.ToolStripEditVariables.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents PanelHeader As Panel
    Friend WithEvents PanelVariables As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanelVariables As ExTableLayoutPanel
    Friend WithEvents UcEditVariables1 As UCEditVariables
    Friend WithEvents Label1 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents ToolStripEditVariables As ToolStrip
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ComboBoxSavedSettings As ToolStripComboBox
    Friend WithEvents ButtonSaveSetting As ToolStripButton
    Friend WithEvents ButtonDeleteSetting As ToolStripButton
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ButtonUp As ToolStripButton
    Friend WithEvents ButtonDown As ToolStripButton
    Friend WithEvents ButtonDeleteRow As ToolStripButton
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ButtonHelp As ToolStripButton
End Class
