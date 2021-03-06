namespace Caist.Framework.Entity.Entity
{
    public class AlarmEntity
    {
        [DBColumn(ColName = "id")]
        public long Id { get; set; }
        [DBColumn(ColName = "ModuleId")]
        public long ModuleId { get; set; }
        [DBColumn(ColName = "system_name")]
        public string SystemName { get; set; }
        /// <summary>
        /// 异常消息名
        /// </summary>
        [DBColumn(ColName = "broadcast_content")]
        public string BroadCastContent { get; set; }
        /// <summary>
        /// 内存地址
        /// </summary>
        [DBColumn(ColName = "manipulate_model_mark")]
        public string ManipulateModelMark { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        [DBColumn(ColName = "min_value")]
        public int MinValue { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        [DBColumn(ColName = "max_value")]
        public int MaxValue { get; set; }
        /// <summary>
        /// 模块类型
        /// </summary>
        [DBColumn(ColName = "manipulate_model_name")]
        public string ManipulateModelName { get; set; }
    }
}
