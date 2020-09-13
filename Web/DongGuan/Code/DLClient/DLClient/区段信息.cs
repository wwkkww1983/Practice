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
    public partial class 区段信息 : Form
    {
        public 区段信息()
        {
            InitializeComponent();
        }

        private void 设备信息_Load(object sender, EventArgs e)
        {
            Fill();
        }
        public void Fill()
        {
            //string sql = string.Format(@"select Name,PointName,ModelNumber,ECode,BarCode,Weight,Units from CL_Equipment order by Name");
            //DataTable dt = DbHelperSQL.GetDataTable(sql);
            //dataGridView1.DataSource = dt;
            DataTable dt = DbHelperSQL.GetDataTable("select * from CL_Section order by SectionCode");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string[] str = new string[9];
                    str[0] = dr["SectionCode"].ToString();
                    str[1] = dr["Distance"].ToString();
                    str[2] = dr["Location"].ToString();
                    str[3] = dr["State"].ToString();
                    str[4] = dr["DefaultEquipmentDistance"].ToString();
                    str[7] = dr["ID"].ToString();
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
            this.dataGridView1.Rows.Add("", "", "", "", "", "", "");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex > -1)
            {
                DataGridViewButtonCell btnCell = dataGridView1.CurrentCell as DataGridViewButtonCell;
                if (btnCell != null)
                {
                    OperationMode operationMode = OperationMode.View;
                    switch (btnCell.Value)
                    {
                        case "编辑":
                            operationMode = OperationMode.Edit;
                            break;
                    }

                    var sectionId = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    SectionRoute form = new SectionRoute(new MapControlModel() { 
                        operationMode = operationMode,
                        SectionId = sectionId
                    });
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }
        }
    }
}
