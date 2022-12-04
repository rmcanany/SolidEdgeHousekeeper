# Release Notes

Solid Edge Housekeeper is a utility for finding annoying little errors in your project.  It is free and open source and you can find it here:

https://github.com/rmcanany/SolidEdgeHousekeeper  (Scroll past the file list for the README)

Please note, the program has been tested on many of our files, but none of yours.  Do not run it on production work without testing on backups first.

Feel free to report bugs and/or ideas for improvement on the Solid Edge Community Forum.  https://community.sw.siemens.com/s/topic/0TO4O000000MihiWAC/solid-edge



## v0.1.10.3 Enhancements/Fixes

### Save as

Fixed an issue with `Use subdirectory formula` when the file contains properties with no assigned name (Thank you @n0minus38).  Applied the same fix to `Property find/replace`.

Fixed another issue with `Use subdirectory formula` when the formula contains illegal characters for the file system.  (Thank you again @n0minus38).  See details on the Readme Tab, `Assembly Save As` section.

### README

In the GETTING HELP section, added instructions on how to subscribe to update notifications and/or become a beta tester.

In the INSTALLATION section, added instructions on how to copy settings files from an earlier release to a new one.


## v0.1.10 Enhancements/Fixes

### Run external program 

Added the ability to run VBScript files (Thank you @Derek G).  These are simple text files that do not require the user to install/learn Visual Studio.

Added an option to automatically save the file after the macro runs (Thank you @Chris42).  This opens up the use of `Run external program` to many macros that were not designed with Housekeeper in mind.

Added new sample code to the HousekeeperExternalPrograms repo.  https://github.com/rmcanany/HousekeeperExternalPrograms

-- BreakExcelLinks and AssemblyReport shows two examples of VBScript code (Thank you @Derek G, @wku).

-- FitIsoView shows how to parse Housekeeper's `defaults.txt` file.


### Disable graphics display  

Added an option to not display graphics during processing (Thank you @Jason1607436093479).  Speed up is approximately 2X.  One possible drawback is you don't look nearly as busy when your boss walks up and sees you playing on your phone.


### Expose variables & Expose variable missing 

Added the ability to 'expose' entries in the variable table (Thank you @MonkTheOCDEngie).  Also added a check for variable presence.  The variables are entered in a comma-delimited list on each task tab, e.g., `Flat_Pattern_Model_CutSizeX, Flat_Pattern_Model_CutSizeY, Mass`.


### Parts list missing

Added a task to check for the presence of a parts list in a Draft file.


### Save as

Added an output directory option, `Use subdirectory formula` (Thank you @SeanCresswell).  The formula is a combination of free text and variable names.  The variable name format is similar to Property Text in a Callout, for example `LASER_%{System.Material}_%{Custom.Material Thickness}`.  For more information, in particular for Draft files, see the Readme for `Save As`.

Added `.png` as an output format.  Since Solid Edge does not support it (as of SE2021), the file is first saved as a `.jpg`, then converted.  The converter is the open source image utility `ffmpeg`, which is included with Housekeeper.  You can read more about it here: https://ffmpeg.org/about.html

Added an option, `Save as image -- Crop to model size`.  The output is saved with the aspect ratio of the model, rather than the window.  The option is found on the Configuration tab.

Combined the three versions of `Save As Sheetmetal` into one.  The previous functionality is covered by two new file output options, `DXF Flat (*.dxf)` and `PDF Drawing (*.pdf)`.



### Update styles from template 

Renamed from the confusing `Move drawing to new template`.  Note, the name has changed, but the program still moves everything from the old file to a new one created using the template specified on the Configuration tab.

Fixed an issue where dimensions were copied to the working sheet from section and detail views (Thank you @Bob Henry, @MonkTheOCDEngie).

Fixed an issue where compound weld symbols did not transfer correctly (Thank you @Bob Henry).


### Property filter

Added `>` and `<` to the comparison choices.  These are mainly intended for dates and numbers (see next), but can be used for any property.

Enhanced the handling of dates and numbers by attempting to convert them into their native formats.  Previously they were treated as text.  Note this is somewhat experimental.  If you find something that doesn't work correctly, please speak up.

