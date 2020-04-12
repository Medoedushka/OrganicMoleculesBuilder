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

            _mainViewer.DrawPlace.MouseUp += DrawPlace_MouseUp;
            _mainViewer.DrawPlace.MouseMove += DrawPlace_MouseMove;
            _mainViewer.DrawPlace.Paint += DrawPlace_Paint;
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
                    _mainViewer.DrawPlace.Image = _model.DrawSolidBond(_mainViewer.DrawPlace, mouseLoc);
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
                    _mainViewer.DrawPlace.Image = _model.ChangeOrder(_mainViewer.DrawPlace, mouseLoc);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
