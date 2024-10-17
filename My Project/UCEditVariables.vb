Option Strict On

Public Class UCEditVariables
    Public Property VariableEditor As FormVariableInputEditor
    Public Property Selected As Boolean
    Public Property VariableName As String
    Public Property Formula As String
    Public Property UnitType As String
    Public Property Expose As Boolean
    Public Property ExposeName As String
    Public Property NotifyVariableEditor As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Selected = False
        Me.VariableName = ""
        Me.Formula = ""
        Me.UnitType = ""
        Me.Expose = False
        Me.ExposeName = ""
    End Sub

    Public Sub New(_VariableEditor As FormVariableInputEditor)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.VariableEditor = _VariableEditor

        Me.Selected = False
        Me.VariableName = ""
        Me.Formula = ""
        Me.UnitType = ""
        Me.Expose = False
        Me.ExposeName = ""
        Me.NotifyVariableEditor = True

        Dim UnitTypeConstant As SolidEdgeConstants.UnitTypeConstants
        ComboBoxUnitType.Items.Add(" ")
        For Each UnitTypeConstant In System.Enum.GetValues(GetType(SolidEdgeConstants.UnitTypeConstants))
            ComboBoxUnitType.Items.Add(UnitTypeConstant.ToString.Replace("igUnit", ""))
        Next

    End Sub

    Private Sub CheckBoxSelect_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSelect.CheckedChanged
        Me.Selected = CheckBoxSelect.Checked
        Notify()
    End Sub

    Private Sub TextBoxVariableName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxVariableName.TextChanged
        Me.VariableName = TextBoxVariableName.Text
        Notify()
    End Sub

    Private Sub TextBoxFormula_TextChanged(sender As Object, e As EventArgs) Handles TextBoxFormula.TextChanged
        Me.Formula = TextBoxFormula.Text
        Notify()
    End Sub

    Private Sub ComboBoxUnitType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxUnitType.SelectedIndexChanged
        Me.UnitType = ComboBoxUnitType.Text
        Notify()
    End Sub

    Private Sub CheckBoxExpose_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxExpose.CheckedChanged
        Me.Expose = CheckBoxExpose.Checked
        If (Me.Expose) And (Me.ExposeName = "") Then
            TextBoxExposeName.Text = Me.VariableName
        End If
        Notify()
    End Sub

    Private Sub TextBoxExposeName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxExposeName.TextChanged
        Me.ExposeName = TextBoxExposeName.Text
        Notify()
    End Sub

    Public Sub ReconcileFormWithProps()
        Me.NotifyVariableEditor = False

        CheckBoxSelect.Checked = Me.Selected
        TextBoxVariableName.Text = Me.VariableName
        TextBoxFormula.Text = Me.Formula
        ComboBoxUnitType.Text = Me.UnitType
        CheckBoxExpose.Checked = Me.Expose
        TextBoxExposeName.Text = Me.ExposeName

        Me.NotifyVariableEditor = True
    End Sub

    Public Sub Notify()
        If NotifyVariableEditor Then
            VariableEditor.UCChanged(Me)
        End If

    End Sub

    Private Sub InsertPropertyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertPropertyToolStripMenuItem.Click
        Dim TextBox = CType(ContextMenuStrip1.SourceControl, TextBox)
        Dim CaretPosition = TextBox.SelectionStart

        Dim FPP As New FormPropertyPicker

        FPP.ShowDialog()

        If FPP.DialogResult = DialogResult.OK Then
            TextBox.Text = TextBox.Text.Insert(CaretPosition, FPP.PropertyString)

        End If

    End Sub
End Class
