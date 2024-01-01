Option Strict On

Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports Newtonsoft.Json
Imports SolidEdgeCommunity


Public Class Form1
    Public SEApp As SolidEdgeFramework.Application

    Private DefaultsFilename As String
    Private MissingFilesFileName As String

    Private ErrorsOccurred As Boolean
    Private TotalAborts As Double
    Private TotalAbortsMaximum As Integer = 4

    Private FilesToProcessTotal As Integer
    Private FilesToProcessCompleted As Integer
    Dim StartTime As DateTime

    Public Shared StopProcess As Boolean

    Private DragDropCache As New List(Of ListViewItem)
    Private DragDropCacheExcluded As New List(Of ListViewItem)

    Private ListItems_TextFiltered As New List(Of ListViewItem)
    Private ListItems_PropFiltered As New List(Of ListViewItem)

    Private ListViewFilesOutOfDate As Boolean

    Private Configuration As New Dictionary(Of String, String)

    Private LabelToActionAssembly As New LabelToAction("Assembly")
    Private LabelToActionPart As New LabelToAction("Part")
    Private LabelToActionSheetmetal As New LabelToAction("Sheetmetal")
    Private LabelToActionDraft As New LabelToAction("Draft")

    Private LaunchTask As New LaunchTask()

    Private FormPropertyFilter As New FormPropertyFilter()
    ' Public Shared PropertyFilterResult As TextBox
    Public Shared PropertyFilterFormula As String
    Public Shared PropertyFilterDict As New Dictionary(Of String, Dictionary(Of String, String))

    Private FormSheetSelector As New FormSheetSelector()
    Public Shared Printer2SelectedSheets As New Dictionary(Of String, SolidEdgeDraft.PaperSizeConstants)





    'DESCRIPTION
    'Solid Edge Housekeeper
    'Robert McAnany 2020
    '
    'Portions adapted from code by Jason Newell, Greg Chasteen, Tushar Suradkar, and others.
    'Most of the rest was copied verbatim from Jason's repo or Tushar's blog.
    '
    'This description is about the organization of the code.  To read how to use it, see the 
    'Readme Tab on Form1.
    '
    'The program performs various tasks on batches of files.  Each task for each file type is 
    'contained in a separate Function.  
    '
    'The basic flow is Process() -> ProcessFiles() -> ProcessFile().  The ProcessFile() routine 
    'calls, in turn, each task that has been checked on the form.  The call is handled in the
    'LaunchTask class, which in turn calls the file type's respective tasks.  These are housed
    'in AssemblyTasks, PartTasks, etc.
    '
    'Error handling is localized in the ProcessFile() routine.  Running hundreds or thousands of 
    'files in a row can sometimes cause an Application malfunction.  In such cases, ProcessFile() 
    'retries the tasks.  Most of the time it succeeds on the second attempt.
    'UPDATE 20200507:  Implementing Jason's IsolatedTask scheme seems to have fixed the Application
    'malfunctions.  Removed the retry functionality in ProcessFile().
    '
    'The mapping between checkboxes and tasks is done in the LabelToAction class.  It creates one 
    'instance for each file type.  The naming convention is LabelToAction<file type>, e.g., 
    'LabelToActionAssembly, LabelToActionPart, etc.  
    '
    'The main processing is done in this file, Form1.vb.  Some ancillary routines are housed in 
    'their own Partial Class, Form1.*.vb file.

    'To add tasks to the program is a matter of adding the code to the appropriate Tasks class.  
    'Note, each task has a Public and Private component.  The Public part sets up the IsolatedTask.
    'The Private part does the actual task processing.  Getting the task to the form's 
    'CheckedListBoxes is accomplished by adding a corresponding entry in the LabelToAction class 
    'and updating LaunchTask accordingly.  

    'To add new user-specific input entails first adding the appropriate control to the form.  
    'The second step would be to add that information to the Configuration dictionary in the 
    'ReconcileFormChanges routine.  Next would be to modify CheckStartConditions to validate 
    'any user input.  Finally, the LoadDefaults would need to be updated as required.

    Private Sub ProcessAll()

        Me.Cursor = Cursors.WaitCursor

        Dim ErrorMessage As String
        Dim ElapsedTime As Double
        Dim ElapsedTimeText As String

        StartTime = Now

        ReconcileFormChanges()
        SaveDefaults()

        ErrorMessage = CheckStartConditions()
        If ErrorMessage <> "" Then
            Me.Cursor = Cursors.Default
            Dim result As MsgBoxResult = MsgBox(ErrorMessage, vbOKOnly)
            If result = MsgBoxResult.Cancel Then
                Exit Sub
            End If
            If ErrorMessage.Contains("Please correct the following before continuing") Then
                Exit Sub
            End If
        End If

        FilesToProcessTotal = GetTotalFilesToProcess()
        FilesToProcessCompleted = 0

        StopProcess = False
        ButtonCancel.Text = "Stop"

        OleMessageFilter.Register()

        LogfileSetName()

        TotalAborts = 0

        SEStart()

        If CheckedListBoxPart.CheckedItems.Count > 0 Then
            ProcessFiles("Part")
        End If

        If CheckedListBoxSheetmetal.CheckedItems.Count > 0 Then
            ProcessFiles("Sheetmetal")
        End If

        If CheckedListBoxAssembly.CheckedItems.Count > 0 Then
            ProcessFiles("Assembly")
        End If

        If CheckedListBoxDraft.CheckedItems.Count > 0 Then
            ProcessFiles("Draft")
        End If

        SEStop()

        OleMessageFilter.Unregister()

        If StopProcess Then
            If TotalAborts > TotalAbortsMaximum Then
                TextBoxStatus.Text = "The number of file processing errors exceeded maximum.  Stopping."
            Else
                TextBoxStatus.Text = "Processing halted by user."
            End If
        Else
            ElapsedTime = Now.Subtract(StartTime).TotalMinutes
            If ElapsedTime < 60 Then
                ElapsedTimeText = "in " + ElapsedTime.ToString("0.0") + " min."
            Else
                ElapsedTimeText = "in " + (ElapsedTime / 60).ToString("0.0") + " hr."
            End If

            TextBoxStatus.Text = "Finished processing " + FilesToProcessTotal.ToString + " files " + ElapsedTimeText
        End If

        LabelTimeRemaining.Text = ""

        StopProcess = False
        ButtonCancel.Text = "Cancel"

        If ErrorsOccurred Then
            Process.Start("Notepad.exe", MissingFilesFileName)
        Else
            TextBoxStatus.Text = TextBoxStatus.Text + "  All checks passed."
        End If

        Me.Cursor = Cursors.Default

    End Sub


    Private Function CheckStartConditions() As String
        Dim msg As String = ""
        Dim msg2 As String = ""
        Dim indent As String = "    "
        Dim SaveMsg As String = ""
        Dim tf As Boolean
        Dim DestinationDirectory As String

        ReconcileFormChanges()

        If SEIsRunning() Then
            msg += "    Close Solid Edge" + Chr(13)
        End If

        If DMIsRunning() Then
            msg += "    Close Design Manager" + Chr(13)
        End If

        If ListViewFilesOutOfDate Then
            msg += "    Update the file list" + Chr(13)
        End If

        tf = CheckedListBoxAssembly.CheckedItems.Count = 0
        tf = tf And CheckedListBoxPart.CheckedItems.Count = 0
        tf = tf And CheckedListBoxSheetmetal.CheckedItems.Count = 0
        tf = tf And CheckedListBoxDraft.CheckedItems.Count = 0
        If tf Then
            msg += "    Select a task to perform on at least one Task tab" + Chr(13)
        End If

        If RadioButtonTLABottomUp.Checked Then
            If Not FileIO.FileSystem.FileExists(TextBoxFastSearchScopeFilename.Text) Then
                msg += "    Fast search scope file (on Configuration Tab) not found" + Chr(13)
            End If
        End If

        For Each Filename As ListViewItem In ListViewFiles.Items 'L-istBoxFiles.Items

            ListViewFiles.BeginUpdate()

            If Filename.Group.Name <> "Sources" Then

                Filename.ImageKey = "Unchecked"

                If Not FileIO.FileSystem.FileExists(Filename.Name) Then
                    msg += "    File not found, or Path exceeds maximum length" + Chr(13)
                    msg += "    " + CType(Filename.Name, String) + Chr(13)
                    ListViewFilesOutOfDate = True
                    Exit For
                End If

            End If

            ListViewFiles.EndUpdate()

        Next

        If ListViewFilesOutOfDate Then
            'msg += "    Update the file list, or otherwise correct the issue" + Chr(13)
        ElseIf ListViewFiles.Items.Count = 0 Then
            msg += "    Select an input directory with files to process" + Chr(13)
        End If

        If new_CheckBoxFileSearch.Checked Then
            If new_ComboBoxFileSearch.Text = "" Then
                msg += "    Enter a file wildcard search string" + Chr(13)
            End If
        End If


        ' For the selected tasks, see if outside information, such as a template file, is required.
        ' If so, verify that the outside information is valid.

        For Each Label As String In CheckedListBoxAssembly.CheckedItems
            If LabelToActionAssembly(Label).RequiresTemplate Then
                If Not FileIO.FileSystem.FileExists(TextBoxTemplateAssembly.Text) Then
                    If Not msg.Contains("Select a valid assembly template") Then
                        msg += "    Select a valid assembly template" + Chr(13)
                    End If
                End If

            End If
            If LabelToActionAssembly(Label).RequiresMaterialTable Then
                If Not FileIO.FileSystem.FileExists(TextBoxActiveMaterialLibrary.Text) Then
                    If Not msg.Contains("Select a valid material library") Then
                        msg += "    Select a valid material library" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionAssembly(Label).RequiresPartNumberFields Then
                If TextBoxPartNumberPropertyName.Text = "" Then
                    If Not msg.Contains("Select a valid part number property name") Then
                        msg += "    Select a valid part number property name" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionAssembly(Label).RequiresSave Then
                SaveMsg += "    Assembly: " + Label + Chr(13)
            End If
            If LabelToActionAssembly(Label).RequiresSaveAsOutputDirectory Then
                tf = Not FileIO.FileSystem.DirectoryExists(TextBoxSaveAsAssemblyOutputDirectory.Text)
                tf = tf And Not CheckBoxSaveAsAssemblyOutputDirectory.Checked
                If tf Then
                    If Not msg.Contains("Select a valid Save As assembly output directory") Then
                        msg += "    Select a valid Save As assembly output directory" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionAssembly(Label).IncompatibleWithOtherTasks Then
                tf = CheckedListBoxAssembly.CheckedItems.Count > 1
                tf = tf Or CheckedListBoxPart.CheckedItems.Count > 0
                tf = tf Or CheckedListBoxSheetmetal.CheckedItems.Count > 0
                tf = tf Or CheckedListBoxDraft.CheckedItems.Count > 0

                If tf Then
                    If Not msg.Contains(Label + "--Assembly cannot be performed with any other tasks selected") Then
                        msg += "    " + Label + "--Assembly cannot be performed with any other tasks selected" + Chr(13)
                    End If
                End If
            End If

            If LabelToActionAssembly(Label).RequiresExternalProgram Then
                If Not FileIO.FileSystem.FileExists(TextBoxExternalProgramAssembly.Text) Then
                    If Not msg.Contains("Select a valid assembly external program") Then
                        msg += "    Select a valid assembly external program" + Chr(13)
                    End If
                Else
                    DestinationDirectory = System.IO.Path.GetDirectoryName(TextBoxExternalProgramAssembly.Text)
                    FileSystem.FileCopy(DefaultsFilename, String.Format("{0}\defaults.txt", DestinationDirectory))
                    ' MsgBox(DefaultsFilename + ", " + DestinationDirectory + ", " + String.Format("{0}\defaults.txt", DestinationDirectory))
                End If
            End If

            If LabelToActionAssembly(Label).RequiresFindReplaceFields Then
                If (TextBoxPropertiesEditAssembly.Text = "") Or (TextBoxPropertiesEditAssembly.Text = "{}") Then
                    If Not msg.Contains("Enter one or more assembly properties to change") Then
                        msg += "    Enter one or more assembly properties to change" + Chr(13)
                    End If
                End If

                'If TextBoxFindReplacePropertyNameAssembly.Text = "" Then
                '    If Not msg.Contains("Enter a valid assembly property name") Then
                '        msg += "    Enter a valid assembly property name" + Chr(13)
                '    End If
                'End If

                'If TextBoxFindReplaceFindAssembly.Text = "" Then
                '    If Not msg.Contains("Enter a valid assembly search string") Then
                '        msg += "    Enter a valid assembly search string" + Chr(13)
                '    End If
                'End If

                'tf = CheckBoxFindReplaceFindPTAssembly.Checked
                'tf = tf Or CheckBoxFindReplaceFindWCAssembly.Checked
                'tf = tf Or CheckBoxFindReplaceFindRXAssembly.Checked
                'If Not tf Then
                '    If Not msg.Contains("Select a search type for the 'Find' text") Then
                '        msg += "    Select a search type for the 'Find' text" + Chr(13)
                '    End If
                'End If

                'tf = CheckBoxFindReplaceReplacePTAssembly.Checked
                'tf = tf Or CheckBoxFindReplaceReplaceRXAssembly.Checked
                'If Not tf Then
                '    If Not msg.Contains("Select a search type for the 'Replace' text") Then
                '        msg += "    Select a search type for the 'Replace' text" + Chr(13)
                '    End If
                'End If

                'tf = CheckBoxFindReplaceReplaceRXAssembly.Checked
                'tf = tf And Not CheckBoxFindReplaceFindRXAssembly.Checked
                'If tf Then
                '    If Not msg.Contains("Replacement search type 'RX' requires Find search type 'RX'") Then
                '        msg += "    Replacement search type 'RX' requires Find search type 'RX'" + Chr(13)
                '    End If
                'End If
            End If

            If LabelToActionAssembly(Label).RequiresPictorialView Then
                tf = RadioButtonPictorialViewDimetric.Checked
                tf = tf Or RadioButtonPictorialViewIsometric.Checked
                tf = tf Or RadioButtonPictorialViewTrimetric.Checked

                If Not tf Then
                    If Not msg.Contains("Select a pictorial view orientation on the Configuration tab") Then
                        msg += "    Select a pictorial view orientation on the Configuration tab"
                    End If
                End If
            End If

            If LabelToActionAssembly(Label).RequiresForegroundProcessing Then
                If CheckBoxBackgroundProcessing.Checked Then
                    If Not msg.Contains(Label + " cannot be run in a background process") Then
                        msg += "    " + Label + " cannot be run in a background process" + Chr(13)
                    End If
                End If
            End If

            If LabelToActionAssembly(Label).RequiresVariablesToEdit Then
                If TextBoxVariablesEditAssembly.Text = "" Then
                    msg2 = indent + "Enter as least one Assembly variable to edit/add/expose"
                    If Not msg.Contains(msg2) Then
                        msg += msg2 + Chr(13)
                    End If
                End If
            End If

            If LabelToActionAssembly(Label).RequiresOverallSizeVariables Then
                tf = CheckBoxStockSizeXYZ.Checked
                tf = tf Or CheckBoxStockSizeMinMidMax.Checked
                If Not tf Then
                    msg2 = indent + "Select at least one overall variable type"
                    If Not msg.Contains(msg2) Then
                        msg += msg2 + Chr(13)
                    End If
                End If
                If CheckBoxStockSizeXYZ.Checked Then
                    tf = TextBoxStockSizeX.Text = ""
                    tf = tf Or TextBoxStockSizeY.Text = ""
                    tf = tf Or TextBoxStockSizeZ.Text = ""
                    If tf Then
                        msg2 = indent + "Enter a name for each XYZ variable"
                        If Not msg.Contains(msg2) Then
                            msg += msg2 + Chr(13)
                        End If
                    End If
                End If
                If CheckBoxStockSizeMinMidMax.Checked Then
                    tf = TextBoxStockSizeMin.Text = ""
                    tf = tf Or TextBoxStockSizeMid.Text = ""
                    tf = tf Or TextBoxStockSizeMax.Text = ""
                    If tf Then
                        msg2 = indent + "Enter a name for each MinMidMax variable"
                        If Not msg.Contains(msg2) Then
                            msg += msg2 + Chr(13)
                        End If
                    End If
                End If
            End If

        Next

        ' Part Tasks

        For Each Label As String In CheckedListBoxPart.CheckedItems
            If LabelToActionPart(Label).RequiresTemplate Then
                If Not FileIO.FileSystem.FileExists(TextBoxTemplatePart.Text) Then
                    If Not msg.Contains("Select a valid part template") Then
                        msg += "    Select a valid part template" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionPart(Label).RequiresMaterialTable Then
                If Not FileIO.FileSystem.FileExists(TextBoxActiveMaterialLibrary.Text) Then
                    If Not msg.Contains("Select a valid material library") Then
                        msg += "    Select a valid material library" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionPart(Label).RequiresPartNumberFields Then
                If TextBoxPartNumberPropertyName.Text = "" Then
                    If Not msg.Contains("Select a valid part number property name") Then
                        msg += "    Select a valid part number property name" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionPart(Label).RequiresSave Then
                SaveMsg += "    Part: " + Label + Chr(13)
            End If
            If LabelToActionPart(Label).RequiresSaveAsOutputDirectory Then
                tf = Not FileIO.FileSystem.DirectoryExists(TextBoxSaveAsPartOutputDirectory.Text)
                tf = tf And Not CheckBoxSaveAsPartOutputDirectory.Checked
                If tf Then
                    If Not msg.Contains("Select a valid Save As part output directory") Then
                        msg += "    Select a valid Save As part output directory" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionPart(Label).IncompatibleWithOtherTasks Then
                tf = CheckedListBoxPart.CheckedItems.Count > 1
                tf = tf Or CheckedListBoxAssembly.CheckedItems.Count > 0
                tf = tf Or CheckedListBoxSheetmetal.CheckedItems.Count > 0
                tf = tf Or CheckedListBoxDraft.CheckedItems.Count > 0

                If tf Then
                    If Not msg.Contains(Label + "--Part cannot be performed with any other tasks selected") Then
                        msg += "    " + Label + "--Part cannot be performed with any other tasks selected" + Chr(13)
                    End If
                End If
            End If

            If LabelToActionPart(Label).RequiresExternalProgram Then
                If Not FileIO.FileSystem.FileExists(TextBoxExternalProgramPart.Text) Then
                    If Not msg.Contains("Select a valid part external program") Then
                        msg += "    Select a valid part external program" + Chr(13)
                    End If
                Else
                    DestinationDirectory = System.IO.Path.GetDirectoryName(TextBoxExternalProgramPart.Text)
                    FileSystem.FileCopy(DefaultsFilename, String.Format("{0}\defaults.txt", DestinationDirectory))

                End If
            End If

            If LabelToActionPart(Label).RequiresFindReplaceFields Then
                If (TextBoxPropertiesEditPart.Text = "") Or (TextBoxPropertiesEditPart.Text = "{}") Then
                    If Not msg.Contains("Enter one or more part properties to change") Then
                        msg += "    Enter one or more part properties to change" + Chr(13)
                    End If
                End If

                'If TextBoxFindReplacePropertyNamePart.Text = "" Then
                '    If Not msg.Contains("Enter a valid part property name") Then
                '        msg += "    Enter a valid part property name" + Chr(13)
                '    End If
                'End If
                'If TextBoxFindReplaceFindPart.Text = "" Then
                '    If Not msg.Contains("Enter a valid part search string") Then
                '        msg += "    Enter a valid part search string" + Chr(13)
                '    End If
                'End If

                'tf = CheckBoxFindReplaceFindPTPart.Checked
                'tf = tf Or CheckBoxFindReplaceFindWCPart.Checked
                'tf = tf Or CheckBoxFindReplaceFindRXPart.Checked
                'If Not tf Then
                '    If Not msg.Contains("Select a search type for the 'Find' text") Then
                '        msg += "    Select a search type for the 'Find' text" + Chr(13)
                '    End If
                'End If

                'tf = CheckBoxFindReplaceReplacePTPart.Checked
                'tf = tf Or CheckBoxFindReplaceReplaceRXPart.Checked
                'If Not tf Then
                '    If Not msg.Contains("Select a search type for the 'Replace' text") Then
                '        msg += "    Select a search type for the 'Replace' text" + Chr(13)
                '    End If
                'End If

                'tf = CheckBoxFindReplaceReplaceRXPart.Checked
                'tf = tf And Not CheckBoxFindReplaceFindRXPart.Checked
                'If tf Then
                '    If Not msg.Contains("Replacement search type 'RX' requires Find search type 'RX'") Then
                '        msg += "    Replacement search type 'RX' requires Find search type 'RX'" + Chr(13)
                '    End If
                'End If

            End If

            If LabelToActionPart(Label).RequiresPictorialView Then
                tf = RadioButtonPictorialViewDimetric.Checked
                tf = tf Or RadioButtonPictorialViewIsometric.Checked
                tf = tf Or RadioButtonPictorialViewTrimetric.Checked

                If Not tf Then
                    If Not msg.Contains("Select a pictorial view orientation on the Configuration tab") Then
                        msg += "    Select a pictorial view orientation on the Configuration tab"
                    End If
                End If
            End If

            If LabelToActionPart(Label).RequiresForegroundProcessing Then
                If CheckBoxBackgroundProcessing.Checked Then
                    If Not msg.Contains(Label + " cannot be run in a background process") Then
                        msg += "    " + Label + " cannot be run in a background process" + Chr(13)
                    End If
                End If
            End If

            If LabelToActionPart(Label).RequiresVariablesToEdit Then
                If TextBoxVariablesEditPart.Text = "" Then
                    msg2 = indent + "Enter as least one Part variable to add/edit/expose"
                    If Not msg.Contains(msg2) Then
                        msg += msg2 + Chr(13)
                    End If
                End If
            End If

        Next

        ' Sheetmetal Tasks

        For Each Label As String In CheckedListBoxSheetmetal.CheckedItems
            If LabelToActionSheetmetal(Label).RequiresTemplate Then
                If Not FileIO.FileSystem.FileExists(TextBoxTemplateSheetmetal.Text) Then
                    If Not msg.Contains("Select a valid sheetmetal template") Then
                        msg += "    Select a valid sheetmetal template" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionSheetmetal(Label).RequiresMaterialTable Then
                If Not FileIO.FileSystem.FileExists(TextBoxActiveMaterialLibrary.Text) Then
                    If Not msg.Contains("Select a valid material library") Then
                        msg += "    Select a valid material library" + Chr(13)
                    End If
                End If
            End If
            'If LabelToActionSheetmetal(Label).RequiresLaserOutputDirectory Then
            '    tf = Not FileIO.FileSystem.DirectoryExists(TextBoxLaserOutputDirectory.Text)
            '    tf = tf And Not CheckBoxLaserOutputDirectory.Checked
            '    If tf Then
            '        If Not msg.Contains("Select a valid laser output directory") Then
            '            msg += "    Select a valid laser output directory" + Chr(13)
            '        End If
            '    End If
            'End If
            If LabelToActionSheetmetal(Label).RequiresPartNumberFields Then
                If TextBoxPartNumberPropertyName.Text = "" Then
                    If Not msg.Contains("Select a valid part number property name") Then
                        msg += "    Select a valid part number property name" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionSheetmetal(Label).RequiresSave Then
                SaveMsg += "    Sheetmetal: " + Label + Chr(13)
            End If
            If LabelToActionSheetmetal(Label).RequiresSaveAsOutputDirectory Then
                tf = Not FileIO.FileSystem.DirectoryExists(TextBoxSaveAsSheetmetalOutputDirectory.Text)
                tf = tf And Not CheckBoxSaveAsSheetmetalOutputDirectory.Checked
                If tf Then
                    If Not msg.Contains("Select a valid Save As sheetmetal output directory") Then
                        msg += "    Select a valid Save As sheetmetal output directory" + Chr(13)
                    End If
                End If
            End If
            'If LabelToActionSheetmetal(Label).RequiresSaveAsFlatDXFOutputDirectory Then
            '    tf = Not FileIO.FileSystem.DirectoryExists(TextBoxSaveAsFlatDXFOutputDirectory.Text)
            '    tf = tf And Not CheckBoxSaveAsFlatDXFOutputDirectory.Checked
            '    If tf Then
            '        If Not msg.Contains("Select a valid Save As Flat DXF output directory") Then
            '            msg += "    Select a valid Save As Flat DXF output directory" + Chr(13)
            '        End If
            '    End If
            'End If
            If LabelToActionSheetmetal(Label).IncompatibleWithOtherTasks Then
                tf = CheckedListBoxSheetmetal.CheckedItems.Count > 1
                tf = tf Or CheckedListBoxAssembly.CheckedItems.Count > 0
                tf = tf Or CheckedListBoxPart.CheckedItems.Count > 0
                tf = tf Or CheckedListBoxDraft.CheckedItems.Count > 0

                If tf Then
                    If Not msg.Contains(Label + "--Sheetmetal cannot be performed with any other tasks selected") Then
                        msg += "    " + Label + "--Sheetmetal cannot be performed with any other tasks selected" + Chr(13)
                    End If
                End If
            End If

            If LabelToActionSheetmetal(Label).RequiresExternalProgram Then
                If Not FileIO.FileSystem.FileExists(TextBoxExternalProgramSheetmetal.Text) Then
                    If Not msg.Contains("Select a valid sheetmetal external program") Then
                        msg += "    Select a valid sheetmetal external program" + Chr(13)
                    End If
                Else
                    DestinationDirectory = System.IO.Path.GetDirectoryName(TextBoxExternalProgramSheetmetal.Text)
                    FileSystem.FileCopy(DefaultsFilename, String.Format("{0}\defaults.txt", DestinationDirectory))

                End If
            End If

            If LabelToActionSheetmetal(Label).RequiresFindReplaceFields Then
                If (TextBoxPropertiesEditSheetmetal.Text = "") Or (TextBoxPropertiesEditSheetmetal.Text = "{}") Then
                    If Not msg.Contains("Enter one or more sheetmetal properties to change") Then
                        msg += "    Enter one or more sheetmetal properties to change" + Chr(13)
                    End If
                End If

                'If TextBoxFindReplacePropertyNameSheetmetal.Text = "" Then
                '    If Not msg.Contains("Enter a valid sheetmetal property name") Then
                '        msg += "    Enter a valid sheetmetal property name" + Chr(13)
                '    End If
                'End If
                'If TextBoxFindReplaceFindSheetmetal.Text = "" Then
                '    If Not msg.Contains("Enter a valid sheetmetal search string") Then
                '        msg += "    Enter a valid sheetmetal search string" + Chr(13)
                '    End If
                'End If

                'tf = CheckBoxFindReplaceFindPTSheetmetal.Checked
                'tf = tf Or CheckBoxFindReplaceFindWCSheetmetal.Checked
                'tf = tf Or CheckBoxFindReplaceFindRXSheetmetal.Checked
                'If Not tf Then
                '    If Not msg.Contains("Select a search type for the 'Find' text") Then
                '        msg += "    Select a search type for the 'Find' text" + Chr(13)
                '    End If
                'End If

                'tf = CheckBoxFindReplaceReplacePTSheetmetal.Checked
                'tf = tf Or CheckBoxFindReplaceReplaceRXSheetmetal.Checked
                'If Not tf Then
                '    If Not msg.Contains("Select a search type for the 'Replace' text") Then
                '        msg += "    Select a search type for the 'Replace' text" + Chr(13)
                '    End If
                'End If

                'tf = CheckBoxFindReplaceReplaceRXSheetmetal.Checked
                'tf = tf And Not CheckBoxFindReplaceFindRXSheetmetal.Checked
                'If tf Then
                '    If Not msg.Contains("Replacement search type 'RX' requires Find search type 'RX'") Then
                '        msg += "    Replacement search type 'RX' requires Find search type 'RX'" + Chr(13)
                '    End If
                'End If

            End If

            If LabelToActionSheetmetal(Label).RequiresPictorialView Then
                tf = RadioButtonPictorialViewDimetric.Checked
                tf = tf Or RadioButtonPictorialViewIsometric.Checked
                tf = tf Or RadioButtonPictorialViewTrimetric.Checked

                If Not tf Then
                    If Not msg.Contains("Select a pictorial view orientation on the Configuration tab") Then
                        msg += "    Select a pictorial view orientation on the Configuration tab"
                    End If
                End If
            End If

            If LabelToActionSheetmetal(Label).RequiresForegroundProcessing Then
                If CheckBoxBackgroundProcessing.Checked Then
                    If Not msg.Contains(Label + " cannot be run in a background process") Then
                        msg += "    " + Label + " cannot be run in a background process" + Chr(13)
                    End If
                End If
            End If

            If LabelToActionSheetmetal(Label).RequiresVariablesToEdit Then
                If TextBoxVariablesEditSheetmetal.Text = "" Then
                    msg2 = indent + "Enter as least one Sheetmetal variable to edit/add/expose"
                    If Not msg.Contains(msg2) Then
                        msg += msg2 + Chr(13)
                    End If
                End If
            End If

        Next

        ' Draft Tasks

        For Each Label As String In CheckedListBoxDraft.CheckedItems
            If LabelToActionDraft(Label).RequiresTemplate Then
                If Not FileIO.FileSystem.FileExists(TextBoxTemplateDraft.Text) Then
                    If Not msg.Contains("Select a valid draft template") Then
                        msg += "    Select a valid draft template" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionDraft(Label).RequiresMaterialTable Then
                If Not FileIO.FileSystem.FileExists(TextBoxActiveMaterialLibrary.Text) Then
                    If Not msg.Contains("Select a valid material library") Then
                        msg += "    Select a valid material library" + Chr(13)
                    End If
                End If
            End If
            'If LabelToActionDraft(Label).RequiresLaserOutputDirectory Then
            '    tf = Not FileIO.FileSystem.DirectoryExists(TextBoxLaserOutputDirectory.Text)
            '    tf = tf And Not CheckBoxLaserOutputDirectory.Checked
            '    If tf Then
            '        If Not msg.Contains("Select a valid laser output directory") Then
            '            msg += "    Select a valid laser output directory" + Chr(13)
            '        End If
            '    End If
            'End If
            If LabelToActionDraft(Label).RequiresPartNumberFields Then
                If TextBoxPartNumberPropertyName.Text = "" Then
                    If Not msg.Contains("Select a valid part number property name") Then
                        msg += "    Select a valid part number property name" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionDraft(Label).RequiresSave Then
                SaveMsg += "    Draft: " + Label + Chr(13)
            End If
            If LabelToActionDraft(Label).RequiresSaveAsOutputDirectory Then
                tf = Not FileIO.FileSystem.DirectoryExists(TextBoxSaveAsDraftOutputDirectory.Text)
                tf = tf And Not CheckBoxSaveAsDraftOutputDirectory.Checked
                If tf Then
                    If Not msg.Contains("Select a valid Save As draft output directory") Then
                        msg += "    Select a valid Save As draft output directory" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionDraft(Label).IncompatibleWithOtherTasks Then
                tf = CheckedListBoxDraft.CheckedItems.Count > 1
                tf = tf Or CheckedListBoxAssembly.CheckedItems.Count > 0
                tf = tf Or CheckedListBoxPart.CheckedItems.Count > 0
                tf = tf Or CheckedListBoxSheetmetal.CheckedItems.Count > 0

                If tf Then
                    If Not msg.Contains(Label + "--Draft cannot be performed with any other tasks selected") Then
                        msg += "    " + Label + "--Draft cannot be performed with any other tasks selected" + Chr(13)
                    End If
                End If
            End If
            If LabelToActionDraft(Label).RequiresExternalProgram Then
                If Not FileIO.FileSystem.FileExists(TextBoxExternalProgramDraft.Text) Then
                    If Not msg.Contains("Select a valid draft external program") Then
                        msg += "    Select a valid draft external program" + Chr(13)
                    End If
                Else
                    DestinationDirectory = System.IO.Path.GetDirectoryName(TextBoxExternalProgramDraft.Text)
                    FileSystem.FileCopy(DefaultsFilename, String.Format("{0}\defaults.txt", DestinationDirectory))

                End If
            End If
            If LabelToActionDraft(Label).RequiresPrinter Then
                Dim PD As New PrinterDoctor

                Dim PrinterInstalled As Boolean = False
                Dim InstalledPrinter As String
                Dim NumberCopies As Integer

                For Each InstalledPrinter In PD.GetInstalledPrinterNames
                    If InstalledPrinter = ComboBoxPrinter1.Text Then
                        PrinterInstalled = True
                        Exit For
                    End If
                Next InstalledPrinter
                If Not PrinterInstalled Then
                    If Not msg.Contains("Select a valid Printer1") Then
                        msg += "    Select a valid Printer1" + Chr(13)
                    End If
                End If
                Try
                    NumberCopies = CInt(TextBoxPrinter1Copies.Text)
                Catch ex As Exception
                    If Not msg.Contains("Set Printer1 Copies to a number") Then
                        msg += "    Set Printer1 Copies to a number" + Chr(13)
                    End If
                End Try

                If (Not CheckBoxEnablePrinter1.Checked) And (Not CheckBoxEnablePrinter2.Checked) Then
                    If Not msg.Contains("Enable at least one printer") Then
                        msg += "    Enable at least one printer" + Chr(13)
                    End If
                End If

                If CheckBoxEnablePrinter2.Checked Then
                    For Each InstalledPrinter In PD.GetInstalledPrinterNames
                        If InstalledPrinter = ComboBoxPrinter2.Text Then
                            PrinterInstalled = True
                            Exit For
                        End If
                    Next InstalledPrinter
                    If Not PrinterInstalled Then
                        If Not msg.Contains("Select a valid Printer2, or disable it") Then
                            msg += "    Select a valid Printer2, or disable it" + Chr(13)
                        End If
                    End If

                    Try
                        NumberCopies = CInt(TextBoxPrinter2Copies.Text)
                    Catch ex As Exception
                        If Not msg.Contains("Set Printer2 Copies to a number") Then
                            msg += "    Set Printer2 Copies to a number" + Chr(13)
                        End If
                    End Try

                    If TextBoxPrinter2SheetSelections.Text.Trim = "" Then
                        If Not msg.Contains("Select at least one sheet size for Printer2") Then
                            msg += "    Select at least one sheet size for Printer2" + Chr(13)
                        End If
                    End If

                End If

            End If

            If LabelToActionDraft(Label).RequiresForegroundProcessing Then
                If CheckBoxBackgroundProcessing.Checked Then
                    If Not msg.Contains(Label + " cannot be run in a background process") Then
                        msg += "    " + Label + " cannot be run in a background process" + Chr(13)
                    End If
                End If
            End If

            If LabelToActionDraft(Label).RequiresFindReplaceFields Then
                If (TextBoxPropertiesEditDraft.Text = "") Or (TextBoxPropertiesEditDraft.Text = "{}") Then
                    If Not msg.Contains("Enter one or more draft properties to change") Then
                        msg += "    Enter one or more draft properties to change" + Chr(13)
                    End If
                End If


            End If
        Next

        If Len(msg) <> 0 Then
            msg = "Please correct the following before continuing" + Chr(13) + msg
        End If

        If (Len(SaveMsg) <> 0) And CheckBoxWarnSave.Checked Then
            Dim s As String = "The following options require the original file to be saved." + Chr(13)
            s += "Please verify you have a backup before continuing."
            SaveMsg += Chr(13) + "Disable this warning on the Configuration tab."
            SaveMsg = s + Chr(13) + SaveMsg + Chr(13) + Chr(13)
        Else
            SaveMsg = ""
        End If

        Return SaveMsg + msg
    End Function

    Private Sub UpdateTimeRemaining()
        Dim ElapsedTime As Double
        Dim RemainingTime As Double
        Dim TotalEstimatedTime As Double
        Dim ElapsedTimeString As String
        Dim RemainingTimeString As String

        If FilesToProcessCompleted > 2 Then
            ElapsedTime = Now.Subtract(StartTime).TotalMinutes

            TotalEstimatedTime = ElapsedTime * CDbl(FilesToProcessTotal) / CDbl(FilesToProcessCompleted)
            RemainingTime = TotalEstimatedTime - ElapsedTime

            If ElapsedTime < 60 Then
                ElapsedTimeString = String.Format("{0} min.", ElapsedTime.ToString("0.0"))
            Else
                ElapsedTimeString = String.Format("{0} hr.", (ElapsedTime / 60).ToString("0.0"))
            End If

            If RemainingTime < 60 Then
                RemainingTimeString = String.Format("{0} min.", RemainingTime.ToString("0.0"))
            Else
                RemainingTimeString = String.Format("{0} hr.", (RemainingTime / 60).ToString("0.0"))
            End If

            If RemainingTime < 0.1 Then
                LabelTimeRemaining.Text = ""
            Else
                LabelTimeRemaining.Text = String.Format("Time elapsed: {0}, Time remaining: {1}", ElapsedTimeString, RemainingTimeString)
            End If


        End If
    End Sub

    Private Sub ProcessFiles(ByVal Filetype As String)
        Dim FilesToProcess As List(Of String)
        Dim FileToProcess As String
        Dim RestartAfter As Integer
        Dim msg As String
        Dim ErrorMessagesCombined As New Dictionary(Of String, List(Of String))

        Dim DMApp As DesignManager.Application = Nothing
        If CheckBoxProcessReadOnly.Checked Then
            DMApp = New DesignManager.Application
            DMApp.Visible = 1
            SEApp.Activate()
        End If

        Try
            RestartAfter = CInt(TextBoxRestartAfter.Text)
        Catch ex As Exception
            RestartAfter = 50
            TextBoxRestartAfter.Text = CStr(50)
        End Try

        If Filetype = "Assembly" Then
            FilesToProcess = GetFileNames("*.asm")
        ElseIf Filetype = "Part" Then
            FilesToProcess = GetFileNames("*.par")
        ElseIf Filetype = "Sheetmetal" Then
            FilesToProcess = GetFileNames("*.psm")
        ElseIf Filetype = "Draft" Then
            FilesToProcess = GetFileNames("*.dft")
        Else
            MsgBox("In ProcessFiles(), Filetype not recognized: " + Filetype + ".  Exiting...")
            SEApp.Quit()
            End
        End If

        For Each FileToProcess In FilesToProcess

            For Each tmpItem As ListViewItem In ListViewFiles.Items
                If tmpItem.Name = FileToProcess Then
                    tmpItem.EnsureVisible()
                    Exit For
                End If
            Next

            System.Windows.Forms.Application.DoEvents()
            If StopProcess Then
                TextBoxStatus.Text = "Processing aborted"
                If CheckBoxProcessReadOnly.Checked Then
                    DMApp.Quit()
                End If
                Exit Sub
            End If

            FilesToProcessCompleted += 1

            If FilesToProcessCompleted Mod RestartAfter = 0 Then
                SEStop()
                SEStart()
            End If

            msg = FilesToProcessCompleted.ToString + "/" + FilesToProcessTotal.ToString + " "
            'msg += CommonTasks.TruncateFullPath(FileToProcess, Nothing)
            msg += System.IO.Path.GetFileName(FileToProcess)
            TextBoxStatus.Text = msg

            ErrorMessagesCombined = ProcessFile(FileToProcess, Filetype, DMApp)

            If ErrorMessagesCombined.Count > 0 Then
                LogfileAppend(FileToProcess, ErrorMessagesCombined)
                ListViewFiles.Items.Item(FileToProcess).ImageKey = "Error"
            Else
                ListViewFiles.Items.Item(FileToProcess).ImageKey = "Checked"
            End If

        Next

        If CheckBoxProcessReadOnly.Checked Then
            DMApp.Quit()
        End If

    End Sub

    Private Function ProcessFile(
        ByVal Path As String,
        ByVal Filetype As String,
        DMApp As DesignManager.Application
        ) As Dictionary(Of String, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim ExitStatus As Integer
        'Dim SupplementalErrorMessage As String
        Dim ErrorMessagesCombined As New Dictionary(Of String, List(Of String))

        Dim LabelText As String = ""
        Dim SEDoc As SolidEdgeFramework.SolidEdgeDocument = Nothing
        Dim CheckedListBoxX As CheckedListBox
        Dim ModifiedFilename As String = ""
        Dim OriginalFilename As String = ""
        Dim RemnantsFilename As String = ""

        Dim ActiveWindow As SolidEdgeFramework.Window
        Dim ActiveSheetWindow As SolidEdgeDraft.SheetWindow

        ' Account for infrequent malfunctions on a large number of files.
        TotalAborts -= 0.1
        If TotalAborts < 0 Then
            TotalAborts = 0
        End If

        Dim OldStatus As SolidEdgeConstants.DocumentStatus
        Dim StatusChangeSuccessful As Boolean
        'Dim DMApp As DesignManager.Application = Nothing

        If CheckBoxProcessReadOnly.Checked Then
            'DMApp = New DesignManager.Application
            'DMApp.Visible = 1

            OldStatus = CommonTasks.GetStatus(DMApp, Path)

            '' For some reason if OldStatus is igAvailable, OldStatus = Nothing is True
            'If OldStatus = Nothing Then
            '    ErrorMessagesCombined("Unable to read document Status") = New List(Of String) From {""}
            'End If

            StatusChangeSuccessful = CommonTasks.SetStatus(DMApp, Path, SolidEdgeConstants.DocumentStatus.igStatusAvailable)
            If Not StatusChangeSuccessful Then
                ErrorMessagesCombined("Change status to Available did not succeed") = New List(Of String) From {""}
            End If

            SEApp.DoIdle()
        End If

        Try
            If (CheckBoxBackgroundProcessing.Checked) And (Not Filetype = "Assembly") Then
                SEDoc = SolidEdgeCommunity.Extensions.DocumentsExtensions.OpenInBackground(Of SolidEdgeFramework.SolidEdgeDocument)(SEApp.Documents, Path)

                ' Here is the same functionality without using the SolidEdgeCommunity dependency
                ' https://blogs.sw.siemens.com/solidedge/how-to-open-documents-silently/
                ' Dim JDOCUMENTPROP_NOWINDOW As UInt16 = 8
                ' SEDoc = DirectCast(SEApp.Documents.Open(Path, JDOCUMENTPROP_NOWINDOW), SolidEdgeFramework.SolidEdgeDocument)

            Else
                SEDoc = DirectCast(SEApp.Documents.Open(Path), SolidEdgeFramework.SolidEdgeDocument)
                SEDoc.Activate()

                ' Maximize the window in the application
                If Filetype = "Draft" Then
                    ActiveSheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
                    ActiveSheetWindow.WindowState = 2
                Else
                    ActiveWindow = CType(SEApp.ActiveWindow, SolidEdgeFramework.Window)
                    ActiveWindow.WindowState = 2  '0 normal, 1 minimized, 2 maximized
                End If
            End If

            SEApp.DoIdle()

            If Filetype = "Assembly" Then
                CheckedListBoxX = CheckedListBoxAssembly
            ElseIf Filetype = "Part" Then
                CheckedListBoxX = CheckedListBoxPart
            ElseIf Filetype = "Sheetmetal" Then
                CheckedListBoxX = CheckedListBoxSheetmetal
            ElseIf Filetype = "Draft" Then
                CheckedListBoxX = CheckedListBoxDraft
            Else
                MsgBox("In ProcessFile(), Filetype not recognized: " + Filetype + ".  Exiting...")
                SEApp.Quit()
                End
            End If

            For Each LabelText In CheckedListBoxX.CheckedItems
                If Filetype = "Assembly" Then
                    ErrorMessage = LaunchTask.Launch(SEDoc, Configuration, SEApp, Filetype, LabelToActionAssembly, LabelText)
                ElseIf Filetype = "Part" Then
                    ErrorMessage = LaunchTask.Launch(SEDoc, Configuration, SEApp, Filetype, LabelToActionPart, LabelText)
                ElseIf Filetype = "Sheetmetal" Then
                    ErrorMessage = LaunchTask.Launch(SEDoc, Configuration, SEApp, Filetype, LabelToActionSheetmetal, LabelText)
                Else
                    ErrorMessage = LaunchTask.Launch(SEDoc, Configuration, SEApp, Filetype, LabelToActionDraft, LabelText)
                End If

                ExitStatus = ErrorMessage.Keys(0)
                'SupplementalErrorMessage = ErrorMessage(1)

                If ExitStatus <> 0 Then
                    ErrorMessagesCombined(LabelText) = ErrorMessage(ErrorMessage.Keys(0))

                    If ExitStatus = 99 Then
                        StopProcess = True
                    End If
                    'LogfileAppend(LabelText, TruncateFullPath(Path), SupplementalErrorMessage)
                End If
            Next

            'If LabelText = "Update styles from template" Then
            '    OriginalFilename = SEDoc.FullName
            '    ModifiedFilename = String.Format("{0}\{1}-Housekeeper.dft",
            '                                System.IO.Path.GetDirectoryName(SEDoc.FullName),
            '                                System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))
            '    RemnantsFilename = ModifiedFilename.Replace("Housekeeper", "HousekeeperOld")
            'End If

            SEDoc.Close(False)
            SEApp.DoIdle()

            If CheckBoxProcessReadOnly.Checked Then
                If RadioButtonReadOnlyRevert.Checked Then
                    If Not OldStatus = SolidEdgeConstants.DocumentStatus.igStatusAvailable Then
                        StatusChangeSuccessful = CommonTasks.SetStatus(DMApp, Path, OldStatus)
                        If Not StatusChangeSuccessful Then
                            ErrorMessagesCombined(
                            String.Format("Change status to '{0}' did not succeed", OldStatus.ToString)
                            ) = New List(Of String) From {""}
                        End If
                    End If
                End If

                If RadioButtonReadOnlyChange.Checked Then
                    Dim NewStatus As SolidEdgeConstants.DocumentStatus

                    Dim StatusChangedCheckedRadioButtons As New List(Of RadioButton)
                    StatusChangedCheckedRadioButtons = GetStatusChangeRadioButtons(True)

                    Dim FromStatus As String = ""
                    Dim ToStatus As String = ""

                    ' RadioButtonStatusAtoA, A, B, IR, IW, O, R
                    If OldStatus = SolidEdgeConstants.DocumentStatus.igStatusAvailable Then
                        FromStatus = "RadioButtonStatusAto"
                    End If
                    If OldStatus = SolidEdgeConstants.DocumentStatus.igStatusBaselined Then
                        FromStatus = "RadioButtonStatusBto"
                    End If
                    If OldStatus = SolidEdgeConstants.DocumentStatus.igStatusInReview Then
                        FromStatus = "RadioButtonStatusIRto"
                    End If
                    If OldStatus = SolidEdgeConstants.DocumentStatus.igStatusInWork Then
                        FromStatus = "RadioButtonStatusIWto"
                    End If
                    If OldStatus = SolidEdgeConstants.DocumentStatus.igStatusObsolete Then
                        FromStatus = "RadioButtonStatusOto"
                    End If
                    If OldStatus = SolidEdgeConstants.DocumentStatus.igStatusReleased Then
                        FromStatus = "RadioButtonStatusRto"
                    End If

                    For Each RB As RadioButton In StatusChangedCheckedRadioButtons
                        If RB.Name.Contains(FromStatus) Then
                            ToStatus = RB.Name.Replace(FromStatus, "")
                        End If
                    Next

                    If ToStatus = "A" Then
                        NewStatus = SolidEdgeConstants.DocumentStatus.igStatusAvailable
                    End If
                    If ToStatus = "B" Then
                        NewStatus = SolidEdgeConstants.DocumentStatus.igStatusBaselined
                    End If
                    If ToStatus = "IR" Then
                        NewStatus = SolidEdgeConstants.DocumentStatus.igStatusInReview
                    End If
                    If ToStatus = "IW" Then
                        NewStatus = SolidEdgeConstants.DocumentStatus.igStatusInWork
                    End If
                    If ToStatus = "O" Then
                        NewStatus = SolidEdgeConstants.DocumentStatus.igStatusObsolete
                    End If
                    If ToStatus = "R" Then
                        NewStatus = SolidEdgeConstants.DocumentStatus.igStatusReleased
                    End If

                    StatusChangeSuccessful = CommonTasks.SetStatus(DMApp, Path, NewStatus)
                    If Not StatusChangeSuccessful Then
                        ErrorMessagesCombined(
                            String.Format("Change status to '{0}' did not succeed", NewStatus.ToString)
                            ) = New List(Of String) From {""}
                    End If

                End If

                'DMApp.Quit()

            End If


            'If LabelText = "Update styles from template" Then
            '    If ExitStatus = 0 Then
            '        System.IO.File.Delete(OriginalFilename)
            '        FileSystem.Rename(ModifiedFilename, OriginalFilename)
            '    ElseIf ExitStatus = 1 Then
            '        If System.IO.File.Exists(RemnantsFilename) Then
            '            System.IO.File.Delete(RemnantsFilename)
            '        End If
            '        If System.IO.File.Exists(ModifiedFilename) Then  ' Not created if a task-generated file was processed
            '            FileSystem.Rename(OriginalFilename, RemnantsFilename)
            '            FileSystem.Rename(ModifiedFilename, OriginalFilename)
            '        End If
            '    ElseIf ExitStatus = 2 Then
            '        ' Nothing was saved.  Leave SEDoc unmodified.
            '    End If

            'End If

        Catch ex As Exception
            Dim AbortList As New List(Of String)

            AbortList.Add(ex.ToString)

            TotalAborts += 1
            If TotalAborts >= TotalAbortsMaximum Then
                StopProcess = True
                AbortList.Add(String.Format("Total aborts exceed maximum of {0}.  Exiting...", TotalAbortsMaximum))
            Else
                SEStop()
                SEStart()
            End If
            ErrorMessagesCombined("Error processing file") = AbortList
        End Try

        UpdateTimeRemaining()

        Return ErrorMessagesCombined
    End Function

    'Private Function ProcessFile(ByVal Path As String, ByVal Filetype As String) As Dictionary(Of String, List(Of String))
    '    Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
    '    Dim ExitStatus As Integer
    '    'Dim SupplementalErrorMessage As String
    '    Dim ErrorMessagesCombined As New Dictionary(Of String, List(Of String))

    '    Dim LabelText As String = ""
    '    Dim SEDoc As SolidEdgeFramework.SolidEdgeDocument = Nothing
    '    Dim CheckedListBoxX As CheckedListBox
    '    Dim ModifiedFilename As String = ""
    '    Dim OriginalFilename As String = ""
    '    Dim RemnantsFilename As String = ""

    '    Dim ActiveWindow As SolidEdgeFramework.Window
    '    Dim ActiveSheetWindow As SolidEdgeDraft.SheetWindow

    '    ' Account for infrequent malfunctions on a large number of files.
    '    TotalAborts -= 0.1
    '    If TotalAborts < 0 Then
    '        TotalAborts = 0
    '    End If

    '    Try

    '        SEDoc = SolidEdgeCommunity.Extensions.DocumentsExtensions.OpenInBackground(Of SolidEdgeFramework.SolidEdgeDocument)(SEApp.Documents, Path)

    '        'SEDoc = DirectCast(SEApp.Documents.Open(Path), SolidEdgeFramework.SolidEdgeDocument)
    '        'SEDoc.Activate()
    '        SEApp.DoIdle()

    '        ' Maximize the window in the application
    '        'If Filetype = "Draft" Then
    '        '    ActiveSheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
    '        '    ActiveSheetWindow.WindowState = 2
    '        'Else
    '        '    ActiveWindow = CType(SEApp.ActiveWindow, SolidEdgeFramework.Window)
    '        '    ActiveWindow.WindowState = 2
    '        '    'ActiveWindow.WindowState = 0  '0 normal, 1 minimized, 2 maximized
    '        '    'Dim h As Integer = ActiveWindow.Height
    '        '    'Dim w As Integer = ActiveWindow.Width
    '        'End If


    '        If Filetype = "Assembly" Then
    '            CheckedListBoxX = CheckedListBoxAssembly
    '        ElseIf Filetype = "Part" Then
    '            CheckedListBoxX = CheckedListBoxPart
    '        ElseIf Filetype = "Sheetmetal" Then
    '            CheckedListBoxX = CheckedListBoxSheetmetal
    '        ElseIf Filetype = "Draft" Then
    '            CheckedListBoxX = CheckedListBoxDraft
    '        Else
    '            MsgBox("In ProcessFile(), Filetype not recognized: " + Filetype + ".  Exiting...")
    '            SEApp.Quit()
    '            End
    '        End If

    '        For Each LabelText In CheckedListBoxX.CheckedItems
    '            If Filetype = "Assembly" Then
    '                ErrorMessage = LaunchTask.Launch(SEDoc, Configuration, SEApp, Filetype, LabelToActionAssembly, LabelText)
    '            ElseIf Filetype = "Part" Then
    '                ErrorMessage = LaunchTask.Launch(SEDoc, Configuration, SEApp, Filetype, LabelToActionPart, LabelText)
    '            ElseIf Filetype = "Sheetmetal" Then
    '                ErrorMessage = LaunchTask.Launch(SEDoc, Configuration, SEApp, Filetype, LabelToActionSheetmetal, LabelText)
    '            Else
    '                ErrorMessage = LaunchTask.Launch(SEDoc, Configuration, SEApp, Filetype, LabelToActionDraft, LabelText)
    '            End If

    '            ExitStatus = ErrorMessage.Keys(0)
    '            'SupplementalErrorMessage = ErrorMessage(1)

    '            If ExitStatus <> 0 Then
    '                ErrorMessagesCombined(LabelText) = ErrorMessage(ErrorMessage.Keys(0))

    '                If ExitStatus = 99 Then
    '                    StopProcess = True
    '                End If
    '                'LogfileAppend(LabelText, TruncateFullPath(Path), SupplementalErrorMessage)
    '            End If
    '        Next

    '        'If LabelText = "Update styles from template" Then
    '        '    OriginalFilename = SEDoc.FullName
    '        '    ModifiedFilename = String.Format("{0}\{1}-Housekeeper.dft",
    '        '                                System.IO.Path.GetDirectoryName(SEDoc.FullName),
    '        '                                System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))
    '        '    RemnantsFilename = ModifiedFilename.Replace("Housekeeper", "HousekeeperOld")
    '        'End If

    '        SEDoc.Close(False)
    '        SEApp.DoIdle()

    '        'If LabelText = "Update styles from template" Then
    '        '    If ExitStatus = 0 Then
    '        '        System.IO.File.Delete(OriginalFilename)
    '        '        FileSystem.Rename(ModifiedFilename, OriginalFilename)
    '        '    ElseIf ExitStatus = 1 Then
    '        '        If System.IO.File.Exists(RemnantsFilename) Then
    '        '            System.IO.File.Delete(RemnantsFilename)
    '        '        End If
    '        '        If System.IO.File.Exists(ModifiedFilename) Then  ' Not created if a task-generated file was processed
    '        '            FileSystem.Rename(OriginalFilename, RemnantsFilename)
    '        '            FileSystem.Rename(ModifiedFilename, OriginalFilename)
    '        '        End If
    '        '    ElseIf ExitStatus = 2 Then
    '        '        ' Nothing was saved.  Leave SEDoc unmodified.
    '        '    End If

    '        'End If

    '    Catch ex As Exception
    '        Dim AbortList As New List(Of String)

    '        AbortList.Add(ex.ToString)

    '        TotalAborts += 1
    '        If TotalAborts >= TotalAbortsMaximum Then
    '            StopProcess = True
    '            AbortList.Add(String.Format("Total aborts exceed maximum of {0}.  Exiting...", TotalAbortsMaximum))
    '        Else
    '            SEStop()
    '            SEStart()
    '        End If
    '        ErrorMessagesCombined("Error processing file") = AbortList
    '    End Try

    '    UpdateTimeRemaining()

    '    Return ErrorMessagesCombined
    'End Function



    Private Sub Startup()

        CreatePreferencesFolder()
        PopulateCheckedListBoxes()
        LoadDefaults()
        ' LoadPrinterSettings()
        ReconcileFormChanges()
        BuildReadmeFile()

        CarIcona()

        Dim ListViewGroup1 As New ListViewGroup("Files sources", HorizontalAlignment.Left)
        ListViewGroup1.Name = "Sources"
        Dim ListViewGroup2 As New ListViewGroup("Excluded files", HorizontalAlignment.Left)
        ListViewGroup2.Name = "Excluded"
        Dim ListViewGroup3 As New ListViewGroup("Assemblies", HorizontalAlignment.Left)
        ListViewGroup3.Name = ".asm"
        Dim ListViewGroup4 As New ListViewGroup("Parts", HorizontalAlignment.Left)
        ListViewGroup4.Name = ".par"
        Dim ListViewGroup5 As New ListViewGroup("Sheetmetals", HorizontalAlignment.Left)
        ListViewGroup5.Name = ".psm"
        Dim ListViewGroup6 As New ListViewGroup("Drafts", HorizontalAlignment.Left)
        ListViewGroup6.Name = ".dft"
        ListViewFiles.Groups.Add(ListViewGroup1)
        ListViewFiles.Groups.Add(ListViewGroup2)
        ListViewFiles.Groups.Add(ListViewGroup3)
        ListViewFiles.Groups.Add(ListViewGroup4)
        ListViewFiles.Groups.Add(ListViewGroup5)
        ListViewFiles.Groups.Add(ListViewGroup6)

        LinkLabelGitHubReadme.Text = "Help is now hosted on GitHub"
        Dim StartIdx As Integer = Len(LinkLabelGitHubReadme.Text) - 6
        Dim EndIdx As Integer = Len(LinkLabelGitHubReadme.Text) - 1
        LinkLabelGitHubReadme.Links.Add(StartIdx, EndIdx, "https://github.com/rmcanany/SolidEdgeHousekeeper#readme")

        Me.Text = "Solid Edge Housekeeper 2024.1"

        new_CheckBoxFileSearch.Checked = False
        new_ComboBoxFileSearch.Enabled = False
        new_CheckBoxEnablePropertyFilter.Checked = False
        new_ButtonPropertyFilter.Enabled = False

        If RadioButtonListSortDependency.Checked Then
            CheckBoxListIncludeNoDependencies.Enabled = True
        Else
            CheckBoxListIncludeNoDependencies.Enabled = False
        End If

        If RadioButtonListSortRandomSample.Checked Then
            TextBoxRandomSampleFraction.Enabled = True
        Else
            TextBoxRandomSampleFraction.Enabled = False
        End If

        ListViewFilesOutOfDate = False
        BT_Update.BackColor = Color.FromName("Control")


    End Sub


    Private Sub PopulateCheckedListBoxes()

        CheckedListBoxAssembly.Items.Clear()
        For Each Key In LabelToActionAssembly.Keys
            CheckedListBoxAssembly.Items.Add(Key)
        Next

        CheckedListBoxPart.Items.Clear()
        For Each Key In LabelToActionPart.Keys
            CheckedListBoxPart.Items.Add(Key)
        Next

        CheckedListBoxSheetmetal.Items.Clear()
        For Each Key In LabelToActionSheetmetal.Keys
            CheckedListBoxSheetmetal.Items.Add(Key)
        Next

        CheckedListBoxDraft.Items.Clear()
        For Each Key In LabelToActionDraft.Keys
            CheckedListBoxDraft.Items.Add(Key)
        Next
    End Sub

    Private Function EvaluateBoolean(formula As String) As Boolean
        ' https://stackoverflow.com/questions/49005926/conversion-from-string-to-boolean-vb-net
        Dim sc As New MSScriptControl.ScriptControl
        'SET LANGUAGE TO VBSCRIPT
        sc.Language = "VBSCRIPT"
        'ATTEMPT MATH
        Try
            Return Convert.ToBoolean(sc.Eval(formula))
        Catch ex As Exception
            'SHOW THAT IT WAS INVALID
            ' MessageBox.Show("Invalid Boolean expression")
            Return (False)
        End Try
    End Function

    Private Sub ReconcileFormChanges(Optional UpdateFileList As Boolean = False)

        ' Update configuration
        Configuration = GetConfiguration()

        Dim backcolor As New Color
        backcolor = BT_Update.BackColor

        BT_Update.BackColor = Color.FromName("Control")

        If ListViewFilesOutOfDate Then
            BT_Update.BackColor = Color.Orange
        Else
            BT_Update.BackColor = Color.FromName("Control")
        End If

        If Not CheckBoxProcessReadOnly.Checked Then
            Dim StatusChangeRadioButtons As New List(Of RadioButton)
            Dim RB As RadioButton

            StatusChangeRadioButtons = GetStatusChangeRadioButtons()

            RadioButtonReadOnlyRevert.Enabled = False
            RadioButtonReadOnlyChange.Enabled = False
            For Each RB In StatusChangeRadioButtons
                RB.Enabled = False
            Next
        End If

        If Not CheckBoxEnablePrinter1.Checked Then
            ComboBoxPrinter1.Enabled = False
            TextBoxPrinter1Copies.Enabled = False
            CheckBoxPrinter1AutoOrient.Enabled = False
            CheckBoxPrinter1BestFit.Enabled = False
            CheckBoxPrinter1PrintAsBlack.Enabled = False
            CheckBoxPrinter1ScaleLineTypes.Enabled = False
            CheckBoxPrinter1ScaleLineWidths.Enabled = False
        End If

        If Not CheckBoxEnablePrinter2.Checked Then
            ComboBoxPrinter2.Enabled = False
            TextBoxPrinter2Copies.Enabled = False
            CheckBoxPrinter2AutoOrient.Enabled = False
            CheckBoxPrinter2BestFit.Enabled = False
            CheckBoxPrinter2PrintAsBlack.Enabled = False
            CheckBoxPrinter2ScaleLineTypes.Enabled = False
            CheckBoxPrinter2ScaleLineWidths.Enabled = False

            RadioButtonSheetsAnsiPrinter2.Enabled = False
            RadioButtonSheetsIsoPrinter2.Enabled = False
            RadioButtonSheetsAllPrinter2.Enabled = False
            TextBoxPrinter2SheetSelections.Enabled = False
            ButtonPrinter2SheetSelections.Enabled = False
        End If

        If CheckBoxStockSizeXYZ.Checked Then
            LabelStockSizeXYZ.Enabled = True
            LabelStockSizeX.Enabled = True
            LabelStockSizeY.Enabled = True
            LabelStockSizeZ.Enabled = True
            TextBoxStockSizeX.Enabled = True
            TextBoxStockSizeY.Enabled = True
            TextBoxStockSizeZ.Enabled = True
        Else
            LabelStockSizeXYZ.Enabled = False
            LabelStockSizeX.Enabled = False
            LabelStockSizeY.Enabled = False
            LabelStockSizeZ.Enabled = False
            TextBoxStockSizeX.Enabled = False
            TextBoxStockSizeY.Enabled = False
            TextBoxStockSizeZ.Enabled = False
        End If

        If CheckBoxStockSizeMinMidMax.Checked Then
            LabelStockSizeMin.Enabled = True
            LabelStockSizeMid.Enabled = True
            LabelStockSizeMax.Enabled = True
            TextBoxStockSizeMin.Enabled = True
            TextBoxStockSizeMid.Enabled = True
            TextBoxStockSizeMax.Enabled = True
        Else
            LabelStockSizeMin.Enabled = False
            LabelStockSizeMid.Enabled = False
            LabelStockSizeMax.Enabled = False
            TextBoxStockSizeMin.Enabled = False
            TextBoxStockSizeMid.Enabled = False
            TextBoxStockSizeMax.Enabled = False
        End If
        ' Enable/Disable option controls based on task selection

        ' ASSEMBLY

        ComboBoxSaveAsAssemblyFileType.Enabled = False
        CheckBoxSaveAsAssemblyOutputDirectory.Enabled = False
        TextBoxSaveAsAssemblyOutputDirectory.Enabled = False
        ButtonSaveAsAssemblyOutputDirectory.Enabled = False

        CheckBoxSaveAsFormulaAssembly.Enabled = False
        TextBoxSaveAsFormulaAssembly.Enabled = False

        TextBoxExternalProgramAssembly.Enabled = False
        ButtonExternalProgramAssembly.Enabled = False

        'ComboBoxFindReplacePropertySetAssembly.Enabled = False
        'TextBoxFindReplacePropertyNameAssembly.Enabled = False
        'TextBoxFindReplaceFindAssembly.Enabled = False
        'TextBoxFindReplaceReplaceAssembly.Enabled = False
        'CheckBoxFindReplaceFindPTAssembly.Enabled = False
        'CheckBoxFindReplaceFindWCAssembly.Enabled = False
        'CheckBoxFindReplaceFindRXAssembly.Enabled = False
        'CheckBoxFindReplaceReplacePTAssembly.Enabled = False
        'CheckBoxFindReplaceReplaceRXAssembly.Enabled = False

        TextBoxPropertiesEditAssembly.Enabled = False
        ButtonPropertiesEditAssembly.Enabled = False

        TextBoxVariablesEditAssembly.Enabled = False
        ButtonVariablesEditAssembly.Enabled = False

        For Each Label As String In CheckedListBoxAssembly.CheckedItems

            If LabelToActionAssembly(Label).RequiresSaveAsOutputDirectory Then
                ComboBoxSaveAsAssemblyFileType.Enabled = True
                CheckBoxSaveAsAssemblyOutputDirectory.Enabled = True

                If CheckBoxSaveAsAssemblyOutputDirectory.Checked Then
                    TextBoxSaveAsAssemblyOutputDirectory.Enabled = False
                    ButtonSaveAsAssemblyOutputDirectory.Enabled = False
                Else
                    TextBoxSaveAsAssemblyOutputDirectory.Enabled = True
                    ButtonSaveAsAssemblyOutputDirectory.Enabled = True
                    CheckBoxSaveAsFormulaAssembly.Enabled = True
                    If CheckBoxSaveAsFormulaAssembly.Checked = True Then
                        TextBoxSaveAsFormulaAssembly.Enabled = True
                    End If
                End If

            End If

            If LabelToActionAssembly(Label).RequiresExternalProgram Then
                TextBoxExternalProgramAssembly.Enabled = True
                ButtonExternalProgramAssembly.Enabled = True
            End If

            If LabelToActionAssembly(Label).RequiresFindReplaceFields Then
                'ComboBoxFindReplacePropertySetAssembly.Enabled = True
                'TextBoxFindReplacePropertyNameAssembly.Enabled = True
                'TextBoxFindReplaceFindAssembly.Enabled = True
                'TextBoxFindReplaceReplaceAssembly.Enabled = True
                'CheckBoxFindReplaceFindPTAssembly.Enabled = True
                'CheckBoxFindReplaceFindWCAssembly.Enabled = True
                'CheckBoxFindReplaceFindRXAssembly.Enabled = True
                'CheckBoxFindReplaceReplacePTAssembly.Enabled = True
                'CheckBoxFindReplaceReplaceRXAssembly.Enabled = True

                TextBoxPropertiesEditAssembly.Enabled = True
                ButtonPropertiesEditAssembly.Enabled = True

            End If

            If LabelToActionAssembly(Label).RequiresVariablesToEdit Then
                TextBoxVariablesEditAssembly.Enabled = True
                ButtonVariablesEditAssembly.Enabled = True
            End If

        Next

        ' PART

        ComboBoxSaveAsPartFileType.Enabled = False
        CheckBoxSaveAsPartOutputDirectory.Enabled = False
        TextBoxSaveAsPartOutputDirectory.Enabled = False
        ButtonSaveAsPartOutputDirectory.Enabled = False

        CheckBoxSaveAsFormulaPart.Enabled = False
        TextBoxSaveAsFormulaPart.Enabled = False

        TextBoxExternalProgramPart.Enabled = False
        ButtonExternalProgramPart.Enabled = False

        'ComboBoxFindReplacePropertySetPart.Enabled = False
        'TextBoxFindReplacePropertyNamePart.Enabled = False
        'TextBoxFindReplaceFindPart.Enabled = False
        'TextBoxFindReplaceReplacePart.Enabled = False
        'CheckBoxFindReplaceFindPTPart.Enabled = False
        'CheckBoxFindReplaceFindWCPart.Enabled = False
        'CheckBoxFindReplaceFindRXPart.Enabled = False
        'CheckBoxFindReplaceReplacePTPart.Enabled = False
        'CheckBoxFindReplaceReplaceRXPart.Enabled = False
        TextBoxPropertiesEditPart.Enabled = False
        ButtonPropertiesEditPart.Enabled = False


        TextBoxVariablesEditPart.Enabled = False
        ButtonVariablesEditPart.Enabled = False

        For Each Label As String In CheckedListBoxPart.CheckedItems

            If LabelToActionPart(Label).RequiresSaveAsOutputDirectory Then
                ComboBoxSaveAsPartFileType.Enabled = True
                CheckBoxSaveAsPartOutputDirectory.Enabled = True

                If CheckBoxSaveAsPartOutputDirectory.Checked Then
                    TextBoxSaveAsPartOutputDirectory.Enabled = False
                    ButtonSaveAsPartOutputDirectory.Enabled = False
                Else
                    TextBoxSaveAsPartOutputDirectory.Enabled = True
                    ButtonSaveAsPartOutputDirectory.Enabled = True
                    CheckBoxSaveAsFormulaPart.Enabled = True
                    If CheckBoxSaveAsFormulaPart.Checked = True Then
                        TextBoxSaveAsFormulaPart.Enabled = True
                    End If
                End If

            End If

            If LabelToActionPart(Label).RequiresExternalProgram Then
                TextBoxExternalProgramPart.Enabled = True
                ButtonExternalProgramPart.Enabled = True
            End If

            If LabelToActionPart(Label).RequiresFindReplaceFields Then
                'ComboBoxFindReplacePropertySetPart.Enabled = True
                'TextBoxFindReplacePropertyNamePart.Enabled = True
                'TextBoxFindReplaceFindPart.Enabled = True
                'TextBoxFindReplaceReplacePart.Enabled = True
                'CheckBoxFindReplaceFindPTPart.Enabled = True
                'CheckBoxFindReplaceFindWCPart.Enabled = True
                'CheckBoxFindReplaceFindRXPart.Enabled = True
                'CheckBoxFindReplaceReplacePTPart.Enabled = True
                'CheckBoxFindReplaceReplaceRXPart.Enabled = True
                TextBoxPropertiesEditPart.Enabled = True
                ButtonPropertiesEditPart.Enabled = True

            End If

            If LabelToActionPart(Label).RequiresVariablesToEdit Then
                TextBoxVariablesEditPart.Enabled = True
                ButtonVariablesEditPart.Enabled = True
            End If

        Next

        ' SHEETMETAL

        ComboBoxSaveAsSheetmetalFileType.Enabled = False
        CheckBoxSaveAsSheetmetalOutputDirectory.Enabled = False
        TextBoxSaveAsSheetmetalOutputDirectory.Enabled = False
        ButtonSaveAsSheetmetalOutputDirectory.Enabled = False

        CheckBoxSaveAsFormulaSheetmetal.Enabled = False
        TextBoxSaveAsFormulaSheetmetal.Enabled = False

        TextBoxExternalProgramSheetmetal.Enabled = False
        ButtonExternalProgramSheetmetal.Enabled = False

        'ComboBoxFindReplacePropertySetSheetmetal.Enabled = False
        'TextBoxFindReplacePropertyNameSheetmetal.Enabled = False
        'TextBoxFindReplaceFindSheetmetal.Enabled = False
        'TextBoxFindReplaceReplaceSheetmetal.Enabled = False
        'CheckBoxFindReplaceFindPTSheetmetal.Enabled = False
        'CheckBoxFindReplaceFindWCSheetmetal.Enabled = False
        'CheckBoxFindReplaceFindRXSheetmetal.Enabled = False
        'CheckBoxFindReplaceReplacePTSheetmetal.Enabled = False
        'CheckBoxFindReplaceReplaceRXSheetmetal.Enabled = False
        TextBoxPropertiesEditSheetmetal.Enabled = False
        ButtonPropertiesEditSheetmetal.Enabled = False

        TextBoxVariablesEditSheetmetal.Enabled = False
        ButtonVariablesEditSheetmetal.Enabled = False


        For Each Label As String In CheckedListBoxSheetmetal.CheckedItems

            If LabelToActionSheetmetal(Label).RequiresSaveAsOutputDirectory Then
                ComboBoxSaveAsSheetmetalFileType.Enabled = True
                CheckBoxSaveAsSheetmetalOutputDirectory.Enabled = True
                ' TextBoxSaveAsSheetmetalOutputDirectory.Enabled = True

                If CheckBoxSaveAsSheetmetalOutputDirectory.Checked Then
                    TextBoxSaveAsSheetmetalOutputDirectory.Enabled = False
                    ButtonSaveAsSheetmetalOutputDirectory.Enabled = False
                Else
                    TextBoxSaveAsSheetmetalOutputDirectory.Enabled = True
                    ButtonSaveAsSheetmetalOutputDirectory.Enabled = True
                    CheckBoxSaveAsFormulaSheetmetal.Enabled = True
                    If CheckBoxSaveAsFormulaSheetmetal.Checked = True Then
                        TextBoxSaveAsFormulaSheetmetal.Enabled = True
                    End If
                End If
            End If


            If LabelToActionSheetmetal(Label).RequiresExternalProgram Then
                TextBoxExternalProgramSheetmetal.Enabled = True
                ButtonExternalProgramSheetmetal.Enabled = True
            End If

            If LabelToActionSheetmetal(Label).RequiresFindReplaceFields Then
                'ComboBoxFindReplacePropertySetSheetmetal.Enabled = True
                'TextBoxFindReplacePropertyNameSheetmetal.Enabled = True
                'TextBoxFindReplaceFindSheetmetal.Enabled = True
                'TextBoxFindReplaceReplaceSheetmetal.Enabled = True
                'CheckBoxFindReplaceFindPTSheetmetal.Enabled = True
                'CheckBoxFindReplaceFindWCSheetmetal.Enabled = True
                'CheckBoxFindReplaceFindRXSheetmetal.Enabled = True
                'CheckBoxFindReplaceReplacePTSheetmetal.Enabled = True
                'CheckBoxFindReplaceReplaceRXSheetmetal.Enabled = True
                TextBoxPropertiesEditSheetmetal.Enabled = True
                ButtonPropertiesEditSheetmetal.Enabled = True

            End If

            If LabelToActionSheetmetal(Label).RequiresVariablesToEdit Then
                TextBoxVariablesEditSheetmetal.Enabled = True
                ButtonVariablesEditSheetmetal.Enabled = True
            End If

        Next

        ' DRAFT

        ComboBoxSaveAsDraftFileType.Enabled = False
        CheckBoxSaveAsDraftOutputDirectory.Enabled = False
        TextBoxSaveAsDraftOutputDirectory.Enabled = False
        ButtonSaveAsDraftOutputDirectory.Enabled = False

        CheckBoxSaveAsFormulaDraft.Enabled = False
        TextBoxSaveAsFormulaDraft.Enabled = False

        CheckBoxWatermark.Enabled = False
        ButtonWatermark.Enabled = False
        TextBoxWatermarkFilename.Enabled = False
        TextBoxWatermarkScale.Enabled = False
        TextBoxWatermarkX.Enabled = False
        TextBoxWatermarkY.Enabled = False

        TextBoxExternalProgramDraft.Enabled = False
        ButtonExternalProgramDraft.Enabled = False

        'ComboBoxFindReplacePropertySetDraft.Enabled = False
        'TextBoxFindReplacePropertyNameDraft.Enabled = False
        'TextBoxFindReplaceFindDraft.Enabled = False
        'TextBoxFindReplaceReplaceDraft.Enabled = False
        'CheckBoxFindReplaceFindPTDraft.Enabled = False
        'CheckBoxFindReplaceFindWCDraft.Enabled = False
        'CheckBoxFindReplaceFindRXDraft.Enabled = False
        'CheckBoxFindReplaceReplacePTDraft.Enabled = False
        'CheckBoxFindReplaceReplaceRXDraft.Enabled = False
        TextBoxPropertiesEditDraft.Enabled = False
        ButtonPropertiesEditDraft.Enabled = False



        For Each Label As String In CheckedListBoxDraft.CheckedItems

            If LabelToActionDraft(Label).RequiresLaserOutputDirectory Then

            End If

            If LabelToActionDraft(Label).RequiresSaveAsOutputDirectory Then
                ComboBoxSaveAsDraftFileType.Enabled = True
                CheckBoxSaveAsDraftOutputDirectory.Enabled = True
                CheckBoxWatermark.Enabled = True

                If CheckBoxSaveAsDraftOutputDirectory.Checked Then
                    TextBoxSaveAsDraftOutputDirectory.Enabled = False
                    ButtonSaveAsDraftOutputDirectory.Enabled = False
                Else
                    TextBoxSaveAsDraftOutputDirectory.Enabled = True
                    ButtonSaveAsDraftOutputDirectory.Enabled = True
                    CheckBoxSaveAsFormulaDraft.Enabled = True
                    If CheckBoxSaveAsFormulaDraft.Checked = True Then
                        TextBoxSaveAsFormulaDraft.Enabled = True
                    End If
                End If

                If CheckBoxWatermark.Checked Then
                    ButtonWatermark.Enabled = True
                    TextBoxWatermarkFilename.Enabled = True
                    TextBoxWatermarkScale.Enabled = True
                    TextBoxWatermarkX.Enabled = True
                    TextBoxWatermarkY.Enabled = True
                Else
                    ButtonWatermark.Enabled = False
                    TextBoxWatermarkFilename.Enabled = False
                    TextBoxWatermarkScale.Enabled = False
                    TextBoxWatermarkX.Enabled = False
                    TextBoxWatermarkY.Enabled = False
                End If

            End If

            If LabelToActionDraft(Label).RequiresFindReplaceFields Then
                'ComboBoxFindReplacePropertySetDraft.Enabled = True
                'TextBoxFindReplacePropertyNameDraft.Enabled = True
                'TextBoxFindReplaceFindDraft.Enabled = True
                'TextBoxFindReplaceReplaceDraft.Enabled = True
                'CheckBoxFindReplaceFindPTDraft.Enabled = True
                'CheckBoxFindReplaceFindWCDraft.Enabled = True
                'CheckBoxFindReplaceFindRXDraft.Enabled = True
                'CheckBoxFindReplaceReplacePTDraft.Enabled = True
                'CheckBoxFindReplaceReplaceRXDraft.Enabled = True
                TextBoxPropertiesEditDraft.Enabled = True
                ButtonPropertiesEditDraft.Enabled = True
            End If

            If LabelToActionDraft(Label).RequiresExternalProgram Then
                TextBoxExternalProgramDraft.Enabled = True
                ButtonExternalProgramDraft.Enabled = True
            End If

        Next


    End Sub

    Private Sub CheckOrUncheckAll(ByVal CheckedListBoxX As CheckedListBox)
        Dim NoneChecked As Boolean

        'If no items are checked, NoneChecked will be True
        NoneChecked = CheckedListBoxX.CheckedItems.Count = 0

        For i As Integer = 0 To CheckedListBoxX.Items.Count - 1
            CheckedListBoxX.SetItemChecked(i, NoneChecked)
        Next

        ReconcileFormChanges()

    End Sub



    ' **************** CONTROLS ****************
    ' BUTTONS

    Private Sub ButtonActiveMaterialLibrary_Click(sender As Object, e As EventArgs) Handles ButtonActiveMaterialLibrary.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a material table file"
        tmpFileDialog.Filter = "Material Documents|*.mtl"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            TextBoxActiveMaterialLibrary.Text = tmpFileDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxActiveMaterialLibrary, TextBoxActiveMaterialLibrary.Text)
        ReconcileFormChanges()

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        If ButtonCancel.Text = "Stop" Then
            StopProcess = True
        Else
            ReconcileFormChanges()
            SaveDefaults()
            End
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As EventArgs) Handles Me.FormClosing
        ReconcileFormChanges()
        SaveDefaults()
        End
    End Sub

    Private Sub ButtonExternalProgramAssembly_Click(sender As Object, e As EventArgs) Handles ButtonExternalProgramAssembly.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a program file"
        tmpFileDialog.Filter = "Programs|*.exe;*.vbs;*.ps1"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            TextBoxExternalProgramAssembly.Text = tmpFileDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxExternalProgramAssembly, TextBoxExternalProgramAssembly.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonExternalProgramPart_Click(sender As Object, e As EventArgs) Handles ButtonExternalProgramPart.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a program file"
        tmpFileDialog.Filter = "Programs|*.exe;*.vbs;*.ps1"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            TextBoxExternalProgramPart.Text = tmpFileDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxExternalProgramPart, TextBoxExternalProgramPart.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonExternalProgramSheetmetal_Click(sender As Object, e As EventArgs) Handles ButtonExternalProgramSheetmetal.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a program file"
        tmpFileDialog.Filter = "Programs|*.exe;*.vbs;*.ps1"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            TextBoxExternalProgramSheetmetal.Text = tmpFileDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxExternalProgramSheetmetal, TextBoxExternalProgramSheetmetal.Text)
        ReconcileFormChanges()

    End Sub

    Private Sub ButtonExternalProgramDraft_Click(sender As Object, e As EventArgs) Handles ButtonExternalProgramDraft.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a program file"
        tmpFileDialog.Filter = "Programs|*.exe;*.vbs;*.ps1"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            TextBoxExternalProgramDraft.Text = tmpFileDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxExternalProgramDraft, TextBoxExternalProgramDraft.Text)
        ReconcileFormChanges()

    End Sub

    Private Sub ButtonFastSearchScopeFilename_Click(sender As Object, e As EventArgs) Handles ButtonFastSearchScopeFilename.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a fast search scope file"
        tmpFileDialog.Filter = "Search Scope Documents|*.txt"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            TextBoxFastSearchScopeFilename.Text = tmpFileDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxFastSearchScopeFilename, TextBoxFastSearchScopeFilename.Text)
        ReconcileFormChanges()

    End Sub

    Private Sub new_ButtonFileSearchDelete_Click(sender As Object, e As EventArgs) Handles new_ButtonFileSearchDelete.Click
        If Not new_ComboBoxFileSearch.Text = "" Then
            new_ComboBoxFileSearch.Items.Remove(new_ComboBoxFileSearch.Text)
            new_ComboBoxFileSearch.Text = ""
        End If
    End Sub

    Private Sub ButtonSaveAsDraftOutputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonSaveAsDraftOutputDirectory.Click
        Dim tmpFolderDialog As New CommonOpenFileDialog
        tmpFolderDialog.IsFolderPicker = True

        If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
            TextBoxSaveAsDraftOutputDirectory.Text = tmpFolderDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxSaveAsDraftOutputDirectory, TextBoxSaveAsDraftOutputDirectory.Text)
        ReconcileFormChanges()

    End Sub


    'Private Sub ButtonPrintOptions_Click(sender As Object, e As EventArgs)
    '    'PrintDialog1.ShowDialog()
    '    Dim h, w As Integer
    '    Dim long_dim, short_dim As Double

    '    If PrintDialog1.ShowDialog() = DialogResult.OK Then
    '        'SavePrinterSettings()

    '        TextBoxPrintOptionsPrinter.Text = PrintDialog1.PrinterSettings.PrinterName

    '        h = PrintDialog1.PrinterSettings.DefaultPageSettings.PaperSize.Height
    '        w = PrintDialog1.PrinterSettings.DefaultPageSettings.PaperSize.Width
    '        If w > h Then
    '            long_dim = CDbl(w) / 100
    '            short_dim = CDbl(h) / 100
    '        Else
    '            long_dim = CDbl(h) / 100
    '            short_dim = CDbl(w) / 100
    '        End If
    '        TextBoxPrintOptionsWidth.Text = CStr(long_dim)
    '        TextBoxPrintOptionsHeight.Text = CStr(short_dim)

    '        TextBoxPrinter1Copies.Text = CStr(PrintDialog1.PrinterSettings.Copies)

    '        ReconcileFormChanges()
    '    End If

    'End Sub

    Private Sub new_ButtonPropertyFilter_Click(sender As Object, e As EventArgs) Handles new_ButtonPropertyFilter.Click

        FormPropertyFilter.SetReadmeFontsize(CInt(TextBoxFontSize.Text))
        FormPropertyFilter.GetPropertyFilter()

        If FormPropertyFilter.DialogResult = DialogResult.OK Then
            ListViewFilesOutOfDate = True
            BT_Update.BackColor = Color.Orange
        End If

    End Sub

    Private Sub ButtonProcess_Click(sender As Object, e As EventArgs) Handles ButtonProcess.Click
        ProcessAll()
    End Sub


    Private Sub ButtonSaveAsAssemblyOutputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonSaveAsAssemblyOutputDirectory.Click
        Dim tmpFolderDialog As New CommonOpenFileDialog
        tmpFolderDialog.IsFolderPicker = True

        If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
            TextBoxSaveAsAssemblyOutputDirectory.Text = tmpFolderDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxSaveAsAssemblyOutputDirectory, TextBoxSaveAsAssemblyOutputDirectory.Text)
        ReconcileFormChanges()


    End Sub

    Private Sub ButtonSaveAsPartOutputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonSaveAsPartOutputDirectory.Click
        Dim tmpFolderDialog As New CommonOpenFileDialog
        tmpFolderDialog.IsFolderPicker = True

        If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
            TextBoxSaveAsPartOutputDirectory.Text = tmpFolderDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxSaveAsPartOutputDirectory, TextBoxSaveAsPartOutputDirectory.Text)
        ReconcileFormChanges()

    End Sub

    Private Sub ButtonSaveAsSheetmetalOutputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonSaveAsSheetmetalOutputDirectory.Click
        Dim tmpFolderDialog As New CommonOpenFileDialog
        tmpFolderDialog.IsFolderPicker = True

        If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
            TextBoxSaveAsSheetmetalOutputDirectory.Text = tmpFolderDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxSaveAsSheetmetalOutputDirectory, TextBoxSaveAsSheetmetalOutputDirectory.Text)
        ReconcileFormChanges()

    End Sub

    Private Sub ButtonTemplateAssembly_Click(sender As Object, e As EventArgs) Handles ButtonTemplateAssembly.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select an assembly template file"
        tmpFileDialog.Filter = "asm files|*.asm"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            TextBoxTemplateAssembly.Text = tmpFileDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxTemplateAssembly, TextBoxTemplateAssembly.Text)
        ReconcileFormChanges()

    End Sub

    Private Sub ButtonTemplateDraft_Click(sender As Object, e As EventArgs) Handles ButtonTemplateDraft.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a draft template file"
        tmpFileDialog.Filter = "Draft Documents|*.dft"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            TextBoxTemplateDraft.Text = tmpFileDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxTemplateDraft, TextBoxTemplateDraft.Text)
        ReconcileFormChanges()

    End Sub

    Private Sub ButtonTemplatePart_Click(sender As Object, e As EventArgs) Handles ButtonTemplatePart.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a part template file"
        tmpFileDialog.Filter = "Part Documents|*.par"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            TextBoxTemplatePart.Text = tmpFileDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxTemplatePart, TextBoxTemplatePart.Text)
        ReconcileFormChanges()

    End Sub

    Private Sub ButtonTemplateSheetmetal_Click(sender As Object, e As EventArgs) Handles ButtonTemplateSheetmetal.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a sheetmetal template file"
        tmpFileDialog.Filter = "Sheetmetal Documents|*.psm"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            TextBoxTemplateSheetmetal.Text = tmpFileDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxTemplateSheetmetal, TextBoxTemplateSheetmetal.Text)
        ReconcileFormChanges()

    End Sub

    Private Sub ButtonWatermark_Click(sender As Object, e As EventArgs) Handles ButtonWatermark.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select an image file"
        tmpFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            TextBoxWatermarkFilename.Text = tmpFileDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxWatermarkFilename, TextBoxWatermarkFilename.Text)
        ReconcileFormChanges()

    End Sub


    ' FORM LOAD

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Startup()
    End Sub



    ' CHECKBOXES

    Private Sub new_CheckBoxEnablePropertyFilter_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxEnablePropertyFilter.CheckedChanged

        If new_CheckBoxEnablePropertyFilter.Checked Then

            new_CheckBoxEnablePropertyFilter.Image = My.Resources.Checked
            new_ButtonPropertyFilter.Enabled = True

            If PropertyFilterFormula = "" Then
                FormPropertyFilter.SetReadmeFontsize(CInt(TextBoxFontSize.Text))
                FormPropertyFilter.GetPropertyFilter()
            End If

        Else
            new_CheckBoxEnablePropertyFilter.Image = My.Resources.Unchecked
            new_ButtonPropertyFilter.Enabled = False
        End If

        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

        'ApplyFilters()

    End Sub

    Private Sub new_CheckBoxFileSearch_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxFileSearch.CheckedChanged

        If new_CheckBoxFileSearch.Checked Then
            new_CheckBoxFileSearch.Image = My.Resources.Checked
            new_ComboBoxFileSearch.Enabled = True
        Else
            new_CheckBoxFileSearch.Image = My.Resources.Unchecked
            new_ComboBoxFileSearch.Enabled = False
        End If

        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

        'ApplyFilters()

    End Sub

    Private Sub new_CheckBoxFilterAsm_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxFilterAsm.CheckedChanged
        ListViewFilesOutOfDate = True
        ReconcileFormChanges()
    End Sub

    Private Sub new_CheckBoxFilterPar_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxFilterPar.CheckedChanged
        ListViewFilesOutOfDate = True
        ReconcileFormChanges()
    End Sub

    Private Sub new_CheckBoxFilterPsm_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxFilterPsm.CheckedChanged
        ListViewFilesOutOfDate = True
        ReconcileFormChanges()
    End Sub

    Private Sub new_CheckBoxFilterDft_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxFilterDft.CheckedChanged
        ListViewFilesOutOfDate = True
        ReconcileFormChanges()
    End Sub


    Private Sub CheckBoxMoveDrawingViewAllowPartialSuccess_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxMoveDrawingViewAllowPartialSuccess.CheckedChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxPdfDraftOutputDirectory_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsDraftOutputDirectory.CheckedChanged
        'If CheckBoxSaveAsDraftOutputDirectory.Checked Then
        '    TextBoxSaveAsDraftOutputDirectory.Enabled = False
        'Else
        '    TextBoxSaveAsDraftOutputDirectory.Enabled = True
        'End If
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxRememberTasks_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRememberTasks.CheckedChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxSaveAsDraftOutputDirectory_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsDraftOutputDirectory.CheckedChanged
        If CheckBoxSaveAsDraftOutputDirectory.Checked Then
            TextBoxSaveAsDraftOutputDirectory.Enabled = False
        Else
            TextBoxSaveAsDraftOutputDirectory.Enabled = True
        End If
        ReconcileFormChanges()
    End Sub


    Private Sub CheckBoxSaveAsFormulaAssembly_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsFormulaAssembly.CheckedChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxSaveAsFormulaDraft_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsFormulaDraft.CheckedChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxSaveAsFormulaPart_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsFormulaPart.CheckedChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxSaveAsFormulaSheetmetal_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsFormulaSheetmetal.CheckedChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxStepAssemblyOutputDirectory_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsAssemblyOutputDirectory.CheckedChanged
        'If CheckBoxSaveAsAssemblyOutputDirectory.Checked Then
        '    TextBoxSaveAsAssemblyOutputDirectory.Enabled = False
        'Else
        '    TextBoxSaveAsAssemblyOutputDirectory.Enabled = True
        'End If
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxStepPartOutputDirectory_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsPartOutputDirectory.CheckedChanged
        'If CheckBoxSaveAsPartOutputDirectory.Checked Then
        '    TextBoxSaveAsPartOutputDirectory.Enabled = False
        'Else
        '    TextBoxSaveAsPartOutputDirectory.Enabled = True
        'End If
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxStepSheetmetalOutputDirectory_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsSheetmetalOutputDirectory.CheckedChanged
        'If CheckBoxSaveAsSheetmetalOutputDirectory.Checked Then
        '    TextBoxSaveAsSheetmetalOutputDirectory.Enabled = False
        'Else
        '    TextBoxSaveAsSheetmetalOutputDirectory.Enabled = True
        'End If
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxTLAReportUnrelatedFiles_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTLAReportUnrelatedFiles.CheckedChanged
        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxWarnSave_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxWarnSave.CheckedChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxWatermark_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxWatermark.CheckedChanged
        ReconcileFormChanges()
    End Sub



    ' CHECKLISTBOXES

    Private Sub CheckedListBoxAssembly_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBoxAssembly.DoubleClick
        CheckOrUncheckAll(CheckedListBoxAssembly)
    End Sub

    Private Sub CheckedListBoxAssembly_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBoxAssembly.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckedListBoxPart_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBoxPart.DoubleClick
        CheckOrUncheckAll(CheckedListBoxPart)
    End Sub

    Private Sub CheckedListBoxPart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBoxPart.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckedListBoxSheetmetal_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBoxSheetmetal.DoubleClick
        CheckOrUncheckAll(CheckedListBoxSheetmetal)
    End Sub

    Private Sub CheckedListBoxSheetmetal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBoxSheetmetal.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckedListBoxDraft_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBoxDraft.DoubleClick
        CheckOrUncheckAll(CheckedListBoxDraft)
    End Sub

    Private Sub CheckedListBoxDraft_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBoxDraft.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub



    ' COMBOBOXES

    Private Sub new_ComboBoxFileSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles new_ComboBoxFileSearch.SelectedIndexChanged
        'new_CheckBoxFileSearch.Checked = True
        'ApplyFilters()

    End Sub

    Private Sub new_ComboBoxFileSearch_LostFocus(sender As Object, e As EventArgs) Handles new_ComboBoxFileSearch.LostFocus

        Dim Key As String = new_ComboBoxFileSearch.Text

        If Not new_ComboBoxFileSearch.Items.Contains(Key) Then
            new_ComboBoxFileSearch.Items.Add(new_ComboBoxFileSearch.Text)
        End If

    End Sub

    Private Sub ComboBoxPartNumberPropertySet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPartNumberPropertySet.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub

    Private Sub ComboBoxSaveAsAssemblyFileType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxSaveAsAssemblyFileType.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub

    Private Sub ComboBoxSaveAsPartFiletype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxSaveAsPartFileType.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub

    Private Sub ComboBoxSaveAsSheetmetalFileType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxSaveAsSheetmetalFileType.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub

    Private Sub ComboBoxSaveAsDraftFileType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxSaveAsDraftFileType.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub


    ' LINK LABELS

    Private Sub LinkLabelGitHubReadme_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelGitHubReadme.LinkClicked
        System.Diagnostics.Process.Start(e.Link.LinkData.ToString())
    End Sub


    ' RADIO BUTTONS
    Private Sub RadioButtonTLABottomUp_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTLABottomUp.CheckedChanged
        Dim tf As Boolean

        tf = RadioButtonTLABottomUp.Checked
        If tf Then
            TextBoxFastSearchScopeFilename.Enabled = True
            ButtonFastSearchScopeFilename.Enabled = True
            CheckBoxDraftAndModelSameName.Enabled = True
        End If

        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

        ReconcileFormChanges()
    End Sub

    Private Sub RadioButtonTLATopDown_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTLATopDown.CheckedChanged
        Dim tf As Boolean

        tf = RadioButtonTLATopDown.Checked
        If tf Then
            TextBoxFastSearchScopeFilename.Enabled = False
            ButtonFastSearchScopeFilename.Enabled = False
            CheckBoxDraftAndModelSameName.Enabled = False
        End If

        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

        ReconcileFormChanges()
    End Sub


    ' TEXT BOXES



    Private Sub TextBoxFastSearchScopeFilename_TextChanged(sender As Object, e As EventArgs) Handles TextBoxFastSearchScopeFilename.TextChanged
        ReconcileFormChanges()
    End Sub

    'Private Sub TextBoxFileSearch_LostFocus(sender As Object, e As EventArgs) Handles TextBoxFileSearch.LostFocus
    '    ListViewFilesOutOfDate = True
    '    ReconcileFormChanges()
    'End Sub

    Private Sub TextBoxPartNumberPropertyName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPartNumberPropertyName.TextChanged
        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxSaveAsFormulaAssembly_LostFocus(sender As Object, e As EventArgs) Handles TextBoxSaveAsFormulaAssembly.LostFocus
        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxSaveAsFormulaDraft_LostFocus(sender As Object, e As EventArgs) Handles TextBoxSaveAsFormulaDraft.LostFocus
        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxSaveAsFormulaPart_LostFocus(sender As Object, e As EventArgs) Handles TextBoxSaveAsFormulaPart.LostFocus
        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxSaveAsFormulaSheetmetal_LostFocus(sender As Object, e As EventArgs) Handles TextBoxSaveAsFormulaSheetmetal.LostFocus
        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxStatus_TextChanged(sender As Object, e As EventArgs) Handles TextBoxStatus.TextChanged
        ToolTip1.SetToolTip(TextBoxStatus, TextBoxStatus.Text)
    End Sub

    Private Sub TextBoxWatermarkFilename_TextChanged(sender As Object, e As EventArgs) Handles TextBoxWatermarkScale.TextChanged
        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxWatermarkScale_TextChanged(sender As Object, e As EventArgs) Handles TextBoxWatermarkScale.TextChanged
        ReconcileFormChanges()
    End Sub
    Private Sub TextBoxWatermarkX_TextChanged(sender As Object, e As EventArgs) Handles TextBoxWatermarkScale.TextChanged
        ReconcileFormChanges()
    End Sub
    Private Sub TextBoxWatermarkY_TextChanged(sender As Object, e As EventArgs) Handles TextBoxWatermarkScale.TextChanged
        ReconcileFormChanges()
    End Sub

    Private Sub BT_AddFolder_Click(sender As Object, e As EventArgs) Handles BT_AddFolder.Click

        Dim tmpFolderDialog As New CommonOpenFileDialog
        tmpFolderDialog.IsFolderPicker = True

        If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
            Dim tmpItem As New ListViewItem
            tmpItem.Text = "Folder"
            tmpItem.SubItems.Add(tmpFolderDialog.FileName)
            tmpItem.Group = ListViewFiles.Groups.Item("Sources")
            tmpItem.ImageKey = "Folder"
            tmpItem.Tag = "Folder"
            tmpItem.Name = tmpFolderDialog.FileName
            If Not ListViewFiles.Items.ContainsKey(tmpItem.Name) Then ListViewFiles.Items.Add(tmpItem)

            ListViewFilesOutOfDate = True
            BT_Update.BackColor = Color.Orange
        End If

    End Sub

    Private Sub BT_AddFolderSubfolders_Click(sender As Object, e As EventArgs) Handles BT_AddFolderSubfolders.Click

        Dim tmpFolderDialog As New CommonOpenFileDialog
        tmpFolderDialog.IsFolderPicker = True

        If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
            Dim tmpItem As New ListViewItem
            tmpItem.Text = "Folder with subfolders"
            tmpItem.SubItems.Add(tmpFolderDialog.FileName)
            tmpItem.Group = ListViewFiles.Groups.Item("Sources")
            tmpItem.ImageKey = "Folders"
            tmpItem.Tag = "Folders"
            tmpItem.Name = tmpFolderDialog.FileName
            If Not ListViewFiles.Items.ContainsKey(tmpItem.Name) Then ListViewFiles.Items.Add(tmpItem)

            ListViewFilesOutOfDate = True
            BT_Update.BackColor = Color.Orange

        End If

    End Sub

    Private Sub BT_AddFromlist_Click(sender As Object, e As EventArgs) Handles BT_AddFromlist.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a list of files"
        tmpFileDialog.Filter = "Text files|*.txt|CSV files|*.csv|Excel files|*.xls;*.xlsx;*.xlsm"
        If tmpFileDialog.ShowDialog() = DialogResult.OK Then

            Dim tmpItem As New ListViewItem

            Select Case IO.Path.GetExtension(tmpFileDialog.FileName).ToLower

                Case Is = ".txt"
                    tmpItem.Text = "TXT list"
                    tmpItem.ImageKey = "txt"
                    tmpItem.Tag = "txt"
                Case Is = ".csv"
                    tmpItem.Text = "CSV list"
                    tmpItem.ImageKey = "csv"
                    tmpItem.Tag = "csv"
                Case Is = ".xls", ".xlsx", ".xlsm"
                    tmpItem.Text = "Excel list"
                    tmpItem.ImageKey = "excel"
                    tmpItem.Tag = "excel"

            End Select

            tmpItem.SubItems.Add(tmpFileDialog.FileName)
            tmpItem.Group = ListViewFiles.Groups.Item("Sources")

            tmpItem.Name = tmpFileDialog.FileName
            If Not ListViewFiles.Items.ContainsKey(tmpItem.Name) Then ListViewFiles.Items.Add(tmpItem)

        End If

    End Sub

    Private Sub BT_TopLevelAsm_Click(sender As Object, e As EventArgs) Handles BT_TopLevelAsm.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select an assembly file"
        tmpFileDialog.Filter = "asm files|*.asm"
        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Dim tmpItem As New ListViewItem
            tmpItem.Text = "Top level assembly"
            tmpItem.SubItems.Add(tmpFileDialog.FileName)
            tmpItem.Group = ListViewFiles.Groups.Item("Sources")
            tmpItem.ImageKey = "asm"
            tmpItem.Tag = "asm"
            tmpItem.Name = tmpFileDialog.FileName
            If Not ListViewFiles.Items.ContainsKey(tmpItem.Name) Then ListViewFiles.Items.Add(tmpItem)

            If CheckBoxTLAAutoIncludeTLF.Checked Then
                Dim Folder As String = System.IO.Path.GetDirectoryName(tmpFileDialog.FileName)

                Dim tmpItem2 As New ListViewItem
                tmpItem2.Text = "Top level asm folder"
                tmpItem2.SubItems.Add(Folder)
                tmpItem2.Group = ListViewFiles.Groups.Item("Sources")
                tmpItem2.ImageKey = "ASM_Folder"
                tmpItem2.Tag = "ASM_Folder"
                tmpItem2.Name = Folder
                If Not ListViewFiles.Items.ContainsKey(tmpItem2.Name) Then ListViewFiles.Items.Add(tmpItem2)
            End If

            ListViewFilesOutOfDate = True
            BT_Update.BackColor = Color.Orange

        End If

    End Sub
    Private Sub BT_ASM_Folder_Click(sender As Object, e As EventArgs) Handles BT_ASM_Folder.Click

        Dim tmpFolderDialog As New CommonOpenFileDialog
        tmpFolderDialog.IsFolderPicker = True

        If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
            Dim tmpItem As New ListViewItem
            tmpItem.Text = "Top level asm folder"
            tmpItem.SubItems.Add(tmpFolderDialog.FileName)
            tmpItem.Group = ListViewFiles.Groups.Item("Sources")
            tmpItem.ImageKey = "ASM_Folder"
            tmpItem.Tag = "ASM_Folder"
            tmpItem.Name = tmpFolderDialog.FileName
            If Not ListViewFiles.Items.ContainsKey(tmpItem.Name) Then ListViewFiles.Items.Add(tmpItem)

            ListViewFilesOutOfDate = True
            BT_Update.BackColor = Color.Orange

        End If

    End Sub

    Private Sub BT_DeleteAll_Click(sender As Object, e As EventArgs) Handles BT_DeleteAll.Click

        ListViewFiles.BeginUpdate()
        ListViewFiles.Items.Clear()
        ListViewFiles.EndUpdate()

        DragDropCache.Clear()
        DragDropCacheExcluded.Clear()

        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

    End Sub

    Private Sub BT_Reload_Click(sender As Object, e As EventArgs) Handles BT_Update.Click

        New_UpdateFileList()

    End Sub

    Private Sub New_UpdateFileList()

        Me.Cursor = Cursors.WaitCursor

        Dim GroupTags As New List(Of String)
        Dim BareTopLevelAssembly As Boolean = False
        Dim msg As String

        Dim ElapsedTime As Double
        Dim ElapsedTimeText As String

        StartTime = Now


        TextBoxStatus.Text = "Updating list..."
        LabelTimeRemaining.Text = ""
        System.Windows.Forms.Application.DoEvents()

        ListViewFiles.BeginUpdate()

        ' Remove everything except the "Sources" group.
        For i = ListViewFiles.Items.Count - 1 To 0 Step -1
            If ListViewFiles.Items.Item(i).Group.Name <> "Sources" Then
                ListViewFiles.Items.Item(i).Remove()
            Else
                GroupTags.Add(CType(ListViewFiles.Items.Item(i).Tag, String))
            End If
        Next

        If (GroupTags.Contains("ASM_Folder")) And Not (GroupTags.Contains("asm")) Then
            msg = "A top level assembly folder was found with no top level assembly.  "
            msg += "Please add an assembly, or delete the folder(s)."
            ListViewFiles.EndUpdate()
            Me.Cursor = Cursors.Default
            TextBoxStatus.Text = ""
            MsgBox(msg, vbOKOnly)
            Exit Sub
        End If

        If (RadioButtonTLABottomUp.Checked) And (Not FileIO.FileSystem.FileExists(TextBoxFastSearchScopeFilename.Text)) Then
            msg = "Fast search scope file (on Configuration Tab) not found" + Chr(13)
            ListViewFiles.EndUpdate()
            Me.Cursor = Cursors.Default
            TextBoxStatus.Text = ""
            MsgBox(msg, vbOKOnly)
            Exit Sub
        End If

        If (GroupTags.Contains("asm")) And Not (GroupTags.Contains("ASM_Folder")) Then

            'If CheckBoxWarnBareTLA.Enabled And CheckBoxWarnBareTLA.Checked Then
            If CheckBoxWarnBareTLA.Checked Then
                msg = "A top-level assembly with no top-level folder detected.  "
                msg += "No 'Where Used' will be performed." + vbCrLf + vbCrLf
                msg += "Click OK to continue, or Cancel to stop." + vbCrLf
                msg += "Disable this message on the Configuration tab."
                Dim result As MsgBoxResult = MsgBox(msg, vbOKCancel)
                If result = MsgBoxResult.Ok Then
                    BareTopLevelAssembly = True
                Else
                    ListViewFiles.EndUpdate()
                    Me.Cursor = Cursors.Default
                    TextBoxStatus.Text = ""
                    Exit Sub
                End If
            Else
                BareTopLevelAssembly = True
            End If
        End If


        ' Only remaining items should be in the "Sources" group.
        For Each item As ListViewItem In ListViewFiles.Items
            UpdateListViewFiles(item, BareTopLevelAssembly)
        Next

        'DragDropCache.Clear()
        'For Each item As ListViewItem In ListViewFiles.Items
        '    DragDropCache.Add(item)
        'Next

        ListViewFiles.EndUpdate()

        Me.Cursor = Cursors.Default
        'If TextBoxStatus.Text = "Updating list..." Then
        '    TextBoxStatus.Text = "No files found"
        'End If

        ElapsedTime = Now.Subtract(StartTime).TotalMinutes
        If ElapsedTime < 60 Then
            ElapsedTimeText = "in " + ElapsedTime.ToString("0.0") + " min."
        Else
            ElapsedTimeText = "in " + (ElapsedTime / 60).ToString("0.0") + " hr."
        End If


        Dim filecount As Integer = ListViewFiles.Items.Count - ListViewFiles.Groups.Item("Sources").Items.Count
        TextBoxStatus.Text = String.Format("{0} files found in {1}", filecount, ElapsedTimeText)


    End Sub

    Private Sub ListViewFiles_KeyUp(sender As Object, e As KeyEventArgs) Handles ListViewFiles.KeyUp

        If e.KeyCode = Keys.Escape Then ListViewFiles.SelectedItems.Clear()
        If e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then

            For i = ListViewFiles.SelectedItems.Count - 1 To 0 Step -1

                Dim tmpItem As ListViewItem = ListViewFiles.SelectedItems.Item(i)
                If tmpItem.Group.Name = "Sources" Then
                    tmpItem.Remove()
                    ListViewFilesOutOfDate = True
                    BT_Update.BackColor = Color.Orange

                ElseIf tmpItem.Group.Name <> "Excluded" Then
                    ' Move item to "Excluded" group
                    tmpItem.Group = ListViewFiles.Groups.Item("Excluded")

                    If DragDropCache.Contains(tmpItem) Then
                        DragDropCacheExcluded.Add(tmpItem)
                        DragDropCache.Remove(tmpItem)
                    End If
                Else
                    ' Move item from "Excluded" group back to the group matching the file's extension
                    tmpItem.Group = ListViewFiles.Groups.Item(IO.Path.GetExtension(tmpItem.Name))

                    If DragDropCacheExcluded.Contains(tmpItem) Then
                        DragDropCache.Add(tmpItem)
                        DragDropCacheExcluded.Remove(tmpItem)

                    End If

                End If

            Next

            'ListViewFilesOutOfDate = True
            'BT_Update.BackColor = Color.Orange

        End If

    End Sub

    Private Sub CarIcona()

        TabPage_ImageList.Images.Clear()

        CaricaImmagine16x16(TabPage_ImageList, "se", My.Resources.se)
        CaricaImmagine16x16(TabPage_ImageList, "asm", My.Resources.asm)
        CaricaImmagine16x16(TabPage_ImageList, "cfg", My.Resources.cfg)
        CaricaImmagine16x16(TabPage_ImageList, "dft", My.Resources.dft)
        CaricaImmagine16x16(TabPage_ImageList, "par", My.Resources.par)
        CaricaImmagine16x16(TabPage_ImageList, "psm", My.Resources.psm)
        CaricaImmagine16x16(TabPage_ImageList, "Checked", My.Resources.Checked)
        CaricaImmagine16x16(TabPage_ImageList, "Unchecked", My.Resources.Unchecked)
        CaricaImmagine16x16(TabPage_ImageList, "config", My.Resources.config)
        CaricaImmagine16x16(TabPage_ImageList, "Help", My.Resources.Help)
        CaricaImmagine16x16(TabPage_ImageList, "Info", My.Resources.Info)
        CaricaImmagine16x16(TabPage_ImageList, "Error", My.Resources.Errore)
        CaricaImmagine16x16(TabPage_ImageList, "txt", My.Resources.txt)
        CaricaImmagine16x16(TabPage_ImageList, "csv", My.Resources.csv)
        CaricaImmagine16x16(TabPage_ImageList, "excel", My.Resources.excel)
        CaricaImmagine16x16(TabPage_ImageList, "folder", My.Resources.folder)
        CaricaImmagine16x16(TabPage_ImageList, "folders", My.Resources.folders)
        CaricaImmagine16x16(TabPage_ImageList, "ASM_folder", My.Resources.ASM_Folder)
        CaricaImmagine16x16(TabPage_ImageList, "list", My.Resources.list)
        CaricaImmagine16x16(TabPage_ImageList, "Tools", My.Resources.Tools)

    End Sub

    Private Sub CaricaImmagine16x16(IL As ImageList, Key As String, Immagine As Image)

        Dim b = New Bitmap(16, 16)
        Dim g = Graphics.FromImage(b)
        g.FillRectangle(New SolidBrush(Color.Transparent), 0, 0, 16, 16)
        g.DrawImage(Immagine, 0, 0, 16, 16)

        Try
            IL.Images.Add(Key, b)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub BT_ExportList_Click(sender As Object, e As EventArgs) Handles BT_ExportList.Click

        Dim tmpFileDialog As New SaveFileDialog
        tmpFileDialog.Title = "Save a list of files"
        tmpFileDialog.Filter = "Text files|*.txt"
        If tmpFileDialog.ShowDialog() = DialogResult.OK Then

            Dim content As String = ""
            For Each tmpItem As ListViewItem In ListViewFiles.Items

                If tmpItem.Group.Name <> "Sources" And tmpItem.Group.Name <> "Excluded" Then
                    content += tmpItem.Name & vbCrLf
                End If

            Next

            IO.File.WriteAllText(tmpFileDialog.FileName, content)



        End If

    End Sub

    Private Sub BT_ErrorList_Click(sender As Object, e As EventArgs) Handles BT_ErrorList.Click

        ListViewFiles.BeginUpdate()

        For i = ListViewFiles.Items.Count - 1 To 0 Step -1

            If ListViewFiles.Items.Item(i).ImageKey <> "Error" Then ListViewFiles.Items.Item(i).Remove()

        Next

        ListViewFiles.EndUpdate()

    End Sub

    Private Sub ListViewFiles_MouseClick(sender As Object, e As MouseEventArgs) Handles ListViewFiles.MouseClick

        If ListViewFiles.SelectedItems.Count > 0 And e.Button = MouseButtons.Right Then

            Menu_ListViewFile.Show(ListViewFiles, New Point(e.X, e.Y))

        End If

    End Sub

    Private Sub BT_Open_Click(sender As Object, e As EventArgs) Handles BT_Open.Click
        For Each item As ListViewItem In ListViewFiles.SelectedItems
            Process.Start(item.Name)
        Next
    End Sub

    Private Sub BT_OpenFolder_Click(sender As Object, e As EventArgs) Handles BT_OpenFolder.Click
        For Each item As ListViewItem In ListViewFiles.SelectedItems
            Process.Start(IO.Path.GetDirectoryName(item.Name))
        Next
    End Sub

    Private Sub BT_Remove_Click(sender As Object, e As EventArgs) Handles BT_Remove.Click
        For i = ListViewFiles.SelectedItems.Count - 1 To 0 Step -1

            Dim tmpItem As ListViewItem = ListViewFiles.SelectedItems.Item(i)
            If tmpItem.Group.Name = "Sources" Then
                tmpItem.Remove()
            ElseIf tmpItem.Group.Name <> "Excluded" Then
                tmpItem.Group = ListViewFiles.Groups.Item("Excluded")
            Else
                tmpItem.Group = ListViewFiles.Groups.Item(IO.Path.GetExtension(tmpItem.Name))
            End If

        Next
    End Sub


    Private Sub TextBoxFontSize_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxFontSize.KeyDown

        If e.KeyCode = Keys.Enter Then
            Me.ActiveControl = Nothing
            e.Handled = True
            e.SuppressKeyPress = True
        End If

    End Sub

    Private Sub TextBoxFontSize_Leave(sender As Object, e As EventArgs) Handles TextBoxFontSize.Leave

        If Not IsNumeric(TextBoxFontSize.Text) Then TextBoxFontSize.Text = "8"
        If TextBoxFontSize.Text = "0" Then TextBoxFontSize.Text = "8"
        ListViewFiles.Font = New Font(ListViewFiles.Font.FontFamily, CInt(TextBoxFontSize.Text), FontStyle.Regular)

    End Sub

    Private Sub new_ComboBoxFileSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles new_ComboBoxFileSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            'ApplyFilters()
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub ListViewFiles_DragEnter(sender As Object, e As DragEventArgs) Handles ListViewFiles.DragEnter

        e.Effect = DragDropEffects.Copy

    End Sub

    Private Sub ListViewFiles_DragDrop(sender As Object, e As DragEventArgs) Handles ListViewFiles.DragDrop

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            Dim DragDropGroupPresent As Boolean

            DragDropGroupPresent = False
            For Each g As ListViewGroup In ListViewFiles.Groups
                If g.Name = "DragDrop" Then
                    DragDropGroupPresent = True
                    Exit For
                End If
            Next

            If Not DragDropGroupPresent Then
                Dim tmpItem As New ListViewItem
                tmpItem.Text = "DragDrop"
                'tmpItem.SubItems.Add(tmpFolderDialog.FileName)
                tmpItem.Group = ListViewFiles.Groups.Item("Sources")
                tmpItem.ImageKey = "ASM_Folder"
                tmpItem.Tag = "DragDrop"
                tmpItem.Name = "DragDrop"
                If Not ListViewFiles.Items.ContainsKey(tmpItem.Name) Then ListViewFiles.Items.Add(tmpItem)

            End If

            ListViewFiles.BeginUpdate()

            Dim extFilter As New List(Of String)
            If new_CheckBoxFilterAsm.Checked Then extFilter.Add(".asm")
            If new_CheckBoxFilterPar.Checked Then extFilter.Add(".par")
            If new_CheckBoxFilterPsm.Checked Then extFilter.Add(".psm")
            If new_CheckBoxFilterDft.Checked Then extFilter.Add(".dft")

            Dim Filenames As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            For Each FileName As String In Filenames

                If CommonTasks.FilenameIsOK(FileName) Then

                    If Not extFilter.Contains(IO.Path.GetExtension(FileName).ToLower) Then Continue For

                    If IO.File.Exists(FileName) Then

                        If Not ListViewFiles.Items.ContainsKey(FileName) Then

                            Dim tmpLVItem As New ListViewItem
                            tmpLVItem.Text = IO.Path.GetFileName(FileName)
                            tmpLVItem.SubItems.Add(IO.Path.GetDirectoryName(FileName))
                            tmpLVItem.ImageKey = "Unchecked"
                            tmpLVItem.Tag = IO.Path.GetExtension(FileName).ToLower 'Backup gruppo
                            tmpLVItem.Name = FileName
                            tmpLVItem.Group = ListViewFiles.Groups.Item(IO.Path.GetExtension(FileName).ToLower)
                            ListViewFiles.Items.Add(tmpLVItem)

                            Dim ItemPresent As Boolean

                            ItemPresent = False
                            For Each CacheItem As ListViewItem In DragDropCache
                                If tmpLVItem.Name = CacheItem.Name Then
                                    ItemPresent = True
                                    Exit For
                                End If
                            Next
                            If Not ItemPresent Then
                                DragDropCache.Add(tmpLVItem)
                            End If

                        End If

                    End If

                End If

            Next

            ListViewFiles.EndUpdate()

        End If

    End Sub

    Private Sub BT_ProcessSelected_Click(sender As Object, e As EventArgs) Handles BT_ProcessSelected.Click

        If Not ListViewFiles.SelectedItems.Count = 0 Then ProcessAll()

    End Sub

    Private Sub BT_FindLinks_Click(sender As Object, e As EventArgs) Handles BT_FindLinks.Click

        ' A user reported he was confused by this command.  In particular that it didn't
        ' find Draft files or use his Property Filter settings.
        ' Disabling for now.

        MsgBox("This command is temporarily disabled.  Please use 'Update' instead.")
        Exit Sub


        Dim DMApp As New DesignManager.Application
        Dim DMDoc As DesignManager.Document

        For Each item As ListViewItem In ListViewFiles.SelectedItems

            DMDoc = CType(DMApp.Open(item.Name), DesignManager.Document)

            CommonTasks.tmpList = New Collection
            CommonTasks.FindLinked(DMDoc)

            For Each FoundFile In CommonTasks.tmpList
                If CommonTasks.FilenameIsOK(FoundFile.ToString) Then

                    If IO.File.Exists(FoundFile.ToString) Then

                        If Not ListViewFiles.Items.ContainsKey(FoundFile.ToString) Then

                            Dim tmpLVItem As New ListViewItem
                            tmpLVItem.Text = IO.Path.GetFileName(FoundFile.ToString)
                            tmpLVItem.SubItems.Add(IO.Path.GetDirectoryName(FoundFile.ToString))
                            tmpLVItem.ImageKey = "Unchecked"
                            tmpLVItem.Tag = IO.Path.GetExtension(FoundFile.ToString).ToLower 'Backup gruppo
                            tmpLVItem.Name = FoundFile.ToString
                            tmpLVItem.Group = ListViewFiles.Groups.Item(IO.Path.GetExtension(FoundFile.ToString).ToLower)
                            ListViewFiles.Items.Add(tmpLVItem)

                        End If

                    End If

                End If
            Next

        Next

        CommonTasks.tmpList = Nothing

        DMApp.Quit()

    End Sub

    'Private Sub CheckBoxTLAAutoIncludeTLF_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTLAAutoIncludeTLF.CheckedChanged
    '    If CheckBoxTLAAutoIncludeTLF.Checked Then
    '        CheckBoxWarnBareTLA.Enabled = False
    '    Else
    '        CheckBoxWarnBareTLA.Enabled = True
    '    End If
    'End Sub

    'Private Sub CheckBoxFindReplaceFindPTAssembly_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceFindPTAssembly.Checked Then
    '        CheckBoxFindReplaceFindWCAssembly.Checked = False
    '        CheckBoxFindReplaceFindRXAssembly.Checked = False
    '    End If
    'End Sub
    'Private Sub CheckBoxFindReplaceFindWCAssembly_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceFindWCAssembly.Checked Then
    '        CheckBoxFindReplaceFindPTAssembly.Checked = False
    '        CheckBoxFindReplaceFindRXAssembly.Checked = False
    '    End If
    'End Sub
    'Private Sub CheckBoxFindReplaceFindRXAssembly_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceFindRXAssembly.Checked Then
    '        CheckBoxFindReplaceFindPTAssembly.Checked = False
    '        CheckBoxFindReplaceFindWCAssembly.Checked = False
    '    End If
    'End Sub

    'Private Sub CheckBoxFindReplaceReplacePTAssembly_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceReplacePTAssembly.Checked Then
    '        CheckBoxFindReplaceReplaceRXAssembly.Checked = False
    '    End If
    'End Sub
    'Private Sub CheckBoxFindReplaceReplaceRXAssembly_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceReplaceRXAssembly.Checked Then
    '        CheckBoxFindReplaceReplacePTAssembly.Checked = False
    '    End If
    'End Sub

    'Private Sub CheckBoxFindReplaceFindPTPart_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceFindPTPart.Checked Then
    '        CheckBoxFindReplaceFindWCPart.Checked = False
    '        CheckBoxFindReplaceFindRXPart.Checked = False
    '    End If
    'End Sub
    'Private Sub CheckBoxFindReplaceFindWCPart_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceFindWCPart.Checked Then
    '        CheckBoxFindReplaceFindPTPart.Checked = False
    '        CheckBoxFindReplaceFindRXPart.Checked = False
    '    End If
    'End Sub
    'Private Sub CheckBoxFindReplaceFindRXPart_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceFindRXPart.Checked Then
    '        CheckBoxFindReplaceFindPTPart.Checked = False
    '        CheckBoxFindReplaceFindWCPart.Checked = False
    '    End If
    'End Sub

    'Private Sub CheckBoxFindReplaceReplacePTPart_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceReplacePTPart.Checked Then
    '        CheckBoxFindReplaceReplaceRXPart.Checked = False
    '    End If
    'End Sub
    'Private Sub CheckBoxFindReplaceReplaceRXPart_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceReplaceRXPart.Checked Then
    '        CheckBoxFindReplaceReplacePTPart.Checked = False
    '    End If
    'End Sub
    'Private Sub CheckBoxFindReplaceFindPTSheetmetal_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceFindPTSheetmetal.Checked Then
    '        CheckBoxFindReplaceFindWCSheetmetal.Checked = False
    '        CheckBoxFindReplaceFindRXSheetmetal.Checked = False
    '    End If
    'End Sub
    'Private Sub CheckBoxFindReplaceFindWCSheetmetal_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceFindWCSheetmetal.Checked Then
    '        CheckBoxFindReplaceFindPTSheetmetal.Checked = False
    '        CheckBoxFindReplaceFindRXSheetmetal.Checked = False
    '    End If
    'End Sub
    'Private Sub CheckBoxFindReplaceFindRXSheetmetal_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceFindRXSheetmetal.Checked Then
    '        CheckBoxFindReplaceFindPTSheetmetal.Checked = False
    '        CheckBoxFindReplaceFindWCSheetmetal.Checked = False
    '    End If
    'End Sub

    'Private Sub CheckBoxFindReplaceReplacePTSheetmetal_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceReplacePTSheetmetal.Checked Then
    '        CheckBoxFindReplaceReplaceRXSheetmetal.Checked = False
    '    End If
    'End Sub
    'Private Sub CheckBoxFindReplaceReplaceRXSheetmetal_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBoxFindReplaceReplaceRXSheetmetal.Checked Then
    '        CheckBoxFindReplaceReplacePTSheetmetal.Checked = False
    '    End If
    'End Sub

    Private Sub CheckBoxTLAIncludePartCopies_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTLAIncludePartCopies.CheckedChanged
        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange
    End Sub

    Private Sub CheckBoxDraftAndModelSameName_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDraftAndModelSameName.CheckedChanged
        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

    End Sub

    Private Sub RadioButtonListSortDependency_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonListSortDependency.CheckedChanged
        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

        If RadioButtonListSortDependency.Checked Then
            CheckBoxListIncludeNoDependencies.Enabled = True
        Else
            CheckBoxListIncludeNoDependencies.Enabled = False
        End If
    End Sub

    Private Sub RadioButtonListSortNone_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonListSortNone.CheckedChanged
        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

    End Sub

    Private Sub RadioButtonListSortAlphabetical_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonListSortAlphabetical.CheckedChanged
        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

    End Sub

    Private Sub CheckBoxListIncludeNoDependencies_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxListIncludeNoDependencies.CheckedChanged
        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

    End Sub

    Private Sub RadioButtonListSortRandomSample_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonListSortRandomSample.CheckedChanged
        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

        If RadioButtonListSortRandomSample.Checked Then
            TextBoxRandomSampleFraction.Enabled = True
        Else
            TextBoxRandomSampleFraction.Enabled = False
        End If
    End Sub

    Private Sub TextBoxRandomSampleFraction_LostFocus(sender As Object, e As EventArgs) Handles TextBoxRandomSampleFraction.LostFocus
        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

        Dim Fraction As Double
        Try
            Fraction = CDbl(TextBoxRandomSampleFraction.Text)
            If Fraction < 0 Or Fraction > 1 Then
                MsgBox(String.Format("Number '{0}' is not between 0.0 and 1.0", Fraction))
                TextBoxRandomSampleFraction.Text = "0.1"
            End If
        Catch ex As Exception
            MsgBox(String.Format("Cannot convert '{0}' to a decimal number", TextBoxRandomSampleFraction.Text))
            TextBoxRandomSampleFraction.Text = "0.1"
        End Try
    End Sub

    Private Sub CheckBoxProcessReadOnly_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxProcessReadOnly.CheckedChanged
        Dim StatusChangeRadioButtons As New List(Of RadioButton)
        Dim RB As RadioButton

        StatusChangeRadioButtons = GetStatusChangeRadioButtons()

        If CheckBoxProcessReadOnly.Checked Then
            RadioButtonReadOnlyRevert.Enabled = True
            RadioButtonReadOnlyChange.Enabled = True
            If RadioButtonReadOnlyChange.Checked Then
                For Each RB In StatusChangeRadioButtons
                    RB.Enabled = True
                Next
            End If
        Else
            RadioButtonReadOnlyRevert.Enabled = False
            RadioButtonReadOnlyChange.Enabled = False
            For Each RB In StatusChangeRadioButtons
                RB.Enabled = False
            Next
        End If
    End Sub

    Private Sub RadioButtonReadOnlyRevert_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonReadOnlyRevert.CheckedChanged
        Dim StatusChangeRadioButtons As New List(Of RadioButton)
        Dim RB As RadioButton

        StatusChangeRadioButtons = GetStatusChangeRadioButtons()

        If RadioButtonReadOnlyRevert.Checked Then
            For Each RB In StatusChangeRadioButtons
                RB.Enabled = False
            Next
        Else
            For Each RB In StatusChangeRadioButtons
                RB.Enabled = True
            Next
        End If
    End Sub

    Private Function GetStatusChangeRadioButtons(Optional CheckedOnly As Boolean = False) As List(Of RadioButton)
        Dim StatusChangeRadioButtons As New List(Of RadioButton)
        Dim tmpList As New List(Of RadioButton)

        tmpList.Add(RadioButtonStatusAtoA)
        tmpList.Add(RadioButtonStatusAtoB)
        tmpList.Add(RadioButtonStatusAtoIR)
        tmpList.Add(RadioButtonStatusAtoIW)
        tmpList.Add(RadioButtonStatusAtoO)
        tmpList.Add(RadioButtonStatusAtoR)

        tmpList.Add(RadioButtonStatusBtoA)
        tmpList.Add(RadioButtonStatusBtoB)
        tmpList.Add(RadioButtonStatusBtoIR)
        tmpList.Add(RadioButtonStatusBtoIW)
        tmpList.Add(RadioButtonStatusBtoO)
        tmpList.Add(RadioButtonStatusBtoR)

        tmpList.Add(RadioButtonStatusIRtoA)
        tmpList.Add(RadioButtonStatusIRtoB)
        tmpList.Add(RadioButtonStatusIRtoIR)
        tmpList.Add(RadioButtonStatusIRtoIW)
        tmpList.Add(RadioButtonStatusIRtoO)
        tmpList.Add(RadioButtonStatusIRtoR)

        tmpList.Add(RadioButtonStatusIWtoA)
        tmpList.Add(RadioButtonStatusIWtoB)
        tmpList.Add(RadioButtonStatusIWtoIR)
        tmpList.Add(RadioButtonStatusIWtoIW)
        tmpList.Add(RadioButtonStatusIWtoO)
        tmpList.Add(RadioButtonStatusIWtoR)

        tmpList.Add(RadioButtonStatusOtoA)
        tmpList.Add(RadioButtonStatusOtoB)
        tmpList.Add(RadioButtonStatusOtoIR)
        tmpList.Add(RadioButtonStatusOtoIW)
        tmpList.Add(RadioButtonStatusOtoO)
        tmpList.Add(RadioButtonStatusOtoR)

        tmpList.Add(RadioButtonStatusRtoA)
        tmpList.Add(RadioButtonStatusRtoB)
        tmpList.Add(RadioButtonStatusRtoIR)
        tmpList.Add(RadioButtonStatusRtoIW)
        tmpList.Add(RadioButtonStatusRtoO)
        tmpList.Add(RadioButtonStatusRtoR)

        If CheckedOnly Then
            For Each RB As RadioButton In tmpList
                If RB.Checked Then
                    StatusChangeRadioButtons.Add(RB)
                End If
            Next
        Else
            For Each RB As RadioButton In tmpList
                StatusChangeRadioButtons.Add(RB)
            Next
        End If

        Return StatusChangeRadioButtons
    End Function

    Private Sub CheckBoxEnablePrinter2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxEnablePrinter2.CheckedChanged
        If CheckBoxEnablePrinter2.Checked Then
            ComboBoxPrinter2.Enabled = True
            TextBoxPrinter2Copies.Enabled = True
            CheckBoxPrinter2AutoOrient.Enabled = True
            CheckBoxPrinter2BestFit.Enabled = True
            CheckBoxPrinter2PrintAsBlack.Enabled = True
            CheckBoxPrinter2ScaleLineTypes.Enabled = True
            CheckBoxPrinter2ScaleLineWidths.Enabled = True

            RadioButtonSheetsAnsiPrinter2.Enabled = True
            RadioButtonSheetsIsoPrinter2.Enabled = True
            RadioButtonSheetsAllPrinter2.Enabled = True
            TextBoxPrinter2SheetSelections.Enabled = True
            ButtonPrinter2SheetSelections.Enabled = True
        Else
            ComboBoxPrinter2.Enabled = False
            TextBoxPrinter2Copies.Enabled = False
            CheckBoxPrinter2AutoOrient.Enabled = False
            CheckBoxPrinter2BestFit.Enabled = False
            CheckBoxPrinter2PrintAsBlack.Enabled = False
            CheckBoxPrinter2ScaleLineTypes.Enabled = False
            CheckBoxPrinter2ScaleLineWidths.Enabled = False

            RadioButtonSheetsAnsiPrinter2.Enabled = False
            RadioButtonSheetsIsoPrinter2.Enabled = False
            RadioButtonSheetsAllPrinter2.Enabled = False
            TextBoxPrinter2SheetSelections.Enabled = False
            ButtonPrinter2SheetSelections.Enabled = False
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub RadioButtonSheetsAnsiPrinter2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSheetsAnsiPrinter2.CheckedChanged
        'If RadioButtonSheetsAnsiPrinter2.Checked Then
        '    Dim PD As New PrinterDoctor
        '    Dim SheetListDict As New Dictionary(Of String, SolidEdgeDraft.PaperSizeConstants)
        '    SheetListDict = PD.GetSheetSizes("Ansi")

        '    ListBoxSheetsPrinter2.Items.Clear()
        '    For Each Key As String In SheetListDict.Keys
        '        ListBoxSheetsPrinter2.Items.Add(Key)
        '    Next
        'End If
    End Sub

    Private Sub RadioButtonSheetsIsoPrinter2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSheetsIsoPrinter2.CheckedChanged
        'If RadioButtonSheetsIsoPrinter2.Checked Then
        '    Dim PD As New PrinterDoctor
        '    Dim SheetListDict As New Dictionary(Of String, SolidEdgeDraft.PaperSizeConstants)
        '    SheetListDict = PD.GetSheetSizes("Iso")

        '    ListBoxSheetsPrinter2.Items.Clear()
        '    For Each Key As String In SheetListDict.Keys
        '        ListBoxSheetsPrinter2.Items.Add(Key)
        '    Next
        'End If
    End Sub


    Private Sub RadioButtonSheetsAllPrinter2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSheetsAllPrinter2.CheckedChanged
        'If RadioButtonSheetsAllPrinter2.Checked Then
        '    Dim PD As New PrinterDoctor
        '    Dim SheetListDict As New Dictionary(Of String, SolidEdgeDraft.PaperSizeConstants)
        '    SheetListDict = PD.GetSheetSizes("All")

        '    ListBoxSheetsPrinter2.Items.Clear()
        '    For Each Key As String In SheetListDict.Keys
        '        ListBoxSheetsPrinter2.Items.Add(Key)
        '    Next
        'End If
    End Sub

    Private Sub ButtonPrinter2SheetSelections_Click(sender As Object, e As EventArgs) Handles ButtonPrinter2SheetSelections.Click
        Dim Filter As String
        'Printer2SelectedSheets = New Dictionary(Of String, SolidEdgeDraft.PaperSizeConstants)

        If RadioButtonSheetsAnsiPrinter2.Checked Then
            Filter = "Ansi"
        ElseIf RadioButtonSheetsIsoPrinter2.Checked Then
            Filter = "Iso"
        ElseIf RadioButtonSheetsAllPrinter2.Checked Then
            Filter = "All"
        Else
            RadioButtonSheetsAnsiPrinter2.Checked = True
            Filter = "Ansi"
        End If

        FormSheetSelector.ShowForm2(Filter)

        If FormSheetSelector.DialogResult = DialogResult.OK Then
            TextBoxPrinter2SheetSelections.Text = ""
            For Each SheetSize As String In Printer2SelectedSheets.Keys
                If TextBoxPrinter2SheetSelections.Text = "" Then
                    TextBoxPrinter2SheetSelections.Text = SheetSize
                Else
                    TextBoxPrinter2SheetSelections.Text = String.Format("{0}, {1}", TextBoxPrinter2SheetSelections.Text, SheetSize)
                End If
            Next
        End If

    End Sub

    Private Sub CheckBoxEnablePrinter1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxEnablePrinter1.CheckedChanged
        If CheckBoxEnablePrinter1.Checked Then
            ComboBoxPrinter1.Enabled = True
            TextBoxPrinter1Copies.Enabled = True
            CheckBoxPrinter1AutoOrient.Enabled = True
            CheckBoxPrinter1BestFit.Enabled = True
            CheckBoxPrinter1PrintAsBlack.Enabled = True
            CheckBoxPrinter1ScaleLineTypes.Enabled = True
            CheckBoxPrinter1ScaleLineWidths.Enabled = True
        Else
            ComboBoxPrinter1.Enabled = False
            TextBoxPrinter1Copies.Enabled = False
            CheckBoxPrinter1AutoOrient.Enabled = False
            CheckBoxPrinter1BestFit.Enabled = False
            CheckBoxPrinter1PrintAsBlack.Enabled = False
            CheckBoxPrinter1ScaleLineTypes.Enabled = False
            CheckBoxPrinter1ScaleLineWidths.Enabled = False
        End If
        ReconcileFormChanges()

    End Sub

    Private Sub ButtonVariableEditPart_Click(sender As Object, e As EventArgs) Handles ButtonVariablesEditPart.Click
        Dim VariableInputEditor As New FormVariableInputEditor
        Dim TableValuesDict As New Dictionary(Of String, Dictionary(Of String, String))

        VariableInputEditor.ShowInputEditor("par")

        If VariableInputEditor.DialogResult = DialogResult.OK Then
            TextBoxVariablesEditPart.Text = VariableInputEditor.TextBoxJSON.Text

            If VariableInputEditor.CheckBoxCopyToAsm.Checked Then
                TextBoxVariablesEditAssembly.Text = TextBoxVariablesEditPart.Text
            End If
            If VariableInputEditor.CheckBoxCopyToPsm.Checked Then
                TextBoxVariablesEditSheetmetal.Text = TextBoxVariablesEditPart.Text
            End If
        End If
    End Sub

    Private Sub ButtonVariablesEditAssembly_Click(sender As Object, e As EventArgs) Handles ButtonVariablesEditAssembly.Click
        Dim VariableInputEditor As New FormVariableInputEditor
        Dim TableValuesDict As New Dictionary(Of String, Dictionary(Of String, String))

        VariableInputEditor.ShowInputEditor("asm")

        If VariableInputEditor.DialogResult = DialogResult.OK Then
            TextBoxVariablesEditAssembly.Text = VariableInputEditor.TextBoxJSON.Text

            If VariableInputEditor.CheckBoxCopyToPar.Checked Then
                TextBoxVariablesEditPart.Text = TextBoxVariablesEditAssembly.Text
            End If
            If VariableInputEditor.CheckBoxCopyToPsm.Checked Then
                TextBoxVariablesEditSheetmetal.Text = TextBoxVariablesEditAssembly.Text
            End If
        End If
    End Sub


    Private Sub CheckBoxUpdatePhysicalPropertiesCOGShow_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxUpdatePhysicalPropertiesCOGShow.CheckedChanged
        If CheckBoxUpdatePhysicalPropertiesCOGShow.Checked Then
            CheckBoxUpdatePhysicalPropertiesCOGHide.Checked = False
        End If
    End Sub

    Private Sub CheckBoxUpdatePhysicalPropertiesCOGHide_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxUpdatePhysicalPropertiesCOGHide.CheckedChanged
        If CheckBoxUpdatePhysicalPropertiesCOGHide.Checked Then
            CheckBoxUpdatePhysicalPropertiesCOGShow.Checked = False
        End If

    End Sub

    Private Sub ButtonVariablesEditSheetmetal_Click(sender As Object, e As EventArgs) Handles ButtonVariablesEditSheetmetal.Click
        Dim VariableInputEditor As New FormVariableInputEditor
        Dim TableValuesDict As New Dictionary(Of String, Dictionary(Of String, String))

        VariableInputEditor.ShowInputEditor("psm")

        If VariableInputEditor.DialogResult = DialogResult.OK Then
            TextBoxVariablesEditSheetmetal.Text = VariableInputEditor.TextBoxJSON.Text

            If VariableInputEditor.CheckBoxCopyToAsm.Checked Then
                TextBoxVariablesEditAssembly.Text = TextBoxVariablesEditSheetmetal.Text
            End If
            If VariableInputEditor.CheckBoxCopyToPar.Checked Then
                TextBoxVariablesEditPart.Text = TextBoxVariablesEditSheetmetal.Text
            End If
        End If
    End Sub

    Private Sub ButtonPropertiesEditAssembly_Click(sender As Object, e As EventArgs) Handles ButtonPropertiesEditAssembly.Click
        PropertiesEdit("asm")
    End Sub

    Private Sub ButtonPropertiesEditPart_Click(sender As Object, e As EventArgs) Handles ButtonPropertiesEditPart.Click
        PropertiesEdit("par")
    End Sub

    Private Sub ButtonPropertiesEditSheetmetal_Click(sender As Object, e As EventArgs) Handles ButtonPropertiesEditSheetmetal.Click
        PropertiesEdit("psm")
    End Sub

    Private Sub ButtonPropertiesEditDraft_Click(sender As Object, e As EventArgs) Handles ButtonPropertiesEditDraft.Click
        PropertiesEdit("dft")
    End Sub

    Private Sub PropertiesEdit(FileType As String)
        Dim PropertyInputEditor As New FormPropertyInputEditor
        Dim s As String

        PropertyInputEditor.ShowInputEditor(FileType)

        If PropertyInputEditor.DialogResult = DialogResult.OK Then
            s = PropertyInputEditor.TextBoxJSON.Text

            If PropertyInputEditor.CheckBoxCopyToAsm.Checked Then
                TextBoxPropertiesEditAssembly.Text = s
            End If
            If PropertyInputEditor.CheckBoxCopyToPar.Checked Then
                TextBoxPropertiesEditPart.Text = s
            End If
            If PropertyInputEditor.CheckBoxCopyToPsm.Checked Then
                TextBoxPropertiesEditSheetmetal.Text = s
            End If
            If PropertyInputEditor.CheckBoxCopyToDft.Checked Then
                TextBoxPropertiesEditDraft.Text = s
            End If

        End If

    End Sub

    Private Sub CheckBoxStockSizeXYZ_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxStockSizeXYZ.CheckedChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxStockSizeMinMidMax_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxStockSizeMinMidMax.CheckedChanged
        ReconcileFormChanges()
    End Sub








    ' Commands I can never remember

    ' System.Windows.Forms.Application.DoEvents()

    ' tf = FileIO.FileSystem.FileExists(Filename)

    ' tf = Not FileIO.FileSystem.DirectoryExists(TextBoxSaveAsAssemblyOutputDirectory.Text)

    ' If Not FileIO.FileSystem.DirectoryExists(BaseDir) Then
    '     FileIO.FileSystem.CreateDirectory(BaseDir)
    ' End If

    ' Extension = IO.Path.GetExtension(WhereUsedFile)
    ' C:\project\part.par -> .par

    ' DirName = System.IO.Path.GetDirectoryName(SEDoc.FullName)
    ' C:\project\part.par -> C:\project

    ' BaseName = System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))
    ' C:\project\part.par -> part

    ' BaseFilename = System.IO.Path.GetFileName(SEDoc.FullName)
    ' C:\project\part.par -> part.par

    ' System.Threading.Thread.Sleep(100)

    ' TypeName = Microsoft.VisualBasic.Information.TypeName(SEDoc)

    ' Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()

    ' Dim Defaults As String() = Nothing
    ' Defaults = IO.File.ReadAllLines(DefaultsFilename)

    ' Dim Defaults As New List(Of String)
    ' IO.File.WriteAllLines(DefaultsFilename, Defaults)

    ' Iterate through an Enum
    ' For Each PaperSizeConstant In System.Enum.GetValues(GetType(SolidEdgeDraft.PaperSizeConstants))


End Class
