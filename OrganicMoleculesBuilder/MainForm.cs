using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MoleculesBuilder;

namespace OrganicMoleculesBuilder
{
    public partial class MainForm : Form
    {
        const double L = 35; // Длина связи C-C
        const double ANGLE = 120;//109.47; // Угол связи C-C
        const double K = Math.PI / 180;
        Graphics g;
        float x0 = 0, y0 = 0;
        PointF vector = new PointF();
        
        string lastCommand;
        Molecule crrMolecule;

        string[] Keywords =
        {
            "Create",
            "Add",
            "Insert",
            "at",
            "Rotate",
            "base",
            "Delete",
            "Connect",
            "inv",
            "by",
            "Circles",
            "Numbers",
            "Move",
            "Clear"
        };
        string[] Subs =
        {
            "Me",
            "Et",
            "OH",
            "COOH",
            "NH2",
            "F",
            "Cl",
            "Br",
            "I",
            "S"
        };

        public MainForm()
        {
            InitializeComponent();
            x0 = 100;
            y0 = pcb_Output.Height / 2 + 50;
            vector.X = (float)(L * Math.Sin(K * ANGLE / 2));
            vector.Y = (float)(L * Math.Cos(K * ANGLE / 2));

        }
        private void rtb_Command_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txb_Command.Text != "")
            {
                if (txb_Command.Text != "File")
                {
                    pcb_Output.Image = Molecule.RunCommand(ref crrMolecule, txb_Command.Text, pcb_Output.Width, pcb_Output.Height);
                    lastCommand = txb_Command.Text;
                    txb_Command.Text = string.Empty;
                    //MessageBox.Show(crrMolecule.ToString());
                }
                else
                {
                    pcb_Output.Image = Molecule.RunCommandsFromFile(crrMolecule, "test.txt", pcb_Output.Width, pcb_Output.Height);
                    lastCommand = txb_Command.Text;
                    txb_Command.Text = string.Empty;
                }
            }
            
        }

        private void txb_Command_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                txb_Command.Text = string.Empty;
                txb_Command.Text = lastCommand;
            }
        }

        private void rtb_Out_TextChanged(object sender, EventArgs e)
        {
            int save = txb_Command.SelectionStart;

            string str = txb_Command.Text;
            for(int i = 0; i < Keywords.Length; i++)
            {
                int val;    
                if ( (val = str.IndexOf(Keywords[i])) >= 0)
                {
                    txb_Command.SelectionStart = val;
                    txb_Command.SelectionLength = Keywords[i].Length;
                    txb_Command.SelectionColor = Color.Blue;
                    txb_Command.SelectionFont = new Font("Arial", 10, FontStyle.Bold);

                    txb_Command.SelectionStart = txb_Command.Text.Length;
                    txb_Command.SelectionColor = Color.Black;
                    txb_Command.SelectionFont = new Font("Arial", 10);
                }
            }
            for (int i = 0; i < Subs.Length; i++)
            {
                int val;
                if ((val = str.IndexOf(Subs[i])) >= 0)
                {
                    txb_Command.SelectionStart = val;
                    txb_Command.SelectionLength = Subs[i].Length;
                    txb_Command.SelectionColor = Color.FromArgb(194, 110, 27);
                    txb_Command.SelectionFont = new Font("Arial", 10, FontStyle.Bold);

                    txb_Command.SelectionStart = txb_Command.Text.Length;
                    txb_Command.SelectionColor = Color.Black;
                    txb_Command.SelectionFont = new Font("Arial", 10);
                }
            }
            if (txb_Command.Text == "")
            {
                txb_Command.SelectionColor = Color.Black;
                txb_Command.SelectionFont = new Font("Arial", 10);
            }
            txb_Command.SelectionStart = save;
        }

    }
}
