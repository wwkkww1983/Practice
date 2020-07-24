/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/4/30 13:30:04
*说明:			地理坐标 - Coordinate 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    /// 地理坐标 - Coordinate
    /// </summary>
    public class Coordinate : BaseEntity
    {
    
        /// <summary>
        /// 控制单元 - control_unit_id
        /// </summary>
        public string control_unit_id { get; set; }
        /// <summary>
        ///  - point_x
        /// </summary>
        public decimal? point_x { get; set; }
        /// <summary>
        ///  - ponit_y
        /// </summary>
        public decimal? ponit_y { get; set; }
        /// <summary>
        ///  - point_z
        /// </summary>
        public decimal? point_z { get; set; }
        /// <summary>
        /// 敷设路径分段id - laying_path
        /// </summary>
        public string laying_path { get; set; }
        /// <summary>
        /// 备注 - frmark
        /// </summary>
        public string frmark { get; set; }
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

