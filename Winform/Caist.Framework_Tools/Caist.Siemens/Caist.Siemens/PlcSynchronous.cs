using Caist.Siemens.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Caist.Siemens.Protocol;

namespace Caist.Siemens
{
    public partial class Plc
    {
        /// <summary>
        /// 连接到PLC，执行COTP连接请求和S7通信设置。
        /// </summary>
        public void Open()
        {
            Connect();

            try
            {
                stream.Write(ConnectionRequest.GetCOTPConnectionRequest(CPU, Rack, Slot), 0, 22);
                var response = COTP.TPDU.Read(stream);
                if (response.PDUType != 0xd0) //Connect Confirm
                {
                    throw new InvalidDataException("读取错误连接确认", response.TPkt.Data, 1, 0x0d);
                }

                stream.Write(GetS7ConnectionSetup(), 0, 25);

                var s7data = COTP.TSDU.Read(stream);
                if (s7data == null)
                    throw new WrongNumberOfBytesException("没有收到响应通信设置的数据");

                //Check for S7 Ack Data
                if (s7data[1] != 0x03)
                    throw new InvalidDataException("读取通信设置响应时出错", s7data, 1, 0x03);

                MaxPDUSize = (short)(s7data[18] * 256 + s7data[19]);
            }
            catch (Exception exc)
            {
                throw new PlcException(ErrorCode.ConnectionError,
                    $"无法建立到{IP}.\nMessage的连接: {exc.Message}", exc);
            }
        }

        private void Connect()
        {
            try
            {
                tcpClient = new TcpClient();
                ConfigureConnection();
                tcpClient.Connect(IP, Port);
                stream = tcpClient.GetStream();
            }
            catch (SocketException sex)
            {
                throw new PlcException(
                    sex.SocketErrorCode == SocketError.TimedOut
                        ? ErrorCode.IPAddressNotAvailable
                        : ErrorCode.ConnectionError, sex);
            }
            catch (Exception ex)
            {
                throw new PlcException(ErrorCode.ConnectionError, ex);
            }
        }

        /// <summary>
        /// 从指定的索引开始从数据库中读取字节数。它可以处理超过200个字节的多个请求
        /// 如果读取不成功，请检查LastErrorCode或LastErrorString。
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出。</param>
        /// <param name="db">内存区域的地址(如果你想读DB1，这个被设置为1)。这个也必须为其他内存区域类型设置:计数器，计时器等等.</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <param name="count">字节计数，如果你想读取120字节，设置为120.</param>
        /// <returns></returns>
        public byte[] ReadBytes(DataType dataType, int db, int startByteAdr, int count)
        {
            List<byte> resultBytes = new List<byte>();
            int index = startByteAdr;
            while (count > 0)
            {
                var maxToRead = (int)Math.Min(count, MaxPDUSize - 18);
                byte[] bytes = ReadBytesWithSingleRequest(dataType, db, index, maxToRead);
                if (bytes == null)
                    return resultBytes.ToArray();
                resultBytes.AddRange(bytes);
                count -= maxToRead;
                index += maxToRead;
            }
            return resultBytes.ToArray();
        }

        /// <summary>
        /// 读取并解码提供的“VarType”的特定字节数。
        /// 这可以用来读取多个相同类型的连续变量(Word、DWord、Int等)。
        /// 如果读取不成功，请检查LastErrorCode或LastErrorString。
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出。</param>
        /// <param name="db">内存区域的地址(如果你想读DB1，这个被设置为1)。这个也必须为其他内存区域类型设置:计数器，计时器等等.</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <param name="varType">要读取的变量/s的类型</param>
        /// <param name="bitAdr">地址。如果要读取DB1.DBX200.6，请将6设置为该参数。</param>
        /// <param name="varCount"></param>
        public object Read(DataType dataType, int db, int startByteAdr, VarType varType, int varCount, byte bitAdr = 0)
        {
            int cntBytes = VarTypeToByteLength(varType, varCount);
            byte[] bytes = ReadBytes(dataType, db, startByteAdr, cntBytes);

            return ParseBytes(varType, bytes, varCount, bitAdr);
        }

