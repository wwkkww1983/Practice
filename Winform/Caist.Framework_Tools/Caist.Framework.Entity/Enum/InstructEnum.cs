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
        [Description("瓦斯利用及瓦斯发电")]
        WLFD = 20,
    }
}
