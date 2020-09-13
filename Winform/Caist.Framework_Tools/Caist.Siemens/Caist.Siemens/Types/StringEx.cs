using System;
using System.Text;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含从S7字符串转换为c#字符串的方法
    /// 字符串的发送方式有两种。这个的预值是两个字节
    /// 它们包含字符串的长度
    /// </summary>
    public static class StringEx
    {
        /// <summary>
        /// 将S7字节转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FromByteArray(byte[] bytes)
        {
            if (bytes.Length < 2) return "";

            int size = bytes[0];
            int length = bytes[1];

            try
            {
                return Encoding.ASCII.GetString(bytes, 2, length);
            }
            catch (Exception e)
            {
                throw new PlcException(ErrorCode.ReadData,
                    $"未能解析{VarType.StringEx}。读取以下字段:size: '{size}'，实际长度:'{length}'，总字节数(包括头):'{bytes.Length}'.",
                    e);
            }
            
        }

        /// <summary>
        /// 参考 <see cref="T:string"/> 以2字节标题的S7字符串。
        /// </summary>
        /// <param name="value">要转换为字节数组的字符串.</param>
        /// <param name="reservedLength">在PLC中为不包括头的字符串分配的长度(以字节为单位)</param>
        /// <returns>A <see cref="T:byte[]" /> 包含最大长度为的字符串标题和字符串值 <paramref name="reservedLength"/> + 2.</returns>
        public static byte[] ToByteArray(string value, int reservedLength)
        {
            if (reservedLength > byte.MaxValue) throw new ArgumentException($"支持的最大字符串长度为 {byte.MaxValue}.");

            var length = value?.Length;
            if (length > reservedLength) length = reservedLength;

            var bytes = new byte[(length ?? 0) + 2];
            bytes[0] = (byte) reservedLength;

            if (value == null) return bytes;

            bytes[1] = (byte) Encoding.ASCII.GetBytes(value, 0, length.Value, bytes, 2);
            return bytes;
        }
    }
}
