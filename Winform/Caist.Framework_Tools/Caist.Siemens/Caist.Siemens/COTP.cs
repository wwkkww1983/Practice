using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Caist.Siemens
{

    /// <summary>
    /// COTP协议函数和类型
    /// </summary>
    internal class COTP
    {
        /// <summary>
        /// 描述一个COTP TPDU(传输协议数据单元)
        /// </summary>
        public class TPDU
        {
            public TPKT TPkt { get; }
            public byte HeaderLength;
            public byte PDUType;
            public int TPDUNumber;
            public byte[] Data;
            public bool LastDataUnit;

            public TPDU(TPKT tPKT)
            {
                TPkt = tPKT;

                var br = new BinaryReader(new MemoryStream(tPKT.Data));
                HeaderLength = br.ReadByte();
                if (HeaderLength >= 2)
                {
                    PDUType = br.ReadByte();
                    if (PDUType == 0xf0) //DT Data
                    {
                        var flags = br.ReadByte();
                        TPDUNumber = flags & 0x7F;
                        LastDataUnit = (flags & 0x80) > 0;
                        Data = br.ReadBytes(tPKT.Length - HeaderLength - 4); //4 = TPKT Size
                        return;
                    }
                }
                Data = new byte[0];
            }

            /// <summary>
            /// 从网络流读取COTP TPDU(传输协议数据单元)
            /// </summary>
            /// <param name="stream">要读取的Socket</param>
            /// <returns></returns>
            public static TPDU Read(Stream stream)
            {
                var tpkt = TPKT.Read(stream);
                if (tpkt.Length > 0) return new TPDU(tpkt);
                return null;
            }

            /// <summary>
            /// 从网络流读取COTP TPDU(传输协议数据单元)
            /// </summary>
            /// <param name="stream">要读取的Socket from</param>
            /// <returns>COTP DPDU instance</returns>
            public static async Task<TPDU> ReadAsync(Stream stream)
            {
                var tpkt = await TPKT.ReadAsync(stream);
                if (tpkt.Length > 0) return new TPDU(tpkt);
                return null;
            }

            public override string ToString()
            {
                return string.Format("长度:{0}PDUType: {1} tpdun编号:{2}最后:{3}段数据:{4}",
                    HeaderLength,
                    PDUType,
                    TPDUNumber,
                    LastDataUnit,
                    BitConverter.ToString(Data)
                    );
            }

        }

        /// <summary>
        /// 描述一个COTP TSDU(传输服务数据单元)
        /// </summary>
        public class TSDU
        {
            /// <summary>
            ///读取完整的COTP TSDU(传输服务数据单元)
            /// </summary>
            /// <param name="stream">要读取的流</param>
            /// <returns></returns>
            public static byte[] Read(Stream stream)
            {
                var segment = TPDU.Read(stream);
                if (segment == null) return null;

                var buffer = new byte[segment.Data.Length];
                var output = new MemoryStream(buffer);
                output.Write(segment.Data, 0, segment.Data.Length);

                while (!segment.LastDataUnit)
                {
                    segment = TPDU.Read(stream);
                    Array.Resize(ref buffer, buffer.Length + segment.Data.Length);
                    var lastPosition = output.Position;
                    output = new MemoryStream(buffer);
                    output.Write(segment.Data, (int) lastPosition, segment.Data.Length);
                }

                return buffer.Take((int)output.Position).ToArray();
            }

            /// <summary>
            /// 读取完整的COTP TSDU(传输服务数据单元)
            /// </summary>
            /// <param name="stream">要读取的流</param>
            /// <returns>Data in TSDU</returns>
            public static async Task<byte[]> ReadAsync(Stream stream)
            {                
                var segment = await TPDU.ReadAsync(stream);
                if (segment == null) return null;

                var buffer = new byte[segment.Data.Length];
                var output = new MemoryStream(buffer);
                output.Write(segment.Data, 0, segment.Data.Length);

                while (!segment.LastDataUnit)
                {
                    segment = await TPDU.ReadAsync(stream);
                    Array.Resize(ref buffer, buffer.Length + segment.Data.Length);
                    var lastPosition = output.Position;
                    output = new MemoryStream(buffer);
                    output.Write(segment.Data, (int) lastPosition, segment.Data.Length);
                }
                return buffer.Take((int)output.Position).ToArray();
            }
        }
    }
}
