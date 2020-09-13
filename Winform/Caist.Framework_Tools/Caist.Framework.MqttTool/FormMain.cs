using Caist.Framework.M2MQTT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caist.Framework.Mqtt
{
    public partial class FrmMian : Form
    {
        public static FrmMian MyForm;
        private M2MqttServer M2Mqtt;
        public FrmMian()
        {
            InitializeComponent();
            MyForm = this;
        }

        public void LimitLine(int maxLength, RichTextBox richTextBox)
        {
            if (richTextBox.Lines.Length > maxLength)
            {
                int moreLines = richTextBox.Lines.Length - maxLength;
                string[] lines = richTextBox.Lines;
                Array.Copy(lines, moreLines, lines, 0, maxLength);
                Array.Resize(ref lines, maxLength);
                richTextBox.Lines = lines;
            }
        }

        private void FrmMian_Load(object sender, EventArgs e)
        {
            try
            {
                //初始化mqtt配置信息
                LoadMqtt();
            }
            catch (Exception ex)
            {

                M2Mqtt.MqtMessage($"初始化配置错误" + ex.Message);
            }

        }

        private void FrmMian_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.MqttStart.Enabled = true;
            this.MqttStop.Enabled = false;
            StopMqtt(e);
        }

        private void FrmMian_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MqttStart.Enabled = true;
            this.MqttStop.Enabled = false;
            StopMqtt(e);
        }
    }
}
