using System;
using System.Drawing;
using System.Windows.Forms;
using MoleculesBuilder;

namespace OrganicMoleculesBuilder.Model
{

    public class BuilderModel
    {
        Molecule crrMolecule;
        Atom founAtom;
        Bond foundBond;
        int[] angles;
        int angleCounter = 0;
        int orderPos = 1;

        public BuilderModel()
        {
            crrMolecule = new Molecule("name", "");
            crrMolecule.ShowAtomNumbers = false;
            angles = new int[] { 0, 30, 60, 90, 120, 150, 180, -150, -120, -90, -60, -30 };
        }

        public Image DrawSolidBond(PictureBox pictureBox, PointF pos)
        {
            string str = "";
            if (founAtom != null || crrMolecule.atoms.Count == 0)
            {
                if (crrMolecule.atoms.Count == 0)
                {
                    str = $"Add Et at {pos.X};{pos.Y} 0";
                    return Molecule.RunCommand(ref crrMolecule, str, pictureBox.Width, pictureBox.Height);
                }

                str = $"Add Me at {founAtom.Index} {angles[angleCounter]}";

                angleCounter++;
                if (angleCounter == angles.Length)
                    angleCounter = 0;

                return Molecule.RunCommand(ref crrMolecule, str, pictureBox.Width, pictureBox.Height);
            }
            return pictureBox.Image;
        }

        public Image ChangeOrder(PictureBox pictureBox, PointF pos)
        {
            string str;
            if (foundBond != null)
            {
                orderPos++;
                if (orderPos == 4)
                    orderPos = 1;
                str = $"Connect {foundBond.A.Index} {foundBond.B.Index} by {orderPos}";
                return pictureBox.Image = Molecule.RunCommand(ref crrMolecule, str, pictureBox.Width, pictureBox.Height);
            }
            return pictureBox.Image;
        }

        public void SearchAtomBonds(PointF pos)
        {
            foreach (Atom a in crrMolecule.atoms)
            {
                if (Math.Abs(a.Position.X - pos.X) <= 5 && Math.Abs(a.Position.Y - pos.Y) <= 5)
                {
                    founAtom = a;
                    break;
                }
                else founAtom = null;
            }
            foreach (Bond b in crrMolecule.bonds)
            {
                if (Math.Abs(b.BondCenter.X - pos.X) <= 5 && Math.Abs(b.BondCenter.Y - pos.Y) <= 5)
                {
                    foundBond = b;
                    break;
                }
                else foundBond = null;
            }
        }

        public void ApdateSelection(Graphics g)
        {
            if (founAtom != null)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(120, Color.Blue)), founAtom.Position.X - 5, founAtom.Position.Y - 5, 10, 10);
            }
            if (foundBond != null)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(120, Color.Red)), foundBond.BondCenter.X - 5, foundBond.BondCenter.Y - 5, 10, 10);
            }
            if (crrMolecule.atoms.Count > 2)
            {
                g.DrawRectangle(Pens.Blue, Molecule.GetRectangle(crrMolecule));
            }
        }
    }
}
