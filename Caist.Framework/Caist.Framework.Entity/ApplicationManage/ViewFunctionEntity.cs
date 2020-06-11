using Caist.Framework.Util;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.ApplicationManage
{
    [Table("mk_view_function")]
    public class ViewFunctionEntity : BaseExtensionEntity
    {
        [JsonConverter(typeof(StringJsonConverter))]
        public long? SystemSettingId { get; set; }
        public string ViewName { get; set; }
        public int? ViewSort { get; set; }
        public int? ViewType { get; set; }
        public string ViewButtonEvent { get; set; }
        public string ViewButtonId { get; set; }
        public string ViewButtonAlt { get; set; }
        public string ViewButtonUrl { get; set; }
        public string ViewButtonImage { get; set; }
        public int? ViewStatus { get; set; }

        [NotMapped]
        public string SystemSettingName { get; set; }
    }
}
