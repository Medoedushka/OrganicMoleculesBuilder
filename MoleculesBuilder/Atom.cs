﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoleculesBuilder
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
        private Element type;
        public Element Type
        {
            get { return type; }
            set
            {
                type = value;
                AtomWeight = (int)value;
                
            }
        }

        public double AtomWeight { get; private set; }
        public PointF Position { get; set; }
        public Atom[] Neighbours;

        public Atom(Element type, int valence, int ind, PointF pos)
        {
            this.type = type;
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

        public void ApdateValence(int val)
        {
            Atom[] temp = new Atom[val];
            for(int i = 0; i < Neighbours.Length; i++)
            {
                if (Neighbours[i] != null) temp[i] = Neighbours[i];
            }
            Neighbours = temp;
            temp = null;
            Valence = val;
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
