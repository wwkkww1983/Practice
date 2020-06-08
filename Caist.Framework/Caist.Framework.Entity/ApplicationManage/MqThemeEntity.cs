using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.ApplicationManage
{
    [Table("mk_mq_theme")]
    public class MqThemeEntity : BaseExtensionEntity
    {
        /// <summary>
        /// ip
        /// </summary>
        public string MqHost { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int? MqPort { get; set; }
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string MqClientid { get; set; }
        /// <summary>
        /// 加密字符
        /// </summary>
        public string MqEncryption { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string MqUser { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string MqPassword { get; set; }
        /// <summary>
        /// 集团
        /// </summary>
        public string MqCtl { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string MqCode { get; set; }
        /// <summary>
        /// 承建公司
        /// </summary>
        public string MqName { get; set; }
        /// <summary>
        /// 煤矿名称
        /// </summary>
        public string MqCollieryName { get; set; }
        /// <summary>
        /// 煤矿编码
        /// </summary>
        public string MqCollieryCode { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? MqStutas { get; set; }
    }
}
