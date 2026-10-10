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

        OleMessageFilter.Register()

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Dim CheckItems As List(Of String) = {"Failed relationships", "Underconstrained relationships", "Suppressed relationships"}.ToList
        Dim CheckOptions As List(Of Boolean) = {Me.CheckFailed, Me.CheckUnderconstrained, Me.CheckSuppressed}.ToList

        Dim Sketches As SolidEdgePart.Sketchs = Nothing
        Dim Models As SolidEdgePart.Models = Nothing
        Dim ProfileSets As SolidEdgePart.ProfileSets = Nothing

        For ListIndex As Integer = 0 To CheckItems.Count - 1

            If Not CheckOptions(ListIndex) Then
                Continue For
            End If

            Dim CheckItem As String = CheckItems(ListIndex)

            Dim SubLogger As Logger = TaskLogger.AddLogger(CheckItem)

            Select Case DocType

                Case = "asm"
                    CheckAssemblySketches(CheckItem, SEDoc, SubLogger)
                    CheckOccurrences(CheckItem, SEDoc, SubLogger)

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
                    MsgBox($"{Me.Name} DocType '{DocType}' not recognized")
            End Select

            If (DocType = "par") Or (DocType = "psm") Then

                CheckSketches(CheckItem, Sketches, SubLogger)

                Dim RefPlanesWithUnderconstrainedProfiles As New List(Of SolidEdgePart.RefPlane)

                If CheckItem = "Underconstrained relationships" Then
                    RefPlanesWithUnderconstrainedProfiles = GetRefPlanesWithUnderconstrainedProfiles(ProfileSets)
                End If

                CheckFeatures(CheckItem, Models, RefPlanesWithUnderconstrainedProfiles, SubLogger)

            End If
        Next

    End Sub


    Private Sub CheckAssemblySketches(
        CheckItem As String,
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        SubLogger As Logger)

        Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

        ' The collections ComponentLayouts and Layouts hold the same information, at least in SE2024 and SE2025.
        ' They can have a collection of:
        '     - Layout (Assembly Sketches)
        '     - ComponentLayout (Component Sketches, part of the Virtual Component workflow)
        ' ComponentLayouts do not have a Status property, so cannot be checked.

        Try
            Dim AssemblySketches As SolidEdgeAssembly.Layouts = tmpSEDoc.Layouts

            For Each AssemblySketch As Object In AssemblySketches

                Dim AssemblyLayout As SolidEdgeAssembly.Layout = TryCast(AssemblySketch, SolidEdgeAssembly.Layout)

                If AssemblyLayout Is Nothing Then
                    Dim AssemblyComponentLayout As SolidEdgeAssembly.ComponentLayout = TryCast(AssemblySketch, SolidEdgeAssembly.ComponentLayout)
                    If AssemblyComponentLayout IsNot Nothing Then
                        SubLogger.AddMessage($"Could not process sketch '{AssemblyComponentLayout.Name}'")
                    End If
                    Continue For
                End If

                Try
                    Select Case CheckItem
                        Case "Failed relationships"
                            Dim Status = AssemblyLayout.Status
                            Dim tf As Boolean
                            tf = (Status = SolidEdgePart.FeatureStatusConstants.igFeatureFailed)
                            tf = tf Or (Status = SolidEdgePart.FeatureStatusConstants.igFeatureWarned)
                            If tf Then
                                SubLogger.AddMessage(AssemblyLayout.Name)
                            End If

                        Case "Underconstrained relationships"
                            Dim Profile = CType(AssemblyLayout.Profile, SolidEdgePart.Profile)
                            Dim ProfileSet As SolidEdgePart.ProfileSet = CType(Profile.Parent, SolidEdgePart.ProfileSet)
                            Dim tf As Boolean
                            tf = ProfileSet.IsUnderDefined  ' Shouldn't have to do this, but accessing IsUnderDefined sets it to False.
                            If tf Then
                                SubLogger.AddMessage(AssemblyLayout.Name)
                            End If

                        Case "Suppressed relationships"
                            Dim Status = AssemblyLayout.Status
                            Dim tf As Boolean
                            tf = (Status = SolidEdgePart.FeatureStatusConstants.igFeatureSuppressed)
                            tf = tf Or (Status = SolidEdgePart.FeatureStatusConstants.igFeatureRolledBack)
                            If tf Then
                                SubLogger.AddMessage(AssemblyLayout.Name)
                            End If

                    End Select

                Catch ex As Exception
                    Try
                        SubLogger.AddMessage($"Could not process sketch '{AssemblyLayout.Name}'.  Exception {ex.Message}")
                    Catch ex2 As Exception
                        SubLogger.AddMessage($"Could not process assembly sketch.  Exception {ex2.Message}")
                    End Try

                End Try

            Next
        Catch ex As Exception
            SubLogger.AddMessage($"Could not process assembly sketches.  Exception {ex.Message}")
        End Try

    End Sub

    Private Sub CheckOccurrences(
        CheckItem As String,
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        SubLogger As Logger)

        Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

        Select Case CheckItem

            Case "Failed relationships", "Underconstrained relationships"

                Dim Occurrences As SolidEdgeAssembly.Occurrences = tmpSEDoc.Occurrences

                For Each Occurrence As SolidEdgeAssembly.Occurrence In Occurrences
                    If Not (Occurrence.Adjustable Or Occurrence.IsAdjustablePart) Then

                        Select Case CheckItem
                            Case "Failed relationships"
                                Dim tf As Boolean
                                tf = Occurrence.Status = SolidEdgeAssembly.OccurrenceStatusConstants.seOccurrenceStatusOverDefined
                                tf = tf Or (Occurrence.Status = SolidEdgeAssembly.OccurrenceStatusConstants.seOccurrenceStatusNotConsistent)
                                If tf Then
                                    SubLogger.AddMessage(Occurrence.Name)
                                End If

                            Case "Underconstrained relationships"
                                If Occurrence.Status = SolidEdgeAssembly.OccurrenceStatusConstants.seOccurrenceStatusUnderDefined Then
                                    SubLogger.AddMessage(Occurrence.Name)
                                End If

                        End Select

                    End If
                Next

            Case "Suppressed relationships"

                'https://community.sw.siemens.com/s/question/0D5Vb00000TenJTKAZ/unsuppress-occurrences-from-api
                Dim Count As Integer
                Dim ComponentType = SolidEdgeAssembly.AssemblyComponentTypeConstants.seAssemblyComponentTypeAll
                Dim Components As System.Array = System.Array.CreateInstance(GetType(SolidEdgeAssembly.SuppressComponent), 0)

                tmpSEDoc.GetSuppressedComponents(ComponentType, Count, Components)

                If Components IsNot Nothing AndAlso Count > 0 Then
                    For Each SuppressedComponent As SolidEdgeAssembly.SuppressComponent In Components
                        SubLogger.AddMessage(SuppressedComponent.Name)
                    Next
                End If

        End Select

    End Sub

    Private Sub CheckFeatures(
        CheckItem As String,
        Models As SolidEdgePart.Models,
        RefPlanesWithUnderconstrainedProfiles As List(Of SolidEdgePart.RefPlane),
        SubLogger As Logger)

        If (Models IsNot Nothing) Then

            If Models.Count > 300 Then
                SubLogger.AddMessage($"{Models.Count.ToString} models exceeds maximum to process")
            End If

            If (Models.Count > 0) And (Models.Count <= 300) Then
                For Each Model As SolidEdgePart.Model In Models
                    Dim Features As SolidEdgePart.Features = Model.Features
                    For Each Feature As Object In Features
                        If IsOrdered(Feature) Then

                            Dim FeatureTypeConstant As SolidEdgePart.FeatureTypeConstants = GetFeatureType(Feature)
                            If FeatureTypeConstant = Nothing Then
                                Continue For
                            End If

                            Dim Name = GetName(Feature)

                            Select Case CheckItem
                                Case "Failed relationships"
                                    ' Status is 0 if the feature doesn't have one
                                    Dim Status As SolidEdgePart.FeatureStatusConstants = GetStatus(Feature)
                                    If Not Status = 0 Then
                                        Dim tf As Boolean
                                        tf = (Status = SolidEdgePart.FeatureStatusConstants.igFeatureFailed)
                                        tf = tf Or (Status = SolidEdgePart.FeatureStatusConstants.igFeatureWarned)
                                        If tf Then
                                            SubLogger.AddMessage(Name)
                                        End If
                                    End If

                                Case "Suppressed relationships"
                                    ' Status is 0 if the feature doesn't have one
                                    Dim Status As SolidEdgePart.FeatureStatusConstants = GetStatus(Feature)
                                    If Not Status = 0 Then
                                        Dim tf As Boolean
                                        tf = (Status = SolidEdgePart.FeatureStatusConstants.igFeatureSuppressed)
                                        tf = tf Or (Status = SolidEdgePart.FeatureStatusConstants.igFeatureRolledBack)
                                        If tf Then
                                            SubLogger.AddMessage(Name)
                                        End If
                                    End If

                                Case "Underconstrained relationships"
                                    Dim RefPlane As SolidEdgePart.RefPlane = Nothing

                                    If FeatureTypeConstant = SolidEdgePart.FeatureTypeConstants.igUserDefinedPatternFeatureObject Then
                                        RefPlane = GetPatternPlane(Feature)
                                    Else
                                        ' Some features do not have a profile
                                        Try
                                            Dim Profile As SolidEdgePart.Profile = GetProfile(Feature)
                                            If Profile Is Nothing Then
                                                Continue For
                                            End If
                                            RefPlane = CType(Profile.Plane, SolidEdgePart.RefPlane)

                                        Catch ex As Exception
                                            Continue For
                                        End Try
                                    End If
                                    If RefPlanesWithUnderconstrainedProfiles.Contains(RefPlane) Then
                                        SubLogger.AddMessage(Name)
                                    End If

                            End Select

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

        If Sketches IsNot Nothing Then
            For Each Sketch As SolidEdgePart.Sketch In Sketches

                Select Case CheckItem
                    Case "Failed relationships"
                        Dim StatusEx As SolidEdgePart.FeatureStatusConstants = Sketch.GetStatusEx
                        tf = (StatusEx = SolidEdgePart.FeatureStatusConstants.igFeatureFailed)
                        tf = tf Or (StatusEx = SolidEdgePart.FeatureStatusConstants.igFeatureWarned)
                        If tf Then
                            SubLogger.AddMessage(Sketch.Name)
                        End If
                    Case "Suppressed relationships"
                        Dim StatusEx As SolidEdgePart.FeatureStatusConstants = Sketch.GetStatusEx
                        tf = (StatusEx = SolidEdgePart.FeatureStatusConstants.igFeatureSuppressed)
                        tf = tf Or (StatusEx = SolidEdgePart.FeatureStatusConstants.igFeatureRolledBack)
                        If tf Then
                            SubLogger.AddMessage(Sketch.Name)
                        End If
                    Case "Underconstrained relationships"
                        If Sketch.IsUnderDefined Then
                            SubLogger.AddMessage(Sketch.Name)
                        End If
                End Select

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
                    Try
                        RefPlane = CType(Profile.Plane, SolidEdgePart.RefPlane)
                        If Not RefPlanesWithUnderconstrainedProfiles.Contains(RefPlane) Then
                            RefPlanesWithUnderconstrainedProfiles.Add(RefPlane)
                        End If
                    Catch ex As Exception
                    End Try
                Next
            End If
        Next

        Return RefPlanesWithUnderconstrainedProfiles

    End Function


    Private Function GetName(Feature As Object) As String
        Return HCComObject.GetPropertyValue(Of String)(Feature, "Name")
    End Function

    Private Function IsOrdered(Feature As Object) As Boolean
        ' Some features do not have a ModelingModeType
        Try
            Dim ModelingModeType = HCComObject.GetPropertyValue(Of SolidEdgePart.ModelingModeConstants)(Feature, "ModelingModeType")
            Return ModelingModeType = SolidEdgePart.ModelingModeConstants.seModelingModeOrdered
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetFeatureType(Feature As Object) As SolidEdgePart.FeatureTypeConstants
        Dim FeatureType As SolidEdgePart.FeatureTypeConstants = Nothing
        Try
            FeatureType = HCComObject.GetPropertyValue(Of SolidEdgePart.FeatureTypeConstants)(Feature, "Type")
        Catch ex As Exception
        End Try
        Return FeatureType
    End Function

    Private Function GetStatus(Feature As Object) As SolidEdgePart.FeatureStatusConstants
        Dim Status As SolidEdgePart.FeatureStatusConstants = Nothing
        Try
            Status = HCComObject.GetPropertyValue(Of SolidEdgePart.FeatureStatusConstants)(Feature, "Status")
        Catch ex As Exception
        End Try
        Return Status
    End Function

    Private Function GetProfile(Feature As Object) As SolidEdgePart.Profile
        Dim Profile As SolidEdgePart.Profile = Nothing

        'Some features do not have a profile
        Try
            Profile = HCComObject.GetPropertyValue(Of SolidEdgePart.Profile)(Feature, "Profile")
        Catch ex As Exception
        End Try
        Return Profile
    End Function

    Private Function GetPatternPlane(Feature As Object) As SolidEdgePart.RefPlane
        Dim PatternPlane As SolidEdgePart.RefPlane = Nothing
        Try
            PatternPlane = HCComObject.GetPropertyValue(Of SolidEdgePart.RefPlane)(Feature, "PatternPlane")
        Catch ex As Exception
        End Try
        Return PatternPlane
    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

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
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If

            If Not (Me.CheckFailed Or Me.CheckUnderconstrained Or Me.CheckSuppressed) Then
                ErrorLogger.AddMessage("Select at least one type of check to perform")
            End If
        End If

    End Sub


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
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Checks if the file has any failed, underconstrained, or suppressed relationships."

        HelpString += vbCrLf + vbCrLf + "![CheckRelationships](My%20Project/media/task_check_relationships.png)"


        Return HelpString
    End Function

End Class
