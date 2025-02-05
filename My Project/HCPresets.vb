Option Strict On

Imports System.Reflection
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

                Try
                    Item.FromJSON(PresetJSONString)
                Catch ex As Exception
                    Me.Items.Clear()

                    Dim s As String = String.Format("Unable to load saved Presets.{0}", vbCrLf)
                    s = String.Format("{0}Reported error: '{1}'.", s, ex.Message)
                    MsgBox(ex.Message)

                    Exit Sub
                End Try
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

        If Not CheckJSONDict(tmpPresetDict) Then
            MsgBox(String.Format("{0}: Extra or missing property names in JSON dictionary", Me.ToString))
            JSONString = ""
        Else
            JSONString = JsonConvert.SerializeObject(tmpPresetDict)
        End If

        Return JSONString
    End Function

    Public Sub FromJSON(JSONString As String)

        Try
            Dim tmpPresetDict As Dictionary(Of String, String)

            tmpPresetDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

            If Not CheckJSONDict(tmpPresetDict) Then
                Throw New Exception(String.Format("{0}: Extra or missing property names in JSON dictionary", Me.ToString))
            End If

            Me.Name = tmpPresetDict("Name")
            Me.TaskListJSON = tmpPresetDict("TaskListJSON")
            Me.FormSettingsJSON = tmpPresetDict("FormSettingsJSON")
            Me.PropertyFiltersJSON = tmpPresetDict("PropertyFiltersJSON")
        Catch ex As Exception

        End Try

    End Sub

    Private Function CheckJSONDict(JSONDict As Dictionary(Of String, String)) As Boolean
        Dim Proceed As Boolean = True

        Dim PropInfos() As PropertyInfo = Me.GetType.GetProperties()

        ' Check for missing info
        For Each PropInfo As PropertyInfo In PropInfos
            If Not JSONDict.Keys.Contains(PropInfo.Name) Then
                Proceed = False
                Exit For
            End If
        Next

        ' Surplus info can safely be ignored
        ' Check for surplus info
        'If Proceed Then
        '    For Each PropertyName As String In JSONDict.Keys
        '        If Not PropertyNamesList.Contains(PropertyName) Then
        '            Proceed = False
        '            Exit For
        '        End If
        '    Next
        'End If

        Return Proceed
    End Function


End Class

