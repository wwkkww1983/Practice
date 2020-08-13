using Caist.Framework.IdGenerator;

namespace SyncModel
{
    public class DataBaseModel
    {
        public DataEmun SourceDBType { get; set; }
        public string SourceDB { get; set; }
        public string SourceTable { get; set; }
        public string SourceDBConnStr { get; set; }
        public string SourceSql { get; set; }

        public DataEmun TargetDBType { get; set; }
        public string TargetDB { get; set; }
        public string TargetTable { get; set; }
        public string TargetDBConnStr { get; set; }

        /// <summary>
        /// 增量同步，还是全量同步(true:增量；false:全量)
        /// </summary>
        public bool SyncPartial { get; set; }
        /// <summary>
        /// 记录最后同步的关键字段
        /// </summary>
        public string FlagField { get; set; }
        public string[] TableFields { get; set; }
        /// <summary>
        /// 备用属性
        /// </summary>
        public string[] SourceTableFields { get; set; }
        /// <summary>
        /// 备用属性
        /// </summary>
        public string[] TargetTableFields { get; set; }
    }
}
