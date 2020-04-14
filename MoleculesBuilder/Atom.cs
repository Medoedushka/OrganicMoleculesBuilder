using System;
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
        public string Label
        {
            get
            {
                string symbol = "";
                int hidrNum = 0;
                PointF bondVector = new PointF();
                for (int i = 0; i < Valence; i++)
                {
                    if (Neighbours[i] == null)
                        hidrNum++;
                    if (Neighbours[i] != null)
                    {
                        bondVector = new PointF(Position.X - Neighbours[i].Position.X,
                            Position.Y - Neighbours[i].Position.Y);
                    }
                }
                if ((bondVector.X < 0 && bondVector.Y < 0) || (bondVector.X < 0 && bondVector.Y > 0) || (bondVector.X < 0 && bondVector.Y == 0))
                {
                    symbol = hidrNum > 1 ? "H_{" + $"{hidrNum}" + "}" + ToString() : (hidrNum == 0 ? "" : "H") + ToString();
                }
                else symbol = hidrNum > 1 ? ToString() + "H_{" + $"{hidrNum}" + "}" : ToString() + (hidrNum == 0 ? "" : "H");
                return symbol;
            }
            set
            {
                Label = value;   
            }
        }
        public Font LabelFont { get; set; }

        public Atom(Element type, int valence, int ind, PointF pos)
        {
            this.type = type;
            Valence = valence;
            Neighbours = new Atom[Valence];
            Index = ind;
            Position = pos;
            LabelFont = new Font("Arial", 10);

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

        public int GetFreeBonds()
        {
            int count = 0;
            foreach (Atom atom in Neighbours)
            {
                if (atom == null)
                    count++;
            }
            return count;
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
