<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormNCalc
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNCalc))
        Me.TextEditorNCalc = New ICSharpCode.TextEditor.TextEditorControl()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BT_Test = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_Save = New System.Windows.Forms.ToolStripButton()
        Me.DD_SavedExpressions = New System.Windows.Forms.ToolStripDropDownButton()
        Me.TextEditorResult = New ICSharpCode.TextEditor.TextEditorControl()
        Me.BT_Help = New System.Windows.Forms.ToolStripButton()
        Me.BT_Delete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextEditorNCalc
        '
        Me.TextEditorNCalc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextEditorNCalc.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextEditorNCalc.Highlighting = Nothing
        Me.TextEditorNCalc.Location = New System.Drawing.Point(0, 25)
        Me.TextEditorNCalc.Name = "TextEditorNCalc"
        Me.TextEditorNCalc.Size = New System.Drawing.Size(784, 200)
        Me.TextEditorNCalc.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BT_Test, Me.ToolStripSeparator1, Me.DD_SavedExpressions, Me.BT_Save, Me.BT_Delete, Me.ToolStripSeparator2, Me.BT_Help})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(784, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BT_Test
        '
        Me.BT_Test.Image = Global.Housekeeper.My.Resources.Resources.Play
        Me.BT_Test.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Test.Name = "BT_Test"
        Me.BT_Test.Size = New System.Drawing.Size(47, 22)
        Me.BT_Test.Text = "Test"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BT_Save
        '
        Me.BT_Save.Image = Global.Housekeeper.My.Resources.Resources.Save
        Me.BT_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Save.Name = "BT_Save"
        Me.BT_Save.Size = New System.Drawing.Size(51, 22)
        Me.BT_Save.Text = "Save"
        '
        'DD_SavedExpressions
        '
        Me.DD_SavedExpressions.Image = Global.Housekeeper.My.Resources.Resources.list
        Me.DD_SavedExpressions.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.DD_SavedExpressions.Name = "DD_SavedExpressions"
        Me.DD_SavedExpressions.Size = New System.Drawing.Size(131, 22)
        Me.DD_SavedExpressions.Text = "Saved Expressions"
        '
        'TextEditorResult
        '
        Me.TextEditorResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextEditorResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextEditorResult.Highlighting = Nothing
        Me.TextEditorResult.Location = New System.Drawing.Point(0, 225)
        Me.TextEditorResult.Name = "TextEditorResult"
        Me.TextEditorResult.ReadOnly = True
        Me.TextEditorResult.Size = New System.Drawing.Size(784, 136)
        Me.TextEditorResult.TabIndex = 2
        '
        'BT_Help
        '
        Me.BT_Help.Image = Global.Housekeeper.My.Resources.Resources.Help
        Me.BT_Help.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Help.Name = "BT_Help"
        Me.BT_Help.Size = New System.Drawing.Size(52, 22)
        Me.BT_Help.Text = "Help"
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
        'FormNCalc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 361)
        Me.Controls.Add(Me.TextEditorResult)
        Me.Controls.Add(Me.TextEditorNCalc)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormNCalc"
        Me.Text = "Property Formula Editor"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextEditorNCalc As ICSharpCode.TextEditor.TextEditorControl
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents BT_Test As ToolStripButton
    Friend WithEvents TextEditorResult As ICSharpCode.TextEditor.TextEditorControl
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents BT_Save As ToolStripButton
    Friend WithEvents DD_SavedExpressions As ToolStripDropDownButton
    Friend WithEvents BT_Help As ToolStripButton
    Friend WithEvents BT_Delete As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
End Class