Dates are converted to the format `YYYYMMDD`.  That is the format for the `Value` that must be used on the Property Filter dialog.  

Many numbers, notably those exposed from the variable table, include units.  The program needs to strip them off before comparing.  Currently only distance and mass units (`in`, `mm`, `lbm`, `kg`) are handled.  Please ask if you need others.


### Error log display 

If errors occur during processing, the log file is automatically opened in Notepad (Thank you @Martin Bernhard).  Previous behavior was to simply notify the user of its location.  The file is still saved to the input directory.


### File wildcard search

Changed from a text box to a combo box.  Keeps the user from having to remember the sometimes arcane wildcard syntax.  Added the ability to delete entries that are no longer needed.


### Task list input options

Each input option is now disabled unless a selected task requires it.  This makes it easier to see what options need the user's attention.


### Readme

The first time Housekeeper is launched, focus is given to the Readme tab.  

Added a link to the Solid Edge Community Forum for user feedback.

Added a better explanation of the TODO list and its use.


### Top level assembly search option

Fixed an issue where Top down search crashes if `FastSearchScope.txt` is blank (Thank you @JayJay101).


### Underconstrained profiles

Fixed an issue where Sync Used Sketches were reported as underconstrained.


### Interactive Edit

Not saving the file, or cancelling processing, is no longer reported as an error in the log file.






## v0.1.9 Enhancements/Fixes

### Update drawing border from template

Added the ability to do a simple drawing border replacement.

Unlike the similar task 'Move drawing to new template', this only replaces the background sheet.  It does not attempt to update styles or anything else.

### Update design for cost

Added the ability to update Sheetmetal Design For Cost properties (Thank you @MonkTheOCD_Engie).  

An annoyance of this command is that it needs to open the DesignForCost Edgebar pane, but is not able to close it.  The user must manually close the pane in a subsequent Sheetmetal session.  The state of the pane is system-wide, not per-document, so closing it is a one-time action.  

### Fit pictorial view

Added options for dimetric and trimetric views.  Previously only isometric was supported.  The option is set on the Configuration Tab.

Moved the 'Hide constructions' functionality to its own task.  Previously it was part of the 'Fit iso view' task.

Fixed an issue where some construction elements were not hidden correctly in assemblies (Thank you @Fiorini).

### Save As

For model files, added bitmap image output formats.  

For draft files, added an optional watermark.  It can be positioned and scaled as desired.  The original file is unchanged.

### Update material from material table

The log file now reports which material properties were updated.  Previous behavior was to simply state that an update had occurred.

Disabled checking the seVSPlusStyle property.

### Update face and view styles from template

Fixed an issue where existing view styles were sometimes incorrectly marked 'in use' and could not be changed.

Fixed an issue where the Color Manager base styles were not updated.

### File names

Fixed an issue where a tilde '~' in the file name caused an error.  

Added a check for path names exceeding the maximum length.

### README font size

Changed the font to match that of the file list box.  Previously it was set to the system default.


## v0.1.8 Enhancements/Fixes

### Open/Save

Added a task to open a file and save it in the current version.

### Top Level Assembly search

Added detection of indexed drives for bottom-up searches (Thank you @Jean-Louis).  Requires a valid Fast Search Scope filename.  See Readme for details.

Added a filter to avoid processing Family of Parts master and unrelated child documents.

### Print Task

Added the ability to override several Windows printing defaults (Thank you @n0minus38).  

Added controls for Solid Edge-specific printing options.

Fixed an issue where the print options were not saved between sessions.  

### Known Issues

Added user feedback on Teamcenter managed files (Thank you @[mike miller]).  

Added an entry on some printing limitations and workarounds.


## v0.1.7 Enhancements/Fixes

### Run External Program

Added the ability to launch a Console App from within Housekeeper.  The idea is to be able to customize tasks to your site-specific requirements.  

The Console App works on a single Solid Edge file.  Housekeeper serves up files one at a time for processing.  It handles the batching, filtering, error reporting, etc.  

Several rules on the program implementation apply.  See details and examples at https://github.com/rmcanany/HousekeeperExternalPrograms

