Option Strict On

Public Class TaskCheckFlatPattern

    Inherits Task

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = False
        Me.AppliesToAssembly = False
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = False
        Me.HasOptions = False
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskCheckFlatPattern
        Me.Category = "Check"
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

        Dim UC As New UtilsCommon
        Dim DocType = UC.GetDocType(SEDoc)

        Dim FlatpatternModels As SolidEdgePart.FlatPatternModels = Nothing
        Dim FlatpatternModel As SolidEdgePart.FlatPatternModel

        Select Case DocType
            Case = "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                FlatpatternModels = tmpSEDoc.FlatPatternModels

            Case = "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                FlatpatternModels = tmpSEDoc.FlatPatternModels

            Case Else
                MsgBox(String.Format("{0} DocType '{0}' not recognized", Me.Name, DocType))
        End Select

        '' Active flat environment to regenerate flat model then save part if no errors
        'If ExitStatus = 0 And FlatpatternModels.Count > 0 Then
        '    SEDoc.Activate()
        '    SEApp.DoIdle()
        '    SEApp.StartCommand(CType(SolidEdgeConstants.SheetMetalCommandConstants.SheetMetalToolsSelectTool, SolidEdgeFramework.SolidEdgeCommandConstants))
        '    SEApp.DoIdle()
        '    SEApp.StartCommand(CType(SolidEdgeConstants.SheetMetalCommandConstants.SheetMetalModelFlatPattern, SolidEdgeFramework.SolidEdgeCommandConstants))
        '    SEApp.DoIdle()
        '    'SEDoc.Save()
        '    SEApp.DoIdle()
        'End If


        If Not FlatpatternModels Is Nothing Then
            If FlatpatternModels.Count > 0 Then
                For Each FlatpatternModel In FlatpatternModels
                    If Not FlatpatternModel.IsUpToDate Then
                        TaskLogger.AddMessage("Flat pattern is out of date")
                    End If
                    If Not FlatpatternModel.FlatPatterns.Item(1).Status = SolidEdgePart.FeatureStatusConstants.igFeatureOK Then
                        TaskLogger.AddMessage("Flat pattern is out of date")
                    End If

                    If FlatpatternModel.FlatPatterns.Item(1).ShowDimensions = False Then
                        FlatpatternModel.FlatPatterns.Item(1).ShowDimensions = True
                        FlatpatternModel.UpdateCutSize()
                    End If

                Next
            Else
                TaskLogger.AddMessage("No flat patterns found")
            End If
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
        HelpString = "Checks for the existence of a flat pattern. "
        HelpString += "If one is found, checks if it is up to date. "

        Return HelpString
    End Function


End Class
