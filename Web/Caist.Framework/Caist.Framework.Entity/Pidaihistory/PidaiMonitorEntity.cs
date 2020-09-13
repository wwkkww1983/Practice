using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caist.Framework.Entity.Pidaihistory
{
    [Serializable]
    [Table("mk_plc_pidai_values")]
    public class PidaiMonitorEntity : BaseEntity
    {
        public string dictId { get; set; }
        public string sysId { get; set; }
        public string dictValue { get; set; }
        public DateTime? createTime { get; set; }
    }
}
