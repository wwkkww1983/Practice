using System.ComponentModel;

namespace Caist.Framework.Enum.PointManage
{
    public enum InstructEnum
    {
        [Description("SHORT")]
        SHORT = 0,
        [Description("UINT32")]
        UINT32 = 1,
        [Description("USHORT")]
        USHORT = 2,
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

}
