<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormExpressionEditor
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormExpressionEditor))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.EditorLabel = New System.Windows.Forms.ToolStripLabel()
        Me.ComboBoxLanguage = New System.Windows.Forms.ToolStripComboBox()
        Me.BT_Test = New System.Windows.Forms.ToolStripButton()
        Me.BT_TestOnCurrentFile = New System.Windows.Forms.ToolStripButton()
        Me.BT_Clear = New System.Windows.Forms.ToolStripButton()
        Me.BT_InsertProp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SavedExpressionsLabel = New System.Windows.Forms.ToolStripLabel()
        Me.DD_SavedExpressions = New System.Windows.Forms.ToolStripDropDownButton()
        Me.BT_Save = New System.Windows.Forms.ToolStripButton()
        Me.BT_SaveAs = New System.Windows.Forms.ToolStripButton()
        Me.BT_Delete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_Help = New System.Windows.Forms.ToolStripButton()
        Me.TextEditorFormula = New FastColoredTextBoxNS.FastColoredTextBox()
        Me.TextEditorResults = New FastColoredTextBoxNS.FastColoredTextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ExTableLayoutPanel1 = New Housekeeper.ExTableLayoutPanel()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.TextEditorFormula, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditorResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ExTableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.White
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditorLabel, Me.ComboBoxLanguage, Me.BT_Test, Me.BT_TestOnCurrentFile, Me.BT_Clear, Me.BT_InsertProp, Me.ToolStripSeparator1, Me.SavedExpressionsLabel, Me.DD_SavedExpressions, Me.BT_Save, Me.BT_SaveAs, Me.BT_Delete, Me.ToolStripSeparator2, Me.BT_Help})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(781, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'EditorLabel
        '
        Me.EditorLabel.BackColor = System.Drawing.Color.Gainsboro
        Me.EditorLabel.Name = "EditorLabel"
        Me.EditorLabel.Size = New System.Drawing.Size(38, 22)
        Me.EditorLabel.Text = "Editor"
        '
        'ComboBoxLanguage
        '
        Me.ComboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxLanguage.Items.AddRange(New Object() {"NCalc", "VB"})
        Me.ComboBoxLanguage.Name = "ComboBoxLanguage"
        Me.ComboBoxLanguage.Size = New System.Drawing.Size(75, 25)
        Me.ComboBoxLanguage.ToolTipText = "Expression language"
        '
        'BT_Test
        '
        Me.BT_Test.Image = Global.Housekeeper.My.Resources.Resources.Play
        Me.BT_Test.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Test.Name = "BT_Test"
        Me.BT_Test.Size = New System.Drawing.Size(48, 22)
        Me.BT_Test.Text = "Test"
        '
        'BT_TestOnCurrentFile
        '
        Me.BT_TestOnCurrentFile.Image = Global.Housekeeper.My.Resources.Resources.Play
        Me.BT_TestOnCurrentFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_TestOnCurrentFile.Name = "BT_TestOnCurrentFile"
        Me.BT_TestOnCurrentFile.Size = New System.Drawing.Size(94, 22)
        Me.BT_TestOnCurrentFile.Text = "Test on Edge"
        Me.BT_TestOnCurrentFile.ToolTipText = "Test on current Solid Edge file"
        '
        'BT_Clear
        '
        Me.BT_Clear.Image = Global.Housekeeper.My.Resources.Resources.unchecked_disabled
        Me.BT_Clear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Clear.Name = "BT_Clear"
        Me.BT_Clear.Size = New System.Drawing.Size(54, 22)
        Me.BT_Clear.Text = "Clear"
        '
        'BT_InsertProp
        '
        Me.BT_InsertProp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.BT_InsertProp.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BT_InsertProp.Image = CType(resources.GetObject("BT_InsertProp.Image"), System.Drawing.Image)
        Me.BT_InsertProp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_InsertProp.Name = "BT_InsertProp"
        Me.BT_InsertProp.Size = New System.Drawing.Size(41, 22)
        Me.BT_InsertProp.Text = """%{}"""
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'SavedExpressionsLabel
        '
        Me.SavedExpressionsLabel.BackColor = System.Drawing.Color.Gainsboro
        Me.SavedExpressionsLabel.Name = "SavedExpressionsLabel"
        Me.SavedExpressionsLabel.Size = New System.Drawing.Size(101, 22)
        Me.SavedExpressionsLabel.Text = "Saved Expressions"
        '
        'DD_SavedExpressions
        '
        Me.DD_SavedExpressions.Image = Global.Housekeeper.My.Resources.Resources.list
        Me.DD_SavedExpressions.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.DD_SavedExpressions.Name = "DD_SavedExpressions"
        Me.DD_SavedExpressions.Size = New System.Drawing.Size(67, 22)
        Me.DD_SavedExpressions.Text = "Select"
        '
        'BT_Save
        '
        Me.BT_Save.Image = Global.Housekeeper.My.Resources.Resources.Save
        Me.BT_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Save.Name = "BT_Save"
        Me.BT_Save.Size = New System.Drawing.Size(51, 22)
        Me.BT_Save.Text = "Save"
        '
        'BT_SaveAs
        '
        Me.BT_SaveAs.Image = Global.Housekeeper.My.Resources.Resources.Save
        Me.BT_SaveAs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_SaveAs.Name = "BT_SaveAs"
        Me.BT_SaveAs.Size = New System.Drawing.Size(67, 22)
        Me.BT_SaveAs.Text = "Save As"
        '
        'BT_Delete
        '
        Me.BT_Delete.Image = Global.Housekeeper.My.Resources.Resources.delete
        Me.BT_Delete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Delete.Name = "BT_Delete"
        Me.BT_Delete.Size = New System.Drawing.Size(60, 22)
        Me.BT_Delete.Text = "Delete"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'BT_Help
        '
        Me.BT_Help.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.BT_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_Help.Image = Global.Housekeeper.My.Resources.Resources.Help
        Me.BT_Help.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Help.Name = "BT_Help"
        Me.BT_Help.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BT_Help.Size = New System.Drawing.Size(23, 22)
        Me.BT_Help.Text = "Help"
        '
        'TextEditorFormula
        '
        Me.TextEditorFormula.AutoCompleteBracketsList = New Char() {Global.Microsoft.VisualBasic.ChrW(40), Global.Microsoft.VisualBasic.ChrW(41), Global.Microsoft.VisualBasic.ChrW(123), Global.Microsoft.VisualBasic.ChrW(125), Global.Microsoft.VisualBasic.ChrW(91), Global.Microsoft.VisualBasic.ChrW(93), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(39), Global.Microsoft.VisualBasic.ChrW(39)}
        Me.TextEditorFormula.AutoScrollMinSize = New System.Drawing.Size(27, 14)
        Me.TextEditorFormula.BackBrush = Nothing
        Me.TextEditorFormula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextEditorFormula.CharHeight = 14
        Me.TextEditorFormula.CharWidth = 8
        Me.TextEditorFormula.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextEditorFormula.DisabledColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.TextEditorFormula.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextEditorFormula.IsReplaceMode = False
        Me.TextEditorFormula.Location = New System.Drawing.Point(0, 0)
        Me.TextEditorFormula.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextEditorFormula.Name = "TextEditorFormula"
        Me.TextEditorFormula.Paddings = New System.Windows.Forms.Padding(0)
        Me.TextEditorFormula.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEditorFormula.ServiceColors = CType(resources.GetObject("TextEditorFormula.ServiceColors"), FastColoredTextBoxNS.ServiceColors)
        Me.TextEditorFormula.Size = New System.Drawing.Size(775, 245)
        Me.TextEditorFormula.TabIndex = 3
        Me.TextEditorFormula.Zoom = 100
        '
        'TextEditorResults
        '
        Me.TextEditorResults.AutoCompleteBracketsList = New Char() {Global.Microsoft.VisualBasic.ChrW(40), Global.Microsoft.VisualBasic.ChrW(41), Global.Microsoft.VisualBasic.ChrW(123), Global.Microsoft.VisualBasic.ChrW(125), Global.Microsoft.VisualBasic.ChrW(91), Global.Microsoft.VisualBasic.ChrW(93), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(39), Global.Microsoft.VisualBasic.ChrW(39)}
        Me.TextEditorResults.AutoScrollMinSize = New System.Drawing.Size(27, 14)
        Me.TextEditorResults.BackBrush = Nothing
        Me.TextEditorResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextEditorResults.CharHeight = 14
        Me.TextEditorResults.CharWidth = 8
        Me.TextEditorResults.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextEditorResults.DisabledColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.TextEditorResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextEditorResults.IsReplaceMode = False
        Me.TextEditorResults.Location = New System.Drawing.Point(0, 0)
        Me.TextEditorResults.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextEditorResults.Name = "TextEditorResults"
        Me.TextEditorResults.Paddings = New System.Windows.Forms.Padding(0)
        Me.TextEditorResults.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEditorResults.ServiceColors = CType(resources.GetObject("TextEditorResults.ServiceColors"), FastColoredTextBoxNS.ServiceColors)
        Me.TextEditorResults.Size = New System.Drawing.Size(775, 164)
        Me.TextEditorResults.TabIndex = 4
        Me.TextEditorResults.Zoom = 100
        '
        'SplitContainer1
        '
        Me.ExTableLayoutPanel1.SetColumnSpan(Me.SplitContainer1, 3)
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextEditorFormula)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextEditorResults)
        Me.SplitContainer1.Size = New System.Drawing.Size(775, 413)
        Me.SplitContainer1.SplitterDistance = 245
        Me.SplitContainer1.TabIndex = 5
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.ColumnCount = 3
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.ExTableLayoutPanel1.Controls.Add(Me.SplitContainer1, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonOK, 1, 1)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ButtonCancel, 2, 1)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 25)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 2
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(781, 448)
        Me.ExTableLayoutPanel1.TabIndex = 6
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(622, 422)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 6
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(703, 422)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 7
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'FormExpressionEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 473)
        Me.Controls.Add(Me.ExTableLayoutPanel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "FormExpressionEditor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Expression editor"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.TextEditorFormula, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditorResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents BT_Test As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents BT_Save As ToolStripButton
    Friend WithEvents DD_SavedExpressions As ToolStripDropDownButton
    Friend WithEvents BT_Help As ToolStripButton
    Friend WithEvents BT_Delete As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents TextEditorFormula As FastColoredTextBoxNS.FastColoredTextBox
    Friend WithEvents TextEditorResults As FastColoredTextBoxNS.FastColoredTextBox
    Friend WithEvents BT_Clear As ToolStripButton
    Friend WithEvents BT_SaveAs As ToolStripButton
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents EditorLabel As ToolStripLabel
    Friend WithEvents BT_InsertProp As ToolStripButton
    Friend WithEvents SavedExpressionsLabel As ToolStripLabel
    Friend WithEvents BT_TestOnCurrentFile As ToolStripButton
    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ComboBoxLanguage As ToolStripComboBox
End Class
