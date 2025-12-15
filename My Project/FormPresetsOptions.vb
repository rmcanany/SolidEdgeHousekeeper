Public Class FormPresetsOptions

    Private Property FMain As Form_Main

    Private _PresetsSaveFileFilters As Boolean
    Public Property PresetsSaveFileFilters As Boolean
        Get
            Return _PresetsSaveFileFilters
        End Get
        Set(value As Boolean)
            _PresetsSaveFileFilters = value
            If Me.CheckBoxPresetsSaveFileFilters IsNot Nothing Then
                CheckBoxPresetsSaveFileFilters.Checked = value
            End If
        End Set
    End Property

    Private _PresetsSavePropertyFilters As Boolean
    Public Property PresetsSavePropertyFilters As Boolean
        Get
            Return _PresetsSavePropertyFilters
        End Get
        Set(value As Boolean)
            _PresetsSavePropertyFilters = value
            If Me.CheckBoxPresetsSavePropertyFilters IsNot Nothing Then
                CheckBoxPresetsSavePropertyFilters.Checked = value
            End If
        End Set
    End Property


    Public Sub New(_FMain As Form_Main)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.FMain = _FMain
    End Sub

    Private Sub FormPresetsOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.PresetsSaveFileFilters = FMain.PresetsSaveFileFilters
        Me.PresetsSavePropertyFilters = FMain.PresetsSavePropertyFilters
    End Sub

    Private Sub CheckBoxSaveFileFilters_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPresetsSaveFileFilters.CheckedChanged
        Me.PresetsSaveFileFilters = CheckBoxPresetsSaveFileFilters.Checked
    End Sub

    Private Sub CheckBoxSavePropertyFilterSettings_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPresetsSavePropertyFilters.CheckedChanged
        Me.PresetsSavePropertyFilters = CheckBoxPresetsSavePropertyFilters.Checked
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        FMain.PresetsSaveFileFilters = Me.PresetsSaveFileFilters
        FMain.PresetsSavePropertyFilters = Me.PresetsSavePropertyFilters
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

End Class