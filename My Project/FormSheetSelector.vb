Public Class FormSheetSelector
	Dim SheetSizeDict As New Dictionary(Of String, SolidEdgeDraft.PaperSizeConstants)
	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.

	End Sub

	Public Sub ShowForm2(Filter As String)
		Dim PD As New PrinterDoctor
		' SheetSizeDict = New Dictionary(Of String, SolidEdgeDraft.PaperSizeConstants)
		SheetSizeDict = PD.GetSheetSizes(Filter)

		CheckedListBox1.Items.Clear()
		For Each SheetSize As String In SheetSizeDict.Keys
			CheckedListBox1.Items.Add(SheetSize)
		Next

		Me.ShowDialog()

	End Sub

	Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
		Form1.SelectedSheets.Clear()
		For Each Item In CheckedListBox1.CheckedItems
			Form1.SelectedSheets(Item.ToString) = SheetSizeDict(Item.ToString)
		Next

		Me.DialogResult = DialogResult.OK

	End Sub

	Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
		Me.DialogResult = DialogResult.Cancel
	End Sub
End Class