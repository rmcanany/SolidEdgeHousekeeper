Option Strict On

Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
'Imports System.Windows
Imports ListViewExtended
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports Newtonsoft.Json
Imports OpenMcdf

Public Class Form_Main

    Private lvwColumnSorter As ListViewColumnSorter


    Private editbox As New TextBox()
    Private hitinfo As ListViewHitTestInfo

    Public Property Version As String = "2024.4"  ' Two fields, both integers: Year.ReleaseNumber.  Can include a bugfix number which is ignored

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

    '0 Available
    '1 InWork
    '2 InReview
    '3 Released
    '4 Baselined
    '5 Obsolete

    Private _ProcessAsAvailable As Boolean
    Public Property ProcessAsAvailable As Boolean
        Get
            Return _ProcessAsAvailable
        End Get
        Set(value As Boolean)
            _ProcessAsAvailable = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxProcessAsAvailable.Checked = value

                Dim StatusChangeRadioButtons As New List(Of RadioButton)
                Dim RB As RadioButton

                StatusChangeRadioButtons = GetStatusChangeRadioButtons()

                CheckBoxUseDMForStatusChanges.Enabled = Me.ProcessAsAvailable
                RadioButtonProcessAsAvailableRevert.Enabled = Me.ProcessAsAvailable
                RadioButtonProcessAsAvailableChange.Enabled = Me.ProcessAsAvailable
                If RadioButtonProcessAsAvailableChange.Checked Then
                    For Each RB In StatusChangeRadioButtons
                        RB.Enabled = Me.ProcessAsAvailable
                    Next
                End If
            End If
        End Set
    End Property

    Private _UseDMForStatusChanges As Boolean
    Public Property UseDMForStatusChanges As Boolean
        Get
            Return _UseDMForStatusChanges
        End Get
        Set(value As Boolean)
            _UseDMForStatusChanges = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxUseDMForStatusChanges.Checked = value
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

    Private _StatusAtoX As Integer
    Public Property StatusAtoX As Integer
        Get
            Return _StatusAtoX
        End Get
        Set(value As Integer)
            _StatusAtoX = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case 0
                        RadioButtonStatusAtoA.Checked = True
                    Case 1
                        RadioButtonStatusAtoIW.Checked = True
                    Case 2
                        RadioButtonStatusAtoIR.Checked = True
                    Case 3
                        RadioButtonStatusAtoR.Checked = True
                    Case 4
                        RadioButtonStatusAtoB.Checked = True
                    Case 5
                        RadioButtonStatusAtoO.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusBtoX As Integer
    Public Property StatusBtoX As Integer
        Get
            Return _StatusBtoX
        End Get
        Set(value As Integer)
            _StatusBtoX = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case 0
                        RadioButtonStatusBtoA.Checked = True
                    Case 1
                        RadioButtonStatusBtoIW.Checked = True
                    Case 2
                        RadioButtonStatusBtoIR.Checked = True
                    Case 3
                        RadioButtonStatusBtoR.Checked = True
                    Case 4
                        RadioButtonStatusBtoB.Checked = True
                    Case 5
                        RadioButtonStatusBtoO.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusIRtoX As Integer
    Public Property StatusIRtoX As Integer
        Get
            Return _StatusIRtoX
        End Get
        Set(value As Integer)
            _StatusIRtoX = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case 0
                        RadioButtonStatusIRtoA.Checked = True
                    Case 1
                        RadioButtonStatusIRtoIW.Checked = True
                    Case 2
                        RadioButtonStatusIRtoIR.Checked = True
                    Case 3
                        RadioButtonStatusIRtoR.Checked = True
                    Case 4
                        RadioButtonStatusIRtoB.Checked = True
                    Case 5
                        RadioButtonStatusIRtoO.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusIWtoX As Integer
    Public Property StatusIWtoX As Integer
        Get
            Return _StatusIWtoX
        End Get
        Set(value As Integer)
            _StatusIWtoX = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case 0
                        RadioButtonStatusIWtoA.Checked = True
                    Case 1
                        RadioButtonStatusIWtoIW.Checked = True
                    Case 2
                        RadioButtonStatusIWtoIR.Checked = True
                    Case 3
                        RadioButtonStatusIWtoR.Checked = True
                    Case 4
                        RadioButtonStatusIWtoB.Checked = True
                    Case 5
                        RadioButtonStatusIWtoO.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusOtoX As Integer
    Public Property StatusOtoX As Integer
        Get
            Return _StatusOtoX
        End Get
        Set(value As Integer)
            _StatusOtoX = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case 0
                        RadioButtonStatusOtoA.Checked = True
                    Case 1
                        RadioButtonStatusOtoIW.Checked = True
                    Case 2
                        RadioButtonStatusOtoIR.Checked = True
                    Case 3
                        RadioButtonStatusOtoR.Checked = True
                    Case 4
                        RadioButtonStatusOtoB.Checked = True
                    Case 5
                        RadioButtonStatusOtoO.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusRtoX As Integer
    Public Property StatusRtoX As Integer
        Get
            Return _StatusRtoX
        End Get
        Set(value As Integer)
            _StatusRtoX = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case 0
                        RadioButtonStatusRtoA.Checked = True
                    Case 1
                        RadioButtonStatusRtoIW.Checked = True
                    Case 2
                        RadioButtonStatusRtoIR.Checked = True
                    Case 3
                        RadioButtonStatusRtoR.Checked = True
                    Case 4
                        RadioButtonStatusRtoB.Checked = True
                    Case 5
                        RadioButtonStatusRtoO.Checked = True
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
                CheckBoxSortIncludeNoDependencies.Enabled = value
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
                TextBoxSortRandomSampleFraction.Enabled = value
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

    '' See UC.TemplatePropertyDictAddProp for dictionary key definitions and values.
    'Private _TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    'Public Property TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    '    Get
    '        Return _TemplatePropertyDict
    '    End Get
    '    Set(value As Dictionary(Of String, Dictionary(Of String, String)))
    '        _TemplatePropertyDict = value
    '        If Me.TabControl1 IsNot Nothing Then
    '            Dim s = JsonConvert.SerializeObject(Me.TemplatePropertyDict)
    '            If Not Me.TemplatePropertyDictJSON = s Then
    '                Me.TemplatePropertyDictJSON = s
    '            End If
    '        End If
    '    End Set
    'End Property

    'Private _TemplatePropertyDictJSON As String
    'Public Property TemplatePropertyDictJSON As String
    '    Get
    '        Return _TemplatePropertyDictJSON
    '    End Get
    '    Set(value As String)
    '        _TemplatePropertyDictJSON = value
    '        If Me.TabControl1 IsNot Nothing Then
    '            If Not _TemplatePropertyDictJSON = JsonConvert.SerializeObject(Me.TemplatePropertyDict) Then
    '                Me.TemplatePropertyDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(_TemplatePropertyDictJSON)
    '            End If
    '        End If

    '    End Set
    'End Property

    Public Property PropertiesData As PropertiesData


    ' ###### SERVER QUERY ######

    Private _ServerConnectionString As String
    Public Property ServerConnectionString As String
        Get
            Return _ServerConnectionString
        End Get
        Set(value As String)
            _ServerConnectionString = value
            If Me.TabControl1 IsNot Nothing Then
                TextBoxServerConnectionString.Text = value
            End If
        End Set
    End Property


    Private _ServerQuery As String
    Public Property ServerQuery As String
        Get
            Return _ServerQuery
        End Get
        Set(value As String)
            _ServerQuery = value
            If Me.TabControl1 IsNot Nothing Then
                'TextBoxServerQuery.Text = value
                If FastColoredServerQuery.Text <> value Then FastColoredServerQuery.Text = value  '<---- This may throw an exception due to a weird initialization of the component in Form_Main.Designer.vb
            End If
        End Set
    End Property



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
                new_ButtonPropertyFilter.Enabled = value
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
                ComboBoxFileWildcard.Enabled = value
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

    Private _FilterAsm As Boolean
    Public Property FilterAsm As Boolean
        Get
            Return _FilterAsm
        End Get
        Set(value As Boolean)
            _FilterAsm = value
            If Me.TabControl1 IsNot Nothing Then
                new_CheckBoxFilterAsm.Checked = value
            End If
        End Set
    End Property

    Private _FilterPar As Boolean
    Public Property FilterPar As Boolean
        Get
            Return _FilterPar
        End Get
        Set(value As Boolean)
            _FilterPar = value
            If Me.TabControl1 IsNot Nothing Then
                new_CheckBoxFilterPar.Checked = value
            End If
        End Set
    End Property

    Private _FilterPsm As Boolean
    Public Property FilterPsm As Boolean
        Get
            Return _FilterPsm
        End Get
        Set(value As Boolean)
            _FilterPsm = value
            If Me.TabControl1 IsNot Nothing Then
                new_CheckBoxFilterPsm.Checked = value
            End If
        End Set
    End Property

    Private _FilterDft As Boolean
    Public Property FilterDft As Boolean
        Get
            Return _FilterDft
        End Get
        Set(value As Boolean)
            _FilterDft = value
            If Me.TabControl1 IsNot Nothing Then
                new_CheckBoxFilterDft.Checked = value
            End If
        End Set
    End Property

    Public Property SolidEdgeRequired As Integer


    Private _ListOfColumns As List(Of PropertyColumn)
    Public Property ListOfColumns As List(Of PropertyColumn)
        Get
            Return _ListOfColumns
        End Get
        Set(value As List(Of PropertyColumn))
            _ListOfColumns = value
            If Me.TabControl1 IsNot Nothing Then
                For Each PropColumn As PropertyColumn In _ListOfColumns
                    If Not CLB_Properties.Items.Contains(PropColumn.Name) Then
                        CLB_Properties.Items.Add(PropColumn.Name)
                    End If
                Next

                Dim UFL As New UtilsFileList(Me, ListViewFiles)
                Me.Cursor = Cursors.WaitCursor
                UFL.UpdatePropertiesColumns()
                Me.Cursor = Cursors.Default

                Dim tmpListOfColumnsJSON As New List(Of String)

                For Each PropColumn In Me.ListOfColumns
                    tmpListOfColumnsJSON.Add(PropColumn.ToJSON)
                Next

                Dim UC As New UtilsCommon
                If Not UC.CompareListOfJSON(tmpListOfColumnsJSON, Me.ListOfColumnsJSON) Then
                    Me.ListOfColumnsJSON = tmpListOfColumnsJSON
                End If
            End If
        End Set

    End Property

    Private _ListOfColumnsJSON As List(Of String)
    Public Property ListOfColumnsJSON As List(Of String)
        Get
            Return _ListOfColumnsJSON
        End Get
        Set(value As List(Of String))
            _ListOfColumnsJSON = value
            If Me.TabControl1 IsNot Nothing Then
                Dim tmpListOfColumns As New List(Of PropertyColumn)
                For Each s As String In _ListOfColumnsJSON
                    Dim tmpPropertyColumn As New PropertyColumn
                    tmpPropertyColumn.FromJSON(s)
                    tmpListOfColumns.Add(tmpPropertyColumn)
                Next

                Dim UC As New UtilsCommon
                If Not UC.CompareListOfColumns(tmpListOfColumns, Me.ListOfColumns) Then
                    Me.ListOfColumns = tmpListOfColumns
                End If
            End If
        End Set
    End Property


    'Private _PresetsList As List(Of Preset)
    'Public Property PresetsList As List(Of Preset)
    '    Get
    '        Return _PresetsList
    '    End Get
    '    Set(value As List(Of Preset))
    '        _PresetsList = value
    '        If Me.TabControl1 IsNot Nothing Then
    '            Dim TextOld = ComboBoxPresetName.Text
    '            ComboBoxPresetName.Items.Clear()
    '            ComboBoxPresetName.Items.Add("")
    '            For Each tmpPreset In PresetsList
    '                ComboBoxPresetName.Items.Add(tmpPreset.Name)
    '            Next
    '            ComboBoxPresetName.Text = TextOld

    '            Dim tmpPresetsListJSON As New List(Of String)
    '            For Each tmpPreset As Preset In _PresetsList
    '                tmpPresetsListJSON.Add(tmpPreset.ToJSON)
    '            Next

    '            Dim UC As New UtilsCommon
    '            If Not UC.CompareListOfJSON(tmpPresetsListJSON, Me.PresetsListJSON) Then
    '                Me.PresetsListJSON = tmpPresetsListJSON
    '            End If
    '        End If
    '    End Set
    'End Property

    'Private _PresetsListJSON As List(Of String)
    'Public Property PresetsListJSON As List(Of String)
    '    Get
    '        Return _PresetsListJSON
    '    End Get
    '    Set(value As List(Of String))
    '        _PresetsListJSON = value
    '        If Me.TabControl1 IsNot Nothing Then
    '            Dim tmpPresetsList As New List(Of Preset)
    '            For Each s As String In _PresetsListJSON
    '                Dim tmpPreset As New Preset
    '                tmpPreset.FromJSON(s)
    '                tmpPresetsList.Add(tmpPreset)
    '            Next

    '            Dim UC As New UtilsCommon
    '            If Not UC.ComparePresetList(tmpPresetsList, Me.PresetsList) Then
    '                Me.PresetsList = tmpPresetsList
    '            End If
    '        End If
    '    End Set
    'End Property


    Private _Presets As Presets
    Public Property Presets As Presets
        Get
            Return _Presets
        End Get
        Set(value As Presets)
            _Presets = value
            If Me.TabControl1 IsNot Nothing Then

                Dim TextOld = ComboBoxPresetName.Text

                ComboBoxPresetName.Items.Clear()
                ComboBoxPresetName.Items.Add("")
                'For Each tmpPreset In PresetsList
                '    ComboBoxPresetName.Items.Add(tmpPreset.Name)
                'Next
                For Each tmpPreset As Preset In Presets.Items
                    ComboBoxPresetName.Items.Add(tmpPreset.Name)
                Next

                ComboBoxPresetName.Text = TextOld

                'Dim tmpPresetsJSON As String = Me.Presets.ToJSON

                'If Not Me.PresetsJSON = tmpPresetsJSON Then
                '    Me.PresetsJSON = tmpPresetsJSON
                'End If

            End If
        End Set
    End Property

    'Private _PresetsJSON As String
    'Public Property PresetsJSON As String
    '    Get
    '        Return _PresetsJSON
    '    End Get
    '    Set(value As String)
    '        _PresetsJSON = value
    '        If Me.TabControl1 IsNot Nothing Then

    '            If Not Me.PresetsJSON = Me.Presets.ToJSON Then
    '                Me.Presets.FromJSON(Me.PresetsJSON)
    '            End If

    '        End If
    '    End Set
    'End Property




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


    Public Property PropertyFilters As PropertyFilters


    Private Sub Startup()

        Me.Cursor = Cursors.WaitCursor

        Dim UP As New UtilsPreferences()
        Dim UD As New UtilsDocumentation
        Dim UC As New UtilsCommon


        '###### INITIALIZE PREFERENCES IF NEEDED ######

        UP.CreatePreferencesDirectory()
        UP.CreateFilenameCharmap()
        UP.CreateSavedExpressions()
        UP.CreateInteractiveEditCommands()


        '###### LOAD MAIN FORM SAVED SETTINGS IF ANY ######

        UP.GetFormMainSettings(Me)


        '###### INITIALIZE DATA STRUCTURES IF NEEDED ######

        If Me.PropertyFilterDict Is Nothing Then
            Me.PropertyFilterDict = New Dictionary(Of String, Dictionary(Of String, String))
        End If

        'If Me.TemplatePropertyDict Is Nothing Then
        '    Me.TemplatePropertyDict = New Dictionary(Of String, Dictionary(Of String, String))
        'End If

        If Me.FileWildcardList Is Nothing Then
            Me.FileWildcardList = New List(Of String)
        End If

        If Me.ListOfColumns Is Nothing Then
            Me.ListOfColumns = New List(Of PropertyColumn)
        End If

        If Me.ListOfColumns.Count = 0 Then
            Dim tmpListOfColumns = New List(Of PropertyColumn)

            Dim NameColumn As New PropertyColumn With {
                .Name = "Name",
                .Visible = True,
                .Formula = "",
                .Width = 100
            }
            tmpListOfColumns.Add(NameColumn)

            Dim PathColumn As New PropertyColumn With {
                .Name = "Path",
                .Visible = True,
                .Formula = "",
                .Width = 100
            }
            tmpListOfColumns.Add(PathColumn)

            'CLB_Properties.SetItemChecked(0, True)
            'CLB_Properties.SetItemChecked(1, True)

            Me.ListOfColumns = tmpListOfColumns ' Triggers a Property update

        End If

        For i As Integer = 0 To Me.ListOfColumns.Count - 1
            Dim PropColumn As PropertyColumn = ListOfColumns(i)
            CLB_Properties.SetItemChecked(i, PropColumn.Visible)
        Next

        Dim TemplateList = {Me.AssemblyTemplate, Me.PartTemplate, Me.SheetmetalTemplate, Me.DraftTemplate}.ToList

        'Me.TemplatePropertyDict = UC.TemplatePropertyDictPopulate(TemplateList, Me.TemplatePropertyDict)

        Me.PropertiesData = New PropertiesData  ' Automatically loads saved settings if any.

        'Me.PresetsListJSON = UP.GetPresetsListJSON
        'If Me.PresetsListJSON Is Nothing Then
        '    Me.PresetsListJSON = New List(Of String)
        'End If
        Me.Presets = New Presets

        Me.PropertyFilters = New PropertyFilters  ' Automatically loads saved settings if any.

        UD.BuildReadmeFile()

        CarIcona()


        '###### INITIALIZE FILE LIST IF NEEDED ######

        Dim NewWay As Boolean = True

        If NewWay Then

            If ListViewFiles.Groups.Count = 0 Then

                Dim GroupHeaderNames As New List(Of String)
                GroupHeaderNames.AddRange({"Files sources", "Excluded files", "Assemblies", "Parts", "Sheetmetals", "Drafts"})

                Dim GroupNames As New List(Of String)
                GroupNames.AddRange({"Sources", "Excluded", ".asm", ".par", ".psm", ".dft"})

                For i As Integer = 0 To GroupHeaderNames.Count - 1
                    Dim LVGroup As New ListViewGroup(GroupHeaderNames(i), HorizontalAlignment.Left)
                    LVGroup.Name = GroupNames(i)
                    ListViewFiles.Groups.Add(LVGroup)
                Next

            End If

        Else
            'If ListViewFiles.Groups.Count = 0 Then

            '    Dim ListViewGroup1 As New ListViewGroup("Files sources", HorizontalAlignment.Left)
            '    ListViewGroup1.Name = "Sources"
            '    Dim ListViewGroup2 As New ListViewGroup("Excluded files", HorizontalAlignment.Left)
            '    ListViewGroup2.Name = "Excluded"
            '    Dim ListViewGroup3 As New ListViewGroup("Assemblies", HorizontalAlignment.Left)
            '    ListViewGroup3.Name = ".asm"
            '    Dim ListViewGroup4 As New ListViewGroup("Parts", HorizontalAlignment.Left)
            '    ListViewGroup4.Name = ".par"
            '    Dim ListViewGroup5 As New ListViewGroup("Sheetmetals", HorizontalAlignment.Left)
            '    ListViewGroup5.Name = ".psm"
            '    Dim ListViewGroup6 As New ListViewGroup("Drafts", HorizontalAlignment.Left)
            '    ListViewGroup6.Name = ".dft"
            '    ListViewFiles.Groups.Add(ListViewGroup1)
            '    ListViewFiles.Groups.Add(ListViewGroup2)
            '    ListViewFiles.Groups.Add(ListViewGroup3)
            '    ListViewFiles.Groups.Add(ListViewGroup4)
            '    ListViewFiles.Groups.Add(ListViewGroup5)
            '    ListViewFiles.Groups.Add(ListViewGroup6)

            'End If

        End If

        ListViewFiles.SetGroupState(ListViewGroupState.Collapsible)

        ListViewFilesOutOfDate = False

        ' Form title
        Me.Text = String.Format("Solid Edge Housekeeper {0}", Me.Version)


        '###### INITIALIZE TASK LIST ######

        Me.TaskList = UP.GetTaskList

        Dim tmpTaskPanel As Panel = Nothing

        For Each c As Control In TabPageTasks.Controls
            If c.Name = "TaskPanel" Then
                tmpTaskPanel = CType(c, Panel)
                Exit For
            End If
        Next

        If NewWay Then
            tmpTaskPanel.Controls.Clear()
        End If

        For i = TaskList.Count - 1 To 0 Step -1

            Dim Task = TaskList(i)

            If Not Me.RememberTasks Then
                Task.IsSelectedTask = False
                Task.IsSelectedAssembly = False
                Task.IsSelectedPart = False
                Task.IsSelectedSheetmetal = False
                Task.IsSelectedDraft = False
            End If

            If Task.RequiresPropertiesData Then
                Select Case Task.Name
                    Case "TaskEditProperties"
                        Dim T = CType(Task, TaskEditProperties)
                        'T.TemplatePropertyDict = Me.TemplatePropertyDict
                        T.PropertiesData = Me.PropertiesData
                    Case "TaskEditVariables"
                        Dim T = CType(Task, TaskEditVariables)
                        'T.TemplatePropertyDict = Me.TemplatePropertyDict
                        T.PropertiesData = Me.PropertiesData
                    Case "TaskSaveDrawingAs"
                        Dim T = CType(Task, TaskSaveDrawingAs)
                        'T.TemplatePropertyDict = Me.TemplatePropertyDict
                        T.PropertiesData = Me.PropertiesData
                    Case "TaskSaveModelAs"
                        Dim T = CType(Task, TaskSaveModelAs)
                        'T.TemplatePropertyDict = Me.TemplatePropertyDict
                        T.PropertiesData = Me.PropertiesData
                    Case Else
                        MsgBox(String.Format("PropertiesData not added to {0} in Form_Main.Startup()", Task.Name))
                End Select
            End If

            tmpTaskPanel.Controls.Add(Task.TaskControl)
        Next

        AddHandler editbox.Leave, AddressOf editbox_LostFocus
        AddHandler editbox.KeyUp, AddressOf editbox_KeyUp


        '################# Questo risolver il problema del bordo sgrazinato della ToolStrip
        ToolStrip_Filter.Renderer = New MySR()
        ToolStripPresets.Renderer = New MySR()
        '################# rif: https://stackoverflow.com/questions/1918247/how-to-disable-the-line-under-tool-strip-in-winform-c




        UP.CheckVersionFormat(Me.Version)

        If Me.CheckForNewerVersion Then
            UP.CheckForNewerVersion(Me.Version)
        End If

        'new_ButtonPropertyFilter.Enabled = CheckBoxEnablePropertyFilter.Checked
        'ComboBoxFileWildcard.Enabled = CheckBoxEnableFileWildcard.Checked

        Me.Cursor = Cursors.Default

    End Sub



    Public Sub SaveSettings()
        ' Set Properties equal to themselves to trigger JSON updates

        ' ListOfColumnsJSON
        ' Updating directly to not trigger an uneeded file list update.
        Me.TextBoxStatus.Text = "Updating JSON ListOfColumns"

        Dim tmpListOfColumnsJSON As New List(Of String)

        For Each PropColumn In Me.ListOfColumns
            tmpListOfColumnsJSON.Add(PropColumn.ToJSON)
        Next

        Dim UC As New UtilsCommon
        If Not UC.CompareListOfJSON(tmpListOfColumnsJSON, Me.ListOfColumnsJSON) Then
            Me.ListOfColumnsJSON = tmpListOfColumnsJSON
        End If


        ' Other JSON
        Me.TextBoxStatus.Text = "Updating JSON TemplatePropertyDict"
        'Me.TemplatePropertyDict = Me.TemplatePropertyDict
        Me.TextBoxStatus.Text = "Updating JSON PropertyFilterDict"
        Me.PropertyFilterDict = Me.PropertyFilterDict
        Me.TextBoxStatus.Text = "Updating JSON PresetsList"
        'Me.PresetsList = Me.PresetsList
        Me.Presets = Me.Presets


        ' Save settings
        Dim UP As New UtilsPreferences
        Me.TextBoxStatus.Text = "Saving settings"
        UP.SaveFormMainSettings(Me)
        Me.TextBoxStatus.Text = "Saving tasks"
        UP.SaveTaskList(Me.TaskList)
        Me.TextBoxStatus.Text = "Saving presets"
        'UP.SavePresetsListJSON(Me.PresetsListJSON)
        Me.Presets.Save()
        Me.TextBoxStatus.Text = "Saving properties data"
        Me.PropertiesData.Save()
        Me.TextBoxStatus.Text = "Saving property filters"
        Me.PropertyFilters.Save()

        Me.TextBoxStatus.Text = ""

    End Sub

    Private Sub CopyTemplatesToTasks()
        Dim s As String = ""
        For Each Task As Task In Me.TaskList

            Dim TaskType As Type = Task.GetType

            If Task.RequiresAssemblyTemplate Then
                If TypeOf Task Is TaskUpdateModelStylesFromTemplate Then
                    Dim T = CType(Task, TaskUpdateModelStylesFromTemplate)
                    If T.UseConfigurationPageTemplates Then
                        T.AssemblyTemplate = Me.AssemblyTemplate
                    End If
                Else
                    s = String.Format("{0}RequiresAssemblyTemplate {1}{2}", s, TaskType.ToString, vbCrLf)
                End If
            End If
            If Task.RequiresPartTemplate Then
                If TypeOf Task Is TaskUpdateModelStylesFromTemplate Then
                    Dim T = CType(Task, TaskUpdateModelStylesFromTemplate)
                    If T.UseConfigurationPageTemplates Then
                        T.PartTemplate = Me.PartTemplate
                    End If
                Else
                    s = String.Format("{0}RequiresPartTemplate {1}{2}", s, TaskType.ToString, vbCrLf)
                End If
            End If
            If Task.RequiresSheetmetalTemplate Then
                If TypeOf Task Is TaskUpdateModelStylesFromTemplate Then
                    Dim T = CType(Task, TaskUpdateModelStylesFromTemplate)
                    If T.UseConfigurationPageTemplates Then
                        T.SheetmetalTemplate = Me.SheetmetalTemplate
                    End If
                Else
                    s = String.Format("{0}RequiresSheetmetalTemplate {1}{2}", s, TaskType.ToString, vbCrLf)
                End If
            End If
            If Task.RequiresDraftTemplate Then
                If TypeOf Task Is TaskUpdateDrawingStylesFromTemplate Then
                    Dim T = CType(Task, TaskUpdateDrawingStylesFromTemplate)
                    If T.UseConfigurationPageTemplates Then
                        T.DraftTemplate = Me.DraftTemplate
                    End If
                ElseIf TypeOf Task Is TaskCreateDrawingOfFlatPattern Then
                    Dim T = CType(Task, TaskCreateDrawingOfFlatPattern)
                    If T.UseConfigurationPageTemplates Then
                        T.DraftTemplate = Me.DraftTemplate
                    End If
                Else
                    s = String.Format("{0}RequiresDraftTemplate {1}{2}", s, TaskType.ToString, vbCrLf)
                End If
            End If
            If Task.RequiresMaterialTable Then
                If TypeOf Task Is TaskCheckMaterialNotInMaterialTable Then
                    Dim T = CType(Task, TaskCheckMaterialNotInMaterialTable)
                    If T.UseConfigurationPageTemplates Then
                        T.MaterialTable = Me.MaterialTable
                    End If
                ElseIf TypeOf Task Is TaskEditProperties Then
                    Dim T = CType(Task, TaskEditProperties)
                    If T.UseConfigurationPageTemplates Then
                        T.MaterialTable = Me.MaterialTable
                    End If
                ElseIf TypeOf Task Is TaskUpdateMaterialFromMaterialTable Then
                    Dim T = CType(Task, TaskUpdateMaterialFromMaterialTable)
                    If T.UseConfigurationPageTemplates Then
                        T.MaterialTable = Me.MaterialTable
                    End If
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


    ' **************** CONTROLS ****************

    ' BUTTONS


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        If ButtonCancel.Text = "Stop" Then
            StopProcess = True
        Else
            SaveSettings()

            ' Shut down
            Try
                End
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As EventArgs) Handles Me.FormClosing
        SaveSettings()

        '############ Uncollapse the groups to not throw the exception, not ideal but works 'F.Arfilli
        'For Each item As ListViewGroup In ListViewFiles.Groups
        '    ListViewFiles.SetGroupState(ListViewGroupState.Normal, item)
        'Next

        ' Shut down
        Try
            End '########## <------- This throws an error if some ListView groups are collapsed 'F.Arfilli
        Catch ex As Exception
        End Try

        '##### 16/10/24 It seems the error doesn't occur anymore

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

        'tf = Me.TemplatePropertyDict Is Nothing
        ''tf = tf Or Me.TemplatePropertyList Is Nothing

        'If Not tf Then
        '    tf = Me.TemplatePropertyDict.Count = 0
        '    'tf = tf Or Me.TemplatePropertyList.Count = 0
        'End If

        tf = Me.PropertiesData Is Nothing

        If Not tf Then tf = Me.PropertiesData.Items.Count = 0

        If Not tf Then
            Dim FPF As New FormPropertyFilter

            FPF.PropertyFilterDict = Me.PropertyFilterDict
            FPF.PropertyFilters = Me.PropertyFilters
            FPF.ShowDialog()

            If FPF.DialogResult = DialogResult.OK Then
                Me.PropertyFilterDict = FPF.PropertyFilterDict
                Me.PropertyFilters = FPF.PropertyFilters
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
        Me.FilterAsm = new_CheckBoxFilterAsm.Checked
        ListViewFilesOutOfDate = True
    End Sub

    Private Sub new_CheckBoxFilterPar_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxFilterPar.CheckedChanged
        Me.FilterPar = new_CheckBoxFilterPar.Checked
        ListViewFilesOutOfDate = True
    End Sub

    Private Sub new_CheckBoxFilterPsm_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxFilterPsm.CheckedChanged
        Me.FilterPsm = new_CheckBoxFilterPsm.Checked
        ListViewFilesOutOfDate = True
    End Sub

    Private Sub new_CheckBoxFilterDft_CheckedChanged(sender As Object, e As EventArgs) Handles new_CheckBoxFilterDft.CheckedChanged
        Me.FilterDft = new_CheckBoxFilterDft.Checked
        ListViewFilesOutOfDate = True
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
        CaricaImmagine16x16(TabPage_ImageList, "Query", My.Resources.Query)

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
                    content += tmpItem.Name ' & vbCrLf

                    For Each subItem As ListViewItem.ListViewSubItem In tmpItem.SubItems
                        If subItem.Bounds.Width <> 0 Then content += "," & subItem.Text
                    Next

                    content += vbCrLf

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
            If Me.FilterAsm Then extFilter.Add(".asm")
            If Me.FilterPar Then extFilter.Add(".par")
            If Me.FilterPsm Then extFilter.Add(".psm")
            If Me.FilterDft Then extFilter.Add(".dft")

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

        CheckBoxUseDMForStatusChanges.Enabled = Me.ProcessAsAvailable
        RadioButtonProcessAsAvailableRevert.Enabled = Me.ProcessAsAvailable
        RadioButtonProcessAsAvailableChange.Enabled = Me.ProcessAsAvailable
        If RadioButtonProcessAsAvailableChange.Checked Then
            For Each RB In StatusChangeRadioButtons
                RB.Enabled = Me.ProcessAsAvailable
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
                If Task.RequiresPropertiesData Then
                    Select Case Task.Name
                        Case "TaskEditProperties"
                            Dim T = CType(Task, TaskEditProperties)
                            'T.TemplatePropertyDict = Me.TemplatePropertyDict
                            T.PropertiesData = Me.PropertiesData
                        Case "TaskEditVariables"
                            Dim T = CType(Task, TaskEditVariables)
                            'T.TemplatePropertyDict = Me.TemplatePropertyDict
                            T.PropertiesData = Me.PropertiesData
                        Case "TaskSaveDrawingAs"
                            Dim T = CType(Task, TaskSaveDrawingAs)
                            'T.TemplatePropertyDict = Me.TemplatePropertyDict
                            T.PropertiesData = Me.PropertiesData
                        Case "TaskSaveModelAs"
                            Dim T = CType(Task, TaskSaveModelAs)
                            'T.TemplatePropertyDict = Me.TemplatePropertyDict
                            T.PropertiesData = Me.PropertiesData
                        Case Else
                            MsgBox(String.Format("PropertiesData not added to {0} in Form_Main.EditTaskListButton_Click()", Task.Name))
                    End Select
                End If
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
            CopyTemplatesToTasks()
            'TextBoxAssemblyTemplate.Text = Me.AssemblyTemplate
        End If

    End Sub

    Private Sub ButtonPartTemplate_Click(sender As Object, e As EventArgs) Handles ButtonPartTemplate.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a part template"
        tmpFileDialog.Filter = "Part Template|*.par"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.PartTemplate = tmpFileDialog.FileName
            CopyTemplatesToTasks()
            'TextBoxPartTemplate.Text = Me.PartTemplate
        End If

    End Sub

    Private Sub ButtonSheetmetalTemplate_Click(sender As Object, e As EventArgs) Handles ButtonSheetmetalTemplate.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a sheetmetal template"
        tmpFileDialog.Filter = "Sheetmetal Template|*.psm"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.SheetmetalTemplate = tmpFileDialog.FileName
            CopyTemplatesToTasks()
            'TextBoxSheetmetalTemplate.Text = Me.SheetmetalTemplate
        End If

    End Sub

    Private Sub ButtonDraftTemplate_Click(sender As Object, e As EventArgs) Handles ButtonDraftTemplate.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a draft template"
        tmpFileDialog.Filter = "Draft Template|*.dft"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.DraftTemplate = tmpFileDialog.FileName
            CopyTemplatesToTasks()
            'TextBoxDraftTemplate.Text = Me.DraftTemplate
        End If

    End Sub

    Private Sub ButtonMaterialTable_Click(sender As Object, e As EventArgs) Handles ButtonMaterialTable.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a material table"
        tmpFileDialog.Filter = "Material Table|*.mtl"

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.MaterialTable = tmpFileDialog.FileName
            CopyTemplatesToTasks()
            'TextBoxMaterialTable.Text = Me.MaterialTable
        End If

    End Sub

    Private Sub ButtonUpdatePropertiesData_Click(sender As Object, e As EventArgs) Handles ButtonUpdatePropertiesData.Click

        Dim UC As New UtilsCommon

        Dim TemplateList = {Me.AssemblyTemplate, Me.PartTemplate, Me.SheetmetalTemplate, Me.DraftTemplate}.ToList

        Me.Cursor = Cursors.WaitCursor

        'Me.TemplatePropertyDict = UC.TemplatePropertyDictPopulate(TemplateList, Me.TemplatePropertyDict)

        Me.PropertiesData.Populate(TemplateList)

        Me.Cursor = Cursors.Default

        ButtonCustomizePropertiesData.PerformClick()

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

    Private Sub ButtonCustomizePropertiesData_Click(sender As Object, e As EventArgs) Handles ButtonCustomizePropertiesData.Click

        Dim FPLC As New FormPropertyListCustomize

        Dim Result As DialogResult = FPLC.ShowDialog()

        If Result = DialogResult.OK Then
            Dim UC As New UtilsCommon
            'Me.TemplatePropertyDict = UC.TemplatePropertyDictUpdateFavorites(Me.TemplatePropertyDict, FPLC.FavoritesList)

            Me.PropertiesData.UpdateFavorites(FPLC.FavoritesList)

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
            Me.StatusAtoX = 0
        End If
    End Sub
    Private Sub RadioButtonStatusAtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoB.CheckedChanged
        If RadioButtonStatusAtoB.Checked Then
            Me.StatusAtoX = 4
        End If
    End Sub
    Private Sub RadioButtonStatusAtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoIR.CheckedChanged
        If RadioButtonStatusAtoIR.Checked Then
            Me.StatusAtoX = 2
        End If
    End Sub
    Private Sub RadioButtonStatusAtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoIW.CheckedChanged
        If RadioButtonStatusAtoIW.Checked Then
            Me.StatusAtoX = 1
        End If
    End Sub
    Private Sub RadioButtonStatusAtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoO.CheckedChanged
        If RadioButtonStatusAtoO.Checked Then
            Me.StatusAtoX = 5
        End If
    End Sub
    Private Sub RadioButtonStatusAtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoR.CheckedChanged
        If RadioButtonStatusAtoR.Checked Then
            Me.StatusAtoX = 3
        End If
    End Sub

    Private Sub RadioButtonStatusBtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoA.CheckedChanged
        If RadioButtonStatusBtoA.Checked Then
            Me.StatusBtoX = 0
        End If
    End Sub
    Private Sub RadioButtonStatusBtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoB.CheckedChanged
        If RadioButtonStatusBtoB.Checked Then
            Me.StatusBtoX = 4
        End If
    End Sub
    Private Sub RadioButtonStatusBtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoIR.CheckedChanged
        If RadioButtonStatusBtoIR.Checked Then
            Me.StatusBtoX = 2
        End If
    End Sub
    Private Sub RadioButtonStatusBtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoIW.CheckedChanged
        If RadioButtonStatusBtoIW.Checked Then
            Me.StatusBtoX = 1
        End If
    End Sub
    Private Sub RadioButtonStatusBtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoO.CheckedChanged
        If RadioButtonStatusBtoO.Checked Then
            Me.StatusBtoX = 5
        End If
    End Sub
    Private Sub RadioButtonStatusBtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoR.CheckedChanged
        If RadioButtonStatusBtoR.Checked Then
            Me.StatusBtoX = 3
        End If
    End Sub

    Private Sub RadioButtonStatusIRtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoA.CheckedChanged
        If RadioButtonStatusIRtoA.Checked Then
            Me.StatusIRtoX = 0
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoB.CheckedChanged
        If RadioButtonStatusIRtoB.Checked Then
            Me.StatusIRtoX = 4
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoIR.CheckedChanged
        If RadioButtonStatusIRtoIR.Checked Then
            Me.StatusIRtoX = 2
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoIW.CheckedChanged
        If RadioButtonStatusIRtoIW.Checked Then
            Me.StatusIRtoX = 1
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoO.CheckedChanged
        If RadioButtonStatusIRtoO.Checked Then
            Me.StatusIRtoX = 5
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoR.CheckedChanged
        If RadioButtonStatusIRtoR.Checked Then
            Me.StatusIRtoX = 3
        End If
    End Sub

    Private Sub RadioButtonStatusIWtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoA.CheckedChanged
        If RadioButtonStatusIWtoA.Checked Then
            Me.StatusIWtoX = 0
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoB.CheckedChanged
        If RadioButtonStatusIWtoB.Checked Then
            Me.StatusIWtoX = 4
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoIR.CheckedChanged
        If RadioButtonStatusIWtoIR.Checked Then
            Me.StatusIWtoX = 2
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoIW.CheckedChanged
        If RadioButtonStatusIWtoIW.Checked Then
            Me.StatusIWtoX = 1
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoO.CheckedChanged
        If RadioButtonStatusIWtoO.Checked Then
            Me.StatusIWtoX = 5
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoR.CheckedChanged
        If RadioButtonStatusIWtoR.Checked Then
            Me.StatusIWtoX = 3
        End If
    End Sub

    Private Sub RadioButtonStatusOtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoA.CheckedChanged
        If RadioButtonStatusOtoA.Checked Then
            Me.StatusOtoX = 0
        End If
    End Sub
    Private Sub RadioButtonStatusOtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoB.CheckedChanged
        If RadioButtonStatusOtoB.Checked Then
            Me.StatusOtoX = 4
        End If
    End Sub
    Private Sub RadioButtonStatusOtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoIR.CheckedChanged
        If RadioButtonStatusOtoIR.Checked Then
            Me.StatusOtoX = 2
        End If
    End Sub
    Private Sub RadioButtonStatusOtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoIW.CheckedChanged
        If RadioButtonStatusOtoIW.Checked Then
            Me.StatusOtoX = 1
        End If
    End Sub
    Private Sub RadioButtonStatusOtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoO.CheckedChanged
        If RadioButtonStatusOtoO.Checked Then
            Me.StatusOtoX = 5
        End If
    End Sub
    Private Sub RadioButtonStatusOtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoR.CheckedChanged
        If RadioButtonStatusOtoR.Checked Then
            Me.StatusOtoX = 3
        End If
    End Sub

    Private Sub RadioButtonStatusRtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoA.CheckedChanged
        If RadioButtonStatusRtoA.Checked Then
            Me.StatusRtoX = 0
        End If
    End Sub
    Private Sub RadioButtonStatusRtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoB.CheckedChanged
        If RadioButtonStatusRtoB.Checked Then
            Me.StatusRtoX = 4
        End If
    End Sub
    Private Sub RadioButtonStatusRtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoIR.CheckedChanged
        If RadioButtonStatusRtoIR.Checked Then
            Me.StatusRtoX = 2
        End If
    End Sub
    Private Sub RadioButtonStatusRtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoIW.CheckedChanged
        If RadioButtonStatusRtoIW.Checked Then
            Me.StatusRtoX = 1
        End If
    End Sub
    Private Sub RadioButtonStatusRtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoO.CheckedChanged
        If RadioButtonStatusRtoO.Checked Then
            Me.StatusRtoX = 5
        End If
    End Sub
    Private Sub RadioButtonStatusRtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoR.CheckedChanged
        If RadioButtonStatusRtoR.Checked Then
            Me.StatusRtoX = 3
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

    Private Sub ButtonCopyToTasks_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BT_ColumnsSelect_Click(sender As Object, e As EventArgs) Handles BT_ColumnsSelect.Click

        ColumnSelectionPanel.Visible = True

    End Sub

    Private Sub CLB_Properties_MouseMove(sender As Object, e As MouseEventArgs) Handles CLB_Properties.MouseMove

        Dim itemIndex As Integer = CLB_Properties.IndexFromPoint(e.Location)
        If itemIndex > 1 Then

            Dim tmpPoint As Point = CLB_Properties.GetItemRectangle(itemIndex).Location
            tmpPoint.X += CLB_Properties.GetItemRectangle(itemIndex).Width - BT_DeleteCLBItem.Width

            BT_DeleteCLBItem.Location = tmpPoint
            BT_DeleteCLBItem.Tag = itemIndex
            BT_DeleteCLBItem.Visible = True
        Else
            BT_DeleteCLBItem.Visible = False
        End If

    End Sub

    Private Sub BT_DeleteCLBItem_Click(sender As Object, e As EventArgs) Handles BT_DeleteCLBItem.Click

        ListViewFiles.Columns.RemoveAt(CInt(BT_DeleteCLBItem.Tag))
        CLB_Properties.Items.RemoveAt(CInt(BT_DeleteCLBItem.Tag))

        Me.ListOfColumns.RemoveAt(CInt(BT_DeleteCLBItem.Tag))

    End Sub

    Private Sub ButtonCloseListOfColumns_Click(sender As Object, e As EventArgs) Handles ButtonCloseListOfColumns.Click

        Dim tmpButton As Button = DirectCast(sender, Button)

        If CStr(tmpButton.Tag) = "Dirty" Then

            Dim tmpListOfColumns As New List(Of PropertyColumn)
            For Each PropColumn In Me.ListOfColumns
                tmpListOfColumns.Add(PropColumn)
            Next
            Me.ListOfColumns = tmpListOfColumns  ' Trigger update
            Dim UFL As New UtilsFileList(Me, ListViewFiles)
            Me.Cursor = Cursors.WaitCursor
            UFL.UpdatePropertiesColumns()
            Me.Cursor = Cursors.Default
            ButtonCloseListOfColumns.Tag = ""

        End If

        ColumnSelectionPanel.Visible = False


    End Sub

    Private Sub ButtonAddToListOfColumns_Click(sender As Object, e As EventArgs) Handles ButtonAddToListOfColumns.Click

        Dim FPP As New FormPropertyPicker

        FPP.ButtonPropAndIndex.Enabled = False

        FPP.ShowDialog()

        If FPP.DialogResult = DialogResult.OK Then

            Dim UC As New UtilsCommon

            Dim PropFormula As String = FPP.PropertyString
            Dim PropName As String = UC.PropNameFromFormula(PropFormula)

            Dim tmpColumn As New PropertyColumn
            tmpColumn.Name = PropName
            tmpColumn.Visible = True
            tmpColumn.Formula = PropFormula
            tmpColumn.Width = 100

            If Not ListOfColumns.Contains(tmpColumn) Then

                ListOfColumns.Add(tmpColumn)
                CLB_Properties.Items.Add(tmpColumn.Name, tmpColumn.Visible)

                ButtonCloseListOfColumns.Tag = "Dirty"

            End If

        End If

        'Me.ListOfColumns = Me.ListOfColumns

    End Sub

    Private Sub CLB_Properties_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CLB_Properties.ItemCheck

        For Each item In Me.ListOfColumns ' tmpListOfColumns

            If item.Name = CLB_Properties.Items(e.Index).ToString Then
                item.Visible = CType(e.NewValue, Boolean)

                If ListViewFiles.Columns.Count > e.Index Then 'Insert this check in case of adding a new property the column doesn't exists yet
                    If Not item.Visible Then
                        ListViewFiles.Columns.Item(e.Index).Width = 0
                    Else
                        ListViewFiles.Columns.Item(e.Index).Width = item.Width  'AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
                    End If
                End If

                Exit For
            End If

        Next

    End Sub

    Private Sub ListViewFiles_ColumnWidthChanged(sender As Object, e As ColumnWidthChangedEventArgs) Handles ListViewFiles.ColumnWidthChanged

        If Not IsNothing(ListOfColumns) Then

            If ListOfColumns.Count > e.ColumnIndex Then

                If ListOfColumns.Item(e.ColumnIndex).Width <> ListViewFiles.Columns.Item(e.ColumnIndex).Width Then 'We don't want to fire an event if the value is not changed

                    If ListViewFiles.Columns.Item(e.ColumnIndex).Width <> 0 Then 'We don't want to store a value of 0, column visibility is a different property

                        ListOfColumns.Item(e.ColumnIndex).Width = ListViewFiles.Columns.Item(e.ColumnIndex).Width

                    End If
                End If
            End If

        End If

    End Sub

    Private Sub ListViewFiles_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListViewFiles.MouseDoubleClick

        If ListViewFiles.SelectedItems.Count > 0 And e.Button = MouseButtons.Left Then

            Dim mousePos As Point = ListViewFiles.PointToClient(Control.MousePosition)
            hitinfo = ListViewFiles.HitTest(mousePos)

            Dim columnIndex As Integer = hitinfo.Item.SubItems.IndexOf(hitinfo.SubItem)

            If columnIndex > 1 Then 'We don't want to be able to edit Name and Path columns

                Dim ListViewFile As ListViewItem = hitinfo.Item

                editbox.Parent = ListViewFiles

                editbox.Bounds = hitinfo.SubItem.Bounds
                editbox.Text = hitinfo.SubItem.Text
                editbox.Show()
                editbox.SelectionStart = editbox.TextLength
                editbox.SelectedText = ""
                editbox.SelectionLength = 0
                editbox.Focus()

            End If

        End If

    End Sub

    Private Sub editbox_LostFocus(sender As Object, e As EventArgs)

        Dim UC As New UtilsCommon

        Dim columnIndex As Integer = hitinfo.Item.SubItems.IndexOf(hitinfo.SubItem)
        Dim PropertySet As String = ""
        Dim PropertySetConstant As PropertyData.PropertySetNameConstants = PropertyData.PropertySetNameConstants.System
        Dim PropertyNameEnglish = ""
        Dim tmpPropertyData As PropertyData

        Dim PropertyName As String = hitinfo.Item.ListView.Columns.Item(columnIndex).Text

        ' Template propertydict doesn't contain manually added properties, a method that adds them to the dictionary is needed
        ' Done 20241015 -Robert
        'Try
        '    PropertySet = TemplatePropertyDict(PropertyName)("PropertySet")
        '    PropertyNameEnglish = TemplatePropertyDict(PropertyName)("EnglishName")
        'Catch ex As Exception
        '    MsgBox(String.Format("In editbox_LostFocus, TemplatePropertyDict key {0} not found", PropertyName))
        '    PropertySet = "Custom"
        '    PropertyNameEnglish = hitinfo.Item.ListView.Columns.Item(columnIndex).Text
        'End Try

        tmpPropertyData = Me.PropertiesData.GetPropertyData(PropertyName)
        If tmpPropertyData IsNot Nothing Then
            PropertySetConstant = tmpPropertyData.PropertySetName
            Select Case PropertySetConstant
                Case PropertyData.PropertySetNameConstants.Custom
                    PropertySet = "Custom"
                Case PropertyData.PropertySetNameConstants.System
                    PropertySet = "System"
            End Select

            PropertyNameEnglish = tmpPropertyData.EnglishName
        Else
            MsgBox(String.Format("In editbox_LostFocus, PropertyData {0} not found", PropertyName))
            PropertySet = "Custom"
            PropertyNameEnglish = hitinfo.Item.ListView.Columns.Item(columnIndex).Text
        End If

        Dim FullName As String = hitinfo.Item.Name 'File to edit
        'hitinfo.Item.SubItems.IndexOf(hitinfo.SubItem) 'Property index to edit
        'hitinfo.SubItem.Text 'New value

        If UC.SetOLEPropValue(FullName, PropertySet, PropertyNameEnglish, editbox.Text) Then
            hitinfo.SubItem.Text = editbox.Text
            hitinfo.SubItem.BackColor = Color.Empty
        End If

        editbox.Hide()

    End Sub

    Private Sub editbox_KeyUp(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter Then

            ListViewFiles.Focus()

        End If

    End Sub

    Private Sub ListViewFiles_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles ListViewFiles.ColumnClick

        If IsNothing(lvwColumnSorter) Then
            lvwColumnSorter = New ListViewColumnSorter()
            ListViewFiles.ListViewItemSorter = lvwColumnSorter

        End If

        If (e.Column = lvwColumnSorter.SortColumn) Then
            ' Reverse the current sort direction for this column.
            If (lvwColumnSorter.Order = SortOrder.Ascending) Then
                lvwColumnSorter.Order = SortOrder.Descending
            Else
                lvwColumnSorter.Order = SortOrder.Ascending
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending.
            lvwColumnSorter.SortColumn = e.Column
            lvwColumnSorter.Order = SortOrder.Ascending
        End If

        ' Perform the sort with these new sort options.
        ListViewFiles.Sort()

    End Sub

    Private Sub CheckBoxUseDMForStatusChanges_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxUseDMForStatusChanges.CheckedChanged
        Me.UseDMForStatusChanges = CheckBoxUseDMForStatusChanges.Checked
    End Sub

    Private Sub ListViewFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewFiles.SelectedIndexChanged

        If ListViewFiles.SelectedItems.Count > 0 Then
            ButtonProcess.Text = "    Process selected"
        Else
            ButtonProcess.Text = "Process"
        End If

    End Sub

    Private Sub TextBoxServerConnectionString_TextChanged(sender As Object, e As EventArgs) Handles TextBoxServerConnectionString.TextChanged
        ServerConnectionString = TextBoxServerConnectionString.Text
    End Sub

    'Private Sub TextBoxServerQuery_TextChanged(sender As Object, e As EventArgs) Handles TextBoxServerQuery.TextChanged
    '    ServerQuery = TextBoxServerQuery.Text
    'End Sub

    Private Sub FastColoredServerQuery_TextChanged(sender As Object, e As FastColoredTextBoxNS.TextChangedEventArgs) Handles FastColoredServerQuery.TextChanged
        ServerQuery = FastColoredServerQuery.Text
    End Sub

    Private Sub ComboBoxPresetName_LostFocus(sender As Object, e As EventArgs) Handles ComboBoxPresetName.LostFocus
        Dim s As String = ComboBoxPresetName.Text

        If Not s = "" Then
            If Not ComboBoxPresetName.Items.Contains(s) Then
                ComboBoxPresetName.Items.Add(s)
            End If
        End If
    End Sub

    Private Sub ComboBoxPresetName_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ButtonPresetLoad_Click(sender As Object, e As EventArgs) Handles ButtonPresetLoad.Click

        Dim Name As String = ComboBoxPresetName.Text
        Dim tmpPreset As Preset = Nothing
        Dim GotAMatch As Boolean = False

        If Not Name = "" Then
            Dim UP As New UtilsPreferences

            'For Each tmpPreset In Me.PresetsList
            '    If tmpPreset.Name = Name Then
            '        GotAMatch = True
            '        Exit For
            '    End If
            'Next
            For Each tmpPreset In Me.Presets.Items
                If tmpPreset.Name = Name Then
                    GotAMatch = True
                    Exit For
                End If
            Next

            If GotAMatch Then
                UP.SaveFormMainSettingsJSON(tmpPreset.FormSettingsJSON)
                UP.SaveTaskListJSON(tmpPreset.TaskListJSON)
                'UP.SavePresetsListJSON(Me.PresetsListJSON)
                Presets.Save()
                'SaveSettings()  ' Incorrect.  This saves the current settings

                Application.DoEvents()
                Startup()
            End If

        Else
            MsgBox("Enter a name of the preset to load", vbOKOnly)
        End If


    End Sub

    Private Sub ButtonPresetSave_Click(sender As Object, e As EventArgs) Handles ButtonPresetSave.Click

        Dim Name As String = ComboBoxPresetName.Text
        Dim tmpPreset As Preset = Nothing
        Dim GotAMatch As Boolean = False

        If Not Name = "" Then
            Dim UP As New UtilsPreferences

            'For Each tmpPreset In Me.PresetsList
            '    If tmpPreset.Name = Name Then
            '        Dim Result As MsgBoxResult = MsgBox(String.Format("The preset '{0}' already exists.  Do you want to overwrite it?", Name), vbYesNo)
            '        If Result = MsgBoxResult.No Then
            '            Exit Sub
            '        Else
            '            GotAMatch = True
            '            Exit For
            '        End If
            '    End If
            'Next
            For Each tmpPreset In Me.Presets.Items
                If tmpPreset.Name = Name Then
                    Dim Result As MsgBoxResult = MsgBox(String.Format("The preset '{0}' already exists.  Do you want to overwrite it?", Name), vbYesNo)
                    If Result = MsgBoxResult.No Then
                        Exit Sub
                    Else
                        GotAMatch = True
                        Exit For
                    End If
                End If
            Next

            If Not GotAMatch Then
                tmpPreset = New Preset
            End If

            SaveSettings()  ' Updates the task list and form properties to their current state.

            tmpPreset.Name = Name
            tmpPreset.TaskListJSON = UP.GetTaskListJSON()
            tmpPreset.FormSettingsJSON = UP.GetFormMainSettingsJSON

            If Not GotAMatch Then
                'Me.PresetsList.Add(tmpPreset)
                Me.Presets.Items.Add(tmpPreset)
            End If

            'Me.PresetsList = Me.PresetsList ' Trigger update
            Me.Presets = Me.Presets ' Trigger update

            'SaveSettings()
            'Application.DoEvents()
            'UP.SavePresetsListJSON(Me.PresetsListJSON)
            Presets.Save()

        Else
            MsgBox("Enter a name for the preset to save", vbOKOnly)
        End If


    End Sub

    Private Sub ButtonPresetDelete_Click(sender As Object, e As EventArgs) Handles ButtonPresetDelete.Click

        Dim Name As String = ComboBoxPresetName.Text
        Dim Idx As Integer = -1
        Dim UP As New UtilsPreferences

        'For i As Integer = 0 To Me.PresetsList.Count - 1
        '    If Me.PresetsList(i).Name = Name Then
        '        Idx = i
        '        Exit For
        '    End If
        'Next
        For i As Integer = 0 To Me.Presets.Items.Count - 1
            If Me.Presets.Items(i).Name = Name Then
                Idx = i
                Exit For
            End If
        Next

        'If Not Idx = -1 Then
        '    Me.PresetsList.RemoveAt(Idx)
        '    Me.PresetsList = Me.PresetsList ' Trigger update
        '    UP.SavePresetsListJSON(Me.PresetsListJSON)
        'End If
        If Not Idx = -1 Then
            Me.Presets.Items.RemoveAt(Idx)
            Me.Presets = Me.Presets ' Trigger update
            'UP.SavePresetsListJSON(Me.PresetsListJSON)
            Presets.Save()
        End If

        If ComboBoxPresetName.Items.Contains(Name) Then
            ComboBoxPresetName.Items.Remove(Name)
        End If
        ComboBoxPresetName.Text = ""

    End Sub


    Public Shared Function ExecuteQuery(cf As CompoundFile, FullName As String, Query As String) As String

        ExecuteQuery = ""

        Dim UC As New UtilsCommon
        'Dim Q = UC.SubstitutePropertyFormula(Nothing, cf, Nothing, FullName, Query, False, Form_Main.TemplatePropertyDict)
        Dim Q = UC.SubstitutePropertyFormula(Nothing, cf, Nothing, FullName, Query, False, Form_Main.PropertiesData)


        Try
            'TBD Determine the type of DB, if its a SQL it need a different connection type

            If Form_Main.TextBoxServerConnectionString.Text.Contains("OLEDB") Then

                Dim con As New OleDbConnection(Form_Main.TextBoxServerConnectionString.Text)
                con.Open()

                Dim cmd As New OleDbCommand(Q, con) 'TBD <--- Convert the property formula into text
                Dim reader As OleDbDataReader = cmd.ExecuteReader()

                If reader.HasRows Then
                    reader.Read()

                    ExecuteQuery = reader(0).ToString

                End If

                reader.Close()
                con.Close()

            Else

                Dim con As New SqlConnection(Form_Main.TextBoxServerConnectionString.Text)
                con.Open()

                Dim cmd As New SqlCommand(Q, con)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                If reader.HasRows Then
                    reader.Read()

                    ExecuteQuery = reader(0).ToString

                End If

                reader.Close()
                con.Close()

            End If

        Catch ex As Exception

            ExecuteQuery = ex.Message

        End Try

    End Function

    Private Sub TextBoxServerQuery_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Public Class MySR
        Inherits ToolStripSystemRenderer

        Public Sub New()
        End Sub

        Protected Overrides Sub OnRenderToolStripBorder(ByVal e As ToolStripRenderEventArgs)
        End Sub
    End Class


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

    'System.IO.File.Copy(SettingsFilename, NewSettingsFilename)

    '_PropertyFilterDictJSON = JsonConvert.SerializeObject(Me.PropertyFilterDict) Then

    'PropertyFilterDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(_PropertyFilterDictJSON)

End Class


Public Class PropertyColumn
    Property Name As String
    Property Visible As Boolean
    Property Formula As String
    Property Width As Integer

    Public Function ToJSON() As String
        Dim JSONString As String
        Dim tmpList As New List(Of String)

        tmpList.AddRange({Name, CStr(Visible), Formula, CStr(Width)})

        JSONString = JsonConvert.SerializeObject(tmpList)

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)
        Dim tmpList As New List(Of String)

        tmpList = JsonConvert.DeserializeObject(Of List(Of String))(JSONString)

        Name = tmpList(0)
        Visible = CBool(tmpList(1))
        Formula = tmpList(2)
        Width = CInt(tmpList(3))

    End Sub
