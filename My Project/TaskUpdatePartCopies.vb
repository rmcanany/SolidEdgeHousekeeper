﻿Option Strict On

Public Class TaskUpdatePartCopies

    Inherits Task

    Private _UpdateParents As Boolean
    Public Property UpdateParents As Boolean
        Get
            Return _UpdateParents
        End Get
        Set(value As Boolean)
            _UpdateParents = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateParents.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _AutoHideOptions As Boolean
    Public Property AutoHideOptions As Boolean
        Get
            Return _AutoHideOptions
        End Get
        Set(value As Boolean)
            _AutoHideOptions = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AutoHideOptions.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property


    Enum ControlNames
        UpdateParents
        AutoHideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = False
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = False
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskUpdatePartCopies
        Me.Category = "Update"
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.UpdateParents = False

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

        Dim SupplementalExitStatus As Integer = 0
        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Models As SolidEdgePart.Models = Nothing
        Dim Model As SolidEdgePart.Model
        Dim CopiedParts As SolidEdgePart.CopiedParts
        Dim CopiedPart As SolidEdgePart.CopiedPart


        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case = "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                Models = tmpSEDoc.Models

            Case = "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                Models = tmpSEDoc.Models

            Case Else
                MsgBox(String.Format("{0} DocType '{0}' not recognized", Me.Name, DocType))
        End Select

        Dim TF As Boolean

        If Not Models Is Nothing Then
            If (Models.Count > 0) And (Models.Count < 300) Then
                For Each Model In Models
                    CopiedParts = Model.CopiedParts
                    If CopiedParts.Count > 0 Then
                        For Each CopiedPart In CopiedParts
                            If CopiedPart.FileName IsNot Nothing Then
                                TF = FileIO.FileSystem.FileExists(CopiedPart.FileName)
                                TF = TF Or (CopiedPart.FileName = "")  ' Implies no link to outside file
                                If Not TF Then
                                    ExitStatus = 1
                                    ErrorMessageList.Add(String.Format("Part copy file not found: '{0}'", CopiedPart.FileName))
                                Else
                                    If Me.UpdateParents Then
                                        ' Try a recursion

                                        Dim ParentDoc = CType(SEApp.Documents.Open(CopiedPart.FileName), SolidEdgeFramework.SolidEdgeDocument)
                                        SupplementalErrorMessage = ProcessInternal(ParentDoc, Configuration, SEApp)
                                        AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)

                                        ParentDoc.Close()
                                        SEApp.DoIdle()

                                    End If

                                    If Not CopiedPart.IsUpToDate Then
                                        CopiedPart.Update()
                                    End If

                                    If SEDoc.ReadOnly Then
                                        ExitStatus = 1
                                        ErrorMessageList.Add("Cannot save document marked 'Read Only'")
                                    Else
                                        SEDoc.Save()
                                        SEApp.DoIdle()
                                    End If
                                End If

                            End If
                        Next
                    End If
                Next
            ElseIf Models.Count >= 300 Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0} models exceeds maximum to process", Models.Count.ToString))
            End If

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


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        'Dim IU As New InterfaceUtilities

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 2)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateParents.ToString, "Update parent documents")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
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


    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.UpdateParents.ToString
                Me.UpdateParents = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "In conjuction with `Assembly Activate and update all`, "
        HelpString += "used mainly to eliminate the gray corners on assembly drawings. "

        HelpString += vbCrLf + vbCrLf + "![UpdatePartCopies](My%20Project/media/task_update_part_copies.png)"

        HelpString += vbCrLf + vbCrLf + "You can optionally update the parent files recursively "
        HelpString += "by enabling `Update parent documents` on the Options panel."

        HelpString += vbCrLf + vbCrLf + "In many situations, only a few parts have part copies. "
        HelpString += "Using this command with the Dependency Sort option can filter out those files, "
        HelpString += "greatly speeding up processing. "
        HelpString += "See details on the "
        HelpString += "[<ins>**Configuration Tab -- Sorting Page**</ins>](#sorting)."

        Return HelpString
    End Function


End Class
