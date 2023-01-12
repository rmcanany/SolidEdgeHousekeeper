# Solid Edge Housekeeper v0.1.10.3
Robert McAnany 2022

Portions adapted from code by Jason Newell, Greg Chasteen, Tushar Suradkar, and others.  Most of the rest copied verbatim from Jason's repo and Tushar's blog.

Helpful feedback and bug reports: @Satyen, @n0minus38, @wku, @aredderson, @bshand, @TeeVar, @SeanCresswell, @Jean-Louis, @Jan_Bos, @MonkTheOCD_Engie, @[mike miller], @Fiorini, @[Martin Bernhard], @Derek G, @Chris42, @Jason1607436093479, @Bob Henry, @JayJay101

## DESCRIPTION
This tool is designed to help you find annoying little errors in your project.  It can identify failed features in 3D models, detached dimensions in drawings, missing parts in assemblies, and more.  It can also update certain individual file settings to match those in a template you specify.

## GETTING HELP
Ask questions or suggest improvements on the Solid Edge Forum: 
https://community.sw.siemens.com/s/topic/0TO4O000000MihiWAC/solid-edge

To subscribe to update notices or volunteer to be a beta tester, message me, RobertMcAnany, on the forum.  (Click your profile picture, then 'My Messages', then 'Create').  Unsubscribe the same way.  To combat bots and spam, I will probably ignore requests from 'User16612341234...'.  (Change your nickname by clicking your profile picture, then 'My Profile', then 'Edit').  

## INSTALLATION
There is no installation per se.  The preferred method is to download or clone the project and compile it yourself.

The other option is to use the latest released version here https://github.com/rmcanany/SolidEdgeHousekeeper/releases  Under the latest release, click the SolidEdgeHousekeeper-vx.x.x.zip file (sometimes hidden under the Assets dropdown).  It should prompt you to save it.  Choose a convenient location on your machine.  Extract the zip file (probably by right-clicking and selecting Extract All).  Double-click the .exe file to run.

If you are upgrading from a previous release, you should be able to copy the settings files from the old version to the new.   The files are 'defaults.txt', 'property_filters.txt', and 'filename_charmap.txt'.  If you haven't used Property Filter, 'property_filters.txt' won't be there.  Versions prior to 0.1.10 won't have 'filename_charmap.txt' either.  

## OPERATION
On each file type's tab, select which errors to detect.  On the General tab, browse to the desired input folder, then select the desired file search option.  

You can refine the search using a file filter, a property filter, or both.  See the file selection section for details.  

If any errors are found, a log file will be written to the input folder.  It will identify each error and the file in which it occurred.  When processing is complete, the log file is opened in Notepad for review.

The first time you use the program, some site-specific information is needed.  This includes the location of your templates, material table, etc.  These are populated on the Configuration Tab.

You can interrupt the program before it finishes.  While processing, the Cancel button changes to a Stop button.  Just click that to halt processing.  It may take several seconds to register the request.  It doesn't hurt to click it a couple of times.

## CAVEATS
Since the program can process a large number of files in a short amount of time, it can be very taxing on Solid Edge.  To maintain a clean environment, the program restarts Solid Edge periodically.  This is by design and does not necessarily indicate a problem.

However, problems can arise.  Those cases will be reported in the log file with the message 'Error processing file'.  A stack trace will be included.  The stack trace looks scary, but may be useful for program debugging.  If four of these errors are detected in a run, the programs halts with the Status Bar message 'Processing aborted'.

Please note this is not a perfect program.  It is not guaranteed not to mess up your files.  Back up any files before using it.

## KNOWN ISSUES
Does not support managed files.  Cause: Unknown.  Possible workaround: Process the files in an unmanaged workspace.   
Update 10/10/2021: Some users have reported success with BiDM managed files.  
Update 1/25/2022: One user has reported success with Teamcenter 'cached' files.  

Some tasks may not support versions of Solid Edge prior to SE2020.  Cause: Maybe an API call not available in previous versions.  Possible workaround: Use SE2020 or later.  

May not support multiple installed Solid Edge versions on the same machine.  Cause: Unknown.  Possible workaround: Use the version that was 'silently' installed.  

