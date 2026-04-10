Option Strict On

Imports System.IO
Imports System.Text
Imports SolidEdgeExplorerDLL
Imports SolidEdgeFramework

Public Class Form_SolidEdgeExplorer

    Public FileName As String

    Public Sub UpdateTree()

        Dim ext As String = Path.GetExtension(FileName).ToLower()

        If ext = ".par" OrElse ext = ".psm" OrElse ext = ".asm" Then

            Try
                Dim tmpPartsLiteData As New SolidEdgeExplorerDLL.PartsLiteData
                tmpPartsLiteData.FindData(FileName)

                Dim tmpVariableInfos As New SolidEdgeExplorerDLL.CustomPropertyVariableInfo
                tmpVariableInfos.FindData(FileName)

                Dim tmpProperties As New SolidEdgeExplorerDLL.Properties
                tmpProperties.FindData(FileName)

                Dim tmpPSMCluster0 As New SolidEdgeExplorerDLL.PSMCluster0
                tmpPSMCluster0.FindData(FileName)

                ''Gather unit constants for in & mm
                'Dim tmpUnitDict As New Dictionary(Of String, String)
                'For Each DefaultUnit As SolidEdgeExplorerDLL.DefaultUnit In tmpPSMCluster0.DefaultUnits
                '    If DefaultUnit.Name.Contains("Default") Then
                '        If DefaultUnit.Name.Contains("Primary") Or DefaultUnit.Name.Contains("Secondary") Or DefaultUnit.Name.Contains("Tertiary") Then
                '            tmpUnitDict(DefaultUnit.Name) = CStr(DefaultUnit.Value)
                '        End If
                '    End If
                'Next

                PopulateTreeView(FileName, tmpPartsLiteData, tmpVariableInfos, tmpProperties, tmpPSMCluster0)

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If

        If ext = ".dft" Then
            Try

                Dim tmpVariableInfos As New SolidEdgeExplorerDLL.CustomPropertyVariableInfo
                tmpVariableInfos.FindData(FileName)

                Dim tmpProperties As New SolidEdgeExplorerDLL.Properties
                tmpProperties.FindData(FileName)

                Dim tmpPSMCluster0 As New SolidEdgeExplorerDLL.PSMCluster0
                tmpPSMCluster0.FindData(FileName)

                PopulateTreeView(FileName, Nothing, tmpVariableInfos, tmpProperties, tmpPSMCluster0)

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub

    Private Sub PopulateTreeView(File As String, PartsLiteData As PartsLiteData, CustomPropertyVariableInfos As CustomPropertyVariableInfo, Properties As SolidEdgeExplorerDLL.Properties, PSMCluster0 As PSMCluster0)


        TreeView1.BeginUpdate()

        TreeView1.Nodes.Clear() ' Puliamo il TreeView

        Dim FileNode As New TreeNode("File: " & File)
        TreeView1.Nodes.Add(FileNode)

        Dim DefaultsNode As New TreeNode("Defaults")
        For Each tmpDefaul In PSMCluster0.DefaultUnits
            Dim tmpDefaulNode As New TreeNode(tmpDefaul.Name)
            tmpDefaulNode.Nodes.Add("Unknown1: " & tmpDefaul.Unknown1.ToString)
            tmpDefaulNode.Nodes.Add("Unknown2: " & tmpDefaul.Unknown2.ToString)
            tmpDefaulNode.Nodes.Add("ValueType: " & tmpDefaul.ValueType.ToString)
            If tmpDefaul.Name.StartsWith("Default") Then
                tmpDefaulNode.Nodes.Add("Value: " & tmpDefaul.Value.ToString & "   " & New Utilities.UnitOfMeasure(CInt(tmpDefaul.Value)).Description)
            Else
                tmpDefaulNode.Nodes.Add("Value: " & tmpDefaul.Value.ToString)
            End If
            DefaultsNode.Nodes.Add(tmpDefaulNode)
        Next
        TreeView1.Nodes.Add(DefaultsNode)

        Dim PropertiesNode As New TreeNode("Properties")
        For Each PropertySet In Properties.PropertySets
            Dim tmpPropertySetNode As New TreeNode(PropertySet.Name)
            For Each item In PropertySet.Items
                Dim tmpItemNode As New TreeNode(item.OLEProp.PropertyName & " = " & item.Value.ToString)
                tmpPropertySetNode.Nodes.Add(tmpItemNode)
            Next
            PropertiesNode.Nodes.Add(tmpPropertySetNode)
        Next
        TreeView1.Nodes.Add(PropertiesNode)


        If Not IsNothing(PartsLiteData) Then

            Dim NamedViewsNode As New TreeNode("NamedViews (" & PartsLiteData.NamedViews.Count.ToString & "/" & PartsLiteData.ExpectedNumNamedViews.ToString & ")")
            For Each view As SolidEdgeExplorerDLL.NamedView In PartsLiteData.NamedViews
                ' Nodo principale con il nome della vista
                Dim viewNode As New TreeNode(view.Name)

                ' Aggiungiamo le proprietà come sotto-nodi
                viewNode.Nodes.Add("Description: " & view.Description)
                viewNode.Nodes.Add("plFlags: " & view.plFlags.ToString())
                viewNode.Nodes.Add("Eye: " & String.Join(", ", view.Eye))
                viewNode.Nodes.Add("Target: " & String.Join(", ", view.Target))
                viewNode.Nodes.Add("Up: " & String.Join(", ", view.Up))
                viewNode.Nodes.Add("NearClip: " & view.NearClip.ToString())
                viewNode.Nodes.Add("FarClip: " & view.FarClip.ToString())
                viewNode.Nodes.Add("FrameWidth: " & view.FrameWidth.ToString())
                viewNode.Nodes.Add("FrameHeight: " & view.FrameHeight.ToString())
                viewNode.Nodes.Add("FrameEyeX: " & view.FrameEyeX.ToString())
                viewNode.Nodes.Add("FrameEyeY: " & view.FrameEyeY.ToString())
                viewNode.Nodes.Add("FrameScale: " & view.FrameScale.ToString())

                ' Aggiungiamo il nodo principale al TreeView
                NamedViewsNode.Nodes.Add(viewNode)
            Next
            TreeView1.Nodes.Add(NamedViewsNode)


            Dim featuresNode As New TreeNode("Features (" & PartsLiteData.Features.Count.ToString & "/" & PartsLiteData.ExpectedNumFeatures.ToString & ")")
            For Each feature As Feature In PartsLiteData.Features
                ' Nodo principale con il nome della feature
                Dim featureNode As New TreeNode(feature.Name)

                ' Aggiungiamo le proprietà come sotto-nodi
                featureNode.Nodes.Add("ID: " & feature.Metadata1.ToString())
                featureNode.Nodes.Add("Feature Type: " & feature.Metadata2.ToString())
                If feature.Length > 2 Then featureNode.Nodes.Add("Metadata 3: " & feature.Metadata3.ToString())
                If feature.Length > 3 Then featureNode.Nodes.Add("Metadata 4: " & feature.Metadata4.ToString())
                If feature.Length > 4 Then featureNode.Nodes.Add("Metadata 5: " & feature.Metadata5.ToString())
                If feature.Length > 5 Then featureNode.Nodes.Add("Metadata 6: " & feature.Metadata6.ToString())
                If feature.Length > 6 Then featureNode.Nodes.Add("Metadata 7: " & feature.Metadata7.ToString())

                ' Aggiungiamo il nodo principale al TreeView
                featuresNode.Nodes.Add(featureNode)
            Next
            TreeView1.Nodes.Add(featuresNode)


            Dim variablesNode As New TreeNode("Variables (" & PartsLiteData.Variables.Count.ToString & "/" & PartsLiteData.ExpectedNumVariables.ToString & ")")
            For Each variable As SolidEdgeExplorerDLL.Variable In PartsLiteData.Variables
                ' Nodo principale con il nome della variabile

                Dim V As Double = UserUnits(variable, PSMCluster0)

                'Dim variableNode As New TreeNode(variable.Name & " = " & variable.Value.ToString)
                Dim variableNode As New TreeNode(variable.Name & " = " & V.ToString)

                ' Aggiungiamo le proprietà come sotto-nodi
                variableNode.Nodes.Add("ID: " & variable.ID.ToString())
                variableNode.Nodes.Add("Unit Type: " & variable.UnitType & "   " & New Utilities.UnitTypeConstant(CInt(variable.UnitType)).Description)
                variableNode.Nodes.Add("Database Value: " & variable.Value.ToString())
                variableNode.Nodes.Add("User Value: " & V.ToString())

                If CustomPropertyVariableInfos.VariableInfos.Exists(Function(x) x.Variable_ID = variable.ID) Then
                    variableNode.Nodes.Add("Exposed property ID: " & CustomPropertyVariableInfos.VariableInfos.Find(Function(x) x.Variable_ID = variable.ID).Property_ID.ToString)
                    variableNode.NodeFont = New Font(TreeView1.Font, FontStyle.Bold)
                    variableNode.BackColor = Color.Gainsboro
                End If


                ' Aggiungiamo il nodo principale al TreeView
                variablesNode.Nodes.Add(variableNode)
            Next
            TreeView1.Nodes.Add(variablesNode)

        End If

        For Each node As TreeNode In TreeView1.Nodes
            node.Expand()
        Next

        TreeView1.EndUpdate()

    End Sub

    Private Function UserUnits(
        InVar As SolidEdgeExplorerDLL.Variable,
        PSMCluster0 As SolidEdgeExplorerDLL.PSMCluster0
        ) As Double

        Dim InValue As Double = InVar.Value
        Dim SentinelValue As Double = 123454321
        Dim OutValue As Double = SentinelValue

        Dim LUTDefaultNameToUnitType As New Dictionary(Of String, Integer)

        For Each DefaultUnit As SolidEdgeExplorerDLL.DefaultUnit In PSMCluster0.DefaultUnits
            If DefaultUnit.Name.Contains("Default") And DefaultUnit.Name.Contains("Primary") Then
                Dim tmpName As String = DefaultUnit.Name.Replace(vbNullChar, "")
                LUTDefaultNameToUnitType(tmpName) = CInt(DefaultUnit.Value)
            End If
        Next

        Dim UnitConstant As Integer = 345 ' Default to SeNone

        Select Case InVar.UnitType
            Case 1
                'Name = "igUnitDistance"
                UnitConstant = LUTDefaultNameToUnitType("DefaultDistancePrimary")
            Case 2
                'Name = "igUnitAngle"
                UnitConstant = LUTDefaultNameToUnitType("DefaultAnglePrimary")
            Case 3
                'Name = "igUnitMass"
                UnitConstant = LUTDefaultNameToUnitType("DefaultMassPrimary")
            Case 4
                'Name = "igUnitTime"
                'UnitConstant = LUTDefaultNames("")
            Case 5
                'Name = "igUnitTemperature"
                UnitConstant = LUTDefaultNameToUnitType("DefaultTemperaturePrimary")
            Case 6
                'Name = "igUnitElectricCharge"
                'UnitConstant = LUTDefaultNames("")
            Case 7
                'Name = "igUnitLuminousIntensity"
                'UnitConstant = LUTDefaultNames("")
            Case 8
                'Name = "igUnitAmountOfSubstance"
                'UnitConstant = LUTDefaultNames("")
            Case 9
                'Name = "igUnitSolidAngle"
                'UnitConstant = LUTDefaultNames("")
            Case 10
                'Name = "igUnitAngularAcceleration"
                UnitConstant = LUTDefaultNameToUnitType("DefaultAngularAccelerationPrimary")
            Case 11
                'Name = "igUnitAngularMomentum"
                'UnitConstant = LUTDefaultNames("")
            Case 12
                'Name = "igUnitAngularVelocity"
                UnitConstant = LUTDefaultNameToUnitType("DefaultAngularVelocityPrimary")
            Case 13
                'Name = "igUnitArea"
                UnitConstant = LUTDefaultNameToUnitType("DefaultAreaPrimary")
            Case 14
                'Name = "igUnitBodyForce"
                UnitConstant = LUTDefaultNameToUnitType("DefaultBodyForcePrimary")
            Case 15
                'Name = "igUnitCoefficientOfThermalExpansion"
                UnitConstant = LUTDefaultNameToUnitType("DefaultCoefThermExp")
            Case 16
                'Name = "igUnitDensity"
                UnitConstant = LUTDefaultNameToUnitType("DefaultDensityPrimary")
            Case 17
                'Name = "igUnitElectricalCapacitance"
                'UnitConstant = LUTDefaultNames("")
            Case 18
                'Name = "igUnitElectricalConductance"
                'UnitConstant = LUTDefaultNames("")
            Case 19
                'Name = "igUnitElectricalFieldStrength"
                'UnitConstant = LUTDefaultNames("")
            Case 20
                'Name = "igUnitElectricalInductance"
                'UnitConstant = LUTDefaultNames("")
            Case 21
                'Name = "igUnitElectricalPotential"
                'UnitConstant = LUTDefaultNames("")
            Case 22
                'Name = "igUnitElectricalResistance"
                'UnitConstant = LUTDefaultNames("")
            Case 23
                'Name = "igUnitEnergy"
                UnitConstant = LUTDefaultNameToUnitType("DefaultEnergyPrimary")
            Case 24
                'Name = "igUnitEntropy"
                'UnitConstant = LUTDefaultNames("")
            Case 25
                'Name = "igUnitFilmCoefficient"
                UnitConstant = LUTDefaultNameToUnitType("DefaultFilmCoefficientPrimary")
            Case 26
                'Name = "igUnitForce"
                UnitConstant = LUTDefaultNameToUnitType("DefaultForcePrimary")
            Case 27
                'Name = "igUnitForcePerArea"
                'UnitConstant = LUTDefaultNames("")
            Case 28
                'Name = "igUnitForcePerDistance"
                UnitConstant = LUTDefaultNameToUnitType("DefaultForceperDistancePrimary")
            Case 29
                'Name = "igUnitFrequency"
                UnitConstant = LUTDefaultNameToUnitType("DefaultFrequencyPrimary")
            Case 30
                'Name = "igUnitHeatCapacity"
                UnitConstant = LUTDefaultNameToUnitType("DefaultSpecificHeat")
            Case 31
                'Name = "igUnitHeatFluxPerArea"
                UnitConstant = LUTDefaultNameToUnitType("DefaultHeatFluxperAreaPrimary")
            Case 32
                'Name = "igUnitHeatFluxPerDistance"
                UnitConstant = LUTDefaultNameToUnitType("DefaultHeatFluxPerDistancePrimary")
            Case 33
                'Name = "igUnitHeatSource"
                UnitConstant = LUTDefaultNameToUnitType("DefaultHeatSourcePrimary")
            Case 34
                'Name = "igUnitIlluminance"
                'UnitConstant = LUTDefaultNames("")
            Case 35
                'Name = "igUnitLinearAcceleration"
                UnitConstant = LUTDefaultNameToUnitType("DefaultLinearAccelerationPrimary")
            Case 36
                'Name = "igUnitLinearPerAngular"
                UnitConstant = LUTDefaultNameToUnitType("DefaultLinearPerAngularPrimary")
            Case 37
                'Name = "igUnitLinearVelocity"
                UnitConstant = LUTDefaultNameToUnitType("DefaultLinearVelocityPrimary")
            Case 38
                'Name = "igUnitLuminousFlux"
                'UnitConstant = LUTDefaultNames("")
            Case 39
                'Name = "igUnitMagneticFieldStrength"
                'UnitConstant = LUTDefaultNames("")
            Case 40
                'Name = "igUnitMagneticFlux"
                'UnitConstant = LUTDefaultNames("")
            Case 41
                'Name = "igUnitMagneticFluxDensity"
                'UnitConstant = LUTDefaultNames("")
            Case 42
                'Name = "igUnitMassFlowRate"
                UnitConstant = LUTDefaultNameToUnitType("DefaultMassFlowratePrimary")
            Case 43
                'Name = "igUnitMassMomentOfInertia"
                'UnitConstant = LUTDefaultNames("")
            Case 44
                'Name = "igUnitMassPerArea"
                UnitConstant = LUTDefaultNameToUnitType("DefaultMassPerAreaPrimary")
            Case 45
                'Name = "igUnitMassPerLength"
                'UnitConstant = LUTDefaultNames("")
            Case 46
                'Name = "igUnitMomentum"
                'UnitConstant = LUTDefaultNames("")
            Case 47
                'Name = "igUnitPerDistance"
                UnitConstant = LUTDefaultNameToUnitType("DefaultPerDistancePrimary")
            Case 48
                'Name = "igUnitPower"
                UnitConstant = LUTDefaultNameToUnitType("DefaultPowerPrimary")
            Case 49
                'Name = "igUnitQuantityOfElectricity"
                'UnitConstant = LUTDefaultNames("")
            Case 50
                'Name = "igUnitRadiantIntensity"
                'UnitConstant = LUTDefaultNames("")
            Case 51
                'Name = "igUnitRotationalStiffness"
                UnitConstant = LUTDefaultNameToUnitType("DefaultRotationalStiffnessPrimary")
            Case 52
                'Name = "igUnitSecondMomentOfArea"
                'UnitConstant = LUTDefaultNames("")
            Case 53
                'Name = "igUnitThermalConductivity"
                UnitConstant = LUTDefaultNameToUnitType("DefaultThermCond")
            Case 54
                'Name = "igUnitDynamicViscosity"
                UnitConstant = LUTDefaultNameToUnitType("DefaultDynamicViscosityPrimary")
            Case 55
                'Name = "igUnitKinematicViscosity"
                UnitConstant = LUTDefaultNameToUnitType("DefaultKinematicVisPrimary")
            Case 56
                'Name = "igUnitVolume"
                UnitConstant = LUTDefaultNameToUnitType("DefaultVolumePrimary")
            Case 57
                'Name = "igUnitVolumeFlowRate"
                UnitConstant = LUTDefaultNameToUnitType("DefaultVolumeFlowRatePrimary")
            Case 58
                'Name = "igUnitScalar"
                'UnitConstant = LUTDefaultNames("")
            Case 59
                'Name = "igUnitTorque"
                UnitConstant = LUTDefaultNameToUnitType("DefaultTorquePrimary")
            Case 60
                'Name = "igUnitEnergyDensity"
                UnitConstant = LUTDefaultNameToUnitType("DefaultEnergyDensityPrimary")
            Case 61
                'Name = "igUnitPressure"
                UnitConstant = LUTDefaultNameToUnitType("DefaultPressurePrimary")
            Case 62
                'Name = "igUnitHeatGeneration"
                UnitConstant = LUTDefaultNameToUnitType("DefaultHeatGenerationPrimary")
            Case 63
                'Name = "igUnitTemperatureGradient"
                UnitConstant = LUTDefaultNameToUnitType("DefaultTemperatureGradientPrimary")

        End Select

        '###### UnitOfMeasureLength ######

        Dim dmPERm = 10
        Dim cmPERm = 100
        Dim mmPERm = 1000
        Dim kmPERm = 0.001
        Dim nmPERm = 1000000000
        Dim inPERm = 1 / 0.0254
        Dim ftPERm = inPERm / 12
        Dim ydPERm = ftPERm / 3
        Dim miPERm = ftPERm / 5280
        Dim tenthPERm = inPERm * 10
        Dim hundredthPERm = inPERm * 100
        Dim thousandthPERm = inPERm * 1000
        Dim polePERm = ydPERm / 5.5
        Dim rodPERm = polePERm
        Dim furlongPERm = miPERm * 8
        Dim chainPERm = furlongPERm * 10

        If OutValue = SentinelValue Then
            Select Case UnitConstant
                Case = 345
                    'Name = "seNone", Description = "None", Symbol = ""
                    OutValue = InValue * 1

                Case = 59
                    'Name = "seLengthMeter", Description = "Meter", Symbol = "m"
                    OutValue = InValue * 1
                Case = 60
                    'Name = "seLengthCentimeter", Description = "Centimeter", Symbol = "cm"
                    OutValue = InValue * cmPERm
                Case = 61
                    'Name = "seLengthMillimeter", Description = "Millimeter", Symbol = "mm"
                    OutValue = InValue * mmPERm
                Case = 62
                    'Name = "seLengthKilometer", Description = "Kilometer", Symbol = "km"
                    OutValue = InValue * kmPERm
                Case = 63
                    'Name = "seLengthNanometer", Description = "Nanometer", Symbol = "nm"
                    OutValue = InValue * nmPERm
                Case = 64
                    'Name = "seLengthInch", Description = "Inch", Symbol = "in"
                    OutValue = InValue * inPERm
                Case = 65
                    'Name = "seLengthFoot", Description = "Foot", Symbol = "ft"
                    OutValue = InValue * ftPERm
                Case = 361
                    'Name = "seLengthInchAbbr", Description = "Inch Abbreviated", Symbol = "''"
                    OutValue = InValue * inPERm
                Case = 360
                    'Name = "seLengthFootAbbr", Description = "Foot Abbreviated", Symbol = "'"
                    OutValue = InValue * ftPERm
                Case = 66
                    'Name = "seLengthYard", Description = "Yard", Symbol = "yd"
                    OutValue = InValue * ydPERm
                Case = 67
                    'Name = "seLengthMile", Description = "Mile", Symbol = "mi"
                    OutValue = InValue * miPERm
                Case = 68
                    'Name = "seLengthTenth", Description = "Tenth", Symbol = "tenth"
                    OutValue = InValue * tenthPERm
                Case = 69
                    'Name = "seLengthHundredth", Description = "Hundredth", Symbol = "hundred"
                    OutValue = InValue * hundredthPERm
                Case = 70
                    'Name = "seLengthThousandth", Description = "Thousandth", Symbol = "thousandth"
                    OutValue = InValue * thousandthPERm
                Case = 71
                    'Name = "seLengthRod", Description = "Rod", Symbol = "rod"
                    OutValue = InValue * rodPERm
                Case = 72
                    'Name = "seLengthPole", Description = "Pole", Symbol = "pole"
                    OutValue = InValue * polePERm
                Case = 74
                    'Name = "seLengthChain", Description = "Chain", Symbol = "chain"
                    OutValue = InValue * chainPERm
                Case = 76
                    'Name = "seLengthFurlong", Description = "Furlong", Symbol = "furlong"
                    OutValue = InValue * furlongPERm

            End Select
        End If

        '###### UnitOfMeasureAngle ######

        Dim degreePERrad = 360 / (2 * Math.PI)
        Dim minutePERrad = degreePERrad / 60
        Dim secondPERrad = minutePERrad / 60
        Dim gradPERrad = 400 / (2 * Math.PI)

        If OutValue = SentinelValue Then
            Select Case UnitConstant
                Case = 77
                    'Name = "seRadian", Description = "Radian", Symbol = "rad"
                    OutValue = InValue * 1
                Case = 78
                    'Name = "seAngleDegree", Description = "Degree", Symbol = "°"if the , Symbol is "°"
                    OutValue = InValue * degreePERrad
                Case = 79
                    'Name = "seAngleMinute", Description = "Minute", Symbol = "'"
                    OutValue = InValue * minutePERrad
                Case = 80
                    'Name = "seAngleSecond", Description = "Second", Symbol = "''"
                    OutValue = InValue * secondPERrad
                Case = 81
                    'Name = "seAngleGradient", Description = "Gradient", Symbol = "gon"
                    OutValue = InValue * gradPERrad
                Case = 357
                    'Name = "seAngleDegreeAbbr", Description = "Degree Abbreviated", Symbol = "°"
                    OutValue = InValue * degreePERrad
                Case = 358
                    'Name = "seAngleMinuteAbbr", Description = "Minute Abbreviated", Symbol = "'"
                    OutValue = InValue * minutePERrad
                Case = 359
                    'Name = "seAngleSecondAbbr", Description = "Second Abbreviated", Symbol = "''"
                    OutValue = InValue * secondPERrad
            End Select
        End If

        '###### UnitOfMeasureMass ######

        Dim gPERkg = 1000
        Dim MgPERkg = 0.001
        Dim lbmPERkg = 1 / 0.45359237
        Dim slugPERkg = lbmPERkg / 32.17405
        Dim slinchPERkg = slugPERkg * 12
        Dim tonPERkg = 1 / 1016.047  ' Mystery number from SE
        Dim nettonPERkg = lbmPERkg / 2000

        If OutValue = SentinelValue Then
            Select Case UnitConstant
                Case = 83
                    'Name = "seMassKilogram", Description = "Kilogram", Symbol = "kg"
                    OutValue = InValue * 1
                Case = 84
                    'Name = "seMassGram", Description = "Gram", Symbol = "g"
                    OutValue = InValue * gPERkg
                Case = 86
                    'Name = "seMassMegagram", Description = "Megagram", Symbol = "Mg"
                    OutValue = InValue * MgPERkg
                Case = 88
                    'Name = "seMassSlug", Description = "Slug", Symbol = "slug"
                    OutValue = InValue * slugPERkg
                Case = 89
                    'Name = "seMassPoundMass", Description = "Pound Mass", Symbol = "lbm"
                    OutValue = InValue * lbmPERkg
                Case = 90
                    'Name = "seMassSlinch", Description = "Slinch", Symbol = "slinch"
                    OutValue = InValue * slinchPERkg
                Case = 92
                    'Name = "seMassTon", Description = "Ton", Symbol = "ton"
                    OutValue = InValue * tonPERkg
                Case = 93
                    'Name = "seMassTonne", Description = "Tonne", Symbol = "tonne"
                    OutValue = InValue * tonPERkg
                Case = 94
                    'Name = "seMassNetTon", Description = "Net Ton", Symbol = "net-ton"
                    OutValue = InValue * nettonPERkg
            End Select
        End If

        '###### UnitOfMeasureTemperature ######

        If OutValue = SentinelValue Then
            Select Case UnitConstant
                Case = 103
                    'Name = "seKelvin", Description = "Kelvin", Symbol = "K"
                    OutValue = InValue
                Case = 104
                    'Name = "seFahrenheit", Description = "Fahrenheit", Symbol = "F"
                    OutValue = (InValue - 273.15) * 180 / 100 + 32
                Case = 105
                    'Name = "seCelsius", Description = "Celsius", Symbol = "C"
                    OutValue = InValue - 273.15
                Case = 106
                    'Name = "seRankine", Description = "Rankine", Symbol = "R"
                    OutValue = InValue * 180 / 100
            End Select
        End If

        '###### UnitOfMeasureArea ######

        If OutValue = SentinelValue Then
            Select Case UnitConstant
                Case = 124
                    'Name = "seMeterSquared", Description = "Squared Meter", Symbol = "m^2"
                    OutValue = InValue * 1
                Case = 125
                    'Name = "seMillimeterSquared", Description = "Squared Millimeter", Symbol = "mm^2"
                    OutValue = InValue * mmPERm ^ 2
                Case = 126
                    'Name = "seCentimeterSquared", Description = "Squared Centimeter", Symbol = "cm^2"
                    OutValue = InValue * cmPERm ^ 2
                Case = 127
                    'Name = "seKilometerSquared", Description = "Squared Kilometer", Symbol = "km^2"
                    OutValue = InValue * kmPERm ^ 2
                Case = 128
                    'Name = "seInchSquared", Description = "Squared Inch", Symbol = "in^2"
                    OutValue = InValue * inPERm ^ 2
                Case = 129
                    'Name = "seFootSquared", Description = "Squared Foot", Symbol = "ft^2"
                    OutValue = InValue * ftPERm ^ 2
                Case = 130
                    'Name = "seYardSquared", Description = "Squared Yard", Symbol = "yd^2"
                    OutValue = InValue * ydPERm ^ 2
                Case = 131
                    'Name = "seMileSquared", Description = "Squared Mile", Symbol = "mi^2"
                    OutValue = InValue * miPERm ^ 2
            End Select
        End If

        '###### UnitOfMeasureVolume ######
        Dim LPERm3 = 1000
        Dim galPERm3 = (1 / 231) * inPERm ^ 3  ' Gal/in^3 * in^3/m^3
        Dim qtPERm3 = galPERm3 * 4
        Dim ptPERm3 = galPERm3 * 8
        Dim ozPERm3 = galPERm3 * 128

        If OutValue = SentinelValue Then
            Select Case UnitConstant
                Case = 334
                    'Name = "seVolumeMiterCubed", Description = "Cubed Meter", Symbol = "m^3"
                    OutValue = InValue * 1
                Case = 383
                    'Name = "seVolumeDecimeterCubed", Description = "Cubed Decimeter", Symbol = "dm^3"
                    OutValue = InValue * dmPERm ^ 3
                Case = 366
                    'Name = "seVolumeCentimeterCubed", Description = "Cubed Centimeter", Symbol = "cm^3"
                    OutValue = InValue * cmPERm ^ 3
                Case = 335
                    'Name = "seVolumeMillimeterCubed", Description = "Cubed Millimeter", Symbol = "mm^3"
                    OutValue = InValue * mmPERm ^ 3
                Case = 336
                    'Name = "seVolumeLiter", Description = "Liter", Symbol = "L"
                    OutValue = InValue * LPERm3
                Case = 338
                    'Name = "seVolumeInchCubed", Description = "Cubed Inch", Symbol = "in^3"
                    OutValue = InValue * inPERm ^ 3
                Case = 339
                    'Name = "seVolumeFootCubed", Description = "Cubed Foot", Symbol = "ft^3"
                    OutValue = InValue * ftPERm ^ 3
                Case = 340
                    'Name = "seVolumeYardCubed", Description = "Cubed Yard", Symbol = "yd^3"
                    OutValue = InValue * ydPERm ^ 3
                Case = 341
                    'Name = "seVolumeGallon", Description = "Gallon", Symbol = "gallon"
                    OutValue = InValue * galPERm3
                Case = 342
                    'Name = "seVolumeQuart", Description = "Quart", Symbol = "quart"
                    OutValue = InValue * qtPERm3
                Case = 343
                    'Name = "seVolumePint", Description = "Pint", Symbol = "pint"
                    OutValue = InValue * ptPERm3
                Case = 344
                    'Name = "seVolumeOunce", Description = "Ounce", Symbol = "ounce"
                    OutValue = InValue * ozPERm3
            End Select
        End If

        '###### UnitOfMeasureDensity ######

        If OutValue = SentinelValue Then
            Select Case UnitConstant
                Case = 143
                    'Name = "seDensityKilogramPerMeterCubed", Description = "Kilogram per Cubed Meter", Symbol = "kg/m^3"
                    OutValue = InValue * 1
                Case = 523
                    'Name = "seDensityKilogramPerDecimeterCubed", Description = "Kilogram per Cubed Decimeter", Symbol = "kg/dm^3"
                    OutValue = InValue * (1 / dmPERm ^ 3)
                Case = 145
                    'Name = "seDensityKilogramPerCentimeterCubed", Description = "Kilogram per Cubed Centimeter", Symbol = "kg/cm^3"
                    OutValue = InValue * (1 / cmPERm ^ 3)
                Case = 144
                    'Name = "seDensitykilogramPerMillimeterCubed", Description = "Kilogram per Cubed Millimeter", Symbol = "kg/mm^3"
                    OutValue = InValue * (1 / mmPERm ^ 3)
                Case = 525
                    'Name = "seDensityKilogramPerLiter", Description = "Kilogram per Liter", Symbol = "kg/L"
                    OutValue = InValue * (1 / LPERm3)
                Case = 520
                    'Name = "seDensityGramPerMeterCubed", Description = "Gram per Cubed Meter", Symbol = "g/m^3"
                    OutValue = InValue * (gPERkg / 1)
                Case = 524
                    'Name = "seDensityGramPerDecimeterCubed", Description = "Gram per Cubed Decimeter", Symbol = "g/dm^3"
                    OutValue = InValue * (gPERkg / dmPERm ^ 3)
                Case = 521
                    'Name = "seDensityGramPerCentimeterCubed", Description = "Gram per Cubed Centimeter", Symbol = "g/cm^3"
                    OutValue = InValue * (gPERkg / cmPERm ^ 3)
                Case = 522
                    'Name = "seDensityGramPerMillimeterCubed", Description = "Gram per Cubed Millimeter", Symbol = "g/mm^3"
                    OutValue = InValue * (gPERkg / mmPERm ^ 3)
                Case = 146
                    'Name = "seDensityPoundMassPerFootCubed", Description = "Pound Mass per Cubed Foot", Symbol = "lbm/ft^3"
                    OutValue = InValue * (lbmPERkg / ftPERm ^ 3)
                Case = 147
                    'Name = "seDensityPoundMassPerInchCubed", Description = "Pound Mass per Cubed Inch", Symbol = "lbm/in^3"
                    OutValue = InValue * (lbmPERkg / inPERm ^ 3)
                Case = 148
                    'Name = "seDensitySlugPerFootCubed", Description = "Slug per Cubed Foot", Symbol = "slug/ft^3"
                    OutValue = InValue * (slugPERkg / ftPERm ^ 3)
                Case = 149
                    'Name = "seDensitySlinchPerFootCubed", Description = "Slinch per Cubed Foot", Symbol = "slinch/ft^3"
                    OutValue = InValue * (slinchPERkg / ftPERm ^ 3)
            End Select
        End If

        If OutValue = SentinelValue Then
            OutValue = InValue
        End If

        Return OutValue
    End Function

End Class