End Class

Public Class ListViewColumnSorter
    Implements System.Collections.IComparer

    Private ColumnToSort As Integer
    Private OrderOfSort As SortOrder
    Private ObjectCompare As CaseInsensitiveComparer

    Public Sub New()
        ' Initialize the column to '0'.
        ColumnToSort = 0

        ' Initialize the sort order to 'none'.
        OrderOfSort = SortOrder.Unspecified

        ' Initialize the CaseInsensitiveComparer object.
        ObjectCompare = New CaseInsensitiveComparer()
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim compareResult As Integer
        Dim listviewX As ListViewItem
        Dim listviewY As ListViewItem

        ' Cast the objects to be compared to ListViewItem objects.
        listviewX = CType(x, ListViewItem)
        listviewY = CType(y, ListViewItem)

        Dim Text_X As String = ""
        Dim Text_Y As String = ""

        If listviewX.SubItems.Count > ColumnToSort Then Text_X = listviewX.SubItems(ColumnToSort).Text
        If listviewY.SubItems.Count > ColumnToSort Then Text_Y = listviewY.SubItems(ColumnToSort).Text

        ' Compare the two items.
        compareResult = ObjectCompare.Compare(Text_X, Text_Y)

        ' Calculate the correct return value based on the object 
        ' comparison.
        If (OrderOfSort = SortOrder.Ascending) Then
            ' Ascending sort is selected, return typical result of 
            ' compare operation.
            Return compareResult
        ElseIf (OrderOfSort = SortOrder.Descending) Then
            ' Descending sort is selected, return negative result of 
            ' compare operation.
            Return (-compareResult)
        Else
            ' Return '0' to indicate that they are equal.
            Return 0
        End If
    End Function

    Public Property SortColumn() As Integer
        Set(ByVal Value As Integer)
            ColumnToSort = Value
        End Set

        Get
            Return ColumnToSort
        End Get
    End Property

    Public Property Order() As SortOrder
        Set(ByVal Value As SortOrder)
            OrderOfSort = Value
        End Set

        Get
            Return OrderOfSort
        End Get
    End Property
