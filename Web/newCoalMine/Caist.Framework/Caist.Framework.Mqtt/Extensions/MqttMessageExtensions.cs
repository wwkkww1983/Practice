using Caist.Framework.Mqtt.Message;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Caist.Framework.Mqtt.Extensions
{
    public static class MqttMessageExtensions
    {
        public static T ConvertPayloadToJson<T>(this MqttMessage message)
        {
            var str = message.ConvertPayloadToString();
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static object ConvertPayloadToObject(this MqttMessage message)
        {
            var str = message.ConvertPayloadToString();
            return JsonConvert.DeserializeObject(str);
        }

        public static string ConvertPayloadToString(this MqttMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (message.Payload == null)
            {
                return null;
            }

            if (message.Payload.Length == 0)
            {
                return string.Empty;
            }

            return Encoding.UTF8.GetString(message.Payload, 0, message.Payload.Length);
        }

        public static MqttMessage ConvertToMessage(this MqttJsonPayloadMessage message)
        {
            return message.ConvertToMessageBuilder().Build();
        }

        public static MqttMessageBuilder ConvertToMessageBuilder(this MqttJsonPayloadMessage message)
        {
            return new MqttMessageBuilder()
               .WithTopic(message.Topic)
               .WithRetainFlag(message.Retain)
               .WithPayload(message.Payload)
               .WithQos(message.Qos)
               ;
        }
    }
}
