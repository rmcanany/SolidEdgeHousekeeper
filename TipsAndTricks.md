<div class="center">
  <p align=center>
  <img src="My%20Project/media/logo.png" width=50%;>
  <p align=center>
  <span class="description">Robert McAnany 2026</span>
</div>

# Tips and Tricks

<details open><summary><h2 style="margin-bottom:-20px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">PROGRAM NOT STARTING</h2></summary>

For an overview, see the [<ins>**Installation Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#installation)

<details open><summary><h3 style="margin-bottom:-20px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Released Code</h3></summary>

This section if for those who obtained Housekeeper in a `*.zip` file from the `Releases` page on GitHub.

#### Verify the Directory Settings

Downloaded files are frequently blocked or marked read-only.  The program cannot function if so.  Right-click the extracted directory (not the `*.zip` file) and select Properties.  Click the `Unblock` button and clear the `Read-only` checkbox.

#### Cannot Find Housekeeper.exe

GitHub confusingly adds the source code to the release page.  It does not contain the executable.  Verify the downloaded file is called `SolidEdgeHousekeeper-vYYYY.N.zip`.  (Where `YYYY` is the year, `N` is the release number.)

#### Could not start Solid Edge.  Exiting...

Check for an error message similar to this:

`Unable to cast COM object System.__ComObject in interface SolidEdgeFramework.Application ... HRESULT:0x8000002 (E_NOINTERFACE)`

That may mean the Solid Edge COM objects have become unregistered.  A quick way to reregister them is to run the latest SE Maintenance Pack.  Worked one time here, anyway.

</details>

<details><summary><h3 style="margin-bottom:-20px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Cloned Code</h3></summary>

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

**Cause:** `ListViewExtended.vbproj` (and the main `Housekeeper.vbproj`) reference .NET Framework
assemblies (`System.Windows.Forms.dll`, `System.dll`) using a hardcoded *relative* path back to:

```
C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\
```

The number of `..\` segments in that path assumes the repo was cloned to a specific folder depth.
If you clone it somewhere deeper or shallower than that (e.g. into a subfolder of `Downloads`
instead of directly under a drive root), the relative path no longer resolves to that folder, so
the reference silently fails to load — which cascades into "type not defined" errors for every
WinForms type.

**Fix — use an MSBuild reserved property instead of a relative path.**

Replace the hardcoded `..\..\..\` segments with `$(MSBuildProgramFiles32)`, which MSBuild always
resolves to `Program Files (x86)` regardless of where the repo is cloned:

```xml
<Reference Include="System.Windows.Forms">
  <HintPath>$(MSBuildProgramFiles32)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Windows.Forms.dll</HintPath>
</Reference>
```

Apply the same change to the `System` reference's `HintPath` in `Housekeeper.vbproj`. Confirmed
by rebuilding the whole solution after the change — no other errors.

**Alternative fix (bigger change, not applied here):** `ListViewExtended.vbproj` targets
`netstandard2.0`, which doesn't include `System.Windows.Forms` at all — that's why it needs an
explicit `HintPath` in the first place. Since the library is only ever consumed by the WinForms
exe (`Housekeeper.vbproj`, targeting `net472`), retargeting `ListViewExtended.vbproj` to `net472`
would let `<Reference Include="System.Windows.Forms" />` resolve automatically with no `HintPath`
at all — the same way the main project's own `System.Windows.Forms` reference already works.
Trade-off: it moves the build output from `bin\Debug\netstandard2.0\` to `bin\Debug\net472\`, so
the consuming project's `HintPath` to `ListViewExtended.dll` would need updating too.

A quicker sanity check for either fix: right-click the reference in Visual Studio's Solution
Explorer — if it shows a warning icon or `Path not found`, the `HintPath` is broken.

</details>

</details>


<details open><summary><h2 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">FILE LIST</h2></summary>

For an overview, see the [<ins>**File Selection Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#file-selection)


<details open><summary><h3 style="margin-bottom:-20px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Missing Files</h3></summary>

After selecting file sources then updating the list, files that should be there are not.

**Check file filters**

The filter controls are located on the bottom toolbar of the Home Page.

- Make sure the file type filters are set as needed.  These buttons are on the left side of the filter toolbar.
- Verify the File Wildcard search, if enabled, is set correctly.
- Check if a Property Filter is active and, if so, configured properly.

**Check sort options**

The sorting options are on the **Configuration Tab -- Sorting Page**.  Dependency and Random sort, in particular, can give confusing results if you are unaware they are enabled.

</details>

<details open><summary><h3 style="margin-bottom:-20px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">Top Level Assembly</h3></summary>

#### Removing Unneeded Files

At some point in practically every project, there comes a time to do some cleanup.  The option `Report unrelated files` can help.  It is set, along with the other options, on the **Configuration Tab -- Top Level Assembly Page**.

If any unrelated files are found, they are presented in a Notepad window.  You can manually remove files that are no longer needed.  For a more automatic method, take a look at [<ins>**File List Shortcut Menu Help Topic**</ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#shortcut-menu).

</details>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="Resources/SE_asm.png"><img src="My%20Project/media/spacer.png">Importing Lists</h3></summary>

A list contains the names of files to be processed.  It accepts Excel, `*.txt`, `*.csv`, and `*.tsv` file types.

The file names must include the full path.  So `C:\Projects\Project123\Part1.dft`, not just `Part1.dft` or `Part1`.

</details>

</details>

<details open><summary><h2 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">SORTING</h2></summary>

For an overview, see the [<ins>**Sorting Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#sorting)

#### Dependency Sort

This is useful in conjunction with the `Update part copy` command.  It is intended to help eliminate the tedious `model out-of-date` (dark gray corners) on drawings.  

Files are processed in strict dependency order.  Processing parts with no dependencies is optional.  If updating part copies only, it would normally be disabled.

#### Unsorted

This is primarily intended for lists prepared in advance in the desired processing order.

An option, `Keep duplicates`, can be useful in some cases.  For example printing job packets for multiple production shops, where certain files need to be provided to each organization.

</details>


<details open><summary><h2 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="My%20Project/media/spacer.png">FILTERING</h2></summary>

For an overview, see the [<ins>**Filtering Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#filtering)

#### Detecting if a Property Exists in a File

You may need to find files that have a given property, even if you don't care what its value is.

![](My%20Project/media/tips_property_filter.png)

A wildcard match for `*` will match every file that contains that property and exclude all others.  If instead you want every file that *does not* contain the property, use `Edit Formula` to set it to `Not A`.

#### Property Filter Formula

The filter formula is a boolean expression.  They're usually pretty straight-forward, but can get tricky.

Let's say you wanted to find all files with Document Numbers containing `19-37-70` and `19-37-71`.  You might set up the following property filter.
```
       A System.Document Number contains 19-37-70 
       B System.Document Number contains 19-37-71 
       Filter Formula A AND B
```

You would expect it to find `19-37-70-01`, `19-37-70-02B`, `19-37-71-99`, etc.  Instead, no matches would occur.

The reason has to do with the filter formula. When the program checks, `say 19-37-70-01`, it will find `Condition A` to be `TRUE` and `Condition B` to be `FALSE`. It then substitutes those results into the filter formula and evaluates the boolean expression `TRUE AND FALSE` which, if you remember your truth tables, is `FALSE`.

The way to handle it here is to change the filter formula from `A AND B` to `A OR B`. In the example the expression becomes `TRUE OR FALSE`, which evaluates to `TRUE`.

#### Working with Property Filter Options

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

<details open><summary><h2 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="Resources/SE_asm.png"><img src="My%20Project/media/spacer.png">TASK-SPECIFIC TIPS</h2></summary>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="Resources/SE_asm.png"><img src="My%20Project/media/spacer.png">Edit Properties</h3></summary>

For an overview, see the [<ins>**Edit Properties Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#edit-properties)

#### No Properties Available

There is some setup to make your properties available to the program.  See the [<ins>**Templates Page Help Topic**</ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#templates-page) for details.

#### Change a Custom Property Name

This command is designed to update property values, but can also change the name of the property itself.

![Change Name](My%20Project/media/tips_edit_properties_change_name.png)

On the Edit Properties option panel (not shown), enable `Add property`.  

In the input editor, assign the value of the old property to the new.  You can then delete the old property as shown on the second row.  (You can delete it in another run of the command if you first want to verify results.)

#### Using a Property Filter

If you are editing a property, there is no reason to process files that do not contain it.  See the tip [<ins>**Detecting if a Property Exists in a File**</ins>](#detecting-if-a-property-exists-in-a-file) for a way to speed things up.

#### Delete Properties

As shown above, the command can delete properties.  That's fine for a couple of them.  

If you have a bunch, there is an external program, [<ins>**AddRemoveCustomProperties**</ins>](https://github.com/rmcanany/HousekeeperExternalPrograms/tree/main/AddRemoveCustomProperties#readme), that may help.  It can run stand-alone on a single file, or with the `Run External Program` for a batch of them.  

There are two operating modes -- `Remove` and `RemoveAllExcept`.  They are discussed in the link provided.

</details>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="Resources/SE_asm.png"><img src="My%20Project/media/spacer.png">Update Material from Material Table</h3></summary>

For an overview, see the [<ins>**Update Material from Material Table Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#update-material-from-material-table)

**Change which Material Table Files Use**

In Solid Edge, the Material Library and Material Table are not the same thing.  The Material Library is a directory.  The Material Table(s) are `*.mtl` files in that directory.

SE doesn't care which Material Table contains the file's material, as long as one does.  Housekeeper is pickier.  You specify the Material table to use on the **Configuration Tab -- Templates Page**.  Then run this command to update the file.

</details>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="Resources/SE_asm.png"><img src="My%20Project/media/spacer.png">Save Model/Drawing As</h3></summary>

For an overview, see the [<ins>**Save Drawing As Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#save-drawing-as)

#### Save Multiple File Types

You can configure the Task List with multiple copies of the same command, then configure each as needed.

See the [<ins>**Customizing the Task Tab Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#customizing) for details.

</details>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="Resources/SE_asm.png"><img src="My%20Project/media/spacer.png">Print</h3></summary>

For an overview, see the [<ins>**Print Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#print)

#### Send to Printer/Plotter as Needed

You can configure the Task List with multiple copies of the same command, then configure each as needed.  The Print command has the ability to specify sheet sizes accepted by the selected printer.

See the [<ins>**Customizing the Task Tab Help Topic**<ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#customizing) for details.

</details>

<details open><summary><h3 style="margin:0px; display:inline-block"><img src="My%20Project/media/spacer.png"><img src="Resources/SE_asm.png"><img src="My%20Project/media/spacer.png">Task Dummy</h3></summary>

</details>

</details>

