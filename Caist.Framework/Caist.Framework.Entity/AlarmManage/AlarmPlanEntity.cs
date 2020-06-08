using Caist.Framework.Entity.SystemManage;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.AlarmManage
{
    [Table("mk_alarm_plan")]
    public class AlarmPlanEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 预警名称
        /// </summary>
        public string AlarmName { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        public long? SysId { get; set; }
        /// <summary>
        /// 报警字段
        /// </summary>
        public long? AlarmField { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否启用(1:启用)
        /// </summary>
        public int Enable { get; set; }
    }

    public class AlarmPlanParam
    {
        public long? Id { get; set; }
        /// <summary>
        /// 文件集合
        /// </summary>
        public List<FileEntity> Files { get; set; }
        /// <summary>
        /// 预警名称
        /// </summary>
        public string AlarmName { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        public long? SysId { get; set; }
        /// <summary>
        /// 报警字段
        /// </summary>
        public long? AlarmField { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否启用(1:启用)
        /// </summary>
        public int Enable { get; set; }
    }
}
