using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MoleculesBuilder
{
    public enum Order
    {
        First,
        Second,
        Third
    }

    public class Bond
    {
        public Atom A { get; set; }
        public Atom B { get; set; }
        public PointF BondCenter { get; set; }
        public Order Order { get; set; }
        public bool InverseBond { get; set; }

        public Bond(Atom a, Atom b, Order _order)
        {
            A = a;
            B = b;
            Order = _order;
            BondCenter = new PointF((A.Position.X + B.Position.X) / 2, (A.Position.Y + B.Position.Y) / 2);
        }

        public void DrawBond(Graphics g)
        {
            if (Order == Order.First)
                g.DrawLine(new Pen(Color.Black), A.Position, B.Position);
            else if (Order == Order.Second)
            {
                int d = 5;
                PointF pt1, pt2;
                PointF vector = new PointF(B.Position.X - A.Position.X, B.Position.Y - A.Position.Y);
                double n_y = vector.X * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                double n_x = -vector.Y * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                PointF moveVector = new PointF((float)n_x, (float)n_y);
                int p = 1;
                if (InverseBond)
                {
                    pt1 = new PointF(A.Position.X + p * moveVector.X + vector.X * 0.9f, A.Position.Y + p * moveVector.Y + vector.Y * 0.9f);
                    pt2 = new PointF(B.Position.X + p * moveVector.X - vector.X * 0.9f, B.Position.Y + p * moveVector.Y - vector.Y * 0.9f);
                }
                else
                {
                    pt1 = new PointF(A.Position.X - p * moveVector.X + vector.X * 0.9f, A.Position.Y - p * moveVector.Y + vector.Y * 0.9f);
                    pt2 = new PointF(B.Position.X - p * moveVector.X - vector.X * 0.9f, B.Position.Y - p * moveVector.Y - vector.Y * 0.9f);
                }
                g.DrawLine(new Pen(Color.Black), A.Position, B.Position);
                g.DrawLine(new Pen(Color.Black), pt1, pt2);
            }
        }
    }
}
