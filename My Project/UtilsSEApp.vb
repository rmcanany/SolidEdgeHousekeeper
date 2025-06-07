Option Strict On

Imports System.Runtime.InteropServices

Public Class UtilsSEApp


    Public Property SEApp As SolidEdgeFramework.Application
    Private Property FMain As Form_Main


    Public Sub New(_FMain As Form_Main)
        Me.FMain = _FMain
    End Sub


    Public Sub SEStart(RunInBackground As Boolean,
                        UseCurrentSession As Boolean,
                        NoUpdateMRU As Boolean)

        FMain.TextBoxStatus.Text = "Starting Solid Edge..."

        Try

            If UseCurrentSession Then
                Try
                    SEApp = CType(GetObject(, "SolidEdge.Application"), SolidEdgeFramework.Application)
                Catch ex As Exception
                    SEApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
                End Try
            Else
                SEApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
            End If

            ' Turn off popups.
            SEApp.DisplayAlerts = False

            ' Disable Most Recently Used list updating if option is set.
            If NoUpdateMRU Then
                SEApp.SuspendMRU()
            End If

            ' Set foreground/background processing options
            If RunInBackground Then
                SEApp.DelayCompute = True
                SEApp.Interactive = False
                SEApp.ScreenUpdating = False
                SEApp.Visible = False
                'assemblyDocument.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
            Else
                SEApp.DelayCompute = False
                SEApp.Interactive = True
                SEApp.ScreenUpdating = True
                SEApp.Visible = True
                SEApp.WindowState = 2  'Maximizes Solid Edge
                'assemblyDocument.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
            End If
            'SEApp.DisplayAlerts = True  ' Needed this one time when using a new license
        Catch ex As Exception
            Dim s As String
            s = String.Format("Could not start Solid Edge.  Exiting...{0}", vbCrLf)
            s = String.Format("{0}Exception:{1}", s, vbCrLf)
            s = String.Format("{0}{1}", s, ex.ToString)
            MsgBox(s)
            End
        End Try

    End Sub

    Public Sub SEStop(UseCurrentSession As Boolean)

        If Not UseCurrentSession Then

            FMain.TextBoxStatus.Text = "Closing Solid Edge..."
            If (Not (SEApp Is Nothing)) Then
                Try
                    SEApp.Quit()
                Catch ex As Exception
                    SEKillProcess("edge")
                End Try
            End If
            SEGarbageCollect(SEApp)
            System.Threading.Thread.Sleep(100)

            If Not SEApp Is Nothing Then
                SEApp = Nothing
            End If

            System.Threading.Thread.Sleep(100)

        End If

    End Sub

    Private Sub SEGarbageCollect(ByVal obj As Object)
        Try
            '******* Added because of .NET
            If Not (obj Is Nothing) Then
                'Marshal.ReleaseComObject(obj)
                Marshal.FinalReleaseComObject(obj)
            End If

            GC.Collect(GC.MaxGeneration)
            GC.WaitForPendingFinalizers()
            GC.Collect(GC.MaxGeneration)
            GC.WaitForPendingFinalizers()
            '******* Added because of .NET
        Catch ex As Exception
            obj = Nothing
            GC.Collect(GC.MaxGeneration)
            GC.WaitForPendingFinalizers()
            GC.Collect(GC.MaxGeneration)
            GC.WaitForPendingFinalizers()

        End Try
    End Sub

    Private Function SEKillProcess(ByVal Name As String) As Long

        Dim LocalProcs As Process()
        Dim Proc As Process
        Dim i As Integer
        Dim blnProcessTerminated As Boolean
        blnProcessTerminated = False

        LocalProcs = System.Diagnostics.Process.GetProcesses
        For Each Proc In LocalProcs
            If UCase(Proc.ProcessName) = UCase(Name) Then
                Try
                    Proc.Kill()
                    SEKillProcess = 0
                    blnProcessTerminated = True
                Catch ex As System.Exception
                    SEKillProcess = -1
                    LocalProcs = Nothing
                    Exit Function
                End Try
            End If
            i += 1
        Next

        If blnProcessTerminated = True Then
            SEKillProcess = 0
            Exit Function
        End If

        If blnProcessTerminated = False Then
            SEKillProcess = -2
            Exit Function
        End If

        SEKillProcess = -1

    End Function

    Public Function SEIsRunning() As Boolean
        Try
            SEApp = CType(GetObject(, "SolidEdge.Application"), SolidEdgeFramework.Application)
        Catch ex As Exception
        End Try

        If Not SEApp Is Nothing Then
            SEApp = Nothing
            Return True
        Else
            Return False
        End If
    End Function

    Public Function DMIsRunning() As Boolean
        Dim DMApp As RevisionManager.Application = Nothing

        Try
            DMApp = CType(GetObject(, "RevisionManager.Application"), RevisionManager.Application)
        Catch ex As Exception
        End Try

        If Not DMApp Is Nothing Then
            DMApp = Nothing
            Return True
        Else
            Return False
        End If

    End Function

End Class
