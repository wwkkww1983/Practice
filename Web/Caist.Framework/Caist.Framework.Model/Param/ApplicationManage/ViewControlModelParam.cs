using Caist.Framework.Model.Param.SystemManage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.Framework.Model.Param.ApplicationManage
{
    public class ViewControlModelListParam 
    {
        /// <summary>
        /// 控制模块名称
        /// </summary>
        public string ControlName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? ControlStutas { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }
    }
}
