using Caist.Framework.PLC.Siemens.CheckType;
using Caist.Framework.PLC.Siemens.Model;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using static Caist.Framework.PLC.Siemens.Enum.ModularType;

namespace Caist.Framework.PLC.Siemens.Common
{
	public class Device
    {
		#region   PLC连接属性
		///<summary >”1”: S7-200/smart系列，”2”： S7-300/400/1200/1500系列；</summary>
		private string _PLCtype;
		///<summary >CPU所在的槽号，S7-300的PLC一般都为"02"，S7-400的PLC一般都为"03", S7-200/1200/1500的PLC一般都为"01"</summary>
		private string _CPUSlotNO;
		///<summary >S7-200/Smart需要用的参数，S7-200："10 11"，Smart："02 01" 。其他PLC忽略</summary>
		private string _LocalTASP;
		///<summary >S7-200/Smart需要用的参数，S7-200："10 01"，Smart："02 00"。其他PLC忽略</summary>
		private string _RemoteTASP;
		private Dictionary<string, InstructGroupEntity> _ValuePairs;
		private Queue<WriteDataEntity> _Queue;//表示对象的先进先出集合
		private Socket _Socket;

		public string PLCtype { get => _PLCtype; set => _PLCtype = value; }
		public string CPUSlotNO { get => _CPUSlotNO; set => _CPUSlotNO = value; }
		public string LocalTASP { get => _LocalTASP; set => _LocalTASP = value; }
		public string RemoteTASP { get => _RemoteTASP; set => _RemoteTASP = value; }
		public Dictionary<string, InstructGroupEntity> ValuePairs { get => _ValuePairs; set => _ValuePairs = value; }
		
		public Device() : base()
		{
			PLCtype = FuckProtect.DataFrom(1736);//2 系列
			CPUSlotNO = FuckProtect.DataFrom(1310);//02 槽号
			LocalTASP = string.Format("");
			RemoteTASP = string.Format("");
			_Queue = new Queue<WriteDataEntity>(11);
			_ValuePairs = new Dictionary<string, InstructGroupEntity>();
		}

		public Device(string pLCtype, string cPUSlotNO, string localTASP, string remoteTASP):base()
		{
			PLCtype = pLCtype;
			CPUSlotNO = cPUSlotNO;
			LocalTASP = localTASP;
			RemoteTASP = remoteTASP;
		}
		#endregion

		/// <summary>
		/// 清除载入数据
		/// </summary>
		public void Clear()
		{
			this._ValuePairs.Clear();
		}

		public void SetDataBuff(string name, byte[] buff, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this._ValuePairs[name].SetData(i, buff[i]);
			}
		}

