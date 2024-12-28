Option Strict On

Imports System.IO
Imports OpenMcdf
Imports OpenMcdf.Extensions
Imports OpenMcdf.Extensions.OLEProperties

Public Class HelperStructuredStorageDocument

    Public Property FullName As String

    Private Property PropSets As PropertySets
    Private Property fs As FileStream
    Private Property cf As CompoundFile
    Private Property LinkNames As LinkFullNames
    Private Property LinkManagementOrder As List(Of String)

    Public Sub New(
        _FullName As String,
        NeedProperties As Boolean,
        NeedLinks As Boolean,
        _LinkManagementOrder As List(Of String))

        Me.FullName = _FullName
        Me.LinkManagementOrder = _LinkManagementOrder

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

        If NeedProperties Then
            Me.PropSets = New PropertySets(cf)
        End If

        If NeedLinks Then
            If Me.LinkManagementOrder Is Nothing Then
                Throw New Exception("NeedLinks option requires LinkManagementOrder list")
            Else
                Me.LinkNames = New LinkFullNames(cf, IsFOA(Me.cf), Me.LinkManagementOrder, Me.FullName)
            End If
        End If

    End Sub

    Public Function IsFOA(cf As CompoundFile) As Boolean
        If cf.RootStorage.ContainsStorage("Master") Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function IsFOP(cf As CompoundFile) As Boolean
        If cf.RootStorage.ContainsStorage("FamilyOfParts") Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Sub Save()
        For Each PropSet In Me.PropSets.Items
            ' The Custom stream gets saved with DocumentSummaryInformation
            If Not PropSet.Name.ToLower = "custom" Then
                PropSet.Save()
            End If
        Next

        Me.cf.Commit()

    End Sub

    Public Sub Close()
        Me.cf.Close()
        Me.cf = Nothing

        Me.fs.Close()
        Me.fs = Nothing
    End Sub

    Public Function GetPropValue(PropSetName As String, PropNameEnglish As String) As Object
        ' Return Nothing if a value is not found

        Dim Value As Object = Nothing

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
                    Dim s = GetType(HelperStructuredStorageDocument).FullName
                    MsgBox(String.Format("In {0}: Property type {1} not recognized", s, VTType.ToString))
            End Select
        End If

        Return TypeName
    End Function

    Public Function SetPropValue(PropSetName As String, PropNameEnglish As String, Value As Object, AddProperty As Boolean) As Boolean
        ' Returns False if unsuccessful

        Dim Success As Boolean = False

        If Me.PropSets Is Nothing Then
            Return False
        End If

        'Dim PropSets As PropertySets = Nothing
        'Dim PropSet As PropertySet = Nothing

        PropNameEnglish = PropNameEnglish.ToLower

        If ProcessSpecialProperty(PropSetName, PropNameEnglish) Is Nothing Then
            'Dim Prop = GetProp(PropSetName, PropNameEnglish, PropSets, PropSet)
            Dim Prop = GetProp(PropSetName, PropNameEnglish)

            If Prop IsNot Nothing Then
                Prop.Value = Value

                If Prop.Value.ToString = Value.ToString Then
                    'PropSet.Save()
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
            Return False
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
            Return False
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
            Prop = PropSet.GetItem(PropNameEnglish)

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
        Dim s As String
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

    Public Function GetLinkNames() As List(Of String)
        If Me.LinkNames IsNot Nothing Then
            Return Me.LinkNames.Items
        Else
            Return Nothing
        End If
    End Function

    Public Function GetBadLinkNames() As List(Of String)
        If Me.LinkNames IsNot Nothing Then
            Return Me.LinkNames.BadLinks
        Else
            Return Nothing
        End If
    End Function



    Private Class PropertySets
        Public Property Items As List(Of PropertySet)

        Private Property cf As CompoundFile
        Private Property PropertySetNames As List(Of String)

        Public Sub New(_cf As CompoundFile)
            Me.cf = _cf

            Me.Items = New List(Of PropertySet)

            Me.PropertySetNames = New List(Of String)

            Me.PropertySetNames.AddRange({"SummaryInformation", "DocumentSummaryInformation", "ExtendedSummaryInformation"})
            Me.PropertySetNames.AddRange({"ProjectInformation", "MechanicalModeling", "Custom"})

            Dim cs As CFStream = Nothing
            Dim co As OLEPropertiesContainer = Nothing

            Dim PropertySetNamesInFile As New List(Of String)

            For Each PropertySetName As String In Me.PropertySetNames
                If Not PropertySetName = "Custom" Then

                    'Some files don't have every possible PropertySet
                    Try
                        Me.Items.Add(New PropertySet(Me.cf, PropertySetName))
                        PropertySetNamesInFile.Add(PropertySetName)
                    Catch ex As Exception
                        'Not an error, but need to update PropertySetNames after the loop.
                    End Try

                    If PropertySetName = "DocumentSummaryInformation" Then
                        cs = GetItem("DocumentSummaryInformation").cs
                        co = GetItem("DocumentSummaryInformation").co
                    End If
                Else
                    ' The Custom PropertySet needs the same cs and co as DocumentSummaryInformation, not copies.
                    Me.Items.Add(New PropertySet(Me.cf, PropertySetName, cs, co))
                    PropertySetNamesInFile.Add(PropertySetName)
                End If

            Next

            PropertySetNames = PropertySetNamesInFile

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
                        CorrectedName = FixOLEPropName(OLEProp.PropertyName, PropertySetName)
                        Me.PropNames.Add(CorrectedName)
                        Me.Items.Add(New Prop(OLEProp, CorrectedName))
                    Next
                End If

            Else
                For Each OLEProp As OLEProperty In co.Properties
                    CorrectedName = FixOLEPropName(OLEProp.PropertyName, PropertySetName)
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
                    ' Accounting for a hidden property in the Container or some other weirdness.
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

        Private Function FixOLEPropName(OLEPropName As String, PropertySetName As String) As String
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
                        OLEProp.Value = CType(PropertyValue, Boolean)
                    Catch ex As Exception
                        Success = False
                    End Try

                Case = VTPropertyType.VT_I4
                    RequiredType = "Integer"
                    Try
                        OLEProp.Value = CType(PropertyValue, Integer)
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
                        OLEProp.Value = CType(PropertyValue, Double)
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

                        Dim FullName As String = GetFullNameFromOLEStream(JSiteStorage.GetStream(String.Format("{0}Ole", ChrW(1))))

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

        Private Function GetFullNameFromOLEStream(OLEStream As CFStream) As String

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

            Dim FilenamesDict = ExtractFilenamesFromByteArray(ByteArray)

            If FilenamesDict IsNot Nothing Then
                ABSOLUTE = FilenamesDict("ABSOLUTE")
                RELATIVE = FilenamesDict("RELATIVE")
                CONTAINER = FilenamesDict("CONTAINER")

                '' ###### OUTPUT FOR DEBUGGING ######
                'Dim Outfile As String = ".\ole_links.csv"

                'Dim Valids = {&H3A, &H5C} ' ":", "\"
                'If ByteArray.Count > 56 Then
                '    If Not Valids.Contains(ByteArray(35)) Then
                '        If Not Valids.Contains(ByteArray(55)) Then
                '            Try
                '                Using writer As New IO.StreamWriter(Outfile, True)
                '                    writer.WriteLine(Me.ContainingFileFullName)
                '                    writer.WriteLine(FormatByteString(ByteArray, False))
                '                    writer.WriteLine(FormatByteString(ByteArray, True))
                '                    writer.WriteLine("")
                '                End Using
                '            Catch ex As Exception
                '            End Try

                '        End If

                '    End If
                'End If

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

                If (FullName Is Nothing) And (Not ABSOLUTE = "") Then
                    If Not Me.BadLinks.Contains(ABSOLUTE, StringComparer.OrdinalIgnoreCase) Then
                        BadLinks.Add(ABSOLUTE)
                    End If
                End If

            End If

            Return FullName
        End Function

        Private Function ExtractFilenamesFromByteArray(ByteArray As Byte()) As Dictionary(Of String, String)
            ' Searches the OLE byte string for model file extensions (.par, .psm, .asm, .PAR, .PSM, .ASM)
            ' Returns a dictionary
            ' {
            ' "ABSOLUTE": Full path filename,
            ' "RELATIVE": Full path filename,
            ' "CONTAINER": .\filename
            ' }
            ' Assumes the order is DOSNAME, ABSOLUTE, RELATIVE.  Ignores any matches after those.
            ' 
            ' The search starts at the end of the byte array.  If an extension is detected,
            ' it proceeds towards the start until a filename start indicator is reached.
            ' For Ascii strings the indicator is &H00.  For Unicode it is &H03 &H00

            Dim FilenamesDict As New Dictionary(Of String, String)
            Dim FilenamesList As New List(Of String)
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
                        FilenamesList.Add(System.Text.Encoding.ASCII.GetString(ByteList.ToArray))
                        'For j = AsciiStartIdx To AsciiEndIdx
                        '    ByteList.Add(ByteArray(j))
                        '    ByteList.Add(&H0)
                        'Next
                        'FilenamesList.Add(System.Text.Encoding.Unicode.GetString(ByteList.ToArray))
                    End If
                End If

                If UnicodeMatch Then

                    ' Checks to see if the stop indicator is present just ahead of the current index

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
                        FilenamesList.Add(System.Text.Encoding.Unicode.GetString(ByteList.ToArray))
                    End If
                End If

                CurrentIdx -= 1

            End While

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

            'Strip off leading "\" if present
            If RELATIVE(0) = "\" Then
                RELATIVE = RELATIVE.Substring(1, RELATIVE.Count - 1)
            End If

            'Create relative motion prefix eg. "..\..\"
            Dim Prefix As String = ""

            Dim RelativeMotionIdx As Integer = StartIdxs(StartIdxs.Count - 3) - 6
            Dim RelativeMotion As Integer = CInt(ByteArray(RelativeMotionIdx))

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

            RELATIVE = String.Format("{0}{1}", Prefix, RELATIVE)

            'Create full path from relative path
            'Dim OLD = RELATIVE
            RELATIVE = Path.GetFullPath(Path.Combine(ContainerFileDirectory, RELATIVE))
            'Try
            'Catch ex As Exception
            '    Dim i = 0
            'End Try

            FilenamesDict("RELATIVE") = RELATIVE

            Return FilenamesDict

        End Function

        Private Function ProcessRelativeFilename(tmpByteList As List(Of Byte), RelativeMotion As Integer) As String
            Dim RELATIVE As String = ""

            'Convert ASCII to Unicode
            Dim tmptmpByteList As New List(Of Byte)
            For i = 0 To tmpByteList.Count - 1
                tmptmpByteList.Add(tmpByteList(i))
                tmptmpByteList.Add(&H0)
            Next

            RELATIVE = System.Text.Encoding.Unicode.GetString(tmptmpByteList.ToArray)

            'Strip off leading "\" if present
            If RELATIVE(0) = "\" Then
                RELATIVE = RELATIVE.Substring(1, RELATIVE.Count - 1)
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

            RELATIVE = String.Format("{0}{1}", Prefix, RELATIVE)  ' Does this preserve unicode format?

            'Create full path from relative path
            Dim ContainerFileDirectory = IO.Path.GetDirectoryName(Me.ContainingFileFullName)

            RELATIVE = Path.GetFullPath(Path.Combine(ContainerFileDirectory, RELATIVE))

            Return RELATIVE
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


End Class

