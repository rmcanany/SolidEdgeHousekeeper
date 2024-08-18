Option Strict On

Public Class TaskBreakLinks
    Inherits Task

    Private _BreakDesignCopies As Boolean
    Public Property BreakDesignCopies As Boolean
        Get
            Return _BreakDesignCopies
        End Get
        Set(value As Boolean)
            _BreakDesignCopies = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BreakDesignCopies.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property


    Private _BreakConstructionCopies As Boolean
    Public Property BreakConstructionCopies As Boolean
        Get
            Return _BreakConstructionCopies
        End Get
        Set(value As Boolean)
            _BreakConstructionCopies = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BreakConstructionCopies.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _BreakExcel As Boolean
    Public Property BreakExcel As Boolean
        Get
            Return _BreakExcel
        End Get
        Set(value As Boolean)
            _BreakExcel = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BreakExcel.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _BreakInterpartCopies As Boolean
    Public Property BreakInterpartCopies As Boolean
        Get
            Return _BreakInterpartCopies
        End Get
        Set(value As Boolean)
            _BreakInterpartCopies = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BreakInterpartCopies.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _BreakDraftModels As Boolean
    Public Property BreakDraftModels As Boolean
        Get
            Return _BreakDraftModels
        End Get
        Set(value As Boolean)
            _BreakDraftModels = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BreakDraftModels.ToString), CheckBox).Checked = value
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
        BreakDesignCopies
        BreakConstructionCopies
        BreakExcel
        BreakInterpartCopies
        BreakDraftModels
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
        Me.Image = My.Resources.TaskCheckPartCopies
        Me.Category = "Update"
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.BreakDesignCopies = False
        Me.BreakConstructionCopies = False
        Me.BreakExcel = False
        Me.BreakInterpartCopies = False
        Me.BreakDraftModels = False

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

        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))


        If Me.BreakDesignCopies Then
            SupplementalErrorMessage = DoBreakDesignCopies(SEDoc, Configuration, SEApp)
            AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)
        End If

        If Me.BreakConstructionCopies Then
            SupplementalErrorMessage = DoBreakConstructionCopies(SEDoc, Configuration, SEApp)
            AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)
        End If

        If Me.BreakExcel Then
            SupplementalErrorMessage = DoBreakExcel(SEDoc, Configuration, SEApp)
            AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)
        End If

        ' https://community.sw.siemens.com/s/question/0D5Vb000007bHr1KAE/how-do-i-break-these-links
        If Me.BreakInterpartCopies Then
            ' Design copies
            SupplementalErrorMessage = DoBreakDesignCopies(SEDoc, Configuration, SEApp)
            AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)

            ' Construction copies
            SupplementalErrorMessage = DoBreakConstructionCopies(SEDoc, Configuration, SEApp)
            AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)

            ' Excel
            SupplementalErrorMessage = DoBreakExcel(SEDoc, Configuration, SEApp)
            AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)

            ' Whatever else it wants
            SupplementalErrorMessage = DoBreakInterpartCopies(SEDoc, Configuration, SEApp)
            AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)
        End If

        If Me.BreakDraftModels Then
            SupplementalErrorMessage = DoBreakDraftModels(SEDoc, Configuration, SEApp)
            AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)
        End If

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
            If ExitStatus = 0 Then
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        End If


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function

    Private Function DoBreakInterpartCopies(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim FileChanged As Boolean = True

        Dim UC As New UtilsCommon

        Select Case UC.GetDocType(SEDoc)

            Case "par"
                Dim tmpSEDoc As SolidEdgePart.PartDocument = CType(SEDoc, SolidEdgePart.PartDocument)
                tmpSEDoc.BreakAllInterpartLinks()

            Case "psm"
                Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                tmpSEDoc.BreakAllInterpartLinks()

            Case "asm"
                Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)
                tmpSEDoc.BreakAllInterpartLinks()

        End Select

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Private Function DoBreakDraftModels(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim FileChanged As Boolean = True

        Dim UC As New UtilsCommon

        Select Case UC.GetDocType(SEDoc)

            Case "dft"
                Dim tmpSEDoc As SolidEdgeDraft.DraftDocument = CType(SEDoc, SolidEdgeDraft.DraftDocument)
                Dim Sheets As List(Of SolidEdgeDraft.Sheet)

                Try
                    Sheets = UC.GetSheets(tmpSEDoc, "Background")
                    ProcessCallouts(Sheets)
                    ProcessDrawingViews(Sheets)

                    Sheets = UC.GetSheets(tmpSEDoc, "Working")
                    ProcessCallouts(Sheets)
                    ProcessDrawingViews(Sheets)

                Catch ex As Exception
                    FileChanged = False
                    ExitStatus = 1
                    ErrorMessageList.Add("Unable to process all sheets.  No changes made.")
                End Try

        End Select

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Private Shared Sub ProcessCallouts(Sheets As List(Of SolidEdgeDraft.Sheet))
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim Balloons As SolidEdgeFrameworkSupport.Balloons
        Dim Balloon As SolidEdgeFrameworkSupport.Balloon

        For Each Sheet In Sheets
            Balloons = CType(Sheet.Balloons, SolidEdgeFrameworkSupport.Balloons)
            For Each Balloon In Balloons
                Try
                    Balloon.BalloonText = Balloon.BalloonDisplayedText
                Catch ex2 As Exception
                End Try
            Next
        Next

    End Sub

    Private Shared Sub ProcessDrawingViews(Sheets As List(Of SolidEdgeDraft.Sheet))
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim DrawingViews As SolidEdgeDraft.DrawingViews
        Dim DrawingView As SolidEdgeDraft.DrawingView

        For Each Sheet In Sheets
            DrawingViews = Sheet.DrawingViews
            For Each DrawingView In DrawingViews
                ' Some drawing views are already 2D
                Try
                    DrawingView.Drop()
                Catch ex2 As Exception
                    'exitstatus = 1
                    'Errormessagelist.add("something")
                End Try
            Next
        Next

    End Sub


    Private Function DoBreakExcel(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim FileChanged As Boolean = False

        Dim UC As New UtilsCommon

        Dim Variables = UC.GetDocVariables(SEDoc)
        Dim Variable As SolidEdgeFramework.variable

        For Each VariableName As String In Variables.Keys
            Variable = Variables(VariableName)
            If (Variable.Formula.Contains(".xlsx")) Or (Variable.Formula.Contains(".xls")) Then
                Variable.Formula = ""
                FileChanged = True
            End If
        Next

        Dim Dimensions = UC.GetDocDimensions(SEDoc)
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension

        For Each DimensionName As String In Dimensions.Keys
            Dimension = Dimensions(DimensionName)
            If (Dimension.Formula.Contains(".xlsx")) Or (Dimension.Formula.Contains(".xls")) Then
                Dimension.Formula = ""
                FileChanged = True
            End If
        Next

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Private Function DoBreakDesignCopies(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Models As SolidEdgePart.Models = Nothing
        Dim Model As SolidEdgePart.Model
        Dim CopiedParts As SolidEdgePart.CopiedParts
        Dim CopiedPart As SolidEdgePart.CopiedPart
        'Dim CopyConstructions As SolidEdgePart.CopyConstructions = Nothing
        'Dim CopyConstruction As SolidEdgePart.CopyConstruction
        Dim FileChanged As Boolean = False

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case = "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                Models = tmpSEDoc.Models
                'CopyConstructions = tmpSEDoc.Constructions.CopyConstructions

            Case = "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                Models = tmpSEDoc.Models
                'CopyConstructions = tmpSEDoc.Constructions.CopyConstructions

        End Select

        If Not Models Is Nothing Then
            If (Models.Count > 0) And (Models.Count < 300) Then
                For Each Model In Models
                    CopiedParts = Model.CopiedParts
                    If CopiedParts.Count > 0 Then
                        For Each CopiedPart In CopiedParts
                            ' Synchronous part copies are not linked.
                            ' Must be ignored or NotImplemented exception will be thrown
                            If CopiedPart.ModelingModeType = 2 Then
                                If Not CopiedPart.IsBroken Then
                                    FileChanged = True
                                    CopiedPart.BreakLinks()
                                    SEApp.DoIdle()
                                    ' SE will report an out of date link on next open if we don't update
                                    CopiedPart.Update()
                                    SEApp.DoIdle()
                                End If
                            End If
                        Next
                    End If
                Next
            ElseIf Models.Count >= 300 Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0} models exceeds maximum to process", Models.Count.ToString))
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Private Function DoBreakConstructionCopies(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'Dim Models As SolidEdgePart.Models = Nothing
        'Dim Model As SolidEdgePart.Model
        'Dim CopiedParts As SolidEdgePart.CopiedParts
        'Dim CopiedPart As SolidEdgePart.CopiedPart
        Dim CopyConstructions As SolidEdgePart.CopyConstructions = Nothing
        Dim CopyConstruction As SolidEdgePart.CopyConstruction
        Dim FileChanged As Boolean = False


        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case = "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                'Models = tmpSEDoc.Models
                CopyConstructions = tmpSEDoc.Constructions.CopyConstructions

            Case = "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                'Models = tmpSEDoc.Models
                CopyConstructions = tmpSEDoc.Constructions.CopyConstructions

        End Select

        If Not CopyConstructions Is Nothing Then
            If (CopyConstructions.Count > 0) And (CopyConstructions.Count < 300) Then
                For Each CopyConstruction In CopyConstructions
                    ' Synchronous part copies are not links.
                    ' Must be ignored or NotImplemented exception will be thrown
                    If CopyConstruction.ModelingModeType = 2 Then
                        If Not CopyConstruction.IsBroken Then
                            FileChanged = True
                            CopyConstruction.BreakLinks()
                            SEApp.DoIdle()
                            ' SE will report an out of date link on next open if we don' update
                            CopyConstruction.Update()
                            SEApp.DoIdle()
                        End If
                    End If
                Next
            ElseIf CopyConstructions.Count >= 300 Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0} models exceeds maximum to process", CopyConstructions.Count.ToString))
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        'Dim IU As New InterfaceUtilities

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.BreakDesignCopies.ToString, "Break part copy design links (*.par, *.psm)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BreakConstructionCopies.ToString, "Break part copy construction links (*.par, *.psm)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BreakExcel.ToString, "Break Excel links (*.*)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BreakInterpartCopies.ToString, "Break all interpart links (*.par, *.psm, *.asm)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BreakDraftModels.ToString, "Break draft model links (*.dft)")
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

            Dim tf As Boolean = False
            tf = Me.BreakConstructionCopies
            tf = tf Or Me.BreakDesignCopies
            tf = tf Or Me.BreakExcel
            tf = tf Or Me.BreakInterpartCopies
            tf = tf Or Me.BreakDraftModels

            If Not tf Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one type of link to break", Indent))
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

            Case ControlNames.BreakDesignCopies.ToString
                Me.BreakDesignCopies = Checkbox.Checked

            Case ControlNames.BreakConstructionCopies.ToString
                Me.BreakConstructionCopies = Checkbox.Checked

            Case ControlNames.BreakExcel.ToString
                Me.BreakExcel = Checkbox.Checked

            Case ControlNames.BreakInterpartCopies.ToString
                Me.BreakInterpartCopies = Checkbox.Checked

            Case ControlNames.BreakDraftModels.ToString
                Me.BreakDraftModels = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Breaks external links to a file.  This is irreversible, so you know, think about it. "
        HelpString += "Several options are available.  They are explained below. "

        HelpString += vbCrLf + vbCrLf + "`Break part copy design links` and `Break part copy construction links` "
        HelpString += "remove links created with the `Part Copy` command. "
        HelpString += "The geometry remains intact."

        HelpString += vbCrLf + vbCrLf + "`Break Excel links` removes Excel references from `Variable` and `Dimension` formulas. "
        HelpString += "In both cases, the value remains as it was before the link was removed."

        HelpString += vbCrLf + vbCrLf + "`Break all interpart links` is the sledgehammer option. "
        HelpString += "It removes the links cited above. "
        HelpString += "It also removes `included links` in profiles and `pasted links` in the variable table. "
        HelpString += "It might do more.  The complete API documentation (below) is, uh, short on details. "

        HelpString += vbCrLf + vbCrLf + "![Break all interpart links](My%20Project/media/break_all_interpart_links_documentation.png)"


        HelpString += vbCrLf + vbCrLf + "`Break draft model links` converts drawing views to 2D, "
        HelpString += "removing external references in the process. "
        HelpString += "In testing it quickly became apparent that this operation "
        HelpString += "also converts Property text to blank lines in Callouts. "

        HelpString += vbCrLf + vbCrLf + "![Title Block](My%20Project/media/title_block.png)"

        HelpString += vbCrLf + vbCrLf + "Luckily, Solid Edge can take care of that. "
        HelpString += "That's in the program, but only for Callouts. "
        HelpString += "If you have TextBoxes, Blocks, or other objects that use Property text, let me know. "
        HelpString += "I can try to address those in a future release. "

        Return HelpString
    End Function


End Class
