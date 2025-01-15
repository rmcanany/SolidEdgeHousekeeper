Option Strict On

Public Class UtilsPrinters

    Public Function GetInstalledPrinterNames() As List(Of String)
        Dim PrinterList As New List(Of String)
        Dim InstalledPrinter As String
        For Each InstalledPrinter In System.Drawing.Printing.PrinterSettings.InstalledPrinters
            PrinterList.Add(InstalledPrinter)
        Next InstalledPrinter

        Return PrinterList
    End Function

    Public Function GetSheetSizesFromNames(Names As List(Of String)) As Dictionary(Of String, SolidEdgeDraft.PaperSizeConstants)
        Dim SheetSizeDict As New Dictionary(Of String, SolidEdgeDraft.PaperSizeConstants)
        Dim PaperSizeConstant As SolidEdgeDraft.PaperSizeConstants
        Dim Name As String
        Dim tmpName As String

        For Each Name In Names
            For Each PaperSizeConstant In System.Enum.GetValues(GetType(SolidEdgeDraft.PaperSizeConstants))
                tmpName = PaperSizeConstant.ToString
                tmpName = tmpName.Replace("ig", "")
                tmpName = tmpName.Replace("Eng", "Eng ")
                tmpName = tmpName.Replace("Ansi", "Ansi ")
                tmpName = tmpName.Replace("Iso", "Iso ")
                tmpName = tmpName.Replace("Tall", " Tall")
                tmpName = tmpName.Replace("Wide", " Wide")

                If Name = tmpName Then
                    SheetSizeDict(Name) = PaperSizeConstant
                    Exit For
                End If
            Next

        Next

        Return SheetSizeDict
    End Function

    Public Function GetSheetSizes(Filter As String) As Dictionary(Of String, SolidEdgeDraft.PaperSizeConstants)
        Dim SheetSizeDict As New Dictionary(Of String, SolidEdgeDraft.PaperSizeConstants)
        Dim PaperSizeConstant As SolidEdgeDraft.PaperSizeConstants
        Dim Name As String = ""

        For Each PaperSizeConstant In System.Enum.GetValues(GetType(SolidEdgeDraft.PaperSizeConstants))

            ' PaperSizeConstant.ToString format is igEngFolioTall, igAnsiBWide, igIsoA4Tall, etc.
            ' Convert it some something more readable.
            Name = PaperSizeConstant.ToString
            Name = Name.Replace("ig", "")
            Name = Name.Replace("Eng", "Eng ")
            Name = Name.Replace("Ansi", "Ansi ")
            Name = Name.Replace("Iso", "Iso ")
            Name = Name.Replace("Tall", " Tall")
            Name = Name.Replace("Wide", " Wide")

            If Filter.ToLower = "ansi" Then
                If Name.ToLower.Contains("ansi") Then
                    SheetSizeDict(Name) = PaperSizeConstant
                End If
            End If

            If Filter.ToLower = "iso" Then
                If Name.ToLower.Contains("iso a") Then
                    SheetSizeDict(Name) = PaperSizeConstant
                End If
            End If

            If Filter.ToLower = "all" Then
                If Not Name.ToLower.Contains("custom") Then
                    SheetSizeDict(Name) = PaperSizeConstant
                End If
            End If
        Next

        ' MsgBox(SheetSizeDict.Keys(0))
        Return SheetSizeDict
    End Function

    'Private Sub LoadPrinterSettings()
    '    Dim s As String
    '    Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
    '    Dim PrintSettingsFilename As String = String.Format("{0}\Preferences\print_settings.dat", StartupPath)

    '    Try
    '        s = IO.File.ReadAllText(PrintSettingsFilename)
    '        PrintDialog1.PrinterSettings = PrinterSettingsFromString(s)
    '    Catch ex As Exception
    '        'MsgBox("Error loading printer settings")
    '    End Try

    'End Sub

    'Private Sub SavePrinterSettings()
    '    Dim s As String
    '    Dim StartupPath As String = System.Windows.Forms.Application.StartupPath()
    '    Dim PrintSettingsFilename As String = String.Format("{0}\Preferences\print_settings.dat", StartupPath)
    '    Try
    '        s = PrinterSettingsToString(PrintDialog1.PrinterSettings)
    '        IO.File.WriteAllText(PrintSettingsFilename, s)
    '    Catch ex As Exception
    '        MsgBox(String.Format("Unable to save print settings file '{0}'", PrintSettingsFilename))
    '    End Try

    'End Sub
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
