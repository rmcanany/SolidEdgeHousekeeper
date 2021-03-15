Option Strict On

Imports SolidEdgeCommunity

Public Class SheetmetalTasks
    Inherits IsolatedTaskProxy

    Public Function FailedOrWarnedFeatures(
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
                                   AddressOf FailedOrWarnedFeaturesInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function FailedOrWarnedFeaturesInternal(
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

        ' Not applicable in sync models.
        If SEDoc.ModelingMode.ToString = "seModelingModeOrdered" Then
            For Each ProfileSet In ProfileSets
                If ProfileSet.IsUnderDefined Then
                    ExitStatus = 1
                End If
            Next
        End If

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

        Dim Models As SolidEdgePart.Models = SEDoc.Models
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
            If Not Filename.Contains(PartNumber) Then
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


    Public Function GenerateLaserDXFAndPDF(
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
                                   AddressOf GenerateLaserDXFAndPDFInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function GenerateLaserDXFAndPDFInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim ErrorMessageTemp As New Dictionary(Of Integer, List(Of String))

        Dim DraftTasks As New DraftTasks

        Dim DraftFilename As String
        Dim DraftFileMissing As Boolean = False
        Dim DraftOutOfDate As Boolean = False
        Dim FlatPatternOutOfDate As Boolean = False
        Dim DXFFilename As String
        Dim PDFFilename As String
        Dim SheetmetalBaseFilename As String

        Dim SEDraftDoc As SolidEdgeDraft.DraftDocument = Nothing
        Dim Models As SolidEdgePart.Models = Nothing

        Dim msg As String = ""

        ' CheckBoxLaserOutputDirectory
        If Configuration("CheckBoxLaserOutputDirectory") = "False" Then
            If Not FileIO.FileSystem.DirectoryExists(Configuration("TextBoxLaserOutputDirectory")) Then
                msg = "Laser output directory not found: '" + Configuration("TextBoxLaserOutputDirectory") + "'.  "
                msg += "Please select a valid directory on the Sheetmetal Tab." + Chr(13)
                msg += "Exiting..."
                MsgBox(msg)
                SEApp.Quit()
                End
            End If
        End If

        DraftFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, ".dft")
        If Not FileIO.FileSystem.FileExists(DraftFilename) Then
            DraftFileMissing = True
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Draft document not found: {0}", TruncateFullPath(DraftFilename, Configuration)))
        Else
            SEDraftDoc = CType(SEApp.Documents.Open(DraftFilename), SolidEdgeDraft.DraftDocument)
            SEApp.DoIdle()
        End If

        SheetmetalBaseFilename = System.IO.Path.GetFileName(SEDoc.FullName)
        If Configuration("CheckBoxLaserOutputDirectory") = "False" Then
            DXFFilename = Configuration("TextBoxLaserOutputDirectory") + "\" + System.IO.Path.ChangeExtension(SheetmetalBaseFilename, ".dxf")
            PDFFilename = Configuration("TextBoxLaserOutputDirectory") + "\" + System.IO.Path.ChangeExtension(SheetmetalBaseFilename, ".pdf")
        Else
            DXFFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, ".dxf")
            PDFFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, ".pdf")
        End If

        ErrorMessageTemp = FlatPatternMissingOrOutOfDate(CType(SEDoc, SolidEdgeFramework.SolidEdgeDocument), Configuration, SEApp)
        If ExitStatus = 0 Then
            ExitStatus = ErrorMessageTemp.Keys(0)
        End If
        If ErrorMessageTemp.Keys(0) = 1 Then
            FlatPatternOutOfDate = True
            ErrorMessageList.Add("Flat pattern missing or out of date")
        End If

        If Not DraftFileMissing Then
            ErrorMessageTemp = DraftTasks.DrawingViewsOutOfDate(CType(SEDraftDoc, SolidEdgeFramework.SolidEdgeDocument), Configuration, SEApp)
            If ExitStatus = 0 Then
                ExitStatus = ErrorMessageTemp.Keys(0)
            End If
            If ErrorMessageTemp.Keys(0) = 1 Then
                DraftOutOfDate = True
                ErrorMessageList.Add("Draft views out of date")
            End If
        End If

        'Save flat pattern even if there is no drawing
        'Don't save if there is a drawing but it is out of date
        If (Not FlatPatternOutOfDate) And (Not DraftOutOfDate) Then
            Models = SEDoc.Models
            Models.SaveAsFlatDXFEx(DXFFilename, Nothing, Nothing, Nothing, True)
            SEApp.DoIdle()
        End If

        'Don't save PDF if the flat pattern or draft is out of date
        If (Not DraftFileMissing) And (Not FlatPatternOutOfDate) And (Not DraftOutOfDate) Then
            SEDraftDoc.SaveAs(PDFFilename)
            SEApp.DoIdle()
        End If

        If SEDraftDoc IsNot Nothing Then
            SEDraftDoc.Close(False)
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Public Function SaveAsFlatDXF(
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
                                   AddressOf SaveAsFlatDXFInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function SaveAsFlatDXFInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageTemp As New Dictionary(Of Integer, List(Of String))
        Dim FlatPatternOutOfDate As Boolean = False
        Dim DXFFilename As String
        Dim SheetmetalBaseFilename As String

        Dim SEDraftDoc As SolidEdgeDraft.DraftDocument = Nothing
        Dim Models As SolidEdgePart.Models = Nothing

        SheetmetalBaseFilename = System.IO.Path.GetFileName(SEDoc.FullName)
        If Configuration("CheckBoxSaveAsFlatDXFOutputDirectory") = "False" Then
            DXFFilename = Configuration("TextBoxSaveAsFlatDXFOutputDirectory") + "\" + System.IO.Path.ChangeExtension(SheetmetalBaseFilename, ".dxf")
        Else
            DXFFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, ".dxf")
        End If

        ErrorMessageTemp = FlatPatternMissingOrOutOfDate(CType(SEDoc, SolidEdgeFramework.SolidEdgeDocument), Configuration, SEApp)
        If ExitStatus = 0 Then
            ExitStatus = ErrorMessageTemp.Keys(0)
        End If
        If ErrorMessageTemp.Keys(0) = 1 Then
            FlatPatternOutOfDate = True
            ErrorMessageList.Add("Flat pattern missing or out of date")
        End If

        If Not FlatPatternOutOfDate Then
            Models = SEDoc.Models
            Models.SaveAsFlatDXFEx(DXFFilename, Nothing, Nothing, Nothing, True)
            SEApp.DoIdle()
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

    Private Function UpdateInsertPartCopiesInternal(
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

        Dim SETemplateDoc As SolidEdgePart.SheetMetalDocument
        Dim Windows As SolidEdgeFramework.Windows
        Dim Window As SolidEdgeFramework.Window
        Dim View As SolidEdgeFramework.View
        Dim ViewStyles As SolidEdgeFramework.ViewStyles
        Dim ViewStyle As SolidEdgeFramework.ViewStyle

        Dim TemplateFilename As String = Configuration("TextBoxTemplateSheetmetal")
        Dim TemplateActiveStyleName As String = ""
        Dim TempViewStyleName As String = ""
        Dim ViewStyleAlreadyPresent As Boolean
        Dim TemplateSkyboxName(5) As String
        Dim msg As String = ""

        SEDoc.ImportStyles(TemplateFilename, True)

        ' Find the active ViewStyle in the template file.
        SETemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgePart.SheetMetalDocument)
        SEApp.DoIdle()

        Windows = SETemplateDoc.Windows
        For Each Window In Windows
            View = Window.View
            TemplateActiveStyleName = View.Style.ToString
        Next

        ViewStyles = CType(SETemplateDoc.ViewStyles, SolidEdgeFramework.ViewStyles)

        For Each ViewStyle In ViewStyles
            If ViewStyle.StyleName = TemplateActiveStyleName Then
                For i As Integer = 0 To 5
                    TemplateSkyboxName(i) = ViewStyle.GetSkyboxSideFilename(i)
                Next
            End If
        Next

        SETemplateDoc.Close(False)
        SEApp.DoIdle()

        ' If a style by the same name exists in the target file, delete it.
        ViewStyleAlreadyPresent = False
        ViewStyles = CType(SEDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
        For Each ViewStyle In ViewStyles
            If ViewStyle.StyleName = TemplateActiveStyleName Then
                ViewStyleAlreadyPresent = True
            Else
                TempViewStyleName = ViewStyle.StyleName
            End If
        Next

        SEApp.DoIdle()

        Windows = SEDoc.Windows

        If ViewStyleAlreadyPresent Then ' Hopefully deactivate the desired ViewStyle so it can be removed
            For Each Window In Windows
                View = Window.View
                View.Style = TempViewStyleName
            Next
            ' ViewStyles can sometimes be flagged 'in use' even if they are not
            Try
                ViewStyles.Remove(TemplateActiveStyleName)
            Catch ex As Exception
                ExitStatus = 1
                ErrorMessageList.Add("Unable to update view style")
            End Try
        End If

        If ExitStatus = 0 Then
            ViewStyles.AddFromFile(TemplateFilename, TemplateActiveStyleName)

            For Each ViewStyle In ViewStyles
                If ViewStyle.StyleName = TemplateActiveStyleName Then
                    ViewStyle.SkyboxType = SolidEdgeFramework.SeSkyboxType.seSkyboxTypeSkybox
                    For i As Integer = 0 To 5
                        ViewStyle.SetSkyboxSideFilename(i, TemplateSkyboxName(i))
                    Next
                End If
            Next

            For Each Window In Windows
                View = Window.View
                View.Style = TemplateActiveStyleName
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


    Public Function FitIsometricView(
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
                                   AddressOf FitIsometricViewInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function FitIsometricViewInternal(
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

        SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.PartViewISOView, SolidEdgeFramework.SolidEdgeCommandConstants))
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

        Dim NewFilename As String = ""
        Dim NewExtension As String = ""
        Dim SheetmetalBaseFilename As String

        ' ComboBoxSaveAsSheetmetalFileType
        ' Format: Parasolid (*.xt), IGES (*.igs)
        NewExtension = Configuration("ComboBoxSaveAsSheetmetalFileType")
        NewExtension = Split(NewExtension, Delimiter:="*")(1)
        NewExtension = Split(NewExtension, Delimiter:=")")(0)

        SheetmetalBaseFilename = System.IO.Path.GetFileName(SEDoc.FullName)

        ' CheckBoxStepSheetmetalOutputDirectory
        If Configuration("CheckBoxSaveAsSheetmetalOutputDirectory") = "False" Then
            NewFilename = Configuration("TextBoxSaveAsSheetmetalOutputDirectory") + "\" + System.IO.Path.ChangeExtension(SheetmetalBaseFilename, NewExtension)
        Else
            NewFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, NewExtension)
        End If

        'Capturing a fault to update ExitStatus
        Try
            SEDoc.SaveAs(NewFilename)
            SEApp.DoIdle()
        Catch ex As Exception
            ExitStatus = 1
            ErrorMessageList.Add(String.Format("Error saving {0}", TruncateFullPath(NewFilename, Configuration)))
        End Try

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
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf InteractiveEditInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function InteractiveEditInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
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
            ExitStatus = 1
            ErrorMessageList.Add("File was not saved.")
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
                               Of SolidEdgePart.SheetMetalDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf RunExternalProgramInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function RunExternalProgramInternal(
        ByVal SEDoc As SolidEdgePart.SheetMetalDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim ExternalProgram As String = Configuration("TextBoxExternalProgramSheetmetal")
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
            ErrorMessageList.Add(String.Format("Drawing {0} not found", TruncateFullPath(DrawingFilename, Configuration)))
        End If


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
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

    Public Function PropertyFindReplace(
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
                                   AddressOf PropertyFindReplaceInternal,
                                   CType(SEDoc, SolidEdgePart.SheetMetalDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function PropertyFindReplaceInternal(
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

        Dim PropertyFound As Boolean = False
        Dim tf As Boolean
        Dim FindString As String = Configuration("TextBoxFindReplaceFindSheetmetal")
        Dim ReplaceString As String = Configuration("TextBoxFindReplaceReplaceSheetmetal")

        PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)

        For Each Properties In PropertySets
            For Each Prop In Properties
                tf = (Configuration("ComboBoxFindReplacePropertySetSheetmetal").ToLower = "custom")
                tf = tf And (Properties.Name.ToLower = "custom")
                If tf Then
                    If Prop.Name = Configuration("TextBoxFindReplacePropertyNameSheetmetal") Then
                        PropertyFound = True
                        ' Only works on text type properties
                        Try
                            Prop.Value = Replace(CType(Prop.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)
                            Properties.Save()
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add("Unable to replace property value.  This command only works on text type properties.")
                        End Try
                        Exit For
                    End If
                Else
                    If Prop.Name = Configuration("TextBoxFindReplacePropertyNameSheetmetal") Then
                        PropertyFound = True
                        ' Only works on text type properties
                        Try
                            Prop.Value = Replace(CType(Prop.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)
                            Properties.Save()
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add("Unable to replace property value.  This command only works on text type properties.")
                        End Try
                        Exit For
                    End If
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

        ErrorMessage(ExitStatus) = ErrorMessageList
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
