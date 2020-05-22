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

        Models = SEDoc.Models

        If (Models.Count > 0) And (Models.Count < 10) Then
            For Each Model In Models
                Features = Model.Features
                For Each Feature In Features
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
                Next
            Next
        ElseIf Models.Count >= 10 Then
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

        Models = SEDoc.Models

        If (Models.Count > 0) And (Models.Count < 10) Then
            For Each Model In Models
                Features = Model.Features
                For Each Feature In Features
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
                Next
            Next
        ElseIf Models.Count >= 10 Then
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

        If (Models.Count > 0) And (Models.Count < 10) Then
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
        ElseIf Models.Count >= 10 Then
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

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim MatTable As SolidEdgeFramework.MatTable

        Dim MaterialLibList As Object = Nothing
        Dim NumMaterialLibraries As Integer
        Dim MaterialList As Object = Nothing
        Dim NumMaterials As Integer

        Dim ActiveMaterialLibrary As String = System.IO.Path.GetFileNameWithoutExtension(Configuration("TextBoxActiveMaterialLibrary"))
        Dim ActiveMaterialLibraryPresent As Boolean = False
        Dim CurrentMaterialName As String = ""
        Dim MatTableMaterial As Object
        Dim CurrentMaterialNameInLibrary As Boolean = False
        Dim CurrentMaterialMatchesLibMaterial As Boolean = True

        Dim msg As String = ""

        Dim Models As SolidEdgePart.Models

        Models = SEDoc.Models

        If Models.Count > 0 Then

            MatTable = SEApp.GetMaterialTable()
            MatTable.GetCurrentMaterialName(SEDoc, CurrentMaterialName)
            MatTable.GetMaterialLibraryList(MaterialLibList, NumMaterialLibraries)

            'Make sure the ActiveMaterialLibrary in settings.txt is present
            For Each MatTableMaterial In CType(MaterialLibList, System.Array)
                If MatTableMaterial.ToString = ActiveMaterialLibrary Then
                    ActiveMaterialLibraryPresent = True
                    Exit For
                End If
            Next

            If Not ActiveMaterialLibraryPresent Then
                msg = "ActiveMaterialLibrary " + Configuration("TextBoxActiveMaterialLibrary") + " not found.  Exiting..." + Chr(13)
                msg += "Please update the Material Table on the Configuration tab." + Chr(13)
                MsgBox(msg)
                SEApp.Quit()
                End
            End If

            'See if the CurrentMaterialName is in the ActiveLibrary
            MatTable.GetMaterialListFromLibrary(ActiveMaterialLibrary, NumMaterials, MaterialList)
            For Each MatTableMaterial In CType(MaterialList, System.Array)
                If MatTableMaterial.ToString.ToLower.Trim = CurrentMaterialName.ToLower.Trim Then
                    CurrentMaterialNameInLibrary = True
                    Exit For
                End If
            Next

            If Not CurrentMaterialNameInLibrary Then
                ExitStatus = 1
                If CurrentMaterialName = "" Then
                    ErrorMessageList.Add(String.Format("Material 'None' not in {0}", ActiveMaterialLibrary))
                Else
                    ErrorMessageList.Add(String.Format("Material '{0}' not in {1}", CurrentMaterialName, ActiveMaterialLibrary))
                End If
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
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
            ErrorMessageList.Add(String.Format("PartNumberPropertyName: '{0}' not found in PartNumberPropertySet: '{1}'",
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

        If Not FileIO.FileSystem.DirectoryExists(Configuration("TextBoxLaserOutputDirectory")) Then
            msg = "Laser output directory not found: '" + Configuration("TextBoxLaserOutputDirectory") + "'.  "
            msg += "Please select a valid directory on the Sheetmetal Tab." + Chr(13)
            msg += "Exiting..."
            MsgBox(msg)
            SEApp.Quit()
            End
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

        DXFFilename = Configuration("TextBoxLaserOutputDirectory") + "\" + System.IO.Path.ChangeExtension(SheetmetalBaseFilename, ".dxf")
        PDFFilename = Configuration("TextBoxLaserOutputDirectory") + "\" + System.IO.Path.ChangeExtension(SheetmetalBaseFilename, ".pdf")

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

        'ErrorMessageList.Clear()

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

        If (Models.Count > 0) And (Models.Count < 10) Then
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
        ElseIf Models.Count >= 10 Then
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

        Dim MatTable As SolidEdgeFramework.MatTable

        Dim MaterialLibList As Object = Nothing
        Dim NumMaterialLibraries As Integer
        Dim MaterialList As Object = Nothing
        Dim NumMaterials As Integer

        Dim MatTableProps As Array
        Dim MatTableProp As SolidEdgeFramework.MatTablePropIndex
        Dim DocPropValue As Object = Nothing
        Dim LibPropValue As Object = Nothing

        Dim ActiveMaterialLibrary As String = System.IO.Path.GetFileNameWithoutExtension(Configuration("TextBoxActiveMaterialLibrary"))
        Dim ActiveMaterialLibraryPresent As Boolean = False
        Dim CurrentMaterialName As String = ""
        Dim MatTableMaterial As Object
        Dim CurrentMaterialNameInLibrary As Boolean = False
        Dim CurrentMaterialMatchesLibMaterial As Boolean = True

        Dim msg As String = ""

        Dim Models As SolidEdgePart.Models

        Models = SEDoc.Models
        Dim Model As SolidEdgePart.Model
        Dim Body As SolidEdgeGeometry.Body


        If Models.Count > 0 Then

            MatTable = SEApp.GetMaterialTable()
            MatTable.GetCurrentMaterialName(SEDoc, CurrentMaterialName)
            MatTable.GetMaterialLibraryList(MaterialLibList, NumMaterialLibraries)
            MatTableProps = System.Enum.GetValues(GetType(SolidEdgeConstants.MatTablePropIndex))

            'Make sure the ActiveMaterialLibrary in settings.txt is present
            For Each MatTableMaterial In CType(MaterialLibList, System.Array)
                If MatTableMaterial.ToString = ActiveMaterialLibrary Then
                    ActiveMaterialLibraryPresent = True
                    Exit For
                End If
            Next

            If Not ActiveMaterialLibraryPresent Then
                msg = "ActiveMaterialLibrary " + Configuration("TextBoxActiveMaterialLibrary") + " not found.  Exiting..." + Chr(13)
                msg += "Please update the Material Table on the Configuration tab." + Chr(13)
                MsgBox(msg)
                SEApp.Quit()
                End
            End If

            'See if the CurrentMaterialName is in the ActiveLibrary
            MatTable.GetMaterialListFromLibrary(ActiveMaterialLibrary, NumMaterials, MaterialList)
            For Each MatTableMaterial In CType(MaterialList, System.Array)
                If MatTableMaterial.ToString.ToLower.Trim = CurrentMaterialName.ToLower.Trim Then
                    CurrentMaterialNameInLibrary = True

                    'The names match.  Check if their properties do, too.
                    MatTableProps = System.Enum.GetValues(GetType(SolidEdgeConstants.MatTablePropIndex))
                    For Each MatTableProp In MatTableProps
                        MatTable.GetMaterialPropValueFromLibrary(MatTableMaterial.ToString, ActiveMaterialLibrary, MatTableProp, LibPropValue)
                        MatTable.GetMaterialPropValueFromDoc(SEDoc, MatTableProp, DocPropValue)

                        ' MatTableProps are either Double or String.
                        If (DocPropValue.GetType = GetType(Double)) And (LibPropValue.GetType = GetType(Double)) Then
                            ' Double types may have insignificant differences.
                            Dim DPV As Double = CType(DocPropValue, Double)
                            Dim LPV As Double = CType(LibPropValue, Double)

                            If Not ((DPV = 0) And (LPV = 0)) Then  ' Avoid divide by 0.  Anyway, they match.
                                Dim NormalizedDifference As Double = (DPV - LPV) / (DPV + LPV) / 2
                                If Not Math.Abs(NormalizedDifference) < 0.001 Then
                                    msg += CStr(NormalizedDifference) + Chr(13)
                                    CurrentMaterialMatchesLibMaterial = False
                                    Exit For
                                End If
                            End If
                        Else
                            If CType(DocPropValue, String) <> CType(LibPropValue, String) Then
                                Dim a As String = CType(DocPropValue, String)
                                Dim b As String = CType(LibPropValue, String)
                                msg += a + " " + b + Chr(13)
                                CurrentMaterialMatchesLibMaterial = False
                                Exit For
                            End If
                        End If
                        DocPropValue = Nothing
                        LibPropValue = Nothing
                    Next

                    If Not CurrentMaterialMatchesLibMaterial Then
                        MatTable.ApplyMaterialToDoc(SEDoc, MatTableMaterial.ToString, ActiveMaterialLibrary)
                        ' Changing material does not automatically update face styles
                        For Each Model In Models
                            Body = CType(Model.Body, SolidEdgeGeometry.Body)
                            Body.Style = Nothing
                        Next
                        If SEDoc.ReadOnly Then
                            ExitStatus = 1
                            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
                        Else
                            SEDoc.Save()
                            SEApp.DoIdle()
                            ExitStatus = 1
                            ErrorMessageList.Add("Material was updated")
                        End If

                        Exit For
                    End If
                End If
                If CurrentMaterialNameInLibrary Then
                    Exit For
                End If
            Next

            If Not CurrentMaterialNameInLibrary Then
                ExitStatus = 1
                If CurrentMaterialName = "" Then
                    ErrorMessageList.Add(String.Format("Material 'None' not in {0}", ActiveMaterialLibrary))
                Else
                    ErrorMessageList.Add(String.Format("Material '{0}' not in {1}", CurrentMaterialName, ActiveMaterialLibrary))
                End If
            End If
        End If


        ErrorMessage(ExitStatus) = ErrorMessageList
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
            ViewStyles.Remove(TemplateActiveStyleName)
        End If

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

        SEDoc.Save()
        SEApp.DoIdle()

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

        SEDoc.Save()
        SEApp.DoIdle()

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

End Class
