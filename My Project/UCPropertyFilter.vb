Option Strict On
Imports System.Net.NetworkInformation

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
    'Public Property TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property TemplatePropertyList As List(Of String)
    Public Property ProcessEvents As Boolean = True



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

        Dim UC As New UtilsCommon

        Me.PropertyFilter = _PropertyFilter
        'Me.TemplatePropertyDict = PropertyFilter.TemplatePropertyDict
        Me.TemplatePropertyList = UC.TemplatePropertyGetFavoritesList(Form_Main.TemplatePropertyDict)

        'MsgBox("Temporarily setting hmk_Make_From PropertySet to 'Duplicate'")
        'Me.TemplatePropertyDict("hmk_Make_From")("PropertySet") = "Duplicate"

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

        'ComboBoxPropertyName.Items.Clear()

        'Select Case Me.PropertySet
        '    Case ""
        '        For Each s As String In TemplatePropertyList
        '            ComboBoxPropertyName.Items.Add(s)
        '        Next
        '    Case "Custom"
        '        For Each s As String In TemplatePropertyList
        '            If Form_Main.TemplatePropertyDict.Keys.Contains(s) Then
        '                If (Form_Main.TemplatePropertyDict(s)("PropertySet") = "Custom") Or (Form_Main.TemplatePropertyDict(s)("PropertySet") = "Duplicate") Then
        '                    ComboBoxPropertyName.Items.Add(s)
        '                End If
        '            End If
        '        Next
        '    Case "System"
        '        For Each s As String In TemplatePropertyList
        '            If Form_Main.TemplatePropertyDict.Keys.Contains(s) Then
        '                If (Not Form_Main.TemplatePropertyDict(s)("PropertySet") = "Custom") Or (Form_Main.TemplatePropertyDict(s)("PropertySet") = "Duplicate") Then
        '                    ComboBoxPropertyName.Items.Add(s)
        '                End If
        '            End If
        '        Next
        'End Select

        'If Me.ProcessEvents Then
        '    Me.ProcessEvents = False
        '    ComboBoxPropertyName.Text = CStr(ComboBoxPropertyName.Items(0))
        '    Me.ProcessEvents = True
        'End If

        Notify()
    End Sub

    Private Sub UpdatePropertyName()
        Me.PropertyName = ComboBoxPropertyName.Text

        Dim PropSet As String = ""

        If Not IsNothing(Form_Main.TemplatePropertyDict) Then
            If Form_Main.TemplatePropertyDict.Keys.Contains(Me.PropertyName) Then
                PropSet = Form_Main.TemplatePropertyDict(Me.PropertyName)("PropertySet")

                If Not ((PropSet = "") Or (PropSet = "Custom") Or (PropSet = "Duplicate")) Then
                    PropSet = "System"
                End If

                If PropSet = "Duplicate" Then
                    PropSet = ""
                End If

            Else
                PropSet = ""
            End If
        End If

        If Me.ProcessEvents Then
            Me.ProcessEvents = False
            ComboBoxPropertySet.Text = PropSet
            Me.ProcessEvents = True
        End If

    End Sub

    Private Sub ComboBoxPropertyName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertyName.SelectedIndexChanged
        UpdatePropertyName()
        Notify()
    End Sub

    Private Sub ComboBoxPropertyName_Leave(sender As Object, e As EventArgs) Handles ComboBoxPropertyName.Leave
        UpdatePropertyName()
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

    'Private Sub SelectPropertyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertPropertyToolStripMenuItem.Click

    '    Dim TextBox = CType(ContextMenuStrip1.SourceControl, TextBox)
    '    Dim CaretPosition = TextBox.SelectionStart

    '    Dim FPP As New FormPropertyPicker
    '    FPP.TemplatePropertyDict = Me.TemplatePropertyDict
    '    FPP.TemplatePropertyList = Me.TemplatePropertyList

    '    FPP.ShowDialog()

    '    If FPP.DialogResult = DialogResult.OK Then
    '        TextBox.Text = TextBox.Text.Insert(CaretPosition, FPP.PropertyString)

    '    End If
    '    Dim i = 0

    'End Sub
End Class
