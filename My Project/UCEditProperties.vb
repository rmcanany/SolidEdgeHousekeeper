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
                If ComboBoxPropertyName.Items.Contains(value) Then
                    ComboBoxPropertyName.Text = value
                Else
                    ComboBoxPropertyName.Items.Add(value)
                    ComboBoxPropertyName.Text = value
                End If
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
    Public Property FavoritesList As List(Of String)
    Public Property ProcessEvents As Boolean = True



    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim UC As New UtilsCommon

        'Me.TemplatePropertyList = UC.TemplatePropertyGetFavoritesList(Form_Main.TemplatePropertyDict)
        Me.FavoritesList = Form_Main.PropertiesData.GetFavoritesList

        ComboBoxPropertyName.Items.Add("")
        For Each s As String In FavoritesList
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

    Public Sub New(_PropertyEditor As FormPropertyInputEditor)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim UC As New UtilsCommon

        Me.PropertyEditor = _PropertyEditor
        Me.FavoritesList = Form_Main.PropertiesData.GetFavoritesList

        If Not Me.PropertyEditor.ShowAllProps Then
            ComboBoxPropertyName.Items.Add("")
            For Each s As String In FavoritesList
                ComboBoxPropertyName.Items.Add(s)
            Next
        Else
            ComboBoxPropertyName.Items.Add("")
            For Each s As String In Form_Main.PropertiesData.GetAvailableList
                ComboBoxPropertyName.Items.Add(s)
            Next
        End If

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
        'Me.PropertySet = ComboBoxPropertySet.Text
        'Notify()

        Dim TryIt As Boolean = True

        If Not TryIt Then
            Me.PropertySet = ComboBoxPropertySet.Text
            Notify()
        Else
            If Me.ProcessEvents Then
                Me.ProcessEvents = False

                Me.PropertySet = ComboBoxPropertySet.Text


                Dim PreviousPropertyName = Me.PropertyName
                Dim IsInList As Boolean = False
                ComboBoxPropertyName.Items.Clear()
                For Each PropName As String In FilterPropertyNames()
                    ComboBoxPropertyName.Items.Add(PropName)
                    If PropName = PreviousPropertyName Then IsInList = True
                Next
                If IsInList Then
                    ComboBoxPropertyName.Text = PreviousPropertyName
                Else
                    ComboBoxPropertyName.Text = ""
                End If

                Notify()

                Me.ProcessEvents = True
            End If

        End If


    End Sub

    Private Sub ComboBoxPropertyName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertyName.SelectedIndexChanged
        'UpdatePropertyName()
        'Notify()

        Dim TryIt As Boolean = True

        If Not TryIt Then
            Me.PropertyName = ComboBoxPropertyName.Text
            UpdatePropertySet()
            Notify()

        Else
            If Me.ProcessEvents Then
                Me.ProcessEvents = False

                Me.PropertyName = ComboBoxPropertyName.Text
                UpdatePropertySet()
                Notify()

                Me.ProcessEvents = True
            End If

        End If

    End Sub

    Private Function FilterPropertyNames() As List(Of String)
        Dim FilteredList = New List(Of String)
        Dim PropSet As String = ""
        'Dim PropSetConstant As PropertyData.PropertySetNameConstants

        Dim tmpPropList As New List(Of String)

        If Me.PropertyEditor.ShowAllProps Then
            'For Each s As String In Form_Main.TemplatePropertyDict.Keys
            '    tmpPropList.Add(s)
            'Next
            For Each s As String In Form_Main.PropertiesData.GetAvailableList
                tmpPropList.Add(s)
            Next
        Else
            For Each s As String In FavoritesList
                tmpPropList.Add(s)
            Next
        End If

        For Each PropName As String In tmpPropList
            'If Form_Main.TemplatePropertyDict.Keys.Contains(PropName) Then
            '    PropSet = Form_Main.TemplatePropertyDict(PropName)("PropertySet")  ' 'SummaryInformation', ..., 'Custom', 'Duplicate', ''

            '    If Not ((PropSet = "") Or (PropSet = "Custom") Or (PropSet = "Duplicate")) Then
            '        PropSet = "System"
            '    End If

            '    If PropSet = "Duplicate" Then
            '        'PropSet = ""
            '        PropSet = Me.PropertySet
            '    End If

            'Else
            '    PropSet = ""
            'End If

            Dim tmpPropertyData = Form_Main.PropertiesData.GetPropertyData(PropName)
            If tmpPropertyData IsNot Nothing Then

                If tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.Custom Then
                    PropSet = "Custom"
                ElseIf tmpPropertyData.IsDuplicate Then
                    PropSet = Me.PropertySet
                ElseIf tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.System Then
                    PropSet = "System"
                End If

            Else
                PropSet = ""
            End If

            If Me.PropertySet = "" Then
                FilteredList.Add(PropName)
            ElseIf PropSet = Me.PropertySet Then
                FilteredList.Add(PropName)
            End If

        Next

        Return FilteredList
    End Function

    Private Sub UpdatePropertySet()
        Dim PropSet As String = ""
        'Dim PropSetConstant As PropertyData.PropertySetNameConstants

        'If Not IsNothing(Form_Main.TemplatePropertyDict) Then
        '    If Form_Main.TemplatePropertyDict.Keys.Contains(Me.PropertyName) Then
        '        PropSet = Form_Main.TemplatePropertyDict(Me.PropertyName)("PropertySet")

        '        If Not ((PropSet = "") Or (PropSet = "Custom") Or (PropSet = "Duplicate")) Then
        '            PropSet = "System"
        '        End If

        '        If PropSet = "Duplicate" Then
        '            'PropSet = ""
        '            PropSet = Me.PropertySet
        '        End If

        '    Else
        '        PropSet = ""
        '    End If
        'End If

        If Form_Main.PropertiesData IsNot Nothing Then
            Dim tmpPropertyData As PropertyData = Form_Main.PropertiesData.GetPropertyData(Me.PropertyName)
            If tmpPropertyData IsNot Nothing Then

                If tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.Custom Then
                    PropSet = "Custom"
                ElseIf tmpPropertyData.IsDuplicate Then
                    PropSet = Me.PropertySet
                ElseIf tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.System Then
                    PropSet = "System"
                End If

            Else
                PropSet = ""
            End If
        End If

        Me.PropertySet = PropSet

    End Sub


    'Private Sub ComboBoxPropertyName_Leave(sender As Object, e As EventArgs) Handles ComboBoxPropertyName.Leave
    '    UpdatePropertyName()
    '    Notify()
    'End Sub


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
