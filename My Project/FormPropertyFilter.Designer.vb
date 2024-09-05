<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPropertyFilter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPropertyFilter))
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ComboBoxSavedSettings = New System.Windows.Forms.ToolStripComboBox()
        Me.ButtonSaveSettings = New System.Windows.Forms.ToolStripButton()
        Me.ButtonRemoveSetting = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonRowUp = New System.Windows.Forms.ToolStripButton()
        Me.ButtonRowDown = New System.Windows.Forms.ToolStripButton()
        Me.ButtonRowDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonEditFormula = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonShowAllProps = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonHelp = New System.Windows.Forms.ToolStripButton()
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.ExTableLayoutPanelHeader = New Housekeeper.ExTableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PanelFilters = New System.Windows.Forms.Panel()
        Me.ExTableLayoutPanelFilters = New Housekeeper.ExTableLayoutPanel()
        Me.PanelFooter = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxFormula = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.PanelHeader.SuspendLayout()
        Me.ExTableLayoutPanelHeader.SuspendLayout()
        Me.PanelFilters.SuspendLayout()
        Me.PanelFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(597, 329)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(88, 23)
        Me.ButtonCancel.TabIndex = 22
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(488, 329)
        Me.ButtonOK.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(88, 23)
        Me.ButtonOK.TabIndex = 21
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ComboBoxSavedSettings, Me.ButtonSaveSettings, Me.ButtonRemoveSetting, Me.ToolStripLabel1, Me.ToolStripSeparator1, Me.ButtonRowUp, Me.ButtonRowDown, Me.ButtonRowDelete, Me.ToolStripLabel2, Me.ToolStripSeparator2, Me.ButtonEditFormula, Me.ToolStripLabel3, Me.ToolStripSeparator3, Me.ButtonShowAllProps, Me.ToolStripSeparator4, Me.ButtonHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(705, 25)
        Me.ToolStrip1.TabIndex = 24
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ComboBoxSavedSettings
        '
        Me.ComboBoxSavedSettings.Name = "ComboBoxSavedSettings"
        Me.ComboBoxSavedSettings.Size = New System.Drawing.Size(150, 25)
        Me.ComboBoxSavedSettings.Sorted = True
        Me.ComboBoxSavedSettings.ToolTipText = "Saved settings"
        '
        'ButtonSaveSettings
        '
        Me.ButtonSaveSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonSaveSettings.Image = Global.Housekeeper.My.Resources.Resources.Save
        Me.ButtonSaveSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonSaveSettings.Name = "ButtonSaveSettings"
        Me.ButtonSaveSettings.Size = New System.Drawing.Size(23, 22)
        Me.ButtonSaveSettings.Text = "ToolStripButton1"
        Me.ButtonSaveSettings.ToolTipText = "Save settings"
        '
        'ButtonRemoveSetting
        '
        Me.ButtonRemoveSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonRemoveSetting.Image = Global.Housekeeper.My.Resources.Resources.Close
        Me.ButtonRemoveSetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonRemoveSetting.Name = "ButtonRemoveSetting"
        Me.ButtonRemoveSetting.Size = New System.Drawing.Size(23, 22)
        Me.ButtonRemoveSetting.Text = "ToolStripButton2"
        Me.ButtonRemoveSetting.ToolTipText = "Remove setting"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(83, 22)
        Me.ToolStripLabel1.Text = "Saved Settings"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ButtonRowUp
        '
        Me.ButtonRowUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonRowUp.Image = Global.Housekeeper.My.Resources.Resources.up
        Me.ButtonRowUp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonRowUp.Name = "ButtonRowUp"
        Me.ButtonRowUp.Size = New System.Drawing.Size(23, 22)
        Me.ButtonRowUp.Text = "ToolStripButton1"
        Me.ButtonRowUp.ToolTipText = "Move row up"
        '
        'ButtonRowDown
        '
        Me.ButtonRowDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonRowDown.Image = Global.Housekeeper.My.Resources.Resources.down
        Me.ButtonRowDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonRowDown.Name = "ButtonRowDown"
        Me.ButtonRowDown.Size = New System.Drawing.Size(23, 22)
        Me.ButtonRowDown.Text = "ToolStripButton2"
        Me.ButtonRowDown.ToolTipText = "Move row down"
        '
        'ButtonRowDelete
        '
        Me.ButtonRowDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonRowDelete.Image = Global.Housekeeper.My.Resources.Resources.Close
        Me.ButtonRowDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonRowDelete.Name = "ButtonRowDelete"
        Me.ButtonRowDelete.Size = New System.Drawing.Size(23, 22)
        Me.ButtonRowDelete.Text = "ToolStripButton3"
        Me.ButtonRowDelete.ToolTipText = "Delete row"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(60, 22)
        Me.ToolStripLabel2.Text = "Row Tools"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ButtonEditFormula
        '
        Me.ButtonEditFormula.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonEditFormula.Image = Global.Housekeeper.My.Resources.Resources.fx
        Me.ButtonEditFormula.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonEditFormula.Name = "ButtonEditFormula"
        Me.ButtonEditFormula.Size = New System.Drawing.Size(23, 22)
        Me.ButtonEditFormula.Text = "ToolStripButton1"
        Me.ButtonEditFormula.ToolTipText = "Edit formula"
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(74, 22)
        Me.ToolStripLabel3.Text = "Edit Formula"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ButtonShowAllProps
        '
        Me.ButtonShowAllProps.CheckOnClick = True
        Me.ButtonShowAllProps.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.ButtonShowAllProps.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonShowAllProps.Name = "ButtonShowAllProps"
        Me.ButtonShowAllProps.Size = New System.Drawing.Size(106, 22)
        Me.ButtonShowAllProps.Text = "Show All Props"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
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
        'PanelHeader
        '
        Me.PanelHeader.BackColor = System.Drawing.Color.LightSteelBlue
        Me.PanelHeader.Controls.Add(Me.ExTableLayoutPanelHeader)
        Me.PanelHeader.Location = New System.Drawing.Point(5, 30)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(750, 35)
        Me.PanelHeader.TabIndex = 25
        '
        'ExTableLayoutPanelHeader
        '
        Me.ExTableLayoutPanelHeader.ColumnCount = 6
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanelHeader.Controls.Add(Me.Label1, 1, 0)
        Me.ExTableLayoutPanelHeader.Controls.Add(Me.Label2, 2, 0)
        Me.ExTableLayoutPanelHeader.Controls.Add(Me.Label3, 3, 0)
        Me.ExTableLayoutPanelHeader.Controls.Add(Me.Label4, 4, 0)
        Me.ExTableLayoutPanelHeader.Controls.Add(Me.Label5, 5, 0)
        Me.ExTableLayoutPanelHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanelHeader.Name = "ExTableLayoutPanelHeader"
        Me.ExTableLayoutPanelHeader.RowCount = 1
        Me.ExTableLayoutPanelHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanelHeader.Size = New System.Drawing.Size(750, 35)
        Me.ExTableLayoutPanelHeader.TabIndex = 0
        Me.ExTableLayoutPanelHeader.Task = Nothing
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(38, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(14, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "V"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(63, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Property Set"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(163, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Property Name"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(313, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 15)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Comparison"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(413, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 15)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Value"
        '
        'PanelFilters
        '
        Me.PanelFilters.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelFilters.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.PanelFilters.Controls.Add(Me.ExTableLayoutPanelFilters)
        Me.PanelFilters.Location = New System.Drawing.Point(5, 65)
        Me.PanelFilters.Name = "PanelFilters"
        Me.PanelFilters.Size = New System.Drawing.Size(696, 200)
        Me.PanelFilters.TabIndex = 26
        '
        'ExTableLayoutPanelFilters
        '
        Me.ExTableLayoutPanelFilters.AutoScroll = True
        Me.ExTableLayoutPanelFilters.ColumnCount = 1
        Me.ExTableLayoutPanelFilters.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanelFilters.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ExTableLayoutPanelFilters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanelFilters.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanelFilters.Name = "ExTableLayoutPanelFilters"
        Me.ExTableLayoutPanelFilters.RowCount = 2
        Me.ExTableLayoutPanelFilters.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanelFilters.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanelFilters.Size = New System.Drawing.Size(696, 200)
        Me.ExTableLayoutPanelFilters.TabIndex = 0
        Me.ExTableLayoutPanelFilters.Task = Nothing
        '
        'PanelFooter
        '
        Me.PanelFooter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelFooter.BackColor = System.Drawing.Color.LightSteelBlue
        Me.PanelFooter.Controls.Add(Me.Label7)
        Me.PanelFooter.Controls.Add(Me.TextBoxFormula)
        Me.PanelFooter.Controls.Add(Me.Label6)
        Me.PanelFooter.Location = New System.Drawing.Point(5, 265)
        Me.PanelFooter.Name = "PanelFooter"
        Me.PanelFooter.Size = New System.Drawing.Size(696, 50)
        Me.PanelFooter.TabIndex = 27
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(225, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 15)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "FORMULA"
        '
        'TextBoxFormula
        '
        Me.TextBoxFormula.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFormula.Enabled = False
        Me.TextBoxFormula.Location = New System.Drawing.Point(293, 12)
        Me.TextBoxFormula.Name = "TextBoxFormula"
        Me.TextBoxFormula.Size = New System.Drawing.Size(387, 23)
        Me.TextBoxFormula.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(25, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 15)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "*LEGEND         V: Variable"
        '
        'FormPropertyFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(705, 361)
        Me.Controls.Add(Me.PanelFooter)
        Me.Controls.Add(Me.PanelFilters)
        Me.Controls.Add(Me.PanelHeader)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "FormPropertyFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Property Filter"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.PanelHeader.ResumeLayout(False)
        Me.ExTableLayoutPanelHeader.ResumeLayout(False)
        Me.ExTableLayoutPanelHeader.PerformLayout()
        Me.PanelFilters.ResumeLayout(False)
        Me.PanelFooter.ResumeLayout(False)
        Me.PanelFooter.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ComboBoxSavedSettings As ToolStripComboBox
    Friend WithEvents ButtonSaveSettings As ToolStripButton
    Friend WithEvents ButtonRemoveSetting As ToolStripButton
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ButtonRowUp As ToolStripButton
    Friend WithEvents ButtonRowDown As ToolStripButton
    Friend WithEvents ButtonRowDelete As ToolStripButton
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ButtonEditFormula As ToolStripButton
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ButtonHelp As ToolStripButton
    Friend WithEvents PanelHeader As Panel
    Friend WithEvents PanelFilters As Panel
    Friend WithEvents PanelFooter As Panel
    Friend WithEvents ExTableLayoutPanelFilters As ExTableLayoutPanel
    Friend WithEvents UcPropertyFilter1 As UCPropertyFilter
    Friend WithEvents ExTableLayoutPanelHeader As ExTableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxFormula As TextBox
    Friend WithEvents ButtonShowAllProps As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
End Class
