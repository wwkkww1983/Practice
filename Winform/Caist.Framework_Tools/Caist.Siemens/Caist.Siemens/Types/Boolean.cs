namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含读取、设置和重置字节内位的方法
    /// </summary>
    public static class Boolean
    {
        /// <summary>
        /// 给定位的地址，以位的形式返回位的值
        /// </summary>
        public static bool GetValue(byte value, int bit)
        {
            return (((int)value & (1 << bit)) != 0);
        }

        /// <summary>
        /// 给定位的地址，将位的值设置为1 (true)
        /// </summary>
        public static byte SetBit(byte value, int bit)
        {
            return (byte)((value | (1 << bit)) & 0xFF);
        }

        /// <summary>
        /// 给定位的地址，将位的值重置为0 (false)
        /// </summary>
        public static byte ClearBit(byte value, int bit)
        {
            return (byte)((value | (~(1 << bit))) & 0xFF);
        }

    }
}
