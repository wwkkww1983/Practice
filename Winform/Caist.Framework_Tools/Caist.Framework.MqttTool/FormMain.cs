using Caist.Framework.M2MQTT;
using Microsoft.Win32;
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
            //加入/验证开机启动项
#if !DEBUG
            SetAutoRun("MQTT上传", Application.StartupPath + "\\MQTT上传.exe");
#endif

            try
            {
                //初始化mqtt配置信息
                LoadMqtt(sender, e);

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


        public bool SetAutoRun(string keyName, string filePath)
        {
            try
            {
                RegistryKey Local = Registry.LocalMachine;
                RegistryKey runKey = Local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\");
                if (runKey.GetValue(keyName) == null)
                {

                    runKey.SetValue(keyName, filePath);
                    Message("添加程序开启自启动成功！");
                }
                else
                {
                    //runKey.DeleteValue(keyName);
                    Message("本程序已加入开机自启动项！");
                }
                Local.Close();

            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
