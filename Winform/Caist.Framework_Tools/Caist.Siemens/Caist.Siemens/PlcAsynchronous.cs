using Caist.Siemens.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Caist.Siemens.Protocol;

namespace Caist.Siemens
{
    /// <summary>
    /// 创建一个Caist实例。西门子驱动程序
    /// </summary>
    public partial class Plc
    {
        /// <summary>
        /// 连接到PLC，执行COTP连接请求和S7通信设置。
        /// </summary>
        /// <returns></returns>
        public async Task OpenAsync()
        {
            await ConnectAsync();
            await stream.WriteAsync(ConnectionRequest.GetCOTPConnectionRequest(CPU, Rack, Slot), 0, 22);
            var response = await COTP.TPDU.ReadAsync(stream);
            if (response.PDUType != 0xd0)
            {
                throw new InvalidDataException("读取错误连接确认", response.TPkt.Data, 1, 0x0d);
            }
            await stream.WriteAsync(GetS7ConnectionSetup(), 0, 25);
            var s7data = await COTP.TSDU.ReadAsync(stream);
            if (s7data == null)
                throw new WrongNumberOfBytesException("没有收到响应通信设置的数据");
            if (s7data[1] != 0x03)
                throw new InvalidDataException("读取通信设置响应时出错", s7data, 1, 0x03);

            MaxPDUSize = (short)(s7data[18] * 256 + s7data[19]);
        }

        private async Task ConnectAsync()
        {
            tcpClient = new TcpClient();
            ConfigureConnection();
            await tcpClient.ConnectAsync(IP, Port);
            stream = tcpClient.GetStream();
        }

