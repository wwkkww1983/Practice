using System;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含将字节转换为字节数组的方法
    /// </summary>
    public static class Byte
    {
        /// <summary>
        /// 转换字节到字节数组
        /// </summary>
        public static byte[] ToByteArray(byte value)
        {
            return new byte[] { value }; ;
        }

        /// <summary>
        /// 将字节数组转换为字节
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte FromByteArray(byte[] bytes)
        {
            if (bytes.Length != 1)
            {
                throw new ArgumentException("字节数错误。字节数组必须包含1个字节.");
            }
            return bytes[0];
        }
        
    }
}
