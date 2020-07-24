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
    public partial class DataMonitor : Form
    {
        public DataMonitor()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rNumber = new Random();
            int addvalues = rNumber.Next(1, 10);
            Decimal valuess = Convert.ToDecimal(addvalues / 10);
            label5.Text = Convert.ToDouble(2 + valuess).ToString("0.00") + "r/min";

            label6.Text = 34659 + addvalues + "N";
            label7.Text = Convert.ToDecimal((20 + addvalues)/10).ToString("0.00") + "米";
            label8.Text = Convert.ToDecimal(6 + valuess).ToString("0.00") + "米/分";
        }

        private void DataMonitor_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
