using System;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含转换方法转换DWord从S7 plc到c#。
    /// </summary>
    public static class DWord
    {
        /// <summary>
        /// 将S7 DWord(4bytes)转换为uint (UInt32)
        /// </summary>
        public static UInt32 FromByteArray(byte[] bytes)
        {
            return (UInt32)(bytes[0] << 24 | bytes[1] << 16 | bytes[2] << 8 | bytes[3]);
        }


        /// <summary>
        /// 将4个字节转换为DWord (UInt32)
        /// </summary>
        public static UInt32 FromBytes(byte b1, byte b2, byte b3, byte b4)
        {
            return (UInt32)((b4 << 24) | (b3 << 16) | (b2 << 8) | b1);
        }


        /// <summary>
        /// 将uint (UInt32)转换为S7 DWord (4 bytes) 
        /// </summary>
        public static byte[] ToByteArray(UInt32 value)
        {
            byte[] bytes = new byte[4];

            bytes[0] = (byte)((value >> 24) & 0xFF);
            bytes[1] = (byte)((value >> 16) & 0xFF);
            bytes[2] = (byte)((value >> 8) & 0xFF);
            bytes[3] = (byte)((value) & 0xFF);

            return bytes;
        }

        /// <summary>
        /// 将数组uint (UInt32)转换为数组S7 DWord(4字节)
        /// </summary>
        public static byte[] ToByteArray(UInt32[] value)
        {
            ByteArray arr = new ByteArray();
            foreach (UInt32 val in value)
                arr.Add(ToByteArray(val));
            return arr.Array;
        }

        /// <summary>
        /// 将一个S7 DWord数组转换为一个uint数组(UInt32)
        /// </summary>
        public static UInt32[] ToArray(byte[] bytes)
        {
            UInt32[] values = new UInt32[bytes.Length / 4];

            int counter = 0;
            for (int cnt = 0; cnt < bytes.Length / 4; cnt++)
                values[cnt] = FromByteArray(new byte[] { bytes[counter++], bytes[counter++], bytes[counter++], bytes[counter++] });

            return values;
        }
    }
}
