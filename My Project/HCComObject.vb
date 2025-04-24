
' Copied from https://github.com/SolidEdgeCommunity/SolidEdge.Community/tree/master/src/SolidEdge.Community/Runtime/InteropServices

Option Strict On

Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.ComTypes

' For this to work correctly, the Solid Edge Interop assemblies must have their
' Embed Interop Types set to False, which in turn sets Copy Local to True.  
' 
' Embedded Interops only have definitions for items actually used in the assembly.
' This function needs definitions for all items, which can only happen with
' Interops copied locally.
'
' Access this option on the Project > Properties > References dialog.  Select the
' Interop, then on its Properties page, set Embed Interop Types to False.

Public Class HCComObject
    Const LOCALE_SYSTEM_DEFAULT As Integer = 2048

    Public Shared Function GetPropertyValue(Of T)(ByVal comObject As Object, ByVal name As String) As T
        If Marshal.IsComObject(comObject) = False Then Throw New InvalidComObjectException()
        Dim type = comObject.[GetType]()
        Dim value = type.InvokeMember(name, System.Reflection.BindingFlags.GetProperty, Nothing, comObject, Nothing)
        Return CType(value, T)
    End Function

    Public Shared Function GetPropertyValue(Of T)(ByVal comObject As Object, ByVal name As String, ByVal defaultValue As T) As T
        If Marshal.IsComObject(comObject) = False Then Throw New InvalidComObjectException()
        Dim type = comObject.[GetType]()

        Try
            Dim value = type.InvokeMember(name, System.Reflection.BindingFlags.GetProperty, Nothing, comObject, Nothing)
            Return CType(value, T)
        Catch
            Return defaultValue
        End Try
    End Function

    Public Shared Function GetCOMObjectType(ByVal comObject As Object) As Type
        If Marshal.IsComObject(comObject) = False Then Throw New InvalidComObjectException()
        Dim type As Type = Nothing
        Dim dispatch = TryCast(comObject, IDispatch)
        Dim typeInfo As ITypeInfo = Nothing
        Dim pTypeAttr = IntPtr.Zero
        Dim typeAttr As System.Runtime.InteropServices.ComTypes.TYPEATTR = Nothing

        Try

            If dispatch IsNot Nothing Then
                typeInfo = dispatch.GetTypeInfo(0, LOCALE_SYSTEM_DEFAULT)
                typeInfo.GetTypeAttr(pTypeAttr)
                typeAttr = CType(Marshal.PtrToStructure(
                                     pTypeAttr,
                                     GetType(System.Runtime.InteropServices.ComTypes.TYPEATTR)),
                                     System.Runtime.InteropServices.ComTypes.TYPEATTR)

                Dim assemblies = AppDomain.CurrentDomain.GetAssemblies()

                For Each assembly In assemblies
                    type = assembly.GetTypes().Where(Function(x) x.IsInterface).Where(Function(x) x.GUID.Equals(typeAttr.guid)).FirstOrDefault()

                    If type IsNot Nothing Then
                        Exit For
                    End If
                Next
            End If

        Finally

            If typeInfo IsNot Nothing Then
                typeInfo.ReleaseTypeAttr(pTypeAttr)
                Marshal.ReleaseComObject(typeInfo)
            End If
        End Try

        Return type
    End Function
End Class
