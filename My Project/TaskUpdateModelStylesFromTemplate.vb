Option Strict On

Public Class TaskUpdateModelStylesFromTemplate
    Inherits Task

    Private _AssemblyTemplate As String
    Public Property AssemblyTemplate As String
        Get
            Return _AssemblyTemplate
        End Get
        Set(value As String)
            _AssemblyTemplate = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AssemblyTemplate.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _PartTemplate As String
    Public Property PartTemplate As String
        Get
            Return _PartTemplate
        End Get
        Set(value As String)
            _PartTemplate = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.PartTemplate.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _SheetmetalTemplate As String
    Public Property SheetmetalTemplate As String
        Get
            Return _SheetmetalTemplate
        End Get
        Set(value As String)
            _SheetmetalTemplate = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.SheetmetalTemplate.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _UseConfigurationPageTemplates As Boolean
    Public Property UseConfigurationPageTemplates As Boolean
        Get
            Return _UseConfigurationPageTemplates
        End Get
        Set(value As Boolean)
            _UseConfigurationPageTemplates = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UseConfigurationPageTemplates.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateAllStyles As Boolean
    Public Property UpdateAllStyles As Boolean
        Get
            Return _UpdateAllStyles
        End Get
        Set(value As Boolean)
            _UpdateAllStyles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateAllStyles.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateDimensionStyles As Boolean
    Public Property UpdateDimensionStyles As Boolean
        Get
            Return _UpdateDimensionStyles
        End Get
        Set(value As Boolean)
            _UpdateDimensionStyles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateDimensionStyles.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateFaceStyles As Boolean
    Public Property UpdateFaceStyles As Boolean
        Get
            Return _UpdateFaceStyles
        End Get
        Set(value As Boolean)
            _UpdateFaceStyles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateFaceStyles.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateLinearStyles As Boolean
    Public Property UpdateLinearStyles As Boolean
        Get
            Return _UpdateLinearStyles
        End Get
        Set(value As Boolean)
            _UpdateLinearStyles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateLinearStyles.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateTextCharStyles As Boolean
    Public Property UpdateTextCharStyles As Boolean
        Get
            Return _UpdateTextCharStyles
        End Get
        Set(value As Boolean)
            _UpdateTextCharStyles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateTextCharStyles.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateTextStyles As Boolean
    Public Property UpdateTextStyles As Boolean
        Get
            Return _UpdateTextStyles
        End Get
        Set(value As Boolean)
            _UpdateTextStyles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateTextStyles.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateViewStyles As Boolean
    Public Property UpdateViewStyles As Boolean
        Get
            Return _UpdateViewStyles
        End Get
        Set(value As Boolean)
            _UpdateViewStyles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateViewStyles.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateBaseStyles As Boolean
    Public Property UpdateBaseStyles As Boolean
        Get
            Return _UpdateBaseStyles
        End Get
        Set(value As Boolean)
            _UpdateBaseStyles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateBaseStyles.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _RemoveNonTemplateStyles As Boolean
    Public Property RemoveNonTemplateStyles As Boolean
        Get
            Return _RemoveNonTemplateStyles
        End Get
        Set(value As Boolean)
            _RemoveNonTemplateStyles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.RemoveNonTemplateStyles.ToString), CheckBox).Checked = value
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
        UseConfigurationPageTemplates
        BrowseAssembly
        AssemblyTemplate
        BrowsePart
        PartTemplate
        BrowseSheetmetal
        SheetmetalTemplate
        UpdateAllStyles
        UpdateDimensionStyles
        UpdateFaceStyles
        UpdateLinearStyles
        UpdateTextCharStyles
        UpdateTextStyles
        UpdateViewStyles
        UpdateBaseStyles
        RemoveNonTemplateStyles
        AutoHideOptions
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
        Me.RequiresAssemblyTemplate = True
        Me.RequiresPartTemplate = True
        Me.RequiresSheetmetalTemplate = True
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.AssemblyTemplate = ""
        Me.PartTemplate = ""
        Me.SheetmetalTemplate = ""
        Me.UpdateDimensionStyles = False
        Me.UpdateFaceStyles = False
        Me.UpdateLinearStyles = False
        Me.UpdateTextCharStyles = False
        Me.UpdateTextStyles = False
        Me.UpdateViewStyles = False
        Me.UpdateBaseStyles = False
        Me.RemoveNonTemplateStyles = False

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

        Dim tf As Boolean
        tf = SEDoc.FullName = Me.AssemblyTemplate
        tf = tf Or SEDoc.FullName = Me.PartTemplate
        tf = tf Or SEDoc.FullName = Me.SheetmetalTemplate
        If tf Then
            TaskLogger.AddMessage("Template file itself ineligible for processing")
            Exit Sub
        End If


        Dim UC As New UtilsCommon

        Dim AsmTemplateDoc As SolidEdgeAssembly.AssemblyDocument = Nothing
        Dim ParTemplateDoc As SolidEdgePart.PartDocument = Nothing
        Dim PsmTemplateDoc As SolidEdgePart.SheetMetalDocument = Nothing

        Dim DocDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles = Nothing
        Dim TemplateDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles = Nothing

        Dim DocLinearStyles As SolidEdgeFramework.LinearStyles = Nothing
        Dim TemplateLinearStyles As SolidEdgeFramework.LinearStyles = Nothing

        Dim DocTextCharStyles As SolidEdgeFramework.TextCharStyles = Nothing
        Dim TemplateTextCharStyles As SolidEdgeFramework.TextCharStyles = Nothing

        Dim DocTextStyles As SolidEdgeFramework.TextStyles = Nothing
        Dim TemplateTextStyles As SolidEdgeFramework.TextStyles = Nothing

        Dim DocViewStyles As SolidEdgeFramework.ViewStyles = Nothing
        Dim TemplateViewStyles As SolidEdgeFramework.ViewStyles = Nothing

        Dim DocWindows As SolidEdgeFramework.Windows = Nothing
        Dim TemplateWindows As SolidEdgeFramework.Windows = Nothing


        ' All style collections.
        ' DimensionStyles, FaceStyles, FillStyles, HatchPatternStyles, 
        ' LinearStyles, TextCharStyles, TextStyles, ViewStyles

        ' Styles updated
        ' DimensionStyles, FaceStyles,
        ' LinearStyles, TextCharStyles, TextStyles, ViewStyles

        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case "asm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)
                AsmTemplateDoc = CType(SEApp.Documents.Open(Me.AssemblyTemplate), SolidEdgeAssembly.AssemblyDocument)

                tmpSEDoc.Activate()

                ' DimensionStyles
                DocDimensionStyles = CType(tmpSEDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)
                TemplateDimensionStyles = CType(AsmTemplateDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)

                ' FaceStyles
                If Me.UpdateFaceStyles Then
                    tmpSEDoc.ImportStyles(Me.AssemblyTemplate, True)
                End If

                ' LinearStyles
                DocLinearStyles = CType(tmpSEDoc.LinearStyles, SolidEdgeFramework.LinearStyles)
                TemplateLinearStyles = CType(AsmTemplateDoc.LinearStyles, SolidEdgeFramework.LinearStyles)

                ' TextCharStyles
                DocTextCharStyles = CType(tmpSEDoc.TextCharStyles, SolidEdgeFramework.TextCharStyles)
                TemplateTextCharStyles = CType(AsmTemplateDoc.TextCharStyles, SolidEdgeFramework.TextCharStyles)

                ' In SE2024 the TextStyles collection lists LineStyle objects
                '' TextStyles
                'DocTextStyles = CType(tmpSEDoc.TextStyles, SolidEdgeFramework.TextStyles)
                'TemplateTextStyles = CType(AsmTemplateDoc.TextStyles, SolidEdgeFramework.TextStyles)

                ' ViewStyles
                DocViewStyles = CType(tmpSEDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
                TemplateViewStyles = CType(AsmTemplateDoc.ViewStyles, SolidEdgeFramework.ViewStyles)

                ' Windows
                DocWindows = tmpSEDoc.Windows
                TemplateWindows = AsmTemplateDoc.Windows

                ' BaseStyles
                If Me.UpdateBaseStyles Then
                    DoUpdateBaseStyles(SEDoc, CType(AsmTemplateDoc, SolidEdgeFramework.SolidEdgeDocument))
                End If

            Case "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                ParTemplateDoc = CType(SEApp.Documents.Open(Me.PartTemplate), SolidEdgePart.PartDocument)

                tmpSEDoc.Activate()

                ' DimensionStyles
                DocDimensionStyles = CType(tmpSEDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)
                TemplateDimensionStyles = CType(ParTemplateDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)

                ' FaceStyles
                If Me.UpdateFaceStyles Then
                    tmpSEDoc.ImportStyles(Me.PartTemplate, True)
                End If

                ' LinearStyles
                DocLinearStyles = CType(tmpSEDoc.LinearStyles, SolidEdgeFramework.LinearStyles)
                TemplateLinearStyles = CType(ParTemplateDoc.LinearStyles, SolidEdgeFramework.LinearStyles)

                ' TextCharStyles
                DocTextCharStyles = CType(tmpSEDoc.TextCharStyles, SolidEdgeFramework.TextCharStyles)
                TemplateTextCharStyles = CType(ParTemplateDoc.TextCharStyles, SolidEdgeFramework.TextCharStyles)

                ' TextStyles
                DocTextStyles = CType(tmpSEDoc.TextStyles, SolidEdgeFramework.TextStyles)
                TemplateTextStyles = CType(ParTemplateDoc.TextStyles, SolidEdgeFramework.TextStyles)

                ' ViewStyles
                DocViewStyles = CType(tmpSEDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
                TemplateViewStyles = CType(ParTemplateDoc.ViewStyles, SolidEdgeFramework.ViewStyles)

                ' Windows
                DocWindows = tmpSEDoc.Windows
                TemplateWindows = ParTemplateDoc.Windows

                ' BaseStyles
                If Me.UpdateBaseStyles Then
                    DoUpdateBaseStyles(SEDoc, CType(ParTemplateDoc, SolidEdgeFramework.SolidEdgeDocument))
                End If

            Case "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                PsmTemplateDoc = CType(SEApp.Documents.Open(Me.SheetmetalTemplate), SolidEdgePart.SheetMetalDocument)

                tmpSEDoc.Activate()

                ' DimensionStyles
                DocDimensionStyles = CType(tmpSEDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)
                TemplateDimensionStyles = CType(PsmTemplateDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)

                ' FaceStyles
                If Me.UpdateFaceStyles Then
                    tmpSEDoc.ImportStyles(Me.SheetmetalTemplate, True)
                End If

                ' LinearStyles
                DocLinearStyles = CType(tmpSEDoc.LinearStyles, SolidEdgeFramework.LinearStyles)
                TemplateLinearStyles = CType(PsmTemplateDoc.LinearStyles, SolidEdgeFramework.LinearStyles)

                ' TextCharStyles
                DocTextCharStyles = CType(tmpSEDoc.TextCharStyles, SolidEdgeFramework.TextCharStyles)
                TemplateTextCharStyles = CType(PsmTemplateDoc.TextCharStyles, SolidEdgeFramework.TextCharStyles)

                ' TextStyles
                DocTextStyles = CType(tmpSEDoc.TextStyles, SolidEdgeFramework.TextStyles)
                TemplateTextStyles = CType(PsmTemplateDoc.TextStyles, SolidEdgeFramework.TextStyles)

                ' ViewStyles
                DocViewStyles = CType(tmpSEDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
                TemplateViewStyles = CType(PsmTemplateDoc.ViewStyles, SolidEdgeFramework.ViewStyles)

                ' Windows
                DocWindows = tmpSEDoc.Windows
                TemplateWindows = PsmTemplateDoc.Windows

                ' BaseStyles
                If Me.UpdateBaseStyles Then
                    DoUpdateBaseStyles(SEDoc, CType(PsmTemplateDoc, SolidEdgeFramework.SolidEdgeDocument))
                End If

        End Select

        If Me.UpdateDimensionStyles Then
            DoUpdateDimensionStyles(DocDimensionStyles, TemplateDimensionStyles)
        End If

        If Me.UpdateLinearStyles Then
            DoUpdateLinearStyles(DocLinearStyles, TemplateLinearStyles)
        End If

        If Me.UpdateTextCharStyles Then
            DoUpdateTextCharStyles(DocTextCharStyles, TemplateTextCharStyles)
        End If

        If Not (DocTextStyles Is Nothing Or TemplateTextStyles Is Nothing) Then
            If Me.UpdateTextStyles Then
                DoUpdateTextStyles(DocTextStyles, TemplateTextStyles)
            End If
        End If

        If Me.UpdateViewStyles Then
            DoUpdateViewStyles(DocViewStyles, TemplateViewStyles)

            SetActiveViewStyle(SEApp, DocWindows, TemplateWindows, AsmTemplateDoc, ParTemplateDoc, PsmTemplateDoc)
        End If


        If SEDoc.ReadOnly Then
            TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

    End Sub


    Private Sub SetActiveViewStyle(SEApp As SolidEdgeFramework.Application,
                                   DocWindows As SolidEdgeFramework.Windows,
                                   TemplateWindows As SolidEdgeFramework.Windows,
                                   AsmTemplateDoc As SolidEdgeAssembly.AssemblyDocument,
                                   ParTemplateDoc As SolidEdgePart.PartDocument,
                                   PsmTemplateDoc As SolidEdgePart.SheetMetalDocument
                                   )

        Dim TemplateActiveViewStyle As SolidEdgeFramework.ViewStyle = Nothing
        Dim DocActiveViewStyle As SolidEdgeFramework.ViewStyle = Nothing

        If AsmTemplateDoc IsNot Nothing Then
            AsmTemplateDoc.Activate()
            SEApp.DoIdle()
        End If
        If ParTemplateDoc IsNot Nothing Then
            ParTemplateDoc.Activate()
            SEApp.DoIdle()
        End If
        If PsmTemplateDoc IsNot Nothing Then
            PsmTemplateDoc.Activate()
            SEApp.DoIdle()
        End If

        For Each Window As SolidEdgeFramework.Window In TemplateWindows
            Dim View = Window.View
            TemplateActiveViewStyle = CType(View.ViewStyle, SolidEdgeFramework.ViewStyle)
        Next

        Dim ViewStyleName = TemplateActiveViewStyle.StyleName

        If AsmTemplateDoc IsNot Nothing Then
            AsmTemplateDoc.Close()
            SEApp.DoIdle()
        End If
        If ParTemplateDoc IsNot Nothing Then
            ParTemplateDoc.Close()
            SEApp.DoIdle()
        End If
        If PsmTemplateDoc IsNot Nothing Then
            PsmTemplateDoc.Close()
            SEApp.DoIdle()
        End If

        For Each Window As SolidEdgeFramework.Window In DocWindows
            Dim View = Window.View
            View.Style = ViewStyleName
        Next


    End Sub

    Private Sub DoUpdateViewStyles(
        ByRef DocViewStyles As SolidEdgeFramework.ViewStyles,
        ByRef TemplateViewStyles As SolidEdgeFramework.ViewStyles
        )

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim MissingStyles As String = ""

        Dim UC As New UtilsCommon

        For Each TemplateViewStyle As SolidEdgeFramework.ViewStyle In TemplateViewStyles
            If Not TemplateStyleNames.Contains(TemplateViewStyle.StyleName) Then
                TemplateStyleNames.Add(TemplateViewStyle.StyleName)
            End If

            Dim TemplateStyleInDoc = False

            For Each DocViewStyle As SolidEdgeFramework.ViewStyle In DocViewStyles
                If Not DocStyleNames.Contains(DocViewStyle.StyleName) Then
                    DocStyleNames.Add(DocViewStyle.StyleName)
                End If
                If TemplateViewStyle.StyleName = DocViewStyle.StyleName Then
                    TemplateStyleInDoc = True
                    Try
                        UC.CopyProperties(TemplateViewStyle, DocViewStyle, TaskLogger.AddLogger($"View style {TemplateViewStyle.StyleName}"))

                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Error updating ViewStyle '{0}'", TemplateViewStyle.StyleName))
                    End Try

                    'Update skybox
                    If TemplateViewStyle.SkyboxType = SolidEdgeFramework.SeSkyboxType.seSkyboxTypeSkybox Then
                        DocViewStyle.SkyboxType = TemplateViewStyle.SkyboxType

                        Dim s As String = ""
                        Dim i As Integer
                        For i = 0 To 5
                            Try
                                s = TemplateViewStyle.GetSkyboxSideFilename(i)
                                DocViewStyle.SetSkyboxSideFilename(i, s)
                            Catch ex As Exception
                                TaskLogger.AddMessage(String.Format("ViewStyle '{0}' SkyBox image '{1}' not found", TemplateViewStyle.StyleName, s))
                            End Try
                        Next

                    End If

                    Exit For

                End If
            Next

            If Not TemplateStyleInDoc Then
                ' Add it
                Dim NewViewStyle = DocViewStyles.Add(TemplateViewStyle.StyleName, "")
                Try
                    UC.CopyProperties(TemplateViewStyle, NewViewStyle, TaskLogger.AddLogger($"View style {TemplateViewStyle.StyleName}"))
                Catch ex As Exception
                    TaskLogger.AddMessage(String.Format("Error configuring ViewStyle '{0}'", TemplateViewStyle.StyleName))
                End Try

                'Update skybox
                If TemplateViewStyle.SkyboxType = SolidEdgeFramework.SeSkyboxType.seSkyboxTypeSkybox Then
                    NewViewStyle.SkyboxType = TemplateViewStyle.SkyboxType

                    Dim s As String = ""
                    Dim i As Integer
                    For i = 0 To 5
                        Try
                            s = TemplateViewStyle.GetSkyboxSideFilename(i)
                            NewViewStyle.SetSkyboxSideFilename(i, s)
                        Catch ex As Exception
                            TaskLogger.AddMessage(String.Format("ViewStyle '{0}' SkyBox image '{1}' not found", TemplateViewStyle.StyleName, s))
                        End Try
                    Next

                End If

            End If
        Next

        Dim tf As Boolean
        If Me.RemoveNonTemplateStyles Then
            For Each DocViewStyle As SolidEdgeFramework.ViewStyle In DocViewStyles
                tf = Not DocViewStyle.StyleName.ToLower() = "default"
                tf = tf And Not TemplateStyleNames.Contains(DocViewStyle.StyleName)
                If tf Then
                    Dim s = DocViewStyle.StyleName.ToString
                    Try
                        DocViewStyle.Delete()
                        DocStyleNames.Remove(s)
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Unable to remove ViewStyle '{0}'", s))
                    End Try
                End If
            Next
        End If

    End Sub

    Private Sub DoUpdateTextStyles(
        ByRef DocTextStyles As SolidEdgeFramework.TextStyles,
        ByRef TemplateTextStyles As SolidEdgeFramework.TextStyles
        )

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim MissingStyles As String = ""

        Dim UC As New UtilsCommon

        For Each TemplateTextStyle As SolidEdgeFramework.TextStyle In TemplateTextStyles
            If Not TemplateStyleNames.Contains(TemplateTextStyle.Name) Then
                TemplateStyleNames.Add(TemplateTextStyle.Name)
            End If

            Dim TemplateStyleInDoc = False

            For Each DocTextStyle As SolidEdgeFramework.TextStyle In DocTextStyles
                If Not DocStyleNames.Contains(DocTextStyle.Name) Then
                    DocStyleNames.Add(DocTextStyle.Name)
                End If
                If TemplateTextStyle.Name = DocTextStyle.Name Then
                    TemplateStyleInDoc = True

                    Try
                        UC.CopyProperties(TemplateTextStyle, DocTextStyle, TaskLogger.AddLogger($"Text style {TemplateTextStyle.Name}"))
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Error applying TextStyle '{0}'", TemplateTextStyle.Name))
                    End Try

                    Exit For
                End If
            Next

            If Not TemplateStyleInDoc Then
                ' Add it
                Dim NewTextStyle = DocTextStyles.Add(TemplateTextStyle.Name, "")
                Try
                    UC.CopyProperties(TemplateTextStyle, NewTextStyle, TaskLogger.AddLogger($"Text style {TemplateTextStyle.Name}"))
                Catch ex As Exception
                    TaskLogger.AddMessage(String.Format("Error configuring TextStyle '{0}'", TemplateTextStyle.Name))
                End Try
            End If

        Next

        Dim tf As Boolean
        If Me.RemoveNonTemplateStyles Then
            For Each DocTextStyle As SolidEdgeFramework.TextStyle In DocTextStyles
                tf = Not DocTextStyle.Name.ToLower() = "default"
                tf = tf And Not TemplateStyleNames.Contains(DocTextStyle.Name)
                If tf Then
                    Dim s = DocTextStyle.Name.ToString
                    Try
                        DocTextStyles.Remove(DocTextStyle.Name)
                        DocStyleNames.Remove(s)
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Unable to remove TextStyle '{0}'", s))
                    End Try
                End If
            Next
        End If

    End Sub

    Private Sub DoUpdateTextCharStyles(
        ByRef DocTextCharStyles As SolidEdgeFramework.TextCharStyles,
        ByRef TemplateTextCharStyles As SolidEdgeFramework.TextCharStyles
        )

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim MissingStyles As String = ""

        Dim UC As New UtilsCommon

        For Each TemplateTextCharStyle As SolidEdgeFramework.TextCharStyle In TemplateTextCharStyles
            If Not TemplateStyleNames.Contains(TemplateTextCharStyle.Name) Then
                TemplateStyleNames.Add(TemplateTextCharStyle.Name)
            End If

            Dim TemplateStyleInDoc = False

            For Each DocTextCharStyle As SolidEdgeFramework.TextCharStyle In DocTextCharStyles
                If Not DocStyleNames.Contains(DocTextCharStyle.Name) Then
                    DocStyleNames.Add(DocTextCharStyle.Name)
                End If
                If TemplateTextCharStyle.Name = DocTextCharStyle.Name Then
                    TemplateStyleInDoc = True

                    Try
                        UC.CopyProperties(TemplateTextCharStyle, DocTextCharStyle, TaskLogger.AddLogger($"Text char style {TemplateTextCharStyle.Name}"))
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Error applying TextCharStyle '{0}'", TemplateTextCharStyle.Name))
                    End Try

                    Exit For

                End If
            Next

            If Not TemplateStyleInDoc Then
                ' Add it
                Dim NewTextCharStyle = DocTextCharStyles.Add(TemplateTextCharStyle.Name, "")
                Try
                    UC.CopyProperties(TemplateTextCharStyle, NewTextCharStyle, TaskLogger.AddLogger($"Text char style {TemplateTextCharStyle.Name}"))
                Catch ex As Exception
                    TaskLogger.AddMessage(String.Format("Error configuring TextCharStyle '{0}'", TemplateTextCharStyle.Name))
                End Try
            End If

        Next

        Dim tf As Boolean
        If Me.RemoveNonTemplateStyles Then
            For Each DocTextCharStyle As SolidEdgeFramework.TextCharStyle In DocTextCharStyles
                tf = Not DocTextCharStyle.Name.ToLower() = "default"
                tf = tf And Not TemplateStyleNames.Contains(DocTextCharStyle.Name)
                If tf Then
                    Dim s = DocTextCharStyle.Name.ToString
                    Try
                        DocTextCharStyles.Remove(DocTextCharStyle.Name)
                        DocStyleNames.Remove(s)
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Unable to remove TextCharStyle '{0}'", s))
                    End Try
                End If
            Next
        End If

    End Sub

    Private Sub DoUpdateLinearStyles(
        ByRef DocLinearStyles As SolidEdgeFramework.LinearStyles,
        ByRef TemplateLinearStyles As SolidEdgeFramework.LinearStyles
        )

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim MissingStyles As String = ""

        Dim UC As New UtilsCommon

        For Each TemplateLinearStyle As SolidEdgeFramework.LinearStyle In TemplateLinearStyles
            If Not TemplateStyleNames.Contains(TemplateLinearStyle.Name) Then
                TemplateStyleNames.Add(TemplateLinearStyle.Name)
            End If

            Dim TemplateStyleInDoc = False

            For Each DocLinearStyle As SolidEdgeFramework.LinearStyle In DocLinearStyles
                If Not DocStyleNames.Contains(DocLinearStyle.Name) Then
                    DocStyleNames.Add(DocLinearStyle.Name)
                End If
                If TemplateLinearStyle.Name = DocLinearStyle.Name Then
                    TemplateStyleInDoc = True

                    Try
                        UC.CopyProperties(TemplateLinearStyle, DocLinearStyle, TaskLogger.AddLogger($"Linear style {TemplateLinearStyle.Name}"))
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Error applying LinearStyle '{0}'", TemplateLinearStyle.Name))
                    End Try

                    Exit For
                End If
            Next

            If Not TemplateStyleInDoc Then
                ' Add it
                Dim NewLinearStyle = DocLinearStyles.Add(TemplateLinearStyle.Name, "")
                Try
                    UC.CopyProperties(TemplateLinearStyle, NewLinearStyle, TaskLogger.AddLogger($"Linear style {TemplateLinearStyle.Name}"))
                Catch ex As Exception
                    TaskLogger.AddMessage(String.Format("Error configuring LinearStyle '{0}'", TemplateLinearStyle.Name))
                End Try
            End If

        Next

        Dim tf As Boolean
        If Me.RemoveNonTemplateStyles Then
            For Each DocLinearStyle As SolidEdgeFramework.LinearStyle In DocLinearStyles
                tf = Not DocLinearStyle.Name.ToLower() = "default"
                tf = tf And Not TemplateStyleNames.Contains(DocLinearStyle.Name)
                If tf Then
                    Dim s = DocLinearStyle.Name.ToString
                    Try
                        DocLinearStyles.Remove(DocLinearStyle.Name)
                        DocStyleNames.Remove(s)
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Unable to remove LinearStyle '{0}'", s))
                    End Try
                End If
            Next
        End If

    End Sub

    Private Sub DoUpdateDimensionStyles(
        ByRef DocDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles,
        ByRef TemplateDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles
        )

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim MissingStyles As String = ""

        Dim UC As New UtilsCommon

        For Each TemplateDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle In TemplateDimensionStyles
            If Not TemplateStyleNames.Contains(TemplateDimensionStyle.Name) Then
                TemplateStyleNames.Add(TemplateDimensionStyle.Name)
            End If

            Dim TemplateStyleInDoc = False

            For Each DocDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle In DocDimensionStyles
                If Not DocStyleNames.Contains(DocDimensionStyle.Name) Then
                    DocStyleNames.Add(DocDimensionStyle.Name)
                End If
                If TemplateDimensionStyle.Name = DocDimensionStyle.Name Then
                    TemplateStyleInDoc = True

                    Try
                        UC.CopyProperties(TemplateDimensionStyle, DocDimensionStyle, TaskLogger.AddLogger($"Dimension style {TemplateDimensionStyle.Name}"))
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Error applying DimensionStyle '{0}'", TemplateDimensionStyle.Name))

                    End Try

                    Exit For
                End If
            Next

            If Not TemplateStyleInDoc Then
                ' Add it
                Dim NewDimensionStyle = DocDimensionStyles.Add(TemplateDimensionStyle.Name, "")
                Try
                    UC.CopyProperties(TemplateDimensionStyle, NewDimensionStyle, TaskLogger.AddLogger($"Dimension style {TemplateDimensionStyle.Name}"))
                Catch ex As Exception
                    TaskLogger.AddMessage(String.Format("Error configuring DimensionStyle '{0}'", TemplateDimensionStyle.Name))
                End Try
            End If

        Next

        Dim tf As Boolean
        If Me.RemoveNonTemplateStyles Then
            For Each DocDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle In DocDimensionStyles
                tf = Not DocDimensionStyle.Name.ToLower() = "default"
                tf = tf And Not TemplateStyleNames.Contains(DocDimensionStyle.Name)
                If tf Then
                    Dim s = DocDimensionStyle.Name.ToString
                    Try
                        DocDimensionStyles.Remove(DocDimensionStyle.Name)
                        DocStyleNames.Remove(s)
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Unable to remove DimensionStyle '{0}'", s))
                    End Try
                End If
            Next
        End If

    End Sub

    Private Sub DoUpdateBaseStyles(
        ByRef SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByRef SETemplateDoc As SolidEdgeFramework.SolidEdgeDocument
        )

        Dim TemplateBaseStyles As New Dictionary(Of String, SolidEdgeFramework.FaceStyle)
        Dim DocBaseStyles As New Dictionary(Of String, SolidEdgeFramework.FaceStyle)

        TemplateBaseStyles = GetBaseStyles(SETemplateDoc)

        UpdateBaseFaceStyles(SEDoc, TemplateBaseStyles)

        DocBaseStyles = GetBaseStyles(SEDoc)

        If DocBaseStyles.Keys.Count < TemplateBaseStyles.Keys.Count Then
            TaskLogger.AddMessage("Unable to update all Color Manager base styles")
        End If

    End Sub

    Private Function GetBaseStyles(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument
        ) As Dictionary(Of String, SolidEdgeFramework.FaceStyle)

        Dim BaseStyles As New Dictionary(Of String, SolidEdgeFramework.FaceStyle)

        Dim ConstructionBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim ThreadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim CurveBaseStyle As SolidEdgeFramework.FaceStyle = Nothing

        Dim WeldbeadBaseStyle As SolidEdgeFramework.FaceStyle = Nothing
        Dim PartBaseStyle As SolidEdgeFramework.FaceStyle = Nothing

        Dim UC As New UtilsCommon

        Dim DocType As String = UC.GetDocType(SEDoc)

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

        Dim UC As New UtilsCommon

        Dim DocType As String = UC.GetDocType(SEDoc)

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


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim TextBox As TextBox
        Dim Button As Button
        Dim CheckBox As CheckBox

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.UseConfigurationPageTemplates.ToString, "Use configuration page templates")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.BrowseAssembly.ToString, "Asm Template")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.AssemblyTemplate.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.BrowsePart.ToString, "Par Template")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.PartTemplate.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.BrowseSheetmetal.ToString, "Psm Template")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.SheetmetalTemplate.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateAllStyles.ToString, "Update all styles")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateDimensionStyles.ToString, "Update dimension styles")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateFaceStyles.ToString, "Update face styles")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateLinearStyles.ToString, "Update linear styles")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateTextCharStyles.ToString, "Update text char styles")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateTextStyles.ToString, "Update text styles")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateViewStyles.ToString, "Update view styles")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateBaseStyles.ToString, "Update base styles")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.RemoveNonTemplateStyles.ToString, "Remove styles not in template")
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

            If (Me.IsSelectedAssembly) And (Not FileIO.FileSystem.FileExists(Me.AssemblyTemplate)) Then
                ErrorLogger.AddMessage("Select a valid assembly template")
            End If

            If (Me.IsSelectedPart) And (Not FileIO.FileSystem.FileExists(Me.PartTemplate)) Then
                ErrorLogger.AddMessage("Select a valid part template")
            End If

            If (Me.IsSelectedSheetmetal) And (Not FileIO.FileSystem.FileExists(Me.SheetmetalTemplate)) Then
                ErrorLogger.AddMessage("Select a valid sheetmetal template")
            End If

        End If

    End Sub


    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox

        Select Case Name
            Case ControlNames.BrowseAssembly.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select an assembly template file"
                tmpFileDialog.Filter = "asm files|*.asm"

                If IO.File.Exists(Me.AssemblyTemplate) Then
                    tmpFileDialog.InitialDirectory = IO.Path.GetDirectoryName(Me.AssemblyTemplate)
                Else
                    tmpFileDialog.InitialDirectory = Form_Main.SETemplatePath
                End If

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.AssemblyTemplate = tmpFileDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.AssemblyTemplate.ToString), TextBox)
                    TextBox.Text = Me.AssemblyTemplate

                    'Form_Main.SETemplatePath = IO.Path.GetDirectoryName(Me.AssemblyTemplate)
                End If

            Case ControlNames.BrowsePart.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a part template file"
                tmpFileDialog.Filter = "par files|*.par"

                If IO.File.Exists(Me.AssemblyTemplate) Then ' Not a typo
                    tmpFileDialog.InitialDirectory = IO.Path.GetDirectoryName(Me.AssemblyTemplate)
                Else
                    tmpFileDialog.InitialDirectory = Form_Main.SETemplatePath
                End If

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.PartTemplate = tmpFileDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.PartTemplate.ToString), TextBox)
                    TextBox.Text = Me.PartTemplate

                    'Form_Main.SETemplatePath = IO.Path.GetDirectoryName(Me.PartTemplate)
                End If

            Case ControlNames.BrowseSheetmetal.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a sheetmetal template file"
                tmpFileDialog.Filter = "psm files|*.psm"

                If IO.File.Exists(Me.AssemblyTemplate) Then ' Not a typo
                    tmpFileDialog.InitialDirectory = IO.Path.GetDirectoryName(Me.AssemblyTemplate)
                Else
                    tmpFileDialog.InitialDirectory = Form_Main.SETemplatePath
                End If

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.SheetmetalTemplate = tmpFileDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.SheetmetalTemplate.ToString), TextBox)
                    TextBox.Text = Me.SheetmetalTemplate

                    'Form_Main.SETemplatePath = IO.Path.GetDirectoryName(Me.SheetmetalTemplate)
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select

    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.UseConfigurationPageTemplates.ToString
                Me.UseConfigurationPageTemplates = Checkbox.Checked

                If Me.UseConfigurationPageTemplates Then
                    Me.AssemblyTemplate = Form_Main.AssemblyTemplate
                    CType(ControlsDict(ControlNames.BrowseAssembly.ToString), Button).Visible = False
                    CType(ControlsDict(ControlNames.AssemblyTemplate.ToString), TextBox).Visible = False

                    Me.PartTemplate = Form_Main.PartTemplate
                    CType(ControlsDict(ControlNames.BrowsePart.ToString), Button).Visible = False
                    CType(ControlsDict(ControlNames.PartTemplate.ToString), TextBox).Visible = False

                    Me.SheetmetalTemplate = Form_Main.SheetmetalTemplate
                    CType(ControlsDict(ControlNames.BrowseSheetmetal.ToString), Button).Visible = False
                    CType(ControlsDict(ControlNames.SheetmetalTemplate.ToString), TextBox).Visible = False

                Else
                    CType(ControlsDict(ControlNames.BrowseAssembly.ToString), Button).Visible = True
                    CType(ControlsDict(ControlNames.AssemblyTemplate.ToString), TextBox).Visible = True

                    CType(ControlsDict(ControlNames.BrowsePart.ToString), Button).Visible = True
                    CType(ControlsDict(ControlNames.PartTemplate.ToString), TextBox).Visible = True

                    CType(ControlsDict(ControlNames.BrowseSheetmetal.ToString), Button).Visible = True
                    CType(ControlsDict(ControlNames.SheetmetalTemplate.ToString), TextBox).Visible = True

                End If


            Case ControlNames.UpdateAllStyles.ToString
                Me.UpdateAllStyles = Checkbox.Checked

                If Checkbox.Checked Then
                    Me.UpdateDimensionStyles = True
                    Me.UpdateFaceStyles = True
                    Me.UpdateLinearStyles = True
                    Me.UpdateTextCharStyles = True
                    Me.UpdateTextStyles = True
                    Me.UpdateViewStyles = True
                    Me.UpdateBaseStyles = True

                    CType(ControlsDict(ControlNames.UpdateDimensionStyles.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.UpdateFaceStyles.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.UpdateLinearStyles.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.UpdateTextCharStyles.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.UpdateTextStyles.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.UpdateViewStyles.ToString), CheckBox).Checked = True
                    CType(ControlsDict(ControlNames.UpdateBaseStyles.ToString), CheckBox).Checked = True

                End If

                CType(ControlsDict(ControlNames.UpdateDimensionStyles.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.UpdateFaceStyles.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.UpdateLinearStyles.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.UpdateTextCharStyles.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.UpdateTextStyles.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.UpdateViewStyles.ToString), CheckBox).Visible = Not Checkbox.Checked
                CType(ControlsDict(ControlNames.UpdateBaseStyles.ToString), CheckBox).Visible = Not Checkbox.Checked



            Case ControlNames.UpdateDimensionStyles.ToString
                Me.UpdateDimensionStyles = Checkbox.Checked

            Case ControlNames.UpdateFaceStyles.ToString
                Me.UpdateFaceStyles = Checkbox.Checked

            Case ControlNames.UpdateLinearStyles.ToString
                Me.UpdateLinearStyles = Checkbox.Checked

            Case ControlNames.UpdateTextCharStyles.ToString
                Me.UpdateTextCharStyles = Checkbox.Checked

            Case ControlNames.UpdateTextStyles.ToString
                Me.UpdateTextStyles = Checkbox.Checked

            Case ControlNames.UpdateViewStyles.ToString
                Me.UpdateViewStyles = Checkbox.Checked

            Case ControlNames.UpdateBaseStyles.ToString
                Me.UpdateBaseStyles = Checkbox.Checked

            Case ControlNames.RemoveNonTemplateStyles.ToString
                Me.RemoveNonTemplateStyles = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

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
        HelpString = "Updates the styles you select from a template you specify. "

        HelpString += vbCrLf + vbCrLf + "![UpdateModelStylesFromTemplate](My%20Project/media/task_update_model_styles_from_template.png)"

        HelpString += vbCrLf + vbCrLf + "Using the checkboxes provided, you can update all styles, or select which ones to update individually. "

        HelpString += vbCrLf + vbCrLf + "Styles present in the template, but not in the file, are added. "
        HelpString += "Styles present in the file, but not in the template, can optionally be removed, if possible. "
        HelpString += "Set the option `Remove styles not in template` as needed. "
        HelpString += "It is not possible to remove a style if Solid Edge thinks it is in use (even if it isn't). "

        HelpString += vbCrLf + vbCrLf + "Styles are updated/added as described, but no mapping takes place. "
        HelpString += "For example, if the template has a dimension style ANSI(in), and the file instead uses ANSI(inch), the dimensions will not be updated. "
        HelpString += "A workaround is to create the target style in the template and modify it in that file as needed. "

        HelpString += vbCrLf + vbCrLf + "The active view style of the file is changed to match the one active in the template. "
        HelpString += "Note, it must be a named style.  Overrides are ignored. "
        HelpString += "To create a named style from an override, open the template in Solid Edge, activate the `View Overrides` dialog, and click `Save As`."
        HelpString += vbCrLf + vbCrLf + "![View Override Dialog](My%20Project/media/view_override_dialog.png)"

        Return HelpString
    End Function



End Class
