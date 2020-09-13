using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.FiberManage
{
    [Serializable]
    [Table("mk_cable_thermometry")]
    public class CableEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id
        {
            get; set;
        }
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
        /// 
        /// </summary>
        public string MaxValuePos
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
        /// 
        /// </summary>
        public string MinValuePos
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
    }
}
