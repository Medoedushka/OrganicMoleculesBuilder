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
    }
}
