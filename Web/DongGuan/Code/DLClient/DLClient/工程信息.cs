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
    public partial class ProjectInfo : Form
    {
        public ProjectInfo()
        {
            InitializeComponent();
        }

        private void 工程信息_Load(object sender, EventArgs e)
        {
            Fill();
        }
        public void Fill()
        {
            //string sql = string.Format(@"select Id, ProjectCode, ProjectName, Location, Distance, State, SectionCount, CircuitCount, ClectricityType, LayEnvironment, Temperature, Humidity, Voltage, CutSize, BuildUnit, BuildTuch, BuildTelephon, SysCompanyId, Contacts, Phone, Supervision, SupervisionTuch, SupervisionTelephone, NowProject, Remark, CreateId, CreateUser, CreateTime, UpdateId, UpdateUser, UpdateTime, Delteted from dbo.Project_Info");
            //DataTable dt = DBHelper.Select(sql);
            //dataGridView1.DataSource = dt;
        }

    }
}
