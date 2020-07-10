using Caist.Framework.Entity.Enum;
using System.Collections.Generic;

namespace Caist.Framework.Entity.Entity
{

    #region 返回数据模型
    #region 报警
    public class AlarmModel
    {
        public List<AlarmContent> Alarm { get; set; }
    }

    public class AlarmContent
    {
        public long SysId { get; set; }
        public long ModuleId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public bool Type { get; set; }
    }
    #endregion

    #region 光纤测温
    public class FiberModel
    {
        public List<FiberContent> Fiber { get; set; }
    }

    public class FiberContent
    {
        public string AreaName { get; set; }
        public string MaxValue { get; set; }
        public string MaxValuePos { get; set; }
        public string MinValue { get; set; }
        public string MinValuePos { get; set; }
        public string AverageValue { get; set; }
    }
    #endregion

    #region 配电站
    public class SubStationModel
    {
        public List<SubStationContent> SubStation { get; set; }
    }

    public class SubStationContent
    {
        public string Sys_Id { get; set; }
        public string F { get; set; }
        public string IA { get; set; }
        public string P { get; set; }
        public string Q { get; set; }
        public string COS { get; set; }
    }
    #endregion

    #region 人员定位
    public class PepolePostionModel
    {
        public List<PepolePostionContent> PepolePosition { get; set; }
    }

    public class PepolePostionContent
    {
        public string CurrentStation { get; set; }
        public string StationAddress { get; set; }
        public string Nums { get; set; }
    }
    #endregion
    #endregion

    #region 接收数据模型
    #region 前端命令操作模型
    public class InstructModel
    {
        public InstructInfo RemoteControl { get; set; }
    }

    public class InstructInfo
    {
        public string Instruct { get; set; }
        public string Value { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public string SystemId { get; set; }
        public RequestType RequestType { get; set; }
    }
    #endregion
    #endregion

    #region 接收数据模型
    #region 前端命令操作模型
    public class InstructModelReturns
    {
        public List<InstructReturn> InstructModelReturn { get; set; }
    }

    public class InstructReturn
    {
        public string ControlName { get; set; }
        public string ParamenterUnit { get; set; }
        public string ParamenterInstructStart { get; set; }
        public string ParamenterInstructStart_V { get; set; }
        public string ParamenterInstructEnd { get; set; }
        public string ParamenterInstructEnd_V { get; set; }
        public string ParamenterInstruct { get; set; }
        public string ParamenterInstruct_V { get; set; }
        public string ParamenterName { get; set; }
        public string Id { get; set; }
    }

    #endregion
    #endregion

    #region 地址模型
    public class InstructAddrs
    {
        [DBColumn(ColName = "Device_Name")]
        public string DeviceName { get; set; }
        [DBColumn(ColName = "Device_Host")]
        public string DeviceHost { get; set; }
        [DBColumn(ColName = "Device_Port")]
        public string DevicePort { get; set; }
        [DBColumn(ColName = "addr")]
        public string Addr { get; set; }
    }
    #endregion
}
