Option Strict On

Public Class FormBlockLibraryBlockNames

    Public Property BlockLibraryBlockNames As List(Of String)
    Public Property FormTop As Integer
    Public Property FormLeft As Integer

    Private _MaxNumChars As String
    Public Property MaxNumChars As String
        Get
            Return _MaxNumChars
        End Get
        Set(value As String)
            _MaxNumChars = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                TextBoxMaxNumChars.Text = value
            End If
        End Set
    End Property

    Private _MaxNumBlockViews As String
    Public Property MaxNumBlockViews As String
        Get
            Return _MaxNumBlockViews
        End Get
        Set(value As String)
            _MaxNumBlockViews = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                TextBoxMaxNumBlockViews.Text = value
            End If
        End Set
    End Property


    Public Sub New(_BlockLibraryBlockNames As List(Of String))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.BlockLibraryBlockNames = _BlockLibraryBlockNames

        'Me.StartPosition = Form_Main.StartPosition
        If Not (Form_Main.FBLBNHeight = 0 Or Form_Main.FBLBNWidth = 0) Then
            Me.Top = Form_Main.FBLBNTop
            Me.Left = Form_Main.FBLBNLeft
            Me.Height = Form_Main.FBLBNHeight
            Me.Width = Form_Main.FBLBNWidth
        End If
    End Sub

    Private Sub FormBlockLibraryBlockNames_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Rows.Clear()

        For i = 0 To BlockLibraryBlockNames.Count - 1
            DataGridView1.Rows.Add(BlockLibraryBlockNames(i))
        Next

        DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(0)
        DataGridView1.ClearSelection()

    End Sub

    Private Sub DataGridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyUp
        If e.Control And e.KeyCode = Keys.C Then
            If DataGridView1.SelectedRows IsNot Nothing Then
                Dim idx As Integer = DataGridView1.SelectedRows(0).Index
                Clipboard.SetText(Me.BlockLibraryBlockNames(idx))
                Dim i = 0
            End If
        End If

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Form_Main.FBLBNTop = Me.Top
        Form_Main.FBLBNLeft = Me.Left
        Form_Main.FBLBNHeight = Me.Height
        Form_Main.FBLBNWidth = Me.Width

        Me.Dispose()
    End Sub

    Private Sub Form_Closing(sender As Object, e As EventArgs) Handles Me.FormClosing
        Form_Main.FBLBNTop = Me.Top
        Form_Main.FBLBNLeft = Me.Left
        Form_Main.FBLBNHeight = Me.Height
        Form_Main.FBLBNWidth = Me.Width

        Me.Dispose()
    End Sub

    Private Sub TextBoxMaxNumBlockViews_TextChanged(sender As Object, e As EventArgs) Handles TextBoxMaxNumBlockViews.TextChanged

    End Sub

    Private Sub TextBoxMaxNumChars_TextChanged(sender As Object, e As EventArgs) Handles TextBoxMaxNumChars.TextChanged

    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim i = 0
    End Sub

End Class