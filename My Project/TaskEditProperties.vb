Option Strict On

Imports Newtonsoft.Json
Imports OpenMcdf
Imports OpenMcdf.Extensions
Imports OpenMcdf.Extensions.OLEProperties
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Shapes

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

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.JSONDict = ""
        Me.AutoAddMissingProperty = False
        Me.AutoUpdateMaterial = False
        Me.ActiveMaterialLibrary = ""
        Me.RemoveFaceStyleOverrides = False
        Me.StructuredStorageEdit = False
        'Me.SolidEdgeRequired = False
        Me.SolidEdgeRequired = True

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

        Dim Proceed As Boolean = True
        Dim s As String

        Dim PropertiesToEditDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim PropertiesToEdit As String = ""
        Dim RowIndexString As String

        Dim TC As New Task_Common

        PropertiesToEdit = Me.JSONDict

        Dim DocType As String = TC.GetDocType(SEDoc)

        Dim tmpAsmDoc As SolidEdgeAssembly.AssemblyDocument = Nothing

        If Not PropertiesToEdit = "" Then

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
            '{"2":{
            ' ...
            '}

            PropertiesToEditDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(PropertiesToEdit)

        Else
            ExitStatus = 1
            ErrorMessageList.Add("No properties provided")
        End If

        If ExitStatus = 0 Then

            Dim IsFOA As Boolean = False
            If DocType = "asm" Then
                tmpAsmDoc = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)
                IsFOA = tmpAsmDoc.IsFileFamilyByDocument
            End If

            If (DocType = "asm") And (IsFOA) Then
                Dim Members As SolidEdgeAssembly.AssemblyFamilyMembers = tmpAsmDoc.AssemblyFamilyMembers
                If Not Members.GlobalEditMode Then
                    ExitStatus = 1
                    ErrorMessageList.Add("Cannot process FOA with 'Apply edits to all members' disabled")
                Else
                    For Each Member As SolidEdgeAssembly.AssemblyFamilyMember In Members
                        Members.ActivateMember(Member.MemberName)
                        SEApp.DoIdle()
                        SupplementalErrorMessage = DoFindReplace(SEApp, CType(tmpAsmDoc, SolidEdgeFramework.SolidEdgeDocument), PropertiesToEditDict)
                        AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)
                    Next
                End If
            Else
                SupplementalErrorMessage = DoFindReplace(SEApp, SEDoc, PropertiesToEditDict)
                AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)
            End If

        End If

        If ExitStatus = 0 Then
            If SEDoc.ReadOnly Then
                ExitStatus = 1
                ErrorMessageList.Add("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If

        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Private Overloads Function ProcessInternal(ByVal FullName As String) As Dictionary(Of Integer, List(Of String))

        ' Structured Storage
        ' https://github.com/ironfede/openmcdf

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

        Dim dsiStream As CFStream = Nothing
        Dim co As OLEPropertiesContainer = Nothing
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

            'Direct properties editing doesn't support linked files |Rx sintax: " & PropertyName
            If Proceed Then
                If FindString.StartsWith("[ERROR]") Then
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Direct edit doesn't support links in Find text '{0}' for property '{1}'", FindString.Replace("[ERROR]", ""), PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End If
                If ReplaceString.StartsWith("[ERROR]") Then
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Direct edit doesn't support links in Replace text '{0}' for property '{1}'", ReplaceString.Replace("[ERROR]", ""), PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End If
            End If

            If Proceed Then

                Try

                    '######## get the property here
                    If PropertySetName = "System" And (PropertyName <> "Category" And PropertyName <> "Manager" And PropertyName <> "Company" And PropertyName <> "Document Number" And PropertyName <> "Revision" And PropertyName <> "Project Name") Then
                        dsiStream = cf.RootStorage.GetStream("SummaryInformation")
                        co = dsiStream.AsOLEPropertiesContainer

                        OLEProp = co.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyName.ToUpper)
                    End If

                    If PropertySetName = "System" And (PropertyName = "Category" Or PropertyName = "Manager" Or PropertyName = "Company") Then
                        dsiStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
                        co = dsiStream.AsOLEPropertiesContainer

                        OLEProp = co.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyName.ToUpper)
                    End If

                    If PropertySetName = "System" And (PropertyName = "Document Number" Or PropertyName = "Revision" Or PropertyName = "Project Name") Then
                        dsiStream = cf.RootStorage.GetStream("Rfunnyd1AvtdbfkuIaamtae3Ie")
                        co = dsiStream.AsOLEPropertiesContainer

                        OLEProp = co.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName.ToLower Like "*" & PropertyName.ToLower & "*")
                    End If

                    If PropertySetName = "Custom" Then
                        dsiStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
                        co = dsiStream.AsOLEPropertiesContainer

                        OLEProp = co.UserDefinedProperties.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName = PropertyName)
                        If IsNothing(OLEProp) Then

                            Dim userProperties = co.UserDefinedProperties
                            Dim newPropertyId As UInteger = CType(userProperties.PropertyNames.Keys.Max() + 1, UInteger)
                            userProperties.PropertyNames(newPropertyId) = PropertyName
                            OLEProp = userProperties.NewProperty(VTPropertyType.VT_LPWSTR, newPropertyId)
                            OLEProp.Value = " "
                            userProperties.AddProperty(OLEProp)

                        End If

                    End If

                    'If PropertySetName = "Project" Then
                    '    dsiStream = cf.RootStorage.GetStream("Rfunnyd1AvtdbfkuIaamtae3Ie")
                    '    co = dsiStream.AsOLEPropertiesContainer

                    '    OLEProp = co.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName.ToLower Like "*" & PropertyName.ToLower & "*")
                    'End If

                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Property '{0}.{1}' not found or not recognized.", PropertySetName, PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

            End If

            If IsNothing(OLEProp) Then
                Proceed = False
                ExitStatus = 1
                s = String.Format("Property '{0}.{1}' not found or not recognized.", PropertySetName, PropertyName)
                If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
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

                    ' <<<<<<<<<<<<<<<<<<<<<<<<<<< @Francesco
                    ' <<<<<<<<<<<<<<<<<<<<<<<<<<< Pretty sure we don't want the check for "Custom"
                    ' <<<<<<<<<<<<<<<<<<<<<<<<<<< It doesn't match what you added for the EditInSolidEdge case
                    ' <<<<<<<<<<<<<<<<<<<<<<<<<<< Also, it will trigger a formula substitution which almost certainly won't find 'DeleteProperty'

                    '############ delete the property here
                    If PropertySetName = "Custom" And ReplaceString = "%{DeleteProperty}" Then
                        co.UserDefinedProperties.RemoveProperty(OLEProp.PropertyIdentifier)
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
                    If PropertySetName = "System" Or PropertySetName = "Custom" Or PropertySetName = "Project" Then
                        co.Save(dsiStream)
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
        If PropertySetName = "System" Or PropertySetName = "Custom" Or PropertySetName = "Project" Then
            cf.Commit()
        Else
            'ExitStatus = 1
            's = "Project properties are ReadOnly (Writeable in next release)"
            'If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
        End If
        cf.Close()

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage

    End Function


    Private Function DoFindReplace(
        SEApp As SolidEdgeFramework.Application,
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        PropertiesToEditDict As Dictionary(Of String, Dictionary(Of String, String))
        ) As Dictionary(Of Integer, List(Of String))

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

        Dim Proceed As Boolean = True
        Dim s As String

        Dim TC As New Task_Common

        For Each RowIndexString In PropertiesToEditDict.Keys

            Proceed = True

            ' ####################### Get names and find/replace strings #######################

            PropertyName = PropertiesToEditDict(RowIndexString)("PropertyName")
            PropertySetName = PropertiesToEditDict(RowIndexString)("PropertySet")
            FindString = PropertiesToEditDict(RowIndexString)("FindString")
            ReplaceString = PropertiesToEditDict(RowIndexString)("ReplaceString")

            ' ####################### Get search types #######################

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

            ' ####################### Do formula substitution #######################

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

            ' ####################### Get the property object from the file #######################

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

            ' ####################### Do the replacement. #######################

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

            ' ####################### Delete the property if needed. #######################

            If Proceed Then
                Try

                    If ReplaceString = "%{DeleteProperty}" Then
                        Prop.Delete()
                    End If

                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Unable to delete property '{0}'.  This command only works on custom properties.", PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

            End If

            ' ####################### Save the properties #######################

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

            ' ####################### For System.Material, update density, face style etc if needed #######################

            If Proceed Then
                PropertyName = PropertiesToEditDict(RowIndexString)("PropertyName")
                PropertySetName = PropertiesToEditDict(RowIndexString)("PropertySet")

                If (PropertyName.ToLower = "material") And (PropertySetName.ToLower = "system") Then

                    If Me.AutoUpdateMaterial Then

                        Select Case TC.GetDocType(SEDoc)
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


        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Button As Button
        Dim TextBox As TextBox

        'Dim IU As New InterfaceUtilities

        FormatTLPOptions(tmpTLPOptions, "TLPOptions", 4)

        RowIndex = 0

        Button = FormatOptionsButton(ControlNames.Edit.ToString, "Edit")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button

        TextBox = FormatOptionsTextBox(ControlNames.JSONDict.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoAddMissingProperty.ToString, "Add any property not already in the file")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.StructuredStorageEdit.ToString, "Edit properties outside of Solid Edge")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoUpdateMaterial.ToString, "For Material, update density, face style, etc.")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.Browse.ToString, "Matl Table")
        AddHandler Button.Click, AddressOf ButtonOptions_Click
        tmpTLPOptions.Controls.Add(Button, 0, RowIndex)
        ControlsDict(Button.Name) = Button
        Button.Visible = False

        TextBox = FormatOptionsTextBox(ControlNames.ActiveMaterialLibrary.ToString, "")
        TextBox.BackColor = Color.FromArgb(255, 240, 240, 240)
        AddHandler TextBox.TextChanged, AddressOf TextBoxOptions_Text_Changed
        tmpTLPOptions.Controls.Add(TextBox, 1, RowIndex)
        ControlsDict(TextBox.Name) = TextBox
        TextBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.RemoveFaceStyleOverrides.ToString, "Remove face style overrides")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.HideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        ControlsDict(CheckBox.Name) = CheckBox

        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)

        Return tmpTLPOptions
    End Function

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

                If Not Me.SolidEdgeRequired Then
                    If Not ErrorMessageList.Contains(Me.Description) Then
                        ErrorMessageList.Add(Me.Description)
                    End If
                    ExitStatus = 1
                    Dim s = String.Format("{0}'Material update' incompatible with 'Edit outside SE'.", Indent)
                    ErrorMessageList.Add(s)

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
        Dim TextBox As TextBox

        Select Case Name
            Case ControlNames.Edit.ToString '"Edit"
                Dim PropertyInputEditor As New FormPropertyInputEditor

                PropertyInputEditor.JSONDict = Me.JSONDict

                ' Workaround
                Dim FileType = "asm"

                PropertyInputEditor.ShowDialog()

                If PropertyInputEditor.DialogResult = DialogResult.OK Then
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
                HandleHideOptionsChange(Me, Me.TaskOptionsTLP, Checkbox)

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
        HelpString += "To activate the editor click the `Edit` button in the options panel. "

        HelpString += vbCrLf + vbCrLf + "![Find_Replace](My%20Project/media/property_input_editor.png)"

        HelpString += vbCrLf + vbCrLf + "A `Property set`, either `System` or `Custom`, is required. "
        HelpString += "For more information, see the **Property Filter** section in this README file. "

        HelpString += vbCrLf + vbCrLf + "There are three search modes, `PT`, `WC`, `RX`, and `EX`. "
        HelpString += vbCrLf + vbCrLf + "- `PT` stands for 'Plain Text'.  It is simple to use, but finds literal matches only. "
        HelpString += vbCrLf + "- `WC` stands for 'Wild Card'.  You use `*`, `?`  `[charlist]`, and `[!charlist]` according to the VB `Like` syntax. "
        HelpString += vbCrLf + "- `RX` stands for 'Regex'.  It is a more comprehensive (and notoriously cryptic) method of matching text. "
        HelpString += "Check the [<ins>**.NET Regex Guide**</ins>](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference) "
        HelpString += "for more information."
        HelpString += vbCrLf + "- `EX` stands for 'Expression'.  It is discussed below. "

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
        HelpString += "you can optionally add it by enabling the `Add any property not already in file` option. "
        HelpString += "Note, this only works for `Custom` properties.  Adding `System` properties is not allowed. "

        HelpString += vbCrLf + vbCrLf + "On the other hand, if you want to delete a property, "
        HelpString += "set the Replace type to `PT` and "
        HelpString += "enter the special code `%{DeleteProperty}` as the Replace string. "
        HelpString += "As above, this only works for `Custom` properties. "

        HelpString += vbCrLf + vbCrLf + "If you are changing `System.Material` specifically, you can "
        HelpString += "also update the properties associated with the material itself. "
        HelpString += "Select the option `For material, update density, face styles, etc.`. "

        HelpString += vbCrLf + vbCrLf + "The properties are processed in the order in the table. "
        HelpString += "You can change the order by selecting a row and using the Up/Down buttons "
        HelpString += "at the top of the form.  Only one row can be moved at a time. "
        HelpString += "The delete button, also at the top of the form, removes selected rows. "

        HelpString += vbCrLf + vbCrLf + "Note the textbox adjacent to the `Edit` button "
        HelpString += "is a `Dictionary` representation of the table settings in `JSON` format. "
        HelpString += "You can edit it if you want, but the form is probably easier to use. "

        HelpString += vbCrLf + vbCrLf + "EXPERIMENTAL: Direct edit using Windows Structured Storage for fast execution. "
        HelpString += "If you want to try that out, select the option `Edit properties outside Solid Edge`. "
        HelpString += vbCrLf + vbCrLf + "Due to some upstream limitations, certain properties in Structured Storage are read-only for now. "
        HelpString += "That means you can use them in formulas in the `Find` and `Replace` strings, but cannot change the properties themselves. "
        HelpString += "The affected properties are `System.Document Number`, `System.Revision`, `System.Project Name`. "
        HelpString += vbCrLf + vbCrLf + "There are other items that Solid Edge presents as properties, but are not kept in Structured Storage. "
        HelpString += "As such, they are not accesible using this technique. "
        HelpString += "There are quite a few of these, for example density, fill style, etc. "
        HelpString += "The only two in this category that are currently supported by Housekeeper (but not Structured Storage) "
        HelpString += "are `System.Material` and `System.Sheet Metal Gage`. "

        HelpString += vbCrLf + vbCrLf + "**Expressions**"

        HelpString += vbCrLf + vbCrLf + "![Expression Editor](My%20Project/media/expression_editor.png)"

        HelpString += vbCrLf + vbCrLf + "With this tool you create an `expression` to use in a `Find` and/or `Replace` string. "
        HelpString += "It functions similar to a formula in Excel. "
        HelpString += "You can do string manipulations, like changing capitalization or rearranging text. "
        HelpString += "You can create logical expressions, do arithmetic, and, well, almost anything.  The avaialable functions are listed below. "

        HelpString += vbCrLf + vbCrLf + "Like Excel, the expression must return a value.  Nested expressions are the norm for complex manipulations. "
        HelpString += "Unlike Excel, multi-line text is allowed, which can make the code more readable. "

        HelpString += vbCrLf + vbCrLf + "You can check your expression using the `Test` button. "
        HelpString += "If there are variables not defined in the formula itself, for example `%{Custom.Engineer}`, it prompts you for a value. "
        HelpString += "You can `Save` or `Save As` your expression with the buttons provided. "
        HelpString += "Retreive them with the `Save Expressions` button.  There are a couple of example saved expressions you can review there. "
        HelpString += "The `Help` button opens a web site where you can learn more. "

        HelpString += vbCrLf + vbCrLf + "Available functions"
        HelpString += vbCrLf + vbCrLf + "`concat()`, `contains()`, `convert()`, `count()`, `countBy()`, `dateAdd()`, "
        HelpString += "`dateTime()`, `dateTimeAsEpoch()`, `dateTimeAsEpochMs()`, `dictionary()`,"
        HelpString += "`distinct()`, `endsWith()`, `extend()`, `first()`, `firstOrDefault()`, "
        HelpString += "`format()`, `getProperties()`, `getProperty()`, `humanize()`, `if()`, `in()`, "
        HelpString += "`indexOf()`, `isGuid()`, `isInfinite()`, `isNaN()`, `isNull()`, `isNullOrEmpty()`, "
        HelpString += "`isNullOrWhiteSpace()`, `isSet()`, `itemAtIndex()`, `jObject()`, "
        HelpString += "`join()`, `jPath()`, `last()`, `lastIndexOf()`, `lastOrDefault()`, `length()`, "
        HelpString += "`list()`, `listOf()`, `max()`, `maxValue()`, `min()`, `minValue()`, "
        HelpString += "`nullCoalesce()`, `orderBy()`, `padLeft()`, `parse()`, `parseInt()`, `regexGroup()`, "
        HelpString += "`regexIsMatch()`, `replace()`, `retrieve`, `reverse()`, `sanitize()`, "
        HelpString += "`select()`, `selectDistinct()`, `setProperties()`, `skip()`, `Sort()`, `Split()`, "
        HelpString += "`startsWith()`, `store()`, `substring()`, `sum()`, `switch()`, `take()`, "
        HelpString += "`throw()`, `timeSpan()`, `toDateTime()`, `toLower()`, `toString()`, `toUpper()`, "
        HelpString += "`try()`, `tryParse()`, `typeOf()`, `where()`"
        HelpString += ""
        HelpString += ""
        HelpString += ""
        HelpString += ""
        HelpString += ""


        Return HelpString
    End Function

End Class
