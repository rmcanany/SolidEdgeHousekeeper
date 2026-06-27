Public Class FormPropertyFilterOptions

    Private _PropertyFilterIncludeDraftModel As Boolean
    Public Property PropertyFilterIncludeDraftModel As Boolean
        Get
            Return _PropertyFilterIncludeDraftModel
        End Get
        Set(value As Boolean)
            _PropertyFilterIncludeDraftModel = value
            If Me.CheckBoxPropertyFilterIncludeDraftModel IsNot Nothing Then
                CheckBoxPropertyFilterIncludeDraftModel.Checked = value
            End If
        End Set
    End Property

    Private _PropertyFilterIncludeDraftItself As Boolean
    Public Property PropertyFilterIncludeDraftItself As Boolean
        Get
            Return _PropertyFilterIncludeDraftItself
        End Get
        Set(value As Boolean)
            _PropertyFilterIncludeDraftItself = value
            If Me.CheckBoxPropertyFilterIncludeDraftItself IsNot Nothing Then
                CheckBoxPropertyFilterIncludeDraftItself.Checked = value
            End If
        End Set
    End Property

    Private Sub CheckBoxPropertyFilterIncludeDraftModel_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPropertyFilterIncludeDraftModel.CheckedChanged
        Me.PropertyFilterIncludeDraftModel = CheckBoxPropertyFilterIncludeDraftModel.Checked
    End Sub

    Private Sub CheckBoxPropertyFilterIncludeDraftItself_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPropertyFilterIncludeDraftItself.CheckedChanged
        Me.PropertyFilterIncludeDraftItself = CheckBoxPropertyFilterIncludeDraftItself.Checked
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click

        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel

    End Sub

    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
        Dim UD As New UtilsDocumentation
        Dim Tag = "filtering"
        Diagnostics.Process.Start(UD.GenerateVersionURL(Tag))
    End Sub
End Class