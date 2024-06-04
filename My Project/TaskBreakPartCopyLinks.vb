Option Strict On

Public Class TaskBreakPartCopyLinks

    Inherits Task
    
    Public Property BreakDesignCopies as Boolean
    Public Property BreakConstructionCopies as Boolean

    Enum ControlNames
        BreakDesignCopies
        BreakConstructionCopies
        HideOptions
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
        Me.Image = My.Resources.TaskCheckPartCopies
        Me.Category = "Edit"

        SetColorFromCategory(Me)

        ' Options
        Me.BreakDesignCopies = False
        Me.BreakConstructionCopies = False
    End Sub

    Public Sub New(Task As TaskBreakPartCopyLinks)

        'Options
        Me.BreakDesignCopies = Task.BreakDesignCopies
        Me.BreakConstructionCopies = Task.BreakConstructionCopies
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

        Dim Models As SolidEdgePart.Models = Nothing
        Dim Model As SolidEdgePart.Model
        Dim CopiedParts As SolidEdgePart.CopiedParts
        Dim CopiedPart As SolidEdgePart.CopiedPart
        Dim CopyConstructions As SolidEdgePart.CopyConstructions = Nothing
        Dim CopyConstruction As SolidEdgePart.CopyConstruction
        Dim FileChanged As Boolean = False

        Dim TC As New Task_Common
        Dim DocType As String = TC.GetDocType(SEDoc)

        Select Case DocType
            Case = "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                Models = tmpSEDoc.Models
                CopyConstructions = tmpSEDoc.Constructions.CopyConstructions

            Case = "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                Models = tmpSEDoc.Models
                CopyConstructions = tmpSEDoc.Constructions.CopyConstructions

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType), vbOKOnly)
        End Select

        If BreakDesignCopies Then
            If Not Models Is Nothing Then
                If (Models.Count > 0) And (Models.Count < 300) Then
                    For Each Model In Models
                        CopiedParts = Model.CopiedParts
                        If CopiedParts.Count > 0 Then
                            For Each CopiedPart In CopiedParts
                                ' Synchronous part copies are not linked.
                                ' Must be ignored or NotImplemented exception will be thrown
                                If CopiedPart.ModelingModeType = 2
                                    If Not CopiedPart.IsBroken Then
                                        FileChanged = True
                                        CopiedPart.BreakLinks()
                                        SEApp.DoIdle()
                                        ' SE will report an out of date link on next open if we don't update
                                        CopiedPart.Update()
                                        SEApp.DoIdle()
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
        End If
        
        If BreakConstructionCopies Then
            If Not CopyConstructions Is Nothing Then
                if (CopyConstructions.Count > 0) And (CopyConstructions.Count < 300) Then
                    For Each CopyConstruction in CopyConstructions
                        ' Synchronous part copies are not links.
                        ' Must be ignored or NotImplemented exception will be thrown
                        If CopyConstruction.ModelingModeType = 2
                            If Not CopyConstruction.IsBroken Then
                                FileChanged = true
                                CopyConstruction.BreakLinks()
                                SEApp.DoIdle()
                                ' SE will report an out of date link on next open if we don' update
                                CopyConstruction.Update()
                                SEApp.DoIdle()
                            End If
                        End If
                    Next
                ElseIf CopyConstructions.Count >= 300 Then
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("{0} models exceeds maximum to process", CopyConstructions.Count.ToString))
                End If
            End If
        End If
        
        If (ExitStatus = 0) And (FileChanged = True) Then
            SEDoc.Save()
            SEApp.DoIdle()
        End If
        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function

    Public Overrides Function GetTLPTask(TLPParent As ExTableLayoutPanel) As ExTableLayoutPanel
        ControlsDict = New Dictionary(Of String, Control)

        Dim IU As New InterfaceUtilities

        Me.TLPTask = IU.BuildTLPTask(Me, TLPParent)
        
        Me.TLPOptions = BuildTLPOptions()

        For Each Control As Control In Me.TLPTask.Controls
            If ControlsDict.Keys.Contains(Control.Name) Then
                MsgBox(String.Format("ControlsDict already has Key '{0}'", Control.Name))
            End If
            ControlsDict(Control.Name) = Control
        Next
        
        Me.TLPTask.Controls.Add(TLPOptions, Me.TLPTask.ColumnCount - 2, 1)

        Return Me.TLPTask
    End Function
    
    Private Function BuildTLPOptions() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        Dim IU As New InterfaceUtilities

        IU.FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.BreakDesignCopies.ToString, "Break design copy links")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.BreakConstructionCopies.ToString, "Break construction copy links")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1
        
        CheckBox = IU.FormatOptionsCheckBox(ControlNames.HideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Private Sub InitializeOptionProperties()
        Dim CheckBox As CheckBox

        CheckBox = CType(ControlsDict(ControlNames.BreakDesignCopies.ToString), CheckBox)
        Me.BreakDesignCopies = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.BreakConstructionCopies.ToString), CheckBox)
        Me.BreakConstructionCopies = CheckBox.Checked
        
        CheckBox = CType(ControlsDict(ControlNames.HideOptions.ToString), CheckBox)
        Me.AutoHideOptions = CheckBox.Checked
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

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.BreakDesignCopies.ToString
                Me.BreakDesignCopies = Checkbox.Checked

            Case ControlNames.BreakConstructionCopies.ToString
                Me.BreakConstructionCopies = Checkbox.Checked
                
            Case ControlNames.HideOptions.ToString
                HandleHideOptionsChange(Me, Me.TLPTask, Me.TLPOptions, Checkbox)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Break design and/or construction part copy links"

        Return HelpString
    End Function


End Class
