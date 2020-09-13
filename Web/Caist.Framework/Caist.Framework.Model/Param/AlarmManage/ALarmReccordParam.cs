using System;

namespace Caist.Framework.Model.Param.AlarmManage
{
    public class ALarmReccordListParam
    {
        public string SystemId { get; set; }
        /// <summary>
        /// 报警等级
        /// </summary>
        public int? BroadcastCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
