Option Strict On

Public Class FormEditTaskList

    Public Property ManuallySelectFileTypes As Boolean
    Public Property TaskList As List(Of Task)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.TaskList = New List(Of Task)
        For Each Task As Task In Form1.TaskList
            If Task.ColorHue = "" Then Task.ColorHue = "Green"
            If Task.ColorSaturation = 0 Then Task.ColorSaturation = 0.2
            If Task.ColorBrightness = 0 Then Task.ColorBrightness = 1

            Me.TaskList.Add(Task)
        Next
    End Sub



    Private Sub CheckBoxAutoSelectFiletypes_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxManuallySelectFiletypes.CheckedChanged
        Dim CheckBox = CType(sender, CheckBox)
        Me.ManuallySelectFileTypes = CheckBox.Checked
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub FormEditTaskList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CheckBoxManuallySelectFiletypes.Checked = Me.ManuallySelectFileTypes

        BuildColumns()

        UpdateDataGridView()

    End Sub

    Private Sub BuildColumns()

        Dim ColumnNames As List(Of String) = "Task Tint Saturation Brightness".Split(CChar(" ")).ToList
        Dim ColumnName As String
        Dim ColumnTypes As List(Of String) = "TextBox ComboBox TextBox TextBox".Split(CChar(" ")).ToList
        Dim ColumnType As String

        For i = 0 To ColumnNames.Count - 1
            ColumnType = ColumnTypes(i)
            ColumnName = ColumnNames(i)

            Select Case ColumnType

                Case "TextBox"
                    Dim Col As New DataGridViewTextBoxColumn
                    Col.Name = ColumnName
                    Col.HeaderText = ColumnName

                    DataGridView1.Columns.Add(Col)

                Case "ComboBox"
                    Dim Col As New DataGridViewComboBoxColumn
                    Col.Name = ColumnName
                    Col.HeaderText = ColumnName

                    Col.Items.Add("Red")
                    Col.Items.Add("Green")
                    Col.Items.Add("Blue")
                    Col.Items.Add("Cyan")
                    Col.Items.Add("Magenta")
                    Col.Items.Add("Yellow")
                    Col.Items.Add("White")

                    DataGridView1.Columns.Add(Col)

                Case Else
                    MsgBox(String.Format("Column type '{0}' not recognized", ColumnType), vbOKOnly)
            End Select
        Next

    End Sub

    Private Sub UpdateDataGridView()

        DataGridView1.Rows.Clear()

        For Each Task As Task In Me.TaskList
            DataGridView1.Rows.Add({Task.Description, Task.ColorHue, Task.ColorSaturation, Task.ColorBrightness})
        Next

        DataGridView1.AutoResizeColumns()

    End Sub

    Private Sub UpdateDataGridView(SelectedRowIndex As Integer)
        UpdateDataGridView()

        DataGridView1.CurrentCell = DataGridView1.Rows(SelectedRowIndex).Cells(0)
        DataGridView1.Rows(SelectedRowIndex).Selected = True

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    'Private Sub DataGridView1_EditingControlShowing(
    '    sender As Object,
    '    e As DataGridViewEditingControlShowingEventArgs
    '    ) Handles DataGridView1.EditingControlShowing

    '    MsgBox("something")
    'End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(
        ByVal sender As Object,
        ByVal e As EventArgs
        ) Handles DataGridView1.CurrentCellDirtyStateChanged

        If DataGridView1.IsCurrentCellDirty Then
            DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(
        ByVal sender As Object,
        ByVal e As DataGridViewCellEventArgs
        ) Handles DataGridView1.CellValueChanged

        Select Case e.ColumnIndex
            Case 0 ' Description
                Dim TextBox As DataGridViewTextBoxCell = CType(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewTextBoxCell)
                Me.TaskList(e.RowIndex).Description = CStr(TextBox.Value)

                DataGridView1.Invalidate()

            Case 1 ' Tint
                Dim ComboBox As DataGridViewComboBoxCell = CType(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewComboBoxCell)

                If ComboBox.Value IsNot Nothing Then
                    Me.TaskList(e.RowIndex).ColorHue = CStr(ComboBox.Value)

                    DataGridView1.Invalidate()
                End If

            Case 2 ' Saturation
                Dim TextBox As DataGridViewTextBoxCell = CType(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewTextBoxCell)
                Try
                    Me.TaskList(e.RowIndex).ColorSaturation = CDbl(TextBox.Value)
                Catch ex As Exception
                End Try

                DataGridView1.Invalidate()

            Case 3 ' Brightness
                Dim TextBox As DataGridViewTextBoxCell = CType(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewTextBoxCell)
                Try
                    Me.TaskList(e.RowIndex).ColorBrightness = CDbl(TextBox.Value)
                Catch ex As Exception
                End Try

                DataGridView1.Invalidate()

            Case Else
                MsgBox(String.Format("Column '{0}' not processed", e.ColumnIndex))

        End Select
    End Sub

    Private Sub MoveRow(Direction As String)
        Dim SelectedRowIndex As Integer
        Dim tmpTaskList As New List(Of Task)
        Dim tf As Boolean

        If Not DataGridView1.SelectedRows.Count = 1 Then
            MsgBox("Select a single row to move", vbOKOnly)
        Else

            SelectedRowIndex = DataGridView1.SelectedRows(0).Index

            'Can't move up from the top
            tf = (SelectedRowIndex = 0) And (Direction = "up")

            'Can't move down from the bottom
            tf = tf Or ((SelectedRowIndex = DataGridView1.Rows.Count - 1) And (Direction = "down"))

            If Not tf Then

                SelectedRowIndex = DataGridView1.SelectedRows(0).Index

                For i As Integer = 0 To Form1.TaskList.Count - 1

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonMoveUp.Click
        MoveRow("up")
    End Sub

    Private Sub ButtonMoveDown_Click(sender As Object, e As EventArgs) Handles ButtonMoveDown.Click
        MoveRow("down")
    End Sub
End Class