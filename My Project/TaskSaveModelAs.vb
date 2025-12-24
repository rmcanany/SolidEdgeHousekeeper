Option Strict On

Imports Microsoft.WindowsAPICodePack.Dialogs
Imports SolidEdgePart

Public Class TaskSaveModelAs

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
            _FilenameFormula = value.Replace(Chr(182), vbCrLf)
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.FilenameFormula.ToString), TextBox).Text = value.Replace(vbCrLf, Chr(182))
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
            _Formula = value.Replace(Chr(182), vbCrLf)
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.Formula.ToString), TextBox).Text = value.Replace(vbCrLf, Chr(182))
            End If
        End Set
    End Property

    Private _CropImage As Boolean
    Public Property CropImage As Boolean
        Get
            Return _CropImage
        End Get
        Set(value As Boolean)
            _CropImage = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.CropImage.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _HideConstructions As Boolean
    Public Property HideConstructions As Boolean
        Get
            Return _HideConstructions
        End Get
        Set(value As Boolean)
            _HideConstructions = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.HideConstructions.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _FitView As Boolean
    Public Property FitView As Boolean
        Get
            Return _FitView
        End Get
        Set(value As Boolean)
            _FitView = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.FitView.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _Isometric As Boolean
    Public Property Isometric As Boolean
        Get
            Return _Isometric
        End Get
        Set(value As Boolean)
            _Isometric = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.Isometric.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _Dimetric As Boolean
    Public Property Dimetric As Boolean
        Get
            Return _Dimetric
        End Get
        Set(value As Boolean)
            _Dimetric = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.Dimetric.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _Trimetric As Boolean
    Public Property Trimetric As Boolean
        Get
            Return _Trimetric
        End Get
        Set(value As Boolean)
            _Trimetric = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.Trimetric.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _ChangeViewStyle As Boolean
    Public Property ChangeViewStyle As Boolean
        Get
            Return _ChangeViewStyle
        End Get
        Set(value As Boolean)
            _ChangeViewStyle = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ChangeViewStyle.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _ViewStyleName As String
    Public Property ViewStyleName As String
        Get
            Return _ViewStyleName
        End Get
        Set(value As String)
            _ViewStyleName = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ViewStyleName.ToString), TextBox).Text = value
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

    Public Property ImageFileTypeNames As New List(Of String)


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
        CropImage
        HideConstructions
        FitView
        Isometric
        Dimetric
        Trimetric
        ChangeViewStyle
        ViewStyleName
        ViewStyleNameLabel
        AutoHideOptions
    End Enum

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = False
        Me.AppliesToAssembly = True
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = False
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskSaveAs
        Me.Category = "Output"
        Me.RequiresPropertiesData = True
        SetColorFromCategory(Me)
        Me.RequiresForegroundProcessing = True

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
        Me.CropImage = False
        Me.HideConstructions = False
        Me.FitView = False
        Me.Isometric = False
        Me.Dimetric = False
        Me.Trimetric = False
        Me.ChangeViewStyle = False
        Me.ViewStyleName = ""

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

        Dim NewFilename As String = ""
        Dim NewExtension As String = ""
        Dim NewFileFormat As String = ""
        Dim Filename As String = SEDoc.FullName

        Dim Proceed As Boolean = True

        Dim UC As New UtilsCommon
        Dim DocType As String = UC.GetDocType(SEDoc)

        ' Configuration("ComboBoxSaveAsAssemblyFileType") format examples
        ' IGES (*.igs)
        ' Parasolid text (*.x_b)
        ' Copy (*.*)

        Dim IsSaveCopyAs As Boolean = Me.NewFileTypeName.ToLower.Contains("copy")

        If Not IsSaveCopyAs Then
            NewExtension = Me.NewFileTypeName
            NewExtension = NewExtension.Split("*"c)(1)  ' "Parasolid text (*.xt)" -> ".xt)"
            NewExtension = NewExtension.Split(")"c)(0)  ' ".xt)" -> ".xt"
        Else
            NewExtension = String.Format(".{0}", DocType)
        End If

        NewFileFormat = Me.NewFileTypeName
        NewFileFormat = NewFileFormat.Split("("c)(0)  ' "Parasolid text (*.xt)" -> "Parasolid text "

        Select Case DocType
            Case = "asm"
                Dim tmpSEDoc = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

                Dim Members As SolidEdgeAssembly.AssemblyFamilyMembers
                Dim Member As SolidEdgeAssembly.AssemblyFamilyMember
                Dim msg As String = ""

                If (Not tmpSEDoc.IsFileFamilyByDocument) Or (IsSaveCopyAs) Then  ' SaveCopyAs doesn't need a new file for every member

                    NewFilename = GenerateNewFilename(SEDoc, NewExtension)  ' Updates TaskLogger

                    If Not TaskLogger.HasErrors Then
                        FileIO.FileSystem.CreateDirectory(System.IO.Path.GetDirectoryName(NewFilename))

                        Try
                            If Not ImageExtensions.Contains(NewExtension) Then  ' Saving as a model, not an image.
                                SaveAsModel(SEDoc, NewFilename, SEApp)
                            Else  ' Saving as image
                                SaveAsImage(SEDoc, NewFilename, SEApp, NewExtension)
                            End If
                        Catch ex As Exception
                            TaskLogger.AddMessage(String.Format("Error saving {0}", NewFilename))
                        End Try

                    End If

                Else
                    Members = tmpSEDoc.AssemblyFamilyMembers
                    For Each Member In Members
                        Members.ActivateMember(Member.MemberName)

                        NewFilename = GenerateNewFilename(SEDoc, NewExtension, Member.MemberName)

                        If Not TaskLogger.HasErrors Then
                            FileIO.FileSystem.CreateDirectory(System.IO.Path.GetDirectoryName(NewFilename))

                            Try
                                If Not ImageExtensions.Contains(NewExtension) Then  ' Saving as a model, not an image.
                                    SaveAsModel(SEDoc, NewFilename, SEApp)
                                Else  ' Saving as image
                                    SaveAsImage(SEDoc, NewFilename, SEApp, NewExtension)
                                End If
                            Catch ex As Exception
                                TaskLogger.AddMessage(String.Format("Error saving {0}", NewFilename))
                            End Try

                        End If

                    Next
                End If


            'Case = "par"
            '    NewFilename = GenerateNewFilename(SEDoc, NewExtension)

            '    If Not TaskLogger.HasErrors Then
            '        FileIO.FileSystem.CreateDirectory(System.IO.Path.GetDirectoryName(NewFilename))

            '        Try
            '            If Not ImageExtensions.Contains(NewExtension) Then  ' Saving as a model, not an image.
            '                SaveAsModel(SEDoc, NewFilename, SEApp)
            '            Else  ' Saving as image
            '                SaveAsImage(SEDoc, NewFilename, SEApp, NewExtension)
            '            End If
            '        Catch ex As Exception
            '            TaskLogger.AddMessage(String.Format("Error saving {0}", NewFilename))
            '        End Try

            '    End If


            Case = "par", "psm"
                'Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)

                Dim DraftFilename As String
                Dim SEDraftDoc As SolidEdgeDraft.DraftDocument = Nothing

                NewFilename = GenerateNewFilename(SEDoc, NewExtension)

                If Not TaskLogger.HasErrors Then
                    FileIO.FileSystem.CreateDirectory(System.IO.Path.GetDirectoryName(NewFilename))

                    Try
                        If Not ImageExtensions.Contains(NewExtension) Then  ' Saving as a model, not an image.

                            If NewExtension = ".dxf" Then
                                SaveAsFlatDXF(SEDoc, NewFilename, SEApp, DocType)

                            ElseIf NewExtension = ".pdf" Then
                                DraftFilename = System.IO.Path.ChangeExtension(SEDoc.FullName, ".dft")
                                If Not FileIO.FileSystem.FileExists(DraftFilename) Then
                                    TaskLogger.AddMessage(String.Format("Draft document not found '{0}'", DraftFilename))
                                Else
                                    SEDraftDoc = CType(SEApp.Documents.Open(DraftFilename), SolidEdgeDraft.DraftDocument)
                                    SEApp.DoIdle()

                                    SaveAsDrawing(SEDraftDoc, NewFilename, SEApp)
                                End If

                            Else
                                SaveAsModel(SEDoc, NewFilename, SEApp)
                            End If


                        Else  ' Saving as image
                            SaveAsImage(SEDoc, NewFilename, SEApp, NewExtension)
                        End If
                    Catch ex As Exception
                        TaskLogger.AddMessage(String.Format("Error saving {0}", NewFilename))
                    End Try

                    Try
                        If Not SEDraftDoc Is Nothing Then
                            SEDraftDoc.Close(False)
                            SEApp.DoIdle()
                        End If
                    Catch ex As Exception
                    End Try

                End If

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))

        End Select

    End Sub


    Private Function GenerateNewFilename(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        NewExtension As String,
        Optional Suffix As String = "") As String

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
            If Me.Formula.StartsWith("EXPRESSION_") Then
                NewSubDirectoryName = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, Me.Formula, Me.PropertiesData, TaskLogger, True)
                'NewSubDirectoryName = NewSubDirectoryName.Replace("/", "\")

                'If NewSubDirectoryName Is Nothing Then
                '    Success = False
                '    Me.TaskLogger.AddMessage(String.Format("Could not parse subdirectory formula '{0}'", Me.Formula))
                'Else
                '    Dim DoNotSubstituteChars As New List(Of String)
                '    DoNotSubstituteChars.Add("\")
                '    UFC.SubstituteIllegalCharacters(NewSubDirectoryName, DoNotSubstituteChars)
                'End If

                If NewSubDirectoryName Is Nothing OrElse NewSubDirectoryName.Contains("<Nothing>") Then
                    Success = False
                    Me.TaskLogger.AddMessage(String.Format("Could not parse subdirectory formula '{0}'", Me.Formula))
                Else
                    Dim DoNotSubstituteChars As New List(Of String)
                    DoNotSubstituteChars.Add("\")
                    UFC.SubstituteIllegalCharacters(NewSubDirectoryName, DoNotSubstituteChars)
                End If

            Else
                NewSubDirectoryName = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, Me.Formula, Me.PropertiesData, TaskLogger)

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
            If Me.FilenameFormula.StartsWith("EXPRESSION_") Then
                NewFilenameWOExt = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, Me.FilenameFormula, Me.PropertiesData, TaskLogger, True)

                If NewFilenameWOExt Is Nothing Then
                    Success = False
                    Me.TaskLogger.AddMessage(String.Format("Could not parse filename formula '{0}'", Me.FilenameFormula))
                Else
                    Dim DoNotSubstituteChars As New List(Of String)
                    DoNotSubstituteChars.Add("\")
                    UFC.SubstituteIllegalCharacters(NewSubDirectoryName, DoNotSubstituteChars)
                End If

            Else
                NewFilenameWOExt = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, Me.FilenameFormula, Me.PropertiesData, TaskLogger)

                If NewFilenameWOExt Is Nothing Then
                    Success = False
                    Me.TaskLogger.AddMessage(String.Format("Could not parse filename formula '{0}'", Me.FilenameFormula))
                Else
                    Dim DoNotSubstituteChars As New List(Of String)
                    DoNotSubstituteChars.Add("\")
                    UFC.SubstituteIllegalCharacters(NewSubDirectoryName, DoNotSubstituteChars)
                End If

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
                End If
            Else
                If Me.SaveInOriginalDirectory Then
                    SEDoc.SaveCopyAs(NewFilename)
                    SEApp.DoIdle()
                Else
                    Me.TaskLogger.AddMessage("Can not SaveCopyAs to the original directory")
                End If
            End If

        Catch ex As Exception
            Me.TaskLogger.AddMessage(String.Format("Error saving file {0}", NewFilename))
        End Try

    End Sub

    Private Sub SaveAsFlatDXF(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        NewFilename As String,
        SEApp As SolidEdgeFramework.Application,
        DocType As String
        )
        Dim FlatPatternModels As SolidEdgePart.FlatPatternModels = Nothing
        Dim Models As SolidEdgePart.Models = Nothing

        Select Case DocType
            Case "par"
                Dim tmpSEDoc As SolidEdgePart.PartDocument = CType(SEDoc, PartDocument)
                FlatPatternModels = tmpSEDoc.FlatPatternModels
                Models = tmpSEDoc.Models
                'Dim ActiveEnvironment As String = SEApp.ActiveEnvironment
                'If ActiveEnvironment = "SheetMetal" Then
                '    FlatPatternModels = tmpSEDoc.FlatPatternModels
                '    Models = tmpSEDoc.Models
                'End If
            Case "psm"
                Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                FlatPatternModels = tmpSEDoc.FlatPatternModels
                Models = tmpSEDoc.Models
        End Select
        Try
            If FlatPatternModels IsNot Nothing Then
                If FlatPatternModels.Count > 0 Then
                    Dim FlatPattern As SolidEdgePart.FlatPatternModel
                    FlatPattern = FlatPatternModels.Item(1)
                    If FlatPattern.IsUpToDate Then

                        Try
                            If Models IsNot Nothing AndAlso Models.Count > 0 Then
                                Models.SaveAsFlatDXFEx(NewFilename, Nothing, Nothing, Nothing, True)
                                SEApp.DoIdle()
                            Else
                                Me.TaskLogger.AddMessage("No model detected")
                            End If
                        Catch ex As Exception
                            Me.TaskLogger.AddMessage(String.Format("Error saving '{0}'.", NewFilename))
                            If ex.Message.Contains("E_POINTER") Then
                                Me.TaskLogger.AddMessage("Possibly the flat pattern was a 'Blank', not a 'Flatten'.")
                            Else
                                Me.TaskLogger.AddMessage($"Error was: {ex.Message}.")
                            End If
                        End Try
                    Else
                        Me.TaskLogger.AddMessage("Flat pattern reported out of date")
                    End If
                Else
                    Me.TaskLogger.AddMessage("No flat pattern detected")
                End If
            End If
        Catch ex As Exception
            Me.TaskLogger.AddMessage("Error accessing a flat pattern model.")
            Me.TaskLogger.AddMessage($"Error was: {ex.Message}.")

        End Try

    End Sub

    Private Sub SaveAsModel(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        NewFilename As String,
        SEApp As SolidEdgeFramework.Application
        )

        Try
            SEDoc.SaveAs(NewFilename)
            SEApp.DoIdle()
        Catch ex As Exception
            Me.TaskLogger.AddMessage(String.Format("Could not save '{0}'", NewFilename))
        End Try

    End Sub

    Private Sub SaveAsImage(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        NewFilename As String,
        SEApp As SolidEdgeFramework.Application,
        NewExtension As String
        )

        Dim Window As SolidEdgeFramework.Window
        Dim View As SolidEdgeFramework.View

        Dim UC As New UtilsCommon

        If Me.HideConstructions Then
            Dim TaskHideConstructions As New TaskShowHideConstructions
            TaskHideConstructions.FileLogger = Me.FileLogger

            TaskHideConstructions.Process(SEDoc, SEApp)
            If TaskHideConstructions.TaskLogger.HasErrors Then
                Dim tmpMessages As List(Of String) = TaskHideConstructions.TaskLogger.GetMessages
                For Each s As String In tmpMessages
                    TaskLogger.AddMessage(s)
                Next
            End If
        End If

        If Me.FitView Then
            Dim TaskFitView As New TaskFitView
            TaskFitView.FileLogger = Me.FileLogger

            TaskFitView.Isometric = Me.Isometric
            TaskFitView.Dimetric = Me.Dimetric
            TaskFitView.Trimetric = Me.Trimetric

            TaskFitView.Process(SEDoc, SEApp)
            If TaskFitView.TaskLogger.HasErrors Then
                Dim tmpMessages As List(Of String) = TaskFitView.TaskLogger.GetMessages
                For Each s As String In tmpMessages
                    TaskLogger.AddMessage(s)
                Next
            End If

        End If

        Window = CType(SEApp.ActiveWindow, SolidEdgeFramework.Window)
        View = Window.View

        If Me.ChangeViewStyle Then

            Try
                Dim ViewStyles As SolidEdgeFramework.ViewStyles = Nothing
                Dim ViewStyle As SolidEdgeFramework.ViewStyle = Nothing

                Select Case UC.GetDocType(SEDoc)
                    Case "asm"
                        Dim tmpSEDoc = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)
                        ViewStyles = CType(tmpSEDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
                    Case "par"
                        Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                        ViewStyles = CType(tmpSEDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
                    Case "psm"
                        Dim tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                        ViewStyles = CType(tmpSEDoc.ViewStyles, SolidEdgeFramework.ViewStyles)
                End Select

                For Each VS As SolidEdgeFramework.ViewStyle In ViewStyles
                    If VS.StyleName = ViewStyleName Then
                        ViewStyle = VS
                        Exit For
                    End If
                Next

                View.ViewStyle = ViewStyle
                View.Update()
                SEApp.DoIdle()

            Catch ex As Exception
                Me.TaskLogger.AddMessage(String.Format("Error changing to view style '{0}'", ViewStyleName))

            End Try
        End If

        If Not NewExtension = ".png" Then
            View.SaveAsImage(NewFilename)
        Else
            UC.SaveAsPNG(View, NewFilename, TaskLogger)
        End If

        If Me.CropImage Then
            DoCropImage(SEDoc, NewFilename, NewExtension, Window.Height, Window.Width)
        End If

    End Sub

    Private Function GetNewFileTypeNames() As List(Of String)
        Dim NewFileTypeNames As New List(Of String)

        NewFileTypeNames.Add("")
        NewFileTypeNames.Add("Step (*.stp)")
        NewFileTypeNames.Add("IGES (*.igs)")
        NewFileTypeNames.Add("Parasolid Text (*.x_t)")
        NewFileTypeNames.Add("Parasolid Binary (*.x_b)")
        NewFileTypeNames.Add("OBJ (*.obj)")
        NewFileTypeNames.Add("STL (*.stl)")
        NewFileTypeNames.Add("JT Document (*.jt)")
        NewFileTypeNames.Add("SEV (*.sev)")
        NewFileTypeNames.Add("bmp (*.bmp)")
        NewFileTypeNames.Add("jpg (*.jpg)")
        NewFileTypeNames.Add("png (*.png)")
        NewFileTypeNames.Add("tif (*.tif)")
        NewFileTypeNames.Add("Copy (*.*)")
        NewFileTypeNames.Add("PDF Drawing (*.pdf)")
        NewFileTypeNames.Add("DXF Flat (*.dxf)")

        ImageFileTypeNames.Clear()
        ImageFileTypeNames.Add("bmp (*.bmp)")
        ImageFileTypeNames.Add("jpg (*.jpg)")
        ImageFileTypeNames.Add("png (*.png)")
        ImageFileTypeNames.Add("tif (*.tif)")



        Return NewFileTypeNames
    End Function

    Public Sub DoCropImage(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        NewFilename As String,
        NewExtension As String,
        WindowH As Integer,
        WindowW As Integer
        )

        ' See SolidEdgeHousekeeper\reference\crop_image_derivation

        Dim ModelX As Double
        Dim ModelY As Double
        Dim ModelZ As Double

        Dim ImageW As Double
        Dim ImageH As Double
        Dim ImageAspectRatio As Double

        Dim CropW As Integer
        Dim CropH As Integer

        Dim TempFilename As String

        Dim WindowAspectRatio As Double = WindowH / WindowW

        Dim UC As New UtilsCommon

        Dim Range As New List(Of Double)

        Range = UC.GetDocRange(SEDoc)
        ModelX = Range(0)
        ModelY = Range(1)
        ModelZ = Range(2)

        ImageW = 0.557 * ModelX + 0.830667 * ModelY
        ImageH = 0.325444 * ModelX + 0.217778 * ModelY + 0.920444 * ModelZ

        ImageAspectRatio = ImageH / ImageW

        If WindowAspectRatio > ImageAspectRatio Then
            CropH = CInt(Math.Round(WindowW * ImageAspectRatio))
            CropW = WindowW
        Else
            CropH = WindowH
            CropW = CInt(Math.Round(WindowH / ImageAspectRatio))
        End If

        TempFilename = NewFilename.Replace(NewExtension, String.Format("-Housekeeper{0}", NewExtension))

        Dim LocX = (WindowW - CropW) / 2
        Dim LocY = (WindowH - CropH) / 2
        Dim CropRect As New Rectangle(CInt(LocX), CInt(LocY), CropW, CropH)
        Dim OriginalImage = System.Drawing.Image.FromFile(NewFilename)
        Dim xCropImage = New Bitmap(CropRect.Width, CropRect.Height)

        Try

            Using grp = Graphics.FromImage(xCropImage)
                grp.DrawImage(OriginalImage, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
                OriginalImage.Dispose()
                xCropImage.Save(TempFilename)
            End Using

            Try
                System.IO.File.Delete(NewFilename)
                FileSystem.Rename(TempFilename, NewFilename)
            Catch ex As Exception
                Me.TaskLogger.AddMessage(String.Format("Unable to save cropped image '{0}'", NewFilename))
            End Try

        Catch ex As Exception
            Me.TaskLogger.AddMessage(String.Format("Unable to save cropped image '{0}'", TempFilename))
        End Try

    End Sub


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
        Dim NewFileTypeLabelText = "(Sheetmetal only: PDF, DXF)"

        FormatTLPOptionsEx(tmpTLPOptions, "TLPOptions", 7, 75, 75)

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

        CheckBox = FormatOptionsCheckBox(ControlNames.ChangeFilename.ToString, "Change filename")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        TextBox = FormatOptionsTextBox(ControlNames.FilenameFormula.ToString, "")
        TextBox.ContextMenuStrip = Me.TaskControl.ContextMenuStrip1
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
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
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
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
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(TextBox, 3)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.CropImage.ToString, "Crop image to model size")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        CheckBox.Visible = False
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.HideConstructions.ToString, "Hide constructions")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        CheckBox.Visible = False
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.FitView.ToString, "Fit view")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        CheckBox.Visible = False
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.Isometric.ToString, "Isometric")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        CheckBox.Visible = False
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.Dimetric.ToString, "Dimetric")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        CheckBox.Visible = False
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.Trimetric.ToString, "Trimetric")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        CheckBox.Visible = False
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.ChangeViewStyle.ToString, "Change view style")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 3)
        CheckBox.Visible = False
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        Label = FormatOptionsLabel(ControlNames.ViewStyleNameLabel.ToString, "Style name")
        tmpTLPOptions.Controls.Add(Label, 0, RowIndex)
        Label.Visible = False
        ControlsDict(Label.Name) = Label

        TextBox = FormatOptionsTextBox(ControlNames.ViewStyleName.ToString, "")
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        AddHandler TextBox.GotFocus, AddressOf TextBox_GotFocus
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        tmpTLPOptions.SetColumnSpan(TextBox, 2)
        TextBox.Visible = False
        ControlsDict(TextBox.Name) = TextBox

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
                ErrorLogger.AddMessage("Enter a valid directory")
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

            If Me.FitView And ImageFileTypeNames.Contains(Me.NewFileTypeName) Then
                If Not (Me.Isometric Or Me.Dimetric Or Me.Trimetric) Then
                    ErrorLogger.AddMessage("Select a view orientation")
                End If
            End If

            If Me.ChangeViewStyle And ImageFileTypeNames.Contains(Me.NewFileTypeName) Then
                If Me.ViewStyleName = "" Then
                    ErrorLogger.AddMessage("Enter a view style name")
                End If
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

                If IO.Directory.Exists(Me.NewDir) Then
                    tmpFolderDialog.InitialDirectory = Me.NewDir
                Else
                    tmpFolderDialog.InitialDirectory = Form_Main.WorkingFilesPath
                End If

                If tmpFolderDialog.ShowDialog() = DialogResult.OK Then
                    Me.NewDir = tmpFolderDialog.FileName

                    TextBox = CType(ControlsDict(ControlNames.NewDir.ToString), TextBox)
                    TextBox.Text = Me.NewDir

                    'Form_Main.WorkingFilesPath = Me.NewDir
                End If
            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select

    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Dim ParticipatingCheckBoxes As New List(Of CheckBox)
        ParticipatingCheckBoxes.Add(CType(ControlsDict(ControlNames.Isometric.ToString), CheckBox))
        ParticipatingCheckBoxes.Add(CType(ControlsDict(ControlNames.Dimetric.ToString), CheckBox))
        ParticipatingCheckBoxes.Add(CType(ControlsDict(ControlNames.Trimetric.ToString), CheckBox))

        Select Case Name

            Case ControlNames.ChangeFilename.ToString
                Me.ChangeFilename = Checkbox.Checked

                CType(ControlsDict(ControlNames.FilenameFormula.ToString), TextBox).Visible = Me.ChangeFilename

            Case ControlNames.SaveInOriginalDirectory.ToString
                Me.SaveInOriginalDirectory = Checkbox.Checked

                CType(ControlsDict(ControlNames.BrowseNewDir.ToString), Button).Visible = Not Me.SaveInOriginalDirectory
                CType(ControlsDict(ControlNames.NewDir.ToString), TextBox).Visible = Not Me.SaveInOriginalDirectory

            Case ControlNames.UseSubdirectoryFormula.ToString
                Me.UseSubdirectoryFormula = Checkbox.Checked

                CType(ControlsDict(ControlNames.Formula.ToString), TextBox).Visible = Me.UseSubdirectoryFormula

            Case ControlNames.CropImage.ToString
                Me.CropImage = Checkbox.Checked

            Case ControlNames.HideConstructions.ToString
                Me.HideConstructions = Checkbox.Checked

            Case ControlNames.FitView.ToString
                Me.FitView = Checkbox.Checked

                CType(ControlsDict(ControlNames.Isometric.ToString), CheckBox).Visible = Me.FitView And Checkbox.Visible
                CType(ControlsDict(ControlNames.Dimetric.ToString), CheckBox).Visible = Me.FitView And Checkbox.Visible
                CType(ControlsDict(ControlNames.Trimetric.ToString), CheckBox).Visible = Me.FitView And Checkbox.Visible

            Case ControlNames.Isometric.ToString
                Me.Isometric = Checkbox.Checked
                If Me.Isometric Then
                    HandleMutuallyExclusiveCheckBoxes(Me.TaskOptionsTLP, Checkbox, ParticipatingCheckBoxes)
                End If

            Case ControlNames.Dimetric.ToString
                Me.Dimetric = Checkbox.Checked
                If Me.Dimetric Then
                    HandleMutuallyExclusiveCheckBoxes(Me.TaskOptionsTLP, Checkbox, ParticipatingCheckBoxes)
                End If

            Case ControlNames.Trimetric.ToString
                Me.Trimetric = Checkbox.Checked
                If Me.Trimetric Then
                    HandleMutuallyExclusiveCheckBoxes(Me.TaskOptionsTLP, Checkbox, ParticipatingCheckBoxes)
                End If

            Case ControlNames.ChangeViewStyle.ToString
                Me.ChangeViewStyle = Checkbox.Checked

                CType(ControlsDict(ControlNames.ViewStyleName.ToString), TextBox).Visible = Me.ChangeViewStyle
                CType(ControlsDict(ControlNames.ViewStyleNameLabel.ToString), Label).Visible = Me.ChangeViewStyle

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub ComboBoxOptions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Dim ComboBox = CType(sender, ComboBox)
        Dim Name = ComboBox.Name
        Dim CheckBox As CheckBox = Nothing
        Dim TextBox As TextBox = Nothing
        Dim Label As Label = Nothing

        Select Case Name
            Case ControlNames.NewFileTypeName.ToString
                Me.NewFileTypeName = ComboBox.Text

                CheckBox = CType(ControlsDict(ControlNames.CropImage.ToString), CheckBox)
                CheckBox.Visible = ImageFileTypeNames.Contains(Me.NewFileTypeName)

                CheckBox = CType(ControlsDict(ControlNames.HideConstructions.ToString), CheckBox)
                CheckBox.Visible = ImageFileTypeNames.Contains(Me.NewFileTypeName)

                CheckBox = CType(ControlsDict(ControlNames.FitView.ToString), CheckBox)
                CheckBox.Visible = ImageFileTypeNames.Contains(Me.NewFileTypeName)

                CheckBox = CType(ControlsDict(ControlNames.Isometric.ToString), CheckBox)
                CheckBox.Visible = ImageFileTypeNames.Contains(Me.NewFileTypeName) And Me.FitView

                CheckBox = CType(ControlsDict(ControlNames.Dimetric.ToString), CheckBox)
                CheckBox.Visible = ImageFileTypeNames.Contains(Me.NewFileTypeName) And Me.FitView

                CheckBox = CType(ControlsDict(ControlNames.Trimetric.ToString), CheckBox)
                CheckBox.Visible = ImageFileTypeNames.Contains(Me.NewFileTypeName) And Me.FitView

                CheckBox = CType(ControlsDict(ControlNames.ChangeViewStyle.ToString), CheckBox)
                CheckBox.Visible = ImageFileTypeNames.Contains(Me.NewFileTypeName)

                TextBox = CType(ControlsDict(ControlNames.ViewStyleName.ToString), TextBox)
                TextBox.Visible = ImageFileTypeNames.Contains(Me.NewFileTypeName) And Me.ChangeViewStyle

                Label = CType(ControlsDict(ControlNames.ViewStyleNameLabel.ToString), Label)
                Label.Visible = ImageFileTypeNames.Contains(Me.NewFileTypeName) And Me.ChangeViewStyle

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))

        End Select

    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name
            Case ControlNames.FilenameFormula.ToString
                Me.FilenameFormula = TextBox.Text

            Case ControlNames.Formula.ToString
                Me.Formula = TextBox.Text

            Case ControlNames.NewDir.ToString
                Me.NewDir = TextBox.Text

            Case ControlNames.ViewStyleName.ToString
                Me.ViewStyleName = TextBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String
        HelpString = "Exports the file to either a non-Solid Edge format, or the same format in a different directory. "

        HelpString += vbCrLf + vbCrLf + "![SaveModelAs](My%20Project/media/task_save_model_as.png)"

        HelpString += vbCrLf + vbCrLf + "Select the file type using the combobox. "
        HelpString += "Select the directory using the `Browse` button, "
        HelpString += "or check the `Original Directory` checkbox. "

        HelpString += vbCrLf + vbCrLf + "You can optionally rename the file or create subdirectories using a formula similar to the Property Text Callout. "
        HelpString += "Enable the `Change filename` and/or `Use subdirectory formula` options to do so. "
        HelpString += "To add a property, right-click the text box and select `Insert property`. "
        HelpString += "You can also just type it in if that's easier. "
        HelpString += "You can create nested subdirectories if desired. Simply add `\` in the subdirectory formula. Here are two examples. "

        HelpString += vbCrLf + "- `Project %{System.Project Name}` "
        HelpString += vbCrLf + "- `%{System.Material}\%{System.Sheet Metal Gage}` "

        HelpString += vbCrLf + vbCrLf + "Another option to create the new file or subdirectory name is to use the Expression Editor.  "
        HelpString += "This can be especially handy if an `If` statement is needed for any reason.  "
        HelpString += "For details on the Expression Editor, refer to the "
        HelpString += "[<ins>**Edit Properties Help Topic**</ins>](https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/HelpTopics.md#edit-properties).  "
        HelpString += "You'll have to scroll down a bit to see it."

        HelpString += vbCrLf + vbCrLf + "You can enter a subdirectory formula even if `Original directory` is enabled. "
        HelpString += "In that case, it is interpreted as a subdirectory under the original directory. "

        HelpString += vbCrLf + vbCrLf + "As illustrated in the examples, a `Property set`, either `System` or `Custom`, is required. "
        HelpString += "For more information, refer to the "
        HelpString += "[<ins>**Property Filter**</ins>](#property-filter) section in this Readme file. "

        HelpString += vbCrLf + vbCrLf + "It is possible that a property contains a character that cannot be used in a file name. "
        HelpString += "If that happens, a replacement is read from `filename_charmap.txt` in the `Preferences` directory in the Housekeeper root folder. "
        HelpString += "You can/should edit it to change the replacement characters to your preference. "
        HelpString += "The file is created the first time you run Housekeeper.  For details, see the header comments in that file. "

        HelpString += vbCrLf + vbCrLf + "Sheetmetal files (including `*.par` ""switched to"" sheetmetal) "
        HelpString += "have two additional options -- `DXF Flat (*.dxf)` and `PDF Drawing (*.pdf)`. "
        HelpString += "The `DXF Flat` option saves the flat pattern of the sheet metal file. "
        HelpString += "Note the file's flat pattern must be of type `Flatten`, not `Blank`. "
        HelpString += "The program is not able to handle the latter.  You will have to do those manually. "
        HelpString += "If such a condition is found, it is reported in the log file. "

        HelpString += vbCrLf + vbCrLf + "The `PDF Drawing` option saves the drawing of the sheet metal file. "
        HelpString += "The drawing must have the same name as the model, and be in the same directory. "
        HelpString += "A more flexible option may be to use `Save Drawing As` command, "
        HelpString += "using a `Property Filter` if needed. "

        HelpString += vbCrLf + vbCrLf + "For image file formats there are additional options. "
        HelpString += "You can hide constructions and/or fit the view.  For Fit, choose an orientation, either `Isometric`, `Dimetric`, or `Trimetric`. "
        HelpString += "You can also crop images to the aspect ratio of the model, rather than the window. "
        HelpString += "The option is called `Crop image to model size`. "
        HelpString += "On tall skinny parts cropping works a little *too* well.  You might need to expand the margins a bit in Photoshop for those. "
        HelpString += "Finally, you can change the view style by selecting that option and entering its name in the textbox provided. "


        Return HelpString
    End Function


End Class
