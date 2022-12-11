Imports System.IO
Imports Microsoft.Office.Interop

Public Class CommonTasks

    Shared Function SaveAsPNG(View As SolidEdgeFramework.View,
                               NewFilename As String
                               ) As String

        Dim ExitMessage As String = ""
        Dim TempFilename As String = NewFilename.Replace(".png", "-Housekeeper.tif")

        Try

            View.SaveAsImage(TempFilename)

            Try

                Using fs As New IO.FileStream(TempFilename, IO.FileMode.Open, IO.FileAccess.Read)
                    Image.FromStream(fs).Save(NewFilename, Imaging.ImageFormat.Png)
                End Using

                IO.File.Delete(TempFilename)

            Catch ex As Exception
                ExitMessage = String.Format("Unable to save '{0}'.  ", NewFilename)
            End Try

        Catch ex As Exception
            ExitMessage = String.Format("Unable to save '{0}'.  ", TempFilename)
        End Try

        Return ExitMessage

    End Function

    Shared Function TruncateFullPath(ByVal Path As String,
    Configuration As Dictionary(Of String, String)
    ) As String

        'Dim Length As Integer = Len(Configuration("TextBoxInputDirectory"))
        'Dim NewPath As String

        'If Path.Contains(Configuration("TextBoxInputDirectory")) Then
        '    NewPath = Path.Remove(0, Length)
        '    NewPath = "~" + NewPath
        'Else
        '    NewPath = Path
        'End If
        Return Path
    End Function

    Shared Function ReadExcel(FileName As String) As String()

        Dim tmpList As String() = Nothing

        Dim xlApp As Excel.Application = New Excel.Application
        Dim xlWb As Excel.Workbook = xlApp.Workbooks.Open(FileName)
        Dim xlWs As Excel.Worksheet = CType(xlWb.Worksheets.Item(1), Excel.Worksheet)

        For i As Object = 1 To xlWs.Rows.Count

            If xlWs.Cells(i, 1).value <> "" Then
                ReDim Preserve tmpList(i)
                tmpList(i) = xlWs.Cells(i, 1).value
            Else

                Exit For
            End If
        Next

        Return tmpList

    End Function

    Public Shared Function FilenameIsOK(ByVal fileName As String) As Boolean

        Try
            Dim fi As New IO.FileInfo(fileName)
        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function

End Class
