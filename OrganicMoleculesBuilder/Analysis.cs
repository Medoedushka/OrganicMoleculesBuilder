using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDrawing;
using System.Windows.Forms;

namespace OrganicMoleculesBuilder
{
    public partial class Analysis : Form
    {
        CircleDiagram cd;
        PointsGraphic pg;
        string _data;
        public Analysis(string data)
        {
            InitializeComponent();
            cd = new CircleDiagram(pictureBox1);
            //pg = new PointsGraphic(pictureBox1, AxesMode.Static, AxesPosition.AllQuarters);
            //pg.AddCurve(new Curves(new PointF[] { new PointF(4, 4), new PointF(10, 10) }, Color.Red));
            //pg.DrawDiagram();
            cd.AddDiagramLegend = true;
            _data = data;
        }

        

        private void Analysis_Load(object sender, EventArgs e)
        {
            string[] parts = _data.Split('\n');
            Color color = Color.Red;
            foreach(string str in parts)
            {
                if (str == "") continue;
                string[] el = str.Split(':');
                if (el[0] == "C") color = Color.FromArgb(79, 235, 243);
                else if (el[0] == "H") color = Color.FromArgb(115, 115, 115);
                else if (el[0] == "O") color = Color.Red;
                else if (el[0] == "N") color = Color.Blue;
                else if (el[0] == "S") color = Color.FromArgb(127, 127, 0);
                cd.AddSector(new Sectors(double.Parse(el[1]), color, el[0]));
            }
            //pictureBox1.Invalidate();

            cd.DrawDiagram();
           // button1_Click(new object(), new EventArgs());
        }
      
    }
}
