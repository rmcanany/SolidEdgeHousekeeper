Imports PanoramicData.NCalcExtensions
Imports SolidEdgePart
Imports System.Text.RegularExpressions

Public Class FormNCalc

    Public Formula As String

    Private Sub FormNCalc_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        Formula = TextEditorNCalc.Text

    End Sub

    Private Sub TextEditorNCalc_DoubleClick(sender As Object, e As EventArgs) Handles TextEditorNCalc.DoubleClick



    End Sub

    Private Sub BT_Test_Click(sender As Object, e As EventArgs) Handles BT_Test.Click

        Dim calculation As String = TextEditorNCalc.Text

        Dim Pattern As String = "%{[^}]*}"
        Dim Matches As MatchCollection = Regex.Matches(calculation, Pattern)
        Dim Parameters As New List(Of String)

        For Each MatchString In Matches
            If Not Parameters.Contains(MatchString.Value) Then Parameters.Add(MatchString.Value)
        Next

        For Each Parameter In Parameters
            Dim tmpVal As String = InputBox("Insert value for parameter: " & Parameter)
            calculation = calculation.Replace(Parameter, tmpVal)
        Next

        Dim nCalcExpression As New ExtendedExpression(calculation)

        TextEditorResult.Clear()

        Try
            Dim A = nCalcExpression.Evaluate()
            TextEditorResult.Text = A
        Catch ex As Exception

            TextEditorResult.Text = ex.Message

        End Try

    End Sub

End Class