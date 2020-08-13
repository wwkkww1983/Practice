/****************************************************************************************************
*项目:			ICL 
*创建人:			zmq
*创建时间:		2019/8/4  13:30:04
*说明:			设备力学属性 - Device_Mechanics 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Models
{
    /// <summary>
    /// 设备力学属性 - Device_Mechanics
    /// </summary>
    public class Device_Mechanics : BaseEntity
    {
    
        /// <summary>
        /// 控制单元 - control_unit_id
        /// </summary>
        public string control_unit_id { get; set; }
        /// <summary>
        /// 科目 - subject_type
        /// </summary>
        public int? subject_type { get; set; }
        /// <summary>
        /// 值 - data_value
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

