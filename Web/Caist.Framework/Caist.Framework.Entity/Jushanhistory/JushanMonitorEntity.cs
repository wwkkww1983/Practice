using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caist.Framework.Entity.Jushanhistory
{
    [Serializable]
    [Table("mk_plc_jushan_values")]
    public class JushanMonitorEntity : BaseEntity
    
    {
        public string dictId { get; set; }
        public string sysId { get; set; }
        public string dictValue { get; set; }
        public DateTime createTime { get; set; }
    }
}
