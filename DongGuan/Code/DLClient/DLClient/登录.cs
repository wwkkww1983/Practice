using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace DLClient
{
    public partial class 登录 : Form
    {
        public 登录()
        {
            InitializeComponent();
        }
        public int id;

        private void butTc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butQd_Click(object sender, EventArgs e)
        {

        }

        private void 登录_Load(object sender, EventArgs e)
        {
            //LoadXMLinfo();
            Login();
        }
        public void LoadXMLinfo()
        {
            XmlDocument xd = new XmlDocument();
            xd.Load("XMLlogin.xml");
            XmlNode root = xd.DocumentElement;
            this.Text = root.ChildNodes[0].InnerText;

            label2.Text = root.ChildNodes[2].InnerText;
            label3.Text = root.ChildNodes[3].InnerText;

        }

        private void denglu_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            //if (textYhm.Text.Trim() != "admin" || textMm.Text.Trim() != "123456")
            //{
            //    MessageBox.Show("用户名或者密码有误！");
            //}
            //else
            //{
                this.Hide();
                FormShow();
            //}
        }

        private void FormShow()
        {
            Form1 form = new Form1(this);
            form.ShowDialog();
        }

        private void 登录_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }
    }
}
