Option Strict On

Imports System.Runtime.InteropServices

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

        If SEIsRunning() Then
            Me.EdgeProcess = GetEdgeProcess()
            If Me.EdgeProcess Is Nothing Then
                ErrorLogger.AddMessage("Unable to obtain process 'edge.exe'")
            End If
        Else
            NoCurrentSessionFound = True
            Me.EdgeProcess.StartInfo.FileName = "edge.exe"
            Me.EdgeProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            Try
                EdgeProcess.Start()
            Catch ex As Exception
                ErrorLogger.AddMessage($"Unable to start process '{Me.EdgeProcess.StartInfo.FileName}'")
                ErrorLogger.AddMessage($"Error was: '{ex.Message}'")
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
                    SEApp = CType(GetObject(, "SolidEdge.Application"), SolidEdgeFramework.Application)
                Catch ex As Exception
                    WaitTime += SleepTime
                    Threading.Thread.Sleep(SleepTime)
                End Try
            End While
        End If

        If Me.SEApp Is Nothing Then
            ErrorLogger.AddMessage("Unable to connect to Solid Edge")
        Else
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

                'assemblyDocument.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
            End If

            ' For ProcessDraftsInactive, need to remember the previous setting
            Dim Param = SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalSessionDraftOpenInactive
            SEApp.GetGlobalParameter(Param, Me.PreviousProcessDraftsInactive)
            SEApp.SetGlobalParameter(Param, ProcessDraftsInactive)

            ' Save currently open document names, if any.
            If Not SEApp.Documents.Count = 0 Then
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
        End If

        'If Not SEIsRunning() Then
        '    Dim P As New Diagnostics.Process
        '    P.StartInfo.FileName = "edge.exe"
        '    'P.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        '    Try
        '        P.Start()

        '    Catch ex As Exception
        '        Dim k = 0
        '    End Try

        '    Dim Count As Integer = 0
        '    SEApp = Nothing
        '    Dim WaitTime As Integer = 1000
        '    Dim MaxWaitTime As Integer = 60000
        '    While SEApp Is Nothing
        '        If Count >= MaxWaitTime Then Exit While
        '        Try
        '            SEApp = CType(GetObject(, "SolidEdge.Application"), SolidEdgeFramework.Application)
        '        Catch ex As Exception
        '            Count += WaitTime
        '            Threading.Thread.Sleep(WaitTime)

        '        End Try
        '    End While
        '    Try
        '        SEApp.DoIdle()
        '    Catch ex As Exception
        '        Dim j = 0
        '    End Try
        '    P.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
        '    'P.Kill()
        '    SEApp.Quit()
        '    SEApp = Nothing
        '    Dim i = 0
        'End If

        'Try

        '    'If UseCurrentSession Then
        '    '    Try
        '    '        SEApp = CType(GetObject(, "SolidEdge.Application"), SolidEdgeFramework.Application)
        '    '    Catch ex As Exception
        '    '        SEApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
        '    '        FMain.ActiveFile = ""
        '    '        FMain.ActiveFiles = New List(Of String)
        '    '        NoCurrentSessionFound = True
        '    '    End Try
        '    'Else
        '    '    SEApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
        '    'End If

        '    Try
        '        SEApp = CType(GetObject(, "SolidEdge.Application"), SolidEdgeFramework.Application)
        '    Catch ex As Exception
        '        SEApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
        '        FMain.ActiveFile = ""
        '        FMain.ActiveFiles = New List(Of String)
        '        NoCurrentSessionFound = True
        '    End Try

        '    ' Turn off popups.
        '    SEApp.DisplayAlerts = False

        '    ' Disable Most Recently Used list updating if option is set.
        '    If NoUpdateMRU Then
        '        SEApp.SuspendMRU()
        '    End If

        '    ' Set foreground/background processing options
        '    If RunInBackground Then
        '        SEApp.DelayCompute = True
        '        SEApp.Interactive = False
        '        SEApp.ScreenUpdating = False
        '        SEApp.Visible = False
        '        'assemblyDocument.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        '    Else
        '        SEApp.DelayCompute = False
        '        SEApp.Interactive = True
        '        SEApp.ScreenUpdating = True
        '        SEApp.Visible = True

        '        If UseCurrentSession Then
        '            If NoCurrentSessionFound Then
        '                SEApp.WindowState = 2  'Maximizes Solid Edge
        '            End If
        '        Else
        '            SEApp.WindowState = 2
        '        End If

        '        'assemblyDocument.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        '    End If

        '    ' For ProcessDraftsInactive, need to remember the previous setting
        '    Dim Param = SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalSessionDraftOpenInactive
        '    SEApp.GetGlobalParameter(Param, Me.PreviousProcessDraftsInactive)
        '    SEApp.SetGlobalParameter(Param, ProcessDraftsInactive)

        '    'SEApp.DisplayAlerts = True  ' Needed this one time when using a new license

        'Catch ex As Exception
        '    Dim s As String
        '    s = String.Format("Could not start Solid Edge.  Exiting...{0}", vbCrLf)
        '    s = String.Format("{0}Exception:{1}", s, vbCrLf)
        '    s = String.Format("{0}{1}", s, ex.ToString)
        '    MsgBox(s)
        '    End
        'End Try

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
        Catch ex As Exception
        End Try

        Return tmpSEApp IsNot Nothing

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
