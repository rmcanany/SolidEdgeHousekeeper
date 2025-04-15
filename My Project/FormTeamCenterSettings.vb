Public Class FormTeamCenterSettings
    Private Property FMain As Form_Main

    Private _TCItemIDRx As String
    Public Property TCItemIDRx As String
        Get
            Return _TCItemIDRx
        End Get
        Set(value As String)
            _TCItemIDRx = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                TextBoxTCItemIDRx.Text = value
            End If
        End Set
    End Property

    Private _TCRevisionRx As String
    Public Property TCRevisionRx As String
        Get
            Return _TCRevisionRx
        End Get
        Set(value As String)
            _TCRevisionRx = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                TextBoxTCRevisionRx.Text = value
            End If
        End Set
    End Property


    Public Sub New(_FMain As Form_Main)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.FMain = _FMain
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub TextBoxTCItemIDRx_TextChanged(sender As Object, e As EventArgs) Handles TextBoxTCItemIDRx.TextChanged
        Me.TCItemIDRx = TextBoxTCItemIDRx.Text
    End Sub

    Private Sub TextBoxTCRevisionRx_TextChanged(sender As Object, e As EventArgs) Handles TextBoxTCRevisionRx.TextChanged
        Me.TCRevisionRx = TextBoxTCRevisionRx.Text
    End Sub

    Private Sub FormTeamCenterSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TCItemIDRx = Me.FMain.TCItemIDRx
        Me.TCRevisionRx = Me.FMain.TCRevisionRx
    End Sub
End Class