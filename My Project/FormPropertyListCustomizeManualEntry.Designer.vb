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
        Me.LabelPropname = New System.Windows.Forms.Label()
        Me.TextBoxPropname = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LabelPropname
        '
        Me.LabelPropname.AutoSize = True
        Me.LabelPropname.Location = New System.Drawing.Point(13, 13)
        Me.LabelPropname.Name = "LabelPropname"
        Me.LabelPropname.Size = New System.Drawing.Size(102, 13)
        Me.LabelPropname.TabIndex = 0
        Me.LabelPropname.Text = "Enter property name"
        '
        'TextBoxPropname
        '
        Me.TextBoxPropname.Location = New System.Drawing.Point(16, 30)
        Me.TextBoxPropname.Name = "TextBoxPropname"
        Me.TextBoxPropname.Size = New System.Drawing.Size(261, 20)
        Me.TextBoxPropname.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(116, 68)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(204, 68)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 3
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'FormPropertyListCustomizeManualEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 102)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBoxPropname)
        Me.Controls.Add(Me.LabelPropname)
        Me.Name = "FormPropertyListCustomizeManualEntry"
        Me.Text = "Add Property Manually"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelPropname As Label
    Friend WithEvents TextBoxPropname As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents ButtonCancel As Button
End Class
