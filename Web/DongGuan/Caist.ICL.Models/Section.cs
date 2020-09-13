/****************************************************************************************************
*项目:			ICL 
*创建人:			zmq
*创建时间:		2019/8/4 9:10:52
*说明:			 - Section 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Models
{
    /// <summary>
    ///  - Section
    /// </summary>
    public class Section : BaseEntity
    {
    
        /// <summary>
        /// 分段编号 - SectionCode
        /// </summary>
        public string SectionCode { get; set; }
        /// <summary>
        /// 工程Id - ProjectId
        /// </summary>
        public string ProjectID { get; set; }
        /// <summary>
        /// 分段长度 - Distance
        /// </summary>
        public decimal? Distance { get; set; }
        /// <summary>
        /// 地理位置 - Location
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 分段状态 - State
        /// </summary>
        public int? State { get; set; }
        /// <summary>
        /// 设备间默认距离 - DefaultEquipmentDistance
        /// </summary>
        public decimal? DefaultEquipmentDistance { get; set; }
        /// <summary>
        /// 启用预警短信 - WarningSMS
        /// </summary>
        public int? WarningSMS { get; set; }
        /// <summary>
        ///  - NowSection
        /// </summary>
        public int? NowSection { get; set; }
        /// <summary>
        /// 接收短信手机号列表 - Phones
        /// </summary>
        public string Phones { get; set; }
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

