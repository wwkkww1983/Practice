using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.Security
{
    [Serializable]
    [Table("security_monitor_ingreal_time_data_realTime")]
    public class SecurityMonitorEntity
    {
        public int Id { get; set; }
        public string Point { get; set; }
        public string Address { get; set; }
        public string lc { get; set; }
        public string lx { get; set; }
        public string dw { get; set; }
        public string ssz { get; set; }
        public string StatName { get; set; }
    }
}
