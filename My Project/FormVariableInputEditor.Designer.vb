<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormVariableInputEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormVariableInputEditor))
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.TextBoxJSON = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CheckBoxSelectAll = New System.Windows.Forms.CheckBox()
        Me.ButtonMoveSelectedDown = New System.Windows.Forms.Button()
        Me.ButtonMoveSelectedUp = New System.Windows.Forms.Button()
        Me.ButtonClearSelected = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.CheckBoxCopyToAsm = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCopyToPar = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCopyToPsm = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCopyToDft = New System.Windows.Forms.CheckBox()
        Me.LabelCopyTo = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(557, 326)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(94, 27)
        Me.ButtonOK.TabIndex = 0
        Me.ButtonOK.Text = "OK"
        Me.ToolTip1.SetToolTip(Me.ButtonOK, "OK")
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(667, 326)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(105, 27)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.ButtonCancel, "Cancel")
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'TextBoxJSON
        '
        Me.TextBoxJSON.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxJSON.Enabled = False
        Me.TextBoxJSON.Location = New System.Drawing.Point(22, 326)
        Me.TextBoxJSON.Name = "TextBoxJSON"
        Me.TextBoxJSON.Size = New System.Drawing.Size(517, 20)
        Me.TextBoxJSON.TabIndex = 3
        '
        'CheckBoxSelectAll
        '
        Me.CheckBoxSelectAll.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxSelectAll.AutoSize = True
        Me.CheckBoxSelectAll.Location = New System.Drawing.Point(42, 33)
        Me.CheckBoxSelectAll.Name = "CheckBoxSelectAll"
        Me.CheckBoxSelectAll.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxSelectAll.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CheckBoxSelectAll, "Select all")
        Me.CheckBoxSelectAll.UseVisualStyleBackColor = True
        '
        'ButtonMoveSelectedDown
        '
        Me.ButtonMoveSelectedDown.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonMoveSelectedDown.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonMoveSelectedDown.Enabled = False
        Me.ButtonMoveSelectedDown.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.ButtonMoveSelectedDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonMoveSelectedDown.ForeColor = System.Drawing.Color.Transparent
        Me.ButtonMoveSelectedDown.Image = Global.Housekeeper.My.Resources.Resources.down
        Me.ButtonMoveSelectedDown.Location = New System.Drawing.Point(70, 3)
        Me.ButtonMoveSelectedDown.Name = "ButtonMoveSelectedDown"
        Me.ButtonMoveSelectedDown.Size = New System.Drawing.Size(25, 24)
        Me.ButtonMoveSelectedDown.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.ButtonMoveSelectedDown, "Move selected down")
        Me.ButtonMoveSelectedDown.UseVisualStyleBackColor = False
        '
        'ButtonMoveSelectedUp
        '
        Me.ButtonMoveSelectedUp.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonMoveSelectedUp.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonMoveSelectedUp.Enabled = False
        Me.ButtonMoveSelectedUp.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.ButtonMoveSelectedUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonMoveSelectedUp.ForeColor = System.Drawing.Color.Transparent
        Me.ButtonMoveSelectedUp.Image = Global.Housekeeper.My.Resources.Resources.up
        Me.ButtonMoveSelectedUp.Location = New System.Drawing.Point(37, 3)
        Me.ButtonMoveSelectedUp.Name = "ButtonMoveSelectedUp"
        Me.ButtonMoveSelectedUp.Size = New System.Drawing.Size(25, 24)
        Me.ButtonMoveSelectedUp.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.ButtonMoveSelectedUp, "Move selected up")
        Me.ButtonMoveSelectedUp.UseVisualStyleBackColor = False
        '
        'ButtonClearSelected
        '
        Me.ButtonClearSelected.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonClearSelected.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonClearSelected.Enabled = False
        Me.ButtonClearSelected.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.ButtonClearSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonClearSelected.ForeColor = System.Drawing.Color.Transparent
        Me.ButtonClearSelected.Image = Global.Housekeeper.My.Resources.Resources.Cancel
        Me.ButtonClearSelected.ImageKey = "Delete_16.png"
        Me.ButtonClearSelected.Location = New System.Drawing.Point(4, 3)
        Me.ButtonClearSelected.Name = "ButtonClearSelected"
        Me.ButtonClearSelected.Size = New System.Drawing.Size(25, 24)
        Me.ButtonClearSelected.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.ButtonClearSelected, "Clear selected")
        Me.ButtonClearSelected.UseVisualStyleBackColor = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Delete_16.png")
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(10, 10)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 9
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(765, 260)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label3, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(249, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(176, 54)
        Me.TableLayoutPanel4.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.Label3.Size = New System.Drawing.Size(98, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Number or formula"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.Label4.Size = New System.Drawing.Size(132, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Blank OK if only exposing"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(113, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Label2.Size = New System.Drawing.Size(84, 25)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Variable name"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(431, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Label5.Size = New System.Drawing.Size(59, 25)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Unit type"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(582, 41)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(0, 0, 0, 6)
        Me.Label6.Size = New System.Drawing.Size(42, 19)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Expose"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(631, 35)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Label7.Size = New System.Drawing.Size(81, 25)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Expose name"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonMoveSelectedDown, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonClearSelected, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonMoveSelectedUp, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.CheckBoxSelectAll, 1, 1)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(5, 5)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(100, 50)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'CheckBoxCopyToAsm
        '
        Me.CheckBoxCopyToAsm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxCopyToAsm.AutoSize = True
        Me.CheckBoxCopyToAsm.Location = New System.Drawing.Point(575, 290)
        Me.CheckBoxCopyToAsm.Name = "CheckBoxCopyToAsm"
        Me.CheckBoxCopyToAsm.Size = New System.Drawing.Size(45, 17)
        Me.CheckBoxCopyToAsm.TabIndex = 5
        Me.CheckBoxCopyToAsm.Text = "asm"
        Me.CheckBoxCopyToAsm.UseVisualStyleBackColor = True
        '
        'CheckBoxCopyToPar
        '
        Me.CheckBoxCopyToPar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxCopyToPar.AutoSize = True
        Me.CheckBoxCopyToPar.Location = New System.Drawing.Point(625, 290)
        Me.CheckBoxCopyToPar.Name = "CheckBoxCopyToPar"
        Me.CheckBoxCopyToPar.Size = New System.Drawing.Size(41, 17)
        Me.CheckBoxCopyToPar.TabIndex = 6
        Me.CheckBoxCopyToPar.Text = "par"
        Me.CheckBoxCopyToPar.UseVisualStyleBackColor = True
        '
        'CheckBoxCopyToPsm
        '
        Me.CheckBoxCopyToPsm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxCopyToPsm.AutoSize = True
        Me.CheckBoxCopyToPsm.Location = New System.Drawing.Point(675, 290)
        Me.CheckBoxCopyToPsm.Name = "CheckBoxCopyToPsm"
        Me.CheckBoxCopyToPsm.Size = New System.Drawing.Size(45, 17)
        Me.CheckBoxCopyToPsm.TabIndex = 7
        Me.CheckBoxCopyToPsm.Text = "psm"
        Me.CheckBoxCopyToPsm.UseVisualStyleBackColor = True
        '
        'CheckBoxCopyToDft
        '
        Me.CheckBoxCopyToDft.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxCopyToDft.AutoSize = True
        Me.CheckBoxCopyToDft.Location = New System.Drawing.Point(725, 290)
        Me.CheckBoxCopyToDft.Name = "CheckBoxCopyToDft"
        Me.CheckBoxCopyToDft.Size = New System.Drawing.Size(38, 17)
        Me.CheckBoxCopyToDft.TabIndex = 8
        Me.CheckBoxCopyToDft.Text = "dft"
        Me.CheckBoxCopyToDft.UseVisualStyleBackColor = True
        '
        'LabelCopyTo
        '
        Me.LabelCopyTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCopyTo.AutoSize = True
        Me.LabelCopyTo.Location = New System.Drawing.Point(393, 291)
        Me.LabelCopyTo.Name = "LabelCopyTo"
        Me.LabelCopyTo.Size = New System.Drawing.Size(168, 13)
        Me.LabelCopyTo.TabIndex = 9
        Me.LabelCopyTo.Text = "Copy these settings to another tab"
        '
        'FormVariableInputEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 361)
        Me.Controls.Add(Me.LabelCopyTo)
        Me.Controls.Add(Me.CheckBoxCopyToDft)
        Me.Controls.Add(Me.CheckBoxCopyToPsm)
        Me.Controls.Add(Me.CheckBoxCopyToPar)
        Me.Controls.Add(Me.CheckBoxCopyToAsm)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.TextBoxJSON)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(800, 400)
        Me.Name = "FormVariableInputEditor"
        Me.Text = "Variable Input Editor"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents TextBoxJSON As TextBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents ButtonClearSelected As Button
    Friend WithEvents ButtonMoveSelectedUp As Button
    Friend WithEvents ButtonMoveSelectedDown As Button
    Friend WithEvents CheckBoxSelectAll As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents CheckBoxCopyToAsm As CheckBox
    Friend WithEvents CheckBoxCopyToPar As CheckBox
    Friend WithEvents CheckBoxCopyToPsm As CheckBox
    Friend WithEvents CheckBoxCopyToDft As CheckBox
    Friend WithEvents LabelCopyTo As Label
    Friend WithEvents ImageList1 As ImageList
End Class
