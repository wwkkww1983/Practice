using System;

namespace Caist.Framework.Entity.AlarmManage
{
    public class AlarmRecordEntity : BaseExtensionEntity
    {
        public long? AlarmId { get; set; }
        public DateTime? AlarmTime { get; set; }
        public string AlarmTimeLength { get; set; }
        public string AlarmReason { get; set; }
        public long? ViewManipulateId { get; set; }
        public string ManipulateModelName { get; set; }
        public string BroadcastContent { get; set; }
        public int? BroadcastCount { get; set; }
        public string SystemId { get; set; }
        public string SystemName { get; set; }
        /// <summary>
        /// 点位,根据点位取值
        /// </summary>
        public string ManipulateModelMark { get; set; }
        /// <summary>
        /// 视图名称
        /// </summary>
        public string ViewName { get; set; }
        /// <summary>
        /// 报警值
        /// </summary>
        public string AlarmValue { get; set; }
    }
}
