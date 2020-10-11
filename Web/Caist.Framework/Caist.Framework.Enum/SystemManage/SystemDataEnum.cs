using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Enum.SystemManage
{
    public enum CycleEnum
    {
        [Description("日数据")]
        Day = 1,

        [Description("周数据")]
        Week = 2,

        [Description("月数据")]
        Month = 3,
        [Description("季数据")]
        Season = 4,
        [Description("年数据")]
        Year = 5
    }

    public enum SensorEnum
    {
        [Description("数据类型")]
        Data = 1,

        [Description("报警类型")]
        Cotrl = 2,
        [Description("运行总控")]
        Status = 3,
        [Description("视频数据")]
        Video = 4,

    }
}
