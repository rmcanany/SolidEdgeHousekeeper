<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCEditVariables
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
        Me.ExTableLayoutPanel1 = New Housekeeper.ExTableLayoutPanel()
        Me.CheckBoxSelect = New System.Windows.Forms.CheckBox()
        Me.TextBoxVariableName = New System.Windows.Forms.TextBox()
        Me.TextBoxFormula = New System.Windows.Forms.TextBox()
        Me.ComboBoxUnitType = New System.Windows.Forms.ComboBox()
        Me.CheckBoxExpose = New System.Windows.Forms.CheckBox()
        Me.TextBoxExposeName = New System.Windows.Forms.TextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.InsertPropertyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExTableLayoutPanel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.ColumnCount = 6
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.CheckBoxSelect, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.TextBoxVariableName, 1, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.TextBoxFormula, 2, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ComboBoxUnitType, 3, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.CheckBoxExpose, 4, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.TextBoxExposeName, 5, 0)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 1
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(700, 30)
        Me.ExTableLayoutPanel1.TabIndex = 0
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'CheckBoxSelect
        '
        Me.CheckBoxSelect.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxSelect.AutoSize = True
        Me.CheckBoxSelect.Location = New System.Drawing.Point(4, 5)
        Me.CheckBoxSelect.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBoxSelect.Name = "CheckBoxSelect"
        Me.CheckBoxSelect.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.CheckBoxSelect.Size = New System.Drawing.Size(22, 19)
        Me.CheckBoxSelect.TabIndex = 0
        Me.CheckBoxSelect.TabStop = False
        Me.CheckBoxSelect.Text = "CheckBox1"
        Me.CheckBoxSelect.UseVisualStyleBackColor = True
        '
        'TextBoxVariableName
        '
        Me.TextBoxVariableName.Location = New System.Drawing.Point(33, 3)
        Me.TextBoxVariableName.Name = "TextBoxVariableName"
        Me.TextBoxVariableName.Size = New System.Drawing.Size(154, 23)
        Me.TextBoxVariableName.TabIndex = 1
        '
        'TextBoxFormula
        '
        Me.TextBoxFormula.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TextBoxFormula.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxFormula.Location = New System.Drawing.Point(193, 3)
        Me.TextBoxFormula.Name = "TextBoxFormula"
        Me.TextBoxFormula.Size = New System.Drawing.Size(154, 23)
        Me.TextBoxFormula.TabIndex = 2
        '
        'ComboBoxUnitType
        '
        Me.ComboBoxUnitType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxUnitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxUnitType.FormattingEnabled = True
        Me.ComboBoxUnitType.Location = New System.Drawing.Point(353, 3)
        Me.ComboBoxUnitType.Name = "ComboBoxUnitType"
        Me.ComboBoxUnitType.Size = New System.Drawing.Size(154, 23)
        Me.ComboBoxUnitType.TabIndex = 3
        '
        'CheckBoxExpose
        '
        Me.CheckBoxExpose.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxExpose.AutoSize = True
        Me.CheckBoxExpose.Location = New System.Drawing.Point(514, 5)
        Me.CheckBoxExpose.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBoxExpose.Name = "CheckBoxExpose"
        Me.CheckBoxExpose.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.CheckBoxExpose.Size = New System.Drawing.Size(22, 19)
        Me.CheckBoxExpose.TabIndex = 4
        Me.CheckBoxExpose.Text = "CheckBox2"
        Me.CheckBoxExpose.UseVisualStyleBackColor = True
        '
        'TextBoxExposeName
        '
        Me.TextBoxExposeName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxExposeName.Location = New System.Drawing.Point(543, 3)
        Me.TextBoxExposeName.Name = "TextBoxExposeName"
        Me.TextBoxExposeName.Size = New System.Drawing.Size(154, 23)
        Me.TextBoxExposeName.TabIndex = 5
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
        Me.InsertPropertyToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.InsertPropertyToolStripMenuItem.Text = "Insert Property"
        '
        'UCEditVariables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ExTableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCEditVariables"
        Me.Size = New System.Drawing.Size(700, 30)
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents CheckBoxSelect As CheckBox
    Friend WithEvents TextBoxVariableName As TextBox
    Friend WithEvents TextBoxFormula As TextBox
    Friend WithEvents ComboBoxUnitType As ComboBox
    Friend WithEvents CheckBoxExpose As CheckBox
    Friend WithEvents TextBoxExposeName As TextBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents InsertPropertyToolStripMenuItem As ToolStripMenuItem
End Class
