Option Strict On

Public Class UtilsMaterials

    Private Property SEApp As SolidEdgeFramework.Application
    Private Property SEDoc As SolidEdgeFramework.SolidEdgeDocument
    Private Property ActiveMaterialLibrary As String
    Private Property RetainFaceStyleOverrides As Boolean
    Private Property UpdateFaceStyles As Boolean
    Private Property UseFinishFaceStyle As Boolean
    Private Property FinishName As String
    Private Property ExcludedFinishesList As List(Of String)
    Private Property OverrideBodyFaceStyle As Boolean
    Private Property OverrideMaterialFaceStyle As Boolean

    Public Sub MaterialNotInMaterialTable(
        ByVal _SEApp As SolidEdgeFramework.Application,
        ByVal _SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        _ActiveMaterialLibrary As String,
        ErrorLogger As Logger
        )

        _ActiveMaterialLibrary = System.IO.Path.GetFileNameWithoutExtension(_ActiveMaterialLibrary)

        Dim UC As New UtilsCommon

        Dim DocType = UC.GetDocType(_SEDoc)
        Dim Models As SolidEdgePart.Models = Nothing
        Dim IsWeldment As Boolean = False

        Select Case DocType
            Case "par"
                Dim tmpSEDoc = CType(_SEDoc, SolidEdgePart.PartDocument)
                Models = tmpSEDoc.Models
            Case "psm"
                Dim tmpSEDoc = CType(_SEDoc, SolidEdgePart.SheetMetalDocument)
                Models = tmpSEDoc.Models
            Case "asm"
                Dim tmpSEDoc = CType(_SEDoc, SolidEdgeAssembly.AssemblyDocument)
                IsWeldment = tmpSEDoc.WeldmentAssembly
        End Select

        If ((DocType = "asm") And (IsWeldment)) Or (Models IsNot Nothing AndAlso Models.Count > 0) Then

            Dim MatTable As SolidEdgeFramework.MatTable = _SEApp.GetMaterialTable()

            Dim CurrentMaterialName As String = ""
            MatTable.GetCurrentMaterialName(_SEDoc, CurrentMaterialName)

            'Dim MaterialLibList As Object = Nothing ' Populated next
            'Dim NumMaterialLibraries As Integer '     Populated next
            'MatTable.GetMaterialLibraryList(MaterialLibList, NumMaterialLibraries)

            'See if the CurrentMaterialName is in the ActiveLibrary
            Dim NumMaterials As Integer '          Populated next
            Dim MaterialList As Object = Nothing ' Populated next
            MatTable.GetMaterialListFromLibrary(_ActiveMaterialLibrary, NumMaterials, MaterialList)

            'Dim MaterialInLibrary As Boolean = False
            'For Each MatTableMaterial As Object In CType(MaterialList, System.Array)
            '    If MatTableMaterial.ToString.ToLower.Trim = CurrentMaterialName.ToLower.Trim Then
            '        MaterialInLibrary = True
            '        Exit For
            '    End If
            'Next

            If Not CurrentMaterialNameInLibrary(CurrentMaterialName, MaterialList) Then
                If CurrentMaterialName = "" Then
                    ErrorLogger.AddMessage($"Material 'None' not in {_ActiveMaterialLibrary}")
                Else
                    ErrorLogger.AddMessage($"Material '{CurrentMaterialName}' not in {_ActiveMaterialLibrary}")
                End If
            End If
        End If

    End Sub


    Public Sub UpdateMaterialFromMaterialTable(
        ByVal _SEApp As SolidEdgeFramework.Application,
        ByVal _SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        _ActiveMaterialLibrary As String,
        _RetainFaceStyleOverrides As Boolean,
        _UpdateFaceStyles As Boolean,
        _UseFinishFaceStyle As Boolean,
        _FinishName As String,
        _ExcludedFinishesList As List(Of String),
        _OverrideBodyFaceStyle As Boolean,
        _OverrideMaterialFaceStyle As Boolean,
        ErrorLogger As Logger
        )

        ArgsToClassProperties(_SEApp, _SEDoc, _ActiveMaterialLibrary, _RetainFaceStyleOverrides, _UpdateFaceStyles, _UseFinishFaceStyle,
                              _FinishName, _ExcludedFinishesList, _OverrideBodyFaceStyle, _OverrideMaterialFaceStyle)

        Dim UC As New UtilsCommon


        ' ###### GET THE MATERIAL TABLE AND A LIST OF MATERIALS IT CONTAINS ######

        Dim MatTable As SolidEdgeFramework.MatTable = Me.SEApp.GetMaterialTable()

        Dim NumMaterials As Integer '          Populated next
        Dim MaterialList As Object = Nothing ' Populated next
        MatTable.GetMaterialListFromLibrary(Me.ActiveMaterialLibrary, NumMaterials, MaterialList)


        ' ###### GET THE DOCUMENT OBJECTS ######

        Dim Models As SolidEdgePart.Models = Nothing
        Dim CurrentMaterialName As String = ""

        Select Case UC.GetDocType(Me.SEDoc)
            Case "par"
                Dim tmpSEDoc = CType(Me.SEDoc, SolidEdgePart.PartDocument)
                Models = tmpSEDoc.Models
                MatTable.GetCurrentMaterialName(tmpSEDoc, CurrentMaterialName)
                If Models.Count = 0 Then
                    ' Not an error
                    Exit Sub
                End If
            Case "psm"
                Dim tmpSEDoc = CType(Me.SEDoc, SolidEdgePart.SheetMetalDocument)
                Models = tmpSEDoc.Models
                MatTable.GetCurrentMaterialName(tmpSEDoc, CurrentMaterialName)
                If Models.Count = 0 Then
                    ' Not an error
                    Exit Sub
                End If
        End Select


        ' ###### CHECK START CONDITIONS ######

        Dim MaxModelCount As Integer = 10
        CheckUpdateMaterialStartConditions(Me.SEDoc, Models, MaxModelCount, CurrentMaterialName, MaterialList, ErrorLogger)
        If ErrorLogger.HasErrors Then Exit Sub


        ' ###### GET THE MATERIAL OBJECT FROM THE MATERIAL TABLE ######
        ' CheckUpdateMaterialStartConditions should catch if the material is not in the library

        Dim MatTableMaterial As Object = Nothing ' Populated next
        For Each tmpMatTableMaterial As Object In CType(MaterialList, System.Array)
            If tmpMatTableMaterial.ToString.ToLower.Trim = CurrentMaterialName.ToLower.Trim Then
                ' This is the one we want
                MatTableMaterial = tmpMatTableMaterial
                Exit For
            End If
        Next


        ' ###### FIX ANY MISMATCHES BETWEEN THE MATERIAL TABLE AND DOCUMENT ######

        Dim Mismatches As List(Of SolidEdgeFramework.MatTablePropIndex) = GetMaterialPropertyMismatches(MatTable, MatTableMaterial, Me.ActiveMaterialLibrary)
        If Mismatches.Count > 0 Or Me.UpdateFaceStyles Then
            FixMismatches(Mismatches, MatTable, MatTableMaterial, ErrorLogger)
        End If


        ' ###### UPDATE FACE STYLES IF NEEDED ######

        If Me.UpdateFaceStyles Then ' Allowing this to proceed to catch any downstream errors.
            UpdateFaces(ErrorLogger)
        End If

        '' The caller should decide whether to save or not
        'SEDoc.Save()
        'SEApp.DoIdle()

        MatTable = Nothing
        SEApp.DoIdle()

    End Sub

    Private Sub ArgsToClassProperties(
        ByVal _SEApp As SolidEdgeFramework.Application,
        ByVal _SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        _ActiveMaterialLibrary As String,
        _RetainFaceStyleOverrides As Boolean,
        _UpdateFaceStyles As Boolean,
        _UseFinishFaceStyle As Boolean,
        _FinishName As String,
        _ExcludedFinishesList As List(Of String),
        _OverrideBodyFaceStyle As Boolean,
        _OverrideMaterialFaceStyle As Boolean)

        Me.SEApp = _SEApp
        Me.SEDoc = _SEDoc
        Me.ActiveMaterialLibrary = _ActiveMaterialLibrary
        Me.RetainFaceStyleOverrides = _RetainFaceStyleOverrides
        Me.UpdateFaceStyles = _UpdateFaceStyles
        Me.UseFinishFaceStyle = _UseFinishFaceStyle
        Me.FinishName = _FinishName
        Me.ExcludedFinishesList = _ExcludedFinishesList
        Me.OverrideBodyFaceStyle = _OverrideBodyFaceStyle
        Me.OverrideMaterialFaceStyle = _OverrideMaterialFaceStyle

        'Me.ActiveMaterialLibrary = System.IO.Path.GetFileNameWithoutExtension(Me.ActiveMaterialLibrary)

    End Sub

    Private Sub FixMismatches(
        Mismatches As List(Of SolidEdgeFramework.MatTablePropIndex),
        MatTable As SolidEdgeFramework.MatTable,
        MatTableMaterial As Object,
        ErrorLogger As Logger)

        Dim UC As New UtilsCommon

        ' TODO: Probably need to check if RetainFaceStyleOverrides is enabled.  I think this doesn't do that.
        If Me.UpdateFaceStyles Then
            ' Face styles are not always updated, especially on imported files.
            ' I wonder if ApplyMaterialToDoc is calling Body.ClearOverrides.
            Try
                MatTable.ApplyMaterialToDoc(Me.SEDoc, MatTableMaterial.ToString, Me.ActiveMaterialLibrary)
            Catch ex As Exception
                ErrorLogger.AddMessage($"Some face styles may not have been updated.  Please verify results.  Exception: {ex.Message}")
            End Try
        Else
            For Each MismatchProp As SolidEdgeFramework.MatTablePropIndex In Mismatches
                Dim tf As Boolean
                tf = MismatchProp = SolidEdgeFramework.MatTablePropIndex.seFaceStyle
                tf = tf Or MismatchProp = SolidEdgeFramework.MatTablePropIndex.seFillStyle
                If tf Then
                    Continue For
                End If

                Dim LibPropValue As Object = Nothing
                MatTable.GetMaterialPropValueFromLibrary(
                        MatTableMaterial.ToString, Me.ActiveMaterialLibrary, MismatchProp, LibPropValue)
                SEApp.DoIdle()

                'Dim ModelIdx As Integer = 0
                'Dim AutoAdd As Boolean = False

                FixMismatchProp(MismatchProp, LibPropValue, ErrorLogger)

                'Select Case MismatchProp
                '    Case SolidEdgeFramework.MatTablePropIndex.seDensity
                '        ' Density is a variable, not a property

                '        Dim DocVariableDict As New Dictionary(Of String, SolidEdgeFramework.variable)

                '        Dim UseDisplayName As Boolean = True
                '        DocVariableDict = UC.GetDocVariables(SEDoc, UseDisplayName)

                '        If DocVariableDict.Keys.Contains("PhysicalProperties_Density") Then
                '            ' Density from the material table is in kg/m^3
                '            ' Density in the document needs to be in its own units

                '            Dim UnitsOfMeasure = SEDoc.UnitsOfMeasure
                '            Dim Factor As Double = 1.0
                '            Dim LibPropDouble As Double = CDbl(LibPropValue)
                '            For Each UnitOfMeasure As SolidEdgeFramework.UnitOfMeasure In UnitsOfMeasure
                '                If UnitOfMeasure.Type = SolidEdgeConstants.UnitTypeConstants.igUnitDistance Then
                '                    If UnitOfMeasure.Units = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthInch Then
                '                        ' Weight conversion: 0.45359237 kg = 1 lb.  lb/in^3 = kg/m^3 * lb/kg * (m/in)^3
                '                        Factor = (1 / 0.45359237) * ((25.4 / 1000) ^ 3)
                '                        LibPropDouble = Factor * LibPropDouble
                '                        DocVariableDict("PhysicalProperties_Density").Formula = LibPropDouble.ToString
                '                    ElseIf UnitOfMeasure.Units = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthMillimeter Then
                '                        Factor = 1 / (1000 * 1000 * 1000)
                '                        LibPropDouble = Factor * LibPropDouble
                '                        DocVariableDict("PhysicalProperties_Density").Formula = LibPropDouble.ToString
                '                    Else
                '                        ErrorLogger.AddMessage($"Could not update density with '{UnitOfMeasure.Units}' units")
                '                    End If
                '                End If
                '            Next
                '        End If

                '    Case SolidEdgeFramework.MatTablePropIndex.seCoefOfThermalExpansion
                '        UC.SetPropValue(SEDoc, "System", "Coef. of Thermal Exp", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
                '    Case SolidEdgeFramework.MatTablePropIndex.seThermalConductivity
                '        UC.SetPropValue(SEDoc, "System", "Thermal Conductivity", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
                '    Case SolidEdgeFramework.MatTablePropIndex.seSpecificHeat
                '        UC.SetPropValue(SEDoc, "System", "Specific Heat", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
                '    Case SolidEdgeFramework.MatTablePropIndex.seModulusElasticity
                '        UC.SetPropValue(SEDoc, "System", "Modulus of Elasticity", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
                '    Case SolidEdgeFramework.MatTablePropIndex.sePoissonRatio
                '        UC.SetPropValue(SEDoc, "System", "Poisson's Ratio", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
                '    Case SolidEdgeFramework.MatTablePropIndex.seYieldStress
                '        UC.SetPropValue(SEDoc, "System", "Yield Stress", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
                '    Case SolidEdgeFramework.MatTablePropIndex.seUltimateStress
                '        UC.SetPropValue(SEDoc, "System", "Ultimate Stress", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
                '    Case SolidEdgeFramework.MatTablePropIndex.seElongation
                '        UC.SetPropValue(SEDoc, "System", "Elongation", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
                'End Select

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
    End Sub

    Private Sub FixMismatchProp(
        MismatchProp As SolidEdgeFramework.MatTablePropIndex,
        LibPropValue As Object,
        ErrorLogger As Logger)

        Dim UC As New UtilsCommon

        Dim ModelIdx As Integer = 0
        Dim AutoAdd As Boolean = False

        Select Case MismatchProp

            Case SolidEdgeFramework.MatTablePropIndex.seDensity
                FixDensityProp(LibPropValue, ErrorLogger)

                '' Density is a variable, not a property

                'Dim DocVariableDict As New Dictionary(Of String, SolidEdgeFramework.variable)

                'Dim UseDisplayName As Boolean = True
                'DocVariableDict = UC.GetDocVariables(SEDoc, UseDisplayName)

                'If DocVariableDict.Keys.Contains("PhysicalProperties_Density") Then
                '    ' Density from the material table is in kg/m^3
                '    ' Density in the document needs to be in its own units

                '    Dim UnitsOfMeasure = SEDoc.UnitsOfMeasure
                '    Dim Factor As Double = 1.0
                '    Dim LibPropDouble As Double = CDbl(LibPropValue)
                '    For Each UnitOfMeasure As SolidEdgeFramework.UnitOfMeasure In UnitsOfMeasure
                '        If UnitOfMeasure.Type = SolidEdgeConstants.UnitTypeConstants.igUnitDistance Then
                '            If UnitOfMeasure.Units = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthInch Then
                '                ' Weight conversion: 0.45359237 kg = 1 lb.  lb/in^3 = kg/m^3 * lb/kg * (m/in)^3
                '                Factor = (1 / 0.45359237) * ((25.4 / 1000) ^ 3)
                '                LibPropDouble = Factor * LibPropDouble
                '                DocVariableDict("PhysicalProperties_Density").Formula = LibPropDouble.ToString
                '            ElseIf UnitOfMeasure.Units = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthMillimeter Then
                '                Factor = 1 / (1000 * 1000 * 1000)
                '                LibPropDouble = Factor * LibPropDouble
                '                DocVariableDict("PhysicalProperties_Density").Formula = LibPropDouble.ToString
                '            Else
                '                ErrorLogger.AddMessage($"Could not update density with '{UnitOfMeasure.Units}' units")
                '            End If
                '        End If
                '    Next
                'End If

            Case SolidEdgeFramework.MatTablePropIndex.seCoefOfThermalExpansion
                UC.SetPropValue(SEDoc, "System", "Coef. of Thermal Exp", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
            Case SolidEdgeFramework.MatTablePropIndex.seThermalConductivity
                UC.SetPropValue(SEDoc, "System", "Thermal Conductivity", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
            Case SolidEdgeFramework.MatTablePropIndex.seSpecificHeat
                UC.SetPropValue(SEDoc, "System", "Specific Heat", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
            Case SolidEdgeFramework.MatTablePropIndex.seModulusElasticity
                UC.SetPropValue(SEDoc, "System", "Modulus of Elasticity", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
            Case SolidEdgeFramework.MatTablePropIndex.sePoissonRatio
                UC.SetPropValue(SEDoc, "System", "Poisson's Ratio", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
            Case SolidEdgeFramework.MatTablePropIndex.seYieldStress
                UC.SetPropValue(SEDoc, "System", "Yield Stress", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
            Case SolidEdgeFramework.MatTablePropIndex.seUltimateStress
                UC.SetPropValue(SEDoc, "System", "Ultimate Stress", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
            Case SolidEdgeFramework.MatTablePropIndex.seElongation
                UC.SetPropValue(SEDoc, "System", "Elongation", ModelIdx, AutoAdd, CDbl(LibPropValue), ErrorLogger)
        End Select
    End Sub

    Private Sub FixDensityProp(
        LibPropValue As Object,
        ErrorLogger As Logger)

        ' Density is a variable, not a property

        Dim UC As New UtilsCommon

        Dim DocVariableDict As New Dictionary(Of String, SolidEdgeFramework.variable)

        Dim UseDisplayName As Boolean = True
        DocVariableDict = UC.GetDocVariables(Me.SEDoc, UseDisplayName)

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
                        ErrorLogger.AddMessage($"Could not update density with '{UnitOfMeasure.Units}' units")
                    End If
                End If
            Next
        End If

    End Sub


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


    Private Sub UpdateFaces(ErrorLogger As Logger)

        Dim UC As New UtilsCommon


        ' ###### GET OBJECTS FROM FILE #######

        Dim DocType = UC.GetDocType(SEDoc)
        Dim Models As SolidEdgePart.Models = Nothing
        Dim DocFacestyles As SolidEdgeFramework.FaceStyles = Nothing

        Select Case DocType
            Case "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                Models = tmpSEDoc.Models
                DocFacestyles = CType(tmpSEDoc.FaceStyles, SolidEdgeFramework.FaceStyles)
            Case "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                Models = tmpSEDoc.Models
                DocFacestyles = CType(tmpSEDoc.FaceStyles, SolidEdgeFramework.FaceStyles)
        End Select


        ' ###### CHECK FILE START CONDITIONS #######

        If Models.Count = 0 Then
            ' Not an error.
            Exit Sub
        End If

        Dim MaxModelsToProcess As Integer = 500
        If Models.Count > MaxModelsToProcess Then
            Dim s As String = $"Number of models {Models.Count} exceeds maximum {MaxModelsToProcess}"
            If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
            Exit Sub
        End If


        ' ###### PROCESS #######

        Dim Count As Integer = 0
        Dim Stage As String = ""

        For Each Model As SolidEdgePart.Model In Models

            Count += 1

            Try
                Dim Body As SolidEdgeGeometry.Body = Nothing
                Dim BodyOverride As SolidEdgeFramework.FaceStyle = Nothing
                Dim Faces As SolidEdgeGeometry.Faces = Nothing
                Dim FaceOverrides As New Dictionary(Of Integer, SolidEdgeFramework.FaceStyle)


                ' ###### CHECK MODEL BODY ######
                Stage = "Checking model body"

                ' Features like Union, ToggleToDesign, ConvertToSM have models with no bodies.
                Try
                    If Model.Body Is Nothing Then
                        ' Not an error
                        Continue For
                    End If
                Catch ex As Exception
                    ' Not an error
                    Continue For
                End Try


                ' ###### COLLECT INCOMING FACE STYLE INFORMATION ######
                Stage = "Collecting face styles"

                Dim Success As Boolean = True
                GetModelOverrides(Model, ErrorLogger, Success, Body, BodyOverride, Faces, FaceOverrides)

                If Faces IsNot Nothing AndAlso Faces.Count = 0 Then Continue For
                If Not Success Then Continue For

                If Me.RetainFaceStyleOverrides Then
                    Dim MaxOverridesToProcess As Integer = 5000
                    If FaceOverrides.Count > MaxOverridesToProcess Then
                        ErrorLogger.AddMessage($"Number of overridden faces {FaceOverrides.Count} exceeds maximum {MaxOverridesToProcess}")
                        Continue For
                    End If
                End If


                ' ###### CLEAR OVERRIDES ######
                Stage = "Clearing overrides"

                ' Cache incoming face styles

                Dim CachedFaceIDs As New List(Of Integer)
                For Each Face As SolidEdgeGeometry.Face In Faces
                    CachedFaceIDs.Add(Face.ID)
                Next

                Dim CachedFaceOverrides As New Dictionary(Of Integer, SolidEdgeFramework.FaceStyle)
                For Each FaceID As Integer In FaceOverrides.Keys
                    CachedFaceOverrides(FaceID) = FaceOverrides(FaceID)
                Next

                ' Clear overrides

                Dim SubStage As String = "Starting"
                Try
                    Dim NewWay As Boolean = True
                    If Not NewWay Then
                        Body.ClearOverrides()
                    Else
                        Dim FacesList As New List(Of SolidEdgeGeometry.Face)
                        For Each Face As SolidEdgeGeometry.Face In Faces
                            FacesList.Add(Face)
                        Next
                        Try
                            SubStage = "Body"
                            Body.Style = Nothing
                            SubStage = "Features"
                            For Each Feature As Object In Model.Features
                                Try
                                    HCComObject.InvokeMethod(Feature, "SetStyle", New System.Runtime.InteropServices.DispatchWrapper(Nothing))
                                Catch ex As Exception
                                    ErrorLogger.AddMessage($"Error clearing feature facestyles.  Exception: {ex.Message}")
                                End Try
                            Next
                            SubStage = "Faces"
                            Body.SetFacesStyle(Faces.Count, FacesList.ToArray, Nothing)
                        Catch ex2 As Exception
                            ErrorLogger.AddMessage($"{SubStage} {ex2.Message}")
                        End Try
                    End If

                Catch ex As Exception
                    Dim s As String = $"Could not reset body face styles.  Exception: {ex.Message}"
                    If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
                    Continue For
                End Try

                Dim HasOverrides As Boolean = CachedFaceOverrides.Count > 0 OrElse BodyOverride IsNot Nothing

                If Me.UseFinishFaceStyle Or (Me.RetainFaceStyleOverrides And HasOverrides) Then

                    ' Deal with possible new body from clear overrides.  (Legacy issue with older versions.)
                    '     Collect new face style information
                    Stage = "Re-reading body"

                    Faces = Nothing
                    FaceOverrides.Clear()

                    GetModelOverrides(Model, ErrorLogger, Success, Body, BodyOverride, Faces, FaceOverrides)

                    '     Update FaceIDs in FaceOverrides

                    If Success Then
                        Success = ReconcileFaceIDs(CachedFaceIDs, CachedFaceOverrides, Faces, FaceOverrides, ErrorLogger)
                    End If

                    If Not Success Then
                        If Me.UseFinishFaceStyle Then
                            ErrorLogger.AddMessage("Unable to apply finish face styles")
                        End If
                        If Me.RetainFaceStyleOverrides And HasOverrides Then
                            Dim OverridesCount As Integer = CachedFaceOverrides.Count
                            If BodyOverride IsNot Nothing Then OverridesCount += 1
                            ErrorLogger.AddMessage($"Unable to apply style overrides to {OverridesCount} item(s)")
                        End If
                        Continue For
                    End If


                    ' ###### APPLY FINISH FACE STYLE IF NEEDED ######
                    Stage = "Applying finish"

                    If Me.UseFinishFaceStyle Then
                        ApplyFinishFacestyles(DocFacestyles, Body, ErrorLogger)
                    End If


                    ' ###### APPLY INCOMING FACE STYLE OVERRIDES IF NEEDED ######
                    Stage = "Re-applying overrides"

                    If Me.RetainFaceStyleOverrides Then
                        ApplyOverrides(Body, BodyOverride, Faces, FaceOverrides, ErrorLogger)
                    End If

                End If

            Catch ex As Exception
                ErrorLogger.AddMessage($"Unable to process model {Count} of {Models.Count}.  Stage: {Stage}.  Exception: {ex.Message}")
            End Try

        Next

    End Sub

    Private Function ReconcileFaceIDs(
        CachedFaceIDs As List(Of Integer),
        CachedFaceOverrides As Dictionary(Of Integer, SolidEdgeFramework.FaceStyle),
        NewFaces As SolidEdgeGeometry.Faces,
        ByRef FaceOverrides As Dictionary(Of Integer, SolidEdgeFramework.FaceStyle),
        ErrorLogger As Logger
        ) As Boolean
        Dim Success As Boolean = True

        If Not NewFaces.Count = CachedFaceIDs.Count Then
            Success = False
            ErrorLogger.AddMessage("Face count mismatch after clearing overrides")
        Else
            FaceOverrides.Clear()
            For i As Integer = 0 To CachedFaceIDs.Count - 1
                If CachedFaceOverrides.Keys.Contains(CachedFaceIDs(i)) Then
                    Dim newFace As SolidEdgeGeometry.Face = CType(NewFaces.Item(i + 1), SolidEdgeGeometry.Face)
                    FaceOverrides(newFace.ID) = CachedFaceOverrides(CachedFaceIDs(i))
                End If
            Next
        End If

        Return Success
    End Function

    Private Sub ApplyFinishFacestyles(
        DocFacestyles As SolidEdgeFramework.FaceStyles,
        Body As SolidEdgeGeometry.Body,
        ErrorLogger As Logger)

        Dim tf As Boolean
        tf = Me.UseFinishFaceStyle
        tf = tf And ExcludedFinishesList IsNot Nothing
        tf = tf AndAlso Not Me.ExcludedFinishesList.Contains(Me.FinishName, StringComparer.OrdinalIgnoreCase)
        If tf Then
            Dim FinishFaceStyle As SolidEdgeFramework.FaceStyle = Nothing
            For Each tmpFaceStyle As SolidEdgeFramework.FaceStyle In DocFacestyles
                If tmpFaceStyle.StyleName.ToLower = FinishName.ToLower Then
                    FinishFaceStyle = tmpFaceStyle
                    Exit For
                End If
            Next

            If FinishFaceStyle IsNot Nothing Then
                Body.Style = FinishFaceStyle
            Else
                Dim s As String = $"Finish face style '{FinishName}' not found"
                If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
            End If
        End If

    End Sub

    Private Sub ApplyOverrides(
        Body As SolidEdgeGeometry.Body,
        BodyOverride As SolidEdgeFramework.FaceStyle,
        Faces As SolidEdgeGeometry.Faces,
        FaceOverrides As Dictionary(Of Integer, SolidEdgeFramework.FaceStyle),
        ErrorLogger As Logger)

        If BodyOverride IsNot Nothing And Not Me.UseFinishFaceStyle Then Body.Style = BodyOverride

        ' Group overridden faces by style, then apply each group in one call.
        Dim Groups As New Dictionary(Of String, List(Of Object))
        Dim StylesByName As New Dictionary(Of String, SolidEdgeFramework.FaceStyle)

        For Each Face As SolidEdgeGeometry.Face In Faces
            Dim Style As SolidEdgeFramework.FaceStyle = Nothing
            If FaceOverrides.TryGetValue(Face.ID, Style) AndAlso Style IsNot Nothing Then
                Dim Name As String = Style.StyleName
                If Not Groups.ContainsKey(Name) Then
                    Groups(Name) = New List(Of Object)
                    StylesByName(Name) = Style
                End If
                Groups(Name).Add(Face)
            End If
        Next

        For Each Name As String In Groups.Keys
            Dim FaceArray As Array = Groups(Name).ToArray()
            Try
                Body.SetFacesStyle(FaceArray.Length, FaceArray, StylesByName(Name))
            Catch ex As Exception
                ErrorLogger.AddMessage($"Unable to set face style '{Name}'")
            End Try
        Next
    End Sub

    Private Sub GetModelOverrides(
         Model As SolidEdgePart.Model,
         ErrorLogger As Logger,
         ByRef Success As Boolean,
         ByRef Body As SolidEdgeGeometry.Body,
         ByRef BodyOverride As SolidEdgeFramework.FaceStyle,
         ByRef Faces As SolidEdgeGeometry.Faces,
         ByRef FaceOverrides As Dictionary(Of Integer, SolidEdgeFramework.FaceStyle))

        Success = True

        Try
            Body = CType(Model.Body, SolidEdgeGeometry.Body)
        Catch ex As Exception
            Success = False
            ErrorLogger.AddMessage($"Could not process model body.  Exception: {ex.Message}")
            Exit Sub
        End Try

        If Body.Style IsNot Nothing Then
            BodyOverride = Body.Style
        End If

        'Body.Faces
        Faces = CType(Body.Faces(SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll), SolidEdgeGeometry.Faces)

        If Faces.Count = 0 Then
            ' Not an error.
            'Success = False
            Exit Sub
        End If

        Dim MaxFacesToProcess As Integer = 5000
        If Faces.Count > MaxFacesToProcess And Me.RetainFaceStyleOverrides Then
            Success = False
            Dim s As String = $"Number of faces {Faces.Count} exceeds maximum {MaxFacesToProcess}"
            'If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
            ErrorLogger.AddMessage(s)
            Exit Sub
        End If

        For Each Face As SolidEdgeGeometry.Face In Faces
            ' In rare cases, Face.Style causes an exception
            Try
                If Face.Style IsNot Nothing Then
                    FaceOverrides(Face.ID) = Face.Style
                End If
            Catch ex As Exception
                ' Can't use it.  No need to report.
            End Try
        Next

        '  ###### FEATURE FACE STYLES ######

        ' If a face does not have an individually assigned FaceStyle, pick it
        ' up from the feature FaceStyle if one is assigned at that level.

        Dim Features As SolidEdgePart.Features = Model.Features
        For Each Feature As Object In Features

            Dim FeatureFaceStyle As SolidEdgeFramework.FaceStyle = GetFeatureFaceStyle(Feature)

            If FeatureFaceStyle IsNot Nothing Then
                Dim igQueryAll = SolidEdgeGeometry.FeatureTopologyQueryTypeConstants.igQueryAll
                Dim FeatureFaces = CType(HCComObject.InvokeMethod(Feature, "Faces", igQueryAll), SolidEdgeGeometry.Faces)

                For Each FeatureFace As SolidEdgeGeometry.Face In FeatureFaces
                    If Not FaceOverrides.ContainsKey(FeatureFace.ID) Then
                        FaceOverrides(FeatureFace.ID) = FeatureFaceStyle
                    End If
                Next
            End If
        Next
    End Sub

    Private Function GetFeatureFaceStyle(Feature As Object) As SolidEdgeFramework.FaceStyle

        ' These features do not have a FaceStyle override: Box, Cylinder, and some others.  

        ' These didn't work previously: ConvertToSM, FaceSet, Hem, Subtract, Thread, Transform (ConvertPartToSM?), Union.
        ' They can legitimately have a model with no body.  That's now checked.

        ' Every feature type that supports a face style override has the same
        ' GetStyle, Faces and SetStyle members, but on unrelated interop types,
        ' so they're called by name.  A feature type without them (BlueSurf,
        ' FaceMove, some CopiedParts...) throws, and is treated as having no
        ' override.
        Try
            Return CType(HCComObject.InvokeMethod(Feature, "GetStyle"), SolidEdgeFramework.FaceStyle)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Sub CheckUpdateMaterialStartConditions(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        Models As SolidEdgePart.Models,
        MaxModelCount As Integer,
        CurrentMaterialName As String,
        MaterialList As Object,
        ErrorLogger As Logger
        )

        If SEDoc.ReadOnly Then
            ErrorLogger.AddMessage("Cannot save document marked 'Read Only'")
            Exit Sub
        End If

        If Models.Count > MaxModelCount Then
            ErrorLogger.AddMessage($"Number of models {Models.Count} exceeds maximum {MaxModelCount}")
            Exit Sub
        End If

        If Not CurrentMaterialNameInLibrary(CurrentMaterialName, MaterialList) Then
            If CurrentMaterialName = "" Then
                ErrorLogger.AddMessage($"Material 'None' not in {ActiveMaterialLibrary}")
            Else
                ErrorLogger.AddMessage($"Material '{CurrentMaterialName}' not in {ActiveMaterialLibrary}")
            End If
        End If

    End Sub

End Class
