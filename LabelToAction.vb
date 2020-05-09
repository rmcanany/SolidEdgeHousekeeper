Public Class LabelToAction
    Inherits List(Of L2A)

    Public Class L2A
        Public Property MethodName As String
        Public Property LabelText As String
        Public Property RequiresTemplate As Boolean
        Public Property RequiresMaterialTable As Boolean
        Public Property RequiresLaserOutputDirectory As Boolean
        Public Property RequiresPartNumberFields As Boolean
    End Class

    'Public L2A As New Dictionary(Of String, Dictionary(Of String, Object))
    'Private Label As String


    Public Sub New(Filetype As String)
        If Filetype = "Assembly" Then
            PopulateAssembly()
        ElseIf Filetype = "Part" Then
            PopulatePart()
        ElseIf Filetype = "Sheetmetal" Then
            PopulateSheetmetal()
        ElseIf Filetype = "Draft" Then
            PopulateDraft()
        Else
            MsgBox("Filetype not recognized: " + Filetype + ".  Exiting...")
            End
        End If
    End Sub

    Public Sub PopulateList(Entry As L2A, MethodName As String, LabelText As String, RequiresTemplate As Boolean,
                         RequiresMaterialTable As Boolean, RequiresLaserOutputDirectory As Boolean,
                         RequiresPartNumberFields As Boolean)
        Entry.MethodName = MethodName
        Entry.LabelText = LabelText
        Entry.RequiresTemplate = RequiresTemplate
        Entry.RequiresMaterialTable = RequiresMaterialTable
        Entry.RequiresLaserOutputDirectory = RequiresLaserOutputDirectory
        Entry.RequiresPartNumberFields = RequiresPartNumberFields
        Me.Add(Entry)

    End Sub

    Private Sub PopulateAssembly()
        Dim OccurrenceMissingFiles As New L2A
        PopulateList(OccurrenceMissingFiles, "OccurrenceMissingFiles",
                     "Occurrence missing files", False, False, False, False)

        Dim OccurrenceOutsideProjectDirectory As New L2A
        PopulateList(OccurrenceOutsideProjectDirectory, "OccurrenceOutsideProjectDirectory",
                     "Occurrence outside project directory", False, False, False, False)

        Dim FailedRelationships As New L2A
        PopulateList(FailedRelationships, "FailedRelationships",
                     "Failed relationships", False, False, False, False)

        Dim UnderconstrainedRelationships As New L2A
        PopulateList(UnderconstrainedRelationships, "UnderconstrainedRelationships",
                     "Underconstrained relationships", False, False, False, False)

        Dim PartNumberDoesNotMatchFilename As New L2A
        PopulateList(PartNumberDoesNotMatchFilename, "PartNumberDoesNotMatchFilename",
                     "Part number does not match file name", False, False, False, True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        PopulateList(UpdateFaceAndViewStylesFromTemplate, "UpdateFaceAndViewStylesFromTemplate",
                     "Update face and view styles from template", True, False, False, False)

        Dim FitIsometricView As New L2A
        PopulateList(FitIsometricView, "FitIsometricView",
                     "Fit isometric view", False, False, False, False)

    End Sub

    Private Sub PopulatePart()

        Dim FailedOrWarnedFeatures As New L2A
        PopulateList(FailedOrWarnedFeatures, "FailedOrWarnedFeatures",
                     "Failed or warned features", False, False, False, False)

        Dim SuppressedOrRolledBackFeatures As New L2A
        PopulateList(SuppressedOrRolledBackFeatures, "SuppressedOrRolledBackFeatures",
                     "Suppressed or rolled back features", False, False, False, False)

        Dim UnderconstrainedProfiles As New L2A
        PopulateList(UnderconstrainedProfiles, "UnderconstrainedProfiles",
                     "Underconstrained profiles", False, False, False, False)

        Dim MaterialNotInMaterialTable As New L2A
        PopulateList(MaterialNotInMaterialTable, "MaterialNotInMaterialTable",
                     "Material not in material table", False, True, False, False)

        Dim PartNumberDoesNotMatchFilename As New L2A
        PopulateList(PartNumberDoesNotMatchFilename, "PartNumberDoesNotMatchFilename",
                     "Part number does not match file name", False, False, False, True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        PopulateList(UpdateFaceAndViewStylesFromTemplate, "UpdateFaceAndViewStylesFromTemplate",
                     "Update face and view styles from template", True, False, False, False)

        Dim FitIsometricView As New L2A
        PopulateList(FitIsometricView, "FitIsometricView",
                     "Fit isometric view", False, False, False, False)

    End Sub

    Public Sub PopulateSheetmetal()

        Dim FailedOrWarnedFeatures As New L2A
        PopulateList(FailedOrWarnedFeatures, "FailedOrWarnedFeatures",
                     "Failed or warned features", False, False, False, False)

        Dim SuppressedOrRolledBackFeatures As New L2A
        PopulateList(SuppressedOrRolledBackFeatures, "SuppressedOrRolledBackFeatures",
                     "Suppressed or rolled back features", False, False, False, False)

        Dim UnderconstrainedProfiles As New L2A
        PopulateList(UnderconstrainedProfiles, "UnderconstrainedProfiles",
                     "Underconstrained profiles", False, False, False, False)


        Dim FlatPatternMissingOrOutOfDate As New L2A
        PopulateList(FlatPatternMissingOrOutOfDate, "FlatPatternMissingOrOutOfDate",
                     "Flat pattern missing or out of date", False, False, False, False)

        Dim MaterialNotInMaterialTable As New L2A
        PopulateList(MaterialNotInMaterialTable, "MaterialNotInMaterialTable",
                     "Material not in material table", False, True, False, False)

        Dim PartNumberDoesNotMatchFilename As New L2A
        PopulateList(PartNumberDoesNotMatchFilename, "PartNumberDoesNotMatchFilename",
                     "Part number does not match file name", False, False, False, True)

        Dim GenerateLaserDXFAndPDF As New L2A
        PopulateList(GenerateLaserDXFAndPDF, "GenerateLaserDXFAndPDF",
                     "Generate Laser DXF and PDF", False, False, True, True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        PopulateList(UpdateFaceAndViewStylesFromTemplate, "UpdateFaceAndViewStylesFromTemplate",
                     "Update face and view styles from template", True, False, False, False)

        Dim FitIsometricView As New L2A
        PopulateList(FitIsometricView, "FitIsometricView",
                     "Fit isometric view", False, False, False, False)

    End Sub

    Private Sub PopulateDraft()

        Dim DrawingViewsMissingFile As New L2A
        PopulateList(DrawingViewsMissingFile, "DrawingViewsMissingFile",
                     "Drawing views missing file", False, False, False, False)

        Dim DrawingViewsOutOfDate As New L2A
        PopulateList(DrawingViewsOutOfDate, "DrawingViewsOutOfDate",
                     "Drawing views out of date", False, False, False, False)

        Dim DetachedDimensionsOrAnnotations As New L2A
        PopulateList(DetachedDimensionsOrAnnotations, "DetachedDimensionsOrAnnotations",
                     "Detached dimensions or annotations", False, False, False, False)

        Dim FileNameDoesNotMatchModelFilename As New L2A
        PopulateList(FileNameDoesNotMatchModelFilename, "FileNameDoesNotMatchModelFilename",
                     "File name does not match model file name", False, False, False, False)

        Dim UpdateDrawingBorderFromTemplate As New L2A
        PopulateList(UpdateDrawingBorderFromTemplate, "UpdateDrawingBorderFromTemplate",
                     "Update drawing border from template", True, False, False, False)

        Dim UpdateDimensionStylesFromTemplate As New L2A
        PopulateList(UpdateDimensionStylesFromTemplate, "UpdateDimensionStylesFromTemplate",
                     "Update dimension styles from template", True, False, False, False)

        Dim FitView As New L2A
        PopulateList(FitView, "FitView",
                     "Fit view", False, False, False, False)

        Dim SaveAsPDF As New L2A
        PopulateList(SaveAsPDF, "SaveAsPDF",
                     "Save as PDF", False, False, False, False)

    End Sub


End Class
