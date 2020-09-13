using Caist.Framework.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caist.Framework.Entity.PointManage
{
   [Table("mk_opc_power")]
    public class DistributionEntity : BaseExtensionEntity
    {
        public string DeviceName { get; set; }
        public string DeviceHost { get; set; }
        public string DevicePort { get; set; }
        public string PLCType { get; set; }
        public string SlotNo { get; set; }
        public string Local { get; set; }
        public string Remote { get; set; }
        public int TabStatus { get; set; }
    }
}
