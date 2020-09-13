using NPOI.SS.Formula.Functions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.EventRecordManage
{
    [Table("mk_Event_Record")]
    public class EventRecordEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 操作模块名称
        /// </summary>
        public string InstructName { get; set; }
        /// <summary>
        /// 操作的数值  比如开启：0 关闭：1
        /// </summary>
        public string InstructValue { get; set; }
        /// <summary>
        /// 操作人ID   当前登录用户ID
        /// </summary>
        public string Operator { get; set; }
        public DateTime OperatorTime { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }
        /// <summary>
        /// 操作模式   远程：0  就地：1
        /// </summary>
        public int? OperationModel { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [NotMapped]
        public string RealName { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [NotMapped]
        public string SystemName{ get; set; }

    }
}
