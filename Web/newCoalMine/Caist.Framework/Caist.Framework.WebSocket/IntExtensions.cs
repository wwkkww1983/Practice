using System;

namespace Caist.Framework.WebSocket
{
    public static class IntExtensions
    {
        public static byte[] ToBigEndianBytes<T>(this int source)
        {
            byte[] bytes;

            var type = typeof(T);
            if (type == typeof(ushort))
                bytes = BitConverter.GetBytes((ushort)source);
            else if (type == typeof(ulong))
                bytes = BitConverter.GetBytes((ulong)source);
            else if (type == typeof(int))
                bytes = BitConverter.GetBytes(source);
            else
                throw new InvalidCastException("不支持转换为泛型！");

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }

        public static int ToLittleEndianInt(this byte[] source)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(source);

            if (source.Length == 2)
                return BitConverter.ToUInt16(source, 0);

            if (source.Length == 8)
                return (int)BitConverter.ToUInt64(source, 0);

            throw new ArgumentException("大小不支持！");
        }
    }
}
