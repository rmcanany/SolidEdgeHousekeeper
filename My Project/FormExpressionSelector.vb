Option Strict On

Public Class FormExpressionSelector
    Public Property SavedExpressionsItems As Dictionary(Of String, String)
    Public Property SavedExpressionName As String
    Public Property SavedExpresssionLanguage As String

    Private Sub FormExpressionSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SavedExpressionsItems = New Dictionary(Of String, String)

        Dim UP As New UtilsPreferences
        Dim PreferencesDirectory = UP.GetPreferencesDirectory()

        Dim SavedExpressionsFilename = UP.GetSavedExpressionsFilename()

        Dim SR As IO.StreamReader = IO.File.OpenText(SavedExpressionsFilename)
        Dim SavedExpressions = SR.ReadToEnd

        Dim Expressions = SavedExpressions.Split(New String() {"[EXP]"}, StringSplitOptions.RemoveEmptyEntries)

        For Each Expression In Expressions

            Dim ExpressionItems = Expression.Split(New String() {"[EXP_TEXT]"}, StringSplitOptions.RemoveEmptyEntries)
            If ExpressionItems.Length = 2 Then
                ComboBoxExpressionNames.Items.Add(ExpressionItems(0).Replace(vbCrLf, ""))
                SavedExpressionsItems.Add(ExpressionItems(0).Replace(vbCrLf, ""), ExpressionItems(1).Replace(vbCrLf, Chr(182)))
            End If
        Next

        SR.Close()

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.SavedExpressionName = ComboBoxExpressionNames.Text

        If Me.SavedExpressionsItems.Keys.Contains(Me.SavedExpressionName) Then
            If Me.SavedExpressionsItems(Me.SavedExpressionName).ToLower.Contains("return") Then
                Me.SavedExpresssionLanguage = "VB"
            Else
                Me.SavedExpresssionLanguage = "NCalc"
            End If
        End If

        Me.DialogResult = DialogResult.OK
    End Sub
End Class