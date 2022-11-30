# Solid Edge Housekeeper v0.1.10.0
Robert McAnany 2022

Portions adapted from code by Jason Newell, Greg Chasteen, Tushar Suradkar, and others.  Most of the rest copied verbatim from Jason's repo and Tushar's blog.

Helpful feedback and bug reports: @Satyen, @n0minus38, @wku, @aredderson, @bshand, @TeeVar, @SeanCresswell, @Jean-Louis, @Jan_Bos, @MonkTheOCD_Engie, @[mike miller], @Fiorini, @[Martin Bernhard], @Derek G, @Chris42, @Jason1607436093479, @Bob Henry, @JayJay101

## DESCRIPTION
This tool is designed to help you find annoying little errors in your project.  It can identify failed features in 3D models, detached dimensions in drawings, missing parts in assemblies, and more.  It can also update certain individual file settings to match those in a template you specify.

## GETTING HELP
Ask questions, report issues, or suggest ideas for improvement on the Solid Edge Forum: 
https://community.sw.siemens.com/s/topic/0TO4O000000MihiWAC/solid-edge

## INSTALLATION
There is no installation per se.  The preferred method is to download or clone the project and compile it yourself.

The other option is to use the latest released version here https://github.com/rmcanany/SolidEdgeHousekeeper/releases  Under the latest release, click the SolidEdgeHousekeeper-vx.x.x.zip file (sometimes hidden under the Assets dropdown).  It should prompt you to save it.  Choose a convenient location on your machine.  Extract the zip file (probably by right-clicking and selecting Extract All).  Double-click the .exe file to run.

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

