using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Caist.Siemens.Types;


namespace Caist.Siemens
{
    /// <summary>
    /// ����һ��Caistʵ������������������
    /// </summary>
    public partial class Plc : IDisposable
    {
        private const int CONNECTION_TIMED_OUT_ERROR_CODE = 10060;
        
        private TcpClient tcpClient;
        private NetworkStream stream;

        private int readTimeout = System.Threading.Timeout.Infinite;
        private int writeTimeout = System.Threading.Timeout.Infinite;

        /// <summary>
        /// PLC��IP��ַ
        /// </summary>
        public string IP { get; private set; }

        /// <summary>
        /// PLC�Ķ˿ںţ�Ĭ��Ϊ102
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// PLC��CPU����
        /// </summary>
        public CpuType CPU { get; private set; }

        /// <summary>
        /// PLC����
        /// </summary>
        public Int16 Rack { get; private set; }

        /// <summary>
        /// PLC��CPU���
        /// </summary>
        public Int16 Slot { get; private set; }

        /// <summary>
        /// ���cpu֧�ֵ����PDU��С
        /// </summary>
        public Int16 MaxPDUSize { get; set; }

        /// <summary>��ȡ�����ö�ȡ���������ȴ�PLC���ݵ�ʱ��.</summary>
        /// <returns>A <see cref="T:System.Int32" /> ��ָ����ȡ����ʧ��ǰ��������ʱ�䣬�Ժ���Ϊ��λ��Ĭ��ֵ, <see cref="F:System.Threading.Timeout.Infinite" />, ָ�����������ᳬʱ.</returns>
        public int ReadTimeout
        {
            get => readTimeout;
            set
            {
                readTimeout = value;
                if (tcpClient != null) tcpClient.ReceiveTimeout = readTimeout;
            }
        }

        /// <summary>��ȡ������д���������ȴ����ݵ�PLC��ʱ��. </summary>
        /// <returns>A <see cref="T:System.Int32" /> ��ָ��д����ʧ��ǰ��������ʱ�䣬�Ժ���Ϊ��λ��Ĭ��ֵ, <see cref="F:System.Threading.Timeout.Infinite" />, s�ر�˵��д�������ᳬʱ.</returns>
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
        /// ������Խ�����PLC�����ӣ�����true
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
        /// ����׽����Ƿ����ӣ�����ѯ��һ���Եȵ�(PLC)�������Ƿ����ӡ�
        /// ������Ӧ�ò��ϼ��ı������Բ鿴ͨ���Ƿ��ڹ���
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
        /// ʹ��������������в�������PLC����
        /// ����S7-1200��S7-1500��Ĭ��ֵΪ����=0�����=0��
        /// ���Ҫ���ӵ��ⲿ��̫������CP��������Ҫ���>0��
        /// ����S7-300��S7-400��Ĭ��ֵΪ����=0�����=2��
        /// </summary>
        /// <param name="cpu">PLC CPU����(select from the enum)</param>
        /// <param name="ip">PLC IP��ַ</param>
        /// <param name="rack">PLC�Ļ��ܣ�ͨ��Ϊ0��������Step7��TIA portal��Ӳ������</param>
        /// <param name="slot">PLC��CPU��ۣ�ͨ��S7300-S7400Ϊ2��S7-1200��S7-1500ͨ��Ϊ0��
        ///  ���ʹ���ⲿ��̫�������������Ӧ����.</param>
        public Plc(CpuType cpu, string ip, int port, Int16 rack, Int16 slot)
        {
            if (!System.Enum.IsDefined(typeof(CpuType), cpu))
                throw new ArgumentException($"���� '{nameof(cpu)}' ({cpu}) ��ֵ����ö��������Ч '{typeof(CpuType).Name}'.", nameof(cpu));

            if (string.IsNullOrEmpty(ip))
                throw new ArgumentException("IP��ַ�쳣��", nameof(ip));

            CPU = cpu;
            IP = ip;
            Port = port;
            Rack = rack;
            Slot = slot;
            MaxPDUSize = 240;
        }
        /// <summary>
        /// ʹ��������������в�������PLC����
        /// ����S7-1200��S7-1500��Ĭ��ֵΪ����=0�����=0��
        /// ���Ҫ���ӵ��ⲿ��̫������CP��������Ҫ���>0��
        /// ����S7-300��S7-400��Ĭ��ֵΪ����=0�����=2��
        /// </summary>
        /// <param name="cpu">PLC CPU����(select from the enum)</param>
        /// <param name="ip">PLC IP��ַ</param>
        /// <param name="rack">PLC�Ļ��ܣ�ͨ��Ϊ0��������Step7��TIA portal��Ӳ������</param>
        /// <param name="slot">PLC��CPU��ۣ�ͨ��S7300-S7400Ϊ2��S7-1200��S7-1500ͨ��Ϊ0��
        ///  ���ʹ���ⲿ��̫�������������Ӧ����.</param>
        public Plc(CpuType cpu, string ip, Int16 rack, Int16 slot)
        {
            if (!System.Enum.IsDefined(typeof(CpuType), cpu))
                throw new ArgumentException($"���� '{nameof(cpu)}' ({cpu}) ��ֵ����ö��������Ч '{typeof(CpuType).Name}'.", nameof(cpu));

            if (string.IsNullOrEmpty(ip))
                throw new ArgumentException("IP ��ַ��Ч", nameof(ip));

            CPU = cpu;
            IP = ip;
            Port = 102;
            Rack = rack;
            Slot = slot;
            MaxPDUSize = 240;
        }

        /// <summary>
        /// ��PLC��������
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
            // 12�ֽڵ�ͷ���ݣ�12�ֽڵĲ�������Ϊÿ��������
            if ((dataItems.Count + 1) * 12 > MaxPDUSize) throw new Exception("Ҫ���ȡ�ı���̫����");

            // 14�ֽڵ�ͷ���ݣ�4�ֽڵĽ������Ϊÿ���������ʵ������
            if (GetDataLength(dataItems) + dataItems.Count * 4 + 14 > MaxPDUSize) throw new Exception("�����ȡ������̫��");
        }

        private void AssertPduSizeForWrite(ICollection<DataItem> dataItems)
        {
            // 12���ֽڵ�ͷ���ݣ�18���ֽڵĲ�������Ϊÿ��������
            if (dataItems.Count * 18 + 12 > MaxPDUSize) throw new Exception("�ṩ��̫��ı�������д");

            // 12�ֽڵ�ͷ���ݣ�16�ֽڵ�����Ϊÿ���������ʵ������
            if (GetDataLength(dataItems) + dataItems.Count * 16 + 12 > MaxPDUSize)
                throw new Exception("�ṩ��̫������ݹ�д��");
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
                    $"�յ��� {s7Data.Length} bytes: '{BitConverter.ToString(s7Data)}', Ԥ�� {expectedLength} bytes.")
            ;

            if (s7Data == null)
                throw new PlcException(ErrorCode.WrongNumberReceivedBytes, "û��s7Data�յ���");

            if (s7Data.Length < 15) throw NotEnoughBytes();

            if (s7Data[14] != 0xff)
                throw new PlcException(ErrorCode.ReadData,
                    $"����PLC����Ч��Ӧ: '{BitConverter.ToString(s7Data)}'.");

            if (s7Data.Length < expectedLength) throw NotEnoughBytes();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>
        /// Plc�������
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
