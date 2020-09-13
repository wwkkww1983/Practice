using System;
using System.Collections;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含转换方法转换位从S7 plc到c#。
    /// </summary>
    public static class Bit
    {
        /// <summary>
        /// 将一个位转换为bool
        /// </summary>
        public static bool FromByte(byte v, byte bitAdr)
        {
            return (((int)v & (1 << bitAdr)) != 0);
        }

        /// <summary>
        /// 将字节数组转换为位数组。
        /// </summary>
        /// <param name="bytes">要转换的字节.</param>
        /// <returns>具有相同位数和相等值的位数组<paramref name="bytes"/>.</returns>
        public static BitArray ToBitArray(byte[] bytes) => ToBitArray(bytes, bytes.Length * 8);

        /// <summary>
        /// 将字节数组转换为位数组。
        /// </summary>
        /// <param name="bytes">要转换的字节</param>
        /// <param name="length">返回长度.</param>
        /// <returns>一个BitArray <paramref name="length"/> bits.</returns>
        public static BitArray ToBitArray(byte[] bytes, int length)
        {
            if (length > bytes.Length * 8) throw new ArgumentException($"没有足够的字节数据返回{length}位。", nameof(bytes));

            var bitArr = new BitArray(bytes);
            var bools = new bool[length];
            for (var i = 0; i < length; i++) bools[i] = bitArr[i];

            return new BitArray(bools);
        }
    }
}
