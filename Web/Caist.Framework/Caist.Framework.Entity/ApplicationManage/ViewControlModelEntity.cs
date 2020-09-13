using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caist.Framework.Entity.ApplicationManage
{
    [Table("mk_view_control_model")]
    public class ViewControlModelEntity: BaseExtensionEntity
    {
        /// <summary>
        /// 视图ID
        /// </summary>
        public long? ViewFunctionId { get; set; }
        /// <summary>
        /// 控制模块名称
        /// </summary>
        public string ControlName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? ControlStutas { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int? ControlSort { get; set; }

        /// <summary>
        /// 视图名称
        /// </summary>
        [NotMapped]
        public string ViewName { get; set; }
    }
}
