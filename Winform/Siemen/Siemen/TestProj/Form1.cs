using PLCComHelperProj;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace TestProj
{
    public partial class Form1 : Form
    {

        private ClientComHelper m_PLCComHelper = new ClientComHelper();

        public Form1()
        {
            InitializeComponent();


            //m_PLCComHelper.TagConfigFile = Path.Combine(Application.StartupPath, "syscfg.xml");

            //m_PLCComHelper.Init();

            //Device s7 = m_PLCComHelper.GetDevice();

            //int count = 0;

            //foreach (KeyValuePair<string, TagGroup> ch in s7.TagGroups)
            //{
            //    TagGroup c = ch.Value;
            //    foreach (KeyValuePair<string, Tag> tag in c.Tags)
            //    {
            //        Tag ta = tag.Value;
            //        string name = string.Format("{0}.{1}", c.Name, ta.Name);
            //        ListViewItem item = new ListViewItem(name);
            //        item.SubItems.Add(ta.Desc);
            //        item.SubItems.Add(ta.DataType);
            //        item.SubItems.Add(ta.GetAddressName());
            //        item.SubItems.Add("");
            //        listView1.Items.Add(item);
            //        count++;
            //    }
            //}
            //toolStripStatusLabel2.Text = count.ToString() + "个"+"    无使用限制";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_PLCComHelper.IP = txtIP.Text;
            
            bool res = m_PLCComHelper.Start();


            if (!res)
            {
                MessageBox.Show("连接失败");
            }
            else
            {
                button1.Enabled = false;

                button2.Enabled = true;

                timer1.Start();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

           timer1.Stop();

           m_PLCComHelper.Stop();

           button1.Enabled = true;

           button2.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            // 0：未连接，1：TCP连接成功，2：PLC握手成功，3：读取过程中
            switch (m_PLCComHelper.CommStatus)
            {
                case 0:
                    label1.Text = "未连接";
                    break;
                case 1:
                    label1.Text = "正在进行TCP连接";
                    break;
                case 2:
                    label1.Text = "TCP连接成功";
                    break;
                case 3:
                    label1.Text = "PLC握手成功";
                    break;
                case 4:
                    label1.Text = "正常采集过程中...";
                    break;
                case 5:
                    label1.Text = "PLC握手错误";
                    break;
                case 6:
                    label1.Text = "通讯错误";
                    break;
                default:
                    break;
            }

            Device s7 = m_PLCComHelper.GetDevice();

            
            foreach (KeyValuePair<string, TagGroup> ch in s7.TagGroups)
            {
                TagGroup c = ch.Value;
                foreach (KeyValuePair<string, Tag> tag in c.Tags)
                {

                    Tag ta = tag.Value;
                    string name = string.Format("{0}.{1}", c.Name, ta.Name);
                    ListViewItem item = listView1.FindItemWithText(name);
                    switch (ta.CheckDataType())
                    {
                        case e_PLC_DATA_TYPE.TYPE_INT:
                            item.SubItems[4].Text = Convert.ToInt32(m_PLCComHelper.GetValue(name)).ToString();
                            break;
                        case e_PLC_DATA_TYPE.TYPE_BYTE:
                            item.SubItems[4].Text = Convert.ToInt32(m_PLCComHelper.GetValue(name)).ToString();
                            break;
                        case e_PLC_DATA_TYPE.TYPE_FLOAT:
                            item.SubItems[4].Text =m_PLCComHelper.GetValue(name).ToString();
                            break;
                        case e_PLC_DATA_TYPE.TYPE_SHORT:
                            item.SubItems[4].Text = Convert.ToInt32(m_PLCComHelper.GetValue(name)).ToString();
                            break;
                        case e_PLC_DATA_TYPE.TYPE_BOOL:
                            item.SubItems[4].Text = Convert.ToInt32(m_PLCComHelper.GetValue(name)).ToString();
                            break;
                        default:
                            break;
                    }
                }
                
            }
            
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_PLCComHelper.Stop();
        }

        private void 修改数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = listView1.SelectedItems[0].SubItems[0].Text;

            Form2 frm = new Form2();

            frm.DataValue = Convert.ToDouble(listView1.SelectedItems[0].SubItems[4].Text=="" ? null : listView1.SelectedItems[0].SubItems[4].Text);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                m_PLCComHelper.WriteData(name, frm.DataValue);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }


    }
}
