using System;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含了从S7 plc到c#的转换方法。
    /// </summary>
    public static class Word
    {
        /// <summary>
        /// 将一个单词(2字节)转换为ushort (UInt16)
        /// </summary>
        public static UInt16 FromByteArray(byte[] bytes)
        {
            if (bytes.Length != 2)
            {
                throw new ArgumentException("字节数错误。字节数组必须包含2个字节.");
            }

            return (UInt16)((bytes[0] << 8) | bytes[1]);
        }


        /// <summary>
        /// 将2个字节转换为ushort (UInt16)
        /// </summary>
        public static UInt16 FromBytes(byte b1, byte b2)
        {
            return (UInt16)((b2 << 8) | b1);
        }


        /// <summary>
        /// 将ushort (UInt16)转换为word(2字节)
        /// </summary>
        public static byte[] ToByteArray(UInt16 value)
        {
            byte[] bytes = new byte[2];

            bytes[1] = (byte)(value & 0xFF);
            bytes[0] = (byte)((value>>8) & 0xFF);

            return bytes;
        }

        /// <summary>
        /// 将一个ushort (UInt16)数组转换为一个字节数组
        /// </summary>
        public static byte[] ToByteArray(UInt16[] value)
        {
            ByteArray arr = new ByteArray();
            foreach (UInt16 val in value)
                arr.Add(ToByteArray(val));
            return arr.Array;
        }

        /// <summary>
        /// 将字节数组转换为ushort数组
        /// </summary>
        public static UInt16[] ToArray(byte[] bytes)
        {
            UInt16[] values = new UInt16[bytes.Length/2];

            int counter = 0;
            for (int cnt = 0; cnt < bytes.Length/2; cnt++)
                values[cnt] = FromByteArray(new byte[] {bytes[counter++], bytes[counter++]});

            return values;
        }
    }
}
