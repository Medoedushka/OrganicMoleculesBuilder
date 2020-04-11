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
    public partial class MainForm : Form
    {
        Molecule crrMolecule;
        PointF mouseLoc;
        bool firstClick = true;
        Atom founAtom;
        public MainForm()
        {
            InitializeComponent();
            crrMolecule = new Molecule("Name", "sfse");
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLoc = e.Location;
            /*
            foreach(Atom a in crrMolecule.atoms)
            {
                if (a.Position == mouseLoc)
                    MessageBox.Show("asdgd");
            }
            */
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string str = $"Add Et at {1};{1} 60";
            pictureBox1.Image = Molecule.RunCommand(ref crrMolecule, str, pictureBox1.Width, pictureBox1.Height);
        }
    }
}
