'Option Strict On

Partial Class TaskCheckRelationships

	Public Function GetProfilePlane(a As Object) As SolidEdgePart.RefPlane
		Return a.Plane
	End Function

	Public Overrides Function Process(ByVal FileName As String) As Dictionary(Of Integer, List(Of String))

		Dim ErrorMessage As New Dictionary(Of Integer, List(Of String))

		Return ErrorMessage

	End Function

End Class
