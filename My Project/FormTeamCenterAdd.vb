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

            LabelSearchStatus.Text = "Connecting to TeamCenter...(You may need to sign in)"
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

            'Get the Item ID and revision from each line
            For Each line As String In lines
                Dim columns() As String = line.Split(vbTab)
                If columns.Length <> 2 Then
                    Throw New ApplicationException($"Invalid format: '{line}'. Each line must contain exactly 2 columns separated by a tab.")
                End If

                Dim itemID As String = columns(0).Trim()
                Dim revision As String = columns(1).Trim()

                ' Validate item ID and revision
                If Not System.Text.RegularExpressions.Regex.IsMatch(itemID, "^\d{8}$") Then
                    Throw New ApplicationException($"Invalid Item ID: {itemID}. It should be 8 digits.")
                End If

                If String.IsNullOrEmpty(revision) Then
                    Throw New ApplicationException($"Invalid Revision for Item ID: {itemID}. Revision cannot be blank or null.")
                End If

                ' Validate if revision exists
                Dim MFKAttributes(0, 1) As Object
                Dim RevIdAndUIDs As Object = Nothing

                MFKAttributes(0, 0) = "item_id"
                MFKAttributes(0, 1) = itemID

                TCE.GetAllRevisions(MFKAttributes, "MFK9Item1", RevIdAndUIDs)

                Dim revisionExists As Boolean = False
                If RevIdAndUIDs IsNot Nothing Then
                    For i As Integer = 0 To RevIdAndUIDs.GetLength(0) - 1
                        If RevIdAndUIDs(i, 0).ToString() = revision Then
                            revisionExists = True
                            Exit For
                        End If
                    Next
                End If

                If Not revisionExists Then
                    Throw New ApplicationException($"Invalid Revision: {revision} for Item ID: {itemID}. Revision does not exist.")
                End If

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

        Catch ex As ApplicationException
            MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            LabelSearchStatus.Text = "Search failed!"
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            LabelSearchStatus.Text = "Search failed!"
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub ButtonDownload_Click(sender As Object, e As EventArgs) Handles ButtonDownload.Click
        If ListViewTeamCenterItems.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a file to add to cache.", "No File Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedItem As ListViewItem = ListViewTeamCenterItems.SelectedItems(0)
        Dim fileName As String = selectedItem.Text
        Dim fileItemID As String = selectedItem.SubItems(1).Text
        Dim fileItemRevID As String = selectedItem.SubItems(2).Text

        ' Check if the item is already in ListViewDownloadedFiles
        For Each item As ListViewItem In ListViewDownloadedFiles.Items
            If item.Text = fileName Then
                LabelDownloadStatus.Text = "Item already in cache!"
                MessageBox.Show(fileName + " is already in the cache folder", "File Already In Cache", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        Dim objApp As SolidEdgeFramework.Application = Nothing
        Dim TCE As SolidEdgeFramework.SolidEdgeTCE = Nothing

        Try
            Cursor.Current = Cursors.WaitCursor
            LabelDownloadStatus.Text = "Adding to cache..."

            Try
                objApp = CType(Marshal.GetActiveObject("SolidEdge.Application"), SolidEdgeFramework.Application)
            Catch ex As COMException When ex.ErrorCode = &H800401E3
                objApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
                objApp.Visible = True
            End Try
            TCE = objApp.SolidEdgeTCE

            ' Download the selected item
            Dim temp(,) As Object = New Object(1, 1) {}
            TCE.DownladDocumentsFromServerWithOptions(fileItemID, fileItemRevID, fileName, "", "", False, True, 1, temp)

            ' Get cache path and add the filename and file path to listview
            Dim filePath As String = System.IO.Path.Combine(My.Settings.cachePathTC, fileName)

            ListViewDownloadedFiles.Items.Add(New ListViewItem(New String() {fileName, filePath}))

            LabelDownloadStatus.Text = "Added to cache!"
        Catch ex As Exception
            LabelDownloadStatus.Text = "Item already in cache!"
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
            LabelDownloadStatus.Text = "Adding to cache..."

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
                    MessageBox.Show("File Already In Cache", fileName + " is already in the cache folder", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Continue For
                End If

                Dim temp(,) As Object = New Object(1, 1) {}
                TCE.DownladDocumentsFromServerWithOptions(fileItemID, fileItemRevID, fileName, "", "", False, True, 1, temp)
                Dim filePath As String = System.IO.Path.Combine(My.Settings.cachePathTC, fileName)
                ListViewDownloadedFiles.Items.Add(New ListViewItem(New String() {fileName, filePath}))
            Next

            LabelDownloadStatus.Text = "Added to cache!"
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

            ' Check if the file already exists in the ListView
            Dim exists As Boolean = False
            For Each existingItem As ListViewItem In _mainForm.ListViewFiles.Items
                If existingItem.Name = filePath Then
                    exists = True
                    Exit For
                End If
            Next

            ' Add the file to the ListView in the main form if it doesn't already exist
            If Not exists Then
                Dim tmpItem As New ListViewItem
                tmpItem.Text = fileName
                tmpItem.SubItems.Add(filePath)
                tmpItem.ImageKey = "Unchecked"
                tmpItem.Tag = IO.Path.GetExtension(filePath).ToLower
                tmpItem.Name = filePath
                tmpItem.Group = _mainForm.ListViewFiles.Groups.Item(IO.Path.GetExtension(filePath).ToLower)
                _mainForm.ListViewFiles.Items.Add(tmpItem)
            End If
        Next

        Me.Close()
    End Sub

    'Was supposed to add a tab key when the tab key is pressed instead of changing selected object but doesn't work
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
