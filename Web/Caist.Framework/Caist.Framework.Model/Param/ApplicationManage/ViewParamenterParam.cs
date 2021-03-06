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

        /// <summary>
        /// 系统模块ID
        /// </summary>
        public long? SystemId { get; set; }

        /// <summary>
        /// 控制类型
        /// </summary>
        public string ControlModels { get; set; }
        /// <summary>
        /// 所属控制模块
        /// </summary>
        public long? ViewControlModelId { get; set; }
    }
}
