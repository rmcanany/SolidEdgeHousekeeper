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

        'Dim TemplateFilename As String = Configuration("TextBoxTemplateDraft")
        Dim SETemplateDoc As SolidEdgeDraft.DraftDocument

        Dim DocStyleNames As New List(Of String)
        Dim TemplateStyleNames As New List(Of String)
        Dim MissingStyles As String = ""

        'Dim SupplementalExitStatus As Integer = 0
        'Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim UC As New UtilsCommon

        Dim tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

        'Open template
        SETemplateDoc = CType(SEApp.Documents.Open(Me.DraftTemplate), SolidEdgeDraft.DraftDocument)
        SEApp.DoIdle()

        SEDoc.Activate()
        SEApp.DoIdle()

        If Me.UpdateBorder Then
            Dim TemplateSheetNames As New List(Of String)

            For Each Sheet In UC.GetSheets(SETemplateDoc, "Background")
                TemplateSheetNames.Add(Sheet.Name)
            Next

            'SETemplateDoc.Close()
            'SEApp.DoIdle()

            For Each Sheet In UC.GetSheets(tmpSEDoc, "Background")
                If TemplateSheetNames.Contains(Sheet.Name) Then
                    Sheet.ReplaceBackground(DraftTemplate, Sheet.Name)
                Else
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("Template has no background named '{0}'", Sheet.Name))
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


        If Me.UpdateStyles Then

            ' ############ DimensionStyles ############
            Dim DocDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles
            DocDimensionStyles = CType(tmpSEDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)

            Dim TemplateDimensionStyles As SolidEdgeFrameworkSupport.DimensionStyles
            TemplateDimensionStyles = CType(SETemplateDoc.DimensionStyles, SolidEdgeFrameworkSupport.DimensionStyles)

            For Each TemplateDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle In TemplateDimensionStyles
                If Not TemplateStyleNames.Contains(TemplateDimensionStyle.Name) Then
                    TemplateStyleNames.Add(TemplateDimensionStyle.Name)
                End If
                For Each DocDimensionStyle As SolidEdgeFrameworkSupport.DimensionStyle In DocDimensionStyles
                    If Not DocStyleNames.Contains(DocDimensionStyle.Name) Then
                        DocStyleNames.Add(DocDimensionStyle.Name)
                    End If
                    If TemplateDimensionStyle.Name = DocDimensionStyle.Name Then
                        Try
                            UC.CopyProperties(TemplateDimensionStyle, DocDimensionStyle)
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Error applying DimensionStyle '{0}'", TemplateDimensionStyle.Name))
                        End Try
                    End If
                Next
            Next

            MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
            If Len(MissingStyles) > 0 Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Dimension styles in Draft but not in Template: {0}", MissingStyles))
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
                For Each DocDrawingViewStyle As SolidEdgeFrameworkSupport.DrawingViewStyle In DocDrawingViewStyles
                    If Not DocStyleNames.Contains(DocDrawingViewStyle.Name) Then
                        DocStyleNames.Add(DocDrawingViewStyle.Name)
                    End If
                    If TemplateDrawingViewStyle.Name = DocDrawingViewStyle.Name Then
                        Try
                            UC.CopyProperties(TemplateDrawingViewStyle, DocDrawingViewStyle)
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Error applying DrawingViewStyle '{0}'", TemplateDrawingViewStyle.Name))
                        End Try
                    End If
                Next
            Next

            MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
            If Len(MissingStyles) > 0 Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Drawing View styles in Draft but not in Template: {0}", MissingStyles))
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
                For Each DocLinearStyle As SolidEdgeFramework.LinearStyle In DocLinearStyles
                    If Not DocStyleNames.Contains(DocLinearStyle.Name) Then
                        DocStyleNames.Add(DocLinearStyle.Name)
                    End If
                    If TemplateLinearStyle.Name = DocLinearStyle.Name Then
                        Try
                            UC.CopyProperties(TemplateLinearStyle, DocLinearStyle)
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Error applying LinearStyle '{0}'", TemplateLinearStyle.Name))
                        End Try
                    End If
                Next
            Next

            MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
            If Len(MissingStyles) > 0 Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Linear styles in Draft but not in Template: {0}", MissingStyles))
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
                For Each DocTableStyle As SolidEdgeFrameworkSupport.TableStyle In DocTableStyles
                    If Not DocStyleNames.Contains(DocTableStyle.Name) Then
                        DocStyleNames.Add(DocTableStyle.Name)
                    End If
                    If TemplateTableStyle.Name = DocTableStyle.Name Then
                        Try
                            UC.CopyProperties(TemplateTableStyle, DocTableStyle)
                            '#### added because CopyProperties didn't work in old SE Release, to be verified if still needed
                            For c = 0 To 6
                                DocTableStyle.LineColor(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineColor(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                                DocTableStyle.LineDashType(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineDashType(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                                DocTableStyle.LineWidth(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants)) = TemplateTableStyle.LineWidth(CType(c, SolidEdgeFrameworkSupport.TableStyleLineTypeConstants))
                            Next
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Error applying TableStyle '{0}'", TemplateTableStyle.Name))
                        End Try
                    End If
                Next
            Next

            MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
            If Len(MissingStyles) > 0 Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Table styles in Draft but not in Template: {0}", MissingStyles))
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
                For Each DocTextCharStyle As SolidEdgeFramework.TextCharStyle In DocTextCharStyles
                    If Not DocStyleNames.Contains(DocTextCharStyle.Name) Then
                        DocStyleNames.Add(DocTextCharStyle.Name)
                    End If
                    If TemplateTextCharStyle.Name = DocTextCharStyle.Name Then
                        Try
                            UC.CopyProperties(TemplateTextCharStyle, DocTextCharStyle)
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Error applying TextCharStyle '{0}'", TemplateTextCharStyle.Name))
                        End Try
                    End If
                Next
            Next

            MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
            If Len(MissingStyles) > 0 Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Text Char styles in Draft but not in Template: {0}", MissingStyles))
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
                For Each DocTextStyle As SolidEdgeFramework.TextStyle In DocTextStyles
                    If Not DocStyleNames.Contains(DocTextStyle.Name) Then
                        DocStyleNames.Add(DocTextStyle.Name)
                    End If
                    If TemplateTextStyle.Name = DocTextStyle.Name Then
                        Try
                            UC.CopyProperties(TemplateTextStyle, DocTextStyle)
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Error applying TextStyle '{0}'", TemplateTextStyle.Name))
                        End Try
                    End If
                Next
            Next

            MissingStyles = DocStyleNotInTemplate(DocStyleNames, TemplateStyleNames)
            If Len(MissingStyles) > 0 Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Text styles in Draft but not in Template: {0}", MissingStyles))
            End If
            DocStyleNames.Clear()
            TemplateStyleNames.Clear()
            MissingStyles = ""
        End If


        SETemplateDoc.Close()
        SEApp.DoIdle()

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

        'Dim IU As New InterfaceUtilities

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

            If Not FileIO.FileSystem.FileExists(Me.DraftTemplate) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select a valid drawing template", Indent))
            End If

            If Not (Me.UpdateBorder Or Me.UpdateStyles) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select Update border, Update styles, or both", Indent))
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


    'Public Overrides Sub ReconcileFormWithProps()
    '    ControlsDict(ControlNames.DraftTemplate.ToString).Text = Me.DraftTemplate
    'End Sub


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
