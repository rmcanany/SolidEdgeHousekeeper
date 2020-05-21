Option Strict On

Imports SolidEdgeCommunity

Public Class LaunchTask

    Public Function Launch(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application,
        Filetype As String,
        LabelToActionX As LabelToAction,
        LabelText As String
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        If Filetype = "Assembly" Then
            ErrorMessageList = LaunchAssembly(SEDoc, Configuration, SEApp, LabelToActionX, LabelText)
        ElseIf Filetype = "Part" Then
            ErrorMessageList = LaunchPart(SEDoc, Configuration, SEApp, LabelToActionX, LabelText)
        ElseIf Filetype = "Sheetmetal" Then
            ErrorMessageList = LaunchSheetmetal(SEDoc, Configuration, SEApp, LabelToActionX, LabelText)
        ElseIf Filetype = "Draft" Then
            ErrorMessageList = LaunchDraft(SEDoc, Configuration, SEApp, LabelToActionX, LabelText)
        Else
            MsgBox("LaunchTask: Filetype not recognized: " + Filetype + ".  Exiting...")
            SEApp.Quit()
            End
        End If

        Return ErrorMessageList

    End Function

    Private Function LaunchAssembly(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application,
        ByVal LabelToActionX As LabelToAction,
        LabelText As String
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        Dim MethodName As String = ""

        For Each Item In LabelToActionX
            If Item.LabelText = LabelText Then
                MethodName = Item.MethodName
                Exit For
            End If
        Next

        Using task = New IsolatedTask(Of AssemblyTasks)()

            'MsgBox(LabelToActionX.L2A(LabelText)("MethodName").ToString)
            Select Case MethodName
                Case "OccurrenceMissingFiles"
                    ErrorMessageList = task.Proxy.OccurrenceMissingFiles(SEDoc, Configuration, SEApp)
                Case "OccurrenceOutsideProjectDirectory"
                    ErrorMessageList = task.Proxy.OccurrenceOutsideProjectDirectory(SEDoc, Configuration, SEApp)
                Case "FailedRelationships"
                    ErrorMessageList = task.Proxy.FailedRelationships(SEDoc, Configuration, SEApp)
                Case "UnderconstrainedRelationships"
                    ErrorMessageList = task.Proxy.UnderconstrainedRelationships(SEDoc, Configuration, SEApp)
                Case "PartNumberDoesNotMatchFilename"
                    ErrorMessageList = task.Proxy.PartNumberDoesNotMatchFilename(SEDoc, Configuration, SEApp)
                Case "ActivateAndUpdateAll"
                    ErrorMessageList = task.Proxy.ActivateAndUpdateAll(SEDoc, Configuration, SEApp)
                Case "UpdateFaceAndViewStylesFromTemplate"
                    ErrorMessageList = task.Proxy.UpdateFaceAndViewStylesFromTemplate(SEDoc, Configuration, SEApp)
                Case "FitIsometricView"
                    ErrorMessageList = task.Proxy.FitIsometricView(SEDoc, Configuration, SEApp)
                Case Else
                    MsgBox("LaunchTask: Method not recognized: " + MethodName + ".  Exiting...")
                    SEApp.Quit()
                    End
            End Select

        End Using

        Return ErrorMessageList

    End Function

    Private Function LaunchPart(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application,
        ByVal LabelToActionX As LabelToAction,
        LabelText As String
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        Dim MethodName As String = ""

        Dim msg As String = LabelText + Chr(13)
        For Each Item In LabelToActionX
            msg += Item.LabelText + ":" + Item.MethodName + Chr(13)
            If Item.LabelText = LabelText Then
                MethodName = Item.MethodName
                Exit For
            End If
        Next
        'MsgBox(msg)

        Using task = New IsolatedTask(Of PartTasks)()

            Select Case MethodName
                Case "FailedOrWarnedFeatures"
                    ErrorMessageList = task.Proxy.FailedOrWarnedFeatures(SEDoc, Configuration, SEApp)
                Case "SuppressedOrRolledBackFeatures"
                    ErrorMessageList = task.Proxy.SuppressedOrRolledBackFeatures(SEDoc, Configuration, SEApp)
                Case "UnderconstrainedProfiles"
                    ErrorMessageList = task.Proxy.UnderconstrainedProfiles(SEDoc, Configuration, SEApp)
                Case "InsertPartCopiesOutOfDate"
                    ErrorMessageList = task.Proxy.InsertPartCopiesOutOfDate(SEDoc, Configuration, SEApp)
                Case "MaterialNotInMaterialTable"
                    ErrorMessageList = task.Proxy.MaterialNotInMaterialTable(SEDoc, Configuration, SEApp)
                Case "PartNumberDoesNotMatchFilename"
                    ErrorMessageList = task.Proxy.PartNumberDoesNotMatchFilename(SEDoc, Configuration, SEApp)
                Case "UpdateInsertPartCopies"
                    ErrorMessageList = task.Proxy.UpdateInsertPartCopies(SEDoc, Configuration, SEApp)
                Case "UpdateMaterialFromMaterialTable"
                    ErrorMessageList = task.Proxy.UpdateMaterialFromMaterialTable(SEDoc, Configuration, SEApp)
                Case "UpdateFaceAndViewStylesFromTemplate"
                    ErrorMessageList = task.Proxy.UpdateFaceAndViewStylesFromTemplate(SEDoc, Configuration, SEApp)
                Case "FitIsometricView"
                    ErrorMessageList = task.Proxy.FitIsometricView(SEDoc, Configuration, SEApp)
                Case Else
                    MsgBox("LaunchTask: Method not recognized: " + MethodName + ".  Exiting...")
                    SEApp.Quit()
                    End
            End Select

        End Using

        Return ErrorMessageList

    End Function

    Private Function LaunchSheetmetal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application,
        ByVal LabelToActionX As LabelToAction,
        LabelText As String
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        Dim MethodName As String = ""

        For Each Item In LabelToActionX
            If Item.LabelText = LabelText Then
                MethodName = Item.MethodName
                Exit For
            End If
        Next

        Using task = New IsolatedTask(Of SheetmetalTasks)()

            Select Case MethodName
                Case "FailedOrWarnedFeatures"
                    ErrorMessageList = task.Proxy.FailedOrWarnedFeatures(SEDoc, Configuration, SEApp)
                Case "SuppressedOrRolledBackFeatures"
                    ErrorMessageList = task.Proxy.SuppressedOrRolledBackFeatures(SEDoc, Configuration, SEApp)
                Case "UnderconstrainedProfiles"
                    ErrorMessageList = task.Proxy.UnderconstrainedProfiles(SEDoc, Configuration, SEApp)
                Case "FlatPatternMissingOrOutOfDate"
                    ErrorMessageList = task.Proxy.FlatPatternMissingOrOutOfDate(SEDoc, Configuration, SEApp)
                Case "InsertPartCopiesOutOfDate"
                    ErrorMessageList = task.Proxy.InsertPartCopiesOutOfDate(SEDoc, Configuration, SEApp)
                Case "MaterialNotInMaterialTable"
                    ErrorMessageList = task.Proxy.MaterialNotInMaterialTable(SEDoc, Configuration, SEApp)
                Case "PartNumberDoesNotMatchFilename"
                    ErrorMessageList = task.Proxy.PartNumberDoesNotMatchFilename(SEDoc, Configuration, SEApp)
                Case "GenerateLaserDXFAndPDF"
                    ErrorMessageList = task.Proxy.GenerateLaserDXFAndPDF(SEDoc, Configuration, SEApp)
                Case "UpdateInsertPartCopies"
                    ErrorMessageList = task.Proxy.UpdateInsertPartCopies(SEDoc, Configuration, SEApp)
                Case "UpdateMaterialFromMaterialTable"
                    ErrorMessageList = task.Proxy.UpdateMaterialFromMaterialTable(SEDoc, Configuration, SEApp)
                Case "UpdateFaceAndViewStylesFromTemplate"
                    ErrorMessageList = task.Proxy.UpdateFaceAndViewStylesFromTemplate(SEDoc, Configuration, SEApp)
                Case "FitIsometricView"
                    ErrorMessageList = task.Proxy.FitIsometricView(SEDoc, Configuration, SEApp)
                Case Else
                    MsgBox("LaunchTask: Method not recognized: " + MethodName + ".  Exiting...")
                    SEApp.Quit()
                    End
            End Select

        End Using

        Return ErrorMessageList

    End Function

    Private Function LaunchDraft(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application,
        ByVal LabelToActionX As LabelToAction,
        LabelText As String
        ) As List(Of String)

        Dim ErrorMessageList As New List(Of String)

        Dim MethodName As String = ""

        For Each Item In LabelToActionX
            If Item.LabelText = LabelText Then
                MethodName = Item.MethodName
                Exit For
            End If
        Next

        Using task = New IsolatedTask(Of DraftTasks)()

            Select Case MethodName
                Case "DrawingViewsMissingFile"
                    ErrorMessageList = task.Proxy.DrawingViewsMissingFile(SEDoc, Configuration, SEApp)
                Case "DrawingViewsOutOfDate"
                    ErrorMessageList = task.Proxy.DrawingViewsOutOfDate(SEDoc, Configuration, SEApp)
                Case "DetachedDimensionsOrAnnotations"
                    ErrorMessageList = task.Proxy.DetachedDimensionsOrAnnotations(SEDoc, Configuration, SEApp)
                Case "FileNameDoesNotMatchModelFilename"
                    ErrorMessageList = task.Proxy.FileNameDoesNotMatchModelFilename(SEDoc, Configuration, SEApp)
                Case "UpdateDrawingViews"
                    ErrorMessageList = task.Proxy.UpdateDrawingViews(SEDoc, Configuration, SEApp)
                Case "UpdateDrawingBorderFromTemplate"
                    ErrorMessageList = task.Proxy.UpdateDrawingBorderFromTemplate(SEDoc, Configuration, SEApp)
                Case "UpdateDimensionStylesFromTemplate"
                    ErrorMessageList = task.Proxy.UpdateDimensionStylesFromTemplate(SEDoc, Configuration, SEApp)
                Case "FitView"
                    ErrorMessageList = task.Proxy.FitView(SEDoc, Configuration, SEApp)
                Case "SaveAsPDF"
                    ErrorMessageList = task.Proxy.SaveAsPDF(SEDoc, Configuration, SEApp)
                Case Else
                    MsgBox("LaunchTask: Method not recognized: " + MethodName + ".  Exiting...")
                    SEApp.Quit()
                    End
            End Select

        End Using

        Return ErrorMessageList
    End Function

End Class
