using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicMoleculesBuilder
{
    public enum Element
    {
        H = 1,
        C = 12,
        O = 16,
        N = 14,
        S = 32,
        P = 31,
        F = 19,
        Cl, 
        Br = 80,
        I = 127
    }
    public class Atom
    {
        public int Index { get; set; }
        public int Valence { get; set; }
        public Element Type { get; set; }
        public double AtomWeight { get; set; }
        public PointF Position { get; set; }
        public Atom[] Neighbours;

        public Atom(Element type, int valence, int ind, PointF pos)
        {
            Type = type;
            Valence = valence;
            Neighbours = new Atom[Valence];
            Index = ind;
            Position = pos;

            if (Type.ToString() == "Cl") AtomWeight = 35.5;
            else AtomWeight = (int)Type;

            for (int i = 0; i < Valence; i++)
            {
                Neighbours[i] = null;
            }

        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
