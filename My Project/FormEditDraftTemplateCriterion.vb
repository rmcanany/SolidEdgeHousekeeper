Option Strict On

Public Class FormEditDraftTemplateCriterion

    Private _DraftTemplateCriteria As List(Of String)
    Public Property DraftTemplateCriterion As List(Of String)
        Get
            Return _DraftTemplateCriteria
        End Get
        Set(value As List(Of String))
            _DraftTemplateCriteria = value
            If Me.IsHandleCreated Then
                Me.TextBoxPropertyFormula.Text = _DraftTemplateCriteria(0)
                Me.TextBoxValue.Text = _DraftTemplateCriteria(1)
                Me.TextBoxTemplate.Text = _DraftTemplateCriteria(2)
            End If
        End Set
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Me.DraftTemplateCriterion Is Nothing Then Me.DraftTemplateCriterion = New List(Of String)
        'Me.DraftTemplateCriteria.AddRange({"A", "B", "C"})
    End Sub

    Private Sub TextBoxPropertyFormula_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPropertyFormula.TextChanged
        If Me.IsHandleCreated And Me.DraftTemplateCriterion IsNot Nothing AndAlso Me.DraftTemplateCriterion.Count = 3 Then
            Dim tmpDraftTemplateCriteria = Me.DraftTemplateCriterion
            tmpDraftTemplateCriteria(0) = TextBoxPropertyFormula.Text
            Me.DraftTemplateCriterion = tmpDraftTemplateCriteria
        End If
    End Sub

    Private Sub TextBoxValue_TextChanged(sender As Object, e As EventArgs) Handles TextBoxValue.TextChanged
        If Me.IsHandleCreated And Me.DraftTemplateCriterion IsNot Nothing AndAlso Me.DraftTemplateCriterion.Count = 3 Then
            Me.DraftTemplateCriterion(1) = TextBoxValue.Text
        End If
    End Sub

    Private Sub TextBoxTemplate_TextChanged(sender As Object, e As EventArgs) Handles TextBoxTemplate.TextChanged
        If Me.IsHandleCreated And Me.DraftTemplateCriterion IsNot Nothing AndAlso Me.DraftTemplateCriterion.Count = 3 Then
            Me.DraftTemplateCriterion(2) = TextBoxTemplate.Text
        End If
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.DraftTemplateCriterion = Me.DraftTemplateCriterion  ' Updates fields on form
    End Sub

    Private Sub ButtonPropertyFormula_Click(sender As Object, e As EventArgs) Handles ButtonPropertyFormula.Click

        'Dim CaretPosition = TextBoxPropertyFormula.Text.Length
        Dim CaretPosition = TextBoxPropertyFormula.SelectionStart

        Dim FPP As New FormPropertyPicker

        FPP.ShowDialog()

        If FPP.DialogResult = DialogResult.OK Then
            TextBoxPropertyFormula.Text = TextBoxPropertyFormula.Text.Insert(CaretPosition, FPP.PropertyString)
        End If

    End Sub

    Private Sub ButtonTemplate_Click(sender As Object, e As EventArgs) Handles ButtonTemplate.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a draft template file"
        tmpFileDialog.Filter = "dft files|*.dft"

        tmpFileDialog.InitialDirectory = Form_Main.SETemplatePath

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            TextBoxTemplate.Text = tmpFileDialog.FileName
        End If

    End Sub
End Class