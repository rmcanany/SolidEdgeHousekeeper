﻿Option Strict On

Imports System.Runtime.InteropServices
Imports SolidEdgeCommunity


Public Class Form1
    Public SEApp As SolidEdgeFramework.Application

    Private DefaultsFilename As String
    Private LogfileName As String

    Private ErrorsOccurred As Boolean
    Private TotalAborts As Double
    Private TotalAbortsMaximum As Integer = 4

    Private FilesToProcessTotal As Integer
    Private FilesToProcessCompleted As Integer
    Dim StartTime As DateTime

    Public Shared StopProcess As Boolean

    Private ListBoxFilesOutOfDate As Boolean

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

    Private Sub Process()
        Dim ErrorMessage As String
        Dim ElapsedTime As Double
        Dim ElapsedTimeText As String

        StartTime = Now

        SaveDefaults()

        ErrorMessage = CheckStartConditions()
        If ErrorMessage <> "" Then
            Dim result As MsgBoxResult = MsgBox(ErrorMessage, vbOKCancel)
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

        If CheckBoxCreateTODOList.Checked Then
            ClearTODOList()

        End If

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
            TextBoxStatus.Text = "Processing aborted"
        Else
            ElapsedTime = Now.Subtract(StartTime).TotalMinutes
            If ElapsedTime < 60 Then
                ElapsedTimeText = "in " + ElapsedTime.ToString("0.0") + " min."
            Else
                ElapsedTimeText = "in " + (ElapsedTime / 60).ToString("0.0") + " hr."
            End If

            TextBoxStatus.Text = "Finished processing " + FilesToProcessTotal.ToString + " files " + ElapsedTimeText
        End If

        StopProcess = False
        ButtonCancel.Text = "Cancel"

        If ErrorsOccurred Then
            Dim msg As String = ""
            Dim indent As String = ""
            Dim FancyPrint As List(Of String) = LogfileName.Split("\"c).ToList
            msg += "Some checks did not pass and/or more feedback is available." + Chr(13)
            msg += "Path to log file:" + Chr(13)
            For Each s As String In FancyPrint
                msg += indent + s + Chr(13)
                indent += "    "
            Next
            MsgBox(msg)
        Else
            TextBoxStatus.Text = TextBoxStatus.Text + "  All checks passed."
        End If

    End Sub

    Private Function CheckStartConditions() As String
        Dim msg As String = ""
        Dim SaveMsg As String = ""
        Dim tf As Boolean

        ReconcileFormChanges()

        If SEIsRunning() Then
            msg += "    Close Solid Edge" + Chr(13)
        End If

        If DMIsRunning() Then
            msg += "    Close Design Manager" + Chr(13)
        End If

        tf = CheckedListBoxAssembly.CheckedItems.Count = 0
        tf = tf And CheckedListBoxPart.CheckedItems.Count = 0
        tf = tf And CheckedListBoxSheetmetal.CheckedItems.Count = 0
        tf = tf And CheckedListBoxDraft.CheckedItems.Count = 0
        If tf Then
            msg += "    Select a task to perform on at least one Task tab" + Chr(13)
        End If

        If Not IsCheckedFilesToProcess() Then
            msg += "    Select an option on Files To Process" + Chr(13)
        End If

        For Each Filename As String In ListBoxFiles.Items
            If Not FileIO.FileSystem.FileExists(String.Format("{0}/{1}", TextBoxInputDirectory.Text, Filename)) Then
                msg += "    Some files have been renamed or deleted" + Chr(13)
                ListBoxFilesOutOfDate = True
                Exit For
            End If
        Next

        If ListBoxFilesOutOfDate Then
            msg += "    Update the file list" + Chr(13)
        ElseIf ListBoxFiles.Items.Count = 0 Then
            msg += "    Select an input directory with files to process" + Chr(13)
        End If

        If Not FileIO.FileSystem.DirectoryExists(TextBoxInputDirectory.Text) Then
            msg += "    Select a valid input directory" + Chr(13)
        End If

        If RadioButtonTopLevelAssembly.Checked Then
            If Not FileIO.FileSystem.FileExists(TextBoxTopLevelAssembly.Text) Then
                msg += "    Select a valid top level assembly" + Chr(13)
            End If
            tf = RadioButtonTLABottomUp.Checked
            tf = tf Or RadioButtonTLATopDown.Checked
            If Not tf Then
                msg += "    Set top level assembly processing option on the Configuration tab" + Chr(13)
            End If
        End If

        If CheckBoxFileSearch.Checked Then
            If TextBoxFileSearch.Text = "" Then
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
                End If
            End If

            If LabelToActionAssembly(Label).RequiresFindReplaceFields Then
                If TextBoxFindReplacePropertyNameAssembly.Text = "" Then
                    If Not msg.Contains("Enter a valid assembly property name") Then
                        msg += "    Enter a valid assembly property name" + Chr(13)
                    End If
                End If
                If TextBoxFindReplaceFindAssembly.Text = "" Then
                    If Not msg.Contains("Enter a valid assembly search string") Then
                        msg += "    Enter a valid assembly search string" + Chr(13)
                    End If
                End If
            End If
        Next

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
                End If
            End If

            If LabelToActionPart(Label).RequiresFindReplaceFields Then
                If TextBoxFindReplacePropertyNamePart.Text = "" Then
                    If Not msg.Contains("Enter a valid part property name") Then
                        msg += "    Enter a valid part property name" + Chr(13)
                    End If
                End If
                If TextBoxFindReplaceFindPart.Text = "" Then
                    If Not msg.Contains("Enter a valid part search string") Then
                        msg += "    Enter a valid part search string" + Chr(13)
                    End If
                End If
            End If


        Next

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
            If LabelToActionSheetmetal(Label).RequiresLaserOutputDirectory Then
                tf = Not FileIO.FileSystem.DirectoryExists(TextBoxLaserOutputDirectory.Text)
                tf = tf And Not CheckBoxLaserOutputDirectory.Checked
                If tf Then
                    If Not msg.Contains("Select a valid laser output directory") Then
                        msg += "    Select a valid laser output directory" + Chr(13)
                    End If
                End If
            End If
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
            If LabelToActionSheetmetal(Label).RequiresSaveAsFlatDXFOutputDirectory Then
                tf = Not FileIO.FileSystem.DirectoryExists(TextBoxSaveAsFlatDXFOutputDirectory.Text)
                tf = tf And Not CheckBoxSaveAsFlatDXFOutputDirectory.Checked
                If tf Then
                    If Not msg.Contains("Select a valid Save As Flat DXF output directory") Then
                        msg += "    Select a valid Save As Flat DXF output directory" + Chr(13)
                    End If
                End If
            End If
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
                End If
            End If

            If LabelToActionSheetmetal(Label).RequiresFindReplaceFields Then
                If TextBoxFindReplacePropertyNameSheetmetal.Text = "" Then
                    If Not msg.Contains("Enter a valid sheetmetal property name") Then
                        msg += "    Enter a valid sheetmetal property name" + Chr(13)
                    End If
                End If
                If TextBoxFindReplaceFindSheetmetal.Text = "" Then
                    If Not msg.Contains("Enter a valid sheetmetal search string") Then
                        msg += "    Enter a valid sheetmetal search string" + Chr(13)
                    End If
                End If
            End If


        Next

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
            If LabelToActionDraft(Label).RequiresLaserOutputDirectory Then
                tf = Not FileIO.FileSystem.DirectoryExists(TextBoxLaserOutputDirectory.Text)
                tf = tf And Not CheckBoxLaserOutputDirectory.Checked
                If tf Then
                    If Not msg.Contains("Select a valid laser output directory") Then
                        msg += "    Select a valid laser output directory" + Chr(13)
                    End If
                End If
            End If
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

        If FilesToProcessCompleted > 2 Then
            ElapsedTime = Now.Subtract(StartTime).TotalMinutes

            TotalEstimatedTime = ElapsedTime * CDbl(FilesToProcessTotal) / CDbl(FilesToProcessCompleted)
            RemainingTime = TotalEstimatedTime - ElapsedTime

            If RemainingTime < 60 Then
                If RemainingTime < 0.1 Then
                    LabelTimeRemaining.Text = ""
                Else
                    LabelTimeRemaining.Text = String.Format("Estimated time remaining: {0} min.", RemainingTime.ToString("0.0"))
                End If
            Else
                LabelTimeRemaining.Text = String.Format("Estimated time remaining: {0} hr.", (RemainingTime / 60).ToString("0.0"))
            End If
        End If
    End Sub

    Private Sub ProcessFiles(ByVal Filetype As String)
        Dim FilesToProcess As List(Of String)
        Dim FileToProcess As String
        Dim RestartAfter As Integer
        Dim msg As String
        Dim ErrorMessagesCombined As New Dictionary(Of String, List(Of String))

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
            System.Windows.Forms.Application.DoEvents()
            If StopProcess Then
                TextBoxStatus.Text = "Processing aborted"
                Exit Sub
            End If

            FilesToProcessCompleted += 1

            If FilesToProcessCompleted Mod RestartAfter = 0 Then
                SEStop()
                SEStart()
            End If

            msg = FilesToProcessCompleted.ToString + "/" + FilesToProcessTotal.ToString + " "
            msg += TruncateFullPath(FileToProcess)
            TextBoxStatus.Text = msg

            ErrorMessagesCombined = ProcessFile(FileToProcess, Filetype)

            If ErrorMessagesCombined.Count > 0 Then
                LogfileAppend(FileToProcess, ErrorMessagesCombined)
            End If

        Next
    End Sub

    Private Function ProcessFile(ByVal Path As String, ByVal Filetype As String) As Dictionary(Of String, List(Of String))
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

        Try
            SEDoc = DirectCast(SEApp.Documents.Open(Path), SolidEdgeFramework.SolidEdgeDocument)
            SEDoc.Activate()
            SEApp.DoIdle()

            ' Maximize the window in the application
            If Filetype = "Draft" Then
                ActiveSheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
                ActiveSheetWindow.WindowState = 2
            Else
                ActiveWindow = CType(SEApp.ActiveWindow, SolidEdgeFramework.Window)
                ActiveWindow.WindowState = 2
            End If

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
                'LogfileAppend("Trying " + LabelText, TruncateFullPath(Path), "")
                'MsgBox("Trying " + LabelText + " " + Path)

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

            If LabelText = "Move drawing to new template" Then
                OriginalFilename = SEDoc.FullName
                ModifiedFilename = String.Format("{0}\{1}-Housekeeper.dft",
                                            System.IO.Path.GetDirectoryName(SEDoc.FullName),
                                            System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))
                RemnantsFilename = String.Format("{0}\{1}-HousekeeperOld.dft",
                                            System.IO.Path.GetDirectoryName(SEDoc.FullName),
                                            System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))
            End If

            SEDoc.Close(False)
            SEApp.DoIdle()

            If LabelText = "Move drawing to new template" Then
                If ExitStatus = 0 Then
                    System.IO.File.Delete(OriginalFilename)
                    FileSystem.Rename(ModifiedFilename, OriginalFilename)
                ElseIf ExitStatus = 1 Then
                    If System.IO.File.Exists(RemnantsFilename) Then
                        System.IO.File.Delete(RemnantsFilename)
                    End If
                    If System.IO.File.Exists(ModifiedFilename) Then  ' Not created if a task-generated file was processed
                        FileSystem.Rename(OriginalFilename, RemnantsFilename)
                        FileSystem.Rename(ModifiedFilename, OriginalFilename)
                    End If
                ElseIf ExitStatus = 2 Then
                    ' Nothing was saved.  Leave SEDoc unmodified.
                End If

            End If

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


    Private Sub Startup()

        PopulateCheckedListBoxes()
        LoadDefaults()
        ReconcileFormChanges()
        LoadTextBoxReadme()

        FakeFolderBrowserDialog.Filter = "No files (*.___)|(*.___)"
        If Not TextBoxInputDirectory.Text = "" Then
            FakeFolderBrowserDialog.InitialDirectory = TextBoxInputDirectory.Text
            OpenFileDialog1.InitialDirectory = TextBoxInputDirectory.Text
        End If

        IO.Directory.SetCurrentDirectory(TextBoxInputDirectory.Text)

        ' ButtonUpdateListBoxFiles.Enabled = False

        ListBoxFilesOutOfDate = False

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
        Dim tf As Boolean
        ' Update configuration
        Configuration = GetConfiguration()

        If ListBoxFilesOutOfDate Then
            ListBoxFiles.Items.Clear()
            tf = RadioButtonTopLevelAssembly.Checked
            tf = tf Or CheckBoxEnablePropertyFilter.Checked
            If tf Then
                ButtonUpdateListBoxFiles.Enabled = True
                ButtonUpdateListBoxFiles.BackColor = System.Drawing.Color.Orange
                ButtonUpdateListBoxFiles.UseVisualStyleBackColor = False
            Else
                'ButtonUpdateListBoxFiles.Enabled = False
                ButtonUpdateListBoxFiles.BackColor = System.Drawing.SystemColors.Control
                ButtonUpdateListBoxFiles.UseVisualStyleBackColor = True

                tf = RadioButtonFilesDirectoriesAndSubdirectories.Checked
                tf = tf Or RadioButtonFilesDirectoryOnly.Checked
                tf = tf Or RadioButtonTODOList.Checked
                If tf Then
                    UpdateListBoxFiles()
                End If
            End If
        End If

        If CheckBoxEnablePropertyFilter.Checked Then
            ButtonPropertyFilter.Enabled = True
        Else
            ButtonPropertyFilter.Enabled = False
        End If

        If CheckBoxFileSearch.Checked Then
            TextBoxFileSearch.Enabled = True
        Else
            TextBoxFileSearch.Enabled = False
        End If

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
        OpenFileDialog1.Filter = "Material Documents|*.mtl"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxActiveMaterialLibrary.Text = OpenFileDialog1.FileName
            OpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName)
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        If ButtonCancel.Text = "Stop" Then
            StopProcess = True
        Else
            SaveDefaults()
            End
        End If
    End Sub

    Private Sub ButtonExternalProgramAssembly_Click(sender As Object, e As EventArgs) Handles ButtonExternalProgramAssembly.Click
        OpenFileDialog1.Filter = "Programs|*.exe"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxExternalProgramAssembly.Text = OpenFileDialog1.FileName
            OpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName)
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonExternalProgramPart_Click(sender As Object, e As EventArgs) Handles ButtonExternalProgramPart.Click
        OpenFileDialog1.Filter = "Programs|*.exe"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxExternalProgramPart.Text = OpenFileDialog1.FileName
            OpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName)
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonExternalProgramSheetmetal_Click(sender As Object, e As EventArgs) Handles ButtonExternalProgramSheetmetal.Click
        OpenFileDialog1.Filter = "Programs|*.exe"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxExternalProgramSheetmetal.Text = OpenFileDialog1.FileName
            OpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName)
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonExternalProgramDraft_Click(sender As Object, e As EventArgs) Handles ButtonExternalProgramDraft.Click
        OpenFileDialog1.Filter = "Programs|*.exe"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxExternalProgramDraft.Text = OpenFileDialog1.FileName
            OpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName)
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonSaveAsDraftOutputDirectory_Click(sender As Object, e As EventArgs)
        FakeFolderBrowserDialog.FileName = "Select Folder"
        If TextBoxSaveAsDraftOutputDirectory.Text <> "" Then
            FakeFolderBrowserDialog.InitialDirectory = TextBoxSaveAsDraftOutputDirectory.Text
        End If
        If FakeFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            TextBoxSaveAsDraftOutputDirectory.Text = System.IO.Path.GetDirectoryName(FakeFolderBrowserDialog.FileName)
            FakeFolderBrowserDialog.InitialDirectory = TextBoxSaveAsDraftOutputDirectory.Text
        End If
        ToolTip1.SetToolTip(TextBoxSaveAsDraftOutputDirectory, TextBoxSaveAsDraftOutputDirectory.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonInputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonInputDirectory.Click
        FakeFolderBrowserDialog.FileName = "Select Folder"
        If TextBoxInputDirectory.Text <> "" Then
            FakeFolderBrowserDialog.InitialDirectory = TextBoxInputDirectory.Text
        End If
        If FakeFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            TextBoxInputDirectory.Text = System.IO.Path.GetDirectoryName(FakeFolderBrowserDialog.FileName)
            FakeFolderBrowserDialog.InitialDirectory = TextBoxInputDirectory.Text
            ListBoxFilesOutOfDate = True
        End If

        ToolTip1.SetToolTip(TextBoxInputDirectory, TextBoxInputDirectory.Text)

        ReconcileFormChanges()
    End Sub

    Private Sub ButtonLaserOutputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonLaserOutputDirectory.Click
        FakeFolderBrowserDialog.FileName = "Select Folder"
        If TextBoxLaserOutputDirectory.Text <> "" Then
            FakeFolderBrowserDialog.InitialDirectory = TextBoxLaserOutputDirectory.Text
        End If
        If FakeFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            TextBoxLaserOutputDirectory.Text = System.IO.Path.GetDirectoryName(FakeFolderBrowserDialog.FileName)
            FakeFolderBrowserDialog.InitialDirectory = TextBoxLaserOutputDirectory.Text
        End If
        ToolTip1.SetToolTip(TextBoxLaserOutputDirectory, TextBoxLaserOutputDirectory.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonPdfDraftOutputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonSaveAsDraftOutputDirectory.Click
        FakeFolderBrowserDialog.FileName = "Select Folder"
        If TextBoxSaveAsDraftOutputDirectory.Text <> "" Then
            FakeFolderBrowserDialog.InitialDirectory = TextBoxSaveAsDraftOutputDirectory.Text
        End If
        If FakeFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            TextBoxSaveAsDraftOutputDirectory.Text = System.IO.Path.GetDirectoryName(FakeFolderBrowserDialog.FileName)
            FakeFolderBrowserDialog.InitialDirectory = TextBoxSaveAsDraftOutputDirectory.Text
        End If
        ToolTip1.SetToolTip(TextBoxSaveAsDraftOutputDirectory, TextBoxSaveAsDraftOutputDirectory.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonPrintOptions_Click(sender As Object, e As EventArgs) Handles ButtonPrintOptions.Click
        PrintDialog1.ShowDialog()
    End Sub

    Private Sub ButtonPropertyFilter_Click(sender As Object, e As EventArgs) Handles ButtonPropertyFilter.Click
        Dim tf As Boolean

        FormPropertyFilter.GetPropertyFilter()

        tf = FormPropertyFilter.DialogResult = DialogResult.OK
        tf = tf And PropertyFilterFormula <> ""
        If tf Then
            ' ListBoxFiles.Items.Clear()
            ListBoxFilesOutOfDate = True
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonProcess_Click(sender As Object, e As EventArgs) Handles ButtonProcess.Click
        Process()
    End Sub

    Private Sub ButtonSaveAsFlatDXF_Click(sender As Object, e As EventArgs) Handles ButtonSaveAsFlatDXF.Click
        FakeFolderBrowserDialog.FileName = "Select Folder"
        If TextBoxSaveAsFlatDXFOutputDirectory.Text <> "" Then
            FakeFolderBrowserDialog.InitialDirectory = TextBoxSaveAsFlatDXFOutputDirectory.Text
        End If
        If FakeFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            TextBoxSaveAsFlatDXFOutputDirectory.Text = System.IO.Path.GetDirectoryName(FakeFolderBrowserDialog.FileName)
            FakeFolderBrowserDialog.InitialDirectory = TextBoxSaveAsFlatDXFOutputDirectory.Text
        End If

        ReconcileFormChanges()
    End Sub

    Private Sub ButtonStepAssemblyOutputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonSaveAsAssemblyOutputDirectory.Click
        FakeFolderBrowserDialog.FileName = "Select Folder"
        If TextBoxSaveAsAssemblyOutputDirectory.Text <> "" Then
            FakeFolderBrowserDialog.InitialDirectory = TextBoxSaveAsAssemblyOutputDirectory.Text
        End If
        If FakeFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            TextBoxSaveAsAssemblyOutputDirectory.Text = System.IO.Path.GetDirectoryName(FakeFolderBrowserDialog.FileName)
            FakeFolderBrowserDialog.InitialDirectory = TextBoxSaveAsAssemblyOutputDirectory.Text
        End If
        ToolTip1.SetToolTip(TextBoxSaveAsAssemblyOutputDirectory, TextBoxSaveAsAssemblyOutputDirectory.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonStepPartOutputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonSaveAsPartOutputDirectory.Click
        FakeFolderBrowserDialog.FileName = "Select Folder"
        If TextBoxSaveAsPartOutputDirectory.Text <> "" Then
            FakeFolderBrowserDialog.InitialDirectory = TextBoxSaveAsPartOutputDirectory.Text
        End If
        If FakeFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            TextBoxSaveAsPartOutputDirectory.Text = System.IO.Path.GetDirectoryName(FakeFolderBrowserDialog.FileName)
            FakeFolderBrowserDialog.InitialDirectory = TextBoxSaveAsPartOutputDirectory.Text
        End If
        ToolTip1.SetToolTip(TextBoxSaveAsPartOutputDirectory, TextBoxSaveAsPartOutputDirectory.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonStepSheetmetalOutputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonSaveAsSheetmetalOutputDirectory.Click
        FakeFolderBrowserDialog.FileName = "Select Folder"
        If TextBoxSaveAsSheetmetalOutputDirectory.Text <> "" Then
            FakeFolderBrowserDialog.InitialDirectory = TextBoxSaveAsSheetmetalOutputDirectory.Text
        End If
        If FakeFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            TextBoxSaveAsSheetmetalOutputDirectory.Text = System.IO.Path.GetDirectoryName(FakeFolderBrowserDialog.FileName)
            FakeFolderBrowserDialog.InitialDirectory = TextBoxSaveAsSheetmetalOutputDirectory.Text
        End If
        ToolTip1.SetToolTip(TextBoxSaveAsSheetmetalOutputDirectory, TextBoxSaveAsSheetmetalOutputDirectory.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonTemplateAssembly_Click(sender As Object, e As EventArgs) Handles ButtonTemplateAssembly.Click
        OpenFileDialog1.Filter = "Assembly Documents|*.asm"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTemplateAssembly.Text = OpenFileDialog1.FileName
            OpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName)
        End If
        ToolTip1.SetToolTip(TextBoxTemplateAssembly, TextBoxTemplateAssembly.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonTemplateDraft_Click(sender As Object, e As EventArgs) Handles ButtonTemplateDraft.Click
        OpenFileDialog1.Filter = "Draft Documents|*.dft"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTemplateDraft.Text = OpenFileDialog1.FileName
            OpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName)
        End If
        ToolTip1.SetToolTip(TextBoxTemplateDraft, TextBoxTemplateDraft.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonTemplatePart_Click(sender As Object, e As EventArgs) Handles ButtonTemplatePart.Click
        OpenFileDialog1.Filter = "Part Documents|*.par"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTemplatePart.Text = OpenFileDialog1.FileName
            OpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName)
        End If
        ToolTip1.SetToolTip(TextBoxTemplatePart, TextBoxTemplatePart.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonTemplateSheetmetal_Click(sender As Object, e As EventArgs) Handles ButtonTemplateSheetmetal.Click
        OpenFileDialog1.Filter = "Sheetmetal Documents|*.psm"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTemplateSheetmetal.Text = OpenFileDialog1.FileName
            OpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName)
        End If
        ToolTip1.SetToolTip(TextBoxTemplateSheetmetal, TextBoxTemplateSheetmetal.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonTopLevelAssembly_Click(sender As Object, e As EventArgs) Handles ButtonTopLevelAssembly.Click
        OpenFileDialog1.Filter = "Assembly Documents|*.asm"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTopLevelAssembly.Text = OpenFileDialog1.FileName
            OpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName)
        End If
        ToolTip1.SetToolTip(TextBoxTopLevelAssembly, TextBoxTopLevelAssembly.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonUpdateListBoxFiles_Click(sender As Object, e As EventArgs) Handles ButtonUpdateListBoxFiles.Click
        Dim tf As Boolean
        Dim msg1 As String = ""
        Dim msg2 As String = ""

        If RadioButtonTopLevelAssembly.Checked Then
            tf = RadioButtonTLABottomUp.Checked
            tf = tf Or RadioButtonTLATopDown.Checked
            If Not tf Then
                msg1 = "Select the top level assembly processing option on the Configuration tab."
            End If

            tf = FileIO.FileSystem.FileExists(TextBoxTopLevelAssembly.Text)
            If Not tf Then
                msg2 = "  Select a valid top level assembly on the General tab"
            End If

            tf = (msg1 <> "") Or (msg2 <> "")
            If tf Then
                MsgBox(msg1 + msg2)
                Exit Sub
            End If
        End If

        UpdateListBoxFiles()
    End Sub



    ' FORM LOAD

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Startup()
    End Sub



    ' CHECKBOXES


    Private Sub CheckBoxCreateTODOList_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCreateTODOList.CheckedChanged
        Dim tf As Boolean

        If CheckBoxCreateTODOList.Checked Then
            RadioButtonTODOList.Checked = False

            tf = CheckBoxEnablePropertyFilter.Checked
            tf = tf Or RadioButtonTopLevelAssembly.Checked
            If Not tf Then
                UpdateListBoxFiles()
            End If

        End If

        ReconcileFormChanges()

    End Sub

    Private Sub CheckBoxEnablePropertyFilter_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxEnablePropertyFilter.CheckedChanged
        ListBoxFilesOutOfDate = True

        If CheckBoxEnablePropertyFilter.Checked Then
            If PropertyFilterFormula = "" Then
                FormPropertyFilter.GetPropertyFilter()
            End If
        End If

        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxFileSearch_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFileSearch.CheckedChanged
        If CheckBoxFileSearch.Checked Then
            TextBoxFileSearch.Enabled = True
        Else
            TextBoxFileSearch.Enabled = False
        End If
        ListBoxFilesOutOfDate = True
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxFilterAsm_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFilterAsm.CheckedChanged
        ListBoxFilesOutOfDate = True
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxFilterPar_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFilterPar.CheckedChanged
        ListBoxFilesOutOfDate = True
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxFilterPsm_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFilterPsm.CheckedChanged
        ListBoxFilesOutOfDate = True
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxFilterDft_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFilterDft.CheckedChanged
        ListBoxFilesOutOfDate = True
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxLaserOutputDirectory_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxLaserOutputDirectory.CheckedChanged
        If CheckBoxLaserOutputDirectory.Checked Then
            TextBoxLaserOutputDirectory.Enabled = False
        Else
            TextBoxLaserOutputDirectory.Enabled = True
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxMoveDrawingViewAllowPartialSuccess_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxMoveDrawingViewAllowPartialSuccess.CheckedChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxPdfDraftOutputDirectory_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsDraftOutputDirectory.CheckedChanged
        If CheckBoxSaveAsDraftOutputDirectory.Checked Then
            TextBoxSaveAsDraftOutputDirectory.Enabled = False
        Else
            TextBoxSaveAsDraftOutputDirectory.Enabled = True
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxRememberTasks_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRememberTasks.CheckedChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxSaveAsDraftOutputDirectory_CheckedChanged(sender As Object, e As EventArgs)
        If CheckBoxSaveAsDraftOutputDirectory.Checked Then
            TextBoxSaveAsDraftOutputDirectory.Enabled = False
        Else
            TextBoxSaveAsDraftOutputDirectory.Enabled = True
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxSaveAsFlatDXFOutputDirectory_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsFlatDXFOutputDirectory.CheckedChanged
        If CheckBoxSaveAsFlatDXFOutputDirectory.Checked Then
            TextBoxSaveAsFlatDXFOutputDirectory.Enabled = False
        Else
            TextBoxSaveAsFlatDXFOutputDirectory.Enabled = True
        End If
        ReconcileFormChanges()
    End Sub
    Private Sub CheckBoxStepAssemblyOutputDirectory_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsAssemblyOutputDirectory.CheckedChanged
        If CheckBoxSaveAsAssemblyOutputDirectory.Checked Then
            TextBoxSaveAsAssemblyOutputDirectory.Enabled = False
        Else
            TextBoxSaveAsAssemblyOutputDirectory.Enabled = True
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxStepPartOutputDirectory_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsPartOutputDirectory.CheckedChanged
        If CheckBoxSaveAsPartOutputDirectory.Checked Then
            TextBoxSaveAsPartOutputDirectory.Enabled = False
        Else
            TextBoxSaveAsPartOutputDirectory.Enabled = True
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxStepSheetmetalOutputDirectory_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSaveAsSheetmetalOutputDirectory.CheckedChanged
        If CheckBoxSaveAsSheetmetalOutputDirectory.Checked Then
            TextBoxSaveAsSheetmetalOutputDirectory.Enabled = False
        Else
            TextBoxSaveAsSheetmetalOutputDirectory.Enabled = True
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxTLAReportUnrelatedFiles_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTLAReportUnrelatedFiles.CheckedChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxWarnSave_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxWarnSave.CheckedChanged
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



    ' RADIO BUTTONS

    Private Sub RadioButtonFilesDirectoriesAndSubdirectories_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonFilesDirectoriesAndSubdirectories.CheckedChanged
        If RadioButtonFilesDirectoriesAndSubdirectories.Checked Then
            ListBoxFilesOutOfDate = True
        End If

        ReconcileFormChanges()
    End Sub

    Private Sub RadioButtonFilesDirectoryOnly_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonFilesDirectoryOnly.CheckedChanged
        If RadioButtonFilesDirectoryOnly.Checked Then
            ListBoxFilesOutOfDate = True
        End If

        ReconcileFormChanges()
    End Sub

    Private Sub RadioButtonTLABottomUp_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTLABottomUp.CheckedChanged
        Dim tf As Boolean

        tf = RadioButtonTLABottomUp.Checked
        If tf Then
            CheckBoxTLAReportUnrelatedFiles.Enabled = False
        End If

        tf = RadioButtonTopLevelAssembly.Checked
        If tf Then
            ListBoxFiles.Items.Clear()
        End If

        ReconcileFormChanges()
    End Sub

    Private Sub RadioButtonTLATopDown_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTLATopDown.CheckedChanged
        Dim tf As Boolean

        tf = RadioButtonTLATopDown.Checked
        If tf Then
            CheckBoxTLAReportUnrelatedFiles.Enabled = True
        End If

        tf = RadioButtonTopLevelAssembly.Checked
        If tf Then
            ListBoxFiles.Items.Clear()
        End If

        ReconcileFormChanges()
    End Sub

    Private Sub RadioButtonTODOList_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTODOList.CheckedChanged
        If RadioButtonTODOList.Checked Then
            CheckBoxCreateTODOList.Checked = False
            ListBoxFilesOutOfDate = True
        End If

        ReconcileFormChanges()
    End Sub

    Private Sub RadioButtonTopLevelAssembly_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTopLevelAssembly.CheckedChanged
        If RadioButtonTopLevelAssembly.Checked Then
            ListBoxFilesOutOfDate = True
        End If

        ReconcileFormChanges()
    End Sub



    ' TEXT BOXES

    Private Sub TextBoxColumnWidth_TextChanged(sender As Object, e As EventArgs) Handles TextBoxColumnWidth.TextChanged
        Dim ColCharPixels As Double = 5.5
        'Dim MaxFilenameLength As Double

        Try
            ColCharPixels = CDbl(TextBoxColumnWidth.Text)
        Catch ex As Exception
            TextBoxColumnWidth.Text = CStr(ColCharPixels)
        End Try

        ' MaxFilenameLength = CDbl(ListBoxFiles.ColumnWidth) / CDbl(TextBoxColumnWidth.Text)
        ' ListBoxFiles.ColumnWidth = CInt(CDbl(TextBoxColumnWidth.Text) * MaxFilenameLength)
        ListBoxFilesOutOfDate = True
        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxFileSearch_LostFocus(sender As Object, e As EventArgs) Handles TextBoxFileSearch.LostFocus
        ListBoxFilesOutOfDate = True
        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxFontSize_LostFocus(sender As Object, e As EventArgs) Handles TextBoxFontSize.LostFocus
        Dim FontSize As Single = 8

        Try
            FontSize = CSng(TextBoxFontSize.Text)
        Catch ex As Exception
            TextBoxFontSize.Text = CStr(FontSize)
        End Try

        'ListBoxFiles.Font = New Font("Microsoft Sans Serif", FontSize, FontStyle.Regular)
        ListBoxFilesOutOfDate = True
        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxInputDirectory_TextChanged(sender As Object, e As EventArgs) Handles TextBoxInputDirectory.TextChanged
        Dim tf As Boolean

        tf = RadioButtonFilesDirectoriesAndSubdirectories.Checked
        tf = tf Or RadioButtonFilesDirectoryOnly.Checked
        tf = tf And Not CheckBoxEnablePropertyFilter.Checked

        If tf Then
            UpdateListBoxFiles()
        Else
            ListBoxFiles.Items.Clear()
        End If

        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxPartNumberPropertyName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPartNumberPropertyName.TextChanged
        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxStatus_TextChanged(sender As Object, e As EventArgs) Handles TextBoxStatus.TextChanged
        ToolTip1.SetToolTip(TextBoxStatus, TextBoxStatus.Text)
    End Sub

    Private Sub TextBoxTopLevelAssembly_TextChanged(sender As Object, e As EventArgs) Handles TextBoxTopLevelAssembly.TextChanged
        Dim tf As Boolean

        tf = RadioButtonTopLevelAssembly.Checked

        If tf Then
            ListBoxFiles.Items.Clear()
        End If

        ReconcileFormChanges()

    End Sub


    ' Commands I can never remember
    ' tf = FileIO.FileSystem.FileExists(Filename)

    ' Extension = IO.Path.GetExtension(WhereUsedFile)
    ' C:\project\part.par -> .par

    ' DirName = System.IO.Path.GetDirectoryName(SEDoc.FullName)
    ' C:\project\part.par -> C:\project

    ' BaseName = System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))
    ' C:\project\part.par -> part

    ' System.Threading.Thread.Sleep(100)
    ' 

End Class
