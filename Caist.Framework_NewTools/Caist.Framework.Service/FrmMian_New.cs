using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.Entity.Enum;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caist.Framework.Service
{
    public class FrmMian_New : Form
    {
        private Dictionary<string, PlcExtends> _siemensHelpers = new Dictionary<string, PlcExtends>();
        private TreeView TreeDevice;
        private Control.ListViewLoad lvPoint;
        private RichTextBox txtMessage;
        private Button btnStart;
        private List<NewTimer> _newTimers = new List<NewTimer>();

        private void FrmMian_New_Load(object sender, EventArgs e)
        {
            //加载plc列表
            DataServices.LoadDataDevice(TreeDevice.Nodes);
            //加载PLC内存块长度配置信息列表
            DataServices.LoadAllAddress();
            //加载PLC内存块长度配置信息列表
            DataServices.LoadDataTagGroup();
            //加载PLC后台配置内存地址指向表
            DataServices.LoadDataTag();
            //实例化绑定的PLC列表对象
            InitPlc();

            InitGrid();
            //DataServices.LoadDataTag();
        }

        private void InitPlc()
        {
            PublicEntity.DeviceEntities.ForEach((p) =>
            {
                //TODO:plc型号要进行动态配置
                var helper = new PlcExtends(CpuType.S71200, p.Host, Convert.ToInt16(0),
               Convert.ToInt16(p.CPU_SlotNO));
                _siemensHelpers.Add(p.Host, helper);
            });
        }

        /// <summary>
        /// 加载指令
        /// </summary>
        public void InitGrid()
        {
            try
            {
                foreach (DeviceEntity device in PublicEntity.DeviceEntities)//设备集合
                {
                    PublicEntity.TagGroupsEntities.FindAll(p => p.DeviceId == device.Id).ForEach((g) =>
                    {
                        PublicEntity.TagEntities.FindAll(p => p.TagGroup == g.Id).ForEach((instructEntity) =>
                        {
                            string Name = string.Format("{2}-{0}.{1}", g.Name, instructEntity.Name, device.Host);
                            //string Key = string.Format("{0}.{1}", g.Id, instructEntity.Id);
                            ListViewItem lvitem = new ListViewItem(Name);
                            var addr = PublicEntity.InstructAddrs.Find(t => t.Id == instructEntity.Id).Addr;
                            lvitem.ToolTipText = Name;
                            lvitem.SubItems.Add(instructEntity.Desc);
                            lvitem.SubItems.Add(instructEntity.DataType);
                            lvitem.SubItems.Add(addr);
                            lvitem.SubItems.Add("");
                            lvitem.SubItems.Add(Extensions.ConvertOutPutEnum(int.Parse(instructEntity.Output)));
                            lvitem.Tag = instructEntity;
                            lvPoint.Items.Add(lvitem);
                            lvPoint.Columns[0].Width = -1;
                        });
                    });
                }
            }
            catch (Exception)
            {
            }
        }

        private void TimerMessage_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var timer = (NewTimer)sender;
            PublicEntity.InstructAddrs.FindAll(t => t.DeviceHost == timer.obj.IP && t.DevicePort == timer.obj.Port.ToString()).ForEach((s) =>//获取数据库地址
            {
                var v = timer.obj.Read(s.Addr);
                string key = string.Format("{0}-{1}", s.DeviceHost, s.Addr);
                ListViewItem item = lvPoint.FindItemWithText(key);
                item.SubItems[4].Text = v.ToString();
            });
        }

        private void SetContent(string msg)
        {
            txtMessage.Invoke(new Action(() =>//设置文本框值
            {
                txtMessage.Text += msg + "\n";
            }));
        }

        private void InitializeComponent()
        {
            this.TreeDevice = new System.Windows.Forms.TreeView();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.lvPoint = new Caist.Framework.Service.Control.ListViewLoad();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TreeDevice
            // 
            this.TreeDevice.Location = new System.Drawing.Point(12, 49);
            this.TreeDevice.Name = "TreeDevice";
            this.TreeDevice.Size = new System.Drawing.Size(172, 563);
            this.TreeDevice.TabIndex = 0;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(190, 427);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(723, 185);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.Text = "";
            // 
            // lvPoint
            // 
            this.lvPoint.HideSelection = false;
            this.lvPoint.Location = new System.Drawing.Point(190, 49);
            this.lvPoint.Name = "lvPoint";
            this.lvPoint.Size = new System.Drawing.Size(723, 372);
            this.lvPoint.TabIndex = 1;
            this.lvPoint.UseCompatibleStateImageBehavior = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // FrmMian_New
            // 
            this.ClientSize = new System.Drawing.Size(925, 633);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lvPoint);
            this.Controls.Add(this.TreeDevice);
            this.Name = "FrmMian_New";
            this.Load += new System.EventHandler(this.FrmMian_New_Load);
            this.ResumeLayout(false);

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
        public PlcExtends(CpuType cpu, string ip, short rack, short slot) : base(cpu, ip, rack, slot) { }
        public DeviceEntity deviceEntity { get; set; }
    }
}
