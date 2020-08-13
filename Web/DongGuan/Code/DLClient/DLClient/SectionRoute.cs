using System;
using System.Drawing;
using System.Windows.Forms;

namespace DLClient
{
    public partial class SectionRoute : Form
    {
        #region 变量
        MapControlModel _mcm;
        #endregion
        public SectionRoute(MapControlModel mcm)
        {
            _mcm = mcm;
            InitializeComponent();
            mapControl1._OperationMode = _mcm.operationMode;
            mapControl1._SectionId = _mcm.SectionId;
            mapControl1._ParentObj = this;
            
            var pb = mapControl1.Controls.Find("pictureBox1", true)[0] as PictureBox;
            int gLeft = this.Width / 2 - pb.Width / 2;
            int gTop = this.Height / 2 - pb.Height / 2;
            pb.Location = new Point(gLeft, gTop);
        }

        private void gMap_Load(object sender, EventArgs e)
        {
        }

        private void SectionRoute_FormClosing(object sender, FormClosingEventArgs e)
        {
            //mapControl1.Clear();
        }
    }
}
