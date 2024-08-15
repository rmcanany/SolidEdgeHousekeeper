Option Strict On

Public Class FormPropertyListCustomize

    Public Property TemplatePropertyList As List(Of String)
    Public Property AvailableProperties As List(Of String)


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.TemplatePropertyList = New List(Of String)
        For Each Propname As String In Form_Main.TemplatePropertyList
            Me.TemplatePropertyList.Add(Propname)
        Next

        Me.AvailableProperties = New List(Of String)
        If Not Form_Main.TemplatePropertyDict Is Nothing Then
            For Each Propname As String In Form_Main.TemplatePropertyDict.Keys
                Me.AvailableProperties.Add(Propname)
            Next
        End If

    End Sub


    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click

        Dim DescriptionList As New List(Of String)
        Dim RepeatsList As New List(Of String)

        For Each Propname As String In Me.TemplatePropertyList
            DescriptionList.Add(Propname)
        Next

        DescriptionList.Sort()

        Dim s As String
        Dim s2 As String

        For i = 1 To DescriptionList.Count - 1
            s = DescriptionList(i)
            s2 = DescriptionList(i - 1)
            If s.ToLower = s2.ToLower Then
                If Not RepeatsList.Contains(s) Then
                    RepeatsList.Add(s)
                End If
            End If
        Next

        If RepeatsList.Count = 0 Then
            Me.DialogResult = DialogResult.OK

        Else
            s = String.Format("Property names must be unique{0}", vbCrLf)
            s = String.Format("{0}Please remove duplicates of the following:{1}", s, vbCrLf)
            For Each s2 In RepeatsList
                s = String.Format("{0}'{1}'{2}", s, s2, vbCrLf)
            Next
            MsgBox(s, vbOKOnly)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub


    Private Sub FormPropertyListCustomize_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BuildColumns()

        Dim i As Integer = 0

        For Each Propname As String In Me.AvailableProperties
            DataGridViewSource.Rows.Add({Propname})
            i += 1
        Next

        DataGridViewSource.Columns.Item(0).Width = DataGridViewSource.Width - 20

        UpdateDataGridViewTarget()

    End Sub

    Private Sub FormPropertyListCustomize_Resize(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
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
                    Col.HeaderText = "AVAILABLE PROPERTIES"

                    DataGridViewSource.Columns.Add(Col)


                    Dim Col2 As New DataGridViewTextBoxColumn
                    Col2.Name = ColumnName
                    Col2.HeaderText = "SELECTED PROPERTIES"

                    DataGridViewTarget.Columns.Add(Col2)

                Case Else
                    MsgBox(String.Format("Column type '{0}' not recognized", ColumnType), vbOKOnly)
            End Select
        Next

    End Sub

    Private Sub UpdateDataGridViewTargetSource()
        Dim i As Integer

        DataGridViewSource.Rows.Clear()

        i = 0

        For Each Propname As String In Me.AvailableProperties
            DataGridViewSource.Rows.Add({Propname})
            i += 1
        Next

        DataGridViewSource.Columns.Item(0).Width = DataGridViewSource.Width - 20

    End Sub

    Private Sub UpdateDataGridViewTarget()
        Dim i As Integer

        DataGridViewTarget.Rows.Clear()

        i = 0

        For Each Propname As String In Me.TemplatePropertyList
            DataGridViewTarget.Rows.Add({Propname})
            i += 1
        Next

        DataGridViewTarget.Columns.Item(0).Width = DataGridViewTarget.Width - 20

    End Sub

    Private Sub UpdateDataGridViewTarget(SelectedRowIndex As Integer)

        UpdateDataGridViewTarget()

        DataGridViewTarget.CurrentCell = DataGridViewTarget.Rows(SelectedRowIndex).Cells(0)
        DataGridViewTarget.Rows(SelectedRowIndex).Selected = True

    End Sub

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

                Me.TemplatePropertyList(e.RowIndex) = CStr(TextBox.Value)
                'Me.TaskList(e.RowIndex).Description = CStr(TextBox.Value)

                DataGridViewTarget.Invalidate()

            Case Else
                MsgBox(String.Format("Column '{0}' not processed", e.ColumnIndex))

        End Select
    End Sub

    Private Sub MoveRow(Direction As String)
        Dim SelectedRowIndex As Integer

        Dim tmpTemplatePropertyList As New List(Of String)

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

                For i As Integer = 0 To Me.TemplatePropertyList.Count - 1

                    Select Case Direction

                        Case "up"
                            If i = SelectedRowIndex - 1 Then
                                tmpTemplatePropertyList.Add(Me.TemplatePropertyList(i + 1))
                            ElseIf i = SelectedRowIndex Then
                                tmpTemplatePropertyList.Add(Me.TemplatePropertyList(i - 1))
                            Else
                                tmpTemplatePropertyList.Add(Me.TemplatePropertyList(i))
                            End If

                        Case "down"
                            If i = SelectedRowIndex Then
                                tmpTemplatePropertyList.Add(Me.TemplatePropertyList(i + 1))
                            ElseIf i = SelectedRowIndex + 1 Then
                                tmpTemplatePropertyList.Add(Me.TemplatePropertyList(i - 1))
                            Else
                                tmpTemplatePropertyList.Add(Me.TemplatePropertyList(i))
                            End If

                        Case Else
                            MsgBox(String.Format("Direction '{0}' not recognized", Direction))
                    End Select

                Next

                Me.TemplatePropertyList = tmpTemplatePropertyList

                If Direction = "up" Then
                    UpdateDataGridViewTarget(SelectedRowIndex - 1)
                ElseIf Direction = "down" Then
                    UpdateDataGridViewTarget(SelectedRowIndex + 1)
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
        Dim FPLCM As New FormPropertyListCustomizeManualEntry

        Dim Result = FPLCM.ShowDialog()

        If FPLCM.DialogResult = DialogResult.OK Then
            TemplatePropertyList.Add(FPLCM.Propname)
            UpdateDataGridViewTarget()
        End If

    End Sub

    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        Dim SelectedRowIndices As New List(Of Integer)
        Dim s As String = ""

        Dim tmpTemplatePropertyList As New List(Of String)
        'Dim tmpTaskList As New List(Of Task)

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
        For Each Propname As String In Me.TemplatePropertyList
            If Not SelectedRowIndices.Contains(i) Then
                tmpTemplatePropertyList.Add(Propname)
            End If
            i += 1
        Next

        Me.TemplatePropertyList = tmpTemplatePropertyList

        UpdateDataGridViewTarget()

    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        Dim SelectedRowIndices As New List(Of Integer)
        Dim s As String = ""

        Dim tmpTemplatePropertyList As New List(Of String)
        'Dim tmpTaskList As New List(Of Task)

        Dim i As Integer
        'Dim TaskDescription As String

        Dim PU As New UtilsPreferences

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
        For Each Propname As String In Me.TemplatePropertyList
            tmpTemplatePropertyList.Add(Propname)
            i += 1
        Next

        i = 0
        For Each Propname As String In Me.AvailableProperties
            If SelectedRowIndices.Contains(i) Then
                'TaskDescription = GetUniqueTaskDescription(tmpTaskList, Task.Description)
                tmpTemplatePropertyList.Add(Propname)
            End If
            i += 1
        Next

        Me.TemplatePropertyList = tmpTemplatePropertyList

        UpdateDataGridViewTarget()

    End Sub

    Private Function GetUniqueTaskDescription(
        tmpTaskList As List(Of Task),
        ProposedDescription As String
        ) As String

        Return ProposedDescription
        'Dim DescriptionList As New List(Of String)
        'Dim Description As String = ProposedDescription
        'Dim i As Integer

        'For Each Task As Task In tmpTaskList
        '    DescriptionList.Add(Task.Description)
        'Next

        'i = 2
        'While DescriptionList.Contains(Description)
        '    Description = String.Format("{0} [{1}]", ProposedDescription, CStr(i))
        '    i += 1
        'End While

        'Return Description
    End Function

    Private Sub CheckBoxSortSourceList_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSortSourceList.CheckedChanged

        If CheckBoxSortSourceList.Checked Then
            Me.AvailableProperties.Sort()
        Else
            Me.AvailableProperties.Clear()
            If Form_Main.TemplatePropertyDict IsNot Nothing Then
                For Each Propname As String In Form_Main.TemplatePropertyDict.Keys
                    Me.AvailableProperties.Add(Propname)
                Next
            End If
        End If

        UpdateDataGridViewTargetSource()

    End Sub
End Class