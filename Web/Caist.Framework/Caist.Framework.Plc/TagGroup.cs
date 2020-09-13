using Caist.Framework.Enum.PLCManage;
using System;
using System.Collections.Generic;

namespace Caist.Framework.Plc
{
    public class TagGroup
    {
        private string name;
        private string mMType;
        private int blockInt;
        private int addressInt;
        private int readCount;
        private Dictionary<int, byte> keyValuePairs;
        private Dictionary<string, Tag> keyValues;
        private byte[] byte_0;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public string MMType
        {
            get
            {
                return this.mMType;
            }
            set
            {
                this.mMType = value.ToUpper();
            }
        }
        public int Block
        {
            get
            {
                return this.blockInt;
            }
            set
            {
                this.blockInt = value;
            }
        }
        public int StartAddress
        {
            get
            {
                return this.addressInt;
            }
            set
            {
                this.addressInt = value;
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
                return this.keyValues;
            }
            set
            {
                this.keyValues = value;
            }
        }

        public TagGroup(string Name, string MMType, int Block, int Address, int ReadCount) : base()
        {
            this.keyValuePairs = new Dictionary<int, byte>(137);
            this.keyValues = new Dictionary<string, Tag>(137);

            this.name = Name;
            this.mMType = MMType;
            this.Block = Block;
            this.addressInt = Address;
            this.readCount = ReadCount;
            this.byte_0 = new byte[ReadCount];
        }
        ///<summary >将数据复制到buff中</summary>
        public void SetBuff(byte[] buff, int soureIndex)
        {
            Array.Copy(this.byte_0, 0, buff, soureIndex, this.ReadCount);
        }
        public PlcTypeEnum Get_MMtype()
        {
            string text = "";
            return this.get_MMtype(out text);
        }
        public PlcTypeEnum get_MMtype(out string funCode)
        {
            PlcTypeEnum result = PlcTypeEnum.MM_UNKNOW;
            funCode = FuckProtect.DataFrom(1302);
            string key;
            key = this.name;

            if (key == FuckProtect.DataFrom(1772))//0
            {
                result = PlcTypeEnum.MM_I_E;
                funCode = FuckProtect.DataFrom(1886);
                return result;
            }
;
            if (key == FuckProtect.DataFrom(1778))//1
            {
                result = PlcTypeEnum.MM_I_E;
                funCode = FuckProtect.DataFrom(1886);
                return result;
            }

            if (key == FuckProtect.DataFrom(1784))//2
            {
                result = PlcTypeEnum.MM_A_Q;
                funCode = FuckProtect.DataFrom(1894);
                return result;
            }

            if (key == FuckProtect.DataFrom(1790))//3
            {
                result = PlcTypeEnum.MM_A_Q;
                funCode = FuckProtect.DataFrom(1894);
                return result;
            }

            if (key == FuckProtect.DataFrom(1802))//5
            {
                result = PlcTypeEnum.MM_M_F;
                funCode = FuckProtect.DataFrom(1902);
                return result;
            }

            if (key == FuckProtect.DataFrom(1816))//7
            {
                result = PlcTypeEnum.MM_PI_PE;
                funCode = FuckProtect.DataFrom(1910);
                return result;
            }

            if (key == FuckProtect.DataFrom(1832))//9
            {
                result = PlcTypeEnum.MM_PQ_PA;
                funCode = FuckProtect.DataFrom(1918);
                return result;
            }

            if (key == FuckProtect.DataFrom(1840))//10
            {
                result = PlcTypeEnum.MM_DB;
                funCode = FuckProtect.DataFrom(1926);
                return result;
            }

            if (key == FuckProtect.DataFrom(1848))//11
            {
                result = PlcTypeEnum.MM_V;
                funCode = FuckProtect.DataFrom(1926);
                return result;
            }

            if (key == FuckProtect.DataFrom(1862))//13
            {
                result = PlcTypeEnum.MM_AI_AE;
                funCode = FuckProtect.DataFrom(1348);
                return result;
            }
;
            if (key == FuckProtect.DataFrom(1878))//15
            {
                result = PlcTypeEnum.MM_AQ_AA;
                funCode = FuckProtect.DataFrom(1934);
                return result;
            }
            return result;
        }

        ///<summary >写入接受到的数据</summary>
		public void SetData(int index, byte bytes)
        {
            this.keyValuePairs[this.StartAddress + index] = bytes;
        }
        public int GetBitData(int address, int bitIndex)
        {
            if (this.keyValuePairs.ContainsKey(address))
            {
                byte byte_ = this.keyValuePairs[address];
                return this.method_2(byte_, bitIndex);
            }
            return -9999;
        }
        public int GetByteData(int address)
        {
            if (this.keyValuePairs.ContainsKey(address))
            {
                return (int)this.keyValuePairs[address];
            }
            return -9999;
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
            if (this.keyValuePairs.ContainsKey(address))
            {
                byte[] value = new byte[]
                {
                    this.keyValuePairs[address + 3],
                    this.keyValuePairs[address + 2],
                    this.keyValuePairs[address + 1],
                    this.keyValuePairs[address]
                };
                return BitConverter.ToSingle(value, 0);
            }
            return -9999f;
        }
        public int GetIntData(int address)
        {
            if (this.keyValuePairs.ContainsKey(address))
            {
                byte[] value = new byte[]
                {
                    this.keyValuePairs[address + 3],
                    this.keyValuePairs[address + 2],
                    this.keyValuePairs[address + 1],
                    this.keyValuePairs[address]
                };
                return BitConverter.ToInt32(value, 0);
            }
            return -9999;
        }
        public short GetShortValue(int address)
        {
            if (this.keyValuePairs.ContainsKey(address))
            {
                byte[] value = new byte[]
                {
                    this.keyValuePairs[address + 1],
                    this.keyValuePairs[address]
                };
                return BitConverter.ToInt16(value, 0);
            }
            return -9999;
        }
    }
}
