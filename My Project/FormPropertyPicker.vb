Option Strict On

Public Class FormPropertyPicker
    Public Property TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property TemplatePropertyList As List(Of String)
    Public Property PropertyString As String
    Public Property PropertyOnly As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.PropertyOnly = True
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        If Me.PropertyOnly Then
            Me.PropertyString = String.Format("%{{{0}}}", ComboBoxProperties.Text)
        Else
            Me.PropertyString = String.Format("%{{{0}|R1}}", ComboBoxProperties.Text)
        End If
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ComboBoxProperties_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxProperties.SelectedIndexChanged

    End Sub

    Private Sub ButtonPropOnly_Click(sender As Object, e As EventArgs) Handles ButtonPropOnly.Click
        Me.PropertyOnly = True

        ButtonPropOnly.Image = My.Resources.Checked
        ButtonPropAndIndex.Image = My.Resources.Unchecked

        'Me.PropertyString = String.Format("%{{{0}}}", ComboBoxProperties.Text)
    End Sub

    Private Sub ButtonPropAndIndex_Click(sender As Object, e As EventArgs) Handles ButtonPropAndIndex.Click
        Me.PropertyOnly = False

        ButtonPropOnly.Image = My.Resources.Unchecked
        ButtonPropAndIndex.Image = My.Resources.Checked

        'Me.PropertyString = String.Format("%{{{0}}}", ComboBoxProperties.Text)
    End Sub

    Private Sub ButtonShowAll_Click(sender As Object, e As EventArgs) Handles ButtonShowAll.Click
        If ButtonShowAll.Checked Then
            ButtonShowAll.Image = My.Resources.Checked

            ComboBoxProperties.Items.Clear()
            'ComboBoxProperties.Items.Add("")
            For Each Key As String In Me.TemplatePropertyDict.Keys
                ComboBoxProperties.Items.Add(Key)
            Next
            ComboBoxProperties.Text = ComboBoxProperties.Items(0).ToString
        Else
            ButtonShowAll.Image = My.Resources.Unchecked

            ComboBoxProperties.Items.Clear()
            'ComboBoxProperties.Items.Add("")
            For Each Key As String In Me.TemplatePropertyList
                ComboBoxProperties.Items.Add(Key)
            Next
            ComboBoxProperties.Text = ComboBoxProperties.Items(0).ToString
        End If
    End Sub

    Private Sub FormPropertyPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBoxProperties.Items.Add("")
        For Each Key As String In Me.TemplatePropertyList
            ComboBoxProperties.Items.Add(Key)
        Next
    End Sub
End Class