/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/5/21 14:21:52
*说明:			 - CL_Equipment 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    ///  - Equipment
    /// </summary>
    public class Equipment : BaseEntity
    {
    
        /// <summary>
        /// 设备名称 - Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 设备类型编号 - EquipmentTypeId
        /// </summary>
        public string EquipmentTypeId { get; set; }
        /// <summary>
        /// 型号 - ModelNumber
        /// </summary>
        public string ModelNumber { get; set; }
        /// <summary>
        /// 设备编码 - ECode
        /// </summary>
        public string ECode { get; set; }
        /// <summary>
        /// 条码 - BarCode
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 重量 - Weight
        /// </summary>
        public decimal? Weight { get; set; }
        /// <summary>
        /// 用途 - USEINFO
        /// </summary>
        public string USEINFO { get; set; }
        /// <summary>
        /// 设备状态 - State
        /// </summary>
        public int? State { get; set; }
        /// <summary>
        /// 设备使用状态 - Enabled
        /// </summary>
        public int? Enabled { get; set; }
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

