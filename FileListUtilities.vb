Option Strict On

Public Class FileListUtilities
    Public Property ListViewFiles As ListView

    Public Sub New(ListViewFiles As ListView)
        Me.ListViewFiles = ListViewFiles
    End Sub


    Public Function GetSourceDirectories() As List(Of String)
        Dim SourceDirectories As New List(Of String)
        Dim TagName As String
        Dim s As String

        For Each Item As ListViewItem In ListViewFiles.Items 'L-istBoxFiles.Items

            'ListViewFiles.BeginUpdate()

            If Item.Group.Name = "Sources" Then

                TagName = CType(Item.Tag, String)

                If TagName.ToLower.Contains("folder") Then
                    s = Item.Name
                    If Not SourceDirectories.Contains(s) Then SourceDirectories.Add(s)
                ElseIf TagName.ToLower = "asm" Then
                    s = System.IO.Path.GetDirectoryName(Item.Name)
                    If Not SourceDirectories.Contains(s) Then SourceDirectories.Add(s)
                End If

            End If

            'ListViewFiles.EndUpdate()

        Next

        Return SourceDirectories
    End Function
End Class
