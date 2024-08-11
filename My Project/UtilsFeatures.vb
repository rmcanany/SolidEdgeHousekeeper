Option Strict On


Public Class UtilsFeatures

    Public Function GetName(Feature As Object) As String

        Dim Name = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(Of String)(Feature, "Name")

        Return Name

    End Function

    Public Function IsOrdered(Feature As Object) As Boolean

        ' Some features do not have a ModelingModeType
        Try
            Dim ModelingModeType = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(
        Of SolidEdgePart.ModelingModeConstants)(Feature, "ModelingModeType")

            If ModelingModeType = SolidEdgePart.ModelingModeConstants.seModelingModeOrdered Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function GetFeatureType(Feature As Object) As SolidEdgePart.FeatureTypeConstants

        Dim FeatureType As SolidEdgePart.FeatureTypeConstants = Nothing

        Try
            FeatureType = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(
        Of SolidEdgePart.FeatureTypeConstants)(Feature, "Type")
        Catch ex As Exception
        End Try


        Return FeatureType

    End Function

    Public Function GetStatus(Feature As Object) As SolidEdgePart.FeatureStatusConstants

        Dim Status = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(
                     Of SolidEdgePart.FeatureStatusConstants)(Feature, "Status")

        Return Status

    End Function

    Public Function GetStatusEx(Feature As Object) As SolidEdgePart.FeatureStatusConstants

        Dim StatusEx = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(
                     Of SolidEdgePart.FeatureStatusConstants)(Feature, "Status")

        Return StatusEx
    End Function

    Public Function GetProfile(Feature As Object) As SolidEdgePart.Profile

        Dim Profile As SolidEdgePart.Profile = Nothing

        'Some features do not have a profile
        Try
            Profile = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(
                     Of SolidEdgePart.Profile)(Feature, "Profile")
        Catch ex As Exception
        End Try

        Return Profile

    End Function

    Public Function GetPatternPlane(Feature As Object) As SolidEdgePart.RefPlane

        Dim PatternPlane = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(
                     Of SolidEdgePart.RefPlane)(Feature, "PatternPlane")

        Return PatternPlane

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

    Public Function GetRuntimeType(Of T)(Feature As T) As Type
        Return GetType(T)
    End Function


    Public Function GetTypeFromFeature(Feature As Object) As Type

        Dim FeatureType As Type = Nothing

        Dim FeatureTypeConstant As SolidEdgePart.FeatureTypeConstants = GetFeatureType(Feature)

        Select Case FeatureTypeConstant

            Case SolidEdgePart.FeatureTypeConstants.igExtrudedProtrusionFeatureObject
                Dim _Feature As SolidEdgePart.ExtrudedProtrusion = CType(Feature, SolidEdgePart.ExtrudedProtrusion)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igBeadFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Bead)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igBendFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Bend)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igBlueSurfFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.BlueSurf)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igBodyFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.BodyFeature)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igBooleanFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.BooleanFeature)

            Case SolidEdgePart.FeatureTypeConstants.igBreakCornerFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.BreakCorner)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igChamferFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Chamfer)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igCloseCornerFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.CloseCorner)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igContourFlangeFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.ContourFlange)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igCopiedPartFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.CopiedPart)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igCopyConstructionObject
                Dim _Feature = CType(Feature, SolidEdgePart.CopyConstruction)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igDeleteBlendFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.DeleteBlend)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igDeleteFaceFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.DeleteFace)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igDeleteHoleFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.DeleteHole)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igDimpleFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Dimple)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igDraftFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Draft)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igDrawnCutoutFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.DrawnCutout)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igExtrudedCutoutFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.ExtrudedCutout)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igExtrudedProtrusionFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.ExtrudedProtrusion)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igExtrudedSurfaceObject
                Dim _Feature = CType(Feature, SolidEdgePart.ExtrudedSurface)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igFaceMoveObject
                Dim _Feature = CType(Feature, SolidEdgePart.FaceMove)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igFlangeFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Flange)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igHelixCutoutFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.HelixCutout)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igHelixProtrusionFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.HelixProtrusion)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igHoleFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Hole)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igJogFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Jog)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igLipFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Lip)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igLoftedCutoutFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.LoftedCutout)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igLoftedFlangeFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.LoftedFlange)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igLoftedProtrusionFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.LoftedProtrusion)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igLouverFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Louver)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igMidSurfaceObject
                Dim _Feature = CType(Feature, SolidEdgePart.MidSurface)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igMirrorCopyFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.MirrorCopy)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igMirrorPartFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.MirrorPart)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igNormalCutoutFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.NormalCutout)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igPartingSplitFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.PartingSplit)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igPatternFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Pattern)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igPatternPartFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.PatternPart)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igRebendFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Rebend)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igReplaceFaceFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.ReplaceFace)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igResizeHoleObject
                Dim _Feature = CType(Feature, SolidEdgePart.ResizeHole)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igRevolvedCutoutFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.RevolvedCutout)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igRevolvedProtrusionFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.RevolvedProtrusion)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igRibFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Rib)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igRoundFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Round)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igSlotFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Slot)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igSplitFaceObject
                Dim _Feature = CType(Feature, SolidEdgePart.SplitFace)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igSweptCutoutFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.SweptCutout)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igSweptProtrusionFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.SweptProtrusion)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igTabFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Tab)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igThickenFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Thicken)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igThinwallFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Thinwall)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igTubeFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.TubeFeature)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igUnbendFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.Unbend)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igUserDefinedPatternFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.UserDefinedPattern)
                FeatureType = GetRuntimeType(_Feature)

            Case SolidEdgePart.FeatureTypeConstants.igWebNetworkFeatureObject
                Dim _Feature = CType(Feature, SolidEdgePart.WebNetwork)
                FeatureType = GetRuntimeType(_Feature)

        End Select

        Return FeatureType


    End Function

End Class
