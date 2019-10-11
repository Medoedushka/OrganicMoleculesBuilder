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
        List<Atom> molecule = new List<Atom>();

        public Molecule(string name)
        {
            Name = name;
        }

        public void AddAtom(Atom newAtom, int order, int pos)
        {
            if (order > newAtom.Valence) throw new ArgumentOutOfRangeException("order", "order > valence", "Валентность не может быть меньше указаного порядка связи!");

            if (molecule.Count == 0) molecule.Add(newAtom);
            else
            {
                for(int i = 0; i < molecule.Count; i++)
                {
                    if (molecule[i].Index == pos)
                    {
                        int freeBons = 0;
                        for(int j = 0; j < molecule[i].Neighbours.Length; j++)
                        {
                            if (molecule[i].Neighbours[j] == null) freeBons++;
                        }
                        if (freeBons >= order)
                        {
                            int k = 0;
                            for (int j = 0; j < molecule[i].Neighbours.Length; j++)
                            {
                                if (molecule[i].Neighbours[j] == null && k < order)
                                {
                                    //Atom temp = molecule[i];
                                    molecule[i].Neighbours[j] = newAtom;
                                    newAtom.Neighbours[j] = molecule[i];
                                    k++;
                                }
                              
                            }
                            molecule.Add(newAtom);
                            return;
                        }
                        else throw new ArgumentOutOfRangeException("freeBonds", "order > freeBonds", "У атома с индексом " + pos + " недостаточно свободных валентностей для образования связи!");
                    }
                }
            }
        }
        
        public Bitmap ReturnPic(int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bm))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                foreach(Atom at in molecule)
                {
                    for(int i = 0; i < at.Neighbours.Length; i++)
                    {
                        if (at.Neighbours[i] != null)
                            g.DrawLine(new Pen(Color.Black), at.Position, at.Neighbours[i].Position);
                    }
                }
            }
            return bm;
        }
    }
}
