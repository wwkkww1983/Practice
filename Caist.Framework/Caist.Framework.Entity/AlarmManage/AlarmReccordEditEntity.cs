using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.AlarmManage
{
    [Table("mk_alarm_record")]
    public class AlarmRecordEditEntity : BaseExtensionEntity
    {
        public long? AlarmId { get; set; }
        public DateTime? AlarmTime { get; set; }
        public string AlarmTimeLength { get; set; }
        public string AlarmReason { get; set; }
    }
}
