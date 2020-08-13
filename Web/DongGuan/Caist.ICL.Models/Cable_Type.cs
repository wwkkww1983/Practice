using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Models
{
    public class Cable_Type:BaseEntity
    {
        /// <summary>
        /// 电缆类型
        /// </summary>
        public string CableType { get; set; }
        /// <summary>
        /// 电压等级 - Voltage_Type
        /// </summary>
        public string Voltage_Type { get; set; }
        /// <summary>
        /// 护套类型 - Sheath_Type
        /// </summary>
        public string Sheath_Type { get; set; }
        /// <summary>
        /// 电缆截面 - Fsection
        /// </summary>
        public string Fsection { get; set; }
        /// <summary>
        /// 最大牵引力 - Max_Traction
        /// </summary>
        public string Max_Traction { get; set; }
        /// <summary>
        /// 最大侧压力 - Max_Lateral_Pressure
        /// </summary>
        public string Max_Lateral_Pressure { get; set; }
        /// <summary>
        /// 电缆外径 - CableOd
        /// </summary>
        public string CableOd { get; set; }
        /// <summary>
        /// 电缆重量 - CableWeight
        /// </summary>
        public string CableWeight { get; set; }
        /// <summary>
        /// 电缆半径 - CableRadius
        /// </summary>
        public string CableRadius { get; set; }
        /// <summary>
        /// 工厂 - Factory
        /// </summary>
        public string Factory { get; set; }
        /// <summary>
        /// 是否分裂 - IsBreak
        /// </summary>
        public string IsBreak { get; set; }

    }
}
