using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.PointManage
{
    [Table("mk_device")]
    public class DeviceEntity : BaseExtensionEntity
    {
        public string DeviceName { get; set; }
        public string DeviceHost { get; set; }
        public string DevicePort { get; set; }
        public string PLCType { get; set; }
        public string SlotNo { get; set; }
        public string Local { get; set; }
        public string Remote { get; set; }
        public string ParentId { get; set; }
    }
}
