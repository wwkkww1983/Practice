﻿using System.Configuration;

namespace Communication.Modbus.Core
{
    /// <summary>
    ///     Modbus/Rtu协议
    /// </summary>
    public class ModbusRtuProtocal : ModbusProtocal
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="slaveAddress">从站号</param>
        /// <param name="masterAddress">主站号</param>
        public ModbusRtuProtocal(byte slaveAddress, byte masterAddress)
            : this(ConfigurationManager.AppSettings["COM"], slaveAddress, masterAddress)
        {
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="com">串口</param>
        /// <param name="slaveAddress">从站号</param>
        /// <param name="masterAddress">主站号</param>
        public ModbusRtuProtocal(string com, byte slaveAddress, byte masterAddress)
            : base(slaveAddress, masterAddress)
        {
            ProtocalLinker = new ModbusRtuProtocalLinker(com, slaveAddress);
        }
    }
}