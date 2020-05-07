Public Class LabelToAction
    Public L2A As New Dictionary(Of String, Dictionary(Of String, Object))
    Private Label As String


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

    Private Sub PopulateAssembly()

        'Check.Add("Occurrence missing file")
        'Action.Add(AddressOf AssemblyTasks.AssemblyMissingFiles)
        'Template.Add(False)

        Label = "Occurrence missing files"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "OcccurrenceMissingFiles")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Occurrence outside project directory")
        'Action.Add(AddressOf AssemblyTasks.AssemblyOccurrenceOutsideProjectDirectory)
        'Template.Add(False)

        Label = "Occurrence outside project directory"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "OccurrenceOutsideProjectDirectory")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Failed relationships")
        'Action.Add(AddressOf AssemblyTasks.AssemblyFailedRelationships)
        'Template.Add(False)

        Label = "Failed relationships"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "FailedRelationships")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Underconstrained relationships")
        'Action.Add(AddressOf AssemblyTasks.AssemblyUnderconstrainedRelationships)
        'Template.Add(False)

        Label = "Underconstrained relationships"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "UnderconstrainedRelationships")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Part number does not match file name")
        'Action.Add(AddressOf AssemblyTasks.AssemblyPartNumberDoesNotMatchFilename)
        'Template.Add(False)

        Label = "Part number does not match file name"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "PartNumberDoesNotMatchFilename")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", True)

        'Check.Add("Update face and view styles from template")
        'Action.Add(AddressOf AssemblyTasks.AssemblyUpdateFaceAndViewStylesFromTemplate)
        'Template.Add(True)

        Label = "Update face and view styles from template"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "UpdateFaceAndViewStylesFromTemplate")
        L2A(Label).Add("RequiresTemplate", True)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Fit isometric view")
        'Action.Add(AddressOf AssemblyTasks.AssemblyFitIsometricView)
        'Template.Add(False)

        Label = "Fit isometric view"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "FitIsometricView")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)



    End Sub

    Private Sub PopulatePart()
        'Check.Add("Failed or warned features")
        'Action.Add(AddressOf PartTasks.PartFailedOrWarnedFeatures)
        'Template.Add(False)

        Label = "Failed or warned features"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "FailedOrWarnedFeatures")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Suppressed or rolled back features")
        'Action.Add(AddressOf PartTasks.PartSuppressedOrRolledBackFeatures)
        'Template.Add(False)

        Label = "Suppressed or rolled back features"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "SuppressedOrRolledBackFeatures")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Underconstrained profiles")
        'Action.Add(AddressOf PartTasks.PartUnderconstrainedProfiles)
        'Template.Add(False)

        Label = "Underconstrained profiles"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "UnderconstrainedProfiles")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Material not in material table")
        'Action.Add(AddressOf PartTasks.PartMaterialNotInMaterialTable)
        'Template.Add(False)

        Label = "Material not in material table"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "MaterialNotInMaterialTable")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", True)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Part number does not match file name")
        'Action.Add(AddressOf PartTasks.PartPartNumberDoesNotMatchFilename)
        'Template.Add(False)

        Label = "Part number does not match file name"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "PartNumberDoesNotMatchFilename")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", True)

        'Check.Add("Update face and view styles from template")
        'Action.Add(AddressOf PartTasks.PartUpdateFaceAndViewStylesFromTemplate)
        'Template.Add(True)

        Label = "Update face and view styles from template"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "UpdateFaceAndViewStylesFromTemplate")
        L2A(Label).Add("RequiresTemplate", True)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Fit isometric view")
        'Action.Add(AddressOf PartTasks.PartFitIsometricView)
        'Template.Add(False)

        Label = "Fit isometric view"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "FitIsometricView")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)


    End Sub

    Private Sub PopulateSheetmetal()

        'Check.Add("Failed or warned features")
        'Action.Add(AddressOf SheetmetalTasks.SheetmetalFailedOrWarnedFeatures)
        'Template.Add(False)

        Label = "Failed or warned features"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "FailedOrWarnedFeatures")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Suppressed or rolled back features")
        'Action.Add(AddressOf SheetmetalTasks.SheetmetalSuppressedOrRolledBackFeatures)
        'Template.Add(False)

        Label = "Suppressed or rolled back features"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "SuppressedOrRolledBackFeatures")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Underconstrained profiles")
        'Action.Add(AddressOf SheetmetalTasks.SheetmetalUnderconstrainedProfiles)
        'Template.Add(False)

        Label = "Underconstrained profiles"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "UnderconstrainedProfiles")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Flat pattern missing or out of date")
        'Action.Add(AddressOf SheetmetalTasks.SheetmetalFlatPatternMissingOrOutOfDate)
        'Template.Add(False)

        Label = "Flat pattern missing or out of date"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "FlatPatternMissingOrOutOfDate")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Material not in material table")
        'Action.Add(AddressOf SheetmetalTasks.SheetmetalMaterialNotInMaterialTable)
        'Template.Add(False)

        Label = "Material not in material table"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "MaterialNotInMaterialTable")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", True)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Part number does not match file name")
        'Action.Add(AddressOf SheetmetalTasks.SheetmetalPartNumberDoesNotMatchFilename)
        'Template.Add(False)

        Label = "Part number does not match file name"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "PartNumberDoesNotMatchFilename")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", True)

        'Check.Add("Generate Laser DXF and PDF")
        'Action.Add(AddressOf SheetmetalTasks.SheetmetalGenerateLaserDXFAndPDF)
        'Template.Add(False)

        Label = "Generate Laser DXF and PDF"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "GenerateLaserDXFAndPDF")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", True)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Update face and view styles from template")
        'Action.Add(AddressOf SheetmetalTasks.SheetmetalUpdateFaceAndViewStylesFromTemplate)
        'Template.Add(True)

        Label = "Update face and view styles from template"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "UpdateFaceAndViewStylesFromTemplate")
        L2A(Label).Add("RequiresTemplate", True)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Fit isometric view")
        'Action.Add(AddressOf SheetmetalTasks.SheetmetalFitIsometricView)
        'Template.Add(False)

        Label = "Fit isometric view"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "FitIsometricView")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

    End Sub

    Private Sub PopulateDraft()

        'Check.Add("Drawing views missing file")
        'Action.Add(AddressOf DraftTasks.DraftDrawingViewsMissingFiles)
        'Template.Add(False)

        Label = "Drawing views missing file"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "DrawingViewsMissingFile")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Drawing views out of date")
        'Action.Add(AddressOf DraftTasks.DraftViewsOutOfDate)
        'Template.Add(False)

        Label = "Drawing views out of date"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "DrawingViewsOutOfDate")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Detached dimensions or annotations")
        'Action.Add(AddressOf DraftTasks.DraftDetachedDimensionsOrAnnotations)
        'Template.Add(False)

        Label = "Detached dimensions or annotations"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "DetachedDimensionsOrAnnotations")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("File name does not match model file name")
        'Action.Add(AddressOf DraftTasks.DraftPartNumberDoesNotMatchFilename)
        'Template.Add(False)

        Label = "File name does not match model file name"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "FileNameDoesNotMatchModelFilename")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Update drawing border from template")
        'Action.Add(AddressOf DraftTasks.DraftUpdateBackgroundFromTemplate)
        'Template.Add(True)

        Label = "Update drawing border from template"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "UpdateDrawingBorderFromTemplate")
        L2A(Label).Add("RequiresTemplate", True)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        ''Check.Add("Update dimension styles from template")
        ''Action.Add(AddressOf DraftUpdateDimensionStylesFromTemplate)
        ''Template.Add(True)

        Label = "Update dimension styles from template"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "UpdateDimensionStylesFromTemplate")
        L2A(Label).Add("RequiresTemplate", True)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Fit view")
        'Action.Add(AddressOf DraftTasks.DraftFitView)
        'Template.Add(False)

        Label = "Fit view"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "FitView")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)

        'Check.Add("Save as PDF")
        'Action.Add(AddressOf DraftTasks.DraftSaveAsPDF)
        'Template.Add(False)

        Label = "Save as PDF"
        L2A.Add(Label, New Dictionary(Of String, Object))
        L2A(Label).Add("MethodName", "SaveAsPDF")
        L2A(Label).Add("RequiresTemplate", False)
        L2A(Label).Add("RequiresMaterialTable", False)
        L2A(Label).Add("RequiresLaserOutputDirectory", False)
        L2A(Label).Add("RequiresPartNumberFields", False)


    End Sub


End Class
