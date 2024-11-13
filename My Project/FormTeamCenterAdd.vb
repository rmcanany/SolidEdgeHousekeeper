Imports System.Media
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports Housekeeper.My

Public Class FormTeamCenterAdd
    Private _mainForm As Form_Main
    Dim cachePath As String = Nothing

    Public Sub New(mainForm As Form_Main)
        InitializeComponent()
        _mainForm = mainForm
    End Sub
    Private Sub FormTeamCenterAdd_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        CloseSolidEdge()
    End Sub
    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        Dim objApp As SolidEdgeFramework.Application = Nothing
        Dim objSEEC As SolidEdgeFramework.SolidEdgeTCE = Nothing
        Dim objDocuments As SolidEdgeFramework.Documents = Nothing

        Try
            Cursor.Current = Cursors.WaitCursor
            Try
                LabelSearchStatus.Text = "Connecting to Solid Edge..."
                objApp = CType(Marshal.GetActiveObject("SolidEdge.Application"), SolidEdgeFramework.Application)
            Catch ex As COMException When ex.ErrorCode = &H800401E3
                LabelSearchStatus.Text = "Opening Solid Edge..."
                objApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
                objApp.Visible = True
            End Try
            objSEEC = objApp.SolidEdgeTCE
            objDocuments = objApp.Documents

            LabelSearchStatus.Text = "Searching..."

            Dim files = Nothing
            Dim numOfFiles As Integer = 0
            objSEEC.GetListOfFilesFromTeamcenterServer(TextBoxItemID.Text, TextBoxRev.Text, files, numOfFiles)

            If numOfFiles = 0 Then
                LabelSearchStatus.Text = "No files found with item ID of " + TextBoxItemID.Text + " with revision " + TextBoxRev.Text
            Else
                LabelSearchStatus.Text = numOfFiles.ToString + " files found!"
            End If

            ListViewTeamCenterItems.Items.Clear()

            For Each file As String In CType(files, Object())
                Dim fileName As String = file
                Dim itemID As String = TextBoxItemID.Text
                Dim revision As String = TextBoxRev.Text

                Dim listViewItem As New ListViewItem(fileName)
                listViewItem.SubItems.Add(itemID)
                listViewItem.SubItems.Add(revision)
                ListViewTeamCenterItems.Items.Add(listViewItem)
            Next

        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Sub ButtonDownload_Click(sender As Object, e As EventArgs) Handles ButtonDownload.Click
        If ListViewTeamCenterItems.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a file to download.", "No File Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedItem As ListViewItem = ListViewTeamCenterItems.SelectedItems(0)
        Dim fileName As String = selectedItem.Text
        Dim fileItemID As String = selectedItem.SubItems(1).Text
        Dim fileItemRevID As String = selectedItem.SubItems(2).Text

        Dim objApp As SolidEdgeFramework.Application = Nothing
        Dim objSEEC As SolidEdgeFramework.SolidEdgeTCE = Nothing

        Try
            Cursor.Current = Cursors.WaitCursor
            LabelDownloadStatus.Text = "Downloading..."

            Try
                objApp = CType(Marshal.GetActiveObject("SolidEdge.Application"), SolidEdgeFramework.Application)
            Catch ex As COMException When ex.ErrorCode = &H800401E3
                objApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
                objApp.Visible = True
            End Try
            objSEEC = objApp.SolidEdgeTCE

            Dim temp(,) As Object = New Object(1, 1) {}
            objSEEC.DownladDocumentsFromServerWithOptions(fileItemID, fileItemRevID, fileName, "", "", False, True, 1, temp)

            objSEEC.GetPDMCachePath(cachePath)
            My.Settings.cachePathTC = cachePath
            Dim filePath As String = System.IO.Path.Combine(cachePath, fileName)

            ListViewDownloadedFiles.Items.Add(New ListViewItem(New String() {fileName, filePath}))

            LabelDownloadStatus.Text = "Download complete!"
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub CloseSolidEdge()
        Dim objApp As SolidEdgeFramework.Application = Nothing
        Try
            objApp = CType(Marshal.GetActiveObject("SolidEdge.Application"), SolidEdgeFramework.Application)
            objApp.Quit()
        Catch ex As Exception
            ' Handle any exceptions if needed
        Finally
            ReleaseComObject(objApp)
        End Try
    End Sub

    Private Sub ReleaseComObject(ByVal obj As Object)
        If obj IsNot Nothing Then
            Marshal.ReleaseComObject(obj)
            obj = Nothing
        End If
    End Sub

    Private Sub ButtonAddAndClose_Click(sender As Object, e As EventArgs) Handles ButtonAddAndClose.Click
        If ListViewTeamCenterItems.Items.Count = 0 Then
            MessageBox.Show("No files to add.", "No Files", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If
        For Each item As ListViewItem In ListViewDownloadedFiles.Items
            Dim fileName As String = item.Text
            Dim filePath As String = System.IO.Path.Combine(cachePath, fileName)

            ' Add the file to the ListView in the main form
            Dim tmpItem As New ListViewItem
            tmpItem.Text = fileName
            tmpItem.SubItems.Add(filePath)
            tmpItem.ImageKey = "Unchecked"
            tmpItem.Tag = IO.Path.GetExtension(filePath).ToLower
            tmpItem.Name = filePath
            tmpItem.Group = _mainForm.ListViewFiles.Groups.Item(IO.Path.GetExtension(filePath).ToLower)
            _mainForm.ListViewFiles.Items.Add(tmpItem)
        Next

        Me.Close()
    End Sub
End Class