using System;

namespace Caist.Framework.Entity
{
    [Serializable]
    public class SubStationEntity
    {
        [DBColumn(ColName = "Id")]
        public string Id { get; set; }
        [DBColumn(ColName = "Sys_Id")]
        public string SysId { get; set; }
        [DBColumn(ColName = "Dian_Wei")]
        public string Dian_Wei { get; set; }
        [DBColumn(ColName = "F")]
        public string F { get; set; }
        [DBColumn(ColName = "IA")]
        public string IA { get; set; }
        [DBColumn(ColName = "P")]
        public string P { get; set; }
        [DBColumn(ColName = "Q")]
        public string Q { get; set; }
        [DBColumn(ColName = "COS")]
        public string COS { get; set; }
    }
}
