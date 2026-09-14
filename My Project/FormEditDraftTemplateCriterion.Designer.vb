<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEditDraftTemplateCriterion
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonTemplate = New System.Windows.Forms.Button()
        Me.TextBoxPropertyFormula = New System.Windows.Forms.TextBox()
        Me.TextBoxValue = New System.Windows.Forms.TextBox()
        Me.TextBoxTemplate = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonPropertyFormula = New System.Windows.Forms.Button()
        Me.LabelValue = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonTemplate, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxPropertyFormula, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxValue, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxTemplate, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonPropertyFormula, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelValue, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(443, 144)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ButtonTemplate
        '
        Me.ButtonTemplate.Location = New System.Drawing.Point(4, 63)
        Me.ButtonTemplate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonTemplate.Name = "ButtonTemplate"
        Me.ButtonTemplate.Size = New System.Drawing.Size(88, 23)
        Me.ButtonTemplate.TabIndex = 2
        Me.ButtonTemplate.Text = "Template"
        Me.ButtonTemplate.UseVisualStyleBackColor = True
        '
        'TextBoxPropertyFormula
        '
        Me.TextBoxPropertyFormula.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPropertyFormula.Location = New System.Drawing.Point(100, 3)
        Me.TextBoxPropertyFormula.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextBoxPropertyFormula.Name = "TextBoxPropertyFormula"
        Me.TextBoxPropertyFormula.Size = New System.Drawing.Size(339, 23)
        Me.TextBoxPropertyFormula.TabIndex = 3
        Me.TextBoxPropertyFormula.Text = "%{System.Template}"
        '
        'TextBoxValue
        '
        Me.TextBoxValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxValue.Location = New System.Drawing.Point(100, 33)
        Me.TextBoxValue.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextBoxValue.Name = "TextBoxValue"
        Me.TextBoxValue.Size = New System.Drawing.Size(339, 23)
        Me.TextBoxValue.TabIndex = 4
        Me.TextBoxValue.Text = "Normal.dft"
        '
        'TextBoxTemplate
        '
        Me.TextBoxTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplate.Location = New System.Drawing.Point(100, 63)
        Me.TextBoxTemplate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextBoxTemplate.Name = "TextBoxTemplate"
        Me.TextBoxTemplate.Size = New System.Drawing.Size(339, 23)
        Me.TextBoxTemplate.TabIndex = 5
        Me.TextBoxTemplate.Text = "C:\CAD\Normal.dft"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonCancel, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonOK, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(100, 93)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(339, 48)
        Me.TableLayoutPanel2.TabIndex = 6
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(260, 22)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(177, 22)
        Me.ButtonOK.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 0
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonPropertyFormula
        '
        Me.ButtonPropertyFormula.Location = New System.Drawing.Point(4, 3)
        Me.ButtonPropertyFormula.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonPropertyFormula.Name = "ButtonPropertyFormula"
        Me.ButtonPropertyFormula.Size = New System.Drawing.Size(88, 23)
        Me.ButtonPropertyFormula.TabIndex = 7
        Me.ButtonPropertyFormula.Text = "Property"
        Me.ButtonPropertyFormula.UseVisualStyleBackColor = True
        '
        'LabelValue
        '
        Me.LabelValue.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LabelValue.AutoSize = True
        Me.LabelValue.Location = New System.Drawing.Point(51, 37)
        Me.LabelValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelValue.Name = "LabelValue"
        Me.LabelValue.Padding = New System.Windows.Forms.Padding(0, 0, 6, 0)
        Me.LabelValue.Size = New System.Drawing.Size(41, 15)
        Me.LabelValue.TabIndex = 8
        Me.LabelValue.Text = "Value"
        '
        'FormEditDraftTemplateCriterion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 144)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "FormEditDraftTemplateCriterion"
        Me.Text = "Edit Template Criterion"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ButtonPropertyFormula As Button
    'Friend WithEvents LabelValue As Label
    Friend WithEvents ButtonTemplate As Button
    Friend WithEvents TextBoxPropertyFormula As TextBox
    Friend WithEvents TextBoxValue As TextBox
    Friend WithEvents TextBoxTemplate As TextBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents LabelValue As Label
End Class
