Option Strict On

Public Class LabelToAction
    Inherits Dictionary(Of String, L2A)

    Public Class L2A
        Public Property TaskName As String
        Public Property RequiresTemplate As Boolean
        Public Property RequiresMaterialTable As Boolean
        Public Property RequiresLaserOutputDirectory As Boolean
        Public Property RequiresPartNumberFields As Boolean
        Public Property RequiresSave As Boolean
    End Class

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

    Public Sub PopulateList(Entry As L2A,
                            LabelText As String,
                            TaskName As String,
                            RequiresTemplate As Boolean,
                            RequiresMaterialTable As Boolean,
                            RequiresLaserOutputDirectory As Boolean,
                            RequiresPartNumberFields As Boolean,
                            RequiresSave As Boolean)

        Entry.TaskName = TaskName
        Entry.RequiresTemplate = RequiresTemplate
        Entry.RequiresMaterialTable = RequiresMaterialTable
        Entry.RequiresLaserOutputDirectory = RequiresLaserOutputDirectory
        Entry.RequiresPartNumberFields = RequiresPartNumberFields
        Entry.RequiresSave = RequiresSave
        Me(LabelText) = Entry

    End Sub

    Private Sub PopulateAssembly()
        Dim AssemblyTasks As New AssemblyTasks

        Dim OccurrenceMissingFiles As New L2A
        PopulateList(OccurrenceMissingFiles,
                     "Occurrence missing files",
                     "OccurrenceMissingFiles",
                     False, False, False, False, False)

        Dim OccurrenceOutsideProjectDirectory As New L2A
        PopulateList(OccurrenceOutsideProjectDirectory,
                     "Occurrence outside project directory",
                     "OccurrenceOutsideProjectDirectory",
                     False, False, False, False, False)

        Dim FailedRelationships As New L2A
        PopulateList(FailedRelationships,
                     "Failed relationships",
                     "FailedRelationships",
                     False, False, False, False, False)

        Dim UnderconstrainedRelationships As New L2A
        PopulateList(UnderconstrainedRelationships,
                     "Underconstrained relationships",
                     "UnderconstrainedRelationships",
                     False, False, False, False, False)

        Dim PartNumberDoesNotMatchFilename As New L2A
        PopulateList(PartNumberDoesNotMatchFilename,
                     "Part number does not match file name",
                     "PartNumberDoesNotMatchFilename",
                     False, False, False, True, False)

        Dim ActivateAndUpdateAll As New L2A
        PopulateList(ActivateAndUpdateAll,
                     "Activate and update all",
                     "ActivateAndUpdateAll",
                     False, False, False, False, True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     True, False, False, False, True)

        Dim FitIsometricView As New L2A
        PopulateList(FitIsometricView,
                     "Fit isometric view",
                     "FitIsometricView",
                     False, False, False, False, True)

    End Sub

    Private Sub PopulatePart()
        Dim PartTasks As New PartTasks

        Dim FailedOrWarnedFeatures As New L2A
        PopulateList(FailedOrWarnedFeatures,
                     "Failed or warned features",
                     "FailedOrWarnedFeatures",
                     False, False, False, False, False)

        Dim SuppressedOrRolledBackFeatures As New L2A
        PopulateList(SuppressedOrRolledBackFeatures,
                     "Suppressed or rolled back features",
                     "SuppressedOrRolledBackFeatures",
                     False, False, False, False, False)

        Dim UnderconstrainedProfiles As New L2A
        PopulateList(UnderconstrainedProfiles,
                     "Underconstrained profiles",
                     "UnderconstrainedProfiles",
                     False, False, False, False, False)

        Dim InsertPartCopiesOutOfDate As New L2A
        PopulateList(InsertPartCopiesOutOfDate,
                     "Insert part copies out of date",
                     "InsertPartCopiesOutOfDate",
                     False, False, False, False, False)

        Dim MaterialNotInMaterialTable As New L2A
        PopulateList(MaterialNotInMaterialTable,
                     "Material not in material table",
                     "MaterialNotInMaterialTable",
                     False, True, False, False, False)

        Dim PartNumberDoesNotMatchFilename As New L2A
        PopulateList(PartNumberDoesNotMatchFilename,
                     "Part number does not match file name",
                     "PartNumberDoesNotMatchFilename",
                     False, False, False, True, False)

        Dim UpdateInsertPartCopies As New L2A
        PopulateList(UpdateInsertPartCopies,
                     "Update insert part copies",
                     "UpdateInsertPartCopies",
                     False, False, False, True, True)

        Dim UpdateMaterialFromMaterialTable As New L2A
        PopulateList(UpdateMaterialFromMaterialTable,
                     "Update material from material table",
                     "UpdateMaterialFromMaterialTable",
                     False, True, False, False, True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     True, False, False, False, True)

        Dim FitIsometricView As New L2A
        PopulateList(FitIsometricView,
                     "Fit isometric view",
                     "FitIsometricView",
                     False, False, False, False, True)

    End Sub

    Public Sub PopulateSheetmetal()
        Dim SheetmetalTasks As New SheetmetalTasks

        Dim FailedOrWarnedFeatures As New L2A
        PopulateList(FailedOrWarnedFeatures,
                     "Failed or warned features",
                     "FailedOrWarnedFeatures",
                     False, False, False, False, False)

        Dim SuppressedOrRolledBackFeatures As New L2A
        PopulateList(SuppressedOrRolledBackFeatures,
                     "Suppressed or rolled back features",
                     "SuppressedOrRolledBackFeatures",
                     False, False, False, False, False)

        Dim UnderconstrainedProfiles As New L2A
        PopulateList(UnderconstrainedProfiles,
                     "Underconstrained profiles",
                     "UnderconstrainedProfiles",
                     False, False, False, False, False)

        Dim InsertPartCopiesOutOfDate As New L2A
        PopulateList(InsertPartCopiesOutOfDate,
                     "Insert part copies out of date",
                     "InsertPartCopiesOutOfDate",
                     False, False, False, False, False)

        Dim FlatPatternMissingOrOutOfDate As New L2A
        PopulateList(FlatPatternMissingOrOutOfDate,
                     "Flat pattern missing or out of date",
                     "FlatPatternMissingOrOutOfDate",
                     False, False, False, False, False)

        Dim MaterialNotInMaterialTable As New L2A
        PopulateList(MaterialNotInMaterialTable,
                     "Material not in material table",
                     "MaterialNotInMaterialTable",
                     False, True, False, False, False)

        Dim PartNumberDoesNotMatchFilename As New L2A
        PopulateList(PartNumberDoesNotMatchFilename,
                     "Part number does not match file name",
                     "PartNumberDoesNotMatchFilename",
                     False, False, False, True, False)

        Dim GenerateLaserDXFAndPDF As New L2A
        PopulateList(GenerateLaserDXFAndPDF,
                     "Generate Laser DXF and PDF",
                     "GenerateLaserDXFAndPDF",
                     False, False, True, True, False)

        Dim UpdateInsertPartCopies As New L2A
        PopulateList(UpdateInsertPartCopies,
                     "Update insert part copies",
                     "UpdateInsertPartCopies",
                     False, False, False, True, True)

        Dim UpdateMaterialFromMaterialTable As New L2A
        PopulateList(UpdateMaterialFromMaterialTable,
                     "Update material from material table",
                     "UpdateMaterialFromMaterialTable",
                     False, True, False, False, True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     True, False, False, False, True)

        Dim FitIsometricView As New L2A
        PopulateList(FitIsometricView,
                     "Fit isometric view",
                     "FitIsometricView",
                     False, False, False, False, True)

    End Sub

    Private Sub PopulateDraft()
        Dim DraftTasks As New DraftTasks

        Dim DrawingViewsMissingFile As New L2A
        PopulateList(DrawingViewsMissingFile,
                     "Drawing views missing file",
                     "DrawingViewsMissingFile",
                     False, False, False, False, False)

        Dim DrawingViewsOutOfDate As New L2A
        PopulateList(DrawingViewsOutOfDate,
                     "Drawing views out of date",
                     "DrawingViewsOutOfDate",
                     False, False, False, False, False)

        Dim DetachedDimensionsOrAnnotations As New L2A
        PopulateList(DetachedDimensionsOrAnnotations,
                     "Detached dimensions or annotations",
                     "DetachedDimensionsOrAnnotations",
                     False, False, False, False, False)

        Dim FileNameDoesNotMatchModelFilename As New L2A
        PopulateList(FileNameDoesNotMatchModelFilename,
                     "File name does not match model file name",
                     "FileNameDoesNotMatchModelFilename",
                     False, False, False, False, False)

        Dim UpdateDrawingViews As New L2A
        PopulateList(UpdateDrawingViews,
                     "Update drawing views",
                     "UpdateDrawingViews",
                     False, False, False, False, True)

        Dim UpdateDrawingBorderFromTemplate As New L2A
        PopulateList(UpdateDrawingBorderFromTemplate,
                     "Update drawing border from template",
                     "UpdateDrawingBorderFromTemplate",
                     True, False, False, False, True)

        Dim UpdateDimensionStylesFromTemplate As New L2A
        PopulateList(UpdateDimensionStylesFromTemplate,
                     "Update dimension styles from template",
                     "UpdateDimensionStylesFromTemplate",
                     True, False, False, False, True)

        Dim FitView As New L2A
        PopulateList(FitView,
                     "Fit view",
                     "FitView",
                     False, False, False, False, True)

        Dim SaveAsPDF As New L2A
        PopulateList(SaveAsPDF,
                     "Save as PDF",
                     "SaveAsPDF",
                     False, False, False, False, False)

    End Sub


End Class
