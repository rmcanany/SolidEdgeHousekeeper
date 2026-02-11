Option Strict On

Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports ListViewExtended
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports Newtonsoft.Json


Public Class Form_Main

    Public Property Version As String = "2025.4"  ' Two fields, both integers: Year.ReleaseNumber.  Can include a bugfix number which is ignored
    Public Property PreviewVersion As String = "35" ' ######### Empty string for a release

    Private lvwColumnSorter As ListViewColumnSorter

    Private editbox As New TextBox()
    Private hitinfo As ListViewHitTestInfo

    Public Property StopProcess As Boolean

    Public DragDropCache As New List(Of ListViewItem)
    Public DragDropCacheExcluded As New List(Of ListViewItem)


    Private _ListViewFilesOutOfDate As Boolean
    Public Property ListViewFilesOutOfDate As Boolean
        Get
            Return _ListViewFilesOutOfDate
        End Get
        Set(value As Boolean)
            _ListViewFilesOutOfDate = value
            If Me.TabControl1 IsNot Nothing And Not RunningStartup Then
                If ListViewFilesOutOfDate Then
                    BT_Update.BackColor = Color.Orange

                    If Me.RemindFilelistUpdate Then
                        Dim s As String = String.Format("The file list is out of date.{0}", vbCrLf)
                        s = String.Format("{0}When you are done with setup, press the orange Update button to populate the list.{1}{1}", s, vbCrLf)
                        s = String.Format("{0}(Disable this message on the Configuration Tab -- General Page)", s, vbCrLf)
                        MsgBox(s, vbOKOnly)
                    End If


                Else
                    BT_Update.BackColor = Color.FromName("Control")
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

    Private _LinkManagementFilename As String
    Public Property LinkManagementFilename As String
        Get
            Return _LinkManagementFilename
        End Get
        Set(value As String)
            _LinkManagementFilename = value

            Dim UP As New UtilsPreferences
            Me.LinkManagementOrder = UP.GetLinkManagementOrder() ' Defaluts to {"CONTAINER", "RELATIVE", "ABSOLUTE"}
            If Me.TabControl1 IsNot Nothing Then
                TextBoxLinkManagementFilename.Text = value
            End If
        End Set
    End Property

    Public Property LinkManagementOrder As List(Of String)


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

    Private _StatusAtoX As String
    Public Property StatusAtoX As String
        Get
            Return _StatusAtoX
        End Get
        Set(value As String)
            _StatusAtoX = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case "Available"
                        RadioButtonStatusAtoA.Checked = True
                    Case "InWork"
                        RadioButtonStatusAtoIW.Checked = True
                    Case "InReview"
                        RadioButtonStatusAtoIR.Checked = True
                    Case "Released"
                        RadioButtonStatusAtoR.Checked = True
                    Case "Baselined"
                        RadioButtonStatusAtoB.Checked = True
                    Case "Obsolete"
                        RadioButtonStatusAtoO.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusBtoX As String
    Public Property StatusBtoX As String
        Get
            Return _StatusBtoX
        End Get
        Set(value As String)
            _StatusBtoX = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case "Available"
                        RadioButtonStatusBtoA.Checked = True
                    Case "InWork"
                        RadioButtonStatusBtoIW.Checked = True
                    Case "InReview"
                        RadioButtonStatusBtoIR.Checked = True
                    Case "Released"
                        RadioButtonStatusBtoR.Checked = True
                    Case "Baselined"
                        RadioButtonStatusBtoB.Checked = True
                    Case "Obsolete"
                        RadioButtonStatusBtoO.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusIRtoX As String
    Public Property StatusIRtoX As String
        Get
            Return _StatusIRtoX
        End Get
        Set(value As String)
            _StatusIRtoX = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case "Available"
                        RadioButtonStatusIRtoA.Checked = True
                    Case "InWork"
                        RadioButtonStatusIRtoIW.Checked = True
                    Case "InReview"
                        RadioButtonStatusIRtoIR.Checked = True
                    Case "Released"
                        RadioButtonStatusIRtoR.Checked = True
                    Case "Baselined"
                        RadioButtonStatusIRtoB.Checked = True
                    Case "Obsolete"
                        RadioButtonStatusIRtoO.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusIWtoX As String
    Public Property StatusIWtoX As String
        Get
            Return _StatusIWtoX
        End Get
        Set(value As String)
            _StatusIWtoX = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case "Available"
                        RadioButtonStatusIWtoA.Checked = True
                    Case "InWork"
                        RadioButtonStatusIWtoIW.Checked = True
                    Case "InReview"
                        RadioButtonStatusIWtoIR.Checked = True
                    Case "Released"
                        RadioButtonStatusIWtoR.Checked = True
                    Case "Baselined"
                        RadioButtonStatusIWtoB.Checked = True
                    Case "Obsolete"
                        RadioButtonStatusIWtoO.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusOtoX As String
    Public Property StatusOtoX As String
        Get
            Return _StatusOtoX
        End Get
        Set(value As String)
            _StatusOtoX = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case "Available"
                        RadioButtonStatusOtoA.Checked = True
                    Case "InWork"
                        RadioButtonStatusOtoIW.Checked = True
                    Case "InReview"
                        RadioButtonStatusOtoIR.Checked = True
                    Case "Released"
                        RadioButtonStatusOtoR.Checked = True
                    Case "Baselined"
                        RadioButtonStatusOtoB.Checked = True
                    Case "Obsolete"
                        RadioButtonStatusOtoO.Checked = True
                End Select
            End If
        End Set
    End Property

    Private _StatusRtoX As String
    Public Property StatusRtoX As String
        Get
            Return _StatusRtoX
        End Get
        Set(value As String)
            _StatusRtoX = value
            If Me.TabControl1 IsNot Nothing Then
                Select Case value
                    Case "Available"
                        RadioButtonStatusRtoA.Checked = True
                    Case "InWork"
                        RadioButtonStatusRtoIW.Checked = True
                    Case "InReview"
                        RadioButtonStatusRtoIR.Checked = True
                    Case "Released"
                        RadioButtonStatusRtoR.Checked = True
                    Case "Baselined"
                        RadioButtonStatusRtoB.Checked = True
                    Case "Obsolete"
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

    Private _KeepUnsortedDuplicates As Boolean
    Public Property KeepUnsortedDuplicates As Boolean
        Get
            Return _KeepUnsortedDuplicates
        End Get
        Set(value As Boolean)
            _KeepUnsortedDuplicates = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxKeepUnsortedDuplicates.Checked = value
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
                LabelSortRandomSampleFraction.Enabled = value
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

    Public Property PropertiesData As HCPropertiesData


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

    Private _ListViewUpdateFrequency As String
    Public Property ListViewUpdateFrequency As String
        Get
            Return _ListViewUpdateFrequency
        End Get
        Set(value As String)
            _ListViewUpdateFrequency = value
            If Me.TabControl1 IsNot Nothing Then
                Try
                    Dim i = CInt(value)
                    TextBoxListViewUpdateFrequency.Text = value
                Catch ex As Exception
                    TextBoxListViewUpdateFrequency.Text = "1"
                End Try
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

    Private _GroupFiles As Boolean
    Public Property GroupFiles As Boolean
        Get
            Return _GroupFiles
        End Get
        Set(value As Boolean)
            _GroupFiles = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxGroupFiles.Checked = value
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

    Private _RemindFilelistUpdate As Boolean
    Public Property RemindFilelistUpdate As Boolean
        Get
            Return _RemindFilelistUpdate
        End Get
        Set(value As Boolean)
            _RemindFilelistUpdate = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxRemindFilelistUpdate.Checked = value
            End If
        End Set
    End Property

    Private _ProcessDraftsInactive As Boolean
    Public Property ProcessDraftsInactive As Boolean
        Get
            Return _ProcessDraftsInactive
        End Get
        Set(value As Boolean)
            _ProcessDraftsInactive = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxProcessDraftsInactive.Checked = value
            End If
        End Set
    End Property

    Private _ExpressionEditorLanguage As String
    Public Property ExpressionEditorLanguage As String
        Get
            Return _ExpressionEditorLanguage
        End Get
        Set(value As String)
            _ExpressionEditorLanguage = value
            If Me.TabControl1 IsNot Nothing Then
                ComboBoxExpressionEditorLanguage.Text = value
            End If
        End Set
    End Property

    Private _DebugMode As Boolean
    Public Property DebugMode As Boolean
        Get
            Return _DebugMode
        End Get
        Set(value As Boolean)
            _DebugMode = value
            If Me.TabControl1 IsNot Nothing Then
                CheckBoxDebugMode.Checked = value
            End If
        End Set
    End Property

    Public Property HCDebugLogger As HCErrorLogger


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

    Public Property PresetsSaveFileFilters As Boolean

    Public Property PresetsSavePropertyFilters As Boolean

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

                Dim UFL As New UtilsFileList(Me)
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

    Private _Presets As HCPresets
    Public Property Presets As HCPresets
        Get
            Return _Presets
        End Get
        Set(value As HCPresets)
            _Presets = value
            If Me.TabControl1 IsNot Nothing Then

                Dim TextOld = ComboBoxPresetName.Text

                ComboBoxPresetName.Items.Clear()
                ComboBoxPresetName.Items.Add("")

                For Each tmpPreset As Preset In Presets.Items
                    ComboBoxPresetName.Items.Add(tmpPreset.Name)
                Next

                ComboBoxPresetName.Text = TextOld

            End If
        End Set
    End Property

    Private _ActivePreset As String
    Public Property ActivePreset As String
        Get
            Return _ActivePreset
        End Get
        Set(value As String)
            _ActivePreset = value
            If Me.TabControl1 IsNot Nothing Then
                ComboBoxPresetName.Text = value
            End If
        End Set
    End Property



    Public Property PropertyFilters As PropertyFilters

    Public Property TCCachePath As String
    Public Property TCItemIDRx As String
    Public Property TCRevisionRx As String
    Public Property TCItemIDName As String

    'Public Property SEInstalledPath As String
    'Public Property SEVersion As String
    Public Property SETemplatePath As String
    Public Property SEPreferencesPath As String
    Public Property SEMaterialsPath As String
    Public Property WorkingFilesPath As String
    Public Property SavedExpressions As HCSavedExpressions

    Public Property ActiveFile As String
    Public Property ActiveFiles As List(Of String)
    Public Property USEA As UtilsSEApp

    Private Property RunningStartup As Boolean

    'DESCRIPTION
    'Solid Edge Housekeeper
    'Robert McAnany 2020-2026
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

    'Public Sub New()

    '    ' This call is required by the designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.

    '    'https://stackoverflow.com/questions/1179532/how-do-i-pass-command-line-arguments-to-a-winforms-application
    '    'string[] args = Environment.GetCommandLineArgs();
    '    Dim Args = Environment.GetCommandLineArgs.ToList
    '    Dim s As String = ""
    '    For Each s1 As String In Args
    '        s = $"{s}{s1}{vbCrLf}"
    '    Next
    '    MsgBox(s)
    'End Sub

    Private Sub Startup(SavingPresets As Boolean)

        RunningStartup = True
        Me.Cursor = Cursors.WaitCursor

        Dim UP As New UtilsPreferences()
        Dim UD As New UtilsDocumentation
        Dim UC As New UtilsCommon

        'If UP.CheckInactive(Me.Version, Me.PreviewVersion) Then

        '    Dim FilenameString As String = $"SolidEdgeHousekeeper-v{Me.Version}_BetaPreview-{Me.PreviewVersion}"
        '    Dim s As String = FilenameString
        '    s = $"{s}{vbCrLf}currently appears to be inactive.{vbCrLf}{vbCrLf}"
        '    s = $"{s}Please contact support.{vbCrLf}"

        '    MsgBox(s)
        '    End
        'End If

        Dim Splash As FormSplash = Nothing
        If Not SavingPresets Then
            Splash = New FormSplash()
            Splash.Show()
            Splash.UpdateStatus("Initializing")
        End If

        Me.HCDebugLogger = New HCErrorLogger("Housekeeper")
        Dim StartupLogger As Logger = Me.HCDebugLogger.AddFile("Startup")
        StartupLogger.AddTimestamps(True)

        '###### INITIALIZE PREFERENCES IF NEEDED ######

        If Not SavingPresets Then Splash.UpdateStatus("Loading Preferences")

        UP.CreatePreferencesDirectory()
        UP.CreateFilenameCharmap()
        UP.CreateSavedExpressions()
        UP.CreateInteractiveEditCommands()

        Me.SavedExpressions = New HCSavedExpressions

        '###### LOAD MAIN FORM SAVED SETTINGS IF ANY ######

        If Not SavingPresets Then Splash.UpdateStatus("Loading Interface Preferences")

        UP.GetFormMainSettings(Me)


        '###### INITIALIZE DATA STRUCTURES IF NEEDED ######

        If Not SavingPresets Then Splash.UpdateStatus("Loading Data Structures")

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

            Me.ListOfColumns = tmpListOfColumns ' Triggers a Property update

        End If

        For i As Integer = 0 To Me.ListOfColumns.Count - 1
            Dim PropColumn As PropertyColumn = ListOfColumns(i)
            CLB_Properties.SetItemChecked(i, PropColumn.Visible)
        Next

        Dim TemplateList = {Me.AssemblyTemplate, Me.PartTemplate, Me.SheetmetalTemplate, Me.DraftTemplate}.ToList

        If Not SavingPresets Then Splash.UpdateStatus("Loading Properties Data")
        Me.PropertiesData = New HCPropertiesData  ' Automatically loads saved settings if any.

        If Not SavingPresets Then Splash.UpdateStatus("Loading Presets")
        Me.Presets = New HCPresets  ' Automatically loads saved settings if any.

        If Not SavingPresets Then Splash.UpdateStatus("Loading Property Filters")
        Me.PropertyFilters = New PropertyFilters  ' Automatically loads saved settings if any.

        If Not SavingPresets Then Splash.UpdateStatus("Building Readme")
        UD.BuildReadmeFile()

        CarIcona()

        'If (Me.LinkManagementFilename IsNot Nothing) AndAlso (Not Me.LinkManagementFilename.Trim = "") AndAlso (IO.File.Exists(Me.LinkManagementFilename)) Then
        '    Me.LinkManagementOrder = UP.GetLinkManagementOrder()
        'End If
        Me.LinkManagementOrder = UP.GetLinkManagementOrder() ' Defaults to {"CONTAINER", "RELATIVE", "ABSOLUTE"}

        'If Me.DebugMode Then
        '    If Me.LinkManagementOrder Is Nothing Then
        '        StartupLogger.AddMessage("Initialize LinkManagementOrder: 'Nothing'")
        '    Else
        '        Dim s As String = ""
        '        For Each s1 As String In Me.LinkManagementOrder
        '            If s = "" Then
        '                s = s1
        '            Else
        '                s = $"{s}, {s1}"
        '            End If
        '        Next
        '        StartupLogger.AddMessage($"Initialize LinkManagementOrder: '{s}'")
        '    End If
        'End If


        '###### INITIALIZE FILE LIST IF NEEDED ######

        If ListViewFiles.Groups.Count = 0 Then

            Dim GroupHeaderNames As New List(Of String)
            GroupHeaderNames.AddRange({"Files sources", "Excluded files", "Assemblies", "Parts", "Sheetmetals", "Drafts"})

            Dim GroupNames As New List(Of String)
            GroupNames.AddRange({"Sources", "Excluded", ".asm", ".par", ".psm", ".dft"})

            For i As Integer = 0 To GroupHeaderNames.Count - 1
                If Not SavingPresets Then Splash.UpdateStatus(String.Format("Initializing {0}", GroupHeaderNames(i)))

                Dim LVGroup As New ListViewGroup(GroupHeaderNames(i), HorizontalAlignment.Left)
                LVGroup.Name = GroupNames(i)
                ListViewFiles.Groups.Add(LVGroup)
            Next

        End If

        ListViewFiles.SetGroupState(ListViewGroupState.Collapsible)

        ' Form title
        Me.Text = String.Format("Solid Edge Housekeeper {0}", Me.Version)
        If Not Me.PreviewVersion = "" Then Me.Text = $"{Me.Text} Preview {Me.PreviewVersion}"

        '###### INITIALIZE TASK LIST ######

        If Not SavingPresets Then Splash.UpdateStatus("Loading Tasks")

        Me.TaskList = UP.GetTaskList(Splash)

        Dim tmpTaskPanel As Panel = Nothing

        For Each c As Control In TabPageTasks.Controls
            If Not SavingPresets Then Splash.UpdateStatus(String.Format("{0}", c.Name))

            If c.Name = "TaskPanel" Then
                tmpTaskPanel = CType(c, Panel)
                Exit For
            End If
        Next

        tmpTaskPanel.Controls.Clear()

        For i = TaskList.Count - 1 To 0 Step -1

            Dim Task = TaskList(i)

            If Not SavingPresets Then Splash.UpdateStatus(String.Format("Configuring {0}", Task.Name))

            If Not Me.RememberTasks Then
                Task.IsSelectedTask = False
                Task.IsSelectedAssembly = False
                Task.IsSelectedPart = False
                Task.IsSelectedSheetmetal = False
                Task.IsSelectedDraft = False
            End If

            Task.LinkManagementOrder = Me.LinkManagementOrder

            'If Me.DebugMode Then
            '    If Task.LinkManagementOrder Is Nothing Then
            '        StartupLogger.AddMessage($"{Task.Description}.LinkManagementOrder: 'Nothing'")
            '    End If
            'End If

            If Task.RequiresPropertiesData Then
                Task.PropertiesData = Me.PropertiesData
            End If

            tmpTaskPanel.Controls.Add(Task.TaskControl)
        Next

        'If Me.DebugMode Then
        '    If Me.LinkManagementOrder Is Nothing Then
        '        StartupLogger.AddMessage("Post task initialization LinkManagementOrder: 'Nothing'")
        '    Else
        '        Dim s As String = ""
        '        For Each s1 As String In Me.LinkManagementOrder
        '            If s = "" Then
        '                s = s1
        '            Else
        '                s = $"{s}, {s1}"
        '            End If
        '        Next
        '        StartupLogger.AddMessage($"Post task initialize LinkManagementOrder: '{s}'")
        '    End If
        'End If

        AddHandler editbox.Leave, AddressOf editbox_LostFocus
        AddHandler editbox.KeyUp, AddressOf editbox_KeyUp


        '################# Questo risolver il problema del bordo sgrazinato della ToolStrip
        If Not SavingPresets Then Splash.UpdateStatus("Updating Filters")
        ToolStrip_Filter.Renderer = New MySR()
        If Not SavingPresets Then Splash.UpdateStatus("Updating Presets")
        ToolStripPresets.Renderer = New MySR()
        '################# rif: https://stackoverflow.com/questions/1918247/how-to-disable-the-line-under-tool-strip-in-winform-c

        If Not IsNumeric(ListViewUpdateFrequency) Then ListViewUpdateFrequency = "1"

        If Not (Me.ExpressionEditorLanguage = "VB" Or Me.ExpressionEditorLanguage = "NCalc") Then
            Me.ExpressionEditorLanguage = "VB"
        End If

        If Me.TCItemIDRx Is Nothing OrElse Me.TCItemIDRx.Trim = "" Then TCItemIDRx = ".*"
        If Me.TCRevisionRx Is Nothing OrElse Me.TCRevisionRx.Trim = "" Then TCRevisionRx = ".*"
        If Me.TCItemIDName Is Nothing OrElse Me.TCItemIDName.Trim = "" Then TCItemIDName = "MFK9Item1"

        If Not SavingPresets Then Splash.UpdateStatus("Wrapping up")

        UP.CheckVersionFormat(Me.Version)  ' Displays MsgBox for malformed string.

        If Me.CheckForNewerVersion Then
            UP.CheckForNewerVersion(Me.Version)
        End If

        UP.SetSEDefaultFolders(Me)

        If Not SavingPresets Then
            Splash.UpdateStatus("")

            Splash.Animate()
            Splash.Dispose()
        End If
        Me.Cursor = Cursors.Default

        'If Me.DebugMode Then
        '    If Me.LinkManagementOrder Is Nothing Then
        '        StartupLogger.AddMessage("Post startup LinkManagementOrder: 'Nothing'")
        '    Else
        '        Dim s As String = ""
        '        For Each s1 As String In Me.LinkManagementOrder
        '            If s = "" Then
        '                s = s1
        '            Else
        '                s = $"{s}, {s1}"
        '            End If
        '        Next
        '        StartupLogger.AddMessage($"Post startup LinkManagementOrder: '{s}'")
        '    End If
        'End If

        'If Me.DebugMode Then
        '    HCDebugLogger.ReportErrors(UseMessageBox:=False)
        'End If

        Me.USEA = New UtilsSEApp(Me)
        Me.USEA.ErrorLogger = New Logger("Dummy", Nothing)

        RunningStartup = False

    End Sub


    Public Sub SaveSettings(SavingPresets As Boolean)
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
        Me.TextBoxStatus.Text = "Updating JSON PresetsList"
        Me.Presets = Me.Presets  ' Triggers save of presets JSON


        ' Save settings
        Dim UP As New UtilsPreferences
        Me.TextBoxStatus.Text = "Saving Form_Main settings"
        UP.SaveFormMainSettings(Me, SavingPresets:=SavingPresets)  ' If SavingPresets Then Don't save form size or location
        Me.TextBoxStatus.Text = "Saving tasks"
        UP.SaveTaskList(Me.TaskList)
        Me.TextBoxStatus.Text = "Saving presets"
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

        Next
        If Not s = "" Then
            MsgBox(s)
        End If
    End Sub


    ' BUTTONS


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        If ButtonCancel.Text = "Stop" Then
            StopProcess = True
        Else
            SaveSettings(SavingPresets:=False)

            ' Shut down
            Try
                End
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As EventArgs) Handles Me.FormClosing
        SaveSettings(SavingPresets:=False)

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

        tmpFileDialog.InitialDirectory = Me.SEPreferencesPath

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.FastSearchScopeFilename = tmpFileDialog.FileName
            Me.SEPreferencesPath = IO.Path.GetDirectoryName(Me.FastSearchScopeFilename)
        End If

    End Sub

    Private Sub ButtonLinkManagementFilename_Click(sender As Object, e As EventArgs) Handles ButtonLinkManagementFilename.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a link management file"
        tmpFileDialog.Filter = "Link Management Documents|*.txt"
        tmpFileDialog.InitialDirectory = Me.SEPreferencesPath

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.LinkManagementFilename = tmpFileDialog.FileName
            Me.SEPreferencesPath = IO.Path.GetDirectoryName(Me.LinkManagementFilename)
        End If

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

        tf = Me.PropertiesData Is Nothing

        If Not tf Then tf = Me.PropertiesData.Items.Count = 0

        If Not tf Then
            Dim FPF As New FormPropertyFilter

            FPF.PropertyFilters = Me.PropertyFilters
            FPF.ShowDialog()

            If FPF.DialogResult = DialogResult.OK Then
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
        UE.ProcessAll()
    End Sub


    ' FORM LOAD

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Startup(SavingPresets:=False)
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
    End Sub

    Private Sub CheckBoxTLAReportUnrelatedFiles_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTLAReportUnrelatedFiles.CheckedChanged
        Me.TLAReportUnrelatedFiles = CheckBoxTLAReportUnrelatedFiles.Checked

        ListViewFilesOutOfDate = True
    End Sub



    ' RADIO BUTTONS

    Private Sub RadioButtonTLABottomUp_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTLABottomUp.CheckedChanged
        Dim tf As Boolean = RadioButtonTLABottomUp.Checked
        Me.TLABottomUp = tf

        If tf Then
            TextBoxFastSearchScopeFilename.Enabled = True
            ButtonFastSearchScopeFilename.Enabled = True
            CheckBoxDraftAndModelSameName.Enabled = True
        End If

        ListViewFilesOutOfDate = True
    End Sub

    Private Sub RadioButtonTLATopDown_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTLATopDown.CheckedChanged
        Dim tf As Boolean = RadioButtonTLATopDown.Checked
        Me.TLATopDown = tf

        If tf Then
            TextBoxFastSearchScopeFilename.Enabled = False
            ButtonFastSearchScopeFilename.Enabled = False
            CheckBoxDraftAndModelSameName.Enabled = False
        End If

        ListViewFilesOutOfDate = True
    End Sub


    ' TEXT BOXES

    Private Sub TextBoxFastSearchScopeFilename_TextChanged(sender As Object, e As EventArgs) Handles TextBoxFastSearchScopeFilename.TextChanged
        Me.FastSearchScopeFilename = TextBoxFastSearchScopeFilename.Text
    End Sub

    Private Sub TextBoxStatus_TextChanged(sender As Object, e As EventArgs) Handles TextBoxStatus.TextChanged
        ToolTip1.SetToolTip(TextBoxStatus, TextBoxStatus.Text)
    End Sub

    Private Sub BT_AddSingleFiles_Click(sender As Object, e As EventArgs) Handles BT_AddSingleFiles.Click

        Dim NewWay As Boolean = True

        Dim tmpFolderDialog As New OpenFileDialog
        tmpFolderDialog.Multiselect = True
        tmpFolderDialog.Filter =
                                "Solid Edge files (*.par;*.psm;*.asm;*.dft)|*.par;*.psm;*.asm;*.dft|" +
                                "Assembly (*.asm)|*.asm|" +
                                "Part (*.par)|*.par|" +
                                "Sheet Metal (*.psm)|*.psm|" +
                                "Draft (*.dft)|*.dft"
        tmpFolderDialog.InitialDirectory = Me.WorkingFilesPath

        If tmpFolderDialog.ShowDialog() = DialogResult.OK Then

            Dim tmpItem As New ListViewItem

            tmpItem.Text = "Files selection"
            tmpItem.ImageKey = "Files"
            tmpItem.Tag = "Files"

            Dim FileLists As String = ""
            For Each tmpFile As String In tmpFolderDialog.FileNames
                If NewWay Then
                    FileLists = FileLists & tmpFile & vbTab
                Else
                    FileLists = FileLists & tmpFile & ","
                End If
            Next
            FileLists = FileLists.Remove(FileLists.Length - 1)  ' Remove trailing comma

            tmpItem.SubItems.Add(FileLists)
            tmpItem.Group = ListViewSources.Groups.Item("Sources")
            tmpItem.Name = FileLists

            If Not ListViewSources.Items.ContainsKey(tmpItem.Name) Then
                ListViewSources.Items.Add(tmpItem)
                ListViewSources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            End If

            ListViewFilesOutOfDate = True

            Me.WorkingFilesPath = IO.Path.GetDirectoryName(tmpFolderDialog.FileNames(0))
        End If

    End Sub

    Private Sub BT_AddFolder_Click(sender As Object, e As EventArgs) Handles BT_AddFolder.Click

        Dim tmpFolderDialog As New CommonOpenFileDialog
        tmpFolderDialog.IsFolderPicker = True
        tmpFolderDialog.Multiselect = True
        tmpFolderDialog.InitialDirectory = Me.WorkingFilesPath

        If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
            For Each tmpFolder As String In tmpFolderDialog.FileNames
                Dim tmpItem As New ListViewItem

                tmpItem.Text = "Folder"
                tmpItem.ImageKey = "Folder"
                tmpItem.Tag = "Folder"

                tmpItem.SubItems.Add(tmpFolder)
                tmpItem.Group = ListViewSources.Groups.Item("Sources")
                tmpItem.Name = tmpFolder

                If Not ListViewSources.Items.ContainsKey(tmpItem.Name) Then
                    ListViewSources.Items.Add(tmpItem)
                    ListViewSources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                End If
            Next

            ListViewFilesOutOfDate = True

            Me.WorkingFilesPath = tmpFolderDialog.FileNames(0)

        End If

    End Sub

    Private Sub BT_AddFolderSubfolders_Click(sender As Object, e As EventArgs) Handles BT_AddFolderSubfolders.Click

        Dim tmpFolderDialog As New CommonOpenFileDialog
        tmpFolderDialog.IsFolderPicker = True
        tmpFolderDialog.Multiselect = True
        tmpFolderDialog.InitialDirectory = Me.WorkingFilesPath

        If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
            For Each tmpFolder As String In tmpFolderDialog.FileNames
                Dim tmpItem As New ListViewItem

                tmpItem.Text = "Folder with subfolders"
                tmpItem.ImageKey = "Folders"
                tmpItem.Tag = "Folders"

                tmpItem.SubItems.Add(tmpFolder)
                tmpItem.Group = ListViewSources.Groups.Item("Sources")
                tmpItem.Name = tmpFolder

                If Not ListViewSources.Items.ContainsKey(tmpItem.Name) Then
                    ListViewSources.Items.Add(tmpItem)
                    ListViewSources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                End If
            Next

            ListViewFilesOutOfDate = True


            Dim tmpDir As String = tmpFolderDialog.FileNames(0)
            Me.WorkingFilesPath = IO.Directory.GetParent(tmpDir.TrimEnd(IO.Path.DirectorySeparatorChar)).ToString
        End If
    End Sub

    Private Sub BT_AddTeamCenter_Click(sender As Object, e As EventArgs) Handles BT_AddTeamCenter.Click

        Dim NewWay As Boolean = True

        Dim FTCA As New FormTeamCenterAdd(Me)
        Dim Result As DialogResult = FTCA.ShowDialog()

        If (Result = DialogResult.OK) And (FTCA.Filelist.Count > 0) Then
            Dim tmpFilelist As List(Of String) = FTCA.Filelist

            Dim tmpItem As New ListViewItem

            tmpItem.Text = "TeamCenter"
            tmpItem.ImageKey = "teamcenter"
            tmpItem.Tag = "TeamCenter"

            Dim FileLists As String = ""
            For Each tmpFile As String In tmpFilelist

                If NewWay Then
                    FileLists = FileLists & tmpFile & vbTab
                Else
                    FileLists = FileLists & tmpFile & ","
                End If

            Next
            FileLists = FileLists.Remove(FileLists.Length - 1)  ' Remove trailing comma

            tmpItem.SubItems.Add(FileLists)
            tmpItem.Group = ListViewSources.Groups.Item("Sources")
            tmpItem.Name = FileLists

            If Not ListViewSources.Items.ContainsKey(tmpItem.Name) Then
                ListViewSources.Items.Add(tmpItem)
                ListViewSources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            End If

            Me.ListViewFilesOutOfDate = True

        End If
    End Sub

    Private Sub BT_AddFromlist_Click(sender As Object, e As EventArgs) Handles BT_AddFromlist.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select list of files"
        tmpFileDialog.Filter = "TSV files|*.tsv|Text files|*.txt|CSV files|*.csv|Excel files|*.xls;*.xlsx;*.xlsm"
        tmpFileDialog.Multiselect = True
        tmpFileDialog.InitialDirectory = Me.WorkingFilesPath

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then

            For Each tmpFileName As String In tmpFileDialog.FileNames

                Dim tmpItem As New ListViewItem

                Select Case IO.Path.GetExtension(tmpFileName).ToLower

                    Case Is = ".tsv"
                        tmpItem.Text = "TSV list"
                        tmpItem.ImageKey = "csv" ' Not a typo.  Reusing 'csv'
                        tmpItem.Tag = "tsv"
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
                        MsgBox(String.Format("{0}: Extension {1} not recognized", Me.ToString, IO.Path.GetExtension(tmpFileName).ToLower))
                End Select

                tmpItem.SubItems.Add(tmpFileName)
                tmpItem.Group = ListViewSources.Groups.Item("Sources")

                tmpItem.Name = tmpFileName
                If Not ListViewSources.Items.ContainsKey(tmpItem.Name) Then
                    ListViewSources.Items.Add(tmpItem)
                    ListViewSources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                End If

            Next

            ListViewFilesOutOfDate = True

            Me.WorkingFilesPath = IO.Path.GetDirectoryName(tmpFileDialog.FileNames(0))
        End If

    End Sub

    Private Sub BT_TopLevelAsm_Click(sender As Object, e As EventArgs) Handles BT_TopLevelAsm.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select an assembly file"
        tmpFileDialog.Filter = "asm files|*.asm"
        tmpFileDialog.InitialDirectory = Me.WorkingFilesPath

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Dim tmpItem As New ListViewItem
            tmpItem.Text = "Top level assembly"
            tmpItem.SubItems.Add(tmpFileDialog.FileName)
            tmpItem.Group = ListViewSources.Groups.Item("Sources")
            tmpItem.ImageKey = "asm"
            tmpItem.Tag = "asm"
            tmpItem.Name = tmpFileDialog.FileName
            If Not ListViewSources.Items.ContainsKey(tmpItem.Name) Then ListViewSources.Items.Add(tmpItem) : ListViewSources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

            If CheckBoxTLAAutoIncludeTLF.Checked Then
                Dim Folder As String = System.IO.Path.GetDirectoryName(tmpFileDialog.FileName)

                Dim tmpItem2 As New ListViewItem
                tmpItem2.Text = "Top level asm folder"
                tmpItem2.SubItems.Add(Folder)
                tmpItem2.Group = ListViewSources.Groups.Item("Sources")
                tmpItem2.ImageKey = "ASM_Folder"
                tmpItem2.Tag = "ASM_Folder"
                tmpItem2.Name = Folder
                If Not ListViewSources.Items.ContainsKey(tmpItem2.Name) Then
                    ListViewSources.Items.Add(tmpItem2)
                    ListViewSources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                End If

            End If

            ListViewFilesOutOfDate = True

            Me.WorkingFilesPath = IO.Path.GetDirectoryName(tmpFileDialog.FileName)
        End If

    End Sub

    Private Sub BT_ASM_Folder_Click(sender As Object, e As EventArgs) Handles BT_ASM_Folder.Click

        Dim tmpFolderDialog As New CommonOpenFileDialog
        tmpFolderDialog.IsFolderPicker = True
        tmpFolderDialog.Multiselect = True
        tmpFolderDialog.InitialDirectory = Me.WorkingFilesPath

        If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
            For Each tmpFolder As String In tmpFolderDialog.FileNames
                Dim tmpItem As New ListViewItem

                tmpItem.Text = "Top level asm folder"
                tmpItem.ImageKey = "ASM_Folder"
                tmpItem.Tag = "ASM_Folder"

                tmpItem.SubItems.Add(tmpFolder)
                tmpItem.Group = ListViewSources.Groups.Item("Sources")
                tmpItem.Name = tmpFolder

                If Not ListViewSources.Items.ContainsKey(tmpItem.Name) Then
                    ListViewSources.Items.Add(tmpItem)
                    ListViewSources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                End If
            Next

            ListViewFilesOutOfDate = True

            Dim tmpDir As String = tmpFolderDialog.FileNames(0)
            Me.WorkingFilesPath = IO.Directory.GetParent(tmpDir.TrimEnd(IO.Path.DirectorySeparatorChar)).ToString
        End If
    End Sub

    Private Sub BT_ActiveFile_Click(sender As Object, e As EventArgs) Handles BT_ActiveFile.Click
        Dim tmpItem As New ListViewItem
        tmpItem.Text = "Active file"
        tmpItem.Group = ListViewSources.Groups.Item("Sources")
        tmpItem.ImageKey = "ActiveFile"
        tmpItem.Tag = "ActiveFile"
        tmpItem.Name = "ActiveFile"
        If Not ListViewSources.Items.ContainsKey(tmpItem.Name) Then ListViewSources.Items.Add(tmpItem) : ListViewSources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

    End Sub

    Private Sub BT_ActiveFiles_Click(sender As Object, e As EventArgs) Handles BT_ActiveFiles.Click
        Dim tmpItem As New ListViewItem
        tmpItem.Text = "Active files"
        tmpItem.Group = ListViewSources.Groups.Item("Sources")
        tmpItem.ImageKey = "ActiveFiles"
        tmpItem.Tag = "ActiveFiles"
        tmpItem.Name = "ActiveFiles"
        If Not ListViewSources.Items.ContainsKey(tmpItem.Name) Then ListViewSources.Items.Add(tmpItem) : ListViewSources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub

    Private Sub ListViewFiles_DragEnter(sender As Object, e As DragEventArgs) Handles ListViewFiles.DragEnter

        e.Effect = DragDropEffects.Copy

    End Sub

    Private Sub ListViewFiles_DragDrop(sender As Object, e As DragEventArgs) Handles ListViewFiles.DragDrop

        Dim UC As New UtilsCommon

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            Dim DragDropGroupPresent As Boolean

            DragDropGroupPresent = False
            For Each g As ListViewGroup In ListViewSources.Groups
                If g.Name = "DragDrop" Then
                    DragDropGroupPresent = True
                    Exit For
                End If
            Next

            If Not DragDropGroupPresent Then
                Dim tmpItem As New ListViewItem
                tmpItem.Text = "DragDrop"
                tmpItem.Group = ListViewSources.Groups.Item("Sources")
                tmpItem.ImageKey = "ASM_Folder"
                tmpItem.Tag = "DragDrop"
                tmpItem.Name = "DragDrop"
                If Not ListViewSources.Items.ContainsKey(tmpItem.Name) Then ListViewSources.Items.Add(tmpItem)

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
                            'ListViewFiles.Items.Add(tmpLVItem)

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

    Private Sub BT_DeleteAll_Click(sender As Object, e As EventArgs) Handles BT_DeleteAll.Click

        ListViewSources.Items.Clear()

        ListViewFiles.BeginUpdate()
        ListViewFiles.Items.Clear()
        ListViewFiles.EndUpdate()

        DragDropCache.Clear()
        DragDropCacheExcluded.Clear()

        'ListViewFilesOutOfDate = True

        ButtonProcess.Text = "Process"

    End Sub

    Private Sub BT_Reload_Click(sender As Object, e As EventArgs) Handles BT_Update.Click

        If My.Computer.Keyboard.ShiftKeyDown Then

