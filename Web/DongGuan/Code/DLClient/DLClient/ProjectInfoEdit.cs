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
    public partial class ProjectInfoEdit : Form
    {
        public ProjectInfoEdit()
        {
            InitializeComponent();
        }

        private void ProjectInfoEdit_Load(object sender, EventArgs e)
        {
            ProjectInfoShow("1732cee8-3dfc-46e5-ad15-21f5670dfa51");
        }

        private void ProjectInfoShow(string pid)
        {
            DataTable dt = DbHelperSQL.GetDataTable("select * from Project_Info where Id='" + pid + "'");
            if (dt.Rows.Count > 0)
            {
                txtprjcode.Text = dt.Rows[0]["ProjectCode"].ToString();
                txtprjname.Text = dt.Rows[0]["ProjectName"].ToString();
                txtaddress.Text = dt.Rows[0]["Location"].ToString();
                txtsectionnums.Text = dt.Rows[0]["SectionCount"].ToString();
                txtdistance.Text = dt.Rows[0]["Distance"].ToString();
                txtcontacts.Text = dt.Rows[0]["BuildContacts"].ToString();
                txtmobile.Text = dt.Rows[0]["Phone"].ToString();
            }
            dt.Clear();
            dt.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ProjectCode = txtprjcode.Text.Trim();
            string ProjectName = txtprjname.Text.Trim();
            string Location = txtaddress.Text.Trim();
            string SectionCount = txtsectionnums.Text.Trim();
            string Distance = txtdistance.Text.Trim();
            string Contacts = txtcontacts.Text.Trim();
            string Phone = txtmobile.Text.Trim();
            DbHelperSQL.ExecuteSql("update Project_Info set ProjectCode='" + ProjectCode + "',ProjectName='" + ProjectName + "',Location='" + Location + "',SectionCount='" + SectionCount + "',Distance='" + Distance + "',Contacts='" + Contacts + "',Phone='" + Phone + "' where ID='1732cee8-3dfc-46e5-ad15-21f5670dfa51'");
            MessageBox.Show("编辑成功");
        }
    }
}
