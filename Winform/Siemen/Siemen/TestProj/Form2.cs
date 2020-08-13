using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestProj
{
    public partial class Form2 : Form
    {



        public double DataValue
        {
            get
            {

                return Convert.ToDouble(textBox1.Text.Trim());
            }
            set
            {

                textBox1.Text = value.ToString();
            }
        }



        public Form2()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (!isNumberic(textBox1.Text))
            {
                MessageBox.Show("请输入数值");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }


        private bool isNumberic(string message)
        {

            if (message == "")
            {
                return false;
            }
            else
            {
                System.Text.RegularExpressions.Regex m_regex = new System.Text.RegularExpressions.Regex("^(-?[0-9]*[.]*[0-9]{0,5})$");
                return m_regex.IsMatch(message);
            }

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
