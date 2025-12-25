Option Strict On
Imports Newtonsoft.Json

Public Class HCSavedExpressions
    Public Property Items As List(Of SavedExpression)

    Public Sub New()
        Me.Items = New List(Of SavedExpression)
        Populate()

    End Sub

    Private Sub Populate()
        Dim UP As New UtilsPreferences

        Dim SavedExpressionsFilename As String = UP.GetSavedExpressionsFilename(CheckExisting:=True)

        If Not SavedExpressionsFilename = "" Then
            Dim JSONString As String = IO.File.ReadAllText(SavedExpressionsFilename)
            Dim JSONList As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(JSONString)

            Me.Items.Clear()
            For Each tmpJSONString As String In JSONList
                Me.Items.Add(New SavedExpression(tmpJSONString))
            Next

        Else
            PopulateFromTextFile(IO.Path.ChangeExtension(UP.GetSavedExpressionsFilename(CheckExisting:=False), ".txt"))
        End If


    End Sub

    Private Sub PopulateFromTextFile(Filename As String)

        If Not IO.File.Exists(Filename) Then Exit Sub

        Dim UP As New UtilsPreferences

        '[EXP]
        'Example toUpper()
        '[EXP_TEXT]
        'toUpper('%{System.Title}')
        '\\Any text will be converted in UPPERCASE
        '[EXP]
        'Example VB ToUpper
        '[EXP_TEXT]
        'Return "%{System.Title}".ToUpper()
        '\\Any text will be converted in UPPERCASE
        '...

        Dim NewWay As Boolean = True
        If NewWay Then

            Dim InList As List(Of String) = IO.File.ReadAllLines(Filename).ToList

            Dim ItemsList As New List(Of List(Of String))
            Dim ItemList As List(Of String) = Nothing

            For Each Line In InList
                If Line.StartsWith("[EXP]") Then
                    ItemList = New List(Of String)
                    ItemsList.Add(ItemList)
                    Continue For
                End If
                ItemList.Add(Line)
            Next

            Me.Items.Clear()

            For Each tmpItemList As List(Of String) In ItemsList
                Dim tmpName As String = ""
                Dim CommentList As List(Of String) = Nothing
                Dim ExpressionList As List(Of String) = Nothing
                Dim InCodeSection As Boolean = False
                Dim Language As String = "NCalc"

                For i = 0 To tmpItemList.Count - 1
                    Dim Line = tmpItemList(i)
                    If i = 0 Then
                        tmpName = Line
                        Continue For
                    End If
                    If Line.StartsWith("[EXP_TEXT]") Then
                        CommentList = New List(Of String)
                        ExpressionList = New List(Of String)
                        InCodeSection = True
                        Continue For
                    End If
                    If InCodeSection Then
                        If Line.StartsWith("\\") Then
                            CommentList.Add(Line.Replace("\\", "").Trim)
                        Else
                            ExpressionList.Add(Line)
                            If Line.ToLower.Contains("return") Then Language = "VB"
                        End If
                    End If
                Next

                Dim tmpSE As New SavedExpression
                tmpSE.Name = tmpName
                tmpSE.Language = Language
                tmpSE.Comments = CommentList

                Dim ExpressionString As String = ListOfStringToString(ExpressionList)
                ExpressionString = ExpressionString.Trim()
                ExpressionList = StringToListOfString(ExpressionString)

                tmpSE.Expression = ExpressionList

                Me.Items.Add(tmpSE)

            Next

            Save()

        Else
            'Dim SavedExpressionsDict As New Dictionary(Of String, Dictionary(Of String, String))

            'Dim SR As IO.StreamReader = IO.File.OpenText(Filename)
            'Dim SavedExpressions = SR.ReadToEnd

            'SR.Close()

            'SavedExpressions = SavedExpressions.Replace(vbLf, Chr(182)).Replace(vbCr, Chr(182))
            'SavedExpressions = SavedExpressions.Replace(Chr(182), vbCrLf)

            'Dim Expressions = SavedExpressions.Split(New String() {"[EXP]"}, StringSplitOptions.RemoveEmptyEntries)

            'For Each Expression In Expressions

            '    Dim ExpressionItems = Expression.Split(New String() {"[EXP_TEXT]"}, StringSplitOptions.RemoveEmptyEntries)

            '    If ExpressionItems.Length = 2 Then

            '        Dim SaveName As String = ExpressionItems(0).Replace(vbCrLf, "")
            '        'Dim ExpressionAndComments = ExpressionItems(1).Split(CType("\\", Char)).First
            '        Dim ExpressionAndComments = ExpressionItems(1).Split(CType("\\", Char))
            '        Dim SaveExpression As String = UP.TrimCR(ExpressionAndComments(0))
            '        Dim SaveComments As String = ""
            '        If ExpressionAndComments.Count > 1 Then
            '            For i As Integer = 1 To ExpressionAndComments.Count - 1
            '                SaveComments = $"{SaveComments}{ExpressionAndComments(i)}{vbCrLf}"
            '            Next
            '        End If
            '        SaveComments = UP.TrimCR(SaveComments)
            '        Dim SaveLanguage As String = ""
            '        If SaveExpression.ToLower.Contains("return") Then
            '            SaveLanguage = "VB"
            '        Else
            '            SaveLanguage = "NCalc"
            '        End If

            '        SavedExpressionsDict(SaveName) = New Dictionary(Of String, String)
            '        SavedExpressionsDict(SaveName)("Language") = SaveLanguage
            '        SavedExpressionsDict(SaveName)("Comments") = SaveComments
            '        SavedExpressionsDict(SaveName)("Expression") = SaveExpression
            '    End If
            'Next

            'Me.Items.Clear()

            'For Each Name As String In SavedExpressionsDict.Keys
            '    Me.Items.Add(New SavedExpression)
            '    Dim tmpSE As SavedExpression = Me.Items(Me.Items.Count - 1)
            '    tmpSE.Name = Name
            '    tmpSE.Language = SavedExpressionsDict(Name)("Language")
            '    'tmpSE.Comments = SavedExpressionsDict(Name)("Comments").Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries).ToList
            '    'tmpSE.Expression = SavedExpressionsDict(Name)("Expression").Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries).ToList
            '    tmpSE.Comments = SavedExpressionsDict(Name)("Comments").Split(CType(vbCrLf, Char)).ToList
            '    tmpSE.Expression = SavedExpressionsDict(Name)("Expression").Split(CType(vbCrLf, Char)).ToList
            '    Dim i = 0
            'Next

            'Save()
        End If

    End Sub

    Public Function GetSavedExpression(Name As String) As SavedExpression
        Dim SE As SavedExpression = Nothing

        For Each E As SavedExpression In Me.Items
            If E.Name = Name Then
                SE = E
                Exit For
            End If
        Next

        Return SE
    End Function

    Public Function GetSavedExpressionFromShorthandText(ShorthandText As String) As SavedExpression
        ' ShorthandText examples
        ' SavedSetting:StdNummer
        ' EXPRESSION_VB¶Return "%{System.Title}".ToUpper¶

        Dim SE As SavedExpression = Nothing
        Dim Name As String

        If ShorthandText.StartsWith("SavedSetting:") Then
            Name = ShorthandText.Replace("SavedSetting:", "")
            SE = GetSavedExpression(Name)
            If SE Is Nothing Then
                MsgBox($"Expression name not recognized: '{Name}'")
            End If
        End If

        If ShorthandText.StartsWith("EXPRESSION_") Then
            Dim tmpFormula As String = ShorthandText

            tmpFormula = tmpFormula.Replace($"EXPRESSION_VB{Chr(182)}", "")
            tmpFormula = tmpFormula.Replace($"EXPRESSION_NCalc{Chr(182)}", "")
            tmpFormula = tmpFormula.Replace(Chr(182), vbCrLf)

            If Not tmpFormula = "" Then
                For Each tmpSE As SavedExpression In Me.Items
                    Dim tmpExpression As String = ""
                    For Each Line As String In tmpSE.Expression
                        tmpExpression = $"{tmpExpression}{Line}{vbCrLf}"
                    Next

                    Dim tmptmpFormula = tmpFormula.ToLower.Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "")
                    Dim tmptmpExpression = tmpExpression.ToLower.Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "")
                    If tmptmpFormula.Contains(tmptmpExpression) Or tmptmpExpression.Contains(tmptmpFormula) Then
                        SE = tmpSE
                        Exit For
                    End If

                Next
            End If
        End If

        Return SE
    End Function

    Public Sub Save()
        Dim JSONString As String
        Dim JSONList As New List(Of String)

        For Each SE As SavedExpression In Me.Items
            JSONList.Add(SE.ToJSON)
        Next

        JSONString = JsonConvert.SerializeObject(JSONList)

        Dim UP As New UtilsPreferences

        Dim SavedExpressionsFilename As String = UP.GetSavedExpressionsFilename(CheckExisting:=False)

        IO.File.WriteAllText(SavedExpressionsFilename, JSONString)

    End Sub

    Public Function ListOfStringToString(InList As List(Of String)) As String
        Dim OutString As String = ""
        For Each Line As String In InList
            OutString = $"{OutString}{Line}{vbCrLf}"
        Next
        Return OutString
    End Function

    Public Function StringToListOfString(InString As String) As List(Of String)
        'Dim tmpInString As String = InString.Replace(vbLf, vbCrLf).Replace(vbCr, vbCrLf)
        Dim tmpInString As String = InString.Replace(vbCrLf, Chr(182)).Replace(vbLf, Chr(182)).Replace(vbCr, Chr(182))
        Dim OutList As List(Of String)
        OutList = tmpInString.Split(Chr(182)).ToList
        If OutList Is Nothing Then OutList = New List(Of String)
        Return OutList
    End Function

    Public Sub DeleteSavedExpression(Name As String)
        Dim tmpItems As New List(Of SavedExpression)
        For Each SE As SavedExpression In Me.Items
            If Not Name = SE.Name Then
                tmpItems.Add(SE)
            End If
        Next
        Me.Items = tmpItems
    End Sub
