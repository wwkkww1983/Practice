using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Model.Result.SystemManage
{

    public class Energy
    {
        public double? value { get; set; }
    }
    /// <summary>
    /// 光前点位平均温度
    /// </summary>
    public class FiberCure
    {
        public string PointName { get; set; }
        public List<FiberAverage> value { get; set; }
    }
    public class FiberAverage
    {
        public string AverageValue { get; set; }
        public DateTime? CreateTime { get; set; }
    }
    public class SystemEnergy
    {
        public string SystemName { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
    }


    public class AlarmCure
    {
        public string SystemName { get; set; }
        public string SystemId { get; set; }
        public List<AlarmCount> Data { get; set; }
    }
    public class AlarmCount
    {
        public int Count { get; set; }
        public string AlarmTime { get; set; }
    }

    public class CableInfo
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
    }

    public class SubStationInfo
    {
        public string DictId { get; set; }

        public string DictValue { get; set; }
        public int InstructType { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
