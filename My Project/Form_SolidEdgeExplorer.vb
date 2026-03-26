Imports System.IO
Imports System.Text
Imports SolidEdgeExplorerDLL
Imports SolidEdgeFramework

Public Class Form_SolidEdgeExplorer

    Public FileName As String

    Public Sub UpdateTree()

        Dim ext As String = Path.GetExtension(FileName).ToLower()

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

    Private Sub PopulateTreeView(File As String, PartsLiteData As PartsLiteData, CustomPropertyVariableInfos As CustomPropertyVariableInfo, Properties As SolidEdgeExplorerDLL.Properties, PSMCluster0 As PSMCluster0)

        'Dim header As String = BitConverter.ToString(PartsLiteData.RawData)
        'Dim namedViews As List(Of SolidEdgeExplorerDLL.NamedView) = PartsLiteData.NamedViews
        'Dim midHeader1 As String = ""
        'Dim Features As List(Of Feature) = PartsLiteData.Features
        'Dim midHeader2 As String = ""
        'Dim Variables As List(Of SolidEdgeExplorerDLL.Variable) = PartsLiteData.Variables

        TreeView1.BeginUpdate()

        TreeView1.Nodes.Clear() ' Puliamo il TreeView

        Dim FileNode As New TreeNode("File: " & File)
        TreeView1.Nodes.Add(FileNode)

        Dim DefaultsNode As New TreeNode("Defaults")
        For Each tmpDefaul In PSMCluster0.DefaultUnits
            Dim tmpDefaulNode As New TreeNode(tmpDefaul.Name)
            tmpDefaulNode.Nodes.Add("Unknown1: " & tmpDefaul.Unknown1.ToString)
            tmpDefaulNode.Nodes.Add("Unknown2: " & tmpDefaul.Unknown2.ToString)
            tmpDefaulNode.Nodes.Add("ValueType: " & tmpDefaul.ValueType.ToString)
            tmpDefaulNode.Nodes.Add("Value: " & tmpDefaul.Value.ToString)
            DefaultsNode.Nodes.Add(tmpDefaulNode)
        Next
        TreeView1.Nodes.Add(DefaultsNode)

        Dim PropertiesNode As New TreeNode("Properties")
        For Each PropertySet In Properties.PropertySets
            Dim tmpPropertySetNode As New TreeNode(PropertySet.Name)
            For Each item In PropertySet.Items
                Dim tmpItemNode As New TreeNode(item.OLEProp.PropertyName & " = " & item.Value.ToString)
                tmpPropertySetNode.Nodes.Add(tmpItemNode)
            Next
            PropertiesNode.Nodes.Add(tmpPropertySetNode)
        Next
        TreeView1.Nodes.Add(PropertiesNode)


        'Used to DEBUG PartsLiteData
        'Dim HeaderNode As New TreeNode("Header: " & header.Substring(0, 34) & "....")
        'HeaderNode.ToolTipText = header
        'TreeView1.Nodes.Add(HeaderNode)

        If Not IsNothing(PartsLiteData) Then

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

            'If midHeader1.Length > 0 Then
            '    Dim midHeaderNode1 As New TreeNode("Unknown: " & midHeader1)
            '    TreeView1.Nodes.Add(midHeaderNode1)
            'End If

            Dim featuresNode As New TreeNode("Features (" & PartsLiteData.Features.Count.ToString & "/" & PartsLiteData.ExpectedNumFeatures.ToString & ")")
            For Each feature As Feature In PartsLiteData.Features
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

            'If midHeader2.Length > 0 Then
            '    Dim midHeaderNode2 As New TreeNode("Unknown: " & midHeader2)
            '    TreeView1.Nodes.Add(midHeaderNode2)
            'End If

            Dim variablesNode As New TreeNode("Variables (" & PartsLiteData.Variables.Count.ToString & "/" & PartsLiteData.ExpectedNumVariables.ToString & ")")
            For Each variable As SolidEdgeExplorerDLL.Variable In PartsLiteData.Variables
                ' Nodo principale con il nome della variabile
                Dim variableNode As New TreeNode(variable.Name & " = " & variable.Value.ToString)

                ' Aggiungiamo le proprietà come sotto-nodi
                variableNode.Nodes.Add("ID: " & variable.ID.ToString())
                variableNode.Nodes.Add("Unit Type: " & variable.UnitType.ToString())
                variableNode.Nodes.Add("Value: " & variable.Value.ToString())


                If CustomPropertyVariableInfos.VariableInfos.Exists(Function(x) x.Variable_ID = variable.ID) Then
                    variableNode.Nodes.Add("Exposed property ID: " & CustomPropertyVariableInfos.VariableInfos.Find(Function(x) x.Variable_ID = variable.ID).Property_ID.ToString)
                    variableNode.NodeFont = New Font(TreeView1.Font, FontStyle.Bold)
                    variableNode.BackColor = Color.Gainsboro
                End If


                ' Aggiungiamo il nodo principale al TreeView
                variablesNode.Nodes.Add(variableNode)
            Next
            TreeView1.Nodes.Add(variablesNode)

        End If

        For Each node As TreeNode In TreeView1.Nodes
            node.Expand()
        Next

        TreeView1.EndUpdate()

    End Sub

End Class

