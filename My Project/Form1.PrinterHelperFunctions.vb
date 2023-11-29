Partial Class Form1

    Private Sub LoadPrinterSettings()
        Dim s As String
        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim PrintSettingsFilename As String = String.Format("{0}\Preferences\print_settings.dat", StartupPath)

        Try
            s = IO.File.ReadAllText(PrintSettingsFilename)
            PrintDialog1.PrinterSettings = PrinterSettingsFromString(s)
        Catch ex As Exception
            'MsgBox("Error loading printer settings")
        End Try

    End Sub

    Private Sub SavePrinterSettings()
        Dim s As String
        Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
        Dim PrintSettingsFilename As String = String.Format("{0}\Preferences\print_settings.dat", StartupPath)

        Try
            s = PrinterSettingsToString(PrintDialog1.PrinterSettings)
            IO.File.WriteAllText(PrintSettingsFilename, s)
        Catch ex As Exception
            MsgBox(String.Format("Unable to save print settings file '{0}'", PrintSettingsFilename))
        End Try

    End Sub
    Private Function PrinterSettingsToString(settings As Printing.PrinterSettings) As String
        ' https://stackoverflow.com/questions/32945127/save-printdialog-configuration-for-next-time-open
        ' https://stackoverflow.com/questions/33184970/how-to-save-printdocument-printersettings-in-a-file-so-that-user-have-not-to-se

        If settings Is Nothing Then
            Return Nothing
        End If

        Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        Using ms As New IO.MemoryStream()
            bf.Serialize(ms, settings)
            Return Convert.ToBase64String(ms.ToArray())
        End Using

    End Function

    Private Function PrinterSettingsFromString(base64 As String) As Printing.PrinterSettings
        Try
            Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Using ms As New IO.MemoryStream(Convert.FromBase64String(base64))
                Return CType(bf.Deserialize(ms), Printing.PrinterSettings)
            End Using
        Catch ex As Exception
            Return New Printing.PrinterSettings()
        End Try
    End Function

End Class
