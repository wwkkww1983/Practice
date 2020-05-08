using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace PLCComHelperProj
{

	public class Device
	{
        #region   字段
        ///<summary >”1”: S7-200/smart系列，”2”： S7-300/400/1200/1500系列；</summary>
        private string PLCtype;
        ///<summary >CPU所在的槽号，S7-300的PLC一般都为"02"，S7-400的PLC一般都为"03", S7-200/1200/1500的PLC一般都为"01"</summary>
        private string cPU_SlotNO;
        ///<summary >S7-200/Smart需要用的参数，S7-200："10 11"，Smart："02 01" 。其他PLC忽略</summary>
        private string localTASP;
        ///<summary >S7-200/Smart需要用的参数，S7-200："10 01"，Smart："02 00"。其他PLC忽略</summary>
		private string remoteTASP;
        private Socket socket;
        private Queue<WriteData> queue;//表示对象的先进先出集合
        private Dictionary<string, TagGroup> tagGroups;
        #endregion
        #region  PLC连接属性
        ///<summary >”1”: S7-200/smart系列，”2”： S7-300/400/1200/1500系列</summary>
        public string PLCType
		{
			get
			{
				return this.PLCtype;
			}
			set
			{
				this.PLCtype = value;
			}
		}
        ///<summary >CPU所在的槽号，S7-300的PLC一般都为"02"，S7-400的PLC一般都为"03", S7-200/1200/1500的PLC一般都为"01"</summary>
		public string CPU_SlotNO
		{
			get
			{
				return this.cPU_SlotNO;
			}
			set
			{
				this.cPU_SlotNO = value;
			}
		}
        ///<summary >S7-200/Smart需要用的参数，S7-200："10 11"，Smart："02 01" 。其他PLC忽略</summary>
		public string LocalTASP
        {
			get
			{
				return this.localTASP;
			}
			set
			{
				this.localTASP = value;
			}
		}
        ///<summary >S7-200/Smart需要用的参数，S7-200："10 01"，Smart："02 00"。其他PLC忽略</summary>
		public string RemoteTASP
        {
			get
			{
				return this.remoteTASP;
			}
			set
			{
				this.remoteTASP = value;
			}
		}

        public Dictionary<string, TagGroup> TagGroups
        {
            get
            {
                return this.tagGroups;
            }
            set
            {
                this.tagGroups = value;
            }
        }
        #endregion
        public Device() : base()//构造函数
        {
            PLCtype = FuckProtect.DataFrom(1736);//2 系列
            cPU_SlotNO = FuckProtect.DataFrom(1310);//02 槽号
            localTASP = "";
            remoteTASP = "";
            queue = new Queue<WriteData>(11);
            tagGroups = new Dictionary<string, TagGroup>();

        }

        ///<summary >清除载入数据</summary>
		public void Clear()
		{
			this.tagGroups.Clear();
		}
		public void SetDataBuff(string name, byte[] buff, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this.tagGroups[name].SetData(i, buff[i]);
			}
		}		
		public bool ProcessPLCData(Socket socket, bool isStop)
		{
			bool result;
			try
			{
				this.socket = socket;
				foreach (KeyValuePair<string, TagGroup> keyValuePair in this.tagGroups)
				{
					if (isStop)
					{
						break;
					}
					while (this.IsNeedToWrite())//发送数据集合内的数据
					{
						WriteData writeData_ = this.queue.Dequeue();//从集合内取数据
						this.WriteD(writeData_);
						Thread.Sleep(20);
						this.WriteOK();//
					}
					TagGroup value = keyValuePair.Value;
					this.SendData(value);
					Thread.Sleep(20);
					this.ReceiveData(value);
				}
				result = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(FuckProtect.DataFrom(870) + ex.Message);
				result = false;
			}
			return result;
		}
        ///<summary >连接plc</summary>
        public bool ConnectToPLC(Socket socket)//握手信息
		{
			bool result;
			try
			{
				bool flag = false;
				socket.Send(StrTobyte.StrToArraybyte(this.SelectPLC()));
				Thread.Sleep(20);
				byte[] buffer = new byte[1024];
				int num = socket.Receive(buffer);
				if (num == 22)
				{
					socket.Send(StrTobyte.StrToArraybyte(FuckProtect.DataFrom(884)));
					Thread.Sleep(20);
					num = socket.Receive(buffer);
					if (num == 27)
					{
						flag = true;
					}
				}
				result = flag;
			}
			catch (Exception ex)
			{
				Console.WriteLine(FuckProtect.DataFrom(1036) + ex.Message);
				result = false;
			}
			return result;
		}
        ///<summary >将需要发送的数据存入集合</summary>
        public void AddWriteData(TagGroup tagGroup_0, Tag tag_0, double double_0)
        {
            WriteData item = new WriteData(tagGroup_0, tag_0, double_0);
            this.queue.Enqueue(item);
        }
        ///<summary >检查集合里是否有数据需要发送</summary>
        public bool IsNeedToWrite()
        {
            return this.queue.Count > 0;
        }






        ///<summary >选择PLC类型、确定握手数据</summary>
        private string SelectPLC()
        {
            if (this.PLCtype == FuckProtect.DataFrom(602))//”1”: S7-200/smart系列，执行
            {
                return string.Format(FuckProtect.DataFrom(608), this.remoteTASP, this.localTASP);//数据合成
            }
            return string.Format(FuckProtect.DataFrom(734), cPU_SlotNO);//”2”： S7-300/400/1200/1500系列
        }

        ///<summary >判断数据类型</summary>
        private string DataType(TagGroup tagGroup)
		{
			e_PLC_MMType e_PLC_MMType = tagGroup.Get_MMtype();
			string result = "00 00";
			if (e_PLC_MMType == e_PLC_MMType.MM_DB)
			{
				result = this.ShortTOstr((short)tagGroup.Block);
			}
			else if (e_PLC_MMType == e_PLC_MMType.MM_V)
			{
				result = "00 01";//00 01
            }
			else
			{
				result = "00 00";//00 01
            }
			return result;
		}



        ///<summary >发送数据格式</summary>
        private void SendData(TagGroup tagGroup)
        {
            if (!this.socket.Connected)
            {
                return;
            }
            int startAddress = tagGroup.StartAddress;
            int readCount = tagGroup.ReadCount;
            string text = this.DataType(tagGroup);
            string text2 = this.ShortTOstr((short)(startAddress * 8));
            string text3 = Convert.ToString(readCount, 16);
            string text4;
            tagGroup.get_MMtype(out text4);
            string text5 = string.Format(FuckProtect.DataFrom(1058), new object[]
            {
                text2,
                text3,
                text4,
                text
            });
            this.socket.Send(StrTobyte.StrToArraybyte(text5));
        }
        ///<summary >接收数据</summary>
        private void ReceiveData(TagGroup tagGroup)
		{
			if (!this.socket.Connected)
			{
				return;
			}
			int readCount = tagGroup.ReadCount;
			byte[] array = new byte[1024];
			int num = this.socket.Receive(array);
			int num2 = 25 + readCount;
			if (num == num2)
			{
				for (int i = 0; i < readCount; i++)
				{
					tagGroup.SetData(i, array[25 + i]);
				}
				tagGroup.SetBuff(array, 25);
			}
		}



        ///<summary >写入数据</summary>
        private bool WriteD(WriteData writeData_0)
		{
			bool result = true;
			if (!this.socket.Connected)
			{
				return false;
			}
			TagGroup tagGroup = writeData_0.TagGroup;
			string text = FuckProtect.DataFrom(1270);
			string text2 = FuckProtect.DataFrom(1278);
			string text3 = FuckProtect.DataFrom(1286);
			string text4 = FuckProtect.DataFrom(1286);
			string text5 = this.DataType(tagGroup);
			string text6 = FuckProtect.DataFrom(1242);
			string text7 = FuckProtect.DataFrom(1294);
			string text8 = FuckProtect.DataFrom(1256);
			string text9 = FuckProtect.DataFrom(1302);
			switch (writeData_0.Tag.CheckDataType())
			{
			case e_PLC_DATA_TYPE.TYPE_UNKNOW:
				return false;
			case e_PLC_DATA_TYPE.TYPE_INT:
			{
				text = FuckProtect.DataFrom(1370);
				text2 = FuckProtect.DataFrom(1378);
				text3 = FuckProtect.DataFrom(1310);
				text4 = FuckProtect.DataFrom(1318);
				text7 = FuckProtect.DataFrom(1318);
				text8 = FuckProtect.DataFrom(1386);
				short num = Convert.ToInt16(writeData_0.Tag.Address);
				text6 = this.ShortTOstr(Convert.ToInt16((int)(num * 8)));
				text9 = this.IntTOstr((int)writeData_0.WrData);
				break;
			}
			case e_PLC_DATA_TYPE.TYPE_FLOAT:
			{
				text = FuckProtect.DataFrom(1370);
				text2 = FuckProtect.DataFrom(1378);
				text3 = FuckProtect.DataFrom(1310);
				text4 = FuckProtect.DataFrom(1318);
				text7 = FuckProtect.DataFrom(1318);
				text8 = FuckProtect.DataFrom(1386);
				short num = Convert.ToInt16(writeData_0.Tag.Address);
				text6 = this.ShortTOstr(Convert.ToInt16((int)(num * 8)));
				text9 = this.FloatTOstr((float)writeData_0.WrData);
				break;
			}
			case e_PLC_DATA_TYPE.TYPE_BOOL:
			{
				text = FuckProtect.DataFrom(1270);
				text2 = FuckProtect.DataFrom(1278);
				text3 = FuckProtect.DataFrom(1286);
				text4 = FuckProtect.DataFrom(1286);
				text7 = FuckProtect.DataFrom(1294);
				text8 = FuckProtect.DataFrom(1256);
				string[] array = writeData_0.Tag.Address.Split(new char[]
				{
					'.'
				});
				short num = Convert.ToInt16(array[0]);
				short num2 = Convert.ToInt16(array[1]);
				text6 = this.ShortTOstr(Convert.ToInt16((int)(num * 8 + num2)));
				text9 = ((writeData_0.WrData == 0.0) ? FuckProtect.DataFrom(1302) : FuckProtect.DataFrom(1286));
				break;
			}
			case e_PLC_DATA_TYPE.TYPE_SHORT:
			{
				text = FuckProtect.DataFrom(1340);
				text2 = FuckProtect.DataFrom(1348);
				text3 = FuckProtect.DataFrom(1310);
				text4 = FuckProtect.DataFrom(1310);
				text7 = FuckProtect.DataFrom(1318);
				text8 = FuckProtect.DataFrom(1356);
				short num = Convert.ToInt16(writeData_0.Tag.Address);
				text6 = this.ShortTOstr(Convert.ToInt16((int)(num * 8)));
				text9 = this.ShortTOstr((short)writeData_0.WrData);
				break;
			}
			case e_PLC_DATA_TYPE.TYPE_BYTE:
			{
				text = FuckProtect.DataFrom(1270);
				text2 = FuckProtect.DataFrom(1278);
				text3 = FuckProtect.DataFrom(1310);
				text4 = FuckProtect.DataFrom(1286);
				text7 = FuckProtect.DataFrom(1318);
				text8 = FuckProtect.DataFrom(1326);
				short num = Convert.ToInt16(writeData_0.Tag.Address);
				text6 = this.ShortTOstr(Convert.ToInt16((int)(num * 8)));
				text9 = this.ByteTOstr((byte)writeData_0.WrData);
				break;
			}
			}
			string text10;
			tagGroup.get_MMtype(out text10);
			string text11 = string.Format(FuckProtect.DataFrom(1400), new object[]
			{
				text,
				text2,
				text3,
				text4,
				text5,
				text10,
				text6,
				text7,
				text8,
				text9
			});
			this.socket.Send(StrTobyte.StrToArraybyte(text11));
			return result;
		}
        ///<summary >写入数据成功</summary>
        private bool WriteOK()
		{
			bool result = false;
			if (!this.socket.Connected)
			{
				return false;
			}
			byte[] array = new byte[1024];
			int num = this.socket.Receive(array);
			if (num == 22 && array[20] == 1 && array[21] == 255)
			{
				result = true;
			}
			return result;
		}

        ///<summary >byte转换字符串</summary>
        private string ByteTOstr(byte bytes)
		{
			if (bytes == 0)
			{
				return FuckProtect.DataFrom(1302);//00
            }
			return string.Format(FuckProtect.DataFrom(1620), bytes);//{0:X}
        }
        ///<summary >short转换字符串</summary>
        private string ShortTOstr(short shorts)
		{
			if (shorts == 0)
			{
				return FuckProtect.DataFrom(1242);//00 00
            }
			byte[] bytes = BitConverter.GetBytes(shorts);
			return string.Format(FuckProtect.DataFrom(1634), bytes[1], bytes[0]); //{0:X} {1:X}
        }
        ///<summary >int转换字符串</summary>
        private string IntTOstr(int int_0)
		{
			if (int_0 == 0)
			{
				return FuckProtect.DataFrom(1660);//00 00 00 00
            }
			byte[] bytes = BitConverter.GetBytes(int_0);
			return string.Format(FuckProtect.DataFrom(1686), new object[]
			{
				bytes[3],
				bytes[2],
				bytes[1],
				bytes[0]
			});
		}
        ///<summary >浮点转换字符串</summary>
        private string FloatTOstr(float floats)
		{
			if (floats == 0f)
			{
				return FuckProtect.DataFrom(1660);//00 00 00 00
            }
			byte[] bytes = BitConverter.GetBytes(floats);
			return string.Format(FuckProtect.DataFrom(1686), new object[]   /*{0:X} {1:X} {2:X} {3:X}*/
			{
				bytes[3],
				bytes[2],
				bytes[1],
				bytes[0]
			});
		}
	}
}