If you could use this, but are not the programming type, your VAR might be willing to help.  If not, you could try waving around a little bit of money on this forum.  It may well bring someone to the rescue.

### Interactive Edit

Added the ability to bring up files one at a time for manual editing.  An always-on-top dialog box lets the user signal when they are finished and ready for the next file.  The main idea is to allow the user to fix errors, such as missing flat patterns, that are not easily accomplished automatically.  Some rules apply.  See the README tab for details.

### TODO List

Added the ability to create a list of files in which errors were detected.  This list can be used as a file search option on subsequent runs.  It is intended mainly to work as a preprocessor for Interactive Edit, but can be used with any task.

### Wildcard Property Comparison

Added the ability to search properties with a wildcard pattern.  It is implemented with the VB `Like` operator, which is similar to the old DOS wildcard search, but with a few more options.  For details and examples, see https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator.

### File Filters

Added the ability to narrow the file listing based on file type.  Also added a wildcard search option.

### Missing Drawing

Added a task to find missing drawings for model files.  A current limitation is that the drawing name and directory must both be the same as the model file.

### Property Find/Replace

Added a task to edit property values.  It searches for text in the defined property and replaces it if found.  

The search is not case sensitive, the replacement is.  For example, say the search is `aluminum`, the replacement is `Aluminum`, and the property value is `ALUMINUM 6061-T6`.  The new value will be `Aluminum 6061-T6`.

### Save As

Made Save As more generic by having the user specify the file type from a ComboBox.  Previously, each file type had its own task.

### Save As Flat DXF

Added the ability to save a sheetmetal flat pattern as a DXF file.  Previously, this function was combined with the task 'Save Laser DXF and PDF'.

### Print

Added the ability to print draft files.  

### File List Out Of Date Issue

Fixed an issue where the file list did not update correctly under certain conditions.  Also added a pre-process check to verify that all files on the list still exist on disk.



## v0.1.6 Enhancements/Fixes

### Top Level Assembly

Added a top-level assembly file search option (Thank you @Jean-Louis).  The search can be conducted in one of two ways, bottom up or top down.  

Bottom up is meant for general purpose directories (e.g., `\\BIG_SERVER\huge list of files\`).  

Top down is meant for self-contained project directories (e.g., `C:\Projects\Project123\`).  A top down search can optionally report files with no links to the top level assembly.

See the README tab for more information.

### Property Filter

Added a property filter search.  You identify a condition consisting of a property, a comparison, and a value (e.g., `Material contains Steel`).  Only those files matching the condition are processed.

System or custom properties can be searched.  Multiple conditions can be included.  Filters can optionally be named, saved, modified, or deleted.

Each condition is assigned a variable name, (`A`, `B`, ...).  The default filter formula is to match all conditions (e.g., `A AND B AND C`).  You can optionally change the formula (e.g., `A AND (B OR C)`).

See the Property Filter README tab for more information.

### Help Text

Added extra help text on the README tab for tasks where the label did not fully explain the action (Thank you @bshand).

### Known Limitations

Added a section to the README, listing known issues and possible workarounds.  

### File Select Set

Improved the behavior of the file list select set.  Previously, the select set was cleared whenever a task option was changed.  Now the select set is preserved whenever possible.

### Save Task Selections Now Optional

Previous behavior was to always save.  All other information, such as input directory and template locations, is still always saved.  

### Allow Partial Success on 'Move drawing to new template' Now Optional

Previous behavior was to always allow.

### Readme Scroll Issue

Fixed an issue where the README tab did not scroll correctly.

### Broken Out Section Views Issue

Fixed an issue where `Move drawing to new template` failed on broken-out section views.



## v0.1.5 Enhancements/Fixes

### Move Drawing to New Template

This new task replaces `Update drawing border from template` and `Update dimension styles from template`. It is an improvement because any Styles you modify in your template (even ones I don't know about) will be present in the updated Draft file.

### Save As STEP

Added for Assembly, Part and SheetMetal files.

### Save As DXF

Added for Draft files

### File List

Changed file listbox to multi-column and improved the sort order.

### Widget Positioning

Fixed an issue where form controls were not postioned correctly when the main form was resized.
