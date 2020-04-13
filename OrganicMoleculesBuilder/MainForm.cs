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
        public MainForm()
        {
            InitializeComponent();

        }
        public ToolType ToolType { get; set; }
        public PictureBox DrawPlace => pictureBox3;

        private void pcb_SolidBond_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.SolidBond;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_WedgetBond_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.WedgetBond;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_HashedWedgetBond_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.HashedWedgetBond;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_DashedBond_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.DashedBond;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_WavyBond_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.WavyBond;
            pictureBox3.Cursor = Cursors.Default;
        }
        private void pcb_Arrow_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.Arrow;
            pictureBox3.Cursor = Cursors.Cross;
        }

        private void pcb_None_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.None;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_Text_Click(object sender, EventArgs e)
        {
            ToolType = ToolType.Text;
            pictureBox3.Cursor = Cursors.IBeam;
        }
    }
}
