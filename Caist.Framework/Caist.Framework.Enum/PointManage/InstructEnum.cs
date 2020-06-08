using System.ComponentModel;

namespace Caist.Framework.Enum.PointManage
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
}
