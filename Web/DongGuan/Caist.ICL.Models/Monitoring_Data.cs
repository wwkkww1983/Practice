/****************************************************************************************************
*项目:			ICL 
*创建人:			zmq
*创建时间:		2019/8/4  17:09:03
*说明:			监测数据 - Monitoring_Data 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Models
{
    /// <summary>
    /// 监测数据 - Monitoring_Data
    /// </summary>
    public class Monitoring_Data : BaseEntity
    {
    
        /// <summary>
        /// 时间 - ftime
        /// </summary>
        public DateTime? ftime { get; set; }
        /// <summary>
        /// 指标ID - PIndexId
        /// </summary>
        public string PIndexId { get; set; }
        /// <summary>
        /// 测点ID - MonitorPointId
        /// </summary>
        public string MonitorPointId { get; set; }
        /// <summary>
        /// 值 - fdata
        /// </summary>
        public decimal? fdata { get; set; }
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

