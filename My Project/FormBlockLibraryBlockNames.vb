Option Strict On

Public Class FormBlockLibraryBlockNames


    Private _BlockLibraryBlockNames As List(Of String)
    Public Property BlockLibraryBlockNames As List(Of String)
        Get
            Return _BlockLibraryBlockNames
        End Get
        Set(value As List(Of String))
            If value IsNot Nothing Then
                _BlockLibraryBlockNames = value
            Else
                _BlockLibraryBlockNames = New List(Of String)
            End If

            If Me.DataGridViewBlockLibraryBlockNames IsNot Nothing Then
                DataGridViewBlockLibraryBlockNames.Rows.Clear()
                For Each s As String In _BlockLibraryBlockNames
                    If Not s.Trim = "" Then
                        DataGridViewBlockLibraryBlockNames.Rows.Add(s)
                    End If
                Next

                Dim N As Integer = DataGridViewBlockLibraryBlockNames.Rows.Count
                If Not N = 0 Then
                    Dim H As Integer = DataGridViewBlockLibraryBlockNames.Rows(0).Height
                    Dim MaxVisibleRows As Integer = 12
                    If N < MaxVisibleRows Then
                        DataGridViewBlockLibraryBlockNames.Height = (N + 1) * (H)
                    Else
                        DataGridViewBlockLibraryBlockNames.Height = (MaxVisibleRows + 1) * (H)
                    End If
                End If

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
                _ManuallyAddedBlockNames = value
            Else
                _ManuallyAddedBlockNames = New List(Of String)
            End If

            If Me.DataGridViewManuallyAddedBlockNames IsNot Nothing Then
                DataGridViewManuallyAddedBlockNames.Rows.Clear()
                For Each s As String In _ManuallyAddedBlockNames
                    DataGridViewManuallyAddedBlockNames.Rows.Add(s)
                Next
            End If

            UpdateDGVSize(DataGridViewManuallyAddedBlockNames)

        End Set
    End Property

    Public Property BlockLibrary As String



    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' The calling routine reads and writes the block name lists

    End Sub

    Private Sub FormBlockLibraryBlockNames_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LabelStatus.Text = ""

        If BlockLibraryBlockNames IsNot Nothing AndAlso BlockLibraryBlockNames.Count > 0 Then
            DataGridViewBlockLibraryBlockNames.Rows.Clear()
            For Each BlockName As String In BlockLibraryBlockNames
                If Not BlockName.Trim = "" Then
                    DataGridViewBlockLibraryBlockNames.Rows.Add(BlockName)
                End If
            Next

            UpdateDGVSize(DataGridViewBlockLibraryBlockNames)

            DataGridViewBlockLibraryBlockNames.CurrentCell = DataGridViewBlockLibraryBlockNames.Rows(DataGridViewBlockLibraryBlockNames.Rows.Count - 1).Cells(0)
            DataGridViewBlockLibraryBlockNames.ClearSelection()
        End If

        If ManuallyAddedBlockNames IsNot Nothing AndAlso ManuallyAddedBlockNames.Count > 0 Then
            DataGridViewManuallyAddedBlockNames.Rows.Clear()
            For Each BlockName As String In ManuallyAddedBlockNames
                DataGridViewManuallyAddedBlockNames.Rows.Add(BlockName)
            Next

            UpdateDGVSize(DataGridViewManuallyAddedBlockNames)

            DataGridViewManuallyAddedBlockNames.CurrentCell = DataGridViewManuallyAddedBlockNames.Rows(DataGridViewManuallyAddedBlockNames.Rows.Count - 1).Cells(0)
            DataGridViewManuallyAddedBlockNames.ClearSelection()
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Form_Closing(sender As Object, e As EventArgs) Handles Me.FormClosing
        'Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ButtonUpdateLibrary_Click(sender As Object, e As EventArgs) Handles ButtonUpdateLibrary.Click

        If Not IO.File.Exists(Me.BlockLibrary) Then
            MsgBox($"Block library not found: '{Me.BlockLibrary}'", vbOKOnly, "Block Library Not Found")
            Exit Sub
        End If

        Dim USEA As New UtilsSEApp(Form_Main)

        LabelStatus.Text = "Starting Solid Edge..."

        USEA.SEStart(
            RunInBackground:=False,
            UseCurrentSession:=True,
            NoUpdateMRU:=False,
            ProcessDraftsInactive:=False)

        Dim SEDoc As SolidEdgeDraft.DraftDocument = CType(USEA.SEApp.Documents.Open(Me.BlockLibrary), SolidEdgeDraft.DraftDocument)

        Dim Blocks As SolidEdgeDraft.Blocks = SEDoc.Blocks

        Dim tmpBlockLibraryBlockNames As New List(Of String)
        tmpBlockLibraryBlockNames.Add("")

        If Blocks IsNot Nothing Then
            For Each Block As SolidEdgeDraft.Block In Blocks
                tmpBlockLibraryBlockNames.Add(Block.Name)
            Next
        End If

        Me.BlockLibraryBlockNames = tmpBlockLibraryBlockNames

        SEDoc.Close(False)

        USEA.SEStop(UseCurrentSession:=True)

        LabelStatus.Text = $"Found {BlockLibraryBlockNames.Count - 1} blocks in the library"

    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click

        ' Update lists
        Me.BlockLibraryBlockNames.Clear()
        Me.BlockLibraryBlockNames.Add("")

        For Each Row As DataGridViewRow In DataGridViewBlockLibraryBlockNames.Rows
            If Row.IsNewRow Then Continue For
            Dim s As String = Row.Cells(0).Value.ToString
            If s IsNot Nothing AndAlso Not s.Trim = "" Then
                If Not Me.BlockLibraryBlockNames.Contains(s) Then
                    Me.BlockLibraryBlockNames.Add(s)
                End If
            End If
            Me.BlockLibraryBlockNames.Sort()
        Next

        Me.ManuallyAddedBlockNames.Clear()
        'Me.ManuallyAddedBlockNames.Add("")

        For Each Row As DataGridViewRow In DataGridViewManuallyAddedBlockNames.Rows
            If Row.IsNewRow Then Continue For
            Dim s As String = Row.Cells(0).Value.ToString
            If s IsNot Nothing AndAlso Not s.Trim = "" Then
                If Not Me.ManuallyAddedBlockNames.Contains(s) Then
                    Me.ManuallyAddedBlockNames.Add(s)
                End If
            End If
            Me.ManuallyAddedBlockNames.Sort()
        Next

        Me.DialogResult = DialogResult.OK
    End Sub

    'Private Sub ButtonClearFileBlockList_Click(sender As Object, e As EventArgs)
    '    For i = DataGridViewManuallyAddedBlockNames.Rows.Count - 1 To 0 Step -1
    '        If DataGridViewManuallyAddedBlockNames.Rows(i).IsNewRow Then Continue For
    '        DataGridViewManuallyAddedBlockNames.Rows.RemoveAt(i)
    '    Next

    '    UpdateDGVSize(DataGridViewManuallyAddedBlockNames)

    'End Sub

    Public Sub UpdateDGVSize(DGV As DataGridView)
        DGV.Height = (DGV.Rows(0).Height + 1) * (DGV.Rows.Count + 2)
    End Sub


End Class