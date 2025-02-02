Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Reflection
Imports System.Security.Permissions

Namespace TestDispatchUtility
    Module DispatchUtility
        Private Const S_OK As Integer = 0
        Private Const LOCALE_SYSTEM_DEFAULT As Integer = 2048

        Function ImplementsIDispatch(ByVal obj As Object) As Boolean
            Dim result As Boolean = TypeOf obj Is IDispatchInfo
            Return result
        End Function

        <SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.UnmanagedCode)>
        Function [GetType](ByVal obj As Object, ByVal throwIfNotFound As Boolean) As Type
            RequireReference(obj, "obj")
            Dim result As Type = [GetType](CType(obj, IDispatchInfo), throwIfNotFound)
            Return result
        End Function

        <SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.UnmanagedCode)>
        Function TryGetDispId(ByVal obj As Object, ByVal name As String, <Out> ByRef dispId As Integer) As Boolean
            RequireReference(obj, "obj")
            Dim result As Boolean = TryGetDispId(CType(obj, IDispatchInfo), name, dispId)
            Return result
        End Function

        Function Invoke(ByVal obj As Object, ByVal dispId As Integer, ByVal args As Object()) As Object
            Dim memberName As String = "[DispId=" & dispId & "]"
            Dim result As Object = Invoke(obj, memberName, args)
            Return result
        End Function

        Function Invoke(ByVal obj As Object, ByVal memberName As String, ByVal args As Object()) As Object
            RequireReference(obj, "obj")
            Dim type As Type = obj.[GetType]()
            Dim result As Object = type.InvokeMember(memberName, BindingFlags.InvokeMethod Or BindingFlags.GetProperty, Nothing, obj, args, Nothing)
            Return result
        End Function

        Private Sub RequireReference(Of T As Class)(ByVal value As T, ByVal name As String)
            If value Is Nothing Then
                Throw New ArgumentNullException(name)
            End If
        End Sub

        Private Function [GetType](ByVal dispatch As IDispatchInfo, ByVal throwIfNotFound As Boolean) As Type
            RequireReference(dispatch, "dispatch")
            Dim result As Type = Nothing
            Dim typeInfoCount As Integer
            Dim hr As Integer = dispatch.GetTypeInfoCount(typeInfoCount)

            If hr = S_OK AndAlso typeInfoCount > 0 Then
                dispatch.GetTypeInfo(0, LOCALE_SYSTEM_DEFAULT, result)
            End If

            If result Is Nothing AndAlso throwIfNotFound Then
                Marshal.ThrowExceptionForHR(hr)
                Throw New TypeLoadException()
            End If

            Return result
        End Function

        Private Function TryGetDispId(ByVal dispatch As IDispatchInfo, ByVal name As String, <Out> ByRef dispId As Integer) As Boolean
            RequireReference(dispatch, "dispatch")
            RequireReference(name, "name")
            Dim result As Boolean = False
            Dim iidNull As Guid = Guid.Empty
            Dim hr As Integer = dispatch.GetDispId(iidNull, name, 1, LOCALE_SYSTEM_DEFAULT, dispId)
            '         ''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for UncheckedExpression not implemented, please report this issue in 'unchecked((int)0x80020006)' at character 6654
            '         at ICSharpCode.CodeConverter.VB.NodesVisitor.DefaultVisit(SyntaxNode node) in /home/runner/work/CodeConverter/CodeConverter/.temp/codeconverter/ICSharpCode.CodeConverter/VB/NodesVisitor.cs:line 89
            'at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitCheckedExpression(CheckedExpressionSyntax node)
            'at Microsoft.CodeAnalysis.CSharp.Syntax.CheckedExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            'at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            'at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node) in /home/runner/work/CodeConverter/CodeConverter/.temp/codeconverter/ICSharpCode.CodeConverter/VB/CommentConvertingNodesVisitor.cs:line 28
            'at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitCheckedExpression(CheckedExpressionSyntax node)
            'at Microsoft.CodeAnalysis.CSharp.Syntax.CheckedExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            'at ICSharpCode.CodeConverter.VB.CommonConversions.RemodelVariableDeclaration(VariableDeclarationSyntax declaration) in /home/runner/work/CodeConverter/CodeConverter/.temp/codeconverter/ICSharpCode.CodeConverter/VB/CommonConversions.cs:line 352
            'at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node) in /home/runner/work/CodeConverter/CodeConverter/.temp/codeconverter/ICSharpCode.CodeConverter/VB/MethodBodyVisitor.cs:line 65
            'at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            'at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node) in /home/runner/work/CodeConverter/CodeConverter/.temp/codeconverter/ICSharpCode.CodeConverter/VB/CommentConvertingMethodBodyVisitor.cs:line 27

Input:

            'Const int DISP_E_UNKNOWNNAME = unchecked((int)0x80020006); //From WinError.h
            Const DISP_E_UNKNOWNNAME = CInt(&H80020006)

            Const DISPID_UNKNOWN As Integer = -1

            If hr = S_OK Then
                result = True
            ElseIf hr = DISP_E_UNKNOWNNAME AndAlso dispId = DISPID_UNKNOWN Then
                result = False
            Else
                Marshal.ThrowExceptionForHR(hr)
            End If

            Return result
        End Function

        <ComImport>
        <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
        <Guid("00020400-0000-0000-C000-000000000046")>
        Private Interface IDispatchInfo
            <PreserveSig>
            Function GetTypeInfoCount(<Out> ByRef typeInfoCount As Integer) As Integer

            Sub GetTypeInfo(ByVal typeInfoIndex As Integer, ByVal lcid As Integer, <Out>
<MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef:=GetType(System.Runtime.InteropServices.CustomMarshalers.TypeToTypeInfoMarshaler))> ByRef typeInfo As Type)

            '            Sub GetTypeInfo(ByVal typeInfoIndex As Integer, ByVal lcid As Integer, <Out>
            '<MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef:=GetType(System.Runtime.InteropServices.))> ByRef typeInfo As Type)

            <PreserveSig>
            Function GetDispId(ByRef riid As Guid, ByRef name As String, ByVal nameCount As Integer, ByVal lcid As Integer, <Out> ByRef dispId As Integer) As Integer
        End Interface
    End Module
End Namespace
