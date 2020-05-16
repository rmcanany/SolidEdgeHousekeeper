Option Strict On

Imports System.Runtime.InteropServices
Imports SolidEdgeCommunity

Public Class Form1
    Public SEApp As SolidEdgeFramework.Application

    Private DefaultsFilename As String
    Private LogfileName As String

    Private ErrorsOccurred As Boolean
    Private TotalAborts As Integer = 0
    Private TotalAbortsMaximum As Integer = 4

    Private FilesToProcessTotal As Integer
    Private FilesToProcessCompleted As Integer

    Private StopProcess As Boolean

    Private Configuration As New Dictionary(Of String, String)

    Public LabelToActionAssembly As New LabelToAction("Assembly")
    Private LabelToActionPart As New LabelToAction("Part")
    Private LabelToActionSheetmetal As New LabelToAction("Sheetmetal")
    Private LabelToActionDraft As New LabelToAction("Draft")

    Private LaunchTask As New LaunchTask()


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
        Dim StartTime As DateTime = Now
        Dim ElapsedTime As Double
        Dim ElapsedTimeText As String

        SaveDefaults()

        ErrorMessage = CheckStartConditions()
        If ErrorMessage <> "" Then
            MsgBox(ErrorMessage)
            Exit Sub
        End If

        FilesToProcessTotal = GetTotalFilesToProcess()
        FilesToProcessCompleted = 0

        StopProcess = False
        ButtonCancel.Text = "Stop"

        OleMessageFilter.Register()

        FilesToProcessCompleted = 0
        LogfileSetName()

        SEStart()

        If CheckBoxFileTypePart.Checked Then
            ProcessFiles("Part")
        End If

        If CheckBoxFileTypeSheetmetal.Checked Then
            ProcessFiles("Sheetmetal")
        End If

        If CheckBoxFileTypeAssembly.Checked Then
            ProcessFiles("Assembly")
        End If

        If CheckBoxFileTypeDraft.Checked Then
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
            MsgBox("Some checks did not pass.  See log file:" + Chr(13) + LogfileName)
        Else
            TextBoxStatus.Text = TextBoxStatus.Text + "  All checks passed."
        End If

    End Sub

    Private Function CheckStartConditions() As String
        Dim msg As String = ""

        If SEIsRunning() Then
            msg += "    Close Solid Edge" + Chr(13)
        End If

        If Not IsCheckedFilesToProcess() Then
            msg += "    Select an option on Files To Process" + Chr(13)
        End If

        If Not FileIO.FileSystem.DirectoryExists(TextBoxInputDirectory.Text) Then
            msg += "    Select a valid input directory" + Chr(13)
        End If

        ' For the selected tasks, see if outside information, such as a template file, is required.
        ' If so, verify that the outside information is valid.
        For Each Label As String In CheckedListBoxAssembly.CheckedItems
            For Each Item In LabelToActionAssembly
                If Item.LabelText = Label Then
                    If Item.RequiresTemplate Then
                        If Not FileIO.FileSystem.FileExists(TextBoxTemplateAssembly.Text) Then
                            If Not msg.Contains("Select a valid assembly template") Then
                                msg += "    Select a valid assembly template" + Chr(13)
                            End If
                        End If
                    End If
                    If Item.RequiresMaterialTable Then
                        If Not FileIO.FileSystem.FileExists(TextBoxActiveMaterialLibrary.Text) Then
                            If Not msg.Contains("Select a valid material library") Then
                                msg += "    Select a valid material library" + Chr(13)
                            End If
                        End If
                    End If
                    If Item.RequiresLaserOutputDirectory Then
                        If Not FileIO.FileSystem.DirectoryExists(TextBoxLaserOutputDirectory.Text) Then
                            If Not msg.Contains("Select a valid laser output directory") Then
                                msg += "    Select a valid laser output directory" + Chr(13)
                            End If
                        End If
                    End If
                    If Item.RequiresPartNumberFields Then
                        If TextBoxPartNumberPropertyName.Text = "" Then
                            If Not msg.Contains("Select a valid part number property name") Then
                                msg += "    Select a valid part number property name" + Chr(13)
                            End If
                        End If
                    End If
                    Exit For
                End If
            Next
        Next

        For Each Label As String In CheckedListBoxPart.CheckedItems
            For Each Item In LabelToActionPart
                If Item.LabelText = Label Then
                    If Item.RequiresTemplate Then
                        If Not FileIO.FileSystem.FileExists(TextBoxTemplatePart.Text) Then
                            If Not msg.Contains("Select a valid part template") Then
                                msg += "    Select a valid part template" + Chr(13)
                            End If
                        End If
                    End If
                    If Item.RequiresMaterialTable Then
                        If Not FileIO.FileSystem.FileExists(TextBoxActiveMaterialLibrary.Text) Then
                            If Not msg.Contains("Select a valid material library") Then
                                msg += "    Select a valid material library" + Chr(13)
                            End If
                        End If
                    End If
                    If Item.RequiresLaserOutputDirectory Then
                        If Not FileIO.FileSystem.DirectoryExists(TextBoxLaserOutputDirectory.Text) Then
                            If Not msg.Contains("Select a valid laser output directory") Then
                                msg += "    Select a valid laser output directory" + Chr(13)
                            End If
                        End If
                    End If
                    If Item.RequiresPartNumberFields Then
                        If TextBoxPartNumberPropertyName.Text = "" Then
                            If Not msg.Contains("Select a valid part number property name") Then
                                msg += "    Select a valid part number property name" + Chr(13)
                            End If
                        End If
                    End If
                    Exit For
                End If
            Next
        Next

        For Each Label As String In CheckedListBoxSheetmetal.CheckedItems
            For Each Item In LabelToActionSheetmetal
                If Item.LabelText = Label Then
                    If Item.RequiresTemplate Then
                        If Not FileIO.FileSystem.FileExists(TextBoxTemplateSheetmetal.Text) Then
                            If Not msg.Contains("Select a valid sheetmetal template") Then
                                msg += "    Select a valid sheetmetal template" + Chr(13)
                            End If
                        End If
                    End If
                    If Item.RequiresMaterialTable Then
                        If Not FileIO.FileSystem.FileExists(TextBoxActiveMaterialLibrary.Text) Then
                            If Not msg.Contains("Select a valid material library") Then
                                msg += "    Select a valid material library" + Chr(13)
                            End If
                        End If
                    End If
                    If Item.RequiresLaserOutputDirectory Then
                        If Not FileIO.FileSystem.DirectoryExists(TextBoxLaserOutputDirectory.Text) Then
                            If Not msg.Contains("Select a valid laser output directory") Then
                                msg += "    Select a valid laser output directory" + Chr(13)
                            End If
                        End If
                    End If
                    If Item.RequiresPartNumberFields Then
                        If TextBoxPartNumberPropertyName.Text = "" Then
                            If Not msg.Contains("Select a valid part number property name") Then
                                msg += "    Select a valid part number property name" + Chr(13)
                            End If
                        End If
                    End If

                    Exit For
                End If
            Next
        Next

        For Each Label As String In CheckedListBoxDraft.CheckedItems
            For Each Item In LabelToActionDraft
                If Item.LabelText = Label Then
                    If Item.RequiresTemplate Then
                        If Not FileIO.FileSystem.FileExists(TextBoxTemplateDraft.Text) Then
                            If Not msg.Contains("Select a valid draft template") Then
                                msg += "    Select a valid draft template" + Chr(13)
                            End If
                        End If
                    End If
                    If Item.RequiresMaterialTable Then
                        If Not FileIO.FileSystem.FileExists(TextBoxActiveMaterialLibrary.Text) Then
                            If Not msg.Contains("Select a valid material library") Then
                                msg += "    Select a valid material library" + Chr(13)
                            End If
                        End If
                    End If
                    If Item.RequiresLaserOutputDirectory Then
                        If Not FileIO.FileSystem.DirectoryExists(TextBoxLaserOutputDirectory.Text) Then
                            If Not msg.Contains("Select a valid laser output directory") Then
                                msg += "    Select a valid laser output directory" + Chr(13)
                            End If
                        End If
                    End If
                    If Item.RequiresPartNumberFields Then
                        If TextBoxPartNumberPropertyName.Text = "" Then
                            If Not msg.Contains("Select a valid part number property name") Then
                                msg += "    Select a valid part number property name" + Chr(13)
                            End If
                        End If
                    End If

                    Exit For
                End If
            Next
        Next

        If Len(msg) <> 0 Then
            msg = "Please correct the following before continuing" + Chr(13) + msg
        End If

        Return msg
    End Function

    Private Sub ProcessFiles(ByVal Filetype As String)
        Dim FilesToProcess As List(Of String)
        Dim FileToProcess As String
        Dim RestartAfter As Integer
        Dim msg As String

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

            ProcessFile(FileToProcess, Filetype)

        Next
    End Sub

    Private Sub ProcessFile(ByVal Path As String, ByVal Filetype As String)
        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As String
        Dim SupplementalErrorMessage As String

        Dim SEDoc As SolidEdgeFramework.SolidEdgeDocument = Nothing
        Dim CheckedListBoxX As CheckedListBox

        Try
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

            For Each LabelText As String In CheckedListBoxX.CheckedItems
                'LogfileAppend("Trying " + LabelText, TruncateFullPath(Path), "")

                If Filetype = "Assembly" Then
                    SEDoc = DirectCast(SEApp.Documents.Open(Path), SolidEdgeFramework.SolidEdgeDocument)
                    ErrorMessageList = LaunchTask.Launch(SEDoc, Configuration, SEApp, Filetype, LabelToActionAssembly, LabelText)
                ElseIf Filetype = "Part" Then
                    SEDoc = DirectCast(SEApp.Documents.Open(Path), SolidEdgeFramework.SolidEdgeDocument)
                    ErrorMessageList = LaunchTask.Launch(SEDoc, Configuration, SEApp, Filetype, LabelToActionPart, LabelText)
                ElseIf Filetype = "Sheetmetal" Then
                    SEDoc = DirectCast(SEApp.Documents.Open(Path), SolidEdgeFramework.SolidEdgeDocument)
                    ErrorMessageList = LaunchTask.Launch(SEDoc, Configuration, SEApp, Filetype, LabelToActionSheetmetal, LabelText)
                Else
                    SEDoc = DirectCast(SEApp.Documents.Open(Path), SolidEdgeFramework.SolidEdgeDocument)
                    ErrorMessageList = LaunchTask.Launch(SEDoc, Configuration, SEApp, Filetype, LabelToActionDraft, LabelText)
                End If

                ExitStatus = ErrorMessageList(0)
                SupplementalErrorMessage = ErrorMessageList(1)

                If ExitStatus <> "0" Then
                    LogfileAppend(LabelText, TruncateFullPath(Path), SupplementalErrorMessage)
                End If
            Next

            SEDoc.Close(False)
            SEApp.DoIdle()

        Catch ex As Exception
            TotalAborts += 1
            LogfileAppend("Error processing file", TruncateFullPath(Path), ex.ToString + Chr(13))
            If TotalAborts >= TotalAbortsMaximum Then
                StopProcess = True
                LogfileAppend(String.Format("Total aborts exceed maximum of {0}.  Exiting...", TotalAbortsMaximum), "", "" + Chr(13))
            Else
                SEStop()
                SEStart()
            End If
        End Try

    End Sub


    Private Sub Startup()
        PopulateCheckedListBoxes()
        LoadDefaults()
        ReconcileFormChanges()
        LoadTextBoxReadme()

        FakeFolderBrowserDialog.Filter = "No files (*.___)|(*.___)"
        If Not TextBoxInputDirectory.Text = "" Then
            FakeFolderBrowserDialog.InitialDirectory = TextBoxInputDirectory.Text
        End If

        If Not TextBoxTemplateAssembly.Text = "" Then
            OpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(TextBoxTemplateAssembly.Text)
            'Else
            '    If Not TextBoxInputDirectory.Text = "" Then
            '        OpenFileDialog1.InitialDirectory = TextBoxInputDirectory.Text
            '    End If
        End If

        IO.Directory.SetCurrentDirectory(TextBoxInputDirectory.Text)

    End Sub

    Private Sub PopulateCheckedListBoxes()

        CheckedListBoxAssembly.Items.Clear()
        For Each Item In LabelToActionAssembly
            CheckedListBoxAssembly.Items.Add(Item.LabelText)
        Next

        CheckedListBoxPart.Items.Clear()
        For Each Item In LabelToActionPart
            CheckedListBoxPart.Items.Add(Item.LabelText)
        Next

        CheckedListBoxSheetmetal.Items.Clear()
        For Each Item In LabelToActionSheetmetal
            CheckedListBoxSheetmetal.Items.Add(Item.LabelText)
        Next

        CheckedListBoxDraft.Items.Clear()
        For Each Item In LabelToActionDraft
            CheckedListBoxDraft.Items.Add(Item.LabelText)
        Next
    End Sub

    Private Sub ReconcileFormChanges()
        ' Update configuration
        Configuration(TextBoxInputDirectory.Name) = TextBoxInputDirectory.Text
        Configuration(TextBoxTemplateAssembly.Name) = TextBoxTemplateAssembly.Text
        Configuration(TextBoxTemplatePart.Name) = TextBoxTemplatePart.Text
        Configuration(TextBoxTemplateSheetmetal.Name) = TextBoxTemplateSheetmetal.Text
        Configuration(TextBoxTemplateDraft.Name) = TextBoxTemplateDraft.Text
        Configuration(TextBoxActiveMaterialLibrary.Name) = TextBoxActiveMaterialLibrary.Text
        Configuration(TextBoxLaserOutputDirectory.Name) = TextBoxLaserOutputDirectory.Text
        Configuration(ComboBoxPartNumberPropertySet.Name) = ComboBoxPartNumberPropertySet.Text
        Configuration(TextBoxPartNumberPropertyName.Name) = TextBoxPartNumberPropertyName.Text


        ' Update file types
        If CheckedListBoxAssembly.CheckedItems.Count > 0 Then
            CheckBoxFileTypeAssembly.Checked = True
        Else
            CheckBoxFileTypeAssembly.Checked = False
        End If
        If CheckedListBoxPart.CheckedItems.Count > 0 Then
            CheckBoxFileTypePart.Checked = True
        Else
            CheckBoxFileTypePart.Checked = False
        End If
        If CheckedListBoxSheetmetal.CheckedItems.Count > 0 Then
            CheckBoxFileTypeSheetmetal.Checked = True
        Else
            CheckBoxFileTypeSheetmetal.Checked = False
        End If
        If CheckedListBoxDraft.CheckedItems.Count > 0 Then
            CheckBoxFileTypeDraft.Checked = True
        Else
            CheckBoxFileTypeDraft.Checked = False
        End If


        ' Update ListBoxFiles on Form1
        Dim FoundFiles As IReadOnlyCollection(Of String)
        Dim FoundFile As String
        Dim ActiveFileExtensionsList As New List(Of String)

        If CheckBoxFileTypeAssembly.Checked Then
            ActiveFileExtensionsList.Add("*.asm")
        End If
        If CheckBoxFileTypePart.Checked Then
            ActiveFileExtensionsList.Add("*.par")
        End If
        If CheckBoxFileTypeSheetmetal.Checked Then
            ActiveFileExtensionsList.Add("*.psm")
        End If
        If CheckBoxFileTypeDraft.Checked Then
            ActiveFileExtensionsList.Add("*.dft")
        End If

        ListBoxFiles.Items.Clear()

        If ActiveFileExtensionsList.Count > 0 Then
            If FileIO.FileSystem.DirectoryExists(TextBoxInputDirectory.Text) Then

                FoundFiles = FileIO.FileSystem.GetFiles(TextBoxInputDirectory.Text,
                                    FileIO.SearchOption.SearchTopLevelOnly,
                                    ActiveFileExtensionsList.ToArray)

                For Each FoundFile In FoundFiles
                    ListBoxFiles.Items.Add(System.IO.Path.GetFileName(FoundFile))
                Next
            End If
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


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Startup()
    End Sub

    Private Sub ButtonProcess_Click(sender As Object, e As EventArgs) Handles ButtonProcess.Click
        Process()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        If ButtonCancel.Text = "Stop" Then
            StopProcess = True
        Else
            SaveDefaults()
            End
        End If
    End Sub

    Private Sub CheckedListBoxAssembly_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBoxAssembly.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub

    Private Sub CheckedListBoxAssembly_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBoxAssembly.DoubleClick
        CheckOrUncheckAll(CheckedListBoxAssembly)
    End Sub

    Private Sub CheckedListBoxPart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBoxPart.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub
    Private Sub CheckedListBoxPart_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBoxPart.DoubleClick
        CheckOrUncheckAll(CheckedListBoxPart)
    End Sub

    Private Sub CheckedListBoxSheetmetal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBoxSheetmetal.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub
    Private Sub CheckedListBoxSheetmetal_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBoxSheetmetal.DoubleClick
        CheckOrUncheckAll(CheckedListBoxSheetmetal)
    End Sub

    Private Sub CheckedListBoxDraft_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBoxDraft.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub
    Private Sub CheckedListBoxDraft_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBoxDraft.DoubleClick
        CheckOrUncheckAll(CheckedListBoxDraft)
    End Sub

    Private Sub ButtonInputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonInputDirectory.Click
        FakeFolderBrowserDialog.FileName = "Select Folder"
        If TextBoxInputDirectory.Text <> "" Then
            FakeFolderBrowserDialog.InitialDirectory = TextBoxInputDirectory.Text
        End If
        If FakeFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            TextBoxInputDirectory.Text = System.IO.Path.GetDirectoryName(FakeFolderBrowserDialog.FileName)
        End If

        ToolTip1.SetToolTip(TextBoxInputDirectory, TextBoxInputDirectory.Text)

        ReconcileFormChanges()
    End Sub

    Private Sub ListBoxFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxFiles.SelectedIndexChanged
        RadioButtonFilesSelected.Checked = True
    End Sub

    Private Sub ButtonTemplateAssembly_Click(sender As Object, e As EventArgs) Handles ButtonTemplateAssembly.Click
        OpenFileDialog1.Filter = "Assembly Documents|*.asm"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTemplateAssembly.Text = OpenFileDialog1.FileName
        End If
        ToolTip1.SetToolTip(TextBoxTemplateAssembly, TextBoxTemplateAssembly.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonTemplatePart_Click(sender As Object, e As EventArgs) Handles ButtonTemplatePart.Click
        OpenFileDialog1.Filter = "Part Documents|*.par"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTemplatePart.Text = OpenFileDialog1.FileName
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
        End If
        ToolTip1.SetToolTip(TextBoxTemplateSheetmetal, TextBoxTemplateSheetmetal.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonTemplateDraft_Click(sender As Object, e As EventArgs) Handles ButtonTemplateDraft.Click
        OpenFileDialog1.Filter = "Draft Documents|*.dft"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTemplateDraft.Text = OpenFileDialog1.FileName
        End If
        ToolTip1.SetToolTip(TextBoxTemplateDraft, TextBoxTemplateDraft.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonActiveMaterialLibrary_Click(sender As Object, e As EventArgs) Handles ButtonActiveMaterialLibrary.Click
        OpenFileDialog1.Filter = "Material Documents|*.mtl"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxActiveMaterialLibrary.Text = OpenFileDialog1.FileName
        End If
        ReconcileFormChanges()
    End Sub

    Private Sub ButtonLaserOutputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonLaserOutputDirectory.Click
        FakeFolderBrowserDialog.FileName = "Select Folder"
        If TextBoxLaserOutputDirectory.Text <> "" Then
            FakeFolderBrowserDialog.InitialDirectory = TextBoxLaserOutputDirectory.Text
        End If
        If FakeFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            TextBoxLaserOutputDirectory.Text = System.IO.Path.GetDirectoryName(FakeFolderBrowserDialog.FileName)
        End If
        ToolTip1.SetToolTip(TextBoxLaserOutputDirectory, TextBoxLaserOutputDirectory.Text)
        ReconcileFormChanges()
    End Sub

    Private Sub ComboBoxPartNumberPropertySet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPartNumberPropertySet.SelectedIndexChanged
        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxPartNumberPropertyName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPartNumberPropertyName.TextChanged
        ReconcileFormChanges()
    End Sub

    Private Sub TextBoxStatus_TextChanged(sender As Object, e As EventArgs) Handles TextBoxStatus.TextChanged
        ToolTip1.SetToolTip(TextBoxStatus, TextBoxStatus.Text)
    End Sub
End Class
