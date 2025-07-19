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
        Me.SolidEdgeRequired = True  ' Default is so checking the box toggles a property update
        Me.RequiresLinkManagementOrder = True

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.PropertyFormula = ""
        Me.ComparisonContains = False
        Me.ComparisonIsExactly = False
        Me.DraftsCheckModels = False
        Me.DraftsCheckDraftItself = False
        Me.StructuredStorageEdit = False

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

        ProcessInternal(FileName)
    End Sub

    Private Overloads Sub ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        Dim Formula As String
        Dim FormulaFound As Boolean
        Dim Filename As String

        Dim UC As New UtilsCommon

        Filename = UC.GetFOAFilename(SEDoc.FullName)

        Filename = System.IO.Path.GetFileNameWithoutExtension(Filename)  'c:\project\part.par' -> 'part'

        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case = "asm", "par", "psm"

                Formula = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, Me.PropertyFormula, Me.PropertiesData, TaskLogger)

                If Formula Is Nothing Then
                    TaskLogger.AddMessage(String.Format("Could not process formula '{0}'", Me.PropertyFormula))

                Else
                    Formula = Formula.Trim
                End If

                If Not TaskLogger.HasErrors Then
                    If Formula = "" Then
                        TaskLogger.AddMessage(String.Format("Formula '{0}' not assigned", Me.PropertyFormula))
                    End If
                End If

                If Not TaskLogger.HasErrors Then
                    If Me.ComparisonContains Then
                        If Not Filename.ToLower.Contains(Formula.ToLower) Then
                            TaskLogger.AddMessage(String.Format("File name '{0}' does not contain property formula '{1}'", Filename, Formula))
                        End If
                    ElseIf Me.ComparisonIsExactly Then
                        If Not Filename.ToLower = Formula.ToLower Then
                            TaskLogger.AddMessage(String.Format("File name '{0}' not the same as property formula '{1}'", Filename, Formula))
                        End If
                    End If
                End If

            Case = "dft"

                If Me.DraftsCheckDraftItself Then
                    Formula = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, Me.PropertyFormula, Me.PropertiesData, TaskLogger)

                    If Formula Is Nothing Then
                        TaskLogger.AddMessage(String.Format("Could not process formula '{0}'", Me.PropertyFormula))

                    End If

                    If Not TaskLogger.HasErrors Then
                        If Formula = "" Then
                            TaskLogger.AddMessage(String.Format("Formula '{0}' not assigned", Me.PropertyFormula))
                        End If
                    End If

                    If Not TaskLogger.HasErrors Then
                        If Me.ComparisonContains Then
                            If Not Filename.ToLower.Contains(Formula.ToLower) Then
                                TaskLogger.AddMessage(String.Format("File name '{0}' does not contain formula value '{1}'", Filename, Formula))
                            End If
                        ElseIf Me.ComparisonIsExactly Then
                            If Not Filename.ToLower = Formula.ToLower Then
                                TaskLogger.AddMessage(String.Format("File name '{0}' not the same as the formula value '{1}'", Filename, Formula))
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
                        ModelLinkFilename = UC.GetFOAFilename(ModelLink.FileName)
                        Extension = IO.Path.GetExtension(ModelLinkFilename)

                        If (IO.File.Exists(ModelLinkFilename)) And (ValidExtensionsList.Contains(Extension)) Then

                            ModelLinkFilenames.Add(System.IO.Path.GetFileName(ModelLinkFilename))
                            ModelLinkDoc = CType(ModelLink.ModelDocument, SolidEdgeFramework.SolidEdgeDocument)

                            Formula = UC.SubstitutePropertyFormulas(
                                    ModelLinkDoc, ModelLinkDoc.FullName, Me.PropertyFormula, Me.PropertiesData, TaskLogger)

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
                        If Me.ComparisonContains Then
                            If ModelLinkFilenames.Count = 1 Then
                                TaskLogger.AddMessage(String.Format("File name '{0}' does not contain property in the following model file", Filename))
                            Else
                                TaskLogger.AddMessage(String.Format("File name '{0}' does not contain property in the following model files", Filename))
                            End If
                        ElseIf Me.ComparisonIsExactly Then
                            If ModelLinkFilenames.Count = 1 Then
                                TaskLogger.AddMessage(String.Format("File name '{0}' not the same as the property in the following model file", Filename))
                            Else
                                TaskLogger.AddMessage(String.Format("File name '{0}' not the same as the property in the following model files", Filename))
                            End If
                        End If
                        For i As Integer = 0 To ModelLinkFilenames.Count - 1
                            TaskLogger.AddMessage(String.Format("    Model file: '{0}', property value: '{1}'", ModelLinkFilenames(i), Formulas(i)))
                        Next
                    End If

                End If

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        End Select

    End Sub

    Private Overloads Sub ProcessInternal(ByVal FullName As String)

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
            TaskLogger.AddMessage(ex.Message)

        End Try

        If Proceed Then

            Select Case ExtensionParent
                Case ".asm", ".par", ".psm"

                    Formula = SSParentDoc.SubstitutePropertyFormulas(Me.PropertyFormula, TaskLogger)

                    If Formula Is Nothing Then
                        TaskLogger.AddMessage(String.Format("Could not process formula '{0}'", Me.PropertyFormula))
                    Else
                        Formula = Formula.Trim
                    End If

                    If Not TaskLogger.HasErrors Then
                        If Formula = "" Then
                            TaskLogger.AddMessage(String.Format("Formula '{0}' not assigned", Me.PropertyFormula))
                        End If
                    End If

                    If Not TaskLogger.HasErrors Then
                        If Me.ComparisonContains Then
                            If Not Filename.ToLower.Contains(Formula.ToLower) Then
                                TaskLogger.AddMessage(String.Format("File name '{0}' does not contain formula value '{1}'", Filename, Formula))
                            End If
                        ElseIf Me.ComparisonIsExactly Then
                            If Not Filename.ToLower = Formula.ToLower Then
                                TaskLogger.AddMessage(String.Format("File name '{0}' not the same as formula value '{1}'", Filename, Formula))
                            End If
                        End If
                    End If


                Case ".dft"

                    If Me.DraftsCheckDraftItself Then

                        Formula = SSParentDoc.SubstitutePropertyFormulas(Me.PropertyFormula, TaskLogger)

                        If Formula Is Nothing Then
                            TaskLogger.AddMessage(String.Format("Could not process formula '{0}'", Me.PropertyFormula))
                        Else
                            Formula = Formula.Trim
                        End If

                        If Not TaskLogger.HasErrors Then
                            If Formula = "" Then
                                TaskLogger.AddMessage(String.Format("Formula '{0}' not assigned", Me.PropertyFormula))
                            End If
                        End If

                        If Not TaskLogger.HasErrors Then
                            If Me.ComparisonContains Then
                                If Not Filename.ToLower.Contains(Formula.ToLower) Then
                                    TaskLogger.AddMessage(String.Format("File name '{0}' does not contain formula value '{1}'", Filename, Formula))
                                End If
                            ElseIf Me.ComparisonIsExactly Then
                                If Not Filename.ToLower = Formula.ToLower Then
                                    TaskLogger.AddMessage(String.Format("File name '{0}' not the same as formula value '{1}'", Filename, Formula))
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

                                    Formula = SSChildDoc.SubstitutePropertyFormulas(Me.PropertyFormula, TaskLogger)

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
                                        TaskLogger.AddMessage(String.Format("FOA file '{0}' not processed", IO.Path.GetFileName(ChildName)))
                                    End If
                                End Try

                                If SSChildDoc IsNot Nothing Then SSChildDoc.Close()

                            Next

                            If (Not FormulaFound) And (ChildNames.Count > 0) Then
                                If Me.ComparisonContains Then
                                    If ChildNames.Count = 1 Then
                                        TaskLogger.AddMessage(String.Format("File name '{0}' does not contain property in the following model file", Filename))
                                    Else
                                        TaskLogger.AddMessage(String.Format("File name '{0}' does not contain property in the following model files", Filename))
                                    End If
                                ElseIf Me.ComparisonIsExactly Then
                                    If ChildNames.Count = 1 Then
                                        TaskLogger.AddMessage(String.Format("File name '{0}' not the same as the property in the following model file", Filename))
                                    Else
                                        TaskLogger.AddMessage(String.Format("File name '{0}' not the same as the property in the following model files", Filename))
                                    End If
                                End If
                                For i As Integer = 0 To ChildNames.Count - 1
                                    TaskLogger.AddMessage(String.Format("    Model file: '{0}', property value: '{1}'", IO.Path.GetFileName(ChildNames(i)), Formulas(i)))
                                Next
                            End If
                        End If
                    End If

                Case Else
                    MsgBox(String.Format("{0} Extension '{1}' not recognized", Me.Name, ExtensionParent))

            End Select

        End If

        If SSParentDoc IsNot Nothing Then SSParentDoc.Close()

    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
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

    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If

            If Me.PropertyFormula = "" Then
                ErrorLogger.AddMessage("Enter a property formula")
            End If

            If (Not Me.ComparisonContains) And (Not Me.ComparisonIsExactly) Then
                ErrorLogger.AddMessage("Select a comparison option")
            End If

            If (Not Me.DraftsCheckModels) And (Not Me.DraftsCheckDraftItself) And (Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one Draft process option")
            End If

            Me.LinkManagementOrder = Form_Main.LinkManagementOrder

            If (Me.StructuredStorageEdit) And (Me.DraftsCheckModels) And (Me.LinkManagementOrder Is Nothing) Then
                ErrorLogger.AddMessage("Populate LinkMgmt.txt file name on the Configuration Tab -- Templates page")
            End If

            If (Me.StructuredStorageEdit) And (Me.DraftsCheckModels) And (Me.LinkManagementOrder IsNot Nothing) Then
                If Me.LinkManagementOrder.Count = 0 Then
                    ErrorLogger.AddMessage("LinkMgmt.txt file does not contain any search order information")
                End If
            End If
        End If

    End Sub


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
        HelpString = "Checks the file name for the presence of a property (or combination of properties and other text). "

        HelpString += vbCrLf + vbCrLf + "![CheckFilename](My%20Project/media/task_check_filename.png)"

        HelpString += vbCrLf + vbCrLf + "Enter the property formula on the Options panel.  For example,"
        HelpString += vbCrLf + "- `%{System.Document Number}`"
        HelpString += vbCrLf + "- `%{Custom.PartNumber}-%{Custom.RevisionLetter}`"

        HelpString += vbCrLf + vbCrLf + "In the image above, the program is set to check if a `*.dft` has the same name as the model it depicts.  "

        HelpString += vbCrLf + vbCrLf + "For more information on properties, in particular how to make them available with a right-click, see the "
        HelpString += "[<ins>**Property Filter**</ins>](#property-filter) section in this README file. "

        HelpString += vbCrLf + vbCrLf + "There are two comparison methods. "
        HelpString += vbCrLf + "- `Comparison contains` checks if the property appears anywhere in the file name.  "
        HelpString += "If the property value is, say, `7481-12104` and the file name is `7481-12104 Motor Mount.par`, "
        HelpString += "you would get a match. "
        HelpString += vbCrLf + "- `Comparison is exactly` looks for an exact match.  "
        HelpString += "With the previous example, the program would report an error. "

        HelpString += vbCrLf + vbCrLf + "For `*.dft` files, there are two ways to search.  You can use either one, or both. "
        HelpString += vbCrLf + "- `Check model files` is the option most users will want.  "
        HelpString += "It searches any models linked to the file for a property match.  "
        HelpString += vbCrLf + "- `Check draft itself` searches properties in the draft file.  "
        HelpString += "Since draft files rarely have properties of their own, this is usually not necessary. "
        HelpString += "Also, because missing properties are reported as an error, "
        HelpString += "it can be distracting/confusing as well. "
        HelpString += vbCrLf + "- `Check draft itself`  I'm not done with this.  "
        HelpString += "In the example above, the program will check if the model and draft files have the same name.  "
        HelpString += "With this option enabled, it would check if the draft file has the same name as itself.  "
        HelpString += "That will always be true, and never be what you want. "

        Return HelpString
    End Function



End Class
