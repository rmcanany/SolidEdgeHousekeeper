Option Strict On

Public Class FormPropertyPicker

    Public Property FavoritesList As List(Of String)
    Public Property PropertyString As String
    Public Property PropertyOnly As Boolean


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.PropertyOnly = True
        Me.FavoritesList = Form_Main.PropertiesData.GetFavoritesList

    End Sub


    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Dim s As String = ""
        Dim indent As String = "    "
        If ComboBoxPropertySet.Text = "" Then
            s = $"{s}{indent}Select a Property Set{vbCrLf}"
        End If
        If ComboBoxPropertyName.Text = "" Then
            s = $"{s}{indent}Select a Property{vbCrLf}"
        End If

        If s = "" Then
            If Me.PropertyOnly Then
                Me.PropertyString = $"%{{{ComboBoxPropertySet.Text}.{ComboBoxPropertyName.Text}}}"
            Else
                Me.PropertyString = $"%{{{ComboBoxPropertySet.Text}.{ComboBoxPropertyName.Text}|R1}}"
            End If
            Me.DialogResult = DialogResult.OK
        Else
            MsgBox(s, vbOKOnly)
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ComboBoxPropertyName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertyName.SelectedIndexChanged

        If Form_Main.PropertiesData IsNot Nothing Then
            Dim tmpPropertyData As PropertyData = Form_Main.PropertiesData.GetPropertyData(ComboBoxPropertyName.Text)
            If tmpPropertyData IsNot Nothing Then
                Dim PropertySet As PropertyData.PropertySetNameConstants = tmpPropertyData.PropertySetName
                If tmpPropertyData.IsDuplicate Then
                    ComboBoxPropertySet.Text = ""
                ElseIf PropertySet = PropertyData.PropertySetNameConstants.Custom Then
                    ComboBoxPropertySet.Text = "Custom"
                Else
                    ComboBoxPropertySet.Text = "System"
                End If
            End If
        End If


    End Sub

    Private Sub ButtonPropOnly_Click(sender As Object, e As EventArgs) Handles ButtonPropOnly.Click
        Me.PropertyOnly = True

        ButtonPropOnly.Image = My.Resources.Checked
        ButtonPropAndIndex.Image = My.Resources.Unchecked

    End Sub

    Private Sub ButtonPropAndIndex_Click(sender As Object, e As EventArgs) Handles ButtonPropAndIndex.Click
        Me.PropertyOnly = False

        ButtonPropOnly.Image = My.Resources.Unchecked
        ButtonPropAndIndex.Image = My.Resources.Checked

    End Sub

    Private Sub ButtonShowAll_Click(sender As Object, e As EventArgs) Handles ButtonShowAll.Click

        If ButtonShowAll.Checked Then
            ButtonShowAll.Image = My.Resources.Checked

            ComboBoxPropertyName.Items.Clear()

            For Each PropName As String In Form_Main.PropertiesData.GetAvailableList
                ComboBoxPropertyName.Items.Add(PropName)
            Next

        Else
            ButtonShowAll.Image = My.Resources.Unchecked

            ComboBoxPropertyName.Items.Clear()
            For Each PropName As String In Me.FavoritesList
                ComboBoxPropertyName.Items.Add(PropName)
            Next

        End If

        If ComboBoxPropertyName.Items.Count > 0 Then ComboBoxPropertyName.Text = ComboBoxPropertyName.Items(0).ToString 'Check the items count in case of PropertyList not populated

    End Sub

    Private Sub FormPropertyPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Form_Main.PropertiesData Is Nothing OrElse Form_Main.PropertiesData.Items.Count = 0 Then
            Dim s = "Template properties required for this command not found. "
            s = $"{s}Populate them on the Configuration Tab -- Templates Page."
            MsgBox(s, vbOKOnly)
            ButtonCancel.PerformClick()
            'Exit Sub
        End If

        ComboBoxPropertyName.Items.Add("")
        For Each PropName As String In Me.FavoritesList
            ComboBoxPropertyName.Items.Add(PropName)
        Next
    End Sub

    Private Sub ButtonAddProp_Click(sender As Object, e As EventArgs) Handles ButtonAddProp.Click
        Dim FPLC As New FormPropertyListCustomize

        Dim Result As DialogResult = FPLC.ShowDialog()

        If Result = DialogResult.OK Then
            Dim UC As New UtilsCommon
            Me.FavoritesList = FPLC.FavoritesList

            Form_Main.PropertiesData.UpdateFavorites(Me.FavoritesList)

            ComboBoxPropertyName.Items.Clear()
            ComboBoxPropertyName.Items.Add("")
            For Each PropName As String In Me.FavoritesList
                ComboBoxPropertyName.Items.Add(PropName)
            Next

        End If


    End Sub

    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
        Dim UD As New UtilsDocumentation

        Dim Tag As String = "accessing-customized-properties"
        Dim HelpURL = UD.GenerateVersionURL(Tag)
        Diagnostics.Process.Start(HelpURL)
    End Sub

End Class