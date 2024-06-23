Option Strict On
'Imports Microsoft.WindowsAPICodePack.Dialogs
'Imports SolidEdgeAssembly
'Imports SolidEdgeFramework
'Imports SolidEdgePart

Public Class TaskUpdateModelStylesFromTemplate
    Inherits Task

    'Public Property AssemblyTemplate As String
    'Public Property PartTemplate As String
    'Public Property SheetmetalTemplate As String

    Enum ControlNames
        BrowseAssembly
        AssemblyTemplate
        BrowsePart
        PartTemplate
        BrowseSheetmetal
        SheetmetalTemplate
        HideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = True
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = False
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskUpdateModelStylesFromTemplate
        Me.Category = "Restyle"
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.AssemblyTemplate = ""
        Me.PartTemplate = ""
        Me.SheetmetalTemplate = ""

    End Sub

    Public Sub New(Task As TaskUpdateModelStylesFromTemplate)

        ' Options
        Me.AssemblyTemplate = Task.AssemblyTemplate
        Me.PartTemplate = Task.PartTemplate
        Me.SheetmetalTemplate = Task.SheetmetalTemplate
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
    Public Overrides Function Process(ByVal FileName As String) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

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

        Dim TempErrorMessageList As New List(Of String)

        Dim TemplateDoc As SolidEdgeFramework.SolidEdgeDocument
        Dim TemplateFilename As String = ""

        Dim TC As New Task_Common
        Dim DocType As String = TC.GetDocType(SEDoc)

        Select Case DocType
            Case Is = "asm"
                Dim AsmDoc As SolidEdgeAssembly.AssemblyDocument
                AsmDoc = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

                TemplateFilename = Me.AssemblyTemplate

                ' Import face styles from template
                AsmDoc.ImportStyles(TemplateFilename, True)
                SEApp.DoIdle()

                TemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgeFramework.SolidEdgeDocument)
                SEApp.DoIdle()

                ' Update base styles from template
                TempErrorMessageList = UpdateBaseStyles(SEDoc, TemplateDoc)
                If TempErrorMessageList.Count > 0 Then
                    ExitStatus = 1
                    For Each s As String In TempErrorMessageList
                        ErrorMessageList.Add(s)
                    Next
                End If

                ' Update view styles from template
                TempErrorMessageList = UpdateViewStyles(SEApp, SEDoc, TemplateDoc)
                If TempErrorMessageList.Count > 0 Then
                    ExitStatus = 1
                    For Each s As String In TempErrorMessageList
                        ErrorMessageList.Add(s)
                    Next
                End If

                TemplateDoc.Close()
                SEApp.DoIdle()

            Case Is = "par"
                Dim ParDoc As SolidEdgePart.PartDocument = CType(SEDoc, SolidEdgePart.PartDocument)

                TemplateFilename = Me.PartTemplate

                ParDoc.ImportStyles(TemplateFilename, True)
                SEApp.DoIdle()

                TemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgeFramework.SolidEdgeDocument)
                SEApp.DoIdle()

                ' Update base styles from template
                TempErrorMessageList = UpdateBaseStyles(SEDoc, TemplateDoc)
                If TempErrorMessageList.Count > 0 Then
                    ExitStatus = 1
                    For Each s As String In TempErrorMessageList
                        ErrorMessageList.Add(s)
                    Next
                End If

                ' Update view styles from template
                TempErrorMessageList = UpdateViewStyles(SEApp, SEDoc, TemplateDoc)
                If TempErrorMessageList.Count > 0 Then
                    ExitStatus = 1
                    For Each s As String In TempErrorMessageList
                        ErrorMessageList.Add(s)
                    Next
                End If

                TemplateDoc.Close()
                SEApp.DoIdle()

            Case Is = "psm"
                Dim PsmDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)

                TemplateFilename = Me.SheetmetalTemplate

                PsmDoc.ImportStyles(TemplateFilename, True)
                SEApp.DoIdle()

                TemplateDoc = CType(SEApp.Documents.Open(TemplateFilename), SolidEdgeFramework.SolidEdgeDocument)
                SEApp.DoIdle()

                TempErrorMessageList = UpdateBaseStyles(SEDoc, TemplateDoc)
                If TempErrorMessageList.Count > 0 Then
                    ExitStatus = 1
                    For Each s As String In TempErrorMessageList
                        ErrorMessageList.Add(s)
                    Next
                End If

                ' Update view styles from template
                TempErrorMessageList = UpdateViewStyles(SEApp, SEDoc, TemplateDoc)
                If TempErrorMessageList.Count > 0 Then
                    ExitStatus = 1
                    For Each s As String In TempErrorMessageList
                        ErrorMessageList.Add(s)
                    Next
                End If

                TemplateDoc.Close()
                SEApp.DoIdle()

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))

        End Select


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


    Private Function UpdateBaseStyles(
        ByRef SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByRef SETemplateDoc As SolidEdgeFramework.SolidEdgeDocument
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        Dim TemplateBaseStyles As New Dictionary(Of String, SolidEdgeFramework.FaceStyle)
        Dim DocBaseStyles As New Dictionary(Of String, SolidEdgeFramework.FaceStyle)

        TemplateBaseStyles = GetBaseStyles(SETemplateDoc)

        UpdateBaseFaceStyles(SEDoc, TemplateBaseStyles)

        DocBaseStyles = GetBaseStyles(SEDoc)

        If DocBaseStyles.Keys.Count < TemplateBaseStyles.Keys.Count Then
            ErrorMessageList.Add("Unable to update all Color Manager base styles")
        End If

        Return ErrorMessageList
    End Function

    Private Function GetBaseStyles(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument
        ) As Dictionary(Of String, SolidEdgeFramework.FaceStyle)

        Dim BaseStyles As New Dictionary(Of String, SolidEdgeFramework.FaceStyle)

        Dim ConstructionBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim ThreadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim CurveBaseStyle As SolidEdgeFramework.FaceStyle = Nothing

        Dim WeldbeadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim PartBaseStyle As SolidEdgeFramework.FaceStyle = Nothing

        Dim TC As New Task_Common

        Dim DocType As String = TC.GetDocType(SEDoc)

        Select Case DocType
            Case = "asm"
                Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

                tmpSEDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyConstructionStyle, ConstructionBaseStyle)
                If Not ConstructionBaseStyle Is Nothing Then
                    BaseStyles(ConstructionBaseStyle.StyleName) = ConstructionBaseStyle
                End If

                tmpSEDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyThreadedCylindersStyle, ThreadBaseStyle)
                If Not ThreadBaseStyle Is Nothing Then
                    BaseStyles(ThreadBaseStyle.StyleName) = ThreadBaseStyle
                End If

                tmpSEDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyCurveStyle, CurveBaseStyle)
                If Not CurveBaseStyle Is Nothing Then
                    BaseStyles(CurveBaseStyle.StyleName) = CurveBaseStyle
                End If

                tmpSEDoc.GetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyWeldBeadStyle, WeldbeadBaseStyle)
                If Not WeldbeadBaseStyle Is Nothing Then
                    BaseStyles(WeldbeadBaseStyle.StyleName) = WeldbeadBaseStyle
                End If

            Case = "par"
                Dim tmpSEDoc As SolidEdgePart.PartDocument = CType(SEDoc, SolidEdgePart.PartDocument)

                tmpSEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seConstructionBaseStyle, ConstructionBaseStyle)
                If Not ConstructionBaseStyle Is Nothing Then
                    BaseStyles(ConstructionBaseStyle.StyleName) = ConstructionBaseStyle
                End If

                tmpSEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seThreadedCylindersBaseStyle, ThreadBaseStyle)
                If Not ThreadBaseStyle Is Nothing Then
                    BaseStyles(ThreadBaseStyle.StyleName) = ThreadBaseStyle
                End If

                tmpSEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seCurveBaseStyle, CurveBaseStyle)
                If Not CurveBaseStyle Is Nothing Then
                    BaseStyles(CurveBaseStyle.StyleName) = CurveBaseStyle
                End If

                tmpSEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle, PartBaseStyle)
                If Not PartBaseStyle Is Nothing Then
                    BaseStyles(PartBaseStyle.StyleName) = PartBaseStyle
                End If

            Case = "psm"
                Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)

                tmpSEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seConstructionBaseStyle, ConstructionBaseStyle)
                BaseStyles(ConstructionBaseStyle.StyleName) = ConstructionBaseStyle

                tmpSEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seThreadedCylindersBaseStyle, ThreadBaseStyle)
                If Not ThreadBaseStyle Is Nothing Then
                    BaseStyles(ThreadBaseStyle.StyleName) = ThreadBaseStyle
                End If

                tmpSEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seCurveBaseStyle, CurveBaseStyle)
                If Not CurveBaseStyle Is Nothing Then
                    BaseStyles(CurveBaseStyle.StyleName) = CurveBaseStyle
                End If

                tmpSEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle, PartBaseStyle)
                If Not PartBaseStyle Is Nothing Then
                    BaseStyles(PartBaseStyle.StyleName) = PartBaseStyle
                End If

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        End Select

        Return BaseStyles
    End Function

    Private Sub UpdateBaseFaceStyles(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        SourceBaseStyles As Dictionary(Of String, SolidEdgeFramework.FaceStyle))

        Dim FaceStyles As SolidEdgeFramework.FaceStyles
        Dim FaceStyle As SolidEdgeFramework.FaceStyle
        Dim Name As String

        Dim TC As New Task_Common

        Dim DocType As String = TC.GetDocType(SEDoc)

        Select Case DocType
            Case = "asm"
                Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)
                FaceStyles = CType(tmpSEDoc.FaceStyles, SolidEdgeFramework.FaceStyles)

                For Each FaceStyle In FaceStyles
                    Name = FaceStyle.StyleName
                    If SourceBaseStyles.Keys.Contains(Name) Then
                        If Not SourceBaseStyles(Name) Is Nothing Then
                            If Name.Contains("Construction") Then
                                tmpSEDoc.SetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyConstructionStyle, FaceStyle)
                            End If
                            If Name.Contains("Thread") Then
                                tmpSEDoc.SetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyThreadedCylindersStyle, FaceStyle)
                            End If
                            If Name.Contains("Curve") Then
                                tmpSEDoc.SetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyCurveStyle, FaceStyle)
                            End If
                            If Name.Contains("Weld") Then
                                tmpSEDoc.SetBaseStyle(SolidEdgeAssembly.AssemblyBaseStylesConstants.seAssemblyWeldBeadStyle, FaceStyle)
                            End If
                        End If
                    End If
                Next

            Case = "par"
                Dim tmpSEDoc As SolidEdgePart.PartDocument = CType(SEDoc, SolidEdgePart.PartDocument)
                FaceStyles = CType(tmpSEDoc.FaceStyles, SolidEdgeFramework.FaceStyles)

                For Each FaceStyle In FaceStyles
                    Name = FaceStyle.StyleName
                    If SourceBaseStyles.Keys.Contains(Name) Then
                        If Not SourceBaseStyles(Name) Is Nothing Then
                            If Name.Contains("Construction") Then
                                tmpSEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seConstructionBaseStyle, FaceStyle)
                            End If
                            If Name.Contains("Thread") Then
                                tmpSEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seThreadedCylindersBaseStyle, FaceStyle)
                            End If
                            If Name.Contains("Curve") Then
                                tmpSEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seCurveBaseStyle, FaceStyle)
                            End If
                            If Name.Contains("Model") Then
                                ' Don't overwrite if it already exists
                                Dim PartBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
                                tmpSEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle, PartBaseStyle)
                                If PartBaseStyle Is Nothing Then
                                    tmpSEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle, FaceStyle)
                                End If
                            End If
                        End If
                    End If
                Next

            Case = "psm"
                Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                FaceStyles = CType(tmpSEDoc.FaceStyles, SolidEdgeFramework.FaceStyles)

                For Each FaceStyle In FaceStyles
                    Name = FaceStyle.StyleName
                    If SourceBaseStyles.Keys.Contains(Name) Then
                        If Not SourceBaseStyles(Name) Is Nothing Then
                            If Name.Contains("Construction") Then
                                tmpSEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seConstructionBaseStyle, FaceStyle)
                            End If
                            If Name.Contains("Thread") Then
                                tmpSEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seThreadedCylindersBaseStyle, FaceStyle)
                            End If
                            If Name.Contains("Curve") Then
                                tmpSEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.seCurveBaseStyle, FaceStyle)
                            End If
                            If Name.Contains("Model") Then
                                ' Don't overwrite if it already exists
                                Dim PartBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
                                tmpSEDoc.GetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle, PartBaseStyle)
                                If PartBaseStyle Is Nothing Then
                                    tmpSEDoc.SetBaseStyle(SolidEdgePart.PartBaseStylesConstants.sePartBaseStyle, FaceStyle)
                                End If
                            End If
                        End If
                    End If
                Next

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        End Select

    End Sub

    Private Function UpdateViewStyles(
        ByRef SEApp As SolidEdgeFramework.Application,
        ByRef SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByRef SETemplateDoc As SolidEdgeFramework.SolidEdgeDocument
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        Dim TempErrorMessageList As New List(Of String)

        Dim TemplateViewStyles As SolidEdgeFramework.ViewStyles = Nothing
        Dim DocViewStyles As SolidEdgeFramework.ViewStyles = Nothing
        Dim DocViewStyle As SolidEdgeFramework.ViewStyle
        Dim TemplateActiveViewStyle As SolidEdgeFramework.ViewStyle = Nothing
        Dim DocActiveViewStyle As SolidEdgeFramework.ViewStyle = Nothing

        Dim DocWindows As SolidEdgeFramework.Windows = Nothing
        Dim TemplateWindows As SolidEdgeFramework.Windows = Nothing
        Dim Window As SolidEdgeFramework.Window
        Dim View As SolidEdgeFramework.View

        Dim AsmDoc As SolidEdgeAssembly.AssemblyDocument = Nothing
        Dim AsmTemplateDoc As SolidEdgeAssembly.AssemblyDocument = Nothing
        Dim ParDoc As SolidEdgePart.PartDocument = Nothing
        Dim ParTemplateDoc As SolidEdgePart.PartDocument = Nothing
        Dim PsmDoc As SolidEdgePart.SheetMetalDocument = Nothing
        Dim PsmTemplateDoc As SolidEdgePart.SheetMetalDocument = Nothing

        Dim tf As Boolean

        Dim TC As New Task_Common

        Dim DocType As String = TC.GetDocType(SEDoc)

        Select Case DocType
            Case = "asm"
                AsmDoc = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)
                DocViewStyles = CType(AsmDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
                DocWindows = AsmDoc.Windows

                AsmTemplateDoc = CType(SETemplateDoc, SolidEdgeAssembly.AssemblyDocument)
                TemplateViewStyles = CType(AsmTemplateDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
                TemplateWindows = AsmTemplateDoc.Windows

                AsmTemplateDoc.Activate()

            Case = "par"
                ParDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                DocViewStyles = CType(ParDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
                DocWindows = ParDoc.Windows

                ParTemplateDoc = CType(SETemplateDoc, SolidEdgePart.PartDocument)
                TemplateViewStyles = CType(ParTemplateDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
                TemplateWindows = ParTemplateDoc.Windows

                ParTemplateDoc.Activate()

            Case = "psm"
                PsmDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                DocViewStyles = CType(PsmDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
                DocWindows = PsmDoc.Windows

                PsmTemplateDoc = CType(SETemplateDoc, SolidEdgePart.SheetMetalDocument)
                TemplateViewStyles = CType(PsmTemplateDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
                TemplateWindows = PsmTemplateDoc.Windows

                PsmTemplateDoc.Activate()

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        End Select

        'SETemplateDoc.Activate()

        For Each Window In TemplateWindows
            View = Window.View
            TemplateActiveViewStyle = CType(View.ViewStyle, SolidEdgeFramework.ViewStyle)
        Next

        If DocType = "asm" Then
            AsmDoc.Activate()
            DocActiveViewStyle = DocViewStyles.AddFromFile(AsmTemplateDoc.FullName, TemplateActiveViewStyle.StyleName)
        End If
        If DocType = "par" Then
            ParDoc.Activate()
            DocActiveViewStyle = DocViewStyles.AddFromFile(ParTemplateDoc.FullName, TemplateActiveViewStyle.StyleName)
        End If
        If DocType = "psm" Then
            PsmDoc.Activate()
            DocActiveViewStyle = DocViewStyles.AddFromFile(PsmTemplateDoc.FullName, TemplateActiveViewStyle.StyleName)
        End If

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

        For Each Window In DocWindows
            View = Window.View
            View.Style = DocActiveViewStyle.StyleName
        Next

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


    'Public Overrides Function GetTLPTask(TLPParent As ExTableLayoutPanel) As ExTableLayoutPanel
    '    ControlsDict = New Dictionary(Of String, Control)

    '    Dim IU As New InterfaceUtilities

    '    Me.TLPTask = IU.BuildTLPTask(Me, TLPParent)

    '    Me.TLPOptions = BuildTLPOptions()

    '    For Each Control As Control In Me.TLPTask.Controls
    '        If ControlsDict.Keys.Contains(Control.Name) Then
    '            MsgBox(String.Format("ControlsDict already has Key '{0}'", Control.Name))
    '        End If
    '        ControlsDict(Control.Name) = Control
    '    Next

    '    Me.TLPTask.Controls.Add(TLPOptions, Me.TLPTask.ColumnCount - 2, 1)

    '    Return Me.TLPTask
    'End Function

    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim TextBox As TextBox
        Dim Button As Button
        Dim CheckBox As CheckBox

        Dim IU As New InterfaceUtilities

        IU.FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        Button = IU.FormatOptionsButton(ControlNames.BrowseAssembly.ToString, "Asm Template")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = IU.FormatOptionsTextBox(ControlNames.AssemblyTemplate.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Button = IU.FormatOptionsButton(ControlNames.BrowsePart.ToString, "Par Template")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = IU.FormatOptionsTextBox(ControlNames.PartTemplate.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Button = IU.FormatOptionsButton(ControlNames.BrowseSheetmetal.ToString, "Psm Template")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = IU.FormatOptionsTextBox(ControlNames.SheetmetalTemplate.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

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
        Dim TextBox As TextBox

        TextBox = CType(ControlsDict(ControlNames.AssemblyTemplate.ToString), TextBox)
        Me.AssemblyTemplate = TextBox.Text

        TextBox = CType(ControlsDict(ControlNames.PartTemplate.ToString), TextBox)
        Me.PartTemplate = TextBox.Text

        TextBox = CType(ControlsDict(ControlNames.SheetmetalTemplate.ToString), TextBox)
        Me.SheetmetalTemplate = TextBox.Text

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

            If (Me.IsSelectedAssembly) And (Not FileIO.FileSystem.FileExists(Me.AssemblyTemplate)) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select a valid assembly template", Indent))
            End If

            If (Me.IsSelectedPart) And (Not FileIO.FileSystem.FileExists(Me.PartTemplate)) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select a valid part template", Indent))
            End If

            If (Me.IsSelectedSheetmetal) And (Not FileIO.FileSystem.FileExists(Me.SheetmetalTemplate)) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select a valid sheetmetal template", Indent))
            End If

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox

        Select Case Name
            Case ControlNames.BrowseAssembly.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select an assembly template file"
                tmpFileDialog.Filter = "asm files|*.asm"

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.AssemblyTemplate = tmpFileDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.AssemblyTemplate.ToString), TextBox)
                    TextBox.Text = Me.AssemblyTemplate
                End If

            Case ControlNames.BrowsePart.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a part template file"
                tmpFileDialog.Filter = "par files|*.par"

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.PartTemplate = tmpFileDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.PartTemplate.ToString), TextBox)
                    TextBox.Text = Me.PartTemplate
                End If

            Case ControlNames.BrowseSheetmetal.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a sheetmetal template file"
                tmpFileDialog.Filter = "psm files|*.psm"

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.SheetmetalTemplate = tmpFileDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.SheetmetalTemplate.ToString), TextBox)
                    TextBox.Text = Me.SheetmetalTemplate
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select

    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.HideOptions.ToString
                HandleHideOptionsChange(Me, Me.TaskOptionsTLP, Checkbox)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name

            Case ControlNames.AssemblyTemplate.ToString
                Me.AssemblyTemplate = TextBox.Text

            Case ControlNames.PartTemplate.ToString
                Me.PartTemplate = TextBox.Text

            Case ControlNames.SheetmetalTemplate.ToString
                Me.SheetmetalTemplate = TextBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Updates the file with face and view styles from a file you specify on the **Configuration Tab -- Templates Page**. "
        HelpString += vbCrLf + vbCrLf + "Note, the view style must be a named style.  Overrides are ignored. "
        HelpString += "To create a named style from an override, open the template in Solid Edge, activate the `View Overrides` dialog, and click `Save As`."
        HelpString += vbCrLf + vbCrLf + "![View Override Dialog](My%20Project/media/view_override_dialog.png)"

        Return HelpString
    End Function



End Class
