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
        public string SystemId { get; set; }
        public string SystemName { get; set; }
    }
}
