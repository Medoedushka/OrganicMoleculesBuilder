using System;
using MoleculesBuilder;
using System.Drawing;
using OrganicMoleculesBuilder.Model;
using OrganicMoleculesBuilder.Viewer;
using System.Windows.Forms;
using MyDrawing.Figures;

namespace OrganicMoleculesBuilder.Presenter
{
    public class Presenter
    {
        IMainViewer _mainViewer;
        BuilderModel _model;
        PointF mouseLoc;
        bool controlPressed = false;
        bool writingText = false;
        bool movingMolecule = false;
        bool FigureMoving = false;
        TextBox tb;
        Timer timer;

        public Presenter(IMainViewer main)
        {
            _mainViewer = main;
            _model = new BuilderModel();
            tb = new TextBox();
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += (object o, EventArgs e) => {
                _model.DrawMolecules(_mainViewer.DrawPlace);
            };
            timer.Start();
            _mainViewer.DrawPlace.MouseUp += DrawPlace_MouseUp;
            _mainViewer.DrawPlace.DoubleClick += DrawPlace_DoubleClick;
            _mainViewer.DrawPlace.MouseDown += DrawPlace_MouseDown;
            _mainViewer.DrawPlace.MouseMove += DrawPlace_MouseMove;
            _mainViewer.DrawPlace.Paint += DrawPlace_Paint;
            (_mainViewer as MainForm).KeyDown += Presenter_KeyDown;
            (_mainViewer as MainForm).KeyUp += Presenter_KeyUp;
            _mainViewer.SaveWorkSpace += _mainViewer_SaveWorkSpace;
        }
        private void _mainViewer_SaveWorkSpace(string path)
        {
            _model.SaveAsImage(path, _mainViewer.DrawPlace.Image);
        }

        private void DrawPlace_DoubleClick(object sender, EventArgs e)
        {
            // Выделение отдельной молекулы.
            if (_mainViewer.ToolType == ToolType.None)
            {
                foreach(Molecule mol in _model.Molecules)
                {
                    System.Drawing.Rectangle rect = Molecule.GetRectangle(mol);
                    if (mouseLoc.X >= rect.X && mouseLoc.X <= (rect.X + rect.Width) && mouseLoc.Y >= rect.Y && mouseLoc.Y <= (rect.Y + rect.Height))
                    {
                        _model.checkedMolecule = mol;
                        break;
                    }
                    _model.checkedMolecule = null;
                }
            }
        }
        private void DrawPlace_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Инициализация начальных параметров для рисования стрелки.
                if (_mainViewer.ToolType == ToolType.Arrow)
                {
                    _model.FigureDrawing = true;
                    _model.crrFigure = new Arrow(e.Location, e.Location);
                    (_model.crrFigure as Arrow).FillArrowEnd = true;
                    (_model.crrFigure as Arrow).StrokeWidth = 2;
                }

