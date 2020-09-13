using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.Framework.Model.Param.Jushanhistory
{
   public class JushanMonitorParam
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Frequency { get; set; }
        public string DictId { get; set; }
    }
}
