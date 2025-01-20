Option Strict On
Imports Newtonsoft.Json

Public Class PropertyFilters
    Public Property Items As List(Of HCPropertyFilter)

    Public Sub New()
        Dim UP As New UtilsPreferences
        Dim Infile As String = UP.GetPropertyFiltersFilename(CheckExisting:=True)
        Dim JSONString As String

        If Not Infile = "" Then
            JSONString = IO.File.ReadAllText(Infile)
            FromJSON(JSONString)
        Else
            Me.Items = New List(Of HCPropertyFilter)
        End If

    End Sub

    Public Sub New(JSONString As String)
        Me.Items = New List(Of HCPropertyFilter)
        FromJSON(JSONString)
    End Sub

    Public Sub Save()
        Dim UP As New UtilsPreferences
        Dim JSONString As String
        Dim Outfile As String

        Outfile = UP.GetPropertyFiltersFilename(CheckExisting:=False)
        JSONString = ToJSON()

        IO.File.WriteAllText(Outfile, JSONString)
    End Sub

    Public Sub RemoveItem(_Name As String)
        For idx = 0 To Me.Items.Count - 1
            If Me.Items(idx).Name = _Name Then
                Me.Items.RemoveAt(idx)
                Exit For
            End If
        Next
    End Sub
    Public Function GetActivePropertyFilter() As HCPropertyFilter
        Dim tmpPropertyFilter As HCPropertyFilter = Nothing

        For Each Item As HCPropertyFilter In Me.Items
            If Item.IsActiveFilter Then
                tmpPropertyFilter = Item
                Exit For
            End If
        Next

        Return tmpPropertyFilter
    End Function

    Public Function GetPropertyFilter(Name As String) As HCPropertyFilter
        Dim tmpPropertyFilter As HCPropertyFilter = Nothing

        For Each Item As HCPropertyFilter In Me.Items
            If Item.Name = Name Then
                tmpPropertyFilter = Item
                Exit For
            End If
        Next

        Return tmpPropertyFilter
    End Function

    Public Function ToJSON() As String
        Dim JSONString As String

        Dim tmpItemsList As New List(Of String)

        For Each Item As HCPropertyFilter In Items
            tmpItemsList.Add(Item.ToJSON)
        Next

        JSONString = JsonConvert.SerializeObject(tmpItemsList)

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)
        Dim tmpItemsList As List(Of String)

        tmpItemsList = JsonConvert.DeserializeObject(Of List(Of String))(JSONString)

        If Me.Items Is Nothing Then
            Me.Items = New List(Of HCPropertyFilter)
        End If

        Me.Items.Clear()

        If tmpItemsList IsNot Nothing Then
            For Each ItemJSON In tmpItemsList
                Dim P As New HCPropertyFilter(ItemJSON)
                Me.Items.Add(P)
            Next
        End If
    End Sub

End Class

