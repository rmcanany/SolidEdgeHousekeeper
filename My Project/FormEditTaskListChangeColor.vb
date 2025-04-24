Option Strict On

Public Class FormEditTaskListChangeColor
    Public Property ColorHue As String
    Public Property ColorSaturation As Double
    Public Property ColorBrightness As Double
    Public Property ColorR As Integer
    Public Property ColorG As Integer
    Public Property ColorB As Integer


    Private Sub UpdateColor()
        Dim R As Integer = 0
        Dim G As Integer = 0
        Dim B As Integer = 0

        Select Case Me.ColorHue
            Case "Red"
                R = 255
                G = CInt(255 * (1 - Me.ColorSaturation))
                B = CInt(255 * (1 - Me.ColorSaturation))
            Case "Green"
                R = CInt(255 * (1 - Me.ColorSaturation))
                G = 255
                B = CInt(255 * (1 - Me.ColorSaturation))
            Case "Blue"
                R = CInt(255 * (1 - Me.ColorSaturation))
                G = CInt(255 * (1 - Me.ColorSaturation))
                B = 255
            Case "Cyan"
                R = CInt(255 * (1 - Me.ColorSaturation))
                G = 255
                B = 255
            Case "Magenta"
                R = 255
                G = CInt(255 * (1 - Me.ColorSaturation))
                B = 255
            Case "Yellow"
                R = 255
                G = 255
                B = CInt(255 * (1 - Me.ColorSaturation))
            Case "White"
                R = 255
                G = 255
                B = 255
            Case "Orange"
                R = 255
                G = CInt(127 + 127 * (1 - Me.ColorSaturation))
                B = CInt(255 * (1 - Me.ColorSaturation))
            Case "Purple"
                R = CInt(127 + 127 * (1 - Me.ColorSaturation))
                G = CInt(255 * (1 - Me.ColorSaturation))
                B = 255
        End Select

        R = CInt(R * Me.ColorBrightness)
        G = CInt(G * Me.ColorBrightness)
        B = CInt(B * Me.ColorBrightness)

        Me.ColorR = R
        Me.ColorG = G
        Me.ColorB = B

        ButtonColor.BackColor = Color.FromArgb(R, G, B)
    End Sub


    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Dim Proceed As Boolean = True
        Dim s As String = ""

        Me.ColorHue = ComboBoxColor.Text

        Try
            Me.ColorSaturation = CDbl(NumericUpDownSaturation.Value)
            If (Me.ColorSaturation < 0) Or (Me.ColorSaturation > 1) Then
                Proceed = False
                s = String.Format("{0}{1}{2}", s, "Saturation must be a number between 0 and 1", vbCrLf)
            End If
        Catch ex As Exception
            Proceed = False
            s = String.Format("{0}{1}{2}", s, "Saturation must be a number between 0 and 1", vbCrLf)
        End Try

        Try
            Me.ColorBrightness = CDbl(NumericUpDownBrightness.Value)
            If (Me.ColorBrightness < 0) Or (Me.ColorBrightness > 1) Then
                Proceed = False
                s = String.Format("{0}{1}{2}", s, "Brightness must be a number between 0 and 1", vbCrLf)
            End If
        Catch ex As Exception
            Proceed = False
            s = String.Format("{0}{1}{2}", s, "Brightness must be a number between 0 and 1", vbCrLf)
        End Try

        If Proceed Then
            Me.DialogResult = DialogResult.OK
        Else
            MsgBox(s)
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub FormEditTaskListChangeColor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ColorHueList As List(Of String) = "Red Green Blue Cyan Magenta Yellow White Orange Purple".Split(" "c).ToList
        For Each s As String In ColorHueList
            ComboBoxColor.Items.Add(s)
        Next
        ComboBoxColor.Text = Me.ColorHue
        NumericUpDownSaturation.Value = CDec(Me.ColorSaturation)
        NumericUpDownBrightness.Value = CDec(Me.ColorBrightness)
    End Sub

    Private Sub ComboBoxColor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxColor.SelectedIndexChanged
        Me.ColorHue = ComboBoxColor.Text
        UpdateColor()

    End Sub

    Private Sub NumericUpDownSaturation_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownSaturation.ValueChanged
        Try
            Me.ColorSaturation = CDbl(NumericUpDownSaturation.Value)
            UpdateColor()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub NumericUpDownBrightness_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownBrightness.ValueChanged
        Try
            Me.ColorBrightness = CDbl(NumericUpDownBrightness.Value)
            UpdateColor()
        Catch ex As Exception
        End Try
    End Sub
End Class