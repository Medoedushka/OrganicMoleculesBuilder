using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicMoleculesBuilder
{
    public class Molecule
    {
        string Name { get; set; }
        public List<Atom> atoms = new List<Atom>();
        List<string> InvAtomPairs = new List<string>();
        public bool ShowAtomNumbers { get; set; }
        public bool DrawAtomCircle { get; set; }

        public Molecule(string name)
        {
            Name = name;
            ShowAtomNumbers = true;
            DrawAtomCircle = true;
        }

        public void AddInvPair(int ind1, int ind2)
        {
            if (IsInvPair(ind1, ind2))
                return;

            InvAtomPairs.Add(ind1 + "-" + ind2);
        }

        private bool IsInvPair(int ind1, int ind2)
        {
            foreach(string str in InvAtomPairs)
            {
                string[] el = str.Split('-');
                if ((ind1 == int.Parse(el[0]) && ind2 == int.Parse(el[1])) || (ind1 == int.Parse(el[1]) && ind2 == int.Parse(el[0])))
                    return true;
                
            }
            return false;
        }

        private bool AtomExists(int atomIndex)
        {
            foreach(Atom at in atoms)
            {
                if (at.Index == atomIndex) return true;
            }
            return false;
        }

        private int Bonds(int firstInd, int secondInd, bool mode)
        {
            if (mode == true)
            {
                int count = 0;
                for (int i = 0; i < atoms[firstInd - 1].Neighbours.Length; i++)
                {
                    if (atoms[firstInd - 1].Neighbours[i]?.Index == atoms[secondInd - 1].Index) count++;
                }
                return count;
            }
            else
            {
                int count = 0;
                for (int i = 0; i < atoms[firstInd - 1].Neighbours.Length; i++)
                {
                    if (atoms[firstInd - 1].Neighbours[i] == null) count++;
                }
                return count;
            }
        }

        public void AddAtom(Atom newAtom, int order, int pos)
        {
            if (order > newAtom.Valence) throw new ArgumentOutOfRangeException("order", "order > valence", "Валентность не может быть меньше указаного порядка связи!");

            if (atoms.Count == 0) atoms.Add(newAtom);
            else
            {
                for(int i = 0; i < atoms.Count; i++)
                {
                    if (atoms[i].Index == pos)
                    {
                        int freeBons = 0;
                        for(int j = 0; j < atoms[i].Neighbours.Length; j++)
                        {
                            if (atoms[i].Neighbours[j] == null) freeBons++;
                        }

                        if (freeBons >= order)
                        {
                            int k = 0;
                            for (int j = 0; j < atoms[i].Neighbours.Length; j++)
                            {
                                if (atoms[i].Neighbours[j] == null && k < order)
                                {
                                    
                                    atoms[i].Neighbours[j] = newAtom;
                                    newAtom.Neighbours[j] = atoms[i];
                                    k++;
                                }
                              
                            }
                            atoms.Add(newAtom);
                            return;
                        }
                        else throw new ArgumentOutOfRangeException("freeBonds", "order > freeBonds", "У атома с индексом " + pos + " недостаточно свободных валентностей для образования связи!");
                    }
                }
            }
        }

        public void ConnectAtoms(int firstInd, int secondInd, int order)
        {
            if (order > 0 && order < 4)
            {
                if (!AtomExists(firstInd) || !AtomExists(secondInd))
                    throw new ArgumentOutOfRangeException("Передаваемые индексы атомов не существуют!");
                int d = order - Bonds(firstInd, secondInd, true);
                if (d > 0)
                {
                    if (d <= Bonds(firstInd, 0, false) && d <= Bonds(secondInd, 0, false))
                    {
                        int count = 0;
                        for (int i = 0; i < atoms[firstInd - 1].Neighbours.Length; i++)
                        {
                            if (atoms[firstInd - 1].Neighbours[i] == null && count < d)
                            {
                                atoms[firstInd - 1].Neighbours[i] = atoms[secondInd - 1];
                                count++;
                            }

                        }
                        count = 0;
                        for (int i = 0; i < atoms[secondInd - 1].Neighbours.Length; i++)
                        {
                            if (atoms[secondInd - 1].Neighbours[i] == null && count < d)
                            {
                                atoms[secondInd - 1].Neighbours[i] = atoms[firstInd - 1];
                                count++;
                            }

                        }
                    }
                }
                else
                {
                    if (Math.Abs(d) <= Bonds(firstInd, secondInd, true) && d <= Bonds(secondInd, firstInd, true))
                    {
                        int count = 0;
                        for (int i = 0; i < atoms[firstInd - 1].Neighbours.Length; i++)
                        {
                            if (atoms[firstInd - 1].Neighbours[i] == atoms[secondInd - 1] && count < Math.Abs(d))
                            {
                                atoms[firstInd - 1].Neighbours[i] = null;
                                count++;
                            }

                        }
                        count = 0;
                        for (int i = 0; i < atoms[secondInd - 1].Neighbours.Length; i++)
                        {
                            if (atoms[secondInd - 1].Neighbours[i] == atoms[firstInd - 1] && count < Math.Abs(d))
                            {
                                atoms[secondInd - 1].Neighbours[i] = null;
                                count++;
                            }

                        }
                    }
                }

            }
        }

        public void RemoveAtom(int index)
        {
            foreach(Atom at in atoms)
            {
                for(int i = 0; i < at.Neighbours.Length; i++)
                {
                    if (at.Neighbours[i] == atoms[index - 1])
                        at.Neighbours[i] = null;
                }
            }
            atoms.Remove(atoms[index - 1]);
        }

        public Bitmap ReturnPic(int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);
            Color roundColor = Color.Red; int r = 4;
            Font font = new Font("Arial", 7);
            using (Graphics g = Graphics.FromImage(bm))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                foreach(Atom at in atoms)
                {
                    
                    for(int i = 0; i < at.Neighbours.Length; i++)
                    {
                        if (at.Neighbours[i] != null && at.Index < at.Neighbours[i].Index)
                        {
                            int numBonds = Bonds(at.Index, at.Neighbours[i].Index, true);
                            g.DrawLine(new Pen(Color.Black), at.Position, at.Neighbours[i].Position);
                            if (numBonds >= 2)
                            {
                                int d = 5;
                                PointF vector = new PointF(at.Neighbours[i].Position.X - at.Position.X, at.Neighbours[i].Position.Y - at.Position.Y);
                               
                                double n_y = vector.X * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                                double n_x = -vector.Y * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                                
                                PointF moveVector = new PointF((float)n_x, (float)n_y);
                                PointF pt1;
                                PointF pt2;
                                if (IsInvPair(at.Index, at.Neighbours[i].Index))
                                {
                                    pt1 = new PointF(at.Position.X + moveVector.X, at.Position.Y + moveVector.Y);
                                    pt2 = new PointF(at.Neighbours[i].Position.X + moveVector.X, at.Neighbours[i].Position.Y + moveVector.Y);
                                }
                                else
                                {
                                    pt1 = new PointF(at.Position.X - moveVector.X, at.Position.Y - moveVector.Y);
                                    pt2 = new PointF(at.Neighbours[i].Position.X - moveVector.X, at.Neighbours[i].Position.Y - moveVector.Y);
                                }

                                g.DrawLine(new Pen(Color.Black), pt1, pt2);
                                
                            }
                            if (numBonds == 3)
                            {
                                int d = 5;
                                PointF vector = new PointF(at.Neighbours[i].Position.X - at.Position.X, at.Neighbours[i].Position.Y - at.Position.Y);

                                double n_y = vector.X * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                                double n_x = -vector.Y * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                                PointF moveVector = new PointF((float)n_x, (float)n_y);
                                PointF pt1;
                                PointF pt2;
                                if (IsInvPair(at.Index, at.Neighbours[i].Index))
                                {
                                    pt1 = new PointF(at.Position.X - moveVector.X, at.Position.Y - moveVector.Y);
                                    pt2 = new PointF(at.Neighbours[i].Position.X - moveVector.X, at.Neighbours[i].Position.Y - moveVector.Y);
                                }
                                else
                                {
                                    pt1 = new PointF(at.Position.X + moveVector.X, at.Position.Y + moveVector.Y);
                                    pt2 = new PointF(at.Neighbours[i].Position.X + moveVector.X, at.Neighbours[i].Position.Y + moveVector.Y);
                                }
                                
                                g.DrawLine(new Pen(Color.Black), pt1, pt2);
                            }
                        }
                        if (DrawAtomCircle)
                        {
                            switch (at.ToString())
                            {
                                case "C": roundColor = Color.Black; break;
                                case "N": roundColor = Color.Indigo; break;
                                case "O": roundColor = Color.Red; break;
                            }
                            g.FillEllipse(new SolidBrush(roundColor), at.Position.X - r / 2, at.Position.Y - r / 2, r, r);
                        }
                        
                        if (ShowAtomNumbers)
                        {
                            SizeF s = g.MeasureString(at.Index.ToString(), font);
                            g.DrawString(at.Index.ToString(), font, new SolidBrush(Color.Black), at.Position.X - s.Width, at.Position.Y - s.Height);
                        }
                        
                    }
                }
            }
            return bm;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
