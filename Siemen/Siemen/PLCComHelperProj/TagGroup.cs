using System;
using System.Collections.Generic;

namespace PLCComHelperProj
{
    public class TagGroup
    {

        private string m_Name;
        private string string_0;
        private int int_0;
        private int int_1;
        private int readCount;
        private Dictionary<int, byte> dictionary_0;
        private Dictionary<string, Tag> dictionary_1;
        private byte[] byte_0;
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
        public string MMType
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value.ToUpper();
            }
        }
        public int Block
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }
        public int StartAddress
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }
        public int ReadCount
        {
            get
            {
                return this.readCount;
            }
            set
            {
                this.readCount = value;
            }
        }
        public Dictionary<string, Tag> Tags
        {
            get
            {
                return this.dictionary_1;
            }
            set
            {
                this.dictionary_1 = value;
            }
        }



        public TagGroup(string Name, string MMType, int Block, int Address, int ReadCount) : base()
        {
            this.dictionary_0 = new Dictionary<int, byte>(137);
            this.dictionary_1 = new Dictionary<string, Tag>(137);

            this.m_Name = Name;
            this.string_0 = MMType;
            this.Block = Block;
            this.int_1 = Address;
            this.readCount = ReadCount;
            this.byte_0 = new byte[ReadCount];
        }
        ///<summary >将数据复制到buff中</summary>
        public void SetBuff(byte[] buff, int soureIndex)
        {
            Array.Copy(this.byte_0, 0, buff, soureIndex, this.ReadCount);
        }
        public e_PLC_MMType Get_MMtype()
        {
            string text = "";
            return this.get_MMtype(out text);
        }
        public e_PLC_MMType get_MMtype(out string funCode)
        {
            e_PLC_MMType result = e_PLC_MMType.MM_UNKNOW;
            funCode = FuckProtect.DataFrom(1302);
            string key;
            key = this.string_0;

            if (key == FuckProtect.DataFrom(1772))//0
            {
                result = e_PLC_MMType.MM_I_E;
                funCode = FuckProtect.DataFrom(1886);
                return result;
            }
;
            if (key == FuckProtect.DataFrom(1778))//1
            {
                result = e_PLC_MMType.MM_I_E;
                funCode = FuckProtect.DataFrom(1886);
                return result;
            }

            if (key == FuckProtect.DataFrom(1784))//2
            {
                result = e_PLC_MMType.MM_A_Q;
                funCode = FuckProtect.DataFrom(1894);
                return result;
            }

            if (key == FuckProtect.DataFrom(1790))//3
            {
                result = e_PLC_MMType.MM_A_Q;
                funCode = FuckProtect.DataFrom(1894);
                return result;
            }

            if (key == FuckProtect.DataFrom(1802))//5
            {
                result = e_PLC_MMType.MM_M_F;
                funCode = FuckProtect.DataFrom(1902);
                return result;
            }
            //case Class4.smethod_11(1796)://4

            if (key == FuckProtect.DataFrom(1816))//7
            {
                result = e_PLC_MMType.MM_PI_PE;
                funCode = FuckProtect.DataFrom(1910);
                return result;
            }
            //case Class4.smethod_11(1808)://6

            if (key == FuckProtect.DataFrom(1832))//9
            {
                result = e_PLC_MMType.MM_PQ_PA;
                funCode = FuckProtect.DataFrom(1918);
                return result;
            }
            //case Class4.smethod_11(1824):;//8
            if (key == FuckProtect.DataFrom(1840))//10
            {
                result = e_PLC_MMType.MM_DB;
                funCode = FuckProtect.DataFrom(1926);
                return result;
            }

            if (key == FuckProtect.DataFrom(1848))//11
            {
                result = e_PLC_MMType.MM_V;
                funCode = FuckProtect.DataFrom(1926);
                return result;
            }

            if (key == FuckProtect.DataFrom(1862))//13
            {
                result = e_PLC_MMType.MM_AI_AE;
                funCode = FuckProtect.DataFrom(1348);
                return result;
            }
            //case Class4.smethod_11(1854)://12
;
            if (key == FuckProtect.DataFrom(1878))//15
            {
                result = e_PLC_MMType.MM_AQ_AA;
                funCode = FuckProtect.DataFrom(1934);
                return result;
            }
            //case Class4.smethod_11(1870)://14
            return result;
		}

        ///<summary >写入接受到的数据</summary>
		public void SetData(int index, byte bytes)
		{
			this.dictionary_0[this.StartAddress + index] = bytes;
		}
		public int GetBitData(int address, int bitIndex)
		{
			if (this.dictionary_0.ContainsKey(address))
			{
				byte byte_ = this.dictionary_0[address];
				return this.method_2(byte_, bitIndex);
			}
            return 0;// -9999;
		}
		public int GetByteData(int address)
		{
			if (this.dictionary_0.ContainsKey(address))
			{
				return (int)this.dictionary_0[address];
			}
            return 0;// -9999;
		}
		private int method_2(byte byte_1, int int_3)
		{
			int num = (int)byte_1 & Convert.ToInt32(Math.Pow(2.0, (double)int_3));
			if (num <= 0)
			{
				return 0;
			}
			return 1;
		}
		public float GetFloatData(int address)
		{
			if (this.dictionary_0.ContainsKey(address))
			{
				byte[] value = new byte[]
				{
					this.dictionary_0[address + 3],
					this.dictionary_0[address + 2],
					this.dictionary_0[address + 1],
					this.dictionary_0[address]
				};
				return BitConverter.ToSingle(value, 0);
			}
            return 0;// -9999f;
		}
		public int GetIntData(int address)
		{
			if (this.dictionary_0.ContainsKey(address))
			{
				byte[] value = new byte[]
				{
					this.dictionary_0[address + 3],
					this.dictionary_0[address + 2],
					this.dictionary_0[address + 1],
					this.dictionary_0[address]
				};
				return BitConverter.ToInt32(value, 0);
			}
            return 0;// -9999;
		}
		public short GetShortValue(int address)
		{
			if (this.dictionary_0.ContainsKey(address))
			{
				byte[] value = new byte[]
				{
					this.dictionary_0[address + 1],
					this.dictionary_0[address]
				};
				return BitConverter.ToInt16(value, 0);
			}
            return 0;// -9999;
		}
	}
}
