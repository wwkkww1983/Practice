using Caist.Framework.Entity.Entity;
using System;
using System.Collections.Generic;

namespace Caist.Framework.Entity
{
    [Serializable]
    public class DeviceEntity
    {
        [DBColumn(ColName = "Id")]
        public string Id { get; set; }
        [DBColumn(ColName = "Name")]
        public string Name { get; set; }
        [DBColumn(ColName = "Host")]
        public string Host { get; set; }
        [DBColumn(ColName = "Port")]
        public string Port { get; set; }
        [DBColumn(ColName = "CPU_SlotNO")]
        public string CPU_SlotNO { get; set; }
        [DBColumn(ColName = "PLCType")]
        public string PLCType { get; set; }
        [DBColumn(ColName = "LocalTASP")]
        public string LocalTASP { get; set; }
        [DBColumn(ColName = "RemoteTASP")]
        public string RemoteTASP { get; set; }
        [DBColumn(ColName = "ParentId")]
        public string ParentId { get; set; }
        [DBColumn(ColName = "tab_name")]
        public string TabName { get; set; }
        [DBColumn(ColName = "system_id")]
        public string SystemId { get; set; }
        public List<TagGroupsEntity> TagGroupsEntities { get; set; }
    }
}
