using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.ThreadPool;
using Caist.Siemens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Caist.Framework.Service
{
    public partial class TestForm : Form
    {
        private List<SiemensHelpers> _siemensHelpers = new List<SiemensHelpers>();
        public TestForm()
        {
            InitializeComponent();
        }

        string _ip = string.Empty, _instruct = string.Empty, _instructType = string.Empty;
        string _value = string.Empty;
        SiemensHelpers _helper;
        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                _instruct = txtInstruct.Text;
                _instructType = GetCodeForText(ddlType.SelectedItem.ToString());
                if (JudgeContent())
                {
                    if (!_instructType.HasValue())
                    {
                        MessageBox.Show("instruct type required input！");
                        return;
                    }
                    if (GetSendDataType(_instructType) == SendDataType.Short)
                    {
                        _value = _helper.Read(_instruct);
                    }
                    else
                    {
                        _value = _helper.Read(_instruct, GetSendDataType(_instructType));
                    }
                    rtbContent.AppendText(_value + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetCodeForText(string v)
        {
            string code = string.Empty;   
            switch (v)
            {
                case "SHORT":
                    code = "0";
                    break;
                case "UINT32":
                    code = "1";
                    break;
                case "USHORT":
                    code = "2";
                    break;
                case "INT":
                    code = "3";
                    break;
                case "FLOAT":
                    code = "4";
                    break;
                default:
                    break;
            }
            return code;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            DataServices.LoadDataDevice();
            DataServices.LoadDataTagGroup();
            DataServices.LoadDataTag();
            SiemensInit();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                _ip = txtIP.Text;
                if (_ip.HasValue())
                {
                    _helper = _siemensHelpers.FirstOrDefault(p => p.IP == _ip);
                    ConnectPlc(_helper);
                }
                else
                {
                    MessageBox.Show("please input IP address!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ConnectPlc(SiemensHelpers helper)
        {
            var result = helper.Open(helper.DeviceEntity.PLCType, helper.DeviceEntity.Host, short.Parse(helper.DeviceEntity.Port), 0, short.Parse(helper.DeviceEntity.CPU_SlotNO));
            if (!result)
            {
                lblPlcStatus.ForeColor = Color.Red;
                lblPlcStatus.Text = "Not Connected";
            }
            else
            {
                lblPlcStatus.ForeColor = Color.Green;
                lblPlcStatus.Text = "Connected";
            }
        }
        private void SiemensInit()
        {
            PublicEntity.DeviceEntities.ForEach(d =>
            {
                SiemensHelpers siemensHelper = new SiemensHelpers();
                siemensHelper.DeviceEntity = d;
                siemensHelper.IP = d.Host;
                siemensHelper.Port = d.Port;
                siemensHelper.Init();
                _siemensHelpers.Add(siemensHelper);
            });
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                if (JudgeContent())
                {
                    if (!_value.HasValue())
                    {
                        MessageBox.Show("please input your value!！");
                        return;
                    }
                    var msg = MessageBox.Show("are you sure input this instruct？", "waring", MessageBoxButtons.OKCancel);
                    if (msg == DialogResult.OK)
                    {
                        _helper.Write(_instruct, _value);
                        rtbContent.AppendText($"{_instruct}:input success！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbContent.Text = string.Empty;
        }

        private SendDataType GetSendDataType(string dataType)
        {
            SendDataType type = SendDataType.Float;
            switch (dataType)
            {
                case "0":
                    type = SendDataType.Short;
                    break;
                case "1":
                    type = SendDataType.UInt32;
                    break;
                case "2":
                    type = SendDataType.Ushort;
                    break;
                case "3":
                    type = SendDataType.Int;
                    break;
                case "4":
                    type = SendDataType.Float;
                    break;
                default:
                    break;
            }
            return type;
        }

        private bool JudgeContent()
        {
            bool flag = true;
            string msg = string.Empty;
            if (!_ip.HasValue())
            {
                msg += "ip address required input！\r\n";
            }
            if (!_instruct.HasValue())
            {
                msg += "instruct  required input！\r\n";
            }
            if (msg.HasValue())
            {
                flag = false;
                MessageBox.Show(msg);
            }
            return flag;
        }
    }
}
