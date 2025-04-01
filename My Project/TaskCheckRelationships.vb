Option Strict On

Public Class TaskCheckRelationships
    Inherits Task

    Private _CheckFailed As Boolean
    Public Property CheckFailed As Boolean
        Get
            Return _CheckFailed
        End Get
        Set(value As Boolean)
            _CheckFailed = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.CheckFailed.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _CheckUnderconstrained As Boolean
    Public Property CheckUnderconstrained As Boolean
        Get
            Return _CheckUnderconstrained
        End Get
        Set(value As Boolean)
            _CheckUnderconstrained = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.CheckUnderconstrained.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _CheckSuppressed As Boolean
    Public Property CheckSuppressed As Boolean
        Get
            Return _CheckSuppressed
        End Get
        Set(value As Boolean)
            _CheckSuppressed = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.CheckSuppressed.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _AutoHideOptions As Boolean
    Public Property AutoHideOptions As Boolean
        Get
            Return _AutoHideOptions
        End Get
        Set(value As Boolean)
            _AutoHideOptions = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AutoHideOptions.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property



    Enum ControlNames
        CheckFailed
        CheckUnderconstrained
        CheckSuppressed
        AutoHideOptions
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

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.CheckFailed = False
        Me.CheckUnderconstrained = False
        Me.CheckSuppressed = False

    End Sub

    Public Overrides Sub Process(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application)

        Me.TaskLogger = Me.FileLogger.AddLogger(Me.Description)

        InvokeSTAThread(
            Of SolidEdgeFramework.SolidEdgeDocument,
            SolidEdgeFramework.Application)(
                AddressOf ProcessInternal,
                SEDoc,
                SEApp)
    End Sub

    Public Overrides Sub Process(ByVal FileName As String)
        Me.TaskLogger = Me.FileLogger.AddLogger(Me.Description)
    End Sub

    Private Sub ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        Dim Sketches As SolidEdgePart.Sketchs = Nothing
        Dim Models As SolidEdgePart.Models = Nothing
        Dim ProfileSets As SolidEdgePart.ProfileSets = Nothing

        Dim RefPlanesWithUnderconstrainedProfiles As New List(Of SolidEdgePart.RefPlane)

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Dim UF As New UtilsFeatures

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

            Dim SubLogger As Logger = TaskLogger.AddLogger(CheckItem)

            Select Case DocType

                Case = "asm"
                    Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

                    Dim SketchesAssembly As SolidEdgeAssembly.ComponentLayouts
                    Dim SketchAssembly As SolidEdgeAssembly.Layout

                    ' Causes an invalid cast on files with sketches
                    ' of type SolidEdgeAssembly.ComponentLayout
                    ' rather than SolidEdgeAssembly.Layout
                    ' At least in SE2024, ComponentLayouts and Layouts hold the same information
                    ' They can have a collection of Layout, which are Assembly Sketches
                    ' and also can have a collection of ComponentLayout, which are Component Sketches.
                    ' (Component Sketches are part of the Virtual Component workflow and don't appear in PathFinder)
                    Try
                        SketchesAssembly = tmpSEDoc.ComponentLayouts
                        For Each SketchAssembly In SketchesAssembly
                            Dim Status = SketchAssembly.Status
                            If CheckItem = "Failed relationships" Then
                                tf = (Status = SolidEdgePart.FeatureStatusConstants.igFeatureFailed)
                                tf = tf Or (Status = SolidEdgePart.FeatureStatusConstants.igFeatureWarned)
                                If tf Then
                                    SubLogger.AddMessage(SketchAssembly.Name)
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
                                    SubLogger.AddMessage(SketchAssembly.Name)
                                End If
                            End If

                        Next

                    Catch ex As Exception
                        s = "Unable to process sketches"
                        If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                    End Try


                    Dim Occurrences As SolidEdgeAssembly.Occurrences = tmpSEDoc.Occurrences
                    Dim Occurrence As SolidEdgeAssembly.Occurrence

                    For Each Occurrence In Occurrences
                        If Not (Occurrence.Adjustable Or Occurrence.IsAdjustablePart) Then
                            If CheckItem = "Failed relationships" Then
                                tf = Occurrence.Status = SolidEdgeAssembly.OccurrenceStatusConstants.seOccurrenceStatusOverDefined
                                tf = tf Or (Occurrence.Status = SolidEdgeAssembly.OccurrenceStatusConstants.seOccurrenceStatusNotConsistent)
                                If tf Then
                                    SubLogger.AddMessage(Occurrence.Name)
                                End If
                            End If

                            If CheckItem = "Underconstrained relationships" Then
                                If Occurrence.Status = SolidEdgeAssembly.OccurrenceStatusConstants.seOccurrenceStatusUnderDefined Then
                                    SubLogger.AddMessage(Occurrence.Name)
                                End If
                            End If

                        End If
                    Next

                    If CheckItem = "Suppressed relationships" Then
                        'https://community.sw.siemens.com/s/question/0D5Vb00000TenJTKAZ/unsuppress-occurrences-from-api
                        Dim Count As Integer
                        Dim ComponentType = SolidEdgeAssembly.AssemblyComponentTypeConstants.seAssemblyComponentTypeAll
                        Dim Components As System.Array = System.Array.CreateInstance(GetType(SolidEdgeAssembly.SuppressComponent), 0)

                        tmpSEDoc.GetSuppressedComponents(ComponentType, Count, Components)

                        If Components IsNot Nothing AndAlso Count > 0 Then
                            For Each tmpSC As SolidEdgeAssembly.SuppressComponent In Components
                                SubLogger.AddMessage(tmpSC.Name)
                            Next
                        End If

                    End If

                Case = "par"
                    Dim tmpSEDoc As SolidEdgePart.PartDocument = CType(SEDoc, SolidEdgePart.PartDocument)
                    Models = tmpSEDoc.Models
                    Sketches = tmpSEDoc.Sketches
                    ProfileSets = tmpSEDoc.ProfileSets

                Case = "psm"
                    Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                    Models = tmpSEDoc.Models
                    Sketches = tmpSEDoc.Sketches
                    ProfileSets = tmpSEDoc.ProfileSets

                Case Else
                    MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
            End Select

            If (DocType = "par") Or (DocType = "psm") Then

                RefPlanesWithUnderconstrainedProfiles = GetRefPlanesWithUnderconstrainedProfiles(ProfileSets)

                CheckSketches(CheckItem, Sketches, SubLogger)
                CheckFeatures(CheckItem, Models, RefPlanesWithUnderconstrainedProfiles, SubLogger)

            End If
        Next

    End Sub

    Private Function GetSuppressedComponents(
        tmpSEDoc As SolidEdgeAssembly.AssemblyDocument
        ) As List(Of SolidEdgeAssembly.Occurrence)

        Dim SuppressedOccurrences As New List(Of SolidEdgeAssembly.Occurrence)

        Dim ComponentType As SolidEdgeAssembly.AssemblyComponentTypeConstants
        ComponentType = SolidEdgeAssembly.AssemblyComponentTypeConstants.seAssemblyComponentTypeAll
        Dim ComponentCount As Integer
        'Dim SuppressedComponents() As Object = Nothing
        Dim SuppressedComponents As Array = Nothing
        tmpSEDoc.GetSuppressedComponents(ComponentType, ComponentCount, SuppressedComponents)

        For Each Component As Object In SuppressedComponents
            Try
                Dim tmpComponent As SolidEdgeAssembly.Occurrence = CType(Component, SolidEdgeAssembly.Occurrence)
                SuppressedOccurrences.Add(tmpComponent)
            Catch ex As Exception

            End Try
        Next

        Return SuppressedOccurrences
    End Function

    Private Sub CheckFeatures(
        CheckItem As String,
        Models As SolidEdgePart.Models,
        RefPlanesWithUnderconstrainedProfiles As List(Of SolidEdgePart.RefPlane),
        SubLogger As Logger)

        Dim s As String
        Dim tf As Boolean

        Dim Model As SolidEdgePart.Model
        Dim Features As SolidEdgePart.Features
        Dim Profile As SolidEdgePart.Profile

        Dim FeatureTypeConstant As SolidEdgePart.FeatureTypeConstants
        Dim Status As SolidEdgePart.FeatureStatusConstants
        Dim RefPlane As SolidEdgePart.RefPlane

        Dim UF As New UtilsFeatures

        If (Models IsNot Nothing) Then

            If Models.Count > 300 Then
                SubLogger.AddMessage(String.Format("{0} models exceeds maximum to process", Models.Count.ToString))
            End If

            If (Models.Count > 0) And (Models.Count <= 300) Then
                For Each Model In Models
                    Features = Model.Features
                    For Each Feature In Features
                        If UF.IsOrdered(Feature) Then
                            Dim Name = UF.GetName(Feature)
                            FeatureTypeConstant = UF.GetFeatureType(Feature)
                            If FeatureTypeConstant = Nothing Then
                                Continue For
                            End If
                            s = String.Format("SolidEdgePart.{0}", Name)

                            'https://stackoverflow.com/questions/15252266/ctype-in-vb-net-with-dynamic-second-parameter-type
                            Dim FeatureType As Type = UF.GetTypeFromFeature(Feature)
                            If FeatureType Is Nothing Then
                                Continue For
                            End If

                            Dim _Feature = CTypeDynamic(Feature, FeatureType)
                            Status = UF.GetStatus(_Feature)
                            Dim _FeatureName = UF.GetName(_Feature)

                            If CheckItem = "Failed relationships" Then
                                tf = (Status = SolidEdgePart.FeatureStatusConstants.igFeatureFailed)
                                tf = tf Or (Status = SolidEdgePart.FeatureStatusConstants.igFeatureWarned)
                                If tf Then
                                    SubLogger.AddMessage(_FeatureName)
                                End If
                            End If

                            If CheckItem = "Underconstrained relationships" Then
                                'RefPlane = Nothing
                                ' UserDefinedPatterns (eg Hole Pattern) are different
                                If FeatureTypeConstant = SolidEdgePart.FeatureTypeConstants.igUserDefinedPatternFeatureObject Then
                                    RefPlane = UF.GetPatternPlane(_Feature)
                                Else
                                    ' Some features do not have a profile
                                    Try
                                        Profile = UF.GetProfile(_Feature)
                                        If Profile Is Nothing Then
                                            Continue For
                                        End If
                                        'RefPlane = GetProfilePlane(Profile)
                                        RefPlane = CType(Profile.Plane, SolidEdgePart.RefPlane)

                                    Catch ex As Exception
                                        Continue For
                                    End Try
                                End If
                                If RefPlanesWithUnderconstrainedProfiles.Contains(RefPlane) Then
                                    SubLogger.AddMessage(_FeatureName)
                                End If
                            End If

                            If CheckItem = "Suppressed relationships" Then
                                tf = (Status = SolidEdgePart.FeatureStatusConstants.igFeatureSuppressed)
                                tf = tf Or (Status = SolidEdgePart.FeatureStatusConstants.igFeatureRolledBack)
                                If tf Then
                                    SubLogger.AddMessage(_FeatureName)
                                End If
                            End If

                        End If
                    Next
                Next
            End If
        End If

    End Sub

    Private Sub CheckSketches(
        CheckItem As String,
        Sketches As SolidEdgePart.Sketchs,
        SubLogger As Logger)

        Dim tf As Boolean
        Dim Sketch As SolidEdgePart.Sketch

        If Sketches IsNot Nothing Then
            For Each Sketch In Sketches
                Dim StatusEx As SolidEdgePart.FeatureStatusConstants = Sketch.GetStatusEx

                If CheckItem = "Failed relationships" Then
                    tf = (StatusEx = SolidEdgePart.FeatureStatusConstants.igFeatureFailed)
                    tf = tf Or (StatusEx = SolidEdgePart.FeatureStatusConstants.igFeatureWarned)
                    If tf Then
                        SubLogger.AddMessage(Sketch.Name)
                    End If
                End If
                If CheckItem = "Underconstrained relationships" Then
                    If Sketch.IsUnderDefined Then
                        SubLogger.AddMessage(Sketch.Name)
                    End If
                End If
                If CheckItem = "Suppressed relationships" Then
                    tf = (StatusEx = SolidEdgePart.FeatureStatusConstants.igFeatureSuppressed)
                    tf = tf Or (StatusEx = SolidEdgePart.FeatureStatusConstants.igFeatureRolledBack)
                    If tf Then
                        SubLogger.AddMessage(Sketch.Name)
                    End If
                End If

            Next
        End If


    End Sub

    Private Function GetRefPlanesWithUnderconstrainedProfiles(
        ProfileSets As SolidEdgePart.ProfileSets
        ) As List(Of SolidEdgePart.RefPlane)

        Dim RefPlanesWithUnderconstrainedProfiles As New List(Of SolidEdgePart.RefPlane)
        Dim Profiles As SolidEdgePart.Profiles
        Dim RefPlane As SolidEdgePart.RefPlane

        For Each ProfileSet As SolidEdgePart.ProfileSet In ProfileSets
            If ProfileSet.IsUnderDefined Then
                Profiles = ProfileSet.Profiles
                For Each Profile As SolidEdgePart.Profile In Profiles
                    RefPlane = CType(Profile.Plane, SolidEdgePart.RefPlane)
                    If Not RefPlanesWithUnderconstrainedProfiles.Contains(RefPlane) Then
                        RefPlanesWithUnderconstrainedProfiles.Add(RefPlane)
                    End If
                Next
            End If
        Next

        Return RefPlanesWithUnderconstrainedProfiles

    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        'Dim IU As New InterfaceUtilities

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.CheckFailed.ToString, "Check failed relationships")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.CheckUnderconstrained.ToString, "Check underconstrained relationships")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.CheckSuppressed.ToString, "Check suppressed relationships")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
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

        CheckBox = CType(ControlsDict(ControlNames.AutoHideOptions.ToString), CheckBox)
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

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Checks if the file has any failed, underconstrained, or suppressed relationships."

        HelpString += vbCrLf + vbCrLf + "![CheckRelationships](My%20Project/media/task_check_relationships.png)"


        Return HelpString
    End Function

End Class
