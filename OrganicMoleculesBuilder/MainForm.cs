using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OrganicMoleculesBuilder
{
    public partial class MainForm : Form
    {
        const double L = 35; // Длина связи C-C
        const double ANGLE = 109.47; // Угол связи C-C
        const double K = Math.PI / 180;
        Graphics g;
        int n = 15;
        float x0 = 0, y0 = 0;
        PointF vector = new PointF();

        public MainForm()
        {
            InitializeComponent();
            x0 = 150;
            y0 = pcb_Output.Height / 2;
            vector.X = (float)(L * Math.Sin(K * ANGLE / 2));
            vector.Y = (float)(L * Math.Cos(K * ANGLE / 2));
        }

        private void rtb_Command_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) DrawStructure();
        }

        private void DrawStructure()
        {
            PointF[] molPoints = new PointF[n];
            for(int i = 0; i < molPoints.Length; i++)
            {
                molPoints[i].X = x0 + i * vector.X;
                molPoints[i].Y = (float)(y0 - vector.Y * (1 - Math.Pow(-1, i)) / 2);
            }

            //PointF pt1 = new PointF();
            //pt1.X = molPoints[1].X + RotateVector(ANGLE / 2).X;
            //pt1.Y = (float)(molPoints[1].Y - RotateVector(ANGLE / 2).Y);

            //PointF pt2 = new PointF();
            //pt2.X = molPoints[1 - 1].X;
            //pt2.Y = (float)(molPoints[1].Y - 2 * RotateVector(90).Y);

            //PointF pt3 = new PointF();
            //pt3.X = molPoints[0].X + RotateVector(ANGLE).X;
            //pt3.Y = (float)(molPoints[0].Y - RotateVector(ANGLE).Y);

            //PointF pt4 = new PointF();
            //pt4.X = pt3.X - RotateVector(ANGLE / 2).X;
            //pt4.Y = (float)(pt3.Y - RotateVector(ANGLE / 2).Y);

            //PointF pt5 = new PointF();
            //pt5.X = pt3.X - RotateVector(ANGLE / 2).X;
            //pt5.Y = (float)(pt3.Y - RotateVector(ANGLE / 2).Y);

            Bitmap bm = new Bitmap(pcb_Output.Width, pcb_Output.Height);
            using (g = Graphics.FromImage(bm))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLines(new Pen(Color.Black), molPoints);
                //g.DrawLine(new Pen(Color.Black), molPoints[1], pt1);
                //g.DrawLine(new Pen(Color.Red), x0, y0, vector.X + x0, -vector.Y + y0);
                //g.DrawLine(new Pen(Color.Black), pt1, pt2);
                //g.DrawLine(new Pen(Color.Black), molPoints[0], pt3);
                //g.DrawLine(new Pen(Color.Black), pt3, pt4);
                //g.DrawLine(new Pen(Color.Black), pt4, pt2);
                //g.DrawLine(new Pen(Color.Black), molPoints[0].X, molPoints[0].Y, molPoints[0].X, 0);
                
            }
            pcb_Output.Image = bm;
        }

        private PointF RotateVector(double ang)
        {
            float x = 0, y = 0;
            x = (float)(vector.X * Math.Cos(ang * K) - vector.Y * Math.Sin(ang * K));
            y = (float)(vector.X * Math.Sin(ang * K) + vector.Y * Math.Cos(ang * K));
            return new PointF(x, y);
        }
    }
}
