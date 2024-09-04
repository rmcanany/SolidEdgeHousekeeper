Option Strict On

Imports System.Runtime.InteropServices
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports Newtonsoft.Json

Public Class Form_Main

    Public Property Version As String = "2024.2"

    Public Property UtilsLogFile As UtilsLogFile

    Public Property StopProcess As Boolean

    Public DragDropCache As New List(Of ListViewItem)
    Public DragDropCacheExcluded As New List(Of ListViewItem)

    Private ListItems_TextFiltered As New List(Of ListViewItem)
    Private ListItems_PropFiltered As New List(Of ListViewItem)

    Private _ListViewFilesOutOfDate As Boolean
    Public Property ListViewFilesOutOfDate As Boolean
        Get
            Return _ListViewFilesOutOfDate
        End Get
        Set(value As Boolean)
            _ListViewFilesOutOfDate = value
            If Me.TabControl1 IsNot Nothing Then
                If ListViewFilesOutOfDate Then
                    BT_Update.BackColor = Color.Orange
                Else
                    BT_Update.BackColor = Color.FromName("Control")
                End If
            End If
        End Set
    End Property


    Public Property Configuration As Dictionary(Of String, String) = New Dictionary(Of String, String)


    Private _PropertyFilterDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property PropertyFilterDict As Dictionary(Of String, Dictionary(Of String, String))
        Get
            Return _PropertyFilterDict
        End Get
        Set(value As Dictionary(Of String, Dictionary(Of String, String)))
            _PropertyFilterDict = value
            If Me.TabControl1 IsNot Nothing Then
                Dim s = JsonConvert.SerializeObject(Me.PropertyFilterDict)
                If Not Me.PropertyFilterDictJSON = s Then
                    Me.PropertyFilterDictJSON = s
                End If
            End If
        End Set
    End Property


    Private _PropertyFilterDictJSON As String
    Public Property PropertyFilterDictJSON As String
        Get
            Return _PropertyFilterDictJSON
        End Get
        Set(value As String)
            _PropertyFilterDictJSON = value
            If Me.TabControl1 IsNot Nothing Then
                If Not _PropertyFilterDictJSON = JsonConvert.SerializeObject(Me.PropertyFilterDict) Then
                    Me.PropertyFilterDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(_PropertyFilterDictJSON)
                End If
            End If

        End Set
    End Property



    Public Property TaskList As List(Of Task)

    ' ###### TOP LEVEL ASSEMBLY ######

    Private _TLAAutoIncludeTLF As Boolean
    Public Property TLAAutoIncludeTLF As Boolean
        Get
            Return _TLAAutoIncludeTLF
        End Get
        Set(value As Boolean)
            _TLAAutoIncludeTLF = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxTLAAutoIncludeTLF.Checked = value
            End If
        End Set
    End Property

    Private _WarnBareTLA As Boolean
    Public Property WarnBareTLA As Boolean
        Get
            Return _WarnBareTLA
        End Get
        Set(value As Boolean)
            _WarnBareTLA = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxWarnBareTLA.Checked = value
            End If
        End Set
    End Property

    Private _TLAIncludePartCopies As Boolean
    Public Property TLAIncludePartCopies As Boolean
        Get
            Return _TLAIncludePartCopies
        End Get
        Set(value As Boolean)
            _TLAIncludePartCopies = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxTLAIncludePartCopies.Checked = value
            End If
        End Set
    End Property

    Private _TLAReportUnrelatedFiles As Boolean
    Public Property TLAReportUnrelatedFiles As Boolean
        Get
            Return _TLAReportUnrelatedFiles
        End Get
        Set(value As Boolean)
            _TLAReportUnrelatedFiles = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxTLAReportUnrelatedFiles.Checked = value
            End If
        End Set
    End Property

    Private _TLATopDown As Boolean
    Public Property TLATopDown As Boolean
        Get
            Return _TLATopDown
        End Get
        Set(value As Boolean)
            _TLATopDown = value
            If Me.TabControl1 IsNot Nothing Then
                RadioButtonTLATopDown.Checked = value
            End If
        End Set
    End Property

    Private _TLABottomUp As Boolean
    Public Property TLABottomUp As Boolean
        Get
            Return _TLABottomUp
        End Get
        Set(value As Boolean)
            _TLABottomUp = value
            If Me.TabControl1 IsNot Nothing Then
                RadioButtonTLABottomUp.Checked = value
            End If
        End Set
    End Property

    Private _DraftAndModelSameName As Boolean
    Public Property DraftAndModelSameName As Boolean
        Get
            Return _DraftAndModelSameName
        End Get
        Set(value As Boolean)
            _DraftAndModelSameName = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxDraftAndModelSameName.Checked = value
            End If
        End Set
    End Property

    Private _FastSearchScopeFilename As String
    Public Property FastSearchScopeFilename As String
        Get
            Return _FastSearchScopeFilename
        End Get
        Set(value As String)
            _FastSearchScopeFilename = value
            If Me.TabControl1 IsNot Nothing Then
                TextBoxFastSearchScopeFilename.Text = value
            End If
        End Set
    End Property


    ' ###### DOCUMENT STATUS ######

    Private _ProcessAsAvailable As Boolean
    Public Property ProcessAsAvailable As Boolean
        Get
            Return _ProcessAsAvailable
        End Get
        Set(value As Boolean)
            _ProcessAsAvailable = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxProcessAsAvailable.Checked = value
            End If
        End Set
    End Property

    Private _ProcessAsAvailableRevert As Boolean
    Public Property ProcessAsAvailableRevert As Boolean
        Get
            Return _ProcessAsAvailableRevert
        End Get
        Set(value As Boolean)
            _ProcessAsAvailableRevert = value
            If Me.TabControl1 IsNot Nothing Then
                RadioButtonProcessAsAvailableRevert.Checked = value
            End If
        End Set
    End Property

    Private _ProcessAsAvailableChange As Boolean
    Public Property ProcessAsAvailableChange As Boolean
        Get
            Return _ProcessAsAvailableChange
        End Get
        Set(value As Boolean)
            _ProcessAsAvailableChange = value
            If Me.TabControl1 IsNot Nothing Then
                RadioButtonProcessAsAvailableChange.Checked = value
            End If
        End Set
    End Property

    Private _StatusAto As String
    Public Property StatusAto As String
        Get
            Return _StatusAto
        End Get
        Set(value As String)
            _StatusAto = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case "A"
                        RadioButtonStatusAtoA.Checked = True
                    Case "B"
                        RadioButtonStatusAtoB.Checked = True
                    Case "IR"
                        RadioButtonStatusAtoIR.Checked = True
                    Case "IW"
                        RadioButtonStatusAtoIW.Checked = True
                    Case "O"
                        RadioButtonStatusAtoO.Checked = True
                    Case "R"
                        RadioButtonStatusAtoR.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusBto As String
    Public Property StatusBto As String
        Get
            Return _StatusBto
        End Get
        Set(value As String)
            _StatusBto = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case "A"
                        RadioButtonStatusBtoA.Checked = True
                    Case "B"
                        RadioButtonStatusBtoB.Checked = True
                    Case "IR"
                        RadioButtonStatusBtoIR.Checked = True
                    Case "IW"
                        RadioButtonStatusBtoIW.Checked = True
                    Case "O"
                        RadioButtonStatusBtoO.Checked = True
                    Case "R"
                        RadioButtonStatusBtoR.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusIRto As String
    Public Property StatusIRto As String
        Get
            Return _StatusIRto
        End Get
        Set(value As String)
            _StatusIRto = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case "A"
                        RadioButtonStatusIRtoA.Checked = True
                    Case "B"
                        RadioButtonStatusIRtoB.Checked = True
                    Case "IR"
                        RadioButtonStatusIRtoIR.Checked = True
                    Case "IW"
                        RadioButtonStatusIRtoIW.Checked = True
                    Case "O"
                        RadioButtonStatusIRtoO.Checked = True
                    Case "R"
                        RadioButtonStatusIRtoR.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusIWto As String
    Public Property StatusIWto As String
        Get
            Return _StatusIWto
        End Get
        Set(value As String)
            _StatusIWto = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case "A"
                        RadioButtonStatusIWtoA.Checked = True
                    Case "B"
                        RadioButtonStatusIWtoB.Checked = True
                    Case "IR"
                        RadioButtonStatusIWtoIR.Checked = True
                    Case "IW"
                        RadioButtonStatusIWtoIW.Checked = True
                    Case "O"
                        RadioButtonStatusIWtoO.Checked = True
                    Case "R"
                        RadioButtonStatusIWtoR.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusOto As String
    Public Property StatusOto As String
        Get
            Return _StatusOto
        End Get
        Set(value As String)
            _StatusOto = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case "A"
                        RadioButtonStatusOtoA.Checked = True
                    Case "B"
                        RadioButtonStatusOtoB.Checked = True
                    Case "IR"
                        RadioButtonStatusOtoIR.Checked = True
                    Case "IW"
                        RadioButtonStatusOtoIW.Checked = True
                    Case "O"
                        RadioButtonStatusOtoO.Checked = True
                    Case "R"
                        RadioButtonStatusOtoR.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusRto As String
    Public Property StatusRto As String
        Get
            Return _StatusRto
        End Get
        Set(value As String)
            _StatusRto = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case "A"
                        RadioButtonStatusRtoA.Checked = True
                    Case "B"
                        RadioButtonStatusRtoB.Checked = True
                    Case "IR"
                        RadioButtonStatusRtoIR.Checked = True
                    Case "IW"
                        RadioButtonStatusRtoIW.Checked = True
                    Case "O"
                        RadioButtonStatusRtoO.Checked = True
                    Case "R"
                        RadioButtonStatusRtoR.Checked = True
                End Select
            End If
        End Set
    End Property


    ' ###### LIST SORT ######

    Private _SortNone As Boolean
    Public Property SortNone As Boolean
        Get
            Return _SortNone
        End Get
        Set(value As Boolean)
            _SortNone = value
            If Me.TabControl1 IsNot Nothing Then
                RadioButtonSortNone.Checked = value
            End If
        End Set
    End Property

    Private _SortAlphabetical As Boolean
    Public Property SortAlphabetical As Boolean
        Get
            Return _SortAlphabetical
        End Get
        Set(value As Boolean)
            _SortAlphabetical = value
            If Me.TabControl1 IsNot Nothing Then
                RadioButtonSortAlphabetical.Checked = value
            End If
        End Set
    End Property

    Private _SortDependency As Boolean
    Public Property SortDependency As Boolean
        Get
            Return _SortDependency
        End Get
        Set(value As Boolean)
            _SortDependency = value
            If Me.TabControl1 IsNot Nothing Then
                RadioButtonSortDependency.Checked = value
            End If
        End Set
    End Property

    Private _SortIncludeNoDependencies As Boolean
    Public Property SortIncludeNoDependencies As Boolean
        Get
            Return _SortIncludeNoDependencies
        End Get
        Set(value As Boolean)
            _SortIncludeNoDependencies = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxSortIncludeNoDependencies.Checked = value
            End If
        End Set
    End Property

    Private _SortRandomSample As Boolean
    Public Property SortRandomSample As Boolean
        Get
            Return _SortRandomSample
        End Get
        Set(value As Boolean)
            _SortRandomSample = value
            If Me.TabControl1 IsNot Nothing Then
                RadioButtonSortRandomSample.Checked = value
            End If
        End Set
    End Property

    Private _SortRandomSampleFraction As String
    Public Property SortRandomSampleFraction As String
        Get
            Return _SortRandomSampleFraction
        End Get
        Set(value As String)
            _SortRandomSampleFraction = value
            If Me.TabControl1 IsNot Nothing Then
                TextBoxSortRandomSampleFraction.Text = value
            End If
        End Set
    End Property


    ' ###### TEMPLATES ######

    Private _AssemblyTemplate As String
    Public Property AssemblyTemplate As String
        Get
            Return _AssemblyTemplate
        End Get
        Set(value As String)
            _AssemblyTemplate = value
            If Me.TabControl1 IsNot Nothing Then
                TextBoxAssemblyTemplate.Text = value
            End If
        End Set
    End Property

    Private _PartTemplate As String
    Public Property PartTemplate As String
        Get
            Return _PartTemplate
        End Get
        Set(value As String)
            _PartTemplate = value
            If Me.TabControl1 IsNot Nothing Then
                TextBoxPartTemplate.Text = value
            End If
        End Set
    End Property

    Private _SheetmetalTemplate As String
    Public Property SheetmetalTemplate As String
        Get
            Return _SheetmetalTemplate
        End Get
        Set(value As String)
            _SheetmetalTemplate = value
            If Me.TabControl1 IsNot Nothing Then
                TextBoxSheetmetalTemplate.Text = value
            End If
        End Set
    End Property

    Private _DraftTemplate As String
    Public Property DraftTemplate As String
        Get
            Return _DraftTemplate
        End Get
        Set(value As String)
            _DraftTemplate = value
            If Me.TabControl1 IsNot Nothing Then
                TextBoxDraftTemplate.Text = value
            End If
        End Set
    End Property

    Private _MaterialTable As String
    Public Property MaterialTable As String
        Get
            Return _MaterialTable
        End Get
        Set(value As String)
            _MaterialTable = value
            If Me.TabControl1 IsNot Nothing Then
                TextBoxMaterialTable.Text = value
            End If
        End Set
    End Property

    'Private _UseTemplateProperties As Boolean
    'Public Property UseTemplateProperties As Boolean
    '    Get
    '        Return _UseTemplateProperties
    '    End Get
    '    Set(value As Boolean)
    '        _UseTemplateProperties = value
    '        If Me.TabControl1 IsNot Nothing Then
    '            CheckBoxUseTemplateProperties.Checked = value
    '        End If
    '    End Set
    'End Property


    Private _TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
        Get
            Return _TemplatePropertyDict
        End Get
        Set(value As Dictionary(Of String, Dictionary(Of String, String)))
            _TemplatePropertyDict = value
            If Me.TabControl1 IsNot Nothing Then
                Dim s = JsonConvert.SerializeObject(Me.TemplatePropertyDict)
                If Not Me.TemplatePropertyDictJSON = s Then
                    Me.TemplatePropertyDictJSON = s
                End If
            End If
        End Set
    End Property


    Private _TemplatePropertyDictJSON As String
    Public Property TemplatePropertyDictJSON As String
        Get
            Return _TemplatePropertyDictJSON
        End Get
        Set(value As String)
            _TemplatePropertyDictJSON = value
            If Me.TabControl1 IsNot Nothing Then
                If Not _TemplatePropertyDictJSON = JsonConvert.SerializeObject(Me.TemplatePropertyDict) Then
                    Me.TemplatePropertyDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(_TemplatePropertyDictJSON)
                End If
            End If

        End Set
    End Property

    'Public Property TemplatePropertyList As List(Of String)


    ' ###### GENERAL ######

    Private _UseCurrentSession As Boolean
    Public Property UseCurrentSession As Boolean
        Get
            Return _UseCurrentSession
        End Get
        Set(value As Boolean)
            _UseCurrentSession = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxUseCurrentSession.Checked = value
            End If
        End Set
    End Property

    Private _WarnSave As Boolean
    Public Property WarnSave As Boolean
        Get
            Return _WarnSave
        End Get
        Set(value As Boolean)
            _WarnSave = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxWarnSave.Checked = value
            End If
        End Set
    End Property

    Private _NoUpdateMRU As Boolean
    Public Property NoUpdateMRU As Boolean
        Get
            Return _NoUpdateMRU
        End Get
        Set(value As Boolean)
            _NoUpdateMRU = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxNoUpdateMRU.Checked = value
            End If
        End Set
    End Property

    Private _FileListFontSize As String
    Public Property FileListFontSize As String
        Get
            Return _FileListFontSize
        End Get
        Set(value As String)
            _FileListFontSize = value
            If Me.TabControl1 IsNot Nothing Then
                TextBoxFileListFontSize.Text = value
            End If
        End Set
    End Property

    Private _RememberTasks As Boolean
    Public Property RememberTasks As Boolean
        Get
            Return _RememberTasks
        End Get
        Set(value As Boolean)
            _RememberTasks = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxRememberTasks.Checked = value
            End If
        End Set
    End Property

    Private _RunInBackground As Boolean
    Public Property RunInBackground As Boolean
        Get
            Return _RunInBackground
        End Get
        Set(value As Boolean)
            _RunInBackground = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxRunInBackground.Checked = value
            End If
        End Set
    End Property

    Private _PropertyFilterIncludeDraftModel As Boolean
    Public Property PropertyFilterIncludeDraftModel As Boolean
        Get
            Return _PropertyFilterIncludeDraftModel
        End Get
        Set(value As Boolean)
            _PropertyFilterIncludeDraftModel = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxPropertyFilterIncludeDraftModel.Checked = value
            End If
        End Set
    End Property

    Private _PropertyFilterIncludeDraftItself As Boolean
    Public Property PropertyFilterIncludeDraftItself As Boolean
        Get
            Return _PropertyFilterIncludeDraftItself
        End Get
        Set(value As Boolean)
            _PropertyFilterIncludeDraftItself = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxPropertyFilterIncludeDraftItself.Checked = value
            End If
        End Set
    End Property

    Private _CheckForNewerVersion As Boolean
    Public Property CheckForNewerVersion As Boolean
        Get
            Return _CheckForNewerVersion
        End Get
        Set(value As Boolean)
            _CheckForNewerVersion = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxCheckForNewerVersion.Checked = value
            End If
        End Set
    End Property

    'Private _WarnNoImportedProperties As Boolean
    'Public Property WarnNoImportedProperties As Boolean
    '    Get
    '        Return _WarnNoImportedProperties
    '    End Get
    '    Set(value As Boolean)
    '        _WarnNoImportedProperties = value
    '        If Me.TabControl1 IsNot Nothing Then
    '            CheckBoxWarnNoImportedProperties.Checked = value
    '        End If
    '    End Set
    'End Property



    '###### HOME TAB ######

    Private _EnablePropertyFilter As Boolean
    Public Property EnablePropertyFilter As Boolean
        Get
            Return _EnablePropertyFilter
        End Get
        Set(value As Boolean)
            _EnablePropertyFilter = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxEnablePropertyFilter.Checked = value
            End If
        End Set
    End Property

    Private _EnableFileWildcard As Boolean
    Public Property EnableFileWildcard As Boolean
        Get
            Return _EnableFileWildcard
        End Get
        Set(value As Boolean)
            _EnableFileWildcard = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxEnableFileWildcard.Checked = value
            End If
        End Set
    End Property

    Private _FileWildcard As String
    Public Property FileWildcard As String
        Get
            Return _FileWildcard
        End Get
        Set(value As String)
            _FileWildcard = value
            If Me.TabControl1 IsNot Nothing Then
                ComboBoxFileWildcard.Text = value
            End If
        End Set
    End Property

    Private _FileWildcardList As List(Of String)
    Public Property FileWildcardList As List(Of String)
        Get
            Return _FileWildcardList
        End Get
        Set(value As List(Of String))
            _FileWildcardList = value
            If Me.TabControl1 IsNot Nothing Then
                For Each s As String In _FileWildcardList
                    If Not ComboBoxFileWildcard.Items.Contains(s) Then
                        ComboBoxFileWildcard.Items.Add(s)
                    End If
                Next
            End If
        End Set
    End Property



    Public Property SolidEdgeRequired As Integer



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

        'MsgBox("Fix VersionSpecificReadme")

        Dim UP As New UtilsPreferences()
        Dim UD As New UtilsDocumentation

        UP.CreatePreferencesDirectory()
        UP.CreateFilenameCharmap()
        UP.CreateSavedExpressions()
        UP.CreateInteractiveEditCommands()

        UP.GetFormMainSettings(Me)

        If Me.PropertyFilterDict Is Nothing Then
            Me.PropertyFilterDict = New Dictionary(Of String, Dictionary(Of String, String))
        End If

        If Me.TemplatePropertyDict Is Nothing Then
            Me.TemplatePropertyDict = New Dictionary(Of String, Dictionary(Of String, String))
        End If

        'If Me.TemplatePropertyList Is Nothing Then
        '    Me.TemplatePropertyList = New List(Of String)
        'End If

        If Me.FileWildcardList Is Nothing Then
            Me.FileWildcardList = New List(Of String)
        End If

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

        If Not Me.SortDependency Then
            CheckBoxSortIncludeNoDependencies.Enabled = False
        End If

        If Not Me.SortRandomSample Then
            TextBoxSortRandomSampleFraction.Enabled = False
        End If

        If Not Me.ProcessAsAvailable Then
            Dim StatusChangeRadioButtons As New List(Of RadioButton)
            Dim RB As RadioButton

            StatusChangeRadioButtons = GetStatusChangeRadioButtons()

            RadioButtonProcessAsAvailableRevert.Enabled = False
            RadioButtonProcessAsAvailableChange.Enabled = False
            For Each RB In StatusChangeRadioButtons
                RB.Enabled = False
            Next
        End If

        ListViewFilesOutOfDate = False

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
            If Not Me.RememberTasks Then
                Task.IsSelectedTask = False
                Task.IsSelectedAssembly = False
                Task.IsSelectedPart = False
                Task.IsSelectedSheetmetal = False
                Task.IsSelectedDraft = False
            End If
            tmpTaskPanel.Controls.Add(Task.TaskControl)
        Next

        'Me.TemplatePropertyDict = UP.GetTemplatePropertyDict()
        'Me.TemplatePropertyList = UP.GetTemplatePropertyList

        'Me.PropertyFilterDict = UP.GetPropertyFilterDict

        If Me.CheckForNewerVersion Then
            UP.CheckForNewerVersion(Me.Version)
        End If

    End Sub


    'Public Sub ReconcileFormChanges(Optional UpdateFileList As Boolean = False)

    '    'Dim UD As New UtilsDefaults(Me)

    '    ' Update configuration
    '    'Configuration = UD.GetConfiguration()

    '    'Dim backcolor As New Color
    '    'backcolor = BT_Update.BackColor

    '    'BT_Update.BackColor = Color.FromName("Control")

    '    'If ListViewFilesOutOfDate Then
    '    '    BT_Update.BackColor = Color.Orange
    '    'Else
    '    '    BT_Update.BackColor = Color.FromName("Control")
    '    'End If

    '    'If Not CheckBoxProcessAsAvailable.Checked Then
    '    '    Dim StatusChangeRadioButtons As New List(Of RadioButton)
    '    '    Dim RB As RadioButton

    '    '    StatusChangeRadioButtons = GetStatusChangeRadioButtons()

    '    '    RadioButtonProcessAsAvailableRevert.Enabled = False
    '    '    RadioButtonProcessAsAvailableChange.Enabled = False
    '    '    For Each RB In StatusChangeRadioButtons
    '    '        RB.Enabled = False
    '    '    Next
    '    'End If

    'End Sub

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
            'Dim UD As New UtilsDefaults(Me)

            'ReconcileFormChanges()
            'UD.SaveDefaults()

            Dim UP As New UtilsPreferences

            UP.SaveFormMainSettings(Me)

            UP.SaveTaskList(Me.TaskList)
            'UP.SaveTemplatePropertyDict(Me.TemplatePropertyDict)
            'UP.SaveTemplatePropertyList(Me.TemplatePropertyList)
            End
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As EventArgs) Handles Me.FormClosing

        'Dim UD As New UtilsDefaults(Me)

        'ReconcileFormChanges()
        'UD.SaveDefaults()

        Dim UP As New UtilsPreferences

        UP.SaveFormMainSettings(Me)

        UP.SaveTaskList(Me.TaskList)
        'UP.SaveTemplatePropertyDict(Me.TemplatePropertyDict)
        'UP.SaveTemplatePropertyList(Me.TemplatePropertyList)
        End
    End Sub

    Private Sub ButtonFastSearchScopeFilename_Click(sender As Object, e As EventArgs) Handles ButtonFastSearchScopeFilename.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a fast search scope file"
        tmpFileDialog.Filter = "Search Scope Documents|*.txt"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.FastSearchScopeFilename = tmpFileDialog.FileName
        End If
        ToolTip1.SetToolTip(TextBoxFastSearchScopeFilename, TextBoxFastSearchScopeFilename.Text)
        'ReconcileFormChanges()

    End Sub

    Private Sub new_ButtonFileSearchDelete_Click(sender As Object, e As EventArgs) Handles new_ButtonFileSearchDelete.Click
        If Not ComboBoxFileWildcard.Text = "" Then
            ComboBoxFileWildcard.Items.Remove(ComboBoxFileWildcard.Text)
            ComboBoxFileWildcard.Text = ""
        End If
    End Sub

    Private Sub new_ButtonPropertyFilter_Click(sender As Object, e As EventArgs) Handles new_ButtonPropertyFilter.Click

        ' Check if the Properties were imported from templates
        Dim tf As Boolean

        tf = Me.TemplatePropertyDict Is Nothing
        'tf = tf Or Me.TemplatePropertyList Is Nothing

        If Not tf Then
            tf = Me.TemplatePropertyDict.Count = 0
            'tf = tf Or Me.TemplatePropertyList.Count = 0
        End If

        If Not tf Then
            Dim FPF As New FormPropertyFilter

            FPF.PropertyFilterDict = Me.PropertyFilterDict
            FPF.ShowDialog()

            If FPF.DialogResult = DialogResult.OK Then
                Me.PropertyFilterDict = FPF.PropertyFilterDict
                ListViewFilesOutOfDate = True
            End If
        Else
            Dim s = "Template properties required for this command not found. "
            s = String.Format("{0}Populate them on the Configuration Tab -- Templates Page.", s)
            MsgBox(s, vbOKOnly)
            Exit Sub
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

    Private Sub CheckBoxEnablePropertyFilter_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxEnablePropertyFilter.CheckedChanged
        Me.EnablePropertyFilter = CheckBoxEnablePropertyFilter.Checked

        If CheckBoxEnablePropertyFilter.Checked Then

            CheckBoxEnablePropertyFilter.Image = My.Resources.Checked
            new_ButtonPropertyFilter.Enabled = True

        Else
            CheckBoxEnablePropertyFilter.Image = My.Resources.Unchecked
            new_ButtonPropertyFilter.Enabled = False
        End If

        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange

    End Sub

    Private Sub CheckBoxEnableFileWildcard_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxEnableFileWildcard.CheckedChanged
        Me.EnableFileWildcard = CheckBoxEnableFileWildcard.Checked

        If CheckBoxEnableFileWildcard.Checked Then
            CheckBoxEnableFileWildcard.Image = My.Resources.Checked
            ComboBoxFileWildcard.Enabled = True
        Else
            CheckBoxEnableFileWildcard.Image = My.Resources.Unchecked
            ComboBoxFileWildcard.Enabled = False
        End If

        ListViewFilesOutOfDate = True
        BT_Update.BackColor = Color.Orange

        'ApplyFilters()

    End Sub

    Private Sub new_CheckBoxFilterAsm_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxFilterAsm.CheckedChanged
        ListViewFilesOutOfDate = True
        'ReconcileFormChanges()
    End Sub

    Private Sub new_CheckBoxFilterPar_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxFilterPar.CheckedChanged
        ListViewFilesOutOfDate = True
        'ReconcileFormChanges()
    End Sub

    Private Sub new_CheckBoxFilterPsm_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxFilterPsm.CheckedChanged
        ListViewFilesOutOfDate = True
        'ReconcileFormChanges()
    End Sub

    Private Sub new_CheckBoxFilterDft_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxFilterDft.CheckedChanged
        ListViewFilesOutOfDate = True
        'ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxRememberTasks_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRememberTasks.CheckedChanged
        Me.RememberTasks = CheckBoxRememberTasks.Checked
        'ReconcileFormChanges()
    End Sub

    Private Sub CheckBoxTLAReportUnrelatedFiles_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTLAReportUnrelatedFiles.CheckedChanged
        Me.TLAReportUnrelatedFiles = CheckBoxTLAReportUnrelatedFiles.Checked

        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange

        'ReconcileFormChanges()
    End Sub


    ' COMBOBOXES


    ' LINK LABELS

    Private Sub LinkLabelGitHubReadme_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start(e.Link.LinkData.ToString())
    End Sub


    ' RADIO BUTTONS
    Private Sub RadioButtonTLABottomUp_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTLABottomUp.CheckedChanged
        Dim tf As Boolean = RadioButtonTLABottomUp.Checked
        Me.TLABottomUp = tf

        If tf Then
            TextBoxFastSearchScopeFilename.Enabled = True
            ButtonFastSearchScopeFilename.Enabled = True
            CheckBoxDraftAndModelSameName.Enabled = True
            'CheckBoxTLAIgnoreIncludeInReports.Enabled = True
        End If

        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange

        'ReconcileFormChanges()
    End Sub

    Private Sub RadioButtonTLATopDown_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTLATopDown.CheckedChanged
        Dim tf As Boolean = RadioButtonTLATopDown.Checked
        Me.TLATopDown = tf

        If tf Then
            TextBoxFastSearchScopeFilename.Enabled = False
            ButtonFastSearchScopeFilename.Enabled = False
            CheckBoxDraftAndModelSameName.Enabled = False
            'CheckBoxTLAIgnoreIncludeInReports.Enabled = False
        End If

        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange

        'ReconcileFormChanges()
    End Sub


    ' TEXT BOXES



    Private Sub TextBoxFastSearchScopeFilename_TextChanged(sender As Object, e As EventArgs) Handles TextBoxFastSearchScopeFilename.TextChanged
        Me.FastSearchScopeFilename = TextBoxFastSearchScopeFilename.Text

        'ReconcileFormChanges()
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
            'BT_Update.BackColor = Color.Orange
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
            'BT_Update.BackColor = Color.Orange

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
            'BT_Update.BackColor = Color.Orange

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
            'BT_Update.BackColor = Color.Orange

        End If

    End Sub

    Private Sub BT_DeleteAll_Click(sender As Object, e As EventArgs) Handles BT_DeleteAll.Click

        ListViewFiles.BeginUpdate()
        ListViewFiles.Items.Clear()
        ListViewFiles.EndUpdate()

        DragDropCache.Clear()
        DragDropCacheExcluded.Clear()

        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange

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
                    'BT_Update.BackColor = Color.Orange

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


    Private Sub TextBoxFontSize_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxFileListFontSize.KeyDown

        If e.KeyCode = Keys.Enter Then
            Me.ActiveControl = Nothing
            e.Handled = True
            e.SuppressKeyPress = True
        End If

    End Sub

    Private Sub TextBoxFontSize_Leave(sender As Object, e As EventArgs) Handles TextBoxFileListFontSize.Leave

        If Not IsNumeric(TextBoxFileListFontSize.Text) Then TextBoxFileListFontSize.Text = "9"
        If TextBoxFileListFontSize.Text = "0" Then TextBoxFileListFontSize.Text = "9"
        ListViewFiles.Font = New Font(ListViewFiles.Font.FontFamily, CInt(TextBoxFileListFontSize.Text), FontStyle.Regular)

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
        Me.TLAIncludePartCopies = CheckBoxTLAIncludePartCopies.Checked

        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange
    End Sub

    Private Sub CheckBoxDraftAndModelSameName_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDraftAndModelSameName.CheckedChanged
        Me.DraftAndModelSameName = CheckBoxDraftAndModelSameName.Checked

        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange

    End Sub

    Private Sub RadioButtonSortDependency_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSortDependency.CheckedChanged
        Me.SortDependency = RadioButtonSortDependency.Checked

        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange

        If RadioButtonSortDependency.Checked Then
            CheckBoxSortIncludeNoDependencies.Enabled = True
        Else
            CheckBoxSortIncludeNoDependencies.Enabled = False
        End If
    End Sub

    Private Sub RadioButtonSortNone_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSortNone.CheckedChanged
        Me.SortNone = RadioButtonSortNone.Checked

        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange

    End Sub

    Private Sub RadioButtonSortAlphabetical_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSortAlphabetical.CheckedChanged
        Me.SortAlphabetical = RadioButtonSortAlphabetical.Checked

        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange

    End Sub

    Private Sub CheckBoxSortIncludeNoDependencies_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSortIncludeNoDependencies.CheckedChanged
        Me.SortIncludeNoDependencies = CheckBoxSortIncludeNoDependencies.Checked

        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange

    End Sub

    Private Sub RadioButtonSortRandomSample_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSortRandomSample.CheckedChanged
        Me.SortRandomSample = RadioButtonSortRandomSample.Checked

        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange

        If RadioButtonSortRandomSample.Checked Then
            TextBoxSortRandomSampleFraction.Enabled = True
        Else
            TextBoxSortRandomSampleFraction.Enabled = False
        End If
    End Sub

    Private Sub TextBoxRandomSampleFraction_LostFocus(sender As Object, e As EventArgs) Handles TextBoxSortRandomSampleFraction.LostFocus
        ListViewFilesOutOfDate = True
        'BT_Update.BackColor = Color.Orange

        Dim Fraction As Double
        Try
            Fraction = CDbl(TextBoxSortRandomSampleFraction.Text)
            If Fraction < 0 Or Fraction > 1 Then
                MsgBox(String.Format("Number '{0}' is not between 0.0 and 1.0", Fraction))
                TextBoxSortRandomSampleFraction.Text = "0.1"
            End If
        Catch ex As Exception
            MsgBox(String.Format("Cannot convert '{0}' to a decimal number", TextBoxSortRandomSampleFraction.Text))
            TextBoxSortRandomSampleFraction.Text = "0.1"
        End Try
    End Sub

    Private Sub CheckBoxProcessAsAvailable_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxProcessAsAvailable.CheckedChanged
        Me.ProcessAsAvailable = CheckBoxProcessAsAvailable.Checked

        Dim StatusChangeRadioButtons As New List(Of RadioButton)
        Dim RB As RadioButton

        StatusChangeRadioButtons = GetStatusChangeRadioButtons()

        If CheckBoxProcessAsAvailable.Checked Then
            RadioButtonProcessAsAvailableRevert.Enabled = True
            RadioButtonProcessAsAvailableChange.Enabled = True
            If RadioButtonProcessAsAvailableChange.Checked Then
                For Each RB In StatusChangeRadioButtons
                    RB.Enabled = True
                Next
            End If
        Else
            RadioButtonProcessAsAvailableRevert.Enabled = False
            RadioButtonProcessAsAvailableChange.Enabled = False
            For Each RB In StatusChangeRadioButtons
                RB.Enabled = False
            Next
        End If
    End Sub

    Private Sub RadioButtonProcessAsAvailableRevert_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonProcessAsAvailableRevert.CheckedChanged
        Me.ProcessAsAvailableRevert = RadioButtonProcessAsAvailableRevert.Checked

        Dim StatusChangeRadioButtons As New List(Of RadioButton)
        Dim RB As RadioButton

        StatusChangeRadioButtons = GetStatusChangeRadioButtons()

        If RadioButtonProcessAsAvailableRevert.Checked Then
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

    Private Sub LabelRandomSampleFraction_Click(sender As Object, e As EventArgs) Handles LabelSortRandomSampleFraction.Click

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
        ETL.RememberTaskSelections = Me.RememberTasks
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
            'TextBoxAssemblyTemplate.Text = Me.AssemblyTemplate
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

    'Private Sub CheckBoxUseTemplateProperties_CheckedChanged(sender As Object, e As EventArgs)
    '    Me.UseTemplateProperties = CheckBoxUseTemplateProperties.Checked

    '    ButtonUseTemplateProperties.Enabled = Me.UseTemplateProperties
    'End Sub

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

        Dim UC As New UtilsCommon

        Dim TemplateList = {Me.AssemblyTemplate, Me.PartTemplate, Me.SheetmetalTemplate, Me.DraftTemplate}.ToList

        Me.Cursor = Cursors.WaitCursor

        Me.TemplatePropertyDict = UC.TemplatePropertyDictPopulate(TemplateList, Me.TemplatePropertyDict)

        Me.Cursor = Cursors.Default

        ButtonCustomizeTemplatePropertyDict.PerformClick()

    End Sub

    Private Sub TextBoxAssemblyTemplate_TextChanged(sender As Object, e As EventArgs) Handles TextBoxAssemblyTemplate.TextChanged
        Me.AssemblyTemplate = TextBoxAssemblyTemplate.Text
    End Sub

    Private Sub TextBoxPartTemplate_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPartTemplate.TextChanged
        Me.PartTemplate = TextBoxPartTemplate.Text
    End Sub

    Private Sub TextBoxSheetmetalTemplate_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSheetmetalTemplate.TextChanged
        Me.SheetmetalTemplate = TextBoxSheetmetalTemplate.Text
    End Sub

    Private Sub TextBoxDraftTemplate_TextChanged(sender As Object, e As EventArgs) Handles TextBoxDraftTemplate.TextChanged
        Me.DraftTemplate = TextBoxDraftTemplate.Text
    End Sub

    Private Sub TextBoxMaterialTable_TextChanged(sender As Object, e As EventArgs) Handles TextBoxMaterialTable.TextChanged
        Me.MaterialTable = TextBoxMaterialTable.Text
    End Sub

    Private Sub ButtonCustomizeTemplatePropertyDict_Click(sender As Object, e As EventArgs) Handles ButtonCustomizeTemplatePropertyDict.Click


        Dim FPLC As New FormPropertyListCustomize

        Dim Result As DialogResult = FPLC.ShowDialog()

        If Result = DialogResult.OK Then
            Dim UC As New UtilsCommon
            Me.TemplatePropertyDict = UC.TemplatePropertyDictUpdateFavorites(Me.TemplatePropertyDict, FPLC.FavoritesList)
        End If

    End Sub

    Private Sub CheckBoxNoUpdateMRU_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxNoUpdateMRU.CheckedChanged
        Me.NoUpdateMRU = CheckBoxNoUpdateMRU.Checked
    End Sub

    Private Sub CheckBoxRunInBackground_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRunInBackground.CheckedChanged
        Me.RunInBackground = CheckBoxRunInBackground.Checked
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

    Private Sub CheckBoxTLAAutoIncludeTLF_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTLAAutoIncludeTLF.CheckedChanged
        Me.TLAAutoIncludeTLF = CheckBoxTLAAutoIncludeTLF.Checked
    End Sub

    Private Sub CheckBoxWarnBareTLA_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxWarnBareTLA.CheckedChanged
        Me.WarnBareTLA = CheckBoxWarnBareTLA.Checked
    End Sub

    Private Sub RadioButtonProcessAsAvailableChange_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonProcessAsAvailableChange.CheckedChanged
        Me.ProcessAsAvailableChange = RadioButtonProcessAsAvailableChange.Checked
    End Sub


    Private Sub RadioButtonStatusAtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoA.CheckedChanged
        If RadioButtonStatusAtoA.Checked Then
            Me.StatusAto = "A"
        End If
    End Sub
    Private Sub RadioButtonStatusAtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoB.CheckedChanged
        If RadioButtonStatusAtoB.Checked Then
            Me.StatusAto = "B"
        End If
    End Sub
    Private Sub RadioButtonStatusAtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoIR.CheckedChanged
        If RadioButtonStatusAtoIR.Checked Then
            Me.StatusAto = "IR"
        End If
    End Sub
    Private Sub RadioButtonStatusAtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoIW.CheckedChanged
        If RadioButtonStatusAtoIW.Checked Then
            Me.StatusAto = "IW"
        End If
    End Sub
    Private Sub RadioButtonStatusAtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoO.CheckedChanged
        If RadioButtonStatusAtoO.Checked Then
            Me.StatusAto = "O"
        End If
    End Sub
    Private Sub RadioButtonStatusAtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoR.CheckedChanged
        If RadioButtonStatusAtoR.Checked Then
            Me.StatusAto = "R"
        End If
    End Sub

    Private Sub RadioButtonStatusBtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoA.CheckedChanged
        If RadioButtonStatusBtoA.Checked Then
            Me.StatusBto = "A"
        End If
    End Sub
    Private Sub RadioButtonStatusBtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoB.CheckedChanged
        If RadioButtonStatusBtoB.Checked Then
            Me.StatusBto = "B"
        End If
    End Sub
    Private Sub RadioButtonStatusBtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoIR.CheckedChanged
        If RadioButtonStatusBtoIR.Checked Then
            Me.StatusBto = "IR"
        End If
    End Sub
    Private Sub RadioButtonStatusBtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoIW.CheckedChanged
        If RadioButtonStatusBtoIW.Checked Then
            Me.StatusBto = "IW"
        End If
    End Sub
    Private Sub RadioButtonStatusBtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoO.CheckedChanged
        If RadioButtonStatusBtoO.Checked Then
            Me.StatusBto = "O"
        End If
    End Sub
    Private Sub RadioButtonStatusBtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoR.CheckedChanged
        If RadioButtonStatusBtoR.Checked Then
            Me.StatusBto = "R"
        End If
    End Sub

    Private Sub RadioButtonStatusIRtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoA.CheckedChanged
        If RadioButtonStatusIRtoA.Checked Then
            Me.StatusIRto = "A"
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoB.CheckedChanged
        If RadioButtonStatusIRtoB.Checked Then
            Me.StatusIRto = "B"
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoIR.CheckedChanged
        If RadioButtonStatusIRtoIR.Checked Then
            Me.StatusIRto = "IR"
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoIW.CheckedChanged
        If RadioButtonStatusIRtoIW.Checked Then
            Me.StatusIRto = "IW"
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoO.CheckedChanged
        If RadioButtonStatusIRtoO.Checked Then
            Me.StatusIRto = "O"
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoR.CheckedChanged
        If RadioButtonStatusIRtoR.Checked Then
            Me.StatusIRto = "R"
        End If
    End Sub

    Private Sub RadioButtonStatusIWtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoA.CheckedChanged
        If RadioButtonStatusIWtoA.Checked Then
            Me.StatusIWto = "A"
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoB.CheckedChanged
        If RadioButtonStatusIWtoB.Checked Then
            Me.StatusIWto = "B"
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoIR.CheckedChanged
        If RadioButtonStatusIWtoIR.Checked Then
            Me.StatusIWto = "IR"
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoIW.CheckedChanged
        If RadioButtonStatusIWtoIW.Checked Then
            Me.StatusIWto = "IW"
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoO.CheckedChanged
        If RadioButtonStatusIWtoO.Checked Then
            Me.StatusIWto = "O"
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoR.CheckedChanged
        If RadioButtonStatusIWtoR.Checked Then
            Me.StatusIWto = "R"
        End If
    End Sub

    Private Sub RadioButtonStatusOtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoA.CheckedChanged
        If RadioButtonStatusOtoA.Checked Then
            Me.StatusOto = "A"
        End If
    End Sub
    Private Sub RadioButtonStatusOtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoB.CheckedChanged
        If RadioButtonStatusOtoB.Checked Then
            Me.StatusOto = "B"
        End If
    End Sub
    Private Sub RadioButtonStatusOtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoIR.CheckedChanged
        If RadioButtonStatusOtoIR.Checked Then
            Me.StatusOto = "IR"
        End If
    End Sub
    Private Sub RadioButtonStatusOtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoIW.CheckedChanged
        If RadioButtonStatusOtoIW.Checked Then
            Me.StatusOto = "IW"
        End If
    End Sub
    Private Sub RadioButtonStatusOtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoO.CheckedChanged
        If RadioButtonStatusOtoO.Checked Then
            Me.StatusOto = "O"
        End If
    End Sub
    Private Sub RadioButtonStatusOtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoR.CheckedChanged
        If RadioButtonStatusOtoR.Checked Then
            Me.StatusOto = "R"
        End If
    End Sub

    Private Sub RadioButtonStatusRtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoA.CheckedChanged
        If RadioButtonStatusRtoA.Checked Then
            Me.StatusRto = "A"
        End If
    End Sub
    Private Sub RadioButtonStatusRtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoB.CheckedChanged
        If RadioButtonStatusRtoB.Checked Then
            Me.StatusRto = "B"
        End If
    End Sub
    Private Sub RadioButtonStatusRtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoIR.CheckedChanged
        If RadioButtonStatusRtoIR.Checked Then
            Me.StatusRto = "IR"
        End If
    End Sub
    Private Sub RadioButtonStatusRtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoIW.CheckedChanged
        If RadioButtonStatusRtoIW.Checked Then
            Me.StatusRto = "IW"
        End If
    End Sub
    Private Sub RadioButtonStatusRtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoO.CheckedChanged
        If RadioButtonStatusRtoO.Checked Then
            Me.StatusRto = "O"
        End If
    End Sub
    Private Sub RadioButtonStatusRtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoR.CheckedChanged
        If RadioButtonStatusRtoR.Checked Then
            Me.StatusRto = "R"
        End If
    End Sub

    Private Sub TextBoxSortRandomSampleFraction_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSortRandomSampleFraction.TextChanged
        Me.SortRandomSampleFraction = TextBoxSortRandomSampleFraction.Text
    End Sub

    Private Sub CheckBoxWarnSave_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxWarnSave.CheckedChanged
        Me.WarnSave = CheckBoxWarnSave.Checked

    End Sub

    Private Sub TextBoxFileListFontSize_TextChanged(sender As Object, e As EventArgs) Handles TextBoxFileListFontSize.TextChanged
        Me.FileListFontSize = TextBoxFileListFontSize.Text
    End Sub

    Private Sub CheckBoxPropertyFilterIncludeDraftModel_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPropertyFilterIncludeDraftModel.CheckedChanged
        Me.PropertyFilterIncludeDraftModel = CheckBoxPropertyFilterIncludeDraftModel.Checked

    End Sub

    Private Sub CheckBoxPropertyFilterIncludeDraftItself_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPropertyFilterIncludeDraftItself.CheckedChanged
        Me.PropertyFilterIncludeDraftItself = CheckBoxPropertyFilterIncludeDraftItself.Checked
    End Sub

    Private Sub ComboBoxFileWildcard_LostFocus(sender As Object, e As EventArgs) Handles ComboBoxFileWildcard.LostFocus
        Me.FileWildcard = ComboBoxFileWildcard.Text

        If Not FileWildcardList.Contains(Me.FileWildcard) Then
            FileWildcardList.Add(Me.FileWildcard)
        End If

        If Not ComboBoxFileWildcard.Items.Contains(ComboBoxFileWildcard.Text) Then
            ComboBoxFileWildcard.Items.Add(ComboBoxFileWildcard.Text)
        End If

        ListViewFilesOutOfDate = True
    End Sub

    Private Sub ComboBoxFileWildcard_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxFileWildcard.SelectedIndexChanged
        Me.FileWildcard = ComboBoxFileWildcard.Text

        ListViewFilesOutOfDate = True
    End Sub

    'Private Sub CheckBoxWarnNoImportedProperties_CheckedChanged(sender As Object, e As EventArgs)
    '    Me.WarnNoImportedProperties = CheckBoxWarnNoImportedProperties.Checked
    'End Sub









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

    ' BaseName = System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName)
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


