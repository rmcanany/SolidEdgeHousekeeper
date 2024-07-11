Option Strict On

Public Class FormEditInteractively

    Public Property UseCountdownTimer As Boolean = False
    Public Property PauseTime As Double = 0
    Public Property RunCommands As Boolean = False
    Public Property CommandIDAssembly As Integer = 0
    Public Property CommandIDPart As Integer = 0
    Public Property CommandIDSheetmetal As Integer = 0
    Public Property CommandIDDraft As Integer = 0
    Public Property FormX As Integer = 0
    Public Property FormY As Integer = 0
    Public Property SEApp As SolidEdgeFramework.Application = Nothing
    Public Property SaveAfterTimeout As Boolean = False

    Private Property Paused As Boolean = False
    Private Property LabelChars As Integer = 80
    Public Property Filetype As String = ""

    Private WithEvents AlwaysOnTopTimer As Windows.Forms.Timer
    Private WithEvents Timer As Windows.Forms.Timer
    Private Countdown As Integer
    Private CountdownOriginal As Integer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub FormEditInteractively_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AlwaysOnTopTimer = New Windows.Forms.Timer()
        AlwaysOnTopTimer.Interval = 1000
        AlwaysOnTopTimer.Start()

        If Me.UseCountdownTimer Then
            PauseButton.Visible = True
            Label1.Visible = True
            Timer = New Windows.Forms.Timer()
            Timer.Interval = 100
            Timer.Start()
            CountdownOriginal = CInt(Me.PauseTime * 1000 / Timer.Interval)
            Countdown = CountdownOriginal

        Else
            PauseButton.Enabled = False
            Label1.Enabled = False
        End If

        If Me.RunCommands Then
            Select Case Me.Filetype.ToLower
                Case "asm"
                    If Not Me.CommandIDAssembly = 0 Then
                        SEApp.StartCommand(CType(Me.CommandIDAssembly, SolidEdgeFramework.SolidEdgeCommandConstants))
                    End If
                Case "par"
                    If Not Me.CommandIDPart = 0 Then
                        SEApp.StartCommand(CType(Me.CommandIDPart, SolidEdgeFramework.SolidEdgeCommandConstants))
                    End If
                Case "psm"
                    If Not Me.CommandIDSheetmetal = 0 Then
                        SEApp.StartCommand(CType(Me.CommandIDSheetmetal, SolidEdgeFramework.SolidEdgeCommandConstants))
                    End If
                Case "dft"
                    If Not Me.CommandIDDraft = 0 Then
                        SEApp.StartCommand(CType(Me.CommandIDDraft, SolidEdgeFramework.SolidEdgeCommandConstants))
                    End If
            End Select
        End If

    End Sub

    Private Sub PauseButton_Click(sender As Object, e As EventArgs) Handles PauseButton.Click
        Me.Paused = True
        PauseButton.BackColor = Color.Orange
        PauseButton.Text = "Paused"
        Label1.Visible = False
        Timer.Stop()

    End Sub

    Private Sub Timer_Tick(sender As System.Object, e As System.EventArgs) Handles Timer.Tick

        Countdown -= 1
        Dim FractionRemaining = Countdown / CountdownOriginal
        Label1.Text = Microsoft.VisualBasic.StrDup(CInt(LabelChars * FractionRemaining), "|")

        If Countdown <= 0 Then
            Timer.Stop()
            If Me.SaveAfterTimeout Then
                Me.DialogResult = DialogResult.Yes
            Else
                Me.DialogResult = DialogResult.No
            End If
        End If
    End Sub

    Private Sub AlwaysOnTopTimer_Tick(sender As System.Object, e As System.EventArgs) Handles AlwaysOnTopTimer.Tick
        'Me.Activate()
        Me.TopMost = False
        Me.TopMost = True
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Me.DialogResult = DialogResult.Yes
    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles DoNotSaveButton.Click
        Me.DialogResult = DialogResult.No
    End Sub

    Private Sub AbortButton_Click(sender As Object, e As EventArgs) Handles AbortButton.Click
        Me.DialogResult = DialogResult.Abort
    End Sub

End Class