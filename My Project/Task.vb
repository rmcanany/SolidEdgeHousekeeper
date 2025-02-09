Option Strict On

Imports System.Reflection
Imports Newtonsoft.Json

Public MustInherit Class Task
    Inherits HCIsolatedTaskProxy

    Public Property Name As String

    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(value As String)
            _Description = value
            If Me.TaskControl IsNot Nothing Then
                Me.TaskControl.TaskName.Text = value
            End If
        End Set
    End Property

    Public Property HelpText As String
    Public Property RequiresSave As Boolean
    Public Property AppliesToAssembly As Boolean
    Public Property AppliesToPart As Boolean
    Public Property AppliesToSheetmetal As Boolean
    Public Property AppliesToDraft As Boolean
    Public Property HasOptions As Boolean
    Public Property HelpURL As String
    Public Property Image As Image
    Public Property TaskControl As UCTaskControl
    Public Property TaskOptionsTLP As ExTableLayoutPanel
    Public Property ManualOptionsOnlyString As String = "Only show options manually. Use [+] to show."

    Private _IsSelectedTask As Boolean
    Public Property IsSelectedTask As Boolean
        Get
            Return _IsSelectedTask
        End Get
        Set(value As Boolean)
            _IsSelectedTask = value
            If Me.TaskControl IsNot Nothing Then
                Me.TaskControl.CBEnabled.Checked = value
            End If
        End Set
    End Property

    Private _IsSelectedAssembly As Boolean
    Public Property IsSelectedAssembly As Boolean
        Get
            Return _IsSelectedAssembly
        End Get
        Set(value As Boolean)
            _IsSelectedAssembly = value
            If Me.TaskControl IsNot Nothing Then
                Me.TaskControl.CBAssembly.Checked = value
            End If
        End Set
    End Property

    Private _IsSelectedPart As Boolean
    Public Property IsSelectedPart As Boolean
        Get
            Return _IsSelectedPart
        End Get
        Set(value As Boolean)
            _IsSelectedPart = value
            If Me.TaskControl IsNot Nothing Then
                Me.TaskControl.CBPart.Checked = value
            End If
        End Set
    End Property

    Private _IsSelectedSheetmetal As Boolean
    Public Property IsSelectedSheetmetal As Boolean
        Get
            Return _IsSelectedSheetmetal
        End Get
        Set(value As Boolean)
            _IsSelectedSheetmetal = value
            If Me.TaskControl IsNot Nothing Then
                Me.TaskControl.CBSheetmetal.Checked = value
            End If
        End Set
    End Property


    Private _IsSelectedDraft As Boolean
    Public Property IsSelectedDraft As Boolean
        Get
            Return _IsSelectedDraft
        End Get
        Set(value As Boolean)
            _IsSelectedDraft = value
            If Me.TaskControl IsNot Nothing Then
                Me.TaskControl.CBDraft.Checked = value
            End If
        End Set
    End Property

    'Public Property AutoHideOptions As Boolean
    Public Property IsOptionsHidden As Boolean

    'Public Property RememberTaskSelections As Boolean

    'Public Property Visible As Boolean

    Public Property RequiresSourceDirectories As Boolean
    Public Property SourceDirectories As List(Of String)

    Public TLPHeader As ExTableLayoutPanel
    Public Property ControlsDict As Dictionary(Of String, Control)

    'Shared Property BaseControlsDict As New Dictionary(Of String, Control)

    Public Property ColorHue As String
    Public Property ColorSaturation As Double
    Public Property ColorBrightness As Double
    Public Property ColorR As Integer
    Public Property ColorG As Integer
    Public Property ColorB As Integer

    Public Property RequiresAssemblyTemplate As Boolean = False
    Public Property RequiresPartTemplate As Boolean = False
    Public Property RequiresSheetmetalTemplate As Boolean = False
    Public Property RequiresDraftTemplate As Boolean = False
    Public Property RequiresMaterialTable As Boolean = False

    'These belong only in the tasks that need them.
    'Easier to synchronize props with UI there, for one thing.
    'Public Property AssemblyTemplate As String = ""
    'Public Property PartTemplate As String = ""
    'Public Property SheetmetalTemplate As String = ""
    'Public Property DraftTemplate As String = ""
    'Public Property MaterialTable As String = ""
    'Public Property UseConfigurationPageTemplates As Boolean = False

    Public Property Category As String
    Public Property SolidEdgeRequired As Boolean = True

    'Public Property RequiresTemplatePropertyDict As Boolean = False
    Public Property RequiresPropertiesData As Boolean = False
    Public Property PropertiesData As HCPropertiesData
    Public Property CompatibleWithOtherTasks As Boolean = True

    Public Property RequiresLinkManagementOrder As Boolean = False
    Public Property LinkManagementOrder As List(Of String) = Nothing



    Public Enum BaseControlNames
        Expand
        SelectTask
        SelectAssembly
        SelectPart
        SelectSheetmetal
        SelectDraft
        Task
        Help
        EditTaskList
    End Enum

    Public MustOverride Function Process(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        Configuration As Dictionary(Of String, String),
        SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

    Public MustOverride Function Process(FileName As String) As Dictionary(Of Integer, List(Of String))


    Public MustOverride Function CheckStartConditions(
        PriorErrorMessage As Dictionary(Of Integer, List(Of String))
        ) As Dictionary(Of Integer, List(Of String))


    Public Sub New()

    End Sub

    Public Sub GenerateTaskControl()

        ControlsDict = New Dictionary(Of String, Control)

        Me.TaskControl = New UCTaskControl(Me)
        Me.TaskControl.BackColor = Color.FromArgb(Me.ColorR, Me.ColorG, Me.ColorB)

        For Each Control As Control In Me.TaskControl.Controls
            If ControlsDict.Keys.Contains(Control.Name) Then
                MsgBox(String.Format("ControlsDict already has Key '{0}'", Control.Name))
            End If
            ControlsDict(Control.Name) = Control
        Next

    End Sub


    'UTILITIES
    Public Sub ResetTaskColor()
        Me.TaskControl.BackColor = Color.FromArgb(Me.ColorR, Me.ColorG, Me.ColorB)
    End Sub

    Public Sub SetColorFromCategory(Task As Task)

        ' Red Green Blue Cyan Magenta Yellow White Orange Purple

        Task.ColorSaturation = 0.05
        Task.ColorBrightness = 1

        Select Case Task.Category.ToLower
            Case "update"
                Task.ColorHue = "Orange"
            Case "edit"
                Task.ColorHue = "Cyan"
            Case "restyle"
                Task.ColorHue = "Red"
            Case "check"
                Task.ColorHue = "Yellow"
            Case "output"
                Task.ColorHue = "Purple"
            Case Else
                MsgBox(String.Format("Task '{0}' category '{1}' not recognized", Task.Name, Task.Category.ToLower))
        End Select

        SetRBGFromHSB(Task)
    End Sub

    Public Sub SetRBGFromHSB(Task As Task)

        ' Red Green Blue Cyan Magenta Yellow White Orange Purple

        Dim R As Integer = 0
        Dim G As Integer = 0
        Dim B As Integer = 0

        Select Case Task.ColorHue
            Case "Red"
                R = 255
                G = CInt(255 * (1 - Me.ColorSaturation))
                B = CInt(255 * (1 - Me.ColorSaturation))
            Case "Green"
                R = CInt(255 * (1 - Me.ColorSaturation))
                G = 255
                B = CInt(255 * (1 - Me.ColorSaturation))
            Case "Blue"
                R = CInt(255 * (1 - Me.ColorSaturation))
                G = CInt(255 * (1 - Me.ColorSaturation))
                B = 255
            Case "Cyan"
                R = CInt(255 * (1 - Me.ColorSaturation))
                G = 255
                B = 255
            Case "Magenta"
                R = 255
                G = CInt(255 * (1 - Me.ColorSaturation))
                B = 255
            Case "Yellow"
                R = 255
                G = 255
                B = CInt(255 * (1 - Me.ColorSaturation))
            Case "White"
                R = 255
                G = 255
                B = 255
            Case "Orange"
                R = 255
                G = CInt(127 + 127 * (1 - Me.ColorSaturation))
                B = CInt(255 * (1 - Me.ColorSaturation))
            Case "Purple"
                R = CInt(127 + 127 * (1 - Me.ColorSaturation))
                G = CInt(255 * (1 - Me.ColorSaturation))
                B = 255
        End Select

        R = CInt(R * Task.ColorBrightness)
        G = CInt(G * Task.ColorBrightness)
        B = CInt(B * Task.ColorBrightness)

        Task.ColorR = R
        Task.ColorG = G
        Task.ColorB = B

    End Sub

    Public Sub AddSupplementalErrorMessage(
        ByRef ExitStatus As Integer,
        ErrorMessageList As List(Of String),
        SupplementalErrorMessage As Dictionary(Of Integer, List(Of String))
        )

        Dim SupplementalExitStatus As Integer = SupplementalErrorMessage.Keys(0)

        If Not SupplementalExitStatus = 0 Then
            If SupplementalExitStatus > ExitStatus Then
                ExitStatus = SupplementalExitStatus
            End If
            For Each s As String In SupplementalErrorMessage(SupplementalExitStatus)
                ErrorMessageList.Add(s)
            Next
        End If
    End Sub

    Public Sub UpdateErrorMessageList(
        ErrorMessageList As List(Of String),
        ErrorMessage As String,
        TreatAsSubtask As Boolean,
        LabelText As String)

        Dim Indent As String = "    "

        If TreatAsSubtask Then
            If Not ErrorMessageList.Contains(LabelText) Then
                ErrorMessageList.Add(LabelText)
            End If
            ErrorMessageList.Add(String.Format("{0}{1}", Indent, ErrorMessage))
        Else
            ErrorMessageList.Add(ErrorMessage)
        End If


    End Sub

    Public Function GenerateLabelText() As String
        ' Scratch.TaskOpenSave -> Open save
        ' Housekeeper.TaskOpenSave -> Open save

        Dim InString As String
        Dim OutString As String = ""

        InString = Me.ToString
        InString = InString.Replace("Scratch.Task", "")  ' 'Scratch.TaskOpenSave' -> 'OpenSave'
        InString = InString.Replace("Housekeeper.Task", "")  ' 'Housekeeper.TaskOpenSave' -> 'OpenSave'

        OutString = InString(0)  ' '' -> 'O'
        InString = Right(InString, Len(InString) - 1)  ' 'OpenSave' -> 'penSave'

        For Each c As Char In InString
            If (Asc(c) >= 65) And (Asc(c) <= 90) Then  ' It's a capital letter
                ' Upper case.  Add a space and change the character to lower case.
                OutString = String.Format("{0} {1}", OutString, CStr(c).ToLower)
            Else
                ' Lower case.  Add the character as is.
                OutString = String.Format("{0}{1}", OutString, CStr(c))
            End If
        Next

        Return OutString
    End Function

    Public Function GenerateCtrlText(CtrlName As String) As String
        ' AutoOrient -> Auto orient
        ' PrintAsBlack -> Print as black

        Dim InString As String
        Dim OutString As String = ""

        InString = CtrlName

        OutString = InString(0)  ' '' -> 'O'
        InString = Right(InString, Len(InString) - 1)  ' 'OpenSave' -> 'penSave'

        For Each c As Char In InString
            If (Asc(c) >= 65) And (Asc(c) <= 90) Then  ' It's a capital letter
                ' Upper case.  Add a space and change the character to lower case.
                OutString = String.Format("{0} {1}", OutString, CStr(c).ToLower)
            Else
                ' Lower case.  Add the character as is.
                OutString = String.Format("{0}{1}", OutString, CStr(c))
            End If
        Next

        Return OutString
    End Function

    Public Function GenerateHelpURL(tmpLabelText As String) As String

        Dim Tag As String

        ' Example address
        ' https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/README-2024.2.md#open-save

        Tag = tmpLabelText         '  'Open save'
        Tag = Tag.Replace("/", "")   '  'Open save' -> 'Open save'
        Tag = Tag.Replace(" ", "-")  '  'Open save' -> 'Open-save'
        Tag = Tag.ToLower            '  'Open-save' -> 'open-save'

        Dim UD As New UtilsDocumentation

        Dim HelpURL = UD.GenerateVersionURL(Tag)

        Return HelpURL

    End Function

    Public Sub HandleMutuallyExclusiveCheckBoxes(
        TLPOptions As ExTableLayoutPanel,
        NewlyCheckedCheckBox As CheckBox,
        ParticipatingCheckBoxes As List(Of CheckBox)
        )

        Dim ParticipatingCheckBox As CheckBox
        'Dim OtherCheckBox As CheckBox
        'Dim Ctrl As Control

        For Each ParticipatingCheckBox In ParticipatingCheckBoxes
            If Not ParticipatingCheckBox Is NewlyCheckedCheckBox Then
                ParticipatingCheckBox.Checked = False
            End If
        Next


    End Sub

    Public Function GetFormState() As String
        Dim JSONString As String

        Dim tmpJSONDict As New Dictionary(Of String, String)
        'Dim Ctrl As Control
        'Dim CtrlName As String

        Dim TaskType As Type = Me.GetType()
        Dim PropInfos = New List(Of PropertyInfo)(TaskType.GetProperties())

        Dim IgnoreProps As New List(Of String)
        IgnoreProps.AddRange({"Description", "HelpText", "RequiresSave", "AppliesToAssembly", "AppliesToPart", "AppliesToSheetmetal"})
        IgnoreProps.AddRange({"AppliesToDraft", "HasOptions", "HelpURL", "Image", "TaskControl", "TaskOptionsTLP"})
        IgnoreProps.AddRange({"ManualOptionsOnlyString", "Visible", "RequiresSourceDirectories"})
        IgnoreProps.AddRange({"SourceDirectories", "ControlsDict", "RequiresAssemblyTemplate", "RequiresPartTemplate"})
        IgnoreProps.AddRange({"RequiresSheetmetalTemplate", "RequiresDraftTemplate", "RequiresMaterialTable", "Category"})
        IgnoreProps.AddRange({"SolidEdgeRequired", "Application", "Document", "IsOptionsHidden"})
        IgnoreProps.AddRange({"PropertiesData"})

        Dim s As String = ""

        For Each PropInfo As PropertyInfo In PropInfos

            Dim PropInfoName As String = PropInfo.Name

            If IgnoreProps.Contains(PropInfoName) Then
                Continue For
            End If

            Dim PropTypestring = PropInfo.PropertyType.Name

            Select Case PropTypestring.ToLower
                Case "String".ToLower
                    tmpJSONDict(PropInfoName) = CStr(PropInfo.GetValue(Me, Nothing))
                Case "Double".ToLower
                    tmpJSONDict(PropInfoName) = CStr(PropInfo.GetValue(Me, Nothing))
                Case "Int32".ToLower
                    tmpJSONDict(PropInfoName) = CStr(PropInfo.GetValue(Me, Nothing))
                Case "Boolean".ToLower
                    tmpJSONDict(PropInfoName) = CStr(PropInfo.GetValue(Me, Nothing))

                Case "Image".ToLower  ' Nothing to do here and below
                Case "UCTaskControl".ToLower
                Case "ExTableLayoutPanel".ToLower
                Case "List`1".ToLower
                Case "Dictionary`2".ToLower
                Case "Application".ToLower
                Case "SolidEdgeDocument".ToLower
                Case "Int16".ToLower

                Case Else
                    s = String.Format("{0}{1}{2}", s, PropTypestring, vbCrLf)
            End Select

        Next

        If Not s = "" Then
            MsgBox(String.Format("In GetFormState(), {2} PropTypestrings not recognized{0}{1}", vbCrLf, s, Me.Name))
        End If

        tmpJSONDict("TaskName") = Me.Name

        JSONString = JsonConvert.SerializeObject(tmpJSONDict)

        Return JSONString
    End Function

    Public Sub SetFormState(JSONString As String)

        ' Dictionary format
        '{
        '    "ShowCOG":"False",
        '    "HideCOG":"True",
        '    "HideOptions":"False",
        '    "IsSelectedTask":"True",
        '    "IsSelectedAssembly":"True",
        '    "IsSelectedPart":"False",
        '    "IsSelectedSheetmetal":"False",
        '    "IsSelectedDraft":"False"
        '}

        Dim tmpJSONDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

        Dim TaskType As Type = Me.GetType()
        Dim PropInfos = New List(Of PropertyInfo)(TaskType.GetProperties())

        For Each PropInfo As PropertyInfo In PropInfos

            Dim PropInfoName As String = PropInfo.Name

            If tmpJSONDict.Keys.Contains(PropInfoName) Then
                Dim PropTypestring = PropInfo.PropertyType.Name

                Select Case PropTypestring
                    Case "String"
                        PropInfo.SetValue(Me, CStr(tmpJSONDict(PropInfoName)))
                    Case "Double"
                        PropInfo.SetValue(Me, CDbl(tmpJSONDict(PropInfoName)))
                    Case "Int32"
                        PropInfo.SetValue(Me, CInt(tmpJSONDict(PropInfoName)))
                    Case "Boolean"
                        PropInfo.SetValue(Me, CBool(tmpJSONDict(PropInfoName)))
                End Select

            End If

        Next

        Me.ResetTaskColor()

    End Sub



    'CONTROLS FORMATTING

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



    'EVENT HANDLERS


    Public Sub TextBox_GotFocus(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        TextBox.BeginInvoke(New Action(AddressOf TextBox.SelectAll))
    End Sub

End Class