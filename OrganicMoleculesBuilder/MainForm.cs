﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MoleculesBuilder;

namespace OrganicMoleculesBuilder
{
    public partial class MainForm : Form
    {
        string lastCommand;
        Molecule crrMolecule;
        public MainForm()
        {
            InitializeComponent();
            
        }
        private void rtb_Command_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txb_Command.Text != "")
            {
                try
                {
                    pcb_Output.Image = Molecule.RunCommand(ref crrMolecule, txb_Command.Text, pcb_Output.Width, pcb_Output.Height);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка выполнения команды!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                lastCommand = txb_Command.Text;
                txb_Command.Text = string.Empty;
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
            for(int i = 0; i < Molecule.Keywords.Length; i++)
            {
                int val;    
                if ( (val = str.IndexOf(Molecule.Keywords[i])) >= 0)
                {
                    txb_Command.SelectionStart = val;
                    txb_Command.SelectionLength = Molecule.Keywords[i].Length;
                    txb_Command.SelectionColor = Color.Blue;
                    txb_Command.SelectionFont = new Font("Arial", 10, FontStyle.Bold);

                    txb_Command.SelectionStart = txb_Command.Text.Length;
                    txb_Command.SelectionColor = Color.Black;
                    txb_Command.SelectionFont = new Font("Arial", 10);
                }
            }
            for (int i = 0; i < Molecule.Subs.Length; i++)
            {
                int val;
                if ((val = str.IndexOf(Molecule.Subs[i])) >= 0)
                {
                    txb_Command.SelectionStart = val;
                    txb_Command.SelectionLength = Molecule.Subs[i].Length;
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
