<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPropertyPicker
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ButtonPropOnly = New System.Windows.Forms.ToolStripButton()
        Me.ButtonPropAndIndex = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonShowAll = New System.Windows.Forms.ToolStripButton()
        Me.ComboBoxProperties = New System.Windows.Forms.ComboBox()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ButtonPropOnly, Me.ButtonPropAndIndex, Me.ToolStripSeparator1, Me.ButtonShowAll})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(299, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ButtonPropOnly
        '
        Me.ButtonPropOnly.Checked = True
        Me.ButtonPropOnly.CheckOnClick = True
        Me.ButtonPropOnly.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ButtonPropOnly.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPropOnly.Image = Global.Housekeeper.My.Resources.Resources.Checked
        Me.ButtonPropOnly.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonPropOnly.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonPropOnly.Name = "ButtonPropOnly"
        Me.ButtonPropOnly.Size = New System.Drawing.Size(53, 22)
        Me.ButtonPropOnly.Text = "%{~}"
        Me.ButtonPropOnly.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonPropOnly.ToolTipText = "Property only"
        '
        'ButtonPropAndIndex
        '
        Me.ButtonPropAndIndex.CheckOnClick = True
        Me.ButtonPropAndIndex.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPropAndIndex.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.ButtonPropAndIndex.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonPropAndIndex.Name = "ButtonPropAndIndex"
        Me.ButtonPropAndIndex.Size = New System.Drawing.Size(70, 22)
        Me.ButtonPropAndIndex.Text = "%{~|R1}"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ButtonShowAll
        '
        Me.ButtonShowAll.CheckOnClick = True
        Me.ButtonShowAll.Image = Global.Housekeeper.My.Resources.Resources.Unchecked
        Me.ButtonShowAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonShowAll.Name = "ButtonShowAll"
        Me.ButtonShowAll.Size = New System.Drawing.Size(106, 22)
        Me.ButtonShowAll.Text = "Show All Props"
        '
        'ComboBoxProperties
        '
        Me.ComboBoxProperties.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxProperties.FormattingEnabled = True
        Me.ComboBoxProperties.Location = New System.Drawing.Point(12, 40)
        Me.ComboBoxProperties.Name = "ComboBoxProperties"
        Me.ComboBoxProperties.Size = New System.Drawing.Size(272, 24)
        Me.ComboBoxProperties.TabIndex = 1
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(121, 81)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 2
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(209, 80)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 3
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'FormPropertyPicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(299, 115)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ComboBoxProperties)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormPropertyPicker"
        Me.Text = "Property Selector"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ButtonPropOnly As ToolStripButton
    Friend WithEvents ButtonPropAndIndex As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ButtonShowAll As ToolStripButton
    Friend WithEvents ComboBoxProperties As ComboBox
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
End Class
