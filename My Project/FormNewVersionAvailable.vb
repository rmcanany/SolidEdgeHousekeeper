Public Class FormNewVersionAvailable

    Public Sub New(CurrentVersion As String, NewVersion As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim s = String.Format("Version {0} is available.  You have version {1}.", NewVersion, CurrentVersion)
        LabelNewVersionAvailable.Text = s

    End Sub
    Private Sub FormNewVersionAvailable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim LLList As List(Of LinkLabel)
        LLList = {LinkLabelReleaseNotes, LinkLabelInstallationInstructions, LinkLabelDownloadPage}.ToList

        Dim URLList As New List(Of String)
        URLList.Add("https://github.com/rmcanany/SolidEdgeHousekeeper/blob/master/release_notes.md")
        URLList.Add("https://github.com/rmcanany/SolidEdgeHousekeeper#installation")
        URLList.Add("https://github.com/rmcanany/SolidEdgeHousekeeper/releases")

        Dim StartIdx = 0
        Dim EndIdx As Integer

        For i = 0 To LLList.Count - 1
            EndIdx = Len(LLList(i).Text)
            LLList(i).Links.Add(StartIdx, EndIdx, URLList(i))
        Next

    End Sub

    Private Sub LinkLabelReleaseNotes_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelReleaseNotes.LinkClicked
        System.Diagnostics.Process.Start(e.Link.LinkData.ToString())
    End Sub

    Private Sub LinkLabelInstallationInstructions_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelInstallationInstructions.LinkClicked
        System.Diagnostics.Process.Start(e.Link.LinkData.ToString())
    End Sub

    Private Sub LinkLabelDownloadPage_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelDownloadPage.LinkClicked
        System.Diagnostics.Process.Start(e.Link.LinkData.ToString())
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.DialogResult = DialogResult.OK
    End Sub
End Class