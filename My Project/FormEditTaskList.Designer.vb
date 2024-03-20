<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEditTaskList
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
        Me.CheckBoxManuallySelectFiletypes = New System.Windows.Forms.CheckBox()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TaskListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ButtonMoveUp = New System.Windows.Forms.Button()
        Me.ButtonMoveDown = New System.Windows.Forms.Button()
        Me.Form1BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TaskListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Form1BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CheckBoxManuallySelectFiletypes
        '
        Me.CheckBoxManuallySelectFiletypes.AutoSize = True
        Me.CheckBoxManuallySelectFiletypes.Location = New System.Drawing.Point(24, 32)
        Me.CheckBoxManuallySelectFiletypes.Name = "CheckBoxManuallySelectFiletypes"
        Me.CheckBoxManuallySelectFiletypes.Size = New System.Drawing.Size(257, 17)
        Me.CheckBoxManuallySelectFiletypes.TabIndex = 0
        Me.CheckBoxManuallySelectFiletypes.Text = "Manually select file types when a task is selected"
        Me.CheckBoxManuallySelectFiletypes.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(592, 399)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 1
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(682, 399)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.DataGridView1.Location = New System.Drawing.Point(24, 86)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(475, 313)
        Me.DataGridView1.TabIndex = 3
        '
        'TaskListBindingSource
        '
        Me.TaskListBindingSource.DataMember = "TaskList"
        Me.TaskListBindingSource.DataSource = Me.Form1BindingSource
        '
        'ButtonMoveUp
        '
        Me.ButtonMoveUp.Location = New System.Drawing.Point(277, 415)
        Me.ButtonMoveUp.Name = "ButtonMoveUp"
        Me.ButtonMoveUp.Size = New System.Drawing.Size(75, 23)
        Me.ButtonMoveUp.TabIndex = 4
        Me.ButtonMoveUp.Text = "Move up"
        Me.ButtonMoveUp.UseVisualStyleBackColor = True
        '
        'ButtonMoveDown
        '
        Me.ButtonMoveDown.Location = New System.Drawing.Point(192, 415)
        Me.ButtonMoveDown.Name = "ButtonMoveDown"
        Me.ButtonMoveDown.Size = New System.Drawing.Size(75, 23)
        Me.ButtonMoveDown.TabIndex = 5
        Me.ButtonMoveDown.Text = "Move down"
        Me.ButtonMoveDown.UseVisualStyleBackColor = True
        '
        'Form1BindingSource
        '
        Me.Form1BindingSource.DataSource = GetType(Housekeeper.Form1)
        '
        'FormEditTaskList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.ButtonMoveDown)
        Me.Controls.Add(Me.ButtonMoveUp)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.CheckBoxManuallySelectFiletypes)
        Me.Name = "FormEditTaskList"
        Me.Text = "FormEditTaskList"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TaskListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Form1BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CheckBoxManuallySelectFiletypes As CheckBox
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents TaskListBindingSource As BindingSource
    Friend WithEvents Form1BindingSource As BindingSource
    Friend WithEvents ButtonMoveUp As Button
    Friend WithEvents ButtonMoveDown As Button
End Class
