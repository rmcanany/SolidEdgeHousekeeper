Option Strict On

Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml
Imports OpenMcdf
Imports OpenMcdf.Extensions
Imports OpenMcdf.Extensions.OLEProperties
Imports PanoramicData.NCalcExtensions

Public Class HCStructuredStorageDoc

    Public Property FullName As String

    Private Property PropSets As PropertySets
    Private Property fs As FileStream
    Private Property cf As CompoundFile
    Private Property LinkNames As LinkFullNames
    Private Property LinkManagementOrder As List(Of String)
    Private Property PropertiesData As HCPropertiesData
    Private Property MatTable As MaterialTable
    Private Property DocType As String 'asm, dft, par, psm, mtl

    Public Sub New(_FullName As String)

        Me.FullName = _FullName

        Try
            Me.fs = New FileStream(Me.FullName, FileMode.Open, FileAccess.ReadWrite)
        Catch ex As Exception
            Throw New Exception("Unable to open file")
        End Try

        Me.cf = Nothing
        Try
            Dim cfg As CFSConfiguration = CFSConfiguration.SectorRecycle Or CFSConfiguration.EraseFreeSectors
            Me.cf = New CompoundFile(fs, CFSUpdateMode.Update, cfg)
        Catch ex As Exception
            Me.fs.Close()
            Me.fs = Nothing
            Throw New Exception("Unable to open file")
        End Try

        If IsFOA(cf) Then
            Throw New Exception("FOA files currently not supported")
        End If

        Me.DocType = IO.Path.GetExtension(FullName).ToLower.Replace(".", "")

    End Sub

    Public Sub ReadProperties(_PropertiesData As HCPropertiesData)
        Me.PropertiesData = _PropertiesData
        Me.PropSets = New PropertySets(Me.cf, Me.FullName)
    End Sub

    Public Sub ReadLinks(_LinkManagementOrder As List(Of String))
        Me.LinkManagementOrder = _LinkManagementOrder
        Me.LinkNames = New LinkFullNames(cf, IsFOA(Me.cf), Me.LinkManagementOrder, Me.FullName)
    End Sub

    Public Sub ReadMaterialTable()
        Dim Extension As String = IO.Path.GetExtension(Me.FullName)

        If Not Extension = ".mtl" Then
            Throw New Exception(String.Format("'{0}' is not a material table file", IO.Path.GetFileName(Me.FullName)))
        End If

        Me.MatTable = New MaterialTable(Me.cf)
    End Sub


    Public Function IsFOA(cf As CompoundFile) As Boolean
        Return cf.RootStorage.ContainsStorage("Master")
    End Function

    Public Function IsFOPMaster(cf As CompoundFile) As Boolean
        Return cf.RootStorage.ContainsStorage("FamilyOfParts")
    End Function

    Public Function IsMaterialTable(cf As CompoundFile) As Boolean
        Return cf.RootStorage.ContainsStream("MaterialDataEx")
    End Function

    Public Sub Save()

        If Me.PropSets IsNot Nothing Then
            For Each PropSet In Me.PropSets.Items
                ' The Custom stream gets saved with DocumentSummaryInformation
                If Not PropSet.Name.ToLower = "custom" Then
                    PropSet.Save()
                End If
            Next
        End If

        Me.cf.Commit()

    End Sub

    Public Sub Close()
        If Me.cf IsNot Nothing Then Me.cf.Close()
        Me.cf = Nothing

        If Me.fs IsNot Nothing Then Me.fs.Close()
        Me.fs = Nothing
    End Sub

    Public Function GetPropValue(PropSetName As String, PropNameEnglish As String) As Object
        ' Return Nothing if a value is not found

        Dim Value As Object = Nothing

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        PropNameEnglish = PropNameEnglish.ToLower

        Value = ProcessSpecialProperty(PropSetName, PropNameEnglish)

        If Value Is Nothing Then
            Dim Prop As Prop = GetProp(PropSetName, PropNameEnglish)
            If Prop IsNot Nothing Then
                Value = Prop.Value
            End If
        End If

        Return Value
    End Function

    Public Function GetPropTypeName(PropSetName As String, PropNameEnglish As String) As String
        ' Returns Nothing if the property is not found

        Dim TypeName As String = Nothing
        Dim VTType As VTPropertyType = Nothing

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        PropNameEnglish = PropNameEnglish.ToLower

        'TypeName = ProcessSpecialProperty(PropSetName, PropNameEnglish)

        Dim Prop As Prop = GetProp(PropSetName, PropNameEnglish)

        If Prop IsNot Nothing Then
            VTType = Prop.VTType
            Select Case VTType
                Case = VTPropertyType.VT_BOOL
                    TypeName = "Boolean"
                Case = VTPropertyType.VT_I4
                    TypeName = "Integer"
                Case = VTPropertyType.VT_LPSTR, VTPropertyType.VT_LPWSTR
                    TypeName = "String"
                Case = VTPropertyType.VT_FILETIME
                    TypeName = "Date"
                Case = VTPropertyType.VT_R8
                    TypeName = "Double"
                Case Else
                    Dim s = GetType(HCStructuredStorageDoc).FullName
                    MsgBox(String.Format("In {0}: Property type {1} not recognized", s, VTType.ToString))
            End Select
        End If

        Return TypeName
    End Function

    Public Function SetPropValue(PropSetName As String, PropNameEnglish As String, Value As Object, AddProperty As Boolean) As Boolean
        ' Returns False if unsuccessful

        Dim Success As Boolean = False

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        PropNameEnglish = PropNameEnglish.ToLower

        If ProcessSpecialProperty(PropSetName, PropNameEnglish) Is Nothing Then
            Dim Prop = GetProp(PropSetName, PropNameEnglish)

            If Prop IsNot Nothing Then
                Prop.Value = Value

                If Prop.Value.ToString = Value.ToString Then
                    Success = True
                End If

            ElseIf AddProperty Then
                Success = AddProp(PropSetName, PropNameEnglish, Value)
            End If
        End If

        Return Success
    End Function

    Public Function AddProp(PropSetName As String, PropNameEnglish As String, Value As Object) As Boolean
        Dim Success As Boolean = False

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        Dim PropSet As PropertySet = Me.PropSets.GetItem(PropSetName)

        PropNameEnglish = PropNameEnglish.ToLower

        If PropSetName.ToLower = "custom" Then
            Dim Prop = GetProp(PropSetName, PropNameEnglish)
            If Prop Is Nothing Then
                Success = PropSet.AddProp(PropNameEnglish, Value)
            End If
        End If

        Return Success
    End Function

    Public Function DeleteProp(PropSetName As String, PropNameEnglish As String) As Boolean
        Dim Success As Boolean = False

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        Dim PropSet As PropertySet = Me.PropSets.GetItem(PropSetName)

        PropNameEnglish = PropNameEnglish.ToLower

        If PropSetName.ToLower = "custom" Then
            Dim Prop = GetProp(PropSetName, PropNameEnglish)
            If Prop IsNot Nothing Then
                Success = PropSet.DeleteProp(PropNameEnglish)
            End If
        End If

        Return Success
    End Function

    Public Function ExistsProp(PropSetName As String, PropNameEnglish As String) As Boolean
        Dim Success As Boolean = False

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        PropNameEnglish = PropNameEnglish.ToLower

        If ProcessSpecialProperty(PropSetName, PropNameEnglish) Is Nothing Then
            Dim Prop = GetProp(PropSetName, PropNameEnglish)

            Success = Prop IsNot Nothing
        Else
            Success = True
        End If

        Return Success
    End Function



    Public Function GetPropNames() As List(Of String)
        Dim PropNames As New List(Of String)

        For Each _PropSet As PropertySet In Me.PropSets.Items
            If Not _PropSet.Name = "Custom" Then
                For Each _Prop As Prop In _PropSet.Items
                    If Not PropNames.Contains(_Prop.Name) Then PropNames.Add(String.Format("{0}, {1}, {2}", _PropSet.Name, CStr(_Prop.PropertyIdentifier), _Prop.Name))
                Next
            End If
        Next

        Return PropNames
    End Function



    Public Function SubstitutePropertyFormulas(
         InString As String,
         ValidFilenameRequired As Boolean,
         Optional IsExpression As Boolean = False
         ) As String

        ' Replaces property formulas in a string.
        ' Returns Nothing if an error occurs

        ' Examples
        ' "Material: %{System.Material}, Engineer: %{Custom.Engineer}" --> "Material: STEEL, Engineer: FRED"
        ' "%{System.Titulo}"                                           --> "Ciao Pizza!"
        ' "%{Custom.Engineer|R1}"                                      --> "Fred" (From index reference 1)
        ' "%{Server.Query|R3}"                                         --> "Some response from server" (From server query 3)

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        Dim OutString As String = Nothing

        Dim UC As New UtilsCommon
        'Dim UFC As New UtilsFilenameCharmap

        Dim Proceed As Boolean = True

        Dim DocValues As New List(Of String)
        Dim DocValue As String

        Dim Formulas As New List(Of String)
        Dim Formula As String

        Dim Matches As MatchCollection
        Dim MatchString As Match
        Dim Pattern As String


        ' Any number of substrings that start with "%{" and end with the first encountered "}".
        Pattern = "%{[^}]*}"
        Matches = Regex.Matches(InString, Pattern)
        If Matches.Count = 0 Then
            OutString = InString
            Proceed = False
        Else
            For Each MatchString In Matches
                Formulas.Add(MatchString.Value)
            Next
        End If

        If Proceed Then
            For Each Formula In Formulas
                DocValue = ProcessFormula(Formula, ValidFilenameRequired)
                If DocValue Is Nothing Then
                    Return Nothing
                Else
                    DocValues.Add(DocValue)
                End If
            Next
        End If

        If Proceed Then
            OutString = InString

            For i = 0 To DocValues.Count - 1
                OutString = OutString.Replace(Formulas(i), DocValues(i))
            Next

        End If

        If Proceed Then
            If IsExpression Then

                Dim nCalcExpression As New ExtendedExpression(OutString)

                Try
                    Dim A = nCalcExpression.Evaluate()

                    OutString = A.ToString

                Catch ex As Exception

                    OutString = ex.Message.Replace(vbCrLf, "-")

                End Try

            End If
        End If


        Return OutString
    End Function


    Private Function ProcessFormula(Formula As String, ValidFilenameRequired As Boolean) As String
        Dim DocValue As String = Nothing

        Dim UC As New UtilsCommon
        Dim UFC As New UtilsFilenameCharmap

        Dim PropertySetName As String
        Dim PropertyName As String
        Dim PropertyNameEnglish As String
        Dim ModelIdx As Integer

        Dim LinkName As String


        PropertySetName = UC.PropSetFromFormula(Formula)
        PropertyName = UC.PropNameFromFormula(Formula)
        ModelIdx = UC.ModelIdxFromFormula(Formula)

        If Not ((PropertySetName = "System") Or (PropertySetName = "Custom") Or (PropertySetName = "Server")) Then
            Return Nothing
        End If

        If (PropertySetName = "Server") And (PropertyName = "Query") Then
            Return Form_Main.ExecuteQuery(FullName, Form_Main.ServerQuery, ModelIdx).Replace(vbCrLf, " ")
        End If

        PropertyNameEnglish = Me.PropertiesData.GetPropertyData(PropertyName).EnglishName

        If ModelIdx = 0 Then
            DocValue = CStr(GetPropValue(PropertySetName, PropertyNameEnglish))
            If DocValue Is Nothing Then
                Return Nothing
            Else
                If ValidFilenameRequired Then
                    DocValue = UFC.SubstituteIllegalCharacters(DocValue)
                End If
            End If

        Else
            If Me.LinkNames Is Nothing Then
                Throw New Exception("LinkNames not initialized")
            End If

            If ModelIdx > Me.LinkNames.Items.Count - 1 Then
                Return Nothing
            End If

            LinkName = Me.LinkNames.Items(ModelIdx - 1)

            Dim SSDoc As HCStructuredStorageDoc = Nothing
            Try
                SSDoc = New HCStructuredStorageDoc(LinkName)
                SSDoc.ReadProperties(Me.PropertiesData)
            Catch ex As Exception
                If SSDoc IsNot Nothing Then SSDoc.Close()
                Return Nothing
            End Try

            DocValue = CStr(SSDoc.GetPropValue(PropertySetName, PropertyNameEnglish))
            If DocValue Is Nothing Then
                SSDoc.Close()
                Return Nothing
            Else
                If ValidFilenameRequired Then
                    DocValue = UFC.SubstituteIllegalCharacters(DocValue)
                End If
            End If

            SSDoc.Close()
        End If

        Return DocValue
    End Function

    Private Function GetProp(
        PropSetName As String,
        PropNameEnglish As String,
        Optional ByRef PropSets As PropertySets = Nothing,
        Optional ByRef PropSet As PropertySet = Nothing
        ) As Prop

        If Me.PropSets Is Nothing Then
            Return Nothing
        End If

        PropNameEnglish = PropNameEnglish.ToLower

        Dim Prop As Prop = Nothing

        If (PropSetName.ToLower = "system") Or (PropSetName.ToLower = "duplicate") Then

            ' Need to search for it
            For Each PropSet In Me.PropSets.Items
                If Not PropSet.Name.ToLower = "custom" Then
                    For Each P As Prop In PropSet.Items
                        If P.Name = PropNameEnglish Then
                            Prop = P
                            Exit For
                        End If
                    Next
                    If Prop IsNot Nothing Then
                        Exit For
                    End If
                End If
            Next

        Else
            PropSet = Me.PropSets.GetItem(PropSetName)
            If PropSet IsNot Nothing Then
                Prop = PropSet.GetItem(PropNameEnglish)
            End If

        End If

        Return Prop
    End Function

    Private Function ProcessSpecialProperty(PropSetName As String, PropNameEnglish As String) As String
        ' Returns Nothing if not a special property
        ' Special properties:
        '     "System.File Name (full path)"     ' C:\project\part.par -> C:\project\part.par
        '     "System.File Name"                 ' C:\project\part.par -> part.par
        '     "System.File Name (no extension)"  ' C:\project\part.par -> part

        PropNameEnglish = PropNameEnglish.ToLower

        Dim SpecialProperty As String = Nothing

        If PropSetName.ToLower = "system" Then
            If PropNameEnglish = "File Name".ToLower Then
                SpecialProperty = Me.FullName
            ElseIf PropNameEnglish = "File Name (full path)".ToLower Then
                SpecialProperty = System.IO.Path.GetFileName(Me.FullName)
            ElseIf PropNameEnglish = "File Name (no extension)".ToLower Then
                SpecialProperty = System.IO.Path.GetFileNameWithoutExtension(Me.FullName)
            End If
        End If

        Return SpecialProperty
    End Function

    Private Sub OutputPropList()
        Dim Outfile As String = ".\ole_props.csv"
        'Dim s As String
        'Dim InList As New List(Of String)
        Dim OutList As New List(Of String)

        If IO.File.Exists(Outfile) Then
            Try
                OutList = IO.File.ReadAllLines(Outfile).ToList
            Catch ex As Exception
                MsgBox("Error reading Outfile")
            End Try
        End If

        OutList = Me.PropSets.PrepOutput(OutList, Me.FullName)

        Try
            IO.File.WriteAllLines(Outfile, OutList)
        Catch ex As Exception
            MsgBox("Error saving Outfile")
        End Try


    End Sub

    Public Function IsFileEmpty() As Boolean
        Dim IsEmpty As Boolean

        Select Case Me.DocType
            Case "par", "psm"
                Dim ParasolidStorage As CFStorage = Me.cf.RootStorage.GetStorage("PARASOLID")
                Dim StreamList As New List(Of CFStream)
                ParasolidStorage.VisitEntries(Sub(item) If item.IsStream Then StreamList.Add(CType(item, CFStream)), recursive:=False)
                IsEmpty = StreamList.Count = 0
            Case "asm", "dft"
                If Me.LinkNames IsNot Nothing Then
                    IsEmpty = (Me.LinkNames.Items.Count = 0) And (Me.LinkNames.BadLinks.Count = 0)
                Else
                    Dim tmpLinkManagmentOrder As List(Of String) = {"CONTAINER", "RELATIVE", "ABSOLUTE"}.ToList
                    Dim tmpLinkNames = New LinkFullNames(Me.cf, IsFOA(Me.cf), tmpLinkManagmentOrder, Me.FullName)
                    IsEmpty = (tmpLinkNames.Items.Count = 0) And (tmpLinkNames.BadLinks.Count = 0)
                End If
        End Select

        Return IsEmpty
    End Function

    Public Function GetLinkNames() As List(Of String)
        If Me.LinkNames Is Nothing Then
            Throw New Exception("Links not initialized")
        End If

        Return Me.LinkNames.Items
    End Function

    Public Function GetBadLinkNames() As List(Of String)
        If Me.LinkNames Is Nothing Then
            Throw New Exception("Links not initialized")
        End If

        Return Me.LinkNames.BadLinks
    End Function


    Public Function MaterialInTable(Name As String) As Boolean
        If Me.MatTable Is Nothing Then
            Throw New Exception("Material table is not initialized")
        End If

        Return MatTable.GetMaterial(Name) IsNot Nothing
    End Function

    Public Function MaterialUpToDate(SSDoc As HCStructuredStorageDoc) As Boolean
        If Me.MatTable Is Nothing Then
            Throw New Exception("Material table is not initialized")
        End If

        Return Me.MatTable.MaterialUpToDate(SSDoc)
    End Function

    'Public Function UpdateMaterial(SSDoc As HCStructuredStorageDoc) As Boolean
    '    If Me.MatTable Is Nothing Then
    '        Throw New Exception("Material table is not initialized")
    '    End If

    '    Return Me.MatTable.UpdateMaterial(SSDoc)
    'End Function


    Private Class PropertySets
        Public Property Items As List(Of PropertySet)

        Private Property cf As CompoundFile
        Private Property FullName As String
        Private Property PropertySetNames As List(Of String)

        Public Sub New(_cf As CompoundFile, _FullName As String)
            Me.cf = _cf
            Me.FullName = _FullName

            Me.Items = New List(Of PropertySet)

            Me.PropertySetNames = New List(Of String)

            Dim tmpPropertySetNames As New List(Of String)

            tmpPropertySetNames.AddRange({"SummaryInformation", "DocumentSummaryInformation", "ExtendedSummaryInformation"})
            tmpPropertySetNames.AddRange({"ProjectInformation", "MechanicalModeling", "Custom"})

            Dim cs As CFStream = Nothing
            Dim co As OLEPropertiesContainer = Nothing

            For Each PropertySetName As String In tmpPropertySetNames
                If Not PropertySetName = "Custom" Then

                    'Not all files have every PropertySet
                    'Properties of some very old files appear to be organized differently
                    Try
                        Me.Items.Add(New PropertySet(Me.cf, PropertySetName))
                        Me.PropertySetNames.Add(PropertySetName)
                    Catch ex As Exception
                    End Try

                    If (PropertySetName = "DocumentSummaryInformation") And (PropertySetNames.Contains(PropertySetName)) Then
                        cs = GetItem("DocumentSummaryInformation").cs
                        co = GetItem("DocumentSummaryInformation").co
                    End If
                Else
                    ' The Custom PropertySet needs the same cs and co as DocumentSummaryInformation, not copies.
                    If (cs IsNot Nothing) And (cs IsNot Nothing) Then
                        Me.Items.Add(New PropertySet(Me.cf, PropertySetName, cs, co))
                        PropertySetNames.Add(PropertySetName)
                    End If
                End If

            Next

        End Sub

        Public Function PrepOutput(InList As List(Of String), FullName As String) As List(Of String)
            Dim OutList As New List(Of String)
            Dim tmpList As List(Of String)
            Dim s As String

            OutList.AddRange(InList)

            For Each PS As PropertySet In Me.Items
                tmpList = PS.PrepOutput()
                For Each s In tmpList
                    If Not OutList.Contains(s) Then
                        OutList.Add(s)
                        If (Not s.Contains("Custom")) And (OutList.Count > 100) Then
                            OutList.Add(FullName)
                        End If

                    End If
                Next
            Next

            Return OutList
        End Function

        Public Function GetItem(PropertySetName As String) As PropertySet
            Dim idx As Integer = PropertySetNames.IndexOf(PropertySetName)
            If idx = -1 Then
                Return Nothing
            Else
                Return Items(idx)
            End If
        End Function

    End Class


    Private Class PropertySet
        Public Property Items As New List(Of Prop)
        Public Property Name As String

        Private Property cf As CompoundFile
        Public Property cs As CFStream
        Public Property co As OLEPropertiesContainer
        Private Property PropNames As New List(Of String)
        Private Property PropertySetNameToStreamName As New Dictionary(Of String, String)

        Public Sub New(_cf As CompoundFile,
                       PropertySetName As String,
                       Optional _cs As CFStream = Nothing,
                       Optional _co As OLEPropertiesContainer = Nothing)

            Me.cf = _cf
            Me.Name = PropertySetName

            Me.PropertySetNameToStreamName("SummaryInformation") = "SummaryInformation"
            Me.PropertySetNameToStreamName("DocumentSummaryInformation") = "DocumentSummaryInformation"
            Me.PropertySetNameToStreamName("ProjectInformation") = "Rfunnyd1AvtdbfkuIaamtae3Ie"
            Me.PropertySetNameToStreamName("ExtendedSummaryInformation") = "C3teagxwOttdbfkuIaamtae3Ie"
            Me.PropertySetNameToStreamName("MechanicalModeling") = "K4teagxwOttdbfkuIaamtae3Ie"
            Me.PropertySetNameToStreamName("Custom") = "DocumentSummaryInformation"

            Dim StreamName As String = Me.PropertySetNameToStreamName(PropertySetName)

            If Not Me.Name.ToLower = "custom" Then
                Me.cs = Me.cf.RootStorage.GetStream(StreamName)
                Me.co = Me.cs.AsOLEPropertiesContainer
            Else
                Me.cs = _cs
                Me.co = _co
            End If

            Dim CorrectedName As String

            If PropertySetName.ToLower = "custom" Then
                If co.HasUserDefinedProperties Then
                    For Each OLEProp As OLEProperty In co.UserDefinedProperties.Properties
                        CorrectedName = CorrectOLEPropName(OLEProp.PropertyName, PropertySetName)
                        Me.PropNames.Add(CorrectedName)
                        Me.Items.Add(New Prop(OLEProp, CorrectedName))
                    Next
                End If

            Else
                For Each OLEProp As OLEProperty In co.Properties
                    CorrectedName = CorrectOLEPropName(OLEProp.PropertyName, PropertySetName)
                    Me.PropNames.Add(CorrectedName)
                    Me.Items.Add(New Prop(OLEProp, CorrectedName))
                Next

            End If

        End Sub

        Public Function PrepOutput() As List(Of String)
            Dim OutList As New List(Of String)
            Dim tmpList As List(Of String)
            Dim s As String

            For Each P As Prop In Me.Items
                tmpList = New List(Of String)
                tmpList.Add(Me.Name)
                tmpList = P.PrepOutput(tmpList)
                s = ""
                s = String.Join(",", tmpList)
                OutList.Add(s)
            Next

            Return OutList
        End Function

        Public Function GetItem(PropName As String) As Prop
            Dim idx As Integer = PropNames.IndexOf(PropName)
            If idx = -1 Then
                Return Nothing
            Else
                Return Items(idx)
            End If
        End Function

        Public Sub Save()
            Me.co.Save(Me.cs)
            'Me.cf.Commit()
        End Sub

        Public Function AddProp(PropertyNameEnglish As String, Value As Object) As Boolean
            Dim Success As Boolean = True

            Dim OLEProp As OLEProperty = Nothing
            Dim UserProperties As OLEPropertiesContainer
            Dim NewPropertyId As UInteger

            PropertyNameEnglish = PropertyNameEnglish.ToLower

            If Value Is Nothing Then
                Value = " "
            End If

            Try
                UserProperties = co.UserDefinedProperties

                If UserProperties.PropertyNames.Keys.Count = 0 Then
                    ' The first two PropertyIDs (0 and 1) are reserved.  (Saw a reference to this somewhere in the OpenMCDF source code.)
                    NewPropertyId = 2
                Else
                    ' Could possibly find an unused ID number.  Going with .Max() + 1 for now.
                    NewPropertyId = CType(UserProperties.PropertyNames.Keys.Max() + 1, UInteger)
                End If

                UserProperties.PropertyNames(NewPropertyId) = PropertyNameEnglish
                OLEProp = UserProperties.NewProperty(VTPropertyType.VT_LPSTR, NewPropertyId)
                OLEProp.Value = Value.ToString
                UserProperties.AddProperty(OLEProp)

            Catch ex As Exception
                Success = False
            End Try

            If OLEProp Is Nothing Then
                Success = False
            End If

            If Success Then
                PropNames.Add(PropertyNameEnglish)
                Dim Prop = New Prop(OLEProp, PropertyNameEnglish)
                Items.Add(Prop)
            End If

            Return Success
        End Function

        Public Function DeleteProp(PropertyNameEnglish As String) As Boolean
            Dim Success As Boolean = True

            PropertyNameEnglish = PropertyNameEnglish.ToLower

            Dim Prop = GetItem(PropertyNameEnglish)

            If Prop IsNot Nothing Then
                Try
                    co.UserDefinedProperties.RemoveProperty(Prop.PropertyIdentifier)
                Catch ex As Exception
                    Success = False
                End Try
            End If

            If Success Then
                Dim idx As Integer = PropNames.IndexOf(PropertyNameEnglish)
                If Not idx = -1 Then
                    PropNames.RemoveAt(idx)
                    Items.RemoveAt(idx)
                End If

            End If

            Return Success
        End Function

        Private Function CorrectOLEPropName(OLEPropName As String, PropertySetName As String) As String
            Dim CorrectedName As String = ""

            Select Case PropertySetName
                Case "SummaryInformation"
                    CorrectedName = OLEPropName.Replace("PIDSI_", "")
                Case "DocumentSummaryInformation"
                    CorrectedName = OLEPropName.Replace("PIDDSI_", "")
                Case Else
                    CorrectedName = OLEPropName
            End Select

            CorrectedName = CorrectedName.ToLower

            Return CorrectedName
        End Function


    End Class

    Private Class Prop
        Public Property Name As String

        Public Property Value As Object
            Get
                Return Me.OLEProp.Value
            End Get
            Set(value As Object)
                UpdatePropValue(value)
            End Set
        End Property

        Public Property VTType As VTPropertyType
        Public Property PropertyIdentifier As UInteger

        Private Property OLEProp As OLEProperty

        Public Sub New(_OLEProp As OLEProperty, CorrectedName As String)
            Me.OLEProp = _OLEProp
            Me.Name = CorrectedName
            Me.Value = Me.OLEProp.Value
            Me.VTType = Me.OLEProp.VTType
            Me.PropertyIdentifier = Me.OLEProp.PropertyIdentifier
        End Sub

        Public Function PrepOutput(InList As List(Of String)) As List(Of String)
            Dim tmpList As New List(Of String)
            Dim L = 20
            Dim v As String

            Try
                v = CStr(Me.Value)
                If Len(v) > L Then v = v.Substring(0, L)
            Catch ex As Exception
                v = "Value exception"
            End Try

            'tmpList.AddRange({CStr(Me.PropertyIdentifier), Me.OLEProp.PropertyName, Me.Name, v})
            tmpList.AddRange({CStr(Me.PropertyIdentifier), Me.OLEProp.PropertyName, Me.Name})

            For Each s As String In tmpList
                InList.Add(s.Replace(",", "~"))
            Next

            Return InList
        End Function

        Private Sub UpdatePropValue(PropertyValue As Object)

            ' VTPropertyType enum: https://github.com/ironfede/openmcdf/blob/master/OpenMcdf.Ole/VTPropertyType.cs

            Dim Success As Boolean = True
            Dim RequiredType As String = ""

            Select Case OLEProp.VTType

                Case = VTPropertyType.VT_BOOL
                    RequiredType = "Boolean"
                    Try
                        'OLEProp.Value = CType(PropertyValue, Boolean)
                        OLEProp.Value = CBool(PropertyValue)
                    Catch ex As Exception
                        Success = False
                    End Try

                Case = VTPropertyType.VT_I4
                    RequiredType = "Integer"
                    Try
                        'OLEProp.Value = CType(PropertyValue, Integer)
                        OLEProp.Value = CInt(PropertyValue)
                    Catch ex As Exception
                        Success = False
                    End Try

                Case = VTPropertyType.VT_LPSTR, VTPropertyType.VT_LPWSTR
                    RequiredType = "String"
                    Try
                        OLEProp.Value = PropertyValue.ToString
                    Catch ex As Exception
                        Success = False
                    End Try

                Case = VTPropertyType.VT_FILETIME
                    RequiredType = "Date"
                    Try
                        OLEProp.Value = CType(PropertyValue, DateTime)
                    Catch ex As Exception
                        Success = False
                    End Try

                Case = VTPropertyType.VT_R8
                    RequiredType = "Double"
                    Try
                        'OLEProp.Value = CType(PropertyValue, Double)
                        OLEProp.Value = CDbl(PropertyValue)
                    Catch ex As Exception
                        Success = False
                    End Try

            End Select

            If Not Success Then
                Throw New Exception(String.Format("In property '{0}': Could not convert '{1}' to type '{2}'", Me.Name, Value.ToString, RequiredType))
            End If

        End Sub

    End Class


    Private Class LinkFullNames
        Public Property Items As New List(Of String)
        Public Property BadLinks As New List(Of String)

        Private Property cf As CompoundFile
        Private Property IsFOA As Boolean
        Private Property LinkManagementOrder As List(Of String)
        Private Property ContainingFileFullName As String

        Public Sub New(_cf As CompoundFile, _IsFOA As Boolean, _LinkManagementOrder As List(Of String), _ContainingFileFullName As String)
            Me.cf = _cf
            Me.IsFOA = _IsFOA
            Me.LinkManagementOrder = _LinkManagementOrder
            Me.ContainingFileFullName = _ContainingFileFullName

            GetFullNames()
        End Sub

        Private Sub GetFullNames()
            Dim RootStorages As New List(Of CFStorage)

            If Me.IsFOA Then
                Me.cf.RootStorage.VisitEntries(Sub(item) If item.IsStorage Then RootStorages.Add(CType(item, CFStorage)), recursive:=False)
            Else
                RootStorages.Add(Me.cf.RootStorage)
            End If

            For Each RootStorage As CFStorage In RootStorages
                Dim tmpList = ProcessRootStorage(RootStorage)
                For Each FullName As String In tmpList
                    If Not Me.Items.Contains(FullName, StringComparer.OrdinalIgnoreCase) Then
                        Me.Items.Add(FullName)
                    End If
                Next
            Next


        End Sub

        Private Function ProcessRootStorage(RootStorage As CFStorage) As List(Of String)
            Dim FullNames As New List(Of String)
            Dim AllStorages As New List(Of CFStorage)
            Dim JSiteStorages As New List(Of CFStorage)

            ' For assemblies, get all filenames in the Attachments stream.
            ' It is needed because some JSite* OLE streams have filenames not in the assembly.
            ' Ideally we could parse the Attachments stream directly, but its format is messy.

            Dim AttachmentNamesList As New List(Of String)
            If IO.Path.GetExtension(Me.ContainingFileFullName).ToLower = ".asm" Then
                Dim AllStreams As New List(Of CFStream)
                Dim Attachments As CFStream = Nothing
                RootStorage.VisitEntries(Sub(item) If item.IsStream Then AllStreams.Add(CType(item, CFStream)), recursive:=False)
                For Each AllStream As CFStream In AllStreams
                    If AllStream.Name = "Attachments" Then
                        Attachments = AllStream
                    End If
                Next
                Dim AttachmentsDict As Dictionary(Of String, String) = ExtractFilenamesFromByteArray(Attachments.GetData, IsAttachmentsStream:=True)
                For Each Key As String In AttachmentsDict.Keys
                    AttachmentNamesList.Add(Key)
                Next

            End If

            RootStorage.VisitEntries(Sub(item) If item.IsStorage Then AllStorages.Add(CType(item, CFStorage)), recursive:=False)

            For Each AllStorage As CFStorage In AllStorages
                If AllStorage.Name Like "JSite*" Then
                    JSiteStorages.Add(AllStorage)
                End If
            Next

            For Each JSiteStorage As CFStorage In JSiteStorages
                Dim JSiteStreams As New List(Of CFStream)
                Dim JSiteStreamNames As New List(Of String)

                JSiteStorage.VisitEntries(Sub(item) If item.IsStream Then JSiteStreams.Add(CType(item, CFStream)), recursive:=False)

                For Each JSiteStream As CFStream In JSiteStreams
                    JSiteStreamNames.Add(JSiteStream.Name)
                Next

                Dim OLEName = String.Format("{0}Ole", ChrW(1))

                If (JSiteStreamNames.Contains(OLEName)) And (JSiteStreamNames.Contains("JProperties")) Then
                    If CheckJProperties(JSiteStorage.GetStream("JProperties")) Then

                        Dim FullName As String = GetFullNameFromOLEStream(JSiteStorage.GetStream(OLEName), AttachmentNamesList)

                        If FullName IsNot Nothing Then
                            If Not FullNames.Contains(FullName, StringComparer.OrdinalIgnoreCase) Then
                                FullNames.Add(FullName)
                            End If
                        End If

                    End If

                End If
            Next

            Return FullNames
        End Function

        Private Function GetFullNameFromOLEStream(
            OLEStream As CFStream,
            AttachmentNamesList As List(Of String)
            ) As String

            ' The OLE stream stores three filename formats, or none if the drawing view doesn't have a model link.

            ' ###### Needs changes to handle FOA and non-SE linked files ######

            ' EXAMPLE
            ' Index  000 001 ... 033 034 035 036 ... 145 146 147 148 149 150 ... 183 184 185 186 187 188 ... 413 414 415 416
            ' Indicator               <>                              <>          <    >                              <    >
            ' Byte    56  57      00  44  3a  5c ...  2e  70  61  72  00  ff ...  03  00  44  00  3a  00 ...  72  00  01  00
            ' Ascii    ?   ?       ?   D   :   \ ...   .   p   a   r   ?   ? ...   ?   ?   D   ?   :   ? ...   ?   ?   ?   ?  
            ' Var                    iS1                         iE1                     iS2                      iE2

            ' EXAMPLE (cont)
            ' Index   ... 434 435 436 437 438 439 440 441 442 ... 476 477 478 479 480 481
            ' Indicator    <>  <>(<- relative motion byte)                         <>
            ' Byte    ...  46  01  00  28  00  00  00  5c  44 ...  2e  70  61  72  00  ff
            ' Ascii   ...   ?   ?   ?   ?   ?   ?   ?   \   D ...   .   p   a   r   ?   ?
            ' Var                                     iS3                     iE3

            ' FIRST FILENAME: Full path filename, Ascii format, sometimes in DOS 8.3 format, Start index: 34
            ' SECOND FILENAME: Full path filename, Unicode format, normal format
            ' THIRD FILENAME: Relative path filename, Ascii format, need to convert to Unicode for possible unicode characters
            '                 Based on relative motion value, need to prepend '.\' or the correct number of '..\' to the filename

            Dim FullName As String = Nothing

            Dim DOSName As String = ""    ' FIRST FILENAME
            Dim ABSOLUTE As String = ""   ' SECOND FILENAME
            Dim RELATIVE As String = ""   ' THIRD FILENAME
            Dim CONTAINER As String = ""  ' Derived from ABSOLUTE.  Used to see if the link is in the container directory

            Dim ByteArray As Byte() = OLEStream.GetData

            Dim FilenamesDict = ExtractFilenamesFromByteArray(ByteArray, IsAttachmentsStream:=False)

            If FilenamesDict IsNot Nothing Then
                ABSOLUTE = FilenamesDict("ABSOLUTE")
                RELATIVE = FilenamesDict("RELATIVE")
                CONTAINER = FilenamesDict("CONTAINER")

                For Each Target As String In Me.LinkManagementOrder
                    Select Case Target.ToUpper
                        Case "ABSOLUTE"
                            If IO.File.Exists(ABSOLUTE) Then
                                FullName = ABSOLUTE
                                Exit For
                            End If
                        Case "RELATIVE"
                            If IO.File.Exists(RELATIVE) Then
                                FullName = RELATIVE
                                Exit For
                            End If
                        Case "CONTAINER"
                            If IO.File.Exists(CONTAINER) Then
                                FullName = CONTAINER
                                Exit For
                            End If
                    End Select
                Next

                ' If the filename is in the attachments list, add it to the BadLinks list.  Otherwise ignore.
                If (FullName Is Nothing) Then
                    Dim BadName As String = Nothing
                    For Each Target As String In Me.LinkManagementOrder
                        Select Case Target.ToUpper
                            Case "ABSOLUTE"
                                If AttachmentNamesList.Contains(ABSOLUTE) Then
                                    BadName = ABSOLUTE
                                    Exit For
                                End If
                            Case "RELATIVE"
                                If AttachmentNamesList.Contains(RELATIVE) Then
                                    BadName = RELATIVE
                                    Exit For
                                End If
                            Case "CONTAINER"
                                If AttachmentNamesList.Contains(CONTAINER) Then
                                    BadName = CONTAINER
                                    Exit For
                                End If
                        End Select
                    Next
                    If BadName IsNot Nothing Then

                        FullName = BadName 'The Items list needs this for index references to work correctly

                        If Not Me.BadLinks.Contains(BadName, StringComparer.OrdinalIgnoreCase) Then
                            BadLinks.Add(BadName)
                        End If
                    End If
                End If

            End If

            Return FullName
        End Function

        Private Function ExtractFilenamesFromByteArray(ByteArray As Byte(), IsAttachmentsStream As Boolean) As Dictionary(Of String, String)
            ' Searches the OLE byte string for model file extensions (.par, .psm, .asm, .PAR, .PSM, .ASM)
            ' Returns a dictionary
            ' {
            ' "ABSOLUTE": Full path filename,
            ' "RELATIVE": Full path filename,
            ' "CONTAINER": Full path filename
            ' }
            ' Assumes the order is DOSNAME, ABSOLUTE, RELATIVE.  Ignores any matches after those.
            ' 
            ' The search starts at the end of the byte array.  If an extension is detected,
            ' it proceeds towards the start until a filename start indicator is reached.
            ' For Ascii strings the indicator is &H00.  For Unicode it is &H03 &H00
            '
            ' The IsAttachmentsStream flag simply populates the dictionary keys with
            ' all filenames (and other garbage) found in the Root/Attachments stream.

            Dim FilenamesDict As New Dictionary(Of String, String)
            Dim FilenamesList As New List(Of String)
            Dim Filename As String
            Dim StartIdxs As New List(Of Integer)
            Dim CurrentIdx As Integer

            Dim AsciiPatterns As New List(Of List(Of Byte))

            AsciiPatterns.Add(New List(Of Byte) From {&H2E, &H70, &H61, &H72}) '.par
            AsciiPatterns.Add(New List(Of Byte) From {&H2E, &H50, &H41, &H52}) '.PAR
            AsciiPatterns.Add(New List(Of Byte) From {&H2E, &H70, &H73, &H6D}) '.psm
            AsciiPatterns.Add(New List(Of Byte) From {&H2E, &H50, &H53, &H4D}) '.PSM
            AsciiPatterns.Add(New List(Of Byte) From {&H2E, &H61, &H73, &H6D}) '.asm
            AsciiPatterns.Add(New List(Of Byte) From {&H2E, &H41, &H53, &H4D}) '.ASM

            Dim AsciiStartIndicators As New List(Of Byte) From {&H0}
            Dim AsciiMatch As Boolean
            Dim AsciiEndIdx As Integer
            Dim AsciiStartIdx As Integer

            Dim UnicodePatterns As New List(Of List(Of Byte))

            For Each Pattern As List(Of Byte) In AsciiPatterns
                Dim L As New List(Of Byte)
                For Each B As Byte In Pattern
                    L.Add(B)
                    L.Add(&H0)
                Next
                UnicodePatterns.Add(L)
            Next

            Dim UnicodeStartIndicators As New List(Of Byte) From {&H3, &H0}
            Dim UnicodeStartIndicatorsAlt As New List(Of Byte) From {&H0, &H0}
            Dim UnicodeMatch As Boolean
            Dim UnicodeEndIdx As Integer
            Dim UnicodeStartIdx As Integer

            Dim RelativeMotionIdx As Integer
            Dim RelativeMotion As Integer

            CurrentIdx = ByteArray.Count - 1
            AsciiMatch = False
            UnicodeMatch = False

            While CurrentIdx > 30

                If (Not AsciiMatch) And (Not UnicodeMatch) Then

                    ' Checks to see if any of the patterns are present at the current idx

                    For Each Pattern In AsciiPatterns
                        AsciiMatch = True
                        For i = 0 To Pattern.Count - 1
                            If Not Pattern(Pattern.Count - 1 - i) = ByteArray(CurrentIdx - i) Then
                                AsciiMatch = False
                                Exit For
                            End If
                        Next

                        If AsciiMatch Then
                            AsciiEndIdx = CurrentIdx
                            Exit For
                        End If
                    Next

                    For Each Pattern In UnicodePatterns
                        UnicodeMatch = True
                        For i = 0 To Pattern.Count - 1
                            If Not Pattern(Pattern.Count - 1 - i) = ByteArray(CurrentIdx - i) Then
                                UnicodeMatch = False
                                Exit For
                            End If
                        Next

                        If UnicodeMatch Then
                            UnicodeEndIdx = CurrentIdx
                            Exit For
                        End If
                    Next

                End If

                If AsciiMatch Then

                    ' Checks to see if the stop indicator is present just ahead of the current index

                    Dim StartIndicatorMatch As Boolean = True
                    For i = 0 To AsciiStartIndicators.Count - 1
                        If Not AsciiStartIndicators(AsciiStartIndicators.Count - 1 - i) = ByteArray(CurrentIdx - 1 - i) Then
                            StartIndicatorMatch = False
                            Exit For
                        End If
                    Next

                    If StartIndicatorMatch Then
                        AsciiStartIdx = CurrentIdx
                        StartIdxs.Add(CurrentIdx)
                        AsciiMatch = False
                        Dim ByteList As New List(Of Byte)

                        For j = AsciiStartIdx To AsciiEndIdx
                            ByteList.Add(ByteArray(j))
                        Next

                        'Filename = System.Text.Encoding.ASCII.GetString(ByteList.ToArray)
                        Filename = Encoding.Default.GetString(ByteList.ToArray)

                        ' Check for RELATIVE filename
                        If ByteArray(AsciiStartIdx - 7) = &H46 Then
                            RelativeMotionIdx = AsciiStartIdx - 6
                            RelativeMotion = CInt(ByteArray(RelativeMotionIdx))
                            Filename = ProcessRelativeFilename(Filename, RelativeMotion)
                        End If

                        If Filename IsNot Nothing Then
                            FilenamesList.Add(Filename)
                        End If

                    End If
                End If

                If UnicodeMatch Then

                    ' Checks to see if either stop indicator is present just ahead of the current index

                    Dim StartIndicatorMatch As Boolean = True
                    Dim StartIndicatorMatchAlt As Boolean = True

                    For i = 0 To UnicodeStartIndicators.Count - 1
                        If Not UnicodeStartIndicators(UnicodeStartIndicators.Count - 1 - i) = ByteArray(CurrentIdx - 1 - i) Then
                            StartIndicatorMatch = False
                            Exit For
                        End If
                    Next
                    For i = 0 To UnicodeStartIndicatorsAlt.Count - 1
                        If Not UnicodeStartIndicatorsAlt(UnicodeStartIndicatorsAlt.Count - 1 - i) = ByteArray(CurrentIdx - 1 - i) Then
                            StartIndicatorMatchAlt = False
                            Exit For
                        End If
                    Next

                    If StartIndicatorMatch Or StartIndicatorMatchAlt Then
                        UnicodeStartIdx = CurrentIdx
                        StartIdxs.Add(CurrentIdx)
                        UnicodeMatch = False
                        Dim ByteList As New List(Of Byte)
                        For j = UnicodeStartIdx To UnicodeEndIdx
                            ByteList.Add(ByteArray(j))
                        Next

                        Filename = System.Text.Encoding.Unicode.GetString(ByteList.ToArray)

                        ' Check for RELATIVE filename
                        If ByteArray(UnicodeStartIdx - 7) = &H46 Then
                            RelativeMotionIdx = UnicodeStartIdx - 6
                            RelativeMotion = CInt(ByteArray(RelativeMotionIdx))
                            Filename = ProcessRelativeFilename(Filename, RelativeMotion)
                        End If

                        If Filename IsNot Nothing Then
                            FilenamesList.Add(Filename)
                        End If

                    End If
                End If

                CurrentIdx -= 1

            End While

            If IsAttachmentsStream Then
                For Each Filename In FilenamesList
                    FilenamesDict(Filename) = Filename
                Next
                Return FilenamesDict
            End If

            If Not FilenamesList.Count >= 3 Then
                Return Nothing
            End If

            Dim ContainerFileDirectory = IO.Path.GetDirectoryName(Me.ContainingFileFullName)

            Dim ABSOLUTE = FilenamesList(FilenamesList.Count - 2)

            FilenamesDict("ABSOLUTE") = ABSOLUTE

            Dim CONTAINER = String.Format(".\{0}", IO.Path.GetFileName(ABSOLUTE))
            CONTAINER = Path.GetFullPath(Path.Combine(ContainerFileDirectory, CONTAINER))

            FilenamesDict("CONTAINER") = CONTAINER

            Dim RELATIVE = FilenamesList(FilenamesList.Count - 3)

            FilenamesDict("RELATIVE") = RELATIVE

            Return FilenamesDict

        End Function

        Private Function ProcessRelativeFilename(
            Filename As String,
            RelativeMotion As Integer
            ) As String

            If RelativeMotion = 0 Then
                ' Seems to indicate a full-path filename.  Return unmodified filename.
            Else
                'Strip off leading "\" if present
                If Filename(0) = "\" Then
                    Filename = Filename.Substring(1, Filename.Count - 1)
                End If

                'Create relative motion prefix eg. "..\..\"
                Dim Prefix As String = ""

                If RelativeMotion = 1 Then
                    Prefix = ".\"
                Else
                    'For i = 0 To RelativeMotion - 1
                    '    Prefix = String.Format("{0}..\", Prefix)
                    'Next
                    For j = 0 To RelativeMotion - 2
                        Prefix = String.Format("{0}..\", Prefix)
                    Next
                End If

                Filename = String.Format("{0}{1}", Prefix, Filename)

                'Create full path from relative path
                Dim ContainerFileDirectory = IO.Path.GetDirectoryName(Me.ContainingFileFullName)

                Dim OLD = Filename
                Try
                    Filename = Path.GetFullPath(Path.Combine(ContainerFileDirectory, Filename))
                Catch ex As Exception
                    Filename = Nothing
                End Try

            End If


            Return Filename
        End Function

        Public Function FindIndicatorIdx(
        StartIdx As Integer,
        IndicatorArray As Byte(),
        ByteArray As Byte()
        ) As Integer

            ' Start index: iS2 = FindIndicatorIdx(StartIdx:= iE1 + 1, IndicatorList:= {"3", "0"}, ByteStringList:= ByteStringList) + 2

            Dim DefaultIdx = -1000000
            Dim IndicatorIdx As Integer = DefaultIdx
            Dim CurrentIdx As Integer = StartIdx
            Dim GotAMatch As Boolean = False

            While IndicatorIdx = DefaultIdx
                If CurrentIdx + IndicatorArray.Count > ByteArray.Count Then
                    Exit While
                End If
                For i = 0 To IndicatorArray.Count - 1
                    GotAMatch = ByteArray(CurrentIdx + i) = IndicatorArray(i)
                    If Not GotAMatch Then
                        Exit For
                    End If
                Next
                If GotAMatch Then
                    IndicatorIdx = CurrentIdx
                    Exit While
                End If
                CurrentIdx += 1
            End While

            Return IndicatorIdx
        End Function

        Private Function CheckJProperties(JPropertiesStream As CFStream) As Boolean

            ' Checks if the JProperties stream contains the following byte array
            ' 4F 4C 45 53 40 00 01 00

            ' The JProperties stream for Non-SE links appears to have different values

            Dim ValidStream As Boolean = True

            Dim ByteArray As Byte() = JPropertiesStream.GetData()

            Dim ValidArray As Byte() = {&H4F, &H4C, &H45, &H53, &H40, &H0, &H1, &H0}

            If Not ByteArray.Count = ValidArray.Count Then
                ValidStream = False
            End If

            If ValidStream Then
                ' Note the last ValidArray byte is not checked.  It appears to be some sort of file counter
                For i = 0 To ValidArray.Count - 2
                    If Not ByteArray(i) = ValidArray(i) Then
                        ValidStream = False
                        Exit For
                    End If
                Next
            End If

            Return ValidStream
        End Function

        Private Function FormatByteString(ByteArray As Byte(), AsChr As Boolean) As String

            'Public Static string ByteArrayToString(Byte[] ba)
            '{
            '  StringBuilder Hex() = New StringBuilder(ba.Length * 2);
            '  foreach(Byte b In ba)
            '            Hex.AppendFormat("{0:x2}", b);
            '  Return Hex.ToString();
            '}

            Dim s As String = ""
            Dim _s As String = ""

            For Each B As Byte In ByteArray
                If Not AsChr Then
                    s = String.Format("{0},{1:x2}", s, B)
                Else
                    If CInt(B) < 32 Then
                        _s = "."
                    ElseIf CInt(B) > 127 Then
                        _s = "?"
                    ElseIf CInt(B) = 44 Then
                        _s = ";"
                    Else
                        _s = System.Text.Encoding.ASCII.GetString({B})
                    End If
                    s = String.Format("{0},{1}", s, _s)
                End If
            Next

            Return s

        End Function

    End Class


    Private Class MaterialTable
        Public Property Materials As List(Of Material)
        Public Property Gages As List(Of Gage)

        ' This is for material table files *.mat only

        Public Sub New(cf As CompoundFile)
            Dim ByteArray As Byte()
            Dim AllStreams As New List(Of CFStream)
            Dim MaterialStream As CFStream = Nothing
            Dim RawMaterialTable As String

            Materials = New List(Of Material)
            Gages = New List(Of Gage)

            cf.RootStorage.VisitEntries(Sub(item) If item.IsStream Then AllStreams.Add(CType(item, CFStream)), recursive:=False)

            For Each AllStream As CFStream In AllStreams
                If AllStream.Name = "MaterialDataEx" Then
                    MaterialStream = AllStream
                End If
            Next

            ByteArray = MaterialStream.GetData

            RawMaterialTable = System.Text.Encoding.Unicode.GetString(ByteArray)
            RawMaterialTable = RawMaterialTable.Substring(2, Len(RawMaterialTable) - 2)

            Dim XmlDoc As New XmlDocument
            XmlDoc.LoadXml(RawMaterialTable)

            TraverseNodes(XmlDoc)  'Populates Me.Materials and Me.Gages

        End Sub

        Public Function UpdateMaterial(SSDoc As HCStructuredStorageDoc) As Boolean
            Dim IsUpToDate As Boolean
            Dim Matl As String = Nothing
            Dim MatTableMatl As Material = Nothing
            Dim Proceed As Boolean = True
            Dim tf As Boolean

            If SSDoc.PropSets Is Nothing Then
                Throw New Exception(String.Format("Properties not initialized in '{0}'", IO.Path.GetFileName(SSDoc.FullName)))
            End If

            If MaterialUpToDate(SSDoc) Then
                Proceed = False
                IsUpToDate = True
            End If

            If Proceed Then
                Matl = CStr(SSDoc.GetPropValue("System", "Material"))

                MatTableMatl = GetMaterial(Matl)

                If MatTableMatl Is Nothing Then
                    Proceed = False
                    IsUpToDate = False
                End If
            End If

            If Proceed Then
                Try
                    tf = SSDoc.SetPropValue("System", "Material", Matl, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Face Style", MatTableMatl.FaceStyle, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Fill Style", MatTableMatl.FillStyle, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Virtual Style", MatTableMatl.VirtualStyle, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Coef. of Thermal Exp", MatTableMatl.CoefOfThermalExp, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Thermal Conductivity", MatTableMatl.ThermalConductivity, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Specific Heat", MatTableMatl.SpecificHeat, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Modulus of Elasticity", MatTableMatl.ModulusOfElasticity, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Poisson's Ratio", MatTableMatl.PoissonsRatio, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Yield Stress", MatTableMatl.YieldStress, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Ultimate Stress", MatTableMatl.UltimateStress, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Elongation", MatTableMatl.Elongation, AddProperty:=False)

                    IsUpToDate = tf
                Catch ex As Exception
                    IsUpToDate = False
                End Try

            End If

            Return IsUpToDate
        End Function

        Public Function MaterialUpToDate(SSDoc As HCStructuredStorageDoc) As Boolean
            Dim IsUpToDate As Boolean
            Dim Matl As String
            Dim MatTableMatl As Material
            Dim tf As Boolean
            Dim Threshold As Double = 0.001

            If SSDoc.PropSets Is Nothing Then
                Throw New Exception(String.Format("Properties not initialized in '{0}'", IO.Path.GetFileName(SSDoc.FullName)))
            End If

            Matl = CStr(SSDoc.GetPropValue("System", "Material"))

            MatTableMatl = GetMaterial(Matl)

            If MatTableMatl Is Nothing Then
                IsUpToDate = False
            Else
                tf = MatTableMatl.FaceStyle = CStr(SSDoc.GetPropValue("System", "Face Style"))
                tf = tf And MatTableMatl.FillStyle = CStr(SSDoc.GetPropValue("System", "Fill Style"))
                'tf = tf And MatTableMatl.VirtualStyle = CStr(SSDoc.GetPropValue("System", "Virtual Style"))
                tf = tf And CloseEnough(MatTableMatl.CoefOfThermalExp, CDbl(SSDoc.GetPropValue("System", "Coef. of Thermal Exp")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.ThermalConductivity, CDbl(SSDoc.GetPropValue("System", "Thermal Conductivity")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.SpecificHeat, CDbl(SSDoc.GetPropValue("System", "Specific Heat")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.ModulusOfElasticity, CDbl(SSDoc.GetPropValue("System", "Modulus of Elasticity")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.PoissonsRatio, CDbl(SSDoc.GetPropValue("System", "Poisson's Ratio")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.YieldStress, CDbl(SSDoc.GetPropValue("System", "Yield Stress")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.UltimateStress, CDbl(SSDoc.GetPropValue("System", "Ultimate Stress")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.Elongation, CDbl(SSDoc.GetPropValue("System", "Elongation")), Threshold)

                IsUpToDate = tf
            End If

            Return IsUpToDate
        End Function

        Public Function GetMaterial(Name As String) As Material
            Dim Matl As Material = Nothing

            For Each Item As Material In Me.Materials
                If Item.Name.ToLower = Name.ToLower Then
                    Matl = Item
                    Exit For
                End If
            Next

            Return Matl
        End Function

        Public Function GetGage(Name As String) As Gage
            Dim Gage As Gage = Nothing

            For Each Item As Gage In Me.Gages
                If Item.Name.ToLower = Name.ToLower Then
                    Gage = Item
                    Exit For
                End If
            Next

            Return Gage
        End Function


        Private Sub TraverseNodes(RootNode As Xml.XmlNode)
            For Each ChildNode As XmlElement In RootNode
                If ChildNode.Name.ToLower = "material" Then
                    Dim Matl As New Material
                    If ChildNode.HasAttributes Then
                        Matl.Name = ChildNode.Attributes(0).Value
                    End If
                    For Each PropertyNode As XmlElement In ChildNode.ChildNodes
                        If PropertyNode.HasAttributes Then
                            Dim Name As String = ""
                            Dim Value As String = ""
                            For Each Attribute As XmlAttribute In PropertyNode.Attributes
                                If Attribute.Name = "Name" Then
                                    Name = Attribute.Value
                                ElseIf Attribute.Name = "Value" Then
                                    Value = Attribute.Value
                                End If
                            Next
                            Select Case Name
                                Case "Face Style"
                                    Matl.FaceStyle = Value
                                Case "Fill Style"
                                    Matl.FillStyle = Value
                                Case "VSPlus Style"
                                    Matl.VirtualStyle = Value
                                Case "Coef. of Thermal Exp"
                                    Matl.CoefOfThermalExp = CDbl(Value)
                                Case "Thermal Conductivity"
                                    Matl.ThermalConductivity = CDbl(Value)
                                Case "Specific Heat"
                                    Matl.SpecificHeat = CDbl(Value)
                                Case "Modulus of Elasticity"
                                    Matl.ModulusOfElasticity = CDbl(Value)
                                Case "Poisson's Ratio"
                                    Matl.PoissonsRatio = CDbl(Value)
                                Case "Yield Stress"
                                    Matl.YieldStress = CDbl(Value)
                                Case "Ultimate Stress"
                                    Matl.UltimateStress = CDbl(Value)
                                Case "Elongation"
                                    Matl.Elongation = CDbl(Value)
                            End Select

                        End If
                    Next
                    Me.Materials.Add(Matl)

                ElseIf ChildNode.Name.ToLower = "psmgauge" Then
                    Dim Gage As New Gage
                    If ChildNode.HasAttributes Then
                        Gage.Name = ChildNode.Attributes(0).Value
                    End If
                    For Each PropertyNode As XmlElement In ChildNode.ChildNodes
                        If PropertyNode.HasAttributes Then
                            Dim Name As String = ""
                            Dim Value As String = ""
                            For Each Attribute As XmlAttribute In PropertyNode.Attributes
                                If Attribute.Name = "Name" Then
                                    Name = Attribute.Value
                                ElseIf Attribute.Name = "Value" Then
                                    Value = Attribute.Value
                                End If
                            Next
                            Select Case Name
                                Case "SMetal Thickness"
                                    Gage.Thickness = CDbl(Value)
                                Case "SMetal Bend Radius"
                                    Gage.BendRadius = CDbl(Value)
                                Case "SMetal Relief Width"
                                    Gage.ReliefWidth = CDbl(Value)
                                Case "SMetal Relief Length"
                                    Gage.ReliefLength = CDbl(Value)
                                Case "SMetal Neutral Factor"
                                    Gage.NeutralFactor = CDbl(Value)
                                Case "SMetal Bend Param Type"
                                    Gage.BendParamType = CDbl(Value)
                                Case "SMetal Bend Equation"
                                    Gage.BendEquation = Value
                            End Select

                        End If
                    Next
                    Me.Gages.Add(Gage)
                Else
                    If ChildNode.HasChildNodes Then
                        TraverseNodes(ChildNode)
                    End If
                End If
            Next
        End Sub

        Private Function CloseEnough(X As Double, Y As Double, Threshold As Double) As Boolean
            Dim Epsilon As Double = 0.000000001
            Dim Value As Double = Math.Abs((X - Y) / (X + Epsilon))
            Return (Value = 0) Or (1 - Threshold < Value)
        End Function


    End Class

    Private Class Material
        Public Property Name As String
        Public Property FaceStyle As String
        Public Property FillStyle As String
        Public Property VirtualStyle As String
        Public Property CoefOfThermalExp As Double
        Public Property ThermalConductivity As Double
        Public Property SpecificHeat As Double
        Public Property ModulusOfElasticity As Double
        Public Property PoissonsRatio As Double
        Public Property YieldStress As Double
        Public Property UltimateStress As Double
        Public Property Elongation As Double

    End Class

    Private Class Gage
        Public Property Name As String
        Public Property Thickness As Double
        Public Property BendRadius As Double
        Public Property ReliefWidth As Double
        Public Property ReliefLength As Double
        Public Property NeutralFactor As Double
        Public Property BendParamType As Double
        Public Property BendEquation As String

    End Class

End Class

