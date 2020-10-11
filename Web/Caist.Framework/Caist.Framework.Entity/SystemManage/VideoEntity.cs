using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.SystemManage
{
    [Table("mk_eb_video")]
    public class VideoEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 视频区域名称
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Number
        {
            get; set;
        }

        /// <summary>
        /// 视频唯一标识值
        /// </summary>
        public string Gid
        {
            get; set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder
        {
            get; set;
        }

        /// <summary>
        /// 视频请求地址
        /// </summary>
        public string Url
        {
            get; set;
        }
        /// <summary>
        /// 所属区域
        /// </summary>
        public string Area
        {
            get; set;
        }
        /// <summary>
        /// 视频拉流通道编号
        /// </summary>
        public string ChannelNumber
        {
            get; set;
        }
    }
}
