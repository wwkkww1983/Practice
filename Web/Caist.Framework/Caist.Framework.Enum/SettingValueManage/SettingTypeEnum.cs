using System.ComponentModel;

namespace Caist.Framework.Enum.SettingValueManage
{
    public enum SettingPlcTypeEnum
    {
        [Description("S7200")]
        S7200 = 1,

        [Description("S7300")]
        S7300 = 2,

        [Description("S71200")]
        S71200 = 3,

        [Description("S71500")]
        S71500 = 4
    }

    public enum SettingParamenterType
    {
        [Description("单参数")]
        Single = 1,

        [Description("双参数")]
        Pair = 2
    }
}