#Region "TOBEMOVED"
            Me.Cursor = Cursors.WaitCursor

            Dim ElapsedTime As Double
            Dim ElapsedTimeText As String

            Dim StartTime As DateTime = Now

            Me.TextBoxStatus.Text = "Updating properties..."
            Me.LabelTimeRemaining.Text = ""
            System.Windows.Forms.Application.DoEvents()
#End Region

            ' Core function is here
            Me.StopProcess = False
            Me.ButtonCancel.Text = "Stop"

            Dim UFL As New UtilsFileList(Me)
            Me.Cursor = Cursors.WaitCursor
            UFL.UpdatePropertiesColumns()
            Me.Cursor = Cursors.Default

            Me.ButtonCancel.Text = "Cancel"
            ' End of core function
#Region "TOBEMOVED"
            Me.Cursor = Cursors.Default

            ElapsedTime = Now.Subtract(StartTime).TotalMinutes
            If ElapsedTime < 60 Then
                ElapsedTimeText = "in " + ElapsedTime.ToString("0.0") + " min."
            Else
                ElapsedTimeText = "in " + (ElapsedTime / 60).ToString("0.0") + " hr."
            End If

            Dim filecount As Integer = ListViewFiles.Items.Count - ListViewFiles.Groups.Item("Sources").Items.Count
            Me.TextBoxStatus.Text = String.Format("Updated properties in {0} files in {1}", filecount, ElapsedTimeText)
