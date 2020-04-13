using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoleculesBuilder.Quantitative
{
    struct Elements
    {
        public string Name { get; set; }
        public double Weight { get; set; }
    }

    /// <summary>
    /// Класс, предоставляющий методы для количественной характеристики молекулы.
    /// </summary>
    public static class MoleculesProperties
    {
        /// <summary>
        /// Рассчитывает молекулярную массу передаваемой молекулы.
        /// </summary>
        /// <param name="crrMol"></param>
        /// <returns></returns>
        public static double CountMolecularWeight(Molecule crrMol)
        {
            double weight = 0;
            foreach (Atom at in crrMol.atoms)
            {
                int nH = 0;
                foreach (Atom n in at.Neighbours)
                {
                    if (n == null) nH++;
                }
                weight += nH + at.AtomWeight;
            }
            return weight;
        }

        public static string GetAtomPercents(Molecule crrMol)
        {
            Dictionary<Element, int> comp = new Dictionary<Element, int>();
            double weight = CountMolecularWeight(crrMol);
            foreach(Atom at in crrMol.atoms)
            {
                foreach (Atom n in at.Neighbours)
                {
                    if (n == null)
                    {
                        if (comp.ContainsKey(Element.H)) comp[Element.H]++;
                        else comp.Add(Element.H, 1);
                    }
                }
                if (comp.ContainsKey(at.Type)) comp[at.Type]++;
                else comp.Add(at.Type, 1);
            }
            string res = "";
            foreach(KeyValuePair<Element, int> pair in comp)
            {
                res += pair.Key + ":" + Math.Round(pair.Value * (int)pair.Key / weight * 100, 4) + "\n"; 
            }
            res = res.Remove(res.Length - 1, 1);
            return res;
        }

        static Elements[] elements = new Elements[68];
        static void FillingElements()
        {
            #region s-элементы
            #region щелочные металлы + H
            elements[0].Name = "H";
            elements[0].Weight = 1;

            elements[1].Name = "Li";
            elements[1].Weight = 7;

            elements[2].Name = "Na";
            elements[2].Weight = 23;

            elements[3].Name = "K";
            elements[3].Weight = 39;

            elements[4].Name = "Rb";
            elements[4].Weight = 85;

            elements[5].Name = "Cs";
            elements[5].Weight = 133;

            elements[6].Name = "Fr";
            elements[6].Weight = 39;
            #endregion
            #region щелочноземельные элементы
            elements[7].Name = "Be";
            elements[7].Weight = 9;

            elements[8].Name = "Mg";
            elements[8].Weight = 24;

            elements[9].Name = "Ca";
            elements[9].Weight = 40;

            elements[10].Name = "Sr";
            elements[10].Weight = 88;

            elements[11].Name = "Ba";
            elements[11].Weight = 137;
            #endregion
            #endregion
            #region d-элементы
            elements[12].Name = "Sc";
            elements[12].Weight = 45;

            elements[13].Name = "Ti";
            elements[13].Weight = 48;

            elements[14].Name = "V";
            elements[14].Weight = 51;

            elements[15].Name = "Cr";
            elements[15].Weight = 52;

            elements[16].Name = "Mn";
            elements[16].Weight = 55;

            elements[17].Name = "Fe";
            elements[17].Weight = 56;

            elements[18].Name = "Co";
            elements[18].Weight = 59;

            elements[19].Name = "Ni";
            elements[19].Weight = 59;

            elements[20].Name = "Cu";
            elements[20].Weight = 64;

            elements[21].Name = "Zn";
            elements[21].Weight = 65;

            elements[22].Name = "Cu";
            elements[22].Weight = 64;

            elements[23].Name = "Y";
            elements[23].Weight = 89;

            elements[24].Name = "Zr";
            elements[24].Weight = 91;

            elements[25].Name = "Nb";
            elements[25].Weight = 93;

            elements[26].Name = "Mo";
            elements[26].Weight = 96;

            elements[27].Name = "Tc";
            elements[27].Weight = 98;

            elements[28].Name = "Ru";
            elements[28].Weight = 101;

            elements[29].Name = "Rh";
            elements[29].Weight = 103;

            elements[30].Name = "Pd";
            elements[30].Weight = 106;

            elements[31].Name = "Ag";
            elements[31].Weight = 108;

            elements[32].Name = "Cd";
            elements[32].Weight = 112;

            elements[33].Name = "La";
            elements[33].Weight = 139;

            elements[34].Name = "Hf";
            elements[34].Weight = 178;

            elements[35].Name = "Ta";
            elements[35].Weight = 181;

            elements[36].Name = "W";
            elements[36].Weight = 184;

            elements[37].Name = "Re";
            elements[37].Weight = 186;

            elements[38].Name = "Os";
            elements[38].Weight = 190;

            elements[39].Name = "Ir";
            elements[39].Weight = 192;

            elements[40].Name = "Pt";
            elements[40].Weight = 195;

            elements[41].Name = "Au";
            elements[41].Weight = 197;

            elements[42].Name = "Hg";
            elements[42].Weight = 201;

            elements[43].Name = "Ac";
            elements[43].Weight = 227;
            #endregion
            #region p-элементы
            elements[44].Name = "B";
            elements[44].Weight = 11;

            elements[45].Name = "Al";
            elements[45].Weight = 27;

            elements[46].Name = "Ga";
            elements[46].Weight = 70;

            elements[47].Name = "In";
            elements[47].Weight = 115;

            elements[48].Name = "Tl";
            elements[48].Weight = 204;

            elements[49].Name = "C";
            elements[49].Weight = 12;

            elements[50].Name = "Si";
            elements[50].Weight = 28;

            elements[51].Name = "Ge";
            elements[51].Weight = 73;

            elements[52].Name = "Sn";
            elements[52].Weight = 119;

            elements[53].Name = "Pb";
            elements[53].Weight = 207;

            elements[54].Name = "N";
            elements[54].Weight = 14;

            elements[55].Name = "P";
            elements[55].Weight = 31;

            elements[56].Name = "As";
            elements[56].Weight = 75;

            elements[57].Name = "Sb";
            elements[57].Weight = 122;

            elements[58].Name = "Bi";
            elements[58].Weight = 209;

            elements[59].Name = "O";
            elements[59].Weight = 16;

            elements[60].Name = "S";
            elements[60].Weight = 32;

            elements[61].Name = "Se";
            elements[61].Weight = 79;

            elements[62].Name = "Te";
            elements[62].Weight = 128;

            elements[63].Name = "Po";
            elements[63].Weight = 209;
            #region галогены
            elements[64].Name = "F";
            elements[64].Weight = 19;

            elements[65].Name = "Cl";
            elements[65].Weight = 35.5;

            elements[66].Name = "Br";
            elements[66].Weight = 80;

            elements[67].Name = "I";
            elements[67].Weight = 127;
            #endregion
            #endregion
        }
        /// <summary>
        /// Рассчитывает молекулярную массу вещества по формуле.
        /// </summary>
        /// <param name="formula">химическая формула вещества. Например: K2Cr2O7.</param>
        /// <returns></returns>
        public static double CountMolecularWeight(string formula)
        {
            FillingElements();

            double mw = 0;
            int ii = 0;
            string form = formula + ".";
            string elem = "";
            while (form[ii] != '.')
            {

                if ((int)form[ii] > 47 && (int)form[ii] < 58)
                {
                    mw += CountMas(elem) * ((int)form[ii] - 48);
                    elem = "";
                }
                else
                {
                    if ((int)form[ii] >= 65 && (int)form[ii] <= 90 && ii > 0)
                    {
                        mw += CountMas(elem); elem = ""; elem += form[ii];
                    }
                    else elem += form[ii];
                }

                ii++;
            }
            if ((int)form[ii - 1] >= 65 && (int)form[ii - 1] <= 122) mw += CountMas(elem);
            return mw;
        }
        private static double CountMas(string words)
        {

            double molweight = 0; bool fl = false;

            foreach (Elements e in elements)
            {
                if (words == e.Name)
                {
                    molweight = e.Weight;
                    fl = true;
                }

            }
            if (fl) return molweight;
            else if (words == "")
                return 0;
            else { throw new Exception("Передаваемый элемент не существует."); }
        }
    }
}
