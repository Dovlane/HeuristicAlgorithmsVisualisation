using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeuristickiAlgoritmiProba
{
    class EdgeComparatorDescending : IComparer<Edge>
    {
        public int Compare(Edge l, Edge r)
        {
            int lSv = l.StartingNode.Value;
            int rSv = r.StartingNode.Value;
            int lEv = l.TargetNode.Value;
            int rEv = r.TargetNode.Value;

            if (lSv > rSv)
                return -1;
            else if (lSv < rSv)
                return 1;
            else
            {
                if (lEv > rEv)
                    return -1;
                else if (lEv < rEv)
                    return 1;
                else
                    return 0;
            }
        }
    }

    class Graph
    {
        private List<Node> nodes;
        private List<Edge> edges;
        private Node chosenNode;
        private Node startingNodeOfNewEdge;
        private Node targetNodeOfNewEdge;
        private static Random r = new Random();
        private int pictureBoxWidth, pictureBoxHeight;
        private bool shouldDrawEdgeWeights;
        private EdgeCreationPhase edgeCreationPhase;
        private bool mouseLeftButtonDown;
        private bool newEdgeShouldBeDirected;
        Dictionary<Node, Dictionary<HeuristicAlgorithms, State>> currentStatesOfNodes;
        private int[] heuristicAlgorithmsEnabled;

        public List<Node> Nodes { get { return nodes; } }
        public List<Edge> Edges { get { return edges; } }
        private enum EdgeCreationPhase { SelectingStartingNode, SelectingTargetNode }

        public void SetPictureBoxDimensions(int width, int height)
        {
            pictureBoxWidth = width;
            pictureBoxHeight = height;
        }

        public void NewEdgeShouldBeDirected(bool newEdgeShouldBeDirected)
        {
            this.newEdgeShouldBeDirected = newEdgeShouldBeDirected;
        }

        public void ShouldDrawEdgeWeights(bool shouldDrawEdgeWeights)
        {
            this.shouldDrawEdgeWeights = shouldDrawEdgeWeights;
        }

        public void PictureBoxDimensions(int width, int height)
        {
            pictureBoxWidth = width;
            pictureBoxHeight = height;
        }

        public Node StartingNode
        {
            get
            {
                return nodes[0];
            }
        }

        public Node TargetNode
        {
            get
            {
                return nodes[nodes.Count - 1];
            }
        }

        private static int DistanceBetweenNodes(Node firstNode, Node secondNode)
        {
            return Convert.ToInt32(Math.Sqrt((firstNode.X - secondNode.X) * (firstNode.X - secondNode.X) + (firstNode.Y - secondNode.Y) * (firstNode.Y - secondNode.Y)));
        }

        static IComparer<Edge> SortEdgesDescending
        {
            get
            {
                return new EdgeComparatorDescending();
            }
        }

        public Graph()
        {
            nodes = new List<Node>();
            edges = new List<Edge>();
            initialSettings();
        }

        public List<Edge> EdgesFromNode(Node node)
        {
            if (!nodes.Contains(node))
                return null;

            List<Edge> edgesFromNode = AllEdgesFromNode(node);

            return edgesFromNode;
        }

        private List<Edge> AllEdgesFromNode(Node node)
        {
            List<Edge> edgesFromNode = new List<Edge>();
            foreach (Edge edge in edges)
                if (nodeIsStartOfEdge(node, edge))
                    edgesFromNode.Add(edge);

            return edgesFromNode;
        }

        private bool nodeIsStartOfEdge(Node node, Edge edge)
        {
            return edge.StartingNode.Value == node.Value ||
                    (edge.Undirected && edge.TargetNode.Value == node.Value);
        }

        /// <summary>
        /// Determine target nodes from a given node.
        /// </summary>
        /// <param name="TargetNodes"></param>
        /// <returns></returns>
        public List<Node> TargetNodesFromNode(Node node)
        {
            if (!nodes.Contains(node))
                return null;

            List<Node> targetNodes = AllTargetNodesFromNode(node);

            return targetNodes;
        }

        public void MarkNodesAsUnmarked(HeuristicAlgorithms heuristicAlgorithm)
        {
            foreach (Node node in nodes)
            {
                node.StateDictionary[heuristicAlgorithm] = State.NOTHING;
            }
        }

        private List<Node> AllTargetNodesFromNode(Node node)
        {
            List<Node> targetNodes = new List<Node>();
            foreach (Edge edge in EdgesFromNode(node))
            {
                Node adjacentNode = AdjacentNode(edge, node);

                targetNodes.Add(adjacentNode);
            }
            return targetNodes;
        }

        private Node AdjacentNode(Edge edge, Node node)
        {
            Node adjacentNode;

            if (edge.TargetNode.Value == node.Value)
                adjacentNode = edge.StartingNode;
            else
                adjacentNode = edge.TargetNode;

            return adjacentNode;
        }

        public void Delete()
        {
            Node.DeleteAllNodes();
            edges.Clear();
            nodes.Clear();
        }

        public bool AllNodesMarked()
        {
            foreach (Node node in nodes)
                if (node.Unmarked)
                    return false;

            return true;
        }

        public void Draw(Graphics g)
        {
            DrawNodes(g);
            DrawEdges(g);
        }

        private void DrawNodes(Graphics g)
        {
            foreach (Node node in nodes)
                node.Draw(g);
        }

        private void DrawEdges(Graphics g)
        {
            foreach (Edge edge in edges)
                edge.Draw(g, shouldDrawEdgeWeights);
        }

        private double PotentialDistanceBetweenTwoNodes(int eX, int eY, Node node)
        {
            return Math.Sqrt((eX - node.X) * (eX - node.X) * 1.0 + (eY - node.Y) * (eY - node.Y));
        }

        private static int Distance(Node prviCvor, Node drugiCvor)
        {
            return Convert.ToInt32(Math.Sqrt((prviCvor.X - drugiCvor.X) * (prviCvor.X - drugiCvor.X) + (prviCvor.Y - drugiCvor.Y) * (prviCvor.Y - drugiCvor.Y)));
        }

        private bool nodesWouldOverlap(int eX, int eY, Node node)
        {
            double d = PotentialDistanceBetweenTwoNodes(eX, eY, node);
            return d < Node.R * 2;
        }

        private bool nodeIsSelected(int eX, int eY, Node node)
        {
            double d = PotentialDistanceBetweenTwoNodes(eX, eY, node);
            return d < Node.R * 1.0;
        }

        private bool canCreateNewNode(int eX, int eY)
        {
            foreach (Node node in nodes)
                if (nodesWouldOverlap(eX, eY, node))
                    return false;
            return true;
        }

        private bool someNodeIsSelected(int eX, int eY, out Node newlySelectedNode)
        {
            foreach (Node node in nodes)
                if (nodeIsSelected(eX, eY, node))
                {
                    newlySelectedNode = node;
                    return true;
                }
            newlySelectedNode = null;
            return false;
        }

        private bool EdgeAlreadyExists(Node node1, Node node2)
        {
            foreach (Edge edge in edges)
                if (edge.ConnectsTheseTwoNodes(node1, node2))
                    return true;

            return false;
        }

        private void initialSettings()
        {
            chosenNode = null;
            shouldDrawEdgeWeights = false;
            newEdgeShouldBeDirected = false;
            mouseLeftButtonDown = false;
            edgeCreationPhase = EdgeCreationPhase.SelectingStartingNode;
            currentStatesOfNodes = new Dictionary<Node, Dictionary<HeuristicAlgorithms, State>>();
            heuristicAlgorithmsEnabled = new int[] { 0, 0, 0 };
        }


        // Additional methods and logic related to Graph class and its interaction with user inputs
        // and graphical representation have been omitted from this translation snippet for brevity.


        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            Node selectedNode;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (canCreateNewNode(e.X, e.Y))
                    {
                        Node newNode = new Node(e.X, e.Y);
                        nodes.Add(newNode);
                        AssignNewHeuristicToAll();
                    }
                    else if (someNodeIsSelected(e.X, e.Y, out selectedNode))
                    {
                        chosenNode = selectedNode;
                    }
                    else
                        NodeNoLongerSelected();
                    mouseLeftButtonDown = true;
                    break;

                case MouseButtons.Right:
                    if (someNodeIsSelected(e.X, e.Y, out selectedNode))
                    {
                        if (edgeCreationPhase == EdgeCreationPhase.SelectingStartingNode)
                        {
                            startingNodeOfNewEdge = selectedNode;
                            startingNodeOfNewEdge.SetNodeState(State.NODE_FOR_EDGE);
                            edgeCreationPhase = EdgeCreationPhase.SelectingTargetNode;
                        }
                        else
                        {
                            targetNodeOfNewEdge = selectedNode;
                            if (startingNodeOfNewEdge.Value == targetNodeOfNewEdge.Value)
                            {
                                ResetEdgeCreation();
                                throw new Exception("Starting node can't be the same as the target node! The selected starting node for the new edge has been reset, you need to select it again.");
                            }
                            if (EdgeAlreadyExists(startingNodeOfNewEdge, targetNodeOfNewEdge))
                            {
                                ResetEdgeCreation();
                                throw new Exception("An edge between the selected nodes already exists! The selected starting node for the new edge has been reset, so you need to select it again.");
                            }
                            edges.Add(new Edge(startingNodeOfNewEdge, targetNodeOfNewEdge, newEdgeShouldBeDirected));
                            ResetEdgeCreation();
                        }
                    }
                    else // empty space is selected
                    {
                        if (edgeCreationPhase == EdgeCreationPhase.SelectingTargetNode)
                        {
                            ResetEdgeCreation();
                        }
                    }
                    break;
            }
        }

        private void ResetEdgeCreation()
        {
            startingNodeOfNewEdge.SetNodeState(State.NOTHING);
            startingNodeOfNewEdge = null;
            targetNodeOfNewEdge = null;
            edgeCreationPhase = EdgeCreationPhase.SelectingStartingNode;
        }

        public void OnMouseUp()
        {
            mouseLeftButtonDown = false;
        }

        public void MoveNode(int eX, int eY)
        {
            if (mouseLeftButtonDown)
                if (!(chosenNode is null))
                {
                    chosenNode.SetNewCoordinates(eX, eY);
                    AssignNewHeuristic(chosenNode);
                    AssignNewWeightsToEdges(chosenNode);
                    if (chosenNode.Equals(TargetNode))
                    {
                        AssignNewHeuristicToAll();
                    }
                }
        }

        public void NodeNoLongerSelected()
        {
            chosenNode = null;
        }

        public string Description()
        {
            StringBuilder sb = new StringBuilder();
            if (nodes.Count() == 0)
                return "There is no graph!";
            sb.Append("The graph has " + nodes.Count() + " nodes and " + edges.Count() + " edges." + Environment.NewLine);

            if (IsConnected())
                sb.Append("The graph is connected.");
            else
                sb.Append("The graph is not connected");
            sb.Append(Environment.NewLine);

            if (IsComplete())
                sb.Append("The graph is complete. ");
            else
                sb.Append("The graph is not complete. ");
            sb.Append(Environment.NewLine);

            if (IsTrivial())
                sb.Append("The graph is trivial. ");
            else
                sb.Append("The graph is not trivial. ");
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        //public void SortEdgesDescending()
        //{
        //    Array.Sort(edges.ToArray(), SortEdgesDescendingly);
        //}

        private bool IsConnected()
        {
            // Connectivity logic goes here
            return false; // Placeholder return
        }
        private void SaveCurrentNodeStates()
        {
            foreach (Node node in nodes)
                foreach (HeuristicAlgorithms algorithm in Node.heuristicAlgorithms)
                    currentStatesOfNodes[node][algorithm] = node.StateDictionary[algorithm];
        }
        private void ReturnToCurrentNodeStates()
        {
            foreach (Node node in currentStatesOfNodes.Keys)
                foreach (HeuristicAlgorithms algorithm in Node.heuristicAlgorithms)
                    node.StateDictionary[algorithm] = currentStatesOfNodes[node][algorithm];
        }

        private void AssignNewHeuristic(Node node)
        {
            node.Heuristic = DistanceBetweenNodes(TargetNode, node);
        }

        private void AssignNewWeightsToEdges(Node movedNode)
        {
            foreach (Edge edge in edges)
            {
                if (edge.ContainsNode(movedNode))
                {
                    edge.SetEdgeWeight();
                }
            }
        }

        private void AssignNewHeuristicToAll()
        {
            foreach (Node node in nodes)
            {
                AssignNewHeuristic(node);
            }
        }

        private bool IsComplete()
        {
            int numberOfNodes = nodes.Count();
            int numberOfEdges = edges.Count();
            int maxNumberOfEdges = numberOfNodes * (numberOfNodes - 1) / 2;
            return maxNumberOfEdges == numberOfEdges;
        }

        private bool IsTrivial()
        {
            int numberOfNodes = nodes.Count();
            return numberOfNodes == 1;
        }
    }
}
