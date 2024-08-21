﻿Option Strict On

Imports Newtonsoft.Json

Public Class FormPropertyFilter

    Public Property PropertyFilterDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property JSONString As String
    '{"0":
    '    {"Variable":"A",
    '     "PropertySet":"Custom",
    '     "PropertyName":"hmk_Part_Number",
    '     "Comparison":"contains",
    '     "Value":"aluminum",
    '     "Formula":"A AND B"},
    ' "1":
    '...
    '}

    Public Property UCList As List(Of UCPropertyFilter)
    Public Property HelpURL As String
    Public Property SavedSettingsDict As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String)))
    Public Property TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property TemplatePropertyList As List(Of String)
    Public Property Formula As String



    'Dim t As Timer = New Timer()

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.TemplatePropertyDict = Form_Main.TemplatePropertyDict
        Me.TemplatePropertyList = Form_Main.TemplatePropertyList

        Dim UP As New UtilsPreferences
        'Me.PropertyFilterDict = UP.GetPropertyFilterDict
        Me.SavedSettingsDict = UP.GetPropertyFilterSavedSettings()

        'Me.JSONString = JsonConvert.SerializeObject(Me.PropertyFilterDict)

        Me.UCList = New List(Of UCPropertyFilter)


        Dim Version = Form_Main.Version

        Dim VersionSpecificReadme = String.Format("https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/README-{0}.md", Version)

        Me.HelpURL = String.Format("{0}#{1}", VersionSpecificReadme, "filtering")


        Dim tf As Boolean

        tf = Me.TemplatePropertyDict Is Nothing
        tf = tf Or Me.TemplatePropertyList Is Nothing

        If Not tf Then
            tf = Me.TemplatePropertyDict.Count = 0
            tf = tf Or Me.TemplatePropertyList.Count = 0
        End If

        tf = tf And Form_Main.WarnNoImportedProperties

        If tf Then
            Dim s As String = "Template properties not populated.  This is not an error, "
            s = String.Format("{0}however it means properties must be entered manually.  ", s)
            s = String.Format("{0}They cannot be selected from the drop down list.{1}{2}", s, vbCrLf, vbCrLf)
            s = String.Format("{0}If desired, populate the list on the Configuration Tab -- Templates Page.{1}{2}", s, vbCrLf, vbCrLf)
            s = String.Format("{0}Disable this message on the Configuration Tab -- General Page.", s)
            MsgBox(s, vbOKOnly)
        End If

    End Sub


    Private Function CheckInputs() As Boolean
        Dim InputsOK As Boolean = True
        Dim s As String = ""
        Dim indent As String = "    "

        'For Each UC As UCEditProperties In UCList

        '	' Ignore any with no PropertyName
        '	If Not UC.PropertyName = "" Then
        '		If UC.PropertySet = "" Then
        '			s = String.Format("{0}{1}Select a PropertySet for '{2}'{3}", s, indent, UC.PropertyName, vbCrLf)
        '		End If
        '		If UC.FindSearch = "" Then
        '			s = String.Format("{0}{1}Select a Find Search Type for '{2}'{3}", s, indent, UC.PropertyName, vbCrLf)
        '		End If
        '		If Not UC.FindSearch = "X" Then
        '			If UC.ReplaceSearch = "" Then
        '				s = String.Format("{0}{1}Select a Replace Search Type for '{2}'{3}", s, indent, UC.PropertyName, vbCrLf)
        '			End If
        '		End If
        '	End If

        'Next

        'If Not s = "" Then
        '	InputsOK = False
        '	s = String.Format("Please correct the following before continuing{0}{1}", vbCrLf, s)
        '	MsgBox(s, vbOKOnly)
        'End If

        Return InputsOK
    End Function

    Private Function CreateJSONDict() As Dictionary(Of String, Dictionary(Of String, String))
        Dim JSONDict As New Dictionary(Of String, Dictionary(Of String, String))

        Dim i = 0

        For Each UC As UCPropertyFilter In UCList
            If Not UC.PropertyName = "" Then
                Dim d = New Dictionary(Of String, String)
                d("Variable") = UC.Variable
                d("PropertySet") = UC.PropertySet
                d("PropertyName") = UC.PropertyName
                d("Comparison") = UC.Comparison
                d("Value") = UC.Value
                d("Formula") = UC.Formula

                JSONDict(CStr(i)) = d

                i += 1
            End If
        Next

        Return JSONDict
    End Function

    Public Sub PopulateUCList(JSONDict As Dictionary(Of String, Dictionary(Of String, String)))
        Dim NewUC As UCPropertyFilter

        Me.UCList.Clear()

        'Dim Ascii = 65  ' "A"

        For Each Key As String In JSONDict.Keys
            NewUC = New UCPropertyFilter(Me)

            NewUC.Variable = JSONDict(Key)("Variable")
            'NewUC.Variable = Chr(Ascii)
            NewUC.PropertySet = JSONDict(Key)("PropertySet")
            NewUC.PropertyName = JSONDict(Key)("PropertyName")
            NewUC.Comparison = JSONDict(Key)("Comparison")
            NewUC.Value = JSONDict(Key)("Value")
            NewUC.Formula = JSONDict(Key)("Formula")

            NewUC.ReconcileFormWithProps()

            NewUC.Dock = DockStyle.Fill

            UCList.Add(NewUC)

            'Ascii += 1
        Next

    End Sub

    Public Sub PopulateForm()

        Dim JSONDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim ListIsFromSavedSettings As Boolean = False

        If Not (Me.JSONString = "" Or Me.JSONString = "{}") Then
            ListIsFromSavedSettings = True
            JSONDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(Me.JSONString)
        End If

        PopulateUCList(JSONDict)

        UpdateForm(ListIsFromSavedSettings:=ListIsFromSavedSettings)

    End Sub

    Private Sub FormatFormula(tmpFormula As String)
        Dim s = tmpFormula.ToUpper

        s = s.Replace("(", " ( ")
        s = s.Replace(")", " ) ")
        s = s.Replace("  ", " ")
        s = s.Trim()
        s = String.Format(" {0} ", s)

        Me.Formula = s
        TextBoxFormula.Text = Me.Formula

        For Each UC As UCPropertyFilter In UCList
            If Not UC.PropertyName = "" Then
                UC.Formula = Me.Formula
                'UC.ReconcileFormWithProps()
            End If
        Next

    End Sub

    Private Sub UpdateVariablesAndFormula()
        Dim tmpFormula As String = ""
        Dim Ascii = 65  ' "A"

        For Each UC As UCPropertyFilter In UCList
            UC.Variable = Chr(Ascii)
            If Not UC.PropertyName = "" Then
                If tmpFormula = "" Then
                    tmpFormula = Chr(Ascii)
                Else
                    tmpFormula = String.Format("{0} AND {1}", tmpFormula, Chr(Ascii))
                End If
            End If
            Ascii += 1
        Next

        tmpFormula = String.Format(" {0} ", tmpFormula)

        Me.Formula = tmpFormula
        TextBoxFormula.Text = Me.Formula

        For Each UC As UCPropertyFilter In UCList
            If Not UC.PropertyName = "" Then
                UC.Formula = Me.Formula
                UC.ReconcileFormWithProps()
            End If
        Next

    End Sub

    Private Sub UpdateForm(ListIsFromSavedSettings As Boolean)

        If Not ListIsFromSavedSettings Then
            UpdateVariablesAndFormula()
        Else
            Dim tmpFormula As String = UCList(0).Formula
            Me.Formula = tmpFormula
            TextBoxFormula.Text = Me.Formula
        End If

        ExTableLayoutPanelFilters.Controls.Clear()
        ExTableLayoutPanelFilters.RowStyles.Clear()

        ' Shouldn't need this, but it doesn't work without it.
        ExTableLayoutPanelFilters.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))

        ' Not true, but it doesn't work without it.
        ExTableLayoutPanelFilters.RowCount = 1

        Dim NeedANewRow As Boolean = True

        For Each UC As UCPropertyFilter In UCList
            ExTableLayoutPanelFilters.RowCount += 1
            ExTableLayoutPanelFilters.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))
            ExTableLayoutPanelFilters.Controls.Add(UC)
            If UC.PropertyName = "" Then
                NeedANewRow = False
            End If
        Next

        If NeedANewRow Then
            AddRow(ListIsFromSavedSettings:=ListIsFromSavedSettings)
        End If

    End Sub

    Public Sub AddRow(ListIsFromSavedSettings As Boolean)
        Dim NewUC As New UCPropertyFilter(Me)
        NewUC.Dock = DockStyle.Fill
        Me.UCList.Add(NewUC)

        If Not ListIsFromSavedSettings Then
            UpdateVariablesAndFormula()
        End If

        ExTableLayoutPanelFilters.RowCount += 1
        ExTableLayoutPanelFilters.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))
        ExTableLayoutPanelFilters.Controls.Add(NewUC)

    End Sub

    Private Sub MoveRow(Direction As String)
        Dim SelectedRow As Integer = GetSelectedRow()
        Dim tmpUCList As New List(Of UCPropertyFilter)
        Dim tf As Boolean

        tf = Direction.ToLower = "up"
        tf = tf Or Direction.ToLower = "down"
        If Not tf Then
            MsgBox(String.Format("Unrecognized direction '{0}'", Direction), vbOKOnly)
            Exit Sub
        End If

        'Can't move up from the top
        tf = (SelectedRow = 0) And (Direction = "up")

        'Can't move down from the bottom
        Dim Bottom = UCList.Count - 1
        tf = tf Or ((SelectedRow >= Bottom) And (Direction = "down"))

        If Not tf Then
            For i As Integer = 0 To Me.UCList.Count - 1

                Select Case Direction

                    Case "up"
                        If i = SelectedRow - 1 Then
                            tmpUCList.Add(Me.UCList(i + 1))
                        ElseIf i = SelectedRow Then
                            tmpUCList.Add(Me.UCList(i - 1))
                        Else
                            tmpUCList.Add(Me.UCList(i))
                        End If

                    Case "down"
                        If i = SelectedRow Then
                            tmpUCList.Add(Me.UCList(i + 1))
                        ElseIf i = SelectedRow + 1 Then
                            tmpUCList.Add(Me.UCList(i - 1))
                        Else
                            tmpUCList.Add(Me.UCList(i))
                        End If

                    Case Else
                        MsgBox(String.Format("Direction '{0}' not recognized", Direction))
                End Select

            Next

            Me.UCList = tmpUCList

        End If

        UpdateForm(ListIsFromSavedSettings:=False)

    End Sub

    Private Function GetSelectedRow() As Integer
        Dim SelectedRow As Integer = -1

        Dim i = 0
        For Each UC As UCPropertyFilter In UCList
            If UC.Selected Then
                If UC.Selected Then SelectedRow = i
                Exit For
            End If
            i += 1
        Next

        Return SelectedRow
    End Function

    Public Sub UCChanged(ChangedUC As UCPropertyFilter)

        Dim NeedANewRow As Boolean = True

        For Each UC As UCPropertyFilter In UCList
            If ChangedUC.Selected Then
                If UC IsNot ChangedUC Then
                    UC.CheckBoxSelect.Checked = False
                End If
            End If
            If UC.PropertyName = "" Then
                NeedANewRow = False
            End If
        Next

        If NeedANewRow Then
            AddRow(ListIsFromSavedSettings:=False)
        End If

    End Sub


    Private Sub FormPropertyFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.JSONString = JsonConvert.SerializeObject(Me.PropertyFilterDict)

        PopulateForm()

        ComboBoxSavedSettings.Items.Add("")
        For Each Key As String In Me.SavedSettingsDict.Keys
            ComboBoxSavedSettings.Items.Add(Key)
        Next

        If Not PropertyFilterDict.Keys.Count = 0 Then
            FormatFormula(PropertyFilterDict("0")("Formula"))
        End If

    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click

        If CheckInputs() Then
            Dim JSONDict As Dictionary(Of String, Dictionary(Of String, String))

            JSONDict = CreateJSONDict()
            Me.JSONString = JsonConvert.SerializeObject(JSONDict)

            Me.PropertyFilterDict = JSONDict

            Dim UP As New UtilsPreferences
            UP.SavePropertyFilterDict(Me.PropertyFilterDict)

            Me.DialogResult = DialogResult.OK
        End If

    End Sub


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub


    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
        System.Diagnostics.Process.Start(Me.HelpURL)
    End Sub


    Private Sub ButtonRowDelete_Click(sender As Object, e As EventArgs) Handles ButtonRowDelete.Click
        Dim SelectedRow = GetSelectedRow()
        Dim tmpUCList As New List(Of UCPropertyFilter)

        If SelectedRow = -1 Then
            MsgBox("No row is selected.  Select one by enabling its checkbox.")
        Else
            For i = 0 To UCList.Count - 1
                If Not i = SelectedRow Then
                    tmpUCList.Add(UCList(i))
                End If
            Next

            UCList = tmpUCList

            UpdateForm(ListIsFromSavedSettings:=False)
        End If

    End Sub

    Private Sub ButtonRowUp_Click(sender As Object, e As EventArgs) Handles ButtonRowUp.Click
        MoveRow("up")
    End Sub

    Private Sub ButtonRowDown_Click(sender As Object, e As EventArgs) Handles ButtonRowDown.Click
        MoveRow("down")
    End Sub

    Private Sub ButtonSaveSettings_Click(sender As Object, e As EventArgs) Handles ButtonSaveSettings.Click
        Dim Name As String = ComboBoxSavedSettings.Text
        Dim Proceed As Boolean = True

        If Name = "" Then
            Proceed = False
            MsgBox("Enter a name for these settings", vbOKOnly)
        End If

        If Proceed And ComboBoxSavedSettings.Items.Contains(Name) Then
            Dim Result = MsgBox(String.Format("Name '{0}' already exists.  Do you want to replace it?", Name), vbYesNo)
            If Result = vbNo Then
                Proceed = False
            End If
        End If

        If Proceed Then
            Dim JSONDict As Dictionary(Of String, Dictionary(Of String, String))
            JSONDict = CreateJSONDict()

            SavedSettingsDict(Name) = JSONDict

            If Not ComboBoxSavedSettings.Items.Contains(Name) Then
                ComboBoxSavedSettings.Items.Add(Name)
            End If

            Dim UP As New UtilsPreferences
            UP.SavePropertyFilterSavedSettings(Me.SavedSettingsDict)

        End If
    End Sub

    Private Sub ComboBoxSavedSettings_Click(sender As Object, e As EventArgs) Handles ComboBoxSavedSettings.SelectedIndexChanged
        Dim Name As String = ComboBoxSavedSettings.Text
        If SavedSettingsDict.Keys.Contains(Name) Then

            PopulateUCList(SavedSettingsDict(Name))

            UpdateForm(ListIsFromSavedSettings:=True)

        End If

    End Sub

    Private Sub ButtonRemoveSetting_Click(sender As Object, e As EventArgs) Handles ButtonRemoveSetting.Click
        Dim Name As String = ComboBoxSavedSettings.Text
        If SavedSettingsDict.Keys.Contains(Name) Then
            SavedSettingsDict.Remove(Name)
        End If
        ComboBoxSavedSettings.Items.Remove(Name)
        ComboBoxSavedSettings.Text = ""

        Dim UP As New UtilsPreferences
        UP.SavePropertyFilterSavedSettings(Me.SavedSettingsDict)

    End Sub

    Private Sub ToolStripLabel3_Click(sender As Object, e As EventArgs) Handles ToolStripLabel3.Click

        Dim FTP As New FormTextPrompt
        FTP.Text = "Edit Formula"
        FTP.TextBoxInput.Text = TextBoxFormula.Text
        FTP.LabelPrompt.Text = "Edit formula"
        Dim Result = FTP.ShowDialog()

        If Result = DialogResult.OK Then
            FormatFormula(FTP.TextBoxInput.Text)
        End If

    End Sub

    Private Sub ButtonEditFormula_Click(sender As Object, e As EventArgs) Handles ButtonEditFormula.Click

        Dim FTP As New FormTextPrompt
        FTP.Text = "Edit Formula"
        FTP.TextBoxInput.Text = TextBoxFormula.Text
        FTP.LabelPrompt.Text = "Edit formula"
        Dim Result = FTP.ShowDialog()

        If Result = DialogResult.OK Then
            FormatFormula(FTP.TextBoxInput.Text)
        End If

    End Sub


End Class