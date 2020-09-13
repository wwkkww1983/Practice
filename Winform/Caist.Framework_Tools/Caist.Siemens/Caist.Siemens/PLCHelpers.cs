using Caist.Siemens.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using DateTime = Caist.Siemens.Types.DateTime;

namespace Caist.Siemens
{
    public partial class Plc
    {
        /// <summary>
        /// 创建从PLC读取字节的头
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private ByteArray ReadHeaderPackage(int amount = 1)
        {
            //头大小= 19个字节
            var package = new Types.ByteArray(19);
            package.Add(new byte[] { 0x03, 0x00 });
            //完整的包大小
            package.Add(Types.Int.ToByteArray((short)(19 + (12 * amount))));
            package.Add(new byte[] { 0x02, 0xf0, 0x80, 0x32, 0x01, 0x00, 0x00, 0x00, 0x00 });
            //数据部分的大小
            package.Add(Types.Word.ToByteArray((ushort)(2 + (amount * 12))));
            package.Add(new byte[] { 0x00, 0x00, 0x04 });
            //数量的要求
            package.Add((byte)amount);

            return package;
        }

        /// <summary>
        /// 创建字节包从PLC请求数据。你必须指定内存类型(dataType)，
        /// 内存的地址，字节的地址和字节计数。
        /// </summary>
        /// <param name="dataType">内存类型(DB，定时器，计数器等)</param>
        /// <param name="db">要读取的内存地址</param>
        /// <param name="startByteAdr">字节的起始地址</param>
        /// <param name="count">要读取的字节数</param>
        /// <returns></returns>
        private ByteArray CreateReadDataRequestPackage(DataType dataType, int db, int startByteAdr, int count = 1)
        {
            //single data req = 12
            var package = new Types.ByteArray(12);
            package.Add(new byte[] { 0x12, 0x0a, 0x10 });
            switch (dataType)
            {
                case DataType.Timer:
                case DataType.Counter:
                    package.Add((byte)dataType);
                    break;
                default:
                    package.Add(0x02);
                    break;
            }

            package.Add(Word.ToByteArray((ushort)(count)));
            package.Add(Word.ToByteArray((ushort)(db)));
            package.Add((byte)dataType);
            var overflow = (int)(startByteAdr * 8 / 0xffffU);
            package.Add((byte)overflow);
            switch (dataType)
            {
                case DataType.Timer:
                case DataType.Counter:
                    package.Add(Types.Word.ToByteArray((ushort)(startByteAdr)));
                    break;
                default:
                    package.Add(Types.Word.ToByteArray((ushort)((startByteAdr) * 8)));
                    break;
            }

            return package;
        }

        /// <summary>
        /// 给定一个S7变量类型(Bool、Word、DWord等)，它将以适当的c#格式转换字节。
        /// </summary>
        /// <param name="varType"></param>
        /// <param name="bytes"></param>
        /// <param name="varCount"></param>
        /// <param name="bitAdr"></param>
        /// <returns></returns>
        private object ParseBytes(VarType varType, byte[] bytes, int varCount, byte bitAdr = 0)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            switch (varType)
            {
                case VarType.Byte:
                    if (varCount == 1)
                        return bytes[0];
                    else
                        return bytes;
                case VarType.Word:
                    if (varCount == 1)
                        return Word.FromByteArray(bytes);
                    else
                        return Word.ToArray(bytes);
                case VarType.Int:
                    if (varCount == 1)
                        return Int.FromByteArray(bytes);
                    else
                        return Int.ToArray(bytes);
                case VarType.DWord:
                    if (varCount == 1)
                        return DWord.FromByteArray(bytes);
                    else
                        return DWord.ToArray(bytes);
                case VarType.DInt:
                    if (varCount == 1)
                        return DInt.FromByteArray(bytes);
                    else
                        return DInt.ToArray(bytes);
                case VarType.Real:
                    if (varCount == 1)
                        return Types.Single.FromByteArray(bytes);
                    else
                        return Types.Single.ToArray(bytes);

                case VarType.String:
                    return Types.String.FromByteArray(bytes);
                case VarType.StringEx:
                    return StringEx.FromByteArray(bytes);

                case VarType.Timer:
                    if (varCount == 1)
                        return Timer.FromByteArray(bytes);
                    else
                        return Timer.ToArray(bytes);
                case VarType.Counter:
                    if (varCount == 1)
                        return Counter.FromByteArray(bytes);
                    else
                        return Counter.ToArray(bytes);
                case VarType.Bit:
                    if (varCount == 1)
                    {
                        if (bitAdr > 7)
                            return null;
                        else
                            return Bit.FromByte(bytes[0], bitAdr);
                    }
                    else
                    {
                        return Bit.ToBitArray(bytes, varCount);
                    }
                case VarType.DateTime:
                    if (varCount == 1)
                    {
                        return DateTime.FromByteArray(bytes);
                    }
                    else
                    {
                        return DateTime.ToArray(bytes);
                    }
                default:
                    return null;
            }
        }

        /// <summary>
        /// 给定一个S7 <see cref="VarType"/>(Bool, Word, DWord等)，它返回多少字节读取。
        /// </summary>
        /// <param name="varType"></param>
        /// <param name="varCount"></param>
        /// <returns></returns>
        private int VarTypeToByteLength(VarType varType, int varCount = 1)
        {
            switch (varType)
            {
                case VarType.Bit:
                    return varCount + 7 / 8;
                case VarType.Byte:
                    return (varCount < 1) ? 1 : varCount;
                case VarType.String:
                    return varCount;
                case VarType.StringEx:
                    return varCount + 2;
                case VarType.Word:
                case VarType.Timer:
                case VarType.Int:
                case VarType.Counter:
                    return varCount * 2;
                case VarType.DWord:
                case VarType.DInt:
                case VarType.Real:
                    return varCount * 4;
                case VarType.DateTime:
                    return varCount * 8;
                default:
                    return 0;
            }
        }

        private byte[] GetS7ConnectionSetup()
        {
            return new byte[] {  3, 0, 0, 25, 2, 240, 128, 50, 1, 0, 0, 255, 255, 0, 8, 0, 0, 240, 0, 0, 3, 0, 3,
                    3, 192
            };
        }

        private void ParseDataIntoDataItems(byte[] s7data, List<DataItem> dataItems)
        {
            int offset = 14;
            foreach (var dataItem in dataItems)
            {
                if (s7data[offset] != 0xff)
                    throw new PlcException(ErrorCode.WrongNumberReceivedBytes);
                offset += 4;
                int byteCnt = VarTypeToByteLength(dataItem.VarType, dataItem.Count);
                dataItem.Value = ParseBytes(
                    dataItem.VarType,
                    s7data.Skip(offset).Take(byteCnt).ToArray(),
                    dataItem.Count,
                    dataItem.BitAdr
                );
                offset += byteCnt;
                if (dataItem.Count % 2 != 0 && (dataItem.VarType == VarType.Byte || dataItem.VarType == VarType.Bit))
                    offset++;
            }
        }
    }
}
