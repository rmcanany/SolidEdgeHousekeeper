Option Strict On

Public Class TaskUpdateDrawingStylesFromTemplate

    Inherits Task

    Private _DraftTemplate As String
    Public Property DraftTemplate As String
        Get
            Return _DraftTemplate
        End Get
        Set(value As String)
            _DraftTemplate = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DraftTemplate.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _UseConfigurationPageTemplates As Boolean
    Public Property UseConfigurationPageTemplates As Boolean
        Get
            Return _UseConfigurationPageTemplates
        End Get
        Set(value As Boolean)
            _UseConfigurationPageTemplates = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UseConfigurationPageTemplates.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateBorder As Boolean
    Public Property UpdateBorder As Boolean
        Get
            Return _UpdateBorder
        End Get
        Set(value As Boolean)
            _UpdateBorder = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateBorder.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UpdateStyles As Boolean
    Public Property UpdateStyles As Boolean
        Get
            Return _UpdateStyles
        End Get
        Set(value As Boolean)
            _UpdateStyles = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UpdateStyles.ToString), CheckBox).Checked = value
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
        UseConfigurationPageTemplates
        Browse
        DraftTemplate
        UpdateBorder
        UpdateStyles
        AutoHideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = False
        Me.AppliesToPart = False
        Me.AppliesToSheetmetal = False
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskUpdateDrawingStylesFromTemplate
        Me.Category = "Restyle"
        Me.RequiresDraftTemplate = True
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.DraftTemplate = ""
        Me.UpdateBorder = False
        Me.UpdateStyles = False

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

        If SEDoc.FullName = Me.DraftTemplate Then
            TaskLogger.AddMessage("Template file itself ineligible for processing")
            Exit Sub
        End If

        Dim SETemplateDoc As SolidEdgeDraft.DraftDocument = Nothing

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim MissingStyles As String = ""

        Dim UC As New UtilsCommon

        Dim tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

        'Open template
        Try
            SETemplateDoc = CType(SEApp.Documents.Open(Me.DraftTemplate), SolidEdgeDraft.DraftDocument)
            SEApp.DoIdle()
        Catch ex As Exception
            TaskLogger.AddMessage(String.Format("Could not open template '{0}'", Me.DraftTemplate))
        End Try

        SEDoc.Activate()
        SEApp.DoIdle()

        If Me.UpdateBorder And SETemplateDoc IsNot Nothing Then
            Dim TemplateSheetNames As New List(Of String)

            For Each Sheet In UC.GetSheets(SETemplateDoc, "Background")
                TemplateSheetNames.Add(Sheet.Name)
            Next

            For Each Sheet In UC.GetSheets(tmpSEDoc, "Background")
                If TemplateSheetNames.Contains(Sheet.Name) Then
                    Sheet.ReplaceBackground(DraftTemplate, Sheet.Name)
                    SEApp.DoIdle()
                Else
                    TaskLogger.AddMessage(String.Format("Template has no background named '{0}'", Sheet.Name))
                End If
            Next

        End If


        ' All style collections.
        ' DashStyles, DimensionStyles, DrawingViewStyles, FillStyles, HatchPatternStyles, 
        ' LinearStyles, SmartFrame2dStyles, TableStyles, TextCharStyles, TextStyles

        ' Style collections to receive updates.
        ' DimensionStyles, DrawingViewStyles, LinearStyles, TableStyles, TextCharStyles, TextStyles

        ' Styles not updated at this time.
        ' DashStyles, FillStyles, HatchPatternStyles, SmartFrame2dStyles


        If Me.UpdateStyles And SETemplateDoc IsNot Nothing Then

            Dim TemplateStyleInDoc As Boolean

            ' ############ DimensionStyles ############

            Dim DocDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles
            DocDimensionStyles = CType(tmpSEDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)

            Dim TemplateDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles
            TemplateDimensionStyles = CType(SETemplateDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)

            For Each TemplateDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle In TemplateDimensionStyles
                If Not TemplateStyleNames.Contains(TemplateDimensionStyle.Name) Then
                    TemplateStyleNames.Add(TemplateDimensionStyle.Name)
                End If
                TemplateStyleInDoc = False
                For Each DocDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle In DocDimensionStyles
                    If Not DocStyleNames.Contains(DocDimensionStyle.Name) Then
                        DocStyleNames.Add(DocDimensionStyle.Name)
                    End If
                    If TemplateDimensionStyle.Name = DocDimensionStyle.Name Then
                        TemplateStyleInDoc = True
                        Try
                            UC.CopyProperties(TemplateDimensionStyle, DocDimensionStyle, TaskLogger.AddLogger($"Dimension style {TemplateDimensionStyle.Name}"))
                            ' #### The following are not updating correctly in SE2019 with UC.CopyProperties
                            DocDimensionStyle.HoleCalloutCounterdrill = TemplateDimensionStyle.HoleCalloutCounterdrill
                            DocDimensionStyle.HoleCalloutCounterdrillThreaded = TemplateDimensionStyle.HoleCalloutCounterdrillThreaded
                        Catch ex As Exception
                            TaskLogger.AddMessage(String.Format("Error applying DimensionStyle '{0}'", TemplateDimensionStyle.Name))
                        End Try
                    End If
                Next
                If Not TemplateStyleInDoc Then
                    Dim tmpDocDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle
                    Try
                        tmpDocDimensionStyle = DocDimensionStyles.Add(TemplateDimensionStyle.Name, "")
                        UC.CopyProperties(TemplateDimensionStyle, tmpDocDimensionStyle, TaskLogger.AddLogger($"Dimension style {TemplateDimensionStyle.Name}"))
                        ' #### The following are not updating correctly in SE2019 with UC.CopyProperties
                        tmpDocDimensionStyle.HoleCalloutCounterdrill = TemplateDimensionStyle.HoleCalloutCounterdrill
                        tmpDocDimensionStyle.HoleCalloutCounterdrillThreaded = TemplateDimensionStyle.HoleCalloutCounterdrillThreaded
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Error adding DimensionStyle '{0}'", TemplateDimensionStyle.Name))
                    End Try
                End If
            Next

            MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
            If Len(MissingStyles) > 0 Then
                TaskLogger.AddMessage(String.Format("Dimension styles in Draft but not in Template: {0}", MissingStyles))
            End If
            DocStyleNames.Clear()
            TemplateStyleNames.Clear()
            MissingStyles = ""


            ' ############ DrawingViewStyles ############

            Dim DocDrawingViewStyles As SolidEdgeFrameworkSupport.DrawingViewStyles
            DocDrawingViewStyles = CType(tmpSEDoc.DrawingViewStyles, SolidEdgeFrameworkSupport.DrawingViewStyles)

            Dim TemplateDrawingViewStyles As SolidEdgeFrameworkSupport.DrawingViewStyles
            TemplateDrawingViewStyles = CType(SETemplateDoc.DrawingViewStyles, SolidEdgeFrameworkSupport.DrawingViewStyles)

            For Each TemplateDrawingViewStyle As SolidEdgeFrameworkSupport.DrawingViewStyle In TemplateDrawingViewStyles
                If Not TemplateStyleNames.Contains(TemplateDrawingViewStyle.Name) Then
                    TemplateStyleNames.Add(TemplateDrawingViewStyle.Name)
                End If
                TemplateStyleInDoc = False
                For Each DocDrawingViewStyle As SolidEdgeFrameworkSupport.DrawingViewStyle In DocDrawingViewStyles
                    If Not DocStyleNames.Contains(DocDrawingViewStyle.Name) Then
                        DocStyleNames.Add(DocDrawingViewStyle.Name)
                    End If
                    If TemplateDrawingViewStyle.Name = DocDrawingViewStyle.Name Then
                        TemplateStyleInDoc = True
                        Try
                            UC.CopyProperties(TemplateDrawingViewStyle, DocDrawingViewStyle, TaskLogger.AddLogger($"Drawing view style {TemplateDrawingViewStyle.Name}"))
                        Catch ex As Exception
                            TaskLogger.AddMessage(String.Format("Error applying DrawingViewStyle '{0}'", TemplateDrawingViewStyle.Name))
                        End Try
                    End If
                Next
                If Not TemplateStyleInDoc Then
                    Dim tmpDocDrawingViewStyle As SolidEdgeFrameworkSupport.DrawingViewStyle
                    Try
                        tmpDocDrawingViewStyle = DocDrawingViewStyles.Add(TemplateDrawingViewStyle.Name, "")
                        UC.CopyProperties(TemplateDrawingViewStyle, tmpDocDrawingViewStyle, TaskLogger.AddLogger($"Drawing view style {TemplateDrawingViewStyle.Name}"))
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Error adding DrawingViewStyle '{0}'", TemplateDrawingViewStyle.Name))
                    End Try
                End If
            Next

            MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
            If Len(MissingStyles) > 0 Then
                TaskLogger.AddMessage(String.Format("Drawing View styles in Draft but not in Template: {0}", MissingStyles))
            End If
            DocStyleNames.Clear()
            TemplateStyleNames.Clear()
            MissingStyles = ""


            ' ############ LinearStyles ############

            Dim DocLinearStyles As SolidEdgeFramework.LinearStyles
            DocLinearStyles = CType(tmpSEDoc.LinearStyles, SolidEdgeFramework.LinearStyles)

            Dim TemplateLinearStyles As SolidEdgeFramework.LinearStyles
            TemplateLinearStyles = CType(SETemplateDoc.LinearStyles, SolidEdgeFramework.LinearStyles)

            For Each TemplateLinearStyle As SolidEdgeFramework.LinearStyle In TemplateLinearStyles
                If Not TemplateStyleNames.Contains(TemplateLinearStyle.Name) Then
                    TemplateStyleNames.Add(TemplateLinearStyle.Name)
                End If
                TemplateStyleInDoc = False
                For Each DocLinearStyle As SolidEdgeFramework.LinearStyle In DocLinearStyles
                    If Not DocStyleNames.Contains(DocLinearStyle.Name) Then
                        DocStyleNames.Add(DocLinearStyle.Name)
                    End If
                    If TemplateLinearStyle.Name = DocLinearStyle.Name Then
                        TemplateStyleInDoc = True
                        Try
                            UC.CopyProperties(TemplateLinearStyle, DocLinearStyle, TaskLogger.AddLogger($"Linear style {TemplateLinearStyle.Name}"))
                        Catch ex As Exception
                            TaskLogger.AddMessage(String.Format("Error applying LinearStyle '{0}'", TemplateLinearStyle.Name))
                        End Try
                    End If
                Next
                If Not TemplateStyleInDoc Then
                    Dim tmpDocLinearStyle As SolidEdgeFramework.LinearStyle
                    Try
                        tmpDocLinearStyle = DocLinearStyles.Add(TemplateLinearStyle.Name, "")
                        UC.CopyProperties(TemplateLinearStyle, tmpDocLinearStyle, TaskLogger.AddLogger($"Linear style {TemplateLinearStyle.Name}"))
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Error adding LinearStyle '{0}'", TemplateLinearStyle.Name))
                    End Try
                End If
            Next

            MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
            If Len(MissingStyles) > 0 Then
                TaskLogger.AddMessage(String.Format("Linear styles in Draft but not in Template: {0}", MissingStyles))
            End If
            DocStyleNames.Clear()
            TemplateStyleNames.Clear()
            MissingStyles = ""


            ' ############ TableStyles ############

            Dim DocTableStyles As SolidEdgeFrameworkSupport.TableStyles
            DocTableStyles = CType(tmpSEDoc.TableStyles, SolidEdgeFrameworkSupport.TableStyles)

            Dim TemplateTableStyles As SolidEdgeFrameworkSupport.TableStyles
            TemplateTableStyles = CType(SETemplateDoc.TableStyles, SolidEdgeFrameworkSupport.TableStyles)

            For Each TemplateTableStyle As SolidEdgeFrameworkSupport.TableStyle In TemplateTableStyles
                If Not TemplateStyleNames.Contains(TemplateTableStyle.Name) Then
                    TemplateStyleNames.Add(TemplateTableStyle.Name)
                End If
                TemplateStyleInDoc = False
                For Each DocTableStyle As SolidEdgeFrameworkSupport.TableStyle In DocTableStyles
                    If Not DocStyleNames.Contains(DocTableStyle.Name) Then
                        DocStyleNames.Add(DocTableStyle.Name)
                    End If
                    If TemplateTableStyle.Name = DocTableStyle.Name Then
                        TemplateStyleInDoc = True
                        Try
                            UC.CopyProperties(TemplateTableStyle, DocTableStyle, TaskLogger.AddLogger($"Table style {TemplateTableStyle.Name}"))
                            '#### added because CopyProperties didn't work in old SE Release, to be verified if still needed
                            For c = 0 To 6
                                DocTableStyle.LineColor(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineColor(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                                DocTableStyle.LineDashType(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineDashType(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                                DocTableStyle.LineWidth(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineWidth(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                            Next
                            '#### 20251021 CopyProperties not doing these either
                            DocTableStyle.HeaderTextStyle = TemplateTableStyle.HeaderTextStyle
                            DocTableStyle.DataTextStyle = TemplateTableStyle.DataTextStyle
                        Catch ex As Exception
                            TaskLogger.AddMessage(String.Format("Error applying TableStyle '{0}'", TemplateTableStyle.Name))
                        End Try
                    End If
                Next
                If Not TemplateStyleInDoc Then
                    Dim tmpDocTableStyle As SolidEdgeFrameworkSupport.TableStyle
                    Try
                        tmpDocTableStyle = DocTableStyles.Add(TemplateTableStyle.Name, "")
                        UC.CopyProperties(TemplateTableStyle, tmpDocTableStyle, TaskLogger.AddLogger($"Table style {TemplateTableStyle.Name}"))
                        '#### added because CopyProperties didn't work in old SE Release, to be verified if still needed
                        For c = 0 To 6
                            tmpDocTableStyle.LineColor(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineColor(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                            tmpDocTableStyle.LineDashType(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineDashType(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                            tmpDocTableStyle.LineWidth(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineWidth(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                        Next
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Error adding TableStyle '{0}'", TemplateTableStyle.Name))
                    End Try
                End If
            Next

            MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
            If Len(MissingStyles) > 0 Then
                TaskLogger.AddMessage(String.Format("Table styles in Draft but not in Template: {0}", MissingStyles))
            End If
            DocStyleNames.Clear()
            TemplateStyleNames.Clear()
            MissingStyles = ""


            ' ############ TextCharStyles ############

            Dim DocTextCharStyles As SolidEdgeFramework.TextCharStyles
            DocTextCharStyles = CType(tmpSEDoc.TextCharStyles, SolidEdgeFramework.TextCharStyles)

            Dim TemplateTextCharStyles As SolidEdgeFramework.TextCharStyles
            TemplateTextCharStyles = CType(SETemplateDoc.TextCharStyles, SolidEdgeFramework.TextCharStyles)

            For Each TemplateTextCharStyle As SolidEdgeFramework.TextCharStyle In TemplateTextCharStyles
                If Not TemplateStyleNames.Contains(TemplateTextCharStyle.Name) Then
                    TemplateStyleNames.Add(TemplateTextCharStyle.Name)
                End If
                TemplateStyleInDoc = False
                For Each DocTextCharStyle As SolidEdgeFramework.TextCharStyle In DocTextCharStyles
                    If Not DocStyleNames.Contains(DocTextCharStyle.Name) Then
                        DocStyleNames.Add(DocTextCharStyle.Name)
                    End If
                    If TemplateTextCharStyle.Name = DocTextCharStyle.Name Then
                        TemplateStyleInDoc = True
                        Try
                            UC.CopyProperties(TemplateTextCharStyle, DocTextCharStyle, TaskLogger.AddLogger($"Text char style {TemplateTextCharStyle.Name}"))
                        Catch ex As Exception
                            TaskLogger.AddMessage(String.Format("Error applying TextCharStyle '{0}'", TemplateTextCharStyle.Name))
                        End Try
                    End If
                Next
                If Not TemplateStyleInDoc Then
                    Dim tmpDocTextCharStyle As SolidEdgeFramework.TextCharStyle
                    Try
                        tmpDocTextCharStyle = DocTextCharStyles.Add(TemplateTextCharStyle.Name, "")
                        UC.CopyProperties(TemplateTextCharStyle, tmpDocTextCharStyle, TaskLogger.AddLogger($"Text char style {TemplateTextCharStyle.Name}"))
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Error adding TextCharStyle '{0}'", TemplateTextCharStyle.Name))
                    End Try
                End If
            Next

            MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
            If Len(MissingStyles) > 0 Then
                TaskLogger.AddMessage(String.Format("Text Char styles in Draft but not in Template: {0}", MissingStyles))
            End If
            DocStyleNames.Clear()
            TemplateStyleNames.Clear()
            MissingStyles = ""


            ' ############ TextStyles ############

            Dim DocTextStyles As SolidEdgeFramework.TextStyles
            DocTextStyles = CType(tmpSEDoc.TextStyles, SolidEdgeFramework.TextStyles)

            Dim TemplateTextStyles As SolidEdgeFramework.TextStyles
            TemplateTextStyles = CType(SETemplateDoc.TextStyles, SolidEdgeFramework.TextStyles)

            For Each TemplateTextStyle As SolidEdgeFramework.TextStyle In TemplateTextStyles
                If Not TemplateStyleNames.Contains(TemplateTextStyle.Name) Then
                    TemplateStyleNames.Add(TemplateTextStyle.Name)
                End If
                TemplateStyleInDoc = False
                For Each DocTextStyle As SolidEdgeFramework.TextStyle In DocTextStyles
                    If Not DocStyleNames.Contains(DocTextStyle.Name) Then
                        DocStyleNames.Add(DocTextStyle.Name)
                    End If
                    If TemplateTextStyle.Name = DocTextStyle.Name Then
                        TemplateStyleInDoc = True
                        Try
                            UC.CopyProperties(TemplateTextStyle, DocTextStyle, TaskLogger.AddLogger($"Text style {TemplateTextStyle.Name}"))
                        Catch ex As Exception
                            TaskLogger.AddMessage(String.Format("Error applying TextStyle '{0}'", TemplateTextStyle.Name))
                        End Try
                    End If
                Next
                If Not TemplateStyleInDoc Then
                    Dim tmpDocTextStyle As SolidEdgeFramework.TextStyle
                    Try
                        tmpDocTextStyle = DocTextStyles.Add(TemplateTextStyle.Name, "")
                        UC.CopyProperties(TemplateTextStyle, tmpDocTextStyle, TaskLogger.AddLogger($"Text style {TemplateTextStyle.Name}"))
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Error adding TextStyle '{0}'", TemplateTextStyle.Name))
                    End Try
                End If
            Next

            MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
            If Len(MissingStyles) > 0 Then
                TaskLogger.AddMessage(String.Format("Text styles in Draft but not in Template: {0}", MissingStyles))
            End If
            DocStyleNames.Clear()
            TemplateStyleNames.Clear()
            MissingStyles = ""
        End If

        If SETemplateDoc IsNot Nothing Then
            SETemplateDoc.Close(False)
            SEApp.DoIdle()

            If SEDoc.ReadOnly Then
                TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If

        End If

    End Sub


    Private Function DocStyleNotInTemplate(
       DocStyleNameList As List(Of String),
       TemplateStyleNameList As List(Of String)
       ) As String
        Dim Names As String = ""
        For Each s As String In DocStyleNameList
            If Not TemplateStyleNameList.Contains(s) Then
                Names = String.Format("{0} {1},", Names, s)
            End If
        Next

        If Len(Names) > 0 Then
            ' Remove trailing comma.
            Names = Names.Substring(0, Len(Names) - 1)
        End If

        Return Names
    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim TextBox As TextBox
        Dim Button As Button

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.UseConfigurationPageTemplates.ToString, "Use configuration page templates")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.Browse.ToString, "Dft Template")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.DraftTemplate.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateBorder.ToString, "Update drawing border")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UpdateStyles.ToString, "Update styles")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

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

            If Not FileIO.FileSystem.FileExists(Me.DraftTemplate) Then
                ErrorLogger.AddMessage("Select a valid drawing template")
            End If

            If Not (Me.UpdateBorder Or Me.UpdateStyles) Then
                ErrorLogger.AddMessage("Select Update border, Update styles, or both")
            End If

        End If

    End Sub


    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox

        Select Case Name

            Case ControlNames.Browse.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a draft template file"
                tmpFileDialog.Filter = "dft files|*.dft"

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.DraftTemplate = tmpFileDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.DraftTemplate.ToString), TextBox)
                    TextBox.Text = Me.DraftTemplate
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select

    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.UseConfigurationPageTemplates.ToString
                Me.UseConfigurationPageTemplates = Checkbox.Checked

                If Me.UseConfigurationPageTemplates Then
                    Me.DraftTemplate = Form_Main.DraftTemplate
                    CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = False
                    CType(ControlsDict(ControlNames.DraftTemplate.ToString), TextBox).Visible = False

                Else
                    CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = True
                    CType(ControlsDict(ControlNames.DraftTemplate.ToString), TextBox).Visible = True

                End If

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case ControlNames.UpdateBorder.ToString
                Me.UpdateBorder = Checkbox.Checked

            Case ControlNames.UpdateStyles.ToString
                Me.UpdateStyles = Checkbox.Checked

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name

            Case ControlNames.DraftTemplate.ToString
                Me.DraftTemplate = TextBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Updates styles and/or background sheets from a template you specify. "

        HelpString += vbCrLf + vbCrLf + "![UpdateDrawingStylesFromTemplate](My%20Project/media/task_update_drawing_styles_from_template.png)"

        HelpString += vbCrLf + vbCrLf
        HelpString += "These styles are processed: `DimensionStyles`, `DrawingViewStyles`, `LinearStyles`, `TableStyles`, `TextCharStyles`, `TextStyles`. "
        HelpString += "These are not: `FillStyles`, `HatchPatternStyles`, `SmartFrame2dStyles`. "
        HelpString += "The latter group encountered errors with the current implementation.  The errors were not thoroughly investigated, however. "
        HelpString += "If you need one or more of those styles updated, please ask on the Forum. "

        Return HelpString
    End Function


End Class
