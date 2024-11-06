<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPropertyListCustomizeManualEntry
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPropertyListCustomizeManualEntry))
        Me.LabelPropertyName = New System.Windows.Forms.Label()
        Me.TextBoxPropertyName = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.LabelPropertySet = New System.Windows.Forms.Label()
        Me.LabelEnglishName = New System.Windows.Forms.Label()
        Me.ComboBoxPropertySet = New System.Windows.Forms.ComboBox()
        Me.TextBoxEnglishName = New System.Windows.Forms.TextBox()
        Me.ExTableLayoutPanel3 = New Housekeeper.ExTableLayoutPanel()
        Me.ExTableLayoutPanel4 = New Housekeeper.ExTableLayoutPanel()
        Me.ExTableLayoutPanel5 = New Housekeeper.ExTableLayoutPanel()
        Me.ExTableLayoutPanel6 = New Housekeeper.ExTableLayoutPanel()
        Me.ExTableLayoutPanel3.SuspendLayout()
        Me.ExTableLayoutPanel4.SuspendLayout()
        Me.ExTableLayoutPanel5.SuspendLayout()
        Me.ExTableLayoutPanel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelPropertyName
        '
        Me.LabelPropertyName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelPropertyName.AutoSize = True
        Me.LabelPropertyName.Location = New System.Drawing.Point(104, 4)
        Me.LabelPropertyName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelPropertyName.Name = "LabelPropertyName"
        Me.LabelPropertyName.Size = New System.Drawing.Size(87, 15)
        Me.LabelPropertyName.TabIndex = 0
        Me.LabelPropertyName.Text = "Property Name"
        '
        'TextBoxPropertyName
        '
        Me.TextBoxPropertyName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPropertyName.Location = New System.Drawing.Point(103, 3)
        Me.TextBoxPropertyName.Name = "TextBoxPropertyName"
        Me.TextBoxPropertyName.Size = New System.Drawing.Size(164, 23)
        Me.TextBoxPropertyName.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.Location = New System.Drawing.Point(288, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(69, 28)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonCancel.Location = New System.Drawing.Point(365, 3)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(67, 28)
        Me.ButtonCancel.TabIndex = 3
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'LabelPropertySet
        '
        Me.LabelPropertySet.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelPropertySet.AutoSize = True
        Me.LabelPropertySet.Location = New System.Drawing.Point(3, 4)
        Me.LabelPropertySet.Name = "LabelPropertySet"
        Me.LabelPropertySet.Size = New System.Drawing.Size(71, 15)
        Me.LabelPropertySet.TabIndex = 0
        Me.LabelPropertySet.Text = "Property Set"
        '
        'LabelEnglishName
        '
        Me.LabelEnglishName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelEnglishName.AutoSize = True
        Me.LabelEnglishName.Location = New System.Drawing.Point(273, 4)
        Me.LabelEnglishName.Name = "LabelEnglishName"
        Me.LabelEnglishName.Size = New System.Drawing.Size(120, 15)
        Me.LabelEnglishName.TabIndex = 1
        Me.LabelEnglishName.Text = "English Name (if any)"
        '
        'ComboBoxPropertySet
        '
        Me.ComboBoxPropertySet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxPropertySet.FormattingEnabled = True
        Me.ComboBoxPropertySet.Items.AddRange(New Object() {"", "System", "Custom"})
        Me.ComboBoxPropertySet.Location = New System.Drawing.Point(3, 3)
        Me.ComboBoxPropertySet.Name = "ComboBoxPropertySet"
        Me.ComboBoxPropertySet.Size = New System.Drawing.Size(94, 23)
        Me.ComboBoxPropertySet.TabIndex = 2
        '
        'TextBoxEnglishName
        '
        Me.TextBoxEnglishName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEnglishName.Location = New System.Drawing.Point(273, 3)
        Me.TextBoxEnglishName.Name = "TextBoxEnglishName"
        Me.TextBoxEnglishName.Size = New System.Drawing.Size(164, 23)
        Me.TextBoxEnglishName.TabIndex = 3
        '
        'ExTableLayoutPanel3
        '
        Me.ExTableLayoutPanel3.ColumnCount = 1
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel3.Controls.Add(Me.ExTableLayoutPanel4, 0, 0)
        Me.ExTableLayoutPanel3.Controls.Add(Me.ExTableLayoutPanel5, 0, 1)
        Me.ExTableLayoutPanel3.Controls.Add(Me.ExTableLayoutPanel6, 0, 2)
        Me.ExTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel3.Name = "ExTableLayoutPanel3"
        Me.ExTableLayoutPanel3.RowCount = 4
        Me.ExTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.ExTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.ExTableLayoutPanel3.Size = New System.Drawing.Size(446, 105)
        Me.ExTableLayoutPanel3.TabIndex = 5
        Me.ExTableLayoutPanel3.Task = Nothing
        '
        'ExTableLayoutPanel4
        '
        Me.ExTableLayoutPanel4.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ExTableLayoutPanel4.ColumnCount = 3
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelEnglishName, 2, 0)
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelPropertyName, 1, 0)
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelPropertySet, 0, 0)
        Me.ExTableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel4.Location = New System.Drawing.Point(3, 3)
        Me.ExTableLayoutPanel4.Name = "ExTableLayoutPanel4"
        Me.ExTableLayoutPanel4.RowCount = 1
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.ExTableLayoutPanel4.Size = New System.Drawing.Size(440, 24)
        Me.ExTableLayoutPanel4.TabIndex = 0
        Me.ExTableLayoutPanel4.Task = Nothing
        '
        'ExTableLayoutPanel5
        '
        Me.ExTableLayoutPanel5.ColumnCount = 3
        Me.ExTableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel5.Controls.Add(Me.TextBoxEnglishName, 2, 0)
        Me.ExTableLayoutPanel5.Controls.Add(Me.TextBoxPropertyName, 1, 0)
        Me.ExTableLayoutPanel5.Controls.Add(Me.ComboBoxPropertySet, 0, 0)
        Me.ExTableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel5.Location = New System.Drawing.Point(3, 33)
        Me.ExTableLayoutPanel5.Name = "ExTableLayoutPanel5"
        Me.ExTableLayoutPanel5.RowCount = 1
        Me.ExTableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.ExTableLayoutPanel5.Size = New System.Drawing.Size(440, 29)
        Me.ExTableLayoutPanel5.TabIndex = 1
        Me.ExTableLayoutPanel5.Task = Nothing
        '
        'ExTableLayoutPanel6
        '
        Me.ExTableLayoutPanel6.ColumnCount = 3
        Me.ExTableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.ExTableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.ExTableLayoutPanel6.Controls.Add(Me.Button1, 1, 0)
        Me.ExTableLayoutPanel6.Controls.Add(Me.ButtonCancel, 2, 0)
        Me.ExTableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel6.Location = New System.Drawing.Point(3, 68)
        Me.ExTableLayoutPanel6.Name = "ExTableLayoutPanel6"
        Me.ExTableLayoutPanel6.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.ExTableLayoutPanel6.RowCount = 1
        Me.ExTableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel6.Size = New System.Drawing.Size(440, 34)
        Me.ExTableLayoutPanel6.TabIndex = 2
        Me.ExTableLayoutPanel6.Task = Nothing
        '
        'FormPropertyListCustomizeManualEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(446, 105)
        Me.Controls.Add(Me.ExTableLayoutPanel3)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormPropertyListCustomizeManualEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Property Manually"
        Me.ExTableLayoutPanel3.ResumeLayout(False)
        Me.ExTableLayoutPanel4.ResumeLayout(False)
        Me.ExTableLayoutPanel4.PerformLayout()
        Me.ExTableLayoutPanel5.ResumeLayout(False)
        Me.ExTableLayoutPanel5.PerformLayout()
        Me.ExTableLayoutPanel6.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LabelPropertyName As Label
    Friend WithEvents TextBoxPropertyName As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents LabelPropertySet As Label
    Friend WithEvents LabelEnglishName As Label
    Friend WithEvents ComboBoxPropertySet As ComboBox
    Friend WithEvents TextBoxEnglishName As TextBox
    Friend WithEvents ExTableLayoutPanel3 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel4 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel5 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel6 As ExTableLayoutPanel
End Class
