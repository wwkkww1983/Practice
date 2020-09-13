using System;
using System.Collections.Generic;

namespace Caist.Framework.Entity
{
    [Serializable]
    public class PLCEntity
    {
        [DBColumn(ColName = "Id")]
        public string Id { get; set; }
        [DBColumn(ColName = "area_name")]
        public string AreaName { get; set; }
        [DBColumn(ColName = "max_value")]
        public string MaxValue { get; set; }
        [DBColumn(ColName = "max_value_pos")]
        public string MaxValuePos { get; set; }
        [DBColumn(ColName = "min_value")]
        public string MinValue { get; set; }
        [DBColumn(ColName = "min_value_pos")]
        public string MinValuePos { get; set; }
        [DBColumn(ColName = "average_value")]
        public string AverageValue { get; set; }
    }

    public class PlcConnects
    {
        public List<PlcConnect> PlcConnectStatus { get; set; }
    }
    public class PlcConnect
    {
        public string SysId { get; set; }
        public string PlcName { get; set; }
        /// <summary>
        /// plc状态：0：关闭；1：开启；
        /// </summary>
        public int PlcStatus { get; set; }
    }
}
