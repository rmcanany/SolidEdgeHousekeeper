Option Strict On

Imports SolidEdgeCommunity

Public Class PartTasks
    Inherits IsolatedTaskProxy

    Public Function FailedOrWarnedFeatures(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf FailedOrWarnedFeaturesInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function FailedOrWarnedFeaturesInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
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

                            TF = Status = SolidEdgePart.FeatureStatusConstants.igFeatureFailed
                            TF = TF Or Status = SolidEdgePart.FeatureStatusConstants.igFeatureWarned
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



    Public Function SuppressedOrRolledBackFeatures(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf SuppressedOrRolledBackFeaturesInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function SuppressedOrRolledBackFeaturesInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
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
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UnderconstrainedProfilesInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UnderconstrainedProfilesInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
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
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf InsertPartCopiesOutOfDateInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function InsertPartCopiesOutOfDateInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
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
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf BrokenLinksInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function BrokenLinksInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
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
                        ' TF = TF And CopiedPart.IsUpToDate
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



    Public Function MaterialNotInMaterialTable(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf MaterialNotInMaterialTableInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function MaterialNotInMaterialTableInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim MaterialDoctorPart As New MaterialDoctorPart()

        ErrorMessage = MaterialDoctorPart.MaterialNotInMaterialTable(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function



    Public Function PartNumberDoesNotMatchFilename(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf PartNumberDoesNotMatchFilenameInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function PartNumberDoesNotMatchFilenameInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
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
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UpdateInsertPartCopiesInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UpdateInsertPartCopiesInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
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
                        If Not TF Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Insert part copy file not found: {0}", CopiedPart.FileName))
                        ElseIf Not CopiedPart.IsUpToDate Then
                            CopiedPart.Update()
                            If SEDoc.ReadOnly Then
                                ExitStatus = 1
                                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
                            Else
                                SEDoc.Save()
                                SEApp.DoIdle()
                                ExitStatus = 1
                                ErrorMessageList.Add(String.Format("Updated insert part copy: {0}", CopiedPart.Name))
                            End If
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



    Public Function UpdateMaterialFromMaterialTable(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UpdateMaterialFromMaterialTableInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UpdateMaterialFromMaterialTableInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim MaterialDoctorPart As New MaterialDoctorPart()

        ErrorMessage = MaterialDoctorPart.UpdateMaterialFromMaterialTable(SEDoc, Configuration, SEApp)

        Return ErrorMessage

    End Function



    Public Function UpdateFaceAndViewStylesFromTemplate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UpdateFaceAndViewStylesFromTemplateInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UpdateFaceAndViewStylesFromTemplateInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TempErrorMessageList As New List(Of String)

        Dim SETemplateDoc As SolidEdgePart.PartDocument
        Dim TemplateFilename As String = Configuration("TextBoxTemplatePart")

        ' Import face styles from template
        SEDoc.ImportStyles(TemplateFilename, True)
        SEApp.DoIdle()

        SETemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgePart.PartDocument)
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
        ByRef SEDoc As SolidEdgePart.PartDocument,
        ByRef SETemplateDoc As SolidEdgePart.PartDocument
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
        ByRef SEDoc As SolidEdgePart.PartDocument,
        ByRef SETemplateDoc As SolidEdgePart.PartDocument
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


    'Private Function UpdateFaceAndViewStylesFromTemplateInternal_OLD(
    '    ByVal SEDoc As SolidEdgePart.PartDocument,
    '    ByVal Configuration As Dictionary(Of String, String),
    '    ByVal SEApp As SolidEdgeFramework.Application
    '    ) As Dictionary(Of Integer, List(Of String))

    '    Dim ErrorMessageList As New List(Of String)
    '    Dim ExitStatus As Integer = 0
    '    Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

    '    Dim SETemplateDoc As SolidEdgePart.PartDocument
    '    Dim Windows As SolidEdgeFramework.Windows
    '    Dim Window As SolidEdgeFramework.Window
    '    Dim View As SolidEdgeFramework.View
    '    Dim ViewStyles As SolidEdgeFramework.ViewStyles
    '    Dim ViewStyle As SolidEdgeFramework.ViewStyle
    '    Dim FaceStyles As SolidEdgeFramework.FaceStyles
    '    Dim FaceStyle As SolidEdgeFramework.FaceStyle

    '    Dim TemplateFilename As String = Configuration("TextBoxTemplatePart")
    '    Dim TemplateActiveStyleName As String = ""
    '    Dim TempViewStyleName As String = ""
    '    Dim ViewStyleAlreadyPresent As Boolean
    '    Dim TemplateSkyboxName(5) As String
    '    Dim msg As String = ""
    '    Dim tf As Boolean = False

    '    Dim ConstructionBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
    '    Dim ThreadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
    '    Dim PartBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
    '    Dim CurveBaseStyle As SolidEdgeFramework.FaceStyle = Nothing

    '    Dim TemplateConstructionBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
    '    Dim TemplateThreadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
    '    Dim TemplatePartBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
    '    Dim TemplateCurveBaseStyle As SolidEdgeFramework.FaceStyle = Nothing

    '    SEDoc.ImportStyles(TemplateFilename, True)  ' FaceStyles, that is.

    '    ' Find the active ViewStyle in the template file.
    '    SETemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgePart.PartDocument)
    '    SEApp.DoIdle()

    '    ' Get the template base styles
    '    SETemplateDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seConstructionBaseStyle, TemplateConstructionBaseStyle)
    '    SETemplateDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seThreadedCylindersBaseStyle, TemplateThreadBaseStyle)
    '    SETemplateDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle, TemplatePartBaseStyle)
    '    SETemplateDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seCurveBaseStyle, TemplateCurveBaseStyle)


    '    ' Update base styles in the document
    '    FaceStyles = CType(SEDoc.FaceStyles, SolidEdgeFramework.FaceStyles)

    '    ' Need the doc PartBaseStyle below.  If it is not Nothing, don't overwrite it.
    '    SEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle, PartBaseStyle)

    '    For Each FaceStyle In FaceStyles
    '        If TemplateConstructionBaseStyle IsNot Nothing Then
    '            If FaceStyle.StyleName = TemplateConstructionBaseStyle.StyleName Then
    '                SEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seConstructionBaseStyle,
    '                                   FaceStyle)
    '            End If
    '        End If

    '        If TemplateThreadBaseStyle IsNot Nothing Then
    '            If FaceStyle.StyleName = TemplateThreadBaseStyle.StyleName Then
    '                SEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seThreadedCylindersBaseStyle,
    '                                   FaceStyle)
    '            End If
    '        End If

    '        If TemplatePartBaseStyle IsNot Nothing Then
    '            If PartBaseStyle Is Nothing Then
    '                If FaceStyle.StyleName = TemplatePartBaseStyle.StyleName Then
    '                    SEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle,
    '                                   FaceStyle)
    '                End If
    '            End If
    '        End If

    '        If TemplateCurveBaseStyle IsNot Nothing Then
    '            If FaceStyle.StyleName = TemplateCurveBaseStyle.StyleName Then
    '                SEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seCurveBaseStyle,
    '                                   FaceStyle)
    '            End If
    '        End If
    '    Next


    '    Windows = SETemplateDoc.Windows
    '    For Each Window In Windows
    '        View = Window.View
    '        TemplateActiveStyleName = View.Style.ToString
    '    Next

    '    ViewStyles = CType(SETemplateDoc.ViewStyles, SolidEdgeFramework.ViewStyles)

    '    For Each ViewStyle In ViewStyles
    '        If ViewStyle.StyleName = TemplateActiveStyleName Then
    '            For i As Integer = 0 To 5
    '                TemplateSkyboxName(i) = ViewStyle.GetSkyboxSideFilename(i)
    '            Next
    '        End If
    '    Next

    '    SETemplateDoc.Close(False)
    '    SEApp.DoIdle()

    '    ' If a style by the same name exists in the target file, delete it.
    '    ViewStyleAlreadyPresent = False
    '    ViewStyles = CType(SEDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
    '    For Each ViewStyle In ViewStyles
    '        If ViewStyle.StyleName = TemplateActiveStyleName Then
    '            ViewStyleAlreadyPresent = True
    '        Else
    '            TempViewStyleName = ViewStyle.StyleName
    '        End If
    '    Next

    '    SEApp.DoIdle()

    '    Windows = SEDoc.Windows

    '    If ViewStyleAlreadyPresent Then ' Hopefully deactivate the desired ViewStyle so it can be removed
    '        For Each Window In Windows
    '            View = Window.View
    '            View.Style = TempViewStyleName
    '        Next
    '        ' ViewStyles can sometimes be flagged 'in use' even if they are not
    '        Try
    '            ViewStyles.Remove(TemplateActiveStyleName)
    '        Catch ex As Exception
    '            ExitStatus = 1
    '            ErrorMessageList.Add("View style not updated")
    '        End Try
    '    End If

    '    If ExitStatus = 0 Then
    '        ViewStyles.AddFromFile(TemplateFilename, TemplateActiveStyleName)

    '        For Each ViewStyle In ViewStyles
    '            If ViewStyle.StyleName = TemplateActiveStyleName Then
    '                ViewStyle.SkyboxType = SolidEdgeFramework.SeSkyboxType.seSkyboxTypeSkybox
    '                For i As Integer = 0 To 5
    '                    ViewStyle.SetSkyboxSideFilename(i, TemplateSkyboxName(i))
    '                Next
    '            End If
    '        Next

    '        For Each Window In Windows
    '            View = Window.View
    '            View.Style = TemplateActiveStyleName
    '        Next

    '        SEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seConstructionBaseStyle,
    '                       ConstructionBaseStyle)
    '        SEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seThreadedCylindersBaseStyle,
    '                   ThreadBaseStyle)
    '        SEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle,
    '                   PartBaseStyle)
    '        SEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seCurveBaseStyle,
    '                   CurveBaseStyle)

    '        tf = ConstructionBaseStyle Is Nothing
    '        tf = tf Or (ThreadBaseStyle Is Nothing)
    '        tf = tf Or (PartBaseStyle Is Nothing)
    '        tf = tf Or (CurveBaseStyle Is Nothing)

    '        If tf Then
    '            ExitStatus = 1
    '            ErrorMessageList.Add("Some Color Manager base styles undefined.")
    '        End If

    '        If SEDoc.ReadOnly Then
    '            ExitStatus = 1
    '            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
    '        Else
    '            SEDoc.Save()
    '            SEApp.DoIdle()
    '        End If
    '    End If

    '    ErrorMessage(ExitStatus) = ErrorMessageList
    '    Return ErrorMessage
    'End Function



    Public Function OpenSave(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf OpenSaveInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function OpenSaveInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

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
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf HideConstructionsInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function HideConstructionsInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
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

        '' SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.PartViewISOView, SolidEdgeFramework.SolidEdgeCommandConstants))
        'SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.SheetMetalViewTrimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))
        'SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.PartViewFit, SolidEdgeFramework.SolidEdgeCommandConstants))

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
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf FitPictorialViewInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function FitPictorialViewInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
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

        If Models.Count > 0 Then
            'RefPlanes = SEDoc.RefPlanes
            'For Each RefPlane In RefPlanes
            '    RefPlane.Visible = False
            'Next
        Else
            RefPlanes = SEDoc.RefPlanes
            For Each RefPlane In RefPlanes
                RefPlane.Visible = True
            Next
        End If

        ''Some imported files crash on this command
        'Try
        '    SEDoc.Constructions.Visible = False
        'Catch ex As Exception
        'End Try

        'SEDoc.CoordinateSystems.Visible = False

        ' SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.PartViewISOView, SolidEdgeFramework.SolidEdgeCommandConstants))
        ' SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.SheetMetalViewTrimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))

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
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf SaveAsInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function SaveAsInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim SupplementalExitStatus As Integer = 0
        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim NewFilename As String = ""
        Dim NewExtension As String = ""
        Dim PartBaseFilename As String

        Dim BaseDir As String
        Dim SubDir As String
        Dim Formula As String
        Dim Proceed As Boolean = True

        Dim ExitMessage As String = ""

        Dim ImageExtensions As New List(Of String)

        ImageExtensions.Add(".bmp")
        ImageExtensions.Add(".jpg")
        ImageExtensions.Add(".png")
        ImageExtensions.Add(".tif")

        ' ComboBoxSaveAsPartFiletype
        ' Format: Parasolid (*.xt), IGES (*.igs)
        NewExtension = Configuration("ComboBoxSaveAsPartFileType")
        NewExtension = Split(NewExtension, Delimiter:="*")(1)
        NewExtension = Split(NewExtension, Delimiter:=")")(0)

        PartBaseFilename = System.IO.Path.GetFileName(SEDoc.FullName)

        ' CheckBoxStepPartOutputDirectory
        If Configuration("CheckBoxSaveAsPartOutputDirectory") = "False" Then
            BaseDir = Configuration("TextBoxSaveAsPartOutputDirectory")

            If Configuration("CheckBoxSaveAsFormulaPart").ToLower = "true" Then
                Formula = Configuration("TextBoxSaveAsFormulaPart")

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

                'SubDir = ParseSubdirectoryFormula(SEDoc, Formula)

                'If SubDir = "" Then
                '    Proceed = False
                '    ExitStatus = 1
                '    ErrorMessageList.Add(String.Format("Property not found or other issue with formula '{0}'", Formula))
                'Else
                '    BaseDir = String.Format("{0}\{1}", BaseDir, SubDir)
                '    If Not FileIO.FileSystem.DirectoryExists(BaseDir) Then
                '        Try
                '            FileIO.FileSystem.CreateDirectory(BaseDir)
                '        Catch ex As Exception
                '            Proceed = False
                '            ExitStatus = 1
                '            ErrorMessageList.Add(String.Format("Could not create '{0}'", BaseDir))
                '        End Try
                '    End If
                'End If

            End If

            If Proceed Then
                NewFilename = BaseDir + "\" + System.IO.Path.ChangeExtension(PartBaseFilename, NewExtension)
            End If

            ' NewFilename = Configuration("TextBoxSaveAsPartOutputDirectory") + "\" + System.IO.Path.ChangeExtension(PartBaseFilename, NewExtension)
        Else
            NewFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, NewExtension)
        End If

        If Proceed Then
            'Capturing a fault to update ExitStatus
            Try
                If Not ImageExtensions.Contains(NewExtension) Then
                    SEDoc.SaveAs(NewFilename)
                    SEApp.DoIdle()
                Else
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
                        ExitMessage = CropImage(Configuration, SEDoc, NewFilename, NewExtension, Window.Height, Window.Width)
                        If Not ExitMessage = "" Then
                            ExitStatus = 1
                            ErrorMessageList.Add(ExitMessage)
                        End If
                    End If
                End If
            Catch ex As Exception
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Error saving {0}", TruncateFullPath(NewFilename, Configuration)))
            End Try

        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    'Private Function SaveAsPNG(View As SolidEdgeFramework.View,
    '                           NewFilename As String
    '                           ) As String

    '    Dim ExitCode As Integer = 0
    '    Dim ExitMessage As String = ""
    '    Dim FfmpegCmd As String
    '    Dim FfmpegArgs As String
    '    Dim P As New Process
    '    Dim TempFilename As String

    '    Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()

    '    TempFilename = NewFilename.Replace(".png", "-Housekeeper.jpg")

    '    View.SaveAsImage(TempFilename)

    '    FfmpegCmd = String.Format("{0}\ffmpeg.exe", StartupPath)

    '    FfmpegArgs = String.Format("-y -i {0}{1}{2} ", Chr(34), TempFilename, Chr(34))
    '    FfmpegArgs = String.Format("{0} {1}{2}{3}", FfmpegArgs, Chr(34), NewFilename, Chr(34))

    '    Try

    '        P = Process.Start(FfmpegCmd, FfmpegArgs)
    '        P.WaitForExit()
    '        ExitCode = P.ExitCode

    '        If ExitCode = 0 Then
    '            System.IO.File.Delete(TempFilename)
    '        Else
    '            ExitMessage = String.Format("Unable to save '{0}'", NewFilename)
    '        End If

    '    Catch ex As Exception
    '        ExitMessage = String.Format("Unable to save '{0}'.  ", NewFilename)
    '        ExitMessage = String.Format("{0}  Verify the following file is present on the system '{1}'.  ", ExitMessage, FfmpegCmd)
    '    End Try



    '    Return ExitMessage
    'End Function

    Private Function CropImage(Configuration As Dictionary(Of String, String),
                          SEDoc As SolidEdgePart.PartDocument,
                          NewFilename As String,
                          NewExtension As String,
                          WindowH As Integer,
                          WindowW As Integer
                          ) As String

        Dim ModelX As Double
        Dim ModelY As Double
        Dim ModelZ As Double
        'Dim XMin As Double = 1000000
        'Dim YMin As Double = 1000000
        'Dim ZMin As Double = 1000000
        'Dim XMax As Double = -1000000
        'Dim YMax As Double = -1000000
        'Dim ZMax As Double = -1000000

        Dim ImageW As Double
        Dim ImageH As Double
        Dim ImageAspectRatio As Double

        Dim CropW As Integer
        Dim CropH As Integer

        Dim FfmpegCmd As String
        Dim FfmpegArgs As String
        Dim P As New Process
        Dim TempFilename As String

        Dim ExitCode As Integer = 0
        Dim ExitMessage As String = ""

        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()

        Dim WindowAspectRatio As Double = WindowH / WindowW

        Dim Models As SolidEdgePart.Models
        Dim Model As SolidEdgePart.Model
        Dim Body As SolidEdgeGeometry.Body

        Dim FeatureDoctor As New FeatureDoctor
        Dim PointsList As New List(Of Double)
        Dim PointsListTemp As New List(Of Double)
        Dim Point As Double

        Models = SEDoc.Models

        If (Models.Count = 0) Then
            ExitMessage = "No models to process.  Cropped image not created."
            Return ExitMessage
        End If
        If (Models.Count = 0) Or (Models.Count > 25) Then
            ExitMessage = "Too many models to process.  Cropped image not created."
            Return ExitMessage
        End If

        For Each Model In Models
            Body = CType(Model.Body, SolidEdgeGeometry.Body)
            PointsListTemp = FeatureDoctor.GetBodyRange(Body)
            If PointsList.Count = 0 Then
                For Each Point In PointsListTemp
                    PointsList.Add(Point)
                Next
            Else
                For i As Integer = 0 To 2
                    If PointsListTemp(i) < PointsList(i) Then
                        PointsList(i) = PointsListTemp(i)
                    End If
                Next
                For i As Integer = 3 To 5
                    If PointsListTemp(i) > PointsList(i) Then
                        PointsList(i) = PointsListTemp(i)
                    End If
                Next
            End If
        Next

        ModelX = PointsList(3) - PointsList(0) 'XMax - XMin
        ModelY = PointsList(4) - PointsList(1) ' YMax - YMin
        ModelZ = PointsList(5) - PointsList(2) ' ZMax - ZMin

        If Configuration("RadioButtonPictorialViewIsometric").ToLower = "true" Then
            ImageW = 0.707 * ModelX + 0.707 * ModelY
            ImageH = 0.40833 * ModelX + 0.40833 * ModelY + 0.81689 * ModelZ
        ElseIf Configuration("RadioButtonPictorialViewDimetric").ToLower = "true" Then
            ImageW = 0.9356667 * ModelX + 0.353333 * ModelY
            ImageH = 0.117222 * ModelX + 0.311222 * ModelY + 0.942444 * ModelZ
        Else
            ImageW = 0.557 * ModelX + 0.830667 * ModelY
            ImageH = 0.325444 * ModelX + 0.217778 * ModelY + 0.920444 * ModelZ
        End If

        ImageAspectRatio = ImageH / ImageW

        If WindowAspectRatio > ImageAspectRatio Then
            CropH = CInt(Math.Round(WindowW * ImageAspectRatio))
            CropW = WindowW
        Else
            CropH = WindowH
            CropW = CInt(Math.Round(WindowH / ImageAspectRatio))
        End If

        TempFilename = NewFilename.Replace(NewExtension, String.Format("-Housekeeper{0}", NewExtension))

        FfmpegCmd = String.Format("{0}\ffmpeg.exe", StartupPath)

        FfmpegArgs = String.Format("-y -i {0}{1}{2} ", Chr(34), NewFilename, Chr(34))
        FfmpegArgs = String.Format("{0} -vf crop={1}:{2} ", FfmpegArgs, CropW, CropH)
        FfmpegArgs = String.Format("{0} {1}{2}{3}", FfmpegArgs, Chr(34), TempFilename, Chr(34))

        Try
            P = Process.Start(FfmpegCmd, FfmpegArgs)
            P.WaitForExit()
            ExitCode = P.ExitCode

            If ExitCode = 0 Then
                System.IO.File.Delete(NewFilename)
                FileSystem.Rename(TempFilename, NewFilename)
            Else
                ExitMessage = String.Format("Unable to save cropped image '{0}'", TempFilename)
            End If

        Catch ex As Exception
            ExitMessage = String.Format("Unable to save cropped image '{0}'.  ", TempFilename)
            ExitMessage = String.Format("{0}  Verify the following file is present on the system '{1}'.  ", ExitMessage, FfmpegCmd)
        End Try



        Return ExitMessage

    End Function

    Private Function ParseSubdirectoryFormula(SEDoc As SolidEdgePart.PartDocument,
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

    Private Function GetPropertyValue(SEDoc As SolidEdgePart.PartDocument,
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
                    msg = String.Format("Property '{0}' not found in {1}", PropertyName, TruncateFullPath(SEDoc.FullName, Configuration))
                    ErrorMessageList.Add(msg)
                Else
                    msg = String.Format("Property '{0}' not found in {1}", PropertyName, TruncateFullPath(ModelDocName, Configuration))
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


    'Private Function ParseSubdirectoryFormula(SEDoc As SolidEdgePart.PartDocument,
    '                                          SubdirectoryFormula As String
    '                                          ) As String
    '    Dim OutString As String = ""

    '    ' Formatting for subdirectory name formula
    '    ' Example property callout: %{hmk_Part_Number/CP|G}  
    '    ' Need to know PropertySet, so maybe: %{Custom.hmk_Part_Number}
    '    ' For Drafts, maybe: %{Custom.hmk_Part_Number|R1}

    '    ' Example 1 Formula: "Material_%{System.Material}_Thickness_%{Custom.Material Thickness}"
    '    ' Example 2 Formula: "%{System.Material} %{Custom.Material Thickness}"

    '    If Not SubdirectoryFormula.Contains("%") Then
    '        Return SubdirectoryFormula
    '    End If

    '    Dim PropertySet As String
    '    Dim PropertyName As String

    '    Dim DocValues As New List(Of String)
    '    Dim DocValue As String

    '    Dim StartPositions As New List(Of Integer)
    '    Dim StartPosition As Integer
    '    Dim EndPositions As New List(Of Integer)
    '    Dim EndPosition As Integer
    '    Dim Length As Integer
    '    Dim i As Integer

    '    Dim Formulas As New List(Of String)
    '    Dim Formula As String

    '    For StartPosition = 0 To SubdirectoryFormula.Length - 1
    '        If SubdirectoryFormula.Substring(StartPosition, 1) = "%" Then
    '            StartPositions.Add(StartPosition)
    '        End If
    '    Next

    '    For EndPosition = 0 To SubdirectoryFormula.Length - 1
    '        If SubdirectoryFormula.Substring(EndPosition, 1) = "}" Then
    '            EndPositions.Add(EndPosition)
    '        End If
    '    Next

    '    For i = 0 To StartPositions.Count - 1
    '        Length = EndPositions(i) - StartPositions(i) + 1
    '        Formulas.Add(SubdirectoryFormula.Substring(StartPositions(i), Length))
    '    Next

    '    For Each Formula In Formulas
    '        Formula = Formula.Replace("%{", "")
    '        Formula = Formula.Replace("}", "")
    '        i = Formula.IndexOf(".")
    '        'PropertySet = Formula.Split("."c)(0)
    '        'PropertyName = Formula.Split("."c)(1)
    '        PropertySet = Formula.Substring(0, i)
    '        PropertyName = Formula.Substring(i + 1)
    '        DocValue = GetPropertyValue(SEDoc, PropertySet, PropertyName).Trim
    '        If DocValue = "" Then
    '            Return ""
    '        End If
    '        DocValues.Add(DocValue)
    '    Next

    '    OutString = SubdirectoryFormula

    '    For i = 0 To DocValues.Count - 1
    '        OutString = OutString.Replace(Formulas(i), DocValues(i))
    '    Next

    '    Return OutString
    'End Function

    'Private Function GetPropertyValue(SEDoc As SolidEdgePart.PartDocument,
    '                                  PropertySet As String,
    '                                  PropertyName As String
    '                                  ) As String

    '    Dim PropertySets As SolidEdgeFramework.PropertySets = Nothing
    '    Dim Properties As SolidEdgeFramework.Properties = Nothing
    '    Dim Prop As SolidEdgeFramework.Property = Nothing

    '    Dim DocValue As String = ""
    '    Dim PropertyFound As Boolean = False
    '    Dim tf As Boolean

    '    PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)

    '    For Each Properties In PropertySets
    '        For Each Prop In Properties
    '            tf = (PropertySet.ToLower = "custom")
    '            tf = tf And (Properties.Name.ToLower = "custom")
    '            If tf Then
    '                If Prop.Name.ToLower = PropertyName.ToLower Then
    '                    PropertyFound = True
    '                    DocValue = Prop.Value.ToString
    '                    Exit For
    '                End If
    '            Else
    '                If Prop.Name.ToLower = PropertyName.ToLower Then
    '                    PropertyFound = True
    '                    DocValue = Prop.Value.ToString
    '                    Exit For
    '                End If
    '            End If
    '        Next
    '        If PropertyFound Then
    '            Exit For
    '        End If
    '    Next

    '    Return DocValue
    'End Function



    Public Function InteractiveEdit(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf InteractiveEditInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function InteractiveEditInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Result As MsgBoxResult
        Dim msg As String
        Dim indent As String = "    "

        SEApp.DisplayAlerts = True

        msg = String.Format("When finished, do one of the following:{0}", vbCrLf)
        msg = String.Format("{0}{1}Click Yes to save and close{2}", msg, indent, vbCrLf)
        msg = String.Format("{0}{1}Click No to close without saving{2}", msg, indent, vbCrLf)
        msg = String.Format("{0}{1}Click Cancel to quit{2}", msg, indent, vbCrLf)

        Result = MsgBox(msg, MsgBoxStyle.YesNoCancel Or MsgBoxStyle.SystemModal, Title:="Solid Edge Housekeeper")

        If Result = vbYes Then
            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save read-only file.")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        ElseIf Result = vbNo Then
            'ExitStatus = 1
            'ErrorMessageList.Add("File was not saved.")
        Else  ' Cancel was chosen
            ExitStatus = 99
            ErrorMessageList.Add("Operation was cancelled.")
        End If

        SEApp.DisplayAlerts = False

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function RunExternalProgram(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf RunExternalProgramInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function RunExternalProgramInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim ExternalProgram As String = Configuration("TextBoxExternalProgramPart")
        Dim P As New Process
        Dim ExitCode As Integer
        Dim ErrorMessageFilename As String
        Dim ErrorMessages As String()
        Dim Key As String
        Dim Value As String

        ErrorMessageFilename = String.Format("{0}\error_messages.txt", System.IO.Path.GetDirectoryName(ExternalProgram))

        P = Process.Start(ExternalProgram)
        P.WaitForExit()
        ExitCode = P.ExitCode

        If ExitCode <> 0 Then
            ExitStatus = 1
            If FileIO.FileSystem.FileExists(ErrorMessageFilename) Then
                Dim KeyFound As Boolean = False
                ErrorMessages = IO.File.ReadAllLines(ErrorMessageFilename)
                For Each KVPair As String In ErrorMessages
                    ' Error message file format:
                    ' 1 Some error occurred
                    ' 2 Some other error occurred

                    KVPair = Trim(KVPair)

                    Key = Split(KVPair, Delimiter:=" ")(0)
                    If Key = CStr(ExitCode) Then
                        Value = KVPair.Substring(Len(Key) + 1)
                        ErrorMessageList.Add(Value)
                        Exit For
                    End If
                Next
                If Not KeyFound Then
                    ErrorMessageList.Add(String.Format("Program terminated with exit code {0}", ExitCode))
                End If
            Else
                ErrorMessageList.Add(String.Format("Program terminated with exit code {0}", ExitCode))
            End If
        End If


        If Configuration("CheckBoxRunExternalProgramSaveFile").ToLower = "true" Then
            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function MissingDrawing(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf MissingDrawingInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function MissingDrawingInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
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
            ErrorMessageList.Add(String.Format("Drawing {0} not found", TruncateFullPath(DrawingFilename, Configuration)))
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

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf PropertyFindReplaceInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function PropertyFindReplaceInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))


        Dim PropertySets As SolidEdgeFramework.PropertySets = Nothing
        Dim Properties As SolidEdgeFramework.Properties = Nothing
        Dim Prop As SolidEdgeFramework.Property = Nothing

        Dim PropertyFound As Boolean = False
        Dim tf As Boolean
        Dim FindString As String = Configuration("TextBoxFindReplaceFindPart")
        Dim ReplaceString As String = Configuration("TextBoxFindReplaceReplacePart")
        Dim ReplaceStringDummy As String = String.Format("{0}_", ReplaceString)

        Dim Proceed As Boolean = True

        Try
            PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)
        Catch ex As Exception
            Proceed = False
            ExitStatus = 1
            ErrorMessageList.Add("Problem accessing PropertySets.")
        End Try

        If Proceed Then
            For Each Properties In PropertySets
                For Each Prop In Properties
                    tf = (Configuration("ComboBoxFindReplacePropertySetPart").ToLower = "custom")
                    tf = tf And (Properties.Name.ToLower = "custom")
                    If tf Then
                        ' Some properties do not have names.
                        Try
                            If Prop.Name = Configuration("TextBoxFindReplacePropertyNamePart") Then
                                PropertyFound = True
                                ' Only works on text type properties
                                Try
                                    Prop.Value = Replace(CType(Prop.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)
                                    Properties.Save()
                                    SEApp.DoIdle()
                                Catch ex As Exception
                                    ExitStatus = 1
                                    ErrorMessageList.Add("Unable to replace property value.  This command only works on text type properties.")
                                End Try
                                Exit For
                            End If
                        Catch ex As Exception
                        End Try
                    Else
                        ' Some properties do not have names.
                        Try
                            If Prop.Name = Configuration("TextBoxFindReplacePropertyNamePart") Then
                                PropertyFound = True
                                ' Only works on text type properties
                                Try
                                    Prop.Value = Replace(CType(Prop.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)
                                    Properties.Save()
                                    SEApp.DoIdle()
                                Catch ex As Exception
                                    ExitStatus = 1
                                    ErrorMessageList.Add("Unable to replace property value.  This command only works on text type properties.")
                                End Try
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

            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If

        End If


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function ExposeVariables(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf ExposeVariablesInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function ExposeVariablesInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim DisplayName As String
        Dim ExposeValue As Integer

        Dim Variables As SolidEdgeFramework.Variables = Nothing
        Dim VariableList As SolidEdgeFramework.VariableList = Nothing
        Dim Variable As SolidEdgeFramework.variable = Nothing
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension = Nothing
        Dim VariableListItemTypeName As String

        Dim VariablesToExpose As String
        Dim VariablesToExposeDict As New Dictionary(Of String, String)

        VariablesToExpose = Configuration("TextBoxExposeVariablesPart")
        VariablesToExposeDict = StringToDict(VariablesToExpose, ","c, ":"c)

        Variables = DirectCast(SEDoc.Variables, SolidEdgeFramework.Variables)

        VariableList = DirectCast(Variables.Query(pFindCriterium:="*",
                                  NamedBy:=SolidEdgeConstants.VariableNameBy.seVariableNameByBoth,
                                  VarType:=SolidEdgeConstants.VariableVarType.SeVariableVarTypeBoth),
                                  SolidEdgeFramework.VariableList)

        For Each VariableListItem In VariableList.OfType(Of Object)()
            VariableListItemTypeName = Microsoft.VisualBasic.Information.TypeName(VariableListItem)

            If VariableListItemTypeName.ToLower() = "dimension" Then
                Dimension = CType(VariableListItem, SolidEdgeFrameworkSupport.Dimension)
                ExposeValue = Dimension.Expose
                DisplayName = Dimension.DisplayName
                If VariablesToExposeDict.Keys.Contains(DisplayName) Then
                    Try
                        Dimension.Expose = 1
                        Dimension.ExposeName = VariablesToExposeDict(DisplayName)
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Unable to expose '{0}'", DisplayName))
                    End Try
                End If

            ElseIf VariableListItemTypeName.ToLower() = "variable" Then
                Variable = CType(VariableListItem, SolidEdgeFramework.variable)
                ExposeValue = Variable.Expose
                DisplayName = Variable.DisplayName
                If VariablesToExposeDict.Keys.Contains(DisplayName) Then
                    Try
                        Variable.Expose = 1
                        Variable.ExposeName = VariablesToExposeDict(DisplayName)
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Unable to expose '{0}'", DisplayName))
                    End Try
                End If
            End If

        Next

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



    Public Function ExposeVariablesMissing(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf ExposeVariablesMissingInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function ExposeVariablesMissingInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Variables As SolidEdgeFramework.Variables = Nothing
        Dim VariableList As SolidEdgeFramework.VariableList = Nothing
        Dim Variable As SolidEdgeFramework.variable = Nothing
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension = Nothing
        Dim VariableListItemTypeName As String

        Dim VariablesToExpose As String
        Dim VariablesToExposeDict As New Dictionary(Of String, String)

        Dim VariablesPresentInDocument As New List(Of String)

        VariablesToExpose = Configuration("TextBoxExposeVariablesPart")
        VariablesToExposeDict = StringToDict(VariablesToExpose, ","c, ":"c)

        Variables = DirectCast(SEDoc.Variables, SolidEdgeFramework.Variables)

        VariableList = DirectCast(Variables.Query(pFindCriterium:="*",
                                  NamedBy:=SolidEdgeConstants.VariableNameBy.seVariableNameByBoth,
                                  VarType:=SolidEdgeConstants.VariableVarType.SeVariableVarTypeBoth),
                                  SolidEdgeFramework.VariableList)

        For Each VariableListItem In VariableList.OfType(Of Object)()
            VariableListItemTypeName = Microsoft.VisualBasic.Information.TypeName(VariableListItem)

            If VariableListItemTypeName.ToLower() = "dimension" Then
                Dimension = CType(VariableListItem, SolidEdgeFrameworkSupport.Dimension)
                VariablesPresentInDocument.Add(Dimension.DisplayName)

            ElseIf VariableListItemTypeName.ToLower() = "variable" Then
                Variable = CType(VariableListItem, SolidEdgeFramework.variable)
                VariablesPresentInDocument.Add(Variable.DisplayName)
            End If

        Next

        For Each Key As String In VariablesToExposeDict.Keys
            If Not VariablesPresentInDocument.Contains(Key) Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Variable '{0}' not found", Key))
            End If
        Next


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Private Function StringToDict(s As String, delimiter1 As Char, delimiter2 As Char) As Dictionary(Of String, String)
        ' Takes a double-delimited string and returns a dictionary
        ' delimiter1 separates entries in the dictionary
        ' delimiter2 separates the Key from the Value in each entry.

        ' Example string: "weight: Weight of Object, length:, width"
        ' Returns a dictionary like:

        ' {"weight": "Weight of Object",
        '  "length": "length",
        '  "width": "width"}

        ' Notes
        ' Whitespace before and after each Key and Value is removed.
        ' To convert a single string, say ",", to a char, do ","c
        ' If delimiter2 is not present in an entry, or there is nothing after delimiter2, the Key and Value are the same.

        Dim D As New Dictionary(Of String, String)
        Dim A() As String
        Dim K As String
        Dim V As String

        A = s.Split(delimiter1)

        For i As Integer = 0 To A.Length - 1
            If A(i).Contains(delimiter2) Then
                K = A(i).Split(delimiter2)(0).Trim
                V = A(i).Split(delimiter2)(1).Trim

                If V = "" Then
                    V = K
                End If
            Else
                K = A(i).Trim
                V = K
            End If

            D.Add(K, V)

        Next

        Return D

    End Function

    Private Function TruncateFullPath(ByVal Path As String,
         Configuration As Dictionary(Of String, String)
         ) As String

        Dim Length As Integer = Len(Configuration("TextBoxInputDirectory"))
        Dim NewPath As String

        If Path.Contains(Configuration("TextBoxInputDirectory")) Then
            NewPath = Path.Remove(0, Length)
            NewPath = "~" + NewPath
        Else
            NewPath = Path
        End If
        Return NewPath
    End Function



    Public Function Dummy(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgePart.PartDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf DummyInternal,
                                   CType(SEDoc, SolidEdgePart.PartDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function DummyInternal(
        ByVal SEDoc As SolidEdgePart.PartDocument,
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
