using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caist.Framework.Service
{
    public partial class FrmMian_New : Form
    {
        private Dictionary<string, PlcExtends> _siemensHelpers = new Dictionary<string, PlcExtends>();
        private List<NewTimer> _newTimers = new List<NewTimer>();
        public FrmMian_New()
        {
            InitializeComponent();
        }

        private void FrmMian_New_Load(object sender, EventArgs e)
        {
            //加载plc列表
            DataServices.LoadDataDevice(TreeDevice.Nodes);
            //加载PLC内存块长度配置信息列表
            DataServices.LoadAllAddress();
            //实例化绑定的PLC列表对象
            InitPlc();
            //DataServices.LoadDataTag();
        }

        private void InitPlc()
        {
            PublicEntity.DeviceEntities.ForEach((p) =>
            {
                var helper = new PlcExtends(CpuType.S71200, p.Host, Convert.ToInt16(0),
               Convert.ToInt16(p.CPU_SlotNO));
                _siemensHelpers.Add(p.Host, helper);
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            foreach (var p in _siemensHelpers)
            {
                Task.Run(() =>
                {
                    Task.Delay(1000);
                    try
                    {
                        //调用S7.NET中的方法连接PLC
                        p.Value.Open();
                        //连接成功后使能操作按钮
                        if (p.Value.IsConnected)
                        {
                            if (_newTimers.FindIndex(t => p.Key.Contains(t.obj.IP)) == -1)
                            {
                                NewTimer timerMessage = new NewTimer() { Interval = 1000 };
                                timerMessage.Elapsed += TimerMessage_Elapsed;
                                timerMessage.obj = p.Value;
                                timerMessage.Start();
                                _newTimers.Add(timerMessage);
                            }
                            else//存在就启动
                            {
                                _newTimers.FindAll(t => p.Key.Contains(t.obj.IP)).ForEach(s =>//找到相关的plc设备把定时器打开
                                {
                                    s.Start();
                                });
                            }

                            SetContent("已连接到PLC");
                        }
                        else
                        {
                            SetContent("PLC 连接不成功，请检查IP地址、机架、插槽等是否正确");
                        }
                    }
                    catch (Exception ex)
                    {
                        SetContent("PLC 连接不成功，请检查IP地址、机架、插槽等是否正确");
                    }
                });
            }
        }

        private void TimerMessage_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var timer = (NewTimer)sender;
            var addrs = PublicEntity.InstructAddrs.FindAll(t => t.DeviceHost == timer.obj.IP && t.DevicePort == timer.obj.Port.ToString());
            addrs.ForEach((s) =>//获取数据库地址
            {
                var v = timer.obj.Read(s.Addr);
                richTextBox1.Invoke(new Action(() =>//设置文本框值
                {
                    richTextBox1.Text += v.ToString() + "\n";
                }));
            });
        }

        private void SetContent(string msg)
        {
            richTextBox1.Invoke(new Action(() =>//设置文本框值
            {
                richTextBox1.Text += msg + "\n";
            }));
        }

        //private void ReadValue()
        //{
        //    int Data_Type_Value = 0;
        //    if (Data_Type.Text == "Bool") Data_Type_Value = 1;
        //    else if (Data_Type.Text == "Int") Data_Type_Value = 2;
        //    else if (Data_Type.Text == "DInt") Data_Type_Value = 3;
        //    else if (Data_Type.Text == "Real") Data_Type_Value = 4;
        //    else Data_Type_Value = 0;

        //    switch (Data_Type_Value)
        //    {
        //        case 1:
        //            Current_Value.Text = Convert.ToString(S71500.Read(DataType.DataBlock,
        //            Convert.ToInt16(DB_Number.Text), Convert.ToInt16(Start_Address.Text), VarType.Bit, 1, 0));
        //            break;
        //        case 2:
        //            Current_Value.Text = Convert.ToString(S71500.Read(DataType.DataBlock,
        //            Convert.ToInt16(DB_Number.Text), Convert.ToInt16(Start_Address.Text), VarType.Int, 1, 0));
        //            break;
        //        case 3:
        //            Current_Value.Text = Convert.ToString(S71500.Read(DataType.DataBlock,
        //            Convert.ToInt16(DB_Number.Text), Convert.ToInt16(Start_Address.Text), VarType.DInt, 1, 0));
        //            break;
        //        case 4:
        //            Current_Value.Text = Convert.ToString(S71500.Read(DataType.DataBlock,
        //            Convert.ToInt16(DB_Number.Text), Convert.ToInt16(Start_Address.Text), VarType.Real, 1, 0));
        //            break;
        //        default: break;
        //    }
        //}
    }
    public class NewTimer : System.Timers.Timer
    {
        public string State { get; set; }
        public string ClientFlag { get; set; }
        public PlcExtends obj { get; set; }
    }
    public class PlcExtends : Plc
    {
        public PlcExtends(CpuType cpu, string ip, short rack, short slot) : base(cpu, ip, rack, slot)
        {

        }
        public DeviceEntity deviceEntity { get; set; }
    }
}
