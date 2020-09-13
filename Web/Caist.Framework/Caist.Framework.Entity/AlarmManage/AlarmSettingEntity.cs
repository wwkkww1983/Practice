using Caist.Framework.Util;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.AlarmManage
{
    [Table("mk_alarm_settings")]
    public class AlarmSettingEntity : BaseExtensionEntity
    {
        [JsonConverter(typeof(StringJsonConverter))]
        public long? ViewManipulateId { get; set; }
        [NotMapped]
        public string ManipulateModelName { get; set; }
        public string MinValue { get; set; }
        public string MaxValue { get; set; }
        public string BroadcastContent { get; set; }
        public int? BroadcastCount { get; set; }
        public long? SystemModels { get; set; }
    }
}
