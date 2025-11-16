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
                    If Form_Main.PropertiesData.GetAvailableList.Contains(value) Then
                        ComboBoxPropertyName.Items.Add(value)
                        ComboBoxPropertyName.Text = value
                    End If
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
            _ReplaceString = value.Replace(Chr(182), vbCrLf)
            If Me.ExTableLayoutPanel1 IsNot Nothing Then
                TextBoxReplaceString.Text = value.Replace(vbCrLf, Chr(182))
            End If
        End Set
    End Property


    Public Property NotifyPropertyEditor As Boolean
    Public Property FavoritesList As List(Of String)
    Public Property ProcessEvents As Boolean = True


    Public Sub New(_PropertyEditor As FormPropertyInputEditor)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

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


    Private Function FilterPropertyNames() As List(Of String)
        ' When the user changes the PropertySet ComboBox, this function creates a list of properties
        ' in the selected PropertySet.  If the selected PropertySet is blank, all properties are returned.

        Dim FilteredList = New List(Of String)
        Dim PropSet As String = ""

        Dim tmpPropList As New List(Of String)

        If Me.PropertyEditor.ShowAllProps Then
            For Each s As String In Form_Main.PropertiesData.GetAvailableList
                tmpPropList.Add(s)
            Next
        Else
            For Each s As String In FavoritesList
                tmpPropList.Add(s)
            Next
        End If

        For Each PropName As String In tmpPropList

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

    Public Sub Notify()
        If NotifyPropertyEditor Then
            PropertyEditor.UCChanged(Me)
        End If

    End Sub


    Private Sub CheckBoxSelect_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSelected.CheckedChanged
        Me.Selected = CheckBoxSelected.Checked
        Notify()
    End Sub

    Private Sub ComboBoxPropertySet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertySet.SelectedIndexChanged

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


    End Sub

    Private Sub ComboBoxPropertyName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPropertyName.SelectedIndexChanged

        If Me.ProcessEvents Then
            Me.ProcessEvents = False

            Me.PropertyName = ComboBoxPropertyName.Text
            UpdatePropertySet()
            Notify()

            Me.ProcessEvents = True
        End If

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

    Private Sub ButtonInsertProperty_Click(sender As Object, e As EventArgs) Handles ButtonInsertProperty.Click

        Dim TextBox = CType(ContextMenuStrip1.SourceControl, TextBox)
        Dim CaretPosition = TextBox.Text.Length

        Dim FPP As New FormPropertyPicker

        FPP.ShowDialog()

        If FPP.DialogResult = DialogResult.OK Then
            TextBox.Text = TextBox.Text.Insert(CaretPosition, FPP.PropertyString)
        End If

    End Sub

    Private Sub ButtonInsertExpression_Click(sender As Object, e As EventArgs) Handles ButtonInsertExpression.Click

        Dim TextBox = CType(ContextMenuStrip1.SourceControl, TextBox)
        Dim CaretPosition = TextBox.Text.Length


        Dim FES As New FormExpressionSelector

        Dim Result As DialogResult = FES.ShowDialog

        If Result = DialogResult.OK Then
            Dim tmpSavedExpressionsItems = FES.SavedExpressionsItems
            Dim tmpSavedExpressionName = FES.SavedExpressionName

            If Not tmpSavedExpressionName = "" And tmpSavedExpressionsItems.Keys.Contains(tmpSavedExpressionName) Then
                Dim A As String = $"EXPRESSION_{FES.SavedExpresssionLanguage}{vbCrLf}{tmpSavedExpressionsItems(tmpSavedExpressionName)}"
                A = A.Split(CType("\\", Char)).First
                A = A.Replace(vbCrLf, Chr(182))  ' Chr(182) is the extended ascii paragraph symbol

                TextBox.Text = A

                'TextBox.Text = tmpSavedExpressionsItems(tmpSavedExpressionName)

            End If
        End If

    End Sub

    Private Sub ButtonEditExpression_Click(sender As Object, e As EventArgs) Handles ButtonEditExpression.Click

        Dim TextBox = CType(ContextMenuStrip1.SourceControl, TextBox)
        Dim CaretPosition = TextBox.Text.Length

        Dim FEE As New FormExpressionEditor

        Select Case Form_Main.ExpressionEditorLanguage
            Case "VB"
                FEE.TextEditorFormula.Language = FastColoredTextBoxNS.Language.VB
            Case "NCalc"
                FEE.TextEditorFormula.Language = FastColoredTextBoxNS.Language.SQL
            Case Else
                MsgBox($"Unrecognized expression editor language '{Form_Main.ExpressionEditorLanguage}'", vbOKOnly)
                Exit Sub
        End Select

        Dim Result As DialogResult = FEE.ShowDialog()

        If Result = DialogResult.OK Then
            If Not FEE.Formula = "" Then

                Select Case FEE.TextEditorFormula.Language
                    Case FastColoredTextBoxNS.Language.VB
                        Form_Main.ExpressionEditorLanguage = "VB"
                    Case FastColoredTextBoxNS.Language.SQL
                        Form_Main.ExpressionEditorLanguage = "NCalc"
                End Select

                Dim A As String = $"EXPRESSION_{Form_Main.ExpressionEditorLanguage}{vbCrLf}{FEE.Formula}"
                A = A.Split(CType("\\", Char)).First
                A = A.Replace(vbCrLf, Chr(182))  ' Chr(182) is the extended ascii paragraph symbol

                TextBox.Text = A

            End If

        End If


    End Sub
End Class
