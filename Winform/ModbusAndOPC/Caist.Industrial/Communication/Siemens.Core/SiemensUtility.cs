﻿using System;
using System.Threading.Tasks;
using Caist.Industrial.Communication.Enum;
using Communication.Core;
using Serilog;

namespace Modbus.Siemens.Core
{
    /// <summary>
    ///     西门子协议类型
    /// </summary>
    public enum SiemensType
    {
        /// <summary>
        /// PPI
        /// </summary>
        Ppi = 0,
        /// <summary>
        /// MPI
        /// </summary>
        Mpi = 1,
        /// <summary>
        /// 以太网
        /// </summary>
        Tcp = 2
    }

    /// <summary>
    ///     西门子设备类型
    /// </summary>
    public enum SiemensMachineModel
    {
        /// <summary>
        /// S7-200
        /// </summary>
        S7_200 = 0,
        /// <summary>
        /// S7-200 Smart
        /// </summary>
        S7_200_Smart = 1,
        /// <summary>
        /// S7-300
        /// </summary>
        S7_300 = 2,
        /// <summary>
        /// S7-400
        /// </summary>
        S7_400 = 3,
        /// <summary>
        /// S7-1200
        /// </summary>
        S7_1200 = 4,
        /// <summary>
        /// S7-1500
        /// </summary>
        S7_1500 = 5
    }

    /// <summary>
    ///     西门子通讯Api入口
    /// </summary>
    public class SiemensUtility : BaseUtility
    {
        private readonly ushort _maxCalled;
        private readonly ushort _maxCalling;
        private readonly ushort _maxPdu;
        private readonly ushort _taspSrc;
        private readonly byte _tdpuSize;
        private readonly ushort _tsapDst;

        private SiemensType _siemensType;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="connectionType">连接类型</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="model">设备类型</param>
        /// <param name="slaveAddress">从站地址</param>
        /// <param name="masterAddress">主站地址</param>
        public SiemensUtility(SiemensType connectionType, string connectionString, SiemensMachineModel model,
            byte slaveAddress, byte masterAddress) : base(slaveAddress, masterAddress)
        {
            ConnectionString = connectionString;
            switch (model)
            {
                case SiemensMachineModel.S7_200:
                {
                    _tdpuSize = 0x09;
                    _taspSrc = 0x1001;
                    _tsapDst = 0x1000;
                    _maxCalling = 0x0001;
                    _maxCalled = 0x0001;
                    _maxPdu = 0x03c0;
                    break;
                }
                case SiemensMachineModel.S7_300:
                case SiemensMachineModel.S7_400:
                {
                    _tdpuSize = 0x1a;
                    _taspSrc = 0x4b54;
                    _tsapDst = 0x0302;
                    _maxCalling = 0x0001;
                    _maxCalled = 0x0001;
                    _maxPdu = 0x00f0;
                    break;
                }
                case SiemensMachineModel.S7_1200:
                case SiemensMachineModel.S7_1500:
                {
                    _tdpuSize = 0x0a;
                    _taspSrc = 0x1011;
                    _tsapDst = 0x0301;
                    _maxCalling = 0x0003;
                    _maxCalled = 0x0003;
                    _maxPdu = 0x0100;
                    break;
                }
                case SiemensMachineModel.S7_200_Smart:
                {
                    _tdpuSize = 0x0a;
                    _taspSrc = 0x0101;
                    _tsapDst = 0x0101;
                    _maxCalling = 0x0001;
                    _maxCalled = 0x0001;
                    _maxPdu = 0x03c0;
                    break;
                }
                default:
                {
                    throw new NotImplementedException("Siemens PLC Model not Supported");
                }
            }
            ConnectionType = connectionType;
            AddressTranslator = new AddressTranslatorSiemens();
        }

        /// <summary>
        ///     端格式
        /// </summary>
        public override Endian Endian => Endian.BigEndianLsb;

        /// <summary>
        ///     IP地址
        /// </summary>
        protected string ConnectionStringIp
        {
            get
            {
                if (ConnectionString == null) return null;
                return ConnectionString.Contains(":") ? ConnectionString.Split(':')[0] : ConnectionString;
            }
        }

