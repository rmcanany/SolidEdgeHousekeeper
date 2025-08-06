Option Strict On
Imports System.Drawing.Text
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

    Private _ReplaceBlocksReplaceExisting As Boolean
    Public Property ReplaceBlocksReplaceExisting As Boolean
        Get
            Return _ReplaceBlocksReplaceExisting
        End Get
        Set(value As Boolean)
            _ReplaceBlocksReplaceExisting = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ReplaceBlocksReplaceExisting.ToString), CheckBox).Checked = value
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

    Private _AddBlocksReplaceExisting As Boolean
    Public Property AddBlocksReplaceExisting As Boolean
        Get
            Return _AddBlocksReplaceExisting
        End Get
        Set(value As Boolean)
            _AddBlocksReplaceExisting = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AddBlocksReplaceExisting.ToString), CheckBox).Checked = value
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

                Try
                    For i = 0 To _ReplaceBlocksList.Count - 1
                        Dim SplitList = _ReplaceBlocksList(i).Split(CChar(","))
                        tmpDataGridView.Rows.Add(SplitList(0), SplitList(1))
                    Next
                Catch ex As Exception
                End Try

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

                Try
                    For i = 0 To _DeleteBlocksList.Count - 1
                        tmpDataGridView.Rows.Add(_DeleteBlocksList(i))
                    Next
                Catch ex As Exception
                End Try

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

                Try
                    For i = 0 To _AddBlocksList.Count - 1
                        tmpDataGridView.Rows.Add(_AddBlocksList(i))
                    Next
                Catch ex As Exception
                End Try

                tmpDataGridView.CurrentCell = tmpDataGridView.Rows(tmpDataGridView.Rows.Count - 1).Cells(0)
                tmpDataGridView.ClearSelection()
            End If
        End Set
    End Property

    Private _BlockLibraryBlockNames As List(Of String)
    Public Property BlockLibraryBlockNames As List(Of String)
        Get
            Return _BlockLibraryBlockNames
        End Get
        Set(value As List(Of String))

            If value IsNot Nothing Then
                value.Sort()
                _BlockLibraryBlockNames = value
            Else
                _BlockLibraryBlockNames = New List(Of String)
            End If


            If Me.TaskOptionsTLP IsNot Nothing And _BlockLibraryBlockNames IsNot Nothing Then
                Dim DVGs As New List(Of DataGridView)
                DVGs.Add(CType(ControlsDict(ControlNames.ReplaceBlocksDGV.ToString), DataGridView))
                DVGs.Add(CType(ControlsDict(ControlNames.DeleteBlocksDGV.ToString), DataGridView))
                DVGs.Add(CType(ControlsDict(ControlNames.AddBlocksDGV.ToString), DataGridView))

                For Each DVG As DataGridView In DVGs
                    For Each Column As DataGridViewComboBoxColumn In DVG.Columns
                        Column.Items.Clear()

                        Try
                            If _BlockLibraryBlockNames IsNot Nothing Then
                                For Each BlockName As String In _BlockLibraryBlockNames
                                    Column.Items.Add(BlockName)
                                Next
                            End If
                            If _ManuallyAddedBlockNames IsNot Nothing And Not Column.Name.ToLower.Contains("library") Then
                                For Each BlockName As String In _ManuallyAddedBlockNames
                                    Column.Items.Add(BlockName)
                                Next
                            End If
                        Catch ex As Exception
                        End Try

                    Next
                Next

            End If
        End Set
    End Property

    Private _ManuallyAddedBlockNames As List(Of String)
    Public Property ManuallyAddedBlockNames As List(Of String)
        Get
            Return _ManuallyAddedBlockNames
        End Get
        Set(value As List(Of String))

            If value IsNot Nothing Then
                value.Sort()
                _ManuallyAddedBlockNames = value
            Else
                _ManuallyAddedBlockNames = New List(Of String)
            End If



            If Me.TaskOptionsTLP IsNot Nothing And _ManuallyAddedBlockNames IsNot Nothing Then
                Dim DVGs As New List(Of DataGridView)
                DVGs.Add(CType(ControlsDict(ControlNames.ReplaceBlocksDGV.ToString), DataGridView))
                DVGs.Add(CType(ControlsDict(ControlNames.DeleteBlocksDGV.ToString), DataGridView))
                DVGs.Add(CType(ControlsDict(ControlNames.AddBlocksDGV.ToString), DataGridView))

                For Each DVG As DataGridView In DVGs
                    For Each Column As DataGridViewComboBoxColumn In DVG.Columns
                        Column.Items.Clear()

                        If _BlockLibraryBlockNames IsNot Nothing Then
                            For Each BlockName As String In _BlockLibraryBlockNames
                                Column.Items.Add(BlockName)
                            Next
                        End If
                        If Not Column.Name.ToLower.Contains("library") Then
                            For Each BlockName As String In _ManuallyAddedBlockNames
                                Column.Items.Add(BlockName)
                            Next
                        End If
                        Try
                        Catch ex As Exception
                        End Try
                    Next
                Next

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
        EditBlockList
        ReplaceBlocks
        ReplaceBlocksDGV
        ReplaceBlocksReplaceExisting
        DeleteBlocks
        DeleteBlocksDGV
        AddBlocks
        AddBlocksDGV
        AddBlocksReplaceExisting
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

        If SEDoc.FullName = Me.BlockLibrary Then
            TaskLogger.AddMessage("Cannot process the block library itself")
        End If

        Dim tmpSEDoc As SolidEdgeDraft.DraftDocument = CType(SEDoc, SolidEdgeDraft.DraftDocument)

        If Not TaskLogger.HasErrors Then
            Try
                BlockLibraryDoc = CType(SEApp.Documents.Open(Me.BlockLibrary), SolidEdgeDraft.DraftDocument)
            Catch ex As Exception
                TaskLogger.AddMessage($"Unable to open '{Me.BlockLibrary}'")
            End Try
        End If

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

            Dim ReplacedSameNameBlock As Boolean = False

            For Each DocBlockName In ReplacementsDict.Keys
                LibraryBlockName = ReplacementsDict(DocBlockName)
                If DocBlocksDict.Keys.Contains(DocBlockName) Then
                    If LibraryBlocksDict.Keys.Contains(LibraryBlockName) Then


                        If Not DocBlockName = LibraryBlockName Then
                            ' Blocks have different names.  Check if the document already has
                            ' a block with the same name as the library block.
                            If DocBlocksDict.Keys.Contains(LibraryBlockName) Then
                                ' It does have a block with the same name.
                                ' Replace it if the option is set.
                                If Me.ReplaceBlocksReplaceExisting Then
                                    Try
                                        tmpSEDoc.Blocks.ReplaceBlock(LibraryBlocksDict(LibraryBlockName))
                                        ReplacedSameNameBlock = True
                                    Catch ex As Exception
                                        Dim s = $"Unable to replace '{DocBlockName}' with '{LibraryBlockName}'.  "
                                        If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                                    End Try
                                Else
                                    Dim s = $"Unable to replace '{DocBlockName}' with '{LibraryBlockName}'.  "
                                    s = $"{s}A block with that name already exists in the file."
                                    If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                                    Continue For
                                End If
                            Else
                                DocBlocksDict(DocBlockName).Name = LibraryBlockName
                            End If
                        End If

                        Try
                            If Not ReplacedSameNameBlock Then
                                tmpSEDoc.Blocks.ReplaceBlock(LibraryBlocksDict(LibraryBlockName))
                            Else
                                ' Need to change the source block of each occurrence
                                tmpSEDoc.Save()
                                SEApp.DoIdle()

                                Dim OldBlock As SolidEdgeDraft.Block = DocBlocksDict(DocBlockName)
                                Dim NewBlock As SolidEdgeDraft.Block = Nothing
                                For Each tmpDocBlock As SolidEdgeDraft.Block In tmpSEDoc.Blocks
                                    If tmpDocBlock.Name = LibraryBlockName Then
                                        NewBlock = tmpDocBlock
                                    End If
                                Next

                                For Each DocSheet As SolidEdgeDraft.Sheet In tmpSEDoc.Sheets
                                    For Each DocBlockOccurrence As SolidEdgeDraft.BlockOccurrence In DocSheet.BlockOccurrences
                                        If DocBlockOccurrence.Block Is OldBlock Then
                                            Dim x As Double
                                            Dim y As Double
                                            DocBlockOccurrence.GetOrigin(x, y)
                                            DocBlockOccurrence.Block = NewBlock
                                            DocBlockOccurrence.SetOrigin(x, y)
                                            Dim i = 0
                                        End If
                                    Next
                                Next
                            End If
                        Catch ex As Exception
                            Dim s = $"Unable to replace '{DocBlockName}' with '{LibraryBlockName}'.  "
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
                    If Me.AddBlocksReplaceExisting Then
                        Dim tmpDocBlockName As String = LibraryBlockName
                        Try
                            Dim LibraryBlock As SolidEdgeDraft.Block = LibraryBlocksDict(LibraryBlockName)
                            Dim DocBlock As SolidEdgeDraft.Block = tmpSEDoc.Blocks.ReplaceBlock(LibraryBlocksDict(LibraryBlockName))
                            SEApp.DoIdle()

                            ' Add occurrences on SEDoc sheets at locations to match those in the library
                            AddBlockOccurrences(BlockLibraryDoc, LibraryBlock, tmpSEDoc, DocBlock)

                        Catch ex As Exception
                            Dim s = $"Unable to replace '{tmpDocBlockName}' with '{LibraryBlockName}'.  "
                            's = $"{s}A block with that name may already exist in the file."
                            If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                        End Try

                    Else
                        TaskLogger.AddMessage($"File already has a block named '{LibraryBlockName}'")
                    End If
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

    'Private Sub EditBlockList()
    '    Dim USEA As New UtilsSEApp(Form_Main)

    '    Form_Main.TextBoxStatus.Text = "Starting Solid Edge..."

    '    USEA.SEStart(
    '        RunInBackground:=False,
    '        UseCurrentSession:=True,
    '        NoUpdateMRU:=False,
    '        ProcessDraftsInactive:=False)

    '    Dim SEDoc As SolidEdgeDraft.DraftDocument = CType(USEA.SEApp.Documents.Open(Me.BlockLibrary), SolidEdgeDraft.DraftDocument)

    '    Dim Blocks As SolidEdgeDraft.Blocks = SEDoc.Blocks

    '    Dim tmpBlockLibraryBlockNames As New List(Of String)
    '    tmpBlockLibraryBlockNames.Add("")

    '    If Blocks IsNot Nothing Then
    '        For Each Block As SolidEdgeDraft.Block In Blocks
    '            tmpBlockLibraryBlockNames.Add(Block.Name)
    '        Next
    '    End If

    '    Me.BlockLibraryBlockNames = tmpBlockLibraryBlockNames

    '    SEDoc.Close(False)

    '    USEA.SEStop(UseCurrentSession:=True)

    '    Form_Main.TextBoxStatus.Text = $"Found {BlockLibraryBlockNames.Count - 1} blocks in the library"

    'End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Button As Button
        Dim TextBox As TextBox
        Dim DataGridView As DataGridView
        Dim ColumnHeaders As List(Of String)
        Dim ColumnType As String
        If Me.BlockLibraryBlockNames Is Nothing Then Me.BlockLibraryBlockNames = New List(Of String)

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        Button = FormatOptionsButton(ControlNames.Browse.ToString, "Block library")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.BlockLibrary.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.EditBlockList.ToString, "Edit list")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ReplaceBlocks.ToString, "Replace blocks")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ReplaceBlocksReplaceExisting.ToString, "Overwrite existing with replacement")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        'https://stackoverflow.com/questions/41281920/datagridview-combobox-column-that-will-accept-any-text
        'https://www.vbforums.com/showthread.php?784101-Datagridview-Combobox-Column-DropDownStyle

        ColumnHeaders = {"File block name", "Library block name"}.ToList
        ColumnType = "Combobox"
        'ColumnItems = Me.BlockLibraryBlockNames
        DataGridView = FormatOptionsDataGridView(ControlNames.ReplaceBlocksDGV.ToString, ColumnHeaders, ColumnType, Me.BlockLibraryBlockNames)
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
        'AddHandler DataGridView.CellEnter, AddressOf DataGridViewOptions_CellEnter
        AddHandler DataGridView.CellValueChanged, AddressOf DataGridViewOptions_UpdateSize
        AddHandler DataGridView.KeyDown, AddressOf dataGridViewItems_KeyDown
        'AddHandler DataGridView.CellDoubleClick, AddressOf DataGridViewOptions_CellContentDoubleClick
        AddHandler DataGridView.DataError, AddressOf DataGridViewOptions_DataError
        tmpTLPOptions.Controls.Add(DataGridView, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(DataGridView, 2)
        For i = 0 To ColumnHeaders.Count - 1
            DataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        DataGridView.Height = (DataGridView.Rows(0).Height + 1) * (DataGridView.Rows.Count + 2)
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
        ColumnType = "Combobox"
        'ColumnItems = Me.BlockLibraryBlockNames
        DataGridView = FormatOptionsDataGridView(ControlNames.DeleteBlocksDGV.ToString, ColumnHeaders, ColumnType, Me.BlockLibraryBlockNames)
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
        'AddHandler DataGridView.CellEnter, AddressOf DataGridViewOptions_CellEnter
        AddHandler DataGridView.CellValueChanged, AddressOf DataGridViewOptions_UpdateSize
        AddHandler DataGridView.KeyDown, AddressOf dataGridViewItems_KeyDown
        'AddHandler DataGridView.CellDoubleClick, AddressOf DataGridViewOptions_CellContentDoubleClick
        tmpTLPOptions.Controls.Add(DataGridView, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(DataGridView, 2)
        For i = 0 To ColumnHeaders.Count - 1
            DataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        DataGridView.Height = (DataGridView.Rows(0).Height + 1) * (DataGridView.Rows.Count + 2)
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

        CheckBox = FormatOptionsCheckBox(ControlNames.AddBlocksReplaceExisting.ToString, "Overwrite existing with added block")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ReportMissingSheet.ToString, "Report missing sheet in document")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        ColumnHeaders = {"Library block name"}.ToList
        ColumnType = "Combobox"
        'ColumnItems = Me.BlockLibraryBlockNames
        DataGridView = FormatOptionsDataGridView(ControlNames.AddBlocksDGV.ToString, ColumnHeaders, ColumnType, Me.BlockLibraryBlockNames)
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
        'AddHandler DataGridView.CellEnter, AddressOf DataGridViewOptions_CellEnter
        AddHandler DataGridView.CellValueChanged, AddressOf DataGridViewOptions_UpdateSize
        AddHandler DataGridView.KeyDown, AddressOf dataGridViewItems_KeyDown
        'AddHandler DataGridView.CellDoubleClick, AddressOf DataGridViewOptions_CellContentDoubleClick
        tmpTLPOptions.Controls.Add(DataGridView, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(DataGridView, 2)
        For i = 0 To ColumnHeaders.Count - 1
            DataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        DataGridView.Height = (DataGridView.Rows(0).Height + 1) * (DataGridView.Rows.Count + 2)
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
                        ErrorLogger.AddMessage("Enter at least one block to replace OR disable the option")
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
                    ErrorLogger.AddMessage("Enter at least one block to replace OR disable the option")
                End If
            End If

            If Me.DeleteBlocks Then
                If Me.DeleteBlocksList IsNot Nothing Then
                    If Me.DeleteBlocksList.Count = 0 Then
                        ErrorLogger.AddMessage("Enter at least one block to delete OR disable the option")
                    End If
                Else
                    ErrorLogger.AddMessage("Enter at least one block to delete OR disable the option")
                End If
            End If

            If Me.AddBlocks Then
                If Me.AddBlocksList IsNot Nothing Then
                    If Me.AddBlocksList.Count = 0 Then
                        ErrorLogger.AddMessage("Enter at least one block to add OR disable the option")
                    End If
                Else
                    ErrorLogger.AddMessage("Enter at least one block to add OR disable the option")
                End If
            End If
        End If

    End Sub


    Private Sub dataGridViewItems_KeyDown(sender As Object, e As KeyEventArgs)

        Dim DataGridView = CType(sender, DataGridView)
        If DataGridView.SelectedCells IsNot Nothing AndAlso DataGridView.SelectedCells.Count > 0 Then
            Dim SelectedCell = DataGridView.SelectedCells(0)

            If e.Control And e.KeyCode = Keys.V Then
                SelectedCell.Value = Clipboard.GetText.Trim
            ElseIf e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back Then
                SelectedCell.Value = ""
            End If

        End If

    End Sub

    'Public Sub DataGridViewOptions_CellEnter(sender As System.Object, e As DataGridViewCellEventArgs)

    '    Dim DataGridView = CType(sender, DataGridView)
    '    Dim RowHeight As Integer = DataGridView.Rows(0).Height

    '    Dim Name = DataGridView.Name

    '    Select Case Name
    '        Case ControlNames.ReplaceBlocksDGV.ToString
    '            DataGridView.Height = (RowHeight + 1) * (DataGridView.Rows.Count + 1)

    '        Case ControlNames.DeleteBlocksDGV.ToString
    '            DataGridView.Height = (RowHeight + 1) * (DataGridView.Rows.Count + 1)

    '        Case ControlNames.AddBlocksDGV.ToString
    '            DataGridView.Height = (RowHeight + 1) * (DataGridView.Rows.Count + 1)

    '        Case Else
    '            MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
    '    End Select
    'End Sub

    Public Sub DataGridViewOptions_UpdateSize(sender As System.Object, e As DataGridViewCellEventArgs)

        Dim DataGridView = CType(sender, DataGridView)

        Select Case DataGridView.Name
            Case ControlNames.ReplaceBlocksDGV.ToString
                DataGridView.Height = (DataGridView.Rows(0).Height + 1) * (DataGridView.Rows.Count + 2)

            Case ControlNames.DeleteBlocksDGV.ToString
                DataGridView.Height = (DataGridView.Rows(0).Height + 1) * (DataGridView.Rows.Count + 2)

            Case ControlNames.AddBlocksDGV.ToString
                DataGridView.Height = (DataGridView.Rows(0).Height + 1) * (DataGridView.Rows.Count + 2)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select
    End Sub

    Public Sub DataGridViewOptions_Leave(sender As System.Object, e As System.EventArgs)

        Dim DataGridView = CType(sender, DataGridView)

        DataGridView.CommitEdit(DataGridViewDataErrorContexts.LeaveControl)
        DataGridView.EndEdit()

        Select Case DataGridView.Name

            Case ControlNames.ReplaceBlocksDGV.ToString

                Dim tmpReplaceBlocksList As New List(Of String)

                ' Remove blank rows
                For RowIdx = DataGridView.Rows.Count - 1 To 0 Step -1
                    If DataGridView.Rows(RowIdx).IsNewRow Then Continue For

                    Dim BlockToReplace As String = ""
                    Dim ReplacementBlock As String = ""

                    Try
                        BlockToReplace = CStr(DataGridView.Rows(RowIdx).Cells(0).Value).Trim
                    Catch ex As Exception
                        BlockToReplace = ""
                    End Try
                    Try
                        ReplacementBlock = CStr(DataGridView.Rows(RowIdx).Cells(1).Value).Trim
                    Catch ex As Exception
                        ReplacementBlock = ""
                    End Try

                    If BlockToReplace = "" And ReplacementBlock = "" Then
                        DataGridView.Rows.RemoveAt(RowIdx)
                    End If
                Next

                ' Update Me.ReplaceBlocksList
                For RowIdx = 0 To DataGridView.Rows.Count - 1
                    If DataGridView.Rows(RowIdx).IsNewRow Then Continue For

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

                DataGridView.Height = (DataGridView.Rows(0).Height + 1) * (DataGridView.Rows.Count + 1)

            Case ControlNames.DeleteBlocksDGV.ToString
                Dim tmpDeleteBlocksList As New List(Of String)

                ' Remove blank rows
                For RowIdx = DataGridView.Rows.Count - 1 To 0 Step -1
                    If DataGridView.Rows(RowIdx).IsNewRow Then Continue For

                    Dim BlockToDelete As String = ""
                    Try
                        BlockToDelete = CStr(DataGridView.Rows(RowIdx).Cells(0).Value).Trim
                    Catch ex As Exception
                        BlockToDelete = ""
                    End Try

                    If BlockToDelete = "" Then
                        DataGridView.Rows.RemoveAt(RowIdx)
                    End If

                Next

                ' Update Me.DeleteBlocksList
                For RowIdx = 0 To DataGridView.Rows.Count - 1
                    If DataGridView.Rows(RowIdx).IsNewRow Then Continue For

                    tmpDeleteBlocksList.Add(CStr(DataGridView.Rows(RowIdx).Cells(0).Value).Trim)
                Next

                Me.DeleteBlocksList = tmpDeleteBlocksList

                DataGridView.Height = (DataGridView.Rows(0).Height + 1) * (DataGridView.Rows.Count + 1)

            Case ControlNames.AddBlocksDGV.ToString
                Dim tmpAddBlocksList As New List(Of String)

                ' Remove blank rows
                For RowIdx = DataGridView.Rows.Count - 1 To 0 Step -1
                    If DataGridView.Rows(RowIdx).IsNewRow Then Continue For

                    Dim BlockToAdd As String = ""
                    Try
                        BlockToAdd = CStr(DataGridView.Rows(RowIdx).Cells(0).Value).Trim
                    Catch ex As Exception
                        BlockToAdd = ""
                    End Try

                    If BlockToAdd = "" Then
                        DataGridView.Rows.RemoveAt(RowIdx)
                    End If

                Next

                ' Update Me.AddBlocksList
                For RowIdx = 0 To DataGridView.Rows.Count - 1
                    If DataGridView.Rows(RowIdx).IsNewRow Then Continue For

                    tmpAddBlocksList.Add(CStr(DataGridView.Rows(RowIdx).Cells(0).Value).Trim)
                Next

                Me.AddBlocksList = tmpAddBlocksList

                DataGridView.Height = (DataGridView.Rows(0).Height + 1) * (DataGridView.Rows.Count + 1)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    'Private Sub DataGridViewOptions_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs)

    '    Dim DGV As DataGridView = CType(sender, DataGridView)
    '    Dim SelectedCell As DataGridViewComboBoxCell = CType(DGV.SelectedCells(0), DataGridViewComboBoxCell)
    '    Dim CellColumn As DataGridViewComboBoxColumn = CType(DGV.Columns(e.ColumnIndex), DataGridViewComboBoxColumn)

    '    If Not CellColumn.Name.ToLower.Contains("library") Then
    '        Dim FTP As New FormTextPrompt
    '        FTP.Text = "Enter Block Name"
    '        If SelectedCell.Value IsNot Nothing Then FTP.TextBoxInput.Text = CStr(SelectedCell.Value)

    '        FTP.LabelPrompt.Text = "Enter block name"
    '        Dim Result = FTP.ShowDialog()

    '        If Result = DialogResult.OK Then
    '            Dim Name As String = FTP.TextBoxInput.Text

    '            If Not CellColumn.Items.Contains(Name) Then
    '                CellColumn.Items.Add(Name)
    '            End If

    '            SelectedCell.Value = Name

    '            If Me.ManuallyAddedBlockNames Is Nothing Then Me.ManuallyAddedBlockNames = New List(Of String)

    '            Dim tmpManuallyAddedBlockNames As List(Of String) = Me.ManuallyAddedBlockNames
    '            If Not tmpManuallyAddedBlockNames.Contains(Name) And Not Me.BlockLibraryBlockNames.Contains(Name) Then
    '                tmpManuallyAddedBlockNames.Add(Name)
    '                Me.ManuallyAddedBlockNames = tmpManuallyAddedBlockNames
    '            End If

    '        End If
    '    End If
    'End Sub

    Private Sub DataGridViewOptions_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)

    End Sub

    'Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs)
    '    Dim DGV As DataGridView = CType(sender, DataGridView)
    '    Dim comboBoxColumn As DataGridViewComboBoxColumn = CType(DGV.Columns(e.ColumnIndex), DataGridViewComboBoxColumn)
    '    If (Not comboBoxColumn.Items.Contains(e.FormattedValue)) Then
    '        comboBoxColumn.Items.Add(e.FormattedValue)
    '    End If
    '    Dim comboBox As DataGridViewComboBoxCell = CType(DGV.CurrentCell, DataGridViewComboBoxCell)
    '    Dim OwningColumn As DataGridViewComboBoxColumn = CType(comboBox.OwningColumn, DataGridViewComboBoxColumn)
    '    comboBox.Value = OwningColumn.Items(comboBoxColumn.Items.Count - 1)
    'End Sub


    'Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs)
    '    If TypeOf e.Control Is ComboBox Then
    '        Dim cb As ComboBox = TryCast(e.Control, ComboBox)
    '        cb.DropDownStyle = ComboBoxStyle.DropDown
    '        'cb.Items.Insert(cb.Items.Count - 1, cb.Text)
    '        'cb.Items.Add(cb.Text)

    '    End If
    'End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.ReplaceBlocks.ToString
                Me.ReplaceBlocks = Checkbox.Checked
                CType(ControlsDict(ControlNames.ReplaceBlocksDGV.ToString), DataGridView).Visible = Me.ReplaceBlocks
                CType(ControlsDict(ControlNames.ReplaceBlocksReplaceExisting.ToString), CheckBox).Visible = Me.ReplaceBlocks
                CType(ControlsDict(ControlNames.ReplaceBlocksDGV.ToString), DataGridView).Width = TaskOptionsTLP.Width - 5

            Case ControlNames.ReplaceBlocksReplaceExisting.ToString
                Me.ReplaceBlocksReplaceExisting = Checkbox.Checked

            Case ControlNames.DeleteBlocks.ToString
                Me.DeleteBlocks = Checkbox.Checked
                CType(ControlsDict(ControlNames.DeleteBlocksDGV.ToString), DataGridView).Visible = Me.DeleteBlocks
                CType(ControlsDict(ControlNames.DeleteBlocksDGV.ToString), DataGridView).Width = TaskOptionsTLP.Width - 5

            Case ControlNames.AddBlocks.ToString
                Me.AddBlocks = Checkbox.Checked
                CType(ControlsDict(ControlNames.AddBlocksDGV.ToString), DataGridView).Visible = Me.AddBlocks
                CType(ControlsDict(ControlNames.AddBlocksReplaceExisting.ToString), CheckBox).Visible = Me.AddBlocks
                CType(ControlsDict(ControlNames.ReportMissingSheet.ToString), CheckBox).Visible = Me.AddBlocks
                CType(ControlsDict(ControlNames.AddBlocksDGV.ToString), DataGridView).Width = TaskOptionsTLP.Width - 5

            Case ControlNames.AddBlocksReplaceExisting.ToString
                Me.AddBlocksReplaceExisting = Checkbox.Checked

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

                    'Dim SSDoc As New HCStructuredStorageDoc(Me.BlockLibrary)
                    'SSDoc.ReadBlockLibrary()

                    'Me.BlockLibraryBlockNames = SSDoc.GetBlockLibraryBlockNames
                    'Me.BlockLibraryBlockNames.Sort()

                End If

            Case ControlNames.EditBlockList.ToString
                Dim FBLBN As New FormBlockLibraryBlockNames()

                FBLBN.BlockLibraryBlockNames = Me.BlockLibraryBlockNames
                FBLBN.ManuallyAddedBlockNames = Me.ManuallyAddedBlockNames
                FBLBN.BlockLibrary = Me.BlockLibrary

                Dim Result As DialogResult = FBLBN.ShowDialog

                If Result = DialogResult.OK Then
                    Me.BlockLibraryBlockNames = FBLBN.BlockLibraryBlockNames
                    Me.ManuallyAddedBlockNames = FBLBN.ManuallyAddedBlockNames

                End If
                'EditBlockList()

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
        HelpString += "Click the `Block Library` button to select it. "

        HelpString += vbCrLf + vbCrLf + "To populate the combo boxes with the blocks in the library, click the `Update list` button. "
        HelpString += "To add a block name that is not in the library, double-click the combo box. "
        HelpString += "You can add a name to a file block list, but not to a library block list. "
        HelpString += "Note, the combo box behavior is a little quirky.  You have to click twice to use it. "
        HelpString += "The first click gives it `focus`, the next opens the drop down. "
        'HelpString += "In theory there is a way around it, but it messes up the double-click function. "

        HelpString += vbCrLf + vbCrLf + "There are a few options.  They are described next.  "

        HelpString += vbCrLf + "- `Replace Blocks` `Overwrite existing with replacement`: "
        HelpString += "This is confusing; the point of the command is to overwrite blocks, right? "
        HelpString += "There is an ambiguity, however. "
        HelpString += "Say you want to replace `Block1` in the file with `Block2` in the library. "
        HelpString += "It's clear what will happen to `Block1`.  But what if there is already a `Block2` in the file? "
        HelpString += "This option tells the program how to proceed for that situation. "

        HelpString += vbCrLf + "- `Add Blocks` `Overwrite existing with added block`: "
        HelpString += "Similar to above, this is for when you're adding `Block1` to the file, but it already has one with that name.  "

        HelpString += vbCrLf + "- `Add Blocks` `Report missing sheet in document`: "
        HelpString += "The Solid Edge `Add Blocks` command updates the file's library, "
        HelpString += "but does not add it to drawing sheets. "
        HelpString += "But you do want it on drawings, so the program checks each sheet of the library "
        HelpString += "and places an occurrence on the corresponding sheet in the file, "
        HelpString += "at the same location, scale, and rotation as the original.  "
        HelpString += "If the file does not have a corresponding sheet, enable this option "
        HelpString += "to have it reported in the log file.  "

        Return HelpString
    End Function



End Class
