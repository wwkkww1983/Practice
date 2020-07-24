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
    public partial class ForceCalculation : Form
    {
        public ForceCalculation()
        {
            InitializeComponent();
        }

        private void ForceCalculation_Load(object sender, EventArgs e)
        {
            cmsection.DataSource = DbHelperSQL.GetDataTable("select SectionCode,Id from dbo.Section");
            cmsection.DisplayMember = "SectionCode";
            cmsection.ValueMember = "Id";
            
        }

        private void PointsBind(string p)
        {
            DataTable dt = DbHelperSQL.GetDataTable("select a.*,b.ProjectName from T_Point a inner join Project_Info b on a.ProjectID=b.Id order by a.Codes");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string[] str = new string[15];
                    str[0] = (i + 1).ToString();
                    str[1] = dr["ProjectName"].ToString();
                    str[2] = dr["PointName"].ToString();
                    str[3] = dr["JoinPointName"].ToString();
                    str[4] = dr["PointFeatures"].ToString();
                    str[5] = dr["Xvalues"].ToString();
                    str[6] = dr["Yvaules"].ToString();
                    str[7] = dr["Zvaules"].ToString();
                    str[8] = dr["Materail"].ToString();
                    dataGridView1.Rows.Add(str);
                }
            }
            dt.Clear();
            dt.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbHelperSQL.ExecuteSql("delete from T_ShouLiFenXi");
            DataTable dt = DbHelperSQL.GetDataTable("select a.*,b.ProjectName from T_Point a inner join Project_Info b on a.ProjectID=b.Id order by a.Codes");
            if (dt.Rows.Count > 0)
            {
                double diffX = 0;
                double diffY = 0;
                string ifhege = "1";
                double totalqianyingli = 0;
                for (int i = 0; i < dt.Rows.Count-1; i++)
                {
                    DataRow dr = dt.Rows[i];
                    DataRow dr2 = dt.Rows[i+1];
                    string endx = dr2["Xvalues"].ToString();
                    string endy = dr2["Yvaules"].ToString();
                    string endz = dr2["Zvaules"].ToString();
                    string startx = dr["Xvalues"].ToString();
                    string starty = dr["Yvaules"].ToString();
                    string startz = dr["Zvaules"].ToString();
                    diffX = Convert.ToDouble(endx) - Convert.ToDouble(startx);
                    diffY = Convert.ToDouble(endy) - Convert.ToDouble(starty);
                    if (diffX < 0)
                    {
                        diffX = 0 - diffX;
                    }
                    if (diffY < 0)
                    {
                        diffY = 0 - diffY;
                    }
                    double angle = Math.Atan2(diffY, diffX) * 180 / Math.PI;  //两个点线的角度
                    //if (angle > 90 & angle <= 180)
                    //{
                    //    angle = 180 - angle;
                    //}
                    //else if (angle < 0 & angle >= -90)
                    //{
                    //    angle = 0 - angle;
                    //}
                    //else if (angle < -90)
                    //{
                    //    angle = 180 + angle;
                    //}
                    double lenghtss = Math.Sqrt(diffX * diffX + diffY * diffY); //两个点的长度
                    var T1 = (9.8) * lenghtss * (0.2 * Math.Cos(angle) + Math.Sin(angle)); //起始牵引力 
                    if (T1 < 0)
                    {
                        T1 = 0 - T1;
                    }
                    var T2 = (9.8) * (lenghtss) * (0.2 * Math.Cos(angle) - Math.Sin(angle)); //最终牵引力
                    if (T2 < 0)
                    {
                        T2 = 0 - T2;
                    }
                    totalqianyingli += T2;
                    var vT2 = T1 + (9.8) * (lenghtss) / Math.Cos(angle) * (0.2 * Math.Cos(angle) - Math.Sin(angle)); // 最终牵引力比较
                    if (vT2 < 0)
                    {
                        vT2 = 0 - vT2;
                    }
                    string jianyi = "";
                    if (Convert.ToDecimal(vT2) > Convert.ToDecimal(300))
                    {
                        ifhege = "0";
                        jianyi = "建议添加滑轮";
                        if (i % 2 == 0)
                        {
                            jianyi = "建议添加输送机";
                        }
                    }
                    else
                    {
                        ifhege = "1";
                    }
                    //vT2
                    string section = dr["PointName"].ToString() + "-" + dr2["PointName"].ToString();
                    string insertsql = "insert into T_ShouLiFenXi values(newid()," + i.ToString() + ",'" + section + "','管沟'," + ifhege + ",'" + jianyi + "','" + dr["Materail"].ToString() + "'," + Convert.ToDecimal(lenghtss).ToString("0.00") + ",'" + startx + "','" + starty + "','" + startz + "','" + endx + "','" + endy + "','" + endz + "','" + Convert.ToDecimal(T1).ToString("0.00") + "','" + Convert.ToDecimal(totalqianyingli).ToString("0.00") + "',getdate())";
                    DbHelperSQL.ExecuteSql(insertsql);                   
                }
                MessageBox.Show("计算完成");
                //加载计算结果数据库表内容
                this.ShowResult();

            }
        }

        private void ShowResult()
        {
            DataTable dt = DbHelperSQL.GetDataTable("select * from T_ShouLiFenXi order by codes");
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
                    dataGridView2.Rows.Add(str);
                }
            }
            dt.Clear();
            dt.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.PointsBind("1732cee8-3dfc-46e5-ad15-21f5670dfa51");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", " --kiosk file:///E:/%E4%B8%AD%E8%88%AA%E4%BF%A1%E6%81%AF/%E7%94%B5%E7%BC%86%E9%93%BA%E8%AE%BE%E7%9B%91%E6%8E%A7/line3d-.html");
        }
    }
}
