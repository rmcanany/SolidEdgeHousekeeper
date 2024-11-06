<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPropertyListCustomize
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPropertyListCustomize))
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CustomizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TaskListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Form1BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ExTableLayoutPanel1 = New Housekeeper.ExTableLayoutPanel()
        Me.ExTableLayoutPanel2 = New Housekeeper.ExTableLayoutPanel()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.ButtonMoveDown = New System.Windows.Forms.Button()
        Me.ButtonMoveUp = New System.Windows.Forms.Button()
        Me.DataGridViewSource = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTarget = New System.Windows.Forms.DataGridView()
        Me.CheckBoxSortSourceList = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.TaskListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Form1BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExTableLayoutPanel1.SuspendLayout()
        Me.ExTableLayoutPanel2.SuspendLayout()
        CType(Me.DataGridViewSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewTarget, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(370, 323)
        Me.ButtonOK.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(88, 29)
        Me.ButtonOK.TabIndex = 1
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(475, 323)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(88, 29)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CustomizeToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(197, 26)
        '
        'CustomizeToolStripMenuItem
        '
        Me.CustomizeToolStripMenuItem.Name = "CustomizeToolStripMenuItem"
        Me.CustomizeToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.CustomizeToolStripMenuItem.Text = "Add property manually"
        '
        'TaskListBindingSource
        '
        Me.TaskListBindingSource.DataMember = "TaskList"
        Me.TaskListBindingSource.DataSource = Me.Form1BindingSource
        '
        'Form1BindingSource
        '
        Me.Form1BindingSource.DataSource = GetType(Housekeeper.Form_Main)
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExTableLayoutPanel1.ColumnCount = 3
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.ExTableLayoutPanel2, 1, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.DataGridViewSource, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.DataGridViewTarget, 2, 0)
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(14, 12)
        Me.ExTableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 1
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(556, 305)
        Me.ExTableLayoutPanel1.TabIndex = 7
        Me.ExTableLayoutPanel1.Task = Nothing
        '
        'ExTableLayoutPanel2
        '
        Me.ExTableLayoutPanel2.ColumnCount = 1
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel2.Controls.Add(Me.ButtonRemove, 0, 2)
        Me.ExTableLayoutPanel2.Controls.Add(Me.ButtonAdd, 0, 1)
        Me.ExTableLayoutPanel2.Controls.Add(Me.ButtonMoveDown, 0, 4)
        Me.ExTableLayoutPanel2.Controls.Add(Me.ButtonMoveUp, 0, 3)
        Me.ExTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel2.Location = New System.Drawing.Point(223, 3)
        Me.ExTableLayoutPanel2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ExTableLayoutPanel2.Name = "ExTableLayoutPanel2"
        Me.ExTableLayoutPanel2.RowCount = 6
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel2.Size = New System.Drawing.Size(109, 299)
        Me.ExTableLayoutPanel2.TabIndex = 4
        Me.ExTableLayoutPanel2.Task = Nothing
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonRemove.Location = New System.Drawing.Point(10, 116)
        Me.ButtonRemove.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(88, 29)
        Me.ButtonRemove.TabIndex = 9
        Me.ButtonRemove.Text = "<-- Remove"
        Me.ButtonRemove.UseVisualStyleBackColor = True
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonAdd.Location = New System.Drawing.Point(10, 79)
        Me.ButtonAdd.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(88, 29)
        Me.ButtonAdd.TabIndex = 8
        Me.ButtonAdd.Text = "Add -->"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'ButtonMoveDown
        '
        Me.ButtonMoveDown.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonMoveDown.Location = New System.Drawing.Point(10, 190)
        Me.ButtonMoveDown.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonMoveDown.Name = "ButtonMoveDown"
        Me.ButtonMoveDown.Size = New System.Drawing.Size(88, 29)
        Me.ButtonMoveDown.TabIndex = 5
        Me.ButtonMoveDown.Text = "Move down"
        Me.ButtonMoveDown.UseVisualStyleBackColor = True
        '
        'ButtonMoveUp
        '
        Me.ButtonMoveUp.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonMoveUp.Location = New System.Drawing.Point(10, 153)
        Me.ButtonMoveUp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ButtonMoveUp.Name = "ButtonMoveUp"
        Me.ButtonMoveUp.Size = New System.Drawing.Size(88, 29)
        Me.ButtonMoveUp.TabIndex = 4
        Me.ButtonMoveUp.Text = "Move up"
        Me.ButtonMoveUp.UseVisualStyleBackColor = True
        '
        'DataGridViewSource
        '
        Me.DataGridViewSource.AllowUserToAddRows = False
        Me.DataGridViewSource.AllowUserToDeleteRows = False
        Me.DataGridViewSource.AllowUserToResizeRows = False
        Me.DataGridViewSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewSource.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewSource.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.DataGridViewSource.Location = New System.Drawing.Point(4, 3)
        Me.DataGridViewSource.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DataGridViewSource.Name = "DataGridViewSource"
        Me.DataGridViewSource.ReadOnly = True
        Me.DataGridViewSource.RowHeadersVisible = False
        Me.DataGridViewSource.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewSource.Size = New System.Drawing.Size(211, 299)
        Me.DataGridViewSource.TabIndex = 3
        '
        'DataGridViewTarget
        '
        Me.DataGridViewTarget.AllowUserToAddRows = False
        Me.DataGridViewTarget.AllowUserToDeleteRows = False
        Me.DataGridViewTarget.AllowUserToResizeRows = False
        Me.DataGridViewTarget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewTarget.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DataGridViewTarget.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewTarget.Location = New System.Drawing.Point(340, 3)
        Me.DataGridViewTarget.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DataGridViewTarget.Name = "DataGridViewTarget"
        Me.DataGridViewTarget.ReadOnly = True
        Me.DataGridViewTarget.RowHeadersVisible = False
        Me.DataGridViewTarget.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewTarget.Size = New System.Drawing.Size(212, 299)
        Me.DataGridViewTarget.TabIndex = 5
        '
        'CheckBoxSortSourceList
        '
        Me.CheckBoxSortSourceList.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSortSourceList.AutoSize = True
        Me.CheckBoxSortSourceList.Location = New System.Drawing.Point(18, 325)
        Me.CheckBoxSortSourceList.Name = "CheckBoxSortSourceList"
        Me.CheckBoxSortSourceList.Size = New System.Drawing.Size(228, 19)
        Me.CheckBoxSortSourceList.TabIndex = 8
        Me.CheckBoxSortSourceList.Text = "Sort available properties alphabetically"
        Me.CheckBoxSortSourceList.UseVisualStyleBackColor = True
        '
        'FormPropertyListCustomize
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 361)
        Me.Controls.Add(Me.CheckBoxSortSourceList)
        Me.Controls.Add(Me.ExTableLayoutPanel1)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "FormPropertyListCustomize"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Customize Property List"
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.TaskListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Form1BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.ExTableLayoutPanel2.ResumeLayout(False)
        CType(Me.DataGridViewSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewTarget, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents DataGridViewSource As DataGridView
    Friend WithEvents TaskListBindingSource As BindingSource
    Friend WithEvents Form1BindingSource As BindingSource
    Friend WithEvents ButtonMoveUp As Button
    Friend WithEvents ButtonMoveDown As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CustomizeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExTableLayoutPanel1 As ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel2 As ExTableLayoutPanel
    Friend WithEvents ButtonAdd As Button
    Friend WithEvents ButtonRemove As Button
    Friend WithEvents DataGridViewTarget As DataGridView
    Friend WithEvents CheckBoxSortSourceList As CheckBox
End Class
