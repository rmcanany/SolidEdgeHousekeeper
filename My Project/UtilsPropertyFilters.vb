Option Strict On

Imports System.Text.RegularExpressions

Public Class UtilsPropertyFilters

    Public Property FMain As Form_Main
    Public Property PropertyFilter As PropertyFilter
    'Public Property PropNotFoundString As String = "HOUSEKEEPER_PROP_NOT_FOUND"


    Public Sub New(_Form_Main As Form_Main)
        Me.FMain = _Form_Main
        Me.PropertyFilter = FMain.PropertyFilters.GetActivePropertyFilter
    End Sub

    Public Function FilterProperties(
        FoundFiles As IReadOnlyCollection(Of String)
        ) As IReadOnlyCollection(Of String)

        Dim LocalFoundFiles As New List(Of String)
        Dim FilteredFiles As New List(Of String)
        Dim FoundFile As String

        For Each FoundFile In FoundFiles
            LocalFoundFiles.Add(FoundFile)
        Next

        FilteredFiles = ProcessFiles(LocalFoundFiles)

        FMain.TextBoxStatus.Text = ""

        Return FilteredFiles
    End Function

    Private Function ProcessFiles(
        FoundFiles As List(Of String)
        ) As List(Of String)

        Dim FoundFile As String
        Dim FilteredFiles As New List(Of String)
        Dim GotAMatch As Boolean

        For Each FoundFile In FoundFiles
            If Form_Main.StopProcess Then
                Exit For
            End If

            FMain.TextBoxStatus.Text = String.Format("Property Filter {0}", System.IO.Path.GetFileName(FoundFile))

            GotAMatch = ProcessFile(FoundFile)
            If GotAMatch Then
                FilteredFiles.Add(FoundFile)
            End If
        Next

        Return FilteredFiles
    End Function

    Public Function ProcessFile(
        FoundFile As String
        ) As Boolean

        ' Returns True if a match is found

        Dim GotAMatch As Boolean
        Dim Extension As String

        Dim ModelExtensions As New List(Of String)
        ModelExtensions.AddRange({".asm", ".par", ".psm"})

        Dim SSDoc As HCStructuredStorageDoc = Nothing

        System.Windows.Forms.Application.DoEvents()
        If Form_Main.StopProcess Then
            Return False
        End If

        Extension = System.IO.Path.GetExtension(FoundFile)

        If Not Extension = ".dft" Then
            GotAMatch = ProcessProperties(FoundFile)
        Else
            If FMain.PropertyFilterIncludeDraftItself Then
                GotAMatch = ProcessProperties(FoundFile)
            Else
                GotAMatch = False
            End If

            If FMain.PropertyFilterIncludeDraftModel Then
                Try
                    SSDoc = New HCStructuredStorageDoc(FoundFile)
                    SSDoc.ReadLinks(FMain.LinkManagementOrder)

                    For Each LinkFilename As String In SSDoc.GetLinkNames
                        If ModelExtensions.Contains(System.IO.Path.GetExtension(LinkFilename)) Then
                            If IO.File.Exists(LinkFilename) Then
                                GotAMatch = GotAMatch Or ProcessFile(LinkFilename)
                            End If
                        End If
                    Next

                    SSDoc.Close()
                Catch ex As Exception
                    If SSDoc IsNot Nothing Then SSDoc.Close()
                    GotAMatch = False
                End Try

            End If
        End If

        Return GotAMatch

    End Function

    Public Function ProcessProperties(
        FoundFile As String
        ) As Boolean

        Dim GotAMatch As Boolean
        Dim Variable As String
        Dim PropertySetName As String = ""
        Dim PropertyName As String
        Dim Comparison As String = ""
        Dim Value As String
        Dim DocValue As String
        Dim tf2 As Boolean
        Dim VariableTruthValues As New Dictionary(Of String, String)
        Dim BooleanExpression As String

        Dim SSDoc As HCStructuredStorageDoc = Nothing

        Dim UC As New UtilsCommon

        Try
            SSDoc = New HCStructuredStorageDoc(FoundFile)
            SSDoc.ReadProperties(FMain.PropertiesData)
            SSDoc.ReadLinks(FMain.LinkManagementOrder)
        Catch ex As Exception
            If SSDoc IsNot Nothing Then SSDoc.Close()
            Return False
        End Try

        For Each Condition As PropertyFilterCondition In Me.PropertyFilter.Conditions

            Variable = Condition.VariableName

            Select Case Condition.PropertySetName
                Case PropertyFilterCondition.PropertySetNameConstants.Custom
                    PropertySetName = "Custom"
                Case PropertyFilterCondition.PropertySetNameConstants.System
                    PropertySetName = "System"
            End Select

            PropertyName = Condition.PropertyName

            Select Case Condition.Comparison
                Case PropertyFilterCondition.ComparisonConstants.Contains
                    Comparison = "contains"
                Case PropertyFilterCondition.ComparisonConstants.IsExactly
                    Comparison = "is_exactly"
                Case PropertyFilterCondition.ComparisonConstants.WildcardMatch
                    Comparison = "wildcard_match"
                Case PropertyFilterCondition.ComparisonConstants.RegexMatch
                    Comparison = "regex_match"
                Case PropertyFilterCondition.ComparisonConstants.GreaterThan
                    Comparison = ">"
                Case PropertyFilterCondition.ComparisonConstants.LessThan
                    Comparison = "<"
            End Select

            Value = Condition.Value

            Value = SSDoc.SubstitutePropertyFormulas(Value, ValidFilenameRequired:=False)

            If Value Is Nothing Then
                SSDoc.Close()
                Return False
            End If

            DocValue = CStr(SSDoc.GetPropValue(PropertySetName, PropertyName))

            If DocValue Is Nothing Then
                'DocValue = Me.PropNotFoundString

                ''DocValue Is Nothing' occurs when the property doesn't exist in the file.
                'Comparing Me.PropNotFoundString to any real value will become FALSE.
                'With a negated formula, like 'NOT ( A )', the evaluation will be TRUE.
                'Not sure about the comparison 'is_not', which is itself negated.
                'For now it seems better to give up on the file and say no match.

                Return False
            End If

            tf2 = DoComparison(Comparison, Value, DocValue)

            VariableTruthValues.Add(Variable, tf2.ToString)

        Next

        SSDoc.Close()

        BooleanExpression = DoSubstitution(Me.PropertyFilter.Formula, VariableTruthValues)

        GotAMatch = EvaluateBoolean(BooleanExpression)

        Return GotAMatch

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

        'If Not DocValue = Me.PropNotFoundString Then  ' Can't match a prop that doesn't exist
        'End If

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
        ' If that fails, Convert.ToDateTime raises a FormatException.

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
