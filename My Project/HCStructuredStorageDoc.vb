' https://support.sw.siemens.com/en-US/knowledge-base/PL8758767
' https://community.sw.siemens.com/s/question/0D5Vb000006uL9iKAE/how-to-repair-corrupt-daft-drawing-file

Option Strict On

Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml
Imports OpenMcdf
Imports OpenMcdf.Extensions
Imports OpenMcdf.Extensions.OLEProperties
Imports PanoramicData.NCalcExtensions

Public Class HCStructuredStorageDoc

    Public Property FullName As String
    Public Property OpenReadWrite As Boolean

    Private Property PropSets As PropertySets
    Private Property fs As FileStream
    Private Property cf As CompoundFile
    Private Property LinkNames As LinkFullNames
    Private Property LinkManagementOrder As List(Of String)
    Private Property PropertiesData As HCPropertiesData
    Private Property MatTable As MaterialTable
    Private Property DocType As String 'asm, dft, par, psm, mtl
    Private Property BlkLibrary As BlockLibrary
    Private Property VarNames As VariableNames


    Public Enum StatusSecurityMapping
        ssmAvailable = 0
        ssmInWork = 0
        ssmInReview = 0
        ssmReleased = 4
        ssmBaselined = 4
        ssmObsolete = 4
    End Enum


    Public Sub New(_FullName As String, Optional _OpenReadWrite As Boolean = True)

        Me.FullName = _FullName
        Me.OpenReadWrite = _OpenReadWrite

        Try
            If _OpenReadWrite Then
                Me.fs = New FileStream(Me.FullName, FileMode.Open, FileAccess.ReadWrite)
            Else
                Me.fs = New FileStream(Me.FullName, FileMode.Open, FileAccess.Read)
            End If
        Catch ex As Exception
            'Dim L As Integer = Me.FullName.Count
            Throw New Exception(String.Format("Unable to open file.  {0}", ex.Message))
        End Try

        Me.cf = Nothing
        Try
            Dim cfg As CFSConfiguration = CFSConfiguration.SectorRecycle Or CFSConfiguration.EraseFreeSectors
            Me.cf = New CompoundFile(fs, CFSUpdateMode.Update, cfg)
        Catch ex As Exception
            If Me.fs IsNot Nothing Then Me.fs.Close()
            Me.fs = Nothing
            Throw New Exception(String.Format("Unable to open file.  {0}", ex.Message))
        End Try

        Me.DocType = IO.Path.GetExtension(FullName).ToLower.Replace(".", "")

    End Sub


    Public Sub ReadProperties(_PropertiesData As HCPropertiesData)
        Me.PropertiesData = _PropertiesData
        Me.PropSets = New PropertySets(Me.cf, Me.FullName, Me.OpenReadWrite)
    End Sub

    Public Sub ReadLinks(_LinkManagementOrder As List(Of String))
        If _LinkManagementOrder Is Nothing Then
            Throw New Exception("LinkManagementOrder is null.  Set LinkMgmt.txt on the Configuration Tab -- Top Level Assembly Page")
        End If
        Me.LinkManagementOrder = _LinkManagementOrder
        Me.LinkNames = New LinkFullNames(cf, IsFOA(), Me.LinkManagementOrder, Me.FullName)
    End Sub

    Public Sub ReadMaterialTable()
        Dim Extension As String = IO.Path.GetExtension(Me.FullName)

        If Not Extension = ".mtl" Then
            Throw New Exception(String.Format("'{0}' is not a material table file", IO.Path.GetFileName(Me.FullName)))
        End If

        Me.MatTable = New MaterialTable(Me.cf)
    End Sub

    Public Sub ReadBlockLibrary()
        Dim Extension As String = IO.Path.GetExtension(Me.FullName)

        If Not Extension = ".dft" Then
            Throw New Exception(String.Format("Cannot process blocks for '{0}' file types", Extension))
        End If

        Me.BlkLibrary = New BlockLibrary(Me.cf, Me.FullName)
    End Sub

    Public Sub ReadVariableNames()
        Dim Extension As String = IO.Path.GetExtension(Me.FullName)

        If Extension = ".dft" Then
            Throw New Exception(String.Format("Cannot get variable names for '{0}' file types", Extension))
        End If

        Me.VarNames = New VariableNames(Me.cf, Me.FullName)
    End Sub


    Public Function IsFOA() As Boolean
        If IO.Path.GetExtension(Me.FullName) = ".asm" Then
            Return Me.cf.RootStorage.ContainsStorage("Master")
        Else
            Return False
        End If
    End Function

    Public Function IsFOPMaster() As Boolean
        Return Me.cf.RootStorage.ContainsStorage("FamilyOfParts")
    End Function

    Public Function IsMaterialTable(cf As CompoundFile) As Boolean
        Return cf.RootStorage.ContainsStream("MaterialDataEx")
    End Function

    Public Sub Save()

        If Me.PropSets IsNot Nothing Then
            For Each PropSet In Me.PropSets.Items
                ' The Custom stream gets saved with DocumentSummaryInformation
                If Not PropSet.Name.ToLower = "custom" Then
                    PropSet.Save()
                End If
            Next
        End If

        Me.cf.Commit()

    End Sub

    Public Sub Close()
        If Me.cf IsNot Nothing Then Me.cf.Close()
        Me.cf = Nothing

        If Me.fs IsNot Nothing Then Me.fs.Close()
        Me.fs = Nothing
    End Sub

    Public Function GetPropValue(PropSetName As String, PropNameEnglish As String) As Object
        ' Return Nothing if a value is not found

        Dim Value As Object = Nothing

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        PropNameEnglish = PropNameEnglish.ToLower

        Value = ProcessSpecialProperty(PropSetName, PropNameEnglish)

        If Value Is Nothing Then
            Dim Prop As Prop = GetProp(PropSetName, PropNameEnglish)
            If Prop IsNot Nothing Then
                Value = Prop.Value
            End If
        End If

        Return Value
    End Function

    Public Function GetPropTypeName(PropSetName As String, PropNameEnglish As String) As String
        ' Returns Nothing if the property is not found

        Dim TypeName As String = Nothing
        Dim VTType As VTPropertyType = Nothing

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        PropNameEnglish = PropNameEnglish.ToLower

        Dim Prop As Prop = GetProp(PropSetName, PropNameEnglish)

        If Prop IsNot Nothing Then
            VTType = Prop.VTType
            Select Case VTType
                Case = VTPropertyType.VT_BOOL
                    TypeName = "Boolean"
                Case = VTPropertyType.VT_I4
                    TypeName = "Integer"
                Case = VTPropertyType.VT_LPSTR, VTPropertyType.VT_LPWSTR
                    TypeName = "String"
                Case = VTPropertyType.VT_FILETIME
                    TypeName = "Date"
                Case = VTPropertyType.VT_R8
                    TypeName = "Double"
                Case Else
                    Dim s = GetType(HCStructuredStorageDoc).FullName
                    MsgBox(String.Format("In {0}: Property type {1} not recognized", s, VTType.ToString))
            End Select
        End If

        Return TypeName
    End Function

    Public Function SetPropValue(PropSetName As String, PropNameEnglish As String, Value As Object, AddProperty As Boolean) As Boolean
        ' Returns False if unsuccessful

        Dim Success As Boolean = False

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        PropNameEnglish = PropNameEnglish.ToLower

        If ProcessSpecialProperty(PropSetName, PropNameEnglish) Is Nothing Then
            Dim Prop = GetProp(PropSetName, PropNameEnglish)

            If Prop IsNot Nothing Then
                Success = Prop.SetValue(Value)
            ElseIf AddProperty Then
                Success = AddProp(PropSetName, PropNameEnglish, Value)
            End If
        End If

        Return Success
    End Function

    Public Function AddProp(PropSetName As String, PropNameEnglish As String, Value As Object) As Boolean
        Dim Success As Boolean = False

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        Dim PropSet As PropertySet = Me.PropSets.GetItem(PropSetName)

        PropNameEnglish = PropNameEnglish.ToLower

        If PropSetName.ToLower = "custom" Then
            Dim Prop = GetProp(PropSetName, PropNameEnglish)
            If Prop Is Nothing Then
                Success = PropSet.AddProp(PropNameEnglish, Value)
            End If
        End If

        Return Success
    End Function

    Public Function DeleteProp(PropSetName As String, PropNameEnglish As String) As Boolean
        Dim Success As Boolean = False

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        Dim PropSet As PropertySet = Me.PropSets.GetItem(PropSetName)

        PropNameEnglish = PropNameEnglish.ToLower

        If PropSetName.ToLower = "custom" Then
            Dim Prop = GetProp(PropSetName, PropNameEnglish)
            If Prop IsNot Nothing Then
                Success = PropSet.DeleteProp(PropNameEnglish)
            End If
        End If

        Return Success
    End Function

    Public Function ExistsProp(PropSetName As String, PropNameEnglish As String) As Boolean
        Dim Success As Boolean = False

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        PropNameEnglish = PropNameEnglish.ToLower

        If ProcessSpecialProperty(PropSetName, PropNameEnglish) Is Nothing Then
            Dim Prop = GetProp(PropSetName, PropNameEnglish)

            Success = Prop IsNot Nothing
        Else
            Success = True
        End If

        Return Success
    End Function

    Public Function GetStatus() As String
        Dim Status As String = Nothing
        Dim StatusConstant As SolidEdgeConstants.DocumentStatus

        StatusConstant = CType(GetPropValue("System", "Status"), SolidEdgeConstants.DocumentStatus)

        Select Case StatusConstant
            Case SolidEdgeConstants.DocumentStatus.igStatusAvailable
                Status = "Available"
            Case SolidEdgeConstants.DocumentStatus.igStatusBaselined
                Status = "Baselined"
            Case SolidEdgeConstants.DocumentStatus.igStatusInReview
                Status = "InReview"
            Case SolidEdgeConstants.DocumentStatus.igStatusInWork
                Status = "InWork"
            Case SolidEdgeConstants.DocumentStatus.igStatusObsolete
                Status = "Obsolete"
            Case SolidEdgeConstants.DocumentStatus.igStatusReleased
                Status = "Released"
        End Select

        Return Status
    End Function

    Public Function SetStatus(Status As String) As Boolean
        Dim Success As Boolean

        Dim CurrentStatus As String

        Dim NewStatusConstant As SolidEdgeConstants.DocumentStatus
        Dim NewSecurity As StatusSecurityMapping

        CurrentStatus = GetStatus()

        If Status.ToLower = CurrentStatus.ToLower Then
            Success = True
        Else
            Select Case Status.ToLower
                Case "Available".ToLower
                    NewStatusConstant = SolidEdgeConstants.DocumentStatus.igStatusAvailable
                    NewSecurity = StatusSecurityMapping.ssmAvailable
                Case "Baselined".ToLower
                    NewStatusConstant = SolidEdgeConstants.DocumentStatus.igStatusBaselined
                    NewSecurity = StatusSecurityMapping.ssmBaselined
                Case "InReview".ToLower
                    NewStatusConstant = SolidEdgeConstants.DocumentStatus.igStatusInReview
                    NewSecurity = StatusSecurityMapping.ssmInReview
                Case "InWork".ToLower
                    NewStatusConstant = SolidEdgeConstants.DocumentStatus.igStatusInWork
                    NewSecurity = StatusSecurityMapping.ssmInWork
                Case "Obsolete".ToLower
                    NewStatusConstant = SolidEdgeConstants.DocumentStatus.igStatusObsolete
                    NewSecurity = StatusSecurityMapping.ssmObsolete
                Case "Released".ToLower
                    NewStatusConstant = SolidEdgeConstants.DocumentStatus.igStatusReleased
                    NewSecurity = StatusSecurityMapping.ssmReleased
            End Select

            'Success = SetPropValue("System", "Doc_Security", NewSecurity, AddProperty:=False)
            Success = SetPropValue("System", "Security", NewSecurity, AddProperty:=False)
            Success = Success And SetPropValue("System", "Status", NewStatusConstant, AddProperty:=False)
        End If

        Return Success
    End Function

    Public Function GetPropNames() As List(Of String)
        Dim PropNames As New List(Of String)

        For Each _PropSet As PropertySet In Me.PropSets.Items
            If Not _PropSet.Name = "Custom" Then
                For Each _Prop As Prop In _PropSet.Items
                    If Not PropNames.Contains(_Prop.Name) Then PropNames.Add(String.Format("{0}, {1}, {2}", _PropSet.Name, CStr(_Prop.PropertyIdentifier), _Prop.Name))
                Next
            End If
        Next

        Return PropNames
    End Function

    Public Function SubstitutePropertyFormulas(
         InString As String,
         ErrorLogger As Logger,
         Optional IsExpression As Boolean = False
         ) As String

        ' Replaces property formulas in a string.
        ' Returns Nothing if an error occurs

        ' Examples
        ' "Material: %{System.Material}, Engineer: %{Custom.Engineer}" --> "Material: STEEL, Engineer: FRED"
        ' "%{System.Titulo}"                                           --> "Ciao Pizza!"
        ' "%{Custom.Engineer|R1}"                                      --> "Fred" (From index reference 1)
        ' "%{Server.Query|R3}"                                         --> "Some response from server" (From server query 3)

        If Me.PropSets Is Nothing Then
            Throw New Exception("Properties not initialized")
        End If

        Dim OutString As String = Nothing

        Dim UC As New UtilsCommon
        Dim UP As New UtilsPreferences

        Dim Proceed As Boolean = True

        Dim DocValues As New List(Of String)
        Dim DocValue As String

        Dim Formulas As New List(Of String)
        Dim Formula As String

        Dim Matches As MatchCollection
        Dim MatchString As Match
        Dim Pattern As String

        Dim ExpressionLanguage As String = ""

        'If IsExpression And InString.StartsWith("EXPRESSION_") Then
        '    Dim TextToRemove As String = InString.Split(CChar(vbCrLf))(0)  ' EXPRESSION_VB, EXPRESSION_SQL

        '    Select Case TextToRemove
        '        Case "EXPRESSION_VB"
        '            ExpressionLanguage = "VB"
        '        Case "EXPRESSION_NCalc"
        '            ExpressionLanguage = "NCalc"
        '        Case Else
        '            ErrorLogger.AddMessage($"SubstitutePropertyFormulas: Expression header not recognized '{TextToRemove}'")
        '            'MsgBox($"SubstitutePropertyFormulas: Expression header not recognized '{TextToRemove}'")
        '            Return Nothing
        '    End Select

        '    InString = InString.Replace($"{TextToRemove}{vbCrLf}", "")

        'End If
        If IsExpression Then
            If InString.StartsWith("EXPRESSION_") Then
                Dim TextToRemove As String = InString.Split(CChar(vbCrLf))(0)  ' EXPRESSION_VB, EXPRESSION_SQL

                Select Case TextToRemove
                    Case "EXPRESSION_VB"
                        ExpressionLanguage = "VB"
                    Case "EXPRESSION_NCalc"
                        ExpressionLanguage = "NCalc"
                    Case Else
                        MsgBox($"SubstitutePropertyFormula: Expression header not recognized '{TextToRemove}'")
                        Return Nothing
                End Select

                InString = InString.Replace($"{TextToRemove}{vbCrLf}", "")

            ElseIf InString.StartsWith("SavedSetting:") Then  ' eg SavedSetting:StdNummer

                'Dim SavedExpressionsDict As Dictionary(Of String, Dictionary(Of String, String))
                'SavedExpressionsDict = UP.GetSavedExpressionsDict()

                'Dim SaveName As String = InString.Replace("SavedSetting:", "")
                'If SavedExpressionsDict.Keys.Contains(SaveName) Then
                '    ExpressionLanguage = SavedExpressionsDict(SaveName)("Language")
                '    InString = SavedExpressionsDict(SaveName)("Expression")
                'End If

                Dim SavedExpressions As New HCSavedExpressions

                Dim SaveName As String = InString.Replace("SavedSetting:", "")
                Dim tmpSE As SavedExpression = SavedExpressions.GetSavedExpression(SaveName)
                If tmpSE IsNot Nothing Then
                    ExpressionLanguage = tmpSE.Language
                    InString = SavedExpressions.ListOfStringToString(tmpSE.Expression)
                Else
                    MsgBox($"SubstitutePropertyFormula: Saved expression not found '{SaveName}'")
                    Return Nothing
                End If
            Else
                MsgBox($"SubstitutePropertyFormula: Expression header not recognized '{InString.Split(CChar(vbCrLf))(0)}'")
                Return Nothing
            End If

        End If


        ' Any number of substrings that start with "%{" and end with the first encountered "}".
        Pattern = "%{[^}]*}"
        Matches = Regex.Matches(InString, Pattern)
        If Matches.Count = 0 Then
            OutString = InString
            Proceed = False
        Else
            For Each MatchString In Matches
                Formulas.Add(MatchString.Value)
            Next
        End If

        If Proceed Then
            For Each Formula In Formulas
                DocValue = ProcessFormula(Formula, ErrorLogger, IsExpression)
                If DocValue Is Nothing Then
                    ErrorLogger.AddMessage($"Could not process formula '{Formula}'")
                    Return Nothing
                Else
                    If Not IsExpression Then
                        DocValues.Add(DocValue)
                    Else
                        DocValues.Add(DocValue.Replace("""", """"""))
                    End If
                End If
            Next
        End If

        If Proceed Then
            OutString = InString

            For i = 0 To DocValues.Count - 1
                OutString = OutString.Replace(Formulas(i), DocValues(i))
            Next

        End If

        If Proceed Then
            If IsExpression Then

                If ExpressionLanguage = "" Or ExpressionLanguage = "NCalc" Then
                    Dim nCalcExpression As New ExtendedExpression(OutString)
                    Try
                        Dim A = nCalcExpression.Evaluate()
                        OutString = A.ToString
                    Catch ex As Exception
                        ErrorLogger.AddMessage($"Could not process expression '{OutString}'")
                        ErrorLogger.AddMessage("Exception was:")
                        ErrorLogger.AddMessage(ex.Message)
                        OutString = Nothing
                    End Try

                Else  ' Must be VB
                    Dim UPS As New UtilsPowerShell
                    Dim PowerShellFileContents As List(Of String) = UPS.BuildExpressionFile(OutString.Split(CChar(vbCrLf)).ToList)

                    Dim PowerShellFilename As String = $"{UP.GetTempDirectory}\HousekeeperExpression.ps1"
                    'IO.File.WriteAllLines(PowerShellFilename, PowerShellFileContents, System.Text.Encoding.Unicode)

                    'Try
                    '    OutString = UPS.RunExpressionScript(PowerShellFilename)
                    'Catch ex As Exception
                    '    ErrorLogger.AddMessage($"Could not process expression '{OutString}'")
                    '    ErrorLogger.AddMessage("Exception was:")
                    '    ErrorLogger.AddMessage(ex.Message)
                    '    OutString = Nothing
                    'End Try

                    Dim tmpSuccess As Boolean = UPS.WriteExpressionFile(PowerShellFilename, PowerShellFileContents)

                    If tmpSuccess Then
                        Try
                            OutString = UPS.RunExpressionScript(PowerShellFilename)
                        Catch ex As Exception
                            ErrorLogger.AddMessage($"Unable to process expression '{OutString}'")
                            ErrorLogger.AddMessage("Exception was:")
                            ErrorLogger.AddMessage(ex.Message)
                            OutString = Nothing
                        End Try
                    Else
                        ErrorLogger.AddMessage($"Unable to create file '{PowerShellFilename}'")
                    End If

                End If

            End If
        End If


        Return OutString
    End Function


    Private Function ProcessFormula(
        Formula As String,
        ErrorLogger As Logger,
        IsExpression As Boolean
        ) As String

        Dim DocValue As String = Nothing

        Dim UC As New UtilsCommon

        Dim PropertySetName As String
        Dim PropertyName As String
        Dim PropertyNameEnglish As String = Nothing
        Dim ModelIdx As Integer

        Dim LinkName As String


        PropertySetName = UC.PropSetFromFormula(Formula)
        PropertyName = UC.PropNameFromFormula(Formula)
        ModelIdx = UC.ModelIdxFromFormula(Formula)

        If Not ((PropertySetName = "System") Or (PropertySetName = "Custom") Or (PropertySetName = "Server")) Then
            ErrorLogger.AddMessage($"PropertySet not recognized '{PropertySetName}'")
            Return Nothing
        End If

        If (PropertySetName = "Server") And (PropertyName = "Query") Then
            Dim tmpServerQuery = SubstitutePropertyFormulas(Form_Main.ServerQuery, ErrorLogger)
            Return Form_Main.ExecuteQuery(FullName, tmpServerQuery, ModelIdx).Replace(vbCrLf, " ")
        End If

        Dim tmpPropertyData As PropertyData = Me.PropertiesData.GetPropertyData(PropertyName)
        If tmpPropertyData IsNot Nothing Then
            PropertyNameEnglish = tmpPropertyData.EnglishName
        Else
            ErrorLogger.AddMessage($"Property not recognized '{PropertyName}'")
            Return Nothing
        End If

        If Not IsNothing(PropertyNameEnglish) Then

            If ModelIdx = 0 Then
                DocValue = CStr(GetPropValue(PropertySetName, PropertyNameEnglish))
                If DocValue Is Nothing Then
                    If Not IsExpression Then
                        ErrorLogger.AddMessage($"No value found for '{PropertySetName}.{PropertyNameEnglish}'")
                        Return Nothing
                    Else
                        DocValue = "<Nothing>"
                    End If
                End If

            Else
                If Me.LinkNames Is Nothing Then
                    ErrorLogger.AddMessage("LinkNames not initialized")
                    Return Nothing
                End If

                If Not Me.DocType = "dft" Then
                    ErrorLogger.AddMessage($"*.{DocType} files do not support '|R1' type references")
                    Return Nothing
                End If

                If ModelIdx > Me.LinkNames.Items.Count Then
                    Return Nothing
                End If

                LinkName = Me.LinkNames.Items(ModelIdx - 1)

                Dim SSDoc As HCStructuredStorageDoc = Nothing
                Try
                    SSDoc = New HCStructuredStorageDoc(LinkName)
                    SSDoc.ReadProperties(Me.PropertiesData)
                Catch ex As Exception
                    If SSDoc IsNot Nothing Then SSDoc.Close()
                    Return Nothing
                End Try

                DocValue = CStr(SSDoc.GetPropValue(PropertySetName, PropertyNameEnglish))
                If DocValue Is Nothing Then
                    If Not IsExpression Then
                        SSDoc.Close()
                        Return Nothing
                    Else
                        DocValue = "<Nothing>"
                    End If
                End If

                SSDoc.Close()
            End If

        End If

        Return DocValue

    End Function

    Private Function GetProp(
        PropSetName As String,
        PropNameEnglish As String,
        Optional ByRef PropSets As PropertySets = Nothing,
        Optional ByRef PropSet As PropertySet = Nothing
        ) As Prop

        If Me.PropSets Is Nothing Then
            Return Nothing
        End If

        PropNameEnglish = PropNameEnglish.ToLower

        Dim Prop As Prop = Nothing

        If (PropSetName.ToLower = "system") Or (PropSetName.ToLower = "duplicate") Then

            ' Need to search for it
            For Each PropSet In Me.PropSets.Items
                If Not PropSet.Name.ToLower = "custom" Then
                    For Each P As Prop In PropSet.Items
                        Dim tmpPName As String = P.Name
                        If P.Name = PropNameEnglish Then
                            Prop = P
                            Exit For
                        End If
                    Next
                    If Prop IsNot Nothing Then
                        Exit For
                    End If
                End If
            Next

        Else
            PropSet = Me.PropSets.GetItem(PropSetName)
            If PropSet IsNot Nothing Then
                Prop = PropSet.GetItem(PropNameEnglish)
            End If

        End If

        Return Prop
    End Function

    Private Function ProcessSpecialProperty(PropSetName As String, PropNameEnglish As String) As String
        ' Returns Nothing if not a special property
        ' Special properties:
        '     "System.File Name (full path)"     ' C:\project\part.par -> C:\project\part.par
        '     "System.File Name"                 ' C:\project\part.par -> part.par
        '     "System.File Name (no extension)"  ' C:\project\part.par -> part

        PropNameEnglish = PropNameEnglish.ToLower

        Dim SpecialProperty As String = Nothing

        If PropSetName.ToLower = "system" Then
            If PropNameEnglish = "File Name".ToLower Then
                SpecialProperty = System.IO.Path.GetFileName(Me.FullName)
            ElseIf PropNameEnglish = "File Name (full path)".ToLower Then
                SpecialProperty = Me.FullName
            ElseIf PropNameEnglish = "File Name (no extension)".ToLower Then
                SpecialProperty = System.IO.Path.GetFileNameWithoutExtension(Me.FullName)
            End If
        End If

        Return SpecialProperty
    End Function

    Private Sub OutputPropList()
        ' Utility if needed for testing.  Not used in production.
        Dim Outfile As String = ".\ole_props.csv"
        Dim OutList As New List(Of String)

        If IO.File.Exists(Outfile) Then
            Try
                OutList = IO.File.ReadAllLines(Outfile).ToList
            Catch ex As Exception
                MsgBox("Error reading Outfile")
            End Try
        End If

        OutList = Me.PropSets.PrepOutput(OutList, Me.FullName)

        Try
            IO.File.WriteAllLines(Outfile, OutList)
        Catch ex As Exception
            MsgBox("Error saving Outfile")
        End Try


    End Sub


    Public Function IsFileEmpty() As Boolean
        Dim IsEmpty As Boolean

        Select Case Me.DocType
            Case "par", "psm"
                Dim ParasolidStorage As CFStorage = Me.cf.RootStorage.GetStorage("PARASOLID")
                Dim StreamList As New List(Of CFStream)
                ParasolidStorage.VisitEntries(Sub(item) If item.IsStream Then StreamList.Add(CType(item, CFStream)), recursive:=False)
                IsEmpty = StreamList.Count = 0
            Case "asm", "dft"
                If Me.LinkNames IsNot Nothing Then
                    IsEmpty = (Me.LinkNames.Items.Count = 0) And (Me.LinkNames.BadLinks.Count = 0)
                Else
                    Dim tmpLinkManagmentOrder As List(Of String) = {"CONTAINER", "RELATIVE", "ABSOLUTE"}.ToList
                    Dim tmpLinkNames = New LinkFullNames(Me.cf, IsFOA(), tmpLinkManagmentOrder, Me.FullName)
                    IsEmpty = (tmpLinkNames.Items.Count = 0) And (tmpLinkNames.BadLinks.Count = 0)
                End If
        End Select

        Return IsEmpty
    End Function


    Public Function GetLinkNames() As List(Of String)
        If Me.LinkNames Is Nothing Then
            Throw New Exception("Links not initialized")
        End If

        Return Me.LinkNames.Items
    End Function

    Public Function GetBadLinkNames() As List(Of String)
        If Me.LinkNames Is Nothing Then
            Throw New Exception("Links not initialized")
        End If

        Return Me.LinkNames.BadLinks
    End Function


    Public Function MaterialInTable(Name As String) As Boolean
        If Me.MatTable Is Nothing Then
            Throw New Exception("Material table is not initialized")
        End If

        Return MatTable.GetMaterial(Name) IsNot Nothing
    End Function

    Public Function MaterialUpToDate(SSDoc As HCStructuredStorageDoc) As Boolean
        If Me.MatTable Is Nothing Then
            Throw New Exception("Material table is not initialized")
        End If

        Return Me.MatTable.MaterialUpToDate(SSDoc)
    End Function

    Public Function UpdateMaterial(SSDoc As HCStructuredStorageDoc) As Boolean
        If Me.MatTable Is Nothing Then
            Throw New Exception("Material table is not initialized")
        End If

        Return Me.MatTable.UpdateMaterial(SSDoc)
    End Function


    Public Function GetBlockLibraryBlockNames() As List(Of String)
        If Me.BlkLibrary Is Nothing Then
            Throw New Exception("Block library not initialized")
        End If
        Return Me.BlkLibrary.Items
    End Function

    Private Class PropertySets
        Public Property Items As List(Of PropertySet)
        Public Property OpenReadWrite As Boolean

        Private Property cf As CompoundFile
        Private Property FullName As String
        Private Property PropertySetNames As List(Of String)


        Public Sub New(
            _cf As CompoundFile,
            _FullName As String,
            _OpenReadWrite As Boolean)

            Me.cf = _cf
            Me.FullName = _FullName
            Me.OpenReadWrite = _OpenReadWrite

            Me.Items = New List(Of PropertySet)

            Me.PropertySetNames = New List(Of String)

            Dim tmpPropertySetNames As New List(Of String)

            tmpPropertySetNames.AddRange({"SummaryInformation", "DocumentSummaryInformation", "ExtendedSummaryInformation"})
            tmpPropertySetNames.AddRange({"ProjectInformation", "MechanicalModeling", "Custom"})

            Dim cs As CFStream = Nothing
            Dim co As OLEPropertiesContainer = Nothing

            For Each PropertySetName As String In tmpPropertySetNames
                If Not PropertySetName = "Custom" Then

                    'Not all files have every PropertySet
                    'Properties of some very old files appear to be organized differently
                    Try
                        Me.Items.Add(New PropertySet(Me.cf, PropertySetName, Me.OpenReadWrite))
                        Me.PropertySetNames.Add(PropertySetName)
                    Catch ex As Exception
                    End Try

                    If (PropertySetName = "DocumentSummaryInformation") And (PropertySetNames.Contains(PropertySetName)) Then
                        cs = GetItem("DocumentSummaryInformation").cs
                        co = GetItem("DocumentSummaryInformation").co
                    End If
                Else
                    ' The Custom PropertySet needs the same cs and co as DocumentSummaryInformation, not copies.
                    'If (cs IsNot Nothing) And (cs IsNot Nothing) Then
                    If (cs IsNot Nothing) And (co IsNot Nothing) Then
                        Me.Items.Add(New PropertySet(Me.cf, PropertySetName, Me.OpenReadWrite, cs, co))
                        PropertySetNames.Add(PropertySetName)
                    End If
                End If

            Next

        End Sub


        Public Function PrepOutput(InList As List(Of String), FullName As String) As List(Of String)
            Dim OutList As New List(Of String)
            Dim tmpList As List(Of String)
            Dim s As String

            OutList.AddRange(InList)

            For Each PS As PropertySet In Me.Items
                tmpList = PS.PrepOutput()
                For Each s In tmpList
                    If Not OutList.Contains(s) Then
                        OutList.Add(s)
                        If (Not s.Contains("Custom")) And (OutList.Count > 100) Then
                            OutList.Add(FullName)
                        End If

                    End If
                Next
            Next

            Return OutList
        End Function

        Public Function GetItem(PropertySetName As String) As PropertySet
            Dim idx As Integer = PropertySetNames.IndexOf(PropertySetName)
            If idx = -1 Then
                Return Nothing
            Else
                Return Items(idx)
            End If
        End Function

    End Class


    Private Class PropertySet
        Public Property Items As New List(Of Prop)
        Public Property Name As String
        Public Property OpenReadWrite As Boolean

        Private Property cf As CompoundFile
        Public Property cs As CFStream
        Public Property co As OLEPropertiesContainer
        Private Property PropNames As New List(Of String)
        Private Property PropertySetNameToStreamName As New Dictionary(Of String, String)


        Public Sub New(
            _cf As CompoundFile,
            PropertySetName As String,
            _OpenReadWrite As Boolean,
            Optional _cs As CFStream = Nothing,
            Optional _co As OLEPropertiesContainer = Nothing)

            Me.cf = _cf
            Me.Name = PropertySetName
            Me.OpenReadWrite = _OpenReadWrite

            Me.PropertySetNameToStreamName("SummaryInformation") = "SummaryInformation"
            Me.PropertySetNameToStreamName("DocumentSummaryInformation") = "DocumentSummaryInformation"
            Me.PropertySetNameToStreamName("ProjectInformation") = "Rfunnyd1AvtdbfkuIaamtae3Ie"
            Me.PropertySetNameToStreamName("ExtendedSummaryInformation") = "C3teagxwOttdbfkuIaamtae3Ie"
            Me.PropertySetNameToStreamName("MechanicalModeling") = "K4teagxwOttdbfkuIaamtae3Ie"
            Me.PropertySetNameToStreamName("Custom") = "DocumentSummaryInformation"

            Dim StreamName As String = Me.PropertySetNameToStreamName(PropertySetName)

            If Not Me.Name.ToLower = "custom" Then
                If Me.cf.RootStorage.ContainsStorage("Master") Then
                    Me.cs = Me.cf.RootStorage.GetStorage("Master").GetStream(StreamName)
                Else
                    Me.cs = Me.cf.RootStorage.GetStream(StreamName)
                End If
                Me.co = Me.cs.AsOLEPropertiesContainer
            Else
                Me.cs = _cs
                Me.co = _co
            End If

            Dim CorrectedName As String

            If PropertySetName.ToLower = "custom" Then
                If co.HasUserDefinedProperties Then
                    'For Each OLEProp As OLEProperty In co.UserDefinedProperties.Properties
                    '    CorrectedName = CorrectedOLEPropName(PropertySetName, OLEProp)
                    '    Me.PropNames.Add(CorrectedName)
                    '    Try
                    '        Me.Items.Add(New Prop(co, OLEProp, CorrectedName, OpenReadWrite))
                    '    Catch ex As Exception
                    '        Dim i = 0
                    '    End Try
                    'Next
                    For i As Integer = 0 To co.UserDefinedProperties.Properties.Count - 1
                        Dim OLEProp As OLEProperty = co.UserDefinedProperties.Properties(i)
                        CorrectedName = CorrectedOLEPropName(PropertySetName, OLEProp)
                        Me.PropNames.Add(CorrectedName)
                        Me.Items.Add(New Prop(co, OLEProp, CorrectedName, OpenReadWrite))
                    Next
                End If

            Else
                For Each OLEProp As OLEProperty In co.Properties
                    CorrectedName = CorrectedOLEPropName(PropertySetName, OLEProp)
                    Me.PropNames.Add(CorrectedName)
                    Me.Items.Add(New Prop(co, OLEProp, CorrectedName, OpenReadWrite))
                Next

            End If

        End Sub


        Private Function CorrectedOLEPropName(PropertySetName As String, OLEProp As OLEProperty) As String
            Dim CorrectedName As String = ""

            Select Case PropertySetName
                Case "SummaryInformation"
                    Select Case OLEProp.PropertyIdentifier
                        Case 8
                            CorrectedName = "Last Author"
                        Case 12
                            CorrectedName = "Origination Date"
                        Case 13
                            CorrectedName = "Last Save Date"
                        Case 19
                            CorrectedName = "Security"
                        Case Else
                            CorrectedName = OLEProp.PropertyName.Replace("PIDSI_", "")
                    End Select
                Case "DocumentSummaryInformation"
                    CorrectedName = OLEProp.PropertyName.Replace("PIDDSI_", "")
                Case "ExtendedSummaryInformation"
                    Select Case OLEProp.PropertyIdentifier
                        ' These properties have names like '0x00000019' in Structured Storage
                        Case 19
                            CorrectedName = "StatusChangeDate"
                        Case 20
                            CorrectedName = "FriendlyUserName"
                        Case 22
                            CorrectedName = "User Profile Name (created by)"
                        Case 23
                            CorrectedName = "User Profile Initials (created by)"
                        Case 24
                            CorrectedName = "User Profile Mailing address (created by)"
                        Case 25
                            CorrectedName = "User Profile Name (modified by)"
                        Case 26
                            CorrectedName = "User Profile Initials (modified by)"
                        Case 27
                            CorrectedName = "User Profile Mailing address (modified by)"
                        Case Else
                            CorrectedName = OLEProp.PropertyName
                    End Select
                Case "MechanicalModeling"
                    Select Case OLEProp.PropertyIdentifier
                        Case 3
                            CorrectedName = "Material"
                        Case 8
                            CorrectedName = "TubeBendRadius"
                        Case 9
                            CorrectedName = "TubeOuterDiameter"
                        Case 10
                            CorrectedName = "TubeMinimumFlatLength"
                        Case 11
                            CorrectedName = "TubeWallThickness"
                        Case 12
                            CorrectedName = "TubeFlatLength"
                        Case 13
                            CorrectedName = "TubeAreaInsideDiameter"
                        Case 14
                            CorrectedName = "TubeVolumeInsideDiameter"
                        Case 15
                            CorrectedName = "TubeEndTreatmentTypeEnd1"
                        Case 16
                            CorrectedName = "TubeEndTreatmentTypeEnd2"
                        Case 19
                            CorrectedName = "Sheet Metal Gage"
                        Case 20
                            CorrectedName = "Face Style"
                        Case 21
                            CorrectedName = "Fill Style"
                        Case 22
                            CorrectedName = "Virtual Style"
                        Case 23
                            CorrectedName = "Density"
                        Case 24
                            CorrectedName = "Coef. of Thermal Exp"
                        Case 25
                            CorrectedName = "Thermal Conductivity"
                        Case 26
                            CorrectedName = "Specific Heat"
                        Case 27
                            CorrectedName = "Modulus of Elasticity"
                        Case 28
                            CorrectedName = "Poisson's Ratio"
                        Case 29
                            CorrectedName = "Yield Stress"
                        Case 30
                            CorrectedName = "Ultimate Stress"
                        Case 31
                            CorrectedName = "Elongation"
                        Case 32
                            CorrectedName = "Bead Material"
                        Case Else
                            CorrectedName = OLEProp.PropertyName
                    End Select
                Case Else
                    CorrectedName = OLEProp.PropertyName
            End Select

            CorrectedName = CorrectedName.ToLower

            Return CorrectedName
        End Function

        Public Function PrepOutput() As List(Of String)
            Dim OutList As New List(Of String)
            Dim tmpList As List(Of String)
            Dim s As String

            For Each P As Prop In Me.Items
                tmpList = New List(Of String)
                tmpList.Add(Me.Name)
                tmpList = P.PrepOutput(tmpList)
                s = ""
                s = String.Join(",", tmpList)
                OutList.Add(s)
            Next

            Return OutList
        End Function

        Public Function GetItem(PropName As String) As Prop
            Dim idx As Integer = PropNames.IndexOf(PropName)
            If idx = -1 Then
                Return Nothing
            Else
                Return Items(idx)
            End If
        End Function

        Public Sub Save()
            Me.co.Save(Me.cs)
            'Me.cf.Commit()
        End Sub

        Public Function AddProp(PropertyNameEnglish As String, Value As Object) As Boolean
            Dim Success As Boolean = True

            If Not Me.OpenReadWrite Then Return False

            Dim OLEProp As OLEProperty = Nothing
            Dim UserProperties As OLEPropertiesContainer
            Dim NewPropertyId As UInteger

            PropertyNameEnglish = PropertyNameEnglish.ToLower

            If Value Is Nothing Then
                Value = " "
            End If

            Try
                UserProperties = Me.co.UserDefinedProperties

                If UserProperties.PropertyNames.Keys.Count = 0 Then
                    ' The first two PropertyIDs (0 and 1) are reserved.  (Saw a reference to this somewhere in the OpenMCDF source code.)
                    NewPropertyId = 2
                Else
                    ' Could possibly find an unused ID number.  Going with .Max() + 1 for now.
                    NewPropertyId = CType(UserProperties.PropertyNames.Keys.Max() + 1, UInteger)
                End If

                UserProperties.PropertyNames(NewPropertyId) = PropertyNameEnglish

                ' ###### TODO: Maybe don't assume string, but check the actual property type and proceed accordingly. ######
                ' Probably consult PropertiesData to find that information.
                OLEProp = UserProperties.NewProperty(VTPropertyType.VT_LPSTR, NewPropertyId)
                OLEProp.Value = Value.ToString

                UserProperties.AddProperty(OLEProp)

            Catch ex As Exception
                Success = False
            End Try

            If OLEProp Is Nothing Then
                Success = False
            End If

            If Success Then
                PropNames.Add(PropertyNameEnglish)
                Dim Prop = New Prop(co, OLEProp, PropertyNameEnglish, Me.OpenReadWrite)
                Items.Add(Prop)
            End If

            Return Success
        End Function

        Public Function DeleteProp(PropertyNameEnglish As String) As Boolean
            Dim Success As Boolean = True

            PropertyNameEnglish = PropertyNameEnglish.ToLower

            Dim Prop = GetItem(PropertyNameEnglish)

            If Prop IsNot Nothing Then
                Try
                    co.UserDefinedProperties.RemoveProperty(Prop.PropertyIdentifier)
                Catch ex As Exception
                    Success = False
                End Try
            End If

            If Success Then
                Dim idx As Integer = PropNames.IndexOf(PropertyNameEnglish)
                If Not idx = -1 Then
                    PropNames.RemoveAt(idx)
                    Items.RemoveAt(idx)
                End If

            End If

            Return Success
        End Function


    End Class

    Private Class Prop
        Public Property Name As String

        Public ReadOnly Property Value As Object
            Get
                Return Me.OLEProp.Value
            End Get
        End Property

        Public Property VTType As VTPropertyType
        Public Property PropertyIdentifier As UInteger
        Public Property OpenReadWrite As Boolean


        Private co As OLEPropertiesContainer
        Private Property OLEProp As OLEProperty


        Public Sub New(
            _co As OLEPropertiesContainer,
            _OLEProp As OLEProperty,
            CorrectedName As String,
            _OpenReadWrite As Boolean)

            Me.co = _co
            Me.OLEProp = _OLEProp
            Me.Name = CorrectedName
            Me.OpenReadWrite = _OpenReadWrite
            Me.PropertyIdentifier = Me.OLEProp.PropertyIdentifier
            Me.VTType = Me.OLEProp.VTType

            If Me.OpenReadWrite Then Me.SetValue(Me.OLEProp.Value)
        End Sub


        Public Function PrepOutput(InList As List(Of String)) As List(Of String)
            Dim tmpList As New List(Of String)
            Dim L = 20
            Dim v As String

            Try
                v = CStr(Me.Value)
                If Len(v) > L Then v = v.Substring(0, L)
            Catch ex As Exception
                v = "Value exception"
            End Try

            tmpList.AddRange({CStr(Me.PropertyIdentifier), Me.OLEProp.PropertyName, Me.Name})

            For Each s As String In tmpList
                InList.Add(s.Replace(",", "~"))
            Next

            Return InList
        End Function

        Private Function MaybeChangePropType(PropertyValue As Object) As Boolean
            Dim Success As Boolean = True

            Dim ToTypeName As String = Nothing
            Dim ChangeNeeded As Boolean = False
            Dim tmpName As String
            Dim tmpPropertyIdentifier As UInteger
            Dim UDP As OLEPropertiesContainer

            If Success Then
                If CStr(PropertyValue) = CInt(PropertyValue).ToString Then
                    ToTypeName = "int32"
                Else
                    ToTypeName = "double"
                End If

                If (Me.VTType = VTPropertyType.VT_I4 And ToTypeName = "double") Or (Me.VTType = VTPropertyType.VT_R8 And ToTypeName = "int32") Then
                    ChangeNeeded = True
                End If
            End If

            If Success And ChangeNeeded Then

                tmpName = Me.OLEProp.PropertyName
                tmpPropertyIdentifier = Me.OLEProp.PropertyIdentifier

                UDP = co.UserDefinedProperties

                Try
                    UDP.RemoveProperty(tmpPropertyIdentifier)
                    UDP.PropertyNames(tmpPropertyIdentifier) = tmpName

                    Select Case ToTypeName
                        Case "int32"
                            Me.OLEProp = UDP.NewProperty(VTPropertyType.VT_I4, tmpPropertyIdentifier)
                        Case "double"
                            Me.OLEProp = UDP.NewProperty(VTPropertyType.VT_R8, tmpPropertyIdentifier)
                    End Select

                    UDP.AddProperty(Me.OLEProp)

                    Me.VTType = Me.OLEProp.VTType

                Catch ex As Exception
                    Success = False
                End Try

            End If

            Return Success
        End Function

        Public Function SetValue(PropertyValue As Object) As Boolean

            ' VTPropertyType enum: https://github.com/ironfede/openmcdf/blob/master/OpenMcdf.Ole/VTPropertyType.cs

            Dim Success As Boolean = True
            Dim tf As Boolean

            ' EditProperties currently always passes in the new value as a string

            tf = Me.VTType = VTPropertyType.VT_I4
            tf = tf Or Me.VTType = VTPropertyType.VT_R8
            tf = tf And Me.co.HasUserDefinedProperties
            If tf Then
                Success = MaybeChangePropType(PropertyValue)
            End If

            If Success Then
                Select Case Me.OLEProp.VTType

                    Case = VTPropertyType.VT_BOOL
                        Try
                            OLEProp.Value = CBool(PropertyValue)
                        Catch ex As Exception
                            Success = False
                        End Try

                    Case = VTPropertyType.VT_I4
                        Try
                            OLEProp.Value = CInt(PropertyValue)
                        Catch ex As Exception
                            Success = False
                        End Try

                    Case = VTPropertyType.VT_LPSTR, VTPropertyType.VT_LPWSTR
                        Try
                            OLEProp.Value = PropertyValue.ToString
                        Catch ex As Exception
                            Success = False
                        End Try

                    Case = VTPropertyType.VT_FILETIME
                        Try
                            OLEProp.Value = CType(PropertyValue, DateTime)
                        Catch ex As Exception
                            Success = False
                        End Try

                    Case = VTPropertyType.VT_R8
                        Try
                            OLEProp.Value = CDbl(PropertyValue)
                        Catch ex As Exception
                            Success = False
                        End Try

                End Select
            End If

            Return Success
        End Function

    End Class


    Private Class LinkFullNames
        Public Property Items As New List(Of String)
        Public Property BadLinks As New List(Of String)

        Private Property cf As CompoundFile
        Private Property IsFOA As Boolean
        Private Property LinkManagementOrder As List(Of String)
        Private Property ContainingFileFullName As String


        Public Sub New(_cf As CompoundFile, _IsFOA As Boolean, _LinkManagementOrder As List(Of String), _ContainingFileFullName As String)
            Me.cf = _cf
            Me.IsFOA = _IsFOA
            Me.LinkManagementOrder = _LinkManagementOrder
            Me.ContainingFileFullName = _ContainingFileFullName

            GetFullNames()
        End Sub


        Private Sub GetFullNames()
            Dim RootStorages As New List(Of CFStorage)

            If Me.IsFOA Then
                Me.cf.RootStorage.VisitEntries(Sub(item) If item.IsStorage Then RootStorages.Add(CType(item, CFStorage)), recursive:=False)
            Else
                RootStorages.Add(Me.cf.RootStorage)
            End If

            For Each RootStorage As CFStorage In RootStorages
                Dim tmpList = ProcessRootStorage(RootStorage)
                For Each FullName As String In tmpList
                    If Not Me.Items.Contains(FullName, StringComparer.OrdinalIgnoreCase) Then
                        Me.Items.Add(FullName)
                    End If
                Next
            Next


        End Sub

        Private Function ProcessRootStorage(RootStorage As CFStorage) As List(Of String)
            Dim FullNames As New List(Of String)
            Dim AllStorages As New List(Of CFStorage)
            Dim JSiteStorages As New List(Of CFStorage)

            Dim DocType As String = IO.Path.GetExtension(Me.ContainingFileFullName).ToLower

            ' For assemblies, get all filenames in the Attachments stream.
            ' It is needed because some JSite* OLE streams have filenames not in the assembly.
            ' Ideally we could parse the Attachments stream directly, but its format is messy.

            Dim AttachmentNamesList As New List(Of String)
            If DocType = ".asm" Then
                Dim AllStreams As New List(Of CFStream)
                Dim Attachments As CFStream = Nothing
                RootStorage.VisitEntries(Sub(item) If item.IsStream Then AllStreams.Add(CType(item, CFStream)), recursive:=False)
                For Each AllStream As CFStream In AllStreams
                    If AllStream.Name = "Attachments" Then
                        Attachments = AllStream
                    End If
                Next
                Dim AttachmentsDict As Dictionary(Of String, String) = ExtractFilenamesFromByteArray(Attachments.GetData, IsAttachmentsStream:=True)
                For Each Key As String In AttachmentsDict.Keys
                    AttachmentNamesList.Add(Key)
                Next

            End If

            RootStorage.VisitEntries(Sub(item) If item.IsStorage Then AllStorages.Add(CType(item, CFStorage)), recursive:=False)

            For Each AllStorage As CFStorage In AllStorages
                If AllStorage.Name Like "JSite*" Then
                    JSiteStorages.Add(AllStorage)
                End If
            Next

            For Each JSiteStorage As CFStorage In JSiteStorages
                Dim JSiteStreams As New List(Of CFStream)
                Dim JSiteStreamNames As New List(Of String)

                JSiteStorage.VisitEntries(Sub(item) If item.IsStream Then JSiteStreams.Add(CType(item, CFStream)), recursive:=False)

                For Each JSiteStream As CFStream In JSiteStreams
                    JSiteStreamNames.Add(JSiteStream.Name)
                Next

                Dim OLEName = String.Format("{0}Ole", ChrW(1))

                If (JSiteStreamNames.Contains(OLEName)) And (JSiteStreamNames.Contains("JProperties")) Then
                    If CheckJProperties(JSiteStorage.GetStream("JProperties")) Then

                        Dim FullName As String = GetFullNameFromOLEStream(JSiteStorage.GetStream(OLEName), AttachmentNamesList, DocType)

                        If FullName IsNot Nothing Then
                            If Not FullNames.Contains(FullName, StringComparer.OrdinalIgnoreCase) Then
                                FullNames.Add(FullName)
                            End If
                        End If

                    End If

                End If
            Next

            Return FullNames
        End Function

        Private Function GetFullNameFromOLEStream(
            OLEStream As CFStream,
            AttachmentNamesList As List(Of String),
            DocType As String
            ) As String

            ' The OLE stream stores three filename formats, or none if the drawing view doesn't have a model link.

            ' ###### Needs changes to handle FOA and non-SE linked files ######

            ' EXAMPLE
            ' Index  000 001 ... 033 034 035 036 ... 145 146 147 148 149 150 ... 183 184 185 186 187 188 ... 413 414 415 416
            ' Indicator               <>                              <>          <    >                              <    >
            ' Byte    56  57      00  44  3a  5c ...  2e  70  61  72  00  ff ...  03  00  44  00  3a  00 ...  72  00  01  00
            ' Ascii    ?   ?       ?   D   :   \ ...   .   p   a   r   ?   ? ...   ?   ?   D   ?   :   ? ...   ?   ?   ?   ?  
            ' Var                    iS1                         iE1                     iS2                      iE2

            ' EXAMPLE (cont)
            ' Index   ... 434 435 436 437 438 439 440 441 442 ... 476 477 478 479 480 481
            ' Indicator    <>  <>(<- relative motion byte)                         <>
            ' Byte    ...  46  01  00  28  00  00  00  5c  44 ...  2e  70  61  72  00  ff
            ' Ascii   ...   ?   ?   ?   ?   ?   ?   ?   \   D ...   .   p   a   r   ?   ?
            ' Var                                     iS3                     iE3

            ' FIRST FILENAME: Full path filename, Ascii format, sometimes in DOS 8.3 format, Start index: 34
            ' SECOND FILENAME: Full path filename, Unicode format, normal format
            ' THIRD FILENAME: Relative path filename, Ascii format, need to convert to Unicode for possible unicode characters
            '                 Based on relative motion value, need to prepend '.\' or the correct number of '..\' to the filename

            Dim FullName As String = Nothing

            Dim DOSName As String = ""    ' FIRST FILENAME
            Dim ABSOLUTE As String = ""   ' SECOND FILENAME
            Dim RELATIVE As String = ""   ' THIRD FILENAME
            Dim CONTAINER As String = ""  ' Derived from ABSOLUTE.  Used to see if the link is in the container directory

            Dim ByteArray As Byte() = OLEStream.GetData

            Dim FilenamesDict = ExtractFilenamesFromByteArray(ByteArray, IsAttachmentsStream:=False)

            If FilenamesDict IsNot Nothing Then
                ABSOLUTE = FilenamesDict("ABSOLUTE")
                RELATIVE = FilenamesDict("RELATIVE")
                CONTAINER = FilenamesDict("CONTAINER")

                For Each Target As String In Me.LinkManagementOrder
                    Select Case Target.ToUpper
                        Case "ABSOLUTE"
                            If IO.File.Exists(ABSOLUTE) Then
                                FullName = ABSOLUTE
                                Exit For
                            End If
                        Case "RELATIVE"
                            If IO.File.Exists(RELATIVE) Then
                                FullName = RELATIVE
                                Exit For
                            End If
                        Case "CONTAINER"
                            If IO.File.Exists(CONTAINER) Then
                                FullName = CONTAINER
                                Exit For
                            End If
                    End Select
                Next

                ' If the filename is in the attachments list, add it to the BadLinks list.  Otherwise ignore.
                If (FullName Is Nothing) Then
                    Dim BadName As String = Nothing
                    For Each Target As String In Me.LinkManagementOrder
                        Select Case Target.ToUpper
                            Case "ABSOLUTE"
                                If DocType = ".asm" Then
                                    If AttachmentNamesList.Contains(ABSOLUTE) Then
                                        BadName = ABSOLUTE
                                        Exit For
                                    End If
                                Else
                                    BadName = ABSOLUTE
                                    Exit For
                                End If
                            Case "RELATIVE"
                                If DocType = ".asm" Then
                                    If AttachmentNamesList.Contains(CONTAINER) Then
                                        BadName = RELATIVE
                                        Exit For
                                    End If
                                Else
                                    BadName = RELATIVE
                                    Exit For
                                End If
                            Case "CONTAINER"
                                If DocType = ".asm" Then
                                    If AttachmentNamesList.Contains(CONTAINER) Then
                                        BadName = CONTAINER
                                        Exit For
                                    End If
                                Else
                                    BadName = CONTAINER
                                    Exit For
                                End If
                        End Select
                    Next
                    If BadName IsNot Nothing Then

                        FullName = BadName 'The Items list needs this for index references to work correctly

                        If Not Me.BadLinks.Contains(BadName, StringComparer.OrdinalIgnoreCase) Then
                            BadLinks.Add(BadName)
                        End If
                    End If
                End If

            End If

            Return FullName
        End Function

        Private Function ExtractFilenamesFromByteArray(ByteArray As Byte(), IsAttachmentsStream As Boolean) As Dictionary(Of String, String)
            ' Searches the OLE byte string for model file extensions (.par, .psm, .asm, .PAR, .PSM, .ASM)
            ' Returns a dictionary
            ' {
            ' "ABSOLUTE": Full path filename,
            ' "RELATIVE": Full path filename,
            ' "CONTAINER": Full path filename
            ' }
            ' Assumes the order is DOSNAME, ABSOLUTE, RELATIVE.  Ignores any matches after those.
            ' 
            ' The search starts at the end of the byte array.  If an extension is detected,
            ' it proceeds towards the start until a filename start indicator is reached.
            ' For Ascii strings the indicator is &H00.  For Unicode it is &H03 &H00
            '
            ' The IsAttachmentsStream flag simply populates the dictionary keys with
            ' all filenames (and other garbage) found in the Root/Attachments stream.

            Dim FilenamesDict As New Dictionary(Of String, String)
            Dim FilenamesList As New List(Of String)
            Dim Filename As String
            Dim StartIdxs As New List(Of Integer)
            Dim CurrentIdx As Integer

            Dim AsciiPatterns As New List(Of List(Of Byte))

            AsciiPatterns.Add(New List(Of Byte) From {&H2E, &H70, &H61, &H72}) '.par
            AsciiPatterns.Add(New List(Of Byte) From {&H2E, &H50, &H41, &H52}) '.PAR
            AsciiPatterns.Add(New List(Of Byte) From {&H2E, &H70, &H73, &H6D}) '.psm
            AsciiPatterns.Add(New List(Of Byte) From {&H2E, &H50, &H53, &H4D}) '.PSM
            AsciiPatterns.Add(New List(Of Byte) From {&H2E, &H61, &H73, &H6D}) '.asm
            AsciiPatterns.Add(New List(Of Byte) From {&H2E, &H41, &H53, &H4D}) '.ASM

            Dim AsciiStartIndicators As New List(Of Byte) From {&H0}
            Dim AsciiMatch As Boolean
            Dim AsciiEndIdx As Integer
            Dim AsciiStartIdx As Integer

            Dim UnicodePatterns As New List(Of List(Of Byte))

            For Each Pattern As List(Of Byte) In AsciiPatterns
                Dim L As New List(Of Byte)
                For Each B As Byte In Pattern
                    L.Add(B)
                    L.Add(&H0)
                Next
                UnicodePatterns.Add(L)
            Next

            Dim UnicodeStartIndicators As New List(Of Byte) From {&H3, &H0}
            Dim UnicodeStartIndicatorsAlt As New List(Of Byte) From {&H0, &H0}
            Dim UnicodeMatch As Boolean
            Dim UnicodeEndIdx As Integer
            Dim UnicodeStartIdx As Integer

            Dim RelativeMotionIdx As Integer
            Dim RelativeMotion As Integer

            CurrentIdx = ByteArray.Count - 1
            AsciiMatch = False
            UnicodeMatch = False

            While CurrentIdx > 30

                If (Not AsciiMatch) And (Not UnicodeMatch) Then

                    ' Checks to see if any of the patterns are present at the current idx

                    For Each Pattern In AsciiPatterns
                        AsciiMatch = True
                        For i = 0 To Pattern.Count - 1
                            If Not Pattern(Pattern.Count - 1 - i) = ByteArray(CurrentIdx - i) Then
                                AsciiMatch = False
                                Exit For
                            End If
                        Next

                        If AsciiMatch Then
                            AsciiEndIdx = CurrentIdx
                            Exit For
                        End If
                    Next

                    For Each Pattern In UnicodePatterns
                        UnicodeMatch = True
                        For i = 0 To Pattern.Count - 1
                            If Not Pattern(Pattern.Count - 1 - i) = ByteArray(CurrentIdx - i) Then
                                UnicodeMatch = False
                                Exit For
                            End If
                        Next

                        If UnicodeMatch Then
                            UnicodeEndIdx = CurrentIdx
                            Exit For
                        End If
                    Next

                End If

                If AsciiMatch Then

                    ' Checks to see if the stop indicator is present just ahead of the current index

                    Dim StartIndicatorMatch As Boolean = True
                    For i = 0 To AsciiStartIndicators.Count - 1
                        If Not AsciiStartIndicators(AsciiStartIndicators.Count - 1 - i) = ByteArray(CurrentIdx - 1 - i) Then
                            StartIndicatorMatch = False
                            Exit For
                        End If
                    Next

                    If StartIndicatorMatch Then
                        AsciiStartIdx = CurrentIdx
                        StartIdxs.Add(CurrentIdx)
                        AsciiMatch = False
                        Dim ByteList As New List(Of Byte)

                        For j = AsciiStartIdx To AsciiEndIdx
                            ByteList.Add(ByteArray(j))
                        Next

                        Filename = Encoding.Default.GetString(ByteList.ToArray)

                        ' Check for RELATIVE filename
                        If ByteArray(AsciiStartIdx - 7) = &H46 Then
                            RelativeMotionIdx = AsciiStartIdx - 6
                            RelativeMotion = CInt(ByteArray(RelativeMotionIdx))
                            Filename = ProcessRelativeFilename(Filename, RelativeMotion)
                        End If

                        If Filename IsNot Nothing Then
                            FilenamesList.Add(Filename)
                        End If

                    End If
                End If

                If UnicodeMatch Then

                    ' Checks to see if either stop indicator is present just ahead of the current index

                    Dim StartIndicatorMatch As Boolean = True
                    Dim StartIndicatorMatchAlt As Boolean = True

                    For i = 0 To UnicodeStartIndicators.Count - 1
                        If Not UnicodeStartIndicators(UnicodeStartIndicators.Count - 1 - i) = ByteArray(CurrentIdx - 1 - i) Then
                            StartIndicatorMatch = False
                            Exit For
                        End If
                    Next
                    For i = 0 To UnicodeStartIndicatorsAlt.Count - 1
                        If Not UnicodeStartIndicatorsAlt(UnicodeStartIndicatorsAlt.Count - 1 - i) = ByteArray(CurrentIdx - 1 - i) Then
                            StartIndicatorMatchAlt = False
                            Exit For
                        End If
                    Next

                    If StartIndicatorMatch Or StartIndicatorMatchAlt Then
                        UnicodeStartIdx = CurrentIdx
                        StartIdxs.Add(CurrentIdx)
                        UnicodeMatch = False
                        Dim ByteList As New List(Of Byte)
                        For j = UnicodeStartIdx To UnicodeEndIdx
                            ByteList.Add(ByteArray(j))
                        Next

                        Filename = System.Text.Encoding.Unicode.GetString(ByteList.ToArray)

                        ' Check for RELATIVE filename
                        If ByteArray(UnicodeStartIdx - 7) = &H46 Then
                            RelativeMotionIdx = UnicodeStartIdx - 6
                            RelativeMotion = CInt(ByteArray(RelativeMotionIdx))
                            Filename = ProcessRelativeFilename(Filename, RelativeMotion)
                        End If

                        If Filename IsNot Nothing Then
                            FilenamesList.Add(Filename)
                        End If

                    End If
                End If

                CurrentIdx -= 1

            End While

            If IsAttachmentsStream Then
                For Each Filename In FilenamesList
                    FilenamesDict(Filename) = Filename
                Next
                Return FilenamesDict
            End If

            If Not FilenamesList.Count >= 3 Then
                Return Nothing
            End If

            Dim ContainerFileDirectory = IO.Path.GetDirectoryName(Me.ContainingFileFullName)

            Dim ABSOLUTE = FilenamesList(FilenamesList.Count - 2)

            FilenamesDict("ABSOLUTE") = ABSOLUTE

            Dim CONTAINER = String.Format(".\{0}", IO.Path.GetFileName(ABSOLUTE))
            CONTAINER = Path.GetFullPath(Path.Combine(ContainerFileDirectory, CONTAINER))

            FilenamesDict("CONTAINER") = CONTAINER

            Dim RELATIVE = FilenamesList(FilenamesList.Count - 3)

            FilenamesDict("RELATIVE") = RELATIVE

            Return FilenamesDict

        End Function

        Private Function ProcessRelativeFilename(
            Filename As String,
            RelativeMotion As Integer
            ) As String

            If RelativeMotion = 0 Then
                ' Seems to indicate a full-path filename.  Return unmodified filename.
            Else
                'Strip off leading "\" if present
                If Filename(0) = "\" Then
                    Filename = Filename.Substring(1, Filename.Count - 1)
                End If

                'Create relative motion prefix eg. "..\..\"
                Dim Prefix As String = ""

                If RelativeMotion = 1 Then
                    Prefix = ".\"
                Else
                    For j = 0 To RelativeMotion - 2
                        Prefix = String.Format("{0}..\", Prefix)
                    Next
                End If

                Filename = String.Format("{0}{1}", Prefix, Filename)

                'Create full path from relative path
                Dim ContainerFileDirectory = IO.Path.GetDirectoryName(Me.ContainingFileFullName)

                Dim OLD = Filename
                Try
                    Filename = Path.GetFullPath(Path.Combine(ContainerFileDirectory, Filename))
                Catch ex As Exception
                    Filename = Nothing
                End Try

            End If


            Return Filename
        End Function

        Public Function FindIndicatorIdx(
        StartIdx As Integer,
        IndicatorArray As Byte(),
        ByteArray As Byte()
        ) As Integer

            ' Start index: iS2 = FindIndicatorIdx(StartIdx:= iE1 + 1, IndicatorList:= {"3", "0"}, ByteStringList:= ByteStringList) + 2

            Dim DefaultIdx = -1000000
            Dim IndicatorIdx As Integer = DefaultIdx
            Dim CurrentIdx As Integer = StartIdx
            Dim GotAMatch As Boolean = False

            While IndicatorIdx = DefaultIdx
                If CurrentIdx + IndicatorArray.Count > ByteArray.Count Then
                    Exit While
                End If
                For i = 0 To IndicatorArray.Count - 1
                    GotAMatch = ByteArray(CurrentIdx + i) = IndicatorArray(i)
                    If Not GotAMatch Then
                        Exit For
                    End If
                Next
                If GotAMatch Then
                    IndicatorIdx = CurrentIdx
                    Exit While
                End If
                CurrentIdx += 1
            End While

            Return IndicatorIdx
        End Function

        Private Function CheckJProperties(JPropertiesStream As CFStream) As Boolean

            ' Checks if the JProperties stream contains the following byte array
            ' 4F 4C 45 53 40 00 01 00

            ' The JProperties stream for Non-SE links appears to have different values

            Dim ValidStream As Boolean = True

            Dim ByteArray As Byte() = JPropertiesStream.GetData()

            Dim ValidArray As Byte() = {&H4F, &H4C, &H45, &H53, &H40, &H0, &H1, &H0}

            If Not ByteArray.Count = ValidArray.Count Then
                ValidStream = False
            End If

            If ValidStream Then
                ' Note the last ValidArray byte is not checked.  It appears to be some sort of file counter
                For i = 0 To ValidArray.Count - 2
                    If Not ByteArray(i) = ValidArray(i) Then
                        ValidStream = False
                        Exit For
                    End If
                Next
            End If

            Return ValidStream
        End Function

        Private Function FormatByteString(ByteArray As Byte(), AsChr As Boolean) As String
            ' Utility for investigating format of ByteArray.  Not used in production.

            Dim ByteList As New List(Of String)
            Dim CharList As New List(Of String)

            For Each B As Byte In ByteArray
                ByteList.Add($"{B:x2}")

                If CInt(B) < 32 Then ' Non-printing
                    CharList.Add(".")
                ElseIf CInt(B) > 127 Then ' Extended ASCII
                    CharList.Add("?")
                ElseIf CInt(B) = 44 Then ' period character
                    CharList.Add(";")
                Else
                    CharList.Add(System.Text.Encoding.ASCII.GetString({B}))
                End If
            Next

            Dim s As String = ""
            Dim _s As String = ""

            For Each B As Byte In ByteArray
                If Not AsChr Then
                    s = String.Format("{0},{1:x2}", s, B)
                Else
                    If CInt(B) < 32 Then ' Non-printing
                        _s = "."
                    ElseIf CInt(B) > 127 Then ' Extended ASCII
                        _s = "?"
                    ElseIf CInt(B) = 44 Then ' period character
                        _s = ";"
                    Else
                        _s = System.Text.Encoding.ASCII.GetString({B})
                    End If
                    s = String.Format("{0},{1}", s, _s)
                End If
            Next

            Return s

        End Function

    End Class


    Private Class MaterialTable
        Public Property Materials As List(Of Material)
        Public Property Gages As List(Of Gage)

        ' This is for material table files *.mat only


        Public Sub New(cf As CompoundFile)
            Dim ByteArray As Byte()
            Dim AllStreams As New List(Of CFStream)
            Dim MaterialStream As CFStream = Nothing
            Dim RawMaterialTable As String

            Materials = New List(Of Material)
            Gages = New List(Of Gage)

            cf.RootStorage.VisitEntries(Sub(item) If item.IsStream Then AllStreams.Add(CType(item, CFStream)), recursive:=False)

            For Each AllStream As CFStream In AllStreams
                If AllStream.Name = "MaterialDataEx" Then
                    MaterialStream = AllStream
                End If
            Next

            ByteArray = MaterialStream.GetData

            RawMaterialTable = System.Text.Encoding.Unicode.GetString(ByteArray)
            RawMaterialTable = RawMaterialTable.Substring(2, Len(RawMaterialTable) - 2)

            Dim XmlDoc As New XmlDocument
            XmlDoc.LoadXml(RawMaterialTable)

            TraverseNodes(XmlDoc)  'Populates Me.Materials and Me.Gages

        End Sub


        Public Function UpdateMaterial(SSDoc As HCStructuredStorageDoc) As Boolean
            Dim IsUpToDate As Boolean
            Dim Matl As String = Nothing
            Dim MatTableMatl As Material = Nothing
            Dim Proceed As Boolean = True
            Dim tf As Boolean

            If SSDoc.PropSets Is Nothing Then
                Throw New Exception(String.Format("Properties not initialized in '{0}'", IO.Path.GetFileName(SSDoc.FullName)))
            End If

            If MaterialUpToDate(SSDoc) Then
                Proceed = False
                IsUpToDate = True
            End If

            If Proceed Then
                Matl = CStr(SSDoc.GetPropValue("System", "Material"))

                MatTableMatl = GetMaterial(Matl)

                If MatTableMatl Is Nothing Then
                    Proceed = False
                    IsUpToDate = False
                End If
            End If

            If Proceed Then
                Try
                    tf = True

                    'tf = tf And SSDoc.SetPropValue("System", "Material", Matl, AddProperty:=False)
                    'tf = tf And SSDoc.SetPropValue("System", "Face Style", MatTableMatl.FaceStyle, AddProperty:=False)
                    'tf = tf And SSDoc.SetPropValue("System", "Fill Style", MatTableMatl.FillStyle, AddProperty:=False)
                    'tf = tf And SSDoc.SetPropValue("System", "Virtual Style", MatTableMatl.VirtualStyle, AddProperty:=False)

                    tf = tf And SSDoc.SetPropValue("System", "Coef. of Thermal Exp", MatTableMatl.CoefOfThermalExp, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Thermal Conductivity", MatTableMatl.ThermalConductivity, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Specific Heat", MatTableMatl.SpecificHeat, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Modulus of Elasticity", MatTableMatl.ModulusOfElasticity, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Poisson's Ratio", MatTableMatl.PoissonsRatio, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Yield Stress", MatTableMatl.YieldStress, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Ultimate Stress", MatTableMatl.UltimateStress, AddProperty:=False)
                    tf = tf And SSDoc.SetPropValue("System", "Elongation", MatTableMatl.Elongation, AddProperty:=False)

                    IsUpToDate = tf
                Catch ex As Exception
                    IsUpToDate = False
                End Try

            End If

            Return IsUpToDate
        End Function

        Public Function MaterialUpToDate(SSDoc As HCStructuredStorageDoc) As Boolean
            Dim IsUpToDate As Boolean
            Dim Matl As String
            Dim MatTableMatl As Material
            Dim tf As Boolean
            Dim Threshold As Double = 0.001

            If SSDoc.PropSets Is Nothing Then
                Throw New Exception(String.Format("Properties not initialized in '{0}'", IO.Path.GetFileName(SSDoc.FullName)))
            End If

            Matl = CStr(SSDoc.GetPropValue("System", "Material"))

            MatTableMatl = GetMaterial(Matl)

            If MatTableMatl Is Nothing Then
                IsUpToDate = False
            Else
                tf = MatTableMatl.FaceStyle = CStr(SSDoc.GetPropValue("System", "Face Style"))
                tf = tf And MatTableMatl.FillStyle = CStr(SSDoc.GetPropValue("System", "Fill Style"))
                'tf = tf And MatTableMatl.VirtualStyle = CStr(SSDoc.GetPropValue("System", "Virtual Style"))
                tf = tf And CloseEnough(MatTableMatl.CoefOfThermalExp, CDbl(SSDoc.GetPropValue("System", "Coef. of Thermal Exp")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.ThermalConductivity, CDbl(SSDoc.GetPropValue("System", "Thermal Conductivity")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.SpecificHeat, CDbl(SSDoc.GetPropValue("System", "Specific Heat")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.ModulusOfElasticity, CDbl(SSDoc.GetPropValue("System", "Modulus of Elasticity")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.PoissonsRatio, CDbl(SSDoc.GetPropValue("System", "Poisson's Ratio")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.YieldStress, CDbl(SSDoc.GetPropValue("System", "Yield Stress")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.UltimateStress, CDbl(SSDoc.GetPropValue("System", "Ultimate Stress")), Threshold)
                tf = tf And CloseEnough(MatTableMatl.Elongation, CDbl(SSDoc.GetPropValue("System", "Elongation")), Threshold)

                IsUpToDate = tf
            End If

            Return IsUpToDate
        End Function

        Public Function GetMaterial(Name As String) As Material
            Dim Matl As Material = Nothing

            For Each Item As Material In Me.Materials
                If Item.Name.ToLower = Name.ToLower Then
                    Matl = Item
                    Exit For
                End If
            Next

            Return Matl
        End Function

        Public Function GetGage(Name As String) As Gage
            Dim Gage As Gage = Nothing

            For Each Item As Gage In Me.Gages
                If Item.Name.ToLower = Name.ToLower Then
                    Gage = Item
                    Exit For
                End If
            Next

            Return Gage
        End Function


        Private Sub TraverseNodes(RootNode As Xml.XmlNode)
            For Each ChildNode As XmlElement In RootNode
                If ChildNode.Name.ToLower = "material" Then
                    Dim Matl As New Material
                    If ChildNode.HasAttributes Then
                        Matl.Name = ChildNode.Attributes(0).Value
                    End If
                    For Each PropertyNode As XmlElement In ChildNode.ChildNodes
                        If PropertyNode.HasAttributes Then
                            Dim Name As String = ""
                            Dim Value As String = ""
                            For Each Attribute As XmlAttribute In PropertyNode.Attributes
                                If Attribute.Name = "Name" Then
                                    Name = Attribute.Value
                                ElseIf Attribute.Name = "Value" Then
                                    Value = Attribute.Value
                                End If
                            Next
                            Select Case Name
                                Case "Face Style"
                                    Matl.FaceStyle = Value
                                Case "Fill Style"
                                    Matl.FillStyle = Value
                                Case "VSPlus Style"
                                    Matl.VirtualStyle = Value
                                Case "Coef. of Thermal Exp"
                                    Matl.CoefOfThermalExp = CDbl(Value)
                                Case "Thermal Conductivity"
                                    Matl.ThermalConductivity = CDbl(Value)
                                Case "Specific Heat"
                                    Matl.SpecificHeat = CDbl(Value)
                                Case "Modulus of Elasticity"
                                    Matl.ModulusOfElasticity = CDbl(Value)
                                Case "Poisson's Ratio"
                                    Matl.PoissonsRatio = CDbl(Value)
                                Case "Yield Stress"
                                    Matl.YieldStress = CDbl(Value)
                                Case "Ultimate Stress"
                                    Matl.UltimateStress = CDbl(Value)
                                Case "Elongation"
                                    Matl.Elongation = CDbl(Value)
                            End Select

                        End If
                    Next
                    Me.Materials.Add(Matl)

                ElseIf ChildNode.Name.ToLower = "psmgauge" Then
                    Dim Gage As New Gage
                    If ChildNode.HasAttributes Then
                        Gage.Name = ChildNode.Attributes(0).Value
                    End If
                    For Each PropertyNode As XmlElement In ChildNode.ChildNodes
                        If PropertyNode.HasAttributes Then
                            Dim Name As String = ""
                            Dim Value As String = ""
                            For Each Attribute As XmlAttribute In PropertyNode.Attributes
                                If Attribute.Name = "Name" Then
                                    Name = Attribute.Value
                                ElseIf Attribute.Name = "Value" Then
                                    Value = Attribute.Value
                                End If
                            Next
                            Select Case Name
                                Case "SMetal Thickness"
                                    Gage.Thickness = CDbl(Value)
                                Case "SMetal Bend Radius"
                                    Gage.BendRadius = CDbl(Value)
                                Case "SMetal Relief Width"
                                    Gage.ReliefWidth = CDbl(Value)
                                Case "SMetal Relief Length"
                                    Gage.ReliefLength = CDbl(Value)
                                Case "SMetal Neutral Factor"
                                    Gage.NeutralFactor = CDbl(Value)
                                Case "SMetal Bend Param Type"
                                    Gage.BendParamType = CDbl(Value)
                                Case "SMetal Bend Equation"
                                    Gage.BendEquation = Value
                            End Select

                        End If
                    Next
                    Me.Gages.Add(Gage)
                Else
                    If ChildNode.HasChildNodes Then
                        TraverseNodes(ChildNode)
                    End If
                End If
            Next
        End Sub

        Private Function CloseEnough(X As Double, Y As Double, Threshold As Double) As Boolean
            Dim Epsilon As Double = 0.000000001
            Dim Value As Double = Math.Abs((X - Y) / (X + Epsilon))
            Return (Value = 0) Or (1 - Threshold < Value)
        End Function


    End Class

    Private Class Material
        Public Property Name As String
        Public Property FaceStyle As String
        Public Property FillStyle As String
        Public Property VirtualStyle As String
        Public Property CoefOfThermalExp As Double
        Public Property ThermalConductivity As Double
        Public Property SpecificHeat As Double
        Public Property ModulusOfElasticity As Double
        Public Property PoissonsRatio As Double
        Public Property YieldStress As Double
        Public Property UltimateStress As Double
        Public Property Elongation As Double

    End Class

    Private Class Gage
        Public Property Name As String
        Public Property Thickness As Double
        Public Property BendRadius As Double
        Public Property ReliefWidth As Double
        Public Property ReliefLength As Double
        Public Property NeutralFactor As Double
        Public Property BendParamType As Double
        Public Property BendEquation As String

    End Class

    Private Class BlockLibrary
        Public Property Items As List(Of String)
        Private Property FullName As String

        Public Sub New(cf As CompoundFile, _FullName As String)
            Me.FullName = _FullName

            Dim ByteArray As Byte()
            Dim AllStreams As New List(Of CFStream)
            Dim BlockStream As CFStream = Nothing
            'Dim RawMaterialTable As String

            Me.Items = New List(Of String)

            cf.RootStorage.VisitEntries(Sub(item) If item.IsStream Then AllStreams.Add(CType(item, CFStream)), recursive:=False)

            For Each AllStream As CFStream In AllStreams
                If AllStream.Name = "JBlocks" Then
                    BlockStream = AllStream
                End If
            Next

            ByteArray = BlockStream.GetData

            'FormatByteString(ByteArray)

            Dim NamedBytes As List(Of List(Of Byte))
            NamedBytes = GetNamesBytes(ByteArray)

            If NamedBytes IsNot Nothing Then
                For Each NamedByteList As List(Of Byte) In NamedBytes
                    Dim s As String = System.Text.Encoding.Unicode.GetString(NamedByteList.ToArray)
                    Items.Add(s)
                Next
            End If

        End Sub

        Private Function GetNamesBytes(ByteArray As Byte()) As List(Of List(Of Byte))

            'BYTE STREAM FORMAT
            '
            'Strings are null-terminated (&H00) 2-byte Unicode.  MSB can be anything, LSB must be non zero.
            'The first letter of each string is aligned in the diagram to more easily see the pattern.
            '
            '                              \|/ Number of letters in string           \|/ Last letter offset = 2 * Number of letters + 4 + 1 
            '          Offset --->    0  1  2  3  4     6     8    10    12    14    16 \|/ Null terminator
            '           Value --->    Z  Z NZ  Z  Z  A NZ  A NZ  A NZ  A NZ  A NZ  A NZ  Z \|/ Number of block views
            '48 05 00 00 00 7f 17 00 00 00 06 00 00 00 42 00 6c 00 6f 00 63 00 6b 00 31 00 01
            ' H  .  .  .  .  .  .  .  .  .  .  .  .  .  B  .  l  .  o  .  c  .  k  .  1  .  .
            '                                        x                                x    /|\ This value was copied from the next row for reference
            '      01 00 00 00 7e 17 00 00 05 00 00 00 56 00 69 00 65 00 77 00 31 00   
            '       .  .  .  .  ~  .  .  .  .  .  .  .  V  .  i  .  e  .  w  .  1  .   
            '                                        x                          x    
            '               88 15 00 00 00 03 00 00 00 43 00 4f 00 47 00       
            '                ?  .  .  .  .  .  .  .  .  C  .  O  .  G  .       
            '                                        x              x        
            '      01 00 00 00 89 15 00 00 05 00 00 00 56 00 69 00 65 00 77 00 31 00   
            '       .  .  .  .  ?  .  .  .  .  .  .  .  V  .  i  .  e  .  w  .  1  .   
            '                                        x                          x    
            '               a3 16 00 00 00 04 00 00 00 73 00 6c 00 6f 00 74 00     
            '                ?  .  .  .  .  .  .  .  .  s  .  l  .  o  .  t  .     
            '                                        x                    x      
            ' Value legend
            '    A Anything
            '   NZ Non zero
            '    Z Zero

            Dim MaxNumChars As Integer = 30
            Dim MaxNumBlockViews As Integer = 2

            Dim ByteLists As New List(Of List(Of Byte))
            Dim C As Integer = ByteArray.Count
            Dim NumBlockViews As Integer = 0

            Dim SearchStart As Integer = CInt(ByteArray.Count / 2)

            For i = SearchStart To C - 1

                Dim ByteList As New List(Of Byte)

                ' Check for leading pattern Z Z NZ Z Z
                If i + 0 < C AndAlso Not ByteArray(i) = &H0 Then Continue For
                If i + 1 < C AndAlso Not ByteArray(i + 1) = &H0 Then Continue For
                If i + 2 < C AndAlso ByteArray(i + 2) = &H0 Then Continue For
                If i + 3 < C AndAlso Not ByteArray(i + 3) = &H0 Then Continue For
                If i + 4 < C AndAlso Not ByteArray(i + 4) = &H0 Then Continue For

                ' Check for null terminator
                If Not i + 2 < C Then Exit For

                Dim NumChars = CInt(ByteArray(i + 2))
                If NumChars > MaxNumChars Then Continue For
                Dim TerminatorIdx = i + 2 * NumChars + 4 + 1
                If TerminatorIdx < C AndAlso Not ByteArray(TerminatorIdx) = &H0 Then Continue For

                ' Check Unicode
                For j = i + 6 To i + 6 + 2 * (NumChars - 1) Step 2
                    If j < C AndAlso ByteArray(j) = &H0 Then  ' There may be other invalid hex values
                        Continue For
                    Else
                        ' These need to be in reverse order for the unicode translation to work properly
                        ByteList.Add(ByteArray(j))
                        ByteList.Add(ByteArray(j - 1))
                    End If
                Next

                If NumBlockViews = 0 Then
                    NumBlockViews = CInt(ByteArray(TerminatorIdx + 1))
                    If Not NumBlockViews <= MaxNumBlockViews Then
                        NumBlockViews = 0
                        Continue For
                    End If

                    ByteLists.Add(ByteList)
                Else
                    NumBlockViews -= 1
                End If

            Next

            'Dim View1ByteList As New List(Of Byte)
            'View1ByteList.AddRange({&H0, &H56, &H0, &H69, &H0, &H65, &H0, &H77, &H0, &H31})

            'Dim View1StartIdxList As New List(Of Integer)

            'Dim IndicatorNameStart As New List(Of Byte)
            'IndicatorNameStart.AddRange({&H1, &H0})  ' In reverse order

            'Dim IndicatorNameEnd As New List(Of Byte)
            'IndicatorNameEnd.AddRange({&H0, &H0})  ' In reverse order, kinda

            '' Forward pass to find start idx of each "View1"
            'For i = 0 To ByteArray.Count - View1ByteList.Count - 1
            '    Dim Matched As Boolean = True

            '    For j = 0 To View1ByteList.Count - 1
            '        If Not ByteArray(i + j) = View1ByteList(j) Then

            '            If j = View1ByteList.Count - 1 Then
            '                ' Found a View that is not View1
            '                MsgBox("Cannot currently process blocks with more than 1 view.  Exiting...", vbOKOnly)
            '                Return Nothing
            '            End If
            '            Matched = False
            '            Exit For
            '        End If
            '    Next

            '    If Matched Then View1StartIdxList.Add(i)

            'Next

            'If View1StartIdxList.Count = 0 Then Return Nothing


            'For i = View1StartIdxList.Count - 1 To 0 Step -1
            '    Dim idx = View1StartIdxList(i)

            '    idx -= 1

            '    Do Until ByteArray(idx) = IndicatorNameStart(0) And ByteArray(idx - 1) = IndicatorNameStart(1)
            '        idx -= 1

            '        If idx < 0 Then
            '            Return Nothing
            '        End If
            '    Loop

            '    idx -= 1

            '    Dim ReversedByteList As New List(Of Byte)

            '    Do Until ByteArray(idx) = IndicatorNameEnd(0) And ByteArray(idx - 1) = IndicatorNameEnd(1)

            '        ReversedByteList.Add(ByteArray(idx))
            '        idx -= 1

            '        If idx < 0 Then
            '            Return Nothing
            '        End If
            '    Loop

            '    Dim ByteList As New List(Of Byte)

            '    For j = ReversedByteList.Count - 1 To 0 Step -1
            '        ByteList.Add(ReversedByteList(j))
            '    Next

            '    ByteLists.Add(ByteList)

            'Next

            Return ByteLists

        End Function

        Private Sub FormatByteString(ByteArray As Byte())
            ' Utility for investigating format of ByteArray.  Not used in production.

            Dim ByteList As New List(Of String)
            Dim CharList As New List(Of String)

            For Each B As Byte In ByteArray
                ByteList.Add($"{B:x2}")

                If CInt(B) < 32 Then ' Non-printing
                    CharList.Add(".")
                ElseIf CInt(B) > 127 Then ' Extended ASCII
                    CharList.Add("?")
                ElseIf CInt(B) = 44 Then ' period character
                    CharList.Add(";")
                Else
                    CharList.Add(System.Text.Encoding.ASCII.GetString({B}))
                End If
            Next

        End Sub

    End Class

    Private Class VariableNames
        Public Property Items As List(Of String)
        Private Property FullName As String

        Public Sub New(cf As CompoundFile, _FullName As String)

            Me.FullName = _FullName

            Dim ByteArray As Byte()
            Dim AllStreams As New List(Of CFStream)
            Dim BlockStream As CFStream = Nothing
            'Dim RawMaterialTable As String

            Me.Items = New List(Of String)

            cf.RootStorage.VisitEntries(Sub(item) If item.IsStream Then AllStreams.Add(CType(item, CFStream)), recursive:=False)

            For Each AllStream As CFStream In AllStreams
                If AllStream.Name = "PartsLiteData" Then
                    BlockStream = AllStream
                End If
            Next

            ByteArray = BlockStream.GetData

            FormatByteString(ByteArray)

            'Dim NamedBytes As List(Of List(Of Byte))
            'NamedBytes = GetNamesBytes(ByteArray)

            'If NamedBytes IsNot Nothing Then
            '    For Each NamedByteList As List(Of Byte) In NamedBytes
            '        Dim s As String = System.Text.Encoding.Unicode.GetString(NamedByteList.ToArray)
            '        Items.Add(s)
            '    Next
            'End If

        End Sub

        Private Function GetNamesBytes(ByteArray As Byte()) As List(Of List(Of Byte))

            'BYTE STREAM FORMAT
            ' See C:\data\CAD\scripts\SolidEdgeHousekeeper\reference\20251003_structured_storage_variables\worksheet.ods

            Dim ByteLists As New List(Of List(Of Byte))
            Dim C As Integer

            C = ByteArray.Count

            Dim InBeginning As Boolean = True
            Dim BeginningDataLength As Integer = 44
            Dim InNamedViews As Boolean = False
            Dim NamedViewsDataLength As Integer = 133
            Dim InDefaultViews As Boolean = False
            Dim DefaultViewsDataLength As Integer = 17
            Dim InFeatures As Boolean = False
            Dim FeaturesDataLength As Integer = 53
            Dim InVariables As Boolean = False
            Dim VariablesDataLength As Integer = 21

            Dim NameCharCount As Integer
            Dim NameCharCountIdx As Integer
            Dim NameStartIdx As Integer
            Dim NameEndIdx As Integer

            Dim DescriptionCharCount As Integer
            Dim DescriptionCharCountIdx As Integer
            Dim DescriptionStartIdx As Integer
            Dim DescriptionEndIdx As Integer

            Dim CurrentIdx As Integer = 0

            Dim tf As Boolean

            While True
                Dim ByteList As New List(Of Byte)
                'Dim PeekAheadIdx As Integer

                If InBeginning Then
                    CurrentIdx += BeginningDataLength ' 0 -> 44
                    InBeginning = False
                    InNamedViews = True
                    'Continue While
                End If

                If InNamedViews Then
                    NameCharCountIdx = CurrentIdx ' Includes null terminator.  Unicode length will be 2 bytes shorter
                    NameCharCount = CInt(ByteArray(NameCharCountIdx))
                    NameStartIdx = NameCharCountIdx + 3
                    NameEndIdx = NameStartIdx + 2 * (NameCharCount - 1) - 1

                    DescriptionCharCountIdx = CurrentIdx + 2 + 2 * (NameCharCount - 1) + 2
                    DescriptionCharCount = CInt(ByteArray(DescriptionCharCountIdx))
                    DescriptionStartIdx = DescriptionCharCountIdx + 3
                    DescriptionEndIdx = DescriptionStartIdx + 2 * (DescriptionCharCount - 1) - 1

                    For j = NameStartIdx To NameEndIdx
                        ByteList.Add(ByteArray(j))
                    Next

                    For j = DescriptionStartIdx To DescriptionEndIdx
                        ByteList.Add(ByteArray(j))
                    Next

                    ByteLists.Add(ByteList)

                    CurrentIdx = DescriptionEndIdx + 3 + NamedViewsDataLength

                    ' Check if this is the last Named View

                    tf = ByteArray(CurrentIdx + 5) = &H0
                    tf = tf And ByteArray(CurrentIdx + 6) = &H0
                    tf = tf And ByteArray(CurrentIdx + 7) = &H0
                    If tf Then
                        CurrentIdx += 4
                        InNamedViews = False
                        InDefaultViews = True
                    End If

                End If

                If InDefaultViews Then
                    NameCharCountIdx = CurrentIdx ' Includes null terminator.  Unicode length will be 2 bytes shorter
                    NameCharCount = CInt(ByteArray(NameCharCountIdx))
                    NameStartIdx = NameCharCountIdx + 3
                    NameEndIdx = NameStartIdx + 2 * (NameCharCount - 1) - 1

                    ' Check if this is a default view
                    tf = ByteArray(NameEndIdx + 4) = &H21
                    tf = tf Or ByteArray(NameEndIdx + 4) = &H22
                    tf = tf Or ByteArray(NameEndIdx + 4) = &H23
                    If Not tf Then
                        InDefaultViews = False
                        InFeatures = True
                    Else
                        For j = NameCharCountIdx To NameEndIdx
                            ByteList.Add(ByteArray(j))
                        Next

                        ByteLists.Add(ByteList)

                        CurrentIdx = NameEndIdx + 3 + DefaultViewsDataLength
                    End If

                End If

                If InFeatures Then

                End If

            End While


            Return ByteLists

        End Function

        Private Sub FormatByteString(ByteArray As Byte())
            ' Utility for investigating format of ByteArray.  Not used in production.

            Dim ByteList As New List(Of String)
            Dim CharList As New List(Of String)

            Dim SaveDir As String = "C:\data\CAD\scripts\SolidEdgeHousekeeper\reference\20251003_structured_storage_variables"
            Dim TsvFilename As String = IO.Path.GetExtension(Me.FullName).Replace(".", "_")
            TsvFilename = $"{SaveDir}\PartsLiteStream{TsvFilename}.tsv"
            Dim Modelfilename = IO.Path.GetFileName(Me.FullName)

            Dim ByteString As String = Modelfilename
            Dim CharString As String = ""

            For Each B As Byte In ByteArray
                ByteList.Add($"{B:x2}")

                If CInt(B) < 32 Then ' Non-printing.  Includes TAB character.
                    CharList.Add(".")
                ElseIf CInt(B) > 127 Then ' Extended ASCII
                    CharList.Add("?")
                ElseIf CInt(B) = 44 Then ' period character
                    CharList.Add(";")
                ElseIf CInt(B) = 34 Then ' double quote
                    CharList.Add("'")
                Else
                    CharList.Add(System.Text.Encoding.ASCII.GetString({B}))
                End If

                ByteString = $"{ByteString}{Chr(9)}{ByteList(ByteList.Count - 1)}"
                CharString = $"{CharString}{Chr(9)}{CharList(CharList.Count - 1)}"
            Next

            Dim Outstring As String = $"{vbCrLf}{ByteString}{vbCrLf}{CharString}{vbCrLf}"
            System.IO.File.AppendAllText(TsvFilename, Outstring)

        End Sub

    End Class

End Class

