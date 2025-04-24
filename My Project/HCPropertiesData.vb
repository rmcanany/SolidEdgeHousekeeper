Option Strict On

Imports System.Reflection
Imports Newtonsoft.Json

Public Class HCPropertiesData

    Public Property Items As List(Of PropertyData)


    Public Sub New()

        Items = New List(Of PropertyData)

        '###### Load saved PropertiesData, if any ######

        Dim UP As New UtilsPreferences
        Dim Infile As String = UP.GetPropertiesDataFilename(CheckExisting:=True)

        If Not Infile = "" Then
            Dim JSONString As String = IO.File.ReadAllText(Infile)

            Dim tmpList As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(JSONString)

            For Each PropertyDataJSON As String In tmpList
                Dim tmpPropertyData As New PropertyData

                Try
                    tmpPropertyData.FromJSON(PropertyDataJSON)
                Catch ex As Exception
                    Items.Clear()

                    Dim s As String = String.Format("Unable to load saved Property information.{0}", vbCrLf)
                    s = String.Format("{0}Reported error: {1}{2}", s, ex.Message, vbCrLf)
                    s = String.Format("{0}Please rerun Update on the Configuration Tab -- Templates Page", s)
                    MsgBox(ex.Message)
                    Exit Sub

                End Try

                Items.Add(tmpPropertyData)
            Next

        End If
    End Sub


    Public Sub Save()

        Dim UP As New UtilsPreferences
        Dim Outfile As String = UP.GetPropertiesDataFilename(CheckExisting:=False)

        Dim JSONString As String
        Dim tmpList As New List(Of String)

        For Each Item As PropertyData In Me.Items
            tmpList.Add(Item.ToJSON)
        Next

        JSONString = JsonConvert.SerializeObject(tmpList)

        IO.File.WriteAllText(Outfile, JSONString)

    End Sub

    Public Function GetFavoritesList() As List(Of String)

        Dim FavoritesList As New List(Of String)
        Dim FavoritesArray(Me.Items.Count) As String
        Dim Idx As Integer

        For Each Item As PropertyData In Me.Items
            Idx = Item.FavoritesListIdx
            If Not Idx = -1 Then
                FavoritesArray(Idx) = Item.Name
            End If
        Next

        For i As Integer = 0 To FavoritesArray.Count - 1
            If Not FavoritesArray(i) = "" Then
                FavoritesList.Add(FavoritesArray(i))
            End If
        Next

        Return FavoritesList

    End Function

    Public Function GetAvailableList() As List(Of String)

        Dim AvailableList As New List(Of String)

        For Each Item As PropertyData In Me.Items
            AvailableList.Add(Item.Name)
        Next

        Return AvailableList
    End Function

    Public Function GetPropertyData(
        Name As String,
        Optional tmpItems As List(Of PropertyData) = Nothing
        ) As PropertyData

        Dim tmpProperty As PropertyData = Nothing

        If tmpItems Is Nothing Then
            For Each Item As PropertyData In Me.Items
                If Item.Name.ToLower = Name.ToLower Then
                    tmpProperty = Item
                    Exit For
                End If
            Next
        Else
            For Each Item As PropertyData In tmpItems
                If Item.Name.ToLower = Name.ToLower Then
                    tmpProperty = Item
                    Exit For
                End If
            Next
        End If

        Return tmpProperty
    End Function

    Public Function GetPropertyData(
        PropertySetActualName As String,
        PropID As Integer,
        Optional tmpItems As List(Of PropertyData) = Nothing
        ) As PropertyData

        Dim tmpPropertyData As PropertyData = Nothing

        If tmpItems Is Nothing Then
            For Each Item As PropertyData In Items
                If (Item.PropertySetActualName = PropertySetActualName) And (Item.PropID = PropID) Then
                    tmpPropertyData = Item
                    Exit For
                End If
            Next
        Else
            For Each Item As PropertyData In tmpItems
                If (Item.PropertySetActualName = PropertySetActualName) And (Item.PropID = PropID) Then
                    tmpPropertyData = Item
                    Exit For
                End If
            Next
        End If

        Return tmpPropertyData
    End Function

    Private Function UpdateFromTemplates(
         TemplateList As List(Of String),
         tmpItems As List(Of PropertyData),
         KnownSystemProps As List(Of String)
         ) As List(Of PropertyData)

        ' ###### PROCESS TEMPLATES FOR CUSTOM PROPERTIES AND LOCALIZED NAMES ######
        ' This needs to use SE, not SSDoc, so that localized names can be found

        Dim PropertySets As SolidEdgeFileProperties.PropertySets
        Dim PropertySet As SolidEdgeFileProperties.Properties
        Dim Prop As SolidEdgeFileProperties.Property = Nothing
        Dim PropertySetHousekeeperName As PropertyData.PropertySetNameConstants
        Dim PropName As String = ""

        Dim PropertySetActualNames As New List(Of String)
        PropertySetActualNames.AddRange({"SummaryInformation", "DocumentSummaryInformation", "ExtendedSummaryInformation"})
        PropertySetActualNames.AddRange({"ProjectInformation", "MechanicalModeling"})
        ' Do Custom last to deal with duplicates
        PropertySetActualNames.Add("Custom")

        Dim tmpPropertyData As PropertyData

        Dim tf As Boolean
        Dim PropID As Integer

        PropertySets = New SolidEdgeFileProperties.PropertySets


        ' ###### PROCESS TEMPLATES ######

        For Each TemplateName As String In TemplateList

            tf = Not TemplateName = ""
            tf = tf And FileIO.FileSystem.FileExists(TemplateName)
            If Not tf Then
                Continue For
            End If

            Dim OpenReadOnly As Boolean = True

            Try
                PropertySets.Open(TemplateName, OpenReadOnly)
            Catch ex As Exception
                MsgBox(String.Format("Could not open template '{0}'", TemplateName), vbOKOnly)
                Return Nothing
            End Try


            ' ###### PROCESS PROPERTY SETS ######

            For Each PropertySetActualName In PropertySetActualNames

                ' Not all files have all property sets.
                Try
                    PropertySet = CType(PropertySets.Item(PropertySetActualName), SolidEdgeFileProperties.Properties)
                Catch ex As Exception
                    Continue For
                End Try

                If PropertySetActualName = "Custom" Then
                    PropertySetHousekeeperName = PropertyData.PropertySetNameConstants.Custom
                Else
                    PropertySetHousekeeperName = PropertyData.PropertySetNameConstants.System
                End If


                ' ###### PROCESS PROPERTIES ######

                For i = 0 To PropertySet.Count - 1
                    Try
                        Prop = CType(PropertySet.Item(i), SolidEdgeFileProperties.Property)
                        PropName = Prop.Name
                        PropID = Prop.ID
                    Catch ex As Exception
                        Dim s = "Error building PropertiesData: "
                        s = String.Format("{0} PropertySetName '{1}', Item Number '{2}', PropName '{3}'", s, PropertySetActualName, PropID, PropName)
                        MsgBox(s, vbOKOnly)
                    End Try


                    ' ###### LOCALIZED NAME CHECK ######

                    tmpPropertyData = GetPropertyData(PropertySetActualName, PropID, tmpItems)

                    If tmpPropertyData IsNot Nothing Then
                        If Not tmpPropertyData.Name = PropName Then
                            If Not PropName = Nothing Then  ' Not all properties have names.  Happens with User Profile entries and others.
                                tmpPropertyData.Name = PropName
                            Else
                                PropName = tmpPropertyData.Name
                            End If
                        End If
                    End If


                    ' ###### DUPLICATE NAME CHECK ######

                    tmpPropertyData = GetPropertyData(PropName, tmpItems)

                    If tmpPropertyData IsNot Nothing Then
                        tf = PropertySetActualName = "Custom"
                        tf = tf And (Not tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.Custom)
                        If tf Then
                            tmpPropertyData.IsDuplicate = True
                        End If
                    End If


                    ' ###### PROPERTY NOT FOUND CHECK ######

                    If tmpPropertyData Is Nothing Then

                        tf = PropertySetActualName = "Custom"
                        tf = tf Or KnownSystemProps.Contains(PropName)  ' Keep from adding unwanted items like Creation Locale, etc.
                        If tf Then

                            tmpPropertyData = New PropertyData
                            tmpItems.Add(tmpPropertyData)

                            tmpPropertyData.Name = PropName
                            tmpPropertyData.PropertySetName = PropertySetHousekeeperName
                            tmpPropertyData.EnglishName = PropName
                            tmpPropertyData.SSName = PropName
                            tmpPropertyData.PropertySetActualName = PropertySetActualName
                            If PropertySetActualName = "Custom" Then
                                tmpPropertyData.PropID = -1
                            Else
                                tmpPropertyData.PropID = PropID
                            End If
                            tmpPropertyData.PropertySource = PropertyData.PropertySourceConstants.Auto
                            tmpPropertyData.FavoritesListIdx = -1

                            Dim PropTypeName As String = Prop.Value.GetType().Name

                            Select Case PropTypeName
                                Case "String"
                                    tmpPropertyData.TypeName = PropertyData.TypeNameConstants._String
                                Case "Int32"
                                    tmpPropertyData.TypeName = PropertyData.TypeNameConstants._Integer
                                Case "Double"
                                    tmpPropertyData.TypeName = PropertyData.TypeNameConstants._Double
                                Case "Boolean"
                                    tmpPropertyData.TypeName = PropertyData.TypeNameConstants._Boolean
                                Case "DateTime"
                                    tmpPropertyData.TypeName = PropertyData.TypeNameConstants._DateTime
                                Case Else
                                    Dim s As String = String.Format("In PropertiesDataPopulate, PropTypeName '{0}' not recognized", PropTypeName)
                                    MsgBox(s, vbOKOnly)
                            End Select
                        End If
                    End If

                Next
            Next

        Next

        Return tmpItems
    End Function

    Public Sub Populate(TemplateList As List(Of String))

        Dim tmpItems As New List(Of PropertyData)

        Dim PreviousFavoritesList As List(Of String) = GetFavoritesList()

        Dim PropertySetActualName As String

        Dim tmpPropertyData As PropertyData
        Dim tmpPreviousPropertyData As PropertyData

        Dim KnownSystemProps As New List(Of String)

        Dim PropertySetActualNames As New List(Of String)
        PropertySetActualNames.AddRange({"SummaryInformation", "DocumentSummaryInformation", "ExtendedSummaryInformation"})
        PropertySetActualNames.AddRange({"ProjectInformation", "MechanicalModeling"})
        ' Do Custom last to deal with duplicates
        PropertySetActualNames.Add("Custom")
        Dim PropID As Integer
        Dim PropName As String


        ' ###### GET KNOWN SYSTEM PROPERTIES ######

        For Each PropertySetActualName In PropertySetActualNames
            For PropID = 0 To 50  ' Current max not including Custom is 27
                tmpPropertyData = New PropertyData(PropertySetActualName, PropID)
                If tmpPropertyData.Name IsNot Nothing Then
                    tmpItems.Add(tmpPropertyData)
                    KnownSystemProps.Add(tmpPropertyData.Name)
                End If
            Next
        Next


        '' ###### PROCESS TEMPLATES FOR CUSTOM PROPERTIES AND LOCALIZED NAMES ######

        tmpItems = UpdateFromTemplates(TemplateList, tmpItems, KnownSystemProps)

        If tmpItems Is Nothing Then
            Exit Sub
        End If

        '' ###### ADD SPECIAL PROPERTIES ######

        Dim PropNames As List(Of String) = {"File Name", "File Name (full path)", "File Name (no extension)"}.ToList

        For Each PropName In PropNames

            tmpPropertyData = GetPropertyData(PropName)

            If tmpPropertyData Is Nothing Then

                tmpPropertyData = New PropertyData
                tmpItems.Add(tmpPropertyData)

                tmpPropertyData.Name = PropName
                tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.System
                tmpPropertyData.PropertySetActualName = "System"
                tmpPropertyData.PropID = -1
                tmpPropertyData.EnglishName = PropName
                tmpPropertyData.SSName = ""
                tmpPropertyData.PropertySource = PropertyData.PropertySourceConstants.Auto
                tmpPropertyData.FavoritesListIdx = -1
                tmpPropertyData.TypeName = PropertyData.TypeNameConstants._String

            Else
                tmpItems.Add(tmpPropertyData)
            End If
        Next


        ' ###### UPDATE FAVORITES ######

        For i As Integer = 0 To PreviousFavoritesList.Count - 1
            PropName = PreviousFavoritesList(i)

            ' Check if it is already in tmpItems
            tmpPropertyData = GetPropertyData(PropName, tmpItems)

            If tmpPropertyData IsNot Nothing Then
                ' If so, update FavoritesListIdx
                tmpPropertyData.FavoritesListIdx = i

            Else
                ' If not in tmpItems, but in Items, add it.
                tmpPreviousPropertyData = GetPropertyData(PropName)

                If tmpPreviousPropertyData IsNot Nothing Then
                    tmpPropertyData = New PropertyData
                    tmpItems.Add(tmpPropertyData)

                    tmpPropertyData.Name = PropName
                    tmpPropertyData.PropertySetName = tmpPreviousPropertyData.PropertySetName
                    tmpPropertyData.PropertySetActualName = tmpPreviousPropertyData.PropertySetActualName
                    tmpPropertyData.PropID = tmpPreviousPropertyData.PropID
                    tmpPropertyData.EnglishName = tmpPreviousPropertyData.EnglishName
                    tmpPropertyData.SSName = tmpPreviousPropertyData.SSName
                    tmpPropertyData.PropertySource = tmpPreviousPropertyData.PropertySource
                    tmpPropertyData.TypeName = tmpPreviousPropertyData.TypeName
                    tmpPropertyData.FavoritesListIdx = i
                    tmpPropertyData.IsDuplicate = tmpPreviousPropertyData.IsDuplicate
                End If
            End If

        Next

        Me.Items = tmpItems

    End Sub

    Public Sub UpdateFavorites(FavoritesList As List(Of String))

        Dim PropName As String
        Dim tmpPropertyData As PropertyData

        For Each Item As PropertyData In Me.Items
            Item.FavoritesListIdx = -1
        Next

        For idx As Integer = 0 To FavoritesList.Count - 1
            PropName = FavoritesList(idx)
            tmpPropertyData = GetPropertyData(PropName)
            If tmpPropertyData IsNot Nothing Then
                tmpPropertyData.FavoritesListIdx = idx
            End If
        Next

    End Sub

    Public Sub AddProp(
        PropertySetActualName As String,
        PropertyName As String,
        EnglishName As String,
        FavoritesListIdx As Integer)

        Dim tmpPropertyData As PropertyData

        Dim SystemPropertyList As New List(Of String)
        SystemPropertyList.AddRange({"SummaryInformation", "ExtendedSummaryInformation", "DocumentSummaryInformation"})
        SystemPropertyList.AddRange({"ProjectInformation", "MechanicalModeling"})

        Dim PropertySet As PropertyData.PropertySetNameConstants
        Dim OriginalPropertySet As PropertyData.PropertySetNameConstants
        Dim s As String

        If SystemPropertyList.Contains(PropertySetActualName) Then
            PropertySet = PropertyData.PropertySetNameConstants.System
        ElseIf PropertySetActualName.ToLower = "system" Then
            PropertySetActualName = ""
            PropertySet = PropertyData.PropertySetNameConstants.System
        Else
            PropertySet = PropertyData.PropertySetNameConstants.Custom
        End If

        tmpPropertyData = GetPropertyData(PropertyName)

        If tmpPropertyData Is Nothing Then

            tmpPropertyData = New PropertyData
            Me.Items.Add(tmpPropertyData)

            tmpPropertyData.Name = PropertyName
            tmpPropertyData.PropertySetName = PropertySet
            tmpPropertyData.PropertySetActualName = PropertySetActualName
            tmpPropertyData.PropID = -1
            tmpPropertyData.EnglishName = EnglishName
            tmpPropertyData.PropertySource = PropertyData.PropertySourceConstants.Manual
            tmpPropertyData.FavoritesListIdx = FavoritesListIdx
            tmpPropertyData.TypeName = PropertyData.TypeNameConstants._Unknown

        Else
            OriginalPropertySet = tmpPropertyData.PropertySetName
            If Not PropertySet = OriginalPropertySet Then
                s = String.Format("The list already contains '{0}' in the '{1}' property set. ", PropertyName, OriginalPropertySet)
                s = String.Format("{0}Do you want to add the new property set '{1}'?", s, PropertySet)
                Dim Result As MsgBoxResult = MsgBox(s, vbYesNo)
                If Result = vbYes Then
                    tmpPropertyData.IsDuplicate = True
                    tmpPropertyData.EnglishName = EnglishName
                    tmpPropertyData.PropertySource = PropertyData.PropertySourceConstants.Manual
                    tmpPropertyData.FavoritesListIdx = FavoritesListIdx
                End If
            Else
                s = String.Format("The list already contains '{0}' in the '{1}' property set. ", PropertyName, OriginalPropertySet)
                s = String.Format("{0}Adding that one instead.", s)
                MsgBox(s, vbOKOnly)
            End If
        End If

    End Sub

    Public Function GetLocalizedEnglishNames() As List(Of String)
        Dim Names As New List(Of String)
        Dim EnglishName As String

        For Each tmpPropertyData As PropertyData In Me.Items
            ' Not adding names from Custom properties
            If Not tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.Custom Then
                EnglishName = tmpPropertyData.EnglishName
                If (EnglishName IsNot Nothing) AndAlso (Not EnglishName = "") Then
                    Names.Add(EnglishName)
                End If
            End If
        Next

        Return Names
    End Function

    Private Function ScanFilesForProps(Directory As String, Method As String) As List(Of String)
        ' This a utility that can be used to open (many) files to see what properties are defined.
        ' It is not normally used in production.

        ' Searches a directory and subdirectories for property names
        ' Directory: The top-level directory to search
        ' Method: What program to use to process the files.  'SE' or 'SS'

        Dim EnglishNames As New List(Of String)

        Dim ActiveFileExtensionsList As New List(Of String)
        ActiveFileExtensionsList.AddRange({"*.asm", "*.par", "*.psm", "*.dft"})
        Dim tmpFoundFiles As New List(Of String)

        tmpFoundFiles.AddRange(FileIO.FileSystem.GetFiles(Directory, FileIO.SearchOption.SearchAllSubDirectories, ActiveFileExtensionsList.ToArray))

        If Method = "SS" Then
            Dim SSDoc As HCStructuredStorageDoc

            'Dim CheckNames As New List(Of String)
            'CheckNames.AddRange({"poission's ratio", "面样式", "file size", "number of sheets", "number of objects", "tubeminimumflatlength", "grouping"})
            'Dim CheckedNames As New List(Of String)
            'Dim CheckFiles As New List(Of String)

            For Each filename As String In tmpFoundFiles
                Try
                    SSDoc = New HCStructuredStorageDoc(filename)
                    SSDoc.ReadProperties(Form_Main.PropertiesData)
                Catch ex As Exception
                    Continue For
                End Try

                Dim tmpEnglishNames As New List(Of String)
                tmpEnglishNames = SSDoc.GetPropNames()
                For Each s As String In tmpEnglishNames
                    If Not EnglishNames.Contains(s) Then EnglishNames.Add(s)
                    'If CheckNames.Contains(s) And Not CheckedNames.Contains(s) Then
                    '    CheckedNames.Add(s)
                    '    CheckFiles.Add(filename)
                    'End If
                Next
                If SSDoc IsNot Nothing Then SSDoc.Close()
            Next

        Else
            Dim PropertySets As SolidEdgeFileProperties.PropertySets
            Dim PropertySet As SolidEdgeFileProperties.Properties
            Dim Prop As SolidEdgeFileProperties.Property
            PropertySets = New SolidEdgeFileProperties.PropertySets
            Dim PropertySetActualName As String
            Dim PropName As String = ""
            Dim KeepList As List(Of String) = "SummaryInformation DocumentSummaryInformation ExtendedSummaryInformation ProjectInformation MechanicalModeling".Split(CChar(" ")).ToList

            For Each filename As String In tmpFoundFiles
                Try
                    PropertySets.Open(filename, True)
                    Dim tmpEnglishNames As New List(Of String)
                    For Each PropertySetActualName In KeepList
                        If Not PropertySetActualName = "Custom" Then
                            ' Not all file types have all property sets.
                            Try
                                PropertySet = CType(PropertySets.Item(PropertySetActualName), SolidEdgeFileProperties.Properties)
                            Catch ex2 As Exception
                                Continue For
                            End Try
                            For i = 0 To PropertySet.Count - 1
                                Prop = CType(PropertySet.Item(i), SolidEdgeFileProperties.Property)
                                PropName = Prop.Name
                                Dim ID = Prop.ID
                                Dim s As String = String.Format("{0},{1},{2}", PropertySet.Name, ID, PropName)
                                If Not tmpEnglishNames.Contains(s) Then tmpEnglishNames.Add(s)
                            Next
                            For Each s As String In tmpEnglishNames
                                If Not EnglishNames.Contains(s) Then EnglishNames.Add(s)
                            Next
                        End If
                    Next
                    PropertySets.Close()

                Catch ex As Exception

                End Try
            Next

        End If

        Return EnglishNames
    End Function

