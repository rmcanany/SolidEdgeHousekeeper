Option Strict On

'Imports System.Text.RegularExpressions
'Imports Newtonsoft.Json

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
        Me.RequiresLinkManagementOrder = True
        SetColorFromCategory(Me)
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

        OleMessageFilter.Register()

        Dim NewWay As Boolean = True

        If NewWay Then

            Dim Proceed As Boolean = True

            Proceed = InitiateEdit2(SEApp, SEDoc, Nothing)

            If Proceed Then
                If SEDoc.ReadOnly Then
                    TaskLogger.AddMessage("Cannot save document marked 'Read Only'")
                Else
                    'SEApp.DisplayAlerts = True
                    'SEApp.DoIdle()
                    SEDoc.Save()
                    'SEApp.DisplayAlerts = False
                    SEApp.DoIdle()
                End If
            Else
                Dim s As String = "Errors encountered.  No changes made."
                If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
            End If

        Else
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

            Dim UC As New UtilsCommon

            Dim DocType As String = UC.GetDocType(SEDoc)

            Dim tmpAsmDoc As SolidEdgeAssembly.AssemblyDocument = Nothing

            Dim PropertiesToEditDict As Dictionary(Of String, Dictionary(Of String, String))
            PropertiesToEditDict = GetPropertiesToEditDict()
            If PropertiesToEditDict Is Nothing Then Proceed = False

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
                Dim s As String = "Errors encountered.  No changes made."
                If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)

            End If
        End If

    End Sub

    Private Overloads Sub ProcessInternal(ByVal FullName As String)

        ' Structured Storage
        ' https://github.com/ironfede/openmcdf

        Dim NewWay As Boolean = True

        If NewWay Then

            Dim Proceed As Boolean = True

            Dim SSDoc As HCStructuredStorageDoc = Nothing

            Try
                SSDoc = New HCStructuredStorageDoc(FullName, _OpenReadWrite:=True)
                SSDoc.ReadProperties(Me.PropertiesData)
                SSDoc.ReadLinks(Me.LinkManagementOrder)
            Catch ex As Exception
                If SSDoc IsNot Nothing Then SSDoc.Close()
                Proceed = False
                TaskLogger.AddMessage(ex.Message)
            End Try

            If Proceed And SSDoc.IsFOA Then
                Proceed = False
                TaskLogger.AddMessage("FOA processing outside of SE not currently implemented")
            End If

            If Proceed Then
                Proceed = InitiateEdit2(Nothing, Nothing, SSDoc)
            End If

            If Proceed Then
                If SSDoc IsNot Nothing Then
                    SSDoc.Save()
                End If
            Else
                Dim s As String = "Errors encountered.  No changes made."
                If Not TaskLogger.ContainsMessage(s) Then TaskLogger.AddMessage(s)
            End If

            If SSDoc IsNot Nothing Then SSDoc.Close()

        Else
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

            Dim RowIndexString As String

            Dim UC As New UtilsCommon

            Dim PropertiesToEditDict As Dictionary(Of String, Dictionary(Of String, String))
            PropertiesToEditDict = GetPropertiesToEditDict()
            If PropertiesToEditDict Is Nothing Then Proceed = False

            Dim SSDoc As HCStructuredStorageDoc = Nothing

            Try
                SSDoc = New HCStructuredStorageDoc(FullName, _OpenReadWrite:=True)
            Catch ex As Exception
                If SSDoc IsNot Nothing Then SSDoc.Close()
                Proceed = False
                TaskLogger.AddMessage(ex.Message)
            End Try

            If Proceed And SSDoc.IsFOA Then
                Proceed = False
                TaskLogger.AddMessage("FOA processing outside of SE not currently implemented")
            End If

            If Proceed Then
                SSDoc.ReadProperties(Me.PropertiesData)
                SSDoc.ReadLinks(Me.LinkManagementOrder)

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
                        TaskLogger.AddMessage($"Property '{PropertyName}' not recognized")
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
                        tf = AutoAdd Or FindSearchType = "X" Or SSDoc.ExistsProp(PropertySetName, PropertyNameEnglish)
                        If Not tf Then
                            Proceed = False
                            If PropertyName = PropertyNameEnglish Then
                                s = $"Property '{PropertyName}' not found or not recognized."
                            Else
                                s = $"Property '{PropertyName}({PropertyNameEnglish})' not found or not recognized."
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

        End If
    End Sub


    Private Function InitiateEdit2(
        SEApp As SolidEdgeFramework.Application,
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        SSDoc As HCStructuredStorageDoc
        ) As Boolean

        Dim Proceed As Boolean = True

        Dim PropertiesToEditDict As Dictionary(Of String, Dictionary(Of String, String))
        PropertiesToEditDict = GetPropertiesToEditDict()  ' Function populates TaskLogger if needed

        If PropertiesToEditDict Is Nothing Then Proceed = False
        If Not Proceed Then Return False

        If SEDoc IsNot Nothing Then
            Dim UC As New UtilsCommon

            Dim DocType As String = UC.GetDocType(SEDoc)
            Dim tmpAsmDoc As SolidEdgeAssembly.AssemblyDocument = Nothing
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
                        DoFindReplace2(SEApp, SEDoc, Nothing, PropertiesToEditDict)
                    Next
                End If
            Else
                DoFindReplace2(SEApp, SEDoc, Nothing, PropertiesToEditDict)
            End If

            If Me.TaskLogger.HasErrors Then Proceed = False

        Else
            DoFindReplace2(Nothing, Nothing, SSDoc, PropertiesToEditDict)

            If Me.TaskLogger.HasErrors Then Proceed = False

        End If

        Return Proceed
    End Function

    Private Sub DoFindReplace2(
        SEApp As SolidEdgeFramework.Application,
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        SSDoc As HCStructuredStorageDoc,
        PropertiesToEditDict As Dictionary(Of String, Dictionary(Of String, String))
        )

        'Dim Proceed As Boolean = True

        For Each EditStepIdx As String In PropertiesToEditDict.Keys

            Dim Proceed As Boolean = True  ' Resetting to True at each step, for a more complete error message.

            Dim StepLogger As Logger = TaskLogger.AddLogger($"Edit Step {CInt(EditStepIdx) + 1}")


            ' ####################### Get parameters #######################

            Dim PropertyName As String = PropertiesToEditDict(EditStepIdx)("PropertyName")
            Dim PropertySetName As String = PropertiesToEditDict(EditStepIdx)("PropertySet")
            Dim FindSearchType As String = PropertiesToEditDict(EditStepIdx)("FindSearch")
            Dim FindString As String = PropertiesToEditDict(EditStepIdx)("FindString")
            Dim ReplaceSearchType As String = PropertiesToEditDict(EditStepIdx)("ReplaceSearch")
            Dim ReplaceString As String = PropertiesToEditDict(EditStepIdx)("ReplaceString")


            ' ####################### Get property data #######################

            Dim tmpPropertyData As PropertyData = Me.PropertiesData.GetPropertyData(PropertySetName, PropertyName)
            Dim PropertyNameEnglish As String = ""
            If tmpPropertyData Is Nothing Then
                Proceed = False
                StepLogger.AddMessage($"Property '{PropertySetName}.{PropertyName}' not recognized")
            Else
                PropertyNameEnglish = tmpPropertyData.EnglishName
            End If


            ' ####################### Formula substitution #######################

            If Proceed Then
                Proceed = DoFormulaSubstitution2(SEDoc, SSDoc, PropertyName, ReplaceSearchType, FindString, ReplaceString, StepLogger)
                'If StepLogger.HasErrors Then Proceed = False
            End If


            ' ####################### Replacement #######################

            If Proceed Then
                Proceed = DoReplacement2(SEDoc, SSDoc, PropertySetName, PropertyName, PropertyNameEnglish,
                                         FindSearchType, FindString, ReplaceSearchType, ReplaceString, StepLogger)
                'If StepLogger.HasErrors Then Proceed = False
            End If


            ' ####################### Update SEDoc material #######################

            If Proceed And SEDoc IsNot Nothing Then
                Proceed = UpdateMaterial2(SEApp, SEDoc, PropertySetName, PropertyName, PropertyNameEnglish, StepLogger)
            End If


            ' ####################### Save SEDoc properties #######################

            If Proceed And SEDoc IsNot Nothing Then
                Proceed = SaveProperties2(SEApp, SEDoc, StepLogger)
            End If

        Next

    End Sub

    Private Function DoFormulaSubstitution2(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        SSDoc As HCStructuredStorageDoc,
        PropertyName As String,
        ReplaceSearchType As String,
        ByRef FindString As String,
        ByRef ReplaceString As String,
        ErrorLogger As Logger
        ) As Boolean

        Dim Proceed As Boolean = True

        Dim UC As New UtilsCommon
        Dim s As String


        ' ###### FIND STRING ######

        Dim OriginalFindString As String = FindString

        If SEDoc IsNot Nothing Then
            FindString = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, FindString, Me.PropertiesData, Me.TaskLogger, IsExpression:=False)
        Else
            FindString = SSDoc.SubstitutePropertyFormulas(FindString, TaskLogger, IsExpression:=False)
        End If

        If FindString Is Nothing Then
            Proceed = False
            s = $"Unable to process formula in Find text '{OriginalFindString}' for property '{PropertyName}'"
            If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
        End If


        ' ###### REPLACE STRING ######

        Dim OriginalReplaceString As String = ReplaceString  ' Just for error reporting.

        If ReplaceSearchType = "EX" Then
            If SEDoc IsNot Nothing Then
                ReplaceString = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, ReplaceString, Me.PropertiesData, Me.TaskLogger, IsExpression:=True)
            Else
                ReplaceString = SSDoc.SubstitutePropertyFormulas(ReplaceString, TaskLogger, IsExpression:=True)
            End If

            If ReplaceString Is Nothing OrElse ReplaceString.ToLower.Contains("<nothing>") Then
                Proceed = False
                s = $"Unable to evaluate expression in Replace text '{OriginalReplaceString}' for property '{PropertyName}'"
                If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
            End If

        Else
            If SEDoc IsNot Nothing Then
                ReplaceString = UC.SubstitutePropertyFormulas(SEDoc, SEDoc.FullName, ReplaceString, Me.PropertiesData, Me.TaskLogger, IsExpression:=False)
            Else
                ReplaceString = SSDoc.SubstitutePropertyFormulas(ReplaceString, TaskLogger, IsExpression:=False)
            End If

            If ReplaceString Is Nothing Then
                Proceed = False
                s = $"Unable to process formula in Replace text '{ReplaceString}' for property '{PropertyName}'"
                If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
            End If
        End If

        Return Proceed
    End Function

    Private Function DoReplacement2(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        SSDoc As HCStructuredStorageDoc,
        PropertySetName As String,
        PropertyName As String,
        PropertyNameEnglish As String,
        FindSearchType As String,
        FindString As String,
        ReplaceSearchType As String,
        ReplaceString As String,
        ErrorLogger As Logger
        ) As Boolean

        Dim UC As New UtilsCommon

        Dim Proceed As Boolean = True

        If FindSearchType = "X" Then
            Proceed = DeleteProp2(SEDoc, SSDoc, PropertySetName, PropertyName, PropertyNameEnglish, ErrorLogger)

        Else
            Dim AddProp As Boolean = (Me.AutoAddMissingProperty) And (PropertySetName.ToLower = "custom")

            Dim PropertyValue As String = Nothing
            Proceed = GetPropValue2(SEDoc, SSDoc, PropertySetName, PropertyName, PropertyNameEnglish, AddProp, PropertyValue, ErrorLogger)

            If Proceed Then
                ' Remove trailing carriage return characters
                PropertyValue = PropertyValue.Trim

                If FindSearchType = "PT" Then
                    ' FindString can be blank.  The Replace method can't handle that.  It can handle a blank ReplaceString.
                    If Not FindString = "" Then
                        PropertyValue = PropertyValue.Replace(FindString, ReplaceString)
                    Else
                        PropertyValue = ReplaceString
                    End If

                Else
                    If FindSearchType = "WC" Then
                        FindString = UC.GlobToRegex(FindString)
                    End If

                    PropertyValue = Text.RegularExpressions.Regex.Replace(
                        PropertyValue, FindString, ReplaceString, Text.RegularExpressions.RegexOptions.IgnoreCase)

                End If

                Dim tf As Boolean

                If SEDoc IsNot Nothing Then
                    tf = UC.SetPropValue(SEDoc, PropertySetName, PropertyName, 0, AddProp, PropertyValue, ErrorLogger)
                Else
                    tf = SSDoc.SetPropValue(PropertySetName, PropertyNameEnglish, PropertyValue, AddProperty:=AddProp)
                End If

                If Not tf Then
                    Proceed = False

                    Dim s As String
                    If PropertyName = PropertyNameEnglish Then
                        s = $"Unable to replace property value '{PropertyName}'."
                    Else
                        s = $"Unable to replace property value '{PropertyName}({PropertyNameEnglish})'."
                    End If
                    If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
                End If

            End If

        End If

        Return Proceed
    End Function

    Private Function GetPropValue2(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        SSDoc As HCStructuredStorageDoc,
        PropertySetName As String,
        PropertyName As String,
        PropertyNameEnglish As String,
        AddProp As Boolean,
        ByRef PropertyValue As String,
        ErrorLogger As Logger
        ) As Boolean

        Dim Proceed As Boolean = True

        Dim UC As New UtilsCommon
        Dim tf As Boolean = True
        Dim s As String

        'Dim PropertyValue As String = Nothing

        If SEDoc IsNot Nothing Then
            PropertyValue = CStr(UC.GetPropValue(SEDoc, PropertySetName, PropertyName, ModelLinkIdx:=0, AddProp:=False))
        Else
            PropertyValue = CStr(SSDoc.GetPropValue(PropertySetName, PropertyNameEnglish))
        End If

        If PropertyValue Is Nothing Then
            If AddProp Then
                If SEDoc IsNot Nothing Then
                    Dim tmpProp = UC.GetProp(SEDoc, PropertySetName, PropertyName, ModelLinkIdx:=0, AddProp)
                    If tmpProp Is Nothing Then Proceed = False
                Else
                    Proceed = SSDoc.AddProp(PropertySetName, PropertyNameEnglish, Value:=Nothing)
                End If

                If Proceed Then
                    If SEDoc IsNot Nothing Then
                        PropertyValue = CStr(UC.GetPropValue(SEDoc, PropertySetName, PropertyName, ModelLinkIdx:=0, AddProp:=False))
                    Else
                        PropertyValue = CStr(SSDoc.GetPropValue(PropertySetName, PropertyNameEnglish))
                    End If
                    If PropertyValue Is Nothing Then
                        Proceed = False
                    End If
                End If
            Else
                Proceed = False
                If PropertyName = PropertyNameEnglish Then
                    s = $"Property '{PropertyName}' not found or not recognized."
                Else
                    s = $"Property '{PropertyName}({PropertyNameEnglish})' not found or not recognized."
                End If
                If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
            End If

        End If

        Return Proceed
    End Function

    Private Function DeleteProp2(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        SSDoc As HCStructuredStorageDoc,
        PropertySetName As String,
        PropertyName As String,
        PropertyNameEnglish As String,
        ErrorLogger As Logger
        ) As Boolean

        Dim Proceed As Boolean = True

        Dim UC As New UtilsCommon
        Dim tf As Boolean = True
        Dim s As String

        Try
            If SEDoc IsNot Nothing Then
                tf = UC.DeleteProp(SEDoc, PropertySetName, PropertyName)
            Else
                tf = SSDoc.DeleteProp(PropertySetName, PropertyNameEnglish)
            End If
        Catch ex As Exception
            Proceed = False
            ErrorLogger.AddMessage($"Unable to delete property.  Exception {ex.Message}")
            tf = False
        End Try

        If Proceed And Not tf Then
            Proceed = False
            If PropertyName = PropertyNameEnglish Then
                s = $"Unable to delete property '{PropertyName}'.  This command only works on custom properties."
            Else
                s = $"Unable to delete property '{PropertyName}({PropertyNameEnglish})'.  This command only works on custom properties."
            End If
            If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
        End If

        Return Proceed
    End Function

    Private Function UpdateMaterial2(
        SEApp As SolidEdgeFramework.Application,
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        PropertySetName As String,
        PropertyName As String,
        PropertyNameEnglish As String,
        ErrorLogger As Logger
        ) As Boolean

        Dim Proceed As Boolean = True

        Dim UC As New UtilsCommon

        Dim tf As Boolean = True
        tf = tf And (PropertySetName.ToLower = "system")
        tf = tf And (PropertyNameEnglish.ToLower = "material")
        tf = tf And (Me.AutoUpdateMaterial)

        If tf Then
            Select Case UC.GetDocType(SEDoc)
                Case "par", "psm"
                    Dim UM As New UtilsMaterials
                    UM.UpdateMaterialFromMaterialTable(SEApp, SEDoc, Me.MaterialTable,
                                                       _RemoveFaceStyleOverrides:=False,
                                                       _UpdateFaceStyles:=True,
                                                       _UseFinishFaceStyle:=False,
                                                       _FinishName:="",
                                                       _ExcludedFinishesList:=Nothing,
                                                       _OverrideBodyFaceStyle:=False,
                                                       _OverrideMaterialFaceStyle:=False,
                                                       ErrorLogger)
                    If ErrorLogger.HasErrors Then Proceed = False

                Case Else
                    ' Not an error
            End Select
        End If


        Return Proceed
    End Function

    Private Function SaveProperties2(
        SEApp As SolidEdgeFramework.Application,
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ErrorLogger As Logger
        ) As Boolean

        Dim Proceed As Boolean = True

        Dim PropertySets As SolidEdgeFramework.PropertySets

        Try
            SEApp.DoIdle()
            PropertySets = CType(SEDoc.Properties, SolidEdgeFramework.PropertySets)
            For Each Properties As SolidEdgeFramework.Properties In PropertySets
                Dim ss = Properties.Name
                Properties.Save()
                SEApp.DoIdle()
            Next

            'If SEDoc.ReadOnly Then
            '    Dim s As String = "Cannot save document marked 'Read Only'"
            '    If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)

            'Else
            '    SEDoc.Save()
            '    SEApp.DoIdle()
            'End If

        Catch ex As Exception
            Proceed = False
            Dim s As String = $"Problem accessing or saving Properties.  Exception: {ex.Message}"
            If Not ErrorLogger.ContainsMessage(s) Then ErrorLogger.AddMessage(s)
        End Try

        Return Proceed
    End Function



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

        AddProp = (Me.AutoAddMissingProperty) And (PropertySetName.ToLower = "custom")

        If FindSearchType = "X" Then
            If SSDoc.ExistsProp(PropertySetName, PropertyNameEnglish) Then ' Not an error if it's already not there.
                tf = SSDoc.DeleteProp(PropertySetName, PropertyNameEnglish)

                If Not tf Then
                    Proceed = False
                    If PropertyName = PropertyNameEnglish Then
                        s = $"Unable to delete property '{PropertyName}'.  This command only works on custom properties."
                    Else
                        s = $"Unable to delete property '{PropertyName}({PropertyNameEnglish})'.  This command only works on custom properties."
                    End If
                    If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                End If

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
                        s = $"Property '{PropertyName}' not found or not recognized."
                    Else
                        s = $"Property '{PropertyName}({PropertyNameEnglish})' not found or not recognized."
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

                    PropertyValue = Text.RegularExpressions.Regex.Replace(PropertyValue, FindString, ReplaceString, Text.RegularExpressions.RegexOptions.IgnoreCase)
                    tf = SSDoc.SetPropValue(PropertySetName, PropertyNameEnglish, PropertyValue, AddProperty:=AddProp)

                End If

                If Not tf Then
                    Proceed = False
                    If PropertyName = PropertyNameEnglish Then
                        s = $"Unable to replace property value '{PropertyName}'."
                    Else
                        s = $"Unable to replace property value '{PropertyName}({PropertyNameEnglish})'."
                    End If
                    If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                End If

            End If

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

        FindString = SSDoc.SubstitutePropertyFormulas(FindString, TaskLogger)
        If FindString Is Nothing Then
            s = $"Unable to process formula in Find text '{FindString}' for property '{PropertyName}'"
            If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
        End If

        If ReplaceSearchType = "EX" Then
            Dim OriginalReplaceString As String = ReplaceString  ' Just for error reporting.
            ReplaceString = SSDoc.SubstitutePropertyFormulas(ReplaceString, TaskLogger, True)
            If ReplaceString Is Nothing OrElse ReplaceString.ToLower.Contains("<nothing>") Then
                s = $"Unable to evaluate expression in Replace text '{OriginalReplaceString}' for property '{PropertyName}'"
                If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
            End If
        Else
            ReplaceString = SSDoc.SubstitutePropertyFormulas(ReplaceString, TaskLogger)
            If ReplaceString Is Nothing Then
                s = $"Unable to process formula in Replace text '{ReplaceString}' for property '{PropertyName}'"
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
                Dim FullName As String = UC.GetFOAFilename(SEDoc.FullName)

                FindString = UC.SubstitutePropertyFormulas(SEDoc, FullName, FindString, Me.PropertiesData, TaskLogger)
                If FindString Is Nothing Then
                    Proceed = False
                    s = $"Unable to process formula in Find text '{FindString}' for property '{PropertyName}'"
                    If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                End If

                If ReplaceSearchType = "EX" Then
                    Dim OriginalReplaceString As String = ReplaceString

                    ReplaceString = UC.SubstitutePropertyFormulas(SEDoc, FullName, ReplaceString, Me.PropertiesData, TaskLogger, True)

                    If ReplaceString Is Nothing OrElse ReplaceString.ToLower.Contains("<nothing>") Then
                        Proceed = False
                        s = $"Unable to evaluate expression in Replace text '{OriginalReplaceString}' for property '{PropertyName}'"
                        If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                    End If
                Else
                    ReplaceString = UC.SubstitutePropertyFormulas(SEDoc, FullName, ReplaceString, Me.PropertiesData, TaskLogger)
                    If ReplaceString Is Nothing Then
                        Proceed = False
                        s = $"Unable to process formula in Replace text '{ReplaceString}' for property '{PropertyName}'"
                        If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                    End If
                End If
            End If

            ' ####################### Get the property object from the file #######################

            If Proceed Then
                Try
                    Prop = UC.GetProp(SEDoc, PropertySetName, PropertyName, 0, AutoAddMissingProperty)
                    If Prop Is Nothing Then
                        If Not FindSearchType = "X" Then
                            Proceed = False
                            s = $"Property '{PropertySetName}.{PropertyName}' not found."
                            If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                        End If
                    End If

                Catch ex As Exception
                    Proceed = False
                    s = $"Property '{PropertySetName}.{PropertyName}' not found or not recognized.  Exception: {ex.Message}"
                    If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                End Try

            End If

            ' ####################### Delete or do the replacement. #######################

            If Proceed Then

                If FindSearchType = "X" Then
                    Try
                        If Prop IsNot Nothing Then
                            Prop.Delete()
                        End If
                    Catch ex As Exception
                        Proceed = False
                        s = $"Unable to delete property '{PropertyName}'.  This command only works on custom properties.  Exception: {ex.Message}"
                        If Not Me.TaskLogger.ContainsMessage(s) Then Me.TaskLogger.AddMessage(s)
                    End Try

                Else

                    Dim PropValue As String = ""
                    Dim SETypeName As String = ""

                    Try
                        Dim TypeName = Microsoft.VisualBasic.Information.TypeName(Prop.Value) ' Integer, String, Double, Date, Boolean

                        If FindSearchType = "PT" Then
                            If Not FindString.Trim = "" Then
                                PropValue = Replace(CType(Prop.Value, String), FindString, ReplaceString, 1, -1, vbTextCompare)
                            Else
                                PropValue = ReplaceString
                            End If

                        Else
                            If FindSearchType = "WC" Then
                                FindString = UC.GlobToRegex(FindString)
                            End If

                            PropValue = Text.RegularExpressions.Regex.Replace(
                                CType(Prop.Value, String), FindString, ReplaceString, Text.RegularExpressions.RegexOptions.IgnoreCase)
                        End If

                        Select Case TypeName.ToLower
                            Case "string"
                                SETypeName = "Text"
                                'Prop.Value = PropValue
                                If Not PropValue.Trim = "" Then
                                    Prop.Value = PropValue.Trim
                                Else
                                    Prop.Value = " "  ' Cannot set Prop.Value = "" with API.  Works in UI.
                                End If

                            Case "integer"
                                SETypeName = "Number"

                                Proceed = False
                                s = $"Property '{PropertyName}': Currently unable to process variable type '{SETypeName}'"
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
                                s = $"Property '{PropertyName}': Currently unable to process variable type '{SETypeName}'"
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
                        s = $"Unable to set '{PropertyName}' (variable type '{SETypeName}') to '{PropValue}'.  Exception: {ex.Message}"
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
                    s = $"Problem accessing or saving Property.  Exception: {ex.Message}"
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


    Private Function GetPropertiesToEditDict() As Dictionary(Of String, Dictionary(Of String, String))

        Dim PropertiesToEditDict As Dictionary(Of String, Dictionary(Of String, String)) = Nothing
        Dim PropertiesToEdit As String = ""

        Dim UC As New UtilsCommon

        PropertiesToEdit = Me.JSONString

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

            If PropertiesToEdit.StartsWith("SavedSetting") Then
                Dim UP As New UtilsPreferences
                Dim EditPropertiesSavedSettingsDict = UP.GetEditPropertiesSavedSettings
                Dim Key As String = Me.JSONString.Split(":"c)(1)
                PropertiesToEditDict = EditPropertiesSavedSettingsDict(Key)
                Dim i = 0
            Else
                PropertiesToEditDict = Newtonsoft.Json.JsonConvert.DeserializeObject(
                    Of Dictionary(Of String, Dictionary(Of String, String)))(PropertiesToEdit)
            End If

        Else
            TaskLogger.AddMessage("No properties provided")
        End If

        Return PropertiesToEditDict
    End Function


    Private Function GenerateTaskOptionsTLP() As ExTableLayoutPanel
        Dim tmpTLPOptions = New ExTableLayoutPanel

        Dim RowIndex As Integer
        Dim CheckBox As CheckBox
        Dim Button As Button
        Dim TextBox As TextBox

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

        RowIndex += 1

        CheckBox = FormatOptionsCheckBox(ControlNames.UseConfigurationPageTemplates.ToString, "Use configuration page material table")
        CheckBox.Padding = New Padding(Me.ControlIndent, 0, 0, 0)
        AddHandler CheckBox.CheckedChanged, AddressOf CheckBoxOptions_Check_Changed
        tmpTLPOptions.Controls.Add(CheckBox, 0, RowIndex)
        tmpTLPOptions.SetColumnSpan(CheckBox, 2)
        ControlsDict(CheckBox.Name) = CheckBox
        CheckBox.Visible = False

        RowIndex += 1

        Button = FormatOptionsButton(ControlNames.Browse.ToString, "Matl Table")
        Button.Margin = New Padding(Me.ControlIndent, 0, 0, 0)
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

        CheckBox = FormatOptionsCheckBox(ControlNames.AutoHideOptions.ToString, ManualOptionsOnlyString)
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

            If Me.PropertiesData.Items.Count = 0 Then
                ErrorLogger.AddMessage("Template properties not found.  Update them on the Configuration Tab -- Templates Page")
            End If

            If Not Me.SolidEdgeRequired Then
                If Me.LinkManagementOrder Is Nothing Then
                    ErrorLogger.AddMessage("LinkManagementOrder is null.  Set LinkMgmt.txt on the Configuration Tab -- Top Level Assembly Page")
                ElseIf Me.LinkManagementOrder.Count = 0 Then
                    ErrorLogger.AddMessage("LinkMgmt.txt file does not contain any search order information")
                End If
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

                    Me.PropertiesData = FPIE.PropertiesData

                End If

            Case ControlNames.Browse.ToString
                Dim tmpFileDialog As New OpenFileDialog
                tmpFileDialog.Title = "Select a material table file"
                tmpFileDialog.Filter = "Material Documents|*.mtl"

                If IO.File.Exists(Me.MaterialTable) Then
                    tmpFileDialog.InitialDirectory = IO.Path.GetDirectoryName(Me.MaterialTable)
                Else
                    tmpFileDialog.InitialDirectory = Form_Main.SEMaterialsPath
                End If

                If tmpFileDialog.ShowDialog() = DialogResult.OK Then
                    Me.MaterialTable = tmpFileDialog.FileName
                    TextBox = CType(ControlsDict(ControlNames.MaterialTable.ToString), TextBox)
                    TextBox.Text = Me.MaterialTable

                    'Form_Main.SEMaterialsPath = IO.Path.GetDirectoryName(Me.MaterialTable)
                End If

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
        End Select


    End Sub

    Public Sub CheckBoxOptions_Check_Changed(sender As System.Object, e As System.EventArgs)
        Dim Checkbox = CType(sender, CheckBox)
        Dim Name = Checkbox.Name
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


            Case ControlNames.AutoUpdateMaterial.ToString '"AutoUpdateMaterial"
                Me.AutoUpdateMaterial = Checkbox.Checked
                Checkbox.Visible = Not Me.StructuredStorageEdit

                tf = Me.AutoUpdateMaterial

                CType(ControlsDict(ControlNames.UseConfigurationPageTemplates.ToString), CheckBox).Visible = tf

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

            Case ControlNames.AutoHideOptions.ToString '"HideOptions"
                Me.TaskControl.AutoHideOptions = Checkbox.Checked
                If Not Me.AutoHideOptions = TaskControl.AutoHideOptions Then
                    Me.AutoHideOptions = Checkbox.Checked
                End If

            Case Else
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
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
                MsgBox($"{Me.Name} Name '{Name}' not recognized")
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
        HelpString += "for more information.  "
        HelpString += "Note this mode in the `Replace Text` assumes a `capture group` from a `Find Text` `RX` "
        HelpString += "(I don't know what that means, either)."
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

        HelpString += vbCrLf + vbCrLf + "An `expression` is basically a program. "
        HelpString += "It enables more complex manipulations of the `Replace` string. "
        HelpString += "To create one, right-click the `Replace` textbox and select `Edit expression`. "
        HelpString += "To use one you have already saved, select `Insert expression` instead. "

        HelpString += vbCrLf + vbCrLf + "There are two programming languages available, `NCalc` and `VB`. "
        HelpString += "Choose which one to use on the toolbar. "
        HelpString += "`VB` is a full-featured programming language.  Explaining its use is beyond the scope of this Help Topic.  "
        HelpString += "`NCalc` is more like a formula in Excel. Some of its features are detailed below. "

        HelpString += vbCrLf + vbCrLf + "Oh wait, there is one thing to say about `VB`.  "
        HelpString += "That is about when a property is not found in the file. "
        HelpString += "Normally VB returns the null object `Nothing` when that happens.  "
        HelpString += "Expressions can't deal with that at present.  "
        HelpString += "So instead of `Nothing` it returns the `String` `""<Nothing>""`.  "
        HelpString += "You can check for that in your code if needed. "

        HelpString += vbCrLf + vbCrLf + "![Expression Editor](My%20Project/media/expression_editor.png)"

        HelpString += vbCrLf + vbCrLf + "The toolbar has two sections, `Editor` and `Saved Expressions`. "

        HelpString += vbCrLf + vbCrLf + "`Editor` commands"

        HelpString += vbCrLf + vbCrLf + "- `Select program language` Choose either `NCalc` or `VB` from the drop down."
        HelpString += vbCrLf + "- `Test` Check your expression.  If there are undefined variables, for example `%{Custom.Engineer}`, it prompts you for a value."
        HelpString += vbCrLf + "- `Test on Edge` Same as above except variables are read from a file.  SE must be running with the target file active. "
        HelpString += vbCrLf + "- `Clear` Deletes all text from the editor window. "
        HelpString += vbCrLf + "- `""%{}""` Insert property.  Brings up a form for you to choose it. "

        HelpString += vbCrLf + vbCrLf + "`Saved Expressions` commands"

        HelpString += vbCrLf + vbCrLf + "- `Select` Choose a saved expression from the drop down. The drop-down comes with a few examples. You can study those to get the hang of it. "
        HelpString += vbCrLf + "- `Save` Save an expression. "
        HelpString += vbCrLf + "- `Save As` Save an expression with a new name. "
        HelpString += vbCrLf + "- `Delete` Delete the expression from the saved expressions. "

        HelpString += vbCrLf + vbCrLf + "With `NCalc` you can perform string processing, "
        HelpString += "create logical expressions, do arithmetic, and, well, almost anything.  The available functions are listed below. "
        HelpString += "Like Excel, the expression must return a value.  Nested functions are the norm for complex manipulations. "
        HelpString += "Unlike Excel, multi-line text is allowed, which can make the code more readable. "

        HelpString += vbCrLf + vbCrLf + "`NCalc` functions"
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