End Class


Public Class Presets
    Public Property Items As List(Of Preset)

    Public Sub New()

        Me.Items = New List(Of Preset)

        Dim UP As New UtilsPreferences

        Dim JSONString As String
        Dim Infile As String = UP.GetPresetsFilename(CheckExisting:=True)

        If Not Infile = "" Then
            JSONString = IO.File.ReadAllText(Infile)

            Dim tmpList As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(JSONString)

            For Each PresetJSONString As String In tmpList
                Dim Item As New Preset
                Item.FromJSON(PresetJSONString)
                Me.Items.Add(Item)
            Next
        End If

    End Sub

    Public Sub Save()

        Dim UP As New UtilsPreferences
        Dim Outfile As String = UP.GetPresetsFilename(CheckExisting:=False)

        Dim tmpList As New List(Of String)
        For Each Item As Preset In Me.Items
            tmpList.Add(Item.ToJSON)
        Next

        Dim JSONString As String = JsonConvert.SerializeObject(tmpList)
        IO.File.WriteAllText(Outfile, JSONString)

    End Sub
End Class

Public Class Preset
    Public Property Name As String
    Public Property TaskListJSON As String
    Public Property FormSettingsJSON As String

    Public Sub New()

    End Sub

    Public Function ToJSON() As String

        Dim JSONString As String = Nothing

        Dim tmpPresetDict As New Dictionary(Of String, String)

        tmpPresetDict("Name") = Me.Name
        tmpPresetDict("TaskListJSON") = Me.TaskListJSON
        tmpPresetDict("FormSettingJSON") = Me.FormSettingsJSON

        JSONString = JsonConvert.SerializeObject(tmpPresetDict)

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)

        Dim tmpPresetDict As Dictionary(Of String, String)

        tmpPresetDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

        Me.Name = tmpPresetDict("Name")
        Me.TaskListJSON = tmpPresetDict("TaskListJSON")
        Me.FormSettingsJSON = tmpPresetDict("FormSettingJSON")

    End Sub

