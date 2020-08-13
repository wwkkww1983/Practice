using Newtonsoft.Json;
using System;

namespace Caist.Framework.Mqtt.Message
{
    [Serializable]
    public class MqttJsonPayloadMessage : MqttDeformMessage, IMqttPayload<string>
    {
        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
