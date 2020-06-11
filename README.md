# Solid Edge Housekeeper
Robert McAnany 2020

Portions adapted from code by Jason Newell, Greg Chasteen, Tushar Suradkar, and others.  Most of the rest copied verbatim from Jason's repo and Tushar's blog.

Helpful feedback and bug reports: @Satyen, @n0minus38, @wku, @aredderson

## DESCRIPTION
This tool is designed to help you find annoying little errors in your project.  It can identify failed features in 3D models, detached dimensions in drawings, missing parts in assemblies, and more.  It can also update certain individual file settings to match those in a template you specify.

## INSTALLATION
There is no installation per se.  The preferred method is to download or clone the project and compile it yourself.

The other option is to use the latest released version.  At the top of file list above, click the 'Releases' tab.  On that page, click the Assets dropdown.  Select the most recent SolidEdgeHousekeeper-vX.X.X.zip.  It should prompt you to save it.  Choose a convenient location on your machine.  Extract the zip file (probably by right-clicking and selecting Extract All).  Double-click the .exe file to run.

## OPERATION
On each file type's tab, select which errors to detect.  On the General tab, select which files to process.  Do this by browsing to the desired input folder, then clicking the desired directory search option.

If any errors are found, a log file will be written to the input folder.  It will identify each error and the file in which it occurred.  When processing is complete, a message box will give you the file name.

The first time you use the program, some site-specific information is needed.  This includes the location of your templates, material table, etc.  These are accessed on the Configuration Tab.

You can interrupt the program before it finishes.  While processing, the Cancel button changes to a Stop button.  Just click that to halt processing.  It may take several seconds to register the request.  It doesn't hurt to click it a couple of times.

## CAVEATS
Since the program can process a large number of files in a short amount of time, it can be very taxing on Solid Edge.  To maintain a clean environment, the program restarts Solid Edge periodically.  This is by design and does not necessarily indicate a problem.  

However, problems can arise.  Those cases will also be reported in the log file with the message 'Error processing file'.  A stack trace will be included, which may be useful for program debugging.

Please note this is not a perfect program.  It is not guaranteed not to mess up your files.  Back up any files before using it.

## DETAILS
List of implemented tests and actions.
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
### Draft
    Update drawing views
    Update drawing border from template
    Update dimension styles from template
    Fit view
    Drawing views missing file
    Drawing views out of date
    Detached dimensions or annotations
    File name does not match model file name
    Save as PDF

## CODE ORGANIZATION
Processing starts in Form1.vb.  A short description of the code's organization can be found there.
