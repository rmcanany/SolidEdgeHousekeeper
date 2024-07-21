Option Strict On
Imports Newtonsoft.Json

Public Class TaskUpdatePhysicalProperties
    Inherits Task

    Public Property HideSymbols As Boolean
    Public Property ShowSymbols As Boolean

    Enum ControlNames
        HideSymbols
        ShowSymbols
        HideOptions
    End Enum


    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = True
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = False
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskUpdatePhysicalProperties
        Me.Category = "Update"
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.HideSymbols = False
        Me.ShowSymbols = False

    End Sub


    Public Overrides Function Process(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        ErrorMessage = InvokeSTAThread(
                               Of SolidEdgeFramework.SolidEdgeDocument,
                               Dictionary(Of String, String),
                               SolidEdgeFramework.Application,
                               Dictionary(Of Integer, List(Of String)))(
                                   AddressOf ProcessInternal,
                                   SEDoc,
                                   Configuration,
                                   SEApp)

        Return ErrorMessage

    End Function

    Public Overrides Function Process(ByVal FileName As String) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Return ErrorMessage

    End Function

    Private Function ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim s As String
        Dim Proceed As Boolean

        Dim DocVariableDict As New Dictionary(Of String, SolidEdgeFramework.variable)
        'Dim VariableFound As Boolean

        Dim MaterialTable As SolidEdgeFramework.MatTable = SEApp.GetMaterialTable()
        Dim PropertyType As SolidEdgeFramework.MatTablePropIndexConstants
        PropertyType = SolidEdgeFramework.MatTablePropIndexConstants.seDensity
        'Dim PropValue As String = Nothing
        Dim PropValue As Object = Nothing
        Dim Models As SolidEdgePart.Models = Nothing
        Dim Model As SolidEdgePart.Model


        Dim Density As Double

        ' How to display center of mass
        'https://community.sw.siemens.com/s/question/0D5KZ0000063E8W0AU/show-center-of-mass-using-api
        'https://community.sw.siemens.com/s/question/0D5KZ000006oz1C0AQ/solid-edge-housekeeper-v20241-released
        'For parts, you need To set true\false the property
        'objPar.Models.Item(x).DisplayCenterOfMass

        'In assembly you need to set true\false the property
        'objAsm.PhisicalProperties.DisplayCenterOfMass

        'For parts and sheet metals its called just "UpdateOnFileSave"


        Dim TC As New Task_Common

        Dim DocType As String = TC.GetDocType(SEDoc)

        Select Case DocType
            Case "asm"
                Try
                    Dim tmpSEDoc As SolidEdgeAssembly.AssemblyDocument = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)

                    Dim PhysicalProperties As SolidEdgeAssembly.PhysicalProperties = tmpSEDoc.PhysicalProperties

                    Dim ParFileNamesWithoutDensity() As String
                    Dim ParFileNamesWithoutDensityArray As Array = {""}

                    Dim Occurrences As SolidEdgeAssembly.Occurrences
                    Occurrences = tmpSEDoc.Occurrences

                    Proceed = True

                    If Occurrences.Count = 0 Then
                        Proceed = False
                        'ExitStatus = 1
                        'ErrorMessageList.Add("No models found")
                    End If

                    If Proceed Then
                        PhysicalProperties.UpdateV2(ParFileNamesWithoutDensityArray)
                        SEApp.DoIdle()

                        ' Try again for assemblies whose parts have also been updated
                        If Not TC.IsVariablePresent(SEDoc, "Mass") Then
                            SEDoc.Save()
                            SEApp.DoIdle()
                            ParFileNamesWithoutDensityArray = {""}
                            PhysicalProperties.UpdateV2(ParFileNamesWithoutDensityArray)
                            SEApp.DoIdle()
                        End If

                        ParFileNamesWithoutDensity = CType(ParFileNamesWithoutDensityArray, String())

                        If Not ParFileNamesWithoutDensity Is Nothing Then
                            If ParFileNamesWithoutDensity.Count > 0 Then
                                ExitStatus = 1
                                s = String.Format("Found {0} models with no density assigned.", ParFileNamesWithoutDensity.Count)
                                s = String.Format("{0}  Please verify results.", s)
                                ErrorMessageList.Add(s)
                            End If
                        End If

                        If Me.HideSymbols Then
                            PhysicalProperties.DisplayCenterOfMass = False
                            PhysicalProperties.DisplayPrincipalAxes = False
                            PhysicalProperties.DisplayCenterOfVolume = False
                        End If
                        If Me.ShowSymbols Then
                            PhysicalProperties.DisplayCenterOfMass = True
                            PhysicalProperties.DisplayPrincipalAxes = True
                            PhysicalProperties.DisplayCenterOfVolume = True
                        End If

                        tmpSEDoc.Save()
                        SEApp.DoIdle()

                    End If
                Catch ex As Exception
                    ExitStatus = 1
                    ErrorMessageList.Add("Error updating physical properties.")
                End Try

            Case "par"
                Dim tmpSEDoc As SolidEdgePart.PartDocument = CType(SEDoc, SolidEdgePart.PartDocument)

                Models = tmpSEDoc.Models

                MaterialTable.GetMaterialPropValueFromDoc(tmpSEDoc, CType(PropertyType, SolidEdgeFramework.MatTablePropIndex), PropValue)
                Density = CDbl(PropValue)

            Case "psm"
                Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument = CType(SEDoc, SolidEdgePart.SheetMetalDocument)

                Models = tmpSEDoc.Models

                MaterialTable.GetMaterialPropValueFromDoc(tmpSEDoc, CType(PropertyType, SolidEdgeFramework.MatTablePropIndex), PropValue)
                Density = CDbl(PropValue)

            Case Else
                MsgBox(String.Format("{0} DocType '{1}' not recognized", Me.Name, DocType))
        End Select

        If (DocType = "par") Or (DocType = "psm") Then

            Proceed = True

            SEApp.StartCommand(CType(SolidEdgeConstants.PartCommandConstants.PartToolsPhysicalProperties,
                                       SolidEdgeFramework.SolidEdgeCommandConstants))
            SEApp.DoIdle()

            If Models Is Nothing Then
                Proceed = False
            End If

            If Proceed Then
                If Models.Count = 0 Then
                    Proceed = False
                End If
            End If

            If Proceed Then
                If Density <= 0 Then
                    Proceed = False
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("Density set to {0}", Density))
                End If

            End If

            If Proceed Then
                For Each Model In Models
                    If Me.HideSymbols Then
                        ' Can crash on certain models
                        Try
                            Model.DisplayCenterOfMass = False
                            Model.DisplayPrincipalAxes = False
                            Model.DisplayCenterOfVolume = False
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add("Error reported hiding symbols.  Please verify results.")
                        End Try
                    End If
                    If Me.ShowSymbols Then
                        Try
                            Model.DisplayCenterOfMass = True
                            Model.DisplayPrincipalAxes = True
                            Model.DisplayCenterOfVolume = True
                        Catch ex As Exception
                            ExitStatus = 1
                            ErrorMessageList.Add("Error reported hiding symbols.  Please verify results.")
                        End Try
                    End If

                Next
            End If

        End If

        If Proceed Then
            If Not TC.IsVariablePresent(SEDoc, "Mass") Then
                ExitStatus = 1
                ErrorMessageList.Add("Unable to add 'Mass' to the variable table")
            End If
        End If

        If SEDoc.ReadOnly Then
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

        'Dim IU As New InterfaceUtilities

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 3)

        RowIndex = 0

        CheckBox = FormatOptionsCheckBox(ControlNames.ShowSymbols.ToString, "Show symbols")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1
        CheckBox = FormatOptionsCheckBox(ControlNames.HideSymbols.ToString, "Hide symbols")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1
        CheckBox = FormatOptionsCheckBox(ControlNames.HideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function


    'ERROR CHECKING

    Private Sub InitializeOptionProperties()
        Dim CheckBox As CheckBox

        CheckBox = CType(ControlsDict(ControlNames.ShowSymbols.ToString), CheckBox)
        Me.ShowSymbols = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.HideSymbols.ToString), CheckBox)
        Me.HideSymbols = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.HideOptions.ToString), CheckBox)
        Me.AutoHideOptions = CheckBox.Checked

    End Sub

    Public Overrides Function CheckStartConditions(
        PriorErrorMessage As Dictionary(Of Integer, List(Of String))
        ) As Dictionary(Of Integer, List(Of String))

        Dim PriorExitStatus As Integer = PriorErrorMessage.Keys(0)

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))
        Dim ExitStatus As Integer = 0
        Dim ErrorMessageList = PriorErrorMessage(PriorExitStatus)
        Dim Indent = "    "

        If Me.IsSelectedTask Then
            ' Check start conditions.
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one type of file to process", Indent))
            End If

            If Me.ShowSymbols And Me.HideSymbols Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Cannot both show and hide symbols", Indent))
            End If
        End If


        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    'EVENT HANDLERS

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name
        'Dim Ctrl As Control
        'Dim Button As Button

        Select Case Name
            Case ControlNames.ShowSymbols.ToString
                Me.ShowSymbols = Checkbox.Checked

            Case ControlNames.HideSymbols.ToString
                Me.HideSymbols = Checkbox.Checked

            Case ControlNames.HideOptions.ToString
                HandleHideOptionsChange(Me, Me.TaskOptionsTLP, Checkbox)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String

        HelpString = "Updates mass, volume, etc.  Models with no density are reported in the log file. "
        HelpString += vbCrLf + vbCrLf + "You can optionally control the display of the center of mass symbol. "
        HelpString += "It can either be shown, hidden, or left unchanged. "
        HelpString += "The option is set on the Options panel. "
        HelpString += "To leave the symbol's display unchanged, "
        HelpString += "disable both the `Show` and `Hide` options. "
        HelpString += "Note, controlling the symbol display only works for assembly files at this time. "
        HelpString += vbCrLf + vbCrLf + "Occasionally, the physical properties are updated correctly, "
        HelpString += "but the results are not carried over to the Variable Table. "
        HelpString += "The error is detected and reported in the log file. The easiest fix I've found "
        HelpString += "is to open the file in SE, change the material, then change it right back. "
        HelpString += "You can verify if it worked by checking for `Mass` in the Variable Table. "

        Return HelpString
    End Function


End Class
