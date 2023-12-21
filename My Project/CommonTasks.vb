'Option Strict On
Imports System.IO
Imports System.Net.WebRequestMethods
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports ExcelDataReader
Imports MS.Internal
Imports Newtonsoft.Json
Imports SolidEdgeFrameworkSupport

Public Class CommonTasks

    Public Shared tmpList As Collection
    Shared Function SaveAsPNG(View As SolidEdgeFramework.View,
                               NewFilename As String
                               ) As String

        Dim ExitMessage As String = ""
        Dim TempFilename As String = NewFilename.Replace(".png", "-Housekeeper.tif")

        Try

            View.SaveAsImage(TempFilename)

            Try

                Using fs As New IO.FileStream(TempFilename, IO.FileMode.Open, IO.FileAccess.Read)
                    Image.FromStream(fs).Save(NewFilename, Imaging.ImageFormat.Png)
                End Using

                IO.File.Delete(TempFilename)

            Catch ex As Exception
                ExitMessage = String.Format("Unable to save '{0}'.  ", NewFilename)
            End Try

        Catch ex As Exception
            ExitMessage = String.Format("Unable to save '{0}'.  ", TempFilename)
        End Try

        Return ExitMessage

    End Function

    Shared Function TruncateFullPath(ByVal Path As String,
    Configuration As Dictionary(Of String, String)
    ) As String

        'Dim Length As Integer = Len(Configuration("TextBoxInputDirectory"))
        'Dim NewPath As String

        'If Path.Contains(Configuration("TextBoxInputDirectory")) Then
        '    NewPath = Path.Remove(0, Length)
        '    NewPath = "~" + NewPath
        'Else
        '    NewPath = Path
        'End If
        Return Path
    End Function

    Shared Function RunExternalProgram(
        ExternalProgram As String,
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        Configuration As Dictionary(Of String, String),
        SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'Dim ExternalProgram As String = Configuration("TextBoxExternalProgramAssembly")

        Dim ExternalProgramDirectory As String = System.IO.Path.GetDirectoryName(ExternalProgram)
        Dim P As New Process
        Dim ExitCode As Integer
        Dim ErrorMessageFilename As String
        Dim ErrorMessages As String()


        P = Process.Start(ExternalProgram)
        P.WaitForExit()
        ExitCode = P.ExitCode  ' If the program doesn't supply one, what value can it take?  Null?

        ErrorMessageFilename = String.Format("{0}\error_messages.txt", ExternalProgramDirectory)

        If ExitCode <> 0 Then
            ExitStatus = 1
            If FileIO.FileSystem.FileExists(ErrorMessageFilename) Then
                ErrorMessages = IO.File.ReadAllLines(ErrorMessageFilename)
                If ErrorMessages.Length > 0 Then
                    For Each ErrorMessageFromProgram As String In ErrorMessages
                        ErrorMessageList.Add(ErrorMessageFromProgram)
                    Next
                Else
                    ErrorMessageList.Add(String.Format("Program terminated with exit code {0}", ExitCode))
                End If

                IO.File.Delete(ErrorMessageFilename)
            Else
                ErrorMessageList.Add(String.Format("Program terminated with exit code {0}", ExitCode))
            End If
        Else

        End If

        If Configuration("CheckBoxRunExternalProgramSaveFile").ToLower = "true" Then
            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        End If


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Shared Function InteractiveEdit(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Result As MsgBoxResult
        Dim msg As String
        Dim indent As String = "    "

        SEApp.DisplayAlerts = True

        msg = String.Format("When finished, do one of the following:{0}", vbCrLf)
        msg = String.Format("{0}{1}Click Yes to save and close{2}", msg, indent, vbCrLf)
        msg = String.Format("{0}{1}Click No to close without saving{2}", msg, indent, vbCrLf)
        msg = String.Format("{0}{1}Click Cancel to quit{2}", msg, indent, vbCrLf)

        Result = MsgBox(msg, MsgBoxStyle.YesNoCancel Or MsgBoxStyle.SystemModal, Title:="Solid Edge Housekeeper")

        If Result = vbYes Then
            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save read-only file.")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        ElseIf Result = vbNo Then
            '    ExitStatus = 1
            '    ErrorMessageList.Add("File was not saved.")
        Else  ' Cancel was chosen
            ExitStatus = 99
            ErrorMessageList.Add("Operation was cancelled.")
        End If

        SEApp.DisplayAlerts = False

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Public Shared Sub CopyProperties(Source As Object, Destination As Object)

        Dim destType As Type = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetType(Destination)
        Dim sourceType As Type = SolidEdgeCommunity.Runtime.InteropServices.ComObject.GetType(Source)

        Dim destProps() As PropertyInfo = destType.GetProperties()
        Dim sourceProps() As PropertyInfo = sourceType.GetProperties()

        For Each sourceProp As PropertyInfo In sourceProps
            For Each destProp As PropertyInfo In destProps
                If destProp.CanWrite Then
                    If destProp.Name <> "Parent" Then
                        If destProp.Name = sourceProp.Name Then
                            If destProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType) Then
                                Try
                                    Dim tmpValue = sourceProp.GetValue(Source, Nothing)
                                    If tmpValue <> Nothing Then destProp.SetValue(Destination, tmpValue, Nothing)
                                Catch ex As Exception
                                    'Console.WriteLine(destType.FullName & " - " & destProp.Name)
                                End Try
                            End If
                        End If
                    End If
                End If
            Next
        Next

    End Sub

    Shared Function SubstitutePropertyFormula(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Instring As String
        ) As String

        ' Replaces property formulas in a string
        ' "Material: %{System.Material}, Engineer: %{Custom.Engineer}" --> "Material: STEEL, Engineer: FRED"

        Dim Outstring As String = ""
        Dim tf As Boolean
        Dim Proceed As Boolean = True

        Dim PropertySet As String
        Dim PropertyName As String
        Dim FoundProp As SolidEdgeFramework.Property

        Dim DocValues As New List(Of String)
        'Dim DocValue As String

        'Dim StartPositions As New List(Of Integer)
        'Dim StartPosition As Integer
        'Dim EndPositions As New List(Of Integer)
        'Dim EndPosition As Integer
        'Dim Length As Integer
        Dim i As Integer
        'Dim msg As String


        Dim Formulas As New List(Of String)
        Dim Formula As String

        Dim Matches As MatchCollection
        Dim MatchString As Match
        Dim Pattern As String

        Dim ModelIdx As Integer


        tf = Instring.Contains("%{System")
        tf = tf Or Instring.Contains("%{Custom")

        If Not tf Then
            Outstring = Instring
            Proceed = False
        End If

        If Proceed Then
            ' Any number of substrings that start with "%{" and end with the first encountered "}".
            Pattern = "%{[^}]*}"
            Matches = Regex.Matches(Instring, Pattern)
            For Each MatchString In Matches
                Formulas.Add(MatchString.Value)
            Next
        End If

        If Proceed Then
            For Each Formula In Formulas
                Formula = Formula.Replace("%{", "")  ' "%{Custom.hmk_Engineer|R1}" -> "Custom.hmk_Engineer|R1}"
                Formula = Formula.Replace("}", "")   ' "Custom.hmk_Engineer|R1}" -> "Custom.hmk_Engineer|R1"
                i = Formula.IndexOf(".")  ' First occurrence
                PropertySet = Formula.Substring(0, i)    ' "Custom"
                PropertyName = Formula.Substring(i + 1)  ' "hmk_Engineer|R1"
                If PropertyName.Contains("|R") Then
                    i = PropertyName.IndexOf("|")
                    ModelIdx = CInt(PropertyName.Substring(i + 2))  ' "hmk_Engineer|R1" -> "1"
                    PropertyName = PropertyName.Substring(0, i)  ' "hmk_Engineer|R1" -> "hmk_Engineer"
                Else
                    ModelIdx = 0
                End If

                'Check for special properties %{File Name}, %{File Name (full path)}, %{File Name (no extension)}

                If PropertyName.ToLower = "File Name".ToLower Then
                    DocValues.Add(System.IO.Path.GetFileName(SEDoc.FullName))  ' C:\project\part.par -> part.par
                ElseIf PropertyName.ToLower = "File Name (full path)".ToLower Then
                    DocValues.Add(SEDoc.FullName)
                ElseIf PropertyName.ToLower = "File Name (no extension)".ToLower Then
                    DocValues.Add(System.IO.Path.GetFileNameWithoutExtension(SEDoc.FullName))  ' C:\project\part.par -> part
                Else
                    FoundProp = GetProp(SEDoc, PropertySet, PropertyName, ModelIdx, False)
                    If Not FoundProp Is Nothing Then
                        DocValues.Add(FoundProp.Value)
                    Else
                        DocValues.Add("")
                    End If

                End If

            Next
        End If

        If Proceed Then
            Outstring = Instring

            For i = 0 To DocValues.Count - 1
                Outstring = Outstring.Replace(Formulas(i), DocValues(i))
            Next

        End If


        Return Outstring

    End Function

    Shared Function GetProp(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        PropertySetName As String,
        PropertyName As String,
        ModelLinkIdx As Integer,
        AddProp As Boolean
        ) As SolidEdgeFramework.Property

        Dim PropertySets As SolidEdgeFramework.PropertySets = Nothing
        Dim Properties As SolidEdgeFramework.Properties = Nothing
        Dim Prop As SolidEdgeFramework.Property = Nothing
        Dim FoundProp As SolidEdgeFramework.Property = Nothing
        Dim Proceed As Boolean = True
        Dim tf As Boolean
        Dim tf1 As Boolean
        Dim tf2 As Boolean
        Dim PropertyFound As Boolean

        If ModelLinkIdx = 0 Then
            Try
                PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)
            Catch ex As Exception
                Proceed = False
            End Try
        Else
            Dim ModelLink As SolidEdgeDraft.ModelLink
            Dim Typename As String = ""
            Dim ModelDocName As String = ""

            Try
                ModelLink = SEDoc.ModelLinks.Item(ModelLinkIdx)
                Typename = GetDocType(ModelLink.ModelDocument)

                tf = True

                If Typename.ToLower = "par" Then
                    Dim ModelDoc As SolidEdgePart.PartDocument = CType(ModelLink.ModelDocument, SolidEdgePart.PartDocument)
                    PropertySets = CType(ModelDoc.Properties, SolidEdgeFramework.PropertySets)
                    ModelDocName = ModelDoc.FullName
                End If

                If Typename.ToLower = "psm" Then
                    Dim ModelDoc As SolidEdgePart.SheetMetalDocument = CType(ModelLink.ModelDocument, SolidEdgePart.SheetMetalDocument)
                    PropertySets = CType(ModelDoc.Properties, SolidEdgeFramework.PropertySets)
                    ModelDocName = ModelDoc.FullName
                End If

                If Typename.ToLower = "asm" Then
                    Dim ModelDoc As SolidEdgeAssembly.AssemblyDocument = CType(ModelLink.ModelDocument, SolidEdgeAssembly.AssemblyDocument)
                    PropertySets = CType(ModelDoc.Properties, SolidEdgeFramework.PropertySets)
                    ModelDocName = ModelDoc.FullName
                End If
            Catch ex As Exception
                Proceed = False
            End Try

        End If


        If Proceed Then
            For Each Properties In PropertySets
                For Each Prop In Properties
                    'Check if both names are 'custom' or neither are
                    tf1 = (PropertySetName.ToLower = "custom") And (Properties.Name.ToLower = "custom")
                    tf2 = (PropertySetName.ToLower <> "custom") And (Properties.Name.ToLower <> "custom")

                    tf = tf1 Or tf2
                    If Not tf Then
                        Continue For
                    End If

                    ' Some properties do not have names.
                    Try
                        If Prop.Name = PropertyName Then
                            FoundProp = Prop
                            PropertyFound = True
                            Exit For
                        End If
                    Catch ex As Exception
                    End Try

                Next

                If PropertyFound Then
                    Exit For
                End If
            Next

        End If

        'If the property was not found, and the PropertySetName is "Custom", add it.
        tf = (AddProp) And (Not PropertyFound) And (PropertySetName.ToLower = "custom")
        If tf Then
            Try
                Properties = PropertySets.Item("Custom")
                FoundProp = Properties.Add(PropertyName, "")
                Properties.Save()
                PropertySets.Save()
            Catch ex As Exception

            End Try

        End If


        Return FoundProp

    End Function

    Shared Function PropertyFindReplace(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        ' Convert glob to regex 
        ' https://stackoverflow.com/questions/74683013/regex-to-glob-and-vice-versa-conversion
        ' https://stackoverflow.com/questions/11276909/how-to-convert-between-a-glob-pattern-and-a-regexp-pattern-in-ruby
        ' https://learn.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim PropertySets As SolidEdgeFramework.PropertySets = Nothing
        Dim Properties As SolidEdgeFramework.Properties = Nothing
        Dim Prop As SolidEdgeFramework.Property = Nothing

        Dim PropertySetName As String = ""
        Dim PropertyName As String = ""
        Dim FindString As String = ""
        Dim ReplaceString As String = ""
        Dim FindSearchType As String = ""
        Dim ReplaceSearchType As String = ""

        Dim PropertyFound As Boolean = False
        Dim AutoAddMissingProperty As Boolean = Configuration("CheckBoxAutoAddMissingProperty").ToLower = "true"

        Dim Proceed As Boolean = True

        Dim DocType As String = GetDocType(SEDoc)

        If DocType = "asm" Then
            PropertySetName = Configuration("ComboBoxFindReplacePropertySetAssembly")
            PropertyName = Configuration("TextBoxFindReplacePropertyNameAssembly")
            FindString = Configuration("TextBoxFindReplaceFindAssembly")
            ReplaceString = Configuration("TextBoxFindReplaceReplaceAssembly")

            If Configuration("CheckBoxFindReplaceFindPTAssembly") = "True" Then
                FindSearchType = "PT"
            ElseIf Configuration("CheckBoxFindReplaceFindWCAssembly") = "True" Then
                FindSearchType = "WC"
            Else
                FindSearchType = "RX"
            End If

            If Configuration("CheckBoxFindReplaceReplacePTAssembly") = "True" Then
                ReplaceSearchType = "PT"
            Else
                ReplaceSearchType = "RX"
            End If

        ElseIf DocType = "par" Then
            PropertySetName = Configuration("ComboBoxFindReplacePropertySetPart")
            PropertyName = Configuration("TextBoxFindReplacePropertyNamePart")
            FindString = Configuration("TextBoxFindReplaceFindPart")
            ReplaceString = Configuration("TextBoxFindReplaceReplacePart")

            If Configuration("CheckBoxFindReplaceFindPTPart") = "True" Then
                FindSearchType = "PT"
            ElseIf Configuration("CheckBoxFindReplaceFindWCPart") = "True" Then
                FindSearchType = "WC"
            Else
                FindSearchType = "RX"
            End If

            If Configuration("CheckBoxFindReplaceReplacePTPart") = "True" Then
                ReplaceSearchType = "PT"
            Else
                ReplaceSearchType = "RX"
            End If

        ElseIf DocType = "psm" Then
            PropertySetName = Configuration("ComboBoxFindReplacePropertySetSheetmetal")
            PropertyName = Configuration("TextBoxFindReplacePropertyNameSheetmetal")
            FindString = Configuration("TextBoxFindReplaceFindSheetmetal")
            ReplaceString = Configuration("TextBoxFindReplaceReplaceSheetmetal")

            If Configuration("CheckBoxFindReplaceFindPTSheetmetal") = "True" Then
                FindSearchType = "PT"
            ElseIf Configuration("CheckBoxFindReplaceFindWCSheetmetal") = "True" Then
                FindSearchType = "WC"
            Else
                FindSearchType = "RX"
            End If

            If Configuration("CheckBoxFindReplaceReplacePTSheetmetal") = "True" Then
                ReplaceSearchType = "PT"
            Else
                ReplaceSearchType = "RX"
            End If

        Else  ' Must be a Draft file.  Hopefully.
            PropertySetName = Configuration("ComboBoxFindReplacePropertySetDraft")
            PropertyName = Configuration("TextBoxFindReplacePropertyNameDraft")
            FindString = Configuration("TextBoxFindReplaceFindDraft")
            ReplaceString = Configuration("TextBoxFindReplaceReplaceDraft")

            If Configuration("CheckBoxFindReplaceFindPTDraft") = "True" Then
                FindSearchType = "PT"
            ElseIf Configuration("CheckBoxFindReplaceFindWCDraft") = "True" Then
                FindSearchType = "WC"
            Else
                FindSearchType = "RX"
            End If

            If Configuration("CheckBoxFindReplaceReplacePTDraft") = "True" Then
                ReplaceSearchType = "PT"
            Else
                ReplaceSearchType = "RX"
            End If

        End If

        If Proceed Then
            Try
                FindString = SubstitutePropertyFormula(SEDoc, FindString)
            Catch ex As Exception
                Proceed = False
                ExitStatus = 1
                ErrorMessageList.Add("Find text not recognized.")
            End Try

            Try
                ReplaceString = SubstitutePropertyFormula(SEDoc, ReplaceString)
            Catch ex As Exception
                Proceed = False
                ExitStatus = 1
                ErrorMessageList.Add("Replace text not recognized.")
            End Try
        End If

        If Proceed Then
            Try
                Prop = GetProp(SEDoc, PropertySetName, PropertyName, 0, AutoAddMissingProperty)
                If Prop Is Nothing Then
                    Proceed = False
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("Property '{0}.{1}' not found or not recognized.", PropertySetName, PropertyName))
                End If
            Catch ex As Exception
                Proceed = False
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Property '{0}.{1}' not found or not recognized.", PropertySetName, PropertyName))
            End Try

        End If

        If Proceed Then
            Try
                If FindSearchType = "PT" Then
                    Prop.Value = Replace(CType(Prop.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)
                Else
                    If FindSearchType = "WC" Then
                        FindString = CommonTasks.GlobToRegex(FindString)
                    End If
                    If ReplaceSearchType = "PT" Then
                        ' ReplaceString = Regex.Escape(ReplaceString)
                    End If

                    Prop.Value = Regex.Replace(CType(Prop.Value, String), FindString, ReplaceString, RegexOptions.IgnoreCase)

                End If
                ' Properties.Save()
            Catch ex As Exception
                Proceed = False
                ExitStatus = 1
                ErrorMessageList.Add("Unable to replace property value.  This command only works on text type properties.")
            End Try

        End If

        If Proceed Then
            Try
                PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)
                For Each Properties In PropertySets
                    Properties.Save()
                    SEApp.DoIdle()
                Next
                If SEDoc.ReadOnly Then
                    ExitStatus = 1
                    ErrorMessageList.Add("Cannot save document marked 'Read Only'")
                Else
                    SEDoc.Save()
                    SEApp.DoIdle()
                End If

            Catch ex As Exception
                Proceed = False
                ExitStatus = 1
                ErrorMessageList.Add("Problem accessing or saving Property.")
            End Try
        End If


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Public Shared Function GlobToRegex(GlobString As String) As String

        Dim wildcard As String = GlobString
        Dim outstring As String = ""
        Dim in_character_class As Boolean = False
        Dim r As String


        For i As Integer = 0 To Len(wildcard) - 1
            If in_character_class Then
                If wildcard(i) = "]" Then
                    in_character_class = False
                    outstring = outstring + "_META_CLOSE_BRACKET_"
                ElseIf wildcard(i) = "*" Then
                    outstring = outstring + "_LITERAL_ASTERIX_"
                ElseIf wildcard(i) = "?" Then
                    outstring = outstring + "_LITERAL_QUESTION_MARK_"
                ElseIf wildcard(i) = "[" Then
                    outstring = outstring + "_LITERAL_OPEN_BRACKET_"
                ElseIf wildcard(i) = "#" Then
                    outstring = outstring + "_LITERAL_HASH_MARK_"
                Else
                    outstring = outstring + wildcard(i)
                End If
            Else
                If wildcard(i) = "[" Then
                    in_character_class = True
                    outstring = outstring + "_META_OPEN_BRACKET_"
                Else
                    outstring = outstring + wildcard(i)
                End If
            End If
        Next

        r = Regex.Escape(outstring)

        r = r.Replace("\*", ".*")
        r = r.Replace("\?", ".")
        r = r.Replace("\[", "[")
        ' r = r.Replace("[!", "[^")
        r = r.Replace("_META_OPEN_BRACKET_!", "[^")
        r = r.Replace("_META_OPEN_BRACKET_", "[")
        r = r.Replace("\#", "[0-9]")
        r = r.Replace("_LITERAL_OPEN_BRACKET_", "\[")
        r = r.Replace("_META_CLOSE_BRACKET_", "]")
        r = r.Replace("_LITERAL_ASTERIX_", "\*")
        r = r.Replace("_LITERAL_QUESTION_MARK_", "\?")
        r = r.Replace("_LITERAL_HASH_MARK_", "\#")

        ' The following prevents a second match on the ending empty string.
        ' https://stackoverflow.com/questions/52351217/why-do-some-regex-engines-match-twice-in-a-single-input-string/52352744#52352744
        If r = ".*" Then
            r = "^.*"
        End If

        Return r

    End Function


    Shared Function ReadExcel(FileName As String) As String()

        Dim tmpList As String() = Nothing
        Dim i As Integer = 0

        Using stream = System.IO.File.Open(FileName, FileMode.Open, FileAccess.Read)
            ' Auto-detect format, supports:
            '  - Binary Excel files (2.0-2003 format; *.xls)
            '  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
            Using reader = ExcelReaderFactory.CreateReader(stream)
                ' Choose one of either 1 or 2:

                ' 1. Use the reader methods
                Do
                    While reader.Read()
                        i += 1
                        ReDim Preserve tmpList(i)
                        tmpList(i) = reader.GetValue(0)
                    End While
                Loop While reader.NextResult()

                '' 2. Use the AsDataSet extension method
                'Dim result = reader.AsDataSet()

                '' The result of each spreadsheet is in result.Tables
            End Using
        End Using

        Return tmpList

    End Function

    Public Shared Function FilenameIsOK(ByVal fileName As String) As Boolean

        Try
            Dim fi As New IO.FileInfo(fileName)
        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function

    Shared Function GetDocTypeByExtension(Filename As String) As String
        Dim Extension As String = IO.Path.GetExtension(Filename)
        Return Extension
    End Function

    Shared Function GetDocType(SEDoc As SolidEdgeFramework.SolidEdgeDocument) As String
        ' See SolidEdgeFramework.DocumentTypeConstants

        ' If the type is not recognized, the empty string is returned.
        Dim DocType As String = ""

        Select Case SEDoc.Type

            Case Is = 3, 7, 10 'asm
                DocType = "asm"

            Case Is = 1, 8 'par
                DocType = "par"

            Case Is = 4, 9 'psm
                DocType = "psm"

            Case Is = 2 'dft
                DocType = "dft"

        End Select

        Return DocType
    End Function

    Shared Function CropImage(Configuration As Dictionary(Of String, String),
                          SEDoc As SolidEdgeFramework.SolidEdgeDocument,
                          NewFilename As String,
                          NewExtension As String,
                          WindowH As Integer,
                          WindowW As Integer
                          ) As String

        Dim ModelX As Double
        Dim ModelY As Double
        Dim ModelZ As Double
        Dim XMin As Double
        Dim YMin As Double
        Dim ZMin As Double
        Dim XMax As Double
        Dim YMax As Double
        Dim ZMax As Double

        Dim ImageW As Double
        Dim ImageH As Double
        Dim ImageAspectRatio As Double

        Dim CropW As Integer
        Dim CropH As Integer

        Dim TempFilename As String

        Dim ExitMessage As String = ""

        Dim WindowAspectRatio As Double = WindowH / WindowW

        Select Case SEDoc.Type
            Case Is = 3, 7, 10 'asm
                Dim tmpAsm As SolidEdgeAssembly.AssemblyDocument = SEDoc
                tmpAsm.Range(XMin, YMin, ZMin, XMax, YMax, ZMax)
                ModelX = XMax - XMin
                ModelY = YMax - YMin
                ModelZ = ZMax - ZMin

            Case Is = 1, 8 'par
                Dim tmpPar As SolidEdgePart.PartDocument = SEDoc
                tmpPar.Range(XMin, YMin, ZMin, XMax, YMax, ZMax)
                ModelX = XMax - XMin
                ModelY = YMax - YMin
                ModelZ = ZMax - ZMin

            Case Is = 4, 9 'psm
                Dim tmpPsm As SolidEdgePart.SheetMetalDocument = SEDoc
                Dim Models As SolidEdgePart.Models
                Dim Model As SolidEdgePart.Model
                Dim Body As SolidEdgeGeometry.Body

                Dim FeatureDoctor As New FeatureDoctor
                Dim PointsList As New List(Of Double)
                Dim PointsListTemp As New List(Of Double)
                Dim Point As Double

                Models = tmpPsm.Models

                If (Models.Count = 0) Then
                    ExitMessage = "No models to process.  Cropped image not created."
                    Return ExitMessage
                End If
                If (Models.Count = 0) Or (Models.Count > 25) Then
                    ExitMessage = "Too many models to process.  Cropped image not created."
                    Return ExitMessage
                End If

                For Each Model In Models
                    Body = CType(Model.Body, SolidEdgeGeometry.Body)
                    PointsListTemp = FeatureDoctor.GetBodyRange(Body)
                    If PointsList.Count = 0 Then
                        For Each Point In PointsListTemp
                            PointsList.Add(Point)
                        Next
                    Else
                        For i As Integer = 0 To 2
                            If PointsListTemp(i) < PointsList(i) Then
                                PointsList(i) = PointsListTemp(i)
                            End If
                        Next
                        For i As Integer = 3 To 5
                            If PointsListTemp(i) > PointsList(i) Then
                                PointsList(i) = PointsListTemp(i)
                            End If
                        Next
                    End If
                Next

                ModelX = PointsList(3) - PointsList(0) 'XMax - XMin
                ModelY = PointsList(4) - PointsList(1) ' YMax - YMin
                ModelZ = PointsList(5) - PointsList(2) ' ZMax - ZMin

        End Select

        If Configuration("RadioButtonPictorialViewIsometric").ToLower = "true" Then
            ImageW = 0.707 * ModelX + 0.707 * ModelY
            ImageH = 0.40833 * ModelX + 0.40833 * ModelY + 0.81689 * ModelZ
        ElseIf Configuration("RadioButtonPictorialViewDimetric").ToLower = "true" Then
            ImageW = 0.9356667 * ModelX + 0.353333 * ModelY
            ImageH = 0.117222 * ModelX + 0.311222 * ModelY + 0.942444 * ModelZ
        Else
            ImageW = 0.557 * ModelX + 0.830667 * ModelY
            ImageH = 0.325444 * ModelX + 0.217778 * ModelY + 0.920444 * ModelZ
        End If

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
        Dim CropRect As New Rectangle(LocX, LocY, CropW, CropH)
        Dim OriginalImage = Image.FromFile(NewFilename)
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
                ExitMessage = String.Format("Unable to save cropped image '{0}'", NewFilename)
            End Try

        Catch ex As Exception
            ExitMessage = String.Format("Unable to save cropped image '{0}'", TempFilename)
        End Try

        Return ExitMessage

    End Function

    Shared Sub FindLinked(objDoc As Object)

        Dim i As Short
        Dim objLinkedDocs As Object

        ' Loop through the items in the assembly
        objLinkedDocs = objDoc.LinkedDocuments

        If Not IsNothing(objLinkedDocs) Then

            For i = 1 To objLinkedDocs.Count
                If Not tmpList.Contains(objLinkedDocs(i).FullName.ToString) Then
                    tmpList.Add(objLinkedDocs(i).FullName, objLinkedDocs(i).FullName.ToString)
                    If IO.Path.GetExtension(objLinkedDocs(i).fullname).ToLower = ".asm" Then
                        ' there is a problem in traversing back up through the tree that throws an exception, so using a try-catch.
                        Try
                            FindLinked(objLinkedDocs(i))
                        Catch ex As Exception
                            'MsgBox("Problem with traversing back up the tree " & objLinkedDocs(i).fullname)
                        End Try
                    End If
                End If
            Next

        End If

        objLinkedDocs = Nothing

    End Sub

    Shared Function SetStatus(
        DMApp As DesignManager.Application,
        Filename As String,
        NewStatus As SolidEdgeConstants.DocumentStatus,
        Optional StatusChangeRadioButtons As List(Of RadioButton) = Nothing
        ) As Boolean
        ' https://community.sw.siemens.com/s/question/0D54O000061wzRaSAI/changing-document-status

        Dim Success As Boolean = True

        Try
            'Dim DMApp As New DesignManager.Application
            'DMApp.Visible = 0

            If Not StatusChangeRadioButtons Is Nothing Then
                ' Replace NewStatus with change indicated
            End If

            Dim PropertySets As DesignManager.PropertySets
            Dim PropertySet As DesignManager.Properties

            PropertySets = CType(DMApp.PropertySets, DesignManager.PropertySets)
            PropertySets.Open(Filename, False)

            PropertySet = PropertySets.Item("ExtendedSummaryInformation")
            PropertySet.Item("Status").Value = NewStatus
            'PropertySet.Save()
            PropertySets.Save()

            PropertySet = PropertySets.Item("SummaryInformation")
            If NewStatus = SolidEdgeConstants.DocumentStatus.igStatusAvailable Then
                PropertySet.Item("Security").Value = StatusSecurityMapping.ssmAvailable
            End If
            If NewStatus = SolidEdgeConstants.DocumentStatus.igStatusBaselined Then
                PropertySet.Item("Security").Value = StatusSecurityMapping.ssmBaselined
            End If
            If NewStatus = SolidEdgeConstants.DocumentStatus.igStatusInReview Then
                PropertySet.Item("Security").Value = StatusSecurityMapping.ssmInReview
            End If
            If NewStatus = SolidEdgeConstants.DocumentStatus.igStatusInWork Then
                PropertySet.Item("Security").Value = StatusSecurityMapping.ssmInWork
            End If
            If NewStatus = SolidEdgeConstants.DocumentStatus.igStatusObsolete Then
                PropertySet.Item("Security").Value = StatusSecurityMapping.ssmObsolete
            End If
            If NewStatus = SolidEdgeConstants.DocumentStatus.igStatusReleased Then
                PropertySet.Item("Security").Value = StatusSecurityMapping.ssmReleased
            End If

            PropertySets.Save()
            PropertySets.Close()

            'DMApp.Quit()
        Catch ex As Exception
            Success = False
        End Try

        Return Success
    End Function

    Public Enum StatusSecurityMapping
        ssmAvailable = 0
        ssmInWork = 0
        ssmInReview = 0
        ssmReleased = 4
        ssmBaselined = 4
        ssmObsolete = 4
    End Enum


    Shared Function GetStatus(DMApp As DesignManager.Application, Filename As String) As SolidEdgeConstants.DocumentStatus
        Dim Status As SolidEdgeConstants.DocumentStatus = Nothing

        'Dim DMApp As New DesignManager.Application
        'DMApp.Visible = 0

        Try

            Dim PropertySets As DesignManager.PropertySets
            Dim PropertySet As DesignManager.Properties

            PropertySets = CType(DMApp.PropertySets, DesignManager.PropertySets)
            PropertySets.Open(Filename, True)

            PropertySet = PropertySets.Item("ExtendedSummaryInformation")

            Status = PropertySet.Item("Status").Value

            PropertySets.Close()

        Catch ex As Exception

        End Try

        'DMApp.Quit()

        Return Status
    End Function



    Shared Function VariablesEdit(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim SupplementalExitStatus As Integer = 0

        Dim VariablesToEditDict As New Dictionary(Of String, Dictionary(Of String, String))

        Dim VariableName As String

        Dim DocDimensionDict As New Dictionary(Of String, SolidEdgeFrameworkSupport.Dimension)
        Dim DocVariableDict As New Dictionary(Of String, SolidEdgeFramework.variable)

        Dim Variables As SolidEdgeFramework.Variables = Nothing
        Dim VariableListObject As SolidEdgeFramework.VariableList = Nothing
        Dim Variable As SolidEdgeFramework.variable = Nothing
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension = Nothing
        Dim VariableTypeName As String

        Dim VariablesToEdit As String = ""
        Dim VariablesToExposeDict As New Dictionary(Of String, String)

        Dim DocType As String

        'Dim Proceed As Boolean = True
        Dim tf As Boolean
        'Dim s As String

        DocType = GetDocType(SEDoc)
        If DocType = "asm" Then VariablesToEdit = Configuration("TextBoxVariablesEditAssembly")
        If DocType = "par" Then VariablesToEdit = Configuration("TextBoxVariablesEditPart")
        If DocType = "psm" Then VariablesToEdit = Configuration("TextBoxVariablesEditSheetmetal")
        If DocType = "dft" Then VariablesToEdit = Configuration("TextBoxVariablesEditDraft")

        If Not VariablesToEdit = "" Then
            VariablesToEditDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(VariablesToEdit)
            ' Dictionary layout
            '{
            '    "v1":
            '    {
            '        "Formula":"1",
            '        "Units":"Distance",
            '        "Expose":"True",
            '        "ExposeName":"var1"
            '    },
            '    ...
            '}

        Else
            ExitStatus = 1
            ErrorMessageList.Add("No variables provided")
        End If

        If ExitStatus = 0 Then
            Try
                Variables = DirectCast(SEDoc.Variables, SolidEdgeFramework.Variables)

                VariableListObject = DirectCast(Variables.Query(pFindCriterium:="*",
                                  NamedBy:=SolidEdgeConstants.VariableNameBy.seVariableNameByBoth,
                                  VarType:=SolidEdgeConstants.VariableVarType.SeVariableVarTypeBoth),
                                  SolidEdgeFramework.VariableList)

                ' Populate DocDimensionDict and DocVariableDict
                For Each VariableListItem In VariableListObject.OfType(Of Object)()
                    VariableTypeName = Microsoft.VisualBasic.Information.TypeName(VariableListItem)

                    If VariableTypeName.ToLower() = "dimension" Then
                        Dimension = CType(VariableListItem, SolidEdgeFrameworkSupport.Dimension)
                        DocDimensionDict(Dimension.DisplayName) = Dimension
                    ElseIf VariableTypeName.ToLower() = "variable" Then
                        Variable = CType(VariableListItem, SolidEdgeFramework.variable)
                        DocVariableDict(Variable.DisplayName) = Variable
                    End If
                Next

            Catch ex As Exception
                ExitStatus = 1
                ErrorMessageList.Add("Unable to access variables")
            End Try

        End If

        If ExitStatus = 0 Then

            ' Process variables.
            For Each VariableName In VariablesToEditDict.Keys
                Dim Formula As String = VariablesToEditDict(VariableName)("Formula").Trim
                Dim Units As SolidEdgeConstants.UnitTypeConstants = GetUnits(VariablesToEditDict(VariableName)("Units").Trim)
                Dim Expose As Boolean = VariablesToEditDict(VariableName)("Expose").Trim.ToLower = "true"
                Dim ExposeName As String = VariablesToEditDict(VariableName)("ExposeName").Trim

                tf = DocDimensionDict.Keys.Contains(VariableName)
                tf = tf Or DocVariableDict.Keys.Contains(VariableName)

                If Not tf Then  ' Add it.
                    If Configuration("CheckBoxAutoAddMissingVariable").ToLower = "true" Then
                        If Formula = "" Then  ' Can't add a variable without a formula
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Unable to add variable named '{0}'.  No value or formula supplied.", VariableName))
                            Continue For
                        End If

                        Try
                            ' Pretty sure this must be a variable, not a dimension.
                            Variable = Variables.Add(VariableName, Formula, Units)
                            If Expose Then
                                Variable.Expose = CInt(Expose)
                                If Not ExposeName = "" Then
                                    Variable.ExposeName = ExposeName
                                End If
                            End If
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add(String.Format("Unable to add variable named '{0}'.  Check name and formula.", VariableName))

                        End Try

                    End If

                Else  ' Edit and/or Expose.
                    Try
                        If DocDimensionDict.Keys.Contains(VariableName) Then
                            Dimension = DocDimensionDict(VariableName)
                            If Expose Then
                                Dimension.Expose = CInt(Expose)
                                If Not ExposeName = "" Then
                                    Dimension.ExposeName = ExposeName
                                End If
                            End If
                            If Not Formula = "" Then
                                Dimension.Formula = Formula
                            End If

                        ElseIf DocVariableDict.Keys.Contains(VariableName) Then
                            Variable = DocVariableDict(VariableName)
                            If Expose Then
                                Variable.Expose = CInt(Expose)
                                If Not ExposeName = "" Then
                                    Variable.ExposeName = ExposeName
                                End If
                            End If
                            If Not Formula = "" Then
                                Variable.Formula = Formula
                            End If

                        End If
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Unable to change variable '{0}'", VariableName))
                    End Try

                End If
            Next

        End If

        If ExitStatus = 0 Then
            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Shared Function GetUnits(Name As String) As SolidEdgeConstants.UnitTypeConstants
        Dim UnitType As SolidEdgeConstants.UnitTypeConstants

        UnitType = SolidEdgeConstants.UnitTypeConstants.igUnitDistance

        For Each UnitTypeConstant As SolidEdgeConstants.UnitTypeConstants In System.Enum.GetValues(GetType(SolidEdgeConstants.UnitTypeConstants))
            If Name = UnitTypeConstant.ToString.Replace("igUnit", "") Then
                UnitType = UnitTypeConstant
                Exit For
            End If
        Next

        Return UnitType
    End Function
    Shared Function ExposeVariables(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim SupplementalExitStatus As Integer = 0

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        Dim DisplayName As String
        Dim ExposeValue As Integer

        Dim Variables As SolidEdgeFramework.Variables = Nothing
        Dim VariableList As SolidEdgeFramework.VariableList = Nothing
        Dim Variable As SolidEdgeFramework.variable = Nothing
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension = Nothing
        Dim VariableListItemTypeName As String

        Dim VariablesToExpose As String = ""
        Dim VariablesToExposeDict As New Dictionary(Of String, String)

        Dim DocType As String

        SupplementalErrorMessage = ExposeVariablesMissing(SEDoc, Configuration, SEApp)
        SupplementalExitStatus = SupplementalErrorMessage.Keys(0)
        If Not SupplementalExitStatus = 0 Then
            ExitStatus = SupplementalExitStatus
            For Each s As String In SupplementalErrorMessage(SupplementalExitStatus)
                ErrorMessageList.Add(s)
            Next
        End If

        DocType = GetDocType(SEDoc)
        If DocType = "asm" Then VariablesToExpose = Configuration("TextBoxExposeVariablesAssembly")
        If DocType = "par" Then VariablesToExpose = Configuration("TextBoxExposeVariablesPart")
        If DocType = "psm" Then VariablesToExpose = Configuration("TextBoxExposeVariablesSheetmetal")
        If DocType = "dft" Then VariablesToExpose = Configuration("TextBoxExposeVariablesDraft")

        VariablesToExposeDict = StringToDict(VariablesToExpose, ","c, ":"c)

        Variables = DirectCast(SEDoc.Variables, SolidEdgeFramework.Variables)

        VariableList = DirectCast(Variables.Query(pFindCriterium:="*",
                                  NamedBy:=SolidEdgeConstants.VariableNameBy.seVariableNameByBoth,
                                  VarType:=SolidEdgeConstants.VariableVarType.SeVariableVarTypeBoth),
                                  SolidEdgeFramework.VariableList)

        For Each VariableListItem In VariableList.OfType(Of Object)()
            VariableListItemTypeName = Microsoft.VisualBasic.Information.TypeName(VariableListItem)

            If VariableListItemTypeName.ToLower() = "dimension" Then
                Dimension = CType(VariableListItem, SolidEdgeFrameworkSupport.Dimension)
                ExposeValue = Dimension.Expose
                DisplayName = Dimension.DisplayName
                If VariablesToExposeDict.Keys.Contains(DisplayName) Then
                    Try
                        Dimension.Expose = 1
                        Dimension.ExposeName = VariablesToExposeDict(DisplayName)
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Unable to expose '{0}'", DisplayName))
                    End Try
                End If

            ElseIf VariableListItemTypeName.ToLower() = "variable" Then
                Variable = CType(VariableListItem, SolidEdgeFramework.variable)
                ExposeValue = Variable.Expose
                DisplayName = Variable.DisplayName
                If VariablesToExposeDict.Keys.Contains(DisplayName) Then
                    Try
                        Variable.Expose = 1
                        Variable.ExposeName = VariablesToExposeDict(DisplayName)
                    Catch ex As Exception
                        ExitStatus = 1
                        ErrorMessageList.Add(String.Format("Unable to expose '{0}'", DisplayName))
                    End Try
                End If
            End If

        Next

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Shared Function ExposeVariablesMissing(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        'If Configuration("CheckBoxBackgroundProcessing") = "True" Then
        '    SEDoc.UpdatePathfinder(SolidEdgeAssembly.AssemblyPathfinderUpdateConstants.seSuspend)
        'End If

        Dim Variables As SolidEdgeFramework.Variables = Nothing
        Dim VariableList As SolidEdgeFramework.VariableList = Nothing
        Dim Variable As SolidEdgeFramework.variable = Nothing
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension = Nothing
        Dim VariableListItemTypeName As String

        Dim VariablesToExpose As String = ""
        Dim VariablesToExposeDict As New Dictionary(Of String, String)

        Dim DocType As String

        Dim VariablesPresentInDocument As New List(Of String)

        DocType = GetDocType(SEDoc)
        If DocType = "asm" Then VariablesToExpose = Configuration("TextBoxExposeVariablesAssembly")
        If DocType = "par" Then VariablesToExpose = Configuration("TextBoxExposeVariablesPart")
        If DocType = "psm" Then VariablesToExpose = Configuration("TextBoxExposeVariablesSheetmetal")
        If DocType = "dft" Then VariablesToExpose = Configuration("TextBoxExposeVariablesDraft")

        VariablesToExposeDict = StringToDict(VariablesToExpose, ","c, ":"c)

        Variables = DirectCast(SEDoc.Variables, SolidEdgeFramework.Variables)

        VariableList = DirectCast(Variables.Query(pFindCriterium:="*",
                                  NamedBy:=SolidEdgeConstants.VariableNameBy.seVariableNameByBoth,
                                  VarType:=SolidEdgeConstants.VariableVarType.SeVariableVarTypeBoth),
                                  SolidEdgeFramework.VariableList)

        For Each VariableListItem In VariableList.OfType(Of Object)()
            VariableListItemTypeName = Microsoft.VisualBasic.Information.TypeName(VariableListItem)

            If VariableListItemTypeName.ToLower() = "dimension" Then
                Dimension = CType(VariableListItem, SolidEdgeFrameworkSupport.Dimension)
                VariablesPresentInDocument.Add(Dimension.DisplayName)

            ElseIf VariableListItemTypeName.ToLower() = "variable" Then
                Variable = CType(VariableListItem, SolidEdgeFramework.variable)
                VariablesPresentInDocument.Add(Variable.DisplayName)
            End If

        Next

        For Each Key As String In VariablesToExposeDict.Keys
            If Not VariablesPresentInDocument.Contains(Key) Then
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Variable '{0}' not found", Key))
            End If
        Next

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Shared Function StringToDict(s As String, delimiter1 As Char, delimiter2 As Char) As Dictionary(Of String, String)
        ' Takes a double-delimited string and returns a dictionary
        ' delimiter1 separates entries in the dictionary
        ' delimiter2 separates the Key from the Value in each entry.

        ' Example string: "weight: Weight of Object, length:, width"
        ' Returns a dictionary like:

        ' {"weight": "Weight of Object",
        '  "length": "length",
        '  "width": "width"}

        ' Notes
        ' Whitespace before and after each Key and Value is removed.
        ' To convert a single string, say ",", to a char, do ","c
        ' If delimiter2 is not present in an entry, or there is nothing after delimiter2, the Key and Value are the same.

        Dim D As New Dictionary(Of String, String)
        Dim A() As String
        Dim K As String
        Dim V As String

        A = s.Split(delimiter1)

        For i As Integer = 0 To A.Length - 1
            If A(i).Contains(delimiter2) Then
                K = A(i).Split(delimiter2)(0).Trim
                V = A(i).Split(delimiter2)(1).Trim

                If V = "" Then
                    V = K
                End If
            Else
                K = A(i).Trim
                V = K
            End If

            D.Add(K, V)

        Next

        Return D

    End Function

End Class
