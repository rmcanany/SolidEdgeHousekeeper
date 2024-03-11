Option Strict On

Imports SolidEdgeCommunity

'
' CheckList order is not set here.  It is set in LabelToAction.vb.
'
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
                Case "OpenSave"
                    ErrorMessage = task.Proxy.OpenSave(SEDoc, Configuration, SEApp)
                Case "PartNumberDoesNotMatchFilename"
                    ErrorMessage = task.Proxy.PartNumberDoesNotMatchFilename(SEDoc, Configuration, SEApp)
                Case "ActivateAndUpdateAll"
                    ErrorMessage = task.Proxy.ActivateAndUpdateAll(SEDoc, Configuration, SEApp)
                Case "UpdateFaceAndViewStylesFromTemplate"
                    ErrorMessage = task.Proxy.UpdateFaceAndViewStylesFromTemplate(SEDoc, Configuration, SEApp)
                Case "RemoveFaceStyleOverrides"
                    ErrorMessage = task.Proxy.RemoveFaceStyleOverrides(SEDoc, Configuration, SEApp)
                Case "HideConstructions"
                    ErrorMessage = task.Proxy.HideConstructions(SEDoc, Configuration, SEApp)
                Case "FitPictorialView"
                    ErrorMessage = task.Proxy.FitPictorialView(SEDoc, Configuration, SEApp)
                Case "SaveAs"
                    ErrorMessage = task.Proxy.SaveAs(SEDoc, Configuration, SEApp)
                Case "InteractiveEdit"
                    ErrorMessage = task.Proxy.InteractiveEdit(SEDoc, Configuration, SEApp)
                Case "RunExternalProgram"
                    ErrorMessage = task.Proxy.RunExternalProgram(SEDoc, Configuration, SEApp)
                Case "MissingDrawing"
                    ErrorMessage = task.Proxy.MissingDrawing(SEDoc, Configuration, SEApp)
                Case "PropertyFindReplace"
                    ErrorMessage = task.Proxy.PropertyFindReplace(SEDoc, Configuration, SEApp)
                Case "VariablesEdit"
                    ErrorMessage = task.Proxy.VariablesEdit(SEDoc, Configuration, SEApp)
                Case "UpdatePhysicalProperties"
                    ErrorMessage = task.Proxy.UpdatePhysicalProperties(SEDoc, Configuration, SEApp)
                Case "CheckInterference"
                    ErrorMessage = task.Proxy.CheckInterference(SEDoc, Configuration, SEApp)
                    'Case "ConfigurationsOutOfDate"
                    '    ErrorMessage = task.Proxy.ConfigurationsOutOfDate(SEDoc, Configuration, SEApp)
                Case "ModelSizeToVariableTable"
                    ErrorMessage = task.Proxy.ModelSizeToVariableTable(SEDoc, Configuration, SEApp)
                Case "CheckRelationships"
                    ErrorMessage = task.Proxy.CheckRelationships(SEDoc, Configuration, SEApp)
                Case "CheckLinks"
                    ErrorMessage = task.Proxy.CheckLinks(SEDoc, Configuration, SEApp)
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
                Case "OpenSave"
                    ErrorMessage = task.Proxy.OpenSave(SEDoc, Configuration, SEApp)
                'Case "FailedOrWarnedFeatures"
                '    ErrorMessage = task.Proxy.FailedOrWarnedFeatures(SEDoc, Configuration, SEApp)
                'Case "SuppressedOrRolledBackFeatures"
                '    ErrorMessage = task.Proxy.SuppressedOrRolledBackFeatures(SEDoc, Configuration, SEApp)
                'Case "UnderconstrainedProfiles"
                '    ErrorMessage = task.Proxy.UnderconstrainedProfiles(SEDoc, Configuration, SEApp)
                Case "InsertPartCopiesOutOfDate"
                    ErrorMessage = task.Proxy.InsertPartCopiesOutOfDate(SEDoc, Configuration, SEApp)
                'Case "BrokenLinks"
                '    ErrorMessage = task.Proxy.BrokenLinks(SEDoc, Configuration, SEApp)
                'Case "LinksOutsideInputDirectory"
                '    ErrorMessage = task.Proxy.LinksOutsideInputDirectory(SEDoc, Configuration, SEApp)
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
                Case "HideConstructions"
                    ErrorMessage = task.Proxy.HideConstructions(SEDoc, Configuration, SEApp)
                Case "FitPictorialView"
                    ErrorMessage = task.Proxy.FitPictorialView(SEDoc, Configuration, SEApp)
                'Case "SaveAsSTEP"
                '    ErrorMessage = task.Proxy.SaveAsSTEP(SEDoc, Configuration, SEApp)
                Case "SaveAs"
                    ErrorMessage = task.Proxy.SaveAs(SEDoc, Configuration, SEApp)
                Case "InteractiveEdit"
                    ErrorMessage = task.Proxy.InteractiveEdit(SEDoc, Configuration, SEApp)
                Case "RunExternalProgram"
                    ErrorMessage = task.Proxy.RunExternalProgram(SEDoc, Configuration, SEApp)
                Case "MissingDrawing"
                    ErrorMessage = task.Proxy.MissingDrawing(SEDoc, Configuration, SEApp)
                Case "PropertyFindReplace"
                    ErrorMessage = task.Proxy.PropertyFindReplace(SEDoc, Configuration, SEApp)
                'Case "ExposeVariables"
                '    ErrorMessage = task.Proxy.ExposeVariables(SEDoc, Configuration, SEApp)
                Case "VariablesEdit"
                    ErrorMessage = task.Proxy.VariablesEdit(SEDoc, Configuration, SEApp)
                Case "UpdatePhysicalProperties"
                    ErrorMessage = task.Proxy.UpdatePhysicalProperties(SEDoc, Configuration, SEApp)
                Case "ModelSizeToVariableTable"
                    ErrorMessage = task.Proxy.ModelSizeToVariableTable(SEDoc, Configuration, SEApp)
                Case "CheckRelationships"
                    ErrorMessage = task.Proxy.CheckRelationships(SEDoc, Configuration, SEApp)
                Case "CheckLinks"
                    ErrorMessage = task.Proxy.CheckLinks(SEDoc, Configuration, SEApp)
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
                Case "OpenSave"
                    ErrorMessage = task.Proxy.OpenSave(SEDoc, Configuration, SEApp)
                Case "UpdateDesignForCost"
                    ErrorMessage = task.Proxy.UpdateDesignForCost(SEDoc, Configuration, SEApp)
                'Case "FailedOrWarnedFeatures"
                '    ErrorMessage = task.Proxy.FailedOrWarnedFeatures(SEDoc, Configuration, SEApp)
                'Case "SuppressedOrRolledBackFeatures"
                '    ErrorMessage = task.Proxy.SuppressedOrRolledBackFeatures(SEDoc, Configuration, SEApp)
                'Case "UnderconstrainedProfiles"
                '    ErrorMessage = task.Proxy.UnderconstrainedProfiles(SEDoc, Configuration, SEApp)
                Case "FlatPatternMissingOrOutOfDate"
                    ErrorMessage = task.Proxy.FlatPatternMissingOrOutOfDate(SEDoc, Configuration, SEApp)
                Case "InsertPartCopiesOutOfDate"
                    ErrorMessage = task.Proxy.InsertPartCopiesOutOfDate(SEDoc, Configuration, SEApp)
                'Case "BrokenLinks"
                '    ErrorMessage = task.Proxy.BrokenLinks(SEDoc, Configuration, SEApp)
                'Case "LinksOutsideInputDirectory"
                '    ErrorMessage = task.Proxy.LinksOutsideInputDirectory(SEDoc, Configuration, SEApp)
                Case "MaterialNotInMaterialTable"
                    ErrorMessage = task.Proxy.MaterialNotInMaterialTable(SEDoc, Configuration, SEApp)
                Case "PartNumberDoesNotMatchFilename"
                    ErrorMessage = task.Proxy.PartNumberDoesNotMatchFilename(SEDoc, Configuration, SEApp)
                'Case "GenerateLaserDXFAndPDF"
                '    ErrorMessage = task.Proxy.GenerateLaserDXFAndPDF(SEDoc, Configuration, SEApp)
                Case "UpdateInsertPartCopies"
                    ErrorMessage = task.Proxy.UpdateInsertPartCopies(SEDoc, Configuration, SEApp)
                Case "UpdateMaterialFromMaterialTable"
                    ErrorMessage = task.Proxy.UpdateMaterialFromMaterialTable(SEDoc, Configuration, SEApp)
                Case "UpdateFaceAndViewStylesFromTemplate"
                    ErrorMessage = task.Proxy.UpdateFaceAndViewStylesFromTemplate(SEDoc, Configuration, SEApp)
                Case "HideConstructions"
                    ErrorMessage = task.Proxy.HideConstructions(SEDoc, Configuration, SEApp)
                Case "FitPictorialView"
                    ErrorMessage = task.Proxy.FitPictorialView(SEDoc, Configuration, SEApp)
                'Case "SaveAsSTEP"
                '    ErrorMessage = task.Proxy.SaveAsSTEP(SEDoc, Configuration, SEApp)
                Case "SaveAs"
                    ErrorMessage = task.Proxy.SaveAs(SEDoc, Configuration, SEApp)
                'Case "SaveAsFlatDXF"
                '    ErrorMessage = task.Proxy.SaveAsFlatDXF(SEDoc, Configuration, SEApp)
                Case "InteractiveEdit"
                    ErrorMessage = task.Proxy.InteractiveEdit(SEDoc, Configuration, SEApp)
                Case "RunExternalProgram"
                    ErrorMessage = task.Proxy.RunExternalProgram(SEDoc, Configuration, SEApp)
                Case "MissingDrawing"
                    ErrorMessage = task.Proxy.MissingDrawing(SEDoc, Configuration, SEApp)
                Case "PropertyFindReplace"
                    ErrorMessage = task.Proxy.PropertyFindReplace(SEDoc, Configuration, SEApp)
                Case "VariablesEdit"
                    ErrorMessage = task.Proxy.VariablesEdit(SEDoc, Configuration, SEApp)
                Case "UpdatePhysicalProperties"
                    ErrorMessage = task.Proxy.UpdatePhysicalProperties(SEDoc, Configuration, SEApp)
                Case "ModelSizeToVariableTable"
                    ErrorMessage = task.Proxy.ModelSizeToVariableTable(SEDoc, Configuration, SEApp)
                Case "CheckRelationships"
                    ErrorMessage = task.Proxy.CheckRelationships(SEDoc, Configuration, SEApp)
                Case "CheckLinks"
                    ErrorMessage = task.Proxy.CheckLinks(SEDoc, Configuration, SEApp)
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
                Case "OpenSave"
                    ErrorMessage = task.Proxy.OpenSave(SEDoc, Configuration, SEApp)
                Case "PropertyFindReplace"
                    ErrorMessage = task.Proxy.PropertyFindReplace(SEDoc, Configuration, SEApp)
                'Case "BrokenLinks"
                '    ErrorMessage = task.Proxy.BrokenLinks(SEDoc, Configuration, SEApp)
                'Case "LinksOutsideInputDirectory"
                '    ErrorMessage = task.Proxy.LinksOutsideInputDirectory(SEDoc, Configuration, SEApp)
                Case "DrawingViewsOutOfDate"
                    ErrorMessage = task.Proxy.DrawingViewsOutOfDate(SEDoc, Configuration, SEApp)
                Case "DetachedDimensionsOrAnnotations"
                    ErrorMessage = task.Proxy.DetachedDimensionsOrAnnotations(SEDoc, Configuration, SEApp)
                Case "PartsListMissingOrOutOfDate"
                    ErrorMessage = task.Proxy.PartsListMissingOrOutOfDate(SEDoc, Configuration, SEApp)
                Case "PartNumberDoesNotMatchFilename"
                    ErrorMessage = task.Proxy.PartNumberDoesNotMatchFilename(SEDoc, Configuration, SEApp)
                Case "DrawingViewOnBackgroundSheet"
                    ErrorMessage = task.Proxy.DrawingViewOnBackgroundSheet(SEDoc, Configuration, SEApp)
                Case "UpdateDrawingViews"
                    ErrorMessage = task.Proxy.UpdateDrawingViews(SEDoc, Configuration, SEApp)
                'Case "MoveDrawingToNewTemplate"
                '    ErrorMessage = task.Proxy.MoveDrawingToNewTemplate(SEDoc, Configuration, SEApp)
                Case "UpdateStylesFromTemplate"
                    ErrorMessage = task.Proxy.UpdateStylesFromTemplate(SEDoc, Configuration, SEApp)
                Case "UpdateDrawingBorderFromTemplate"
                    ErrorMessage = task.Proxy.UpdateDrawingBorderFromTemplate(SEDoc, Configuration, SEApp)
                'Case "UpdateDimensionStylesFromTemplate"
                '    ErrorMessage = task.Proxy.UpdateDimensionStylesFromTemplate(SEDoc, Configuration, SEApp)
                Case "FitView"
                    ErrorMessage = task.Proxy.FitView(SEDoc, Configuration, SEApp)
                'Case "SaveAsPDF"
                '    ErrorMessage = task.Proxy.SaveAsPDF(SEDoc, Configuration, SEApp)
                'Case "SaveAsDXF"
                '    ErrorMessage = task.Proxy.SaveAsDXF(SEDoc, Configuration, SEApp)
                Case "SaveAs"
                    ErrorMessage = task.Proxy.SaveAs(SEDoc, Configuration, SEApp)
                Case "Print"
                    ErrorMessage = task.Proxy.Print(SEDoc, Configuration, SEApp)
                Case "InteractiveEdit"
                    ErrorMessage = task.Proxy.InteractiveEdit(SEDoc, Configuration, SEApp)
                Case "RunExternalProgram"
                    ErrorMessage = task.Proxy.RunExternalProgram(SEDoc, Configuration, SEApp)
                Case "CheckLinks"
                    ErrorMessage = task.Proxy.CheckLinks(SEDoc, Configuration, SEApp)
                Case Else
                    MsgBox("LaunchTask: Method not recognized: " + LabelToActionX(LabelText).TaskName + ".  Exiting...")
                    SEApp.Quit()
                    End
            End Select

        End Using

        Return ErrorMessage
    End Function

End Class
