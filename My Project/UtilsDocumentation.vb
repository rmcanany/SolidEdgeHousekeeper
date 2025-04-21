Option Strict On

Public Class UtilsDocumentation

    Public Function GenerateVersionURL(Tag As String) As String

        ' Builds a URL to the version-specific help page on GitHub.

        ' Old way
        ' To get the BaseURL, on GitHub click the Commits button on the file list header.
        ' Click the top commit on the list.  On that page, click Browse Files.
        ' Click HelpTopics.md.  The URL that opens is the BaseURL.

        ' New way
        ' To update the base URL, run Housekeeper in Release mode, then <CTRL><ALT>-click the main Help button.

        Dim UP As New UtilsPreferences

        Dim Filename As String = UP.GetHelpfileBaseURLFilename
        Dim VersionURL As String = ""

        If Not FileIO.FileSystem.FileExists(Filename) Then
            UpdateBaseURL()
        End If

        Dim Inlist As List(Of String) = IO.File.ReadAllLines(Filename).ToList

        Dim BaseURL As String = Inlist(0)
        VersionURL = String.Format("{0}#{1}", BaseURL, Tag.Replace("#", ""))

        Return VersionURL

    End Function

    Public Sub UpdateBaseURL()

        ' Updates <StartupDirectory>\HelpTextBaseURL.txt with the latest commit hash

        ' ###### GET LATEST COMMIT STRING ######

        Dim WC As New System.Net.WebClient
        Dim NewList As List(Of String)
        Dim s As String
        Dim DoubleQuote As Char = Chr(34)
        Dim UP As New UtilsPreferences
        Dim CommitString As String
        Dim BaseURL As String
        Dim HelpfileBaseURLFilename As String
        Dim Outlist As New List(Of String)

        ' Format example (in testing, this was line 1 from the api results)
        '"{""sha"":""dfbcf706c5cc8417d751351d2b56e46983ffbe29"""

        WC.Headers.Add("User-Agent: Other")  ' Get a 403 error without this.

        s = WC.DownloadString("https://api.github.com/repos/rmcanany/solidedgehousekeeper/commits/master")

        NewList = s.Split(CChar(",")).ToList

        For Each s In NewList
            If s.Contains("sha") Then
                Exit For
            End If
        Next

        s = s.ToLower
        s = s.Replace(DoubleQuote, "")  ' '{""sha"":""dfbcf706c5cc8417d751351d2b56e46983ffbe29""' -> '{sha:dfbcf706c5cc8417d751351d2b56e46983ffbe29'
        s = s.Split(CChar(":"))(1)      ' '{sha:dfbcf706c5cc8417d751351d2b56e46983ffbe29' -> 'dfbcf706c5cc8417d751351d2b56e46983ffbe29'

        CommitString = s

        ' ###### SAVE TO FILE ######

        BaseURL = String.Format("https://github.com/rmcanany/SolidEdgeHousekeeper/blob/{0}/HelpTopics.md", CommitString)
        Outlist.Add(BaseURL)

        HelpfileBaseURLFilename = UP.GetHelpfileBaseURLFilename
        IO.File.WriteAllLines(HelpfileBaseURLFilename, Outlist)

        'Dim HelpfileBaseURLFilenameForDevs = UP.GetHelpfileBaseURLFilenameForDevs
        'IO.File.WriteAllLines(HelpfileBaseURLFilenameForDevs, Outlist)


        If HelpfileBaseURLFilename.Contains(UP.GetHardCodedPath) Then
            MsgBox(String.Format("Updated BaseURL '{0}'", BaseURL))
        End If

    End Sub

    Public Sub BuildReadmeFile()
        Dim UP As New UtilsPreferences
        Dim ReadmeFileName As String = String.Format("{0}\README.md", UP.GetHardCodedPath)

        Dim HelpTopicsFileName As String = ReadmeFileName.Replace("README", "HelpTopics")

        ' The readme file is not needed on the user's machine.  
        ' StartupPath is hard coded so this hopefully doesn't do anything on their machine.
        Dim StartupPath As String = String.Format("{0}\bin\Release", UP.GetHardCodedPath)

        Dim TaskListHeader As String = "<!-- Start -->"
        Dim Proceed As Boolean = True
        Dim i As Integer
        Dim msg As String

        Dim ReadmeIn As String() = Nothing
        Dim ReadmeOut As New List(Of String)

        If FileIO.FileSystem.DirectoryExists(StartupPath) Then
            Try
                ReadmeIn = IO.File.ReadAllLines(ReadmeFileName)
            Catch ex As Exception
                MsgBox(String.Format("Error opening {0}", ReadmeFileName))
                Proceed = False
            End Try
        Else
            Proceed = False
        End If

        If Proceed Then
            For i = 0 To ReadmeIn.Count - 1
                If Not ReadmeIn(i).Contains(TaskListHeader) Then
                    ReadmeOut.Add(ReadmeIn(i))
                Else
                    Exit For
                End If
            Next

            ReadmeOut.Add(TaskListHeader)
            ReadmeOut.Add("")

            Dim tmpTaskList = UP.BuildTaskListFromScratch(Nothing)

            ' html for collapsible sections
            '<details><summary><h2 style="display:inline-block">Task Details</h2></summary>
            'msg = String.Format(
            '    "<details><summary><h2 style={0}margin:0px; display:inline-block{0}><img src={0}My%20Project/media/spacer.png{0}><img src={0}Resources/SE_asm.png{0}><img src={0}My%20Project/media/spacer.png{0}>TASK DETAILS</h2></summary>",
            '    Chr(34))
            msg = String.Format("<details><summary><h2 style={0}margin:0px; display:inline-block{0}>", Chr(34))
            msg = String.Format("{1}<img src={0}My%20Project/media/spacer.png{0}>", Chr(34), msg)
            msg = String.Format("{1}<img src={0}Resources/SE_asm.png{0}>", Chr(34), msg)
            msg = String.Format("{1}<img src={0}My%20Project/media/spacer.png{0}>", Chr(34), msg)
            msg = String.Format("{1}TASK DETAILS</h2></summary>", Chr(34), msg)

            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            For Each Task As Task In tmpTaskList
                Dim ImageName As String = Task.Name
                'msg = String.Format(
                '    "<details><summary><h3 style={0}margin:0px; display:inline-block{0}><img src={0}My%20Project/media/spacer.png{0}><img src={0}Resources/{2}.png{0}><img src={0}My%20Project/media/spacer.png{0}>{1}</h3></summary>",
                '    Chr(34), Task.Description, ImageName)
                msg = String.Format("<details><summary><h3 style={0}margin:0px; display:inline-block{0}>", Chr(34))
                msg = String.Format("{1}<img src={0}My%20Project/media/spacer.png{0}>", Chr(34), msg)
                msg = String.Format("{1}<img src={0}Resources/{2}.png{0}>", Chr(34), msg, ImageName)
                msg = String.Format("{1}<img src={0}My%20Project/media/spacer.png{0}>", Chr(34), msg)
                msg = String.Format("{1}{2}</h3></summary>", Chr(34), msg, Task.Description)

                ReadmeOut.Add(msg)
                ReadmeOut.Add("")

                ReadmeOut.Add(Task.HelpText)
                ReadmeOut.Add("")

                ReadmeOut.Add("</details>") ' Close collapsible section
                ReadmeOut.Add("")
            Next

            ReadmeOut.Add("</details>") ' Close collapsible section
            ReadmeOut.Add("")

            ReadmeOut.Add("")
            'msg = "# KNOWN ISSUES"
            'msg = String.Format("<details><summary><h2 style={0}margin:0px; display:inline-block{0}><img src={0}Resources/icons8_help_16.png{0} style={0}padding-right:10px{0}>KNOWN ISSUES</h2></summary>", Chr(34))
            msg = String.Format("<details><summary><h2 style={0}margin:0px; display:inline-block{0}>", Chr(34))
            msg = String.Format("{1}<img src={0}My%20Project/media/spacer.png{0}>", Chr(34), msg)
            msg = String.Format("{1}<img src={0}Resources/icons8_help_16.png{0}>", Chr(34), msg)
            msg = String.Format("{1}<img src={0}My%20Project/media/spacer.png{0}>", Chr(34), msg)
            msg = String.Format("{1}KNOWN ISSUES</h2></summary>", Chr(34), msg)

            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            msg = "**The program is not perfect**"
            ReadmeOut.Add(msg)
            msg = "- *Cause*: The programmer is not perfect."
            ReadmeOut.Add(msg)
            msg = "- *Possible workaround*: Back up any files before using it.  The program can process a large number of files in a short amount of time.  It can do damage at the same rate.  It has been tested on thousands of our files, but none of yours.  So, you know, back up any files before using it.  "
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            msg = "**Does not support managed files**"
            ReadmeOut.Add(msg)
            msg = "- *Cause*: Unknown."
            ReadmeOut.Add(msg)
            msg = "- *Possible workaround*: Process the files in an unmanaged workspace."
            ReadmeOut.Add(msg)
            msg = "- *Update 10/10/2021* Some users have reported success with BiDM managed files."
            ReadmeOut.Add(msg)
            msg = "- *Update 1/25/2022* One user has reported success with Teamcenter 'cached' files."
            ReadmeOut.Add(msg)
            msg = "- *Update 4/18/2025* Now supports Teamcenter cached files, including the ability to download from the database."
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            msg = "**Some tasks cannot run on older Solid Edge versions**"
            ReadmeOut.Add(msg)
            msg = "- *Cause*: Probably an API call not available in previous versions."
            ReadmeOut.Add(msg)
            msg = "- *Possible workaround*: Use the latest version, or avoid use of the task causing problems."
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            msg = "**May not support multiple installed Solid Edge versions**"
            ReadmeOut.Add(msg)
            msg = "- *Cause*: Unknown."
            ReadmeOut.Add(msg)
            msg = "- *Possible workaround*: Use the version that was 'silently' installed."
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            msg = "**Pathfinder sometimes blank during Interactive Edit**"
            ReadmeOut.Add(msg)
            msg = "- *Cause*: Unknown."
            ReadmeOut.Add(msg)
            msg = "- *Possible workaround*: Refresh the screen by minimizing and maximizing the Solid Edge window."
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            ReadmeOut.Add("</details>")
            ReadmeOut.Add("")

            ReadmeOut.Add("")
            'msg = "# OPEN SOURCE PACKAGES"
            'msg = String.Format("<details><summary><h2 style={0}margin:0px; display:inline-block{0}><img src={0}Resources/TaskRunExternalProgram.png{0} style={0}padding-right:10px{0}>OPEN SOURCE PACKAGES</h2></summary>", Chr(34))
            msg = String.Format("<details><summary><h2 style={0}margin:0px; display:inline-block{0}>", Chr(34))
            msg = String.Format("{1}<img src={0}My%20Project/media/spacer.png{0}>", Chr(34), msg)
            msg = String.Format("{1}<img src={0}Resources/TaskRunExternalProgram.png{0}>", Chr(34), msg)
            msg = String.Format("{1}<img src={0}My%20Project/media/spacer.png{0}>", Chr(34), msg)
            msg = String.Format("{1}OPEN SOURCE PACKAGES</h2></summary>", Chr(34), msg)

            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            msg = "This project uses these awesome open source packages."
            ReadmeOut.Add(msg)
            'msg = "- Solid Edge Community [<ins>**SolidEdgeCommunity**</ins>](https://github.com/SolidEdgeCommunity)"
            'ReadmeOut.Add(msg)
            msg = "- JSON Converter [<ins>**Newtonsoft.Json**</ins>](https://github.com/JamesNK/Newtonsoft.Json)"
            ReadmeOut.Add(msg)
            msg = "- Excel reader [<ins>**ExcelDataReader**</ins>](https://github.com/ExcelDataReader/ExcelDataReader)"
            ReadmeOut.Add(msg)
            msg = "- Expression engine [<ins>**PanoramicData.NCalcExtensions**</ins>](https://github.com/panoramicdata/PanoramicData.NCalcExtensions)"
            ReadmeOut.Add(msg)
            msg = "- Expression editor [<ins>**FastColoredTextBox**</ins>](https://github.com/PavelTorgashov/FastColoredTextBox)"
            ReadmeOut.Add(msg)
            msg = "- Structured storage editor [<ins>**OpenMCDF**</ins>](https://github.com/ironfede/openmcdf)"
            ReadmeOut.Add(msg)
            msg = "- Icons [<ins>**Icons8**</ins>](https://icons8.com)"
            ReadmeOut.Add(msg)

            ReadmeOut.Add("</details>")
            ReadmeOut.Add("")

            msg = ""
            ReadmeOut.Add("")
            'msg = "# CODE ORGANIZATION"
            'msg = String.Format("<details><summary><h2 style={0}margin:0px; display:inline-block{0}><img src={0}Resources/Info-16.png{0} style={0}padding-right:10px{0}>CODE ORGANIZATION</h2></summary>", Chr(34))
            msg = String.Format("<details><summary><h2 style={0}margin:0px; display:inline-block{0}>", Chr(34))
            msg = String.Format("{1}<img src={0}My%20Project/media/spacer.png{0}>", Chr(34), msg)
            msg = String.Format("{1}<img src={0}Resources/Info-16.png{0}>", Chr(34), msg)
            msg = String.Format("{1}<img src={0}My%20Project/media/spacer.png{0}>", Chr(34), msg)
            msg = String.Format("{1}CODE ORGANIZATION</h2></summary>", Chr(34), msg)

            ReadmeOut.Add(msg)
            ReadmeOut.Add("")
            msg = "Processing starts in Form_Main.vb.  A short description of the code's so-called organization can be found there."
            ReadmeOut.Add(msg)
            ReadmeOut.Add("")

            ReadmeOut.Add("</details>")
            ReadmeOut.Add("")

            IO.File.WriteAllLines(ReadmeFileName, ReadmeOut)

            ' This is a copy of the README with all sections expanded.
            ' It is needed for the version-specific help URLs.
            Dim HelpTopicsOut As New List(Of String)
            For Each Line As String In ReadmeOut
                If Line.Contains("<details") And Not Line.Contains("<details open") Then
                    Line = Line.Replace("<details>", "<details open>")
                End If
                HelpTopicsOut.Add(Line)
            Next

            IO.File.WriteAllLines(HelpTopicsFileName, HelpTopicsOut)

        End If

    End Sub


End Class
