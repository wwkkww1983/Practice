using System;

namespace Caist.Framework.Entity
{
    [Serializable]
    public class FiberEntity
    {
        [DBColumn(ColName = "id")]
        public string Id { get; set; }
        [DBColumn(ColName = "AreaName")]
        public string AreaName { get; set; }
        [DBColumn(ColName = "MaxValue")]
        public string MaxValue { get; set; }
        [DBColumn(ColName = "MinValue")]
        public string MinValue { get; set; }
        [DBColumn(ColName = "AverageValue")]
        public string AverageValue { get; set; }
        [DBColumn(ColName = "CreateTime")]
        public string CreateTime { get; set; }
    }
}
