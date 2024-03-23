using HeuristickiAlgoritmiProba;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeuristickiAlgoritmiProba
{
    class Edge
    {
        private static Random r = new Random();
        private static double maxEdgeWeight = 10.0;
        private static Pen p = new Pen(Color.Black);
        private static int arrowLength = 10; // length of the arrow arm
        private static double arrowAngle = 45 * Math.PI / 180;
        private Node startingNode;
        private Node targetNode;
        private bool directed;
        public Node StartingNode { get { return startingNode; } }
        public Node TargetNode { get { return targetNode; } }
        public bool Undirected { get { return !directed; } }
        public bool Directed { get { return directed; } }
        private int edgeWeight;
        public int EdgeWeight => edgeWeight;

        private double determineRandomEdgeWeight()
        {
            return r.NextDouble() * maxEdgeWeight;
        }
        public Edge(Node startingNode, Node targetNode)
        {
            this.startingNode = startingNode;
            this.targetNode = targetNode;
            //this.edgeWeight = determineRandomEdgeWeight();
            directed = false;
        }

        public Edge(Node startingNode, Node targetNode, bool directed)
        {
            this.startingNode = startingNode;
            this.targetNode = targetNode;
            //this.edgeWeight = determineRandomEdgeWeight();
            this.edgeWeight = weightBasedOnDistance();
            this.directed = directed;
        }

        private double DetermineAngle()
        {
            int xP = startingNode.X;
            int yP = startingNode.Y;
            int xC = targetNode.X;
            int yC = targetNode.Y;
            double d = Math.Sqrt((xP - xC) * (xP - xC) + (yP - yC) * (yP - yC));

            int dX = xC - xP;
            int dY = yC - yP;
            double arccos = Math.Acos(dX * 1.0 / d);
            double arcsin = Math.Asin(dY * 1.0 / d);

            double alpha = arccos;
            if (arcsin < 0)
                alpha *= -1;

            return alpha;
        }

        private int weightBasedOnDistance()
        {
            int xP = startingNode.X;
            int yP = startingNode.Y;
            int xC = targetNode.X;
            int yC = targetNode.Y;
            return Convert.ToInt32(Math.Sqrt((xP - xC) * (xP - xC) + (yP - yC) * (yP - yC)));
        }

        private int LineLength()
        {
            int xP = startingNode.X;
            int yP = startingNode.Y;
            int xC = targetNode.X;
            int yC = targetNode.Y;
            return Convert.ToInt32(Math.Sqrt((xP - xC) * (xP - xC) + (yP - yC) * (yP - yC))) - 2 * Node.R;
        }

        public void SetEdgeWeight()
        {
            edgeWeight = weightBasedOnDistance();
        }

        public void Draw(Graphics g, bool drawEdgeWeights)
        {
            double alfa = DetermineAngle();
            int edgeLength = LineLength();

            int xP = startingNode.X;
            int yP = startingNode.Y;
            int xC = targetNode.X;
            int yC = targetNode.Y;

            int xP_line = xP + Convert.ToInt32(Node.R * Math.Cos(alfa));
            int yP_line = yP + Convert.ToInt32(Node.R * Math.Sin(alfa));
            int xC_line = xC - Convert.ToInt32(Node.R * Math.Cos(alfa));
            int yC_line = yC - Convert.ToInt32(Node.R * Math.Sin(alfa));

            g.DrawLine(p, xP_line, yP_line, xC_line, yC_line);

            if (directed)
            {
                double firstArmAngle = alfa + Math.PI + arrowAngle;
                double secondArmAngle = alfa + Math.PI - arrowAngle;

                g.DrawLine(p, xC_line, yC_line, xC_line + Convert.ToInt32(arrowLength * Math.Cos(firstArmAngle)), yC_line + Convert.ToInt32(arrowLength * Math.Sin(firstArmAngle)));
                g.DrawLine(p, xC_line, yC_line, xC_line + Convert.ToInt32(arrowLength * Math.Cos(secondArmAngle)), yC_line + Convert.ToInt32(arrowLength * Math.Sin(secondArmAngle)));
            }

            if (drawEdgeWeights)
            {
                int xWeight = (xP_line + xC_line) / 2;
                int yWeight = (yP_line + yC_line) / 2;

                System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 8);
                System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                g.DrawString(edgeWeight.ToString(), drawFont, drawBrush, new Point(xWeight - 2, yWeight - 2));
            }
        }

        public bool ConnectsTheseTwoNodes(Node node1, Node node2)
        {
            return (node1.Equals(startingNode) && node2.Equals(targetNode)) ||
                    (node1.Equals(targetNode) && node2.Equals(startingNode));
        }

        public bool ContainsNode(Node node)
        {
            return startingNode.Equals(node) || targetNode.Equals(node);
        }

        public override string ToString()
        {
            if (directed)
                return startingNode + " -> " + targetNode;
            else
                return startingNode + " <-> " + targetNode;
        }

        public Node OtherNodeOfEdge(Node node)
        {
            if (node.Equals(startingNode))
                return targetNode;
            else
                return startingNode;
        }
    }
}