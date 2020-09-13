using System;

namespace Caist.Framework.Entity
{
    [Serializable]
    public class PepolePositionEntity
    {
        [DBColumn(ColName = "CurrentStation")]
        public string CurrentStation { get; set; }
        [DBColumn(ColName = "StationAddress")]
        public string StationAddress { get; set; }
        [DBColumn(ColName = "PepoleNumber")]
        public string PepoleNumber { get; set; }
        [DBColumn(ColName = "PepoleName")]
        public string PepoleName { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        [DBColumn(ColName = "TypeOfWorkName")]
        public string TypeOfWorkName { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        [DBColumn(ColName = "Duty")]
        public string Duty { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [DBColumn(ColName = "Post")]
        public string Post { get; set; }
        /// <summary>
        /// 下井时间
        /// </summary>
        [DBColumn(ColName = "InMineTime")]
        public string InMineTime { get; set; }
    }
}
