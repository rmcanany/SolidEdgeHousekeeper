Option Strict On

Imports Newtonsoft.Json

Public Class FormPropertyInputEditor
    Private ColumnsDict As New Dictionary(Of Integer, Dictionary(Of String, String))
    Private InputEditorDoctor As New InputEditorDoctor
    Private ProcessCheckBoxEvents As Boolean
    Private FileType As String

    Public Property JSONString As String
    '{"0":
    '    {"PropertySet":"Custom",
    '     "PropertyName":"hmk_Part_Number",
    '     "FindSearch":"PT",
    '     "FindString":"a",
    '     "ReplaceSearch":"PT",
    '     "ReplaceString":"b"},
    ' "1":
    '...
    '}

    Public Property UCList As List(Of UCEditProperties)
    Public Property HelpURL As String
    Public Property SavedSettingsDict As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String)))
    Public Property TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property TemplatePropertyList As List(Of String)


    Dim t As Timer = New Timer()

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.TemplatePropertyDict = Form1.TemplatePropertyDict
        Me.TemplatePropertyList = Form1.TemplatePropertyList
        Me.UCList = New List(Of UCEditProperties)

    End Sub


    Private Function CheckInputs() As Boolean
        Dim InputsOK As Boolean = True
        Dim s As String = ""
        Dim indent As String = "    "

        For Each UC As UCEditProperties In UCList

            ' Ignore any with no PropertyName
            If Not UC.PropertyName = "" Then
                If UC.PropertySet = "" Then
                    s = String.Format("{0}{1}Select a PropertySet for '{2}'{3}", s, indent, UC.PropertyName, vbCrLf)
                End If
                If UC.FindSearch = "" Then
                    s = String.Format("{0}{1}Select a Find Search Type for '{2}'{3}", s, indent, UC.PropertyName, vbCrLf)
                End If
                If Not UC.FindSearch = "X" Then
                    If UC.ReplaceSearch = "" Then
                        s = String.Format("{0}{1}Select a Replace Search Type for '{2}'{3}", s, indent, UC.PropertyName, vbCrLf)
                    End If
                End If
            End If

        Next

        If Not s = "" Then
            InputsOK = False
            s = String.Format("Please correct the following before continuing{0}{1}", vbCrLf, s)
            MsgBox(s, vbOKOnly)
        End If

        Return InputsOK
    End Function

    Private Function CreateJSONDict() As Dictionary(Of String, Dictionary(Of String, String))
        Dim JSONDict As New Dictionary(Of String, Dictionary(Of String, String))

        Dim i = 0

        For Each UC As UCEditProperties In UCList
            If Not UC.PropertyName = "" Then
                Dim d = New Dictionary(Of String, String)
                d("PropertySet") = UC.PropertySet
                d("PropertyName") = UC.PropertyName
                d("FindSearch") = UC.FindSearch
                d("FindString") = UC.FindString
                d("ReplaceSearch") = UC.ReplaceSearch
                d("ReplaceString") = UC.ReplaceString

                JSONDict(CStr(i)) = d

                i += 1
            End If
        Next

        Return JSONDict
    End Function

    Public Sub PopulateUCList(JSONDict As Dictionary(Of String, Dictionary(Of String, String)))
        Dim NewUC As UCEditProperties

        Me.UCList.Clear()

        For Each Key As String In JSONDict.Keys
            NewUC = New UCEditProperties(Me)
            NewUC.PropertySet = JSONDict(Key)("PropertySet")
            NewUC.PropertyName = JSONDict(Key)("PropertyName")
            NewUC.FindSearch = JSONDict(Key)("FindSearch")
            NewUC.FindString = JSONDict(Key)("FindString")
            NewUC.ReplaceSearch = JSONDict(Key)("ReplaceSearch")
            NewUC.ReplaceString = JSONDict(Key)("ReplaceString")

            NewUC.ReconcileFormWithProps()

            NewUC.Dock = DockStyle.Fill

            UCList.Add(NewUC)
        Next

    End Sub

    Public Sub PopulateForm()

        Dim JSONDict As New Dictionary(Of String, Dictionary(Of String, String))

        If Not (Me.JSONString = "" Or Me.JSONString = "{}") Then
            JSONDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(Me.JSONString)
        End If

        PopulateUCList(JSONDict)

        UpdateForm()

    End Sub

    Private Sub UpdateForm()
        ExTableLayoutPanelSearches.Controls.Clear()
        ExTableLayoutPanelSearches.RowStyles.Clear()

        ' Shouldn't need this, but it doesn't work without it.
        ExTableLayoutPanelSearches.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))

        ' Not true, but it doesn't work without it.
        ExTableLayoutPanelSearches.RowCount = 1

        Dim NeedANewRow As Boolean = True

        For Each UC As UCEditProperties In UCList
            ExTableLayoutPanelSearches.RowCount += 1
            ExTableLayoutPanelSearches.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))
            ExTableLayoutPanelSearches.Controls.Add(UC)
            If UC.PropertyName = "" Then
                NeedANewRow = False
            End If
        Next

        If NeedANewRow Then
            AddRow()
        End If

    End Sub

    Public Sub AddRow()
        Dim NewUC As New UCEditProperties(Me)
        NewUC.Dock = DockStyle.Fill
        Me.UCList.Add(NewUC)

        ExTableLayoutPanelSearches.RowCount += 1
        ExTableLayoutPanelSearches.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))
        ExTableLayoutPanelSearches.Controls.Add(NewUC)

    End Sub

    Private Sub MoveRow(Direction As String)
        Dim SelectedRow As Integer = GetSelectedRow()
        Dim tmpUCList As New List(Of UCEditProperties)
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

        UpdateForm()

    End Sub

    Private Function GetSelectedRow() As Integer
        Dim SelectedRow As Integer = -1

        Dim i = 0
        For Each UC As UCEditProperties In UCList
            If UC.Selected Then
                If UC.Selected Then SelectedRow = i
                Exit For
            End If
            i += 1
        Next

        Return SelectedRow
    End Function

    Public Sub UCChanged(ChangedUC As UCEditProperties)

        Dim NeedANewRow As Boolean = True

        For Each UC As UCEditProperties In UCList
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
            AddRow()
        End If

    End Sub


    Private Sub FormPropertyInputEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        PopulateForm()

        t.Interval = 1500
        AddHandler t.Tick, AddressOf HandleTimerTick

        Dim UP As New UtilsPreferences
        Me.SavedSettingsDict = UP.GetEditPropertiesSavedSettings()

        ComboBoxSavedSettings.Items.Add("")
        For Each Key As String In Me.SavedSettingsDict.Keys
            ComboBoxSavedSettings.Items.Add(Key)
        Next

    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click

        If CheckInputs() Then
            Dim UP As New UtilsPreferences
            UP.SaveEditPropertiesSavedSettings(Me.SavedSettingsDict)

            Dim JSONDict As Dictionary(Of String, Dictionary(Of String, String))

            JSONDict = CreateJSONDict()
            Me.JSONString = JsonConvert.SerializeObject(JSONDict)

            Me.DialogResult = DialogResult.OK
        End If

    End Sub


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub


    Sub MessageTimeOut(sMessage As String, sTitle As String, iSeconds As Integer)

        Dim tmpForm As New Form
        Dim tmpSize = New Size(200, 75)
        tmpForm.Text = String.Empty
        tmpForm.ControlBox = False
        tmpForm.BackColor = Color.White

        'tmpForm.FormBorderStyle = FormBorderStyle.None
        tmpForm.Size = tmpSize
        tmpForm.StartPosition = FormStartPosition.Manual
        tmpForm.Location = New Point(CInt(Me.Left + Me.Width / 2 - tmpForm.Width / 2), CInt(Me.Top + Me.Height / 2 - tmpForm.Height / 2))

        Dim tmpLabel As New Label
        tmpLabel.Font = New Font(Me.Font.Name, 8, FontStyle.Bold)
        tmpLabel.Width = 180
        tmpLabel.Dock = DockStyle.Fill
        tmpLabel.TextAlign = ContentAlignment.MiddleCenter
        tmpLabel.Text = "Expression copied to clipboard"
        tmpForm.Controls.Add(tmpLabel)

        tmpForm.Show(Me)

        t.Start()

    End Sub

    Sub HandleTimerTick(sender As Object, e As EventArgs)

        For Each item In Me.OwnedForms
            item.Close()
        Next

        t.Stop()

    End Sub

    Private Sub ToolStripButtonHelp_Click(sender As Object, e As EventArgs) Handles ToolStripButtonHelp.Click
        System.Diagnostics.Process.Start(Me.HelpURL)
    End Sub

    Private Sub ToolStripButtonExpressionEditor_Click(sender As Object, e As EventArgs) Handles ToolStripButtonExpressionEditor.Click

        Dim tmp As New FormNCalc
        tmp.TextEditorFormula.Language = FastColoredTextBoxNS.Language.SQL
        'tmp.TextEditorNCalc.Text = "'%{System.Title}' + '-' + toString(cast(substring('%{System.Comments}', lastIndexOf('%{System.Comments}', 'L=')+2, length('%{System.Comments}') - lastIndexOf('%{System.Comments}', ' ')),'System.Int32'),'D4') + '-' + substring('%{System.Comments}', lastIndexOf('%{System.Comments}', ' ')+1)"
        tmp.ShowDialog()
        Dim A = tmp.Formula.Replace(vbCrLf, "")
        A = A.Split(CType("\\", Char)).First

        If A <> "" Then
            Clipboard.SetText(A)
            MessageTimeOut("Expression copied in clipboard", "Expression editor", 1)
        End If

    End Sub

    Private Sub ToolStripButtonDeleteRow_Click(sender As Object, e As EventArgs) Handles ToolStripButtonDeleteRow.Click
        Dim SelectedRow = GetSelectedRow()
        Dim tmpUCList As New List(Of UCEditProperties)

        If SelectedRow = -1 Then
            MsgBox("No row is selected.  Select one by enabling its checkbox.")
        Else
            For i = 0 To UCList.Count - 1
                If Not i = SelectedRow Then
                    tmpUCList.Add(UCList(i))
                End If
            Next

            UCList = tmpUCList

            UpdateForm()
        End If

    End Sub

    Private Sub ToolStripButtonUp_Click(sender As Object, e As EventArgs) Handles ToolStripButtonUp.Click
        MoveRow("up")
    End Sub

    Private Sub ToolStripButtonDown_Click(sender As Object, e As EventArgs) Handles ToolStripButtonDown.Click
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

        End If
    End Sub

    Private Sub ComboBoxSavedSettings_Click(sender As Object, e As EventArgs) Handles ComboBoxSavedSettings.SelectedIndexChanged
        Dim Name As String = ComboBoxSavedSettings.Text
        If SavedSettingsDict.Keys.Contains(Name) Then

            PopulateUCList(SavedSettingsDict(Name))

            UpdateForm()

        End If

    End Sub

    Private Sub ButtonDeleteSetting_Click(sender As Object, e As EventArgs) Handles ButtonDeleteSetting.Click
        Dim Name As String = ComboBoxSavedSettings.Text
        If SavedSettingsDict.Keys.Contains(Name) Then
            SavedSettingsDict.Remove(Name)
        End If
        ComboBoxSavedSettings.Items.Remove(Name)
        ComboBoxSavedSettings.Text = ""
    End Sub
End Class