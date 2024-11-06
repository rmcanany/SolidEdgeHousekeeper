<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormNewVersionAvailable
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNewVersionAvailable))
        Me.ExTableLayoutPanel1 = New Housekeeper.ExTableLayoutPanel()
        Me.LabelNewVersionAvailable = New System.Windows.Forms.Label()
        Me.LinkLabelReleaseNotes = New System.Windows.Forms.LinkLabel()
        Me.LinkLabelInstallationInstructions = New System.Windows.Forms.LinkLabel()
        Me.LinkLabelDownloadPage = New System.Windows.Forms.LinkLabel()
        Me.ExTableLayoutPanel2 = New Housekeeper.ExTableLayoutPanel()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.LabelDisable = New System.Windows.Forms.Label()
        Me.ExTableLayoutPanel1.SuspendLayout()
        Me.ExTableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.ColumnCount = 1
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.LabelNewVersionAvailable, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.LinkLabelReleaseNotes, 0, 1)
        Me.ExTableLayoutPanel1.Controls.Add(Me.LinkLabelInstallationInstructions, 0, 2)
        Me.ExTableLayoutPanel1.Controls.Add(Me.LinkLabelDownloadPage, 0, 3)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ExTableLayoutPanel2, 0, 4)
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 5
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(384, 161)
        Me.ExTableLayoutPanel1.TabIndex = 0
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'LabelNewVersionAvailable
        '
        Me.LabelNewVersionAvailable.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelNewVersionAvailable.AutoSize = True
        Me.LabelNewVersionAvailable.Location = New System.Drawing.Point(3, 7)
        Me.LabelNewVersionAvailable.Name = "LabelNewVersionAvailable"
        Me.LabelNewVersionAvailable.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelNewVersionAvailable.Size = New System.Drawing.Size(46, 15)
        Me.LabelNewVersionAvailable.TabIndex = 0
        Me.LabelNewVersionAvailable.Text = "Label1"
        '
        'LinkLabelReleaseNotes
        '
        Me.LinkLabelReleaseNotes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LinkLabelReleaseNotes.AutoSize = True
        Me.LinkLabelReleaseNotes.Location = New System.Drawing.Point(3, 37)
        Me.LinkLabelReleaseNotes.Name = "LinkLabelReleaseNotes"
        Me.LinkLabelReleaseNotes.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LinkLabelReleaseNotes.Size = New System.Drawing.Size(85, 15)
        Me.LinkLabelReleaseNotes.TabIndex = 1
        Me.LinkLabelReleaseNotes.TabStop = True
        Me.LinkLabelReleaseNotes.Text = "Release Notes"
        '
        'LinkLabelInstallationInstructions
        '
        Me.LinkLabelInstallationInstructions.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LinkLabelInstallationInstructions.AutoSize = True
        Me.LinkLabelInstallationInstructions.Location = New System.Drawing.Point(3, 67)
        Me.LinkLabelInstallationInstructions.Name = "LinkLabelInstallationInstructions"
        Me.LinkLabelInstallationInstructions.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LinkLabelInstallationInstructions.Size = New System.Drawing.Size(135, 15)
        Me.LinkLabelInstallationInstructions.TabIndex = 2
        Me.LinkLabelInstallationInstructions.TabStop = True
        Me.LinkLabelInstallationInstructions.Text = "Installation Instructions"
        '
        'LinkLabelDownloadPage
        '
        Me.LinkLabelDownloadPage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LinkLabelDownloadPage.AutoSize = True
        Me.LinkLabelDownloadPage.Location = New System.Drawing.Point(3, 97)
        Me.LinkLabelDownloadPage.Name = "LinkLabelDownloadPage"
        Me.LinkLabelDownloadPage.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LinkLabelDownloadPage.Size = New System.Drawing.Size(100, 15)
        Me.LinkLabelDownloadPage.TabIndex = 3
        Me.LinkLabelDownloadPage.TabStop = True
        Me.LinkLabelDownloadPage.Text = "Downloads Page"
        '
        'ExTableLayoutPanel2
        '
        Me.ExTableLayoutPanel2.ColumnCount = 2
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.ExTableLayoutPanel2.Controls.Add(Me.ButtonOK, 1, 0)
        Me.ExTableLayoutPanel2.Controls.Add(Me.LabelDisable, 0, 0)
        Me.ExTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel2.Location = New System.Drawing.Point(3, 123)
        Me.ExTableLayoutPanel2.Name = "ExTableLayoutPanel2"
        Me.ExTableLayoutPanel2.RowCount = 1
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanel2.Size = New System.Drawing.Size(378, 35)
        Me.ExTableLayoutPanel2.TabIndex = 4
        Me.ExTableLayoutPanel2.Task = Nothing
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonOK.Location = New System.Drawing.Point(306, 6)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(69, 23)
        Me.ButtonOK.TabIndex = 4
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'LabelDisable
        '
        Me.LabelDisable.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelDisable.AutoSize = True
        Me.LabelDisable.Location = New System.Drawing.Point(3, 10)
        Me.LabelDisable.Name = "LabelDisable"
        Me.LabelDisable.Size = New System.Drawing.Size(236, 15)
        Me.LabelDisable.TabIndex = 5
        Me.LabelDisable.Text = "Disable this check on the Configuration Tab"
        '
        'FormNewVersionAvailable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(384, 161)
        Me.Controls.Add(Me.ExTableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormNewVersionAvailable"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "New Version Available"
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel1.PerformLayout()
        Me.ExTableLayoutPanel2.ResumeLayout(False)
        Me.ExTableLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents LabelNewVersionAvailable As Label
    Friend WithEvents LinkLabelReleaseNotes As LinkLabel
    Friend WithEvents LinkLabelInstallationInstructions As LinkLabel
    Friend WithEvents LinkLabelDownloadPage As LinkLabel
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ExTableLayoutPanel2 As ExTableLayoutPanel
    Friend WithEvents LabelDisable As Label
End Class
