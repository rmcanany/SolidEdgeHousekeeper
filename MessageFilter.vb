Imports System.Runtime.InteropServices

'''<summary>
''' Class containing the IOleMessageFilter
''' thread error-handling functions.
'''</summary>
Friend Class MessageFilter
    Implements IOleMessageFilter

    '''<summary>Start the filter.</summary>
    Public Shared Sub Register()
        Dim newFilter As IOleMessageFilter = New MessageFilter()
        Dim oldFilter As IOleMessageFilter = Nothing
        CoRegisterMessageFilter(newFilter, oldFilter)
    End Sub

    '''<summary>Done with the filter, close it.</summary>
    Public Shared Sub Revoke()
        Dim oldFilter As IOleMessageFilter = Nothing
        CoRegisterMessageFilter(Nothing, oldFilter)
    End Sub


    ' IOleMessageFilter functions.

    '''<summary>Handle incoming thread requests.</summary>
    Private Function HandleInComingCall(ByVal dwCallType As Integer, ByVal hTaskCaller As System.IntPtr, ByVal dwTickCount As Integer, ByVal lpInterfaceInfo As System.IntPtr) As Integer Implements IOleMessageFilter.HandleInComingCall
        'Return the flag SERVERCALL_ISHANDLED.
        Return 0
    End Function

    '''<summary>Thread call was rejected, so try again.</summary>
    Private Function RetryRejectedCall(ByVal hTaskCallee As System.IntPtr, ByVal dwTickCount As Integer, ByVal dwRejectType As Integer) As Integer Implements IOleMessageFilter.RetryRejectedCall
        If dwRejectType = 2 Then
            ' flag = SERVERCALL_RETRYLATER.
            ' Retry the thread call immediately if return >=0 & 
            ' <100.
            Return 99
        End If
        ' Too busy; cancel call.
        Return -1
    End Function

    Private Function MessagePending(ByVal hTaskCallee As System.IntPtr, ByVal dwTickCount As Integer, ByVal dwPendingType As Integer) As Integer Implements IOleMessageFilter.MessagePending
        'Return the flag PENDINGMSG_WAITDEFPROCESS.
        Return 2
    End Function

    '''<summary>Implement the IOleMessageFilter interface.</summary>
    <DllImport("Ole32.dll")> _
    Private Shared Function CoRegisterMessageFilter(ByVal newFilter As IOleMessageFilter, ByRef oldFilter As IOleMessageFilter) As Integer
    End Function
End Class


<ComImport(), Guid("00000016-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
Interface IOleMessageFilter
    <PreserveSig()> _
    Function HandleInComingCall(ByVal dwCallType As Integer, ByVal hTaskCaller As IntPtr, ByVal dwTickCount As Integer, ByVal lpInterfaceInfo As IntPtr) As Integer

    <PreserveSig()> _
    Function RetryRejectedCall(ByVal hTaskCallee As IntPtr, ByVal dwTickCount As Integer, ByVal dwRejectType As Integer) As Integer

    <PreserveSig()> _
    Function MessagePending(ByVal hTaskCallee As IntPtr, ByVal dwTickCount As Integer, ByVal dwPendingType As Integer) As Integer
End Interface
