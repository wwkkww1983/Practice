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
    public partial class 设备信息 : Form
    {
        public 设备信息()
        {
            InitializeComponent();
        }

        private void 设备信息_Load(object sender, EventArgs e)
        {
            Fill();
        }
        public void Fill()
        {
            //string sql = string.Format(@"select Name,PointName,ModelNumber,ECode,BarCode,Weight,Units from CL_Equipment order by Name");
            //DataTable dt = DbHelperSQL.GetDataTable(sql);
            //dataGridView1.DataSource = dt;
            DataTable dt = DbHelperSQL.GetDataTable("select * from CL_Equipment order by Name");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string[] str = new string[8];
                    str[0] = (i + 1).ToString();
                    str[1] = dr["Name"].ToString();
                    str[2] = dr["ModelNumber"].ToString();
                    str[3] = dr["ECode"].ToString();
                    str[4] = dr["BarCode"].ToString();
                    str[5] = dr["Weight"].ToString();
                    str[6] = dr["Units"].ToString();
                    dataGridView1.Rows.Add(str);
                }
            }
            dt.Clear();
            dt.Dispose();
        }
    }
}
