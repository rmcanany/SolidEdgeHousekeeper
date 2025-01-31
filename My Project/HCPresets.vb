Option Strict On
Imports Newtonsoft.Json

Public Class HCPresets
    Public Property Items As List(Of Preset)

    Public Sub New()

        Me.Items = New List(Of Preset)

        Dim UP As New UtilsPreferences

        Dim JSONString As String
        Dim Infile As String = UP.GetPresetsFilename(CheckExisting:=True)

        If Not Infile = "" Then
            JSONString = IO.File.ReadAllText(Infile)

            Dim tmpList As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(JSONString)

            For Each PresetJSONString As String In tmpList
                Dim Item As New Preset
                Item.FromJSON(PresetJSONString)
                Me.Items.Add(Item)
            Next
        End If

    End Sub

    Public Sub Save()

        Dim UP As New UtilsPreferences
        Dim Outfile As String = UP.GetPresetsFilename(CheckExisting:=False)

        Dim tmpList As New List(Of String)
        For Each Item As Preset In Me.Items
            tmpList.Add(Item.ToJSON)
        Next

        Dim JSONString As String = JsonConvert.SerializeObject(tmpList)
        IO.File.WriteAllText(Outfile, JSONString)

    End Sub
End Class

Public Class Preset
    Public Property Name As String
    Public Property TaskListJSON As String
    Public Property FormSettingsJSON As String
    Public Property PropertyFiltersJSON As String

    Public Sub New()

    End Sub

    Public Function ToJSON() As String

        Dim JSONString As String = Nothing

        Dim tmpPresetDict As New Dictionary(Of String, String)

        tmpPresetDict("Name") = Me.Name
        tmpPresetDict("TaskListJSON") = Me.TaskListJSON
        tmpPresetDict("FormSettingsJSON") = Me.FormSettingsJSON
        tmpPresetDict("PropertyFiltersJSON") = Me.PropertyFiltersJSON

        JSONString = JsonConvert.SerializeObject(tmpPresetDict)

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)

        Try
            Dim tmpPresetDict As Dictionary(Of String, String)

            tmpPresetDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

            Me.Name = tmpPresetDict("Name")
            Me.TaskListJSON = tmpPresetDict("TaskListJSON")
            Me.FormSettingsJSON = tmpPresetDict("FormSettingsJSON")
            Me.PropertyFiltersJSON = tmpPresetDict("PropertyFiltersJSON")
        Catch ex As Exception

        End Try

    End Sub

End Class

