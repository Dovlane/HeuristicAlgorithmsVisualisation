using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HeuristickiAlgoritmiProba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Graph graph;
        private List<IAlgorithm>? algorithms;
        private void Form1_Load(object sender, EventArgs e)
        {
            graph = new Graph();
            Node.SetPictureBoxDimensions(pictureBox1.Width, pictureBox1.Height);
            graph.SetPictureBoxDimensions(pictureBox1.Width, pictureBox1.Height);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            graph.Draw(e.Graphics);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            graph.MoveNode(e.X, e.Y);
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            graph.OnMouseUp();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                graph.OnMouseDown(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            pictureBox1.Refresh();
        }

        private void prikazTezinaChB_CheckedChanged(object sender, EventArgs e)
        {
            bool shouldShow = prikazTezinaChB.Checked;
            graph.ShouldDrawEdgeWeights(shouldShow);
            pictureBox1.Refresh();
        }

        private void usmerenaGranaChB_CheckedChanged(object sender, EventArgs e)
        {
            bool nextEdgeDirected = usmerenaGranaChB.Checked;
            graph.NewEdgeShouldBeDirected(nextEdgeDirected);
        }

        private void obrisiGrafBtn_Click(object sender, EventArgs e)
        {
            graph.Delete();
            pictureBox1.Refresh();
        }

        private void pokreniBtn_Click(object sender, EventArgs e)
        {
            statistikaRichTextBox.Text = "";
            timerAlgoritma.Start();
            algorithms = new List<IAlgorithm>() { new HillClimbing(graph), new AStar(graph), new BestFirst(graph) };
        }

        private void pauzirajBtn_Click(object sender, EventArgs e)
        {
            if (timerAlgoritma.Enabled == true)
            {
                timerAlgoritma.Enabled = false;
                pauzirajBtn.Text = "Continue";
            }
            else
            {
                timerAlgoritma.Enabled = true;
                pauzirajBtn.Text = "Pause";
            }
        }

       
        private void prekiniBtn_Click(object sender, EventArgs e)
        {
            if (algorithms != null)
            {
                foreach (IAlgorithm algorithm in algorithms)
                {
                    statistikaRichTextBox.Text += algorithm.FinalReport();
                }
                algorithms.Clear();
                algorithms = null;
            }
            pictureBox1.Refresh();
        }
        

        // - temporarily removed
        //private void describeBtn_Click(object sender, EventArgs e)
        //{
        //    descriptionRichTextBox.Text = graph.Description();
        //}


        private void timerAlgoritma_Tick(object sender, EventArgs e)
        {
            if (algorithms != null)
            {
                int i = 0;
                while (algorithms.Count > 0 && i < algorithms.Count)
                {
                    IAlgorithm algorithm = algorithms[i];
                    if (algorithm.AlgorithmFinished())
                    {
                        Console.WriteLine("The execution of the algorithm is finished");
                        statistikaRichTextBox.Text += algorithm.FinalReport();
                        algorithms.Remove(algorithm);
                        i--;
                    }
                    else
                    {
                        algorithm.Iteration();
                    }
                    i++;
                }
                if (algorithms.Count == 0)
                {
                    timerAlgoritma.Stop();
                    algorithms = null;
                }

            }
            pictureBox1.Refresh();
        }

    }
}