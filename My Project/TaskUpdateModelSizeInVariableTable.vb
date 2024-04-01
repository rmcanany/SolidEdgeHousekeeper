Option Strict On

Imports System.Security.Cryptography
Imports SolidEdgeConstants
Imports SolidEdgeFramework

Public Class TaskUpdateModelSizeInVariableTable
    Inherits Task

    Public Property ReportXYZ As Boolean
    Public Property XVariableName As String
    Public Property YVariableName As String
    Public Property ZVariableName As String
    Public Property ReportMinMidMax As Boolean
    Public Property MinVariableName As String
    Public Property MidVariableName As String
    Public Property MaxVariableName As String

    Enum ControlNames
        ReportXYZ
        XVariableName
        YVariableName
        ZVariableName
        ReportMinMidMax
        MinVariableName
        MidVariableName
        MaxVariableName
        HideOptions
        XVariableLabel
        YVariableLabel
        ZVariableLabel
        MinVariableLabel
        MidVariableLabel
        MaxVariableLabel
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
        Me.Image = My.Resources.TaskUpdateModelSizeInVariableTable
        Me.Category = "Update"

        SetColorFromCategory(Me)

        ' Options
        Me.ReportXYZ = False
        Me.XVariableName = ""
        Me.YVariableName = ""
        Me.ZVariableName = ""
        Me.ReportMinMidMax = False
        Me.MinVariableName = ""
        Me.MidVariableName = ""
        Me.MaxVariableName = ""

    End Sub

    Public Sub New(Task As TaskUpdateModelSizeInVariableTable)

        ' Options
        Me.ReportXYZ = Task.ReportXYZ
        Me.XVariableName = Task.XVariableName
        Me.YVariableName = Task.YVariableName
        Me.ZVariableName = Task.ZVariableName
        Me.ReportMinMidMax = Task.ReportMinMidMax
        Me.MinVariableName = Task.MinVariableName
        Me.MidVariableName = Task.MidVariableName
        Me.MaxVariableName = Task.MaxVariableName

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

        Dim Range As New List(Of Double)
        Dim DocVariableDict As New Dictionary(Of String, SolidEdgeFramework.variable)
        Dim Variables As SolidEdgeFramework.Variables
        Dim Variable As SolidEdgeFramework.variable
        Dim VariableNames As New List(Of String)
        Dim VariableName As String
        Dim Formula As String
        Dim i As Integer

        Dim TC As New Task_Common

        ' Generates an exception on some Model Bodies
        Try
            Range = TC.GetDocRange(SEDoc)
        Catch ex As Exception
            ExitStatus = 1
            ErrorMessageList.Add("Unable to obtain stock size")
        End Try

        If ExitStatus = 0 Then
            'DocVariableDict = TC.GetDocVariables(SEDoc)

            Variables = CType(SEDoc.Variables, SolidEdgeFramework.Variables)


            If Me.ReportXYZ Then
                VariableNames.Add(Me.XVariableName)
                VariableNames.Add(Me.YVariableName)
                VariableNames.Add(Me.ZVariableName)

                i = 0
                For Each VariableName In VariableNames
                    Formula = String.Format("{0} m", Range(i))
                    If Not TC.IsVariablePresent(SEDoc, VariableName) Then
                        ' Add it
                        Try
                            ' Pretty sure this must be a variable, not a dimension.
                            Variable = CType(Variables.Add(VariableName, Formula), variable)
                            Variable.Expose = CInt(True)
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Unable to add and/or expose variable '{0}'", VariableName))
                        End Try
                    Else
                        ' Update it
                        Try
                            Variable = DocVariableDict(VariableName)
                            Variable.Formula = Formula
                            Variable.Expose = CInt(True)
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Unable to change and/or expose variable '{0}'", VariableName))
                        End Try
                    End If
                    i += 1
                Next
            End If

            If Me.ReportMinMidMax Then
                If VariableNames.Count > 0 Then VariableNames.Clear()
                VariableNames.Add(Me.MinVariableName)
                VariableNames.Add(Me.MidVariableName)
                VariableNames.Add(Me.MaxVariableName)

                Range.Sort()

                i = 0
                For Each VariableName In VariableNames
                    Formula = String.Format("{0} m", Range(i))
                    If Not TC.IsVariablePresent(SEDoc, VariableName) Then
                        ' Add it
                        Try
                            ' Pretty sure this must be a variable, not a dimension.
                            Variable = CType(Variables.Add(VariableName, Formula), variable)
                            Variable.Expose = CInt(True)
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Unable to add and/or expose variable '{0}'", VariableName))
                        End Try
                    Else
                        ' Update it
                        Try
                            Variable = DocVariableDict(VariableName)
                            Variable.Formula = Formula
                            Variable.Expose = CInt(True)
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Unable to change and/or expose variable '{0}'", VariableName))
                        End Try
                    End If
                    i += 1
                Next
            End If


            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If

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

        ' Initialize display by double-toggling the 'Report' checkboxes.
        Dim CheckBox As CheckBox
        CheckBox = CType(ControlsDict(ControlNames.ReportXYZ.ToString), CheckBox)
        CheckBox.Checked = Not CheckBox.Checked
        CheckBox.Checked = Not CheckBox.Checked
        CheckBox = CType(ControlsDict(ControlNames.ReportMinMidMax.ToString), CheckBox)
        CheckBox.Checked = Not CheckBox.Checked
        CheckBox.Checked = Not CheckBox.Checked

        Me.TLPTask.Controls.Add(TLPOptions, Me.TLPTask.ColumnCount - 2, 1)

        Return Me.TLPTask
    End Function

    Private Function BuildTLPOptions() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Label As Label
        Dim TextBox As TextBox

        Dim IU As New InterfaceUtilities

        IU.FormatTLPOptions(tmpTLPOptions, "TLPOptions", 9)

        RowIndex = 0

        ' XYZ
        CheckBox = IU.FormatOptionsCheckBox(ControlNames.ReportXYZ.ToString, "Report XYZ.  (Enter variable names below)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Label = IU.FormatOptionsLabel(ControlNames.XVariableLabel.ToString, "X")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        ControlsDict(Label.Name) = Label

        TextBox = IU.FormatOptionsTextBox(ControlNames.XVariableName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf Task_EventHandler.TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Label = IU.FormatOptionsLabel(ControlNames.YVariableLabel.ToString, "Y")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        ControlsDict(Label.Name) = Label

        TextBox = IU.FormatOptionsTextBox(ControlNames.YVariableName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf Task_EventHandler.TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Label = IU.FormatOptionsLabel(ControlNames.ZVariableLabel.ToString, "Z")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        ControlsDict(Label.Name) = Label

        TextBox = IU.FormatOptionsTextBox(ControlNames.ZVariableName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf Task_EventHandler.TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox


        ' MinMidMax
        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.ReportMinMidMax.ToString, "Report Min Mid Max.  (Enter variable names below)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Label = IU.FormatOptionsLabel(ControlNames.MinVariableLabel.ToString, "Min")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        ControlsDict(Label.Name) = Label

        TextBox = IU.FormatOptionsTextBox(ControlNames.MinVariableName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf Task_EventHandler.TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Label = IU.FormatOptionsLabel(ControlNames.MidVariableLabel.ToString, "Mid")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        ControlsDict(Label.Name) = Label

        TextBox = IU.FormatOptionsTextBox(ControlNames.MidVariableName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf Task_EventHandler.TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Label = IU.FormatOptionsLabel(ControlNames.MaxVariableLabel.ToString, "Max")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        ControlsDict(Label.Name) = Label

        TextBox = IU.FormatOptionsTextBox(ControlNames.MaxVariableName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf Task_EventHandler.TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        ' HideOptions
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

        CheckBox = CType(ControlsDict(ControlNames.ReportXYZ.ToString), CheckBox)
        Me.ReportXYZ = CheckBox.Checked

        TextBox = CType(ControlsDict(ControlNames.XVariableName.ToString), TextBox)
        Me.XVariableName = TextBox.Text

        TextBox = CType(ControlsDict(ControlNames.YVariableName.ToString), TextBox)
        Me.YVariableName = TextBox.Text

        TextBox = CType(ControlsDict(ControlNames.ZVariableName.ToString), TextBox)
        Me.ZVariableName = TextBox.Text

        CheckBox = CType(ControlsDict(ControlNames.ReportMinMidMax.ToString), CheckBox)
        Me.ReportMinMidMax = CheckBox.Checked

        TextBox = CType(ControlsDict(ControlNames.MinVariableName.ToString), TextBox)
        Me.MinVariableName = TextBox.Text

        TextBox = CType(ControlsDict(ControlNames.MidVariableName.ToString), TextBox)
        Me.MidVariableName = TextBox.Text

        TextBox = CType(ControlsDict(ControlNames.MaxVariableName.ToString), TextBox)
        Me.MaxVariableName = TextBox.Text

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

            If Not (Me.ReportXYZ Or Me.ReportMinMidMax) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select ReportXYZ, ReportMinMidMax, or both", Indent))
            End If

            Dim MissingOrDuplicateErrorMessage = String.Format("{0}Enter unique names for each variable", Indent)

            If Me.ReportXYZ Then
                If CheckDuplicates() Then
                    If Not ErrorMessageList.Contains(Me.Description) Then
                        ErrorMessageList.Add(Me.Description)
                    End If
                    If Not ErrorMessageList.Contains(MissingOrDuplicateErrorMessage) Then
                        ExitStatus = 1
                        ErrorMessageList.Add(MissingOrDuplicateErrorMessage)
                    End If
                End If
            End If

            If Me.ReportMinMidMax Then
                If CheckDuplicates() Then
                    If Not ErrorMessageList.Contains(Me.Description) Then
                        ErrorMessageList.Add(Me.Description)
                    End If
                    If Not ErrorMessageList.Contains(MissingOrDuplicateErrorMessage) Then
                        ExitStatus = 1
                        ErrorMessageList.Add(MissingOrDuplicateErrorMessage)
                    End If
                End If
            End If

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function

    Private Function CheckDuplicates() As Boolean
        Dim HasDuplicates As Boolean = False
        Dim s As String

        Dim XYZList As List(Of String) = {Me.XVariableName.Trim, Me.YVariableName.Trim, Me.ZVariableName.Trim}.ToList
        Dim MinMidMaxList As List(Of String) = {Me.MinVariableName.Trim, Me.MidVariableName.Trim, Me.MaxVariableName.Trim}.ToList

        Dim CombinedList As New List(Of String)
        For Each s In XYZList
            CombinedList.Add(s)
        Next
        For Each s In MinMidMaxList
            CombinedList.Add(s)
        Next

        If Me.ReportXYZ And Me.ReportMinMidMax Then
            If CombinedList.Contains("") Then HasDuplicates = True
            If Not CombinedList.Count = CombinedList.Distinct.Count Then HasDuplicates = True


        ElseIf Me.ReportXYZ Then
            If XYZList.Contains("") Then HasDuplicates = True
            If Not XYZList.Count = XYZList.Distinct.Count Then HasDuplicates = True

        ElseIf Me.ReportMinMidMax Then
            If MinMidMaxList.Contains("") Then HasDuplicates = True
            If Not MinMidMaxList.Count = MinMidMaxList.Distinct.Count Then HasDuplicates = True

        End If

        Return HasDuplicates
    End Function

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name
            Case ControlNames.ReportXYZ.ToString
                Me.ReportXYZ = Checkbox.Checked

                If Me.ReportXYZ Then
                    Me.XVariableName = CType(ControlsDict(ControlNames.XVariableName.ToString), TextBox).Text
                    Me.YVariableName = CType(ControlsDict(ControlNames.YVariableName.ToString), TextBox).Text
                    Me.ZVariableName = CType(ControlsDict(ControlNames.ZVariableName.ToString), TextBox).Text
                End If

                ControlsDict(ControlNames.XVariableLabel.ToString).Visible = Me.ReportXYZ
                ControlsDict(ControlNames.XVariableName.ToString).Visible = Me.ReportXYZ
                ControlsDict(ControlNames.YVariableLabel.ToString).Visible = Me.ReportXYZ
                ControlsDict(ControlNames.YVariableName.ToString).Visible = Me.ReportXYZ
                ControlsDict(ControlNames.ZVariableLabel.ToString).Visible = Me.ReportXYZ
                ControlsDict(ControlNames.ZVariableName.ToString).Visible = Me.ReportXYZ


            Case ControlNames.ReportMinMidMax.ToString
                Me.ReportMinMidMax = Checkbox.Checked

                If Me.ReportMinMidMax Then
                    Me.MinVariableName = CType(ControlsDict(ControlNames.MinVariableName.ToString), TextBox).Text
                    Me.MidVariableName = CType(ControlsDict(ControlNames.MidVariableName.ToString), TextBox).Text
                    Me.MaxVariableName = CType(ControlsDict(ControlNames.MaxVariableName.ToString), TextBox).Text
                End If

                ControlsDict(ControlNames.MinVariableLabel.ToString).Visible = Me.ReportMinMidMax
                ControlsDict(ControlNames.MinVariableName.ToString).Visible = Me.ReportMinMidMax
                ControlsDict(ControlNames.MidVariableLabel.ToString).Visible = Me.ReportMinMidMax
                ControlsDict(ControlNames.MidVariableName.ToString).Visible = Me.ReportMinMidMax
                ControlsDict(ControlNames.MaxVariableLabel.ToString).Visible = Me.ReportMinMidMax
                ControlsDict(ControlNames.MaxVariableName.ToString).Visible = Me.ReportMinMidMax

            Case ControlNames.HideOptions.ToString '"HideOptions"
                HandleHideOptionsChange(Me, Me.TLPTask, Me.TLPOptions, Checkbox)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name
            Case ControlNames.XVariableName.ToString
                Me.XVariableName = TextBox.Text
            Case ControlNames.YVariableName.ToString
                Me.YVariableName = TextBox.Text
            Case ControlNames.ZVariableName.ToString
                Me.ZVariableName = TextBox.Text

            Case ControlNames.MinVariableName.ToString
                Me.MinVariableName = TextBox.Text
            Case ControlNames.MidVariableName.ToString
                Me.MidVariableName = TextBox.Text
            Case ControlNames.MaxVariableName.ToString
                Me.MaxVariableName = TextBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String

        HelpString = "Copies the model size to the variable table. "
        HelpString += "This is primarily intended for standard cross-section material "
        HelpString += "(barstock, channel, etc.), but can be used for any purpose. "
        HelpString += "Exposes the variables so they can be used in a callout, parts list, or the like. "
        HelpString += vbCrLf + vbCrLf + "The size is determined using the built-in Solid Edge `RangeBox`. "
        HelpString += "The range box is oriented along the XYZ axes. "
        HelpString += "Misleading values will result for parts with an off axis orientation, such as a 3D tube. "
        HelpString += vbCrLf + vbCrLf + "![Overall Size Options](My%20Project/media/overall_size_options.png)"
        HelpString += vbCrLf + vbCrLf + "The size can be reported as `XYZ`, or `MinMidMax`, or both. "
        HelpString += "`MinMidMax` is independent of the part's orientation in the file. "
        HelpString += "Set your preference on the **Configuration Tab -- General Page**. "
        HelpString += "Set the desired variable names there, too. "
        HelpString += vbCrLf + vbCrLf + "Note that the values are non-associative copies. "
        HelpString += "Any change to the model will require rerunning this command to update the variable table. "
        HelpString += vbCrLf + vbCrLf + "The command reports sheet metal size in the formed state. "
        HelpString += "For a flat pattern, instead of this using this command, "
        HelpString += "you can use the variables from the flat pattern command -- "
        HelpString += "`Flat_Pattern_Model_CutSizeX`, `Flat_Pattern_Model_CutSizeY`, and `Sheet Metal Gage`. "

        Return HelpString
    End Function

End Class
