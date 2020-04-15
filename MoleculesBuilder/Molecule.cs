using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoleculesBuilder
{
    public class Molecule : ICloneable
    {
        public static string[] Keywords =
        {
            //"Create",
            "Add",
            "Insert",
            "at",
            "Rotate",
            "base",
            "Delete",
            "Connect",
            "inv",
            "by",
            "Circles",
            "Numbers",
            "Move",
            "Clear",
            "AddFromFile"
        };
        public static string[] Subs =
        {
            "Me",
            "Et",
            "O",
            "N",
            "F",
            "Cl",
            "Br",
            "I",
            "S"
        };
        public const double L = 35; // Длина связи C-C
        const double ANGLE = 120;//109.47; // Угол связи C-C
        const double K = Math.PI / 180;

        public string Name { get; set; }
        public List<Atom> atoms { get; set; }
        public List<Bond> bonds { get; set; }
        public bool ShowAtomNumbers { get; set; }
        public bool DrawAtomCircle { get; set; }
        public string MolecularPartsDir { get; set; }
        public Image Image { get => GetImage(); set => Image = value; }
        public string Pattern { get; private set; }

        public Molecule(string name, string dir)
        {
            Name = name;
            MolecularPartsDir = dir;
            atoms = new List<Atom>();
            bonds = new List<Bond>();
            ShowAtomNumbers = false;
            DrawAtomCircle = false;
        }
        public object Clone()
        {
            Molecule newMol = new Molecule(this.Name, this.MolecularPartsDir);
            newMol.DrawAtomCircle = this.DrawAtomCircle;
            newMol.ShowAtomNumbers = this.ShowAtomNumbers;
            string[] commands = this.Pattern.Split('\n');
            foreach(string c in commands)
            {
                if (c != "")
                {
                    //if (c.Contains("Rotate"))
                    //{
                    //    int secInd = 0;
                    //    foreach (Bond b in newMol.bonds)
                    //    {
                    //        if (b.A.Index == newMol.atoms[newMol.atoms.Count - 1].Index)
                    //            secInd = b.B.Index;
                    //        if (b.B.Index == newMol.atoms[newMol.atoms.Count - 1].Index)
                    //            secInd = b.A.Index;
                    //    }
                    //    newMol.atoms[newMol.atoms.Count - 1].Position = new PointF(newMol.atoms[secInd - 1].Position.X,
                    //            (float)(newMol.atoms[secInd - 1].Position.Y - Molecule.L));
                    //}
                    Molecule.RunCommand(ref newMol, c, this.Image.Width, this.Image.Height);
                }
                    
            }
            return newMol;
        }

        //Вспомагательные методы
        private static string AddStrings(string strToSum) 
        {
            double sum = 0;
            if (strToSum.Contains("+"))
            {
                string[] comp = strToSum.Split('+');
                for (int i = 0; i < comp.Length; i++)
                {
                    sum += double.Parse(comp[i]);
                }
            }
            else if (strToSum.Contains("-"))
            {
                string[] comp = strToSum.Split('-');
                if (comp[0] == "") sum = 0;
                else sum = double.Parse(comp[0]);

                for (int i = 1; i < comp.Length; i++)
                {
                    if (comp[i] == "") continue;
                    sum -= double.Parse(comp[i]);
                }
            }
            else throw new Exception("Неверная арифметическая операция!");
            
            return Convert.ToString(sum);
        }
        private static bool ContainsNotNumbers(string str, char exc = '*')
        {
            foreach(char c in str)
            {
                if (!char.IsNumber(c))
                {
                    if (c == exc) continue;
                    else return true;
                }
            }
            return false;
        }
        //Расчитывает точки для правильного n-угольника
        private static PointF[] DrawPoly(double l, PointF center, int n = 6)
        {
            PointF[] pt = new PointF[n];
            double R = l / (2 * Math.Sin(Math.PI / n));
            for (int i = 0; i < pt.Length; i++)
            {
                pt[i].X = (float)(R * Math.Sin(i * 360 / n * K) + center.X);
                pt[i].Y = (float)(R * Math.Cos(i * 360 / n * K) + center.Y);
            }

            return pt;
        }

        private Image GetImage()
        {
            Rectangle r = Molecule.GetRectangle(this);
            Bitmap bm = new Bitmap(r.Width, r.Height);
            bm.MakeTransparent();
            PointF moveVector = new PointF(-r.Location.X, -r.Location.Y);
            foreach(Atom a in atoms)
            {
                a.Position = new PointF(a.Position.X + moveVector.X, a.Position.Y + moveVector.Y);
            }
            bm = ReturnPic(bm.Width, bm.Height);
            foreach (Atom a in atoms)
            {
                a.Position = new PointF(a.Position.X - moveVector.X, a.Position.Y - moveVector.Y);
            }
            return bm;
        }

        //Вращает вектор по часовой стрелке для положительных значений аргумента ang
        public static PointF RotateVector(double ang, PointF vec)
        {
            float x = 0, y = 0;
            x = (float)(vec.X * Math.Cos(ang * K) - vec.Y * Math.Sin(ang * K));
            y = (float)(vec.X * Math.Sin(ang * K) + vec.Y * Math.Cos(ang * K));
            return new PointF(x, y);
        }

        //Вращение указанных вершин молекулы относительно базовой вершины на угол ang
        private static void RotateMolecularPart(Molecule crrMolecule, int[] rotInd, int baseInd, double ang)
        {
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

            //поиск координат атомов
            PointF[] rotPt = new PointF[rotInd.Length];
            PointF basePt = new PointF();
            foreach (Atom at in crrMolecule.atoms)
            {
                if (at.Index == baseInd)
                {
                    basePt = at.Position;
                    break;
                }
            }
            for (int i = 0; i < rotInd.Length; i++)
            {
                foreach (Atom at in crrMolecule.atoms)
                {
                    if (at.Index == rotInd[i])
                    {
                        PointF oldVector = new PointF(at.Position.X - basePt.X, at.Position.Y - basePt.Y);
                        PointF newVector = RotateVector(ang, oldVector);
                        at.Position = new PointF(basePt.X + newVector.X, basePt.Y + newVector.Y);
                        break;
                    }

                }
            }

             //Если при вращении заместитель совпадёт с же сущ. атомом, то атом-основание вращения соединяется с уже сущ. атомом.
            if (rotInd.Length == 1)
            {
                Atom oldAtom;
                if (crrMolecule.SameCoords(crrMolecule.atoms[rotInd[0] - 1], out oldAtom))
                {
                    // ничего не делать если одинарная связь уже существует
                    if (crrMolecule.ExistBond(oldAtom.Index, crrMolecule.atoms[baseInd - 1].Index))
                        return;
                    int ind = 0;
                    for(int i = 0; i < crrMolecule.atoms[baseInd - 1].Neighbours.Length; i++)
                    {
                        if (crrMolecule.atoms[baseInd - 1].Neighbours[i] == crrMolecule.atoms[rotInd[0] - 1])
                        {
                            crrMolecule.atoms[baseInd - 1].Neighbours[i] = null;
                            ind = i;
                        }
                    }
                    foreach (Bond b in crrMolecule.bonds)
                    {
                        if (b.A.Index == rotInd[0] || b.B.Index == rotInd[0])
                        {
                            crrMolecule.bonds.Remove(b);
                            break;
                        }
                    }
                    crrMolecule.atoms.Remove(crrMolecule.atoms[rotInd[0] - 1]);

                    crrMolecule.atoms[baseInd - 1].Neighbours[ind] = oldAtom;
                    for (int k = 0; k < oldAtom.Neighbours.Length; k++)
                    {
                        if (oldAtom.Neighbours[k] == null)
                        {
                            oldAtom.Neighbours[k] = crrMolecule.atoms[baseInd - 1];
                            Bond bond1 = new Bond(oldAtom, crrMolecule.atoms[baseInd - 1], Order.First);
                            crrMolecule.bonds.Add(bond1);
                            return;
                        }

                    }
                }
            }
        }

        //Вращение всех вершин молекулы относительно указанной точки _basePt.
        public static Bitmap RotateMolecularPart(PictureBox pictureBox, Molecule crrMolecule, PointF _basePt, double ang)
        {
            //поиск координат атомов
            PointF[] rotPt = new PointF[crrMolecule.atoms.Count];
            PointF basePt = _basePt;

            foreach (Atom at in crrMolecule.atoms)
            {
                PointF oldVector = new PointF(at.Position.X - basePt.X, at.Position.Y - basePt.Y);
                PointF newVector = RotateVector(ang, oldVector);
                at.Position = new PointF(basePt.X + newVector.X, basePt.Y + newVector.Y);
            }
            return crrMolecule.ReturnPic(pictureBox.Width, pictureBox.Height);
        }

        //Добавление нового заместителя в молекулу
        private static void DrawSub(Molecule crrMolecule, string type, string pos, double ang)
        {
            PointF targetPos = new PointF(0, 0); // Точка или индекс атома, к которому будет цепляться заместитель (целевой атома).
            PointF[] newVector = new PointF[1]; // Массив векторов, которые соответствуют атомам заместителя.
            PointF[] subPt = new PointF[1]; /* Массив точек атомов заместителя, который получается путём перемемещения 
                                                целевого атома на соотв етствующий вектор.*/

            int subPos; // Индекс атома, к которму будет цепляться заместитель.
            Element el = Element.C;
            int val = 4;

            // Поиск координат атома, к которому будет цепляться заместитель.
            if (pos.Contains(";")) // в случае добавление первого заместителя
            {
                string[] parts = pos.Split(';');

                /*Проверки на сумму или разность*/
                if (ContainsNotNumbers(parts[0])) parts[0] = AddStrings(parts[0]); 
                if (ContainsNotNumbers(parts[1])) parts[1] = AddStrings(parts[1]);
                targetPos = new PointF(float.Parse(parts[0]), float.Parse(parts[1]));
                subPos = 0;
            }
            else
            {
                if (ContainsNotNumbers(pos)) pos = AddStrings(pos);
                subPos = int.Parse(pos);
                foreach (Atom at in crrMolecule.atoms)
                {
                    if (at.Index == subPos)
                    {
                        targetPos = at.Position;
                        break;
                    }
                }
            }
           
            // Рассчёт расположения заместителя, который состоит из одного атома.
            if (type != "Et")
            {
                newVector = new PointF[1];
                newVector[0] = RotateVector(ang, new PointF(0, (float)-L));
                subPt = new PointF[2];
            }

            switch (type)
            {
                case "Me":
                    el = Element.C;
                    val = 4;
                    break;
                case "Et":
                    newVector = new PointF[2];
                    if (crrMolecule.atoms.Count > 0)
                    {
                        newVector[0] = RotateVector(ang, new PointF(0, (float)-L));
                        newVector[1] = RotateVector(ang, new PointF((float)(2 * L * Math.Sin(K * ANGLE / 2) * Math.Cos(Math.PI / 3)),
                           (float)(-2 * L * Math.Sin(K * ANGLE / 2) * Math.Sin(Math.PI / 3))));
                    }
                    else
                    {
                        newVector[0] = RotateVector(ang, new PointF(0, 0));
                        newVector[1] = RotateVector(60, new PointF(0, (float)-L));
                    }
                    subPt = new PointF[3];
                    el = Element.C;
                    val = 4;
                    break;
                case "O":
                    el = Element.O;
                    val = 2;
                    break;
                case "N":
                    el = Element.N;
                    val = 3;
                    break;
                case "F":
                    el = Element.F;
                    val = 1;
                    break;
                case "Cl":
                    el = Element.Cl;
                    val = 1;
                    break;
                case "Br":
                    el = Element.Br;
                    val = 1;
                    break;
                case "I":
                    el = Element.I;
                    val = 1;
                    break;
                case "S":
                    el = Element.S;
                    val = 2;
                    break;
                default:
                    if (type.Contains("cyclo-"))
                    {
                        string[] parts = type.Split('-');
                        int n = int.Parse(parts[1]);
                        PointF vec = new PointF(0, 2 * (float)L);
                        PointF[] MainAcyclicChain = DrawPoly(L, new PointF(targetPos.X - vec.X, targetPos.Y - vec.Y), n);
                        int p = subPos;
                        int s = 0;
                        int[] ind = new int[n];
                        for (int i = 0; i < MainAcyclicChain.Length; i++)
                        {
                            ind[i] = crrMolecule.atoms.Count + 1;
                            Atom atom = new Atom(Element.C, 4, crrMolecule.atoms.Count + 1, new PointF(MainAcyclicChain[i].X, MainAcyclicChain[i].Y));
                            crrMolecule.AddAtom(atom, 1, p);
                            
                            p = crrMolecule.atoms.Count;
                            if (s == 0) s = p;
                        }
                        crrMolecule.ConnectAtoms(s, p, 1);
                        RotateMolecularPart(crrMolecule, ind, subPos, ang);
                    }
                    return;
            }

            int prev = subPos;
            for (int i = 1; i < subPt.Length; i++)
            {
                // получение координат каждого атома в заместителе.
                subPt[i].X = targetPos.X + newVector[i - 1].X;
                subPt[i].Y = targetPos.Y + newVector[i - 1].Y;
                Atom atom = new Atom(el, val, crrMolecule.atoms.Count + 1, new PointF(subPt[i].X, subPt[i].Y));
                crrMolecule.AddAtom(atom, 1, prev); // Добавление атома в молекулу.
                prev = crrMolecule.atoms.Count;
            }


        }
        /// <summary>
        /// Возвращает прямоугольную область, которую занимает молекула.
        /// </summary>
        /// <param name="crrMolecule"></param>
        /// <returns></returns>
        public static Rectangle GetRectangle(Molecule crrMolecule)
        {
            Rectangle rectangleF;
            float minX = float.MaxValue, maxX = float.MinValue, minY = float.MaxValue, maxY = float.MinValue;
            int shift = 10;
            using (Graphics gr = Graphics.FromImage(new Bitmap(1, 1)))
            {
                foreach (Atom a in crrMolecule.atoms)
                {
                    SizeF size = gr.MeasureString(a.Label, a.LabelFont);
                    if ((a.Position.X - size.Width) < minX)
                    {
                        minX = a.Position.X - size.Width;
                    }
                    if ((a.Position.X + size.Width) > maxX)
                    {
                        maxX = a.Position.X + size.Width;
                    }

                    if ((a.Position.Y - size.Height) < minY)
                    {
                        minY = a.Position.Y - size.Height;
                    }
                    if ((a.Position.Y + size.Height) > maxY)
                    {
                        maxY = a.Position.Y + size.Height;
                    }
                }
            }
            return rectangleF = new Rectangle((int)minX - shift, (int)minY - shift, (int)(maxX - minX) + 2 * shift, (int)(maxY - minY) + 2 * shift);

        }
        /// <summary>
        /// Выполняет команду по отрисовке молекулы. Возвращает картинку с отрисованной молекулой.
        /// </summary>
        /// <param name="crrMolecule"></param>
        /// <param name="comm"></param>
        /// <param name="bmWidth"></param>
        /// <param name="bmHeight"></param>
        /// <returns></returns>
        public static Bitmap RunCommand(ref Molecule crrMolecule, string comm, int bmWidth, int bmHeight)
        {
            if (crrMolecule == null)
                throw new Exception("Молекула не создана!");

            Bitmap result = new Bitmap(bmWidth, bmHeight);
            crrMolecule.Pattern += comm + "\n";
            string command = comm.Trim(new char[] { '\n' });
            string[] el = command.Split(' '); // получение массива ключевых слов и передаваемых значений
            switch (el[0])
            {
                case "Add":
                    if (crrMolecule != null && el[2].ToLower() == "at")
                    {
                        /* el[3] содержит координату области построения в случае если молкула не содержит ни одного атома или
                         * индекс атома, к которому будет прицеплен заместитель.
                         * e[1] представляет заместитель или, если молекула не содержит ни одного атома, молекулу
                        */

                        /* Строка координат может содержать арифметические операции сложения/вычетания
                         * Здесь проверятеся наличие в строке координат символов, которые относятся к сложению/вычитанию.*/
                        if (ContainsNotNumbers(el[4])) el[4] = AddStrings(el[4]);

                        if (el[3].Contains(";") && crrMolecule.atoms.Count != 0)
                            throw new Exception("Начальная точка уже была построена.");
                        else DrawSub(crrMolecule, el[1], el[3], double.Parse(el[4]));
                    }
                    break;
                case "AddFromFile":
                    if (el[1][0] == '[' && el[1][el[1].Length - 1] == ']' && crrMolecule != null)
                    {
                        string path = el[1].Remove(0, 1);
                        path = path.Remove(path.Length - 1, 1);
                        if (!path.Contains(".txt")) path += ".txt";
                        if (!string.IsNullOrEmpty(crrMolecule.MolecularPartsDir))
                            path = crrMolecule.MolecularPartsDir + @"\" + path;
                        if (File.Exists(path))
                        {
                            FileStream read = new FileStream(path, FileMode.Open, FileAccess.Read);
                            using (StreamReader reader = new StreamReader(read))
                            {
                                string str = "";
                                List<string> commands = new List<string>();
                                string[] ind = null;
                                while (str != null)
                                {
                                    str = reader.ReadLine();
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        if (str.Contains("//"))
                                        {
                                            int num = str.IndexOf('/');
                                            str = str.Remove(num);
                                            if (str == "") continue;
                                        }
                                        if (str[0] == '(')
                                        {
                                            str = str.Remove(0, 1);
                                            str = str.Remove(str.Length - 1, 1);
                                            ind = str.Split(',');
                                            if (ind.Length != el.Length - 2) throw new Exception("Не хватает параметров!");
                                            continue;
                                        }
                                        if (str.Contains("{"))
                                        {
                                            string s = "";
                                            bool open = false;
                                            int k = 0;
                                            foreach (char c in str)
                                            {
                                                if (c == '{')
                                                {
                                                    open = true;
                                                    k++;
                                                }
                                                if (open) s += c;
                                                if (c == '}')
                                                {
                                                    open = false;
                                                    string temp = s.Remove(0, 1);
                                                    temp = temp.Remove(temp.Length - 1, 1);
                                                    int n = 0;
                                                    for (int i = 0; i < ind.Length; i++)
                                                    {
                                                        if (ind[i] == temp)
                                                        {
                                                            n = i;
                                                            break;
                                                        }
                                                    }
                                                    str = str.Replace(s, el[2 + n]);
                                                    s = "";
                                                }
                                            }
                                        }
                                        if (str != "")
                                            commands.Add(str);
                                    }
                                }
                                foreach (string c in commands)
                                {
                                    result = RunCommand(ref crrMolecule, c, bmWidth, bmHeight);
                                }
                            }
                            return result;
                        }
                        else throw new FileNotFoundException(path + " не найден.");
                    }
                    break;
                case "Rotate":
                    if (crrMolecule != null && el[2].ToLower() == "base")
                    {
                        int[] indexes;
                        if (el[1] == "all")
                        {
                            indexes = new int[crrMolecule.atoms.Count - 1];
                            for(int i = 0; i < indexes.Length; i++)
                            {
                                if (crrMolecule.atoms[i].Index != int.Parse(el[3])) indexes[i] = crrMolecule.atoms[i].Index;
                                else indexes[i] = crrMolecule.atoms[crrMolecule.atoms.Count - 1].Index;
                            }
                        }
                        else
                        {
                            string[] parts = el[1].Split(',');
                            indexes = new int[parts.Length];
                            for (int i = 0; i < indexes.Length; i++)
                            {
                                indexes[i] = int.Parse(parts[i]);
                            }
                        }

                        
                        RotateMolecularPart(crrMolecule, indexes, int.Parse(el[3]), double.Parse(el[4]));
                    }
                    break;
                case "Connect":
                    if (crrMolecule != null && el[3].ToLower() == "by")
                    {
                        if (ContainsNotNumbers(el[1])) el[1] = AddStrings(el[1]);
                        if (ContainsNotNumbers(el[2])) el[2] = AddStrings(el[2]);
                        if (char.IsDigit(el[4][0]))
                        {
                            Bond b1 = null;
                            crrMolecule.ExistBond(int.Parse(el[1]), int.Parse(el[2]), out b1);
                            if (b1 != null && el[4] == "2")
                                b1.InverseBond = !b1.InverseBond;
                            b1.BondType = BondType.Default;
                            crrMolecule.ConnectAtoms(int.Parse(el[1]), int.Parse(el[2]), int.Parse(el[4]));
                        }
                        else
                        {
                            Bond b1;
                            crrMolecule.ExistBond(int.Parse(el[1]), int.Parse(el[2]), out b1);
                            if (b1 != null && el[4] == "w")
                                b1.BondType = BondType.Wedget;
                            else if (b1 != null && el[4] == "hw")
                                b1.BondType = BondType.HashedWedget;
                            else if (b1 != null && el[4] == "wavy")
                                b1.BondType = BondType.Wavy;
                            else if (b1 != null && el[4] == "dashed")
                                b1.BondType = BondType.Dashed;
                            b1.Order = Order.First;

                            if (b1 != null && b1.BondType != BondType.Default)
                                b1.InverseBond = !b1.InverseBond;
                        }
                    }
                    break;
                case "Delete":
                    if (crrMolecule != null)
                    {
                        crrMolecule.RemoveAtom(int.Parse(el[1]));

                    }
                    break;
                case "Move":
                    if (crrMolecule != null)
                    {
                        string[] parts = el[1].Split(',');
                        float a, b;
                        if (float.TryParse(parts[0], out a) && float.TryParse(parts[1], out b) && parts.Length == 2)
                        {
                            PointF moveVector = new PointF(a, b);
                            foreach (Atom at in crrMolecule.atoms)
                            {
                                at.Position = new PointF(at.Position.X + moveVector.X, at.Position.Y + moveVector.Y);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Неверный синтаксис команды Move!", "Ошибка синтаксиса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                case "Clear":
                    if (crrMolecule != null)
                    {
                        string name = crrMolecule.ToString();
                        crrMolecule = null;
                        GC.Collect();
                        MessageBox.Show("Молекула " + name + " успешно удалена", "Удаление молекулы", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    break;
                case "Insert":
                    if (crrMolecule != null)
                    {
                        int val = int.Parse(el[1].Substring(el[1].IndexOf('(') + 1, 1));
                        string atom = el[1].Remove(el[1].IndexOf('('));
                        int ind = int.Parse(el[2]);
                        crrMolecule.InsertAtom(atom, val, ind);

                    }
                    break;
                case "Circles":
                    if (crrMolecule != null)
                    {
                        if (el[1] == "On")
                        {
                            crrMolecule.DrawAtomCircle = true;
                        }
                        else if (el[1] == "Off")
                            crrMolecule.DrawAtomCircle = false;


                    }
                    break;
                case "Numbers":
                    if (crrMolecule != null)
                    {
                        if (el[1] == "On")
                        {
                            crrMolecule.ShowAtomNumbers = true;
                        }
                        else if (el[1] == "Off")
                            crrMolecule.ShowAtomNumbers = false;

                    }
                    break;
                default:
                    throw new ArgumentException("Неверная команда.");
                    
            }
            
            result = crrMolecule?.ReturnPic(bmWidth, bmHeight);
            return result;
        }

        private void AddAtom(Atom newAtom, int order, int pos)
        {
            if (atoms.Count == 0) atoms.Add(newAtom);
            else
            {
                for (int i = 0; i < atoms.Count; i++)
                {
                    if (atoms[i].Index == pos)
                    {
                        int freeBons = 0; // поиск свободных валентностей у атома, к которому будет цепляться заместитель.
                        for (int j = 0; j < atoms[i].Neighbours.Length; j++)
                        {
                            if (atoms[i].Neighbours[j] == null) freeBons++;
                        }

                        if (freeBons >= order)
                        {
                            Atom oldAtom;
                            // Заполнение свободных валентностей целевого атома, с учётом порядка связи.
                            for (int j = 0; j < atoms[i].Neighbours.Length; j++)
                            {
                                if (atoms[i].Neighbours[j] == null)
                                {
                                    if (SameCoords(newAtom, out oldAtom))
                                    {
                                        // ничего не делать если одинарная связь уже существует
                                        if (ExistBond(oldAtom.Index, atoms[i].Index))
                                            return;

                                        atoms[i].Neighbours[j] = oldAtom;
                                        for (int k = 0; k < oldAtom.Neighbours.Length; k++)
                                        {
                                            if (oldAtom.Neighbours[k] == null)
                                            {
                                                oldAtom.Neighbours[k] = atoms[i];
                                                Bond bond1 = new Bond(oldAtom, atoms[i], Order.First);
                                                bonds.Add(bond1);
                                                return;
                                            }
                                                
                                        }
                                    }
                                    else
                                    {
                                        atoms[i].Neighbours[j] = newAtom;
                                        newAtom.Neighbours[0] = atoms[i];
                                        atoms.Add(newAtom);
                                        int num2 = Bonds(newAtom.Index, pos, true);
                                        Bond bond2 = new Bond(atoms[pos - 1], newAtom, Order.First);
                                        if (num2 == 1)
                                            bond2.Order = Order.First;
                                        else if (num2 == 2)
                                            bond2.Order = Order.Second;
                                        else bond2.Order = Order.Third;
                                        bonds.Add(bond2);
                                        return;
                                    }
                                }

                            }
                            int num = Bonds(newAtom.Index, pos, true);
                            Bond bond = new Bond(atoms[pos - 1], newAtom, Order.First);
                            if (num == 1)
                                bond.Order = Order.First;
                            else if (num == 2)
                                bond.Order = Order.Second;
                            else bond.Order = Order.Third;
                            bonds.Add(bond);
                        }
                        else throw new ArgumentOutOfRangeException("freeBonds", "order > freeBonds", "У атома с индексом " + pos + " недостаточно свободных валентностей для образования связи!");
                    }
                }
            }
        }

        private void ConnectAtoms(int firstInd, int secondInd, int order)
        {
            if (order >= 0 && order < 4)
            {
                if (order == 0) // сброс ссылок каждого атома из пары
                {
                    for (int i = 0; i < atoms[firstInd - 1].Neighbours.Length; i++)
                    {
                        if (atoms[firstInd - 1].Neighbours[i] == atoms[secondInd - 1])
                            atoms[firstInd - 1].Neighbours[i] = null;
                    }
                    for (int i = 0; i < atoms[secondInd - 1].Neighbours.Length; i++)
                    {
                        if (atoms[secondInd - 1].Neighbours[i] == atoms[firstInd - 1])
                            atoms[secondInd - 1].Neighbours[i] = null;
                    }
                    Bond b;
                    if (ExistBond(atoms[firstInd - 1].Index, atoms[secondInd - 1].Index, out b))
                    {
                        if (b != null)
                            bonds.Remove(b);
                    }

                    return;
                }

                int d = order - Bonds(firstInd, secondInd, true); // кол-во связей, которые нужно создать между атомами
                if (d > 0)
                {
                    // проверка кол-ва связей, которые нужно создать кол-ву свободных валентностей каждого атома.
                    if (d <= Bonds(firstInd, 0, false) && d <= Bonds(secondInd, 0, false))
                    {
                        // заполнение ссылок на атом firstInd
                        int count = 0;
                        for (int i = 0; i < atoms[firstInd - 1].Neighbours.Length; i++)
                        {
                            if (atoms[firstInd - 1].Neighbours[i] == null && count < d)
                            {
                                atoms[firstInd - 1].Neighbours[i] = atoms[secondInd - 1];
                                count++;
                            }

                        }
                        // заполнение ссылок на атом secondInd
                        count = 0;
                        for (int i = 0; i < atoms[secondInd - 1].Neighbours.Length; i++)
                        {
                            if (atoms[secondInd - 1].Neighbours[i] == null && count < d)
                            {
                                atoms[secondInd - 1].Neighbours[i] = atoms[firstInd - 1];
                                count++;
                            }

                        }
                        Bond bond;
                        if (!ExistBond(atoms[firstInd - 1].Index, atoms[secondInd - 1].Index, out bond))
                        {
                            bond = new Bond(atoms[firstInd - 1], atoms[secondInd - 1], Order.First);
                            bonds.Add(bond);
                        }

                        if (order == 1)
                            bond.Order = Order.First;
                        else if (order == 2)
                            bond.Order = Order.Second;
                        else bond.Order = Order.Third;
                    }
                }
                else // удалить лишние связи если d < 0
                {
                    if (Math.Abs(d) <= Bonds(firstInd, secondInd, true) && d <= Bonds(secondInd, firstInd, true))
                    {
                        int count = 0;
                        for (int i = 0; i < atoms[firstInd - 1].Neighbours.Length; i++)
                        {
                            if (atoms[firstInd - 1].Neighbours[i] == atoms[secondInd - 1] && count < Math.Abs(d))
                            {
                                atoms[firstInd - 1].Neighbours[i] = null;
                                count++;
                            }

                        }
                        count = 0;
                        for (int i = 0; i < atoms[secondInd - 1].Neighbours.Length; i++)
                        {
                            if (atoms[secondInd - 1].Neighbours[i] == atoms[firstInd - 1] && count < Math.Abs(d))
                            {
                                atoms[secondInd - 1].Neighbours[i] = null;
                                count++;
                            }

                        }
                        Bond bond;
                        if (!ExistBond(atoms[firstInd - 1].Index, atoms[secondInd - 1].Index, out bond))
                        {
                            bond = new Bond(atoms[firstInd - 1], atoms[secondInd - 1], Order.First);
                            bonds.Add(bond);
                        }

                        if (order == 1)
                            bond.Order = Order.First;
                        else if (order == 2)
                            bond.Order = Order.Second;
                        else bond.Order = Order.Third;
                    }
                }
                
            }       
        }
            
        private void RemoveAtom(int index)
        {
            Atom targetAtom = atoms[index - 1];
            // Удаление связей атома с индексом index и обнуление ссылок у соседей.
            foreach (Atom at in atoms)
            {
                List<Bond> delBonds = new List<Bond>();
                foreach (Bond b in bonds)
                {
                    if (b.A == atoms[index - 1] || b.B == atoms[index - 1])
                    {
                        delBonds.Add(b);
                    }

                }
                foreach (Bond bond in delBonds)
                {
                    bonds.Remove(bond);
                }
                delBonds = null;

                for (int i = 0; i < at.Neighbours.Length; i++)
                {
                    Atom checkAt = at.Neighbours[i];
                    if (at.Neighbours[i] == atoms[index - 1])
                        at.Neighbours[i] = null;
                }
            }

            // Поиск и удаление атомов, которые больше не имеют соседей.
            List<Atom> delAtoms = new List<Atom>();
            foreach (Atom a in atoms)
            {
                if (a.GetFreeBonds() == a.Valence)
                {
                    delAtoms.Add(a);
                }
            }
            foreach (Atom atom in delAtoms)
            {
                atoms.Remove(atom);
            }
            delAtoms = null;
            atoms.Remove(targetAtom); // Удаление целевого атома
            
            // Переназанчение индексов оставшихся атомов.
            int d = 1;
            foreach (Atom a in atoms)
            {
                a.Index = d;
                d++;
            }
            targetAtom = null;
            GC.Collect();
        }

        private void InsertAtom(string newAtom, int newAtomVal, int baseAtomInd)
        {
            int takenBonds = atoms[baseAtomInd - 1].Valence - Bonds(baseAtomInd, 0, false);
            if (newAtomVal < takenBonds)
                throw new ArgumentException("Значение валентности вставляемого атома меньше, чем кол-во занятых связей у базового атома!", "newAtomVal");
            else 
            {
                Element el;
                if (newAtom == Element.C.ToString()) el = Element.C;
                else if (newAtom == Element.N.ToString()) el = Element.N;
                else if (newAtom == Element.O.ToString()) el = Element.O;
                else if (newAtom == Element.S.ToString()) el = Element.S;
                else if (newAtom == Element.H.ToString()) el = Element.H;
                else if (newAtom == Element.F.ToString()) el = Element.F;
                else if (newAtom == Element.Cl.ToString()) el = Element.Cl;
                else if (newAtom == Element.Br.ToString()) el = Element.Br;
                else if (newAtom == Element.I.ToString()) el = Element.I;
                else throw new ArgumentException("Недопустимое название нового атома!", "newAtom");
                atoms[baseAtomInd - 1].Type = el;
                atoms[baseAtomInd - 1].ApdateValence(newAtomVal);
            }
        }

        /// <summary>
        /// Проверка наличия связи между атомами и возврат объекта Bond если связь существует.
        /// </summary>
        /// <param name="A1">Индекс первого атома</param>
        /// <param name="A2">Индекс второго атома</param>
        /// <param name="foundBond"></param>
        /// <returns></returns>
        private bool ExistBond(int A1, int A2, out Bond foundBond)
        {
            if (bonds.Count > 0)
            {
                foreach(Bond b in bonds)
                {
                    if ((b.A.Index == A1 && b.B.Index == A2) || (b.A.Index == A2 && b.B.Index == A1))
                    {
                        foundBond = b;
                        return true;
                    }
                        
                }
            }
            else
            {
                foundBond = null;
                return false;
            }
            foundBond = null;
            return false;
        }
        /// <summary>
        /// Проверка наличия свзязи между атомами.
        /// </summary>
        /// <param name="A1">Индекс первого атома</param>
        /// <param name="A2">Индекс второго атома.</param>
        /// <returns></returns>
        private bool ExistBond(int A1, int A2)
        {
            if (bonds.Count > 0)
            {
                foreach (Bond b in bonds)
                {
                    if ((b.A.Index == A1 && b.B.Index == A2) || (b.A.Index == A2 && b.B.Index == A1))
                        return true;

                }
            }
            else return false;
            return false;
        }
        /// <summary>
        /// Поиск порядка связи между атомами для случае, когда mode == true; поиск свободных валентностей, когда mode == false
        /// (в этом случае secondInd игнорируется).
        /// </summary>
        /// <param name="firstInd"></param>
        /// <param name="secondInd"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private int Bonds(int firstInd, int secondInd, bool mode)
        {

            if (mode == true)
            {
                int count = 0;
                for (int i = 0; i < atoms[firstInd - 1].Neighbours.Length; i++)
                {
                    if (atoms[firstInd - 1].Neighbours[i]?.Index == atoms[secondInd - 1].Index) count++;
                }
                return count;
            }
            else
            {
                int count = 0;
                for (int i = 0; i < atoms[firstInd - 1].Neighbours.Length; i++)
                {
                    if (atoms[firstInd - 1].Neighbours[i] == null) count++;
                }
                return count;
            }
        }
        /// <summary>
        /// Метод для проверки совпадения координат добавляемого атома с координатами уже существующего атома.
        /// В случае если такой атом найдет, метод возвращает объект уже существующего атома.
        /// </summary>
        /// <param name="newAtom"></param>
        /// <param name="createdAtom"></param>
        /// <returns></returns>
        private bool SameCoords(Atom newAtom, out Atom createdAtom)
        {
            foreach (Atom a in atoms)
            {
                if (Math.Abs(a.Position.X - newAtom.Position.X) <= 0.01 && Math.Abs(a.Position.Y - newAtom.Position.Y) <= 0.01)
                {
                    for (int k = 0; k < a.Neighbours.Length; k++)
                    {
                        if (a.Neighbours[k] == null)
                        {
                            createdAtom = a;
                            return true;
                        }

                    }
                }
            }
            createdAtom = null;
            return false;
        }

        private Bitmap ReturnPic(int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);
            Color roundColor = Color.Red; int r = 4;
            Font font = new Font("Arial", 7);

            using (Graphics g = Graphics.FromImage(bm))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                foreach(Atom at in atoms)
                {
                    for(int i = 0; i < at.Neighbours.Length; i++)
                    {
                        if (at.Neighbours[i] != null && at.Index < at.Neighbours[i].Index)
                        {
                            foreach (Bond b in bonds)
                            {
                                Bond temp;
                                if (ExistBond(at.Index, at.Neighbours[i].Index, out temp))
                                {
                                    temp.DrawBond(g);
                                    break;
                                }
                            }
                        }
                        if (ShowAtomNumbers) // отрисовка нумерации атомов
                        {
                            SizeF s = g.MeasureString(at.Index.ToString(), font);
                            g.DrawString(at.Index.ToString(), font, new SolidBrush(Color.Black), at.Position.X - s.Width, at.Position.Y - s.Height);
                        }
                        
                        if (DrawAtomCircle == false && at.ToString() != "C") // отрисовка символов
                        {
                            string temp = at.Label.Replace("{", "").Replace("_", "").Replace("}", "");
                            SizeF size = g.MeasureString(temp, at.LabelFont);

                            g.FillRectangle(new SolidBrush(Color.Transparent), new RectangleF(new PointF(at.Position.X - size.Width / 2, at.Position.Y - size.Height / 2), size));
                            if (at.Valence - Bonds(at.Index, 0, false) > 1)
                            {
                                g.FillRectangle(new SolidBrush(Color.White), 
                                    new RectangleF(new PointF(at.Position.X - size.Width / 2, at.Position.Y - size.Height / 2), size));
                            }
                            MyDrawing.Figures.Figure.DrawString(at.Label, at.LabelPosition, at.LabelFont, Color.Black, g);
                        } 
                        if (DrawAtomCircle)
                        {
                            switch (at.ToString())
                            {
                                case "C": roundColor = Color.Black; break;
                                case "N": roundColor = Color.Blue; break;
                                case "O": roundColor = Color.Red; break;
                            }
                            g.FillEllipse(new SolidBrush(roundColor), at.Position.X - r / 2, at.Position.Y - r / 2, r, r);
                        }
                    }
                }
            }
            return bm;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
