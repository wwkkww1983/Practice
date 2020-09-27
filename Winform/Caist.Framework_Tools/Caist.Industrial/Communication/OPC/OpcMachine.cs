﻿using Communication.Common;
using System;
using System.Collections.Generic;

namespace Communication.OPC
{
    /// <summary>
    ///     Opc Da设备
    /// </summary>
    public abstract class OpcMachine<TKey, TUnitKey> : BaseMachine<TKey, TUnitKey> where TKey : IEquatable<TKey>
        where TUnitKey : IEquatable<TUnitKey>
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="getAddresses">需要读写的地址</param>
        /// <param name="keepConnect">是否保持连接</param>
        protected OpcMachine(IEnumerable<AddressUnit<TUnitKey>> getAddresses, bool keepConnect)
            : base(getAddresses, keepConnect)
        {
            AddressCombiner = new AddressCombinerSingle<TUnitKey>();
            AddressCombinerSet = new AddressCombinerSingle<TUnitKey>();
        }
    }

    /// <summary>
    ///     Opc Da设备
    /// </summary>
    public abstract class OpcMachine : BaseMachine
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="getAddresses">需要读写的地址</param>
        /// <param name="keepConnect">是否保持连接</param>
        protected OpcMachine(IEnumerable<AddressUnit> getAddresses, bool keepConnect)
            : base(getAddresses, keepConnect)
        {
            AddressCombiner = new AddressCombinerSingle();
            AddressCombinerSet = new AddressCombinerSingle();
        }
    }
}