        /// <summary>
        /// 从指定的索引开始从数据库中读取字节数。它可以处理超过200个字节的多个请求。
        /// 如果读取不成功，请检查LastErrorCode或LastErrorString。
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出.</param>
        /// <param name="db">内存区域的地址(如果你想读DB1，这个被设置为1)。这个也必须为其他内存区域类型设置:计数器，计时器等等.</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <param name="count">字节计数，如果你想读取120字节，设置为120.</param>
        /// <returns></returns>
        public async Task<byte[]> ReadBytesAsync(DataType dataType, int db, int startByteAdr, int count)
        {
            List<byte> resultBytes = new List<byte>();
            int index = startByteAdr;
            while (count > 0)
            {
                //This works up to MaxPDUSize-1 on SNAP7. But not MaxPDUSize-0.
                var maxToRead = (int)Math.Min(count, MaxPDUSize - 18);
                byte[] bytes = await ReadBytesWithSingleRequestAsync(dataType, db, index, maxToRead);
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
        /// 这可以用来读取多个相同类型的连续变量(Word、DWord、Int等).
        /// 如果读取不成功，请检查LastErrorCode或LastErrorString
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出.</param>
        /// <param name="db">内存区域的地址(如果你想读DB1，这个被设置为1)。这个也必须为其他内存区域类型设置:计数器，计时器等等.</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <param name="varType">要读取的变量/s的类型</param>
        /// <param name="bitAdr">地址。如果要读取DB1.DBX200.6，请将6设置为该参数.</param>
        /// <param name="varCount"></param>
        public async Task<object> ReadAsync(DataType dataType, int db, int startByteAdr, VarType varType, int varCount, byte bitAdr = 0)
        {
            int cntBytes = VarTypeToByteLength(varType, varCount);
            byte[] bytes = await ReadBytesAsync(dataType, db, startByteAdr, cntBytes);
            return ParseBytes(varType, bytes, varCount, bitAdr);
        }

        /// <summary>
        /// 从PLC读取一个变量，接受输入字符串，如"DB1.DBX0.0"， "DB20。DBD200"， "MB20"， "T45"等。
        /// 如果读取不成功，请检查LastErrorCode或LastErrorString。
        /// </summary>
        /// <param name="variable">输入字符串，如"DB1.DBX0.0"， "DB20。DBD200"， "MB20"， "T45"等。</param>
        /// <returns></returns>
        public async Task<object> ReadAsync(string variable)
        {
            var adr = new PLCAddress(variable);
            return await ReadAsync(adr.DataType, adr.DbNumber, adr.StartByte, adr.VarType, 1, (byte)adr.BitNumber);
        }

        /// <summary>
        /// 在c#中读取填充结构所需的所有字节，从某个特定的地址开始，并返回一个可转换到结构的对象。
        /// </summary>
        /// <param name="structType">要读取的结构的类型。: TypeOf (MyStruct)).</param>
        /// <param name="db">DB/DB1</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <returns></returns>
        public async Task<object> ReadStructAsync(Type structType, int db, int startByteAdr = 0)
        {
            int numBytes = Types.Struct.GetStructSize(structType);
            var resultBytes = await ReadBytesAsync(DataType.DataBlock, db, startByteAdr, numBytes);
            return Types.Struct.FromBytes(structType, resultBytes);
        }

        /// <summary>
        /// 从某个地址开始，在c#中读取填充结构所需的所有字节，如果没有读取任何内容，则返回结构或null。
        /// </summary>
        /// <typeparam name="T">指令类型</typeparam>
        /// <param name="db">DB/DB1</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <returns></returns>
        public async Task<T?> ReadStructAsync<T>(int db, int startByteAdr = 0) where T : struct
        {
            return await ReadStructAsync(typeof(T), db, startByteAdr) as T?;
        }

        /// <summary>
        /// 从某个地址开始，读取c#中填充类所需的所有字节，并将所有属性值设置为从PLC读取的值。
        /// 它只读取属性，如果没有指定{get;set;}，它不会读取私有变量或公共变量。
        /// </summary>
        /// <param name="sourceClass">将存储值的类的实例</param>       
        /// <param name="db">DB/DB1</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <returns></returns>
        public async Task<Tuple<int, object>> ReadClassAsync(object sourceClass, int db, int startByteAdr = 0)
        {
            int numBytes = (int)Class.GetClassSize(sourceClass);
            if (numBytes <= 0)
            {
                throw new Exception("类的大小小于1字节，因此无法读取");
            }

            // now read the package
            var resultBytes = await ReadBytesAsync(DataType.DataBlock, db, startByteAdr, numBytes);
            // and decode it
            Class.FromBytes(sourceClass, resultBytes);

            return new Tuple<int, object>(resultBytes.Length, sourceClass);
        }

        /// <summary>
        /// 从某个地址开始，读取c#中填充类所需的所有字节，并将所有属性值设置为从PLC读取的值。
        /// 它只读取属性，如果没有指定{get;set;}，它不会读取私有变量或公共变量。实例化泛型定义的类类型，该类需要一个默认构造函数。 
        /// </summary>
        /// <typeparam name="T">将被实例化的类。需要一个默认构造函数</typeparam>
        /// <param name="db">DB/DB1</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <returns></returns>
        public async Task<T> ReadClassAsync<T>(int db, int startByteAdr = 0) where T : class
        {
            return await ReadClassAsync(() => Activator.CreateInstance<T>(), db, startByteAdr);
        }

        /// <summary>
        /// 从某个地址开始，读取c#中填充类所需的所有字节，并将所有属性值设置为从PLC读取的值。
        /// 它只读取属性，如果没有指定{get;set;}，它不会读取私有变量或公共变量.
        /// </summary>
        /// <typeparam name="T">将被实例化的类</typeparam>
        /// <param name="classFactory">函数实例化该类</param>
        /// <param name="db">DB/DB1</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <returns></returns>
        public async Task<T> ReadClassAsync<T>(Func<T> classFactory, int db, int startByteAdr = 0) where T : class
        {
            var instance = classFactory();
            var res = await ReadClassAsync(instance, db, startByteAdr);
            int readBytes = res.Item1;
            if (readBytes <= 0)
            {
                return null;
            }

            return (T)res.Item2;
        }

        /// <summary>
        /// 在一个请求中读取多个变量。
        /// 您必须创建并传递一个数据项列表，并且您将获得具有值的相同列表的响应。
        /// 值存储在数据项的属性“Value”中，并且已经进行了转换
        /// 如果不想进行转换，只需创建一个字节数据项。
        /// 数据项不能超过20(协议限制)和字节不能超过头的200 + 22(协议限制).
        /// </summary>
        /// <param name="dataItems">包含必须读取的变量列表的数据项列表。最多接受20个数据项.</param>
        public async Task<List<DataItem>> ReadMultipleVarsAsync(List<DataItem> dataItems)
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

                await stream.WriteAsync(package.Array, 0, package.Array.Length);

                var s7data = await COTP.TSDU.ReadAsync(stream); 
                if (s7data == null || s7data[14] != 0xff)
                    throw new PlcException(ErrorCode.WrongNumberReceivedBytes);

                ParseDataIntoDataItems(s7data, dataItems);
            }
            catch (SocketException socketException)
            {
                throw new PlcException(ErrorCode.ReadData, socketException);
            }
            catch (Exception exc)
            {
                throw new PlcException(ErrorCode.ReadData, exc);
            }
            return dataItems;
        }

