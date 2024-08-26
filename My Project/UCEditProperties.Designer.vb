<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCEditProperties
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
        Me.ComboBoxPropertySet = New System.Windows.Forms.ComboBox()
        Me.ComboBoxPropertyName = New System.Windows.Forms.ComboBox()
        Me.ComboBoxFindSearch = New System.Windows.Forms.ComboBox()
        Me.TextBoxFindString = New System.Windows.Forms.TextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.InsertPropertyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ComboBoxReplaceSearch = New System.Windows.Forms.ComboBox()
        Me.TextBoxReplaceString = New System.Windows.Forms.TextBox()
        Me.ExTableLayoutPanel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.ColumnCount = 7
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.CheckBoxSelect, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ComboBoxPropertySet, 1, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ComboBoxPropertyName, 2, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ComboBoxFindSearch, 3, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.TextBoxFindString, 4, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ComboBoxReplaceSearch, 5, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.TextBoxReplaceString, 6, 0)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 1
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(700, 30)
        Me.ExTableLayoutPanel1.TabIndex = 0
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'CheckBoxSelect
        '
        Me.CheckBoxSelect.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CheckBoxSelect.AutoSize = True
        Me.CheckBoxSelect.Location = New System.Drawing.Point(4, 9)
        Me.CheckBoxSelect.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBoxSelect.Name = "CheckBoxSelect"
        Me.CheckBoxSelect.Padding = New System.Windows.Forms.Padding(3, 0, 0, 6)
        Me.CheckBoxSelect.Size = New System.Drawing.Size(18, 20)
        Me.CheckBoxSelect.TabIndex = 0
        Me.CheckBoxSelect.TabStop = False
        Me.CheckBoxSelect.UseVisualStyleBackColor = True
        '
        'ComboBoxPropertySet
        '
        Me.ComboBoxPropertySet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxPropertySet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPropertySet.FormattingEnabled = True
        Me.ComboBoxPropertySet.Items.AddRange(New Object() {"", "System", "Custom"})
        Me.ComboBoxPropertySet.Location = New System.Drawing.Point(33, 3)
        Me.ComboBoxPropertySet.Name = "ComboBoxPropertySet"
        Me.ComboBoxPropertySet.Size = New System.Drawing.Size(94, 24)
        Me.ComboBoxPropertySet.TabIndex = 1
        Me.ComboBoxPropertySet.TabStop = False
        '
        'ComboBoxPropertyName
        '
        Me.ComboBoxPropertyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxPropertyName.FormattingEnabled = True
        Me.ComboBoxPropertyName.Location = New System.Drawing.Point(133, 3)
        Me.ComboBoxPropertyName.Name = "ComboBoxPropertyName"
        Me.ComboBoxPropertyName.Size = New System.Drawing.Size(144, 24)
        Me.ComboBoxPropertyName.TabIndex = 2
        '
        'ComboBoxFindSearch
        '
        Me.ComboBoxFindSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxFindSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxFindSearch.FormattingEnabled = True
        Me.ComboBoxFindSearch.Items.AddRange(New Object() {"", "PT", "WC", "RX", "X"})
        Me.ComboBoxFindSearch.Location = New System.Drawing.Point(283, 3)
        Me.ComboBoxFindSearch.Name = "ComboBoxFindSearch"
        Me.ComboBoxFindSearch.Size = New System.Drawing.Size(44, 24)
        Me.ComboBoxFindSearch.TabIndex = 3
        '
        'TextBoxFindString
        '
        Me.TextBoxFindString.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TextBoxFindString.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxFindString.Location = New System.Drawing.Point(333, 3)
        Me.TextBoxFindString.Name = "TextBoxFindString"
        Me.TextBoxFindString.Size = New System.Drawing.Size(106, 23)
        Me.TextBoxFindString.TabIndex = 4
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
        'ComboBoxReplaceSearch
        '
        Me.ComboBoxReplaceSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxReplaceSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxReplaceSearch.FormattingEnabled = True
        Me.ComboBoxReplaceSearch.Items.AddRange(New Object() {"", "PT", "RX", "EX"})
        Me.ComboBoxReplaceSearch.Location = New System.Drawing.Point(445, 3)
        Me.ComboBoxReplaceSearch.Name = "ComboBoxReplaceSearch"
        Me.ComboBoxReplaceSearch.Size = New System.Drawing.Size(44, 24)
        Me.ComboBoxReplaceSearch.TabIndex = 5
        '
        'TextBoxReplaceString
        '
        Me.TextBoxReplaceString.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TextBoxReplaceString.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxReplaceString.Location = New System.Drawing.Point(495, 3)
        Me.TextBoxReplaceString.Margin = New System.Windows.Forms.Padding(3, 3, 15, 3)
        Me.TextBoxReplaceString.Name = "TextBoxReplaceString"
        Me.TextBoxReplaceString.Size = New System.Drawing.Size(190, 23)
        Me.TextBoxReplaceString.TabIndex = 6
        '
        'UCEditProperties
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ExTableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCEditProperties"
        Me.Size = New System.Drawing.Size(700, 30)
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents CheckBoxSelect As CheckBox
    Friend WithEvents ComboBoxPropertySet As ComboBox
    Friend WithEvents ComboBoxPropertyName As ComboBox
    Friend WithEvents ComboBoxFindSearch As ComboBox
    Friend WithEvents TextBoxFindString As TextBox
    Friend WithEvents ComboBoxReplaceSearch As ComboBox
    Friend WithEvents TextBoxReplaceString As TextBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents InsertPropertyToolStripMenuItem As ToolStripMenuItem
End Class
