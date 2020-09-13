using Caist.Framework.Model.Param.SystemManage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.Framework.Model.Param.ApplicationManage
{
    public class ViewParamenterListParam
    {
        /// <summary>
        /// 控制名称
        /// </summary>
        public string ParamenterName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? ParamenterStatus { get; set; }
    }
}
