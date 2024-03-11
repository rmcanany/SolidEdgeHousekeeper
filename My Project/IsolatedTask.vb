' Copy of Jason Newell's code translated to VB.Net
' Original code: https://github.com/SolidEdgeCommunity/SolidEdge.Community/blob/master/src/SolidEdge.Community/IsolatedTask.cs
' Abstract classes primer https://www.informit.com/articles/article.aspx?p=170719&seqNum=5

Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports System.Text


Public NotInheritable Class IsolatedTask(Of T As IsolatedTaskProxy)
    Implements IDisposable

    Private _proxyType As Type = Nothing
    Private _appDomain As AppDomain = Nothing
    Private _proxy As T = Nothing

    Public Sub New()
        _proxyType = GetType(T)
        _appDomain = AppDomain.CreateDomain(String.Format("{0} AppDomain", _proxyType.Name), Nothing, AppDomain.CurrentDomain.SetupInformation)
        _proxy = CType(_appDomain.CreateInstanceAndUnwrap(_proxyType.Assembly.FullName, _proxyType.FullName), T)
    End Sub

    Public Sub New(ExistingInstance As T)

        ' ExistingInstance is an instance with the desired options set.
        ' Here, _proxy creates a new instance of this class.
        ' The ExistingInstance constructor populates the new instance
        ' with the options from the one passed in.

        _proxyType = GetType(T)
        _appDomain = AppDomain.CreateDomain(String.Format("{0} AppDomain", _proxyType.Name), Nothing, AppDomain.CurrentDomain.SetupInformation)
        '_proxy = CType(_appDomain.CreateInstanceAndUnwrap(_proxyType.Assembly.FullName, _proxyType.FullName), T)

        'https://learn.microsoft.com/en-us/dotnet/api/system.appdomain.createinstance?view=netframework-4.8#System_AppDomain_CreateInstance_System_String_System_String_System_Boolean_System_Reflection_BindingFlags_System_Reflection_Binder_System_Object___System_Globalization_CultureInfo_System_Object___

        Dim args As Object() = {ExistingInstance}
        _proxy = CType(
            _appDomain.CreateInstanceAndUnwrap(
                _proxyType.Assembly.FullName,
                _proxyType.FullName,
                False,
                0,
                Nothing,
                args,
                Nothing,
                Nothing), T)

    End Sub

    Private Sub Dispose() Implements IDisposable.Dispose
        If _appDomain IsNot Nothing Then
            AppDomain.Unload(_appDomain)
        End If

        _proxy = Nothing
        _appDomain = Nothing
        _proxyType = Nothing
    End Sub

    Public ReadOnly Property Proxy As T
        Get
            Return _proxy
        End Get
    End Property

End Class

