Option Strict On

Public Class TaskRecognizeHoles
    Inherits Task

    Public Sub New()
        Me.Name = Me.ToString.Replace("Housekeeper.", "")
        Me.Description = GenerateLabelText()
        Me.HelpText = GetHelpText()
        Me.RequiresSave = True
        Me.AppliesToAssembly = False
        Me.AppliesToPart = True
        Me.AppliesToSheetmetal = True
        Me.AppliesToDraft = False
        Me.HasOptions = False
        Me.HelpURL = GenerateHelpURL(Description)
        Me.Image = My.Resources.TaskOptimize
        Me.Category = "Edit"
        SetColorFromCategory(Me)

        GenerateTaskControl()

        ' Options

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

        Return ErrorMessage

    End Function
    Private Function ProcessInternal(
        ByVal SEDoc As SolidEdgeFramework.SolidEdgeDocument,
        ByVal Configuration As Dictionary(Of String, String),
        ByVal SEApp As SolidEdgeFramework.Application
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim SupplementalErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Proceed As Boolean = True

        Dim WasOrdered As Boolean = False

        Dim UC As New UtilsCommon
        Dim DocType = UC.GetDocType(SEDoc)

        If SEDoc.ReadOnly Then
            Proceed = False
            ExitStatus = 1
            ErrorMessageList.Add("Cannot save document marked 'Read Only'")
        End If

        Dim Models As SolidEdgePart.Models = Nothing
        Dim Model As SolidEdgePart.Model

        If Proceed Then
            Select Case DocType

                Case "par"
                    Dim tmpSEDoc As SolidEdgePart.PartDocument
                    tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)

                    Models = tmpSEDoc.Models
                    If Models.Count = 0 Then Proceed = False  ' Not an error, but nothing to do.
                    If Models.Count > 1 Then
                        Proceed = False
                        ExitStatus = 1
                        ErrorMessageList.Add("Cannot process files with more than one model")
                    End If

                    If Proceed Then
                        If tmpSEDoc.ModelingMode = SolidEdgePart.ModelingModeConstants.seModelingModeOrdered Then
                            WasOrdered = True
                        End If

                        'Determine if the first body is in Synchronous Mode
                        If WasOrdered Then
                            Try
                                tmpSEDoc.ModelingMode = SolidEdgePart.ModelingModeConstants.seModelingModeSynchronous
                            Catch ex As Exception
                                'The first body are in Ordered Mode, move to Synchronous is needed
                                SupplementalErrorMessage = MoveToSync(tmpSEDoc, Models.Item(1))
                                AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)
                            End Try
                        End If

                    End If

                Case "psm"
                    Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument
                    tmpSEDoc = CType(SEApp.ActiveDocument, SolidEdgePart.SheetMetalDocument)

                    Models = tmpSEDoc.Models
                    If Models.Count = 0 Then Proceed = False  ' Not an error, but nothing to do.
                    If Models.Count > 1 Then
                        Proceed = False
                        ExitStatus = 1
                        ErrorMessageList.Add("Cannot process files with more than one model")
                    End If

                    If Proceed Then
                        If tmpSEDoc.ModelingMode = SolidEdgePart.ModelingModeConstants.seModelingModeOrdered Then
                            WasOrdered = True
                        End If

                        'Determine if the first body is in Synchronous Mode
                        If WasOrdered Then
                            Try
                                tmpSEDoc.ModelingMode = SolidEdgePart.ModelingModeConstants.seModelingModeSynchronous
                            Catch ex As Exception
                                'The first body are in Ordered Mode, move to Synchronous is needed
                                SupplementalErrorMessage = MoveToSync(tmpSEDoc, Models.Item(1))
                                AddSupplementalErrorMessage(ExitStatus, ErrorMessageList, SupplementalErrorMessage)
                            End Try
                        End If

                    End If

            End Select

        End If

        If Proceed Then
            'Heal and optimize the body
            Model = Models.Item(1)
            Try
                Model.HealAndOptimizeBody(False, True)
                SEApp.DoIdle()
            Catch ex As Exception
                Proceed = False
                ExitStatus = 1
                ErrorMessageList.Add("Geometry optimization did not succeed")
            End Try

        End If

        If Proceed Then
            'Recognize holes
            Model = Models.Item(1)
            Dim numBodies As Integer = 1
            Dim Body As SolidEdgeGeometry.Body
            Body = CType(Model.Body, SolidEdgeGeometry.Body)
            Dim Bodies As Array
            Bodies = New SolidEdgeGeometry.Body(0) {Body}
            Dim numHoles As Integer = 1
            Dim RecognizedHoles As Array
            RecognizedHoles = New Object() {}
            Try
                Model.Holes.RecognizeAndCreateHoleGroups(numBodies, Bodies, numHoles, RecognizedHoles)
                SEApp.DoIdle()
                Model.Recompute()
            Catch ex As Exception
                ' Holes not found.  Not an error.
            End Try

        End If

        If Proceed And WasOrdered Then
            'Finish in Ordered Mode ready to work (This could be an option)
            Select Case DocType
                Case "par"
                    Dim tmpSEDoc As SolidEdgePart.PartDocument
                    tmpSEDoc = CType(SEDoc, SolidEdgePart.PartDocument)
                    tmpSEDoc.ModelingMode = SolidEdgePart.ModelingModeConstants.seModelingModeOrdered
                Case "psm"
                    Dim tmpSEDoc As SolidEdgePart.SheetMetalDocument
                    tmpSEDoc = CType(SEDoc, SolidEdgePart.SheetMetalDocument)
                    tmpSEDoc.ModelingMode = SolidEdgePart.ModelingModeConstants.seModelingModeOrdered
            End Select

        End If

        If Proceed Then
            'Save file
            SEDoc.Save()
            SEApp.DoIdle()
        End If

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function


    Private Function MoveToSync(
        ByRef tmpSEDoc As SolidEdgePart.PartDocument,
        ByRef Model As SolidEdgePart.Model
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Features As SolidEdgePart.Features = Nothing
        Dim Feature As Object = Nothing
        Features = Model.Features

        Dim bIgnoreWarnings As Boolean = True
        Dim bExtentSelection As Boolean = True
        Dim aErrorMessages As Array
        Dim aWarningMessages As Array
        Dim lNumberOfFeaturesCausingError As Integer
        Dim lNumberOfFeaturesCausingWarning As Integer

        For Each Feature In Features
            aErrorMessages = Array.CreateInstance(GetType(String), 0)
            aWarningMessages = Array.CreateInstance(GetType(String), 0)
            Dim dVolumeDifference As Double = 0
            'MoveToSynchronous in Part Mode have 8 arguments
            tmpSEDoc.MoveToSynchronous(Feature,
                                       bIgnoreWarnings,
                                       bExtentSelection,
                                       lNumberOfFeaturesCausingError,
                                       aErrorMessages,
                                       lNumberOfFeaturesCausingWarning,
                                       aWarningMessages,
                                       dVolumeDifference)

            If aErrorMessages.Length > 0 Then
                ExitStatus = 1
                For Each s As String In aErrorMessages
                    ErrorMessageList.Add(s)
                Next
            End If

        Next
        tmpSEDoc.ModelingMode = SolidEdgePart.ModelingModeConstants.seModelingModeSynchronous

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
    End Function

    Private Function MoveToSync(
        ByRef tmpSEDoc As SolidEdgePart.SheetMetalDocument,
        ByRef Model As SolidEdgePart.Model
        ) As Dictionary(Of Integer, List(Of String))

        Dim ErrorMessageList As New List(Of String)
        Dim ExitStatus As Integer = 0
        Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

        Dim Features As SolidEdgePart.Features = Nothing
        Dim Feature As Object = Nothing
        Features = Model.Features

        Dim bIgnoreWarnings As Boolean = True
        Dim bExtentSelection As Boolean = True
        Dim aErrorMessages As Array
        Dim aWarningMessages As Array
        Dim lNumberOfFeaturesCausingError As Integer
        Dim lNumberOfFeaturesCausingWarning As Integer

        For Each Feature In Features
            aErrorMessages = Array.CreateInstance(GetType(String), 0)
            aWarningMessages = Array.CreateInstance(GetType(String), 0)
            Dim dVolumeDifference As Double = 0
            'MoveToSynchronous in Part Mode have 8 arguments
            tmpSEDoc.MoveToSynchronous(Feature,
                                       bIgnoreWarnings,
                                       bExtentSelection,
                                       lNumberOfFeaturesCausingError,
                                       aErrorMessages,
                                       lNumberOfFeaturesCausingWarning,
                                       aWarningMessages)

            If aErrorMessages.Length > 0 Then
                ExitStatus = 1
                For Each s As String In aErrorMessages
                    ErrorMessageList.Add(s)
                Next
            End If

        Next
        tmpSEDoc.ModelingMode = SolidEdgePart.ModelingModeConstants.seModelingModeSynchronous

        ErrorMessage(ExitStatus) = ErrorMessageList
        Return ErrorMessage
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

        End If

        If ExitStatus > 0 Then  ' Start conditions not met.
            ErrorMessage(ExitStatus) = ErrorMessageList
            Return ErrorMessage
        Else
            Return PriorErrorMessage
        End If

    End Function


    Private Function GetHelpText() As String

        ' https://github.com/lrmoreno007/SolidEdge-OptimizarPlus

        'Macro for Solid Edge ST, the intention is to use it right after importing a model from STEP, 
        'IGES, SAT or any other format, to heal-optimize and recognize some features (only holes at 
        'this moment), both in part and sheet metal mode. It also move the model into synchronous 
        'mode, hides the coordinate system icon, hide the reference planes, make a zoom fit and ends 
        'in ordered mode.

        'Limitations:

        'It's intented to use it right after importing a model from STEP, IGES, SAT or any other format. 
        'Not for parts designed within SolidEdge, these have nothing to heal-optimize or recognize.
        'You must have only one document opened and one instance of SolidEdge running.
        'You can't use it when editing a part in a assembly.

        Dim HelpString As String
        HelpString = "Finds cylindrical cutouts in an imported model and converts them into hole features. "
        HelpString += "For the command to work correctly, the model must be in a freshly-imported state, "
        HelpString += "with no subsequent modifications performed in Solid Edge. "

        HelpString += vbCrLf + vbCrLf + "As the first step of the conversion process, the Optimize command is run on the imported geometry. "
        HelpString += "While not strictly necessary, it is considered good practice for any imported file. "

        HelpString += vbCrLf + vbCrLf + "The conversion is only possible in Synchronous mode. "
        HelpString += "Ordered files are switched to Sync before the conversion, then switched back. "
        HelpString += "Note, the imported body and the new hole features remain in Sync after the transition. "

        Return HelpString
    End Function

End Class

