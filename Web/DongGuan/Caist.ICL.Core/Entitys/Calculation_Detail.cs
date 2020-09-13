/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/5/21 14:21:52
*说明:			计算结果明细 - Calculation_Detail 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    /// 计算结果明细 - Calculation_Detail
    /// </summary>
    public class Calculation_Detail : BaseEntity
    {
    
        /// <summary>
        ///  - point_x
        /// </summary>
        public decimal? point_x { get; set; }
        /// <summary>
        ///  - point_y
        /// </summary>
        public decimal? point_y { get; set; }
        /// <summary>
        ///  - point_z
        /// </summary>
        public decimal? point_z { get; set; }
        /// <summary>
        ///  - data_value
        /// </summary>
        public decimal? data_value { get; set; }
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

