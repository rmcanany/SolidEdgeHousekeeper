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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPropertyFilter))
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
        Me.ButtonPropertyFilterCancel.Location = New System.Drawing.Point(488, 493)
        Me.ButtonPropertyFilterCancel.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonPropertyFilterCancel.Name = "ButtonPropertyFilterCancel"
        Me.ButtonPropertyFilterCancel.Size = New System.Drawing.Size(75, 22)
        Me.ButtonPropertyFilterCancel.TabIndex = 22
        Me.ButtonPropertyFilterCancel.Text = "Cancel"
        Me.ButtonPropertyFilterCancel.UseVisualStyleBackColor = True
        '
        'ButtonPropertyFilterOK
        '
        Me.ButtonPropertyFilterOK.Location = New System.Drawing.Point(394, 493)
        Me.ButtonPropertyFilterOK.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonPropertyFilterOK.Name = "ButtonPropertyFilterOK"
        Me.ButtonPropertyFilterOK.Size = New System.Drawing.Size(75, 22)
        Me.ButtonPropertyFilterOK.TabIndex = 21
        Me.ButtonPropertyFilterOK.Text = "OK"
        Me.ButtonPropertyFilterOK.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(585, 468)
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
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage1.Size = New System.Drawing.Size(577, 440)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Options"
        '
        'ButtonPropertyFilterDelete
        '
        Me.ButtonPropertyFilterDelete.Location = New System.Drawing.Point(469, 28)
        Me.ButtonPropertyFilterDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonPropertyFilterDelete.Name = "ButtonPropertyFilterDelete"
        Me.ButtonPropertyFilterDelete.Size = New System.Drawing.Size(75, 23)
        Me.ButtonPropertyFilterDelete.TabIndex = 36
        Me.ButtonPropertyFilterDelete.Text = "Delete"
        Me.ButtonPropertyFilterDelete.UseVisualStyleBackColor = True
        '
        'ButtonFormula
        '
        Me.ButtonFormula.Location = New System.Drawing.Point(469, 407)
        Me.ButtonFormula.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonFormula.Name = "ButtonFormula"
        Me.ButtonFormula.Size = New System.Drawing.Size(75, 22)
        Me.ButtonFormula.TabIndex = 35
        Me.ButtonFormula.Text = "Edit"
        Me.ButtonFormula.UseVisualStyleBackColor = True
        '
        'LabelFormula
        '
        Me.LabelFormula.AutoSize = True
        Me.LabelFormula.Location = New System.Drawing.Point(19, 384)
        Me.LabelFormula.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFormula.Name = "LabelFormula"
        Me.LabelFormula.Size = New System.Drawing.Size(45, 15)
        Me.LabelFormula.TabIndex = 34
        Me.LabelFormula.Text = "Formula"
        '
        'TextBoxFormula
        '
        Me.TextBoxFormula.Location = New System.Drawing.Point(19, 407)
        Me.TextBoxFormula.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxFormula.Name = "TextBoxFormula"
        Me.TextBoxFormula.Size = New System.Drawing.Size(432, 22)
        Me.TextBoxFormula.TabIndex = 33
        '
        'LabelPropertyFilterValue
        '
        Me.LabelPropertyFilterValue.AutoSize = True
        Me.LabelPropertyFilterValue.Location = New System.Drawing.Point(394, 70)
        Me.LabelPropertyFilterValue.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPropertyFilterValue.Name = "LabelPropertyFilterValue"
        Me.LabelPropertyFilterValue.Size = New System.Drawing.Size(33, 15)
        Me.LabelPropertyFilterValue.TabIndex = 32
        Me.LabelPropertyFilterValue.Text = "Value"
        '
        'LabelPropertyFilterComparison
        '
        Me.LabelPropertyFilterComparison.AutoSize = True
        Me.LabelPropertyFilterComparison.Location = New System.Drawing.Point(281, 70)
        Me.LabelPropertyFilterComparison.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPropertyFilterComparison.Name = "LabelPropertyFilterComparison"
        Me.LabelPropertyFilterComparison.Size = New System.Drawing.Size(65, 15)
        Me.LabelPropertyFilterComparison.TabIndex = 31
        Me.LabelPropertyFilterComparison.Text = "Comparison"
        '
        'LabelPropertyFilterPropertyName
        '
        Me.LabelPropertyFilterPropertyName.AutoSize = True
        Me.LabelPropertyFilterPropertyName.Location = New System.Drawing.Point(131, 70)
        Me.LabelPropertyFilterPropertyName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPropertyFilterPropertyName.Name = "LabelPropertyFilterPropertyName"
        Me.LabelPropertyFilterPropertyName.Size = New System.Drawing.Size(78, 15)
        Me.LabelPropertyFilterPropertyName.TabIndex = 30
        Me.LabelPropertyFilterPropertyName.Text = "Property name"
        '
        'LabelPropertyFilterPropertySet
        '
        Me.LabelPropertyFilterPropertySet.AutoSize = True
        Me.LabelPropertyFilterPropertySet.Location = New System.Drawing.Point(19, 70)
        Me.LabelPropertyFilterPropertySet.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPropertyFilterPropertySet.Name = "LabelPropertyFilterPropertySet"
        Me.LabelPropertyFilterPropertySet.Size = New System.Drawing.Size(67, 15)
        Me.LabelPropertyFilterPropertySet.TabIndex = 29
        Me.LabelPropertyFilterPropertySet.Text = "Property set"
        '
        'LabelPropertyFilterName
        '
        Me.LabelPropertyFilterName.AutoSize = True
        Me.LabelPropertyFilterName.Location = New System.Drawing.Point(19, 5)
        Me.LabelPropertyFilterName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPropertyFilterName.Name = "LabelPropertyFilterName"
        Me.LabelPropertyFilterName.Size = New System.Drawing.Size(104, 15)
        Me.LabelPropertyFilterName.TabIndex = 28
        Me.LabelPropertyFilterName.Text = "Property filter name"
        '
        'ListViewPropertyFilter
        '
        Me.ListViewPropertyFilter.HideSelection = False
        Me.ListViewPropertyFilter.Location = New System.Drawing.Point(19, 164)
        Me.ListViewPropertyFilter.Margin = New System.Windows.Forms.Padding(2)
        Me.ListViewPropertyFilter.Name = "ListViewPropertyFilter"
        Me.ListViewPropertyFilter.Size = New System.Drawing.Size(526, 212)
        Me.ListViewPropertyFilter.TabIndex = 27
        Me.ListViewPropertyFilter.UseCompatibleStateImageBehavior = False
        '
        'ButtonPropertyFilterRemove
        '
        Me.ButtonPropertyFilterRemove.Location = New System.Drawing.Point(469, 132)
        Me.ButtonPropertyFilterRemove.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonPropertyFilterRemove.Name = "ButtonPropertyFilterRemove"
        Me.ButtonPropertyFilterRemove.Size = New System.Drawing.Size(75, 22)
        Me.ButtonPropertyFilterRemove.TabIndex = 26
        Me.ButtonPropertyFilterRemove.Text = "Remove"
        Me.ButtonPropertyFilterRemove.UseVisualStyleBackColor = True
        '
        'ButtonPropertyFilterAdd
        '
        Me.ButtonPropertyFilterAdd.Location = New System.Drawing.Point(375, 132)
        Me.ButtonPropertyFilterAdd.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonPropertyFilterAdd.Name = "ButtonPropertyFilterAdd"
        Me.ButtonPropertyFilterAdd.Size = New System.Drawing.Size(75, 22)
        Me.ButtonPropertyFilterAdd.TabIndex = 25
        Me.ButtonPropertyFilterAdd.Text = "Add"
        Me.ButtonPropertyFilterAdd.UseVisualStyleBackColor = True
        '
        'TextBoxPropertyFilterValue
        '
        Me.TextBoxPropertyFilterValue.Location = New System.Drawing.Point(394, 93)
        Me.TextBoxPropertyFilterValue.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPropertyFilterValue.Name = "TextBoxPropertyFilterValue"
        Me.TextBoxPropertyFilterValue.Size = New System.Drawing.Size(151, 22)
        Me.TextBoxPropertyFilterValue.TabIndex = 24
        '
        'ComboBoxPropertyFilterComparison
        '
        Me.ComboBoxPropertyFilterComparison.FormattingEnabled = True
        Me.ComboBoxPropertyFilterComparison.Location = New System.Drawing.Point(281, 93)
        Me.ComboBoxPropertyFilterComparison.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxPropertyFilterComparison.Name = "ComboBoxPropertyFilterComparison"
        Me.ComboBoxPropertyFilterComparison.Size = New System.Drawing.Size(95, 23)
        Me.ComboBoxPropertyFilterComparison.TabIndex = 23
        '
        'TextBoxPropertyFilterPropertyName
        '
        Me.TextBoxPropertyFilterPropertyName.Location = New System.Drawing.Point(131, 93)
        Me.TextBoxPropertyFilterPropertyName.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPropertyFilterPropertyName.Name = "TextBoxPropertyFilterPropertyName"
        Me.TextBoxPropertyFilterPropertyName.Size = New System.Drawing.Size(132, 22)
        Me.TextBoxPropertyFilterPropertyName.TabIndex = 22
        '
        'ComboBoxPropertyFilterPropertySet
        '
        Me.ComboBoxPropertyFilterPropertySet.FormattingEnabled = True
        Me.ComboBoxPropertyFilterPropertySet.Location = New System.Drawing.Point(19, 93)
        Me.ComboBoxPropertyFilterPropertySet.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxPropertyFilterPropertySet.Name = "ComboBoxPropertyFilterPropertySet"
        Me.ComboBoxPropertyFilterPropertySet.Size = New System.Drawing.Size(95, 23)
        Me.ComboBoxPropertyFilterPropertySet.TabIndex = 21
        '
        'ButtonPropertyFilterSave
        '
        Me.ButtonPropertyFilterSave.Location = New System.Drawing.Point(375, 28)
        Me.ButtonPropertyFilterSave.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonPropertyFilterSave.Name = "ButtonPropertyFilterSave"
        Me.ButtonPropertyFilterSave.Size = New System.Drawing.Size(75, 22)
        Me.ButtonPropertyFilterSave.TabIndex = 20
        Me.ButtonPropertyFilterSave.Text = "Save"
        Me.ButtonPropertyFilterSave.UseVisualStyleBackColor = True
        '
        'ComboBoxPropertyFilterName
        '
        Me.ComboBoxPropertyFilterName.FormattingEnabled = True
        Me.ComboBoxPropertyFilterName.Location = New System.Drawing.Point(19, 28)
        Me.ComboBoxPropertyFilterName.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxPropertyFilterName.Name = "ComboBoxPropertyFilterName"
        Me.ComboBoxPropertyFilterName.Size = New System.Drawing.Size(338, 23)
        Me.ComboBoxPropertyFilterName.Sorted = True
        Me.ComboBoxPropertyFilterName.TabIndex = 19
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.TextBoxReadme)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage2.Size = New System.Drawing.Size(577, 440)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Readme"
        '
        'TextBoxReadme
        '
        Me.TextBoxReadme.Location = New System.Drawing.Point(13, 6)
        Me.TextBoxReadme.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxReadme.Multiline = True
        Me.TextBoxReadme.Name = "TextBoxReadme"
        Me.TextBoxReadme.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxReadme.Size = New System.Drawing.Size(563, 431)
        Me.TextBoxReadme.TabIndex = 0
        '
        'FormPropertyFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 518)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ButtonPropertyFilterCancel)
        Me.Controls.Add(Me.ButtonPropertyFilterOK)
        Me.Font = New System.Drawing.Font("Segoe UI Variable Display", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "FormPropertyFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
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
