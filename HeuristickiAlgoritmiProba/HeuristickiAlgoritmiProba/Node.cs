using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeuristickiAlgoritmiProba
{
    public enum State
    {
        NOTHING,
        NODE_FOR_EDGE,
        MARKED,
        CONSIDERED
    }

    public enum HeuristicAlgorithms
    {
        HillClimbing,
        BestFirst,
        AStar
    }

    class Node
    {
        public static readonly int R = 15; // Radius of the node
        private static int sequentialValue = 0; // Sequential value for each node

        private static int PictureBoxWidth, PictureBoxHeight;

        private static Pen p = new Pen(Color.Black);
        private static SolidBrush sb_in_other_cases = new SolidBrush(Color.White);
        private static SolidBrush sb_node_for_edge = new SolidBrush(Color.Blue);

        private static SolidBrush sb_node_marked_a_star = new SolidBrush(Color.Yellow);
        private static SolidBrush sb_node_considered_a_star = new SolidBrush(Color.LightYellow);
        private static SolidBrush sb_node_marked_hill_climbing = new SolidBrush(Color.Red);
        private static SolidBrush sb_node_considered_hill_climbing = new SolidBrush(Color.HotPink);
        private static SolidBrush sb_node_marked_best_first = new SolidBrush(Color.Green);
        private static SolidBrush sb_node_considered_best_first = new SolidBrush(Color.LightGreen);
        private static Dictionary<Tuple<HeuristicAlgorithms, State>, SolidBrush> heuristic_algorithm_brushes = new Dictionary<Tuple<HeuristicAlgorithms, State>, SolidBrush>()
        { { new Tuple<HeuristicAlgorithms, State>(HeuristicAlgorithms.AStar, State.CONSIDERED), sb_node_considered_a_star},
          { new Tuple<HeuristicAlgorithms, State>(HeuristicAlgorithms.AStar, State.MARKED), sb_node_marked_a_star},
          { new Tuple<HeuristicAlgorithms, State>(HeuristicAlgorithms.HillClimbing, State.CONSIDERED), sb_node_considered_hill_climbing},
          { new Tuple<HeuristicAlgorithms, State>(HeuristicAlgorithms.HillClimbing, State.MARKED), sb_node_marked_hill_climbing},
          { new Tuple<HeuristicAlgorithms, State>(HeuristicAlgorithms.BestFirst, State.CONSIDERED), sb_node_considered_best_first},
          { new Tuple<HeuristicAlgorithms, State>(HeuristicAlgorithms.BestFirst, State.MARKED), sb_node_marked_best_first},
        };

        public State State { get; set; }

        private Dictionary<HeuristicAlgorithms, State> stateDictionary;
        public Dictionary<HeuristicAlgorithms, State> StateDictionary
        {
            get
            {
                return stateDictionary;
            }
        }
        public int Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Heuristic { get; set; }

        public bool Unmarked
        {
            get
            {
                State state = stateDictionary[HeuristicAlgorithms.AStar];
                return state == State.NOTHING;
            }
        }

        public static void SetPictureBoxDimensions(int width, int height)
        {
            PictureBoxWidth = width;
            PictureBoxHeight = height;
        }

        public Node(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
            this.Value = sequentialValue++;
            stateDictionary = new Dictionary<HeuristicAlgorithms, State>();
            SetNodeState(State.NOTHING);
        }

        public static void DeleteAllNodes()
        {
            sequentialValue = 0;
        }

        private SolidBrush GetSolidBrush(HeuristicAlgorithms heuristicAlgorithm)
        {
            State heuristicAlgorithmState = stateDictionary[heuristicAlgorithm];
            if (heuristicAlgorithmState == State.NODE_FOR_EDGE)
                return sb_node_for_edge;
            else if (heuristicAlgorithmState != State.NOTHING)
            {
                Tuple<HeuristicAlgorithms, State> tuple = new Tuple<HeuristicAlgorithms, State>(heuristicAlgorithm, heuristicAlgorithmState);
                return heuristic_algorithm_brushes[tuple];
            }
            else
                return sb_in_other_cases;
        }

        public void Draw(Graphics g)
        {
            int n = heuristicAlgorithms.Count;
            float startAngle = 90.0F - 360.0F / n;
            float sweepAngle = 360.0F / n;
            foreach (HeuristicAlgorithms heuristicAlgorithm in heuristicAlgorithms)
            {
                g.FillPie(GetSolidBrush(heuristicAlgorithm), X - R, Y - R, 2 * R, 2 * R, startAngle, sweepAngle);
                startAngle += sweepAngle;
            }

            g.DrawEllipse(p, X - R, Y - R, 2 * R, 2 * R);

            g.DrawString(Value.ToString(), fontForNodeValue, drawBrush, new Point(X - R + 2, Y - R + 2));

            g.DrawString(Heuristic.ToString(), fontForHeuristic, drawBrush, new PointF(X + R, Y - R));
        }

        private System.Drawing.Font fontForNodeValue
        {
            get
            {
                if (Value <= 9)
                    return new System.Drawing.Font("Arial", 20);
                else if (Value <= 99)
                    return new System.Drawing.Font("Arial", 15);
                else
                    throw new Exception("Too many nodes!");
            }
        }

        private static System.Drawing.Font fontForHeuristic = new System.Drawing.Font("Arial", 8);

        private static SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public bool AmISelected(int eX, int eY)
        {
            double d = Math.Sqrt((X - eX) * (X - eX) * 1.0 + (Y - eY) * (Y - eY));
            return d <= R * 1.0;
        }

        private bool withinBounds(int X, int Y)
        {
            return X - R >= 0 && X + R <= PictureBoxWidth &&
                    Y - R >= 0 && Y + R <= PictureBoxHeight;
        }

        private bool hitsRightEdge(int X)
        {
            return X + R > PictureBoxWidth;
        }

        private bool hitsLeftEdge(int X)
        {
            return X - R < 0;
        }

        private bool hitsTopEdge(int Y)
        {
            return Y - R < 0;
        }

        private bool hitsBottomEdge(int Y)
        {
            return Y + R > PictureBoxHeight;
        }

        public bool DoesAnotherNodeCollideWithMe(int otherNodeX, int otherNodeY)
        {
            double d = Math.Sqrt((otherNodeX - X) * (otherNodeX - X) + (otherNodeY - Y) * (otherNodeY - Y));
            return d <= R * 2.0;
        }

        public bool AmIBelowAnotherNode(int otherNodeY)
        {
            return Y > otherNodeY;
        }

        public bool AmIAboveAnotherNode(int otherNodeY)
        {
            return Y < otherNodeY;
        }

        public bool AmIRightOfAnotherNode(int otherNodeX)
        {
            return X > otherNodeX;
        }

        public bool AmILeftOfAnotherNode(int otherNodeX)
        {
            return X < otherNodeX;
        }

        public void SetNodeState(State state)
        {
            foreach (HeuristicAlgorithms heuristicAlgorithm in heuristicAlgorithms)
            {
                stateDictionary[heuristicAlgorithm] = state;
            }
        }

        public static List<HeuristicAlgorithms> heuristicAlgorithms
        {
            get { return Enum.GetValues(typeof(HeuristicAlgorithms)).Cast<HeuristicAlgorithms>().ToList(); }
        }

        public void SetNewCoordinates(int eX, int eY)
        {
            if (withinBounds(eX, eY))
            {
                X = eX;
                Y = eY;
            }
            else
            {
                if (hitsTopEdge(eY))
                    Y = R;
                if (hitsRightEdge(eX))
                    X = PictureBoxWidth - R;
                if (hitsBottomEdge(eY))
                    Y = PictureBoxHeight - R;
                if (hitsLeftEdge(eX))
                    X = R;
            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != GetType())
            {
                return false;
            }
            if (((Node)obj).Value != Value)
            {
                return false;
            }
            return true;
        }
    }
}
