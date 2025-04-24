Option Strict On

Public Class FormPropertyFilter

    Private _PropertyFilters As PropertyFilters
    Public Property PropertyFilters As PropertyFilters
        Get
            Return _PropertyFilters
        End Get
        Set(value As PropertyFilters)
            _PropertyFilters = value

            Me.PropertyFilter = Me.PropertyFilters.GetActivePropertyFilter
        End Set
    End Property

    Public Property PropertyFilter As HCPropertyFilter
    Public Property UCList As List(Of UCPropertyFilter)
    Public Property HelpURL As String
    Public Property TemplatePropertyList As List(Of String)
    Public Property Formula As String

    Private _ShowAllProps As Boolean
    Public Property ShowAllProps As Boolean
        Get
            Return _ShowAllProps
        End Get
        Set(value As Boolean)
            _ShowAllProps = value
            If Me.PanelHeader IsNot Nothing Then
                ButtonShowAllProps.Checked = value
            End If
        End Set
    End Property


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim UC As New UtilsCommon

        Me.TemplatePropertyList = Form_Main.PropertiesData.GetFavoritesList

        Dim UP As New UtilsPreferences

        Me.UCList = New List(Of UCPropertyFilter)

        Dim UD As New UtilsDocumentation
        Dim Tag = "filtering"
        Me.HelpURL = UD.GenerateVersionURL(Tag)


        ' Check if the Properties were imported from templates
        Dim tf As Boolean

        tf = Form_Main.PropertiesData Is Nothing
        tf = tf Or Me.TemplatePropertyList Is Nothing

        If Not tf Then
            tf = Form_Main.PropertiesData.Items.Count = 0
            tf = tf Or Me.TemplatePropertyList.Count = 0
        End If

        If tf Then
            Dim s = "Template properties required for this command not found. "
            s = String.Format("{0}Populate them on the Configuration Tab -- Templates Page.", s)
            MsgBox(s, vbOKOnly)
            Exit Sub
        End If


    End Sub


    Private Function CheckInputs() As Boolean
        Dim InputsOK As Boolean = True
        Dim s As String = ""
        Dim indent As String = "    "
        Dim UtilsCommon = New UtilsCommon

        For Each UC As UCPropertyFilter In UCList

            ' Ignore any with no PropertyName
            If Not UC.PropertyName = "" Then

                If UC.PropertySet = "" Then
                    s = String.Format("{0}{1}Select a PropertySet for '{2}'{3}", s, indent, UC.PropertyName, vbCrLf)
                End If

                If UC.Comparison = "" Then
                    s = String.Format("{0}{1}Set a Comparison for '{2}'{3}", s, indent, UC.PropertyName, vbCrLf)
                End If

                If Not UC.Value = "" Then
                    If Not UtilsCommon.CheckValidPropertyFormulas(UC.Value) Then
                        s = String.Format("{0}{1}Property formula '{2}' missing 'System.' or 'Custom.'{3}", s, indent, UC.Value, vbCrLf)
                    End If
                End If

            End If

        Next

        If Not s = "" Then
            InputsOK = False
            s = String.Format("Please correct the following before continuing{0}{1}", vbCrLf, s)
            MsgBox(s, vbOKOnly)
        End If

        Return InputsOK
    End Function

    Private Function UpdatePropertyFilterFromForm() As HCPropertyFilter

        For Each PF As HCPropertyFilter In Me.PropertyFilters.Items
            PF.IsActiveFilter = False
        Next

        Dim Name As String = ComboBoxSavedSettings.Text

        Dim tmpPropertyFilter As HCPropertyFilter = Me.PropertyFilters.GetPropertyFilter(Name)

        If tmpPropertyFilter Is Nothing Then
            tmpPropertyFilter = New HCPropertyFilter
            tmpPropertyFilter.Name = ComboBoxSavedSettings.Text
        End If

        tmpPropertyFilter.IsActiveFilter = True
        tmpPropertyFilter.Formula = UCList(0).Formula

        tmpPropertyFilter.Conditions.Clear()

        Dim i = 0

        For Each UC As UCPropertyFilter In UCList
            If Not UC.PropertyName = "" Then

                Dim C As New PropertyFilterCondition

                tmpPropertyFilter.Conditions.Add(C)

                C.VariableName = UC.Variable

                Select Case UC.PropertySet
                    Case "System"
                        C.PropertySetName = PropertyFilterCondition.PropertySetNameConstants.System
                    Case "Custom"
                        C.PropertySetName = PropertyFilterCondition.PropertySetNameConstants.Custom
                End Select

                C.PropertyName = UC.PropertyName

                Select Case UC.Comparison
                    Case "contains"
                        C.Comparison = PropertyFilterCondition.ComparisonConstants.Contains
                    Case "is_exactly"
                        C.Comparison = PropertyFilterCondition.ComparisonConstants.IsExactly
                    Case "wildcard_match"
                        C.Comparison = PropertyFilterCondition.ComparisonConstants.WildcardMatch
                    Case "regex_match"
                        C.Comparison = PropertyFilterCondition.ComparisonConstants.RegexMatch
                    Case ">"
                        C.Comparison = PropertyFilterCondition.ComparisonConstants.GreaterThan
                    Case "<"
                        C.Comparison = PropertyFilterCondition.ComparisonConstants.LessThan
                End Select

                C.Value = UC.Value


            End If
        Next

        Return tmpPropertyFilter
    End Function

    Public Sub PopulateUCList(tmpPropertyFilter As HCPropertyFilter)

        Dim NewUC As UCPropertyFilter

        Me.UCList.Clear()

        For Each Condition As PropertyFilterCondition In tmpPropertyFilter.Conditions
            NewUC = New UCPropertyFilter(Me)
            NewUC.NotifyPropertyFilter = False

            NewUC.Variable = Condition.VariableName

            Select Case Condition.PropertySetName
                Case PropertyFilterCondition.PropertySetNameConstants.Custom
                    NewUC.PropertySet = "Custom"
                Case PropertyFilterCondition.PropertySetNameConstants.System
                    NewUC.PropertySet = "System"
            End Select

            NewUC.PropertyName = Condition.PropertyName

            Select Case Condition.Comparison
                Case PropertyFilterCondition.ComparisonConstants.Contains
                    NewUC.Comparison = "contains"
                Case PropertyFilterCondition.ComparisonConstants.IsExactly
                    NewUC.Comparison = "is_exactly"
                Case PropertyFilterCondition.ComparisonConstants.WildcardMatch
                    NewUC.Comparison = "wildcard_match"
                Case PropertyFilterCondition.ComparisonConstants.RegexMatch
                    NewUC.Comparison = "regex_match"
                Case PropertyFilterCondition.ComparisonConstants.GreaterThan
                    NewUC.Comparison = ">"
                Case PropertyFilterCondition.ComparisonConstants.LessThan
                    NewUC.Comparison = "<"
            End Select

            NewUC.Value = Condition.Value
            NewUC.Formula = tmpPropertyFilter.Formula

            NewUC.Dock = DockStyle.Fill

            UCList.Add(NewUC)
            NewUC.NotifyPropertyFilter = True
        Next

    End Sub

    Public Sub PopulateForm()

        If Me.PropertyFilter IsNot Nothing Then
            PopulateUCList(Me.PropertyFilter)
        End If

        UpdateForm(UpdateFormula:=False)

    End Sub

    Private Sub FormatFormula(tmpFormula As String)
        Dim s = tmpFormula.ToUpper

        s = s.Replace("(", " ( ")
        s = s.Replace(")", " ) ")
        s = s.Replace("  ", " ")
        s = s.Trim()
        s = String.Format(" {0} ", s)

        Me.Formula = s
        TextBoxFormula.Text = Me.Formula

        For Each UC As UCPropertyFilter In UCList
            If Not UC.PropertyName = "" Then
                UC.Formula = Me.Formula
            End If
        Next

    End Sub

    Private Sub UpdateVariablesAndFormula()
        Dim tmpFormula As String = ""
        Dim Ascii = 65  ' "A"

        For Each UC As UCPropertyFilter In UCList
            UC.Variable = Chr(Ascii)
            If Not UC.PropertyName = "" Then
                If tmpFormula = "" Then
                    tmpFormula = Chr(Ascii)
                Else
                    tmpFormula = String.Format("{0} AND {1}", tmpFormula, Chr(Ascii))
                End If
            End If
            Ascii += 1
        Next

        tmpFormula = String.Format(" {0} ", tmpFormula)

        Me.Formula = tmpFormula
        TextBoxFormula.Text = Me.Formula

        For Each UC As UCPropertyFilter In UCList
            If Not UC.PropertyName = "" Then
                UC.Formula = Me.Formula
            End If
        Next

    End Sub

    Private Sub UpdateForm(UpdateFormula As Boolean)

        If UpdateFormula Then
            UpdateVariablesAndFormula()
        Else
            If UCList.Count > 0 Then
                Me.Formula = UCList(0).Formula
            Else
                Me.Formula = ""
            End If
            TextBoxFormula.Text = Me.Formula
        End If

        ExTableLayoutPanelFilters.Controls.Clear()
        ExTableLayoutPanelFilters.RowStyles.Clear()

        ' Shouldn't need this, but it doesn't work without it.
        ExTableLayoutPanelFilters.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))

        ' Not true, but it doesn't work without it.
        ExTableLayoutPanelFilters.RowCount = 1

        Dim NeedANewRow As Boolean = True

        For Each UC As UCPropertyFilter In UCList
            ExTableLayoutPanelFilters.RowCount += 1
            ExTableLayoutPanelFilters.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))
            ExTableLayoutPanelFilters.Controls.Add(UC)
            If UC.PropertyName = "" Then
                NeedANewRow = False
            End If
        Next

        If NeedANewRow Then
            AddRow(UpdateFormula:=UpdateFormula)
        End If

    End Sub

    Public Sub AddRow(UpdateFormula As Boolean)
        Dim NewUC As New UCPropertyFilter(Me)
        NewUC.Dock = DockStyle.Fill
        Me.UCList.Add(NewUC)

        If UpdateFormula Then
            UpdateVariablesAndFormula()
        End If

        ExTableLayoutPanelFilters.RowCount += 1
        ExTableLayoutPanelFilters.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))
        ExTableLayoutPanelFilters.Controls.Add(NewUC)

    End Sub

    Private Sub MoveRow(Direction As String)
        Dim SelectedRow As Integer = GetSelectedRow()
        Dim tmpUCList As New List(Of UCPropertyFilter)
        Dim tf As Boolean

        tf = Direction.ToLower = "up"
        tf = tf Or Direction.ToLower = "down"
        If Not tf Then
            MsgBox(String.Format("Unrecognized direction '{0}'", Direction), vbOKOnly)
            Exit Sub
        End If

        'Can't move up from the top
        tf = (SelectedRow = 0) And (Direction = "up")

        'Can't move down from the bottom
        Dim Bottom = UCList.Count - 1
        tf = tf Or ((SelectedRow >= Bottom) And (Direction = "down"))

        If Not tf Then
            For i As Integer = 0 To Me.UCList.Count - 1

                Select Case Direction

                    Case "up"
                        If i = SelectedRow - 1 Then
                            tmpUCList.Add(Me.UCList(i + 1))
                        ElseIf i = SelectedRow Then
                            tmpUCList.Add(Me.UCList(i - 1))
                        Else
                            tmpUCList.Add(Me.UCList(i))
                        End If

                    Case "down"
                        If i = SelectedRow Then
                            tmpUCList.Add(Me.UCList(i + 1))
                        ElseIf i = SelectedRow + 1 Then
                            tmpUCList.Add(Me.UCList(i - 1))
                        Else
                            tmpUCList.Add(Me.UCList(i))
                        End If

                    Case Else
                        MsgBox(String.Format("Direction '{0}' not recognized", Direction))
                End Select

            Next

            Me.UCList = tmpUCList

            UpdateForm(UpdateFormula:=True)
        End If


    End Sub

    Private Function GetSelectedRow() As Integer
        Dim SelectedRow As Integer = -1

        Dim i = 0
        For Each UC As UCPropertyFilter In UCList
            If UC.Selected Then
                If UC.Selected Then SelectedRow = i
                Exit For
            End If
            i += 1
        Next

        Return SelectedRow
    End Function

    Public Sub UCChanged(ChangedUC As UCPropertyFilter)

        Dim NeedANewRow As Boolean = True

        For Each UC As UCPropertyFilter In UCList
            If ChangedUC.Selected Then
                If UC IsNot ChangedUC Then
                    UC.CheckBoxSelected.Checked = False
                End If
            End If
            If UC.PropertyName = "" Then
                NeedANewRow = False
            End If
        Next

        If NeedANewRow Then
            AddRow(UpdateFormula:=True)
        End If

    End Sub


    Private Sub FormPropertyFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Me.PropertyFilters Is Nothing Then
            Me.PropertyFilters = New PropertyFilters
        End If

        PopulateForm()

        Dim ActiveFilterName As String = Nothing

        For Each Item As HCPropertyFilter In Me.PropertyFilters.Items
            ComboBoxSavedSettings.Items.Add(Item.Name)
            If Item.IsActiveFilter Then
                ActiveFilterName = Item.Name
            End If
        Next

        If ActiveFilterName IsNot Nothing Then
            ComboBoxSavedSettings.Text = ActiveFilterName
        Else
            ComboBoxSavedSettings.Text = ""
        End If

        If Not PropertyFilters.Items.Count = 0 Then
            If PropertyFilter IsNot Nothing Then
                FormatFormula(PropertyFilter.Formula)
            End If
        End If

    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click

        If CheckInputs() Then
            Me.PropertyFilter = UpdatePropertyFilterFromForm()

            For Each PF As HCPropertyFilter In Me.PropertyFilters.Items
                PF.IsActiveFilter = False
            Next

            Me.PropertyFilter.IsActiveFilter = True

            Dim tmpPropertyFilter As HCPropertyFilter = Me.PropertyFilters.GetPropertyFilter(Me.PropertyFilter.Name)

            If tmpPropertyFilter Is Nothing Then
                Me.PropertyFilter.Name = ComboBoxSavedSettings.Text
                PropertyFilters.Items.Add(Me.PropertyFilter)
            Else
                tmpPropertyFilter = Me.PropertyFilter
            End If

            Me.DialogResult = DialogResult.OK
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
        System.Diagnostics.Process.Start(Me.HelpURL)
    End Sub

    Private Sub ButtonRowDelete_Click(sender As Object, e As EventArgs) Handles ButtonRowDelete.Click
        Dim SelectedRow = GetSelectedRow()
        Dim tmpUCList As New List(Of UCPropertyFilter)

        If SelectedRow = -1 Then
            MsgBox("No row is selected.  Select one by enabling its checkbox.")
        Else
            For i = 0 To UCList.Count - 1
                If Not i = SelectedRow Then
                    tmpUCList.Add(UCList(i))
                End If
            Next

            UCList = tmpUCList

            UpdateForm(UpdateFormula:=True)
        End If

    End Sub

    Private Sub ButtonRowUp_Click(sender As Object, e As EventArgs) Handles ButtonRowUp.Click
        MoveRow("up")
    End Sub

    Private Sub ButtonRowDown_Click(sender As Object, e As EventArgs) Handles ButtonRowDown.Click
        MoveRow("down")
    End Sub

    Private Sub ButtonSaveSettings_Click(sender As Object, e As EventArgs) Handles ButtonSaveSettings.Click
        Dim Name As String = ComboBoxSavedSettings.Text
        Dim Proceed As Boolean = True

        If Name = "" Then
            Proceed = False
            MsgBox("Enter a name for these settings", vbOKOnly)
        End If

        If Proceed And ComboBoxSavedSettings.Items.Contains(Name) Then
            Dim Result = MsgBox(String.Format("Name '{0}' already exists.  Do you want to replace it?", Name), vbYesNo)
            If Result = vbNo Then
                Proceed = False
            End If
        End If

        If Proceed Then
            For Each PF As HCPropertyFilter In Me.PropertyFilters.Items
                PF.IsActiveFilter = False
            Next

            Dim tmpPropertyFilter As HCPropertyFilter = PropertyFilters.GetPropertyFilter(Name)

            If tmpPropertyFilter Is Nothing Then
                tmpPropertyFilter = UpdatePropertyFilterFromForm()

                tmpPropertyFilter.Name = Name
                tmpPropertyFilter.IsActiveFilter = True

                Me.PropertyFilters.Items.Add(tmpPropertyFilter)
            Else
                tmpPropertyFilter = UpdatePropertyFilterFromForm()
                tmpPropertyFilter.IsActiveFilter = True
            End If

            If Not ComboBoxSavedSettings.Items.Contains(Name) Then
                ComboBoxSavedSettings.Items.Add(Name)
            End If

        End If
    End Sub

    Private Sub ComboBoxSavedSettings_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxSavedSettings.SelectedIndexChanged
        Dim Name As String = ComboBoxSavedSettings.Text

        Dim tmpPropertyFilter = Me.PropertyFilters.GetPropertyFilter(Name)
        If tmpPropertyFilter IsNot Nothing Then

            For Each PF As HCPropertyFilter In Me.PropertyFilters.Items
                PF.IsActiveFilter = False
            Next

            tmpPropertyFilter.IsActiveFilter = True

            PopulateUCList(tmpPropertyFilter)
            UpdateForm(UpdateFormula:=False)
        End If

    End Sub

    Private Sub ButtonRemoveSetting_Click(sender As Object, e As EventArgs) Handles ButtonRemoveSetting.Click
        Dim Name As String = ComboBoxSavedSettings.Text

        Me.PropertyFilters.RemoveItem(Name)

        ComboBoxSavedSettings.Items.Remove(Name)
        ComboBoxSavedSettings.Text = ""

    End Sub

    Private Sub ToolStripLabel3_Click(sender As Object, e As EventArgs) Handles ToolStripLabel3.Click

        Dim FTP As New FormTextPrompt
        FTP.Text = "Edit Formula"
        FTP.TextBoxInput.Text = TextBoxFormula.Text
        FTP.LabelPrompt.Text = "Edit formula"
        Dim Result = FTP.ShowDialog()

        If Result = DialogResult.OK Then
            FormatFormula(FTP.TextBoxInput.Text)
        End If

    End Sub

    Private Sub ButtonEditFormula_Click(sender As Object, e As EventArgs) Handles ButtonEditFormula.Click

        Dim FTP As New FormTextPrompt
        FTP.Text = "Edit Formula"
        FTP.TextBoxInput.Text = TextBoxFormula.Text
        FTP.LabelPrompt.Text = "Edit formula"
        Dim Result = FTP.ShowDialog()

        If Result = DialogResult.OK Then
            FormatFormula(FTP.TextBoxInput.Text)
        End If

    End Sub

    Private Sub ButtonShowAll_Click(sender As Object, e As EventArgs) Handles ButtonShowAllProps.Click

        Me.ShowAllProps = ButtonShowAllProps.Checked

        Dim PreviousPropertyName As String

        If ButtonShowAllProps.Checked Then
            ButtonShowAllProps.Image = My.Resources.Checked

            For i As Integer = 0 To UCList.Count - 1
                UCList(i).NotifyPropertyFilter = False
                PreviousPropertyName = UCList(i).ComboBoxPropertyName.Text
                UCList(i).ComboBoxPropertyName.Items.Clear()
                UCList(i).ComboBoxPropertyName.Items.Add("")

                For Each Key As String In Form_Main.PropertiesData.GetAvailableList
                    UCList(i).ComboBoxPropertyName.Items.Add(Key)
                Next
                UCList(i).ComboBoxPropertyName.Text = PreviousPropertyName
                UCList(i).NotifyPropertyFilter = True
            Next
        Else
            ButtonShowAllProps.Image = My.Resources.Unchecked

            For i As Integer = 0 To UCList.Count - 1
                UCList(i).NotifyPropertyFilter = False
                PreviousPropertyName = UCList(i).ComboBoxPropertyName.Text
                UCList(i).ComboBoxPropertyName.Items.Clear()
                UCList(i).ComboBoxPropertyName.Items.Add("")

                For Each Key As String In Me.TemplatePropertyList
                    UCList(i).ComboBoxPropertyName.Items.Add(Key)
                Next
                UCList(i).ComboBoxPropertyName.Text = PreviousPropertyName
                UCList(i).NotifyPropertyFilter = True
            Next
        End If

    End Sub

End Class