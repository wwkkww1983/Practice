using Caist.Framework.Entity.Entity;
using Caist.Framework.PLC.Siemens.CheckType;
using Caist.Framework.PLC.Siemens.Model;
using System;
using System.Net.Sockets;
using System.Threading;
using static Caist.Framework.PLC.Siemens.Enum.ModularType;

namespace Caist.Framework.PLC.Siemens.Common
{
    internal class PLCLink
    {
		#region 参数
		private string IP;
		private bool _StopFlag;
		public int Port;
		private Socket _Socket;
		private int _Statue;
		private Device _Device;
		private int _Number;
		private Thread _Thread;


		public string MethodIp()
		{
			return this.IP;
		}
		public void SettingIP(string ip)
		{
			this.IP = ip;
		}

		public void SettingPort(int port)
		{
			this.Port = port;
		}

		/// <summary>
		/// 传递私有参数device
		/// </summary>
		/// <returns></returns>
		public Device GetDevice()
		{
			return this._Device;
		}

		public void MethodDevice(Device device)
		{
			this._Device = device;
		}

		public int MethodInt()
		{
			return this._Number;
		}

		public void MethodCompare(int value)
		{
			if (value < 20)
			{
				this._Number = 20;
				return;
			}
			this._Number = value;
		}

		public int CommStatus()
		{
			return this._Statue;
		}
		#endregion

		public PLCLink() : base()
		{
			this.IP = FuckProtect.DataFrom(1742);//默认IP
			this.Port = 102;
			this._Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			this._Device = new Device();
			this._Number = 100;
		}

		private bool Connect()
		{
			bool result = false;
			bool result2;
			try
			{
				this._Statue = 1;//正在连接状态
				if (this._Socket.Connected)//如果套接字处于打开状态、则关闭
				{
					this._Socket.Close();
				}
				this._Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IAsyncResult asyncResult = this._Socket.BeginConnect(this.IP, this.Port, null, null);
				DateTime now = DateTime.Now;
				while (!asyncResult.IsCompleted)//超时检测
				{
					DateTime now2 = DateTime.Now;
					if ((now2 - now).TotalMilliseconds > 3000.0)
					{
						return false;
					}
					Thread.Sleep(50);//每间隔50ms检测一次
				}
				result = true;
				this._Socket.EndConnect(asyncResult);//终止连接
				this._Socket.ReceiveTimeout = 3000;//3000ms无数据接收则超时
				return result;
			}
			catch (Exception)
			{
				result2 = false;
			}
			return result2;
		}

		private void Close()
		{
			if (this._Socket != null)
			{
				this._Socket.Close();
			}
			this._Statue = 0;
		}

		public bool Statue()
		{
			this._Statue = 0;
			return true;
		}

		public bool StartConnect()//开始连接
		{
			bool result;
			if (result = this.Connect())//PLC连接状态返回值=0 连接失败
			{
				this._Statue = 2; /*TCP连接成功*/
				this._StopFlag = false;
				this._Thread = new Thread(new ParameterizedThreadStart(this.GetStatue));
				this._Thread.IsBackground = true;
				this._Thread.Start(this);
			}
			else
			{
				this._Statue = 0; /*未连接*/
			}
			return result;
		}

		public void Stop()
		{
			this._StopFlag = true;
			if (this._Thread != null && this._Thread.IsAlive)
			{
				this._Thread.Join(1000);
			}
			this.Close();
		}

		private int MethodByte(string value, int address)
		{
			return this._Device.ValuePairs[value].GetByteData(address);
		}

		private int MethodInt(string value, int address)
		{
			return this._Device.ValuePairs[value].GetIntData(address);
		}

		private float MethodFloat(string value, int address)
		{
			return this._Device.ValuePairs[value].GetFloatData(address);
		}

		private short MethodShort(string value, int address)
		{
			return this._Device.ValuePairs[value].GetShortValue(address);
		}

		private int MethodBit(string value, int number, int index)
		{
			return this._Device.ValuePairs[value].GetBitData(number, index);
		}

		public object GetValue(string str)
		{
			str = str.Trim().ToUpper();
			string[] array = str.Split(new char[] { '.' });
			string text = array[0];
			string keys = array[1];
			InstructGroupEntity GroupEntity = this._Device.ValuePairs[text];
			InstructEntity Entity = GroupEntity.InstructPairs[keys];
			object result;
			switch (Entity.CheckDataType())
			{
				case DataTypeEnum.TYPE_INT:
					result = this.MethodInt(text, Convert.ToInt32(Entity.Address));
					break;
				case DataTypeEnum.TYPE_FLOAT:
					result = this.MethodFloat(text, Convert.ToInt32(Entity.Address));
					break;
				case DataTypeEnum.TYPE_BOOL:
					{
						string[] array2 = Entity.Address.Split(new char[]{ '.' });
						int Adderss = Convert.ToInt32(array2[0]);
						int Index = Convert.ToInt32(array2[1]);
						result = this.MethodBit(text, Adderss, Index);
						break;
					}
				case DataTypeEnum.TYPE_SHORT:
					result = this.MethodShort(text, Convert.ToInt32(Entity.Address));
					break;
				case DataTypeEnum.TYPE_BYTE:
					result = this.MethodByte(text, Convert.ToInt32(Entity.Address));
					break;
				default:
					result = -9999f;
					break;
			}
			return result;
		}

		private void GetStatue(object object_0)//独立线程处理状态
		{
			bool flag = false;
			while (!this._StopFlag)
			{
				#region  while
				if (!flag)
				{
					if (!this._Device.Connect(this._Socket))
					{
						flag = false;
						this._Statue = 5; /*PLC握手错误*/
						continue;
					}
					flag = true;
					this._Statue = 3; /*PLC握手成功*/
				}
				if (this._Device.Process(this._Socket, this._StopFlag))
				{
					this._Statue = 4; /*正常采集过程中..*/
				}
				else
				{
					this._Statue = 6; /*通讯错误*/
					while (!this._StopFlag)
					{
						if (this.Connect())
						{
							flag = false;
							break;
						}
						Thread.Sleep(1000);
					}
				}
				Thread.Sleep(this._Number);
				#endregion
			}
			this._Statue = 0; /*未连接*/
		}

		public void WriteData(string name, double value)//写数据
		{
			name = name.Trim().ToUpper();
			string[] array = name.Split(new char[] { '.' });//以.分隔数组
			string key = array[0];
			string key2 = array[1];
			InstructGroupEntity GroupEntity = this._Device.ValuePairs[key];
			InstructEntity Entity = GroupEntity.InstructPairs[key2];
			ModularTypeEnum ModularType = GroupEntity.GetModularType();
			DataTypeEnum DataType = Entity.CheckDataType();
			if (this.BackTrue(ModularType, DataType))
			{
				new WriteDataEntity(GroupEntity, Entity, value);
				this._Device.WriteData(GroupEntity, Entity, value);
			}
		}

		private bool BackTrue(ModularTypeEnum ModularType, DataTypeEnum DataType)
		{
			return true;
		}
	}
}
