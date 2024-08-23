Option Strict On

Public Class UCPropertyFilter
    Public Property PropertyFilter As FormPropertyFilter

    Public Property Selected As Boolean
    Public Property Variable As String
    Public Property PropertySet As String
    Public Property PropertyName As String
    Public Property Comparison As String
    Public Property Value As String
    Public Property Formula As String

    Public Property NotifyPropertyFilter As Boolean
    Public Property TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property TemplatePropertyList As List(Of String)


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Selected = False
        Me.Variable = ""
        Me.PropertySet = ""
        Me.PropertyName = ""
        Me.Comparison = ""
        Me.Value = ""
        Me.Formula = ""
        Me.NotifyPropertyFilter = True

    End Sub

    Public Sub New(_PropertyFilter As FormPropertyFilter)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.PropertyFilter = _PropertyFilter
        Me.TemplatePropertyDict = PropertyFilter.TemplatePropertyDict
        Me.TemplatePropertyList = PropertyFilter.TemplatePropertyList

        ComboBoxPropertyName.Items.Add("")
        For Each s As String In TemplatePropertyList
            ComboBoxPropertyName.Items.Add(s)
        Next


        Me.Selected = False
        Me.Variable = ""
        Me.PropertySet = ""
        Me.PropertyName = ""
        Me.Comparison = ""
        Me.Value = ""
        Me.Formula = ""
        Me.NotifyPropertyFilter = True

    End Sub

    Private Sub CheckBoxSelect_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSelect.CheckedChanged
        Me.Selected = CheckBoxSelect.Checked
        Notify()
    End Sub

    Private Sub ComboBoxPropertySet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertySet.SelectedIndexChanged
        Me.PropertySet = ComboBoxPropertySet.Text
        Notify()
    End Sub

    Private Sub ComboBoxPropertyName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertyName.SelectedIndexChanged
        Me.PropertyName = ComboBoxPropertyName.Text

        If Me.TemplatePropertyDict.Keys.Contains(Me.PropertyName) Then
            Dim s As String = TemplatePropertyDict(Me.PropertyName)("PropertySet")

            If Not ((s = "") Or (s = "Custom")) Then
                s = "System"
            End If

            ComboBoxPropertySet.Text = s
        Else
            ComboBoxPropertySet.Text = ""
        End If

        Notify()
    End Sub

    Private Sub ComboBoxPropertyName_Leave(sender As Object, e As EventArgs) Handles ComboBoxPropertyName.Leave
        Me.PropertyName = ComboBoxPropertyName.Text

        If Me.TemplatePropertyDict.Keys.Contains(Me.PropertyName) Then
            Dim s As String = TemplatePropertyDict(Me.PropertyName)("PropertySet")

            If Not ((s = "") Or (s = "Custom")) Then
                s = "System"
            End If

            ComboBoxPropertySet.Text = s
        Else
            ComboBoxPropertySet.Text = ""
        End If

        Notify()
    End Sub

    Private Sub ComboBoxComparison_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxComparison.SelectedIndexChanged
        Me.Comparison = ComboBoxComparison.Text
        Notify()
    End Sub

    Private Sub TextBoxValue_TextChanged(sender As Object, e As EventArgs) Handles TextBoxValue.TextChanged
        Me.Value = TextBoxValue.Text
        Notify()
    End Sub

    Public Sub ReconcileFormWithProps()
        Me.NotifyPropertyFilter = False

        CheckBoxSelect.Checked = Me.Selected
        LabelVariable.Text = Me.Variable
        ComboBoxPropertySet.Text = Me.PropertySet
        ComboBoxPropertyName.Text = Me.PropertyName
        ComboBoxComparison.Text = Me.Comparison
        TextBoxValue.Text = Me.Value

        Me.NotifyPropertyFilter = True
    End Sub

    Public Sub Notify()
        If NotifyPropertyFilter Then
            PropertyFilter.UCChanged(Me)
        End If

    End Sub

End Class
