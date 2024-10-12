Option Strict On

Imports Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties
Imports Newtonsoft.Json
Imports OpenMcdf
Imports OpenMcdf.Extensions
Imports OpenMcdf.Extensions.OLEProperties
Imports System.IO
Imports System.Text.RegularExpressions

Public Class TaskEditProperties
    Inherits Task

    Private _MaterialTable As String
    Public Property MaterialTable As String
        Get
            Return _MaterialTable
        End Get
        Set(value As String)
            _MaterialTable = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox).Text = value
            End If
        End Set
    End Property


    Private _JSONString As String
    Public Property JSONString As String
        Get
            Return _JSONString
        End Get
        Set(value As String)
            _JSONString = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.JSONString.ToString), TextBox).Text = value
            End If
        End Set
    End Property

    Private _AutoAddMissingProperty As Boolean
    Public Property AutoAddMissingProperty As Boolean
        Get
            Return _AutoAddMissingProperty
        End Get
        Set(value As Boolean)
            _AutoAddMissingProperty = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AutoAddMissingProperty.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _AutoUpdateMaterial As Boolean
    Public Property AutoUpdateMaterial As Boolean
        Get
            Return _AutoUpdateMaterial
        End Get
        Set(value As Boolean)
            _AutoUpdateMaterial = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.AutoUpdateMaterial.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _UseConfigurationPageTemplates As Boolean
    Public Property UseConfigurationPageTemplates As Boolean
        Get
            Return _UseConfigurationPageTemplates
        End Get
        Set(value As Boolean)
            _UseConfigurationPageTemplates = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.UseConfigurationPageTemplates.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _RemoveFaceStyleOverrides As Boolean
    Public Property RemoveFaceStyleOverrides As Boolean
        Get
            Return _RemoveFaceStyleOverrides
        End Get
        Set(value As Boolean)
            _RemoveFaceStyleOverrides = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.RemoveFaceStyleOverrides.ToString), CheckBox).Checked = value
            End If
        End Set
    End Property

    Private _StructuredStorageEdit As Boolean
    Public Property StructuredStorageEdit As Boolean
        Get
            Return _StructuredStorageEdit
        End Get
        Set(value As Boolean)
            _StructuredStorageEdit = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                CType(ControlsDict(ControlNames.StructuredStorageEdit.ToString), CheckBox).Checked = value
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

    Private _TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
    Public Property TemplatePropertyDict As Dictionary(Of String, Dictionary(Of String, String))
        Get
            Return _TemplatePropertyDict
        End Get
        Set(value As Dictionary(Of String, Dictionary(Of String, String)))
            _TemplatePropertyDict = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                Dim s = JsonConvert.SerializeObject(Me.TemplatePropertyDict)
                If Not Me.TemplatePropertyDictJSON = s Then
                    Me.TemplatePropertyDictJSON = s
                End If
            End If
        End Set
    End Property


    Private _TemplatePropertyDictJSON As String
    Public Property TemplatePropertyDictJSON As String
        Get
            Return _TemplatePropertyDictJSON
        End Get
        Set(value As String)
            _TemplatePropertyDictJSON = value
            If Me.TaskOptionsTLP IsNot Nothing Then
                If Not _TemplatePropertyDictJSON = JsonConvert.SerializeObject(Me.TemplatePropertyDict) Then
                    Me.TemplatePropertyDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(_TemplatePropertyDictJSON)
                End If
            End If

        End Set
    End Property

    Enum ControlNames
        Edit
        JSONString
        AutoAddMissingProperty
        AutoUpdateMaterial
        UseConfigurationPageTemplates
        Browse
        MaterialTable
        RemoveFaceStyleOverrides
        StructuredStorageEdit
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
        Me.AppliesToDraft = True
        Me.HasOptions = True
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskEditPropertiesEx
        Me.Category = "Edit"
        Me.RequiresMaterialTable = True
        Me.RequiresTemplatePropertyDict = True
        SetColorFromCategory(Me)

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.MaterialTable = ""
        Me.JSONString = ""
        Me.AutoAddMissingProperty = False
        Me.AutoUpdateMaterial = False
        Me.RemoveFaceStyleOverrides = False
        Me.StructuredStorageEdit = False
        'Me.SolidEdgeRequired = False
        Me.SolidEdgeRequired = True  ' Default is so checking the box toggles a property update

        Me.TemplatePropertyDict = New Dictionary(Of String, Dictionary(Of String, String))

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

        Dim PropertiesToEditDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim PropertiesToEdit As String = ""

        Dim UC As New UtilsCommon

        PropertiesToEdit = Me.JSONString

        Dim DocType As String = UC.GetDocType(SEDoc)

        Dim tmpAsmDoc As SolidEdgeAssembly.AssemblyDocument = Nothing

        If Not PropertiesToEdit = "" Then

            '{"0":
            '    {"PropertySet":"Custom",
            '     "PropertyName":"hmk_Part_Number",
            '     "FindSearch":"PT",
            '     "FindString":"a",
            '     "ReplaceSearch":"PT",
            '     "ReplaceString":"b"},
            ' "1":
            '...
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

        Dim UC As New UtilsCommon

        PropertiesToEdit = Me.JSONString

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

            ' ####################### Get parameters #######################

            PropertyName = PropertiesToEditDict(RowIndexString)("PropertyName")
            PropertySetName = PropertiesToEditDict(RowIndexString)("PropertySet")
            FindSearchType = PropertiesToEditDict(RowIndexString)("FindSearch")
            FindString = PropertiesToEditDict(RowIndexString)("FindString")
            ReplaceSearchType = PropertiesToEditDict(RowIndexString)("ReplaceSearch")
            ReplaceString = PropertiesToEditDict(RowIndexString)("ReplaceString")

            If Not TemplatePropertyDict.Keys.Contains(PropertyName) Then
                Proceed = False
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("Property '{0}' not found in template dictionary", PropertyName))
            End If


            ' ####################### Do formula substitution #######################

            If Proceed Then
                Try
                    FindString = UC.SubstitutePropertyFormula(Nothing, cf, FullName, FindString, ValidFilenameRequired:=False,
                                                              TemplatePropertyDict)
                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Unable to process formula in Find text '{0}' for property '{1}'", FindString, PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

                Try
                    ReplaceString = UC.SubstitutePropertyFormula(Nothing, cf, FullName, ReplaceString, ValidFilenameRequired:=False,
                                                                 TemplatePropertyDict, ReplaceSearchType = "EX")
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


            ' ####################### Get the property object #######################

            Dim PropertyNameEnglish = TemplatePropertyDict(PropertyName)("EnglishName")

            If Proceed Then

                Dim SIList As New List(Of String)
                SIList.AddRange({"Title", "Subject", "Author", "Keywords", "Comments"})

                Dim DSIList As New List(Of String)
                DSIList.AddRange({"Category", "Company", "Manager"})

                Dim FunnyList As New List(Of String)
                FunnyList.AddRange({"Document Number", "Revision", "Project Name"})

                Dim tfSystem As Boolean = (PropertySetName.ToLower = "system") Or (PropertySetName = "")
                Dim tfCustom As Boolean = (PropertySetName.ToLower = "custom") Or (PropertySetName = "")

                Try

                    '######## get the property here

                    If (SIList.Contains(PropertyNameEnglish)) And (tfSystem) Then
                        dsiStream = cf.RootStorage.GetStream("SummaryInformation")
                        co = dsiStream.AsOLEPropertiesContainer

                        OLEProp = co.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyNameEnglish.ToUpper)

                    ElseIf (DSIList.Contains(PropertyNameEnglish)) And (tfSystem) Then
                        dsiStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
                        co = dsiStream.AsOLEPropertiesContainer

                        OLEProp = co.Properties.First(Function(Proper) Proper.PropertyName = "PIDSI_" & PropertyNameEnglish.ToUpper)

                    ElseIf (FunnyList.Contains(PropertyNameEnglish)) And (tfSystem) Then
                        dsiStream = cf.RootStorage.GetStream("Rfunnyd1AvtdbfkuIaamtae3Ie")
                        co = dsiStream.AsOLEPropertiesContainer

                        OLEProp = co.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName.ToLower Like "*" & PropertyNameEnglish.ToLower & "*")

                    Else  ' Hopefully a Custom Property
                        If tfCustom Then
                            dsiStream = cf.RootStorage.GetStream("DocumentSummaryInformation")
                            co = dsiStream.AsOLEPropertiesContainer

                            OLEProp = co.UserDefinedProperties.Properties.FirstOrDefault(Function(Proper) Proper.PropertyName = PropertyNameEnglish)
                            If (IsNothing(OLEProp)) And (Me.AutoAddMissingProperty) Then ' Add it

                                Try
                                    Dim userProperties = co.UserDefinedProperties
                                    Dim newPropertyId As UInteger = 2 'For some reason when custom property is empty there is an hidden property therefore the starting index must be 2

                                    If userProperties.PropertyNames.Keys.Count > 0 Then newPropertyId = CType(userProperties.PropertyNames.Keys.Max() + 1, UInteger)
                                    'This is the ID the new property will have
                                    'Duplicated IDs are not allowed
                                    'We need a method to calculate an unique ID; .Max() seems a good one cause .Max() + 1 should be unique
                                    'Alternatively we need a method that find unused IDs inbetwen existing one; this will find unused IDs from previous property deletion

                                    userProperties.PropertyNames(newPropertyId) = PropertyNameEnglish
                                    OLEProp = userProperties.NewProperty(VTPropertyType.VT_LPWSTR, newPropertyId)
                                    OLEProp.Value = " "
                                    userProperties.AddProperty(OLEProp)
                                    Dim i = 0
                                Catch ex As Exception
                                    Proceed = False
                                    ExitStatus = 1
                                    If PropertyName = PropertyNameEnglish Then
                                        s = String.Format("Unable to add property '{0}.{1}'.", PropertySetName, PropertyName)
                                    Else
                                        s = String.Format("Unable to add property '{0}.{1}({2})'.", PropertySetName, PropertyName, PropertyNameEnglish)
                                    End If
                                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                                End Try

                            End If

                        End If

                    End If

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
                If PropertyName = PropertyNameEnglish Then
                    s = String.Format("Property '{0}' not found or not recognized.", PropertyName)
                Else
                    s = String.Format("Property '{0}({1})' not found or not recognized.", PropertyName, PropertyNameEnglish)
                End If
                If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
            End If


            ' ####################### Delete or do the replacement #######################

            If Proceed Then

                If FindSearchType = "X" Then
                    Try
                        '############ delete the property here
                        co.UserDefinedProperties.RemoveProperty(OLEProp.PropertyIdentifier)

                    Catch ex As Exception
                        Proceed = False
                        ExitStatus = 1
                        If PropertyName = PropertyNameEnglish Then
                            s = String.Format("Unable to delete property '{0}'.  This command only works on custom properties.", PropertyName)
                        Else
                            s = String.Format("Unable to delete property '{0}({1})'.  This command only works on custom properties.", PropertyName, PropertyNameEnglish)
                        End If
                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                    End Try

                Else
                    Try

                        '####### set the property here
                        If FindSearchType = "PT" Then
                            OLEProp.Value = Replace(CType(OLEProp.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)
                        Else
                            If FindSearchType = "WC" Then
                                FindString = UC.GlobToRegex(FindString)
                            End If
                            If ReplaceSearchType = "PT" Then
                                ' ReplaceString = Regex.Escape(ReplaceString)
                            End If

                            OLEProp.Value = Regex.Replace(CType(OLEProp.Value, String), FindString, ReplaceString, RegexOptions.IgnoreCase)

                        End If

                    Catch ex As Exception
                        Proceed = False
                        ExitStatus = 1
                        If PropertyName = PropertyNameEnglish Then
                            s = String.Format("Unable to replace property value '{0}'.  This command only works on text type properties.", PropertyName)
                        Else
                            s = String.Format("Unable to replace property value '{0}({1})'.  This command only works on text type properties.", PropertyName, PropertyNameEnglish)
                        End If
                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                    End Try


                End If


            End If


            ' ####################### Save the properties #######################

            If Proceed Then
                Try

                    '############ save the properties here (!)
                    co.Save(dsiStream)
                    'If PropertySetName = "System" Or PropertySetName = "Custom" Or PropertySetName = "Project" Then
                    'End If

                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = "Problem accessing or saving Property."
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try
            End If

        Next


        ' ###### In case of error, don't save anything. ######
        If ExitStatus = 0 Then
            '############ save the properties here (!)
            cf.Commit()
        Else
            s = "Errors encountered.  No changes made."
            If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
        End If

        If cf IsNot Nothing Then
            cf.Close()
        End If

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

        Dim UC As New UtilsCommon

        For Each RowIndexString In PropertiesToEditDict.Keys

            Proceed = True

            ' ####################### Get parameters #######################

            PropertyName = PropertiesToEditDict(RowIndexString)("PropertyName")
            PropertySetName = PropertiesToEditDict(RowIndexString)("PropertySet")
            FindSearchType = PropertiesToEditDict(RowIndexString)("FindSearch")
            FindString = PropertiesToEditDict(RowIndexString)("FindString")
            ReplaceSearchType = PropertiesToEditDict(RowIndexString)("ReplaceSearch")
            ReplaceString = PropertiesToEditDict(RowIndexString)("ReplaceString")


            ' ####################### Do formula substitution #######################

            If Proceed Then
                Try
                    FindString = UC.SubstitutePropertyFormula(SEDoc, Nothing, SEDoc.FullName, FindString, ValidFilenameRequired:=False,
                                                              TemplatePropertyDict)
                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    s = String.Format("Unable to process formula in Find text '{0}' for property '{1}'", FindString, PropertyName)
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

                Try
                    ReplaceString = UC.SubstitutePropertyFormula(SEDoc, Nothing, SEDoc.FullName, ReplaceString, ValidFilenameRequired:=False,
                                                                 TemplatePropertyDict, ReplaceSearchType = "EX")
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
                    Prop = UC.GetProp(SEDoc, PropertySetName, PropertyName, 0, AutoAddMissingProperty)
                    If Prop Is Nothing Then
                        Proceed = False
                        ExitStatus = 1
                        If Not PropertySetName = "" Then
                            s = String.Format("Property '{0}.{1}' not found or not recognized.", PropertySetName, PropertyName)
                        Else
                            s = String.Format("Property '{0}' not found or not recognized.", PropertyName)
                        End If
                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                    End If

                Catch ex As Exception
                    Proceed = False
                    ExitStatus = 1
                    If Not PropertySetName = "" Then
                        s = String.Format("Property '{0}.{1}' not found or not recognized.", PropertySetName, PropertyName)
                    Else
                        s = String.Format("Property '{0}' not found or not recognized.", PropertyName)
                    End If
                    If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                End Try

            End If

            ' ####################### Delete or do the replacement. #######################

            If Proceed Then

                If FindSearchType = "X" Then
                    Try

                        If FindSearchType = "X" Then
                            Prop.Delete()
                        End If

                    Catch ex As Exception
                        Proceed = False
                        ExitStatus = 1
                        s = String.Format("Unable to delete property '{0}'.  This command only works on custom properties.", PropertyName)
                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                    End Try

                Else

                    Try
                        If ReplaceSearchType = "EX" Then

                        End If

                        If FindSearchType = "PT" Then
                            Prop.Value = Replace(CType(Prop.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)
                        Else
                            If FindSearchType = "WC" Then
                                FindString = UC.GlobToRegex(FindString)
                            End If
                            If ReplaceSearchType = "PT" Then
                                ' ReplaceString = Regex.Escape(ReplaceString)
                            End If

                            Prop.Value = Regex.Replace(CType(Prop.Value, String), FindString, ReplaceString, RegexOptions.IgnoreCase)

                        End If

                    Catch ex As Exception
                        Proceed = False
                        ExitStatus = 1
                        s = String.Format("Unable to replace property value '{0}'.  This command only works on text type properties.", PropertyName)
                        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
                    End Try

                End If

            End If

            '' ####################### Delete the property if needed. #######################

            'If Proceed Then
            '    Try

            '        If FindSearchType = "X" Then
            '            Prop.Delete()
            '        End If

            '    Catch ex As Exception
            '        Proceed = False
            '        ExitStatus = 1
            '        s = String.Format("Unable to delete property '{0}'.  This command only works on custom properties.", PropertyName)
            '        If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
            '    End Try

            'End If

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

                Dim tf As Boolean = PropertySetName.ToLower = "system"
                tf = tf Or (PropertySetName.ToLower = "system")
                tf = tf And (PropertyName.ToLower = "material")
                tf = tf And (Me.AutoUpdateMaterial)

                If tf Then
                    Select Case UC.GetDocType(SEDoc)
                        Case "par", "psm"
                            Dim UM As New UtilsMaterials
                            SupplementalErrorMessage = UM.UpdateMaterialFromMaterialTable(
                                        SEDoc, Me.MaterialTable, Me.RemoveFaceStyleOverrides, SEApp)

                            AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)

                        Case Else
                            ' Not an error
                    End Select
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

        TextBox = FormatOptionsTextBox(ControlNames.JSONString.ToString, "")
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

        CheckBox = FormatOptionsCheckBox(ControlNames.UseConfigurationPageTemplates.ToString, "Use configuration page material table")
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

        TextBox = FormatOptionsTextBox(ControlNames.MaterialTable.ToString, "")
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

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
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

            If (Me.JSONString = "") Or (Me.JSONString = "{}") Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                ErrorMessageList.Add(String.Format("{0}Select at least one property to edit", Indent))
            End If

            If Me.AutoUpdateMaterial Then
                If Not FileIO.FileSystem.FileExists(Me.MaterialTable) Then
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

            If (Not Me.SolidEdgeRequired) And (Me.TemplatePropertyDict.Count = 0) Then
                If Not ErrorMessageList.Contains(Me.Description) Then
                    ErrorMessageList.Add(Me.Description)
                End If
                ExitStatus = 1
                Dim s = String.Format("{0}Template properties required for 'Edit outside SE'.  Update them on the Configuration Tab -- Templates Page", Indent)
                ErrorMessageList.Add(s)
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
                Dim FPIE As New FormPropertyInputEditor

                FPIE.JSONString = Me.JSONString
                FPIE.HelpURL = Me.HelpURL

                FPIE.ShowDialog()

                If FPIE.DialogResult = DialogResult.OK Then
                    ' Me.JSONDict is updated when the TextBox changes.
                    TextBox = CType(ControlsDict(ControlNames.JSONString.ToString), TextBox)
                    TextBox.Text = FPIE.JSONString

                    Me.TemplatePropertyDict = FPIE.TemplatePropertyDict

                End If

            Case ControlNames.Browse.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a material table file"
                tmpFileDialog.Filter = "Material Documents|*.mtl"

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.MaterialTable = tmpFileDialog.FileName
                    TextBox = CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox)
                    TextBox.Text = Me.MaterialTable
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

                CheckBox2 = CType(ControlsDict(ControlNames.UseConfigurationPageTemplates.ToString), CheckBox)
                CheckBox2.Visible = Me.AutoUpdateMaterial

                Button = CType(ControlsDict(ControlNames.Browse.ToString), Button)
                Button.Visible = Me.AutoUpdateMaterial And Not CheckBox2.Checked

                TextBox = CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox)
                TextBox.Visible = Me.AutoUpdateMaterial And Not CheckBox2.Checked

                CheckBox2 = CType(ControlsDict(ControlNames.RemoveFaceStyleOverrides.ToString), CheckBox)
                CheckBox2.Visible = Me.AutoUpdateMaterial

            Case ControlNames.UseConfigurationPageTemplates.ToString
                Me.UseConfigurationPageTemplates = Checkbox.Checked

                If Me.UseConfigurationPageTemplates Then
                    Me.MaterialTable = Form_Main.MaterialTable
                    CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = False
                    CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox).Visible = False

                Else
                    CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = True
                    CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox).Visible = True

                End If

            Case ControlNames.RemoveFaceStyleOverrides.ToString
                Me.RemoveFaceStyleOverrides = Checkbox.Checked

            Case ControlNames.StructuredStorageEdit.ToString
                Me.StructuredStorageEdit = Checkbox.Checked
                Me.RequiresSave = Not Checkbox.Checked
                Me.SolidEdgeRequired = Not Checkbox.Checked

            Case ControlNames.AutoHideOptions.ToString '"HideOptions"
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub

    Public Sub TextBoxOptions_Text_Changed(sender As System.Object, e As System.EventArgs)
        Dim TextBox = CType(sender, TextBox)
        Dim Name = TextBox.Name

        Select Case Name
            Case ControlNames.JSONString.ToString
                Me.JSONString = TextBox.Text

            Case ControlNames.MaterialTable.ToString
                Me.MaterialTable = TextBox.Text

            Case Else
                MsgBox(String.Format("{0} Name '{1}' not recognized", Me.Name, Name))
        End Select

    End Sub


    'Public Overrides Sub ReconcileFormWithProps()
    '    ControlsDict(ControlNames.MaterialTable.ToString).Text = Me.MaterialTable
    'End Sub


    Private Function GetHelpText() As String
        Dim HelpString As String

        HelpString = "Searches for text in a specified property and replaces it if found. "

        HelpString += vbCrLf + vbCrLf + "![EditProperties](My%20Project/media/task_edit_properties.png)"

        HelpString += vbCrLf + vbCrLf + "The property, search text, and replacement text are entered on the Input Editor. "
        HelpString += "To activate the editor click the `Edit` button in the options panel. "

        HelpString += vbCrLf + vbCrLf + "![Find_Replace](My%20Project/media/property_input_editor.png)"

        HelpString += vbCrLf + vbCrLf + "This is a powerful tool with a lot of options.  These are detailed below. "

        HelpString += vbCrLf + vbCrLf + "**Using the Input Editor**"

        HelpString += vbCrLf + vbCrLf + "Before using this command, you must pre-populate property choices from your templates. "
        HelpString += "To do so, on the "
        HelpString += "[<ins>**Configuration Tab -- Templates Page**</ins>](#templates-page), "
        HelpString += "select your templates and click the `Update` button. "
        HelpString += "There are a lot of properties.  After the update is complete, the `Customize` dialog appears. "
        HelpString += "Choose which to make available there. "
        HelpString += "If you need a property that is not in your templates, right-click the Favorites pane and click `Add property manually`. "
        HelpString += "To access properties not in your Favorites, enable the `Show All Props` option on the toolbar. "

        HelpString += vbCrLf + vbCrLf + "A `Property set`, either `System` or `Custom`, is required. "
        HelpString += "The program will normally set the correct choice automatically. "
        HelpString += "One exception is if you have a custom property with the same name as a system property. "
        HelpString += "In that case, you have to select the appropriate one yourself. "
        HelpString += "For more information about `Property sets`, see the "
        HelpString += "[<ins>**Property Filter**</ins>](#1-property-filter) "
        HelpString += "section in this README file. "

        HelpString += vbCrLf + vbCrLf + "There are five search modes, `PT`, `WC`, `RX`, `EX`, and `X`. "
        HelpString += vbCrLf + vbCrLf + "- `PT` stands for 'Plain Text'.  It is simple to use, but finds literal matches only. "
        HelpString += vbCrLf + "- `WC` stands for 'Wild Card'.  You use `*`, `?`  `[charlist]`, and `[!charlist]` according to the VB `Like` syntax. "
        HelpString += vbCrLf + "- `RX` stands for 'Regex'.  It is a more comprehensive (and notoriously cryptic) method of matching text. "
        HelpString += "Check the [<ins>**.NET Regex Guide**</ins>](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference) "
        HelpString += "for more information."
        HelpString += vbCrLf + "- `EX` stands for 'Expression'.  It is discussed below. "
        HelpString += vbCrLf + "- `X` isn't really a search mode.  It means delete the chosen property. "

        HelpString += vbCrLf + vbCrLf + "The properties are processed in the order in the table. "
        HelpString += "To change the order, select a row and, on the toolbar `Row Tools` group, click the `Up` or `Down` arrow. "
        HelpString += "The `Delete` button removes the selected row. "

        HelpString += vbCrLf + vbCrLf + "You can save a setup for future use. "
        HelpString += "In the toolbar `Saved Settings` group, enter the name and click `Save`. "
        HelpString += "To retrieve a setting, click the down arrow and select it. "
        HelpString += "To remove a setting, select it and click `Delete`. "

        HelpString += vbCrLf + vbCrLf + "**Case Sensitivity**"

        HelpString += vbCrLf + vbCrLf + "The search *is not* case sensitive, the replacement *is*. "
        HelpString += "For example, say the search is `aluminum`, "
        HelpString += "the replacement is `ALUMINUM`, "
        HelpString += "and the property value in a file is `Aluminum 6061-T6`. "
        HelpString += "Then the new value would be `ALUMINUM 6061-T6`. "

        HelpString += vbCrLf + vbCrLf + "**Property Substitution**"

        HelpString += vbCrLf + vbCrLf + "In addition to plain text and pattern matching, you can also "
        HelpString += "do property substitution.   The example in the Input Editor above "
        HelpString += "is telling the program to use the file name for the Document Number. "
        HelpString += "To select a property, right-click the `Find` or `Replace` field and select `Insert property`. "
        HelpString += "You can also type it in if preferred.  The formula has the same syntax as the Callout command, "
        HelpString += "except preceeded with `System.` or `Custom.` as shown in the example. "

        HelpString += vbCrLf + vbCrLf + "**Options**"

        HelpString += vbCrLf + vbCrLf + "If the specified property does not exist in the file, "
        HelpString += "you can optionally add it by enabling `Add any property not already in file`. "
        HelpString += "Note, this only works for `Custom` properties.  Adding `System` properties is not allowed. "

        HelpString += vbCrLf + vbCrLf + "To delete a property, "
        HelpString += "set the Find Search to `X`. "
        HelpString += "As above, this only works for `Custom` properties. "

        HelpString += vbCrLf + vbCrLf + "If you are changing `System.Material` specifically, you can "
        HelpString += "also update the properties associated with the material itself. "
        HelpString += "Select the option `For material, update density, face styles, etc.`. "

        HelpString += vbCrLf + vbCrLf + "**Expressions**"

        HelpString += vbCrLf + vbCrLf + "An `expression` is similar to a formula in Excel. "
        HelpString += "Expressions enable more complex manipulations of the `Replace` string. "
        HelpString += "To create one, click the `Expression Editor` button on the input editor form. "

        HelpString += vbCrLf + vbCrLf + "![Expression Editor](My%20Project/media/expression_editor.png)"

        HelpString += vbCrLf + vbCrLf + "You can perform string processing, "
        HelpString += "create logical expressions, do arithmetic, and, well, almost anything.  The available functions are listed below. "
        HelpString += "Like Excel, the expression must return a value.  Nested functions are the norm for complex manipulations. "
        HelpString += "Unlike Excel, multi-line text is allowed, which can make the code more readable. "

        HelpString += vbCrLf + vbCrLf + "You can check your expression using the `Test` button. "
        HelpString += "If there are undefined variables, for example `%{Custom.Engineer}`, it prompts you for a value. "
        HelpString += "You can `Save` or `Save As` your expression with the buttons provided. "
        HelpString += "Retreive them with the `Saved Expressions` drop-down. "
        HelpString += "That drop-down comes with a few examples. You can study those to get the hang of it. "
        HelpString += "To learn more, click the `Help` button.  That opens a web site with lots of useful information, and links to more. "

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

        HelpString += vbCrLf + vbCrLf + "**Edit Outside Solid Edge (Experimental)**"

        HelpString += vbCrLf + vbCrLf + "Direct edit using Windows Structured Storage for fast execution. "
        HelpString += "Like *blazingly* fast -- 100x to 400x faster than Solid Edge. "
        HelpString += "If you want to try this out, select the option `Edit properties outside Solid Edge`. "

        HelpString += vbCrLf + vbCrLf + "There are certain items Solid Edge presents as properties, "
        HelpString += "but do not actually reside in a Structured Storage `Property Stream`. "
        HelpString += "As such, they are not accessible using this technique. "
        HelpString += "There are quite a few of these, mostly related to materials, for example density, fill style, etc. "
        HelpString += "The only two that Housekeeper (but not Structured Storage) currently supports "
        HelpString += "are `System.Material` and `System.Sheet Metal Gage`. "

        HelpString += vbCrLf + vbCrLf + "Also, Structured Storage does not know about file links. "
        HelpString += "That means it cannot access models from their drawings. "
        HelpString += "Property callouts that require such access, for example `%{System.Material|R1}`, generate an error with this option. "

        Return HelpString
    End Function


End Class
