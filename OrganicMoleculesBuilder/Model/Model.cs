using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MoleculesBuilder;
using OrganicMoleculesBuilder.Viewer;

namespace OrganicMoleculesBuilder.Model
{

    public class BuilderModel
    {
        List<Molecule> Molecules { get; set; }
        string path = "";
        Molecule crrMolecule;
        Atom founAtom;
        Bond foundBond;
        int[] angles;
        int angleCounter = 0;
        int orderPos = 1;

        public BuilderModel()
        {
            Molecules = new List<Molecule>();
            angles = new int[] { 0, 30, 60, 90, 120, 150, 180, -150, -120, -90, -60, -30 };
        }

        public Image TestRot()
        {
            Rectangle r = Molecule.GetRectangle(crrMolecule);

            return crrMolecule.Image;
        }

        public void DrawSolidBond(PictureBox pictureBox, PointF pos)
        {
            string str = "";
            if (founAtom == null)
            {
                Molecule molecule = new Molecule(Convert.ToString(Molecules.Count + 1), path);
                Molecules.Add(molecule);
                crrMolecule = molecule;
            }
            if (founAtom != null || crrMolecule.atoms.Count == 0)
            {
                if (crrMolecule.atoms.Count == 0)
                {
                    str = $"Add Et at {pos.X};{pos.Y} 0";
                    Molecule.RunCommand(ref crrMolecule, str, pictureBox.Width, pictureBox.Height);
                }
                else
                {
                    str = $"Add Me at {founAtom.Index} {angles[angleCounter]}";
                    angleCounter++;
                    if (angleCounter == angles.Length)
                        angleCounter = 0;

                    Molecule.RunCommand(ref crrMolecule, str, pictureBox.Width, pictureBox.Height);
                }
            }
        }

        public void ChangeOrder(PictureBox pictureBox, int order)
        {
            string str;
            if (foundBond != null)
            {
                str = $"Connect {foundBond.A.Index} {foundBond.B.Index} by {order}";
                Molecule.RunCommand(ref crrMolecule, str, pictureBox.Width, pictureBox.Height);
            }
        }
        public void ChangeBondType(PictureBox pictureBox, ToolType type)
        {
            string str = "";
            if (foundBond != null)
            {
                if (type == ToolType.WedgetBond)
                    str = $"Connect {foundBond.A.Index} {foundBond.B.Index} by w";
                else if (type == ToolType.HashedWedgetBond)
                    str = $"Connect {foundBond.A.Index} {foundBond.B.Index} by hw";
                else if (type == ToolType.DashedBond)
                    str = $"Connect {foundBond.A.Index} {foundBond.B.Index} by dashed";
                else if (type == ToolType.WavyBond)
                    str = $"Connect {foundBond.A.Index} {foundBond.B.Index} by wavy";
                Molecule.RunCommand(ref crrMolecule, str, pictureBox.Width, pictureBox.Height);
            }
        }

        public void SearchAtomBonds(PointF pos)
        {
            foreach (Molecule mol in Molecules)
            {
                foreach (Atom a in mol.atoms)
                {
                    if (Math.Abs(a.Position.X - pos.X) <= 5 && Math.Abs(a.Position.Y - pos.Y) <= 5)
                    {
                        founAtom = a;
                        crrMolecule = mol;
                        return;
                    }
                    else founAtom = null;
                }
                foreach (Bond b in mol.bonds)
                {
                    if (Math.Abs(b.BondCenter.X - pos.X) <= 5 && Math.Abs(b.BondCenter.Y - pos.Y) <= 5)
                    {
                        foundBond = b;
                        crrMolecule = mol;
                        return;
                    }
                    else foundBond = null;
                }
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
            if (Molecules.Count == 0)
            {
                g.Clear(Color.White);
            }
        }

        public void DeleteSelectedAtom(PictureBox pictureBox)
        {
            if (founAtom != null)
            {
                string str = $"Delete {founAtom.Index}";
                Molecule.RunCommand(ref crrMolecule, str, pictureBox.Width, pictureBox.Height);
            }
            if (crrMolecule.atoms.Count == 0)
                Molecules.Remove(crrMolecule);
        }

        public void RotateSub(PictureBox pictureBox, bool direction)
        {
            if (direction == true)
            {
                angleCounter++;
                if (angleCounter == angles.Length)
                    angleCounter = 0;
            }
            else
            {
                angleCounter--;
                if (angleCounter < 0)
                    angleCounter = angles.Length - 1;
                
            }

            int secInd = 0;
            foreach (Bond b in crrMolecule.bonds)
            {
                if (b.A.Index == crrMolecule.atoms[crrMolecule.atoms.Count - 1].Index)
                    secInd = b.B.Index;
                if (b.B.Index == crrMolecule.atoms[crrMolecule.atoms.Count - 1].Index)
                    secInd = b.A.Index;
            }

            crrMolecule.atoms[crrMolecule.atoms.Count - 1].Position = new PointF(crrMolecule.atoms[secInd - 1].Position.X,
            (float)(crrMolecule.atoms[secInd - 1].Position.Y - Molecule.L));

            string str = $"Rotate {crrMolecule.atoms[crrMolecule.atoms.Count - 1].Index} base {secInd} {angles[angleCounter]}";
            Molecule.RunCommand(ref crrMolecule, str, pictureBox.Width, pictureBox.Height);
        }

        public void InsertAtom(string atom, PictureBox pictureBox)
        {
            string path = "";
            if (founAtom != null)
            {
                path = $"Insert {atom} {founAtom.Index}";
                if (!string.IsNullOrEmpty(path))
                    Molecule.RunCommand(ref crrMolecule, path, pictureBox.Width, pictureBox.Height);
            }
        }

        public void DrawMolecules(PictureBox picture)
        {
            Bitmap bm = new Bitmap(picture.Width, picture.Height);
            using (Graphics g = Graphics.FromImage(bm))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                foreach (Molecule mol in Molecules)
                {
                    g.DrawImage(mol.Image, Molecule.GetRectangle(mol).Location);
                }
            }
            picture.Image = bm;
        }
    }
}
