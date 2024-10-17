Option Strict On

Imports System.Text.RegularExpressions

Public Class UtilsPropertyFilters

    Public Property FMain As Form_Main
    Public Property PropNotFoundString As String = "HOUSEKEEPER_PROP_NOT_FOUND"

    Public Sub New(_Form_Main As Form_Main)
        Me.FMain = _Form_Main
    End Sub

    Public Function PropertyFilter(FoundFiles As IReadOnlyCollection(Of String),
                PropertyFilterDict As Dictionary(Of String, Dictionary(Of String, String))
                ) As IReadOnlyCollection(Of String)

        ' PropertyFilterFormula is a string containing the desired boolean expression
        ' e.g., " A AND ( B OR C ) "
        ' Each variable is separated by whitespace from any parenthesis character or operator

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

        Dim PropertyFilterFormula As String = PropertyFilterDict("0")("Formula")  ' Formula is the same for all entries.  Picking the first one.

        Dim LocalFoundFiles As New List(Of String)
        Dim FilteredFiles As New List(Of String)
        Dim FoundFile As String

        For Each FoundFile In FoundFiles
            LocalFoundFiles.Add(FoundFile)
        Next

        FilteredFiles = ProcessFiles(LocalFoundFiles, PropertyFilterDict, PropertyFilterFormula)

        FMain.TextBoxStatus.Text = ""

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

        FMain.Activate()

        For Each FoundFile In FoundFiles
            If Form_Main.StopProcess Then
                Exit For
            End If

            msg = System.IO.Path.GetFileName(FoundFile)
            FMain.TextBoxStatus.Text = String.Format("Property Filter {0}", msg)

            tf = ProcessFile(DMApp, FoundFile, PropertyFilterDict, PropertyFilterFormula)
            If tf Then
                FilteredFiles.Add(FoundFile)
            End If
        Next

        DMApp.Quit()

        Return FilteredFiles
    End Function

    Public Function ProcessFile(DMApp As DesignManager.Application, FoundFile As String,
                PropertyFilterDict As Dictionary(Of String, Dictionary(Of String, String)),
                PropertyFilterFormula As String) As Boolean

        ' Returns True if a match is found

        Dim tf As Boolean
        Dim DMDoc As DesignManager.Document
        Dim Extension As String
        Dim LinkedDocuments As DesignManager.LinkedDocuments
        Dim LinkedDocument As DesignManager.Document

        System.Windows.Forms.Application.DoEvents()
        If Form_Main.StopProcess Then
            Return False
        End If

        Extension = System.IO.Path.GetExtension(FoundFile)

        If Extension = ".dft" Then
            DMDoc = CType(DMApp.Open(FoundFile), DesignManager.Document)
            LinkedDocuments = CType(DMDoc.LinkedDocuments, DesignManager.LinkedDocuments)

            If FMain.CheckBoxPropertyFilterIncludeDraftItself.Checked Then
                tf = ProcessProperties(FoundFile, DMApp, PropertyFilterDict, PropertyFilterFormula, Extension)
            Else
                tf = False
            End If

            If FMain.CheckBoxPropertyFilterIncludeDraftModel.Checked Then
                For Each LinkedDocument In LinkedDocuments
                    tf = tf Or ProcessFile(DMApp, LinkedDocument.FullName, PropertyFilterDict, PropertyFilterFormula)
                Next
            End If
        Else
            tf = ProcessProperties(FoundFile, DMApp, PropertyFilterDict, PropertyFilterFormula, Extension)
        End If

        Return tf

    End Function

    Public Function ProcessProperties(FoundFile As String,
        DMApp As DesignManager.Application,
        PropertyFilterDict As Dictionary(Of String, Dictionary(Of String, String)),
        PropertyFilterFormula As String,
        Extension As String) As Boolean

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

        Dim UC As New UtilsCommon

        PropertySets = CType(DMApp.PropertySets, DesignManager.PropertySets)
        PropertySets.Open(FoundFile, True)

        For Each Key As String In PropertyFilterDict.Keys
            Variable = PropertyFilterDict(Key)("Variable")
            PropertySet = PropertyFilterDict(Key)("PropertySet")
            PropertyName = PropertyFilterDict(Key)("PropertyName")
            Comparison = PropertyFilterDict(Key)("Comparison")

            Value = PropertyFilterDict(Key)("Value")
            Value = UC.SubstitutePropertyFormula(Nothing, Nothing, DMApp, FoundFile, Value, ValidFilenameRequired:=False,
                                                 FMain.TemplatePropertyDict)

            DocValue = SearchProperties(PropertySets, PropertyName, FoundFile)

            tf2 = DoComparison(Comparison, Value, DocValue)

            VariableTruthValues.Add(Variable, tf2.ToString)

        Next

        'PropertySets.Close()

        BooleanExpression = DoSubstitution(PropertyFilterFormula, VariableTruthValues)

        tf = EvaluateBoolean(BooleanExpression)

        Return tf

    End Function

    Public Function DoSubstitution(Formula As String, VariableTruthValues As Dictionary(Of String, String)) As String
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

    Public Function DoComparison(Comparison As String, Value As String, DocValue As String) As Boolean

        Dim tf As Boolean = False

        If Not DocValue = Me.PropNotFoundString Then  ' Can't match a prop that doesn't exist

            If Comparison = "contains" Then
                tf = DocValue.ToLower.Contains(Value.ToLower)
            ElseIf Comparison = "is_exactly" Then
                tf = DocValue.ToLower = Value.ToLower
            ElseIf Comparison = "is_not" Then
                tf = DocValue.ToLower <> Value.ToLower
            ElseIf Comparison = "wildcard_match" Then
                tf = DocValue.ToLower Like Value.ToLower
            ElseIf Comparison = "regex_match" Then
                tf = Regex.IsMatch(DocValue, Value, RegexOptions.IgnoreCase)
            ElseIf Comparison = ">" Then
                Try
                    tf = TextToDouble(DocValue) > TextToDouble(Value)
                Catch ex As Exception
                    tf = DocValue.ToLower > Value.ToLower
                End Try
            ElseIf Comparison = "<" Then
                Try
                    tf = TextToDouble(DocValue) < TextToDouble(Value)
                Catch ex As Exception
                    tf = DocValue.ToLower < Value.ToLower
                End Try
            End If

        End If

        Return tf
    End Function

    Public Function TextToDouble(Text As String) As Double
        Dim DoubleNumber As Double

        Dim Units As New List(Of String)
        Units.Add("in")
        Units.Add("mm")
        Units.Add("lbm")
        Units.Add("kg")

        Dim DateTime As DateTime

        ' First try to convert the text directly.
        ' If that fails, try to strip a unit from the end of the text, then convert.
        ' If that fails, either because a unit match was not found, or the conversion still didn't work, 
        ' try to treat it as a date and convert
        ' If that fails, raise a FormatException for the function caller.

        Try
            DoubleNumber = CDbl(Text)
            Return DoubleNumber
        Catch ex As Exception
            Try
                For Each Unit In Units
                    ' Example "0.325 in",  Length=8, UnitLength=2, idx_start = 6 = Length - UnitLength.
                    If Text.Substring(Text.Length - Unit.Length) = Unit Then
                        DoubleNumber = CDbl(Text.Substring(0, Text.Length - Unit.Length))
                        Return DoubleNumber
                    End If
                Next
            Catch ex2 As Exception
            End Try
        End Try

        DateTime = Convert.ToDateTime(Text, Globalization.CultureInfo.CurrentCulture)  ' Returns a FormatException if it doesn't work.
        Text = String.Format("{0}{1}{2}", DateTime.Year, DateTime.Month, DateTime.Day)
        DoubleNumber = CDbl(Text)

        Return DoubleNumber

    End Function


    Public Function SearchProperties(PropertySets As DesignManager.PropertySets,
                                     PropertyName As String,
                                     FoundFile As String) As String

        Dim DocValue As String = Me.PropNotFoundString
        Dim PropertySet As DesignManager.Properties
        Dim Prop As DesignManager.Property
        Dim PropertySetNames As New List(Of String)
        Dim PropertySetName As String

        Dim FilePropNames = {"File Name", "File Name (full path)", "File Name (no extension)"}.ToList

        If FilePropNames.Contains(PropertyName) Then
            Select Case PropertyName
                Case "File Name"
                    DocValue = System.IO.Path.GetFileName(FoundFile)  ' C:\project\part.par -> part.par
                Case "File Name (full path)"
                    DocValue = FoundFile  ' C:\project\part.par -> C:\project\part.par
                Case "File Name (no extension)"
                    DocValue = System.IO.Path.GetFileNameWithoutExtension(FoundFile)  ' C:\project\part.par -> part
            End Select

        Else
            Dim DateTime As DateTime

            PropertySetNames.Add("SummaryInformation")
            PropertySetNames.Add("ExtendedSummaryInformation")
            PropertySetNames.Add("DocumentSummaryInformation")
            PropertySetNames.Add("ProjectInformation")
            PropertySetNames.Add("MechanicalModeling") ' Not in Draft or non-weldment Assemblies.
            PropertySetNames.Add("Custom") ' Checked last.  In case of duplicate names, system properties get assigned.

            Dim TypeName As String

            Dim GotAMatch As Boolean = False

            For Each PropertySetName In PropertySetNames
                ' Not all files have all PropertySets
                Try
                    PropertySet = CType(PropertySets.Item(PropertySetName), DesignManager.Properties)
                    For i As Integer = 0 To PropertySet.Count - 1
                        Prop = CType(PropertySet.Item(i), DesignManager.Property)
                        If Prop.Name.ToLower = PropertyName.ToLower Then
                            TypeName = Microsoft.VisualBasic.Information.TypeName(Prop.Value)
                            DocValue = Prop.Value.ToString
                            If TypeName.ToLower = "date" Then
                                DateTime = Convert.ToDateTime(DocValue, Globalization.CultureInfo.CurrentCulture)
                                DocValue = String.Format("{0}{1}{2}", DateTime.Year, DateTime.Month, DateTime.Day)
                            End If
                            GotAMatch = True
                            Exit For
                        End If
                    Next
                Catch ex As Exception
                End Try

                If GotAMatch Then
                    Exit For
                End If
            Next

        End If



        Return DocValue
    End Function

    Public Function EvaluateBoolean(Formula As String) As Boolean
        Dim tf As Boolean

        Dim UPS As New UtilsPowerShell

        Try
            Dim Result As String = UPS.RunScript(FormulaToPSSyntax(Formula))

            If Result.ToLower.Contains("true") Then
                tf = True
            Else
                tf = False
            End If
        Catch ex As Exception
            tf = False
        End Try

        Return tf

        '' https://stackoverflow.com/questions/49005926/conversion-from-string-to-boolean-vb-net
        'Dim sc As New MSScriptControl.ScriptControl
        ''SET LANGUAGE TO VBSCRIPT
        'sc.Language = "VBSCRIPT"
        ''ATTEMPT MATH
        'Try
        '    Return Convert.ToBoolean(sc.Eval(formula))
        'Catch ex As Exception
        '    'SHOW THAT IT WAS INVALID
        '    MsgBox(String.Format("Unable to evaluate boolean expression: {0}", formula))
        '    Return (False)
        'End Try
    End Function

    Public Function FormulaToPSSyntax(Formula As String) As String
        Dim s As String = Formula.ToUpper.Trim
        Dim s1 As String = ""

        s = String.Format(" {0}", s)
        s = s.Replace("TRUE", "$TRUE")
        s = s.Replace("FALSE", "$FALSE")

        s1 = s(0)

        For i = 1 To Len(s) - 1
            If s(i - 1) = " " Then
                If (Asc(s(i)) >= 65) And (Asc(s(i)) <= 90) Then
                    s1 = String.Format("{0}-", s1)
                End If
            End If
            s1 = String.Format("{0}{1}", s1, s(i))
        Next

        Return s1.Trim
    End Function
End Class
