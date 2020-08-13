/****************************************************************************************************
*项目:			ICL 
*创建人:			zmq
*创建时间:		2019/8/4  14:21:53
*说明:			 - Formula_Detail 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Models
{
    /// <summary>
    ///  - Formula_Detail
    /// </summary>
    public class Formula_Detail : BaseEntity
    {
    
        /// <summary>
        /// 版本 - version
        /// </summary>
        public int? version { get; set; }
        /// <summary>
        /// 表达式 - fexpression
        /// </summary>
        public string fexpression { get; set; }
        /// <summary>
        /// 类文件 - class_name
        /// </summary>
        public string class_name { get; set; }
        /// <summary>
        /// XML文件 - xml_name
        /// </summary>
        public string xml_name { get; set; }
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

