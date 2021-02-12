# Solid Edge Housekeeper
Robert McAnany 2021

Portions adapted from code by Jason Newell, Greg Chasteen, Tushar Suradkar, and others.  Most of the rest copied verbatim from Jason's repo and Tushar's blog.

Helpful feedback and bug reports: @Satyen, @n0minus38, @wku, @aredderson, @bshand, @TeeVar, @Jean-Louis

## DESCRIPTION
This tool is designed to help you find annoying little errors in your project.  It can identify failed features in 3D models, detached dimensions in drawings, missing parts in assemblies, and more.  It can also update certain individual file settings to match those in a template you specify.

## INSTALLATION
There is no installation per se.  The preferred method is to download or clone the project and compile it yourself.

The other option is to use the latest released version here https://github.com/rmcanany/SolidEdgeHousekeeper/releases/tag/v0.1.5  From the Assets list, click the SolidEdgeHousekeeper zip file.  It should prompt you to save it.  Choose a convenient location on your machine.  Extract the zip file (probably by right-clicking and selecting Extract All).  Double-click the .exe file to run.

## OPERATION
On each file type's tab, select which errors to detect.  On the General tab, browse to the desired input folder, then select the desired directory search option.  

You can further refine the search using a property filter.  For details, see the Property Filter Readme tab.  Note, if 'Top level assembly' is the chosen search option, or a property filter is set, click 'Update File List' to populate the list.  On large assemblies, this can take some time.

If any errors are found, a log file will be written to the input folder.  It will identify each error and the file in which it occurred.  When processing is complete, a message box will give you the file name.

The first time you use the program, some site-specific information is needed.  This includes the location of your templates, material table, etc.  These are accessed on the Configuration Tab.

You can interrupt the program before it finishes.  While processing, the Cancel button changes to a Stop button.  Just click that to halt processing.  It may take several seconds to register the request.  It doesn't hurt to click it a couple of times.

## CAVEATS
Since the program can process a large number of files in a short amount of time, it can be very taxing on Solid Edge.  To maintain a clean environment, the program restarts Solid Edge periodically.  This is by design and does not necessarily indicate a problem.  

However, problems can arise.  Those cases will be reported in the log file with the message 'Error processing file'.  A stack trace will be included, which looks scary, but may be useful for program debugging.  If four of these errors are detected in a run, the programs halts processing.

Please note this is not a perfect program.  It is not guaranteed not to mess up your files.  Back up any files before using it.

## KNOWN ISSUES
Does not support managed files.  Cause: Unknown.  Possible workaround: Process the files in an unmanaged workspace.   

Some tasks may not support versions of Solid Edge prior to SE2020.  Cause: Maybe an API call not available in previous versions.  Possible workaround: Use SE2020 or later.  

May not support multiple installed Solid Edge versions on the same machine.  Cause: Unknown.  Possible workaround: Use the version that was 'silently' installed.  

## DETAILS

TESTS AND ACTIONS
### Assembly
    Activate and update all
    Update face and view styles from template
    Remove face style overrides
    Fit isometric view
    Occurrence missing files
    Occurrence outside project directory
    Failed relationships
    Underconstrained relationships
    Part number does not match file name
    Save as STEP
### Part
    Update insert part copies
    Update material from material table
    Update face and view styles from template
    Fit isometric view
    Failed or warned features
    Suppressed or rolled back features
    Underconstrained profiles
    Insert part copies out of date
    Material not in material table
    Part number does not match file name
    Save as STEP
### Sheetmetal
    Update insert part copies
    Update material from material table
    Update face and view styles from template
    Fit isometric view
    Failed or warned features
    Suppressed or rolled back features
    Underconstrained profiles
    Insert part copies out of date
    Flat pattern missing or out of date
    Material not in material table
    Part number does not match file name
    Generate Laser DXF and PDF
    Save as STEP
### Draft
    Update drawing views
    Move drawing to new template
    Fit view
    Drawing views missing file
    Drawing views out of date
    Detached dimensions or annotations
    File name does not match model file name
    Save as PDF
    Save as DXF

## CODE ORGANIZATION
Processing starts in Form1.vb.  A short description of the code's organization can be found there.
