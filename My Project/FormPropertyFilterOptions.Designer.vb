<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPropertyFilterOptions
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
        Me.ButtonHelp = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.CheckBoxPropertyFilterIncludeDraftModel = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPropertyFilterIncludeDraftItself = New System.Windows.Forms.CheckBox()
        Me.ExTableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.ColumnCount = 4
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonHelp, 3, 2)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonCancel, 2, 2)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonOK, 1, 2)
        Me.ExTableLayoutPanel1.Controls.Add(Me.CheckBoxPropertyFilterIncludeDraftModel, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.CheckBoxPropertyFilterIncludeDraftItself, 0, 1)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 3
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(362, 136)
        Me.ExTableLayoutPanel1.TabIndex = 0
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'ButtonHelp
        '
        Me.ButtonHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ButtonHelp.Location = New System.Drawing.Point(270, 106)
        Me.ButtonHelp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonHelp.Name = "ButtonHelp"
        Me.ButtonHelp.Size = New System.Drawing.Size(88, 27)
        Me.ButtonHelp.TabIndex = 0
        Me.ButtonHelp.Text = "Help"
        Me.ButtonHelp.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ButtonCancel.Location = New System.Drawing.Point(174, 106)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(88, 27)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ButtonOK.Location = New System.Drawing.Point(78, 106)
        Me.ButtonOK.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(88, 27)
        Me.ButtonOK.TabIndex = 2
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'CheckBoxPropertyFilterIncludeDraftModel
        '
        Me.CheckBoxPropertyFilterIncludeDraftModel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxPropertyFilterIncludeDraftModel.AutoSize = True
        Me.ExTableLayoutPanel1.SetColumnSpan(Me.CheckBoxPropertyFilterIncludeDraftModel, 4)
        Me.CheckBoxPropertyFilterIncludeDraftModel.Location = New System.Drawing.Point(4, 8)
        Me.CheckBoxPropertyFilterIncludeDraftModel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxPropertyFilterIncludeDraftModel.Name = "CheckBoxPropertyFilterIncludeDraftModel"
        Me.CheckBoxPropertyFilterIncludeDraftModel.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.CheckBoxPropertyFilterIncludeDraftModel.Size = New System.Drawing.Size(211, 19)
        Me.CheckBoxPropertyFilterIncludeDraftModel.TabIndex = 3
        Me.CheckBoxPropertyFilterIncludeDraftModel.Text = "Include Draft file models in search"
        Me.CheckBoxPropertyFilterIncludeDraftModel.UseVisualStyleBackColor = True
        '
        'CheckBoxPropertyFilterIncludeDraftItself
        '
        Me.CheckBoxPropertyFilterIncludeDraftItself.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxPropertyFilterIncludeDraftItself.AutoSize = True
        Me.ExTableLayoutPanel1.SetColumnSpan(Me.CheckBoxPropertyFilterIncludeDraftItself, 4)
        Me.CheckBoxPropertyFilterIncludeDraftItself.Location = New System.Drawing.Point(4, 43)
        Me.CheckBoxPropertyFilterIncludeDraftItself.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxPropertyFilterIncludeDraftItself.Name = "CheckBoxPropertyFilterIncludeDraftItself"
        Me.CheckBoxPropertyFilterIncludeDraftItself.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.CheckBoxPropertyFilterIncludeDraftItself.Size = New System.Drawing.Size(197, 19)
        Me.CheckBoxPropertyFilterIncludeDraftItself.TabIndex = 4
        Me.CheckBoxPropertyFilterIncludeDraftItself.Text = "Include Draft file itself in search"
        Me.CheckBoxPropertyFilterIncludeDraftItself.UseVisualStyleBackColor = True
        '
        'FormPropertyFilterOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(362, 136)
        Me.Controls.Add(Me.ExTableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "FormPropertyFilterOptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Property Filter Options"
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents ButtonHelp As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOK As Button
    Friend WithEvents CheckBoxPropertyFilterIncludeDraftModel As CheckBox
    Friend WithEvents CheckBoxPropertyFilterIncludeDraftItself As CheckBox
End Class