        /// <summary>
        /// 从PLC读取一个变量，接受输入字符串，如"DB1.DBX0.0"， "DB20。DBD200"， "MB20"， "T45"等。
        /// 如果读取不成功，请检查LastErrorCode或LastErrorString。
        /// </summary>
        /// <param name="variable">输入字符串，如"DB1.DBX0.0"， "DB20。DBD200"， "MB20"， "T45"等.</param>
        /// <returns></returns>
        public object Read(string variable)
        {
            var adr = new PLCAddress(variable);
            return Read(adr.DataType, adr.DbNumber, adr.StartByte, adr.VarType, 1, (byte)adr.BitNumber);
        }

        /// <summary>
        /// 从PLC读取一个变量，接受输入字符串，如"DB1.DBX0.0"， "DB20。DBD200"， "MB20"， "T45"等。
        /// 如果读取不成功，请检查LastErrorCode或LastErrorString。
        /// </summary>
        /// <param name="variable">输入字符串，如"DB1.DBX0.0"， "DB20。DBD200"， "MB20"， "T45"等.</param>
        /// <returns></returns>
        public object Read(string variable, VarType varType)
        {
            var adr = new PLCAddress(variable);
            return Read(adr.DataType, adr.DbNumber, adr.StartByte, varType, 1, (byte)adr.BitNumber);
        }

        /// <summary>
        /// 在c#中读取填充结构所需的所有字节，从某个特定的地址开始，并返回一个可转换到结构的对象。
        /// </summary>
        /// <param name="structType">要读取的结构的类型。: TypeOf (MyStruct)).</param>
        /// <param name="db">数据库地址.</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <returns></returns>
        public object ReadStruct(Type structType, int db, int startByteAdr = 0)
        {
            int numBytes = Struct.GetStructSize(structType);
            var resultBytes = ReadBytes(DataType.DataBlock, db, startByteAdr, numBytes);
            return Struct.FromBytes(structType, resultBytes);
        }

        /// <summary>
        /// 从某个地址开始，在c#中读取填充结构所需的所有字节，如果没有读取任何内容，则返回结构或null。
        /// </summary>
        /// <typeparam name="T">将被实例化的类</typeparam>
        /// <param name="db"> DB/DB1</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想写DB1。DBW200，这是200。</param>
        /// <returns>Returns a nullable struct. If nothing was read null will be returned.</returns>
        public T? ReadStruct<T>(int db, int startByteAdr = 0) where T : struct
        {
            return ReadStruct(typeof(T), db, startByteAdr) as T?;
        }


        /// <summary>
        /// 从某个地址开始，读取c#中填充类所需的所有字节，并将所有属性值设置为从PLC读取的值。
        /// 它只读取属性，如果没有指定{get;set;}，它不会读取私有变量或公共变量。
        /// </summary>
        /// <param name="sourceClass">将存储值的类的实例</param>       
        /// <param name="db"> DB/DB1</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想写DB1。DBW200，这是200。</param>
        /// <returns></returns>
        public int ReadClass(object sourceClass, int db, int startByteAdr = 0)
        {
            int numBytes = (int)Class.GetClassSize(sourceClass);
            if (numBytes <= 0)
            {
                throw new Exception("类的大小小于1字节，因此无法读取");
            }
            var resultBytes = ReadBytes(DataType.DataBlock, db, startByteAdr, numBytes);
            Class.FromBytes(sourceClass, resultBytes);
            return resultBytes.Length;
        }

        /// <summary>
        /// 从某个地址开始，读取c#中填充类所需的所有字节，并将所有属性值设置为从PLC读取的值
        /// 它只读取属性，如果没有指定{get;set;}，它不会读取私有变量或公共变量。实例化泛型定义的类
        /// 类型，该类需要一个默认构造函数。
        /// </summary>
        /// <typeparam name="T">将被实例化的类</typeparam>
        /// <param name="db"> DB/DB1</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想写DB1。DBW200，这是200。</param>
        /// <returns>具有从PLC读取的值的类的实例。如果没有读取数据，将返回null</returns>
        public T ReadClass<T>(int db, int startByteAdr = 0) where T : class
        {
            return ReadClass(() => Activator.CreateInstance<T>(), db, startByteAdr);
        }