                // Подготовка к перещению молекулы/фигуры
                if (_model.checkedMolecule != null)
                    movingMolecule = true;
                if (_model.checkedFigure != null)
                    FigureMoving = true;
            }
        }
        private void DrawPlace_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Прекращение рисования фигуры и добавление её в список фигур.
                if (_model.FigureDrawing)
                {
                    _model.FigureDrawing = false;
                    if (_model.crrFigure != null && _model.crrFigure.DotA != _model.crrFigure.DotB)
                        _model.Figures.Add(_model.crrFigure);
                    _model.crrFigure = null;
                }
                // Выделение нарисованной фигуры.
                if (_model.Figures.Count > 0 && _mainViewer.ToolType == ToolType.None && !FigureMoving)
                {
                    foreach (Figure f in _model.Figures)
                    {
                        if (f.FigureChecked(mouseLoc))
                        {
                            _model.checkedFigure = f;
                            break;
                        }
                        _model.checkedFigure = null;
                    }

                }
                // Прекращение перемещение молекулы.
                if (movingMolecule)
                {
                    _model.checkedMolecule = null;
                    movingMolecule = false;
                }
                // Прекращение перемещения фигуры.
                if (FigureMoving)
                {
                    FigureMoving = false;
                    _model.checkedFigure = null;
                }

                if (_mainViewer.ToolType == ToolType.SolidBond) // Инструмент для рисования одиночной связи.
                {
                    try
                    {
                        _model.DrawSolidBond(_mainViewer.DrawPlace, mouseLoc);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                // Изменения внешенего вида одиночной связи.
                else if (_mainViewer.ToolType == ToolType.WedgetBond || _mainViewer.ToolType == ToolType.HashedWedgetBond ||
                    _mainViewer.ToolType == ToolType.DashedBond || _mainViewer.ToolType == ToolType.WavyBond)
                {
                    _model.ChangeBondType(_mainViewer.DrawPlace, _mainViewer.ToolType);
                }
                //Инструмент для вставки текста.
                else if (_mainViewer.ToolType == ToolType.Text)
                {
                    _mainViewer.DrawPlace.Controls.Clear();
                    tb = new TextBox()
                    {
                        Multiline = true,
                        BackColor = _mainViewer.DrawPlace.BackColor,
                        BorderStyle = BorderStyle.Fixed3D,
                        Location = new Point((int)mouseLoc.X, (int)mouseLoc.Y),
                        Font = new Font("Times New Roman", 12),
                        Size = new Size(150, 50),
                    };
                    _mainViewer.DrawPlace.Controls.Add(tb);
                    writingText = true;
                }
                else if (_mainViewer.ToolType == ToolType.Connection)
                {
                    _model.ConnectAtoms(_mainViewer.DrawPlace);
                }
                else if (_mainViewer.ToolType == ToolType.Cycles && _mainViewer.Cycloalkane != 0)
                {
                    _model.DrawCycles(_mainViewer.Cycloalkane, _mainViewer.DrawPlace, mouseLoc);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (_model.checkedMolecule != null)
                {
                    System.Drawing.Rectangle rect = Molecule.GetRectangle(_model.checkedMolecule);
                    if (mouseLoc.X >= rect.X && mouseLoc.X <= (rect.X + rect.Width) && mouseLoc.Y >= rect.Y
                        && mouseLoc.Y <= (rect.Y + rect.Height))
                    {
                        (_mainViewer as MainForm).MoleculeSettings.Show(_mainViewer.DrawPlace, e.Location, ToolStripDropDownDirection.Left);
                    }
                }
            }
                        
            //
            // Блок сброса параметров
            //
            if (_mainViewer.ToolType != ToolType.Connection)
                _model.ConnectAtoms();
        }
        private void DrawPlace_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLoc = e.Location;

            // Перемещение молекулы пока зажата кнопки мыши.
            if (movingMolecule)
            {
                System.Drawing.Rectangle rect = Molecule.GetRectangle(_model.checkedMolecule);
                if (mouseLoc.X >= rect.X && mouseLoc.X <= (rect.X + rect.Width) && mouseLoc.Y >= rect.Y &&
                    mouseLoc.Y <= (rect.Y + rect.Height))
                {
                    PointF vector = new PointF(mouseLoc.X - rect.X - rect.Width / 2, mouseLoc.Y - rect.Y - rect.Height / 2);
                    if (!controlPressed)
                        Molecule.RunCommand(ref _model.checkedMolecule, $"Move {vector.X},{vector.Y}", _mainViewer.DrawPlace.Width, _mainViewer.DrawPlace.Height);
                    else Molecule.RunCommand(ref _model.checkedMolecule, $"Move {vector.X},{0}", _mainViewer.DrawPlace.Width, _mainViewer.DrawPlace.Height);
                }
            }
            // Перемещение фигуры пока зажата кнопка мыши.
            if (FigureMoving)
            {
                if (!controlPressed)
                    _model.checkedFigure.DotA = mouseLoc;
                else _model.checkedFigure.DotA = new PointF(mouseLoc.X, _model.checkedFigure.DotB.Y);
            }
            // Рисование стрелки пока зажата кнопка мыши.
            if (_mainViewer.ToolType == ToolType.Arrow && _model.FigureDrawing)
            {
                if (!controlPressed)
                    _model.crrFigure.DotB = mouseLoc;
                else _model.crrFigure.DotB = new PointF(mouseLoc.X, _model.crrFigure.DotA.Y);
            }

            // Выделение атома или связи если на них наведён курсор
            _model.SearchAtomBonds(e.Location);
            // Перерисовка области построения.
            _mainViewer.DrawPlace.Invalidate();
        }


        private void Presenter_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Control)
                controlPressed = false;

            try
            {
                #region<---Вставка гетероатомов--->
                if (e.KeyCode == Keys.O && !writingText)
                    _model.InsertAtom("O(2)", _mainViewer.DrawPlace);
                else if (e.KeyCode == Keys.N && !writingText)
                    _model.InsertAtom("N(3)", _mainViewer.DrawPlace);
                else if (e.KeyCode == Keys.S && !writingText)
                    _model.InsertAtom("S(2)", _mainViewer.DrawPlace);
                else if (e.KeyCode == Keys.H && !writingText)
                    _model.InsertAtom("H(1)", _mainViewer.DrawPlace);
                else if (e.KeyCode == Keys.F && !writingText)
                    _model.InsertAtom("F(1)", _mainViewer.DrawPlace);
                else if (e.KeyCode == Keys.L && !writingText)
                    _model.InsertAtom("Cl(1)", _mainViewer.DrawPlace);
                else if (e.KeyCode == Keys.B && !writingText)
                    _model.InsertAtom("Br(1)", _mainViewer.DrawPlace);
                else if (e.KeyCode == Keys.I && !writingText)
                    _model.InsertAtom("I(1)", _mainViewer.DrawPlace);
                #endregion
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message + "\nДля вставки атома с переменной валентностью используйте редактор атомов.", "Несоответствие валентносетй", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }
        private void Presenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                controlPressed = true;

            // Удаление выделеного атома, связи или фигуры.
            if (e.KeyCode == Keys.Delete)
            {
                _model.DeleteSelectedAtom(_mainViewer.DrawPlace);
                _model.DeleteSelectedBond(_mainViewer.DrawPlace);
                if (_model.checkedFigure != null)
                {
                    _model.Figures.Remove(_model.checkedFigure);
                    _model.checkedFigure = null;
                }
                else if (_model.checkedMolecule != null)
                {
                    _model.Molecules.Remove(_model.checkedMolecule);
                    _model.checkedMolecule = null;
                }
                
            }
            // Выход из режима ввода текста.
            else if (e.KeyCode == Keys.Escape && writingText)
            {
                if (tb.Text != "")
                {
                    Text text = new Text(tb.Location, tb.Text);
                    text.Font = tb.Font;
                    text.TextColor = Color.Black;
                    _mainViewer.DrawPlace.Controls.Clear();
                    _model.Figures.Add(text);
                    writingText = false;
                }
            }
            // Вращение последнего добавленого заместителя.
            if (e.KeyCode == Keys.E && !writingText)
                _model.RotateSub(_mainViewer.DrawPlace, true);
            else if (e.KeyCode == Keys.Q && !writingText)
                _model.RotateSub(_mainViewer.DrawPlace, false);

            // Добавление кратной связи.
            else if (e.KeyCode == Keys.D1 && !writingText)
                _model.ChangeOrder(_mainViewer.DrawPlace, 1);
            else if (e.KeyCode == Keys.D2 && !writingText)
                _model.ChangeOrder(_mainViewer.DrawPlace, 2);
            else if (e.KeyCode == Keys.D3 && !writingText)
                _model.ChangeOrder(_mainViewer.DrawPlace, 3);
        }

        private void DrawPlace_Paint(object sender, PaintEventArgs e)
        {
           _model.ApdateSelection(e.Graphics);
        }

        

        
    }
}
