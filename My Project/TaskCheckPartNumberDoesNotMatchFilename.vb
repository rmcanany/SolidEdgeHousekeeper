Option Strict On

Public Class TaskCheckPartNumberDoesNotMatchFilename

    Inherits Task

    Private _PropertySet As String
    Public Property PropertySet As String
        Get
            Return _PropertySet
        End Get
        Set(value As String)
            _PropertySet = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.PropertySet.ToString), ComboBox).Text = value
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
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.PropertyName.ToString), TextBox).Text = value
            End If
        End Set
    End Property


    Private _AutoHideOptions As Boolean
    Public Property AutoHideOptions As Boolean
        Get
            Return _AutoHideOptions
        End Get
        Set(value As Boolean)
            _AutoHideOptions = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AutoHideOptions.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property


    Enum ControlNames
        PropertySet
        PropertySetLabel
        PropertyName
        PropertyNameLabel
        AutoHideOptions
    End Enum


    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = True
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskCheckPartNumberDoesNotMatchFilename
        Me.Category = "Check"
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.PropertySet = ""
        Me.PropertyName = ""

    End Sub

    Public Overrides Function Process(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeFramework.SolidEdgeDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf ProcessInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Public Overrides Function Process(ByVal FileName As String) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Return ErrorMessage

    End Function

    Private Function ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Prop As SolidEdgeFramework.Property = Nothing

        Dim PartNumber As String = ""
        Dim PartNumberFound As Boolean
        Dim Filename As String

        Dim UC As New UtilsCommon

        Filename = UC.SplitFOAName(SEDoc.FullName)("Filename")
        'If Filename.Contains("!") Then
        '    Filename = Filename.Split("!"c)(0)
        'End If

        Filename = System.IO.Path.GetFileName(Filename)  ' Removes path

        If PropertyName = "" Then
            ExitStatus = 1
            ErrorMessageList.Add("Missing part number property name")
        End If

        If ExitStatus = 0 Then
            Dim DocType As String = UC.GetDocType(SEDoc)

            Select Case DocType
                Case = "asm", "par", "psm"

                    Prop = UC.GetProp(SEDoc, Me.PropertySet, Me.PropertyName, 0, False)
                    If Prop Is Nothing Then
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Property name: '{0}' not found in property set: '{1}'",
                                         Me.PropertyName, Me.PropertySet))
                    End If

                    If ExitStatus = 0 Then
                        PartNumber = CStr(Prop.Value).Trim
                        If PartNumber = "" Then
                            ExitStatus = 1
                            ErrorMessageList.Add("Part number not assigned")
                        End If
                    End If

                    If ExitStatus = 0 Then
                        If Not Filename.ToLower.Contains(PartNumber.ToLower) Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Part number '{0}' not found in filename '{1}'", PartNumber, Filename))
                        End If
                    End If

                Case = "dft"
                    Dim tmpSEDoc As SolidEdgeDraft.DraftDocument = CType(SEDoc, SolidEdgeDraft.DraftDocument)

                    Dim ModelLinks As SolidEdgeDraft.ModelLinks = Nothing
                    Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing
                    Dim ModelLinkFilenames As New List(Of String)
                    Dim ModelLinkFilename As String

                    'Get the bare file name without directory information or extension
                    Filename = System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName)

                    ModelLinks = tmpSEDoc.ModelLinks

                    For Each ModelLink In ModelLinks
                        ModelLinkFilenames.Add(System.IO.Path.GetFileNameWithoutExtension(ModelLink.FileName))
                    Next

                    PartNumberFound = False

                    If ModelLinkFilenames.Count > 0 Then
                        For Each ModelLinkFilename In ModelLinkFilenames
                            If Filename = ModelLinkFilename Then
                                PartNumberFound = True
                            End If
                        Next
                        If Not PartNumberFound Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Drawing file name '{0}' not the same as any model file name:", Filename))
                            For Each ModelLinkFilename In ModelLinkFilenames
                                ErrorMessageList.Add(String.Format("    '{0}'", ModelLinkFilename))
                            Next
                        End If
                    End If

                Case Else
                    MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
            End Select

        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim ComboBox As ComboBox
        Dim ComboBoxItems As List(Of String) = Split("System Custom", " ").ToList
        Dim TextBox As TextBox
        Dim Label As Label
        Dim ControlWidth As Integer = 125

        'Dim IU As New InterfaceUtilities

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        ComboBox = FormatOptionsComboBox(ControlNames.PropertySet.ToString, ComboBoxItems, "DropDownList")
        ComboBox.Width = ControlWidth
        AddHandler ComboBox.SelectedIndexChanged, AddressOf ComboBoxOptions_SelectedIndexChanged
        tmpTLPOptions.Controls.Add(ComboBox, 0, RowIndex)
        ControlsDict(ComboBox.Name) = ComboBox

        Label = FormatOptionsLabel(ControlNames.PropertySetLabel.ToString, "Part number prop set")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.PropertyName.ToString, "")
        TextBox.Width = ControlWidth
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        Label = FormatOptionsLabel(ControlNames.PropertyNameLabel.ToString, "Part number prop name")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function


    Public Overrides Function CheckStartConditions(
        PriorErrorMessage As Dictionary(Of Integer, List(Of String))
        ) As Dictionary(Of Integer, List(Of String))

        Dim PriorExitStatus As Integer = PriorErrorMessage.Keys(0)

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim ExitStatus As Integer = 0
        Dim ErrorMessageList = PriorErrorMessage(PriorExitStatus)
        Dim Indent = "    "

        If Me.IsSelectedTask Then
            ' Check start conditions.
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one type of file to process", Indent))
            End If

            If Me.PropertyName = "" Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Enter the property name that contains the part number", Indent))
            End If

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    Public Sub ComboBoxOptions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Dim ComboBox = CType(sender, ComboBox)
        Dim Name = ComboBox.Name

        Select Case Name
            Case ControlNames.PropertySet.ToString
                Me.PropertySet = ComboBox.Text
            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name
            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name
            Case ControlNames.PropertyName.ToString
                Me.PropertyName = TextBox.Text
            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Checks if the file name contains the part number. "

        HelpString += vbCrLf + vbCrLf + "![CheckPartNumberDoesNotMatchFilename](My%20Project/media/task_check_part_number_does_not_match_filename.png)"

        HelpString += vbCrLf + vbCrLf + "Enter the property name that holds part number on the Options panel. "
        HelpString += "A `Property set`, either `System` or `Custom`, is required. "
        HelpString += "For more information, see the **Property Filter** section in this README file. "

        HelpString += vbCrLf + vbCrLf + "The command only checks that the part number appears somewhere in the file name. "
        HelpString += "If the part number is, say, `7481-12104` and the file name is `7481-12104 Motor Mount.par`, "
        HelpString += "you will get a match. "

        Return HelpString
    End Function



End Class
