Option Strict On
Imports Housekeeper.Task

Public Class InterfaceUtilities

    Private Property TaskList As List(Of Task)

    Public Sub FormatTLPOptions(
        TLP As TableLayoutPanel,
        Name As String,
        NumRows As Integer,
        Optional NumColumns As Integer = 2)

        TLP.Name = Name
        TLP.RowCount = NumRows
        For i As Integer = 0 To TLP.RowCount - 1
            TLP.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        Next

        TLP.ColumnCount = NumColumns
        For i As Integer = 0 To NumColumns - 1
            If i < NumColumns - 1 Then
                TLP.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            Else
                TLP.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
            End If

        Next
        TLP.Dock = DockStyle.Fill

        TLP.AutoSize = True
        TLP.Visible = False

    End Sub

    Public Sub FormatTLPOptionsEx(
        TLP As TableLayoutPanel,
        Name As String,
        NumRows As Integer,
        Column1Width As Integer,
        Column2Width As Integer)

        TLP.Name = Name
        TLP.RowCount = NumRows
        For i As Integer = 0 To TLP.RowCount - 1
            TLP.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        Next

        TLP.ColumnCount = 3

        TLP.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, Column1Width))
        TLP.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, Column2Width))
        TLP.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))

        TLP.Dock = DockStyle.Fill

        TLP.AutoSize = True
        TLP.Visible = False

    End Sub

    Public Function FormatOptionsCheckBox(
        ControlName As String,
        CheckBoxText As String
        ) As CheckBox

        Dim CheckBox = New CheckBox
        CheckBox.Name = ControlName
        CheckBox.Text = CheckBoxText
        CheckBox.Anchor = AnchorStyles.Left
        CheckBox.AutoSize = True

        Return CheckBox
    End Function

    Public Function FormatOptionsButton(
        ControlName As String,
        ButtonText As String
        ) As Button

        Dim Button = New Button
        Button.Name = ControlName
        Button.Text = ButtonText
        Button.Anchor = AnchorStyles.Left
        Button.AutoSize = True
        Button.BackColor = Color.FromName("Control")

        Return Button
    End Function

    Public Function FormatOptionsTextBox(
        ControlName As String,
        TextBoxText As String
        ) As TextBox

        Dim TextBox = New TextBox
        TextBox.Name = ControlName
        TextBox.Text = TextBoxText
        TextBox.Anchor = CType(AnchorStyles.Left + AnchorStyles.Right, AnchorStyles)
        'TextBox.AutoSize = True

        Return TextBox
    End Function

    Public Function FormatOptionsComboBox(
        ControlName As String,
        ComboBoxItems As List(Of String),
        DropDownStyleName As String
        ) As ComboBox

        ' DropDownStyleName: Simple | DropDown | DropDownList

        Dim ComboBox = New ComboBox
        ComboBox.Name = ControlName
        Dim ComboBoxItem As String
        Dim MaxCharacters As Integer = 0

        Select Case DropDownStyleName
            Case "Simple"
                ComboBox.DropDownStyle = ComboBoxStyle.Simple
            Case "DropDown"
                ComboBox.DropDownStyle = ComboBoxStyle.DropDown
            Case "DropDownList"
                ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
            Case Else
                MsgBox(String.Format("{0} DropDownStyleName '{1}' not recognized", Me.ToString, DropDownStyleName))

        End Select

        For Each ComboBoxItem In ComboBoxItems
            ComboBox.Items.Add(ComboBoxItem)
            If Len(ComboBoxItem) > MaxCharacters Then MaxCharacters = Len(ComboBoxItem)
        Next
        ComboBox.Text = CStr(ComboBox.Items(0))

        ComboBox.Width = MaxCharacters * 15

        ComboBox.Anchor = CType(AnchorStyles.Left, AnchorStyles)

        Return ComboBox
    End Function

    Public Function FormatOptionsLabel(
        Name As String,
        LabelText As String
        ) As Label

        Dim Label As New Label

        Label.Name = Name
        Label.Text = LabelText
        Label.AutoSize = True
        Label.Anchor = CType(AnchorStyles.Left, AnchorStyles)

        Return Label
    End Function

End Class
