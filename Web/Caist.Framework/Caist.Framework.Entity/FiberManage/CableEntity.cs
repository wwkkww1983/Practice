using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.FiberManage
{
    [Serializable]
    [Table("mk_cable_thermometry")]
    public class CableEntity:BaseEntity
    {

        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName
        {
            get; set;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxValue
        {
            get; set;
        }


        /// <summary>
        /// 最小值
        /// </summary>
        public string MinValue
        {
            get; set;
        }

        /// <summary>
        /// 平均值
        /// </summary>
        public string AverageValue       
        {
            get; set;
        }
        /// <summary>
        /// 当前值
        /// </summary>
        public string CurrentTemperature
        {
            get; set;
        }
    }
}
