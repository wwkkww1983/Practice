using System;
using System.Collections.Generic;
using Caist.Siemens.Types;

namespace Caist.Siemens.Protocol
{
    internal static class S7WriteMultiple
    {
        public static int CreateRequest(ByteArray message, DataItem[] dataItems)
        {
            message.Add(Header.Template);

            message[Header.Offsets.ParameterCount] = (byte) dataItems.Length;
            var paramSize = dataItems.Length * Parameter.Template.Length;

            Serialization.SetWordAt(message, Header.Offsets.ParameterSize,
                (ushort) (2 + paramSize));

            var paramOffset = Header.Template.Length;
            var dataOffset = paramOffset + paramSize;
            var data = new ByteArray();

            var itemCount = 0;

            foreach (var item in dataItems)
            {
                itemCount++;
                message.Add(Parameter.Template);
                var value = Serialization.SerializeDataItem(item);
                var wordLen = item.Value is bool ? 1 : 2;

                message[paramOffset + Parameter.Offsets.WordLength] = (byte) wordLen;
                Serialization.SetWordAt(message, paramOffset + Parameter.Offsets.Amount, (ushort) value.Length);
                Serialization.SetWordAt(message, paramOffset + Parameter.Offsets.DbNumber, (ushort) item.DB);
                message[paramOffset + Parameter.Offsets.Area] = (byte) item.DataType;

                data.Add(0x00);
                if (item.Value is bool b)
                {
                    if (item.BitAdr > 7)
                        throw new ArgumentException(
                            $"无法读取无效的位 {nameof(item.BitAdr)} '{item.BitAdr}'.", nameof(dataItems));

                    Serialization.SetAddressAt(message, paramOffset + Parameter.Offsets.Address, item.StartByteAdr,
                        item.BitAdr);

                    data.Add(0x03);
                    data.AddWord(1);

                    data.Add(b ? (byte)1 : (byte)0);
                    if (itemCount != dataItems.Length) { 
                        data.Add(0);
                    }
                }
                else
                {
                    Serialization.SetAddressAt(message, paramOffset + Parameter.Offsets.Address, item.StartByteAdr, 0);

                    var len = value.Length;
                    data.Add(0x04);
                    data.AddWord((ushort) (len << 3));
                    data.Add(value);
                    
                    if ((len & 0b1) == 1 && itemCount != dataItems.Length)
                    {
                        data.Add(0);
                    }
                }

                paramOffset += Parameter.Template.Length;
            }

            message.Add(data.Array);

            Serialization.SetWordAt(message, Header.Offsets.MessageLength, (ushort) message.Length);
            Serialization.SetWordAt(message, Header.Offsets.DataLength, (ushort) (message.Length - paramOffset));

            return message.Length;
        }

        public static void ParseResponse(byte[] message, int length, DataItem[] dataItems)
        {
            if (length < 12) throw new Exception("接收到的数据不足，无法分析写入响应.");

            var messageError = Serialization.GetWordAt(message, 10);
            if (messageError != 0)
                throw new Exception($"写入失败，出现错误 {messageError}.");

            if (length < 14 + dataItems.Length)
                throw new Exception("接收到的数据不足，无法分析单个项目的响应.");

            IList<byte> itemResults = new ArraySegment<byte>(message, 14, dataItems.Length);

            List<Exception> errors = null;

            for (int i = 0; i < dataItems.Length; i++)
            {
                var result = itemResults[i];
                if (result == 0xff) continue;

                if (errors == null) errors = new List<Exception>();
                errors.Add(new Exception($"写入dataItem{dataItems[i]}失败，错误代码为{result}."));
            }

            if (errors != null)
                throw new AggregateException(
                    $"写入失败{errors.Count}项目。有关详细信息，请参见.", errors);
        }

        private static class Header
        {
            public static byte[] Template { get; } =
            {
                0x03, 0x00, 0x00, 0x00, // TPKT
                0x02, 0xf0, 0x80, // ISO DT
                0x32, // S7 协议 ID
                0x01, // 工作申请
                0x00, 0x00, // 保留
                0x05, 0x00, // PDU 参考
                0x00, 0x0e, // 参数长度
                0x00, 0x00, // 数据长度
                0x05, // 函数: Write var
                0x00, // 要写入的项目数
            };

            public static class Offsets
            {
                public const int MessageLength = 2;
                public const int ParameterSize = 13;
                public const int DataLength = 15;
                public const int ParameterCount = 18;
            }
        }

        private static class Parameter
        {
            public static byte[] Template { get; } =
            {
                0x12, // 规格
                0x0a, // 剩余字节长度
                0x10, // 寻址方式
                0x02, // 传输大小
                0x00, 0x00, // 元素数量
                0x00, 0x00, // DB 数量
                0x84, // 区域类型
                0x00, 0x00, 0x00 // 面积偏移量
            };

            public static class Offsets
            {
                public const int WordLength = 3;
                public const int Amount = 4;
                public const int DbNumber = 6;
                public const int Area = 8;
                public const int Address = 9;
            }
        }
    }
}