End Class

Public Class SavedExpression
    Public Property Name As String
    Public Property Language As String
    Public Property Comments As List(Of String)
    Public Property Expression As List(Of String)

    Public Sub New()
        Me.Name = ""
        Me.Language = ""
        Me.Comments = New List(Of String)
        Me.Expression = New List(Of String)

    End Sub

    Public Sub New(JSONString As String)
        Dim JSONDict As Dictionary(Of String, String) = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

        Me.Name = JSONDict("Name")
        Me.Language = JSONDict("Language")
        Me.Comments = JsonConvert.DeserializeObject(Of List(Of String))(JSONDict("Comments"))
        Me.Expression = JsonConvert.DeserializeObject(Of List(Of String))(JSONDict("Expression"))

    End Sub

    Public Function ToJSON() As String
        ' JSONString = JsonConvert.SerializeObject(Me.SomeDict) Then

        ' SomeDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(JSONString)

        Dim JSONString As String = Nothing
        Dim JSONDict As New Dictionary(Of String, String)

        JSONDict("Name") = Me.Name
        JSONDict("Language") = Me.Language
        JSONDict("Comments") = JsonConvert.SerializeObject(Me.Comments)
        JSONDict("Expression") = JsonConvert.SerializeObject(Me.Expression)

        JSONString = JsonConvert.SerializeObject(JSONDict)

        Return JSONString
    End Function

End Class
