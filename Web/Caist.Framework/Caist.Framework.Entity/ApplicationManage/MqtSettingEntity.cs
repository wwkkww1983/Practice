using Caist.Framework.Util;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.ApplicationManage
{
    [Table("mk_mqtt_code_setting")]
    public class MqtSettingEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 应用模块系统ID
        /// </summary>
        [JsonConverter(typeof(StringJsonConverter))]
        public long SystemId { get; set;}

        /// <summary>
        /// 应用模块名称
        /// </summary>
        [NotMapped]
        public string SystemName { get; set; }
        /// <summary>
        /// 数据类型 1：数据 2：报警
        /// </summary>
        public int CodeType { get; set; }
        /// <summary>
        /// plc点表名称
        /// </summary>
        public string CodeName { get; set; }
        /// <summary>
        /// 配置的plc点表地址
        /// </summary>
        public string CodePointSetting { get; set; }
        /// <summary>
        /// 子系统编码
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// 地址编码-采区编码
        /// </summary>
        public string AddressAreCode { get; set; }
        /// <summary>
        /// 地址编码-类型编码
        /// </summary>
        public string AddressTypeCode { get; set; }
        /// <summary>
        /// 地址类型名称
        /// </summary>
        [NotMapped]
        public string AddressTypeName { get; set; }
        /// <summary>
        /// 地址编码-设备安装点
        /// </summary>
        public string AddressDeviceCode { get; set; }
        /// <summary>
        /// 设备顺序编码
        /// </summary>
        public string DeviceCode { get; set; }
        /// <summary>
        /// 传感器类型编码
        /// </summary>
        public string SensorTypeCode { get; set; }

        /// <summary>
        /// 值总占用字符长度
        /// </summary>
        public int ValueLength { get; set; }
        /// <summary>
        /// 值得小数位数
        /// </summary>
        public int DecimalPlaces { get; set; }

        /// <summary>
        /// 传感器ID
        /// </summary>
        [JsonConverter(typeof(StringJsonConverter))]
        public long SensorId { get; set; }

        /// <summary>
        /// 报警类型关联点位，以逗号分隔
        /// </summary>
        public string AlarmPoint { get; set; }



    }

}
