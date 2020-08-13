/****************************************************************************************************
*项目:			ICL 
*创建人:			zmq
*创建时间:		2019/8/4  13:30:04
*说明:			设备类型 - Device_Type 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Models
{
    /// <summary>
    /// 设备类型 - Device_Type
    /// </summary>
    public class Device_Type : BaseEntity
    {
    
        /// <summary>
        /// 控制单元 - control_unit_id
        /// </summary>
        public string control_unit_id { get; set; }
        /// <summary>
        /// 名称 - fname
        /// </summary>
        public string fname { get; set; }
        /// <summary>
        /// 编号 - fnumber
        /// </summary>
        public string fnumber { get; set; }
        /// <summary>
        /// 父类型 - sys_pid
        /// </summary>
        public string sys_pid { get; set; }
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