End Class


Public Class PropertiesData

    Public Property Items As List(Of PropertyData)

    Public Sub New()

        Items = New List(Of PropertyData)

        Dim UP As New UtilsPreferences
        Dim Infile As String = UP.GetPropertiesDataFilename(CheckExisting:=True)

        If Not Infile = "" Then
            Dim JSONString As String = IO.File.ReadAllText(Infile)

            Dim tmpList As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(JSONString)

            For Each PropertyDataJSON As String In tmpList
                Dim tmpPropertyData As New PropertyData
                tmpPropertyData.FromJSON(PropertyDataJSON)
                Items.Add(tmpPropertyData)
            Next

        End If
    End Sub

    Public Sub Save()

        Dim UP As New UtilsPreferences
        Dim Outfile As String = UP.GetPropertiesDataFilename(CheckExisting:=False)

        Dim JSONString As String
        Dim tmpList As New List(Of String)

        For Each Item As PropertyData In Me.Items
            tmpList.Add(Item.ToJSON)
        Next

        JSONString = JsonConvert.SerializeObject(tmpList)

        IO.File.WriteAllText(Outfile, JSONString)

    End Sub

    Public Function GetFavoritesList() As List(Of String)

        Dim FavoritesList As New List(Of String)
        Dim FavoritesArray(Me.Items.Count) As String
        Dim Idx As Integer

        For Each Item As PropertyData In Me.Items
            Idx = Item.FavoritesListIdx
            If Not Idx = -1 Then
                FavoritesArray(Idx) = Item.Name
            End If
        Next

        For i As Integer = 0 To FavoritesArray.Count - 1
            If Not FavoritesArray(i) = "" Then
                FavoritesList.Add(FavoritesArray(i))
            End If
        Next

        Return FavoritesList

    End Function

    Public Function GetAvailableList() As List(Of String)

        Dim AvailableList As New List(Of String)

        For Each Item As PropertyData In Me.Items
            AvailableList.Add(Item.Name)
        Next

        Return AvailableList

    End Function

    Public Function GetPropertyData(Name As String, Optional tmpItems As List(Of PropertyData) = Nothing) As PropertyData

        Dim tmpProperty As PropertyData = Nothing

        If tmpItems Is Nothing Then
            For Each Item As PropertyData In Items
                If Item.Name.ToLower = Name.ToLower Then
                    tmpProperty = Item
                    Exit For
                End If
            Next
        Else
            For Each Item As PropertyData In tmpItems
                If Item.Name.ToLower = Name.ToLower Then
                    tmpProperty = Item
                    Exit For
                End If
            Next
        End If

        Return tmpProperty
    End Function

    Public Sub Populate(TemplateList As List(Of String))

        'Dim tmpPropertiesData As New PropertiesData
        Dim tmpItems As New List(Of PropertyData)

        Dim PreviousFavoritesList As List(Of String) = GetFavoritesList()

        '{
        '"Titolo":{
        '    "PropertySet":"System",                         'System', 'Custom', 'Duplicate'
        '    "PropertySetActualName":"SummaryInformation",   'SummaryInformation', 'ExtendedSummaryInformation', ...
        '    "AsmPropItemNumber":"1",
        '    "ParPropItemNumber":"1",
        '    "PsmPropItemNumber":"1",
        '    "DftPropItemNumber":"1",
        '    "EnglishName":"Title",
        '    "PropertySource":"Auto",                         Was property added automatically or manually?:  "Auto", "Manual"
        '    "FavoritesListIdx":"-1"},                        If on the Favorites list, 'list index number', otherwise '-1'

        Dim PropertySets As SolidEdgeFileProperties.PropertySets
        Dim PropertySet As SolidEdgeFileProperties.Properties
        Dim Prop As SolidEdgeFileProperties.Property
        PropertySets = New SolidEdgeFileProperties.PropertySets
        Dim PropertySetActualName As String
        Dim PropertySetHousekeeperName As String
        Dim PropName As String = ""
        Dim DocType As String
        Dim PropTypeName As String

        Dim tf As Boolean

        Dim tmpPropertyData As PropertyData
        Dim tmpPreviousPropertyData As PropertyData

        Dim UC As New UtilsCommon


        ' ###### PROPERTIES TO PROCESS ######

        Dim KeepDict As New Dictionary(Of String, List(Of String))
        KeepDict("SummaryInformation") = {"Title", "Subject", "Author", "Keywords", "Comments"}.ToList
        KeepDict("ExtendedSummaryInformation") = {"Status", "Hardware"}.ToList
        KeepDict("DocumentSummaryInformation") = {"Category", "Manager", "Company"}.ToList
        KeepDict("ProjectInformation") = {"Document Number", "Revision", "Project Name"}.ToList
        KeepDict("MechanicalModeling") = {"Material", "Sheet Metal Gage"}.ToList

        ' Do Custom last to deal with duplicates
        KeepDict("Custom") = New List(Of String)


        ' ###### PROCESS TEMPLATES ######

        For Each TemplateName As String In TemplateList
            tf = Not TemplateName = ""
            tf = tf And FileIO.FileSystem.FileExists(TemplateName)
            If tf Then

                DocType = IO.Path.GetExtension(TemplateName).Replace(".", "")  ' 'C:\project\part.par' -> 'par'

                PropertySets.Open(TemplateName, True)


                ' ###### PROCESS PROPERTY SETS ######

                For Each PropertySetActualName In KeepDict.Keys

                    If PropertySetActualName = "Custom" Then
                        PropertySetHousekeeperName = "Custom"
                    Else
                        PropertySetHousekeeperName = "System"
                    End If

                    ' Not all file types have all property sets.
                    Try
                        PropertySet = CType(PropertySets.Item(PropertySetActualName), SolidEdgeFileProperties.Properties)
                    Catch ex As Exception
                        Continue For
                    End Try

                    For i = 0 To PropertySet.Count - 1

                        ' ###### PROCESS PROPERTY ######

                        Try
                            ' ###### GET THE PROPERTY OBJECT ######

                            Prop = CType(PropertySet.Item(i), SolidEdgeFileProperties.Property)
                            PropName = Prop.Name

                            Dim EnglishName As String = UC.PropLocalizedToEnglish(PropertySetActualName, i + 1, DocType)
                            If EnglishName = "" Then EnglishName = PropName

                            ' Skip unwanted properties
                            If Not PropertySetActualName = "Custom" Then
                                If KeepDict.Keys.Contains(PropertySetActualName) Then
                                    If Not KeepDict(PropertySetActualName).Contains(EnglishName) Then
                                        Continue For
                                    End If
                                End If
                            End If

                            PropTypeName = Microsoft.VisualBasic.Information.TypeName(Prop.Value)

                            tmpPropertyData = GetPropertyData(PropName, tmpItems)

                            If tmpPropertyData Is Nothing Then


                                ' ###### PROPERTY NOT FOUND: ADD AND POPULATE IT ######

                                tmpPropertyData = New PropertyData
                                tmpItems.Add(tmpPropertyData)

                                tmpPropertyData.Name = PropName

                                Select Case PropertySetHousekeeperName
                                    Case "System"
                                        tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.System
                                    Case "Custom"
                                        tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.Custom
                                    Case Else
                                        Dim s As String = String.Format("In PropertiesDataPopulate, PropertySet '{0}' not recognized", PropertySetHousekeeperName)
                                        MsgBox(s, vbOKOnly)
                                End Select

                                tmpPropertyData.EnglishName = EnglishName
                                tmpPropertyData.PropertySetActualName = PropertySetActualName
                                tmpPropertyData.AsmIdx = -1
                                tmpPropertyData.ParIdx = -1
                                tmpPropertyData.PsmIdx = -1
                                tmpPropertyData.DftIdx = -1
                                tmpPropertyData.PropertySource = PropertyData.PropertySourceConstants.Auto
                                tmpPropertyData.FavoritesListIdx = -1

                                Select Case PropTypeName
                                    Case "String"
                                        tmpPropertyData.TypeName = PropertyData.TypeNameConstants._String
                                    Case "Integer"
                                        tmpPropertyData.TypeName = PropertyData.TypeNameConstants._Integer
                                    Case "Boolean"
                                        tmpPropertyData.TypeName = PropertyData.TypeNameConstants._Boolean
                                    Case "Date"
                                        tmpPropertyData.TypeName = PropertyData.TypeNameConstants._Date
                                    Case Else
                                        Dim s As String = String.Format("In PropertiesDataPopulate, PropTypeName '{0}' not recognized", PropTypeName)
                                        MsgBox(s, vbOKOnly)
                                End Select

                            Else

                                ' ###### PROPERTY FOUND: CHECK IF DUPLICATE ######

                                tf = PropertySetActualName = "Custom"
                                tf = tf And (Not tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.Custom)
                                If tf Then
                                    tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.Duplicate
                                End If

                            End If


                            ' ###### SET THE PROPERTY INDEX FOR LOCALIZED PROPERTY MAPPING ######

                            If Not PropertySetActualName = "Custom" Then
                                Select Case DocType
                                    Case "asm"
                                        tmpPropertyData.AsmIdx = i + 1
                                    Case "par"
                                        tmpPropertyData.ParIdx = i + 1
                                    Case "psm"
                                        tmpPropertyData.PsmIdx = i + 1
                                    Case "dft"
                                        tmpPropertyData.DftIdx = i + 1
                                End Select
                            End If

                        Catch ex As Exception
                            Dim s = "Error building PropertiesData: "
                            s = String.Format("{0} DocType '{1}', PropertySetName '{2}', Item Number '{3}', PropName '{4}'", s, DocType, PropertySetActualName, i + 1, PropName)
                            MsgBox(s, vbOKOnly)
                        End Try
                    Next
                Next

                PropertySets.Close()

            End If
            'TemplateIdx += 1
        Next


        ' ###### ADD OTHER KNOWN AND SPECIAL PROPERTIES ######

        ' Sheet Metal Gage

        PropName = "Sheet Metal Gage"

        tmpPropertyData = GetPropertyData(PropName)

        If tmpPropertyData Is Nothing Then

            tmpPropertyData = New PropertyData
            tmpItems.Add(tmpPropertyData)

            tmpPropertyData.Name = PropName
            tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.System
            tmpPropertyData.PropertySetActualName = "MechanicalModeling"
            tmpPropertyData.AsmIdx = -1
            tmpPropertyData.ParIdx = -1
            tmpPropertyData.PsmIdx = 2
            tmpPropertyData.DftIdx = -1
            tmpPropertyData.EnglishName = PropName
            tmpPropertyData.PropertySource = PropertyData.PropertySourceConstants.Auto
            tmpPropertyData.FavoritesListIdx = -1
            tmpPropertyData.TypeName = PropertyData.TypeNameConstants._String

        Else
            tmpItems.Add(tmpPropertyData)

        End If


        ' Special File Properties

        Dim PropNames = {"File Name", "File Name (full path)", "File Name (no extension)"}.ToList
        For Each PropName In PropNames

            tmpPropertyData = GetPropertyData(PropName)

            If tmpPropertyData Is Nothing Then

                tmpPropertyData = New PropertyData
                tmpItems.Add(tmpPropertyData)

                tmpPropertyData.Name = PropName
                tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.System
                tmpPropertyData.PropertySetActualName = "System"
                tmpPropertyData.AsmIdx = -1
                tmpPropertyData.ParIdx = -1
                tmpPropertyData.PsmIdx = -1
                tmpPropertyData.DftIdx = -1
                tmpPropertyData.EnglishName = PropName
                tmpPropertyData.PropertySource = PropertyData.PropertySourceConstants.Auto
                tmpPropertyData.FavoritesListIdx = -1
                tmpPropertyData.TypeName = PropertyData.TypeNameConstants._String

            Else
                tmpItems.Add(tmpPropertyData)
            End If
        Next


        ' ###### UPDATE FAVORITES ######

        For i As Integer = 0 To PreviousFavoritesList.Count - 1
            PropName = PreviousFavoritesList(i)

            ' Check if it is already in tmpItems
            tmpPropertyData = GetPropertyData(PropName, tmpItems)

            If tmpPropertyData IsNot Nothing Then

                ' If so, update FavoritesListIdx
                tmpPropertyData.FavoritesListIdx = i

            Else

                ' If not in tmpItems, but in Items, add it.
                tmpPreviousPropertyData = GetPropertyData(PropName)

                If tmpPreviousPropertyData IsNot Nothing Then
                    tmpPropertyData = New PropertyData
                    tmpItems.Add(tmpPropertyData)

                    tmpPropertyData.Name = PropName
                    tmpPropertyData.PropertySetName = tmpPreviousPropertyData.PropertySetName
                    tmpPropertyData.PropertySetActualName = tmpPreviousPropertyData.PropertySetActualName
                    tmpPropertyData.AsmIdx = tmpPreviousPropertyData.AsmIdx
                    tmpPropertyData.ParIdx = tmpPreviousPropertyData.ParIdx
                    tmpPropertyData.PsmIdx = tmpPreviousPropertyData.PsmIdx
                    tmpPropertyData.DftIdx = tmpPreviousPropertyData.DftIdx
                    tmpPropertyData.EnglishName = tmpPreviousPropertyData.EnglishName
                    tmpPropertyData.PropertySource = tmpPreviousPropertyData.PropertySource
                    tmpPropertyData.TypeName = tmpPreviousPropertyData.TypeName
                    tmpPropertyData.FavoritesListIdx = i
                End If
            End If

        Next

        Me.Items = tmpItems

    End Sub

    Public Sub UpdateFavorites(FavoritesList As List(Of String))

        Dim PropName As String
        Dim tmpPropertyData As PropertyData

        For Each Item As PropertyData In Me.Items
            Item.FavoritesListIdx = -1
        Next

        For idx As Integer = 0 To FavoritesList.Count - 1
            PropName = FavoritesList(idx)
            tmpPropertyData = GetPropertyData(PropName)
            If tmpPropertyData IsNot Nothing Then
                tmpPropertyData.FavoritesListIdx = idx
            End If
        Next

    End Sub

    Public Sub AddProp(
        PropertySetActualName As String,
        PropertyName As String,
        EnglishName As String,
        FavoritesListIdx As Integer)

        Dim tmpPropertyData As PropertyData

        Dim SystemPropertyList As New List(Of String)
        SystemPropertyList.AddRange({"SummaryInformation", "ExtendedSummaryInformation", "DocumentSummaryInformation"})
        SystemPropertyList.AddRange({"ProjectInformation", "MechanicalModeling"})

        Dim PropertySet As PropertyData.PropertySetNameConstants
        Dim OriginalPropertySet As PropertyData.PropertySetNameConstants
        Dim s As String

        If SystemPropertyList.Contains(PropertySetActualName) Then
            PropertySet = PropertyData.PropertySetNameConstants.System
        Else
            PropertySet = PropertyData.PropertySetNameConstants.Custom
        End If

        tmpPropertyData = GetPropertyData(PropertyName)

        If tmpPropertyData Is Nothing Then

            tmpPropertyData = New PropertyData
            Me.Items.Add(tmpPropertyData)

            tmpPropertyData.Name = PropertyName
            tmpPropertyData.PropertySetName = PropertySet
            tmpPropertyData.PropertySetActualName = PropertySetActualName
            tmpPropertyData.AsmIdx = -1
            tmpPropertyData.ParIdx = -1
            tmpPropertyData.PsmIdx = -1
            tmpPropertyData.DftIdx = -1
            tmpPropertyData.EnglishName = EnglishName
            tmpPropertyData.PropertySource = PropertyData.PropertySourceConstants.Manual
            tmpPropertyData.FavoritesListIdx = FavoritesListIdx
            tmpPropertyData.TypeName = PropertyData.TypeNameConstants._Unknown

        Else
            OriginalPropertySet = tmpPropertyData.PropertySetName
            If Not PropertySet = OriginalPropertySet Then
                s = String.Format("The list already contains '{0}' in the '{1}' property set. ", PropertyName, OriginalPropertySet)
                s = String.Format("{0}Do you want to add the new property set '{1}'?", s, PropertySet)
                Dim Result As MsgBoxResult = MsgBox(s, vbYesNo)
                If Result = vbYes Then
                    tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.Duplicate
                    tmpPropertyData.EnglishName = EnglishName
                    tmpPropertyData.PropertySource = PropertyData.PropertySourceConstants.Manual
                    tmpPropertyData.FavoritesListIdx = FavoritesListIdx
                End If

            End If
        End If

    End Sub
