Option Strict On

Public Class FormPropertyListCustomizeManualEntry

    Public Property PropertySet As String
    Public Property PropertyName As String
    Public Property EnglishName As String
    Private Property LocalizedEnglishNames As List(Of String)


    Public Sub New(_PropertiesData As HCPropertiesData)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.LocalizedEnglishNames = _PropertiesData.GetLocalizedEnglishNames()

        ComboBoxEnglishName.Items.Add("")
        For Each Item As String In Me.LocalizedEnglishNames
            ComboBoxEnglishName.Items.Add(Item)
        Next
    End Sub

    Private Function CheckInputs() As Boolean
        Dim InputsOK As Boolean = True
        Dim s As String = ""
        Dim indent As String = "    "

        If Me.PropertySet = "" Then
            s = String.Format("{0}Select a Property Set{1}{2}", s, indent, vbCrLf)
        End If

        If Me.PropertyName = "" Then
            s = String.Format("{0}Enter a Property Name{1}{2}", s, indent, vbCrLf)
        End If

        If Not s = "" Then
            InputsOK = False
            s = String.Format("Please correct the following before continuing{0}{1}", vbCrLf, s)
            MsgBox(s, vbOKOnly)
        End If

        Return InputsOK
    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.PropertySet = ComboBoxPropertySet.Text
        Me.PropertyName = TextBoxPropertyName.Text
        Me.EnglishName = ComboBoxEnglishName.Text

        If CheckInputs() Then
            If Me.PropertySet = "Custom" Then
                Me.EnglishName = Me.PropertyName
            End If

            If (Me.PropertySet = "System") And (Me.EnglishName = "") Then
                Me.EnglishName = Me.PropertyName
            End If

            Me.DialogResult = DialogResult.OK
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ComboBoxPropertySet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertySet.SelectedIndexChanged
        ComboBoxEnglishName.Enabled = Not ComboBoxPropertySet.Text = "Custom"
    End Sub

End Class