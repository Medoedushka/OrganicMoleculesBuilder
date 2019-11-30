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
        string _data;
        public Analysis(string data)
        {
            InitializeComponent();
            cd = new CircleDiagram(pictureBox1);
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
            cd.DrawDiagram();
        }
      
    }
}
