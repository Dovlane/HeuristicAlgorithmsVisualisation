using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeuristickiAlgoritmiProba;

namespace HeuristickiAlgoritmiProba
{
    class HillClimbing : IAlgorithm
    {
        private Graph graph;
        private Queue<Node> order;
        private Stack<Tuple<Node, Edge>> consideredNodesStack;
        private Node startingNode;
        private Node targetNode;
        private bool pathFound;
        private bool noSolution;
        private int pathCost;
        private static NodeHeuristicsComparator nodeHeuristicsComparator = new NodeHeuristicsComparator();

        public HillClimbing(Graph graph)
        {
            initialization(graph);
        }

        private void initialization(Graph graph)
        {
            this.graph = graph;
            graph.ShouldDrawEdgeWeights(true);
            startingNode = graph.StartingNode;
            targetNode = graph.TargetNode;

            order = new Queue<Node>();
            consideredNodesStack = new Stack<Tuple<Node, Edge>>();
            consideredNodesStack.Push(new Tuple<Node, Edge>(startingNode, null));

            pathFound = false;
            noSolution = false;
            pathCost = 0;
        }

        public void Iteration()
        {
            if (consideredNodesStack.Count() == 0)
            {
                noSolution = true;
                return;
            }

            Tuple<Node, Edge> nodeAndEdgeToNode = consideredNodesStack.Peek();
            Node currentNode = nodeAndEdgeToNode.Item1;
            if (nodeAndEdgeToNode.Item2 != null)
                pathCost += nodeAndEdgeToNode.Item2.EdgeWeight;
            order.Enqueue(currentNode);
            consideredNodesStack.Pop();
            currentNode.StateDictionary[HeuristicAlgorithms.HillClimbing] = State.MARKED;

            if (currentNode.Equals(targetNode))
            {
                pathFound = true;
                return;
            }

            List<Tuple<Node, Edge>> neighborsList = new List<Tuple<Node, Edge>>();
            foreach (Edge edge in graph.EdgesFromNode(currentNode))
            {
                Node node = edge.OtherNodeOfEdge(currentNode);
                if (node.StateDictionary[HeuristicAlgorithms.HillClimbing] != State.MARKED)
                {
                    node.StateDictionary[HeuristicAlgorithms.HillClimbing] = State.CONSIDERED;
                    neighborsList.Add(new Tuple<Node, Edge>(node, edge));
                }
            }
            neighborsList.Sort(nodeHeuristicsComparator);
            for (int i = neighborsList.Count() - 1; i >= 0; i--)
            {
                consideredNodesStack.Push(neighborsList[i]);
            }
        }

        public string FinalReport()
        {
            graph.MarkNodesAsUnmarked(HeuristicAlgorithms.HillClimbing);
            if (noSolution)
                return string.Format("Hill Climbing algorithm did not find a solution from node {0} to node {1}.\n", startingNode, targetNode);
            if (!noSolution && !pathFound)
                return "Hill Climbing algorithm was forcibly terminated.\n";

            StringBuilder sb = new StringBuilder();
            while (order.Count() > 1)
            {
                sb.Append(order.Peek().ToString() + "-");
                order.Dequeue();
            }
            sb.Append(order.Peek());
            order.Dequeue();
            String path = sb.ToString();

            return string.Format("Hill Climbing algorithm is now completed. Found a path from node {0} to node {1}. The path to the node was {2}, and the path cost was {3}. You can run another algorithm.\n",
                                startingNode, targetNode, path, pathCost);

        }

        public bool AlgorithmFinished()
        {
            if (noSolution)
                return true;
            return pathFound;
        }
    }
}
