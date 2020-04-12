using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MoleculesBuilder;

namespace OrganicMoleculesBuilder
{
    enum ToolType
    {
        SolidBond,
        ChangeOrder
    }

    public partial class MainForm : Form
    {
        Molecule crrMolecule;
        ToolType toolType = ToolType.SolidBond;
        PointF mouseLoc;
        bool firstClick = true;
        Atom founAtom;
        Bond foundBond;
        int[] angles;
        public MainForm()
        {
            InitializeComponent();
            crrMolecule = new Molecule("Name", "sfse");
            crrMolecule.ShowAtomNumbers = false;
            angles = new int[] { 0, 30, 60, 90, 120, 150, 180, -150, -120, -90, -60, -30};
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLoc = e.Location;
            
            foreach(Atom a in crrMolecule.atoms)
            {
                if (Math.Abs(a.Position.X - mouseLoc.X) <= 5 && Math.Abs(a.Position.Y - mouseLoc.Y) <= 5)
                {
                    founAtom = a;
                    break;
                }
                else founAtom = null;
            }
            foreach (Bond b in crrMolecule.bonds)
            {
                if (Math.Abs(b.BondCenter.X - mouseLoc.X) <= 5 && Math.Abs(b.BondCenter.Y - mouseLoc.Y) <= 5)
                {
                    foundBond = b;
                    break;
                }
                else foundBond = null;
            }
            
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (founAtom != null)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(120, Color.Blue)), founAtom.Position.X - 5, founAtom.Position.Y - 5, 10, 10);
            }
            if (foundBond != null)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(120, Color.Red)), foundBond.BondCenter.X - 5, foundBond.BondCenter.Y - 5, 10, 10);
            }
            if (crrMolecule.atoms.Count > 2)
            {
                e.Graphics.DrawRectangle(Pens.Blue, Molecule.GetRectangle(crrMolecule));
            }
        }

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
            string str;
            if ((founAtom != null && toolType == ToolType.SolidBond) || (crrMolecule.atoms.Count == 0 && toolType == ToolType.SolidBond) )
            {
                if (crrMolecule.atoms.Count == 0)
                {
                    str = $"Add Et at {mouseLoc.X};{mouseLoc.Y} 0";
                    pictureBox1.Image = Molecule.RunCommand(ref crrMolecule, str, pictureBox1.Width, pictureBox1.Height);
                    return;
                }

                check = !check;
                str = $"Add Me at {founAtom.Index} {angles[counter]}";
                try
                {
                    pictureBox1.Image = Molecule.RunCommand(ref crrMolecule, str, pictureBox1.Width, pictureBox1.Height);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                counter++;
                if (counter == angles.Length)
                    counter = 0;
            }
            if (foundBond != null && toolType == ToolType.ChangeOrder)
            {
                order++;
                if (order == 4)
                    order = 1;
                str = $"Connect {foundBond.A.Index} {foundBond.B.Index} by {order}";
                pictureBox1.Image = Molecule.RunCommand(ref crrMolecule, str, pictureBox1.Width, pictureBox1.Height);
            }
            
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
    }
}
