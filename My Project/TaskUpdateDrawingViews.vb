Option Strict On

Public Class TaskUpdateDrawingViews

    Inherits Task

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = False
        Me.AppliesToPart = False
        Me.AppliesToSheetmetal = False
        Me.AppliesToDraft = True
        Me.HasOptions = False
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskUpdateDrawingViews
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

        Dim Sheet As SolidEdgeDraft.Sheet = Nothing
        Dim DrawingViews As SolidEdgeDraft.DrawingViews = Nothing
        Dim DrawingView As SolidEdgeDraft.DrawingView = Nothing

        Dim ModelLinks As SolidEdgeDraft.ModelLinks = Nothing
        Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing

        Dim Filename As String

        Dim PerformedUpdate As Boolean = False

        Dim PartsLists As SolidEdgeDraft.PartsLists
        Dim PartsList As SolidEdgeDraft.PartsList

        Dim s As String

        Dim UC As New UtilsCommon

        Dim tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

        ' Check Partslists.
        PartsLists = tmpSEDoc.PartsLists

        ' Not all draft files have PartsLists
        Try
            For Each PartsList In PartsLists
                If Not PartsList.IsUpToDate Then
                    PartsList.Update()
                    If PartsList.IsUpToDate Then
                        PerformedUpdate = True
                    Else
                        TaskLogger.AddMessage("Unable to update parts list")
                    End If
                End If
            Next
        Catch ex As Exception
        End Try

        ' Check for missing model files.
        ModelLinks = tmpSEDoc.ModelLinks

        For Each ModelLink In ModelLinks
            If ModelLink.IsAssemblyFamilyMember Then
                Filename = ModelLink.FileName.Split("!"c)(0)
            Else
                Filename = ModelLink.FileName
            End If

            If Not FileIO.FileSystem.FileExists(Filename) Then
                TaskLogger.AddMessage(String.Format("Model file '{0}' not found", Filename))

            ElseIf ModelLink.ModelOutOfDate Then
                s = String.Format("Model link out of date '{0}'", Filename)
                If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
            End If
        Next

        ' Check drawing views.
        For Each SheetType As String In {"Working", "Background"}
            For Each Sheet In UC.GetSheets(tmpSEDoc, SheetType)
                DrawingViews = Sheet.DrawingViews
                For Each DrawingView In DrawingViews.OfType(Of SolidEdgeDraft.DrawingView)()
                    If Not DrawingView.IsUpToDate Then
                        ' Can fail if the model file is missing.
                        Try
                            DrawingView.Update()
                            If DrawingView.IsUpToDate Then
                                PerformedUpdate = True
                            Else
                                TaskLogger.AddMessage("Unable to update drawing view")
                            End If
                        Catch ex As Exception
                        End Try
                    End If
                Next DrawingView
            Next Sheet
        Next

        If PerformedUpdate Then
            If SEDoc.ReadOnly Then
                TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
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
        HelpString = "Checks drawing views one by one, and updates them if needed."

        Return HelpString
    End Function


End Class
