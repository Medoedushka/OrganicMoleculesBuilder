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
        /*
         * 
        bool check = true;
        int ang = 60;
        int order = 1;
        private void связьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolType = ToolType.SolidBond;
            связьToolStripMenuItem.Checked = true;
            порядокСвязиToolStripMenuItem.Checked = false;
        }

        private void порядокСвязиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolType = ToolType.ChangeOrder;
            связьToolStripMenuItem.Checked = false;
            порядокСвязиToolStripMenuItem.Checked = true;
        }

        int counter = 0;
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            
            
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (founAtom != null)
                {
                    //if (founAtom.Index == crrMolecule.atoms.Count)
                    //{
                    string str = $"Delete {founAtom.Index}";
                    pictureBox1.Image = Molecule.RunCommand(ref crrMolecule, str, pictureBox1.Width, pictureBox1.Height);
                    //}
                }
            }
            else if (e.KeyCode == Keys.E)
            {
                counter++;
                if (counter == angles.Length)
                    counter = 0;

                int secInd = 0;
                foreach (Bond b in crrMolecule.bonds)
                {
                    if (b.A.Index == crrMolecule.atoms[crrMolecule.atoms.Count - 1].Index)
                        secInd = b.B.Index;
                    if (b.B.Index == crrMolecule.atoms[crrMolecule.atoms.Count - 1].Index)
                        secInd = b.A.Index;
                }
                
                crrMolecule.atoms[crrMolecule.atoms.Count - 1].Position = new PointF(crrMolecule.atoms[secInd - 1].Position.X,
                    (float)(crrMolecule.atoms[secInd - 1].Position.Y - Molecule.L));

                string str = $"Rotate {crrMolecule.atoms[crrMolecule.atoms.Count - 1].Index} base {secInd} {angles[counter]}";
                pictureBox1.Image = Molecule.RunCommand(ref crrMolecule, str, pictureBox1.Width, pictureBox1.Height);
            }
            else if (e.KeyCode == Keys.Q)
            {
                counter--;
                if (counter < 0)
                    counter = angles.Length - 1;

                int secInd = 0;
                foreach (Bond b in crrMolecule.bonds)
                {
                    if (b.A.Index == crrMolecule.atoms[crrMolecule.atoms.Count - 1].Index)
                        secInd = b.B.Index;
                    if (b.B.Index == crrMolecule.atoms[crrMolecule.atoms.Count - 1].Index)
                        secInd = b.A.Index;
                }

                crrMolecule.atoms[crrMolecule.atoms.Count - 1].Position = new PointF(crrMolecule.atoms[secInd - 1].Position.X,
                    (float)(crrMolecule.atoms[secInd - 1].Position.Y - Molecule.L));
                string str = $"Rotate {crrMolecule.atoms[crrMolecule.atoms.Count - 1].Index} base {secInd} {angles[counter]}";
                pictureBox1.Image = Molecule.RunCommand(ref crrMolecule, str, pictureBox1.Width, pictureBox1.Height);
            }
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            string path = "";
            if (founAtom != null)
            {
                
                if (e.KeyCode == Keys.O)
                {
                    path = $"Insert O(2) {founAtom.Index}";
                }
                if (e.KeyCode == Keys.N)
                {
                    path = $"Insert N(3) {founAtom.Index}";
                }
                if (e.KeyCode == Keys.F)
                {
                    path = $"Insert F(1) {founAtom.Index}";
                }

                if (!string.IsNullOrEmpty(path))
                    pictureBox1.Image = Molecule.RunCommand(ref crrMolecule, path, pictureBox1.Width, pictureBox1.Height);
            }
        }
        */
    }
}
