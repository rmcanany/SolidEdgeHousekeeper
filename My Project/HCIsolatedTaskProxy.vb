' Copy of Jason Newell's code translated to VB.Net
' Original code: https://github.com/SolidEdgeCommunity/SolidEdge.Community/blob/master/src/SolidEdge.Community/IsolatedTaskProxy.cs
' https://www.informit.com/articles/article.aspx?p=170719&seqNum=5

Option Strict On

'Imports System.Runtime.InteropServices
'Imports System.Runtime.Remoting

Public MustInherit Class HCIsolatedTaskProxy
    Inherits MarshalByRefObject

    Private _application As SolidEdgeFramework.Application
    Private _document As SolidEdgeFramework.SolidEdgeDocument

    Public NotOverridable Overrides Function InitializeLifetimeService() As Object
        Return Nothing
    End Function

    ' A genuinely wedged 'edge.exe' (still running, but its message loop has
    ' stopped responding) never returns a synchronous Solid Edge automation call
    ' on its own.  Every task's Process() runs its Solid Edge calls on the STA
    ' thread started below, so a plain, untimed thread.Join() here hangs the
    ' whole batch forever.  Give it a generous window, and if it's still not
    ' back, kill 'edge.exe' so the RPC call unblocks (or the thread is simply
    ' abandoned) and let the caller's existing exception handling recover -
    ' UtilsExecute already relaunches Solid Edge after a caught exception.
    '
    ' User-configurable (Form_Main.TaskTimeoutSeconds / TextBoxTaskTimeoutSeconds)
    ' since a legitimately long-running task (eg. Check Interference on a large
    ' assembly) can't be told apart from a genuine hang by wall-clock time alone -
    ' UtilsExecute.ProcessAll copies the user's setting into every Task before a
    ' run starts.  180000ms (3 minutes) here is only the fallback default.
    Public Property STAThreadTimeoutMilliseconds As Integer = 180000

    Private Sub JoinOrRecover(thread As System.Threading.Thread)
        If thread.Join(STAThreadTimeoutMilliseconds) Then Return

        Try
            For Each Proc As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName("edge")
                Proc.Kill()
            Next
        Catch
        End Try

        Throw New System.Exception("Solid Edge stopped responding and was terminated.")
    End Sub

    Protected Sub InvokeSTAThread(ByVal target As Action)
        If target Is Nothing Then Throw New ArgumentNullException("target")
        Dim exception As Exception = Nothing
        Dim thread = New System.Threading.Thread(
            Sub()
                Try
                    target()
                Catch ex As System.Exception
                    exception = ex
                End Try
            End Sub)
        thread.SetApartmentState(System.Threading.ApartmentState.STA)
        thread.Start()
        JoinOrRecover(thread)

        If exception IsNot Nothing Then
            Throw New System.Exception("An unhandled exception has occurred. See inner exception for details.", exception)
        End If
    End Sub

    Protected Sub InvokeSTAThread(Of TArg1)(ByVal target As Action(Of TArg1), ByVal arg1 As TArg1)
        If target Is Nothing Then Throw New ArgumentNullException("target")
        Dim exception As Exception = Nothing
        Dim thread = New System.Threading.Thread(
            Sub()
                Try
                    target(arg1)
                Catch ex As System.Exception
                    exception = ex
                End Try
            End Sub)
        thread.SetApartmentState(System.Threading.ApartmentState.STA)
        thread.Start()
        JoinOrRecover(thread)

        If exception IsNot Nothing Then
            Throw New System.Exception("An unhandled exception has occurred. See inner exception for details.", exception)
        End If
    End Sub

    Protected Sub InvokeSTAThread(Of TArg1, TArg2)(ByVal target As Action(Of TArg1, TArg2), ByVal arg1 As TArg1, ByVal arg2 As TArg2)
        If target Is Nothing Then Throw New ArgumentNullException("target")
        Dim exception As Exception = Nothing
        Dim thread = New System.Threading.Thread(
            Sub()
                Try
                    target(arg1, arg2)
                Catch ex As System.Exception
                    exception = ex
                End Try
            End Sub)
        thread.SetApartmentState(System.Threading.ApartmentState.STA)
        thread.Start()
        JoinOrRecover(thread)

        If exception IsNot Nothing Then
            Throw New System.Exception("An unhandled exception has occurred. See inner exception for details.", exception)
        End If
    End Sub

    Protected Sub InvokeSTAThread(Of TArg1, TArg2, TArg3)(ByVal target As Action(Of TArg1, TArg2, TArg3), ByVal arg1 As TArg1, ByVal arg2 As TArg2, ByVal arg3 As TArg3)
        If target Is Nothing Then Throw New ArgumentNullException("target")
        Dim exception As Exception = Nothing
        Dim thread = New System.Threading.Thread(
            Sub()
                Try
                    target(arg1, arg2, arg3)
                Catch ex As System.Exception
                    exception = ex
                End Try
            End Sub)
        thread.SetApartmentState(System.Threading.ApartmentState.STA)
        thread.Start()
        JoinOrRecover(thread)

        If exception IsNot Nothing Then
            Throw New System.Exception("An unhandled exception has occurred. See inner exception for details.", exception)
        End If
    End Sub

    Protected Sub InvokeSTAThread(Of TArg1, TArg2, TArg3, TArg4)(ByVal target As Action(Of TArg1, TArg2, TArg3, TArg4), ByVal arg1 As TArg1, ByVal arg2 As TArg2, ByVal arg3 As TArg3, ByVal arg4 As TArg4)
        If target Is Nothing Then Throw New ArgumentNullException("target")
        Dim exception As Exception = Nothing
        Dim thread = New System.Threading.Thread(
            Sub()
                Try
                    target(arg1, arg2, arg3, arg4)
                Catch ex As System.Exception
                    exception = ex
                End Try
            End Sub)
        thread.SetApartmentState(System.Threading.ApartmentState.STA)
        thread.Start()
        JoinOrRecover(thread)

        If exception IsNot Nothing Then
            Throw New System.Exception("An unhandled exception has occurred. See inner exception for details.", exception)
        End If
    End Sub

    Protected Function InvokeSTAThread(Of TResult)(ByVal target As Func(Of TResult)) As TResult
        If target Is Nothing Then Throw New ArgumentNullException("target")
        Dim returnValue As TResult = Nothing
        Dim exception As Exception = Nothing
        Dim thread = New System.Threading.Thread(
            Sub()
                Try
                    returnValue = target()
                Catch ex As System.Exception
                    exception = ex
                End Try
            End Sub)
        thread.SetApartmentState(System.Threading.ApartmentState.STA)
        thread.Start()
        JoinOrRecover(thread)

        If exception IsNot Nothing Then
            Throw New System.Exception("An unhandled exception has occurred. See inner exception for details.", exception)
        End If

        Return returnValue
    End Function

    Protected Function InvokeSTAThread(Of TArg1, TResult)(ByVal target As Func(Of TArg1, TResult), ByVal arg1 As TArg1) As TResult
        If target Is Nothing Then Throw New ArgumentNullException("target")
        Dim returnValue As TResult = Nothing
        Dim exception As Exception = Nothing
        Dim thread = New System.Threading.Thread(
            Sub()
                Try
                    returnValue = target(arg1)
                Catch ex As System.Exception
                    exception = ex
                End Try
            End Sub)
        thread.SetApartmentState(System.Threading.ApartmentState.STA)
        thread.Start()
        JoinOrRecover(thread)

        If exception IsNot Nothing Then
            Throw New System.Exception("An unhandled exception has occurred. See inner exception for details.", exception)
        End If

        Return returnValue
    End Function

    Protected Function InvokeSTAThread(Of TArg1, TArg2, TResult)(ByVal target As Func(Of TArg1, TArg2, TResult), ByVal arg1 As TArg1, ByVal arg2 As TArg2) As TResult
        If target Is Nothing Then Throw New ArgumentNullException("target")
        Dim returnValue As TResult = Nothing
        Dim exception As Exception = Nothing
        Dim thread = New System.Threading.Thread(
            Sub()
                Try
                    returnValue = target(arg1, arg2)
                Catch ex As System.Exception
                    exception = ex
                End Try
            End Sub)
        thread.SetApartmentState(System.Threading.ApartmentState.STA)
        thread.Start()
        JoinOrRecover(thread)

        If exception IsNot Nothing Then
            Throw New System.Exception("An unhandled exception has occurred. See inner exception for details.", exception)
        End If

        Return returnValue
    End Function

    Protected Function InvokeSTAThread(Of TArg1, TArg2, TArg3, TResult)(ByVal target As Func(Of TArg1, TArg2, TArg3, TResult), ByVal arg1 As TArg1, ByVal arg2 As TArg2, ByVal arg3 As TArg3) As TResult
        If target Is Nothing Then Throw New ArgumentNullException("target")
        Dim returnValue As TResult = Nothing
        Dim exception As Exception = Nothing
        Dim thread = New System.Threading.Thread(
            Sub()
                Try
                    returnValue = target(arg1, arg2, arg3)
                Catch ex As System.Exception
                    exception = ex
                End Try
            End Sub)
        thread.SetApartmentState(System.Threading.ApartmentState.STA)
        thread.Start()
        JoinOrRecover(thread)

        If exception IsNot Nothing Then
            Throw New System.Exception("An unhandled exception has occurred. See inner exception for details.", exception)
        End If

        Return returnValue
    End Function

    Protected Function InvokeSTAThread(Of TArg1, TArg2, TArg3, TArg4, TResult)(ByVal target As Func(Of TArg1, TArg2, TArg3, TArg4, TResult), ByVal arg1 As TArg1, ByVal arg2 As TArg2, ByVal arg3 As TArg3, ByVal arg4 As TArg4) As TResult
        If target Is Nothing Then Throw New ArgumentNullException("target")
        Dim returnValue As TResult = Nothing
        Dim exception As Exception = Nothing
        Dim thread = New System.Threading.Thread(
            Sub()
                Try
                    returnValue = target(arg1, arg2, arg3, arg4)
                Catch ex As System.Exception
                    exception = ex
                End Try
            End Sub)
        thread.SetApartmentState(System.Threading.ApartmentState.STA)
        thread.Start()
        JoinOrRecover(thread)

        If exception IsNot Nothing Then
            Throw New System.Exception("An unhandled exception has occurred. See inner exception for details.", exception)
        End If

        Return returnValue
    End Function

    Public Property Application As SolidEdgeFramework.Application
        Get
            Return _application
        End Get
        Set(ByVal value As SolidEdgeFramework.Application)
            _application = UnwrapRuntimeCallableWrapper(Of SolidEdgeFramework.Application)(value)
        End Set
    End Property

    Public Property Document As SolidEdgeFramework.SolidEdgeDocument
        Get
            Return _document
        End Get
        Set(ByVal value As SolidEdgeFramework.SolidEdgeDocument)
            _document = UnwrapRuntimeCallableWrapper(Of SolidEdgeFramework.SolidEdgeDocument)(value)
        End Set
    End Property

    Protected Function UnwrapRuntimeCallableWrapper(Of TInterface As Class)(ByVal rcw As Object) As TInterface
        If Runtime.Remoting.RemotingServices.IsTransparentProxy(rcw) Then

            If Runtime.InteropServices.Marshal.IsComObject(rcw) Then
                Dim punk As IntPtr = Runtime.InteropServices.Marshal.GetIUnknownForObject(rcw)

                Try
                    Return CType(Runtime.InteropServices.Marshal.GetObjectForIUnknown(punk), TInterface)
                Finally
                    Runtime.InteropServices.Marshal.Release(punk)
                End Try
            Else
                Throw New Runtime.InteropServices.InvalidComObjectException()
            End If
        End If

        Return TryCast(rcw, TInterface)
    End Function
End Class

