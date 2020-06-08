using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.ApplicationManage
{
    [Table("mk_system_setting")]
    public class SystemSettingEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        /// <returns></returns>
        public string SystemName { get; set; }
        /// <summary>
        /// 系统昵称
        /// </summary>
        /// <returns></returns>
        public string SystemNickName { get; set; }
        /// <summary>
        /// 系统图标
        /// </summary>
        public string SystemImage { get; set; }
        /// <summary>
        /// 系统3D模型
        /// </summary>
        /// <returns></returns>
        public string SystemModel { get; set; }
        /// <summary>
        /// 访问地址
        /// </summary>
        /// <returns></returns>
        public string SystemUrl { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        public int? SystemSort { get; set; }
    }
}
