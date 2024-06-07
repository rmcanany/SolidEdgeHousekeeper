Imports ICSharpCode.TextEditor.Util
Imports PanoramicData.NCalcExtensions
Imports SolidEdgePart
Imports System.IO
Imports System.Text.RegularExpressions

Public Class FormNCalc

    Public Formula As String
    Dim SavedExpressionsItems As New Dictionary(Of String, String)
    Dim SavedParameters As New Dictionary(Of String, String)

    Private Sub FormNCalc_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        Formula = TextEditorNCalc.Text.Replace(vbCrLf, "")

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

            Dim tmpVal As String = ""

            If SavedParameters.ContainsKey(Parameter) Then
                tmpVal = SavedParameters.Item(Parameter)
            End If

            tmpVal = InputBox("Insert value for parameter: " & Parameter,, tmpVal)

            If SavedParameters.ContainsKey(Parameter) Then
                SavedParameters.Item(Parameter) = tmpVal
            Else
                SavedParameters.Add(Parameter, tmpVal)
            End If

            calculation = calculation.Replace(Parameter, tmpVal)

        Next

        Dim nCalcExpression As New ExtendedExpression(calculation)

        TextEditorResult.Clear()

        Try
            Dim A = nCalcExpression.Evaluate()
            Dim p As String = vbCrLf & vbCrLf & "Parameters list" & vbCrLf & "---------------"

            For Each tmpPar In Parameters

                p += vbCrLf & tmpPar & ": " & SavedParameters(tmpPar)

            Next

            TextEditorResult.Clear()
            TextEditorResult.Text = "Expression result: " & A & p

        Catch ex As Exception

            TextEditorResult.Clear()
            TextEditorResult.Text = ex.Message

        End Try

    End Sub

    Private Sub FormNCalc_Load(sender As Object, e As EventArgs) Handles Me.Load


        Dim SR As IO.StreamReader = IO.File.OpenText(Application.StartupPath() & "\SavedExpressions.txt")
        Dim SavedExpressions = SR.ReadToEnd

        Dim Expressions = SavedExpressions.Split(New String() {"[EXP]"}, StringSplitOptions.RemoveEmptyEntries)

        For Each Expression In Expressions

            Dim ExpressionItems = Expression.Split(New String() {"[EXP_TEXT]"}, StringSplitOptions.RemoveEmptyEntries)
            If ExpressionItems.Length = 2 Then
                DD_SavedExpressions.DropDownItems.Add(ExpressionItems(0).Replace(vbCrLf, ""))
                SavedExpressionsItems.Add(ExpressionItems(0).Replace(vbCrLf, ""), ExpressionItems(1))
            End If

        Next

    End Sub

    Private Sub DD_SavedExpressions_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles DD_SavedExpressions.DropDownItemClicked

        Dim tmpItem As ToolStripDropDownItem = e.ClickedItem
        TextEditorNCalc.Clear()
        TextEditorNCalc.Text = SavedExpressionsItems.Item(tmpItem.Text)

    End Sub

    Private Sub BT_Help_Click(sender As Object, e As EventArgs) Handles BT_Help.Click

        Dim webAddress As String = "https://github.com/panoramicdata/PanoramicData.NCalcExtensions"
        Process.Start(webAddress)

    End Sub

End Class