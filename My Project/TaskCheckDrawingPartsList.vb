Option Strict On

Public Class TaskCheckDrawingPartsList

    Inherits Task


    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = False
        Me.AppliesToAssembly = False
        Me.AppliesToPart = False
        Me.AppliesToSheetmetal = False
        Me.AppliesToDraft = True
        Me.HasOptions = False
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskCheckDrawingPartsList
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

        Dim tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

        Dim PartsLists As SolidEdgeDraft.PartsLists = tmpSEDoc.PartsLists
        Dim PartsList As SolidEdgeDraft.PartsList

        If PartsLists.Count = 0 Then
            TaskLogger.AddMessage("Parts list missing")

        Else
            For Each PartsList In PartsLists
                If Not PartsList.IsUpToDate Then
                    TaskLogger.AddMessage("Parts list out of date")

                    Exit For
                End If
            Next

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
        HelpString = "Checks is there are any parts list in the drawing and if they are all up to date."

        Return HelpString
    End Function


End Class
