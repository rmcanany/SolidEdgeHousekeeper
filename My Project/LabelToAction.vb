Option Strict On
Imports Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System


Imports SolidEdgePart




' The order on the CheckBoxLists is set by the order of creation of the L2A objects.




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
        Public Property RequiresVariablesToEdit As Boolean
        Public Property RequiresOverallSizeVariables As Boolean




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
                            Optional RequiresVariablesToEdit As Boolean = False,
                            Optional RequiresOverallSizeVariables As Boolean = False)

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
        Entry.RequiresVariablesToEdit = RequiresVariablesToEdit
        Entry.RequiresOverallSizeVariables = RequiresOverallSizeVariables

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
        HelpString += "The property, search text, and replacement text are entered on the Input Editor. "
        HelpString += "Activate the editor using the `Property find/replace` `Edit` button on the **Task Tab** below the task list. "
        HelpString += ""
        HelpString += vbCrLf + vbCrLf + "![Find_Replace](My%20Project/media/property_input_editor.png)"
        HelpString += ""
        HelpString += vbCrLf + vbCrLf + "A `Property set`, either `System` or `Custom`, is required. "
        HelpString += "For more information, see the **Property Filter** section above. "
        HelpString += vbCrLf + vbCrLf + "There are three search modes, `PT`, `WC`, and `RX`. "
        HelpString += vbCrLf + vbCrLf + "- `PT` stands for 'Plain Text'.  It is simple to use, but finds literal matches only. "
        HelpString += vbCrLf + "- `WC` stands for 'Wild Card'.  You use `*`, `?`  `[charlist]`, and `[!charlist]` according to the VB Like syntax. "
        HelpString += vbCrLf + "- `RX` stands for 'Regex'.  It is a more comprehensive (and notoriously cryptic) method of matching text. "
        HelpString += "Check the [<ins>**.NET Regex Guide**</ins>](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference) "
        HelpString += "for more information."
        HelpString += vbCrLf + vbCrLf + "The search *is not* case sensitive, the replacement *is*. "
        HelpString += "For example, say the search is `aluminum`, "
        HelpString += "the replacement is `ALUMINUM`, "
        HelpString += "and the property value is `Aluminum 6061-T6`. "
        HelpString += "Then the new value would be `ALUMINUM 6061-T6`. "
        ' HelpString += vbCrLf + vbCrLf + "![Property Formula](My%20Project/media/property_formula.png)"
        HelpString += vbCrLf + vbCrLf + "In addition to plain text and pattern matching, you can also use "
        HelpString += "a property formula.  The formula has the same syntax as the Callout command, "
        HelpString += "except preceeded with `System.` or `Custom.` as shown in the Input Editor above. "
        HelpString += vbCrLf + vbCrLf + "If the specified property does not exist in the file, "
        HelpString += "you can optionally have it added automatically. "
        HelpString += "This option is set on the **Configuration Tab -- General Page**. "
        HelpString += "Note, this only works for `Custom` properties.  Adding `System` properties is not allowed. "
        HelpString += vbCrLf + vbCrLf + "If you are changing `System.Material` specifically, there is an option "
        HelpString += "to automatically update the part's material properties (density, face styles, etc.). "
        HelpString += "Set the option on the **Configuration Tab -- General Page**. "
        HelpString += vbCrLf + vbCrLf + "The properties are processed in the order in the table. "
        HelpString += "You can change the order by selecting a row and using the Up/Down buttons "
        HelpString += "at the top of the form.  Only one row can be moved at a time. "
        HelpString += "The delete button, also at the top of the form, removes selected rows. "
        HelpString += vbCrLf + vbCrLf + "You can copy the settings on the form to other tabs. "
        HelpString += "Set the `Copy To` CheckBoxes as desired."
        HelpString += vbCrLf + vbCrLf + "Note the textbox adjacent to the `Edit` button "
        HelpString += "is a `Dictionary` representation of the table settings in `JSON` format. "
        HelpString += "You can edit it if you want, but the form is probably easier to use. "
        PopulateList(PropertyFindReplace,
                     "Property find replace",
                     "PropertyFindReplace",
                     HelpString,
                     RequiresFindReplaceFields:=True, RequiresSave:=True)

        Dim UpdatePhysicalProperties As New L2A
        HelpString = "Updates mass, volume, etc.  Models with no density are reported in the log file. "
        HelpString += vbCrLf + vbCrLf + "You can optionally control the display of the center of mass symbol. "
        HelpString += "It can either be shown, hidden, or left unchanged. "
        HelpString += "The option is set on the **Configuration Tab -- General Page**. "
        HelpString += "To leave the symbol's display unchanged, "
        HelpString += "disable both the `Show` and `Hide` options. "
        HelpString += "Note, controlling the symbol display only works for assembly files at this time. "
        HelpString += vbCrLf + vbCrLf + "Occasionally, the physical properties are updated correctly, "
        HelpString += "but the results are not carried over to the Variable Table. "
        HelpString += "The error is detected and reported in the log file. The easiest fix I've found "
        HelpString += "is to open the file in SE, change the material, then change it right back. "
        HelpString += "You can verify if it worked by checking for `Mass` in the Variable Table. "
        PopulateList(UpdatePhysicalProperties,
                     "Update physical properties",
                     "UpdatePhysicalProperties",
                      HelpString,
                      RequiresSave:=True)

        Dim VariablesEdit As New L2A
        HelpString = "Adds, changes, and/or exposes variables.  The information is entered on the Input Editor. "
        HelpString += "Access the form using the `Variables edit/add/expose` `Edit` button. "
        HelpString += "It is located below the task list on each **Task Tab**."
        HelpString += vbCrLf + vbCrLf + "![Variable_Editor](My%20Project/media/variable_input_editor.png)"
        HelpString += vbCrLf + vbCrLf + "The Variable name is required.  There are restrictions on the name.  "
        HelpString += "It cannot start with a number.  It can only contain letters and numbers and the "
        HelpString += "underscore '_' character."
        HelpString += vbCrLf + vbCrLf + "If a variable on the list is not in the file, it can optionally be added automatically.  "
        HelpString += "Set the option on the **Configuration Tab -- General Page**. "
        HelpString += vbCrLf + vbCrLf + "The number/formula is not required if only exposing an existing variable, "
        HelpString += "otherwise it is.  If a formula references a variable not in the file, the "
        HelpString += "program will report an error."
        HelpString += vbCrLf + vbCrLf + "If exposing a variable, the Expose name defaults to the variable name. "
        HelpString += "You can optionally change it.  The Expose name does not have restrictions like the variable name. "
        HelpString += vbCrLf + vbCrLf + "The variables are processed in the order in the table. "
        HelpString += "You can change the order by selecting a row and using the Up/Down buttons "
        HelpString += "at the top of the form.  Only one row can be moved at a time.  "
        HelpString += "The delete button, also at the top of the form, removes selected rows.  "
        HelpString += vbCrLf + vbCrLf + "You can copy the settings on the form to other tabs.  "
        HelpString += "Set the `Copy To` CheckBoxes as desired."
        HelpString += vbCrLf + vbCrLf + "Note the textbox adjacent to the `Edit` button "
        HelpString += "is a `Dictionary` representation of the table settings in `JSON` format. "
        HelpString += "You can edit it if you want, but the form is probably easier to use. "
        PopulateList(VariablesEdit,
                     "Variables add/edit/expose",
                     "VariablesEdit",
                     HelpString,
                     RequiresVariablesToEdit:=True)

        Dim CopyOverallSizeToVariableTable As New L2A
        HelpString = "Copies the model size to the variable table. "
        HelpString += "This is primarily intended for standard cross-section material "
        HelpString += "(barstock, channel, etc.), but can be used for any purpose. "
        HelpString += "Exposes the variables so they can be used in a callout, parts list, or the like. "
        HelpString += vbCrLf + vbCrLf + "The size is determined using the built-in Solid Edge `RangeBox`. "
        HelpString += "The range box is oriented along the XYZ axes. "
        HelpString += "Misleading values will result for parts with an off axis orientation, such as a 3D tube. "
        HelpString += vbCrLf + vbCrLf + "![Overall Size Options](My%20Project/media/overall_size_options.png)"
        HelpString += vbCrLf + vbCrLf + "The size can be reported as `XYZ`, or `MinMidMax`, or both. "
        HelpString += "`MinMidMax` is independent of the part's orientation in the file. "
        HelpString += "Set your preference on the **Configuration Tab -- General Page**. "
        HelpString += "Set the desired variable names there, too. "
        HelpString += vbCrLf + vbCrLf + "Note that the values are non-associative copies. "
        HelpString += "Any change to the model will require rerunning this command to update the variable table. "
        HelpString += vbCrLf + vbCrLf + "The command reports sheet metal size in the formed state. "
        HelpString += "For a flat pattern, instead of this using this command, "
        HelpString += "you can use the variables from the flat pattern command -- "
        HelpString += "`Flat_Pattern_Model_CutSizeX`, `Flat_Pattern_Model_CutSizeY`, and `Sheet Metal Gage`. "
        PopulateList(CopyOverallSizeToVariableTable,
                     "Copy overall size to variable table",
                     "CopyOverallSizeToVariableTable",
                     HelpString,
                     RequiresOverallSizeVariables:=True)

        Dim RemoveFaceStyleOverrides As New L2A
        HelpString = "Face style overrides change a part's appearance in the assembly. "
        HelpString += "This command causes the part to appear the same in the part file and the assembly."
        PopulateList(RemoveFaceStyleOverrides,
                     "Remove face style overrides",
                     "RemoveFaceStyleOverrides",
                     HelpString,
                     RequiresSave:=True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        HelpString = "Updates the file with face and view styles from a file you specify on the **Configuration Tab -- Templates Page**. "
        HelpString += vbCrLf + vbCrLf + "Note, the view style must be a named style.  Overrides are ignored. "
        HelpString += "To create a named style from an override, open the template in Solid Edge, activate the `View Overrides` dialog, and click `Save As`."
        HelpString += vbCrLf + vbCrLf + "![View Override Dialog](My%20Project/media/view_override_dialog.png)"
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
        HelpString += "Select the desired orientation on the **Configuration Tab -- General Page**."
        PopulateList(FitPictorialView,
                     "Fit pictorial view",
                     "FitPictorialView",
                     HelpString,
                     RequiresSave:=True,
                     RequiresPictorialView:=True,
                     RequiresForegroundProcessing:=True)

        Dim PartNumberDoesNotMatchFilename As New L2A
        HelpString = "Checks if the file name contains the part number. "
        HelpString += "The part number is drawn from a property you specify on the **Configuration Tab -- General Page**. "
        HelpString += "It only checks that the part number appears somewhere in the file name. "
        HelpString += "If the part number is, say, `7481-12104` and the file name is `7481-12104 Motor Mount.par`, "
        HelpString += "you will get a match. "
        HelpString += vbCrLf + vbCrLf + "![part_number_matches_file_name](My%20Project/media/part_number_matches_file_name.png)"
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

        Dim CheckInterference As New L2A
        HelpString = "Runs an interference check.  All parts are checked against all others. "
        HelpString += "This can take a long time on large assemblies, "
        HelpString += "so there is a limit to the number of parts to check. "
        HelpString += "Set it on the **Configuration Tab -- General Page**."
        PopulateList(CheckInterference,
                     "Check interference",
                     "CheckInterference",
                     HelpString)

        'Dim ConfigurationsOutOfDate As New L2A
        'HelpString = ""
        'PopulateList(ConfigurationsOutOfDate,
        '             "Configurations out of date",
        '             "ConfigurationsOutOfDate",
        '             HelpString)

        Dim RunExternalProgram As New L2A
        HelpString = "Runs an `*.exe` or `*.vbs` or `*.ps1` file.  Select the program with the `Browse` button. "
        HelpString += "It is located on the **Task Tab** below the task list. "
        HelpString += vbCrLf + vbCrLf + "If you are writing your own program, be aware several interoperability rules apply. "
        HelpString += "See [<ins>**HousekeeperExternalPrograms**</ins>](https://github.com/rmcanany/HousekeeperExternalPrograms) for details and examples. "
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
        HelpString = "Exports the file to either a non-Solid Edge format, or the same format in a different directory. "
        HelpString += vbCrLf + vbCrLf + "Select the file type using the `Save As` combobox. "
        HelpString += "Select the directory using the `Browse` button, "
        HelpString += "or check the `Original Directory` checkbox. "
        HelpString += "These controls are on the **Task Tab** below the task list. "
        HelpString += vbCrLf + vbCrLf + "Images can be saved with the aspect ratio of the model, rather than the window. "
        HelpString += "The option is called `Save as image -- crop to model size`. "
        HelpString += "It is located on the **Configuration Tab -- General Page**. "
        HelpString += vbCrLf + vbCrLf + "You can optionally create subdirectories using a formula similar to the Property Text Callout. "
        HelpString += "For example `Material %{System.Material} Thickness %{Custom.Material Thickness}`. "
        HelpString += vbCrLf + vbCrLf + "A `Property set`, either `System` or `Custom`, is required. "
        HelpString += "For more information, see the **Property Filter** section above. "
        HelpString += vbCrLf + vbCrLf + "It is possible that a property contains a character that cannot be used in a file name. "
        HelpString += "If that happens, a replacement is read from filename_charmap.txt in the Preferences directory in the Housekeeper root folder. "
        HelpString += "You can/should edit it to change the replacement characters to your preference. "
        HelpString += "The file is created the first time you run Housekeeper.  For details, see the header comments in that file. "

        PopulateList(SaveAs,
                     "Save As",
                     "SaveAs",
                     HelpString,
                     RequiresSaveAsOutputDirectory:=True,
                     RequiresForegroundProcessing:=True)

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

        Dim UpdatePhysicalProperties As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(UpdatePhysicalProperties,
                     "Update physical properties",
                     "UpdatePhysicalProperties",
                      HelpString,
                      RequiresSave:=True)

        Dim VariablesEdit As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(VariablesEdit,
                     "Variables add/edit/expose",
                     "VariablesEdit",
                     HelpString,
                     RequiresVariablesToEdit:=True)

        ' 
        Dim CopyOverallSizeToVariableTable As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(CopyOverallSizeToVariableTable,
                     "Copy overall size to variable table",
                     "CopyOverallSizeToVariableTable",
                     HelpString,
                     RequiresOverallSizeVariables:=True)

        Dim UpdateFaceAndViewStylesFromTemplate As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(UpdateFaceAndViewStylesFromTemplate,
                     "Update face and view styles from template",
                     "UpdateFaceAndViewStylesFromTemplate",
                     HelpString,
                     RequiresTemplate:=True, RequiresSave:=True)

        Dim UpdateMaterialFromMaterialTable As New L2A
        HelpString = "Checks to see if the part's material name and properties match any material "
        HelpString += "in a file you specify on the **Configuration Tab -- Templates Page**. "
        HelpString += vbCrLf + vbCrLf + "If the names match, "
        HelpString += "but their properties (e.g., face style) do not, the material is updated. "
        HelpString += "If the names do not match, or no material is assigned, it is reported in the log file."
        HelpString += vbCrLf + vbCrLf + "You can optionally remove any face style overrides. "
        HelpString += "Set the option on the **Configuration Tab -- General Page**. "
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
        HelpString += "used mainly to eliminate the gray corners on assembly drawings. "
        HelpString += "You can optionally update the parent files recursively. "
        HelpString += "That option is on the **Configuration Tab -- General Page**."
        PopulateList(UpdateInsertPartCopies,
                     "Update part copies",
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
        
        Dim FileExtTypeMismatch As New L2A
        HelpString = "Checks for mismatch between document type and file extension. "
        PopulateList(FileExtTypeMismatch,
                     "File extension type mismatch",
                     "FileExtTypeMismatch",
                     HelpString)

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
                     "Part copies out of date",
                     "InsertPartCopiesOutOfDate",
                     HelpString)

        Dim MaterialNotInMaterialTable As New L2A
        HelpString = "Checks the file's material against the material table. "
        HelpString += "The material table is chosen on the **Configuration Tab -- Templates Page**. "
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
                     RequiresSaveAsOutputDirectory:=True,
                     RequiresForegroundProcessing:=True)

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

        Dim UpdatePhysicalProperties As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(UpdatePhysicalProperties,
                     "Update physical properties",
                     "UpdatePhysicalProperties",
                      HelpString,
                      RequiresSave:=True)

        Dim VariablesEdit As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(VariablesEdit,
                     "Variables add/edit/expose",
                     "VariablesEdit",
                     HelpString,
                     RequiresVariablesToEdit:=True)

        Dim CopyOverallSizeToVariableTable As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(CopyOverallSizeToVariableTable,
                     "Copy overall size to variable table",
                     "CopyOverallSizeToVariableTable",
                     HelpString,
                     RequiresOverallSizeVariables:=True)

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
                     "Update part copies",
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
        
        Dim FileExtTypeMismatch As New L2A
        HelpString = "Checks for mismatch between document type and file extension. "
        PopulateList(FileExtTypeMismatch,
                     "File extension type mismatch",
                     "FileExtTypeMismatch",
                     HelpString)

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
                     "Part copies out of date",
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
                     RequiresSaveAsOutputDirectory:=True,
                     RequiresForegroundProcessing:=True)

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

        Dim PropertyFindReplace As New L2A
        HelpString = "Same as the Assembly command of the same name."
        PopulateList(PropertyFindReplace,
                     "Property find replace",
                     "PropertyFindReplace",
                     HelpString,
                     RequiresFindReplaceFields:=True, RequiresSave:=True)

        Dim UpdateDrawingViews As New L2A
        HelpString = "Checks drawing views one by one, and updates them if needed."
        PopulateList(UpdateDrawingViews,
                     "Update drawing views",
                     "UpdateDrawingViews",
                     HelpString,
                     RequiresSave:=True)


        Dim UpdateStylesFromTemplate As New L2A
        HelpString = "Updates styles and background sheets from a template you specify on the **Configuration Tab -- Templates Page**. "
        HelpString += vbCrLf + vbCrLf
        HelpString += "These styles are processed: DimensionStyles, DrawingViewStyles, LinearStyles, TableStyles, TextCharStyles, TextStyles. "
        HelpString += "These are not: FillStyles, HatchPatternStyles, SmartFrame2dStyles. "
        HelpString += "The latter group encountered errors with the current implementation.  The errors were not thoroughly investigated. "
        HelpString += "If you need one or more of those styles updated, please ask on the Forum. "
        PopulateList(UpdateStylesFromTemplate,
                     "Update styles from template",
                     "UpdateStylesFromTemplate",
                     HelpString,
                     RequiresTemplate:=True, RequiresSave:=True)

        Dim UpdateDrawingBorderFromTemplate As New L2A
        HelpString = "Replaces the background border with that of the Draft template specified on "
        HelpString += "the **Configuration Tab -- Templates Page**."
        HelpString += vbCrLf + vbCrLf + "In contrast to `UpdateStylesFromTemplate`, this command only replaces the border. "
        HelpString += "It does not attempt to update styles or anything else."
        PopulateList(UpdateDrawingBorderFromTemplate,
                     "Update drawing border from template",
                     "UpdateDrawingBorderFromTemplate",
                     HelpString,
                     RequiresTemplate:=True, RequiresSave:=True)

        Dim DrawingViewOnBackgroundSheet As New L2A
        HelpString = "Checks background sheets for the presence of drawing views."
        PopulateList(DrawingViewOnBackgroundSheet,
                     "Drawing view on background sheet",
                      "DrawingViewOnBackgroundSheet",
                      HelpString)


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
        HelpString = "Print settings are accessed on the **Configuration Tab -- Printing Page**."
        HelpString += vbCrLf + vbCrLf + "![Printer_Setup](My%20Project/media/printer_setup.png)"
        HelpString += vbCrLf + vbCrLf + "The dropdown should list all installed printers. "
        HelpString += "You can configure up to two of them, `Printer1` and `Printer2`. "
        HelpString += "`Printer1` is the default.  It prints everything not assigned to `Printer2`. "
        HelpString += vbCrLf + vbCrLf + "`Printer2` prints any sheet on the drawing whose size is listed in the Sheet selection textbox. "
        HelpString += "Click the `Set` button to select the sheet sizes. "
        HelpString += vbCrLf + vbCrLf + "Enable/disable a printer using the checkbox next to its name. "
        HelpString += "If you need to print only certain sizes of drawings, you can disable `Printer1` "
        HelpString += "and enable `Printer2` with the desired sheet sizes set. "
        HelpString += vbCrLf + vbCrLf + "This command may not work with PDF printers. "
        HelpString += "Try the Save As PDF command instead. "
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
        HelpString += vbCrLf + vbCrLf + "When creating PDF files, there are two options, `PDF` and `PDF per Sheet`. "
        HelpString += "The first saves all sheets to one file.  The second saves each sheet to a separate file, "
        HelpString += "called `<Filename>-<Sheetname>.pdf`.  You can optionally suppress the `Sheetname` suffix"
        HelpString += "on file with only one sheet.  Set the option on the **Configuration Tab -- Open/Save Page**."
        HelpString += "To save sheets to separate `dxf` or `dwg` files, refer to the Save As Options in Solid Edge. "
        PopulateList(SaveAs,
                     "Save As",
                     "SaveAs",
                     HelpString,
                     RequiresSaveAsOutputDirectory:=True,
                     RequiresForegroundProcessing:=True)

    End Sub


End Class
