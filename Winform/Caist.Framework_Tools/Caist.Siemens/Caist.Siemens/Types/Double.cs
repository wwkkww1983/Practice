using System;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含转换方法，以转换真实从S7 plc到c#双.
    /// </summary>
    public static class Double
    {
        /// <summary>
        /// 将S7实值(4字节)转换为双精度
        /// </summary>
        public static double FromByteArray(byte[] bytes)
        {
            if (bytes.Length != 4)
            {
                throw new ArgumentException("字节数错误。字节数组必须包含4个字节.");
            }
            if (BitConverter.IsLittleEndian)
            {
                bytes = new byte[] { bytes[3], bytes[2], bytes[1], bytes[0] };
            }

            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// 将S7力转换为double
        /// </summary>
        public static double FromDWord(Int32 value)
        {
            byte[] b = DInt.ToByteArray(value);
            double d = FromByteArray(b);
            return d;
        }

        /// <summary>
        /// 将S7 DWord转换为双精度
        /// </summary>
        public static double FromDWord(UInt32 value)
        {
            byte[] b = DWord.ToByteArray(value);
            double d = FromByteArray(b);
            return d;
        }


        /// <summary>
        /// 将double转换为S7实值 (4 bytes)
        /// </summary>
        public static byte[] ToByteArray(double value)
        {
            byte[] bytes = BitConverter.GetBytes((float)(value));
            if (!BitConverter.IsLittleEndian) return bytes;
            return new byte[] { bytes[3], bytes[2], bytes[1], bytes[0] };
        }

        /// <summary>
        /// 将双精度数组转换为字节数组
        /// </summary>
        public static byte[] ToByteArray(double[] value)
        {
            ByteArray arr = new ByteArray();
            foreach (double val in value)
                arr.Add(ToByteArray(val));
            return arr.Array;
        }

        /// <summary>
        /// 将一个S7实数组转换为一个双精度数组
        /// </summary>
        public static double[] ToArray(byte[] bytes)
        {
            double[] values = new double[bytes.Length / 4];

            int counter = 0;
            for (int cnt = 0; cnt < bytes.Length / 4; cnt++)
                values[cnt] = FromByteArray(new byte[] { bytes[counter++], bytes[counter++], bytes[counter++], bytes[counter++] });

            return values;
        }
        
    }
}
