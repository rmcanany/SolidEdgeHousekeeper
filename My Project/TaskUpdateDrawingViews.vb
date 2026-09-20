Option Strict On

Public Class TaskUpdateDrawingViews

    Inherits Task

    Private _DrawingView As Boolean
    Public Property DrawingView As Boolean
        Get
            Return _DrawingView
        End Get
        Set(value As Boolean)
            _DrawingView = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.DrawingView.ToString), CheckBox).Checked = value
                CType(
                    ControlsDict(ControlNames.ForceDrawingViewUpdate.ToString),
                    CheckBox).Visible = value
            End If
        End Set
    End Property

    Private _ForceDrawingViewUpdate As Boolean
    Public Property ForceDrawingViewUpdate As Boolean
        Get
            Return _ForceDrawingViewUpdate
        End Get
        Set(value As Boolean)
            _ForceDrawingViewUpdate = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ForceDrawingViewUpdate.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _PropertyText As Boolean
    Public Property PropertyText As Boolean
        Get
            Return _PropertyText
        End Get
        Set(value As Boolean)
            _PropertyText = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.PropertyText.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _PartsList As Boolean
    Public Property PartsList As Boolean
        Get
            Return _PartsList
        End Get
        Set(value As Boolean)
            _PartsList = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.PartsList.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _HoleTable As Boolean
    Public Property HoleTable As Boolean
        Get
            Return _HoleTable
        End Get
        Set(value As Boolean)
            _HoleTable = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.HoleTable.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _BendTable As Boolean
    Public Property BendTable As Boolean
        Get
            Return _BendTable
        End Get
        Set(value As Boolean)
            _BendTable = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BendTable.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _BlockTable As Boolean
    Public Property BlockTable As Boolean
        Get
            Return _BlockTable
        End Get
        Set(value As Boolean)
            _BlockTable = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.BlockTable.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _ConnectorTable As Boolean
    Public Property ConnectorTable As Boolean
        Get
            Return _ConnectorTable
        End Get
        Set(value As Boolean)
            _ConnectorTable = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ConnectorTable.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UserTable As Boolean
    Public Property UserTable As Boolean
        Get
            Return _UserTable
        End Get
        Set(value As Boolean)
            _UserTable = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UserTable.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _SheetScale As Boolean
    Public Property SheetScale As Boolean
        Get
            Return _SheetScale
        End Get
        Set(value As Boolean)
            _SheetScale = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.SheetScale.ToString), CheckBox).Checked = value
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
        DrawingView
        ForceDrawingViewUpdate
        PropertyText
        PartsList
        HoleTable
        BendTable
        BlockTable
        ConnectorTable
        UserTable
        SheetScale
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
        Me.Image = My.Resources.TaskUpdateDrawingViews
        Me.Category = "Update"
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.DrawingView = False
        Me.ForceDrawingViewUpdate = False
        Me.PropertyText = False
        Me.PartsList = False
        Me.HoleTable = False
        Me.BendTable = False
        Me.BlockTable = False
        Me.ConnectorTable = False
        Me.UserTable = False
        Me.SheetScale = False

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

        OleMessageFilter.Register()

        Dim Sheet As SolidEdgeDraft.Sheet = Nothing
        Dim DrawingViews As SolidEdgeDraft.DrawingViews = Nothing
        Dim DrawingView As SolidEdgeDraft.DrawingView = Nothing

        Dim ModelLinks As SolidEdgeDraft.ModelLinks = Nothing
        Dim ModelLink As SolidEdgeDraft.ModelLink = Nothing

        Dim Filename As String

        Dim PerformedUpdate As Boolean = False

        Dim PartsLists As SolidEdgeDraft.PartsLists
        Dim PartsList As SolidEdgeDraft.PartsList

        Dim s As String

        Dim UC As New UtilsCommon

        Dim tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

        ' Check for missing model files.
        ModelLinks = tmpSEDoc.ModelLinks

        For Each ModelLink In ModelLinks
            Filename = UC.GetFOAFilename(ModelLink.FileName)

            'If ModelLink.IsAssemblyFamilyMember Then
            '    Filename = ModelLink.FileName.Split("!"c)(0)
            'Else
            '    Filename = ModelLink.FileName
            'End If

            If Not FileIO.FileSystem.FileExists(Filename) Then
                TaskLogger.AddMessage($"Model file '{Filename}' not found")

            ElseIf ModelLink.ModelOutOfDate Then
                s = $"Model link out of date '{Filename}'"
                If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
            End If
        Next

        If Me.DrawingView Or Me.SheetScale Then
            For Each SheetType As String In {"Working", "Background"}
                For Each Sheet In UC.GetSheets(tmpSEDoc, SheetType)
                    DrawingViews = Sheet.DrawingViews
                    Dim DVCount As Integer = 0

                    For Each DrawingView In DrawingViews.OfType(Of SolidEdgeDraft.DrawingView)()
                        If Me.DrawingView Then
                            Dim UpdateRequired As Boolean
                            UpdateRequired = Me.ForceDrawingViewUpdate OrElse Not DrawingView.IsUpToDate

                            If UpdateRequired Then
                                ' Update can fail if the model file is missing.
                                Try
                                    If Me.ForceDrawingViewUpdate Then
                                        DrawingView.ForceUpdate()
                                    Else
                                        DrawingView.Update()
                                    End If

                                    If DrawingView.IsUpToDate Then
                                        PerformedUpdate = True
                                    Else
                                        TaskLogger.AddMessage($"Unable to update drawing view on sheet '{Sheet.Name}'")
                                    End If

                                Catch ex As Exception
                                    ' Some drawing view types can reject ForceUpdate().
                                    ' Fall back to the standard Update() method before reporting a failure.
                                    If Me.ForceDrawingViewUpdate Then
                                        Try
                                            DrawingView.Update()

                                            If DrawingView.IsUpToDate Then
                                                PerformedUpdate = True
                                            Else
                                                TaskLogger.AddMessage($"Unable to update drawing view on sheet '{Sheet.Name}'")
                                            End If

                                        Catch exUpdate As Exception
                                            TaskLogger.AddMessage(
                                                $"Unable to update drawing view on sheet '{Sheet.Name}'.  Exception: {exUpdate.Message}")
                                        End Try

                                    Else
                                        TaskLogger.AddMessage(
                                            $"Unable to update drawing view on sheet '{Sheet.Name}'.  Exception: {ex.Message}")
                                    End If
                                End Try
                            End If
                        End If

                        If DVCount = 0 And Me.SheetScale Then
                            'Sheet.SheetSetup.SetDefaultDrawingViewScale(1, 1 / DrawingView.ScaleFactor)

                            If Sheet.SheetSetup.IsManualSheetScale Then
                                Sheet.SheetSetup.DrawingViewForSheetScale = DrawingView
                            End If

                            PerformedUpdate = True

                        End If

                        DVCount += 1
                    Next DrawingView
                Next Sheet
            Next

        End If

        If Me.PropertyText Then
            'tmpSEDoc.UpdatePropertyTextDisplay()
            tmpSEDoc.UpdatePropertyTextCacheAndDisplay()
            PerformedUpdate = True
        End If

        If Me.PartsList Then
            PartsLists = tmpSEDoc.PartsLists

            ' Not all draft files have PartsLists
            Try
                For Each PartsList In PartsLists
                    If Not PartsList.IsUpToDate Then
                        PartsList.Update()
                        If PartsList.IsUpToDate Then
                            PerformedUpdate = True
                        Else
                            TaskLogger.AddMessage("Unable to update parts list")
                        End If
                    End If
                Next
            Catch ex As Exception
            End Try

        End If

        If Me.HoleTable Then
            If UpdateHoleTables(tmpSEDoc) Then PerformedUpdate = True
        End If

        If Me.BendTable Then
            If UpdateBendTables(tmpSEDoc) Then PerformedUpdate = True
        End If

        If Me.BlockTable Then
            If UpdateBlockTables(tmpSEDoc) Then PerformedUpdate = True
        End If

        If Me.ConnectorTable Then
            If UpdateConnectorTables(tmpSEDoc) Then PerformedUpdate = True
        End If

        If Me.UserTable Then
            If UpdateUserTables(tmpSEDoc) Then PerformedUpdate = True
        End If

        If PerformedUpdate Then
            If SEDoc.ReadOnly Then
                TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        End If

    End Sub


    Private Function UpdateHoleTables(
        ByVal DraftDocument As SolidEdgeDraft.DraftDocument) As Boolean

        Dim PerformedUpdate As Boolean = False

        Try
            For Each HoleTable As SolidEdgeDraft.HoleTable In DraftDocument.HoleTables
                Try
                    HoleTable.Update()
                    PerformedUpdate = True
                Catch ex As Exception
                    TaskLogger.AddMessage($"Unable to update hole table.  Exception: {ex.Message}")
                End Try
            Next

        Catch ex As Exception
            TaskLogger.AddMessage($"Unable to access hole tables.  Exception: {ex.Message}")
        End Try

        Return PerformedUpdate
    End Function


    Private Function UpdateBendTables(
        ByVal DraftDocument As SolidEdgeDraft.DraftDocument) As Boolean

        Dim PerformedUpdate As Boolean = False

        Try
            For Each BendTable As SolidEdgeDraft.DraftBendTable In DraftDocument.DraftBendTables
                Try
                    BendTable.Update()
                    PerformedUpdate = True
                Catch ex As Exception
                    TaskLogger.AddMessage($"Unable to update bend table.  Exception: {ex.Message}")
                End Try
            Next

        Catch ex As Exception
            TaskLogger.AddMessage($"Unable to access bend tables.  Exception: {ex.Message}")
        End Try

        Return PerformedUpdate
    End Function


    Private Function UpdateBlockTables(
        ByVal DraftDocument As SolidEdgeDraft.DraftDocument) As Boolean

        Dim PerformedUpdate As Boolean = False

        Try
            For Each BlockTable As SolidEdgeDraft.BlockTable In DraftDocument.BlockTables
                Try
                    BlockTable.Update()
                    PerformedUpdate = True
                Catch ex As Exception
                    TaskLogger.AddMessage($"Unable to update block table.  Exception: {ex.Message}")
                End Try
            Next

        Catch ex As Exception
            TaskLogger.AddMessage($"Unable to access block tables.  Exception: {ex.Message}")
        End Try

        Return PerformedUpdate
    End Function


    Private Function UpdateConnectorTables(
        ByVal DraftDocument As SolidEdgeDraft.DraftDocument) As Boolean

        Dim PerformedUpdate As Boolean = False

        Try
            For Each ConnectorTable As SolidEdgeDraft.ConnectorTable In DraftDocument.ConnectorTables
                Try
                    ConnectorTable.Update()
                    PerformedUpdate = True
                Catch ex As Exception
                    TaskLogger.AddMessage($"Unable to update connector table.  Exception: {ex.Message}")
                End Try
            Next

        Catch ex As Exception
            TaskLogger.AddMessage($"Unable to access connector tables.  Exception: {ex.Message}")
        End Try

        Return PerformedUpdate
    End Function


    Private Function UpdateUserTables(
        ByVal DraftDocument As SolidEdgeDraft.DraftDocument) As Boolean

        Dim PerformedUpdate As Boolean = False

        Try
            For Each UserTable As SolidEdgeDraft.Table In DraftDocument.Tables
                Try
                    UserTable.Update()
                    PerformedUpdate = True
                Catch ex As Exception
                    TaskLogger.AddMessage($"Unable to update user table.  Exception: {ex.Message}")
                End Try
            Next

        Catch ex As Exception
            TaskLogger.AddMessage($"Unable to access user tables.  Exception: {ex.Message}")
        End Try

        Return PerformedUpdate
    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.DrawingView.ToString, "Drawing view")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ForceDrawingViewUpdate.ToString, "Force update even if current")
        CheckBox.Padding = New Padding(Me.ControlIndent, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.PropertyText.ToString, "Property text")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.PartsList.ToString, "Part list")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.HoleTable.ToString, "Hole table")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BendTable.ToString, "Bend table")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.BlockTable.ToString, "Block table")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ConnectorTable.ToString, "Connector table")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UserTable.ToString, "User table")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.SheetScale.ToString, "If unlinked, link sheet scale to first drawing view")
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

            If Not (
                Me.DrawingView Or
                Me.PropertyText Or
                Me.PartsList Or
                Me.HoleTable Or
                Me.BendTable Or
                Me.BlockTable Or
                Me.ConnectorTable Or
                Me.UserTable Or
                Me.SheetScale) Then

                ErrorLogger.AddMessage("Select at least one type of object to update")
            End If
        End If

    End Sub


    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name

            Case ControlNames.DrawingView.ToString
                Me.DrawingView = Checkbox.Checked

            Case ControlNames.ForceDrawingViewUpdate.ToString
                Me.ForceDrawingViewUpdate = Checkbox.Checked

            Case ControlNames.PropertyText.ToString
                Me.PropertyText = Checkbox.Checked

            Case ControlNames.PartsList.ToString
                Me.PartsList = Checkbox.Checked

            Case ControlNames.HoleTable.ToString
                Me.HoleTable = Checkbox.Checked

            Case ControlNames.BendTable.ToString
                Me.BendTable = Checkbox.Checked

            Case ControlNames.BlockTable.ToString
                Me.BlockTable = Checkbox.Checked

            Case ControlNames.ConnectorTable.ToString
                Me.ConnectorTable = Checkbox.Checked

            Case ControlNames.UserTable.ToString
                Me.UserTable = Checkbox.Checked

            Case ControlNames.SheetScale.ToString
                Me.SheetScale = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
        End Select

    End Sub

    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Updates draft files.  Presents options to process drawing views, property text, parts lists, hole tables, bend tables, block tables, connector tables, user tables, and sheet scale.  "
        HelpString += "The drawing view sub-option can force an update even when Solid Edge reports the view as current.  "
        HelpString += "If a sheet scale is not linked to a drawing view, the latter option links it to the first drawing view added the sheet.  "
        Return HelpString
    End Function


End Class
