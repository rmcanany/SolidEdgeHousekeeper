Option Strict On

Public Class TaskCheckFilename

    Inherits Task

    Private _PropertyFormula As String
    Public Property PropertyFormula As String
        Get
            Return _PropertyFormula
        End Get
        Set(value As String)
            _PropertyFormula = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.PropertyFormula.ToString), TextBox).Text = value

                'Dim UC As New UtilsCommon
                'Me.PropertySet = UC.PropSetFromFormula(value)
                'Me.PropertyName = UC.PropNameFromFormula(value)

                'If Me.PropertiesData IsNot Nothing Then
                '    Dim PropertyData = Me.PropertiesData.GetPropertyData(Me.PropertyName)
                '    If PropertyData IsNot Nothing Then
                '        Me.PropertyNameEnglish = PropertyData.EnglishName
                '    Else
                '        Me.PropertyNameEnglish = ""
                '    End If

                'End If
            End If
        End Set
    End Property

    Private _ComparisonContains As Boolean
    Public Property ComparisonContains As Boolean
        Get
            Return _ComparisonContains
        End Get
        Set(value As Boolean)
            _ComparisonContains = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ComparisonContains.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _ComparisonIsExactly As Boolean
    Public Property ComparisonIsExactly As Boolean
        Get
            Return _ComparisonIsExactly
        End Get
        Set(value As Boolean)
            _ComparisonIsExactly = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ComparisonIsExactly.ToString), CheckBox).Checked = value
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

    Enum ControlNames
        PropertyFormula
        PropertyFormulaLabel
        ComparisonContains
        ComparisonIsExactly
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
        Me.Image = My.Resources.TaskCheckFilename
        Me.Category = "Check"
        Me.RequiresPropertiesData = True
        SetColorFromCategory(Me)
        'Me.SolidEdgeRequired = False
        Me.SolidEdgeRequired = True  ' Default is so checking the box toggles a property update
        Me.RequiresLinkManagementOrder = True

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        'Me.PropertySet = ""
        'Me.PropertyName = ""
        Me.PropertyFormula = ""
        Me.ComparisonContains = False
        Me.ComparisonIsExactly = False
        Me.DraftsCheckModels = False
        Me.DraftsCheckDraftItself = False
        Me.StructuredStorageEdit = False

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

        Dim Formula As String
        Dim FormulaFound As Boolean
        Dim Filename As String

        Dim UC As New UtilsCommon

        Filename = UC.SplitFOAName(SEDoc.FullName)("Filename")

        Filename = System.IO.Path.GetFileNameWithoutExtension(Filename)  'c:\project\part.par' -> 'part'

        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case = "asm", "par", "psm"

                Formula = UC.SubstitutePropertyFormula(SEDoc, SEDoc.FullName, Me.PropertyFormula, ValidFilenameRequired:=False, Me.PropertiesData)

                If Formula Is Nothing Then
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("Could not process formula '{0}'", Me.PropertyFormula))
                Else
                    Formula = Formula.Trim
                End If

                If ExitStatus = 0 Then
                    If Formula = "" Then
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Formula '{0}' not assigned", Me.PropertyFormula))
                    End If
                End If

                If ExitStatus = 0 Then
                    If Me.ComparisonContains Then
                        If Not Filename.ToLower.Contains(Formula.ToLower) Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("File name '{0}' does not contain property formula '{1}'", Filename, Formula))
                        End If
                    ElseIf Me.ComparisonIsExactly Then
                        If Not Filename.ToLower = Formula.ToLower Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("File name '{0}' not the same as property formula '{1}'", Filename, Formula))
                        End If
                    End If
                End If

            Case = "dft"

                If Me.DraftsCheckDraftItself Then
                    Formula = UC.SubstitutePropertyFormula(SEDoc, SEDoc.FullName, Me.PropertyFormula, ValidFilenameRequired:=False, Me.PropertiesData)

                    If Formula Is Nothing Then
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Could not process formula '{0}'", Me.PropertyFormula))
                    End If

                    If ExitStatus = 0 Then
                        If Formula = "" Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Formula '{0}' not assigned", Me.PropertyFormula))
                        End If
                    End If

                    If ExitStatus = 0 Then
                        If Me.ComparisonContains Then
                            If Not Filename.ToLower.Contains(Formula.ToLower) Then
                                ExitStatus = 1
                                ErrorMessageList.Add(String.Format("File name '{0}' does not contain property formula '{1}'", Filename, Formula))
                            End If
                        ElseIf Me.ComparisonIsExactly Then
                            If Not Filename.ToLower = Formula.ToLower Then
                                ExitStatus = 1
                                ErrorMessageList.Add(String.Format("File name '{0}' not the same as the property formula '{1}'", Filename, Formula))
                            End If
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
                    Dim Formulas As New List(Of String)

                    Dim ValidExtensionsList As List(Of String) = ".asm .par .psm".Split(CChar(" ")).ToList
                    Dim Extension As String

                    ModelLinks = tmpSEDoc.ModelLinks

                    FormulaFound = False

                    For Each ModelLink In ModelLinks
                        ModelLinkFilename = UC.SplitFOAName(ModelLink.FileName)("Filename")
                        Extension = IO.Path.GetExtension(ModelLinkFilename)

                        If (IO.File.Exists(ModelLinkFilename)) And (ValidExtensionsList.Contains(Extension)) Then

                            ModelLinkFilenames.Add(System.IO.Path.GetFileName(ModelLinkFilename))
                            ModelLinkDoc = CType(ModelLink.ModelDocument, SolidEdgeFramework.SolidEdgeDocument)

                            Formula = UC.SubstitutePropertyFormula(
                                    ModelLinkDoc, ModelLinkDoc.FullName, Me.PropertyFormula, ValidFilenameRequired:=False, Me.PropertiesData)

                            If Formula IsNot Nothing Then
                                Formulas.Add(Formula)
                            Else
                                Formulas.Add("")
                            End If

                            If Formula IsNot Nothing AndAlso Not Formula = "" Then
                                If Me.ComparisonContains Then
                                    If Filename.ToLower.Contains(Formula.ToLower) Then
                                        FormulaFound = True
                                        Exit For
                                    End If
                                ElseIf Me.ComparisonIsExactly Then
                                    If Filename.ToLower = Formula.ToLower Then
                                        FormulaFound = True
                                        Exit For
                                    End If
                                End If
                            End If
                        End If
                    Next

                    If (Not FormulaFound) And (ModelLinkFilenames.Count > 0) Then
                        ExitStatus = 1
                        If Me.ComparisonContains Then
                            If ModelLinkFilenames.Count = 1 Then
                                ErrorMessageList.Add(String.Format("File name '{0}' does not contain property in this model file", Filename))
                            Else
                                ErrorMessageList.Add(String.Format("File name '{0}' does not contain property in these model files", Filename))
                            End If
                        ElseIf Me.ComparisonIsExactly Then
                            If ModelLinkFilenames.Count = 1 Then
                                ErrorMessageList.Add(String.Format("File name '{0}' not the same as the property in this model file", Filename))
                            Else
                                ErrorMessageList.Add(String.Format("File name '{0}' not the same as the property in these model files", Filename))
                            End If
                        End If
                        For i As Integer = 0 To ModelLinkFilenames.Count - 1
                            ErrorMessageList.Add(String.Format("    Model file: '{0}', property value: '{1}'", ModelLinkFilenames(i), Formulas(i)))
                        Next
                    End If

                End If

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        End Select

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Private Overloads Function ProcessInternal(ByVal FullName As String) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Proceed As Boolean = True
        Dim Formula As String = ""
        Dim FormulaFound As Boolean

        Dim Filename As String = IO.Path.GetFileNameWithoutExtension(FullName)  'c:\project\part.par' -> 'part'

        Dim ValidExtensionsList As List(Of String) = ".asm .par .psm".Split(CChar(" ")).ToList
        Dim ExtensionParent As String = IO.Path.GetExtension(FullName)

        Dim ChildNames As New List(Of String)
        Dim ChildName As String = ""
        Dim Formulas As New List(Of String)

        Dim UC As New UtilsCommon

        Dim SSParentDoc As HCStructuredStorageDoc = Nothing

        Try
            SSParentDoc = New HCStructuredStorageDoc(FullName)
            SSParentDoc.ReadProperties(Me.PropertiesData)
            SSParentDoc.ReadLinks(Me.LinkManagementOrder)

        Catch ex As Exception
            Proceed = False
            ExitStatus = 1
            ErrorMessageList.Add(ex.Message)
        End Try

        If Proceed Then

            Select Case ExtensionParent
                Case ".asm", ".par", ".psm"

                    Formula = SSParentDoc.SubstitutePropertyFormulas(Me.PropertyFormula, ValidFilenameRequired:=False)

                    If Formula Is Nothing Then
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Could not process formula '{0}'", Me.PropertyFormula))
                    Else
                        Formula = Formula.Trim
                    End If

                    If ExitStatus = 0 Then
                        If Formula = "" Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Formula '{0}' not assigned", Me.PropertyFormula))
                        End If
                    End If

                    If ExitStatus = 0 Then
                        If Me.ComparisonContains Then
                            If Not Filename.ToLower.Contains(Formula.ToLower) Then
                                ExitStatus = 1
                                ErrorMessageList.Add(String.Format("File name '{0}' does not contain property formula '{1}'", Filename, Formula))
                            End If
                        ElseIf Me.ComparisonIsExactly Then
                            If Not Filename.ToLower = Formula.ToLower Then
                                ExitStatus = 1
                                ErrorMessageList.Add(String.Format("File name '{0}' not the same as property formula '{1}'", Filename, Formula))
                            End If
                        End If
                    End If


                Case ".dft"

                    If Me.DraftsCheckDraftItself Then

                        Formula = SSParentDoc.SubstitutePropertyFormulas(Me.PropertyFormula, ValidFilenameRequired:=False)

                        If Formula Is Nothing Then
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Could not process formula '{0}'", Me.PropertyFormula))
                        Else
                            Formula = Formula.Trim
                        End If

                        If ExitStatus = 0 Then
                            If Formula = "" Then
                                ExitStatus = 1
                                ErrorMessageList.Add(String.Format("Formula '{0}' not assigned", Me.PropertyFormula))
                            End If
                        End If

                        If ExitStatus = 0 Then
                            If Me.ComparisonContains Then
                                If Not Filename.ToLower.Contains(Formula.ToLower) Then
                                    ExitStatus = 1
                                    ErrorMessageList.Add(String.Format("File name '{0}' does not contain property formula '{1}'", Filename, Formula))
                                End If
                            ElseIf Me.ComparisonIsExactly Then
                                If Not Filename.ToLower = Formula.ToLower Then
                                    ExitStatus = 1
                                    ErrorMessageList.Add(String.Format("File name '{0}' not the same as property formula '{1}'", Filename, Formula))
                                End If
                            End If
                        End If

                    End If

                    If Me.DraftsCheckModels Then

                        ChildNames = SSParentDoc.GetLinkNames

                        If (ChildNames IsNot Nothing) AndAlso (ChildNames.Count > 0) Then

                            For Each ChildName In ChildNames

                                If Not ValidExtensionsList.Contains(IO.Path.GetExtension(ChildName)) Then
                                    Continue For
                                End If

                                Dim SSChildDoc As HCStructuredStorageDoc = Nothing

                                Try
                                    SSChildDoc = New HCStructuredStorageDoc(ChildName)
                                    SSChildDoc.ReadProperties(Me.PropertiesData)

                                    'Formula = CStr(SSChildDoc.GetPropValue(Me.PropertySet, Me.PropertyNameEnglish))
                                    Formula = SSChildDoc.SubstitutePropertyFormulas(Me.PropertyFormula, ValidFilenameRequired:=False)

                                    If Formula IsNot Nothing Then  ' Nothing is not an error, but no match possible.
                                        Formula = Formula.Trim
                                        Formulas.Add(Formula)
                                        If Me.ComparisonContains Then
                                            If Filename.ToLower.Contains(Formula.ToLower) Then
                                                FormulaFound = True
                                                If SSChildDoc IsNot Nothing Then SSChildDoc.Close()
                                                Exit For
                                            End If
                                        ElseIf Me.ComparisonIsExactly Then
                                            If Filename.ToLower = Formula.ToLower Then
                                                FormulaFound = True
                                                If SSChildDoc IsNot Nothing Then SSChildDoc.Close()
                                                Exit For
                                            End If
                                        End If
                                    Else
                                        Formulas.Add("")
                                    End If

                                Catch ex As Exception
                                    Formulas.Add("")
                                    If ex.Message.Contains("FOA") Then
                                        ExitStatus = 1
                                        ErrorMessageList.Add(String.Format("FOA file '{0}' not processed", IO.Path.GetFileName(ChildName)))
                                    End If
                                End Try

                                If SSChildDoc IsNot Nothing Then SSChildDoc.Close()

                            Next

                            If (Not FormulaFound) And (ChildNames.Count > 0) Then
                                ExitStatus = 1
                                If Me.ComparisonContains Then
                                    If ChildNames.Count = 1 Then
                                        ErrorMessageList.Add(String.Format("File name '{0}' does not contain property in this model file", Filename))
                                    Else
                                        ErrorMessageList.Add(String.Format("File name '{0}' does not contain property in these model files", Filename))
                                    End If
                                ElseIf Me.ComparisonIsExactly Then
                                    If ChildNames.Count = 1 Then
                                        ErrorMessageList.Add(String.Format("File name '{0}' not the same as the property in this model file", Filename))
                                    Else
                                        ErrorMessageList.Add(String.Format("File name '{0}' not the same as the property in these model files", Filename))
                                    End If
                                End If
                                For i As Integer = 0 To ChildNames.Count - 1
                                    ErrorMessageList.Add(String.Format("    Model file: '{0}', property value: '{1}'", IO.Path.GetFileName(ChildNames(i)), Formulas(i)))
                                Next
                            End If
                        End If
                    End If

                Case Else
                    MsgBox(String.Format("{0} Extension '{1}' not recognized", Me.Name, ExtensionParent))

            End Select

        End If

        If SSParentDoc IsNot Nothing Then SSParentDoc.Close()

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

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 2)

        RowIndex = 0

        Label = FormatOptionsLabel(ControlNames.PropertyFormulaLabel.ToString, "Property formula")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.PropertyFormula.ToString, "")
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        'TextBox.Width = ControlWidth
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(TextBox, 2)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ComparisonContains.ToString, "Comparison contains")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ComparisonIsExactly.ToString, "Comparison is exactly")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DraftsCheckModels.ToString, "Drafts -- Check model files for formula")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.DraftsCheckDraftItself.ToString, "Drafts -- Check draft itself for formula")
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

            If Me.PropertyFormula = "" Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Enter a property formula", Indent))
            End If

            If (Not Me.ComparisonContains) And (Not Me.ComparisonIsExactly) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select a comparison option", Indent))
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

            Case ControlNames.ComparisonContains.ToString
                Me.ComparisonContains = Checkbox.Checked
                Me.ComparisonIsExactly = Not Checkbox.Checked

            Case ControlNames.ComparisonIsExactly.ToString
                Me.ComparisonIsExactly = Checkbox.Checked
                Me.ComparisonContains = Not Checkbox.Checked

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
        HelpString = "Checks the file name for the presence of a property (or combination of properties). "

        HelpString += vbCrLf + vbCrLf + "![CheckFilename](My%20Project/media/task_check_filename.png)"

        HelpString += vbCrLf + vbCrLf + "Enter the property formula on the Options panel. "
        HelpString += "For example `%{System.Document Number}`, `%{Custom.PartNumber}-%{Custom.RevisionLetter}`, etc.  "
        HelpString += "(The example above is set to check if a `*.dft` has the same name as the model it depicts.)  "
        HelpString += "For more information on properties, see the "
        HelpString += "[<ins>**Property Filter**</ins>](#property-filter) section in this README file. "

        HelpString += vbCrLf + vbCrLf + "There are two comparison methods. "
        HelpString += vbCrLf + "- `Comparison contains` checks if the property appears anywhere in the file name.  "
        HelpString += "If the property value is, say, `7481-12104` and the file name is `7481-12104 Motor Mount.par`, "
        HelpString += "you would get a match. "
        HelpString += vbCrLf + "- `Comparison is exactly` looks for an exact match.  "
        HelpString += "With the previous example, the program would report an error. "

        HelpString += vbCrLf + vbCrLf + "For *.dft files, there are two ways to search.  You can use either one, or both. "
        HelpString += vbCrLf + "- `Check model files` is the option most users will want.  "
        HelpString += "It searches any models linked to the file for a property match.  "
        HelpString += vbCrLf + "- `Check draft itself` searches properties in the draft file.  "
        HelpString += "Since draft files rarely have properties of their own, this is usually not necessary. "
        HelpString += "Also, because missing properties are reported as an error, "
        HelpString += "it can be confusing as well. "

        Return HelpString
    End Function



End Class
