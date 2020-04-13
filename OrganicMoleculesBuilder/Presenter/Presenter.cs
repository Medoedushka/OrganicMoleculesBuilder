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
        TextBox tb;

        public Presenter(IMainViewer main)
        {
            _mainViewer = main;
            _model = new BuilderModel();
            tb = new TextBox();
            Timer timer = new Timer();
            timer.Interval = 50;
            timer.Tick += (object o, EventArgs e) => {
                _model.DrawMolecules(_mainViewer.DrawPlace);
            };
            timer.Start();
            _mainViewer.DrawPlace.MouseUp += DrawPlace_MouseUp;
            _mainViewer.DrawPlace.MouseDown += DrawPlace_MouseDown;
            _mainViewer.DrawPlace.MouseMove += DrawPlace_MouseMove;
            _mainViewer.DrawPlace.Paint += DrawPlace_Paint;
            (_mainViewer as MainForm).KeyDown += Presenter_KeyDown;
            (_mainViewer as MainForm).KeyUp += Presenter_KeyUp;
            
        }

        private void DrawPlace_MouseDown(object sender, MouseEventArgs e)
        {
            if (_mainViewer.ToolType == ToolType.Arrow)
            {
                _model.FigureDrawing = true;
                _model.crrFigure = new Arrow(e.Location, e.Location);
                (_model.crrFigure as Arrow).FillArrowEnd = false;
                (_model.crrFigure as Arrow).StrokeWidth = 2;
            }
        }

        private void Presenter_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Control)
                controlPressed = false;

            if (e.KeyCode == Keys.O && !writingText)
            {
                _model.InsertAtom("O(2)", _mainViewer.DrawPlace);
            }
            else if (e.KeyCode == Keys.N && !writingText)
            {
                _model.InsertAtom("N(3)", _mainViewer.DrawPlace);
            }
            else if (e.KeyCode == Keys.S && !writingText)
            {
                _model.InsertAtom("S(2)", _mainViewer.DrawPlace);
            }
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

        }

        private void Presenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                controlPressed = true;

            if (e.KeyCode == Keys.Delete)
            {
                _model.DeleteSelectedAtom(_mainViewer.DrawPlace);
                if (_model.checkedFigure != null)
                {
                    _model.Figures.Remove(_model.checkedFigure);
                    _model.checkedFigure = null;
                }
            }
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
            if (e.KeyCode == Keys.E && !writingText)
                _model.RotateSub(_mainViewer.DrawPlace, true);
            else if (e.KeyCode == Keys.Q && !writingText)
                _model.RotateSub(_mainViewer.DrawPlace, false);
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

        private void DrawPlace_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLoc = e.Location;

            if (_mainViewer.ToolType == ToolType.Arrow && _model.FigureDrawing)
            {
                if (!controlPressed)
                    _model.crrFigure.DotB = mouseLoc;
                else _model.crrFigure.DotB = new PointF(mouseLoc.X, _model.crrFigure.DotA.Y);
            }

            _model.SearchAtomBonds(e.Location);

            _mainViewer.DrawPlace.Invalidate();

            
        }

        private void DrawPlace_MouseUp(object sender, MouseEventArgs e)
        {
            if (_model.FigureDrawing)
            {
                _model.FigureDrawing = false;
                if (_model.crrFigure != null && _model.crrFigure.DotA != _model.crrFigure.DotB)
                    _model.Figures.Add(_model.crrFigure);
                _model.crrFigure = null;
            }

            if (_model.Figures.Count > 0 && _mainViewer.ToolType == ToolType.None)
            {
                foreach (Figure f in _model.Figures)
                {
                    if (f.FigureChecked(mouseLoc))
                        _model.checkedFigure = f;
                    else _model.checkedFigure = null;
                }
                
            }
            else if (_mainViewer.ToolType == ToolType.SolidBond)
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
            else if (_mainViewer.ToolType == ToolType.WedgetBond || _mainViewer.ToolType == ToolType.HashedWedgetBond ||
                _mainViewer.ToolType == ToolType.DashedBond || _mainViewer.ToolType == ToolType.WavyBond)
            {
                _model.ChangeBondType(_mainViewer.DrawPlace, _mainViewer.ToolType);
            }
            else if (_mainViewer.ToolType == ToolType.Text)
            {
                _mainViewer.DrawPlace.Controls.Clear();
                tb = new TextBox()
                {
                    Multiline = true,
                    BackColor = _mainViewer.DrawPlace.BackColor,
                    BorderStyle = BorderStyle.Fixed3D,
                    Location = new Point((int)mouseLoc.X, (int)mouseLoc.Y),
                    Font = new Font("Arial", 10),
                    Size = new Size(150, 50),
                };
                _mainViewer.DrawPlace.Controls.Add(tb);
                writingText = true;
            }
        }
    }
}
