Imports System.Media
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports Housekeeper.My
Imports System.Text.RegularExpressions

Public Class FormTeamCenterAdd
    Private _mainForm As Form_Main

    Public Sub New(mainForm As Form_Main)
        InitializeComponent()
        _mainForm = mainForm
    End Sub

    Private Sub FormTeamCenterAdd_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        CloseSolidEdge()
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        Dim objApp As SolidEdgeFramework.Application = Nothing
        Dim TCE As SolidEdgeFramework.SolidEdgeTCE = Nothing
        Dim objDocuments As SolidEdgeFramework.Documents = Nothing

        Try
            Cursor.Current = Cursors.WaitCursor
            ' Connect to Solid Edge, if not open then Open Solid Edge

            Try
                LabelSearchStatus.Text = "Connecting to Solid Edge..."
                objApp = CType(Marshal.GetActiveObject("SolidEdge.Application"), SolidEdgeFramework.Application)
            Catch ex As COMException When ex.ErrorCode = &H800401E3
                LabelSearchStatus.Text = "Opening Solid Edge..."
                objApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
                objApp.Visible = True
            End Try

            TCE = objApp.SolidEdgeTCE
            objDocuments = objApp.Documents

            'Save Cache Path to settings.settings
            Dim cachePath As String = Nothing
            TCE.GetPDMCachePath(cachePath)
            My.Settings.cachePathTC = cachePath

            LabelSearchStatus.Text = "Searching..."

            'Get the list of item IDs and Revisions and split each line into a list 
            Dim listOfData As String = TextBoxItems.Text
            Dim lines() As String = listOfData.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
            Dim files As New List(Of Tuple(Of String, String, String))
            Dim numOfFiles As Integer = 0

            'Get the Item ID and revison from each line
            For Each line As String In lines
                Dim columns() As String = line.Split(vbTab)
                If columns.Length >= 2 Then
                    Dim itemID As String = columns(0).Trim()
                    Dim revision As String = columns(1).Trim()
                    Dim tempFiles As Object = Nothing
                    Dim tempNumOfFiles As Integer = 0
                    TCE.GetListOfFilesFromTeamcenterServer(itemID, revision, tempFiles, tempNumOfFiles)
                    If tempNumOfFiles > 0 Then
                        If TypeOf tempFiles Is Array Then
                            For Each file As Object In CType(tempFiles, Object())
                                files.Add(Tuple.Create(file.ToString(), itemID, revision))
                            Next
                        End If
                        numOfFiles += tempNumOfFiles
                    End If
                End If
            Next


            ListViewTeamCenterItems.Items.Clear()

            'Add files to listview. Don't add any .jt and .pdf and don't add any files that have unchecked filters
            For Each fileTuple As Tuple(Of String, String, String) In files
                Dim fileName As String = fileTuple.Item1
                Dim itemID As String = fileTuple.Item2
                Dim revision As String = fileTuple.Item3
                Dim extension As String = System.IO.Path.GetExtension(fileName).ToLower()
                If extension = ".pdf" Or extension = ".jt" OrElse
                   (Not CheckBoxAsm.Checked AndAlso extension = ".asm") OrElse
                   (Not CheckBoxPar.Checked AndAlso extension = ".par") OrElse
                   (Not CheckBoxDft.Checked AndAlso extension = ".dft") OrElse
                   (Not CheckBoxPsm.Checked AndAlso extension = ".psm") Then
                    Continue For
                End If
                Dim listViewItem As New ListViewItem(fileName)
                listViewItem.SubItems.Add(itemID)
                listViewItem.SubItems.Add(revision)
                ListViewTeamCenterItems.Items.Add(listViewItem)
            Next

            If numOfFiles = 0 Then
                LabelSearchStatus.Text = "No files found."
            Else
                LabelSearchStatus.Text = numOfFiles.ToString + " files found, filtered down to " + ListViewTeamCenterItems.Items.Count.ToString
            End If

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

        ' Check if the item is already in ListViewDownloadedFiles
        For Each item As ListViewItem In ListViewDownloadedFiles.Items
            If item.Text = fileName AndAlso item.SubItems(1).Text = fileItemID AndAlso item.SubItems(2).Text = fileItemRevID Then
                MessageBox.Show(fileName + " is already downloaded", "File Already Downloaded", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        Dim objApp As SolidEdgeFramework.Application = Nothing
        Dim TCE As SolidEdgeFramework.SolidEdgeTCE = Nothing

        Try
            Cursor.Current = Cursors.WaitCursor
            LabelDownloadStatus.Text = "Downloading..."

            Try
                objApp = CType(Marshal.GetActiveObject("SolidEdge.Application"), SolidEdgeFramework.Application)
            Catch ex As COMException When ex.ErrorCode = &H800401E3
                objApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
                objApp.Visible = True
            End Try
            TCE = objApp.SolidEdgeTCE

            ' Check if the item is already in ListViewDownloadedFiles
            Dim alreadyDownloaded As Boolean = False
            For Each downloadedItem As ListViewItem In ListViewDownloadedFiles.Items
                If downloadedItem.Text = fileName Then
                    alreadyDownloaded = True
                    Exit For
                End If
            Next

            If alreadyDownloaded Then
                MessageBox.Show(fileName + " is already downloaded", "File Already Downloaded", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Download the selected item
            Dim temp(,) As Object = New Object(1, 1) {}
            TCE.DownladDocumentsFromServerWithOptions(fileItemID, fileItemRevID, fileName, "", "", False, True, 1, temp)

            ' Get cache path and add the filename and file path to listview
            Dim filePath As String = System.IO.Path.Combine(My.Settings.cachePathTC, fileName)

            ListViewDownloadedFiles.Items.Add(New ListViewItem(New String() {fileName, filePath}))

            LabelDownloadStatus.Text = "Download complete!"
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub ButtonDownloadAll_Click(sender As Object, e As EventArgs) Handles ButtonDownloadAll.Click
        Dim objApp As SolidEdgeFramework.Application = Nothing
        Dim TCE As SolidEdgeFramework.SolidEdgeTCE = Nothing

        Try
            Cursor.Current = Cursors.WaitCursor
            LabelDownloadStatus.Text = "Downloading..."

            Try
                objApp = CType(Marshal.GetActiveObject("SolidEdge.Application"), SolidEdgeFramework.Application)
            Catch ex As COMException When ex.ErrorCode = &H800401E3
                objApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
                objApp.Visible = True
            End Try
            TCE = objApp.SolidEdgeTCE

            ' Download each file in listview
            For Each item As ListViewItem In ListViewTeamCenterItems.Items
                Dim fileName As String = item.Text
                Dim fileItemID As String = item.SubItems(1).Text
                Dim fileItemRevID As String = item.SubItems(2).Text

                ' Check if the item is already in ListViewDownloadedFiles
                Dim alreadyDownloaded As Boolean = False
                For Each downloadedItem As ListViewItem In ListViewDownloadedFiles.Items
                    If downloadedItem.Text = fileName Then
                        alreadyDownloaded = True
                        Exit For
                    End If
                Next

                If alreadyDownloaded Then
                    MessageBox.Show("File Already Downloaded", fileName + " is already downloaded", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Continue For
                End If

                Dim temp(,) As Object = New Object(1, 1) {}
                TCE.DownladDocumentsFromServerWithOptions(fileItemID, fileItemRevID, fileName, "", "", False, True, 1, temp)
                Dim filePath As String = System.IO.Path.Combine(My.Settings.cachePathTC, fileName)
                ListViewDownloadedFiles.Items.Add(New ListViewItem(New String() {fileName, filePath}))
            Next

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
            Dim filePath As String = System.IO.Path.Combine(My.Settings.cachePathTC, fileName)

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
    Private Sub TextBoxPastedData_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxItems.KeyDown
        If e.KeyCode = Keys.Tab Then
            Dim textBox As TextBox = CType(sender, TextBox)
            Dim selectionIndex As Integer = textBox.SelectionStart
            textBox.Text = textBox.Text.Insert(selectionIndex, vbTab)
            textBox.SelectionStart = selectionIndex + 1
            e.SuppressKeyPress = True
        End If
    End Sub
End Class