		public bool Process(Socket socket, bool isStop)
		{
			bool result;
			try
			{
				this._Socket = socket;
				foreach (KeyValuePair<string, InstructGroupEntity> keyValuePair in this._ValuePairs)
				{
					if (isStop)
					{
						break;
					}
					while (this.IsNeedToWrite())//发送数据集合内的数据
					{
						WriteDataEntity writeData_ = this._Queue.Dequeue();//从集合内取数据
						this.WriteD(writeData_);
						Thread.Sleep(20);
						this.Write();//写入数据
					}
					InstructGroupEntity value = keyValuePair.Value;
					this.Send(value);
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


		/// <summary>
		/// 连接plc
		/// </summary>
		/// <param name="socket"></param>
		/// <returns></returns>
		public bool Connect(Socket socket)//握手信息
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

		/// <summary>
		/// 将需要发送的数据存入集合
		/// </summary>
		/// <param name="GroupEntity"></param>
		/// <param name="Entity"></param>
		/// <param name="value"></param>
		public void WriteData(InstructGroupEntity GroupEntity, InstructEntity Entity, double value)
		{
			WriteDataEntity item = new WriteDataEntity(GroupEntity, Entity, value);
			this._Queue.Enqueue(item);
		}

		/// <summary>
		/// 选择PLC类型、确定握手数据
		/// </summary>
		/// <returns></returns>
		private string SelectPLC()
		{
			if (this.PLCtype == FuckProtect.DataFrom(602))//”1”: S7-200/smart系列，执行
			{
				return string.Format(FuckProtect.DataFrom(608), this.RemoteTASP, this.LocalTASP);//数据合成
			}
			return string.Format(FuckProtect.DataFrom(734),CPUSlotNO);//”2”： S7-300/400/1200/1500系列
		}

		public bool IsNeedToWrite()
		{
			return this._Queue.Count > 0;
		}

		private bool WriteD(WriteDataEntity entity)
		{
			bool result = true;
			if (!this._Socket.Connected)
			{
				return false;
			}
			InstructGroupEntity GroupEntity = entity.InstructGroupEntity;
			string text = FuckProtect.DataFrom(1270);
			string text2 = FuckProtect.DataFrom(1278);
			string text3 = FuckProtect.DataFrom(1286);
			string text4 = FuckProtect.DataFrom(1286);
			string text5 = this.DataType(GroupEntity);
			string text6 = FuckProtect.DataFrom(1242);
			string text7 = FuckProtect.DataFrom(1294);
			string text8 = FuckProtect.DataFrom(1256);
			string text9 = FuckProtect.DataFrom(1302);
			switch (entity.InstructEntity.CheckDataType())
			{
				case DataTypeEnum.TYPE_UNKNOW:
					return false;
				case DataTypeEnum.TYPE_INT:
					{
						text = FuckProtect.DataFrom(1370);
						text2 = FuckProtect.DataFrom(1378);
						text3 = FuckProtect.DataFrom(1310);
						text4 = FuckProtect.DataFrom(1318);
						text7 = FuckProtect.DataFrom(1318);
						text8 = FuckProtect.DataFrom(1386);
						short num = Convert.ToInt16(entity.InstructEntity.Address);
						text6 = this.ShortTOstr(Convert.ToInt16((int)(num * 8)));
						text9 = this.IntTOstr((int)entity.Double);
						break;
					}
				case DataTypeEnum.TYPE_FLOAT:
					{
						text = FuckProtect.DataFrom(1370);
						text2 = FuckProtect.DataFrom(1378);
						text3 = FuckProtect.DataFrom(1310);
						text4 = FuckProtect.DataFrom(1318);
						text7 = FuckProtect.DataFrom(1318);
						text8 = FuckProtect.DataFrom(1386);
						short num = Convert.ToInt16(entity.InstructEntity.Address);
						text6 = this.ShortTOstr(Convert.ToInt16((int)(num * 8)));
						text9 = this.FloatTOstr((float)entity.Double);
						break;
					}
				case DataTypeEnum.TYPE_BOOL:
					{
						text = FuckProtect.DataFrom(1270);
						text2 = FuckProtect.DataFrom(1278);
						text3 = FuckProtect.DataFrom(1286);
						text4 = FuckProtect.DataFrom(1286);
						text7 = FuckProtect.DataFrom(1294);
						text8 = FuckProtect.DataFrom(1256);
						string[] array = entity.InstructEntity.Address.Split(new char[]
						{
					'.'
						});
						short num = Convert.ToInt16(array[0]);
						short num2 = Convert.ToInt16(array[1]);
						text6 = this.ShortTOstr(Convert.ToInt16((int)(num * 8 + num2)));
						text9 = ((entity.Double == 0.0) ? FuckProtect.DataFrom(1302) : FuckProtect.DataFrom(1286));
						break;
					}
				case DataTypeEnum.TYPE_SHORT:
					{
						text = FuckProtect.DataFrom(1340);
						text2 = FuckProtect.DataFrom(1348);
						text3 = FuckProtect.DataFrom(1310);
						text4 = FuckProtect.DataFrom(1310);
						text7 = FuckProtect.DataFrom(1318);
						text8 = FuckProtect.DataFrom(1356);
						short num = Convert.ToInt16(entity.InstructEntity.Address);
						text6 = this.ShortTOstr(Convert.ToInt16((int)(num * 8)));
						text9 = this.ShortTOstr((short)entity.Double);
						break;
					}
				case DataTypeEnum.TYPE_BYTE:
					{
						text = FuckProtect.DataFrom(1270);
						text2 = FuckProtect.DataFrom(1278);
						text3 = FuckProtect.DataFrom(1310);
						text4 = FuckProtect.DataFrom(1286);
						text7 = FuckProtect.DataFrom(1318);
						text8 = FuckProtect.DataFrom(1326);
						short num = Convert.ToInt16(entity.InstructEntity.Address);
						text6 = this.ShortTOstr(Convert.ToInt16((int)(num * 8)));
						text9 = this.ByteTOstr((byte)entity.Double);
						break;
					}
			}
			string text10;
			GroupEntity.GetModularType(out text10);
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
			this._Socket.Send(StrTobyte.StrToArraybyte(text11));
			return result;
		}

		/// <summary>
		/// 判断数据类型
		/// </summary>
		/// <param name="tagGroup"></param>
		/// <returns></returns>
		private string DataType(InstructGroupEntity GroupEntity)
		{
			ModularTypeEnum ModularType = GroupEntity.GetModularType();
			string result = "00 00";
			if (ModularType == ModularTypeEnum.MM_DB)
			{
				result = this.ShortTOstr((short)GroupEntity.BlockInt);
			}
			else if (ModularType == ModularTypeEnum.MM_V)
			{
				result = "00 01";//00 01
			}
			else
			{
				result = "00 00";//00 01
			}
			return result;
		}

		/// <summary>
		/// short转换字符串
		/// </summary>
		/// <param name="shorts"></param>
		/// <returns></returns>
		private string ShortTOstr(short shorts)
		{
			if (shorts == 0)
			{
				return FuckProtect.DataFrom(1242);//00 00
			}
			byte[] bytes = BitConverter.GetBytes(shorts);
			return string.Format(FuckProtect.DataFrom(1634), bytes[1], bytes[0]); //{0:X} {1:X}
		}

		/// <summary>
		/// int转换字符串
		/// </summary>
		/// <param name="int_0"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 浮点转换字符串
		/// </summary>
		/// <param name="floats"></param>
		/// <returns></returns>
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

		/// <summary>
		/// byte转换字符串
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		private string ByteTOstr(byte bytes)
		{
			if (bytes == 0)
			{
				return FuckProtect.DataFrom(1302);//00
			}
			return string.Format(FuckProtect.DataFrom(1620), bytes);//{0:X}
		}

		///<summary >写入数据</summary>
		private bool Write()
		{
			bool result = false;
			if (!this._Socket.Connected)
			{
				return false;
			}
			byte[] array = new byte[1024];
			int num = this._Socket.Receive(array);
			if (num == 22 && array[20] == 1 && array[21] == 255)
			{
				result = true;
			}
			return result;
		}

		/// <summary>
		/// 发送数据
		/// </summary>
		/// <param name="GroupEntity"></param>
		private void Send(InstructGroupEntity GroupEntity)
		{
			if (!this._Socket.Connected)
			{
				return;
			}
			int startAddress = GroupEntity.AddressInt;
			int readCount = GroupEntity.ReadCount;
			string DataString = this.DataType(GroupEntity);
			string ShortString = this.ShortTOstr((short)(startAddress * 8));
			string ReadString = Convert.ToString(readCount, 16);
			string DataType;
			GroupEntity.GetModularType(out DataType);
			string text5 = string.Format(FuckProtect.DataFrom(1058), new object[]
			{
				ShortString,
				ReadString,
				DataType,
				DataString
			});
			this._Socket.Send(StrTobyte.StrToArraybyte(text5));
		}

		/// <summary>
		/// 接收数据
		/// </summary>
		/// <param name="tagGroup"></param>
		private void ReceiveData(InstructGroupEntity GroupEntity)
		{
			if (!this._Socket.Connected)
			{
				return;
			}
			int readCount = GroupEntity.ReadCount;
			byte[] array = new byte[1024];
			int num = this._Socket.Receive(array);
			int num2 = 25 + readCount;
			if (num == num2)
			{
				for (int i = 0; i < readCount; i++)
				{
					GroupEntity.SetData(i, array[25 + i]);
				}
				GroupEntity.SetBuff(array, 25);
			}
		}
	}
}
