using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.ApplicationManage
{
    [Table("mk_led_device")]
    public class LedDeviceEntity : BaseExtensionEntity
    {
        /// <summary>
        /// led设备唯一ID号
        /// </summary>
        public string DeviceUID { get; set; }
        /// <summary>
        /// 设别名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 设备IP地址
        /// </summary>
        public string Ip_Address { get; set; }
    }
}
