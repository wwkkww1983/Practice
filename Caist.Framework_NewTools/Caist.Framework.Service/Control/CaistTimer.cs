using Caist.Framework.Entity.Enum;
using Caist.Framework.PLC.Siemens;

namespace Caist.Framework.Service.Control
{
    public class CaistTimer:System.Timers.Timer
    {
        public string State { get; set; }
        public string ClientFlag { get; set; }
        public SiemensHelper obj { get; set; }
    }
    public class DataBaseTimer : CaistTimer
    {
        public RequestType Tag { get; set; }
    }
}
