﻿using System;
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
        int n = 15;
        float x0 = 0, y0 = 0;
        PointF vector = new PointF();
        
        string lastCommand;
        Molecule crrMolecule;
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
                
                string command = txb_Command.Text;
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
                        if (crrMolecule != null)
                        {
                            string[] coords = el[2].Split(';');
                            if (el[1].Contains("-"))
                            {
                                string[] parts = el[1].Split('-');
                                DrawMolecularPart(parts[0], new PointF(float.Parse(coords[0]), float.Parse(coords[1])), int.Parse(parts[1]));
                            }
                            else
                                DrawMolecularPart(el[1], new PointF(float.Parse(coords[0]), float.Parse(coords[1])));
                        }
                        lastCommand = command;
                        txb_Command.Text = string.Empty;
                        break;
                    case "Addsub":
                        if (crrMolecule != null && el[2].ToLower() == "at")
                        {
                            DrawSub(el[1], int.Parse(el[3]), double.Parse(el[4]));
                            lastCommand = command;
                            txb_Command.Text = string.Empty;
                        }
                        break;
                    case "Rotate":
                        if (crrMolecule != null && el[2].ToLower() == "base")
                        {
                            string[] parts = el[1].Split(',');
                            int[] indexes = new int[parts.Length];
                            for(int i = 0; i < indexes.Length; i++)
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
                            pcb_Output.Image = crrMolecule.ReturnPic(pcb_Output.Width, pcb_Output.Height);
                        }
                        lastCommand = command;
                        txb_Command.Text = string.Empty;
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
                        break;
                    default:
                        MessageBox.Show("Неверная команда!", "Ошибка синтаксиса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
                
            }
            
        }

        private void DrawMolecularPart(string type, PointF pos, int n = 5)
        {
            if (type == "alkane")
            {
                PointF[] MainAcyclicChain = new PointF[n];
                x0 = pos.X; y0 = pos.Y;
                for (int i = 0; i < MainAcyclicChain.Length; i++)
                {
                    MainAcyclicChain[i].X = x0 + i * vector.X;
                    MainAcyclicChain[i].Y = (float)(y0 - vector.Y * (1 - Math.Pow(-1, i)) / 2);
                    Atom atom = new Atom(Element.C, 4, i + 1, new PointF(MainAcyclicChain[i].X, MainAcyclicChain[i].Y));
                    crrMolecule.AddAtom(atom, 1, i);
                }
            }
            else return;
            pcb_Output.Image = crrMolecule.ReturnPic(pcb_Output.Width, pcb_Output.Height);
        }

        private void DrawSub(string type, int subPos, double ang)
        {
            PointF[] subPt = new PointF[1];
            PointF targetPos = new PointF(0, 0);
            PointF[] newVector = new PointF[1];
            //Поиск координат атома, к которому будет цепляться заместитель
            foreach (Atom at in crrMolecule.atoms)
            {
                if (at.Index == subPos)
                {
                    targetPos = at.Position;
                    break;
                }
            }
            switch (type)
            {
                case "Me":
                    newVector = new PointF[1];
                    newVector[0] = RotateVector(ang, new PointF(0, (float)-L));
                    subPt = new PointF[2];
                    break;
                case "Et":
                    newVector = new PointF[2];
                    newVector[0] = RotateVector(ang, new PointF(0, (float)-L));
                    newVector[1] = RotateVector(ang, new PointF((float)(2 * L * Math.Sin( K * ANGLE / 2) * Math.Cos(Math.PI / 3)),
                       (float)(-2 * L * Math.Sin(K * ANGLE / 2) * Math.Sin(Math.PI / 3))));
                    subPt = new PointF[3];
                    break;
            }
            int prev = subPos;
            for (int i = 1; i < subPt.Length; i++)
            {
                subPt[i].X = targetPos.X + newVector[i - 1].X;
                subPt[i].Y = targetPos.Y + newVector[i - 1].Y;
                Atom atom = new Atom(Element.C, 4, crrMolecule.atoms.Count + 1, new PointF(subPt[i].X, subPt[i].Y));
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
                pt[i].X = (float)(l * Math.Sin(i * 72 * K) + center.X);
                pt[i].Y = (float)(l * Math.Cos(i * 72 * K) + center.Y);
            }

            return pt;
        }
    }
}