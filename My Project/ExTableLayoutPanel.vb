Imports System.Runtime.InteropServices

Public Class ExTableLayoutPanel
    ''''' https://www.vbforums.com/showthread.php?600989-Slow-resizing-of-a-TableLayoutPanel

    ' https://stackoverflow.com/questions/8900099/tablelayoutpanel-responds-very-slowly-to-events

    Inherits TableLayoutPanel

    Public Property Task As Task

    Public Sub New()
        'Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        'Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        Me.Task = Nothing
    End Sub

    Public Sub New(Task As Task)
        'Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        'Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        Me.Task = Task
    End Sub



    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.CacheText, True)
        'Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.CacheText Or ControlStyles.AllPaintingInWmPaint, True)
    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or NativeMethods.WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    Public Sub BeginUpdate()
        NativeMethods.SendMessage(Me.Handle, NativeMethods.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero)
    End Sub

    Public Sub EndUpdate()
        NativeMethods.SendMessage(Me.Handle, NativeMethods.WM_SETREDRAW, New IntPtr(1), IntPtr.Zero)
        Parent.Invalidate(True)
    End Sub



End Class

Module NativeMethods
    Public WM_SETREDRAW As Integer = &HB
    Public WS_EX_COMPOSITED As Integer = &H2000000
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr

    End Function

End Module