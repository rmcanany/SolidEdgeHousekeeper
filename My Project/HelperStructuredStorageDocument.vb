Option Strict On

Imports System.IO
Imports System.Security.AccessControl
Imports OpenMcdf
Imports OpenMcdf.Extensions
Imports OpenMcdf.Extensions.OLEProperties

Public Class HelperStructuredStorageDocument

    Public Property FullName As String
    Public Property PropSets As PropertySets

    Private Property fs As FileStream
    Private Property cf As CompoundFile

    Public Sub New(_FullName As String)
        Me.FullName = _FullName

        Try
            Me.fs = New FileStream(Me.FullName, FileMode.Open, FileAccess.ReadWrite)
        Catch ex As Exception
            Throw New Exception(String.Format("Unable to open '{0}'", Me.FullName))
        End Try

        Dim cfg As CFSConfiguration = CFSConfiguration.SectorRecycle Or CFSConfiguration.EraseFreeSectors
        Me.cf = New CompoundFile(fs, CFSUpdateMode.Update, cfg)

        If IsFOA(cf) Then
            Throw New Exception(String.Format("Unable to process FOA file '{0}'", Me.FullName))
        End If

        Me.PropSets = New PropertySets(cf)

    End Sub

    Public Function IsFOA(cf As CompoundFile) As Boolean
        If cf.RootStorage.ContainsStorage("Master") Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Sub OutputPropList()
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


    Public Function GetProp(
             PropSetName As String,
             PropNameEnglish As String,
             Optional ByRef PropSets As PropertySets = Nothing,
             Optional ByRef PropSet As PropertySet = Nothing
             ) As Prop

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

        Dim PropSets As PropertySets = Nothing
        Dim PropSet As PropertySet = Nothing

        PropNameEnglish = PropNameEnglish.ToLower

        If ProcessSpecialProperty(PropSetName, PropNameEnglish) Is Nothing Then
            Dim Prop = GetProp(PropSetName, PropNameEnglish, PropSets, PropSet)

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



    Public Class PropertySets
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

            For Each PropertySetName As String In Me.PropertySetNames
                If Not PropertySetName = "Custom" Then

                    'Some files don't have every possible PropertySet
                    Try
                        Me.Items.Add(New PropertySet(Me.cf, PropertySetName))
                    Catch ex As Exception
                        'Not an error
                    End Try

                    If PropertySetName = "DocumentSummaryInformation" Then
                        cs = GetItem("DocumentSummaryInformation").cs
                        co = GetItem("DocumentSummaryInformation").co
                    End If
                Else
                    ' The Custom PropertySet needs the same cs and co as DocumentSummaryInformation, not copies.
                    Me.Items.Add(New PropertySet(Me.cf, PropertySetName, cs, co))
                End If

            Next

            'Dim P1 = GetItem("DocumentSummaryInformation")
            'Dim P2 = GetItem("Custom")

            'If P1.cs Is P2.cs Then
            '    Dim i = 0
            'Else
            '    Dim i = 0
            'End If
            'If P1.co Is P2.co Then
            '    Dim i = 0
            'Else
            '    Dim i = 0
            'End If
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


    Public Class PropertySet
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

    Public Class Prop
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


End Class

