using Caist.Framework.Mqtt.Topic;
using System.Collections.Generic;
using System.Linq;

namespace Caist.Framework.Mqtt.Extensions
{
    public static class IMqttTopicExtensions
    {
        public static IEnumerable<IMqttTopic> Concat(this IMqttTopic topic, params IMqttTopic[] topics)
        {
            return new List<IMqttTopic> { topic }.Union(topics);
        }

        public static string Build(this IEnumerable<IMqttTopic> topics)
        {
            return string.Join("/", topics
                .Where(x => x != null)
                .Select(x => x.Build().Trim('/'))
                .Where(x => string.IsNullOrWhiteSpace(x) == false));
        }

        public static string Build(this IMqttTopic topic, params IMqttTopic[] topics)
        {
            return topic.Concat(topics).Build();
        }
    }
}
