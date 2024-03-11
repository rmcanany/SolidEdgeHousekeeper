Public Class ExTableLayoutPanel
    ' https://www.vbforums.com/showthread.php?600989-Slow-resizing-of-a-TableLayoutPanel

    Inherits TableLayoutPanel

    Public Property Task As Task

    Public Sub New()
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        Me.Task = Nothing
    End Sub

    Public Sub New(Task As Task)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        Me.Task = Task
    End Sub

End Class