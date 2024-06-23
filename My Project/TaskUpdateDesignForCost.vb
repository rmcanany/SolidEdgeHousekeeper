Option Strict On
Imports SolidEdgeConstants
Imports SolidEdgeFramework

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

    'Public Sub New(Task As TaskUpdateDesignForCost)

    '    'Options

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

        Dim TC As New Task_Common
        Dim DocType = TC.GetDocType(SEDoc)

        If DocType = "psm" Then
            SEApp.StartCommand(CType(11805, SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.DoIdle()
        End If

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
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
        HelpString = "Updates DesignForCost and saves the document."
        HelpString += vbCrLf + vbCrLf + "An annoyance of this command is that it opens the "
        HelpString += "DesignForCost Edgebar pane, but is not able to close it. "
        HelpString += "The user must manually close the pane in an interactive Sheetmetal session. "
        HelpString += "The state of the pane is system-wide, not per-document, "
        HelpString += "so closing it is a one-time action. "

        Return HelpString
    End Function


End Class
