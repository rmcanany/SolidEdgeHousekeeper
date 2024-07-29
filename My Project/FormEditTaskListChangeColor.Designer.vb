<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEditTaskListChangeColor
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
        Me.ComboBoxColor = New System.Windows.Forms.ComboBox()
        Me.LabelColor = New System.Windows.Forms.Label()
        Me.LabelSaturation = New System.Windows.Forms.Label()
        Me.LabelBrightness = New System.Windows.Forms.Label()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonColor = New System.Windows.Forms.Button()
        Me.NumericUpDownSaturation = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownBrightness = New System.Windows.Forms.NumericUpDown()
        CType(Me.NumericUpDownSaturation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownBrightness, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBoxColor
        '
        Me.ComboBoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxColor.FormattingEnabled = True
        Me.ComboBoxColor.Location = New System.Drawing.Point(122, 11)
        Me.ComboBoxColor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ComboBoxColor.Name = "ComboBoxColor"
        Me.ComboBoxColor.Size = New System.Drawing.Size(140, 24)
        Me.ComboBoxColor.Sorted = True
        Me.ComboBoxColor.TabIndex = 1
        '
        'LabelColor
        '
        Me.LabelColor.AutoSize = True
        Me.LabelColor.Location = New System.Drawing.Point(75, 14)
        Me.LabelColor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelColor.Name = "LabelColor"
        Me.LabelColor.Size = New System.Drawing.Size(36, 16)
        Me.LabelColor.TabIndex = 5
        Me.LabelColor.Text = "Color"
        '
        'LabelSaturation
        '
        Me.LabelSaturation.AutoSize = True
        Me.LabelSaturation.Location = New System.Drawing.Point(14, 54)
        Me.LabelSaturation.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSaturation.Name = "LabelSaturation"
        Me.LabelSaturation.Size = New System.Drawing.Size(88, 16)
        Me.LabelSaturation.TabIndex = 6
        Me.LabelSaturation.Text = "Saturation (0-1)"
        '
        'LabelBrightness
        '
        Me.LabelBrightness.AutoSize = True
        Me.LabelBrightness.Location = New System.Drawing.Point(14, 88)
        Me.LabelBrightness.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelBrightness.Name = "LabelBrightness"
        Me.LabelBrightness.Size = New System.Drawing.Size(88, 16)
        Me.LabelBrightness.TabIndex = 7
        Me.LabelBrightness.Text = "Brightness (0-1)"
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(103, 123)
        Me.ButtonOK.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(88, 29)
        Me.ButtonOK.TabIndex = 4
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(214, 122)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(88, 29)
        Me.ButtonCancel.TabIndex = 5
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonColor
        '
        Me.ButtonColor.Location = New System.Drawing.Point(194, 48)
        Me.ButtonColor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonColor.Name = "ButtonColor"
        Me.ButtonColor.Size = New System.Drawing.Size(104, 63)
        Me.ButtonColor.TabIndex = 10
        Me.ButtonColor.TabStop = False
        Me.ButtonColor.UseVisualStyleBackColor = True
        '
        'NumericUpDownSaturation
        '
        Me.NumericUpDownSaturation.DecimalPlaces = 2
        Me.NumericUpDownSaturation.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.NumericUpDownSaturation.Location = New System.Drawing.Point(122, 52)
        Me.NumericUpDownSaturation.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.NumericUpDownSaturation.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDownSaturation.Name = "NumericUpDownSaturation"
        Me.NumericUpDownSaturation.Size = New System.Drawing.Size(51, 23)
        Me.NumericUpDownSaturation.TabIndex = 2
        '
        'NumericUpDownBrightness
        '
        Me.NumericUpDownBrightness.DecimalPlaces = 2
        Me.NumericUpDownBrightness.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.NumericUpDownBrightness.Location = New System.Drawing.Point(121, 85)
        Me.NumericUpDownBrightness.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.NumericUpDownBrightness.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDownBrightness.Name = "NumericUpDownBrightness"
        Me.NumericUpDownBrightness.Size = New System.Drawing.Size(52, 23)
        Me.NumericUpDownBrightness.TabIndex = 3
        '
        'FormEditTaskListChangeColor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(315, 161)
        Me.Controls.Add(Me.NumericUpDownBrightness)
        Me.Controls.Add(Me.NumericUpDownSaturation)
        Me.Controls.Add(Me.ButtonColor)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.LabelBrightness)
        Me.Controls.Add(Me.LabelSaturation)
        Me.Controls.Add(Me.LabelColor)
        Me.Controls.Add(Me.ComboBoxColor)
        Me.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "FormEditTaskListChangeColor"
        Me.Text = "Change Color"
        CType(Me.NumericUpDownSaturation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownBrightness, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ComboBoxColor As ComboBox
    Friend WithEvents LabelColor As Label
    Friend WithEvents LabelSaturation As Label
    Friend WithEvents LabelBrightness As Label
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonColor As Button
    Friend WithEvents NumericUpDownSaturation As NumericUpDown
    Friend WithEvents NumericUpDownBrightness As NumericUpDown
End Class
