/****************************************************************************************************
*项目:			ICL 
*创建人:			zmq
*创建时间:		2019/8/4 9:10:52
*说明:			电缆信息 - Cable_Info 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Models
{
    /// <summary>
    /// 电缆信息 - Cable_Info
    /// </summary>
    public class Cable_Info : BaseEntity
    {
    
        /// <summary>
        /// 电缆编号 - CLNumber
        /// </summary>
        public string CLNumber { get; set; }
        /// <summary>
        /// 分段 - SectionId
        /// </summary>
        public string SectionId { get; set; }
        /// <summary>
        /// <summary>
        ///  - TotelLenth
        /// </summary>
        public decimal? TotelLenth { get; set; }
        /// <summary>
        ///  - CurrentLenth
        /// </summary>
        public decimal? CurrentLenth { get; set; }
        /// <summary>
        /// 电压等级 - Voltage_Type
        /// </summary>
        public int? Voltage_Type { get; set; }
        /// <summary>
        /// 护套类型 - Sheath_Type
        /// </summary>
        public int? Sheath_Type { get; set; }
        /// <summary>
        /// 电缆截面 - Fsection
        /// </summary>
        public decimal? Fsection { get; set; }
        /// <summary>
        /// 最大牵引力 - Max_Traction
        /// </summary>
        public decimal? Max_Traction { get; set; }
        /// <summary>
        /// 最大侧压力 - Max_Lateral_Pressure
        /// </summary>
        public decimal? Max_Lateral_Pressure { get; set; }
        /// <summary>
        /// 使用状态 0=未使用  1=在使用 - UserStatus
        /// </summary>
        public int? UserStatus { get; set; }
        /// <summary>
        /// 备注 - Remark
        /// </summary>
        public string Remark { get; set; }
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
        /// <summary>
        /// 工程ID - ProjectID
        /// </summary>
        public string ProjectID { get; set; }
        /// <summary>
        /// 电缆类型ID
        /// </summary>
        public string CableTypeID { get; set; }
    }
}

