using System;
using System.Collections.Generic;

namespace Caist.Framework.Entity
{
    [Serializable]
    public class MqtSettingEntity
    {

        /// <summary>
        /// 应用模块系统ID
        /// </summary>
        [DBColumn(ColName = "system_id")]
        public long SystemId { get; set; }

        /// <summary>
        /// 数据类型 1：数据 2：报警
        /// </summary>
        [DBColumn(ColName = "code_type")]
        public int CodeType { get; set; }

        /// <summary>
        /// plc点表名称
        /// </summary>
        [DBColumn(ColName = "code_name")]
        public string CodeName { get; set; }
        /// <summary>
        /// 配置的plc点表地址
        /// </summary>
        [DBColumn(ColName = "code_point_setting")]
        public string CodePointSetting { get; set; }
        /// <summary>
        /// 子系统编码
        /// </summary>
        [DBColumn(ColName = "system_code")]
        public string SystemCode { get; set; }
        /// <summary>
        /// 地址编码-采区编码
        /// </summary>
        [DBColumn(ColName = "address_are_code")]
        public string AddressAreCode { get; set; }
        /// <summary>
        /// 地址编码-类型编码
        /// </summary>
        [DBColumn(ColName = "address_type_code")]
        public string AddressTypeCode { get; set; }
        /// <summary>
        /// 地址编码-设备安装点
        /// </summary>
        [DBColumn(ColName = "address_device_code")]
        public string AddressDeviceCode { get; set; }
        /// <summary>
        /// 设备顺序编码
        /// </summary>
        [DBColumn(ColName = "device_code")]
        public string DeviceCode { get; set; }
        /// <summary>
        /// sensor_type_code
        /// </summary>
        [DBColumn(ColName = "sensor_type_code")]
        public string SensorTypeCode { get; set; }

        /// <summary>
        /// 上传数据总长度
        /// </summary>
        [DBColumn(ColName = "value_length")]
        public int ValueLength { get; set; }

        /// <summary>
        /// 上传数据小数位数
        /// </summary>
        [DBColumn(ColName = "decimal_places")]
        public int DecimalPlaces { get; set; }

        /// <summary>
        /// 报警类型关联点位，以逗号分隔
        /// </summary>
        [DBColumn(ColName = "alarm_point")]
        public string AlarmPoint { get; set; }
        /// <summary>
        /// 上传数据的数据表名称
        /// </summary>
        [DBColumn(ColName ="tab_name")]
        public string TabName { get; set; }

    }

    [Serializable]
    public class MqtPlcDataEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "dict_id")]
        public string DictId { get; set; }
        [DBColumn(ColName = "sys_Id")]
        public string SysId { get; set; }

        [DBColumn(ColName = "dict_Value")]
        public string DictValue { get; set; }
    }



    public class MqttDataList
    {
        public string tableName { get; set; }
        public List<PointData> Data { get; set; }

    }

    public class PointData
    {

        public string SensorCode { get; set; }
        public string Point { get; set; }
        public float Value { get; set; }

    }
}
