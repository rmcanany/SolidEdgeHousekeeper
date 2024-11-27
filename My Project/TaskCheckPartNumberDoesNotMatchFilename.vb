Option Strict On
Imports System.IO
'Imports OpenMcdf
'Imports OpenMcdf.Extensions.OLEProperties

Public Class TaskCheckPartNumberDoesNotMatchFilename

    Inherits Task
    Public Property PropertySet As String

    Public Property PropertyName As String

    Private _PropertyFormula As String
    Public Property PropertyFormula As String
        Get
            Return _PropertyFormula
        End Get
        Set(value As String)
            _PropertyFormula = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.PropertyFormula.ToString), TextBox).Text = value

                Dim UC As New UtilsCommon
                Me.PropertySet = UC.PropSetFromFormula(value)
                Me.PropertyName = UC.PropNameFromFormula(value)
            End If
        End Set
    End Property

    'Private _StructuredStorageEdit As Boolean
    'Public Property StructuredStorageEdit As Boolean
    '    Get
    '        Return _StructuredStorageEdit
    '    End Get
    '    Set(value As Boolean)
    '        _StructuredStorageEdit = value
    '        If Me.TaskOptionsTLP IsNot Nothing Then
    '            CType(ControlsDict(ControlNames.StructuredStorageEdit.ToString), CheckBox).Checked = value
    '        End If
    '    End Set
    'End Property




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
        'PropertySet
        'PropertySetLabel
        PropertyFormula
        PropertyFormulaLabel
        'StructuredStorageEdit
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
        Me.PropertyFormula = ""
        'Me.StructuredStorageEdit = False
        ''Me.SolidEdgeRequired = False
        'Me.SolidEdgeRequired = True  ' Default is so checking the box toggles a property update

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

        'ErrorMessage = ProcessInternal(FileName)

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

    'Private Overloads Function ProcessInternal(ByVal FullName As String) As Dictionary(Of Integer, List(Of String))

    '    ' Structured Storage
    '    ' https://github.com/ironfede/openmcdf

    '    ' Convert glob to regex 
    '    ' https://stackoverflow.com/questions/74683013/regex-to-glob-and-vice-versa-conversion
    '    ' https://stackoverflow.com/questions/11276909/how-to-convert-between-a-glob-pattern-and-a-regexp-pattern-in-ruby
    '    ' https://learn.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator

    '    Dim ErrorMessageList As New List(Of String)
    '    Dim ExitStatus As Integer = 0
    '    Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

    '    Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

    '    Dim PropertySetName As String = ""
    '    Dim PropertyName As String = ""
    '    Dim FindString As String = ""
    '    Dim ReplaceString As String = ""
    '    Dim FindSearchType As String = ""
    '    Dim ReplaceSearchType As String = ""

    '    Dim PropertyFound As Boolean = False

    '    Dim Proceed As Boolean = True
    '    Dim s As String

    '    Dim PropertiesToEditDict As New Dictionary(Of String, Dictionary(Of String, String))
    '    Dim PropertiesToEdit As String = ""
    '    Dim RowIndexString As String

    '    Dim UC As New UtilsCommon

    '    PropertiesToEdit = Me.JSONString

    '    If Not PropertiesToEdit = "" Then
    '        PropertiesToEditDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(PropertiesToEdit)
    '    Else
    '        Proceed = False
    '        ExitStatus = 1
    '        ErrorMessageList.Add("No properties provided")
    '    End If

    '    Dim fs As FileStream = Nothing

    '    If Proceed Then
    '        Try
    '            fs = New FileStream(FullName, FileMode.Open, FileAccess.ReadWrite)
    '        Catch ex As Exception
    '            Proceed = False
    '            ExitStatus = 1
    '            ErrorMessageList.Add("Unable to open file")
    '        End Try
    '    End If

    '    Dim cf As CompoundFile = Nothing

    '    If Proceed Then
    '        Dim cfg As CFSConfiguration = CFSConfiguration.SectorRecycle Or CFSConfiguration.EraseFreeSectors
    '        cf = New CompoundFile(fs, CFSUpdateMode.Update, cfg)

    '        Dim dsiStream As CFStream = Nothing
    '        Dim co As OLEPropertiesContainer = Nothing
    '        Dim OLEProp As OLEProperty = Nothing

    '        For Each RowIndexString In PropertiesToEditDict.Keys

    '            ' ####################### Get parameters #######################

    '            PropertyName = PropertiesToEditDict(RowIndexString)("PropertyName")
    '            PropertySetName = PropertiesToEditDict(RowIndexString)("PropertySet")
    '            FindSearchType = PropertiesToEditDict(RowIndexString)("FindSearch")
    '            FindString = PropertiesToEditDict(RowIndexString)("FindString")
    '            ReplaceSearchType = PropertiesToEditDict(RowIndexString)("ReplaceSearch")
    '            ReplaceString = PropertiesToEditDict(RowIndexString)("ReplaceString")

    '            'If Not TemplatePropertyDict.Keys.Contains(PropertyName) Then
    '            '    Proceed = False
    '            '    ExitStatus = 1
    '            '    ErrorMessageList.Add(String.Format("Property '{0}' not found in template dictionary", PropertyName))
    '            'End If

    '            Dim tmpPropertyData As PropertyData = Me.PropertiesData.GetPropertyData(PropertyName)
    '            If tmpPropertyData Is Nothing Then
    '                Proceed = False
    '                ExitStatus = 1
    '                ErrorMessageList.Add(String.Format("Property '{0}' not recognized", PropertyName))
    '            End If

    '            ' ####################### Do formula substitution #######################

    '            If Proceed Then
    '                Try
    '                    'FindString = UC.SubstitutePropertyFormula(Nothing, cf, Nothing, FullName, FindString, ValidFilenameRequired:=False,
    '                    '                                      TemplatePropertyDict)
    '                    FindString = UC.SubstitutePropertyFormula(
    '                        Nothing, cf, Nothing, FullName, FindString, ValidFilenameRequired:=False, Me.PropertiesData)
    '                Catch ex As Exception
    '                    Proceed = False
    '                    ExitStatus = 1
    '                    s = String.Format("Unable to process formula in Find text '{0}' for property '{1}'", FindString, PropertyName)
    '                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
    '                End Try

    '                Try
    '                    'ReplaceString = UC.SubstitutePropertyFormula(Nothing, cf, Nothing, FullName, ReplaceString, ValidFilenameRequired:=False,
    '                    '                                         TemplatePropertyDict, ReplaceSearchType = "EX")
    '                    ReplaceString = UC.SubstitutePropertyFormula(
    '                        Nothing, cf, Nothing, FullName, ReplaceString, ValidFilenameRequired:=False,
    '                        Me.PropertiesData, ReplaceSearchType = "EX")
    '                Catch ex As Exception
    '                    Proceed = False
    '                    ExitStatus = 1
    '                    s = String.Format("Unable to process formula in Replace text '{0}' for property '{1}'", ReplaceString, PropertyName)
    '                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
    '                End Try
    '            End If

    '            'Direct properties editing doesn't support linked files |Rx sintax: " & PropertyName
    '            If Proceed Then
    '                If FindString.StartsWith("[ERROR]") Then
    '                    Proceed = False
    '                    ExitStatus = 1
    '                    s = String.Format("Direct edit doesn't support links in Find text '{0}' for property '{1}'", FindString.Replace("[ERROR]", ""), PropertyName)
    '                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
    '                End If
    '                If ReplaceString.StartsWith("[ERROR]") Then
    '                    Proceed = False
    '                    ExitStatus = 1
    '                    s = String.Format("Direct edit doesn't support links in Replace text '{0}' for property '{1}'", ReplaceString.Replace("[ERROR]", ""), PropertyName)
    '                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
    '                End If
    '            End If


    '            ' ####################### Get the property object #######################

    '            'Dim PropertyNameEnglish = TemplatePropertyDict(PropertyName)("EnglishName")
    '            Dim PropertyNameEnglish = tmpPropertyData.EnglishName

    '            If Proceed Then
    '                OLEProp = UC.GetOLEProp(cf, PropertySetName, PropertyNameEnglish, Me.AutoAddMissingProperty, dsiStream, co)
    '            End If

    '            If IsNothing(OLEProp) Then
    '                Proceed = False
    '                ExitStatus = 1
    '                If PropertyName = PropertyNameEnglish Then
    '                    s = String.Format("Property '{0}' not found or not recognized.", PropertyName)
    '                Else
    '                    s = String.Format("Property '{0}({1})' not found or not recognized.", PropertyName, PropertyNameEnglish)
    '                End If
    '                If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
    '            End If


    '            ' ####################### Delete or do the replacement #######################

    '            If Proceed Then

    '                If FindSearchType = "X" Then
    '                    Try
    '                        '############ delete the property here
    '                        co.UserDefinedProperties.RemoveProperty(OLEProp.PropertyIdentifier)

    '                    Catch ex As Exception
    '                        Proceed = False
    '                        ExitStatus = 1
    '                        If PropertyName = PropertyNameEnglish Then
    '                            s = String.Format("Unable to delete property '{0}'.  This command only works on custom properties.", PropertyName)
    '                        Else
    '                            s = String.Format("Unable to delete property '{0}({1})'.  This command only works on custom properties.", PropertyName, PropertyNameEnglish)
    '                        End If
    '                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
    '                    End Try

    '                Else
    '                    Try

    '                        '####### set the property here
    '                        If FindSearchType = "PT" Then
    '                            'OLEProp.Value = Replace(CType(OLEProp.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)

    '                            Dim PropertyValue = Replace(CType(OLEProp.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)
    '                            UC.SetOLEPropValue(OLEProp, PropertyValue, co, cf, dsiStream)

    '                        Else
    '                            If FindSearchType = "WC" Then
    '                                FindString = UC.GlobToRegex(FindString)
    '                            End If
    '                            If ReplaceSearchType = "PT" Then
    '                                ' ReplaceString = Regex.Escape(ReplaceString)
    '                            End If

    '                            'OLEProp.Value = Regex.Replace(CType(OLEProp.Value, String), FindString, ReplaceString, RegexOptions.IgnoreCase)

    '                            Dim PropertyValue = Regex.Replace(CType(OLEProp.Value, String), FindString, ReplaceString, RegexOptions.IgnoreCase)
    '                            UC.SetOLEPropValue(OLEProp, PropertyValue, co, cf, dsiStream)

    '                        End If

    '                    Catch ex As Exception
    '                        Proceed = False
    '                        ExitStatus = 1
    '                        If PropertyName = PropertyNameEnglish Then
    '                            s = String.Format("Unable to replace property value '{0}'.  This command only works on text type properties.", PropertyName)
    '                        Else
    '                            s = String.Format("Unable to replace property value '{0}({1})'.  This command only works on text type properties.", PropertyName, PropertyNameEnglish)
    '                        End If
    '                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
    '                    End Try


    '                End If


    '            End If


    '            ' ####################### Save the properties #######################

    '            If Proceed Then
    '                Try

    '                    '############ save the properties here (!)
    '                    co.Save(dsiStream)

    '                Catch ex As Exception
    '                    Proceed = False
    '                    ExitStatus = 1
    '                    s = "Problem accessing or saving Property."
    '                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
    '                End Try
    '            End If

    '        Next

    '    End If

    '    ' ###### In case of error, don't save anything. ######
    '    If ExitStatus = 0 Then
    '        '############ save the properties here (!)
    '        cf.Commit()
    '    Else
    '        s = "Errors encountered.  No changes made."
    '        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
    '    End If

    '    If cf IsNot Nothing Then
    '        cf.Close()
    '    End If

    '    If fs IsNot Nothing Then
    '        fs.Close()
    '        System.Windows.Forms.Application.DoEvents()
    '    End If

    '    ErrorMessage(ExitStatus) = ErrorMessageList
    '    Return ErrorMessage

    'End Function



    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        'Dim ComboBox As ComboBox
        'Dim ComboBoxItems As List(Of String) = Split("System Custom", " ").ToList
        Dim TextBox As TextBox
        Dim Label As Label
        Dim ControlWidth As Integer = 225

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        'ComboBox = FormatOptionsComboBox(ControlNames.PropertySet.ToString, ComboBoxItems, "DropDownList")
        'ComboBox.Width = ControlWidth
        'AddHandler ComboBox.SelectedIndexChanged, AddressOf ComboBoxOptions_SelectedIndexChanged
        'tmpTLPOptions.Controls.Add(ComboBox, 0, RowIndex)
        'ControlsDict(ComboBox.Name) = ComboBox

        'Label = FormatOptionsLabel(ControlNames.PropertySetLabel.ToString, "Part number prop set")
        'tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        'ControlsDict(Label.Name) = Label

        'RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.PropertyFormula.ToString, "")
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        TextBox.Width = ControlWidth
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        Label = FormatOptionsLabel(ControlNames.PropertyFormulaLabel.ToString, "Part number prop")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

        'RowIndex += 1

        'CheckBox = FormatOptionsCheckBox(ControlNames.StructuredStorageEdit.ToString, "Run outside of Solid Edge")
        'AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        'tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        'tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        'ControlsDict(CheckBox.Name) = CheckBox

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
                ErrorMessageList.Add(String.Format("{0}Select the property that contains the part number", Indent))
            End If

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    'Public Sub ComboBoxOptions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
    '    Dim ComboBox = CType(sender, ComboBox)
    '    Dim Name = ComboBox.Name

    '    Select Case Name
    '        Case ControlNames.PropertySet.ToString
    '            Me.PropertySet = ComboBox.Text
    '        Case Else
    '            MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
    '    End Select

    'End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name
            'Case ControlNames.StructuredStorageEdit.ToString
            '    Me.StructuredStorageEdit = Checkbox.Checked
            '    Me.RequiresSave = Not Checkbox.Checked
            '    Me.SolidEdgeRequired = Not Checkbox.Checked

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

        Dim UC As New UtilsCommon

        Select Case Name
            Case ControlNames.PropertyFormula.ToString
                Me.PropertyFormula = TextBox.Text
            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Checks if the file name contains the part number. "

        HelpString += vbCrLf + vbCrLf + "![CheckPartNumberDoesNotMatchFilename](My%20Project/media/task_check_part_number_does_not_match_filename.png)"

        HelpString += vbCrLf + vbCrLf + "Enter the property formula that holds part number on the Options panel. "
        'HelpString += "A `Property set`, either `System` or `Custom`, is required. "
        HelpString += "For more information, see the "
        HelpString += "[<ins>**Property Filter**</ins>](#1-property-filter) section in this README file. "

        HelpString += vbCrLf + vbCrLf + "The command only checks that the part number appears somewhere in the file name. "
        HelpString += "If the part number is, say, `7481-12104` and the file name is `7481-12104 Motor Mount.par`, "
        HelpString += "you will get a match. "

        Return HelpString
    End Function



End Class