End Class

Public Class PropertyData

    ' ###### English mapping from UC.PropLocalizedToEnglish            ######
    ' ###### In case of a duplicate, PropertySet will be 'Duplicate'   ######
    ' ###### and ItemNumbers will be for the 'System' property.        ######

    Public Property Name As String
    Public Property EnglishName As String
    Public Property PropertySetName As PropertySetNameConstants
    Public Property PropertySetActualName As String
    Public Property TypeName As TypeNameConstants
    Public Property AsmIdx As Integer
    Public Property ParIdx As Integer
    Public Property PsmIdx As Integer
    Public Property DftIdx As Integer
    Public Property PropertySource As PropertySourceConstants
    Public Property FavoritesListIdx As Integer

    Public Enum PropertySetNameConstants
        System
        Custom
        Duplicate
        'Server
    End Enum

    'Public Enum PropertySetActualNameConstants
    '    SummaryInformation
    '    ExtendedSummaryInformation
    '    DocumentSummaryInformation
    '    ProjectInformation
    '    MechanicalModeling
    '    Custom
    'End Enum

    Public Enum TypeNameConstants
        ' Preceeding names with '_' to avoid VB reserved keywords
        _String
        _Integer
        _Boolean
        _Date
        _Unknown
    End Enum

    Public Enum PropertySourceConstants
        Auto
        Manual
    End Enum

    Public Sub New()

    End Sub


    Public Function ToJSON() As String

        Dim JSONString As String

        Dim tmpDict As New Dictionary(Of String, String)

        tmpDict("Name") = Me.Name
        tmpDict("EnglishName") = Me.EnglishName
        tmpDict("PropertySetName") = CStr(CInt(Me.PropertySetName)) ' "0", "1"
        tmpDict("PropertySetActualName") = Me.PropertySetActualName
        tmpDict("TypeName") = CStr(CInt(Me.TypeName))
        tmpDict("AsmPropItemNumber") = CStr(Me.AsmIdx)
        tmpDict("ParPropItemNumber") = CStr(Me.ParIdx)
        tmpDict("PsmPropItemNumber") = CStr(Me.PsmIdx)
        tmpDict("DftPropItemNumber") = CStr(Me.DftIdx)
        tmpDict("PropertySource") = CStr(CInt(Me.PropertySource))
        tmpDict("FavoritesListIdx") = CStr(Me.FavoritesListIdx)

        JSONString = JsonConvert.SerializeObject(tmpDict)

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)

        Dim tmpDict As Dictionary(Of String, String)

        tmpDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

        Me.Name = tmpDict("Name")
        Me.EnglishName = tmpDict("EnglishName")
        Me.PropertySetName = CType(CInt(tmpDict("PropertySetName")), PropertySetNameConstants)
        Me.PropertySetActualName = tmpDict("PropertySetActualName")
        Me.TypeName = CType(CInt(tmpDict("TypeName")), TypeNameConstants)
        Me.AsmIdx = CInt(tmpDict("AsmPropItemNumber"))
        Me.ParIdx = CInt(tmpDict("ParPropItemNumber"))
        Me.PsmIdx = CInt(tmpDict("PsmPropItemNumber"))
        Me.DftIdx = CInt(tmpDict("DftPropItemNumber"))
        Me.PropertySource = CType(CInt(tmpDict("PropertySource")), PropertySourceConstants)
        Me.FavoritesListIdx = CInt(tmpDict("FavoritesListIdx"))

    End Sub