Public Class HCPropertyFilter
    Public Property Name As String
    Public Property Formula As String  ' "A AND NOT ( B OR C )", etc.
    Public Property Conditions As List(Of PropertyFilterCondition)
    Public Property IsActiveFilter As Boolean

    Public Sub New()
        Me.Conditions = New List(Of PropertyFilterCondition)
    End Sub

    Public Sub New(JSONString As String)
        Me.Conditions = New List(Of PropertyFilterCondition)
        FromJSON(JSONString)
    End Sub

    Public Function ToJSON() As String
        Dim JSONString As String

        Dim tmpConditionsList As New List(Of String)
        Dim tmpConditionsListJSON As String
        Dim tmpDict As New Dictionary(Of String, String)

        tmpDict("Name") = Me.Name
        tmpDict("Formula") = Me.Formula
        tmpDict("IsActiveFilter") = CStr(Me.IsActiveFilter)

        For Each Condition As PropertyFilterCondition In Me.Conditions
            tmpConditionsList.Add(Condition.ToJSON)
        Next

        tmpConditionsListJSON = JsonConvert.SerializeObject(tmpConditionsList)

        tmpDict("ConditionsListJSON") = tmpConditionsListJSON

        JSONString = JsonConvert.SerializeObject(tmpDict)

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)
        Dim tmpConditionsList As List(Of String)
        Dim tmpConditionsListJSON As String
        Dim tmpDict As Dictionary(Of String, String)

        tmpDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

        Me.Name = tmpDict("Name")
        Me.Formula = tmpDict("Formula")
        Me.IsActiveFilter = CBool(tmpDict("IsActiveFilter"))

        tmpConditionsListJSON = tmpDict("ConditionsListJSON")
        tmpConditionsList = JsonConvert.DeserializeObject(Of List(Of String))(tmpConditionsListJSON)

        Me.Conditions.Clear()

        For Each ConditionJSON As String In tmpConditionsList
            Dim C As New PropertyFilterCondition(ConditionJSON)
            Me.Conditions.Add(C)
        Next

    End Sub

    Public Sub SortConditions()

        Dim tmpConditionsDict As New Dictionary(Of String, PropertyFilterCondition)
        Dim tmpVariableNameList As New List(Of String)
        Dim Name As String

        For Each C As PropertyFilterCondition In Me.Conditions
            Name = C.VariableName
            If Not tmpConditionsDict.Keys.Contains(Name) Then
                tmpConditionsDict(Name) = C
            Else
                MsgBox(String.Format("Duplicate variable name '{0}' found in PropertyFilter Conditions", Name), vbOKOnly)
                Exit Sub
            End If
        Next

        tmpVariableNameList = tmpConditionsDict.Keys.ToList

        tmpVariableNameList.Sort()

        Me.Conditions.Clear()

        For i As Integer = 0 To tmpVariableNameList.Count - 1
            Name = tmpVariableNameList(i)
            Dim C As PropertyFilterCondition = tmpConditionsDict(Name)
            Dim NewName As String = Chr(i + 65)
            C.VariableName = NewName
            Me.Conditions.Add(C)
        Next

    End Sub

End Class

Public Class PropertyFilterCondition
    ' ###### PropertyFilterDict is obsolete and should be removed throughout. ######
    ' PropertyFilterDict format:
    '{"0":
    '    {"Variable":"A",
    '     "PropertySet":"Custom",
    '     "PropertyName":"hmk_Part_Number",
    '     "Comparison":"contains",
    '     "Value":"aluminum",
    '     "Formula":" A AND B "},
    ' "1":
    '...
    '}

    Public Property VariableName As String  ' "A", "B", etc.  Used in property filter formulas.
    Public Property PropertySetName As PropertySetNameConstants
    Public Property PropertySetActualName As String  ' "SummaryInformation", "Custom", etc.
    Public Property PropertyName As String  ' "Title", "Titolo", etc.
    Public Property EnglishName As String  ' "Title", etc.
    Public Property Comparison As ComparisonConstants
    Public Property Value As String  ' "aluminum", "%{System.Material|R1}", etc.

    Public Enum PropertySetNameConstants
        Custom
        System
        Duplicate
        Server
    End Enum

    Public Enum ComparisonConstants
        Contains
        IsExactly
        WildcardMatch
        RegexMatch
        GreaterThan
        LessThan
    End Enum

    Public Sub New()

    End Sub

    Public Sub New(JSONString As String)
        FromJSON(JSONString)
    End Sub

    Public Function ToJSON() As String
        Dim JSONString As String
        Dim tmpComparisonDict As New Dictionary(Of String, String)

        tmpComparisonDict("VariableName") = Me.VariableName
        tmpComparisonDict("PropertySetName") = CStr(CInt(Me.PropertySetName))
        tmpComparisonDict("PropertySetActualName") = Me.PropertySetActualName
        tmpComparisonDict("PropertyName") = Me.PropertyName
        tmpComparisonDict("EnglishName") = Me.EnglishName
        tmpComparisonDict("Comparison") = CStr(CInt(Me.Comparison))
        tmpComparisonDict("Value") = Me.Value

        JSONString = JsonConvert.SerializeObject(tmpComparisonDict)

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)

        Dim tmpComparisonDict As Dictionary(Of String, String)
        tmpComparisonDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

        Me.VariableName = tmpComparisonDict("VariableName")
        Me.PropertySetName = CType(CInt(tmpComparisonDict("PropertySetName")), PropertySetNameConstants)
        Me.PropertySetActualName = tmpComparisonDict("PropertySetActualName")
        Me.PropertyName = tmpComparisonDict("PropertyName")
        Me.EnglishName = tmpComparisonDict("EnglishName")
        Me.Comparison = CType(CInt(tmpComparisonDict("Comparison")), ComparisonConstants)
        Me.Value = tmpComparisonDict("Value")

    End Sub
End Class
