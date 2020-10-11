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
        [DBColumn(ColName = "dict_Id")]
        public string DictId { get; set; }
        [DBColumn(ColName = "dict_value")]
        public string DictValue { get; set; }
        [DBColumn(ColName = "instruct_type")]
        public string InstructType { get; set; }
    }
}
