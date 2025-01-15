Option Strict On

Imports Newtonsoft.Json

Public Class HCPropertiesData

    Public Property Items As List(Of PropertyData)

    Public Sub New()

        Items = New List(Of PropertyData)

        Dim UP As New UtilsPreferences
        Dim Infile As String = UP.GetPropertiesDataFilename(CheckExisting:=True)

        If Not Infile = "" Then
            Dim JSONString As String = IO.File.ReadAllText(Infile)

            Dim tmpList As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(JSONString)

            For Each PropertyDataJSON As String In tmpList
                Dim tmpPropertyData As New PropertyData
                tmpPropertyData.FromJSON(PropertyDataJSON)
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

    Public Function GetPropertyData(Name As String, Optional tmpItems As List(Of PropertyData) = Nothing) As PropertyData

        Dim tmpProperty As PropertyData = Nothing

        If tmpItems Is Nothing Then
            For Each Item As PropertyData In Items
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

    Public Sub Populate(TemplateList As List(Of String))

        'Dim tmpPropertiesData As New PropertiesData
        Dim tmpItems As New List(Of PropertyData)

        Dim PreviousFavoritesList As List(Of String) = GetFavoritesList()

        Dim PropertySets As SolidEdgeFileProperties.PropertySets
        Dim PropertySet As SolidEdgeFileProperties.Properties
        Dim Prop As SolidEdgeFileProperties.Property
        PropertySets = New SolidEdgeFileProperties.PropertySets
        Dim PropertySetActualName As String
        Dim PropertySetHousekeeperName As String
        Dim PropName As String = ""
        Dim PropID As Integer
        Dim PropTypeName As String

        Dim tf As Boolean

        Dim tmpPropertyData As PropertyData
        Dim tmpPreviousPropertyData As PropertyData

        ' ###### PROPERTIES TO PROCESS ######

        Dim KeepDict As New Dictionary(Of String, List(Of String))
        KeepDict("SummaryInformation") = {"Title", "Subject", "Author", "Keywords", "Comments", "Last Author", "Origin Date", "Last Save Date"}.ToList
        KeepDict("DocumentSummaryInformation") = {"Category", "Manager", "Company"}.ToList
        KeepDict("ExtendedSummaryInformation") = {"Status", "Username", "Hardware", "StatusChangeDate", "FriendlyUserName"}.ToList
        KeepDict("ProjectInformation") = {"Document Number", "Revision", "Project Name"}.ToList
        KeepDict("MechanicalModeling") = {"Material", "Sheet Metal Gage"}.ToList

        ' Do Custom last to deal with duplicates
        KeepDict("Custom") = New List(Of String)

        ' ###### PROCESS TEMPLATES ######

        For Each TemplateName As String In TemplateList
            tf = Not TemplateName = ""
            tf = tf And FileIO.FileSystem.FileExists(TemplateName)
            If tf Then

                PropertySets.Open(TemplateName, True)

                ' ###### PROCESS PROPERTY SETS ######

                For Each PropertySetActualName In KeepDict.Keys

                    ' Not all files have all property sets.
                    Try
                        PropertySet = CType(PropertySets.Item(PropertySetActualName), SolidEdgeFileProperties.Properties)
                    Catch ex As Exception
                        Continue For
                    End Try

                    If PropertySetActualName = "Custom" Then
                        PropertySetHousekeeperName = "Custom"
                    Else
                        PropertySetHousekeeperName = "System"
                    End If

                    For i = 0 To PropertySet.Count - 1

                        ' ###### PROCESS PROPERTY ######

                        Try
                            ' ###### GET THE PROPERTY OBJECT ######

                            Prop = CType(PropertySet.Item(i), SolidEdgeFileProperties.Property)
                            PropName = Prop.Name
                            PropID = Prop.ID
                            If PropertySetActualName = "Custom" Then PropID = -1

                            Dim EnglishName As String = PropLocalizedToEnglishOrSS(PropertySetActualName, IDNumber:=PropID, WhichName:="English")
                            If EnglishName = Nothing Then EnglishName = PropName

                            ' Skip unwanted properties
                            tf = Not PropertySetActualName = "Custom"
                            tf = tf And KeepDict.Keys.Contains(PropertySetActualName)
                            tf = tf And Not KeepDict(PropertySetActualName).Contains(EnglishName)
                            If tf Then
                                Continue For
                            End If

                            PropTypeName = Microsoft.VisualBasic.Information.TypeName(Prop.Value)

                            tmpPropertyData = GetPropertyData(PropName, tmpItems)

                            If tmpPropertyData Is Nothing Then
                                ' ###### PROPERTY NOT FOUND: ADD AND POPULATE IT ######

                                tmpPropertyData = New PropertyData
                                tmpItems.Add(tmpPropertyData)

                                tmpPropertyData.Name = PropName

                                Select Case PropertySetHousekeeperName
                                    Case "System"
                                        tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.System
                                    Case "Custom"
                                        tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.Custom
                                    Case Else
                                        Dim s As String = String.Format("In PropertiesDataPopulate, PropertySet '{0}' not recognized", PropertySetHousekeeperName)
                                        MsgBox(s, vbOKOnly)
                                End Select

                                tmpPropertyData.EnglishName = EnglishName
                                tmpPropertyData.PropertySetActualName = PropertySetActualName
                                tmpPropertyData.PropID = PropID
                                tmpPropertyData.PropertySource = PropertyData.PropertySourceConstants.Auto
                                tmpPropertyData.FavoritesListIdx = -1

                                Select Case PropTypeName
                                    Case "String"
                                        tmpPropertyData.TypeName = PropertyData.TypeNameConstants._String
                                    Case "Integer"
                                        tmpPropertyData.TypeName = PropertyData.TypeNameConstants._Integer
                                    Case "Boolean"
                                        tmpPropertyData.TypeName = PropertyData.TypeNameConstants._Boolean
                                    Case "Date"
                                        tmpPropertyData.TypeName = PropertyData.TypeNameConstants._Date
                                    Case Else
                                        Dim s As String = String.Format("In PropertiesDataPopulate, PropTypeName '{0}' not recognized", PropTypeName)
                                        MsgBox(s, vbOKOnly)
                                End Select

                            Else
                                ' ###### PROPERTY FOUND: CHECK IF DUPLICATE ######

                                tf = PropertySetActualName = "Custom"
                                tf = tf And (Not tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.Custom)
                                If tf Then
                                    tmpPropertyData.IsDuplicate = True
                                End If

                            End If

                        Catch ex As Exception
                            Dim s = "Error building PropertiesData: "
                            s = String.Format("{0} PropertySetName '{1}', Item Number '{2}', PropName '{3}'", s, PropertySetActualName, PropID, PropName)
                            MsgBox(s, vbOKOnly)
                        End Try
                    Next
                Next

                PropertySets.Close()

            End If
        Next


        ' ###### ADD OTHER KNOWN AND SPECIAL PROPERTIES ######

        ' Sheet Metal Gage

        PropName = "Sheet Metal Gage"

        tmpPropertyData = GetPropertyData(PropName)

        If tmpPropertyData Is Nothing Then

            tmpPropertyData = New PropertyData
            tmpItems.Add(tmpPropertyData)

            tmpPropertyData.Name = PropName
            tmpPropertyData.PropertySetName = PropertyData.PropertySetNameConstants.System
            tmpPropertyData.PropertySetActualName = "MechanicalModeling"
            tmpPropertyData.PropID = 19
            tmpPropertyData.EnglishName = PropName
            tmpPropertyData.PropertySource = PropertyData.PropertySourceConstants.Auto
            tmpPropertyData.FavoritesListIdx = -1
            tmpPropertyData.TypeName = PropertyData.TypeNameConstants._String

        Else
            tmpItems.Add(tmpPropertyData)

        End If


        ' Special File Properties

        Dim PropNames = {"File Name", "File Name (full path)", "File Name (no extension)"}.ToList
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

            End If
        End If

    End Sub

    Public Function GetLocalizedEnglishNames() As List(Of String)
        Dim Names As New List(Of String)
        Dim Name As String
        Dim PropertySetActualNames As List(Of String) =
            "SummaryInformation DocumentSummaryInformation ExtendedSummaryInformation ProjectInformation MechanicalModeling".Split(CChar(" ")).ToList
        Dim UsedInSe As String
        Dim LocalizedInSE As String

        For Each PropertySetActualName As String In PropertySetActualNames
            For i = 1 To 50
                UsedInSe = PropLocalizedToEnglishOrSS(PropertySetActualName, i, "UsedInSE")
                LocalizedInSE = PropLocalizedToEnglishOrSS(PropertySetActualName, i, "LocalizedInSE")
                If (Not UsedInSe = Nothing) And (Not LocalizedInSE = Nothing) Then
                    If (UsedInSe.ToLower = "true") And (LocalizedInSE.ToLower = "true") Then
                        Name = PropLocalizedToEnglishOrSS(PropertySetActualName, i, "English")
                        If Not Name = Nothing Then
                            Names.Add(Name)
                        End If
                    End If

                End If
            Next
        Next

        Return Names
    End Function

    Public Function PropLocalizedToEnglishOrSS(
        PropertySetActualName As String,
        IDNumber As Integer,
        WhichName As String
        ) As String

        ' PropertySetActualName: SummaryInformation, DocumentSummaryInformation, ...
        ' IDNumber: The property identifier in Structured Storage (same as Prop.ID in SE)
        ' WhichName: English, SS, UsedInSE, LocalizedInSE
        ' Returns Nothing if no matches occurs

        Dim EnglishName As String = Nothing
        Dim SSName As String = Nothing
        Dim UsedInSE As String = Nothing
        Dim LocalizedInSE As String = Nothing

        Dim WhichNames As List(Of String) = "english ss usedinse localizedinse".Split(CChar(" ")).ToList

        WhichName = WhichName.ToLower

        If Not WhichNames.Contains(WhichName) Then
            Throw New Exception("Valid values for WhichName are 'English', 'SS', 'UsedInSE', 'LocalizedInSE'")
        End If

        If PropertySetActualName.ToLower = "custom" Then
            Return Nothing
        End If

        Select Case PropertySetActualName

            Case "SummaryInformation"
                Select Case IDNumber
                    Case 2
                        EnglishName = "Title"
                        SSName = EnglishName.ToUpper
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 3
                        EnglishName = "Subject"
                        SSName = EnglishName.ToUpper
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 4
                        EnglishName = "Author"
                        SSName = EnglishName.ToUpper
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 5
                        EnglishName = "Keywords"
                        SSName = EnglishName.ToUpper
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 6
                        EnglishName = "Comments"
                        SSName = EnglishName.ToUpper
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 7
                        EnglishName = "Template"
                        SSName = EnglishName.ToUpper
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 8
                        EnglishName = "Last Author"
                        SSName = "LASTAUTHOR"
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 9
                        EnglishName = "Revision Number"
                        SSName = "REVNUMBER"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 10 'Not maintained by SE
                        EnglishName = "Total Editing Time"
                        SSName = "EDITTIME"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 11 'Not maintained by SE
                        EnglishName = "Last Print Date"
                        SSName = "LASTPRINTED"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 12
                        EnglishName = "Origination Date"
                        SSName = "CREATE_DTM"
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 13
                        EnglishName = "Last Save Date"
                        SSName = "LASTSAVE_DTM"
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 14 'Not maintained by SE
                        EnglishName = "Number of pages"
                        SSName = "PAGECOUNT"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 15 'Not maintained by SE
                        EnglishName = "Number of words"
                        SSName = "WORDCOUNT"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 16 'Not maintained by SE
                        EnglishName = "Number of characters"
                        SSName = "CHARCOUNT"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 18
                        EnglishName = "Application Name"
                        SSName = "APPNAME"
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 19
                        EnglishName = "Security"
                        SSName = "DOC_SECURITY"
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                End Select

            Case "DocumentSummaryInformation"
                Select Case IDNumber
                    Case 2
                        EnglishName = "Category"
                        SSName = EnglishName.ToUpper
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 3
                        EnglishName = "Presentation Format"
                        SSName = "PRESFORMAT"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 4
                        EnglishName = "Byte Count"
                        SSName = "BYTECOUNT"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 5
                        EnglishName = "Lines"
                        SSName = "LINECOUNT"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 6
                        EnglishName = "Paragraphs"
                        SSName = "PARCOUNT"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 7
                        EnglishName = "Slides"
                        SSName = "SLIDECOUNT"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 8
                        EnglishName = "Notes"
                        SSName = "NOTECOUNT"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 9
                        EnglishName = "Hidden Objects"
                        SSName = "HIDDENCOUNT"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 10
                        EnglishName = "Multimedia Clips"
                        SSName = "MMCLIPCOUNT"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(True)
                    Case 14
                        EnglishName = "Manager"
                        SSName = EnglishName.ToUpper
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 15
                        EnglishName = "Company"
                        SSName = EnglishName.ToUpper
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                End Select

            Case "ExtendedSummaryInformation"
                Select Case IDNumber
                    Case 2
                        EnglishName = "Name of Saving Application"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 3
                        EnglishName = Nothing
                        SSName = "File Size"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(False)
                    Case 4
                        EnglishName = Nothing
                        SSName = "Number of Sheets"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(False)
                    Case 5
                        EnglishName = Nothing
                        SSName = "Number of Objects"
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(False)
                    Case 6
                        EnglishName = "DocumentID"
                        SSName = EnglishName
                        UsedInSE = CStr(False)
                        LocalizedInSE = CStr(False)
                    Case 7
                        EnglishName = "Status"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 8
                        EnglishName = "Username"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 9
                        EnglishName = "CreationLocale"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 14
                        EnglishName = "Hardware"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 19
                        EnglishName = "StatusChangeDate"
                        SSName = Nothing
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 20
                        EnglishName = "FriendlyUserName"
                        SSName = Nothing
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 22
                        EnglishName = "User Profile Name (created by)"
                        SSName = Nothing
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 23
                        EnglishName = "User Profile Initials (created by)"
                        SSName = Nothing
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 24
                        EnglishName = "User Profile Mailing address (created by)"
                        SSName = Nothing
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 25
                        EnglishName = "User Profile Name (modified by)"
                        SSName = Nothing
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 26
                        EnglishName = "User Profile Initials (modified by)"
                        SSName = Nothing
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 27
                        EnglishName = "User Profile Mailing address (modified by)"
                        SSName = Nothing
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                End Select

            Case "ProjectInformation"
                Select Case IDNumber
                    Case 2
                        EnglishName = "Document Number"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 3
                        EnglishName = "Revision"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                    Case 4
                        EnglishName = "Project Name"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(True)
                End Select

            Case "MechanicalModeling"
                Select Case IDNumber
                    Case 3
                        EnglishName = "Material"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 19
                        EnglishName = "Sheet Metal Gage"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 20 'Localized in structured storage
                        EnglishName = "Face Style"
                        SSName = Nothing
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 21 'Localized in structured storage
                        EnglishName = "Fill Style"
                        SSName = Nothing
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 22 'Localized in structured storage
                        EnglishName = "Virtual Style"
                        SSName = Nothing
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 23
                        EnglishName = "Density"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 24
                        EnglishName = "Coef. of Thermal Exp"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 25
                        EnglishName = "Thermal Conductivity"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 26
                        EnglishName = "Specific Heat"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 27
                        EnglishName = "Modulus of Elasticity"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 28
                        EnglishName = "Poisson's Ratio"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 29
                        EnglishName = "Yield Stress"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 30
                        EnglishName = "Ultimate Stress"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 31
                        EnglishName = "Elongation"
                        SSName = EnglishName
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                    Case 32
                        EnglishName = "Bead Material"
                        SSName = Nothing
                        UsedInSE = CStr(True)
                        LocalizedInSE = CStr(False)
                End Select

        End Select

        If WhichName = "english" Then
            Return EnglishName
        ElseIf WhichName = "ss" Then
            Return SSName
        ElseIf WhichName = "usedinse" Then
            Return UsedInSE
        ElseIf WhichName = "localizedinse" Then
            Return LocalizedInSE
        Else
            Return Nothing
        End If

    End Function

    Public Function ScanFilesForProps(Directory As String, Method As String) As List(Of String)
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

    ' ###### English mapping from PropLocalizedToEnglish    ######
    ' ###### In case of a duplicate, 'IsDuplicate' = TRUE   ######
    ' ###### and PropID will be for the 'System' property.  ######

    Public Property Name As String
    Public Property EnglishName As String
    Public Property PropertySetName As PropertySetNameConstants
    Public Property PropertySetActualName As String
    Public Property TypeName As TypeNameConstants
    Public Property PropID As Integer
    Public Property PropertySource As PropertySourceConstants
    Public Property FavoritesListIdx As Integer
    Public Property IsDuplicate As Boolean = False

    Public Enum PropertySetNameConstants
        System
        Custom
        'Duplicate
        Server
    End Enum

    'Public Enum PropertySetActualNameConstants
    '    SummaryInformation
    '    ExtendedSummaryInformation
    '    DocumentSummaryInformation
    '    ProjectInformation
    '    MechanicalModeling
    '    Custom
    'End Enum

    Public Enum TypeNameConstants
        ' Preceeding names with '_' to avoid VB reserved keywords
        _String
        _Integer
        _Boolean
        _Date
        _Unknown
    End Enum

    Public Enum PropertySourceConstants
        Auto
        Manual
    End Enum

    Public Sub New()

    End Sub


    Public Function ToJSON() As String

        Dim JSONString As String

        Dim tmpDict As New Dictionary(Of String, String)

        tmpDict("Name") = Me.Name
        tmpDict("EnglishName") = Me.EnglishName
        tmpDict("PropertySetName") = CStr(CInt(Me.PropertySetName)) ' "0", "1"
        tmpDict("PropertySetActualName") = Me.PropertySetActualName
        tmpDict("TypeName") = CStr(CInt(Me.TypeName))
        tmpDict("PropID") = CStr(Me.PropID)
        tmpDict("PropertySource") = CStr(CInt(Me.PropertySource))
        tmpDict("FavoritesListIdx") = CStr(Me.FavoritesListIdx)
        tmpDict("IsDuplicate") = CStr(Me.IsDuplicate)

        JSONString = JsonConvert.SerializeObject(tmpDict)

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)

        Dim tmpDict As Dictionary(Of String, String)

        tmpDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

        Me.Name = tmpDict("Name")
        Me.EnglishName = tmpDict("EnglishName")
        Me.PropertySetName = CType(CInt(tmpDict("PropertySetName")), PropertySetNameConstants)
        Me.PropertySetActualName = tmpDict("PropertySetActualName")
        Me.TypeName = CType(CInt(tmpDict("TypeName")), TypeNameConstants)
        Me.PropID = CInt(tmpDict("PropID"))
        Me.PropertySource = CType(CInt(tmpDict("PropertySource")), PropertySourceConstants)
        Me.FavoritesListIdx = CInt(tmpDict("FavoritesListIdx"))
        Me.IsDuplicate = CBool(tmpDict("IsDuplicate"))

    End Sub

End Class

