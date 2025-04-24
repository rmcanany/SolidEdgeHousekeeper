Option Strict On

Public Class TaskHideConstructions

    Inherits Task

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = True
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = False
        Me.HasOptions = False
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskHideConstructions
        Me.Category = "Restyle"
        SetColorFromCategory(Me)

        GenerateTaskControl()

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
                    SEApp.StartCommand(CType(40080, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Sketches
                    SEApp.StartCommand(CType(40081, SolidEdgeFramework.SolidEdgeCommandConstants)) 'Hide Reference Axes
                    SEApp.StartCommand(CType(40082, SolidEdgeFramework.SolidEdgeCommandConstants))
                    SEApp.StartCommand(CType(40083, SolidEdgeFramework.SolidEdgeCommandConstants))
                    SEApp.StartCommand(CType(40084, SolidEdgeFramework.SolidEdgeCommandConstants))
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

        If SEDoc.ReadOnly Then
            TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

    End Sub


    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If
        End If

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Hides all non-model elements such as reference planes, PMI dimensions, etc."

        Return HelpString
    End Function


End Class
