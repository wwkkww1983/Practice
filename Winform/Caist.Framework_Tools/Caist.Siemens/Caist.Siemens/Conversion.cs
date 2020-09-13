using System;
using System.Globalization;

namespace Caist.Siemens
{
    /// <summary>
    /// 转换方法转换从西门子数字格式到c#和回来
    /// </summary>
    public static class Conversion
    {
        /// <summary>
        /// 将二进制字符串转换为Int32值
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static int BinStringToInt32(this string txt)
        {
            int ret = 0;

            for (int i = 0; i < txt.Length; i++)
            {
                ret = (ret << 1) | ((txt[i] == '1') ? 1 : 0);
            }
            return ret;
        }

        /// <summary>
        /// 将二进制字符串转换为字节。可以返回null
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static byte? BinStringToByte(this string txt)
        {
            if (txt.Length == 8) return (byte)BinStringToInt32(txt);
            return null;
        }

        /// <summary>
        /// 将值转换为二进制字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ValToBinString(this object value)
        {
            int cnt = 0;
            int cnt2 = 0;
            int x = 0;
            string txt = "";
            long longValue = 0;

            try
            {
                if (value.GetType().Name.IndexOf("[]") < 0)
                {
                    switch (value.GetType().Name)
                    {
                        case "Byte":
                            x = 7;
                            longValue = (long)((byte)value);
                            break;
                        case "Int16":
                            x = 15;
                            longValue = (long)((Int16)value);
                            break;
                        case "Int32":
                            x = 31;
                            longValue = (long)((Int32)value);
                            break;
                        case "Int64":
                            x = 63;
                            longValue = (long)((Int64)value);
                            break;
                        default:
                            throw new Exception();
                    }
                    for (cnt = x; cnt >= 0; cnt += -1)
                    {
                        if (((Int64)longValue & (Int64)Math.Pow(2, cnt)) > 0)
                            txt += "1";
                        else
                            txt += "0";
                    }
                }
                else
                {
                    switch (value.GetType().Name)
                    {
                        case "Byte[]":
                            x = 7;
                            byte[] ByteArr = (byte[])value;
                            for (cnt2 = 0; cnt2 <= ByteArr.Length - 1; cnt2++)
                            {
                                for (cnt = x; cnt >= 0; cnt += -1)
                                    if ((ByteArr[cnt2] & (byte)Math.Pow(2, cnt)) > 0) txt += "1"; else txt += "0";
                            }
                            break;
                        case "Int16[]":
                            x = 15;
                            Int16[] Int16Arr = (Int16[])value;
                            for (cnt2 = 0; cnt2 <= Int16Arr.Length - 1; cnt2++)
                            {
                                for (cnt = x; cnt >= 0; cnt += -1)
                                    if ((Int16Arr[cnt2] & (byte)Math.Pow(2, cnt)) > 0) txt += "1"; else txt += "0";
                            }
                            break;
                        case "Int32[]":
                            x = 31;
                            Int32[] Int32Arr = (Int32[])value;
                            for (cnt2 = 0; cnt2 <= Int32Arr.Length - 1; cnt2++)
                            {
                                for (cnt = x; cnt >= 0; cnt += -1)
                                    if ((Int32Arr[cnt2] & (byte)Math.Pow(2, cnt)) > 0) txt += "1"; else txt += "0";
                            }
                            break;
                        case "Int64[]":
                            x = 63;
                            byte[] Int64Arr = (byte[])value;
                            for (cnt2 = 0; cnt2 <= Int64Arr.Length - 1; cnt2++)
                            {
                                for (cnt = x; cnt >= 0; cnt += -1)
                                    if ((Int64Arr[cnt2] & (byte)Math.Pow(2, cnt)) > 0) txt += "1"; else txt += "0";
                            }
                            break;
                        default:
                            throw new Exception();
                    }
                }
                return txt;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 帮助获得给定字节和位索引的位值。
        /// 例如:DB1.DBX0.5→var bytes = ReadBytes(DB1.DBW0);bool bit =字节[0].SelectBit(5);
        /// </summary>
        /// <param name="data"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static bool SelectBit(this byte data, int bitPosition)
        {
            int mask = 1 << bitPosition;
            int result = data & mask;

            return (result != 0);
        }

        /// <summary>
        /// 从短值转换为短值;它用于从单词中检索负值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static short ConvertToShort(this ushort input)
        {
            short output;
            output = short.Parse(input.ToString("X"), NumberStyles.HexNumber);
            return output;
        }

        /// <summary>
        /// 从短值转换为短值;它用于将负值传递给DWs
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static ushort ConvertToUshort(this short input)
        {
            ushort output;
            output = ushort.Parse(input.ToString("X"), NumberStyles.HexNumber);
            return output;
        }

        /// <summary>
        /// 从UInt32值转换为Int32值;它用于从dbd中检索负值DBs
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Int32 ConvertToInt(this uint input)
        {
            int output;
            output = int.Parse(input.ToString("X"), NumberStyles.HexNumber);
            return output;
        }

        /// <summary>
        /// 从Int32值转换为UInt32值;它用于向dbd传递负值DBs
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static UInt32 ConvertToUInt(this int input)
        {
            uint output;
            output = uint.Parse(input.ToString("X"), NumberStyles.HexNumber);
            return output;
        }

        /// <summary>
        /// 从双字符转换为双字符(DBD)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Obsolete("双重支持已经过时了。使用ConvertToUInt(浮动).")]
        public static UInt32 ConvertToUInt(this double input)
        {
            uint output;
            output = Caist.Siemens.Types.DWord.FromByteArray(Caist.Siemens.Types.Double.ToByteArray(input));
            return output;
        }

        /// <summary>
        /// 从浮点数转换为DWord (DBD)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static UInt32 ConvertToUInt(this float input)
        {
            uint output;
            output = Caist.Siemens.Types.DWord.FromByteArray(Caist.Siemens.Types.Single.ToByteArray(input));
            return output;
        }

        /// <summary>
        /// 从DWord (DBD)转换为双精度
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Obsolete("双重支持已经过时了。使用ConvertToFloat(单位).")]
        public static double ConvertToDouble(this uint input)
        {
            double output;
            output = Caist.Siemens.Types.Double.FromByteArray(Caist.Siemens.Types.DWord.ToByteArray(input));
            return output;
        }

        /// <summary>
        /// 从DWord (DBD)转换为浮点数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static float ConvertToFloat(this uint input)
        {
            float output;
            output = Caist.Siemens.Types.Single.FromByteArray(Caist.Siemens.Types.DWord.ToByteArray(input));
            return output;
        }
    }
}
