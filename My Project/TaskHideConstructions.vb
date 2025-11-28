Option Strict On

Public Class TaskHideConstructions

    Inherits Task

    Private _AllShow As Boolean
    Public Property AllShow As Boolean
        Get
            Return _AllShow
        End Get
        Set(value As Boolean)
            _AllShow = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AllShow.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _AllHide As Boolean
    Public Property AllHide As Boolean
        Get
            Return _AllHide
        End Get
        Set(value As Boolean)
            _AllHide = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AllHide.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _ReferencePlanesShow As Boolean
    Public Property ReferencePlanesShow As Boolean
        Get
            Return _ReferencePlanesShow
        End Get
        Set(value As Boolean)
            _ReferencePlanesShow = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.ReferencePlanesShow.ToString), CheckBox)
                CB.Checked = ReferencePlanesShow
                If ReferencePlanesShow And ReferencePlanesHide Then ReferencePlanesHide = False
            End If
        End Set
    End Property

    Private _ReferencePlanesHide As Boolean
    Public Property ReferencePlanesHide As Boolean
        Get
            Return _ReferencePlanesHide
        End Get
        Set(value As Boolean)
            _ReferencePlanesHide = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.ReferencePlanesHide.ToString), CheckBox)
                CB.Checked = ReferencePlanesHide
                If ReferencePlanesHide And ReferencePlanesShow Then ReferencePlanesShow = False
            End If
        End Set
    End Property

    Private _CoordinateSystemsShow As Boolean
    Public Property CoordinateSystemsShow As Boolean
        Get
            Return _CoordinateSystemsShow
        End Get
        Set(value As Boolean)
            _CoordinateSystemsShow = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.CoordinateSystemsShow.ToString), CheckBox)
                CB.Checked = CoordinateSystemsShow
                If CoordinateSystemsShow And CoordinateSystemsHide Then CoordinateSystemsHide = False
            End If
        End Set
    End Property

    Private _CoordinateSystemsHide As Boolean
    Public Property CoordinateSystemsHide As Boolean
        Get
            Return _CoordinateSystemsHide
        End Get
        Set(value As Boolean)
            _CoordinateSystemsHide = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.CoordinateSystemsHide.ToString), CheckBox)
                CB.Checked = CoordinateSystemsHide
                If CoordinateSystemsHide And CoordinateSystemsShow Then CoordinateSystemsShow = False
            End If
        End Set
    End Property

    Private _SketchesShow As Boolean
    Public Property SketchesShow As Boolean
        Get
            Return _SketchesShow
        End Get
        Set(value As Boolean)
            _SketchesShow = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.SketchesShow.ToString), CheckBox)
                CB.Checked = SketchesShow
                If SketchesShow And SketchesHide Then SketchesHide = False
            End If
        End Set
    End Property

    Private _SketchesHide As Boolean
    Public Property SketchesHide As Boolean
        Get
            Return _SketchesHide
        End Get
        Set(value As Boolean)
            _SketchesHide = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.SketchesHide.ToString), CheckBox)
                CB.Checked = SketchesHide
                If SketchesHide And SketchesShow Then SketchesShow = False
            End If
        End Set
    End Property

    Private _Sketches_3dShow As Boolean
    Public Property Sketches_3dShow As Boolean
        Get
            Return _Sketches_3dShow
        End Get
        Set(value As Boolean)
            _Sketches_3dShow = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.Sketches_3dShow.ToString), CheckBox)
                CB.Checked = Sketches_3dShow
                If Sketches_3dShow And Sketches_3dHide Then Sketches_3dHide = False
            End If
        End Set
    End Property

    Private _Sketches_3dHide As Boolean
    Public Property Sketches_3dHide As Boolean
        Get
            Return _Sketches_3dHide
        End Get
        Set(value As Boolean)
            _Sketches_3dHide = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.Sketches_3dHide.ToString), CheckBox)
                CB.Checked = Sketches_3dHide
                If Sketches_3dHide And Sketches_3dShow Then Sketches_3dShow = False
            End If
        End Set
    End Property

    Private _CurvesShow As Boolean
    Public Property CurvesShow As Boolean
        Get
            Return _CurvesShow
        End Get
        Set(value As Boolean)
            _CurvesShow = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.CurvesShow.ToString), CheckBox)
                CB.Checked = CurvesShow
                If CurvesShow And CurvesHide Then CurvesHide = False
            End If
        End Set
    End Property

    Private _CurvesHide As Boolean
    Public Property CurvesHide As Boolean
        Get
            Return _CurvesHide
        End Get
        Set(value As Boolean)
            _CurvesHide = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.CurvesHide.ToString), CheckBox)
                CB.Checked = CurvesHide
                If CurvesHide And CurvesShow Then CurvesShow = False
            End If
        End Set
    End Property

    Private _ConstructionsShow As Boolean
    Public Property ConstructionsShow As Boolean
        Get
            Return _ConstructionsShow
        End Get
        Set(value As Boolean)
            _ConstructionsShow = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.ConstructionsShow.ToString), CheckBox)
                CB.Checked = ConstructionsShow
                If ConstructionsShow And ConstructionsHide Then ConstructionsHide = False
            End If
        End Set
    End Property

    Private _ConstructionsHide As Boolean
    Public Property ConstructionsHide As Boolean
        Get
            Return _ConstructionsHide
        End Get
        Set(value As Boolean)
            _ConstructionsHide = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.ConstructionsHide.ToString), CheckBox)
                CB.Checked = ConstructionsHide
                If ConstructionsHide And ConstructionsShow Then ConstructionsShow = False
            End If
        End Set
    End Property

    Private _SurfacesShow As Boolean
    Public Property SurfacesShow As Boolean
        Get
            Return _SurfacesShow
        End Get
        Set(value As Boolean)
            _SurfacesShow = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.SurfacesShow.ToString), CheckBox)
                CB.Checked = SurfacesShow
                If SurfacesShow And SurfacesHide Then SurfacesHide = False
            End If
        End Set
    End Property

    Private _SurfacesHide As Boolean
    Public Property SurfacesHide As Boolean
        Get
            Return _SurfacesHide
        End Get
        Set(value As Boolean)
            _SurfacesHide = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.SurfacesHide.ToString), CheckBox)
                CB.Checked = SurfacesHide
                If SurfacesHide And SurfacesShow Then SurfacesShow = False
            End If
        End Set
    End Property

    Private _PhysicalPropertySymbolsShow As Boolean
    Public Property PhysicalPropertySymbolsShow As Boolean
        Get
            Return _PhysicalPropertySymbolsShow
        End Get
        Set(value As Boolean)
            _PhysicalPropertySymbolsShow = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.PhysicalPropertySymbolsShow.ToString), CheckBox)
                CB.Checked = PhysicalPropertySymbolsShow
                If PhysicalPropertySymbolsShow And PhysicalPropertySymbolsHide Then PhysicalPropertySymbolsHide = False
            End If
        End Set
    End Property

    Private _PhysicalPropertySymbolsHide As Boolean
    Public Property PhysicalPropertySymbolsHide As Boolean
        Get
            Return _PhysicalPropertySymbolsHide
        End Get
        Set(value As Boolean)
            _PhysicalPropertySymbolsHide = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.PhysicalPropertySymbolsHide.ToString), CheckBox)
                CB.Checked = PhysicalPropertySymbolsHide
                If PhysicalPropertySymbolsHide And PhysicalPropertySymbolsShow Then PhysicalPropertySymbolsShow = False
            End If
        End Set
    End Property

    Private _ReferenceAxesShow As Boolean
    Public Property ReferenceAxesShow As Boolean
        Get
            Return _ReferenceAxesShow
        End Get
        Set(value As Boolean)
            _ReferenceAxesShow = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.ReferenceAxesShow.ToString), CheckBox)
                CB.Checked = ReferenceAxesShow
                If ReferenceAxesShow And ReferenceAxesHide Then ReferenceAxesHide = False
            End If
        End Set
    End Property

    Private _ReferenceAxesHide As Boolean
    Public Property ReferenceAxesHide As Boolean
        Get
            Return _ReferenceAxesHide
        End Get
        Set(value As Boolean)
            _ReferenceAxesHide = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.ReferenceAxesHide.ToString), CheckBox)
                CB.Checked = ReferenceAxesHide
                If ReferenceAxesHide And ReferenceAxesShow Then ReferenceAxesShow = False
            End If
        End Set
    End Property

    Private _PMIShow As Boolean
    Public Property PMIShow As Boolean
        Get
            Return _PMIShow
        End Get
        Set(value As Boolean)
            _PMIShow = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.PMIShow.ToString), CheckBox)
                CB.Checked = PMIShow
                If PMIShow And PMIHide Then PMIHide = False
            End If
        End Set
    End Property

    Private _PMIHide As Boolean
    Public Property PMIHide As Boolean
        Get
            Return _PMIHide
        End Get
        Set(value As Boolean)
            _PMIHide = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim CB = CType(ControlsDict(ControlNames.PMIHide.ToString), CheckBox)
                CB.Checked = PMIHide
                If PMIHide And PMIShow Then PMIShow = False
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
        Show
        Hide
        All
        AllShow
        AllHide
        ReferencePlanes
        ReferencePlanesShow
        ReferencePlanesHide
        CoordinateSystems
        CoordinateSystemsShow
        CoordinateSystemsHide
        Sketches
        SketchesShow
        SketchesHide
        Sketches_3d
        Sketches_3dShow
        Sketches_3dHide
        Curves
        CurvesShow
        CurvesHide
        Constructions
        ConstructionsShow
        ConstructionsHide
        Surfaces
        SurfacesShow
        SurfacesHide
        PhysicalPropertySymbols
        PhysicalPropertySymbolsShow
        PhysicalPropertySymbolsHide
        ReferenceAxes
        ReferenceAxesShow
        ReferenceAxesHide
        PMI
        PMIShow
        PMIHide
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
        Me.Image = My.Resources.TaskHideConstructions
        Me.Category = "Restyle"
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options

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

        Dim NewWay As Boolean = True
        If NewWay Then
            DoReferencePlanes(SEApp, SEDoc)
            DoCoordinateSystems(SEApp, SEDoc)
        Else
            Dim PMI As SolidEdgeFrameworkSupport.PMI = Nothing
            Dim Sketches As SolidEdgePart.Sketchs = Nothing
            Dim Sketch As SolidEdgePart.Sketch
            Dim Profiles As SolidEdgePart.Profiles = Nothing
            Dim RefPlanes As SolidEdgePart.RefPlanes = Nothing
            Dim RefPlane As SolidEdgePart.RefPlane
            Dim Models As SolidEdgePart.Models = Nothing
            Dim Model As SolidEdgePart.Model
            Dim Constructions As SolidEdgePart.Constructions = Nothing
            Dim CoordinateSystems As SolidEdgePart.CoordinateSystems = Nothing

            Dim UC As New UtilsCommon
            Dim DocType As String = UC.GetDocType(SEDoc)
            Dim Profile As SolidEdgePart.Profile

            Select Case DocType
                Case = "asm"
                    Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

                    Dim Occurrences As SolidEdgeAssembly.Occurrences = tmpSEDoc.Occurrences
                    Dim AsmRefPlanes As SolidEdgeAssembly.AsmRefPlanes = tmpSEDoc.AsmRefPlanes

                    Dim SketchesAssembly As SolidEdgeAssembly.ComponentLayouts
                    Dim SketchAssembly As SolidEdgeAssembly.Layout

                    PMI = CType(tmpSEDoc.PMI, SolidEdgeFrameworkSupport.PMI)

                    Try
                        SketchesAssembly = tmpSEDoc.ComponentLayouts
                        For Each SketchAssembly In SketchesAssembly
                            Profile = CType(SketchAssembly.Profile, SolidEdgePart.Profile)
                            Profile.Visible = False
                        Next
                    Catch ex As Exception
                    End Try

                    If Occurrences.Count = 0 Then
                        AsmRefPlanes.Visible = True

                    Else
                        SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsShowAll, SolidEdgeFramework.SolidEdgeCommandConstants))
                        SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsHideAllReferencePlanes, SolidEdgeFramework.SolidEdgeCommandConstants))
                        SEApp.StartCommand(CType(40080, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Sketches & 3DSketches
                        SEApp.StartCommand(CType(40081, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Reference Axes
                        SEApp.StartCommand(CType(40082, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Coordinate Systems
                        SEApp.StartCommand(CType(40083, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide surfaces
                        SEApp.StartCommand(CType(40084, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Curves
                    End If

                Case = "par"
                    Dim tmpSEDoc As SolidEdgePart.PartDocument = CType(SEDoc, SolidEdgePart.PartDocument)

                    Sketches = tmpSEDoc.Sketches

                    PMI = CType(tmpSEDoc.PMI, SolidEdgeFrameworkSupport.PMI)

                    Models = tmpSEDoc.Models

                    RefPlanes = tmpSEDoc.RefPlanes

                    Constructions = tmpSEDoc.Constructions

                    CoordinateSystems = tmpSEDoc.CoordinateSystems

                Case = "psm"
                    Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)

                    Dim Etches As SolidEdgePart.Etches
                    Dim Etch As SolidEdgePart.Etch

                    Sketches = tmpSEDoc.Sketches

                    PMI = CType(tmpSEDoc.PMI, SolidEdgeFrameworkSupport.PMI)

                    Models = tmpSEDoc.Models

                    RefPlanes = tmpSEDoc.RefPlanes

                    Constructions = tmpSEDoc.Constructions

                    CoordinateSystems = tmpSEDoc.CoordinateSystems

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

                Case Else
                    MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
            End Select


            If PMI IsNot Nothing Then
                Try
                    PMI.Show = False
                    If Not DocType = "asm" Then
                        PMI.ShowDimensions = False
                        PMI.ShowAnnotations = False
                    End If
                Catch ex As Exception
                End Try
            End If

            If (DocType = "par") Or (DocType = "psm") Then
                If Sketches IsNot Nothing Then
                    Try
                        For Each Sketch In Sketches
                            Profiles = Sketch.Profiles
                            For Each Profile In Profiles
                                Profile.Visible = False
                            Next
                        Next
                    Catch ex As Exception
                    End Try
                End If

                If (Models IsNot Nothing) And (RefPlanes IsNot Nothing) Then
                    If Models.Count > 0 Then
                        For Each RefPlane In RefPlanes
                            RefPlane.Visible = False
                        Next
                    Else
                        For Each RefPlane In RefPlanes
                            RefPlane.Visible = True
                        Next
                    End If
                End If

                If Constructions IsNot Nothing Then
                    'Some imported files crash on this command
                    Try
                        Constructions.Visible = False
                    Catch ex As Exception
                    End Try
                End If

                If CoordinateSystems IsNot Nothing Then
                    CoordinateSystems.Visible = False
                End If

            End If
        End If

        If SEDoc.ReadOnly Then
            TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

    End Sub

    Private Sub DoReferencePlanes(
        SEApp As SolidEdgeFramework.Application,
        SEDoc As SolidEdgeFramework.SolidEdgeDocument)

        If Not (Me.ReferencePlanesShow Or Me.ReferencePlanesHide) Then Exit Sub

        Dim Show As Boolean = Me.ReferenceAxesShow ' False means Hide

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case "asm"
                Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)
                Dim OccurrenceCount As Integer = tmpSEDoc.Occurrences.Count
                Dim AsmRefPlanes As SolidEdgeAssembly.AsmRefPlanes = tmpSEDoc.AsmRefPlanes

                If Show Then
                    AsmRefPlanes.Visible = True
                    For Each AsmRefPlane As SolidEdgeAssembly.AsmRefPlane In AsmRefPlanes
                        If AsmRefPlane.Global Then
                            AsmRefPlane.Visible = False
                            AsmRefPlane.Visible = True
                        Else
                            AsmRefPlane.Visible = False
                        End If
                    Next
                Else
                    If OccurrenceCount = 0 Then
                        AsmRefPlanes.Visible = True
                        For Each AsmRefPlane As SolidEdgeAssembly.AsmRefPlane In AsmRefPlanes
                            If AsmRefPlane.Global Then
                                AsmRefPlane.Visible = False
                                AsmRefPlane.Visible = True
                            Else
                                AsmRefPlane.Visible = False
                            End If
                        Next
                    Else
                        Dim HideCommand = SolidEdgeConstants.AssemblyCommandConstants.AssemblyAssemblyToolsHideAllReferencePlanes
                        SEApp.StartCommand(CType(HideCommand, SolidEdgeFramework.SolidEdgeCommandConstants))
                    End If
                End If

            Case "par", "psm"
                Dim RefPlanes As SolidEdgePart.RefPlanes = Nothing
                Dim ModelsCount As Integer

                If DocType = "par" Then
                    Dim tmpSEDoc As SolidEdgePart.PartDocument = CType(SEDoc, SolidEdgePart.PartDocument)
                    RefPlanes = tmpSEDoc.RefPlanes
                    ModelsCount = tmpSEDoc.Models.Count
                Else
                    Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                    RefPlanes = tmpSEDoc.RefPlanes
                    ModelsCount = tmpSEDoc.Models.Count
                End If

                For Each RefPlane As SolidEdgePart.RefPlane In RefPlanes
                    If RefPlane.Global Then
                        If Show Then
                            RefPlane.Visible = True
                        Else
                            If ModelsCount = 0 Then
                                RefPlane.Visible = True
                            Else
                                RefPlane.Visible = False
                            End If
                        End If
                    Else
                        RefPlane.Visible = False
                    End If
                Next
                If Show Then

                End If
        End Select

    End Sub

    Private Sub DoCoordinateSystems(
        SEApp As SolidEdgeFramework.Application,
        SEDoc As SolidEdgeFramework.SolidEdgeDocument)

        If Not (Me.CoordinateSystemsShow Or Me.CoordinateSystemsHide) Then Exit Sub

        Dim Show As Boolean = Me.ReferenceAxesShow ' False means Hide

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case "asm"
                Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)
                Dim CoordinateSystems As SolidEdgePart.CoordinateSystems = tmpSEDoc.CoordinateSystems

                If Show Then
                    CoordinateSystems.Visible = True
                    For Each CoordinateSystem As SolidEdgePart.CoordinateSystem In CoordinateSystems
                        CoordinateSystem.Visible = False
                        CoordinateSystem.Visible = True
                    Next
                Else
                    Dim HideCoordinateSystems = 40082
                    SEApp.StartCommand(CType(HideCoordinateSystems, SolidEdgeFramework.SolidEdgeCommandConstants))
                End If

            Case "par", "psm"
                Dim CoordinateSystems As SolidEdgePart.CoordinateSystems = Nothing

                If DocType = "par" Then
                    Dim tmpSEDoc As SolidEdgePart.PartDocument = CType(SEDoc, SolidEdgePart.PartDocument)
                    CoordinateSystems = tmpSEDoc.CoordinateSystems
                Else
                    Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                    CoordinateSystems = tmpSEDoc.CoordinateSystems
                End If

                If Show Then
                    CoordinateSystems.Visible = True
                    For Each CoordinateSystem As SolidEdgePart.CoordinateSystem In CoordinateSystems
                        CoordinateSystem.Visible = False
                        CoordinateSystem.Visible = True
                    Next
                Else
                    CoordinateSystems.Visible = False
                    For Each CoordinateSystem As SolidEdgePart.CoordinateSystem In CoordinateSystems
                        CoordinateSystem.Visible = True
                        CoordinateSystem.Visible = False
                    Next
                End If
        End Select
    End Sub

    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        'Dim ComboBox As ComboBox
        'Dim TextBox As TextBox
        Dim Label As Label
        'Dim Button As Button
        'Dim ControlWidth As Integer = 200
        Dim NewFileTypeLabelText = ""

        'Dim CtrlName As String
        'Dim CtrlText As String

        Dim OptionList As New List(Of String)

        FormatTLPOptionsEx(tmpTLPOptions, "TLPOptions", 10, 50, 50)

        RowIndex = 0

        Label = FormatOptionsLabel(ControlNames.Show.ToString, ControlNames.Show.ToString)
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        Label.Anchor = AnchorStyles.None
        ControlsDict(Label.Name) = Label

        Label = FormatOptionsLabel(ControlNames.Hide.ToString, ControlNames.Hide.ToString)
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        Label.Anchor = AnchorStyles.None
        ControlsDict(Label.Name) = Label

        Dim ShowList As New List(Of String)
        ShowList.AddRange({ControlNames.AllShow.ToString, ControlNames.ReferencePlanesShow.ToString, ControlNames.CoordinateSystemsShow.ToString})
        ShowList.AddRange({ControlNames.SketchesShow.ToString, ControlNames.Sketches_3dShow.ToString, ControlNames.CurvesShow.ToString})
        ShowList.AddRange({ControlNames.ConstructionsShow.ToString, ControlNames.SurfacesShow.ToString, ControlNames.PhysicalPropertySymbolsShow.ToString})
        ShowList.AddRange({ControlNames.ReferenceAxesShow.ToString, ControlNames.PMIShow.ToString})

        Dim HideList As New List(Of String)
        HideList.AddRange({ControlNames.AllHide.ToString, ControlNames.ReferencePlanesHide.ToString, ControlNames.CoordinateSystemsHide.ToString})
        HideList.AddRange({ControlNames.SketchesHide.ToString, ControlNames.Sketches_3dHide.ToString, ControlNames.CurvesHide.ToString})
        HideList.AddRange({ControlNames.ConstructionsHide.ToString, ControlNames.SurfacesHide.ToString, ControlNames.PhysicalPropertySymbolsHide.ToString})
        HideList.AddRange({ControlNames.ReferenceAxesHide.ToString, ControlNames.PMIHide.ToString})

        Dim LabelList As New List(Of String)
        LabelList.AddRange({ControlNames.All.ToString, ControlNames.ReferencePlanes.ToString, ControlNames.CoordinateSystems.ToString})
        LabelList.AddRange({ControlNames.Sketches.ToString, ControlNames.Sketches_3d.ToString, ControlNames.Curves.ToString})
        LabelList.AddRange({ControlNames.Constructions.ToString, ControlNames.Surfaces.ToString, ControlNames.PhysicalPropertySymbols.ToString})
        LabelList.AddRange({ControlNames.ReferenceAxes.ToString, ControlNames.PMI.ToString})


        For i As Integer = 0 To ShowList.Count - 1
            Dim ShowName As String = ShowList(i)
            Dim HideName As String = HideList(i)
            Dim LabelName As String = LabelList(i)

            RowIndex += 1

            CheckBox = FormatOptionsCheckBox(ShowName, "")
            AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
            tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
            CheckBox.Anchor = AnchorStyles.None
            ControlsDict(CheckBox.Name) = CheckBox

            CheckBox = FormatOptionsCheckBox(HideName, "")
            AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
            tmpTLPOptions.Controls.Add(CheckBox, 1, RowIndex)
            CheckBox.Anchor = AnchorStyles.None
            ControlsDict(CheckBox.Name) = CheckBox

            Label = FormatOptionsLabel(LabelName, GenerateLabelText(LabelName))
            tmpTLPOptions.Controls.Add(Label, 3, RowIndex)
            ControlsDict(Label.Name) = Label
            If Label.Text = "Sketches_3d" Then Label.Text = "3DSketches"
            If Label.Text = "P m i" Then Label.Text = "PMI"

        Next

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If
        End If

    End Sub


    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.AllShow.ToString
                Me.AllShow = Checkbox.Checked

                If Me.AllShow Then
                    Me.AllHide = False
                    Me.ReferencePlanesShow = True
                    Me.CoordinateSystemsShow = True
                    Me.SketchesShow = True
                    Me.Sketches_3dShow = True
                    Me.CurvesShow = True
                    Me.ConstructionsShow = True
                    Me.SurfacesShow = True
                    Me.PhysicalPropertySymbolsShow = True
                    Me.ReferenceAxesShow = True
                    Me.PMIShow = True
                End If

            Case ControlNames.AllHide.ToString
                Me.AllHide = Checkbox.Checked

                If Me.AllHide Then
                    Me.AllShow = False
                    Me.ReferencePlanesHide = True
                    Me.CoordinateSystemsHide = True
                    Me.SketchesHide = True
                    Me.Sketches_3dHide = True
                    Me.CurvesHide = True
                    Me.ConstructionsHide = True
                    Me.SurfacesHide = True
                    Me.PhysicalPropertySymbolsHide = True
                    Me.ReferenceAxesHide = True
                    Me.PMIHide = True
                End If

            Case ControlNames.ReferencePlanesShow.ToString
                Me.ReferencePlanesShow = Checkbox.Checked

            Case ControlNames.ReferencePlanesHide.ToString
                Me.ReferencePlanesHide = Checkbox.Checked

            Case ControlNames.CoordinateSystemsShow.ToString
                Me.CoordinateSystemsShow = Checkbox.Checked

            Case ControlNames.CoordinateSystemsHide.ToString
                Me.CoordinateSystemsHide = Checkbox.Checked

            Case ControlNames.SketchesShow.ToString
                Me.SketchesShow = Checkbox.Checked

            Case ControlNames.SketchesHide.ToString
                Me.SketchesHide = Checkbox.Checked

            Case ControlNames.Sketches_3dShow.ToString
                Me.Sketches_3dShow = Checkbox.Checked

            Case ControlNames.Sketches_3dHide.ToString
                Me.Sketches_3dHide = Checkbox.Checked

            Case ControlNames.CurvesShow.ToString
                Me.CurvesShow = Checkbox.Checked

            Case ControlNames.CurvesHide.ToString
                Me.CurvesHide = Checkbox.Checked

            Case ControlNames.ConstructionsShow.ToString
                Me.ConstructionsShow = Checkbox.Checked

            Case ControlNames.ConstructionsHide.ToString
                Me.ConstructionsHide = Checkbox.Checked

            Case ControlNames.SurfacesShow.ToString
                Me.SurfacesShow = Checkbox.Checked

            Case ControlNames.SurfacesHide.ToString
                Me.SurfacesHide = Checkbox.Checked

            Case ControlNames.PhysicalPropertySymbolsShow.ToString
                Me.PhysicalPropertySymbolsShow = Checkbox.Checked

            Case ControlNames.PhysicalPropertySymbolsHide.ToString
                Me.PhysicalPropertySymbolsHide = Checkbox.Checked

            Case ControlNames.ReferenceAxesShow.ToString
                Me.ReferenceAxesShow = Checkbox.Checked

            Case ControlNames.ReferenceAxesHide.ToString
                Me.ReferenceAxesHide = Checkbox.Checked

            Case ControlNames.PMIShow.ToString
                Me.PMIShow = Checkbox.Checked

            Case ControlNames.PMIHide.ToString
                Me.PMIHide = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select

    End Sub

    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Hides all non-model elements such as reference planes, PMI dimensions, etc."

        Return HelpString
    End Function


End Class
