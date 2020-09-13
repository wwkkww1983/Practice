using System;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 将定时器数据类型转换为c#数据类型
    /// </summary>
    public static class Timer
    {
        /// <summary>
        /// 将计时器字节转换为双字节
        /// </summary>
        public static double FromByteArray(byte[] bytes)
        {
            double wert = 0;
            wert = ((bytes[0]) & 0x0F) * 100.0;
            wert += ((bytes[1] >> 4) & 0x0F) * 10.0;
            wert += ((bytes[1]) & 0x0F) * 1.0;
            switch ((bytes[0] >> 4) & 0x03)
            {
                case 0:
                    wert *= 0.01;
                    break;
                case 1:
                    wert *= 0.1;
                    break;
                case 2:
                    wert *= 1.0;
                    break;
                case 3:
                    wert *= 10.0;
                    break;
            }
            return wert;
        }

        /// <summary>
        /// 将ushort (UInt16)转换为一个格式化为时间的字节数组
        /// </summary>
        public static byte[] ToByteArray(UInt16 value)
        {
            byte[] bytes = new byte[2];
            bytes[1] = (byte)((int)value & 0xFF);
            bytes[0] = (byte)((int)value >> 8 & 0xFF);
            return bytes;
        }

        /// <summary>
        /// 将ushort数组(Uint16)转换为格式化为时间的字节数组
        /// </summary>
        public static byte[] ToByteArray(UInt16[] value)
        {
            ByteArray arr = new ByteArray();
            foreach (UInt16 val in value)
                arr.Add(ToByteArray(val));
            return arr.Array;
        }

        /// <summary>
        /// 将格式化为时间的字节数组转换为双精度数数组
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static double[] ToArray(byte[] bytes)
        {
            double[] values = new double[bytes.Length / 2];

            int counter = 0;
            for (int cnt = 0; cnt < bytes.Length / 2; cnt++)
                values[cnt] = FromByteArray(new byte[] { bytes[counter++], bytes[counter++] });

            return values;
        }
    }
}
