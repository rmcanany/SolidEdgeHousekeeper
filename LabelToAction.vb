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
        Public Property RequiresSaveAsOutputDirectory As Boolean
        Public Property IncompatibleWithOtherTasks As Boolean
        Public Property RequiresExternalProgram As Boolean
        Public Property RequiresSaveAsFlatDXFOutputDirectory As Boolean
        Public Property RequiresFindReplaceFields As Boolean
        Public Property RequiresPrinter As Boolean


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
                            Optional RequiresTemplate As Boolean = False,
                            Optional RequiresMaterialTable As Boolean = False,
                            Optional RequiresLaserOutputDirectory As Boolean = False,
                            Optional RequiresPartNumberFields As Boolean = False,
                            Optional RequiresSave As Boolean = False,
                            Optional RequiresSaveAsOutputDirectory As Boolean = False,
                            Optional IncompatibleWithOtherTasks As Boolean = False,
                            Optional RequiresExternalProgram As Boolean = False,
                            Optional RequiresSaveAsFlatDXFOutputDirectory As Boolean = False,
                            Optional RequiresFindReplaceFields As Boolean = False,
                            Optional RequiresPrinter As Boolean = False)

        Entry.TaskName = TaskName
        Entry.HelpText = HelpText
        Entry.RequiresTemplate = RequiresTemplate
        Entry.RequiresMaterialTable = RequiresMaterialTable
        Entry.RequiresLaserOutputDirectory = RequiresLaserOutputDirectory
        Entry.RequiresPartNumberFields = RequiresPartNumberFields
        Entry.RequiresSave = RequiresSave
        Entry.RequiresSaveAsOutputDirectory = RequiresSaveAsOutputDirectory
        Entry.IncompatibleWithOtherTasks = IncompatibleWithOtherTasks
        Entry.RequiresExternalProgram = RequiresExternalProgram
        Entry.RequiresSaveAsFlatDXFOutputDirectory = RequiresSaveAsFlatDXFOutputDirectory
        Entry.RequiresFindReplaceFields = RequiresFindReplaceFields
        Entry.RequiresPrinter = RequiresPrinter

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
                     RequiresSave:=True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        HelpString = "    Updates the file with face and view styles from a file you specify on the Configuration tab.  " + vbCrLf
        HelpString += "    Note, the view style must be a named style.  Overrides are ignored.  "
        HelpString += "To create a named style from an override, use 'Save As' on the View Overrides dialog."
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     HelpString,
                     RequiresTemplate:=True, RequiresSave:=True)

        Dim RemoveFaceStyleOverrides As New L2A
        HelpString = "    Face style overrides change a part's appearance in the assembly.  "
        HelpString += "This command causes the part to appear the same in the part file and the assembly."
        PopulateList(RemoveFaceStyleOverrides,
                     "Remove face style overrides",
                     "RemoveFaceStyleOverrides",
                     HelpString,
                     RequiresSave:=True)

        Dim FitIsometricView As New L2A
        HelpString = "    Hides reference planes, maximizes the window, sets the view to iso, and does a fit."
        PopulateList(FitIsometricView,
                     "Fit isometric view",
                     "FitIsometricView",
                     HelpString,
                     RequiresSave:=True)

        Dim MissingDrawing As New L2A
        HelpString = "    Assumes drawing has the same name as the model, and is in the same directory"
        PopulateList(MissingDrawing,
                     "Missing drawing",
                     "MissingDrawing",
                     HelpString)

        Dim OccurrenceMissingFiles As New L2A
        HelpString = "    Checks to see if any assembly occurrence is pointing to a file not found on disk."
        PopulateList(OccurrenceMissingFiles,
                     "Occurrence missing files",
                     "OccurrenceMissingFiles",
                     HelpString)

        Dim OccurrenceOutsideProjectDirectory As New L2A
        HelpString = "    Checks to see if any assembly occurrence resides outside the input directory specified on the General tab.  " + vbCrLf
        HelpString += "    Only useful when a project is housed in one top-level directory."
        PopulateList(OccurrenceOutsideProjectDirectory,
                     "Occurrence outside project directory",
                     "OccurrenceOutsideProjectDirectory",
                     HelpString)

        Dim FailedRelationships As New L2A
        HelpString = "    Checks if any assembly occurrences have conflicting or otherwise broken relationships."
        PopulateList(FailedRelationships,
                     "Failed relationships",
                     "FailedRelationships",
                     HelpString)

        Dim UnderconstrainedRelationships As New L2A
        HelpString = "    Checks if any assembly occurrences have missing relationships."
        PopulateList(UnderconstrainedRelationships,
                     "Underconstrained relationships",
                     "UnderconstrainedRelationships",
                     HelpString)

        Dim PartNumberDoesNotMatchFilename As New L2A
        HelpString = "    Checks if a file property, that you specify on the Configuration tab, matches the file name."
        PopulateList(PartNumberDoesNotMatchFilename,
                     "Part number does not match file name",
                     "PartNumberDoesNotMatchFilename",
                     HelpString,
                     RequiresPartNumberFields:=True)

        Dim SaveAs As New L2A
        HelpString = "    Exports the file to a non-Solid Edge format.  "
        HelpString += vbCrLf + "    Select the file type using the Save As combobox.  "
        HelpString += "Select the directory using the Save As Browse button, "
        HelpString += "or check the Original Directory checkbox.  "
        HelpString += "These controls are on the task tab below the task list.  "
        PopulateList(SaveAs,
                     "Save as",
                     "SaveAs",
                     HelpString,
                     RequiresSaveAsOutputDirectory:=True)

        Dim InteractiveEdit As New L2A
        HelpString = "    Brings up files one at a time for manual processing.  Some rules apply."
        HelpString += vbCrLf + "    It is important to leave Solid Edge in the state you found it when the file was opened.  "
        HelpString += "For example, if you open another file, such as a drawing, you need to close it.  "
        HelpString += "If you add or modify a feature, you need to click Finish.  "
        HelpString += vbCrLf + "    Also, do not Close the file or do a Save As on it.  "
        HelpString += "Housekeeper maintains a 'reference' to the file.  "
        HelpString += "Those two commands cause the reference to be lost, resulting in an exception.  "
        PopulateList(InteractiveEdit,
                     "Interactive edit",
                     "InteractiveEdit",
                     HelpString)

        Dim RunExternalProgram As New L2A
        HelpString = "    Runs an *.exe file.  Select the program with the External Program Browse button.  "
        HelpString += "It is located on the task tab below the task list.  "
        HelpString += vbCrLf + "    Several rules about the program implementation apply.  "
        HelpString += "See https://github.com/rmcanany/HousekeeperExternalPrograms for details and examples.  "
        PopulateList(RunExternalProgram,
                     "Run external program",
                     "RunExternalProgram",
                     HelpString,
                     RequiresExternalProgram:=True)

        Dim PropertyFindReplace As New L2A
        HelpString = "    Searches for text in a specified property and replaces it if found.  "
        HelpString += "The property, search text, and replacement text are entered on the task tab, "
        HelpString += "below the task list.  "
        HelpString += vbCrLf + "    A 'Property set', either 'System' or 'Custom', is required.  "
        HelpString += "System properties are in every Solid Edge file.  "
        HelpString += "They include Material, Manager, Project, etc.  "
        HelpString += "At this time, they must be in English.  "
        HelpString += "Custom properties are ones that you create, probably in a template.  "
        HelpString += vbCrLf + "    The search is case insensitive, the replace is case sensitive.  "
        HelpString += "For example, say the search is 'aluminum', "
        HelpString += "the replacement is 'ALUMINUM', "
        HelpString += "and the property value is 'Aluminum 6061-T6'.  "
        HelpString += "Then the new value would be 'ALUMINUM 6061-T6'.  "
        PopulateList(PropertyFindReplace,
                     "Property find replace",
                     "PropertyFindReplace",
                     HelpString,
                     RequiresFindReplaceFields:=True, RequiresSave:=True)

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
                     RequiresSave:=True)

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
                     RequiresMaterialTable:=True, RequiresSave:=True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     HelpString,
                     RequiresTemplate:=True, RequiresSave:=True)

        Dim FitIsometricView As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(FitIsometricView,
                     "Fit isometric view",
                     "FitIsometricView",
                     HelpString,
                     RequiresSave:=True)

        Dim MissingDrawing As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(MissingDrawing,
                     "Missing drawing",
                     "MissingDrawing",
                     HelpString)

        Dim FailedOrWarnedFeatures As New L2A
        HelpString = "    Checks if any features of the model are in the Failed or Warned status."
        PopulateList(FailedOrWarnedFeatures,
                     "Failed or warned features",
                     "FailedOrWarnedFeatures",
                     HelpString)

        Dim SuppressedOrRolledBackFeatures As New L2A
        HelpString = "    Checks if any features of the model are in the Suppressed or Rolledback status."
        PopulateList(SuppressedOrRolledBackFeatures,
                     "Suppressed or rolled back features",
                     "SuppressedOrRolledBackFeatures",
                     HelpString)

        Dim UnderconstrainedProfiles As New L2A
        HelpString = "    Checks if any profiles are not fully constrained."
        PopulateList(UnderconstrainedProfiles,
                     "Underconstrained profiles",
                     "UnderconstrainedProfiles",
                     HelpString)

        Dim InsertPartCopiesOutOfDate As New L2A
        HelpString = "    If the file has any insert part copies, checks if they are up to date."
        PopulateList(InsertPartCopiesOutOfDate,
                     "Insert part copies out of date",
                     "InsertPartCopiesOutOfDate",
                     HelpString)

        Dim MaterialNotInMaterialTable As New L2A
        HelpString = "    Checks the file's material against the material table.  "
        HelpString += "The material table is chosen on the Configuration tab.  "
        PopulateList(MaterialNotInMaterialTable,
                     "Material not in material table",
                     "MaterialNotInMaterialTable",
                     HelpString,
                     RequiresMaterialTable:=True)

        Dim PartNumberDoesNotMatchFilename As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(PartNumberDoesNotMatchFilename,
                     "Part number does not match file name",
                     "PartNumberDoesNotMatchFilename",
                     HelpString,
                     RequiresPartNumberFields:=True)

        Dim SaveAs As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(SaveAs,
                     "Save As",
                     "SaveAs",
                     HelpString,
                     RequiresSaveAsOutputDirectory:=True)

        Dim InteractiveEdit As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(InteractiveEdit,
                     "Interactive edit",
                     "InteractiveEdit",
                     HelpString)

        Dim RunExternalProgram As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(RunExternalProgram,
                     "Run external program",
                     "RunExternalProgram",
                     HelpString,
                     RequiresExternalProgram:=True)

        Dim PropertyFindReplace As New L2A
        HelpString = "    Same as the Assembly command of the same name."
        PopulateList(PropertyFindReplace,
                     "Property find replace",
                     "PropertyFindReplace",
                     HelpString,
                     RequiresFindReplaceFields:=True, RequiresSave:=True)

    End Sub

    Public Sub PopulateSheetmetal()
        Dim HelpString As String

        Dim UpdateInsertPartCopies As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(UpdateInsertPartCopies,
                     "Update insert part copies",
                     "UpdateInsertPartCopies",
                     HelpString,
                     RequiresSave:=True)

        Dim UpdateMaterialFromMaterialTable As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(UpdateMaterialFromMaterialTable,
                     "Update material from material table",
                     "UpdateMaterialFromMaterialTable",
                     HelpString,
                     RequiresMaterialTable:=True, RequiresSave:=True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     HelpString,
                     RequiresTemplate:=True, RequiresSave:=True)

        Dim FitIsometricView As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(FitIsometricView,
                     "Fit isometric view",
                     "FitIsometricView",
                     HelpString,
                     RequiresSave:=True)

        Dim MissingDrawing As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(MissingDrawing,
                     "Missing drawing",
                     "MissingDrawing",
                     HelpString)

        Dim FailedOrWarnedFeatures As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(FailedOrWarnedFeatures,
                     "Failed or warned features",
                     "FailedOrWarnedFeatures",
                     HelpString)

        Dim SuppressedOrRolledBackFeatures As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(SuppressedOrRolledBackFeatures,
                     "Suppressed or rolled back features",
                     "SuppressedOrRolledBackFeatures",
                     HelpString)

        Dim UnderconstrainedProfiles As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(UnderconstrainedProfiles,
                     "Underconstrained profiles",
                     "UnderconstrainedProfiles",
                     HelpString)

        Dim InsertPartCopiesOutOfDate As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(InsertPartCopiesOutOfDate,
                     "Insert part copies out of date",
                     "InsertPartCopiesOutOfDate",
                     HelpString)

        Dim FlatPatternMissingOrOutOfDate As New L2A
        HelpString = "    Checks for the existence of a flat pattern.  "
        HelpString += "If one is found, checks if it is up to date.  "
        PopulateList(FlatPatternMissingOrOutOfDate,
                     "Flat pattern missing or out of date",
                     "FlatPatternMissingOrOutOfDate",
                     HelpString)

        Dim MaterialNotInMaterialTable As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(MaterialNotInMaterialTable,
                     "Material not in material table",
                     "MaterialNotInMaterialTable",
                     HelpString,
                     RequiresMaterialTable:=True)

        Dim PartNumberDoesNotMatchFilename As New L2A
        HelpString = "    Same as the part command of the same name."
        PopulateList(PartNumberDoesNotMatchFilename,
                     "Part number does not match file name",
                     "PartNumberDoesNotMatchFilename",
                     HelpString,
                     RequiresPartNumberFields:=True)

        Dim GenerateLaserDXFAndPDF As New L2A
        HelpString = "    Creates a DXF file of the sheet metal flat pattern.  "
        HelpString += "Creates a PDF of the drawing file.  "
        HelpString += "Select the directory using the Laser Files Browse button, "
        HelpString += "or check the Original Directory checkbox.  "
        HelpString += vbCrLf + "  If the flat pattern is missing or out of date, or if the drawing is out of date, "
        HelpString += "it is reported in the log file."
        HelpString += vbCrLf + "    Note, the drawing file must have the same name "
        HelpString += "and directory as the sheet metal file.  "
        PopulateList(GenerateLaserDXFAndPDF,
                     "Generate Laser DXF and PDF",
                     "GenerateLaserDXFAndPDF",
                     HelpString,
                     RequiresLaserOutputDirectory:=True, RequiresSave:=True)

        Dim SaveAs As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(SaveAs,
                     "Save As",
                     "SaveAs",
                     HelpString,
                     RequiresSaveAsOutputDirectory:=True)

        Dim SaveAsFlatDXF As New L2A
        HelpString = "    Saves a flat pattern as a DXF file.  "
        HelpString += vbCrLf + "    Select the file type using the Save As Flat combobox.  "
        HelpString += "Select the directory using the Save As Flat Browse button, "
        HelpString += "or save it in the orginal directory checking the Original directory checkbox.  "
        PopulateList(SaveAsFlatDXF,
                     "Save As Flat DXF",
                     "SaveAsFlatDXF",
                     HelpString,
                     RequiresSaveAsFlatDXFOutputDirectory:=True)

        Dim InteractiveEdit As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(InteractiveEdit,
                     "Interactive edit",
                     "InteractiveEdit",
                     HelpString)

        Dim RunExternalProgram As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(RunExternalProgram,
                     "Run external program",
                     "RunExternalProgram",
                     HelpString,
                     RequiresExternalProgram:=True)

        Dim PropertyFindReplace As New L2A
        HelpString = "    Same as the Assembly command of the same name."
        PopulateList(PropertyFindReplace,
                     "Property find replace",
                     "PropertyFindReplace",
                     HelpString,
                     RequiresFindReplaceFields:=True, RequiresSave:=True)

    End Sub

    Private Sub PopulateDraft()
        Dim HelpString As String

        Dim UpdateDrawingViews As New L2A
        HelpString = "    Checks drawing views one by one, and updates them if needed."
        PopulateList(UpdateDrawingViews,
                     "Update drawing views",
                     "UpdateDrawingViews",
                     HelpString,
                     RequiresSave:=True)

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
                     RequiresTemplate:=True, RequiresSave:=True, IncompatibleWithOtherTasks:=True)

        Dim FitView As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(FitView,
                     "Fit view",
                     "FitView",
                     HelpString,
                     RequiresSave:=True)

        Dim DrawingViewsMissingFile As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(DrawingViewsMissingFile,
                     "Drawing views missing file",
                     "DrawingViewsMissingFile",
                     HelpString)

        Dim DrawingViewsOutOfDate As New L2A
        HelpString = "    Checks if drawing views are not up to date."
        PopulateList(DrawingViewsOutOfDate,
                     "Drawing views out of date",
                     "DrawingViewsOutOfDate",
                     HelpString)

        Dim DetachedDimensionsOrAnnotations As New L2A
        HelpString = "    Checks that dimensions, balloons, callouts, etc. are attached to geometry in the drawing."
        PopulateList(DetachedDimensionsOrAnnotations,
                     "Detached dimensions or annotations",
                     "DetachedDimensionsOrAnnotations",
                     HelpString)

        Dim FileNameDoesNotMatchModelFilename As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(FileNameDoesNotMatchModelFilename,
                     "File name does not match model file name",
                     "FileNameDoesNotMatchModelFilename",
                     HelpString)

        Dim SaveAs As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(SaveAs,
                     "Save As",
                     "SaveAs",
                     HelpString,
                     RequiresSaveAsOutputDirectory:=True)

        Dim Print As New L2A
        HelpString = "    Print settings are accessed on the Configuration tab." + vbCrLf
        HelpString += "    Note, the presence of the Printer Settings dialog is somewhat misleading.  "
        HelpString += "The only settings taken from it are the printer name, page height and width, "
        HelpString += "and the number of copies.  "
        HelpString += "Any other selections "
        HelpString += "revert back to the Windows defaults when printing.  "
        HelpString += "A workaround is to create a new Windows printer with the desired defaults.  " + vbCrLf
        HelpString += "    Another quirk is that, no matter the selection, the page width "
        HelpString += "is always listed as greater than or equal to the page height.  "
        HelpString += "In most cases, checking 'Auto orient' should provide the desired result.  "
        PopulateList(Print,
                     "Print",
                     "Print",
                     HelpString,
                     RequiresPrinter:=True)

        Dim InteractiveEdit As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(InteractiveEdit,
                     "Interactive edit",
                     "InteractiveEdit",
                     HelpString)

        Dim RunExternalProgram As New L2A
        HelpString = "    Same as the assembly command of the same name."
        PopulateList(RunExternalProgram,
                     "Run external program",
                     "RunExternalProgram",
                     HelpString,
                     RequiresExternalProgram:=True)


    End Sub


End Class
