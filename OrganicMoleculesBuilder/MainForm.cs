using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MoleculesBuilder;
using OrganicMoleculesBuilder.Viewer;

namespace OrganicMoleculesBuilder
{
    public partial class MainForm : Form, IMainViewer
    {
        //Molecule crrMolecule;
        //ToolType toolType = ToolType.SolidBond;
        //PointF mouseLoc;
        //bool firstClick = true;

        public MainForm()
        {
            InitializeComponent();
            //crrMolecule = new Molecule("Name", "sfse");
            //crrMolecule.ShowAtomNumbers = false;

        }
        public ToolType ToolType { get; set; }
        public PictureBox DrawPlace => pictureBox1;

        private void pcb_SolidBond_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.SolidBond;
        }

        private void pcb_WedgetBond_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.WedgetBond;
        }

        private void pcb_HashedWedgetBond_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.HashedWedgetBond;
        }

        private void pcb_DashedBond_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.DashedBond;
        }

        private void pcb_WavyBond_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.WavyBond;
        }

        private void pcb_ChangeOrder_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.ChangeOrder;
        }
    }
}