        /// <summary>
        /// 从某个地址开始，读取c#中填充类所需的所有字节，并将所有属性值设置为从PLC读取的值。
        /// 它只读取属性，如果没有指定{get;set;}，它不会读取私有变量或公共变量。
        /// </summary>
        /// <typeparam name="T">将被实例化的类</typeparam>
        /// <param name="classFactory">函数实例化该类</param>
        /// <param name="db"> DB/DB1</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想写DB1。DBW200，这是200。</param>
        /// <returns>具有从PLC读取的值的类的实例。如果没有读取数据，将返回null</returns>
        public T ReadClass<T>(Func<T> classFactory, int db, int startByteAdr = 0) where T : class
        {
            var instance = classFactory();
            int readBytes = ReadClass(instance, db, startByteAdr);
            if (readBytes <= 0)
            {
                return null;
            }
            return instance;
        }

        /// <summary>
        /// 从指定的索引开始从数据库中写入字节数。它可以处理超过200个字节的多个请求。
        /// 如果写入不成功，请检查LastErrorCode或LastErrorString。
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出。</param>
        /// <param name="db">内存区域的地址(如果你想写DB1，这是设置为1)。这必须为其他内存区域类型设置:计数器，计时器等。</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想写DB1。DBW200，这是200。</param>
        /// <param name="value">值要写入(0或1)。</param>
        public void WriteBytes(DataType dataType, int db, int startByteAdr, byte[] value)
        {
            int localIndex = 0;
            int count = value.Length;
            while (count > 0)
            {
                var maxToWrite = Math.Min(count, MaxPDUSize - 28);
                WriteBytesWithASingleRequest(dataType, db, startByteAdr + localIndex, value.Skip(localIndex).Take(maxToWrite).ToArray());
                count -= maxToWrite;
                localIndex += maxToWrite;
            }
        }

        /// <summary>
        /// 用指定的索引从数据库写入一个位。
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出。</param>
        /// <param name="db">内存区域的地址(如果你想写DB1，这是设置为1)。这必须为其他内存区域类型设置:计数器，计时器等。</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想写DB1。DBW200，这是200。</param>
        /// <param name="bitAdr">位的地址. (0-7)</param>
        /// <param name="value">值要写入(0或1)。</param>
        public void WriteBit(DataType dataType, int db, int startByteAdr, int bitAdr, bool value)
        {
            if (bitAdr < 0 || bitAdr > 7)
                throw new InvalidAddressException(string.Format("寻址错误:你只能引用0-7位的位置。地址{0}无效", bitAdr));

            WriteBitWithASingleRequest(dataType, db, startByteAdr, bitAdr, value);
        }

        /// <summary>
        /// 用指定的索引向数据库写入一个位。
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出。</param>
        /// <param name="db">内存区域的地址(如果你想写DB1，这是设置为1)。这必须为其他内存区域类型设置:计数器，计时器等。</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想写DB1。DBW200，这是200。</param>
        /// <param name="bitAdr">位的地址. (0-7)</param>
        /// <param name="value">值要写入(0或1)。</param>
        public void WriteBit(DataType dataType, int db, int startByteAdr, int bitAdr, int value)
        {
            if (value < 0 || value > 1)
                throw new ArgumentException("值必须为0或1", nameof(value));

            WriteBit(dataType, db, startByteAdr, bitAdr, value == 1);
        }

        /// <summary>
        /// 接受一个对象的输入，并尝试将其解析为一个值数组。这可以用于写入许多相同类型的数据
        /// 必须指定内存区域类型、内存地址、字节起始地址和字节计数.
        /// 如果读取不成功，请检查LastErrorCode或LastErrorString.
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出。</param>
        /// <param name="db">内存区域的地址(如果你想写DB1，这是设置为1)。这必须为其他内存区域类型设置:计数器，计时器等。</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想写DB1。DBW200，这是200。</param>
        /// <param name="bitAdr">位的地址. (0-7)</param>
        /// <param name="value">值要写入(0或1)。</param>
        public void Write(DataType dataType, int db, int startByteAdr, object value, int bitAdr = -1)
        {
            if (bitAdr != -1)
            {
                if (value is bool)
                {
                    WriteBit(dataType, db, startByteAdr, bitAdr, (bool) value);
                }
                else if (value is int intValue)
                {
                    if (intValue < 0 || intValue > 7)
                        throw new ArgumentOutOfRangeException(
                            string.Format(
                                "寻址错误:你只能引用0-7位的位置。地址{0}无效",
                                bitAdr), nameof(bitAdr));

                    WriteBit(dataType, db, startByteAdr, bitAdr, intValue == 1);
                }
                else
                    throw new ArgumentException("值必须是bool或int才能写入位", nameof(value));
            }
            else WriteBytes(dataType, db, startByteAdr, Serialization.SerializeValue(value));
        }

