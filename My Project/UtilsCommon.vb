Option Strict On

Imports System.IO
Imports System.Reflection
Imports System.Security.AccessControl
Imports System.Text.RegularExpressions
Imports System.Windows
Imports ExcelDataReader
Imports OpenMcdf
Imports OpenMcdf.Extensions
Imports OpenMcdf.Extensions.OLEProperties
Imports PanoramicData.NCalcExtensions

Public Class UtilsCommon

    Public tmpList As Collection

    Public Function FilenameIsOK(ByVal fileName As String) As Boolean

        Try
            Dim fi As New IO.FileInfo(fileName)
            Dim i = 1
        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function


    Public Sub FindLinked(DMDoc As DesignManager.Document)

        Dim Filename As String

        Dim LinkedDocs As DesignManager.LinkedDocuments
        Dim LinkedDoc As DesignManager.Document

        ' Loop through the items in the assembly
        LinkedDocs = CType(DMDoc.LinkedDocuments, DesignManager.LinkedDocuments)

        If Not IsNothing(LinkedDocs) Then
            For Each LinkedDoc In LinkedDocs
                Filename = LinkedDoc.FullName
                If Not tmpList.Contains(Filename) Then

                    'tmpList.Add(LinkedDocs(i).FullName, LinkedDocs(i).FullName.ToString)
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
            'ExitStatus = 1
            'ErrorMessageList.Add("Unable to access variables")
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
            'ExitStatus = 1
            'ErrorMessageList.Add("Unable to access variables")
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

    Public Function GetStatus(
        DMApp As DesignManager.Application,
        Filename As String
        ) As SolidEdgeConstants.DocumentStatus

        Dim Status As SolidEdgeConstants.DocumentStatus = Nothing

        Try

            Dim PropertySets As DesignManager.PropertySets
            Dim PropertySet As DesignManager.Properties

            PropertySets = CType(DMApp.PropertySets, DesignManager.PropertySets)
            PropertySets.Open(Filename, True)

            PropertySet = CType(PropertySets.Item("ExtendedSummaryInformation"), DesignManager.Properties)

            Status = CType(CType(PropertySet.Item("Status"), DesignManager.Property).Value, SolidEdgeConstants.DocumentStatus)

            PropertySets.Close()

        Catch ex As Exception
            Dim i = 0
        End Try

        Return Status
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

    Public Function SaveAsPNG(
        View As SolidEdgeFramework.View,
        NewFilename As String
        ) As String

        Dim ExitMessage As String = ""
        Dim TempFilename As String = NewFilename.Replace(".png", "-Housekeeper.tif")

        Try

            View.SaveAsImage(TempFilename)

            Try

                Using fs As New IO.FileStream(TempFilename, IO.FileMode.Open, IO.FileAccess.Read)
                    System.Drawing.Image.FromStream(fs).Save(NewFilename, Imaging.ImageFormat.Png)
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

    Public Function SetStatus(
        DMApp As DesignManager.Application,
        Filename As String,
        NewStatus As SolidEdgeConstants.DocumentStatus,
        Optional StatusChangeRadioButtons As List(Of RadioButton) = Nothing
        ) As Boolean
        ' https://community.sw.siemens.com/s/question/0D54O000061wzRaSAI/changing-document-status

        Dim Success As Boolean = True

        Try
            Dim PropertySets As DesignManager.PropertySets
            Dim PropertySet As DesignManager.Properties
            Dim Prop As DesignManager.Property

            PropertySets = CType(DMApp.PropertySets, DesignManager.PropertySets)
            PropertySets.Open(Filename, False)

            'PropertySet = PropertySets.Item("ExtendedSummaryInformation")
            PropertySet = CType(PropertySets.Item("ExtendedSummaryInformation"), DesignManager.Properties)

            'PropertySet.Item("Status").Value = NewStatus
            Prop = CType(PropertySet.Item("Status"), DesignManager.Property)

            Prop.Value = NewStatus


            PropertySets.Save()

            PropertySet = CType(PropertySets.Item("SummaryInformation"), DesignManager.Properties)
            Prop = CType(PropertySet.Item("Security"), DesignManager.Property)

            If NewStatus = SolidEdgeConstants.DocumentStatus.igStatusAvailable Then
                Prop.Value = StatusSecurityMapping.ssmAvailable
            End If
            If NewStatus = SolidEdgeConstants.DocumentStatus.igStatusBaselined Then
                Prop.Value = StatusSecurityMapping.ssmBaselined
            End If
            If NewStatus = SolidEdgeConstants.DocumentStatus.igStatusInReview Then
                Prop.Value = StatusSecurityMapping.ssmInReview
            End If
            If NewStatus = SolidEdgeConstants.DocumentStatus.igStatusInWork Then
                Prop.Value = StatusSecurityMapping.ssmInWork
            End If
            If NewStatus = SolidEdgeConstants.DocumentStatus.igStatusObsolete Then
                Prop.Value = StatusSecurityMapping.ssmObsolete
            End If
            If NewStatus = SolidEdgeConstants.DocumentStatus.igStatusReleased Then
                Prop.Value = StatusSecurityMapping.ssmReleased
            End If

            PropertySets.Save()
            PropertySets.Close()

            'DMApp.Quit()
        Catch ex As Exception
            Success = False
        End Try

        Return Success
    End Function

    Public Function SplitFOAName(SEDocFullName As String) As Dictionary(Of String, String)
        Dim SplitDict As New Dictionary(Of String, String)
        Dim Filename As String
        Dim MemberName As String

        If SEDocFullName.Contains("!") Then
            Filename = SEDocFullName.Split("!"c)(0)
            MemberName = SEDocFullName.Split("!"c)(1)
        Else
            Filename = SEDocFullName
            MemberName = ""
        End If

        SplitDict("Filename") = Filename
        SplitDict("MemberName") = MemberName

        Return SplitDict
    End Function

    Public Enum StatusSecurityMapping
        ssmAvailable = 0
        ssmInWork = 0
        ssmInReview = 0
        ssmReleased = 4
        ssmBaselined = 4
        ssmObsolete = 4
    End Enum


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

    Public Function CompareListOfColumnsJSON(A As List(Of String), B As List(Of String)) As Boolean
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
                    'If Not PropertySetName = "" Then

                    'End If

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
                FoundProp = PropertySet.Add(PropertyName, "")
                PropertySet.Save()
                PropertySets.Save()
            Catch ex As Exception
                ' Might want to report an error.
            End Try

        End If


        Return FoundProp

    End Function

    Public Function GetOLEProp(
        cf As CompoundFile,
        PropertySet As String,
        PropertyNameEnglish As String,
        AddProp As Boolean
        ) As OLEProperty

        Dim Proceed As Boolean = True

        Dim dsiStream As CFStream = Nothing
        Dim co As OLEPropertiesContainer = Nothing
        Dim OLEProp As OLEProperty = Nothing

        Dim SIList = GetSIList()
        Dim DSIList = GetDSIList()
        Dim FunnyList = GetFunnyList()

        If Not PropertySet = "Custom" Then
            PropertySet = "System"
        End If

        Try
            If (SIList.Contains(PropertyNameEnglish)) And (PropertySet.ToLower = "system") Then
                dsiStream = cf.RootStorage.GetStream("SummaryInformation")
                co = dsiStream.AsOLEPropertiesContainer

                OLEProp = co.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyNameEnglish.ToUpper)

            ElseIf (DSIList.Contains(PropertyNameEnglish)) And (PropertySet.ToLower = "system") Then
                dsiStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
                co = dsiStream.AsOLEPropertiesContainer

                OLEProp = co.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyNameEnglish.ToUpper)

            ElseIf (FunnyList.Contains(PropertyNameEnglish)) And (PropertySet.ToLower = "system") Then
                dsiStream = cf.RootStorage.GetStream("Rfunnyd1AvtdbfkuIaamtae3Ie")
                co = dsiStream.AsOLEPropertiesContainer

                OLEProp = co.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName.ToLower Like "*" & PropertyNameEnglish.ToLower & "*")

            Else
                If PropertySet.ToLower = "custom" Then
                    dsiStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
                    co = dsiStream.AsOLEPropertiesContainer

                    OLEProp = co.UserDefinedProperties.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName = PropertyNameEnglish)
                End If

            End If

        Catch ex As Exception
        End Try

        If (IsNothing(OLEProp)) And (AddProp) And (PropertySet.ToLower = "custom") Then

            ' Add it
            Try
                Dim userProperties = co.UserDefinedProperties
                Dim newPropertyId As UInteger = 2 'For some reason when custom property is empty there is an hidden property therefore the starting index must be 2

                If userProperties.PropertyNames.Keys.Count > 0 Then newPropertyId = CType(userProperties.PropertyNames.Keys.Max() + 1, UInteger)
                'This is the ID the new property will have
                'Duplicated IDs are not allowed
                'We need a method to calculate an unique ID; .Max() seems a good one cause .Max() + 1 should be unique
                'Alternatively we need a method that find unused IDs inbetwen existing one; this will find unused IDs from previous property deletion

                userProperties.PropertyNames(newPropertyId) = PropertyNameEnglish
                OLEProp = userProperties.NewProperty(VTPropertyType.VT_LPSTR, newPropertyId)
                OLEProp.Value = " "
                userProperties.AddProperty(OLEProp)
            Catch ex As Exception
            End Try

        End If

        co.Save(dsiStream)
        cf.Commit()

        Return OLEProp

    End Function

    Public Function GetOLEPropValue(
        cf As CompoundFile,
        PropertySetName As String,
        PropertyName As String,
        AddProp As Boolean
        ) As String

        GetOLEPropValue = ""

        Dim Proceed As Boolean = True

        Dim OLEProp As OLEProperty = Nothing

        Dim SIList = GetSIList()
        Dim DSIList = GetDSIList()
        Dim FunnyList = GetFunnyList()

        Try
            If (SIList.Contains(PropertyName)) And (PropertySetName.ToLower = "system") Then
                Dim System_Stream As CFStream = cf.RootStorage.GetStream("SummaryInformation")
                Dim System_Properties As OLEPropertiesContainer = System_Stream.AsOLEPropertiesContainer

                OLEProp = System_Properties.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyName.ToUpper)

            ElseIf (DSIList.Contains(PropertyName)) And (PropertySetName.ToLower = "system") Then
                Dim System_Stream As CFStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
                Dim System_Properties As OLEPropertiesContainer = System_Stream.AsOLEPropertiesContainer

                OLEProp = System_Properties.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyName.ToUpper)

            ElseIf (FunnyList.Contains(PropertyName)) And (PropertySetName.ToLower = "system") Then
                Dim System_Stream As CFStream = cf.RootStorage.GetStream("Rfunnyd1AvtdbfkuIaamtae3Ie")
                Dim System_Properties As OLEPropertiesContainer = System_Stream.AsOLEPropertiesContainer

                OLEProp = System_Properties.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName.ToLower Like "*" & PropertyName.ToLower & "*")

            Else
                If PropertySetName.ToLower = "custom" Then
                    Dim Custom_Stream As CFStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
                    Dim Custom_Properties As OLEPropertiesContainer = Custom_Stream.AsOLEPropertiesContainer

                    OLEProp = Custom_Properties.UserDefinedProperties.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName = PropertyName)
                End If

            End If

        Catch ex As Exception
        End Try

        If Not IsNothing(OLEProp) Then

            Return OLEProp.Value.ToString

        Else

            Return Nothing

            'If AddProp Then
            '    Try
            '        'TBD Add property here
            '    Catch ex As Exception
            '        Proceed = False
            '    End Try

            'End If
        End If

    End Function

    Public Sub UpdateSingleProperty(FullName As String, PropertySet As String, PropertyNameEnglish As String, PropertyValue As String)

        ' https://stackoverflow.com/questions/26741191/ioexception-the-process-cannot-access-the-file-file-path-because-it-is-being
        Dim Retries As Integer = 3
        Dim RetryDelay As Integer = 1000
        Dim fs As FileStream = Nothing
        For Retry As Integer = 0 To Retries
            Try
                fs = New FileStream(FullName, FileMode.Open, FileAccess.ReadWrite)
                Exit For
            Catch ex As Exception
                System.Threading.Thread.Sleep(RetryDelay)
            End Try
        Next

        If fs Is Nothing Then
            MsgBox("Could not change property", vbOKOnly)
            Exit Sub
        End If

        Dim cfg As CFSConfiguration = CFSConfiguration.SectorRecycle Or CFSConfiguration.EraseFreeSectors
        Dim cf As CompoundFile = New CompoundFile(fs, CFSUpdateMode.Update, cfg)
        Dim OLEProp As OLEProperty = Nothing



        ' ###### my attempt to consolidate fetching OLEProp.  It's not working. ######
        ' Your way is working and I don't see the difference.

        Dim MyWay As Boolean = True

        If MyWay Then
            OLEProp = GetOLEProp(cf, PropertySet, PropertyNameEnglish, AddProp:=True)

            OLEProp.Value = PropertyValue

            cf.Commit()
            cf.Close()
            cf = Nothing
            fs.Close()
            fs = Nothing

        Else
            Dim dsiStream As CFStream = Nothing
            Dim co As OLEPropertiesContainer = Nothing
            'Dim OLEProp As OLEProperty = Nothing

            Dim SIList = GetSIList()
            Dim DSIList = GetDSIList()
            Dim FunnyList = GetFunnyList()

            If (SIList.Contains(PropertyNameEnglish)) Then
                dsiStream = cf.RootStorage.GetStream("SummaryInformation")
                co = dsiStream.AsOLEPropertiesContainer

                OLEProp = co.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyNameEnglish.ToUpper)

            ElseIf (DSIList.Contains(PropertyNameEnglish)) Then
                dsiStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
                co = dsiStream.AsOLEPropertiesContainer

                OLEProp = co.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyNameEnglish.ToUpper)

            ElseIf (FunnyList.Contains(PropertyNameEnglish)) Then
                dsiStream = cf.RootStorage.GetStream("Rfunnyd1AvtdbfkuIaamtae3Ie")
                co = dsiStream.AsOLEPropertiesContainer

                OLEProp = co.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName.ToLower Like "*" & PropertyNameEnglish.ToLower & "*")

            Else  ' Hopefully a Custom Property

                dsiStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
                co = dsiStream.AsOLEPropertiesContainer

                OLEProp = co.UserDefinedProperties.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName = PropertyNameEnglish)

                If IsNothing(OLEProp) Then ' Add it

                    Try
                        Dim userProperties = co.UserDefinedProperties
                        Dim newPropertyId As UInteger = 2 'For some reason when custom property is empty there is an hidden property therefore the starting index must be 2

                        If userProperties.PropertyNames.Keys.Count > 0 Then newPropertyId = CType(userProperties.PropertyNames.Keys.Max() + 1, UInteger)
                        'This is the ID the new property will have
                        'Duplicated IDs are not allowed
                        'We need a method to calculate an unique ID; .Max() seems a good one cause .Max() + 1 should be unique
                        'Alternatively we need a method that find unused IDs inbetwen existing one; this will find unused IDs from previous property deletion

                        userProperties.PropertyNames(newPropertyId) = PropertyNameEnglish
                        OLEProp = userProperties.NewProperty(VTPropertyType.VT_LPSTR, newPropertyId)
                        OLEProp.Value = " "
                        userProperties.AddProperty(OLEProp)
                        Dim i = 0
                    Catch ex As Exception

                        MsgBox("Could not change property", vbOKOnly)

                    End Try

                End If

            End If

            OLEProp.Value = PropertyValue

            co.Save(dsiStream)
            cf.Commit()
            cf.Close()
            cf = Nothing
            fs.Close()
            fs = Nothing

        End If

    End Sub

    Public Function GetSIList() As List(Of String)
        Dim SIList As New List(Of String)
        SIList.AddRange({"Title", "Subject", "Author", "Keywords", "Comments"})
        Return SIList
    End Function

    Public Function GetDSIList() As List(Of String)
        Dim DSIList As New List(Of String)
        DSIList.AddRange({"Category", "Company", "Manager"})
        Return DSIList
    End Function

    Public Function GetFunnyList() As List(Of String)
        Dim FunnyList As New List(Of String)
        FunnyList.AddRange({"Document Number", "Revision", "Project Name"})
        Return FunnyList
    End Function


    Public Function SubstitutePropertyFormula(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal cf As CompoundFile,
        ByVal FullName As String,
        ByVal Instring As String,
        ValidFilenameRequired As Boolean,
        TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String)),
        Optional Expression As Boolean = False
        ) As String

        ' Replaces property formulas in a string
        ' "Material: %{System.Material}, Engineer: %{Custom.Engineer}" --> "Material: STEEL, Engineer: FRED"
        ' "%{System.Titulo}" -> "Va bene!"

        Dim Outstring As String = ""
        Dim tf As Boolean
        Dim Proceed As Boolean = True

        Dim PropertySet As String = ""
        Dim PropertyName As String = ""

        Dim FoundProp As SolidEdgeFramework.Property

        Dim DocValues As New List(Of String)

        Dim i As Integer

        Dim Formulas As New List(Of String)
        Dim Formula As String

        Dim Matches As MatchCollection
        Dim MatchString As Match
        Dim Pattern As String

        Dim ModelIdx As Integer

        Dim UFC As New UtilsFilenameCharmap

        FullName = SplitFOAName(FullName)("Filename")


        ' Any number of substrings that start with "%{" and end with the first encountered "}".
        Pattern = "%{[^}]*}"
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

                Formula = Formula.Replace("%{", "")  ' "%{Custom.hmk_Engineer|R1}" -> "Custom.hmk_Engineer|R1}"
                Formula = Formula.Replace("}", "")   ' "Custom.hmk_Engineer|R1}" -> "Custom.hmk_Engineer|R1"

                tf = Formula.ToLower.Contains("system.")
                tf = tf Or Formula.ToLower.Contains("custom.")

                If tf Then
                    i = Formula.IndexOf(".")  ' First occurrence
                    Dim L = Len(Formula)

                    PropertySet = Formula.Substring(0, i)
                    PropertyName = Formula.Substring(i + 1, L - (i + 1))
                Else
                    Proceed = False
                    'PropertySet = ""
                    'PropertyName = Formula
                End If

                'Not supported by Direct Structured Storage
                If PropertyName.Contains("|R") Then
                    If Not IsNothing(SEDoc) Then
                        i = PropertyName.IndexOf("|")
                        ModelIdx = CInt(PropertyName.Substring(i + 2))  ' "hmk_Engineer|R12" -> 12
                        PropertyName = PropertyName.Substring(0, i)  ' "hmk_Engineer|R1" -> "hmk_Engineer"
                    Else
                        Return "[ERROR]" & PropertyName
                    End If
                Else
                    ModelIdx = 0
                End If

                'Check for special properties %{File Name}, %{File Name (full path)}, %{File Name (no extension)}

                Dim tmpValue As String = Nothing

                If PropertyName.ToLower = "File Name".ToLower Then
                    tmpValue = System.IO.Path.GetFileName(FullName)  ' C:\project\part.par -> part.par
                ElseIf PropertyName.ToLower = "File Name (full path)".ToLower Then
                    tmpValue = FullName
                ElseIf PropertyName.ToLower = "File Name (no extension)".ToLower Then
                    tmpValue = System.IO.Path.GetFileNameWithoutExtension(FullName)  ' C:\project\part.par -> part
                Else
                    If Not IsNothing(SEDoc) Then
                        FoundProp = GetProp(SEDoc, PropertySet, PropertyName, ModelIdx, False)
                        If Not FoundProp Is Nothing Then tmpValue = FoundProp.Value.ToString
                    Else
                        Dim EnglishName As String = TemplatePropertyDict(PropertyName)("EnglishName")
                        tmpValue = GetOLEPropValue(cf, PropertySet, EnglishName, False)
                    End If

                End If

                If tmpValue IsNot Nothing Then
                    If ValidFilenameRequired Then
                        tmpValue = UFC.SubstituteIllegalCharacters(tmpValue)
                    End If

                    DocValues.Add(tmpValue)
                Else
                    Throw New Exception(String.Format("Property '{0}' not found", PropertyName))
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
            If Expression Then

                Dim nCalcExpression As New ExtendedExpression(Outstring)

                Try
                    Dim A = nCalcExpression.Evaluate()

                    Outstring = A.ToString

                Catch ex As Exception

                    Outstring = ex.Message.Replace(vbCrLf, "-")

                End Try

            End If
        End If

        Return Outstring

    End Function


    Public Function CheckValidPropertyFormulas(Instring As String) As Boolean

        Dim Valid As Boolean = True

        Dim Matches As MatchCollection
        Dim MatchString As Match
        Dim Pattern As String


        Pattern = "%{[^}]*}"
        Matches = Regex.Matches(Instring, Pattern)
        If Matches.Count > 0 Then
            For Each MatchString In Matches
                If Not ((MatchString.Value.Contains("%{System.")) Or (MatchString.Value.Contains("%{Custom."))) Then
                    Valid = False
                    Exit For
                End If
            Next
        End If

        Return Valid
    End Function


    Public Sub CopyProperties(Source As Object, Destination As Object)

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
                                    If Not tmpValue Is Nothing Then destProp.SetValue(Destination, tmpValue, Nothing)
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


    Public Function TemplatePropertyDictPopulate(
        TemplateList As List(Of String),
        PreviousTemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
        ) As Dictionary(Of String, Dictionary(Of String, String))

        Dim tmpTemplatePropertyDict As New Dictionary(Of String, Dictionary(Of String, String))

        Dim PreviousFavoritesList = TemplatePropertyGetFavoritesList(PreviousTemplatePropertyDict)

        ' ###### Dict structure below.  English mapping from UC.PropLocalizedToEnglish ######
        ' ###### In case of a duplicate, PropertySet will be 'Duplicate'               ######
        ' ###### and ItemNumbers will be for the 'System' property.                    ######

        '{
        '"Titolo":{
        '    "PropertySet":"SummaryInformation",
        '    "AsmPropItemNumber":"1",
        '    "ParPropItemNumber":"1",
        '    "PsmPropItemNumber":"1",
        '    "DftPropItemNumber":"1",
        '    "EnglishName":"Title",
        '    "PropertySource":"Auto",  ' Was property added automatically or manually?:  "Auto", "Manual"
        '    "FavoritesListIdx":"-1"},  ' If on the Favorites list, 'list index number', otherwise '-1'
        '"Oggetto":{
        '    "PropertySet":"SummaryInformation",
        '    "AsmPropItemNumber":"2",
        '    "ParPropItemNumber":"2",
        '    "PsmPropItemNumber":"2",
        '    "DftPropItemNumber":"2",
        '    "EnglishName":"Subject",
        '    "PropertySource":"Auto",  ' Was property added automatically or manually?:  "Auto", "Manual"
        '    "FavoritesListIdx":"-1"},  ' If on the Favorites list, 'list index number', otherwise '-1'
        ' ...
        '}

        'Dim TemplateDocTypes As List(Of String) = {"asm", "par", "psm", "dft"}.ToList

        Dim PropertySets As SolidEdgeFileProperties.PropertySets
        Dim PropertySet As SolidEdgeFileProperties.Properties
        Dim Prop As SolidEdgeFileProperties.Property
        PropertySets = New SolidEdgeFileProperties.PropertySets
        Dim PropertySetName As String
        Dim PropName As String
        Dim DocType As String

        Dim KeepDict As New Dictionary(Of String, List(Of String))
        KeepDict("SummaryInformation") = {"Title", "Subject", "Author", "Keywords", "Comments"}.ToList
        KeepDict("ExtendedSummaryInformation") = {"Status", "Hardware"}.ToList
        KeepDict("DocumentSummaryInformation") = {"Category", "Manager", "Company"}.ToList
        KeepDict("ProjectInformation") = {"Document Number", "Revision", "Project Name"}.ToList
        KeepDict("MechanicalModeling") = {"Material", "Sheet Metal Gage"}.ToList
        ' Do Custom last to deal with duplicates
        KeepDict("Custom") = New List(Of String)

        Dim tf As Boolean

        'Dim TemplateIdx As Integer = 0

        For Each TemplateName As String In TemplateList
            tf = Not TemplateName = ""
            tf = tf And FileIO.FileSystem.FileExists(TemplateName)
            If tf Then
                DocType = IO.Path.GetExtension(TemplateName).Replace(".", "")  ' 'C:\project\part.par' -> 'par'

                PropertySets.Open(TemplateName, True)

                For Each PropertySetName In KeepDict.Keys

                    Try
                        PropertySet = CType(PropertySets.Item(PropertySetName), SolidEdgeFileProperties.Properties)
                    Catch ex As Exception
                        Continue For
                    End Try
                    'PropertySetName = PropertySet.Name

                    For i = 0 To PropertySet.Count - 1

                        Try
                            Prop = CType(PropertySet.Item(i), SolidEdgeFileProperties.Property)
                            PropName = Prop.Name

                            Dim EnglishName As String = PropLocalizedToEnglish(PropertySetName, i + 1, DocType)
                            If EnglishName = "" Then EnglishName = PropName

                            If Not PropertySetName = "Custom" Then
                                If KeepDict.Keys.Contains(PropertySetName) Then
                                    'If Not KeepDict(PropertySetName).Contains(PropName) Then
                                    '    Continue For
                                    'End If
                                    If Not KeepDict(PropertySetName).Contains(EnglishName) Then
                                        Continue For
                                    End If
                                End If
                            End If

                            If Not tmpTemplatePropertyDict.Keys.Contains(PropName) Then

                                tmpTemplatePropertyDict(PropName) = New Dictionary(Of String, String)

                                tmpTemplatePropertyDict(PropName)("PropertySet") = PropertySetName
                                tmpTemplatePropertyDict(PropName)("AsmPropItemNumber") = ""
                                tmpTemplatePropertyDict(PropName)("ParPropItemNumber") = ""
                                tmpTemplatePropertyDict(PropName)("PsmPropItemNumber") = ""
                                tmpTemplatePropertyDict(PropName)("DftPropItemNumber") = ""
                                tmpTemplatePropertyDict(PropName)("EnglishName") = EnglishName
                                tmpTemplatePropertyDict(PropName)("PropertySource") = "Auto"
                                tmpTemplatePropertyDict(PropName)("FavoritesListIdx") = "-1"
                            Else
                                tf = PropertySetName = "Custom"
                                tf = tf And (Not tmpTemplatePropertyDict(PropName)("PropertySet") = "Custom")
                                If tf Then
                                    tmpTemplatePropertyDict(PropName)("PropertySet") = "Duplicate"
                                End If
                            End If

                            If Not PropertySetName = "Custom" Then
                                Select Case DocType
                                    Case "asm"
                                        tmpTemplatePropertyDict(PropName)("AsmPropItemNumber") = CStr(i + 1)
                                    Case "par"
                                        tmpTemplatePropertyDict(PropName)("ParPropItemNumber") = CStr(i + 1)
                                    Case "psm"
                                        tmpTemplatePropertyDict(PropName)("PsmPropItemNumber") = CStr(i + 1)
                                    Case "dft"
                                        tmpTemplatePropertyDict(PropName)("DftPropItemNumber") = CStr(i + 1)
                                End Select
                            End If

                        Catch ex As Exception
                            Dim s = "Error building TemplatePropertyDict: "
                            s = String.Format("{0} DocType '{1}', PropertySetName '{2}', Item Number '{3}'", s, DocType, PropertySetName, i + 1)
                            MsgBox(s, vbOKOnly)
                        End Try
                    Next
                Next

                PropertySets.Close()

            End If
            'TemplateIdx += 1
        Next

        ' Add Sheet Metal Gage if it's not already there
        PropName = "Sheet Metal Gage"
        If Not tmpTemplatePropertyDict.Keys.Contains(PropName) Then
            tmpTemplatePropertyDict(PropName) = New Dictionary(Of String, String)
            tmpTemplatePropertyDict(PropName)("PropertySet") = "MechanicalModeling"
            tmpTemplatePropertyDict(PropName)("AsmPropItemNumber") = ""
            tmpTemplatePropertyDict(PropName)("ParPropItemNumber") = ""
            tmpTemplatePropertyDict(PropName)("PsmPropItemNumber") = "2"
            tmpTemplatePropertyDict(PropName)("DftPropItemNumber") = ""
            tmpTemplatePropertyDict(PropName)("EnglishName") = PropName
            tmpTemplatePropertyDict(PropName)("PropertySource") = "Auto"
            tmpTemplatePropertyDict(PropName)("FavoritesListIdx") = "-1"
        End If

        ' Add special File properties
        Dim PropNames = {"File Name", "File Name (full path)", "File Name (no extension)"}.ToList
        For Each PropName In PropNames
            If Not tmpTemplatePropertyDict.Keys.Contains(PropName) Then
                tmpTemplatePropertyDict(PropName) = New Dictionary(Of String, String)
                tmpTemplatePropertyDict(PropName)("PropertySet") = "System"
                tmpTemplatePropertyDict(PropName)("AsmPropItemNumber") = ""
                tmpTemplatePropertyDict(PropName)("ParPropItemNumber") = ""
                tmpTemplatePropertyDict(PropName)("PsmPropItemNumber") = ""
                tmpTemplatePropertyDict(PropName)("DftPropItemNumber") = ""
                tmpTemplatePropertyDict(PropName)("EnglishName") = PropName
                tmpTemplatePropertyDict(PropName)("PropertySource") = "Auto"
                tmpTemplatePropertyDict(PropName)("FavoritesListIdx") = "-1"
            End If
        Next

        For i As Integer = 0 To PreviousFavoritesList.Count - 1
            PropName = PreviousFavoritesList(i)
            If tmpTemplatePropertyDict.Keys.Contains(PropName) Then
                tmpTemplatePropertyDict(PropName)("FavoritesListIdx") = CStr(i)
            ElseIf PreviousTemplatePropertyDict.Keys.Contains(PropName) Then
                If PreviousTemplatePropertyDict(PropName)("PropertySource") = "Manual" Then
                    tmpTemplatePropertyDict(PropName) = New Dictionary(Of String, String)
                    tmpTemplatePropertyDict(PropName)("PropertySet") = PreviousTemplatePropertyDict(PropName)("PropertySet")
                    tmpTemplatePropertyDict(PropName)("AsmPropItemNumber") = PreviousTemplatePropertyDict(PropName)("AsmPropItemNumber")
                    tmpTemplatePropertyDict(PropName)("ParPropItemNumber") = PreviousTemplatePropertyDict(PropName)("ParPropItemNumber")
                    tmpTemplatePropertyDict(PropName)("PsmPropItemNumber") = PreviousTemplatePropertyDict(PropName)("PsmPropItemNumber")
                    tmpTemplatePropertyDict(PropName)("DftPropItemNumber") = PreviousTemplatePropertyDict(PropName)("PropertySet")
                    tmpTemplatePropertyDict(PropName)("EnglishName") = PreviousTemplatePropertyDict(PropName)("EnglishName")
                    tmpTemplatePropertyDict(PropName)("PropertySource") = PreviousTemplatePropertyDict(PropName)("PropertySource")
                    tmpTemplatePropertyDict(PropName)("FavoritesListIdx") = CStr(i)
                End If
            End If
        Next

        Return tmpTemplatePropertyDict

    End Function

    Public Function TemplatePropertyGetFavoritesList(
        TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
        ) As List(Of String)

        Dim FavoritesList As New List(Of String)
        Dim FavoritesArray(TemplatePropertyDict.Keys.Count) As String
        Dim Idx As Integer

        For Each PropName As String In TemplatePropertyDict.Keys
            Idx = CInt(TemplatePropertyDict(PropName)("FavoritesListIdx"))
            If Not Idx = -1 Then
                FavoritesArray(Idx) = PropName
            End If
        Next

        For i As Integer = 0 To FavoritesArray.Count - 1
            If Not FavoritesArray(i) = "" Then
                FavoritesList.Add(FavoritesArray(i))
            End If
        Next

        Return FavoritesList

    End Function

    Public Function TemplatePropertyGetAvailableList(
        TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
        ) As List(Of String)

        Dim AvailableList As New List(Of String)

        For Each PropName As String In TemplatePropertyDict.Keys
            AvailableList.Add(PropName)
        Next

        Return AvailableList

    End Function

    Public Function TemplatePropertyDictUpdateFavorites(
        TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String)),
        FavoritesList As List(Of String)
        ) As Dictionary(Of String, Dictionary(Of String, String))

        Dim tmpTemplatePropertyDict = TemplatePropertyDict

        For Each PropName As String In tmpTemplatePropertyDict.Keys
            tmpTemplatePropertyDict(PropName)("FavoritesListIdx") = "-1"
        Next

        For idx As Integer = 0 To FavoritesList.Count - 1
            Dim PropName As String = FavoritesList(idx)
            If tmpTemplatePropertyDict.Keys.Contains(PropName) Then
                tmpTemplatePropertyDict(PropName)("FavoritesListIdx") = CStr(idx)

            End If
        Next

        Return tmpTemplatePropertyDict
    End Function

    Public Function TemplatePropertyDictAddProp(
        TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String)),
        PropertySet As String,
        PropertyName As String,
        EnglishName As String,
        FavoritesListIdx As Integer
        ) As Dictionary(Of String, Dictionary(Of String, String))

        '"Titolo":{
        '    "PropertySet":"SummaryInformation",
        '    "AsmPropItemNumber":"1",
        '    "ParPropItemNumber":"1",
        '    "PsmPropItemNumber":"1",
        '    "DftPropItemNumber":"1",
        '    "EnglishName":"Title",
        '    "PropertySource":"Auto",  ' Was property added automatically or manually?:  "Auto", "Manual"
        '    "FavoritesListIdx":"-1"},  ' If on the Favorites list, 'list index number', otherwise '-1'
        '"Oggetto":{
        '    "PropertySet":"SummaryInformation",
        '    "AsmPropItemNumber":"2",
        '    "ParPropItemNumber":"2",
        '    "PsmPropItemNumber":"2",
        '    "DftPropItemNumber":"2",
        '    "EnglishName":"Subject",
        '    "PropertySource":"Auto",  ' Was property added automatically or manually?:  "Auto", "Manual"
        '    "FavoritesListIdx":"-1"},  ' If on the Favorites list, 'list index number', otherwise '-1'
        ' ...
        '}

        Dim tmpTemplatePropertyDict = TemplatePropertyDict

        If Not tmpTemplatePropertyDict.Keys.Contains(PropertyName) Then
            tmpTemplatePropertyDict(PropertyName) = New Dictionary(Of String, String)

            tmpTemplatePropertyDict(PropertyName)("PropertySet") = PropertySet
            tmpTemplatePropertyDict(PropertyName)("AsmPropItemNumber") = ""
            tmpTemplatePropertyDict(PropertyName)("ParPropItemNumber") = ""
            tmpTemplatePropertyDict(PropertyName)("PsmPropItemNumber") = ""
            tmpTemplatePropertyDict(PropertyName)("DftPropItemNumber") = ""
            tmpTemplatePropertyDict(PropertyName)("EnglishName") = EnglishName
            tmpTemplatePropertyDict(PropertyName)("PropertySource") = "Manual"
            tmpTemplatePropertyDict(PropertyName)("FavoritesListIdx") = CStr(FavoritesListIdx)

        Else
            Dim OriginalPropertySet As String = tmpTemplatePropertyDict(PropertyName)("PropertySet")
            Dim s As String

            If Not ((OriginalPropertySet = "Custom") Or (OriginalPropertySet = "Duplicate")) Then
                OriginalPropertySet = "System"
            End If

            If Not OriginalPropertySet = PropertySet Then
                s = String.Format("The list already contains '{0}' in the '{1}' property set. ", PropertyName, OriginalPropertySet)
                s = String.Format("{0}Do you want to add the new property set '{1}'?", s, PropertySet)
                Dim Result As MsgBoxResult = MsgBox(s, vbYesNo)
                If Result = vbYes Then
                    tmpTemplatePropertyDict(PropertyName)("PropertySet") = "Duplicate"
                    tmpTemplatePropertyDict(PropertyName)("EnglishName") = EnglishName
                    tmpTemplatePropertyDict(PropertyName)("PropertySource") = "Manual"
                    tmpTemplatePropertyDict(PropertyName)("FavoritesListIdx") = CStr(FavoritesListIdx)
                End If
            Else
                s = String.Format("The list already contains '{0}' in the '{1}' property set. ", PropertyName, OriginalPropertySet)
                s = String.Format("{0}No action taken.", s)
                MsgBox(s, vbOKOnly)
            End If
        End If

        Return tmpTemplatePropertyDict

    End Function

    Public Function PropLocalizedToEnglish(PropertySetName As String,
                                           ItemNumber As Integer,
                                           DocType As String) As String

        ' ###### There is a comment contained in each Case statement.               ###### 
        ' ###### It shows the English name for each file type in order.             ###### 
        ' ###### The order is par, psm, asm, dft.                                   ###### 
        ' ###### For example, the following entry is for MechanicalModeling Item 2  ###### 
        ' ###### 'Item 2 [Face Style] [Sheet Metal Gage] [Bead Material] []         ###### 
        ' ###### It says that item's property name for a .par is Face Style,        ###### 
        ' ###### .psm: Sheet Metal Gage, .asm: Bead Material, .dft: (not present),  ###### 

        Dim EnglishName As String = ""

        Select Case PropertySetName

            Case "SummaryInformation"
                Select Case ItemNumber
                    Case 1
                        'Item  [Title] [Title] [Title] [Title] 
                        EnglishName = "Title"
                    Case 2
                        'Item 2 [Subject] [Subject] [Subject] [Subject] 
                        EnglishName = "Subject"
                    Case 3
                        'Item 3 [Author] [Author] [Author] [Author] 
                        EnglishName = "Author"
                    Case 4
                        'Item 4 [Keywords] [Keywords] [Keywords] [Keywords] 
                        EnglishName = "Keywords"
                    Case 5
                        'Item 5 [Comments] [Comments] [Comments] [Comments] 
                        EnglishName = "Comments"
                    Case 6
                        'Item 6 [Template] [Template] [Template] [Template] 
                        EnglishName = "Template"
                    Case 7
                        'Item 7 [Last Author] [Last Author] [Last Author] [Last Author] 
                        EnglishName = "Last Author"
                    Case 8
                        'Item 8 [Revision Number] [Revision Number] [Revision Number] [Revision Number] 
                        EnglishName = "Revision Number"
                    Case 9
                        'Item 9 [Total Editing Time] [Total Editing Time] [Total Editing Time] [Total Editing Time] 
                        EnglishName = "Total Editing Time"
                    Case 10
                        'Item 10 [Last Print Date] [Last Print Date] [Last Print Date] [Last Print Date] 
                        EnglishName = "Last Print Date"
                    Case 11
                        'Item 11 [Origination Date] [Origination Date] [Origination Date] [Origination Date] 
                        EnglishName = "Origination Date"
                    Case 12
                        'Item 12 [Last Save Date] [Last Save Date] [Last Save Date] [Last Save Date] 
                        EnglishName = "Last Save Date"
                    Case 13
                        'Item 13 [Number of pages] [Number of pages] [Number of pages] [Number of pages] 
                        EnglishName = "Number of pages"
                    Case 14
                        'Item 14 [Number of words] [Number of words] [Number of words] [Number of words] 
                        EnglishName = "Number of words"
                    Case 15
                        'Item 15 [Number of characters] [Number of characters] [Number of characters] [Number of characters] 
                        EnglishName = "Number of characters"
                    Case 16
                        'Item 16 [Application Name] [Application Name] [Application Name] [Application Name] 
                        EnglishName = "Application Name"
                    Case 17
                        'Item 17 [Security] [Security] [Security] [Security] 
                        EnglishName = "Security"
                End Select

            Case "ExtendedSummaryInformation"
                Select Case ItemNumber
                    Case 1
                        'Item 1 [Name of Saving Application] [Name of Saving Application] [Name of Saving Application] [Name of Saving Application] 
                        EnglishName = "Name of Saving Application"
                    Case 2
                        'Item 2 [DocumentID] [DocumentID] [DocumentID] [DocumentID] 
                        EnglishName = "DocumentID"
                    Case 3
                        'Item 3 [Status] [Status] [Status] [Status] 
                        EnglishName = "Status"
                    Case 4
                        'Item 4 [Username] [Username] [Username] [Username] 
                        EnglishName = "Username"
                    Case 5
                        'Item 5 [CreationLocale] [CreationLocale] [CreationLocale] [CreationLocale] 
                        EnglishName = "CreationLocale"
                    Case 6
                        'Item 6 [Hardware] [Hardware] [StatusChangeDate] [StatusChangeDate]
                        Select Case DocType
                            Case "par", "psm"
                                EnglishName = "Hardware"
                            Case "asm", "dft"
                                EnglishName = "StatusChangeDate"
                        End Select
                    Case 7
                        'Item 7 [] [StatusChangeDate] [StatusChangeDate] [] 
                        Select Case DocType
                            Case "par", "psm"
                                EnglishName = "StatusChangeDate"
                            Case "asm", "dft"
                                EnglishName = ""
                        End Select
                End Select

            Case "DocumentSummaryInformation"
                Select Case ItemNumber
                    Case 1
                        'Item 1 [Category] [Category] [Category] [Category]
                        EnglishName = "Category"
                    Case 2
                        'Item 2 [Presentation Format] [Presentation Format] [Presentation Format] [Presentation Format]
                        EnglishName = "Presentation Format"
                    Case 3
                        'Item 3 [Byte Count] [Byte Count] [Byte Count] [Byte Count]
                        EnglishName = "Byte Count"
                    Case 4
                        'Item 4 [Lines] [Lines] [Lines] [Lines]
                        EnglishName = "Lines"
                    Case 5
                        'Item 5 [Paragraphs] [Paragraphs] [Paragraphs] [Paragraphs]
                        EnglishName = "Paragraphs"
                    Case 6
                        'Item 6 [Slides] [Slides] [Slides] [Slides]
                        EnglishName = "Slides"
                    Case 7
                        'Item 7 [Notes] [Notes] [Notes] [Notes]
                        EnglishName = "Notes"
                    Case 8
                        'Item 8 [Hidden Objects] [Hidden Objects] [Hidden Objects] [Hidden Objects]
                        EnglishName = "Hidden Objects"
                    Case 9
                        'Item 9 [Multimedia Clips] [Multimedia Clips] [Multimedia Clips] [Multimedia Clips]
                        EnglishName = "Multimedia Clips"
                    Case 10
                        'Item 10 [Manager] [Manager] [Manager] [Manager]
                        EnglishName = "Manager"
                    Case 11
                        'Item 11 [Company] [Company] [Company] [Company]
                        EnglishName = "Company"

                End Select

            Case "ProjectInformation"
                Select Case ItemNumber
                    Case 1
                        'Item 1 [Document Number] [Document Number] [Document Number] [Document Number] 
                        EnglishName = "Document Number"
                    Case 2
                        'Item 2 [Revision] [Revision] [Revision] [Revision] 
                        EnglishName = "Revision"
                    Case 3
                        'Item 3 [Project Name] [Project Name] [Project Name] [Project Name] 
                        EnglishName = "Project Name"

                End Select

            Case "MechanicalModeling"
                Select Case ItemNumber
                    Case 1
                        'Item 1 [Material] [Material] [Material] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Material"
                            Case "psm"
                                EnglishName = "Material"
                            Case "asm"
                                EnglishName = "Material"
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 2
                        'Item 2 [Face Style] [Sheet Metal Gage] [Bead Material] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Face Style"
                            Case "psm"
                                EnglishName = "Sheet Metal Gage"
                            Case "asm"
                                EnglishName = "Bead Material"
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 3
                        'Item 3 [Fill Style] [Face Style] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Fill Style"
                            Case "psm"
                                EnglishName = "Face Style"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 4
                        'Item 4 [Virtual Style] [Fill Style] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Virtual Style"
                            Case "psm"
                                EnglishName = "Fill Style"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 5
                        'Item 5 [Coef. of Thermal Exp] [Virtual Style] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Coef. of Thermal Exp"
                            Case "psm"
                                EnglishName = "Virtual Style"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 6
                        'Item 6 [Thermal Conductivity] [Coef. of Thermal Exp] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Thermal Conductivity"
                            Case "psm"
                                EnglishName = "Coef. of Thermal Exp"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 7
                        'Item 7 [Specific Heat] [Thermal Conductivity] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Specific Heat"
                            Case "psm"
                                EnglishName = "Thermal Conductivity"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 8
                        'Item 8 [Modulus of Elasticity] [Specific Heat] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Modulus of Elasticity"
                            Case "psm"
                                EnglishName = "Specific Heat"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 9
                        'Item 9 [Poisson's Ratio] [Modulus of Elasticity] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Poisson's Ratio"
                            Case "psm"
                                EnglishName = "Modulus of Elasticity"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 10
                        'Item 10 [Yield Stress] [Poisson's Ratio] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Yield Stress"
                            Case "psm"
                                EnglishName = "Poisson's Ratio"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 11
                        'Item 11 [Ultimate Stress] [Yield Stress] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Ultimate Stress"
                            Case "psm"
                                EnglishName = "Yield Stress"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 12
                        'Item 12 [Elongation] [Ultimate Stress] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Elongation"
                            Case "psm"
                                EnglishName = "Ultimate Stress"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 13
                        'Item 13 [Grouping] [Elongation] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "Grouping"
                            Case "psm"
                                EnglishName = "Elongation"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 14
                        'Item 14 [LibraryName] [Grouping] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = "LibraryName"
                            Case "psm"
                                EnglishName = "Grouping"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select
                    Case 15
                        'Item 15 [] [LibraryName] [] []
                        Select Case DocType
                            Case "par"
                                EnglishName = ""
                            Case "psm"
                                EnglishName = "LibraryName"
                            Case "asm"
                                EnglishName = ""
                            Case "dft"
                                EnglishName = ""
                        End Select

                End Select

        End Select



        Return EnglishName
    End Function


    Public Function PropNameFromFormula(PropFormula As String) As String
        ' '%{System.Title}' -> 'Title'
        ' '%{Custom.Donut|R12}' -> 'Donut'

        Dim PropName As String

        PropName = PropFormula
        PropName = PropName.Replace("%{System.", "") ' '%{System.Title}' -> 'Title}'
        PropName = PropName.Replace("%{Custom.", "") ' '%{Custom.Donut|R12}' -> 'Donut|R12}'
        PropName = PropName.Replace("}", "") '         'Title}' -> 'Title'
        PropName = PropName.Split(CChar("|"))(0) '     'Donut|R12' -> 'Donut'

        Return PropName
    End Function

    Public Function PropSetFromFormula(PropFormula As String) As String
        ' '%{System.Title}' -> 'System'
        Dim PropSet As String

        PropSet = PropFormula
        PropSet = PropSet.Replace("%{", "") '    '%{System.Title}' -> 'System.Title}'
        PropSet = PropSet.Replace("}", "") '     'System.Title}' -> 'System.Title'
        PropSet = PropSet.Split(CChar("."))(0) ' 'System.Title' -> 'System'

        Return PropSet
    End Function


    Public Function tmpGetSEProperties(SEDoc As SolidEdgeFramework.SolidEdgeDocument) As Dictionary(Of String, List(Of String))
        Dim PropDict As New Dictionary(Of String, List(Of String))

        Dim PropertySets As SolidEdgeFramework.PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)
        Dim PropertySet As SolidEdgeFramework.Properties
        Dim Prop As SolidEdgeFramework.Property

        Dim s As String
        Dim s1 As String

        For i = 1 To PropertySets.Count
            PropertySet = PropertySets.Item(i)
            s = String.Format("Item {0} [{1}]", i, PropertySet.Name)
            PropDict(s) = New List(Of String)

            For j = 1 To PropertySet.Count
                Prop = PropertySet.Item(j)
                s1 = String.Format("Item {0} [{1}]", j, Prop.Name)
                PropDict(s).Add(s1)
            Next
        Next

        Dim DocType As String = GetDocType(SEDoc)

        Dim UP As New UtilsPreferences

        Dim PreferencesDir As String = UP.GetPreferencesDirectory()
        Dim OutFilename As String = String.Format("{0}/PropDict_{1}.txt", PreferencesDir, DocType)
        Dim OutList As New List(Of String)

        For Each s In PropDict.Keys
            For Each s1 In PropDict(s)
                OutList.Add(String.Format("{0},{1}", s, s1))
            Next
        Next

        IO.File.WriteAllLines(OutFilename, OutList)

        Return PropDict
    End Function

    Public Function GetFileProperties(Filename As String) As List(Of String)
        ' Gets the properties using Windows functionality

        Dim PropList As New List(Of String)
        Dim ValList As New List(Of String)

        Dim shell As New Shell32.Shell
        Dim Directory As Shell32.Folder

        Directory = shell.NameSpace(System.IO.Path.GetDirectoryName(Filename))

        Dim n = 10000
        For Each s In Directory.Items
            If Directory.GetDetailsOf(s, 0) = Path.GetFileName(Filename) Then
                For i = 0 To n
                    Dim Val = Directory.GetDetailsOf(s, i)
                    Dim Key = Directory.GetDetailsOf(Directory.Items, i)
                    If Not (Key = "" And Val = "") Then
                        ValList.Add(String.Format("{0}: {1}", Key, Val))
                    End If
                Next
                Exit For
            End If
        Next

        Return ValList

    End Function




End Class
