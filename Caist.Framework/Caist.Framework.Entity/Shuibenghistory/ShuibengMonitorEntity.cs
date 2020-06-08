using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caist.Framework.Entity.Shuibenghistory
{
    [Serializable]
    [Table("mk_plc_shuibeng_values")]
    public class ShuibengMonitorEntity : BaseEntity
    {
        public string dictId { get; set; }
        public string sysId { get; set; }
        public string dictValue { get; set; }
        public DateTime createTime { get; set; }
    }
}
