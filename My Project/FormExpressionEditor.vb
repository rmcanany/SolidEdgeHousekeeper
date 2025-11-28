Option Strict On

Imports System.Text.RegularExpressions
'Imports System.Windows.Media.Media3D
Imports FastColoredTextBoxNS
Imports PanoramicData.NCalcExtensions
Imports Microsoft.WindowsAPICodePack.Dialogs

Public Class FormExpressionEditor

    Public Formula As String

    Private _OutputType As String
    Public Property OutputType As String
        Get
            Return _OutputType
        End Get
        Set(value As String)
            _OutputType = value
            If Me.TextEditorFormula IsNot Nothing Then
                If value = "Snippet" Then
                    ComboBoxLanguage.Enabled = False
                    DD_SavedExpressions.Visible = False
                    BT_Delete.Visible = False
                    ButtonOpen.Visible = True
                    BT_Test.Visible = False
                Else
                    ComboBoxLanguage.Enabled = True
                    DD_SavedExpressions.Visible = True
                    BT_Delete.Visible = True
                    ButtonOpen.Visible = False
                    BT_Test.Visible = True
                End If
            End If
        End Set
    End Property

    Private _SnippetFilename As String
    Public Property SnippetFilename As String
        Get
            Return _SnippetFilename
        End Get
        Set(value As String)
            _SnippetFilename = value
            'If Me.TabControl1 IsNot Nothing Then
            '    TextBoxSnippetFilename.Text = value
            'End If
        End Set
    End Property

    Dim SavedExpressionsItems As New Dictionary(Of String, String)
    Dim SavedParameters As New Dictionary(Of String, String)

    Dim popupMenu As FastColoredTextBoxNS.AutocompleteMenu

    Dim ParametersStyle As FastColoredTextBoxNS.TextStyle = New FastColoredTextBoxNS.TextStyle(Brushes.MediumBlue, Nothing, FontStyle.Regular)
    Dim CommandsStyle As FastColoredTextBoxNS.TextStyle = New FastColoredTextBoxNS.TextStyle(Brushes.SaddleBrown, Nothing, FontStyle.Regular)
    Dim CommentsStyle As FastColoredTextBoxNS.TextStyle = New FastColoredTextBoxNS.TextStyle(Brushes.Green, Nothing, FontStyle.Italic)

    Dim CommandsList As String = "\b("

    Dim CurrentExpression As String


    Public Sub New()

        ' FastColoredTextBox
        ' https://github.com/PavelTorgashov/FastColoredTextBox/tree/master

        ' NCalcExtension
        ' https://github.com/panoramicdata/PanoramicData.NCalcExtensions

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        popupMenu = New FastColoredTextBoxNS.AutocompleteMenu(TextEditorFormula)

        popupMenu.MinFragmentLength = 2

        Dim NCalcExtensionsWords As List(Of String) = New List(Of String) From {"all()", "any()", "canEvaluate()", "capitalize()", "cast()", "changeTimeZone()",
            "concat()", "contains()", "convert()", "count()", "countBy()", "dateAdd()", "dateTime()", "dateTimeAsEpoch()", "dateTimeAsEpochMs()", "dictionary()",
            "distinct()", "endsWith()", "extend()", "first()", "firstOrDefault()", "format()", "getProperties()", "getProperty()", "humanize()", "if()", "in()",
            "indexOf()", "isGuid()", "isInfinite()", "isNaN()", "isNull()", "isNullOrEmpty()", "isNullOrWhiteSpace()", "isSet()", "itemAtIndex()", "jObject()",
            "join()", "jPath()", "last()", "lastIndexOf()", "lastOrDefault()", "length()", "list()", "listOf()", "max()", "maxValue()", "min()", "minValue()",
            "nullCoalesce()", "orderBy()", "padLeft()", "parse()", "parseInt()", "regexGroup()", "regexIsMatch()", "replace()", "retrieve", "reverse()", "sanitize()",
            "select()", "selectDistinct()", "setProperties()", "skip()", "Sort()", "Split()", "startsWith()", "store()", "substring()", "sum()", "switch()", "take()",
            "throw()", "timeSpan()", "toDateTime()", "toLower()", "toString()", "toUpper()", "try()", "tryParse()", "typeOf()", "where()"
            }

        For Each item In NCalcExtensionsWords
            CommandsList = CommandsList & item.Replace("()", "") & "|"
        Next
        CommandsList = CommandsList.Remove(CommandsList.LastIndexOf("|")) & ")\b"

        Dim SolidEdgeProperties As List(Of String) = New List(Of String) From {"System", "System.Title", "System.Subject", "System.Author", "System.Keywords", "System.Comments",
            "System.Template", "System.LastAuthor", "System.RevNumber", "System.EditTime", "System.LastPrinted", "System.Create_DTM", "System.LastSave_DTM", "System.PageCount",
            "System.WordCount", "System.CharCount", "System.AppName", "System.Doc_Security", "System.Document Number", "System.Revision", "System.Name", "Custom"}

        popupMenu.SearchPattern = "[\w\.]"

        Dim items = New List(Of AutocompleteItem)()

        For Each Item In SolidEdgeProperties
            items.Add(New MethodAutocompleteItem2(Item))
        Next

        For Each item In NCalcExtensionsWords
            items.Add(New AutocompleteItem(item))
        Next

        popupMenu.Items.SetAutocompleteItems(items)

        'TextEditorFormula.Language = CType(5, Language)
        TextEditorFormula.Language = FastColoredTextBoxNS.Language.SQL  ' Can change this as needed after instantiating the object.

        OutputType = "Expression"
        SnippetFilename = ""

    End Sub


    Private Sub SaveExpressionItem(ExpressionName As String, Overwrite As Boolean)

        Dim tmpExpressionsText As String = ""

        If Overwrite Then

            SavedExpressionsItems.Item(ExpressionName) = TextEditorFormula.Text

        Else

            DD_SavedExpressions.DropDownItems.Add(ExpressionName.Replace(vbCrLf, ""))
            SavedExpressionsItems.Add(ExpressionName.Replace(vbCrLf, ""), TextEditorFormula.Text)

        End If

        For Each item As ToolStripDropDownItem In DD_SavedExpressions.DropDownItems

            tmpExpressionsText = tmpExpressionsText & "[EXP]" & vbCrLf & item.Text & vbCrLf & "[EXP_TEXT]" & vbCrLf & SavedExpressionsItems(item.Text) & vbCrLf

        Next

        Dim UP As New UtilsPreferences
        Dim PreferencesDirectory = UP.GetPreferencesDirectory()

        Dim SavedExpressionsFilename = UP.GetSavedExpressionsFilename()

        IO.File.WriteAllText(SavedExpressionsFilename, tmpExpressionsText)

    End Sub

    Private Sub DeleteExpressionItem(ExpressionName As String)

        Dim tmpExpressionsText As String = ""

        DD_SavedExpressions.DropDownItems.RemoveByKey(ExpressionName)
        For Each item As ToolStripDropDownItem In DD_SavedExpressions.DropDownItems
            If item.Text = ExpressionName Then
                DD_SavedExpressions.DropDownItems.Remove(item)
                Exit For
            End If
        Next


        SavedExpressionsItems.Remove(ExpressionName)

        For Each item As ToolStripDropDownItem In DD_SavedExpressions.DropDownItems

            tmpExpressionsText = tmpExpressionsText & "[EXP]" & vbCrLf & item.Text & vbCrLf & "[EXP_TEXT]" & vbCrLf & SavedExpressionsItems(item.Text) & vbCrLf

        Next

        Dim UP As New UtilsPreferences

        IO.File.WriteAllText(UP.GetSavedExpressionsFilename, tmpExpressionsText)

        TextEditorFormula.Clear()
        CurrentExpression = ""
        Me.Text = "Expression editor"

        DD_SavedExpressions.DropDownItems.Item(0).PerformClick()

    End Sub



    Private Sub FormNCalc_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        Formula = TextEditorFormula.Text

    End Sub

    Private Sub BT_Test_Click(sender As Object, e As EventArgs) Handles BT_Test.Click, BT_TestOnCurrentFile.Click

        If Me.OutputType = "Snippet" Then

            Dim UPS As New UtilsPowerShell
            Dim PowershellFilename = UPS.BuildSnippetFile(Me.SnippetFilename)

            Dim P As New Diagnostics.Process
            Dim ExitCode As Integer

            Dim PSError As String = ""

            P.StartInfo.FileName = "powershell.exe"
            P.StartInfo.Arguments = String.Format("-command {1}{0}{1}", PowershellFilename.Replace(" ", "` "), Chr(34))
            P.StartInfo.RedirectStandardError = True
            P.StartInfo.UseShellExecute = False
            'If Me.HideConsoleWindow Then P.StartInfo.CreateNoWindow = True
            P.Start()
            PSError = P.StandardError.ReadToEnd

            P.WaitForExit()
            ExitCode = P.ExitCode

            Dim ResultsMessage As String = $"Exit Code: {P.ExitCode}{vbCrLf}"
            If Not PSError = "" Then
                ResultsMessage = $"{ResultsMessage}Errors reported{vbCrLf}"
                ResultsMessage = $"{ResultsMessage}{PSError}"
            End If

            Dim ErrorMessageFilename = String.Format("{0}\error_messages.txt", IO.Path.GetDirectoryName(PowershellFilename))

            If IO.File.Exists(PowershellFilename) Then IO.File.Delete(PowershellFilename)

            If ExitCode <> 0 Then
                If FileIO.FileSystem.FileExists(ErrorMessageFilename) Then
                    Dim ErrorMessages As List(Of String) = IO.File.ReadAllLines(ErrorMessageFilename).ToList
                    If ErrorMessages.Count > 0 Then
                        For Each ErrorMessageFromProgram As String In ErrorMessages
                            ResultsMessage = $"{ResultsMessage}{vbCrLf}{ErrorMessageFromProgram}"
                        Next
                    End If

                    IO.File.Delete(ErrorMessageFilename)
                End If
            End If

            TextEditorResults.Text = ResultsMessage


        Else
            Dim calculation As String = TextEditorFormula.Text

            Dim Pattern As String = "%{[^}]*}"
            Dim Matches As MatchCollection = Regex.Matches(calculation, Pattern)
            Dim Parameters As New List(Of String)

            For Each MatchString As Match In Matches
                If Not Parameters.Contains(MatchString.Value) Then Parameters.Add(MatchString.Value)
            Next

            Dim UC As New UtilsCommon
            Dim UP As New UtilsPreferences

            For Each Parameter In Parameters

                Dim tmpVal As String = ""

                If SavedParameters.ContainsKey(Parameter) Then
                    tmpVal = SavedParameters.Item(Parameter)
                End If


                If sender.Equals(BT_Test) Then

                    tmpVal = InputBox("Insert value for parameter: " & Parameter,, tmpVal)

                Else

                    ' Test on current file
                    ' This a concept and must be implemented properly.
                    ' I wrote this code RAW and is not perfect; I always forget how to use the proper method you have developed in UtilsCommon.vb please replace it with the proper one.
                    ' F.Arfilli

                    Dim USEA As New UtilsSEApp(Form_Main)

                    Dim SEApp As SolidEdgeFramework.Application
                    Dim SEDoc As SolidEdgeFramework.SolidEdgeDocument

                    Try
                        USEA.SEStart(RunInBackground:=False, UseCurrentSession:=True, NoUpdateMRU:=True, ProcessDraftsInactive:=False)
                        SEApp = USEA.SEApp
                        SEDoc = CType(SEApp.ActiveDocument, SolidEdgeFramework.SolidEdgeDocument)
                    Catch ex As Exception
                        MsgBox("Error connecting to Solid Edge or no document open.", MsgBoxStyle.Exclamation, "Error")
                        Exit Sub
                    End Try

                    Dim PropertySetName = UC.PropSetFromFormula(Parameter)
                    Dim PropertyName = UC.PropNameFromFormula(Parameter)
                    Dim ModelIdx = UC.ModelIdxFromFormula(Parameter)

                    Dim tmpObj = UC.GetPropValue(SEDoc, PropertySetName, PropertyName, ModelIdx, AddProp:=False)
                    If tmpObj IsNot Nothing Then
                        tmpVal = tmpObj.ToString
                    Else
                        'tmpVal = "Property not found."
                        tmpVal = "<Nothing>"
                    End If
                End If

                If SavedParameters.ContainsKey(Parameter) Then
                    SavedParameters.Item(Parameter) = tmpVal
                Else
                    SavedParameters.Add(Parameter, tmpVal)
                End If

                calculation = calculation.Replace(Parameter, tmpVal)
                calculation = calculation.Split(CChar("\\")).First

            Next

            Dim Success As Boolean = True
            Dim nCalcExpression As ExtendedExpression = Nothing
            Dim ExpressionResult As String = ""

            If TextEditorFormula.Language = FastColoredTextBoxNS.Language.SQL Then
                Try
                    nCalcExpression = New ExtendedExpression(calculation)
                    ExpressionResult = CStr(nCalcExpression.Evaluate())
                Catch ex As Exception
                    Success = False
                    TextEditorResults.Clear()
                    TextEditorResults.Text = ex.Message
                End Try

            ElseIf TextEditorFormula.Language = FastColoredTextBoxNS.Language.VB Then
                Dim UPS As New UtilsPowerShell
                Dim PowerShellFileContents As List(Of String) = UPS.BuildExpressionFile(calculation.Split(CChar(vbCrLf)).ToList)

                Dim PowerShellFilename As String = $"{UP.GetTempDirectory}\HousekeeperExpression.ps1"

                'IO.File.WriteAllLines(PowerShellFilename, PowerShellFileContents, System.Text.Encoding.Unicode)

                'Try
                '    ExpressionResult = UPS.RunExpressionScript(PowerShellFilename)
                'Catch ex As Exception
                '    Success = False
                '    TextEditorResults.Clear()
                '    TextEditorResults.Text = ex.Message
                'End Try

                Dim tmpSuccess As Boolean = UPS.WriteExpressionFile(PowerShellFilename, PowerShellFileContents)

                If tmpSuccess Then
                    Try
                        ExpressionResult = UPS.RunExpressionScript(PowerShellFilename)
                    Catch ex As Exception
                        Success = False
                        TextEditorResults.Clear()
                        TextEditorResults.Text = ex.Message
                    End Try
                Else
                    Success = False
                End If
            End If

            If Success Then
                Dim p As String = vbCrLf & vbCrLf & "Parameters list" & vbCrLf & "---------------"

                For Each tmpPar In Parameters

                    p += vbCrLf & tmpPar & ": " & SavedParameters(tmpPar)

                Next

                TextEditorResults.Clear()
                TextEditorResults.Text = "Expression result: " & ExpressionResult & p

            End If
        End If

    End Sub

    Private Sub FormNCalc_Load(sender As Object, e As EventArgs) Handles Me.Load

        TextEditorFormula.WordWrap = True
        TextEditorResults.WordWrap = True

        Dim UP As New UtilsPreferences
        Dim PreferencesDirectory = UP.GetPreferencesDirectory()

        Dim SavedExpressionsFilename = UP.GetSavedExpressionsFilename()

        Dim SR As IO.StreamReader = IO.File.OpenText(SavedExpressionsFilename)
        Dim SavedExpressions = SR.ReadToEnd

        Dim Expressions = SavedExpressions.Split(New String() {"[EXP]"}, StringSplitOptions.RemoveEmptyEntries)

        For Each Expression In Expressions

            Dim ExpressionItems = Expression.Split(New String() {"[EXP_TEXT]"}, StringSplitOptions.RemoveEmptyEntries)
            If ExpressionItems.Length = 2 Then
                DD_SavedExpressions.DropDownItems.Add(ExpressionItems(0).Replace(vbCrLf, ""))
                SavedExpressionsItems.Add(ExpressionItems(0).Replace(vbCrLf, ""), ExpressionItems(1))
            End If
        Next

        SR.Close()

        If TextEditorFormula.Language = FastColoredTextBoxNS.Language.VB Then
            ComboBoxLanguage.Text = "VB"
        Else
            ComboBoxLanguage.Text = "NCalc"
        End If

        Dim tmpFormula As String = Formula
        tmpFormula = tmpFormula.Replace("EXPRESSION_VB", "")
        tmpFormula = tmpFormula.Replace("EXPRESSION_NCalc", "")
        tmpFormula = tmpFormula.Replace(Chr(182), vbCrLf)

        If Not tmpFormula = "" Then
            While tmpFormula(0) = vbCrLf Or tmpFormula(0) = vbCr Or tmpFormula(0) = vbLf
                tmpFormula = tmpFormula.Substring(1)
            End While
        End If

        TextEditorFormula.Text = tmpFormula

    End Sub

    Private Sub DD_SavedExpressions_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles DD_SavedExpressions.DropDownItemClicked

        Dim tmpItem As ToolStripItem = e.ClickedItem

        TextEditorFormula.Clear()
        TextEditorFormula.Text = SavedExpressionsItems.Item(tmpItem.Text)
        CurrentExpression = tmpItem.Text
        Me.Text = "Expression editor - " & CurrentExpression

        If TextEditorFormula.Text.ToLower.Contains("return") Then
            TextEditorFormula.Language = FastColoredTextBoxNS.Language.VB
            ComboBoxLanguage.Text = "VB"
        Else
            TextEditorFormula.Language = FastColoredTextBoxNS.Language.SQL
            ComboBoxLanguage.Text = "NCalc"
        End If

    End Sub

    Private Sub BT_Help_Click(sender As Object, e As EventArgs) Handles BT_Help.Click

        Dim webAddress As String = "https://github.com/panoramicdata/PanoramicData.NCalcExtensions"
        Process.Start(webAddress)

    End Sub

    Private Sub TextEditorFormula_TextChanged(sender As Object, e As TextChangedEventArgs) Handles TextEditorFormula.TextChanged
        Try

            e.ChangedRange.SetStyle(Me.CommentsStyle, "\\((.|\n)*)", RegexOptions.Multiline)
            e.ChangedRange.ClearStyle(New Style() {Me.ParametersStyle})
            e.ChangedRange.SetStyle(Me.ParametersStyle, "'%{[^}']+}'")
            e.ChangedRange.SetStyle(Me.CommandsStyle, CommandsList)

        Catch ex As Exception

        End Try


    End Sub

    Private Sub BT_Delete_Click(sender As Object, e As EventArgs) Handles BT_Delete.Click

        Dim A = InputBox("Expression to delete ?", "Delete expression", CurrentExpression)

        If A <> "" Then

            If SavedExpressionsItems.ContainsKey(A) Then

                Dim B = MsgBox("Delete expression " & A & " ?", vbYesNoCancel, "Delete expression")

                Select Case B
                    Case = MsgBoxResult.Cancel
                        Exit Sub
                    Case = MsgBoxResult.No
                        BT_Delete_Click(sender, e)
                    Case = MsgBoxResult.Yes
                        DeleteExpressionItem(A)
                End Select

            Else

                MsgBox("Expression " & A & " not found.", MsgBoxStyle.Exclamation, "Delete expression")

            End If

        End If

    End Sub

    Private Sub BT_Save_Click(sender As Object, e As EventArgs) Handles BT_Save.Click

        If Me.OutputType = "Expression" Then
            If CurrentExpression <> "" Then
                SaveExpressionItem(CurrentExpression, True)
            Else
                BT_SaveAs_Click(sender, e)
            End If
        Else
            'MsgBox($"{OutputType}")
            'Dim tmpFilename As String = Me.SnippetFilename.Replace(".snp", "- Copy.snp")
            If Not IO.File.Exists(Me.SnippetFilename) Then
                BT_SaveAs.PerformClick()
            Else
                System.IO.File.WriteAllText(Me.SnippetFilename, TextEditorFormula.Text)
                Formula = TextEditorFormula.Text
            End If
        End If

    End Sub

    Private Sub BT_Clear_Click(sender As Object, e As EventArgs) Handles BT_Clear.Click
        TextEditorFormula.Clear()
        CurrentExpression = ""
        Me.Text = "Expression editor"
    End Sub

    Private Sub BT_SaveAs_Click(sender As Object, e As EventArgs) Handles BT_SaveAs.Click

        If Me.OutputType = "Expression" Then
            Dim A = InputBox("Expression name ?", "Save expression", CurrentExpression)

            If A <> "" Then

                If SavedExpressionsItems.ContainsKey(A) Then

                    Dim B = MsgBox("Overwrite expression " & A & " ?", vbYesNoCancel, "Save expression")

                    Select Case B
                        Case = MsgBoxResult.Cancel
                            Exit Sub
                        Case = MsgBoxResult.No
                            BT_SaveAs_Click(sender, e)
                        Case = MsgBoxResult.Yes
                            SaveExpressionItem(A, True)
                    End Select

                Else
                    SaveExpressionItem(A, False)
                End If

                CurrentExpression = A
                Me.Text = "Expression editor - " & CurrentExpression

            End If
        Else
            'MsgBox($"{OutputType}")
            Dim tmpFileDialog As New CommonSaveFileDialog

            tmpFileDialog.Title = "Enter the file name for the new part"
            tmpFileDialog.DefaultFileName = IO.Path.GetFileName(Me.SnippetFilename)
            tmpFileDialog.EnsureFileExists = False
            tmpFileDialog.Filters.Add(New CommonFileDialogFilter("Solid Edge Files", "*.snp"))
            If IO.File.Exists(Me.SnippetFilename) Then
                tmpFileDialog.InitialDirectory = IO.Path.GetDirectoryName(Me.SnippetFilename)
            End If

            If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                Me.SnippetFilename = tmpFileDialog.FileName
                System.IO.File.WriteAllText(Me.SnippetFilename, TextEditorFormula.Text)
                Formula = TextEditorFormula.Text
            End If

        End If

    End Sub

    Private Sub BT_InsertProp_Click(sender As Object, e As EventArgs) Handles BT_InsertProp.Click
        'TextEditorFormula.SelectedText = """%{}"""
        'TextEditorFormula.SelectionStart -= 2

        Dim CaretPosition = TextEditorFormula.SelectionStart

        Dim FPP As New FormPropertyPicker

        FPP.ShowDialog()

        If FPP.DialogResult = DialogResult.OK Then
            Dim PropString As String = FPP.PropertyString
            Select Case Me.TextEditorFormula.Language
                Case FastColoredTextBoxNS.Language.VB
                    PropString = $"""{PropString}"""
                Case FastColoredTextBoxNS.Language.SQL
                    PropString = $"'{PropString}'"
            End Select

            TextEditorFormula.Text = TextEditorFormula.Text.Insert(CaretPosition, PropString)
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        If Me.OutputType = "Snippet" And Not Formula = TextEditorFormula.Text Then
            MsgBox("Text has changed.  Please save the file before proceeding.", vbOKOnly)
            Exit Sub
        End If

        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub ComboBoxLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxLanguage.SelectedIndexChanged
        Select Case ComboBoxLanguage.Text
            Case "VB"
                Me.TextEditorFormula.Language = FastColoredTextBoxNS.Language.VB
            Case "NCalc"
                Me.TextEditorFormula.Language = FastColoredTextBoxNS.Language.SQL
        End Select
    End Sub

    Private Sub ButtonOpen_Click(sender As Object, e As EventArgs) Handles ButtonOpen.Click
        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Title = "Select a code snippet file"
        tmpFileDialog.Filter = "Snippets|*.snp"

        'If IO.File.Exists(Me.ExternalProgram) Then
        '    tmpFileDialog.InitialDirectory = IO.Path.GetDirectoryName(Me.ExternalProgram)
        'Else
        '    tmpFileDialog.InitialDirectory = Form_Main.WorkingFilesPath
        'End If

        If tmpFileDialog.ShowDialog() = DialogResult.OK Then
            Me.SnippetFilename = tmpFileDialog.FileName

            Dim tmpFormula As String = ""
            Dim InFile As List(Of String) = System.IO.File.ReadAllLines(Me.SnippetFilename).ToList
            For Each Line As String In InFile
                tmpFormula = $"{tmpFormula}{Line}{vbCrLf}"
            Next
            Formula = tmpFormula
            Me.TextEditorFormula.Text = tmpFormula
        End If

    End Sub
