<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSheetSelector
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
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.RadioButtonAnsi = New System.Windows.Forms.RadioButton()
        Me.RadioButtonIso = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAll = New System.Windows.Forms.RadioButton()
        Me.CheckBoxSelectAll = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(21, 400)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(94, 27)
        Me.ButtonOK.TabIndex = 0
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(125, 400)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(105, 27)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.CheckOnClick = True
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(12, 14)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(215, 276)
        Me.CheckedListBox1.TabIndex = 2
        '
        'RadioButtonAnsi
        '
        Me.RadioButtonAnsi.AutoSize = True
        Me.RadioButtonAnsi.Location = New System.Drawing.Point(10, 300)
        Me.RadioButtonAnsi.Name = "RadioButtonAnsi"
        Me.RadioButtonAnsi.Size = New System.Drawing.Size(102, 19)
        Me.RadioButtonAnsi.TabIndex = 3
        Me.RadioButtonAnsi.TabStop = True
        Me.RadioButtonAnsi.Text = "Show ANSI only"
        Me.RadioButtonAnsi.UseVisualStyleBackColor = True
        '
        'RadioButtonIso
        '
        Me.RadioButtonIso.AutoSize = True
        Me.RadioButtonIso.Location = New System.Drawing.Point(10, 325)
        Me.RadioButtonIso.Name = "RadioButtonIso"
        Me.RadioButtonIso.Size = New System.Drawing.Size(95, 19)
        Me.RadioButtonIso.TabIndex = 4
        Me.RadioButtonIso.TabStop = True
        Me.RadioButtonIso.Text = "Show ISO only"
        Me.RadioButtonIso.UseVisualStyleBackColor = True
        '
        'RadioButtonAll
        '
        Me.RadioButtonAll.AutoSize = True
        Me.RadioButtonAll.Location = New System.Drawing.Point(10, 350)
        Me.RadioButtonAll.Name = "RadioButtonAll"
        Me.RadioButtonAll.Size = New System.Drawing.Size(65, 19)
        Me.RadioButtonAll.TabIndex = 5
        Me.RadioButtonAll.TabStop = True
        Me.RadioButtonAll.Text = "Show all"
        Me.RadioButtonAll.UseVisualStyleBackColor = True
        '
        'CheckBoxSelectAll
        '
        Me.CheckBoxSelectAll.AutoSize = True
        Me.CheckBoxSelectAll.Location = New System.Drawing.Point(10, 375)
        Me.CheckBoxSelectAll.Name = "CheckBoxSelectAll"
        Me.CheckBoxSelectAll.Size = New System.Drawing.Size(100, 19)
        Me.CheckBoxSelectAll.TabIndex = 6
        Me.CheckBoxSelectAll.Text = "Select all/none"
        Me.CheckBoxSelectAll.UseVisualStyleBackColor = True
        '
        'FormSheetSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(247, 443)
        Me.Controls.Add(Me.CheckBoxSelectAll)
        Me.Controls.Add(Me.RadioButtonAll)
        Me.Controls.Add(Me.RadioButtonIso)
        Me.Controls.Add(Me.RadioButtonAnsi)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Font = New System.Drawing.Font("Segoe UI Variable Display", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FormSheetSelector"
        Me.Text = "Sheet Selector"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents RadioButtonAnsi As RadioButton
    Friend WithEvents RadioButtonIso As RadioButton
    Friend WithEvents RadioButtonAll As RadioButton
    Friend WithEvents CheckBoxSelectAll As CheckBox
End Class
