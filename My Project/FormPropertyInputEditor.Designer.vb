<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPropertyInputEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPropertyInputEditor))
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStripEditProperties = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ComboBoxSavedSettings = New System.Windows.Forms.ToolStripComboBox()
        Me.ButtonSaveSettings = New System.Windows.Forms.ToolStripButton()
        Me.ButtonDeleteSetting = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonUp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonDown = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonDeleteRow = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonExpressionEditor = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonShowAllProps = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonHelp = New System.Windows.Forms.ToolStripButton()
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.ExTableLayoutPanelHeader = New Housekeeper.ExTableLayoutPanel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.PanelSearches = New System.Windows.Forms.Panel()
        Me.ExTableLayoutPanelSearches = New Housekeeper.ExTableLayoutPanel()
        Me.PanelFooter = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.ToolStripEditProperties.SuspendLayout()
        Me.PanelHeader.SuspendLayout()
        Me.ExTableLayoutPanelHeader.SuspendLayout()
        Me.PanelSearches.SuspendLayout()
        Me.PanelFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(564, 327)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 25)
        Me.ButtonOK.TabIndex = 6
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(664, 327)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 7
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ToolStripEditProperties
        '
        Me.ToolStripEditProperties.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripEditProperties.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator3, Me.ComboBoxSavedSettings, Me.ButtonSaveSettings, Me.ButtonDeleteSetting, Me.ToolStripLabel1, Me.ToolStripSeparator1, Me.ToolStripButtonUp, Me.ToolStripButtonDown, Me.ToolStripButtonDeleteRow, Me.ToolStripLabel2, Me.ToolStripSeparator2, Me.ToolStripButtonExpressionEditor, Me.ToolStripSeparator5, Me.ButtonShowAllProps, Me.ToolStripSeparator4, Me.ToolStripButtonHelp})
        Me.ToolStripEditProperties.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripEditProperties.Name = "ToolStripEditProperties"
        Me.ToolStripEditProperties.Size = New System.Drawing.Size(759, 25)
        Me.ToolStripEditProperties.TabIndex = 13
        Me.ToolStripEditProperties.Text = "ToolStrip1"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ComboBoxSavedSettings
        '
        Me.ComboBoxSavedSettings.Name = "ComboBoxSavedSettings"
        Me.ComboBoxSavedSettings.Size = New System.Drawing.Size(150, 25)
        Me.ComboBoxSavedSettings.Sorted = True
        Me.ComboBoxSavedSettings.ToolTipText = "Saved Searches"
        '
        'ButtonSaveSettings
        '
        Me.ButtonSaveSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonSaveSettings.Image = Global.Housekeeper.My.Resources.Resources.Save
        Me.ButtonSaveSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonSaveSettings.Name = "ButtonSaveSettings"
        Me.ButtonSaveSettings.Size = New System.Drawing.Size(23, 22)
        Me.ButtonSaveSettings.Text = "ToolStripButton1"
        Me.ButtonSaveSettings.ToolTipText = "Save Search"
        '
        'ButtonDeleteSetting
        '
        Me.ButtonDeleteSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonDeleteSetting.Image = Global.Housekeeper.My.Resources.Resources.Close
        Me.ButtonDeleteSetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonDeleteSetting.Name = "ButtonDeleteSetting"
        Me.ButtonDeleteSetting.Size = New System.Drawing.Size(23, 22)
        Me.ButtonDeleteSetting.Text = "ToolStripButtonDeleteSearch"
        Me.ButtonDeleteSetting.ToolTipText = "Delete Search"
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
        'ToolStripButtonUp
        '
        Me.ToolStripButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonUp.Image = Global.Housekeeper.My.Resources.Resources.up
        Me.ToolStripButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonUp.Name = "ToolStripButtonUp"
        Me.ToolStripButtonUp.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonUp.Text = "ToolStripButton2"
        Me.ToolStripButtonUp.ToolTipText = "Move Selected Up"
        '
        'ToolStripButtonDown
        '
        Me.ToolStripButtonDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonDown.Image = Global.Housekeeper.My.Resources.Resources.down
        Me.ToolStripButtonDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonDown.Name = "ToolStripButtonDown"
        Me.ToolStripButtonDown.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonDown.Text = "ToolStripButton3"
        Me.ToolStripButtonDown.ToolTipText = "Move Selected Down"
        '
        'ToolStripButtonDeleteRow
        '
        Me.ToolStripButtonDeleteRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonDeleteRow.Image = Global.Housekeeper.My.Resources.Resources.Close
        Me.ToolStripButtonDeleteRow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonDeleteRow.Name = "ToolStripButtonDeleteRow"
        Me.ToolStripButtonDeleteRow.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonDeleteRow.Text = "ToolStripButton4"
        Me.ToolStripButtonDeleteRow.ToolTipText = "Delete Selected"
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
        'ToolStripButtonExpressionEditor
        '
        Me.ToolStripButtonExpressionEditor.Image = Global.Housekeeper.My.Resources.Resources.fx
        Me.ToolStripButtonExpressionEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonExpressionEditor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExpressionEditor.Name = "ToolStripButtonExpressionEditor"
        Me.ToolStripButtonExpressionEditor.Size = New System.Drawing.Size(117, 22)
        Me.ToolStripButtonExpressionEditor.Text = "Expression Editor"
        Me.ToolStripButtonExpressionEditor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
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
        'ToolStripButtonHelp
        '
        Me.ToolStripButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonHelp.Image = Global.Housekeeper.My.Resources.Resources.Help
        Me.ToolStripButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonHelp.Name = "ToolStripButtonHelp"
        Me.ToolStripButtonHelp.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonHelp.Text = "ToolStripButton1"
        Me.ToolStripButtonHelp.ToolTipText = "Help"
        '
        'PanelHeader
        '
        Me.PanelHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelHeader.Controls.Add(Me.ExTableLayoutPanelHeader)
        Me.PanelHeader.Location = New System.Drawing.Point(5, 30)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(750, 35)
        Me.PanelHeader.TabIndex = 14
        '
        'ExTableLayoutPanelHeader
        '
        Me.ExTableLayoutPanelHeader.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ExTableLayoutPanelHeader.ColumnCount = 7
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.ExTableLayoutPanelHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
        Me.ExTableLayoutPanelHeader.Controls.Add(Me.Label13, 1, 0)
        Me.ExTableLayoutPanelHeader.Controls.Add(Me.Label15, 2, 0)
        Me.ExTableLayoutPanelHeader.Controls.Add(Me.Label16, 3, 0)
        Me.ExTableLayoutPanelHeader.Controls.Add(Me.Label17, 4, 0)
        Me.ExTableLayoutPanelHeader.Controls.Add(Me.Label18, 5, 0)
        Me.ExTableLayoutPanelHeader.Controls.Add(Me.Label19, 6, 0)
        Me.ExTableLayoutPanelHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanelHeader.Name = "ExTableLayoutPanelHeader"
        Me.ExTableLayoutPanelHeader.RowCount = 1
        Me.ExTableLayoutPanelHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanelHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanelHeader.Size = New System.Drawing.Size(750, 35)
        Me.ExTableLayoutPanelHeader.TabIndex = 12
        Me.ExTableLayoutPanelHeader.Task = Nothing
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(38, 10)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(71, 15)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Property Set"
        '
        'Label15
        '
        Me.Label15.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(138, 10)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(87, 15)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "Property Name"
        '
        'Label16
        '
        Me.Label16.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(288, 10)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(24, 15)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "FS*"
        '
        'Label17
        '
        Me.Label17.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(338, 10)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(54, 15)
        Me.Label17.TabIndex = 3
        Me.Label17.Text = "Find Text"
        '
        'Label18
        '
        Me.Label18.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(465, 10)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(25, 15)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "RS*"
        '
        'Label19
        '
        Me.Label19.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(515, 10)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 15)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "Replace Text"
        '
        'PanelSearches
        '
        Me.PanelSearches.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelSearches.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.PanelSearches.Controls.Add(Me.ExTableLayoutPanelSearches)
        Me.PanelSearches.Location = New System.Drawing.Point(5, 65)
        Me.PanelSearches.Name = "PanelSearches"
        Me.PanelSearches.Size = New System.Drawing.Size(750, 200)
        Me.PanelSearches.TabIndex = 15
        '
        'ExTableLayoutPanelSearches
        '
        Me.ExTableLayoutPanelSearches.AutoScroll = True
        Me.ExTableLayoutPanelSearches.ColumnCount = 1
        Me.ExTableLayoutPanelSearches.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanelSearches.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanelSearches.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanelSearches.Name = "ExTableLayoutPanelSearches"
        Me.ExTableLayoutPanelSearches.RowCount = 2
        Me.ExTableLayoutPanelSearches.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanelSearches.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanelSearches.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ExTableLayoutPanelSearches.Size = New System.Drawing.Size(750, 200)
        Me.ExTableLayoutPanelSearches.TabIndex = 11
        Me.ExTableLayoutPanelSearches.Task = Nothing
        '
        'PanelFooter
        '
        Me.PanelFooter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelFooter.BackColor = System.Drawing.Color.LightSteelBlue
        Me.PanelFooter.Controls.Add(Me.Label9)
        Me.PanelFooter.Controls.Add(Me.Label8)
        Me.PanelFooter.Controls.Add(Me.Label7)
        Me.PanelFooter.Controls.Add(Me.Label6)
        Me.PanelFooter.Controls.Add(Me.Label5)
        Me.PanelFooter.Controls.Add(Me.Label4)
        Me.PanelFooter.Controls.Add(Me.Label3)
        Me.PanelFooter.Controls.Add(Me.Label2)
        Me.PanelFooter.Controls.Add(Me.Label1)
        Me.PanelFooter.Controls.Add(Me.Label20)
        Me.PanelFooter.Location = New System.Drawing.Point(5, 265)
        Me.PanelFooter.Name = "PanelFooter"
        Me.PanelFooter.Size = New System.Drawing.Size(750, 50)
        Me.PanelFooter.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(460, 25)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 15)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "EX: Expression"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(315, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(126, 15)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "RX: Regular Expression"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(220, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 15)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "PT: Plain Text"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(100, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(105, 15)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "RS: Replace Search"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(602, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 15)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "X: Delete"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(460, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(126, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "RX: Regular Expression"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(315, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "WC: Wildcard"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(220, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "PT: Plain Text"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(100, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 15)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "FS: Find Search"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(20, 5)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(58, 15)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "* LEGEND"
        '
        'FormPropertyInputEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 361)
        Me.Controls.Add(Me.PanelFooter)
        Me.Controls.Add(Me.PanelSearches)
        Me.Controls.Add(Me.PanelHeader)
        Me.Controls.Add(Me.ToolStripEditProperties)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormPropertyInputEditor"
        Me.Text = "Property Input Editor"
        Me.ToolStripEditProperties.ResumeLayout(False)
        Me.ToolStripEditProperties.PerformLayout()
        Me.PanelHeader.ResumeLayout(False)
        Me.ExTableLayoutPanelHeader.ResumeLayout(False)
        Me.ExTableLayoutPanelHeader.PerformLayout()
        Me.PanelSearches.ResumeLayout(False)
        Me.PanelFooter.ResumeLayout(False)
        Me.PanelFooter.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ExTableLayoutPanelSearches As ExTableLayoutPanel
    Friend WithEvents UcPropertyInput1 As UCEditProperties
    Friend WithEvents ExTableLayoutPanelHeader As ExTableLayoutPanel
    Friend WithEvents Label13 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents ToolStripEditProperties As ToolStrip
    Friend WithEvents ButtonSaveSettings As ToolStripButton
    Friend WithEvents ComboBoxSavedSettings As ToolStripComboBox
    Friend WithEvents ToolStripButtonUp As ToolStripButton
    Friend WithEvents ToolStripButtonDown As ToolStripButton
    Friend WithEvents ToolStripButtonDeleteRow As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripButtonHelp As ToolStripButton
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripButtonExpressionEditor As ToolStripButton
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents PanelHeader As Panel
    Friend WithEvents PanelSearches As Panel
    Friend WithEvents PanelFooter As Panel
    Friend WithEvents Label20 As Label
    Friend WithEvents ButtonDeleteSetting As ToolStripButton
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ButtonShowAllProps As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
End Class
