Option Strict On

Imports Microsoft.WindowsAPICodePack.Dialogs

Public Class TaskCreateDrawingOfFlatPattern
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


    Private _ScaleFactor As Double
    Public Property ScaleFactor As Double
        Get
            Return _ScaleFactor
        End Get
        Set(value As Double)
            _ScaleFactor = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ScaleFactor.ToString), TextBox).Text = CStr(value)
            End If
        End Set
    End Property

    Private _OffsetUnitsIn As Boolean
    Public Property OffsetUnitsIn As Boolean
        Get
            Return _OffsetUnitsIn
        End Get
        Set(value As Boolean)
            _OffsetUnitsIn = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.OffsetUnitsIn.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _OffsetUnitsMM As Boolean
    Public Property OffsetUnitsMM As Boolean
        Get
            Return _OffsetUnitsMM
        End Get
        Set(value As Boolean)
            _OffsetUnitsMM = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.OffsetUnitsMM.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _XOffset As Double
    Public Property XOffset As Double
        Get
            Return _XOffset
        End Get
        Set(value As Double)
            _XOffset = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.XOffset.ToString), TextBox).Text = CStr(value)
            End If
        End Set
    End Property

    Private _YOffset As Double
    Public Property YOffset As Double
        Get
            Return _YOffset
        End Get
        Set(value As Double)
            _YOffset = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.YOffset.ToString), TextBox).Text = CStr(value)
            End If
        End Set
    End Property

    Private _OverwriteExisting As Boolean
    Public Property OverwriteExisting As Boolean
        Get
            Return _OverwriteExisting
        End Get
        Set(value As Boolean)
            _OverwriteExisting = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.OverwriteExisting.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _SaveDraft As Boolean
    Public Property SaveDraft As Boolean
        Get
            Return _SaveDraft
        End Get
        Set(value As Boolean)
            _SaveDraft = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.SaveDraft.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _SaveInOriginalDirectoryDraft As Boolean
    Public Property SaveInOriginalDirectoryDraft As Boolean
        Get
            Return _SaveInOriginalDirectoryDraft
        End Get
        Set(value As Boolean)
            _SaveInOriginalDirectoryDraft = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.SaveInOriginalDirectoryDraft.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _NewDirDraft As String
    Public Property NewDirDraft As String
        Get
            Return _NewDirDraft
        End Get
        Set(value As String)
            _NewDirDraft = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.NewDirDraft.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _SavePDF As Boolean
    Public Property SavePDF As Boolean
        Get
            Return _SavePDF
        End Get
        Set(value As Boolean)
            _SavePDF = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.SavePDF.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _SaveInOriginalDirectoryPDF As Boolean
    Public Property SaveInOriginalDirectoryPDF As Boolean
        Get
            Return _SaveInOriginalDirectoryPDF
        End Get
        Set(value As Boolean)
            _SaveInOriginalDirectoryPDF = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.SaveInOriginalDirectoryPDF.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _NewDirPDF As String
    Public Property NewDirPDF As String
        Get
            Return _NewDirPDF
        End Get
        Set(value As String)
            _NewDirPDF = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.NewDirPDF.ToString), TextBox).Text = value
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

    Public Property OffsetUnits As String



    Enum ControlNames
        Browse
        DraftTemplate
        ScaleFactor
        ScaleFactorLabel
        OffsetUnitsIn
        OffsetUnitsMM
        XOffset
        XOffsetLabel
        YOffset
        YOffsetLabel
        OverwriteExisting
        SaveDraft
        SaveInOriginalDirectoryDraft
        BrowseNewDirDraft
        NewDirDraft
        SavePDF
        SaveInOriginalDirectoryPDF
        BrowseNewDirPDF
        NewDirPDF
        AutoHideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = False
        Me.AppliesToAssembly = False
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = False
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskSaveAs
        Me.Category = "Output"
        Me.RequiresDraftTemplate = True
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.DraftTemplate = ""
        Me.ScaleFactor = 1
        Me.OffsetUnits = ""
        Me.XOffset = 0
        Me.YOffset = 0
        Me.OverwriteExisting = False
        Me.SaveDraft = False
        Me.SaveInOriginalDirectoryDraft = False
        Me.NewDirDraft = ""
        Me.SavePDF = False
        Me.SaveInOriginalDirectoryPDF = False
        Me.NewDirPDF = ""

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

        Dim FlatPatternModels As SolidEdgePart.FlatPatternModels = Nothing
        Dim FlatPatternModel As SolidEdgePart.FlatPatternModel
        Dim FlatPattern As SolidEdgePart.FlatPattern

        Dim UC As New UtilsCommon

        Select Case UC.GetDocType(SEDoc)
            Case "par"
                Dim tmpSEDoc As SolidEdgePart.PartDocument = CType(SEDoc, SolidEdgePart.PartDocument)
                FlatPatternModels = tmpSEDoc.FlatPatternModels

            Case "psm"
                Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                FlatPatternModels = tmpSEDoc.FlatPatternModels

        End Select

        If FlatPatternModels.Count = 0 Then
            ExitStatus = 1
            ErrorMessageList.Add("No flat patterns detected")
        End If

        If ExitStatus = 0 Then
            FlatPatternModel = FlatPatternModels.Item(1)
            FlatPattern = FlatPatternModel.FlatPatterns.Item(1)

            Dim Documents As SolidEdgeFramework.Documents = SEApp.Documents
            Dim DraftDoc As SolidEdgeDraft.DraftDocument
            DraftDoc = CType(Documents.Add("SolidEdge.DraftDocument", Me.DraftTemplate), SolidEdgeDraft.DraftDocument)
            SEApp.DoIdle()

            Dim ModelLinks As SolidEdgeDraft.ModelLinks = DraftDoc.ModelLinks
            Dim ModelLink As SolidEdgeDraft.ModelLink = ModelLinks.Add(SEDoc.FullName)
            SEApp.DoIdle()

            Dim Sheets As List(Of SolidEdgeDraft.Sheet) = UC.GetSheets(DraftDoc, "Working")
            Dim Sheet As SolidEdgeDraft.Sheet = Sheets(0)

            Dim DrawingViews As SolidEdgeDraft.DrawingViews = Sheet.DrawingViews
            Dim TV = SolidEdgeDraft.ViewOrientationConstants.igTopView
            Dim FV = SolidEdgeDraft.SheetMetalDrawingViewTypeConstants.seSheetMetalFlatView
            Dim DrawingView As SolidEdgeDraft.DrawingView = DrawingViews.AddSheetMetalView(ModelLink, TV, 1.0, 0.0, 0.0, FV)

            SupplementalErrorMessage = FormatDrawingView(Sheet, DrawingView)
            AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)

            Dim DrawingFilename As String = ""
            Dim BaseName = System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName) ' C:\project\part.par -> part
            Dim OldDir = System.IO.Path.GetDirectoryName(SEDoc.FullName)  ' C:\project\part.par -> C:\project
            Dim Extension As String = ""

            If Me.SaveDraft Then
                Extension = ".dft"
                If Me.SaveInOriginalDirectoryDraft Then
                    DrawingFilename = String.Format("{0}\{1}{2}", OldDir, BaseName, Extension)
                    If Me.OverwriteExisting Then
                        DraftDoc.SaveAs(DrawingFilename)
                    Else
                        If Not FileIO.FileSystem.FileExists(DrawingFilename) Then
                            DraftDoc.SaveAs(DrawingFilename)
                        End If
                    End If

                Else
                    DrawingFilename = String.Format("{0}\{1}{2}", Me.NewDirDraft, BaseName, Extension)
                    If Me.OverwriteExisting Then
                        DraftDoc.SaveAs(DrawingFilename)
                    Else
                        If Not FileIO.FileSystem.FileExists(DrawingFilename) Then
                            DraftDoc.SaveAs(DrawingFilename)
                        End If
                    End If
                End If
            End If
            SEApp.DoIdle()

            If Me.SavePDF Then
                Extension = ".pdf"
                If Me.SaveInOriginalDirectoryPDF Then
                    DrawingFilename = String.Format("{0}\{1}{2}", OldDir, BaseName, Extension)
                    If Me.OverwriteExisting Then
                        DraftDoc.SaveAs(DrawingFilename)
                    Else
                        If Not FileIO.FileSystem.FileExists(DrawingFilename) Then
                            DraftDoc.SaveAs(DrawingFilename)
                        End If
                    End If

                Else
                    DrawingFilename = String.Format("{0}\{1}{2}", Me.NewDirPDF, BaseName, Extension)
                    If Me.OverwriteExisting Then
                        DraftDoc.SaveAs(DrawingFilename)
                    Else
                        If Not FileIO.FileSystem.FileExists(DrawingFilename) Then
                            DraftDoc.SaveAs(DrawingFilename)
                        End If
                    End If
                End If
            End If
            SEApp.DoIdle()

            DraftDoc.Close()
            SEApp.DoIdle()

        End If


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function


    Private Function FormatDrawingView(
        ByRef Sheet As SolidEdgeDraft.Sheet,
        ByRef DrawingView As SolidEdgeDraft.DrawingView
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))



        ' MEASURED  (Nomenclature defined in ~\SolidEdgeHousekeeper\reference\create_drawing_of_flat_pattern.dft)

        ' Sheet
        Dim SX, SY As Double
        Dim SheetSetup As SolidEdgeDraft.SheetSetup = Sheet.SheetSetup
        SX = SheetSetup.SheetWidth
        SY = SheetSetup.SheetHeight

        ' Drawing view reported origin
        Dim DVXOR, DVYOR As Double
        DrawingView.GetOrigin(DVXOR, DVYOR)

        ' Drawing view range
        Dim DVXL, DVYL, DVXH, DVYH As Double
        DrawingView.Range(DVXL, DVYL, DVXH, DVYH)

        ' Drawing view margin -- padding between the drawing border and the graphics contents
        Dim M As Double = 0.25 * 25.4 / 1000  ' Need to check ISO.


        ' DERIVED

        ' Rotation needed?
        Dim TallSheet As Boolean = SY > SX
        Dim TallDrawing As Boolean = (DVYH - DVYL) > (DVXH - DVXL)
        Dim RotationNeeded As Boolean = Not (TallSheet = TallDrawing)

        ' Scale
        Dim Scale As Double
        If Not RotationNeeded Then
            If (SY / (DVYH - DVYL)) < (SX / (DVXH - DVXL)) Then  ' Height determines scale
                Scale = (SY - 2 * M) / (DVYH - DVYL - 2 * M)
            Else
                Scale = (SX - 2 * M) / (DVXH - DVXL - 2 * M)
            End If
        Else
            If (SY / (DVXH - DVXL)) < (SX / (DVYH - DVYL)) Then  ' Height after rotation determines scale
                Scale = (SY - 2 * M) / (DVXH - DVXL - 2 * M)
            Else
                Scale = (SX - 2 * M) / (DVYH - DVYL - 2 * M)
            End If
        End If

        ' Drawing view actual origin
        Dim DVXOA, DVYOA As Double
        DVXOA = (DVXH + DVXL) / 2
        DVYOA = (DVYH + DVYL) / 2

        ' Difference between reported and actual drawing view origins
        Dim DX, DY As Double
        DX = DVXOR - DVXOA
        DY = DVYOR - DVYOA

        ' Target origin position on the sheet
        Dim SXT, SYT As Double
        If OffsetUnits = "in" Then
            SXT = (SX / 2) + XOffset * 25.4 / 1000
            SYT = (SY / 2) + YOffset * 25.4 / 1000
        Else
            SXT = (SX / 2) + XOffset / 1000
            SYT = (SY / 2) + YOffset / 1000
        End If

        If (Math.Abs(DX) > 0.001) Or (Math.Abs(DY) > 0.001) Then
            SXT += DX
            SYT += DY
        End If

        ' Set position, scale and rotation

        DrawingView.SetOrigin(SXT, SYT)

        DrawingView.ScaleFactor = Me.ScaleFactor * Scale

        If RotationNeeded Then
            DrawingView.SetRotationAngle(90.0 * Math.PI / 180.0)
        End If


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Button As Button
        Dim TextBox As TextBox
        Dim Label As Label

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

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

        TextBox = FormatOptionsTextBox(ControlNames.ScaleFactor.ToString, "")
        TextBox.Width = 40
        TextBox.TextAlign = HorizontalAlignment.Right
        TextBox.Text = "1"
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        Label = FormatOptionsLabel(ControlNames.ScaleFactorLabel.ToString, "Scale factor (0-1)")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.OffsetUnitsIn.ToString, "Offset units inch")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.OffsetUnitsMM.ToString, "Offset units mm")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.XOffset.ToString, "")
        TextBox.Width = 40
        TextBox.TextAlign = HorizontalAlignment.Right
        TextBox.Text = "0"
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        Label = FormatOptionsLabel(ControlNames.XOffsetLabel.ToString, "X Offset (0 = centered)")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.YOffset.ToString, "")
        TextBox.Width = 40
        TextBox.TextAlign = HorizontalAlignment.Right
        TextBox.Text = "0"
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        Label = FormatOptionsLabel(ControlNames.YOffsetLabel.ToString, "Y Offset (0 = centered)")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.OverwriteExisting.ToString, "Overwrite existing file")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.SaveDraft.ToString, "Save as *.dft")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.SaveInOriginalDirectoryDraft.ToString, "Save in original directory")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.BrowseNewDirDraft.ToString, "Directory")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.NewDirDraft.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        'tmpTLPOptions.SetColumnSpan(TextBox, 2)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.SavePDF.ToString, "Save as *.pdf")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.SaveInOriginalDirectoryPDF.ToString, "Save in original directory")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1
        Button = FormatOptionsButton(ControlNames.BrowseNewDirPDF.ToString, "Directory")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.NewDirPDF.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        'tmpTLPOptions.SetColumnSpan(TextBox, 2)
        ControlsDict(TextBox.Name) = TextBox

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

            If Not (Me.SaveDraft Or Me.SavePDF) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one type of file to create", Indent))
            End If

            If Me.OffsetUnits = "" Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select a offset unit", Indent))
            End If

            Try
                Me.ScaleFactor = CDbl(ControlsDict(ControlNames.ScaleFactor.ToString).Text)
                If Not ((Me.ScaleFactor > 0) And (Me.ScaleFactor <= 1)) Then
                    If Not ErrorMessageList.Contains(Me.Description) Then
                        ErrorMessageList.Add(Me.Description)
                    End If
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("{0}Enter a scale factor between 0 and 1", Indent))
                End If
            Catch ex As Exception
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Enter a valid scale factor", Indent))
            End Try

            Try
                Me.XOffset = CDbl(ControlsDict(ControlNames.XOffset.ToString).Text)
            Catch ex As Exception
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Enter a valid XOffset", Indent))
            End Try

            Try
                Me.YOffset = CDbl(ControlsDict(ControlNames.YOffset.ToString).Text)
            Catch ex As Exception
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Enter a valid YOffset", Indent))
            End Try

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

        Dim ParticipatingCheckBoxes As New List(Of CheckBox)
        ParticipatingCheckBoxes.Add(CType(ControlsDict(ControlNames.OffsetUnitsIn.ToString), CheckBox))
        ParticipatingCheckBoxes.Add(CType(ControlsDict(ControlNames.OffsetUnitsMM.ToString), CheckBox))


        Select Case Name
            Case ControlNames.OverwriteExisting.ToString
                Me.OverwriteExisting = Checkbox.Checked

            Case ControlNames.OffsetUnitsIn.ToString
                Me.OffsetUnitsIn = Checkbox.Checked
                If Checkbox.Checked Then
                    Me.OffsetUnits = "in"
                    HandleMutuallyExclusiveCheckBoxes(TaskOptionsTLP, Checkbox, ParticipatingCheckBoxes)
                End If

            Case ControlNames.OffsetUnitsMM.ToString
                Me.OffsetUnitsMM = Checkbox.Checked
                If Checkbox.Checked Then
                    Me.OffsetUnits = "mm"
                    HandleMutuallyExclusiveCheckBoxes(TaskOptionsTLP, Checkbox, ParticipatingCheckBoxes)
                End If

            Case ControlNames.SaveDraft.ToString
                Me.SaveDraft = Checkbox.Checked

                CType(ControlsDict(ControlNames.SaveInOriginalDirectoryDraft.ToString), CheckBox).Visible = Me.SaveDraft

                Dim tf As Boolean = Me.SaveDraft And Not Me.SaveInOriginalDirectoryDraft
                CType(ControlsDict(ControlNames.BrowseNewDirDraft.ToString), Button).Visible = tf
                CType(ControlsDict(ControlNames.NewDirDraft.ToString), TextBox).Visible = tf

            Case ControlNames.SaveInOriginalDirectoryDraft.ToString
                Me.SaveInOriginalDirectoryDraft = Checkbox.Checked

                CType(ControlsDict(ControlNames.BrowseNewDirDraft.ToString), Button).Visible = Not Me.SaveInOriginalDirectoryDraft
                CType(ControlsDict(ControlNames.NewDirDraft.ToString), TextBox).Visible = Not Me.SaveInOriginalDirectoryDraft

            Case ControlNames.SavePDF.ToString
                Me.SavePDF = Checkbox.Checked

                CType(ControlsDict(ControlNames.SaveInOriginalDirectoryPDF.ToString), CheckBox).Visible = Me.SavePDF

                Dim tf As Boolean = Me.SavePDF And Not Me.SaveInOriginalDirectoryPDF
                CType(ControlsDict(ControlNames.BrowseNewDirPDF.ToString), Button).Visible = tf
                CType(ControlsDict(ControlNames.NewDirPDF.ToString), TextBox).Visible = tf

            Case ControlNames.SaveInOriginalDirectoryPDF.ToString
                Me.SaveInOriginalDirectoryPDF = Checkbox.Checked

                CType(ControlsDict(ControlNames.BrowseNewDirPDF.ToString), Button).Visible = Not Me.SaveInOriginalDirectoryPDF
                CType(ControlsDict(ControlNames.NewDirPDF.ToString), TextBox).Visible = Not Me.SaveInOriginalDirectoryPDF

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

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

            Case ControlNames.BrowseNewDirDraft.ToString
                Dim tmpFolderDialog As New CommonOpenFileDialog
                tmpFolderDialog.IsFolderPicker = True

                If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
                    Me.NewDirDraft = tmpFolderDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.NewDirDraft.ToString), TextBox)
                    TextBox.Text = Me.NewDirDraft

                End If

            Case ControlNames.BrowseNewDirPDF.ToString
                Dim tmpFolderDialog As New CommonOpenFileDialog
                tmpFolderDialog.IsFolderPicker = True

                If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
                    Me.NewDirPDF = tmpFolderDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.NewDirPDF.ToString), TextBox)
                    TextBox.Text = Me.NewDirPDF

                End If

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

            Case ControlNames.ScaleFactor.ToString
                'Try
                '    Me.ScaleFactor = CDbl(TextBox.Text)
                'Catch ex As Exception
                'End Try

            Case ControlNames.XOffset.ToString
                'Try
                '    Me.XOffset = CDbl(TextBox.Text)
                'Catch ex As Exception
                'End Try

            Case ControlNames.YOffset.ToString
                'Try
                '    Me.YOffset = CDbl(TextBox.Text)
                'Catch ex As Exception
                'End Try

            Case ControlNames.NewDirDraft.ToString
                Me.NewDirDraft = TextBox.Text

            Case ControlNames.NewDirPDF.ToString
                Me.NewDirPDF = TextBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub SetDraftTemplate(DraftTemplate As String)
        ' Should update the property as well as the textbox
        CType(ControlsDict(ControlNames.DraftTemplate.ToString), TextBox).Text = DraftTemplate

    End Sub


    'Public Overrides Sub ReconcileFormWithProps()
    '    ControlsDict(ControlNames.DraftTemplate.ToString).Text = Me.DraftTemplate
    'End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Creates a drawing of a flat pattern using the template you specify. "
        HelpString += "If the file does not contain a flat pattern, the command reports an error. "
        HelpString += "It does not check if the flat pattern is up to date. "
        HelpString += "For that, run the `Check flat pattern` and/or `Update flat pattern` commands "
        HelpString += "before running this one. "

        HelpString += vbCrLf + vbCrLf + "![Expression Editor](My%20Project/media/task_create_drawing_of_flat_pattern.png)"

        HelpString += vbCrLf + vbCrLf + "The command attempts to place the flat pattern centered on the sheet. "
        HelpString += "It rotates it if needed to match the available space. "
        HelpString += "It scales it to entirely fill the sheet in one direction. "
        HelpString += "That's not the scale you ultimately want, but is the starting point for the `Scale factor`. "
        HelpString += "With that you control the final size. "
        HelpString += "If you want it to take up 90% of the available space, "
        HelpString += "enter `0.9`. For half size, enter `0.5`, etc. "
        HelpString += "X and Y offset will offset the drawing by the specified values from center, "
        HelpString += "+Y will offset up, -Y will offset down. +X will offset right, -X will offset left."

        HelpString += vbCrLf + vbCrLf + "You can save the drawing as a `*.dft` or `*.pdf` or both. "
        HelpString += "If a file with the same name already exists, "
        HelpString += "the `Overwrite existing` checkbox tells the program how to proceed. "
        HelpString += "You can save the drawing to the original directory, or choose another one. "
        HelpString += "Each file type can be saved to a different directory. "

        Return HelpString
    End Function


End Class
