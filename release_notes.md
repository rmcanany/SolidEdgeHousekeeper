![Logo](My%20Project/media/logo.png)
# Release Notes

Solid Edge Housekeeper is a utility for finding annoying little errors in 
your project.  It is free and open source and you can find it 
[**Here**](https://github.com/rmcanany/SolidEdgeHousekeeper#readme).

Please note, the program has been tested on many of our files, but none 
of yours.  Do not run it on production work without testing on backups first.

Feel free to report bugs and/or ideas for improvement on the 
[**Solid Edge Forum**](https://community.sw.siemens.com/s/topic/0TO4O000000MihiWAC/solid-edge)

## V2024.1 Enhancements/Fixes

### Compare Model and Flat Pattern Model Volumes

Contributed by our very own **o_o ....码**.  Thank you!

A Housekeeper External Program that computes the difference in volume of
a bent sheetmetal part and its flat pattern.

The program addresses an issue where a flat pattern
is created in the Synchronous environment.  If Ordered
features are then added, they are not carried over to the flat pattern.
Compounding the problem, even though it is
out-of-date, the flat pattern is not flagged as such.

### Property Find/Replace

(Thank you **@Francesco Arfilli**!)

Added an option to create the property if it doesn't exist.

### Most Recently Used File List (MRU)

(Thank you **@Francesco Arfilli**!)

Added an option to not add files processed by Housekeeper to the MRU.

### Save As

(Thank you **@roger.ribamatic**, **@SatyenB**,
**@Robin BIoemberg**!)

Added a new file type, `PDF per Sheet` for drawings.  The output file name
is of the format `<Filename>-<Sheetname>.pdf`.
There is an option to suppress the `Sheetname` suffix
on drawings with only one sheet.

Added another new file type `*.jt` for model files.

### Draft -- Add Quantity Property

(Thank you **@Pedro0996**!)

A Housekeeper External Program that gets the total quantity 
of each part and subassembly in a given assembly
and adds that information to the part (or subassembly) file. 
In addition to the quantity, the program also records the assembly name 
from which the quantity was derived. Both are added as custom properties, 
making them available, for example, in a Callout in Draft. 

There are good reasons not to do this.
The quantity is a static (non-associative) value, as is the
source assembly name.  They can go stale without warning.
However, several people, including me, wanted this, 
so it got in.

### File List

(Thank you **@jnewell**, **@Robin BIoemberg**!)

Added the ability to process files regardless of Document Status.
After processing, the file can be reverted back to
the old value, or changed to a new one.  These options
are set on the **Configuration Tab -- Open/Save Page**.

![Open/Save](My%20Project/media/file_open_save_options.png)

Fixed an issue where changing the Property Filter did not
always set the out-of-date flag correctly.

### Update Drawing Views

Fixed an issue where family of assembly drawings
could not find the assembly file.  Fixed the same
issue on the Broken Links Task.

### Housekeeper External Program AddRemoveCustomProperties

Added an option to remove all properties *except* those
listed in the program settings file.

### Property Filter

Added an option to not check for properties of the models
contained in Draft files.  Previous behavior was to
always check.  Set the option on 
the **Configuration Tab -- General Page**.
Since Draft files often do not have properties of their own,
normally this option should be enabled.
Searching for Document Status is another story.  For example,
with the option set, an `In Work` drawing of a `Released` part would
confusingly show up in a search for `Released` files.

Added a section in the README on searching for 
Document Status.  (Hint, you have to use a number, not a name.)
See the 
[**Property Filter**](https://github.com/rmcanany/SolidEdgeHousekeeper#1-property-filter) 
section, Document Status topic.

Fixed an issue where properties of Draft files themselves were
sometimes not searched.

### Configuration Tab

(Thank you **@Francesco Arfilli**!)

Converted to a tab-page layout for easier navigation. 

![Configuration](My%20Project/media/top_level_assy_options.png)

### Draft Print

(Thank you **@n0minus38**!)

Added an optional second printer for selected sheet sizes. 

![Configuration](My%20Project/media/printer_setup.png)

Changed how a printer is selected.  It is now
from a pre-populated list of installed printers. 
Previously it was from the Windows Print dialog.  This was 
confusing because clicking OK implied a document would be
printed.

Removed some other confusing control options.

### Preferences Folder

Added a new location for user data.  It is now stored
in Preferences in the Housekeeper root directory.
Previously it was stored in the same folder as the
executable and associated files, making it hard to
identify user-specific files.

### Expose Variables

Added checking for the presence of a variable and
removed the task `Expose variables missing`.


## V2023.6 Enhancements/Fixes

### Process Tasks in Background -- Increased speed

Contributed by **@Fiorini** 
(GitHub: [**@farfilli**](https://github.com/farfilli)).  Thank you!

Uses the SolidEdgeCommunity OpenInBackground function 
(Thank you **@JNewell**!).  Speedup varies -- from 3X to 10X 
depending on the task.  The option is set on the
**Configuration Tab**.

Some tasks (`Fit view`, `Interactive edit`, and `Save As`) are 
incompatible with background processing.  The program checks
 and notifies the user of a conflict.  

At this time, assemblies still run in the old mode -- 
graphics hidden, not disabled.  

### Sort Order -- Random sampling

Added an option to randomly select and shuffle a fraction of the total files 
found.  The `Sample fraction` is a decimal number between `0.0` and `1.0`.  Sort 
options are set on the **Configuration Tab**.

![Logo](My%20Project/media/file_sort_options.png)

This option is primarily intended for software testing, 
but can be used for any purpose.

### Dependency-order Sort

Fixed an issue, in top-level search mode, where the top level 
assembly itself was not sorted correctly.

### Bottom Up Search 

Fixed an issue where bottom up search was not returning 
Family of Assemblies links.  


## V2023.5 Enhancements/Fixes

### Update Draft Styles from Template

Contributed by **@Fiorini** (GitHub: [**@farfilli**](https://github.com/farfilli)).  Thank you!

Now does an in-place update, rather than creating a new file.  

The new method preserves the file creation date, which was requested by 
several users.  It also reports styles and background 
sheets not updated because they were not found in the template.

These styles are processed: DimensionStyles, DrawingViewStyles, 
LinearStyles, TableStyles, TextCharStyles, TextStyles.  

These are not: DashStyles, FillStyles, HatchPatternStyles, 
SmartFrame2dStyles.  

The latter group encountered errors in testing.  However, the cause
was not thoroughly investigated.  If you need them updated, 
please ask on the Forum. 

Note, the previous method (the one that creates a new file) is not 
exposed in the user interface, but has not been removed from the 
code.   If you prefer that version, please ask on the Forum.

### File List Sorting

Added sorting options of `None`, `Alphabetic`, and 
`Dependency` (Thank you **@HIL**). Previously
the list was always sorted alphabetically.

![File list sorting options](My%20Project/media/file_sort_options.png)

The `Dependency` order is intended to help eliminate
the annoying dark gray corners on drawings.  The `None` option is primarily 
intended to preserve the order on imported lists.

These options are set on the **Configuration Tab -- File List Group**.

### Update Part Copies

Added an option to recursively update part copies.  It is yet
another technique to help eliminate gray corners on drawings.

Recursion means
that if a Part Copy is encountered, the parent file is opened.
If *that* file has a Part Copy, its parent is opened in turn.
The program continues in this fashion until the base file
(ie one with no Part Copies) is reached.
It then updates each file in reverse order.

The option is set on the **Configuration Tab -- Miscellaneous Group**.

### Property Find/Replace

Added this functionality for Draft files (Thank you **@Seva**).

Includes wildcard search, Regex processing, and Property formula
substitution.
For details, see the Help topic under Assembly Tasks: Property
Find/Replace.

![Draft Property Find/Replace](My%20Project/media/copy_model_properties_to_draft.png)

The example shown above would populate the Draft file's Title with the
Title of the model file.

### Drawing View on Background Sheet

Checks all background sheets of a Draft file for the presence
of a drawing view.

### Top Level Assembly Search

Fixed an issue where a top-level search with no top-level folders
resulted in no files being found (Thank you **@MonkTheOCD_Engie**)

Added the ability to report files unrelated to a top level
assembly using a Bottom-Up search.  Previously only Top-Down
search had this option.

### File Drag and Drop

Fixed an issue where files added by drag-and-drop were
deleted from the list when running the `Update File List`
command (Thank you **@HIL**).

### Save As

Added `Save Copy As` for all file types.  In the ComboBox, select
the `Copy` option.  If you try to save a copy to the original folder,
you will get an error.

## V2023.3 Enhancements/Fixes

### Property Find/Replace

Added Wildcard and Regular Expression search options 
(Thank you **@DaveG, @mmtrebuchet**).  Also added 
Property Formula substitution (Thank you **@64Pacific**, **@ben.steele6044**).  

![Find_Replace](My%20Project/media/property_find_replace.png)

The example above would replace anything (including nothing) 
in Custom.Engineer with "Superman".  The checkboxes set the 
search type, either Plain Text (PT), Wildcard (WC), or Regular Expression (RX).

Regular Expressions are more powerful than wildcards.  They 
are also notoriously cryptic.  To get started, check out the 
[**.NET Regex Guide**](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference).

![Property Formula](My%20Project/media/property_formula.png)

The Property Formula has the same syntax as the Callout command, except 
preceeded with 'System.' or 'Custom.' as shown.  This example 
would replace anything in Custom.Description with a profile 
definition and its cut length.  (Assuming they are present in the file.)

### Property Filter

Added a regular expression comparison operator, `regex_match`.  

### Top Level Assembly Search

Added a search option for Draft files with the same name as the 
model (Thank you for input **@n0minus38, @KennyG, @Alex_H, @bshand,**
**@Nosybottle, @wku, @nate.arinta5649**).  In a test on a 
small top level assembly and a top level folder with 20k files, 
the new option completed a search in 0.3 minutes.  A top-down 
search took 11.3 minutes.  A bottom-up search (on a non-indexed 
drive) was terminated after an hour.

Fixed an issue where a Top Level search with multiple folders was 
not finding all related files (Thank you **@MonkTheOCD_Engie**).

Added a **Configuration Tab** option `Include part copies in search results`.  Disabling
the option can eliminate extraneous files from being reported in some 
cases.  (Thank you **@n0minus38**)

### File List Out Of Date

Added a flag to detect when the file list goes out of date.
It is set when any change occurs to the file selection or filter inputs.
As a visual indicator, the Update Button turns orange.

Added a check to ensure the file list is
up to date before processing can begin.

### Hide Constructions

Fixed an issue where sketches, and PMI dimensions and annotations 
were not hidden (Thank you **@tempod** (github)).

Fixed an issue where etches were being hidden.

### Update Insert Part Copies

Added PMI Update all.  Fixed an issue where PMI Update All was causing
the flat pattern to become out of date.

### Home Tab file count

Fixed an issue where the file count was incorrect for a multi-folder search.  


## V2023.1 Enhancements/Fixes

### Redesigned Home Tab

Contributed by **@Fiorini** (GitHub: [**@farfilli**](https://github.com/farfilli)).  Thank you!

The new interface is much more flexible, organized, and colorful 
than ever!  Generate your list of files from folders, sub folders, 
top-level assemblies and selected folders (Thank you **@MONKTheOCDEngie**!), 
lists, drag-and-drop, and more. 
Pick any number of each, in any combination.

If you select one or more files in the list, there's now a Shortcut Menu 
that lets you open the files in Solid Edge, view them in File Explorer, 
start processing them in Housekeeper, or exclude them from the list.

You'll also notice new icons throughout.  These are taken from standard Windows and
Solid Edge libraries and you'll recognize them instantly.  If there's one
you don't know, a tooltip pops up to tell you what it does.

**Home Tab**

![Home Tab](My%20Project/media/home_tab_done.png)

**Selection toolbar**

![Select Toolbar](My%20Project/media/selection_toolbar.png)

**Filter toolbar**

![Select Toolbar](My%20Project/media/filter_toolbar.png)

**Shortcut menu**

![Select Toolbar](My%20Project/media/shortcut_menu.png)

### Logo

Contributed by **@daysanduski**.  Thank you!

It's at the very top of this page.  Pretty fun, huh?

### Save As

Contributed by **@Fiorini** (GitHub: [**@farfilli**](https://github.com/farfilli)).  Thank you!

Eliminate the dependency on the external program `ffmpeg.exe` for image files.

### Run external program 

Changed error handling to be more consistent with Housekeeper.

Added a new sample program to the 
[**HousekeeperExternalPrograms**](https://github.com/rmcanany/HousekeeperExternalPrograms)
repo.  

The program, `ChangeToInchAndSaveAsFlatDXF.vbs`, 
is a more complete VBScript example. 

It shows how to return an `ExitCode`,
handle `error messages` and parse the `defaults.txt` file. 
It sets off space for user code and shows how to do error handling. 
Practically every line has comments describing what it does.

At the bottom of the file are helpful links to get you started
writing your own VBScript files, using nothing more than a text editor! 
(Thank you again **@[Derek G]**!)

### Top-Level Assembly

Improved file link traversal performance in Bottom Up mode
approx 10X (Thank you **@Fiorini**).
Unfortunately, *Where Used* performance is not affected.


### Version numbers

Changed to a year-based numbering scheme (Thank you **@bshand**!).

Updated the program title bar so you
can easily check which version you are running.

![Table of Contents](My%20Project/media/tabs.png)

### Help Tab

Renamed from Readme to 
[**Help**](https://github.com/rmcanany/SolidEdgeHousekeeper#readme). 
Now hosted on GitHub, it features a more legible (and bigger) font 
and lots of pictures (Thank you **@Fiorini**!). 

For quick navigation, it also features a built in Table of Contents. 
(It's the icon next to **README.md** shown in the image below.)

![Table of Contents](My%20Project/media/table_of_contents.png)

Also updated and expanded the File Selection and Filtering Sections. 
Added sections on Collaborators, User Feedback, and Helping Out. 

### Beta Program

Implemented a way for volunteers to contribute to Housekeeper 
by testing the program before an upcoming release. 

For this round, we had four volunteers:
@n0minus38, @JayJay101, @Cimarian_RMP, and @xenia.turon.
Thank you one and all!

Because of their work, we made fifteen new commits to the master branch,
fixed six bugs, improved the documentation, 
and tested two release candidates. 

You're welcome to join us for the next release! 
Beta testing is nothing more than conducting 
your own workflow on your own files and telling me if you run into problems. 
It isn't meant to be a lot of work. 
The big idea is to make the program better for you and me and everyone else!

To sign up message me, RobertMcAnany, on the 
[**Solid Edge Forum**](https://community.sw.siemens.com/s/topic/0TO4O000000MihiWAC/solid-edge).

### ToyProject

Created a repo called [**ToyProject**](https://github.com/rmcanany/ToyProject) 
for anyone who wants to get started collaborating on Housekeeper. It has links to 
GitHub's own guides, as well as detailed instructions on how to make your first
contribution!

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
