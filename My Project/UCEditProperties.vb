Option Strict On

Public Class UCEditProperties

    Public Property PropertyEditor As FormPropertyInputEditor
    Public Property Selected As Boolean
    Public Property PropertySet As String
    Public Property PropertyName As String
    Public Property FindSearch As String
    Public Property FindString As String
    Public Property ReplaceSearch As String
    Public Property ReplaceString As String
    Public Property NotifyPropertyEditor As Boolean
    Public Property TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property TemplatePropertyList As List(Of String)


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Selected = False
        Me.PropertySet = ""
        Me.PropertyName = ""
        Me.FindSearch = ""
        Me.FindString = ""
        Me.ReplaceSearch = ""
        Me.ReplaceString = ""
        Me.NotifyPropertyEditor = True

    End Sub

    Public Sub New(_PropertyEditor As FormPropertyInputEditor)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.PropertyEditor = _PropertyEditor
        Me.TemplatePropertyDict = PropertyEditor.TemplatePropertyDict
        Me.TemplatePropertyList = PropertyEditor.TemplatePropertyList

        ComboBoxPropertyName.Items.Add("")
        For Each s As String In TemplatePropertyList
            ComboBoxPropertyName.Items.Add(s)
        Next


        Me.Selected = False
        Me.PropertySet = ""
        Me.PropertyName = ""
        Me.FindSearch = ""
        Me.FindString = ""
        Me.ReplaceSearch = ""
        Me.ReplaceString = ""
        Me.NotifyPropertyEditor = True

    End Sub

    Private Sub CheckBoxSelect_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSelect.CheckedChanged
        Me.Selected = CheckBoxSelect.Checked
        Notify()
    End Sub

    Private Sub ComboBoxPropertySet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertySet.SelectedIndexChanged
        Me.PropertySet = ComboBoxPropertySet.Text
        Notify()
    End Sub

    'Private Sub ComboBoxPropertyName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertyName.SelectedIndexChanged
    '    Me.PropertyName = ComboBoxPropertyName.Text
    '    Notify()
    'End Sub

    Private Sub ComboBoxPropertyName_Leave(sender As Object, e As EventArgs) Handles ComboBoxPropertyName.Leave
        Me.PropertyName = ComboBoxPropertyName.Text

        If Not IsNothing(Me.TemplatePropertyDict) Then
            If Me.TemplatePropertyDict.Keys.Contains(Me.PropertyName) Then
                Dim s As String = TemplatePropertyDict(Me.PropertyName)("PropertySet")

                If Not ((s = "") Or (s = "Custom")) Then
                    s = "System"
                End If

                ComboBoxPropertySet.Text = s
            End If
        End If

        Notify()
    End Sub

    Private Sub ComboBoxFindType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxFindSearch.SelectedIndexChanged
        Me.FindSearch = ComboBoxFindSearch.Text

        Dim tf = Not Me.FindSearch = "X"

        TextBoxFindString.Enabled = tf
        ComboBoxReplaceSearch.Enabled = tf
        TextBoxReplaceString.Enabled = tf
        Notify()
    End Sub

    Private Sub TextBoxFindString_TextChanged(sender As Object, e As EventArgs) Handles TextBoxFindString.TextChanged
        Me.FindString = TextBoxFindString.Text
        Notify()
    End Sub

    Private Sub ComboBoxReplaceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxReplaceSearch.SelectedIndexChanged
        Me.ReplaceSearch = ComboBoxReplaceSearch.Text
        Notify()
    End Sub

    Private Sub TextBoxReplaceString_TextChanged(sender As Object, e As EventArgs) Handles TextBoxReplaceString.TextChanged
        Me.ReplaceString = TextBoxReplaceString.Text
        Notify()
    End Sub

    Public Sub ReconcileFormWithProps()
        Me.NotifyPropertyEditor = False

        CheckBoxSelect.Checked = Me.Selected
        ComboBoxPropertySet.Text = Me.PropertySet
        ComboBoxPropertyName.Text = Me.PropertyName
        ComboBoxFindSearch.Text = Me.FindSearch
        TextBoxFindString.Text = Me.FindString
        ComboBoxReplaceSearch.Text = Me.ReplaceSearch
        TextBoxReplaceString.Text = Me.ReplaceString

        Me.NotifyPropertyEditor = True
    End Sub

    Public Sub Notify()
        If NotifyPropertyEditor Then
            PropertyEditor.UCChanged(Me)
        End If

    End Sub

End Class
