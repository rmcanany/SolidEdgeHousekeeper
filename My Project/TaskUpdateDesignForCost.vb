Option Strict On

Public Class TaskUpdateDesignForCost

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
        Me.Image = My.Resources.TaskUpdateDesignForCost
        Me.Category = "Update"
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

        If DocType = "psm" Then
            SEApp.StartCommand(CType(11805, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.DoIdle()
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
        HelpString = "Updates DesignForCost and saves the document."
        HelpString += vbCrLf + vbCrLf + "An annoyance of this command is that it opens the "
        HelpString += "DesignForCost Edgebar pane, but is not able to close it. "
        HelpString += "The user must manually close the pane in an interactive Sheetmetal session. "
        HelpString += "The state of the pane is system-wide, not per-document, "
        HelpString += "so closing it is a one-time action. "

        Return HelpString
    End Function


End Class
