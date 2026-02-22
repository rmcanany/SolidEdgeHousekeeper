Option Strict On

Imports Newtonsoft.Json

Public Class UtilsPreferences

    Public Property RememberTaskSelections As Boolean

    Public Sub New()
        Me.RememberTaskSelections = True
    End Sub



    '###### FORM MAIN ######
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

    Public Sub SaveFormMainSettings(FMain As Form_Main, SavingPresets As Boolean)

        Dim tmpJSONDict As New Dictionary(Of String, String)
        Dim JSONString As String

        Dim Outfile = GetFormMainSettingsFilename(CheckExisting:=False)

        Dim FormType As Type = FMain.GetType()
        Dim PropInfos = New List(Of System.Reflection.PropertyInfo)(FormType.GetProperties())
        Dim Value As String
        Dim PropType As String

        Dim SkipProps As New List(Of String)
        SkipProps.AddRange({"version", "previewversion", "stopprocess", "listviewfilesoutofdate", "tasklist", "linkmanagementorder"})
        SkipProps.AddRange({"propertiesdata", "listofcolumns", "presets", "propertyfilters"})
        SkipProps.AddRange({"hcdebuglogger", "savedexpressions", "usea"})

        If SavingPresets Then
            SkipProps.AddRange({"setemplatepath", "sepreferencespath", "sematerialspath", "workingfilespath"})
            If Not FMain.PresetsSaveFileFilters Then
                SkipProps.AddRange({"filterasm", "filterpar", "filterpsm", "filterdft"})
            End If
        End If

        For Each PropInfo As System.Reflection.PropertyInfo In PropInfos

            PropType = PropInfo.PropertyType.Name.ToLower

            Dim PropModule As String = PropInfo.Module.ToString.ToLower

            Dim tf As Boolean
            tf = PropInfo.Module.ToString.ToLower.Contains("housekeeper")
            tf = tf Or (Not SavingPresets And {"Left", "Top", "Width", "Height"}.ToList.Contains(PropInfo.Name))
            If Not tf Then Continue For

            If SkipProps.Contains(PropInfo.Name.ToLower) Then Continue For

            Value = Nothing

            Select Case PropType
                Case "string", "double", "int32", "boolean"
                    Value = CStr(PropInfo.GetValue(FMain, Nothing))
                Case "list`1"
                    Value = JsonConvert.SerializeObject(PropInfo.GetValue(FMain, Nothing))
                    'MsgBox(String.Format("list`1 '{0}' detected", PropInfo.Name))
                Case Else
                    MsgBox(String.Format("In UP.SaveFormMainSettings: PropInfo.Name '{0}' not recognized", PropInfo.Name.ToLower))
                    'If PropInfo.Module.ToString.ToLower.Contains("housekeeper") Then tmpUnhandledPropTypes.Add(PropType)
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
                    Case Else
                        MsgBox(String.Format("In UtilsPreferences.SaveFormMainSettings: PropInfo.PropertyType.Name '{0}' not recognized", PropInfo.PropertyType.Name))
                        'If PropInfo.Module.ToString.ToLower.Contains("housekeeper") Then tmpUnhandledPropTypes.Add(PropType)
                End Select
            End If

            tmpJSONDict(PropInfo.Name) = Value

        Next

        JSONString = JsonConvert.SerializeObject(tmpJSONDict)

        IO.File.WriteAllText(Outfile, JSONString)


    End Sub

    Public Sub GetFormMainSettings(FMain As Form_Main)

        Dim tmpJSONDict As New Dictionary(Of String, String)
        Dim JSONString As String

        Dim Infile = GetFormMainSettingsFilename(CheckExisting:=True)

        Dim FormType As Type = FMain.GetType()
        Dim PropInfos = New List(Of System.Reflection.PropertyInfo)(FormType.GetProperties())

        If Not Infile = "" Then
            JSONString = IO.File.ReadAllText(Infile)

            tmpJSONDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

            For Each PropInfo As System.Reflection.PropertyInfo In PropInfos

                If tmpJSONDict.Keys.Contains(PropInfo.Name) Then
                    Dim PropTypestring = PropInfo.PropertyType.Name

                    Select Case PropInfo.PropertyType.Name.ToLower
                        Case "string"
                            PropInfo.SetValue(FMain, CStr(tmpJSONDict(PropInfo.Name)))
                        Case "double"
                            PropInfo.SetValue(FMain, CDbl(tmpJSONDict(PropInfo.Name)))
                        Case "int32"
                            PropInfo.SetValue(FMain, CInt(tmpJSONDict(PropInfo.Name)))
                        Case "boolean"
                            PropInfo.SetValue(FMain, CBool(tmpJSONDict(PropInfo.Name)))
                        Case "list`1"
                            Dim L = JsonConvert.DeserializeObject(Of List(Of String))(tmpJSONDict(PropInfo.Name))
                            PropInfo.SetValue(FMain, L)
                    End Select

                End If
            Next
        End If

    End Sub

    Public Function GetFormMainSettingsJSON() As String

        Dim JSONString As String = ""

        Dim Infile = GetFormMainSettingsFilename(CheckExisting:=True)

        If Not Infile = "" Then
            JSONString = IO.File.ReadAllText(Infile)
        End If

        Return JSONString
    End Function

    Public Sub SaveFormMainSettingsJSON(JSONString As String)

        Dim Outfile = GetFormMainSettingsFilename(CheckExisting:=False)

        IO.File.WriteAllText(Outfile, JSONString)

    End Sub



    '###### FOLDERS ######
    Public Function GetStartupDirectory() As String

        ' Returns the location of Housekeeper.exe

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
            Try
                FileIO.FileSystem.CreateDirectory(PreferencesDirectory)
            Catch ex As Exception
                Dim s As String = String.Format("Unable to create Preferences directory '{0}'.  ", PreferencesDirectory)
                s = String.Format("{0}You may not have the correct permissions.", s)
                MsgBox(s, vbOKOnly)
            End Try
        End If
    End Sub

    Public Function GetTempDirectory() As String
        Dim junk As String = IO.Path.GetTempPath ' Already has a trailing backslash
        'Dim DirName As String = $"{IO.Path.GetTempPath}\Housekeeper"
        Dim DirName As String = $"{IO.Path.GetTempPath}Housekeeper"
        If Not IO.Directory.Exists(DirName) Then IO.Directory.CreateDirectory(DirName)
        Return DirName
    End Function



    '###### SE DEFAULT FOLDERS ######

    Public Sub SetSEDefaultFolders(FMain As Form_Main)

        ' Working directory
        If Not IO.Directory.Exists(FMain.WorkingFilesPath) Then
            FMain.WorkingFilesPath = GetStartupDirectory()
        End If

        ' Install directory
        Dim SEInstallData As New SEInstallDataLib.SEInstallData
        Dim SEInstalledPath As String = SEInstallData.GetInstalledPath ' eg C:\Program Files\Siemens\Solid Edge 2025\Program
        If Not SEInstalledPath.Count <= 8 Then
            ' Strip off trailing `\Program`
            SEInstalledPath = SEInstalledPath.Substring(0, SEInstalledPath.Count - 8) ' eg C:\Program Files\Siemens\Solid Edge 2025
            If Not IO.Directory.Exists(SEInstalledPath) Then
                SEInstalledPath = FMain.WorkingFilesPath
            End If
        End If

        ' Templates
        If IO.File.Exists(FMain.AssemblyTemplate) Then  ' Handles 'Nothing' argument correctly.
            FMain.SETemplatePath = IO.Path.GetDirectoryName(FMain.AssemblyTemplate)
        ElseIf IO.File.Exists(FMain.PartTemplate) Then
            FMain.SETemplatePath = IO.Path.GetDirectoryName(FMain.PartTemplate)
        ElseIf IO.File.Exists(FMain.SheetmetalTemplate) Then
            FMain.SETemplatePath = IO.Path.GetDirectoryName(FMain.SheetmetalTemplate)
        ElseIf IO.File.Exists(FMain.DraftTemplate) Then
            FMain.SETemplatePath = IO.Path.GetDirectoryName(FMain.DraftTemplate)
        Else
            If IO.Directory.Exists($"{SEInstalledPath}\Template") Then
                FMain.SETemplatePath = $"{SEInstalledPath}\Template"
            Else
                FMain.SETemplatePath = SEInstalledPath
            End If
        End If

        ' Preferences
        If Not IO.Directory.Exists(FMain.SEPreferencesPath) Then
            If Not SEInstalledPath = GetStartupDirectory() Then  ' Startup directory also has a Preferences directory.
                If IO.Directory.Exists($"{SEInstalledPath}\Preferences") Then
                    FMain.SEPreferencesPath = $"{SEInstalledPath}\Preferences"
                Else
                    FMain.SEPreferencesPath = FMain.WorkingFilesPath
                End If
            Else
                FMain.SEPreferencesPath = FMain.WorkingFilesPath
            End If
        End If

        ' Materials
        If Not IO.Directory.Exists(FMain.SEMaterialsPath) Then
            If IO.Directory.Exists($"{FMain.SEPreferencesPath}\Materials") Then
                FMain.SEMaterialsPath = $"{FMain.SEPreferencesPath}\Materials"
            Else
                FMain.SEMaterialsPath = FMain.WorkingFilesPath
            End If
        End If

        ' LinkMgmt.txt
        If Not IO.File.Exists(FMain.LinkManagementFilename) Then
            If IO.File.Exists($"{FMain.SEPreferencesPath}\LinkMgmt.txt") Then
                FMain.LinkManagementFilename = $"{FMain.SEPreferencesPath}\LinkMgmt.txt"
            Else
                FMain.LinkManagementFilename = ""
            End If
        End If

        ' FastSearchScope.txt
        If Not IO.File.Exists(FMain.FastSearchScopeFilename) Then
            If IO.File.Exists($"{FMain.SEPreferencesPath}\FastSearchScope.txt") Then
                FMain.FastSearchScopeFilename = $"{FMain.SEPreferencesPath}\FastSearchScope.txt"
            Else
                FMain.FastSearchScopeFilename = ""
            End If
        End If

    End Sub



    '###### HELP FILE BASE URL ######

    Public Function GetHelpfileBaseURLFilename() As String
        Return String.Format("{0}\HelpfileBaseURL.txt", GetStartupDirectory)
    End Function



    '###### FILENAME CHARMAP ######
    Public Sub CreateFilenameCharmap()
        Dim UFC As New UtilsFilenameCharmap()  ' Creates the file filename_charmap.txt if it does not exist.
    End Sub



    '###### SAVED EXPRESSIONS ######
    Public Function GetSavedExpressionsFilename(CheckExisting As Boolean) As String
        Dim Filename As String = $"{GetPreferencesDirectory()}\saved_expressions.json"

        If CheckExisting Then
            If Not IO.File.Exists(Filename) Then Filename = ""
        End If

        Return Filename
    End Function

    Public Sub CreateSavedExpressions()
        Dim SavedExpressionsFilename = GetSavedExpressionsFilename(CheckExisting:=False)  'saved_expressions.json
        SavedExpressionsFilename = IO.Path.ChangeExtension(SavedExpressionsFilename, ".txt")
        If IO.File.Exists(SavedExpressionsFilename) Then Exit Sub


        If Not FileIO.FileSystem.FileExists(SavedExpressionsFilename) Then
            Dim Outlist As New List(Of String)

            Outlist.Add("[EXP]")
            Outlist.Add("Example 1")
            Outlist.Add("[EXP_TEXT]")
            Outlist.Add("")

            'Outlist.Add("'%{System.Title}' + '-' + toString(cast(substring('%{System.Subject}', lastIndexOf('%{System.Subject}', 'L=')+2, lastIndexOf('%{System.Subject}', ' ')-lastIndexOf('%{System.Subject}', 'L=')-2),'System.Int32'),'D4') + '-' + substring('%{System.Subject}', lastIndexOf('%{System.Subject}', ' ')+1)")

            Outlist.Add("'%{System.Title}' ")
            Outlist.Add("+ '-' ")
            Outlist.Add("+ toString( ")
            Outlist.Add("    cast( ")
            Outlist.Add("      substring( ")
            Outlist.Add("        '%{System.Subject}', ")
            Outlist.Add("        lastIndexOf( ")
            Outlist.Add("          '%{System.Subject}', ")
            Outlist.Add("          'L=')+2, ")
            Outlist.Add("        lastIndexOf( ")
            Outlist.Add("          '%{System.Subject}', ")
            Outlist.Add("          ' ')-lastIndexOf( ")
            Outlist.Add("                 '%{System.Subject}', ")
            Outlist.Add("                 'L=')-2), ")
            Outlist.Add("    'System.Int32'), ")
            Outlist.Add(" 'D4') ")
            Outlist.Add("+ '-' ")
            Outlist.Add("+ substring( ")
            Outlist.Add("    '%{System.Subject}', ")
            Outlist.Add("    lastIndexOf( ")
            Outlist.Add("      '%{System.Subject}', ")
            Outlist.Add("      ' ')+1) ")


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
            Outlist.Add("if ('%{System.Category}' == 'LASER','SSM',")
            Outlist.Add("if('%{System.Category}' == 'WATERJET','Kastle',")
            Outlist.Add("if('%{System.Category}' == 'FAB','Internal','')))")
            Outlist.Add("")
            Outlist.Add("\\Example of the usage of if() statement, valid inputs LASER, WATERJET, FAB, ??")
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

            Outlist.Add("[EXP]")
            Outlist.Add("Example VB ToUpper")
            Outlist.Add("[EXP_TEXT]")
            Outlist.Add("")
            Outlist.Add("Return ""%{System.Title}"".ToUpper()")
            Outlist.Add("")
            Outlist.Add("\\Any text will be converted in UPPERCASE")

            Outlist.Add("[EXP]")
            Outlist.Add("Example VB Select Case")
            Outlist.Add("[EXP_TEXT]")
            Outlist.Add("")
            Outlist.Add("Dim SubDirectory as String = """"")
            Outlist.Add("Select Case ""%{System.Category}"".ToUpper()")
            Outlist.Add("    Case ""WATERJET""")
            Outlist.Add("        SubDirectory = ""Kastle""")
            Outlist.Add("    Case ""LASER"", ""WELDMENT""")
            Outlist.Add("        SubDirectory = ""SSM""")
            Outlist.Add("    Case Else")
            Outlist.Add("        SubDirectory = ""Internal""")
            Outlist.Add("End Select")
            Outlist.Add("Return SubDirectory")
            Outlist.Add("")
            Outlist.Add("\\Create output that depends on category")


            IO.File.WriteAllLines(SavedExpressionsFilename, Outlist)
        End If
    End Sub

    'Public Function GetSavedExpressionsDict() As Dictionary(Of String, Dictionary(Of String, String))

    '    Dim SavedExpressionsDict As New Dictionary(Of String, Dictionary(Of String, String))
    '    'Format
    '    '{"Hi_Mom":
    '    '    {"Language": "VB",
    '    '     "Comments": "Simple test" + Chr(182) + "to try it out."
    '    '     "Expression": "Return "Hi Mom!""
    '    '    },
    '    ' "Example VB ToUpper":
    '    '    {"Language": "VB",
    '    '     "Comments": "Converts text to upper case"
    '    '     "Expression": "Return "%{System.Title}".ToUpper()"
    '    '    }
    '    '}

    '    Dim SavedExpressionsFilename As String = GetSavedExpressionsFilename(CheckExisting:=True) ' saved_expressions.json

    '    If Not SavedExpressionsFilename = "" Then
    '        Dim JSONString As String = IO.File.ReadAllText(SavedExpressionsFilename)
    '        SavedExpressionsDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(JSONString)

    '    Else
    '        SavedExpressionsFilename = IO.Path.ChangeExtension(GetSavedExpressionsFilename(CheckExisting:=False), ".txt")
    '        If Not IO.File.Exists(SavedExpressionsFilename) Then Return Nothing

    '        Dim SR As IO.StreamReader = IO.File.OpenText(SavedExpressionsFilename)
    '        Dim SavedExpressions = SR.ReadToEnd

    '        SR.Close()

    '        '[EXP]
    '        'Example toUpper()
    '        '[EXP_TEXT]
    '        'toUpper('%{System.Title}')
    '        '\\Any text will be converted in UPPERCASE
    '        '[EXP]
    '        'Example VB ToUpper
    '        '[EXP_TEXT]
    '        'Return "%{System.Title}".ToUpper()
    '        '\\Any text will be converted in UPPERCASE
    '        '...

    '        Dim Expressions = SavedExpressions.Split(New String() {"[EXP]"}, StringSplitOptions.RemoveEmptyEntries)

    '        For Each Expression In Expressions

    '            Dim ExpressionItems = Expression.Split(New String() {"[EXP_TEXT]"}, StringSplitOptions.RemoveEmptyEntries)

    '            If ExpressionItems.Length = 2 Then

    '                Dim SaveName As String = ExpressionItems(0).Replace(vbCrLf, "")
    '                'Dim ExpressionAndComments = ExpressionItems(1).Split(CType("\\", Char)).First
    '                Dim ExpressionAndComments = ExpressionItems(1).Split(CType("\\", Char))
    '                Dim SaveExpression As String = TrimCR(ExpressionAndComments(0))
    '                Dim SaveComments As String = ""
    '                If ExpressionAndComments.Count > 1 Then
    '                    For i As Integer = 1 To ExpressionAndComments.Count - 1
    '                        SaveComments = $"{SaveComments}{ExpressionAndComments(i)}{vbCrLf}"
    '                    Next
    '                End If
    '                SaveComments = TrimCR(SaveComments)
    '                Dim SaveLanguage As String = ""
    '                If SaveExpression.ToLower.Contains("return") Then
    '                    SaveLanguage = "VB"
    '                Else
    '                    SaveLanguage = "NCalc"
    '                End If

    '                SavedExpressionsDict(SaveName) = New Dictionary(Of String, String)
    '                SavedExpressionsDict(SaveName)("Language") = SaveLanguage
    '                SavedExpressionsDict(SaveName)("Comments") = SaveComments
    '                SavedExpressionsDict(SaveName)("Expression") = SaveExpression
    '            End If
    '        Next

    '        SaveSavedExpressionsDict(SavedExpressionsDict)
    '    End If


    '    Return SavedExpressionsDict
    'End Function

    'Public Sub SaveSavedExpressionsDict(
    '    SavedExpressionsDict As Dictionary(Of String, Dictionary(Of String, String)))

    '    Dim Filename = GetSavedExpressionsFilename(CheckExisting:=False)

    '    Dim JSONString As String = JsonConvert.SerializeObject(SavedExpressionsDict)
    '    IO.File.WriteAllText(Filename, JSONString)

    'End Sub

    'Public Function TrimCR(InString As String) As String

    '    If InString.Count > 2 Then
    '        'Leading carriage returns
    '        While InString(0) = vbCrLf Or InString(0) = vbCr Or InString(0) = vbLf
    '            InString = InString.Substring(1)
    '        End While

    '        'Trailing carriage returns
    '        While InString(InString.Count - 1) = vbCrLf Or InString(InString.Count - 1) = vbCr Or InString(InString.Count - 1) = vbLf
    '            InString = InString.Substring(0, InString.Count - 1)
    '        End While

    '        'Add one trailing carriage return
    '        InString = $"{InString}{vbCrLf}"

    '    End If

    '    Return InString
    'End Function



    '###### INTERACTIVE EDIT COMMANDS ######
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
            Outlist.Add("'ApplicationCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~ApplicationCommandConstants.html")
            Outlist.Add("'AssemblyCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~AssemblyCommandConstants.html")
            Outlist.Add("'CommandUserInterfaceConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~CommandUserInterfaceConstants.html")
            Outlist.Add("'CuttingPlaneLineCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~CuttingPlaneLineCommandConstants.html")
            Outlist.Add("'DetailCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~DetailCommandConstants.html")
            Outlist.Add("'DrawingViewEditCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~DrawingViewEditCommandConstants.html")
            Outlist.Add("'ExplodeCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~ExplodeCommandConstants.html")
            Outlist.Add("'LayoutCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~LayoutCommandConstants.html")
            Outlist.Add("'LayoutInPartCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~LayoutInPartCommandConstants.html")
            Outlist.Add("'MotionCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~MotionCommandConstants.html")
            Outlist.Add("'PartCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~PartCommandConstants.html")
            Outlist.Add("'ProfileCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~ProfileCommandConstants.html")
            Outlist.Add("'ProfileHoleCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~ProfileHoleCommandConstants.html")
            Outlist.Add("'ProfilePatternCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~ProfilePatternCommandConstants.html")
            Outlist.Add("'ProfileRevolvedCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~ProfileRevolvedCommandConstants.html")
            Outlist.Add("'SheetMetalCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~SheetMetalCommandConstants.html")
            Outlist.Add("'SimplifyCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~SimplifyCommandConstants.html")
            Outlist.Add("'SolidEdgeCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~SolidEdgeCommandConstants.html")
            Outlist.Add("'StudioCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~StudioCommandConstants.html")
            Outlist.Add("'TubingCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~TubingCommandConstants.html")
            Outlist.Add("'WeldmentCommandConstants: https://docs.sw.siemens.com/documentation/external/PL20220830878154140/en-US/api/content/SolidEdgeConstants~WeldmentCommandConstants.html")
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



    '###### EDIT PROPERTIES ######
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



    '###### PROPERTY FILTERS ######
    Public Sub SavePropertyFiltersJSON(PropertyFiltersJSON As String)

        Dim Outfile = GetPropertyFiltersFilename(CheckExisting:=False)

        IO.File.WriteAllText(Outfile, PropertyFiltersJSON)

    End Sub

    Public Function GetPropertyFiltersJSON() As String
        Dim JSONString As String = ""
        Dim Filename As String = GetPropertyFiltersFilename(CheckExisting:=True)

        If Not Filename = "" Then
            JSONString = IO.File.ReadAllText(Filename)
        End If

        Return JSONString

    End Function

    Public Function GetPropertyFiltersFilename(CheckExisting As Boolean) As String

        Dim Filename = String.Format("{0}\property_filters.json", GetPreferencesDirectory)

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



    '###### EDIT VARIABLES ######
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



    '###### TASK LIST SETTINGS ######
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

    Public Function GetTaskList(Splash As FormSplash) As List(Of Task)

        Dim TaskList As New List(Of Task)
        Dim Task As Task
        Dim JSONDict As Dictionary(Of String, String)
        Dim JSONString As String

        Dim TaskJSONDict As Dictionary(Of String, String)
        Dim TaskDescription As String
        Dim TaskName As String

        Dim Filename As String = GetTaskListFilename(CheckExisting:=True)

        Dim AvailableTasks = BuildTaskListFromScratch(Splash)

        If Filename = "" Then
            TaskList = AvailableTasks
        Else
            JSONString = IO.File.ReadAllText(Filename)

            JSONDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

            For Each TaskDescription In JSONDict.Keys
                JSONString = JSONDict(TaskDescription)
                TaskJSONDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)
                TaskName = TaskJSONDict("TaskName")

                If Splash IsNot Nothing Then Splash.UpdateStatus(String.Format("Loading {0}", TaskName))

                Task = GetNewTaskInstance(AvailableTasks, TaskName, TaskDescription)

                If Task IsNot Nothing Then
                    Task.SetFormState(JSONString)
                    TaskList.Add(Task)
                End If
            Next

        End If

        Return TaskList
    End Function

    Public Function GetTaskListJSON() As String

        Dim JSONString As String = ""
        Dim Filename As String = GetTaskListFilename(CheckExisting:=True)

        If Not Filename = "" Then
            JSONString = IO.File.ReadAllText(Filename)
        End If

        Return JSONString
    End Function

    Public Sub SaveTaskListJSON(JSONString As String)

        Dim Outfile = GetTaskListFilename(CheckExisting:=False)

        IO.File.WriteAllText(Outfile, JSONString)

    End Sub



    '###### TASK FUNCTIONS ######
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

    Public Function BuildTaskListFromScratch(Splash As FormSplash) As List(Of Task)
        Dim TaskList As New List(Of Task)
        Dim KnownTasks As New List(Of String)

        ' Update
        TaskList.Add(New TaskSetDocumentStatus)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskOpenSave)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskActivateAndUpdateAll)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskUpdateMaterialFromMaterialTable)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskUpdatePartCopies)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskUpdatePhysicalProperties)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskUpdateModelSizeInVariableTable)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskUpdateDesignForCost)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskUpdateDrawingViews)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskUpdateBlocks)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskUpdateFlatPattern)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskBreakLinks)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)

        ' Edit
        TaskList.Add(New TaskEditProperties)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskEditVariables)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskEditInteractively)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)

        ' Restyle
        TaskList.Add(New TaskUpdateModelStylesFromTemplate)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskUpdateDrawingStylesFromTemplate)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskRemoveFaceStyleOverrides)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskShowHideConstructions)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskFitView)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)

        ' Check
        TaskList.Add(New TaskCheckInterference)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskCheckLinks)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskCheckRelationships)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskCheckFlatPattern)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskCheckMaterialNotInMaterialTable)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskCheckMissingDrawing)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskCheckFilename)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskCheckPartCopies)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskCheckDrawingPartsList)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskCheckDrawings)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)

        ' Output
        TaskList.Add(New TaskRunExternalProgram)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskSaveModelAs)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskSaveDrawingAs)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskCreateDrawingOfFlatPattern)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)
        TaskList.Add(New TaskPrint)
        If Splash IsNot Nothing Then Splash.UpdateStatus(TaskList(TaskList.Count - 1).Name)

        For Each Task As Task In TaskList
            KnownTasks.Add(Task.Name.ToLower)
        Next

        CheckForUnknownTasks(KnownTasks)

        Return TaskList

    End Function

    Private Sub CheckForUnknownTasks(KnownTasks As List(Of String))

        If Not RunningDevCode() Then
            Exit Sub
        End If

        Dim HardcodedPath = String.Format("{0}\My Project", GetHardCodedPath)
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



    '###### PRESETS ######
    Public Function GetPresetsFilename(CheckExisting As Boolean) As String
        Dim Filename = "presets.json"
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



    '###### PROPERTIES DATA ######
    Public Function GetPropertiesDataFilename(CheckExisting As Boolean) As String
        Dim Filename = "properties_data.json"
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



    '###### LINK MANAGEMENT ORDER ######
    Public Function GetLinkManagementOrder() As List(Of String)

        Dim LinkManagementFilename As String = Form_Main.LinkManagementFilename

        Dim Contents As String() = Nothing
        Dim CommentString As String = "\\ "
        Dim Line As String
        Dim LinkManagementOrder As New List(Of String)
        Dim msg As String = ""

        Dim ValidOrders As New List(Of String)
        ValidOrders.AddRange({"CONTAINER", "RELATIVE", "ABSOLUTE"})

        Try
            Contents = IO.File.ReadAllLines(LinkManagementFilename)
        Catch ex As Exception
            'msg = String.Format("LinkMgmt.txt file '{0}' (on Configuration Tab) not found.{1}", LinkManagementFilename, vbCrLf)
            'MsgBox(msg)
        End Try

        If Contents IsNot Nothing Then
            If Contents.Count > 0 Then
                For Each item As String In Contents
                    Line = item.Trim.ToUpper
                    If ValidOrders.Contains(Line) Then
                        LinkManagementOrder.Add(Line)
                    End If
                Next
            Else
                'msg = String.Format("LinkMgmt.txt file '{0}' is empty.{1}", LinkManagementFilename, vbCrLf)
                'MsgBox(msg)
            End If

        End If

        For Each s As String In ValidOrders
            If Not LinkManagementOrder.Contains(s) Then LinkManagementOrder.Add(s)
        Next

        Return LinkManagementOrder
    End Function



    '###### VERSION ######
    Public Sub CheckForNewerVersion(CurrentVersion As String)
        ' Version example '2024.2' or '2024.2.1' but the last number is currently ignored for this check
        ' tag_name example '"tag_name":"v2024.1"'

        Dim tf As Boolean
        Dim s As String = ""
        Dim NewList As New List(Of String)

        Dim CurrentYear As Integer
        Dim NewYear As Integer
        Dim CurrentIdx As Integer
        Dim NewIdx As Integer

        Dim DoubleQuote As Char = Chr(34)

        Dim CurrentVersionList As List(Of String) = CurrentVersion.Split(CChar(".")).ToList

        CurrentYear = CInt(CurrentVersionList(0))
        CurrentIdx = CInt(CurrentVersionList(1))
        ' ignore bugfix number

        Try
            Dim WC As New System.Net.WebClient
            WC.Headers.Add("User-Agent: Other")  ' Get a 403 error without this.
            s = WC.DownloadString("https://api.github.com/repos/rmcanany/solidedgehousekeeper/releases/latest")
        Catch ex As Exception
            Exit Sub
        End Try

        NewList = s.Split(CChar(",")).ToList

        For Each s In NewList
            If s.Contains("tag_name") Then
                Exit For
            End If
        Next

        s = s.ToLower
        s = s.Replace(DoubleQuote, "")  ' '"tag_name":"v2024.1"' -> 'tag_name:v2024.1'
        s = s.Split(CChar(":"))(1)      ' 'tag_name:v2024.1' -> 'v2024.1'
        s = s.Replace("v", "")          ' 'v2024.1' -> '2024.1'

        Dim NewVersion As String = s

        Dim NewVersionList As List(Of String) = NewVersion.Split(CChar(".")).ToList

        NewYear = CInt(NewVersionList(0))
        NewIdx = CInt(NewVersionList(1))
        ' ignore bugfix number

        tf = NewYear > CurrentYear
        tf = tf Or (NewYear = CurrentYear) And (NewIdx > CurrentIdx)

        If tf Then
            Dim FNVA As New FormNewVersionAvailable(CurrentVersion, NewVersion)
            FNVA.ShowDialog()
        End If


    End Sub

    Public Sub CheckVersionFormat(Version As String)
        Dim s As String = ""
        Dim indent As String = "    "

        Dim CurrentVersionList As List(Of String) = Version.Split(CChar(".")).ToList
        If Not CurrentVersionList.Count = 2 Then
            If CurrentVersionList.Count = 3 Then
                ' OK
            Else
                s = String.Format("{0}Version incorrect format.  Should be 'YYYY.N', not '{1}'{2}", s, Version, vbCrLf)
            End If
        Else
            Try
                Dim i As Integer
                i = CInt(CurrentVersionList(0))
                i = CInt(CurrentVersionList(1))
            Catch ex As Exception
                s = String.Format("{0}Version incorrect format.  '{1}' contains at least one non-integer{2}", s, Version, vbCrLf)
            End Try
        End If
        If Not s = "" Then
            MsgBox(s, vbOKOnly)
        End If

    End Sub

    Public Function CheckInactive(Version As String, PreviewVersion As String) As Boolean
        ' eg SolidEdgeHousekeeper-v2025.4_BetaPreview-01.txt
        ' Version format: YYYY.N
        ' PerviewVersion format: NN

        Dim Inactive As Boolean = False
        Dim s As String = ""
        Dim FilenameString As String = $"SolidEdgeHousekeeper-v{Version}_BetaPreview-{PreviewVersion}.txt"

        If Not PreviewVersion = "" Then
            Try
                Dim WC As New System.Net.WebClient
                WC.Headers.Add("User-Agent: Other")  ' Get a 403 error without this.
                s = WC.DownloadString("https://github.com/rmcanany/SolidEdgeHousekeeper/tree/master/inactive")

                Dim NewList As List(Of String) = s.Split(CChar(",")).ToList

                'Dim tmpList As New List(Of String)
                For Each s In NewList
                    If s.ToLower.Contains(FilenameString.ToLower) Then
                        Inactive = True
                        Exit For
                    End If
                Next

            Catch ex As Exception
            End Try

        End If

        Return Inactive
    End Function



    '###### HARD CODED PATH ######
    Public Function GetHardCodedPath() As String
        Return "C:\data\CAD\scripts\SolidEdgeHousekeeper"
    End Function

    Public Function RunningDevCode() As Boolean
        Dim tf As Boolean
        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath() ' eg C:\data\CAD\scripts\SolidEdgeHousekeeper\bin\Debug
        tf = StartupPath.Contains(GetHardCodedPath)

        Return tf
    End Function

End Class
