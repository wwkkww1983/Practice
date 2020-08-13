using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLClient
{
    public partial class 监测点信息 : Form
    {
        public 监测点信息()
        {
            InitializeComponent();
        }

        private void 监测点信息_Load(object sender, EventArgs e)
        {
            Fill();
        }
        public void Fill()
        {
            //string sql = string.Format(@"select Id, ProjectId, SectionId, EquipmentId, TerminalId, ParameterId, Position, PositionX, PositionY, PositionZ, PositionSort, Distance, StopOrder, StartOrder, IsEncryption, Remark, CreateId, CreateUser, CreateTime, UpdateId, UpdateUser, UpdateTime, Delteted from dbo.MonitorPoint");
            //DataTable dt = DBHelper.Select(sql);
            //dataGridView1.DataSource = dt;
        }
    }
}
