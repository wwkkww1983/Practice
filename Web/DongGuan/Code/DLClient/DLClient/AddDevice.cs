using System;
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
            comboBox1.DataSource = DbHelperSQL.GetDataTable("select ID,Section from ForceAnalysis where IfQualify=0");
            comboBox1.DisplayMember = "Section";
            comboBox1.ValueMember = "ID";
        }
    }
}
