using System;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含转换方法，转换力从S7 plc到c# int (Int32)。
    /// </summary>
    public static class DInt
    {
        /// <summary>
        /// 将S7 DInt(4字节)转换为int (Int32)
        /// </summary>
        public static Int32 FromByteArray(byte[] bytes)
        {
            if (bytes.Length != 4)
            {
                throw new ArgumentException("字节数错误。字节数组必须包含4个字节。");
            }
            return bytes[0] << 24 | bytes[1] << 16 | bytes[2] << 8 | bytes[3];
        }


        /// <summary>
        /// 将int (Int32)转换为S7 DInt(4字节)
        /// </summary>
        public static byte[] ToByteArray(Int32 value)
        {
            byte[] bytes = new byte[4];

            bytes[0] = (byte)((value >> 24) & 0xFF);
            bytes[1] = (byte)((value >> 16) & 0xFF);
            bytes[2] = (byte)((value >> 8) & 0xFF);
            bytes[3] = (byte)((value) & 0xFF);

            return bytes;
        }

        /// <summary>
        /// 将int (Int32)数组转换为字节数组
        /// </summary>
        public static byte[] ToByteArray(Int32[] value)
        {
            ByteArray arr = new ByteArray();
            foreach (Int32 val in value)
                arr.Add(ToByteArray(val));
            return arr.Array;
        }

        /// <summary>
        /// 将S7力数组转换为int数组(Int32)
        /// </summary>
        public static Int32[] ToArray(byte[] bytes)
        {
            Int32[] values = new Int32[bytes.Length / 4];

            int counter = 0;
            for (int cnt = 0; cnt < bytes.Length / 4; cnt++)
                values[cnt] = FromByteArray(new byte[] { bytes[counter++], bytes[counter++], bytes[counter++], bytes[counter++] });

            return values;
        }
        

    }
}
