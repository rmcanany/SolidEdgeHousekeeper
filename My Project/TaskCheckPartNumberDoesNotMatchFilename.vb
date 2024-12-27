Option Strict On
Imports System.IO
Imports OpenMcdf
Imports OpenMcdf.Extensions.OLEProperties
'Imports OpenMcdf
'Imports OpenMcdf.Extensions.OLEProperties

Public Class TaskCheckPartNumberDoesNotMatchFilename

    Inherits Task
    Public Property PropertySet As String

    Public Property PropertyName As String

    Public Property PropertyNameEnglish As String

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

                If Me.PropertiesData IsNot Nothing Then
                    Dim PropertyData = Me.PropertiesData.GetPropertyData(Me.PropertyName)
                    If PropertyData IsNot Nothing Then
                        Me.PropertyNameEnglish = PropertyData.EnglishName
                    Else
                        Me.PropertyNameEnglish = ""
                    End If

                End If
            End If
        End Set
    End Property

    Private _DraftsCheckModels As Boolean
    Public Property DraftsCheckModels As Boolean
        Get
            Return _DraftsCheckModels
        End Get
        Set(value As Boolean)
            _DraftsCheckModels = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DraftsCheckModels.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _DraftsCheckDraftItself As Boolean
    Public Property DraftsCheckDraftItself As Boolean
        Get
            Return _DraftsCheckDraftItself
        End Get
        Set(value As Boolean)
            _DraftsCheckDraftItself = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DraftsCheckDraftItself.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _StructuredStorageEdit As Boolean
    Public Property StructuredStorageEdit As Boolean
        Get
            Return _StructuredStorageEdit
        End Get
        Set(value As Boolean)
            _StructuredStorageEdit = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.StructuredStorageEdit.ToString), CheckBox).Checked = value
                Me.SolidEdgeRequired = Not value
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

    Public Property PropertiesData As PropertiesData

    Enum ControlNames
        'PropertySet
        'PropertySetLabel
        PropertyFormula
        PropertyFormulaLabel
        DraftsCheckModels
        DraftsCheckDraftItself
        StructuredStorageEdit
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
        Me.RequiresPropertiesData = True
        SetColorFromCategory(Me)
        Me.SolidEdgeRequired = False
        Me.RequiresLinkManagementOrder = True

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.PropertySet = ""
        Me.PropertyName = ""
        Me.PropertyFormula = ""
        Me.DraftsCheckModels = False
        Me.DraftsCheckDraftItself = False
        Me.StructuredStorageEdit = False

        Me.PropertiesData = New PropertiesData

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

        ErrorMessage = ProcessInternal(FileName)

        Return ErrorMessage

    End Function

    Private Overloads Function ProcessInternal(
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

                    If Me.DraftsCheckDraftItself Then
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
                    End If

                    If Me.DraftsCheckModels Then
                        Dim tmpSEDoc As SolidEdgeDraft.DraftDocument = CType(SEDoc, SolidEdgeDraft.DraftDocument)

                        Dim ModelLinks As SolidEdgeDraft.ModelLinks = Nothing
                        Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing
                        Dim ModelLinkFilenames As New List(Of String)
                        Dim ModelLinkFilename As String
                        Dim ModelLinkDoc As SolidEdgeFramework.SolidEdgeDocument

                        Dim ValidExtensionsList As List(Of String) = ".asm .par .psm".Split(CChar(" ")).ToList
                        Dim Extension As String

                        ModelLinks = tmpSEDoc.ModelLinks

                        PartNumberFound = False

                        For Each ModelLink In ModelLinks
                            ModelLinkFilename = UC.SplitFOAName(ModelLink.FileName)("Filename")
                            Extension = IO.Path.GetExtension(ModelLinkFilename)

                            If (IO.File.Exists(ModelLinkFilename)) And (ValidExtensionsList.Contains(Extension)) Then

                                ModelLinkFilenames.Add(System.IO.Path.GetFileName(ModelLinkFilename))
                                ModelLinkDoc = CType(ModelLink.ModelDocument, SolidEdgeFramework.SolidEdgeDocument)

                                Prop = UC.GetProp(ModelLinkDoc, Me.PropertySet, Me.PropertyName, 0, False)
                                If Prop IsNot Nothing Then
                                    PartNumber = CStr(Prop.Value).Trim
                                    If Not PartNumber = "" Then
                                        If Filename.ToLower.Contains(PartNumber.ToLower) Then
                                            PartNumberFound = True
                                            Exit For
                                        End If
                                    End If

                                End If
                            End If
                        Next

                        If (Not PartNumberFound) And (ModelLinkFilenames.Count > 0) Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Part number in the following models not found in filename '{0}'", Filename))
                            For Each ModelLinkFilename In ModelLinkFilenames
                                ErrorMessageList.Add(String.Format("    {0}", ModelLinkFilename))
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

    Private Overloads Function ProcessInternal(ByVal FullName As String) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Proceed As Boolean = True
        Dim PartNumber As String = ""
        Dim PartNumberFound As Boolean

        Dim Filename As String = IO.Path.GetFileName(FullName)

        Dim ValidExtensionsList As List(Of String) = ".asm .par .psm".Split(CChar(" ")).ToList
        Dim ExtensionParent As String = IO.Path.GetExtension(FullName)

        Dim ChildNames As New List(Of String)
        Dim ChildName As String = ""

        Dim UC As New UtilsCommon

        Dim fsParent As FileStream = Nothing
        Dim cfParent As CompoundFile = Nothing
        Dim fsChild As FileStream = Nothing
        Dim cfChild As CompoundFile = Nothing
        Dim dsiStream As CFStream = Nothing
        Dim co As OLEPropertiesContainer = Nothing
        Dim OLEProp As OLEProperty = Nothing


        '
        'Dim TestStructuredStorageClass As Boolean = True
        'If TestStructuredStorageClass Then
        '    Proceed = False

        '    Dim TestSS As Boolean = True

        '    If TestSS Then
        '        Dim SMDoc As HelperStructuredStorageDocument = Nothing
        '        Try
        '            SMDoc = New HelperStructuredStorageDocument(FullName, NeedProperties:=True, NeedLinks:=True, Me.LinkManagementOrder)
        '            SMDoc.Close()

        '        Catch ex As Exception
        '            If SMDoc IsNot Nothing Then
        '                SMDoc.Close()
        '            End If
        '        End Try

        '    End If

        'End If


        If Proceed Then
            Try
                fsParent = New FileStream(FullName, FileMode.Open, FileAccess.ReadWrite)
            Catch ex As Exception
                Proceed = False
                ExitStatus = 1
                ErrorMessageList.Add("Unable to open file")
            End Try
        End If


        If Proceed Then
            Dim cfg As CFSConfiguration = CFSConfiguration.SectorRecycle Or CFSConfiguration.EraseFreeSectors
            cfParent = New CompoundFile(fsParent, CFSUpdateMode.Update, cfg)

        End If

        If Proceed Then

            Select Case ExtensionParent
                Case ".asm", ".par", ".psm"

                    PartNumber = UC.GetOLEPropValue(cfParent, Me.PropertySet, Me.PropertyNameEnglish, AddProp:=False)

                    If PartNumber Is Nothing Then
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Property name: '{0}' not found in property set: '{1}'",
                                         Me.PropertyName, Me.PropertySet))
                    End If

                    If ExitStatus = 0 Then
                        PartNumber = PartNumber.Trim
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


                Case ".dft"

                    If Me.DraftsCheckDraftItself Then

                        PartNumber = UC.GetOLEPropValue(cfParent, Me.PropertySet, Me.PropertyNameEnglish, AddProp:=False)

                        If PartNumber Is Nothing Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Property name: '{0}' not found in property set: '{1}'",
                                         Me.PropertyName, Me.PropertySet))
                        End If

                        If ExitStatus = 0 Then
                            PartNumber = PartNumber.Trim
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

                    End If

                    If Me.DraftsCheckModels Then

                        Dim LinkList = UC.GetOleLinks(cfParent, FullName)  ' Format defined in FindOleLinks()

                        If LinkList.Count > 0 Then

                            For Each LinkDict As Dictionary(Of String, String) In LinkList

                                If LinkDict("ExitStatus") = "1" Then
                                    ExitStatus = 1
                                    Dim EML = LinkDict("ErrorMessage").Split(CChar("."))
                                    For Each s As String In EML
                                        ErrorMessageList.Add(s)
                                    Next

                                End If

                                Proceed = True

                                ChildName = ""

                                For Each Order As String In Me.LinkManagementOrder
                                    Select Case Order
                                        Case "CONTAINER"
                                            If IO.File.Exists(LinkDict("CONTAINER")) Then
                                                ChildName = LinkDict("CONTAINER")
                                                Exit For
                                            End If
                                        Case "RELATIVE"
                                            If IO.File.Exists(LinkDict("RELATIVE")) Then
                                                ChildName = LinkDict("RELATIVE")
                                                Exit For
                                            End If
                                        Case "ABSOLUTE"
                                            If IO.File.Exists(LinkDict("ABSOLUTE")) Then
                                                ChildName = LinkDict("ABSOLUTE")
                                                Exit For
                                            End If
                                    End Select
                                Next

                                If ChildName = "" Then
                                    Continue For
                                End If

                                ChildNames.Add(ChildName)

                                Try
                                    fsChild = New FileStream(ChildName, FileMode.Open, FileAccess.ReadWrite)
                                Catch ex As Exception
                                    Proceed = False
                                    'ExitStatus = 1
                                    'ErrorMessageList.Add(String.Format("Unable to open model '{0}'", ChildName))
                                End Try

                                If Proceed Then
                                    Dim cfg As CFSConfiguration = CFSConfiguration.SectorRecycle Or CFSConfiguration.EraseFreeSectors
                                    cfChild = New CompoundFile(fsChild, CFSUpdateMode.Update, cfg)
                                End If

                                Proceed = Proceed And IO.File.Exists(ChildName)
                                Proceed = Proceed And ValidExtensionsList.Contains(IO.Path.GetExtension(ChildName))

                                If Proceed Then
                                    PartNumber = UC.GetOLEPropValue(cfChild, Me.PropertySet, Me.PropertyNameEnglish, AddProp:=False)
                                    If PartNumber IsNot Nothing Then  ' Nothing is not an error, but no match possible.
                                        If Filename.ToLower.Contains(PartNumber.ToLower) Then
                                            PartNumberFound = True
                                            Exit For
                                        End If
                                    End If

                                End If

                                If cfChild IsNot Nothing Then
                                    cfChild.Close()
                                End If
                                If fsChild IsNot Nothing Then
                                    fsChild.Close()
                                End If

                            Next

                            If (Not PartNumberFound) And (ChildNames.Count > 0) Then
                                ExitStatus = 1
                                ErrorMessageList.Add(String.Format("Part number in the following models not found in filename '{0}'", Filename))
                                For Each ChildName In ChildNames
                                    ErrorMessageList.Add(String.Format("    {0}", ChildName))
                                Next
                            End If

                        End If

                    End If

                Case Else
                    MsgBox(String.Format("{0} Extension '{1}' not recognized", Me.Name, ExtensionParent))

            End Select

        End If

        If cfChild IsNot Nothing Then
            cfChild.Close()
        End If
        If fsChild IsNot Nothing Then
            fsChild.Close()
        End If

        If cfParent IsNot Nothing Then
            cfParent.Close()
        End If
        If fsParent IsNot Nothing Then
            fsParent.Close()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function



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

        Label = FormatOptionsLabel(ControlNames.PropertyFormulaLabel.ToString, "Part number property")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.PropertyFormula.ToString, "")
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        TextBox.Width = ControlWidth
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DraftsCheckModels.ToString, "Drafts -- Check model files for part number")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DraftsCheckDraftItself.ToString, "Drafts -- Check draft itself for part number")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.StructuredStorageEdit.ToString, "Run task without Solid Edge")
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

            If (Not Me.DraftsCheckModels) And (Not Me.DraftsCheckDraftItself) And (Me.IsSelectedDraft) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one Draft process option", Indent))
            End If


            Me.LinkManagementOrder = Form_Main.LinkManagementOrder

            If (Me.StructuredStorageEdit) And (Me.DraftsCheckModels) And (Me.LinkManagementOrder Is Nothing) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Populate LinkMgmt.txt file name on the Configuration Tab -- Templates page", Indent))
            End If

            If (Me.StructuredStorageEdit) And (Me.DraftsCheckModels) And (Me.LinkManagementOrder IsNot Nothing) Then
                If Me.LinkManagementOrder.Count = 0 Then
                    If Not ErrorMessageList.Contains(Me.Description) Then
                        ErrorMessageList.Add(Me.Description)
                    End If
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("{0}LinkMgmt.txt file does not contain any search order information", Indent))

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

            Case ControlNames.DraftsCheckModels.ToString
                Me.DraftsCheckModels = Checkbox.Checked

            Case ControlNames.DraftsCheckDraftItself.ToString
                Me.DraftsCheckDraftItself = Checkbox.Checked

            Case ControlNames.StructuredStorageEdit.ToString
                Me.StructuredStorageEdit = Checkbox.Checked
                Me.RequiresSave = Not Checkbox.Checked
                Me.SolidEdgeRequired = Not Checkbox.Checked

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
                Me.PropertyFormula = TextBox.Text  ' PropertySet and PropertyName extracted in PropertyFormula.Set()
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
