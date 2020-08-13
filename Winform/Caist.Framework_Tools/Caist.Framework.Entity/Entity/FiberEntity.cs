using System;

namespace Caist.Framework.Entity
{
    [Serializable]
    public class FiberEntity
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
        [DBColumn(ColName = "current_temperature")]
        public string CurrentTemperature { get; set; }
    }
}
