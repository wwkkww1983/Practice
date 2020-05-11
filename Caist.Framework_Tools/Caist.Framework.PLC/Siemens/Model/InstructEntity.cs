using Caist.Framework.PLC.Siemens.CheckType;
using static Caist.Framework.PLC.Siemens.Enum.ModularType;

namespace Caist.Framework.PLC.Siemens.Model
{
	public class InstructEntity
    {
        private InstructGroupEntity _InstructGroupEntity;
		private string _Id;
		private string _Name;
        private string _Address;
        private string _DataType;
        private string _Desc;
		private string _Output;

        public InstructEntity(InstructGroupEntity instructGroupEntity, string id,string name, string address, string dataType, string desc,string output)
        {
            _InstructGroupEntity = instructGroupEntity;
			Id = id;
			Name = name;
            Address = address;
            DataType = dataType;
            Desc = desc;
			Output = output;
        }
		public string Id { get => _Id; set => _Id = value; }
		public string Name { get => _Name; set => _Name = value; }
        public string Address { get => _Address; set => _Address = value; }
        public string DataType { get => _DataType; set => _DataType = value; }
        public string Desc { get => _Desc; set => _Desc = value; }
		public string Output { get => _Output; set => _Output = value; }
		public DataTypeEnum CheckDataType()
		{
			string a;
			if ((a = this.DataType.ToLower()) != null)
			{
				if (a == FuckProtect.DataFrom(8))
				{
					return DataTypeEnum.TYPE_INT;
				}
				if (a == FuckProtect.DataFrom(18))
				{
					return DataTypeEnum.TYPE_FLOAT;
				}
				if (a == FuckProtect.DataFrom(32))
				{
					return DataTypeEnum.TYPE_BOOL;
				}
				if (a == FuckProtect.DataFrom(44))
				{
					return DataTypeEnum.TYPE_BYTE;
				}
				if (a == FuckProtect.DataFrom(56))
				{
					return DataTypeEnum.TYPE_SHORT;
				}
			}
			return DataTypeEnum.TYPE_UNKNOW;
		}

		public string GetAddressName()
		{
			string text = "";
			string a;
			if ((a = this.DataType) != null)
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
			ModularTypeEnum  Modular = this._InstructGroupEntity.GetModularType();
			string result;
			if (Modular == ModularTypeEnum.MM_DB)
			{
				result = string.Format(FuckProtect.DataFrom(94), new object[]
				{
					this._InstructGroupEntity.ModularType,
					this._InstructGroupEntity.BlockInt,
					text,
					this.Address
				});
			}
			else if (Modular == ModularTypeEnum.MM_UNKNOW)
			{
				result = FuckProtect.DataFrom(130);
			}
			else
			{
				result = string.Format(FuckProtect.DataFrom(142), this._InstructGroupEntity.ModularType, text, this.Address);
			}
			return result;
		}
	}
}
