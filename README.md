
![Logo](My%20Project/media/logo.png)
<p align=center>Robert McAnany 2022

**Contributors**:
@farfilli (aka @Fiorini), @daysanduski

**Helpful feedback and bug reports:**
@Satyen, @n0minus38, @wku, @aredderson, @bshand, @TeeVar, @SeanCresswell, 
@Jean-Louis, @Jan_Bos, @MonkTheOCD_Engie, @[mike miller], @Fiorini, 
@[Martin Bernhard], @Derek G, @Chris42, @Jason1607436093479, @Bob Henry, 
@JayJay101

Portions adapted from code by Jason Newell, Tushar Suradkar, Greg Chasteen,
and others.  Most of the rest copied verbatim from Jason's repo and Tushar's blog.

## DESCRIPTION

Solid Edge Housekeeper helps you find annoying little errors in your project. 
It can identify failed features in 3D models, detached dimensions in drawings, 
missing parts in assemblies, and more.  It can also update certain individual 
file settings to match those in a template you specify.

![Home Tab Done](My%20Project/media/home_tab_done.png)

*Feedback from users*

> *This is the Michael Jordan of macros!  I've tried lots of them.  This is*
> *on a whole other level.  Thank you!*

> *This is going to save me SO MUCH TIME!  Thank you for sharing!*

> *Thank you for all your time and effort (...) Also thanks a lot for making it*
> *open source. I constantly reference your code for my own macros, which motivates*
> *me to make my projects open source as well.*

> *Awesome. It looks like you are still overachieving with this app, and I thank*
> *you for it. If they ever figure out how to automate me running Housekeeper, I*
> *will be out of a job. Thank God only I know how to press that "process" button.*


## GETTING HELP

