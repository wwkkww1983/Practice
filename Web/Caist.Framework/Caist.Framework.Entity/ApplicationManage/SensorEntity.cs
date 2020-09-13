using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.ApplicationManage
{
    [Table("mk_mqtt_sensor_setting")]
    public class SensorEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 系统编码
        /// </summary>
        public string SystemCode { get; set; }

        public int CodeType { get; set; }
        /// <summary>
        /// 传感器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 传感器编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 最大长度
        /// </summary>
        public int ValueLength { get; set; }
        /// <summary>
        /// 小数位数
        /// </summary>
        public int DecimalPlaces { get; set; }

    }
}
