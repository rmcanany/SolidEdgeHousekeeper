Option Strict On

'Imports System.Runtime.InteropServices

Public Class UtilsSEApp


    Public Property SEApp As SolidEdgeFramework.Application
    Private Property FMain As Form_Main
    Private Property PreviousProcessDraftsInactive As Object  ' In reality this is a boolean

    Private Property EdgeProcess As Diagnostics.Process
    Public Property ErrorLogger As Logger
    Private Property CurrentlyOpenFiles As List(Of String)
    Private Property CurrentlyActiveFile As String


    Public Sub New(_FMain As Form_Main)
        Me.FMain = _FMain
        'Me.ErrorLogger = _ErrorLogger

        Me.EdgeProcess = New Diagnostics.Process
        Me.CurrentlyOpenFiles = New List(Of String)
    End Sub


    Public Sub SEStart(
        RunInBackground As Boolean,
        UseCurrentSession As Boolean,
        NoUpdateMRU As Boolean,
        ProcessDraftsInactive As Boolean)

        FMain.TextBoxStatus.Text = "Starting Solid Edge..."
        System.Windows.Forms.Application.DoEvents()
        Dim NoCurrentSessionFound As Boolean = False

        Me.SEApp = Nothing  ' Don't carry a stale reference forward if a step below is skipped.

        If SEIsRunning() Then
            Me.EdgeProcess = GetEdgeProcess()
            If Me.EdgeProcess Is Nothing Then
                ErrorLogger.AddMessage("Unable to obtain process 'edge.exe'")
            End If
        Else
            NoCurrentSessionFound = True

            If FMain.SEFastLaunch Then
                Dim SEInstallData As New SEInstallDataLib.SEInstallData
                Dim InstalledPath As String = SEInstallData.GetInstalledPath  ' eg 'C:\Program Files\Siemens\Solid Edge 2025\Program'
                Me.EdgeProcess.StartInfo.FileName = $"{InstalledPath}\edge.exe"
                Me.EdgeProcess.StartInfo.UseShellExecute = False
                Me.EdgeProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            Else
                Me.EdgeProcess.StartInfo.FileName = "edge.exe"
                Me.EdgeProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            End If

            Try
                EdgeProcess.Start()
            Catch ex As Exception
                ErrorLogger.AddMessage($"Unable to start process '{Me.EdgeProcess.StartInfo.FileName}'.  Exception: {ex.Message}")
            End Try
        End If

        If EdgeProcess IsNot Nothing Then
            Me.SEApp = Nothing
            Dim SleepTime As Integer = 100
            Dim WaitTime As Integer = 0
            Dim MaxWaitTime As Integer = 30000
            While SEApp Is Nothing
                If WaitTime >= MaxWaitTime Then Exit While
                Try
                    Dim tmpSEApp = CType(GetObject(, "SolidEdge.Application"), SolidEdgeFramework.Application)

                    ' GetObject can return a stale ROT entry for a process that has
                    ' already died (eg. after a forced Process.Kill following an
                    ' unresponsive-Solid-Edge timeout) without throwing.  A real call
                    ' is needed to confirm the object is actually alive.
                    Dim Probe = tmpSEApp.Version

                    SEApp = tmpSEApp
                Catch ex As Exception
                    WaitTime += SleepTime
                    Threading.Thread.Sleep(SleepTime)
                End Try
            End While
        End If

        If Me.SEApp Is Nothing Then
            ErrorLogger.AddMessage("Unable to connect to Solid Edge")
        Else
            Try
                'Me.FMain.TopMost = True
                Me.FMain.Activate()
                Windows.Forms.Application.DoEvents()

                'Threading.Thread.Sleep(1000)
                ' Turn off popups.
                SEApp.DisplayAlerts = False

                ' Disable Most Recently Used list updating if option is set.
                If NoUpdateMRU Then
                    SEApp.SuspendMRU()
                End If

                ' Set foreground/background processing options
                If RunInBackground Then
                    SEApp.Visible = False
                    SEApp.DelayCompute = True
                    SEApp.Interactive = False
                    SEApp.ScreenUpdating = False

                    'This belongs in UtilsExecute for assembly files, probably not for the InteractiveEdit command
                    'Or in each task that accepts assembly files and might benefit from the setting.
                    'assemblyDocument.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
                Else
                    SEApp.Visible = True
                    SEApp.DelayCompute = False
                    SEApp.Interactive = True
                    SEApp.ScreenUpdating = True

                    If UseCurrentSession Then
                        If NoCurrentSessionFound Then
                            SEApp.WindowState = 2  'Maximizes Solid Edge
                        Else
                            ' Should leave it in its existing state
                        End If
                    Else
                        SEApp.WindowState = 2
                    End If

                    'This belongs in UtilsExecute for assembly files, probably not for the InteractiveEdit command
                    'Or in each task that accepts assembly files and might benefit from the setting.
                    'assemblyDocument.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
                End If

                ' For ProcessDraftsInactive, need to remember the previous setting
                Dim Param = SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalSessionDraftOpenInactive
                SEApp.GetGlobalParameter(Param, Me.PreviousProcessDraftsInactive)
                SEApp.SetGlobalParameter(Param, ProcessDraftsInactive)

                ' Save currently open document names, if any.
                Me.CurrentlyOpenFiles.Clear() '<--- reset between sessions
                If Not SEApp.Documents.Count = 0 And Not RunInBackground Then
                    Dim Docs As SolidEdgeFramework.Documents = SEApp.Documents
                    Dim ActiveDoc As SolidEdgeFramework.SolidEdgeDocument = CType(SEApp.ActiveDocument, SolidEdgeFramework.SolidEdgeDocument)

                    For Each Doc As SolidEdgeFramework.SolidEdgeDocument In Docs
                        Me.CurrentlyOpenFiles.Add(Doc.FullName)
                        ActiveDoc = CType(SEApp.ActiveDocument, SolidEdgeFramework.SolidEdgeDocument)
                        If Doc Is ActiveDoc Then
                            Me.CurrentlyActiveFile = Doc.FullName
                        End If
                    Next
                End If
                'SEApp.DisplayAlerts = True  ' Needed this one time when using a new license

            Catch ex As Exception
                ' The liveness probe above isn't a hard guarantee - Solid Edge could
                ' still die in the narrow window between that check and here.  Treat
                ' it the same as never having connected, rather than letting the
                ' exception escape uncaught.
                ErrorLogger.AddMessage($"Solid Edge became unresponsive while starting.  Exception: {ex.Message}")
                Me.SEApp = Nothing
            End Try
        End If

    End Sub

    Public Sub SEStop(UseCurrentSession As Boolean)

        System.Threading.Thread.Sleep(1000) ' Might need a little time for the COM object to become disconneted

        Dim SEAppNotResponsive As Boolean = False

        Try
            If SEApp IsNot Nothing Then SEApp.DisplayAlerts = True  ' Needed when returning to an interactive SE session.
        Catch ex As Exception
            SEAppNotResponsive = True
        End Try

        If Not UseCurrentSession Or SEAppNotResponsive Then

            FMain.TextBoxStatus.Text = "Closing Solid Edge..."
            If SEApp IsNot Nothing Then

                Try
                    Dim Param = SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalSessionDraftOpenInactive
                    SEApp.SetGlobalParameter(Param, Me.PreviousProcessDraftsInactive)
                    SEApp.DoIdle()

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

        Else
            RestoreOpenDocuments()
        End If

    End Sub

    ''' <summary>
    ''' Closes a document, guarding against Solid Edge having crashed or become
    ''' unresponsive.  When 'edge.exe' has died mid-task, SEDoc.Close() talks to a
    ''' dead RPC endpoint and blocks forever instead of throwing, so the call is
    ''' run with a timeout and the process is terminated if it doesn't return.
    ''' </summary>
    Public Function CloseDocumentSafely(SEDoc As SolidEdgeFramework.SolidEdgeDocument, TimeoutMilliseconds As Integer) As Boolean

        If Not EdgeProcessIsAlive() Then
            ErrorLogger.AddMessage("Solid Edge is no longer running.  Skipping document close.")
            Return False
        End If

        Dim CloseTask As Threading.Tasks.Task = Threading.Tasks.Task.Run(Sub() SEDoc.Close(False))

        ' Observe any exception on the background thread (eg. if 'edge.exe' is
        ' killed below while the call is still in flight) so it doesn't surface
        ' later as an unobserved task exception.
        CloseTask.ContinueWith(Sub(t)
                                   Dim swallow = t.Exception
                               End Sub, Threading.Tasks.TaskContinuationOptions.OnlyOnFaulted)

        If CloseTask.Wait(TimeoutMilliseconds) Then Return True

        ErrorLogger.AddMessage("Solid Edge stopped responding while closing a document.  Terminating 'edge.exe'.")
        SEKillProcess("edge")
        SEApp = Nothing
        Return False

    End Function

    Public Function EdgeProcessIsAlive() As Boolean
        Try
            Return Me.EdgeProcess IsNot Nothing AndAlso Not Me.EdgeProcess.HasExited
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub RestoreOpenDocuments()
        Dim ActiveDocument As SolidEdgeFramework.SolidEdgeDocument = Nothing
        Dim tmpDocument As SolidEdgeFramework.SolidEdgeDocument

        If Me.CurrentlyOpenFiles IsNot Nothing Then
            For Each Filename As String In Me.CurrentlyOpenFiles
                tmpDocument = CType(SEApp.Documents.Open(Filename), SolidEdgeFramework.SolidEdgeDocument)
                If Filename = Me.CurrentlyActiveFile Then ActiveDocument = tmpDocument
                SEApp.DoIdle()
            Next
            If ActiveDocument IsNot Nothing Then ActiveDocument.Activate()
            SEApp.DoIdle()
        End If
    End Sub

    Private Sub SEGarbageCollect(ByVal obj As Object)
        Try
            '******* Added because of .NET
            If Not (obj Is Nothing) Then
                'Marshal.ReleaseComObject(obj)
                Runtime.InteropServices.Marshal.FinalReleaseComObject(obj)
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

    Private Function GetEdgeProcess() As Diagnostics.Process
        Dim tmpEdgeProcess As Diagnostics.Process = Nothing

        Dim LocalProcs As Process()
        'Dim Proc As Process
        'Dim i As Integer
        'Dim blnProcessTerminated As Boolean
        'blnProcessTerminated = False

        LocalProcs = System.Diagnostics.Process.GetProcesses
        For Each Proc As Process In LocalProcs
            If Proc.ProcessName.ToUpper = "EDGE" Then
                tmpEdgeProcess = Proc
                Exit For
            End If
        Next

        Return tmpEdgeProcess
    End Function

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

        Dim tmpSEApp As SolidEdgeFramework.Application = Nothing
        Try
            tmpSEApp = CType(GetObject(, "SolidEdge.Application"), SolidEdgeFramework.Application)

            ' GetObject can return a stale ROT entry for a process that has already
            ' died (eg. after a forced Process.Kill following an unresponsive-Solid-
            ' Edge timeout) without throwing.  A real call is needed to confirm the
            ' object is actually alive.
            Dim Probe = tmpSEApp.Version
        Catch ex As Exception
            Return False
        End Try

        Return tmpSEApp IsNot Nothing

    End Function

    Public Function DMIsRunning() As Boolean
        Dim DMApp As RevisionManager.Application = Nothing
        Dim IsRunning As Boolean = False

        Try
            DMApp = CType(GetObject(, "RevisionManager.Application"), RevisionManager.Application)
            IsRunning = DMApp IsNot Nothing
        Catch ex As Exception
            IsRunning = False
        Finally
            If DMApp IsNot Nothing Then
                Try
                    Runtime.InteropServices.Marshal.FinalReleaseComObject(DMApp)
                Catch ex As Exception
                End Try
                DMApp = Nothing
            End If
        End Try

        If Not IsRunning Then IsRunning = DMProcessIsRunning()

        Return IsRunning

    End Function

    Public Function DMForceClose() As Boolean

        If Not DMIsRunning() Then Return True

        Dim DMApp As RevisionManager.Application = Nothing

        ' First request a normal shutdown with alerts disabled.  This usually
        ' closes the application without needing to terminate its process.
        Try
            DMApp = CType(GetObject(, "RevisionManager.Application"), RevisionManager.Application)
            DMApp.DisplayAlerts = 0
            DMApp.Quit()
        Catch ex As Exception
            ' If the COM server is not responding, the process-level fallback
            ' below will attempt to terminate it.
        Finally
            If DMApp IsNot Nothing Then
                Try
                    Runtime.InteropServices.Marshal.FinalReleaseComObject(DMApp)
                Catch ex As Exception
                End Try
                DMApp = Nothing
            End If
        End Try

        If WaitForDesignManagerToExit(1500) Then Return True

        ' Fallback: terminate the Design Manager / Revision Manager process.
        ' Process names have changed between Solid Edge releases, therefore
        ' the match also checks the window title and executable description.
        For Each Proc As Process In System.Diagnostics.Process.GetProcesses()
            Try
                If IsDesignManagerProcess(Proc) Then
                    Proc.Kill()
                    Proc.WaitForExit(5000)
                End If
            Catch ex As Exception
                ' Check the final running state below.  The existing start
                ' condition will block processing if termination failed.
            Finally
                Proc.Dispose()
            End Try
        Next

        Return WaitForDesignManagerToExit(5000)

    End Function

    Private Function WaitForDesignManagerToExit(TimeoutMilliseconds As Integer) As Boolean

        Dim Stopwatch As Diagnostics.Stopwatch = Diagnostics.Stopwatch.StartNew()

        Do
            If Not DMIsRunning() Then Return True
            Threading.Thread.Sleep(100)
        Loop While Stopwatch.ElapsedMilliseconds < TimeoutMilliseconds

        Return Not DMIsRunning()

    End Function

    Private Function IsDesignManagerProcess(Proc As Process) As Boolean

        Dim ProcessName As String = ""
        Dim WindowTitle As String = ""
        Dim FileDescription As String = ""

        Try
            ProcessName = Proc.ProcessName.ToLowerInvariant()
        Catch ex As Exception
        End Try

        If {"revisionmanager", "revmanager", "revman", "designmanager", "designmgr", "sedesignmanager", "sedesignmgr"}.Contains(ProcessName) Then
            Return True
        End If

        Try
            WindowTitle = Proc.MainWindowTitle.ToLowerInvariant()
        Catch ex As Exception
        End Try

        If WindowTitle.Contains("solid edge design manager") OrElse WindowTitle.Contains("solid edge revision manager") Then
            Return True
        End If

        Try
            FileDescription = Proc.MainModule.FileVersionInfo.FileDescription
            If FileDescription Is Nothing Then FileDescription = ""
            FileDescription = FileDescription.ToLowerInvariant()
        Catch ex As Exception
        End Try

        Return FileDescription.Contains("solid edge design manager") OrElse
               FileDescription.Contains("solid edge revision manager")

    End Function

    Private Function DMProcessIsRunning() As Boolean

        For Each Proc As Process In System.Diagnostics.Process.GetProcesses()
            Try
                If IsDesignManagerProcess(Proc) Then Return True
            Catch ex As Exception
            Finally
                Proc.Dispose()
            End Try
        Next

        Return False

    End Function

    Public Function GetEdgeProcessMemoryUsage() As Long
        'Return Me.EdgeProcess.HandleCount
        Return Me.EdgeProcess.WorkingSet64
    End Function

End Class
