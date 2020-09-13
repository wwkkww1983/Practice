namespace Caist.Framework.Entity.Entity
{

    public class HistoryEntity
    {
        public string DictId { get; set; }
        public string SysId { get; set; }
        public string DictValue { set; get; }
        /// <summary>
        /// 指令类型（0:数据;1:控制;2:告警;3:启动;4:停止;）
        /// </summary>
        public int? InstructType { set; get; }
        public string TabName { get; set; }
    }
}
