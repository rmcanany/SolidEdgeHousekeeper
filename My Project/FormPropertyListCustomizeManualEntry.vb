Option Strict On

Public Class FormPropertyListCustomizeManualEntry
    Public Property Propname As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Propname = TextBoxPropname.Text
        If Me.Propname = "" Then
            MsgBox("Name cannot be blank", vbOKOnly)
        Else
            Me.DialogResult = DialogResult.OK
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub
End Class