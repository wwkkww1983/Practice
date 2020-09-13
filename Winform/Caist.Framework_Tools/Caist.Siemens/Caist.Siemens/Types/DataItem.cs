using System;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 创建一个可以使用ReadMultipleVars读取的内存块的实例
    /// </summary>
    public class DataItem
    {
        /// <summary>
        /// 要读取的存储区
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        /// 要读取的数据类型(默认为字节)
        /// </summary>
        public VarType VarType { get; set; }

        /// <summary>
        /// 要读取的内存区域的地址(例如:对于DB1这个值是1，对于T45这个值是45)
        /// </summary>
        public int DB { get; set; }

        /// <summary>
        /// 要读取的第一个字节的地址
        /// </summary>
        public int StartByteAdr { get; set; }

        /// <summary>
        /// 要从StartByteAdr读取的地址
        /// </summary>
        public byte BitAdr { get; set; }

        /// <summary>
        /// 要读取的变量数目
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 包含执行读取后内存区域的值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 创建DataItem的实例
        /// </summary>
        public DataItem()
        {
            VarType = VarType.Byte;
            Count = 1;
        }

        /// <summary>
        /// 从创建的实例 <see cref="DataItem"/> 提供的地址
        /// </summary>
        /// <param name="address">要为其创建数据项的地址.</param>
        /// <returns>A new <see cref="DataItem"/> 实例，其属性已解析 <paramref name="address"/>.</returns>
        /// <remarks>The <see cref="Count" /> 属性不从地址解析.</remarks>
        public static DataItem FromAddress(string address)
        {
            PLCAddress.Parse(address, out var dataType, out var dbNumber, out var varType, out var startByte,
                out var bitNumber);

            return new DataItem
            {
                DataType = dataType,
                DB = dbNumber,
                VarType = varType,
                StartByteAdr = startByte,
                BitAdr = (byte) (bitNumber == -1 ? 0 : bitNumber)
            };
        }

        /// <summary>
        /// 创建一个实例 <see cref="DataItem"/> 从提供的地址和值。
        /// </summary>
        /// <param name="address">要为其创建数据项的地址.</param>
        /// <param name="value">要应用于数据项的值.</param>
        /// <returns>A new <see cref="DataItem"/> 实例，其属性已解析 <paramref name="address"/> 和所提供的值集.</returns>
        public static DataItem FromAddressAndValue<T>(string address, T value)
        {
            var dataItem = FromAddress(address);
            dataItem.Value = value;

            if (typeof(T).IsArray) dataItem.Count = ((Array) dataItem.Value).Length;

            return dataItem;
        }
    }
}
