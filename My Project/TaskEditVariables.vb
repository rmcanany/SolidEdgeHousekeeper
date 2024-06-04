Option Strict On
Imports Newtonsoft.Json

Public Class TaskEditVariables
    Inherits Task

    Public Property JSONDict As String
    Public Property AutoAddMissingVariable As Boolean
    'Private Property ControlsDict As Dictionary(Of String, Control)

    Enum ControlNames
        Edit
        JSONDict
        AutoAddMissingVariable
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
        Me.AppliesToDraft = False
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskEditVariables
        Me.Category = "Edit"

        SetColorFromCategory(Me)

        ' Options
        Me.JSONDict = ""
        Me.AutoAddMissingVariable = False
    End Sub

    Public Sub New(Task As TaskEditVariables)

        ' Options
        Me.JSONDict = Task.JSONDict
        Me.AutoAddMissingVariable = Task.AutoAddMissingVariable
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
        'Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))
        'Dim SupplementalExitStatus As Integer = 0

        Dim tmpVariablesToEditDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim VariablesToEditDict As New Dictionary(Of Integer, Dictionary(Of String, String))
        Dim ColumnIndexString As String
        Dim ColumnIndex As Integer
        Dim RowIndex As Integer

        'Dim VariableName As String

        Dim DocDimensionDict As New Dictionary(Of String, SolidEdgeFrameworkSupport.Dimension)
        Dim DocVariableDict As New Dictionary(Of String, SolidEdgeFramework.variable)

        Dim Variables As SolidEdgeFramework.Variables = Nothing
        Dim VariableListObject As SolidEdgeFramework.VariableList = Nothing
        Dim Variable As SolidEdgeFramework.variable = Nothing
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension = Nothing
        'Dim VariableTypeName As String

        Dim VariablesToEdit As String = ""
        Dim VariablesToExposeDict As New Dictionary(Of String, String)

        Dim DocType As String

        Dim TC As New Task_Common

        'Dim Proceed As Boolean = True
        Dim tf As Boolean
        'Dim s As String

        DocType = TC.GetDocType(SEDoc)

        VariablesToEdit = Me.JSONDict

        If Not VariablesToEdit = "" Then
            tmpVariablesToEditDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(VariablesToEdit)
            ' Dictionary format
            '{
            '    "1":
            '    {
            '        "VariableName":"v1"
            '        "Formula":"1",
            '        "UnitType":"Distance",
            '        "Expose":"True",
            '        "ExposeName":"var1"
            '    },
            '    "2":
            '    ...
            '}

            For Each ColumnIndexString In tmpVariablesToEditDict.Keys
                ColumnIndex = CInt(ColumnIndexString)
                VariablesToEditDict(ColumnIndex) = New Dictionary(Of String, String)

                For Each Key In tmpVariablesToEditDict(ColumnIndexString).Keys
                    VariablesToEditDict(ColumnIndex)(Key) = tmpVariablesToEditDict(ColumnIndexString)(Key)
                Next

            Next

        Else
            ExitStatus = 1
            ErrorMessageList.Add("No variables provided")
        End If

        If ExitStatus = 0 Then
            Variables = DirectCast(SEDoc.Variables, SolidEdgeFramework.Variables)

            DocDimensionDict = TC.GetDocDimensions(SEDoc)
            If DocDimensionDict Is Nothing Then
                ExitStatus = 1
                ErrorMessageList.Add("Unable to access dimensions")
            End If

            DocVariableDict = TC.GetDocVariables(SEDoc)
            If DocVariableDict Is Nothing Then
                ExitStatus = 1
                ErrorMessageList.Add("Unable to access variables")
            End If

        End If

        If ExitStatus = 0 Then

            ' Process variables.

            For Each RowIndex In VariablesToEditDict.Keys
                Dim VariableName As String = VariablesToEditDict(RowIndex)("VariableName").Trim
                Dim Formula As String = VariablesToEditDict(RowIndex)("Formula").Trim
                Dim UnitType As SolidEdgeConstants.UnitTypeConstants = TC.GetUnitType(VariablesToEditDict(RowIndex)("UnitType").Trim)
                Dim Expose As Boolean = VariablesToEditDict(RowIndex)("Expose").Trim.ToLower = "true"
                Dim ExposeName As String = VariablesToEditDict(RowIndex)("ExposeName").Trim

                tf = DocDimensionDict.Keys.Contains(VariableName)
                tf = tf Or DocVariableDict.Keys.Contains(VariableName)

                If Not tf Then  ' Add it.
                    If Me.AutoAddMissingVariable Then
                        If Formula = "" Then  ' Can't add a variable without a formula
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Unable to add variable named '{0}'.  No value or formula supplied.", VariableName))
                            Continue For
                        End If

                        Try
                            ' Pretty sure this must be a variable, not a dimension.
                            Variable = CType(Variables.Add(VariableName, Formula, UnitType), SolidEdgeFramework.variable)
                            If Expose Then
                                Variable.Expose = CInt(Expose)
                                If Not ExposeName = "" Then
                                    Variable.ExposeName = ExposeName
                                End If
                            End If
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Unable to add and/or expose variable '{0}'", VariableName))

                        End Try

                    End If

                Else  ' Edit and/or Expose.
                    Try
                        If DocDimensionDict.Keys.Contains(VariableName) Then
                            Dimension = DocDimensionDict(VariableName)
                            If Expose Then
                                Dimension.Expose = CInt(Expose)
                                If Not ExposeName = "" Then
                                    Dimension.ExposeName = ExposeName
                                End If
                            End If
                            If Not Formula = "" Then
                                Dimension.Formula = Formula
                            End If

                        ElseIf DocVariableDict.Keys.Contains(VariableName) Then
                            Variable = DocVariableDict(VariableName)
                            If Expose Then
                                Variable.Expose = CInt(Expose)
                                If Not ExposeName = "" Then
                                    Variable.ExposeName = ExposeName
                                End If
                            End If
                            If Not Formula = "" Then
                                Variable.Formula = Formula
                            End If

                        End If
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Unable to change variable '{0}'", VariableName))
                    End Try

                End If
            Next

        End If

        If ExitStatus = 0 Then
            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        End If

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
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

        Me.TLPTask.Controls.Add(TLPOptions, Me.TLPTask.ColumnCount - 2, 1)

        Return Me.TLPTask
    End Function

    Private Function BuildTLPOptions() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Button As Button
        Dim TextBox As TextBox

        Dim IU As New InterfaceUtilities

        IU.FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        Button = IU.FormatOptionsButton(ControlNames.Edit.ToString, "Edit")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = IU.FormatOptionsTextBox(ControlNames.JSONDict.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.AutoAddMissingVariable.ToString, "Add any variable not already in the file")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.HideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        ControlsDict(CheckBox.Name) = CheckBox

        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)

        Return tmpTLPOptions
    End Function

    Private Sub InitializeOptionProperties()
        Dim CheckBox As CheckBox
        Dim TextBox As TextBox

        TextBox = CType(ControlsDict(ControlNames.JSONDict.ToString), TextBox)
        Me.JSONDict = TextBox.Text

        CheckBox = CType(ControlsDict(ControlNames.AutoAddMissingVariable.ToString), CheckBox)
        Me.AutoAddMissingVariable = CheckBox.Checked

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

            If (Me.JSONDict = "") Or (Me.JSONDict = "{}") Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one variable to edit", Indent))
            End If

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox

        Select Case Name
            Case ControlNames.Edit.ToString
                Dim VariableInputEditor As New FormVariableInputEditor

                VariableInputEditor.JSONDict = Me.JSONDict

                ' Workaround
                Dim FileType = "asm"

                VariableInputEditor.ShowInputEditor(FileType)

                If VariableInputEditor.DialogResult = DialogResult.OK Then
                    Me.JSONDict = VariableInputEditor.JSONDict

                    TextBox = CType(ControlsDict(ControlNames.JSONDict.ToString), TextBox)
                    TextBox.Text = Me.JSONDict
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name
        'Dim Ctrl As Control
        'Dim Button As Button

        Select Case Name
            Case ControlNames.AutoAddMissingVariable.ToString
                Me.AutoAddMissingVariable = Checkbox.Checked

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
            Case ControlNames.JSONDict.ToString
                Me.JSONDict = TextBox.Text
            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String

        HelpString = "Adds, changes, and/or exposes variables.  The information is entered on the Input Editor. "
        HelpString += "Access the form using the `Variables edit/add/expose` `Edit` button. "
        HelpString += "It is located below the task list on each **Task Tab**."
        HelpString += vbCrLf + vbCrLf + "![Variable_Editor](My%20Project/media/variable_input_editor.png)"
        HelpString += vbCrLf + vbCrLf + "The Variable name is required.  There are restrictions on the name.  "
        HelpString += "It cannot start with a number.  It can only contain letters and numbers and the "
        HelpString += "underscore '_' character."
        HelpString += vbCrLf + vbCrLf + "If a variable on the list is not in the file, it can optionally be added automatically.  "
        HelpString += "Set the option on the **Configuration Tab -- General Page**. "
        HelpString += vbCrLf + vbCrLf + "The number/formula is not required if only exposing an existing variable, "
        HelpString += "otherwise it is.  If a formula references a variable not in the file, the "
        HelpString += "program will report an error."
        HelpString += vbCrLf + vbCrLf + "If exposing a variable, the Expose name defaults to the variable name. "
        HelpString += "You can optionally change it.  The Expose name does not have restrictions like the variable name. "
        HelpString += vbCrLf + vbCrLf + "The variables are processed in the order in the table. "
        HelpString += "You can change the order by selecting a row and using the Up/Down buttons "
        HelpString += "at the top of the form.  Only one row can be moved at a time.  "
        HelpString += "The delete button, also at the top of the form, removes selected rows.  "
        HelpString += vbCrLf + vbCrLf + "You can copy the settings on the form to other tabs.  "
        HelpString += "Set the `Copy To` CheckBoxes as desired."
        HelpString += vbCrLf + vbCrLf + "Note the textbox adjacent to the `Edit` button "
        HelpString += "is a `Dictionary` representation of the table settings in `JSON` format. "
        HelpString += "You can edit it if you want, but the form is probably easier to use. "

        Return HelpString
    End Function

End Class
