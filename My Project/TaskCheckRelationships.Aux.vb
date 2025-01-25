'Option Strict On

Partial Class TaskCheckRelationships

	Public Function GetProfilePlane(a As Object) As SolidEdgePart.RefPlane
		Return a.Plane
	End Function

	'Private Function GetSuppressedComponents(tmpSEDoc As SolidEdgeAssembly.AssemblyDocument) As List(Of Object)
	'	Dim SuppressedOccurrences As New List(Of Object)

	'	'Dim ComponentType As SolidEdgeAssembly.assemblycomponentTypeconstants.seAssemblyComponentTypeAll
	'	Dim ComponentCount As Long
	'	Dim SuppressedComponents() As Object = Nothing
	'	tmpSEDoc.GetSuppressedComponents(0, ComponentCount, SuppressedComponents)

	'	SuppressedOccurrences.AddRange(SuppressedComponents)

	'	Return SuppressedOccurrences
	'End Function

End Class
