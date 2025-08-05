<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBlockLibraryBlockNames
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
        Me.ExTableLayoutPanel1 = New Housekeeper.ExTableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExTableLayoutPanel2 = New Housekeeper.ExTableLayoutPanel()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.ExTableLayoutPanel3 = New Housekeeper.ExTableLayoutPanel()
        Me.TextBoxMaxNumChars = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ExTableLayoutPanel4 = New Housekeeper.ExTableLayoutPanel()
        Me.TextBoxMaxNumBlockViews = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ExTableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExTableLayoutPanel2.SuspendLayout()
        Me.ExTableLayoutPanel3.SuspendLayout()
        Me.ExTableLayoutPanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.ExTableLayoutPanel1.ColumnCount = 1
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.DataGridView1, 0, 1)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ExTableLayoutPanel2, 0, 4)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ExTableLayoutPanel3, 0, 2)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ExTableLayoutPanel4, 0, 3)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 5
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(303, 304)
        Me.ExTableLayoutPanel1.TabIndex = 0
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(215, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "To copy text select a row and enter CTRL-C"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 33)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 30
        Me.DataGridView1.Size = New System.Drawing.Size(297, 168)
        Me.DataGridView1.TabIndex = 1
        '
        'Column1
        '
        Me.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column1.HeaderText = "Library Blocks"
        Me.Column1.Name = "Column1"
        '
        'ExTableLayoutPanel2
        '
        Me.ExTableLayoutPanel2.ColumnCount = 2
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ExTableLayoutPanel2.Controls.Add(Me.ButtonClose, 1, 0)
        Me.ExTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel2.Location = New System.Drawing.Point(3, 267)
        Me.ExTableLayoutPanel2.Name = "ExTableLayoutPanel2"
        Me.ExTableLayoutPanel2.RowCount = 1
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.Size = New System.Drawing.Size(297, 34)
        Me.ExTableLayoutPanel2.TabIndex = 2
        Me.ExTableLayoutPanel2.Task = Nothing
        '
        'ButtonClose
        '
        Me.ButtonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ButtonClose.Location = New System.Drawing.Point(219, 6)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(75, 25)
        Me.ButtonClose.TabIndex = 1
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'ExTableLayoutPanel3
        '
        Me.ExTableLayoutPanel3.ColumnCount = 2
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.ExTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel3.Controls.Add(Me.TextBoxMaxNumChars, 0, 0)
        Me.ExTableLayoutPanel3.Controls.Add(Me.Label2, 1, 0)
        Me.ExTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel3.Location = New System.Drawing.Point(3, 207)
        Me.ExTableLayoutPanel3.Name = "ExTableLayoutPanel3"
        Me.ExTableLayoutPanel3.RowCount = 1
        Me.ExTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.ExTableLayoutPanel3.Size = New System.Drawing.Size(297, 24)
        Me.ExTableLayoutPanel3.TabIndex = 3
        Me.ExTableLayoutPanel3.Task = Nothing
        '
        'TextBoxMaxNumChars
        '
        Me.TextBoxMaxNumChars.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxMaxNumChars.Location = New System.Drawing.Point(3, 3)
        Me.TextBoxMaxNumChars.Name = "TextBoxMaxNumChars"
        Me.TextBoxMaxNumChars.Size = New System.Drawing.Size(44, 20)
        Me.TextBoxMaxNumChars.TabIndex = 0
        Me.TextBoxMaxNumChars.Text = "30"
        Me.TextBoxMaxNumChars.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(53, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(142, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Longest block name allowed"
        '
        'ExTableLayoutPanel4
        '
        Me.ExTableLayoutPanel4.ColumnCount = 2
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel4.Controls.Add(Me.TextBoxMaxNumBlockViews, 0, 0)
        Me.ExTableLayoutPanel4.Controls.Add(Me.Label3, 1, 0)
        Me.ExTableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel4.Location = New System.Drawing.Point(3, 237)
        Me.ExTableLayoutPanel4.Name = "ExTableLayoutPanel4"
        Me.ExTableLayoutPanel4.RowCount = 1
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.ExTableLayoutPanel4.Size = New System.Drawing.Size(297, 24)
        Me.ExTableLayoutPanel4.TabIndex = 4
        Me.ExTableLayoutPanel4.Task = Nothing
        '
        'TextBoxMaxNumBlockViews
        '
        Me.TextBoxMaxNumBlockViews.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxMaxNumBlockViews.Location = New System.Drawing.Point(3, 3)
        Me.TextBoxMaxNumBlockViews.Name = "TextBoxMaxNumBlockViews"
        Me.TextBoxMaxNumBlockViews.Size = New System.Drawing.Size(44, 20)
        Me.TextBoxMaxNumBlockViews.TabIndex = 0
        Me.TextBoxMaxNumBlockViews.Text = "2"
        Me.TextBoxMaxNumBlockViews.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(53, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(160, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Maximum number of block views"
        '
        'FormBlockLibraryBlockNames
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 304)
        Me.Controls.Add(Me.ExTableLayoutPanel1)
        Me.Name = "FormBlockLibraryBlockNames"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Library Available Blocks"
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExTableLayoutPanel2.ResumeLayout(False)
        Me.ExTableLayoutPanel3.ResumeLayout(False)
        Me.ExTableLayoutPanel3.PerformLayout()
        Me.ExTableLayoutPanel4.ResumeLayout(False)
        Me.ExTableLayoutPanel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents ExTableLayoutPanel2 As ExTableLayoutPanel
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents ButtonClose As Button
    Friend WithEvents ExTableLayoutPanel3 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel4 As ExTableLayoutPanel
    Friend WithEvents TextBoxMaxNumChars As TextBox
    Friend WithEvents TextBoxMaxNumBlockViews As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
