using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OrganicMoleculesBuilder
{
    public partial class MainForm : Form
    {
        const double L = 35; // Длина связи C-C
        const double ANGLE = 120;//109.47; // Угол связи C-C
        const double K = Math.PI / 180;
        Graphics g;
        int n = 15;
        float x0 = 0, y0 = 0;
        PointF vector = new PointF();
        PointF[] MainAcyclicChain;
        string lastCommand;

        public MainForm()
        {
            InitializeComponent();
            x0 = 100;
            y0 = pcb_Output.Height / 2 + 50;
            vector.X = (float)(L * Math.Sin(K * ANGLE / 2));
            vector.Y = (float)(L * Math.Cos(K * ANGLE / 2));

        }

        private void rtb_Command_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txb_Command.Text != "")
            {
                string command = txb_Command.Text;
                string[] el = command.Split(' ');
                try
                {
                    if (el[1] == "()")
                    {
                        string[] parts = el[0].Split('-');
                        try
                        {
                            if (parts[0].Contains("\r\n")) parts[0] = parts[0].Remove(0, 2);
                            if (parts[0] == "alkane")
                                DrawAcyclicPart(int.Parse(parts[1]));
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        string ang = el[1].Substring(1, el[1].Length - 2);
                        string[] parts = el[0].Split('-');
                        try
                        {
                            if (parts[0].Contains("\r\n")) parts[0] = parts[0].Remove(0, 2);
                            if (parts[0] == "alkane")
                                DrawAcyclicPart(int.Parse(parts[1]), int.Parse(ang));
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    lastCommand = txb_Command.Text;
                    txb_Command.Text = "";
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
                
            }
            
        }

        private void DrawAcyclicPart(int n, int ang = 0)
        {
            MainAcyclicChain = new PointF[n];
            Molecule mol = new Molecule("Alkane");
            for (int i = 0; i < MainAcyclicChain.Length; i++)
            {
                MainAcyclicChain[i].X = x0 + i * vector.X;
                MainAcyclicChain[i].Y = (float)(y0 - vector.Y * (1 - Math.Pow(-1, i)) / 2);
                Atom atom = new Atom(Element.C, 4, i, new PointF(MainAcyclicChain[i].X, MainAcyclicChain[i].Y));
                mol.AddAtom(atom, 1, i - 1);
            }
            pcb_Output.Image = mol.ReturnPic(pcb_Output.Width, pcb_Output.Height);
            //Bitmap bm = new Bitmap(pcb_Output.Width, pcb_Output.Height);
            //using (g = Graphics.FromImage(bm))
            //{
            //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //    g.DrawLines(new Pen(Color.Black), MainAcyclicChain);
            //    DrawSub(1, new PointF(MainAcyclicChain[4].X, MainAcyclicChain[4].Y), bm, ang);
            //}
            //pcb_Output.Image = bm;
            
        }

        private void DrawSub(int len, PointF pos, Bitmap bm, int a = 0)
        {
            PointF[] pt = new PointF[len + 1];
            pt[0].X = pos.X; pt[0].Y = pos.Y;
            pt[1].X = pt[0].X; pt[1].Y = pt[0].Y - (float)L;
           
            PointF[] pt_temp = new PointF[len + 1];
            for (int i = 0; i < pt_temp.Length; i++)
            {
                if (i == 0)
                {
                    pt_temp[i].X = pos.X;
                    pt_temp[i].Y = pos.Y;
                }
                else
                {
                    PointF oldVector = new PointF(pt[i].X - pt[0].X, pt[i].Y - pt[0].Y);
                    PointF newVector = RotateVector(a, oldVector);
                    pt_temp[i].X = pt[0].X + newVector.X;
                    pt_temp[i].Y = pt[0].Y + newVector.Y;
                }
            }
            
            using (g = Graphics.FromImage(bm))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLines(new Pen(Color.Black), pt_temp); //(float)2 * vector.X + pos.X, pos.Y
                //g.DrawLine(new Pen(Color.Green), pt_temp[0], pt_temp[1]);
                //for (int i = 1; i < 5; i++)
                //{
                //    g.DrawLine(new Pen(Color.Green), pt_temp[1], pt_temp[i]);
                //}

            }
            pcb_Output.Image = bm;
        }

        private PointF RotateVector(double ang, PointF vec)
        {
            float x = 0, y = 0;
            x = (float)(vec.X * Math.Cos(ang * K) - vec.Y * Math.Sin(ang * K));
            y = (float)(vec.X * Math.Sin(ang * K) + vec.Y * Math.Cos(ang * K));
            return new PointF(x, y);
        }

        private void txb_Command_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                txb_Command.Text = string.Empty;
                txb_Command.Text = lastCommand;
            }
        }

        private PointF[] DrawPoly(double l, PointF center, int n = 5)
        {
            PointF[] pt = new PointF[n];
            for(int i = 0; i < pt.Length; i++)
            {
                pt[i].X = (float)(l * Math.Sin(i * 72 * K) + center.X);
                pt[i].Y = (float)(l * Math.Cos(i * 72 * K) + center.Y);
            }

            return pt;
        }
    }
}
