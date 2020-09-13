using Caist.Framework.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caist.Framework.Entity.ApplicationManage
{
    [Table("mk_view_manipulate_model")]
    public class ViewManipulateModelEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 视图ID
        /// </summary>
        [JsonConverter(typeof(StringJsonConverter))]
		public long? ViewFunctionId { get; set; }
        [NotMapped]
        public string SystemId { get; set; }


        /// <summary>
        /// 控制模块
        /// </summary>
        public string ManipulateModelName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? ManipulateModelSort { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? ManipulateModelStutas { get; set; }
        /// <summary>
        /// 是否显示首页
        /// </summary>
        public int? ManipulateModelShowHome { get; set; }
        /// <summary>
        /// 标识
        /// </summary>
        public string ManipulateModelMark { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string ManipulateModelUnit { get; set; }

        /// <summary>
        /// 视图名称
        /// </summary>
        [NotMapped]
        public string ViewName { get; set; }

        /// <summary>
        /// 视图对应PLC点位值  提供给查询某个视图下，的比如  1号皮带-电压-值   ViewName-ManipulateModelName-ViewValue
        /// </summary>
        [NotMapped]
        public string ViewValue { get; set; }
    }
}
