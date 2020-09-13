using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Caist.Siemens.Types;


namespace Caist.Siemens
{
    /// <summary>
    /// 创建一个Caist实例。西门子驱动程序
    /// </summary>
    public partial class Plc : IDisposable
    {
        private const int CONNECTION_TIMED_OUT_ERROR_CODE = 10060;
        
        private TcpClient tcpClient;
        private NetworkStream stream;

        private int readTimeout = System.Threading.Timeout.Infinite;
        private int writeTimeout = System.Threading.Timeout.Infinite;

        /// <summary>
        /// PLC的IP地址
        /// </summary>
        public string IP { get; private set; }

        /// <summary>
        /// PLC的端口号，默认为102
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// PLC的CPU类型
        /// </summary>
        public CpuType CPU { get; private set; }

        /// <summary>
        /// PLC机架
        /// </summary>
        public Int16 Rack { get; private set; }

        /// <summary>
        /// PLC的CPU插槽
        /// </summary>
        public Int16 Slot { get; private set; }

        /// <summary>
        /// 这个cpu支持的最大PDU大小
        /// </summary>
        public Int16 MaxPDUSize { get; set; }

        /// <summary>获取或设置读取操作阻塞等待PLC数据的时间.</summary>
        /// <returns>A <see cref="T:System.Int32" /> 它指定读取操作失败前所经过的时间，以毫秒为单位。默认值, <see cref="F:System.Threading.Timeout.Infinite" />, 指定读操作不会超时.</returns>
        public int ReadTimeout
        {
            get => readTimeout;
            set
            {
                readTimeout = value;
                if (tcpClient != null) tcpClient.ReceiveTimeout = readTimeout;
            }
        }

        /// <summary>获取或设置写操作阻塞等待数据到PLC的时间. </summary>
        /// <returns>A <see cref="T:System.Int32" /> 它指定写操作失败前所经过的时间，以毫秒为单位。默认值, <see cref="F:System.Threading.Timeout.Infinite" />, s特别说明写操作不会超时.</returns>
        public int WriteTimeout
        {
            get => writeTimeout;
            set
            {
                writeTimeout = value;
                if (tcpClient != null) tcpClient.SendTimeout = writeTimeout;
            }
        }

