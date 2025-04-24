Option Strict On

Public Class TaskUpdateModelSizeInVariableTable
    Inherits Task

    Private _ReportXYZ As Boolean
    Public Property ReportXYZ As Boolean
        Get
            Return _ReportXYZ
        End Get
        Set(value As Boolean)
            _ReportXYZ = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ReportXYZ.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _XVariableName As String
    Public Property XVariableName As String
        Get
            Return _XVariableName
        End Get
        Set(value As String)
            _XVariableName = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.XVariableName.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _YVariableName As String
    Public Property YVariableName As String
        Get
            Return _YVariableName
        End Get
        Set(value As String)
            _YVariableName = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.YVariableName.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _ZVariableName As String
    Public Property ZVariableName As String
        Get
            Return _ZVariableName
        End Get
        Set(value As String)
            _ZVariableName = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ZVariableName.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _ReportMinMidMax As Boolean
    Public Property ReportMinMidMax As Boolean
        Get
            Return _ReportMinMidMax
        End Get
        Set(value As Boolean)
            _ReportMinMidMax = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ReportMinMidMax.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _MinVariableName As String
    Public Property MinVariableName As String
        Get
            Return _MinVariableName
        End Get
        Set(value As String)
            _MinVariableName = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.MinVariableName.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _MidVariableName As String
    Public Property MidVariableName As String
        Get
            Return _MidVariableName
        End Get
        Set(value As String)
            _MidVariableName = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.MidVariableName.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _MaxVariableName As String
    Public Property MaxVariableName As String
        Get
            Return _MaxVariableName
        End Get
        Set(value As String)
            _MaxVariableName = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.MaxVariableName.ToString), TextBox).Text = value
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
        ReportXYZ
        XVariableName
        YVariableName
        ZVariableName
        ReportMinMidMax
        MinVariableName
        MidVariableName
        MaxVariableName
        AutoHideOptions
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

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

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


    Public Overrides Sub Process(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application)

        Me.TaskLogger = Me.FileLogger.AddLogger(Me.Description)

        InvokeSTAThread(
            Of SolidEdgeFramework.SolidEdgeDocument,
            SolidEdgeFramework.Application)(
                AddressOf ProcessInternal,
                SEDoc,
                SEApp)
    End Sub

    Public Overrides Sub Process(ByVal FileName As String)
        Me.TaskLogger = Me.FileLogger.AddLogger(Me.Description)
    End Sub

    Private Sub ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        Dim Range As New List(Of Double)
        Dim DocVariableDict As New Dictionary(Of String, SolidEdgeFramework.variable)
        Dim Variables As SolidEdgeFramework.Variables
        Dim Variable As SolidEdgeFramework.variable
        Dim VariableNames As New List(Of String)
        Dim VariableName As String
        Dim Formula As String
        Dim i As Integer

        Dim UC As New UtilsCommon

        ' Generates an exception on some Model Bodies
        Try
            Range = UC.GetDocRange(SEDoc)
        Catch ex As Exception
            TaskLogger.AddMessage("Unable to obtain stock size")
        End Try

        If Not TaskLogger.HasErrors Then
            Variables = CType(SEDoc.Variables, SolidEdgeFramework.Variables)

            If Me.ReportXYZ Then
                VariableNames.Add(Me.XVariableName)
                VariableNames.Add(Me.YVariableName)
                VariableNames.Add(Me.ZVariableName)

                i = 0
                For Each VariableName In VariableNames
                    Formula = String.Format("{0} m", Range(i))
                    If Not UC.IsVariablePresent(SEDoc, VariableName) Then
                        ' Add it
                        Try
                            ' Pretty sure this must be a variable, not a dimension.
                            Variable = CType(Variables.Add(VariableName, Formula), SolidEdgeFramework.variable)
                            Variable.Expose = CInt(True)
                        Catch ex As Exception
                            TaskLogger.AddMessage(String.Format("Unable to add and/or expose variable '{0}'", VariableName))
                        End Try
                    Else
                        ' Update it
                        Try
                            Variable = DocVariableDict(VariableName)
                            Variable.Formula = Formula
                            Variable.Expose = CInt(True)
                        Catch ex As Exception
                            TaskLogger.AddMessage(String.Format("Unable to change and/or expose variable '{0}'", VariableName))
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
                    If Not UC.IsVariablePresent(SEDoc, VariableName) Then
                        ' Add it
                        Try
                            ' Pretty sure this must be a variable, not a dimension.
                            Variable = CType(Variables.Add(VariableName, Formula), SolidEdgeFramework.variable)
                            Variable.Expose = CInt(True)
                        Catch ex As Exception
                            TaskLogger.AddMessage(String.Format("Unable to add and/or expose variable '{0}'", VariableName))
                        End Try
                    Else
                        ' Update it
                        Try
                            Variable = DocVariableDict(VariableName)
                            Variable.Formula = Formula
                            Variable.Expose = CInt(True)
                        Catch ex As Exception
                            TaskLogger.AddMessage(String.Format("Unable to change and/or expose variable '{0}'", VariableName))
                        End Try
                    End If
                    i += 1
                Next
            End If


            If SEDoc.ReadOnly Then
                TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If

        End If

    End Sub


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


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Label As Label
        Dim TextBox As TextBox

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 9)

        RowIndex = 0

        ' XYZ
        CheckBox = FormatOptionsCheckBox(ControlNames.ReportXYZ.ToString, "Report XYZ.  (Enter variable names)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Label = FormatOptionsLabel(ControlNames.XVariableLabel.ToString, "X")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        Label.Visible = False
        ControlsDict(Label.Name) = Label

        TextBox = FormatOptionsTextBox(ControlNames.XVariableName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Label = FormatOptionsLabel(ControlNames.YVariableLabel.ToString, "Y")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        Label.Visible = False
        ControlsDict(Label.Name) = Label

        TextBox = FormatOptionsTextBox(ControlNames.YVariableName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Label = FormatOptionsLabel(ControlNames.ZVariableLabel.ToString, "Z")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        Label.Visible = False
        ControlsDict(Label.Name) = Label

        TextBox = FormatOptionsTextBox(ControlNames.ZVariableName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox


        ' MinMidMax
        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ReportMinMidMax.ToString, "Report Min Mid Max.  (Enter variable names)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Label = FormatOptionsLabel(ControlNames.MinVariableLabel.ToString, "Min")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        Label.Visible = False
        ControlsDict(Label.Name) = Label

        TextBox = FormatOptionsTextBox(ControlNames.MinVariableName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Label = FormatOptionsLabel(ControlNames.MidVariableLabel.ToString, "Mid")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        Label.Visible = False
        ControlsDict(Label.Name) = Label

        TextBox = FormatOptionsTextBox(ControlNames.MidVariableName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        Label = FormatOptionsLabel(ControlNames.MaxVariableLabel.ToString, "Max")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        Label.Visible = False
        ControlsDict(Label.Name) = Label

        TextBox = FormatOptionsTextBox(ControlNames.MaxVariableName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        ' HideOptions
        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If

            If Not (Me.ReportXYZ Or Me.ReportMinMidMax) Then
                ErrorLogger.AddMessage("")
            End If

            Dim s = "Enter unique names for each variable"

            If Me.ReportXYZ Then
                If CheckDuplicates() Then
                    If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
                End If
            End If

            If Me.ReportMinMidMax Then
                If CheckDuplicates() Then
                    If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
                End If
            End If
        End If

    End Sub


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

            Case ControlNames.AutoHideOptions.ToString '"HideOptions"
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

        HelpString += vbCrLf + vbCrLf + "![UpdateModelSizeInVariableTable](My%20Project/media/task_update_model_size_in_variable_table.png)"

        HelpString += vbCrLf + vbCrLf + "This is primarily intended for standard cross-section material "
        HelpString += "(barstock, channel, etc.), but can be used for any purpose. "
        HelpString += "Exposes the variables so they can be used in a callout, parts list, or the like. "

        HelpString += vbCrLf + vbCrLf + "The size is determined using the built-in Solid Edge `RangeBox`. "
        HelpString += "The range box is oriented along the XYZ axes. "
        HelpString += "Misleading values will result for parts with an off axis orientation, such as a 3D tube. "

        HelpString += vbCrLf + vbCrLf + "The size can be reported as `XYZ`, or `MinMidMax`, or both. "
        HelpString += "`MinMidMax` is independent of the part's orientation in the file. "
        HelpString += "Set your preference on the Options panel. "
        HelpString += "Set the desired variable names there, too. "

        HelpString += vbCrLf + vbCrLf + "Note that the values are non-associative copies. "
        HelpString += "Any change to the model will require rerunning this command to update the variable table. "

        HelpString += vbCrLf + vbCrLf + "The command reports sheet metal size in the formed state. "
        HelpString += "For a flat pattern, instead of creating new variables using this command, "
        HelpString += "you can use the variables already created by the flat pattern command -- "
        HelpString += "`Flat_Pattern_Model_CutSizeX`, `Flat_Pattern_Model_CutSizeY`, and `Material Thickness`. "
        HelpString += "If you have an entry in the Material Table Gage Properties Tab `Sheet Metal Gage` combobox, "
        HelpString += "you can use that instead of (or along with) `Material Thickness`. "

        Return HelpString
    End Function

End Class
