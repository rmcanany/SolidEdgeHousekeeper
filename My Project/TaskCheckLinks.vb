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

    Private _SearchDirectoriesList As List(Of String)
    Public Property SearchDirectories As List(Of String)
        Get
            Return _SearchDirectoriesList
        End Get
        Set(value As List(Of String))
            _SearchDirectoriesList = value

            If Me.TaskOptionsTLP IsNot Nothing And _SearchDirectoriesList IsNot Nothing Then
                Dim tmpDataGridView As DataGridView = CType(ControlsDict(ControlNames.SearchDirectoriesDGV.ToString), DataGridView)
                tmpDataGridView.Rows.Clear()

                Try
                    For i = 0 To _SearchDirectoriesList.Count - 1
                        tmpDataGridView.Rows.Add(_SearchDirectoriesList(i))
                    Next
                Catch ex As Exception
                End Try

                UpdateDGVSize(tmpDataGridView)

                tmpDataGridView.CurrentCell = tmpDataGridView.Rows(tmpDataGridView.Rows.Count - 1).Cells(0)
                tmpDataGridView.ClearSelection()
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


    Private Property ContextMenuTest As ContextMenu
    'Private Property tmpText As String
    Private Property DGVRow As Integer


    Enum ControlNames
        CheckMissingLinks
        CheckMisplacedLinks
        StructuredStorageEdit
        SearchDirectoriesDGV
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
        'Me.RequiresSourceDirectories = True
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
        'Me.SourceDirectories = New List(Of String)

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

        OleMessageFilter.Register()

        Dim LinkFilenames As List(Of String) = GetLinkFilenames(SEDoc)

        Dim MissingLogger As Logger = TaskLogger.AddLogger("Missing links")
        Dim MisplacedLogger As Logger = TaskLogger.AddLogger("Misplaced links")

        CheckLinks(SEDoc, Nothing, LinkFilenames, MissingLogger, MisplacedLogger)

    End Sub

    Private Overloads Sub ProcessInternal(ByVal FullName As String)

        Dim Proceed As Boolean = True

        Dim SSDoc As HCStructuredStorageDoc = Nothing
        Try
            SSDoc = New HCStructuredStorageDoc(FullName, _OpenReadWrite:=False)
            SSDoc.ReadProperties(Me.PropertiesData)
            SSDoc.ReadLinks(Me.LinkManagementOrder)
        Catch ex As Exception
            If SSDoc IsNot Nothing Then SSDoc.Close()
            Proceed = False
            TaskLogger.AddMessage(ex.Message)
        End Try

        If Proceed Then

            Dim LinkFilenames As List(Of String) = SSDoc.GetLinkNames
            If SSDoc IsNot Nothing Then SSDoc.Close()

            If LinkFilenames IsNot Nothing Then
                Dim MissingLogger As Logger = TaskLogger.AddLogger("Missing links")
                Dim MisplacedLogger As Logger = TaskLogger.AddLogger("Misplaced links")

                CheckLinks(Nothing, SSDoc, LinkFilenames, MissingLogger, MisplacedLogger)
            Else
                TaskLogger.AddMessage("Unable to read file links")
            End If

        End If

        If SSDoc IsNot Nothing Then SSDoc.Close()

    End Sub


    Private Function MaybeEvaluateExpression(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        SSDoc As HCStructuredStorageDoc,
        SearchDirectory As String) As String

        Dim OutString As String = ""

        Dim Success As Boolean = True
        Dim UC As New UtilsCommon
        Dim UFC As New UtilsFilenameCharmap

        If SearchDirectory.StartsWith("EXPRESSION_") Or SearchDirectory.StartsWith("SavedSetting:") Then
            If SEDoc IsNot Nothing Then
                OutString = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, SearchDirectory, Me.PropertiesData, TaskLogger, IsExpression:=True)
            Else
                OutString = SSDoc.SubstitutePropertyFormulas(SearchDirectory, TaskLogger, IsExpression:=True)
            End If
            OutString = OutString.Replace(vbCrLf, "")

            If OutString Is Nothing OrElse OutString.ToLower.Contains("<nothing>") Then
                Success = False
                Me.TaskLogger.AddMessage(String.Format("Could not parse search directory expression '{0}'", SearchDirectory))
            Else
                Dim DoNotSubstituteChars As New List(Of String)
                DoNotSubstituteChars.Add("\")
                DoNotSubstituteChars.Add(":")
                OutString = UFC.SubstituteIllegalCharacters(OutString, DoNotSubstituteChars)
            End If

        Else
            OutString = SearchDirectory

        End If

        If Success Then
            Return OutString
        Else
            Return Nothing
        End If


        Return OutString
    End Function

    'Private Function MaybeEvaluateExpression(
    '    SSDoc As HCStructuredStorageDoc,
    '    SearchDirectory As String) As String

    '    Dim OutString As String = ""

    '    Dim Success As Boolean = True
    '    Dim UC As New UtilsCommon
    '    Dim UFC As New UtilsFilenameCharmap

    '    If SearchDirectory.StartsWith("EXPRESSION_") Or SearchDirectory.StartsWith("SavedSetting:") Then
    '        'OutString = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, SearchDirectory, Me.PropertiesData, TaskLogger, True)
    '        OutString = SSDoc.SubstitutePropertyFormulas(SearchDirectory, TaskLogger, IsExpression:=True)
    '        OutString = OutString.Replace(vbCrLf, "")

    '        If OutString Is Nothing OrElse OutString.ToLower.Contains("<nothing>") Then
    '            Success = False
    '            Me.TaskLogger.AddMessage(String.Format("Could not parse search directory expression '{0}'", SearchDirectory))
    '        Else
    '            Dim DoNotSubstituteChars As New List(Of String)
    '            DoNotSubstituteChars.Add("\")
    '            DoNotSubstituteChars.Add(":")
    '            OutString = UFC.SubstituteIllegalCharacters(OutString, DoNotSubstituteChars)
    '        End If

    '    Else
    '        OutString = SearchDirectory

    '    End If

    '    If Success Then
    '        Return OutString
    '    Else
    '        Return Nothing
    '    End If


    '    Return OutString
    'End Function

    Private Function GetLinkFilenames(SEDoc As SolidEdgeFramework.SolidEdgeDocument) As List(Of String)
        Dim LinkFilenames As New List(Of String)

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)
        Dim Models As SolidEdgePart.Models = Nothing


        Select Case DocType
            Case "asm"
                Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

                For Each Occurrence As SolidEdgeAssembly.Occurrence In tmpSEDoc.Occurrences
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

                For Each ModelLink As SolidEdgeDraft.ModelLink In tmpSEDoc.ModelLinks
                    Dim tmpFilename As String
                    If ModelLink.IsAssemblyFamilyMember Then
                        tmpFilename = ModelLink.FileName.Split("!"c)(0)
                    Else
                        tmpFilename = ModelLink.FileName
                    End If

                    If Not LinkFilenames.Contains(tmpFilename) Then
                        LinkFilenames.Add(tmpFilename)
                    End If
                Next

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        End Select

        ' Build the list for par and psm files.

        If (DocType = "par") Or (DocType = "psm") Then
            If (Models.Count > 0) And (Models.Count < 300) Then
                For Each Model As SolidEdgePart.Model In Models
                    Dim CopiedParts As SolidEdgePart.CopiedParts = Model.CopiedParts
                    If CopiedParts.Count > 0 Then
                        For Each CopiedPart As SolidEdgePart.CopiedPart In CopiedParts
                            ' Not all Part Copies have outside links
                            If CopiedPart.FileName IsNot Nothing Then
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
                TaskLogger.AddMessage($"{Models.Count.ToString} models exceeds maximum to process")
            End If
        End If


        Return LinkFilenames
    End Function

    Private Sub CheckLinks(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        SSDoc As HCStructuredStorageDoc,
        LinkFilenames As List(Of String),
        MissingLogger As Logger,
        MisplacedLogger As Logger)

        For Each LinkFilename As String In LinkFilenames
            If Not IO.File.Exists(LinkFilename) Then
                If Me.CheckMissingLinks Then
                    Dim tmpDir = System.IO.Path.GetDirectoryName(LinkFilename)
                    Dim tmpFilename = System.IO.Path.GetFileName(LinkFilename)
                    MissingLogger.AddMessage($"{tmpFilename} in {tmpDir}")
                End If
            Else
                If Me.CheckMisplacedLinks Then
                    Dim InSearchDirectory As Boolean = False
                    For Each SearchDirectory As String In Me.SearchDirectories
                        'If SEDoc IsNot Nothing Then
                        '    SearchDirectory = MaybeEvaluateExpression(SEDoc, SearchDirectory)
                        'Else
                        '    SearchDirectory = MaybeEvaluateExpression(SSDoc, SearchDirectory)
                        'End If
                        SearchDirectory = MaybeEvaluateExpression(SEDoc, SSDoc, SearchDirectory)
                        If SearchDirectory IsNot Nothing AndAlso LinkFilename.ToLower.Contains(SearchDirectory.ToLower) Then
                            InSearchDirectory = True
                            Exit For
                        End If
                    Next
                    If Not InSearchDirectory Then
                        Dim tmpDir = System.IO.Path.GetDirectoryName(LinkFilename)
                        Dim tmpFilename = System.IO.Path.GetFileName(LinkFilename)
                        MisplacedLogger.AddMessage($"{tmpFilename} in {tmpDir}")
                    End If

                End If
            End If
        Next

    End Sub


    Public Sub UpdateDGVSize(DGV As DataGridView)
        DGV.Height = (DGV.Rows(0).Height + 1) * (DGV.Rows.Count + 2)
    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim DataGridView As DataGridView
        Dim ColumnHeaders As List(Of String)
        Dim ColumnType As String
        If Me.SearchDirectories Is Nothing Then Me.SearchDirectories = New List(Of String)

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        Me.ContextMenuTest = New ContextMenu
        Me.ContextMenuTest.MenuItems.Add("Add directory", New EventHandler(AddressOf AddDirectory))
        Me.ContextMenuTest.MenuItems.Add("Insert expression", New EventHandler(AddressOf InsertExpression))
        Me.ContextMenuTest.MenuItems.Add("Edit expression", New EventHandler(AddressOf EditExpression))


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

        'https://stackoverflow.com/questions/1718389/right-click-context-menu-for-datagridview

        ColumnHeaders = {"Search directories"}.ToList
        ColumnType = "Textbox"
        DataGridView = FormatOptionsDataGridView(ControlNames.SearchDirectoriesDGV.ToString, ColumnHeaders, ColumnType, Me.SearchDirectories)
        'DataGridView.ContextMenuStrip = Me.TaskControl.ContextMenuStripTaskCheckLinks
        DataGridView.Margin = New Padding(15, 0, 0, 0)
        AddHandler DataGridView.CellClick, AddressOf DataGridViewOptions_CellClick
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
        AddHandler DataGridView.DataError, AddressOf DataGridViewOptions_DataError
        AddHandler DataGridView.MouseClick, AddressOf DataGridViewOptions_MouseClick
        tmpTLPOptions.Controls.Add(DataGridView, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(DataGridView, 2)
        For i = 0 To ColumnHeaders.Count - 1
            DataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        DataGridView.Height = (DataGridView.Rows(0).Height + 1) * (DataGridView.Rows.Count + 2)
        ControlsDict(DataGridView.Name) = DataGridView
        DataGridView.Visible = False

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

            If (Me.CheckMisplacedLinks) And (Me.SearchDirectories.Count = 0) Then
                ErrorLogger.AddMessage("Select at least one folder or top-level assembly to assess misplaced links")
            End If

            If Me.StructuredStorageEdit Then
                If Me.LinkManagementOrder Is Nothing OrElse LinkManagementOrder.Count = 0 Then
                    ErrorLogger.AddMessage("Populate LinkMgmt.txt file name on the Configuration Tab -- Templates page")
                End If

            End If
        End If

    End Sub


    Private Sub DataGridViewOptions_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs)

        Dim DataGridView = CType(sender, DataGridView)

        If e.Button = MouseButtons.Right Then
            'Dim m As ContextMenu = New ContextMenu()
            'Dim m As ContextMenuStrip = Me.TaskControl.ContextMenuStripTaskCheckLinks
            Dim m As ContextMenu = Me.ContextMenuTest
            'm.MenuItems.Add(New MenuItem("Cut"))
            'm.MenuItems.Add(New MenuItem("Copy"))
            'm.MenuItems.Add(New MenuItem("Paste"))
            'Dim currentMouseOverRow As Integer = DataGridView.HitTest(e.X, e.Y).RowIndex
            DGVRow = DataGridView.HitTest(e.X, e.Y).RowIndex

            If DGVRow >= 0 Then
                'm.MenuItems.Add(New MenuItem(String.Format("Do something to row {0}", currentMouseOverRow.ToString())))
            End If

            m.Show(DataGridView, New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub AddDirectory(ByVal sender As Object, ByVal e As EventArgs)

        Dim tmpFolderDialog As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog
        tmpFolderDialog.IsFolderPicker = True

        tmpFolderDialog.InitialDirectory = Form_Main.WorkingFilesPath

        If tmpFolderDialog.ShowDialog() = DialogResult.OK Then

            Dim NewDir As String = tmpFolderDialog.FileName

            Dim DataGridView As DataGridView = CType(ControlsDict(ControlNames.SearchDirectoriesDGV.ToString), DataGridView)
            DataGridView.CurrentCell = DataGridView.Rows(DGVRow).Cells(0)
            DataGridView.BeginEdit(True)
            Dim TextBox As TextBox = CType(DataGridView.EditingControl, TextBox)
            TextBox.Text = NewDir
            DataGridView.EndEdit()

            UpdateDGVSize(DataGridView)

            Form_Main.WorkingFilesPath = IO.Directory.GetParent(NewDir.TrimEnd(IO.Path.DirectorySeparatorChar)).ToString

        End If

    End Sub

    Private Sub InsertExpression(ByVal sender As Object, ByVal e As EventArgs)

        Dim FES As New FormExpressionSelector

        If FES.ShowDialog() = DialogResult.OK Then
            Form_Main.ExpressionEditorLanguage = FES.SavedExpresssionLanguage

            Dim ExpressionText As String = FES.OutputText

            Dim DataGridView As DataGridView = CType(ControlsDict(ControlNames.SearchDirectoriesDGV.ToString), DataGridView)
            DataGridView.CurrentCell = DataGridView.Rows(DGVRow).Cells(0)
            DataGridView.BeginEdit(True)
            Dim TextBox As TextBox = CType(DataGridView.EditingControl, TextBox)
            TextBox.Text = ExpressionText
            DataGridView.EndEdit()

            UpdateDGVSize(DataGridView)
        End If

    End Sub

    Private Sub EditExpression(ByVal sender As Object, ByVal e As EventArgs)

        Dim DataGridView As DataGridView = CType(ControlsDict(ControlNames.SearchDirectoriesDGV.ToString), DataGridView)
        DataGridView.CurrentCell = DataGridView.Rows(DGVRow).Cells(0)

        Dim FEE As New FormExpressionEditor

        FEE.InputText = DataGridView.Rows(DGVRow).Cells(0).Value.ToString

        Select Case Form_Main.ExpressionEditorLanguage
            Case "VB"
                FEE.TextEditorFormula.Language = FastColoredTextBoxNS.Language.VB
            Case "NCalc"
                FEE.TextEditorFormula.Language = FastColoredTextBoxNS.Language.SQL
            Case Else
                MsgBox($"UCTaskControl: Unrecognized expression editor language '{Form_Main.ExpressionEditorLanguage}'", vbOKOnly)
                Exit Sub
        End Select

        If FEE.ShowDialog() = DialogResult.OK Then
            If Not FEE.OutputText = "" Then

                Select Case FEE.TextEditorFormula.Language
                    Case FastColoredTextBoxNS.Language.VB
                        Form_Main.ExpressionEditorLanguage = "VB"
                    Case FastColoredTextBoxNS.Language.SQL
                        Form_Main.ExpressionEditorLanguage = "NCalc"
                End Select

                Dim ExpressionText As String = FEE.OutputText

                'Dim DataGridView As DataGridView = CType(ControlsDict(ControlNames.SearchDirectoriesDGV.ToString), DataGridView)
                DataGridView.CurrentCell = DataGridView.Rows(DGVRow).Cells(0)
                DataGridView.BeginEdit(True)
                Dim TextBox As TextBox = CType(DataGridView.EditingControl, TextBox)
                TextBox.Text = ExpressionText
                DataGridView.EndEdit()

                UpdateDGVSize(DataGridView)
            End If

        End If

        DataGridView.CurrentCell = Nothing

    End Sub


    Public Sub DataGridViewOptions_Leave(sender As System.Object, e As System.EventArgs)

        Dim DataGridView = CType(sender, DataGridView)

        DataGridView.CommitEdit(DataGridViewDataErrorContexts.LeaveControl)
        DataGridView.EndEdit()

        Select Case DataGridView.Name

            Case ControlNames.SearchDirectoriesDGV.ToString
                Dim tmpSearchDirectoriesList As New List(Of String)

                ' ###### Remove blank rows ######

                For RowIdx = DataGridView.Rows.Count - 1 To 0 Step -1
                    If DataGridView.Rows(RowIdx).IsNewRow Then Continue For

                    Dim SearchDirectory As String = ""
                    Try
                        SearchDirectory = CStr(DataGridView.Rows(RowIdx).Cells(0).Value).Trim
                    Catch ex As Exception
                        SearchDirectory = ""
                    End Try

                    If SearchDirectory = "" Then
                        DataGridView.Rows.RemoveAt(RowIdx)
                    End If
                Next

                ' ###### Update Me.SearchDirectoriesList ######
                For RowIdx = 0 To DataGridView.Rows.Count - 1
                    If DataGridView.Rows(RowIdx).IsNewRow Then Continue For

                    tmpSearchDirectoriesList.Add(CStr(DataGridView.Rows(RowIdx).Cells(0).Value).Trim)
                Next

                Me.SearchDirectories = tmpSearchDirectoriesList

                UpdateDGVSize(DataGridView)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Private Sub DataGridViewOptions_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)

    End Sub

    Private Sub DataGridViewOptions_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        ' https://stackoverflow.com/questions/3207420/datagridview-editmode-editonenter-how-to-select-the-row-to-delete-it

        ' Toggles the DGV EditMode so the row headers can be used to delete a row

        Dim tmpDataGridView As DataGridView = CType(sender, DataGridView)

        UpdateDGVSize(tmpDataGridView)

        If e.ColumnIndex = -1 Then  ' Row header column
            tmpDataGridView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            tmpDataGridView.EndEdit()
        ElseIf tmpDataGridView.EditMode <> DataGridViewEditMode.EditOnEnter Then
            tmpDataGridView.EditMode = DataGridViewEditMode.EditOnEnter
            tmpDataGridView.BeginEdit(False)
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

                CType(ControlsDict(ControlNames.SearchDirectoriesDGV.ToString), DataGridView).Visible = Checkbox.Checked

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
        HelpString += "`Misplaced links` are files not contained in the search directories.  "
        HelpString += "Only links directly contained in the file are checked.  "
        HelpString += "Links to links are not."

        HelpString += vbCrLf + vbCrLf + "To add a directory to the `Missing links` search, "
        HelpString += "right-click in the `Search directories table` and select `Add directory`.  "
        HelpString += "You have to have at least one.  "

        HelpString += vbCrLf + vbCrLf + "There are cases when a directory may need to change based on the file being processed.  "
        HelpString += "In that case, you can use one or more `expressions`.  "
        HelpString += "(See the `Edit properties` help topic for details on their use).  "
        HelpString += "The following example shows how to use VB to specify the parent directory of the file being processed.  "

        HelpString += vbCrLf + vbCrLf + $"```{vbCrLf}"
        HelpString += $"Dim FullName as string = ""%{{System.File Name (full path)}}""{vbCrLf}"
        HelpString += $"Dim DirectoryName as string = IO.Path.GetDirectoryName(FullName){vbCrLf}"
        HelpString += $"Return DirectoryName{vbCrLf}"
        HelpString += "```"

        HelpString += vbCrLf + vbCrLf + "There are a couple of things to know about working with the table.  "

        HelpString += vbCrLf + vbCrLf + "First, if you click a cell, followed by a right-click, it brings up the wrong shortcut.  "
        HelpString += "The cell has to be unselected to work properly.  "
        HelpString += "You can click any other control on the form to clear the selection.  "
        HelpString += "Then go back to the cell and right-click first.  "

        HelpString += vbCrLf + vbCrLf + "Second, to remove a row's contents, "
        HelpString += "select the `Row Header` (the gray box left of the text) and hit `Delete`. "
        HelpString += "To clear the entire list, select the top-most `Row Header` and do the same.  "

        Return HelpString
    End Function

End Class
