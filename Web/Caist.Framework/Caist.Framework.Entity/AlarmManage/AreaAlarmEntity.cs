using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.Framework.Entity
{
    public class AreaAlarmEntity: BaseExtensionEntity
    {
        public string SystemName { get; set; }
        public int AlarmNum { get; set; }
        public string State { get; set; }
    }
}
