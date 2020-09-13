using System;
using System.Security.Permissions;

namespace Caist.Framework.Entity
{
    [Serializable]
    public class MqThemeEntity
    {

        [DBColumn(ColName = "id")]
        public long? Id { get; set; }
        /// <summary>
        /// ip
        /// </summary>
        [DBColumn(ColName = "mq_host")] 
        public string MqHost { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        [DBColumn(ColName = "mq_port")]
        public int? MqPort { get; set; }
        /// <summary>
        /// 客户端ID
        /// </summary>
        [DBColumn(ColName = "mq_clientid")]
        public string MqClientid { get; set; }
        /// <summary>
        /// 加密字符
        /// </summary>
        [DBColumn(ColName = "mq_encryption")]
        public string MqEncryption { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [DBColumn(ColName = "mq_user")]
        public string MqUser { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DBColumn(ColName = "mq_password")]
        public string MqPassword { get; set; }
        /// <summary>
        /// 集团
        /// </summary>
        [DBColumn(ColName = "mq_ctl")]
        public string MqCtl { get; set; }
        /// <summary>
        /// 集团编码
        /// </summary>
        [DBColumn(ColName = "mq_code")]
        public string MqCode { get; set; }
        /// <summary>
        /// 承建公司
        /// </summary>
        [DBColumn(ColName = "mq_name")]
        public string MqName { get; set; }
        /// <summary>
        /// 煤矿名称
        /// </summary>
        [DBColumn(ColName = "mq_colliery_name")]
        public string MqCollieryName { get; set; }
        /// <summary>
        /// 煤矿编码
        /// </summary>
        [DBColumn(ColName = "mq_colliery_code")]
        public string MqCollieryCode { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DBColumn(ColName = "mq_stutas")]
        public int? MqStutas { get; set; }

        [DBColumn(ColName = "last_update_time")]
        public DateTime? LastUpdateTime { get; set; }
    }
}
