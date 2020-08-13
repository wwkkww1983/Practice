using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Core.Models
{
    /// <summary>
    /// 线
    /// </summary>
    public class Line
    {
        /// <summary>
        /// 上一个牵引力T1
        /// </summary>
        public double prevPullPower = 0;

        /// <summary>
        /// 重力W
        /// </summary>
        public double weight = 0;

        /// <summary>
        /// 长度L1
        /// </summary>
        public double length1 = 0;

        /// <summary>
        /// 长度L5
        /// </summary>
        public double length5 = 0;

        /// <summary>
        /// 摩擦系数μ
        /// </summary>
        public double friction = 0;

        /// <summary>
        /// 转弯角度θ1，向下
        /// </summary>
        public double angleDown = 0;

        /// <summary>
        /// 转弯角度θ2，向上
        /// </summary>
        public double angleUp = 0;

        /// <summary>
        /// 转弯半径R
        /// </summary>
        public double radius = 0;
    }
}
