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

End Class
