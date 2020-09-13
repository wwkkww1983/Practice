using System;
using System.Reflection;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含将c#结构体转换为S7数据类型的方法
    /// </summary>
    public static class Struct
    {
        /// <summary>
        /// 获取结构的大小，以字节为单位。
        /// </summary>
        /// <param name="structType">结构的类型</param>
        /// <returns>字节数</returns>
        public static int GetStructSize(Type structType)
        {
            double numBytes = 0.0;

            var infos = structType
            #if NETSTANDARD1_3
                .GetTypeInfo().DeclaredFields;
            #else
                .GetFields();
            #endif

            foreach (var info in infos)
            {
                switch (info.FieldType.Name)
                {
                    case "Boolean":
                        numBytes += 0.125;
                        break;
                    case "Byte":
                        numBytes = Math.Ceiling(numBytes);
                        numBytes++;
                        break;
                    case "Int16":
                    case "UInt16":
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        numBytes += 2;
                        break;
                    case "Int32":
                    case "UInt32":
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        numBytes += 4;
                        break;
                    case "Single":
                    case "Double":
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        numBytes += 4;
                        break;
                    default:
                        numBytes += GetStructSize(info.FieldType);
                        break;
                }
            }
            return (int)numBytes;
        }

        /// <summary>
        /// 通过字节数组创建指定类型的结构。
        /// </summary>
        /// <param name="structType">结构体类型</param>
        /// <param name="bytes">字节数组</param>
        /// <returns>对象取决于结构类型，如果失败则为null(数组-length !=结构-length)</returns>
        public static object FromBytes(Type structType, byte[] bytes)
        {
            if (bytes == null)
                return null;
            if (bytes.Length != GetStructSize(structType))
                return null;
            int bytePos = 0;
            int bitPos = 0;
            double numBytes = 0.0;
            object structValue = Activator.CreateInstance(structType);
            var infos = structValue.GetType()
            #if NETSTANDARD1_3
                .GetTypeInfo().DeclaredFields;
            #else
                .GetFields();
            #endif
            foreach (var info in infos)
            {
                switch (info.FieldType.Name)
                {
                    case "Boolean":
                        bytePos = (int)Math.Floor(numBytes);
                        bitPos = (int)((numBytes - (double)bytePos) / 0.125);
                        if ((bytes[bytePos] & (int)Math.Pow(2, bitPos)) != 0)
                            info.SetValue(structValue, true);
                        else
                            info.SetValue(structValue, false);
                        numBytes += 0.125;
                        break;
                    case "Byte":
                        numBytes = Math.Ceiling(numBytes);
                        info.SetValue(structValue, (byte)(bytes[(int)numBytes]));
                        numBytes++;
                        break;
                    case "Int16":
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        ushort source = Word.FromBytes(bytes[(int)numBytes + 1], bytes[(int)numBytes]);
                        info.SetValue(structValue, source.ConvertToShort());
                        numBytes += 2;
                        break;
                    case "UInt16":
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        info.SetValue(structValue, Word.FromBytes(bytes[(int)numBytes + 1],
                                                                          bytes[(int)numBytes]));
                        numBytes += 2;
                        break;
                    case "Int32":
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        uint sourceUInt = DWord.FromBytes(bytes[(int)numBytes + 3],
                                                                           bytes[(int)numBytes + 2],
                                                                           bytes[(int)numBytes + 1],
                                                                           bytes[(int)numBytes + 0]);
                        info.SetValue(structValue, sourceUInt.ConvertToInt());
                        numBytes += 4;
                        break;
                    case "UInt32":
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        info.SetValue(structValue, DWord.FromBytes(bytes[(int)numBytes],
                                                                           bytes[(int)numBytes + 1],
                                                                           bytes[(int)numBytes + 2],
                                                                           bytes[(int)numBytes + 3]));
                        numBytes += 4;
                        break;
                    case "Double":
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        info.SetValue(structValue, Double.FromByteArray(new byte[] { bytes[(int)numBytes],
                                                                           bytes[(int)numBytes + 1],
                                                                           bytes[(int)numBytes + 2],
                                                                           bytes[(int)numBytes + 3] }));
                        numBytes += 4;
                        break;
                    case "Single":
                        numBytes = Math.Ceiling(numBytes);
                        if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                            numBytes++;
                        info.SetValue(structValue, Single.FromByteArray(new byte[] { bytes[(int)numBytes],
                                                                           bytes[(int)numBytes + 1],
                                                                           bytes[(int)numBytes + 2],
                                                                           bytes[(int)numBytes + 3] }));
                        numBytes += 4;
                        break;
                    default:
                        var buffer = new byte[GetStructSize(info.FieldType)];
                        if (buffer.Length == 0)
                            continue;
                        Buffer.BlockCopy(bytes, (int)Math.Ceiling(numBytes), buffer, 0, buffer.Length);
                        info.SetValue(structValue, FromBytes(info.FieldType, buffer));
                        numBytes += buffer.Length;
                        break;
                }
            }
            return structValue;
        }

        /// <summary>
        /// 根据结构类型创建字节数组。
        /// </summary>
        /// <param name="structValue">结构体对象</param>
        /// <returns>一个字节数组或null如果失败.</returns>
        public static byte[] ToBytes(object structValue)
        {
            Type type = structValue.GetType();

            int size = Struct.GetStructSize(type);
            byte[] bytes = new byte[size];
            byte[] bytes2 = null;

            int bytePos = 0;
            int bitPos = 0;
            double numBytes = 0.0;

            var infos = type
            #if NETSTANDARD1_3
                .GetTypeInfo().DeclaredFields;
            #else
                .GetFields();
            #endif

            foreach (var info in infos)
            {
                bytes2 = null;
                switch (info.FieldType.Name)
                {
                    case "Boolean":
                        // get the value
                        bytePos = (int)Math.Floor(numBytes);
                        bitPos = (int)((numBytes - (double)bytePos) / 0.125);
                        if ((bool)info.GetValue(structValue))
                            bytes[bytePos] |= (byte)Math.Pow(2, bitPos);            // is true
                        else
                            bytes[bytePos] &= (byte)(~(byte)Math.Pow(2, bitPos));   // is false
                        numBytes += 0.125;
                        break;
                    case "Byte":
                        numBytes = (int)Math.Ceiling(numBytes);
                        bytePos = (int)numBytes;
                        bytes[bytePos] = (byte)info.GetValue(structValue);
                        numBytes++;
                        break;
                    case "Int16":
                        bytes2 = Int.ToByteArray((Int16)info.GetValue(structValue));
                        break;
                    case "UInt16":
                        bytes2 = Word.ToByteArray((UInt16)info.GetValue(structValue));
                        break;
                    case "Int32":
                        bytes2 = DInt.ToByteArray((Int32)info.GetValue(structValue));
                        break;
                    case "UInt32":
                        bytes2 = DWord.ToByteArray((UInt32)info.GetValue(structValue));
                        break;
                    case "Double":
                        bytes2 = Double.ToByteArray((double)info.GetValue(structValue));
                        break;
                    case "Single":
                        bytes2 = Single.ToByteArray((float)info.GetValue(structValue));
                        break;
                }
                if (bytes2 != null)
                {
                    numBytes = Math.Ceiling(numBytes);
                    if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        numBytes++;
                    bytePos = (int)numBytes;
                    for (int bCnt = 0; bCnt < bytes2.Length; bCnt++)
                        bytes[bytePos + bCnt] = bytes2[bCnt];
                    numBytes += bytes2.Length;
                }
            }
            return bytes;
        }
    }
}
