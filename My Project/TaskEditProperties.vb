Option Strict On

Imports Newtonsoft.Json
Imports OpenMcdf
Imports OpenMcdf.Extensions
Imports OpenMcdf.Extensions.OLEProperties
Imports System.IO
Imports System.Text.RegularExpressions

Public Class TaskEditProperties
    Inherits Task

    Public Property JSONDict As String
    Public Property AutoAddMissingProperty As Boolean
    Public Property AutoUpdateMaterial As Boolean
    Public Property ActiveMaterialLibrary As String
    Public Property RemoveFaceStyleOverrides As Boolean
    Public Property StructuredStorageEdit As Boolean

    Enum ControlNames
        Edit
        JSONDict
        AutoAddMissingProperty
        AutoUpdateMaterial
        Browse
        ActiveMaterialLibrary
        RemoveFaceStyleOverrides
        StructuredStorageEdit
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
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskEditPropertiesEx
        Me.Category = "Edit"

        SetColorFromCategory(Me)

        ' Options
        Me.JSONDict = ""
        Me.AutoAddMissingProperty = False
        Me.AutoUpdateMaterial = False
        Me.ActiveMaterialLibrary = ""
        Me.RemoveFaceStyleOverrides = False
        Me.StructuredStorageEdit = False
        Me.SolidEdgeRequired = False

    End Sub

    Public Sub New(Task As TaskEditProperties)

        ' Options
        Me.JSONDict = Task.JSONDict
        Me.AutoAddMissingProperty = Task.AutoAddMissingProperty
        Me.AutoUpdateMaterial = Task.AutoUpdateMaterial
        Me.StructuredStorageEdit = Task.StructuredStorageEdit

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

        ErrorMessage = ProcessInternal(FileName)

        Return ErrorMessage

    End Function

    Private Overloads Function ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        ' Convert glob to regex 
        ' https://stackoverflow.com/questions/74683013/regex-to-glob-and-vice-versa-conversion
        ' https://stackoverflow.com/questions/11276909/how-to-convert-between-a-glob-pattern-and-a-regexp-pattern-in-ruby
        ' https://learn.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim PropertySets As SolidEdgeFramework.PropertySets = Nothing
        Dim Properties As SolidEdgeFramework.Properties = Nothing
        Dim Prop As SolidEdgeFramework.Property = Nothing

        Dim PropertySetName As String = ""
        Dim PropertyName As String = ""
        Dim FindString As String = ""
        Dim ReplaceString As String = ""
        Dim FindSearchType As String = ""
        Dim ReplaceSearchType As String = ""

        Dim PropertyFound As Boolean = False
        'Dim AutoAddMissingProperty As Boolean = Configuration("CheckBoxAutoAddMissingProperty").ToLower = "true"

        Dim Proceed As Boolean = True
        Dim s As String

        Dim PropertiesToEditDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim PropertiesToEdit As String = ""
        Dim RowIndexString As String

        Dim TC As New Task_Common

        PropertiesToEdit = Me.JSONDict

        Dim DocType As String = TC.GetDocType(SEDoc)
        'If DocType = "asm" Then PropertiesToEdit = Configuration("TextBoxPropertiesEditAssembly")
        'If DocType = "par" Then PropertiesToEdit = Configuration("TextBoxPropertiesEditPart")
        'If DocType = "psm" Then PropertiesToEdit = Configuration("TextBoxPropertiesEditSheetmetal")
        'If DocType = "dft" Then PropertiesToEdit = Configuration("TextBoxPropertiesEditDraft")

        If Not PropertiesToEdit = "" Then
            PropertiesToEditDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(PropertiesToEdit)

            'PropertiesToEditDict format
            '{"1":{
            '    "PropertySet":"System",
            '    "PropertyName":"Material",
            '    "Find_PT":"True",
            '    "Find_WC":"False",
            '    "Find_RX":"False",
            '    "FindString":"Aluminum",
            '    "Replace_PT":"True",
            '    "Replace_RX":"False",
            '    "ReplaceString":"Aluminum 6061-T6"},
            ' ...
            '}

        Else
            ExitStatus = 1
            ErrorMessageList.Add("No properties provided")
        End If

        For Each RowIndexString In PropertiesToEditDict.Keys

            Proceed = True

            PropertyName = PropertiesToEditDict(RowIndexString)("PropertyName")
            PropertySetName = PropertiesToEditDict(RowIndexString)("PropertySet")
            FindString = PropertiesToEditDict(RowIndexString)("FindString")
            ReplaceString = PropertiesToEditDict(RowIndexString)("ReplaceString")

            If PropertiesToEditDict(RowIndexString)("Find_PT").ToLower = "true" Then
                FindSearchType = "PT"
            ElseIf PropertiesToEditDict(RowIndexString)("Find_WC").ToLower = "true" Then
                FindSearchType = "WC"
            Else
                FindSearchType = "RX"
            End If

            If PropertiesToEditDict(RowIndexString)("Replace_PT").ToLower = "true" Then
                ReplaceSearchType = "PT"
            ElseIf PropertiesToEditDict(RowIndexString)("Replace_RX").ToLower = "true" Then
                ReplaceSearchType = "RX"
            Else
                ReplaceSearchType = "EX"
            End If

            If Proceed Then
                Try
                    FindString = TC.SubstitutePropertyFormula(SEDoc, Nothing, SEDoc.FullName, FindString, ValidFilenameRequired:=False)
                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Unable to process formula in Find text '{0}' for property '{1}'", FindString, PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

                Try
                    ReplaceString = TC.SubstitutePropertyFormula(SEDoc, Nothing, SEDoc.FullName, ReplaceString, ValidFilenameRequired:=False, ReplaceSearchType = "EX")
                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Unable to process formula in Replace text '{0}' for property '{1}'", ReplaceString, PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try
            End If

            If Proceed Then

                Try

                    Prop = TC.GetProp(SEDoc, PropertySetName, PropertyName, 0, AutoAddMissingProperty)
                    If Prop Is Nothing Then
                        Proceed = False
                        ExitStatus = 1
                        s = String.Format("Property '{0}.{1}' not found or not recognized.", PropertySetName, PropertyName)
                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                    End If

                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Property '{0}.{1}' not found or not recognized.", PropertySetName, PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

            End If

            If Proceed Then

                Try

                    If ReplaceSearchType = "EX" Then

                    End If

                    If FindSearchType = "PT" Then
                        Prop.Value = Replace(CType(Prop.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)
                    Else
                        If FindSearchType = "WC" Then
                            FindString = TC.GlobToRegex(FindString)
                        End If
                        If ReplaceSearchType = "PT" Then
                            ' ReplaceString = Regex.Escape(ReplaceString)
                        End If

                        Prop.Value = Regex.Replace(CType(Prop.Value, String), FindString, ReplaceString, RegexOptions.IgnoreCase)

                    End If
                    ' Properties.Save()

                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Unable to replace property value '{0}'.  This command only works on text type properties.", PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

            End If

            If Proceed Then

                Try

                    If ReplaceString = "%{DeleteProperty}" Then
                        Prop.Delete()
                    End If

                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Unable to delete property value '{0}'.  This command only works on custom properties.", PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

            End If

            If Proceed Then
                Try

                    PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)
                    For Each Properties In PropertySets
                        Properties.Save()
                        SEApp.DoIdle()
                    Next
                    If SEDoc.ReadOnly Then
                        ExitStatus = 1
                        s = "Cannot save document marked 'Read Only'"
                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                    Else
                        SEDoc.Save()
                        SEApp.DoIdle()
                    End If

                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = "Problem accessing or saving Property."
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try
            End If

            ' If the changed property was System.Material, need to update properties from the material table.
            If Proceed Then
                PropertyName = PropertiesToEditDict(RowIndexString)("PropertyName")
                PropertySetName = PropertiesToEditDict(RowIndexString)("PropertySet")

                If (PropertyName.ToLower = "material") And (PropertySetName.ToLower = "system") Then

                    If Me.AutoUpdateMaterial Then

                        Select Case DocType
                            Case "par", "psm"
                                Dim MaterialDoctor As New MaterialDoctor
                                SupplementalErrorMessage = MaterialDoctor.UpdateMaterialFromMaterialTable(
                                        SEDoc, Me.ActiveMaterialLibrary, Me.RemoveFaceStyleOverrides, SEApp)

                                AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)

                            Case Else
                                ' Not an error

                        End Select

                    End If
                End If
            End If

        Next

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

    Private Overloads Function ProcessInternal(ByVal FullName As String) As Dictionary(Of Integer, List(Of String))

        ' Convert glob to regex 
        ' https://stackoverflow.com/questions/74683013/regex-to-glob-and-vice-versa-conversion
        ' https://stackoverflow.com/questions/11276909/how-to-convert-between-a-glob-pattern-and-a-regexp-pattern-in-ruby
        ' https://learn.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim PropertySetName As String = ""
        Dim PropertyName As String = ""
        Dim FindString As String = ""
        Dim ReplaceString As String = ""
        Dim FindSearchType As String = ""
        Dim ReplaceSearchType As String = ""

        Dim PropertyFound As Boolean = False

        Dim Proceed As Boolean = True
        Dim s As String

        Dim PropertiesToEditDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim PropertiesToEdit As String = ""
        Dim RowIndexString As String

        Dim TC As New Task_Common

        PropertiesToEdit = Me.JSONDict

        If Not PropertiesToEdit = "" Then
            PropertiesToEditDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(PropertiesToEdit)
        Else
            ExitStatus = 1
            ErrorMessageList.Add("No properties provided")
        End If

        Dim cfg As CFSConfiguration = CFSConfiguration.SectorRecycle Or CFSConfiguration.EraseFreeSectors
        Dim fs As FileStream = New FileStream(FullName, FileMode.Open, FileAccess.ReadWrite)
        Dim cf As CompoundFile = New CompoundFile(fs, CFSUpdateMode.Update, cfg)

        Dim dsiStream As CFStream = cf.RootStorage.GetStream("SummaryInformation")
        Dim co As OLEPropertiesContainer = dsiStream.AsOLEPropertiesContainer

        Dim dsiStream2 As CFStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
        Dim co2 As OLEPropertiesContainer = dsiStream2.AsOLEPropertiesContainer

        Dim OLEProp As OLEProperty = Nothing

        For Each RowIndexString In PropertiesToEditDict.Keys

            Proceed = True

            PropertyName = PropertiesToEditDict(RowIndexString)("PropertyName")
            PropertySetName = PropertiesToEditDict(RowIndexString)("PropertySet")
            FindString = PropertiesToEditDict(RowIndexString)("FindString")
            ReplaceString = PropertiesToEditDict(RowIndexString)("ReplaceString")

            If PropertiesToEditDict(RowIndexString)("Find_PT").ToLower = "true" Then
                FindSearchType = "PT"
            ElseIf PropertiesToEditDict(RowIndexString)("Find_WC").ToLower = "true" Then
                FindSearchType = "WC"
            Else
                FindSearchType = "RX"
            End If

            If PropertiesToEditDict(RowIndexString)("Replace_PT").ToLower = "true" Then
                ReplaceSearchType = "PT"
            ElseIf PropertiesToEditDict(RowIndexString)("Replace_RX").ToLower = "true" Then
                ReplaceSearchType = "RX"
            Else
                ReplaceSearchType = "EX"
            End If

            If Proceed Then
                Try
                    FindString = TC.SubstitutePropertyFormula(Nothing, cf, FullName, FindString, ValidFilenameRequired:=False)
                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Unable to process formula in Find text '{0}' for property '{1}'", FindString, PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

                Try
                    ReplaceString = TC.SubstitutePropertyFormula(Nothing, cf, FullName, ReplaceString, ValidFilenameRequired:=False, ReplaceSearchType = "EX")
                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Unable to process formula in Replace text '{0}' for property '{1}'", ReplaceString, PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try
            End If

            If Proceed Then

                Try

                    '######## get the property here
                    If PropertySetName = "System" Then OLEProp = co.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyName.ToUpper)

                    If PropertySetName = "Custom" Then
                        OLEProp = co2.UserDefinedProperties.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName = PropertyName)
                        If IsNothing(OLEProp) Then

                            Dim userProperties = co2.UserDefinedProperties
                            Dim newPropertyId As UInteger = CType(userProperties.PropertyNames.Keys.Max() + 1, UInteger)
                            userProperties.PropertyNames(newPropertyId) = PropertyName
                            OLEProp = userProperties.NewProperty(VTPropertyType.VT_LPWSTR, newPropertyId)
                            OLEProp.Value = " "
                            userProperties.AddProperty(OLEProp)

                        End If
                    End If

                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Property '{0}.{1}' not found or not recognized.", PropertySetName, PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

            End If

            If Proceed Then

                Try

                    '####### set the property here
                    If FindSearchType = "PT" Then
                        OLEProp.Value = Replace(CType(OLEProp.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)
                    Else
                        If FindSearchType = "WC" Then
                            FindString = TC.GlobToRegex(FindString)
                        End If
                        If ReplaceSearchType = "PT" Then
                            ' ReplaceString = Regex.Escape(ReplaceString)
                        End If

                        OLEProp.Value = Regex.Replace(CType(OLEProp.Value, String), FindString, ReplaceString, RegexOptions.IgnoreCase)

                    End If

                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Unable to replace property value '{0}'.  This command only works on text type properties.", PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

            End If

            If Proceed Then

                Try

                    '############ delete the property here
                    If PropertySetName = "Custom" And ReplaceString = "%{DeleteProperty}" Then
                        co2.UserDefinedProperties.RemoveProperty(OLEProp.PropertyIdentifier)
                    End If

                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Unable to delete property value '{0}'.  This command only works on custom properties.", PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

            End If

            If Proceed Then
                Try

                    '############ save the properties here (!)
                    If PropertySetName = "System" Then
                        co.Save(dsiStream)
                    End If
                    If PropertySetName = "Custom" Then
                        co2.Save(dsiStream2)
                    End If

                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = "Problem accessing or saving Property."
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try
            End If

        Next

        '############ save the properties here (!)
        cf.Commit()
        cf.Close()

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function


    Public Overrides Function GetTLPTask(TLPParent As ExTableLayoutPanel) As ExTableLayoutPanel
        ControlsDict = New Dictionary(Of String, Control)

        Dim IU As New InterfaceUtilities

        Me.TLPTask = IU.BuildTLPTask(Me, TLPParent)

        Me.TLPOptions = BuildTLPOptions()

        For Each Control As Control In Me.TLPTask.Controls
            If ControlsDict.Keys.Contains(Control.Name) Then
                MsgBox(String.Format("ControlsDict already has Key '{0}'", Control.Name))
            End If
            ControlsDict(Control.Name) = Control
        Next

        Me.TLPTask.Controls.Add(TLPOptions, Me.TLPTask.ColumnCount - 2, 1)

        Return Me.TLPTask
    End Function

    Private Function BuildTLPOptions() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Button As Button
        Dim TextBox As TextBox

        Dim IU As New InterfaceUtilities

        IU.FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        Button = IU.FormatOptionsButton(ControlNames.Edit.ToString, "Edit")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = IU.FormatOptionsTextBox(ControlNames.JSONDict.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.AutoAddMissingProperty.ToString, "Add any property not already in the file")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.AutoUpdateMaterial.ToString, "For System.Material, also update density, face style, etc.")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.StructuredStorageEdit.ToString, "Direct edit properties without opening the file in Solid Edge")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Button = IU.FormatOptionsButton(ControlNames.Browse.ToString, "Matl Table")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button
        Button.Visible = False

        TextBox = IU.FormatOptionsTextBox(ControlNames.ActiveMaterialLibrary.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.RemoveFaceStyleOverrides.ToString, "Remove face style overrides")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = IU.FormatOptionsCheckBox(ControlNames.HideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        ControlsDict(CheckBox.Name) = CheckBox

        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)

        Return tmpTLPOptions
    End Function

    Private Sub InitializeOptionProperties()
        Dim CheckBox As CheckBox
        Dim TextBox As TextBox

        TextBox = CType(ControlsDict(ControlNames.JSONDict.ToString), TextBox)
        Me.JSONDict = TextBox.Text

        CheckBox = CType(ControlsDict(ControlNames.AutoAddMissingProperty.ToString), CheckBox)
        Me.AutoAddMissingProperty = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.AutoUpdateMaterial.ToString), CheckBox)
        Me.AutoUpdateMaterial = CheckBox.Checked

        CheckBox = CType(ControlsDict(ControlNames.StructuredStorageEdit.ToString), CheckBox)
        Me.StructuredStorageEdit = CheckBox.Checked

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

            If (Me.JSONDict = "") Or (Me.JSONDict = "{}") Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one property to edit", Indent))
            End If

            If Me.AutoUpdateMaterial Then
                If Not FileIO.FileSystem.FileExists(Me.ActiveMaterialLibrary) Then
                    If Not ErrorMessageList.Contains(Me.Description) Then
                        ErrorMessageList.Add(Me.Description)
                    End If
                    ExitStatus = 1
                    ErrorMessageList.Add(String.Format("{0}Select a valid material table", Indent))
                End If


            End If

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    Public Sub ButtonOptions_Click(sender As System.Object, e As System.EventArgs)
        Dim Button = CType(sender, Button)
        Dim Name = Button.Name
        'Dim Ctrl As Control
        Dim TextBox As TextBox

        Select Case Name
            Case ControlNames.Edit.ToString '"Edit"
                Dim PropertyInputEditor As New FormPropertyInputEditor

                PropertyInputEditor.JSONDict = Me.JSONDict

                ' Workaround
                Dim FileType = "asm"

                PropertyInputEditor.ShowInputEditor(FileType)

                If PropertyInputEditor.DialogResult = DialogResult.OK Then
                    'Me.JSONDict = PropertyInputEditor.JSONDict

                    TextBox = CType(ControlsDict(ControlNames.JSONDict.ToString), TextBox)
                    TextBox.Text = PropertyInputEditor.JSONDict

                End If

            Case ControlNames.Browse.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a material table file"
                tmpFileDialog.Filter = "Material Documents|*.mtl"

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.ActiveMaterialLibrary = tmpFileDialog.FileName
                    TextBox = CType(ControlsDict(ControlNames.ActiveMaterialLibrary.ToString), TextBox)
                    TextBox.Text = Me.ActiveMaterialLibrary
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select


    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name
        'Dim Ctrl As Control
        Dim Button As Button
        Dim TextBox As TextBox
        Dim CheckBox2 As CheckBox

        Select Case Name
            Case ControlNames.AutoAddMissingProperty.ToString '"AutoAddMissingProperty"
                Me.AutoAddMissingProperty = Checkbox.Checked

            Case ControlNames.AutoUpdateMaterial.ToString '"AutoUpdateMaterial"
                Me.AutoUpdateMaterial = Checkbox.Checked

                Button = CType(ControlsDict(ControlNames.Browse.ToString), Button)
                Button.Visible = Me.AutoUpdateMaterial

                TextBox = CType(ControlsDict(ControlNames.ActiveMaterialLibrary.ToString), TextBox)
                TextBox.Visible = Me.AutoUpdateMaterial

                CheckBox2 = CType(ControlsDict(ControlNames.RemoveFaceStyleOverrides.ToString), CheckBox)
                CheckBox2.Visible = Me.AutoUpdateMaterial

            Case ControlNames.RemoveFaceStyleOverrides.ToString
                Me.RemoveFaceStyleOverrides = Checkbox.Checked

            Case ControlNames.StructuredStorageEdit.ToString
                Me.StructuredStorageEdit = Checkbox.Checked
                Me.RequiresSave = Not Checkbox.Checked
                Me.SolidEdgeRequired = Not Checkbox.Checked

            Case ControlNames.HideOptions.ToString '"HideOptions"
                HandleHideOptionsChange(Me, Me.TLPTask, Me.TLPOptions, Checkbox)

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name
            Case ControlNames.JSONDict.ToString
                Me.JSONDict = TextBox.Text

            Case ControlNames.ActiveMaterialLibrary.ToString
                Me.ActiveMaterialLibrary = TextBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String

        HelpString = "Searches for text in a specified property and replaces it if found. "
        HelpString += "The property, search text, and replacement text are entered on the Input Editor. "
        HelpString += "Activate the editor using the `Property find/replace` `Edit` button on the **Task Tab** below the task list. "
        HelpString += ""
        HelpString += vbCrLf + vbCrLf + "![Find_Replace](My%20Project/media/property_input_editor.png)"
        HelpString += ""
        HelpString += vbCrLf + vbCrLf + "A `Property set`, either `System` or `Custom`, is required. "
        HelpString += "For more information, see the **Property Filter** section above. "
        HelpString += vbCrLf + vbCrLf + "There are three search modes, `PT`, `WC`, and `RX`. "
        HelpString += vbCrLf + vbCrLf + "- `PT` stands for 'Plain Text'.  It is simple to use, but finds literal matches only. "
        HelpString += vbCrLf + "- `WC` stands for 'Wild Card'.  You use `*`, `?`  `[charlist]`, and `[!charlist]` according to the VB Like syntax. "
        HelpString += vbCrLf + "- `RX` stands for 'Regex'.  It is a more comprehensive (and notoriously cryptic) method of matching text. "
        HelpString += "Check the [<ins>**.NET Regex Guide**</ins>](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference) "
        HelpString += "for more information."
        HelpString += vbCrLf + vbCrLf + "The search *is not* case sensitive, the replacement *is*. "
        HelpString += "For example, say the search is `aluminum`, "
        HelpString += "the replacement is `ALUMINUM`, "
        HelpString += "and the property value is `Aluminum 6061-T6`. "
        HelpString += "Then the new value would be `ALUMINUM 6061-T6`. "
        ' HelpString += vbCrLf + vbCrLf + "![Property Formula](My%20Project/media/property_formula.png)"
        HelpString += vbCrLf + vbCrLf + "In addition to plain text and pattern matching, you can also use "
        HelpString += "a property formula.  The formula has the same syntax as the Callout command, "
        HelpString += "except preceeded with `System.` or `Custom.` as shown in the Input Editor above. "
        HelpString += vbCrLf + vbCrLf + "If the specified property does not exist in the file, "
        HelpString += "you can optionally have it added automatically. "
        HelpString += "This option is set on the **Configuration Tab -- General Page**. "
        HelpString += "Note, this only works for `Custom` properties.  Adding `System` properties is not allowed. "
        HelpString += vbCrLf + vbCrLf + "If you are changing `System.Material` specifically, there is an option "
        HelpString += "to automatically update the part's material properties (density, face styles, etc.). "
        HelpString += "Set the option on the **Configuration Tab -- General Page**. "
        HelpString += vbCrLf + vbCrLf + "The properties are processed in the order in the table. "
        HelpString += "You can change the order by selecting a row and using the Up/Down buttons "
        HelpString += "at the top of the form.  Only one row can be moved at a time. "
        HelpString += "The delete button, also at the top of the form, removes selected rows. "
        HelpString += vbCrLf + vbCrLf + "You can copy the settings on the form to other tabs. "
        HelpString += "Set the `Copy To` CheckBoxes as desired."
        HelpString += vbCrLf + vbCrLf + "Note the textbox adjacent to the `Edit` button "
        HelpString += "is a `Dictionary` representation of the table settings in `JSON` format. "
        HelpString += "You can edit it if you want, but the form is probably easier to use. "
        HelpString += vbCrLf + vbCrLf + "EXPERIMENTAL: Direct edit uses the structured storage for fast execution. "

        Return HelpString
    End Function

End Class