        /// <summary>
        ///     端口
        /// </summary>
        protected int? ConnectionStringPort
        {
            get
            {
                if (ConnectionString == null) return null;
                if (!ConnectionString.Contains(":")) return null;
                var connectionStringSplit = ConnectionString.Split(':');
                try
                {
                    return connectionStringSplit.Length < 2 ? (int?) null : int.Parse(connectionStringSplit[1]);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"SiemensUtility: {ConnectionString} format error");
                    return null;
                }
            }
        }

        /// <summary>
        ///     西门子连接类型
        /// </summary>
        public SiemensType ConnectionType
        {
            get { return _siemensType; }
            set
            {
                _siemensType = value;
                switch (_siemensType)
                {
                    //PPI
                    case SiemensType.Ppi:
                    {
                        Wrapper = ConnectionString == null
                            ? new SiemensPpiProtocal(SlaveAddress, MasterAddress)
                            : new SiemensPpiProtocal(ConnectionString, SlaveAddress, MasterAddress);
                        break;
                    }
                    //MPI
                    case SiemensType.Mpi:
                    {
                        throw new NotImplementedException();
                    }
                    //Ethenet
                    case SiemensType.Tcp:
                    {
                        Wrapper = ConnectionString == null
                            ? new SiemensTcpProtocal(_tdpuSize, _taspSrc, _tsapDst, _maxCalling, _maxCalled, _maxPdu)
                            : (ConnectionStringPort == null
                                ? new SiemensTcpProtocal(_tdpuSize, _taspSrc, _tsapDst, _maxCalling, _maxCalled, _maxPdu,
                                    ConnectionString)
                                : new SiemensTcpProtocal(_tdpuSize, _taspSrc, _tsapDst, _maxCalling, _maxCalled, _maxPdu,
                                    ConnectionStringIp, ConnectionStringPort.Value));
                        break;
                    }
                }
            }
        }

        /// <summary>
        ///     设置连接类型
        /// </summary>
        /// <param name="connectionType">需要设置的连接类型</param>
        public override void SetConnectionType(int connectionType)
        {
            ConnectionType = (SiemensType) connectionType;
        }

        /// <summary>
        ///     读数据
        /// </summary>
        /// <param name="startAddress">开始地址</param>
        /// <param name="getByteCount">读取字节个数</param>
        /// <returns>从设备中读取的数据</returns>
        public override async Task<byte[]> GetDatasAsync(string startAddress, int getByteCount)
        {
            try
            {
                var readRequestSiemensInputStruct = new ReadRequestSiemensInputStruct(SlaveAddress, MasterAddress,
                    0xd3c7, SiemensTypeCode.Byte, startAddress, (ushort) getByteCount, AddressTranslator);
                var readRequestSiemensOutputStruct =
                    await
                        Wrapper.SendReceiveAsync<ReadRequestSiemensOutputStruct>(
                            Wrapper[typeof(ReadRequestSiemensProtocal)],
                            readRequestSiemensInputStruct);
                return readRequestSiemensOutputStruct?.GetValue;
            }
            catch (Exception e)
            {
                Log.Error(e, $"SiemensUtility -> GetDatas: {ConnectionString} error");
                return null;
            }
        }

        /// <summary>
        ///     写数据
        /// </summary>
        /// <param name="startAddress">开始地址</param>
        /// <param name="setContents">需要写入的数据</param>
        /// <returns>写入是否成功</returns>
        public override async Task<bool> SetDatasAsync(string startAddress, object[] setContents)
        {
            try
            {
                var writeRequestSiemensInputStruct = new WriteRequestSiemensInputStruct(SlaveAddress, MasterAddress,
                    0xd3c8, startAddress, setContents, AddressTranslator);
                var writeRequestSiemensOutputStruct =
                    await
                        Wrapper.SendReceiveAsync<WriteRequestSiemensOutputStruct>(
                            Wrapper[typeof(WriteRequestSiemensProtocal)],
                            writeRequestSiemensInputStruct);
                return writeRequestSiemensOutputStruct?.AccessResult == SiemensAccessResult.NoError;
            }
            catch (Exception e)
            {
                Log.Error(e, $"ModbusUtility -> SetDatas: {ConnectionString} error");
                return false;
            }
        }
    }
}