using System.ComponentModel;

namespace Caist.Framework.Entity.Enum
{
    public enum InstructEnum
    {
        [Description("BOOL")]
        BOOL = 0,
        [Description("BYTE")]
        BYTE = 1,
        [Description("SHORT")]
        SHORT = 2,
        [Description("INT")]
        INT = 3,
        [Description("FLOAT")]
        FLOAT = 4
    }

    public enum InstructParamEnum
    {
        [Description("写")]
        OUT = 0,
        [Description("读")]
        PUT = 1,
    }

    public enum MkSystemCode
    {
        [Description("安全监测监控系统")]
        AnQuan = 1,
        [Description("人员定位系统")]
        RenYuan = 2,
        [Description("运输系统")]
        YunShu = 3,
        [Description("排水系统")]
        PaiShui = 4,
        [Description("通风系统")]
        TongFeng = 5,
        [Description("瓦斯抽采系统")]
        WaSi = 6,
        [Description("压风系统")]
        YaFeng = 7,
        [Description("供配电系统")]
        GongPeiDian = 8,
        [Description("工业视频子系统")]
        video = 9,
    }
    public enum RequestType
    {
        [Description("人员定位")]
        PepolePosition = 1,
        [Description("供配电")]
        SubStation = 2,
        [Description("光纤测温")]
        Fiber = 3,
        [Description("获取plc值")]
        GetPlcValues = 4,
        [Description("获取当前系统所有开关的状态")]
        GetCommandValues = 5,
        [Description("获取单个开关状态值")]
        GetCommandValue = 6,
        [Description("设置单个开关状态")]
        SetCommandValue = 7,
        [Description("获取开关状态集合")]
        GetCommandsStatus = 8
    }

    public enum InstructViewEnum
    {
        [Description("数据")]
        Data = 0,
        [Description("控制")]
        Govern = 1,
        [Description("告警")]
        Alarm = 2,
        [Description("启动")]
        Start = 3,
        [Description("停止")]
        Stop = 4,
        [Description("参数设置")]
        Parameter = 9
    }
}
