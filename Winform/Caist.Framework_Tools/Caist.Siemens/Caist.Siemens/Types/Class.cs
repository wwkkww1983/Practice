using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含将C类转换为S7数据类型的方法
    /// </summary>
    public static class Class
    {
        private static IEnumerable<PropertyInfo> GetAccessableProperties(Type classType)
        {
            return classType
#if NETSTANDARD1_3
                .GetTypeInfo().DeclaredProperties.Where(p => p.SetMethod != null);
#else
                .GetProperties(
                    BindingFlags.SetProperty |
                    BindingFlags.Public |
                    BindingFlags.Instance)
                .Where(p => p.GetSetMethod() != null);
#endif

        }

        private static double GetIncreasedNumberOfBytes(double numBytes, Type type)
        {
            switch (type.Name)
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
                    var propertyClass = Activator.CreateInstance(type);
                    numBytes = GetClassSize(propertyClass, numBytes, true);
                    break;
            }

            return numBytes;
        }

        /// <summary>
        /// 获取类的大小，以字节为单位。
        /// </summary>
        /// <param name="instance">class实例</param>
        /// <returns>字节数</returns>
        public static double GetClassSize(object instance, double numBytes = 0.0, bool isInnerProperty = false)
        {
            var properties = GetAccessableProperties(instance.GetType());
            foreach (var property in properties)
            {
                if (property.PropertyType.IsArray)
                {
                    Type elementType = property.PropertyType.GetElementType();
                    Array array = (Array)property.GetValue(instance, null);
                    if (array.Length <= 0)
                    {
                        throw new Exception("Cannot determine size of class, because an array is defined which has no fixed size greater than zero.");
                    }

                    IncrementToEven(ref numBytes);
                    for (int i = 0; i < array.Length; i++)
                    {
                        numBytes = GetIncreasedNumberOfBytes(numBytes, elementType);
                    }
                }
                else
                {
                    numBytes = GetIncreasedNumberOfBytes(numBytes, property.PropertyType);
                }
            }
            if (false == isInnerProperty)
            {
                numBytes = Math.Ceiling(numBytes);
                if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                    numBytes++;
            }
            return numBytes;
        }

        private static object GetPropertyValue(Type propertyType, byte[] bytes, ref double numBytes)
        {
            object value = null;

            switch (propertyType.Name)
            {
                case "Boolean":
                    int bytePos = (int)Math.Floor(numBytes);
                    int bitPos = (int)((numBytes - (double)bytePos) / 0.125);
                    if ((bytes[bytePos] & (int)Math.Pow(2, bitPos)) != 0)
                        value = true;
                    else
                        value = false;
                    numBytes += 0.125;
                    break;
                case "Byte":
                    numBytes = Math.Ceiling(numBytes);
                    value = (byte)(bytes[(int)numBytes]);
                    numBytes++;
                    break;
                case "Int16":
                    numBytes = Math.Ceiling(numBytes);
                    if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        numBytes++;
                    ushort source = Word.FromBytes(bytes[(int)numBytes + 1], bytes[(int)numBytes]);
                    value = source.ConvertToShort();
                    numBytes += 2;
                    break;
                case "UInt16":
                    numBytes = Math.Ceiling(numBytes);
                    if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        numBytes++;
                    value = Word.FromBytes(bytes[(int)numBytes + 1], bytes[(int)numBytes]);
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
                    value = sourceUInt.ConvertToInt();
                    numBytes += 4;
                    break;
                case "UInt32":
                    numBytes = Math.Ceiling(numBytes);
                    if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        numBytes++;
                    value = DWord.FromBytes(
                        bytes[(int)numBytes],
                        bytes[(int)numBytes + 1],
                        bytes[(int)numBytes + 2],
                        bytes[(int)numBytes + 3]);
                    numBytes += 4;
                    break;
                case "Double":
                    numBytes = Math.Ceiling(numBytes);
                    if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        numBytes++;
                    value = Double.FromByteArray(
                        new byte[] {
                            bytes[(int)numBytes],
                            bytes[(int)numBytes + 1],
                            bytes[(int)numBytes + 2],
                            bytes[(int)numBytes + 3] });
                    numBytes += 4;
                    break;
                case "Single":
                    numBytes = Math.Ceiling(numBytes);
                    if ((numBytes / 2 - Math.Floor(numBytes / 2.0)) > 0)
                        numBytes++;
                    value = Single.FromByteArray(
                        new byte[] {
                            bytes[(int)numBytes],
                            bytes[(int)numBytes + 1],
                            bytes[(int)numBytes + 2],
                            bytes[(int)numBytes + 3] });
                    numBytes += 4;
                    break;
                default:
                    var propClass = Activator.CreateInstance(propertyType);
                    numBytes = FromBytes(propClass, bytes, numBytes);
                    value = propClass;
                    break;
            }

            return value;
        }

        /// <summary>
        /// 用给定的字节数组设置对象的值
        /// </summary>
        /// <param name="sourceClass">要填充给定字节数组的对象</param>
        /// <param name="bytes">字节数组</param>
        public static double FromBytes(object sourceClass, byte[] bytes, double numBytes = 0, bool isInnerClass = false)
        {
            if (bytes == null)
                return numBytes;

            var properties = GetAccessableProperties(sourceClass.GetType());
            foreach (var property in properties)
            {
                if (property.PropertyType.IsArray)
                {
                    Array array = (Array)property.GetValue(sourceClass, null);
                    IncrementToEven(ref numBytes);
                    Type elementType = property.PropertyType.GetElementType();
                    for (int i = 0; i < array.Length && numBytes < bytes.Length; i++)
                    {
                        array.SetValue(
                            GetPropertyValue(elementType, bytes, ref numBytes),
                            i);
                    }
                }
                else
                {
                    property.SetValue(
                        sourceClass,
                        GetPropertyValue(property.PropertyType, bytes, ref numBytes),
                        null);
                }
            }

            return numBytes;
        }

        private static double SetBytesFromProperty(object propertyValue, byte[] bytes, double numBytes)
        {
            int bytePos = 0;
            int bitPos = 0;
            byte[] bytes2 = null;

            switch (propertyValue.GetType().Name)
            {
                case "Boolean":
                    bytePos = (int)Math.Floor(numBytes);
                    bitPos = (int)((numBytes - (double)bytePos) / 0.125);
                    if ((bool)propertyValue)
                        bytes[bytePos] |= (byte)Math.Pow(2, bitPos);            // is true
                    else
                        bytes[bytePos] &= (byte)(~(byte)Math.Pow(2, bitPos));   // is false
                    numBytes += 0.125;
                    break;
                case "Byte":
                    numBytes = (int)Math.Ceiling(numBytes);
                    bytePos = (int)numBytes;
                    bytes[bytePos] = (byte)propertyValue;
                    numBytes++;
                    break;
                case "Int16":
                    bytes2 = Int.ToByteArray((Int16)propertyValue);
                    break;
                case "UInt16":
                    bytes2 = Word.ToByteArray((UInt16)propertyValue);
                    break;
                case "Int32":
                    bytes2 = DInt.ToByteArray((Int32)propertyValue);
                    break;
                case "UInt32":
                    bytes2 = DWord.ToByteArray((UInt32)propertyValue);
                    break;
                case "Double":
                    bytes2 = Double.ToByteArray((double)propertyValue);
                    break;
                case "Single":
                    bytes2 = Single.ToByteArray((float)propertyValue);
                    break;
                default:
                    numBytes = ToBytes(propertyValue, bytes, numBytes);
                    break;
            }

            if (bytes2 != null)
            {
                IncrementToEven(ref numBytes);

                bytePos = (int)numBytes;
                for (int bCnt = 0; bCnt < bytes2.Length; bCnt++)
                    bytes[bytePos + bCnt] = bytes2[bCnt];
                numBytes += bytes2.Length;
            }

            return numBytes;
        }

        /// <summary>
        /// 根据结构类型创建字节数组
        /// </summary>
        /// <param name="sourceClass">结构体对象</param>
        /// <returns>一个字节数组或null如果失败.</returns>
        public static double ToBytes(object sourceClass, byte[] bytes, double numBytes = 0.0)
        {
            var properties = GetAccessableProperties(sourceClass.GetType());
            foreach (var property in properties)
            {
                if (property.PropertyType.IsArray)
                {
                    Array array = (Array)property.GetValue(sourceClass, null);
                    IncrementToEven(ref numBytes);
                    Type elementType = property.PropertyType.GetElementType();
                    for (int i = 0; i < array.Length && numBytes < bytes.Length; i++)
                    {
                        numBytes = SetBytesFromProperty(array.GetValue(i), bytes, numBytes);
                    }
                }
                else
                {
                    numBytes = SetBytesFromProperty(property.GetValue(sourceClass, null), bytes, numBytes);
                }
            }
            return numBytes;
        }

        private static void IncrementToEven(ref double numBytes)
        {
            numBytes = Math.Ceiling(numBytes);
            if (numBytes % 2 > 0) numBytes++;
        }
    }
}
