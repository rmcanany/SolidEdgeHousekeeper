# Solid Edge Housekeeper
Robert McAnany 2020

Portions adapted from code by Jason Newell, Greg Chasteen, Tushar Suradkar, and others.  Most of the rest copied verbatim from Jason's repo and Tushar's blog.

## DESCRIPTION
This tool is designed to help you find annoying little errors in your project.  It can identify failed features in 3D models, detached dimensions in drawings, missing parts in assemblies, and more.  It can also update certain individual file settings to match those in a template you specify.

## INSTALLATION
There is no installation per se.  The preferred method is to download or clone the project and compile it yourself.

Not everyone can do that, however, so the compiled executable is provided.  To get it, click the bin/Release folder above and select Housekeeper.exe.  On that page, click Download and save it to a convenient location.  Double-click the executable to run.

## OPERATION
Select which errors to detect by clicking the appropriate checkboxes on each file type's tab.  Select which files to process, on the General tab, by navigating to the desired input folder, and then clicking the desired search type.

If any errors are found, a text file will be written to the input folder.  It will identify each error and the file in which it occurred.  When processing is complete, a message box will give you the file name.

The first time you use the program, you need to supply some user-specific information.  This includes the location of your templates, material table, and the like.  These are accessed on the Configuration Tab.

## CAVEATS
Since the program can process a large number of files in a short amount of time, it can be very taxing on Solid Edge.  To maintain a clean environment, the program restarts Solid Edge periodically.  This is by design and does not necessarily indicate a problem.  

However, problems can arise.  Those cases will also be reported in the text file with the message 'Error processing file'.  A stack trace will be included, which may be useful for program debugging.

Please note this is not a perfect program.  It is not guaranteed not to mess up your files.  Back up any files before using it.

## DETAILS
List of implemented tests and actions.
### Assembly
    Occurrence missing files
    Occurrence outside project directory
    Failed relationships
    Underconstrained relationships
    Part number does not match file name
    Update face and view styles from template
    Fit isometric view
### Part
    Failed or warned features
    Suppressed or rolled back features
    Underconstrained profiles
    Material not in material table
    Part number does not match file name
    Update face and view styles from template
    Fit isometric view
### Sheetmetal
    Failed or warned features
    Suppressed or rolled back features
    Underconstrained profiles
    Flat pattern missing or out of date
    Material not in material table
    Part number does not match file name
    Generate Laser DXF and PDF
    Update face and view styles from template
    Fit isometric view
### Draft
    Drawing views missing file
    Drawing views out of date
    Detached dimensions or annotations
    File name does not match model file name
    Update drawing border from template
    Update dimension styles from template
    Fit view
    Save as PDF

## CODE ORGANIZATION
Processing starts in Form1.vb.  A short description of the code's organization can be found there.