End Class

Public Class PropertyFilters
    Public Property Items As List(Of PropertyFilter)

    Public Sub New()
        Dim UP As New UtilsPreferences
        Dim Infile As String = UP.GetPropertyFiltersFilename(CheckExisting:=True)
        Dim JSONString As String

        If Not Infile = "" Then
            JSONString = IO.File.ReadAllText(Infile)
            FromJSON(JSONString)
        Else
            Me.Items = New List(Of PropertyFilter)
        End If

    End Sub

    Public Sub New(JSONString As String)
        Me.Items = New List(Of PropertyFilter)
        FromJSON(JSONString)
    End Sub

    Public Sub Save()
        Dim UP As New UtilsPreferences
        Dim JSONString As String
        Dim Outfile As String

        Outfile = UP.GetPropertyFiltersFilename(CheckExisting:=False)
        JSONString = ToJSON()

        IO.File.WriteAllText(Outfile, JSONString)
    End Sub

    Public Function GetActivePropertyFilter() As PropertyFilter
        Dim tmpPropertyFilter As PropertyFilter = Nothing

        For Each Item As PropertyFilter In Me.Items
            If Item.IsActiveFilter Then
                tmpPropertyFilter = Item
                Exit For
            End If
        Next

        Return tmpPropertyFilter
    End Function

    Public Function GetPropertyFilter(Name As String) As PropertyFilter
        Dim tmpPropertyFilter As PropertyFilter = Nothing

        For Each Item As PropertyFilter In Me.Items
            If Item.Name = Name Then
                tmpPropertyFilter = Item
                Exit For
            End If
        Next

        Return tmpPropertyFilter
    End Function

    Public Function ToJSON() As String
        Dim JSONString As String

        Dim tmpItemsList As New List(Of String)

        For Each Item As PropertyFilter In Items
            tmpItemsList.Add(Item.ToJSON)
        Next

        JSONString = JsonConvert.SerializeObject(tmpItemsList)

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)
        Dim tmpItemsList As List(Of String)

        tmpItemsList = JsonConvert.DeserializeObject(Of List(Of String))(JSONString)

        If Me.Items Is Nothing Then
            Me.Items = New List(Of PropertyFilter)
        End If

        Me.Items.Clear()

        For Each ItemJSON In tmpItemsList
            Dim P As New PropertyFilter(ItemJSON)
            Me.Items.Add(P)
        Next
    End Sub