        /// <summary>
        /// 从指定的索引开始从数据库中写入字节数。它可以处理超过200个字节的多个请求。
        /// 如果写入不成功，请检查LastErrorCode或LastErrorString。
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出.</param>
        /// <param name="db">内存区域的地址(如果你想读DB1，这个被设置为1)。这个也必须为其他内存区域类型设置:计数器，计时器等等.</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <param name="value">字节写。这个参数的长度不能超过200。如果需要更多，可以使用递归.</param>
        /// <returns></returns>
        public async Task WriteBytesAsync(DataType dataType, int db, int startByteAdr, byte[] value)
        {
            int localIndex = 0;
            int count = value.Length;
            while (count > 0)
            {
                var maxToWrite = (int)Math.Min(count, 200);
                await WriteBytesWithASingleRequestAsync(dataType, db, startByteAdr + localIndex, value.Skip(localIndex).Take(maxToWrite).ToArray());
                count -= maxToWrite;
                localIndex += maxToWrite;
            }
        }

        /// <summary>
        /// 用指定的索引从DB写入一个位。
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出.</param>
        /// <param name="db">内存区域的地址(如果你想读DB1，这个被设置为1)。这个也必须为其他内存区域类型设置:计数器，计时器等等.</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <param name="bitAdr">位的地址. (0-7)</param>
        /// <param name="value">字节写。这个参数的长度不能超过200。如果需要更多，可以使用递归.</param>
        /// <returns></returns>
        public async Task WriteBitAsync(DataType dataType, int db, int startByteAdr, int bitAdr, bool value)
        {
            if (bitAdr < 0 || bitAdr > 7)
                throw new InvalidAddressException(string.Format("寻址错误:你只能引用0-7位的位置。地址{0}无效", bitAdr));

            await WriteBitWithASingleRequestAsync(dataType, db, startByteAdr, bitAdr, value);
        }

        /// <summary>
        /// 用指定的索引从数据库写入一个位。
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出.</param>
        /// <param name="db">内存区域的地址(如果你想读DB1，这个被设置为1)。这个也必须为其他内存区域类型设置:计数器，计时器等等.</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <param name="bitAdr">位的地址. (0-7)</param>
        /// <param name="value">字节写。这个参数的长度不能超过200。如果需要更多，可以使用递归.</param>
        /// <returns></returns>
        public async Task WriteBitAsync(DataType dataType, int db, int startByteAdr, int bitAdr, int value)
        {
            if (value < 0 || value > 1)
                throw new ArgumentException("Value must be 0 or 1", nameof(value));

            await WriteBitAsync(dataType, db, startByteAdr, bitAdr, value == 1);
        }

        /// <summary>
        /// 接受一个对象的输入，并尝试将其解析为一个值数组。这可以用于写入许多相同类型的数据。
        /// 必须指定内存区域类型、内存地址、字节起始地址和字节计数。
        /// 如果读取不成功，请检查LastErrorCode或LastErrorString。
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出.</param>
        /// <param name="db">内存区域的地址(如果你想读DB1，这个被设置为1)。这个也必须为其他内存区域类型设置:计数器，计时器等等.</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <param name="value">字节写。这个参数的长度不能超过200。如果需要更多，可以使用递归.</param>
        /// <param name="bitAdr">位的地址. (0-7)</param>
        /// <returns></returns>
        public async Task WriteAsync(DataType dataType, int db, int startByteAdr, object value, int bitAdr = -1)
        {
            if (bitAdr != -1)
            {
                if (value is bool)
                {
                    await WriteBitAsync(dataType, db, startByteAdr, bitAdr, (bool) value);
                }
                else if (value is int intValue)
                {
                    if (intValue < 0 || intValue > 7)
                        throw new ArgumentOutOfRangeException(
                            string.Format(
                                "寻址错误:你只能引用0-7位的位置。地址{0}无效",
                                bitAdr), nameof(bitAdr));

                    await WriteBitAsync(dataType, db, startByteAdr, bitAdr, intValue == 1);
                }
                else throw new ArgumentException("值必须是bool或int才能写入位", nameof(value));
            }
            else await WriteBytesAsync(dataType, db, startByteAdr, Serialization.SerializeValue(value));
        }

        /// <summary>
        /// 从PLC写入一个变量，接收输入字符串，如“DB1.DBX0.0”，“DB20”。DBD200"， "MB20"， "T45"等。
        /// </summary>
        /// <param name="variable">输入字符串，如"DB1.DBX0.0"， "DB20。DBD200"， "MB20"， "T45"等.</param>
        /// <param name="value">要写入PLC的值</param>
        /// <returns>表示异步写操作的任务.</returns>
        public async Task WriteAsync(string variable, object value)
        {
            var adr = new PLCAddress(variable);
            await WriteAsync(adr.DataType, adr.DbNumber, adr.StartByte, value, adr.BitNumber);
        }

