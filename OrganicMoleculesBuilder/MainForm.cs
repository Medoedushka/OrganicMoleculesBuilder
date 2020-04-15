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
        PictureBox[] pcbGroup1;
        Color buttonChecked = Color.FromArgb(150, 80, 80);

        public event Action<string> SaveWorkSpace;

        public MainForm()
        {
            InitializeComponent();
            pcbGroup1 = new PictureBox[]
            {
                pcb_None,
                pcb_SolidBond,
                pcb_WedgetBond,
                pcb_HashedWedgetBond,
                pcb_WavyBond,
                pcb_DashedBond,
                pcb_Text,
                pcb_Arrow
            };
            ToolType = ToolType.SolidBond;
            foreach(PictureBox pb in pcbGroup1)
            {
                pb.MouseEnter += (object o, EventArgs e) => { if (pb.BackColor != buttonChecked) pb.BackColor = Color.FromArgb(177, 195, 80, 80); };
                pb.MouseLeave += (object o, EventArgs e) => { if (pb.BackColor != buttonChecked) pb.BackColor = Color.LightCoral; };
            }
        }
        public ToolType ToolType { get; set; }
        public PictureBox DrawPlace => pictureBox3;

        public string PathToSave
        {
            get
            {
                using (SaveFileDialog sf = new SaveFileDialog())
                {
                    sf.Filter = "PNG (*.png)|*.png";
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        return sf.FileName;
                    }
                    else return "";
                }
            }
        }

        private void SetGroupColor(Color color, PictureBox[] pbs)
        {
            foreach (PictureBox pb in pbs)
                pb.BackColor = color;
        }

        private void pcb_SolidBond_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_SolidBond.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.SolidBond;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_WedgetBond_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_WedgetBond.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.WedgetBond;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_HashedWedgetBond_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_HashedWedgetBond.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.HashedWedgetBond;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_DashedBond_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_DashedBond.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.DashedBond;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_WavyBond_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_WavyBond.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.WavyBond;
            pictureBox3.Cursor = Cursors.Default;
        }
        private void pcb_Arrow_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_Arrow.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.Arrow;
            pictureBox3.Cursor = Cursors.Cross;
        }

        private void pcb_None_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_None.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.None;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_Text_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_Text.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.Text;
            pictureBox3.Cursor = Cursors.IBeam;
        }

        private void экспортироватьКакPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveWorkSpace?.Invoke(PathToSave);
        }
    }
}
