namespace Caist.Framework.Entity.Entity
{
    public class AlarmEntity
    {
        [DBColumn(ColName = "SysId")]
        public long SysId { get; set; }
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

    public class AlertEntity
    {
        [DBColumn(ColName = "itemId")]
        public long ItemId { get; set; }
        [DBColumn(ColName = "systemId")]
        public long SystemId { get; set; }
        [DBColumn(ColName = "id")]
        public long ModuleId { get; set; }
        [DBColumn(ColName = "system_name")]
        public string SystemName { get; set; }
        [DBColumn(ColName = "system_nick_name")]
        public string SystemNickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "view_name")]
        public string ViewName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "manipulate_model_name")]
        public string ManipulateModelName { get; set; }
        /// <summary>
        /// 指令
        /// </summary>
        [DBColumn(ColName = "command")]
        public string Command { get; set; }
    }
}
