
Imports System.Runtime.InteropServices
    Imports System.Threading

    Public Class OleMessageFilter
        Implements IOleMessageFilter

        Public Shared Sub Register()
            Dim newFilter As IOleMessageFilter = New OleMessageFilter()
            Dim oldFilter As IOleMessageFilter = Nothing
            Dim iRetVal As Integer

            If (Thread.CurrentThread.GetApartmentState() = ApartmentState.STA) Then
                iRetVal = CoRegisterMessageFilter(newFilter, oldFilter)
            Else
                Throw New COMException("Unable to register message filter because the current thread apartment state is not STA.")
            End If

        End Sub

        Public Shared Sub Revoke()
            Dim oldFilter As IOleMessageFilter = Nothing
            CoRegisterMessageFilter(Nothing, oldFilter)
        End Sub

        Private Function HandleInComingCall(ByVal dwCallType As Integer,
                                            ByVal hTaskCaller As System.IntPtr,
                                            ByVal dwTickCount As Integer,
                                            ByVal lpInterfaceInfo As System.IntPtr) _
                                            As Integer Implements IOleMessageFilter.HandleInComingCall

            Return SERVERCALL.SERVERCALL_ISHANDLED
        End Function

        Private Function RetryRejectedCall(ByVal hTaskCallee As System.IntPtr,
                                           ByVal dwTickCount As Integer,
                                           ByVal dwRejectType As Integer) _
                                           As Integer Implements IOleMessageFilter.RetryRejectedCall

            If dwRejectType = SERVERCALL.SERVERCALL_RETRYLATER Then
                Return 99
            End If

            Return -1
        End Function

        Private Function MessagePending(ByVal hTaskCallee As System.IntPtr,
                                        ByVal dwTickCount As Integer,
                                        ByVal dwPendingType As Integer) _
                                        As Integer Implements IOleMessageFilter.MessagePending

            Return PENDINGMSG.PENDINGMSG_WAITDEFPROCESS
        End Function

        <DllImport("Ole32.dll")>
        Private Shared Function CoRegisterMessageFilter(ByVal newFilter As IOleMessageFilter,
                                                        ByRef oldFilter As IOleMessageFilter) _
                                                        As Integer
        End Function
    End Class

    Enum SERVERCALL
        SERVERCALL_ISHANDLED = 0
        SERVERCALL_REJECTED = 1
        SERVERCALL_RETRYLATER = 2
    End Enum

    Enum PENDINGMSG
        PENDINGMSG_CANCELCALL = 0
        PENDINGMSG_WAITNOPROCESS = 1
        PENDINGMSG_WAITDEFPROCESS = 2
    End Enum

    <ComImport(),
    Guid("00000016-0000-0000-C000-000000000046"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)>
    Interface IOleMessageFilter
        <PreserveSig()>
        Function HandleInComingCall(ByVal dwCallType As Integer,
                                    ByVal hTaskCaller As IntPtr,
                                    ByVal dwTickCount As Integer,
                                    ByVal lpInterfaceInfo As IntPtr) _
                                    As Integer

        <PreserveSig()>
        Function RetryRejectedCall(ByVal hTaskCallee As IntPtr,
                                   ByVal dwTickCount As Integer,
                                   ByVal dwRejectType As Integer) _
                                   As Integer

        <PreserveSig()>
        Function MessagePending(ByVal hTaskCallee As IntPtr,
                                ByVal dwTickCount As Integer,
                                ByVal dwPendingType As Integer) _
                                As Integer
    End Interface

