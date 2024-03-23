using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeuristickiAlgoritmiProba
{
    class NodeHeuristicsComparator : IComparer<Tuple<Node, Edge>>
    {
        public int Compare(Tuple<Node, Edge> first, Tuple<Node, Edge> second)
        {
            return first.Item1.Heuristic.CompareTo(second.Item1.Heuristic);
        }
    }
}
