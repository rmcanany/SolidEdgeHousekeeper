Option Strict On

Imports SolidEdgeCommunity
Imports SolidEdgePart

Public Class SheetmetalTasks
    Inherits IsolatedTaskProxy

    Public Function FailedOrWarnedFeatures(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'ErrorMessage = InvokeSTAThread(
        '                       Of SolidEdgePart.SheetMetalDocument,
        '                       Dictionary(Of String, String),
        '                       SolidEdgeFramework.Application,
        '                       Dictionary(Of Integer, List(Of String)))(
        '                           AddressOf FailedOrWarnedFeaturesInternal,
        '                           CType(SEDoc, SolidEdgePart.SheetMetalDocument),
        '                           Configuration,
        '                           SEApp)

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeFramework.SolidEdgeDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf PartTasks.FailedOrWarnedFeaturesInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    'Private Function FailedOrWarnedFeaturesInternal(
    '    ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
    '    ByVal Configuration As Dictionary(Of String, String),
    '    ByVal SEApp As SolidEdgeFramework.Application
    '    ) As Dictionary(Of Integer, List(Of String))

    '    Dim ErrorMessageList As New List(Of String)
    '    Dim ExitStatus As Integer = 0
    '    Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

    '    Dim Models As SolidEdgePart.Models
    '    Dim Model As SolidEdgePart.Model
    '    Dim Features As SolidEdgePart.Features
    '    Dim FeatureName As String
    '    Dim Status As SolidEdgePart.FeatureStatusConstants

    '    Dim TF As Boolean
    '    Dim FeatureSystemNames As New List(Of String)
    '    Dim FeatureSystemName As String

    '    Models = SEDoc.Models

    '    If (Models.Count > 0) And (Models.Count < 300) Then
    '        For Each Model In Models
    '            Features = Model.Features
    '            For Each Feature In Features
    '                FeatureSystemName = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(Of String)(Feature, "Name")

    '                If Not FeatureSystemNames.Contains(FeatureSystemName) Then
    '                    FeatureSystemNames.Add(FeatureSystemName)

    '                    'Some Sync part features don't have a Status field.
    '                    Try
    '                        FeatureName = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(Of String)(Feature, "Name")

    '                        Status = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(
    '                        Of SolidEdgePart.FeatureStatusConstants)(Feature, "Status", CType(0, SolidEdgePart.FeatureStatusConstants))

    '                        TF = Status = SolidEdgePart.FeatureStatusConstants.igFeatureFailed
    '                        TF = TF Or Status = SolidEdgePart.FeatureStatusConstants.igFeatureWarned
    '                        If TF Then
    '                            ExitStatus = 1
    '                            ErrorMessageList.Add(FeatureName)
    '                        End If

    '                    Catch ex As Exception

    '                    End Try
    '                End If

    '            Next
    '        Next
    '    ElseIf Models.Count >= 300 Then
    '        ExitStatus = 1
    '        ErrorMessageList.Add(String.Format("{0} models exceeds maximum to process", Models.Count.ToString))
    '    End If

    '    ErrorMessage(ExitStatus) = ErrorMessageList
    '    Return ErrorMessage
    'End Function



    Public Function SuppressedOrRolledBackFeatures(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf SuppressedOrRolledBackFeaturesInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function SuppressedOrRolledBackFeaturesInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Models As SolidEdgePart.Models
        Dim Model As SolidEdgePart.Model
        Dim Features As SolidEdgePart.Features
        Dim FeatureName As String
        Dim Status As SolidEdgePart.FeatureStatusConstants

        Dim TF As Boolean
        Dim FeatureSystemNames As New List(Of String)
        Dim FeatureSystemName As String

        Models = SEDoc.Models

        If (Models.Count > 0) And (Models.Count < 300) Then
            For Each Model In Models
                Features = Model.Features
                For Each Feature In Features
                    FeatureSystemName = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(Of String)(Feature, "Name")

                    If Not FeatureSystemNames.Contains(FeatureSystemName) Then
                        FeatureSystemNames.Add(FeatureSystemName)

                        'Some Sync part features don't have a Status field.
                        Try
                            FeatureName = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(Of String)(Feature, "Name")

                            Status = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetPropertyValue(
                            Of SolidEdgePart.FeatureStatusConstants)(Feature, "Status", CType(0, SolidEdgePart.FeatureStatusConstants))

                            TF = Status = SolidEdgePart.FeatureStatusConstants.igFeatureSuppressed
                            TF = TF Or Status = SolidEdgePart.FeatureStatusConstants.igFeatureRolledBack
                            If TF Then
                                ExitStatus = 1
                                ErrorMessageList.Add(FeatureName)
                            End If

                        Catch ex As Exception

                        End Try
                    End If
                Next
            Next
        ElseIf Models.Count >= 300 Then
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("{0} models exceeds maximum to process", Models.Count.ToString))
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function UnderconstrainedProfiles(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UnderconstrainedProfilesInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UnderconstrainedProfilesInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim ProfileSets As SolidEdgePart.ProfileSets = SEDoc.ProfileSets
        Dim ProfileSet As SolidEdgePart.ProfileSet
        Dim Profiles As SolidEdgePart.Profiles
        Dim FeatureProfile As SolidEdgePart.Profile
        Dim Profile As SolidEdgePart.Profile

        Dim Sketches As SolidEdgePart.Sketchs = SEDoc.Sketches
        Dim Sketch As SolidEdgePart.Sketch


        Dim Models As SolidEdgePart.Models = SEDoc.Models
        Dim Model As SolidEdgePart.Model

        Dim Features As SolidEdgePart.Features
        Dim Feature As Object

        Dim FeatureDoctor As New FeatureDoctor()

        For Each Sketch In Sketches
            If Sketch.IsUnderDefined Then
                ExitStatus = 1
            End If
        Next

        If (ExitStatus = 0) And (Models.Count > 0) Then
            For Each Model In Models
                Features = Model.Features
                For Each Feature In Features
                    If FeatureDoctor.IsOrdered(Feature) Then
                        FeatureProfile = FeatureDoctor.GetProfile(Feature)
                        If Not FeatureProfile Is Nothing Then
                            ' Look through the profilesets to see if the feature profile is present.
                            For Each ProfileSet In ProfileSets
                                Profiles = ProfileSet.Profiles
                                For Each Profile In Profiles
                                    If Profile Is FeatureProfile Then
                                        If ProfileSet.IsUnderDefined Then
                                            ExitStatus = 1
                                        End If
                                    End If
                                Next
                            Next
                        End If
                    End If
                Next
            Next
        End If

        ' Not applicable in sync models.
        'If SEDoc.ModelingMode.ToString = "seModelingModeOrdered" Then
        '    For Each ProfileSet In ProfileSets
        '        If ProfileSet.IsUnderDefined Then
        '            ExitStatus = 1
        '        End If
        '    Next
        'End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function InsertPartCopiesOutOfDate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf InsertPartCopiesOutOfDateInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function InsertPartCopiesOutOfDateInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Models As SolidEdgePart.Models
        Dim Model As SolidEdgePart.Model
        Dim CopiedParts As SolidEdgePart.CopiedParts
        Dim CopiedPart As SolidEdgePart.CopiedPart

        Models = SEDoc.Models

        Dim TF As Boolean

        If (Models.Count > 0) And (Models.Count < 300) Then
            For Each Model In Models
                CopiedParts = Model.CopiedParts
                If CopiedParts.Count > 0 Then
                    For Each CopiedPart In CopiedParts
                        TF = FileIO.FileSystem.FileExists(CopiedPart.FileName)
                        TF = TF Or (CopiedPart.FileName = "")  ' Implies no link to outside file
                        TF = TF And CopiedPart.IsUpToDate
                        If Not TF Then
                            ExitStatus = 1
                            ErrorMessageList.Add(CopiedPart.Name)
                        End If
                    Next
                End If
            Next
        ElseIf Models.Count >= 300 Then
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("{0} models exceeds maximum to process", Models.Count.ToString))
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function BrokenLinks(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf InsertPartCopiesOutOfDateInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    'Private Function BrokenLinksInternal(
    '    ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
    '    ByVal Configuration As Dictionary(Of String, String),
    '    ByVal SEApp As SolidEdgeFramework.Application
    '    ) As Dictionary(Of Integer, List(Of String))

    '    Dim ErrorMessageList As New List(Of String)
    '    Dim ExitStatus As Integer = 0
    '    Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

    '    Dim Models As SolidEdgePart.Models
    '    Dim Model As SolidEdgePart.Model
    '    Dim CopiedParts As SolidEdgePart.CopiedParts
    '    Dim CopiedPart As SolidEdgePart.CopiedPart

    '    Models = SEDoc.Models

    '    Dim TF As Boolean

    '    If (Models.Count > 0) And (Models.Count < 300) Then
    '        For Each Model In Models
    '            CopiedParts = Model.CopiedParts
    '            If CopiedParts.Count > 0 Then
    '                For Each CopiedPart In CopiedParts
    '                    TF = FileIO.FileSystem.FileExists(CopiedPart.FileName)
    '                    TF = TF Or (CopiedPart.FileName = "")  ' Implies no link to outside file
    '                    'TF = TF And CopiedPart.IsUpToDate
    '                    If Not TF Then
    '                        ExitStatus = 1
    '                        ErrorMessageList.Add(CopiedPart.Name)
    '                    End If
    '                Next
    '            End If
    '        Next
    '    ElseIf Models.Count >= 300 Then
    '        ExitStatus = 1
    '        ErrorMessageList.Add(String.Format("{0} models exceeds maximum to process", Models.Count.ToString))
    '    End If

    '    ErrorMessage(ExitStatus) = ErrorMessageList
    '    Return ErrorMessage
    'End Function



    Public Function FlatPatternMissingOrOutOfDate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf FlatPatternMissingOrOutOfDateInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function FlatPatternMissingOrOutOfDateInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim FlatpatternModels As SolidEdgePart.FlatPatternModels = SEDoc.FlatPatternModels

        If FlatpatternModels.Count > 0 Then
            If Not FlatpatternModels.Item(1).IsUpToDate Then
                ExitStatus = 1
            End If
        Else
            ExitStatus = 1
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function MaterialNotInMaterialTable(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf MaterialNotInMaterialTableInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function MaterialNotInMaterialTableInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim MaterialDoctorSheetmetal As New MaterialDoctorSheetmetal()

        ErrorMessage = MaterialDoctorSheetmetal.MaterialNotInMaterialTable(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function



    Public Function PartNumberDoesNotMatchFilename(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf PartNumberDoesNotMatchFilenameInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function PartNumberDoesNotMatchFilenameInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim PropertySets As SolidEdgeFramework.PropertySets = Nothing
        Dim Properties As SolidEdgeFramework.Properties = Nothing
        Dim Prop As SolidEdgeFramework.Property = Nothing

        Dim PartNumber As String = ""
        Dim PartNumberPropertyFound As Boolean = False
        Dim TF As Boolean
        Dim Filename As String

        'Get the bare file name without directory information
        Filename = System.IO.Path.GetFileName(SEDoc.FullName)

        Dim msg As String = ""

        PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)

        For Each Properties In PropertySets
            msg += Properties.Name + Chr(13)
            For Each Prop In Properties
                TF = (Configuration("ComboBoxPartNumberPropertySet").ToLower = "custom") And (Properties.Name.ToLower = "custom")
                If TF Then
                    If Prop.Name = Configuration("TextBoxPartNumberPropertyName") Then
                        'If Prop.Name = TextBoxPartNumberPropertyName.Text Then
                        PartNumber = CType(Prop.Value, String).Trim
                        PartNumberPropertyFound = True
                        Exit For
                    End If
                Else
                    If Prop.Name = Configuration("TextBoxPartNumberPropertyName") Then
                        PartNumber = CType(Prop.Value, String).Trim
                        PartNumberPropertyFound = True
                        Exit For
                    End If
                End If
            Next
            If PartNumberPropertyFound Then
                Exit For
            End If
        Next

        If PartNumberPropertyFound Then
            If PartNumber.Trim = "" Then
                ExitStatus = 1
                ErrorMessageList.Add("Part number not assigned")
            End If
            If Not Filename.ToLower.Contains(PartNumber.ToLower) Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Part number '{0}' not found in filename '{1}'", PartNumber, Filename))
            End If
        Else
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Property name: '{0}' not found in property set: '{1}'",
                                     Configuration("TextBoxPartNumberPropertyName"),
                                     Configuration("ComboBoxPartNumberPropertySet")))
            If Configuration("TextBoxPartNumberPropertyName") = "" Then
                ErrorMessageList.Add("Check the Configuration tab for valid entries")
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function




    Public Function UpdateInsertPartCopies(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UpdateInsertPartCopiesInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Public Function UpdateInsertPartCopiesInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim SupplementalExitStatus As Integer = 0
        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Models As SolidEdgePart.Models
        Dim Model As SolidEdgePart.Model
        Dim CopiedParts As SolidEdgePart.CopiedParts
        Dim CopiedPart As SolidEdgePart.CopiedPart

        Models = SEDoc.Models

        Dim TF As Boolean

        If (Models.Count > 0) And (Models.Count < 300) Then
            For Each Model In Models
                CopiedParts = Model.CopiedParts
                If CopiedParts.Count > 0 Then
                    For Each CopiedPart In CopiedParts
                        TF = FileIO.FileSystem.FileExists(CopiedPart.FileName)
                        TF = TF Or (CopiedPart.FileName = "")  ' Implies no link to outside file
                        If Not TF Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Insert part copy file not found: '{0}'", CopiedPart.FileName))
                        Else
                            If Configuration("CheckBoxPartCopiesRecursiveSearch") = "True" Then
                                ' Try a recursion
                                Dim Filetype As String = CommonTasks.GetDocTypeByExtension(CopiedPart.FileName)

                                If Filetype = ".par" Then
                                    Dim ParentDoc As SolidEdgePart.PartDocument = CType(SEApp.Documents.Open(CopiedPart.FileName), SolidEdgePart.PartDocument)
                                    Dim PT As New PartTasks

                                    SupplementalErrorMessage = PT.UpdateInsertPartCopiesInternal(ParentDoc, Configuration, SEApp)
                                    SupplementalExitStatus = SupplementalErrorMessage.Keys(0)
                                    If SupplementalExitStatus > 0 Then
                                        ExitStatus = SupplementalExitStatus
                                        For Each s As String In SupplementalErrorMessage(SupplementalExitStatus)
                                            ErrorMessageList.Add(s)
                                        Next
                                    End If
                                    ParentDoc.Close()
                                    SEApp.DoIdle()

                                ElseIf Filetype = ".psm" Then
                                    Dim ParentDoc As SolidEdgePart.SheetMetalDocument = CType(SEApp.Documents.Open(CopiedPart.FileName), SolidEdgePart.SheetMetalDocument)
                                    Dim SMT As New SheetmetalTasks

                                    SupplementalErrorMessage = UpdateInsertPartCopiesInternal(ParentDoc, Configuration, SEApp)
                                    SupplementalExitStatus = SupplementalErrorMessage.Keys(0)
                                    If SupplementalExitStatus > 0 Then
                                        ExitStatus = SupplementalExitStatus
                                        For Each s As String In SupplementalErrorMessage(SupplementalExitStatus)
                                            ErrorMessageList.Add(s)
                                        Next
                                    End If
                                    ParentDoc.Close()
                                    SEApp.DoIdle()

                                End If

                            End If

                            If Not CopiedPart.IsUpToDate Then
                                CopiedPart.Update()
                            End If

                            If SEDoc.ReadOnly Then
                                ExitStatus = 1
                                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
                            Else
                                SEDoc.Save()
                                SEApp.DoIdle()
                                'ExitStatus = 1
                                'ErrorMessageList.Add(String.Format("Updated insert part copy: {0}", CopiedPart.Name))
                            End If
                        End If
                    Next
                End If
            Next
        ElseIf Models.Count >= 300 Then
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("{0} models exceeds maximum to process", Models.Count.ToString))
        End If

        'Update PMI
        Try
            'PMI = CType(SEDoc.PMI, SolidEdgeFrameworkSupport.PMI)
            'PMI.Show = False
            'PMI.ShowDimensions = False
            'PMI.ShowAnnotations = False
            SEApp.StartCommand(CType(10180, SolidEdgeFramework.SolidEdgeCommandConstants))  ' Update PMI
            SEApp.DoIdle()
            SEDoc.Save()
            SEApp.DoIdle()

        Catch ex As Exception
            ExitStatus = 1
            ErrorMessageList.Add("Unable to update PMI")
        End Try

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function UpdateMaterialFromMaterialTable(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UpdateMaterialFromMaterialTableInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UpdateMaterialFromMaterialTableInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim MaterialDoctorSheetmetal As New MaterialDoctorSheetmetal()

        ErrorMessage = MaterialDoctorSheetmetal.UpdateMaterialFromMaterialTable(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function



    Public Function UpdateFaceAndViewStylesFromTemplate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UpdateFaceAndViewStylesFromTemplateInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UpdateFaceAndViewStylesFromTemplateInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TempErrorMessageList As New List(Of String)

        Dim SETemplateDoc As SolidEdgePart.SheetMetalDocument
        Dim TemplateFilename As String = Configuration("TextBoxTemplateSheetmetal")

        ' Import face styles from template
        SEDoc.ImportStyles(TemplateFilename, True)
        SEApp.DoIdle()

        SETemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgePart.SheetMetalDocument)
        SEApp.DoIdle()

        ' Update Color Manager base styles from template
        TempErrorMessageList = UpdateBaseStyles(SEDoc, SETemplateDoc)
        If TempErrorMessageList.Count > 0 Then
            ExitStatus = 1
            For Each s As String In TempErrorMessageList
                ErrorMessageList.Add(s)
            Next
        End If

        ' Update view styles from template
        TempErrorMessageList = UpdateViewStyles(SEApp, SEDoc, SETemplateDoc)
        If TempErrorMessageList.Count > 0 Then
            ExitStatus = 1
            For Each s As String In TempErrorMessageList
                ErrorMessageList.Add(s)
            Next
        End If

        SETemplateDoc.Close()
        SEApp.DoIdle()

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Private Function UpdateViewStyles(
        ByRef SEApp As SolidEdgeFramework.Application,
        ByRef SEDoc As SolidEdgePart.SheetMetalDocument,
        ByRef SETemplateDoc As SolidEdgePart.SheetMetalDocument
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        Dim TempErrorMessageList As New List(Of String)

        Dim TemplateViewStyles As SolidEdgeFramework.ViewStyles
        'Dim TemplateViewStyle As SolidEdgeFramework.ViewStyle
        Dim DocViewStyles As SolidEdgeFramework.ViewStyles
        Dim DocViewStyle As SolidEdgeFramework.ViewStyle
        Dim TemplateActiveViewStyle As SolidEdgeFramework.ViewStyle = Nothing
        Dim DocActiveViewStyle As SolidEdgeFramework.ViewStyle

        Dim Windows As SolidEdgeFramework.Windows
        Dim Window As SolidEdgeFramework.Window
        Dim View As SolidEdgeFramework.View

        Dim tf As Boolean

        SETemplateDoc.Activate()

        Windows = SETemplateDoc.Windows

        For Each Window In Windows
            View = Window.View
            TemplateActiveViewStyle = CType(View.ViewStyle, SolidEdgeFramework.ViewStyle)
        Next

        TemplateViewStyles = CType(SETemplateDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
        DocViewStyles = CType(SEDoc.ViewStyles, SolidEdgeFramework.ViewStyles)

        SEDoc.Activate()

        DocActiveViewStyle = DocViewStyles.AddFromFile(SETemplateDoc.FullName, TemplateActiveViewStyle.StyleName)

        SEApp.DoIdle()

        'Update skybox
        DocActiveViewStyle.SkyboxType = SolidEdgeFramework.SeSkyboxType.seSkyboxTypeSkybox

        Dim s As String
        Dim i As Integer

        For i = 0 To 5
            s = TemplateActiveViewStyle.GetSkyboxSideFilename(i)
            DocActiveViewStyle.SetSkyboxSideFilename(i, s)
        Next

        SEApp.DoIdle()

        Windows = SEDoc.Windows

        For Each Window In Windows
            View = Window.View
            View.Style = DocActiveViewStyle.StyleName
        Next

        DocViewStyles = CType(SEDoc.ViewStyles, SolidEdgeFramework.ViewStyles)

        SEApp.DoIdle()

        For Each DocViewStyle In DocViewStyles
            tf = Not DocViewStyle.StyleName.ToLower() = "default"
            tf = tf And Not DocViewStyle.StyleName = DocActiveViewStyle.StyleName
            If tf Then
                Try
                    DocViewStyle.Delete()
                Catch ex As Exception
                End Try
            End If
        Next

        Return ErrorMessageList
    End Function

    Private Function UpdateBaseStyles(
        ByRef SEDoc As SolidEdgePart.SheetMetalDocument,
        ByRef SETemplateDoc As SolidEdgePart.SheetMetalDocument
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        Dim FaceStyles As SolidEdgeFramework.FaceStyles
        Dim FaceStyle As SolidEdgeFramework.FaceStyle
        Dim tf As Boolean

        Dim TemplateConstructionBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim TemplateThreadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim TemplatePartBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim TemplateCurveBaseStyle As SolidEdgeFramework.FaceStyle = Nothing

        Dim ConstructionBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim ThreadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim PartBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim CurveBaseStyle As SolidEdgeFramework.FaceStyle = Nothing

        SETemplateDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seConstructionBaseStyle,
                                   TemplateConstructionBaseStyle)
        SETemplateDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seThreadedCylindersBaseStyle,
                                   TemplateThreadBaseStyle)
        SETemplateDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle,
                                   TemplatePartBaseStyle)
        SETemplateDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seCurveBaseStyle,
                                   TemplateCurveBaseStyle)
        'MsgBox(TemplateConstructionBaseStyle.StyleName)

        ' Update base styles in the document
        FaceStyles = CType(SEDoc.FaceStyles, SolidEdgeFramework.FaceStyles)

        ' Need the doc PartBaseStyle below.  If it is not Nothing, don't overwrite it.
        SEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle, PartBaseStyle)

        For Each FaceStyle In FaceStyles
            If TemplateConstructionBaseStyle IsNot Nothing Then
                If FaceStyle.StyleName = TemplateConstructionBaseStyle.StyleName Then
                    SEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seConstructionBaseStyle,
                                   FaceStyle)
                End If
            End If

            If TemplateThreadBaseStyle IsNot Nothing Then
                If FaceStyle.StyleName = TemplateThreadBaseStyle.StyleName Then
                    SEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seThreadedCylindersBaseStyle,
                                       FaceStyle)
                End If
            End If

            If TemplatePartBaseStyle IsNot Nothing Then
                If PartBaseStyle Is Nothing Then
                    If FaceStyle.StyleName = TemplatePartBaseStyle.StyleName Then
                        SEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle,
                                       FaceStyle)
                    End If
                End If
            End If

            If TemplateCurveBaseStyle IsNot Nothing Then
                If FaceStyle.StyleName = TemplateCurveBaseStyle.StyleName Then
                    SEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seCurveBaseStyle,
                                       FaceStyle)
                End If
            End If
        Next

        SEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seConstructionBaseStyle,
                           ConstructionBaseStyle)
        SEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seThreadedCylindersBaseStyle,
                       ThreadBaseStyle)
        SEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle,
                       PartBaseStyle)
        SEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seCurveBaseStyle,
                       CurveBaseStyle)

        tf = ConstructionBaseStyle Is Nothing
        tf = tf Or (ThreadBaseStyle Is Nothing)
        tf = tf Or (PartBaseStyle Is Nothing)
        tf = tf Or (CurveBaseStyle Is Nothing)

        If tf Then
            ErrorMessageList.Add("Some Color Manager base styles undefined.")
        End If

        Return ErrorMessageList
    End Function




    Public Function UpdateDesignForCost(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UpdateDesignForCostInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UpdateDesignForCostInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        SEApp.StartCommand(CType(11805, SolidEdgeFramework.SolidEdgeCommandConstants))
        SEApp.DoIdle()

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function OpenSave(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf OpenSaveInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function OpenSaveInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'SEApp.StartCommand(CType(11805, SolidEdgeFramework.SolidEdgeCommandConstants))
        'SEApp.DoIdle()

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function HideConstructions(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf HideConstructionsInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function HideConstructionsInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim RefPlanes As SolidEdgePart.RefPlanes
        Dim RefPlane As SolidEdgePart.RefPlane
        Dim Models As SolidEdgePart.Models

        Dim Model As SolidEdgePart.Model
        Dim Etches As SolidEdgePart.Etches
        Dim Etch As SolidEdgePart.Etch

        Dim PMI As SolidEdgeFrameworkSupport.PMI

        Dim Sketches As SolidEdgePart.Sketchs
        Dim Sketch As SolidEdgePart.Sketch
        Dim Profiles As SolidEdgePart.Profiles
        Dim Profile As SolidEdgePart.Profile

        Try
            Sketches = SEDoc.Sketches
            For Each Sketch In Sketches
                Profiles = Sketch.Profiles
                For Each Profile In Profiles
                    Profile.Visible = False
                Next
            Next
        Catch ex As Exception
        End Try

        Try
            PMI = CType(SEDoc.PMI, SolidEdgeFrameworkSupport.PMI)
            PMI.Show = False
            PMI.ShowDimensions = False
            PMI.ShowAnnotations = False
        Catch ex As Exception
        End Try


        Models = SEDoc.Models

        If Models.Count > 0 Then
            RefPlanes = SEDoc.RefPlanes
            For Each RefPlane In RefPlanes
                RefPlane.Visible = False
            Next
        Else
            RefPlanes = SEDoc.RefPlanes
            For Each RefPlane In RefPlanes
                RefPlane.Visible = True
            Next
        End If

        'Some imported files crash on this command
        Try
            SEDoc.Constructions.Visible = False
        Catch ex As Exception
        End Try

        SEDoc.CoordinateSystems.Visible = False

        If Models.Count > 0 Then
            For Each Model In Models
                Try
                    Etches = Model.Etches
                    If Not Etches Is Nothing Then
                        For Each Etch In Etches
                            Etch.Visible = True
                        Next
                    End If
                Catch ex As Exception
                End Try
            Next
        End If


        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function FitPictorialView(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf FitPictorialViewInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function FitPictorialViewInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim RefPlanes As SolidEdgePart.RefPlanes
        Dim RefPlane As SolidEdgePart.RefPlane
        Dim Models As SolidEdgePart.Models

        Models = SEDoc.Models

        If Models.Count = 0 Then
            RefPlanes = SEDoc.RefPlanes
            For Each RefPlane In RefPlanes
                RefPlane.Visible = True
            Next
        End If

        If Configuration("RadioButtonPictorialViewIsometric").ToLower = "true" Then
            SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.PartViewISOView, SolidEdgeFramework.SolidEdgeCommandConstants))
        End If
        If Configuration("RadioButtonPictorialViewDimetric").ToLower = "true" Then
            SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.PartViewDimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))
        End If
        If Configuration("RadioButtonPictorialViewTrimetric").ToLower = "true" Then
            SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.SheetMetalViewTrimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))
        End If

        SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.PartViewFit, SolidEdgeFramework.SolidEdgeCommandConstants))

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function SaveAs(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf SaveAsInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function SaveAsInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim SupplementalExitStatus As Integer = 0
        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim DraftTasks As New DraftTasks

        'Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim NewFilename As String = ""
        Dim NewExtension As String = ""
        Dim NewFileFormat As String = ""
        Dim PartBaseFilename As String

        Dim BaseDir As String
        Dim SubDir As String
        Dim Formula As String
        Dim Proceed As Boolean = True

        Dim DraftFilename As String
        Dim SEDraftDoc As SolidEdgeDraft.DraftDocument = Nothing
        Dim Models As SolidEdgePart.Models = Nothing
        Dim Model As SolidEdgePart.Model = Nothing

        Dim FlatPatternModels As SolidEdgePart.FlatPatternModels
        Dim FlatPatternModel As SolidEdgePart.FlatPatternModel


        Dim ExitMessage As String = ""

        Dim ImageExtensions As New List(Of String)

        ImageExtensions.Add(".bmp")
        ImageExtensions.Add(".jpg")
        ImageExtensions.Add(".png")
        ImageExtensions.Add(".tif")

        ' ComboBoxSaveAsSheetmetalFiletype
        ' Format: Parasolid (*.xt), IGES (*.igs)
        NewExtension = Configuration("ComboBoxSaveAsSheetmetalFileType")
        NewExtension = NewExtension.Split("*"c)(1)  ' "Parasolid (*.xt)" -> ".xt)"
        NewExtension = NewExtension.Split(")"c)(0)  ' ".xt)" -> ".xt"

        NewFileFormat = Configuration("ComboBoxSaveAsSheetmetalFileType")
        NewFileFormat = NewFileFormat.Split("("c)(0)  ' "Parasolid (*.xt)" -> "Parasolid "

        PartBaseFilename = System.IO.Path.GetFileName(SEDoc.FullName)

        ' CheckBoxSaveAsSheetmetalOutputDirectory
        If Configuration("CheckBoxSaveAsSheetmetalOutputDirectory") = "False" Then
            BaseDir = Configuration("TextBoxSaveAsSheetmetalOutputDirectory")

            If Configuration("CheckBoxSaveAsFormulaSheetmetal").ToLower = "true" Then
                Formula = Configuration("TextBoxSaveAsFormulaSheetmetal")

                SupplementalErrorMessage = ParseSubdirectoryFormula(SEDoc, Configuration, Formula)

                ' SubDir = ParseSubdirectoryFormula(SEDoc, Configuration, Formula)
                SupplementalExitStatus = SupplementalErrorMessage.Keys(0)
                If SupplementalExitStatus = 0 Then
                    SubDir = SupplementalErrorMessage(0)(0)

                    BaseDir = String.Format("{0}\{1}", BaseDir, SubDir)
                    If Not FileIO.FileSystem.DirectoryExists(BaseDir) Then
                        Try
                            FileIO.FileSystem.CreateDirectory(BaseDir)
                        Catch ex As Exception
                            Proceed = False
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Could not create '{0}'", BaseDir))
                        End Try
                    End If
                Else
                    ExitStatus = 1
                    Proceed = False
                    For Each msg In SupplementalErrorMessage(SupplementalExitStatus)
                        ErrorMessageList.Add(msg)
                    Next
                    ErrorMessageList.Add(String.Format("Could not create subdirectory from formula '{0}'", Formula))
                End If

            End If

            If Proceed Then
                NewFilename = BaseDir + "\" + System.IO.Path.ChangeExtension(PartBaseFilename, NewExtension)
            End If

        Else
            NewFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, NewExtension)
        End If

        If Proceed Then
            'Capturing a fault to update ExitStatus
            Try
                If Not ImageExtensions.Contains(NewExtension) Then  ' Saving a model or companion draft file.

                    If NewExtension = ".dxf" Then
                        SupplementalErrorMessage = FlatPatternMissingOrOutOfDate(CType(SEDoc, SolidEdgeFramework.SolidEdgeDocument), Configuration, SEApp)
                        If SupplementalErrorMessage.Keys(0) <> 0 Then
                            ExitStatus = SupplementalErrorMessage.Keys(0)
                            ErrorMessageList.Add("Flat pattern missing or out of date")
                        Else
                            Models = SEDoc.Models
                            Try
                                Models.SaveAsFlatDXFEx(NewFilename, Nothing, Nothing, Nothing, True)
                                SEApp.DoIdle()
                            Catch ex As Exception
                                ExitStatus = 1
                                ErrorMessageList.Add(String.Format("Error saving '{0}'", CommonTasks.TruncateFullPath(NewFilename, Configuration)))
                            End Try
                        End If

                    ElseIf (NewExtension = ".stp") And (NewFileFormat.Contains("Step Flat")) Then
                        SupplementalErrorMessage = FlatPatternMissingOrOutOfDate(CType(SEDoc, SolidEdgeFramework.SolidEdgeDocument), Configuration, SEApp)
                        If SupplementalErrorMessage.Keys(0) <> 0 Then
                            ExitStatus = SupplementalErrorMessage.Keys(0)
                            ErrorMessageList.Add("Flat pattern missing or out of date")
                        Else
                            Try
                                FlatPatternModels = SEDoc.FlatPatternModels
                                FlatPatternModel = FlatPatternModels.Item(1)
                                FlatPatternModel.MakeActive()
                                SEApp.DoIdle()
                                SEDoc.SaveAs(NewFilename)
                                SEApp.DoIdle()
                            Catch ex2 As Exception
                                MsgBox(ex2)
                            End Try

                            Models = SEDoc.Models
                            Model = Models.Item(1)
                            Model.MakeActive()
                            SEApp.DoIdle()
                        End If

                    ElseIf NewExtension = ".pdf" Then
                        DraftFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, ".dft")
                        If Not FileIO.FileSystem.FileExists(DraftFilename) Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Draft document not found '{0}'", CommonTasks.TruncateFullPath(DraftFilename, Configuration)))
                        Else
                            SEDraftDoc = CType(SEApp.Documents.Open(DraftFilename), SolidEdgeDraft.DraftDocument)
                            SEApp.DoIdle()

                            SupplementalErrorMessage = DraftTasks.DrawingViewsOutOfDate(CType(SEDraftDoc, SolidEdgeFramework.SolidEdgeDocument), Configuration, SEApp)
                            If SupplementalErrorMessage.Keys(0) <> 0 Then
                                ExitStatus = SupplementalErrorMessage.Keys(0)
                                ErrorMessageList.Add("Drawing view out of date")
                            Else
                                Try
                                    SEDraftDoc.SaveAs(NewFilename)
                                    SEApp.DoIdle()
                                    SEDraftDoc.Close(False)
                                    SEApp.DoIdle()
                                Catch ex As Exception
                                    ExitStatus = 1
                                    ErrorMessageList.Add(String.Format("Error saving '{0}'", CommonTasks.TruncateFullPath(NewFilename, Configuration)))
                                End Try

                            End If
                        End If


                    Else
                        Try
                            If Not Configuration("ComboBoxSaveAsSheetmetalFileType").ToLower.Contains("copy") Then
                                SEDoc.SaveAs(NewFilename)
                                SEApp.DoIdle()
                            Else
                                If Configuration("CheckBoxSaveAsSheetmetalOutputDirectory").ToLower = "false" Then
                                    SEDoc.SaveCopyAs(NewFilename)
                                    SEApp.DoIdle()
                                Else
                                    ExitStatus = 1
                                    ErrorMessageList.Add("Can not SaveCopyAs to the original directory")
                                    Proceed = False
                                End If
                            End If

                            'SEDoc.SaveAs(NewFilename)
                            'SEApp.DoIdle()
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Error saving '{0}'", CommonTasks.TruncateFullPath(NewFilename, Configuration)))
                        End Try
                    End If

                Else  ' Saving an image file.
                    Dim Window As SolidEdgeFramework.Window
                    Dim View As SolidEdgeFramework.View

                    Window = CType(SEApp.ActiveWindow, SolidEdgeFramework.Window)
                    View = Window.View

                    If Not NewExtension = ".png" Then
                        View.SaveAsImage(NewFilename)
                    Else
                        ExitMessage = CommonTasks.SaveAsPNG(View, NewFilename)
                        If Not ExitMessage = "" Then
                            ExitStatus = 1
                            ErrorMessageList.Add(ExitMessage)
                        End If
                    End If

                    If Configuration("CheckBoxSaveAsImageCrop").ToLower = "true" Then
                        ExitMessage = CommonTasks.CropImage(Configuration, CType(SEDoc, SolidEdgeFramework.SolidEdgeDocument), NewFilename, NewExtension, Window.Height, Window.Width)
                        If Not ExitMessage = "" Then
                            ExitStatus = 1
                            ErrorMessageList.Add(ExitMessage)
                        End If
                    End If
                End If
            Catch ex As Exception
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Error saving {0}", CommonTasks.TruncateFullPath(NewFilename, Configuration)))
            End Try


        End If

        Try
            If Not SEDraftDoc Is Nothing Then
                SEDraftDoc.Close(False)
                SEApp.DoIdle()
            End If
        Catch ex As Exception
        End Try

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function



    Private Function ParseSubdirectoryFormula(SEDoc As SolidEdgePart.SheetMetalDocument,
                                              Configuration As Dictionary(Of String, String),
                                              SubdirectoryFormula As String
                                              ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim SupplementalExitStatus As Integer

        Dim OutString As String = ""

        Dim FCD As New FilenameCharmapDoctor()

        ' Formatting for subdirectory name formula
        ' Example property callout: %{hmk_Part_Number/CP|G}  
        ' Need to know PropertySet, so maybe: %{Custom.hmk_Part_Number}
        ' For Drafts, maybe: %{Custom.hmk_Part_Number|R1}

        ' Example 1 Formula: "Material_%{System.Material}_Thickness_%{Custom.Material Thickness}"
        ' Example 2 Formula: "%{System.Material} %{Custom.Material Thickness}"

        Dim PropertySet As String
        Dim PropertyName As String

        Dim DocValues As New List(Of String)
        Dim DocValue As String

        Dim StartPositions As New List(Of Integer)
        Dim StartPosition As Integer
        Dim EndPositions As New List(Of Integer)
        Dim EndPosition As Integer
        Dim Length As Integer
        Dim i As Integer
        Dim msg As String

        Dim Proceed As Boolean = True

        Dim LastPipeCharPosition As Integer
        Dim ModelLinkIdx As Integer
        Dim ModelLinkSpecifier As String

        Dim Formulas As New List(Of String)
        Dim Formula As String


        If Not SubdirectoryFormula.Contains("%") Then
            OutString = SubdirectoryFormula
            Proceed = False
        End If

        If Proceed Then

            For StartPosition = 0 To SubdirectoryFormula.Length - 1
                If SubdirectoryFormula.Substring(StartPosition, 1) = "%" Then
                    StartPositions.Add(StartPosition)
                End If
            Next

            For EndPosition = 0 To SubdirectoryFormula.Length - 1
                If SubdirectoryFormula.Substring(EndPosition, 1) = "}" Then
                    EndPositions.Add(EndPosition)
                End If
            Next

            For i = 0 To StartPositions.Count - 1
                Length = EndPositions(i) - StartPositions(i) + 1
                Formulas.Add(SubdirectoryFormula.Substring(StartPositions(i), Length))
            Next

            For Each Formula In Formulas
                Formula = Formula.Replace("%{", "")  ' "%{Custom.hmk_Engineer|R1}" -> "Custom.hmk_Engineer|R1}"
                Formula = Formula.Replace("}", "")   ' "Custom.hmk_Engineer|R1}" -> "Custom.hmk_Engineer|R1"
                i = Formula.IndexOf(".")  ' First occurrence
                PropertySet = Formula.Substring(0, i)    ' "Custom"
                PropertyName = Formula.Substring(i + 1)  ' "hmk_Engineer|R1"

                LastPipeCharPosition = 0
                For i = 0 To PropertyName.Length - 1
                    If PropertyName.Substring(i, 1) = "|" Then
                        LastPipeCharPosition = i
                    End If
                Next

                If LastPipeCharPosition = 0 Then
                    ModelLinkIdx = 0
                Else
                    ModelLinkSpecifier = PropertyName.Substring(LastPipeCharPosition)
                    PropertyName = PropertyName.Substring(0, LastPipeCharPosition)

                    Try
                        ModelLinkIdx = CInt(ModelLinkSpecifier.Replace("|R", ""))
                    Catch ex As Exception
                        Proceed = False
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Could not resolve '{0}'", ModelLinkSpecifier))
                        Exit For
                    End Try
                End If

                SupplementalErrorMessage = GetPropertyValue(SEDoc, Configuration, PropertySet, PropertyName, ModelLinkIdx)
                SupplementalExitStatus = SupplementalErrorMessage.Keys(0)
                If SupplementalExitStatus = 0 Then
                    DocValue = SupplementalErrorMessage(SupplementalExitStatus)(0)
                Else
                    DocValue = ""
                    ExitStatus = 1
                    For Each msg In SupplementalErrorMessage(SupplementalExitStatus)
                        If Not ErrorMessageList.Contains(msg) Then
                            ErrorMessageList.Add(msg)
                        End If
                    Next
                End If
                DocValues.Add(DocValue)
            Next

            If Proceed Then
                OutString = SubdirectoryFormula

                For i = 0 To DocValues.Count - 1
                    OutString = OutString.Replace(Formulas(i), DocValues(i))
                Next
            End If
        End If

        If ExitStatus = 0 Then
            OutString = FCD.SubstituteIllegalCharacters(OutString)
            ErrorMessageList.Add(OutString)
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Private Function GetPropertyValue(SEDoc As SolidEdgePart.SheetMetalDocument,
                                      Configuration As Dictionary(Of String, String),
                                      PropertySet As String,
                                      PropertyName As String,
                                      ModelLinkIdx As Integer
                                      ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim PropertySets As SolidEdgeFramework.PropertySets = Nothing
        Dim Properties As SolidEdgeFramework.Properties = Nothing
        Dim Prop As SolidEdgeFramework.Property = Nothing

        Dim DocValue As String = ""
        Dim PropertyFound As Boolean = False
        Dim tf As Boolean
        Dim msg As String
        Dim Proceed As Boolean = True

        Dim SkippedProperties As String = ""

        Dim ModelDocName As String = ""

        If ModelLinkIdx = 0 Then
            Try
                PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)
            Catch ex As Exception
                Proceed = False
                ExitStatus = 1
                ErrorMessageList.Add("Problem accessing PropertySets.")
            End Try
        Else
            Proceed = False
            ExitStatus = 1
            ErrorMessageList.Add("Formula error.  Model documents do not have index references.")
        End If

        If Proceed Then
            For Each Properties In PropertySets
                msg = String.Format("{0}", Properties.Name)
                For Each Prop In Properties
                    tf = (PropertySet.ToLower = "custom")
                    tf = tf And (Properties.Name.ToLower = "custom")
                    If tf Then
                        ' Some properties do not have names
                        Try
                            If Prop.Name.ToLower = PropertyName.ToLower Then
                                PropertyFound = True
                                DocValue = Prop.Value.ToString
                                Exit For
                            End If
                        Catch ex As Exception
                        End Try
                    Else
                        ' Some properties do not have names
                        Try
                            If Prop.Name.ToLower = PropertyName.ToLower Then
                                PropertyFound = True
                                DocValue = Prop.Value.ToString
                                Exit For
                            End If
                        Catch ex As Exception
                        End Try
                    End If
                Next
                If PropertyFound Then
                    Exit For
                End If
            Next

            If Not PropertyFound Then
                ExitStatus = 1
                If ModelLinkIdx = 0 Then
                    msg = String.Format("Property '{0}' not found", PropertyName)
                    If SkippedProperties.Length > 0 Then
                        msg = String.Format("{0}.  One or more property sets could not be processed: {1}", msg, SkippedProperties)
                    End If
                    ErrorMessageList.Add(msg)
                Else
                    msg = String.Format("Property '{0}' not found in {1}", PropertyName, CommonTasks.TruncateFullPath(ModelDocName, Configuration))
                    If SkippedProperties.Length > 0 Then
                        msg = String.Format("{0}.  One or more property sets could not be processed: {1}", msg, SkippedProperties)
                    End If
                    ErrorMessageList.Add(msg)
                End If
            End If
        End If

        If ExitStatus = 0 Then
            ErrorMessageList.Add(DocValue)
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function InteractiveEdit(
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
                                   AddressOf CommonTasks.InteractiveEdit,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function


    Public Function RunExternalProgram(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim ExternalProgram As String = Configuration("TextBoxExternalProgramSheetmetal")

        ErrorMessage = InvokeSTAThread(
                            Of String,
                            SolidEdgeFramework.SolidEdgeDocument,
                            Dictionary(Of String, String),
                            SolidEdgeFramework.Application,
                            Dictionary(Of Integer, List(Of String)))(
                                AddressOf CommonTasks.RunExternalProgram,
                                ExternalProgram,
                                SEDoc,
                                Configuration,
                                SEApp)

        Return ErrorMessage

    End Function



    Public Function MissingDrawing(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf MissingDrawingInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function MissingDrawingInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim ModelFilename As String
        Dim DrawingFilename As String

        ModelFilename = System.IO.Path.GetFileName(SEDoc.FullName)
        DrawingFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, ".dft")

        If Not FileIO.FileSystem.FileExists(DrawingFilename) Then
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Drawing {0} not found", CommonTasks.TruncateFullPath(DrawingFilename, Configuration)))
        End If


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function




    Public Function PropertyFindReplace(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'ErrorMessage = InvokeSTAThread(
        '                       Of SolidEdgePart.SheetMetalDocument,
        '                       Dictionary(Of String, String),
        '                       SolidEdgeFramework.Application,
        '                       Dictionary(Of Integer, List(Of String)))(
        '                           AddressOf PropertyFindReplaceInternal,
        '                           CType(SEDoc, SolidEdgePart.SheetMetalDocument),
        '                           Configuration,
        '                           SEApp)

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeFramework.SolidEdgeDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf CommonTasks.PropertyFindReplace,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function


    Public Function VariablesEdit(
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
                                   AddressOf CommonTasks.VariablesEdit,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function



    Public Function UpdatePhysicalProperties(
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
                                   AddressOf CommonTasks.UpdatePhysicalProperties,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Public Function CopyOverallSizeToVariableTable(
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
                                   AddressOf CommonTasks.CopyOverallSizeToVariableTable,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function



    Public Function Dummy(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf DummyInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function DummyInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

End Class