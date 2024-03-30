Option Strict On

Public Class TaskCheckPartNumberDoesNotMatchFilename

    Inherits Task

    Public Property PropertySet As String
    Public Property PropertyName As String
    'Private Property ControlsDict As Dictionary(Of String, Control)

    Enum ControlNames
        PropertySet
        PropertySetLabel
        PropertyName
        PropertyNameLabel
        HideOptions
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

        ' Options
        Me.PropertySet = ""
        Me.PropertyName = ""
    End Sub

    Public Sub New(Task As TaskCheckPartNumberDoesNotMatchFilename)

        ' Options
        Me.PropertySet = Task.PropertySet
        Me.PropertyName = Task.PropertyName
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

        Filename = SEDoc.FullName
        If Filename.Contains("!") Then
            Filename = Filename.Split("!"c)(0)
        End If

        Filename = System.IO.Path.GetFileName(Filename)  ' Removes path

        Dim TC As New Task_Common

        If PropertyName = "" Then
            ExitStatus = 1
            ErrorMessageList.Add("Part number property name blank on the Configuration Tab -- General Page")
        End If

        If ExitStatus = 0 Then
            Dim DocType As String = TC.GetDocType(SEDoc)

            Select Case DocType
                Case = "asm", "par", "psm"

                    Prop = TC.GetProp(SEDoc, Me.PropertySet, Me.PropertyName, 0, False)
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


    Public Overrides Function GetTLPTask(TLPParent As ExTableLayoutPanel) As ExTableLayoutPanel
        ControlsDict = New Dictionary(Of String, Control)

        Dim IU As New InterfaceUtilities

        Me.TLPTask = IU.BuildTLPTask(Me, TLPParent)

        Me.TLPOptions = BuildTLPOptions()

        For Each Control As Control In Me.TLPTask.Controls
            If ControlsDict.Keys.Contains(Control.Name) Then
                MsgBox(String.Format("ControlsDict already has Key '{0}'", Control.Name))
            End If
            ControlsDict(Control.Name) = Control
        Next

        ' Initializations
        Dim ComboBox = CType(ControlsDict(ControlNames.PropertySet.ToString), ComboBox)
        ComboBox.Text = CStr(ComboBox.Items(0))

        Me.TLPTask.Controls.Add(TLPOptions, Me.TLPTask.ColumnCount - 2, 1)

        Return Me.TLPTask
    End Function

    Private Function BuildTLPOptions() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim ComboBox As ComboBox
        Dim ComboBoxItems As List(Of String) = Split("System Custom", " ").ToList
        Dim TextBox As TextBox
        Dim Label As Label
        Dim ControlWidth As Integer = 175

        Dim IU As New InterfaceUtilities

        IU.FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        ComboBox = IU.FormatOptionsComboBox(ControlNames.PropertySet.ToString, ComboBoxItems, "DropDownList")
        ComboBox.Width = ControlWidth
        AddHandler ComboBox.SelectedIndexChanged, AddressOf ComboBoxOptions_SelectedIndexChanged
        tmpTLPOptions.Controls.Add(ComboBox, 0, RowIndex)
        ControlsDict(ComboBox.Name) = ComboBox

        Label = IU.FormatOptionsLabel(ControlNames.PropertySetLabel.ToString, "Part number property set")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        TextBox = IU.FormatOptionsTextBox(ControlNames.PropertyName.ToString, "")
        TextBox.Width = ControlWidth
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf Task_EventHandler.TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        Label = IU.FormatOptionsLabel(ControlNames.PropertyNameLabel.ToString, "Part number property name")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.HideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Private Sub InitializeOptionProperties()
        Dim ComboBox As ComboBox
        Dim CheckBox As CheckBox
        Dim TextBox As TextBox

        ComboBox = CType(ControlsDict(ControlNames.PropertySet.ToString), ComboBox)
        Me.PropertySet = ComboBox.Text

        TextBox = CType(ControlsDict(ControlNames.PropertyName.ToString), TextBox)
        Me.PropertyName = TextBox.Text

        CheckBox = CType(ControlsDict(ControlNames.HideOptions.ToString), CheckBox)
        Me.AutoHideOptions = CheckBox.Checked

    End Sub

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
            Case ControlNames.HideOptions.ToString
                HandleHideOptionsChange(Me, Me.TLPTask, Me.TLPOptions, Checkbox)

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
        HelpString += "The part number is drawn from a property you specify on the **Configuration Tab -- General Page**. "
        HelpString += "It only checks that the part number appears somewhere in the file name. "
        HelpString += "If the part number is, say, `7481-12104` and the file name is `7481-12104 Motor Mount.par`, "
        HelpString += "you will get a match. "
        HelpString += vbCrLf + vbCrLf + "![part_number_matches_file_name](My%20Project/media/part_number_matches_file_name.png)"

        Return HelpString
    End Function



End Class
