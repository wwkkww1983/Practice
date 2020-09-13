using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.Framework.Model.yafenghistory
{
   public class ExportParam
    {
        public long SystemId { get; set; }
        public string AreaName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
