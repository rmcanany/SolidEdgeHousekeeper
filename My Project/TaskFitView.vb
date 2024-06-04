Option Strict On

Public Class TaskFitView

    Inherits Task

    Public Property Isometric As Boolean
    Public Property Dimetric As Boolean
    Public Property Trimetric As Boolean

    Enum ControlNames
        Isometric
        Dimetric
        Trimetric
        HideOptions
    End Enum



    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = True
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskFitView
        Me.Category = "Restyle"

        SetColorFromCategory(Me)

        ' Options
        Me.Isometric = False
        Me.Dimetric = False
        Me.Trimetric = False

    End Sub

    Public Sub New(Task As TaskFitView)

        ' Options
        Me.Isometric = Task.Isometric
        Me.Dimetric = Task.Dimetric
        Me.Trimetric = Task.Trimetric

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
        Dim DocType As String = TC.GetDocType(SEDoc)

        Select Case DocType
            Case = "asm"
                If Me.Isometric Then
                    SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewISOView, SolidEdgeFramework.SolidEdgeCommandConstants))
                End If
                If Me.Dimetric Then
                    SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewDimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))
                End If
                If Me.Trimetric Then
                    SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewTrimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))
                End If

                SEApp.StartCommand(CType(SolidEdgeConstants.AssemblyCommandConstants.AssemblyViewFit, SolidEdgeFramework.SolidEdgeCommandConstants))

            Case = "par", "psm"
                If Me.Isometric Then
                    SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.PartViewISOView, SolidEdgeFramework.SolidEdgeCommandConstants))
                End If
                If Me.Dimetric Then
                    SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.PartViewDimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))
                End If
                If Me.Trimetric Then
                    SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.SheetMetalViewTrimetricView, SolidEdgeFramework.SolidEdgeCommandConstants))
                End If

                SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.PartViewFit, SolidEdgeFramework.SolidEdgeCommandConstants))

            Case = "dft"
                Dim tmpSEDoc As SolidEdgeDraft.DraftDocument = CType(SEDoc, SolidEdgeDraft.DraftDocument)

                Dim SheetWindow As SolidEdgeDraft.SheetWindow
                Dim Sections As SolidEdgeDraft.Sections = Nothing
                Dim Section As SolidEdgeDraft.Section = Nothing
                Dim SectionSheets As SolidEdgeDraft.SectionSheets = Nothing
                Dim Sheet As SolidEdgeDraft.Sheet = Nothing

                Sections = tmpSEDoc.Sections
                Section = Sections.WorkingSection
                SectionSheets = Section.Sheets

                SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)

                For Each Sheet In SectionSheets.OfType(Of SolidEdgeDraft.Sheet)()
                    SheetWindow.ActiveSheet = Sheet
                    SheetWindow.FitEx(SolidEdgeDraft.SheetFitConstants.igFitSheet)
                Next

                SheetWindow.ActiveSheet = SectionSheets.OfType(Of SolidEdgeDraft.Sheet)().ElementAt(0)

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        End Select

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

        ' Initialize
        CType(ControlsDict(ControlNames.Isometric.ToString), CheckBox).Checked = True

        Me.TLPTask.Controls.Add(TLPOptions, Me.TLPTask.ColumnCount - 2, 1)

        Return Me.TLPTask
    End Function

    Private Function BuildTLPOptions() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        Dim IU As New InterfaceUtilities

        IU.FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.Isometric.ToString, "Isometric")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1
        CheckBox = IU.FormatOptionsCheckBox(ControlNames.Dimetric.ToString, "Dimetric")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1
        CheckBox = IU.FormatOptionsCheckBox(ControlNames.Trimetric.ToString, "Trimetric")
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

        CheckBox = CType(ControlsDict(ControlNames.Isometric.ToString), CheckBox)
        Me.Isometric = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.Dimetric.ToString), CheckBox)
        Me.Dimetric = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.Trimetric.ToString), CheckBox)
        Me.Trimetric = CheckBox.Checked

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

            Dim tf As Boolean
            tf = Me.IsSelectedDraft
            tf = tf And Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal)
            tf = tf Or (Me.Isometric Or Me.Dimetric Or Me.Trimetric)

            If Not tf Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select a view orientation", Indent))
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

        Dim ParticipatingCheckBoxes As New List(Of CheckBox)
        ParticipatingCheckBoxes.Add(CType(ControlsDict(ControlNames.Isometric.ToString), CheckBox))
        ParticipatingCheckBoxes.Add(CType(ControlsDict(ControlNames.Dimetric.ToString), CheckBox))
        ParticipatingCheckBoxes.Add(CType(ControlsDict(ControlNames.Trimetric.ToString), CheckBox))

        Select Case Name
            Case ControlNames.Isometric.ToString
                Me.Isometric = Checkbox.Checked
                If Me.Isometric Then
                    HandleMutuallyExclusiveCheckBoxes(TLPOptions, Checkbox, ParticipatingCheckBoxes)
                End If

            Case ControlNames.Dimetric.ToString
                Me.Dimetric = Checkbox.Checked
                If Me.Dimetric Then
                    HandleMutuallyExclusiveCheckBoxes(TLPOptions, Checkbox, ParticipatingCheckBoxes)
                End If

            Case ControlNames.Trimetric.ToString
                Me.Trimetric = Checkbox.Checked
                If Me.Trimetric Then
                    HandleMutuallyExclusiveCheckBoxes(TLPOptions, Checkbox, ParticipatingCheckBoxes)
                End If

            Case ControlNames.HideOptions.ToString '"HideOptions"
                HandleHideOptionsChange(Me, Me.TLPTask, Me.TLPOptions, Checkbox)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Maximizes the window, sets the view orientation for model files, and does a fit. "
        HelpString += "Select the desired orientation on the **Configuration Tab -- General Page**."

        Return HelpString
    End Function



End Class
