Option Strict On
Imports System.Net.NetworkInformation

Public Class UCPropertyFilter
    Public Property PropertyFilter As FormPropertyFilter

    Private _Selected As Boolean
    Public Property Selected As Boolean
        Get
            Return _Selected
        End Get
        Set(value As Boolean)
            _Selected = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                CheckBoxSelected.Checked = value
            End If
        End Set
    End Property

    Private _Variable As String
    Public Property Variable As String
        Get
            Return _Variable
        End Get
        Set(Variable As String)
            _Variable = Variable
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                LabelVariable.Text = Variable
            End If
        End Set
    End Property


    Private _PropertySet As String
    Public Property PropertySet As String
        Get
            Return _PropertySet
        End Get
        Set(value As String)
            _PropertySet = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                ComboBoxPropertySet.Text = value
            End If
        End Set
    End Property

    Private _PropertyName As String
    Public Property PropertyName As String
        Get
            Return _PropertyName
        End Get
        Set(value As String)
            _PropertyName = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                ComboBoxPropertyName.Text = value
            End If
        End Set
    End Property

    Private _Comparison As String
    Public Property Comparison As String
        Get
            Return _Comparison
        End Get
        Set(value As String)
            _Comparison = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                ComboBoxComparison.Text = value
            End If
        End Set
    End Property

    Private _Value As String
    Public Property Value As String
        Get
            Return _Value
        End Get
        Set(value As String)
            _Value = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                TextBoxValue.Text = value
            End If
        End Set
    End Property

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

    Private Sub CheckBoxSelect_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSelected.CheckedChanged
        Me.Selected = CheckBoxSelected.Checked
        Notify()
    End Sub

    Private Sub ComboBoxPropertySet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertySet.SelectedIndexChanged
        Me.PropertySet = ComboBoxPropertySet.Text

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
                    'PropSet = ""
                    PropSet = Me.PropertySet
                End If

            Else
                PropSet = ""
            End If
        End If

        If Me.ProcessEvents Then
            Me.ProcessEvents = False
            Me.PropertySet = PropSet
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
