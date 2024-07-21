Option Strict On

Public Class TaskUpdateFlatPattern

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
        Me.Image = My.Resources.TaskCheckFlatPattern
        Me.Category = "Update"
        SetColorFromCategory(Me)

        GenerateTaskControl()

        ' Options

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

        Dim TC As New Task_Common
        Dim DocType = TC.GetDocType(SEDoc)

        ' Get FlatpatternModels collection to check if flat patterns exist
        Dim FlatpatternModels As SolidEdgePart.FlatPatternModels = Nothing
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

        If Not SEApp.Visible Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot regenerate flat model in background mode")
        End If

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        End If

        ' Active flat environment to regenerate flat model then save part if no errors
        If ExitStatus = 0 And FlatpatternModels.Count > 0 Then
            SEDoc.Activate()
            SEApp.DoIdle()
            SEApp.StartCommand(CType(SolidEdgeConstants.SheetMetalCommandConstants.SheetMetalToolsSelectTool, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.DoIdle()
            SEApp.StartCommand(CType(SolidEdgeConstants.SheetMetalCommandConstants.SheetMetalModelFlatPattern, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.DoIdle()
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        If ExitStatus = 0 Then
            If FlatpatternModels.Count > 0 Then
                For Each FPM As SolidEdgePart.FlatPatternModel In FlatpatternModels
                    FPM.Update()
                    SEApp.DoIdle()
                    If Not FPM.IsUpToDate Then
                        ExitStatus = 1
                        ErrorMessageList.Add("Unable to update flat pattern")
                    End If
                Next
            Else
                ExitStatus = 1
                ErrorMessageList.Add("No flat patterns found")
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

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
        HelpString = "Regenerates missing flat models by activating flat environment. "

        Return HelpString
    End Function


End Class
