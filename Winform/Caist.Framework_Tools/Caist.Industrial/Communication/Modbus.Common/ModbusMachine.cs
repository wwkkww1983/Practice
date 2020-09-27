﻿using Caist.Industrial.Communication.Enum;
using Communication.Common;
using System;
using System.Collections.Generic;

namespace Communication.Modbus.Common
{
    /// <summary>
    ///     Modbus设备
    /// </summary>
    public class ModbusMachine<TKey, TUnitKey> : BaseMachine<TKey, TUnitKey> where TKey : IEquatable<TKey>
        where TUnitKey : IEquatable<TUnitKey>
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="connectionType">连接类型</param>
        /// <param name="connectionString">连接地址</param>
        /// <param name="getAddresses">读写的地址</param>
        /// <param name="keepConnect">是否保持连接</param>
        /// <param name="slaveAddress">从站号</param>
        /// <param name="masterAddress">主站号</param>
        /// <param name="endian">端格式</param>
        public ModbusMachine(ModbusType connectionType, string connectionString,
            IEnumerable<AddressUnit<TUnitKey>> getAddresses, bool keepConnect, byte slaveAddress, byte masterAddress,
            Endian endian = Endian.BigEndianLsb)
            : base(getAddresses, keepConnect, slaveAddress, masterAddress)
        {
            BaseUtility = new ModbusUtility(connectionType, connectionString, slaveAddress, masterAddress, endian);
            AddressFormater = new AddressFormaterModbus();
            AddressCombiner = new AddressCombinerContinus<TUnitKey>(AddressTranslator, 100);
            AddressCombinerSet = new AddressCombinerContinus<TUnitKey>(AddressTranslator, 100);
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="connectionType">连接类型</param>
        /// <param name="connectionString">连接地址</param>
        /// <param name="getAddresses">读写的地址</param>
        /// <param name="slaveAddress">从站号</param>
        /// <param name="masterAddress">主站号</param>
        /// <param name="endian">端格式</param>
        public ModbusMachine(ModbusType connectionType, string connectionString,
            IEnumerable<AddressUnit<TUnitKey>> getAddresses, byte slaveAddress, byte masterAddress,
            Endian endian = Endian.BigEndianLsb)
            : this(connectionType, connectionString, getAddresses, false, slaveAddress, masterAddress, endian)
        {
        }
    }

    /// <summary>
    ///     Modbus设备
    /// </summary>
    public class ModbusMachine : BaseMachine
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="connectionType">连接类型</param>
        /// <param name="connectionString">连接地址</param>
        /// <param name="getAddresses">读写的地址</param>
        /// <param name="keepConnect">是否保持连接</param>
        /// <param name="slaveAddress">从站号</param>
        /// <param name="masterAddress">主站号</param>
        /// <param name="endian">端格式</param>
        public ModbusMachine(ModbusType connectionType, string connectionString,
            IEnumerable<AddressUnit> getAddresses, bool keepConnect, byte slaveAddress, byte masterAddress,
            Endian endian = Endian.BigEndianLsb)
            : base(getAddresses, keepConnect, slaveAddress, masterAddress)
        {
            BaseUtility = new ModbusUtility(connectionType, connectionString, slaveAddress, masterAddress, endian);
            AddressFormater = new AddressFormaterModbus();
            AddressCombiner = new AddressCombinerContinus(AddressTranslator, 100);
            AddressCombinerSet = new AddressCombinerContinus(AddressTranslator, 100);
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="connectionType">连接类型</param>
        /// <param name="connectionString">连接地址</param>
        /// <param name="getAddresses">读写的地址</param>
        /// <param name="slaveAddress">从站号</param>
        /// <param name="masterAddress">主站号</param>
        /// <param name="endian">端格式</param>
        public ModbusMachine(ModbusType connectionType, string connectionString,
            IEnumerable<AddressUnit> getAddresses, byte slaveAddress, byte masterAddress,
            Endian endian = Endian.BigEndianLsb)
            : this(connectionType, connectionString, getAddresses, false, slaveAddress, masterAddress, endian)
        {
        }
    }
}