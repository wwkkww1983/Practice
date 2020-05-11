using Caist.Framework.PLC.Siemens.CheckType;
using System;
using System.Collections.Generic;
using static Caist.Framework.PLC.Siemens.Enum.ModularType;

namespace Caist.Framework.PLC.Siemens.Model
{
    public class InstructGroupEntity
    {
        private string _Id;
        private string _Name;
        private string _ModularType;
        private int _BlockInt;
        private int _AddressInt;
        private int _ReadCount;
        private Dictionary<int, byte> _KeyValuePairs;
        private Dictionary<string, InstructEntity> _InstructPairs;
        private byte[] _Bytes;

        public string Id { get => _Id; set => _Id = value; }
        public string Name { get => _Name; set => _Name = value; }
        public string ModularType { get => _ModularType; set => _ModularType = value.ToUpper(); }
        public int BlockInt { get => _BlockInt; set => _BlockInt = value; }
        public int AddressInt { get => _AddressInt; set => _AddressInt = value; }
        public int ReadCount { get => _ReadCount; set => _ReadCount = value; }
        public Dictionary<string, InstructEntity> InstructPairs { get => _InstructPairs; set => _InstructPairs = value; }

        public InstructGroupEntity(string id,string name, string modularType, int blockInt, int addressInt, int readCount):base()
        {
            Id = id;
            Name = name;
            ModularType = modularType;
            BlockInt = blockInt;
            AddressInt = addressInt;
            ReadCount = readCount;
            _KeyValuePairs = new Dictionary<int, byte>(137);
            InstructPairs = new Dictionary<string, InstructEntity>(137);
            _Bytes = new byte[readCount];
        }

        /// <summary>
        /// 将数据复制到buff中
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="soureIndex"></param>
        public void SetBuff(byte[] buff, int soureIndex)
        {
            Array.Copy(this._Bytes, 0, buff, soureIndex, this.ReadCount);
        }

        public ModularTypeEnum GetModularType()
        {
            string text = "";
            return this.GetModularType(out text);
        }

        public ModularTypeEnum GetModularType(out string funCode)
        {
            ModularTypeEnum ModularType = ModularTypeEnum.MM_UNKNOW;
            funCode = FuckProtect.DataFrom(1302);
            string key;
            key = this.ModularType;
            if (key == FuckProtect.DataFrom(1772))//0
            {
                ModularType = ModularTypeEnum.MM_I_E;
                funCode = FuckProtect.DataFrom(1886);
                return ModularType;
            }
            if (key == FuckProtect.DataFrom(1778))//1
            {
                ModularType = ModularTypeEnum.MM_I_E;
                funCode = FuckProtect.DataFrom(1886);
                return ModularType;
            }
            if (key == FuckProtect.DataFrom(1784))//2
            {
                ModularType = ModularTypeEnum.MM_A_Q;
                funCode = FuckProtect.DataFrom(1894);
                return ModularType;
            }
            if (key == FuckProtect.DataFrom(1790))//3
            {
                ModularType = ModularTypeEnum.MM_A_Q;
                funCode = FuckProtect.DataFrom(1894);
                return ModularType;
            }
            if (key == FuckProtect.DataFrom(1802))//5
            {
                ModularType = ModularTypeEnum.MM_M_F;
                funCode = FuckProtect.DataFrom(1902);
                return ModularType;
            }
            if (key == FuckProtect.DataFrom(1816))//7
            {
                ModularType = ModularTypeEnum.MM_PI_PE;
                funCode = FuckProtect.DataFrom(1910);
                return ModularType;
            }
            if (key == FuckProtect.DataFrom(1832))//9
            {
                ModularType = ModularTypeEnum.MM_PQ_PA;
                funCode = FuckProtect.DataFrom(1918);
                return ModularType;
            }
            if (key == FuckProtect.DataFrom(1840))//10
            {
                ModularType = ModularTypeEnum.MM_DB;
                funCode = FuckProtect.DataFrom(1926);
                return ModularType;
            }
            if (key == FuckProtect.DataFrom(1848))//11
            {
                ModularType = ModularTypeEnum.MM_V;
                funCode = FuckProtect.DataFrom(1926);
                return ModularType;
            }
            if (key == FuckProtect.DataFrom(1862))//13
            {
                ModularType = ModularTypeEnum.MM_AI_AE;
                funCode = FuckProtect.DataFrom(1348);
                return ModularType;
            }
            if (key == FuckProtect.DataFrom(1878))//15
            {
                ModularType = ModularTypeEnum.MM_AQ_AA;
                funCode = FuckProtect.DataFrom(1934);
                return ModularType;
            }
            return ModularType;
        }

        /// <summary>
        /// 写入接受到的数据
        /// </summary>
        /// <param name="index"></param>
        /// <param name="bytes"></param>
        public void SetData(int index, byte bytes)
        {
            this._KeyValuePairs[this.AddressInt + index] = bytes;
        }

        public int GetBitData(int address, int bitIndex)
        {
            if (this._KeyValuePairs.ContainsKey(address))
            {
                byte byte_ = this._KeyValuePairs[address];
                return this.Method(byte_, bitIndex);
            }
            return -9999;
        }

        public int GetByteData(int address)
        {
            if (this._KeyValuePairs.ContainsKey(address))
            {
                return (int)this._KeyValuePairs[address];
            }
            return -9999;
        }

        private int Method(byte bytes, int ints)
        {
            int num = (int)bytes & Convert.ToInt32(Math.Pow(2.0, (double)ints));
            if (num <= 0)
            {
                return 0;
            }
            return 1;
        }

        public float GetFloatData(int address)
        {
            if (this._KeyValuePairs.ContainsKey(address))
            {
                byte[] value = new byte[]
                {
                    this._KeyValuePairs[address + 3],
                    this._KeyValuePairs[address + 2],
                    this._KeyValuePairs[address + 1],
                    this._KeyValuePairs[address]
                };
                return BitConverter.ToSingle(value, 0);
            }
            return -9999f;
        }

        public int GetIntData(int address)
        {
            if (this._KeyValuePairs.ContainsKey(address))
            {
                byte[] value = new byte[]
                {
                    this._KeyValuePairs[address + 3],
                    this._KeyValuePairs[address + 2],
                    this._KeyValuePairs[address + 1],
                    this._KeyValuePairs[address]
                };
                return BitConverter.ToInt32(value, 0);
            }
            return -9999;
        }

        public short GetShortValue(int address)
        {
            if (this._KeyValuePairs.ContainsKey(address))
            {
                byte[] value = new byte[]
                {
                    this._KeyValuePairs[address + 1],
                    this._KeyValuePairs[address]
                };
                return BitConverter.ToInt16(value, 0);
            }
            return -9999;
        }
    }
}
