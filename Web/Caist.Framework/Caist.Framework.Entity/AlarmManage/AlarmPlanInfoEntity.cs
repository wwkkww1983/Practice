using Caist.Framework.Entity.SystemManage;
using Caist.Framework.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.AlarmManage
{
    public class AlarmPlanInfoEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 预警名称
        /// </summary>
        public string AlarmName { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? SysId { get; set; }

        /// <summary>
        /// 报警字段
        /// </summary>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? AlarmField { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemNickName { get; set; }

        /// <summary>
        /// 路线图
        /// </summary>
        public string SystemImage { get; set; }


        /// <summary>
        /// 是否启用(1:启用)
        /// </summary>
        public int Enable { get; set; }

        /// <summary>
        /// 报警预案文件列表
        /// </summary>
        public List<FileEntity> FileList { get; set; }
    }
}
