Option Strict On

Public Class TaskUpdatePhysicalProperties
    Inherits Task

    Private _HideSymbols As Boolean
    Public Property HideSymbols As Boolean
        Get
            Return _HideSymbols
        End Get
        Set(value As Boolean)
            _HideSymbols = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.HideSymbols.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _ShowSymbols As Boolean
    Public Property ShowSymbols As Boolean
        Get
            Return _ShowSymbols
        End Get
        Set(value As Boolean)
            _ShowSymbols = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.ShowSymbols.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _AutoHideOptions As Boolean
    Public Property AutoHideOptions As Boolean
        Get
            Return _AutoHideOptions
        End Get
        Set(value As Boolean)
            _AutoHideOptions = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AutoHideOptions.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property


    Enum ControlNames
        HideSymbols
        ShowSymbols
        AutoHideOptions
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


    Public Overrides Sub Process(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application)

        Me.TaskLogger = Me.FileLogger.AddLogger(Me.Description)

        InvokeSTAThread(
            Of SolidEdgeFramework.SolidEdgeDocument,
            SolidEdgeFramework.Application)(
                AddressOf ProcessInternal,
                SEDoc,
                SEApp)
    End Sub

    Public Overrides Sub Process(ByVal FileName As String)
        Me.TaskLogger = Me.FileLogger.AddLogger(Me.Description)
    End Sub

    Private Sub ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        Dim s As String
        Dim Proceed As Boolean

        Dim DocVariableDict As New Dictionary(Of String, SolidEdgeFramework.variable)

        Dim MaterialTable As SolidEdgeFramework.MatTable = SEApp.GetMaterialTable()
        Dim PropertyType As SolidEdgeFramework.MatTablePropIndexConstants
        PropertyType = SolidEdgeFramework.MatTablePropIndexConstants.seDensity
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


        Dim UC As New UtilsCommon

        Dim DocType As String = UC.GetDocType(SEDoc)

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
                    End If

                    If Proceed Then
                        PhysicalProperties.UpdateV2(ParFileNamesWithoutDensityArray)
                        SEApp.DoIdle()

                        ' Try again for assemblies whose parts have also been updated
                        If Not UC.IsVariablePresent(SEDoc, "Mass") Then
                            SEDoc.Save()
                            SEApp.DoIdle()
                            ParFileNamesWithoutDensityArray = {""}
                            PhysicalProperties.UpdateV2(ParFileNamesWithoutDensityArray)
                            SEApp.DoIdle()
                        End If

                        ParFileNamesWithoutDensity = CType(ParFileNamesWithoutDensityArray, String())

                        If Not ParFileNamesWithoutDensity Is Nothing Then
                            If ParFileNamesWithoutDensity.Count > 0 Then
                                s = String.Format("Found {0} models with no density assigned.", ParFileNamesWithoutDensity.Count)
                                s = String.Format("{0}  Please verify results.", s)
                                TaskLogger.AddMessage(s)

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
                    TaskLogger.AddMessage("Unable to update physical properties.")
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
                    TaskLogger.AddMessage(String.Format("Density set to {0}", Density))
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
                            TaskLogger.AddMessage("Issue reported when hiding symbols.  Please verify results.")
                        End Try
                    End If
                    If Me.ShowSymbols Then
                        Try
                            Model.DisplayCenterOfMass = True
                            Model.DisplayPrincipalAxes = True
                            Model.DisplayCenterOfVolume = True
                        Catch ex As Exception
                            TaskLogger.AddMessage("Issue reported showing symbols.  Please verify results.")
                        End Try
                    End If

                Next
            End If

        End If

        If Proceed Then
            If Not UC.IsVariablePresent(SEDoc, "Mass") Then
                TaskLogger.AddMessage("Unable to add 'Mass' to the variable table")
            End If
        End If

        If SEDoc.ReadOnly Then
            TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
        Else
            SEDoc.Save()
            SEApp.DoIdle()
        End If

    End Sub


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox

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
        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        Return tmpTLPOptions
    End Function

    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If

            If Me.ShowSymbols And Me.HideSymbols Then
                ErrorLogger.AddMessage("Cannot both show and hide symbols")
            End If

        End If

    End Sub


    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name

        Select Case Name
            Case ControlNames.ShowSymbols.ToString
                Me.ShowSymbols = Checkbox.Checked

            Case ControlNames.HideSymbols.ToString
                Me.HideSymbols = Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String

        HelpString = "Updates mass, volume, etc.  Models with no assigned density are reported in the log file. "

        HelpString += vbCrLf + vbCrLf + "![Update Physical Properties](My%20Project/media/task_update_physical_properties.png)"

        HelpString += vbCrLf + vbCrLf + "You can optionally control the display of the physical properties symbols. "
        HelpString += "They can either be shown, hidden, or left unchanged. "
        HelpString += "To leave their display unchanged, "
        HelpString += "disable both the `Show` and `Hide` options. "

        HelpString += vbCrLf + vbCrLf + "Occasionally, the physical properties are updated correctly, "
        HelpString += "but the results are not carried over to the Variable Table. "
        HelpString += "The error is detected and reported in the log file. One fix that often works "
        HelpString += "is to open the file in SE, change the material, then change it back. "
        HelpString += "To see if it worked, run `Inspect > Physical Properties`, "
        HelpString += "then check for `Mass` in the Variable Table. "

        Return HelpString
    End Function


End Class
