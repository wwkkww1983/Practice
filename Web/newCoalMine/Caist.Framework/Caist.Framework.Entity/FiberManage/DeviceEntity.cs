using System;

namespace Caist.Framework.Entity.FiberManage
{
    [Serializable]
    public class DeviceEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 分站ID
        /// </summary>
        public string DeviceID
        {
            get; set;
        }

        /// <summary>
        /// 分站名称
        /// </summary>
        public string DeviceName
        {
            get; set;
        }

        /// <summary>
        /// 分站IP
        /// </summary>
        public string DeviceIP
        {
            get; set;
        }

        /// <summary>
        /// 分站位置
        /// </summary>
        public string DevicePosition
        {
            get; set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string connflag
        {
            get; set;
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string uptime
        {
            get; set;
        }

    }
}
