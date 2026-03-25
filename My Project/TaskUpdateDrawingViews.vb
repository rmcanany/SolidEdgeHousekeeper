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
        PropertyText
        PartsList
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
        Me.PropertyText = False
        Me.PartsList = False
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
                TaskLogger.AddMessage(String.Format("Model file '{0}' not found", Filename))

            ElseIf ModelLink.ModelOutOfDate Then
                s = String.Format("Model link out of date '{0}'", Filename)
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
                            If Not DrawingView.IsUpToDate Then
                                ' Can fail if the model file is missing.
                                Try
                                    DrawingView.Update()
                                    If DrawingView.IsUpToDate Then
                                        PerformedUpdate = True
                                    Else
                                        TaskLogger.AddMessage("Unable to update drawing view")
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        End If

                        If DVCount = 0 And Me.SheetScale Then
                            Sheet.SheetSetup.SetDefaultDrawingViewScale(1, 1 / DrawingView.ScaleFactor)
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

        If PerformedUpdate Then
            If SEDoc.ReadOnly Then
                TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        End If

    End Sub


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

        CheckBox = FormatOptionsCheckBox(ControlNames.SheetScale.ToString, "Sheet scale does not match first drawing view scale")
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

            If Not (Me.DrawingView Or Me.PropertyText Or Me.PartsList Or Me.SheetScale) Then
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

            Case ControlNames.PropertyText.ToString
                Me.PropertyText = Checkbox.Checked

            Case ControlNames.PartsList.ToString
                Me.PartsList = Checkbox.Checked

            Case ControlNames.SheetScale.ToString
                Me.SheetScale = Checkbox.Checked

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
        HelpString = "Updates draft files.  Presents options to process drawing views, property text, parts lists, and sheet scale.  "
        HelpString += "The latter option sets the scale to match the first drawing view added the sheet.  "
        Return HelpString
    End Function


End Class
