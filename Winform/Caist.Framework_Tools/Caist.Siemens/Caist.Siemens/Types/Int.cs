using System;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含转换方法转换Int从S7 plc到c#。
    /// </summary>
    public static class Int
    {
        /// <summary>
        /// 将S7 Int(2字节)转换为short (Int16)
        /// </summary>
        public static short FromByteArray(byte[] bytes)
        {
            if (bytes.Length != 2)
            {
                throw new ArgumentException("字节数错误。字节数组必须包含2个字节.");
            }
            // bytes[0] -> HighByte
            // bytes[1] -> LowByte
            return (short)((int)(bytes[1]) | ((int)(bytes[0]) << 8));
        }


        /// <summary>
        /// 将一个短的(Int16)转换为一个S7 Int字节数组(2字节)
        /// </summary>
        public static byte[] ToByteArray(Int16 value)
        {
            byte[] bytes = new byte[2];

            bytes[0] = (byte) (value >> 8 & 0xFF);
            bytes[1] = (byte)(value & 0xFF);

            return bytes;
        }

        /// <summary>
        /// 将一个short数组(Int16)转换为一个S7 Int字节数组(2字节)
        /// </summary>
        public static byte[] ToByteArray(Int16[] value)
        {
            byte[] bytes = new byte[value.Length * 2];
            int bytesPos = 0;

            for(int i=0; i< value.Length; i++)
            {
                bytes[bytesPos++] = (byte)((value[i] >> 8) & 0xFF);
                bytes[bytesPos++] = (byte) (value[i] & 0xFF);
            }
            return bytes;
        }

        /// <summary>
        /// 将一个S7 Int数组转换为一个short数组(Int16)
        /// </summary>
        public static Int16[] ToArray(byte[] bytes)
        {
            int shortsCount = bytes.Length / 2;

            Int16[] values = new Int16[shortsCount];

            int counter = 0;
            for (int cnt = 0; cnt < shortsCount; cnt++)
                values[cnt] = FromByteArray(new byte[] { bytes[counter++], bytes[counter++] });

            return values;
        }

        /// <summary>
        /// 将c# int值转换为c# short值，用作word。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int16 CWord(int value)
        {
            if (value > 32767)
            {
                value -= 32768;
                value = 32768 - value;
                value *= -1;
            }
            return (short)value;
        }

    }
}
