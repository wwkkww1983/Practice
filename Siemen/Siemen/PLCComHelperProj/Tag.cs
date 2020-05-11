using System;

namespace PLCComHelperProj
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
		public e_PLC_DATA_TYPE CheckDataType()
		{
			string a;
			if ((a = this.dataType) != null)
			{
				if (a == FuckProtect.DataFrom(8))
				{
					return e_PLC_DATA_TYPE.TYPE_INT;
				}
				if (a == FuckProtect.DataFrom(18))
				{
					return e_PLC_DATA_TYPE.TYPE_FLOAT;
				}
				if (a == FuckProtect.DataFrom(32))
				{
					return e_PLC_DATA_TYPE.TYPE_BOOL;
				}
				if (a == FuckProtect.DataFrom(44))
				{
					return e_PLC_DATA_TYPE.TYPE_BYTE;
				}
				if (a == FuckProtect.DataFrom(56))
				{
					return e_PLC_DATA_TYPE.TYPE_SHORT;
				}
			}
			return e_PLC_DATA_TYPE.TYPE_UNKNOW;
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
			e_PLC_MMType e_PLC_MMType = this.tagGroup_0.Get_MMtype();
			string result;
			if (e_PLC_MMType == e_PLC_MMType.MM_DB)
			{
				result = string.Format(FuckProtect.DataFrom(94), new object[]
				{
					this.tagGroup_0.MMType,
					this.tagGroup_0.Block,
					text,
					this.address
				});
			}
			else if (e_PLC_MMType == e_PLC_MMType.MM_UNKNOW)
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
