Option Strict On
Imports System.IO

Public Class TaskUpdateBlocks
    Inherits Task

    Private _BlockLibrary As String
    Public Property BlockLibrary As String
        Get
            Return _BlockLibrary
        End Get
        Set(value As String)
            _BlockLibrary = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BlockLibrary.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _ReplaceBlocks As Boolean
    Public Property ReplaceBlocks As Boolean
        Get
            Return _ReplaceBlocks
        End Get
        Set(value As Boolean)
            _ReplaceBlocks = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ReplaceBlocks.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _DeleteBlocks As Boolean
    Public Property DeleteBlocks As Boolean
        Get
            Return _DeleteBlocks
        End Get
        Set(value As Boolean)
            _DeleteBlocks = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DeleteBlocks.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _AddBlocks As Boolean
    Public Property AddBlocks As Boolean
        Get
            Return _AddBlocks
        End Get
        Set(value As Boolean)
            _AddBlocks = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AddBlocks.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _ReportMissingSheet As Boolean
    Public Property ReportMissingSheet As Boolean
        Get
            Return _ReportMissingSheet
        End Get
        Set(value As Boolean)
            _ReportMissingSheet = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ReportMissingSheet.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _ReplaceBlocksList As List(Of String)
    Public Property ReplaceBlocksList As List(Of String)
        Get
            Return _ReplaceBlocksList
        End Get
        Set(value As List(Of String))
            _ReplaceBlocksList = value

            If Me.TaskOptionsTLP IsNot Nothing And _ReplaceBlocksList IsNot Nothing Then
                Dim tmpDataGridView As DataGridView = CType(ControlsDict(ControlNames.ReplaceBlocksDGV.ToString), DataGridView)
                tmpDataGridView.Rows.Clear()

                For i = 0 To _ReplaceBlocksList.Count - 1
                    Dim SplitList = _ReplaceBlocksList(i).Split(CChar(","))
                    tmpDataGridView.Rows.Add(SplitList(0), SplitList(1))
                Next

                tmpDataGridView.CurrentCell = tmpDataGridView.Rows(tmpDataGridView.Rows.Count - 1).Cells(0)
                tmpDataGridView.ClearSelection()
            End If

        End Set
    End Property

    Private _DeleteBlocksList As List(Of String)
    Public Property DeleteBlocksList As List(Of String)
        Get
            Return _DeleteBlocksList
        End Get
        Set(value As List(Of String))
            _DeleteBlocksList = value

            If Me.TaskOptionsTLP IsNot Nothing And _DeleteBlocksList IsNot Nothing Then
                Dim tmpDataGridView As DataGridView = CType(ControlsDict(ControlNames.DeleteBlocksDGV.ToString), DataGridView)
                tmpDataGridView.Rows.Clear()

                For i = 0 To _DeleteBlocksList.Count - 1
                    tmpDataGridView.Rows.Add(_DeleteBlocksList(i))
                Next

                tmpDataGridView.CurrentCell = tmpDataGridView.Rows(tmpDataGridView.Rows.Count - 1).Cells(0)
                tmpDataGridView.ClearSelection()
            End If
        End Set
    End Property

    Private _AddBlocksList As List(Of String)
    Public Property AddBlocksList As List(Of String)
        Get
            Return _AddBlocksList
        End Get
        Set(value As List(Of String))
            _AddBlocksList = value

            If Me.TaskOptionsTLP IsNot Nothing And _AddBlocksList IsNot Nothing Then
                Dim tmpDataGridView As DataGridView = CType(ControlsDict(ControlNames.AddBlocksDGV.ToString), DataGridView)
                tmpDataGridView.Rows.Clear()

                For i = 0 To _AddBlocksList.Count - 1
                    tmpDataGridView.Rows.Add(_AddBlocksList(i))
                Next

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


    Enum ControlNames
        Browse
        BlockLibrary
        ReplaceBlocks
        ReplaceBlocksDGV
        DeleteBlocks
        DeleteBlocksDGV
        AddBlocks
        AddBlocksDGV
        ReportMissingSheet
        AutoHideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = False
        Me.AppliesToPart = False
        Me.AppliesToSheetmetal = False
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskUpdateBlocks
        Me.Category = "Update"
        SetColorFromCategory(Me)
        'Me.SolidEdgeRequired = False
        Me.SolidEdgeRequired = True  ' Default is so checking the box toggles a property update

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.BlockLibrary = ""
        Me.ReplaceBlocks = False
        Me.AddBlocks = False
        Me.DeleteBlocks = False
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

        Dim BlockLibraryDoc As SolidEdgeDraft.DraftDocument = Nothing
        Dim DocBlockName As String
        Dim LibraryBlockName As String
        Dim DocBlocksDict As New Dictionary(Of String, SolidEdgeDraft.Block)
        Dim LibraryBlocksDict As New Dictionary(Of String, SolidEdgeDraft.Block)

        Dim tmpSEDoc As SolidEdgeDraft.DraftDocument = CType(SEDoc, SolidEdgeDraft.DraftDocument)

        Try
            BlockLibraryDoc = CType(SEApp.Documents.Open(Me.BlockLibrary), SolidEdgeDraft.DraftDocument)
        Catch ex As Exception
            TaskLogger.AddMessage($"Unable to open '{Me.BlockLibrary}'")
        End Try

        ' Read blocks from both files
        If Not TaskLogger.HasErrors Then
            ' Read all blocks in both the file and template
            ' Populate two dicts such that Dict(BlockName) = Block Object
            For Each DocBlock As SolidEdgeDraft.Block In tmpSEDoc.Blocks
                DocBlocksDict(DocBlock.Name) = DocBlock
            Next
            For Each LibraryBlock As SolidEdgeDraft.Block In BlockLibraryDoc.Blocks
                LibraryBlocksDict(LibraryBlock.Name) = LibraryBlock
            Next

        End If

        ' Find and replace
        If Not TaskLogger.HasErrors And Me.ReplaceBlocks Then

            ' Create a dictionary such that ReplacementsDict(DocBlockName) = LibraryBlockName
            Dim ReplacementsDict As New Dictionary(Of String, String)
            For Each BlockPair As String In Me.ReplaceBlocksList
                Dim SplitList As List(Of String) = BlockPair.Split(CChar(",")).ToList
                DocBlockName = SplitList(0)
                LibraryBlockName = SplitList(1)
                ReplacementsDict(DocBlockName) = LibraryBlockName
            Next

            For Each DocBlockName In ReplacementsDict.Keys
                LibraryBlockName = ReplacementsDict(DocBlockName)
                If DocBlocksDict.Keys.Contains(DocBlockName) Then
                    If LibraryBlocksDict.Keys.Contains(LibraryBlockName) Then
                        If Not DocBlockName = LibraryBlockName Then
                            If DocBlocksDict.Keys.Contains(LibraryBlockName) Then
                                Dim s = $"Unable to replace '{DocBlockName}' with '{LibraryBlockName}'.  "
                                s = $"{s}A block with that name already exists in the file."
                                If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                                Continue For
                            Else
                                DocBlocksDict(DocBlockName).Name = LibraryBlockName
                            End If
                        End If
                        Try
                            tmpSEDoc.Blocks.ReplaceBlock(LibraryBlocksDict(LibraryBlockName))
                        Catch ex As Exception
                            Dim s = $"Unable to replace '{DocBlockName}' with '{LibraryBlockName}'.  "
                            s = $"{s}A block with that name may already exist in the file."
                            If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                        End Try
                    Else
                        TaskLogger.AddMessage($"Library does Not have a block named '{LibraryBlockName}'")
                    End If
                Else
                    ' Not an error
                End If
            Next
        End If

        ' Delete
        If Not TaskLogger.HasErrors And Me.DeleteBlocks Then

            For Each DocBlockName In Me.DeleteBlocksList

                If DocBlocksDict.Keys.Contains(DocBlockName) Then
                    Try
                        DocBlocksDict(DocBlockName).Delete()
                    Catch ex As Exception
                        TaskLogger.AddMessage($"Unable to delete block '{DocBlockName}'")
                    End Try
                Else
                    ' Not an error
                End If
            Next
        End If

        ' Add
        If Not TaskLogger.HasErrors And Me.AddBlocks Then

            For Each LibraryBlockName In Me.AddBlocksList

                ' Check that SEDoc does not already have a block with this name
                If Not DocBlocksDict.Keys.Contains(LibraryBlockName) Then

                    ' Check that the library contains a block with this name
                    If LibraryBlocksDict.Keys.Contains(LibraryBlockName) Then
                        Dim LibraryBlock As SolidEdgeDraft.Block
                        LibraryBlock = LibraryBlocksDict(LibraryBlockName)

                        ' Add the block
                        Dim DocBlock As SolidEdgeDraft.Block
                        Try
                            DocBlock = tmpSEDoc.Blocks.CopyBlock(LibraryBlock)

                            ' Add occurrences on SEDoc sheets at locations to match those in the library
                            AddBlockOccurrences(BlockLibraryDoc, LibraryBlock, tmpSEDoc, DocBlock)
                        Catch ex As Exception
                            TaskLogger.AddMessage($"Unable to add '{LibraryBlockName}'")
                        End Try


                    Else
                        TaskLogger.AddMessage($"Library does not have a block named '{LibraryBlockName}'")
                    End If
                Else
                    TaskLogger.AddMessage($"File already has a block named '{LibraryBlockName}'")
                End If
            Next


        End If

        If BlockLibraryDoc IsNot Nothing Then
            BlockLibraryDoc.Close(False)
            SEApp.DoIdle()
        End If

        If SEDoc.ReadOnly Then
            TaskLogger.AddMessage("Cannot save document marked 'Read Only'")

        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

    End Sub

    Private Overloads Sub ProcessInternal(ByVal FullName As String)

    End Sub


    Private Sub AddBlockOccurrences(
         BlockLibraryDoc As SolidEdgeDraft.DraftDocument,
         LibraryBlock As SolidEdgeDraft.Block,
         SEDoc As SolidEdgeDraft.DraftDocument,
         DocBlock As SolidEdgeDraft.Block)

        Dim LibrarySheetDict As New Dictionary(Of String, SolidEdgeDraft.Sheet)
        For Each LibrarySheet As SolidEdgeDraft.Sheet In BlockLibraryDoc.Sheets
            LibrarySheetDict(LibrarySheet.Name) = LibrarySheet
        Next

        Dim DocSheetDict As New Dictionary(Of String, SolidEdgeDraft.Sheet)
        For Each DocSheet As SolidEdgeDraft.Sheet In SEDoc.Sheets
            DocSheetDict(DocSheet.Name) = DocSheet
        Next

        For Each LibrarySheetName As String In LibrarySheetDict.Keys

            ' Does the LibrarySheet contain any of the block occurrence we're looking for?
            For Each LibraryBlockOccurrence As SolidEdgeDraft.BlockOccurrence In LibrarySheetDict(LibrarySheetName).BlockOccurrences
                If LibraryBlockOccurrence.Block Is LibraryBlock Then

                    ' Found a block occurrence in the library on this sheet.  

                    ' Does SEDoc have a sheet with the same name?
                    If DocSheetDict.Keys.Contains(LibrarySheetName) Then

                        ' Set parameters from the library occurrence and add.
                        Dim DocSheet As SolidEdgeDraft.Sheet = DocSheetDict(LibrarySheetName)
                        Try
                            Dim XOrigin As Double
                            Dim YOrigin As Double
                            LibraryBlockOccurrence.GetOrigin(XOrigin, YOrigin)
                            Dim BlockViewName As Object = LibraryBlockOccurrence.BlockView.Name
                            Dim Scale As Object = LibraryBlockOccurrence.ScaleFactor
                            Dim Rotation As Object = LibraryBlockOccurrence.RotationAngle

                            DocSheet.BlockOccurrences.Add(DocBlock.Name, XOrigin, YOrigin, BlockViewName, Scale, Rotation)

                        Catch ex As Exception
                            TaskLogger.AddMessage($"Unable to add {LibraryBlock.Name} to {DocSheet.Name}.  Error was {ex.Message}")
                        End Try
                    Else
                        If Me.ReportMissingSheet Then
                            TaskLogger.AddMessage($"Library has '{LibraryBlock.Name}' on sheet '{LibrarySheetName}'.  Document does not have a sheet with that name.")
                        End If
                    End If

                End If
            Next
        Next

    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Button As Button
        Dim TextBox As TextBox
        'Dim Label As Label
        Dim DataGridView As DataGridView
        Dim ColumnHeaders As List(Of String)

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        Button = FormatOptionsButton(ControlNames.Browse.ToString, "Block Library")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.BlockLibrary.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ReplaceBlocks.ToString, "Replace blocks")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        ColumnHeaders = {"File block name", "Library block name"}.ToList
        DataGridView = FormatOptionsDataGridView(ControlNames.ReplaceBlocksDGV.ToString, ColumnHeaders)
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
        AddHandler DataGridView.CellEnter, AddressOf DataGridViewOptions_CellEnter
        tmpTLPOptions.Controls.Add(DataGridView, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(DataGridView, 2)
        For i = 0 To ColumnHeaders.Count - 1
            DataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        DataGridView.Height = 46
        ControlsDict(DataGridView.Name) = DataGridView
        DataGridView.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DeleteBlocks.ToString, "Delete blocks")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        'CheckBox.Visible = False

        RowIndex += 1

        ColumnHeaders = {"File block name"}.ToList
        DataGridView = FormatOptionsDataGridView(ControlNames.DeleteBlocksDGV.ToString, ColumnHeaders)
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
        AddHandler DataGridView.CellEnter, AddressOf DataGridViewOptions_CellEnter
        tmpTLPOptions.Controls.Add(DataGridView, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(DataGridView, 2)
        For i = 0 To ColumnHeaders.Count - 1
            DataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        DataGridView.Height = 46
        ControlsDict(DataGridView.Name) = DataGridView
        DataGridView.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AddBlocks.ToString, "Add blocks")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        'CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ReportMissingSheet.ToString, "Report missing sheet in document")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        ColumnHeaders = {"Library block name"}.ToList
        DataGridView = FormatOptionsDataGridView(ControlNames.AddBlocksDGV.ToString, ColumnHeaders)
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
        AddHandler DataGridView.CellEnter, AddressOf DataGridViewOptions_CellEnter
        tmpTLPOptions.Controls.Add(DataGridView, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(DataGridView, 2)
        For i = 0 To ColumnHeaders.Count - 1
            DataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        DataGridView.Height = 46
        ControlsDict(DataGridView.Name) = DataGridView
        DataGridView.Visible = False

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

            If Not (Me.ReplaceBlocks Or Me.DeleteBlocks Or Me.AddBlocks) Then
                ErrorLogger.AddMessage("Select at least one task to perform")
            End If

            If Not IO.File.Exists(Me.BlockLibrary) Then
                If Me.ReplaceBlocks Or Me.AddBlocks Then
                    ErrorLogger.AddMessage("Select a valid block library")
                End If
            End If

            If Me.ReplaceBlocks Then
                If Me.ReplaceBlocksList IsNot Nothing Then
                    If Me.ReplaceBlocksList.Count = 0 Then
                        ErrorLogger.AddMessage("Enter at least one block to replace")
                    End If

                    If Me.ReplaceBlocksList.Count > 0 Then
                        For Each ItemPair As String In Me.ReplaceBlocksList
                            Dim SplitList As List(Of String) = ItemPair.Split(CChar(",")).ToList
                            Dim ReplaceItem = SplitList(0).Trim
                            Dim ReplacingItem = SplitList(1).Trim
                            If ReplaceItem = "" Or ReplacingItem = "" Then
                                ErrorLogger.AddMessage($"Cannot replace '{ReplaceItem}' with '{ReplacingItem}'")
                            End If
                        Next
                    End If
                Else
                    ErrorLogger.AddMessage("Enter at least one block to replace")
                End If
            End If

            If Me.DeleteBlocks Then
                If Me.DeleteBlocksList IsNot Nothing Then
                    If Me.DeleteBlocksList.Count = 0 Then
                        ErrorLogger.AddMessage("Enter at least one block to delete")
                    End If
                Else
                    ErrorLogger.AddMessage("Enter at least one block to delete")
                End If
            End If

            If Me.AddBlocks Then
                If Me.AddBlocksList IsNot Nothing Then
                    If Me.AddBlocksList.Count = 0 Then
                        ErrorLogger.AddMessage("Enter at least one block to add")
                    End If
                Else
                    ErrorLogger.AddMessage("Enter at least one block to add")
                End If
            End If
        End If

    End Sub


    Public Sub DataGridViewOptions_CellEnter(sender As System.Object, e As DataGridViewCellEventArgs)

        Dim DataGridView = CType(sender, DataGridView)
        Dim RowHeight As Integer = DataGridView.Rows(0).Height

        Dim Name = DataGridView.Name

        Select Case Name
            Case ControlNames.ReplaceBlocksDGV.ToString
                DataGridView.Height = (RowHeight + 1) * (DataGridView.Rows.Count + 1)

            Case ControlNames.DeleteBlocksDGV.ToString
                DataGridView.Height = (RowHeight + 1) * (DataGridView.Rows.Count + 1)

            Case ControlNames.AddBlocksDGV.ToString
                DataGridView.Height = (RowHeight + 1) * (DataGridView.Rows.Count + 1)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select
    End Sub

    Public Sub DataGridViewOptions_Leave(sender As System.Object, e As System.EventArgs)

        Dim DataGridView = CType(sender, DataGridView)

        DataGridView.CommitEdit(DataGridViewDataErrorContexts.LeaveControl)
        DataGridView.EndEdit()

        Dim Name = DataGridView.Name

        Select Case Name
            Case ControlNames.ReplaceBlocksDGV.ToString

                Dim tmpReplaceBlocksList As New List(Of String)

                For RowIdx = 0 To DataGridView.Rows.Count - 1
                    If DataGridView.Rows(RowIdx).IsNewRow Then
                        Continue For
                    End If
                    Dim ListItem As String = ""
                    For ColIdx = 0 To DataGridView.Columns.Count - 1
                        Dim Value As String
                        Try
                            Value = CStr(DataGridView.Rows(RowIdx).Cells(ColIdx).Value).Trim
                        Catch ex As Exception
                            Value = ""
                        End Try
                        If ColIdx = 0 Then
                            ListItem = Value
                        Else
                            ListItem = $"{ListItem},{Value}"
                        End If
                    Next
                    tmpReplaceBlocksList.Add(ListItem)
                Next

                Me.ReplaceBlocksList = tmpReplaceBlocksList

            Case ControlNames.DeleteBlocksDGV.ToString
                Dim tmpDeleteBlocksList As New List(Of String)

                For RowIdx = 0 To DataGridView.Rows.Count - 1
                    If DataGridView.Rows(RowIdx).IsNewRow Then
                        Continue For
                    End If
                    tmpDeleteBlocksList.Add(CStr(DataGridView.Rows(RowIdx).Cells(0).Value).Trim)
                Next

                Me.DeleteBlocksList = tmpDeleteBlocksList

            Case ControlNames.AddBlocksDGV.ToString
                Dim tmpAddBlocksList As New List(Of String)

                For RowIdx = 0 To DataGridView.Rows.Count - 1
                    If DataGridView.Rows(RowIdx).IsNewRow Then
                        Continue For
                    End If
                    tmpAddBlocksList.Add(CStr(DataGridView.Rows(RowIdx).Cells(0).Value).Trim)
                Next

                Me.AddBlocksList = tmpAddBlocksList

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.ReplaceBlocks.ToString
                Me.ReplaceBlocks = Checkbox.Checked
                CType(ControlsDict(ControlNames.ReplaceBlocksDGV.ToString), DataGridView).Visible = Me.ReplaceBlocks
                CType(ControlsDict(ControlNames.ReplaceBlocksDGV.ToString), DataGridView).Width = TaskOptionsTLP.Width - 5

            Case ControlNames.DeleteBlocks.ToString
                Me.DeleteBlocks = Checkbox.Checked
                CType(ControlsDict(ControlNames.DeleteBlocksDGV.ToString), DataGridView).Visible = Me.DeleteBlocks
                CType(ControlsDict(ControlNames.DeleteBlocksDGV.ToString), DataGridView).Width = TaskOptionsTLP.Width - 5

            Case ControlNames.AddBlocks.ToString
                Me.AddBlocks = Checkbox.Checked
                CType(ControlsDict(ControlNames.AddBlocksDGV.ToString), DataGridView).Visible = Me.AddBlocks
                CType(ControlsDict(ControlNames.ReportMissingSheet.ToString), CheckBox).Visible = Me.AddBlocks
                CType(ControlsDict(ControlNames.AddBlocksDGV.ToString), DataGridView).Width = TaskOptionsTLP.Width - 5

            Case ControlNames.ReportMissingSheet.ToString
                Me.ReportMissingSheet = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox
        'Dim ComboBox As ComboBox

        Select Case Name
            Case ControlNames.Browse.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a block library"
                tmpFileDialog.Filter = "Draft Documents|*.dft"

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.BlockLibrary = tmpFileDialog.FileName
                    TextBox = CType(ControlsDict(ControlNames.BlockLibrary.ToString), TextBox)
                    TextBox.Text = Me.BlockLibrary
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name
            Case ControlNames.BlockLibrary.ToString
                Me.BlockLibrary = TextBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Adds, replaces and/or deletes blocks in a draft file. "

        HelpString += vbCrLf + vbCrLf + "![UpdateBlocks](My%20Project/media/task_update_blocks.png)"

        HelpString += vbCrLf + vbCrLf + "For adding and replacing, a draft file containing the new blocks is required.  "
        HelpString += "In most cases it will simply be your draft template.  "
        HelpString += "Click the `Block Library` button to select it."

        HelpString += vbCrLf + vbCrLf + "The `Add Blocks` option adds the block to the document block library.  "
        HelpString += "A new block does not automatically appear on drawings. "
        HelpString += "The program checks each sheet of the library "
        HelpString += "and places an occurrence of it on the corresponding sheet of the document.  "
        HelpString += "It is placed at the same location, with the same scale and rotation, as the original.  "
        HelpString += "If the document does not have a corresponding sheet, enable `Report missing sheet` "
        HelpString += "to have it reported in the log file.  "

        Return HelpString
    End Function



End Class
