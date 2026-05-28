Option Strict On

Public Class UtilsPowerShell
    ' https://www.codeproject.com/Articles/18229/How-to-run-PowerShell-scripts-from-C

    'https://stackoverflow.com/questions/67321963/exception-has-occurred-clr-system-management-automation-runspaces-pssnapinexcep
    'Microsoft.PowerShell.SDK is a meta-package that pulls together all of the components of the PowerShell SDK into a
    'single NuGet package. A self-contained .NET application can use Microsoft.PowerShell.SDK to run arbitrary PowerShell
    'functionality without depending on any external PowerShell installations or libraries.
    'https://stackoverflow.com/questions/69530907/c-sharp-powershell-script-not-working-with-executionpolicy
    'If the current user's / machine's execution policy is controlled via GPOs (Group Policy Objects), you fundamentally
    'cannot override it programmatically (except via GPO changes).
    'Run Get-ExecutionPolicy -List to list policies defined for each available scope, in descending order of precedence.
    'If either the MachinePolicy or the UserPolicy scope have a value other than Undefined, then a GPO policy is in effect
    '(run Get-ExecutionPolicy without arguments to see the effective policy for the current session).

    Public Function RunScript(ByVal scriptText As String) As String

        Dim tmpAuthorizationManager As Management.Automation.AuthorizationManager = Nothing
        Dim AMChoice As Integer
        Dim results As Collections.ObjectModel.Collection(Of Management.Automation.PSObject) = Nothing

        Dim NewWay As Boolean = True

        If NewWay Then
            ''https://stackoverflow.com/questions/13420799/enabling-execution-policy-for-powershell-from-c-sharp
            Dim ISS As Management.Automation.Runspaces.InitialSessionState
            ISS = Management.Automation.Runspaces.InitialSessionState.CreateDefault()
            Select Case AMChoice
                Case 1 : tmpAuthorizationManager = New Management.Automation.AuthorizationManager("MyShellID") ' Probably need the actual ShellID
                Case 2 : tmpAuthorizationManager = New Management.Automation.AuthorizationManager("Microsoft.PowerShell")
                Case 3 : tmpAuthorizationManager = New Management.Automation.AuthorizationManager("Microsoft.PowerShell.Host")
                Case 4 : tmpAuthorizationManager = New Management.Automation.AuthorizationManager(Nothing)
            End Select
            ISS.AuthorizationManager = tmpAuthorizationManager

            Dim PS As Management.Automation.PowerShell
            PS = Management.Automation.PowerShell.Create(ISS)
            PS.AddScript(scriptText)
            PS.AddCommand("Out-String")

            results = PS.Invoke()

            If PS.HadErrors Then
                Dim ErrorMessages As New List(Of String)
                For Each E As Management.Automation.ErrorRecord In PS.Streams.Error
                    If Not ErrorMessages.Contains(E.Exception.Message) Then ErrorMessages.Add(E.Exception.Message)
                Next

                ' Clean up exception message formatting
                Dim s As String = ""
                For Each tmpError As String In ErrorMessages
                    tmpError = tmpError.Replace(vbCrLf, Chr(182)).Replace(vbLf, Chr(182)).Replace(vbCr, Chr(182))
                    Dim tmpList As List(Of String) = tmpError.Split(Chr(182)).ToList
                    For Each tmpS As String In tmpList
                        tmpS = tmpS.Trim
                        If Not tmpS = "" Then
                            If s = "" Then
                                s = $"    > {tmpS}"
                            Else
                                s = $"{s}{vbCrLf}    > {tmpS}"
                            End If
                        End If
                    Next
                Next
                s = $"Script error{vbCrLf}{s}"

                Throw New Exception(s)

            End If
        Else
            'Dim runspace As Management.Automation.Runspaces.Runspace
            'runspace = Management.Automation.Runspaces.RunspaceFactory.CreateRunspace()

            'runspace.Open()

            'Dim pipeline As Management.Automation.Runspaces.Pipeline = runspace.CreatePipeline()
            'pipeline.Commands.AddScript(scriptText)
            'pipeline.Commands.Add("Out-String")

            'results = pipeline.Invoke()
            'runspace.Close()
        End If

        Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()

        If results IsNot Nothing Then
            For Each obj As Management.Automation.PSObject In results
                stringBuilder.AppendLine(obj.ToString())
            Next
        End If

        Return stringBuilder.ToString()

    End Function

    Public Function BuildExpressionFile(Expression As List(Of String)) As List(Of String)
        Dim TopList As New List(Of String)
        Dim MidList As New List(Of String)
        Dim BotList As New List(Of String)
        Dim OutList As New List(Of String)
        Dim Indent As String = "        "

        ' Workaround for not being able to redefine a PS Type in the same session.
        ' Needed for serial runs of 'Test on edge' in the expression editor.
        Static Generator As System.Random = New System.Random()
        Dim RandomIdentifier As Integer = Generator.Next(10000, 99999)

        'TopList.Add("$InputEncoding = [Console]::InputEncoding = [Console]::OutputEncoding = New-Object System.Text.UTF8Encoding")

        TopList.Add("$Source = @""")
        TopList.Add("")
        TopList.Add("Imports System")
        TopList.Add("Imports System.Collections.Generic")
        TopList.Add("")
        'TopList.Add("Public Class Expression")
        TopList.Add(String.Format("Public Class Expression{0}", RandomIdentifier))
        TopList.Add("")
        TopList.Add("    Public Shared Function RunExpression() As String")
        TopList.Add("")

        For Each s In Expression
            MidList.Add($"{Indent}{s.Trim}")
        Next

        BotList.Add("    End Function")
        BotList.Add("End Class")
        BotList.Add("""@")
        BotList.Add("")

        '' https://stackoverflow.com/questions/3369662/can-you-remove-an-add-ed-type-in-powershell-again
        ''BotList.Add("$init = {Add-Type -TypeDefinition $Source -Language VisualBasic}")
        ''BotList.Add("$job = Start-Job -InitializationScript $init -ScriptBlock {")
        'BotList.Add("$job = Start-Job -ScriptBlock {")
        'BotList.Add("")
        'BotList.Add("    Add-Type -TypeDefinition $using:Source -Language VisualBasic")
        'BotList.Add("")
        'BotList.Add("    $Result = [Expression]::RunExpression()")
        'BotList.Add("    Write-Output ""Output""+$Result")
        ''BotList.Add("    Write-Output ""Source""+$using:Source")
        'BotList.Add("}")
        'BotList.Add("Wait-Job $job")
        'BotList.Add("$ResultCopy =  Receive-Job $job -Keep")
        'BotList.Add("Write-Output $ResultCopy")
        'BotList.Add("$job | Select *")

        BotList.Add("Add-Type -TypeDefinition $Source -Language VisualBasic")

        BotList.Add("")
        'BotList.Add("$Result = [Expression]::RunExpression()")
        BotList.Add(String.Format("$Result = [Expression{0}]::RunExpression()", RandomIdentifier))
        BotList.Add("Write-Output $Result")

        ''BotList.Add("$Result | Out-File -FilePath 'C:\data\junk\HousekeeperExpressionResult.txt' -Encoding UTF8")

        For Each L As List(Of String) In {TopList, MidList, BotList}
            For Each s In L
                OutList.Add(s)
            Next
        Next

        Return OutList

    End Function

    Public Function WriteExpressionFile(
        PowerShellFilename As String,
        PowerShellFileContents As List(Of String)
        ) As Boolean
        Dim Success As Boolean = True

        Try
            If IO.File.Exists(PowerShellFilename) Then IO.File.Delete(PowerShellFilename)
            IO.File.WriteAllLines(PowerShellFilename, PowerShellFileContents, System.Text.Encoding.Unicode)
        Catch ex As Exception
            Success = False
        End Try

        Return Success
    End Function

    Public Function RunExpressionScript(PowerShellFilename As String) As String

        Dim Result As String = ""

        Dim NewWay As Boolean = True

        If Not NewWay Then
            'Dim Result As String = ""

            'Dim P As New Diagnostics.Process
            'Dim PSError As String = ""

            'P.StartInfo.FileName = "powershell.exe"
            'P.StartInfo.Arguments = $"-command ""{PowerShellFilename.Replace(" ", "` ")}"""
            'P.StartInfo.RedirectStandardError = True
            'P.StartInfo.RedirectStandardOutput = True
            'P.StartInfo.UseShellExecute = False
            'P.StartInfo.CreateNoWindow = True
            'P.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8

            'P.Start()
            'PSError = P.StandardError.ReadToEnd
            'Dim PSResult As String = P.StandardOutput.ReadToEnd

            'If Not PSError = "" Then
            '    Throw New Exception(PSError)
            'End If

            'P.WaitForExit()
            'Result = PSResult.Replace(vbCrLf, "")

            'Return Result

        Else

            Dim ScriptList As List(Of String) = System.IO.File.ReadAllLines(PowerShellFilename).ToList

            Dim ScriptText As String = ""
            For Each s As String In ScriptList
                ScriptText = $"{ScriptText}{vbCrLf}{s}"
            Next

            Result = RunScript(ScriptText)

        End If

        Return Result

    End Function

    Public Function BuildSnippetFile(SnippetFilename As String) As String
        ' https://www.codestack.net/solidworks-pdm-api/permissions/set-folder-permissions/

        Dim Directory As String = IO.Path.GetDirectoryName(SnippetFilename)

        Dim Toplist As New List(Of String)
        Dim Midlist As New List(Of String)
        Dim Botlist As New List(Of String)
        Dim Outlist As New List(Of String)
        Dim s As String
        Dim Indent As String = "                "

        ' Workaround for not being able to redefine a PS Type in the same session.
        ' Needed for serial runs of 'Test on edge' in the expression editor.
        Static Generator As System.Random = New System.Random()
        Dim RandomIdentifier As Integer = Generator.Next(10000, 99999)

        Dim UP As New UtilsPreferences

        Dim DllPath As String = UP.GetStartupDirectory

        Dim PowerShellFilename As String = IO.Path.ChangeExtension(SnippetFilename, ".ps1")

        'Toplist.Add("$StartupPath = Split-Path $script:MyInvocation.MyCommand.Path")
        Toplist.Add($"$StartupPath = ""{Directory}""")
        Toplist.Add("")
        Toplist.Add("$DLLs = (")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgeFramework.dll"",")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgeFrameworkSupport.dll"",")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgeConstants.dll"",")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgePart.dll"",")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgeAssembly.dll"",")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgeDraft.dll"",")
        Toplist.Add($"    ""{DllPath}\Interop.SolidEdgeGeometry.dll""")
        Toplist.Add("    )")
        Toplist.Add("")
        Toplist.Add("$Source = @""")
        Toplist.Add("")
        Toplist.Add("Imports System")
        Toplist.Add("Imports System.Collections.Generic")
        Toplist.Add("Imports Microsoft.VisualBasic")
        Toplist.Add("Imports System.Linq")
        Toplist.Add("")
        'Toplist.Add("Public Class Snippet")
        Toplist.Add(String.Format("Public Class Snippet{0}", RandomIdentifier))
        Toplist.Add("")
        Toplist.Add("    Public Shared Function RunSnippet(StartupPath As String) As Integer")
        Toplist.Add("        Dim ExitStatus As Integer = 0")
        Toplist.Add("        Dim ErrorMessageList As New List(Of String)")
        Toplist.Add("")
        Toplist.Add("        Dim SEApp As SolidEdgeFramework.Application = Nothing")
        Toplist.Add("        Dim SEDoc As SolidEdgeFramework.SolidEdgeDocument = Nothing")
        Toplist.Add("")
        Toplist.Add("        Try")
        Toplist.Add("            SEApp = CType(Runtime.InteropServices.Marshal.GetActiveObject(""SolidEdge.Application""), SolidEdgeFramework.Application)")
        Toplist.Add("            SEDoc = CType(SEApp.ActiveDocument, SolidEdgeFramework.SolidEdgeDocument)")
        Toplist.Add("            Console.WriteLine(String.Format(""Processing {0}"", SEDoc.Name))")
        Toplist.Add("        Catch ex As Exception")
        Toplist.Add("            ExitStatus = 1")
        Toplist.Add("            ErrorMessageList.Add(""Unable to connect to Solid Edge, or no file is open"")")
        Toplist.Add("        End Try")
        Toplist.Add("")
        Toplist.Add("        If ExitStatus = 0 Then")
        Toplist.Add("")
        Toplist.Add("            Dim DocType = IO.Path.GetExtension(SEDoc.Fullname)")
        Toplist.Add("")
        Toplist.Add("            Try")

        Dim tmpMidlist = IO.File.ReadAllLines(SnippetFilename).ToList
        For Each s In tmpMidlist
            Midlist.Add(String.Format("{0}{1}", Indent, s))
        Next

        Botlist.Add("            Catch ex As Exception")
        Botlist.Add("                ExitStatus = 1")
        Botlist.Add("                ErrorMessageList.Add(String.Format(""{0}"", ex.Message))")
        Botlist.Add("            End Try")
        Botlist.Add("        End If")
        Botlist.Add("")
        Botlist.Add("        If ErrorMessageList.Count > 0 Then ExitStatus = 1")
        Botlist.Add("        If Not ExitStatus = 0 Then")
        Botlist.Add("            SaveErrorMessages(StartupPath, ErrorMessageList)")
        Botlist.Add("        End If")
        Botlist.Add("")
        Botlist.Add("        Return ExitStatus")
        Botlist.Add("    End Function")
        Botlist.Add("")
        Botlist.Add("    Public Shared Sub LoadLibrary(ParamArray libs As Object())")
        Botlist.Add("        For Each [lib] As String In libs")
        Botlist.Add("            'Console.WriteLine(String.Format(""Loading library:  {0}"", [lib]))")
        Botlist.Add("            Dim assm As System.Reflection.Assembly = System.Reflection.Assembly.LoadFrom([lib])")
        Botlist.Add("            'Console.WriteLine(assm.GetName().ToString())")
        Botlist.Add("        Next")
        Botlist.Add("    End Sub")
        Botlist.Add("")
        Botlist.Add("    Private Shared Sub SaveErrorMessages(StartupPath As String, ErrorMessageList As List(Of String))")
        Botlist.Add("        Dim ErrorFilename As String")
        Botlist.Add("        ErrorFilename = String.Format(""{0}\error_messages.txt"", StartupPath)")
        Botlist.Add("        IO.File.WriteAllLines(ErrorFilename, ErrorMessageList)")
        Botlist.Add("    End Sub")
        Botlist.Add("")
        Botlist.Add("End Class")
        Botlist.Add("""@")
        Botlist.Add("")
        'Botlist.Add("Add-Type -TypeDefinition $Source -Language VisualBasic")
        Botlist.Add("Add-Type -TypeDefinition $Source -ReferencedAssemblies $DLLs -Language VisualBasic")
        Botlist.Add("")
        'Botlist.Add("[Snippet]::LoadLibrary($DLLs)")
        Botlist.Add(String.Format("[Snippet{0}]::LoadLibrary($DLLs)", RandomIdentifier))
        Botlist.Add("")
        'Botlist.Add("$ExitStatus = [Snippet]::RunSnippet($StartupPath)")
        Botlist.Add(String.Format("$ExitStatus = [Snippet{0}]::RunSnippet($StartupPath)", RandomIdentifier))
        Botlist.Add("")
        Botlist.Add("Function ExitWithCode($exitcode) {")
        Botlist.Add("  $host.SetShouldExit($exitcode)")
        Botlist.Add("  Exit $exitcode")
        Botlist.Add("}")
        Botlist.Add("")
        Botlist.Add("ExitWithCode($ExitStatus)")

        For Each L As List(Of String) In {Toplist, Midlist, Botlist}
            For Each s In L
                Outlist.Add(s)
            Next
        Next

        IO.File.WriteAllLines(PowerShellFilename, Outlist)  ' If unicode problems arise, see WriteExpressionFile() for possible fix.

        Return PowerShellFilename
    End Function

End Class
