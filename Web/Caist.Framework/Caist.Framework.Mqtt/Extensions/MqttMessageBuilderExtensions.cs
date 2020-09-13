using Caist.Framework.Mqtt.Message;
using Newtonsoft.Json;

namespace Caist.Framework.Mqtt.Extensions
{
    public static class MqttMessageBuilderExtensions
    {
        public static MqttMessageBuilder WithPayload<T>(this MqttMessageBuilder builder, T obj) where T : class
        {
            return builder.WithPayload(JsonConvert.SerializeObject(obj));
        }
    }
}
