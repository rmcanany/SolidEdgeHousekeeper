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
        Me.TextBoxColorSaturation = New System.Windows.Forms.TextBox()
        Me.TextBoxColorBrightness = New System.Windows.Forms.TextBox()
        Me.LabelColor = New System.Windows.Forms.Label()
        Me.LabelSaturation = New System.Windows.Forms.Label()
        Me.LabelBrightness = New System.Windows.Forms.Label()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonColor = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ComboBoxColor
        '
        Me.ComboBoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxColor.FormattingEnabled = True
        Me.ComboBoxColor.Location = New System.Drawing.Point(105, 20)
        Me.ComboBoxColor.Name = "ComboBoxColor"
        Me.ComboBoxColor.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxColor.Sorted = True
        Me.ComboBoxColor.TabIndex = 1
        '
        'TextBoxColorSaturation
        '
        Me.TextBoxColorSaturation.Location = New System.Drawing.Point(105, 51)
        Me.TextBoxColorSaturation.Name = "TextBoxColorSaturation"
        Me.TextBoxColorSaturation.Size = New System.Drawing.Size(50, 20)
        Me.TextBoxColorSaturation.TabIndex = 2
        '
        'TextBoxColorBrightness
        '
        Me.TextBoxColorBrightness.Location = New System.Drawing.Point(105, 81)
        Me.TextBoxColorBrightness.Name = "TextBoxColorBrightness"
        Me.TextBoxColorBrightness.Size = New System.Drawing.Size(50, 20)
        Me.TextBoxColorBrightness.TabIndex = 3
        '
        'LabelColor
        '
        Me.LabelColor.AutoSize = True
        Me.LabelColor.Location = New System.Drawing.Point(61, 25)
        Me.LabelColor.Name = "LabelColor"
        Me.LabelColor.Size = New System.Drawing.Size(31, 13)
        Me.LabelColor.TabIndex = 5
        Me.LabelColor.Text = "Color"
        '
        'LabelSaturation
        '
        Me.LabelSaturation.AutoSize = True
        Me.LabelSaturation.Location = New System.Drawing.Point(15, 55)
        Me.LabelSaturation.Name = "LabelSaturation"
        Me.LabelSaturation.Size = New System.Drawing.Size(79, 13)
        Me.LabelSaturation.TabIndex = 6
        Me.LabelSaturation.Text = "Saturation (0-1)"
        '
        'LabelBrightness
        '
        Me.LabelBrightness.AutoSize = True
        Me.LabelBrightness.Location = New System.Drawing.Point(15, 85)
        Me.LabelBrightness.Name = "LabelBrightness"
        Me.LabelBrightness.Size = New System.Drawing.Size(80, 13)
        Me.LabelBrightness.TabIndex = 7
        Me.LabelBrightness.Text = "Brightness (0-1)"
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(85, 128)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 8
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(180, 127)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 9
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonColor
        '
        Me.ButtonColor.Location = New System.Drawing.Point(180, 47)
        Me.ButtonColor.Name = "ButtonColor"
        Me.ButtonColor.Size = New System.Drawing.Size(75, 51)
        Me.ButtonColor.TabIndex = 10
        Me.ButtonColor.UseVisualStyleBackColor = True
        '
        'FormEditTaskListChangeColor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(270, 160)
        Me.Controls.Add(Me.ButtonColor)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.LabelBrightness)
        Me.Controls.Add(Me.LabelSaturation)
        Me.Controls.Add(Me.LabelColor)
        Me.Controls.Add(Me.TextBoxColorBrightness)
        Me.Controls.Add(Me.TextBoxColorSaturation)
        Me.Controls.Add(Me.ComboBoxColor)
        Me.Name = "FormEditTaskListChangeColor"
        Me.Text = "Change Color"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ComboBoxColor As ComboBox
    Friend WithEvents TextBoxColorSaturation As TextBox
    Friend WithEvents TextBoxColorBrightness As TextBox
    Friend WithEvents LabelColor As Label
    Friend WithEvents LabelSaturation As Label
    Friend WithEvents LabelBrightness As Label
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonColor As Button
End Class
