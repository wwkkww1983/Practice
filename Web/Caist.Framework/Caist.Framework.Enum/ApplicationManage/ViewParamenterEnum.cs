using System.ComponentModel;

namespace Caist.Framework.Enum.ApplicationManage
{
    public enum ViewParamenterEnum
    {
        [Description("单控")]
        Single = 1,

        [Description("双控")]
        Both = 2
    }
    public enum InstructEnum1
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
}
