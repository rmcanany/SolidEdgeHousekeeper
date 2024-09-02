Option Strict On

Public Class FormPropertyPicker
    'Public Property TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property TemplatePropertyList As List(Of String)
    Public Property PropertyString As String
    Public Property PropertyOnly As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim UC As New UtilsCommon

        Me.PropertyOnly = True
        'Me.TemplatePropertyDict = Form_Main.TemplatePropertyDict
        Me.TemplatePropertyList = UC.TemplatePropertyGetFavoritesList(Form_Main.TemplatePropertyDict)

    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Dim s As String = ""
        If ComboBoxPropertySet.Text = "" Then
            s = String.Format("{0}Select a Property Set{1}", s, vbCrLf)
        End If
        If ComboBoxPropertyName.Text = "" Then
            s = String.Format("{0}Select a Property{1}", s, vbCrLf)
        End If

        If s = "" Then
            If Me.PropertyOnly Then
                Me.PropertyString = String.Format("%{{{0}.{1}}}", ComboBoxPropertySet.Text, ComboBoxPropertyName.Text)
            Else
                Me.PropertyString = String.Format("%{{{0}.{1}|R1}}", ComboBoxPropertySet.Text, ComboBoxPropertyName.Text)
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

        If Form_Main.TemplatePropertyDict IsNot Nothing Then
            If Form_Main.TemplatePropertyDict.Keys.Contains(ComboBoxPropertyName.Text) Then
                Dim PropertySet = Form_Main.TemplatePropertyDict(ComboBoxPropertyName.Text)("PropertySet")
                If PropertySet = "Duplicate" Then
                    ComboBoxPropertySet.Text = ""
                ElseIf PropertySet = "Custom" Then
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

            ComboBoxPropertyName.Items.Clear()
            For Each Key As String In Form_Main.TemplatePropertyDict.Keys
                ComboBoxPropertyName.Items.Add(Key)
            Next
            ComboBoxPropertyName.Text = ComboBoxPropertyName.Items(0).ToString
        Else
            ButtonShowAll.Image = My.Resources.Unchecked

            ComboBoxPropertyName.Items.Clear()
            For Each Key As String In Me.TemplatePropertyList
                ComboBoxPropertyName.Items.Add(Key)
            Next
            ComboBoxPropertyName.Text = ComboBoxPropertyName.Items(0).ToString
        End If

    End Sub

    Private Sub FormPropertyPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBoxPropertyName.Items.Add("")
        For Each Key As String In Me.TemplatePropertyList
            ComboBoxPropertyName.Items.Add(Key)
        Next
    End Sub
End Class