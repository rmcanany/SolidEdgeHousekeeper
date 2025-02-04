
'Imports SolidEdgeCommunity.Runtime.InteropServices.ComTypes
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.ComTypes

Public Class HCComObject
    Const LOCALE_SYSTEM_DEFAULT As Integer = 2048

    Public Shared Function GetITypeInfo(ByVal comObject As Object) As ITypeInfo
        If Marshal.IsComObject(comObject) = False Then Throw New InvalidComObjectException()
        Dim dispatch = TryCast(comObject, IDispatch)

        If dispatch IsNot Nothing Then
            Return dispatch.GetTypeInfo(0, LOCALE_SYSTEM_DEFAULT)
        End If

        Return Nothing
    End Function

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
        Dim typeAttr = Nothing
        Dim count As Integer

        Try

            If dispatch IsNot Nothing Then
                count = dispatch.GetTypeInfoCount
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
