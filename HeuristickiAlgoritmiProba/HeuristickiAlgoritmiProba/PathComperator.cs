using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeuristickiAlgoritmiProba
{
    class PathComparator : IComparer<List<Node>>
    {
        private Graph graph;
        public Graph Graph { get { return graph; } }

        public PathComparator(Graph graph)
        {
            this.graph = graph;
        }

        public int Compare(List<Node> first, List<Node> second)
        {
            return PathLength(first).CompareTo(PathLength(second));
        }

        public int PathLength(List<Node> path)
        {
            int pathLength = 0;
            for (int i = 1; i < path.Count; i++)
            {
                Node startingNode = path[i - 1];
                Node targetNode = path[i];
                foreach (Edge edge in graph.Edges)
                {
                    if (edge.ConnectsTheseTwoNodes(startingNode, targetNode))
                    {
                        pathLength += edge.EdgeWeight;
                        break;
                    }
                }
            }

            Node lastNode = path[path.Count - 1];

            return pathLength + lastNode.Heuristic;
        }
    }
}