        /// <summary>
        /// 在PLC的数据库中写一个c#结构体
        /// </summary>
        /// <param name="structValue">要写的指令类型</param>
        /// <param name="db">Db 地址</param>
        /// <param name="startByteAdr">在PLC上启动字节</param>
        /// <returns>表示异步写操作的任务.</returns>
        public async Task WriteStructAsync(object structValue, int db, int startByteAdr = 0)
        {
            var bytes = Struct.ToBytes(structValue).ToList();
            await WriteBytesAsync(DataType.DataBlock, db, startByteAdr, bytes.ToArray());
        }

        /// <summary>
        /// 在PLC的数据库中编写一个c#类
        /// </summary>
        /// <param name="classValue">要写的指令类型</param>
        /// <param name="db">Db 地址</param>
        /// <param name="startByteAdr">在PLC上启动字节</param>
        /// <returns>表示异步写操作的任务.</returns>
        public async Task WriteClassAsync(object classValue, int db, int startByteAdr = 0)
        {
            byte[] bytes = new byte[(int)Class.GetClassSize(classValue)];
            Types.Class.ToBytes(classValue, bytes);
            await WriteBytesAsync(DataType.DataBlock, db, startByteAdr, bytes);
        }

        private async Task<byte[]> ReadBytesWithSingleRequestAsync(DataType dataType, int db, int startByteAdr, int count)
        {
            byte[] bytes = new byte[count];

            int packageSize = 31;
            ByteArray package = new ByteArray(packageSize);
            package.Add(ReadHeaderPackage());
            package.Add(CreateReadDataRequestPackage(dataType, db, startByteAdr, count));

            await stream.WriteAsync(package.Array, 0, package.Array.Length);

            var s7data = await COTP.TSDU.ReadAsync(stream);
            AssertReadResponse(s7data, count);

            for (int cnt = 0; cnt < count; cnt++)
                bytes[cnt] = s7data[cnt + 18];

            return bytes;
        }

        /// <summary>
        /// 向PLC写入数据项。如果响应无效，则抛出异常
        /// 或当PLC报告所写项目的错误时。
        /// </summary>
        /// <param name="dataItems">要写入到PLC的数据项</param>
        /// <returns>当从PLC的响应被解析时完成的任务.</returns>
        public async Task WriteAsync(params DataItem[] dataItems)
        {
            AssertPduSizeForWrite(dataItems);

            var message = new ByteArray();
            var length = S7WriteMultiple.CreateRequest(message, dataItems);
            await stream.WriteAsync(message.Array, 0, length).ConfigureAwait(false);

            var response = await COTP.TSDU.ReadAsync(stream).ConfigureAwait(false);
            S7WriteMultiple.ParseResponse(response, response.Length, dataItems);
        }

        /// <summary>
        /// 向PLC写入最多200个字节。必须指定内存区域类型、内存地址、字节起始地址和字节计数。
        /// </summary>
        /// <param name="dataType">数据类型的内存区域，可以是DB，定时器，计数器，Merker(内存)，输入，输出.</param>
        /// <param name="db">内存区域的地址(如果你想读DB1，这个被设置为1)。这个也必须为其他内存区域类型设置:计数器，计时器等等.</param>
        /// <param name="startByteAdr">开始字节的地址。如果你想读DB1。DBW200，这是200.</param>
        /// <param name="value">字节写。这个参数的长度不能超过200。如果需要更多，可以使用递归.</param>
        /// <returns>表示异步写操作的任务。</returns>
        private async Task WriteBytesWithASingleRequestAsync(DataType dataType, int db, int startByteAdr, byte[] value)
        {
            byte[] bReceive = new byte[513];
            int varCount = 0;

            try
            {
                varCount = value.Length;
                int packageSize = 35 + value.Length;
                ByteArray package = new ByteArray(packageSize);

                package.Add(new byte[] { 3, 0, 0 });
                package.Add((byte)packageSize);
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

                await stream.WriteAsync(package.Array, 0, package.Array.Length);

                var s7data = await COTP.TSDU.ReadAsync(stream);
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

        private async Task WriteBitWithASingleRequestAsync(DataType dataType, int db, int startByteAdr, int bitAdr, bool bitValue)
        {
            byte[] bReceive = new byte[513];
            int varCount = 0;

            try
            {
                var value = new[] {bitValue ? (byte) 1 : (byte) 0};
                varCount = value.Length;
                int packageSize = 35 + value.Length;
                ByteArray package = new Types.ByteArray(packageSize);
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

                await stream.WriteAsync(package.Array, 0, package.Array.Length);

                var s7data = await COTP.TSDU.ReadAsync(stream);
                if (s7data == null || s7data[14] != 0xff)
                    throw new PlcException(ErrorCode.WrongNumberReceivedBytes);
            }
            catch (Exception exc)
            {
                throw new PlcException(ErrorCode.WriteData, exc);
            }
        }
    }
}
