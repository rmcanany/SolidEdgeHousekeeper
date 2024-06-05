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
        Me.TextEditorResult = New ICSharpCode.TextEditor.TextEditorControl()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextEditorNCalc
        '
        Me.TextEditorNCalc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextEditorNCalc.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextEditorNCalc.Location = New System.Drawing.Point(0, 25)
        Me.TextEditorNCalc.Name = "TextEditorNCalc"
        Me.TextEditorNCalc.Size = New System.Drawing.Size(784, 200)
        Me.TextEditorNCalc.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BT_Test})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(784, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BT_Test
        '
        Me.BT_Test.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_Test.Image = CType(resources.GetObject("BT_Test.Image"), System.Drawing.Image)
        Me.BT_Test.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Test.Name = "BT_Test"
        Me.BT_Test.Size = New System.Drawing.Size(23, 22)
        Me.BT_Test.Text = "ToolStripButton1"
        '
        'TextEditorResult
        '
        Me.TextEditorResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextEditorResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextEditorResult.Location = New System.Drawing.Point(0, 225)
        Me.TextEditorResult.Name = "TextEditorResult"
        Me.TextEditorResult.ReadOnly = True
        Me.TextEditorResult.Size = New System.Drawing.Size(784, 136)
        Me.TextEditorResult.TabIndex = 2
        '
        'FormNCalc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 361)
        Me.Controls.Add(Me.TextEditorResult)
        Me.Controls.Add(Me.TextEditorNCalc)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "FormNCalc"
        Me.Text = "FormNCalc"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextEditorNCalc As ICSharpCode.TextEditor.TextEditorControl
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents BT_Test As ToolStripButton
    Friend WithEvents TextEditorResult As ICSharpCode.TextEditor.TextEditorControl
End Class
