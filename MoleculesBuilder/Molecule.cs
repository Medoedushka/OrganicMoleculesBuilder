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
    public class Molecule
    {
        public static string[] Keywords =
        {
            "Create",
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

        string Name { get; set; }
        const double L = 35; // Длина связи C-C
        const double ANGLE = 120;//109.47; // Угол связи C-C
        const double K = Math.PI / 180;
        public List<Atom> atoms = new List<Atom>();
        List<string> InvAtomPairs = new List<string>();
        public bool ShowAtomNumbers { get; set; }
        public bool DrawAtomCircle { get; set; }
        public string MolecularPartsDir { get; set; }

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
        private static void GetCoord(double a, out PointF[] newVec, out PointF[] subPt)
        {
            newVec = new PointF[1];
            newVec[0] = RotateVector(a, new PointF(0, (float)-L));
            subPt = new PointF[2];
        }

        //Расчитывает точки для правильного n-угольника
        private static PointF[] DrawPoly(double l, PointF center, int n = 5)
        {
            PointF[] pt = new PointF[n];
            for (int i = 0; i < pt.Length; i++)
            {
                pt[i].X = (float)(l * Math.Sin(i * 360 / n * K) + center.X);
                pt[i].Y = (float)(l * Math.Cos(i * 360 / n * K) + center.Y);
            }

            return pt;
        }
        //Вращает вектор по часовой стрелке для положительных значений аргумента ang
        private static PointF RotateVector(double ang, PointF vec)
        {
            float x = 0, y = 0;
            x = (float)(vec.X * Math.Cos(ang * K) - vec.Y * Math.Sin(ang * K));
            y = (float)(vec.X * Math.Sin(ang * K) + vec.Y * Math.Cos(ang * K));
            return new PointF(x, y);
        }
        
        //Вращение указанных вершин молекулы относительно базовой вершины на угол ang
        private static void RotateMolecularPart(Molecule crrMolecule, int[] rotInd, int baseInd, double ang)
        {
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

            //pcb_Output.Image = crrMolecule.ReturnPic(pcb_Output.Width, pcb_Output.Height);
        }
        //Добавление нового заместителя в молекулу
        private static void DrawSub(Molecule crrMolecule, string type, string pos, double ang)
        {
            PointF[] subPt = new PointF[1];
            PointF targetPos = new PointF(0, 0);
            PointF[] newVector = new PointF[1];
            int subPos;
            Element el = Element.C;
            int val = 4;

            //Поиск координат атома, к которому будет цепляться заместитель
            if (pos.Contains(";"))
            {
                string[] parts = pos.Split(';');
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
                GetCoord(ang, out newVector, out subPt);
            switch (type)
            {
                case "Me":
                    el = Element.C;
                    val = 4;
                    break;
                case "Et":
                    newVector = new PointF[2];
                    newVector[0] = RotateVector(ang, new PointF(0, (float)-L));
                    newVector[1] = RotateVector(ang, new PointF((float)(2 * L * Math.Sin(K * ANGLE / 2) * Math.Cos(Math.PI / 3)),
                       (float)(-2 * L * Math.Sin(K * ANGLE / 2) * Math.Sin(Math.PI / 3))));
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
                        PointF vec = RotateVector(ang, new PointF(0, 2 * (float)L));
                        PointF[] MainAcyclicChain = DrawPoly(L, new PointF(targetPos.X - vec.X, targetPos.Y - vec.Y), n);
                        int p = subPos;
                        int s = 0;
                        for (int i = 0; i < MainAcyclicChain.Length; i++)
                        {
                            //if (i != MainAcyclicChain.Length - 1)
                            //{
                                Atom atom = new Atom(Element.C, 4, crrMolecule.atoms.Count + 1, new PointF(MainAcyclicChain[i].X, MainAcyclicChain[i].Y));
                                crrMolecule.AddAtom(atom, 1, p);
                            //}
                            p = crrMolecule.atoms.Count;
                            if (s == 0) s = p;
                        }
                        crrMolecule.ConnectAtoms(s, p, 1);
                    }
                    return;
            }

            int prev = subPos;
            for (int i = 1; i < subPt.Length; i++)
            {
                subPt[i].X = targetPos.X + newVector[i - 1].X;
                subPt[i].Y = targetPos.Y + newVector[i - 1].Y;
                Atom atom = new Atom(el, val, crrMolecule.atoms.Count + 1, new PointF(subPt[i].X, subPt[i].Y));
                crrMolecule.AddAtom(atom, 1, prev);
                prev = crrMolecule.atoms.Count;
            }


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
            string command = comm.Trim(new char[] { '\n' });
            string[] el = command.Split(' ');
            switch (el[0])
            {
                case "Add":
                    if (crrMolecule != null && el[2].ToLower() == "at")
                    {
                        if (ContainsNotNumbers(el[4])) el[4] = AddStrings(el[4]);
                        if (el[3].Contains(";") && crrMolecule.atoms.Count != 0)
                        {
                            throw new Exception("Начальная точка уже была построена.");
                        }
                        else
                        {
                            DrawSub(crrMolecule, el[1], el[3], double.Parse(el[4]));
                        }
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
                                    if (!string.IsNullOrEmpty(str) && !str.Contains("AddFromFile"))
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
                        string[] parts = el[1].Split(',');
                        int[] indexes = new int[parts.Length];
                        for (int i = 0; i < indexes.Length; i++)
                        {
                            indexes[i] = int.Parse(parts[i]);
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
                            crrMolecule.ConnectAtoms(int.Parse(el[1]), int.Parse(el[2]), int.Parse(el[4]));
                        else
                        {
                            crrMolecule.ConnectAtoms(int.Parse(el[1]), int.Parse(el[2]), 1);
                            crrMolecule.AddInvPair(int.Parse(el[1]), int.Parse(el[2]), el[4]);
                        }

                        if (el.Length == 6 && el[5].TrimEnd(new char[] { '\n' }) == "inv")
                            crrMolecule.AddInvPair(int.Parse(el[1]), int.Parse(el[2]));


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
                                at.Position = new PointF(at.Position.X + moveVector.X, at.Position.Y - moveVector.Y);
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
                        string atom = Convert.ToString(el[1][0]);
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

        public Molecule(string name, string dir)
        {
            Name = name;
            MolecularPartsDir = dir;
            ShowAtomNumbers = true;
            DrawAtomCircle = true;
        }

        public void AddInvPair(int ind1, int ind2, string type = "")
        {
            if (IsInvPair(ind1, ind2))
                return;
            if (type == "")
                InvAtomPairs.Add(ind1 + "-" + ind2);
            else InvAtomPairs.Add(ind1 + "-" + ind2 + "-" + type);
        }
        private bool IsInvPair(int ind1, int ind2)
        {
            foreach(string str in InvAtomPairs)
            {
                string[] el = str.Split('-');
                if ((ind1 == int.Parse(el[0]) && ind2 == int.Parse(el[1])) || (ind1 == int.Parse(el[1]) && ind2 == int.Parse(el[0])))
                    return true;
                
            }
            return false;
        }

        private bool AtomExists(int atomIndex)
        {
            foreach(Atom at in atoms)
            {
                if (at.Index == atomIndex) return true;
            }
            return false;
        }

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

        public void AddAtom(Atom newAtom, int order, int pos)
        {
            if (order > newAtom.Valence) throw new ArgumentOutOfRangeException("order", "order > valence", "Валентность не может быть меньше указаного порядка связи!");

            if (atoms.Count == 0) atoms.Add(newAtom);
            else
            {
                for(int i = 0; i < atoms.Count; i++)
                {
                    if (atoms[i].Index == pos)
                    {
                        int freeBons = 0;
                        for(int j = 0; j < atoms[i].Neighbours.Length; j++)
                        {
                            if (atoms[i].Neighbours[j] == null) freeBons++;
                        }

                        if (freeBons >= order)
                        {
                            int k = 0;
                            for (int j = 0; j < atoms[i].Neighbours.Length; j++)
                            {
                                if (atoms[i].Neighbours[j] == null && k < order)
                                {
                                    
                                    atoms[i].Neighbours[j] = newAtom;
                                    newAtom.Neighbours[k] = atoms[i];
                                    k++;
                                }
                              
                            }
                            atoms.Add(newAtom);
                            return;
                        }
                        else throw new ArgumentOutOfRangeException("freeBonds", "order > freeBonds", "У атома с индексом " + pos + " недостаточно свободных валентностей для образования связи!");
                    }
                }
            }
        }

        public void ConnectAtoms(int firstInd, int secondInd, int order)
        {
            if (order > 0 && order < 4)
            {
                if (order == 1 && IsInvPair(firstInd, secondInd))
                {
                    InvAtomPairs.Remove(firstInd + "-" + secondInd);
                    InvAtomPairs.Remove(secondInd + "-" + firstInd);
                    InvAtomPairs.Remove(firstInd + "-" + secondInd + "-w");
                    InvAtomPairs.Remove(secondInd + "-" + firstInd + "-hw");
                }

                if (!AtomExists(firstInd) || !AtomExists(secondInd))
                    throw new ArgumentOutOfRangeException("Передаваемые индексы атомов не существуют!");
                int d = order - Bonds(firstInd, secondInd, true);
                if (d > 0)
                {
                    if (d <= Bonds(firstInd, 0, false) && d <= Bonds(secondInd, 0, false))
                    {
                        int count = 0;
                        for (int i = 0; i < atoms[firstInd - 1].Neighbours.Length; i++)
                        {
                            if (atoms[firstInd - 1].Neighbours[i] == null && count < d)
                            {
                                atoms[firstInd - 1].Neighbours[i] = atoms[secondInd - 1];
                                count++;
                            }

                        }
                        count = 0;
                        for (int i = 0; i < atoms[secondInd - 1].Neighbours.Length; i++)
                        {
                            if (atoms[secondInd - 1].Neighbours[i] == null && count < d)
                            {
                                atoms[secondInd - 1].Neighbours[i] = atoms[firstInd - 1];
                                count++;
                            }

                        }
                    }
                }
                else
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
                    }
                }

            }       
        }

        public void RemoveAtom(int index)
        {
            foreach(Atom at in atoms)
            {
                for(int i = 0; i < at.Neighbours.Length; i++)
                {
                    if (at.Neighbours[i] == atoms[index - 1])
                        at.Neighbours[i] = null;
                }
            }
            atoms.Remove(atoms[index - 1]);
        }

        public void InsertAtom(string newAtom, int newAtomVal, int baseAtomInd)
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
                else throw new ArgumentException("Недопустимое название нового атома!", "newAtom");
                atoms[baseAtomInd - 1].Type = el;
                atoms[baseAtomInd - 1].ApdateValence(newAtomVal);
            }
        }

        private void MultiBonds(int p, Atom atBase, Atom atNeighbour,  out PointF pt1, out PointF pt2)
        {
            int d = 5;
            PointF vector = new PointF(atNeighbour.Position.X - atBase.Position.X, atNeighbour.Position.Y - atBase.Position.Y);
            double n_y = vector.X * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            double n_x = -vector.Y * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            PointF moveVector = new PointF((float)n_x, (float)n_y);
            
            if (IsInvPair(atBase.Index, atNeighbour.Index))
            {
                pt1 = new PointF(atBase.Position.X + p * moveVector.X, atBase.Position.Y + p * moveVector.Y);
                pt2 = new PointF(atNeighbour.Position.X + p * moveVector.X, atNeighbour.Position.Y + p * moveVector.Y);
            }
            else
            {
                pt1 = new PointF(atBase.Position.X - p * moveVector.X, atBase.Position.Y - p * moveVector.Y);
                pt2 = new PointF(atNeighbour.Position.X - p * moveVector.X, atNeighbour.Position.Y - p * moveVector.Y);
            }
        }

        public Bitmap ReturnPic(int width, int height)
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
                            int numBonds = Bonds(at.Index, at.Neighbours[i].Index, true);
                            if (numBonds == 1 && IsInvPair(at.Index, at.Neighbours[i].Index))
                            {
                                PointF vector = new PointF(at.Neighbours[i].Position.X - at.Position.X, at.Neighbours[i].Position.Y - at.Position.Y);
                                string type = "";
                                foreach (string s in InvAtomPairs)
                                {
                                    if (s.Contains(at.Index + "-" + at.Neighbours[i].Index) || s.Contains(at.Neighbours[i].Index + "-" + at.Index))
                                    {
                                        string[] el = s.Split('-');
                                        type = el[2];
                                    }
                                }
                                if (type == "w")
                                {
                                    int d = 5;
                                    
                                    double n_y = vector.X * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                                    double n_x = -vector.Y * d / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                                    PointF moveVector = new PointF((float)n_x, (float)n_y);
                                    PointF[] pts =
                                    {
                                    new PointF(at.Neighbours[i].Position.X - moveVector.X, at.Neighbours[i].Position.Y - moveVector.Y),
                                    new PointF(at.Neighbours[i].Position.X + moveVector.X, at.Neighbours[i].Position.Y + moveVector.Y),
                                    at.Position
                                };
                                    g.FillPolygon(new SolidBrush(Color.Black), pts);
                                }
                                else if (type == "hw")
                                {
                                    for(int n = 1; n <= 5; n++)
                                    {
                                        double n_y = vector.X * n / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                                        double n_x = -vector.Y * n / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                                        PointF moveVector = new PointF((float)n_x, (float)n_y);
                                        PointF pt = new PointF(at.Position.X + vector.X * 1/5 * n , at.Position.Y + vector.Y * 1 / 5 * n);
                                        g.DrawLine(new Pen(Color.Black), new PointF(pt.X - moveVector.X, pt.Y - moveVector.Y), new PointF(pt.X + moveVector.X, pt.Y + moveVector.Y));
                                    }
                                }
                            }
                            else 
                                g.DrawLine(new Pen(Color.Black), at.Position, at.Neighbours[i].Position);
                            PointF pt1, pt2;
                            if (numBonds >= 2)
                            {
                                MultiBonds(1, at, at.Neighbours[i], out pt1, out pt2);
                                if (numBonds == 3) MultiBonds(-1, at, at.Neighbours[i], out pt1, out pt2);
                                g.DrawLine(new Pen(Color.Black), pt1, pt2);
                            }
                        }


                        if (at.ToString() != "C" && DrawAtomCircle == false)
                        {
                            int hidrNum = Bonds(at.Index, 0, false);
                            string symbol = hidrNum > 1 ? at.ToString() + "H" + hidrNum : at.ToString() + (hidrNum == 0 ? "" : "H");
                            Font symbolFont = new Font("Arial", 10);
                            SizeF size = g.MeasureString(symbol, symbolFont);
                            g.FillRectangle(new SolidBrush(Color.White), new RectangleF(new PointF(at.Position.X - size.Width / 2, at.Position.Y - size.Height / 2), size));
                            g.DrawString(symbol, symbolFont, new SolidBrush(Color.Black), at.Position.X - size.Width / 2, at.Position.Y - size.Height / 2);
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
                        
                        if (ShowAtomNumbers)
                        {
                            SizeF s = g.MeasureString(at.Index.ToString(), font);
                            g.DrawString(at.Index.ToString(), font, new SolidBrush(Color.Black), at.Position.X - s.Width, at.Position.Y - s.Height);
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
