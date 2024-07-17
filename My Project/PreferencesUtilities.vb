Option Strict On
Imports System.IO
Imports Newtonsoft.Json


Public Class PreferencesUtilities

    Public Function GetStartupDirectory() As String
        Dim StartupDirectory As String = System.Windows.Forms.Application.StartupPath()
        Return StartupDirectory
    End Function

    Public Function GetPreferencesDirectory() As String
        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
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
        Dim FCD As New FilenameCharmapDoctor()  ' Creates the file filename_charmap.txt if it does not exist.
    End Sub

    Public Sub CreateNCalcSavedExpressions()
        Dim SavedExpressionsFilename = String.Format("{0}\SavedExpressions.txt", GetPreferencesDirectory)

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
            Outlist.Add("if('%{System.Title}' == 'Cow','Hay','unknow')))")
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

    Public Sub CreateEditInteractivelyCommands()

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
        'View Backgrounds                               10211

        Dim EditInteractivelyCommandsFilename = String.Format("{0}\EditInteractivelyCommands.txt", GetPreferencesDirectory)

        If Not FileIO.FileSystem.FileExists(EditInteractivelyCommandsFilename) Then

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

            IO.File.WriteAllLines(EditInteractivelyCommandsFilename, Outlist)
        End If


    End Sub
    Private Function GetTaskListPath(CheckExisting As Boolean) As String
        Dim Filename = "tasklist.json"
        Dim TaskListPath = String.Format("{0}\{1}", GetPreferencesDirectory, Filename)

        If CheckExisting Then
            If FileIO.FileSystem.FileExists(TaskListPath) Then
                Return TaskListPath
            Else
                Return ""
            End If
        Else
            Return TaskListPath
        End If
    End Function

    Public Sub SaveTaskList(TaskList As List(Of Task))

        Dim Filename As String = GetTaskListPath(CheckExisting:=False)

        Dim tmpJSONDict As New Dictionary(Of String, String)
        Dim JSONString As String

        Dim Outfile = GetTaskListPath(CheckExisting:=False)

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

        Dim Filename As String = GetTaskListPath(CheckExisting:=True)

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
        TaskList.Add(New TaskOptimize)
        TaskList.Add(New TaskOpenSave)
        TaskList.Add(New TaskActivateAndUpdateAll)
        TaskList.Add(New TaskUpdateMaterialFromMaterialTable)
        TaskList.Add(New TaskUpdatePartCopies)
        TaskList.Add(New TaskUpdatePhysicalProperties)
        TaskList.Add(New TaskUpdateModelSizeInVariableTable)
        TaskList.Add(New TaskUpdateDesignForCost)
        TaskList.Add(New TaskUpdateDrawingViews)
        TaskList.Add(New TaskUpdateFlatPattern)
        TaskList.Add(New TaskBreakPartCopyLinks)

        ' Edit
        TaskList.Add(New TaskEditProperties)
        TaskList.Add(New TaskEditVariables)
        TaskList.Add(New TaskEditInteractively)

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
        TaskList.Add(New TaskPrint)

        For Each Task As Task In TaskList
            Task.RememberTaskSelections = Form1.RememberTaskSelections
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

End Class
