using System;

namespace Caist.Siemens.Protocol
{
    internal static class ConnectionRequest
    {
        public static byte[] GetCOTPConnectionRequest(CpuType cpu, Int16 rack, Int16 slot)
        {
            byte[] bSend1 = {
                    3, 0, 0, 22, //TPKT
                    17,     //COTP 头长度
                    224,    //请求内容 
                    0, 0,   //目的地参考
                    0, 46,  //来源参考
                    0,      //标记
                    193,    //参数编码 (src-tasp)
                    2,      //Parameter Length
                    1, 0,   //TASP 来源
                    194,    //参数编码 (dst-tasp)
                    2,      //参数长度
                    3, 0,   //TASP 结点
                    192,    //参数编码 (tpdu-size)
                    1,      //参数长度
                    10      //TPDU 大小 (2^10 = 1024)
                };

            switch (cpu)
            {
                case CpuType.S7200:////S7200:Chr（193）&Chr（2）&Chr（16）&Chr（0）'自有Tsap
                    bSend1[13] = 0x10;
                    bSend1[14] = 0x00;
                    //S7200:Chr（194）&Chr（2）&Chr（16）&Chr（0）‘外部 Tsap
                    bSend1[17] = 0x10;
                    bSend1[18] = 0x00;
                    break;
                case CpuType.Logo0BA8:
                    // 这些值取自NodeS7，如果这些值是
                    // 连接Logo0BA8的确切要求。
                    bSend1[13] = 0x01;
                    bSend1[14] = 0x00;
                    bSend1[17] = 0x01;
                    bSend1[18] = 0x02;
                    break;
                case CpuType.S71200:
                case CpuType.S7300:
                case CpuType.S7400:
                    //S7300: Chr(193) & Chr(2) & Chr(1) & Chr(0)  '自有 Tsap
                    bSend1[13] = 0x01;
                    bSend1[14] = 0x00;
                    //S7300: Chr(194) & Chr(2) & Chr(3) & Chr(2)  '外部 Tsap
                    bSend1[17] = 0x03;
                    bSend1[18] = (byte) ((rack << 5) | (int) slot);
                    break;
                case CpuType.S71500:
                    bSend1[13] = 0x10;
                    bSend1[14] = 0x02;
                    bSend1[17] = 0x03;
                    bSend1[18] = (byte) ((rack << 5) | (int) slot);
                    break;
                default:
                    throw new Exception("CPU类型错误");
            }
            return bSend1;
        }
    }
}
