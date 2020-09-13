using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.Framework.Model.yafenghistory
{
   public class yafengMonitorParam
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DictId { get; set; }
        //班次
        public string Frequency { get; set; }
    }
}
