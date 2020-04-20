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
        string _label;
        public string Label
        {
            get
            {
                string symbol = "";
                int hidrNum = 0;
                for (int i = 0; i < Valence; i++)
                {
                    if (Neighbours[i] == null)
                        hidrNum++;
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
                _label = value;   
            }
        }
        public Font LabelFont { get; set; }
        public PointF LabelPosition
        {
            get
            {
                PointF pt;
                string temp = Label.Replace("{", "").Replace("_", "").Replace("}", "");
                SizeF size = new SizeF();
                using (Graphics g = Graphics.FromImage(new Bitmap(1, 1))) { size = g.MeasureString(temp, LabelFont); }
                if (bondVector.X == 0 && bondVector.Y == 0)
                    return new PointF(Position.X - size.Width / 2, Position.Y - size.Height / 2);
                else if (bondVector.X > 0 && bondVector.Y < 0)
                    return new PointF(Position.X, Position.Y - size.Height / 2);
                else if (bondVector.X > 0 && bondVector.Y > 0)
                    return new PointF(Position.X, Position.Y);
                else if (bondVector.X < 0 && bondVector.Y < 0)
                    return new PointF(Position.X - size.Width, Position.Y - size.Height / 2);
                else if (bondVector.X < 0 && bondVector.Y > 0)
                    return new PointF(Position.X - size.Width, Position.Y);
                else if (bondVector.X == 0 && bondVector.Y > 0)
                    return new PointF(Position.X - size.Width / 4, Position.Y);
                else if (bondVector.X == 0 && bondVector.Y < 0)
                    return new PointF(Position.X - size.Width / 4, Position.Y - size.Height);
                else if (bondVector.X > 0 && bondVector.Y == 0)
                    return new PointF(Position.X, Position.Y - size.Height / 2);
                else //(bondVector.X < 0 && bondVector.Y == 0)
                    return new PointF(Position.X - size.Width, Position.Y - size.Height / 2);
            }
        }

        // Вектор связи дял правильного позиционирования положения лэйбла.
        private PointF bondVector {
            get
            {
                int counter = 0;
                int k = 0;
                for (int i = 0; i < Neighbours.Length; i++)
                {
                    if (Neighbours[i] == null)
                        counter++;
                    else k = i;
                }
                if (Valence - counter > 1)
                    return new PointF(0, 0);
                else if ((Valence - counter) == 1)
                {
                    return new PointF(Position.X - Neighbours[k].Position.X,
                                Position.Y - Neighbours[k].Position.Y);
                }
                else return new PointF();
            }
        }
        
        public Atom(Element type, int valence, int ind, PointF pos)
        {
            this.type = type;
            Valence = valence;
            Neighbours = new Atom[Valence];
            Index = ind;
            Position = pos;
            LabelFont = new Font("Times New Roman", 10);
            Label = "None";
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
            int shift = 0;
            for(int i = 0; i < Neighbours.Length; i++)
            {
                if (Neighbours[i] != null)
                    temp[i - shift] = Neighbours[i];
                else shift++;
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
