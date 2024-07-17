Option Strict On

Imports SolidEdgeCommunity.Extensions
Imports SolidEdgePart

Public Class TaskOptimize
    Inherits Task

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = False
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = False
        Me.HasOptions = False
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskOptimize
        Me.Category = "Update"
        SetColorFromCategory(Me)

        GenerateTaskControl()

        ' Options

    End Sub

    'Public Sub New(Task As TaskOptimize)

    '    ' Options

    'End Sub


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

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
            Dim SEModels As Models = Nothing
            
            ' Make: zoom fit, hide coordinate system and hide reference planes 
            Select Case SEDoc.Type
                Case SolidEdgeFramework.DocumentTypeConstants.igPartDocument
                    Dim SEPartDocument As PartDocument
                    SEPartDocument = CType(SEApp.ActiveDocument, PartDocument)
                    If CType(ModelingModeConstants.seModelingModeOrdered,Boolean) Then
                        SEPartDocument.ModelingMode = CType(ModelingModeConstants.seModelingModeSynchronous,ModelingModeConstants)
                    End If
                    SEModels = SEPartDocument.Models 
                    SEPartDocument.CoordinateSystems.Visible = False
                    SEPartDocument.RefPlanes.Item(1).Visible = False
                    SEPartDocument.RefPlanes.Item(2).Visible = False
                    SEPartDocument.RefPlanes.Item(3).Visible = False
                    SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.PartViewFit,SolidEdgeFramework.SolidEdgeCommandConstants))
                Case SolidEdgeFramework.DocumentTypeConstants.igSheetMetalDocument
                    Dim SEPartDocument As SheetMetalDocument
                    SEPartDocument = CType(SEApp.ActiveDocument, SheetMetalDocument)
                    If CType(ModelingModeConstants.seModelingModeOrdered,Boolean) Then
                        SEPartDocument.ModelingMode = CType(ModelingModeConstants.seModelingModeSynchronous,ModelingModeConstants)
                    End If
                    SEModels = SEPartDocument.Models 
                    SEPartDocument.CoordinateSystems.Visible = False
                    SEPartDocument.RefPlanes.Item(1).Visible = False
                    SEPartDocument.RefPlanes.Item(2).Visible = False
                    SEPartDocument.RefPlanes.Item(3).Visible = False
                    SEApp.StartCommand(CType(SolidEdgeConstants.SheetMetalCommandConstants.SheetMetalViewFit, SolidEdgeFramework.SolidEdgeCommandConstants))
            End Select
            
            'Heal and optimize the body
            Dim SEModel As Model
            SEModel = SEModels.Item(1)
            SEModel.HealAndOptimizeBody(False, True)
            SEApp.DoIdle()
            
            'Recognize holes (Only work if the body are in Synchronous Mode)
            SEModel = SEModels.Item(1)
            Dim numBodies As Integer = 1
            Dim SEModelBody As SolidEdgeGeometry.Body
            SEModelBody = CType(SEModel.Body, SolidEdgeGeometry.Body)
            Dim SEBodies As Array
            SEBodies = New SolidEdgeGeometry.Body(0) {SEModelBody}
            Dim numHoles As Integer = 1
            Dim SERecognizedHoles As Array
            SERecognizedHoles = New Object() {}
            SEModel.Holes.RecognizeAndCreateHoleGroups(numBodies, SEBodies, numHoles, SERecognizedHoles) 
            SEApp.DoIdle()
            SEModel.Recompute()
            
            'Finish in Ordered Mode ready to work (This could be an option)
            Select Case SEDoc.Type
                Case SolidEdgeFramework.DocumentTypeConstants.igPartDocument
                    Dim SEPartDocument As PartDocument
                    SEPartDocument = CType(SEApp.ActiveDocument, PartDocument)
                    If CType(ModelingModeConstants.seModelingModeSynchronous,Boolean) Then
                        SEPartDocument.ModelingMode = CType(ModelingModeConstants.seModelingModeOrdered,ModelingModeConstants)
                    End If
                Case SolidEdgeFramework.DocumentTypeConstants.igSheetMetalDocument
                    Dim SEPartDocument As SheetMetalDocument
                    SEPartDocument = CType(SEApp.ActiveDocument, SheetMetalDocument)
                    If CType(ModelingModeConstants.seModelingModeSynchronous,Boolean) Then
                        SEPartDocument.ModelingMode = CType(ModelingModeConstants.seModelingModeOrdered,ModelingModeConstants)
                    End If
            End Select
            
            'Save file
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    'Public Overrides Function GetTLPTask(TLPParent As ExTableLayoutPanel) As ExTableLayoutPanel
    '    ControlsDict = New Dictionary(Of String, Control)

    '    Dim IU As New InterfaceUtilities

    '    Me.TLPTask = IU.BuildTLPTask(Me, TLPParent)

    '    For Each Control As Control In Me.TLPTask.Controls
    '        If ControlsDict.Keys.Contains(Control.Name) Then
    '            MsgBox(String.Format("ControlsDict already has Key '{0}'", Control.Name))
    '        End If
    '        ControlsDict(Control.Name) = Control
    '    Next

    '    Return Me.TLPTask
    'End Function

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

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Open a document, optimize faces, recognize hole and save in the current version." 'NUEVO

        Return HelpString
    End Function

End Class
