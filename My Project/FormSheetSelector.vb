Option Strict On

Public Class FormSheetSelector
	Public Property SelectedSheets As New List(Of String)
	Public Property Task As TaskPrint
	Dim SheetSizeList As New List(Of String)


	Public Sub New(Task As TaskPrint)

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.

		Me.Task = Task

	End Sub

	Public Sub ShowSheetSelector()
		Me.ShowDialog()
	End Sub


	Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
		Me.SelectedSheets.Clear()
		For Each Item In CheckedListBox1.CheckedItems
			Me.SelectedSheets.Add(Task.DisplayNameToConstantName(Item.ToString))
		Next

		Me.DialogResult = DialogResult.OK
	End Sub

	Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
		Me.DialogResult = DialogResult.Cancel
	End Sub

	Private Sub RadioButtonAnsi_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonAnsi.CheckedChanged
		SheetSizeList = Task.GetSheetSizes("Ansi")

		CheckedListBox1.Items.Clear()
		Me.SelectedSheets.Clear()
		CheckBoxSelectAll.Checked = False

		For Each SheetSize As String In SheetSizeList
			CheckedListBox1.Items.Add(Task.ConstantNameToDisplayName(SheetSize))
		Next

	End Sub

	Private Sub RadioButtonIso_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonIso.CheckedChanged
		SheetSizeList = Task.GetSheetSizes("Iso")

		CheckedListBox1.Items.Clear()
		Me.SelectedSheets.Clear()
		CheckBoxSelectAll.Checked = False


		For Each SheetSize As String In SheetSizeList
			CheckedListBox1.Items.Add(Task.ConstantNameToDisplayName(SheetSize))
		Next

	End Sub

	Private Sub RadioButtonAll_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonAll.CheckedChanged
		SheetSizeList = Task.GetSheetSizes("All")

		CheckedListBox1.Items.Clear()
		Me.SelectedSheets.Clear()
		CheckBoxSelectAll.Checked = False

		For Each SheetSize As String In SheetSizeList
			CheckedListBox1.Items.Add(Task.ConstantNameToDisplayName(SheetSize))
		Next

	End Sub

	Private Sub CheckBoxSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSelectAll.CheckedChanged
		Dim CheckBox = CType(sender, CheckBox)

		For i As Integer = 0 To CheckedListBox1.Items.Count - 1
			CheckedListBox1.SetItemCheckState(i, CheckBox.CheckState)
		Next


	End Sub

End Class