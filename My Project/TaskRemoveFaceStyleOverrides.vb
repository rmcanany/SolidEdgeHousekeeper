Option Strict On

Public Class TaskRemoveFaceStyleOverrides
    Inherits Task


    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = True
        Me.AppliesToPart = False
        Me.AppliesToSheetmetal = False
        Me.AppliesToDraft = False
        Me.HasOptions = False
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskRemoveFaceStyleOverrides
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

        Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

        Dim Occurrences As SolidEdgeAssembly.Occurrences = tmpSEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence

        Dim AllOccurrences As New List(Of SolidEdgeAssembly.Occurrence)
        Dim AllOccurrencesNames As New List(Of String)

        Dim UO As New UtilsOccurrences(tmpSEDoc, True)

        For Each Occurrence In UO.AllOccurrences

            ' Fails at 'Occurrence.FaceStyle IsNot Nothing' on some files.  Reason not known.
            Try
                If Occurrence.FaceStyle IsNot Nothing Then
                    Occurrence.PutStyleNone()
                End If
            Catch ex As Exception
                ' No FaceStyle should mean no override.  Error message not warranted.
            End Try
        Next

        For Each SubOccurrence In UO.AllSubOccurrences

            ' Fails at 'Occurrence.FaceStyle IsNot Nothing' on some files.  Reason not known.
            Try
                If SubOccurrence.FaceStyle IsNot Nothing Then
                    SubOccurrence.PutStyleNone()
                End If
            Catch ex As Exception
                ' No FaceStyle should mean no override.  Error message not warranted.
            End Try
        Next

        If SEDoc.ReadOnly Then
            TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

    End Sub


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
        HelpString = "Face style overrides change a part's appearance in the assembly. "
        HelpString += "This command causes the part to appear the same in the part file and the assembly."

        Return HelpString
    End Function


End Class
