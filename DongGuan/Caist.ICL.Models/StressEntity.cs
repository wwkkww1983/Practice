using System.Collections.Generic;

namespace Caist.ICL.Models
{
    /// <summary>
    /// 受力分析实体类
    /// ljl@2018
    /// </summary>
    public class StressEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int Index { get; set; }
        public List<object> values { get; set; }
        public List<object> dd { get; set; }
        public string StartX { get; set; }
        public string StartY { get; set; }
        public string EndX { get; set; }
        public string EndY { get; set; }
    }
}
