Partial Class Form1
    Private Sub BuildListToActions()

        BuildListToActionsAssembly()
        BuildListToActionsPart()
        BuildListToActionsSheetmetal()
        BuildListToActionsDraft()

    End Sub

    Private Sub BuildListToActionsAssembly()
        Dim Check As New List(Of String)
        Dim Action As New List(Of SEOpAssembly)
        Dim Template As New List(Of Boolean)
        Dim AssemblyTasks As New AssemblyTasks()

        Check.Add("Occurrence missing file")
        Action.Add(AddressOf AssemblyTasks.AssemblyMissingFiles)
        Template.Add(False)

        Check.Add("Occurrence outside project directory")
        Action.Add(AddressOf AssemblyTasks.AssemblyOccurrenceOutsideProjectDirectory)
        Template.Add(False)

        Check.Add("Failed relationships")
        Action.Add(AddressOf AssemblyTasks.AssemblyFailedRelationships)
        Template.Add(False)

        Check.Add("Underconstrained relationships")
        Action.Add(AddressOf AssemblyTasks.AssemblyUnderconstrainedRelationships)
        Template.Add(False)

        Check.Add("Part number does not match file name")
        Action.Add(AddressOf AssemblyTasks.AssemblyPartNumberDoesNotMatchFilename)
        Template.Add(False)

        Check.Add("Update face and view styles from template")
        Action.Add(AddressOf AssemblyTasks.AssemblyUpdateFaceAndViewStylesFromTemplate)
        Template.Add(True)

        Check.Add("Fit isometric view")
        Action.Add(AddressOf AssemblyTasks.AssemblyFitIsometricView)
        Template.Add(False)

        CheckedListBoxAssembly.Items.Clear()

        For i As Integer = 0 To Check.Count - 1
            CheckedListBoxAssembly.Items.Add(Check(i))
            ListToActionAssembly.Add(Check(i), Action(i))
            ListToTemplateAssembly.Add(Check(i), Template(i))
            'ItemDictionary.Add(Check(i), New List(Of Object)({Action(i), Template(i)}))
        Next
    End Sub

    Private Sub BuildListToActionsPart()
        Dim Check As New List(Of String)
        Dim Action As New List(Of SEOpPart)
        Dim Template As New List(Of Boolean)
        Dim PartTasks As New PartTasks()

        Check.Add("Failed or warned features")
        Action.Add(AddressOf PartTasks.PartFailedOrWarnedFeatures)
        Template.Add(False)

        Check.Add("Suppressed or rolled back features")
        Action.Add(AddressOf PartTasks.PartSuppressedOrRolledBackFeatures)
        Template.Add(False)

        Check.Add("Underconstrained profiles")
        Action.Add(AddressOf PartTasks.PartUnderconstrainedProfiles)
        Template.Add(False)

        Check.Add("Material not in material table")
        Action.Add(AddressOf PartTasks.PartMaterialNotInMaterialTable)
        Template.Add(False)

        Check.Add("Part number does not match file name")
        Action.Add(AddressOf PartTasks.PartPartNumberDoesNotMatchFilename)
        Template.Add(False)

        Check.Add("Update face and view styles from template")
        Action.Add(AddressOf PartTasks.PartUpdateFaceAndViewStylesFromTemplate)
        Template.Add(True)

        Check.Add("Fit isometric view")
        Action.Add(AddressOf PartTasks.PartFitIsometricView)
        Template.Add(False)

        CheckedListBoxPart.Items.Clear()

        For i As Integer = 0 To Check.Count - 1
            CheckedListBoxPart.Items.Add(Check(i))
            ListToActionPart.Add(Check(i), Action(i))
            ListToTemplatePart.Add(Check(i), Template(i))
            'ItemDictionary.Add(Check(i), New List(Of Object)({Action(i), Template(i)}))
        Next
    End Sub

    Private Sub BuildListToActionsSheetmetal()
        Dim Check As New List(Of String)
        Dim Action As New List(Of SEOpSheetmetal)
        Dim Template As New List(Of Boolean)
        Dim SheetmetalTasks As New SheetmetalTasks()

        Check.Add("Failed or warned features")
        Action.Add(AddressOf SheetmetalTasks.SheetmetalFailedOrWarnedFeatures)
        Template.Add(False)

        Check.Add("Suppressed or rolled back features")
        Action.Add(AddressOf SheetmetalTasks.SheetmetalSuppressedOrRolledBackFeatures)
        Template.Add(False)

        Check.Add("Underconstrained profiles")
        Action.Add(AddressOf SheetmetalTasks.SheetmetalUnderconstrainedProfiles)
        Template.Add(False)

        Check.Add("Flat pattern missing or out of date")
        Action.Add(AddressOf SheetmetalTasks.SheetmetalFlatPatternMissingOrOutOfDate)
        Template.Add(False)

        Check.Add("Material not in material table")
        Action.Add(AddressOf SheetmetalTasks.SheetmetalMaterialNotInMaterialTable)
        Template.Add(False)

        Check.Add("Part number does not match file name")
        Action.Add(AddressOf SheetmetalTasks.SheetmetalPartNumberDoesNotMatchFilename)
        Template.Add(False)

        Check.Add("Generate Laser DXF and PDF")
        Action.Add(AddressOf SheetmetalTasks.SheetmetalGenerateLaserDXFAndPDF)
        Template.Add(False)

        Check.Add("Update face and view styles from template")
        Action.Add(AddressOf SheetmetalTasks.SheetmetalUpdateFaceAndViewStylesFromTemplate)
        Template.Add(True)

        Check.Add("Fit isometric view")
        Action.Add(AddressOf SheetmetalTasks.SheetmetalFitIsometricView)
        Template.Add(False)

        CheckedListBoxSheetmetal.Items.Clear()

        For i As Integer = 0 To Check.Count - 1
            CheckedListBoxSheetmetal.Items.Add(Check(i))
            ListToActionSheetmetal.Add(Check(i), Action(i))
            ListToTemplateSheetmetal.Add(Check(i), Template(i))
            'ItemDictionary.Add(Check(i), New List(Of Object)({Action(i), Template(i)}))
        Next
    End Sub

    Private Sub BuildListToActionsDraft()
        Dim Check As New List(Of String)
        Dim Action As New List(Of SEOpDraft)
        Dim Template As New List(Of Boolean)
        Dim DraftTasks As New DraftTasks()

        Check.Add("Drawing views missing file")
        Action.Add(AddressOf DraftTasks.DraftViewsMissingFiles)
        Template.Add(False)

        Check.Add("Drawing views out of date")
        Action.Add(AddressOf DraftTasks.DraftViewsOutOfDate)
        Template.Add(False)

        Check.Add("Detached dimensions or annotations")
        Action.Add(AddressOf DraftTasks.DraftDetachedDimensionsOrAnnotations)
        Template.Add(False)

        Check.Add("File name does not match model file name")
        Action.Add(AddressOf DraftTasks.DraftPartNumberDoesNotMatchFilename)
        Template.Add(False)

        Check.Add("Update drawing border from template")
        Action.Add(AddressOf DraftTasks.DraftUpdateBackgroundFromTemplate)
        Template.Add(True)

        'Check.Add("Update dimension styles from template")
        'Action.Add(AddressOf DraftUpdateDimensionStylesFromTemplate)
        'Template.Add(True)

        Check.Add("Fit view")
        Action.Add(AddressOf DraftTasks.DraftFitView)
        Template.Add(False)

        Check.Add("Save as PDF")
        Action.Add(AddressOf DraftTasks.DraftSaveAsPDF)
        Template.Add(False)

        CheckedListBoxDraft.Items.Clear()

        For i As Integer = 0 To Check.Count - 1
            CheckedListBoxDraft.Items.Add(Check(i))
            ListToActionDraft.Add(Check(i), Action(i))
            ListToTemplateDraft.Add(Check(i), Template(i))
        Next

    End Sub

    Private Sub PopulateCheckboxDefault(KVPair As String)
        'See format example in SaveDefaults

        Dim KeyAndName As String
        Dim Key As String
        Dim Value As String
        Dim CheckBoxName As String
        Dim ValueAsBoolean As Boolean
        Dim idx As Integer

        KeyAndName = KVPair.Split("=")(0)
        Key = KeyAndName.Split(".")(0)
        CheckBoxName = KeyAndName.Split(".")(1)
        Value = KVPair.Split("=")(1)

        If Value = "Checked" Then
            ValueAsBoolean = True
        Else
            ValueAsBoolean = False
        End If

        If Key = "Assembly" Then
            For idx = 0 To CheckedListBoxAssembly.Items.Count - 1
                If CheckedListBoxAssembly.Items(idx).ToString = CheckBoxName Then
                    CheckedListBoxAssembly.SetItemChecked(idx, ValueAsBoolean)
                End If
            Next
        End If
        If Key = "Part" Then
            For idx = 0 To CheckedListBoxPart.Items.Count - 1
                If CheckedListBoxPart.Items(idx).ToString = CheckBoxName Then
                    CheckedListBoxPart.SetItemChecked(idx, ValueAsBoolean)
                End If
            Next
        End If
        If Key = "Sheetmetal" Then
            For idx = 0 To CheckedListBoxSheetmetal.Items.Count - 1
                If CheckedListBoxSheetmetal.Items(idx).ToString = CheckBoxName Then
                    CheckedListBoxSheetmetal.SetItemChecked(idx, ValueAsBoolean)
                End If
            Next
        End If
        If Key = "Draft" Then
            For idx = 0 To CheckedListBoxDraft.Items.Count - 1
                If CheckedListBoxDraft.Items(idx).ToString = CheckBoxName Then
                    CheckedListBoxDraft.SetItemChecked(idx, ValueAsBoolean)
                End If
            Next
        End If
    End Sub

End Class