Public Class OccurrenceGetter
    ' https://community.sw.siemens.com/s/question/0D54O000061xsxUSAQ/sorting-through-occurrences
    ' https://community.sw.siemens.com/s/question/0D54O00007OeUiYSAV/activate-all-parts-even-hidden-in-assembly-reaching-occsubocc-document-requires-activation

    Public AllOccurrenceNames As New List(Of String)
    Public AllSubOccurrenceNames As New List(Of String)

    Public AllOccurrences As New List(Of SolidEdgeAssembly.Occurrence)
    Public AllSubOccurrences As New List(Of SolidEdgeAssembly.SubOccurrence)

    Public Sub New(SEDoc As SolidEdgeAssembly.AssemblyDocument, Optional IncludeSubOccurrences As Boolean = True)
        Dim Occurrences As SolidEdgeAssembly.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence
        Dim SubOccurrences As SolidEdgeAssembly.SubOccurrences
        Dim SubOccurrence As SolidEdgeAssembly.SubOccurrence

        Occurrences = SEDoc.Occurrences

        For Each Occurrence In Occurrences
            AllOccurrenceNames.Add(Occurrence.Name)
            AllOccurrences.Add(Occurrence)
        Next

        For Each Occurrence In AllOccurrences
            If Occurrence.Subassembly Then
                SubOccurrences = Occurrence.SubOccurrences
                For Each SubOccurrence In SubOccurrences
                    RecurseSubs(SubOccurrence)

                Next
            End If
        Next
    End Sub

    Private Sub RecurseSubs(SubOccurrence As SolidEdgeAssembly.SubOccurrence)
        Dim SubSubs As SolidEdgeAssembly.SubOccurrences
        Dim SubSub As SolidEdgeAssembly.SubOccurrence

        If Not AllSubOccurrences.Contains(SubOccurrence) Then
            AllSubOccurrences.Add(SubOccurrence)
            AllSubOccurrenceNames.Add(SubOccurrence.Name)
            If SubOccurrence.Subassembly Then
                SubSubs = SubOccurrence.SubOccurrences
                For Each SubSub In SubSubs
                    RecurseSubs(SubSub)
                Next
            End If
        End If
    End Sub

    'Sub Recursive(oOcc As SolidEdgeAssembly.Occurrence)
    '    ' oOcc.Activate = True
    '    If Not AllOccurrences.Contains(oOcc) Then
    '        AllOccurrenceNames.Add(oOcc.Name)
    '        AllOccurrences.Add(oOcc)
    '    End If

    'End Sub

    'Private Sub PopulateAllSubOccurrences()
    '    Dim Occurrence As SolidEdgeAssembly.Occurrence
    '    Dim SubOccurrences As SolidEdgeAssembly.SubOccurrences
    '    Dim SubOccurrence As SolidEdgeAssembly.SubOccurrence

    '    For Each Occurrence In AllOccurrences
    '        SubOccurrences = Occurrence.SubOccurrences
    '        For Each SubOccurrence In SubOccurrences
    '            RecurseSubOccurrence(SubOccurrence)
    '        Next
    '    Next

    'End Sub

    Private Sub RecurseSubOccurrence(SubOccurrence As SolidEdgeAssembly.SubOccurrence)
        If Not AllSubOccurrences.Contains(SubOccurrence) Then
            AllSubOccurrences.Add(SubOccurrence)

        End If
    End Sub

    'Private Sub PopulateAllOccurrences(SEDoc As SolidEdgeAssembly.AssemblyDocument)
    '    Dim Occurrences As SolidEdgeAssembly.Occurrences
    '    Dim Occurrence As SolidEdgeAssembly.Occurrence

    '    Occurrences = SEDoc.Occurrences

    '    For Each Occurrence In Occurrences
    '        AllOccurrenceNames.Add(Occurrence.Name)
    '        AllOccurrences.Add(Occurrence)
    '    Next

    'End Sub


End Class

