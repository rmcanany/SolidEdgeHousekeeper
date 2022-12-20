Imports System.IO
Imports ExcelDataReader

Public Class CommonTasks

    Public Shared tmpList As Collection
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

    Shared Function RunExternalProgram(
        ExternalProgram As String
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim ExternalProgramDirectory As String = System.IO.Path.GetDirectoryName(ExternalProgram)
        Dim P As New Process
        Dim ExitCode As Integer
        Dim ErrorMessageFilename As String
        Dim ErrorMessages As String()

        P = Process.Start(ExternalProgram)
        P.WaitForExit()
        ExitCode = P.ExitCode  ' If the program doesn't supply one, what value can it take?  Null?

        ErrorMessageFilename = String.Format("{0}\error_messages.txt", ExternalProgramDirectory)

        If ExitCode <> 0 Then
            ExitStatus = 1
            If FileIO.FileSystem.FileExists(ErrorMessageFilename) Then
                ErrorMessages = IO.File.ReadAllLines(ErrorMessageFilename)
                If ErrorMessages.Length > 0 Then
                    For Each ErrorMessageFromProgram As String In ErrorMessages
                        ErrorMessageList.Add(ErrorMessageFromProgram)
                    Next
                Else
                    ErrorMessageList.Add(String.Format("Program terminated with exit code {0}", ExitCode))
                End If

                IO.File.Delete(ErrorMessageFilename)
            Else
                ErrorMessageList.Add(String.Format("Program terminated with exit code {0}", ExitCode))
            End If
        Else

        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Shared Function ReadExcel(FileName As String) As String()

        Dim tmpList As String() = Nothing
        Dim i As Integer = 0

        Using stream = File.Open(FileName, FileMode.Open, FileAccess.Read)
            ' Auto-detect format, supports:
            '  - Binary Excel files (2.0-2003 format; *.xls)
            '  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
            Using reader = ExcelReaderFactory.CreateReader(stream)
                ' Choose one of either 1 or 2:

                ' 1. Use the reader methods
                Do
                    While reader.Read()
                        i += 1
                        ReDim Preserve tmpList(i)
                        tmpList(i) = reader.GetValue(0)
                    End While
                Loop While reader.NextResult()

                '' 2. Use the AsDataSet extension method
                'Dim result = reader.AsDataSet()

                '' The result of each spreadsheet is in result.Tables
            End Using
        End Using

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

    Shared Function CropImage(Configuration As Dictionary(Of String, String),
                          SEDoc As SolidEdgeFramework.SolidEdgeDocument,
                          NewFilename As String,
                          NewExtension As String,
                          WindowH As Integer,
                          WindowW As Integer
                          ) As String

        Dim ModelX As Double
        Dim ModelY As Double
        Dim ModelZ As Double
        Dim XMin As Double
        Dim YMin As Double
        Dim ZMin As Double
        Dim XMax As Double
        Dim YMax As Double
        Dim ZMax As Double

        Dim ImageW As Double
        Dim ImageH As Double
        Dim ImageAspectRatio As Double

        Dim CropW As Integer
        Dim CropH As Integer

        Dim TempFilename As String

        Dim ExitMessage As String = ""

        Dim WindowAspectRatio As Double = WindowH / WindowW

        Select Case SEDoc.Type
            Case Is = 3, 7, 10 'asm
                Dim tmpAsm As SolidEdgeAssembly.AssemblyDocument = SEDoc
                tmpAsm.Range(XMin, YMin, ZMin, XMax, YMax, ZMax)
                ModelX = XMax - XMin
                ModelY = YMax - YMin
                ModelZ = ZMax - ZMin

            Case Is = 1, 8 'par
                Dim tmpPar As SolidEdgePart.PartDocument = SEDoc
                tmpPar.Range(XMin, YMin, ZMin, XMax, YMax, ZMax)
                ModelX = XMax - XMin
                ModelY = YMax - YMin
                ModelZ = ZMax - ZMin

            Case Is = 4, 9 'psm
                Dim tmpPsm As SolidEdgePart.SheetMetalDocument = SEDoc
                Dim Models As SolidEdgePart.Models
                Dim Model As SolidEdgePart.Model
                Dim Body As SolidEdgeGeometry.Body

                Dim FeatureDoctor As New FeatureDoctor
                Dim PointsList As New List(Of Double)
                Dim PointsListTemp As New List(Of Double)
                Dim Point As Double

                Models = tmpPsm.Models

                If (Models.Count = 0) Then
                    ExitMessage = "No models to process.  Cropped image not created."
                    Return ExitMessage
                End If
                If (Models.Count = 0) Or (Models.Count > 25) Then
                    ExitMessage = "Too many models to process.  Cropped image not created."
                    Return ExitMessage
                End If

                For Each Model In Models
                    Body = CType(Model.Body, SolidEdgeGeometry.Body)
                    PointsListTemp = FeatureDoctor.GetBodyRange(Body)
                    If PointsList.Count = 0 Then
                        For Each Point In PointsListTemp
                            PointsList.Add(Point)
                        Next
                    Else
                        For i As Integer = 0 To 2
                            If PointsListTemp(i) < PointsList(i) Then
                                PointsList(i) = PointsListTemp(i)
                            End If
                        Next
                        For i As Integer = 3 To 5
                            If PointsListTemp(i) > PointsList(i) Then
                                PointsList(i) = PointsListTemp(i)
                            End If
                        Next
                    End If
                Next

                ModelX = PointsList(3) - PointsList(0) 'XMax - XMin
                ModelY = PointsList(4) - PointsList(1) ' YMax - YMin
                ModelZ = PointsList(5) - PointsList(2) ' ZMax - ZMin

        End Select

        If Configuration("RadioButtonPictorialViewIsometric").ToLower = "true" Then
            ImageW = 0.707 * ModelX + 0.707 * ModelY
            ImageH = 0.40833 * ModelX + 0.40833 * ModelY + 0.81689 * ModelZ
        ElseIf Configuration("RadioButtonPictorialViewDimetric").ToLower = "true" Then
            ImageW = 0.9356667 * ModelX + 0.353333 * ModelY
            ImageH = 0.117222 * ModelX + 0.311222 * ModelY + 0.942444 * ModelZ
        Else
            ImageW = 0.557 * ModelX + 0.830667 * ModelY
            ImageH = 0.325444 * ModelX + 0.217778 * ModelY + 0.920444 * ModelZ
        End If

        ImageAspectRatio = ImageH / ImageW

        If WindowAspectRatio > ImageAspectRatio Then
            CropH = CInt(Math.Round(WindowW * ImageAspectRatio))
            CropW = WindowW
        Else
            CropH = WindowH
            CropW = CInt(Math.Round(WindowH / ImageAspectRatio))
        End If

        TempFilename = NewFilename.Replace(NewExtension, String.Format("-Housekeeper{0}", NewExtension))

        Dim LocX = (WindowW - CropW) / 2
        Dim LocY = (WindowH - CropH) / 2
        Dim CropRect As New Rectangle(LocX, LocY, CropW, CropH)
        Dim OriginalImage = Image.FromFile(NewFilename)
        Dim xCropImage = New Bitmap(CropRect.Width, CropRect.Height)

        Try

            Using grp = Graphics.FromImage(xCropImage)
                grp.DrawImage(OriginalImage, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
                OriginalImage.Dispose()
                xCropImage.Save(TempFilename)
            End Using

            Try
                System.IO.File.Delete(NewFilename)
                FileSystem.Rename(TempFilename, NewFilename)
            Catch ex As Exception
                ExitMessage = String.Format("Unable to save cropped image '{0}'", NewFilename)
            End Try

        Catch ex As Exception
            ExitMessage = String.Format("Unable to save cropped image '{0}'", TempFilename)
        End Try

        Return ExitMessage

    End Function

    Shared Sub FindLinked(objDoc As Object)

        Dim i As Short
        Dim objLinkedDocs As Object

        ' Loop through the items in the assembly
        objLinkedDocs = objDoc.LinkedDocuments

        If Not IsNothing(objLinkedDocs) Then

            For i = 1 To objLinkedDocs.Count
                If Not tmpList.Contains(objLinkedDocs(i).FullName.ToString) Then
                    tmpList.Add(objLinkedDocs(i).FullName, objLinkedDocs(i).FullName.ToString)
                    If IO.Path.GetExtension(objLinkedDocs(i).fullname).ToLower = ".asm" Then
                        ' there is a problem in traversing back up through the tree that throws an exception, so using a try-catch.
                        Try
                            FindLinked(objLinkedDocs(i))
                        Catch ex As Exception
                            'MsgBox("Problem with traversing back up the tree " & objLinkedDocs(i).fullname)
                        End Try
                    End If
                End If
            Next

        End If

        objLinkedDocs = Nothing

    End Sub

End Class
