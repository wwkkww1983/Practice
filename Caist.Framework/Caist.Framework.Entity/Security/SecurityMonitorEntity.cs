using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.Security
{
    [Serializable]
    [Table("security_monitor_ingreal_time_data")]
    public class SecurityMonitorEntity
    {
        public long Id { get; set; }
        public string Point { get; set; }
        public string Address { get; set; }
        public string lc { get; set; }
        public string lx { get; set; }
        public string dw { get; set; }
        public string ssz { get; set; }
        public string StatName { get; set; }
        public string UpdatDateTime { get; set; }
    }
}
