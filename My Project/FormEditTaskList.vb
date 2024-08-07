Option Strict On

Public Class FormEditTaskList

    Public Property ManuallySelectFileTypes As Boolean
    Public Property TaskList As List(Of Task)
    Public Property AvailableTasks As List(Of Task)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.TaskList = New List(Of Task)
        For Each Task As Task In Form1.TaskList
            If Task.ColorHue = "" Then Task.ColorHue = "Green"
            If Task.ColorSaturation = 0 Then Task.ColorSaturation = 0.2
            If Task.ColorBrightness = 0 Then Task.ColorBrightness = 1
            If Task.ColorR = 0 Then Task.ColorR = 240
            If Task.ColorG = 0 Then Task.ColorG = 255
            If Task.ColorB = 0 Then Task.ColorB = 240

            Me.TaskList.Add(Task)
        Next

        Dim UP As New UtilsPreferences()
        Me.AvailableTasks = New List(Of Task)
        Me.AvailableTasks = UP.BuildTaskListFromScratch()


    End Sub


    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click

        Dim DescriptionList As New List(Of String)
        Dim RepeatsList As New List(Of String)

        For Each Task As Task In Me.TaskList
            DescriptionList.Add(Task.Description)
        Next

        DescriptionList.Sort()

        Dim s As String
        Dim s2 As String

        For i = 1 To DescriptionList.Count - 1
            s = DescriptionList(i)
            s2 = DescriptionList(i - 1)
            If s = s2 Then
                If Not RepeatsList.Contains(s) Then
                    RepeatsList.Add(s)
                End If
            End If
        Next

        If RepeatsList.Count = 0 Then
            Me.DialogResult = DialogResult.OK
        Else
            s = String.Format("Task names must be unique{0}", vbCrLf)
            s = String.Format("{0}Please rename the following:{1}", s, vbCrLf)
            For Each s2 In RepeatsList
                s = String.Format("{0}'{1}'{2}", s, s2, vbCrLf)
            Next
            MsgBox(s, vbOKOnly)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub


    Private Sub FormEditTaskList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BuildColumns()

        Dim i As Integer = 0

        For Each SourceTask As Task In Me.AvailableTasks
            DataGridViewSource.Rows.Add({SourceTask.Description})
            DataGridViewSource.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(SourceTask.ColorR, SourceTask.ColorG, SourceTask.ColorB)
            i += 1
        Next

        DataGridViewSource.Columns.Item(0).Width = DataGridViewSource.Width - 20
        'DataGridViewTarget.Columns.Item(0).Width = DataGridViewTarget.Width - 20

        UpdateDataGridView()

    End Sub

    Private Sub FormEditTaskList_Resize(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        DataGridViewSource.Columns.Item(0).Width = DataGridViewSource.Width - 20
        DataGridViewTarget.Columns.Item(0).Width = DataGridViewTarget.Width - 20

    End Sub

    Private Sub BuildColumns()

        Dim ColumnNames As List(Of String) = "Task".Split(CChar(" ")).ToList
        Dim ColumnName As String
        Dim ColumnTypes As List(Of String) = "TextBox".Split(CChar(" ")).ToList
        Dim ColumnType As String

        For i = 0 To ColumnNames.Count - 1
            ColumnType = ColumnTypes(i)
            ColumnName = ColumnNames(i)

            Select Case ColumnType

                Case "TextBox"
                    Dim Col As New DataGridViewTextBoxColumn
                    Col.Name = ColumnName
                    Col.HeaderText = "AVAILABLE TASKS"

                    DataGridViewSource.Columns.Add(Col)


                    Dim Col2 As New DataGridViewTextBoxColumn
                    Col2.Name = ColumnName
                    Col2.HeaderText = "TASK LIST"

                    DataGridViewTarget.Columns.Add(Col2)

                Case Else
                    MsgBox(String.Format("Column type '{0}' not recognized", ColumnType), vbOKOnly)
            End Select
        Next

    End Sub

    Private Sub UpdateDataGridView()
        Dim i As Integer

        DataGridViewTarget.Rows.Clear()

        i = 0

        For Each Task As Task In Me.TaskList
            DataGridViewTarget.Rows.Add({Task.Description})
            DataGridViewTarget.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(Task.ColorR, Task.ColorG, Task.ColorB)
            i += 1
        Next

        DataGridViewTarget.Columns.Item(0).Width = DataGridViewTarget.Width - 20

    End Sub

    Private Sub UpdateDataGridView(SelectedRowIndex As Integer)

        UpdateDataGridView()

        DataGridViewTarget.CurrentCell = DataGridViewTarget.Rows(SelectedRowIndex).Cells(0)
        DataGridViewTarget.Rows(SelectedRowIndex).Selected = True
        If (SelectedRowIndex >= 0) And (SelectedRowIndex < DataGridViewTarget.RowCount - 1) Then
        End If

    End Sub

    'Private Sub DataGridViewTarget_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewTarget.CellContentClick

    'End Sub

    Private Sub DataGridViewTarget_CurrentCellDirtyStateChanged(
        ByVal sender As Object,
        ByVal e As EventArgs
        ) Handles DataGridViewTarget.CurrentCellDirtyStateChanged

        If DataGridViewTarget.IsCurrentCellDirty Then
            DataGridViewTarget.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub DataGridViewTarget_CellValueChanged(
        ByVal sender As Object,
        ByVal e As DataGridViewCellEventArgs
        ) Handles DataGridViewTarget.CellValueChanged

        Select Case e.ColumnIndex
            Case 0 ' Description
                Dim TextBox As DataGridViewTextBoxCell = CType(DataGridViewTarget.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewTextBoxCell)
                Me.TaskList(e.RowIndex).Description = CStr(TextBox.Value)

                DataGridViewTarget.Invalidate()

            Case Else
                MsgBox(String.Format("Column '{0}' not processed", e.ColumnIndex))

        End Select
    End Sub

    Private Sub MoveRow(Direction As String)
        Dim SelectedRowIndex As Integer
        Dim tmpTaskList As New List(Of Task)
        Dim tf As Boolean

        If Not DataGridViewTarget.SelectedRows.Count = 1 Then
            MsgBox("Select a single row to move", vbOKOnly)
        Else

            SelectedRowIndex = DataGridViewTarget.SelectedRows(0).Index

            'Can't move up from the top
            tf = (SelectedRowIndex = 0) And (Direction = "up")

            'Can't move down from the bottom
            tf = tf Or ((SelectedRowIndex = DataGridViewTarget.Rows.Count - 1) And (Direction = "down"))

            If Not tf Then

                'SelectedRowIndex = DataGridViewTarget.SelectedRows(0).Index

                For i As Integer = 0 To Me.TaskList.Count - 1

                    Select Case Direction

                        Case "up"
                            If i = SelectedRowIndex - 1 Then
                                tmpTaskList.Add(Me.TaskList(i + 1))
                            ElseIf i = SelectedRowIndex Then
                                tmpTaskList.Add(Me.TaskList(i - 1))
                            Else
                                tmpTaskList.Add(Me.TaskList(i))
                            End If

                        Case "down"
                            If i = SelectedRowIndex Then
                                tmpTaskList.Add(Me.TaskList(i + 1))
                            ElseIf i = SelectedRowIndex + 1 Then
                                tmpTaskList.Add(Me.TaskList(i - 1))
                            Else
                                tmpTaskList.Add(Me.TaskList(i))
                            End If

                        Case Else
                            MsgBox(String.Format("Direction '{0}' not recognized", Direction))
                    End Select

                Next

                Me.TaskList = tmpTaskList

                If Direction = "up" Then
                    UpdateDataGridView(SelectedRowIndex - 1)
                ElseIf Direction = "down" Then
                    UpdateDataGridView(SelectedRowIndex + 1)
                End If
            End If

        End If

    End Sub

    Private Sub ButtonMoveUp_Click(sender As Object, e As EventArgs) Handles ButtonMoveUp.Click
        MoveRow("up")
    End Sub

    Private Sub ButtonMoveDown_Click(sender As Object, e As EventArgs) Handles ButtonMoveDown.Click
        MoveRow("down")
    End Sub

    Private Sub CustomizeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomizeToolStripMenuItem.Click
        Dim ChangeColor As New FormEditTaskListChangeColor
        Dim s As String = ""
        Dim i As Integer

        If DataGridViewTarget.SelectedRows.Count = 0 Then
            s = String.Format("{0}{1}{2}", s, "No rows are selected.  Click in the column to the left ", vbCrLf)
            s = String.Format("{0}{1}{2}", s, "of the task description to select a row.", vbCrLf)
            MsgBox(s)
            Exit Sub
        End If

        i = DataGridViewTarget.SelectedRows(0).Index
        ChangeColor.ColorHue = Me.TaskList(i).ColorHue
        ChangeColor.ColorSaturation = Me.TaskList(i).ColorSaturation
        ChangeColor.ColorBrightness = Me.TaskList(i).ColorBrightness

        ChangeColor.ShowDialog()

        If ChangeColor.DialogResult = DialogResult.OK Then
            For Each SelectedRow As DataGridViewRow In DataGridViewTarget.SelectedRows
                i = SelectedRow.Index
                Me.TaskList(i).ColorHue = ChangeColor.ColorHue
                Me.TaskList(i).ColorSaturation = ChangeColor.ColorSaturation
                Me.TaskList(i).ColorBrightness = ChangeColor.ColorBrightness
                Me.TaskList(i).ColorR = ChangeColor.ColorR
                Me.TaskList(i).ColorG = ChangeColor.ColorG
                Me.TaskList(i).ColorB = ChangeColor.ColorB
                Me.TaskList(i).ResetTaskColor()
            Next
            UpdateDataGridView()
            'MsgBox("OK")
        End If
    End Sub

    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        Dim SelectedRowIndices As New List(Of Integer)
        Dim s As String = ""
        Dim tmpTaskList As New List(Of Task)
        Dim i As Integer

        If DataGridViewTarget.SelectedRows.Count = 0 Then
            s = String.Format("{0}{1}{2}", s, "No rows are selected.  Click in the column to the left ", vbCrLf)
            s = String.Format("{0}{1}{2}", s, "of the task description to select a row.", vbCrLf)
            MsgBox(s)
            Exit Sub
        End If

        For Each SelectedRow As DataGridViewRow In DataGridViewTarget.SelectedRows
            SelectedRowIndices.Add(SelectedRow.Index)
        Next

        i = 0
        For Each Task As Task In Me.TaskList
            If Not SelectedRowIndices.Contains(i) Then
                tmpTaskList.Add(Task)
            End If
            i += 1
        Next

        Me.TaskList = tmpTaskList

        UpdateDataGridView()

    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        Dim SelectedRowIndices As New List(Of Integer)
        Dim s As String = ""
        Dim tmpTaskList As New List(Of Task)
        Dim i As Integer
        Dim TaskDescription As String

        Dim UP As New UtilsPreferences

        If DataGridViewSource.SelectedRows.Count = 0 Then
            s = String.Format("{0}{1}{2}", s, "No rows are selected.  Click in the column to the left ", vbCrLf)
            s = String.Format("{0}{1}{2}", s, "of the task description to select a row.", vbCrLf)
            MsgBox(s)
            Exit Sub
        End If

        For Each SelectedRow As DataGridViewRow In DataGridViewSource.SelectedRows
            SelectedRowIndices.Add(SelectedRow.Index)
        Next

        i = 0
        For Each Task As Task In Me.TaskList
            tmpTaskList.Add(Task)
            i += 1
        Next

        i = 0
        For Each Task As Task In Me.AvailableTasks
            If SelectedRowIndices.Contains(i) Then
                TaskDescription = GetUniqueTaskDescription(tmpTaskList, Task.Description)
                tmpTaskList.Add(UP.GetNewTaskInstance(AvailableTasks, Task.Name, TaskDescription))
            End If
            i += 1
        Next

        Me.TaskList = tmpTaskList

        UpdateDataGridView()

    End Sub

    Private Function GetUniqueTaskDescription(
        tmpTaskList As List(Of Task),
        ProposedDescription As String
        ) As String

        Dim DescriptionList As New List(Of String)
        Dim Description As String = ProposedDescription
        Dim i As Integer

        For Each Task As Task In tmpTaskList
            DescriptionList.Add(Task.Description)
        Next

        i = 2
        While DescriptionList.Contains(Description)
            Description = String.Format("{0} [{1}]", ProposedDescription, CStr(i))
            i += 1
        End While

        Return Description
    End Function
End Class