        /// <summary>
        /// 从PLC写入一个变量，接收输入字符串，如“DB1.DBX0.0”、“DB20.DBD200”、“MB20”、“T45”等。
        /// 如果写入不成功，请检查<see cref="LastErrorCode"/> or <see cref="LastErrorString"/>.
        /// </summary>
        /// <param name="variable">输入字符串“DB1.DBX0.0”、“DB20.DBD200”、“MB20”、“T45”等。</param>
        /// <param name="value">要写入PLC的值</param>
        public void Write(string variable, object value)
        {
            var adr = new PLCAddress(variable);
            Write(adr.DataType, adr.DbNumber, adr.StartByte, value, adr.BitNumber);
        }

        /// <summary>
        /// 在PLC的数据库中写一个c#结构体
        /// </summary>
        /// <param name="structValue">要写的结构</param>
        /// <param name="db">Db 地址</param>
        /// <param name="startByteAdr">在PLC上启动字节</param>
        public void WriteStruct(object structValue, int db, int startByteAdr = 0)
        {
            WriteStructAsync(structValue, db, startByteAdr).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 在PLC的数据库中编写一个c#类
        /// </summary>
        /// <param name="classValue">要写的课程</param>
        /// <param name="db">Db 地址</param>
        /// <param name="startByteAdr">在PLC上启动字节</param>
        public void WriteClass(object classValue, int db, int startByteAdr = 0)
        {
            WriteClassAsync(classValue, db, startByteAdr).GetAwaiter().GetResult();
        }

        private byte[] ReadBytesWithSingleRequest(DataType dataType, int db, int startByteAdr, int count)
        {
            byte[] bytes = new byte[count];
            try
            {
                int packageSize = 31;
                ByteArray package = new ByteArray(packageSize);
                package.Add(ReadHeaderPackage());
                package.Add(CreateReadDataRequestPackage(dataType, db, startByteAdr, count));
                stream.Write(package.Array, 0, package.Array.Length);
                var s7data = COTP.TSDU.Read(stream);
                AssertReadResponse(s7data, count);
                for (int cnt = 0; cnt < count; cnt++)
                    bytes[cnt] = s7data[cnt + 18];
                return bytes;
            }
            catch (Exception exc)
            {
                throw new PlcException(ErrorCode.ReadData, exc);
            }
        }

        /// <summary>
        /// 将数据项写入PLC。如果响应无效，则引发异常
        /// 或当PLC报告写入项目错误时。
        /// </summary>
        /// <param name="dataItems">写入PLC的数据项。</param>
        public void Write(params DataItem[] dataItems)
        {
            AssertPduSizeForWrite(dataItems);

            var message = new ByteArray();
            var length = S7WriteMultiple.CreateRequest(message, dataItems);
            stream.Write(message.Array, 0, length);

            var response = COTP.TSDU.Read(stream);
            S7WriteMultiple.ParseResponse(response, response.Length, dataItems);
        }

        private void WriteBytesWithASingleRequest(DataType dataType, int db, int startByteAdr, byte[] value)
        {
            int varCount = 0;
            try
            {
                varCount = value.Length;
                int packageSize = 35 + value.Length;
                ByteArray package = new ByteArray(packageSize);
                package.Add(new byte[] { 3, 0 });
                package.Add(Int.ToByteArray((short)packageSize));
                package.Add(new byte[] { 2, 0xf0, 0x80, 0x32, 1, 0, 0 });
                package.Add(Word.ToByteArray((ushort)(varCount - 1)));
                package.Add(new byte[] { 0, 0x0e });
                package.Add(Word.ToByteArray((ushort)(varCount + 4)));
                package.Add(new byte[] { 0x05, 0x01, 0x12, 0x0a, 0x10, 0x02 });
                package.Add(Word.ToByteArray((ushort)varCount));
                package.Add(Word.ToByteArray((ushort)(db)));
                package.Add((byte)dataType);
                var overflow = (int)(startByteAdr * 8 / 0xffffU); 
                package.Add((byte)overflow);
                package.Add(Word.ToByteArray((ushort)(startByteAdr * 8)));
                package.Add(new byte[] { 0, 4 });
                package.Add(Word.ToByteArray((ushort)(varCount * 8)));
                package.Add(value);
                stream.Write(package.Array, 0, package.Array.Length);
                var s7data = COTP.TSDU.Read(stream);
                if (s7data == null || s7data[14] != 0xff)
                {
                    throw new PlcException(ErrorCode.WrongNumberReceivedBytes);
                }
            }
            catch (Exception exc)
            {
                throw new PlcException(ErrorCode.WriteData, exc);
            }
        }

        private void WriteBitWithASingleRequest(DataType dataType, int db, int startByteAdr, int bitAdr, bool bitValue)
        {
            int varCount = 0;

            try
            {
                var value = new[] {bitValue ? (byte) 1 : (byte) 0};
                varCount = value.Length;
                int packageSize = 35 + value.Length;
                ByteArray package = new ByteArray(packageSize);
                package.Add(new byte[] { 3, 0, 0 });
                package.Add((byte)packageSize);
                package.Add(new byte[] { 2, 0xf0, 0x80, 0x32, 1, 0, 0 });
                package.Add(Word.ToByteArray((ushort)(varCount - 1)));
                package.Add(new byte[] { 0, 0x0e });
                package.Add(Word.ToByteArray((ushort)(varCount + 4)));
                package.Add(new byte[] { 0x05, 0x01, 0x12, 0x0a, 0x10, 0x01 });
                package.Add(Word.ToByteArray((ushort)varCount));
                package.Add(Word.ToByteArray((ushort)(db)));
                package.Add((byte)dataType);
                int overflow = (int)(startByteAdr * 8 / 0xffffU);
                package.Add((byte)overflow);
                package.Add(Word.ToByteArray((ushort)(startByteAdr * 8 + bitAdr)));
                package.Add(new byte[] { 0, 0x03 });
                package.Add(Word.ToByteArray((ushort)(varCount)));
                package.Add(value);
                stream.Write(package.Array, 0, package.Array.Length);
                var s7data = COTP.TSDU.Read(stream);
                if (s7data == null || s7data[14] != 0xff)
                    throw new PlcException(ErrorCode.WrongNumberReceivedBytes);
            }
            catch (Exception exc)
            {
                throw new PlcException(ErrorCode.WriteData, exc);
            }
        }

        /// <summary>
        /// 在一个请求中读取多个变量。
        /// 您必须创建并传递一个数据项列表，并且您将获得具有值的相同列表的响应。
        /// 值存储在数据项的属性“Value”中，并且已经进行了转换。
        /// 如果不想进行转换，只需创建一个字节数据项
        /// 数据项不能超过20(协议限制)和字节不能超过头的200 + 22(协议限制)。
        /// </summary>
        /// <param name="dataItems">包含必须读取的变量列表的数据项列表。最多接受20个数据项.</param>
        public void ReadMultipleVars(List<DataItem> dataItems)
        {
            AssertPduSizeForRead(dataItems);
            try
            {
                int packageSize = 19 + (dataItems.Count * 12);
                ByteArray package = new ByteArray(packageSize);
                package.Add(ReadHeaderPackage(dataItems.Count));
                foreach (var dataItem in dataItems)
                {
                    package.Add(CreateReadDataRequestPackage(dataItem.DataType, dataItem.DB, dataItem.StartByteAdr, VarTypeToByteLength(dataItem.VarType, dataItem.Count)));
                }

                stream.Write(package.Array, 0, package.Array.Length);

                var s7data = COTP.TSDU.Read(stream);
                if (s7data == null || s7data[14] != 0xff)
                    throw new PlcException(ErrorCode.WrongNumberReceivedBytes);

                ParseDataIntoDataItems(s7data, dataItems);
            }
            catch (Exception exc)
            {
                throw new PlcException(ErrorCode.ReadData, exc);
            }
        }
    }
}
