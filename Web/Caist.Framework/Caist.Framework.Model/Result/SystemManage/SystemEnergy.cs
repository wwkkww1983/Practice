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
    public class FiberAverage{
        public string AverageValue { get; set; }
        public DateTime? CreateTime { get; set; }
    }
    public class SystemEnergy
    {
        public string SystemName { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
    }


}
