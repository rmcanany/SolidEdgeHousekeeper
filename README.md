<div class="center">
  <p align=center>
  <img src="My%20Project/media/logo.png" width=50%;>
  <p align=center>
  <span class="description">Robert McAnany 2025</span>
</div>

<details open><summary><h2 style="margin:0px; display:inline-block">CREDITS</h2></summary>


**Contributors:**
@[Francesco Arfilli] (github @farfilli), @daysanduski, @mmtrebuchet (github), @[o_o ....码], @ChrisNC (github @ChrisClems), @ZaPpInG (github @lrmoreno007), @aalian.khan8036 (@AalianKhan  github), @KGeetings (github)

**Beta Testers:**
@JayJay04, @Cimarian_RMP, @n0minus38, @xenia.turon, @MonkTheOCD_Engie, @HIL, @[Robin BIoemberg], @[Jan Bos], @Rboyd347, @Jojo15702, @ih0nza, @mefrebo, @KGeetings

**Helpful feedback and bug reports:**
@Satyen, @n0minus38, @wku, @aredderson, @bshand, @TeeVar, @SeanCresswell, @Jean-Louis, @Jan_Bos, @MonkTheOCD_Engie, @[mike miller], @[Francesco Arfilli], @[Martin Bernhard], @[Derek G], @Chris42, @JasonT, @Bob Henry, @JayJay04, @nate.arinta5649, @DaveG, @tempod, @64Pacific, @ben.steele6044, @KennyG, @Alex_H, @Nosybottle, @Seva, @HIL, @[o_o ....码], @roger.ribamatic, @jnewell, @[Robin BIoemberg], @Pedro0996, @Imre Szucs, @Bert303, @gir.isi, @BrianVR74, @CareFrame1, @RBoyd347, @[Artur Sampaio], @pkoevesdi, @Jojo15702, @chjchen, @FERNANDO-DALLAGNOLO, @ih0nza, @mefrebo, @KGeetings

**Notice:**
*Portions adapted from code by Jason Newell, Tushar Suradkar, Greg Chasteen, and others.  Most of the rest copied verbatim from Jason's repo or Tushar's blog.*

</details>

<details open><summary><h2 style="margin:0px; display:inline-block">DESCRIPTION</h2></summary>

Solid Edge Housekeeper helps you find annoying little errors in your project. It can identify failed features in 3D models, detached dimensions in drawings, missing parts in assemblies, and more.  It can also update certain individual file settings to match those in a template you specify.

If this is your first time here, you may want to check out the [<ins>**Quick Start Guide**</ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/quick_start_guide.md).  It's not nearly as detailed as this README, but it will get you up and running much more quickly.


<p align="center">
  <img src="My%20Project/media/home_tab_done.png">
</p>

*Feedback from users*

> *This is the Michael Jordan of macros!  I've tried lots of them.  This is on a whole other level.  Thank you!*

> *This is going to save me SO MUCH TIME!  Thank you for sharing!*

> *Thank you for all your time and effort (...) Also thanks a lot for making it open source. I constantly reference your code for my own macros, which motivates me to make my projects open source as well.*

> *Awesome. It looks like you are still overachieving with this app, and I thank you for it. If they ever figure out how to automate me running Housekeeper, I will be out of a job!*

> *Rad, this saves a mountain of time for me. Thanks!*

Responding to the prompt *"Heard any good jokes about Solid Edge Housekeeper?"*, Google's Bard said:

> *Why did the Solid Edge Housekeeper get a promotion?*  
> *She was the only one who could clean up the mess that Solid Edge users make.*

*Download Tracker*

<p align="center">
  <img src="My%20Project/media/plot_downloads.png">
</p>

*Please note, for ease of navigation, the remaining topics are in outline form.  Click a heading to view its contents.*

</details>

<details><summary><h2 style="margin:0px; display:inline-block"><img src="Resources/TaskSaveAs.png"> INSTALLATION</h2></summary>


There is no installation *per se*.  The preferred method is to download or clone the project and compile it yourself.

The other option is to use the [<ins>**Latest Release**</ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/releases). It will be the top entry on the page. 


<p align="center">
  <img src="My%20Project/media/release_page.png">
</p>

Click the file `SolidEdgeHousekeeper-vYYYY.N.zip` (sometimes hidden under the Assets dropdown). It should prompt you to save it. Choose a convenient location on your machine. Extract the zip file (probably by right-clicking and selecting Extract All). Double-click the `.exe` file to run.

The first time you run it, you may encounter the following dialog.  You can click `More Info` followed by `Run Anyway` to launch the program. ![Run Anyway](My%20Project/media/run_anyway.png)

If you are upgrading from a previous release, you should be able to copy the settings files from the old version to the new. The files are stored in Housekeeper's Preferences folder. There is one exception.  Starting with v2024.2, property filters are stored in a `*.json` format.  The old `*.txt` format is no longer used.

</details>

<details><summary><h2 style="margin:0px; display:inline-block"><img src="Resources/icons8_help_16.png"> GETTING HELP</h2></summary>


Start with the context-sensitive help icon ![Table of Contents](My%20Project/media/help.png).  Every command, and most dialogs, have one.  

