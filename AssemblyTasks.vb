Option Strict On

Imports SolidEdgeCommunity

Public Class AssemblyTasks
    Inherits IsolatedTaskProxy

    Public Function OccurrenceMissingFiles(
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
                                   AddressOf OccurrenceMissingFilesInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function OccurrenceMissingFilesInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

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


    Public Function OccurrenceOutsideProjectDirectory(
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
                                   AddressOf OccurrenceOutsideProjectDirectoryInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function OccurrenceOutsideProjectDirectoryInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence
        Dim OccurrenceFilename As String
        Dim OccurrenceOutsideProjectError As Boolean = False

        For Each Occurrence In Occurrences
            OccurrenceFilename = Occurrence.OccurrenceFileName
            If Not OccurrenceFilename.Contains(Configuration("TextBoxInputDirectory")) Then
                ExitStatus = 1
                If Not ErrorMessageList.Contains(OccurrenceFilename) Then
                    ErrorMessageList.Add(OccurrenceFilename)
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

    Private Function ActivateAndUpdateAllInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

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

    'Private Function SetActiveViewStyle(
    '    ByRef SEDoc As SolidEdgeAssembly.AssemblyDocument,
    '    ByRef SETemplateDoc As SolidEdgeAssembly.AssemblyDocument
    '    ) As List(Of String)

    '    Dim ErrorMessageList As New List(Of String)

    '    Dim Windows As SolidEdgeFramework.Windows
    '    Dim Window As SolidEdgeFramework.Window
    '    Dim View As SolidEdgeFramework.View
    '    'Dim ViewStyles As SolidEdgeFramework.ViewStyles
    '    'Dim ViewStyle As SolidEdgeFramework.ViewStyle
    '    Dim ViewStyleName As String

    '    Windows = SETemplateDoc.Windows
    '    Window = CType(Windows.Item(1), SolidEdgeFramework.Window)
    '    View = Window.View
    '    ViewStyleName = View.Style

    '    'ViewStyles = CType(SEDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
    '    'ViewStyle = ViewStyles.Item(ViewStyleName)

    '    Windows = SEDoc.Windows
    '    Window = CType(Windows.Item(1), SolidEdgeFramework.Window)
    '    View = Window.View
    '    View.Style = ViewStyleName

    '    Return ErrorMessageList
    'End Function


    'Private Function UpdateFaceAndViewStylesFromTemplateInternal_OLD(
    '    ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
    '    ByVal Configuration As Dictionary(Of String, String),
    '    ByVal SEApp As SolidEdgeFramework.Application
    '    ) As Dictionary(Of Integer, List(Of String))

    '    Dim ErrorMessageList As New List(Of String)
    '    Dim ExitStatus As Integer = 0
    '    Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

    '    'Dim TempErrorMessageList As New List(Of String)

    '    Dim SETemplateDoc As SolidEdgeAssembly.AssemblyDocument
    '    Dim Windows As SolidEdgeFramework.Windows
    '    Dim Window As SolidEdgeFramework.Window
    '    Dim View As SolidEdgeFramework.View
    '    Dim ViewStyles As SolidEdgeFramework.ViewStyles
    '    Dim ViewStyle As SolidEdgeFramework.ViewStyle
    '    Dim FaceStyles As SolidEdgeFramework.FaceStyles
    '    Dim FaceStyle As SolidEdgeFramework.FaceStyle

    '    Dim TemplateFilename As String = Configuration("TextBoxTemplateAssembly")
    '    Dim TemplateActiveStyleName As String = ""
    '    Dim TempViewStyleName As String = ""
    '    Dim ViewStyleAlreadyPresent As Boolean
    '    Dim TemplateSkyboxName(5) As String
    '    Dim msg As String = ""
    '    Dim tf As Boolean = False

    '    Dim TemplateConstructionBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
    '    Dim TemplateThreadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
    '    Dim TemplateWeldbeadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
    '    Dim TemplateCurveBaseStyle As SolidEdgeFramework.FaceStyle = Nothing

    '    Dim ConstructionBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
    '    Dim ThreadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
    '    Dim WeldbeadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
    '    Dim CurveBaseStyle As SolidEdgeFramework.FaceStyle = Nothing

    '    ' Import face styles from template
    '    SEDoc.ImportStyles(TemplateFilename, True)

    '    SETemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgeAssembly.AssemblyDocument)
    '    SEApp.DoIdle()


    '    ' Get the template base styles
    '    SETemplateDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyConstructionStyle,
    '                               TemplateConstructionBaseStyle)
    '    SETemplateDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyThreadedCylindersStyle,
    '                               TemplateThreadBaseStyle)
    '    SETemplateDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyWeldBeadStyle,
    '                               TemplateWeldbeadBaseStyle)
    '    SETemplateDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyCurveStyle,
    '                               TemplateCurveBaseStyle)
    '    'MsgBox(TemplateConstructionBaseStyle.StyleName)

    '    ' Update base styles in the document
    '    FaceStyles = CType(SEDoc.FaceStyles, SolidEdgeFramework.FaceStyles)

    '    For Each FaceStyle In FaceStyles
    '        If TemplateConstructionBaseStyle IsNot Nothing Then
    '            If FaceStyle.StyleName = TemplateConstructionBaseStyle.StyleName Then
    '                SEDoc.SetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyConstructionStyle,
    '                               FaceStyle)
    '            End If
    '        End If

    '        If TemplateThreadBaseStyle IsNot Nothing Then
    '            If FaceStyle.StyleName = TemplateThreadBaseStyle.StyleName Then
    '                SEDoc.SetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyThreadedCylindersStyle,
    '                                   FaceStyle)
    '            End If
    '        End If

    '        If TemplateWeldbeadBaseStyle IsNot Nothing Then
    '            If FaceStyle.StyleName = TemplateWeldbeadBaseStyle.StyleName Then
    '                SEDoc.SetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyWeldBeadStyle,
    '                                   FaceStyle)
    '            End If
    '        End If

    '        If TemplateCurveBaseStyle IsNot Nothing Then
    '            If FaceStyle.StyleName = TemplateCurveBaseStyle.StyleName Then
    '                SEDoc.SetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyCurveStyle,
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

    '        SEDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyConstructionStyle,
    '                       ConstructionBaseStyle)
    '        SEDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyThreadedCylindersStyle,
    '                   ThreadBaseStyle)
    '        SEDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyWeldBeadStyle,
    '                   WeldbeadBaseStyle)
    '        SEDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyCurveStyle,
    '                   CurveBaseStyle)

    '        tf = ConstructionBaseStyle Is Nothing
    '        tf = tf Or (ThreadBaseStyle Is Nothing)
    '        tf = tf Or (WeldbeadBaseStyle Is Nothing)
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


    '    'AllowOverrides
    '    'AmbientBlue
    '    ''AmbientColor
    '    'AmbientGreen
    '    ''AmbientIntensity
    '    'AmbientRed
    '    'AmbientShadows
    '    'AntialiasLevel
    '    ''AntialiasSurface
    '    ''AntialiasWireframe
    '    'BackgroundImageFile
    '    'BackgroundMirrorX
    '    'BackgroundMirrorY
    '    'BackgroundType
    '    'Bumpmaps
    '    'CastShadows
    '    'DepthFading
    '    'DimPercentage
    '    'DropShadow
    '    'FloorReflection
    '    'FocalLength
    '    'HiddenLineMode
    '    'HiddenLines
    '    'HighQuality
    '    ''IsBackgroundImageDisplayed
    '    ''NumLights
    '    ''Parent
    '    'Perspective
    '    'Reflections
    '    'RenderMode
    '    'RenderModeType
    '    'Silhouettes
    '    'SilhouettesEnabled
    '    'SkyboxType
    '    ''StyleID
    '    ''StyleName
    '    'Textures





    '    ErrorMessage(ExitStatus) = ErrorMessageList
    '    Return ErrorMessage
    'End Function

    'Private Function UpdateViewStyleProperties(
    '    ByRef TemplateViewStyle As SolidEdgeFramework.ViewStyle,
    '    ByRef DocViewStyle As SolidEdgeFramework.ViewStyle
    '    ) As List(Of String)

    '    Dim ErrorMessageList As New List(Of String)

    '    DocViewStyle.AllowOverrides = TemplateViewStyle.AllowOverrides
    '    DocViewStyle.AmbientBlue = TemplateViewStyle.AmbientBlue
    '    DocViewStyle.AmbientGreen = TemplateViewStyle.AmbientGreen
    '    DocViewStyle.AmbientRed = TemplateViewStyle.AmbientRed
    '    DocViewStyle.AmbientShadows = TemplateViewStyle.AmbientShadows
    '    DocViewStyle.AntialiasLevel = TemplateViewStyle.AntialiasLevel
    '    DocViewStyle.BackgroundImageFile = TemplateViewStyle.BackgroundImageFile
    '    DocViewStyle.BackgroundMirrorX = TemplateViewStyle.BackgroundMirrorX
    '    DocViewStyle.BackgroundMirrorY = TemplateViewStyle.BackgroundMirrorY
    '    DocViewStyle.BackgroundType = TemplateViewStyle.BackgroundType
    '    DocViewStyle.Bumpmaps = TemplateViewStyle.Bumpmaps
    '    DocViewStyle.CastShadows = TemplateViewStyle.CastShadows
    '    DocViewStyle.DepthFading = TemplateViewStyle.DepthFading
    '    DocViewStyle.DimPercentage = TemplateViewStyle.DimPercentage
    '    DocViewStyle.DropShadow = TemplateViewStyle.DropShadow
    '    DocViewStyle.FloorReflection = TemplateViewStyle.FloorReflection
    '    DocViewStyle.FocalLength = TemplateViewStyle.FocalLength
    '    DocViewStyle.HiddenLineMode = TemplateViewStyle.HiddenLineMode
    '    DocViewStyle.HiddenLines = TemplateViewStyle.HiddenLines
    '    DocViewStyle.HighQuality = TemplateViewStyle.HighQuality
    '    DocViewStyle.Perspective = TemplateViewStyle.Perspective
    '    DocViewStyle.Reflections = TemplateViewStyle.Reflections
    '    DocViewStyle.RenderMode = TemplateViewStyle.RenderMode
    '    DocViewStyle.RenderModeType = TemplateViewStyle.RenderModeType
    '    DocViewStyle.Silhouettes = TemplateViewStyle.Silhouettes
    '    DocViewStyle.SilhouettesEnabled = TemplateViewStyle.SilhouettesEnabled
    '    'DocViewStyle.SkyboxType = TemplateViewStyle.SkyboxType
    '    DocViewStyle.Textures = TemplateViewStyle.Textures

    '    DocViewStyle.SkyboxType = SolidEdgeFramework.SeSkyboxType.seSkyboxTypeSkybox
    '    For i As Integer = 0 To 5
    '        DocViewStyle.SetSkyboxSideFilename(i, TemplateViewStyle.GetSkyboxSideFilename(i))
    '    Next


    '    Return ErrorMessageList
    'End Function


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

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence

        For Each Occurrence In Occurrences
            ' Fails at 'Occurrence.FaceStyle IsNot Nothing' on some files.  Reason not known.
            Try
                If Occurrence.FaceStyle IsNot Nothing Then
                    Occurrence.PutStyleNone()
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

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim AsmRefPlanes As SolidEdgeAssembly.AsmRefPlanes = SEDoc.AsmRefPlanes
        Dim AsmRefPlane As SolidEdgeAssembly.AsmRefPlane
        'Dim Occurrence As SolidEdgeAssembly.Occurrence

        If Occurrences.Count = 0 Then
            AsmRefPlanes.Visible = True
            For Each AsmRefPlane In AsmRefPlanes
                AsmRefPlane.Visible = False
                AsmRefPlane.Visible = True
            Next
        Else
            SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsShowAll, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsHideAllReferencePlanes, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.StartCommand(CType(40080, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Sketches
            SEApp.StartCommand(CType(40081, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Reference Axes
            SEApp.StartCommand(CType(40082, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.StartCommand(CType(40083, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.StartCommand(CType(40084, SolidEdgeFramework.SolidEdgeCommandConstants))
        End If

        '' SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewISOView, SolidEdgeFramework.SolidEdgeCommandConstants))
        'SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewTrimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))
        'SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewFit, SolidEdgeFramework.SolidEdgeCommandConstants))

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

        Dim Occurrences As SolidEdgeAssembly.Occurrences = SEDoc.Occurrences
        Dim AsmRefPlanes As SolidEdgeAssembly.AsmRefPlanes = SEDoc.AsmRefPlanes
        Dim AsmRefPlane As SolidEdgeAssembly.AsmRefPlane
        'Dim Occurrence As SolidEdgeAssembly.Occurrence

        If Occurrences.Count = 0 Then
            AsmRefPlanes.Visible = True
            For Each AsmRefPlane In AsmRefPlanes
                AsmRefPlane.Visible = False
                AsmRefPlane.Visible = True
            Next
            'Else
            '    SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsShowAll, SolidEdgeFramework.SolidEdgeCommandConstants))
            '    SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsHideAllReferencePlanes, SolidEdgeFramework.SolidEdgeCommandConstants))
            '    SEApp.StartCommand(CType(40080, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Sketches
            '    SEApp.StartCommand(CType(40081, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Reference Axes
            '    SEApp.StartCommand(CType(40082, SolidEdgeFramework.SolidEdgeCommandConstants))
            '    SEApp.StartCommand(CType(40083, SolidEdgeFramework.SolidEdgeCommandConstants))
            '    SEApp.StartCommand(CType(40084, SolidEdgeFramework.SolidEdgeCommandConstants))
        End If

        If Configuration("RadioButtonPictorialViewIsometric").ToLower = "true" Then
            SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewISOView, SolidEdgeFramework.SolidEdgeCommandConstants))
        End If
        If Configuration("RadioButtonPictorialViewDimetric").ToLower = "true" Then
            SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewDimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))
        End If
        If Configuration("RadioButtonPictorialViewTrimetric").ToLower = "true" Then
            SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewTrimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))
        End If

        'RadioButtonPictorialViewDimetric = True
        'RadioButtonPictorialViewIsometric = False
        ' SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewISOView, SolidEdgeFramework.SolidEdgeCommandConstants))

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

        Dim NewFilename As String = ""
        Dim NewExtension As String = ""
        Dim Filename As String
        Dim BaseFilename As String
        Dim Dir As String

        Dim Members As SolidEdgeAssembly.AssemblyFamilyMembers
        Dim Member As SolidEdgeAssembly.AssemblyFamilyMember
        Dim msg As String = ""

        Dim ImageExtensions As New List(Of String)

        ImageExtensions.Add(".bmp")
        ImageExtensions.Add(".jpg")
        ImageExtensions.Add(".tif")

        'Configuration("ComboBoxSaveAsAssemblyFileType") format examples
        'IGES (*.igs)
        'Parasolid text (*.x_b)
        NewExtension = Configuration("ComboBoxSaveAsAssemblyFileType")
        NewExtension = Split(NewExtension, Delimiter:="*")(1)
        NewExtension = Split(NewExtension, Delimiter:=")")(0)

        Filename = SEDoc.FullName

        If Not SEDoc.IsFileFamilyByDocument Then
            If Configuration("CheckBoxSaveAsAssemblyOutputDirectory") = "False" Then
                Dir = Configuration("TextBoxSaveAsAssemblyOutputDirectory")
                BaseFilename = System.IO.Path.GetFileNameWithoutExtension(Filename)
                NewFilename = Dir + "\" + System.IO.Path.ChangeExtension(BaseFilename, NewExtension)
            Else
                NewFilename = System.IO.Path.ChangeExtension(Filename, NewExtension)
            End If

            Try
                If Not ImageExtensions.Contains(NewExtension) Then
                    SEDoc.SaveAs(NewFilename)
                    SEApp.DoIdle()
                Else
                    Dim Window As SolidEdgeFramework.Window
                    Dim View As SolidEdgeFramework.View

                    Window = CType(SEApp.ActiveWindow, SolidEdgeFramework.Window)
                    View = Window.View

                    View.SaveAsImage(NewFilename)
                End If
            Catch ex As Exception
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Error saving {0}", TruncateFullPath(NewFilename, Configuration)))
            End Try

        Else
            ' D:\projects\foa1.asm!Master -> D:\foa1.asm
            Filename = Filename.Split("!"c)(0)

            Members = SEDoc.AssemblyFamilyMembers
            For Each Member In Members
                Members.ActivateMember(Member.MemberName)

                If Configuration("CheckBoxSaveAsAssemblyOutputDirectory") = "False" Then
                    Dir = Configuration("TextBoxSaveAsAssemblyOutputDirectory")
                    ' D:\projects\foa1.asm -> foa1
                    BaseFilename = System.IO.Path.GetFileNameWithoutExtension(Filename)
                    ' foa1 -> foa1-Member1
                    BaseFilename = String.Format("{0}-{1}", BaseFilename, Member.MemberName)
                    NewFilename = Dir + "\" + System.IO.Path.ChangeExtension(BaseFilename, NewExtension)
                Else
                    Dir = System.IO.Path.GetDirectoryName(Filename)
                    BaseFilename = System.IO.Path.GetFileNameWithoutExtension(Filename)
                    ' foa1 -> foa1-Member1
                    BaseFilename = String.Format("{0}-{1}", BaseFilename, Member.MemberName)
                    NewFilename = Dir + "\" + System.IO.Path.ChangeExtension(BaseFilename, NewExtension)
                End If

                Try
                    If Not ImageExtensions.Contains(NewExtension) Then
                        SEDoc.SaveAs(NewFilename)
                        SEApp.DoIdle()
                    Else
                        Dim Window As SolidEdgeFramework.Window
                        Dim View As SolidEdgeFramework.View

                        Window = CType(SEApp.ActiveWindow, SolidEdgeFramework.Window)
                        View = Window.View

                        View.SaveAsImage(NewFilename)
                    End If
                Catch ex As Exception
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("Error saving {0}", TruncateFullPath(NewFilename, Configuration)))
                End Try

            Next
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
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf InteractiveEditInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function InteractiveEditInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
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
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf RunExternalProgramInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function RunExternalProgramInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim ExternalProgram As String = Configuration("TextBoxExternalProgramAssembly")
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
                        KeyFound = True
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

        Dim ModelFilename As String
        Dim DrawingFilename As String

        ModelFilename = SEDoc.FullName

        If ModelFilename.Contains("!") Then
            ModelFilename = ModelFilename.Split("!"c)(0)
        End If

        DrawingFilename = System.IO.Path.ChangeExtension(ModelFilename, ".dft")

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
                               Of SolidEdgeAssembly.AssemblyDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf PropertyFindReplaceInternal,
                                   CType(SEDoc, SolidEdgeAssembly.AssemblyDocument),
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Private Function PropertyFindReplaceInternal(
        ByVal SEDoc As SolidEdgeAssembly.AssemblyDocument,
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
        Dim FindString As String = Configuration("TextBoxFindReplaceFindAssembly")
        Dim ReplaceString As String = Configuration("TextBoxFindReplaceReplaceAssembly")

        PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)

        For Each Properties In PropertySets
            For Each Prop In Properties
                tf = (Configuration("ComboBoxFindReplacePropertySetAssembly").ToLower = "custom")
                tf = tf And (Properties.Name.ToLower = "custom")
                If tf Then
                    If Prop.Name = Configuration("TextBoxFindReplacePropertyNameAssembly") Then
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
                    If Prop.Name = Configuration("TextBoxFindReplacePropertyNameAssembly") Then
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
