using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.Framework.Mqtt.Message
{
    public enum MqttQosLevel
    {
        /// <summary>
        /// 至多一次
        /// </summary>
        AtMostOnce = 0,

        /// <summary>
        /// 至少一次
        /// </summary>
        AtLeastOnce = 1,

        /// <summary>
        /// 只有一次
        /// </summary>
        ExactlyOnce = 2
    }
}
