Option Strict On

Imports System.Text.RegularExpressions
Imports Newtonsoft.Json

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

    'Private _UpdateFaceStyles As Boolean
    'Public Property UpdateFaceStyles As Boolean
    '    Get
    '        Return _UpdateFaceStyles
    '    End Get
    '    Set(value As Boolean)
    '        _UpdateFaceStyles = value
    '        If Me.TaskOptionsTLP IsNot Nothing Then
    '            CType(ControlsDict(ControlNames.UpdateFaceStyles.ToString), CheckBox).Checked = value
    '        End If
    '    End Set
    'End Property

    'Private _RemoveFaceStyleOverrides As Boolean
    'Public Property RemoveFaceStyleOverrides As Boolean
    '    Get
    '        Return _RemoveFaceStyleOverrides
    '    End Get
    '    Set(value As Boolean)
    '        _RemoveFaceStyleOverrides = value
    '        If Me.TaskOptionsTLP IsNot Nothing Then
    '            CType(ControlsDict(ControlNames.RemoveFaceStyleOverrides.ToString), CheckBox).Checked = value
    '        End If
    '    End Set
    'End Property

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


    Enum ControlNames
        Edit
        JSONString
        AutoAddMissingProperty
        AutoUpdateMaterial
        UseConfigurationPageTemplates
        Browse
        MaterialTable
        'UpdateFaceStyles
        'RemoveFaceStyleOverrides
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
        Me.RequiresPropertiesData = True
        SetColorFromCategory(Me)
        'Me.SolidEdgeRequired = False
        Me.SolidEdgeRequired = True  ' Default is so checking the box toggles a property update

        GenerateTaskControl()
        TaskOptionsTLP = GenerateTaskOptionsTLP()
        Me.TaskControl.AddTaskOptionsTLP(TaskOptionsTLP)

        ' Options
        Me.MaterialTable = ""
        Me.JSONString = ""
        Me.AutoAddMissingProperty = False
        Me.StructuredStorageEdit = False
        Me.AutoUpdateMaterial = False
        Me.UseConfigurationPageTemplates = False
        'Me.UpdateFaceStyles = False
        'Me.RemoveFaceStyleOverrides = False

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
        ProcessInternal(FileName)
    End Sub

    Private Overloads Sub ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal SEApp As SolidEdgeFramework.Application
        )

        ' Convert glob to regex 
        ' https://stackoverflow.com/questions/74683013/regex-to-glob-and-vice-versa-conversion
        ' https://stackoverflow.com/questions/11276909/how-to-convert-between-a-glob-pattern-and-a-regexp-pattern-in-ruby
        ' https://learn.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator

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
            TaskLogger.AddMessage("No properties provided")
        End If

        If Not TaskLogger.HasErrors Then

            Dim IsFOA As Boolean = False
            If DocType = "asm" Then
                tmpAsmDoc = CType(SEDoc, SolidEdgeAssembly.AssemblyDocument)
                IsFOA = tmpAsmDoc.IsFileFamilyByDocument
            End If

            If (DocType = "asm") And (IsFOA) Then
                Dim Members As SolidEdgeAssembly.AssemblyFamilyMembers = tmpAsmDoc.AssemblyFamilyMembers
                If Not Members.GlobalEditMode Then
                    TaskLogger.AddMessage("Cannot process FOA with 'Apply edits to all members' disabled")
                Else
                    For Each Member As SolidEdgeAssembly.AssemblyFamilyMember In Members
                        Members.ActivateMember(Member.MemberName)
                        SEApp.DoIdle()
                        DoFindReplace(SEApp, CType(tmpAsmDoc, SolidEdgeFramework.SolidEdgeDocument), PropertiesToEditDict)
                    Next
                End If
            Else
                DoFindReplace(SEApp, SEDoc, PropertiesToEditDict)
            End If

        End If

        If Not TaskLogger.HasErrors Then
            If SEDoc.ReadOnly Then
                TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
            Else
                SEDoc.Save()
                SEApp.DoIdle()
            End If
        Else
            Dim s = "Errors encountered.  No changes made."
            If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)

        End If

    End Sub

    Private Overloads Sub ProcessInternal(ByVal FullName As String)

        ' Structured Storage
        ' https://github.com/ironfede/openmcdf

        Dim PropertySetName As String = ""
        Dim PropertyName As String = ""
        Dim FindString As String = ""
        Dim ReplaceString As String = ""
        Dim FindSearchType As String = ""
        Dim ReplaceSearchType As String = ""

        Dim PropertyNameEnglish As String

        Dim AutoAdd As Boolean

        Dim PropertyFound As Boolean = False

        Dim Proceed As Boolean = True
        Dim s As String
        Dim tf As Boolean

        Dim PropertiesToEditDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim PropertiesToEdit As String = ""
        Dim RowIndexString As String

        Dim UC As New UtilsCommon

        PropertiesToEdit = Me.JSONString

        If Not PropertiesToEdit = "" Then
            PropertiesToEditDict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Dictionary(Of String, String)))(PropertiesToEdit)
        Else
            Proceed = False
            TaskLogger.AddMessage("No properties provided")
        End If

        Dim SSDoc As HCStructuredStorageDoc = Nothing

        Try
            SSDoc = New HCStructuredStorageDoc(FullName)
        Catch ex As Exception
            If SSDoc IsNot Nothing Then SSDoc.Close()
            Proceed = False
            TaskLogger.AddMessage(ex.Message)
        End Try

        If Proceed Then
            SSDoc.ReadProperties(Me.PropertiesData)

            For Each RowIndexString In PropertiesToEditDict.Keys

                ' The loop continues even in case of error.
                ' The resulting error message is more complete that way.

                ' ####################### Get parameters #######################

                PropertyName = PropertiesToEditDict(RowIndexString)("PropertyName")
                PropertySetName = PropertiesToEditDict(RowIndexString)("PropertySet")
                FindSearchType = PropertiesToEditDict(RowIndexString)("FindSearch")
                FindString = PropertiesToEditDict(RowIndexString)("FindString")
                ReplaceSearchType = PropertiesToEditDict(RowIndexString)("ReplaceSearch")
                ReplaceString = PropertiesToEditDict(RowIndexString)("ReplaceString")

                Dim tmpPropertyData As PropertyData = Me.PropertiesData.GetPropertyData(PropertyName)
                If tmpPropertyData Is Nothing Then
                    Proceed = False
                    TaskLogger.AddMessage(String.Format("Property '{0}' not recognized", PropertyName))
                End If

                AutoAdd = (Me.AutoAddMissingProperty) And (PropertySetName.ToLower = "custom")

                ' ####################### Do formula substitution #######################

                If Proceed Then
                    DoFormulaSubstitution(SSDoc, PropertyName, ReplaceSearchType, FindString, ReplaceString)
                    If TaskLogger.HasErrors Then Proceed = False
                End If

                PropertyNameEnglish = tmpPropertyData.EnglishName

                ' ####################### Check for existence of property #######################
                ' Not an error if AutoAdd = TRUE

                If Proceed Then
                    tf = (SSDoc.ExistsProp(PropertySetName, PropertyNameEnglish)) Or (AutoAdd)
                    If Not tf Then
                        Proceed = False
                        If PropertyName = PropertyNameEnglish Then
                            s = String.Format("Property '{0}' not found or not recognized.", PropertyName)
                        Else
                            s = String.Format("Property '{0}({1})' not found or not recognized.", PropertyName, PropertyNameEnglish)
                        End If
                        If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
                    End If

                End If

                ' ####################### Delete or do the replacement #######################

                If Proceed Then
                    DoReplacement(SSDoc, PropertySetName, PropertyName, PropertyNameEnglish,
                        FindSearchType, FindString, ReplaceSearchType, ReplaceString)

                    If TaskLogger.HasErrors Then Proceed = False

                End If
            Next

        End If

        If Not TaskLogger.HasErrors Then
            If SSDoc IsNot Nothing Then
                SSDoc.Save()
                SSDoc.Close()
            End If
        Else
            s = "Errors encountered.  No changes made."
            If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
        End If

    End Sub


    Private Sub DoReplacement(
        SSDoc As HCStructuredStorageDoc,
        PropertySetName As String,
        PropertyName As String,
        PropertyNameEnglish As String,
        FindSearchType As String,
        FindString As String,
        ReplaceSearchType As String,
        ReplaceString As String
        )

        Dim UC As New UtilsCommon

        Dim tf As Boolean
        Dim Proceed As Boolean = True
        Dim s As String
        Dim AddProp As Boolean
        'Dim UpdateMatl As Boolean

        AddProp = (Me.AutoAddMissingProperty) And (PropertySetName.ToLower = "custom")
        'UpdateMatl = (Me.AutoUpdateMaterial) And (PropertySetName.ToLower = "system") And (PropertyName.ToLower = "material")

        If FindSearchType = "X" Then
            tf = SSDoc.DeleteProp(PropertySetName, PropertyNameEnglish)

            If Not tf Then
                Proceed = False
                If PropertyName = PropertyNameEnglish Then
                    s = String.Format("Unable to delete property '{0}'.  This command only works on custom properties.", PropertyName)
                Else
                    s = String.Format("Unable to delete property '{0}({1})'.  This command only works on custom properties.", PropertyName, PropertyNameEnglish)
                End If
                If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
            End If

        Else
            Dim PropertyValue As String = CStr(SSDoc.GetPropValue(PropertySetName, PropertyNameEnglish))
            If PropertyValue Is Nothing Then
                If AddProp Then
                    Proceed = SSDoc.AddProp(PropertySetName, PropertyNameEnglish, Value:=Nothing)
                    If Proceed Then
                        PropertyValue = CStr(SSDoc.GetPropValue(PropertySetName, PropertyNameEnglish))
                        If PropertyValue Is Nothing Then
                            Proceed = False
                        End If
                    End If
                Else
                    Proceed = False
                    If PropertyName = PropertyNameEnglish Then
                        s = String.Format("Property '{0}' not found or not recognized.", PropertyName)
                    Else
                        s = String.Format("Property '{0}({1})' not found or not recognized.", PropertyName, PropertyNameEnglish)
                    End If
                    If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                End If

            End If

            If Proceed Then
                If FindSearchType = "PT" Then
                    PropertyValue = PropertyValue.Replace(FindString, ReplaceString)
                    tf = SSDoc.SetPropValue(PropertySetName, PropertyNameEnglish, PropertyValue, AddProperty:=AddProp)

                Else
                    If FindSearchType = "WC" Then
                        FindString = UC.GlobToRegex(FindString)
                    End If
                    If ReplaceSearchType = "PT" Then
                        ' ReplaceString = Regex.Escape(ReplaceString)
                    End If

                    PropertyValue = Regex.Replace(PropertyValue, FindString, ReplaceString, RegexOptions.IgnoreCase)
                    tf = SSDoc.SetPropValue(PropertySetName, PropertyNameEnglish, PropertyValue, AddProperty:=AddProp)

                End If

                If Not tf Then
                    Proceed = False
                    If PropertyName = PropertyNameEnglish Then
                        s = String.Format("Unable to replace property value '{0}'.", PropertyName)
                    Else
                        s = String.Format("Unable to replace property value '{0}({1})'.", PropertyName, PropertyNameEnglish)
                    End If
                    If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                End If

            End If

            'If Proceed Then
            '    If UpdateMatl Then
            '        Dim SSMatTable As New HCStructuredStorageDoc(Me.MaterialTable)
            '        SSMatTable.ReadMaterialTable()
            '        tf = SSMatTable.UpdateMaterial(SSDoc)

            '        SSMatTable.Close()

            '        If Not tf Then
            '            Proceed = False
            '            ExitStatus = 1
            '            If PropertyName = PropertyNameEnglish Then
            '                s = String.Format("Unable to update '{0}'.", PropertyName)
            '            Else
            '                s = String.Format("Unable to update '{0}({1})'.", PropertyName, PropertyNameEnglish)
            '            End If
            '            If Not ErrorMessageList.Contains(s) Then ErrorMessageList.Add(s)
            '        End If

            '    End If
            'End If
        End If

    End Sub

    Private Sub DoFormulaSubstitution(
        SSDoc As HCStructuredStorageDoc,
        PropertyName As String,
        ReplaceSearchType As String,
        ByRef FindString As String,
        ByRef ReplaceString As String
        )

        Dim s As String

        FindString = SSDoc.SubstitutePropertyFormulas(FindString, ValidFilenameRequired:=False)
        If FindString Is Nothing Then
            s = String.Format("Unable to process formula in Find text '{0}' for property '{1}'", FindString, PropertyName)
            If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
        End If

        Dim OriginalReplaceString As String = ""
        If ReplaceSearchType = "EX" Then OriginalReplaceString = ReplaceString
        ReplaceString = SSDoc.SubstitutePropertyFormulas(ReplaceString, ValidFilenameRequired:=False, ReplaceSearchType = "EX")
        If ReplaceString Is Nothing Then
            If Not ReplaceSearchType = "EX" Then
                s = String.Format("Unable to process formula in Replace text '{0}' for property '{1}'", ReplaceString, PropertyName)
                If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
            Else
                s = String.Format("Unable to evaluate expression in Replace text '{0}' for property '{1}'", OriginalReplaceString, PropertyName)
                If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
            End If
        End If

    End Sub

    Private Sub DoFindReplace(
        SEApp As SolidEdgeFramework.Application,
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        PropertiesToEditDict As Dictionary(Of String, Dictionary(Of String, String))
        )

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
                Dim FullName As String = UC.SplitFOAName(SEDoc.FullName)("Filename")

                FindString = UC.SubstitutePropertyFormula(SEDoc, FullName, FindString, ValidFilenameRequired:=False, Me.PropertiesData)
                If FindString Is Nothing Then
                    Proceed = False
                    s = String.Format("Unable to process formula in Find text '{0}' for property '{1}'", FindString, PropertyName)
                    If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                End If

                Dim OriginalReplaceString As String = ""
                If ReplaceSearchType = "EX" Then OriginalReplaceString = ReplaceString
                ReplaceString = UC.SubstitutePropertyFormula(SEDoc, FullName, ReplaceString, ValidFilenameRequired:=False, Me.PropertiesData, ReplaceSearchType = "EX")
                If ReplaceString Is Nothing Then
                    Proceed = False
                    If Not ReplaceSearchType = "EX" Then
                        s = String.Format("Unable to process formula in Replace text '{0}' for property '{1}'", ReplaceString, PropertyName)
                        If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                    Else
                        s = String.Format("Unable to evaluate expression in Replace text '{0}' for property '{1}'", OriginalReplaceString, PropertyName)
                        If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                    End If
                End If
            End If

            ' ####################### Get the property object from the file #######################

            If Proceed Then
                Try
                    Prop = UC.GetProp(SEDoc, PropertySetName, PropertyName, 0, AutoAddMissingProperty)
                    If Prop Is Nothing Then
                        Proceed = False
                        s = String.Format("Property '{0}.{1}' not found.", PropertySetName, PropertyName)
                        If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                    End If

                Catch ex As Exception
                    Proceed = False
                    s = String.Format("Property '{0}.{1}' not found or not recognized.", PropertySetName, PropertyName)
                    If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
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
                        s = String.Format("Unable to delete property '{0}'.  This command only works on custom properties.", PropertyName)
                        If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                    End Try

                Else

                    Dim PropValue As String = ""
                    Dim SETypeName As String = ""

                    Try
                        Dim TypeName = Microsoft.VisualBasic.Information.TypeName(Prop.Value) ' Integer, String, Double, Date, Boolean

                        If ReplaceSearchType = "EX" Then

                        End If

                        If FindSearchType = "PT" Then
                            PropValue = Replace(CType(Prop.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)
                        Else
                            If FindSearchType = "WC" Then
                                FindString = UC.GlobToRegex(FindString)
                            End If
                            If ReplaceSearchType = "PT" Then
                                ' ReplaceString = Regex.Escape(ReplaceString)
                            End If

                            PropValue = Regex.Replace(CType(Prop.Value, String), FindString, ReplaceString, RegexOptions.IgnoreCase)
                        End If

                        Select Case TypeName.ToLower
                            Case "string"
                                SETypeName = "Text"
                                Prop.Value = PropValue

                            Case "integer"
                                SETypeName = "Number"

                                Proceed = False
                                s = String.Format("Property '{0}': Currently unable to process variable type '{1}'", PropertyName, SETypeName)
                                If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)

                                ' First try int, then double
                                'Try
                                '    Dim i As Integer = CInt(PropValue)
                                '    Prop.Value = i  <- This doesn't work, but doesn't throw an exception
                                '    If Not CInt(Prop.Value) = i Then  <- This breaks something.  Bombs out later getting SEDoc.Fullname.
                                '        Dim k = 0
                                '    End If
                                '    Dim j = 0
                                'Catch ex2 As Exception
                                '    Dim d As Double = CDbl(PropValue)
                                '    Prop.Value = d
                                '    Dim j = 0
                                'End Try

                            Case "double"
                                SETypeName = "Number"

                                Proceed = False
                                s = String.Format("Property '{0}': Currently unable to process variable type '{1}'", PropertyName, SETypeName)
                                If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)

                                'Dim d As Double = CDbl(PropValue)
                                'Prop.Value = d

                            Case "date"
                                SETypeName = "Date"
                                Prop.Value = CType(PropValue, DateTime)

                            Case "boolean"
                                SETypeName = "Yes or No"
                                Prop.Value = CBool(PropValue)
                        End Select

                    Catch ex As Exception
                        Proceed = False
                        s = String.Format("Unable to set '{0}' (variable type '{1}') to '{2}'.", PropertyName, SETypeName, PropValue)
                        If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                    End Try

                End If

            End If


            ' ####################### Save the properties #######################

            If Proceed Then
                Try
                    SEApp.DoIdle()
                    PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)
                    For Each Properties In PropertySets
                        Dim ss = Properties.Name
                        Properties.Save()
                        SEApp.DoIdle()
                    Next
                    If SEDoc.ReadOnly Then
                        s = "Cannot save document marked 'Read Only'"
                        If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)

                    Else
                        SEDoc.Save()
                        SEApp.DoIdle()
                    End If

                Catch ex As Exception
                    Proceed = False
                    s = "Problem accessing or saving Property."
                    If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
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
                            UM.UpdateMaterialFromMaterialTable(
                                SEApp, SEDoc, Me.MaterialTable, False, True, False, "", Nothing, False, False, Me.TaskLogger)

                        Case Else
                            ' Not an error
                    End Select
                End If
            End If

        Next

    End Sub


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

        CheckBox = FormatOptionsCheckBox(ControlNames.StructuredStorageEdit.ToString, "Run task without Solid Edge")
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
        'CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UseConfigurationPageTemplates.ToString, "Use configuration page material table")
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

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

        'RowIndex += 1

        'CheckBox = FormatOptionsCheckBox(ControlNames.UpdateFaceStyles.ToString, "Update face styles")
        'AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        'tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        'tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        'ControlsDict(CheckBox.Name) = CheckBox
        'CheckBox.Visible = False

        'RowIndex += 1

        'CheckBox = FormatOptionsCheckBox(ControlNames.RemoveFaceStyleOverrides.ToString, "Remove face style overrides")
        'AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        'tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        'tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        'ControlsDict(CheckBox.Name) = CheckBox
        'CheckBox.Visible = False

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
        'CheckBox.Checked = True
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        ControlsDict(CheckBox.Name) = CheckBox

        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)

        Return tmpTLPOptions
    End Function

    Public Overrides Sub CheckStartConditions(ErrorLogger As Logger)

        If Me.IsSelectedTask Then
            If Not (Me.IsSelectedAssembly Or Me.IsSelectedPart Or Me.IsSelectedSheetmetal Or Me.IsSelectedDraft) Then
                ErrorLogger.AddMessage("Select at least one type of file to process")
            End If

            If (Me.JSONString = "") Or (Me.JSONString = "{}") Then
                ErrorLogger.AddMessage("Select at least one property to edit")
            End If

            If Me.AutoUpdateMaterial Then
                If Not FileIO.FileSystem.FileExists(Me.MaterialTable) Then
                    ErrorLogger.AddMessage("Select a valid material table")
                End If

            End If

            If (Not Me.SolidEdgeRequired) And (Me.PropertiesData.Items.Count = 0) Then
                ErrorLogger.AddMessage("Template properties required for 'Edit outside SE'.  Update them on the Configuration Tab -- Templates Page")
            End If
        End If

    End Sub


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

                    'Me.TemplatePropertyDict = FPIE.TemplatePropertyDict
                    Me.PropertiesData = FPIE.PropertiesData

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
        ''Dim Ctrl As Control
        'Dim Button As Button
        'Dim TextBox As TextBox
        'Dim CheckBox2 As CheckBox
        Dim tf As Boolean

        Select Case Name
            Case ControlNames.AutoAddMissingProperty.ToString '"AutoAddMissingProperty"
                Me.AutoAddMissingProperty = Checkbox.Checked

            Case ControlNames.StructuredStorageEdit.ToString
                Me.StructuredStorageEdit = Checkbox.Checked
                Me.RequiresSave = Not Checkbox.Checked
                Me.SolidEdgeRequired = Not Checkbox.Checked

                CType(ControlsDict(ControlNames.AutoUpdateMaterial.ToString), CheckBox).Visible = Not Me.StructuredStorageEdit

                tf = (Not Me.StructuredStorageEdit) And (Me.AutoUpdateMaterial)

                CType(ControlsDict(ControlNames.UseConfigurationPageTemplates.ToString), CheckBox).Visible = tf
                CType(ControlsDict(ControlNames.UseConfigurationPageTemplates.ToString), CheckBox).Visible = tf

                tf = (Not Me.StructuredStorageEdit) And (Me.AutoUpdateMaterial) And (Not Me.UseConfigurationPageTemplates)

                CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = tf
                CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox).Visible = tf

                'tf = (Not Me.StructuredStorageEdit) And (Me.AutoUpdateMaterial)

                'CType(ControlsDict(ControlNames.UpdateFaceStyles.ToString), CheckBox).Visible = tf
                'CType(ControlsDict(ControlNames.RemoveFaceStyleOverrides.ToString), CheckBox).Visible = tf

            Case ControlNames.AutoUpdateMaterial.ToString '"AutoUpdateMaterial"
                Me.AutoUpdateMaterial = Checkbox.Checked
                Checkbox.Visible = Not Me.StructuredStorageEdit

                tf = Me.AutoUpdateMaterial

                CType(ControlsDict(ControlNames.UseConfigurationPageTemplates.ToString), CheckBox).Visible = tf
                'CType(ControlsDict(ControlNames.UpdateFaceStyles.ToString), CheckBox).Visible = tf
                'CType(ControlsDict(ControlNames.RemoveFaceStyleOverrides.ToString), CheckBox).Visible = tf

                tf = Me.AutoUpdateMaterial And Not Me.UseConfigurationPageTemplates

                CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = tf
                CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox).Visible = tf

            Case ControlNames.UseConfigurationPageTemplates.ToString
                Me.UseConfigurationPageTemplates = Checkbox.Checked
                Checkbox.Visible = Not Me.StructuredStorageEdit And Me.AutoUpdateMaterial

                If Me.UseConfigurationPageTemplates Then
                    Me.MaterialTable = Form_Main.MaterialTable
                    CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = False
                    CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox).Visible = False

                Else
                    CType(ControlsDict(ControlNames.Browse.ToString), Button).Visible = True
                    CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox).Visible = True

                End If

            'Case ControlNames.UpdateFaceStyles.ToString
            '    Me.UpdateFaceStyles = Checkbox.Checked
            '    Checkbox.Visible = Not Me.StructuredStorageEdit And Me.AutoUpdateMaterial

            '    'CType(ControlsDict(ControlNames.RemoveFaceStyleOverrides.ToString), CheckBox).Visible = Me.UpdateFaceStyles

            'Case ControlNames.RemoveFaceStyleOverrides.ToString
            '    Me.RemoveFaceStyleOverrides = Checkbox.Checked
            '    Checkbox.Visible = Not Me.StructuredStorageEdit And Me.AutoUpdateMaterial

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
        HelpString += "[<ins>**Property Filter**</ins>](#property-filter) "
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
        HelpString += "For more options to control the material updates, "
        HelpString += "take a look at the command `Update material from material table`."
        HelpString += "Note this option is not currently compatible with `Run task without Solid Edge`. "

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
        HelpString += "When you are satisfied with your expression, dismiss the dialog by clicking the `X` on the upper right. "
        HelpString += "The expression will be copied to the clipboard.  Click in the desired Replace text box and type CTRL-V. "

        HelpString += vbCrLf + vbCrLf + "You can `Save` or `Save As` your expression with the buttons provided. "
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

        HelpString += vbCrLf + vbCrLf + "**Run Task Without Solid Edge (Experimental)**"

        HelpString += vbCrLf + vbCrLf + "This option opens the file with Windows Structured Storage, instead of Solid Edge. "
        HelpString += "It's *blazingly* fast -- 100x to 400x faster than Solid Edge. "
        HelpString += "If you want to try this out, select the option `Run task without Solid Edge`. "

        HelpString += vbCrLf + vbCrLf + "Note, Solid Edge presents exposed variables as Custom properties.  "
        HelpString += "You can change those with this command, but Solid Edge will overwrite them the next time the file is opened. "
        HelpString += "For those, rather than using this command, use `Edit Variables` instead. "

        Return HelpString
    End Function


End Class
