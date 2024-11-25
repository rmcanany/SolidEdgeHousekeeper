Option Strict On

Imports OpenMcdf
Imports OpenMcdf.Extensions.OLEProperties
Imports SolidEdgeCommunity

Public Class UtilsExecute

    Public Property FMain As Form_Main
    Public Property FilesToProcessTotal As Integer
    Public Property FilesToProcessCompleted As Integer
    'Public Property StopProcess As Boolean
    Public Property UtilsLogFile As UtilsLogFile
    Public Property TotalAborts As Double
    Public Property TotalAbortsMaximum As Integer = 4
    Public Property SEApp As SolidEdgeFramework.Application
    Public Property StartTime As DateTime

    Public Property TextBoxStatus As TextBox


    Public Sub New(_Form_Main As Form_Main)
        Me.FMain = _Form_Main
    End Sub


    Public Sub ProcessAll()

        FMain.Cursor = Cursors.WaitCursor

        Dim ErrorMessage As String
        Dim ElapsedTime As Double
        Dim ElapsedTimeText As String

        FMain.SaveSettings()  ' Updates JSON Properties and saves settings

        ErrorMessage = CheckStartConditions()

        If ErrorMessage <> "" Then
            FMain.Cursor = Cursors.Default
            Dim result As MsgBoxResult = MsgBox(ErrorMessage, vbOKOnly, "Check start conditions")
            If result = MsgBoxResult.Cancel Then
                Exit Sub
            End If
            If ErrorMessage.Contains("Please correct the following before continuing") Then
                Exit Sub
            End If
        End If

        Dim UFL As New UtilsFileList(FMain, FMain.ListViewFiles, FMain.ListViewSources)

        FilesToProcessTotal = UFL.GetTotalFilesToProcess()
        FilesToProcessCompleted = 0

        FMain.StopProcess = False
        FMain.ButtonCancel.Text = "Stop"

        OleMessageFilter.Register()

        Me.UtilsLogFile = New UtilsLogFile

        Me.UtilsLogFile.LogfileSetName()

        TotalAborts = 0

        Dim USEA = New UtilsSEApp
        USEA.TextBoxStatus = Me.TextBoxStatus

        If FMain.SolidEdgeRequired > 0 Then
            USEA.SEStart(FMain.RunInBackground, FMain.UseCurrentSession, FMain.NoUpdateMRU)
            SEApp = USEA.SEApp
        End If

        Me.StartTime = Now

        Dim PartCount As Integer = 0
        Dim SheetmetalCount As Integer = 0
        Dim AssemblyCount As Integer = 0
        Dim DraftCount As Integer = 0

        For Each Task As Task In FMain.TaskList
            If Task.IsSelectedTask And Task.IsSelectedPart Then PartCount += 1
            If Task.IsSelectedTask And Task.IsSelectedSheetmetal Then SheetmetalCount += 1
            If Task.IsSelectedTask And Task.IsSelectedAssembly Then AssemblyCount += 1
            If Task.IsSelectedTask And Task.IsSelectedDraft Then DraftCount += 1
        Next

        ' Process the files
        If PartCount > 0 Then ProcessFiles("Part")
        If SheetmetalCount > 0 Then ProcessFiles("Sheetmetal")
        If AssemblyCount > 0 Then ProcessFiles("Assembly")
        If DraftCount > 0 Then ProcessFiles("Draft")

        If FMain.SolidEdgeRequired > 0 Then
            'Dim USEA = New UtilsSEApp
            USEA.SEStop(FMain.UseCurrentSession)
            SEApp = Nothing
        End If

        OleMessageFilter.Unregister()

        If FMain.StopProcess Then
            If TotalAborts > TotalAbortsMaximum Then
                FMain.TextBoxStatus.Text = "The number of file processing errors exceeded maximum.  Stopping."
            Else
                FMain.TextBoxStatus.Text = "Processing halted by user."
            End If
        Else
            ElapsedTime = Now.Subtract(StartTime).TotalMinutes
            If ElapsedTime < 60 Then
                ElapsedTimeText = "in " + ElapsedTime.ToString("0.0") + " min."
            Else
                ElapsedTimeText = "in " + (ElapsedTime / 60).ToString("0.0") + " hr."
            End If

            FMain.TextBoxStatus.Text = "Finished processing " + FilesToProcessTotal.ToString + " files " + ElapsedTimeText
        End If

        FMain.LabelTimeRemaining.Text = ""

        FMain.StopProcess = False
        FMain.ButtonCancel.Text = "Cancel"

        If Me.UtilsLogFile.ErrorsOccurred Then
            Process.Start("Notepad.exe", Me.UtilsLogFile.MissingFilesFileName)
        Else
            FMain.TextBoxStatus.Text = FMain.TextBoxStatus.Text + "  All checks passed."
        End If

        FMain.Cursor = Cursors.Default

    End Sub


    Public Function CheckStartConditions() As String
        Dim msg As String = ""
        Dim msg2 As String = ""
        Dim indent As String = "    "
        Dim SaveMsg As String = ""

        Dim USEA = New UtilsSEApp
        USEA.TextBoxStatus = Me.TextBoxStatus

        If Not FMain.UseCurrentSession Then
            If USEA.SEIsRunning() Then
                msg += "    Close Solid Edge" + Chr(13)
            End If
        End If

        If USEA.DMIsRunning() Then
            msg += "    Close Design Manager" + Chr(13)
        End If

        If FMain.ListViewFilesOutOfDate Then
            msg += "    Update the file list (Orange button toward the top of the Home Tab)" + Chr(13)
        End If

        If FMain.TLABottomUp Then
            If Not FileIO.FileSystem.FileExists(FMain.FastSearchScopeFilename) Then
                msg += "    Enter a valid Fast Search Scope file (on the Configuration Tab - Top Level Assembly Page)" + Chr(13)
            End If
        End If

        For Each Filename As ListViewItem In FMain.ListViewFiles.Items 'L-istBoxFiles.Items

            FMain.ListViewFiles.BeginUpdate()

            If Filename.Group.Name <> "Sources" Then

                Filename.ImageKey = "Unchecked"

                If Not FileIO.FileSystem.FileExists(Filename.Name) Then
                    msg += "    File not found, or Path exceeds maximum length" + Chr(13)
                    msg += "    " + CType(Filename.Name, String) + Chr(13)
                    FMain.ListViewFilesOutOfDate = True
                    Exit For
                End If

            End If

            FMain.ListViewFiles.EndUpdate()

        Next

        If FMain.ListViewFilesOutOfDate Then
            'msg += "    Update the file list, or otherwise correct the issue" + Chr(13)
        ElseIf FMain.ListViewFiles.Items.Count = 0 Then
            msg += "    Select an input directory with files to process" + Chr(13)
        End If

        If FMain.EnableFileWildcard Then
            If FMain.FileWildcard = "" Then
                msg += "    Enter a file wildcard search string" + Chr(13)
            End If
        End If

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        ErrorMessage(0) = New List(Of String)
        Dim ExitStatus As Integer = 0
        'Dim NoTaskSelected As Boolean = True

        FMain.SolidEdgeRequired = 0
        Dim SelectedTasksCount As Integer = 0

        For Each Task As Task In FMain.TaskList
            If Task.IsSelectedTask Then
                SelectedTasksCount += 1

                If Task.RequiresSourceDirectories Then
                    Dim UFL As New UtilsFileList(FMain, FMain.ListViewFiles, FMain.ListViewSources)
                    Task.SourceDirectories = UFL.GetSourceDirectories()
                End If

                If Task.RequiresSave And FMain.WarnSave Then
                    SaveMsg = String.Format("{0}{1}{2}{3}", SaveMsg, indent, Task.Description, vbCrLf)
                End If

                ' True returns -1 upon conversion
                FMain.SolidEdgeRequired -= CType(Task.SolidEdgeRequired, Integer)

                ErrorMessage = Task.CheckStartConditions(ErrorMessage)
            End If
        Next

        If FMain.SolidEdgeRequired <> 0 Then
            If SelectedTasksCount <> FMain.SolidEdgeRequired Then
                msg += String.Format("    Conflicts in Tasks Solid Edge required property{0}", vbCrLf)
                ExitStatus += 1
            End If
        End If


        If SelectedTasksCount = 0 Then
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

        If (Len(SaveMsg) <> 0) And FMain.WarnSave Then
            Dim s As String = "The following options require the original file to be saved." + Chr(13)
            s += "Please verify you have a backup before continuing."
            SaveMsg += Chr(13) + "Disable this warning on the Configuration Tab -- General Page."
            SaveMsg = s + Chr(13) + SaveMsg + Chr(13) + Chr(13)
        Else
            SaveMsg = ""
        End If

        Return SaveMsg + msg
    End Function

    Public Sub UpdateTimeRemaining()
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
                ElapsedTimeString = String.Format("{0} m", ElapsedTime.ToString("0.0"))
            Else
                ElapsedTimeString = String.Format("{0} h", (ElapsedTime / 60).ToString("0.0"))
            End If

            If RemainingTime < 60 Then
                RemainingTimeString = String.Format("{0} m", RemainingTime.ToString("0.0"))
            Else
                RemainingTimeString = String.Format("{0} h", (RemainingTime / 60).ToString("0.0"))
            End If

            If RemainingTime < 0.1 Then
                FMain.LabelTimeRemaining.Text = ""
            Else
                FMain.LabelTimeRemaining.Text = String.Format("Elapsed {0}, Remaining {1}", ElapsedTimeString, RemainingTimeString)
            End If


        End If
    End Sub

    Public Sub ProcessFiles(ByVal Filetype As String)

        Dim FilesToProcess As List(Of String)
        Dim FileToProcess As String
        Dim msg As String
        Dim ErrorMessagesCombined As New Dictionary(Of String, List(Of String))


        Dim UFL As New UtilsFileList(FMain, FMain.ListViewFiles, FMain.ListViewSources)

        If Filetype = "Assembly" Then
            FilesToProcess = UFL.GetFileNames("*.asm")
        ElseIf Filetype = "Part" Then
            FilesToProcess = UFL.GetFileNames("*.par")
        ElseIf Filetype = "Sheetmetal" Then
            FilesToProcess = UFL.GetFileNames("*.psm")
        ElseIf Filetype = "Draft" Then
            FilesToProcess = UFL.GetFileNames("*.dft")
        Else
            MsgBox("In ProcessFiles(), Filetype not recognized: " + Filetype + ".  Exiting...")
            SEApp.Quit()
            End
        End If

        Dim DMApp As DesignManager.Application = Nothing
        ' This should be faster: Dim SEFPPropertySets as SolidEdgeFileProperties.PropertySets = Nothing
        If (FMain.ProcessAsAvailable) And (FMain.UseDMForStatusChanges) And (FilesToProcess.Count > 0) Then
            DMApp = New DesignManager.Application
            DMApp.Visible = 1
            SEApp.Activate()
        End If

        For Each FileToProcess In FilesToProcess

            For Each tmpItem As ListViewItem In FMain.ListViewFiles.Items
                If tmpItem.Name = FileToProcess Then
                    tmpItem.EnsureVisible()
                    Exit For
                End If
            Next

            System.Windows.Forms.Application.DoEvents()
            If FMain.StopProcess Then
                FMain.TextBoxStatus.Text = "Processing aborted"
                If (FMain.ProcessAsAvailable) And (FMain.UseDMForStatusChanges) Then
                    DMApp.Quit()
                End If
                Exit Sub
            End If

            FilesToProcessCompleted += 1

            msg = FilesToProcessCompleted.ToString + "/" + FilesToProcessTotal.ToString + " "
            msg += System.IO.Path.GetFileName(FileToProcess)
            FMain.TextBoxStatus.Text = msg

            ErrorMessagesCombined = ProcessFile(FileToProcess, Filetype, DMApp)

            If ErrorMessagesCombined.Count > 0 Then
                Dim tmpPath As String = System.IO.Path.GetDirectoryName(FileToProcess)
                Dim tmpFilename As String = System.IO.Path.GetFileName(FileToProcess)
                Dim s As String = String.Format("{0} in {1}", tmpFilename, tmpPath)

                Me.UtilsLogFile.LogfileAppend(s, ErrorMessagesCombined)
                FMain.ListViewFiles.Items.Item(FileToProcess).ImageKey = "Error"
            Else
                FMain.ListViewFiles.Items.Item(FileToProcess).ImageKey = "Checked"
            End If

        Next

        If (FMain.ProcessAsAvailable) And (FMain.UseDMForStatusChanges) And (DMApp IsNot Nothing) Then
            DMApp.Quit()
        End If

    End Sub

    Public Function ProcessFile(
        ByVal Path As String,
        ByVal Filetype As String,
        DMApp As DesignManager.Application
        ) As Dictionary(Of String, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim ExitStatus As Integer
        Dim ErrorMessagesCombined As New Dictionary(Of String, List(Of String))

        Dim SEDoc As SolidEdgeFramework.SolidEdgeDocument = Nothing

        Dim ActiveWindow As SolidEdgeFramework.Window
        Dim ActiveSheetWindow As SolidEdgeDraft.SheetWindow

        Dim UC As New UtilsCommon

        Dim tf As Boolean

        Dim Proceed As Boolean = True

        Dim OldStatus As SolidEdgeConstants.DocumentStatus


        '###### EXCEPTION ACCOUNTING ######

        TotalAborts -= 0.1  ' Discounting for infrequent malfunctions on a large number of files.
        If TotalAborts < 0 Then
            TotalAborts = 0
        End If


        ' ###### DOCUMENT STATUS ######

        If FMain.ProcessAsAvailable And FMain.SolidEdgeRequired > 0 Then

            If FMain.UseDMForStatusChanges Then
                OldStatus = UC.GetDMStatus(DMApp, Path)
            Else
                OldStatus = UC.GetOLEStatus(Path)
            End If

            Dim s As String = SetStartingStatus(OldStatus, DMApp, Path)
            If Not s = "" Then
                ErrorMessagesCombined(s) = New List(Of String) From {""}
            End If

        End If


        '###### PROCESS ######

        Try


            '###### OPEN FILE ######

            If FMain.SolidEdgeRequired > 0 Then

                Proceed = CheckVersion(SEApp, Path)
                If Not Proceed Then
                    ErrorMessagesCombined("Error opening file") = New List(Of String) From {""}
                End If

                If Proceed Then
                    If (FMain.RunInBackground) And (Not Filetype = "Assembly") Then
                        SEDoc = SolidEdgeCommunity.Extensions.DocumentsExtensions.OpenInBackground(
                                    Of SolidEdgeFramework.SolidEdgeDocument)(SEApp.Documents, Path)

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

                End If
            End If


            '###### PERFORM TASKS ######

            If Proceed Then
                For Each Task As Task In FMain.TaskList
                    If Task.IsSelectedTask Then
                        tf = (Filetype = "Assembly") And (Task.IsSelectedAssembly)
                        tf = tf Or ((Filetype = "Part") And (Task.IsSelectedPart))
                        tf = tf Or ((Filetype = "Sheetmetal") And (Task.IsSelectedSheetmetal))
                        tf = tf Or ((Filetype = "Draft") And (Task.IsSelectedDraft))

                        If tf Then

                            If FMain.SolidEdgeRequired > 0 Then
                                ErrorMessage = Task.Process(SEDoc, FMain.Configuration, SEApp)
                            Else
                                ErrorMessage = Task.Process(Path)
                            End If

                            ExitStatus = ErrorMessage.Keys(0)

                            If ExitStatus <> 0 Then
                                ErrorMessagesCombined(Task.Description) = ErrorMessage(ErrorMessage.Keys(0))

                                If ExitStatus = 99 Then
                                    FMain.StopProcess = True
                                End If

                            End If
                        End If
                    End If
                Next

            End If


            '###### CLOSE FILE ######

            If FMain.SolidEdgeRequired > 0 Then
                If Proceed Then
                    SEDoc.Close(False)
                    SEApp.DoIdle()
                End If


                '###### DOCUMENT STATUS ######

                If FMain.ProcessAsAvailable Then
                    Dim s As String = SetEndingStatus(OldStatus, DMApp, Path)
                    If Not s = "" Then
                        ErrorMessagesCombined(s) = New List(Of String) From {""}
                    End If

                End If

            End If

        Catch ex As Exception


            '###### EXCEPTION PROCESSING ######

            Dim AbortList As New List(Of String)

            AbortList.Add(ex.ToString)

            TotalAborts += 1
            If TotalAborts >= TotalAbortsMaximum Then
                FMain.StopProcess = True
                AbortList.Add(String.Format("Total aborts exceed maximum of {0}.  Exiting...", TotalAbortsMaximum))
            Else
                If FMain.SolidEdgeRequired > 0 Then
                    Dim USEA = New UtilsSEApp
                    USEA.TextBoxStatus = Me.TextBoxStatus

                    USEA.SEStop(FMain.UseCurrentSession)
                    SEApp = Nothing

                    USEA.SEStart(FMain.RunInBackground, FMain.UseCurrentSession, FMain.NoUpdateMRU)
                    SEApp = USEA.SEApp
                End If
            End If

            If ex.ToString.Contains("SolidEdgeFramework.Documents.Open") Then
                ErrorMessagesCombined("Error opening file") = New List(Of String) From {""}
            Else
                ErrorMessagesCombined("Error processing file") = AbortList
            End If
        End Try

        UpdateTimeRemaining()

        Return ErrorMessagesCombined
    End Function

    Private Function CheckVersion(SEApp As SolidEdgeFramework.Application, Filename As String) As Boolean
        ' Checks if the file to be processed is of the same or earlier version than the version installed on the machine.
        ' On at lease some corrupted files, generates an exception which is interpreted as not possible to open

        Dim IsOK As Boolean = True

        Dim Version = SEApp.Version
        Dim InstalledMajorVersion As Integer = CInt(Version.Split(CChar("."))(0))

        Dim DType As SolidEdgeFramework.DocumentTypeConstants = Nothing
        Dim CreatedVersion As String = ""
        Dim LastSavedVersion As String = ""
        Dim GeometricVersion As UInteger = 0
        Try
            SEApp.SEGetFileVersionInfo(Filename, DType, CreatedVersion, LastSavedVersion, GeometricVersion)
        Catch ex As Exception
            IsOK = False  'Corrupted file or other issue.
        End Try

        If IsOK Then
            Dim LastSavedMajorVersion As Integer = CInt(LastSavedVersion.Split(CChar("."))(0))
            If LastSavedMajorVersion > InstalledMajorVersion Then
                IsOK = False
            End If
        End If

        Return IsOK
    End Function

    Private Function SetStartingStatus(
        OldStatus As SolidEdgeConstants.DocumentStatus,
        DMApp As DesignManager.Application,
        Path As String) As String

        Dim ErrorMessage As String = ""
        Dim StatusChangeSuccessful As Boolean = True

        Dim UC As New UtilsCommon

        If FMain.UseDMForStatusChanges Then

            If Not OldStatus = SolidEdgeConstants.DocumentStatus.igStatusAvailable Then
                StatusChangeSuccessful = UC.SetDMStatus(DMApp, Path, SolidEdgeConstants.DocumentStatus.igStatusAvailable)
            End If

            SEApp.DoIdle()

        Else

            If Not OldStatus = SolidEdgeConstants.DocumentStatus.igStatusAvailable Then
                StatusChangeSuccessful = UC.SetOLEStatus(Path, SolidEdgeConstants.DocumentStatus.igStatusAvailable)
            End If

        End If

        If Not StatusChangeSuccessful Then
            ErrorMessage = "Change status to 'Available' did not succeed"
        End If

        Return ErrorMessage
    End Function

    Private Function SetEndingStatus(
        OldStatus As SolidEdgeConstants.DocumentStatus,
        DMApp As DesignManager.Application,
        Path As String) As String

        Dim ErrorMessage As String = ""
        Dim StatusChangeSuccessful As Boolean = True

        Dim UC As New UtilsCommon

        If FMain.ProcessAsAvailableRevert Then
            If Not OldStatus = SolidEdgeConstants.DocumentStatus.igStatusAvailable Then
                If FMain.UseDMForStatusChanges Then
                    StatusChangeSuccessful = UC.SetDMStatus(DMApp, Path, OldStatus)
                Else
                    StatusChangeSuccessful = UC.SetOLEStatus(Path, OldStatus)
                End If
            End If
        End If

        If FMain.ProcessAsAvailableChange Then
            Dim NewStatus As SolidEdgeConstants.DocumentStatus = GetNewStatus(OldStatus)

            If Not OldStatus = NewStatus Then
                If FMain.UseDMForStatusChanges Then
                    StatusChangeSuccessful = UC.SetDMStatus(DMApp, Path, NewStatus)
                Else
                    StatusChangeSuccessful = UC.SetOLEStatus(Path, NewStatus)
                End If
            End If

        End If

        If Not StatusChangeSuccessful Then
            ErrorMessage = String.Format("Change status to '{0}' did not succeed", OldStatus.ToString)
        End If

        Return ErrorMessage

    End Function

    Private Function GetNewStatus(OldStatus As SolidEdgeConstants.DocumentStatus) As SolidEdgeConstants.DocumentStatus
        Dim NewStatus As SolidEdgeConstants.DocumentStatus

        If OldStatus = SolidEdgeConstants.DocumentStatus.igStatusAvailable Then
            NewStatus = CType(FMain.StatusAtoX, SolidEdgeConstants.DocumentStatus)
        End If
        If OldStatus = SolidEdgeConstants.DocumentStatus.igStatusBaselined Then
            NewStatus = CType(FMain.StatusBtoX, SolidEdgeConstants.DocumentStatus)
        End If
        If OldStatus = SolidEdgeConstants.DocumentStatus.igStatusInReview Then
            NewStatus = CType(FMain.StatusIRtoX, SolidEdgeConstants.DocumentStatus)
        End If
        If OldStatus = SolidEdgeConstants.DocumentStatus.igStatusInWork Then
            NewStatus = CType(FMain.StatusIWtoX, SolidEdgeConstants.DocumentStatus)
        End If
        If OldStatus = SolidEdgeConstants.DocumentStatus.igStatusObsolete Then
            NewStatus = CType(FMain.StatusOtoX, SolidEdgeConstants.DocumentStatus)
        End If
        If OldStatus = SolidEdgeConstants.DocumentStatus.igStatusReleased Then
            NewStatus = CType(FMain.StatusRtoX, SolidEdgeConstants.DocumentStatus)
        End If

        Return NewStatus
    End Function

End Class
