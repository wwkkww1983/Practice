/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/4/30 13:30:04
*说明:			设备部署表 - Device_Deployment 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    /// 设备部署表 - Device_Deployment
    /// </summary>
    public class Device_Deployment : BaseEntity
    {
    
        /// <summary>
        /// 控制单元 - control_unit_id
        /// </summary>
        public string control_unit_id { get; set; }
        /// <summary>
        /// 工程设备id - device_id
        /// </summary>
        public string device_id { get; set; }
        /// <summary>
        /// 开始部署时间 - start_time
        /// </summary>
        public DateTime? start_time { get; set; }
        /// <summary>
        /// 设备位置X - device_x
        /// </summary>
        public decimal? device_x { get; set; }
        /// <summary>
        /// 设备位置Y - device_y
        /// </summary>
        public decimal? device_y { get; set; }
        /// <summary>
        /// 设备位置Z - device_z
        /// </summary>
        public decimal? device_z { get; set; }
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

