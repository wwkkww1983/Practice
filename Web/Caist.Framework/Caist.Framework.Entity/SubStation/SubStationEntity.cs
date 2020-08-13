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
        public string DianWei { get; set; }
        public string F { get; set; }
        public string IA { get; set; }
        public string P { get; set; }
        public string Q { get; set; }
        public string COS { get; set; }
    }
}
