using System;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含转换方法，以转换真实从S7 plc到c#浮动。
    /// </summary>
    public static class Single
    {
        /// <summary>
        /// 将S7实值(4字节)转换为浮点数
        /// </summary>
        public static float FromByteArray(byte[] bytes)
        {
            if (bytes.Length != 4)
            {
                throw new ArgumentException("字节数错误。字节数组必须包含4个字节");
            }
            if (BitConverter.IsLittleEndian)
            {
                bytes = new byte[] { bytes[3], bytes[2], bytes[1], bytes[0] };
            }
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// 将S7力转换为浮点数
        /// </summary>
        public static float FromDWord(Int32 value)
        {
            byte[] b = DInt.ToByteArray(value);
            float d = FromByteArray(b);
            return d;
        }

        /// <summary>
        /// 将S7 DWord转换为浮点数
        /// </summary>
        public static float FromDWord(UInt32 value)
        {
            byte[] b = DWord.ToByteArray(value);
            float d = FromByteArray(b);
            return d;
        }


        /// <summary>
        /// 将double转换为S7实值(4字节)
        /// </summary>
        public static byte[] ToByteArray(float value)
        {
            byte[] bytes = BitConverter.GetBytes((float)(value));
            if (!BitConverter.IsLittleEndian) return bytes;
            return new byte[] { bytes[3], bytes[2], bytes[1], bytes[0] };
        }

        /// <summary>
        /// 将浮点数组转换为字节数组
        /// </summary>
        public static byte[] ToByteArray(float[] value)
        {
            ByteArray arr = new ByteArray();
            foreach (float val in value)
                arr.Add(ToByteArray(val));
            return arr.Array;
        }

        /// <summary>
        /// 将一个S7实数组转换为一个浮点数组
        /// </summary>
        public static float[] ToArray(byte[] bytes)
        {
            float[] values = new float[bytes.Length / 4];

            int counter = 0;
            for (int cnt = 0; cnt < bytes.Length / 4; cnt++)
                values[cnt] = FromByteArray(new byte[] { bytes[counter++], bytes[counter++], bytes[counter++], bytes[counter++] });

            return values;
        }
        
    }
}
