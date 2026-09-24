<div class="center">
  <p align=center>
  <img src="My%20Project/media/logo.png" width=50%;>
  <p align=center>
  <span class="description">Robert McAnany 2026</span>
</div>

# Tips and Tricks

This is a compilation of user situations where Housekeeper doesn't seem to be acting right.  The program can be tricky to use in places.  

It can also not act right for real.  In those cases, please raise an issue on [<ins>**GitHub**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/issues).

<details open><summary><h2 style="margin-bottom:-20px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">INSTALLATION AND FIRST TIME USE</h2></summary>

For an overview, see the [<ins>**Installation Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#installation)


<details open><summary><h3 style="margin-bottom:-20px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Released Code</h3></summary>

This section if for those who obtained Housekeeper in a `*.zip` file from the `Releases` page on GitHub.

#### Could not create the Preferences Directory

Downloaded files are frequently blocked or marked read-only.  The program cannot function if so.  Right-click the extracted directory (not the `*.zip` file) and select Properties.  If applicable, click the `Unblock` button and clear the `Read-only` checkbox.

#### Cannot Find Housekeeper.exe

GitHub confusingly adds the source code to the release page.  The source code does not contain the executable.  Verify the downloaded file is called `SolidEdgeHousekeeper-vYYYY.N.zip` and not `Source code.zip`.

#### Could not start Solid Edge.  Exiting...

Check for an error message similar to this:

`Unable to cast COM object System.__ComObject in interface SolidEdgeFramework.Application ... HRESULT:0x8000002 (E_NOINTERFACE)`

That may mean the Solid Edge COM objects have become unregistered.  A quick way to reregister them is to run the latest SE Maintenance Pack.  Worked one time here, anyway.

</details>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Internet Access</h3></summary>

Not everyone wants their programs to access the internet.  There are two places Housekeeper tries.  One is when you click a Help button; the other is when checking for a newer version.

You can disable the version check on the **Configuration Tab -- General Page**.  It is not possible to disable the Help buttons at this time.  However if you don't click one, it will not attempt to open it.

In any case, the program only receives information; it does not send anything.

</details>


<details open><summary><h3 style="margin-bottom:-20px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Cloned Code</h3></summary>

#### Cannot See Files in Solution Explorer

In Visual Studio, `My Project`, where most of the source code resides, sometimes cannot be expanded.  Enabling the `Show All Files` option should fix it.

![](My%20Project/media/tips_solution_explorer.png)

#### Cannot Build the Project

After cloning the repo, a build may fail with errors like:

```
error BC30002: Type 'ListView' is not defined.
error BC30002: Type 'ListViewGroup' is not defined.
warning BC40056: Namespace or type specified in the Imports 'System.Windows.Forms' ... cannot be found.
```

**Cause:** 

Possibly the project file, `ListViewExtended.vbproj`, has a reference that is using a hardcoded *relative* path.  The file can be found under Housekeeper's `My Project\ListViewExtended` directory.

**Fix:**

In the project file, look for something like this (with any number of `..\`):

```
..\..\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\
```

And replace: 
`..\..\Program Files (x86)\Reference Assemblies` 

with: `$(MSBuildProgramFiles32)`

like so: `$(MSBuildProgramFiles32)\Microsoft\Framework\.NETFramework\v4.7.2\`

</details>

</details>


<details open><summary><h2 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">FILE LIST</h2></summary>

For an overview, see the [<ins>**File Selection Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#file-selection)


<details open><summary><h3 style="margin-bottom:-20px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Missing Files</h3></summary>

If you selected a source for files, but no files are displayed, you might simply need to update the list.  The button is on the selection toolbar at the top of the Home page.

![](My%20Project/media/selection_toolbar.png)

If updating doesn't help, here are a couple of other things to try.

**Check file filters**

The filters toolbar is located on the bottom of the Home Page.

![](My%20Project/media/filter_toolbar.png)

- Make sure the File Type filters are set as needed.  These buttons are on the left side of the filter toolbar.
- Check if a Property Filter is active and, if so, configured properly.
- Verify the File Wildcard search, if enabled, is set correctly.

**Check sort options**

The sorting options are on the **Configuration Tab -- Sorting Page**.  `Dependency` and `Random` sort, in particular, can give confusing results if you are unaware they are enabled.

</details>

<details open><summary><h3 style="margin-bottom:-20px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Top Level Assembly</h3></summary>

For an overview, see the [<ins>**Top Level Assembly Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#select-by-top-level-assembly)

#### Top Level Folders

The top-level assembly is the file you want to process.  The top-level folders tell the program where to look for other files.

Say your project has the structure below, and you want to work on Sub_01.asm.  Most of its parts and drawings are in its own directory.  However, it also uses parts from `Hardware` and `Purchased`.

```
    - Project
        + Hardware
        + Purchased
        - Sub_01
            Sub_01.asm
            Part_01-01.par
            ...
        + Sub_02
        ...
```

You would use the highlighted button to select additional top-level folders:

![](My%20Project/media/tips_tla_folders.png)

You could also simply select the parent directory `Project`, however that would also search `Sub_02`, `Sub_03`..., which you may not want.

#### Removing Unneeded Files

Designs evolve, which can lead to abandoned models in your project.  Eventually these need to be cleaned up.  The option `Report unrelated files`, can help.  It is located on the **Configuration Tab -- Top Level Assembly Page**.

Once enabled, update the file list to initiate the search.  If any unrelated files are found, they are presented in a Notepad window.  They can be manually removed, or for a more automatic method, take a look at [<ins>**File List Shortcut Menu Help Topic**</ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#shortcut-menu).

</details>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Importing Lists</h3></summary>

A list contains the names of files to be processed.  It accepts `Excel`, `*.txt`, `*.csv`, and `*.tsv` file types.

The file names must include the full path.  So `C:\Projects\Project123\Part1.dft`, not just `Part1.dft` or `Part1`.

</details>

</details>

<details open><summary><h2 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">FILTERING</h2></summary>

For an overview, see the [<ins>**Filtering Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#filtering)

#### Detecting if a Property Exists in a File

You may need to find files that have a given property, even if you don't care what its value is.

![](My%20Project/media/tips_property_filter.png)

A wildcard match for `*` will match every file that contains that property and exclude all others.  If instead you want every file that *does not* contain the property, use `Edit Formula` to set it to `Not A`.

#### Property Filter Not Finding Files

There are plenty of reasons this could happen.  Here's a common one.

Let's say you wanted to find all files with Document Numbers containing `Client_A-` and `Client_B-`.  You might set up the following property filter.
```
       A System.Document Number contains Client_A-
       B System.Document Number contains Client_B-
       Filter Formula A AND B
```

It finds nothing.  The filter formula is to blame.  In this situation, rather than `A AND B`, you need `A OR B`.

#### Draft Files and Property Filter

This is the options page for Property Filters.  The toolbar button with the wrench icon opens it.  It needs some explaining.

![](My%20Project/media/tips_property_filter_options.png)

First of all, the options only refer to Draft files.  Those often do not have properties of their own.  The first option says to search *model files* in a drawing for properties.  The second says to search the *drawing file itself* for them.  Either or both can be active.  If both, a match occurs if *either* option is true.

The confusing part is that Draft files *actually do* have certain properties of their own.  `%{System.File Name}` and `%{System.Status}`, for example.  This can affect property filtering in subtle ways.  

Here is a short quiz.  See if you can figure out how the options should be set.

Possible answers for each question.
- **A** Include Model files: YES, Include Draft file: NO
- **B** Include Model files: NO, Include Draft file: YES
- **C** Include Model files: YES, Include Draft file: YES

**Questions**
- **1.** You want to find all files with `%{System.Project Name} = 7481`.  That property comes from your models.  It is not populated in drawing files.  
- **2.** Same as above, but you want all files *EXCEPT* `%{System.Project Name} = 7481`.
- **3.** You want to find all `Released` files (ie `%{System.Status} = 5`).  A characteristic of your system is that you can have a `Released` model, while its drawing can still be `In Work`.
- **4.** You want to exclude files that have `SomeText` in the file name.  You set up the filter like so:
```
       A System.File Name contains SomeText
       Filter Formula NOT A
```

**Answers**
- **1.** **A** or **C**.  The Draft files don't have the property, so will never match.  Only the model files matter.
- **2.** **A**.  With the negation, the Draft files matter.  They will always return `False`.  `NOT False` is `True`, meaning you will always get a match.
- **3.** **B**.  If you check the drawing's models, and they are `Released`, you will get a match even if the drawing itself is still `In Work`.
- **4.** Depends.
  - **B** or **C** if the draft and model have the same name.
  - **B** if they don't.

 
 </details>

<details open><summary><h2 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">TASK-SPECIFIC TIPS</h2></summary>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Edit Properties</h3></summary>

For an overview, see the [<ins>**Edit Properties Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#edit-properties)

#### No Properties Available

There is some setup to make your properties available to the program.  See the [<ins>**Templates Page Help Topic**</ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#templates-page) for details.

#### Wildcard Match

Sometimes you want to replace a property value, no matter its current contents.  If it's not working, it may be the setting of the `Find Search Type` (denoted `FS` on the dialog -- see below.)  Make sure it's `WC` and not `PT` or something else.  

#### Change a Custom Property Name

This command is designed to update property values, but you can also change the name of the property itself.

On the Edit Properties option panel (not shown), enable `Add property`.  

In the input editor, assign the value of the old property to the new.  You can then delete the old property as shown on the second row.  (You can delete it in another run of the command if you want to first verify results.)

![Change Name](My%20Project/media/tips_edit_properties_change_name.png)

#### Using a Property Filter

If you are editing a property, there is no reason to process files that do not contain it.  See the tip [<ins>**Detecting if a Property Exists in a File**</ins>](#detecting-if-a-property-exists-in-a-file) for a way to speed things up.

#### Delete Properties

As shown above, the command can delete properties.  That's fine for a couple of them.  

If you have a bunch, there is an external program, [<ins>**AddRemoveCustomProperties**</ins>](https://github.com/rmcanany/HousekeeperExternalPrograms/tree/main/AddRemoveCustomProperties#readme), that may help.  It can run stand-alone on a single file open in SE, or with the `Run External Program` for a batch of them.  

There are two operating modes -- `Remove` and `RemoveAllExcept`.  They are discussed in the link provided.

</details>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Update Material from Material Table</h3></summary>

For an overview, see the [<ins>**Update Material from Material Table Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#update-material-from-material-table)

**Change which Material Table Is Used**

In Solid Edge, the Material Library and Material Table are not the same thing.  The Material Library is a directory.  The Material Table(s) are `*.mtl` files in that directory.

SE doesn't care which Material Table contains the file's material, as long as one does.  Housekeeper is pickier.  You specify the Material table to use on the **Configuration Tab -- Templates Page**.  Then run this command to update the file.

</details>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Save Model/Drawing As</h3></summary>

For an overview, see the [<ins>**Save Drawing As Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#save-drawing-as)

#### Save Multiple File Types

You can configure the Task List with multiple copies of the same command, then configure each as needed.  See the [<ins>**Customizing the Task Tab Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#customizing) for details.

</details>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Print</h3></summary>

For an overview, see the [<ins>**Print Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#print)

#### Send to Printer/Plotter as Needed

As noted above, you can configure the Task List with multiple copies of the same command, then configure each as needed.  The configuration in this case would be to specify the sheet sizes accepted by the selected printer/plotter.

See the [<ins>**Customizing the Task Tab Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#customizing) for details.

</details>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Task Dummy</h3></summary>

</details>

</details>

<details open><summary><h2 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">HOUSEKEEPER EXTERNAL PROGRAMS</h2></summary>

Housekeeper doesn't do *everything*.  That's on purpose.  Some tasks are very narrowly focused, only needed for one-time use, etc.

For those, there is a separate GitHub repo, [<ins>**Housekeeper External Programs**<ins>](https://github.com/rmcanany/HousekeeperExternalPrograms).  It has a bunch of one-off type programs.  You run them with the [<ins>**Run External Program**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#run-external-program) command.

</details>
