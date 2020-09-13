using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caist.Framework.Entity.Cewenhistory
{
    [Serializable]
    public class CewenMonitorEntity : BaseEntity
    {
        public string areaName { get; set; }
        public string maxValue { get; set; }
        public string minValue { get; set; }
        public string averageValue { get; set; }
        public DateTime createTime { get; set; }
    }
}
