Option Strict On

Imports Microsoft.WindowsAPICodePack.Dialogs
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

    Private FormPropertyFilter As New FormPropertyFilter()
    Public Shared PropertyFilterFormula As String
    Public Shared PropertyFilterDict As New Dictionary(Of String, Dictionary(Of String, String))

    Public Property TaskList As List(Of Task)
    Public Property RememberTaskSelections As Boolean



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

        ReconcileFormChanges()
        SaveDefaults()

        Dim PU As New PreferencesUtilities
        PU.SaveTaskList(Me.TaskList)

        ErrorMessage = CheckStartConditions()

        'Dim LVF = Me.ListViewFiles
        If ErrorMessage <> "" Then
            Me.Cursor = Cursors.Default
            Dim result As MsgBoxResult = MsgBox(ErrorMessage, vbOKOnly, "Check start conditions")
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

        StartTime = Now

        Dim PartCount As Integer = 0
        Dim SheetmetalCount As Integer = 0
        Dim AssemblyCount As Integer = 0
        Dim DraftCount As Integer = 0

        For Each Task As Task In Me.TaskList
            If Task.IsSelectedTask And Task.IsSelectedPart Then PartCount += 1
            If Task.IsSelectedTask And Task.IsSelectedSheetmetal Then SheetmetalCount += 1
            If Task.IsSelectedTask And Task.IsSelectedAssembly Then AssemblyCount += 1
            If Task.IsSelectedTask And Task.IsSelectedDraft Then DraftCount += 1
        Next

        If PartCount > 0 Then ProcessFiles("Part")
        If SheetmetalCount > 0 Then ProcessFiles("Sheetmetal")
        If AssemblyCount > 0 Then ProcessFiles("Assembly")
        If DraftCount > 0 Then ProcessFiles("Draft")

        If Not CheckBoxUseCurrentSession.Checked Then SEStop()

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

        ReconcileFormChanges()

        If SEIsRunning() And Not CheckBoxUseCurrentSession.Checked Then
            msg += "    Close Solid Edge" + Chr(13)
        End If

        If DMIsRunning() Then
            msg += "    Close Design Manager" + Chr(13)
        End If

        If ListViewFilesOutOfDate Then
            msg += "    Update the file list" + Chr(13)
        End If

        If RadioButtonTLABottomUp.Checked Then
            If Not FileIO.FileSystem.FileExists(TextBoxFastSearchScopeFilename.Text) Then
                msg += "    Enter a valid Fast Search Scope file (on the Configuration Tab - Top Level Assembly Page)" + Chr(13)
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

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        ErrorMessage(0) = New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim NoTaskSelected As Boolean = True

        For Each Task As Task In Me.TaskList
            If Task.IsSelectedTask Then
                'MsgBox("Update task with info from the form")
                NoTaskSelected = False
                If Task.RequiresSourceDirectories Then
                    Dim FLU As New FileListUtilities(Me.ListViewFiles)
                    Task.SourceDirectories = FLU.GetSourceDirectories()
                End If
                ErrorMessage = Task.CheckStartConditions(ErrorMessage)
            End If
        Next

        If NoTaskSelected Then
            msg += String.Format("    Select at least one task to perform{0}", vbCrLf)
        End If

        ExitStatus = ErrorMessage.Keys(0)
        If ExitStatus > 0 Then
            For Each s As String In ErrorMessage(ExitStatus)
                msg += String.Format("    {0}{1}", s, vbCrLf)
            Next
        End If

        If Len(msg) <> 0 Then
            msg = "Please correct the following before continuing" + Chr(13) + msg
        End If

        If (Len(SaveMsg) <> 0) And CheckBoxWarnSave.Checked Then
            Dim s As String = "The following options require the original file to be saved." + Chr(13)
            s += "Please verify you have a backup before continuing."
            SaveMsg += Chr(13) + "Disable this warning on the Configuration Tab -- Open/Save Page."
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

            If Not RestartAfter = 0 Then
                If FilesToProcessCompleted Mod RestartAfter = 0 Then
                    SEStop()
                    SEStart()
                End If
            End If

            msg = FilesToProcessCompleted.ToString + "/" + FilesToProcessTotal.ToString + " "
            'msg += CommonTasks.TruncateFullPath(FileToProcess, Nothing)
            msg += System.IO.Path.GetFileName(FileToProcess)
            TextBoxStatus.Text = msg

            ErrorMessagesCombined = ProcessFile(FileToProcess, Filetype, DMApp)

            If ErrorMessagesCombined.Count > 0 Then
                Dim tmpPath As String = System.IO.Path.GetDirectoryName(FileToProcess)
                Dim tmpFilename As String = System.IO.Path.GetFileName(FileToProcess)
                Dim s As String = String.Format("{0} in {1}", tmpFilename, tmpPath)

                LogfileAppend(s, ErrorMessagesCombined)
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
        Dim ModifiedFilename As String = ""
        Dim OriginalFilename As String = ""
        Dim RemnantsFilename As String = ""

        Dim ActiveWindow As SolidEdgeFramework.Window
        Dim ActiveSheetWindow As SolidEdgeDraft.SheetWindow

        Dim TC As New Task_Common

        Dim tf As Boolean

        ' Account for infrequent malfunctions on a large number of files.
        TotalAborts -= 0.1
        If TotalAborts < 0 Then
            TotalAborts = 0
        End If

        ' Deal with Document Status
        Dim OldStatus As SolidEdgeConstants.DocumentStatus
        Dim StatusChangeSuccessful As Boolean

        If CheckBoxProcessReadOnly.Checked Then

            OldStatus = TC.GetStatus(DMApp, Path)

            '' For some reason if OldStatus is igAvailable, OldStatus = Nothing is True
            'If OldStatus = Nothing Then
            '    ErrorMessagesCombined("Unable to read document Status") = New List(Of String) From {""}
            'End If

            StatusChangeSuccessful = TC.SetStatus(DMApp, Path, SolidEdgeConstants.DocumentStatus.igStatusAvailable)
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

            'If Filetype = "Assembly" Then
            '    CheckedListBoxX = CheckedListBoxAssembly
            'ElseIf Filetype = "Part" Then
            '    CheckedListBoxX = CheckedListBoxPart
            'ElseIf Filetype = "Sheetmetal" Then
            '    CheckedListBoxX = CheckedListBoxSheetmetal
            'ElseIf Filetype = "Draft" Then
            '    CheckedListBoxX = CheckedListBoxDraft
            'Else
            '    MsgBox("In ProcessFile(), Filetype not recognized: " + Filetype + ".  Exiting...")
            '    SEApp.Quit()
            '    End
            'End If

            For Each Task As Task In Me.TaskList
                If Task.IsSelectedTask Then
                    tf = (Filetype = "Assembly") And (Task.IsSelectedAssembly)
                    tf = tf Or ((Filetype = "Part") And (Task.IsSelectedPart))
                    tf = tf Or ((Filetype = "Sheetmetal") And (Task.IsSelectedSheetmetal))
                    tf = tf Or ((Filetype = "Draft") And (Task.IsSelectedDraft))

                    If tf Then

                        ErrorMessage = Task.Process(SEDoc, Configuration, SEApp)

                        ExitStatus = ErrorMessage.Keys(0)

                        If ExitStatus <> 0 Then
                            ErrorMessagesCombined(Task.Description) = ErrorMessage(ErrorMessage.Keys(0))

                            If ExitStatus = 99 Then
                                StopProcess = True
                            End If

                        End If
                    End If
                End If
            Next

            SEDoc.Close(False)
            SEApp.DoIdle()


            ' Deal with Document Status
            If CheckBoxProcessReadOnly.Checked Then
                If RadioButtonReadOnlyRevert.Checked Then
                    If Not OldStatus = SolidEdgeConstants.DocumentStatus.igStatusAvailable Then
                        StatusChangeSuccessful = TC.SetStatus(DMApp, Path, OldStatus)
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

                    StatusChangeSuccessful = TC.SetStatus(DMApp, Path, NewStatus)
                    If Not StatusChangeSuccessful Then
                        ErrorMessagesCombined(
                            String.Format("Change status to '{0}' did not succeed", NewStatus.ToString)
                            ) = New List(Of String) From {""}
                    End If

                End If

                'DMApp.Quit()

            End If

        Catch ex As Exception
            Dim AbortList As New List(Of String)

            AbortList.Add(ex.ToString)

            TotalAborts += 1
            If TotalAborts >= TotalAbortsMaximum Then
                StopProcess = True
                AbortList.Add(String.Format("Total aborts exceed maximum of {0}.  Exiting...", TotalAbortsMaximum))
            Else
                'If Not CheckBoxUseCurrentSession.Checked Then
                SEStop()
                SEStart()
                'End If
            End If
            ErrorMessagesCombined("Error processing file") = AbortList
        End Try

        UpdateTimeRemaining()

        Return ErrorMessagesCombined
    End Function


    Private Sub Startup()

        Dim PU As New PreferencesUtilities()

        PU.CreatePreferencesFolder()
        PU.CreateFilenameCharmap()

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

        Me.Text = "Solid Edge Housekeeper 2024.2"

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

        Me.TaskList = PU.GetTaskList

        Dim IU As New InterfaceUtilities

        IU.BuildTaskPage(Me.TaskList, TabPageTasks)

    End Sub


    Private Sub PopulateCheckedListBoxes()

        'CheckedListBoxAssembly.Items.Clear()
        'For Each Key In LabelToActionAssembly.Keys
        '    CheckedListBoxAssembly.Items.Add(Key)
        'Next

        'CheckedListBoxPart.Items.Clear()
        'For Each Key In LabelToActionPart.Keys
        '    CheckedListBoxPart.Items.Add(Key)
        'Next

        'CheckedListBoxSheetmetal.Items.Clear()
        'For Each Key In LabelToActionSheetmetal.Keys
        '    CheckedListBoxSheetmetal.Items.Add(Key)
        'Next

        'CheckedListBoxDraft.Items.Clear()
        'For Each Key In LabelToActionDraft.Keys
        '    CheckedListBoxDraft.Items.Add(Key)
        'Next
    End Sub

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

    End Sub



    ' **************** CONTROLS ****************
    ' BUTTONS


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        If ButtonCancel.Text = "Stop" Then
            StopProcess = True
        Else
            ReconcileFormChanges()
            SaveDefaults()

            Dim PU As New PreferencesUtilities
            PU.SaveTaskList(Me.TaskList)
            End
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As EventArgs) Handles Me.FormClosing
        ReconcileFormChanges()
        SaveDefaults()

        Dim PU As New PreferencesUtilities
        PU.SaveTaskList(Me.TaskList)
        End
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

    Private Sub CheckBoxRememberTasks_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRememberTasks.CheckedChanged
        Dim Checkbox = CType(sender, CheckBox)
        Me.RememberTaskSelections = Checkbox.Checked
        ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxTLAReportUnrelatedFiles_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTLAReportUnrelatedFiles.CheckedChanged
        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

        ReconcileFormChanges()
    End Sub


    ' COMBOBOXES

    Private Sub new_ComboBoxFileSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles new_ComboBoxFileSearch.SelectedIndexChanged
        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

        ReconcileFormChanges()
    End Sub

    Private Sub new_ComboBoxFileSearch_LostFocus(sender As Object, e As EventArgs) Handles new_ComboBoxFileSearch.LostFocus

        Dim Key As String = new_ComboBoxFileSearch.Text

        If Not new_ComboBoxFileSearch.Items.Contains(Key) Then
            new_ComboBoxFileSearch.Items.Add(new_ComboBoxFileSearch.Text)
        End If

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


    Private Sub TextBoxStatus_TextChanged(sender As Object, e As EventArgs) Handles TextBoxStatus.TextChanged
        ToolTip1.SetToolTip(TextBoxStatus, TextBoxStatus.Text)
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
                Case Else
                    MsgBox(String.Format("{0}: Extension {1} not recognized", Me.ToString, IO.Path.GetExtension(tmpFileDialog.FileName).ToLower))
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

        Dim TC As New Task_Common

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

                If TC.FilenameIsOK(FileName) Then

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

        Dim TC As New Task_Common

        For Each item As ListViewItem In ListViewFiles.SelectedItems

            DMDoc = CType(DMApp.Open(item.Name), DesignManager.Document)

            TC.tmpList = New Collection
            TC.FindLinked(DMDoc)

            For Each FoundFile In TC.tmpList
                If TC.FilenameIsOK(FoundFile.ToString) Then

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

        TC.tmpList = Nothing

        DMApp.Quit()

    End Sub

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




    ' Commands I can never remember

    ' System.Windows.Forms.Application.DoEvents()

    ' tf = FileIO.FileSystem.FileExists(Filename)

    ' tf = Not FileIO.FileSystem.DirectoryExists(Dirname)

    ' If Not FileIO.FileSystem.DirectoryExists(Dirname) Then
    '     FileIO.FileSystem.CreateDirectory(Dirname)
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