Does not support all printer settings, e.g., duplexing, collating, etc.  Cause: Not exposed in the DraftPrintUtility() API.  Possible workaround: Create a new Windows printer with the desired settings.  Refer to the TESTS AND ACTIONS topic below for more details.  

Pathfinder is sometimes blank when running the 'Interactive Edit' task.  Cause: Unknown.  Possible workaround: Refresh the screen by minimizing and maximizing the Solid Edge window.  


## DETAILS

### TESTS AND ACTIONS
### ASSEMBLY
    Open/Save
    Activate and update all
    Property find replace
    Expose variables missing
    Expose variables
    Remove face style overrides
    Update face and view styles from template
    Hide constructions
    Fit pictorial view
    Part number does not match file name
    Missing drawing
    Broken links
    Links outside input directory
    Failed relationships
    Underconstrained relationships
    Run external program
    Interactive edit
    Save as
### PART
    Open/Save
    Property find replace
    Expose variables missing
    Expose variables
    Update face and view styles from template
    Update material from material table
    Hide constructions
    Fit pictorial view
    Update insert part copies
    Broken links
    Part number does not match file name
    Missing drawing
    Failed or warned features
    Suppressed or rolled back features
    Underconstrained profiles
    Insert part copies out of date
    Material not in material table
    Run external program
    Interactive edit
    Save As
### SHEETMETAL
    Open/Save
    Property find replace
    Expose variables missing
    Expose variables
    Update face and view styles from template
    Update material from material table
    Hide constructions
    Fit pictorial view
    Update insert part copies
    Update design for cost
    Broken links
    Part number does not match file name
    Missing drawing
    Failed or warned features
    Suppressed or rolled back features
    Underconstrained profiles
    Insert part copies out of date
    Flat pattern missing or out of date
    Material not in material table
    Run external program
    Interactive edit
    Save As
### DRAFT
    Open/Save
    Update drawing views
    Update styles from template
    Update drawing border from template
    Fit view
    File name does not match model file name
    Broken links
    Drawing views out of date
    Detached dimensions or annotations
    Parts list missing or out of date
    Run external program
    Interactive edit
    Print
    Save As

## CODE ORGANIZATION
Processing starts in Form1.vb.  A short description of the code's organization can be found there.

<!-- Start -->

### Assembly

#### Open/Save
Open a document and save in the current version.

#### Activate and update all
Loads all assembly occurrences' geometry into memory and does an update. Used mainly to eliminate the gray corners on assembly drawings. 

Can run out of memory for very large assemblies.

#### Property find replace
Searches for text in a specified property and replaces it if found. The property, search text, and replacement text are entered on the **Task Tab**, below the task list. 

A `Property set`, either `System` or `Custom`, is required. For more information, see the **Property Filter** section above. 

The search *is not* case sensitive, the replacement *is*. For example, say the search is `aluminum`, the replacement is `ALUMINUM`, and the property value is `Aluminum 6061-T6`. Then the new value would be `ALUMINUM 6061-T6`. 

#### Expose variables missing
Checks to see if all the variables listed in `Variables to expose` are present in the document.

#### Expose variables
Exposes entries from the variable table, making them available as a Custom property. Enter the names as a comma-delimited list in the `Variables to expose` textbox. Optionally include a different Expose Name, set off by the colon `:` character. 

For example `var1, var2, var3`

Or `var1: Length, var2: Width, var3: Height`

Or a combination `var1: Length, var2, var3`

Note: You cannot use either a comma `,` or a colon `:` in the Expose Name. Actually there's nothing stopping you, but it will not do what you want. 

#### Remove face style overrides
Face style overrides change a part's appearance in the assembly. This command causes the part to appear the same in the part file and the assembly.

#### Update face and view styles from template
Updates the file with face and view styles from a file you specify on the **Configuration Tab**. 

Note, the view style must be a named style.  Overrides are ignored. To create a named style from an override, open the template in Solid Edge, activate the `View Overrides` dialog, and click `Save As`.

#### Hide constructions
Hides all non-model elements such as reference planes, PMI dimensions, etc.

#### Fit pictorial view
Maximizes the window, sets the view orientation, and does a fit. Select the desired orientation on the **Configuration Tab**.

#### Part number does not match file name
Checks if a file property, that you specify on the **Configuration Tab**, matches the file name.

