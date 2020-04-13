using System;
using MoleculesBuilder;
using System.Drawing;
using OrganicMoleculesBuilder.Model;
using OrganicMoleculesBuilder.Viewer;
using System.Windows.Forms;

namespace OrganicMoleculesBuilder.Presenter
{
    public class Presenter
    {
        IMainViewer _mainViewer;
        BuilderModel _model;
        PointF mouseLoc;

        public Presenter(IMainViewer main)
        {
            _mainViewer = main;
            _model = new BuilderModel();
            Timer timer = new Timer();
            timer.Interval = 50;
            timer.Tick += (object o, EventArgs e) => {
                _model.DrawMolecules(_mainViewer.DrawPlace);
            };
            timer.Start();
            _mainViewer.DrawPlace.MouseUp += DrawPlace_MouseUp;
            _mainViewer.DrawPlace.MouseMove += DrawPlace_MouseMove;
            _mainViewer.DrawPlace.Paint += DrawPlace_Paint;
            (_mainViewer as MainForm).KeyDown += Presenter_KeyDown;
            (_mainViewer as MainForm).KeyUp += Presenter_KeyUp;
            
        }

        private void Presenter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.O)
            {
                _model.InsertAtom("O(2)", _mainViewer.DrawPlace);
            }
            else if (e.KeyCode == Keys.N)
            {
                _model.InsertAtom("N(3)", _mainViewer.DrawPlace);
            }
            else if (e.KeyCode == Keys.S)
            {
                _model.InsertAtom("S(2)", _mainViewer.DrawPlace);
            }
            else if (e.KeyCode == Keys.H)
                _model.InsertAtom("H(1)", _mainViewer.DrawPlace);
            else if (e.KeyCode == Keys.F)
                _model.InsertAtom("F(1)", _mainViewer.DrawPlace);
            else if (e.KeyCode == Keys.L)
                _model.InsertAtom("Cl(1)", _mainViewer.DrawPlace);
            else if (e.KeyCode == Keys.B)
                _model.InsertAtom("Br(1)", _mainViewer.DrawPlace);
            else if (e.KeyCode == Keys.I)
                _model.InsertAtom("I(1)", _mainViewer.DrawPlace);

        }

        private void Presenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                _model.DeleteSelectedAtom(_mainViewer.DrawPlace);
            if (e.KeyCode == Keys.E)
                _model.RotateSub(_mainViewer.DrawPlace, true);
            else if (e.KeyCode == Keys.Q)
                _model.RotateSub(_mainViewer.DrawPlace, false);
            else if (e.KeyCode == Keys.Enter)
            {
                Graphics g = _mainViewer.DrawPlace.CreateGraphics();
                g.DrawImage(_model.TestRot(), 5, 5);
                g.Dispose();
            }
        }

        private void DrawPlace_Paint(object sender, PaintEventArgs e)
        {
            _model.ApdateSelection(e.Graphics);
        }

        private void DrawPlace_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLoc = e.Location;

            _model.SearchAtomBonds(e.Location);

            _mainViewer.DrawPlace.Invalidate();
        }

        private void DrawPlace_MouseUp(object sender, MouseEventArgs e)
        {
            if (_mainViewer.ToolType == ToolType.SolidBond)
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
            else if (_mainViewer.ToolType == ToolType.ChangeOrder)
            {
                try
                {
                    _model.ChangeOrder(_mainViewer.DrawPlace);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (_mainViewer.ToolType == ToolType.WedgetBond || _mainViewer.ToolType == ToolType.HashedWedgetBond ||
                _mainViewer.ToolType == ToolType.DashedBond || _mainViewer.ToolType == ToolType.WavyBond)
            {
                _model.ChangeBondType(_mainViewer.DrawPlace, _mainViewer.ToolType);
            }
        }
    }
}
