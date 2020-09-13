using System;
using System.Windows.Forms;

namespace HikvisionDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnToken_Click(object sender, EventArgs e)
        {
            var url = txtUrl.Text;
            var res = await HttpHelper.Get(url);
            if (res.IsSuccessStatusCode)
            {
                txtResult.Text = await res.Content.ReadAsStringAsync();
            }
        }
    }
}
