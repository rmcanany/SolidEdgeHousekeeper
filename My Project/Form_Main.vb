Option Strict On

Imports System.Runtime.InteropServices
Imports Microsoft.WindowsAPICodePack.Dialogs

Public Class Form_Main

    Public Property Version As String = "2024.2"
    Public Property CheckForNewerVersion As Boolean

    Public Property UtilsLogFile As UtilsLogFile

    Public Property StopProcess As Boolean

    Public DragDropCache As New List(Of ListViewItem)
    Public DragDropCacheExcluded As New List(Of ListViewItem)

    Private ListItems_TextFiltered As New List(Of ListViewItem)
    Private ListItems_PropFiltered As New List(Of ListViewItem)

    Public ListViewFilesOutOfDate As Boolean

    Public Property Configuration As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Public Property PropertyFilterDict As Dictionary(Of String, Dictionary(Of String, String))

    Public Property TaskList As List(Of Task)
    Public Property RememberTaskSelections As Boolean

    Public Property SolidEdgeRequired As Integer

    Public Property UseCurrentSession As Boolean = False
    Public Property NoUpdateMRU As Boolean
    Public Property RunInBackground As Boolean

    Public Property AssemblyTemplate As String
    Public Property PartTemplate As String
    Public Property SheetmetalTemplate As String
    Public Property DraftTemplate As String
    Public Property MaterialTable As String

    Public Property UseTemplateProperties As Boolean
    Public Property TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property TemplatePropertyList As List(Of String)




    'DESCRIPTION
    'Solid Edge Housekeeper
    'Robert McAnany 2020-2024
    '
    'This section is about the code.  To read how to use it, see the Readme on GitHub.
    '
    'The program performs various tasks on batches of files.  Each task is housed in
    'its own class, inherited from the superclass, Task.vb.
    '
    'Creating a new Task
    '-- It's probably easiest to copy an existing task to use as a template.
    '-- Choose a category from [Update, Edit, Restyle, Check, Output], or make a new one.
    '-- Document the task in GetHelpText().  The GitHub Readme is auto-populated with this info.
    '-- Add the task to the list in PreferencesUtilities.BuildTaskListFromScratch().
    '    -- The tasks are presented in the UI in the same order as the list.
    '    -- Place yours in the the appropriate category.
    '    -- For a new category, also update Task.SetColorFromCategory().


    Private Sub Startup()

        Dim UP As New UtilsPreferences()
        Dim UD As New UtilsDocumentation
        Dim UDefaults As New UtilsDefaults(Me)

        UP.CreatePreferencesDirectory()
        UP.CreateFilenameCharmap()
        UP.CreateSavedExpressions()
        UP.CreateInteractiveEditCommands()

        PopulateCheckedListBoxes()
        UDefaults.LoadDefaults()

        ReconcileFormChanges()
        UD.BuildReadmeFile()

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

        ' Form title
        Me.Text = String.Format("Solid Edge Housekeeper {0}", Me.Version)

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

        Me.TaskList = UP.GetTaskList

        Dim tmpTaskPanel As Panel = Nothing

        For Each c As Control In TabPageTasks.Controls
            If c.Name = "TaskPanel" Then
                tmpTaskPanel = CType(c, Panel)
                Exit For
            End If
        Next

        For i = TaskList.Count - 1 To 0 Step -1
            Dim Task = TaskList(i)
            If Not Me.RememberTaskSelections Then
                Task.IsSelectedTask = False
                Task.IsSelectedAssembly = False
                Task.IsSelectedPart = False
                Task.IsSelectedSheetmetal = False
                Task.IsSelectedDraft = False
            End If
            tmpTaskPanel.Controls.Add(Task.TaskControl)
        Next

        Me.TemplatePropertyDict = UP.GetTemplatePropertyDict()
        Me.TemplatePropertyList = UP.GetTemplatePropertyList

        Me.PropertyFilterDict = UP.GetPropertyFilterDict

        'UP.CheckForNewerVersion(Me.Version)
        If Me.CheckForNewerVersion Then
            UP.CheckForNewerVersion(Me.Version)
        End If

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

    Public Sub ReconcileFormChanges(Optional UpdateFileList As Boolean = False)

        Dim UD As New UtilsDefaults(Me)

        ' Update configuration
        Configuration = UD.GetConfiguration()

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

    'Public Function GetControlDict(Exclude As Boolean) As Dictionary(Of String, Control)
    '    Dim ControlDict As New Dictionary(Of String, Control)
    '    ControlDict = RecurseFormControls(Me, ControlDict, Exclude)
    '    Return ControlDict
    'End Function

    'Public Function RecurseFormControls(Ctrl As Control,
    '                                     ControlDict As Dictionary(Of String, Control),
    '                                     Exclude As Boolean
    '                                     ) As Dictionary(Of String, Control)

    '    Dim ChildControl As Control
    '    Dim tf As Boolean
    '    Dim ExcludeControls As New List(Of String)

    '    ExcludeControls.Add(Me.new_CheckBoxEnablePropertyFilter.Name)
    '    ' ExcludeControls.Add(ListViewFiles.Name)

    '    tf = TypeOf Ctrl Is ContainerControl
    '    tf = tf Or TypeOf Ctrl Is TabControl
    '    tf = tf Or TypeOf Ctrl Is TabPage
    '    tf = tf Or TypeOf Ctrl Is GroupBox
    '    tf = tf Or TypeOf Ctrl Is ExTableLayoutPanel
    '    'tf = Ctrl.HasChildren

    '    If tf Then
    '        For Each ChildControl In Ctrl.Controls
    '            ControlDict = RecurseFormControls(ChildControl, ControlDict, Exclude)
    '        Next
    '    Else
    '        tf = TypeOf Ctrl Is Button  ' Don't need to save buttons or labels.
    '        tf = tf Or TypeOf Ctrl Is Label
    '        If Exclude Then
    '            tf = tf Or ExcludeControls.Contains(Ctrl.Name)
    '        End If

    '        If Not tf Then
    '            ControlDict.Add(Ctrl.Name, Ctrl)
    '        End If
    '    End If

    '    Return ControlDict
    'End Function


    ' **************** CONTROLS ****************
    ' BUTTONS


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        If ButtonCancel.Text = "Stop" Then
            StopProcess = True
        Else
            Dim UD As New UtilsDefaults(Me)

            ReconcileFormChanges()
            UD.SaveDefaults()

            Dim UP As New UtilsPreferences
            UP.SaveTaskList(Me.TaskList)
            UP.SaveTemplatePropertyDict(Me.TemplatePropertyDict)
            UP.SaveTemplatePropertyList(Me.TemplatePropertyList)
            End
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As EventArgs) Handles Me.FormClosing

        Dim UD As New UtilsDefaults(Me)

        ReconcileFormChanges()
        UD.SaveDefaults()

        Dim UP As New UtilsPreferences
        UP.SaveTaskList(Me.TaskList)
        UP.SaveTemplatePropertyDict(Me.TemplatePropertyDict)
        UP.SaveTemplatePropertyList(Me.TemplatePropertyList)
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

        'FormPropertyFilter.SetReadmeFontsize(CInt(TextBoxFontSize.Text))
        Dim FPF As New FormPropertyFilter
        FPF.PropertyFilterDict = Me.PropertyFilterDict
        FPF.ShowDialog()

        If FPF.DialogResult = DialogResult.OK Then
            Me.PropertyFilterDict = FPF.PropertyFilterDict

            ListViewFilesOutOfDate = True
            BT_Update.BackColor = Color.Orange
        End If

    End Sub

    Private Sub ButtonProcess_Click(sender As Object, e As EventArgs) Handles ButtonProcess.Click
        Dim UE As New UtilsExecute(Me)
        UE.TextBoxStatus = Me.TextBoxStatus

        UE.ProcessAll()
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

            Dim FPF As New FormPropertyFilter
            FPF.PropertyFilterDict = Me.PropertyFilterDict
            FPF.ShowDialog()

            If FPF.DialogResult = DialogResult.OK Then
                Me.PropertyFilterDict = FPF.PropertyFilterDict

                ListViewFilesOutOfDate = True
                BT_Update.BackColor = Color.Orange
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

    Private Sub LinkLabelGitHubReadme_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
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
            CheckBoxTLAIgnoreIncludeInReports.Enabled = True
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
            CheckBoxTLAIgnoreIncludeInReports.Enabled = False
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

        Dim UFL As New UtilsFileList(Me, ListViewFiles)
        UFL.New_UpdateFileList()

    End Sub

    'Private Sub New_UpdateFileList()

    '    Me.Cursor = Cursors.WaitCursor

    '    Dim GroupTags As New List(Of String)
    '    Dim BareTopLevelAssembly As Boolean = False
    '    Dim msg As String

    '    Dim ElapsedTime As Double
    '    Dim ElapsedTimeText As String

    '    StartTime = Now


    '    TextBoxStatus.Text = "Updating list..."
    '    LabelTimeRemaining.Text = ""
    '    System.Windows.Forms.Application.DoEvents()

    '    ListViewFiles.BeginUpdate()

    '    ' Remove everything except the "Sources" group.
    '    For i = ListViewFiles.Items.Count - 1 To 0 Step -1
    '        If ListViewFiles.Items.Item(i).Group.Name <> "Sources" Then
    '            ListViewFiles.Items.Item(i).Remove()
    '        Else
    '            GroupTags.Add(CType(ListViewFiles.Items.Item(i).Tag, String))
    '        End If
    '    Next

    '    If (GroupTags.Contains("ASM_Folder")) And Not (GroupTags.Contains("asm")) Then
    '        msg = "A top level assembly folder was found with no top level assembly.  "
    '        msg += "Please add an assembly, or delete the folder(s)."
    '        ListViewFiles.EndUpdate()
    '        Me.Cursor = Cursors.Default
    '        TextBoxStatus.Text = ""
    '        MsgBox(msg, vbOKOnly)
    '        Exit Sub
    '    End If

    '    If (RadioButtonTLABottomUp.Checked) And (Not FileIO.FileSystem.FileExists(TextBoxFastSearchScopeFilename.Text)) Then
    '        msg = "Fast search scope file (on Configuration Tab) not found" + Chr(13)
    '        ListViewFiles.EndUpdate()
    '        Me.Cursor = Cursors.Default
    '        TextBoxStatus.Text = ""
    '        MsgBox(msg, vbOKOnly)
    '        Exit Sub
    '    End If

    '    If (GroupTags.Contains("asm")) And Not (GroupTags.Contains("ASM_Folder")) Then

    '        'If CheckBoxWarnBareTLA.Enabled And CheckBoxWarnBareTLA.Checked Then
    '        If CheckBoxWarnBareTLA.Checked Then
    '            msg = "A top-level assembly with no top-level folder detected.  "
    '            msg += "No 'Where Used' will be performed." + vbCrLf + vbCrLf
    '            msg += "Click OK to continue, or Cancel to stop." + vbCrLf
    '            msg += "Disable this message on the Configuration tab."
    '            Dim result As MsgBoxResult = MsgBox(msg, vbOKCancel)
    '            If result = MsgBoxResult.Ok Then
    '                BareTopLevelAssembly = True
    '            Else
    '                ListViewFiles.EndUpdate()
    '                Me.Cursor = Cursors.Default
    '                TextBoxStatus.Text = ""
    '                Exit Sub
    '            End If
    '        Else
    '            BareTopLevelAssembly = True
    '        End If
    '    End If

    '    Dim UFL As New UtilsFileList(Me, ListViewFiles)

    '    ' Only remaining items should be in the "Sources" group.
    '    For Each item As ListViewItem In ListViewFiles.Items
    '        UFL.UpdateListViewFiles(item, BareTopLevelAssembly)
    '    Next

    '    'DragDropCache.Clear()
    '    'For Each item As ListViewItem In ListViewFiles.Items
    '    '    DragDropCache.Add(item)
    '    'Next

    '    ListViewFiles.EndUpdate()

    '    Me.Cursor = Cursors.Default
    '    'If TextBoxStatus.Text = "Updating list..." Then
    '    '    TextBoxStatus.Text = "No files found"
    '    'End If

    '    ElapsedTime = Now.Subtract(StartTime).TotalMinutes
    '    If ElapsedTime < 60 Then
    '        ElapsedTimeText = "in " + ElapsedTime.ToString("0.0") + " min."
    '    Else
    '        ElapsedTimeText = "in " + (ElapsedTime / 60).ToString("0.0") + " hr."
    '    End If


    '    Dim filecount As Integer = ListViewFiles.Items.Count - ListViewFiles.Groups.Item("Sources").Items.Count
    '    TextBoxStatus.Text = String.Format("{0} files found in {1}", filecount, ElapsedTimeText)


    'End Sub

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

        Dim UC As New UtilsCommon

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

                If UC.FilenameIsOK(FileName) Then

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
        Dim UE As New UtilsExecute(Me)
        UE.TextBoxStatus = Me.TextBoxStatus

        If Not ListViewFiles.SelectedItems.Count = 0 Then UE.ProcessAll()

    End Sub

    Private Sub BT_FindLinks_Click(sender As Object, e As EventArgs) Handles BT_FindLinks.Click

        ' A user reported he was confused by this command.  In particular that it didn't
        ' find Draft files or use his Property Filter settings.
        ' Disabling for now.

        MsgBox("This command is temporarily disabled.  Please use 'Update' instead.")
        Exit Sub


        Dim DMApp As New DesignManager.Application
        Dim DMDoc As DesignManager.Document

        Dim UC As New UtilsCommon

        For Each item As ListViewItem In ListViewFiles.SelectedItems

            DMDoc = CType(DMApp.Open(item.Name), DesignManager.Document)

            UC.tmpList = New Collection
            UC.FindLinked(DMDoc)

            For Each FoundFile In UC.tmpList
                If UC.FilenameIsOK(FoundFile.ToString) Then

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

        UC.tmpList = Nothing

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

    Public Function GetStatusChangeRadioButtons(Optional CheckedOnly As Boolean = False) As List(Of RadioButton)
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

    Private Sub LabelRandomSampleFraction_Click(sender As Object, e As EventArgs) Handles LabelRandomSampleFraction.Click

    End Sub

    Private Sub TaskHeaderEnableButton_Click(sender As Object, e As EventArgs) Handles TaskHeaderEnableButton.Click
        For Each Task As Task In Me.TaskList
            Task.TaskControl.CBEnabled.Checked = False
        Next
    End Sub

    Private Sub TaskHeaderCollapseButton_Click(sender As Object, e As EventArgs) Handles TaskHeaderCollapseButton.Click
        For Each Task As Task In Me.TaskList
            If Task.HasOptions Then
                Task.TaskControl.CBExpand.Checked = False
            End If
        Next
    End Sub

    Private Sub TaskHeaderToggleAssemblyButton_Click(sender As Object, e As EventArgs) Handles TaskHeaderToggleAssemblyButton.Click

        ' If all already checked, uncheck all.  Otherwise check all.

        Dim AllChecked As Boolean = True

        For Each Task As Task In Me.TaskList
            If Task.IsSelectedTask Then
                If Task.AppliesToAssembly Then
                    If Not Task.IsSelectedAssembly Then
                        AllChecked = False
                        Exit For
                    End If
                End If
            End If
        Next

        For Each Task As Task In Me.TaskList
            If Task.IsSelectedTask Then
                If Task.AppliesToAssembly Then
                    Task.TaskControl.CBAssembly.Checked = Not AllChecked
                End If
            End If
        Next

    End Sub

    Private Sub TaskHeaderTogglePartButton_Click(sender As Object, e As EventArgs) Handles TaskHeaderTogglePartButton.Click
        ' If all already checked, uncheck all.  Otherwise check all.

        Dim AllChecked As Boolean = True

        For Each Task As Task In Me.TaskList
            If Task.IsSelectedTask Then
                If Task.AppliesToPart Then
                    If Not Task.IsSelectedPart Then
                        AllChecked = False
                        Exit For
                    End If
                End If
            End If
        Next

        For Each Task As Task In Me.TaskList
            If Task.IsSelectedTask Then
                If Task.AppliesToPart Then
                    Task.TaskControl.CBPart.Checked = Not AllChecked
                End If
            End If
        Next

    End Sub

    Private Sub TaskHeaderToggleSheetmetalButton_Click(sender As Object, e As EventArgs) Handles TaskHeaderToggleSheetmetalButton.Click
        ' If all already checked, uncheck all.  Otherwise check all.

        Dim AllChecked As Boolean = True

        For Each Task As Task In Me.TaskList
            If Task.IsSelectedTask Then
                If Task.AppliesToSheetmetal Then
                    If Not Task.IsSelectedSheetmetal Then
                        AllChecked = False
                        Exit For
                    End If
                End If
            End If
        Next

        For Each Task As Task In Me.TaskList
            If Task.IsSelectedTask Then
                If Task.AppliesToSheetmetal Then
                    Task.TaskControl.CBSheetmetal.Checked = Not AllChecked
                End If
            End If
        Next

    End Sub

    Private Sub TaskHeaderToggleDraftButton_Click(sender As Object, e As EventArgs) Handles TaskHeaderToggleDraftButton.Click
        ' If all already checked, uncheck all.  Otherwise check all.

        Dim AllChecked As Boolean = True

        For Each Task As Task In Me.TaskList
            If Task.IsSelectedTask Then
                If Task.AppliesToDraft Then
                    If Not Task.IsSelectedDraft Then
                        AllChecked = False
                        Exit For
                    End If
                End If
            End If
        Next

        For Each Task As Task In Me.TaskList
            If Task.IsSelectedTask Then
                If Task.AppliesToDraft Then
                    Task.TaskControl.CBDraft.Checked = Not AllChecked
                End If
            End If
        Next

    End Sub

    Private Sub EditTaskListButton_Click(sender As Object, e As EventArgs) Handles EditTaskListButton.Click
        'MsgBox("Not currently implemented", vbOKOnly)
        Dim ETL As New FormEditTaskList()
        ETL.RememberTaskSelections = Me.RememberTaskSelections
        ETL.OldTaskList = Me.TaskList

        Dim DialogResult As DialogResult
        Dim tmpTasks As New List(Of Task)

        DialogResult = ETL.ShowDialog()

        If DialogResult = DialogResult.OK Then
            Me.TaskList = ETL.TaskList

            'Dim s As String = ""

            's = "Restart Housekeeper for your changes to take effect."
            's = String.Format("{0}{1}", s, vbCrLf)
            's = String.Format("{0}{1}{2}", s, "Once restarted, any new Tasks can be configured as needed.", vbCrLf)

            ''For Each Task As Task In Me.TaskList
            ''    s = String.Format("{0}{1}{2}", s, Task.Description, vbCrLf)
            ''Next
            'MsgBox(s, vbOKOnly)

            Dim tmpTaskPanel As Panel = Nothing

            For Each c As Control In TabPageTasks.Controls
                If c.Name = "TaskPanel" Then
                    tmpTaskPanel = CType(c, Panel)
                    Exit For
                End If
            Next

            tmpTaskPanel.Controls.Clear()

            For i = TaskList.Count - 1 To 0 Step -1
                Dim Task = TaskList(i)
                tmpTaskPanel.Controls.Add(Task.TaskControl)
            Next


        End If

    End Sub

    Private Sub TaskHeaderHelpButton_Click(sender As Object, e As EventArgs) Handles TaskHeaderHelpButton.Click

        Dim Tag As String = "task-tab"

        Dim UD As New UtilsDocumentation

        Dim HelpURL = UD.GenerateVersionURL(Tag)

        System.Diagnostics.Process.Start(HelpURL)

    End Sub

    Private Sub TaskPanel_Scroll(sender As Object, e As ScrollEventArgs) Handles TaskPanel.Scroll
        ' https://stackoverflow.com/questions/32246132/winforms-layered-controls-with-background-images-cause-tearing-while-scrolling

        If e.Type = ScrollEventType.First Then
            LockWindowUpdate(Me.Handle)
        Else
            LockWindowUpdate(IntPtr.Zero)
            TaskPanel.Update()
        End If

    End Sub

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function LockWindowUpdate(ByVal hWnd As IntPtr) As Boolean
    End Function

    Private Sub CheckBoxUseCurrentSession_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxUseCurrentSession.CheckedChanged
        UseCurrentSession = CheckBoxUseCurrentSession.Checked
    End Sub

    Private Sub BT_Help_Click(sender As Object, e As EventArgs) Handles BT_Help.Click

        Dim Tag As String = "file-selection-and-filtering"

        Dim UD As New UtilsDocumentation

        Dim HelpURL = UD.GenerateVersionURL(Tag)

        System.Diagnostics.Process.Start(HelpURL)

    End Sub

    Private Sub ButtonAssemblyTemplate_Click(sender As Object, e As EventArgs) Handles ButtonAssemblyTemplate.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select an assembly template"
        tmpFileDialog.Filter = "Assembly Template|*.asm"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.AssemblyTemplate = tmpFileDialog.FileName
            TextBoxAssemblyTemplate.Text = Me.AssemblyTemplate
        End If

    End Sub

    Private Sub ButtonPartTemplate_Click(sender As Object, e As EventArgs) Handles ButtonPartTemplate.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a part template"
        tmpFileDialog.Filter = "Part Template|*.par"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.PartTemplate = tmpFileDialog.FileName
            TextBoxPartTemplate.Text = Me.PartTemplate
        End If

    End Sub

    Private Sub ButtonSheetmetalTemplate_Click(sender As Object, e As EventArgs) Handles ButtonSheetmetalTemplate.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a sheetmetal template"
        tmpFileDialog.Filter = "Sheetmetal Template|*.psm"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.SheetmetalTemplate = tmpFileDialog.FileName
            TextBoxSheetmetalTemplate.Text = Me.SheetmetalTemplate
        End If

    End Sub

    Private Sub ButtonDraftTemplate_Click(sender As Object, e As EventArgs) Handles ButtonDraftTemplate.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a draft template"
        tmpFileDialog.Filter = "Draft Template|*.dft"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.DraftTemplate = tmpFileDialog.FileName
            TextBoxDraftTemplate.Text = Me.DraftTemplate
        End If

    End Sub

    Private Sub ButtonMaterialTable_Click(sender As Object, e As EventArgs) Handles ButtonMaterialTable.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a material table"
        tmpFileDialog.Filter = "Material Table|*.mtl"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.MaterialTable = tmpFileDialog.FileName
            TextBoxMaterialTable.Text = Me.MaterialTable
        End If

    End Sub

    Private Sub CheckBoxUseTemplateProperties_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxUseTemplateProperties.CheckedChanged
        Dim CheckBox As CheckBox = CType(sender, CheckBox)
        Me.UseTemplateProperties = CheckBox.Checked

        ButtonUseTemplateProperties.Enabled = CheckBox.Checked
    End Sub

    Private Sub ButtonCopyToTasks_Click(sender As Object, e As EventArgs) Handles ButtonCopyToTasks.Click
        Dim s As String = ""
        For Each Task As Task In Me.TaskList

            Dim TaskType As Type = Task.GetType

            If Task.RequiresAssemblyTemplate Then
                If TypeOf Task Is TaskUpdateModelStylesFromTemplate Then
                    Dim T = CType(Task, TaskUpdateModelStylesFromTemplate)
                    T.AssemblyTemplate = Me.AssemblyTemplate
                Else
                    s = String.Format("{0}RequiresAssemblyTemplate {1}{2}", s, TaskType.ToString, vbCrLf)
                End If
            End If
            If Task.RequiresPartTemplate Then
                If TypeOf Task Is TaskUpdateModelStylesFromTemplate Then
                    Dim T = CType(Task, TaskUpdateModelStylesFromTemplate)
                    T.PartTemplate = Me.PartTemplate
                Else
                    s = String.Format("{0}RequiresPartTemplate {1}{2}", s, TaskType.ToString, vbCrLf)
                End If
            End If
            If Task.RequiresSheetmetalTemplate Then
                If TypeOf Task Is TaskUpdateModelStylesFromTemplate Then
                    Dim T = CType(Task, TaskUpdateModelStylesFromTemplate)
                    T.SheetmetalTemplate = Me.SheetmetalTemplate
                Else
                    s = String.Format("{0}RequiresSheetmetalTemplate {1}{2}", s, TaskType.ToString, vbCrLf)
                End If
            End If
            If Task.RequiresDraftTemplate Then
                If TypeOf Task Is TaskUpdateDrawingStylesFromTemplate Then
                    Dim T = CType(Task, TaskUpdateDrawingStylesFromTemplate)
                    T.DraftTemplate = Me.DraftTemplate
                ElseIf TypeOf Task Is TaskCreateDrawingOfFlatPattern Then
                    Dim T = CType(Task, TaskCreateDrawingOfFlatPattern)
                    T.DraftTemplate = Me.DraftTemplate
                Else
                    s = String.Format("{0}RequiresDraftTemplate {1}{2}", s, TaskType.ToString, vbCrLf)
                End If
            End If
            If Task.RequiresMaterialTable Then
                If TypeOf Task Is TaskCheckMaterialNotInMaterialTable Then
                    Dim T = CType(Task, TaskCheckMaterialNotInMaterialTable)
                    T.MaterialTable = Me.MaterialTable
                ElseIf TypeOf Task Is TaskEditProperties Then
                    Dim T = CType(Task, TaskEditProperties)
                    T.MaterialTable = Me.MaterialTable
                ElseIf TypeOf Task Is TaskUpdateMaterialFromMaterialTable Then
                    Dim T = CType(Task, TaskUpdateMaterialFromMaterialTable)
                    T.MaterialTable = Me.MaterialTable
                Else
                    s = String.Format("{0}RequiresMaterial {1}{2}", s, TaskType.ToString, vbCrLf)
                End If
            End If
            'Task.ReconcileFormWithProps()
        Next
        If Not s = "" Then
            MsgBox(s)
        End If
    End Sub

    Private Sub ButtonUseTemplateProperties_Click(sender As Object, e As EventArgs) Handles ButtonUseTemplateProperties.Click
        '{
        '"Titolo":{
        '    "PropertySet":"SummaryInformation"
        '    "AsmPropItemNumber":"1"
        '    "ParPropItemNumber":"1"
        '    "PsmPropItemNumber":"1"
        '    "DftPropItemNumber":"1"
        '    "EnglishName":"Title"},
        '"Oggetto":{
        '    "PropertySet":"SummaryInformation"
        '    "AsmPropItemNumber":"2"
        '    "ParPropItemNumber":"2"
        '    "PsmPropItemNumber":"2"
        '    "DftPropItemNumber":"2"
        '    "EnglishName":"Subject"},
        ' ...
        '}

        Me.Cursor = Cursors.WaitCursor

        Me.TemplatePropertyDict = New Dictionary(Of String, Dictionary(Of String, String))

        Dim Templates As List(Of String) = {Me.AssemblyTemplate, Me.PartTemplate, Me.SheetmetalTemplate, Me.DraftTemplate}.ToList
        Dim TemplateDocTypes As List(Of String) = {"asm", "par", "psm", "dft"}.ToList

        Dim PropertySets As SolidEdgeFileProperties.PropertySets
        Dim PropertySet As SolidEdgeFileProperties.Properties
        Dim Prop As SolidEdgeFileProperties.Property
        PropertySets = New SolidEdgeFileProperties.PropertySets
        Dim PropertySetName As String
        Dim PropName As String
        Dim DocType As String

        Dim UC As New UtilsCommon

        Dim tf As Boolean

        Dim n As Integer = 0

        For Each Template As String In Templates
            tf = Not Template = ""
            tf = tf And FileIO.FileSystem.FileExists(Template)
            If tf Then
                DocType = TemplateDocTypes(n)
                PropertySets.Open(Template, True)
                For i = 0 To PropertySets.Count - 1
                    Try
                        PropertySet = CType(PropertySets.Item(i), SolidEdgeFileProperties.Properties)
                    Catch ex As Exception
                        Continue For
                    End Try
                    PropertySetName = PropertySet.Name
                    For j = 0 To PropertySet.Count - 1
                        Try
                            Prop = CType(PropertySet.Item(j), SolidEdgeFileProperties.Property)
                            PropName = Prop.Name
                            If Not TemplatePropertyDict.Keys.Contains(PropName) Then
                                TemplatePropertyDict(PropName) = New Dictionary(Of String, String)
                                TemplatePropertyDict(PropName)("PropertySet") = PropertySetName
                                TemplatePropertyDict(PropName)("AsmPropItemNumber") = ""
                                TemplatePropertyDict(PropName)("ParPropItemNumber") = ""
                                TemplatePropertyDict(PropName)("PsmPropItemNumber") = ""
                                TemplatePropertyDict(PropName)("DftPropItemNumber") = ""
                                Dim s As String = UC.PropLocalizedToEnglish(PropertySetName, j + 1, DocType)
                                If s = "" Then s = PropName
                                TemplatePropertyDict(PropName)("EnglishName") = s
                            End If

                            Select Case DocType
                                Case "asm"
                                    TemplatePropertyDict(PropName)("AsmPropItemNumber") = CStr(j + 1)
                                Case "par"
                                    TemplatePropertyDict(PropName)("ParPropItemNumber") = CStr(j + 1)
                                Case "psm"
                                    TemplatePropertyDict(PropName)("PsmPropItemNumber") = CStr(j + 1)
                                Case "dft"
                                    TemplatePropertyDict(PropName)("DftPropItemNumber") = CStr(j + 1)
                            End Select

                        Catch ex As Exception
                            MsgBox(DocType + " " + PropertySetName + " " + CStr(i) + " " + CStr(j) + Chr(13) + ex.ToString)
                        End Try
                    Next
                Next

                PropertySets.Close()

            End If
            n += 1
        Next

        ''Check consistency -- only works when running non-localized
        'For Each Key As String In TemplatePropertyDict.Keys
        '    If Not Key = TemplatePropertyDict(Key)("EnglishName") Then
        '        MsgBox(String.Format("Key '{0}' does not match EnglishName '{1}'", Key, TemplatePropertyDict(Key)("EnglishName")))
        '    End If
        'Next

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub TextBoxAssemblyTemplate_TextChanged(sender As Object, e As EventArgs) Handles TextBoxAssemblyTemplate.TextChanged
        Dim TextBox As TextBox = CType(sender, TextBox)
        Me.AssemblyTemplate = TextBox.Text
    End Sub

    Private Sub TextBoxPartTemplate_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPartTemplate.TextChanged
        Dim TextBox As TextBox = CType(sender, TextBox)
        Me.PartTemplate = TextBox.Text
    End Sub

    Private Sub TextBoxSheetmetalTemplate_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSheetmetalTemplate.TextChanged
        Dim TextBox As TextBox = CType(sender, TextBox)
        Me.SheetmetalTemplate = TextBox.Text
    End Sub

    Private Sub TextBoxDraftTemplate_TextChanged(sender As Object, e As EventArgs) Handles TextBoxDraftTemplate.TextChanged
        Dim TextBox As TextBox = CType(sender, TextBox)
        Me.DraftTemplate = TextBox.Text
    End Sub

    Private Sub TextBoxMaterialTable_TextChanged(sender As Object, e As EventArgs) Handles TextBoxMaterialTable.TextChanged
        Dim TextBox As TextBox = CType(sender, TextBox)
        Me.MaterialTable = TextBox.Text
    End Sub

    Private Sub ButtonCustomizeTemplatePropertyDict_Click(sender As Object, e As EventArgs) Handles ButtonCustomizeTemplatePropertyDict.Click

        If Me.TemplatePropertyList Is Nothing Then
            Me.TemplatePropertyList = New List(Of String)
        End If

        Dim FPLC As New FormPropertyListCustomize

        Dim Result As DialogResult = FPLC.ShowDialog()

        If Result = DialogResult.OK Then
            Me.TemplatePropertyList = FPLC.TemplatePropertyList
        End If

    End Sub

    Private Sub CheckBoxNoUpdateMRU_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxNoUpdateMRU.CheckedChanged
        Me.NoUpdateMRU = CheckBoxNoUpdateMRU.Checked
    End Sub

    Private Sub CheckBoxBackgroundProcessing_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxBackgroundProcessing.CheckedChanged
        Me.RunInBackground = CheckBoxBackgroundProcessing.Checked
    End Sub

    Private Sub CheckBoxCheckForNewerVersion_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCheckForNewerVersion.CheckedChanged
        Me.CheckForNewerVersion = CheckBoxCheckForNewerVersion.Checked
    End Sub

    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
        Dim Tag As String = "readme"

        Dim UD As New UtilsDocumentation

        Dim HelpURL = UD.GenerateVersionURL(Tag)

        System.Diagnostics.Process.Start(HelpURL)

    End Sub



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

    ' Dim DrawingFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, ".dft")

    ' System.Threading.Thread.Sleep(100)

    ' TypeName = Microsoft.VisualBasic.Information.TypeName(SEDoc)

    ' Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()

    ' Dim Defaults As String() = Nothing
    ' Defaults = IO.File.ReadAllLines(DefaultsFilename)

    ' Dim Defaults As New List(Of String)
    ' IO.File.WriteAllLines(DefaultsFilename, Defaults)

    ' Iterate through an Enum
    ' For Each PaperSizeConstant In System.Enum.GetValues(GetType(SolidEdgeDraft.PaperSizeConstants))

    'Me.Cursor = Cursors.WaitCursor
    'Me.Cursor = Cursors.Default

    'ActiveWindow.WindowState = 2  '0 normal, 1 minimized, 2 maximized


End Class


