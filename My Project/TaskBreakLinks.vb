Option Strict On

Public Class TaskBreakLinks
    Inherits Task

    Private _AllowPartialSuccess As Boolean
    Public Property AllowPartialSuccess As Boolean
        Get
            Return _AllowPartialSuccess
        End Get
        Set(value As Boolean)
            _AllowPartialSuccess = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AllowPartialSuccess.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

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

    Private _BreakDraftSymbols As Boolean
    Public Property BreakDraftSymbols As Boolean
        Get
            Return _BreakDraftSymbols
        End Get
        Set(value As Boolean)
            _BreakDraftSymbols = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BreakDraftSymbols.ToString), CheckBox).Checked = value
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
        AllowPartialSuccess
        BreakDesignCopies
        BreakConstructionCopies
        BreakExcel
        BreakInterpartCopies
        BreakDraftModels
        BreakDraftSymbols
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

        If Me.BreakDesignCopies Or Me.BreakInterpartCopies Then
            DoBreakDesignCopies(SEDoc, SEApp)
        End If

        If Me.BreakConstructionCopies Or Me.BreakInterpartCopies Then
            DoBreakConstructionCopies(SEDoc, SEApp)
        End If

        If Me.BreakExcel Or Me.BreakInterpartCopies Then
            DoBreakExcel(SEDoc, SEApp)
        End If

        ' https://community.sw.siemens.com/s/question/0D5Vb000007bHr1KAE/how-do-i-break-these-links
        If Me.BreakInterpartCopies Then
            DoBreakInterpartCopies(SEDoc, SEApp)
        End If

        If Me.BreakDraftModels Then
            DoBreakDraftModels(SEDoc, SEApp)
        End If

        If Me.BreakDraftSymbols Then
            DoBreakDraftSymbols(SEDoc, SEApp)
        End If

        If SEDoc.ReadOnly Then
            TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
        Else
            If TaskLogger.HasErrors Then
                If Me.AllowPartialSuccess Then
                    SEDoc.Save()
                    SEApp.DoIdle()
                Else
                    TaskLogger.AddMessage("Errors encountered.  No changes made")
                End If
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        End If

    End Sub


    Private Sub DoBreakDesignCopies(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        Dim Models As SolidEdgePart.Models = Nothing
        Dim Model As SolidEdgePart.Model
        Dim CopiedParts As SolidEdgePart.CopiedParts
        Dim CopiedPart As SolidEdgePart.CopiedPart
        Dim FileChanged As Boolean = False

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case = "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                Models = tmpSEDoc.Models

            Case = "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                Models = tmpSEDoc.Models

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
                Me.TaskLogger.AddMessage(String.Format("{0} models exceeds maximum to process", Models.Count.ToString))
            End If
        End If

    End Sub

    Private Sub DoBreakConstructionCopies(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        Dim CopyConstructions As SolidEdgePart.CopyConstructions = Nothing
        Dim CopyConstruction As SolidEdgePart.CopyConstruction

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        Select Case DocType
            Case = "par"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                CopyConstructions = tmpSEDoc.Constructions.CopyConstructions

            Case = "psm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                CopyConstructions = tmpSEDoc.Constructions.CopyConstructions

        End Select

        If Not CopyConstructions Is Nothing Then
            If (CopyConstructions.Count > 0) And (CopyConstructions.Count < 300) Then
                For Each CopyConstruction In CopyConstructions
                    ' Synchronous part copies are not links.
                    ' Must be ignored or NotImplemented exception will be thrown
                    If CopyConstruction.ModelingModeType = 2 Then
                        If Not CopyConstruction.IsBroken Then
                            CopyConstruction.BreakLinks()
                            SEApp.DoIdle()
                            ' SE will report an out of date link on next open if we don't update
                            CopyConstruction.Update()
                            SEApp.DoIdle()
                        End If
                    End If
                Next
            ElseIf CopyConstructions.Count >= 300 Then
                Me.TaskLogger.AddMessage(String.Format("{0} construction copies exceeds maximum to process", CopyConstructions.Count.ToString))
            End If
        End If

    End Sub

    Private Sub DoBreakExcel(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        Dim UC As New UtilsCommon

        Dim Variables = UC.GetDocVariables(SEDoc)
        Dim Variable As SolidEdgeFramework.variable

        For Each VariableName As String In Variables.Keys
            Variable = Variables(VariableName)
            If (Variable.Formula.Contains(".xlsx")) Or (Variable.Formula.Contains(".xls")) Then
                Variable.Formula = ""
            End If
        Next

        Dim Dimensions = UC.GetDocDimensions(SEDoc)
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension

        For Each DimensionName As String In Dimensions.Keys
            Dimension = Dimensions(DimensionName)
            If (Dimension.Formula.Contains(".xlsx")) Or (Dimension.Formula.Contains(".xls")) Then
                Dimension.Formula = ""
            End If
        Next

    End Sub

    Private Sub DoBreakInterpartCopies(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

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

    End Sub

    Private Sub DoBreakDraftModels(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        Dim UC As New UtilsCommon

        Select Case UC.GetDocType(SEDoc)

            Case "dft"
                Dim tmpSEDoc As SolidEdgeDraft.DraftDocument = CType(SEDoc, SolidEdgeDraft.DraftDocument)
                Dim Sheets As List(Of SolidEdgeDraft.Sheet)

                Try
                    Sheets = UC.GetSheets(tmpSEDoc, "Background")
                    ProcessPropertyText(Sheets)
                    ProcessDrawingViews(Sheets)

                    Sheets = UC.GetSheets(tmpSEDoc, "Working")
                    ProcessPropertyText(Sheets)
                    ProcessDrawingViews(Sheets)

                Catch ex As Exception
                    'Me.TaskLogger.AddMessage("Unable to process all sheets.  No changes made.")
                End Try

        End Select

    End Sub

    Private Sub ProcessPropertyText(Sheets As List(Of SolidEdgeDraft.Sheet))
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim Balloons As SolidEdgeFrameworkSupport.Balloons
        Dim Balloon As SolidEdgeFrameworkSupport.Balloon
        Dim BlockOccurrences As SolidEdgeDraft.BlockOccurrences
        Dim BlockOccurrence As SolidEdgeDraft.BlockOccurrence

        For Each Sheet In Sheets
            Balloons = CType(Sheet.Balloons, SolidEdgeFrameworkSupport.Balloons)
            For Each Balloon In Balloons
                Try
                    Balloon.BalloonText = Balloon.BalloonDisplayedText
                Catch ex2 As Exception
                    Me.TaskLogger.AddMessage($"Unable to process balloon '{Balloon.Name}'")
                End Try
            Next

            BlockOccurrences = Sheet.BlockOccurrences
            For Each BlockOccurrence In BlockOccurrences
                Dim BlockView As SolidEdgeDraft.BlockView = BlockOccurrence.BlockView
                Balloons = BlockView.Balloons
                For Each Balloon In Balloons
                    Try
                        Balloon.BalloonText = Balloon.BalloonDisplayedText
                    Catch ex2 As Exception
                        Me.TaskLogger.AddMessage($"Unable to process balloon '{Balloon.Name}'")
                    End Try
                Next

                Dim BlockLabelOccurrences As SolidEdgeDraft.BlockLabelOccurrences = BlockOccurrence.BlockLabelOccurrences
                For Each BlockLabelOccurrence As SolidEdgeDraft.BlockLabelOccurrence In BlockLabelOccurrences
                    Try
                        BlockLabelOccurrence.value = BlockLabelOccurrence.DisplayedText
                    Catch ex As Exception
                        Me.TaskLogger.AddMessage($"Unable to process block label '{BlockLabelOccurrence.Name}'")
                    End Try
                Next
            Next
        Next

    End Sub

    Private Sub ProcessDrawingViews(Sheets As List(Of SolidEdgeDraft.Sheet))
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
                    Me.TaskLogger.AddMessage($"Unable to process drawing view on sheet '{Sheet.Name}'.")
                End Try
            Next
        Next

    End Sub

    Private Sub DoBreakDraftSymbols(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        Dim UC As New UtilsCommon

        Select Case UC.GetDocType(SEDoc)

            Case "dft"
                Dim tmpSEDoc As SolidEdgeDraft.DraftDocument = CType(SEDoc, SolidEdgeDraft.DraftDocument)
                Dim SheetLists As New List(Of List(Of SolidEdgeDraft.Sheet))
                Dim SheetList As List(Of SolidEdgeDraft.Sheet)

                SheetLists.Add(UC.GetSheets(tmpSEDoc, "Background"))
                SheetLists.Add(UC.GetSheets(tmpSEDoc, "Working"))

                For Each SheetList In SheetLists
                    For Each Sheet As SolidEdgeDraft.Sheet In SheetList
                        Dim Symbols As SolidEdgeFramework.Symbols = CType(Sheet.Symbols, SolidEdgeFramework.Symbols)
                        If Symbols IsNot Nothing Then
                            For Each Symbol2d As SolidEdgeFramework.Symbol2d In Symbols
                                Try
                                    Dim SourceFilename As String = Symbol2d.SourceDoc
                                    If IO.File.Exists(SourceFilename) Then
                                        If Symbol2d.Class = "SolidEdge.DraftDocument" Then
                                            Symbol2d.ConvertToGroup()
                                        End If
                                    Else
                                        Me.TaskLogger.AddMessage($"Unable to process symbol '{Symbol2d.Name}' on sheet '{Sheet.Name}'")
                                        Me.TaskLogger.AddMessage($"    Linked file not found '{SourceFilename}'")
                                    End If
                                Catch ex As Exception
                                    Me.TaskLogger.AddMessage($"Unable to process symbol '{Symbol2d.Name}' on sheet '{Sheet.Name}'")
                                End Try
                            Next
                        End If
                    Next
                Next

        End Select

    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.AllowPartialSuccess.ToString, "Allow partial success")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BreakDesignCopies.ToString, "Part copy design links (*.par, *.psm)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BreakConstructionCopies.ToString, "Part copy construction links (*.par, *.psm)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BreakExcel.ToString, "Excel links (*.*)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BreakInterpartCopies.ToString, "All interpart links (*.par, *.psm, *.asm)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BreakDraftModels.ToString, "Draft model links (*.dft)")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BreakDraftSymbols.ToString, "Draft symbol links (*.dft)")
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

            Dim tf As Boolean
            tf = Me.BreakConstructionCopies
            tf = tf Or Me.BreakDesignCopies
            tf = tf Or Me.BreakExcel
            tf = tf Or Me.BreakInterpartCopies
            tf = tf Or Me.BreakDraftModels
            tf = tf Or Me.BreakDraftSymbols

            If Not tf Then
                ErrorLogger.AddMessage("Select at least one type of link to break")
            End If

        End If

    End Sub


    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.AllowPartialSuccess.ToString
                Me.AllowPartialSuccess = Checkbox.Checked

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

            Case ControlNames.BreakDraftSymbols.ToString
                Me.BreakDraftSymbols = Checkbox.Checked

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

        HelpString += vbCrLf + vbCrLf + "![BreakLinks](My%20Project/media/task_break_links.png)"

        HelpString += vbCrLf + vbCrLf + "The command options are explained below. "

        HelpString += vbCrLf + vbCrLf + "`Allow partial success` "
        HelpString += "Disabling this option means the file will not be saved if any errors occur. "
        HelpString += "The error will still be reported in the log file, alerting you to investiage manually. "
        HelpString += "Since breaking links is irreversible, this is the safest option.  "
        HelpString += "However, it can also be a nuisance.  That is why it is presented as an option. "

        HelpString += vbCrLf + vbCrLf + "`Part copy design links` and `Part copy construction links` "
        HelpString += "remove links created with the `Part Copy` command. "
        HelpString += "The geometry remains intact."

        HelpString += vbCrLf + vbCrLf + "`Excel links` removes Excel references from `Variable` and `Dimension` formulas. "
        HelpString += "In both cases, the value remains as it was before the link was removed."

        HelpString += vbCrLf + vbCrLf + "`All interpart links` is the sledgehammer option. "
        HelpString += "It removes the links cited above. "
        HelpString += "It also removes `included links` in profiles and `pasted links` in the variable table. "
        HelpString += "It might do more.  The complete API documentation (below) is, uh, short on details. "

        HelpString += vbCrLf + vbCrLf + "![Break all interpart links](My%20Project/media/break_all_interpart_links_documentation.png)"


        HelpString += vbCrLf + vbCrLf + "`Draft model links` converts drawing views to 2D, "
        HelpString += "removing external references in the process. "
        HelpString += "In testing it quickly became apparent that this operation "
        HelpString += "also converts Property text to blank lines in Callouts. "

        HelpString += vbCrLf + vbCrLf + "![Title Block](My%20Project/media/title_block.png)"

        HelpString += vbCrLf + vbCrLf + "Luckily, Solid Edge can take care of that. "
        HelpString += "That's in the program, but only for Callouts. "
        HelpString += "If you have TextBoxes, Blocks, or other objects that use Property text, let me know. "
        HelpString += "I can try to address those in a future release. "

        HelpString += vbCrLf + vbCrLf + "`Draft symbol links` converts symbol blocks to geometry, "
        HelpString += "removing external references in the process. "

        Return HelpString
    End Function


End Class
