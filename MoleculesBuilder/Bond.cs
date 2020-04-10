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

    public enum BondType
    {
        Default,
        Wedget,
        HashedWedget,
        Wavy
    }

    public class Bond
    {
        public Atom A { get; set; }
        public Atom B { get; set; }
        public PointF BondCenter {
            get
            {
                return new PointF((A.Position.X + B.Position.X) / 2, (A.Position.Y + B.Position.Y) / 2);
            } }
        public BondType BondType { get; set; }
        public Order Order { get; set; }
        public bool InverseBond { get; set; }

        public Bond(Atom a, Atom b, Order _order)
        {
            A = a;
            B = b;
            Order = _order;
            BondType = BondType.Default;
        }

        public Bond()
        {
            A = null;
            B = null;
            Order = Order.First;
            BondType = BondType.Default;
        }

        public void DrawBond(Graphics g)
        {
            if (Order == Order.First)
            {
                if (BondType == BondType.Wedget)
                {
                    int d = 5;
                    PointF pt1, pt2;
                    PointF vector = new PointF(B.Position.X - A.Position.X, B.Position.Y - A.Position.Y);
                    double n_y = vector.X * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                    double n_x = -vector.Y * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                    PointF moveVector = new PointF((float)n_x, (float)n_y);
                    PointF[] draw;
                    int p = 1;
                    if (InverseBond)
                    {
                        pt1 = new PointF(A.Position.X + p * moveVector.X, A.Position.Y + p * moveVector.Y);
                        pt2 = new PointF(A.Position.X - p * moveVector.X, A.Position.Y - p * moveVector.Y);
                        draw = new PointF[] { pt1, pt2, B.Position };
                    }
                    else
                    {
                        pt1 = new PointF(B.Position.X - p * moveVector.X, B.Position.Y - p * moveVector.Y);
                        pt2 = new PointF(B.Position.X + p * moveVector.X, B.Position.Y + p * moveVector.Y);
                        draw = new PointF[] { pt1, pt2, A.Position };
                    }
                    g.FillPolygon(new SolidBrush(Color.Black), draw);
                }
                else g.DrawLine(new Pen(Color.Black, 1), A.Position, B.Position);
            }   
            else
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
                g.DrawLine(new Pen(Color.Black, 1), A.Position, B.Position);
                g.DrawLine(new Pen(Color.Black, 1), pt1, pt2);

                if (Order == Order.Third)
                {
                    p = -1;
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
                    g.DrawLine(new Pen(Color.Black, 1), pt1, pt2);
                }
            }
        }
    }
}
