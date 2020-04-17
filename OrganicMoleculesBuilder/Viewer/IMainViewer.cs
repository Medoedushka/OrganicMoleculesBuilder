using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoleculesBuilder;
using System.Windows.Forms;

namespace OrganicMoleculesBuilder.Viewer
{
    public enum ToolType
    {
        None,
        SolidBond,
        WedgetBond,
        HashedWedgetBond,
        WavyBond,
        DashedBond,
        Arrow,
        Text,
        Connection,
        Cycles,
        Benzene = 6
    }

    public interface IMainViewer
    {
        ToolType ToolType { get; set;}
        PictureBox DrawPlace { get;}
        string PathToSave { get;}
        int Cycloalkane { get;}


        event Action<string> SaveWorkSpace;

        event EventHandler<EventArgs> ShowMoleculesProperties;
        event EventHandler<EventArgs> DuplicateMolecule;
        event EventHandler<EventArgs> DeleteMolecule;
    }
}
