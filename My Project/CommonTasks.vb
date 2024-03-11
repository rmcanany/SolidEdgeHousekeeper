Option Strict On

Public Class CommonTasks

    Public Shared tmpList As Collection

    Shared Function ActivateAndUpdateAll(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskActivateAndUpdateAll As New TaskActivateAndUpdateAll

        ErrorMessage = TaskActivateAndUpdateAll.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function CheckDrawings(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskCheckDrawings As New TaskCheckDrawings

        ErrorMessage = TaskCheckDrawings.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage

    End Function

    Shared Function CheckInterference(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskCheckInterference As New TaskCheckInterference

        ErrorMessage = TaskCheckInterference.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage

    End Function

    Shared Function CheckLinks(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskCheckLinks As New TaskCheckLinks
        TaskCheckLinks.CheckMissingLinks = True
        TaskCheckLinks.CheckMisplacedLinks = True

        ErrorMessage = TaskCheckLinks.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function CheckRelationships(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskCheckRelationships As New TaskCheckRelationships

        TaskCheckRelationships.CheckFailed = True
        TaskCheckRelationships.CheckUnderconstrained = True
        TaskCheckRelationships.CheckSuppressed = True

        ErrorMessage = TaskCheckRelationships.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage

    End Function

    Shared Function FitView(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        'Dim ErrorMessageList As New List(Of String)
        'Dim ExitStatus As Integer = 0

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskFitView As New TaskFitView

        ErrorMessage = TaskFitView.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage

    End Function

    Shared Function FlatPatternMissingOrOutOfDate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskFlatPatternMissingOrOutOfDate As New TaskCheckFlatPattern

        ErrorMessage = TaskFlatPatternMissingOrOutOfDate.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage

    End Function

    Shared Function HideConstructions(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskHideConstructions As New TaskHideConstructions

        ErrorMessage = TaskHideConstructions.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage

    End Function

    Shared Function InteractiveEdit(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskInteractiveEdit As New TaskEditInteractively

        ErrorMessage = TaskInteractiveEdit.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage

    End Function

    Shared Function MaterialNotInMaterialTable(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskMaterialNotInMaterialTable As New TaskCheckMaterialNotInMaterialTable

        ErrorMessage = TaskMaterialNotInMaterialTable.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage

    End Function

    Shared Function MissingDrawing(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskMissingDrawing As New TaskCheckMissingDrawing

        ErrorMessage = TaskMissingDrawing.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function ModelSizeToVariableTable(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        Configuration As Dictionary(Of String, String),
        SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskModelSizeToVariableTable As New TaskUpdateModelSizeInVariableTable

        ErrorMessage = TaskModelSizeToVariableTable.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function OpenSave(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        Configuration As Dictionary(Of String, String),
        SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskOpenSave As New TaskOpenSave

        ErrorMessage = TaskOpenSave.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function PartCopiesOutOfDate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskPartPartCopiesOutOfDate As New TaskCheckPartCopies

        ErrorMessage = TaskPartPartCopiesOutOfDate.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage

    End Function

    Shared Function PartNumberDoesNotMatchFilename(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskPartNumberDoesNotMatchFilename As New TaskCheckPartNumberDoesNotMatchFilename

        ErrorMessage = TaskPartNumberDoesNotMatchFilename.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function PartsListMissingOrOutOfDate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskPartsListMissingOrOutOfDate As New TaskCheckDrawingPartsList

        ErrorMessage = TaskPartsListMissingOrOutOfDate.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function Print(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskPrint As New TaskPrint

        ErrorMessage = TaskPrint.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function PropertyFindReplace(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskPropertyFindReplace As New TaskEditProperties

        ErrorMessage = TaskPropertyFindReplace.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function RemoveFaceStyleOverrides(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskRemoveFaceStyleOverrides As New TaskRemoveFaceStyleOverrides

        ErrorMessage = TaskRemoveFaceStyleOverrides.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function RunExternalProgram(
        SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        Configuration As Dictionary(Of String, String),
        SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskRunExternalProgram As New TaskRunExternalProgram

        ErrorMessage = TaskRunExternalProgram.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage

    End Function

    Shared Function SaveAs(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskSaveAs As New TaskSaveModelAs

        ErrorMessage = TaskSaveAs.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function UpdateDesignForCost(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskUpdateDesignForCost As New TaskUpdateDesignForCost

        ErrorMessage = TaskUpdateDesignForCost.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function UpdateDrawingViews(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskUpdateDrawingViews As New TaskUpdateDrawingViews

        ErrorMessage = TaskUpdateDrawingViews.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function UpdateFaceAndViewStylesFromTemplate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskUpdateFaceAndViewStylesFromTemplate As New TaskUpdateModelStylesFromTemplate

        ErrorMessage = TaskUpdateFaceAndViewStylesFromTemplate.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function UpdateMaterialFromMaterialTable(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskUpdateMaterialFromMaterialTable As New TaskUpdateMaterialFromMaterialTable

        ErrorMessage = TaskUpdateMaterialFromMaterialTable.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function UpdatePartCopies(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskUpdatePartCopies As New TaskUpdatePartCopies

        ErrorMessage = TaskUpdatePartCopies.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function UpdatePhysicalProperties(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskUpdatePhysicalProperties As New TaskUpdatePhysicalProperties

        ErrorMessage = TaskUpdatePhysicalProperties.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function UpdateStylesFromTemplate(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskUpdateStylesFromTemplate As New TaskUpdateDrawingStylesFromTemplate

        ErrorMessage = TaskUpdateStylesFromTemplate.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

    Shared Function VariablesEdit(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim TaskVariablesEdit As New TaskEditVariables

        ErrorMessage = TaskVariablesEdit.Process(SEDoc, Configuration, SEApp)

        Return ErrorMessage
    End Function

End Class
