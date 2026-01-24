Option Strict On

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
        Me.AppliesToAssembly = True
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

        Dim Models As SolidEdgePart.Models = Nothing
        Dim Model As SolidEdgePart.Model
        Dim CopiedParts As SolidEdgePart.CopiedParts
        Dim CopiedPart As SolidEdgePart.CopiedPart


        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case = "asm"
                ' Nothing to do here for now.
                ' Could add an option to activate and update all.

                Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument
                tmpSEDoc = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

                tmpSEDoc.ActivateAll()
                tmpSEDoc.UpdateAll()

                'tmpSEDoc.Save()
                'SEApp.DoIdle()

            Case = "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                Models = tmpSEDoc.Models

            Case = "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                Models = tmpSEDoc.Models

            Case Else
                'MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
                Me.TaskLogger.AddMessage($"{Me.Name} DocType '{DocType}' not recognized")
        End Select

        Dim TF As Boolean

        If Models IsNot Nothing Then
            If (Models.Count > 0) And (Models.Count < 300) Then
                For Each Model In Models
                    CopiedParts = Model.CopiedParts
                    If CopiedParts.Count > 0 Then
                        For Each CopiedPart In CopiedParts
                            If CopiedPart.FileName IsNot Nothing Then
                                TF = FileIO.FileSystem.FileExists(CopiedPart.FileName)
                                TF = TF Or (CopiedPart.FileName = "")  ' Implies no link to outside file
                                If Not TF Then
                                    TaskLogger.AddMessage(String.Format("Part copy file not found: '{0}'", CopiedPart.FileName))
                                Else
                                    If Me.UpdateParents Then
                                        ' Try a recursion

                                        Dim ParentDoc = CType(SEApp.Documents.Open(CopiedPart.FileName), SolidEdgeFramework.SolidEdgeDocument)
                                        ProcessInternal(ParentDoc, SEApp)

                                        ParentDoc.Close()
                                        SEApp.DoIdle()

                                    End If

                                    If Not CopiedPart.IsUpToDate Then
                                        CopiedPart.Update()
                                    End If

                                    If SEDoc.ReadOnly Then
                                        TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
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
                TaskLogger.AddMessage(String.Format("{0} models exceeds maximum to process", Models.Count.ToString))

            End If

        End If

        If SEDoc.ReadOnly Then
            TaskLogger.AddMessage("Cannot save document marked 'Read Only'")

        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 2)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateParents.ToString, "Update parent documents")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If
        End If

    End Sub


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
