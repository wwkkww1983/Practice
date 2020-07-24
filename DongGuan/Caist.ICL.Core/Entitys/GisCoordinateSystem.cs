/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/5/21 17:08:31
*说明:			地理坐标系 - GisCoordinateSystem 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    /// 地理坐标系 - GisCoordinateSystem
    /// </summary>
    public class GisCoordinateSystem : BaseEntity
    {
    
        /// <summary>
        /// 坐标系统名 - CoordinateName
        /// </summary>
        public string CoordinateName { get; set; }
        /// <summary>
        /// 地名 - place_name
        /// </summary>
        public string place_name { get; set; }
        /// <summary>
        /// 参照点x - frame_x
        /// </summary>
        public decimal? frame_x { get; set; }
        /// <summary>
        /// 参照点y - frame_y
        /// </summary>
        public decimal? frame_y { get; set; }
        /// <summary>
        /// 参照点z - frame_z
        /// </summary>
        public decimal? frame_z { get; set; }
        /// <summary>
        /// 备注 - fremark
        /// </summary>
        public string fremark { get; set; }
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
        /// 是否删除 - Delteted
        /// </summary>
        public bool? Delteted { get; set; }
    }
}

