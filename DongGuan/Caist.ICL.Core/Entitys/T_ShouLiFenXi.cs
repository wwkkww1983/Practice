using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Core.Entitys
{

    /// <summary>
    /// 受力分析实体
    /// </summary>
    public class T_ShouLiFenXi:BaseEntity
    {
        // [Section]   [Lengths]         [BuryType]      [ForceValue1]     [ForceValue2]     [IfQualify]      
        //[ProposedProg] [Material] [StartX] [StartY] [StartZ] [EndX] [EndY] [EndZ] [codes]
        /// <summary>
        /// 分段 - SectionId
        /// </summary>
        public string Section { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public string Lengths { get; set; }
        /// <summary>
        /// 敷设方式
        /// </summary>
        public string BuryType { get; set; }
        /// <summary>
        /// 侧压力
        /// </summary>
        public string ForceValue1 { get; set; }
        /// <summary>
        /// 牵引力
        /// </summary>
        public string ForceValue2 { get; set; }
        /// <summary>
        /// 是否合格
        /// </summary>
        public string IfQualify { get; set; }
        /// <summary>
        /// 建议控制措施
        /// </summary>
        public string ProposedProg { get; set; }
        /// <summary>
        /// 材质
        /// </summary>
        public string Material { get; set; }
        /// <summary>
        /// 起点X
        /// </summary>
        public string StartX { get; set; }
        /// <summary>
        /// 起点Y
        /// </summary>
        public string StartY { get; set; }
        /// <summary>
        /// 起点Z
        /// </summary>
        public string StartZ{ get; set; }
        /// <summary>
        /// 终点X
        /// </summary>
        public string EndX { get; set; }
        /// <summary>
        /// 终点Y
        /// </summary>
        public string EndY { get; set; }
        /// <summary>
        /// 终点Z
        /// </summary>
        public string EndZ { get; set; }


    }
}
