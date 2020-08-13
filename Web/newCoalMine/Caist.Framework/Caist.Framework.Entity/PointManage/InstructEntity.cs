using System.ComponentModel.DataAnnotations.Schema;

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
        public long? InstructGroupId { get; set; }
        public string InstructGroupName { get; set; }

    }
}
