Option Strict On

Public Class UCEditProperties

    Public Property PropertyEditor As FormPropertyInputEditor

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
                If Not value = "" Then
                    If Not TemplatePropertyList.Contains(value) Then TemplatePropertyList.Add(value)
                    If Not ComboBoxPropertyName.Items.Contains(value) Then ComboBoxPropertyName.Items.Add(value)
                End If
                ComboBoxPropertyName.Text = value
                'If value = "badger" Then
                '    'MsgBox(String.Format("{0} {1}", ComboBoxPropertyName.Text, value))
                'End If
            End If
        End Set
    End Property

    Private _FindSearch As String
    Public Property FindSearch As String
        Get
            Return _FindSearch
        End Get
        Set(value As String)
            _FindSearch = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                ComboBoxFindSearch.Text = value
            End If
        End Set
    End Property

    Private _FindString As String
    Public Property FindString As String
        Get
            Return _FindString
        End Get
        Set(value As String)
            _FindString = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                TextBoxFindString.Text = value
            End If
        End Set
    End Property

    Private _ReplaceSearch As String
    Public Property ReplaceSearch As String
        Get
            Return _ReplaceSearch
        End Get
        Set(value As String)
            _ReplaceSearch = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                ComboBoxReplaceSearch.Text = value
            End If
        End Set
    End Property

    Private _ReplaceString As String
    Public Property ReplaceString As String
        Get
            Return _ReplaceString
        End Get
        Set(value As String)
            _ReplaceString = value
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                TextBoxReplaceString.Text = value
            End If
        End Set
    End Property


    Public Property NotifyPropertyEditor As Boolean
    'Public Property TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property TemplatePropertyList As List(Of String)
    Public Property ProcessEvents As Boolean = True



    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim UC As New UtilsCommon

        'Me.TemplatePropertyDict = Form_Main.TemplatePropertyDict
        Me.TemplatePropertyList = UC.TemplatePropertyGetFavoritesList(Form_Main.TemplatePropertyDict)

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

        Dim UC As New UtilsCommon

        Me.PropertyEditor = _PropertyEditor
        'Me.TemplatePropertyDict = Form_Main.TemplatePropertyDict
        Me.TemplatePropertyList = UC.TemplatePropertyGetFavoritesList(Form_Main.TemplatePropertyDict)

        'MsgBox("Temporarily setting hmk_Make_From PropertySet to 'Duplicate'")
        'Form_Main.TemplatePropertyDict("hmk_Make_From")("PropertySet") = "Duplicate"

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

    Private Sub CheckBoxSelect_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSelected.CheckedChanged
        Me.Selected = CheckBoxSelected.Checked
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
        '            If TemplatePropertyDict.Keys.Contains(s) Then
        '                If (TemplatePropertyDict(s)("PropertySet") = "Custom") Or (TemplatePropertyDict(s)("PropertySet") = "Duplicate") Then
        '                    ComboBoxPropertyName.Items.Add(s)
        '                End If
        '            End If
        '        Next
        '    Case "System"
        '        For Each s As String In TemplatePropertyList
        '            If TemplatePropertyDict.Keys.Contains(s) Then
        '                If (Not TemplatePropertyDict(s)("PropertySet") = "Custom") Or (TemplatePropertyDict(s)("PropertySet") = "Duplicate") Then
        '                    ComboBoxPropertyName.Items.Add(s)
        '                End If
        '            End If
        '        Next
        'End Select

        'If Me.ProcessEvents Then
        '    Me.ProcessEvents = False
        '    If Not ComboBoxPropertyName.Items.Contains(ComboBoxPropertyName.Text) Then
        '        'ComboBoxPropertyName.Text = CStr(ComboBoxPropertyName.Items(0))
        '        Me.PropertyName = ""
        '    End If
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
        'Me.NotifyPropertyEditor = False

        'CheckBoxSelected.Checked = Me.Selected
        'ComboBoxPropertySet.Text = Me.PropertySet
        'ComboBoxPropertyName.Text = Me.PropertyName
        'ComboBoxFindSearch.Text = Me.FindSearch
        'TextBoxFindString.Text = Me.FindString
        'ComboBoxReplaceSearch.Text = Me.ReplaceSearch
        'TextBoxReplaceString.Text = Me.ReplaceString

        'Me.NotifyPropertyEditor = True
    End Sub

    Public Sub Notify()
        If NotifyPropertyEditor Then
            PropertyEditor.UCChanged(Me)
        End If

    End Sub

    Private Sub SelectPropertyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertPropertyToolStripMenuItem.Click

        Dim TextBox = CType(ContextMenuStrip1.SourceControl, TextBox)
        Dim CaretPosition = TextBox.Text.Length

        Dim FPP As New FormPropertyPicker
        'FPP.TemplatePropertyDict = Form_Main.TemplatePropertyDict
        'FPP.TemplatePropertyList = Me.TemplatePropertyList

        FPP.ShowDialog()

        If FPP.DialogResult = DialogResult.OK Then
            TextBox.Text = TextBox.Text.Insert(CaretPosition, FPP.PropertyString)
        End If

    End Sub
End Class
