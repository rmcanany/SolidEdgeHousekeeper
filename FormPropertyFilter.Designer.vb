<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPropertyFilter
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
        Me.ButtonPropertyFilterCancel = New System.Windows.Forms.Button()
        Me.ButtonPropertyFilterOK = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ButtonPropertyFilterDelete = New System.Windows.Forms.Button()
        Me.ButtonFormula = New System.Windows.Forms.Button()
        Me.LabelFormula = New System.Windows.Forms.Label()
        Me.TextBoxFormula = New System.Windows.Forms.TextBox()
        Me.LabelPropertyFilterValue = New System.Windows.Forms.Label()
        Me.LabelPropertyFilterComparison = New System.Windows.Forms.Label()
        Me.LabelPropertyFilterPropertyName = New System.Windows.Forms.Label()
        Me.LabelPropertyFilterPropertySet = New System.Windows.Forms.Label()
        Me.LabelPropertyFilterName = New System.Windows.Forms.Label()
        Me.ListViewPropertyFilter = New System.Windows.Forms.ListView()
        Me.ButtonPropertyFilterRemove = New System.Windows.Forms.Button()
        Me.ButtonPropertyFilterAdd = New System.Windows.Forms.Button()
        Me.TextBoxPropertyFilterValue = New System.Windows.Forms.TextBox()
        Me.ComboBoxPropertyFilterComparison = New System.Windows.Forms.ComboBox()
        Me.TextBoxPropertyFilterPropertyName = New System.Windows.Forms.TextBox()
        Me.ComboBoxPropertyFilterPropertySet = New System.Windows.Forms.ComboBox()
        Me.ButtonPropertyFilterSave = New System.Windows.Forms.Button()
        Me.ComboBoxPropertyFilterName = New System.Windows.Forms.ComboBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TextBoxReadme = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonPropertyFilterCancel
        '
        Me.ButtonPropertyFilterCancel.Location = New System.Drawing.Point(650, 525)
        Me.ButtonPropertyFilterCancel.Name = "ButtonPropertyFilterCancel"
        Me.ButtonPropertyFilterCancel.Size = New System.Drawing.Size(100, 23)
        Me.ButtonPropertyFilterCancel.TabIndex = 22
        Me.ButtonPropertyFilterCancel.Text = "Cancel"
        Me.ButtonPropertyFilterCancel.UseVisualStyleBackColor = True
        '
        'ButtonPropertyFilterOK
        '
        Me.ButtonPropertyFilterOK.Location = New System.Drawing.Point(525, 525)
        Me.ButtonPropertyFilterOK.Name = "ButtonPropertyFilterOK"
        Me.ButtonPropertyFilterOK.Size = New System.Drawing.Size(100, 23)
        Me.ButtonPropertyFilterOK.TabIndex = 21
        Me.ButtonPropertyFilterOK.Text = "OK"
        Me.ButtonPropertyFilterOK.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(780, 500)
        Me.TabControl1.TabIndex = 23
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.ButtonPropertyFilterDelete)
        Me.TabPage1.Controls.Add(Me.ButtonFormula)
        Me.TabPage1.Controls.Add(Me.LabelFormula)
        Me.TabPage1.Controls.Add(Me.TextBoxFormula)
        Me.TabPage1.Controls.Add(Me.LabelPropertyFilterValue)
        Me.TabPage1.Controls.Add(Me.LabelPropertyFilterComparison)
        Me.TabPage1.Controls.Add(Me.LabelPropertyFilterPropertyName)
        Me.TabPage1.Controls.Add(Me.LabelPropertyFilterPropertySet)
        Me.TabPage1.Controls.Add(Me.LabelPropertyFilterName)
        Me.TabPage1.Controls.Add(Me.ListViewPropertyFilter)
        Me.TabPage1.Controls.Add(Me.ButtonPropertyFilterRemove)
        Me.TabPage1.Controls.Add(Me.ButtonPropertyFilterAdd)
        Me.TabPage1.Controls.Add(Me.TextBoxPropertyFilterValue)
        Me.TabPage1.Controls.Add(Me.ComboBoxPropertyFilterComparison)
        Me.TabPage1.Controls.Add(Me.TextBoxPropertyFilterPropertyName)
        Me.TabPage1.Controls.Add(Me.ComboBoxPropertyFilterPropertySet)
        Me.TabPage1.Controls.Add(Me.ButtonPropertyFilterSave)
        Me.TabPage1.Controls.Add(Me.ComboBoxPropertyFilterName)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(772, 471)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Options"
        '
        'ButtonPropertyFilterDelete
        '
        Me.ButtonPropertyFilterDelete.Location = New System.Drawing.Point(625, 30)
        Me.ButtonPropertyFilterDelete.Name = "ButtonPropertyFilterDelete"
        Me.ButtonPropertyFilterDelete.Size = New System.Drawing.Size(100, 25)
        Me.ButtonPropertyFilterDelete.TabIndex = 36
        Me.ButtonPropertyFilterDelete.Text = "Delete"
        Me.ButtonPropertyFilterDelete.UseVisualStyleBackColor = True
        '
        'ButtonFormula
        '
        Me.ButtonFormula.Location = New System.Drawing.Point(625, 435)
        Me.ButtonFormula.Name = "ButtonFormula"
        Me.ButtonFormula.Size = New System.Drawing.Size(100, 23)
        Me.ButtonFormula.TabIndex = 35
        Me.ButtonFormula.Text = "Edit"
        Me.ButtonFormula.UseVisualStyleBackColor = True
        '
        'LabelFormula
        '
        Me.LabelFormula.AutoSize = True
        Me.LabelFormula.Location = New System.Drawing.Point(25, 410)
        Me.LabelFormula.Name = "LabelFormula"
        Me.LabelFormula.Size = New System.Drawing.Size(59, 17)
        Me.LabelFormula.TabIndex = 34
        Me.LabelFormula.Text = "Formula"
        '
        'TextBoxFormula
        '
        Me.TextBoxFormula.Location = New System.Drawing.Point(25, 435)
        Me.TextBoxFormula.Name = "TextBoxFormula"
        Me.TextBoxFormula.Size = New System.Drawing.Size(575, 22)
        Me.TextBoxFormula.TabIndex = 33
        '
        'LabelPropertyFilterValue
        '
        Me.LabelPropertyFilterValue.AutoSize = True
        Me.LabelPropertyFilterValue.Location = New System.Drawing.Point(525, 75)
        Me.LabelPropertyFilterValue.Name = "LabelPropertyFilterValue"
        Me.LabelPropertyFilterValue.Size = New System.Drawing.Size(44, 17)
        Me.LabelPropertyFilterValue.TabIndex = 32
        Me.LabelPropertyFilterValue.Text = "Value"
        '
        'LabelPropertyFilterComparison
        '
        Me.LabelPropertyFilterComparison.AutoSize = True
        Me.LabelPropertyFilterComparison.Location = New System.Drawing.Point(375, 75)
        Me.LabelPropertyFilterComparison.Name = "LabelPropertyFilterComparison"
        Me.LabelPropertyFilterComparison.Size = New System.Drawing.Size(83, 17)
        Me.LabelPropertyFilterComparison.TabIndex = 31
        Me.LabelPropertyFilterComparison.Text = "Comparison"
        '
        'LabelPropertyFilterPropertyName
        '
        Me.LabelPropertyFilterPropertyName.AutoSize = True
        Me.LabelPropertyFilterPropertyName.Location = New System.Drawing.Point(175, 75)
        Me.LabelPropertyFilterPropertyName.Name = "LabelPropertyFilterPropertyName"
        Me.LabelPropertyFilterPropertyName.Size = New System.Drawing.Size(101, 17)
        Me.LabelPropertyFilterPropertyName.TabIndex = 30
        Me.LabelPropertyFilterPropertyName.Text = "Property name"
        '
        'LabelPropertyFilterPropertySet
        '
        Me.LabelPropertyFilterPropertySet.AutoSize = True
        Me.LabelPropertyFilterPropertySet.Location = New System.Drawing.Point(25, 75)
        Me.LabelPropertyFilterPropertySet.Name = "LabelPropertyFilterPropertySet"
        Me.LabelPropertyFilterPropertySet.Size = New System.Drawing.Size(85, 17)
        Me.LabelPropertyFilterPropertySet.TabIndex = 29
        Me.LabelPropertyFilterPropertySet.Text = "Property set"
        '
        'LabelPropertyFilterName
        '
        Me.LabelPropertyFilterName.AutoSize = True
        Me.LabelPropertyFilterName.Location = New System.Drawing.Point(25, 5)
        Me.LabelPropertyFilterName.Name = "LabelPropertyFilterName"
        Me.LabelPropertyFilterName.Size = New System.Drawing.Size(132, 17)
        Me.LabelPropertyFilterName.TabIndex = 28
        Me.LabelPropertyFilterName.Text = "Property filter name"
        '
        'ListViewPropertyFilter
        '
        Me.ListViewPropertyFilter.HideSelection = False
        Me.ListViewPropertyFilter.Location = New System.Drawing.Point(25, 175)
        Me.ListViewPropertyFilter.Name = "ListViewPropertyFilter"
        Me.ListViewPropertyFilter.Size = New System.Drawing.Size(700, 225)
        Me.ListViewPropertyFilter.TabIndex = 27
        Me.ListViewPropertyFilter.UseCompatibleStateImageBehavior = False
        '
        'ButtonPropertyFilterRemove
        '
        Me.ButtonPropertyFilterRemove.Location = New System.Drawing.Point(625, 140)
        Me.ButtonPropertyFilterRemove.Name = "ButtonPropertyFilterRemove"
        Me.ButtonPropertyFilterRemove.Size = New System.Drawing.Size(100, 23)
        Me.ButtonPropertyFilterRemove.TabIndex = 26
        Me.ButtonPropertyFilterRemove.Text = "Remove"
        Me.ButtonPropertyFilterRemove.UseVisualStyleBackColor = True
        '
        'ButtonPropertyFilterAdd
        '
        Me.ButtonPropertyFilterAdd.Location = New System.Drawing.Point(500, 140)
        Me.ButtonPropertyFilterAdd.Name = "ButtonPropertyFilterAdd"
        Me.ButtonPropertyFilterAdd.Size = New System.Drawing.Size(100, 23)
        Me.ButtonPropertyFilterAdd.TabIndex = 25
        Me.ButtonPropertyFilterAdd.Text = "Add"
        Me.ButtonPropertyFilterAdd.UseVisualStyleBackColor = True
        '
        'TextBoxPropertyFilterValue
        '
        Me.TextBoxPropertyFilterValue.Location = New System.Drawing.Point(525, 100)
        Me.TextBoxPropertyFilterValue.Name = "TextBoxPropertyFilterValue"
        Me.TextBoxPropertyFilterValue.Size = New System.Drawing.Size(200, 22)
        Me.TextBoxPropertyFilterValue.TabIndex = 24
        '
        'ComboBoxPropertyFilterComparison
        '
        Me.ComboBoxPropertyFilterComparison.FormattingEnabled = True
        Me.ComboBoxPropertyFilterComparison.Location = New System.Drawing.Point(375, 100)
        Me.ComboBoxPropertyFilterComparison.Name = "ComboBoxPropertyFilterComparison"
        Me.ComboBoxPropertyFilterComparison.Size = New System.Drawing.Size(125, 24)
        Me.ComboBoxPropertyFilterComparison.TabIndex = 23
        '
        'TextBoxPropertyFilterPropertyName
        '
        Me.TextBoxPropertyFilterPropertyName.Location = New System.Drawing.Point(175, 100)
        Me.TextBoxPropertyFilterPropertyName.Name = "TextBoxPropertyFilterPropertyName"
        Me.TextBoxPropertyFilterPropertyName.Size = New System.Drawing.Size(175, 22)
        Me.TextBoxPropertyFilterPropertyName.TabIndex = 22
        '
        'ComboBoxPropertyFilterPropertySet
        '
        Me.ComboBoxPropertyFilterPropertySet.FormattingEnabled = True
        Me.ComboBoxPropertyFilterPropertySet.Location = New System.Drawing.Point(25, 100)
        Me.ComboBoxPropertyFilterPropertySet.Name = "ComboBoxPropertyFilterPropertySet"
        Me.ComboBoxPropertyFilterPropertySet.Size = New System.Drawing.Size(125, 24)
        Me.ComboBoxPropertyFilterPropertySet.TabIndex = 21
        '
        'ButtonPropertyFilterSave
        '
        Me.ButtonPropertyFilterSave.Location = New System.Drawing.Point(500, 30)
        Me.ButtonPropertyFilterSave.Name = "ButtonPropertyFilterSave"
        Me.ButtonPropertyFilterSave.Size = New System.Drawing.Size(100, 23)
        Me.ButtonPropertyFilterSave.TabIndex = 20
        Me.ButtonPropertyFilterSave.Text = "Save"
        Me.ButtonPropertyFilterSave.UseVisualStyleBackColor = True
        '
        'ComboBoxPropertyFilterName
        '
        Me.ComboBoxPropertyFilterName.FormattingEnabled = True
        Me.ComboBoxPropertyFilterName.Location = New System.Drawing.Point(25, 30)
        Me.ComboBoxPropertyFilterName.Name = "ComboBoxPropertyFilterName"
        Me.ComboBoxPropertyFilterName.Size = New System.Drawing.Size(450, 24)
        Me.ComboBoxPropertyFilterName.TabIndex = 19
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.TextBoxReadme)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(772, 471)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Readme"
        '
        'TextBoxReadme
        '
        Me.TextBoxReadme.Location = New System.Drawing.Point(17, 6)
        Me.TextBoxReadme.Multiline = True
        Me.TextBoxReadme.Name = "TextBoxReadme"
        Me.TextBoxReadme.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxReadme.Size = New System.Drawing.Size(749, 459)
        Me.TextBoxReadme.TabIndex = 0
        '
        'FormPropertyFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(782, 553)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ButtonPropertyFilterCancel)
        Me.Controls.Add(Me.ButtonPropertyFilterOK)
        Me.Name = "FormPropertyFilter"
        Me.Text = "Property Filter"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ButtonPropertyFilterCancel As Button
    Friend WithEvents ButtonPropertyFilterOK As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents ComboBoxPropertyFilterName As ComboBox
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents ListViewPropertyFilter As ListView
    Friend WithEvents ButtonPropertyFilterRemove As Button
    Friend WithEvents ButtonPropertyFilterAdd As Button
    Friend WithEvents TextBoxPropertyFilterValue As TextBox
    Friend WithEvents ComboBoxPropertyFilterComparison As ComboBox
    Friend WithEvents TextBoxPropertyFilterPropertyName As TextBox
    Friend WithEvents ComboBoxPropertyFilterPropertySet As ComboBox
    Friend WithEvents ButtonPropertyFilterSave As Button
    Friend WithEvents LabelPropertyFilterValue As Label
    Friend WithEvents LabelPropertyFilterComparison As Label
    Friend WithEvents LabelPropertyFilterPropertyName As Label
    Friend WithEvents LabelPropertyFilterPropertySet As Label
    Friend WithEvents LabelPropertyFilterName As Label
    Friend WithEvents ButtonFormula As Button
    Friend WithEvents LabelFormula As Label
    Friend WithEvents TextBoxFormula As TextBox
    Friend WithEvents ButtonPropertyFilterDelete As Button
    Friend WithEvents TextBoxReadme As TextBox
End Class
