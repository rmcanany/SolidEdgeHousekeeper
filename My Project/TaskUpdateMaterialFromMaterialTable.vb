Option Strict On

Public Class TaskUpdateMaterialFromMaterialTable

    Inherits Task

    Public Property ActiveMaterialLibrary As String
    Public Property RemoveFaceStyleOverrides As Boolean

    Enum ControlNames
        Browse
        ActiveMaterialLibrary
        RemoveFaceStyleOverrides
        HideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = False
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = False
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskUpdateMaterialFromMaterialTable
        Me.Category = "Update"
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.ActiveMaterialLibrary = ""
        Me.RemoveFaceStyleOverrides = False
    End Sub

    'Public Sub New(Task As TaskUpdateMaterialFromMaterialTable)

    '    'Options
    '    Me.ActiveMaterialLibrary = Task.ActiveMaterialLibrary
    '    Me.RemoveFaceStyleOverrides = Task.RemoveFaceStyleOverrides
    'End Sub


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

        Dim ExitStatus As Integer = 0
        Dim ErrorMessageList As New List(Of String)
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TC As New Task_Common
        Dim DocType As String = TC.GetDocType(SEDoc)

        Select Case DocType
            Case = "par", "psm"
                Dim MD As New MaterialDoctor
                SupplementalErrorMessage = MD.UpdateMaterialFromMaterialTable(SEDoc, Me.ActiveMaterialLibrary, Me.RemoveFaceStyleOverrides, SEApp)
                AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        End Select

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


    'Public Overrides Function GetTLPTask(TLPParent As ExTableLayoutPanel) As ExTableLayoutPanel
    '    ControlsDict = New Dictionary(Of String, Control)

    '    Dim IU As New InterfaceUtilities

    '    Me.TLPTask = IU.BuildTLPTask(Me, TLPParent)

    '    Me.TLPOptions = BuildTLPOptions()

    '    For Each Control As Control In Me.TLPTask.Controls
    '        If ControlsDict.Keys.Contains(Control.Name) Then
    '            MsgBox(String.Format("ControlsDict already has Key '{0}'", Control.Name))
    '        End If
    '        ControlsDict(Control.Name) = Control
    '    Next

    '    Me.TLPTask.Controls.Add(TLPOptions, Me.TLPTask.ColumnCount - 2, 1)

    '    Return Me.TLPTask
    'End Function

    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Button As Button
        Dim TextBox As TextBox

        Dim IU As New InterfaceUtilities

        IU.FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        Button = IU.FormatOptionsButton(ControlNames.Browse.ToString, "Matl Table")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = IU.FormatOptionsTextBox(ControlNames.ActiveMaterialLibrary.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.RemoveFaceStyleOverrides.ToString, "Remove face style overrides")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

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
        Dim CheckBox As CheckBox
        Dim TextBox As TextBox

        TextBox = CType(ControlsDict(ControlNames.ActiveMaterialLibrary.ToString), TextBox)
        Me.ActiveMaterialLibrary = TextBox.Text

        CheckBox = CType(ControlsDict(ControlNames.RemoveFaceStyleOverrides.ToString), CheckBox)
        Me.RemoveFaceStyleOverrides = CheckBox.Checked

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

            If Not FileIO.FileSystem.FileExists(Me.ActiveMaterialLibrary) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select a valid material table", Indent))
            End If

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name
            Case ControlNames.RemoveFaceStyleOverrides.ToString
                Me.RemoveFaceStyleOverrides = Checkbox.Checked

            Case ControlNames.HideOptions.ToString
                HandleHideOptionsChange(Me, Me.TaskOptionsTLP, Checkbox)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox

        Select Case Name
            Case ControlNames.Browse.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a material table file"
                tmpFileDialog.Filter = "Material Documents|*.mtl"

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.ActiveMaterialLibrary = tmpFileDialog.FileName
                    TextBox = CType(ControlsDict(ControlNames.ActiveMaterialLibrary.ToString), TextBox)
                    TextBox.Text = Me.ActiveMaterialLibrary
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name
            Case ControlNames.ActiveMaterialLibrary.ToString '"ExternalProgram"
                Me.ActiveMaterialLibrary = TextBox.Text
            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub




    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Checks to see if the part's material name and properties match any material "
        HelpString += "in a file you specify on the **Configuration Tab -- Templates Page**. "
        HelpString += vbCrLf + vbCrLf + "If the names match, "
        HelpString += "but their properties (e.g., face style) do not, the material is updated. "
        HelpString += "If the names do not match, or no material is assigned, it is reported in the log file."
        HelpString += vbCrLf + vbCrLf + "You can optionally remove any face style overrides. "
        HelpString += "Set the option on the **Configuration Tab -- General Page**. "

        Return HelpString
    End Function


End Class
