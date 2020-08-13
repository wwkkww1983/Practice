using Caist.Framework.Util;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.PointManage
{
    [Table("mk_instruct_group")]
    public class InstructGroupEntity : BaseExtensionEntity
    {
        [JsonConverter(typeof(StringJsonConverter))]
        public long? DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Name { get; set; }
        public string ModularType { get; set; }
        public string ReadCount { get; set; }
        public string BeginAddress { get; set; }
        public string BeginBlock { get; set; }
        public string GroupName { get; set; }
    }
}
