<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormEditInteractively
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
        Me.PauseButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.DoNotSaveButton = New System.Windows.Forms.Button()
        Me.AbortButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'PauseButton
        '
        Me.PauseButton.Location = New System.Drawing.Point(15, 10)
        Me.PauseButton.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PauseButton.Name = "PauseButton"
        Me.PauseButton.Size = New System.Drawing.Size(260, 55)
        Me.PauseButton.TabIndex = 0
        Me.PauseButton.Text = "Pause"
        Me.PauseButton.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.SaveButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveButton.Location = New System.Drawing.Point(7, 111)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(88, 29)
        Me.SaveButton.TabIndex = 2
        Me.SaveButton.Text = "Save"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'DoNotSaveButton
        '
        Me.DoNotSaveButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DoNotSaveButton.Location = New System.Drawing.Point(102, 111)
        Me.DoNotSaveButton.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DoNotSaveButton.Name = "DoNotSaveButton"
        Me.DoNotSaveButton.Size = New System.Drawing.Size(88, 29)
        Me.DoNotSaveButton.TabIndex = 3
        Me.DoNotSaveButton.Text = "Don't Save"
        Me.DoNotSaveButton.UseVisualStyleBackColor = True
        '
        'AbortButton
        '
        Me.AbortButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AbortButton.Location = New System.Drawing.Point(197, 111)
        Me.AbortButton.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.AbortButton.Name = "AbortButton"
        Me.AbortButton.Size = New System.Drawing.Size(88, 29)
        Me.AbortButton.TabIndex = 4
        Me.AbortButton.Text = "Abort"
        Me.AbortButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Variable Display", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(20, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(249, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||"
        '
        'FormEditInteractively
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(294, 146)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.AbortButton)
        Me.Controls.Add(Me.DoNotSaveButton)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.PauseButton)
        Me.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "FormEditInteractively"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Interactive Edit"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PauseButton As Button
    Friend WithEvents SaveButton As Button
    Friend WithEvents DoNotSaveButton As Button
    Friend WithEvents AbortButton As Button
    Friend WithEvents Label1 As Label
End Class
