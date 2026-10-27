Option Strict On

'Imports Newtonsoft.Json

Public Class FormVariableInputEditor
    Public Property JSONString As String
    Public Property UCList As List(Of UCEditVariables)
    Public Property HelpURL As String
    Public Property SavedSettingsDict As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String)))


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.UCList = New List(Of UCEditVariables)

    End Sub


    Private Function CheckInputs() As Boolean
        Dim InputsOK As Boolean = True
        Dim s As String = ""
        Dim indent As String = "    "

        For Each UC As UCEditVariables In UCList

            ' Ignore any with no VariableName
            If Not UC.VariableName = "" Then
                If UC.VariableName.Contains(" ") Then
                    s = $"{s}{indent}Remove space characters from variable name '{UC.VariableName}'{vbCrLf}"
                End If
                If IsNumeric(UC.VariableName(0)) Then
                    s = $"{s}{indent}Remove leading numbers from variable name '{UC.VariableName}'{vbCrLf}"
                End If
                If (UC.Formula = "") And (Not (UC.Expose Or UC.ChangeName)) Then
                    s = $"{s}{indent}Enter a number/formula for '{UC.VariableName}'{vbCrLf}"
                End If
            End If

        Next

        If Not s = "" Then
            InputsOK = False
            s = $"Please correct the following before continuing{vbCrLf}{s}"
            MsgBox(s, vbOKOnly)
        End If

        Return InputsOK
    End Function

    Private Function CreateJSONDict() As Dictionary(Of String, Dictionary(Of String, String))
        Dim JSONDict As New Dictionary(Of String, Dictionary(Of String, String))

        Dim i = 0

        For Each UC As UCEditVariables In UCList
            If Not UC.VariableName = "" Then
                Dim d = New Dictionary(Of String, String)
                d("VariableName") = UC.VariableName
                d("Formula") = UC.Formula
                d("UnitType") = UC.UnitType
                d("ChangeName") = CStr(UC.ChangeName)
                d("NewName") = UC.NewName
                d("Expose") = CStr(UC.Expose)
                d("ExposeName") = UC.ExposeName

                JSONDict(CStr(i)) = d

                i += 1
            End If
        Next

        Return JSONDict
    End Function

    Public Sub PopulateUCList(JSONDict As Dictionary(Of String, Dictionary(Of String, String)))
        Dim NewUC As UCEditVariables

        Me.UCList.Clear()

        For Each Key As String In JSONDict.Keys
            NewUC = New UCEditVariables(Me)
            NewUC.VariableName = JSONDict(Key)("VariableName")
            NewUC.Formula = JSONDict(Key)("Formula")
            NewUC.UnitType = JSONDict(Key)("UnitType")
            NewUC.ChangeName = CBool(JSONDict(Key)("ChangeName"))
            NewUC.NewName = JSONDict(Key)("NewName")
            NewUC.Expose = CBool(JSONDict(Key)("Expose"))
            NewUC.ExposeName = JSONDict(Key)("ExposeName")

            NewUC.ReconcileFormWithProps()

            NewUC.Dock = DockStyle.Fill

            UCList.Add(NewUC)
        Next

    End Sub

    Public Sub PopulateForm()

        Dim JSONDict As New Dictionary(Of String, Dictionary(Of String, String))

        Try
            If Not (Me.JSONString = "" Or Me.JSONString = "{}") Then
                JSONDict = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(Me.JSONString)
            End If

            PopulateUCList(JSONDict)
        Catch ex As Exception
        End Try

        UpdateForm()

    End Sub

    Private Sub UpdateForm()
        ExTableLayoutPanelVariables.Controls.Clear()
        ExTableLayoutPanelVariables.RowStyles.Clear()

        ' Shouldn't need this, but it doesn't work without it.
        ExTableLayoutPanelVariables.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))

        ' Not true, but it doesn't work without it.
        ExTableLayoutPanelVariables.RowCount = 1

        Dim NeedANewRow As Boolean = True

        For Each UC As UCEditVariables In UCList
            ExTableLayoutPanelVariables.RowCount += 1
            ExTableLayoutPanelVariables.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))
            ExTableLayoutPanelVariables.Controls.Add(UC)
            If UC.VariableName = "" Then
                NeedANewRow = False
            End If
        Next

        If NeedANewRow Then
            AddRow()
        End If

    End Sub

    Public Sub AddRow()
        Dim NewUC As New UCEditVariables(Me)
        NewUC.Dock = DockStyle.Fill
        Me.UCList.Add(NewUC)

        ExTableLayoutPanelVariables.RowCount += 1
        ExTableLayoutPanelVariables.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))
        ExTableLayoutPanelVariables.Controls.Add(NewUC)

    End Sub

    Private Sub MoveRow(Direction As String)
        'Dim SelectedRow As Integer = GetSelectedRow()
        'Dim tmpUCList As New List(Of UCEditVariables)
        'Dim tf As Boolean

        'tf = Direction.ToLower = "up"
        'tf = tf Or Direction.ToLower = "down"
        'If Not tf Then
        '    MsgBox($"Unrecognized direction '{Direction}'")
        '    Exit Sub
        'End If

        ''Can't move up from the top
        'tf = (SelectedRow = 0) And (Direction = "up")

        ''Can't move down from the bottom
        'Dim Bottom = UCList.Count - 1
        'tf = tf Or ((SelectedRow >= Bottom) And (Direction = "down"))

        'If Not tf Then
        '    For i As Integer = 0 To Me.UCList.Count - 1

        '        Select Case Direction

        '            Case "up"
        '                If i = SelectedRow - 1 Then
        '                    tmpUCList.Add(Me.UCList(i + 1))
        '                ElseIf i = SelectedRow Then
        '                    tmpUCList.Add(Me.UCList(i - 1))
        '                Else
        '                    tmpUCList.Add(Me.UCList(i))
        '                End If

        '            Case "down"
        '                If i = SelectedRow Then
        '                    tmpUCList.Add(Me.UCList(i + 1))
        '                ElseIf i = SelectedRow + 1 Then
        '                    tmpUCList.Add(Me.UCList(i - 1))
        '                Else
        '                    tmpUCList.Add(Me.UCList(i))
        '                End If

        '            Case Else
        '                MsgBox($"Unrecognized direction '{Direction}'")

        '        End Select

        '    Next

        '    Me.UCList = tmpUCList

        'End If

        'UpdateForm()

        If Not (Direction.ToLower = "up" Or Direction.ToLower = "down") Then
            MsgBox($"Unrecognized direction '{Direction}'", vbOKOnly)
            Exit Sub
        End If

        Dim SelectedRows As List(Of Integer) = GetSelectedRows()

        If SelectedRows.Count = 0 Then
            MsgBox("No row is selected.  Select one by enabling its checkbox.")
            Exit Sub
        End If
        If Direction.ToLower = "up" And SelectedRows.Contains(0) Then Exit Sub
        If Direction.ToLower = "down" Then
            ' Deal with NewRow
            Dim tmpUC As UCEditVariables = UCList(UCList.Count - 1)
            If tmpUC.VariableName = "" Then
                If SelectedRows.Contains(UCList.Count - 2) Then Exit Sub
            Else
                If SelectedRows.Contains(UCList.Count - 1) Then Exit Sub
            End If
        End If

        Dim tmpUCList As New List(Of UCEditVariables)
        For Each UC As UCEditVariables In UCList
            tmpUCList.Add(UC)
        Next

        If Direction.ToLower = "up" Then
            For i As Integer = 0 To tmpUCList.Count - 2  ' From top to second-from-bottom
                Dim NextUC As UCEditVariables = tmpUCList(i + 1)
                If NextUC.Selected Then
                    Dim tmpUC As UCEditVariables = tmpUCList(i)
                    tmpUCList(i) = NextUC
                    tmpUCList(i + 1) = tmpUC
                End If
            Next
        Else
            For i As Integer = tmpUCList.Count - 1 To 1 Step -1 ' From bottom to second-from-top
                Dim PreviousUC As UCEditVariables = tmpUCList(i - 1)
                Dim PProp = PreviousUC.VariableName
                If PreviousUC.Selected Then
                    Dim tmpUC As UCEditVariables = tmpUCList(i)
                    Dim tmpProp = tmpUC.VariableName
                    tmpUCList(i) = PreviousUC
                    tmpUCList(i - 1) = tmpUC
                End If
            Next
        End If

        Me.UCList = tmpUCList

        UpdateForm()
    End Sub

    Private Function GetSelectedRow() As Integer
        Dim SelectedRow As Integer = -1

        Dim i = 0
        For Each UC As UCEditVariables In UCList
            If UC.Selected Then
                If UC.Selected Then SelectedRow = i
                Exit For
            End If
            i += 1
        Next

        Return SelectedRow
    End Function

    Private Function GetSelectedRows() As List(Of Integer)
        Dim SelectedRows As New List(Of Integer)

        Dim i = 0
        For Each UC As UCEditVariables In UCList
            If UC.Selected Then
                If UC.Selected Then SelectedRows.Add(i)
            End If
            i += 1
        Next

        Return SelectedRows
    End Function

    Public Sub UCChanged(ChangedUC As UCEditVariables)

        Dim NeedANewRow As Boolean = True

        For Each UC As UCEditVariables In UCList
            'If ChangedUC.Selected Then
            '    If UC IsNot ChangedUC Then
            '        UC.CheckBoxSelect.Checked = False
            '    End If
            'End If
            If UC.VariableName = "" Then
                NeedANewRow = False
            End If
        Next

        If NeedANewRow Then
            AddRow()
        End If

    End Sub

    Public Sub UCSelectionChanged(ChangedUC As UCEditVariables, ModifierKeys As Windows.Forms.Keys)
        Select Case ModifierKeys
            Case Keys.None
                For Each UC As UCEditVariables In UCList
                    'If ChangedUC.Selected Then
                    '    If UC IsNot ChangedUC Then
                    '        UC.CheckBoxSelected.Checked = False
                    '    End If
                    'End If
                    UC.NotifyVariableEditor = False
                    If UC IsNot ChangedUC Then
                        'UC.CheckBoxSelected.Checked = False
                        UC.Selected = False
                    End If
                    UC.NotifyVariableEditor = True
                Next
            Case Keys.Control
                ' Nothing to do here
            Case Keys.Shift
                Dim SelectedRows As List(Of Integer) = GetSelectedRows()
                If SelectedRows.Count = 2 Then
                    Dim tmpUCList As New List(Of UCEditVariables)
                    For Each UC As UCEditVariables In UCList
                        tmpUCList.Add(UC)
                    Next
                    Dim IdxStart As Integer = SelectedRows(0)
                    Dim IdxEnd As Integer = SelectedRows(1)
                    For i As Integer = IdxStart To IdxEnd
                        tmpUCList(i).NotifyVariableEditor = False
                        tmpUCList(i).Selected = True
                        tmpUCList(i).NotifyVariableEditor = True
                    Next
                    UCList = tmpUCList
                End If
        End Select

        UCChanged(ChangedUC)

    End Sub




    Private Sub FormVariableInputEditor_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'PopulateForm()

        Dim UP As New UtilsPreferences
        Me.SavedSettingsDict = UP.GetEditVariablesSavedSettings()

        ComboBoxSavedSettings.Items.Add("")
        For Each Key As String In Me.SavedSettingsDict.Keys
            ComboBoxSavedSettings.Items.Add(Key)
        Next

        If Me.JSONString.Contains("SavedSetting:") Then 'SavedSetting:FP
            Dim Key As String = Me.JSONString.Replace("SavedSetting:", "")
            If Me.SavedSettingsDict.Keys.Contains(Key) Then
                Me.JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(Me.SavedSettingsDict(Key))
                ComboBoxSavedSettings.Text = Key
            Else
                MsgBox($"Saved setting not found: '{Key}'", vbOKOnly)
            End If
        End If

        ''Update ComboBoxSavedSettings.Text with a saved name if it exists.
        'If Me.SavedSettingsDict IsNot Nothing AndAlso Me.SavedSettingsDict.Keys.Count > 0 Then
        '    For Each SavedName As String In Me.SavedSettingsDict.Keys
        '        Dim d As Dictionary(Of String, Dictionary(Of String, String)) = Me.SavedSettingsDict(SavedName)
        '        Dim tmpJSONString As String = JsonConvert.SerializeObject(d)
        '        If tmpJSONString = Me.JSONString Then
        '            ComboBoxSavedSettings.Text = SavedName
        '            Exit For
        '        End If
        '    Next
        'End If

        PopulateForm()


    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click

        If CheckInputs() Then
            Dim UP As New UtilsPreferences
            UP.SaveEditVariablesSavedSettings(Me.SavedSettingsDict)

            Dim JSONDict As Dictionary(Of String, Dictionary(Of String, String))

            JSONDict = CreateJSONDict()

            'Me.JSONString = JsonConvert.SerializeObject(JSONDict)

            Dim SavedName As String = ComboBoxSavedSettings.Text.Trim

            Dim tmpJSONString = Newtonsoft.Json.JsonConvert.SerializeObject(JSONDict)
            If SavedName = "" Then
                Me.JSONString = tmpJSONString
            ElseIf Me.SavedSettingsDict.Keys.Contains(SavedName) Then
                If tmpJSONString = Newtonsoft.Json.JsonConvert.SerializeObject(Me.SavedSettingsDict(SavedName)) Then
                    Me.JSONString = $"SavedSetting:{SavedName}"
                Else
                    MsgBox($"The current form does not match Saved settings '{SavedName}' on file.  Please save it before continuing.")
                    Exit Sub
                End If
            Else
                MsgBox($"Saved settings does not contain '{SavedName}'.  Please save it before continuing.")
                Exit Sub
            End If

            Me.DialogResult = DialogResult.OK
        End If


    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ButtonDeleteRow_Click(sender As Object, e As EventArgs) Handles ButtonDeleteRow.Click
        'Dim SelectedRow = GetSelectedRow()
        'Dim tmpUCList As New List(Of UCEditVariables)

        'If SelectedRow = -1 Then
        '    MsgBox("No row is selected.  Select one by enabling its checkbox.")
        'Else
        '    For i = 0 To UCList.Count - 1
        '        If Not i = SelectedRow Then
        '            tmpUCList.Add(UCList(i))
        '        End If
        '    Next

        '    UCList = tmpUCList

        '    UpdateForm()
        'End If

        Dim SelectedRows = GetSelectedRows()
        Dim tmpUCList As New List(Of UCEditVariables)

        If SelectedRows.Count = 0 Then
            MsgBox("No row is selected.  Select one by enabling its checkbox.")
        Else
            For i = 0 To UCList.Count - 1
                If Not SelectedRows.Contains(i) Then
                    tmpUCList.Add(UCList(i))
                End If
            Next

            UCList = tmpUCList

            UpdateForm()
        End If

    End Sub

    Private Sub ButtonUp_Click(sender As Object, e As EventArgs) Handles ButtonUp.Click
        MoveRow("up")
    End Sub

    Private Sub ButtonDown_Click(sender As Object, e As EventArgs) Handles ButtonDown.Click
        MoveRow("down")
    End Sub

    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click, ButtonHelp2.Click
        System.Diagnostics.Process.Start(Me.HelpURL)
    End Sub

    Private Sub ButtonSaveSetting_Click(sender As Object, e As EventArgs) Handles ButtonSaveSetting.Click
        Dim Name As String = ComboBoxSavedSettings.Text
        Dim Proceed As Boolean = True

        If Name = "" Then
            Proceed = False
            MsgBox("Enter a name for these settings", vbOKOnly)
        End If

        If Proceed And ComboBoxSavedSettings.Items.Contains(Name) Then
            Dim Result = MsgBox($"Name '{Name}' already exists.  Do you want to replace it?", vbYesNo)
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