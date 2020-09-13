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
    /// ����һ��Caistʵ������������������
    /// </summary>
    public partial class Plc
    {
        /// <summary>
        /// ���ӵ�PLC��ִ��COTP���������S7ͨ�����á�
        /// </summary>
        /// <returns></returns>
        public async Task OpenAsync()
        {
            await ConnectAsync();
            await stream.WriteAsync(ConnectionRequest.GetCOTPConnectionRequest(CPU, Rack, Slot), 0, 22);
            var response = await COTP.TPDU.ReadAsync(stream);
            if (response.PDUType != 0xd0)
            {
                throw new InvalidDataException("��ȡ��������ȷ��", response.TPkt.Data, 1, 0x0d);
            }
            await stream.WriteAsync(GetS7ConnectionSetup(), 0, 25);
            var s7data = await COTP.TSDU.ReadAsync(stream);
            if (s7data == null)
                throw new WrongNumberOfBytesException("û���յ���Ӧͨ�����õ�����");
            if (s7data[1] != 0x03)
                throw new InvalidDataException("��ȡͨ��������Ӧʱ����", s7data, 1, 0x03);

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
        /// ��ָ����������ʼ�����ݿ��ж�ȡ�ֽ����������Դ�����200���ֽڵĶ������
        /// �����ȡ���ɹ�������LastErrorCode��LastErrorString��
        /// </summary>
        /// <param name="dataType">�������͵��ڴ����򣬿�����DB����ʱ������������Merker(�ڴ�)�����룬���.</param>
        /// <param name="db">�ڴ�����ĵ�ַ(��������DB1�����������Ϊ1)�����Ҳ����Ϊ�����ڴ�������������:����������ʱ���ȵ�.</param>
        /// <param name="startByteAdr">��ʼ�ֽڵĵ�ַ����������DB1��DBW200������200.</param>
        /// <param name="count">�ֽڼ�������������ȡ120�ֽڣ�����Ϊ120.</param>
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
        /// ��ȡ�������ṩ�ġ�VarType�����ض��ֽ�����
        /// �����������ȡ�����ͬ���͵���������(Word��DWord��Int��).
        /// �����ȡ���ɹ�������LastErrorCode��LastErrorString
        /// </summary>
        /// <param name="dataType">�������͵��ڴ����򣬿�����DB����ʱ������������Merker(�ڴ�)�����룬���.</param>
        /// <param name="db">�ڴ�����ĵ�ַ(��������DB1�����������Ϊ1)�����Ҳ����Ϊ�����ڴ�������������:����������ʱ���ȵ�.</param>
        /// <param name="startByteAdr">��ʼ�ֽڵĵ�ַ����������DB1��DBW200������200.</param>
        /// <param name="varType">Ҫ��ȡ�ı���/s������</param>
        /// <param name="bitAdr">��ַ�����Ҫ��ȡDB1.DBX200.6���뽫6����Ϊ�ò���.</param>
        /// <param name="varCount"></param>
        public async Task<object> ReadAsync(DataType dataType, int db, int startByteAdr, VarType varType, int varCount, byte bitAdr = 0)
        {
            int cntBytes = VarTypeToByteLength(varType, varCount);
            byte[] bytes = await ReadBytesAsync(dataType, db, startByteAdr, cntBytes);
            return ParseBytes(varType, bytes, varCount, bitAdr);
        }

        /// <summary>
        /// ��PLC��ȡһ�����������������ַ�������"DB1.DBX0.0"�� "DB20��DBD200"�� "MB20"�� "T45"�ȡ�
        /// �����ȡ���ɹ�������LastErrorCode��LastErrorString��
        /// </summary>
        /// <param name="variable">�����ַ�������"DB1.DBX0.0"�� "DB20��DBD200"�� "MB20"�� "T45"�ȡ�</param>
        /// <returns></returns>
        public async Task<object> ReadAsync(string variable)
        {
            var adr = new PLCAddress(variable);
            return await ReadAsync(adr.DataType, adr.DbNumber, adr.StartByte, adr.VarType, 1, (byte)adr.BitNumber);
        }

        /// <summary>
        /// ��c#�ж�ȡ���ṹ����������ֽڣ���ĳ���ض��ĵ�ַ��ʼ��������һ����ת�����ṹ�Ķ���
        /// </summary>
        /// <param name="structType">Ҫ��ȡ�Ľṹ�����͡�: TypeOf (MyStruct)).</param>
        /// <param name="db">DB/DB1</param>
        /// <param name="startByteAdr">��ʼ�ֽڵĵ�ַ����������DB1��DBW200������200.</param>
        /// <returns></returns>
        public async Task<object> ReadStructAsync(Type structType, int db, int startByteAdr = 0)
        {
            int numBytes = Types.Struct.GetStructSize(structType);
            var resultBytes = await ReadBytesAsync(DataType.DataBlock, db, startByteAdr, numBytes);
            return Types.Struct.FromBytes(structType, resultBytes);
        }

        /// <summary>
        /// ��ĳ����ַ��ʼ����c#�ж�ȡ���ṹ����������ֽڣ����û�ж�ȡ�κ����ݣ��򷵻ؽṹ��null��
        /// </summary>
        /// <typeparam name="T">ָ������</typeparam>
        /// <param name="db">DB/DB1</param>
        /// <param name="startByteAdr">��ʼ�ֽڵĵ�ַ����������DB1��DBW200������200.</param>
        /// <returns></returns>
        public async Task<T?> ReadStructAsync<T>(int db, int startByteAdr = 0) where T : struct
        {
            return await ReadStructAsync(typeof(T), db, startByteAdr) as T?;
        }

        /// <summary>
        /// ��ĳ����ַ��ʼ����ȡc#�����������������ֽڣ�������������ֵ����Ϊ��PLC��ȡ��ֵ��
        /// ��ֻ��ȡ���ԣ����û��ָ��{get;set;}���������ȡ˽�б����򹫹�������
        /// </summary>
        /// <param name="sourceClass">���洢ֵ�����ʵ��</param>       
        /// <param name="db">DB/DB1</param>
        /// <param name="startByteAdr">��ʼ�ֽڵĵ�ַ����������DB1��DBW200������200.</param>
        /// <returns></returns>
        public async Task<Tuple<int, object>> ReadClassAsync(object sourceClass, int db, int startByteAdr = 0)
        {
            int numBytes = (int)Class.GetClassSize(sourceClass);
            if (numBytes <= 0)
            {
                throw new Exception("��Ĵ�СС��1�ֽڣ�����޷���ȡ");
            }

            // now read the package
            var resultBytes = await ReadBytesAsync(DataType.DataBlock, db, startByteAdr, numBytes);
            // and decode it
            Class.FromBytes(sourceClass, resultBytes);

            return new Tuple<int, object>(resultBytes.Length, sourceClass);
        }

        /// <summary>
        /// ��ĳ����ַ��ʼ����ȡc#�����������������ֽڣ�������������ֵ����Ϊ��PLC��ȡ��ֵ��
        /// ��ֻ��ȡ���ԣ����û��ָ��{get;set;}���������ȡ˽�б����򹫹�������ʵ�������Ͷ���������ͣ�������Ҫһ��Ĭ�Ϲ��캯���� 
        /// </summary>
        /// <typeparam name="T">����ʵ�������ࡣ��Ҫһ��Ĭ�Ϲ��캯��</typeparam>
        /// <param name="db">DB/DB1</param>
        /// <param name="startByteAdr">��ʼ�ֽڵĵ�ַ����������DB1��DBW200������200.</param>
        /// <returns></returns>
        public async Task<T> ReadClassAsync<T>(int db, int startByteAdr = 0) where T : class
        {
            return await ReadClassAsync(() => Activator.CreateInstance<T>(), db, startByteAdr);
        }

        /// <summary>
        /// ��ĳ����ַ��ʼ����ȡc#�����������������ֽڣ�������������ֵ����Ϊ��PLC��ȡ��ֵ��
        /// ��ֻ��ȡ���ԣ����û��ָ��{get;set;}���������ȡ˽�б����򹫹�����.
        /// </summary>
        /// <typeparam name="T">����ʵ��������</typeparam>
        /// <param name="classFactory">����ʵ��������</param>
        /// <param name="db">DB/DB1</param>
        /// <param name="startByteAdr">��ʼ�ֽڵĵ�ַ����������DB1��DBW200������200.</param>
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
        /// ��һ�������ж�ȡ���������
        /// �����봴��������һ���������б�����������þ���ֵ����ͬ�б����Ӧ��
        /// ֵ�洢������������ԡ�Value���У������Ѿ�������ת��
        /// ����������ת����ֻ�贴��һ���ֽ������
        /// ������ܳ���20(Э������)���ֽڲ��ܳ���ͷ��200 + 22(Э������).
        /// </summary>
        /// <param name="dataItems">���������ȡ�ı����б���������б�������20��������.</param>
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
        /// ��ָ����������ʼ�����ݿ���д���ֽ����������Դ�����200���ֽڵĶ������
        /// ���д�벻�ɹ�������LastErrorCode��LastErrorString��
        /// </summary>
        /// <param name="dataType">�������͵��ڴ����򣬿�����DB����ʱ������������Merker(�ڴ�)�����룬���.</param>
        /// <param name="db">�ڴ�����ĵ�ַ(��������DB1�����������Ϊ1)�����Ҳ����Ϊ�����ڴ�������������:����������ʱ���ȵ�.</param>
        /// <param name="startByteAdr">��ʼ�ֽڵĵ�ַ����������DB1��DBW200������200.</param>
        /// <param name="value">�ֽ�д����������ĳ��Ȳ��ܳ���200�������Ҫ���࣬����ʹ�õݹ�.</param>
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
        /// ��ָ����������DBд��һ��λ��
        /// </summary>
        /// <param name="dataType">�������͵��ڴ����򣬿�����DB����ʱ������������Merker(�ڴ�)�����룬���.</param>
        /// <param name="db">�ڴ�����ĵ�ַ(��������DB1�����������Ϊ1)�����Ҳ����Ϊ�����ڴ�������������:����������ʱ���ȵ�.</param>
        /// <param name="startByteAdr">��ʼ�ֽڵĵ�ַ����������DB1��DBW200������200.</param>
        /// <param name="bitAdr">λ�ĵ�ַ. (0-7)</param>
        /// <param name="value">�ֽ�д����������ĳ��Ȳ��ܳ���200�������Ҫ���࣬����ʹ�õݹ�.</param>
        /// <returns></returns>
        public async Task WriteBitAsync(DataType dataType, int db, int startByteAdr, int bitAdr, bool value)
        {
            if (bitAdr < 0 || bitAdr > 7)
                throw new InvalidAddressException(string.Format("Ѱַ����:��ֻ������0-7λ��λ�á���ַ{0}��Ч", bitAdr));

            await WriteBitWithASingleRequestAsync(dataType, db, startByteAdr, bitAdr, value);
        }

        /// <summary>
        /// ��ָ�������������ݿ�д��һ��λ��
        /// </summary>
        /// <param name="dataType">�������͵��ڴ����򣬿�����DB����ʱ������������Merker(�ڴ�)�����룬���.</param>
        /// <param name="db">�ڴ�����ĵ�ַ(��������DB1�����������Ϊ1)�����Ҳ����Ϊ�����ڴ�������������:����������ʱ���ȵ�.</param>
        /// <param name="startByteAdr">��ʼ�ֽڵĵ�ַ����������DB1��DBW200������200.</param>
        /// <param name="bitAdr">λ�ĵ�ַ. (0-7)</param>
        /// <param name="value">�ֽ�д����������ĳ��Ȳ��ܳ���200�������Ҫ���࣬����ʹ�õݹ�.</param>
        /// <returns></returns>
        public async Task WriteBitAsync(DataType dataType, int db, int startByteAdr, int bitAdr, int value)
        {
            if (value < 0 || value > 1)
                throw new ArgumentException("Value must be 0 or 1", nameof(value));

            await WriteBitAsync(dataType, db, startByteAdr, bitAdr, value == 1);
        }

        /// <summary>
        /// ����һ����������룬�����Խ������Ϊһ��ֵ���顣���������д�������ͬ���͵����ݡ�
        /// ����ָ���ڴ��������͡��ڴ��ַ���ֽ���ʼ��ַ���ֽڼ�����
        /// �����ȡ���ɹ�������LastErrorCode��LastErrorString��
        /// </summary>
        /// <param name="dataType">�������͵��ڴ����򣬿�����DB����ʱ������������Merker(�ڴ�)�����룬���.</param>
        /// <param name="db">�ڴ�����ĵ�ַ(��������DB1�����������Ϊ1)�����Ҳ����Ϊ�����ڴ�������������:����������ʱ���ȵ�.</param>
        /// <param name="startByteAdr">��ʼ�ֽڵĵ�ַ����������DB1��DBW200������200.</param>
        /// <param name="value">�ֽ�д����������ĳ��Ȳ��ܳ���200�������Ҫ���࣬����ʹ�õݹ�.</param>
        /// <param name="bitAdr">λ�ĵ�ַ. (0-7)</param>
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
                                "Ѱַ����:��ֻ������0-7λ��λ�á���ַ{0}��Ч",
                                bitAdr), nameof(bitAdr));

                    await WriteBitAsync(dataType, db, startByteAdr, bitAdr, intValue == 1);
                }
                else throw new ArgumentException("ֵ������bool��int����д��λ", nameof(value));
            }
            else await WriteBytesAsync(dataType, db, startByteAdr, Serialization.SerializeValue(value));
        }

        /// <summary>
        /// ��PLCд��һ�����������������ַ������硰DB1.DBX0.0������DB20����DBD200"�� "MB20"�� "T45"�ȡ�
        /// </summary>
        /// <param name="variable">�����ַ�������"DB1.DBX0.0"�� "DB20��DBD200"�� "MB20"�� "T45"��.</param>
        /// <param name="value">Ҫд��PLC��ֵ</param>
        /// <returns>��ʾ�첽д����������.</returns>
        public async Task WriteAsync(string variable, object value)
        {
            var adr = new PLCAddress(variable);
            await WriteAsync(adr.DataType, adr.DbNumber, adr.StartByte, value, adr.BitNumber);
        }

        /// <summary>
        /// ��PLC�����ݿ���дһ��c#�ṹ��
        /// </summary>
        /// <param name="structValue">Ҫд��ָ������</param>
        /// <param name="db">Db ��ַ</param>
        /// <param name="startByteAdr">��PLC�������ֽ�</param>
        /// <returns>��ʾ�첽д����������.</returns>
        public async Task WriteStructAsync(object structValue, int db, int startByteAdr = 0)
        {
            var bytes = Struct.ToBytes(structValue).ToList();
            await WriteBytesAsync(DataType.DataBlock, db, startByteAdr, bytes.ToArray());
        }

        /// <summary>
        /// ��PLC�����ݿ��б�дһ��c#��
        /// </summary>
        /// <param name="classValue">Ҫд��ָ������</param>
        /// <param name="db">Db ��ַ</param>
        /// <param name="startByteAdr">��PLC�������ֽ�</param>
        /// <returns>��ʾ�첽д����������.</returns>
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
        /// ��PLCд������������Ӧ��Ч�����׳��쳣
        /// ��PLC������д��Ŀ�Ĵ���ʱ��
        /// </summary>
        /// <param name="dataItems">Ҫд�뵽PLC��������</param>
        /// <returns>����PLC����Ӧ������ʱ��ɵ�����.</returns>
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
        /// ��PLCд�����200���ֽڡ�����ָ���ڴ��������͡��ڴ��ַ���ֽ���ʼ��ַ���ֽڼ�����
        /// </summary>
        /// <param name="dataType">�������͵��ڴ����򣬿�����DB����ʱ������������Merker(�ڴ�)�����룬���.</param>
        /// <param name="db">�ڴ�����ĵ�ַ(��������DB1�����������Ϊ1)�����Ҳ����Ϊ�����ڴ�������������:����������ʱ���ȵ�.</param>
        /// <param name="startByteAdr">��ʼ�ֽڵĵ�ַ����������DB1��DBW200������200.</param>
        /// <param name="value">�ֽ�д����������ĳ��Ȳ��ܳ���200�������Ҫ���࣬����ʹ�õݹ�.</param>
        /// <returns>��ʾ�첽д����������</returns>
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
