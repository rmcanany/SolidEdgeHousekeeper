Public Class FormEditTaskList

    Public Property ManuallySelectFileTypes As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub



    Private Sub CheckBoxAutoSelectFiletypes_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxManuallySelectFiletypes.CheckedChanged
        Dim CheckBox = CType(sender, CheckBox)
        Me.ManuallySelectFileTypes = CheckBox.Checked
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub FormEditTaskList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckBoxManuallySelectFiletypes.Checked = Me.ManuallySelectFileTypes
    End Sub
End Class