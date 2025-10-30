Option Strict On

Public Class FormSheetSelector
	Public Property SelectedSheets As New List(Of String)
	Public Property Task As TaskPrint

	Private _CustomUnits As String
	Public Property CustomUnits As String
		Get
			Return _CustomUnits
		End Get
		Set(value As String)
			_CustomUnits = value
			If ComboBoxCustomUnits IsNot Nothing Then
				If Not ComboBoxCustomUnits.Text = value Then ComboBoxCustomUnits.Text = value
			End If
		End Set
	End Property

	Private _CustomXMin As String
	Public Property CustomXMin As String
		Get
			Return _CustomXMin
		End Get
		Set(value As String)
			_CustomXMin = value
			If TextBoxCustomXMin IsNot Nothing Then
				If Not TextBoxCustomXMin.Text = value Then TextBoxCustomXMin.Text = value
			End If
		End Set
	End Property

	Private _CustomXMax As String
	Public Property CustomXMax As String
		Get
			Return _CustomXMax
		End Get
		Set(value As String)
			_CustomXMax = value
			If TextBoxCustomXMax IsNot Nothing Then
				If Not TextBoxCustomXMax.Text = value Then TextBoxCustomXMax.Text = value
			End If
		End Set
	End Property

	Private _CustomYMin As String
	Public Property CustomYMin As String
		Get
			Return _CustomYMin
		End Get
		Set(value As String)
			_CustomYMin = value
			If TextBoxCustomYMin IsNot Nothing Then
				If Not TextBoxCustomYMin.Text = value Then TextBoxCustomYMin.Text = value
			End If
		End Set
	End Property

	Private _CustomYMax As String
	Public Property CustomYMax As String
		Get
			Return _CustomYMax
		End Get
		Set(value As String)
			_CustomYMax = value
			If TextBoxCustomYMax IsNot Nothing Then
				If Not TextBoxCustomYMax.Text = value Then TextBoxCustomYMax.Text = value
			End If
		End Set
	End Property

	Dim SheetSizeList As New List(Of String)


	Public Sub New(Task As TaskPrint)

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.

		Me.Task = Task

	End Sub


	Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
		Me.SelectedSheets.Clear()
		For Each Item In CheckedListBox1.CheckedItems
			Me.SelectedSheets.Add(Task.DisplayNameToConstantName(Item.ToString))
		Next

		Dim Proceed As Boolean = True
		Dim D As Double
		Try
			D = CDbl(Me.CustomXMin)
			D = CDbl(Me.CustomXMax)
			D = CDbl(Me.CustomYMin)
			D = CDbl(Me.CustomYMax)
		Catch ex As Exception
			Proceed = False
			MsgBox("Could not convert one of the Custom sizes to a number.  Please check and try again.")
		End Try

		If Proceed Then
			Me.Task.SelectedSheetsList = Me.SelectedSheets
			Me.Task.CustomUnits = Me.CustomUnits
			Me.Task.CustomXMin = CDbl(Me.CustomXMin)
			Me.Task.CustomXMax = CDbl(Me.CustomXMax)
			Me.Task.CustomYMin = CDbl(Me.CustomYMin)
			Me.Task.CustomYMax = CDbl(Me.CustomYMax)

			Me.DialogResult = DialogResult.OK

		End If
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

	Private Sub ComboBoxCustomUnits_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCustomUnits.SelectedIndexChanged
		Me.CustomUnits = ComboBoxCustomUnits.Text
	End Sub

	Private Sub TextBoxCustomXMin_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCustomXMin.TextChanged
		Me.CustomXMin = TextBoxCustomXMin.Text
	End Sub

	Private Sub TextBoxCustomXMax_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCustomXMax.TextChanged
		Me.CustomXMax = TextBoxCustomXMax.Text
	End Sub

	Private Sub TextBoxCustomYMin_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCustomYMin.TextChanged
		Me.CustomYMin = TextBoxCustomYMin.Text
	End Sub

	Private Sub TextBoxCustomYMax_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCustomYMax.TextChanged
		Me.CustomYMax = TextBoxCustomYMax.Text
	End Sub

	Private Sub FormSheetSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Me.CustomUnits = Me.Task.CustomUnits
		Me.CustomXMin = CStr(Me.Task.CustomXMin)
		Me.CustomXMax = CStr(Me.Task.CustomXMax)
		Me.CustomYMin = CStr(Me.Task.CustomYMin)
		Me.CustomYMax = CStr(Me.Task.CustomYMax)
	End Sub

	Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged
		Dim IsCustomSelected As Boolean = False

		For Each Item In CheckedListBox1.CheckedItems
			Me.SelectedSheets.Add(Task.DisplayNameToConstantName(Item.ToString))
			If Item.ToString.ToLower.Contains("custom") Then IsCustomSelected = True
			Exit For
		Next

		ExTableLayoutPanel3.Visible = IsCustomSelected

	End Sub
End Class