#End Region

        Else

            If Me.SortRandomSample Then
                Dim Result As MsgBoxResult = MsgBox("INFO: Sort Random Sample enabled.  Select Cancel to quit.", vbOKCancel)
                If Result = MsgBoxResult.Cancel Then Exit Sub
            End If

            ButtonProcess.Text = "Process"

            Dim UFL As New UtilsFileList(Me)
            UFL.New_UpdateFileList()

        End If

    End Sub

    Private Sub ListViewSources_KeyUp(sender As Object, e As KeyEventArgs) Handles ListViewSources.KeyUp

        If e.KeyCode = Keys.Escape Then ListViewSources.SelectedItems.Clear()

        If e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then

            For i = ListViewSources.SelectedItems.Count - 1 To 0 Step -1

                Dim tmpItem As ListViewItem = ListViewSources.SelectedItems.Item(i)
                If tmpItem.Group.Name = "Sources" Then
                    tmpItem.Remove()
                    ListViewFilesOutOfDate = True
                End If

            Next

        End If

        If e.Control And e.KeyCode = Keys.C Then
            Dim i = 0
        End If

    End Sub

    Private Sub ListViewFiles_KeyUp(sender As Object, e As KeyEventArgs) Handles ListViewFiles.KeyUp

        If e.KeyCode = Keys.Escape Then ListViewFiles.SelectedItems.Clear()

        If e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then

            For i = ListViewFiles.SelectedItems.Count - 1 To 0 Step -1

                Dim tmpItem As ListViewItem = ListViewFiles.SelectedItems.Item(i)
                If tmpItem.Group.Name = "Sources" Then
                    tmpItem.Remove()
                    'ListViewFilesOutOfDate = True

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

            ListViewFiles.SelectedItems.Clear()

        End If

        If e.Control And e.KeyCode = Keys.C Then
            ' https://dotnetref.blogspot.com/2007/06/copy-listview-to-clipboard.html
            ' https://stackoverflow.com/questions/1873870/copying-data-from-a-winforms-listview

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
        CaricaImmagine16x16(TabPage_ImageList, "Up", My.Resources.up)
        CaricaImmagine16x16(TabPage_ImageList, "Down", My.Resources.down)
        CaricaImmagine16x16(TabPage_ImageList, "Files", My.Resources.Files)
        CaricaImmagine16x16(TabPage_ImageList, "teamcenter", My.Resources.teamcenter)
        CaricaImmagine16x16(TabPage_ImageList, "ActiveFile", My.Resources.active_file)
        CaricaImmagine16x16(TabPage_ImageList, "ActiveFiles", My.Resources.active_files)

    End Sub

    Private Sub CaricaImmagine16x16(IL As ImageList, Key As String, Immagine As Image)

        Dim b = New Bitmap(16, 16)
        Dim g = Graphics.FromImage(b)
        g.FillRectangle(New SolidBrush(Color.Transparent), 0, 0, 16, 16)
        g.DrawImage(Immagine, 0, 0, 16, 16)

        IL.Images.Add(Key, b)

    End Sub

    Private Sub BT_ExportList_Click(sender As Object, e As EventArgs) Handles BT_ExportList.Click

        Dim NewWay As Boolean = True

        Dim tmpFileDialog As New SaveFileDialog
        tmpFileDialog.Title = "Save a list of files"

        If NewWay Then
            tmpFileDialog.Filter = "Tab separated variable files|*.tsv"
        Else
            tmpFileDialog.Filter = "Text files|*.txt"
        End If

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then

            Dim content As String = ""
            For Each tmpItem As ListViewItem In ListViewFiles.Items

                If tmpItem.Group.Name <> "Sources" And tmpItem.Group.Name <> "Excluded" Then
                    content += tmpItem.Name

                    For Each subItem As ListViewItem.ListViewSubItem In tmpItem.SubItems
                        If NewWay Then
                            If subItem.Bounds.Width <> 0 Then content += vbTab & subItem.Text
                        Else
                            If subItem.Bounds.Width <> 0 Then content += "," & subItem.Text
                        End If
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

            If ListViewFiles.Items.Item(i).ImageKey <> "Error" Then ListViewFiles.Items.RemoveAt(ListViewFiles.Items.Item(i).Index) ' Item(i).Remove()

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

    Private Sub BT_ExcludeFromProcessing_Click(sender As Object, e As EventArgs) Handles BT_ExcludeFromProcessing.Click

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

        ListViewFiles.SelectedItems.Clear()

        If Not ListViewSources.SelectedItems.Count = 0 Then
            For i = ListViewSources.SelectedItems.Count - 1 To 0 Step -1
                Dim tmpItem As ListViewItem = ListViewSources.SelectedItems.Item(i)
                tmpItem.Remove()
            Next

            ListViewFilesOutOfDate = True
        End If

    End Sub

    Private Sub BT_RemoveFromList_Click(sender As Object, e As EventArgs) Handles BT_RemoveFromList.Click

        ListViewFiles.BeginUpdate()     '##### Inserted for performance reason, removing a large number of items is nearly instantaneous now

        For i = ListViewFiles.SelectedItems.Count - 1 To 0 Step -1
            'Dim tmpItem As ListViewItem = ListViewFiles.SelectedItems.Item(i)
            'tmpItem.Remove()

            ListViewFiles.Items.RemoveAt(ListViewFiles.SelectedItems.Item(i).Index)     '#### Another performance improovement; .Items.RemoveAt is faster than .Item.Remove

        Next

        If Not ListViewSources.SelectedItems.Count = 0 Then
            For i = ListViewSources.SelectedItems.Count - 1 To 0 Step -1
                Dim tmpItem As ListViewItem = ListViewSources.SelectedItems.Item(i)
                tmpItem.Remove()
            Next

            ListViewFilesOutOfDate = True
        End If

        ListViewFiles.EndUpdate()

    End Sub

    Private Sub BT_MoveToRecycleBin_Click(sender As Object, e As EventArgs) Handles BT_MoveToRecycleBin.Click

        ' Check for non-local files.  They don't get moved to the Recycle Bin and are permanently deleted.
        'https://stackoverflow.com/questions/4325712/how-can-i-determine-if-a-string-is-a-local-folder-string-or-a-network-string
        Dim NonLocalFiles As New List(Of String)
        For i = 0 To ListViewFiles.SelectedItems.Count - 1
            Dim tmpItem As ListViewItem = ListViewFiles.SelectedItems.Item(i)
            Dim Filename As String = tmpItem.Name
            If Filename.StartsWith("/") Or Filename.StartsWith("\") Then
                NonLocalFiles.Add(Filename)
            Else
                Dim DriveLetter As String = System.IO.Path.GetPathRoot(Filename)
                Dim DriveInfo As New System.IO.DriveInfo(DriveLetter)
                If DriveInfo.DriveType = IO.DriveType.Network Then
                    NonLocalFiles.Add(Filename)
                End If
            End If
        Next

        If Not NonLocalFiles.Count = 0 Then
            Dim s As String = $"Network files will be permanently deleted.  Do you wish to continue?{vbCrLf}{vbCrLf}"
            Dim Indent As String = "    "
            For i = 0 To NonLocalFiles.Count - 1
                If i = 10 Then
                    s = $"{s}{Indent} (And {NonLocalFiles.Count - 10} more){vbCrLf}"
                    Exit For
                End If
                s = $"{s}{Indent}{NonLocalFiles(i)}{vbCrLf}"
            Next

            Dim Result = MessageBox.Show(s, "Warning !", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If Result = MsgBoxResult.No Then
                Exit Sub
            End If

        End If

        For i = ListViewFiles.SelectedItems.Count - 1 To 0 Step -1

            Dim tmpItem As ListViewItem = ListViewFiles.SelectedItems.Item(i)
            Dim Filename As String = tmpItem.Name
            Try
                My.Computer.FileSystem.DeleteFile(Filename, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                tmpItem.Remove()
            Catch ex As Exception
                Dim s As String = String.Format("Could not move {0} to the recycle bin.", IO.Path.GetFileName(Filename))
                s = String.Format("{0}  Click OK to keep processing, Cancel to quit.", s)
                Dim Result As MsgBoxResult = MsgBox(s)
                If Result = MsgBoxResult.Cancel Then Exit For
            End Try
        Next

    End Sub

    Private Sub TextBoxListViewUpdateFrequency_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxListViewUpdateFrequency.KeyDown

        If e.KeyCode = Keys.Enter Then
            Me.ActiveControl = Nothing
            e.Handled = True
            e.SuppressKeyPress = True
        End If

    End Sub

    Private Sub TextBoxListViewUpdateFrequency_Leave(sender As Object, e As EventArgs) Handles TextBoxListViewUpdateFrequency.Leave

        If Not IsNumeric(TextBoxListViewUpdateFrequency.Text) Then TextBoxListViewUpdateFrequency.Text = "1"
        If TextBoxListViewUpdateFrequency.Text = "0" Then TextBoxListViewUpdateFrequency.Text = "1"
        Me.ListViewUpdateFrequency = TextBoxListViewUpdateFrequency.Text

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


    Private Sub BT_ProcessSelected_Click(sender As Object, e As EventArgs) Handles BT_ProcessSelected.Click
        Dim UE As New UtilsExecute(Me)

        If Not ListViewFiles.SelectedItems.Count = 0 Then UE.ProcessAll()

    End Sub

    Private Sub BT_FindLinks_Click(sender As Object, e As EventArgs) Handles BT_FindLinks.Click

        ' A user reported he was confused by this command.  In particular that it didn't
        ' find Draft files or use his Property Filter settings.
        ' Disabling for now.

        MsgBox("This command is temporarily disabled.  Please use 'Update' instead.")
        Exit Sub


        'Dim DMApp As New DesignManager.Application
        'Dim DMDoc As DesignManager.Document

        'Dim UC As New UtilsCommon

        'For Each item As ListViewItem In ListViewFiles.SelectedItems

        '    DMDoc = CType(DMApp.Open(item.Name), DesignManager.Document)

        '    UC.tmpList = New Collection
        '    UC.FindLinked(DMDoc)

        '    For Each FoundFile In UC.tmpList
        '        If UC.FilenameIsOK(FoundFile.ToString) Then

        '            If IO.File.Exists(FoundFile.ToString) Then

        '                If Not ListViewFiles.Items.ContainsKey(FoundFile.ToString) Then

        '                    Dim tmpLVItem As New ListViewItem
        '                    tmpLVItem.Text = IO.Path.GetFileName(FoundFile.ToString)
        '                    tmpLVItem.SubItems.Add(IO.Path.GetDirectoryName(FoundFile.ToString))
        '                    tmpLVItem.ImageKey = "Unchecked"
        '                    tmpLVItem.Tag = IO.Path.GetExtension(FoundFile.ToString).ToLower 'Backup gruppo
        '                    tmpLVItem.Name = FoundFile.ToString
        '                    tmpLVItem.Group = ListViewFiles.Groups.Item(IO.Path.GetExtension(FoundFile.ToString).ToLower)
        '                    ListViewFiles.Items.Add(tmpLVItem)

        '                End If

        '            End If

        '        End If
        '    Next

        'Next

        'UC.tmpList = Nothing

        'DMApp.Quit()

    End Sub

    Private Sub CheckBoxTLAIncludePartCopies_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTLAIncludePartCopies.CheckedChanged
        Me.TLAIncludePartCopies = CheckBoxTLAIncludePartCopies.Checked

        ListViewFilesOutOfDate = True
    End Sub

    Private Sub CheckBoxDraftAndModelSameName_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDraftAndModelSameName.CheckedChanged
        Me.DraftAndModelSameName = CheckBoxDraftAndModelSameName.Checked

        ListViewFilesOutOfDate = True
    End Sub

    Private Sub RadioButtonSortDependency_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSortDependency.CheckedChanged
        Me.SortDependency = RadioButtonSortDependency.Checked

        CheckBoxSortIncludeNoDependencies.Visible = Me.SortDependency

        ListViewFilesOutOfDate = True

    End Sub

    Private Sub RadioButtonSortNone_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSortNone.CheckedChanged
        Me.SortNone = RadioButtonSortNone.Checked

        CheckBoxKeepUnsortedDuplicates.Visible = Me.SortNone

        ListViewFilesOutOfDate = True
    End Sub

    Private Sub RadioButtonSortAlphabetical_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSortAlphabetical.CheckedChanged
        Me.SortAlphabetical = RadioButtonSortAlphabetical.Checked

        ListViewFilesOutOfDate = True
    End Sub

    Private Sub CheckBoxSortIncludeNoDependencies_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSortIncludeNoDependencies.CheckedChanged
        Me.SortIncludeNoDependencies = CheckBoxSortIncludeNoDependencies.Checked

        ListViewFilesOutOfDate = True
    End Sub

    Private Sub RadioButtonSortRandomSample_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSortRandomSample.CheckedChanged
        Me.SortRandomSample = RadioButtonSortRandomSample.Checked

        TextBoxSortRandomSampleFraction.Visible = Me.SortRandomSample
        LabelSortRandomSampleFraction.Visible = Me.SortRandomSample

        ListViewFilesOutOfDate = True

    End Sub

    Private Sub TextBoxRandomSampleFraction_LostFocus(sender As Object, e As EventArgs) Handles TextBoxSortRandomSampleFraction.LostFocus
        ListViewFilesOutOfDate = True

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

        Dim ETL As New FormEditTaskList()
        ETL.RememberTaskSelections = Me.RememberTasks
        ETL.OldTaskList = Me.TaskList

        Dim DialogResult As DialogResult
        Dim tmpTasks As New List(Of Task)

        DialogResult = ETL.ShowDialog()

        If DialogResult = DialogResult.OK Then
            Me.TaskList = ETL.TaskList

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
                    Task.PropertiesData = Me.PropertiesData
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

        Dim Tag As String = "file-selection"

        Dim UD As New UtilsDocumentation

        Dim HelpURL = UD.GenerateVersionURL(Tag)

        System.Diagnostics.Process.Start(HelpURL)

    End Sub

    Private Sub ButtonAssemblyTemplate_Click(sender As Object, e As EventArgs) Handles ButtonAssemblyTemplate.Click

        Dim tmpFileDialog As New OpenFileDialog()
        tmpFileDialog.Title = "Select an assembly template"
        tmpFileDialog.Filter = "Assembly Template|*.asm"
        tmpFileDialog.InitialDirectory = Me.SETemplatePath

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.AssemblyTemplate = tmpFileDialog.FileName
            CopyTemplatesToTasks()
            Me.SETemplatePath = IO.Path.GetDirectoryName(Me.AssemblyTemplate)
        End If

    End Sub

    Private Sub ButtonPartTemplate_Click(sender As Object, e As EventArgs) Handles ButtonPartTemplate.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a part template"
        tmpFileDialog.Filter = "Part Template|*.par"
        tmpFileDialog.InitialDirectory = Me.SETemplatePath

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.PartTemplate = tmpFileDialog.FileName
            CopyTemplatesToTasks()
            Me.SETemplatePath = IO.Path.GetDirectoryName(Me.PartTemplate)
        End If

    End Sub

    Private Sub ButtonSheetmetalTemplate_Click(sender As Object, e As EventArgs) Handles ButtonSheetmetalTemplate.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a sheetmetal template"
        tmpFileDialog.Filter = "Sheetmetal Template|*.psm"
        tmpFileDialog.InitialDirectory = Me.SETemplatePath

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.SheetmetalTemplate = tmpFileDialog.FileName
            CopyTemplatesToTasks()
            Me.SETemplatePath = IO.Path.GetDirectoryName(Me.SheetmetalTemplate)
        End If

    End Sub

    Private Sub ButtonDraftTemplate_Click(sender As Object, e As EventArgs) Handles ButtonDraftTemplate.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a draft template"
        tmpFileDialog.Filter = "Draft Template|*.dft"
        tmpFileDialog.InitialDirectory = Me.SETemplatePath

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.DraftTemplate = tmpFileDialog.FileName
            CopyTemplatesToTasks()
            Me.SETemplatePath = IO.Path.GetDirectoryName(Me.DraftTemplate)
        End If

    End Sub

    Private Sub ButtonMaterialTable_Click(sender As Object, e As EventArgs) Handles ButtonMaterialTable.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a material table"
        tmpFileDialog.Filter = "Material Table|*.mtl"
        tmpFileDialog.InitialDirectory = Me.SEMaterialsPath

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.MaterialTable = tmpFileDialog.FileName
            CopyTemplatesToTasks()
            Me.SEMaterialsPath = IO.Path.GetDirectoryName(Me.MaterialTable)
        End If

    End Sub

    Private Sub ButtonUpdatePropertiesData_Click(sender As Object, e As EventArgs) Handles ButtonUpdatePropertiesData.Click

        Dim UC As New UtilsCommon

        Dim TemplateList = {Me.AssemblyTemplate, Me.PartTemplate, Me.SheetmetalTemplate, Me.DraftTemplate}.ToList

        Me.Cursor = Cursors.WaitCursor

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

        Dim UD As New UtilsDocumentation

        If ModifierKeys = Keys.Alt + Keys.Control Then
            UD.UpdateBaseURL()
        Else
            Dim Tag As String = "readme"
            Dim HelpURL = UD.GenerateVersionURL(Tag)
            System.Diagnostics.Process.Start(HelpURL)
        End If

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
            Me.StatusAtoX = "Available"
            RadioButtonStatusAtoA.Image = My.Resources.Checked
        Else
            RadioButtonStatusAtoA.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusAtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoB.CheckedChanged
        If RadioButtonStatusAtoB.Checked Then
            Me.StatusAtoX = "Baselined"
            RadioButtonStatusAtoB.Image = My.Resources.Checked
        Else
            RadioButtonStatusAtoB.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusAtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoIR.CheckedChanged
        If RadioButtonStatusAtoIR.Checked Then
            Me.StatusAtoX = "InReview"
            RadioButtonStatusAtoIR.Image = My.Resources.Checked
        Else
            RadioButtonStatusAtoIR.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusAtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoIW.CheckedChanged
        If RadioButtonStatusAtoIW.Checked Then
            Me.StatusAtoX = "InWork"
            RadioButtonStatusAtoIW.Image = My.Resources.Checked
        Else
            RadioButtonStatusAtoIW.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusAtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoO.CheckedChanged
        If RadioButtonStatusAtoO.Checked Then
            Me.StatusAtoX = "Obsolete"
            RadioButtonStatusAtoO.Image = My.Resources.Checked
        Else
            RadioButtonStatusAtoO.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusAtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusAtoR.CheckedChanged
        If RadioButtonStatusAtoR.Checked Then
            Me.StatusAtoX = "Released"
            RadioButtonStatusAtoR.Image = My.Resources.Checked
        Else
            RadioButtonStatusAtoR.Image = My.Resources.Unchecked
        End If
    End Sub

    Private Sub RadioButtonStatusBtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoA.CheckedChanged
        If RadioButtonStatusBtoA.Checked Then
            Me.StatusBtoX = "Available"
            RadioButtonStatusBtoA.Image = My.Resources.Checked
        Else
            RadioButtonStatusBtoA.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusBtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoB.CheckedChanged
        If RadioButtonStatusBtoB.Checked Then
            Me.StatusBtoX = "Baselined"
            RadioButtonStatusBtoB.Image = My.Resources.Checked
        Else
            RadioButtonStatusBtoB.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusBtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoIR.CheckedChanged
        If RadioButtonStatusBtoIR.Checked Then
            Me.StatusBtoX = "InReview"
            RadioButtonStatusBtoIR.Image = My.Resources.Checked
        Else
            RadioButtonStatusBtoIR.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusBtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoIW.CheckedChanged
        If RadioButtonStatusBtoIW.Checked Then
            Me.StatusBtoX = "InWork"
            RadioButtonStatusBtoIW.Image = My.Resources.Checked
        Else
            RadioButtonStatusBtoIW.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusBtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoO.CheckedChanged
        If RadioButtonStatusBtoO.Checked Then
            Me.StatusBtoX = "Obsolete"
            RadioButtonStatusBtoO.Image = My.Resources.Checked
        Else
            RadioButtonStatusBtoO.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusBtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusBtoR.CheckedChanged
        If RadioButtonStatusBtoR.Checked Then
            Me.StatusBtoX = "Released"
            RadioButtonStatusBtoR.Image = My.Resources.Checked
        Else
            RadioButtonStatusBtoR.Image = My.Resources.Unchecked
        End If
    End Sub

    Private Sub RadioButtonStatusIRtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoA.CheckedChanged
        If RadioButtonStatusIRtoA.Checked Then
            Me.StatusIRtoX = "Available"
            RadioButtonStatusIRtoA.Image = My.Resources.Checked
        Else
            RadioButtonStatusIRtoA.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoB.CheckedChanged
        If RadioButtonStatusIRtoB.Checked Then
            Me.StatusIRtoX = "Baselined"
            RadioButtonStatusIRtoB.Image = My.Resources.Checked
        Else
            RadioButtonStatusIRtoB.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoIR.CheckedChanged
        If RadioButtonStatusIRtoIR.Checked Then
            Me.StatusIRtoX = "InReview"
            RadioButtonStatusIRtoIR.Image = My.Resources.Checked
        Else
            RadioButtonStatusIRtoIR.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoIW.CheckedChanged
        If RadioButtonStatusIRtoIW.Checked Then
            Me.StatusIRtoX = "InWork"
            RadioButtonStatusIRtoIW.Image = My.Resources.Checked
        Else
            RadioButtonStatusIRtoIW.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoO.CheckedChanged
        If RadioButtonStatusIRtoO.Checked Then
            Me.StatusIRtoX = "Obsolete"
            RadioButtonStatusIRtoO.Image = My.Resources.Checked
        Else
            RadioButtonStatusIRtoO.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusIRtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIRtoR.CheckedChanged
        If RadioButtonStatusIRtoR.Checked Then
            Me.StatusIRtoX = "Released"
            RadioButtonStatusIRtoR.Image = My.Resources.Checked
        Else
            RadioButtonStatusIRtoR.Image = My.Resources.Unchecked
        End If
    End Sub

    Private Sub RadioButtonStatusIWtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoA.CheckedChanged
        If RadioButtonStatusIWtoA.Checked Then
            Me.StatusIWtoX = "Available"
            RadioButtonStatusIWtoA.Image = My.Resources.Checked
        Else
            RadioButtonStatusIWtoA.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoB.CheckedChanged
        If RadioButtonStatusIWtoB.Checked Then
            Me.StatusIWtoX = "Baselined"
            RadioButtonStatusIWtoB.Image = My.Resources.Checked
        Else
            RadioButtonStatusIWtoB.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoIR.CheckedChanged
        If RadioButtonStatusIWtoIR.Checked Then
            Me.StatusIWtoX = "InReview"
            RadioButtonStatusIWtoIR.Image = My.Resources.Checked
        Else
            RadioButtonStatusIWtoIR.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoIW.CheckedChanged
        If RadioButtonStatusIWtoIW.Checked Then
            Me.StatusIWtoX = "InWork"
            RadioButtonStatusIWtoIW.Image = My.Resources.Checked
        Else
            RadioButtonStatusIWtoIW.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoO.CheckedChanged
        If RadioButtonStatusIWtoO.Checked Then
            Me.StatusIWtoX = "Obsolete"
            RadioButtonStatusIWtoO.Image = My.Resources.Checked
        Else
            RadioButtonStatusIWtoO.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusIWtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusIWtoR.CheckedChanged
        If RadioButtonStatusIWtoR.Checked Then
            Me.StatusIWtoX = "Released"
            RadioButtonStatusIWtoR.Image = My.Resources.Checked
        Else
            RadioButtonStatusIWtoR.Image = My.Resources.Unchecked
        End If
    End Sub

    Private Sub RadioButtonStatusOtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoA.CheckedChanged
        If RadioButtonStatusOtoA.Checked Then
            Me.StatusOtoX = "Available"
            RadioButtonStatusOtoA.Image = My.Resources.Checked
        Else
            RadioButtonStatusOtoA.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusOtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoB.CheckedChanged
        If RadioButtonStatusOtoB.Checked Then
            Me.StatusOtoX = "Baselined"
            RadioButtonStatusOtoB.Image = My.Resources.Checked
        Else
            RadioButtonStatusOtoB.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusOtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoIR.CheckedChanged
        If RadioButtonStatusOtoIR.Checked Then
            Me.StatusOtoX = "InReview"
            RadioButtonStatusOtoIR.Image = My.Resources.Checked
        Else
            RadioButtonStatusOtoIR.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusOtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoIW.CheckedChanged
        If RadioButtonStatusOtoIW.Checked Then
            Me.StatusOtoX = "InWork"
            RadioButtonStatusOtoIW.Image = My.Resources.Checked
        Else
            RadioButtonStatusOtoIW.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusOtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoO.CheckedChanged
        If RadioButtonStatusOtoO.Checked Then
            Me.StatusOtoX = "Obsolete"
            RadioButtonStatusOtoO.Image = My.Resources.Checked
        Else
            RadioButtonStatusOtoO.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusOtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusOtoR.CheckedChanged
        If RadioButtonStatusOtoR.Checked Then
            Me.StatusOtoX = "Released"
            RadioButtonStatusOtoR.Image = My.Resources.Checked
        Else
            RadioButtonStatusOtoR.Image = My.Resources.Unchecked
        End If
    End Sub

    Private Sub RadioButtonStatusRtoA_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoA.CheckedChanged
        If RadioButtonStatusRtoA.Checked Then
            Me.StatusRtoX = "Available"
            RadioButtonStatusRtoA.Image = My.Resources.Checked
        Else
            RadioButtonStatusRtoA.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusRtoB_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoB.CheckedChanged
        If RadioButtonStatusRtoB.Checked Then
            Me.StatusRtoX = "Baselined"
            RadioButtonStatusRtoB.Image = My.Resources.Checked
        Else
            RadioButtonStatusRtoB.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusRtoIR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoIR.CheckedChanged
        If RadioButtonStatusRtoIR.Checked Then
            Me.StatusRtoX = "InReview"
            RadioButtonStatusRtoIR.Image = My.Resources.Checked
        Else
            RadioButtonStatusRtoIR.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusRtoIW_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoIW.CheckedChanged
        If RadioButtonStatusRtoIW.Checked Then
            Me.StatusRtoX = "InWork"
            RadioButtonStatusRtoIW.Image = My.Resources.Checked
        Else
            RadioButtonStatusRtoIW.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusRtoO_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoO.CheckedChanged
        If RadioButtonStatusRtoO.Checked Then
            Me.StatusRtoX = "Obsolete"
            RadioButtonStatusRtoO.Image = My.Resources.Checked
        Else
            RadioButtonStatusRtoO.Image = My.Resources.Unchecked
        End If
    End Sub
    Private Sub RadioButtonStatusRtoR_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonStatusRtoR.CheckedChanged
        If RadioButtonStatusRtoR.Checked Then
            Me.StatusRtoX = "Released"
            RadioButtonStatusRtoR.Image = My.Resources.Checked
        Else
            RadioButtonStatusRtoR.Image = My.Resources.Unchecked
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
            Dim UFL As New UtilsFileList(Me)
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

    End Sub

    Private Sub CLB_Properties_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CLB_Properties.ItemCheck

        For Each item In Me.ListOfColumns

            If item.Name = CLB_Properties.Items(e.Index).ToString Then
                item.Visible = CType(e.NewValue, Boolean)

                If ListViewFiles.Columns.Count > e.Index Then 'Insert this check in case of adding a new property the column doesn't exists yet
                    If Not item.Visible Then
                        ListViewFiles.Columns.Item(e.Index).Width = 0
                    Else
                        ListViewFiles.Columns.Item(e.Index).Width = item.Width
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

                If hitinfo.SubItem.BackColor.Name = "White" Or hitinfo.SubItem.BackColor.Name = "Gainsboro" Then    'Only non read-only file or non present properties can be changed
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

        End If

    End Sub

    Private Sub editbox_LostFocus(sender As Object, e As EventArgs)

        'Dim UC As New UtilsCommon

        Dim columnIndex As Integer = hitinfo.Item.SubItems.IndexOf(hitinfo.SubItem)
        Dim PropertySet As String = ""
        Dim PropertySetConstant As PropertyData.PropertySetNameConstants = PropertyData.PropertySetNameConstants.System
        Dim PropertyNameEnglish = ""
        Dim tmpPropertyData As PropertyData

        Dim PropertyName As String = hitinfo.Item.ListView.Columns.Item(columnIndex).Text

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

        Dim SSDoc As HCStructuredStorageDoc = Nothing

        Try
            SSDoc = New HCStructuredStorageDoc(FullName)
            SSDoc.ReadProperties(Me.PropertiesData)
            'SSDoc.ReadVariableNames()
        Catch ex As Exception
            ' Couldn't open read-write.  Nothing to do here.
            MsgBox("The property cannot be changed.  The file may be read-only or already open elsewhere.", vbOKOnly)
            Exit Sub
        End Try

        Dim tmpErrorLogger As New Logger("tmpLogger", Nothing)
        Dim Q = SSDoc.SubstitutePropertyFormulas(editbox.Text, tmpErrorLogger)

        Dim Success As Boolean = True

        If Q IsNot Nothing Then
            If SSDoc.SetPropValue(PropertySet, PropertyNameEnglish, Q, AddProperty:=True) Then
                hitinfo.SubItem.Text = Q
                hitinfo.SubItem.BackColor = Color.White

                SSDoc.Save()
            Else
                Success = False
            End If
        Else
            Success = False
        End If

        If Not Success Then MsgBox($"Unable to resolve '{editbox.Text}'", vbOKOnly)

        If SSDoc IsNot Nothing Then SSDoc.Close()

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
            If lvwColumnSorter.Order = SortOrder.Ascending Then
                lvwColumnSorter.Order = SortOrder.Descending
                ListViewFiles.Columns(e.Column).ImageKey = "Down"
            Else
                lvwColumnSorter.Order = SortOrder.Ascending
                ListViewFiles.Columns(e.Column).ImageKey = "Up"
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending.
            lvwColumnSorter.SortColumn = e.Column
            lvwColumnSorter.Order = SortOrder.Ascending
            ListViewFiles.Columns(e.Column).ImageKey = "Up"
        End If

        For i = 0 To ListViewFiles.Columns.Count - 1
            If i <> e.Column Then ListViewFiles.Columns(i).ImageKey = "" : ListViewFiles.Columns(i).TextAlign = HorizontalAlignment.Left
        Next

        ' Perform the sort with these new sort options.
        ListViewFiles.Sort()

    End Sub

    Private Sub ListViewFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewFiles.SelectedIndexChanged

        Label_FileCounter.Text = String.Format("{0} files - {1} selected", ListViewFiles.Items.Count, ListViewFiles.SelectedItems.Count)

        If ListViewFiles.SelectedItems.Count > 0 Then

            ButtonProcess.Text = "    Process selected"

            'If TextBoxStatus.Text.Contains(" - Selected files: ") Then
            '    Dim k = TextBoxStatus.Text.IndexOf(" - Selected files")
            '    TextBoxStatus.Text = TextBoxStatus.Text.Substring(0, k).TrimEnd() & " - Selected files: " & ListViewFiles.SelectedItems.Count.ToString
            'Else
            '    TextBoxStatus.Text = TextBoxStatus.Text & " - Selected files: " & ListViewFiles.SelectedItems.Count.ToString
            'End If

        Else

            ButtonProcess.Text = "Process"

            'If TextBoxStatus.Text.Contains(" - Selected files: ") Then
            '    Dim k = TextBoxStatus.Text.IndexOf(" - Selected files")
            '    TextBoxStatus.Text = TextBoxStatus.Text.Substring(0, k).TrimEnd()
            'End If

        End If

    End Sub

    Private Sub TextBoxServerConnectionString_TextChanged(sender As Object, e As EventArgs) Handles TextBoxServerConnectionString.TextChanged
        ServerConnectionString = TextBoxServerConnectionString.Text
    End Sub

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

            For Each tmpPreset In Me.Presets.Items
                If tmpPreset.Name = Name Then
                    GotAMatch = True
                    Exit For
                End If
            Next

            If GotAMatch Then
                Me.ActivePreset = Name

                'Dim tmpRememberTasks = Me.RememberTasks
                'Me.RememberTasks = True
                UP.SaveFormMainSettingsJSON(tmpPreset.FormSettingsJSON)
                'Me.RememberTasks = tmpRememberTasks

                UP.SaveTaskListJSON(tmpPreset.TaskListJSON)
                If Me.PresetsSavePropertyFilters Then
                    UP.SavePropertyFiltersJSON(tmpPreset.PropertyFiltersJSON)
                End If
                Me.Presets.Save()
                'SaveSettings()  ' Incorrect.  This saves the current settings

                Application.DoEvents()

                'Dim tmpRememberTasks = Me.RememberTasks
                'Me.RememberTasks = True
                Startup(SavingPresets:=True)
                'Me.RememberTasks = tmpRememberTasks
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

            Me.ActivePreset = Name

            'Dim tmpRememberTasks = Me.RememberTasks
            'Me.RememberTasks = True
            SaveSettings(SavingPresets:=True)  ' Updates the task list and form properties to their current state.
            'Me.RememberTasks = tmpRememberTasks

            tmpPreset.Name = Name
            tmpPreset.TaskListJSON = UP.GetTaskListJSON()
            tmpPreset.FormSettingsJSON = UP.GetFormMainSettingsJSON
            tmpPreset.PropertyFiltersJSON = UP.GetPropertyFiltersJSON
            tmpPreset.SavePropertyFilters = Me.PresetsSavePropertyFilters

            If Not GotAMatch Then
                Me.Presets.Items.Add(tmpPreset)
            End If

            Me.Presets = Me.Presets ' Triggers JSON update

            'SaveSettings()
            'Application.DoEvents()
            Presets.Save()

        Else
            MsgBox("Enter a name for the preset to save", vbOKOnly)
        End If


    End Sub

    Private Sub ButtonPresetDelete_Click(sender As Object, e As EventArgs) Handles ButtonPresetDelete.Click

        Dim Name As String = ComboBoxPresetName.Text
        Dim Idx As Integer = -1
        Dim UP As New UtilsPreferences

        For i As Integer = 0 To Me.Presets.Items.Count - 1
            If Me.Presets.Items(i).Name = Name Then
                Idx = i
                Exit For
            End If
        Next

        If Not Idx = -1 Then
            Me.Presets.Items.RemoveAt(Idx)
            Me.Presets = Me.Presets ' Trigger update
            Presets.Save()
        End If

        If ComboBoxPresetName.Items.Contains(Name) Then
            ComboBoxPresetName.Items.Remove(Name)
        End If
        ComboBoxPresetName.Text = ""

    End Sub

    Private Sub ButtonPresetsOptions_Click(sender As Object, e As EventArgs) Handles ButtonPresetsOptions.Click
        Dim FPO As New FormPresetsOptions(Me)
        FPO.ShowDialog()
        ' No result action needed.  FPO sets values in this file as needed.
    End Sub


    Public Shared Function ExecuteQuery(FullName As String, Query As String, ResultIndex As Integer) As String

        ExecuteQuery = ""

        Try

            'OLEDB Connections lile ACCESS and EXCEL file
            If Form_Main.TextBoxServerConnectionString.Text.Contains("OLEDB") Then

                Dim con As New OleDbConnection(Form_Main.TextBoxServerConnectionString.Text)
                con.Open()

                Dim cmd As New OleDbCommand(Query, con) 'TBD <--- Convert the property formula into text
                Dim reader As OleDbDataReader = cmd.ExecuteReader()

                If reader.HasRows Then
                    reader.Read()

                    ExecuteQuery = reader(ResultIndex - 1).ToString

                End If

                reader.Close()
                con.Close()

            Else

                'I suppose it is a SQL connection
                Dim con As New SqlConnection(Form_Main.TextBoxServerConnectionString.Text)
                con.Open()

                Dim cmd As New SqlCommand(Query, con)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                If reader.HasRows Then
                    reader.Read()

                    ExecuteQuery = reader(ResultIndex - 1).ToString

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

    Private Sub CheckBoxGroupFiles_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxGroupFiles.CheckedChanged
        Me.GroupFiles = CheckBoxGroupFiles.Checked
        ListViewFiles.ShowGroups = Me.GroupFiles

        ListViewFilesOutOfDate = True

    End Sub

    Private Sub ListViewFiles_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles ListViewFiles.DrawColumnHeader

        e.DrawDefault = True  ' Set ownerdraw.  ######## SET TO FALSE TO USE EXPERIMENTAL CODE

        '####### Scope is to have better column header style (the default has the vertical line missalligned
        '####### Second scope is to show the sorting order arrow on header


        '############## Experimental number 1 ##################################################################################################
        'e.Graphics.FillRectangle(SystemBrushes.Menu, e.Bounds)
        'e.Graphics.DrawRectangle(SystemPens.GradientInactiveCaption, New Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height))


        ''TEXT

        'Dim textAlign As HorizontalAlignment = e.Header.TextAlign
        'Dim flags As TextFormatFlags = If((textAlign = HorizontalAlignment.Left), TextFormatFlags.GlyphOverhangPadding, If((textAlign = HorizontalAlignment.Center), TextFormatFlags.HorizontalCenter, TextFormatFlags.Right))

        ''(I added this line)
        'flags = (flags Or TextFormatFlags.VerticalCenter)

        'Dim text As String = e.Header.Text
        'Dim width As Integer = TextRenderer.MeasureText(" ", e.Font).Width
        'Bounds = Rectangle.Inflate(e.Bounds, -width, 0)
        'TextRenderer.DrawText(e.Graphics, [text], e.Font, bounds, e.ForeColor, flags)
        '############################################################################################################################################




        '############## Experimental number 2 ##################################################################################################
        'Dim state = If(e.State = ListViewItemStates.Selected, VisualStyleElement.Header.Item.Hot, VisualStyleElement.Header.Item.Normal)
        'Dim sortOrder = If(lvwColumnSorter.Order = Windows.Forms.SortOrder.Descending, VisualStyleElement.Header.SortArrow.SortedUp, VisualStyleElement.Header.SortArrow.SortedDown)
        'Dim itemRenderer = New VisualStyleRenderer(state)
        'Dim sortRenderer = New VisualStyleRenderer(sortOrder)
        'Dim r = e.Bounds
        'r.X += 1

        'itemRenderer.DrawBackground(e.Graphics, r)
        'r.Inflate(-2, 0)
        'Dim flags1 = TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine
        'itemRenderer.DrawText(e.Graphics, r, e.Header.Text, False, flags1)
        'Dim d = SystemInformation.VerticalScrollBarWidth

        'If Not IsNothing(lvwColumnSorter) Then
        '    If e.ColumnIndex = lvwColumnSorter.SortColumn Then sortRenderer.DrawBackground(e.Graphics, New Rectangle(r.Right - d, r.Top, d, r.Height))
        'End If
        '############################################################################################################################################





    End Sub

    Private Sub ListViewFiles_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles ListViewFiles.DrawItem
        e.DrawDefault = True
    End Sub

    Private Sub ListViewFiles_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles ListViewFiles.DrawSubItem
        e.DrawDefault = True
    End Sub

    Private Sub CheckBoxKeepUnsortedDuplicates_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxKeepUnsortedDuplicates.CheckedChanged
        Me.KeepUnsortedDuplicates = CheckBoxKeepUnsortedDuplicates.Checked

        ListViewFilesOutOfDate = True
    End Sub

    Private Sub CheckBoxKeepDuplicates_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub CheckBoxRemindFilelistUpdate_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRemindFilelistUpdate.CheckedChanged
        Me.RemindFilelistUpdate = CheckBoxRemindFilelistUpdate.Checked
    End Sub

    Private Sub CheckBoxProcessDraftsInactive_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxProcessDraftsInactive.CheckedChanged
        Me.ProcessDraftsInactive = CheckBoxProcessDraftsInactive.Checked
    End Sub

    Private Sub ComboBoxExpressionEditorLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxExpressionEditorLanguage.SelectedIndexChanged
        Me.ExpressionEditorLanguage = ComboBoxExpressionEditorLanguage.Text
    End Sub

    Private Sub BT_Copy_Click(sender As Object, e As EventArgs) Handles BT_Copy.Click

        Dim ColonneNascoste As New List(Of Integer)

        For Each item As ColumnHeader In ListViewFiles.Columns

            If item.Width = 0 Then ColonneNascoste.Add(item.Index)

        Next

        Dim tmpText As String = ""

        For Each item As ListViewItem In ListViewFiles.SelectedItems

            Dim i = 0

            For Each subItem As ListViewItem.ListViewSubItem In item.SubItems

                If Not ColonneNascoste.Contains(i) Then tmpText &= subItem.Text & vbTab
                i += 1

            Next

            tmpText = tmpText.TrimEnd(CType(vbTab, Char)) & vbCrLf

        Next

        Clipboard.Clear()
        Clipboard.SetText(tmpText)

    End Sub

    Private Sub BT_SelectAll_Click(sender As Object, e As EventArgs) Handles BT_SelectAll.Click

        For Each item As ListViewItem In ListViewFiles.Items
            item.Selected = True
        Next

    End Sub

    Private Sub CheckBoxDebugMode_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDebugMode.CheckedChanged
        Me.DebugMode = CheckBoxDebugMode.Checked
    End Sub

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



' Commands I can never remember


' ###### FILE/DIRECTORY EXISTS, CREATE, DELETE, COPY ######

' tf = System.IO.File.Exists(Filename)

' tf = System.IO.Directory.Exists(Dirname)

' If Not System.IO.Directory.Exists(Dirname) Then
'     System.IO.Directory.CreateDirectory(Dirname)  ' Creates all parent directories if they do not exist.
' End If

' Dim DI As New System.IO.DirectoryInfo(Dirname)
' For Each File As System.IO.FileInfo In DI.GetFiles
'     File.Delete()
' Next

'System.IO.File.Copy(SettingsFilename, NewSettingsFilename)


' ###### FILE FILENAME, EXTENSION, DIRECTORY, STARTUP PATH ######

' BaseFilename = System.IO.Path.GetFileName(SEDoc.FullName)  ' C:\project\part.par -> part.par

' BaseName = System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName)  ' C:\project\part.par -> part

' Extension = System.IO.Path.GetExtension(WhereUsedFile)  ' C:\project\part.par -> .par

' DirName = System.IO.Path.GetDirectoryName(SEDoc.FullName)  ' C:\project\part.par -> C:\project

' Dim DrawingFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, ".dft")

' Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()


' ###### TEXT FILE IO ######

' Dim Defaults As List(Of String) = System.IO.File.ReadAllLines(DefaultsFilename).ToList

' Dim Defaults As New List(Of String)
' System.IO.File.WriteAllLines(DefaultsFilename, Defaults)

' IO.File.WriteAllText(Filename, JSONString)


' ###### DOEVENTS, SLEEP ######

' System.Windows.Forms.Application.DoEvents()

' System.Threading.Thread.Sleep(100)


' ###### TYPES, ENUMS ######

' PropTypeName = Prop.Value.GetType().Name
' The preceeding syntax is probably better than:  TypeName = Microsoft.VisualBasic.Information.TypeName(SEDoc)

' Check COM types
' Select Case HCComObject.GetCOMObjectType(objVar)
'    Case GetType(SolidEdgeFramework.variable)
'        Dim tmpVar = CType(objVar, SolidEdgeFramework.variable)
'        UnitTypeConstant = CType(tmpVar.UnitsType, SolidEdgeFramework.UnitTypeConstants)
'    Case GetType(SolidEdgeFrameworkSupport.Dimension)
'        Dim tmpDim = CType(objVar, SolidEdgeFrameworkSupport.Dimension)
'        UnitTypeConstant = CType(tmpDim.UnitsType, SolidEdgeFramework.UnitTypeConstants)
'    Case Else
'        MsgBox(String.Format("Unrecognized variable type '{0}'", objVar.GetType.ToString))
' End Select

' Iterate through an Enum
' For Each PaperSizeConstant In System.Enum.GetValues(GetType(SolidEdgeDraft.PaperSizeConstants))


' ###### CURSOR AND WINDOW ######

'Me.Cursor = Cursors.WaitCursor
'Me.Cursor = Cursors.Default

'ActiveWindow.WindowState = 2  '0 normal, 1 minimized, 2 maximized


' ###### JSON ######

' JSONString = JsonConvert.SerializeObject(Me.SomeDict) Then

' SomeDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(JSONString)


' ###### ENVIRONMENT VARIABLES: USER NAME, DATETIME  ######

' Dim UserName As String = Environment.UserName
''Dim UserName As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name
' Dim StatusChangeDate = DateTime.Now

