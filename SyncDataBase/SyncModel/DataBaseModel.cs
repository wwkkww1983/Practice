using SyncCommon;

namespace SyncModel
{
    public class DataBaseModel
    {
        public DataBaseType sourceDBType { get; set; }
        public string sourceDB { get; set; }
        public string sourceTable { get; set; }
        public string sourceDBConnStr { get; set; }

        public DataBaseType targetDBType { get; set; }
        public string targetDB { get; set; }
        public string targetTable { get; set; }
        public string targetDBConnStr { get; set; }

        /// <summary>
        /// 增量同步，还是全量同步(true:增量；false:全量)
        /// </summary>
        public bool syncType { get; set; }
        /// <summary>
        /// 记录最后同步的关键字段
        /// </summary>
        public string flagField { get; set; }
        public string[] tableFields { get; set; }
        public string[] sourceTableFields { get; set; }
        public string[] targetTableFields { get; set; }
    }
}
