Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.ComTypes
Imports System.Text

<ComImport>
<Guid("00020400-0000-0000-C000-000000000046")>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
Interface IDispatch
    Function GetTypeInfoCount() As Integer
    Function GetTypeInfo(ByVal iTInfo As Integer, ByVal lcid As Integer) As ITypeInfo
End Interface
