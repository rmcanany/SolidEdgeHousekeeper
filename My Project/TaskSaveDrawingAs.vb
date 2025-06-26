Option Strict On

Imports System.Runtime.InteropServices
Imports Microsoft.WindowsAPICodePack.Dialogs

Public Class TaskSaveDrawingAs

    Inherits Task

    Private _NewFileTypeName As String
    Public Property NewFileTypeName As String
        Get
            Return _NewFileTypeName
        End Get
        Set(value As String)
            _NewFileTypeName = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.NewFileTypeName.ToString), ComboBox).Text = value
            End If
        End Set
    End Property

    Private _ChangeFilename As Boolean
    Public Property ChangeFilename As Boolean
        Get
            Return _ChangeFilename
        End Get
        Set(value As Boolean)
            _ChangeFilename = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ChangeFilename.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _FilenameFormula As String
    Public Property FilenameFormula As String
        Get
            Return _FilenameFormula
        End Get
        Set(value As String)
            _FilenameFormula = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.FilenameFormula.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _SaveInOriginalDirectory As Boolean
    Public Property SaveInOriginalDirectory As Boolean
        Get
            Return _SaveInOriginalDirectory
        End Get
        Set(value As Boolean)
            _SaveInOriginalDirectory = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.SaveInOriginalDirectory.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _NewDir As String
    Public Property NewDir As String
        Get
            Return _NewDir
        End Get
        Set(value As String)
            _NewDir = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.NewDir.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _UseSubdirectoryFormula As Boolean
    Public Property UseSubdirectoryFormula As Boolean
        Get
            Return _UseSubdirectoryFormula
        End Get
        Set(value As Boolean)
            _UseSubdirectoryFormula = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UseSubdirectoryFormula.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _Formula As String
    Public Property Formula As String
        Get
            Return _Formula
        End Get
        Set(value As String)
            _Formula = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.Formula.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _AddWatermark As Boolean
    Public Property AddWatermark As Boolean
        Get
            Return _AddWatermark
        End Get
        Set(value As Boolean)
            _AddWatermark = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AddWatermark.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _WatermarkFilename As String
    Public Property WatermarkFilename As String
        Get
            Return _WatermarkFilename
        End Get
        Set(value As String)
            _WatermarkFilename = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.WatermarkFilename.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _WatermarkPositionX As Double
    Public Property WatermarkPositionX As Double
        Get
            Return _WatermarkPositionX
        End Get
        Set(value As Double)
            _WatermarkPositionX = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.WatermarkPositionX.ToString), TextBox).Text = CStr(value)
            End If
        End Set
    End Property

    Private _WatermarkPositionY As Double
    Public Property WatermarkPositionY As Double
        Get
            Return _WatermarkPositionY
        End Get
        Set(value As Double)
            _WatermarkPositionY = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.WatermarkPositionY.ToString), TextBox).Text = CStr(value)
            End If
        End Set
    End Property

    Private _WatermarkScale As Double
    Public Property WatermarkScale As Double
        Get
            Return _WatermarkScale
        End Get
        Set(value As Double)
            _WatermarkScale = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.WatermarkScale.ToString), TextBox).Text = CStr(value)
            End If
        End Set
    End Property

    Private _PDFPerSheetSuppressSheetname As Boolean
    Public Property PDFPerSheetSuppressSheetname As Boolean
        Get
            Return _PDFPerSheetSuppressSheetname
        End Get
        Set(value As Boolean)
            _PDFPerSheetSuppressSheetname = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.PDFPerSheetSuppressSheetname.ToString), CheckBox).Checked = value
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

    Public Property PDFPerSheetFileTypeName As String


    Enum ControlNames
        NewFileTypeName
        NewFileTypeLabel
        ChangeFilename
        FilenameFormula
        SaveInOriginalDirectory
        BrowseNewDir
        NewDir
        UseSubdirectoryFormula
        Formula
        PDFPerSheetSuppressSheetname
        AddWatermark
        BrowseWatermarkFilename
        WatermarkFilename
        WatermarkScale
        WatermarkScaleLabel
        WatermarkPositionX
        WatermarkPositionXLabel
        WatermarkPositionY
        WatermarkPositionYLabel
        AutoHideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = False
        Me.AppliesToAssembly = False
        Me.AppliesToPart = False
        Me.AppliesToSheetmetal = False
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskSaveAs
        Me.Category = "Output"
        Me.RequiresPropertiesData = True
        SetColorFromCategory(Me)
        Me.CompatibleWithInactiveDraft = True

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.NewFileTypeName = ""
        Me.ChangeFilename = False
        Me.FilenameFormula = ""
        Me.SaveInOriginalDirectory = False
        Me.NewDir = ""
        Me.UseSubdirectoryFormula = False
        Me.Formula = ""
        Me.AddWatermark = False
        Me.WatermarkFilename = ""
        Me.WatermarkPositionX = 0
        Me.WatermarkPositionY = 0
        Me.WatermarkScale = 1
        Me.PDFPerSheetSuppressSheetname = False

        Me.PropertiesData = New HCPropertiesData
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

        Dim ImageExtensions As List(Of String) = {".bmp", ".jpg", ".png", ".tif"}.ToList

        Dim ExitMessage As String = ""

        Dim NewFilename As String = ""
        Dim NewExtension As String = ""
        Dim NewFileFormat As String = ""
        Dim Filename As String = SEDoc.FullName

        Dim Proceed As Boolean = True

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        If Not Me.NewFileTypeName.ToLower.Contains("copy") Then
            NewExtension = Me.NewFileTypeName
            NewExtension = NewExtension.Split("*"c)(1)  ' "Parasolid text (*.xt)" -> ".xt)"
            NewExtension = NewExtension.Split(")"c)(0)  ' "Parasolid text (*.xt)" -> ".xt"
        Else
            NewExtension = String.Format(".{0}", DocType)
        End If

        NewFileFormat = Me.NewFileTypeName
        NewFileFormat = NewFileFormat.Split("("c)(0)  ' "Parasolid text (*.xt)" -> "Parasolid text "

        Select Case DocType

            Case = "dft"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

                NewFilename = GenerateNewFilename(SEDoc, NewExtension)  ' Updates TaskLogger

                If Not TaskLogger.HasErrors Then
                    FileIO.FileSystem.CreateDirectory(System.IO.Path.GetDirectoryName(NewFilename))

                    If Me.AddWatermark Then
                        AddWatermarkToSheets(tmpSEDoc, NewFilename, SEApp)
                    End If

                    SaveAsDrawing(tmpSEDoc, NewFilename, SEApp)
                End If

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        End Select

    End Sub


    Private Function GenerateNewFilename(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        NewExtension As String,
        Optional Suffix As String = ""
        ) As String

        ' Example conversions
        ' NewExtension: ".stp"
        ' Suffix: Not supplied
        ' "C:\Projects\part.par" -> "C:\Projects\part.stp"

        ' NewExtension: ".png"
        ' Suffix: "Member1"
        ' "C:\Projects\assembly.asm!Master" -> "C:\Projects\assembly-Member1.png"

        Dim UC As New UtilsCommon
        Dim UFC As New UtilsFilenameCharmap()

        Dim Success As Boolean = True

        Dim OldFullFilename As String = UC.GetFOAFilename(SEDoc.FullName)   ' "C:\Projects\part.par", "C:\Projects\assembly.asm!Master"

        Dim OldDirectoryName As String = System.IO.Path.GetDirectoryName(OldFullFilename)             ' "C:\Projects"
        Dim OldFilenameWOExt As String = System.IO.Path.GetFileNameWithoutExtension(OldFullFilename)  ' "part"
        Dim OldExtension As String = IO.Path.GetExtension(OldFullFilename)                            ' ".par"

        Dim NewFullFilename As String

        Dim NewDirectoryName As String
        Dim NewSubDirectoryName As String = ""
        Dim NewFilenameWOExt As String = ""
        Dim NewSuffix As String


        ' ###### ROOT DIRECTORY ######
        If Me.SaveInOriginalDirectory Then
            NewDirectoryName = String.Format("{0}\", OldDirectoryName)
        Else
            NewDirectoryName = Me.NewDir
            If Not NewDirectoryName(Len(NewDirectoryName) - 1) = "\" Then
                NewDirectoryName = String.Format("{0}\", NewDirectoryName)
            End If
        End If


        ' ###### SUBDIRECTORY ######
        If Not Me.UseSubdirectoryFormula Then
            NewSubDirectoryName = ""
        Else


            If Me.Formula.StartsWith("EX:") Then

                NewSubDirectoryName = UC.SubstitutePropertyFormula(SEDoc, SEDoc.FullName, Me.Formula.Replace("EX:", ""), Me.PropertiesData, True)
                'NewSubDirectoryName = NewSubDirectoryName.Replace("/", "\")

                If NewSubDirectoryName Is Nothing Then
                    Success = False

                    Me.TaskLogger.AddMessage(String.Format("Could not parse subdirectory formula '{0}'", Me.Formula))
                Else
                    Dim DoNotSubstituteChars As New List(Of String)
                    DoNotSubstituteChars.Add("\")
                    UFC.SubstituteIllegalCharacters(NewSubDirectoryName, DoNotSubstituteChars)

                End If

            Else

                NewSubDirectoryName = UC.SubstitutePropertyFormula(SEDoc, SEDoc.FullName, Me.Formula, Me.PropertiesData)

                If NewSubDirectoryName Is Nothing Then
                    Success = False

                    Me.TaskLogger.AddMessage(String.Format("Could not parse subdirectory formula '{0}'", Me.Formula))
                Else
                    Dim DoNotSubstituteChars As New List(Of String)
                    DoNotSubstituteChars.Add("\")
                    UFC.SubstituteIllegalCharacters(NewSubDirectoryName, DoNotSubstituteChars)

                End If



            End If

            If Success Then
                If Not NewSubDirectoryName(Len(NewSubDirectoryName) - 1) = "\" Then
                    NewSubDirectoryName = String.Format("{0}\", NewSubDirectoryName)
                End If
            End If
        End If


        ' ###### BASE FILENAME ######
        If Not Me.ChangeFilename Then
            NewFilenameWOExt = OldFilenameWOExt
        Else
            NewFilenameWOExt = UC.SubstitutePropertyFormula(SEDoc, SEDoc.FullName, Me.FilenameFormula, Me.PropertiesData)

            If NewFilenameWOExt Is Nothing Then
                Success = False

                Me.TaskLogger.AddMessage(String.Format("Could not parse filename formula '{0}'", Me.FilenameFormula))
            Else
                Dim DoNotSubstituteChars As New List(Of String)
                DoNotSubstituteChars.Add("\")
                UFC.SubstituteIllegalCharacters(NewSubDirectoryName, DoNotSubstituteChars)
            End If
        End If


        ' ###### SUFFIX ######
        If Suffix = "" Then
            NewSuffix = Suffix
        Else
            NewSuffix = String.Format("-{0}", Suffix)
        End If


        ' ###### FULL FILENAME ######
        If Success Then
            NewFullFilename = String.Format("{0}{1}{2}{3}{4}", NewDirectoryName, NewSubDirectoryName, NewFilenameWOExt, NewSuffix, NewExtension)
        Else
            NewFullFilename = ""
        End If


        Return NewFullFilename

    End Function

    Private Sub AddWatermarkToSheets(
        SEDoc As SolidEdgeDraft.DraftDocument,
        NewFilename As String,
        SEApp As SolidEdgeFramework.Application
        )

        Dim Sections As SolidEdgeDraft.Sections
        Dim Section As SolidEdgeDraft.Section
        Dim SectionSheets As SolidEdgeDraft.SectionSheets
        Dim OriginalSheet As SolidEdgeDraft.Sheet
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim Watermark As SolidEdgeFrameworkSupport.Image2d
        Dim SheetWindow As SolidEdgeDraft.SheetWindow

        Dim ImageX As Double
        Dim ImageY As Double
        Dim SheetW As Double
        Dim SheetH As Double


        OriginalSheet = SEDoc.ActiveSheet
        Sections = SEDoc.Sections
        Section = Sections.BackgroundSection
        SectionSheets = Section.Sheets

        For Each Sheet In SectionSheets
            Try
                Sheet.Activate()
                SheetW = Sheet.SheetSetup.SheetWidth
                SheetH = Sheet.SheetSetup.SheetHeight
                Watermark = Sheet.Images2d.AddImage(False, Me.WatermarkFilename)
                Watermark.Height = Me.WatermarkScale * Watermark.Height
                Watermark.ShowBorder = False
                ImageX = Me.WatermarkPositionX * SheetW - Watermark.Width / 2
                ImageY = Me.WatermarkPositionY * SheetH - Watermark.Height / 2
                Watermark.SetOrigin(ImageX, ImageY)

                SEApp.DoIdle()
            Catch ex As Exception
                Me.TaskLogger.AddMessage(String.Format("Unable to add watermark to sheet '{0}'", Sheet.Name))

            End Try
        Next

        OriginalSheet.Activate()
        SEApp.DoIdle()

        SheetWindow = CType(SEApp.ActiveWindow, SolidEdgeDraft.SheetWindow)
        SheetWindow.DisplayBackgroundSheetTabs = False
        SEApp.DoIdle()

    End Sub

    Private Sub SaveAsDrawing(
        SEDoc As SolidEdgeDraft.DraftDocument,
        NewFilename As String,
        SEApp As SolidEdgeFramework.Application
        )

        Dim SaveAsPDFOptions As SolidEdgeFramework.ApplicationGlobalConstants
        SaveAsPDFOptions = SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalDraftSaveAsPDFSheetOptions

        Try
            If Not Me.NewFileTypeName.ToLower.Contains("copy") Then
                If Not Me.NewFileTypeName.ToLower.Contains("pdf per sheet") Then
                    SEDoc.SaveAs(NewFilename)
                    SEApp.DoIdle()

                Else  ' Save as one pdf file per sheet

                    Dim PreviousSetting As Object = Nothing
                    Dim SheetList As New List(Of SolidEdgeDraft.Sheet)
                    Dim Sheet As SolidEdgeDraft.Sheet
                    Dim SheetName As String
                    Dim tmpNewFilename As String

                    ' seApplicationGlobalDraftSaveAsPDFSheetOptions (same mapping as SolidEdgeConstants.DraftSaveAsPDFSheetOptionsConstants)
                    ' 0: Active sheet only
                    ' 1: All sheets
                    ' 2: Sheets: (sheet number and/or ranges)

                    SEApp.GetGlobalParameter(SaveAsPDFOptions, PreviousSetting)
                    SEApp.SetGlobalParameter(SaveAsPDFOptions, 0)

                    Dim UC As New UtilsCommon
                    SheetList = UC.GetSheets(SEDoc, "Working")

                    tmpNewFilename = NewFilename

                    Dim UFC As New UtilsFilenameCharmap

                    For Each Sheet In SheetList
                        Sheet.Activate()

                        SheetName = String.Format("-{0}", Sheet.Name)
                        SheetName = UFC.SubstituteIllegalCharacters(SheetName, New List(Of String))
                        If Me.PDFPerSheetSuppressSheetname Then
                            If SheetList.Count = 1 Then
                                SheetName = ""
                            End If

                        End If

                        NewFilename = tmpNewFilename.Substring(0, tmpNewFilename.Count - 4)
                        NewFilename = String.Format("{0}{1}.pdf", NewFilename, SheetName)
                        SEDoc.SaveAs(NewFilename)
                        SEApp.DoIdle()

                    Next

                    SEApp.SetGlobalParameter(SaveAsPDFOptions, PreviousSetting)

                End If
            Else
                Try
                    SEDoc.SaveAs(NewFilename)
                    SEApp.DoIdle()
                Catch ex As Exception
                    Me.TaskLogger.AddMessage(String.Format("Could not save '{0}'", NewFilename))
                End Try

            End If

        Catch ex As Exception
            Me.TaskLogger.AddMessage(String.Format("Error saving file {0}", NewFilename))

        End Try

    End Sub

    Private Function GetNewFileTypeNames() As List(Of String)
        Dim NewFileTypeNames As New List(Of String)
        '        FileTypesString = "PDF (*.pdf):PDF per Sheet (*.pdf):DXF (*.dxf):DWG (*.dwg):IGES (*.igs)"

        NewFileTypeNames.Add("")
        NewFileTypeNames.Add("PDF (*.pdf)")
        NewFileTypeNames.Add("PDF per Sheet (*.pdf)")
        NewFileTypeNames.Add("DXF (*.dxf)")
        NewFileTypeNames.Add("DWG (*.dwg)")
        NewFileTypeNames.Add("IGES (*.igs)")
        NewFileTypeNames.Add("SEV (*.sev)")
        NewFileTypeNames.Add("Copy (*.*)")

        PDFPerSheetFileTypeName = "PDF per Sheet (*.pdf)"

        Return NewFileTypeNames
    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim ComboBox As ComboBox
        Dim ComboBoxItems As List(Of String) = GetNewFileTypeNames()
        Dim TextBox As TextBox
        Dim Label As Label
        Dim Button As Button
        Dim ControlWidth As Integer = 150
        Dim NewFileTypeLabelText = ""

        FormatTLPOptionsEx(tmpTLPOptions, "TLPOptions", 12, 75, 75)

        RowIndex = 0

        ComboBox = FormatOptionsComboBox(ControlNames.NewFileTypeName.ToString, ComboBoxItems, "DropDownList")
        ComboBox.Anchor = CType(AnchorStyles.Left + AnchorStyles.Right, AnchorStyles)
        AddHandler ComboBox.SelectedIndexChanged, AddressOf ComboBoxOptions_SelectedIndexChanged
        tmpTLPOptions.Controls.Add(ComboBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(ComboBox, 2)
        ControlsDict(ComboBox.Name) = ComboBox

        Label = FormatOptionsLabel(ControlNames.NewFileTypeLabel.ToString, NewFileTypeLabelText)
        tmpTLPOptions.Controls.Add(Label, 2, RowIndex)
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.PDFPerSheetSuppressSheetname.ToString, "Suppress sheet suffix on 1-page drawings")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        CheckBox.Visible = False
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ChangeFilename.ToString, "Change filename")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.FilenameFormula.ToString, "")
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_TextChanged
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(TextBox, 3)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.SaveInOriginalDirectory.ToString, "Save in original directory")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.BrowseNewDir.ToString, "Directory")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.NewDir.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_TextChanged
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        tmpTLPOptions.SetColumnSpan(TextBox, 2)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UseSubdirectoryFormula.ToString, "Use subdirectory formula")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.Formula.ToString, "")
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_TextChanged
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(TextBox, 3)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AddWatermark.ToString, "Add watermark to drawing")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.BrowseWatermarkFilename.ToString, "File")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        Button.Visible = False
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.WatermarkFilename.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_TextChanged
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        tmpTLPOptions.SetColumnSpan(TextBox, 2)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.WatermarkScale.ToString, "")
        TextBox.Text = "1.0"
        TextBox.TextAlign = HorizontalAlignment.Right
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_TextChanged
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

        Label = FormatOptionsLabel(ControlNames.WatermarkScaleLabel.ToString, "Scale")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        tmpTLPOptions.SetColumnSpan(Label, 2)
        Label.Visible = False
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.WatermarkPositionX.ToString, "")
        TextBox.Text = "0.5"
        TextBox.TextAlign = HorizontalAlignment.Right
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_TextChanged
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

        Label = FormatOptionsLabel(ControlNames.WatermarkPositionXLabel.ToString, "Position X/W")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        tmpTLPOptions.SetColumnSpan(Label, 2)
        Label.Visible = False
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.WatermarkPositionY.ToString, "")
        TextBox.Text = "0.5"
        TextBox.TextAlign = HorizontalAlignment.Right
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_TextChanged
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

        Label = FormatOptionsLabel(ControlNames.WatermarkPositionYLabel.ToString, "Position Y/H")
        tmpTLPOptions.Controls.Add(Label, 1, RowIndex)
        tmpTLPOptions.SetColumnSpan(Label, 2)
        Label.Visible = False
        ControlsDict(Label.Name) = Label

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        ControlsDict(CheckBox.Name) = CheckBox


        Return tmpTLPOptions
    End Function

    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        If Me.IsSelectedTask Then

            Dim UC As New UtilsCommon

            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If

            If Me.NewFileTypeName = "" Then
                ErrorLogger.AddMessage("Output file type not detected")
            End If

            If (Me.NewFileTypeName.ToLower.Contains("copy")) And (Me.SaveInOriginalDirectory) And (Not Me.UseSubdirectoryFormula) Then
                ErrorLogger.AddMessage("Cannot save copy to the original directory")
            End If

            If (Me.NewDir = "") And (Not Me.SaveInOriginalDirectory) Then
                ErrorLogger.AddMessage("Select a valid output directory")
            End If

            If (Me.Formula = "") And (Me.UseSubdirectoryFormula) Then
                ErrorLogger.AddMessage("Enter a subdirectory formula")
            End If

            If Not UC.CheckValidPropertyFormulas(Me.Formula) And (Me.UseSubdirectoryFormula) Then
                ErrorLogger.AddMessage("Subdirectory formula missing 'System.' or 'Custom.'")
            End If

            If (Me.FilenameFormula = "") And (Me.ChangeFilename) Then
                ErrorLogger.AddMessage("Enter a filename formula")
            End If

            If Not UC.CheckValidPropertyFormulas(Me.FilenameFormula) And (Me.ChangeFilename) Then
                ErrorLogger.AddMessage("Filename formula missing 'System.' or 'Custom.'")
            End If

            If Me.AddWatermark Then

                If Not FileIO.FileSystem.FileExists(Me.WatermarkFilename) Then
                    ErrorLogger.AddMessage("Select a valid watermark file")
                End If

                Try
                    Me.WatermarkScale = CDbl(ControlsDict(ControlNames.WatermarkScale.ToString).Text)
                    If Not Me.WatermarkScale > 0 Then
                        ErrorLogger.AddMessage("Enter a number > 0 for watermark scale")
                    End If
                Catch ex As Exception
                    ErrorLogger.AddMessage("Enter a valid number for watermark scale")
                End Try

                Try
                    Me.WatermarkPositionX = CDbl(ControlsDict(ControlNames.WatermarkPositionX.ToString).Text)
                    Me.WatermarkPositionY = CDbl(ControlsDict(ControlNames.WatermarkPositionY.ToString).Text)
                Catch ex As Exception
                    ErrorLogger.AddMessage("Enter valid numbers for watermark X and Y positions")
                End Try

            End If
        End If

    End Sub


    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        Dim TextBox As TextBox

        Select Case Name
            Case ControlNames.BrowseNewDir.ToString
                Dim tmpFolderDialog As New CommonOpenFileDialog
                tmpFolderDialog.IsFolderPicker = True

                If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
                    Me.NewDir = tmpFolderDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.NewDir.ToString), TextBox)
                    TextBox.Text = Me.NewDir

                End If

            Case ControlNames.BrowseWatermarkFilename.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select an image file"
                tmpFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff"

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.WatermarkFilename = tmpFileDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.WatermarkFilename.ToString), TextBox)
                    TextBox.Text = Me.WatermarkFilename

                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim CheckBox = CType(sender, CheckBox)
        Dim Name = CheckBox.Name

        Select Case Name
            Case ControlNames.ChangeFilename.ToString
                Me.ChangeFilename = CheckBox.Checked

                CType(ControlsDict(ControlNames.FilenameFormula.ToString), TextBox).Visible = Me.ChangeFilename

            Case ControlNames.SaveInOriginalDirectory.ToString
                Me.SaveInOriginalDirectory = CheckBox.Checked

                CType(ControlsDict(ControlNames.BrowseNewDir.ToString), Button).Visible = Not Me.SaveInOriginalDirectory
                CType(ControlsDict(ControlNames.NewDir.ToString), TextBox).Visible = Not Me.SaveInOriginalDirectory

            Case ControlNames.UseSubdirectoryFormula.ToString
                Me.UseSubdirectoryFormula = CheckBox.Checked

                CType(ControlsDict(ControlNames.Formula.ToString), TextBox).Visible = Me.UseSubdirectoryFormula

            Case ControlNames.PDFPerSheetSuppressSheetname.ToString
                Me.PDFPerSheetSuppressSheetname = CheckBox.Checked

            Case ControlNames.AddWatermark.ToString
                Me.AddWatermark = CheckBox.Checked

                CType(ControlsDict(ControlNames.BrowseWatermarkFilename.ToString), Button).Visible = Me.AddWatermark
                CType(ControlsDict(ControlNames.WatermarkFilename.ToString), TextBox).Visible = Me.AddWatermark
                CType(ControlsDict(ControlNames.WatermarkScale.ToString), TextBox).Visible = Me.AddWatermark
                CType(ControlsDict(ControlNames.WatermarkScaleLabel.ToString), Label).Visible = Me.AddWatermark
                CType(ControlsDict(ControlNames.WatermarkPositionX.ToString), TextBox).Visible = Me.AddWatermark
                CType(ControlsDict(ControlNames.WatermarkPositionXLabel.ToString), Label).Visible = Me.AddWatermark
                CType(ControlsDict(ControlNames.WatermarkPositionY.ToString), TextBox).Visible = Me.AddWatermark
                CType(ControlsDict(ControlNames.WatermarkPositionYLabel.ToString), Label).Visible = Me.AddWatermark

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = CheckBox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = CheckBox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub ComboBoxOptions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Dim ComboBox = CType(sender, ComboBox)
        Dim Name = ComboBox.Name

        Select Case Name
            Case ControlNames.NewFileTypeName.ToString
                Me.NewFileTypeName = ComboBox.Text

                If Me.NewFileTypeName = PDFPerSheetFileTypeName Then
                    CType(ControlsDict(ControlNames.PDFPerSheetSuppressSheetname.ToString), CheckBox).Visible = True

                Else
                    CType(ControlsDict(ControlNames.PDFPerSheetSuppressSheetname.ToString), CheckBox).Visible = False
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub TextBoxOptions_TextChanged(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name
        Dim x As Double

        Select Case Name

            Case ControlNames.NewFileTypeName.ToString
                Me.NewFileTypeName = TextBox.Text

            Case ControlNames.FilenameFormula.ToString
                Me.FilenameFormula = TextBox.Text

            Case ControlNames.NewDir.ToString
                Me.NewDir = TextBox.Text

            Case ControlNames.Formula.ToString
                Me.Formula = TextBox.Text

            Case ControlNames.WatermarkFilename.ToString
                Me.WatermarkFilename = TextBox.Text

            Case ControlNames.WatermarkScale.ToString
                Try
                    x = CDbl(TextBox.Text)
                    Me.WatermarkScale = x
                Catch ex As Exception
                End Try

            Case ControlNames.WatermarkPositionX.ToString
                Try
                    x = CDbl(TextBox.Text)
                    Me.WatermarkPositionX = x
                Catch ex As Exception
                End Try

            Case ControlNames.WatermarkPositionY.ToString
                Try
                    x = CDbl(TextBox.Text)
                    Me.WatermarkPositionY = x
                Catch ex As Exception
                End Try

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Exports the file to either a non-Solid Edge format, or the same format in a different directory. "

        HelpString += vbCrLf + vbCrLf + "![SaveDrawingAs](My%20Project/media/task_save_drawing_as.png)"

        HelpString += vbCrLf + vbCrLf + "Most options for this command are the same as for `Save Model As`. "
        HelpString += "See the help topic for that command for details. "

        HelpString += vbCrLf + vbCrLf + "Unlike with model files, draft subdirectory formulas can include an Index Reference designator (eg, `|R1`). "
        HelpString += "This is the way to refer to a model contained in the draft file, "
        HelpString += "similar to Property Text in a Callout. "
        HelpString += "For example, `%{System.Material|R1}`. "
        HelpString += "To refer to properties of the draft file itself, do not specify a designator, "
        HelpString += "for example, `%{Custom.Last Revision Date}`. "

        HelpString += vbCrLf + vbCrLf + "You can "
        HelpString += "optionally include a watermark image on the drawing output file.  For the watermark, "
        HelpString += "set X/W and Y/H to position the image, and Scale to change its size. "
        HelpString += "The X/W and Y/H values are fractions of the sheet's "
        HelpString += "width and height, respectively. "
        HelpString += "So, (`0,0`) means lower left, (`0.5,0.5`) means centered, etc. "
        HelpString += "Note some file formats may not support bitmap output."

        HelpString += vbCrLf + vbCrLf + "When creating PDF files, there are two options, `PDF` and `PDF per Sheet`. "
        HelpString += "The first saves all sheets to one file.  The second saves each sheet to a separate file, "
        HelpString += "using the format `<Filename>-<Sheetname>.pdf`.  You can optionally suppress the `Sheetname` suffix "
        HelpString += "on files with only one sheet.  The option is called `Suppress sheet suffix on 1-page drawings`.  "
        HelpString += "To save sheets to separate `dxf` or `dwg` files, set the Save As Options in Solid Edge for those file types "
        HelpString += "before running this command. "


        Return HelpString
    End Function


End Class
