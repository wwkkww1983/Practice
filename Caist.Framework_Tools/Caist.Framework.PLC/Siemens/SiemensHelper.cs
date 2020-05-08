using Caist.Framework.Entity;
using Caist.Framework.PLC.Siemens.CheckType;
using Caist.Framework.PLC.Siemens.Common;
using Caist.Framework.PLC.Siemens.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Caist.Framework.PLC.Siemens
{
	public class SiemensHelper
    {
		private readonly int Fuck_int;
		private PLCLink Link;
		private bool ProtectFlg = true;
		public Dictionary<string, Device> device = new Dictionary<string, Device>();
		/// <summary>
		/// 连接状态
		/// </summary>
		public int CommStatus
		{
			get
			{
				return this.Link.CommStatus();
			}
		}

		/// <summary>
		/// 设置IP地址
		/// </summary>
		public string IP//设置IP地址
		{
			set
			{
				this.Link.SettingIP(value);
			}
		}

		/// <summary>
		/// 设置端口
		/// </summary>
		public string Port
		{
			set
			{
				this.Link.SettingPort(int.Parse(value));
			}
		}

		public SiemensHelper() : base()
		{
			this.Fuck_int = 2000;//点位限制
			this.Link = new PLCLink();
		}


		public bool Init()
		{
			int num = 0;
			this.Link.GetDevice().Clear();
			PublicEntity.DeviceEntities.ForEach(d => {
				device.Add(d.Id, new Device(d.PLCType, d.CPU_SlotNO, d.LocalTASP, d.RemoteTASP));
			});
			PublicEntity.TagGroupsEntities.ForEach(v => {
				this.Link.GetDevice().ValuePairs.Add(v.Id, new InstructGroupEntity(v.Id,v.Name, v.MMType, int.Parse(v.Block), int.Parse(v.BeginAddress), int.Parse(v.ReadCount)));
				PublicEntity.TagEntities.Where(s => s.TagGroup == v.Id).ToList().ForEach(x =>
				{
					InstructGroupEntity tagGroup = this.Link.GetDevice().ValuePairs[v.Id];
					string DataType = Entity.Enum.Extensions.ConvertDataEnum(int.Parse(x.DataType));
					tagGroup.InstructPairs.Add(x.Id, new InstructEntity(tagGroup,x.Id, x.Name, x.Address, DataType, x.Desc,x.Output));
					num++;
				});
			});
			if (num>this.Fuck_int)
			{
				MessageBox.Show(FuckProtect.DataFrom(452));
				ProtectFlg = false;
				return false;
			}
			ProtectFlg = true;
			return this.Link.Statue();
		}

		/// <summary>
		/// 打开连接
		/// </summary>
		/// <returns></returns>
		public bool Start()
		{
			if (ProtectFlg)
			{
				return this.Link.StartConnect();
			}
			return false;
		}

		/// <summary>
		/// 关闭连接
		/// </summary>
		public void Stop()
		{
			this.Link.Stop();
		}

		/// <summary>
		/// 获取值
		/// </summary>
		/// <param name="Name"></param>
		/// <returns></returns>
		public object GetValue(string Name)
		{
			return this.Link.GetValue(Name);
		}
		
		/// <summary>
		/// 显示数据
		/// </summary>
		/// <returns></returns>
		public Device GetDevice()
		{
			return this.Link.GetDevice();
		}

		public void Write(string name,double value)
		{
			if (this.ValuTs(value.ToString()))
			{
				this.Link.WriteData(name, value);
			}
		}

		private bool ValuTs(string str)//判断数据是否存在
		{
			if (str == "")
			{
				return false;
			}
			Regex regex = new Regex(FuckProtect.DataFrom(522));//表示不可变的正则表达式
			return regex.IsMatch(str);//构造函数中指定的正则表达式在指定的输入字符串中是否找到了匹配项
		}
	}
}
