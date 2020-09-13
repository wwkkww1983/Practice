/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/5/21 17:02:41
*说明:			 - Laying_Path 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    ///  - Laying_Path
    /// </summary>
    public class Laying_Path : BaseEntity
    {

        /// <summary>
        /// 敷设路径名称 - SName
        /// </summary>
        public string SName { get; set; }
        /// <summary>
        /// 所属工程 - ProjectId
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 分段 - SectionId
        /// </summary>
        public string SectionId { get; set; }
        /// <summary>
        /// 电缆类型 - CableType
        /// </summary>
        public string CableType { get; set; }
        /// <summary>
        /// 敷设长度 - flength
        /// </summary>
        public decimal? flength { get; set; }
        /// <summary>
        /// 敷设方式 - Laying_Type
        /// </summary>
        public string Laying_Type { get; set; }
        /// <summary>
        /// 起点地名 - StartAddress
        /// </summary>
        public string StartAddress { get; set; }
        /// <summary>
        /// 起点经度 - StartPoint_X
        /// </summary>
        public decimal? StartPoint_X { get; set; }
        /// <summary>
        /// 起点纬度 - StartPoint_Y
        /// </summary>
        public decimal? StartPoint_Y { get; set; }
        /// <summary>
        /// 起点高程 - StartPoint_Z
        /// </summary>
        public decimal? StartPoint_Z { get; set; }
        /// <summary>
        /// 终点名称 - EndAddress
        /// </summary>
        public string EndAddress { get; set; }
        /// <summary>
        /// 终点经度 - EndPoint_X
        /// </summary>
        public decimal? EndPoint_X { get; set; }
        /// <summary>
        /// 终点纬度 - EndPoint_Y
        /// </summary>
        public decimal? EndPoint_Y { get; set; }
        /// <summary>
        /// 终点高度 - EndPoint_Y
        /// </summary>
        public decimal? EndPoint_Z { get; set; }

        /// <summary>
        ///  - fremark
        /// </summary>
        public string fremark { get; set; }
        /// <summary>
        ///  - 湾曲半径
        /// </summary>
        public string Radius { get; set; }
        /// <summary>
        ///  - 牵引力值
        /// </summary>
        public string PullValue { get; set; }
        /// <summary>
        ///  - 侧压力值
        /// </summary>
        public string SideValue { get; set; }
        /// <summary>
        ///  - 是否合格
        /// </summary>
        public string IsQualified { get; set; }
        /// <summary>
        ///  - 建议措施
        /// </summary>
        public string Solutions { get; set; }
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

