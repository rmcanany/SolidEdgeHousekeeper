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
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        If Filetype = "Assembly" Then
            ErrorMessage = LaunchAssembly(SEDoc, Configuration, SEApp, LabelToActionX, LabelText)
        ElseIf Filetype = "Part" Then
            ErrorMessage = LaunchPart(SEDoc, Configuration, SEApp, LabelToActionX, LabelText)
        ElseIf Filetype = "Sheetmetal" Then
            ErrorMessage = LaunchSheetmetal(SEDoc, Configuration, SEApp, LabelToActionX, LabelText)
        ElseIf Filetype = "Draft" Then
            ErrorMessage = LaunchDraft(SEDoc, Configuration, SEApp, LabelToActionX, LabelText)
        Else
            MsgBox("LaunchTask: Filetype not recognized: " + Filetype + ".  Exiting...")
            SEApp.Quit()
            End
        End If

        Return ErrorMessage

    End Function

    Private Function LaunchAssembly(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application,
        ByVal LabelToActionX As LabelToAction,
        LabelText As String
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Using task = New IsolatedTask(Of AssemblyTasks)()

            Select Case LabelToActionX(LabelText).TaskName
                Case "OccurrenceMissingFiles"
                    ErrorMessage = task.Proxy.OccurrenceMissingFiles(SEDoc, Configuration, SEApp)
                Case "OccurrenceOutsideProjectDirectory"
                    ErrorMessage = task.Proxy.OccurrenceOutsideProjectDirectory(SEDoc, Configuration, SEApp)
                Case "FailedRelationships"
                    ErrorMessage = task.Proxy.FailedRelationships(SEDoc, Configuration, SEApp)
                Case "UnderconstrainedRelationships"
                    ErrorMessage = task.Proxy.UnderconstrainedRelationships(SEDoc, Configuration, SEApp)
                Case "PartNumberDoesNotMatchFilename"
                    ErrorMessage = task.Proxy.PartNumberDoesNotMatchFilename(SEDoc, Configuration, SEApp)
                Case "ActivateAndUpdateAll"
                    ErrorMessage = task.Proxy.ActivateAndUpdateAll(SEDoc, Configuration, SEApp)
                Case "UpdateFaceAndViewStylesFromTemplate"
                    ErrorMessage = task.Proxy.UpdateFaceAndViewStylesFromTemplate(SEDoc, Configuration, SEApp)
                Case "RemoveFaceStyleOverrides"
                    ErrorMessage = task.Proxy.RemoveFaceStyleOverrides(SEDoc, Configuration, SEApp)
                Case "FitIsometricView"
                    ErrorMessage = task.Proxy.FitIsometricView(SEDoc, Configuration, SEApp)
                Case "SaveAsSTEP"
                    ErrorMessage = task.Proxy.SaveAsSTEP(SEDoc, Configuration, SEApp)
                Case Else
                    MsgBox("LaunchTask: Method not recognized: " + LabelToActionX(LabelText).TaskName + ".  Exiting...")
                    SEApp.Quit()
                    End
            End Select

        End Using

        Return ErrorMessage

    End Function

    Private Function LaunchPart(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application,
        ByVal LabelToActionX As LabelToAction,
        LabelText As String
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Using task = New IsolatedTask(Of PartTasks)()

            Select Case LabelToActionX(LabelText).TaskName
                Case "FailedOrWarnedFeatures"
                    ErrorMessage = task.Proxy.FailedOrWarnedFeatures(SEDoc, Configuration, SEApp)
                Case "SuppressedOrRolledBackFeatures"
                    ErrorMessage = task.Proxy.SuppressedOrRolledBackFeatures(SEDoc, Configuration, SEApp)
                Case "UnderconstrainedProfiles"
                    ErrorMessage = task.Proxy.UnderconstrainedProfiles(SEDoc, Configuration, SEApp)
                Case "InsertPartCopiesOutOfDate"
                    ErrorMessage = task.Proxy.InsertPartCopiesOutOfDate(SEDoc, Configuration, SEApp)
                Case "MaterialNotInMaterialTable"
                    ErrorMessage = task.Proxy.MaterialNotInMaterialTable(SEDoc, Configuration, SEApp)
                Case "PartNumberDoesNotMatchFilename"
                    ErrorMessage = task.Proxy.PartNumberDoesNotMatchFilename(SEDoc, Configuration, SEApp)
                Case "UpdateInsertPartCopies"
                    ErrorMessage = task.Proxy.UpdateInsertPartCopies(SEDoc, Configuration, SEApp)
                Case "UpdateMaterialFromMaterialTable"
                    ErrorMessage = task.Proxy.UpdateMaterialFromMaterialTable(SEDoc, Configuration, SEApp)
                Case "UpdateFaceAndViewStylesFromTemplate"
                    ErrorMessage = task.Proxy.UpdateFaceAndViewStylesFromTemplate(SEDoc, Configuration, SEApp)
                Case "FitIsometricView"
                    ErrorMessage = task.Proxy.FitIsometricView(SEDoc, Configuration, SEApp)
                Case "SaveAsSTEP"
                    ErrorMessage = task.Proxy.SaveAsSTEP(SEDoc, Configuration, SEApp)
                Case Else
                    MsgBox("LaunchTask: Method not recognized: " + LabelToActionX(LabelText).TaskName + ".  Exiting...")
                    SEApp.Quit()
                    End
            End Select

        End Using

        Return ErrorMessage

    End Function

    Private Function LaunchSheetmetal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application,
        ByVal LabelToActionX As LabelToAction,
        LabelText As String
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Using task = New IsolatedTask(Of SheetmetalTasks)()

            Select Case LabelToActionX(LabelText).TaskName
                Case "FailedOrWarnedFeatures"
                    ErrorMessage = task.Proxy.FailedOrWarnedFeatures(SEDoc, Configuration, SEApp)
                Case "SuppressedOrRolledBackFeatures"
                    ErrorMessage = task.Proxy.SuppressedOrRolledBackFeatures(SEDoc, Configuration, SEApp)
                Case "UnderconstrainedProfiles"
                    ErrorMessage = task.Proxy.UnderconstrainedProfiles(SEDoc, Configuration, SEApp)
                Case "FlatPatternMissingOrOutOfDate"
                    ErrorMessage = task.Proxy.FlatPatternMissingOrOutOfDate(SEDoc, Configuration, SEApp)
                Case "InsertPartCopiesOutOfDate"
                    ErrorMessage = task.Proxy.InsertPartCopiesOutOfDate(SEDoc, Configuration, SEApp)
                Case "MaterialNotInMaterialTable"
                    ErrorMessage = task.Proxy.MaterialNotInMaterialTable(SEDoc, Configuration, SEApp)
                Case "PartNumberDoesNotMatchFilename"
                    ErrorMessage = task.Proxy.PartNumberDoesNotMatchFilename(SEDoc, Configuration, SEApp)
                Case "GenerateLaserDXFAndPDF"
                    ErrorMessage = task.Proxy.GenerateLaserDXFAndPDF(SEDoc, Configuration, SEApp)
                Case "UpdateInsertPartCopies"
                    ErrorMessage = task.Proxy.UpdateInsertPartCopies(SEDoc, Configuration, SEApp)
                Case "UpdateMaterialFromMaterialTable"
                    ErrorMessage = task.Proxy.UpdateMaterialFromMaterialTable(SEDoc, Configuration, SEApp)
                Case "UpdateFaceAndViewStylesFromTemplate"
                    ErrorMessage = task.Proxy.UpdateFaceAndViewStylesFromTemplate(SEDoc, Configuration, SEApp)
                Case "FitIsometricView"
                    ErrorMessage = task.Proxy.FitIsometricView(SEDoc, Configuration, SEApp)
                Case "SaveAsSTEP"
                    ErrorMessage = task.Proxy.SaveAsSTEP(SEDoc, Configuration, SEApp)
                Case Else
                    MsgBox("LaunchTask: Method not recognized: " + LabelToActionX(LabelText).TaskName + ".  Exiting...")
                    SEApp.Quit()
                    End
            End Select

        End Using

        Return ErrorMessage

    End Function

    Private Function LaunchDraft(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application,
        ByVal LabelToActionX As LabelToAction,
        LabelText As String
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Using task = New IsolatedTask(Of DraftTasks)()

            Select Case LabelToActionX(LabelText).TaskName
                Case "DrawingViewsMissingFile"
                    ErrorMessage = task.Proxy.DrawingViewsMissingFile(SEDoc, Configuration, SEApp)
                Case "DrawingViewsOutOfDate"
                    ErrorMessage = task.Proxy.DrawingViewsOutOfDate(SEDoc, Configuration, SEApp)
                Case "DetachedDimensionsOrAnnotations"
                    ErrorMessage = task.Proxy.DetachedDimensionsOrAnnotations(SEDoc, Configuration, SEApp)
                Case "FileNameDoesNotMatchModelFilename"
                    ErrorMessage = task.Proxy.FileNameDoesNotMatchModelFilename(SEDoc, Configuration, SEApp)
                Case "UpdateDrawingViews"
                    ErrorMessage = task.Proxy.UpdateDrawingViews(SEDoc, Configuration, SEApp)
                Case "UpdateDrawingBorderFromTemplate"
                    ErrorMessage = task.Proxy.UpdateDrawingBorderFromTemplate(SEDoc, Configuration, SEApp)
                Case "UpdateDimensionStylesFromTemplate"
                    ErrorMessage = task.Proxy.UpdateDimensionStylesFromTemplate(SEDoc, Configuration, SEApp)
                Case "FitView"
                    ErrorMessage = task.Proxy.FitView(SEDoc, Configuration, SEApp)
                Case "SaveAsPDF"
                    ErrorMessage = task.Proxy.SaveAsPDF(SEDoc, Configuration, SEApp)
                Case "SaveAsDXF"
                    ErrorMessage = task.Proxy.SaveAsDXF(SEDoc, Configuration, SEApp)
                Case Else
                    MsgBox("LaunchTask: Method not recognized: " + LabelToActionX(LabelText).TaskName + ".  Exiting...")
                    SEApp.Quit()
                    End
            End Select

        End Using

        Return ErrorMessage
    End Function

End Class
