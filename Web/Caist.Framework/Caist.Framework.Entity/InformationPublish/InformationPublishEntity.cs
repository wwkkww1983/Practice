using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.ApplicationManage
{
    [Table("mk_information_publish")]
    public class InformationPublishEntity : BaseExtensionEntity
    {
        /// <summary>
        /// led设备唯一ID号
        /// </summary>
        public string DeviceUid { get; set; }
        /// <summary>
        /// 发布内容
        /// </summary>
        public string LinkContent { get; set; }

        [NotMapped]
        public string DeviceName { get; set; }
    }
}
