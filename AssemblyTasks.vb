Option Strict On

Imports SolidEdgeCommunity

Public Class AssemblyTasks
    Inherits IsolatedTaskProxy

    Public Function OccurrenceMissingFiles(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        ErrorMessageList = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               List(Of String))(
                                   AddressOf OccurrenceMissingFilesInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessageList

    End Function

    Private Function OccurrenceMissingFilesInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As String = "0"
        Dim ErrorMessage As String = ""

        For Each Occurrence In Occurrences
            If Occurrence.FileMissing() Then
                ExitStatus = "1"
                ErrorMessage += "  " + Occurrence.Name + Chr(13)
            End If
        Next

        ErrorMessageList.Add(ExitStatus)
        ErrorMessageList.Add(ErrorMessage)
        Return ErrorMessageList
    End Function


    Public Function OccurrenceOutsideProjectDirectory(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        ErrorMessageList = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               List(Of String))(
                                   AddressOf OccurrenceOutsideProjectDirectoryInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessageList

    End Function

    Private Function OccurrenceOutsideProjectDirectoryInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence
        Dim OccurrenceFilename As String
        Dim OccurrenceOutsideProjectError As Boolean = False

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As String = "0"
        Dim ErrorMessage As String = ""

        For Each Occurrence In Occurrences
            OccurrenceFilename = Occurrence.OccurrenceFileName
            If Not OccurrenceFilename.Contains(Configuration("TextBoxInputDirectory")) Then
                ExitStatus = "1"
                If Not ErrorMessage.Contains(OccurrenceFilename) Then
                    ErrorMessage += "  " + OccurrenceFilename + Chr(13)
                End If
            End If
        Next

        ErrorMessageList.Add(ExitStatus)
        ErrorMessageList.Add(ErrorMessage)
        Return ErrorMessageList
    End Function


    Public Function FailedRelationships(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        ErrorMessageList = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               List(Of String))(
                                   AddressOf FailedRelationshipsInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessageList

    End Function

    Private Function FailedRelationshipsInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As String = "0"
        Dim ErrorMessage As String = ""

        For Each Occurrence In Occurrences
            If Not (Occurrence.Adjustable Or Occurrence.IsAdjustablePart) Then
                If Occurrence.Status.ToString() = "seOccurrenceStatusOverDefined" Then
                    ExitStatus = "1"
                    ErrorMessage += "  " + Occurrence.Name + Chr(13)
                End If
                If Occurrence.Status.ToString() = "seOccurrenceStatusNotConsistent" Then
                    ExitStatus = "1"
                    ErrorMessage += "  " + Occurrence.Name + Chr(13)
                End If
            End If
        Next

        ErrorMessageList.Add(ExitStatus)
        ErrorMessageList.Add(ErrorMessage)
        Return ErrorMessageList
    End Function


    Public Function UnderconstrainedRelationships(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        ErrorMessageList = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               List(Of String))(
                                   AddressOf UnderconstrainedRelationshipsInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessageList

    End Function

    Private Function UnderconstrainedRelationshipsInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As String = "0"
        Dim ErrorMessage As String = ""

        For Each Occurrence In Occurrences
            If Not (Occurrence.Adjustable Or Occurrence.IsAdjustablePart) Then
                If Occurrence.Status.ToString() = "seOccurrenceStatusUnderDefined" Then
                    ExitStatus = "1"
                    ErrorMessage += "  " + Occurrence.Name + Chr(13)
                End If
            End If
        Next

        ErrorMessageList.Add(ExitStatus)
        ErrorMessageList.Add(ErrorMessage)
        Return ErrorMessageList
    End Function


    Public Function PartNumberDoesNotMatchFilename(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        ErrorMessageList = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               List(Of String))(
                                   AddressOf PartNumberDoesNotMatchFilenameInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessageList

    End Function

    Private Function PartNumberDoesNotMatchFilenameInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As String = "0"
        Dim ErrorMessage As String = ""

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
                ExitStatus = "1"
                ErrorMessage = "    Part number not assigned" + Chr(13)
            End If
            If Not Filename.Contains(PartNumber) Then
                ExitStatus = "1"
                ErrorMessage = "    Part number '" + PartNumber
                ErrorMessage += "' not found in filename '" + Filename + "'" + Chr(13)
            End If
        Else
            ExitStatus = "1"
            ErrorMessage = "    PartNumberPropertyName: '" + Configuration("TextBoxPartNumberPropertyName") + "'"
            ErrorMessage += " not found in PartNumberPropertySet: '" + Configuration("ComboBoxPartNumberPropertySet") + "'" + Chr(13)
            If Configuration("TextBoxPartNumberPropertyName") = "" Then
                ErrorMessage += "    Check the Configuration tab for valid entries" + Chr(13)
            End If
        End If

        ErrorMessageList.Add(ExitStatus)
        ErrorMessageList.Add(ErrorMessage)
        Return ErrorMessageList
    End Function


    Public Function ActivateAndUpdateAll(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        ErrorMessageList = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               List(Of String))(
                                   AddressOf ActivateAndUpdateAllInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessageList

    End Function

    Private Function ActivateAndUpdateAllInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As String = "0"
        Dim ErrorMessage As String = ""

        SEDoc.ActivateAll()
        SEDoc.UpdateAll()

        If SEDoc.ReadOnly Then
            ExitStatus = "1"
            ErrorMessage += "    Cannot save document marked 'Read Only'" + Chr(13)
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessageList.Add(ExitStatus)
        ErrorMessageList.Add(ErrorMessage)
        Return ErrorMessageList
    End Function


    Public Function UpdateFaceAndViewStylesFromTemplate(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        ErrorMessageList = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               List(Of String))(
                                   AddressOf UpdateFaceAndViewStylesFromTemplateInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessageList

    End Function

    Private Function UpdateFaceAndViewStylesFromTemplateInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As String = "0"
        Dim ErrorMessage As String = ""

        Dim SETemplateDoc As SolidEdgeAssembly.AssemblyDocument
        Dim Windows As SolidEdgeFramework.Windows
        Dim Window As SolidEdgeFramework.Window
        Dim View As SolidEdgeFramework.View
        Dim ViewStyles As SolidEdgeFramework.ViewStyles
        Dim ViewStyle As SolidEdgeFramework.ViewStyle

        Dim TemplateFilename As String = Configuration("TextBoxTemplateAssembly")
        Dim TemplateActiveStyleName As String = ""
        Dim TempViewStyleName As String = ""
        Dim ViewStyleAlreadyPresent As Boolean
        Dim TemplateSkyboxName(5) As String
        Dim msg As String = ""

        SEDoc.ImportStyles(TemplateFilename, True)

        ' Find the active ViewStyle in the template file.
        SETemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgeAssembly.AssemblyDocument)
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

        If SEDoc.ReadOnly Then
            ExitStatus = "1"
            ErrorMessage += "    Cannot save document marked 'Read Only'" + Chr(13)
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessageList.Add(ExitStatus)
        ErrorMessageList.Add(ErrorMessage)
        Return ErrorMessageList

    End Function


    Public Function FitIsometricView(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        ErrorMessageList = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               List(Of String))(
                                   AddressOf FitIsometricViewInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessageList

    End Function

    Private Function FitIsometricViewInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As List(Of String)

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim AsmRefPlanes As SolidEdgeAssembly.AsmRefPlanes = SEDoc.AsmRefPlanes
        Dim AsmRefPlane As SolidEdgeAssembly.AsmRefPlane
        'Dim Occurrence As SolidEdgeAssembly.Occurrence

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As String = "0"
        Dim ErrorMessage As String = ""

        If Occurrences.Count = 0 Then
            AsmRefPlanes.Visible = True
            For Each AsmRefPlane In AsmRefPlanes
                AsmRefPlane.Visible = False
                AsmRefPlane.Visible = True
            Next
        Else
            SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsShowAll, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsHideAllReferencePlanes, SolidEdgeFramework.SolidEdgeCommandConstants))
        End If

        SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewISOView, SolidEdgeFramework.SolidEdgeCommandConstants))
        SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewFit, SolidEdgeFramework.SolidEdgeCommandConstants))

        If SEDoc.ReadOnly Then
            ExitStatus = "1"
            ErrorMessage += "    Cannot save document marked 'Read Only'" + Chr(13)
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessageList.Add(ExitStatus)
        ErrorMessageList.Add(ErrorMessage)
        Return ErrorMessageList
    End Function


End Class
