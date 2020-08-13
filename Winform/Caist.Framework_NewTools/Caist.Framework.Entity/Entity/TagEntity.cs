namespace Caist.Framework.Entity.Entity
{
    public class TagEntity
    {
        [DBColumn(ColName = "Id")]
        public string Id { get; set; }
        [DBColumn(ColName = "TagGroup")]
        public string TagGroup { get; set; }
        [DBColumn(ColName = "Name")]
        public string Name { get; set; }
        [DBColumn(ColName = "DataType")]
        public string DataType { get; set; }
        [DBColumn(ColName = "Address")]
        public string Address { get; set; }
        [DBColumn(ColName = "Output")]
        public string Output { get; set; }
        [DBColumn(ColName = "Desc")]
        public string Desc { get; set; }
    }
}
