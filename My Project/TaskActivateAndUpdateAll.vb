Option Strict On

Public Class TaskActivateAndUpdateAll
    Inherits Task

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GenerateHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = True
        Me.AppliesToPart = False
        Me.AppliesToSheetmetal = False
        Me.AppliesToDraft = False
        Me.HasOptions = False
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskActivateAndUpdateAll
        Me.Category = "Update"
        SetColorFromCategory(Me)

        GenerateTaskControl()

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


        If UC.GetDocType(SEDoc) = "asm" Then
            Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument
            tmpSEDoc = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

            tmpSEDoc.ActivateAll()
            tmpSEDoc.UpdateAll()
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


    Private Function GenerateHelpText() As String
        Dim HelpString As String
        HelpString = "Loads all assembly occurrences' geometry into memory and does an update. "
        HelpString += "Used mainly to eliminate the gray corners on assembly drawings. "
        HelpString += vbCrLf + vbCrLf + "Can run out of memory for very large assemblies."

        Return HelpString
    End Function



End Class
