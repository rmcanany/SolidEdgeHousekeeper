Option Strict On

Public Class UCTaskControl

    Public Task As Task

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

        'Me.BackColor = Color.FromArgb(Task.ColorR, Task.ColorG, Task.ColorB)
        ' tableLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        'Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle

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
                If Not Me.Task.AutoHideOptions Then
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

End Class


