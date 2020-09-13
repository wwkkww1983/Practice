using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.ApplicationManage
{
    [Table("mk_mqtt_address_type")]
    public class MqtAddressTypeEntity : BaseEntity
    {
        /// <summary>
        /// 子系统编码  
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址编码-类型编码
        /// </summary>
        public string Code { get; set; }
      

    }

}
