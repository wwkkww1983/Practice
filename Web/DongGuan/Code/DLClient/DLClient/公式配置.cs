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
    public partial class 公式配置 : Form
    {
        public 公式配置()
        {
            InitializeComponent();
        }

        private void 公式配置_Load(object sender, EventArgs e)
        {
            Fill();
        }
        public void Fill()
        {
            //string sql = string.Format(@"select Id, version, fexpression, class_name, xml_name, fremark, CreateId, Delteted from dbo.Formula_Detail");
            //DataTable dt = DBHelper.Select(sql);
            //dataGridView2.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
