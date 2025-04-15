Imports System.Runtime.InteropServices

Public Class FormTeamCenterAdd
    Private FMain As Form_Main

    Public Sub New(_FMain As Form_Main)
        InitializeComponent()
        FMain = _FMain
    End Sub

    Private Sub FormTeamCenterAdd_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        CloseSolidEdge()
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        SearchTeamCenter()
    End Sub

    Private Sub ButtonDownloadAll_Click(sender As Object, e As EventArgs) Handles ButtonDownloadAll.Click
        DownloadAll()
    End Sub

    Private Sub ButtonSearchAndAdd_Click(sender As Object, e As EventArgs) Handles ButtonSearchAndAdd.Click
        SearchTeamCenter()
        DownloadAll()
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
                LabelStatus.Text = "Item already in cache!"
                MessageBox.Show(fileName + " is already in the cache folder", "File Already In Cache", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        Dim objApp As SolidEdgeFramework.Application = Nothing
        Dim TCE As SolidEdgeFramework.SolidEdgeTCE = Nothing

        Try
            Cursor.Current = Cursors.WaitCursor
            LabelStatus.Text = "Connecting to Solid Edge..."

            Try
                objApp = CType(Marshal.GetActiveObject("SolidEdge.Application"), SolidEdgeFramework.Application)
            Catch ex As COMException When ex.ErrorCode = &H800401E3
                objApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
                objApp.Visible = True
            End Try
            TCE = objApp.SolidEdgeTCE

            LabelStatus.Text = "Adding to cache..."

            ' Download the selected item
            Dim temp(,) As Object = New Object(1, 1) {}
            TCE.DownladDocumentsFromServerWithOptions(fileItemID, fileItemRevID, fileName, "", "", False, True, 1, temp)

            ' Get cache path and add the filename and file path to listview
            Dim filePath As String = System.IO.Path.Combine(FMain.TCCachePath, fileName)

            ListViewDownloadedFiles.Items.Add(New ListViewItem(New String() {fileName, filePath}))

            LabelStatus.Text = "Added to cache!"
        Catch ex As Exception
            LabelStatus.Text = "Item already in cache!"
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub SearchTeamCenter()
        Dim objApp As SolidEdgeFramework.Application = Nothing
        Dim TCE As SolidEdgeFramework.SolidEdgeTCE = Nothing
        Dim objDocuments As SolidEdgeFramework.Documents = Nothing

        Try
            Cursor.Current = Cursors.WaitCursor
            ' Connect to Solid Edge, if not open then Open Solid Edge

            Try
                LabelStatus.Text = "Connecting to Solid Edge..."
                objApp = CType(Marshal.GetActiveObject("SolidEdge.Application"), SolidEdgeFramework.Application)
            Catch ex As COMException When ex.ErrorCode = &H800401E3
                LabelStatus.Text = "Opening Solid Edge..."
                objApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
                objApp.Visible = False
            End Try

            TCE = objApp.SolidEdgeTCE

            Dim activePDMMode As UInteger
            TCE.GetActivePDMMode(activePDMMode)

            If activePDMMode = 1 Then
                Throw New ApplicationException($"TeamCenter not set as PDM mode. Please open Solid Edge and enable TeamCenter by going to File > Manage > TeamCenter.")
            End If

            'LabelStatus.Text = "Connecting to TeamCenter..." + Environment.NewLine + "(You may need to sign in)"
            LabelStatus.Text = "Connecting to TeamCenter.  (You may need to sign in)"
            objDocuments = objApp.Documents

            'Save Cache Path to settings.settings
            Dim cachePath As String = Nothing
            TCE.GetPDMCachePath(cachePath)
            FMain.TCCachePath = cachePath

            LabelStatus.Text = "Searching..."

            'Get the list of item IDs and Revisions from DataGridView
            Dim files As New List(Of Tuple(Of String, String, String))
            Dim numOfFiles As Integer = 0

            Dim ErrorList As New List(Of String)

            For Each row As DataGridViewRow In DataGridViewItems.Rows
                If Not row.IsNewRow Then
                    Dim itemID As String = If(row.Cells("ItemIDs").Value IsNot Nothing, row.Cells("ItemIDs").Value.ToString().Trim(), String.Empty)
                    Dim revision As String = If(row.Cells("rev").Value IsNot Nothing, row.Cells("rev").Value.ToString().Trim(), String.Empty)

                    If Not revision = "" Then
                        LabelStatus.Text = String.Format("Item ID {0}, Revision {1}", itemID, revision)
                    Else
                        LabelStatus.Text = String.Format("Item ID {0}, Revision (not specified)", itemID)
                    End If

                    ' Validate item ID

                    ' ###### Does this apply to all TeamCenter sites? ######

                    If Not System.Text.RegularExpressions.Regex.IsMatch(itemID, "^\d{8}$") Then
                        ErrorList.Add($"Invalid Item ID: {itemID}. It should be 8 digits.")
                        'MessageBox.Show($"Invalid Item ID: {itemID}. It should be 8 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Continue For
                    End If

                    ' If revision is empty, find the latest revision
                    If String.IsNullOrEmpty(revision) Then
                        ' Update status label to indicate the search for the latest revision
                        LabelStatus.Text = "Getting latest revision for " & itemID

                        ' Define attributes for the item
                        Dim itemAttributes(0, 1) As Object
                        Dim revisions As Object = Nothing

                        itemAttributes(0, 0) = "item_id"
                        itemAttributes(0, 1) = itemID

                        ' ###### Does this apply to all TeamCenter sites? ######

                        ' Retrieve all revisions for the item
                        TCE.GetAllRevisions(itemAttributes, "MFK9Item1", revisions)

                        ' Check if revisions were found
                        If revisions IsNot Nothing Then
                            ' Filter revisions to include only those with a single letter (excelud baselines)
                            Dim filteredRevisions As New List(Of String)
                            For i As Integer = 0 To revisions.GetLength(0) - 1
                                Dim rev As String = revisions(i, 0).ToString()

                                ' ###### Does this apply to all TeamCenter sites? ######

                                If System.Text.RegularExpressions.Regex.IsMatch(rev, "^[A-Z]$") Then
                                    filteredRevisions.Add(rev)
                                End If
                            Next

                            ' Check if any valid revisions are left after filtering
                            If filteredRevisions.Count > 0 Then
                                ' Set the revision to the latest one
                                revision = filteredRevisions.Last()
                            Else
                                ' Throw an exception if no valid revisions are found
                                'MessageBox.Show($"No valid revisions found for Item ID: {itemID}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                ErrorList.Add($"No valid revisions found for Item ID: {itemID}.")
                                Continue For
                            End If
                        Else
                            ' Throw an exception if no revisions are found
                            'MessageBox.Show($"No revisions found for Item ID: {itemID}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            ErrorList.Add($"No valid revisions found for Item ID: {itemID}.")
                            Continue For
                        End If
                    End If

                    ' Validate if revision exists
                    LabelStatus.Text = "Validating revision for " + itemID
                    Dim MFKAttributes2(0, 1) As Object
                    Dim RevIdAndUIDs2 As Object = Nothing

                    MFKAttributes2(0, 0) = "item_id"
                    MFKAttributes2(0, 1) = itemID

                    ' ###### Is this function call valid for all TeamCenter sites? ######

                    TCE.GetAllRevisions(MFKAttributes2, "MFK9Item1", RevIdAndUIDs2)

                    Dim revisionExists As Boolean = False
                    If RevIdAndUIDs2 IsNot Nothing Then
                        For i As Integer = 0 To RevIdAndUIDs2.GetLength(0) - 1
                            If RevIdAndUIDs2(i, 0).ToString() = revision Then
                                revisionExists = True
                                Exit For
                            End If
                        Next
                    End If

                    If Not revisionExists Then
                        'MessageBox.Show($"Invalid Revision: {revision} for Item ID: {itemID}. Revision does not exist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        ErrorList.Add($"Invalid Revision: {revision} for Item ID: {itemID}. Revision does not exist.")
                        Continue For
                    End If

                    LabelStatus.Text = "Searching..."
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

            ' Report any errors from the previous loop
            If Not ErrorList.Count = 0 Then
                Dim UC As New UtilsCommon

                Dim s As String = UC.FormatMsgBoxText(ErrorList, "These errors occurred")
                Dim Result = MsgBox(s, vbOKCancel, "Validation Error")
                If Result = MsgBoxResult.Cancel Then Return

                ''MessageBox.Show(s, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            ListViewTeamCenterItems.Items.Clear()

            ' Add files to listview. Don't add any .jt and .pdf and don't add any files that have unchecked filters
            For Each fileTuple As Tuple(Of String, String, String) In files
                Dim fileName As String = fileTuple.Item1
                Dim itemID As String = fileTuple.Item2
                Dim revision As String = fileTuple.Item3
                Dim extension As String = System.IO.Path.GetExtension(fileName).ToLower()

                LabelStatus.Text = String.Format("Adding {0}", fileName)

                ' Determine the file type
                Dim fileType As String = String.Empty
                Select Case extension
                    Case ".dft"
                        fileType = "Draft"
                    Case ".asm"
                        fileType = "Assembly"
                    Case ".par"
                        fileType = "Part"
                    Case ".psm"
                        fileType = "Sheet Metal Part"
                    Case Else
                        fileType = "Unknown"
                End Select

                ' Skip files based on extension and checkbox filters
                If extension = ".pdf" Or extension = ".jt" OrElse
                    (Not CheckBoxAsm.Checked AndAlso extension = ".asm") OrElse
                    (Not CheckBoxPar.Checked AndAlso extension = ".par") OrElse
                    (Not CheckBoxDft.Checked AndAlso extension = ".dft") OrElse
                    (Not CheckBoxPsm.Checked AndAlso extension = ".psm") Then
                    Continue For
                End If

                ' Create a new ListViewItem and add it to the ListView
                Dim listViewItem As New ListViewItem(fileName)
                listViewItem.SubItems.Add(itemID)
                listViewItem.SubItems.Add(revision)
                listViewItem.SubItems.Add(fileType) ' Add the file type as a subitem
                ListViewTeamCenterItems.Items.Add(listViewItem)
            Next

            If numOfFiles = 0 Then
                LabelStatus.Text = "No files found."
            Else
                LabelStatus.Text = numOfFiles.ToString + " files found, filtered down to " + ListViewTeamCenterItems.Items.Count.ToString
            End If

        Catch ex As ApplicationException
            MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            LabelStatus.Text = "Search failed!"
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            LabelStatus.Text = "Search failed!"
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub DownloadAll()
        Dim objApp As SolidEdgeFramework.Application = Nothing
        Dim TCE As SolidEdgeFramework.SolidEdgeTCE = Nothing

        Try
            Cursor.Current = Cursors.WaitCursor
            LabelStatus.Text = "Connecting to Solid Edge..."

            Try
                objApp = CType(Marshal.GetActiveObject("SolidEdge.Application"), SolidEdgeFramework.Application)
            Catch ex As COMException When ex.ErrorCode = &H800401E3
                objApp = CType(CreateObject("SolidEdge.Application"), SolidEdgeFramework.Application)
                objApp.Visible = True
            End Try
            TCE = objApp.SolidEdgeTCE

            LabelStatus.Text = "Adding to cache..."

            ' Download each file in listview
            For Each item As ListViewItem In ListViewTeamCenterItems.Items
                Dim fileName As String = item.Text
                Dim fileItemID As String = item.SubItems(1).Text
                Dim fileItemRevID As String = item.SubItems(2).Text

                LabelStatus.Text = String.Format("Adding {0} to cache...", fileName)

                ' Check if the item is already in ListViewDownloadedFiles
                Dim alreadyDownloaded As Boolean = False
                For Each downloadedItem As ListViewItem In ListViewDownloadedFiles.Items
                    If downloadedItem.Text = fileName Then
                        alreadyDownloaded = True
                        Exit For
                    End If
                Next

                If alreadyDownloaded Then
                    'MessageBox.Show("File Already In Cache", fileName + " is already in the cache folder", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Continue For
                End If

                Dim temp(,) As Object = New Object(1, 1) {}
                TCE.DownladDocumentsFromServerWithOptions(fileItemID, fileItemRevID, fileName, "", "", False, True, 1, temp)
                Dim filePath As String = System.IO.Path.Combine(FMain.TCCachePath, fileName)
                ListViewDownloadedFiles.Items.Add(New ListViewItem(New String() {fileName, filePath}))
            Next

            LabelStatus.Text = "Added to cache!"
        Catch ex As Exception
            LabelStatus.Text = ""
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

        Dim NewWay As Boolean = True
        If NewWay Then
            Dim tmpItem As New ListViewItem

            tmpItem.Text = "Folder"
            tmpItem.ImageKey = "Folder"
            tmpItem.Tag = "Folder"

            tmpItem.SubItems.Add(FMain.TCCachePath)
            tmpItem.Group = FMain.ListViewSources.Groups.Item("Sources")
            tmpItem.Name = FMain.TCCachePath

            If Not FMain.ListViewSources.Items.ContainsKey(tmpItem.Name) Then
                FMain.ListViewSources.Items.Add(tmpItem)
                FMain.ListViewSources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            End If

            FMain.ListViewFilesOutOfDate = True

        Else
            For Each item As ListViewItem In ListViewDownloadedFiles.Items
                Dim fileName As String = item.Text
                Dim filePath As String = System.IO.Path.Combine(FMain.TCCachePath, fileName)

                ' Check if the file already exists in the ListView
                Dim exists As Boolean = False
                For Each existingItem As ListViewItem In FMain.ListViewFiles.Items
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
                    tmpItem.Group = FMain.ListViewFiles.Groups.Item(IO.Path.GetExtension(filePath).ToLower)
                    FMain.ListViewFiles.Items.Add(tmpItem)
                End If
            Next
        End If

        Me.Close()
    End Sub

    Private Sub dataGridViewItems_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridViewItems.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            PasteClipboardData()
        ElseIf e.KeyCode = Keys.Delete Then
            DeleteSelectedCells()
        End If
    End Sub

    Private Sub DeleteSelectedCells()
        For Each cell As DataGridViewCell In DataGridViewItems.SelectedCells
            If Not cell.ReadOnly Then
                cell.Value = Nothing
            End If
        Next

        ' Check and delete rows with all empty cells
        For Each row As DataGridViewRow In DataGridViewItems.Rows
            Dim allCellsEmpty As Boolean = True
            For Each cell As DataGridViewCell In row.Cells
                If cell.Value IsNot Nothing AndAlso cell.Value.ToString().Trim() <> String.Empty Then
                    allCellsEmpty = False
                    Exit For
                End If
            Next
            If allCellsEmpty AndAlso Not row.IsNewRow Then
                DataGridViewItems.Rows.Remove(row)
            End If
        Next
    End Sub

    Private Sub PasteClipboardData()
        Try
            Dim clipboardData As String = Clipboard.GetText()
            Dim lines() As String = clipboardData.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

            For Each line As String In lines
                If Not String.IsNullOrWhiteSpace(line) Then
                    Dim columns() As String = line.Split(vbTab)
                    If columns.Length = 1 Then
                        ' Only item ID is provided, add with an empty revision
                        DataGridViewItems.Rows.Add(columns(0).Trim(), String.Empty)
                    ElseIf columns.Length = 2 Then
                        ' Both item ID and revision are provided
                        DataGridViewItems.Rows.Add(columns(0).Trim(), columns(1).Trim())
                    Else
                        Throw New ApplicationException($"Invalid format: '{line}'. Each line must contain either 1 or 2 columns.")
                    End If
                End If
            Next
        Catch ex As ApplicationException
            MessageBox.Show(ex.Message, "Error pasting", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show($"Error pasting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        CloseSolidEdge()
        Me.Close()
    End Sub

    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
        Dim UD As New UtilsDocumentation

        Dim Tag As String = "select-from-teamcenter"
        Dim HelpURL = UD.GenerateVersionURL(Tag)
        System.Diagnostics.Process.Start(HelpURL)

    End Sub

    Private Sub ButtonSettings_Click(sender As Object, e As EventArgs) Handles ButtonSettings.Click
        Dim FTCS As New FormTeamCenterSettings(Me.FMain)

        FTCS.ShowDialog()

        If FTCS.DialogResult = DialogResult.OK Then
            Me.FMain.TCItemIDRx = FTCS.TCItemIDRx
            Me.FMain.TCRevisionRx = FTCS.TCRevisionRx
        End If

    End Sub

End Class
