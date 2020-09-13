using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caist.Framework.Entity.Choucahistory
{
    [Serializable]
    [Table("mk_plc_choucai_values")]
    public class ChoucaiMonitorEntity : BaseEntity
    {
        public string dictId { get; set; }
        public string sysId { get; set; }
        public string dictValue { get; set; }
        public DateTime createTime { get; set; }
    }
}
