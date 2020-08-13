using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Models
{
    /// <summary>
    /// 轨迹点表 - Basic_Locus
    /// </summary>
    public class Basic_Locus: BaseEntity
    {
        /// <summary>
        /// 分段ID - SectionId
        /// </summary>
        public string SectionId { get; set; }
        /// <summary>
        /// 物探点号 - GisPoint
        /// </summary>
        public string GisPoint { get; set; }
        /// <summary>
        /// 连接点号 - LinkPoint
        /// </summary>
        public string LinkPoint { get; set; }
        /// <summary>
        /// 点特征 - PointFeature
        /// </summary>
        public string PointFeature { get; set; }
        /// <summary>
        ///  - point_x
        /// </summary>
        public string Point_X { get; set; }
        /// <summary>
        ///  - point_y
        /// </summary>
        public string Point_Y { get; set; }
        /// <summary>
        ///  - point_z
        /// </summary>
        public string Point_Z { get; set; }
        /// <summary>
        ///  - 地面高程
        /// </summary>
        public decimal? GroundHeight { get; set; }
        /// <summary>
        ///  - 管线高程
        /// </summary>
        public decimal? LineHeight { get; set; }
        /// <summary>
        ///  - 管径或断面尺寸
        /// </summary>
        public string SectionSize { get; set; }
        /// <summary>
        ///  - 材质
        /// </summary>
        public string Material { get; set; }
        /// <summary>
        ///  - 埋设方式
        /// </summary>
        public string BuryType { get; set; }
        /// <summary>
        ///  - remark
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        ///  - CreateId
        /// </summary>
        public string CreateId { get; set; }
        /// <summary>
        ///  - CreateUser
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        ///  - CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        ///  - UpdateId
        /// </summary>
        public string UpdateId { get; set; }
        /// <summary>
        /// 修改人用户名 - UpdateUser
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 修改时间 - UpdateTime
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 项目名称Id
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentTypeId { get; set; }

    }
}
