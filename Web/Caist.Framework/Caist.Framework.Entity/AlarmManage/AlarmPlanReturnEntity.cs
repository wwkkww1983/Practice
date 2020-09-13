using Caist.Framework.Entity.SystemManage;
using System.Collections.Generic;

namespace Caist.Framework.Entity.AlarmManage
{
    public class AlarmPlanReturnEntity
    {
        public string Id { get; set; }
        /// <summary>
        /// 预警名称
        /// </summary>
        public string AlarmName { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        //[NotMapped]
        public string SysName { get; set; }
        /// <summary>
        /// 报警名称
        /// </summary>
        //[NotMapped]
        public string AlarmContent { get; set; }
        /// <summary>
        /// 是否启用(1:启用)
        /// </summary>
        public int Enable { get; set; }
        /// <summary>
        /// 附件列表
        /// </summary>

        public List<FileEntity> Files { get; set; }
        /// <summary>
        /// 逃生图
        /// </summary>

        public FileEntity Img { get; set; }
    }
}
