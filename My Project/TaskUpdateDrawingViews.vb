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

        ' Options
    End Sub

    Public Sub New(Task As TaskUpdateDrawingViews)

        'Options

    End Sub


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

    Private Function ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

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

        Dim TC As New Task_Common

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
                        ExitStatus = 1
                        ErrorMessageList.Add("Unable to update parts list")
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
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Model file '{0}' not found", Filename))

            ElseIf ModelLink.ModelOutOfDate Then
                ExitStatus = 1
                s = String.Format("Model link out of date '{0}'", Filename)
                If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
            End If
        Next

        ' Check drawing views.
        'For Each Sheet In SectionSheets.OfType(Of SolidEdgeDraft.Sheet)()
        For Each Sheet In TC.GetSheets(tmpSEDoc, "Working")
            DrawingViews = Sheet.DrawingViews
            For Each DrawingView In DrawingViews.OfType(Of SolidEdgeDraft.DrawingView)()
                If Not DrawingView.IsUpToDate Then
                    ' Can fail if the model file is missing.
                    Try
                        DrawingView.Update()
                        If DrawingView.IsUpToDate Then
                            PerformedUpdate = True
                        Else
                            ExitStatus = 1
                            ErrorMessageList.Add("Unable to update drawing view")
                        End If
                    Catch ex As Exception
                    End Try
                End If
                'Try
                '    If DrawingView.ModelLink IsNot Nothing Then
                '        ModelLink = CType(DrawingView.ModelLink, SolidEdgeDraft.ModelLink)
                '        If ModelLink.ModelOutOfDate Then
                '            ExitStatus = 1
                '            s = "Drawing views out of date"
                '            If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                '        End If
                '    End If
                'Catch ex As Exception
                'End Try
            Next DrawingView
        Next Sheet

        If PerformedUpdate Then
            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function


    Public Overrides Function GetTLPTask(TLPParent As ExTableLayoutPanel) As ExTableLayoutPanel
        ControlsDict = New Dictionary(Of String, Control)

        Dim IU As New InterfaceUtilities

        Me.TLPTask = IU.BuildTLPTask(Me, TLPParent)

        For Each Control As Control In Me.TLPTask.Controls
            If ControlsDict.Keys.Contains(Control.Name) Then
                MsgBox(String.Format("ControlsDict already has Key '{0}'", Control.Name))
            End If
            ControlsDict(Control.Name) = Control
        Next

        Return Me.TLPTask
    End Function

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
        HelpString = "Checks drawing views one by one, and updates them if needed."

        Return HelpString
    End Function


End Class
