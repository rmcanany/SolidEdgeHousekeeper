Option Strict On

Public Class MaterialDoctorSheetmetal


    Public Function UpdateMaterialFromMaterialTable(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim MatTable As SolidEdgeFramework.MatTable

        Dim MaterialLibList As Object = Nothing
        Dim NumMaterialLibraries As Integer
        Dim MaterialList As Object = Nothing
        Dim NumMaterials As Integer

        Dim ActiveMaterialLibrary As String = System.IO.Path.GetFileNameWithoutExtension(Configuration("TextBoxActiveMaterialLibrary"))
        Dim CurrentMaterialName As String = ""
        Dim MatTableMaterial As Object

        Dim Models As SolidEdgePart.Models = SEDoc.Models

        Dim MaxModelCount As Integer = 10

        If (Models.Count > 0) And (Models.Count <= MaxModelCount) Then

            MatTable = SEApp.GetMaterialTable()

            ' This function populates 'CurrentMaterialName'
            MatTable.GetCurrentMaterialName(SEDoc, CurrentMaterialName)

            ' This function populates 'MaterialLibList' and 'NumMaterialLibraries'
            MatTable.GetMaterialLibraryList(MaterialLibList, NumMaterialLibraries)

            'Make sure the ActiveMaterialLibrary exists
            If Not IsActiveMaterialLibraryPresent(MaterialLibList, ActiveMaterialLibrary) Then
                Dim msg As String
                msg = "ActiveMaterialLibrary " + Configuration("TextBoxActiveMaterialLibrary") + " not found.  Exiting..." + Chr(13)
                msg += "Please update the Material Table on the Configuration tab." + Chr(13)
                MsgBox(msg)
                SEApp.Quit()
                End
            End If

            ' This function populates 'NumMaterials' and 'MaterialList'
            MatTable.GetMaterialListFromLibrary(ActiveMaterialLibrary, NumMaterials, MaterialList)

            If Not CurrentMaterialNameInLibrary(CurrentMaterialName, MaterialList) Then
                ExitStatus = 1
                If CurrentMaterialName = "" Then
                    ErrorMessageList.Add(String.Format("Material 'None' not in {0}", ActiveMaterialLibrary))
                Else
                    ErrorMessageList.Add(String.Format("Material '{0}' not in {1}", CurrentMaterialName, ActiveMaterialLibrary))
                End If
            Else
                For Each MatTableMaterial In CType(MaterialList, System.Array)
                    If MatTableMaterial.ToString.ToLower.Trim = CurrentMaterialName.ToLower.Trim Then

                        ' Names match, check if their properties do.
                        If Not MaterialPropertiesMatch(SEDoc, MatTable, MatTableMaterial, ActiveMaterialLibrary) Then

                            ' Properties do not match.  Update the document's material to match the library version.
                            MatTable.ApplyMaterialToDoc(SEDoc, MatTableMaterial.ToString, ActiveMaterialLibrary)
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("'{0}' was updated", CurrentMaterialName))
                        End If

                        ' Face styles are not always updated, especially on imported files.
                        ' Some imported files have trouble with face updates.
                        Try
                            If Not UpdateFaces(SEApp, SEDoc, CurrentMaterialFaceStyle(SEDoc, MatTable, MatTableMaterial, ActiveMaterialLibrary)) Then
                                ExitStatus = 1
                                ErrorMessageList.Add("Some face styles may not have been updated.  Please verify results.")
                            End If
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add("Some face styles may not have been updated.  Please verify results.")
                        End Try

                        If SEDoc.ReadOnly Then
                            ExitStatus = 1
                            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
                        Else
                            SEDoc.Save()
                            SEApp.DoIdle()
                        End If

                        Exit For
                    End If
                Next

            End If

        ElseIf Models.Count >= MaxModelCount Then
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("{0} models exceeds maximum to process", Models.Count.ToString))
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function

    Private Function IsActiveMaterialLibraryPresent(MaterialLibList As Object, ActiveMaterialLibrary As String) As Boolean
        For Each MatTableLibrary In CType(MaterialLibList, System.Array)
            If MatTableLibrary.ToString = ActiveMaterialLibrary Then
                Return True
                Exit For
            End If
        Next
        Return False
    End Function

    Private Function CurrentMaterialNameInLibrary(CurrentMaterialName As String, MaterialList As Object) As Boolean
        For Each MatTableMaterial In CType(MaterialList, System.Array)
            If MatTableMaterial.ToString.ToLower.Trim = CurrentMaterialName.ToLower.Trim Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function CloseEnough(This As Double, That As Double) As Boolean
        If Not ((This = 0) And (That = 0)) Then  ' Avoid divide by 0.  Anyway, they match.
            Dim NormalizedDifference As Double = (This - That) / (This + That) / 2
            If Not Math.Abs(NormalizedDifference) < 0.001 Then
                Return False
            End If
        End If
        Return True
    End Function

    Private Function MaterialPropertiesMatch(
        SEDoc As SolidEdgePart.SheetMetalDocument,
        MatTable As SolidEdgeFramework.MatTable,
        MatTableMaterial As Object,
        ActiveMaterialLibrary As String) As Boolean

        Dim MatTableProps As Array = System.Enum.GetValues(GetType(SolidEdgeConstants.MatTablePropIndex))
        Dim MatTableProp As SolidEdgeFramework.MatTablePropIndex
        Dim DocPropValue As Object = Nothing
        Dim LibPropValue As Object = Nothing

        For Each MatTableProp In MatTableProps
            ' This function populates 'LibPropValue'
            MatTable.GetMaterialPropValueFromLibrary(MatTableMaterial.ToString, ActiveMaterialLibrary, MatTableProp, LibPropValue)

            ' This function populates 'DocPropValue'
            MatTable.GetMaterialPropValueFromDoc(SEDoc, MatTableProp, DocPropValue)

            ' MatTableProps are either Double or String.
            If (DocPropValue.GetType = GetType(Double)) And (LibPropValue.GetType = GetType(Double)) Then
                ' Double types may have insignificant differences.
                If Not CloseEnough(CType(DocPropValue, Double), CType(LibPropValue, Double)) Then
                    Return False
                End If
            Else
                If CType(DocPropValue, String) <> CType(LibPropValue, String) Then
                    Return False
                End If
            End If
            DocPropValue = Nothing
            LibPropValue = Nothing
        Next

        Return True
    End Function

    Private Function CurrentMaterialFaceStyle(
        SEDoc As SolidEdgePart.SheetMetalDocument,
        MatTable As SolidEdgeFramework.MatTable,
        MatTableMaterial As Object,
        ActiveMaterialLibrary As String) As SolidEdgeFramework.FaceStyle

        Dim MatTableProps As Array = System.Enum.GetValues(GetType(SolidEdgeConstants.MatTablePropIndex))
        Dim LibPropValue As Object = Nothing
        Dim MatTableProp As SolidEdgeFramework.MatTablePropIndex
        Dim FaceStyle As SolidEdgeFramework.FaceStyle

        For Each MatTableProp In MatTableProps
            ' This function populates 'LibPropValue'
            MatTable.GetMaterialPropValueFromLibrary(MatTableMaterial.ToString, ActiveMaterialLibrary, MatTableProp, LibPropValue)

            If MatTableProp.ToString = "seFaceStyle" Then
                For Each FaceStyle In CType(SEDoc.FaceStyles, SolidEdgeFramework.FaceStyles)
                    If FaceStyle.StyleName = LibPropValue.ToString Then
                        Return FaceStyle
                    End If
                Next
            End If

            LibPropValue = Nothing
        Next
        Return Nothing

    End Function

    Private Function UpdateFaces(
        SEApp As SolidEdgeFramework.Application,
        SEDoc As SolidEdgePart.SheetMetalDocument,
        CurrentMaterialFaceStyle As SolidEdgeFramework.FaceStyle) As Boolean

        Dim UpdatesComplete As Boolean = True

        Dim Models As SolidEdgePart.Models
        Dim Model As SolidEdgePart.Model
        Dim Body As SolidEdgeGeometry.Body
        Dim Faces As SolidEdgeGeometry.Faces
        Dim Face As SolidEdgeGeometry.Face

        Dim Features As SolidEdgePart.Features

        Dim FaceOverrides As New Dictionary(Of Integer, SolidEdgeFramework.FaceStyle)
        Dim FeatureFaceOverrides As New Dictionary(Of Integer, SolidEdgeFramework.FaceStyle)
        Dim BodyOverride As SolidEdgeFramework.FaceStyle = Nothing

        Dim FeatureNames As New List(Of String)
        Dim FeatureName As String

        Models = SEDoc.Models

        If (Models.Count > 0) And (Models.Count < 300) Then
            For Each Model In Models

                ' Some Models do not have a Body
                Try
                    Body = CType(Model.Body, SolidEdgeGeometry.Body)
                    If Body.Style IsNot Nothing Then
                        BodyOverride = Body.Style
                    End If

                    'Body.Faces
                    Faces = CType(Body.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll), SolidEdgeGeometry.Faces)

                    For Each Face In Faces
                        FaceOverrides(Face.ID) = Face.Style
                        'If Face.Style IsNot Nothing Then
                        '    FaceOverrides(Face.ID) = Face.Style
                        'End If
                    Next

                    Features = Model.Features
                    For Each Feature In Features
                        FeatureName = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(Of String)(Feature, "Name")
                        If Not FeatureNames.Contains(FeatureName) Then
                            FeatureNames.Add(FeatureName)

                            FeatureFaceOverrides = GetFeatureFaceStyle(Feature)

                            If FeatureFaceOverrides.Count > 0 Then
                                For Each Key In FeatureFaceOverrides.Keys
                                    If FaceOverrides(Key) Is Nothing Then
                                        FaceOverrides(Key) = FeatureFaceOverrides(Key)
                                    End If
                                Next
                            End If

                            FeatureFaceOverrides.Clear()
                        End If

                    Next

                    Dim MaxFacesToProcess As Integer = 500

                    If (FaceOverrides.Count > 0) And (FaceOverrides.Count <= MaxFacesToProcess) Then
                        ' Crashes on some imported files
                        Try
                            ' Body.ClearOverrides() apparently makes a new body, rendeering the previous Body invalid.
                            ' It also, at least in one case, increments the Face.ID for certain Faces.
                            ' Hopefully it does not change the order of the Faces.

                            Dim FaceStyleList As New List(Of SolidEdgeFramework.FaceStyle)
                            For Each Key In FaceOverrides.Keys
                                FaceStyleList.Add(FaceOverrides(Key))
                            Next
                            FaceOverrides.Clear()

                            Body.ClearOverrides()
                            Body = CType(Model.Body, SolidEdgeGeometry.Body)
                            Faces = CType(Body.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll), SolidEdgeGeometry.Faces)

                            Dim i As Integer = 0
                            For Each Face In Faces
                                If FaceStyleList(i) IsNot Nothing Then
                                    FaceOverrides(Face.ID) = FaceStyleList(i)
                                End If
                                i += 1
                            Next

                        Catch ex As Exception
                            UpdatesComplete = False
                            Return UpdatesComplete
                        End Try

                        SEApp.DoIdle()

                        If BodyOverride IsNot Nothing Then
                            Body.Style = BodyOverride
                            BodyOverride = Nothing
                        End If

                        Dim Count As Integer = 0

                        For Each Face In Faces
                            If FaceOverrides.Keys.Contains(Face.ID) Then
                                Face.Style = FaceOverrides(Face.ID)
                                Count += 1
                                If Count Mod 100 = 0 Then
                                    SEApp.DoIdle()
                                End If
                            End If
                        Next
                        FaceOverrides.Clear()

                    ElseIf FaceOverrides.Count > MaxFacesToProcess Then
                        UpdatesComplete = False
                    End If

                Catch ex As Exception
                    UpdatesComplete = False
                End Try
            Next

        ElseIf Models.Count >= 300 Then
            UpdatesComplete = False
        End If

        Return UpdatesComplete

    End Function

    Private Function PopulateFeatureFaceOverrides(
        FeatureFaces As SolidEdgeGeometry.Faces,
        FeatureFaceStyle As SolidEdgeFramework.FaceStyle) As Dictionary(Of Integer, SolidEdgeFramework.FaceStyle)

        Dim Face As SolidEdgeGeometry.Face
        Dim FeatureFaceOverrides As New Dictionary(Of Integer, SolidEdgeFramework.FaceStyle)

        For Each Face In FeatureFaces
            FeatureFaceOverrides(Face.ID) = FeatureFaceStyle
        Next
        Return FeatureFaceOverrides

    End Function

    Private Function GetFeatureFaceStyle(Feature As Object) As Dictionary(Of Integer, SolidEdgeFramework.FaceStyle)

        Dim FeatureFaces As SolidEdgeGeometry.Faces = Nothing
        Dim FeatureFaceStyle As SolidEdgeFramework.FaceStyle
        Dim FeatureFaceOverrides As New Dictionary(Of Integer, SolidEdgeFramework.FaceStyle)

        Dim FeatureType = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(
        Of SolidEdgePart.FeatureTypeConstants)(Feature, "Type", CType(0, SolidEdgePart.FeatureTypeConstants))

        Select Case FeatureType

            Case SolidEdgePart.FeatureTypeConstants.igBeadFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Bead)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll), SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igBendFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Bend)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll), SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igBlueSurfFeatureObject
                        ' BlueSurf objects do not have a FaceStyle override.

            Case SolidEdgePart.FeatureTypeConstants.igBodyFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.BodyFeature)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                                   SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igBooleanFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.BooleanFeature)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igBreakCornerFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.BreakCorner)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igChamferFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Chamfer)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igCloseCornerFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.CloseCorner)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igContourFlangeFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.ContourFlange)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igCopiedPartFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.CopiedPart)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    ' Some CopiedParts do not have Faces
                    Try
                        FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                        Feature_.SetStyle(Nothing)
                        Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                    Catch ex As Exception
                    End Try
                End If

            Case SolidEdgePart.FeatureTypeConstants.igCopyConstructionObject
                Dim Feature_ = CType(Feature, SolidEdgePart.CopyConstruction)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igDeleteBlendFeatureObject 'GetType(SolidEdgePart.DeleteBlend)
                Dim Feature_ = CType(Feature, SolidEdgePart.DeleteBlend)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igDeleteFaceFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.DeleteFace)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igDeleteHoleFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.DeleteHole)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igDimpleFeatureObject 'GetType(SolidEdgePart.Dimple)
                Dim Feature_ = CType(Feature, SolidEdgePart.Dimple)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igDraftFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Draft)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igDrawnCutoutFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.DrawnCutout)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igExtrudedCutoutFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.ExtrudedCutout)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igExtrudedProtrusionFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.ExtrudedProtrusion)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igExtrudedSurfaceObject
                Dim Feature_ = CType(Feature, SolidEdgePart.ExtrudedSurface)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igFaceMoveObject
                        ' Does not have a Faces collection.

            Case SolidEdgePart.FeatureTypeConstants.igFlangeFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Flange)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igHelixCutoutFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.HelixCutout)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igHelixProtrusionFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.HelixProtrusion)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igHoleFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Hole)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igJogFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Jog)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igLipFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Lip)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igLoftedCutoutFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.LoftedCutout)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igLoftedFlangeFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.LoftedFlange)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igLoftedProtrusionFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.LoftedProtrusion)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igLouverFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Louver)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igMidSurfaceObject
                Dim Feature_ = CType(Feature, SolidEdgePart.MidSurface)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igMirrorCopyFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.MirrorCopy)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igMirrorPartFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.MirrorPart)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igNormalCutoutFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.NormalCutout)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igPartingSplitFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.PartingSplit)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igPatternFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Pattern)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igPatternPartFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.PatternPart)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igRebendFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Rebend)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igReplaceFaceFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.ReplaceFace)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igResizeHoleObject
                        ' Does not have a Faces collection.

            Case SolidEdgePart.FeatureTypeConstants.igRevolvedCutoutFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.RevolvedCutout)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igRevolvedProtrusionFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.RevolvedProtrusion)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igRibFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Rib)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igRoundFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Round)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igSlotFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Slot)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igSplitFaceObject
                        ' Does not have a Faces collection.

            Case SolidEdgePart.FeatureTypeConstants.igSweptCutoutFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.SweptCutout)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igSweptProtrusionFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.SweptProtrusion)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igTabFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Tab)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igThickenFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Thicken)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igThinwallFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Thinwall)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igTubeFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.TubeFeature)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igUnbendFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.Unbend)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igUserDefinedPatternFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.UserDefinedPattern)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case SolidEdgePart.FeatureTypeConstants.igWebNetworkFeatureObject
                Dim Feature_ = CType(Feature, SolidEdgePart.WebNetwork)
                FeatureFaceStyle = Feature_.GetStyle()
                If FeatureFaceStyle IsNot Nothing Then
                    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                           SolidEdgeGeometry.Faces)
                    Feature_.SetStyle(Nothing)
                    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                End If

            Case Else
                Select Case FeatureType.ToString
                    Case "0"  ' Box, Cylinder, and some others.  Could be flagging sync parts.
                                ' These features do not have a FaceStyle override.

                    Case "1737031522"  'ConvertToSM
                                ' Seems like it should work.  It doesn't.
                                'Dim Feature_ = CType(Feature, SolidEdgePart.ConvToSM)
                                'FeatureFaceStyle = Feature_.GetStyle()
                                'If FeatureFaceStyle IsNot Nothing Then
                                '    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                                '   SolidEdgeGeometry.Faces)
                                '    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                                'End If

                    Case "1245666020"  'FaceSet
                                ' Seems like it should work.  It doesn't.
                                'Dim Feature_ = CType(Feature, SolidEdgePart.FaceSet)
                                'FeatureFaceStyle = Feature_.GetStyle()
                                'If FeatureFaceStyle IsNot Nothing Then
                                '    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                                '   SolidEdgeGeometry.Faces)
                                '    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                                'End If

                    Case "69347334"  'Hem
                                ' Seems like it should work.  It doesn't.
                                'Dim Feature_ = CType(Feature, SolidEdgePart.Hem)
                                'FeatureFaceStyle = Feature_.GetStyle()
                                'If FeatureFaceStyle IsNot Nothing Then
                                '    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                                '   SolidEdgeGeometry.Faces)
                                '    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                                'End If

                    Case "1477405962"  'Subtract
                                ' Seems like it should work.  It doesn't.
                                'Dim Feature_ = CType(Feature, SolidEdgePart.Subtract)
                                'FeatureFaceStyle = Feature_.GetStyle()
                                'If FeatureFaceStyle IsNot Nothing Then
                                '    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                                '   SolidEdgeGeometry.Faces)
                                '    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                                'End If

                    Case "-127107951"  'Thread
                                ' Seems like it should work.  It doesn't.
                                'Dim Feature_ = CType(Feature, SolidEdgePart.Thread)
                                'FeatureFaceStyle = Feature_.GetStyle()
                                'If FeatureFaceStyle IsNot Nothing Then
                                '    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                                '   SolidEdgeGeometry.Faces)
                                '    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                                'End If

                    Case "451898536"  'Transform.  Guessing it's ConvertPartToSM.
                                ' Seems like it should work.  It doesn't.
                                'Dim Feature_ = CType(Feature, SolidEdgePart.ConvertPartToSM)
                                'FeatureFaceStyle = Feature_.GetStyle()
                                'If FeatureFaceStyle IsNot Nothing Then
                                '    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                                '   SolidEdgeGeometry.Faces)
                                '    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                                'End If

                    Case "1385450842"  'Union
                        ' Seems like it should work.  It doesn't.
                        'Dim Feature_ = CType(Feature, SolidEdgePart.Union)
                        'FeatureFaceStyle = Feature_.GetStyle()
                        'If FeatureFaceStyle IsNot Nothing Then
                        '    FeatureFaces = CType(Feature_.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll),
                        '   SolidEdgeGeometry.Faces)
                        '    Return PopulateFeatureFaceOverrides(FeatureFaces, FeatureFaceStyle)
                        'End If

                    Case Else
                        Dim FeatureName = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(Of String)(Feature, "Name")
                        'ExitStatus = 1
                        'ErrorMessageList.Add(String.Format("{0} (FeatureType={1}) not processed.  Please verify results.", FeatureName, FeatureType.ToString))
                End Select

        End Select

        Return FeatureFaceOverrides


    End Function
End Class
