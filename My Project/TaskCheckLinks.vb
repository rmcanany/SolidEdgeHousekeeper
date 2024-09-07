Option Strict On

Public Class TaskCheckLinks

    Inherits Task

    Private _CheckMissingLinks As Boolean
    Public Property CheckMissingLinks As Boolean
        Get
            Return _CheckMissingLinks
        End Get
        Set(value As Boolean)
            _CheckMissingLinks = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.CheckMissingLinks.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _CheckMisplacedLinks As Boolean
    Public Property CheckMisplacedLinks As Boolean
        Get
            Return _CheckMisplacedLinks
        End Get
        Set(value As Boolean)
            _CheckMisplacedLinks = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.CheckMisplacedLinks.ToString), CheckBox).Checked = value
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
        CheckMissingLinks
        CheckMisplacedLinks
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
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskCheckLinks
        Me.RequiresSourceDirectories = True
        Me.Category = "Check"
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.CheckMissingLinks = False
        Me.CheckMisplacedLinks = False
        Me.SourceDirectories = New List(Of String)

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

        Dim LinkFilenames As New List(Of String)

        Dim Models As SolidEdgePart.Models = Nothing
        Dim Model As SolidEdgePart.Model
        Dim CopiedParts As SolidEdgePart.CopiedParts
        Dim CopiedPart As SolidEdgePart.CopiedPart

        Dim InputDirectories As New List(Of String)
        Dim SourceDirectory As String
        Dim tf As Boolean
        Dim s As String

        Dim Path As String
        Dim Filename As String

        Dim CheckedOcurrences As New List(Of String)

        Dim ListIndex As Integer

        Dim CheckItems As List(Of String) = {"Missing links", "Misplaced links"}.ToList
        Dim CheckItem As String

        Dim CheckOptions As List(Of Boolean) = {Me.CheckMissingLinks, Me.CheckMisplacedLinks}.ToList


        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        For ListIndex = 0 To CheckItems.Count - 1

            If Not CheckOptions(ListIndex) Then
                Continue For
            End If

            CheckItem = CheckItems(ListIndex)

            ' The Select Case section simply builds a list of files to check.
            Select Case DocType
                Case "asm"
                    Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

                    Dim Occurrences As SolidEdgeAssembly.Occurrences = tmpSEDoc.Occurrences
                    Dim Occurrence As SolidEdgeAssembly.Occurrence
                    Dim OccurrenceOutsideProjectError As Boolean = False

                    For Each Occurrence In Occurrences
                        If Not LinkFilenames.Contains(Occurrence.OccurrenceFileName) Then
                            LinkFilenames.Add(Occurrence.OccurrenceFileName)
                        End If
                    Next

                Case = "par"
                    Dim tmpSEDoc As SolidEdgePart.PartDocument = CType(SEDoc, SolidEdgePart.PartDocument)

                    Models = tmpSEDoc.Models

                Case = "psm"
                    Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)

                    Models = tmpSEDoc.Models

                Case "dft"
                    Dim tmpSEDoc As SolidEdgeDraft.DraftDocument = CType(SEDoc, SolidEdgeDraft.DraftDocument)

                    Dim ModelLinks As SolidEdgeDraft.ModelLinks = Nothing
                    Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing

                    'Dim Filename As String

                    ModelLinks = tmpSEDoc.ModelLinks

                    For Each ModelLink In ModelLinks
                        If ModelLink.IsAssemblyFamilyMember Then
                            Filename = ModelLink.FileName.Split("!"c)(0)
                        Else
                            Filename = ModelLink.FileName
                        End If

                        If Not LinkFilenames.Contains(Filename) Then
                            LinkFilenames.Add(Filename)
                        End If
                    Next

                Case Else
                    MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
            End Select

            ' Build the list for par and psm files.

            If (DocType = "par") Or (DocType = "psm") Then
                If (Models.Count > 0) And (Models.Count < 300) Then
                    For Each Model In Models
                        CopiedParts = Model.CopiedParts
                        If CopiedParts.Count > 0 Then
                            For Each CopiedPart In CopiedParts
                                ' Not all Part Copies have outside links
                                If Not CopiedPart.FileName Is Nothing Then
                                    If Not CopiedPart.FileName = "" Then
                                        If Not LinkFilenames.Contains(CopiedPart.FileName) Then
                                            LinkFilenames.Add(CopiedPart.FileName)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next
                ElseIf Models.Count >= 300 Then
                    s = String.Format("{0} models exceeds maximum to process", Models.Count.ToString)
                    ExitStatus = 1
                    UpdateErrorMessageList(ErrorMessageList, s, True, Me.Description)
                End If

            End If

            ' Perform the checks
            For Each s In LinkFilenames

                s = UC.SplitFOAName(s)("Filename")

                If CheckItem = "Missing links" Then
                    If Not FileIO.FileSystem.FileExists(s) Then
                        ExitStatus = 1
                        Path = System.IO.Path.GetDirectoryName(s)
                        Filename = System.IO.Path.GetFileName(s)
                        s = String.Format("{0} in {1}", Filename, Path)

                        UpdateErrorMessageList(ErrorMessageList, s, True, CheckItem)
                    End If
                End If

                If CheckItem = "Misplaced links" Then
                    tf = False
                    For Each SourceDirectory In Me.SourceDirectories
                        If s.Contains(SourceDirectory) Then
                            tf = True
                            Exit For
                        End If
                    Next

                    If Not tf Then
                        ExitStatus = 1
                        Path = System.IO.Path.GetDirectoryName(s)
                        Filename = System.IO.Path.GetFileName(s)
                        s = String.Format("{0} in {1}", Filename, Path)

                        ' Don't add the file to the misplaced list if it is already listed as missing
                        tf = False
                        For Each tmps As String In ErrorMessageList
                            If tmps.Contains(s) Then
                                tf = True
                                Exit For
                            End If
                        Next
                        If Not tf Then
                            UpdateErrorMessageList(ErrorMessageList, s, True, CheckItem)
                        End If
                    End If

                End If
            Next
        Next

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        'Dim IU As New InterfaceUtilities

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.CheckMissingLinks.ToString, "Check missing links")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.CheckMisplacedLinks.ToString, "Check misplaced links")
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

    Private Sub InitializeOptionProperties()
        Dim CheckBox As CheckBox

        CheckBox = CType(ControlsDict(ControlNames.CheckMissingLinks.ToString), CheckBox)
        Me.CheckMissingLinks = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.CheckMisplacedLinks.ToString), CheckBox)
        Me.CheckMisplacedLinks = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.AutoHideOptions.ToString), CheckBox)
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

            If Not (Me.CheckMissingLinks Or Me.CheckMisplacedLinks) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one type of link error to check", Indent))
            End If

            If (Me.CheckMisplacedLinks) And (Me.SourceDirectories.Count = 0) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one folder or top-level assembly to assess misplaced links", Indent))
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

            Case ControlNames.CheckMissingLinks.ToString
                Me.CheckMissingLinks = Checkbox.Checked

            Case ControlNames.CheckMisplacedLinks.ToString
                Me.CheckMisplacedLinks = Checkbox.Checked

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
        HelpString = "Checks linked files.  "

        HelpString += vbCrLf + vbCrLf + "![CheckLinks](My%20Project/media/task_check_links.png)"

        HelpString += vbCrLf + vbCrLf + "`Missing links` are files not found on disk.  "
        HelpString += "`Misplaced links` are files not contained in the search directories specified on the **Home Tab**.  "
        HelpString += "Only links directly contained in the file are checked.  "
        HelpString += "Links to links are not."

        Return HelpString
    End Function



End Class
