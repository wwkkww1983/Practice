namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含从S7字符串转换为c#字符串的方法
    /// </summary>
    public class String
    {
        /// <summary>
        /// 将字符串转换为<paramref name="reservedLength"/> 字节，如果需要，用0字节填充.
        /// </summary>
        /// <param name="value">要写入到PLC的字符串.</param>
        /// <param name="reservedLength"></param>
        public static byte[] ToByteArray(string value, int reservedLength)
        {
            var length = value?.Length;
            if (length > reservedLength) length = reservedLength;
            var bytes = new byte[reservedLength];

            if (length == null || length == 0) return bytes;

            System.Text.Encoding.ASCII.GetBytes(value, 0, length.Value, bytes, 0);

            return bytes;
        }

        /// <summary>
        /// 将S7字节转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FromByteArray(byte[] bytes)
        {
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

    }
}
