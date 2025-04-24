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

    Private _StructuredStorageEdit As Boolean
    Public Property StructuredStorageEdit As Boolean
        Get
            Return _StructuredStorageEdit
        End Get
        Set(value As Boolean)
            _StructuredStorageEdit = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.StructuredStorageEdit.ToString), CheckBox).Checked = value
                Me.SolidEdgeRequired = Not value
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
        StructuredStorageEdit
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
        Me.RequiresPropertiesData = True
        Me.Category = "Check"
        SetColorFromCategory(Me)
        Me.SolidEdgeRequired = True  ' Default is so checking the box toggles a property update
        Me.RequiresLinkManagementOrder = True

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.CheckMissingLinks = False
        Me.CheckMisplacedLinks = False
        Me.SourceDirectories = New List(Of String)

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
        ProcessInternal(FileName)
    End Sub

    Private Overloads Sub ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

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

        Dim tmpMissingLinks As New List(Of String)  ' Used to avoid reporting missing links as also misplaced

        For ListIndex = 0 To CheckItems.Count - 1

            If Not CheckOptions(ListIndex) Then
                Continue For
            End If

            CheckItem = CheckItems(ListIndex)

            Dim SubLogger As Logger = TaskLogger.AddLogger(CheckItem)

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
                    SubLogger.AddMessage(s)
                End If

            End If

            ' Perform the checks
            For Each s In LinkFilenames

                s = UC.GetFOAFilename(s)

                If CheckItem = "Missing links" Then
                    If Not FileIO.FileSystem.FileExists(s) Then
                        Path = System.IO.Path.GetDirectoryName(s)
                        Filename = System.IO.Path.GetFileName(s)
                        s = String.Format("{0} in {1}", Filename, Path)

                        SubLogger.AddMessage(s)

                        tmpMissingLinks.Add(s)

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
                        Path = System.IO.Path.GetDirectoryName(s)
                        Filename = System.IO.Path.GetFileName(s)
                        s = String.Format("{0} in {1}", Filename, Path)

                        ' Don't add the file to the misplaced list if it is already listed as missing

                        If Not tmpMissingLinks.Contains(s) Then
                            SubLogger.AddMessage(s)
                        End If

                    End If

                End If
            Next
        Next

    End Sub

    Private Overloads Sub ProcessInternal(ByVal FullName As String)

        Dim Proceed As Boolean = True
        Dim LinkNames As List(Of String)
        Dim BadLinkNames As List(Of String)
        Dim MisplacedLinkNames As New List(Of String)
        Dim Indent As String = "    "
        Dim Directory As String
        Dim Filename As String

        Dim SSDoc As HCStructuredStorageDoc = Nothing

        Dim ShowException As Boolean = False
        If ShowException Then
            SSDoc = New HCStructuredStorageDoc(FullName)
        Else
            Try
                SSDoc = New HCStructuredStorageDoc(FullName)
            Catch ex As Exception
                Proceed = False
                TaskLogger.AddMessage(ex.Message)
            End Try
        End If


        If Proceed Then

            SSDoc.ReadLinks(Me.LinkManagementOrder)
            LinkNames = SSDoc.GetLinkNames
            BadLinkNames = SSDoc.GetBadLinkNames

            If (LinkNames Is Nothing) Or (BadLinkNames Is Nothing) Then
                Proceed = False
                TaskLogger.AddMessage("Unable to read file links")
            End If

            If Proceed Then
                ' Build a list of misplaced links
                For Each LinkName As String In LinkNames
                    If BadLinkNames.Contains(LinkName) Then
                        Continue For
                    End If

                    Dim tf As Boolean = False
                    For Each SourceDirectory In Me.SourceDirectories
                        If LinkName.Contains(SourceDirectory) Then
                            tf = True
                            Exit For
                        End If
                    Next
                    If Not tf Then
                        MisplacedLinkNames.Add(LinkName)
                    End If
                Next

                If Me.CheckMissingLinks Then

                    Dim SubLogger As Logger = TaskLogger.AddLogger("Missing links")

                    If BadLinkNames.Count > 0 Then
                        For Each BadLinkName As String In BadLinkNames
                            Directory = IO.Path.GetDirectoryName(BadLinkName)
                            Filename = IO.Path.GetFileName(BadLinkName)
                            SubLogger.AddMessage(String.Format("{0} in {1}", Filename, Directory))
                        Next
                    End If
                End If

                If Me.CheckMisplacedLinks Then

                    Dim SubLogger As Logger = TaskLogger.AddLogger("Misplaced links")

                    If MisplacedLinkNames.Count > 0 Then
                        For Each MisplacedLinkName As String In MisplacedLinkNames
                            Directory = IO.Path.GetDirectoryName(MisplacedLinkName)
                            Filename = IO.Path.GetFileName(MisplacedLinkName)
                            SubLogger.AddMessage(String.Format("{0} in {1}", Filename, Directory))
                        Next
                    End If
                End If

            End If
        End If

        If SSDoc IsNot Nothing Then SSDoc.Close()

    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

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

        CheckBox = FormatOptionsCheckBox(ControlNames.StructuredStorageEdit.ToString, "Run task without Solid Edge")
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

            If Not (Me.CheckMissingLinks Or Me.CheckMisplacedLinks) Then
                ErrorLogger.AddMessage("Select at least one type of link error to check")
            End If

            If (Me.CheckMisplacedLinks) And (Me.SourceDirectories.Count = 0) Then
                ErrorLogger.AddMessage("Select at least one folder or top-level assembly to assess misplaced links")
            End If

        End If

    End Sub


    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.CheckMissingLinks.ToString
                Me.CheckMissingLinks = Checkbox.Checked

            Case ControlNames.CheckMisplacedLinks.ToString
                Me.CheckMisplacedLinks = Checkbox.Checked

            Case ControlNames.StructuredStorageEdit.ToString
                Me.StructuredStorageEdit = Checkbox.Checked
                Me.SolidEdgeRequired = Not Checkbox.Checked

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
