'Imports Housekeeper.Task

Imports Housekeeper.Task

Public Class Task_EventHandler
    Public Property Task As Task = Form1.TaskList(0)

    'Public Sub New(Task As Task)
    '    Me.Task = Task
    'End Sub

    Shared Sub Button_Click(sender As System.Object, e As System.EventArgs)

        Dim Button = CType(sender, Button)
        Dim L As New List(Of String)

        Dim Parent As ExTableLayoutPanel = CType(Button.Parent, ExTableLayoutPanel)
        Dim Task = Parent.Task

        ' If a Header or EditTaskList button is pressed, Task will be Nothing
        If Task IsNot Nothing Then
            Select Case Button.Name
                Case Task.BaseControlNames.Expand.ToString
                    If Task.TLPOptions.Visible Then
                        Task.TLPOptions.Visible = False
                        Button.Image = My.Resources.expand
                    Else
                        Task.TLPOptions.Visible = True
                        Button.Image = My.Resources.collapse
                    End If

                Case Task.BaseControlNames.Help.ToString
                    System.Diagnostics.Process.Start(Task.HelpURL)

                Case Else
                    MsgBox(String.Format("{0} No handler for Button '{1}'", "Task_EventHandler", Button.Name))

            End Select

        Else  ' Header button
            Select Case Button.Name

                Case Task.BaseControlNames.Expand.ToString
                    ' Expand or collapse all.  If any are not hidden, collapse all.  Otherwise show all.

                    Dim OneShown As Boolean = False

                    For Each Task In Form1.TaskList
                        If Task.HasOptions Then
                            If Task.TLPOptions.Visible Then
                                OneShown = True
                                Exit For
                            End If
                        End If
                    Next

                    'Form1.SuspendLayout()

                    For Each Task In Form1.TaskList
                        If Task.HasOptions Then

                            ' Task properties and controls
                            Dim TaskButton = CType(Task.ControlsDict(Task.BaseControlNames.Expand.ToString), Button)
                            Task.IsOptionsHidden = Not OneShown
                            Task.TLPOptions.Visible = Not OneShown
                            If OneShown Then
                                TaskButton.Image = My.Resources.expand
                            Else
                                TaskButton.Image = My.Resources.collapse
                            End If

                            ' Header button image
                            Dim HeaderButton = CType(Task.BaseControlsDict(Task.BaseControlNames.Expand.ToString), Button)
                            If OneShown Then
                                HeaderButton.Image = My.Resources.expand
                            Else
                                HeaderButton.Image = My.Resources.collapse
                            End If
                        End If
                    Next

                    'Form1.ResumeLayout()

                Case Task.BaseControlNames.SelectTask.ToString
                    ' Deselect all
                    For Each Task In Form1.TaskList
                        'Task.IsSelectedTask = False  ' Let the checkbox event handler do this
                        Dim CheckBox2 = CType(Task.ControlsDict(Task.BaseControlNames.SelectTask.ToString), CheckBox)
                        CheckBox2.Checked = False
                    Next

                Case Task.BaseControlNames.SelectAssembly.ToString

                    ' Select or deselect checkboxes for selected tasks.
                    ' If all are already selected, deselect all, otherwise select all.

                    Dim AllAlreadySelected As Boolean = True
                    Dim tf As Boolean
                    Dim CheckBox2 As CheckBox

                    For Each Task In Form1.TaskList
                        CheckBox2 = CType(Task.ControlsDict(Task.BaseControlNames.SelectAssembly.ToString), CheckBox)
                        tf = (Task.IsSelectedTask) And (Not Task.IsSelectedAssembly)
                        If (Task.IsSelectedTask) And (Not Task.IsSelectedAssembly) And (CheckBox2.Enabled) Then
                            AllAlreadySelected = False
                            Exit For
                        End If
                    Next

                    For Each Task In Form1.TaskList
                        CheckBox2 = CType(Task.ControlsDict(Task.BaseControlNames.SelectAssembly.ToString), CheckBox)
                        If AllAlreadySelected Then
                            CheckBox2.Checked = False ' Let the checkbox event handler fix the properties and controls.
                        Else
                            If Task.IsSelectedTask And CheckBox2.Enabled Then
                                CheckBox2.Checked = True
                            End If
                        End If
                        'DoDeselectAssembly(Task, AllAlreadySelected)
                    Next

                Case Task.BaseControlNames.SelectPart.ToString

                    ' Select or deselect checkboxes for selected tasks.
                    ' If all are already selected, deselect all, otherwise select all.

                    Dim AllAlreadySelected As Boolean = True
                    Dim tf As Boolean
                    Dim CheckBox2 As CheckBox

                    For Each Task In Form1.TaskList
                        CheckBox2 = CType(Task.ControlsDict(Task.BaseControlNames.SelectPart.ToString), CheckBox)
                        tf = (Task.IsSelectedTask) And (Not Task.IsSelectedPart)
                        If (Task.IsSelectedTask) And (Not Task.IsSelectedPart) And (CheckBox2.Enabled) Then
                            AllAlreadySelected = False
                            Exit For
                        End If
                    Next

                    For Each Task In Form1.TaskList
                        CheckBox2 = CType(Task.ControlsDict(Task.BaseControlNames.SelectPart.ToString), CheckBox)
                        If AllAlreadySelected Then
                            CheckBox2.Checked = False ' Let the checkbox event handler fix the properties and controls.
                        Else
                            If Task.IsSelectedTask And CheckBox2.Enabled Then
                                CheckBox2.Checked = True
                            End If
                        End If
                        'DoDeselectPart(Task, AllAlreadySelected)
                    Next

                Case Task.BaseControlNames.SelectSheetmetal.ToString

                    ' Select or deselect checkboxes for selected tasks.
                    ' If all are already selected, deselect all, otherwise select all.

                    Dim AllAlreadySelected As Boolean = True
                    Dim tf As Boolean
                    Dim CheckBox2 As CheckBox

                    For Each Task In Form1.TaskList
                        CheckBox2 = CType(Task.ControlsDict(Task.BaseControlNames.SelectSheetmetal.ToString), CheckBox)
                        tf = (Task.IsSelectedTask) And (Not Task.IsSelectedSheetmetal)
                        If (Task.IsSelectedTask) And (Not Task.IsSelectedSheetmetal) And (CheckBox2.Enabled) Then
                            AllAlreadySelected = False
                            Exit For
                        End If
                    Next

                    For Each Task In Form1.TaskList
                        CheckBox2 = CType(Task.ControlsDict(Task.BaseControlNames.SelectSheetmetal.ToString), CheckBox)
                        If AllAlreadySelected Then
                            CheckBox2.Checked = False ' Let the checkbox event handler fix the properties and controls.
                        Else
                            If Task.IsSelectedTask And CheckBox2.Enabled Then
                                CheckBox2.Checked = True
                            End If
                        End If
                        'DoDeselectSheetmetal(Task, AllAlreadySelected)
                    Next

                Case Task.BaseControlNames.SelectDraft.ToString

                    ' Select or deselect checkboxes for selected tasks.
                    ' If all are already selected, deselect all, otherwise select all.

                    Dim AllAlreadySelected As Boolean = True
                    Dim tf As Boolean
                    Dim CheckBox2 As CheckBox

                    For Each Task In Form1.TaskList
                        CheckBox2 = CType(Task.ControlsDict(Task.BaseControlNames.SelectDraft.ToString), CheckBox)
                        tf = (Task.IsSelectedTask) And (Not Task.IsSelectedDraft)
                        If (Task.IsSelectedTask) And (Not Task.IsSelectedDraft) And (CheckBox2.Enabled) Then
                            AllAlreadySelected = False
                            Exit For
                        End If
                    Next

                    For Each Task In Form1.TaskList
                        CheckBox2 = CType(Task.ControlsDict(Task.BaseControlNames.SelectDraft.ToString), CheckBox)
                        If AllAlreadySelected Then
                            CheckBox2.Checked = False ' Let the checkbox event handler fix the properties and controls.
                        Else
                            If Task.IsSelectedTask And CheckBox2.Enabled Then
                                CheckBox2.Checked = True
                            End If
                        End If
                        'DoDeselectDraft(Task, AllAlreadySelected)
                    Next

                Case Task.BaseControlNames.Help.ToString
                    ' Go to the top-level readme page
                    Dim tmpHelpURL = "https://github.com/rmcanany/SolidEdgeHousekeeper#readme"
                    System.Diagnostics.Process.Start(tmpHelpURL)

                Case Task.BaseControlNames.EditTaskList.ToString
                    'Dim EditTaskList As New FormEditTaskList
                    'EditTaskList.ManuallySelectFileTypes = Form1.TaskList(0).ManuallySelectFileTypes
                    'EditTaskList.ShowDialog()

                    'If EditTaskList.DialogResult = DialogResult.OK Then
                    '    For Each tmpTask As Task In Form1.TaskList
                    '        tmpTask.ManuallySelectFileTypes = EditTaskList.ManuallySelectFileTypes
                    '    Next
                    'End If

                    MsgBox("Not implemented", vbOKOnly)

                Case Else
                    MsgBox(String.Format("{0} No handler for Button '{1}'", "Task_EventHandler", Button.Name))

            End Select

        End If

    End Sub

    Shared Sub CheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs)
        Dim CheckBox = CType(sender, CheckBox)
        Dim Name = CheckBox.Name

        Dim Parent As ExTableLayoutPanel = CType(CheckBox.Parent, ExTableLayoutPanel)
        Dim Task = Parent.Task

        Dim CheckBox2 As CheckBox
        Dim Button2 As Button
        Dim CheckBox2Image As Bitmap
        Dim Button2Image As Bitmap
        Dim CheckBox2Checked As Boolean


        If CheckBox.Checked Then
            CheckBox.Image = My.Resources.Checked
            If Task.ManuallySelectFileTypes Then
                CheckBox2Image = My.Resources.Unchecked
                CheckBox2Checked = False
            Else
                CheckBox2Image = My.Resources.Checked
                CheckBox2Checked = True
            End If
            Button2Image = My.Resources.collapse
        Else
            CheckBox.Image = My.Resources.Unchecked
            CheckBox2Image = My.Resources.unchecked_disabled
            Button2Image = My.Resources.expand
        End If

        Select Case CheckBox.Name

            Case BaseControlNames.SelectTask.ToString
                Task.IsSelectedTask = CheckBox.Checked
                If (Task.HasOptions) And (Not Task.AutoHideOptions) Then
                    Task.TLPOptions.Visible = CheckBox.Checked
                    Button2 = CType(Task.ControlsDict(BaseControlNames.Expand.ToString), Button)
                    Button2.Image = Button2Image
                End If
                If Task.AppliesToAssembly Then
                    CheckBox2 = CType(Task.ControlsDict(BaseControlNames.SelectAssembly.ToString), CheckBox)
                    If CheckBox2.Enabled Then
                        CheckBox2.Checked = CheckBox2Checked
                        CheckBox2.Image = CheckBox2Image
                    End If
                End If
                If Task.AppliesToPart Then
                    CheckBox2 = CType(Task.ControlsDict(BaseControlNames.SelectPart.ToString), CheckBox)
                    If CheckBox2.Enabled Then
                        CheckBox2.Checked = CheckBox2Checked
                        CheckBox2.Image = CheckBox2Image
                    End If
                End If
                If Task.AppliesToSheetmetal Then
                    CheckBox2 = CType(Task.ControlsDict(BaseControlNames.SelectSheetmetal.ToString), CheckBox)
                    CheckBox2.Checked = CheckBox2Checked
                    CheckBox2.Image = CheckBox2Image
                End If
                If Task.AppliesToDraft Then
                    CheckBox2 = CType(Task.ControlsDict(BaseControlNames.SelectDraft.ToString), CheckBox)
                    If CheckBox2.Enabled Then
                        CheckBox2.Checked = CheckBox2Checked
                        CheckBox2.Image = CheckBox2Image
                    End If
                End If

            Case BaseControlNames.SelectAssembly.ToString
                Task.IsSelectedAssembly = CheckBox.Checked

            Case BaseControlNames.SelectPart.ToString
                Task.IsSelectedPart = CheckBox.Checked

            Case BaseControlNames.SelectSheetmetal.ToString
                Task.IsSelectedSheetmetal = CheckBox.Checked

            Case BaseControlNames.SelectDraft.ToString
                Task.IsSelectedDraft = CheckBox.Checked

            Case Else
                MsgBox(String.Format("{0} No handler for CheckBox '{1}'", "Task_EventHandler", CheckBox.Name))

        End Select

    End Sub

    Shared Sub TextBox_GotFocus(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        TextBox.BeginInvoke(New Action(AddressOf TextBox.SelectAll))
    End Sub

    Shared Sub TLPHeader_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) 'Handles TLPHeader.CellPaint
        ' https://stackoverflow.com/questions/34064499/how-to-set-cell-color-in-tablelayoutpanel-dynamically

        Dim BrushColorFileType As Brush = New SolidBrush(Color.FromArgb(32 + 16, 190, 190, 255))
        Dim BrushColorHeader As Brush = New SolidBrush(Color.FromArgb(127, 190, 190, 255))

        If e.Row = 0 Then
            e.Graphics.FillRectangle(BrushColorHeader, e.CellBounds)
        End If

    End Sub

    Shared Sub TLPTask_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) 'Handles TLPHeader.CellPaint
        ' https://stackoverflow.com/questions/34064499/how-to-set-cell-color-in-tablelayoutpanel-dynamically

        Dim BrushColor As Brush = New SolidBrush(Color.FromArgb(48, 190, 190, 255))

        If e.Column > 1 And e.Column < 6 Then
            e.Graphics.FillRectangle(BrushColor, e.CellBounds)
        End If

    End Sub

End Class
