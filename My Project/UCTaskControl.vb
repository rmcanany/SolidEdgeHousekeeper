Option Strict On

Public Class UCTaskControl

    Public Property Task As Task

    Public Property AutoHideOptions As Boolean

    Private Property OwnedForms As List(Of Form)

    Dim t As Timer = New Timer()


    Public Sub New(Task As Task)
        ' This call is required by the designer.
        InitializeComponent()

        Me.Task = Task
        Me.TaskName.Text = Task.Description
        Me.LBImage.Image = Task.Image

        Me.CBExpand.Enabled = Task.HasOptions
        If Task.HasOptions Then
            Me.CBExpand.Image = My.Resources.expand
        End If

        Me.CBAssembly.Enabled = Task.AppliesToAssembly
        If Task.AppliesToAssembly Then CBAssembly.Image = My.Resources.Unchecked

        Me.CBPart.Enabled = Task.AppliesToPart
        If Task.AppliesToPart Then CBPart.Image = My.Resources.Unchecked

        Me.CBSheetmetal.Enabled = Task.AppliesToSheetmetal
        If Task.AppliesToSheetmetal Then CBSheetmetal.Image = My.Resources.Unchecked

        Me.CBDraft.Enabled = Task.AppliesToDraft
        If Task.AppliesToDraft Then CBDraft.Image = My.Resources.Unchecked

        Me.Dock = DockStyle.Top

        Me.OwnedForms = New List(Of Form)

    End Sub


    Public Sub AddTaskOptionsTLP(TaskOptionsTLP As ExTableLayoutPanel)
        Dim col_idx As Integer = Me.TLP.ColumnCount - 2
        Dim row_idx As Integer = 1
        Me.TLP.Controls.Add(TaskOptionsTLP, col_idx, row_idx)
    End Sub

    Private Sub UpdateAppliesToCBs(Enabled As Boolean)

        ' Changing the checkstate should update the relevant task property

        If Enabled Then
            If Task.AppliesToAssembly Then CBAssembly.Checked = True
            If Task.AppliesToPart Then CBPart.Checked = True
            If Task.AppliesToSheetmetal Then CBSheetmetal.Checked = True
            If Task.AppliesToDraft Then CBDraft.Checked = True
        Else
            If Task.AppliesToAssembly Then CBAssembly.Checked = False
            If Task.AppliesToPart Then CBPart.Checked = False
            If Task.AppliesToSheetmetal Then CBSheetmetal.Checked = False
            If Task.AppliesToDraft Then CBDraft.Checked = False
        End If
    End Sub


    Private Sub CBEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles CBEnabled.CheckedChanged

        Me.Task.IsSelectedTask = CBEnabled.Checked

        If CBEnabled.Checked Then
            CBEnabled.Image = My.Resources.Checked
            If Task.HasOptions Then
                If Not Me.AutoHideOptions Then
                    CBExpand.Checked = True
                End If
            End If
        Else
            CBEnabled.Image = My.Resources.Unchecked
            If Task.HasOptions Then
                CBExpand.Checked = False
            End If
        End If

        UpdateAppliesToCBs(CBEnabled.Checked)


    End Sub

    Private Sub CBExpand_CheckedChanged(sender As Object, e As EventArgs) Handles CBExpand.CheckedChanged

        If Task.HasOptions Then
            Me.Task.TaskOptionsTLP.Visible = CBExpand.Checked

            If CBExpand.Checked Then
                CBExpand.Image = My.Resources.collapse
            Else
                CBExpand.Image = My.Resources.expand
            End If
        End If

    End Sub

    Private Sub CBAssembly_CheckedChanged(sender As Object, e As EventArgs) Handles CBAssembly.CheckedChanged
        Me.Task.IsSelectedAssembly = CBAssembly.Checked

        If CBAssembly.Checked Then
            CBAssembly.Image = My.Resources.Checked
        Else
            CBAssembly.Image = My.Resources.Unchecked
        End If

    End Sub

    Private Sub CBPart_CheckedChanged(sender As Object, e As EventArgs) Handles CBPart.CheckedChanged
        Me.Task.IsSelectedPart = CBPart.Checked

        If CBPart.Checked Then
            CBPart.Image = My.Resources.Checked
        Else
            CBPart.Image = My.Resources.Unchecked
        End If

    End Sub

    Private Sub CBSheetmetal_CheckedChanged(sender As Object, e As EventArgs) Handles CBSheetmetal.CheckedChanged
        Me.Task.IsSelectedSheetmetal = CBSheetmetal.Checked

        If CBSheetmetal.Checked Then
            CBSheetmetal.Image = My.Resources.Checked
        Else
            CBSheetmetal.Image = My.Resources.Unchecked
        End If

    End Sub

    Private Sub CBDraft_CheckedChanged(sender As Object, e As EventArgs) Handles CBDraft.CheckedChanged
        Me.Task.IsSelectedDraft = CBDraft.Checked

        If CBDraft.Checked Then
            CBDraft.Image = My.Resources.Checked
        Else
            CBDraft.Image = My.Resources.Unchecked
        End If

    End Sub

    Private Sub HelpButton_Click(sender As Object, e As EventArgs) Handles HelpButton.Click
        System.Diagnostics.Process.Start(Me.Task.HelpURL)
    End Sub

    Private Sub UCTaskControl_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

        ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                                Color.Gray, 0, ButtonBorderStyle.Solid,
                                Color.Gray, 1, ButtonBorderStyle.Solid,
                                Color.Gray, 0, ButtonBorderStyle.Solid,
                                Color.Gray, 0, ButtonBorderStyle.Solid)
        'left, top, right, bottom

    End Sub

    Private Sub InsertPropertyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertPropertyToolStripMenuItem.Click

        Dim TextBox = CType(ContextMenuStrip1.SourceControl, TextBox)
        Dim CaretPosition = TextBox.Text.Length

        Dim FPP As New FormPropertyPicker

        FPP.ShowDialog()

        If FPP.DialogResult = DialogResult.OK Then
            TextBox.Text = TextBox.Text.Insert(CaretPosition, FPP.PropertyString)

        End If

    End Sub

    Private Sub ExpressionEditorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpressionEditorToolStripMenuItem.Click
        'MsgBox("In UCTaskControl.vb got a ExpressionEditorToolStripMenuItem.Click event")

        Dim TaskType As Type = Me.Task.GetType

        If Not (TaskType.Name = "TaskSaveDrawingAs" Or TaskType.Name = "TaskSaveModelAs") Then
            MsgBox($"Expression editing not implemented for {Me.Task.Description}")
            Exit Sub
        End If

        t.Interval = 1500
        AddHandler t.Tick, AddressOf HandleTimerTick


        Dim tmp As New FormExpressionEditor

        Select Case Form_Main.ExpressionEditorLanguage.ToLower
            Case "vb"
                tmp.TextEditorFormula.Language = FastColoredTextBoxNS.Language.VB
            Case "sql"
                tmp.TextEditorFormula.Language = FastColoredTextBoxNS.Language.SQL
            Case Else
                MsgBox($"Unrecognized expression editor language '{Form_Main.ExpressionEditorLanguage}'", vbOKOnly)
        End Select

        tmp.ShowDialog()
        Dim A = tmp.Formula.Replace(vbCrLf, "")
        A = A.Split(CType("\\", Char)).First

        If A <> "" Then
            Clipboard.SetText(A)
            MessageTimeOut("Expression copied in clipboard", "Expression editor", 1)
        End If

    End Sub

    Private Sub MessageTimeOut(sMessage As String, sTitle As String, iSeconds As Integer)

        Dim tmpForm As New Form
        Dim tmpSize = New Size(200, 75)
        tmpForm.Text = String.Empty
        tmpForm.ControlBox = False
        tmpForm.BackColor = Color.White

        tmpForm.Size = tmpSize
        tmpForm.StartPosition = FormStartPosition.Manual
        tmpForm.Location = New Point(CInt(Me.Left + Me.Width / 2 - tmpForm.Width / 2), CInt(Me.Top + Me.Height / 2 - tmpForm.Height / 2))
        'tmpForm.StartPosition = FormStartPosition.CenterParent

        Dim tmpLabel As New Label
        tmpLabel.Font = New Font(Me.Font.Name, 8, FontStyle.Bold)
        tmpLabel.Width = 180
        tmpLabel.Dock = DockStyle.Fill
        tmpLabel.TextAlign = ContentAlignment.MiddleCenter
        tmpLabel.Text = "Expression copied to clipboard"
        tmpForm.Controls.Add(tmpLabel)

        Me.OwnedForms.Add(tmpForm)

        tmpForm.Show(Me)

        t.Start()

    End Sub

    Private Sub HandleTimerTick(sender As Object, e As EventArgs)

        For Each item In Me.OwnedForms
            item.Close()
        Next

        t.Stop()

    End Sub

End Class