End Class

Public Class PropertyFilter
    Public Property Name As String
    Public Property Formula As String  ' "A AND NOT ( B OR C )", etc.
    Public Property Conditions As List(Of PropertyFilterCondition)
    Public Property IsActiveFilter As Boolean

    Public Sub New()
        Me.Conditions = New List(Of PropertyFilterCondition)
    End Sub

    Public Sub New(JSONString As String)
        Me.Conditions = New List(Of PropertyFilterCondition)
        FromJSON(JSONString)
    End Sub

    Public Function ToJSON() As String
        Dim JSONString As String

        Dim tmpConditionsList As New List(Of String)
        Dim tmpConditionsListJSON As String
        Dim tmpDict As New Dictionary(Of String, String)

        tmpDict("Name") = Me.Name
        tmpDict("Formula") = Me.Formula
        tmpDict("IsActiveFilter") = CStr(Me.IsActiveFilter)

        For Each Condition As PropertyFilterCondition In Me.Conditions
            tmpConditionsList.Add(Condition.ToJSON)
        Next

        tmpConditionsListJSON = JsonConvert.SerializeObject(tmpConditionsList)

        tmpDict("ConditionsListJSON") = tmpConditionsListJSON

        JSONString = JsonConvert.SerializeObject(tmpDict)

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)
        Dim tmpConditionsList As List(Of String)
        Dim tmpConditionsListJSON As String
        Dim tmpDict As Dictionary(Of String, String)

        tmpDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

        Me.Name = tmpDict("Name")
        Me.Formula = tmpDict("Formula")
        Me.IsActiveFilter = CBool(tmpDict("IsActiveFilter"))

        tmpConditionsListJSON = tmpDict("ConditionsListJSON")
        tmpConditionsList = JsonConvert.DeserializeObject(Of List(Of String))(tmpConditionsListJSON)

        Me.Conditions.Clear()

        For Each ConditionJSON As String In tmpConditionsList
            Dim C As New PropertyFilterCondition(ConditionJSON)
            Me.Conditions.Add(C)
        Next

    End Sub

    Public Sub SortConditions()

        Dim tmpConditionsDict As New Dictionary(Of String, PropertyFilterCondition)
        Dim tmpVariableNameList As New List(Of String)
        Dim Name As String

        For Each C As PropertyFilterCondition In Me.Conditions
            Name = C.VariableName
            If Not tmpConditionsDict.Keys.Contains(Name) Then
                tmpConditionsDict(Name) = C
            Else
                MsgBox(String.Format("Duplicate variable name '{0}' found in PropertyFilter Conditions", Name), vbOKOnly)
                Exit Sub
            End If
        Next

        tmpVariableNameList = tmpConditionsDict.Keys.ToList

        tmpVariableNameList.Sort()

        Me.Conditions.Clear()

        For i As Integer = 0 To tmpVariableNameList.Count - 1
            Name = tmpVariableNameList(i)
            Dim C As PropertyFilterCondition = tmpConditionsDict(Name)
            Dim NewName As String = Chr(i + 65)
            C.VariableName = NewName
            Me.Conditions.Add(C)
        Next

    End Sub

