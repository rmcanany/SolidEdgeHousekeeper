Option Strict On

Public Class FormPropertyFilter
	' https://www.homeandlearn.co.uk/NET/nets13p5.html
	' http://vb.net-informations.com/gui/vb.net-listview.htm

	Private FilterDict As New Dictionary(Of String, List(Of List(Of String)))

	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		GetSavedDict()
		InitializeListview()
		InitializeComboBoxes()
		InitializeReadme()
		'TextBoxReadme.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)

	End Sub

	Public Sub GetPropertyFilter()
		Me.ShowDialog()
	End Sub



	Private Sub GetSavedDict()

		Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
		Dim PropertyFilterFilename As String
		Dim PropertyFilter As String() = Nothing
		Dim Key As String
		Dim Value As New List(Of String)
		Dim Line As String
		Dim LineList As New List(Of String)

		PropertyFilterFilename = StartupPath + "\" + "property_filters.txt"

		Try
			PropertyFilter = IO.File.ReadAllLines(PropertyFilterFilename)
		Catch
		End Try

		If Not PropertyFilter Is Nothing Then
			For Each Line In PropertyFilter
				Value = Line.Split(CType(vbTab, Char())).ToList
				Key = Value(0)
				If Not Key = "" Then
					Value.RemoveAt(0)
					If Not FilterDict.ContainsKey(Key) Then
						FilterDict.Add(Key, New List(Of List(Of String)))
					End If
					FilterDict(Key).Add(Value)
				End If
			Next
		End If

		If Not FilterDict.Keys.Contains("None") Then
			Line = "None	A	System.Something	is_exactly	Nothing	A"
			Value = Line.Split(CType(vbTab, Char())).ToList
			Key = Value(0)
			Value.RemoveAt(0)
			FilterDict.Add(Key, New List(Of List(Of String)))
			FilterDict(Key).Add(Value)
		End If
	End Sub

	Private Sub SaveDict()
		' format
		' Tab-separated
		' Name	Variable	Property	Comparison	Value	Formula
		' Filter 1	A	System.Engineer	contains	Frank Hathaway	(A) AND (B)
		' Filter 1	B	Custom.Laser Vendor	is_exactly	Joe's burn, and, bend	(A) AND (B)
		' my search	A	Custom.Part Number	contains	000	A
		' little projects	A	System.Project	is_not	big_project	(A) OR (B)
		' little projects	B	System.Project	is_not	pet_project	(A) OR (B)

		Dim PropertyFiltersList As New List(Of String)
		Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
		Dim PropertyFilterFilename As String

		Dim Key As String
		Dim Filter As List(Of String)
		Dim FilterTerm As String
		Dim s As String

		PropertyFilterFilename = StartupPath + "\" + "property_filters.txt"

		For Each Key In FilterDict.Keys
			If Not Key = "" Then
				For Each Filter In FilterDict(Key)
					s = Key
					For Each FilterTerm In Filter
						s = String.Format("{0}{1}{2}", s, vbTab, FilterTerm)
					Next
					PropertyFiltersList.Add(s)
				Next
			End If
		Next

		IO.File.WriteAllLines(PropertyFilterFilename, PropertyFiltersList)
	End Sub

	Private Sub DictToListview(Key As String)
		Dim Filter As New List(Of String)
		Dim Formula As String
		Dim i As Integer
		Dim NumConditions As Integer
		Dim s As String

		' PrintDict("DictToListview")

		For i = 0 To ListViewPropertyFilter.Items.Count - 1
			ListViewPropertyFilter.Items(0).Remove()
		Next

		If FilterDict.ContainsKey(Key) Then
			NumConditions = FilterDict(Key).Count
			For i = 0 To NumConditions - 1

				' Make an independent copy of FilterDict(Key)
				For Each s In FilterDict(Key)(i)
					Filter.Add(s)
				Next

				Formula = Filter(4)
				Filter.RemoveAt(4)

				ListViewPropertyFilter.Items.Add(New ListViewItem(Filter.ToArray))
				TextBoxFormula.Text = Formula
				Filter.Clear()
			Next
		End If

		'PrintDict("DictToListview")
	End Sub

	Private Sub ListviewToDict(Key As String, Optional ResetFormula As Boolean = True)
		Dim Filterlist As New List(Of List(Of String))
		Dim VariableNames As New List(Of String)
		Dim Formula As String
		Dim NumConditions As Integer
		Dim i As Integer
		Dim j As Integer

		' PrintDict("ListviewToDict")

		VariableNames = "A B C D E F".Split(" "c).ToList

		If ResetFormula Then
			NumConditions = ListViewPropertyFilter.Items.Count

			Formula = VariableNames(0)
			For i = 1 To NumConditions - 1
				Formula = String.Format("{0} AND {1}", Formula, VariableNames(i))
			Next
		Else
			Formula = TextBoxFormula.Text
		End If


		For i = 0 To ListViewPropertyFilter.Items.Count - 1

			Dim Filter As New List(Of String)

			For j = 0 To ListViewPropertyFilter.Items(i).SubItems.Count - 1
				Filter.Add(ListViewPropertyFilter.Items(i).SubItems(j).Text)
			Next
			Filter(0) = VariableNames(i)

			Filter.Add(Formula)
			Filterlist.Add(Filter)
		Next

		If FilterDict.ContainsKey(Key) Then
			FilterDict.Remove(Key)
		End If

		FilterDict.Add(Key, Filterlist)

		If Not ComboBoxPropertyFilterName.Items.Contains(Key) Then
			ComboBoxPropertyFilterName.Items.Add(Key)
		End If

		' PrintDict("ListviewToDict")

		DictToListview(Key)

	End Sub

	Private Sub ListViewAddRow(Key As String)
		Dim Variable As String
		Dim PropertyString As String
		Dim Comparison As String
		Dim Value As String

		Variable = "A"
		PropertyString = String.Format("{0}.{1}",
									   ComboBoxPropertyFilterPropertySet.Text,
									   TextBoxPropertyFilterPropertyName.Text)
		Comparison = ComboBoxPropertyFilterComparison.Text
		Value = TextBoxPropertyFilterValue.Text

		Dim Columns(3) As String
		Dim Row As ListViewItem

		Columns(0) = Variable
		Columns(1) = PropertyString
		Columns(2) = Comparison
		Columns(3) = Value

		Row = New ListViewItem(Columns)

		ListViewPropertyFilter.Items.Add(Row)

		ListviewToDict(Key)
	End Sub

	Private Sub ListViewRemoveRows(Key As String)
		Dim i As Integer
		For i = 0 To ListViewPropertyFilter.SelectedItems.Count - 1
			ListViewPropertyFilter.SelectedItems(0).Remove()
		Next
		ListviewToDict(Key)
	End Sub

	Private Sub UpdateDictFormula(Key As String)
		Dim Condition As List(Of String)
		' PrintDict("UpdateDictFormula")

		For Each Condition In FilterDict(Key)
			Condition(4) = TextBoxFormula.Text
		Next

		' PrintDict("UpdateDictFormula")

	End Sub

	Private Sub DeletePropertyFilter()
		Dim Key As String

		Key = ComboBoxPropertyFilterName.Text

		FilterDict.Remove(Key)

		ComboBoxPropertyFilterName.Items.Remove(Key)
		ComboBoxPropertyFilterName.SelectedItem = ComboBoxPropertyFilterName.Items.Item(0)

		DictToListview(CType(ComboBoxPropertyFilterName.SelectedItem, String))

	End Sub



	Private Sub PopulatePropertyFilterDict()
		' Public Shared PropertyFilterDict As New Dictionary(Of String, Dictionary(Of String, String))
		' Private FilterDict As New Dictionary(Of String, List(Of List(Of String)))
		Dim Key As String
		Dim Condition As List(Of String)
		Dim Variable As String
		Dim PropertyString As String
		Dim Comparison As String
		Dim Value As String

		Form1.PropertyFilterDict.Clear()

		Key = ComboBoxPropertyFilterName.Text

		If FilterDict.Keys.Contains(Key) Then
			For Each Condition In FilterDict(Key)
				Variable = Condition(0)
				PropertyString = Condition(1)
				Comparison = Condition(2)
				Value = Condition(3)

				Form1.PropertyFilterDict.Add(Variable, New Dictionary(Of String, String))
				Form1.PropertyFilterDict(Variable).Add("PropertyString", PropertyString)
				Form1.PropertyFilterDict(Variable).Add("Comparison", Comparison)
				Form1.PropertyFilterDict(Variable).Add("Value", Value)
			Next
		End If

		' PrintPropertyFilterDict()

	End Sub

	Private Sub PopulatePropertyFilterFormula()
		Dim Formula As String

		Formula = TextBoxFormula.Text
		Formula = Formula.Replace("(", " ( ")
		Formula = Formula.Replace(")", " ) ")
		Formula = Formula.Replace("  ", " ")
		Formula = Formula.Trim()
		Formula = String.Format(" {0} ", Formula)

		Form1.PropertyFilterFormula = Formula

	End Sub




	Private Sub Startup()
		TextBoxFormula.Enabled = False
		ReconcileFormChanges()
	End Sub

	Private Sub ReconcileFormChanges()
		Dim tf As Boolean

		tf = ComboBoxPropertyFilterName.Text <> ""
		If tf Then
			ButtonPropertyFilterDelete.Enabled = True
		Else
			ButtonPropertyFilterDelete.Enabled = False
		End If

		tf = ComboBoxPropertyFilterName.Text <> ""
		tf = tf And ListViewPropertyFilter.Items.Count > 0
		If tf Then
			ButtonPropertyFilterSave.Enabled = True
		Else
			ButtonPropertyFilterSave.Enabled = False
		End If

		tf = TextBoxPropertyFilterPropertyName.Text <> ""
		tf = tf And TextBoxPropertyFilterValue.Text <> ""
		If tf Then
			ButtonPropertyFilterAdd.Enabled = True
		Else
			ButtonPropertyFilterAdd.Enabled = False
		End If

		tf = ListViewPropertyFilter.SelectedItems.Count > 0
		If tf Then
			ButtonPropertyFilterRemove.Enabled = True
		Else
			ButtonPropertyFilterRemove.Enabled = False
		End If

	End Sub

	Private Sub InitializeComboBoxes()
		Dim Key As String

		ComboBoxPropertyFilterPropertySet.Items.Clear()
		ComboBoxPropertyFilterPropertySet.Items.Add("Custom")
		'ComboBoxPropertyFilterPropertySet.Items.Add("Filename")
		'ComboBoxPropertyFilterPropertySet.Items.Add("Path")
		ComboBoxPropertyFilterPropertySet.Items.Add("System")
		ComboBoxPropertyFilterPropertySet.SelectedIndex = 1

		ComboBoxPropertyFilterComparison.Items.Clear()
		ComboBoxPropertyFilterComparison.Items.Add("contains")
		ComboBoxPropertyFilterComparison.Items.Add("is_exactly")
		ComboBoxPropertyFilterComparison.Items.Add("is_not")
		ComboBoxPropertyFilterComparison.Items.Add("wildcard_match")
		ComboBoxPropertyFilterComparison.Items.Add(">")
		ComboBoxPropertyFilterComparison.Items.Add("<")
		'ComboBoxPropertyFilterComparison.Items.Add("=")
		ComboBoxPropertyFilterComparison.SelectedIndex = 0

		ComboBoxPropertyFilterName.Items.Clear()
		If FilterDict.Keys.Count > 0 Then
			For Each Key In FilterDict.Keys
				ComboBoxPropertyFilterName.Items.Add(Key)
			Next
		End If
	End Sub

	Private Sub InitializeListview()
		ListViewPropertyFilter.View = View.Details
		ListViewPropertyFilter.GridLines = True
		ListViewPropertyFilter.FullRowSelect = True

		'Add column header
		ListViewPropertyFilter.Columns.Add("Variable", 100)
		ListViewPropertyFilter.Columns.Add("Property", 150)
		ListViewPropertyFilter.Columns.Add("Comparison", 100)
		ListViewPropertyFilter.Columns.Add("Value", 150)

	End Sub

	Private Sub InitializeReadme()
		Dim msg As String
		Dim Paragraphs As New List(Of String)
		Dim p As String

		Paragraphs.Add("Property Filter")

		msg = "The property filter allows you to select files by their property values.  "
		Paragraphs.Add(msg)

		Paragraphs.Add("Composing a Filter")

		msg = "Compose a filter by defining one or more Conditions, and adding them one-by-one to the list.  "
		msg += "A Condition consists of a Property, a Comparison, and a Value.  "
		msg += "For example, 'Material contains Steel', where 'Material' is the Property, "
		msg += "'contains' is the Comparison, and 'Steel' is the Value."
		Paragraphs.Add(msg)

		msg = "Up to six Conditions are allowed for a filter.  "
		msg += "They can be named, saved, modified, and deleted as desired.  "
		Paragraphs.Add(msg)

		Paragraphs.Add("Property")

		msg = "Enter the name of the property to be evaluated in the 'Property name' textbox.  "
		msg += "Select the Property's 'Property set', either System or Custom.  "
		msg += "The Property sets are described below.  "
		Paragraphs.Add(msg)

		msg = "System properties are in every Solid Edge file.  "
		msg += "They include Material, Manager, Project, etc.  "
		msg += "Note, at this time, the System property names must be specified in English.  "
		Paragraphs.Add(msg)

		msg = "Custom properties are ones that you create, probably in a template.  "
		msg += "The custom property names can be in any language.  (In theory, at least -- not tested at this time.)"
		Paragraphs.Add(msg)

		Paragraphs.Add("Comparison")

		msg = "Select the Comparison from its dropdown box.  "
		msg += "The choices are 'contains', 'is_exactly', 'is_not', 'wildcard_match', '>', or '<'.  "
		msg += "The options 'is_exactly', 'is_not', '>', and '<' are hopefully self explanatory.  "
		msg += "The two 'contains' options are described below.  "
		Paragraphs.Add(msg)

		msg = "With the 'contains' option, "
		msg += "the Value you specify can appear anywhere in the property.  "
		msg += "For example, if you specify 'Aluminum' and a part file has 'Aluminum 6061-T6', "
		msg += "you will get a match.  "
		msg += "Note, at this time, all Values (except see below for dates and numbers) are converted to lower case text before comparison.  "
		msg += "So 'ALUMINUM', 'Aluminum', and 'aluminum' would all match.  "
		Paragraphs.Add(msg)

		msg = "The 'wildcard_match' uses a wildcard pattern.  "
		msg += "Internally, it is implemented with the VB 'Like' operator, "
		msg += "which is similar to the old DOS wildcard search, but with a few more options.  "
		msg += "For details and examples, see "
		msg += "https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator."
		Paragraphs.Add(msg)

		Paragraphs.Add("Default Filter Formula")

		msg = "Each Condition is assigned a variable name, (A, B, ...).  "
		msg += "The default filter formula is to match all conditions (e.g., A AND B AND C).  "
		Paragraphs.Add(msg)

		Paragraphs.Add("Example")

		Paragraphs.Add("Say you have the following conditions:")

		msg = "    A  System  Material  contains    Aluminum" + vbCrLf
		msg += "    B  System  Project   is_exactly  7477" + vbCrLf
		msg += "    C  Custom  Engineer  contains    Fred"
		Paragraphs.Add(msg)

		msg = "By default you will get all Aluminum parts in project 7477 engineered by Fred.  "
		Paragraphs.Add(msg)

		Paragraphs.Add("Editing the Formula")

		msg = "You can optionally change the formula.  "
		msg += "Click the Edit button and type the desired expression.  "
		msg += "For example, if you wanted all Aluminum parts, either from project 7477, or engineered by Fred, "
		msg += "you would enter A AND (B OR C)."
		Paragraphs.Add(msg)

		Paragraphs.Add("Dates and Numbers")

		msg = "Dates and numbers are converted to their native format when possible.  "
		msg += "This is done to obtain commonsense results for the comparisons '<' and '>'.  "
		Paragraphs.Add(msg)

		msg = "Dates take the form 'YYYYMMDD' when converted.  "
		msg += "This is the format that must be used in the 'Value' field for comparisons.  "
		msg += "The conversion is supposed to be locale-aware, however this has not been tested.  "
		msg += "Please ask on the Solid Edge Forum if it is not working correctly for you.  "
		Paragraphs.Add(msg)

		msg = "Numbers are converted to floating point values.  "
		msg += "In Solid Edge many numbers, in particular those from the variable table, "
		msg += "include units.  "
		msg += "These must be stripped off by the program to make comparisons.  "
		msg += "Currently only distance and mass units are checked ('in', 'mm', 'lbm', 'kg').  "
		msg += "It's easy to add more, so please ask on the Forum if you need others.  "
		Paragraphs.Add(msg)

		Paragraphs.Add("Saved Settings")

		msg = "The filters are saved in 'property_filters.txt' in the same directory as Housekeeper.exe.  "
		msg += "If desired, you can create a master copy of the file and share it with others.  "
		msg += "You can manually edit the file, "
		msg += "however, note that the field delimiter is the TAB character.  "
		msg += "This was done so that the property name and value fields could contain space (' ') characters.  "
		Paragraphs.Add(msg)

		msg = ""
		msg += ""

		msg = ""
		For Each p In Paragraphs
			msg += String.Format("{0}{1}{2}", p, vbCrLf, vbCrLf)
		Next

		TextBoxReadme.Text = msg



	End Sub

	Public Sub SetReadmeFontsize(Fontsize As Integer)
		If Not TextBoxReadme.Font.Size = Fontsize Then
			TextBoxReadme.Font = New Font("Microsoft Sans Serif", Fontsize, FontStyle.Regular)
		End If
	End Sub

	Private Sub PrintDict(Caller As String)
		Dim msg As String
		Dim Key As String
		Dim Filter As List(Of String)
		Dim Item As String

		msg = String.Format("{0}{1}", Caller, vbCrLf)

		For Each Key In FilterDict.Keys
			msg += String.Format("{0}{1}", Key, vbCrLf)
			For Each Filter In FilterDict(Key)
				For Each Item In Filter
					msg += String.Format("{0} ", Item)
				Next
				msg += vbCrLf
			Next
		Next

		MsgBox(msg)
	End Sub

	Private Sub PrintPropertyFilterDict()
		Dim Variable As String
		Dim Item As String
		Dim msg As String

		msg = ""
		For Each Variable In Form1.PropertyFilterDict.Keys
			msg += String.Format("{0}{1}", Variable, vbCrLf)
			For Each Item In Form1.PropertyFilterDict(Variable).Keys
				msg += String.Format("{0}: {1}{2}", Item, Form1.PropertyFilterDict(Variable)(Item), vbCrLf)
			Next
		Next
		MsgBox(msg)
	End Sub


	Private Sub ButtonFormula_Click(sender As Object, e As EventArgs) Handles ButtonFormula.Click
		TextBoxFormula.Enabled = True
	End Sub

	Private Sub ButtonPropertyFilterAdd_Click(sender As Object, e As EventArgs) Handles ButtonPropertyFilterAdd.Click
		ListViewAddRow(ComboBoxPropertyFilterName.Text)
		ReconcileFormChanges()
	End Sub

	Private Sub ButtonPropertyFilterCancel_Click(sender As Object, e As EventArgs) Handles ButtonPropertyFilterCancel.Click
		Me.DialogResult = DialogResult.Cancel
	End Sub

	Private Sub ButtonPropertyFilterDelete_Click(sender As Object, e As EventArgs) Handles ButtonPropertyFilterDelete.Click
		DeletePropertyFilter()
	End Sub

	Private Sub ButtonPropertyFilterOK_Click(sender As Object, e As EventArgs) Handles ButtonPropertyFilterOK.Click
		SaveDict()
		PopulatePropertyFilterDict()
		PopulatePropertyFilterFormula()
		Me.DialogResult = DialogResult.OK
	End Sub

	Private Sub ButtonPropertyFilterRemove_Click(sender As Object, e As EventArgs) Handles ButtonPropertyFilterRemove.Click
		ListViewRemoveRows(ComboBoxPropertyFilterName.Text)
		ReconcileFormChanges()
	End Sub

	Private Sub ButtonPropertyFilterSave_Click(sender As Object, e As EventArgs) Handles ButtonPropertyFilterSave.Click
		ListviewToDict(ComboBoxPropertyFilterName.Text, ResetFormula:=False)
	End Sub


	Private Sub ComboBoxPropertyFilterName_LostFocus(sender As Object, e As EventArgs) Handles ComboBoxPropertyFilterName.LostFocus
		ReconcileFormChanges()
	End Sub

	Private Sub ComboBoxPropertyFilterName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertyFilterName.SelectedIndexChanged
		DictToListview(ComboBoxPropertyFilterName.Text)
		ReconcileFormChanges()
	End Sub

	Private Sub ComboBoxPropertyFilterPropertySet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertyFilterPropertySet.SelectedIndexChanged
		If ComboBoxPropertyFilterPropertySet.Text = "Filename" Then
			TextBoxPropertyFilterPropertyName.Text = "Filename"
		ElseIf ComboBoxPropertyFilterPropertySet.Text = "Path" Then
			TextBoxPropertyFilterPropertyName.Text = "Path"
		Else
			TextBoxPropertyFilterPropertyName.Text = ""
		End If
	End Sub



	Private Sub FormPropertyFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Startup()
	End Sub


	Private Sub ListViewPropertyFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewPropertyFilter.SelectedIndexChanged
		ReconcileFormChanges()
	End Sub


	Private Sub TextBoxPropertyFilterPropertyName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPropertyFilterPropertyName.TextChanged
		ReconcileFormChanges()
	End Sub

	Private Sub TextBoxPropertyFilterValue_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPropertyFilterValue.TextChanged
		ReconcileFormChanges()
	End Sub

	Private Sub TextBoxFormula_LostFocus(sender As Object, e As EventArgs) Handles TextBoxFormula.LostFocus
		TextBoxFormula.Enabled = False
		UpdateDictFormula(ComboBoxPropertyFilterName.Text)
	End Sub

End Class