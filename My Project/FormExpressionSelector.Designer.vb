<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormExpressionSelector
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
        Me.ComboBoxExpressionNames = New System.Windows.Forms.ComboBox()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ExTableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.ExTableLayoutPanel1.ColumnCount = 3
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.ExTableLayoutPanel1.Controls.Add(Me.ComboBoxExpressionNames, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonOK, 1, 1)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonCancel, 2, 1)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 2
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(301, 155)
        Me.ExTableLayoutPanel1.TabIndex = 0
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'ComboBoxExpressionNames
        '
        Me.ComboBoxExpressionNames.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExTableLayoutPanel1.SetColumnSpan(Me.ComboBoxExpressionNames, 3)
        Me.ComboBoxExpressionNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxExpressionNames.FormattingEnabled = True
        Me.ComboBoxExpressionNames.Location = New System.Drawing.Point(3, 3)
        Me.ComboBoxExpressionNames.Name = "ComboBoxExpressionNames"
        Me.ComboBoxExpressionNames.Size = New System.Drawing.Size(295, 21)
        Me.ComboBoxExpressionNames.TabIndex = 0
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(142, 129)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 1
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(223, 129)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'FormExpressionSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(301, 155)
        Me.Controls.Add(Me.ExTableLayoutPanel1)
        Me.Name = "FormExpressionSelector"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Expression Selector"
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents ComboBoxExpressionNames As ComboBox
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
End Class
