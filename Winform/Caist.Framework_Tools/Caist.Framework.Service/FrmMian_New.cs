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
        private List<Plc> _siemensHelpers = new List<Plc>();
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
            //加载PLC后台配置内存地址指向表
            //DataServices.LoadDataTag();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            PublicEntity.DeviceEntities.ForEach((p) =>
            {
                Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    try
                    {
                        var plcHelper = new Plc(CpuType.S71200, p.Host, Convert.ToInt16(0),
                           Convert.ToInt16(p.CPU_SlotNO));

                        //调用S7.NET中的方法连接PLC
                        await plcHelper.OpenAsync();

                        //连接成功后使能操作按钮
                        if (plcHelper.IsConnected)
                        {
                            var addrs = PublicEntity.InstructAddrs.FindAll(t => t.DeviceHost == p.Host && t.DevicePort == p.Port);
                            addrs.ForEach(async (s) =>//获取数据库地址
                            {
                                var v = await plcHelper.ReadAsync(s.Addr);
                                richTextBox1.Invoke(new Action(() =>//设置文本框值
                                {
                                    richTextBox1.Text = v.ToString();
                                }));
                            });
                            richTextBox1.Text += "已连接到PLC\n";
                        }
                        else
                            richTextBox1.Text += "PLC 连接不成功，请检查IP地址、机架、插槽等是否正确\n";
                        _siemensHelpers.Add(plcHelper);
                    }
                    catch (Exception ex)
                    {
                        richTextBox1.Text += "PLC 连接不成功，请检查IP地址、机架、插槽等是否正确\n";
                    }
                });
            });
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
}
