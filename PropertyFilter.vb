Option Strict Off

Public Class PropertyFilter
    Private _mainInstance As Form1

    Public Sub New(mainInstance As Form1)
        _mainInstance = mainInstance
    End Sub

    Public Function PropertyFilter(FoundFiles As IReadOnlyCollection(Of String),
                PropertyFilterDict As Dictionary(Of String, Dictionary(Of String, String)),
                PropertyFilterFormula As String) As IReadOnlyCollection(Of String)

        ' PropertyFilterFormula is a string containing the desired boolean expression
        ' e.g., " A AND ( B OR C ) "
        ' Each variable is separated by whitespace from any parenthesis character or operator

        ' PropertyFilterDict is a Dictionary whose keys are the forumla variables: "A", "B", ...
        ' Sub keys are "PropertyString", "Comparison", and "Value"

        Dim LocalFoundFiles As New List(Of String)
        Dim FilteredFiles As New List(Of String)
        Dim FoundFile As String

        For Each FoundFile In FoundFiles
            LocalFoundFiles.Add(FoundFile)
        Next

        FilteredFiles = ProcessFiles(LocalFoundFiles, PropertyFilterDict, PropertyFilterFormula)

        Form1.TextBoxStatus.Text = ""

        Return FilteredFiles
    End Function


    Private Function ProcessFiles(FoundFiles As List(Of String),
                PropertyFilterDict As Dictionary(Of String, Dictionary(Of String, String)),
                PropertyFilterFormula As String) As List(Of String)
        Dim FoundFile As String
        Dim FilteredFiles As New List(Of String)
        Dim tf As Boolean
        Dim DMApp As New DesignManager.Application
        Dim msg As String

        DMApp.Visible = 1  ' So it can be seen and closed in case of program malfunction.

        Form1.Activate()

        For Each FoundFile In FoundFiles
            If Form1.StopProcess Then
                Exit For
            End If
            msg = Form1.TruncateFullPath(FoundFile)
            Form1.TextBoxStatus.Text = String.Format("Property Filter {0}", msg)
            tf = ProcessFile(DMApp, FoundFile, PropertyFilterDict, PropertyFilterFormula)
            If tf Then
                FilteredFiles.Add(FoundFile)
            End If
        Next

        DMApp.Quit()

        Return FilteredFiles
    End Function

    Private Function ProcessFile(DMApp As DesignManager.Application, FoundFile As String,
                PropertyFilterDict As Dictionary(Of String, Dictionary(Of String, String)),
                PropertyFilterFormula As String) As Boolean
        Dim tf As Boolean
        Dim DMDoc As DesignManager.Document
        Dim Extension As String
        Dim LinkedDocuments As DesignManager.LinkedDocuments
        Dim LinkedDocument As DesignManager.Document

        System.Windows.Forms.Application.DoEvents()
        If Form1.StopProcess Then
            Return False
        End If

        Extension = System.IO.Path.GetExtension(FoundFile)
        If Extension = ".dft" Then
            DMDoc = CType(DMApp.Open(FoundFile), DesignManager.Document)
            LinkedDocuments = CType(DMDoc.LinkedDocuments, DesignManager.LinkedDocuments)
            tf = False
            For Each LinkedDocument In LinkedDocuments
                tf = tf Or ProcessFile(DMApp, LinkedDocument.FullName, PropertyFilterDict, PropertyFilterFormula)
            Next
        Else
            tf = ProcessProperties(FoundFile, DMApp, PropertyFilterDict, PropertyFilterFormula)
        End If

        Return tf

    End Function

    Private Function ProcessProperties(FoundFile As String,
        DMApp As DesignManager.Application,
        PropertyFilterDict As Dictionary(Of String, Dictionary(Of String, String)),
        PropertyFilterFormula As String) As Boolean

        Dim tf As Boolean
        Dim PropertySets As DesignManager.PropertySets
        Dim Variable As String
        Dim PropertySet As String
        Dim PropertyName As String
        Dim Comparison As String
        Dim Value As String
        Dim DocValue As String
        Dim tf2 As Boolean
        Dim VariableTruthValues As New Dictionary(Of String, String)
        Dim BooleanExpression As String

        PropertySets = CType(DMApp.PropertySets, DesignManager.PropertySets)
        PropertySets.Open(FoundFile, True)

        For Each Variable In PropertyFilterDict.Keys
            PropertySet = ParsePropertyString(PropertyFilterDict(Variable)("PropertyString"), "PropertySet")
            PropertyName = ParsePropertyString(PropertyFilterDict(Variable)("PropertyString"), "PropertyName")
            Comparison = PropertyFilterDict(Variable)("Comparison")
            Value = PropertyFilterDict(Variable)("Value")

            DocValue = SearchProperties(PropertySets, PropertySet, PropertyName)

            tf2 = DoComparison(Comparison, Value, DocValue)

            VariableTruthValues.Add(Variable, tf2.ToString)

        Next

        BooleanExpression = DoSubstitution(PropertyFilterFormula, VariableTruthValues)

        tf = EvaluateBoolean(BooleanExpression)

        Return tf

    End Function

    Private Function DoSubstitution(Formula As String, VariableTruthValues As Dictionary(Of String, String)) As String
        Dim Result As String
        Dim Variable As String
        Dim var As String
        Dim val As String

        Result = Formula

        For Each Variable In VariableTruthValues.Keys
            var = String.Format(" {0} ", Variable)
            val = String.Format(" {0} ", VariableTruthValues(Variable))
            Result = Result.Replace(var, val)
        Next

        Return Result
    End Function

    Private Function DoComparison(Comparison As String, Value As String, DocValue As String) As Boolean
        Dim tf As Boolean

        If Comparison = "contains" Then
            tf = DocValue.ToLower.Contains(Value.ToLower)
        ElseIf Comparison = "is_exactly" Then
            tf = DocValue.ToLower = Value.ToLower
        ElseIf Comparison = "is_not" Then
            tf = DocValue.ToLower <> Value.ToLower
        End If

        Return tf
    End Function

    Private Function SearchProperties(PropertySets As DesignManager.PropertySets,
                                      PropertySet As String,
                                      PropertyName As String) As String
        Dim DocValue As String = ""
        Dim Properties As DesignManager.Properties
        Dim Prop As DesignManager.Property
        Dim SystemPropertySets As New List(Of String)
        Dim SystemPropertySet As String

        SystemPropertySets.Add("ProjectInformation")
        SystemPropertySets.Add("MechanicalModeling")
        SystemPropertySets.Add("SummaryInformation")
        SystemPropertySets.Add("ExtendedSummaryInformation")
        SystemPropertySets.Add("DocumentSummaryInformation")

        If PropertySet.ToLower = "custom" Then
            ' The property may not be in every file
            Try
                Properties = CType(PropertySets.Item("Custom"), DesignManager.Properties)
                Prop = Properties.Item(PropertyName)
                DocValue = Prop.Value.ToString
            Catch ex As Exception
            End Try
        Else
            For Each SystemPropertySet In SystemPropertySets
                Try
                    Properties = CType(PropertySets.Item(SystemPropertySet), DesignManager.Properties)
                    Prop = Properties.Item(PropertyName)
                    DocValue = Prop.Value.ToString
                    Exit For
                Catch ex As Exception
                End Try
            Next

        End If

        Return DocValue
    End Function

    Private Function ParsePropertyString(PropertyString As String, Element As String) As String
        Dim Result As String
        Dim PropertyStringList As List(Of String)

        PropertyStringList = PropertyString.Split("."c).ToList

        If Element = "PropertySet" Then
            Result = PropertyStringList(0)
        Else
            PropertyStringList.RemoveAt(0)

            Result = PropertyStringList(0)
            For i = 1 To PropertyStringList.Count - 1
                Result += String.Format(".{0}", PropertyStringList(i))
            Next

        End If

        Return Result
    End Function

    Private Function EvaluateBoolean(formula As String) As Boolean
        ' https://stackoverflow.com/questions/49005926/conversion-from-string-to-boolean-vb-net
        Dim sc As New MSScriptControl.ScriptControl
        'SET LANGUAGE TO VBSCRIPT
        sc.Language = "VBSCRIPT"
        'ATTEMPT MATH
        Try
            Return Convert.ToBoolean(sc.Eval(formula))
        Catch ex As Exception
            'SHOW THAT IT WAS INVALID
            MsgBox(String.Format("Unable to evaluate boolean expression: {0}", formula))
            Return (False)
        End Try
    End Function


End Class
