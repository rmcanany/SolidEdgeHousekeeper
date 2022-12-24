Option Strict Off

Public Class FeatureDoctor

    Public Function IsOrdered(Feature) As Boolean
        Try
            If Feature.ModelingModeType = 1 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function GetProfile(Feature) As SolidEdgePart.Profile
        Dim Profile As SolidEdgePart.Profile = Nothing

        Try
            Profile = Feature.Profile
        Catch ex As Exception
        End Try

        Return Profile

    End Function

    Public Function GetBodyRange(Body As SolidEdgeGeometry.Body) As List(Of Double)
        Dim minmax As New List(Of Double)
        Dim MinRangePoint = Array.CreateInstance(GetType(Double), 3)
        Dim MaxRangePoint = Array.CreateInstance(GetType(Double), 3)
        Dim Point As Double

        Body.GetRange(MinRangePoint, MaxRangePoint)

        For Each Point In MinRangePoint
            minmax.Add(Point)
        Next
        For Each Point In MaxRangePoint
            minmax.Add(Point)
        Next

        Return minmax

    End Function
End Class
