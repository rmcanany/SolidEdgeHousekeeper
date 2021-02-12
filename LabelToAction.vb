Option Strict On

Public Class LabelToAction
    Inherits Dictionary(Of String, L2A)

    Public Class L2A
        Public Property TaskName As String
        Public Property HelpText As String
        Public Property RequiresTemplate As Boolean
        Public Property RequiresMaterialTable As Boolean
        Public Property RequiresLaserOutputDirectory As Boolean
        Public Property RequiresPartNumberFields As Boolean
        Public Property RequiresSave As Boolean
        Public Property RequiresStepOutputDirectory As Boolean
        Public Property RequiresPdfOutputDirectory As Boolean
        Public Property RequiresDxfOutputDirectory As Boolean
        Public Property IncompatibleWithOtherTasks As Boolean


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
                            HelpText As String,
                            RequiresTemplate As Boolean,
                            RequiresMaterialTable As Boolean,
                            RequiresLaserOutputDirectory As Boolean,
                            RequiresPartNumberFields As Boolean,
                            RequiresSave As Boolean,
                            RequiresStepOutputDirectory As Boolean,
                            RequiresPdfOutputDirectory As Boolean,
                            RequiresDxfOutputDirectory As Boolean,
                            IncompatibleWithOtherTasks As Boolean)

        Entry.TaskName = TaskName
        Entry.HelpText = HelpText
        Entry.RequiresTemplate = RequiresTemplate
        Entry.RequiresMaterialTable = RequiresMaterialTable
        Entry.RequiresLaserOutputDirectory = RequiresLaserOutputDirectory
        Entry.RequiresPartNumberFields = RequiresPartNumberFields
        Entry.RequiresSave = RequiresSave
        Entry.RequiresStepOutputDirectory = RequiresStepOutputDirectory
        Entry.RequiresPdfOutputDirectory = RequiresPdfOutputDirectory
        Entry.RequiresDxfOutputDirectory = RequiresDxfOutputDirectory
        Entry.IncompatibleWithOtherTasks = IncompatibleWithOtherTasks

        Me(LabelText) = Entry

    End Sub

    Private Sub PopulateAssembly()
        Dim HelpString As String

        Dim ActivateAndUpdateAll As New L2A
        HelpString = "    Loads all assembly occurrences' geometry into memory and does an update.  "
        HelpString += "Used mainly to eliminate the gray corners on assembly drawings.  " + vbCrLf
        HelpString += "    Can run out of memory for very large assemblies."
        PopulateList(ActivateAndUpdateAll,
                     "Activate and update all",
                     "ActivateAndUpdateAll",
                     HelpString,
                     False, False, False, False, True, False, False, False, False)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        HelpString = "    Updates the file with face and view styles from a file you specify on the Configuration tab.  " + vbCrLf
        HelpString += "    Note, the view style must be a named style.  Overrides are ignored.  "
        HelpString += "To create a named style from an override, use 'Save As' on the View Overrides dialog."
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     HelpString,
                     True, False, False, False, True, False, False, False, False)

        Dim RemoveFaceStyleOverrides As New L2A
        HelpString = "    Face style overrides change a part's appearance in the assembly.  "
        HelpString += "This command causes the part to appear the same in the part file and the assembly."
        PopulateList(RemoveFaceStyleOverrides,
                     "Remove face style overrides",
                     "RemoveFaceStyleOverrides",
                     HelpString,
                     False, False, False, False, True, False, False, False, False)

        Dim FitIsometricView As New L2A
        HelpString = "    Hides reference planes, maximizes the window, sets the view to iso, and does a fit."
        PopulateList(FitIsometricView,
                     "Fit isometric view",
                     "FitIsometricView",
                     HelpString,
                     False, False, False, False, True, False, False, False, False)

        Dim OccurrenceMissingFiles As New L2A
        HelpString = "    Checks to see if any assembly occurrence is pointing to a file not found on disk."
        PopulateList(OccurrenceMissingFiles,
                     "Occurrence missing files",
                     "OccurrenceMissingFiles",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim OccurrenceOutsideProjectDirectory As New L2A
        HelpString = "    Checks to see if any assembly occurrence resides outside the input directory specified on the General tab.  " + vbCrLf
        HelpString += "    Only useful when a project is housed in one top-level directory."
        PopulateList(OccurrenceOutsideProjectDirectory,
                     "Occurrence outside project directory",
                     "OccurrenceOutsideProjectDirectory",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim FailedRelationships As New L2A
        HelpString = ""
        PopulateList(FailedRelationships,
                     "Failed relationships",
                     "FailedRelationships",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim UnderconstrainedRelationships As New L2A
        HelpString = ""
        PopulateList(UnderconstrainedRelationships,
                     "Underconstrained relationships",
                     "UnderconstrainedRelationships",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim PartNumberDoesNotMatchFilename As New L2A
        HelpString = "    Checks if a file property, that you specify on the Configuration tab, matches the file name."
        PopulateList(PartNumberDoesNotMatchFilename,
                     "Part number does not match file name",
                     "PartNumberDoesNotMatchFilename",
                     HelpString,
                     False, False, False, True, False, False, False, False, False)

        Dim SaveAsSTEP As New L2A
        HelpString = ""
        PopulateList(SaveAsSTEP,
                     "Save as STEP",
                     "SaveAsSTEP",
                     HelpString,
                     False, False, False, False, False, True, False, False, False)


    End Sub

    Private Sub PopulatePart()
        Dim HelpString As String

        Dim UpdateInsertPartCopies As New L2A
        HelpString = "    In conjuction with 'Assembly Activate and update all', "
        HelpString += "used mainly to eliminate the gray corners on assembly drawings."
        PopulateList(UpdateInsertPartCopies,
                     "Update insert part copies",
                     "UpdateInsertPartCopies",
                     HelpString,
                     False, False, False, True, True, False, False, False, False)

        Dim UpdateMaterialFromMaterialTable As New L2A
        HelpString = "    Checks to see if the part's material name and properties match any material "
        HelpString += "in a file you specify on the Configuration tab.  " + vbCrLf
        HelpString += "    If the names match, "
        HelpString += "but their properties (e.g., face style) do not, the material is updated.  "
        HelpString += "If the names do not match, or no material is assigned, it is reported in the log file."
        PopulateList(UpdateMaterialFromMaterialTable,
                     "Update material from material table",
                     "UpdateMaterialFromMaterialTable",
                     HelpString,
                     False, True, False, False, True, False, False, False, False)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     HelpString,
                     True, False, False, False, True, False, False, False, False)

        Dim FitIsometricView As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(FitIsometricView,
                     "Fit isometric view",
                     "FitIsometricView",
                     HelpString,
                     False, False, False, False, True, False, False, False, False)

        Dim FailedOrWarnedFeatures As New L2A
        HelpString = ""
        PopulateList(FailedOrWarnedFeatures,
                     "Failed or warned features",
                     "FailedOrWarnedFeatures",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim SuppressedOrRolledBackFeatures As New L2A
        HelpString = ""
        PopulateList(SuppressedOrRolledBackFeatures,
                     "Suppressed or rolled back features",
                     "SuppressedOrRolledBackFeatures",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim UnderconstrainedProfiles As New L2A
        HelpString = ""
        PopulateList(UnderconstrainedProfiles,
                     "Underconstrained profiles",
                     "UnderconstrainedProfiles",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim InsertPartCopiesOutOfDate As New L2A
        HelpString = ""
        PopulateList(InsertPartCopiesOutOfDate,
                     "Insert part copies out of date",
                     "InsertPartCopiesOutOfDate",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim MaterialNotInMaterialTable As New L2A
        HelpString = ""
        PopulateList(MaterialNotInMaterialTable,
                     "Material not in material table",
                     "MaterialNotInMaterialTable",
                     HelpString,
                     False, True, False, False, False, False, False, False, False)

        Dim PartNumberDoesNotMatchFilename As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(PartNumberDoesNotMatchFilename,
                     "Part number does not match file name",
                     "PartNumberDoesNotMatchFilename",
                     HelpString,
                     False, False, False, True, False, False, False, False, False)

        Dim SaveAsSTEP As New L2A
        HelpString = ""
        PopulateList(SaveAsSTEP,
                     "Save as STEP",
                     "SaveAsSTEP",
                     HelpString,
                     False, False, False, False, False, True, False, False, False)

    End Sub

    Public Sub PopulateSheetmetal()
        Dim HelpString As String

        Dim UpdateInsertPartCopies As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(UpdateInsertPartCopies,
                     "Update insert part copies",
                     "UpdateInsertPartCopies",
                     HelpString,
                     False, False, False, True, True, False, False, False, False)

        Dim UpdateMaterialFromMaterialTable As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(UpdateMaterialFromMaterialTable,
                     "Update material from material table",
                     "UpdateMaterialFromMaterialTable",
                     HelpString,
                     False, True, False, False, True, False, False, False, False)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     HelpString,
                     True, False, False, False, True, False, False, False, False)

        Dim FitIsometricView As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(FitIsometricView,
                     "Fit isometric view",
                     "FitIsometricView",
                     HelpString,
                     False, False, False, False, True, False, False, False, False)

        Dim FailedOrWarnedFeatures As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(FailedOrWarnedFeatures,
                     "Failed or warned features",
                     "FailedOrWarnedFeatures",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim SuppressedOrRolledBackFeatures As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(SuppressedOrRolledBackFeatures,
                     "Suppressed or rolled back features",
                     "SuppressedOrRolledBackFeatures",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim UnderconstrainedProfiles As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(UnderconstrainedProfiles,
                     "Underconstrained profiles",
                     "UnderconstrainedProfiles",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim InsertPartCopiesOutOfDate As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(InsertPartCopiesOutOfDate,
                     "Insert part copies out of date",
                     "InsertPartCopiesOutOfDate",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim FlatPatternMissingOrOutOfDate As New L2A
        HelpString = ""
        PopulateList(FlatPatternMissingOrOutOfDate,
                     "Flat pattern missing or out of date",
                     "FlatPatternMissingOrOutOfDate",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim MaterialNotInMaterialTable As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(MaterialNotInMaterialTable,
                     "Material not in material table",
                     "MaterialNotInMaterialTable",
                     HelpString,
                     False, True, False, False, False, False, False, False, False)

        Dim PartNumberDoesNotMatchFilename As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(PartNumberDoesNotMatchFilename,
                     "Part number does not match file name",
                     "PartNumberDoesNotMatchFilename",
                     HelpString,
                     False, False, False, True, False, False, False, False, False)

        Dim GenerateLaserDXFAndPDF As New L2A
        HelpString = "    Creates a DXF file of the sheet metal flat pattern.  "
        HelpString += "Creates a PDF of the drawing file.  "
        HelpString += "If the flat pattern is missing or out of date, or if the drawing is out of date, "
        HelpString += "it is reported in the log file." + vbCrLf
        HelpString += "    Note, the drawing file must have the same name "
        HelpString += "as the sheet metal file.  "
        PopulateList(GenerateLaserDXFAndPDF,
                     "Generate Laser DXF and PDF",
                     "GenerateLaserDXFAndPDF",
                     HelpString,
                     False, False, True, True, False, False, False, False, False)

        Dim SaveAsSTEP As New L2A
        HelpString = ""
        PopulateList(SaveAsSTEP,
                     "Save as STEP",
                     "SaveAsSTEP",
                     HelpString,
                     False, False, False, False, False, True, False, False, False)

    End Sub

    Private Sub PopulateDraft()
        Dim HelpString As String

        Dim UpdateDrawingViews As New L2A
        HelpString = ""
        PopulateList(UpdateDrawingViews,
                     "Update drawing views",
                     "UpdateDrawingViews",
                     HelpString,
                     False, False, False, False, True, False, False, False, False)

        'Dim UpdateDrawingBorderFromTemplate As New L2A
        'PopulateList(UpdateDrawingBorderFromTemplate,
        '             "Update drawing border from template",
        '             "UpdateDrawingBorderFromTemplate",
        '             True, False, False, False, True, False, False, False, False)

        'Dim UpdateDimensionStylesFromTemplate As New L2A
        'PopulateList(UpdateDimensionStylesFromTemplate,
        '             "Update dimension styles from template",
        '             "UpdateDimensionStylesFromTemplate",
        '             True, False, False, False, True, False, False, False, False)

        Dim MoveDrawingToNewTemplate As New L2A
        HelpString = "    Creates a new file from a template you specify on the Configuration tab.  "
        HelpString += "Copies drawing views, dimensions, etc. from the old file into the new one.  "
        HelpString += "If the template has updated styles, a different background sheet, or other changes, "
        HelpString += "the new drawing will inherit them automatically.  " + vbCrLf
        HelpString += "    This task has the option to 'Allow partial success'.  It is set on the Configuration tab.  "
        HelpString += "If the option is set, and some drawing elements were not transferred, "
        HelpString += "it is reported in the log file.  "
        HelpString += "Also reported in the log file are instructions for completing the transfer.  " + vbCrLf
        HelpString += "    Note, because this task needs to do a 'Save As', it must be run with no other tasks selected."
        PopulateList(MoveDrawingToNewTemplate,
                     "Move drawing to new template",
                     "MoveDrawingToNewTemplate",
                     HelpString,
                     True, False, False, False, True, False, False, False, True)

        Dim FitView As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(FitView,
                     "Fit view",
                     "FitView",
                     HelpString,
                     False, False, False, False, True, False, False, False, False)

        Dim DrawingViewsMissingFile As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(DrawingViewsMissingFile,
                     "Drawing views missing file",
                     "DrawingViewsMissingFile",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim DrawingViewsOutOfDate As New L2A
        HelpString = ""
        PopulateList(DrawingViewsOutOfDate,
                     "Drawing views out of date",
                     "DrawingViewsOutOfDate",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim DetachedDimensionsOrAnnotations As New L2A
        HelpString = ""
        PopulateList(DetachedDimensionsOrAnnotations,
                     "Detached dimensions or annotations",
                     "DetachedDimensionsOrAnnotations",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim FileNameDoesNotMatchModelFilename As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(FileNameDoesNotMatchModelFilename,
                     "File name does not match model file name",
                     "FileNameDoesNotMatchModelFilename",
                     HelpString,
                     False, False, False, False, False, False, False, False, False)

        Dim SaveAsPDF As New L2A
        HelpString = ""
        PopulateList(SaveAsPDF,
                     "Save as PDF",
                     "SaveAsPDF",
                     HelpString,
                     False, False, False, False, False, False, True, False, False)

        Dim SaveAsDXF As New L2A
        HelpString = ""
        PopulateList(SaveAsDXF,
                     "Save as DXF",
                     "SaveAsDXF",
                     HelpString,
                     False, False, False, False, False, False, False, True, False)

    End Sub


End Class