End Class

Public Class PropertyData

    ' ###### In case of a duplicate, 'IsDuplicate' = TRUE   ######
    ' ###### and PropID will be for the 'System' property.  ######

    Public Property PropID As Integer
    Public Property PropertySetActualName As String
    Public Property PropertySetName As PropertySetNameConstants
    Public Property EnglishName As String
    Public Property Name As String
    Public Property SSName As String
    Public Property TypeName As TypeNameConstants
    Public Property PropertySource As PropertySourceConstants
    Public Property FavoritesListIdx As Integer
    Public Property IsDuplicate As Boolean = False

    Public Enum PropertySetNameConstants
        System
        Custom
        Server
    End Enum

    Public Enum TypeNameConstants
        ' Preceeding names with '_' to avoid VB reserved keywords
        _String
        _Integer
        _Double
        _Boolean
        _DateTime
        _Unknown
    End Enum

    Public Enum PropertySourceConstants
        Auto
        Manual
    End Enum


    Public Sub New()

    End Sub


    Public Sub New(_PropertySetActualName As String, _PropID As Integer)

        '###### If the new PropertyData's Name remains Nothing, it's not valid ######
        Me.Name = Nothing

        Me.PropID = _PropID
        Me.PropertySetActualName = _PropertySetActualName

        If Me.PropertySetActualName = "Custom" Then
            Me.PropertySetName = PropertySetNameConstants.Custom
        Else
            Me.PropertySetName = PropertySetNameConstants.System
        End If

        Me.PropertySource = PropertySourceConstants.Auto
        Me.FavoritesListIdx = -1
        Me.IsDuplicate = False

        Select Case _PropertySetActualName

            Case "SummaryInformation"
                Select Case _PropID
                    Case 2
                        Me.EnglishName = "Title"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName.ToUpper
                        Me.TypeName = TypeNameConstants._String
                    Case 3
                        Me.EnglishName = "Subject"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName.ToUpper
                        Me.TypeName = TypeNameConstants._String
                    Case 4
                        Me.EnglishName = "Author"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName.ToUpper
                        Me.TypeName = TypeNameConstants._String
                    Case 5
                        Me.EnglishName = "Keywords"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName.ToUpper
                        Me.TypeName = TypeNameConstants._String
                    Case 6
                        Me.EnglishName = "Comments"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName.ToUpper
                        Me.TypeName = TypeNameConstants._String
                    Case 7
                        Me.EnglishName = "Template"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName.ToUpper
                        Me.TypeName = TypeNameConstants._String
                    Case 8
                        Me.EnglishName = "Last Author"
                        Me.Name = Me.EnglishName
                        Me.SSName = "LASTAUTHOR"
                        Me.TypeName = TypeNameConstants._String
                    Case 9  'Not used by SE
                        'Me.EnglishName = "Revision Number"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "REVNUMBER"
                        'Me.TypeName = TypeNameConstants._String
                    Case 10 'Not used by SE
                        'Me.EnglishName = "Total Editing Time"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "EDITTIME"
                        'Me.TypeName = TypeNameConstants._Date
                    Case 11 'Not used by SE
                        'Me.EnglishName = "Last Print Date"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "LASTPRINTED"
                        'Me.TypeName = TypeNameConstants._Date
                    Case 12
                        Me.EnglishName = "Origination Date"
                        Me.Name = Me.EnglishName
                        Me.SSName = "CREATE_DTM"
                        Me.TypeName = TypeNameConstants._DateTime
                    Case 13
                        Me.EnglishName = "Last Save Date"
                        Me.Name = Me.EnglishName
                        Me.SSName = "LASTSAVE_DTM"
                        Me.TypeName = TypeNameConstants._DateTime
                    Case 14 'Not maintained by SE
                        'Me.EnglishName = "Number of pages"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "PAGECOUNT"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 15 'Not maintained by SE
                        'Me.EnglishName = "Number of words"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "WORDCOUNT"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 16 'Not maintained by SE
                        'Me.EnglishName = "Number of characters"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "CHARCOUNT"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 18
                        'Me.EnglishName = "Application Name"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "APPNAME"
                        'Me.TypeName = TypeNameConstants._String
                    Case 19
                        Me.EnglishName = "Security"
                        Me.Name = Me.EnglishName
                        Me.SSName = "DOC_SECURITY"
                        Me.TypeName = TypeNameConstants._Integer
                End Select

            Case "DocumentSummaryInformation"
                Select Case _PropID
                    Case 2
                        Me.EnglishName = "Category"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName.ToUpper
                        Me.TypeName = TypeNameConstants._String
                    Case 3
                        'Me.EnglishName = "Presentation Format"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "PRESFORMAT"
                        'Me.TypeName = TypeNameConstants._String
                    Case 4
                        'Me.EnglishName = "Byte Count"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "BYTECOUNT"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 5
                        'Me.EnglishName = "Lines"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "LINECOUNT"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 6
                        'Me.EnglishName = "Paragraphs"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "PARCOUNT"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 7
                        'Me.EnglishName = "Slides"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "SLIDECOUNT"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 8
                        'Me.EnglishName = "Notes"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "NOTECOUNT"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 9
                        'Me.EnglishName = "Hidden Objects"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "HIDDENCOUNT"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 10
                        'Me.EnglishName = "Multimedia Clips"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "MMCLIPCOUNT"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 14
                        Me.EnglishName = "Manager"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName.ToUpper
                        Me.TypeName = TypeNameConstants._String
                    Case 15
                        Me.EnglishName = "Company"
                        Me.SSName = EnglishName.ToUpper
                        Me.Name = Me.EnglishName
                        Me.TypeName = TypeNameConstants._String
                End Select

            Case "ExtendedSummaryInformation"
                Select Case _PropID
                    Case 2
                        'Me.EnglishName = "Name of Saving Application"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = EnglishName
                        'Me.TypeName = TypeNameConstants._String
                    Case 3
                        'Me.EnglishName = "File Size"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "File Size"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 4
                        'Me.EnglishName = "Number of Sheets"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "Number of Sheets"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 5
                        'Me.EnglishName = "Number of Objects"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = "Number of Objects"
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 6
                        'Me.EnglishName = "DocumentID"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = EnglishName
                        'Me.TypeName = TypeNameConstants._String
                    Case 7
                        Me.EnglishName = "Status"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._Integer
                    Case 8
                        Me.EnglishName = "Username"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._String
                    Case 9
                        'Me.EnglishName = "CreationLocale"
                        'Me.Name = Me.EnglishName
                        'Me.SSName = EnglishName
                        'Me.TypeName = TypeNameConstants._Integer
                    Case 14
                        Me.EnglishName = "Hardware"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._Boolean
                    Case 19
                        Me.EnglishName = "StatusChangeDate"
                        Me.Name = Me.EnglishName
                        Me.SSName = ""
                        Me.TypeName = TypeNameConstants._DateTime
                    Case 20
                        Me.EnglishName = "FriendlyUserName"
                        Me.Name = Me.EnglishName
                        Me.SSName = ""
                        Me.TypeName = TypeNameConstants._String
                    Case 22
                        Me.EnglishName = "User Profile Name (created by)"
                        Me.Name = Me.EnglishName
                        Me.SSName = ""
                        Me.TypeName = TypeNameConstants._String
                    Case 23
                        Me.EnglishName = "User Profile Initials (created by)"
                        Me.Name = Me.EnglishName
                        Me.SSName = ""
                        Me.TypeName = TypeNameConstants._String
                    Case 24
                        Me.EnglishName = "User Profile Mailing address (created by)"
                        Me.Name = Me.EnglishName
                        Me.SSName = ""
                        Me.TypeName = TypeNameConstants._String
                    Case 25
                        Me.EnglishName = "User Profile Name (modified by)"
                        Me.Name = Me.EnglishName
                        Me.SSName = ""
                        Me.TypeName = TypeNameConstants._String
                    Case 26
                        Me.EnglishName = "User Profile Initials (modified by)"
                        Me.Name = Me.EnglishName
                        Me.SSName = ""
                        Me.TypeName = TypeNameConstants._String
                    Case 27
                        Me.EnglishName = "User Profile Mailing address (modified by)"
                        Me.Name = Me.EnglishName
                        Me.SSName = ""
                        Me.TypeName = TypeNameConstants._String
                End Select

            Case "ProjectInformation"
                Select Case _PropID
                    Case 2
                        Me.EnglishName = "Document Number"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._String
                    Case 3
                        Me.EnglishName = "Revision"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._String
                    Case 4
                        Me.EnglishName = "Project Name"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._String
                End Select

            Case "MechanicalModeling"
                Select Case _PropID
                    Case 3
                        Me.EnglishName = "Material"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._String
                    Case 19
                        Me.EnglishName = "Sheet Metal Gage"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._String
                    Case 20 'Localized in structured storage
                        Me.EnglishName = "Face Style"
                        Me.Name = Me.EnglishName
                        Me.SSName = ""
                        Me.TypeName = TypeNameConstants._String
                    Case 21 'Localized in structured storage
                        Me.EnglishName = "Fill Style"
                        Me.Name = Me.EnglishName
                        Me.SSName = ""
                        Me.TypeName = TypeNameConstants._String
                    Case 22 'Localized in structured storage
                        Me.EnglishName = "Virtual Style"
                        Me.Name = Me.EnglishName
                        Me.SSName = ""
                        Me.TypeName = TypeNameConstants._String
                    Case 23
                        Me.EnglishName = "Density"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._Double
                    Case 24
                        Me.EnglishName = "Coef. of Thermal Exp"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._Double
                    Case 25
                        Me.EnglishName = "Thermal Conductivity"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._Double
                    Case 26
                        Me.EnglishName = "Specific Heat"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._Double
                    Case 27
                        Me.EnglishName = "Modulus of Elasticity"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._Double
                    Case 28
                        Me.EnglishName = "Poisson's Ratio"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._Double
                    Case 29
                        Me.EnglishName = "Yield Stress"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._Double
                    Case 30
                        Me.EnglishName = "Ultimate Stress"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._Double
                    Case 31
                        Me.EnglishName = "Elongation"
                        Me.Name = Me.EnglishName
                        Me.SSName = EnglishName
                        Me.TypeName = TypeNameConstants._Double
                    Case 32
                        Me.EnglishName = "Bead Material"
                        Me.Name = Me.EnglishName
                        Me.SSName = ""
                        Me.TypeName = TypeNameConstants._String
                End Select

            Case "Custom"
                ' Nothing to do here

            Case Else
                MsgBox(String.Format("In HCPropertiesData.New PropertyData: PropertySetActualName '{0}' not recognized", Me.PropertySetActualName))

        End Select

    End Sub

    Public Function ToJSON() As String

        Dim JSONString As String

        Dim tmpDict As New Dictionary(Of String, String)

        tmpDict("Name") = Me.Name
        tmpDict("EnglishName") = Me.EnglishName
        tmpDict("SSName") = Me.SSName
        tmpDict("PropertySetName") = CStr(CInt(Me.PropertySetName)) ' "0", "1"
        tmpDict("PropertySetActualName") = Me.PropertySetActualName
        tmpDict("TypeName") = CStr(CInt(Me.TypeName))
        tmpDict("PropID") = CStr(Me.PropID)
        tmpDict("PropertySource") = CStr(CInt(Me.PropertySource))
        tmpDict("FavoritesListIdx") = CStr(Me.FavoritesListIdx)
        tmpDict("IsDuplicate") = CStr(Me.IsDuplicate)

        If Not CheckJSONDict(tmpDict) Then
            MsgBox(String.Format("{0}: Missing property names in JSON dictionary", Me.ToString))
            JSONString = ""
        Else
            JSONString = JsonConvert.SerializeObject(tmpDict)
        End If

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)

        Dim tmpDict As Dictionary(Of String, String)

        tmpDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

        If Not CheckJSONDict(tmpDict) Then
            Throw New Exception(String.Format("{0}: Missing property names in JSON dictionary", Me.ToString))
        End If

        Try
            Me.Name = tmpDict("Name")
            Me.EnglishName = tmpDict("EnglishName")
            Me.SSName = tmpDict("SSName")
            Me.PropertySetName = CType(CInt(tmpDict("PropertySetName")), PropertySetNameConstants)
            Me.PropertySetActualName = tmpDict("PropertySetActualName")
            Me.TypeName = CType(CInt(tmpDict("TypeName")), TypeNameConstants)
            Me.PropID = CInt(tmpDict("PropID"))
            Me.PropertySource = CType(CInt(tmpDict("PropertySource")), PropertySourceConstants)
            Me.FavoritesListIdx = CInt(tmpDict("FavoritesListIdx"))
            Me.IsDuplicate = CBool(tmpDict("IsDuplicate"))
        Catch ex As Exception

        End Try

    End Sub

    Private Function CheckJSONDict(JSONDict As Dictionary(Of String, String)) As Boolean
        Dim Proceed As Boolean = True

        Dim PropInfos() As PropertyInfo = Me.GetType.GetProperties()

        ' Check for missing info
        For Each PropInfo As PropertyInfo In PropInfos
            If Not JSONDict.Keys.Contains(PropInfo.Name) Then
                Proceed = False
                Exit For
            End If
        Next

        Return Proceed
    End Function


End Class

