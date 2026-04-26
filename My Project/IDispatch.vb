' Copied from https://github.com/SolidEdgeCommunity/SolidEdge.Community/tree/master/src/SolidEdge.Community/Runtime/InteropServices/ComTypes
Option Strict On

'Imports System.Runtime.InteropServices
'Imports System.Runtime.InteropServices.ComTypes

<Runtime.InteropServices.ComImport>
<Runtime.InteropServices.Guid("00020400-0000-0000-C000-000000000046")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown)>
Interface IDispatch
    Function GetTypeInfoCount() As Integer
    Function GetTypeInfo(ByVal iTInfo As Integer, ByVal lcid As Integer) As Runtime.InteropServices.ComTypes.ITypeInfo
End Interface
