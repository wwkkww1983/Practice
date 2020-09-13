using Caist.Framework.Util;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.ApplicationManage
{
    [Table("mk_view_paramenter")]
    public class ViewParamenterEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 控制模块ID
        /// </summary>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? ViewControlModelId { get; set; }
        /// <summary>
        /// 控制名称
        /// </summary>
        public string ParamenterName { get; set; }
        /// <summary>
        /// 指令
        /// </summary>
        public string ParamenterInstruct { get; set; }
        /// <summary>
        /// 启动
        /// </summary>
        public string ParamenterInstructStart { get; set; }
        /// <summary>
        /// 停止
        /// </summary>
        public string ParamenterInstructEnd { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string ParamenterUnit { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? ParamenterStatus { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? ParamenterSort { get; set; }
        /// <summary>
        /// ip
        /// </summary>
        public string ParamenterIp { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public string ParamenterPort { get; set; }
        /// <summary>
        /// 控制模块值得类型  如:bool  int  real
        /// </summary>
        public string ParamenterValueType { get; set; }
        /// <summary>
        /// 控制模块的值
        /// </summary>
        public string ParamenterValue { get; set; }

        /// <summary>
        /// 控制模块指定对应的模型动画索引值  按照0,1 格式组成
        /// </summary>
        public string Animation { get; set; }
        /// <summary>
        /// 控制模式
        /// </summary>
        public string ControlModels { get; set; }

        /// <summary>
        /// 控制模块
        /// </summary>
        [NotMapped]
        public string ControlName { get; set; }
    }
}
