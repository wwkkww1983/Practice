using Caist.Framework.Enum.SystemManage;
using Caist.Framework.Model.Param.SystemManage;
using System;

namespace Caist.Framework.Model.Param.OrganizationManage
{
    public class SystemDataParam
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }
        /// <summary>
        /// 点位
        /// </summary>
        public string DictId { get; set; }

       
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 数据类型  1:日数据  2： 1周数据 3：1个月数据 4：1个季度数据（3个月） 5：1年数据
        /// </summary>
        public CycleEnum Cycle { get; set; }
        /// <summary>
        /// 根据系统ID查询出指定子系统的表名作为参数传入查询详细数据
        /// </summary>
        public string TabName { get; set; }
    }
}
