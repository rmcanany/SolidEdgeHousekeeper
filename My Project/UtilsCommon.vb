Option Strict On

Imports System.IO
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports ExcelDataReader
Imports PanoramicData.NCalcExtensions

Public Class UtilsCommon

    Public tmpList As Collection

    Public Function FilenameIsOK(ByVal fileName As String) As Boolean

        Try
            Dim fi As New IO.FileInfo(fileName)
        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function


    Public Sub FindLinked(DMDoc As RevisionManager.Document)

        Dim Filename As String

        Dim LinkedDocs As RevisionManager.LinkedDocuments
        Dim LinkedDoc As RevisionManager.Document

        ' Loop through the items in the assembly
        LinkedDocs = CType(DMDoc.LinkedDocuments, RevisionManager.LinkedDocuments)

        If Not IsNothing(LinkedDocs) Then
            For Each LinkedDoc In LinkedDocs
                Filename = LinkedDoc.FullName
                If Not tmpList.Contains(Filename) Then

                    tmpList.Add(Filename, Filename)

                    If IO.Path.GetExtension(Filename).ToLower = ".asm" Then
                        ' there is a problem in traversing back up through the tree that throws an exception, so using a try-catch.
                        Try
                            FindLinked(LinkedDoc)
                        Catch ex As Exception
                            'MsgBox("Problem with traversing back up the tree " & objLinkedDocs(i).fullname)
                        End Try
                    End If
                End If
            Next
        End If

    End Sub

    Public Function GetDocRange(SEDoc As SolidEdgeFramework.SolidEdgeDocument) As List(Of Double)
        ' Returns range in a list.  The order is X, Y, Z
        Dim Range As New List(Of Double)
        Dim ModelX As Double
        Dim ModelY As Double
        Dim ModelZ As Double
        Dim XMin As Double
        Dim YMin As Double
        Dim ZMin As Double
        Dim XMax As Double
        Dim YMax As Double
        Dim ZMax As Double

        Dim Models As SolidEdgePart.Models = Nothing

        Dim DocType As String = GetDocType(SEDoc)

        Select Case DocType
            Case "asm"
                Dim tmpAsm As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)
                tmpAsm.Range(XMin, YMin, ZMin, XMax, YMax, ZMax)
                ModelX = XMax - XMin
                ModelY = YMax - YMin
                ModelZ = ZMax - ZMin

            Case "par"
                Dim tmpPar As SolidEdgePart.PartDocument = CType(SEDoc, SolidEdgePart.PartDocument)
                Models = tmpPar.Models

            Case "psm"
                Dim tmpPsm As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                Models = tmpPsm.Models

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", "Task_Common", DocType))
        End Select

        If (DocType = "par") Or (DocType = "psm") Then

            Dim Model As SolidEdgePart.Model
            Dim Body As SolidEdgeGeometry.Body

            Dim UF As New UtilsFeatures
            Dim PointsList As New List(Of Double)
            Dim tmpPointsList As New List(Of Double)
            Dim Point As Double

            If (Models.Count = 0) Then
                ModelX = 0
                ModelY = 0
                ModelZ = 0
            Else
                For Each Model In Models
                    ' Some Models do not have a Body
                    Try
                        Body = CType(Model.Body, SolidEdgeGeometry.Body)
                        tmpPointsList = UF.GetBodyRange(Body)
                        If PointsList.Count = 0 Then
                            For Each Point In tmpPointsList
                                PointsList.Add(Point)
                            Next
                        Else
                            For i As Integer = 0 To 2
                                If tmpPointsList(i) < PointsList(i) Then
                                    PointsList(i) = tmpPointsList(i)
                                End If
                            Next
                            For i As Integer = 3 To 5
                                If tmpPointsList(i) > PointsList(i) Then
                                    PointsList(i) = tmpPointsList(i)
                                End If
                            Next
                        End If

                    Catch ex As Exception
                        ModelX = 0
                        ModelY = 0
                        ModelZ = 0
                    End Try
                Next

                ModelX = PointsList(3) - PointsList(0) ' XMax - XMin
                ModelY = PointsList(4) - PointsList(1) ' YMax - YMin
                ModelZ = PointsList(5) - PointsList(2) ' ZMax - ZMin

            End If

        End If

        Range.Add(ModelX)
        Range.Add(ModelY)
        Range.Add(ModelZ)

        Return Range
    End Function

    Public Function GetDocType(SEDoc As SolidEdgeFramework.SolidEdgeDocument) As String
        ' See SolidEdgeFramework.DocumentTypeConstants

        ' If the type is not recognized, the empty string is returned.
        Dim DocType As String = ""

        If Not IsNothing(SEDoc) Then
            Select Case SEDoc.Type

                Case Is = SolidEdgeFramework.DocumentTypeConstants.igAssemblyDocument
                    DocType = "asm"

                Case Is = SolidEdgeFramework.DocumentTypeConstants.igWeldmentAssemblyDocument
                    DocType = "asm"

                Case Is = SolidEdgeFramework.DocumentTypeConstants.igSyncAssemblyDocument
                    DocType = "asm"

                Case Is = SolidEdgeFramework.DocumentTypeConstants.igPartDocument
                    DocType = "par"

                Case Is = SolidEdgeFramework.DocumentTypeConstants.igSyncPartDocument
                    DocType = "par"

                Case Is = SolidEdgeFramework.DocumentTypeConstants.igSheetMetalDocument
                    DocType = "psm"

                Case Is = SolidEdgeFramework.DocumentTypeConstants.igSyncSheetMetalDocument
                    DocType = "psm"

                Case Is = SolidEdgeFramework.DocumentTypeConstants.igDraftDocument
                    DocType = "dft"

                Case Else
                    MsgBox(String.Format("{0} DocType '{1}' not recognized", "Task_Common", SEDoc.Type.ToString))
            End Select
        End If

        Return DocType
    End Function


    Public Function FormatMsgBoxText(
        ErrorList As List(Of String),
        Title As String,
        Optional MaxErrorsToShow As Integer = 20
        ) As String

        Dim ErrorMessage As String = ""

        If Not ErrorList.Count = 0 Then
            Dim s As String = ""
            Dim Indent As String = "    "
            If MaxErrorsToShow <= 0 Then MaxErrorsToShow = 1

            For i As Integer = 0 To ErrorList.Count - 1
                s = String.Format("{0}{1}{2}{3}", s, Indent, ErrorList(i), vbCrLf)
                If i = MaxErrorsToShow - 1 Then Exit For
            Next

            If ErrorList.Count > MaxErrorsToShow Then
                ErrorMessage = String.Format("{0}{1}", Title, vbCrLf)
                ErrorMessage = String.Format("{0}{1}(Showing {2} of {3}){4}", ErrorMessage, Indent, MaxErrorsToShow, ErrorList.Count, vbCrLf)
            Else
                ErrorMessage = String.Format("{0}{1}", Title, vbCrLf)
            End If

            ErrorMessage = String.Format("{0}{1}", ErrorMessage, s)
        End If

        Return ErrorMessage
    End Function


    '###### DIMENSIONS AND VARIABLES ######

    Public Function GetDocDimensions(SEDoc As SolidEdgeFramework.SolidEdgeDocument
    ) As Dictionary(Of String, SolidEdgeFrameworkSupport.Dimension)
        Dim DocDimensionDict As New Dictionary(Of String, SolidEdgeFrameworkSupport.Dimension)

        Dim Variables As SolidEdgeFramework.Variables = Nothing
        Dim VariableListObject As SolidEdgeFramework.VariableList = Nothing
        Dim Variable As SolidEdgeFramework.variable = Nothing
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension = Nothing
        Dim VariableTypeName As String

        Try
            Variables = DirectCast(SEDoc.Variables, SolidEdgeFramework.Variables)

            VariableListObject = DirectCast(Variables.Query(pFindCriterium:="*",
                                  NamedBy:=SolidEdgeConstants.VariableNameBy.seVariableNameByBoth,
                                  VarType:=SolidEdgeConstants.VariableVarType.SeVariableVarTypeBoth),
                                  SolidEdgeFramework.VariableList)

            ' Populate dictionary
            For Each VariableListItem In VariableListObject.OfType(Of Object)()
                VariableTypeName = Microsoft.VisualBasic.Information.TypeName(VariableListItem)

                If VariableTypeName.ToLower() = "dimension" Then
                    Dimension = CType(VariableListItem, SolidEdgeFrameworkSupport.Dimension)
                    DocDimensionDict(Dimension.DisplayName) = Dimension
                End If
            Next

        Catch ex As Exception
        End Try

        Return DocDimensionDict
    End Function

    Public Function GetDocVariables(SEDoc As SolidEdgeFramework.SolidEdgeDocument
        ) As Dictionary(Of String, SolidEdgeFramework.variable)
        Dim DocVariableDict As New Dictionary(Of String, SolidEdgeFramework.variable)

        Dim Variables As SolidEdgeFramework.Variables = Nothing
        Dim VariableListObject As SolidEdgeFramework.VariableList = Nothing
        Dim Variable As SolidEdgeFramework.variable = Nothing
        Dim Dimension As SolidEdgeFrameworkSupport.Dimension = Nothing
        Dim VariableTypeName As String

        Try
            Variables = DirectCast(SEDoc.Variables, SolidEdgeFramework.Variables)

            VariableListObject = DirectCast(Variables.Query(pFindCriterium:="*",
                                  NamedBy:=SolidEdgeConstants.VariableNameBy.seVariableNameByBoth,
                                  VarType:=SolidEdgeConstants.VariableVarType.SeVariableVarTypeBoth),
                                  SolidEdgeFramework.VariableList)

            ' Populate dictionary
            For Each VariableListItem In VariableListObject.OfType(Of Object)()
                VariableTypeName = Microsoft.VisualBasic.Information.TypeName(VariableListItem)

                If VariableTypeName.ToLower() = "variable" Then
                    Variable = CType(VariableListItem, SolidEdgeFramework.variable)
                    DocVariableDict(Variable.DisplayName) = Variable
                End If
            Next

        Catch ex As Exception
        End Try

        Return DocVariableDict
    End Function

    Public Function IsVariablePresent(SEDoc As SolidEdgeFramework.SolidEdgeDocument, VariableName As String) As Boolean
        Dim VariableFound As Boolean = False
        Dim DocVariableDict As New Dictionary(Of String, SolidEdgeFramework.variable)

        DocVariableDict = GetDocVariables(SEDoc)
        VariableFound = False

        For Each Key As String In DocVariableDict.Keys
            If Key.ToLower = VariableName.ToLower Then
                VariableFound = True
                Exit For
            End If
        Next

        Return VariableFound
    End Function

    Public Function IsDimensionPresent(SEDoc As SolidEdgeFramework.SolidEdgeDocument, DimensionName As String) As Boolean
        Dim DimensionFound As Boolean = False
        Dim DocDimensionDict As New Dictionary(Of String, SolidEdgeFrameworkSupport.Dimension)

        DocDimensionDict = GetDocDimensions(SEDoc)
        DimensionFound = False

        For Each Key As String In DocDimensionDict.Keys
            If Key.ToLower = DimensionName.ToLower Then
                DimensionFound = True
                Exit For
            End If
        Next

        Return DimensionFound
    End Function

    Public Function GetUnitType(Name As String) As SolidEdgeConstants.UnitTypeConstants
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


    Public Function GetSheets(
        Doc As SolidEdgeDraft.DraftDocument,
        SectionType As String
        ) As List(Of SolidEdgeDraft.Sheet)

        Dim SheetList As New List(Of SolidEdgeDraft.Sheet)
        Dim Sheet As SolidEdgeDraft.Sheet
        Dim Section As SolidEdgeDraft.Section = Nothing
        Dim SectionSheets As SolidEdgeDraft.SectionSheets
        Dim SheetGroups As SolidEdgeDraft.SheetGroups
        Dim SheetGroup As SolidEdgeDraft.SheetGroup

        Dim count As Integer

        If SectionType = "Working" Then
            Section = Doc.Sections.WorkingSection
        ElseIf SectionType = "Background" Then
            Section = Doc.Sections.BackgroundSection
        ElseIf SectionType = "2DModel" Then
            Section = Doc.Sections.WorkingSection  ' Ignored below
        ElseIf SectionType = "UserGenerated" Then
            Section = Doc.Sections.WorkingSection
            SheetGroups = CType(Doc.SheetGroups, SolidEdgeDraft.SheetGroups)
        ElseIf SectionType = "AutoGenerated" Then
            Section = Doc.Sections.WorkingSection
            SheetGroups = CType(Doc.SheetGroups, SolidEdgeDraft.SheetGroups)
        Else
            MsgBox(String.Format("SectionType '{0}' not recognized.  Quitting...", SectionType))
        End If

        SectionSheets = Section.Sheets

        If (SectionType = "Working") Or (SectionType = "Background") Then
            For Each Sheet In SectionSheets.OfType(Of SolidEdgeDraft.Sheet)()
                SheetList.Add(Sheet)
            Next

        ElseIf (SectionType = "2DModel") Then
            SheetList.Add(Doc.Sections.Get2DModelSheet)

        Else  ' So, 'UserGenerated' or 'AutoGenerated'
            SheetGroups = CType(Doc.SheetGroups, SolidEdgeDraft.SheetGroups)

            ' 'count' is an index on SheetGroups.  The first SheetGroup is user generated.
            ' The others appear to be automatically generated for PartsLists
            count = 0

            For Each SheetGroup In SheetGroups
                For Each Sheet In SheetGroup.Sheets.OfType(Of SolidEdgeDraft.Sheet)()
                    If (SectionType = "UserGenerated") And (count = 0) Then
                        SheetList.Add(Sheet)
                    End If
                    If (SectionType = "AutoGenerated") And (count > 0) Then
                        SheetList.Add(Sheet)
                    End If
                Next
                count += 1
            Next
        End If

        Return SheetList

    End Function

    Public Function GlobToRegex(GlobString As String) As String

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

    Public Function ReadExcel(FileName As String) As String()

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
                        tmpList(i) = CStr(reader.GetValue(0))
                    End While
                Loop While reader.NextResult()

                '' 2. Use the AsDataSet extension method
                'Dim result = reader.AsDataSet()

                '' The result of each spreadsheet is in result.Tables
            End Using
        End Using

        Return tmpList

    End Function

    Public Sub SaveAsPNG(
        View As SolidEdgeFramework.View,
        NewFilename As String,
        ErrorLogger As Logger
        )

        Dim TempFilename As String = NewFilename.Replace(".png", "-Housekeeper.tif")

        Try

            View.SaveAsImage(TempFilename)

            Try

                Using fs As New IO.FileStream(TempFilename, IO.FileMode.Open, IO.FileAccess.Read)
                    System.Drawing.Image.FromStream(fs).Save(NewFilename, Imaging.ImageFormat.Png)
                End Using

                IO.File.Delete(TempFilename)

            Catch ex As Exception
                ErrorLogger.AddMessage(String.Format("Unable to save '{0}'.  ", NewFilename))
            End Try

        Catch ex As Exception
            ErrorLogger.AddMessage(String.Format("Unable to save '{0}'.  ", TempFilename))
        End Try

    End Sub

    Public Function GetFOAFilename(SEDocFullName As String) As String
        Dim Filename As String

        If SEDocFullName.Contains("!") Then
            Filename = SEDocFullName.Split("!"c)(0)
        Else
            Filename = SEDocFullName
        End If

        Return Filename
    End Function

    Public Function GetFOAMembername(SEDocFullName As String) As String
        Dim MemberName As String

        If SEDocFullName.Contains("!") Then
            MemberName = SEDocFullName.Split("!"c)(1)
        Else
            MemberName = ""
        End If

        Return MemberName
    End Function

    Public Function CompareListOfColumns(A As List(Of PropertyColumn), B As List(Of PropertyColumn)) As Boolean
        Dim IsEqual As Boolean = True

        If A Is Nothing Or B Is Nothing Then
            IsEqual = False
        End If

        If IsEqual Then
            If Not A.Count = B.Count Then
                IsEqual = False
            End If
        End If

        If IsEqual Then
            Dim AJSON As String
            Dim BJSON As String
            For i As Integer = 0 To A.Count - 1
                AJSON = A(i).ToJSON
                BJSON = B(i).ToJSON
                IsEqual = AJSON = BJSON
                If Not IsEqual Then Exit For
            Next
        End If

        Return IsEqual
    End Function

    Public Function CompareListOfJSON(A As List(Of String), B As List(Of String)) As Boolean
        Dim IsEqual As Boolean = True

        If A Is Nothing Or B Is Nothing Then
            IsEqual = False
        End If

        If IsEqual Then
            If Not A.Count = B.Count Then
                IsEqual = False
            End If
        End If

        If IsEqual Then
            For i As Integer = 0 To A.Count - 1
                IsEqual = A(i) = B(i)
                If Not IsEqual Then Exit For
            Next
        End If

        Return IsEqual
    End Function



    '###### PROPERTY FUNCTIONS ######

    Public Function GetPropValue(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        PropertySetName As String,
        PropertyName As String,
        ModelLinkIdx As Integer,
        AddProp As Boolean
        ) As Object

        Dim PropValue As Object = Nothing

        PropValue = ProcessSpecialProperty(SEDoc, PropertySetName, PropertyName, ModelLinkIdx)

        If PropValue Is Nothing Then
            Dim Prop As SolidEdgeFramework.Property

            Prop = GetProp(SEDoc, PropertySetName, PropertyName, ModelLinkIdx, AddProp)

            If Prop IsNot Nothing Then
                PropValue = Prop.Value
            End If
        End If

        Return PropValue

    End Function

    Public Function SetPropValue(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        PropertySetName As String,
        PropertyName As String,
        ModelLinkIdx As Integer,
        AddProp As Boolean,
        PropValue As Object
        ) As Boolean

        Dim Success As Boolean = True

        Dim PropertySets As SolidEdgeFramework.PropertySets
        Dim Prop As SolidEdgeFramework.Property

        PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)

        Dim UC As New UtilsCommon

        Prop = GetProp(SEDoc, PropertySetName, PropertyName, ModelLinkIdx, AddProp)

        If Prop IsNot Nothing AndAlso Not Prop.Value.ToString = PropValue.ToString Then
            Try
                Prop.Value = PropValue
            Catch ex As Exception
                Success = False
            End Try
        Else
            Success = False
        End If

        If Success Then PropertySets.Save()

        Return Success
    End Function

    Public Function GetProp(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        PropertySetName As String,
        PropertyName As String,
        ModelLinkIdx As Integer,
        AddProp As Boolean
        ) As SolidEdgeFramework.Property

        Dim PropertySets As SolidEdgeFramework.PropertySets = Nothing
        Dim PropertySet As SolidEdgeFramework.Properties = Nothing
        Dim Prop As SolidEdgeFramework.Property = Nothing
        Dim FoundProp As SolidEdgeFramework.Property = Nothing
        Dim Proceed As Boolean = True
        Dim tf As Boolean
        Dim tf1 As Boolean
        Dim tf2 As Boolean
        Dim PropertyFound As Boolean

        If ModelLinkIdx = 0 Then  ' Implies the document is a model file
            Try
                PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)
            Catch ex As Exception
                Proceed = False
            End Try

        Else  ' Must be a draft file
            Dim ModelLink As SolidEdgeDraft.ModelLink
            Dim Typename As String = ""
            Dim ModelDocName As String = ""

            Try
                Dim tmpSEDoc As SolidEdgeDraft.DraftDocument
                tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

                ModelLink = tmpSEDoc.ModelLinks.Item(ModelLinkIdx)
                Typename = GetDocType(CType(ModelLink.ModelDocument, SolidEdgeFramework.SolidEdgeDocument))

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
            For Each PropertySet In PropertySets
                For Each Prop In PropertySet

                    'Check if both names are 'custom' or neither are
                    tf1 = (PropertySetName.ToLower = "custom") And (PropertySet.Name.ToLower = "custom")
                    tf2 = (PropertySetName.ToLower <> "custom") And (PropertySet.Name.ToLower <> "custom")

                    tf = tf1 Or tf2
                    If Not tf Then
                        Continue For
                    End If

                    ' Some properties do not have names.
                    Try
                        If Prop.Name.ToLower = PropertyName.ToLower Then
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


        'If the property was not found, add it.
        tf = PropertySetName.ToLower = "custom"
        tf = tf And AddProp
        tf = tf And (Not PropertyFound)

        If tf Then
            ' Can't add a duplicate property
            Try
                PropertySet = PropertySets.Item("Custom")

                ' PropertySet.Add arguments are (Name As Object, Value As Object)
                ' Documentation says the value's Type determines the new property's Type.
                ' So we are currently creating a String property.
                ' If PropertiesData holds the Type info for the new property,
                ' we could make the appropriate decision here.
                FoundProp = PropertySet.Add(PropertyName, "")

                PropertySet.Save()
                PropertySets.Save()
            Catch ex As Exception
                ' Might want to report an error.
            End Try

        End If

        Return FoundProp

    End Function

    Private Function ProcessSpecialProperty(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        PropSetName As String,
        PropName As String,
        ModelLinkIdx As Integer
        ) As String

        ' Returns Nothing if not a special property
        ' Special properties:
        '     "System.File Name (full path)"     ' C:\project\part.par -> C:\project\part.par
        '     "System.File Name"                 ' C:\project\part.par -> part.par
        '     "System.File Name (no extension)"  ' C:\project\part.par -> part

        ' We could eventually add other Housekeeper special properties if the need arises.
        '     Current date/time
        '     ???

        PropName = PropName.ToLower

        Dim Proceed As Boolean = True
        Dim Fullname As String = Nothing
        Dim SpecialProperty As String = Nothing

        If ModelLinkIdx = 0 Then  ' Model file
            Fullname = GetFOAFilename(SEDoc.FullName)

        Else  ' Must be a draft file
            Dim ModelLink As SolidEdgeDraft.ModelLink

            Try
                Dim tmpSEDoc As SolidEdgeDraft.DraftDocument
                tmpSEDoc = CType(SEDoc, SolidEdgeDraft.DraftDocument)

                ModelLink = tmpSEDoc.ModelLinks.Item(ModelLinkIdx)
                Fullname = GetFOAFilename(ModelLink.FileName)

            Catch ex As Exception
                Proceed = False
            End Try
        End If

        If Proceed Then
            If PropSetName.ToLower = "system" Then
                If PropName = "File Name".ToLower Then
                    SpecialProperty = System.IO.Path.GetFileName(Fullname)
                ElseIf PropName = "File Name (full path)".ToLower Then
                    SpecialProperty = Fullname
                ElseIf PropName = "File Name (no extension)".ToLower Then
                    SpecialProperty = System.IO.Path.GetFileNameWithoutExtension(Fullname)
                End If
            End If
        End If

        Return SpecialProperty
    End Function

    Public Function SubstitutePropertyFormulas(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal FullName As String,
        ByVal Instring As String,
        PropertiesData As HCPropertiesData,
        ErrorLogger As Logger,
        Optional IsExpression As Boolean = False
        ) As String

        ' Replaces property formulas in a string

        ' Examples
        ' "%{Custom.Engineer|R1}"                                      --> "Fred" (From index reference 1)
        ' "%{System.Titulo}"                                           --> "Ciao Pizza!"
        ' "%{Server.Query|R3}"                                         --> "Some response from server" (From server query 3)
        ' "Material: %{System.Material}, Engineer: %{Custom.Engineer}" --> "Material: STEEL, Engineer: FRED"

        Dim Outstring As String = ""

        Dim Proceed As Boolean = True

        Dim PropertySet As String = ""
        Dim PropertyName As String = ""
        Dim ModelIdx As Integer

        Dim FoundProp As SolidEdgeFramework.Property

        Dim DocValues As New List(Of String)

        Dim i As Integer

        Dim Formulas As New List(Of String)
        Dim Formula As String

        Dim Matches As MatchCollection
        Dim MatchString As Match
        Dim Pattern As String

        Dim ExpressionLanguage As String = ""

        Dim UFC As New UtilsFilenameCharmap
        Dim UP As New UtilsPreferences

        If IsExpression And Instring.StartsWith("EXPRESSION_") Then
            Dim TextToRemove As String = Instring.Split(CChar(vbCrLf))(0)  ' EXPRESSION_VB, EXPRESSION_SQL

            Select Case TextToRemove
                Case "EXPRESSION_VB"
                    ExpressionLanguage = "VB"
                Case "EXPRESSION_NCalc"
                    ExpressionLanguage = "NCalc"
                Case Else
                    MsgBox($"SubstitutePropertyFormula: Expression header not recognized '{TextToRemove}'")
                    Return Nothing
            End Select

            Instring = Instring.Replace($"{TextToRemove}{vbCrLf}", "")

        End If


        FullName = GetFOAFilename(FullName)

        Pattern = "%{[^}]*}"  ' Any number of substrings that start with "%{" and end with the first encountered "}".
        Matches = Regex.Matches(Instring, Pattern)
        If Matches.Count = 0 Then
            Outstring = Instring
            Proceed = False
        Else
            For Each MatchString In Matches
                Formulas.Add(MatchString.Value)
            Next
        End If

        If Proceed Then
            For Each Formula In Formulas

                PropertySet = PropSetFromFormula(Formula)
                PropertyName = PropNameFromFormula(Formula)
                ModelIdx = ModelIdxFromFormula(Formula)

                If Not ((PropertySet = "System") Or (PropertySet = "Custom") Or (PropertySet = "Server")) Then
                    ErrorLogger.AddMessage($"PropertySet not recognized '{PropertySet}'")
                    Proceed = False
                End If

                'Check for special properties %{System.File Name}, %{System.File Name (full path)}, %{System.File Name (no extension)}

                Dim tmpValue As String = Nothing

                If PropertyName.ToLower = "File Name".ToLower Then
                    tmpValue = System.IO.Path.GetFileName(FullName)                  ' C:\project\part.par -> part.par
                ElseIf PropertyName.ToLower = "File Name (full path)".ToLower Then
                    tmpValue = FullName                                              ' C:\project\part.par -> C:\project\part.par
                ElseIf PropertyName.ToLower = "File Name (no extension)".ToLower Then
                    tmpValue = System.IO.Path.GetFileNameWithoutExtension(FullName)  ' C:\project\part.par -> part

                ElseIf PropertyName = "Query" And PropertySet = "Server" Then
                    'tmpValue = Form_Main.ExecuteQuery(FullName, Form_Main.ServerQuery, ModelIdx).Replace(vbCrLf, " ")

                    Dim tmpServerQuery = SubstitutePropertyFormulas(SEDoc, FullName, Form_Main.ServerQuery, PropertiesData, ErrorLogger)
                    If tmpServerQuery IsNot Nothing Then
                        tmpValue = Form_Main.ExecuteQuery(FullName, tmpServerQuery, ModelIdx).Replace(vbCrLf, " ")
                    Else
                        ErrorLogger.AddMessage($"Unable to process query '{Form_Main.ServerQuery}'")
                    End If

                Else
                    FoundProp = GetProp(SEDoc, PropertySet, PropertyName, ModelIdx, False)
                    If FoundProp IsNot Nothing Then
                        tmpValue = FoundProp.Value.ToString
                    Else
                        If Not IsExpression Then
                            ErrorLogger.AddMessage($"Unable to process property '{PropertySet}.{PropertyName}' in formula '{Formula}'")
                        Else
                            tmpValue = "Nothing"
                        End If
                    End If
                End If

                If tmpValue IsNot Nothing Then
                    'If ValidFilenameRequired Then
                    '    tmpValue = UFC.SubstituteIllegalCharacters(tmpValue)
                    'End If

                    DocValues.Add(tmpValue)
                Else
                    Proceed = False
                    Outstring = Nothing
                    Exit For
                End If

            Next
        End If

        If Proceed Then
            Outstring = Instring

            For i = 0 To DocValues.Count - 1
                Outstring = Outstring.Replace(Formulas(i), DocValues(i))
            Next

        End If

        If Proceed Then
            If IsExpression Then
                If ExpressionLanguage = "" Or ExpressionLanguage = "NCalc" Then
                    Dim nCalcExpression As New ExtendedExpression(Outstring)
                    Try
                        Dim A = nCalcExpression.Evaluate()
                        Outstring = A.ToString
                    Catch ex As Exception
                        ErrorLogger.AddMessage($"Unable to process expression '{Outstring}'")
                        ErrorLogger.AddMessage("Exception was:")
                        ErrorLogger.AddMessage(ex.Message)
                        Outstring = Nothing
                    End Try

                Else  ' Must be VB
                    Dim UPS As New UtilsPowerShell
                    Dim PowerShellFileContents As List(Of String) = UPS.BuildExpressionFile(Outstring.Split(CChar(vbCrLf)).ToList)

                    Dim PowerShellFilename As String = $"{UP.GetTempDirectory}\HousekeeperExpression.ps1"
                    IO.File.WriteAllLines(PowerShellFilename, PowerShellFileContents)

                    Try
                        Outstring = UPS.RunExpressionScript(PowerShellFilename)
                    Catch ex As Exception
                        ErrorLogger.AddMessage($"Unable to process expression '{Outstring}'")
                        ErrorLogger.AddMessage("Exception was:")
                        ErrorLogger.AddMessage(ex.Message)
                        Outstring = Nothing
                    End Try

                End If

            End If
        End If

        Return Outstring

    End Function

    Public Function CheckValidPropertyFormulas(Instring As String) As Boolean

        Dim Valid As Boolean = True

        Dim Matches As MatchCollection
        Dim MatchString As Match
        Dim Pattern As String
        Dim tf As Boolean

        Pattern = "%{[^}]*}"
        Matches = Regex.Matches(Instring, Pattern)
        If Matches.Count > 0 Then
            For Each MatchString In Matches
                tf = MatchString.Value.Contains("%{System.")
                tf = tf Or MatchString.Value.Contains("%{Custom.")
                tf = tf Or MatchString.Value.Contains("%{Server.")
                If Not tf Then
                    Valid = False
                    Exit For
                End If
            Next
        End If

        Return Valid
    End Function

    Public Sub CopyProperties(Source As Object, Destination As Object, ErrorLogger As Logger)

        Dim sourceType As Type = HCComObject.GetCOMObjectType(Source)
        Dim destType As Type = HCComObject.GetCOMObjectType(Destination)

        Dim sourceProps() As PropertyInfo = sourceType.GetProperties()
        Dim destProps() As PropertyInfo = destType.GetProperties()

        For Each sourceProp As PropertyInfo In sourceProps
            For Each destProp As PropertyInfo In destProps
                If destProp.CanWrite Then
                    If destProp.Name <> "Parent" Then
                        If destProp.Name = sourceProp.Name Then
                            If destProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType) Then
                                Try
                                    Dim tmpValue = sourceProp.GetValue(Source, Nothing)
                                    If tmpValue IsNot Nothing Then
                                        Try
                                            destProp.SetValue(Destination, tmpValue, Nothing)
                                        Catch ex2 As Exception
                                            'ErrorLogger.AddMessage($"Unable to set value for '{sourceProp.Name}'.  Exception was '{ex2.Message}'")
                                        End Try
                                    Else
                                        'ErrorLogger.AddMessage($"Null value returned for '{sourceProp.Name}'")
                                    End If
                                Catch ex As Exception
                                    'ErrorLogger.AddMessage($"Exception occurred on '{sourceProp.Name}'.  Exception was '{ex.Message}'")
                                End Try
                            End If
                        End If
                    End If
                End If
            Next
        Next

    End Sub

    Public Function PropNameFromFormula(PropFormula As String) As String
        ' '%{System.Title}'            -> 'Title'
        ' '%{Custom.Donut|R12}'        -> 'Donut'
        ' '%{Custom.foo|bar.baz}'      -> 'foo|bar.baz'
        ' '%{Custom.foo|bar.baz|R12}'  -> 'foo|bar.baz'

        Dim PropName As String
        Dim Matches As MatchCollection
        Dim Match As Match
        Dim Pattern As String

        PropName = PropFormula
        PropName = PropName.Replace("%{System.", "") ' '%{System.Title}' -> 'Title}'
        PropName = PropName.Replace("%{Custom.", "") ' '%{Custom.Donut|R12}' -> 'Donut|R12}'
        PropName = PropName.Replace("%{Server.", "") ' '%{Server.Query|R1}' -> 'Query|R1}'
        PropName = PropName.Replace("}", "") '         'Donut|R12}' -> 'Donut|R12',  'Title}' -> 'Title'

        If PropName.StartsWith("Query") Then PropName = "Query"

        Pattern = "^(.*)(\|R[0-9]+)$"
        Matches = Regex.Matches(PropName, Pattern)

        If Matches.Count > 0 Then
            Match = Matches(0)
            PropName = Regex.Replace(Match.Value, Pattern, "$1")
        End If

        Return PropName
    End Function

    Public Function PropSetFromFormula(PropFormula As String) As String
        ' '%{System.Title}' -> 'System'
        Dim PropSet As String

        PropSet = PropFormula
        PropSet = PropSet.Replace("%{", "") '    '%{System.Title}' -> 'System.Title}'
        PropSet = PropSet.Split(CChar("."))(0) ' 'System.Title}' -> 'System'

        Return PropSet
    End Function

    Public Function ModelIdxFromFormula(PropFormula As String) As Integer
        ' '%{System.Title}' -> 0
        ' '%{System.Title|R12}' -> 12

        Dim Matches As MatchCollection
        Dim Match As Match
        Dim Pattern As String

        Dim ModelIdxString As String
        Dim ModelIdx As Integer

        Pattern = "^.*R([0-9]+)}$"
        Matches = Regex.Matches(PropFormula, Pattern)

        If Matches.Count = 0 Then
            ModelIdx = 0
        Else
            Match = Matches(Matches.Count - 1)
            ModelIdxString = Regex.Replace(Match.Value, Pattern, "$1")
            ModelIdx = CInt(ModelIdxString)
        End If

        Return ModelIdx
    End Function


End Class
