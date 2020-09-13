using System;

namespace Caist.Framework.Entity
{
    [Serializable]
    public class PepolePostionEntity
    {
        [DBColumn(ColName = "Current_Station")]
        public string CurrentStation { get; set; }
        [DBColumn(ColName = "Station_Address")]
        public string StationAddress { get; set; }
        [DBColumn(ColName = "Nums")]
        public string Nums { get; set; }
    }
}
