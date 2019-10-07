using System;
using System.Drawing;
using System.Windows.Forms;

namespace OrganicMoleculesBuilder
{
    public partial class MainForm : Form
    {
        const double L = 50; // Длина связи C-C
        const double ANGLE = 109.47; // Угол связи C-C
        const double K = Math.PI / 180;
        Graphics g;
        int n = 15;
        float x0 = 0, y0 = 0;

        public MainForm()
        {
            InitializeComponent();
            x0 = 5;
            y0 = pcb_Output.Height / 2;
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
                molPoints[i].X = (float)(x0 + i * L * Math.Sin(K * ANGLE / 2 ));
                molPoints[i].Y = (float)(y0 + L * Math.Cos(K * ANGLE / 2) * (1 - Math.Pow(-1, i)) / 2);
            }
            using (g = pcb_Output.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLines(new Pen(Color.Red), molPoints);
            }
        }
    }
}