#### Missing drawing
Assumes drawing has the same name as the model, and is in the same directory

#### Broken links
Checks to see if any assembly occurrence is pointing to a file not found on disk.

#### Links outside input directory
Checks to see if any assembly occurrence resides outside the top level directories specified on the **Home Tab**. 

#### Failed relationships
Checks if any assembly occurrences have conflicting or otherwise broken relationships.

#### Underconstrained relationships
Checks if any assembly occurrences have missing relationships.

#### Run external program
Runs an `\*.exe` or `\*.vbs` file.  Select the program with the `Browse` button. It is located on the **Task Tab** below the task list. 

If you are writing your own program, be aware several interoperability rules apply. See [**HousekeeperExternalPrograms**](https://github.com/rmcanany/HousekeeperExternalPrograms) for details and examples. 

#### Interactive edit
Brings up files one at a time for manual processing. A dialog box lets you tell Housekeeper when you are done. 

Some rules for interactive editing apply. It is important to leave Solid Edge in the state you found it when the file was opened. For example, if you open another file, such as a drawing, you need to close it. If you add or modify a feature, you need to click Finish. 

Also, do not Close the file or do a Save As on it. Housekeeper maintains a `reference` to the file. Those two commands cause the reference to be lost, resulting in an exception. 

#### Save as
Exports the file to a non-Solid Edge format. 

Select the file type using the `Save As` combobox. Select the directory using the `Browse` button, or check the `Original Directory` checkbox. These controls are on the **Task Tab** below the task list. 

Images can be saved with the aspect ratio of the model, rather than the window. The option is called `Save as image -- crop to model size`. It is located on the **Configuration Tab**. 

You can optionally create subdirectories using a formula similar to the Property Text Callout. For example `Material %{System.Material} Thickness %{Custom.Material Thickness}`. 

A `Property set`, either `System` or `Custom`, is required. For more information, see the **Property Filter** section above. 

It is possible that a property contains a character that cannot be used in a file name. If that happens, a replacement is read from filename_charmap.txt in the same directory as Housekeeper.exe. You can/should edit it to change the replacement characters to your preference. The file is created the first time you run Housekeeper.  For details, see the header comments in that file. 

### Part

#### Open/Save
Same as the Assembly command of the same name.

#### Property find replace
Same as the Assembly command of the same name.

#### Expose variables missing
Same as the Assembly command of the same name.

#### Expose variables
Same as the Assembly command of the same name.

#### Update face and view styles from template
Same as the Assembly command of the same name.

#### Update material from material table
Checks to see if the part's material name and properties match any material in a file you specify on the **Configuration Tab**. 

If the names match, but their properties (e.g., face style) do not, the material is updated. If the names do not match, or no material is assigned, it is reported in the log file.

#### Hide constructions
Same as the Assembly command of the same name.

#### Fit pictorial view
Same as the Assembly command of the same name.

#### Update insert part copies
In conjuction with `Assembly Activate and update all`, used mainly to eliminate the gray corners on assembly drawings.

#### Broken links
Same as the Assembly command of the same name.

#### Part number does not match file name
Same as the Assembly command of the same name.

#### Missing drawing
Same as the Assembly command of the same name.

#### Failed or warned features
Checks if any features of the model are in the Failed or Warned status.

#### Suppressed or rolled back features
Checks if any features of the model are in the Suppressed or Rolledback status.

#### Underconstrained profiles
Checks if any profiles are not fully constrained.

#### Insert part copies out of date
If the file has any insert part copies, checks if they are up to date.

#### Material not in material table
Checks the file's material against the material table. The material table is chosen on the **Configuration Tab**. 

#### Run external program
Same as the Assembly command of the same name.

#### Interactive edit
Same as the Assembly command of the same name.

#### Save As
Same as the Assembly command of the same name.

### Sheetmetal

#### Open/Save
Same as the Assembly command of the same name.

#### Property find replace
Same as the Assembly command of the same name.

#### Expose variables missing
Same as the Assembly command of the same name.

#### Expose variables
Same as the Assembly command of the same name.

#### Update face and view styles from template
Same as the Part command of the same name.

#### Update material from material table
Same as the Part command of the same name.

#### Hide constructions
Same as the Assembly command of the same name.

#### Fit pictorial view
Same as the Assembly command of the same name.

#### Update insert part copies
Same as the Part command of the same name.

#### Update design for cost
Updates DesignForCost and saves the document.

An annoyance of this command is that it opens the DesignForCost Edgebar pane, but is not able to close it. The user must manually close the pane in an interactive Sheetmetal session. The state of the pane is system-wide, not per-document, so closing it is a one-time action. 

#### Broken links
Same as the Assembly command of the same name.

#### Part number does not match file name
Same as the Part command of the same name.

#### Missing drawing
Same as the Assembly command of the same name.

#### Failed or warned features
Same as the Part command of the same name.

#### Suppressed or rolled back features
Same as the Part command of the same name.

#### Underconstrained profiles
Same as the Part command of the same name.

#### Insert part copies out of date
Same as the Part command of the same name.

#### Flat pattern missing or out of date
Checks for the existence of a flat pattern. If one is found, checks if it is up to date. 

#### Material not in material table
Same as the Part command of the same name.

#### Run external program
Same as the Assembly command of the same name.

#### Interactive edit
Same as the Assembly command of the same name.

#### Save As
Same as the Assembly command of the same name, except two additional options -- `DXF Flat (\*.dxf)` and `PDF Drawing (\*.pdf)`. 

The `DXF Flat` option saves the flat pattern of the sheet metal file. 

The `PDF Drawing` option saves the drawing of the sheet metal file. The drawing must have the same name as the model, and be in the same directory. A more flexible option may be to use the Draft `Save As`, using a `Property Filter` if needed. 

### Draft

#### Open/Save
Same as the Assembly command of the same name.

#### Update drawing views
Checks drawing views one by one, and updates them if needed.

#### Update styles from template
Creates a new file from a template you specify on the **Configuration Tab**. Copies drawing views, dimensions, etc. from the old file into the new one. If the template has updated styles, a different background sheet, or other changes, the new drawing will inherit them automatically. 

This task has the option to `Allow partial success`.  It is set on the **Configuration Tab**. If the option is set, and some drawing elements were not transferred, it is reported in the log file. Also reported in the log file are instructions for completing the transfer. 

Note, because this task needs to do a `Save As`, it must be run with no other tasks selected.

#### Update drawing border from template
Replaces the background border with that of the Draft template specified on the **Configuration Tab**.

In contrast to `UpdateStylesFromTemplate`, this command only replaces the border. It does not attempt to update styles or anything else.

#### Fit view
Same as the Assembly command of the same name.

#### File name does not match model file name
Same as the Assembly command of the same name.

#### Broken links
Same as the Assembly command of the same name.

#### Drawing views out of date
Checks if drawing views are not up to date.

#### Detached dimensions or annotations
Checks that dimensions, balloons, callouts, etc. are attached to geometry in the drawing.

#### Parts list missing or out of date
Checks is there are any parts list in the drawing and if they are all up to date.

#### Run external program
Same as the Assembly command of the same name.

#### Interactive edit
Same as the Assembly command of the same name.

#### Print
Print settings are accessed on the **Configuration Tab**.

Note, the presence of the Printer Settings dialog is somewhat misleading. The only settings taken from it are the printer name, page height and width, and the number of copies. Any other selections revert back to the Windows defaults when printing. A workaround is to create a new Windows printer with the desired defaults. 

Another quirk is that, no matter the selection, the page width is always listed as greater than or equal to the page height. In most cases, checking `Auto orient` should provide the desired result. 

#### Save As
Same as the Assembly command of the same name, except as follows.

Optionally includes a watermark image on the output.  For the watermark, set X/W and Y/H to position the image, and Scale to change its size. The X/W and Y/H values are fractions of the sheet's width and height, respectively. So, (`0,0`) means lower left, (`0.5,0.5`) means centered, etc. Note some file formats may not support bitmap output.

The option `Use subdirectory formula` can use an Index Reference designator to select a model file contained in the draft file. This is similar to Property Text in a Callout, for example, `%{System.Material|R1}`. To refer to properties of the draft file itself, do not specify a designator, for example, `%{Custom.Last Revision Date}`. 


## CODE ORGANIZATION

Processing starts in Form1.vb.  A short description of the code's organization can be found there.

