using Google.Protobuf.WellKnownTypes;
using NPOI.XSSF.Streaming.Values;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caist.Framework.Entity.Tongfenghistory
{
    [Serializable]
    [Table("mk_plc_tongfeng_values")]
    public class TongfengMonitorEntity : BaseEntity
    {
        public string dictId { get; set; }
        public string sysId { get; set; }
        public string dictValue { get; set; }
        public DateTime? createTime { get; set; }
    }

}
