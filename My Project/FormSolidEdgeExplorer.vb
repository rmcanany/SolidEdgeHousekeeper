Option Strict On

'Imports System.IO
'Imports System.Text
'Imports SolidEdgeExplorerDLL
'Imports SolidEdgeFramework

Public Class FormSolidEdgeExplorer

    Public FileName As String

    Public Property PropertiesData As HCPropertiesData

    Public Sub UpdateTree()

        Dim ext As String = IO.Path.GetExtension(FileName).ToLower()

        If ext = ".par" OrElse ext = ".psm" OrElse ext = ".asm" Then

            Try
                Dim tmpPartsLiteData As New SolidEdgeExplorerDLL.PartsLiteData
                tmpPartsLiteData.FindData(FileName)

                Dim tmpVariableInfos As New SolidEdgeExplorerDLL.CustomPropertyVariableInfo
                tmpVariableInfos.FindData(FileName)

                Dim tmpProperties As New SolidEdgeExplorerDLL.Properties
                tmpProperties.FindData(FileName)

                Dim tmpPSMCluster0 As New SolidEdgeExplorerDLL.PSMCluster0
                tmpPSMCluster0.FindData(FileName)

                SolidEdgeExplorerDLL.Utilities.PopulateVariableUserValues(tmpPartsLiteData, tmpPSMCluster0)

                PopulateTreeView(FileName, tmpPartsLiteData, tmpVariableInfos, tmpProperties, tmpPSMCluster0)

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If

        If ext = ".dft" Then
            Try

                Dim tmpVariableInfos As New SolidEdgeExplorerDLL.CustomPropertyVariableInfo
                tmpVariableInfos.FindData(FileName)

                Dim tmpProperties As New SolidEdgeExplorerDLL.Properties
                tmpProperties.FindData(FileName)

                Dim tmpPSMCluster0 As New SolidEdgeExplorerDLL.PSMCluster0
                tmpPSMCluster0.FindData(FileName)

                PopulateTreeView(FileName, Nothing, tmpVariableInfos, tmpProperties, tmpPSMCluster0)

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub

    Private Sub PopulateTreeView(
        File As String,
        PartsLiteData As SolidEdgeExplorerDLL.PartsLiteData,
        CustomPropertyVariableInfos As SolidEdgeExplorerDLL.CustomPropertyVariableInfo,
        Properties As SolidEdgeExplorerDLL.Properties,
        PSMCluster0 As SolidEdgeExplorerDLL.PSMCluster0)

        TreeView1.BeginUpdate()

        TreeView1.Nodes.Clear() ' Puliamo il TreeView

        Dim FileNode As New TreeNode("File: " & File)
        TreeView1.Nodes.Add(FileNode)

        Dim PropertiesNode As New TreeNode("Properties")
        For Each PropertySet In Properties.PropertySets
            Dim tmpPropertySetNode As New TreeNode(PropertySet.Name)
            For Each item In PropertySet.Items
                Dim tmpItemNode As TreeNode = Nothing
                Dim tmpPropertyData As PropertyData = Nothing

                Try
                    tmpPropertyData = Me.PropertiesData.GetPropertyData(PropertySet.Name, CInt(item.OLEProp.PropertyIdentifier))
                Catch ex As Exception
                End Try

                If Me.PropertiesData Is Nothing Or tmpPropertyData Is Nothing Then
                    tmpItemNode = New TreeNode(item.Name & " = " & item.Value.ToString)
                Else
                    tmpItemNode = New TreeNode(tmpPropertyData.Name & " = " & item.Value.ToString)
                End If

                If tmpItemNode IsNot Nothing Then tmpPropertySetNode.Nodes.Add(tmpItemNode)
            Next
            PropertiesNode.Nodes.Add(tmpPropertySetNode)
        Next
        TreeView1.Nodes.Add(PropertiesNode)


        If Not IsNothing(PartsLiteData) Then

            Dim featuresNode As New TreeNode("Features (" & PartsLiteData.Features.Count.ToString & "/" & PartsLiteData.ExpectedNumFeatures.ToString & ")")
            For Each feature As SolidEdgeExplorerDLL.Feature In PartsLiteData.Features
                ' Nodo principale con il nome della feature
                Dim featureNode As New TreeNode(feature.Name)

                ' Aggiungiamo le proprietà come sotto-nodi
                featureNode.Nodes.Add("ID: " & feature.Metadata1.ToString())
                featureNode.Nodes.Add("Feature Type: " & feature.Metadata2.ToString())
                If feature.Length > 2 Then featureNode.Nodes.Add("Metadata 3: " & feature.Metadata3.ToString())
                If feature.Length > 3 Then featureNode.Nodes.Add("Metadata 4: " & feature.Metadata4.ToString())
                If feature.Length > 4 Then featureNode.Nodes.Add("Metadata 5: " & feature.Metadata5.ToString())
                If feature.Length > 5 Then featureNode.Nodes.Add("Metadata 6: " & feature.Metadata6.ToString())
                If feature.Length > 6 Then featureNode.Nodes.Add("Metadata 7: " & feature.Metadata7.ToString())

                ' Aggiungiamo il nodo principale al TreeView
                featuresNode.Nodes.Add(featureNode)
            Next
            TreeView1.Nodes.Add(featuresNode)


            Dim variablesNode As New TreeNode("Variables (" & PartsLiteData.Variables.Count.ToString & "/" & PartsLiteData.ExpectedNumVariables.ToString & ")")
            For Each variable As SolidEdgeExplorerDLL.Variable In PartsLiteData.Variables
                ' Nodo principale con il nome della variabile

                'Dim V As Double = UserUnits(variable, PSMCluster0)
                Dim V As Double = variable.UserValue

                'Dim variableNode As New TreeNode(variable.Name & " = " & variable.Value.ToString)
                Dim variableNode As New TreeNode(variable.Name & " = " & V.ToString)

                ' Aggiungiamo le proprietà come sotto-nodi
                variableNode.Nodes.Add("ID: " & variable.ID.ToString())
                variableNode.Nodes.Add("Unit Type: " & variable.UnitType & "   " & New SolidEdgeExplorerDLL.Utilities.UnitTypeConstant(CInt(variable.UnitType)).Description)
                variableNode.Nodes.Add("Database Value: " & variable.Value.ToString())
                variableNode.Nodes.Add("User Value: " & V.ToString())

                If CustomPropertyVariableInfos.VariableInfos.Exists(Function(x) x.Variable_ID = variable.ID) Then
                    variableNode.Nodes.Add("Exposed property ID: " & CustomPropertyVariableInfos.VariableInfos.Find(Function(x) x.Variable_ID = variable.ID).Property_ID.ToString)
                    variableNode.NodeFont = New Font(TreeView1.Font, FontStyle.Bold)
                    variableNode.BackColor = Color.Gainsboro
                End If

                ' Aggiungiamo il nodo principale al TreeView
                variablesNode.Nodes.Add(variableNode)
            Next
            TreeView1.Nodes.Add(variablesNode)

            Dim NamedViewsNode As New TreeNode("NamedViews (" & PartsLiteData.NamedViews.Count.ToString & "/" & PartsLiteData.ExpectedNumNamedViews.ToString & ")")
            For Each view As SolidEdgeExplorerDLL.NamedView In PartsLiteData.NamedViews
                ' Nodo principale con il nome della vista
                Dim viewNode As New TreeNode(view.Name)

                ' Aggiungiamo le proprietà come sotto-nodi
                viewNode.Nodes.Add("Description: " & view.Description)
                viewNode.Nodes.Add("plFlags: " & view.plFlags.ToString())
                viewNode.Nodes.Add("Eye: " & String.Join(", ", view.Eye))
                viewNode.Nodes.Add("Target: " & String.Join(", ", view.Target))
                viewNode.Nodes.Add("Up: " & String.Join(", ", view.Up))
                viewNode.Nodes.Add("NearClip: " & view.NearClip.ToString())
                viewNode.Nodes.Add("FarClip: " & view.FarClip.ToString())
                viewNode.Nodes.Add("FrameWidth: " & view.FrameWidth.ToString())
                viewNode.Nodes.Add("FrameHeight: " & view.FrameHeight.ToString())
                viewNode.Nodes.Add("FrameEyeX: " & view.FrameEyeX.ToString())
                viewNode.Nodes.Add("FrameEyeY: " & view.FrameEyeY.ToString())
                viewNode.Nodes.Add("FrameScale: " & view.FrameScale.ToString())

                ' Aggiungiamo il nodo principale al TreeView
                NamedViewsNode.Nodes.Add(viewNode)
            Next
            TreeView1.Nodes.Add(NamedViewsNode)

        End If

        Dim DefaultsNode As New TreeNode("Defaults")
        For Each tmpDefault In PSMCluster0.DefaultUnits
            Dim tmpDefaultNode As New TreeNode(tmpDefault.Name)
            tmpDefaultNode.Nodes.Add("Unknown1: " & tmpDefault.Unknown1.ToString)
            tmpDefaultNode.Nodes.Add("Unknown2: " & tmpDefault.Unknown2.ToString)
            tmpDefaultNode.Nodes.Add("ValueType: " & tmpDefault.ValueType.ToString)
            If tmpDefault.Name.StartsWith("Default") Then
                tmpDefaultNode.Nodes.Add("Value: " & tmpDefault.Value.ToString & "   " & New SolidEdgeExplorerDLL.Utilities.UnitOfMeasure(CInt(tmpDefault.Value)).Description)
            Else
                tmpDefaultNode.Nodes.Add("Value: " & tmpDefault.Value.ToString)
            End If
            DefaultsNode.Nodes.Add(tmpDefaultNode)
        Next
        TreeView1.Nodes.Add(DefaultsNode)

        For Each node As TreeNode In TreeView1.Nodes
            If Not (node.Text = "Defaults" Or node.Text.Contains("NamedViews")) Then
                node.Expand()
            End If
        Next

        TreeView1.EndUpdate()

    End Sub

End Class

