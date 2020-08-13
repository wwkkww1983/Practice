using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using S7.Net;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //定义PLC类型
        Plc S71500;      
        public Form1()
        {
            InitializeComponent();
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            //下拉框添加数据类型项
            Data_Type.Items.Add("Bool");
            Data_Type.Items.Add("Int");
            Data_Type.Items.Add("DInt");
            Data_Type.Items.Add("Real");
            Data_Type.Text = "Real";

            //使能、禁止按钮操作
            button1.Enabled = true;
            button2.Enabled = false;
            Read_Data.Enabled = false;
            Write_Data.Enabled = false;
        }
        
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //创建PLC对象
            //S71500 = new Plc(CpuType.S71500, IP_Address.Text, Convert.ToInt16(Rack.Text),
            //    Convert.ToInt16(Slot.Text));
            S71500 = new Plc(CpuType.S71200, IP_Address.Text, Convert.ToInt16(Rack.Text),
                Convert.ToInt16(Slot.Text));

            //调用S7.NET中的方法连接PLC
            S71500.Open();

            //连接成功后使能操作按钮
            if (S71500.IsConnected)
            {
                button1.Enabled = false;
                button2.Enabled = true;
                Read_Data.Enabled = true;
                Write_Data.Enabled = true;
                textBox1.Text = "已连接到PLC";
            }
            else
                textBox1.Text = "PLC 连接不成功，请检查IP地址、机架、插槽等是否正确";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //调用S7.NET中的方法断开PLC
            S71500.Close();

            //断开成功后使能操作按钮
            if (!S71500.IsConnected)
            {
                button1.Enabled = true;
                button2.Enabled = false;
                Read_Data.Enabled = false;
                Write_Data.Enabled = false;
                textBox1.Text = "PLC断开成功";
            }
           else
                textBox1.Text = "PLC断开不成功";

        }

        private void Read_Data_Click(object sender, EventArgs e)
        {
            int Data_Type_Value = 0;
            if (Data_Type.Text == "Bool") Data_Type_Value = 1;
            else if (Data_Type.Text == "Int") Data_Type_Value = 2;
            else if (Data_Type.Text == "DInt") Data_Type_Value = 3;
            else if (Data_Type.Text == "Real") Data_Type_Value = 4;
            else Data_Type_Value = 0;

            switch (Data_Type_Value)
            {
                case 1:
                     Current_Value.Text = Convert.ToString(S71500.Read(DataType.DataBlock,
                     Convert.ToInt16(DB_Number.Text), Convert.ToInt16(Start_Address.Text), VarType.Bit, 1, 0));                  
                     break;
                case 2:
                    Current_Value.Text = Convert.ToString(S71500.Read(DataType.DataBlock,
                    Convert.ToInt16(DB_Number.Text), Convert.ToInt16(Start_Address.Text), VarType.Int, 1, 0));
                    break;
                case 3:
                    Current_Value.Text = Convert.ToString(S71500.Read(DataType.DataBlock,
                    Convert.ToInt16(DB_Number.Text), Convert.ToInt16(Start_Address.Text), VarType.DInt, 1, 0));
                    break;
                case 4:
                    Current_Value.Text = Convert.ToString(S71500.Read(DataType.DataBlock,
                    Convert.ToInt16(DB_Number.Text), Convert.ToInt16(Start_Address.Text), VarType.Real, 1, 0));
                    break;
                default:  break;
            }        

        }

        private void Write_Data_Click(object sender, EventArgs e)
        {
            int Data_Type_Value = 0;
            if (Data_Type.Text == "Bool") Data_Type_Value = 1;
            else if (Data_Type.Text == "Int") Data_Type_Value = 2;
            else if (Data_Type.Text == "DInt") Data_Type_Value = 3;
            else if (Data_Type.Text == "Real") Data_Type_Value = 4;
            else Data_Type_Value = 0;

           
            switch (Data_Type_Value)
            {
               
                case 1:
                    S71500.WriteBit(DataType.DataBlock, Convert.ToInt16(DB_Number.Text),
                   Convert.ToInt16(Start_Address.Text),0, Convert.ToBoolean(Write_Value.Text));
                    break;
                case 2:
                   S71500.Write(DataType.DataBlock, Convert.ToInt16(DB_Number.Text),
                   Convert.ToInt16(Start_Address.Text), Convert.ToInt16(Write_Value.Text));
                    break;
                case 3:
                    S71500.Write(DataType.DataBlock, Convert.ToInt16(DB_Number.Text),
                    Convert.ToInt16(Start_Address.Text), Convert.ToInt32(Write_Value.Text));
                    break;
                case 4:
                    S71500.Write(DataType.DataBlock, Convert.ToInt16(DB_Number.Text),
                    Convert.ToInt16(Start_Address.Text), Convert.ToDouble(Write_Value.Text));
                    break;
                default: break;
            }
        }
       
    }
}
