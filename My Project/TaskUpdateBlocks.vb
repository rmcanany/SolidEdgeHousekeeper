Option Strict On

Public Class TaskUpdateBlocks
    Inherits Task

    Private NewWay As Boolean = True

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

                UpdateDGVSize(tmpDataGridView)

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

                UpdateDGVSize(tmpDataGridView)

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

                UpdateDGVSize(tmpDataGridView)

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
                    Next
                Next

            End If
        End Set
    End Property

    Private _WSAddByName As Boolean
    Public Property WSAddByName As Boolean
        Get
            Return _WSAddByName
        End Get
        Set(value As Boolean)
            _WSAddByName = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.WSAddByName.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _WSAddByOrder As Boolean
    Public Property WSAddByOrder As Boolean
        Get
            Return _WSAddByOrder
        End Get
        Set(value As Boolean)
            _WSAddByOrder = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.WSAddByOrder.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _WSAddToAll As Boolean
    Public Property WSAddToAll As Boolean
        Get
            Return _WSAddToAll
        End Get
        Set(value As Boolean)
            _WSAddToAll = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.WSAddToAll.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _BSAddByName As Boolean
    Public Property BSAddByName As Boolean
        Get
            Return _BSAddByName
        End Get
        Set(value As Boolean)
            _BSAddByName = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BSAddByName.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _BSAddBySize As Boolean
    Public Property BSAddBySize As Boolean
        Get
            Return _BSAddBySize
        End Get
        Set(value As Boolean)
            _BSAddBySize = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BSAddBySize.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _BSRename As Boolean
    Public Property BSRename As Boolean
        Get
            Return _BSRename
        End Get
        Set(value As Boolean)
            _BSRename = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BSRename.ToString), CheckBox).Checked = value
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
        ReplaceBlocksReplaceExisting
        ReplaceBlocksDGV
        DeleteBlocks
        DeleteBlocksDGV
        AddBlocks
        AddBlocksDGV
        AddBlocksReplaceExisting
        ReportMissingSheet
        WorkingSheetLabel
        WSAddByName
        WSAddByOrder
        WSAddToAll
        BackgroundSheetLabel
        BSAddByName
        BSAddBySize
        BSRename
        StructuredStorageEdit
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

        OleMessageFilter.Register()

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
                TaskLogger.AddMessage($"Unable to open block library '{Me.BlockLibrary}'.  Exception: {ex.Message}")
            End Try
        End If

        ' Read blocks from both files
        If Not TaskLogger.HasErrors Then
            ' Read all blocks in both the file and template
            ' Populate two dicts such that Dict(BlockName) = Block Object
            Try
                For Each DocBlock As SolidEdgeDraft.Block In tmpSEDoc.Blocks
                    DocBlocksDict(DocBlock.Name) = DocBlock
                Next
            Catch ex As Exception
                TaskLogger.AddMessage($"Unable to process blocks in '{IO.Path.GetFileName(SEDoc.FullName)}'.  Exception: {ex.Message}")
            End Try
            Try
                For Each LibraryBlock As SolidEdgeDraft.Block In BlockLibraryDoc.Blocks
                    LibraryBlocksDict(LibraryBlock.Name) = LibraryBlock
                Next
            Catch ex As Exception
                TaskLogger.AddMessage($"Unable to process blocks in '{IO.Path.GetFileName(BlockLibrary)}'.  Exception: {ex.Message}")
            End Try

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

                ' Clear and re-read the document block library every time, as it may have changed.
                Try
                    DocBlocksDict.Clear()
                    For Each DocBlock As SolidEdgeDraft.Block In tmpSEDoc.Blocks
                        DocBlocksDict(DocBlock.Name) = DocBlock
                    Next
                Catch ex As Exception
                    TaskLogger.AddMessage($"Unable to process blocks in '{IO.Path.GetFileName(SEDoc.FullName)}'.  Exception: {ex.Message}")
                End Try

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
                                        Dim s = $"Unable to replace '{DocBlockName}' with '{LibraryBlockName}'.  Exception: {ex.Message}"
                                        If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                                    End Try
                                Else
                                    Dim s = $"Unable to replace '{DocBlockName}' with '{LibraryBlockName}'.  "
                                    s = $"{s}A block with that name already exists in the file."
                                    If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                                    Continue For
                                End If
                            Else
                                Try
                                    DocBlocksDict(DocBlockName).Name = LibraryBlockName
                                Catch ex As Exception
                                    Dim s = $"Unable to replace '{DocBlockName}' with '{LibraryBlockName}'.  Exception: {ex.Message}"
                                    If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                                End Try
                            End If
                        End If

                        Try
                            If Not ReplacedSameNameBlock Then
                                tmpSEDoc.Blocks.ReplaceBlock(LibraryBlocksDict(LibraryBlockName))
                            Else
                                ' We get here if:
                                '     - There was a block already in the file with the same name as the replacement.
                                '     - The option to replace it was enabled.
                                ' If so, change the source of each occurrence to the now-replaced block.
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
                                        End If
                                    Next
                                Next
                            End If
                        Catch ex As Exception
                            Dim s = $"Unable to replace '{DocBlockName}' with '{LibraryBlockName}'.  Exception: {ex.Message}"
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

                Try
                    DocBlocksDict.Clear()
                    For Each DocBlock As SolidEdgeDraft.Block In tmpSEDoc.Blocks
                        DocBlocksDict(DocBlock.Name) = DocBlock
                    Next
                Catch ex As Exception
                    TaskLogger.AddMessage($"Unable to process blocks in '{IO.Path.GetFileName(SEDoc.FullName)}'.  Exception: {ex.Message}")
                End Try

                If DocBlocksDict.Keys.Contains(DocBlockName) Then
                    Try
                        DocBlocksDict(DocBlockName).Delete()
                    Catch ex As Exception
                        TaskLogger.AddMessage($"Unable to delete block '{DocBlockName}'.  Exception: {ex.Message}")
                    End Try
                Else
                    ' Not an error
                End If
            Next
        End If

        ' Add
        If Not TaskLogger.HasErrors And Me.AddBlocks Then

            For Each LibraryBlockName In Me.AddBlocksList

                Try
                    DocBlocksDict.Clear()
                    For Each DocBlock As SolidEdgeDraft.Block In tmpSEDoc.Blocks
                        DocBlocksDict(DocBlock.Name) = DocBlock
                    Next
                Catch ex As Exception
                    TaskLogger.AddMessage($"Unable to process blocks in '{IO.Path.GetFileName(SEDoc.FullName)}'.  Exception: {ex.Message}")
                End Try

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
                            If Not Me.NewWay Then
                                AddBlockOccurrences(BlockLibraryDoc, LibraryBlock, tmpSEDoc, DocBlock)
                            Else
                                AddBlockOccurrences2(BlockLibraryDoc, LibraryBlock, tmpSEDoc, DocBlock)
                            End If
                        Catch ex As Exception
                            TaskLogger.AddMessage($"Unable to add '{LibraryBlockName}'.  Exception: {ex.Message}")
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
                            If Not Me.NewWay Then
                                AddBlockOccurrences(BlockLibraryDoc, LibraryBlock, tmpSEDoc, DocBlock)
                            Else
                                AddBlockOccurrences2(BlockLibraryDoc, LibraryBlock, tmpSEDoc, DocBlock)
                            End If

                        Catch ex As Exception
                            Dim s = $"Unable to replace '{tmpDocBlockName}' with '{LibraryBlockName}'.  Exception: {ex.Message}"
                            If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                        End Try

                    Else
                        TaskLogger.AddMessage($"File already has a block named '{LibraryBlockName}'")
                    End If
                End If
            Next

        End If

        If BlockLibraryDoc IsNot Nothing Then
            Try
                BlockLibraryDoc.Close(False)
                SEApp.DoIdle()
            Catch ex As Exception
                TaskLogger.AddMessage($"Unable to close block library.  Exception: {ex.Message}")
            End Try
        End If

        If SEDoc.ReadOnly Then
            TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
        Else
            Try
                SEDoc.Save()
                SEApp.DoIdle()
            Catch ex As Exception
                TaskLogger.AddMessage($"Unable to save file.  Exception: {ex.Message}")
            End Try
        End If

    End Sub

    Private Overloads Sub ProcessInternal(ByVal FullName As String)

    End Sub


    Private Sub AddBlockOccurrences2(
        BlockLibraryDoc As SolidEdgeDraft.DraftDocument,
        LibraryBlock As SolidEdgeDraft.Block,
        SEDoc As SolidEdgeDraft.DraftDocument,
        DocBlock As SolidEdgeDraft.Block)

        If Me.WSAddByName Then DoWSAddByName(BlockLibraryDoc, LibraryBlock, SEDoc, DocBlock)
        If Me.WSAddByOrder Then DoWSAddByOrder(BlockLibraryDoc, LibraryBlock, SEDoc, DocBlock)
        If Me.WSAddToAll Then DoWSAddToAll(BlockLibraryDoc, LibraryBlock, SEDoc, DocBlock)

        If Me.BSAddByName Then DoBSAddByName(BlockLibraryDoc, LibraryBlock, SEDoc, DocBlock)

    End Sub

    Private Sub DoWSAddByName(
        BlockLibraryDoc As SolidEdgeDraft.DraftDocument,
        LibraryBlock As SolidEdgeDraft.Block,
        SEDoc As SolidEdgeDraft.DraftDocument,
        DocBlock As SolidEdgeDraft.Block)

        ' Get library and document working sheets
        Dim LibrarySheetDict = GetSheetDict(BlockLibraryDoc, SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection)
        Dim DocSheetDict = GetSheetDict(SEDoc, SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection)

        ' Iterate through the library sheets
        For Each LibrarySheetName As String In LibrarySheetDict.Keys

            ' Does the LibrarySheet contain any of the block occurrence we're looking for?
            For Each LibraryBlockOccurrence As SolidEdgeDraft.BlockOccurrence In LibrarySheetDict(LibrarySheetName).BlockOccurrences
                If LibraryBlockOccurrence.Block Is LibraryBlock Then
                    ' Found one.  Does SEDoc have a sheet with the same name?
                    If DocSheetDict.Keys.Contains(LibrarySheetName) Then

                        ' Set parameters from the library occurrence and add.
                        Dim DocSheet As SolidEdgeDraft.Sheet = DocSheetDict(LibrarySheetName)

                        PlaceBlockOccurrence(LibraryBlock, DocBlock, LibraryBlockOccurrence, DocSheet)
                    Else
                        If Me.ReportMissingSheet Then
                            TaskLogger.AddMessage($"Library has '{LibraryBlock.Name}' on sheet '{LibrarySheetName}'.  Document does not have a sheet with that name.")
                        End If
                    End If

                End If
            Next
        Next
    End Sub

    Private Sub DoWSAddByOrder(
        BlockLibraryDoc As SolidEdgeDraft.DraftDocument,
        LibraryBlock As SolidEdgeDraft.Block,
        SEDoc As SolidEdgeDraft.DraftDocument,
        DocBlock As SolidEdgeDraft.Block)

        ' Get library and document working sheets
        Dim LibrarySheetList = GetSheetList(BlockLibraryDoc, SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection)
        Dim DocSheetList = GetSheetList(SEDoc, SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection)

        If DocSheetList.Count > LibrarySheetList.Count Then
            TaskLogger.AddMessage($"Cannot process {DocSheetList.Count} file sheets with {LibrarySheetList.Count} library sheets")
            Exit Sub
        End If

        ' Rearrange sheets in tab order, not the order they were added to the file.
        LibrarySheetList = SheetListInTabOrder(LibrarySheetList)
        DocSheetList = SheetListInTabOrder(DocSheetList)

        ' Iterate through the sheet pairs
        For i As Integer = 0 To DocSheetList.Count - 1
            Dim LibrarySheet As SolidEdgeDraft.Sheet = LibrarySheetList(i)
            Dim DocSheet As SolidEdgeDraft.Sheet = DocSheetList(i)

            For Each LibraryBlockOccurrence As SolidEdgeDraft.BlockOccurrence In LibrarySheet.BlockOccurrences
                If LibraryBlockOccurrence.Block Is LibraryBlock Then
                    PlaceBlockOccurrence(LibraryBlock, DocBlock, LibraryBlockOccurrence, DocSheet)
                End If
            Next
        Next

    End Sub

    Private Sub DoWSAddToAll(
        BlockLibraryDoc As SolidEdgeDraft.DraftDocument,
        LibraryBlock As SolidEdgeDraft.Block,
        SEDoc As SolidEdgeDraft.DraftDocument,
        DocBlock As SolidEdgeDraft.Block)

        ' Get library and document working sheets
        Dim LibrarySheetList = GetSheetList(BlockLibraryDoc, SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection)
        Dim DocSheetList = GetSheetList(SEDoc, SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection)

        If Not LibrarySheetList.Count = 1 Then
            TaskLogger.AddMessage($"Library must have exactly 1 working sheet, not {LibrarySheetList.Count}")
            Exit Sub
        End If

        Dim LibrarySheet As SolidEdgeDraft.Sheet = LibrarySheetList(0)
        Dim LibrarySheetSize As SolidEdgeDraft.PaperSizeConstants = LibrarySheet.SheetSetup.SheetSizeOption

        For Each DocSheet As SolidEdgeDraft.Sheet In DocSheetList
            Dim DocSheetSize As SolidEdgeDraft.PaperSizeConstants = DocSheet.SheetSetup.SheetSizeOption
            If DocSheetSize = LibrarySheetSize Then
                For Each LibraryBlockOccurrence As SolidEdgeDraft.BlockOccurrence In LibrarySheet.BlockOccurrences
                    If LibraryBlockOccurrence.Block Is LibraryBlock Then
                        PlaceBlockOccurrence(LibraryBlock, DocBlock, LibraryBlockOccurrence, DocSheet)
                    End If
                Next
            Else
                Dim s As String = $"File sheet '{DocSheet.Name}' not the same size as library sheet '{LibrarySheet.Name}'"
                If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
            End If
        Next

    End Sub

    Private Sub DoBSAddByName(
        BlockLibraryDoc As SolidEdgeDraft.DraftDocument,
        LibraryBlock As SolidEdgeDraft.Block,
        SEDoc As SolidEdgeDraft.DraftDocument,
        DocBlock As SolidEdgeDraft.Block)

        ' Gather up all library and document background sheets
        Dim LibrarySheetDict = GetSheetDict(BlockLibraryDoc, SolidEdgeDraft.SheetSectionTypeConstants.igBackgroundSection)
        Dim DocSheetDict = GetSheetDict(SEDoc, SolidEdgeDraft.SheetSectionTypeConstants.igBackgroundSection)

        'Dim DocSheetNamesProcessed As New List(Of String)

        For Each LibrarySheetName As String In LibrarySheetDict.Keys

            Dim LibrarySheetSize As SolidEdgeDraft.PaperSizeConstants = LibrarySheetDict(LibrarySheetName).SheetSetup.SheetSizeOption

            ' Does the LibrarySheet contain any of the block occurrence we're looking for?
            For Each LibraryBlockOccurrence As SolidEdgeDraft.BlockOccurrence In LibrarySheetDict(LibrarySheetName).BlockOccurrences
                If LibraryBlockOccurrence.Block Is LibraryBlock Then
                    ' Found one.  Does SEDoc have a sheet with the same name?
                    If DocSheetDict.Keys.Contains(LibrarySheetName) Then

                        ' Set parameters from the library occurrence and add.
                        Dim DocSheet As SolidEdgeDraft.Sheet = DocSheetDict(LibrarySheetName)

                        PlaceBlockOccurrence(LibraryBlock, DocBlock, LibraryBlockOccurrence, DocSheet)

                        'If Not DocSheetNamesProcessed.Contains(DocSheet.Name) Then DocSheetNamesProcessed.Add(DocSheet.Name)

                    ElseIf BSAddBySize Then
                        ' No sheet name match.  Try to match by size, if the option is set.
                        Dim DocSheet As SolidEdgeDraft.Sheet = Nothing
                        For Each DocSheetName As String In DocSheetDict.Keys
                            If DocSheetDict(DocSheetName).SheetSetup.SheetSizeOption = LibrarySheetSize Then
                                DocSheet = DocSheetDict(DocSheetName)
                                Exit For
                            End If
                        Next
                        If DocSheet IsNot Nothing Then
                            If BSRename Then DocSheet.Name = LibrarySheetName
                            PlaceBlockOccurrence(LibraryBlock, DocBlock, LibraryBlockOccurrence, DocSheet)
                            'If Not DocSheetNamesProcessed.Contains(DocSheet.Name) Then DocSheetNamesProcessed.Add(DocSheet.Name)
                        End If
                    Else

                    End If

                End If
            Next
        Next

        If Me.ReportMissingSheet Then
            '' Need a definition of what, if anything, to report.
            '' False alarm currently if a library sheet does not have the passed-in library block.
            '' DocSheetDict is currently not updated when a sheet is renamed.

            'Dim s As String = ""
            'For Each DocSheetName In DocSheetDict.Keys
            '    If Not DocSheetNamesProcessed.Contains(DocSheetName) Then
            '        If s = "" Then
            '            s = DocSheetName
            '        Else
            '            s = $"{s}, {DocSheetName}"
            '        End If
            '    End If
            'Next
            'If Not s = "" Then
            '    s = $"The following sheets were not processed: {s}"
            '    If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
            'End If

        End If
    End Sub


    Private Sub PlaceBlockOccurrence(
        LibraryBlock As SolidEdgeDraft.Block,
        DocBlock As SolidEdgeDraft.Block,
        LibraryBlockOccurrence As SolidEdgeDraft.BlockOccurrence,
        DocSheet As SolidEdgeDraft.Sheet)

        Try
            Dim XOrigin As Double
            Dim YOrigin As Double
            LibraryBlockOccurrence.GetOrigin(XOrigin, YOrigin)
            Dim BlockViewName As String = LibraryBlockOccurrence.BlockView.Name
            Dim Scale As Double = LibraryBlockOccurrence.ScaleFactor
            Dim Rotation As Double = LibraryBlockOccurrence.RotationAngle

            ' Check for a duplicate block on the sheet
            Dim HasDuplicate As Boolean = False
            Dim DifferenceThreshold As Double = 0.0001
            For Each tmpBO As SolidEdgeDraft.BlockOccurrence In DocSheet.BlockOccurrences
                If Not tmpBO.Block.Name = LibraryBlockOccurrence.Block.Name Then Continue For
                If Not BlockViewName = tmpBO.BlockView.Name Then Continue For
                Dim tmpX As Double
                Dim tmpY As Double
                tmpBO.GetOrigin(tmpX, tmpY)
                If Math.Abs(XOrigin - tmpX) > DifferenceThreshold Then Continue For
                If Math.Abs(YOrigin - tmpY) > DifferenceThreshold Then Continue For
                If Math.Abs(Scale - tmpBO.ScaleFactor) > DifferenceThreshold Then Continue For
                If Math.Abs(Rotation - tmpBO.RotationAngle) > DifferenceThreshold Then Continue For
                ' If we get this far, all variable differences are below the threshold -> a duplicate block
                HasDuplicate = True
                Exit For
            Next

            If Not HasDuplicate Then
                DocSheet.BlockOccurrences.Add(DocBlock.Name, XOrigin, YOrigin, BlockViewName, Scale, Rotation)
            Else
                ' Not an error.  A duplicate would have been updated earlier in the AddBlocks process.
                'Dim s As String = $"Duplicate of {LibraryBlock.Name} found on {DocSheet.Name}"
                'If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
            End If

        Catch ex As Exception
            Dim s As String = $"Unable to add {LibraryBlock.Name} to {DocSheet.Name}.  Exception: {ex.Message}"
            If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
        End Try

    End Sub

    Private Function GetSheetDict(
        SEDoc As SolidEdgeDraft.DraftDocument,
        SectionType As SolidEdgeDraft.SheetSectionTypeConstants
        ) As Dictionary(Of String, SolidEdgeDraft.Sheet)

        Dim SheetDict As New Dictionary(Of String, SolidEdgeDraft.Sheet)
        For Each Sheet As SolidEdgeDraft.Sheet In SEDoc.Sheets
            If Sheet.SectionType = SectionType Then
                SheetDict(Sheet.Name) = Sheet
            End If
        Next

        Return SheetDict
    End Function

    Private Function GetSheetList(
        SEDoc As SolidEdgeDraft.DraftDocument,
        SectionType As SolidEdgeDraft.SheetSectionTypeConstants
        ) As List(Of SolidEdgeDraft.Sheet)

        Dim SheetList As New List(Of SolidEdgeDraft.Sheet)
        For Each Sheet As SolidEdgeDraft.Sheet In SEDoc.Sheets
            If Sheet.SectionType = SectionType Then
                SheetList.Add(Sheet)
            End If
        Next

        Return SheetList
    End Function

    Private Function SheetListInTabOrder(
        SheetList As List(Of SolidEdgeDraft.Sheet)
        ) As List(Of SolidEdgeDraft.Sheet)

        If SheetList.Count = 1 Then Return SheetList

        Dim OutList As New List(Of SolidEdgeDraft.Sheet)
        Dim OutDict As New Dictionary(Of Integer, SolidEdgeDraft.Sheet)

        For Each Sheet As SolidEdgeDraft.Sheet In SheetList
            OutDict(Sheet.Number) = Sheet
        Next

        For i As Integer = 1 To OutDict.Count
            OutList.Add(OutDict(i))
        Next

        Return OutList
    End Function


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
                    ' Found one.  Does SEDoc have a sheet with the same name?
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

                            ' ###### TODO: Check for a duplicate block ######
                            DocSheet.BlockOccurrences.Add(DocBlock.Name, XOrigin, YOrigin, BlockViewName, Scale, Rotation)

                        Catch ex As Exception
                            TaskLogger.AddMessage($"Unable to add {LibraryBlock.Name} to {DocSheet.Name}.  Exception: {ex.Message}")
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


    Public Sub UpdateDGVSize(DGV As DataGridView)
        DGV.Height = (DGV.Rows(0).Height + 1) * (DGV.Rows.Count + 2)
    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Button As Button
        Dim TextBox As TextBox
        Dim Label As Label
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

        CheckBox = FormatOptionsCheckBox(ControlNames.StructuredStorageEdit.ToString, "Read blocks without Solid Edge")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ReplaceBlocks.ToString, "Replace blocks")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ReplaceBlocksReplaceExisting.ToString, "Overwrite existing with replacement")
        CheckBox.Padding = New Padding(15, 0, 0, 0)
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
        DataGridView = FormatOptionsDataGridView(ControlNames.ReplaceBlocksDGV.ToString, ColumnHeaders, ColumnType, Me.BlockLibraryBlockNames)
        'DataGridView.Padding = New Padding(15, 0, 0, 0)
        DataGridView.Margin = New Padding(15, 0, 0, 0)
        AddHandler DataGridView.CellClick, AddressOf DataGridViewOptions_CellClick
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
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
        DataGridView = FormatOptionsDataGridView(ControlNames.DeleteBlocksDGV.ToString, ColumnHeaders, ColumnType, Me.BlockLibraryBlockNames)
        DataGridView.Margin = New Padding(15, 0, 0, 0)
        AddHandler DataGridView.CellClick, AddressOf DataGridViewOptions_CellClick
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
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

        CheckBox = FormatOptionsCheckBox(ControlNames.AddBlocks.ToString, "Add blocks")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        'CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AddBlocksReplaceExisting.ToString, "Overwrite existing with added block")
        CheckBox.Padding = New Padding(15, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ReportMissingSheet.ToString, "Report missing sheet in document")
        CheckBox.Padding = New Padding(15, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False



        RowIndex += 1

        Label = FormatOptionsLabel(ControlNames.WorkingSheetLabel.ToString, "Working sheet options")
        Label.Padding = New Padding(15, 0, 0, 0)
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(Label, 2)
        ControlsDict(Label.Name) = Label
        Label.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.WSAddByName.ToString, "Add by sheet name")
        CheckBox.Padding = New Padding(30, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.WSAddByOrder.ToString, "Add by sheet order")
        CheckBox.Padding = New Padding(30, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.WSAddToAll.ToString, "Add to all sheets")
        CheckBox.Padding = New Padding(30, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False


        RowIndex += 1

        Label = FormatOptionsLabel(ControlNames.BackgroundSheetLabel.ToString, "Background sheet options")
        Label.Padding = New Padding(15, 0, 0, 0)
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(Label, 2)
        ControlsDict(Label.Name) = Label
        Label.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BSAddByName.ToString, "Add by sheet name (always enabled)")
        CheckBox.Padding = New Padding(30, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False
        CheckBox.Checked = True

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BSAddBySize.ToString, "If no name match: Add by size")
        CheckBox.Padding = New Padding(30, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BSRename.ToString, "If added by size: Rename sheet")
        CheckBox.Padding = New Padding(30, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False




        RowIndex += 1

        ColumnHeaders = {"Library block name"}.ToList
        ColumnType = "Combobox"
        DataGridView = FormatOptionsDataGridView(ControlNames.AddBlocksDGV.ToString, ColumnHeaders, ColumnType, Me.BlockLibraryBlockNames)
        DataGridView.Margin = New Padding(15, 0, 0, 0)
        AddHandler DataGridView.CellClick, AddressOf DataGridViewOptions_CellClick
        AddHandler DataGridView.Leave, AddressOf DataGridViewOptions_Leave
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

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        'RemoveBlankDGVRows()

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
                If Me.DeleteBlocksList Is Nothing OrElse Me.DeleteBlocksList.Count = 0 Then
                    ErrorLogger.AddMessage("Enter at least one block to delete OR disable the option")
                End If
            End If

            If Me.AddBlocks Then
                If Me.AddBlocksList Is Nothing OrElse Me.AddBlocksList.Count = 0 Then
                    ErrorLogger.AddMessage("Enter at least one block to add OR disable the option")
                End If

                If Not (Me.WSAddByName Or Me.WSAddByOrder Or Me.WSAddToAll) Then
                    If NewWay Then
                        ErrorLogger.AddMessage("Select a working sheet option")
                    End If
                End If

            End If
        End If

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
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
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

        Dim ParticipatingWorkingSheetCheckBoxes As New List(Of CheckBox)
        ParticipatingWorkingSheetCheckBoxes.Add(CType(ControlsDict(ControlNames.WSAddByName.ToString), CheckBox))
        ParticipatingWorkingSheetCheckBoxes.Add(CType(ControlsDict(ControlNames.WSAddByOrder.ToString), CheckBox))
        ParticipatingWorkingSheetCheckBoxes.Add(CType(ControlsDict(ControlNames.WSAddToAll.ToString), CheckBox))

        'Dim ParticipatingBackgroundSheetCheckBoxes As New List(Of CheckBox)
        'ParticipatingBackgroundSheetCheckBoxes.Add(CType(ControlsDict(ControlNames.BSAddByName.ToString), CheckBox))
        'ParticipatingBackgroundSheetCheckBoxes.Add(CType(ControlsDict(ControlNames.BSAddBySize.ToString), CheckBox))
        'ParticipatingBackgroundSheetCheckBoxes.Add(CType(ControlsDict(ControlNames.BSRename.ToString), CheckBox))

        Select Case Name

            Case ControlNames.StructuredStorageEdit.ToString
                Me.StructuredStorageEdit = Checkbox.Checked

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

                If NewWay Then
                    CType(ControlsDict(ControlNames.WorkingSheetLabel.ToString), Label).Visible = Me.AddBlocks
                    CType(ControlsDict(ControlNames.WSAddByName.ToString), CheckBox).Visible = Me.AddBlocks
                    CType(ControlsDict(ControlNames.WSAddByOrder.ToString), CheckBox).Visible = Me.AddBlocks
                    CType(ControlsDict(ControlNames.WSAddToAll.ToString), CheckBox).Visible = Me.AddBlocks

                    CType(ControlsDict(ControlNames.BackgroundSheetLabel.ToString), Label).Visible = Me.AddBlocks
                    CType(ControlsDict(ControlNames.BSAddByName.ToString), CheckBox).Visible = Me.AddBlocks
                    CType(ControlsDict(ControlNames.BSAddBySize.ToString), CheckBox).Visible = Me.AddBlocks
                    CType(ControlsDict(ControlNames.BSRename.ToString), CheckBox).Visible = Me.AddBlocks

                End If


                CType(ControlsDict(ControlNames.AddBlocksDGV.ToString), DataGridView).Width = TaskOptionsTLP.Width - 5

            Case ControlNames.AddBlocksReplaceExisting.ToString
                Me.AddBlocksReplaceExisting = Checkbox.Checked

            Case ControlNames.ReportMissingSheet.ToString
                Me.ReportMissingSheet = Checkbox.Checked

            Case ControlNames.WSAddByName.ToString
                Me.WSAddByName = Checkbox.Checked
                If Me.WSAddByName Then
                    HandleMutuallyExclusiveCheckBoxes(TaskOptionsTLP, Checkbox, ParticipatingWorkingSheetCheckBoxes)
                End If

            Case ControlNames.WSAddByOrder.ToString
                Me.WSAddByOrder = Checkbox.Checked
                If Me.WSAddByOrder Then
                    HandleMutuallyExclusiveCheckBoxes(TaskOptionsTLP, Checkbox, ParticipatingWorkingSheetCheckBoxes)
                End If

            Case ControlNames.WSAddToAll.ToString
                Me.WSAddToAll = Checkbox.Checked
                If Me.WSAddToAll Then
                    HandleMutuallyExclusiveCheckBoxes(TaskOptionsTLP, Checkbox, ParticipatingWorkingSheetCheckBoxes)
                End If

            Case ControlNames.BSAddByName.ToString
                Me.BSAddByName = True

            Case ControlNames.BSAddBySize.ToString
                Me.BSAddBySize = Checkbox.Checked

            Case ControlNames.BSRename.ToString
                Me.BSRename = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
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

                If IO.File.Exists(Me.BlockLibrary) Then
                    tmpFileDialog.InitialDirectory = IO.Path.GetDirectoryName(Me.BlockLibrary)
                Else
                    tmpFileDialog.InitialDirectory = Form_Main.SETemplatePath
                End If

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.BlockLibrary = tmpFileDialog.FileName
                    TextBox = CType(ControlsDict(ControlNames.BlockLibrary.ToString), TextBox)
                    TextBox.Text = Me.BlockLibrary

                    If Not Me.StructuredStorageEdit Then

                        ' ###### Bypassing populating block names using SE.  Slow and unexpected. ######
                        ' ###### User will need to Edit list > Update to populate the names. ######

                        'Form_Main.Cursor = Cursors.WaitCursor

                        'OleMessageFilter.Register()

                        'Dim USEA As New UtilsSEApp(Form_Main)
                        'USEA.ErrorLogger = New Logger("Update block library", Nothing)
                        'Form_Main.TextBoxStatus.Text = "Starting Solid Edge..."

                        'USEA.SEStart(RunInBackground:=False, UseCurrentSession:=True, NoUpdateMRU:=False, ProcessDraftsInactive:=False)

                        'Dim SEDoc As SolidEdgeDraft.DraftDocument = CType(USEA.SEApp.Documents.Open(Me.BlockLibrary), SolidEdgeDraft.DraftDocument)

                        'Dim Blocks As SolidEdgeDraft.Blocks = SEDoc.Blocks

                        'Dim tmpBlockLibraryBlockNames As New List(Of String)
                        'tmpBlockLibraryBlockNames.Add("")

                        'If Blocks IsNot Nothing Then
                        '    For Each Block As SolidEdgeDraft.Block In Blocks
                        '        tmpBlockLibraryBlockNames.Add(Block.Name)
                        '    Next
                        'End If

                        'tmpBlockLibraryBlockNames.Sort()
                        'Me.BlockLibraryBlockNames = tmpBlockLibraryBlockNames

                        'SEDoc.Close(False)

                        ''USEA.SEStop(UseCurrentSession:=True)
                        'USEA.SEStop(UseCurrentSession:=Form_Main.UseCurrentSession)

                        'OleMessageFilter.Revoke()

                        'Form_Main.Cursor = Cursors.Default
                    Else
                        'Dim SSDoc As New HCStructuredStorageDoc(Me.BlockLibrary, _OpenReadWrite:=False)
                        'SSDoc.ReadBlockLibrary()

                        'Me.BlockLibraryBlockNames = SSDoc.GetBlockLibraryBlockNames
                        'Me.BlockLibraryBlockNames.Sort()

                        'If SSDoc IsNot Nothing Then SSDoc.Close()

                        Dim SSDoc As HCStructuredStorageDoc = Nothing
                        Try
                            SSDoc = New HCStructuredStorageDoc(Me.BlockLibrary, _OpenReadWrite:=False)
                            SSDoc.ReadBlockLibrary()

                            Dim tmpBlockLibraryBlockNames As List(Of String) = SSDoc.GetBlockLibraryBlockNames
                            tmpBlockLibraryBlockNames.Sort()
                            Me.BlockLibraryBlockNames = tmpBlockLibraryBlockNames
                        Catch ex As Exception
                            If SSDoc IsNot Nothing Then SSDoc.Close()
                            'Me.BlockLibraryBlockNames.Clear()
                            Me.BlockLibraryBlockNames = New List(Of String) ' Triggers update of the form DGV

                            MsgBox($"Could not read blocks from {IO.Path.GetFileName(Me.BlockLibrary)}", vbOKOnly)
                        End Try

                        If SSDoc IsNot Nothing Then SSDoc.Close()
                    End If

                    'Form_Main.WorkingFilesPath = IO.Path.GetDirectoryName(Me.BlockLibrary)
                End If

            Case ControlNames.EditBlockList.ToString
                Dim FBLBN As New FormBlockLibraryBlockNames()

                FBLBN.BlockLibraryBlockNames = Me.BlockLibraryBlockNames
                FBLBN.ManuallyAddedBlockNames = Me.ManuallyAddedBlockNames
                FBLBN.BlockLibrary = Me.BlockLibrary
                FBLBN.StructuredStorageEdit = Me.StructuredStorageEdit

                Dim Result As DialogResult = FBLBN.ShowDialog

                If Result = DialogResult.OK Then
                    Me.BlockLibraryBlockNames = FBLBN.BlockLibraryBlockNames
                    Me.ManuallyAddedBlockNames = FBLBN.ManuallyAddedBlockNames

                End If

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
        End Select


    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name
            Case ControlNames.BlockLibrary.ToString
                Me.BlockLibrary = TextBox.Text

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Adds, replaces and/or deletes blocks in a draft file. "

        HelpString += vbCrLf + vbCrLf + "![UpdateBlocks](My%20Project/media/task_update_blocks.png)"

        HelpString += vbCrLf + vbCrLf + "For adding and replacing, a draft file containing the new blocks is required.  "
        HelpString += "In most cases it will simply be your draft template.  "
        HelpString += "Click the `Block Library` button to select it. "

        HelpString += vbCrLf + vbCrLf + "Known blocks are available in combo boxes on each list. "
        HelpString += "To populate them, click the `Edit list` button. "
        HelpString += "To get the library blocks, click `Update`. "
        HelpString += "If you have files with block names not found in the library, "
        HelpString += "enter them on the `File Blocks` list.  "

        HelpString += vbCrLf + vbCrLf + "Note the program needs to start Solid Edge to do the update. "
        HelpString += "Unless you enable the `Read blocks without SE` option, that is. "
        HelpString += "In that case it uses the much faster Structured Storage functionality. "

        HelpString += vbCrLf + vbCrLf + "There are a couple of things to note about working with the block lists. "
        HelpString += "First, you may find yourself clicking a drop down twice to choose an item. "
        HelpString += "These combo boxes are picky -- you have to click the down arrow, "
        HelpString += "not the text field, to open the drop down on the first click. "
        HelpString += "Second, to remove a row's contents, "
        HelpString += "select the `Row Header` (the gray box left of the text) and hit `Delete`. "
        HelpString += "To clear the entire list, select the top-most `Row Header` and do the same.  "

        HelpString += vbCrLf + vbCrLf + "This command has several options.  They are described next.  "

        HelpString += vbCrLf + "- `Replace Blocks` `Overwrite existing with replacement`: "
        HelpString += "This is confusing; the point of the command is to overwrite blocks, right? "
        HelpString += "There is an ambiguity, however. "
        HelpString += "Say you want to replace `Block1` in the file with `Block2` in the library. "
        HelpString += "It's clear what will happen to `Block1`.  But what if there is already a `Block2` in the file? "
        HelpString += "This option tells the program how to proceed in that case. "

        HelpString += vbCrLf + "- `Add Blocks` `Overwrite existing with added block`: "
        HelpString += "Similar to above, this is for when you're adding `Block1` to the file, but it already has one with that name.  "

        HelpString += vbCrLf + "- `Add Blocks` `Report missing sheet in document`: "
        HelpString += "The Solid Edge `Add Blocks` command updates the file's library, "
        HelpString += "but does not add it to drawing sheets. "
        HelpString += "Since you, of course, want it on drawings, the program checks each sheet of the library "
        HelpString += "and places an occurrence on the corresponding sheet in the file, "
        HelpString += "at the same location, scale, and rotation as the original.  "
        HelpString += "If the file does not have a corresponding sheet, enable this option "
        HelpString += "to have it reported in the log file.  "

        HelpString += vbCrLf + "- `Add Blocks` `Working sheets add by name`: "
        HelpString += "This option, and those below, set how corresponding sheets (described above) are determined.  "
        HelpString += "This one selects sheets with the same name in the template and the file.  "

        HelpString += vbCrLf + "- `Add Blocks` `Working sheets add by order`: "
        HelpString += "Selects sheets in the same order in both files.  "

        HelpString += vbCrLf + "- `Add Blocks` `Working sheets add to all`: "
        HelpString += "Selects all sheets in the file.  Only one working sheet is allowed in the template.  "
        HelpString += "This is probably the most practical option.  "
        HelpString += "It handles files with multiple working sheets.  "

        HelpString += vbCrLf + "- `Add Blocks` `Background sheets add by name`: "
        HelpString += "Selects background sheets with the same name in both files.  This option cannot be disabled.  "

        HelpString += vbCrLf + "- `Add Blocks` `Background sheets add by size`: "
        HelpString += "If no name match is found, match by sheet size.  "

        HelpString += vbCrLf + "- `Add Blocks` `Background sheets rename`: "
        HelpString += "If a size match was used, rename the file's sheet to that of the template.  "

        Return HelpString
    End Function



End Class