End Class

Public Class PropertyFilterCondition
    ' PropertyFilterDict format:
    '{"0":
    '    {"Variable":"A",
    '     "PropertySet":"Custom",
    '     "PropertyName":"hmk_Part_Number",
    '     "Comparison":"contains",
    '     "Value":"aluminum",
    '     "Formula":" A AND B "},
    ' "1":
    '...
    '}

    Public Property VariableName As String  ' "A", "B", etc.  Used in property filter formulas.
    Public Property PropertySetName As PropertySetNameConstants
    Public Property PropertySetActualName As String  ' "SummaryInformation", "Custom", etc.
    Public Property PropertyName As String  ' "Title", "Titolo", etc.
    Public Property EnglishName As String  ' "Title", etc.
    Public Property Comparison As ComparisonConstants
    Public Property Value As String  ' "aluminum", "%{System.Material|R1}", etc.

    Public Enum PropertySetNameConstants
        Custom
        System
        Duplicate
    End Enum

    Public Enum ComparisonConstants
        Contains
        IsExactly
        WildcardMatch
        RegexMatch
        GreaterThan
        LessThan
    End Enum

    Public Sub New()

    End Sub

    Public Sub New(JSONString As String)
        FromJSON(JSONString)
    End Sub

    Public Function ToJSON() As String
        Dim JSONString As String
        Dim tmpComparisonDict As New Dictionary(Of String, String)

        tmpComparisonDict("VariableName") = Me.VariableName
        tmpComparisonDict("PropertySetName") = CStr(CInt(Me.PropertySetName))
        tmpComparisonDict("PropertySetActualName") = Me.PropertySetActualName
        tmpComparisonDict("PropertyName") = Me.PropertyName
        tmpComparisonDict("EnglishName") = Me.EnglishName
        tmpComparisonDict("Comparison") = CStr(CInt(Me.Comparison))
        tmpComparisonDict("Value") = Me.Value

        JSONString = JsonConvert.SerializeObject(tmpComparisonDict)

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)

        Dim tmpComparisonDict As Dictionary(Of String, String)
        tmpComparisonDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

        Me.VariableName = tmpComparisonDict("VariableName")
        Me.PropertySetName = CType(CInt(tmpComparisonDict("PropertySetName")), PropertySetNameConstants)
        Me.PropertySetActualName = tmpComparisonDict("PropertySetActualName")
        Me.PropertyName = tmpComparisonDict("PropertyName")
        Me.EnglishName = tmpComparisonDict("EnglishName")
        Me.Comparison = CType(CInt(tmpComparisonDict("Comparison")), ComparisonConstants)
        Me.Value = tmpComparisonDict("Value")

    End Sub
End Class