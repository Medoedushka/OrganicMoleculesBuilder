using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MoleculesBuilder;
using OrganicMoleculesBuilder.Viewer;

namespace OrganicMoleculesBuilder
{
    public partial class MainForm : Form, IMainViewer
    {
        PictureBox[] pcbGroup1;
        Color buttonChecked = Color.FromArgb(150, 80, 80);

        public event Action<string> SaveWorkSpace;
        public event EventHandler<EventArgs> ShowMoleculesProperties;
        public event EventHandler<EventArgs> DuplicateMolecule;
        public event EventHandler<EventArgs> DeleteMolecule;

        public MainForm()
        {
            InitializeComponent();
            pcbGroup1 = new PictureBox[]
            {
                pcb_None,
                pcb_SolidBond,
                pcb_WedgetBond,
                pcb_HashedWedgetBond,
                pcb_WavyBond,
                pcb_DashedBond,
                pcb_Text,
                pcb_Arrow,
                pcb_ConnectAtoms,
                pcb_Cyclohexane,
                pcb_Cyclopentane,
                pcb_Benzene
            };
            string[] ExpStrings = new string[]
            {
                /*0*/"Клинкните на связь, чтобы изменить её вид",
                /*1*/"Кликните на свободное место на холсте, чтобы ввести текст. Используйте _{} и ^{}, чтобы вводить верхние и нижние индексы соответственно",
                /*2*/"Нажмите и удерживайте, чтобы добавить стрелку",
                /*3*/"Выберите последовательно два атома, чтобы соединить их обычной одинарной связью",
                /*4*/"Кликните по фигуре, чтобы её выбрать; дважды кликните по молекуле, чтобы её выделить; удерживайте и тяните курсор пока молекула выделена, чтобы её переместить",
                /*5*/"Кликните на свободное место, чтобы нарисовать цикл"

            };
            ToolType = ToolType.SolidBond;
            foreach(PictureBox pb in pcbGroup1)
            {
                pb.MouseEnter += (object o, EventArgs e) => 
                {
                    if (pb.BackColor != buttonChecked)
                        pb.BackColor = Color.FromArgb(177, 195, 80, 80);
                    if (pb.Name == pcb_Text.Name)
                        lbl_Status.Text = ExpStrings[1];
                    else if (pb.Name == pcb_Arrow.Name)
                        lbl_Status.Text = ExpStrings[2];
                    else if (pb.Name == pcb_ConnectAtoms.Name)
                        lbl_Status.Text = ExpStrings[3];
                    else if (pb.Name == pcb_None.Name)
                        lbl_Status.Text = ExpStrings[4];
                    else if (pb.Name == pcb_Benzene.Name || pb.Name == pcb_Cyclohexane.Name || pb.Name == pcb_Cyclopentane.Name)
                        lbl_Status.Text = ExpStrings[5];
                    else lbl_Status.Text = ExpStrings[0];
                };
                pb.MouseLeave += (object o, EventArgs e) => 
                { if (pb.BackColor != buttonChecked)
                        pb.BackColor = Color.LightCoral;
                    lbl_Status.Text = "";
                };
            }
        }
        public ToolType ToolType { get; set; }
        public PictureBox DrawPlace => pictureBox3;
        public int CycloType{ get; private set; }


        public string PathToSave
        {
            get
            {
                using (SaveFileDialog sf = new SaveFileDialog())
                {
                    sf.Filter = "PNG (*.png)|*.png";
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        return sf.FileName;
                    }
                    else return "";
                }
            }
        }

        private void SetGroupColor(Color color, PictureBox[] pbs)
        {
            foreach (PictureBox pb in pbs)
                pb.BackColor = color;
        }

        public void pcb_SolidBond_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_SolidBond.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.SolidBond;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_WedgetBond_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_WedgetBond.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.WedgetBond;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_HashedWedgetBond_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_HashedWedgetBond.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.HashedWedgetBond;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_DashedBond_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_DashedBond.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.DashedBond;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_WavyBond_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_WavyBond.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.WavyBond;
            pictureBox3.Cursor = Cursors.Default;
        }
        private void pcb_Arrow_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_Arrow.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.Arrow;
            pictureBox3.Cursor = Cursors.Cross;
        }

        public void pcb_None_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_None.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.None;
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pcb_Text_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_Text.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.Text;
            pictureBox3.Cursor = Cursors.IBeam;
        }

        private void экспортироватьКакPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveWorkSpace?.Invoke(PathToSave);
        }

        private void pcb_ConnectAtoms_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_ConnectAtoms.BackColor = Color.FromArgb(150, 80, 80);
            ToolType = ToolType.Connection;
            pictureBox3.Cursor = Cursors.Hand;
        }

        private void tsm_MolProperties_Click(object sender, EventArgs e)
        {
            ShowMoleculesProperties?.Invoke(this, EventArgs.Empty);
        }

        private void tsm_MolDuplicate_Click(object sender, EventArgs e)
        {
            DuplicateMolecule?.Invoke(this, EventArgs.Empty);
        }

        private void tsm_Delete_Click(object sender, EventArgs e)
        {
            DeleteMolecule?.Invoke(this, EventArgs.Empty);
        }

        private void pcb_Cyclohexane_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_Cyclohexane.BackColor = Color.FromArgb(150, 80, 80);
            pictureBox3.Cursor = Cursors.Default;
            ToolType = ToolType.Cycles;
            CycloType = 6;
        }

        private void pcb_Cyclopentane_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_Cyclopentane.BackColor = Color.FromArgb(150, 80, 80);
            pictureBox3.Cursor = Cursors.Default;
            ToolType = ToolType.Cycles;
            CycloType = 5;
        }

        private void pcb_Benzene_Click(object sender, EventArgs e)
        {
            SetGroupColor(Color.LightCoral, pcbGroup1);
            pcb_Benzene.BackColor = Color.FromArgb(150, 80, 80);
            pictureBox3.Cursor = Cursors.Default;
            ToolType = ToolType.Cycles;
            CycloType = -6;
        }
    }
}
