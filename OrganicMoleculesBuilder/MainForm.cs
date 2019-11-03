using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
                try
                {
                    string command = txb_Command.Text.Trim(new char[] { '\n' });
                    string[] el = command.Split(' ');
                    switch (el[0])
                    {
                        case "Create":
                            if (crrMolecule == null)
                            {
                                crrMolecule = new Molecule(el[1]);
                            }
                            else MessageBox.Show("Молекула уже создана. Имя: " + crrMolecule);
                            lastCommand = command;
                            txb_Command.Text = string.Empty;
                            break;
                        
                        case "Add":
                            if (crrMolecule != null && el[2].ToLower() == "at")
                            {
                                if (el[3].Contains(";"))
                                {
                                    if (crrMolecule.atoms.Count != 0)
                                    {
                                        MessageBox.Show("((99((9");
                                        return;
                                    }
                                    DrawSub(el[1], "0" + el[3], double.Parse(el[4]));
                                }
                                else
                                {
                                    DrawSub(el[1], el[3], double.Parse(el[4]));
                                }
                                lastCommand = command;
                                txb_Command.Text = string.Empty;
                            }
                            break;
                        case "Rotate":
                            if (crrMolecule != null && el[2].ToLower() == "base")
                            {
                                string[] parts = el[1].Split(',');
                                int[] indexes = new int[parts.Length];
                                for (int i = 0; i < indexes.Length; i++)
                                {
                                    indexes[i] = int.Parse(parts[i]);
                                }
                                RotateMolecularPart(indexes, int.Parse(el[3]), double.Parse(el[4]));
                            }
                            lastCommand = command;
                            txb_Command.Text = string.Empty;
                            break;
                        case "Connect":
                            if (crrMolecule != null && el[3].ToLower() == "by")
                            {
                                crrMolecule.ConnectAtoms(int.Parse(el[1]), int.Parse(el[2]), int.Parse(el[4]));

                                if (el.Length == 6 && el[5].TrimEnd(new char[] { '\n' }) == "inv")
                                    crrMolecule.AddInvPair(int.Parse(el[1]), int.Parse(el[2]));

                                pcb_Output.Image = crrMolecule.ReturnPic(pcb_Output.Width, pcb_Output.Height);
                            }
                            lastCommand = command;
                            txb_Command.Text = string.Empty;
                            break;
                        case "Delete":
                            if (crrMolecule != null)
                            {
                                crrMolecule.RemoveAtom(int.Parse(el[1]));
                                pcb_Output.Image = crrMolecule.ReturnPic(pcb_Output.Width, pcb_Output.Height);
                            }
                            lastCommand = command;
                            txb_Command.Text = string.Empty;

                            break;
                        case "Move":
                            if (crrMolecule != null)
                            {
                                string[] parts = el[1].Split(',');
                                float a, b;
                                if (float.TryParse(parts[0], out a) && float.TryParse(parts[1], out b) && parts.Length == 2)
                                {
                                    PointF moveVector = new PointF(a, b);
                                    foreach(Atom at in crrMolecule.atoms)
                                    {
                                        at.Position = new PointF(at.Position.X + moveVector.X, at.Position.Y - moveVector.Y);
                                    }
                                    pcb_Output.Image = crrMolecule.ReturnPic(pcb_Output.Width, pcb_Output.Height);
                                }
                                else
                                {
                                    MessageBox.Show("Неверный синтаксис команды Move!", "Ошибка синтаксиса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                lastCommand = command;
                                txb_Command.Text = string.Empty;
                            }
                            break;
                        case "Clear":
                            if (crrMolecule != null)
                            {
                                string name = crrMolecule.ToString();
                                crrMolecule = null;
                                GC.Collect();
                                MessageBox.Show("Молекула " + name + " успешно удалена", "Удаление молекулы", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                pcb_Output.Image = new Bitmap(pcb_Output.Width, pcb_Output.Height);
                                lastCommand = command;
                                txb_Command.Text = string.Empty;
                            }
                            break;
                        case "Insert":
                            if (crrMolecule != null)
                            {
                                int val = int.Parse(el[1].Substring(el[1].IndexOf('(') + 1, 1));
                                string atom = Convert.ToString(el[1][0]);
                                int ind = int.Parse(el[2]);
                                crrMolecule.InsertAtom(atom, val, ind);
                                pcb_Output.Image = crrMolecule.ReturnPic(pcb_Output.Width, pcb_Output.Height);
                            }
                            break;
                        case "Circles":
                            if (crrMolecule != null)
                            {
                                if (el[1] == "On")
                                {
                                    crrMolecule.DrawAtomCircle = true;
                                }
                                else if (el[1] == "Off")
                                    crrMolecule.DrawAtomCircle = false;
                                pcb_Output.Image = crrMolecule.ReturnPic(pcb_Output.Width, pcb_Output.Height);

                            }
                            lastCommand = command;
                            txb_Command.Text = string.Empty;
                            break;
                       case "Numbers":
                            if (crrMolecule != null)
                            {
                                if (el[1] == "On")
                                {
                                    crrMolecule.ShowAtomNumbers = true;
                                }
                                else if (el[1] == "Off")
                                    crrMolecule.ShowAtomNumbers = false;
                                pcb_Output.Image = crrMolecule.ReturnPic(pcb_Output.Width, pcb_Output.Height);
                            }
                            lastCommand = command;
                            txb_Command.Text = string.Empty;
                            break;
                        default:
                            MessageBox.Show("Неверная команда!", "Ошибка синтаксиса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка при вводе команды", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            
        }

        //private void DrawMolecularPart(string type, PointF pos, int n = 5)
        //{
        //    if (type == "alkane")
        //    {
        //        PointF[] MainAcyclicChain = new PointF[n];
        //        x0 = pos.X; y0 = pos.Y;
        //        for (int i = 0; i < MainAcyclicChain.Length; i++)
        //        {
        //            MainAcyclicChain[i].X = x0 + i * vector.X;
        //            MainAcyclicChain[i].Y = (float)(y0 - vector.Y * (1 - Math.Pow(-1, i)) / 2);
        //            Atom atom = new Atom(Element.C, 4, i + 1, new PointF(MainAcyclicChain[i].X, MainAcyclicChain[i].Y));
        //            crrMolecule.AddAtom(atom, 1, i);
        //        }
        //    }
        //    else if (type == "cyclo")
        //    {
        //        PointF[] MainAcyclicChain = DrawPoly(L, pos, n);
        //        for(int i = 0; i < MainAcyclicChain.Length; i++)
        //        {
                   
        //            Atom atom = new Atom(Element.C, 4, i + 1, new PointF(MainAcyclicChain[i].X, MainAcyclicChain[i].Y));
        //            crrMolecule.AddAtom(atom, 1, i);
        //        }
        //        crrMolecule.ConnectAtoms(1, n, 1);
        //    }
            
        //    else return;
        //    pcb_Output.Image = crrMolecule.ReturnPic(pcb_Output.Width, pcb_Output.Height);
        //}

        private void GetCoord(double a, out PointF[] newVec, out PointF[] subPt) 
        {
            newVec = new PointF[1];
            newVec[0] = RotateVector(a, new PointF(0, (float)-L));
            subPt = new PointF[2];
        }

        private void DrawSub(string type, string pos, double ang)
        {
            PointF[] subPt = new PointF[1];
            PointF targetPos = new PointF(0, 0);
            PointF[] newVector = new PointF[1];
            int subPos;
            Element el = Element.C;
            int val = 4;

            //Поиск координат атома, к которому будет цепляться заместитель
            if (pos[0] == '0')
            {
                string[] parts = pos.Remove(0, 1).Split(';');
                targetPos = new PointF(float.Parse(parts[0]), float.Parse(parts[1]));
                subPos = 0;
            }
            else
            {
                subPos = int.Parse(pos);
                foreach (Atom at in crrMolecule.atoms)
                {
                    if (at.Index == subPos)
                    {
                        targetPos = at.Position;
                        break;
                    }
                }
            }
            GetCoord(ang, out newVector, out subPt);
            switch (type)
            {
                case "Me":
                    el = Element.C;
                    val = 4;
                    break;
                case "Et":
                    newVector = new PointF[2];
                    newVector[0] = RotateVector(ang, new PointF(0, (float)-L));
                    newVector[1] = RotateVector(ang, new PointF((float)(2 * L * Math.Sin( K * ANGLE / 2) * Math.Cos(Math.PI / 3)),
                       (float)(-2 * L * Math.Sin(K * ANGLE / 2) * Math.Sin(Math.PI / 3))));
                    subPt = new PointF[3];
                    el = Element.C;
                    val = 4;
                    break;
                case "OH":
                    el = Element.O;
                    val = 2;
                    break;
                case "NH2":
                    el = Element.N;
                    val = 3;
                    break;
                case "F":
                    el = Element.F;
                    val = 1;
                    break;
                case "Cl":
                    el = Element.Cl;
                    val = 1;
                    break;
                case "Br":
                    el = Element.Br;
                    val = 1;
                    break;
                case "I":
                    el = Element.I;
                    val = 1;
                    break;
                case "S":
                    el = Element.S;
                    val = 2;
                    break;
                default:
                    if (type.Contains("cyclo-"))
                    {
                        string[] parts = type.Split('-');
                        int n = int.Parse(parts[1]);
                        PointF vec = RotateVector(ang, new PointF(0, 2 * (float)L));
                        PointF[] MainAcyclicChain = DrawPoly(L, new PointF(targetPos.X, (float)(targetPos.Y - vec.Y)), n);
                        int p = subPos;
                        int s = 0;
                        for (int i = 0; i < MainAcyclicChain.Length; i++)
                        {

                            Atom atom = new Atom(Element.C, 4, crrMolecule.atoms.Count + 1, new PointF(MainAcyclicChain[i].X, MainAcyclicChain[i].Y));
                            crrMolecule.AddAtom(atom, 1, p);
                            p = crrMolecule.atoms.Count;
                            if (s == 0) s = p;
                        }
                        crrMolecule.ConnectAtoms(s, p, 1);
                    }
                    break;
            }
            
            int prev = subPos;
            for (int i = 1; i < subPt.Length; i++)
            {
                subPt[i].X = targetPos.X + newVector[i - 1].X;
                subPt[i].Y = targetPos.Y + newVector[i - 1].Y;
                Atom atom = new Atom(el, val, crrMolecule.atoms.Count + 1, new PointF(subPt[i].X, subPt[i].Y));
                crrMolecule.AddAtom(atom, 1, prev);
                prev = crrMolecule.atoms.Count;
            }
            pcb_Output.Image = crrMolecule.ReturnPic(pcb_Output.Width, pcb_Output.Height);
            
        }

        private void RotateMolecularPart(int[] rotInd, int baseInd, double ang)
        {
            //поиск координат атомов
            PointF[] rotPt = new PointF[rotInd.Length];
            PointF basePt = new PointF();
            foreach(Atom at in crrMolecule.atoms)
            {
                if (at.Index == baseInd)
                {
                    basePt = at.Position;
                    break;
                }
            }
            for(int i = 0; i < rotInd.Length; i++)
            {
                foreach(Atom at in crrMolecule.atoms)
                {
                    if (at.Index == rotInd[i])
                    {
                        PointF oldVector = new PointF(at.Position.X - basePt.X, at.Position.Y - basePt.Y);
                        PointF newVector = RotateVector(ang, oldVector);
                        at.Position = new PointF(basePt.X + newVector.X, basePt.Y + newVector.Y);
                        break;
                    }
                    
                }
            }

            pcb_Output.Image = crrMolecule.ReturnPic(pcb_Output.Width, pcb_Output.Height);
        }

        private PointF RotateVector(double ang, PointF vec)
        {
            float x = 0, y = 0;
            x = (float)(vec.X * Math.Cos(ang * K) - vec.Y * Math.Sin(ang * K));
            y = (float)(vec.X * Math.Sin(ang * K) + vec.Y * Math.Cos(ang * K));
            return new PointF(x, y);
        }

        private void txb_Command_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                txb_Command.Text = string.Empty;
                txb_Command.Text = lastCommand;
            }
        }

        private PointF[] DrawPoly(double l, PointF center, int n = 5)
        {
            PointF[] pt = new PointF[n];
            for(int i = 0; i < pt.Length; i++)
            {
                pt[i].X = (float)(l * Math.Sin(i * 360 / n * K) + center.X);
                pt[i].Y = (float)(l * Math.Cos(i * 360 / n * K) + center.Y);
            }

            return pt;
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
