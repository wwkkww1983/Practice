using Newtonsoft.Json;
using System;

namespace Caist.Framework.Mqtt.Message
{
    /// <summary>
    /// 部分Mqtt消息结构
    /// </summary>
    [Serializable]
    public class MqttDeformMessage
    {
        /// <summary>
        /// 主题
        /// </summary>
        [JsonProperty("topic")]
        public string Topic { get; set; }

        /// <summary>
        /// 保留消息
        /// </summary>
        [JsonProperty("retain")]
        public bool Retain { get; set; }

        /// <summary>
        /// 消息质量
        /// </summary>
        [JsonProperty("qos")]
        public MqttQosLevel Qos { get; set; }
    }
}
