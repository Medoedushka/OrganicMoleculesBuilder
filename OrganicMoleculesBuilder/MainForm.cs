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
        List<string> lastCommand = new List<string>();
        int counter = 0;
        int keyDownCounter = 1;
        //string lastCommand;
        string regexKeyWords;
        string regexSubs;
        Molecule crrMolecule;
        public MainForm()
        {
            InitializeComponent();
            crrMolecule = new Molecule("name", "MolecularParts");
            for (int i = 0; i < Molecule.Keywords.Length; i++)
            {
                if (char.IsLower(Molecule.Keywords[i][0]))
                {
                    regexKeyWords += "\\s" + Molecule.Keywords[i] + "\\b|";
                }
                else regexKeyWords += Molecule.Keywords[i] + "\\s|";
            }
            for(int i = 0; i < Molecule.Subs.Length; i++)
            {
                regexSubs += "\\s" + Molecule.Subs[i] + "\\s?|";
            }
        }
        private void rtb_Command_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)Keys.Enter && txb_Command.Text != "")
            {
                
                try
                {
                    string command = txb_Command.Text.TrimEnd(new char[] { ' ' }).TrimStart(new char[] { ' ' });
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    pcb_Output.Image = Molecule.RunCommand(ref crrMolecule, command, pcb_Output.Width, pcb_Output.Height);
                    s.Stop();
                    //MessageBox.Show(s.ElapsedMilliseconds.ToString());
                    //rtb_Debug.AppendText(">" + txb_Command.Text + "\n");
                    
                    if (txb_Command.Text == "Clear") crrMolecule = new Molecule("name", "MolecularParts");
                    lastCommand.Add(txb_Command.Text);
                    keyDownCounter = 1;
                    txb_Command.Text = string.Empty;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка выполнения команды!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                
            }
            
        }
        private void txb_Command_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Up)
            {
                txb_Command.Text = string.Empty;
                txb_Command.Text = lastCommand[lastCommand.Count - keyDownCounter];
                txb_Command.SelectionStart = txb_Command.Text.Length;
                keyDownCounter++;
                if (keyDownCounter > lastCommand.Count) keyDownCounter = 1;
            }
        }
        
        private void rtb_Out_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(regexKeyWords);
            MatchCollection matchesKey = regex.Matches(txb_Command.Text);
            regex = new Regex(regexSubs);
            MatchCollection matchesSub = regex.Matches(txb_Command.Text);
            MatchCollection matchesLinks = new Regex(@"\[\w*\]").Matches(txb_Command.Text);
            if (matchesKey.Count > 0)
            {
                Hightlight(matchesKey, Color.FromArgb(15, 132, 235), new Font("Arial", 10, FontStyle.Bold), txb_Command);
            }
            if (matchesSub.Count > 0)
            {
                Hightlight(matchesSub, Color.FromArgb(194, 110, 27), new Font("Arial", 10, FontStyle.Bold), txb_Command);
            }
            if (matchesLinks.Count > 0)
            {
                Hightlight(matchesLinks, Color.FromArgb(128, 0, 128), new Font("Arial", 10, FontStyle.Italic), txb_Command);
            }
        }

        private void Hightlight(MatchCollection matches, Color hightlight, Font font ,RichTextBox rtb)
        {
            int save = rtb.SelectionStart;
            foreach (Match m in matches)
            {
                if (m.Value == "") continue;
                string temp = rtb.Text;
                while (temp.Contains(m.Value))
                {
                    rtb.SelectionStart = temp.IndexOf(m.Value);
                    rtb.SelectionLength = m.Value.Length;
                    rtb.SelectionColor = hightlight;
                    rtb.SelectionFont = font;
                    temp = temp.Remove(rtb.SelectionStart, m.Value.Length);
                    temp = temp.Insert(rtb.SelectionStart, new string('*', m.Value.Length));
                }
                
            }
            rtb.SelectionStart = save;
            rtb.SelectionLength = 0;
            rtb.SelectionColor = Color.Black;
            rtb.SelectionFont = new Font("Arial", 10);
        }

        private void chb_CommandsDebug_CheckedChanged(object sender, EventArgs e)
        {
            rtb_Debug.ReadOnly = !rtb_Debug.ReadOnly;
        }

        private void rtb_Debug_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(regexKeyWords);
            MatchCollection matchesKey = regex.Matches(rtb_Debug.Text);
            regex = new Regex(regexSubs);
            MatchCollection matchesSub = regex.Matches(rtb_Debug.Text);
            MatchCollection matchesLinks = new Regex(@"\[\w*\]").Matches(rtb_Debug.Text);
            if (matchesKey.Count > 0)
            {
                Hightlight(matchesKey, Color.FromArgb(15, 132, 235), new Font("Arial", 10, FontStyle.Bold), rtb_Debug);
            }
            if (matchesSub.Count > 0)
            {
                Hightlight(matchesSub, Color.FromArgb(194, 110, 27), new Font("Arial", 10, FontStyle.Bold), rtb_Debug);
            }
            if (matchesLinks.Count > 0)
            {
                Hightlight(matchesLinks, Color.FromArgb(128, 0, 128), new Font("Arial", 10, FontStyle.Italic), rtb_Debug);
            }
        }

        private void btn_saveCode_Click(object sender, EventArgs e)
        {
            if (rtb_Debug.Text != "")
            {
                string[] strings = rtb_Debug.Text.Split('\n');
                for(int i = 0; i < strings.Length; i++)
                {
                    if (strings[i].Contains(">")) strings[i] = strings[i].Remove(0, 1);
                }

                string path = ""; DialogResult res;
                using (SaveFileDialog save = new SaveFileDialog())
                {
                    res = save.ShowDialog();
                    path = save.FileName + ".txt";
                }
                if (res == DialogResult.OK)
                {
                    if (!File.Exists(path))
                    {
                        using (File.Create(path)) { }
                        FileStream write = new FileStream(path, FileMode.Open, FileAccess.Write);
                        using (StreamWriter sw = new StreamWriter(write))
                        {
                            foreach(string s in strings)
                            {
                                if (s == "") continue;
                                sw.WriteLine(s);
                            }
                        }
                    }
                    MessageBox.Show("Код успешно сохранён!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void atomicCompositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (crrMolecule.atoms.Count != 0)
            {
                Analysis analysis = new Analysis(Quantitative.GetAtomPercents(crrMolecule));
                analysis.Show();
            }
        }

        private void saveWorkspaceAsPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = ""; DialogResult res;
            using (SaveFileDialog save = new SaveFileDialog())
            {
                res = save.ShowDialog();
                path = save.FileName;
            }
            if (path != "" && res == DialogResult.OK)
            {
                Thread.Sleep(1000);
                Bitmap bitmap = DrawControlToBitMap(pcb_Output);
                bitmap.Save(path + ".png");
                MessageBox.Show("Data has been saved successfully!", "Saving workspace...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static Bitmap DrawControlToBitMap(Control control)
        {
            Bitmap bitmap = new Bitmap(control.Width, control.Height);
            Graphics g = Graphics.FromImage(bitmap);
            System.Drawing.Rectangle rect = control.RectangleToScreen(control.ClientRectangle);
            g.CopyFromScreen(rect.Location, System.Drawing.Point.Empty, control.Size);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            return bitmap;
        }
    }
}
