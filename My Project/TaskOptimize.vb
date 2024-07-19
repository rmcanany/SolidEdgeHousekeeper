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
            
            ' Determine the type of document 
            Select Case SEDoc.Type
                Case SolidEdgeFramework.DocumentTypeConstants.igPartDocument
                    Dim SEPartDocument As PartDocument
                    SEPartDocument = CType(SEApp.ActiveDocument, PartDocument)
                    SEModels = SEPartDocument.Models 
                    'Hide Coordinate Systems Icon (This could be an option)
                    SEPartDocument.CoordinateSystems.Visible = False
                    'Hide Reference Planes (This could be an option)
                    SEPartDocument.RefPlanes.Item(1).Visible = False
                    SEPartDocument.RefPlanes.Item(2).Visible = False
                    SEPartDocument.RefPlanes.Item(3).Visible = False
                    'Make Zoom Fit
                    SEApp.StartCommand(SolidEdgeConstants.PartCommandConstants.PartViewFit)
                    'Determine if the first body is in Synchronous Mode
                    Try
                        If CType(SolidEdgePart.ModelingModeConstants.seModelingModeOrdered,Boolean) Then
                            SEPartDocument.ModelingMode = CType(SolidEdgePart.ModelingModeConstants.seModelingModeSynchronous,SolidEdgePart.ModelingModeConstants)
                        End If
                    Catch
                        'The first body are in Ordered Mode, move to Synchronous is needed
                        Dim SEFeatures As SolidEdgePart.Features = Nothing
                        Dim SEFeature As Object = Nothing
                        Dim SEModelPart As SolidEdgePart.Model
                        SEModelPart = SEModels.Item(1)
                        SEFeatures = SEModelPart.Features
                        Dim bIgnoreWarnings As Boolean = True
                        Dim bExtentSelection As Boolean = True
                        Dim aErrorMessages As Array
                        Dim aWarningMessages As Array
                        Dim lNumberOfFeaturesCausingError As Integer
                        Dim lNumberOfFeaturesCausingWarning As Integer
                        For Each SEFeature In SEFeatures
                            aErrorMessages = Array.CreateInstance(GetType(String), 0)
                            aWarningMessages = Array.CreateInstance(GetType(String), 0)
                            Dim dVolumeDifference As Double = 0
                            'MoveToSynchronous in Part Mode have 8 arguments
                            SEPartDocument.MoveToSynchronous(SEFeature, 
                                                             bIgnoreWarnings, 
                                                             bExtentSelection, 
                                                             lNumberOfFeaturesCausingError, 
                                                             aErrorMessages, 
                                                             lNumberOfFeaturesCausingWarning, 
                                                             aWarningMessages, 
                                                             dVolumeDifference)
                        Next
                        SEPartDocument.ModelingMode = CType(SolidEdgePart.ModelingModeConstants.seModelingModeSynchronous,SolidEdgePart.ModelingModeConstants)
                    End Try
                    
                Case SolidEdgeFramework.DocumentTypeConstants.igSheetMetalDocument
                    Dim SEPartDocument As SheetMetalDocument
                    SEPartDocument = CType(SEApp.ActiveDocument, SheetMetalDocument)
                    SEModels = SEPartDocument.Models 
                    'Hide Coordinate Systems Icon (This could be an option)
                    SEPartDocument.CoordinateSystems.Visible = False
                    'Hide Reference Planes (This could be an option)
                    SEPartDocument.RefPlanes.Item(1).Visible = False
                    SEPartDocument.RefPlanes.Item(2).Visible = False
                    SEPartDocument.RefPlanes.Item(3).Visible = False
                    'Make Zoom Fit 
                    SEApp.StartCommand(SolidEdgeConstants.SheetMetalCommandConstants.SheetMetalViewFit)
                    'Determine if the first body is in Synchronous Mode
                    Try
                        If CType(SolidEdgePart.ModelingModeConstants.seModelingModeOrdered,Boolean) Then
                            SEPartDocument.ModelingMode = CType(SolidEdgePart.ModelingModeConstants.seModelingModeSynchronous,SolidEdgePart.ModelingModeConstants)
                        End If
                    Catch
                        'The first body are in Ordered Mode, move to Synchronous is needed
                        Dim SEFeatures As SolidEdgePart.Features = Nothing
                        Dim SEFeature As Object = Nothing
                        Dim SEModelSheetMetal As SolidEdgePart.Model
                        SEModelSheetMetal = SEModels.Item(1)
                        SEFeatures = SEModelSheetMetal.Features
                        Dim bIgnoreWarnings As Boolean = True
                        Dim bExtentSelection As Boolean = True
                        Dim aErrorMessages As Array
                        Dim aWarningMessages As Array
                        Dim lNumberOfFeaturesCausingError As Integer
                        Dim lNumberOfFeaturesCausingWarning As Integer
                        For Each SEFeature In SEFeatures
                            aErrorMessages = Array.CreateInstance(GetType(String), 0)
                            aWarningMessages = Array.CreateInstance(GetType(String), 0)
                            'MoveToSynchronous in SheetMetal Mode have 7 arguments
                            SEPartDocument.MoveToSynchronous(SEFeature, 
                                                             bIgnoreWarnings, 
                                                             bExtentSelection, 
                                                             lNumberOfFeaturesCausingError, 
                                                             aErrorMessages, 
                                                             lNumberOfFeaturesCausingWarning, 
                                                             aWarningMessages)
                        Next
                        SEPartDocument.ModelingMode = CType(SolidEdgePart.ModelingModeConstants.seModelingModeSynchronous,SolidEdgePart.ModelingModeConstants)
                    End Try
            End Select
            
            'Heal and optimize the body
            Dim SEModel As Model
            SEModel = SEModels.Item(1)
            SEModel.HealAndOptimizeBody(False, True)
            SEApp.DoIdle()
            
            'Recognize holes
            Try
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
            Catch
                'Holes not found
            End Try
            
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

