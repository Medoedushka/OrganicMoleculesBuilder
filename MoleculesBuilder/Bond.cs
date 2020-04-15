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
        First = 1, 
        Second = 2,
        Third = 3
    }

    public enum BondType
    {
        Default,
        Wedget,
        HashedWedget,
        Wavy,
        Dashed
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
            int d = 5;
            PointF pt1, pt2;
            PointF vector = new PointF(B.Position.X - A.Position.X, B.Position.Y - A.Position.Y);
           double n_y = vector.X * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            double n_x = -vector.Y * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            PointF moveVector = new PointF((float)n_x, (float)n_y);

            if (Order == Order.First)
            {
                if (BondType == BondType.Wedget)
                {
                    n_y = vector.X * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                    n_x = -vector.Y * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
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
                else if (BondType == BondType.HashedWedget)
                {
                    if (InverseBond)
                    {
                        for (int n = 1; n <= 10; n++)
                        {
                            n_y = vector.X * n / (2 * Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y));
                            n_x = -vector.Y * n / (2 * Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y));
                            moveVector = new PointF((float)n_x, (float)n_y);
                            PointF pt = new PointF(A.Position.X + vector.X * 1 / 10 * n, A.Position.Y + vector.Y * 1 / 10 * n);
                            g.DrawLine(new Pen(Color.Black), new PointF(pt.X - moveVector.X, pt.Y - moveVector.Y),
                                new PointF(pt.X + moveVector.X, pt.Y + moveVector.Y));
                        }
                    }
                    else
                    {
                        for (int n = 1; n <= 10; n++)
                        {
                            n_y = vector.X * n / (2 * Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y));
                            n_x = -vector.Y * n / (2 * Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y));
                            moveVector = new PointF((float)n_x, (float)n_y);
                            PointF pt = new PointF(B.Position.X - vector.X * 1 / 10 * n, B.Position.Y - vector.Y * 1 / 10 * n);
                            g.DrawLine(new Pen(Color.Black), new PointF(pt.X - moveVector.X, pt.Y - moveVector.Y),
                                new PointF(pt.X + moveVector.X, pt.Y + moveVector.Y));
                        }
                    }

                }
                else if (BondType == BondType.Wavy)
                {
                    double d1 = vector.Y / vector.X;
                    double vectorAng = Math.Atan(d1) * 180 / Math.PI;
                    if (vector.X < 0) vectorAng += 180;
                    double step = Molecule.L / 100;
                    PointF[] temp = new PointF[101];
                    for (int k = 0; k <= 100; k++)
                    {
                        float x = k * (float)step;
                        float y = 4 * (float)Math.Sin(10 * Math.PI / Molecule.L * x);
                        PointF newVec = Molecule.RotateVector(vectorAng, new PointF(x, y));
                        temp[k].X = A.Position.X + newVec.X;
                        temp[k].Y = A.Position.Y +  newVec.Y;
                    }
                    g.DrawLines(Pens.Black, temp);
                }
                else if (BondType == BondType.Dashed)
                {
                    Pen pen = new Pen(Color.Black);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pen.DashPattern = new float[] { 4, 2 };
                    g.DrawLine(pen, A.Position, B.Position);
                }
                else g.DrawLine(new Pen(Color.Black, 1), A.Position, B.Position);
            }   
            else
            {
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