Ask questions or suggest improvements on the 
[**Solid Edge Forum**](https://community.sw.siemens.com/s/topic/0TO4O000000MihiWAC/solid-edge)

To subscribe to update notices or volunteer to be a beta tester, 
message me, RobertMcAnany, on the forum. 
(Click your profile picture, then `My Messages`, then `Create`). 
Unsubscribe the same way.  

To combat bots and spam, I will probably 
ignore requests from `User16612341234...`. 
(Change your nickname by clicking your profile picture, then 
`My Profile`, then `Edit`). 

## HELPING OUT

If you want to help out on Housekeeper, sign up as a beta tester! 
Beta testing is nothing more than doing your own workflow on your own files and 
letting me know if you run into problems.  It isn't meant to be a lot of work. 
The big idea is to make the program better for you and me and everyone else!

If you know .NET, or want to learn, there's more
to do!  To get started on GitHub collaboration, head over to
[**ToyProject**](https://github.com/rmcanany/ToyProject). 
There are instructions and links to get you up to speed.


## INSTALLATION

There is no installation per se.  The preferred method is to download or clone 
the project and compile it yourself.

The other option is to use the latest 
[**Release**](https://github.com/rmcanany/SolidEdgeHousekeeper/releases). 
It will be the top entry on the page. 


![Release Page](My%20Project/media/release_page.png)

Click the 
file `SolidEdgeHousekeeper-vyyyy-x.zip` 
(sometimes hidden under the Assets dropdown). 
It should prompt you to save it. 
Choose a convenient location on your machine. 
Extract the zip file (probably by right-clicking and selecting Extract All). 
Double-click the `.exe` file to run.

If you are upgrading from a previous release, you should be able to copy 
the settings files from the old version to the new. 
The files are `defaults.txt`, `property_filters.txt`, and `filename_charmap.txt`. 
If you haven't used Property Filter, `property_filters.txt` won't be there. 
Versions prior to 0.1.10 won't have `filename_charmap.txt` either.

## OPERATION

![Tabs](My%20Project/media/tabs.png)

On each file type's tab, select which tasks to perform. 
On the Home tab, select which files to process. 
You can select files by folder, subfolder, top-level assembly or list.
There can be any number of each, in any combination.
You can refine the search using a property filter, a wildcard filter, or both. 
See **FILE SELECTION AND FILTERING** below for details. 

If any errors are found, a log file will be written 
to your temp folder. 
It will identify each error and the file in which it occurred. 
When processing is complete, the log file is opened in Notepad for review.

The first time you use the program, some site-specific information is needed. 
This includes the location of your templates, material table, etc. 
These are populated on the Configuration Tab.

To start execution, click the Process button.  The status
bar provides feedback to help you monitor progress. 
You can also stop execution if desired.
See **STARTING, STOPPING, AND MONITORING EXECUTION** for details.




## FILE SELECTION AND FILTERING

### Selection

The Home Tab is where you select which files to process.  As mentioned above,
using the Selection Toolbar, you can select by folder, subfolder, top-level 
assembly, top-level folder, or list.
There can be any number of each, in any combination.  

Another option is to drag and drop files from Windows File Explorer. 
You can use drag and drop and the toolbar in combination.

An alternative method is to select files with errors from a previous run. 

![Toolbar](My%20Project/media/select_toolbar.png)

#### 1. Select by Folder

Choose this option to select files within a single folder, 
or a folder and its subfolders. 
Referring to the diagram, 
click ![Folder](Resources/icons8_Folder_16.png)
to select a single folder, 
click ![Folders](Resources/icons8_folder_tree_16.png)
for a folder and sub folders.

#### 2. Select by Top-Level Assembly

Choose this option to select files linked to an assembly.
Again referring to the diagram, 
click ![Assembly](Resources/ST9%20-%20asm.png)
to choose the assembly, 
click ![Assembly Folders](Resources/icons8_Folders_16.png)
to choose where to search for _where used_ files. 

If you don't specify any *where used* folders, Housekeeper simply finds
files directly linked to the specified assembly and subassemblies.

If you _do_ specify one or more folders, there are two options on how 
the _where used_ is performed, **Top Down** or **Bottom Up** (see next).  Make 
this selection on the **Configuration Tab**.  Guidelines are given below,
however it's not a bad idea to try both methods to see which works best
for you.

**Bottom Up**

Bottom up is meant for general purpose (hopefully indexed) directories 
(e.g., `\\BIG_SERVER\all_parts\`), where the number of files 
in the folder(s) far exceed the number of files in the assembly. 
The program gets links by recursion, then 
finds draft files with _where used_. 

A bottom up search requires a valid Fast Search Scope filename, 
(e.g., `C:\Program Files\...\Preferences\FastSearchScope.txt`), 
which tells the program if the specified folder is on an indexed drive. 
Set the Fast Search Scope filename on the **Configuration Tab**.

**Top Down**

Top down is meant for self-contained project directories 
(e.g., `C:\Projects\Project123\`), where most of the files 
in the folder(s) are related to the assembly. 
The program opens every file within and below the input directory. 
As it does, it creates a graph of the links. 
The graph is subsequently traversed to find related files. 
I don't know how it works; my son did that part. 

A top down search can optionally report files with no links to the 
top level assembly.  It is set on the **Configuration Tab**.

#### 3. Select by list

Referring to the diagram, 
click ![Import List](Resources/icons8_import_16.png)
to import a list, 
click ![Export List](Resources/icons8_export_16.png)
to export one.  

If you are importing a list from another source, be aware that the 
file names must contain the full path.  E.g.,
`D:\Projects\Project123\Partxyz.par`, not just `Project123\Partxyz.par`.

#### 4. Tools

**Select files with errors from the previous run**

Click ![Errors](Resources/icons8_Error_16.png)
to select only files that encountered an error. 
All other files will be removed from the list.  To reproduce the 
TODO list functionality from previous versions, you can export the 
resultant list if desired.

**Remove all**

Click ![Remove All](Resources/icons8_trash_16.png)
to remove all folders and files from the list.

**RMB shortcut menu**

![RMB Shortcut Menu](My%20Project/media/RMB_shortcut_menu.png)

If you select one or more files on the list, you can click the right 
mouse button for more options.  Use **Open** to view the files in 
Solid Edge, **Open folder** to view them in File Explorer, 
**Process selected** to run selected Tasks on only those files, 
and finally **Remove from list** to move them to the *Excluded files* 
section of the list.

#### 5. Update

The update button 
![Update](Resources/Synch_16.png)
populates the file list from the File Sources and Filters.
If any Sources are added or removed, or a change is made to a Filter (see **Filtering** below), 
an update is required.

#### 6. File Type

You can limit the search to return only selected types of Solid Edge files.
To do so, check/uncheck the appropriate File Type
![Assembly](Resources/ST9%20-%20asm.png)
![Part](Resources/ST9%20-%20par.png)
![Sheet Metal](Resources/ST9%20-%20psm.png)
![Draft](Resources/ST9%20-%20dft.png) 

### Filtering

![Filter Toolbar](My%20Project/media/filter_toolbar.png)

Filters are a way to refine the list of files to process.  You can filter 
on file properties, or filenames (with a wildcard search). 
They can be used alone or in combination.

#### Property Filter

The property filter allows you to select files by their property values.
To configure a property filter, 
click the tool icon ![Draft](Resources/icons8_tools_16.png)
to the right of
the Property filter checkbox. 

This is a powerful tool with a lot
of options. These are detailed below.

**Composing a Filter**

![Property Filter](My%20Project/media/property_filter.png)

Compose a filter by defining one or more **Conditions**, and adding them 
one-by-one to the list.
A **Condition** consists of a **Property**, a **Comparison**, and a **Value**.
For example, `Material contains Steel`, where `Material` is the **Property**, 
`contains` is the **Comparison**, and `Steel` is the **Value**.

Up to six Conditions are allowed for a filter.
The filters can be named, saved, modified, and deleted as desired.

**Property Set**

In addition to entering the `Property name`, you must also 
select the `Property set`, either `System` or `Custom`.

`System` properties are in every Solid Edge file.
They include `Material`, `Project`, etc.
Note, at this time, they must be in English.

`Custom` properties are ones that you create, probably in a template.
Solid Edge also creates some Custom properties for you.
These include `Exposed Variables` and output from 
`Inspect > Physical Properties` command.
The custom property names can be in any language. 
(In theory, at least -- not tested at this time.
Not sure about the Solid Edge variables either.)

**Comparison**

Select the Comparison from its dropdown box.
The choices are `contains`, `is_exactly`, `is_not`, 
`wildcard_contains`, `>`, or `<`.
The options `is_exactly`, `is_not`, `>`, and `<` are hopefully 
self explanatory.

`Contains` means the Value can appear anywhere in the property.
For example, if you specify `Aluminum` and a part file has 
`Aluminum 6061-T6`, you will get a match.
Note, at this time, all Values (except see below for dates and numbers) 
are converted to lower case text before comparison.
So `ALUMINUM`, `Aluminum`, and `aluminum` would all match.

`Wildcard_contains` is like `contains`, except with wildcards. 
For example `[bfj]ake` would match
`bake`, `fake`, and `jake`.  The `contains` part means that
`I baked the cake!` would also match.

Internally the 
[**VB Like Operator**](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator)
is used to make the comparison.  Visit the link for details and examples.

**Default Filter Formula**

Each Condition is assigned a variable name, (`A`, `B`, `...`).
The default filter formula is to match all conditions (e.g., `A AND B AND C`).

Referring to the Property Filter dialog shown above, 
the default formula means you will get all parts in project 7477 
made out of Aluminum and engineered by Fred.

**Editing the Formula**

You can optionally change the formula.
Click the Edit button and type the desired expression.
For example, if you wanted all parts from Project 7477, 
either made out of Aluminum, 
or engineered by Fred, you would enter `A AND (B OR C)`.

**Dates and Numbers**

Dates and numbers are converted to their native format when possible.
This is done to obtain commonsense results for `<` and `>`.

Dates take the form `YYYYMMDD` when converted.
This is the format that must be used in the `Value` field.
So if you wanted all files dated before January 1, 2022, your condition would be
`Custom.Date < 20220101`.
The conversion is supposed to be locale-aware, however this has not been tested.
Please ask on the Solid Edge Forum if it is not working correctly for you.

Numbers are converted to floating point decimals.
In Solid Edge many numbers, in particular those from the variable table, 
include units.
These must be stripped off by the program to make comparisons.
Currently only distance and mass units are checked (`in`, `mm`, `lbm`, `kg`).
It`s easy to add more, so please ask on the Forum if you need others.

**Saved Settings**

The filters are saved in `property_filters.txt` in the same directory as 
Housekeeper.exe.
If desired, you can create a master copy of the file and share it with others.
You can manually edit the file, 
however, note that the field delimiter is the TAB character.
This was done so that the property name and value fields could contain 
spaces.

#### Wildcard Filter

Filtering by wildcard is done by entering the wildcard pattern in the 
provided combobox.  Wildcard patterns are automatically saved for 
future use.  Delete a pattern that is no longer needed by selecting it 
and clicking ![Draft](Resources/icons8_Close_Window_16.png). 

As suggested above, see
[**VB Like Operator**](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator)
for details and examples.

## STARTING, STOPPING, AND MONITORING EXECUTION

![Tabs](My%20Project/media/status_bar_running.png)

Press the Process button to start executing the chosen tasks.
If one or more files on the list were selected, only those are processed.
Otherwise, all files are processed.

A checkbox
![Error](Resources/icons8_unchecked_checkbox_16.png) to the left of
the file name indicates it has yet to be processed. Afterwards, if no errors were
detected, a checkmark 
![Error](Resources/icons8_Checked_Checkbox_16.png) is shown. 
Otherwise, an error indicator 
![Error](Resources/icons8_Error_16.png) is displayed.

You can monitor progress on the status bar.  It shows the number of files
processed, the current file, and an estimate of time remaining.
 
You can also interrupt the program before it finishes. 
As shown above, while processing, 
the Cancel button changes to a Stop button.  Just click that to halt 
execution.  It may take several seconds to register the request.  It 
doesn't hurt to click it a couple of times.


## CAVEATS

Since the program can process a large number of files in a short amount of time, 
it can be very taxing on Solid Edge. 
To maintain a clean environment, the program restarts Solid Edge periodically. 
(Set the frequency on the **Configuration** tab.)
This is by design and does not necessarily indicate a problem.

However, problems can arise. 
Those cases will be reported in the log file with the message 
`Error processing file`. 
A stack trace will be included, which may be useful for debugging. 
If four of these errors are detected in a run, the programs halts with the 
Status Bar message `Processing aborted`.

Please note this is not a perfect program.  It is not guaranteed not to mess 
up your work.  Back up any files before using it.

## KNOWN ISSUES

**Does not support managed files**  
*Cause*: Unknown.  
*Possible workaround*: Process the files in an unmanaged workspace.  
*Update 10/10/2021* Some users have reported success with BiDM managed files.  
*Update 1/25/2022* One user has reported success with Teamcenter 'cached' files. 

**Older Solid Edge versions**  
Some tasks may not support versions of Solid Edge prior to SE2020.  
*Cause*: Maybe an API call not available in previous versions.  
*Possible workaround*: Use SE2020 or later. 

**Multiple installed Solid Edge versions**  
May not support multiple installed versions on the same machine.  
*Cause*: Unknown.  
*Possible workaround*: Use the version that was 'silently' installed. 

**Printer settings**  
Does not support all printer settings, e.g., duplexing, collating, etc.  
*Cause*: Not exposed in the `DraftPrintUtility()` API.  
*Possible workaround*: Create a new Windows printer with the desired settings. 
Refer to the **Draft Tasks -- Print Topic** below for more details. 

**Pathfinder during Interactive Edit**  
Pathfinder is sometimes blank when running the 'Interactive Edit' task.  
*Cause*: Unknown.  
*Possible workaround*: Refresh the screen by minimizing and maximizing the 
Solid Edge window. 

## TASK DESCRIPTIONS

![Interface_Large](My%20Project/media/sheetmetal_tab_done.png)

<!-- Everything below this line is auto-generated.  Do not edit. -->
<!-- Start -->

### Assembly

#### Open/Save
Open a document and save in the current version.

#### Activate and update all
Loads all assembly occurrences' geometry into memory and does an update. Used mainly to eliminate the gray corners on assembly drawings. 

Can run out of memory for very large assemblies.

#### Property find replace
Searches for text in a specified property and replaces it if found. The property, search text, and replacement text are entered on the task tab, below the task list. 

A `Property set`, either `System` or `Custom`, is required. System properties are in every Solid Edge file. They include Material, Manager, Project, etc. At this time, they must be in English. Custom properties are ones that you create, probably in a template. 

The search is case insensitive, the replace is case sensitive. For example, say the search is `aluminum`, the replacement is `ALUMINUM`, and the property value is `Aluminum 6061-T6`. Then the new value would be `ALUMINUM 6061-T6`. 

#### Expose variables missing
Checks to see if all the variables listed in `Variables to expose` are present in the document.

#### Expose variables
Enter the names as a comma-delimited list in the `Variables to expose` textbox. Optionally include a different Expose Name, set off by the colon `:` character. 

For example

`var1, var2, var3`

Or

`var1: Variable Name One, var2: Variable Name 2, var3: Variable Name 3`

Or a combination

`var1: Variable Name One, var2, var3`

Note: You cannot use either a comma `,` or a colon `:` in the Expose Name. Actually you can, but it will not do what you expect. 

#### Remove face style overrides
Face style overrides change a part's appearance in the assembly. This command causes the part to appear the same in the part file and the assembly.

#### Update face and view styles from template
Updates the file with face and view styles from a file you specify on the Configuration tab. 

Note, the view style must be a named style.  Overrides are ignored. To create a named style from an override, use `Save As` on the `View Overrides` dialog.

#### Hide constructions
Hides all non-model elements such as reference planes, PMI dimensions, etc.

#### Fit pictorial view
Maximizes the window, sets the view orientation, and does a fit.

Select the desired orientation on the Configuration Tab.

#### Part number does not match file name
Checks if a file property, that you specify on the Configuration tab, matches the file name.

#### Missing drawing
Assumes drawing has the same name as the model, and is in the same directory

#### Broken links
Checks to see if any assembly occurrence is pointing to a file not found on disk.

#### Links outside input directory
Checks to see if any assembly occurrence resides outside the top level directories specified on the Home tab. 

#### Failed relationships
Checks if any assembly occurrences have conflicting or otherwise broken relationships.

#### Underconstrained relationships
Checks if any assembly occurrences have missing relationships.

#### Run external program
Runs an `\*.exe` or `\*.vbs` file.  Select the program with the `Browse` button. It is located on the task tab below the task list. 

Several rules about the program implementation apply. See **[HousekeeperExternalPrograms](https://github.com/rmcanany/HousekeeperExternalPrograms)** for details and examples. 

#### Interactive edit
Brings up files one at a time for manual processing.  Some rules apply.

It is important to leave Solid Edge in the state you found it when the file was opened. For example, if you open another file, such as a drawing, you need to close it. If you add or modify a feature, you need to click Finish. 

Also, do not Close the file or do a Save As on it. Housekeeper maintains a `reference` to the file. Those two commands cause the reference to be lost, resulting in an exception. 

#### Save as
Exports the file to a non-Solid Edge format. 

Select the file type using the `Save As` combobox. Select the directory using the `Browse` button, or check the `Original Directory` checkbox. These controls are on the task tab below the task list. 

Images can be saved with the aspect ratio of the model, rather than the window. The option is called `Save as image -- crop to model size`. It is located on the Configuration tab. 

You can optionally create subdirectories using a formula similar to the Property Text Callout. For example `Material %{System.Material} Thickness %{Custom.Material Thickness}`. The PropertySet designation, `System.` or `Custom.` is required. These refer to where the property is stored in a Solid Edge file. 

System properties are in every Solid Edge file. They include Material, Project, etc. Note, at this time, the System property names must be specified in English. Custom properties are ones that you create, probably in a template. The custom property names can be in any language.  (In theory, at least -- not tested at this time.)

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
Checks to see if the part's material name and properties match any material in a file you specify on the Configuration tab. 

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
Checks the file's material against the material table. The material table is chosen on the Configuration tab. 

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
Creates a new file from a template you specify on the Configuration tab. Copies drawing views, dimensions, etc. from the old file into the new one. If the template has updated styles, a different background sheet, or other changes, the new drawing will inherit them automatically. 

This task has the option to `Allow partial success`.  It is set on the Configuration tab. If the option is set, and some drawing elements were not transferred, it is reported in the log file. Also reported in the log file are instructions for completing the transfer. 

Note, because this task needs to do a `Save As`, it must be run with no other tasks selected.

#### Update drawing border from template
Replaces the background border with that of the Draft template specified on the Configuration tab.

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
Print settings are accessed on the Configuration tab.

Note, the presence of the Printer Settings dialog is somewhat misleading. The only settings taken from it are the printer name, page height and width, and the number of copies. Any other selections revert back to the Windows defaults when printing. A workaround is to create a new Windows printer with the desired defaults. 

Another quirk is that, no matter the selection, the page width is always listed as greater than or equal to the page height. In most cases, checking `Auto orient` should provide the desired result. 

#### Save As
Same as the Assembly command of the same name, except as follows.

Optionally includes a watermark image on the output.  For the watermark, set X/W and Y/H to position the image, and Scale to change its size. The X/W and Y/H values are fractions of the sheet's width and height, respectively. So, (`0,0`) means lower left, (`0.5,0.5`) means centered, etc. Note some file formats may not support bitmap output.

The option `Use subdirectory formula` can use an Index Reference designator to select a model file contained in the draft file. This is similar to Property Text in a Callout, for example, `%{System.Material|R1}`. To refer to properties of the draft file itself, do not specify a designator, for example, `%{Custom.Last Revision Date}`. 


## CODE ORGANIZATION

Processing starts in Form1.vb.  A short description of the code's organization can be found there.

