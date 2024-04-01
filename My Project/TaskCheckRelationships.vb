Option Strict On
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports SolidEdgePart

Public Class TaskCheckRelationships
    Inherits Task

    Public Property CheckFailed As Boolean
    Public Property CheckUnderconstrained As Boolean
    Public Property CheckSuppressed As Boolean

    Enum ControlNames
        CheckFailed
        CheckUnderconstrained
        CheckSuppressed
        HideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = False
        Me.AppliesToAssembly = True
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = False
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskCheckRelationships
        Me.Category = "Check"

        SetColorFromCategory(Me)

        ' Options

        Me.CheckFailed = False
        Me.CheckUnderconstrained = False
        Me.CheckSuppressed = False
    End Sub

    Public Sub New(Task As TaskCheckRelationships)

        ' Options
        Me.CheckFailed = Task.CheckFailed
        Me.CheckUnderconstrained = Task.CheckUnderconstrained
        Me.CheckSuppressed = Task.CheckSuppressed
    End Sub


    Public Overrides Function Process(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeFramework.SolidEdgeDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf ProcessInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))


        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Sketches As SolidEdgePart.Sketchs = Nothing
        Dim Models As SolidEdgePart.Models = Nothing
        Dim ProfileSets As SolidEdgePart.ProfileSets = Nothing

        Dim RefPlanesWithUnderconstrainedProfiles As New List(Of SolidEdgePart.RefPlane)

        Dim TC As New Task_Common
        Dim DocType As String = TC.GetDocType(SEDoc)

        Dim FeatureDoctor As New FeatureDoctor

        Dim ListIndex As Integer

        Dim CheckItem As String
        Dim CheckItems As List(Of String) = {"Failed relationships", "Underconstrained relationships", "Suppressed relationships"}.ToList

        Dim CheckOptions As List(Of Boolean) = {Me.CheckFailed, Me.CheckUnderconstrained, Me.CheckSuppressed}.ToList

        Dim s As String
        Dim tf As Boolean

        For ListIndex = 0 To CheckItems.Count - 1

            If Not CheckOptions(ListIndex) Then
                Continue For
            End If

            CheckItem = CheckItems(ListIndex)

            Select Case DocType

                Case = "asm"
                    Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

                    Dim SketchesAssembly As SolidEdgeAssembly.ComponentLayouts
                    Dim SketchAssembly As SolidEdgeAssembly.Layout
                    'Dim Profiles As SolidEdgePart.Profiles
                    'Dim Profile As SolidEdgePart.Profile

                    ' Causes an invalid cast on files with sketches
                    ' of type SolidEdgeAssembly.ComponentLayout
                    ' rather than SolidEdgeAssembly.Layout
                    Try
                        SketchesAssembly = tmpSEDoc.ComponentLayouts
                        For Each SketchAssembly In SketchesAssembly
                            Dim Status = SketchAssembly.Status
                            If CheckItem = "Failed relationships" Then
                                tf = (Status = SolidEdgePart.FeatureStatusConstants.igFeatureFailed)
                                tf = tf Or (Status = SolidEdgePart.FeatureStatusConstants.igFeatureWarned)
                                If tf Then
                                    ExitStatus = 1
                                    UpdateErrorMessageList(ErrorMessageList, SketchAssembly.Name, True, CheckItem)
                                End If
                            End If

                            If CheckItem = "Underconstrained relationships" Then
                                ' Haven't found a way
                                'Profile = CType(SketchAssembly.Profile, SolidEdgePart.Profile)
                            End If

                            If CheckItem = "Suppressed relationships" Then
                                tf = (Status = SolidEdgePart.FeatureStatusConstants.igFeatureSuppressed)
                                tf = tf Or (Status = SolidEdgePart.FeatureStatusConstants.igFeatureRolledBack)
                                If tf Then
                                    ExitStatus = 1
                                    UpdateErrorMessageList(ErrorMessageList, SketchAssembly.Name, True, CheckItem)
                                End If
                            End If

                        Next

                    Catch ex As Exception
                        ExitStatus = 1
                        s = "Unable to process sketches"
                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                    End Try


                    Dim Occurrences As SolidEdgeAssembly.Occurrences = tmpSEDoc.Occurrences
                    Dim Occurrence As SolidEdgeAssembly.Occurrence

                    For Each Occurrence In Occurrences
                        If Not (Occurrence.Adjustable Or Occurrence.IsAdjustablePart) Then
                            If CheckItem = "Failed relationships" Then
                                tf = Occurrence.Status = SolidEdgeAssembly.OccurrenceStatusConstants.seOccurrenceStatusOverDefined
                                tf = tf Or (Occurrence.Status = SolidEdgeAssembly.OccurrenceStatusConstants.seOccurrenceStatusNotConsistent)
                                If tf Then
                                    ExitStatus = 1
                                    UpdateErrorMessageList(ErrorMessageList, Occurrence.Name, True, CheckItem)
                                End If
                            End If

                            If CheckItem = "Underconstrained relationships" Then
                                If Occurrence.Status = SolidEdgeAssembly.OccurrenceStatusConstants.seOccurrenceStatusUnderDefined Then
                                    ExitStatus = 1
                                    UpdateErrorMessageList(ErrorMessageList, Occurrence.Name, True, CheckItem)
                                End If
                            End If

                            If CheckItem = "Suppressed relationships" Then
                                ' Haven't found a way.  The Occurrences collection does not show suppressed parts.
                            End If
                        End If
                    Next

                Case = "par"
                    Dim tmpSEDoc As SolidEdgePart.PartDocument = CType(SEDoc, PartDocument)
                    Models = tmpSEDoc.Models
                    Sketches = tmpSEDoc.Sketches
                    ProfileSets = tmpSEDoc.ProfileSets

                Case = "psm"
                    Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SheetMetalDocument)
                    Models = tmpSEDoc.Models
                    Sketches = tmpSEDoc.Sketches
                    ProfileSets = tmpSEDoc.ProfileSets

                Case Else
                    MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
            End Select

            If (DocType = "par") Or (DocType = "psm") Then

                RefPlanesWithUnderconstrainedProfiles = GetRefPlanesWithUnderconstrainedProfiles(ProfileSets)

                CheckSketches(ExitStatus, ErrorMessageList, CheckItem, Sketches)
                CheckFeatures(ExitStatus, ErrorMessageList, CheckItem, Models, RefPlanesWithUnderconstrainedProfiles)
                'If CheckItem = "Failed relationships" Then
                'End If
                'If CheckItem = "Underconstrained relationships" Then
                '    CheckSketches(ExitStatus, ErrorMessageList, CheckItem, CheckUnderconstrained, Sketches)
                '    CheckFeatures(ExitStatus, ErrorMessageList, CheckItem, CheckUnderconstrained, Models, RefPlanesWithUnderconstrainedProfiles)
                'End If
                'If CheckItem = "Suppressed relationships" Then
                '    CheckSketches(ExitStatus, ErrorMessageList, CheckItem, CheckSuppressed, Sketches)
                '    CheckFeatures(ExitStatus, ErrorMessageList, CheckItem, CheckSuppressed, Models, RefPlanesWithUnderconstrainedProfiles)
                'End If

            End If
        Next

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage


    End Function


    Private Sub CheckFeatures(
        ByRef ExitStatus As Integer,
        ByRef ErrorMessageList As List(Of String),
        CheckItem As String,
        Models As SolidEdgePart.Models,
        RefPlanesWithUnderconstrainedProfiles As List(Of SolidEdgePart.RefPlane))

        Dim s As String
        Dim tf As Boolean


        Dim Model As SolidEdgePart.Model
        Dim Features As SolidEdgePart.Features
        Dim Profile As SolidEdgePart.Profile

        Dim FeatureTypeConstant As SolidEdgePart.FeatureTypeConstants
        Dim Status As SolidEdgePart.FeatureStatusConstants
        Dim RefPlane As SolidEdgePart.RefPlane

        Dim FeatureDoctor As New FeatureDoctor

        If (Models IsNot Nothing) Then

            If Models.Count > 300 Then
                ExitStatus = 1
                s = String.Format("{0} models exceeds maximum to process", Models.Count.ToString)
                UpdateErrorMessageList(ErrorMessageList, s, True, CheckItem)
            End If

            If (Models.Count > 0) And (Models.Count <= 300) Then
                For Each Model In Models
                    Features = Model.Features
                    For Each Feature In Features
                        If FeatureDoctor.IsOrdered(Feature) Then
                            Dim Name = FeatureDoctor.GetName(Feature)
                            FeatureTypeConstant = FeatureDoctor.GetFeatureType(Feature)
                            If FeatureTypeConstant = Nothing Then
                                Continue For
                            End If
                            s = String.Format("SolidEdgePart.{0}", Name)

                            'https://stackoverflow.com/questions/15252266/ctype-in-vb-net-with-dynamic-second-parameter-type
                            Dim FeatureType As Type = FeatureDoctor.GetTypeFromFeature(Feature)
                            If FeatureType Is Nothing Then
                                Continue For
                            End If

                            Dim _Feature = CTypeDynamic(Feature, FeatureType)
                            Status = FeatureDoctor.GetStatus(_Feature)
                            Dim _FeatureName = FeatureDoctor.GetName(_Feature)

                            If CheckItem = "Failed relationships" Then
                                tf = (Status = SolidEdgePart.FeatureStatusConstants.igFeatureFailed)
                                tf = tf Or (Status = SolidEdgePart.FeatureStatusConstants.igFeatureWarned)
                                If tf Then
                                    ExitStatus = 1
                                    UpdateErrorMessageList(ErrorMessageList, _FeatureName, True, CheckItem)
                                End If
                            End If

                            If CheckItem = "Underconstrained relationships" Then
                                'RefPlane = Nothing
                                ' UserDefinedPatterns (eg Hole Pattern) are different
                                If FeatureTypeConstant = SolidEdgePart.FeatureTypeConstants.igUserDefinedPatternFeatureObject Then
                                    RefPlane = FeatureDoctor.GetPatternPlane(_Feature)
                                Else
                                    ' Some features do not have a profile
                                    Try
                                        Profile = FeatureDoctor.GetProfile(_Feature)
                                        If Profile Is Nothing Then
                                            Continue For
                                        End If
                                        RefPlane = GetProfilePlane(Profile)
                                    Catch ex As Exception
                                        Continue For
                                    End Try
                                End If
                                If RefPlanesWithUnderconstrainedProfiles.Contains(RefPlane) Then
                                    ExitStatus = 1
                                    UpdateErrorMessageList(ErrorMessageList, _FeatureName, True, CheckItem)
                                End If
                            End If

                            If CheckItem = "Suppressed relationships" Then
                                tf = (Status = SolidEdgePart.FeatureStatusConstants.igFeatureSuppressed)
                                tf = tf Or (Status = SolidEdgePart.FeatureStatusConstants.igFeatureRolledBack)
                                If tf Then
                                    ExitStatus = 1
                                    UpdateErrorMessageList(ErrorMessageList, _FeatureName, True, CheckItem)
                                End If
                            End If

                        End If
                    Next
                Next
            ElseIf Models.Count >= 300 Then
                ExitStatus = 1
                s = String.Format("{0} models exceeds maximum to process", Models.Count.ToString)
                UpdateErrorMessageList(ErrorMessageList, s, True, CheckItem)
            End If
        End If

    End Sub

    Private Sub CheckSketches(
        ByRef ExitStatus As Integer,
        ByRef ErrorMessageList As List(Of String),
        CheckItem As String,
        Sketches As SolidEdgePart.Sketchs)

        Dim tf As Boolean
        Dim Sketch As SolidEdgePart.Sketch

        If Sketches IsNot Nothing Then
            For Each Sketch In Sketches
                Dim StatusEx As SolidEdgePart.FeatureStatusConstants = Sketch.GetStatusEx

                If CheckItem = "Failed relationships" Then
                    tf = (StatusEx = SolidEdgePart.FeatureStatusConstants.igFeatureFailed)
                    tf = tf Or (StatusEx = SolidEdgePart.FeatureStatusConstants.igFeatureWarned)
                    If tf Then
                        ExitStatus = 1
                        UpdateErrorMessageList(ErrorMessageList, Sketch.Name, True, CheckItem)
                    End If
                End If
                If CheckItem = "Underconstrained relationships" Then
                    If Sketch.IsUnderDefined Then
                        ExitStatus = 1
                        UpdateErrorMessageList(ErrorMessageList, Sketch.Name, True, CheckItem)
                    End If
                End If
                If CheckItem = "Suppressed relationships" Then
                    tf = (StatusEx = SolidEdgePart.FeatureStatusConstants.igFeatureSuppressed)
                    tf = tf Or (StatusEx = SolidEdgePart.FeatureStatusConstants.igFeatureRolledBack)
                    If tf Then
                        ExitStatus = 1
                        UpdateErrorMessageList(ErrorMessageList, Sketch.Name, True, CheckItem)
                    End If
                End If

            Next
        End If


    End Sub

    Private Function GetRefPlanesWithUnderconstrainedProfiles(
        ProfileSets As SolidEdgePart.ProfileSets
        ) As List(Of SolidEdgePart.RefPlane)

        Dim RefPlanesWithUnderconstrainedProfiles As New List(Of SolidEdgePart.RefPlane)
        Dim ProfileSet As SolidEdgePart.ProfileSet
        Dim Profiles As SolidEdgePart.Profiles
        Dim Profile As SolidEdgePart.Profile
        'Dim RefPlane As SolidEdgePart.RefPlane

        For Each ProfileSet In ProfileSets
            If ProfileSet.IsUnderDefined Then
                Profiles = ProfileSet.Profiles
                For Each Profile In Profiles
                    'RefPlane = CType(Profile.Plane, RefPlane)
                    If Not RefPlanesWithUnderconstrainedProfiles.Contains(GetProfilePlane(Profile)) Then
                        RefPlanesWithUnderconstrainedProfiles.Add(GetProfilePlane(Profile))
                    End If
                Next
            End If

        Next

        Return RefPlanesWithUnderconstrainedProfiles
    End Function


    Public Overrides Function GetTLPTask(TLPParent As ExTableLayoutPanel) As ExTableLayoutPanel
        ControlsDict = New Dictionary(Of String, Control)

        Dim IU As New InterfaceUtilities

        Me.TLPTask = IU.BuildTLPTask(Me, TLPParent)

        Me.TLPOptions = BuildTLPOptions()

        For Each Control As Control In Me.TLPTask.Controls
            If ControlsDict.Keys.Contains(Control.Name) Then
                MsgBox(String.Format("ControlsDict already has Key '{0}'", Control.Name))
            End If
            ControlsDict(Control.Name) = Control
        Next

        Me.TLPTask.Controls.Add(TLPOptions, Me.TLPTask.ColumnCount - 2, 1)

        Return Me.TLPTask
    End Function

    Private Function BuildTLPOptions() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        Dim IU As New InterfaceUtilities

        IU.FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.CheckFailed.ToString, "Check failed relationships")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.CheckUnderconstrained.ToString, "Check underconstrained relationships")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.CheckSuppressed.ToString, "Check suppressed relationships")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.HideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Private Sub InitializeOptionProperties()
        Dim CheckBox As CheckBox

        CheckBox = CType(ControlsDict(ControlNames.CheckFailed.ToString), CheckBox)
        Me.CheckFailed = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.CheckUnderconstrained.ToString), CheckBox)
        Me.CheckUnderconstrained = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.CheckSuppressed.ToString), CheckBox)
        Me.CheckSuppressed = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.HideOptions.ToString), CheckBox)
        Me.AutoHideOptions = CheckBox.Checked

    End Sub

    Public Overrides Function CheckStartConditions(
        PriorErrorMessage As Dictionary(Of Integer, List(Of String))
        ) As Dictionary(Of Integer, List(Of String))

        Dim PriorExitStatus As Integer = PriorErrorMessage.Keys(0)

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim ExitStatus As Integer = 0
        Dim ErrorMessageList = PriorErrorMessage(PriorExitStatus)
        Dim Indent = "    "

        If Me.IsSelectedTask Then
            ' Check start conditions.
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one type of file to process", Indent))
            End If

            If Not (Me.CheckFailed Or Me.CheckUnderconstrained Or Me.CheckSuppressed) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one type of check to perform", Indent))
            End If

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.CheckFailed.ToString
                Me.CheckFailed = Checkbox.Checked


            Case ControlNames.CheckUnderconstrained.ToString
                Me.CheckUnderconstrained = Checkbox.Checked

            Case ControlNames.CheckSuppressed.ToString
                Me.CheckSuppressed = Checkbox.Checked

            Case ControlNames.HideOptions.ToString
                HandleHideOptionsChange(Me, Me.TLPTask, Me.TLPOptions, Checkbox)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Checks if the file has any conflicting, underconstrained, or suppressed relationships."

        Return HelpString
    End Function

End Class
