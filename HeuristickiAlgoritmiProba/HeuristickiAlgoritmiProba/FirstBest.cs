using HeuristickiAlgoritmiProba;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeuristickiAlgoritmiProba
{
    class BestFirst : IAlgorithm
    {
        private Graph graph;
        private Queue<Node> order;
        private List<Tuple<Node, Edge>> priorityQueue;
        private Node startingNode;
        private Node targetNode;
        private bool pathFound;
        private bool noSolution;
        private int pathCost;
        private static NodeHeuristicsComparator nodeHeuristicsComparator = new NodeHeuristicsComparator();

        public BestFirst(Graph graph)
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
            priorityQueue = new List<Tuple<Node, Edge>>();
            priorityQueue.Add(new Tuple<Node, Edge>(startingNode, null));

            pathFound = false;
            noSolution = false;
            pathCost = 0;
        }

        public void Iteration()
        {
            if (priorityQueue.Count() == 0)
            {
                noSolution = true;
                return;
            }

            Tuple<Node, Edge> nodeAndEdgeToNode = priorityQueue[0];
            Node currentNode = nodeAndEdgeToNode.Item1;
            if (nodeAndEdgeToNode.Item2 != null)
                pathCost += nodeAndEdgeToNode.Item2.EdgeWeight;
            order.Enqueue(currentNode);
            priorityQueue.RemoveAt(0);
            currentNode.StateDictionary[HeuristicAlgorithms.BestFirst] = State.MARKED;

            if (currentNode.Equals(targetNode))
            {
                pathFound = true;
                return;
            }

            foreach (Edge edge in graph.EdgesFromNode(currentNode))
            {
                Node node = edge.OtherNodeOfEdge(currentNode);
                if (node.StateDictionary[HeuristicAlgorithms.BestFirst] != State.MARKED)
                {
                    node.StateDictionary[HeuristicAlgorithms.BestFirst] = State.CONSIDERED;
                    priorityQueue.Add(new Tuple<Node, Edge>(node, edge));
                }
            }
            priorityQueue.Sort(nodeHeuristicsComparator);
        }

        public string FinalReport()
        {
            graph.MarkNodesAsUnmarked(HeuristicAlgorithms.BestFirst);
            if (noSolution)
                return string.Format("BestFirst algorithm did not find a solution from node {0} to node {1}.\n", startingNode, targetNode);
            if (!noSolution && !pathFound)
                return "BestFirst algorithm was forcibly terminated.\n";

            StringBuilder sb = new StringBuilder();
            while (order.Count() > 1)
            {
                sb.Append(order.Peek().ToString() + "-");
                order.Dequeue();
            }
            sb.Append(order.Peek());
            order.Dequeue();
            string path = sb.ToString();

            return string.Format("BestFirst algorithm is now completed. Found a path from node {0} to node {1}. The path to the node was {2}, and the path cost was {3}. You can run another algorithm.\n",
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

