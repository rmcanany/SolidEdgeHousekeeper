Option Strict On
Imports SolidEdgeConstants

Public Class UtilsMaterials

    Private Property SEApp As SolidEdgeFramework.Application
    Private Property SEDoc As SolidEdgeFramework.SolidEdgeDocument
    Private Property ActiveMaterialLibrary As String
    Private Property RemoveFaceStyleOverrides As Boolean
    Private Property UpdateFaceStyles As Boolean
    Private Property UseFinishFaceStyle As Boolean
    Private Property FinishName As String
    Private Property ExcludedFinishesList As List(Of String)
    Private Property OverrideBodyFaceStyle As Boolean
    Private Property OverrideMaterialFaceStyle As Boolean

    Public Function MaterialNotInMaterialTable(
        ByVal _SEApp As SolidEdgeFramework.Application,
        ByVal _SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        _ActiveMaterialLibrary As String
        ) As Dictionary(Of Integer, List(Of String))

        Me.SEApp = _SEApp
        Me.SEDoc = _SEDoc
        Me.ActiveMaterialLibrary = _ActiveMaterialLibrary

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim MatTable As SolidEdgeFramework.MatTable

        Dim MaterialLibList As Object = Nothing
        Dim NumMaterialLibraries As Integer
        Dim MaterialList As Object = Nothing
        Dim NumMaterials As Integer

        ActiveMaterialLibrary = System.IO.Path.GetFileNameWithoutExtension(ActiveMaterialLibrary)
        Dim ActiveMaterialLibraryPresent As Boolean = False
        Dim CurrentMaterialName As String = ""
        Dim MatTableMaterial As Object
        Dim CurrentMaterialNameInLibrary As Boolean = False
        Dim CurrentMaterialMatchesLibMaterial As Boolean = True

        Dim msg As String = ""

        Dim Models As SolidEdgePart.Models = Nothing

        Dim UC As New UtilsCommon
        Dim DocType = UC.GetDocType(SEDoc)

        Select Case DocType
            Case "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                Models = tmpSEDoc.Models
            Case "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                Models = tmpSEDoc.Models
            Case "asm"
                'Hopefully, nothing to do here.
        End Select

        If (DocType = "asm") OrElse (Models.Count > 0) Then

            MatTable = SEApp.GetMaterialTable()
            MatTable.GetCurrentMaterialName(SEDoc, CurrentMaterialName)
            MatTable.GetMaterialLibraryList(MaterialLibList, NumMaterialLibraries)

            'See if the CurrentMaterialName is in the ActiveLibrary
            MatTable.GetMaterialListFromLibrary(ActiveMaterialLibrary, NumMaterials, MaterialList)
            For Each MatTableMaterial In CType(MaterialList, System.Array)
                If MatTableMaterial.ToString.ToLower.Trim = CurrentMaterialName.ToLower.Trim Then
                    CurrentMaterialNameInLibrary = True
                    Exit For
                End If
            Next

            If Not CurrentMaterialNameInLibrary Then
                ExitStatus = 1
                If CurrentMaterialName = "" Then
                    ErrorMessageList.Add(String.Format("Material 'None' not in {0}", ActiveMaterialLibrary))
                Else
                    ErrorMessageList.Add(String.Format("Material '{0}' not in {1}", CurrentMaterialName, ActiveMaterialLibrary))
                End If
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Public Function UpdateMaterialFromMaterialTable(
        ByVal _SEApp As SolidEdgeFramework.Application,
        ByVal _SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        _ActiveMaterialLibrary As String,
        _RemoveFaceStyleOverrides As Boolean,
        _UpdateFaceStyles As Boolean,
        _UseFinishFaceStyle As Boolean,
        _FinishName As String,
        _ExcludedFinishesList As List(Of String),
        _OverrideBodyFaceStyle As Boolean,
        _OverrideMaterialFaceStyle As Boolean
        ) As Dictionary(Of Integer, List(Of String))

        Me.SEApp = _SEApp
        Me.SEDoc = _SEDoc
        Me.ActiveMaterialLibrary = _ActiveMaterialLibrary
        Me.RemoveFaceStyleOverrides = _RemoveFaceStyleOverrides
        Me.UpdateFaceStyles = _UpdateFaceStyles
        Me.UseFinishFaceStyle = _UseFinishFaceStyle
        Me.FinishName = _FinishName
        Me.ExcludedFinishesList = _ExcludedFinishesList
        Me.OverrideBodyFaceStyle = _OverrideBodyFaceStyle
        Me.OverrideMaterialFaceStyle = _OverrideMaterialFaceStyle

        'ActiveMaterialLibrary = System.IO.Path.GetFileNameWithoutExtension(ActiveMaterialLibrary)

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Mismatches As List(Of SolidEdgeFramework.MatTablePropIndex)
        Dim MatTable As SolidEdgeFramework.MatTable
        'Dim MaterialLibList As Object
        'Dim NumMaterialLibraries As Integer
        Dim MaterialList As Object = Nothing
        Dim NumMaterials As Integer
        Dim CurrentMaterialName As String = ""
        Dim MatTableMaterial As Object = Nothing
        Dim tf As Boolean
        Dim Models As SolidEdgePart.Models = Nothing
        Dim MaxModelCount As Integer = 10
        Dim UC As New UtilsCommon


        ' ###### GET THE MATERIAL TABLE AND A LIST OF MATERIALS IT CONTAINS ######

        MatTable = SEApp.GetMaterialTable()

        ' Populate 'NumMaterials' and 'MaterialList'
        MatTable.GetMaterialListFromLibrary(ActiveMaterialLibrary, NumMaterials, MaterialList)


        ' ###### GET THE DOCUMENT MODELS ######

        Select Case UC.GetDocType(SEDoc)
            Case "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                Models = tmpSEDoc.Models
                MatTable.GetCurrentMaterialName(tmpSEDoc, CurrentMaterialName)
                If Models.Count = 0 Then
                    ' Not an error
                    ErrorMessage(ExitStatus) = ErrorMessageList
                    Return ErrorMessage
                End If
            Case "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                Models = tmpSEDoc.Models
                MatTable.GetCurrentMaterialName(tmpSEDoc, CurrentMaterialName)
                If Models.Count = 0 Then
                    ' Not an error
                    ErrorMessage(ExitStatus) = ErrorMessageList
                    Return ErrorMessage
                End If
        End Select


        ' ###### CHECK START CONDITIONS ######

        ErrorMessageList = CheckUpdateMaterialStartConditions(
            SEDoc, Models, MaxModelCount, CurrentMaterialName, MaterialList)

        If ErrorMessageList.Count > 0 Then
            ExitStatus = 1
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        End If


        ' ###### GET THE MATERIAL OBJECT FROM THE MATERIAL TABLE ######

        For Each tmpMatTableMaterial As Object In CType(MaterialList, System.Array)
            If tmpMatTableMaterial.ToString.ToLower.Trim = CurrentMaterialName.ToLower.Trim Then
                ' This is the one we want
                MatTableMaterial = tmpMatTableMaterial
                Exit For
            End If
        Next


        ' ###### CHECK FOR MISMATCHES BETWEEN THE MATERIAL TABLE AND DOCUMENT ######

        Mismatches = GetMaterialPropertyMismatches(MatTable, MatTableMaterial, ActiveMaterialLibrary)


        ' ###### FIX ANY MISMATCHES ######

        If Mismatches.Count > 0 Then

            Dim NewWay As Boolean = True
            If NewWay Then
                If Me.UpdateFaceStyles Then
                    ' Face styles are not always updated, especially on imported files.
                    Try
                        MatTable.ApplyMaterialToDoc(SEDoc, MatTableMaterial.ToString, ActiveMaterialLibrary)
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add("Some face styles may not have been updated.  Please verify results.")
                    End Try
                Else
                    For Each MismatchProp As SolidEdgeFramework.MatTablePropIndex In Mismatches

                        tf = MismatchProp = SolidEdgeFramework.MatTablePropIndex.seFaceStyle
                        tf = tf Or MismatchProp = SolidEdgeFramework.MatTablePropIndex.seFillStyle
                        If tf Then
                            Continue For
                        End If

                        Dim LibPropValue As Object = Nothing
                        MatTable.GetMaterialPropValueFromLibrary(
                            MatTableMaterial.ToString, ActiveMaterialLibrary, MismatchProp, LibPropValue)
                        SEApp.DoIdle()

                        Dim ModelIdx As Integer = 0
                        Dim AutoAdd As Boolean = False

                        Select Case MismatchProp
                            Case SolidEdgeFramework.MatTablePropIndex.seDensity
                                ' Density is a variable, not a property

                                Dim DocVariableDict As New Dictionary(Of String, SolidEdgeFramework.variable)
                                DocVariableDict = UC.GetDocVariables(SEDoc)
                                If DocVariableDict.Keys.Contains("PhysicalProperties_Density") Then
                                    ' Density from the material table is in kg/m^3
                                    ' Density in the document needs to be in its own units

                                    Dim UnitsOfMeasure = SEDoc.UnitsOfMeasure
                                    Dim Factor As Double = 1.0
                                    Dim LibPropDouble As Double = CDbl(LibPropValue)
                                    For Each UnitOfMeasure As SolidEdgeFramework.UnitOfMeasure In UnitsOfMeasure
                                        If UnitOfMeasure.Type = SolidEdgeConstants.UnitTypeConstants.igUnitDistance Then
                                            If UnitOfMeasure.Units = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthInch Then
                                                ' Weight conversion: 0.45359237 kg = 1 lb.  lb/in^3 = kg/m^3 * lb/kg * (m/in)^3
                                                Factor = (1 / 0.45359237) * ((25.4 / 1000) ^ 3)
                                                LibPropDouble = Factor * LibPropDouble
                                                DocVariableDict("PhysicalProperties_Density").Formula = LibPropDouble.ToString
                                            ElseIf UnitOfMeasure.Units = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthMillimeter Then
                                                Factor = 1 / (1000 * 1000 * 1000)
                                                LibPropDouble = Factor * LibPropDouble
                                                DocVariableDict("PhysicalProperties_Density").Formula = LibPropDouble.ToString
                                            Else
                                                ExitStatus = 1
                                                ErrorMessageList.Add(String.Format("Could not update density with '{0}' units", UnitOfMeasure.Units))
                                            End If
                                        End If
                                    Next
                                End If

                            Case SolidEdgeFramework.MatTablePropIndex.seCoefOfThermalExpansion
                                UC.SetPropValue(SEDoc, "System", "Coef. of Thermal Exp", ModelIdx, AutoAdd, CDbl(LibPropValue))
                            Case SolidEdgeFramework.MatTablePropIndex.seThermalConductivity
                                UC.SetPropValue(SEDoc, "System", "Thermal Conductivity", ModelIdx, AutoAdd, CDbl(LibPropValue))
                            Case SolidEdgeFramework.MatTablePropIndex.seSpecificHeat
                                UC.SetPropValue(SEDoc, "System", "Specific Heat", ModelIdx, AutoAdd, CDbl(LibPropValue))
                            Case SolidEdgeFramework.MatTablePropIndex.seModulusElasticity
                                UC.SetPropValue(SEDoc, "System", "Modulus of Elasticity", ModelIdx, AutoAdd, CDbl(LibPropValue))
                            Case SolidEdgeFramework.MatTablePropIndex.sePoissonRatio
                                UC.SetPropValue(SEDoc, "System", "Poisson's Ratio", ModelIdx, AutoAdd, CDbl(LibPropValue))
                            Case SolidEdgeFramework.MatTablePropIndex.seYieldStress
                                UC.SetPropValue(SEDoc, "System", "Yield Stress", ModelIdx, AutoAdd, CDbl(LibPropValue))
                            Case SolidEdgeFramework.MatTablePropIndex.seUltimateStress
                                UC.SetPropValue(SEDoc, "System", "Ultimate Stress", ModelIdx, AutoAdd, CDbl(LibPropValue))
                            Case SolidEdgeFramework.MatTablePropIndex.seElongation
                                UC.SetPropValue(SEDoc, "System", "Elongation", ModelIdx, AutoAdd, CDbl(LibPropValue))
                        End Select

                    Next

                    'Dim seFaceStyle = SolidEdgeFramework.MatTablePropIndex.seFaceStyle

                    'Dim OldDocFaceStyleName As Object = Nothing
                    'MatTable.GetMaterialPropValueFromDoc(
                    '                SEDoc, seFaceStyle, OldDocFaceStyleName)
                    'SEApp.DoIdle()

                    'Dim OldLibFaceStyleName As Object = Nothing
                    'MatTable.GetMaterialPropValueFromLibrary(
                    '                MatTableMaterial.ToString, ActiveMaterialLibrary, seFaceStyle, OldLibFaceStyleName)
                    'SEApp.DoIdle()

                    'MatTable.SetMaterialPropValueToLibrary(
                    '                MatTableMaterial.ToString, ActiveMaterialLibrary, seFaceStyle, OldDocFaceStyleName)
                    'SEApp.DoIdle()

                    'SEDoc.Save()
                    'SEApp.DoIdle()

                    'Dim MatOOD As Boolean
                    'Dim GageOOD As Boolean
                    'MatTable.GetOODStatusofMaterialAndGage(SEDoc, MatOOD, GageOOD)

                    'If MatOOD Then
                    '    MatTable.ApplyMaterialToDoc(
                    '                SEDoc, MatTableMaterial.ToString, ActiveMaterialLibrary)
                    '    SEApp.DoIdle()

                    '    SEDoc.Save()
                    '    SEApp.DoIdle()
                    'End If

                    'MatTable.SetMaterialPropValueToLibrary(
                    '                MatTableMaterial.ToString, ActiveMaterialLibrary, seFaceStyle, OldLibFaceStyleName)
                    'SEApp.DoIdle()
                End If

            Else  ' Not NewWay
                ' Face styles are not always updated, especially on imported files.
                Try
                    MatTable.ApplyMaterialToDoc(SEDoc, MatTableMaterial.ToString, ActiveMaterialLibrary)
                    'UpdateFaces()
                Catch ex As Exception
                    ExitStatus = 1
                    ErrorMessageList.Add("Some face styles may not have been updated.  Please verify results.")
                End Try
            End If

        End If

        If Me.UpdateFaceStyles Then
            Dim SupplementalErrorMessageList = UpdateFaces()
            If SupplementalErrorMessageList.Count > 0 Then
                ExitStatus = 1
                For Each s As String In SupplementalErrorMessageList
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                Next
            End If
        End If

        SEDoc.Save()
        SEApp.DoIdle()

        MatTable = Nothing
        SEApp.DoIdle()

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

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

    Private Function GetMaterialPropertyMismatches(
        MatTable As SolidEdgeFramework.MatTable,
        MatTableMaterial As Object,
        ActiveMaterialLibrary As String
        ) As List(Of SolidEdgeFramework.MatTablePropIndex)

        Dim Mismatches As New List(Of SolidEdgeFramework.MatTablePropIndex)

        Dim ErrorMessage As String = ""

        Dim MatTableProps As Array = System.Enum.GetValues(GetType(SolidEdgeFramework.MatTablePropIndex))
        Dim MatTableProp As SolidEdgeFramework.MatTablePropIndex
        Dim DocPropValue As Object = Nothing
        Dim LibPropValue As Object = Nothing

        For Each MatTableProp In System.Enum.GetValues(GetType(SolidEdgeConstants.MatTablePropIndex))
            If MatTableProp = SolidEdgeFramework.MatTablePropIndex.seVSPlusStyle Then
                Continue For
            End If

            ' Populate 'LibPropValue'
            MatTable.GetMaterialPropValueFromLibrary(MatTableMaterial.ToString, ActiveMaterialLibrary, MatTableProp, LibPropValue)

            ' Populate 'DocPropValue'
            MatTable.GetMaterialPropValueFromDoc(SEDoc, MatTableProp, DocPropValue)

            ' MatTableProps are either Double or String.
            If (DocPropValue.GetType = GetType(Double)) And (LibPropValue.GetType = GetType(Double)) Then
                ' Double types may have insignificant differences.
                If Not CloseEnough(CType(DocPropValue, Double), CType(LibPropValue, Double)) Then
                    Mismatches.Add(MatTableProp)
                End If
            Else
                If CType(DocPropValue, String) <> CType(LibPropValue, String) Then
                    Mismatches.Add(MatTableProp)
                End If
            End If
            DocPropValue = Nothing
            LibPropValue = Nothing
        Next

        Return Mismatches
    End Function

    Private Function UpdateFaces() As List(Of String)

        Dim ErrorMessageList As New List(Of String)
        'Dim UpdatesComplete As Boolean = True

        Dim Models As SolidEdgePart.Models = Nothing
        Dim Model As SolidEdgePart.Model
        Dim Body As SolidEdgeGeometry.Body
        Dim Faces As SolidEdgeGeometry.Faces
        Dim Face As SolidEdgeGeometry.Face

        Dim Features As SolidEdgePart.Features

        Dim FaceOverrides As New Dictionary(Of Integer, SolidEdgeFramework.FaceStyle)
        Dim FeatureFaceOverrides As New Dictionary(Of Integer, SolidEdgeFramework.FaceStyle)
        Dim BodyStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim DocFaceStyles As SolidEdgeFramework.FaceStyles = Nothing
        Dim FinishFaceStyle As SolidEdgeFramework.FaceStyle
        Dim FeatureNames As New List(Of String)
        Dim FeatureName As String


        Dim UC As New UtilsCommon
        Dim DocType = UC.GetDocType(SEDoc)

        Select Case DocType
            Case "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                Models = tmpSEDoc.Models
                DocFaceStyles = CType(tmpSEDoc.FaceStyles, SolidEdgeFramework.FaceStyles)
            Case "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                Models = tmpSEDoc.Models
                DocFaceStyles = CType(tmpSEDoc.FaceStyles, SolidEdgeFramework.FaceStyles)
        End Select

        If (Models.Count > 0) And (Models.Count < 300) Then
            For Each Model In Models

                ' Some Models do not have a Body
                Try

                    ' ###### COLLECT STYLE OVERRIDE INFORMATION ######

                    Body = CType(Model.Body, SolidEdgeGeometry.Body)
                    If Body.Style IsNot Nothing Then
                        BodyStyle = Body.Style
                    End If

                    'Body.Faces
                    Faces = CType(Body.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll), SolidEdgeGeometry.Faces)

                    For Each Face In Faces
                        FaceOverrides(Face.ID) = Face.Style
                    Next

                    Features = Model.Features
                    For Each Feature In Features
                        'FeatureName = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(Of String)(Feature, "Name")
                        FeatureName = HCComObject.GetPropertyValue(Of String)(Feature, "Name")
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

                            ' ###### CLEAR OVERRIDES ######

                            ' The method Body.ClearOverrides() apparently makes a new body, rendeering the previous Body invalid.
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

                            If Not Me.RemoveFaceStyleOverrides Then
                                ' Since we have a new Body and possibly different FaceIDs
                                ' Need to transfer old face info to the new items
                                ' 
                                Dim i As Integer = 0
                                For Each Face In Faces
                                    If FaceStyleList(i) IsNot Nothing Then
                                        FaceOverrides(Face.ID) = FaceStyleList(i)
                                    End If
                                    i += 1
                                Next
                            End If

                        Catch ex As Exception
                            Dim s As String = "Could not process model faces"
                            If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                            Return ErrorMessageList
                        End Try

                        SEApp.DoIdle()

                        If Me.UseFinishFaceStyle Then
                            If Not Me.ExcludedFinishesList.Contains(Me.FinishName) Then
                                FinishFaceStyle = CType(DocFaceStyles.Item(FinishName), SolidEdgeFramework.FaceStyle)
                                If FinishFaceStyle IsNot Nothing Then
                                    Body.Style = FinishFaceStyle
                                Else
                                    Dim s As String = String.Format("Finish face style '{0}' not found", FinishFaceStyle.StyleName)
                                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                                End If
                            End If
                        End If

                        If Not RemoveFaceStyleOverrides Then
                            If BodyStyle IsNot Nothing And Not Me.UseFinishFaceStyle Then
                                Body.Style = BodyStyle
                            End If

                            Dim Count As Integer = 0

                            For Each Face In Faces
                                If FaceOverrides.Keys.Contains(Face.ID) Then
                                    Face.Style = FaceOverrides(Face.ID)
                                    Count += 1
                                    If Count Mod 50 = 0 Then
                                        SEApp.DoIdle()
                                    End If
                                End If
                            Next

                        End If

                        FaceOverrides.Clear()  ' Get ready for the next Model

                    ElseIf FaceOverrides.Count > MaxFacesToProcess Then
                        Dim s As String = String.Format("Number of faces '{0}' exceeds maximum '{1}'", FaceOverrides.Count, MaxFacesToProcess)
                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                    End If

                Catch ex As Exception
                    Dim s As String = "Could not process model"
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)

                End Try
            Next

        ElseIf Models.Count >= 300 Then
            Dim s As String = String.Format("Number of models '{0}' exceeds maximum '{1}'", Models.Count, 300)
            If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
        End If

        Return ErrorMessageList

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

        'Dim FeatureType = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(
        'Of SolidEdgePart.FeatureTypeConstants)(Feature, "Type", CType(0, SolidEdgePart.FeatureTypeConstants))
        Dim FeatureType = HCComObject.GetPropertyValue(
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
                        'Dim FeatureName = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(Of String)(Feature, "Name")
                        Dim FeatureName = HCComObject.GetPropertyValue(Of String)(Feature, "Name")
                        'ExitStatus = 1
                        'ErrorMessageList.Add(String.Format("{0} (FeatureType={1}) not processed.  Please verify results.", FeatureName, FeatureType.ToString))
                End Select

        End Select

        Return FeatureFaceOverrides


    End Function

    Private Function CheckUpdateMaterialStartConditions(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        Models As SolidEdgePart.Models,
        MaxModelCount As Integer,
        CurrentMaterialName As String,
        MaterialList As Object
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        If SEDoc.ReadOnly Then
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
            Return ErrorMessageList
        End If

        If Models.Count > MaxModelCount Then
            ErrorMessageList.Add(String.Format("{0} models exceeds maximum to process", Models.Count.ToString))
            Return ErrorMessageList
        End If

        If Not CurrentMaterialNameInLibrary(CurrentMaterialName, MaterialList) Then
            If CurrentMaterialName = "" Then
                ErrorMessageList.Add(String.Format("Material 'None' not in {0}", ActiveMaterialLibrary))
            Else
                ErrorMessageList.Add(String.Format("Material '{0}' not in {1}", CurrentMaterialName, ActiveMaterialLibrary))
            End If
            Return ErrorMessageList
        End If

        Return ErrorMessageList
    End Function

End Class
