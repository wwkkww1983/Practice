using Caist.Framework.Model.Param.SystemManage;
using System;

namespace Caist.Framework.Model.Param.EventRecordManage
{
    public class EventRecordListParam
    {
        public string InstructName { get; set; }
        public string InstructValue { get; set; }
        /// <summary>
        /// 作为写入实体参数的时候传用户ID ，作为查询参数的时候传用户的昵称
        /// </summary>
        public string Operator { get; set; }

        public string OperationModel { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