If you need more general information, try the [<ins>**Table of Contents**</ins>](#table-of-contents).  It has links to all topics in the Readme.  Topics which are particularly detailed have their own contents section.

Ask questions, report bugs, or suggest improvements on the [<ins>**Solid Edge Forum**</ins>](https://community.sw.siemens.com/s/topic/0TO4O000000MihiWAC/solid-edge).

## HELPING OUT

If you want to make Housekeeper better, join us as a beta tester! Beta testing is nothing more than conducting your own workflow on your own files and telling me if you run into problems. It isn't meant to be a lot of work. The big idea is to make the program better for you and me and everyone else!

To sign up, message me, RobertMcAnany, on the forum. (The `Messages` button is hidden under your profile picture, at the very top right of the page). Unsubscribe the same way. To combat bots and spam, I will probably ignore requests from `User16612341234...`. (Change you nickname in `My Profile`, also under your profile picture). 

If you know .NET, or want to learn, there's more to do!  If you're new to GitHub collaboration, head over to [<ins>**ToyProject**</ins>](https://github.com/rmcanany/ToyProject). There are instructions and links to get you up to speed.  Otherwise, feel free to fork the repo and start in with those PRs!

</details>

<details><summary><h2 style="margin:0px; display:inline-block"><img src="Resources/icons8-key-16.png"> OPERATION</h2></summary>


![Tabs](My%20Project/media/tabs.png)

Select which files to process on the **Home Tab**.  Select what to do on the **Task Tab**.  There are many options for selecting files.  See [<ins>**FILE SELECTION AND FILTERING**</ins>](#file-selection-and-filtering) below for details. 

If any errors are found, a log file will be written to your temp folder. It will identify each error and the file in which it occurred. When processing is complete, the log file is opened in Notepad for review. If you want to open an old log file, look for file names starting with 'Housekeeper' in the `%temp%` folder.

![Status Bar](My%20Project/media/status_bar_ready.png)

To start execution, click the `Process` button.  The status bar tracks progress. You can also stop execution if desired. See [<ins>**STARTING, STOPPING, AND MONITORING EXECUTION**</ins>](#starting-stopping-and-monitoring-execution) for details.

</details>


<details><summary><h2 style="margin:0px; display:inline-block"><img src="Resources/icons8_Folders_16.png"> FILE SELECTION</h2></summary>


<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/icons8_Folder_16.png"> Selection</h3></summary>

You can select files by folder, subfolder, top-level assembly, top-level folder, or list. There can be any number of each, in any combination.  

Another option is to drag and drop files from Windows File Explorer. You can use drag and drop and the toolbar in combination.

An alternative method is to select files with errors from a previous run. 

For new users, there is one potential point of confusion.  Most of the commands discussed below populate the *File Sources* list, not the *File List* itself.  To get the *File List* from the *File Sources*, you need to click the `Update` button.  If an update is needed, it should be colored orange to let you know.

![Toolbar](My%20Project/media/selection_toolbar_labeled.png)

<details><summary><h4 style="margin:0px; display:inline-block">Select by Folder</h4></summary>

Choose this option to select individual files, files within a single folder, or a folder and its subfolders. Referring to the toolbar diagram, tool group 1, 

- Click ![Folder](Resources/icons8_versions_16.png) to select individual files
- Click ![Folder](Resources/icons8_Folder_16.png) to select a single folder
- Click ![Folders](Resources/icons8_folder_tree_16.png) for a folder and sub folders

The dialog that appears with these commands has `multi-select` enabled, meaning you can select any number of files/folders at a time.

</details>

<details><summary><h4 style="margin:0px; display:inline-block">Select by Top Level Assembly</h4></summary>

Choose this option to select files linked to an assembly. Referring to the toolbar diagram, tool group 2, 

- click ![Assembly](Resources/ST9%20-%20asm.png) to choose the assembly
- click ![Assembly Folders](Resources/icons8_Folders_16.png) to choose the search path for *where used* files

You would be asking for trouble specifying more than one top-level assembly.  However, you can have any number of folders.  As with the previous commands, `multi-select` is enabled for folders. Note the program always includes subfolders for *where used* files.

There are a lot of options with this selection type.  They are covered here.  Shown below is the top-level assembly configuration page.

![Top level assembly options](My%20Project/media/configuration_tab_top_level_assembly_page.png)

A top level assembly search can optionally report files with no links to the assembly.  Set this and other options on the **Configuration Tab -- Top Level Assembly Page** as shown above.

When selecting a top-level assembly, you can automatically include the folder in which it resides. This `auto include` option is on by default. 

If `auto include` is turned off, you do not have to specify any folders. In that case, Housekeeper simply finds files directly linked to the specified assembly and subassemblies. Note this means that no draft files will be found. For that reason, a warning is displayed; disable the `Warn me` option to turn it off.

If you *do* specify one or more folders, there are two options for performing *where used*, **Top Down** or **Bottom Up** (see next). Guidelines are given below, however it's not a bad idea to try both methods to see which works best for you.

<details><summary><h5 style="margin:0px; display:inline-block">Bottom Up</h5></summary>

Bottom up is meant for general purpose (hopefully indexed) directories (e.g., `\\BIG_SERVER\all_parts\`), where the number of files in the folder(s) far exceed the number of files in the assembly. The program gets links by recursion, then finds draft files with *where used*. If your draft files have the same name as the model they depict, click that option and the program will find drawings directly, bypassing the often time-consuming *where used* operation. 

A bottom up search requires a valid Fast Search Scope filename, (e.g., `C:\Program Files\...\Preferences\FastSearchScope.txt`), which tells the program if the specified folder is on an indexed drive. 

</details>

<details><summary><h5 style="margin:0px; display:inline-block">Top Down</h5></summary>

Top down is meant for self-contained project directories (e.g., `C:\Projects\Project123\`), where most of the files in the folder(s) are related to the assembly. The program opens every file within and below the top-level assembly folder(s). As it does, it creates a graph of the links. The graph is subsequently traversed to find related files. I don't know how it works; my son did that part. 

A bottom up search requires a valid LinkMgmt filename, (e.g., `C:\Program Files\...\Preferences\LinkMgmt.txt`), which tells the program the order in which to resolve links. 

</details>

<details><summary><h5 style="margin:0px; display:inline-block">Include Parents of Part Copies</h5></summary>

This option may be confusing.  Referring to the diagram, note that `C.par` is a parent of `B.par`.  `B.par` is in `top.asm`, while `C.par` is not. Enabling the option means that `C.par` would be included in the search results.

![Top level assembly options](My%20Project/media/top_level_assy_diagram.png)

</details>

</details>

<details><summary><h4 style="margin:0px; display:inline-block">Select by List</h4></summary>

OK, that's enough about top-level assemblies.  Getting back to our file selection options, referring to the toolbar diagram, tool group 3, 
- Click ![Import List](Resources/icons8_Import_16.png) to import a list
- Click ![Export List](Resources/icons8_Export_16.png) to export one

If you are importing a list from another source, be aware that the file names must contain the full path.  E.g., `D:\Projects\Project123\Partxyz.par`, not just `Partxyz.par`.

</details>

<details><summary><h4 style="margin:0px; display:inline-block">Tools</h4></summary>

Referring to the toolbar diagram, tool group 4, 
- Click ![Errors](Resources/icons8_Error_16.png) to select only files that encountered an error. All other files will be removed from the list.  To reproduce the TODO list functionality from previous versions, you can export the resultant list if desired.
- Click ![Remove All](Resources/icons8_trash_16.png) to remove all folders and files from the list.

</details>

<details><summary><h4 style="margin:0px; display:inline-block">Update</h4></summary>

Referring to the toolbar diagram, tool group 5, 
- Click ![Update](Resources/Synch_16.png) to populate the file list from the File Sources and Filters. If any Sources are added or removed, or a change is made to a Filter (see [<ins>**Filtering**</ins>](#filtering) below), an update is required.  In those cases the button will turn orange to let you know.  

</details>

<details><summary><h4 style="margin:0px; display:inline-block">File Type</h4></summary>

You can limit the search to return only selected types of Solid Edge files. Referring to the toolbar diagram, tool group 6, 
- Click ![Assembly](Resources/ST9%20-%20asm.png) to toggle assembly file selection
- Click ![Part](Resources/ST9%20-%20par.png)  to toggle part file selection
- Click ![Sheet Metal](Resources/ST9%20-%20psm.png)  to toggle sheet metal file selection
- Click ![Draft](Resources/ST9%20-%20dft.png)  to toggle draft file selection

</details>

<details><summary><h4 style="margin:0px; display:inline-block">File List Options</h4></summary>

You can customize the list to show file properties if desired.  Referring to the toolbar diagram, tool group 7, 
- Click ![Columns](Resources/icons8_table_properties_16.png) to open the column selection pane, shown on the left below

![Customize file list columns](My%20Project/media/file_list_columns_customize.png)

Click the Add button to bring up the Property Selector dialog.  By default, it shows properties you added to your Favorites.  To show all properties, click the Show All Props button.  Note, before you can use properties, they must first be read from your templates.  See the [<ins>**Templates Page**</ins>](#templates-page) for details.

Getting properties for a large number of files can take some time.  If that's the case, when you click Update, the program asks whether or not to display them.

You can edit properties directly on the list.  Double-click the property and enter the new value.  You can also sort the list by column.  Click the column header to sort.  Click it again to reverse the order.

</details>

<details><summary><h4 style="margin:0px; display:inline-block">Shortcut Menu</h4></summary>

If you select one or more files on the list, you can click the right mouse button for more options.  

![Shortcut Menu](My%20Project/media/shortcut_menu.png)

- **Open:** Opens the files in Solid Edge.
- **Open folder:** Opens the files in Windows File Explorer.
- **Find linked files:** Populates the list with files linked to a top-level assembly.  Similar to **Update** but no other File Sources are processed.
- **Process selected:** Runs selected Tasks on the selected files. This does the same thing as clicking the **Process** button.
- **Remove from list:** Moves the files to the *Excluded files* section of the list.
- **Move to Recycle Bin:** This is kinda asking for trouble, but can be handy with top-level assembly `Report unrelated files`.  If there are a lot of them, cleanup is a hassle.  Here's how I do it now.  
 
  - Save the Unrelated Files report as a *.txt file
  - Empty out the Recycle Bin
  - Clear the file list from the previous run.  *This is important so you don't mix in good files with the bad.*
  - Import the *.txt file and update the list
  - Select files you no longer need and use this command.  Then open the top level assembly; if it complains about missing files, run `Restore items` from the Recycle Bin.  (I do a few files at a time, then repeat.)

</details>

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/icons8_list_view_16.png"> Sorting</h3></summary>

You can sort the file list in a variety of ways.

![File list sorting options](My%20Project/media/configuration_tab_sorting_page.png)

The options are `Unsorted`, `Alphabetical`, `Dependency`, or `Random sample`.  Set the sort mode on the **Configuration Tab -- Sorting Page** as shown above.

The `Unsorted` option is primarily intended to preserve the order of imported lists.  In some cases, for example printing job packets for distrubution, you may want to keep duplicate entries in the list.  Enable the option `Keep duplicates` to do so.

The `Dependency` option is useful in conjunction with the `Update part copy` command.  It is intended to help eliminate the tedious `model out-of-date` (dark gray corners) on drawings. 

Dependency ordering is not fool proof.  It has trouble with mutual dependencies, such as Interpart copies.  I've had some luck simply running `Update part copy` twice in a row.

The `Random sample` option randomly selects and shuffles  a fraction of the total files found.  The `Sample fraction` is a decimal number between `0.0` and `1.0`. This option is primarily intended for software testing, but can be used for any purpose.

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskSetDocumentStatus.png"> Document Status Options</h3></summary>

If you use the document Status functionality, you know that some settings place the file in read-only mode. These cannot normally be processed by Housekeeper.

You can get around this by checking `Process files as Available regardless of document Status`. Set the option on the **Configuration Tab -- Status Page** as shown below.

![Document Status](My%20Project/media/configuration_tab_status_page.png)

After processing, you can choose to change the Status back to the old value, or pick a new one. In the example, I decided to change everything to Available. You can select the new Status by clicking the appropriate button in the table. For instance, if you wanted to convert all Baselined files to Released, you would click the last button on the second row.

If you want simply to change the Status on a batch of files, rather than using this option, run the `Change Document Status` task instead.  That command runs in Microsoft's Structure Storage mode and is 100x to 400x faster than Solid Edge.

If you don't need to worry about document Status for your current task, it's not a bad idea to disable the `Process files as Available` option. That's because, when enabled, it has to open the file in Structured Storage before and after the run.  That shouldn't hurt anything, but opening and closing files unnecessarily isn't the greatest idea in the world.

</details>

</details>


<details><summary><h2 style="margin:0px; display:inline-block"><img src="Resources/icons8-filter-16.png"> FILTERING</h2></summary>

Filters are a way to refine the list of files to process.  You can filter on file properties, or filenames (with a wildcard search). They can be used alone or in combination.

![Filter Toolbar](My%20Project/media/filter_toolbar.png)

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/icons8-search-16.png"> Property Filter</h3></summary>

The property filter allows you to select files by their property values. Prior to using this tool, you first need to populate properties from your templates.  Please refer to instructions in the [<ins>**Configuration Tab -- Templates Page**</ins>](#templates-page) section of this Readme.

To configure a property filter, click the tool icon ![Configure](Resources/icons8_Tools_16.png) to the right of the Property filter checkbox. 

The Property Filter checks Draft files, but they often don't have properties of their own. For those files, Housekeeper can also search any models the drawing contains for the specified properties. Set the option on the [<ins>**Configuration Tab -- General Page**</ins>](#general-page). One situation where you might want to disable this option is when searching for file Status. See [<ins>**Document Status Options**</ins>](#document-status-options) below.

This is a powerful tool with a lot of options. These are detailed below.

<p align="center">
  <img src="My%20Project/media/property_filter.png">
</p>

<details><summary><h4 style="margin:0px; display:inline-block">Composing a Filter</h4></summary>

Compose a filter by defining one or more **Conditions**, and adding them one-by-one to the list. A **Condition** consists of a **Property**, a **Comparison**, and a **Value**. For example, `Material contains stainless`, where `Material` is the **Property**, `contains` is the **Comparison**, and `stainless` is the **Value**.

Any number of Conditions are allowed for a filter. The filters can be saved, modified, and deleted.  The tools to do so are in the **Toolbar -- Saved Setting Group**.

</details>

<details><summary><h4 style="margin:0px; display:inline-block">Property Set</h4></summary>

In addition to entering the `Property name`, you must also specify the `Property set`, either `System` or `Custom`.

`System` properties are in every Solid Edge file. They include `Material`, `Project`, etc. Some are in English only, others are localized.

`Custom` properties are ones that you create, probably in a template. Solid Edge also creates some Custom properties for you. These include `Exposed Variables` and output from the `Inspect > Physical Properties` command. The custom property names can be in any language. (In theory, at least -- not tested at this time. Not sure about the Solid Edge entries either.)

Template properties will know their `PropertySet`.  Unless you added it manually, that is, in which case you specify it yourself.

</details>

<details><summary><h4 style="margin:0px; display:inline-block">Comparison</h4></summary>

Select the Comparison from its dropdown box. The choices are `contains`, `is_exactly`, `wildcard_match`, `regex_match`, `>`, or `<`. The options `is_exactly`, `>`, and `<` are hopefully self-explanatory.

`Contains` means the **Value** can appear anywhere in the property. For example, if you specify `Aluminum` and a part file has `Aluminum 6061-T6`, you will get a match. Note, at this time, all Values (except see below for dates and numbers) are converted to lower case text before comparison. So `ALUMINUM`, `Aluminum`, and `aluminum` would all match.

`Wildcard_match` searches for a match with a wildcard pattern. For example `[bfj]ake` would match `bake`, `fake`, and `jake`. A more familiar example might be `Aluminum*`, which would match `Aluminum 6061-T6`, `Aluminum 2023`, etc. Unlike `contains`, in this example, `Cast Aluminum Jigplate` would *not* match because it doesn't start with `Aluminum`. (`*Aluminum*` *would* match, by the way.)

Internally the [<ins>**VB Like Operator**</ins>](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator) is used to make the wildcard comparison.  Visit the link for details and examples.

`Regex_match` uses Regular Expressions.  They are flexible and powerful, but explaining them is beyond the scope of this document. For more information see [<ins>**REGEX in .NET**</ins>](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference), or better yet find a programmer who uses them daily.  That's what I do.

</details>

<details><summary><h4 style="margin:0px; display:inline-block">Filter Formula</h4></summary>

**Default Formula**

Each Condition is assigned a variable name, (`A`, `B`, `...`). The default filter formula is to match all conditions (e.g., `A AND B AND C`).

In the image above, sticking with the default formula means you would get all parts in project 7481 made out of Stainless and engineered by Fred, i.e., `A AND B AND C`.

**Editing the Formula**

You can optionally change the formula. Click the `Edit Formula` button in the toolbar and type the desired expression. For example, if you wanted all parts from Project 7481, **either** made out of Stainless, **or** engineered by Fred, you would enter the formula shown, 
i.e., `A AND (B OR C)`.

**One More Thing About the Formula**

Prior to Housekeeper V2024.2, the formula, a boolean expression, was parsed and evaluated using the .NET built-in `VBScript` engine.  Oversimplifying a bit, that is no longer supported on 64-bit operating systems.

The program now uses `PowerShell` to do the evaluation.  The communication code is built into .NET, but `PowerShell` itself has to be installed separately.  On recent versions of Windows (maybe back to Win7, not sure) it comes bundled with the OS, so it shouldn't be an issue.

If there is an issue, like `PowerShell` isn't installed, you won't get an error message.  Instead, you'll get no files matching your property search.  If you know that's wrong, please mention it on the [<ins>**Solid Edge Forum**</ins>](https://community.sw.siemens.com/s/topic/0TO4O000000MihiWAC/solid-edge).  There may be other more .NET-native ways of evaluating the formula.

</details>

<details><summary><h4 style="margin:0px; display:inline-block">Dates and Numbers</h4></summary>

Dates and numbers are converted to their native format when possible. This is done to obtain commonsense results for `<` and `>`. Note the conversion is attempted even if the property type is `TEXT`, rather than `NUMBER`, `DATE`, or `YES/NO`.

Dates take the form `YYYYMMDD` when converted. This is the format that must be used in the `Value` field. So if you wanted all files dated before January 1, 2022, your condition would be `Custom.Date < 20220101`. The conversion is supposed to be locale-aware, however this has not been tested. Please ask on the Solid Edge Forum if it is not working correctly for you.

Numbers are converted to floating point decimals. In Solid Edge many numbers, in particular those from the variable table, include units. These must be stripped off by the program to make comparisons. Currently only distance and mass units are checked (`in`, `mm`, `lbm`, `kg`). It`s easy to add more, so please ask on the Forum if you need others.

</details>

<details><summary><h4 style="margin:0px; display:inline-block">Document Status</h4></summary>

You can select files based on Status, but not like this:
	
`System.Status contains Available`

There is a number associated with each Status value. You have to use that instead of the name.  

Here is a way to get all `Available` files: `System.Status is_exactly 0`

For *everything but* `Available` you could use: `System.Status > 0`

Here's the list:

- `0 Available`
- `1 InWork`
- `2 InReview`
- `3 Released`
- `4 Baselined`
- `5 Obsolete`

As mentioned above, this is a situation where the option `Include Draft file model documents in search` can yield confusing results. For example, an `InWork` Draft file containing a `Released` part would appear in a search for `Released` documents.

</details>

<details><summary><h4 style="margin:0px; display:inline-block">Saved Settings</h4></summary>

The filters are saved in `property_filter_saved_settings.json` in the `Preferences` directory under the root directory of `Housekeeper.exe`. If desired, you can create a master copy of the file and share it with others. 

</details>

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/wildcard_17.png"> File Name Filter</h3></summary>

The wildcard filter operates on file names. Simply enter the wildcard pattern in the provided combobox.  Wildcard patterns are automatically saved for future use.  Delete a pattern that is no longer needed by selecting it and clicking ![Draft](Resources/icons8_Close_Window_16.png).
 
As suggested above, see [<ins>**VB Like Operator**</ins>](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator) for details and examples.

</details>

</details>

<details><summary><h2 style="margin:0px; display:inline-block"><img src="Resources/icons8_Circled_Play_16.png"> PROCESSING</h2></summary>


![Tabs](My%20Project/media/status_bar_running.png)

Press the Process button to start executing the chosen tasks. If one or more files on the list were selected, only those are processed. Otherwise, all files are processed.

A checkbox ![Error](Resources/icons8_unchecked_checkbox_16.png) to the left of the file name indicates it has yet to be processed. Afterwards, if no errors were detected, a checkmark ![Error](Resources/icons8_Checked_Checkbox_16.png) is shown. Otherwise, an error indicator ![Error](Resources/icons8_Error_16.png) is displayed.

You can monitor progress on the status bar.  It shows the number of files processed, the current file, and an estimate of time remaining.
 
You can interrupt the program before it finishes. As shown above, while processing, the Cancel button changes to a Stop button.  Just click that to halt execution.  It may take several seconds to register the request.  It doesn't hurt to click it a couple of times.

To save some time, you can process files in the background, without graphics.  This capability is somewhat experimental; let me know if you run into problems.  To save some space on Solid Edge's Most Recently Used list, you can disable adding files that are processed by Housekeeper.  Both options are set on the [<ins>**Configuration Tab -- General Page**</ins>](#general-page).

</details>

<details><summary><h2 style="margin:0px; display:inline-block"><img src="Resources/icons8_list_view_16.png"> TASK TAB</h2></summary>

The Task Tab is where you choose what operations to perform.

<p align="center">
  <img src="My%20Project/media/sheetmetal_done.png">
</p>

<details><summary><h3 style="margin:0px; display:inline-block">Task Controls</h3></summary>

To enable a task, click its left-most checkbox.  If it has options, they will appear when the task is selected.  You can hide the options by clicking ![Collapse](Resources/collapse.png) (Collapse).  If you don't want the options to automatically appear, enable `Only show options manually` at the bottom of the Options pane.

When a task is selected, the applicable file types are automatically enabled.  This is indicated by the four other checkboxes on the task's header row.  You can de-select any you don't want to process.

To open the task's help page, click ![Help](Resources/icons8_help_16.png) on the right side of its header row.  There you can learn what the task does and details about any options it has.

</details>

<details><summary><h3 style="margin:0px; display:inline-block">General Controls</h3></summary>

The row at the top of the task list has buttons that operate on all tasks.  Click the left-most checkbox to disable all.  Click ![Collapse](Resources/collapse.png) (Collapse All) to hide options for all selected tasks.  

The remaining four buttons toggle file type selection.  In order, they are ![Assembly](Resources/ST9%20-%20asm.png) (Assembly), ![Part](Resources/ST9%20-%20par.png) (Part), ![Sheetmetal](Resources/ST9%20-%20psm.png) (Sheetmetal), and ![Draft](Resources/ST9%20-%20dft.png) (Draft).  

On the far right ![Help](Resources/icons8_help_16.png) brings up general help for the task tab.

</details>

<details><summary><h3 style="margin:0px; display:inline-block">Customizing</h3></summary>

You can customize the list.  To do so, click `Edit Task List` at the bottom of the form.  The following dialog will appear.

<p align="center">
  <img src="My%20Project/media/edit_task_list.png">
</p>

The left pane shows all available tasks.  The right pane shows the ones currently in use.  To reposition a task in the list, select it and click `Move up` or `Move down`.  To remove one, select it and click `Remove`.  

To add a task, select one from `AVAILABLE TASKS` and click `Add`.  You can have multiple copies of the same task.  This is handy for many situations.  For example, if you have a printer for small drawings and a plotter for large ones, you can place two `Print` tasks on the list and configure them accordingly.

Each task must have a unique name.  Rename one by double-clicking it in the list.  You can rename all of them, for example in your own language, if desired.

The tasks are color-coded.  Change the color by selecting the task, right-clicking, then selecting `Change color`.  I'm not kidding that the task needs to be selected *before* right-clicking.  Otherwise for some reason the task at the top of the list is modified.  Anyway, you can change hue, saturation and brightness.  A preview of your choices is provided on the dialog.

To save the changes, click `OK`, `Cancel` otherwise.  To start over with the task list, delete the file `task_list.json` in Housekeeper's Preferences directory.  Note, in doing so you will also lose any customizations you did, such as duplicated tasks, colors, etc.

Speaking of `task_list.json`, like any other file in the Preferences directory, you can share your customized version with others.  Just copy it into their Preferences directory.

</details>

<details><summary><h3 style="margin:0px; display:inline-block">Presets</h3></summary>

Presets are a way to capture any setup changes you make in the course of using the program. 

![Tabs](My%20Project/media/presets.png)

Presets help you perform recurring tasks you encounter as part of your job.  An example might be releasing a project.  You probably need to make sure files are up to date, parts have drawings, output files have been generated, etc., etc.  Each step takes a certain amount of setup, for example configuring a property filter, selecting tasks, tweaking options, and so on. 

Using Presets, you can capture that work one time.  The next time it comes up, choose the appropriate Preset and you're ready to go.  It saves a bit of time, but more importantly it can cut down on costly mistakes and delays.

To create a Preset, set up the program as desired, enter a name then click Save.  To load one, select it from the drop down, then click Load.  To delete one, select it and click Delete.  To make a change to an existing preset, load it, make the changes, then save.

To capture the task settings themselves (which you almost always want), before saving, be sure to enable the `Remember selected tasks between sessions` option on the **Configuration Tab -- General Page**.

Creating a baseline Preset, while not a requirement, can be helpful.  It's a known starting point for creating new ones.  It's also a quick, reliable way to set back up for everyday use of the program.

For commands with variable information, such as the output directory for `Save As`, you can leave that field blank when creating the Preset.  That way, if you accidentally click Process before setting it, the program will stop and prompt you for the missing information.

</details>

</details>

<details><summary><h2 style="margin:0px; display:inline-block"><img src="Resources/Support_16.png"> CONFIGURATION TAB</h2></summary>


Global options for the program are set on this tab.

<details><summary><h3 style="margin:0px; display:inline-block">Top Level Assembly Page</h3></summary>

See the [<ins>**Top Level Assembly**</ins>](#select-by-top-level-assembly) section.

</details>

<details><summary><h3 style="margin:0px; display:inline-block">Status Page</h3></summary>

See the [<ins>**Document Status**</ins>](#document-status-options) section.

</details>

<details><summary><h3 style="margin:0px; display:inline-block">Sorting Page</h3></summary>

See the [<ins>**Sorting**</ins>](#sorting) section.

</details>

<details><summary><h3 style="margin:0px; display:inline-block">Templates Page</h3></summary>

Templates and related tools.

<p align="center">
  <img src="My%20Project/media/configuration_tab_templates_page.png">
</p>

Description of tab controls:

- `Assembly`, `Part`, `Sheetmetal`, `Draft`, `Material Table`
Buttons to let you specify template locations.
- `Update`
This is where you pre-populate properties for use in dialogs that need them.  See below.
- `Customize`
There are a lot of properties in a Solid Edge file.  This is where you decide which ones to see and in what order.  

<p align="center">
  <img src="My%20Project/media/customize_property_list.png">
</p>

The **Available Properties** are populated by clicking the `Update` button.  You then use the `Customize` dialog to add, remove, and rearrange as needed.  If you need a property not in the template, right-click the **Selected Properties** list and add it manually.

</details>

<details><summary><h3 style="margin:0px; display:inline-block">Server Query Page</h3></summary>

This is where you configure your server and query.

<p align="center">
  <img src="My%20Project/media/server_query.png">
</p>

Execute the query with the syntax `%{Server.Query|Rx}`.

Replace `x` in `|Rx` to retrieve field `x` in the query results.  Only one server and one query are supported at the moment.  

Example: `%{Server.Query|R2}` will return the second field in the record returned by the query. (If more than one record is returned, fields will come from the first one)


</details>

<details><summary><h3 style="margin:0px; display:inline-block">General Page</h3></summary>

Everything else.

<p align="center">
  <img src="My%20Project/media/configuration_tab_general_page.png">
</p>

Description of tab controls:

- `Use current Solid Edge session (if any)`
Normally Housekeeper will not start if Solid Edge is open.  This is to protect you in case opening a file causes an exception in Solid Edge.  This can happen with a corrupted file and other situations.  If it does, the program closes and reopens Solid Edge, causing any unsaved changes to be lost.  Enabling this option bypasses that check.
- `Warn me if file save is required`
This is a way for me to wag my finger one last time, reminding you to back up files before using the program.  It is enabled by default.  It's very annoying and you'll want to turn it off.  Just don't forget, I told you to make backups!
- `Do not show processed files in Most Recently Used List`
This keeps from clogging up Solid Edge's file list with those processed in batch mode.
- `Update list after this many files`
This controls the file list update frequency.  Setting it to `1` means update the list after each file is processed.  This is normally what you want.  However, you may want to increase it for Structured Storage mode, where updating the list is sometimes the most time-consuming part.
- `File list font size`
This allows you to change the font size of the file list.
- `Remember selected tasks between sessions`
When you close out of Housekeeper, it records the state of all settings, such as template locations, etc.  Normally that's what you want, but not necessarily for task selection.  This lets you decide how to handle that.
- `Property Filter -- Include Draft file model in search`
For details, see the [<ins>**Property Filter**</ins>](#1-property-filter) section
- `Property Filter -- Include the Draft file itself in search`
For details, see the [<ins>**Property Filter**</ins>](#1-property-filter) section
- `Check for newer version at startup`
Uses GitHub's API to get the most recent version and compare it to the running version.  Not everyone wants programs to access outside resources like that.  Feel free to disable it if desired; it won't hurt anything.

</details>

</details>


<!-- Everything below this line is auto-generated.  Do not edit. -->
<!-- Start -->

<details><summary><h2 style="margin:0px; display:inline-block"><img src="Resources/SE_asm.png">TASK DETAILS</h2></summary>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskSetDocumentStatus.png">Set document status</h3></summary>

Sets document status. Select the new status on the Options pane. 

![SetDocumentStatus](My%20Project/media/task_set_document_status.png)

Because certain status settings make the file read-only, the command only runs in Structured Storage mode (i.e. without Solid Edge). 

To eliminate potential confusion, it cannot run with the `Process as available` option on the **Configuration Tab -- Status Page**. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskOpenSave.png">Open save</h3></summary>

Opens a document and saves in the current version.

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskActivateAndUpdateAll.png">Activate and update all</h3></summary>

Loads all assembly occurrences' geometry into memory and does an update. Used mainly to eliminate the gray corners on assembly drawings. 

Can run out of memory for very large assemblies.

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskUpdateMaterialFromMaterialTable.png">Update material from material table</h3></summary>

Checks to see if the part's material name and properties match any material in your material table. 

![UpdateMaterialFromMaterialTable](My%20Project/media/task_update_material_from_material_table.png)

If the names match, but their properties (e.g., density, face style, etc.) do not, the material is updated. If no match is found, or no material is assigned, it is reported in the log file.

There are several options for this command. 

- `Remove face style overrides`:  Change all face styles to match that of the material. 
- `Update face styles`:  Disabling this option leaves faces unchanged. 
- `Finish property determines face style`:  Uses the finish rather than the material face style. Note a face style with the same name as the finish must be present in the file. 
- `Finish property`: The property that contains finish information. Note the syntax required in the image above. Right click the text box to select it from a list. 
- `Finishes that don't change material appearance`: Enter these in the list provided. Note no action is taken with these finishes, so their face styles do *not* need to be present in the file. 
- `Override the Body face style`: Uses Part Painter to change the faces. 
- `Override the Material face style`: Uses the Material Table to change the faces. Note this option is currently disabled. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskUpdatePartCopies.png">Update part copies</h3></summary>

In conjuction with `Assembly Activate and update all`, used mainly to eliminate the gray corners on assembly drawings. 

![UpdatePartCopies](My%20Project/media/task_update_part_copies.png)

You can optionally update the parent files recursively by enabling `Update parent documents` on the Options panel.

In many situations, only a few parts have part copies. Using this command with the Dependency Sort option can filter out those files, greatly speeding up processing. See details on the [<ins>**Configuration Tab -- Sorting Page**</ins>](#sorting).

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskUpdatePhysicalProperties.png">Update physical properties</h3></summary>

Updates mass, volume, etc.  Models with no assigned density are reported in the log file. 

![Update Physical Properties](My%20Project/media/task_update_physical_properties.png)

You can optionally control the display of the physical properties symbols. They can either be shown, hidden, or left unchanged. To leave their display unchanged, disable both the `Show` and `Hide` options. 

Occasionally, the physical properties are updated correctly, but the results are not carried over to the Variable Table. The error is detected and reported in the log file. One fix that often works is to open the file in SE, change the material, then change it back. To see if it worked, run `Inspect > Physical Properties`, then check for `Mass` in the Variable Table. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskUpdateModelSizeInVariableTable.png">Update model size in variable table</h3></summary>

Copies the model size to the variable table. 

![UpdateModelSizeInVariableTable](My%20Project/media/task_update_model_size_in_variable_table.png)

This is primarily intended for standard cross-section material (barstock, channel, etc.), but can be used for any purpose. Exposes the variables so they can be used in a callout, parts list, or the like. 

The size is determined using the built-in Solid Edge `RangeBox`. The range box is oriented along the XYZ axes. Misleading values will result for parts with an off axis orientation, such as a 3D tube. 

The size can be reported as `XYZ`, or `MinMidMax`, or both. `MinMidMax` is independent of the part's orientation in the file. Set your preference on the Options panel. Set the desired variable names there, too. 

Note that the values are non-associative copies. Any change to the model will require rerunning this command to update the variable table. 

The command reports sheet metal size in the formed state. For a flat pattern, instead of creating new variables using this command, you can use the variables already created by the flat pattern command -- `Flat_Pattern_Model_CutSizeX`, `Flat_Pattern_Model_CutSizeY`, and `Material Thickness`. If you have an entry in the Material Table Gage Properties Tab `Sheet Metal Gage` combobox, you can use that instead of (or along with) `Material Thickness`. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskUpdateDesignForCost.png">Update design for cost</h3></summary>

Updates DesignForCost and saves the document.

An annoyance of this command is that it opens the DesignForCost Edgebar pane, but is not able to close it. The user must manually close the pane in an interactive Sheetmetal session. The state of the pane is system-wide, not per-document, so closing it is a one-time action. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskUpdateDrawingViews.png">Update drawing views</h3></summary>

Checks drawing views one by one, and updates them if needed.

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskUpdateFlatPattern.png">Update flat pattern</h3></summary>

Updates flat patterns. If the update was not successful, or no flat patterns were found, it is reported in the log file. 

Before updating the flat pattern, this command first regenerates the flat *model*. That is the parent geometry of the flat pattern. If you have an automated model-to-laser pipeline, you may have noticed that sometimes an exported flat model contains no geometry. This is a fix for that situation.

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskBreakLinks.png">Break links</h3></summary>

Breaks external links to a file.  This is irreversible, so you know, think about it. 

![BreakLinks](My%20Project/media/task_break_links.png)

The command options are explained below. 

`Break part copy design links` and `Break part copy construction links` remove links created with the `Part Copy` command. The geometry remains intact.

`Break Excel links` removes Excel references from `Variable` and `Dimension` formulas. In both cases, the value remains as it was before the link was removed.

`Break all interpart links` is the sledgehammer option. It removes the links cited above. It also removes `included links` in profiles and `pasted links` in the variable table. It might do more.  The complete API documentation (below) is, uh, short on details. 

![Break all interpart links](My%20Project/media/break_all_interpart_links_documentation.png)

`Break draft model links` converts drawing views to 2D, removing external references in the process. In testing it quickly became apparent that this operation also converts Property text to blank lines in Callouts. 

![Title Block](My%20Project/media/title_block.png)

Luckily, Solid Edge can take care of that. That's in the program, but only for Callouts. If you have TextBoxes, Blocks, or other objects that use Property text, let me know. I can try to address those in a future release. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskEditProperties.png">Edit properties</h3></summary>

Searches for text in a specified property and replaces it if found. 

![EditProperties](My%20Project/media/task_edit_properties.png)

The property, search text, and replacement text are entered on the Input Editor. To activate the editor click the `Edit` button in the options panel. 

![Find_Replace](My%20Project/media/property_input_editor.png)

This is a powerful tool with a lot of options.  These are detailed below. 

**Using the Input Editor**

Before using this command, you must pre-populate property choices from your templates. To do so, on the [<ins>**Configuration Tab -- Templates Page**</ins>](#templates-page), select your templates and click the `Update` button. There are a lot of properties.  After the update is complete, the `Customize` dialog appears. Choose which to make available there. If you need a property that is not in your templates, right-click the Favorites pane and click `Add property manually`. To access properties not in your Favorites, enable the `Show All Props` option on the toolbar. 

A `Property set`, either `System` or `Custom`, is required. The program will normally set the correct choice automatically. One exception is if you have a custom property with the same name as a system property. In that case, you have to select the appropriate one yourself. For more information about `Property sets`, see the [<ins>**Property Filter**</ins>](#1-property-filter) section in this README file. 

There are five search modes, `PT`, `WC`, `RX`, `EX`, and `X`. 

- `PT` stands for 'Plain Text'.  It is simple to use, but finds literal matches only. 
- `WC` stands for 'Wild Card'.  You use `*`, `?`  `[charlist]`, and `[!charlist]` according to the VB `Like` syntax. 
- `RX` stands for 'Regex'.  It is a more comprehensive (and notoriously cryptic) method of matching text. Check the [<ins>**.NET Regex Guide**</ins>](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference) for more information.
- `EX` stands for 'Expression'.  It is discussed below. 
- `X` isn't really a search mode.  It means delete the chosen property. 

The properties are processed in the order in the table. To change the order, select a row and, on the toolbar `Row Tools` group, click the `Up` or `Down` arrow. The `Delete` button removes the selected row. 

You can save a setup for future use. In the toolbar `Saved Settings` group, enter the name and click `Save`. To retrieve a setting, click the down arrow and select it. To remove a setting, select it and click `Delete`. 

**Case Sensitivity**

The search *is not* case sensitive, the replacement *is*. For example, say the search is `aluminum`, the replacement is `ALUMINUM`, and the property value in a file is `Aluminum 6061-T6`. Then the new value would be `ALUMINUM 6061-T6`. 

**Property Substitution**

In addition to plain text and pattern matching, you can also do property substitution.   The example in the Input Editor above is telling the program to use the file name for the Document Number. To select a property, right-click the `Find` or `Replace` field and select `Insert property`. You can also type it in if preferred.  The formula has the same syntax as the Callout command, except preceeded with `System.` or `Custom.` as shown in the example. 

**Options**

If the specified property does not exist in the file, you can optionally add it by enabling `Add any property not already in file`. Note, this only works for `Custom` properties.  Adding `System` properties is not allowed. 

To delete a property, set the Find Search to `X`. As above, this only works for `Custom` properties. 

If you are changing `System.Material` specifically, you can also update the properties associated with the material itself. Select the option `For material, update density, face styles, etc.`. For more options to control the material updates, take a look at the command `Update material from material table`.Note this option is not currently compatible with `Run task without Solid Edge`. 

**Expressions**

An `expression` is similar to a formula in Excel. Expressions enable more complex manipulations of the `Replace` string. To create one, click the `Expression Editor` button on the input editor form. 

![Expression Editor](My%20Project/media/expression_editor.png)

You can perform string processing, create logical expressions, do arithmetic, and, well, almost anything.  The available functions are listed below. Like Excel, the expression must return a value.  Nested functions are the norm for complex manipulations. Unlike Excel, multi-line text is allowed, which can make the code more readable. 

You can check your expression using the `Test` button. If there are undefined variables, for example `%{Custom.Engineer}`, it prompts you for a value. When you are satisfied with your expression, dismiss the dialog by clicking the `X` on the upper right. The expression will be copied to the clipboard.  Click in the desired Replace text box and type CTRL-V. 

You can `Save` or `Save As` your expression with the buttons provided. Retreive them with the `Saved Expressions` drop-down. That drop-down comes with a few examples. You can study those to get the hang of it. To learn more, click the `Help` button.  That opens a web site with lots of useful information, and links to more. 

Available functions

`concat()`, `contains()`, `convert()`, `count()`, `countBy()`, `dateAdd()`, `dateTime()`, `dateTimeAsEpoch()`, `dateTimeAsEpochMs()`, `dictionary()`,`distinct()`, `endsWith()`, `extend()`, `first()`, `firstOrDefault()`, `format()`, `getProperties()`, `getProperty()`, `humanize()`, `if()`, `in()`, `indexOf()`, `isGuid()`, `isInfinite()`, `isNaN()`, `isNull()`, `isNullOrEmpty()`, `isNullOrWhiteSpace()`, `isSet()`, `itemAtIndex()`, `jObject()`, `join()`, `jPath()`, `last()`, `lastIndexOf()`, `lastOrDefault()`, `length()`, `list()`, `listOf()`, `max()`, `maxValue()`, `min()`, `minValue()`, `nullCoalesce()`, `orderBy()`, `padLeft()`, `parse()`, `parseInt()`, `regexGroup()`, `regexIsMatch()`, `replace()`, `retrieve`, `reverse()`, `sanitize()`, `select()`, `selectDistinct()`, `setProperties()`, `skip()`, `Sort()`, `Split()`, `startsWith()`, `store()`, `substring()`, `sum()`, `switch()`, `take()`, `throw()`, `timeSpan()`, `toDateTime()`, `toLower()`, `toString()`, `toUpper()`, `try()`, `tryParse()`, `typeOf()`, `where()`

**Run Task Without Solid Edge (Experimental)**

This option opens the file with Windows Structured Storage, instead of Solid Edge. It's *blazingly* fast -- 100x to 400x faster than Solid Edge. If you want to try this out, select the option `Run task without Solid Edge`. 

Note, Solid Edge presents exposed variables as Custom properties.  You can change those with this command, but Solid Edge will overwrite them the next time the file is opened. For those, rather than using this command, use `Edit Variables` instead. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskEditVariables.png">Edit variables</h3></summary>

Adds, changes, and/or exposes variables. 

![EditVariables](My%20Project/media/task_edit_variables.png)

The information is entered on the Input Editor. Access the form using the `Edit` button. 

![Variable_Editor](My%20Project/media/variable_input_editor.png)

The Variable name is required.  There are restrictions on the name.  It cannot start with a number.  It can only contain letters and numbers and the underscore `_` character.

If a variable on the list is not in the file, it can optionally be added.  Set the behavior on the Options panel. 

The number/formula is not required if only exposing an existing variable, otherwise it is.  If a formula references a variable not in the file, the program will report an error.

You can use a file property in the formula.  Right-click the formula text box and select Insert Property to do so.  Note, the property is a static copy.  If it changes, this command needs to be run again to update the variable.  

If exposing a variable, the Expose name defaults to the variable name. You can optionally change it.  The Expose name does not have restrictions like the variable name. 

The variables are processed in the order in the table. To change the order, select a row and, on the toolbar `Row Tools` group, click the `Up` or `Down` arrow. The `Delete` button removes the selected row. 

You can save a setup for future use. In the toolbar `Saved Settings` group, enter the name and click `Save`. To retrieve a setting, click the down arrow and select it. To remove a setting, select it and click `Delete`. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskEditInteractively.png">Edit interactively</h3></summary>

Brings up files one at a time for manual processing. 

![EditInteractively](My%20Project/media/task_edit_interactively.png)

A dialog box lets you tell Housekeeper when you are done. You can save the file or not, or choose to abort.  Aborting stops processing and returns you to the Housekeeper main form.  

![Edit Interactively Dialog](My%20Project/media/edit_interactively_dialog.png)

You choose the dialog's starting position. `X` and `Y` are the number of pixels from the left and top of the screen, respectively. If you move the dialog, it remembers the location for subsequent files. It doesn't remember between runs, unfortunately. That turns out to be surprisingly complicated. As a workaround, a change in position is reported on the form. Once processing is complete, you can enter the reported values in the command options. Those numbers *are* remembered between runs. 

You can optionally set a countdown timer and/or start a command. 

**Countdown Timer**

The countdown timer lets you run hands-free. This can be handy for doing a quick inspection of a batch of files, for example. If something catches your eye, you can pause the timer.  There is an option, `Save file after timeout`, that tells Housekeeper how to proceed if the timer runs to completion.  

**Start Command**

The `Start command` option launches a command when the file opens.  This can help keep you on track when you have a small chore to complete on a bunch of files.  For example: 

- Enable the `Update file on save` option on the `Physical Properties` dialog. 
- Cycle through assembly `display configurations` to make sure they are all set correctly. 

The dropdown list contains commands that we thought might be useful.  The first entry on the list, `Manual entry` is a special case.  It instructs the program to execute the `Command ID` entered in the textboxes below the dropdown. If you don't want a command to start for a given file type, enter `0` in the textbox. 

You can customize the list.  Instructions to do so are in the file `interactive_edit_commands.txt` in the Housekeeper `Preferences` directory. It also shows how to find commands and their corresponding ID numbers. Hundreds of commands are available.  It's worth checking out. Note, the file is created the first time you run Housekeeper. It won't be there prior to that. 

**Rules for Interactive Editing**

Some rules for this command apply. It is important to leave Solid Edge in the state you found it when the file was opened. For example, if you open another file, such as a drawing, you need to close it. If you add or modify a feature, you need to click Finish. If you used the `Start command` option, you need to close any dialog opened in the process. 

Also, do not `Close` or `Save As` the file being processed. Housekeeper maintains a `reference` to the file. Those two commands cause the reference to be lost, resulting in an error. 

One last thing.  Macros interact with Solid Edge through something called the Windows Component Object Model.  That framework appears to have some sort of built-in inactivity detection.  If you let this command sit idle for a period of time, COM reports an error. It doesn't really hurt anything, but Housekeeper stops and restarts SE any time a COM error occurs. I get around it by selecting only a small number of files to work on at a time. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskUpdateModelStylesFromTemplate.png">Update model styles from template</h3></summary>

Updates the styles you select from a template you specify. 

![UpdateModelStylesFromTemplate](My%20Project/media/task_update_model_styles_from_template.png)

Using the checkboxes provided, you can update all styles, or select which ones to update individually. 

Styles present in the template, but not in the file, are added. Styles present in the file, but not in the template, can optionally be removed, if possible. Set the option `Remove styles not in template` as needed. It is not possible to remove a style if Solid Edge thinks it is in use (even if it isn't). 

Styles are updated/added as described, but no mapping takes place. For example, if the template has a dimension style ANSI(in), and the file instead uses ANSI(inch), the dimensions will not be updated. A workaround is to create the target style in the template and modify it in that file as needed. 

The active view style of the file is changed to match the one active in the template. Note, it must be a named style.  Overrides are ignored. To create a named style from an override, open the template in Solid Edge, activate the `View Overrides` dialog, and click `Save As`.

![View Override Dialog](My%20Project/media/view_override_dialog.png)

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskUpdateDrawingStylesFromTemplate.png">Update drawing styles from template</h3></summary>

Updates styles and/or background sheets from a template you specify. 

![UpdateDrawingStylesFromTemplate](My%20Project/media/task_update_drawing_styles_from_template.png)

These styles are processed: `DimensionStyles`, `DrawingViewStyles`, `LinearStyles`, `TableStyles`, `TextCharStyles`, `TextStyles`. These are not: `FillStyles`, `HatchPatternStyles`, `SmartFrame2dStyles`. The latter group encountered errors with the current implementation.  The errors were not thoroughly investigated, however. If you need one or more of those styles updated, please ask on the Forum. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskRemoveFaceStyleOverrides.png">Remove face style overrides</h3></summary>

Face style overrides change a part's appearance in the assembly. This command causes the part to appear the same in the part file and the assembly.

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskHideConstructions.png">Hide constructions</h3></summary>

Hides all non-model elements such as reference planes, PMI dimensions, etc.

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskFitView.png">Fit view</h3></summary>

Maximizes the window, sets the view orientation for model files, and does a fit. 

![FitView](My%20Project/media/task_fit_view.png)

Select the desired orientation on the Options panel. The setting is required, but is ignored for draft files.

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskCheckInterference.png">Check interference</h3></summary>

Runs an interference check.  All parts are checked against all others. 

![CheckInterference](My%20Project/media/task_check_interference.png)

This can take a long time on large assemblies, so there is a limit to the number of parts to check. Set it on the Options panel.

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskCheckLinks.png">Check links</h3></summary>

Checks linked files.  

![CheckLinks](My%20Project/media/task_check_links.png)

`Missing links` are files not found on disk.  `Misplaced links` are files not contained in the search directories specified on the **Home Tab**.  Only links directly contained in the file are checked.  Links to links are not.

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskCheckRelationships.png">Check relationships</h3></summary>

Checks if the file has any failed, underconstrained, or suppressed relationships.

![CheckRelationships](My%20Project/media/task_check_relationships.png)

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskCheckFlatPattern.png">Check flat pattern</h3></summary>

Checks for the existence of a flat pattern. If one is found, checks if it is up to date. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskCheckMaterialNotInMaterialTable.png">Check material not in material table</h3></summary>

Checks if the material in the file exists in the material table. 

![CheckMaterialNotInMaterialTable](My%20Project/media/task_check_material_not_in_material_table.png)

The material table is chosen on the Options panel. This command only checks the name.  To check/update its properties (density, face style, etc.), run the `Update material from material table` command. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskCheckMissingDrawing.png">Check missing drawing</h3></summary>

Assumes drawing has the same name as the model, and is in the same directory

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskCheckPartNumberDoesNotMatchFilename.png">Check part number does not match filename</h3></summary>

Checks if the file name contains the part number. 

![CheckPartNumberDoesNotMatchFilename](My%20Project/media/task_check_part_number_does_not_match_filename.png)

Enter the property formula that holds part number on the Options panel. For more information, see the [<ins>**Property Filter**</ins>](#1-property-filter) section in this README file. 

The command only checks that the part number appears somewhere in the file name. If the part number is, say, `7481-12104` and the file name is `7481-12104 Motor Mount.par`, you will get a match. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskCheckPartCopies.png">Check part copies</h3></summary>

If the file has any Part Copies, checks if they are up to date.

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskCheckDrawingPartsList.png">Check drawing parts list</h3></summary>

Checks is there are any parts list in the drawing and if they are all up to date.

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskCheckDrawings.png">Check drawings</h3></summary>

Checks draft files for various problems. 

![CheckDrawings](My%20Project/media/task_check_drawings.png)

The options are: 
- `Drawing views out of date`: Checks if any drawing views are not up to date. 
- `Detached dimensions or annotations`: Checks that dimensions, balloons, callouts, etc. are attached to geometry in the drawing. 
- `Drawing view on background sheet`: Checks background sheets for the presence of drawing views. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskRunExternalProgram.png">Run external program</h3></summary>

Runs an `*.exe` or `*.vbs` or `*.ps1` file. 

![RunExternalProgram](My%20Project/media/task_run_external_program.png)

Select the program with the `Browse` button on the Options panel. 

If you are writing your own program, be aware several interoperability rules apply. See [<ins>**HousekeeperExternalPrograms**</ins>](https://github.com/rmcanany/HousekeeperExternalPrograms) for details and examples. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskSaveModelAs.png">Save model as</h3></summary>

Exports the file to either a non-Solid Edge format, or the same format in a different directory. 

![SaveModelAs](My%20Project/media/task_save_model_as.png)

Select the file type using the combobox. Select the directory using the `Browse` button, or check the `Original Directory` checkbox. 

You can optionally rename the file or create subdirectories using a formula similar to the Property Text Callout. Enable the `Change filename` and/or `Use subdirectory formula` options to do so. To add a property, right-click the text box and select `Insert property`. You can also just type it in if that's easier. You can create nested subdirectories if desired. Simply add `\` in the subdirectory formula. Here are two examples. 
- `Project %{System.Project Name}` 
- `%{System.Material}\%{System.Sheet Metal Gage}` 

As illustrated in the examples, a `Property set`, either `System` or `Custom`, is required. For more information, refer to the [<ins>**Property Filter**</ins>](#1-property-filter) section in this Readme file. 

It is possible that a property contains a character that cannot be used in a file name. If that happens, a replacement is read from `filename_charmap.txt` in the `Preferences` directory in the Housekeeper root folder. You can/should edit it to change the replacement characters to your preference. The file is created the first time you run Housekeeper.  For details, see the header comments in that file. 

Sheetmetal files have two additional options -- `DXF Flat (*.dxf)` and `PDF Drawing (*.pdf)`. The `DXF Flat` option saves the flat pattern of the sheet metal file. 

The `PDF Drawing` option saves the drawing of the sheet metal file. The drawing must have the same name as the model, and be in the same directory. A more flexible option may be to use `Save Drawing As` command, using a `Property Filter` if needed. 

For image file formats there are additional options. You can hide constructions and/or fit the view.  For Fit, choose an orientation, either `Isometric`, `Dimetric`, or `Trimetric`. You can also crop images to the aspect ratio of the model, rather than the window. The option is called `Crop image to model size`. On tall skinny parts cropping works a little *too* well.  You might need to expand the margins a bit in Photoshop for those. Finally, you can change the view style by selecting that option and entering its name in the textbox provided. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskSaveDrawingAs.png">Save drawing as</h3></summary>

Exports the file to either a non-Solid Edge format, or the same format in a different directory. 

![SaveDrawingAs](My%20Project/media/task_save_drawing_as.png)

Most options for this command are the same as for `Save Model As`. See the help topic for that command for details. 

Unlike with model files, draft subdirectory formulas can include an Index Reference designator (eg, `|R1`). This is the way to refer to a model contained in the draft file, similar to Property Text in a Callout. For example, `%{System.Material|R1}`. To refer to properties of the draft file itself, do not specify a designator, for example, `%{Custom.Last Revision Date}`. 

You can optionally include a watermark image on the drawing output file.  For the watermark, set X/W and Y/H to position the image, and Scale to change its size. The X/W and Y/H values are fractions of the sheet's width and height, respectively. So, (`0,0`) means lower left, (`0.5,0.5`) means centered, etc. Note some file formats may not support bitmap output.

When creating PDF files, there are two options, `PDF` and `PDF per Sheet`. The first saves all sheets to one file.  The second saves each sheet to a separate file, using the format `<Filename>-<Sheetname>.pdf`.  You can optionally suppress the `Sheetname` suffix on files with only one sheet.  The option is called `Suppress sheet suffix on 1-page drawings`.  To save sheets to separate `dxf` or `dwg` files, set the Save As Options in Solid Edge for those file types before running this command. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskCreateDrawingOfFlatPattern.png">Create drawing of flat pattern</h3></summary>

Creates a drawing of a flat pattern using the template you specify. If the file does not contain a flat pattern, the command reports an error. It does not check if the flat pattern is up to date. For that, run the `Check flat pattern` and/or `Update flat pattern` commands before running this one. 

![Expression Editor](My%20Project/media/task_create_drawing_of_flat_pattern.png)

The command attempts to place the flat pattern centered on the sheet. It rotates it if needed to match the available space. It scales it to entirely fill the sheet in one direction. That's not the scale you ultimately want, but is the starting point for the `Scale factor`. With that you control the final size. If you want it to take up 90% of the available space, enter `0.9`. For half size, enter `0.5`, etc. X and Y offset will offset the drawing by the specified values from center, +Y will offset up, -Y will offset down. +X will offset right, -X will offset left.

You can save the drawing as a `*.dft` or `*.pdf` or both. If a file with the same name already exists, the `Overwrite existing` checkbox tells the program how to proceed. You can save the drawing to the original directory, or choose another one. Each file type can be saved to a different directory. 

</details>

<details><summary><h3 style="margin:0px; display:inline-block"><img src="Resources/TaskPrint.png">Print</h3></summary>

Prints drawings. 

![Print](My%20Project/media/task_print.png)

The dropdown should list all installed printers. 

If you use more than one printer, use the `Edit task list` function on the **Task Tab** to add one or more Print tasks. Set up each by selecting the printer/plotter, sheet sizes, and other options as desired. 

You assign sheet sizes to a printer with the `Select Sheets` button. Print jobs are routed on a per-sheet basis. So if a drawing has some sheets that need a printer and others that need a plotter, it will do what you expect. 

![Printer_Setup](My%20Project/media/sheet_selector.png)

This command may not work with PDF printers. Try the Save As PDF command instead. 

</details>

</details>


<details><summary><h2 style="margin:0px; display:inline-block"><img src="Resources/icons8_help_16.png"> KNOWN ISSUES</h2></summary>

**The program is not perfect**
- *Cause*: The programmer is not perfect.
- *Possible workaround*: Back up any files before using it.  The program can process a large number of files in a short amount of time.  It can do damage at the same rate.  It has been tested on thousands of our files, but none of yours.  So, you know, back up any files before using it.  

**Does not support managed files**
- *Cause*: Unknown.
- *Possible workaround*: Process the files in an unmanaged workspace.
- *Update 10/10/2021* Some users have reported success with BiDM managed files.
- *Update 1/25/2022* One user has reported success with Teamcenter 'cached' files.

**Some tasks cannot run on older Solid Edge versions**
- *Cause*: Probably an API call not available in previous versions.
- *Possible workaround*: Use the latest version, or avoid use of the task causing problems.

**May not support multiple installed Solid Edge versions**
- *Cause*: Unknown.
- *Possible workaround*: Use the version that was 'silently' installed.

**Pathfinder sometimes blank during Interactive Edit**
- *Cause*: Unknown.
- *Possible workaround*: Refresh the screen by minimizing and maximizing the Solid Edge window.

</details>


<details><summary><h2 style="margin:0px; display:inline-block"><img src="Resources/TaskRunExternalProgram.png"> OPEN SOURCE PACKAGES</h2></summary>

This project uses these awesome open source packages.
- JSON Converter [<ins>**Newtonsoft.Json**</ins>](https://github.com/JamesNK/Newtonsoft.Json)
- Excel reader [<ins>**ExcelDataReader**</ins>](https://github.com/ExcelDataReader/ExcelDataReader)
- Expression engine [<ins>**PanoramicData.NCalcExtensions**</ins>](https://github.com/panoramicdata/PanoramicData.NCalcExtensions)
- Expression editor [<ins>**FastColoredTextBox**</ins>](https://github.com/PavelTorgashov/FastColoredTextBox)
- Structured storage editor [<ins>**OpenMCDF**</ins>](https://github.com/ironfede/openmcdf)
- Icons [<ins>**Icons8**</ins>](https://icons8.com)
</details>


<details><summary><h2 style="margin:0px; display:inline-block"><img src="Resources/Info-16.png"> CODE ORGANIZATION</h2></summary>

Processing starts in Form_Main.vb.  A short description of the code's so-called organization can be found there.

</details>

