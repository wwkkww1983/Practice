using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.SubStation
{
    [Serializable]
    [Table("mk_substation")]
    public class SubStationEntity
    {
        public int Id { get; set; }
        public int SysId { get; set; }
        public string DictId { get; set; }

        public string DictValue { get; set; }
        public int InstructType { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
