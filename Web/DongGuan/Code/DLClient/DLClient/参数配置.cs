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
    public partial class 参数配置 : Form
    {
        public 参数配置()
        {
            InitializeComponent();
        }

        private void 参数配置_Load(object sender, EventArgs e)
        {
            Fill();
        }
        public void Fill()
        {
            //string sql = string.Format(@"select Id, pipe_type, friction_factor,fremark, Delteted from dbo.Friction");
            //DataTable dt = DBHelper.Select(sql);
            //dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
