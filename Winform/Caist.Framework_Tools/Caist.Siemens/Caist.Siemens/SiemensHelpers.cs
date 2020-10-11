using Caist.Framework.Entity;
using Caist.Framework.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caist.Siemens
{
    public class SiemensHelpers
    {
        private readonly int Fuck_int;
        public string IP { get; set; }
        public string Port { get; set; }
        public DeviceEntity DeviceEntity { get; set; }
        public List<string> ListInstructs { get; set; }
        private S7.Net.Plc SiemensPlc;
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>

        public SiemensHelpers() : base()
        {
            this.Fuck_int = 2000;//点位限制
        }
        public bool Init()
        {
            List<TagGroupsEntity> listGroups = new List<TagGroupsEntity>();
            List<string> instructs = new List<string>();
            int num = 0;
            PublicEntity.TagGroupsEntities.Where(p => p.DeviceId == this.DeviceEntity.Id).ToList().ForEach(v =>
            {
                List<TagEntity> listTags = new List<TagEntity>();
                PublicEntity.TagEntities.Where(s => s.TagGroup == v.Id).ToList().ForEach(x =>
                {
                    if (v.Name.IndexOf("V") == -1 && x.Name.IndexOf("V") == -1)
                    {
                        if (v.Name.StartsWith("DB"))
                        {
                            instructs.Add($"{this.IP}-{v.Name}.{x.Name}:{x.DataType}|{x.InstructType}");
                        }
                        else
                        {//其它不需要中间的点连接
                            instructs.Add($"{this.IP}-{v.Name}{x.Name}:{x.DataType}|{x.InstructType}");
                        }
                        listTags.Add(x);
                        num++;
                    }
                });
                v.TagEntities = listTags;
                listGroups.Add(v);
            });
            ListInstructs = instructs;
            DeviceEntity.TagGroupsEntities = listGroups;
            if (num > this.Fuck_int)
            {
                MessageBox.Show("点位超出2000限制！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 关闭S7服务进程
        /// </summary>
        private void ShutDownServiceS7oiehsx64()
        {
            ServiceController[] services = ServiceController.GetServices();
            var service = services.FirstOrDefault(s => s.ServiceName == "s7oiehsx64");
            if (service != null)
            {
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                }
            }
        }

        /// <summary>
        /// PLC 连接
        /// </summary>
        /// <param name="plcType">PLC类型</param>
        /// <param name="ipAddress">IP 地址</param>
        /// <param name="port">端口号：通常默认为：102</param>
        /// <param name="rack">PLC的机架号：通常为0</param>
        /// <param name="slot">PLC的CPU卡槽</param>
        public bool Open(string plcType, string ipAddress, short port, short rack, short slot)
        {
            try
            {
                ShutDownServiceS7oiehsx64();
                //if (!IsConnected())
                //{
                S7.Net.CpuType cpu = (S7.Net.CpuType)System.Enum.Parse(typeof(S7.Net.CpuType), plcType);
                SiemensPlc = new S7.Net.Plc(cpu, ipAddress, port, rack, slot);
                SiemensPlc.Open();
                //}
                return SiemensPlc.IsConnected;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// PLC 连接
        /// </summary>
        /// <param name="plcType">PLC类型</param>
        /// <param name="ipAddress">IP 地址</param>
        /// <param name="port">端口号：通常默认为：102</param>
        /// <param name="rack">PLC的机架号：通常为0</param>
        /// <param name="slot">PLC的CPU卡槽</param>
        public async Task<bool> OpenAsync(string plcType, string ipAddress, short port, short rack, short slot)
        {
            try
            {
                ShutDownServiceS7oiehsx64();
                S7.Net.CpuType cpu = (S7.Net.CpuType)System.Enum.Parse(typeof(S7.Net.CpuType), plcType);
                SiemensPlc = new S7.Net.Plc(cpu, ipAddress, port, rack, slot);
                await SiemensPlc.OpenAsync();
                return SiemensPlc.IsConnected;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// 获取PLC是否连接成功
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return SiemensPlc.IsConnected;
        }

        /// <summary>
        /// PLC 关闭
        /// </summary>
        public void Close()
        {
            if (SiemensPlc.IsConnected)
            {
                SiemensPlc.Close();
            }
        }

        /// <summary>
        /// PLC写入值
        /// </summary>
        /// <param name="variable">指令名称</param>
        /// <param name="value">写入值</param>
        public void Write(string variable, object value)
        {
            if (!SiemensPlc.IsConnected)
            {
                SiemensPlc.Open();
            }
            SiemensPlc.Write(variable, value);
        }

        /// <summary>
        /// PLC批量写入值
        /// </summary>
        /// <param name="dataItems">指令集合</param>
        public void Write(params S7.Net.Types.DataItem[] dataItems)
        {
            if (!SiemensPlc.IsConnected)
            {
                SiemensPlc.Open();
            }
            SiemensPlc.Write(dataItems);
        }

        /// <summary>
        /// 读取PLC指令值
        /// </summary>
        /// <param name="variable">指令名称</param>
        /// <returns></returns>
        readonly object _lock = new object();
        public string Read(string variable)
        {
            string values = string.Empty;
            try
            {
                if (!SiemensPlc.IsConnected)
                {
                    SiemensPlc.Open();
                }
                lock (_lock)
                {
                    object obj = SiemensPlc.Read(variable);
                    if (obj == null)
                        values = "-9999";
                    else
                        values = obj.ToString();
                }
            }
            catch (Exception ex)
            {
                //Common.LogError(ex);
            }
            return values;
        }

        /// <summary>
        /// 异步读取PLC指令值
        /// </summary>
        /// <param name="variable">指令名称</param>
        /// <returns></returns>
        public async Task<string> ReadAsync(string variable)
        {
            string values = string.Empty;
            try
            {
                if (!SiemensPlc.IsConnected)
                {
                    SiemensPlc.Open();
                }
                object obj = await SiemensPlc.ReadAsync(variable);
                if (obj == null)
                    values = "-9999";
                else
                    values = obj.ToString();
            }
            catch (Exception ex)
            {
                //Common.LogError(ex);
            }
            return values;
        }

        /// <summary>
        /// 读取PLC指令值
        /// </summary>
        /// <param name="variable">指令名称</param>
        /// <returns></returns>
        public string Read(string variable, SendDataType varType)
        {
            string values = string.Empty;
            object obj = SiemensPlc.Read(variable);
            if (obj != null)
            {
                switch (varType)
                {
                    case SendDataType.UInt32:
                        values = Conversion.ConvertToUInt(UInt32.Parse(obj.ToString())).ToString();
                        break;
                    case SendDataType.Short:
                        values = Conversion.ConvertToShort(UInt16.Parse(obj.ToString())).ToString();
                        break;
                    case SendDataType.Ushort:
                        values = Conversion.ConvertToUshort(short.Parse(obj.ToString())).ToString();
                        break;
                    case SendDataType.Int:
                        values = Conversion.ConvertToInt(UInt32.Parse(obj.ToString())).ToString();
                        break;
                    case SendDataType.Float:
                        values = Conversion.ConvertToFloat(UInt32.Parse(obj.ToString())).ToString();
                        break;
                }
            }
            else
                values = "-9999";
            return values;
        }

        /// <summary>
        /// 读取PLC指令值
        /// </summary>
        /// <param name="variable">指令名称</param>
        /// <returns></returns>
        public string Read(string variable, S7.Net.VarType varType)
        {
            string values = string.Empty;
            if (!SiemensPlc.IsConnected)
            {
                SiemensPlc.Open();
            }
            object obj = SiemensPlc.Read(variable);
            if (obj == null)
                values = "-9999";
            else
                values = obj.ToString();
            return values;
        }

        /// <summary>
        /// 读取PLC指令值
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
        public async Task<string> Read(S7.Net.DataType dataType, int db, int startByteAdr, S7.Net.VarType varType, int varCount, byte bitAdr = 0)
        {
            string values = string.Empty;
            if (!SiemensPlc.IsConnected)
            {
                SiemensPlc.Open();
            }
            object obj = await SiemensPlc.ReadAsync(dataType, db, startByteAdr, varType, varCount);
            if (obj == null)
                values = "-9999";
            else
                values = ((double)obj).ToString("N2");
            return values;
        }
    }
}
