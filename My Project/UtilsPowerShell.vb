Option Strict On

Imports System.Collections.ObjectModel
Imports System.Management.Automation
Imports System.Management.Automation.Runspaces

Public Class UtilsPowerShell
    ' https://www.codeproject.com/Articles/18229/How-to-run-PowerShell-scripts-from-C

    Public Function RunScript(ByVal scriptText As String) As String
        Dim runspace As Runspace = RunspaceFactory.CreateRunspace()
        runspace.Open()

        Dim pipeline As Pipeline = runspace.CreatePipeline()
        pipeline.Commands.AddScript(scriptText)
        pipeline.Commands.Add("Out-String")

        Dim results As Collection(Of PSObject) = pipeline.Invoke()

        runspace.Close()

        Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()

        For Each obj As PSObject In results
            stringBuilder.AppendLine(obj.ToString())
        Next

        Return stringBuilder.ToString()
    End Function

    Public Function BuildExpressionFile(Expression As List(Of String)) As List(Of String)
        Dim TopList As New List(Of String)
        Dim MidList As New List(Of String)
        Dim BotList As New List(Of String)
        Dim OutList As New List(Of String)
        Dim Indent As String = "        "

        TopList.Add("$Source = @""")
        TopList.Add("")
        TopList.Add("Imports System")
        TopList.Add("Imports System.Collections.Generic")
        TopList.Add("")
        TopList.Add("Public Class Expression")
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
        BotList.Add("Add-Type -TypeDefinition $Source -Language VisualBasic")
        BotList.Add("")
        BotList.Add("$Result = [Expression]::RunExpression()")
        BotList.Add("Write-Output $Result")

        For Each L As List(Of String) In {TopList, MidList, BotList}
            For Each s In L
                OutList.Add(s)
            Next
        Next

        Return OutList

    End Function

    Public Function RunExpressionScript(PowerShellFilename As String) As String
        Dim Result As String = ""

        Dim P As New Diagnostics.Process
        Dim PSError As String = ""
        P.StartInfo.FileName = "powershell.exe"
        'P.StartInfo.Arguments = String.Format("-command {1}{0}{1}", PowerShellFilename.Replace(" ", "` "), Chr(34))
        P.StartInfo.Arguments = $"-command ""{PowerShellFilename.Replace(" ", "` ")}"""
        P.StartInfo.RedirectStandardError = True
        P.StartInfo.RedirectStandardOutput = True
        P.StartInfo.UseShellExecute = False
        P.StartInfo.CreateNoWindow = True
        P.Start()
        PSError = P.StandardError.ReadToEnd
        Dim PSResult As String = P.StandardOutput.ReadToEnd

        If Not PSError = "" Then
            Throw New Exception(PSError)
        End If

        P.WaitForExit()
        Result = PSResult.Replace(vbCrLf, "")

        Return Result

    End Function

    Private Function BuildSnippetFile(SnippetFilename As String) As String
        ' https://www.codestack.net/solidworks-pdm-api/permissions/set-folder-permissions/

        Dim Toplist As New List(Of String)
        Dim Midlist As New List(Of String)
        Dim Botlist As New List(Of String)
        Dim Outlist As New List(Of String)
        Dim s As String
        Dim Indent As String = "                "

        Dim UP As New UtilsPreferences

        Dim DllPath As String = UP.GetStartupDirectory

        Dim PowerShellFilename As String = IO.Path.ChangeExtension(SnippetFilename, ".ps1")

        Toplist.Add("$StartupPath = Split-Path $script:MyInvocation.MyCommand.Path")
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
        Toplist.Add("")
        Toplist.Add("Public Class Snippet")
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
        Botlist.Add("[Snippet]::LoadLibrary($DLLs)")
        Botlist.Add("")
        Botlist.Add("$ExitStatus = [Snippet]::RunSnippet($StartupPath)")
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

        IO.File.WriteAllLines(PowerShellFilename, Outlist)

        Return PowerShellFilename
    End Function

End Class
