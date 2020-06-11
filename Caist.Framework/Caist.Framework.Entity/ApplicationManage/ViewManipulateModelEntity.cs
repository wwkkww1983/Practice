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
    }
}
