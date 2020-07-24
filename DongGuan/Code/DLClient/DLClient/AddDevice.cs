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
    public partial class AddDevice : Form
    {
        public AddDevice()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddDevice_Load(object sender, EventArgs e)
        {
            this.SectionBind();
            this.DeviceBind();
        }

        private void DeviceBind()
        {
            comboBox2.DataSource = DbHelperSQL.GetDataTable("select distinct Name,EquipmentTypeId from Equipment");
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "EquipmentTypeId";
        }

        private void SectionBind()
        {
            comboBox1.DataSource = DbHelperSQL.GetDataTable("select ID,Section from T_ShouLiFenXi where IfQualify=0");
            comboBox1.DisplayMember = "Section";
            comboBox1.ValueMember = "ID";
        }
    }
}
