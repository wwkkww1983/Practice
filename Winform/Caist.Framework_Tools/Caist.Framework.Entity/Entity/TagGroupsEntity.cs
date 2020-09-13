using System.Collections.Generic;

namespace Caist.Framework.Entity.Entity
{
    public class TagGroupsEntity
    {
        [DBColumn(ColName = "Id")]
        public string Id { get; set; }
        [DBColumn(ColName = "DeviceId")]
        public string DeviceId { get; set; }
        [DBColumn(ColName = "Name")]
        public string Name { get; set; }
        [DBColumn(ColName = "ReadCount")]
        public string ReadCount { get; set; }
        [DBColumn(ColName = "BeginAddress")]
        public string BeginAddress { get; set; }
        [DBColumn(ColName = "BeginBlock")]
        public string BeginBlock { get; set; }
        [DBColumn(ColName = "Block")]
        public string Block { get; set; }
        [DBColumn(ColName = "MMType")]
        public string MMType { get; set; }

        public List<TagEntity> TagEntities { get; set; }
    }
}
