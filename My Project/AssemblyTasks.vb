Option Strict On

Imports System.Windows.Shell
Imports SolidEdgeCommunity

Public Class AssemblyTasks
    Inherits IsolatedTaskProxy

    Public Function BrokenLinks(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf BrokenLinksInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function BrokenLinksInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence

        For Each Occurrence In Occurrences
            If Occurrence.FileMissing() Then
                ExitStatus = 1
                ErrorMessageList.Add(Occurrence.Name)
            End If
        Next

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function LinksOutsideInputDirectory(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf LinksOutsideInputDirectoryInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function LinksOutsideInputDirectoryInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence
        Dim OccurrenceFilename As String
        Dim OccurrenceOutsideProjectError As Boolean = False


        '(0)	{[ListViewFiles.Folder with subfolders.0, D\CAD\scripts\test_files\...\7481-50000_GRIPPER]}
        '(1)	{[ListViewFiles.Folder with subfolders.1, D:\CAD\scripts\test_files\...\7481-01000_purchased]}
        Dim InputDirectories As New List(Of String)
        Dim InputDirectory As String
        Dim Key As String
        Dim tf As Boolean

        Dim CheckedOcurrences As New List(Of String)

        For Each Key In Configuration.Keys
            tf = Key.ToLower.Contains("listviewfiles")
            tf = tf And Key.ToLower.Contains("folder")
            If tf Then
                InputDirectories.Add(Configuration(Key))
            End If
        Next

        For Each Occurrence In Occurrences
            OccurrenceFilename = Occurrence.OccurrenceFileName
            If Not CheckedOcurrences.Contains(OccurrenceFilename) Then
                CheckedOcurrences.Add(OccurrenceFilename)
                tf = False
                For Each InputDirectory In InputDirectories
                    If OccurrenceFilename.Contains(InputDirectory) Then
                        tf = True
                    End If
                Next
                'If Not OccurrenceFilename.Contains(Configuration("TextBoxInputDirectory")) Then
                If Not tf Then
                    ExitStatus = 1
                    If Not ErrorMessageList.Contains(OccurrenceFilename) Then
                        ErrorMessageList.Add(OccurrenceFilename)
                    End If
                End If
            End If
        Next

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function FailedRelationships(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf FailedRelationshipsInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function FailedRelationshipsInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence

        For Each Occurrence In Occurrences
            If Not (Occurrence.Adjustable Or Occurrence.IsAdjustablePart) Then
                If Occurrence.Status.ToString() = "seOccurrenceStatusOverDefined" Then
                    ExitStatus = 1
                    ErrorMessageList.Add(Occurrence.Name)
                End If
                If Occurrence.Status.ToString() = "seOccurrenceStatusNotConsistent" Then
                    ExitStatus = 1
                    ErrorMessageList.Add(Occurrence.Name)
                End If
            End If
        Next

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Public Function UnderconstrainedRelationships(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UnderconstrainedRelationshipsInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UnderconstrainedRelationshipsInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence

        For Each Occurrence In Occurrences
            If Not (Occurrence.Adjustable Or Occurrence.IsAdjustablePart) Then
                If Occurrence.Status.ToString() = "seOccurrenceStatusUnderDefined" Then
                    ExitStatus = 1
                    ErrorMessageList.Add(Occurrence.Name)
                End If
            End If
        Next

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
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf PartNumberDoesNotMatchFilenameInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function PartNumberDoesNotMatchFilenameInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim msg As String = ""

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        Dim PropertySets As SolidEdgeFramework.PropertySets = Nothing
        Dim Properties As SolidEdgeFramework.Properties = Nothing
        Dim Prop As SolidEdgeFramework.Property = Nothing

        Dim PartNumber As String = ""
        Dim PartNumberPropertyFound As Boolean = False
        Dim TF As Boolean
        Dim Filename As String = ""

        Filename = SEDoc.FullName

        If Filename.Contains("!") Then
            Filename = Filename.Split("!"c)(0)
        End If

        Filename = System.IO.Path.GetFileName(Filename)

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



    Public Function ActivateAndUpdateAll(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf ActivateAndUpdateAllInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Public Function ActivateAndUpdateAllInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        SEDoc.ActivateAll()
        SEDoc.UpdateAll()

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



    Public Function UpdateFaceAndViewStylesFromTemplate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf UpdateFaceAndViewStylesFromTemplateInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function UpdateFaceAndViewStylesFromTemplateInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        Dim TempErrorMessageList As New List(Of String)

        Dim SETemplateDoc As SolidEdgeAssembly.AssemblyDocument
        Dim TemplateFilename As String = Configuration("TextBoxTemplateAssembly")

        ' Import face styles from template
        SEDoc.ImportStyles(TemplateFilename, True)
        SEApp.DoIdle()

        SETemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgeAssembly.AssemblyDocument)
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
        ByRef SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByRef SETemplateDoc As SolidEdgeAssembly.AssemblyDocument
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
        ByRef SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByRef SETemplateDoc As SolidEdgeAssembly.AssemblyDocument
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        Dim FaceStyles As SolidEdgeFramework.FaceStyles
        Dim FaceStyle As SolidEdgeFramework.FaceStyle
        Dim tf As Boolean

        Dim TemplateConstructionBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim TemplateThreadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim TemplateWeldbeadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim TemplateCurveBaseStyle As SolidEdgeFramework.FaceStyle = Nothing

        Dim ConstructionBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim ThreadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim WeldbeadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim CurveBaseStyle As SolidEdgeFramework.FaceStyle = Nothing

        SETemplateDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyConstructionStyle,
                                   TemplateConstructionBaseStyle)
        SETemplateDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyThreadedCylindersStyle,
                                   TemplateThreadBaseStyle)
        SETemplateDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyWeldBeadStyle,
                                   TemplateWeldbeadBaseStyle)
        SETemplateDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyCurveStyle,
                                   TemplateCurveBaseStyle)
        'MsgBox(TemplateConstructionBaseStyle.StyleName)

        ' Update base styles in the document
        FaceStyles = CType(SEDoc.FaceStyles, SolidEdgeFramework.FaceStyles)


        For Each FaceStyle In FaceStyles
            If TemplateConstructionBaseStyle IsNot Nothing Then
                If FaceStyle.StyleName = TemplateConstructionBaseStyle.StyleName Then
                    SEDoc.SetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyConstructionStyle,
                                   FaceStyle)
                End If
            End If

            If TemplateThreadBaseStyle IsNot Nothing Then
                If FaceStyle.StyleName = TemplateThreadBaseStyle.StyleName Then
                    SEDoc.SetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyThreadedCylindersStyle,
                                       FaceStyle)
                End If
            End If

            If TemplateWeldbeadBaseStyle IsNot Nothing Then
                If FaceStyle.StyleName = TemplateWeldbeadBaseStyle.StyleName Then
                    SEDoc.SetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyWeldBeadStyle,
                                       FaceStyle)
                End If
            End If

            If TemplateCurveBaseStyle IsNot Nothing Then
                If FaceStyle.StyleName = TemplateCurveBaseStyle.StyleName Then
                    SEDoc.SetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyCurveStyle,
                                       FaceStyle)
                End If
            End If
        Next

        SEDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyConstructionStyle,
                           ConstructionBaseStyle)
        SEDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyThreadedCylindersStyle,
                       ThreadBaseStyle)
        SEDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyWeldBeadStyle,
                       WeldbeadBaseStyle)
        SEDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyCurveStyle,
                       CurveBaseStyle)

        tf = ConstructionBaseStyle Is Nothing
        tf = tf Or (ThreadBaseStyle Is Nothing)
        tf = tf Or (WeldbeadBaseStyle Is Nothing)
        tf = tf Or (CurveBaseStyle Is Nothing)

        If tf Then
            ErrorMessageList.Add("Some Color Manager base styles undefined.")
        End If

        Return ErrorMessageList
    End Function


    'Private Function GetAllOccurrences(
    '    ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument
    '    ) As List(Of SolidEdgeAssembly.Occurrence)

    '    Dim AllOccurrences As New List(Of SolidEdgeAssembly.Occurrence)

    '    Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
    '    Dim Occurrence As SolidEdgeAssembly.Occurrence

    '    For Each Occurrence In Occurrences
    '        AllOccurrences = RecurseOccurrence(Occurrence, AllOccurrences)
    '    Next

    '    Return AllOccurrences
    'End Function

    Private Function RecurseOccurrence(
        ByVal Occurrence As SolidEdgeAssembly.Occurrence,
        ByVal AllOccurrences As List(Of SolidEdgeAssembly.Occurrence)
        ) As List(Of SolidEdgeAssembly.Occurrence)

        Dim SubOccurrences As SolidEdgeAssembly.SubOccurrences
        Dim SubOccurrence As SolidEdgeAssembly.Occurrence

        Dim msg As String = Occurrence.Name


        If AllOccurrences.Contains(Occurrence) Then
            Return AllOccurrences
        Else
            AllOccurrences.Add(Occurrence)
            SubOccurrences = CType(DirectCast(Occurrence.SubOccurrences, SolidEdgeAssembly.Occurrences), SolidEdgeAssembly.SubOccurrences)
            '            If SubOccurrences.Count > 0 Then
            If SubOccurrences IsNot Nothing Then
                If SubOccurrences.Count > 0 Then
                    For Each SubOccurrence In SubOccurrences
                        AllOccurrences = RecurseOccurrence(SubOccurrence, AllOccurrences)
                    Next
                End If
            End If
        End If

        Return AllOccurrences

    End Function



    Public Function RemoveFaceStyleOverrides(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf RemoveFaceStyleOverridesInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function RemoveFaceStyleOverridesInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence

        Dim AllOccurrences As New List(Of SolidEdgeAssembly.Occurrence)
        Dim AllOccurrencesNames As New List(Of String)

        Dim IgnoreIncludeInReports As Boolean = Configuration("CheckBoxTLAIgnoreIncludeInReports").ToLower = "true"

        Dim OccurrenceGetter As New OccurrenceGetter(SEDoc, IgnoreIncludeInReports)

        For Each Occurrence In OccurrenceGetter.AllOccurrences

            ' Fails at 'Occurrence.FaceStyle IsNot Nothing' on some files.  Reason not known.
            Try
                If Occurrence.FaceStyle IsNot Nothing Then
                    Occurrence.PutStyleNone()
                End If
            Catch ex As Exception
                ' No FaceStyle should mean no override.  Error message not warranted.
            End Try
        Next

        For Each SubOccurrence In OccurrenceGetter.AllSubOccurrences

            ' Fails at 'Occurrence.FaceStyle IsNot Nothing' on some files.  Reason not known.
            Try
                If SubOccurrence.FaceStyle IsNot Nothing Then
                    SubOccurrence.PutStyleNone()
                End If
            Catch ex As Exception
                ' No FaceStyle should mean no override.  Error message not warranted.
            End Try
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



    Public Function OpenSave(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf OpenSaveInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function OpenSaveInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

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
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf HideConstructionsInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function HideConstructionsInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim AsmRefPlanes As SolidEdgeAssembly.AsmRefPlanes = SEDoc.AsmRefPlanes
        Dim AsmRefPlane As SolidEdgeAssembly.AsmRefPlane
        'Dim Occurrence As SolidEdgeAssembly.Occurrence

        Dim PMI As SolidEdgeFrameworkSupport.PMI

        Dim Sketches As SolidEdgeAssembly.ComponentLayouts
        Dim Sketch As SolidEdgeAssembly.Layout
        'Dim Profiles As SolidEdgePart.Profiles
        Dim Profile As SolidEdgePart.Profile

        Try
            Sketches = SEDoc.ComponentLayouts
            For Each Sketch In Sketches
                Profile = CType(Sketch.Profile, SolidEdgePart.Profile)
                Profile.Visible = False
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

        If Occurrences.Count = 0 Then
            AsmRefPlanes.Visible = True

            ' Some files containing nothing but sketches fail here
            Try
                For Each AsmRefPlane In AsmRefPlanes
                    AsmRefPlane.Visible = False
                    AsmRefPlane.Visible = True
                Next
            Catch ex As Exception
                ExitStatus = 1
                ErrorMessageList.Add("Problem processing reference planes.  Please verify results.")
            End Try
        Else
            SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsShowAll, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsHideAllReferencePlanes, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.StartCommand(CType(40080, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Sketches
            SEApp.StartCommand(CType(40081, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Reference Axes
            SEApp.StartCommand(CType(40082, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.StartCommand(CType(40083, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.StartCommand(CType(40084, SolidEdgeFramework.SolidEdgeCommandConstants))
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
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf FitPictorialViewInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function FitPictorialViewInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        'Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        'Dim AsmRefPlanes As SolidEdgeAssembly.AsmRefPlanes = SEDoc.AsmRefPlanes
        'Dim AsmRefPlane As SolidEdgeAssembly.AsmRefPlane
        'Dim Occurrence As SolidEdgeAssembly.Occurrence

        'If Occurrences.Count = 0 Then
        '    AsmRefPlanes.Visible = True
        '    ' Some files with nothing but sketches fail here
        '    Try
        '        For Each AsmRefPlane In AsmRefPlanes
        '            AsmRefPlane.Visible = False
        '            AsmRefPlane.Visible = True
        '        Next
        '    Catch ex As Exception
        '        ExitStatus = 1
        '        ErrorMessageList.Add("Problem processing reference planes.  Please verify results.")
        '    End Try
        '    'Else
        '    '    SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsShowAll, SolidEdgeFramework.SolidEdgeCommandConstants))
        '    '    SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsHideAllReferencePlanes, SolidEdgeFramework.SolidEdgeCommandConstants))
        '    '    SEApp.StartCommand(CType(40080, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Sketches
        '    '    SEApp.StartCommand(CType(40081, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Reference Axes
        '    '    SEApp.StartCommand(CType(40082, SolidEdgeFramework.SolidEdgeCommandConstants))
        '    '    SEApp.StartCommand(CType(40083, SolidEdgeFramework.SolidEdgeCommandConstants))
        '    '    SEApp.StartCommand(CType(40084, SolidEdgeFramework.SolidEdgeCommandConstants))
        'End If

        If Configuration("RadioButtonPictorialViewIsometric").ToLower = "true" Then
            SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewISOView, SolidEdgeFramework.SolidEdgeCommandConstants))
        End If
        If Configuration("RadioButtonPictorialViewDimetric").ToLower = "true" Then
            SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewDimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))
        End If
        If Configuration("RadioButtonPictorialViewTrimetric").ToLower = "true" Then
            SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewTrimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))
        End If

        SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewFit, SolidEdgeFramework.SolidEdgeCommandConstants))

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
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf SaveAsInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function SaveAsInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        Dim SupplementalExitStatus As Integer = 0
        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim NewFilename As String = ""
        Dim NewExtension As String = ""
        Dim NewFileFormat As String = ""
        Dim Filename As String
        Dim BaseFilename As String
        Dim Dir As String

        Dim BaseDir As String
        Dim SubDir As String
        Dim Formula As String
        Dim Proceed As Boolean = True

        Dim Members As SolidEdgeAssembly.AssemblyFamilyMembers
        Dim Member As SolidEdgeAssembly.AssemblyFamilyMember
        Dim msg As String = ""

        Dim ImageExtensions As New List(Of String)

        Dim ExitMessage As String = ""

        ImageExtensions.Add(".bmp")
        ImageExtensions.Add(".jpg")
        ImageExtensions.Add(".png")
        ImageExtensions.Add(".tif")

        'Configuration("ComboBoxSaveAsAssemblyFileType") format examples
        'IGES (*.igs)
        'Parasolid text (*.x_b)
        'Copy (*.asm)
        NewExtension = Configuration("ComboBoxSaveAsAssemblyFileType")
        NewExtension = NewExtension.Split("*"c)(1)  ' "Parasolid (*.xt)" -> ".xt)"
        NewExtension = NewExtension.Split(")"c)(0)  ' "Parasolid (*.xt)" -> ".xt)"

        NewFileFormat = Configuration("ComboBoxSaveAsAssemblyFileType")
        NewFileFormat = NewFileFormat.Split("("c)(0)  ' "Parasolid (*.xt)" -> "Parasolid "

        Filename = SEDoc.FullName

        If Not SEDoc.IsFileFamilyByDocument Then
            BaseFilename = System.IO.Path.GetFileName(SEDoc.FullName)
            If Configuration("CheckBoxSaveAsAssemblyOutputDirectory") = "False" Then
                BaseDir = Configuration("TextBoxSaveAsAssemblyOutputDirectory")

                If Configuration("CheckBoxSaveAsFormulaAssembly").ToLower = "true" Then
                    Formula = Configuration("TextBoxSaveAsFormulaAssembly")

                    SupplementalErrorMessage = ParseSubdirectoryFormula(SEDoc, Configuration, Formula)

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
                    NewFilename = BaseDir + "\" + System.IO.Path.ChangeExtension(BaseFilename, NewExtension)
                End If

            Else
                NewFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, NewExtension)
            End If

            If Proceed Then
                Try
                    If Not ImageExtensions.Contains(NewExtension) Then  ' Saving as a model, not an image.
                        If Not Configuration("ComboBoxSaveAsAssemblyFileType").ToLower.Contains("copy") Then
                            If NewExtension = ".jt" Then
                                SEDoc.SaveAs(NewFilename)
                            Else
                                SEDoc.SaveAs(NewFilename)
                            End If
                            SEApp.DoIdle()
                        Else
                            If Configuration("CheckBoxSaveAsAssemblyOutputDirectory").ToLower = "false" Then
                                SEDoc.SaveCopyAs(NewFilename)
                                SEApp.DoIdle()
                            Else
                                ExitStatus = 1
                                ErrorMessageList.Add("Can not SaveCopyAs to the original directory")
                                Proceed = False
                            End If
                        End If

                    Else  ' Saving as image
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

        Else
            ' D:\projects\foa1.asm!Master -> D:\foa1.asm
            Filename = Filename.Split("!"c)(0)
            ' BaseFilename = System.IO.Path.GetFileNameWithoutExtension(Filename)
            BaseFilename = System.IO.Path.GetFileName(Filename)

            Members = SEDoc.AssemblyFamilyMembers
            For Each Member In Members
                Members.ActivateMember(Member.MemberName)

                If Configuration("CheckBoxSaveAsAssemblyOutputDirectory") = "False" Then
                    'Dir = Configuration("TextBoxSaveAsAssemblyOutputDirectory")
                    '' D:\projects\foa1.asm -> foa1
                    'BaseFilename = System.IO.Path.GetFileNameWithoutExtension(Filename)
                    '' foa1 -> foa1-Member1
                    'BaseFilename = String.Format("{0}-{1}", BaseFilename, Member.MemberName)
                    'NewFilename = Dir + "\" + System.IO.Path.ChangeExtension(BaseFilename, NewExtension)
                    BaseDir = Configuration("TextBoxSaveAsAssemblyOutputDirectory")

                    If Configuration("CheckBoxSaveAsFormulaAssembly").ToLower = "true" Then
                        Formula = Configuration("TextBoxSaveAsFormulaAssembly")

                        SupplementalErrorMessage = ParseSubdirectoryFormula(SEDoc, Configuration, Formula)

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
                        ' NewFilename = BaseDir + "\" + System.IO.Path.ChangeExtension(Filename, NewExtension)
                        NewFilename = System.IO.Path.GetFileNameWithoutExtension(Filename)
                        NewFilename = String.Format("{0}\{1}-{2}", BaseDir, NewFilename, Member.MemberName)
                        NewFilename = System.IO.Path.ChangeExtension(NewFilename, NewExtension)
                    End If

                Else
                    Dir = System.IO.Path.GetDirectoryName(Filename)
                    BaseFilename = System.IO.Path.GetFileNameWithoutExtension(Filename)
                    ' foa1 -> foa1-Member1
                    BaseFilename = String.Format("{0}-{1}", BaseFilename, Member.MemberName)
                    NewFilename = Dir + "\" + System.IO.Path.ChangeExtension(BaseFilename, NewExtension)
                End If

                If Proceed Then
                    Try
                        If Not ImageExtensions.Contains(NewExtension) Then
                            If Not Configuration("ComboBoxSaveAsAssemblyFileType").ToLower.Contains("copy") Then
                                If NewExtension = ".jt" Then
                                    SEDoc.SaveAsJT(NewFilename)
                                Else
                                    SEDoc.SaveAs(NewFilename)
                                End If
                                SEApp.DoIdle()
                            Else
                                If Configuration("CheckBoxSaveAsAssemblyOutputDirectory").ToLower = "false" Then
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

            Next
        End If


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function



    Private Function ParseSubdirectoryFormula(SEDoc As SolidEdgeAssembly.AssemblyDocument,
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

    Private Function GetPropertyValue(SEDoc As SolidEdgeAssembly.AssemblyDocument,
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

            'Dim ModelLink As SolidEdgeDraft.ModelLink
            'Dim Typename As String

            'Try
            '    ModelLink = SEDoc.ModelLinks.Item(ModelLinkIdx)
            '    Typename = Microsoft.VisualBasic.Information.TypeName(ModelLink.ModelDocument)  ' "PartDocument", "SheetmetalDocument", "AssemblyDocument"

            '    If Typename.ToLower = "partdocument" Then
            '        Dim ModelDoc As SolidEdgePart.PartDocument = CType(ModelLink.ModelDocument, SolidEdgePart.PartDocument)
            '        PropertySets = CType(ModelDoc.Properties, SolidEdgeFramework.PropertySets)
            '        ModelDocName = ModelDoc.FullName
            '    End If

            '    If Typename.ToLower = "sheetmetaldocument" Then
            '        Dim ModelDoc As SolidEdgePart.SheetMetalDocument = CType(ModelLink.ModelDocument, SolidEdgePart.SheetMetalDocument)
            '        PropertySets = CType(ModelDoc.Properties, SolidEdgeFramework.PropertySets)
            '        ModelDocName = ModelDoc.FullName
            '    End If

            '    If Typename.ToLower = "assemblydocument" Then
            '        Dim ModelDoc As SolidEdgeAssembly.AssemblyDocument = CType(ModelLink.ModelDocument, SolidEdgeAssembly.AssemblyDocument)
            '        PropertySets = CType(ModelDoc.Properties, SolidEdgeFramework.PropertySets)
            '        ModelDocName = ModelDoc.FullName
            '    End If
            'Catch ex As Exception
            '    Proceed = False
            '    ExitStatus = 1
            '    msg = String.Format("Problem accessing index reference {0}", ModelLinkIdx)
            '    If Not ErrorMessageList.Contains(msg) Then
            '        ErrorMessageList.Add(msg)
            '    End If
            'End Try

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
                    msg = String.Format("Property '{0}' not found in {1}", PropertyName, CommonTasks.TruncateFullPath(SEDoc.FullName, Configuration))
                    ErrorMessageList.Add(msg)
                Else
                    msg = String.Format("Property '{0}' not found in {1}", PropertyName, CommonTasks.TruncateFullPath(ModelDocName, Configuration))
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

        Dim ExternalProgram As String = Configuration("TextBoxExternalProgramAssembly")

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
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf MissingDrawingInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function MissingDrawingInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        Dim ModelFilename As String
        Dim DrawingFilename As String

        ModelFilename = SEDoc.FullName

        If ModelFilename.Contains("!") Then
            ModelFilename = ModelFilename.Split("!"c)(0)
        End If

        DrawingFilename = System.IO.Path.ChangeExtension(ModelFilename, ".dft")

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
        Dim AutoAddMissingProperty As Boolean = Configuration("CheckBoxAutoAddMissingProperty").ToLower = "true"

        'ErrorMessage = InvokeSTAThread(
        '                       Of SolidEdgeAssembly.AssemblyDocument,
        '                       Dictionary(Of String, String),
        '                       SolidEdgeFramework.Application,
        '                       Dictionary(Of Integer, List(Of String)))(
        '                           AddressOf PropertyFindReplaceInternal,
        '                           CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
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



    'Private Function StringToDict(s As String, delimiter1 As Char, delimiter2 As Char) As Dictionary(Of String, String)
    '    ' Takes a double-delimited string and returns a dictionary
    '    ' delimiter1 separates entries in the dictionary
    '    ' delimiter2 separates the Key from the Value in each entry.

    '    ' Example string: "weight: Weight of Object, length:, width"
    '    ' Returns a dictionary like:

    '    ' {"weight": "Weight of Object",
    '    '  "length": "length",
    '    '  "width": "width"}

    '    ' Notes
    '    ' Whitespace before and after each Key and Value is removed.
    '    ' To convert a single string, say ",", to a char, do ","c
    '    ' If delimiter2 is not present in an entry, or there is nothing after delimiter2, the Key and Value are the same.

    '    Dim D As New Dictionary(Of String, String)
    '    Dim A() As String
    '    Dim K As String
    '    Dim V As String

    '    A = s.Split(delimiter1)

    '    For i As Integer = 0 To A.Length - 1
    '        If A(i).Contains(delimiter2) Then
    '            K = A(i).Split(delimiter2)(0).Trim
    '            V = A(i).Split(delimiter2)(1).Trim

    '            If V = "" Then
    '                V = K
    '            End If
    '        Else
    '            K = A(i).Trim
    '            V = K
    '        End If

    '        D.Add(K, V)

    '    Next

    '    Return D

    'End Function


    Public Function Dummy(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf DummyInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function DummyInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
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
