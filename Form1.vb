Imports System.Runtime.InteropServices

Public Class Form1
    Private SEApp As SolidEdgeFramework.Application

    Private DefaultsFilename As String
    Private LogfileName As String

    Private ErrorsOccurred As Boolean

    Private FilesToProcessTotal As Integer
    Private FilesToProcessCompleted As Integer

    Private TemplateRequiredAssembly As Boolean
    Private TemplateRequiredPart As Boolean
    Private TemplateRequiredSheetmetal As Boolean
    Private TemplateRequiredDraft As Boolean

    Private StopProcess As Boolean

    Delegate Function SEOpAssembly(ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument)
    Private ListToActionAssembly As New Dictionary(Of String, SEOpAssembly)
    Private ListToTemplateAssembly As New Dictionary(Of String, Boolean)

    Delegate Function SEOpPart(ByVal SEDoc As SolidEdgePart.PartDocument)
    Private ListToActionPart As New Dictionary(Of String, SEOpPart)
    Private ListToTemplatePart As New Dictionary(Of String, Boolean)

    Delegate Function SEOpSheetmetal(ByVal SEDoc As SolidEdgePart.SheetMetalDocument)
    Private ListToActionSheetmetal As New Dictionary(Of String, SEOpSheetmetal)
    Private ListToTemplateSheetmetal As New Dictionary(Of String, Boolean)

    Delegate Function SEOpDraft(ByVal SEDoc As SolidEdgeDraft.DraftDocument)
    Private ListToActionDraft As New Dictionary(Of String, SEOpDraft)
    Private ListToTemplateDraft As New Dictionary(Of String, Boolean)

    'DESCRIPTION
    'Solid Edge Housekeeper
    'Robert McAnany 2020
    '
    'Portions adapted from code by Jason Newell, Greg Chasteen, Tushar Suradkar, and others.
    '
    'This description is about the organization of the code.  To read how to use it, see the 
    'Readme Tab on Form1.
    '
    'The program performs various tasks on batches of files.  Each task for each file type is 
    'contained in a separate Function.  
    '
    'The basic flow is Process() -> ProcessFiles() -> ProcessFile().  The ProcessFile() routine 
    'calls, in turn, each task that has been checked on the form.
    '
    'Error handling is localized in the ProcessFile() routine.  Running hundreds or thousands of 
    'files in a row can sometimes cause an Application malfunction.  In such cases, ProcessFile() 
    'retries the tasks.  Most of the time it succeeds on the second attempt.
    '
    'The mapping between checkboxes and tasks is (somewhat opaquely) done with dictionaries, one 
    'for each file type.  The naming convention is ListToAction<file type>, e.g., 
    'ListToActionAssembly, ListToActionPart, etc.  The dictionary keys are the checkbox labels, 
    'the values are pointers to the desired Function.  The mapping happens in the 
    'BuildListToAction() routine.  
    '
    'The main processing is done in this file.  Ancillary related routines are housed in their 
    'own *.vb file.

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

        Dim WaitSeconds As Integer = 3
        Dim reps As Integer = 2
        Dim msg As String
        For i As Integer = 0 To CheckedListBoxDraft.Items.Count - 1
            For k As Integer = 0 To CheckedListBoxDraft.Items.Count - 1
                CheckedListBoxDraft.SetItemChecked(k, False)
            Next
            CheckedListBoxDraft.SetItemChecked(i, True)
            If i > 0 Then
                System.Threading.Thread.Sleep(1000 * WaitSeconds)
            End If

            For j As Integer = 1 To reps
                FilesToProcessCompleted = 0
                LogfileSetName()
                'MsgBox(CheckedListBoxDraft.CheckedItems(0))
                LogfileAppend(CheckedListBoxDraft.CheckedItems(0), "D:\something", Chr(13))
                'MsgBox(CheckedListBoxDraft.Items.Count.ToString)
                msg = "Checkbox " + (i + 1).ToString + "/" + CheckedListBoxDraft.Items.Count.ToString
                msg += ", Rep " + (j).ToString + "/" + reps.ToString
                LabelInputDirectory.Text = msg
                If j > 1 Then
                    'TextBoxStatus.Text = "Sleeping 30 s before Rep " + j.ToString
                    System.Threading.Thread.Sleep(1000 * WaitSeconds)
                End If


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
            Next
        Next

        OleMessageFilter.Revoke()

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

        If TemplateRequiredAssembly Then
            If Not FileIO.FileSystem.FileExists(TextBoxTemplateAssembly.Text) Then
                msg += "    Select a valid assembly template" + Chr(13)
            End If
        End If

        If TemplateRequiredPart Then
            If Not FileIO.FileSystem.FileExists(TextBoxTemplatePart.Text) Then
                msg += "    Select a valid part template" + Chr(13)
            End If
        End If

        If TemplateRequiredSheetmetal Then
            If Not FileIO.FileSystem.FileExists(TextBoxTemplateSheetmetal.Text) Then
                msg += "    Select a valid sheetmetal template" + Chr(13)
            End If
        End If

        If TemplateRequiredDraft Then
            If Not FileIO.FileSystem.FileExists(TextBoxTemplateDraft.Text) Then
                msg += "    Select a valid draft template" + Chr(13)
            End If
        End If

        If Len(msg) <> 0 Then
            msg = "Please correct the following before continuing" + Chr(13) + msg
        End If

        Return msg
    End Function

    Private Sub ProcessFiles(ByVal Filetype As String)
        Dim FilesToProcess As List(Of String)
        Dim FileToProcess As String
        Dim RestartAfter As Integer
        'Dim Count As Integer = 0
        Dim msg As String

        Try
            RestartAfter = Int(TextBoxRestartAfter.Text)
        Catch ex As Exception
            RestartAfter = 50
            TextBoxRestartAfter.Text = 50
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
            MsgBox("Filetype not recognized: " + Filetype + ".  Exiting...")
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

        Dim SEDoc As SolidEdgeFramework.SolidEdgeDocument
        Dim CheckedListBoxX As CheckedListBox

        Dim RunTasks = Sub()
                           If Filetype = "Assembly" Then
                               SEDoc = DirectCast(SEApp.Documents.Open(Path),
                                                     SolidEdgeAssembly.AssemblyDocument)
                               CheckedListBoxX = CheckedListBoxAssembly
                           ElseIf Filetype = "Part" Then
                               SEDoc = DirectCast(SEApp.Documents.Open(Path),
                                                     SolidEdgePart.PartDocument)
                               CheckedListBoxX = CheckedListBoxPart
                           ElseIf Filetype = "Sheetmetal" Then
                               SEDoc = DirectCast(SEApp.Documents.Open(Path),
                                                     SolidEdgePart.SheetMetalDocument)
                               CheckedListBoxX = CheckedListBoxSheetmetal
                           ElseIf Filetype = "Draft" Then
                               SEDoc = DirectCast(SEApp.Documents.Open(Path),
                                                     SolidEdgeDraft.DraftDocument)
                               CheckedListBoxX = CheckedListBoxDraft
                           Else
                               MsgBox("Filetype not recognized: " + Filetype + ".  Exiting...")
                               End
                           End If

                           For Each LabelText As String In CheckedListBoxX.CheckedItems
                               'LogfileAppend("Trying " + LabelText, TruncateFullPath(Path), "")

                               'Call the function of the checked item
                               'Note, ListToActionAssembly.Item(LabelText) is a function.
                               'Also, ListToActionPart.Item(LabelText), etc.
                               'See BuiltListToAction() for details.
                               If Filetype = "Assembly" Then
                                   ErrorMessageList = ListToActionAssembly.Item(LabelText)(SEDoc)
                               ElseIf Filetype = "Part" Then
                                   ErrorMessageList = ListToActionPart.Item(LabelText)(SEDoc)
                               ElseIf Filetype = "Sheetmetal" Then
                                   ErrorMessageList = ListToActionSheetmetal.Item(LabelText)(SEDoc)
                               Else
                                   ErrorMessageList = ListToActionDraft.Item(LabelText)(SEDoc)
                               End If

                               ExitStatus = ErrorMessageList(0)
                               SupplementalErrorMessage = ErrorMessageList(1)

                               If ExitStatus <> "0" Then
                                   LogfileAppend(LabelText, TruncateFullPath(Path), SupplementalErrorMessage)
                               End If
                           Next

                           SEDoc.Close(False)
                           SEApp.DoIdle()
                       End Sub

        Try
            RunTasks()
        Catch ex As Exception
            LogfileAppend("Retrying", TruncateFullPath(Path), "" + Chr(13) + ex.ToString + Chr(13))
            SEStop()
            SEStart()

            TextBoxStatus.Text = "Retrying " + TruncateFullPath(Path)

            Try
                RunTasks()
            Catch ex2 As Exception
                LogfileAppend("Error processing file", TruncateFullPath(Path), ex2.ToString + Chr(13))
                SEStop()
                SEStart()
            End Try
        End Try

    End Sub


    Private Sub Startup()
        BuildListToActions()
        LoadDefaults()
        LoadTextBoxReadme()
        UpdateFileTypes()
        FolderBrowserDialog1.SelectedPath = TextBoxInputDirectory.Text
        IO.Directory.SetCurrentDirectory(TextBoxInputDirectory.Text)
    End Sub

    Private Sub CheckOrUncheckAll(ByVal CheckedListBoxX As CheckedListBox)
        Dim NoneChecked As Boolean

        'If no items are checked, NoneChecked will be True
        NoneChecked = CheckedListBoxX.CheckedItems.Count = 0

        For i As Integer = 0 To CheckedListBoxX.Items.Count - 1
            CheckedListBoxX.SetItemChecked(i, NoneChecked)
        Next

        UpdateFileTypes()

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
        UpdateFileTypes()
        UpdateTemplateRequired()
    End Sub

    Private Sub CheckedListBoxAssembly_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBoxAssembly.DoubleClick
        CheckOrUncheckAll(CheckedListBoxAssembly)
    End Sub

    Private Sub CheckedListBoxPart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBoxPart.SelectedIndexChanged
        UpdateFileTypes()
        UpdateTemplateRequired()
    End Sub
    Private Sub CheckedListBoxPart_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBoxPart.DoubleClick
        CheckOrUncheckAll(CheckedListBoxPart)
    End Sub

    Private Sub CheckedListBoxSheetmetal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBoxSheetmetal.SelectedIndexChanged
        UpdateFileTypes()
        UpdateTemplateRequired()
    End Sub
    Private Sub CheckedListBoxSheetmetal_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBoxSheetmetal.DoubleClick
        CheckOrUncheckAll(CheckedListBoxSheetmetal)
    End Sub

    Private Sub CheckedListBoxDraft_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBoxDraft.SelectedIndexChanged
        UpdateFileTypes()
        UpdateTemplateRequired()
    End Sub
    Private Sub CheckedListBoxDraft_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBoxDraft.DoubleClick
        CheckOrUncheckAll(CheckedListBoxDraft)
    End Sub

    Private Sub ButtonInputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonInputDirectory.Click
        FolderBrowserDialog1.ShowDialog()
        TextBoxInputDirectory.Text = FolderBrowserDialog1.SelectedPath
        ToolTip1.SetToolTip(TextBoxInputDirectory, TextBoxInputDirectory.Text)
        UpdateListBoxFiles()
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
    End Sub

    Private Sub ButtonTemplatePart_Click(sender As Object, e As EventArgs) Handles ButtonTemplatePart.Click
        OpenFileDialog1.Filter = "Part Documents|*.par"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTemplatePart.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub ButtonTemplateSheetmetal_Click(sender As Object, e As EventArgs) Handles ButtonTemplateSheetmetal.Click
        OpenFileDialog1.Filter = "Sheetmetal Documents|*.psm"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTemplateSheetmetal.Text = OpenFileDialog1.FileName
        End If

    End Sub

    Private Sub ButtonTemplateDraft_Click(sender As Object, e As EventArgs) Handles ButtonTemplateDraft.Click
        OpenFileDialog1.Filter = "Draft Documents|*.dft"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTemplateDraft.Text = OpenFileDialog1.FileName
        End If

    End Sub

    Private Sub ButtonActiveMaterialLibrary_Click(sender As Object, e As EventArgs) Handles ButtonActiveMaterialLibrary.Click
        OpenFileDialog1.Filter = "Material Documents|*.mtl"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxActiveMaterialLibrary.Text = OpenFileDialog1.FileName
        End If

    End Sub

    Private Sub ButtonLaserOutputDirectory_Click(sender As Object, e As EventArgs) Handles ButtonLaserOutputDirectory.Click
        FolderBrowserDialog1.ShowDialog()
        TextBoxLaserOutputDirectory.Text = FolderBrowserDialog1.SelectedPath
        'ToolTip1.SetToolTip(TextBoxInputDirectory, TextBoxInputDirectory.Text)
        'UpdateListBoxFiles()

    End Sub
End Class
