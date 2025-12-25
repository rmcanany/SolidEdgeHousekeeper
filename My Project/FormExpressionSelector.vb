Option Strict On

Public Class FormExpressionSelector
    'Public Property SavedExpressionsItems As Dictionary(Of String, String)
    'Dim SavedExpressionsDict As Dictionary(Of String, Dictionary(Of String, String))
    Dim SavedExpressions As HCSavedExpressions
    Public Property SavedExpressionName As String
    Public Property SavedExpresssionLanguage As String
    Public Property OutputText As String

    Private Sub FormExpressionSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SavedExpressions = Form_Main.SavedExpressions

        For Each tmpSE As SavedExpression In SavedExpressions.Items
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

        Dim tmpSE As SavedExpression = SavedExpressions.GetSavedExpression(Me.SavedExpressionName)
        If tmpSE IsNot Nothing Then
            Me.SavedExpresssionLanguage = tmpSE.Language
        Else
            Me.SavedExpresssionLanguage = "VB"
        End If

        Me.DialogResult = DialogResult.OK
    End Sub
End Class