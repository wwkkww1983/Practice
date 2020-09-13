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
    public partial class 电缆信息 : Form
    {
        public 电缆信息()
        {
            InitializeComponent();
        }

        private void 设备信息_Load(object sender, EventArgs e)
        {
            Fill();
        }
        public void Fill()
        {
            DataTable dt = DbHelperSQL.GetDataTable("select * from Cable_Info order by SectionId");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string[] str = new string[9];
                    str[0] = dr["CLNumber"].ToString();
                    str[1] = "001分段";
                    str[2] = dr["TotelLenth"].ToString();
                    str[3] = dr["Voltage_Type"].ToString();
                    str[4] = dr["Sheath_Type"].ToString();
                    str[5] = dr["Fsection"].ToString();
                    str[6] = dr["Max_Lateral_Pressure"].ToString();
                    str[7] = dr["UserStatus"].ToString();
                    dataGridView1.Rows.Add(str);
                }
            }
            dt.Clear();
            dt.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add("", "", "", "", "","","");
        }
    }
}
