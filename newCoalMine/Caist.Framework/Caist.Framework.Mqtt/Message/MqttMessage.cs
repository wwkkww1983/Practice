using Caist.Framework.Mqtt.Extensions;
using Newtonsoft.Json;
using System;

namespace Caist.Framework.Mqtt.Message
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MqttMessage : MqttDeformMessage, IMqttPayload<byte[]>
    {
        /// <summary>
        /// 消息体
        /// </summary>
        [JsonProperty("payload")]
        public byte[] Payload { get; set; }

        public override string ToString()
        {
            return $"topic:{Topic}{Environment.NewLine}payload:{this.ConvertPayloadToString()}";
        }
    }
}
