using HeuristickiAlgoritmiProba;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;

namespace HeuristickiAlgoritmiProba
{
    class AStar : IAlgorithm
    {
        private Graph graph;
        private Node startingNode;
        private Node targetNode;
        private bool pathFound;
        private bool noSolution;
        private int pathCost;
        private List<List<Node>> paths;
        private List<Node> bestPath;
        private PathComparator pathComparator;
        public AStar(Graph graph)
        {
            initialization(graph);
        }

        private void initialization(Graph graph)
        {
            this.graph = graph;
            graph.ShouldDrawEdgeWeights(true);
            startingNode = graph.StartingNode;
            targetNode = graph.TargetNode;

            paths = new List<List<Node>>();
            paths.Add(new List<Node>() { startingNode });

            pathFound = false;
            noSolution = false;
            pathCost = 0;

            pathComparator = new PathComparator(graph);
        }

        void IAlgorithm.Iteration()
        {
            if (paths.Count == 0)
            {
                noSolution = true;
                return;
            }

            List<Node> optimalPath = paths[0];
            paths.RemoveAt(0);
            Node currentNode = optimalPath[optimalPath.Count - 1];
            currentNode.StateDictionary[HeuristicAlgorithms.AStar] = State.MARKED;
            if (currentNode.Equals(targetNode))
            {
                bestPath = optimalPath;
                pathFound = true;
                return;
            }

            List<Node> endNodesOfNewPaths = new List<Node>();
            foreach (Node node in graph.TargetNodesFromNode(currentNode))
            {
                List<Node> newPath = copyNodeList(optimalPath);
                if (node.StateDictionary[HeuristicAlgorithms.AStar] != State.MARKED)
                {
                    node.StateDictionary[HeuristicAlgorithms.AStar] = State.CONSIDERED;
                    newPath.Add(node);
                    paths.Add(newPath);
                }
            }

            foreach (Node node in endNodesOfNewPaths)
            {
                List<List<Node>> pathsWithSameEnd = new List<List<Node>>();
                foreach (List<Node> path in paths)
                {
                    if (path[path.Count - 1].Equals(node))
                    {
                        pathsWithSameEnd.Add(path);
                    }
                }
                pathsWithSameEnd.Sort(pathComparator);
                for (int i = 1; i < pathsWithSameEnd.Count; i++)
                    paths.Remove(pathsWithSameEnd[i]);
            }
            paths.Sort(pathComparator);
        }

        private List<Node> copyNodeList(List<Node> oldNodeList)
        {
            List<Node> newNodeList = new List<Node>();
            foreach (Node node in oldNodeList)
            {
                newNodeList.Add(node);
            }
            return newNodeList;
        }

        bool IAlgorithm.AlgorithmFinished()
        {
            if (noSolution)
                return true;
            return pathFound;
        }

        string IAlgorithm.FinalReport()
        {
            graph.MarkNodesAsUnmarked(HeuristicAlgorithms.AStar);
            if (noSolution)
                return string.Format("The A* algorithm did not find a solution to get from node {0} to node {1}.\n", startingNode, targetNode);
            if (!noSolution && !pathFound)
                return "The A* algorithm was forcibly terminated.\n";

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bestPath.Count - 1; i++)
                stringBuilder.Append(bestPath[i].ToString() + "-");
            stringBuilder.Append(bestPath[bestPath.Count - 1].ToString());
            string path = stringBuilder.ToString();
            int pathCost = pathComparator.PathLength(bestPath);

            return string.Format("The A* algorithm is now finished. A path from node {0} to node {1} has been found. The path to the node was {2}, and the path cost was {3}. You can run another algorithm.\n",
                                startingNode, targetNode, path, pathCost);

        }
    }
}