        /// <summary>
        /// 如果可以建立与PLC的连接，返回true
        /// </summary>
        public bool IsAvailable
        {
            get
            {
                try
                {
                    Connect();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 检查套接字是否连接，并轮询另一个对等点(PLC)，看它是否连接。
        /// 这是您应该不断检查的变量，以查看通信是否在工作
        /// </summary>
        public bool IsConnected
        {
            get
            {
                try
                {
                    if (tcpClient == null)
                        return false;
                    return tcpClient.Connected;
                }
                catch { return false; }
            }
        }

        /// <summary>
        /// 使用连接所需的所有参数创建PLC对象。
        /// 对于S7-1200和S7-1500，默认值为机架=0，插槽=0。
        /// 如果要连接到外部以太网卡（CP），则需要插槽>0。
        /// 对于S7-300和S7-400，默认值为机架=0，插槽=2。
        /// </summary>
        /// <param name="cpu">PLC CPU类型(select from the enum)</param>
        /// <param name="ip">PLC IP地址</param>
        /// <param name="rack">PLC的机架，通常为0，但请检查Step7或TIA portal的硬件配置</param>
        /// <param name="slot">PLC的CPU插槽，通常S7300-S7400为2，S7-1200和S7-1500通常为0。
        ///  如果使用外部以太网卡，则必须相应设置.</param>
        public Plc(CpuType cpu, string ip, int port, Int16 rack, Int16 slot)
        {
            if (!System.Enum.IsDefined(typeof(CpuType), cpu))
                throw new ArgumentException($"参数 '{nameof(cpu)}' ({cpu}) 的值对于枚举类型无效 '{typeof(CpuType).Name}'.", nameof(cpu));

            if (string.IsNullOrEmpty(ip))
                throw new ArgumentException("IP地址异常！", nameof(ip));

            CPU = cpu;
            IP = ip;
            Port = port;
            Rack = rack;
            Slot = slot;
            MaxPDUSize = 240;
        }
        /// <summary>
        /// 使用连接所需的所有参数创建PLC对象。
        /// 对于S7-1200和S7-1500，默认值为机架=0，插槽=0。
        /// 如果要连接到外部以太网卡（CP），则需要插槽>0。
        /// 对于S7-300和S7-400，默认值为机架=0，插槽=2。
        /// </summary>
        /// <param name="cpu">PLC CPU类型(select from the enum)</param>
        /// <param name="ip">PLC IP地址</param>
        /// <param name="rack">PLC的机架，通常为0，但请检查Step7或TIA portal的硬件配置</param>
        /// <param name="slot">PLC的CPU插槽，通常S7300-S7400为2，S7-1200和S7-1500通常为0。
        ///  如果使用外部以太网卡，则必须相应设置.</param>
        public Plc(CpuType cpu, string ip, Int16 rack, Int16 slot)
        {
            if (!System.Enum.IsDefined(typeof(CpuType), cpu))
                throw new ArgumentException($"参数 '{nameof(cpu)}' ({cpu}) 的值对于枚举类型无效 '{typeof(CpuType).Name}'.", nameof(cpu));

            if (string.IsNullOrEmpty(ip))
                throw new ArgumentException("IP 地址无效", nameof(ip));

            CPU = cpu;
            IP = ip;
            Port = 102;
            Rack = rack;
            Slot = slot;
            MaxPDUSize = 240;
        }

        /// <summary>
        /// 与PLC紧密连接
        /// </summary>
        public void Close()
        {
            if (tcpClient != null)
            {
                if (tcpClient.Connected) tcpClient.Close();
            }
        }

        private void AssertPduSizeForRead(ICollection<DataItem> dataItems)
        {
            // 12字节的头数据，12字节的参数数据为每个数据项
            if ((dataItems.Count + 1) * 12 > MaxPDUSize) throw new Exception("要求读取的变量太多了");

            // 14字节的头数据，4字节的结果数据为每个数据项和实际数据
            if (GetDataLength(dataItems) + dataItems.Count * 4 + 14 > MaxPDUSize) throw new Exception("请求读取的数据太多");
        }

        private void AssertPduSizeForWrite(ICollection<DataItem> dataItems)
        {
            // 12个字节的头数据，18个字节的参数数据为每个数据项
            if (dataItems.Count * 18 + 12 > MaxPDUSize) throw new Exception("提供了太多的变量供编写");

            // 12字节的头数据，16字节的数据为每个数据项和实际数据
            if (GetDataLength(dataItems) + dataItems.Count * 16 + 12 > MaxPDUSize)
                throw new Exception("提供了太多的数据供写入");
        }

        private void ConfigureConnection()
        {
            if (tcpClient == null)
            {
                return;
            }

            tcpClient.ReceiveTimeout = ReadTimeout;
            tcpClient.SendTimeout = WriteTimeout;
        }

        private int GetDataLength(IEnumerable<DataItem> dataItems)
        {
            return dataItems.Select(di => VarTypeToByteLength(di.VarType, di.Count))
                .Sum(len => (len & 1) == 1 ? len + 1 : len);
        }

        private static void AssertReadResponse(byte[] s7Data, int dataLength)
        {
            var expectedLength = dataLength + 18;

            PlcException NotEnoughBytes() =>
                new PlcException(ErrorCode.WrongNumberReceivedBytes,
                    $"收到了 {s7Data.Length} bytes: '{BitConverter.ToString(s7Data)}', 预期 {expectedLength} bytes.")
            ;

            if (s7Data == null)
                throw new PlcException(ErrorCode.WrongNumberReceivedBytes, "没有s7Data收到。");

            if (s7Data.Length < 15) throw NotEnoughBytes();

            if (s7Data[14] != 0xff)
                throw new PlcException(ErrorCode.ReadData,
                    $"来自PLC的无效响应: '{BitConverter.ToString(s7Data)}'.");

            if (s7Data.Length < expectedLength) throw NotEnoughBytes();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>
        /// Plc处理对象
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Close();
                }
                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
