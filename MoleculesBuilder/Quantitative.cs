using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoleculesBuilder
{
    /// <summary>
    /// Класс, предоставляющих методы для количественной характеристики молекулы.
    /// </summary>
    public static class Quantitative
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
            return res;
        }
    }
}
