Option Strict On

Imports Newtonsoft.Json


Public Class UtilsPreferences

    Public Property RememberTaskSelections As Boolean

    Public Sub New()
        Me.RememberTaskSelections = True
    End Sub

    Public Function GetStartupDirectory() As String
        Dim StartupDirectory As String = System.Windows.Forms.Application.StartupPath()
        Return StartupDirectory
    End Function

    Public Function GetPreferencesDirectory() As String
        Dim StartupPath As String = GetStartupDirectory()
        Dim PreferencesDirectory = "Preferences"
        Return String.Format("{0}\{1}", StartupPath, PreferencesDirectory)
    End Function

    Public Sub CreatePreferencesDirectory()
        Dim PreferencesDirectory = GetPreferencesDirectory()
        If Not FileIO.FileSystem.DirectoryExists(PreferencesDirectory) Then
            FileIO.FileSystem.CreateDirectory(PreferencesDirectory)
        End If
    End Sub


    Public Sub CreateFilenameCharmap()
        Dim UFC As New UtilsFilenameCharmap()  ' Creates the file filename_charmap.txt if it does not exist.
    End Sub


    Public Function GetSavedExpressionsFilename() As String
        Return String.Format("{0}\saved_expressions.txt", GetPreferencesDirectory)
    End Function

    Public Sub CreateSavedExpressions()
        Dim SavedExpressionsFilename = GetSavedExpressionsFilename()

        If Not FileIO.FileSystem.FileExists(SavedExpressionsFilename) Then
            Dim Outlist As New List(Of String)

            Outlist.Add("[EXP]")
            Outlist.Add("Example 1")
            Outlist.Add("[EXP_TEXT]")
            Outlist.Add("")
            Outlist.Add("'%{System.Title}' + '-' + toString(cast(substring('%{System.Subject}', lastIndexOf('%{System.Subject}', 'L=')+2, lastIndexOf('%{System.Subject}', ' ')-lastIndexOf('%{System.Subject}', 'L=')-2),'System.Int32'),'D4') + '-' + substring('%{System.Subject}', lastIndexOf('%{System.Subject}', ' ')+1)")
            Outlist.Add("")
            Outlist.Add("\\ Example of text manipulation And number formatting")
            Outlist.Add("\\ System.Title <-- any string")
            Outlist.Add("\\ System.Subject <-- need to end with this format L=xxx YY")
            Outlist.Add("\\ xxx can be any number from 0 to 9999 And YY any two letters")
            Outlist.Add("\\ xxx will be transformed in D4 syntax (example 65 will became 0065)")
            Outlist.Add("")
            Outlist.Add("[EXP]")
            Outlist.Add("Example If()")
            Outlist.Add("[EXP_TEXT]")
            Outlist.Add("if ('%{System.Title}' == 'Dog','Meat',")
            Outlist.Add("if('%{System.Title}' == 'Cat','Fish',")
            Outlist.Add("if('%{System.Title}' == 'Cow','Hay','unknown')))")
            Outlist.Add("")
            Outlist.Add("\\Example of the usage of if() statement, valid inputs Dog, Cat, Cow")
            Outlist.Add("")
            Outlist.Add("[EXP]")
            Outlist.Add("Example Replace()")
            Outlist.Add("[EXP_TEXT]")
            Outlist.Add("")
            Outlist.Add("replace('%{System.Subject}','L=','L:')")
            Outlist.Add("")
            Outlist.Add("\\ %{System.Subject} must contains 'L='")
            Outlist.Add("")
            Outlist.Add("[EXP]")
            Outlist.Add("Example toUpper()")
            Outlist.Add("[EXP_TEXT]")
            Outlist.Add("")
            Outlist.Add("toUpper('%{System.Title}')")
            Outlist.Add("")
            Outlist.Add("\\Any text will be converted in UPPERCASE")

            IO.File.WriteAllLines(SavedExpressionsFilename, Outlist)
        End If
    End Sub

    Public Function GetInteractiveEditCommandsFilename() As String
        Dim InteractiveEditCommandsFilename = String.Format("{0}\interactive_edit_commands.txt", GetPreferencesDirectory)
        Return InteractiveEditCommandsFilename
    End Function


    Public Sub CreateInteractiveEditCommands()

        'Description                CCA    CCP    CCS    CCD
        'Display cfgs               32826			
        'Edit links                 57857                57857
        'Edit variables             25036  25036  25036  10504
        'File options               25042  25042  25042  10508
        'File properties            40001  40001  40001  40001
        'Flat pattern                             45066	
        'Format style               33058  25030  25030	
        'Inquire element            25072  25072  25072	
        'Interpart manager          40277  40277  40277	
        'Part painter                      40314  40314	
        'Physical properties        25038  25038  25038	
        'Property manager           50005  50005  50005	
        'Replace part               32808
        'Sheet setup                                     10002
        'View Backgrounds                                10211

        Dim InteractiveEditCommandsFilename = GetInteractiveEditCommandsFilename()

        If Not FileIO.FileSystem.FileExists(InteractiveEditCommandsFilename) Then

            Dim Outlist As New List(Of String)


            Outlist.Add("'This file stores command IDs for the Edit Interactively task.")
            Outlist.Add("'")
            Outlist.Add("'The format is: Description, Assembly ID, Part ID, Sheetmetal ID, Draft ID")
            Outlist.Add("'Where 'ID' above means the command ID number found in the API documentation.")
            Outlist.Add("'")
            Outlist.Add("'You can add/remove items from the list.  The description is just text for the")
            Outlist.Add("'user.  You can change it to your preference.")
            Outlist.Add("'")
            Outlist.Add("'To find what commands are available, consult the API documentation.  For SE2024,")
            Outlist.Add("'here are the locations:")
            Outlist.Add("'")
            Outlist.Add("'Assembly: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~AssemblyCommandConstants.html")
            Outlist.Add("'Part: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~PartCommandConstants.html")
            Outlist.Add("'Sheetmetal: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~SheetMetalCommandConstants.html")
            Outlist.Add("'Draft: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~DetailCommandConstants.html")
            Outlist.Add("'")
            Outlist.Add("'If a command is not available for a given file type, or you do not want to show it,")
            Outlist.Add("'enter zero in that field.")
            Outlist.Add("'")
            Outlist.Add("'You can add comments to this file if desired.  Just begin the line with the")
            Outlist.Add("'single-quote (') character.")
            Outlist.Add("'")
            Outlist.Add("'If you mess up the file in some way, you can delete it.  It will be regenerated next time")
            Outlist.Add("'the program starts.")
            Outlist.Add("'")
            Outlist.Add("Manual entry, 0, 0, 0, 0")
            Outlist.Add("Display cfgs, 32826, 0, 0, 0")
            Outlist.Add("Edit links, 57857, 0, 0, 57857")
            Outlist.Add("Edit variables, 25036, 25036, 25036, 10504")
            Outlist.Add("File options, 25042, 25042, 25042, 10508")
            Outlist.Add("File properties, 40001, 40001, 40001, 40001")
            Outlist.Add("Flat pattern, 0, 0, 45066, 0")
            Outlist.Add("Format style, 33058, 25030, 25030, 0")
            Outlist.Add("Inquire element, 25072, 25072, 25072, 0")
            Outlist.Add("Interpart manager, 40277, 40277, 40277, 0")
            Outlist.Add("Part painter, 0, 40314, 40314, 0")
            Outlist.Add("Physical properties, 25038, 25038, 25038, 0")
            Outlist.Add("Property manager, 50005, 50005, 50005, 0")
            Outlist.Add("Replace part, 32808, 0, 0, 0")
            Outlist.Add("Sheet setup, 0, 0, 0, 10002")
            Outlist.Add("View backgrounds, 0, 0, 0, 10211")

            IO.File.WriteAllLines(InteractiveEditCommandsFilename, Outlist)
        End If


    End Sub


    Public Function GetTemplatePropertyDictFilename(CheckExisting As Boolean) As String
        Dim Filename = String.Format("{0}\template_property_dict.json", GetPreferencesDirectory)

        If CheckExisting Then
            If FileIO.FileSystem.FileExists(Filename) Then
                Return Filename
            Else
                Return ""
            End If
        Else
            Return Filename
        End If

    End Function

    Public Sub SaveTemplatePropertyDict(TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String)))
        Dim JSONString As String
        Dim Filename = GetTemplatePropertyDictFilename(CheckExisting:=False)

        JSONString = JsonConvert.SerializeObject(TemplatePropertyDict)
        IO.File.WriteAllText(Filename, JSONString)

    End Sub

    Public Function GetTemplatePropertyDict() As Dictionary(Of String, Dictionary(Of String, String))
        Dim TemplatePropertyDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim JSONString As String
        Dim Filename = GetTemplatePropertyDictFilename(CheckExisting:=True)

        If Not Filename = "" Then
            JSONString = IO.File.ReadAllText(Filename)
            TemplatePropertyDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(JSONString)
        End If

        Return TemplatePropertyDict
    End Function


    Public Function GetEditPropertiesSavedSettingsFilename(CheckExisting As Boolean) As String
        Dim Filename = String.Format("{0}\edit_properties_saved_settings.json", GetPreferencesDirectory)

        If CheckExisting Then
            If FileIO.FileSystem.FileExists(Filename) Then
                Return Filename
            Else
                Return ""
            End If
        Else
            Return Filename
        End If

    End Function

    Public Sub SaveEditPropertiesSavedSettings(
        EditPropertiesSavedSettingsDict As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String))))

        Dim JSONString As String
        Dim Filename = GetEditPropertiesSavedSettingsFilename(CheckExisting:=False)

        JSONString = JsonConvert.SerializeObject(EditPropertiesSavedSettingsDict)
        IO.File.WriteAllText(Filename, JSONString)

    End Sub

    Public Function GetPropertyFilterDictFilename(CheckExisting As Boolean) As String
        Dim Filename = String.Format("{0}\property_filter_dict.json", GetPreferencesDirectory)

        If CheckExisting Then
            If FileIO.FileSystem.FileExists(Filename) Then
                Return Filename
            Else
                Return ""
            End If
        Else
            Return Filename
        End If

    End Function


    Public Sub SavePropertyFilterDict(PropertyFilterDict As Dictionary(Of String, Dictionary(Of String, String)))
        Dim JSONString As String
        Dim Filename = GetPropertyFilterDictFilename(CheckExisting:=False)

        JSONString = JsonConvert.SerializeObject(PropertyFilterDict)
        IO.File.WriteAllText(Filename, JSONString)

    End Sub

    Public Function GetPropertyFilterDict() As Dictionary(Of String, Dictionary(Of String, String))
        Dim PropertyFilterDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim JSONString As String
        Dim Filename = GetPropertyFilterDictFilename(CheckExisting:=True)

        If Not Filename = "" Then
            JSONString = IO.File.ReadAllText(Filename)
            PropertyFilterDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(JSONString)
        End If

        Return PropertyFilterDict
    End Function

    Public Function GetEditPropertiesSavedSettings() As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String)))
        Dim EditPropertiesSavedSettingsDict As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String)))
        Dim JSONString As String
        Dim Filename = GetEditPropertiesSavedSettingsFilename(CheckExisting:=True)

        If Not Filename = "" Then
            JSONString = IO.File.ReadAllText(Filename)
            EditPropertiesSavedSettingsDict = JsonConvert.DeserializeObject(
                Of Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String))))(JSONString)
        End If

        Return EditPropertiesSavedSettingsDict
    End Function


    Public Function GetPropertyFilterSavedSettingsFilename(CheckExisting As Boolean) As String
        Dim Filename = String.Format("{0}\property_filter_saved_settings.json", GetPreferencesDirectory)

        If CheckExisting Then
            If FileIO.FileSystem.FileExists(Filename) Then
                Return Filename
            Else
                Return ""
            End If
        Else
            Return Filename
        End If

    End Function

    Public Sub SavePropertyFilterSavedSettings(
        PropertyFilterSavedSettingsDict As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String))))

        Dim JSONString As String
        Dim Filename = GetPropertyFilterSavedSettingsFilename(CheckExisting:=False)

        JSONString = JsonConvert.SerializeObject(PropertyFilterSavedSettingsDict)
        IO.File.WriteAllText(Filename, JSONString)

    End Sub

    Public Function GetPropertyFilterSavedSettings() As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String)))
        Dim PropertyFilterSavedSettingsDict As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String)))
        Dim JSONString As String
        Dim Filename = GetPropertyFilterSavedSettingsFilename(CheckExisting:=True)

        If Not Filename = "" Then
            JSONString = IO.File.ReadAllText(Filename)
            PropertyFilterSavedSettingsDict = JsonConvert.DeserializeObject(
                Of Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String))))(JSONString)
        End If

        Return PropertyFilterSavedSettingsDict
    End Function


    Public Function GetEditVariablesSavedSettingsFilename(CheckExisting As Boolean) As String
        Dim Filename = String.Format("{0}\edit_variables_saved_settings.json", GetPreferencesDirectory)

        If CheckExisting Then
            If FileIO.FileSystem.FileExists(Filename) Then
                Return Filename
            Else
                Return ""
            End If
        Else
            Return Filename
        End If

    End Function

    Public Sub SaveEditVariablesSavedSettings(
        EditVariablesSavedSettingsDict As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String))))

        Dim JSONString As String
        Dim Filename = GetEditVariablesSavedSettingsFilename(CheckExisting:=False)

        JSONString = JsonConvert.SerializeObject(EditVariablesSavedSettingsDict)
        IO.File.WriteAllText(Filename, JSONString)

    End Sub

    Public Function GetEditVariablesSavedSettings() As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String)))
        Dim EditVariablesSavedSettingsDict As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String)))
        Dim JSONString As String
        Dim Filename = GetEditVariablesSavedSettingsFilename(CheckExisting:=True)

        If Not Filename = "" Then
            JSONString = IO.File.ReadAllText(Filename)
            EditVariablesSavedSettingsDict = JsonConvert.DeserializeObject(
                Of Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String))))(JSONString)
        End If

        Return EditVariablesSavedSettingsDict
    End Function


    Public Function GetTemplatePropertyListFilename(CheckExisting As Boolean) As String
        Dim Filename = String.Format("{0}\template_property_list.txt", GetPreferencesDirectory)

        If CheckExisting Then
            If FileIO.FileSystem.FileExists(Filename) Then
                Return Filename
            Else
                Return ""
            End If
        Else
            Return Filename
        End If

    End Function

    Public Sub SaveTemplatePropertyList(TemplatePropertyList As List(Of String))
        Dim JSONString As String
        Dim Filename = GetTemplatePropertyListFilename(CheckExisting:=False)

        JSONString = JsonConvert.SerializeObject(TemplatePropertyList)
        IO.File.WriteAllText(Filename, JSONString)

    End Sub

    Public Function GetTemplatePropertyList() As List(Of String)
        Dim TemplatePropertyList As New List(Of String)
        Dim JSONString As String
        Dim Filename = GetTemplatePropertyListFilename(CheckExisting:=True)

        If Not Filename = "" Then
            JSONString = IO.File.ReadAllText(Filename)
            TemplatePropertyList = JsonConvert.DeserializeObject(Of List(Of String))(JSONString)
        End If

        Return TemplatePropertyList
    End Function


    Public Function GetTaskListFilename(CheckExisting As Boolean) As String
        Dim Filename = "tasklist.json"
        Dim TaskListFilename = String.Format("{0}\{1}", GetPreferencesDirectory, Filename)

        If CheckExisting Then
            If FileIO.FileSystem.FileExists(TaskListFilename) Then
                Return TaskListFilename
            Else
                Return ""
            End If
        Else
            Return TaskListFilename
        End If
    End Function

    Public Sub SaveTaskList(TaskList As List(Of Task))

        Dim tmpJSONDict As New Dictionary(Of String, String)
        Dim JSONString As String

        Dim Outfile = GetTaskListFilename(CheckExisting:=False)

        For Each Task As Task In TaskList
            ' To allow copies of a given Task, the Key Task.Description rather than Task.Name
            tmpJSONDict(Task.Description) = Task.GetFormState()
        Next

        JSONString = JsonConvert.SerializeObject(tmpJSONDict)

        IO.File.WriteAllText(Outfile, JSONString)

    End Sub

    Public Function GetTaskList() As List(Of Task)

        Dim TaskList As New List(Of Task)
        Dim Task As Task
        Dim JSONDict As Dictionary(Of String, String)
        Dim JSONString As String

        Dim TaskJSONDict As Dictionary(Of String, String)
        Dim TaskDescription As String
        Dim TaskName As String

        Dim Filename As String = GetTaskListFilename(CheckExisting:=True)

        Dim AvailableTasks = BuildTaskListFromScratch()

        If Filename = "" Then
            TaskList = AvailableTasks
        Else
            JSONString = IO.File.ReadAllText(Filename)

            JSONDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

            For Each TaskDescription In JSONDict.Keys
                JSONString = JSONDict(TaskDescription)
                TaskJSONDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)
                TaskName = TaskJSONDict("TaskName")

                Task = GetNewTaskInstance(AvailableTasks, TaskName, TaskDescription)

                If Task IsNot Nothing Then
                    Task.SetFormState(JSONString)
                    TaskList.Add(Task)
                End If
            Next

        End If

        Return TaskList
    End Function


    Public Function GetFormMainSettingsFilename(CheckExisting As Boolean) As String
        Dim Filename = "form_main_settings.json"
        Filename = String.Format("{0}\{1}", GetPreferencesDirectory, Filename)

        If CheckExisting Then
            If FileIO.FileSystem.FileExists(Filename) Then
                Return Filename
            Else
                Return ""
            End If
        Else
            Return Filename
        End If

    End Function

    Public Sub SaveFormMainSettings(_Form_Main As Form_Main)

        Dim tmpJSONDict As New Dictionary(Of String, String)
        Dim JSONString As String

        Dim Outfile = GetFormMainSettingsFilename(CheckExisting:=False)

        Dim FormType As Type = _Form_Main.GetType()
        Dim PropInfos = New List(Of System.Reflection.PropertyInfo)(FormType.GetProperties())
        Dim Value As String
        Dim PropType As String

        Dim KeepProps As New List(Of String)
        KeepProps.AddRange({"TLAAutoIncludeTLF", "WarnBareTLA", "TLAIncludePartCopies", "TLAReportUnrelatedFiles", "TLATopDown", "TLABottomUp"})
        KeepProps.AddRange({"DraftAndModelSameName", "FastSearchScopeFilename", "TLAIgnoreIncludeInReports", "ProcessAsAvailable"})
        KeepProps.AddRange({"ProcessAsAvailableRevert", "ProcessAsAvailableChange", "StatusAto", "StatusBto", "StatusIRto", "StatusIWto"})
        KeepProps.AddRange({"StatusOto", "StatusRto", "SortNone", "SortAlphabetical", "SortDependency", "SortIncludeNoDependencies"})
        KeepProps.AddRange({"SortRandomSample", "SortRandomSampleFraction", "AssemblyTemplate", "PartTemplate", "SheetmetalTemplate"})
        KeepProps.AddRange({"DraftTemplate", "MaterialTable", "UseTemplateProperties"})
        KeepProps.AddRange({"UseCurrentSession", "WarnSave", "NoUpdateMRU", "FileListFontSize", "RememberTasks", "RunInBackground"})
        KeepProps.AddRange({"PropertyFilterIncludeDraftModel", "PropertyFilterIncludeDraftItself", "CheckForNewerVersion"})
        KeepProps.AddRange({"WarnNoImportedProperties", "EnablePropertyFilter", "EnableFileWildcard", "FileWildcard", "FileWildcardList", "SolidEdgeRequired"})
        KeepProps.AddRange({"PropertyFilterDictJSON", "TemplatePropertyDictJSON", "TemplatePropertyList"})

        For Each PropInfo As System.Reflection.PropertyInfo In PropInfos

            If Not KeepProps.Contains(PropInfo.Name) Then
                Continue For
            End If

            PropType = PropInfo.PropertyType.Name.ToLower

            Value = Nothing

            Select Case PropType
                Case "string", "double", "int32", "boolean"
                    Value = CStr(PropInfo.GetValue(_Form_Main, Nothing))
                Case "list`1"
                    Value = JsonConvert.SerializeObject(PropInfo.GetValue(_Form_Main, Nothing))
                Case Else
                    MsgBox(String.Format("PropInfo.PropertyType.Name '{0}' not recognized", PropType))
            End Select


            If Value Is Nothing Then
                Select Case PropType
                    Case "string"
                        Value = ""
                    Case "double", "int32"
                        Value = "0"
                    Case "boolean"
                        Value = "False"
                    Case "list`1"
                        Value = JsonConvert.SerializeObject(New List(Of String))
                        MsgBox(String.Format("PropInfo.PropertyType.Name '{0}' detected", PropInfo.PropertyType.Name))
                    Case Else
                        MsgBox(String.Format("PropInfo.PropertyType.Name '{0}' not recognized", PropInfo.PropertyType.Name))
                End Select
            End If

            tmpJSONDict(PropInfo.Name) = Value

        Next

        JSONString = JsonConvert.SerializeObject(tmpJSONDict)

        IO.File.WriteAllText(Outfile, JSONString)


    End Sub

    Public Sub GetFormMainSettings(_Form_Main As Form_Main)

        Dim tmpJSONDict As New Dictionary(Of String, String)
        Dim JSONString As String

        Dim Infile = GetFormMainSettingsFilename(CheckExisting:=True)

        Dim FormType As Type = _Form_Main.GetType()
        Dim PropInfos = New List(Of System.Reflection.PropertyInfo)(FormType.GetProperties())
        'Dim Value As String

        If Not Infile = "" Then
            JSONString = IO.File.ReadAllText(Infile)

            tmpJSONDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

            For Each PropInfo As System.Reflection.PropertyInfo In PropInfos

                If tmpJSONDict.Keys.Contains(PropInfo.Name) Then
                    Dim PropTypestring = PropInfo.PropertyType.Name

                    Select Case PropInfo.PropertyType.Name.ToLower
                        Case "string"
                            PropInfo.SetValue(_Form_Main, CStr(tmpJSONDict(PropInfo.Name)))
                        Case "double"
                            PropInfo.SetValue(_Form_Main, CDbl(tmpJSONDict(PropInfo.Name)))
                        Case "int32"
                            PropInfo.SetValue(_Form_Main, CInt(tmpJSONDict(PropInfo.Name)))
                        Case "boolean"
                            PropInfo.SetValue(_Form_Main, CBool(tmpJSONDict(PropInfo.Name)))
                        Case "list`1"
                            Dim L = JsonConvert.DeserializeObject(Of List(Of String))(tmpJSONDict(PropInfo.Name))
                            PropInfo.SetValue(_Form_Main, L)
                    End Select

                End If
            Next
        End If

    End Sub


    Public Function GetNewTaskInstance(
        AvailableTasks As List(Of Task),
        TaskName As String,
        TaskDescription As String
        ) As Task

        Dim Task As Task = Nothing
        Dim tmpTask As Task = Nothing

        For Each Task In AvailableTasks
            If Task.Name = TaskName Then
                tmpTask = CType(Activator.CreateInstance(Task.GetType), Task)
                tmpTask.Description = TaskDescription
                Exit For
            End If
        Next

        Return tmpTask
    End Function

    Public Function BuildTaskListFromScratch() As List(Of Task)
        Dim TaskList As New List(Of Task)
        Dim KnownTasks As New List(Of String)

        ' Update
        TaskList.Add(New TaskOpenSave)
        TaskList.Add(New TaskActivateAndUpdateAll)
        TaskList.Add(New TaskUpdateMaterialFromMaterialTable)
        TaskList.Add(New TaskUpdatePartCopies)
        TaskList.Add(New TaskUpdatePhysicalProperties)
        TaskList.Add(New TaskUpdateModelSizeInVariableTable)
        TaskList.Add(New TaskUpdateDesignForCost)
        TaskList.Add(New TaskUpdateDrawingViews)
        TaskList.Add(New TaskUpdateFlatPattern)
        TaskList.Add(New TaskBreakLinks)

        ' Edit
        TaskList.Add(New TaskEditProperties)
        TaskList.Add(New TaskEditVariables)
        TaskList.Add(New TaskEditInteractively)
        TaskList.Add(New TaskRecognizeHoles)

        ' Restyle
        TaskList.Add(New TaskUpdateModelStylesFromTemplate)
        TaskList.Add(New TaskUpdateDrawingStylesFromTemplate)
        TaskList.Add(New TaskRemoveFaceStyleOverrides)
        TaskList.Add(New TaskHideConstructions)
        TaskList.Add(New TaskFitView)

        ' Check
        TaskList.Add(New TaskCheckInterference)
        TaskList.Add(New TaskCheckLinks)
        TaskList.Add(New TaskCheckRelationships)
        TaskList.Add(New TaskCheckFlatPattern)
        TaskList.Add(New TaskCheckMaterialNotInMaterialTable)
        TaskList.Add(New TaskCheckMissingDrawing)
        TaskList.Add(New TaskCheckPartNumberDoesNotMatchFilename)
        TaskList.Add(New TaskCheckPartCopies)
        TaskList.Add(New TaskCheckDrawingPartsList)
        TaskList.Add(New TaskCheckDrawings)

        ' Output
        TaskList.Add(New TaskRunExternalProgram)
        TaskList.Add(New TaskSaveModelAs)
        TaskList.Add(New TaskSaveDrawingAs)
        TaskList.Add(New TaskCreateDrawingOfFlatPattern)
        TaskList.Add(New TaskPrint)

        For Each Task As Task In TaskList
            'Task.RememberTaskSelections = Form1.RememberTaskSelections
            'Task.RememberTaskSelections = Me.RememberTaskSelections
            KnownTasks.Add(Task.Name.ToLower)
        Next

        CheckForUnknownTasks(KnownTasks)

        Return TaskList

    End Function

    Private Sub CheckForUnknownTasks(KnownTasks As List(Of String))
        Dim HardcodedPath = "C:\data\CAD\scripts\SolidEdgeHousekeeper\My Project"
        Dim Filenames As List(Of String)
        Dim Filename As String

        Dim UnknownTasks As New List(Of String)

        Dim tf As Boolean
        Dim s As String = String.Format("Unknown Tasks{0}", vbCrLf)

        If FileIO.FileSystem.DirectoryExists(HardcodedPath) Then
            Filenames = IO.Directory.GetFiles(HardcodedPath).ToList

            For Each Filename In Filenames
                Filename = System.IO.Path.GetFileNameWithoutExtension(Filename).ToLower
                tf = Filename.StartsWith("task")
                tf = tf And Not Filename = "task"
                tf = tf And Not Filename.StartsWith("task_")
                tf = tf And Not Filename.EndsWith(".aux")
                tf = tf And Not KnownTasks.Contains(Filename)

                If tf Then
                    UnknownTasks.Add(Filename)
                End If
            Next

            If UnknownTasks.Count > 0 Then
                For Each UnknownTask As String In UnknownTasks
                    s = String.Format("{0}{1}{2}", s, UnknownTask, vbCrLf)
                Next
                MsgBox(s)
            End If

        End If
    End Sub

    Public Sub CheckForNewerVersion(CurrentVersion As String)
        ' Version example '2024.2'
        ' tag_name example '"tag_name":"v2024.1"'

        Dim tf As Boolean
        Dim s As String = ""
        Dim NewList As New List(Of String)

        Dim CurrentYear As Integer
        Dim NewYear As Integer
        Dim CurrentIdx As Integer
        Dim NewIdx As Integer

        Dim DoubleQuote As Char = Chr(34)

        Dim WC As New System.Net.WebClient

        CurrentYear = CInt(CurrentVersion.Split(CChar("."))(0))
        CurrentIdx = CInt(CurrentVersion.Split(CChar("."))(1))

        WC.Headers.Add("User-Agent: Other")  ' Get a 403 error without this.

        s = WC.DownloadString("https://api.github.com/repos/rmcanany/solidedgehousekeeper/releases/latest")

        NewList = s.Split(CChar(",")).ToList

        For Each s In NewList
            If s.Contains("tag_name") Then
                Exit For
            End If
        Next

        s = s.ToLower
        s = s.Replace(DoubleQuote, "")  ' '"tag_name":"v2024.1"' -> 'tag_name:v2024.1'
        s = s.Split(CChar(":"))(1)      ' 'tag_name:v2024.1' -> 'v2024.1'
        s = s.Replace("v", "")  ' 'v2024.1' -> '2024.1'

        Dim NewVersion As String = s

        NewYear = CInt(s.Split(CChar("."))(0))
        NewIdx = CInt(s.Split(CChar("."))(1))

        tf = NewYear > CurrentYear
        tf = tf Or (NewYear = CurrentYear) And (NewIdx > CurrentIdx)

        If tf Then
            Dim FNVA As New FormNewVersionAvailable(CurrentVersion, NewVersion)
            FNVA.ShowDialog()
        End If


    End Sub


End Class
