<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSheetSelector
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSheetSelector))
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.RadioButtonAnsi = New System.Windows.Forms.RadioButton()
        Me.RadioButtonIso = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAll = New System.Windows.Forms.RadioButton()
        Me.CheckBoxSelectAll = New System.Windows.Forms.CheckBox()
        Me.ExTableLayoutPanel1 = New Housekeeper.ExTableLayoutPanel()
        Me.ExTableLayoutPanel2 = New Housekeeper.ExTableLayoutPanel()
        Me.ExTableLayoutPanel3 = New Housekeeper.ExTableLayoutPanel()
        Me.ComboBoxCustomUnits = New System.Windows.Forms.ComboBox()
        Me.LabelCustomUnits = New System.Windows.Forms.Label()
        Me.ExTableLayoutPanel4 = New Housekeeper.ExTableLayoutPanel()
        Me.LabelX = New System.Windows.Forms.Label()
        Me.LabelY = New System.Windows.Forms.Label()
        Me.LabelMin = New System.Windows.Forms.Label()
        Me.LabelMax = New System.Windows.Forms.Label()
        Me.TextBoxCustomXMin = New System.Windows.Forms.TextBox()
        Me.TextBoxCustomYMin = New System.Windows.Forms.TextBox()
        Me.TextBoxCustomXMax = New System.Windows.Forms.TextBox()
        Me.TextBoxCustomYMax = New System.Windows.Forms.TextBox()
        Me.ExTableLayoutPanel1.SuspendLayout()
        Me.ExTableLayoutPanel2.SuspendLayout()
        Me.ExTableLayoutPanel3.SuspendLayout()
        Me.ExTableLayoutPanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ButtonOK.Location = New System.Drawing.Point(4, 4)
        Me.ButtonOK.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(83, 25)
        Me.ButtonOK.TabIndex = 0
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(95, 3)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(84, 25)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.CheckOnClick = True
        Me.ExTableLayoutPanel1.SetColumnSpan(Me.CheckedListBox1, 2)
        Me.CheckedListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(4, 3)
        Me.CheckedListBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(306, 220)
        Me.CheckedListBox1.TabIndex = 2
        '
        'RadioButtonAnsi
        '
        Me.RadioButtonAnsi.AutoSize = True
        Me.RadioButtonAnsi.Location = New System.Drawing.Point(4, 229)
        Me.RadioButtonAnsi.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonAnsi.Name = "RadioButtonAnsi"
        Me.RadioButtonAnsi.Size = New System.Drawing.Size(109, 19)
        Me.RadioButtonAnsi.TabIndex = 3
        Me.RadioButtonAnsi.TabStop = True
        Me.RadioButtonAnsi.Text = "Show ANSI only"
        Me.RadioButtonAnsi.UseVisualStyleBackColor = True
        '
        'RadioButtonIso
        '
        Me.RadioButtonIso.AutoSize = True
        Me.RadioButtonIso.Location = New System.Drawing.Point(4, 259)
        Me.RadioButtonIso.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonIso.Name = "RadioButtonIso"
        Me.RadioButtonIso.Size = New System.Drawing.Size(101, 19)
        Me.RadioButtonIso.TabIndex = 4
        Me.RadioButtonIso.TabStop = True
        Me.RadioButtonIso.Text = "Show ISO only"
        Me.RadioButtonIso.UseVisualStyleBackColor = True
        '
        'RadioButtonAll
        '
        Me.RadioButtonAll.AutoSize = True
        Me.RadioButtonAll.Location = New System.Drawing.Point(4, 289)
        Me.RadioButtonAll.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioButtonAll.Name = "RadioButtonAll"
        Me.RadioButtonAll.Size = New System.Drawing.Size(69, 19)
        Me.RadioButtonAll.TabIndex = 5
        Me.RadioButtonAll.TabStop = True
        Me.RadioButtonAll.Text = "Show all"
        Me.RadioButtonAll.UseVisualStyleBackColor = True
        '
        'CheckBoxSelectAll
        '
        Me.CheckBoxSelectAll.AutoSize = True
        Me.CheckBoxSelectAll.Location = New System.Drawing.Point(4, 319)
        Me.CheckBoxSelectAll.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CheckBoxSelectAll.Name = "CheckBoxSelectAll"
        Me.CheckBoxSelectAll.Size = New System.Drawing.Size(104, 19)
        Me.CheckBoxSelectAll.TabIndex = 6
        Me.CheckBoxSelectAll.Text = "Select all/none"
        Me.CheckBoxSelectAll.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.ColumnCount = 2
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.CheckedListBox1, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.CheckBoxSelectAll, 0, 4)
        Me.ExTableLayoutPanel1.Controls.Add(Me.RadioButtonAnsi, 0, 1)
        Me.ExTableLayoutPanel1.Controls.Add(Me.RadioButtonAll, 0, 3)
        Me.ExTableLayoutPanel1.Controls.Add(Me.RadioButtonIso, 0, 2)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ExTableLayoutPanel2, 1, 10)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ExTableLayoutPanel3, 1, 1)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 11
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(314, 386)
        Me.ExTableLayoutPanel1.TabIndex = 7
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'ExTableLayoutPanel2
        '
        Me.ExTableLayoutPanel2.ColumnCount = 2
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel2.Controls.Add(Me.ButtonCancel, 1, 0)
        Me.ExTableLayoutPanel2.Controls.Add(Me.ButtonOK, 0, 0)
        Me.ExTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel2.Location = New System.Drawing.Point(128, 349)
        Me.ExTableLayoutPanel2.Name = "ExTableLayoutPanel2"
        Me.ExTableLayoutPanel2.RowCount = 1
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.Size = New System.Drawing.Size(183, 34)
        Me.ExTableLayoutPanel2.TabIndex = 7
        Me.ExTableLayoutPanel2.Task = Nothing
        '
        'ExTableLayoutPanel3
        '
        Me.ExTableLayoutPanel3.ColumnCount = 2
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel3.Controls.Add(Me.ComboBoxCustomUnits, 0, 0)
        Me.ExTableLayoutPanel3.Controls.Add(Me.LabelCustomUnits, 1, 0)
        Me.ExTableLayoutPanel3.Controls.Add(Me.ExTableLayoutPanel4, 0, 1)
        Me.ExTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel3.Location = New System.Drawing.Point(128, 229)
        Me.ExTableLayoutPanel3.Name = "ExTableLayoutPanel3"
        Me.ExTableLayoutPanel3.RowCount = 2
        Me.ExTableLayoutPanel1.SetRowSpan(Me.ExTableLayoutPanel3, 4)
        Me.ExTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel3.Size = New System.Drawing.Size(183, 114)
        Me.ExTableLayoutPanel3.TabIndex = 8
        Me.ExTableLayoutPanel3.Task = Nothing
        Me.ExTableLayoutPanel3.Visible = False
        '
        'ComboBoxCustomUnits
        '
        Me.ComboBoxCustomUnits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxCustomUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxCustomUnits.FormattingEnabled = True
        Me.ComboBoxCustomUnits.Items.AddRange(New Object() {"in", "mm"})
        Me.ComboBoxCustomUnits.Location = New System.Drawing.Point(3, 3)
        Me.ComboBoxCustomUnits.Name = "ComboBoxCustomUnits"
        Me.ComboBoxCustomUnits.Size = New System.Drawing.Size(85, 23)
        Me.ComboBoxCustomUnits.TabIndex = 0
        '
        'LabelCustomUnits
        '
        Me.LabelCustomUnits.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelCustomUnits.AutoSize = True
        Me.LabelCustomUnits.Location = New System.Drawing.Point(94, 7)
        Me.LabelCustomUnits.Name = "LabelCustomUnits"
        Me.LabelCustomUnits.Size = New System.Drawing.Size(78, 15)
        Me.LabelCustomUnits.TabIndex = 1
        Me.LabelCustomUnits.Text = "Custom units"
        '
        'ExTableLayoutPanel4
        '
        Me.ExTableLayoutPanel4.ColumnCount = 3
        Me.ExTableLayoutPanel3.SetColumnSpan(Me.ExTableLayoutPanel4, 2)
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelX, 0, 1)
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelY, 0, 2)
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelMin, 1, 0)
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelMax, 2, 0)
        Me.ExTableLayoutPanel4.Controls.Add(Me.TextBoxCustomXMin, 1, 1)
        Me.ExTableLayoutPanel4.Controls.Add(Me.TextBoxCustomYMin, 1, 2)
        Me.ExTableLayoutPanel4.Controls.Add(Me.TextBoxCustomXMax, 2, 1)
        Me.ExTableLayoutPanel4.Controls.Add(Me.TextBoxCustomYMax, 2, 2)
        Me.ExTableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel4.Location = New System.Drawing.Point(3, 33)
        Me.ExTableLayoutPanel4.Name = "ExTableLayoutPanel4"
        Me.ExTableLayoutPanel4.RowCount = 3
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.ExTableLayoutPanel4.Size = New System.Drawing.Size(177, 78)
        Me.ExTableLayoutPanel4.TabIndex = 2
        Me.ExTableLayoutPanel4.Task = Nothing
        '
        'LabelX
        '
        Me.LabelX.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LabelX.AutoSize = True
        Me.LabelX.Location = New System.Drawing.Point(18, 31)
        Me.LabelX.Name = "LabelX"
        Me.LabelX.Size = New System.Drawing.Size(14, 15)
        Me.LabelX.TabIndex = 0
        Me.LabelX.Text = "X"
        '
        'LabelY
        '
        Me.LabelY.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LabelY.AutoSize = True
        Me.LabelY.Location = New System.Drawing.Point(18, 57)
        Me.LabelY.Name = "LabelY"
        Me.LabelY.Size = New System.Drawing.Size(14, 15)
        Me.LabelY.TabIndex = 1
        Me.LabelY.Text = "Y"
        '
        'LabelMin
        '
        Me.LabelMin.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LabelMin.AutoSize = True
        Me.LabelMin.Location = New System.Drawing.Point(56, 5)
        Me.LabelMin.Name = "LabelMin"
        Me.LabelMin.Size = New System.Drawing.Size(28, 15)
        Me.LabelMin.TabIndex = 2
        Me.LabelMin.Text = "Min"
        '
        'LabelMax
        '
        Me.LabelMax.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LabelMax.AutoSize = True
        Me.LabelMax.Location = New System.Drawing.Point(127, 5)
        Me.LabelMax.Name = "LabelMax"
        Me.LabelMax.Size = New System.Drawing.Size(29, 15)
        Me.LabelMax.TabIndex = 3
        Me.LabelMax.Text = "Max"
        '
        'TextBoxCustomXMin
        '
        Me.TextBoxCustomXMin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxCustomXMin.Location = New System.Drawing.Point(38, 29)
        Me.TextBoxCustomXMin.Name = "TextBoxCustomXMin"
        Me.TextBoxCustomXMin.Size = New System.Drawing.Size(65, 23)
        Me.TextBoxCustomXMin.TabIndex = 4
        Me.TextBoxCustomXMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxCustomYMin
        '
        Me.TextBoxCustomYMin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxCustomYMin.Location = New System.Drawing.Point(38, 55)
        Me.TextBoxCustomYMin.Name = "TextBoxCustomYMin"
        Me.TextBoxCustomYMin.Size = New System.Drawing.Size(65, 23)
        Me.TextBoxCustomYMin.TabIndex = 5
        Me.TextBoxCustomYMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxCustomXMax
        '
        Me.TextBoxCustomXMax.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxCustomXMax.Location = New System.Drawing.Point(109, 29)
        Me.TextBoxCustomXMax.Name = "TextBoxCustomXMax"
        Me.TextBoxCustomXMax.Size = New System.Drawing.Size(65, 23)
        Me.TextBoxCustomXMax.TabIndex = 6
        Me.TextBoxCustomXMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxCustomYMax
        '
        Me.TextBoxCustomYMax.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxCustomYMax.Location = New System.Drawing.Point(109, 55)
        Me.TextBoxCustomYMax.Name = "TextBoxCustomYMax"
        Me.TextBoxCustomYMax.Size = New System.Drawing.Size(65, 23)
        Me.TextBoxCustomYMax.TabIndex = 7
        Me.TextBoxCustomYMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'FormSheetSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(314, 386)
        Me.Controls.Add(Me.ExTableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(330, 650)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(330, 425)
        Me.Name = "FormSheetSelector"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Sheet Selector"
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.PerformLayout()
        Me.ExTableLayoutPanel2.ResumeLayout(False)
        Me.ExTableLayoutPanel3.ResumeLayout(False)
        Me.ExTableLayoutPanel3.PerformLayout()
        Me.ExTableLayoutPanel4.ResumeLayout(False)
        Me.ExTableLayoutPanel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents RadioButtonAnsi As RadioButton
    Friend WithEvents RadioButtonIso As RadioButton
    Friend WithEvents RadioButtonAll As RadioButton
    Friend WithEvents CheckBoxSelectAll As CheckBox
    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel2 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel3 As ExTableLayoutPanel
    Friend WithEvents ComboBoxCustomUnits As ComboBox
    Friend WithEvents LabelCustomUnits As Label
    Friend WithEvents ExTableLayoutPanel4 As ExTableLayoutPanel
    Friend WithEvents LabelX As Label
    Friend WithEvents LabelY As Label
    Friend WithEvents LabelMin As Label
    Friend WithEvents LabelMax As Label
    Friend WithEvents TextBoxCustomXMin As TextBox
    Friend WithEvents TextBoxCustomYMin As TextBox
    Friend WithEvents TextBoxCustomXMax As TextBox
    Friend WithEvents TextBoxCustomYMax As TextBox
End Class
