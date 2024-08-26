<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCPropertyFilter
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.InsertPropertyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExTableLayoutPanel1 = New Housekeeper.ExTableLayoutPanel()
        Me.CheckBoxSelect = New System.Windows.Forms.CheckBox()
        Me.LabelVariable = New System.Windows.Forms.Label()
        Me.ComboBoxPropertySet = New System.Windows.Forms.ComboBox()
        Me.ComboBoxComparison = New System.Windows.Forms.ComboBox()
        Me.TextBoxValue = New System.Windows.Forms.TextBox()
        Me.ComboBoxPropertyName = New System.Windows.Forms.ComboBox()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ExTableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InsertPropertyToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(152, 26)
        '
        'InsertPropertyToolStripMenuItem
        '
        Me.InsertPropertyToolStripMenuItem.Name = "InsertPropertyToolStripMenuItem"
        Me.InsertPropertyToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.InsertPropertyToolStripMenuItem.Text = "Insert property"
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.ColumnCount = 6
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.CheckBoxSelect, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.LabelVariable, 1, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ComboBoxPropertySet, 2, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ComboBoxComparison, 4, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.TextBoxValue, 5, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ComboBoxPropertyName, 3, 0)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 1
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(700, 30)
        Me.ExTableLayoutPanel1.TabIndex = 0
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'CheckBoxSelect
        '
        Me.CheckBoxSelect.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxSelect.AutoSize = True
        Me.CheckBoxSelect.Location = New System.Drawing.Point(3, 5)
        Me.CheckBoxSelect.Name = "CheckBoxSelect"
        Me.CheckBoxSelect.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.CheckBoxSelect.Size = New System.Drawing.Size(24, 20)
        Me.CheckBoxSelect.TabIndex = 0
        Me.CheckBoxSelect.TabStop = False
        Me.CheckBoxSelect.Text = "CheckBox1"
        Me.CheckBoxSelect.UseVisualStyleBackColor = True
        '
        'LabelVariable
        '
        Me.LabelVariable.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LabelVariable.AutoSize = True
        Me.LabelVariable.Location = New System.Drawing.Point(45, 7)
        Me.LabelVariable.Name = "LabelVariable"
        Me.LabelVariable.Size = New System.Drawing.Size(0, 16)
        Me.LabelVariable.TabIndex = 1
        '
        'ComboBoxPropertySet
        '
        Me.ComboBoxPropertySet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxPropertySet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPropertySet.FormattingEnabled = True
        Me.ComboBoxPropertySet.Items.AddRange(New Object() {"", "Custom", "System"})
        Me.ComboBoxPropertySet.Location = New System.Drawing.Point(63, 3)
        Me.ComboBoxPropertySet.Name = "ComboBoxPropertySet"
        Me.ComboBoxPropertySet.Size = New System.Drawing.Size(94, 24)
        Me.ComboBoxPropertySet.TabIndex = 2
        Me.ComboBoxPropertySet.TabStop = False
        '
        'ComboBoxComparison
        '
        Me.ComboBoxComparison.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxComparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxComparison.FormattingEnabled = True
        Me.ComboBoxComparison.Items.AddRange(New Object() {"", "contains", "is_exactly", "is_not", "wildcard_match", "regex_match", ">", "<"})
        Me.ComboBoxComparison.Location = New System.Drawing.Point(313, 3)
        Me.ComboBoxComparison.Name = "ComboBoxComparison"
        Me.ComboBoxComparison.Size = New System.Drawing.Size(94, 24)
        Me.ComboBoxComparison.TabIndex = 2
        '
        'TextBoxValue
        '
        Me.TextBoxValue.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TextBoxValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxValue.Location = New System.Drawing.Point(413, 3)
        Me.TextBoxValue.Name = "TextBoxValue"
        Me.TextBoxValue.Size = New System.Drawing.Size(284, 23)
        Me.TextBoxValue.TabIndex = 3
        '
        'ComboBoxPropertyName
        '
        Me.ComboBoxPropertyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxPropertyName.FormattingEnabled = True
        Me.ComboBoxPropertyName.Location = New System.Drawing.Point(163, 3)
        Me.ComboBoxPropertyName.Name = "ComboBoxPropertyName"
        Me.ComboBoxPropertyName.Size = New System.Drawing.Size(144, 24)
        Me.ComboBoxPropertyName.TabIndex = 1
        '
        'UCPropertyFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ExTableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCPropertyFilter"
        Me.Size = New System.Drawing.Size(700, 30)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents CheckBoxSelect As CheckBox
    Friend WithEvents LabelVariable As Label
    Friend WithEvents ComboBoxPropertySet As ComboBox
    Friend WithEvents ComboBoxComparison As ComboBox
    Friend WithEvents TextBoxValue As TextBox
    Friend WithEvents ComboBoxPropertyName As ComboBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents InsertPropertyToolStripMenuItem As ToolStripMenuItem
End Class
