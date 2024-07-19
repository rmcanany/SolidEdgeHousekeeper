Option Strict On

Imports System.IO
Imports System.Reflection
Imports System.Security.AccessControl
Imports System.Security.Cryptography
Imports System.Text.RegularExpressions
Imports System.Windows
Imports System.Windows.Forms.PropertyGridInternal
Imports ExcelDataReader
'Imports Microsoft.WindowsAPICodePack
Imports OpenMcdf
Imports OpenMcdf.Extensions
Imports OpenMcdf.Extensions.OLEProperties
Imports PanoramicData.NCalcExtensions
Imports Shell32
Imports SolidEdgePart

Public Class Task_Common

    Public tmpList As Collection

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

    Public Function FilenameIsOK(ByVal fileName As String) As Boolean

        Try
            Dim fi As New IO.FileInfo(fileName)
            Dim i = 1
        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function

    Public Function GetFileProperties(Filename As String) As List(Of String)
        ' Gets the properties using Windows functionality

        Dim PropList As New List(Of String)
        Dim ValList As New List(Of String)

        Dim shell As New Shell32.Shell
        Dim Directory As Shell32.Folder

        Directory = shell.NameSpace(System.IO.Path.GetDirectoryName(Filename))

        Dim n = 1000
        For Each s In Directory.Items
            If Directory.GetDetailsOf(s, 0) = Path.GetFileName(Filename) Then
                For i = 0 To n
                    Dim Val = Directory.GetDetailsOf(s, i)
                    If Not Val = "" Then
                        Dim Key = Directory.GetDetailsOf(Directory.Items, i)
                        ValList.Add(String.Format("{0}: {1}", Key, Val))
                    End If
                Next
                Exit For
            End If
        Next

        Return ValList

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

            Dim FeatureDoctor As New FeatureDoctor
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
                        tmpPointsList = FeatureDoctor.GetBodyRange(Body)
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

                    If PropertySetName = "System" And PropertySet.Name = "SummaryInformation" Then

                        Select Case PropertyName
                            Case = "Title"
                                FoundProp = PropertySet.Item(1)
                                PropertyFound = True
                                Exit For

                            Case = "Subject"
                                FoundProp = PropertySet.Item(2)
                                PropertyFound = True
                                Exit For

                            Case = "Author"
                                FoundProp = PropertySet.Item(3)
                                PropertyFound = True
                                Exit For

                            Case = "Keywords"
                                FoundProp = PropertySet.Item(4)
                                PropertyFound = True
                                Exit For

                            Case = "Comments"
                                FoundProp = PropertySet.Item(5)
                                PropertyFound = True
                                Exit For

                        End Select

                    End If

                    If PropertySetName = "System" And PropertySet.Name = "DocumentSummaryInformation" Then

                        Select Case PropertyName

                            Case = "Category"
                                FoundProp = PropertySet.Item(1)
                                PropertyFound = True
                                Exit For

                            Case = "Company"
                                FoundProp = PropertySet.Item(11)
                                PropertyFound = True
                                Exit For

                            Case = "Manager"
                                FoundProp = PropertySet.Item(10)
                                PropertyFound = True
                                Exit For

                        End Select

                    End If

                    If PropertySetName = "System" And PropertySet.Name = "ProjectInformation" Then

                        Select Case PropertyName

                            Case = "Document Number"
                                FoundProp = PropertySet.Item(1)
                                PropertyFound = True
                                Exit For

                            Case = "Revision"
                                FoundProp = PropertySet.Item(2)
                                PropertyFound = True
                                Exit For

                            Case = "Project Name"
                                FoundProp = PropertySet.Item(3)
                                PropertyFound = True
                                Exit For

                        End Select

                    End If

                    If PropertySetName = "System" And PropertySet.Name = "MechanicalModeling" Then

                        Select Case PropertyName

                            Case = "Material"
                                FoundProp = PropertySet.Item(1)
                                PropertyFound = True
                                Exit For

                            Case = "Sheet Metal Gage"
                                If GetDocType(SEDoc) = "psm" Then
                                    FoundProp = PropertySet.Item(2)
                                    PropertyFound = True
                                    Exit For
                                End If

                        End Select

                    End If

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
                PropertySet = PropertySets.Item("Custom")
                FoundProp = PropertySet.Add(PropertyName, "")
                PropertySet.Save()
                PropertySets.Save()
            Catch ex As Exception

            End Try

        End If


        Return FoundProp

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

        Try

            If PropertySetName = "System" And (PropertyName <> "Category" And PropertyName <> "Manager" And PropertyName <> "Company" And PropertyName <> "Document Number" And PropertyName <> "Revision" And PropertyName <> "Project Name") Then
                Dim System_Stream As CFStream = cf.RootStorage.GetStream("SummaryInformation")
                Dim System_Properties As OLEPropertiesContainer = System_Stream.AsOLEPropertiesContainer

                OLEProp = System_Properties.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyName.ToUpper)
            End If

            If PropertySetName = "System" And (PropertyName = "Category" Or PropertyName = "Manager" Or PropertyName = "Company") Then
                Dim System_Stream As CFStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
                Dim System_Properties As OLEPropertiesContainer = System_Stream.AsOLEPropertiesContainer

                OLEProp = System_Properties.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyName.ToUpper)
            End If

            If PropertySetName = "System" And (PropertyName = "Document Number" Or PropertyName = "Revision" Or PropertyName = "Project Name") Then
                Dim System_Stream As CFStream = cf.RootStorage.GetStream("Rfunnyd1AvtdbfkuIaamtae3Ie")
                Dim System_Properties As OLEPropertiesContainer = System_Stream.AsOLEPropertiesContainer

                OLEProp = System_Properties.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName.ToLower Like "*" & PropertyName.ToLower & "*")
            End If

            If PropertySetName.ToLower = "custom" Then
                Dim Custom_Stream As CFStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
                Dim Custom_Properties As OLEPropertiesContainer = Custom_Stream.AsOLEPropertiesContainer

                OLEProp = Custom_Properties.UserDefinedProperties.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName = PropertyName)
            End If

        Catch ex As Exception

            Proceed = False

        End Try

        If Proceed Then

            If Not IsNothing(OLEProp) Then

                Return OLEProp.Value.ToString

            Else

                If AddProp Then
                    Try
                        'TBD Add property here
                    Catch ex As Exception
                        Proceed = False
                    End Try

                End If

            End If

        End If

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

    Public Function SubstitutePropertyFormula(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal cf As CompoundFile,
        ByVal FullName As String,
        ByVal Instring As String,
        ValidFilenameRequired As Boolean,
        Optional Expression As Boolean = False
        ) As String

        ' Replaces property formulas in a string
        ' "Material: %{System.Material}, Engineer: %{Custom.Engineer}" --> "Material: STEEL, Engineer: FRED"

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

        Dim FCD As New FilenameCharmapDoctor

        FullName = SplitFOAName(FullName)("Filename")

        tf = Instring.Contains("%{System")
        tf = tf Or Instring.Contains("%{Custom")
        tf = tf Or Instring.Contains("%{Project")

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

                Dim tmpFormula = Formula.Split(CType(".", Char))
                If tmpFormula.Length > 0 Then PropertySet = tmpFormula(0) 'Formula.Substring(0, i)    ' "Custom"
                If tmpFormula.Length > 1 Then PropertyName = tmpFormula(1) 'Formula.Substring(i + 1)  ' "hmk_Engineer|R1"

                'Not supported by Direct Structured Storage
                If PropertyName.Contains("|R") Then
                    If Not IsNothing(SEDoc) Then
                        i = PropertyName.IndexOf("|")
                        ModelIdx = CInt(PropertyName.Substring(i + 2))  ' "hmk_Engineer|R1" -> "1"
                        PropertyName = PropertyName.Substring(0, i)  ' "hmk_Engineer|R1" -> "hmk_Engineer"
                    Else
                        Return "[ERROR]" & PropertyName
                    End If
                Else
                    ModelIdx = 0
                End If

                'Check for special properties %{File Name}, %{File Name (full path)}, %{File Name (no extension)}

                Dim tmpValue As String = ""

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
                        tmpValue = GetOLEPropValue(cf, PropertySet, PropertyName, False)
                    End If

                    If ValidFilenameRequired Then
                        tmpValue = FCD.SubstituteIllegalCharacters(tmpValue)
                    End If

                End If

                DocValues.Add(tmpValue)

            Next
        End If

        If Proceed Then
            Outstring = Instring

            For i = 0 To DocValues.Count - 1
                Outstring = Outstring.Replace(Formulas(i), DocValues(i))
            Next

        End If

        If Expression Then

            Dim nCalcExpression As New ExtendedExpression(Outstring)

            Try
                Dim A = nCalcExpression.Evaluate()

                Outstring = A.ToString

            Catch ex As Exception

                Outstring = ex.Message.Replace(vbCrLf, "-")

            End Try

        End If

        Return Outstring

    End Function

End Class
