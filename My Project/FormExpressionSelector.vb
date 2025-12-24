Option Strict On

Public Class FormExpressionSelector
    'Public Property SavedExpressionsItems As Dictionary(Of String, String)
    'Dim SavedExpressionsDict As Dictionary(Of String, Dictionary(Of String, String))
    Dim SavedExpressions As HCSavedExpressions
    Public Property SavedExpressionName As String
    Public Property SavedExpresssionLanguage As String
    Public Property OutputText As String

    Private Sub FormExpressionSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'SavedExpressionsItems = New Dictionary(Of String, String)

        Dim UP As New UtilsPreferences
        'SavedExpressionsDict = UP.GetSavedExpressionsDict
        SavedExpressions = Form_Main.SavedExpressions

        'If SavedExpressionsDict.Keys.Count > 0 Then
        '    For Each SavedName In SavedExpressionsDict.Keys
        '        ComboBoxExpressionNames.Items.Add(SavedName)
        '    Next
        'End If
        For Each tmpSE As SavedExpression In SavedExpressions.Items
            'DD_SavedExpressions.DropDownItems.Add(tmpSE.Name)
            ComboBoxExpressionNames.Items.Add(tmpSE.Name)
        Next
        ComboBoxExpressionNames.Text = ComboBoxExpressionNames.Items(0).ToString

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.SavedExpressionName = ComboBoxExpressionNames.Text
        Me.OutputText = $"SavedSetting:{Me.SavedExpressionName}"

        'If Me.SavedExpressionsDict.Keys.Contains(Me.SavedExpressionName) Then
        '    Me.SavedExpresssionLanguage = SavedExpressionsDict(Me.SavedExpressionName)("Language")
        '    Me.OutputText = $"SavedSetting:{Me.SavedExpressionName}"

        '    'If Me.SavedExpressionsItems(Me.SavedExpressionName).ToLower.Contains("return") Then
        '    '    Me.SavedExpresssionLanguage = "VB"
        '    'Else
        '    '    Me.SavedExpresssionLanguage = "NCalc"
        '    'End If
        'End If

        Me.DialogResult = DialogResult.OK
    End Sub
End Class