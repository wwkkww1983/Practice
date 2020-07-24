/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/5/21 17:01:34
*说明:			监测点表 - MonitorPoint 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    /// 监测点表 - MonitorPoint
    /// </summary>
    public class MonitorPoint : BaseEntity
    {
    
        /// <summary>
        /// 工程ID - ProjectId
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 分段ID - SectionId
        /// </summary>
        public string SectionId { get; set; }
        /// <summary>
        /// 设备ID - EquipmentId
        /// </summary>
        public string EquipmentId { get; set; }
        /// <summary>
        ///  - TerminalId
        /// </summary>
        public string TerminalId { get; set; }
        /// <summary>
        /// 参数编码 - ParameterId
        /// </summary>
        public string ParameterId { get; set; }
        /// <summary>
        /// 位置 - Position
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// X坐标 - PositionX
        /// </summary>
        public decimal? PositionX { get; set; }
        /// <summary>
        /// Y坐标 - PositionY
        /// </summary>
        public decimal? PositionY { get; set; }
        /// <summary>
        /// Z坐标 - PositionZ
        /// </summary>
        public decimal? PositionZ { get; set; }
        /// <summary>
        /// 位置序号 - PositionSort
        /// </summary>
        public int? PositionSort { get; set; }
        /// <summary>
        /// 与前测点距离 - Distance
        /// </summary>
        public decimal? Distance { get; set; }
        /// <summary>
        /// 启用停机指令 - StopOrder
        /// </summary>
        public string StopOrder { get; set; }
        /// <summary>
        /// 启用开机指令 - StartOrder
        /// </summary>
        public string StartOrder { get; set; }
        /// <summary>
        /// 是否数据加密 - IsEncryption
        /// </summary>
        public string IsEncryption { get; set; }
        /// <summary>
        /// 是否数据加密 - IsEncryption
        /// </summary>
        public string TargetValue { get; set; }
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
    }
}

