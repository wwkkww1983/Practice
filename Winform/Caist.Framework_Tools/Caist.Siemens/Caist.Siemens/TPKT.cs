using System;
using System.IO;
using System.Threading.Tasks;

namespace Caist.Siemens
{

    /// <summary>
    /// 描述一个TPKT包
    /// </summary>
    internal class TPKT
    {
        public byte Version;
        public byte Reserved1;
        public int Length;
        public byte[] Data;

        /// <summary>
        /// 从套接字读取TPKT
        /// </summary>
        /// <param name="stream">要读取的流</param>
        /// <returns>TPKT实例</returns>
        public static TPKT Read(Stream stream)
        {
            var buf = new byte[4];
            int len = stream.Read(buf, 0, 4);
            if (len < 4) throw new TPKTInvalidException("TPKT不完整/无效");
            var pkt = new TPKT
            {
                Version = buf[0],
                Reserved1 = buf[1],
                Length = buf[2] * 256 + buf[3] 
            };
            if (pkt.Length > 0)
            {
                pkt.Data = new byte[pkt.Length - 4];
                len = stream.Read(pkt.Data, 0, pkt.Length - 4);
                if (len < pkt.Length - 4)
                    throw new TPKTInvalidException("TPKT不完整/无效");
            }
            return pkt;
        }

        /// <summary>
        /// 从套接字异步读取TPKT
        /// </summary>
        /// <param name="stream">要读取的流</param>
        /// <returns></returns>
        public static async Task<TPKT> ReadAsync(Stream stream)
        {
            var buf = new byte[4];
            int len = await stream.ReadAsync(buf, 0, 4);
            if (len < 4) throw new TPKTInvalidException("TPKT不完整/无效");
            var pkt = new TPKT
            {
                Version = buf[0],
                Reserved1 = buf[1],
                Length = buf[2] * 256 + buf[3]
            };
            if (pkt.Length > 0)
            {
                pkt.Data = new byte[pkt.Length - 4];
                len = await stream.ReadAsync(pkt.Data, 0, pkt.Length - 4);
                if (len < pkt.Length - 4) throw new TPKTInvalidException("TPKT不完整/无效");
            }
            return pkt;
        }

        public override string ToString()
        {
            return string.Format("版本:{0}长度:{1}数据:{2}",
                Version,
                Length,
                BitConverter.ToString(Data)
                );
        }
    }
}
