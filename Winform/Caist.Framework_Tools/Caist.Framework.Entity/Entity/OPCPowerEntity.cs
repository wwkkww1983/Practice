using System;

namespace Caist.Framework.Entity
{
    [Serializable]
    public class OpcPowerEntity
    {
        [DBColumn(ColName = "Num")]
        public string Num { get; set; }
        [DBColumn(ColName = "Name")]
        public string Name { get; set; }
        [DBColumn(ColName = "Area")]
        public string Area { get; set; }
        [DBColumn(ColName = "Address")]
        public int Address { get; set; }
        [DBColumn(ColName = "Data_Type")]
        public string DataType { get; set; }
        [DBColumn(ColName = "IP")]
        public string IP { get; set; }
        [DBColumn(ColName = "Port")]
        public string Port { get; set; }
    }
}
