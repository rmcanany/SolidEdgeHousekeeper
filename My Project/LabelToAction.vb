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
        Public Property RequiresPictorialView As Boolean
        Public Property RequiresForegroundProcessing As Boolean
        Public Property RequiresExposeVariables As Boolean




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
                            Optional RequiresPrinter As Boolean = False,
                            Optional RequiresPictorialView As Boolean = False,
                            Optional RequiresForegroundProcessing As Boolean = False,
                            Optional RequiresExposeVariables As Boolean = False)

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
        Entry.RequiresPictorialView = RequiresPictorialView
        Entry.RequiresForegroundProcessing = RequiresForegroundProcessing
        Entry.RequiresExposeVariables = RequiresExposeVariables

        Me(LabelText) = Entry

    End Sub

    Private Sub PopulateAssembly()
        Dim HelpString As String

        Dim OpenSave As New L2A
        HelpString = "Open a document and save in the current version."
        PopulateList(OpenSave,
                     "Open/Save",
                     "OpenSave",
                     HelpString,
                     RequiresSave:=True)

        Dim ActivateAndUpdateAll As New L2A
        HelpString = "Loads all assembly occurrences' geometry into memory and does an update. "
        HelpString += "Used mainly to eliminate the gray corners on assembly drawings. "
        HelpString += vbCrLf + vbCrLf + "Can run out of memory for very large assemblies."
        PopulateList(ActivateAndUpdateAll,
                     "Activate and update all",
                     "ActivateAndUpdateAll",
                     HelpString,
                     RequiresSave:=True)

        Dim PropertyFindReplace As New L2A
        HelpString = "Searches for text in a specified property and replaces it if found. "
        HelpString += "The property, search text, and replacement text are entered on the **Task Tab**, "
        HelpString += "below the task list. "
        HelpString += vbCrLf + vbCrLf + "A `Property set`, either `System` or `Custom`, is required. "
        HelpString += "For more information, see the **Property Filter** section above. "
        HelpString += vbCrLf + vbCrLf + "There are three search modes, `PT`, `WC`, and `RX`.  "
        HelpString += "`PT` stands for 'Plain Text'.  It is simple to use, but finds literal matches only.  "
        HelpString += "`WC` stands for 'Wild Card'.  You use `*`, `?`  `[charlist]`, and `[!charlist]` according to the VB Like syntax.  "
        HelpString += "`RX` stands for 'Regex'.  It is a more comprehensive (and notorious) method of matching text.  "
        HelpString += vbCrLf + vbCrLf + "The search *is not* case sensitive, the replacement *is*. "
        HelpString += "For example, say the search is `aluminum`, "
        HelpString += "the replacement is `ALUMINUM`, "
        HelpString += "and the property value is `Aluminum 6061-T6`. "
        HelpString += "Then the new value would be `ALUMINUM 6061-T6`. "
        PopulateList(PropertyFindReplace,
                     "Property find replace",
                     "PropertyFindReplace",
                     HelpString,
                     RequiresFindReplaceFields:=True, RequiresSave:=True)

        Dim ExposeVariablesMissing As New L2A
        HelpString = "Checks to see if all the variables listed in `Variables to expose` are present in the document."
        PopulateList(ExposeVariablesMissing,
                     "Expose variables missing",
                     "ExposeVariablesMissing",
                     HelpString,
                     RequiresExposeVariables:=True)

        Dim ExposeVariables As New L2A
        HelpString = "Exposes entries from the variable table, making them available as a Custom property. "
        HelpString += "Enter the names as a comma-delimited list in the `Variables to expose` textbox. "
        HelpString += "Optionally include a different Expose Name, set off by the colon `:` character. "
        HelpString += vbCrLf + vbCrLf + "For example `var1, var2, var3`"
        HelpString += vbCrLf + vbCrLf + "Or `var1: Length, var2: Width, var3: Height`"
        HelpString += vbCrLf + vbCrLf + "Or a combination `var1: Length, var2, var3`"
        HelpString += vbCrLf + vbCrLf + "Note: You cannot use either a comma `,` or a colon `:` in the Expose Name. "
        HelpString += "Actually there's nothing stopping you, but it will not do what you want. "
        PopulateList(ExposeVariables,
                     "Expose variables",
                     "ExposeVariables",
                     HelpString,
                     RequiresSave:=True,
                     RequiresExposeVariables:=True)

        Dim RemoveFaceStyleOverrides As New L2A
        HelpString = "Face style overrides change a part's appearance in the assembly. "
        HelpString += "This command causes the part to appear the same in the part file and the assembly."
        PopulateList(RemoveFaceStyleOverrides,
                     "Remove face style overrides",
                     "RemoveFaceStyleOverrides",
                     HelpString,
                     RequiresSave:=True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        HelpString = "Updates the file with face and view styles from a file you specify on the **Configuration Tab**. "
        HelpString += vbCrLf + vbCrLf + "Note, the view style must be a named style.  Overrides are ignored. "
        HelpString += "To create a named style from an override, open the template in Solid Edge, activate the `View Overrides` dialog, and click `Save As`."
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     HelpString,
                     RequiresTemplate:=True, RequiresSave:=True)

        Dim HideConstructions As New L2A
        HelpString = "Hides all non-model elements such as reference planes, PMI dimensions, etc."
        PopulateList(HideConstructions,
                     "Hide constructions",
                     "HideConstructions",
                     HelpString,
                     RequiresSave:=True)

        Dim FitPictorialView As New L2A
        HelpString = "Maximizes the window, sets the view orientation, and does a fit. "
        HelpString += "Select the desired orientation on the **Configuration Tab**."
        PopulateList(FitPictorialView,
                     "Fit pictorial view",
                     "FitPictorialView",
                     HelpString,
                     RequiresSave:=True,
                     RequiresPictorialView:=True,
                     RequiresForegroundProcessing:=True)

        Dim PartNumberDoesNotMatchFilename As New L2A
        HelpString = "Checks if a file property, that you specify on the **Configuration Tab**, matches the file name."
        PopulateList(PartNumberDoesNotMatchFilename,
                     "Part number does not match file name",
                     "PartNumberDoesNotMatchFilename",
                     HelpString,
                     RequiresPartNumberFields:=True)

        Dim MissingDrawing As New L2A
        HelpString = "Assumes drawing has the same name as the model, and is in the same directory"
        PopulateList(MissingDrawing,
                     "Missing drawing",
                     "MissingDrawing",
                     HelpString)

        Dim BrokenLinks As New L2A
        HelpString = "Checks to see if any assembly occurrence is pointing to a file not found on disk."
        PopulateList(BrokenLinks,
                     "Broken links",
                     "BrokenLinks",
                     HelpString)

        Dim LinksOutsideInputDirectory As New L2A
        HelpString = "Checks to see if any assembly occurrence resides outside the top level directories specified on the **Home Tab**. "
        PopulateList(LinksOutsideInputDirectory,
                     "Links outside input directory",
                     "LinksOutsideInputDirectory",
                     HelpString)

        Dim FailedRelationships As New L2A
        HelpString = "Checks if any assembly occurrences have conflicting or otherwise broken relationships."
        PopulateList(FailedRelationships,
                     "Failed relationships",
                     "FailedRelationships",
                     HelpString)

        Dim UnderconstrainedRelationships As New L2A
        HelpString = "Checks if any assembly occurrences have missing relationships."
        PopulateList(UnderconstrainedRelationships,
                     "Underconstrained relationships",
                     "UnderconstrainedRelationships",
                     HelpString)

        Dim RunExternalProgram As New L2A
        HelpString = "Runs an `\*.exe` or `\*.vbs` file.  Select the program with the `Browse` button. "
        HelpString += "It is located on the **Task Tab** below the task list. "
        HelpString += vbCrLf + vbCrLf + "If you are writing your own program, be aware several interoperability rules apply. "
        HelpString += "See [**HousekeeperExternalPrograms**](https://github.com/rmcanany/HousekeeperExternalPrograms) for details and examples. "
        PopulateList(RunExternalProgram,
                     "Run external program",
                     "RunExternalProgram",
                     HelpString,
                     RequiresExternalProgram:=True)

        Dim InteractiveEdit As New L2A
        HelpString = "Brings up files one at a time for manual processing. "
        HelpString += "A dialog box lets you tell Housekeeper when you are done. "
        HelpString += vbCrLf + vbCrLf + "Some rules for interactive editing apply. "
        HelpString += "It is important to leave Solid Edge in the state you found it when the file was opened. "
        HelpString += "For example, if you open another file, such as a drawing, you need to close it. "
        HelpString += "If you add or modify a feature, you need to click Finish. "
        HelpString += vbCrLf + vbCrLf + "Also, do not Close the file or do a Save As on it. "
        HelpString += "Housekeeper maintains a `reference` to the file. "
        HelpString += "Those two commands cause the reference to be lost, resulting in an exception. "
        PopulateList(InteractiveEdit,
                     "Interactive edit",
                     "InteractiveEdit",
                     HelpString,
                     RequiresForegroundProcessing:=True)

        Dim SaveAs As New L2A
        HelpString = "Exports the file to a non-Solid Edge format. "
        HelpString += vbCrLf + vbCrLf + "Select the file type using the `Save As` combobox. "
        HelpString += "Select the directory using the `Browse` button, "
        HelpString += "or check the `Original Directory` checkbox. "
        HelpString += "These controls are on the **Task Tab** below the task list. "
        HelpString += vbCrLf + vbCrLf + "Images can be saved with the aspect ratio of the model, rather than the window. "
        HelpString += "The option is called `Save as image -- crop to model size`. "
        HelpString += "It is located on the **Configuration Tab**. "
        HelpString += vbCrLf + vbCrLf + "You can optionally create subdirectories using a formula similar to the Property Text Callout. "
        HelpString += "For example `Material %{System.Material} Thickness %{Custom.Material Thickness}`. "
        HelpString += vbCrLf + vbCrLf + "A `Property set`, either `System` or `Custom`, is required. "
        HelpString += "For more information, see the **Property Filter** section above. "
        HelpString += vbCrLf + vbCrLf + "It is possible that a property contains a character that cannot be used in a file name. "
        HelpString += "If that happens, a replacement is read from filename_charmap.txt in the same directory as Housekeeper.exe. "
        HelpString += "You can/should edit it to change the replacement characters to your preference. "
        HelpString += "The file is created the first time you run Housekeeper.  For details, see the header comments in that file. "

        PopulateList(SaveAs,
                     "Save as",
                     "SaveAs",
                     HelpString,
                     RequiresSaveAsOutputDirectory:=True)

    End Sub

    Private Sub PopulatePart()
        Dim HelpString As String

        Dim OpenSave As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(OpenSave,
                     "Open/Save",
                     "OpenSave",
                     HelpString,
                     RequiresSave:=True)

        Dim PropertyFindReplace As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(PropertyFindReplace,
                     "Property find replace",
                     "PropertyFindReplace",
                     HelpString,
                     RequiresFindReplaceFields:=True, RequiresSave:=True)

        Dim ExposeVariablesMissing As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(ExposeVariablesMissing,
                     "Expose variables missing",
                     "ExposeVariablesMissing",
                     HelpString,
                     RequiresExposeVariables:=True)

        Dim ExposeVariables As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(ExposeVariables,
                     "Expose variables",
                     "ExposeVariables",
                     HelpString,
                     RequiresSave:=True,
                     RequiresExposeVariables:=True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     HelpString,
                     RequiresTemplate:=True, RequiresSave:=True)

        Dim UpdateMaterialFromMaterialTable As New L2A
        HelpString = "Checks to see if the part's material name and properties match any material "
        HelpString += "in a file you specify on the **Configuration Tab**. "
        HelpString += vbCrLf + vbCrLf + "If the names match, "
        HelpString += "but their properties (e.g., face style) do not, the material is updated. "
        HelpString += "If the names do not match, or no material is assigned, it is reported in the log file."
        PopulateList(UpdateMaterialFromMaterialTable,
                     "Update material from material table",
                     "UpdateMaterialFromMaterialTable",
                     HelpString,
                     RequiresMaterialTable:=True, RequiresSave:=True)

        Dim HideConstructions As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(HideConstructions,
                     "Hide constructions",
                     "HideConstructions",
                     HelpString,
                     RequiresSave:=True)

        Dim FitPictorialView As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(FitPictorialView,
                     "Fit pictorial view",
                     "FitPictorialView",
                     HelpString,
                     RequiresSave:=True,
                     RequiresPictorialView:=True,
                     RequiresForegroundProcessing:=True)

        Dim UpdateInsertPartCopies As New L2A
        HelpString = "In conjuction with `Assembly Activate and update all`, "
        HelpString += "used mainly to eliminate the gray corners on assembly drawings."
        PopulateList(UpdateInsertPartCopies,
                     "Update insert part copies",
                     "UpdateInsertPartCopies",
                     HelpString,
                     RequiresSave:=True)

        Dim BrokenLinks As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(BrokenLinks,
                     "Broken links",
                     "BrokenLinks",
                     HelpString)

        Dim PartNumberDoesNotMatchFilename As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(PartNumberDoesNotMatchFilename,
                     "Part number does not match file name",
                     "PartNumberDoesNotMatchFilename",
                     HelpString,
                     RequiresPartNumberFields:=True)

        Dim MissingDrawing As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(MissingDrawing,
                     "Missing drawing",
                     "MissingDrawing",
                     HelpString)

        Dim FailedOrWarnedFeatures As New L2A
        HelpString = "Checks if any features of the model are in the Failed or Warned status."
        PopulateList(FailedOrWarnedFeatures,
                     "Failed or warned features",
                     "FailedOrWarnedFeatures",
                     HelpString)

        Dim SuppressedOrRolledBackFeatures As New L2A
        HelpString = "Checks if any features of the model are in the Suppressed or Rolledback status."
        PopulateList(SuppressedOrRolledBackFeatures,
                     "Suppressed or rolled back features",
                     "SuppressedOrRolledBackFeatures",
                     HelpString)

        Dim UnderconstrainedProfiles As New L2A
        HelpString = "Checks if any profiles are not fully constrained."
        PopulateList(UnderconstrainedProfiles,
                     "Underconstrained profiles",
                     "UnderconstrainedProfiles",
                     HelpString)

        Dim InsertPartCopiesOutOfDate As New L2A
        HelpString = "If the file has any insert part copies, checks if they are up to date."
        PopulateList(InsertPartCopiesOutOfDate,
                     "Insert part copies out of date",
                     "InsertPartCopiesOutOfDate",
                     HelpString)

        Dim MaterialNotInMaterialTable As New L2A
        HelpString = "Checks the file's material against the material table. "
        HelpString += "The material table is chosen on the **Configuration Tab**. "
        PopulateList(MaterialNotInMaterialTable,
                     "Material not in material table",
                     "MaterialNotInMaterialTable",
                     HelpString,
                     RequiresMaterialTable:=True)

        Dim RunExternalProgram As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(RunExternalProgram,
                     "Run external program",
                     "RunExternalProgram",
                     HelpString,
                     RequiresExternalProgram:=True)

        Dim InteractiveEdit As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(InteractiveEdit,
                     "Interactive edit",
                     "InteractiveEdit",
                     HelpString,
                     RequiresForegroundProcessing:=True)

        Dim SaveAs As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(SaveAs,
                     "Save As",
                     "SaveAs",
                     HelpString,
                     RequiresSaveAsOutputDirectory:=True)

    End Sub

    Public Sub PopulateSheetmetal()
        Dim HelpString As String

        Dim OpenSave As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(OpenSave,
                     "Open/Save",
                     "OpenSave",
                     HelpString,
                     RequiresSave:=True)

        Dim PropertyFindReplace As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(PropertyFindReplace,
                     "Property find replace",
                     "PropertyFindReplace",
                     HelpString,
                     RequiresFindReplaceFields:=True, RequiresSave:=True)

        Dim ExposeVariablesMissing As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(ExposeVariablesMissing,
                     "Expose variables missing",
                     "ExposeVariablesMissing",
                     HelpString,
                     RequiresExposeVariables:=True)

        Dim ExposeVariables As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(ExposeVariables,
                     "Expose variables",
                     "ExposeVariables",
                     HelpString,
                     RequiresSave:=True,
                     RequiresExposeVariables:=True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        HelpString = "Same as the Part command of the same name."
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     HelpString,
                     RequiresTemplate:=True, RequiresSave:=True)

        Dim UpdateMaterialFromMaterialTable As New L2A
        HelpString = "Same as the Part command of the same name."
        PopulateList(UpdateMaterialFromMaterialTable,
                     "Update material from material table",
                     "UpdateMaterialFromMaterialTable",
                     HelpString,
                     RequiresMaterialTable:=True, RequiresSave:=True)

        Dim HideConstructions As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(HideConstructions,
                     "Hide constructions",
                     "HideConstructions",
                     HelpString,
                     RequiresSave:=True)

        Dim FitPictorialView As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(FitPictorialView,
                     "Fit pictorial view",
                     "FitPictorialView",
                     HelpString,
                     RequiresSave:=True,
                     RequiresPictorialView:=True,
                     RequiresForegroundProcessing:=True)

        Dim UpdateInsertPartCopies As New L2A
        HelpString = "Same as the Part command of the same name."
        PopulateList(UpdateInsertPartCopies,
                     "Update insert part copies",
                     "UpdateInsertPartCopies",
                     HelpString,
                     RequiresSave:=True)

        Dim UpdateDesignForCost As New L2A
        HelpString = "Updates DesignForCost and saves the document."
        HelpString += vbCrLf + vbCrLf + "An annoyance of this command is that it opens the "
        HelpString += "DesignForCost Edgebar pane, but is not able to close it. "
        HelpString += "The user must manually close the pane in an interactive Sheetmetal session. "
        HelpString += "The state of the pane is system-wide, not per-document, "
        HelpString += "so closing it is a one-time action. "
        PopulateList(UpdateDesignForCost,
                     "Update design for cost",
                     "UpdateDesignForCost",
                     HelpString,
                     RequiresSave:=True)

        Dim BrokenLinks As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(BrokenLinks,
                     "Broken links",
                     "BrokenLinks",
                     HelpString)

        Dim PartNumberDoesNotMatchFilename As New L2A
        HelpString = "Same as the Part command of the same name."
        PopulateList(PartNumberDoesNotMatchFilename,
                     "Part number does not match file name",
                     "PartNumberDoesNotMatchFilename",
                     HelpString,
                     RequiresPartNumberFields:=True)

        Dim MissingDrawing As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(MissingDrawing,
                     "Missing drawing",
                     "MissingDrawing",
                     HelpString)

        Dim FailedOrWarnedFeatures As New L2A
        HelpString = "Same as the Part command of the same name."
        PopulateList(FailedOrWarnedFeatures,
                     "Failed or warned features",
                     "FailedOrWarnedFeatures",
                     HelpString)

        Dim SuppressedOrRolledBackFeatures As New L2A
        HelpString = "Same as the Part command of the same name."
        PopulateList(SuppressedOrRolledBackFeatures,
                     "Suppressed or rolled back features",
                     "SuppressedOrRolledBackFeatures",
                     HelpString)

        Dim UnderconstrainedProfiles As New L2A
        HelpString = "Same as the Part command of the same name."
        PopulateList(UnderconstrainedProfiles,
                     "Underconstrained profiles",
                     "UnderconstrainedProfiles",
                     HelpString)

        Dim InsertPartCopiesOutOfDate As New L2A
        HelpString = "Same as the Part command of the same name."
        PopulateList(InsertPartCopiesOutOfDate,
                     "Insert part copies out of date",
                     "InsertPartCopiesOutOfDate",
                     HelpString)

        Dim FlatPatternMissingOrOutOfDate As New L2A
        HelpString = "Checks for the existence of a flat pattern. "
        HelpString += "If one is found, checks if it is up to date. "
        PopulateList(FlatPatternMissingOrOutOfDate,
                     "Flat pattern missing or out of date",
                     "FlatPatternMissingOrOutOfDate",
                     HelpString)

        Dim MaterialNotInMaterialTable As New L2A
        HelpString = "Same as the Part command of the same name."
        PopulateList(MaterialNotInMaterialTable,
                     "Material not in material table",
                     "MaterialNotInMaterialTable",
                     HelpString,
                     RequiresMaterialTable:=True)

        Dim RunExternalProgram As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(RunExternalProgram,
                     "Run external program",
                     "RunExternalProgram",
                     HelpString,
                     RequiresExternalProgram:=True)

        Dim InteractiveEdit As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(InteractiveEdit,
                     "Interactive edit",
                     "InteractiveEdit",
                     HelpString,
                     RequiresForegroundProcessing:=True)

        Dim SaveAs As New L2A
        HelpString = "Same as the Assembly command of the same name, "
        HelpString += "except two additional options -- `DXF Flat (\*.dxf)` and `PDF Drawing (\*.pdf)`. "
        HelpString += vbCrLf + vbCrLf + "The `DXF Flat` option saves the flat pattern of the sheet metal file. "
        HelpString += vbCrLf + vbCrLf + "The `PDF Drawing` option saves the drawing of the sheet metal file. "
        HelpString += "The drawing must have the same name as the model, and be in the same directory. "
        HelpString += "A more flexible option may be to use the Draft `Save As`, "
        HelpString += "using a `Property Filter` if needed. "
        PopulateList(SaveAs,
                     "Save As",
                     "SaveAs",
                     HelpString,
                     RequiresSaveAsOutputDirectory:=True)

    End Sub

    Private Sub PopulateDraft()
        Dim HelpString As String

        Dim OpenSave As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(OpenSave,
                     "Open/Save",
                     "OpenSave",
                     HelpString,
                     RequiresSave:=True)

        Dim UpdateDrawingViews As New L2A
        HelpString = "Checks drawing views one by one, and updates them if needed."
        PopulateList(UpdateDrawingViews,
                     "Update drawing views",
                     "UpdateDrawingViews",
                     HelpString,
                     RequiresSave:=True)


        Dim UpdateStylesFromTemplate As New L2A
        HelpString = "Creates a new file from a template you specify on the **Configuration Tab**. "
        HelpString += "Copies drawing views, dimensions, etc. from the old file into the new one. "
        HelpString += "If the template has updated styles, a different background sheet, or other changes, "
        HelpString += "the new drawing will inherit them automatically. "
        HelpString += vbCrLf + vbCrLf + "This task has the option to `Allow partial success`.  It is set on the **Configuration Tab**. "
        HelpString += "If the option is set, and some drawing elements were not transferred, "
        HelpString += "it is reported in the log file. "
        HelpString += "Also reported in the log file are instructions for completing the transfer. "
        HelpString += vbCrLf + vbCrLf + "Note, because this task needs to do a `Save As`, it must be run with no other tasks selected."
        PopulateList(UpdateStylesFromTemplate,
                     "Update styles from template",
                     "UpdateStylesFromTemplate",
                     HelpString,
                     RequiresTemplate:=True, RequiresSave:=True, IncompatibleWithOtherTasks:=True)

        Dim UpdateDrawingBorderFromTemplate As New L2A
        HelpString = "Replaces the background border with that of the Draft template specified on "
        HelpString += "the **Configuration Tab**."
        HelpString += vbCrLf + vbCrLf + "In contrast to `UpdateStylesFromTemplate`, this command only replaces the border. "
        HelpString += "It does not attempt to update styles or anything else."
        PopulateList(UpdateDrawingBorderFromTemplate,
                     "Update drawing border from template",
                     "UpdateDrawingBorderFromTemplate",
                     HelpString,
                     RequiresTemplate:=True, RequiresSave:=True)

        Dim FitView As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(FitView,
                     "Fit view",
                     "FitView",
                     HelpString,
                     RequiresSave:=True,
                     RequiresForegroundProcessing:=True)

        Dim FileNameDoesNotMatchModelFilename As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(FileNameDoesNotMatchModelFilename,
                     "File name does not match model file name",
                     "FileNameDoesNotMatchModelFilename",
                     HelpString)

        Dim BrokenLinks As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(BrokenLinks,
                     "Broken links",
                     "BrokenLinks",
                     HelpString)

        Dim DrawingViewsOutOfDate As New L2A
        HelpString = "Checks if drawing views are not up to date."
        PopulateList(DrawingViewsOutOfDate,
                     "Drawing views out of date",
                     "DrawingViewsOutOfDate",
                     HelpString)

        Dim DetachedDimensionsOrAnnotations As New L2A
        HelpString = "Checks that dimensions, balloons, callouts, etc. are attached to geometry in the drawing."
        PopulateList(DetachedDimensionsOrAnnotations,
                     "Detached dimensions or annotations",
                     "DetachedDimensionsOrAnnotations",
                     HelpString)

        Dim PartsListMissingOrOutOfDate As New L2A
        HelpString = "Checks is there are any parts list in the drawing and if they are all up to date."
        PopulateList(PartsListMissingOrOutOfDate,
                     "Parts list missing or out of date",
                     "PartsListMissingOrOutOfDate",
                     HelpString)

        Dim RunExternalProgram As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(RunExternalProgram,
                     "Run external program",
                     "RunExternalProgram",
                     HelpString,
                     RequiresExternalProgram:=True)


        Dim InteractiveEdit As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(InteractiveEdit,
                     "Interactive edit",
                     "InteractiveEdit",
                     HelpString,
                     RequiresForegroundProcessing:=True)

        Dim Print As New L2A
        HelpString = "Print settings are accessed on the **Configuration Tab**."
        HelpString += vbCrLf + vbCrLf + "Note, the presence of the Printer Settings dialog is somewhat misleading. "
        HelpString += "The only settings taken from it are the printer name, page height and width, "
        HelpString += "and the number of copies. "
        HelpString += "Any other selections "
        HelpString += "revert back to the Windows defaults when printing. "
        HelpString += "A workaround is to create a new Windows printer with the desired defaults. "
        HelpString += vbCrLf + vbCrLf + "Another quirk is that, no matter the selection, the page width "
        HelpString += "is always listed as greater than or equal to the page height. "
        HelpString += "In most cases, checking `Auto orient` should provide the desired result. "
        PopulateList(Print,
                     "Print",
                     "Print",
                     HelpString,
                     RequiresPrinter:=True)

        Dim SaveAs As New L2A
        HelpString = "Same as the Assembly command of the same name, except as follows."
        HelpString += vbCrLf + vbCrLf + "Optionally includes a watermark image on the output.  For the watermark, "
        HelpString += "set X/W and Y/H to position the image, and Scale to change its size. "
        HelpString += "The X/W and Y/H values are fractions of the sheet's "
        HelpString += "width and height, respectively. "
        HelpString += "So, (`0,0`) means lower left, (`0.5,0.5`) means centered, etc. "
        HelpString += "Note some file formats may not support bitmap output."
        HelpString += vbCrLf + vbCrLf + "The option `Use subdirectory formula` can use an Index Reference designator "
        HelpString += "to select a model file contained in the draft file. "
        HelpString += "This is similar to Property Text in a Callout, "
        HelpString += "for example, `%{System.Material|R1}`. "
        HelpString += "To refer to properties of the draft file itself, do not specify a designator, "
        HelpString += "for example, `%{Custom.Last Revision Date}`. "
        PopulateList(SaveAs,
                     "Save As",
                     "SaveAs",
                     HelpString,
                     RequiresSaveAsOutputDirectory:=True)

    End Sub


End Class
