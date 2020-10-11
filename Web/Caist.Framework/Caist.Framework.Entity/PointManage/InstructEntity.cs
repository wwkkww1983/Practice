using Caist.Framework.Util;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Caist.Framework.Entity.PointManage
{
    [Table("mk_instruct")]
    public class InstructEntity : BaseExtensionEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string DataType { get; set; }
        public string Output { get; set; }
        public string Remark { get; set; }

        [JsonConverter(typeof(StringJsonConverter))]
        public long? InstructGroupId { get; set; }
        public string InstructGroupName { get; set; }

        public int InstructType { get; set; }
        //public string GroupName { get; set; }
        [NotMapped]
        public string DeviceHost { get; set; }

    }

    public class InstructReturnEntity : BaseExtensionEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string DataType { get; set; }
        public string Output { get; set; }
        public string Remark { get; set; }

        [JsonConverter(typeof(StringJsonConverter))]
        public long? InstructGroupId { get; set; }
        public string InstructGroupName { get; set; }

        public string GroupName { get; set; }

        public string DeviceHost { get; set; }

        public int InstructType { get; set; }
    }
}
