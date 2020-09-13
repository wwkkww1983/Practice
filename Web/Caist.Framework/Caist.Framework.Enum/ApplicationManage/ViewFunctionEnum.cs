using System.ComponentModel;

namespace Caist.Framework.Enum.ApplicationManage
{
    public enum ViewFunctionEnum
    {
        [Description("界面视图")]
        Page = 1,

        [Description("界面按钮")]
        Button = 2
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
        Stop = 4
    }

    public enum AlarmClassEnum
    {
        [Description("一级")]
        ClassA = 0,
        [Description("二级")]
        ClassB = 1,
        [Description("三级")]
        ClassC = 2,
    }
}
