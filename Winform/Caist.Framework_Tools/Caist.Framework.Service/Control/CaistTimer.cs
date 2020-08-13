using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.Control
{
    public class CaistTimer:System.Timers.Timer
    {
        public string State { get; set; }
        public object obj { get; set; }
    }

}
