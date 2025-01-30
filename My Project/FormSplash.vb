Public Class FormSplash

    Public Sub Animate()
        Dim DelayTime As Integer = 125

        PictureBox1.Image = My.Resources.splash_logo_02
        Application.DoEvents()
        System.Threading.Thread.Sleep(DelayTime)

        PictureBox1.Image = My.Resources.splash_logo_01
        Application.DoEvents()
        System.Threading.Thread.Sleep(DelayTime)

        PictureBox1.Image = My.Resources.splash_logo_02
        Application.DoEvents()
        System.Threading.Thread.Sleep(DelayTime)

        PictureBox1.Image = My.Resources.splash_logo_01
        Application.DoEvents()
        System.Threading.Thread.Sleep(DelayTime)

        PictureBox1.Image = My.Resources.splash_logo_02
        Application.DoEvents()
        System.Threading.Thread.Sleep(DelayTime)

    End Sub

    Public Sub UpdateStatus(Message As String)
        TextBoxStatus.Text = String.Format(" {0}", Message)
        Application.DoEvents()
    End Sub

End Class