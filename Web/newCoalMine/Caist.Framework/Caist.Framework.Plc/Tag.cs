using Caist.Framework.Enum.PLCManage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.Framework.Plc
{
    public class Tag
    {
		private TagGroup tagGroup_0;
		private string m_Name;
		private string address;
		private string dataType;
		private string desc;
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}
		public string DataType
		{
			get
			{
				return this.dataType;
			}
			set
			{
				this.dataType = value;
			}
		}
		public string Desc
		{
			get
			{
				return this.desc;
			}
			set
			{
				this.desc = value;
			}
		}

		public Tag(TagGroup tagGroup, string Name, string Address, string DataType, string Desc) : base()
		{
			this.tagGroup_0 = tagGroup;
			this.m_Name = Name;
			this.address = Address;
			this.dataType = DataType;
			this.desc = Desc;
		}
		public PlcValueTypeEnum CheckDataType()
		{
			string a;
			if ((a = this.dataType) != null)
			{
				if (a == FuckProtect.DataFrom(8))
				{
					return PlcValueTypeEnum.TYPE_INT;
				}
				if (a == FuckProtect.DataFrom(18))
				{
					return PlcValueTypeEnum.TYPE_FLOAT;
				}
				if (a == FuckProtect.DataFrom(32))
				{
					return PlcValueTypeEnum.TYPE_BOOL;
				}
				if (a == FuckProtect.DataFrom(44))
				{
					return PlcValueTypeEnum.TYPE_BYTE;
				}
				if (a == FuckProtect.DataFrom(56))
				{
					return PlcValueTypeEnum.TYPE_SHORT;
				}
			}
			return PlcValueTypeEnum.TYPE_UNKNOW;
		}
		public string GetAddressName()
		{
			string text = "";
			string a;
			if ((a = this.dataType) != null)
			{
				if (!(a == FuckProtect.DataFrom(8)))
				{
					if (!(a == FuckProtect.DataFrom(44)))
					{
						if (!(a == FuckProtect.DataFrom(18)))
						{
							if (!(a == FuckProtect.DataFrom(56)))
							{
								if (a == FuckProtect.DataFrom(32))
								{
									text = FuckProtect.DataFrom(88);
								}
							}
							else
							{
								text = FuckProtect.DataFrom(82);
							}
						}
						else
						{
							text = FuckProtect.DataFrom(70);
						}
					}
					else
					{
						text = FuckProtect.DataFrom(76);
					}
				}
				else
				{
					text = FuckProtect.DataFrom(70);
				}
			}
			PlcTypeEnum plcTypeEnum = this.tagGroup_0.Get_MMtype();
			string result;
			if (plcTypeEnum == PlcTypeEnum.MM_DB)
			{
				result = string.Format(FuckProtect.DataFrom(94), new object[]
				{
					this.tagGroup_0.MMType,
					this.tagGroup_0.Block,
					text,
					this.address
				});
			}
			else if (plcTypeEnum == PlcTypeEnum.MM_UNKNOW)
			{
				result = FuckProtect.DataFrom(130);
			}
			else
			{
				result = string.Format(FuckProtect.DataFrom(142), this.tagGroup_0.MMType, text, this.address);
			}
			return result;
		}
	}
}
