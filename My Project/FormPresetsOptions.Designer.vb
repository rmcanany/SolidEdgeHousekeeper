<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPresetsOptions
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
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.CheckBoxPresetsSaveFileFilters = New System.Windows.Forms.CheckBox()
        Me.ExTableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.ColumnCount = 3
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonOK, 1, 7)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonCancel, 2, 7)
        Me.ExTableLayoutPanel1.Controls.Add(Me.CheckBoxPresetsSaveFileFilters, 0, 0)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 8
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(340, 89)
        Me.ExTableLayoutPanel1.TabIndex = 0
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ButtonOK.Location = New System.Drawing.Point(183, 63)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(74, 23)
        Me.ButtonOK.TabIndex = 0
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ButtonCancel.Location = New System.Drawing.Point(263, 63)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(74, 23)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'CheckBoxPresetsSaveFileFilters
        '
        Me.CheckBoxPresetsSaveFileFilters.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxPresetsSaveFileFilters.AutoSize = True
        Me.ExTableLayoutPanel1.SetColumnSpan(Me.CheckBoxPresetsSaveFileFilters, 3)
        Me.CheckBoxPresetsSaveFileFilters.Location = New System.Drawing.Point(3, 6)
        Me.CheckBoxPresetsSaveFileFilters.Name = "CheckBoxPresetsSaveFileFilters"
        Me.CheckBoxPresetsSaveFileFilters.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CheckBoxPresetsSaveFileFilters.Size = New System.Drawing.Size(133, 17)
        Me.CheckBoxPresetsSaveFileFilters.TabIndex = 2
        Me.CheckBoxPresetsSaveFileFilters.Text = "Save file filter settings"
        Me.CheckBoxPresetsSaveFileFilters.UseVisualStyleBackColor = True
        '
        'FormPresetsOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(340, 89)
        Me.Controls.Add(Me.ExTableLayoutPanel1)
        Me.Name = "FormPresetsOptions"
        Me.Text = "Presets Options"
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents CheckBoxPresetsSaveFileFilters As CheckBox
End Class
