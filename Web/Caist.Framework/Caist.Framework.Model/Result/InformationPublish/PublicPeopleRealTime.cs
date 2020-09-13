using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Model.Result.SystemManage
{
    public class PublicPeopleRealTime
    {
        public string CurrentStation { get; set; }
        /// <summary>
        /// 区域 位置
        /// </summary>
        public string StationAddress { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string PepoleNumber { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string PepoleName { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string TypeOfWorkName { get; set; }
    }
}
