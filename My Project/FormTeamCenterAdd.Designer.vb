<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTeamCenterAdd
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
        Me.TextBoxItemID = New System.Windows.Forms.TextBox()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.TextBoxRev = New System.Windows.Forms.TextBox()
        Me.ListViewTeamCenterItems = New System.Windows.Forms.ListView()
        Me.fileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.itemID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.revision = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ButtonDownload = New System.Windows.Forms.Button()
        Me.ListViewDownloadedFiles = New System.Windows.Forms.ListView()
        Me.fileNam = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.filePath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LabelDownloadStatus = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelSearchStatus = New System.Windows.Forms.Label()
        Me.ButtonAddAndClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TextBoxItemID
        '
        Me.TextBoxItemID.Location = New System.Drawing.Point(62, 12)
        Me.TextBoxItemID.Name = "TextBoxItemID"
        Me.TextBoxItemID.Size = New System.Drawing.Size(76, 20)
        Me.TextBoxItemID.TabIndex = 0
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Location = New System.Drawing.Point(243, 13)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSearch.TabIndex = 1
        Me.ButtonSearch.Text = "Search"
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'TextBoxRev
        '
        Me.TextBoxRev.Location = New System.Drawing.Point(198, 13)
        Me.TextBoxRev.Name = "TextBoxRev"
        Me.TextBoxRev.Size = New System.Drawing.Size(39, 20)
        Me.TextBoxRev.TabIndex = 2
        '
        'ListViewTeamCenterItems
        '
        Me.ListViewTeamCenterItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListViewTeamCenterItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.fileName, Me.itemID, Me.revision})
        Me.ListViewTeamCenterItems.HideSelection = False
        Me.ListViewTeamCenterItems.Location = New System.Drawing.Point(12, 42)
        Me.ListViewTeamCenterItems.Name = "ListViewTeamCenterItems"
        Me.ListViewTeamCenterItems.Size = New System.Drawing.Size(306, 191)
        Me.ListViewTeamCenterItems.TabIndex = 3
        Me.ListViewTeamCenterItems.UseCompatibleStateImageBehavior = False
        Me.ListViewTeamCenterItems.View = System.Windows.Forms.View.Details
        '
        'fileName
        '
        Me.fileName.Text = "File Name"
        Me.fileName.Width = 100
        '
        'itemID
        '
        Me.itemID.Text = "Item ID"
        Me.itemID.Width = 100
        '
        'revision
        '
        Me.revision.Text = "Revision"
        '
        'ButtonDownload
        '
        Me.ButtonDownload.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDownload.Location = New System.Drawing.Point(324, 120)
        Me.ButtonDownload.Name = "ButtonDownload"
        Me.ButtonDownload.Size = New System.Drawing.Size(108, 23)
        Me.ButtonDownload.TabIndex = 4
        Me.ButtonDownload.Text = "Download Selected"
        Me.ButtonDownload.UseVisualStyleBackColor = True
        '
        'ListViewDownloadedFiles
        '
        Me.ListViewDownloadedFiles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListViewDownloadedFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.fileNam, Me.filePath})
        Me.ListViewDownloadedFiles.HideSelection = False
        Me.ListViewDownloadedFiles.Location = New System.Drawing.Point(438, 42)
        Me.ListViewDownloadedFiles.Name = "ListViewDownloadedFiles"
        Me.ListViewDownloadedFiles.Size = New System.Drawing.Size(324, 191)
        Me.ListViewDownloadedFiles.TabIndex = 5
        Me.ListViewDownloadedFiles.UseCompatibleStateImageBehavior = False
        Me.ListViewDownloadedFiles.View = System.Windows.Forms.View.Details
        '
        'fileNam
        '
        Me.fileNam.Text = "File Name"
        Me.fileNam.Width = 100
        '
        'filePath
        '
        Me.filePath.Text = "File Path"
        Me.filePath.Width = 1000
        '
        'LabelDownloadStatus
        '
        Me.LabelDownloadStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelDownloadStatus.AutoSize = True
        Me.LabelDownloadStatus.Location = New System.Drawing.Point(324, 146)
        Me.LabelDownloadStatus.Name = "LabelDownloadStatus"
        Me.LabelDownloadStatus.Size = New System.Drawing.Size(0, 13)
        Me.LabelDownloadStatus.TabIndex = 6
        Me.LabelDownloadStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Item ID:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(144, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Revision:"
        '
        'LabelSearchStatus
        '
        Me.LabelSearchStatus.AutoSize = True
        Me.LabelSearchStatus.Location = New System.Drawing.Point(324, 18)
        Me.LabelSearchStatus.Name = "LabelSearchStatus"
        Me.LabelSearchStatus.Size = New System.Drawing.Size(0, 13)
        Me.LabelSearchStatus.TabIndex = 9
        '
        'ButtonAddAndClose
        '
        Me.ButtonAddAndClose.Location = New System.Drawing.Point(677, 244)
        Me.ButtonAddAndClose.Name = "ButtonAddAndClose"
        Me.ButtonAddAndClose.Size = New System.Drawing.Size(85, 23)
        Me.ButtonAddAndClose.TabIndex = 10
        Me.ButtonAddAndClose.Text = "Add and close"
        Me.ButtonAddAndClose.UseVisualStyleBackColor = True
        '
        'FormTeamCenterAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 279)
        Me.Controls.Add(Me.ButtonAddAndClose)
        Me.Controls.Add(Me.LabelSearchStatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelDownloadStatus)
        Me.Controls.Add(Me.ListViewDownloadedFiles)
        Me.Controls.Add(Me.ButtonDownload)
        Me.Controls.Add(Me.ListViewTeamCenterItems)
        Me.Controls.Add(Me.TextBoxRev)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.TextBoxItemID)
        Me.Name = "FormTeamCenterAdd"
        Me.Text = "Add TeamCenter Item"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxItemID As TextBox
    Friend WithEvents ButtonSearch As Button
    Friend WithEvents TextBoxRev As TextBox
    Friend WithEvents ListViewTeamCenterItems As ListView
    Friend WithEvents fileName As ColumnHeader
    Friend WithEvents revision As ColumnHeader
    Friend WithEvents itemID As ColumnHeader
    Friend WithEvents ButtonDownload As Button
    Friend WithEvents ListViewDownloadedFiles As ListView
    Friend WithEvents fileNam As ColumnHeader
    Friend WithEvents filePath As ColumnHeader
    Friend WithEvents LabelDownloadStatus As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LabelSearchStatus As Label
    Friend WithEvents ButtonAddAndClose As Button
End Class