End Class

Public Class MethodAutocompleteItem2
    Inherits MethodAutocompleteItem

    Private firstPart As String
    Private lastPart As String

    Public Sub New(ByVal text As String)
        MyBase.New(text)
        Dim i = text.LastIndexOf("."c)

        If i < 0 Then
            firstPart = text
        Else
            firstPart = text.Substring(0, i)
            lastPart = text.Substring(i + 1)
        End If
    End Sub

    Public Overrides Function Compare(ByVal fragmentText As String) As CompareResult
        Dim i As Integer = fragmentText.LastIndexOf("."c)

        If i < 0 Then
            If firstPart.StartsWith(fragmentText) AndAlso String.IsNullOrEmpty(lastPart) Then Return CompareResult.VisibleAndSelected
        Else
            Dim fragmentFirstPart = fragmentText.Substring(0, i)
            Dim fragmentLastPart = fragmentText.Substring(i + 1)
            If firstPart <> fragmentFirstPart Then Return CompareResult.Hidden
            If lastPart IsNot Nothing AndAlso lastPart.StartsWith(fragmentLastPart) Then Return CompareResult.VisibleAndSelected
            If lastPart IsNot Nothing AndAlso lastPart.ToLower().Contains(fragmentLastPart.ToLower()) Then Return CompareResult.Visible
        End If

        Return CompareResult.Hidden
    End Function

    Public Overrides Function GetTextForReplace() As String
        If lastPart Is Nothing Then Return firstPart
        Return firstPart & "." & lastPart
    End Function

    Public Overrides Function ToString() As String
        If lastPart Is Nothing Then Return firstPart
        Return lastPart
    End Function
End Class
