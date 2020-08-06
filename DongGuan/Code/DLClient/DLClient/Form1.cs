using System;
using System.Data;
using System.Windows.Forms;

namespace DLClient
{
    public partial class Form1 : Form
    {
        Form form;
        public Form1(Form frmLogin)
        {
            form = frmLogin;
            InitializeComponent();
            #region 加载地图
            mapControl1._OperationMode = OperationMode.View;
            mapControl1._SectionId = "1";//TODO:需要修改获取动态的sectionId
            mapControl1._ParentObj = this;

            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = Application.StartupPath + @"\MP10.ssk";
            //this.WindowState = FormWindowState.Maximized;    //最大化窗体 
            //绑定计算结构数据
            //this.dataGridView1.DataSource = DbHelperSQL.GetDataTable("select * from ForceAnalysis");

            LoadData();
        }

        private void LoadData()
        {
            DataTable dt = DbHelperSQL.GetDataTable("select * from ForceAnalysis order by codes");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string[] str = new string[15];
                    str[0] = (i + 1).ToString();
                    str[1] = dr["Section"].ToString();
                    str[2] = dr["Lengths"].ToString() + "米";
                    str[3] = dr["BuryType"].ToString();
                    str[4] = dr["ForceValue1"].ToString();
                    str[5] = dr["ForceValue2"].ToString();
                    if (dr["IfQualify"].ToString() == "1")
                    {
                        str[6] = "合格";
                    }
                    else
                    {
                        str[6] = "不合格";
                    }
                    str[7] = dr["ProposedProg"].ToString();
                    str[8] = dr["Material"].ToString();
                    str[9] = dr["StartX"].ToString();
                    str[10] = dr["StartY"].ToString();
                    str[11] = dr["StartZ"].ToString();
                    str[12] = dr["EndX"].ToString();
                    str[13] = dr["EndY"].ToString();
                    str[14] = dr["EndZ"].ToString();
                    dataGridView1.Rows.Add(str);
                }
            }
            dt.Clear();
            dt.Dispose();

            DeviceInfoBind();

            ProjectInfoShow("36e22bb1-a29b-4890-bf84-4c8b68fb2b21");
        }

        private void ProjectInfoShow(string pid)
        {
            DataTable dt = DbHelperSQL.GetDataTable("select * from Project_Info where Id='" + pid + "'");
            if (dt.Rows.Count > 0)
            {
                label3.Text = dt.Rows[0]["ProjectName"].ToString();
                label4.Text = dt.Rows[0]["Location"].ToString();
                label6.Text = dt.Rows[0]["SectionCount"].ToString()+"段";
            }
            dt.Clear();
            dt.Dispose();
        }

        private void DeviceInfoBind()
        {
            DataTable dt = DbHelperSQL.GetDataTable("select COUNT(id) as nums,Name,units from CL_Equipment group by EquipmentTypeId,Name,units");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string[] str = new string[15];
                    str[0] = (i + 1).ToString();
                    str[1] = dr["Name"].ToString();
                    str[2] = dr["nums"].ToString();
                    str[3] = dr["units"].ToString();                    
                    dataGridView2.Rows.Add(str);
                }
            }
            dt.Clear();
            dt.Dispose();
        }

        private void 受力计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ForceCalculation newform = new ForceCalculation();
            newform.StartPosition = FormStartPosition.CenterScreen;
            newform.ShowDialog();
        }

        private void 退出登陆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 公式配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            公式配置 form = new 公式配置();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void 参数配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            参数配置 form = new 参数配置();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }
        
        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            操作员管理 form = new 操作员管理();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void 工程信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectInfoEdit form = new ProjectInfoEdit();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void 设备信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            设备信息 form = new 设备信息();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void 监测点管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            监测点信息 form = new 监测点信息();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void 区段信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            区段信息 form = new 区段信息();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddDevice form = new AddDevice();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void 实时数据展示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataMonitor form = new DataMonitor();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void 三维轨迹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", " --kiosk file:///E:/%E4%B8%AD%E8%88%AA%E4%BF%A1%E6%81%AF/%E7%94%B5%E7%BC%86%E9%93%BA%E8%AE%BE%E7%9B%91%E6%8E%A7/line3d-.html");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ProjectInfoShow("36e22bb1-a29b-4890-bf84-4c8b68fb2b21");
        }

        private void 电缆信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            电缆信息 form = new 电缆信息();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Refresh(); 
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            LoadData();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Close();
        }
    }
}
